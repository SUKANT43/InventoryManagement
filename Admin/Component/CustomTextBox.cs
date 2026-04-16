using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Admin.Component
{
    public partial class CustomTextBox : UserControl
    {
        Timer timer;
        bool isFocus = false;
        Point lastPoint;

        //border values
        private int _curverRadius = 14;
        public int CurveRadius
        {
            get => _curverRadius;
            set => _curverRadius = value;
        }

        private Color _textBoxborderColor = Color.FromArgb(226, 232, 240);
        public Color TextBoxBorderColor
        {
            get => _textBoxborderColor;
            set => _textBoxborderColor = value;
        }

        //placeholder values
        private Color _placeHolderColor = Color.FromArgb(148, 163, 184);
        public Color PlaceHolderColor
        {
            get => _placeHolderColor;
            set => _placeHolderColor = value;
        }

        private Color _titleColor = Color.FromArgb(37, 99, 235);
        public Color TitleColor
        {
            get => _titleColor;
            set => _titleColor = value;
        }

        private Font _placeHolderFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public Font PlaceHolderFont
        {
            get => _placeHolderFont;
            set => _placeHolderFont = value;
        }

        private Font _titleFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        public Font TitleFont
        {
            get => _titleFont;
            set => _titleFont = value;
        }

        //Textbox values
        public string TextBoxText
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        public Color TextBoxBackColor
        {
            get => textBox1.BackColor;
            set => textBox1.BackColor = value;
        }

        public Color TextBoxForeColor
        {
            get => textBox1.ForeColor;
            set => textBox1.ForeColor = value;
        }

        public Font TextBoxFont
        {
            get => textBox1.Font;
            set => textBox1.Font = value;
        }


        public CustomTextBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += DrawTextBoxUi;
            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += MovePlaceholder;
            textBox1.GotFocus += TextBoxGotFocus;
            textBox1.LostFocus += TextBoxLostFocus;
            label1.Click += (s, e) =>
            {
                textBox1.Focus();
                isFocus = true;
            };
        }

        private void DrawTextBoxUi(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //DrawCurvedPath(g);
        }

        private void DrawCurvedPath(Graphics g)
        {
            int diameter = _curverRadius * 2;

            Rectangle rect = new Rectangle(
                textBox1.Left,
                textBox1.Top ,
                textBox1.Width ,
                textBox1.Height 
            );

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(_textBoxborderColor, 2))
            {
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

                path.CloseFigure();
                Region = new Region(path);
                g.DrawPath(pen, path);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
            AlignUi();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Invalidate();
            AlignUi();
        }

        private void AlignUi()
        {
            if (isFocus)
            {
                label1.Location = new Point(10, 0);
                label1.ForeColor = _titleColor;
                label1.Font = _titleFont;
            }
            else
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    label1.Location = new Point((Width / 2) - (label1.Width / 2), (Height / 2) - (label1.Height / 2));
                    label1.ForeColor = _placeHolderColor;
                    label1.Font = _placeHolderFont;

                }
                else
                {
                    label1.Location = new Point(10, 0);
                    label1.ForeColor = _titleColor;
                    label1.Font = _titleFont;

                }
            }
        }

        private void TextBoxLostFocus(object sender, EventArgs e)
        {
            isFocus = false;
            timer.Start();
        }

        private void TextBoxGotFocus(object sender, EventArgs e)
        {
            isFocus = true;
            timer.Start();
        }

        private void MovePlaceholder(object sender, EventArgs e)
        {
            lastPoint = label1.Location;

            if (isFocus)
            {
                int newX = label1.Location.X;
                int newY = label1.Location.Y;

                if (newX > 10)
                    newX -= 2;

                if (newY > 0)
                    newY -= 2;

                if (newX < 10)
                    newX = 10;

                if (newY < 0)
                    newY = 0;

                label1.Location = new Point(newX, newY);

                if (newX == 10 && newY == 0)
                {
                    label1.ForeColor = _titleColor;
                    label1.Font = _titleFont;
                    timer.Stop();
                }
            }
            else
            {
                int expectedX = (Width / 2) - (label1.Width / 2);
                int expectedY = (Height / 2) - (label1.Height / 2);
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    int newX = label1.Location.X;
                    int newY = label1.Location.Y;
                    if (newX - 2 < expectedX)
                    {
                        newX += 2;
                    }
                    if (newY - 2 < expectedY)
                    {
                        newY += 2;
                    }
                    if (newX > expectedX)
                    {
                        newX = expectedX;
                    }
                    if (newY > expectedY)
                    {
                        newY = expectedY;
                    }
                    label1.Location = new Point(newX, newY);
                    if (newX == expectedX && newY == expectedY)
                    {
                        label1.ForeColor = _placeHolderColor;
                        label1.Font = _placeHolderFont;
                        timer.Stop();
                    }
                }
                else
                {
                    label1.ForeColor = _titleColor;
                    label1.Font = _titleFont;
                    timer.Stop();
                }
            }
        }

        //private void MovePlaceholder(object sender, EventArgs e)
        //{
        //    int speed = 2;

        //    Point targetPoint;

        //    if (isFocus)
        //    {
        //        targetPoint = new Point(10, 0);
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(textBox1.Text))
        //        {
        //            label1.ForeColor = _titleColor;
        //            label1.Font = _titleFont;
        //            timer.Stop();
        //            return;
        //        }

        //        targetPoint = new Point(
        //            (Width / 2) - (label1.Width / 2),
        //            (Height / 2) - (label1.Height / 2)
        //        );
        //    }

        //    int currentX = label1.Location.X;
        //    int currentY = label1.Location.Y;

        //    int deltaX = targetPoint.X - currentX;
        //    int deltaY = targetPoint.Y - currentY;

        //    int stepX = 0;
        //    int stepY = 0;

        //    if (deltaX != 0)
        //        stepX = Math.Sign(deltaX) * Math.Min(speed, Math.Abs(deltaX));

        //    if (deltaY != 0)
        //        stepY = Math.Sign(deltaY) * Math.Min(speed, Math.Abs(deltaY));

        //    label1.Location = new Point(
        //        currentX + stepX,
        //        currentY + stepY
        //    );

        //    if (label1.Location == targetPoint)
        //    {
        //        if (isFocus)
        //        {
        //            label1.ForeColor = _titleColor;
        //            label1.Font = _titleFont;
        //        }
        //        else
        //        {
        //            label1.ForeColor = _placeHolderColor;
        //            label1.Font = _placeHolderFont;
        //        }

        //        timer.Stop();
        //    }
        //}

    }
}
