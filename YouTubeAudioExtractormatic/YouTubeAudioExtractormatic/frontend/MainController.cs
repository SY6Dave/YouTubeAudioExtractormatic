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
        private iGui creator;

        private ThreadHandler threadManager;
        private YoutubeGrabber videoRetriever;
        private DownloadManager downloadManager;

        private uint bitrate;
        public uint Bitrate { get { return bitrate; } }

        /// <summary>
        /// Construct a controller which interfaces with a thread manager, video downloader, and the youtube api to provide the core
        /// functionalities of the application
        /// </summary>
        /// <param name="creator">Normally, "this". Pass in a reference to the form/console that has implemented the iGui interface</param>
        public MainController(iGui creator)
        {
            this.creator = creator;

            this.threadManager = new ThreadHandler();
            this.videoRetriever = new YoutubeGrabber();
            this.downloadManager = new DownloadManager(threadManager, creator);

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
        /// <param name="selectedVideos">A list containing all downloads to begin</param>
        public void Download(List<Download> selectedVideos)
        {
            downloadManager.AddToPending(selectedVideos);
        }

        /// <summary>
        /// Begin downloading a single video
        /// </summary>
        /// <param name="videoToDownload">The video you want to download</param>
        public void Download(Download videoToDownload)
        {
            downloadManager.AddToPending(videoToDownload);
        }

        /// <summary>
        /// Grab the video information from a particular playlist or search query (* search query to be implemented)
        /// </summary>
        /// <param name="query">A valid YouTube video URL, playlist URL or generic search query</param>
        /// <returns>Returns a list of VideoData references that can then be passed to the Download method of the MainController</returns>
        public List<VideoData> GetVideos(string query)
        {
            List<VideoData> retrieved = videoRetriever.GetVideosByPlaylist(query);
            if (retrieved == null) creator.DisplayMessage("Unable to retrieve videos - invalid search query.");
            return retrieved;
        }

        /// <summary>
        /// Verifies that a downloads folder exists and then tries to open it in Windows Explorer
        /// </summary>
        public void OpenDownloadsFolder()
        {
            downloadManager.OpenDownloadDirectory();
        }

        public void CancelDownload(Download video)
        {
            downloadManager.CancelDownload(video);
            creator.OnProgressChanged();
        }

        public void CancelAllDownloads()
        {
            downloadManager.CancelAllDownloads();
            creator.OnProgressChanged();
        }
    }
}
