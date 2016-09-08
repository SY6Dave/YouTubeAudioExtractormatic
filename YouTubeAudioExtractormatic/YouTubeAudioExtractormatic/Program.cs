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
            Downloader d = new Downloader();
            d.BeginDownload("https://www.youtube.com/watch?v=KeN9c2GYJkk&index=16&list=PLeJzBLm_pENl17EXrq9lwuCOkvjdSBM79");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
