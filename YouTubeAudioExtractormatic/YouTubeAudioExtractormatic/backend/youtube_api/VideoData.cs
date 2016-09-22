using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Holds data retrieved from YouTube to download videos
    /// </summary>
    public class VideoData
    {
        private string url;
        private string id;
        private string title;
        private double downloadProgress;
        private double convertProgress;
        private uint desiredBitrate;

        public string Url { get { return url; } set { url = value; } }
        public string Id { get { return id; } }
        public string Title { get { return title; } }
        public double DownloadProgress { get { return downloadProgress; } }
        public double ConvertProgress { get { return convertProgress; } }
        public bool DownloadFailed;
        public uint DesiredBitrate { get { return desiredBitrate; } }

        public VideoData(string id, string title)
        {
            this.url = "youtube.com/watch?v=" + id;
            this.id = id;
            this.title = title;
            this.downloadProgress = 0.0;
            this.DownloadFailed = false;
            this.desiredBitrate = 0;
        }

        public override string ToString()
        {
            return title;
        }

        public void SetDownloadProgress(double progress)
        {
            this.downloadProgress = progress;
        }

        public void SetConvertProgress(double progress)
        {
            this.convertProgress = progress;
        }

        public void SetDesiredBirtate(uint bitrate)
        {
            this.desiredBitrate = bitrate;
        }
    }
}
