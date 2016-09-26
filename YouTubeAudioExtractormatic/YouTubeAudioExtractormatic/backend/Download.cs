using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Holds data about the video which is being download, and the status of the download
    /// </summary>
    public class Download
    {
        private VideoData videoData;
        private double downloadProgress;
        private double convertProgress;
        private uint bitrate;

        public VideoData VideoData { get { return videoData; } }
        public double DownloadProgress { get { return downloadProgress; } }
        public double ConvertProgress { get { return convertProgress; } }
        public uint Bitrate { get { return bitrate; } }
        public bool DownloadFailed;
        public bool Completed;

        /// <summary>
        /// Construct a new object which can be given to the DownloadManager to add to the pending downloads
        /// </summary>
        /// <param name="videoData">VideoData item which should hold reference to a valid YouTube URL</param>
        /// /// <param name="bitrate">Bitrate in kbps - e.g. 320 for high quality. Use 0 to save as videos instead of converting to audio</param>
        public Download(VideoData videoData, uint bitrate)
        {
            this.videoData = videoData;
            this.downloadProgress = 0.0;
            this.DownloadFailed = false;
            this.Completed = false;
            this.bitrate = bitrate;
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

        public void SetBitrate(uint bitrate)
        {
            this.bitrate = bitrate;
        }

        public override string ToString()
        {
            return videoData.ToString();
        }
    }
}
