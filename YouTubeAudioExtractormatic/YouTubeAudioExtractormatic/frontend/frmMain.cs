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
    public partial class frmMain : Form, iGui
    {
        public MainController controller { get; set; }

        public static Color back = Color.FromArgb(66, 59, 76);
        public static Color dark = Color.FromArgb(56, 49, 66);
        public static Color light = Color.FromArgb(96, 91, 104);
        public static Color lighter = Color.FromArgb(144, 140, 150);
        public static Color red = Color.FromArgb(193, 39, 45);

        ThreadHandler threadHandler;
        Downloader downloader;
        YoutubeGrabber grabber;
        uint selectedBitrate;
        bool selectAll = false;

        public frmMain(ThreadHandler threadHandler)
        {
            controller = new MainController(this);

            this.threadHandler = threadHandler;
            downloader = new Downloader(threadHandler, this);
            grabber = new YoutubeGrabber();
            selectedBitrate = 256;
            InitializeComponent();

            BackColor = back;
            lstVideo.BackColor = dark;

            btnDownload.BackColor = red;
            btnSearch.BackColor = red;

            txtUrl.BackColor = light;
            btnOpen.BackColor = light;

            btnOpen.ForeColor = lighter;
            lblUrl.ForeColor = lighter;
            chkAll.ForeColor = lighter;
            rb96.ForeColor = lighter;
            rb128.ForeColor = lighter;
            rb192.ForeColor = lighter;
            rb256.ForeColor = lighter;
            rb320.ForeColor = lighter;
            rbVideo.ForeColor = lighter;
            btnOpen.ForeColor = lighter;
            lblMsg.ForeColor = lighter;
            lblAuthor.ForeColor = lighter;
            lblAuthor.LinkColor = lighter;

            rb96.Paint += RadioButtonPaint;
            rb128.Paint += RadioButtonPaint;
            rb192.Paint += RadioButtonPaint;
            rb256.Paint += RadioButtonPaint;
            rb320.Paint += RadioButtonPaint;
            rbVideo.Paint += RadioButtonPaint;

            chkAll.Paint += CheckBoxPaint;
        }

        public void RefreshGui()
        {
            InvalidateList();
        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {
            ToolStripLabel lblAuthor = (ToolStripLabel)sender;
            System.Diagnostics.Process.Start(lblAuthor.Tag.ToString());
            lblAuthor.LinkVisited = true;
        }

        private void frmMain_Closing(object sender, FormClosingEventArgs e)
        {
            controller.CloseApplication();
        }

        private void rb96_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 96;

            controller.SetBitrate(96);
        }

        private void rb128_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 128;

            controller.SetBitrate(128);
        }

        private void rb192_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 192;

            controller.SetBitrate(192);
        }

        private void rb256_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 256;

            controller.SetBitrate(256);
        }

        private void rb320_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 320;

            controller.SetBitrate(320);
        }

        private void rbVideo_CheckedChanged(object sender, EventArgs e)
        {
            selectedBitrate = 0;

            controller.SetBitrate(0);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            List<VideoData> pendingDownloads = new List<VideoData>();

            foreach(var item in lstVideo.CheckedItems)
            {
                pendingDownloads.Add((VideoData)item);
            }

            controller.Download(pendingDownloads);
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

            List<VideoData> searchResult = controller.GetVideos(txtUrl.Text);

            List<VideoData> retrievedVideos = grabber.GetVideosByPlaylist(txtUrl.Text);
            if (retrievedVideos == null) return;

            foreach (var video in retrievedVideos)
            {
                lstVideo.Items.Add(video);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(downloader.DownloadsPath))
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

        private void RadioButtonPaint(object sender, PaintEventArgs e)
        {
            var radioButton = (RadioButton)sender;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, 12, 16));
            e.Graphics.FillRectangle(new SolidBrush(light), new Rectangle(0, 3, 11, 11));

            if (radioButton.Checked)
                e.Graphics.FillRectangle(Brushes.Red, new Rectangle(2, 5, 7, 7));
        }

        private void CheckBoxPaint(object sender, PaintEventArgs e)
        {
            var checkbox = (CheckBox)sender;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, 12, 16));
            e.Graphics.FillRectangle(new SolidBrush(light), new Rectangle(0, 3, 11, 11));

            if (checkbox.Checked)
                e.Graphics.FillRectangle(Brushes.Red, new Rectangle(2, 5, 7, 7));
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            CustomScrollbar scrollBar = new CustomScrollbar(lstVideo);
            Controls.Add(scrollBar);
            scrollBar.BringToFront();
        }
    }
}
