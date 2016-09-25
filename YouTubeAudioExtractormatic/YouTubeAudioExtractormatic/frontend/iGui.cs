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
        void RefreshGui();
    }
}
