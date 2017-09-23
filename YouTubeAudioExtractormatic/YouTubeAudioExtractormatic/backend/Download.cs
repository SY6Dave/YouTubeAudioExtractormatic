using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Thread downloadThread;
        private bool downloadFailed;
        private bool completed;

        public VideoData VideoData { get { return videoData; } }
        public double DownloadProgress { get { return downloadProgress; } }
        public double ConvertProgress { get { return convertProgress; } }
        public uint Bitrate { get { return bitrate; } }
        public bool DownloadFailed
        {
            get { return downloadFailed; }
            set
            {
                downloadFailed = value;
                if (value == true)
                    downloadThread = null;
            }
        }
        public bool Completed
        {
            get { return completed; }
            set
            {
                completed = value;

                if (value == true)
                    downloadThread = null;
            }
        }
        public Thread DownloadThread { get { return downloadThread; } }

        /// <summary>
        /// Construct a new object which can be given to the DownloadManager to add to the pending downloads
        /// </summary>
        /// <param name="videoData">VideoData item which should hold reference to a valid YouTube URL</param>
        /// /// <param name="bitrate">Bitrate in kbps - e.g. 320 for high quality. Use 0 to save as videos instead of converting to audio</param>
        public Download(VideoData videoData, uint bitrate)
        {
            this.videoData = videoData;
            this.downloadProgress = 0.0;
            this.downloadFailed = false;
            this.completed = false;
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

        public void SetThread(Thread thread)
        {
            this.downloadThread = thread;
        }
    }
}
