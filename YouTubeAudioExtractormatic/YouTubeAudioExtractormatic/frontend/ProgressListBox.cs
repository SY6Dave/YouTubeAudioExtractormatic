using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace YouTubeAudioExtractormatic
{
    public class ProgressListBox : CheckedListBox
    {
        Color colProgress = Color.FromArgb(74, 115, 93);
        Color colProgress2 = Color.FromArgb(64, 105, 83);
        Color colSelected = Color.FromArgb(144, 140, 150);//Color.FromArgb(147, 95, 83);

        public ProgressListBox()
        {
            ItemHeight = 24;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        public override int ItemHeight { get; set; }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0 || Items.Count == 0) return;

            Graphics g = e.Graphics;
            VideoData data = (VideoData)Items[e.Index];
            string text = data.Title;
            Rectangle bounds = e.Bounds;
            bounds.Height -= 4;
            bounds.Y += 2;
            bool selected = GetItemChecked(e.Index);

            g.FillRectangle(new SolidBrush(BackColor), e.Bounds);
            
            if (selected)
            {
                g.FillRectangle(new SolidBrush(colSelected), bounds);
                g.FillRectangle(new SolidBrush(colProgress), new Rectangle(bounds.X, bounds.Y, (int)Math.Ceiling((bounds.Width / 2) / 100.0 * data.DownloadProgress), bounds.Height));
                g.FillRectangle(new SolidBrush(colProgress2), new Rectangle(bounds.X + bounds.Width / 2, bounds.Y, (int)Math.Ceiling((bounds.Width / 2) / 100.0 * data.ConvertProgress), bounds.Height));
            }
            g.DrawString(text, e.Font, Brushes.White, bounds.Left + 20, bounds.Top + bounds.Height/4, StringFormat.GenericDefault);
        }

        // Code from http://yacsharpblog.blogspot.co.uk/2008/07/listbox-flicker.html
        protected override void OnPaint(PaintEventArgs e)  
        {  
            Region iRegion = new Region(e.ClipRectangle);  
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);  
            if (this.Items.Count > 0)  
            {  
                for (int i = 0; i < this.Items.Count; ++i)  
                {  
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);  
                    if (e.ClipRectangle.IntersectsWith(irect))  
                    {  
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)  
                        || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))  
                        || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))  
                        {  
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,  
                                irect, i,  
                                DrawItemState.Selected, this.ForeColor,  
                                this.BackColor));  
                        }  
                        else  
                        {  
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,  
                                irect, i,  
                                DrawItemState.Default, this.ForeColor,  
                                this.BackColor));  
                        }  
                        iRegion.Complement(irect);  
                    }  
                }  
            }  
            base.OnPaint(e);  
        }  
    }
}
