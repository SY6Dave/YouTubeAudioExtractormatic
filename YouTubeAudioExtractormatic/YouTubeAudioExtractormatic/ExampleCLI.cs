using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// A very basic example of how the iGui has been designed in such a way that even a Command Line Interface can be created to run this application
    /// - just change output to Console Application, and replace the code in Program.cs to instantiate a new ExampleCLI reference
    /// </summary>
    public class ExampleCLI : iGui
    {
        public MainController controller { get; set; }
        Download download;

        public ExampleCLI()
        {
            controller = new MainController(this);

            Console.Write("Enter a bitrate: ");
            controller.SetBitrate(Convert.ToUInt16(Console.ReadLine()));
            Console.Write("Enter a video URL: ");
            List<VideoData> retrieved = controller.GetVideos(Console.ReadLine());

            if (retrieved != null)
                if (retrieved.Count > 0)
            {
                download = new Download(retrieved[0], controller.Bitrate);
                Console.WriteLine("Now downloading: {0}", download);
                controller.Download(download);
            }


            Console.ReadLine();
            controller.CloseApplication();
        }

        public void OnProgressChanged()
        {
            Console.WriteLine("Download Progress: {0}%...Conversion Progress: {1}%", download.DownloadProgress, download.ConvertProgress);

            if (download.DownloadProgress == 100 && download.ConvertProgress == 100)
            {
                Console.WriteLine("\nSuccess!");
                controller.OpenDownloadsFolder();
            }
        }

        public void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
