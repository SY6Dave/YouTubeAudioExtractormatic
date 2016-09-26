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
        private List<VideoData> pendingDownloads;

        public DownloadManager(ThreadHandler threadHandler)
        {
            this.pendingDownloads = new List<VideoData>();
            
            for(int i = 0; i < 4; i++)
            {
                Thread listener = new Thread(WaitForDownload);
                threadHandler.AddActive(listener);
                listener.Start();
            }
        }

        public void AddToPending(VideoData video)
        {
            pendingDownloads.Add(video);
        }

        public void AddToPending(List<VideoData> videos)
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
                    VideoData video;
                    lock (downloadLocker)
                    {
                        while (pendingDownloads.Count == 0)
                        {
                            Thread.Sleep(10);
                        }
                        video = pendingDownloads.First();
                        pendingDownloads.Remove(video);
                    }
                    Download(video);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
            }
        }
    }
}
