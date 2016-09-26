using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    public class DownloadManager
    {
        object pendingLocker = new object();
        private List<Download> pendingDownloads;
        private Downloader downloader;


        public DownloadManager(Downloader downloader, ThreadHandler threadHandler)
        {
            this.downloader = downloader;
            this.pendingDownloads = new List<Download>();
            
            for(int i = 0; i < 4; i++)
            {
                Thread listener = new Thread(WaitForDownload);
                threadHandler.AddActive(listener);
                listener.Start();
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
    }
}
