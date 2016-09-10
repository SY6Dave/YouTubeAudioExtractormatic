using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAudioExtractormatic
{
    public partial class frmMain : Form
    {
        ThreadHandler threadHandler;
        Downloader downloader;
        uint selectedBitrate;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(ThreadHandler threadHandler)
        {
            this.threadHandler = threadHandler;
            downloader = new Downloader(threadHandler, this);
            selectedBitrate = 256;
            InitializeComponent();
        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {
            ToolStripLabel lblAuthor = (ToolStripLabel)sender;
            System.Diagnostics.Process.Start(lblAuthor.Tag.ToString());
            lblAuthor.LinkVisited = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void frmMain_Closing(object sender, FormClosingEventArgs e)
        {
            threadHandler.AbortAllThreads();
        }

        private void rb96_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 96;
        }

        private void rb128_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 128;
        }

        private void rb192_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 192;
        }

        private void rb256_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 256;
        }

        private void rb320_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 320;
        }

        private void rbVideo_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 0;
        }

        private void lblOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!Directory.Exists(downloader.DownloadsPath))
            {
                try
                {
                    Directory.CreateDirectory(downloader.DownloadsPath);
                }
                catch
                {
                    lblMsg.Text = "Unable to create downloads directory!";
                    return;
                }
            }

            System.Diagnostics.Process.Start(downloader.DownloadsPath);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtUrl.Text = Clipboard.GetText();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            downloader.BeginDownloadThread(txtUrl.Text, selectedBitrate);
        }

        public void UpdateMsgLbl(string text)
        {
            if (lblMsg.InvokeRequired)
            {
                lblMsg.BeginInvoke((MethodInvoker)delegate() { lblMsg.Text = text; ;});
            }
            else
            {
                lblMsg.Text = text;
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            txtUrl.Text = Clipboard.GetText();
        }
    }
}
