namespace Admin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.MinimumSize = new System.Drawing.Size(1100, 680);
            this.customButton1 = new Admin.Component.CustomButton();
            this.loginPage1 = new Admin.Pages.LoginPage();
            this.pageBorder1 = new Admin.Component.PageBorder();
            this.SuspendLayout();
            // 
            // customButton1
            // 
            this.customButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.customButton1.Location = new System.Drawing.Point(459, 129);
            this.customButton1.Name = "customButton1";
            this.customButton1.Radius = 10;
            this.customButton1.Size = new System.Drawing.Size(123, 47);
            this.customButton1.TabIndex = 2;
            this.customButton1.Text = "ewr";
            // 
            // loginPage1
            // 
            this.loginPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPage1.Location = new System.Drawing.Point(0, 34);
            this.loginPage1.Name = "loginPage1";
            this.loginPage1.Size = new System.Drawing.Size(800, 416);
            this.loginPage1.TabIndex = 1;
            // 
            // pageBorder1
            // 
            this.pageBorder1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageBorder1.Location = new System.Drawing.Point(0, 0);
            this.pageBorder1.Name = "pageBorder1";
            this.pageBorder1.Size = new System.Drawing.Size(800, 34);
            this.pageBorder1.TabIndex = 0;
            this.pageBorder1.Text = "pageBorder1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.loginPage1);
            this.Controls.Add(this.pageBorder1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StockFlow Inventory Management";
            this.ResumeLayout(false);
        }

        #endregion

        private Component.PageBorder pageBorder1;
        private Pages.LoginPage loginPage1;
        private Component.CustomButton customButton1;
    }
}
