using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Stores data about all pending downloads and the overall progress
    /// </summary>
    public class DownloadManager
    {
        static string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
        string downloadsPath = Path.Combine(applicationPath, "Downloads");
        public string DownloadsPath { get { return downloadsPath; } }

        object pendingLocker = new object();
        object activeLocker = new object();
        private List<Download> pendingDownloads;
        private List<Download> activeDownloads;
        private Downloader downloader;

        ThreadHandler threadHandler;

        /// <summary>
        /// Construct a DownloadManager which can add pending downloads, and create threads to start downloading them
        /// </summary>
        /// <param name="threadHandler">The main ThreadHandler of the application</param>
        /// /// <param name="applicationInterface">The frontend interface of the application</param>
        public DownloadManager(ThreadHandler threadHandler, iGui applicationInterface)
        {
            this.threadHandler = threadHandler;
            this.downloader = new Downloader(threadHandler, applicationInterface, this);
            this.pendingDownloads = new List<Download>();
            this.activeDownloads = new List<Download>();
            
            for(int i = 0; i < 4; i++)
            {
                Thread listener = new Thread(WaitForDownload);
                listener.Name = String.Format("{0} listener created by constructor", i);
                threadHandler.AddActive(listener);
                listener.Start();
            }

            //check if downloads folder is there and try to create it if not
            try
            {
                VerifyDownloadDirectory();
            }
            catch
            {
                applicationInterface.DisplayMessage("Unable to create downloads directory!");
            }
        }

        /// <summary>
        /// Queue up a video for downloading
        /// </summary>
        /// <param name="video">The video you want to download</param>
        public void AddToPending(Download video)
        {
            video.Completed = false;
            video.DownloadFailed = false;
            video.SetDownloadProgress(0);
            video.SetConvertProgress(0);

            pendingDownloads.Add(video);
        }

        /// <summary>
        /// Queue up a list of videos for downloading
        /// </summary>
        /// <param name="videos">The videos you want to download</param>
        public void AddToPending(List<Download> videos)
        {
            foreach(var video in videos)
            {
                AddToPending(video);
            }
        }

        /// <summary>
        /// Loops the thread forever, looking for videos to download
        /// </summary>
        private void WaitForDownload()
        {
            for (; ; )
            {
                Debug.WriteLine(String.Format("WAITING FOR PENDING DOWNLOAD: {0}", Thread.CurrentThread.Name));
                try
                {
                    Download video;
                    lock (pendingLocker)
                    {
                        while (pendingDownloads.Count == 0)
                        {
                            Thread.Sleep(10);
                        }
                        video = pendingDownloads.First();
                        pendingDownloads.Remove(video);
                        activeDownloads.Add(video);
                    }
                    video.SetThread(Thread.CurrentThread);
                    downloader.Download(video);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
            }
        }

        public void CancelDownload(Download video)
        {
            if(!video.Completed && activeDownloads.Contains(video))
            {
                int threadID = video.DownloadThread != null ? video.DownloadThread.ManagedThreadId : -1;
                while (video.DownloadThread != null && video.DownloadThread.IsAlive)
                {
                    Debug.WriteLine("Attempting download termination");

                    if (video.DownloadThread != null)
                        video.DownloadThread.Abort();
                }


                threadHandler.RemoveActive(threadID);
                activeDownloads.Remove(video);
                Debug.WriteLine("Download terminated");

                Thread listener = new Thread(WaitForDownload);
                listener.Name = String.Format("Re-listener: {0}", video.VideoData.Title);
                threadHandler.AddActive(listener);
                listener.Start();
            } 
        }

        public void CancelAllDownloads()
        {
            lock(activeLocker)
            {
                for(int i = 0; i < activeDownloads.Count; i++)
                {
                    CancelDownload(activeDownloads[i]);
                    i--;
                }
            }
        }

        public void RemoveActive(Download video)
        {
            lock(activeLocker)
            {
                activeDownloads.Remove(video);
            }
        }

        /// <summary>
        /// Check whether or not the download directory exists, and create it if not
        /// </summary>
        private void VerifyDownloadDirectory()
        {
            if (!Directory.Exists(downloadsPath))
                Directory.CreateDirectory(downloadsPath);
        }

        /// <summary>
        /// Open the downloads folder in Windows Explorer
        /// </summary>
        public void OpenDownloadDirectory()
        {
            VerifyDownloadDirectory();
            System.Diagnostics.Process.Start(downloadsPath);
        }
    }
}
