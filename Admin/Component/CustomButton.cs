using Admin.ThemeManager;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Admin.Component
{
    class CustomButton : Control
    {
        private int radius = 10;
        private bool isHover = false;
        private bool isPressed = false;

        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                Invalidate();
            }
        }

        public CustomButton()
        {
            DoubleBuffered = true;
            Cursor = Cursors.Hand;

            this.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            Thememaintainer.ThemeChange += (s, e) => Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHover = false;
            isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var theme = Thememaintainer.CurrentTheme;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Color baseColor = theme.PrimaryI;

            if (isPressed)
                baseColor = theme.PrimaryIII;
            else if (isHover)
                baseColor = Thememaintainer.GetHoverColor(theme.PrimaryI);

            using (GraphicsPath path = GetRoundedPath(rect, radius))
            using (SolidBrush brush = new SolidBrush(baseColor))
            {
                g.FillPath(brush, path);
            }

            using (Brush textBrush = new SolidBrush(theme.ButtonTextColor))
            using (StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(this.Text, this.Font, textBrush, rect, sf);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            int r = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}