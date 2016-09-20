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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstVideo = new YouTubeAudioExtractormatic.ProgressListBox();
            this.ssMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.BackColor = System.Drawing.Color.Transparent;
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAuthor});
            this.ssMain.Location = new System.Drawing.Point(0, 491);
            this.ssMain.Name = "ssMain";
            this.ssMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssMain.Size = new System.Drawing.Size(761, 25);
            this.ssMain.SizingGrip = false;
            this.ssMain.TabIndex = 0;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblAuthor
            // 
            this.lblAuthor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblAuthor.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.IsLink = true;
            this.lblAuthor.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblAuthor.LinkColor = System.Drawing.Color.White;
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAuthor.Size = new System.Drawing.Size(111, 20);
            this.lblAuthor.Tag = "http://www.davidmortiboy.com";
            this.lblAuthor.Text = "© David Mortiboy 2016";
            this.lblAuthor.VisitedLinkColor = System.Drawing.Color.Red;
            this.lblAuthor.Click += new System.EventHandler(this.lblAuthor_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrl.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.ForeColor = System.Drawing.Color.White;
            this.txtUrl.Location = new System.Drawing.Point(44, 3);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(424, 26);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "this is some text";
            this.txtUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUrl_KeyPress);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Red;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(3, 65);
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
            this.rb96.BackColor = System.Drawing.Color.Transparent;
            this.rb96.FlatAppearance.BorderSize = 0;
            this.rb96.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rb96.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb96.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb96.ForeColor = System.Drawing.Color.DimGray;
            this.rb96.Location = new System.Drawing.Point(3, 35);
            this.rb96.Name = "rb96";
            this.rb96.Size = new System.Drawing.Size(111, 24);
            this.rb96.TabIndex = 3;
            this.rb96.Text = "96 kbit/s - Lowest";
            this.rb96.UseVisualStyleBackColor = false;
            this.rb96.CheckedChanged += new System.EventHandler(this.rb96_CheckedChanged);
            // 
            // rb128
            // 
            this.rb128.AutoSize = true;
            this.rb128.BackColor = System.Drawing.Color.Transparent;
            this.rb128.FlatAppearance.BorderSize = 0;
            this.rb128.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rb128.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb128.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb128.ForeColor = System.Drawing.Color.DimGray;
            this.rb128.Location = new System.Drawing.Point(125, 5);
            this.rb128.Name = "rb128";
            this.rb128.Size = new System.Drawing.Size(101, 24);
            this.rb128.TabIndex = 4;
            this.rb128.Text = "128 kbit/s - Low";
            this.rb128.UseVisualStyleBackColor = false;
            this.rb128.CheckedChanged += new System.EventHandler(this.rb128_CheckedChanged);
            // 
            // rb192
            // 
            this.rb192.AutoSize = true;
            this.rb192.BackColor = System.Drawing.Color.Transparent;
            this.rb192.FlatAppearance.BorderSize = 0;
            this.rb192.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rb192.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb192.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb192.ForeColor = System.Drawing.Color.DimGray;
            this.rb192.Location = new System.Drawing.Point(125, 35);
            this.rb192.Name = "rb192";
            this.rb192.Size = new System.Drawing.Size(115, 24);
            this.rb192.TabIndex = 5;
            this.rb192.Text = "192 kbit/s - Medium";
            this.rb192.UseVisualStyleBackColor = false;
            this.rb192.CheckedChanged += new System.EventHandler(this.rb192_CheckedChanged);
            // 
            // rb256
            // 
            this.rb256.AutoSize = true;
            this.rb256.BackColor = System.Drawing.Color.Transparent;
            this.rb256.Checked = true;
            this.rb256.FlatAppearance.BorderSize = 0;
            this.rb256.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rb256.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb256.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb256.ForeColor = System.Drawing.Color.DimGray;
            this.rb256.Location = new System.Drawing.Point(250, 5);
            this.rb256.Name = "rb256";
            this.rb256.Size = new System.Drawing.Size(103, 24);
            this.rb256.TabIndex = 6;
            this.rb256.TabStop = true;
            this.rb256.Text = "256 kbit/s - High";
            this.rb256.UseVisualStyleBackColor = false;
            this.rb256.CheckedChanged += new System.EventHandler(this.rb256_CheckedChanged);
            // 
            // rb320
            // 
            this.rb320.AutoSize = true;
            this.rb320.BackColor = System.Drawing.Color.Transparent;
            this.rb320.FlatAppearance.BorderSize = 0;
            this.rb320.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rb320.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb320.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb320.ForeColor = System.Drawing.Color.DimGray;
            this.rb320.Location = new System.Drawing.Point(250, 35);
            this.rb320.Name = "rb320";
            this.rb320.Size = new System.Drawing.Size(116, 24);
            this.rb320.TabIndex = 7;
            this.rb320.Text = "320 kbit/s - Highest";
            this.rb320.UseVisualStyleBackColor = false;
            this.rb320.CheckedChanged += new System.EventHandler(this.rb320_CheckedChanged);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.ForeColor = System.Drawing.Color.LightGray;
            this.lblUrl.Location = new System.Drawing.Point(13, 5);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(25, 20);
            this.lblUrl.TabIndex = 8;
            this.lblUrl.Text = "URL";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Red;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(474, 5);
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
            this.rbVideo.BackColor = System.Drawing.Color.Transparent;
            this.rbVideo.FlatAppearance.BorderSize = 0;
            this.rbVideo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.rbVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbVideo.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVideo.ForeColor = System.Drawing.Color.DimGray;
            this.rbVideo.Location = new System.Drawing.Point(3, 5);
            this.rbVideo.Name = "rbVideo";
            this.rbVideo.Size = new System.Drawing.Size(87, 24);
            this.rbVideo.TabIndex = 12;
            this.rbVideo.Text = "Save as video";
            this.rbVideo.UseVisualStyleBackColor = false;
            this.rbVideo.CheckedChanged += new System.EventHandler(this.rbVideo_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.BackColor = System.Drawing.Color.Transparent;
            this.chkAll.FlatAppearance.BorderSize = 0;
            this.chkAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.chkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAll.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.ForeColor = System.Drawing.Color.DimGray;
            this.chkAll.Location = new System.Drawing.Point(65, 137);
            this.chkAll.Margin = new System.Windows.Forms.Padding(65, 3, 60, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(63, 24);
            this.chkAll.TabIndex = 14;
            this.chkAll.Text = "Select all";
            this.chkAll.UseVisualStyleBackColor = false;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(138, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 72);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(0, 103);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 11;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Gray;
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(32, 65);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(49, 23);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "► 📁";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lstVideo, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.chkAll, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(761, 491);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.txtUrl);
            this.panel2.Controls.Add(this.lblUrl);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Location = new System.Drawing.Point(124, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 33);
            this.panel2.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbVideo);
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.rb96);
            this.panel1.Controls.Add(this.rb128);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.rb192);
            this.panel1.Controls.Add(this.rb256);
            this.panel1.Controls.Add(this.rb320);
            this.panel1.Location = new System.Drawing.Point(60, 363);
            this.panel1.Margin = new System.Windows.Forms.Padding(60, 3, 60, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 125);
            this.panel1.TabIndex = 19;
            // 
            // lstVideo
            // 
            this.lstVideo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstVideo.CheckOnClick = true;
            this.lstVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVideo.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVideo.FormattingEnabled = true;
            this.lstVideo.IntegralHeight = false;
            this.lstVideo.Location = new System.Drawing.Point(60, 167);
            this.lstVideo.Margin = new System.Windows.Forms.Padding(60, 3, 60, 3);
            this.lstVideo.Name = "lstVideo";
            this.lstVideo.Size = new System.Drawing.Size(641, 190);
            this.lstVideo.TabIndex = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(761, 516);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(599, 548);
            this.Name = "frmMain";
            this.Text = "YouTube Audio Extractormatic";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblAuthor;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}

