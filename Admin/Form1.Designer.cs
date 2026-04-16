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
            this.customTextBox3 = new Admin.Component.CustomTextBox();
            this.customTextBox2 = new Admin.Component.CustomTextBox();
            this.SuspendLayout();
            // 
            // customTextBox3
            // 
            this.customTextBox3.BackColor = System.Drawing.Color.White;
            this.customTextBox3.CurveRadius = 14;
            this.customTextBox3.Location = new System.Drawing.Point(575, 79);
            this.customTextBox3.Name = "customTextBox3";
            this.customTextBox3.Padding = new System.Windows.Forms.Padding(16, 20, 16, 12);
            this.customTextBox3.PlaceHolderColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.customTextBox3.PlaceHolderFont = new System.Drawing.Font("Segoe UI", 10F);
            this.customTextBox3.Size = new System.Drawing.Size(199, 59);
            this.customTextBox3.TabIndex = 2;
            this.customTextBox3.TextBoxBackColor = System.Drawing.Color.White;
            this.customTextBox3.TextBoxBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.customTextBox3.TextBoxFont = new System.Drawing.Font("Segoe UI", 10.8F);
            this.customTextBox3.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.customTextBox3.TextBoxText = "";
            this.customTextBox3.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.customTextBox3.TitleFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            // 
            // customTextBox2
            // 
            this.customTextBox2.BackColor = System.Drawing.Color.White;
            this.customTextBox2.CurveRadius = 14;
            this.customTextBox2.Location = new System.Drawing.Point(575, 154);
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.Padding = new System.Windows.Forms.Padding(16, 20, 16, 12);
            this.customTextBox2.PlaceHolderColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.customTextBox2.PlaceHolderFont = new System.Drawing.Font("Segoe UI", 10F);
            this.customTextBox2.Size = new System.Drawing.Size(183, 57);
            this.customTextBox2.TabIndex = 1;
            this.customTextBox2.TextBoxBackColor = System.Drawing.Color.White;
            this.customTextBox2.TextBoxBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.customTextBox2.TextBoxFont = new System.Drawing.Font("Segoe UI", 10.8F);
            this.customTextBox2.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.customTextBox2.TextBoxText = "";
            this.customTextBox2.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.customTextBox2.TitleFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customTextBox3);
            this.Controls.Add(this.customTextBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private Component.CustomTextBox customTextBox2;
        private Component.CustomTextBox customTextBox3;
    }
}

