using System;
using System.Drawing;
using System.Windows.Forms;
using Admin.ThemeManager;

namespace Admin.Component
{
    public class PageBorder : Control
    {
        private Timer minimizeTimer;
        private int targetHeight;
        private int targetWidth;

        private Rectangle closeRect;
        private Rectangle minimizeRect;

        private bool hoverClose;
        private bool hoverMin;

        private bool isMouseDown;
        private Point lastPoint;

        public PageBorder()
        {
            DoubleBuffered = true;
            Height = 40;

            minimizeTimer = new Timer();
            minimizeTimer.Interval = 10;
            minimizeTimer.Tick += MinimizeTimerTick;

            Thememaintainer.ThemeChange += (s, e) => Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            closeRect = new Rectangle(Width - 40, 0, 40, Height);
            minimizeRect = new Rectangle(Width - 80, 0, 40, Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var theme = Thememaintainer.CurrentTheme;

            using (SolidBrush bg = new SolidBrush(theme.SecondaryII))
                g.FillRectangle(bg, ClientRectangle);

            DrawButton(g, minimizeRect, "—", hoverMin, theme.PrimaryI);

            DrawButton(g, closeRect, "X", hoverClose, theme.Danger);
        }

        private void DrawButton(Graphics g, Rectangle rect, string text, bool hover, Color baseColor)
        {
            Color bg = hover
                ? Thememaintainer.GetHoverColor(baseColor)
                : Color.Transparent;

            using (SolidBrush brush = new SolidBrush(bg))
                g.FillRectangle(brush, rect);

            using (Brush textBrush = new SolidBrush(Thememaintainer.CurrentTheme.TextPrimary))
            using (StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(text, new Font("Segoe UI", 12, FontStyle.Bold), textBrush, rect, sf);
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            hoverClose = closeRect.Contains(e.Location);
            hoverMin = minimizeRect.Contains(e.Location);

            if (isMouseDown)
            {
                Form form = FindForm();
                if (form != null)
                {
                    form.Left += e.X - lastPoint.X;
                    form.Top += e.Y - lastPoint.Y;
                }
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            hoverClose = false;
            hoverMin = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!closeRect.Contains(e.Location) && !minimizeRect.Contains(e.Location))
            {
                isMouseDown = true;
                lastPoint = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            isMouseDown = false;

            Form form = FindForm();
            if (form == null) return;

            if (closeRect.Contains(e.Location))
            {
                form.Close();
            }
            else if (minimizeRect.Contains(e.Location))
            {
                targetHeight = form.Height;
                targetWidth = form.Width;
                minimizeTimer.Start();
            }
        }

        private void MinimizeTimerTick(object sender, EventArgs e)
        {
            Form form = FindForm();
            if (form == null) return;

            if (form.Height > 80 && form.Width > 150)
            {
                form.Height -= 30;
                form.Width -= 30;
                form.Opacity -= 0.05;
            }
            else
            {
                minimizeTimer.Stop();

                form.Opacity = 1;
                form.Height = targetHeight;
                form.Width = targetWidth;

                form.WindowState = FormWindowState.Minimized;
            }
        }
    }
}