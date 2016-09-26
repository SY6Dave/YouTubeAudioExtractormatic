using System;
using System.Collections.Generic;
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
        private List<Download> pendingDownloads;
        private Downloader downloader;

        /// <summary>
        /// Construct a DownloadManager which can add pending downloads, and create threads to start downloading them
        /// </summary>
        /// <param name="threadHandler">The main ThreadHandler of the application</param>
        /// /// <param name="applicationInterface">The frontend interface of the application</param>
        public DownloadManager(ThreadHandler threadHandler, iGui applicationInterface)
        {
            this.downloader = new Downloader(threadHandler, applicationInterface, this);
            this.pendingDownloads = new List<Download>();
            
            for(int i = 0; i < 4; i++)
            {
                Thread listener = new Thread(WaitForDownload);
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

        public void AddToPending(Download video)
        {
            pendingDownloads.Add(video);
        }

        public void AddToPending(List<Download> videos)
        {
            foreach(var video in videos)
            {
                AddToPending(video);
            }
        }

        private void WaitForDownload()
        {
            for (; ; )
            {
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
                    }
                    downloader.Download(video);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
            }
        }

        private void VerifyDownloadDirectory()
        {
            if (!Directory.Exists(downloadsPath))
                Directory.CreateDirectory(downloadsPath);
        }

        public void OpenDownloadDirectory()
        {
            VerifyDownloadDirectory();
            System.Diagnostics.Process.Start(downloadsPath);
        }
    }
}
