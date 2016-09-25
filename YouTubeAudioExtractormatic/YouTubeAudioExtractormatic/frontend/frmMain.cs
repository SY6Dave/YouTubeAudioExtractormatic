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

        #region Colors
        public static Color back = Color.FromArgb(66, 59, 76);
        public static Color dark = Color.FromArgb(56, 49, 66);
        public static Color light = Color.FromArgb(96, 91, 104);
        public static Color lighter = Color.FromArgb(144, 140, 150);
        public static Color red = Color.FromArgb(193, 39, 45);
        #endregion

        private bool selectAll;

        public frmMain()
        {
            controller = new MainController(this);

            InitializeComponent();
            SetupColors();

            this.selectAll = false;
        }

        /// <summary>
        /// Required by iGui. Gets called when download/conversion progress of a video changes
        /// </summary>
        public void OnProgressChanged()
        {
            //refresh the list to display up-to-date progress bars
            if (lstVideo.InvokeRequired)
            {
                lstVideo.BeginInvoke((MethodInvoker)delegate() { lstVideo.Invalidate(); ;}); //in case called from another thread
            }
            else
            {
                lstVideo.Invalidate();
            }
        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {
            ToolStripLabel lblAuthor = (ToolStripLabel)sender;
            System.Diagnostics.Process.Start(lblAuthor.Tag.ToString()); //shameless self-promotion
            lblAuthor.LinkVisited = true;
        }

        private void frmMain_Closing(object sender, FormClosingEventArgs e)
        {
            controller.CloseApplication(); //ensure all threads are safely aborted
        }

        private void BitrateChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            controller.SetBitrate(Convert.ToUInt16(rb.Tag));
        }

        private void Search(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            lstVideo.Items.Clear(); //make sure list is empty before re-populating

            //call the controller to execute the search
            List<VideoData> searchResult = controller.GetVideos(txtUrl.Text);

            //add videos to list
            if (searchResult != null)
            foreach (var video in searchResult)
            {
                lstVideo.Items.Add(video);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            List<VideoData> pendingDownloads = new List<VideoData>();

            //cast checked items to VideoData references
            foreach(var item in lstVideo.CheckedItems)
            {
                pendingDownloads.Add((VideoData)item);
            }

            controller.Download(pendingDownloads); //call the controller to begin downloading
        }

        public void DisplayMessage(string msg)
        {
            if (lblMsg.InvokeRequired)
            {
                lblMsg.BeginInvoke((MethodInvoker)delegate() { lblMsg.Text = msg; ;});
            }
            else
            {
                lblMsg.Text = msg;
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            txtUrl.Text = Clipboard.GetText(); //paste the clipboard details whenever the window is re-focused
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
                Search(sender, e);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                controller.OpenDownloadsFolder();
            }
            catch(UnauthorizedAccessException)
            {
                lblMsg.Text = "Unable to create downloads directory! - Unathorized Access";
            }
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

        private void SetupColors()
        {
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
    }
}
