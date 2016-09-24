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

            callingForm.bitrateChanged = BitrateChanged;
        }

        private void BitrateChanged(uint bitrate)
        {
            Debug.WriteLine(bitrate);
        }

        public void Close()
        {
            threadManager.AbortAllThreads();
        }

        public void SetBitrate(uint bitrate)
        {
            this.bitrate = bitrate;
        }

        public void InitiateDownload(CheckedListBox.CheckedItemCollection selectedVideos)
        {
            videoDownloader.SetPendingDownloads(selectedVideos, this.bitrate);
            
        }
    }
}
