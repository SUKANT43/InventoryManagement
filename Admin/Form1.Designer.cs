namespace Admin
{
    partial class Form1
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
            this.customTextBox1 = new Admin.Component.CustomTextBox();
            this.SuspendLayout();
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.White;
            this.customTextBox1.CurveRadius = 1;
            this.customTextBox1.Location = new System.Drawing.Point(232, 103);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.Padding = new System.Windows.Forms.Padding(16, 20, 16, 12);
            this.customTextBox1.PlaceHolderColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.customTextBox1.PlaceHolderFont = new System.Drawing.Font("Segoe UI", 10F);
            this.customTextBox1.Size = new System.Drawing.Size(283, 84);
            this.customTextBox1.TabIndex = 0;
            this.customTextBox1.TextBoxBackColor = System.Drawing.Color.White;
            this.customTextBox1.TextBoxBorderColor = System.Drawing.Color.Black;
            this.customTextBox1.TextBoxFont = new System.Drawing.Font("Segoe UI", 10.8F);
            this.customTextBox1.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.customTextBox1.TextBoxText = "";
            this.customTextBox1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.customTextBox1.TitleFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Component.CustomTextBox customTextBox1;
    }
}

