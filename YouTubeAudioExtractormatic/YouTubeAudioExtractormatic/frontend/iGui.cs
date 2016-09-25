using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Should be implemented by the main form (for example, if skinning a new gui)
    /// </summary>
    public interface iGui
    {
        MainController controller { get; set; }

        /// <summary>
        /// This should get called whenever the download/conversion progress of a video changes so that the gui knows to refresh
        /// </summary>
        void OnProgressChanged();
    }
}
