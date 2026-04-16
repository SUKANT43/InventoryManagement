using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Admin.Component
{
    public partial class CustomTextBox : UserControl
    {
        private Timer timer;
        private bool isFocus = false;
        private Point lastPoint;


        // border values
        private int _curverRadius = 1;
        public int CurveRadius
        {
            get => _curverRadius;
            set
            {
                _curverRadius =Math.Max(1,value);
                Invalidate();
            }
        }

        private Color _textBoxborderColor = Color.Black;
        public Color TextBoxBorderColor
        {
            get => _textBoxborderColor;
            set
            {
                _textBoxborderColor = Color.Red;
                Invalidate();
            }
        }

        // placeholder values
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

        private Font _titleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        public Font TitleFont
        {
            get => _titleFont;
            set => _titleFont = value;
        }

        // TextBox values
        public string TextBoxText
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        public Color TextBoxBackColor
        {
            get => BackColor;
            set
            {
                BackColor = value;
                textBox1.BackColor = value;
            }

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

            timer = new Timer();
            timer.Interval = 15;
            timer.Tick += MovePlaceholder;

            Paint += DrawTextBoxUi;

            textBox1.GotFocus += TextBoxGotFocus;
            textBox1.LostFocus += TextBoxLostFocus;

            label1.Click += (s, e) =>
            {
                textBox1.Focus();
                isFocus = true;
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AlignUi();
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AlignUi();
            Invalidate();
        }


        private void DrawTextBoxUi(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle borderRect = new Rectangle(
                ClientRectangle.X + 3,
                ClientRectangle.Y + 2,
                ClientRectangle.Width - 6,
                ClientRectangle.Height - 4
            );

            using (Pen p = new Pen(_textBoxborderColor, 2))
            {
                GraphicsPath path = GetGraphicsPath(borderRect);

                g.DrawPath(p, path);

                if (label1.Location.Y <= 2)
                {
                    using (SolidBrush brush = new SolidBrush(BackColor))
                    {
                        Rectangle cutRect = new Rectangle(
                            label1.Left - 4,
                            0,
                            label1.Width + 8,
                            label1.Height
                        );

                        g.FillRectangle(brush, cutRect);
                    }

                    label1.BringToFront();
                }
            }
        }
        private GraphicsPath GetGraphicsPath(Rectangle rect)
        {

            GraphicsPath path = new GraphicsPath();
            int borderRadius = _curverRadius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, borderRadius, borderRadius, 180, 90);
            path.AddArc(rect.Right - borderRadius, rect.Y, borderRadius, borderRadius, 270, 90);
            path.AddArc(rect.Right - borderRadius, rect.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure(); 
            return path;
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
                    label1.Location = new Point(
                        (Width / 2) - (label1.Width / 2),
                        (Height / 2) - (label1.Height / 2)
                    );

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

        private void TextBoxGotFocus(object sender, EventArgs e)
        {
            isFocus = true;
            timer.Start();
        }

        private void TextBoxLostFocus(object sender, EventArgs e)
        {
            isFocus = false;
            timer.Start();
        }

        private void MovePlaceholder(object sender, EventArgs e)
        {
            lastPoint = label1.Location;

            if (isFocus)
            {
                int newX = label1.Location.X;
                int newY = label1.Location.Y;

                if (newX > 10) newX -= 2;
                if (newY > 0) newY -= 2;

                if (newX < 10) newX = 10;
                if (newY < 0) newY = 0;

                label1.Location = new Point(newX, newY);
                Invalidate();

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

                    if (newX < expectedX) newX += 2;
                    if (newY < expectedY) newY += 2;

                    if (newX > expectedX) newX = expectedX;
                    if (newY > expectedY) newY = expectedY;

                    label1.Location = new Point(newX, newY);
                    Invalidate();


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
    }
}