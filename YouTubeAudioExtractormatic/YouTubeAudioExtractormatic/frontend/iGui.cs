using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    public delegate void BitrateChanged(uint bitrate);
    /// <summary>
    /// Should be implemented by the main form (for example, if skinning a new gui)
    /// </summary>
    public interface iGui
    {
        MainController controller { get; set; }
        void InvalidateList();

        //BitrateChanged bitrateChanged { get; set; }
        Action<uint> bitrateChanged { get; set; }
    }
}
