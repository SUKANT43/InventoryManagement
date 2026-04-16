using System.Drawing;

namespace Admin.Component
{
    partial class CustomTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // textBox1
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Multiline = true;
            this.textBox1.Font = new System.Drawing.Font(
                "Segoe UI",
                10.8F,
                System.Drawing.FontStyle.Regular
            );
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.TabIndex = 0;

            // label1
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Regular
            );
            this.label1.ForeColor = System.Drawing.Color.FromArgb(130, 130, 130);
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Text = "Username";

            // CustomTextBox
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
            this.Padding = new System.Windows.Forms.Padding(16, 20, 16, 12);
            this.Size = new System.Drawing.Size(260, 54);

            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}
