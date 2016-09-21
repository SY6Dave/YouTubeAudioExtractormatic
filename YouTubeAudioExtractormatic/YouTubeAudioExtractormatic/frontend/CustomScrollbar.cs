using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeAudioExtractormatic
{
    public class CustomScrollbar : Panel
    {
        CheckedListBox myParent;

        public CustomScrollbar(CheckedListBox parent)
        {
            Size = new Size(17, parent.Size.Height);
            Location = new Point(parent.Location.X + parent.Size.Width - Size.Width, parent.Location.Y);
            Parent = parent;
            myParent = parent;
            BackColor = myParent.BackColor;
            Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            myParent.Paint += Repaint;
        }

        public void Repaint(object sender, PaintEventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int itemCount = Math.Max(myParent.Items.Count, 1);

            Graphics g = e.Graphics;
            int visibleCount = myParent.ClientSize.Height / myParent.GetItemHeight(0);
            int topIndex = myParent.TopIndex;
            int bottomIndex = Math.Min(topIndex + visibleCount, itemCount);
            int maxScrollPos = Math.Max(itemCount - topIndex, 0);
            int scrollHeight = Size.Height - 17 * 2;
            int scrollBarTop = (int)Math.Ceiling((double)scrollHeight / (double)itemCount * (double)topIndex);
            int scrollBarBottom = (int)Math.Ceiling((double)scrollHeight / (double)itemCount * (double)bottomIndex);

            g.FillRectangle(new SolidBrush(frmMain.light), new Rectangle(0, 0, 17, 17));
            g.FillRectangle(new SolidBrush(frmMain.light), new Rectangle(0, Size.Height - 17, 17, 17));
            g.FillRectangle(new SolidBrush(frmMain.light), new Rectangle(0, 19 + scrollBarTop, 17, scrollBarBottom - scrollBarTop - 4));

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            SolidBrush textCol = new SolidBrush(BackColor);
            g.DrawString("▲", Font, textCol, new Rectangle(1, 1, 17, 17), stringFormat);
            g.DrawString("▼", Font, textCol, new Rectangle(1, Size.Height - 16, 17, 17), stringFormat);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
