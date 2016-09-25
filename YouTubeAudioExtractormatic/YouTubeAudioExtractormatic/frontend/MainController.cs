using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Primary interface for the application. Should be referenced by any GUI form so users can get videos, make downloads, etc.
    /// </summary>
    public class MainController
    {
        private ThreadHandler threadManager;
        private YoutubeGrabber videoRetriever;
        private Downloader videoDownloader;

        private uint bitrate;

        public MainController(iGui callingForm)
        {
            this.threadManager = new ThreadHandler();
            this.videoRetriever = new YoutubeGrabber();
            this.videoDownloader = new Downloader(threadManager, callingForm);

            this.bitrate = 256;
        }

        /// <summary>
        /// Call this from a gui form to update the selected bitrate
        /// </summary>
        /// <param name="bitrate">Bitrate in kbps - e.g. 320 for high quality. Use 0 to save as videos instead of converting to audio</param>
        public void SetBitrate(uint bitrate)
        {
            if(this.bitrate != bitrate)
                this.bitrate = bitrate;
        }

        /// <summary>
        /// This should be called when the application is exiting to ensure all threads are safely aborted
        /// </summary>
        public void CloseApplication()
        {
            threadManager.AbortAllThreads();
        }

        /// <summary>
        /// Begin downloading a collection of selected videos
        /// </summary>
        /// <param name="selectedVideos">A list of VideoData references that represent individual videos to be downloaded</param>
        public void Download(List<VideoData> selectedVideos)
        {
            videoDownloader.SetPendingDownloads(selectedVideos, this.bitrate);
        }

        /// <summary>
        /// Grab the video information from a particular playlist or search query (* search query to be implemented)
        /// </summary>
        /// <param name="query">A valid YouTube video URL, playlist URL or generic search query</param>
        /// <returns>Returns a list of VideoData references that can then be passed to the Download method of the MainController</returns>
        public List<VideoData> GetVideos(string query)
        {
            return new List<VideoData>();
        }

        /// <summary>
        /// Verifies that a downloads folder exists and then tries to open it in Windows Explorer
        /// </summary>
        public void OpenDownloadsFolder()
        {
            videoDownloader.OpenDownloadDirectory();
        }
    }
}
