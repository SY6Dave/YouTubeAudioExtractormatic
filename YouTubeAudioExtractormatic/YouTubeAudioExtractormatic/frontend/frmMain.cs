using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        YoutubeGrabber grabber;
        uint selectedBitrate;
        bool selectAll = false;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(ThreadHandler threadHandler)
        {
            this.threadHandler = threadHandler;
            downloader = new Downloader(threadHandler, this);
            grabber = new YoutubeGrabber();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            downloader.BeginDownloadThread(lstVideo.CheckedItems, selectedBitrate);
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

        public void InvalidateList()
        {
            if (lstVideo.InvokeRequired)
            {
                lstVideo.BeginInvoke((MethodInvoker)delegate() { lstVideo.Invalidate(); ;});
            }
            else
            {
                lstVideo.Invalidate();
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            txtUrl.Text = Clipboard.GetText();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            selectAll = chkAll.Checked;

            for (int i = 0; i < lstVideo.Items.Count; i++) lstVideo.SetItemChecked(i, selectAll);
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Search();
            }
        }

        private void Search()
        {
            chkAll.Checked = false;
            lstVideo.Items.Clear();

            List<VideoData> retrievedVideos = grabber.GetVideosByPlaylist(txtUrl.Text);
            if (retrievedVideos == null) return;

            foreach (var video in retrievedVideos)
            {
                lstVideo.Items.Add(video);
            }
        }
    }
}
