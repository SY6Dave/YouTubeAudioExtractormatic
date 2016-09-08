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
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAuthor});
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(608, 446);
            this.Controls.Add(this.ssMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "YouTube Audio Extractormatic";
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblAuthor;
    }
}

