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
        VideoData downloading;

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
                downloading = retrieved[0];
                Console.WriteLine("Now downloading: {0}", downloading);
                controller.Download(new List<VideoData> { downloading });
            }


            Console.ReadLine();
            controller.CloseApplication();
        }

        public void OnProgressChanged()
        {
            Console.WriteLine("Download Progress: {0}%...Conversion Progress: {1}%", downloading.DownloadProgress, downloading.ConvertProgress);

            if(downloading.DownloadProgress == 100 && downloading.ConvertProgress == 100)
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
