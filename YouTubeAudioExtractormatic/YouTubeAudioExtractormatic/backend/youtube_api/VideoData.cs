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

        public string Url { get { return url; } set { url = value; } }
        public string Id { get { return id; } }
        public string Title { get { return title; } }

        public VideoData(string id, string title)
        {
            this.url = "youtube.com/watch?v=" + id;
            this.id = id;
            this.title = title;
        }

        public override string ToString()
        {
            return title;
        }
    }
}
