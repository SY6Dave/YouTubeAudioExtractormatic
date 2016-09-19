namespace YouTubeAudioExtractormatic
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblAuthor = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNote = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.rb96 = new System.Windows.Forms.RadioButton();
            this.rb128 = new System.Windows.Forms.RadioButton();
            this.rb192 = new System.Windows.Forms.RadioButton();
            this.rb256 = new System.Windows.Forms.RadioButton();
            this.rb320 = new System.Windows.Forms.RadioButton();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.rbVideo = new System.Windows.Forms.RadioButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lstVideo = new YouTubeAudioExtractormatic.ProgressListBox();
            this.ssMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.BackColor = System.Drawing.Color.Transparent;
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAuthor,
            this.lblNote});
            this.ssMain.Location = new System.Drawing.Point(0, 466);
            this.ssMain.Name = "ssMain";
            this.ssMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssMain.Size = new System.Drawing.Size(689, 22);
            this.ssMain.SizingGrip = false;
            this.ssMain.TabIndex = 0;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblAuthor
            // 
            this.lblAuthor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAuthor.IsLink = true;
            this.lblAuthor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblAuthor.LinkColor = System.Drawing.Color.White;
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAuthor.Size = new System.Drawing.Size(137, 17);
            this.lblAuthor.Tag = "http://www.davidmortiboy.com";
            this.lblAuthor.Text = "© David Mortiboy 2016";
            this.lblAuthor.VisitedLinkColor = System.Drawing.Color.Red;
            this.lblAuthor.Click += new System.EventHandler(this.lblAuthor_Click);
            // 
            // lblNote
            // 
            this.lblNote.ForeColor = System.Drawing.Color.LightGray;
            this.lblNote.Name = "lblNote";
            this.lblNote.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblNote.Size = new System.Drawing.Size(441, 17);
            this.lblNote.Text = "Note: This GUI is in pre-pre-pre-alpha state and doesn\'t represent the final prod" +
    "uct";
            // 
            // txtUrl
            // 
            this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrl.ForeColor = System.Drawing.Color.White;
            this.txtUrl.Location = new System.Drawing.Point(127, 96);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(424, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUrl_KeyPress);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Red;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(38, 409);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(23, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "▼";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // rb96
            // 
            this.rb96.AutoSize = true;
            this.rb96.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb96.ForeColor = System.Drawing.Color.LightGray;
            this.rb96.Location = new System.Drawing.Point(38, 375);
            this.rb96.Name = "rb96";
            this.rb96.Size = new System.Drawing.Size(109, 17);
            this.rb96.TabIndex = 3;
            this.rb96.Text = "96 kbit/s - Lowest";
            this.rb96.UseVisualStyleBackColor = true;
            this.rb96.CheckedChanged += new System.EventHandler(this.rb96_CheckedChanged);
            // 
            // rb128
            // 
            this.rb128.AutoSize = true;
            this.rb128.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb128.ForeColor = System.Drawing.Color.LightGray;
            this.rb128.Location = new System.Drawing.Point(160, 357);
            this.rb128.Name = "rb128";
            this.rb128.Size = new System.Drawing.Size(101, 17);
            this.rb128.TabIndex = 4;
            this.rb128.Text = "128 kbit/s - Low";
            this.rb128.UseVisualStyleBackColor = true;
            this.rb128.CheckedChanged += new System.EventHandler(this.rb128_CheckedChanged);
            // 
            // rb192
            // 
            this.rb192.AutoSize = true;
            this.rb192.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb192.ForeColor = System.Drawing.Color.LightGray;
            this.rb192.Location = new System.Drawing.Point(160, 375);
            this.rb192.Name = "rb192";
            this.rb192.Size = new System.Drawing.Size(118, 17);
            this.rb192.TabIndex = 5;
            this.rb192.Text = "192 kbit/s - Medium";
            this.rb192.UseVisualStyleBackColor = true;
            this.rb192.CheckedChanged += new System.EventHandler(this.rb192_CheckedChanged);
            // 
            // rb256
            // 
            this.rb256.AutoSize = true;
            this.rb256.Checked = true;
            this.rb256.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb256.ForeColor = System.Drawing.Color.LightGray;
            this.rb256.Location = new System.Drawing.Point(285, 357);
            this.rb256.Name = "rb256";
            this.rb256.Size = new System.Drawing.Size(103, 17);
            this.rb256.TabIndex = 6;
            this.rb256.TabStop = true;
            this.rb256.Text = "256 kbit/s - High";
            this.rb256.UseVisualStyleBackColor = true;
            this.rb256.CheckedChanged += new System.EventHandler(this.rb256_CheckedChanged);
            // 
            // rb320
            // 
            this.rb320.AutoSize = true;
            this.rb320.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb320.ForeColor = System.Drawing.Color.LightGray;
            this.rb320.Location = new System.Drawing.Point(285, 375);
            this.rb320.Name = "rb320";
            this.rb320.Size = new System.Drawing.Size(117, 17);
            this.rb320.TabIndex = 7;
            this.rb320.Text = "320 kbit/s - Highest";
            this.rb320.UseVisualStyleBackColor = true;
            this.rb320.CheckedChanged += new System.EventHandler(this.rb320_CheckedChanged);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.LightGray;
            this.lblUrl.Location = new System.Drawing.Point(93, 98);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(32, 13);
            this.lblUrl.TabIndex = 8;
            this.lblUrl.Text = "URL:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Red;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(557, 94);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "►";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rbVideo
            // 
            this.rbVideo.AutoSize = true;
            this.rbVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbVideo.ForeColor = System.Drawing.Color.LightGray;
            this.rbVideo.Location = new System.Drawing.Point(38, 357);
            this.rbVideo.Name = "rbVideo";
            this.rbVideo.Size = new System.Drawing.Size(92, 17);
            this.rbVideo.TabIndex = 12;
            this.rbVideo.Text = "Save as video";
            this.rbVideo.UseVisualStyleBackColor = true;
            this.rbVideo.CheckedChanged += new System.EventHandler(this.rbVideo_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAll.ForeColor = System.Drawing.Color.LightGray;
            this.chkAll.Location = new System.Drawing.Point(38, 134);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(66, 17);
            this.chkAll.TabIndex = 14;
            this.chkAll.Text = "Select all";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(96, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 72);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(43, 442);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 11;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Red;
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(67, 409);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(49, 23);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "► 📁";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lstVideo
            // 
            this.lstVideo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstVideo.CheckOnClick = true;
            this.lstVideo.FormattingEnabled = true;
            this.lstVideo.Location = new System.Drawing.Point(38, 157);
            this.lstVideo.Name = "lstVideo";
            this.lstVideo.Size = new System.Drawing.Size(604, 192);
            this.lstVideo.TabIndex = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(689, 488);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lstVideo);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.rbVideo);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.rb320);
            this.Controls.Add(this.rb256);
            this.Controls.Add(this.rb192);
            this.Controls.Add(this.rb128);
            this.Controls.Add(this.rb96);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.ssMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "YouTube Audio Extractormatic";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_Closing);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblAuthor;
        private System.Windows.Forms.ToolStripStatusLabel lblNote;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.RadioButton rb96;
        private System.Windows.Forms.RadioButton rb128;
        private System.Windows.Forms.RadioButton rb192;
        private System.Windows.Forms.RadioButton rb256;
        private System.Windows.Forms.RadioButton rb320;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rbVideo;
        private System.Windows.Forms.CheckBox chkAll;
        private ProgressListBox lstVideo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnOpen;
    }
}

