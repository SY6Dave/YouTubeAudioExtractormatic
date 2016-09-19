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
        Color colSelected = Color.FromArgb(144, 140, 150);//Color.FromArgb(147, 95, 83);

        public ProgressListBox()
        {
            ItemHeight = 24;
        }

        public override int ItemHeight { get; set; }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0 || Items.Count == 0) return;

            Graphics g = e.Graphics;
            string text = Items[e.Index].ToString();
            Rectangle bounds = e.Bounds;
            bounds.Height -= 4;
            bounds.Y += 2;
            Random rnd = new Random();
            bool selected = GetItemChecked(e.Index);

            g.FillRectangle(new SolidBrush(BackColor), e.Bounds);

            if (selected)
            {
                g.FillRectangle(new SolidBrush(colSelected), bounds);
                g.FillRectangle(new SolidBrush(colProgress), new Rectangle(bounds.X, bounds.Y, bounds.Width / 100 * rnd.Next(100), bounds.Height));
            }
            g.DrawString(text, e.Font, Brushes.White, bounds.Left + 20, bounds.Top + bounds.Height/4, StringFormat.GenericDefault);
        }
    }
}
