using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAudioExtractormatic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            YoutubeGrabber y = new YoutubeGrabber();
            List<VideoData> vd = y.GetVideosByPlaylist("https://www.youtube.com/playlist?list=PLeJzBLm_pENmPg4n4kFvDyIitdTGGgtsB");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(new ThreadHandler()));
        }
    }
}
