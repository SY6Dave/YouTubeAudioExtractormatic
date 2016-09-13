using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    public class UrlParser
    {
        private string url;
        public string Url { get { return url; } }

        public UrlParser(string url)
        {
            this.url = InShortFormat(url) ? ParseUrl(url) : url;
        }

        private bool InShortFormat(string url)
        {
            return url.Contains("youtu.be/");
        }

        private string ParseUrl(string url)
        {
            string parsed = "";

            parsed = url.Remove(0, url.IndexOf("youtu.be/") + 9);
            parsed = parsed.Insert(0, "youtube.com/watch?v=");
            return parsed;
        }
    }
}
