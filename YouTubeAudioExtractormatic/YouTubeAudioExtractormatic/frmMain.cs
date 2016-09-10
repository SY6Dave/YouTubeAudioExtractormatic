using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAudioExtractormatic
{
    public partial class frmMain : Form
    {
        ThreadHandler threadHandler;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(ThreadHandler threadHandler)
        {
            this.threadHandler = threadHandler;
            InitializeComponent();
        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {
            ToolStripLabel lblAuthor = (ToolStripLabel)sender;
            Downloader d = new Downloader(threadHandler);
            d.BeginDownloadThread("https://www.youtube.com/watch?v=PR_u9rvFKzE");
            //System.Diagnostics.Process.Start(lblAuthor.Tag.ToString());
            lblAuthor.LinkVisited = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void frmMain_Closing(object sender, FormClosingEventArgs e)
        {
            threadHandler.AbortAllThreads();
        }
    }
}
