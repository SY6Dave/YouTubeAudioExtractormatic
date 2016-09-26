using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    public static class UrlParser
    {
        public static string GetFormattedUrl(string url)
        {
            return InShortFormat(url) ? ParseUrl(url) : url;
        }

        private static bool InShortFormat(string url)
        {
            return url.Contains("youtu.be/");
        }

        private static string ParseUrl(string url)
        {
            string parsed = "";

            parsed = url.Remove(0, url.IndexOf("youtu.be/") + 9);
            parsed = parsed.Insert(0, "youtube.com/watch?v=");
            return parsed;
        }
    }
}
