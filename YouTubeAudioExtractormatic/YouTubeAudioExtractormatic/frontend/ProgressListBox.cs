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
        Color colProgress2 = Color.FromArgb(54, 95, 73);
        Color colSelected = Color.FromArgb(96, 91, 104);

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

            VideoData data = (VideoData)Items[e.Index];
            Rectangle bounds = e.Bounds;
            Graphics g = e.Graphics;
            string text = data.Title;
            bool selected = GetItemChecked(e.Index);
            
            bounds.Height -= 4;
            bounds.Y += 2;

            double dlProgressWidth = bounds.Width / 100.0 * 85.0;
            double convProgressWidth = bounds.Width / 100.0 * 15.0;

            g.FillRectangle(new SolidBrush(BackColor), e.Bounds);
            
            if (selected) g.FillRectangle(new SolidBrush(colSelected), bounds);

            g.FillRectangle(new SolidBrush(colProgress), new Rectangle(bounds.X, bounds.Y, (int)Math.Ceiling(dlProgressWidth / 100.0 * data.DownloadProgress), bounds.Height));
            g.FillRectangle(new SolidBrush(colProgress2), new Rectangle(bounds.X + (int)dlProgressWidth, bounds.Y, (int)Math.Ceiling(convProgressWidth / 100.0 * data.ConvertProgress), bounds.Height));

            g.DrawString(text, e.Font, Brushes.White, bounds.Left + 20, bounds.Top + bounds.Height/4, StringFormat.GenericDefault);
        }

        /// <summary>
        /// Code from http://yacsharpblog.blogspot.co.uk/2008/07/listbox-flicker.html
        /// </summary>
        /// <param name="e"></param>
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
