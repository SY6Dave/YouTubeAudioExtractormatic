using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    public class Download
    {
        private VideoData videoData;
        private double downloadProgress;
        private double convertProgress;
        private uint desiredBitrate;

        public VideoData VideoData { get { return videoData; } }
        public double DownloadProgress { get { return downloadProgress; } }
        public double ConvertProgress { get { return convertProgress; } }
        public bool DownloadFailed;
        public bool Completed;
        public uint DesiredBitrate { get { return desiredBitrate; } }

        public Download(VideoData videoData)
        {
            this.videoData = videoData;
            this.downloadProgress = 0.0;
            this.DownloadFailed = false;
            this.Completed = false;
            this.desiredBitrate = 0;
        }

        public void SetDownloadProgress(double progress)
        {
            this.downloadProgress = progress;
            CheckCompleted();
        }

        public void SetConvertProgress(double progress)
        {
            this.convertProgress = progress;
            CheckCompleted();
        }

        public void SetDesiredBirtate(uint bitrate)
        {
            this.desiredBitrate = bitrate;
        }

        private void CheckCompleted()
        {
            if (DownloadFailed)
            {
                Completed = false;
                return;
            }

            if (downloadProgress == 100 && convertProgress == 100)
            {
                Completed = true;
            }
        }
    }
}
