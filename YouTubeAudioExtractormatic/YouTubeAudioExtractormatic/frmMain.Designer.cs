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
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblAuthor = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.rb96 = new System.Windows.Forms.RadioButton();
            this.rb128 = new System.Windows.Forms.RadioButton();
            this.rb192 = new System.Windows.Forms.RadioButton();
            this.rb256 = new System.Windows.Forms.RadioButton();
            this.rb320 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.LinkLabel();
            this.btnPaste = new System.Windows.Forms.Button();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAuthor,
            this.toolStripStatusLabel1});
            this.ssMain.Location = new System.Drawing.Point(0, 424);
            this.ssMain.Name = "ssMain";
            this.ssMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssMain.Size = new System.Drawing.Size(608, 22);
            this.ssMain.TabIndex = 0;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblAuthor
            // 
            this.lblAuthor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblAuthor.IsLink = true;
            this.lblAuthor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAuthor.Size = new System.Drawing.Size(130, 17);
            this.lblAuthor.Tag = "http://www.davidmortiboy.com";
            this.lblAuthor.Text = "© David Mortiboy 2016";
            this.lblAuthor.Click += new System.EventHandler(this.lblAuthor_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(175, 45);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(279, 20);
            this.txtUrl.TabIndex = 1;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(35, 263);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // rb96
            // 
            this.rb96.AutoSize = true;
            this.rb96.Location = new System.Drawing.Point(34, 84);
            this.rb96.Name = "rb96";
            this.rb96.Size = new System.Drawing.Size(110, 17);
            this.rb96.TabIndex = 3;
            this.rb96.Text = "96 kbit/s - Lowest";
            this.rb96.UseVisualStyleBackColor = true;
            this.rb96.CheckedChanged += new System.EventHandler(this.rb96_CheckedChanged);
            // 
            // rb128
            // 
            this.rb128.AutoSize = true;
            this.rb128.Location = new System.Drawing.Point(34, 118);
            this.rb128.Name = "rb128";
            this.rb128.Size = new System.Drawing.Size(102, 17);
            this.rb128.TabIndex = 4;
            this.rb128.Text = "128 kbit/s - Low";
            this.rb128.UseVisualStyleBackColor = true;
            this.rb128.CheckedChanged += new System.EventHandler(this.rb128_CheckedChanged);
            // 
            // rb192
            // 
            this.rb192.AutoSize = true;
            this.rb192.Location = new System.Drawing.Point(34, 151);
            this.rb192.Name = "rb192";
            this.rb192.Size = new System.Drawing.Size(119, 17);
            this.rb192.TabIndex = 5;
            this.rb192.Text = "192 kbit/s - Medium";
            this.rb192.UseVisualStyleBackColor = true;
            this.rb192.CheckedChanged += new System.EventHandler(this.rb192_CheckedChanged);
            // 
            // rb256
            // 
            this.rb256.AutoSize = true;
            this.rb256.Checked = true;
            this.rb256.Location = new System.Drawing.Point(34, 184);
            this.rb256.Name = "rb256";
            this.rb256.Size = new System.Drawing.Size(104, 17);
            this.rb256.TabIndex = 6;
            this.rb256.TabStop = true;
            this.rb256.Text = "256 kbit/s - High";
            this.rb256.UseVisualStyleBackColor = true;
            this.rb256.CheckedChanged += new System.EventHandler(this.rb256_CheckedChanged);
            // 
            // rb320
            // 
            this.rb320.AutoSize = true;
            this.rb320.Location = new System.Drawing.Point(35, 216);
            this.rb320.Name = "rb320";
            this.rb320.Size = new System.Drawing.Size(118, 17);
            this.rb320.TabIndex = 7;
            this.rb320.Text = "320 kbit/s - Highest";
            this.rb320.UseVisualStyleBackColor = true;
            this.rb320.CheckedChanged += new System.EventHandler(this.rb320_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter YouTube Video URL:";
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(31, 321);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(121, 13);
            this.lblOpen.TabIndex = 9;
            this.lblOpen.TabStop = true;
            this.lblOpen.Text = "Open Downloads Folder";
            this.lblOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpen_LinkClicked);
            // 
            // btnPaste
            // 
            this.btnPaste.Location = new System.Drawing.Point(470, 43);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(75, 23);
            this.btnPaste.TabIndex = 10;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(441, 17);
            this.toolStripStatusLabel1.Text = "Note: This GUI is in pre-pre-pre-alpha state and doesn\'t represent the final prod" +
    "uct";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(35, 386);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 11;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(608, 446);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.lblOpen);
            this.Controls.Add(this.label1);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblAuthor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.RadioButton rb96;
        private System.Windows.Forms.RadioButton rb128;
        private System.Windows.Forms.RadioButton rb192;
        private System.Windows.Forms.RadioButton rb256;
        private System.Windows.Forms.RadioButton rb320;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblOpen;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Label lblMsg;
    }
}

