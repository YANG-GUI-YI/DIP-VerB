namespace DIP
{
    partial class DIPSample
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.負片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度調整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.對比ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方圖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方圖轉換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方圖等化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平均濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中值濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.銳化濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.邊緣濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度增加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度降低ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.對比增強ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.對比降低ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rGBtoGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(876, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stStripLabel
            // 
            this.stStripLabel.Name = "stStripLabel";
            this.stStripLabel.Size = new System.Drawing.Size(158, 19);
            this.stStripLabel.Text = "toolStripStatusLabel1";
            this.stStripLabel.Click += new System.EventHandler(this.stStripLabel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.iPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(876, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // iPToolStripMenuItem
            // 
            this.iPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBtoGrayToolStripMenuItem,
            this.負片ToolStripMenuItem,
            this.切片ToolStripMenuItem,
            this.亮度調整ToolStripMenuItem,
            this.對比ToolStripMenuItem,
            this.直方圖ToolStripMenuItem,
            this.濾波器ToolStripMenuItem});
            this.iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            this.iPToolStripMenuItem.Size = new System.Drawing.Size(36, 24);
            this.iPToolStripMenuItem.Text = "&IP";
            this.iPToolStripMenuItem.Click += new System.EventHandler(this.iPToolStripMenuItem_Click);
            // 
            // 負片ToolStripMenuItem
            // 
            this.負片ToolStripMenuItem.Name = "負片ToolStripMenuItem";
            this.負片ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.負片ToolStripMenuItem.Text = "負片";
            this.負片ToolStripMenuItem.Click += new System.EventHandler(this.負片ToolStripMenuItem_Click);
            // 
            // 切片ToolStripMenuItem
            // 
            this.切片ToolStripMenuItem.Name = "切片ToolStripMenuItem";
            this.切片ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.切片ToolStripMenuItem.Text = "切片";
            this.切片ToolStripMenuItem.Click += new System.EventHandler(this.切片ToolStripMenuItem_Click);
            // 
            // 亮度調整ToolStripMenuItem
            // 
            this.亮度調整ToolStripMenuItem.Name = "亮度調整ToolStripMenuItem";
            this.亮度調整ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.亮度調整ToolStripMenuItem.Text = "亮度調整";
            this.亮度調整ToolStripMenuItem.Click += new System.EventHandler(this.亮度調整ToolStripMenuItem_Click);
            // 
            // 對比ToolStripMenuItem
            // 
            this.對比ToolStripMenuItem.Name = "對比ToolStripMenuItem";
            this.對比ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.對比ToolStripMenuItem.Text = "對比";
            this.對比ToolStripMenuItem.Click += new System.EventHandler(this.對比ToolStripMenuItem_Click);
            // 
            // 直方圖ToolStripMenuItem
            // 
            this.直方圖ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直方圖轉換ToolStripMenuItem,
            this.直方圖等化ToolStripMenuItem});
            this.直方圖ToolStripMenuItem.Name = "直方圖ToolStripMenuItem";
            this.直方圖ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.直方圖ToolStripMenuItem.Text = "直方圖";
            // 
            // 直方圖轉換ToolStripMenuItem
            // 
            this.直方圖轉換ToolStripMenuItem.Name = "直方圖轉換ToolStripMenuItem";
            this.直方圖轉換ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.直方圖轉換ToolStripMenuItem.Text = "轉換";
            this.直方圖轉換ToolStripMenuItem.Click += new System.EventHandler(this.直方圖轉換ToolStripMenuItem_Click);
            // 
            // 直方圖等化ToolStripMenuItem
            // 
            this.直方圖等化ToolStripMenuItem.Name = "直方圖等化ToolStripMenuItem";
            this.直方圖等化ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.直方圖等化ToolStripMenuItem.Text = "等化";
            this.直方圖等化ToolStripMenuItem.Click += new System.EventHandler(this.直方圖等化ToolStripMenuItem_Click);
            // 
            // 濾波器ToolStripMenuItem
            // 
            this.濾波器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.平均濾波ToolStripMenuItem,
            this.中值濾波ToolStripMenuItem,
            this.銳化濾波ToolStripMenuItem,
            this.邊緣濾波ToolStripMenuItem});
            this.濾波器ToolStripMenuItem.Name = "濾波器ToolStripMenuItem";
            this.濾波器ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.濾波器ToolStripMenuItem.Text = "濾波器";
            // 
            // 平均濾波ToolStripMenuItem
            // 
            this.平均濾波ToolStripMenuItem.Name = "平均濾波ToolStripMenuItem";
            this.平均濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.平均濾波ToolStripMenuItem.Text = "平均";
            this.平均濾波ToolStripMenuItem.Click += new System.EventHandler(this.平均濾波ToolStripMenuItem_Click);
            // 
            // 中值濾波ToolStripMenuItem
            // 
            this.中值濾波ToolStripMenuItem.Name = "中值濾波ToolStripMenuItem";
            this.中值濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.中值濾波ToolStripMenuItem.Text = "中值";
            this.中值濾波ToolStripMenuItem.Click += new System.EventHandler(this.中值濾波ToolStripMenuItem_Click);
            // 
            // 銳化濾波ToolStripMenuItem
            // 
            this.銳化濾波ToolStripMenuItem.Name = "銳化濾波ToolStripMenuItem";
            this.銳化濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.銳化濾波ToolStripMenuItem.Text = "銳化";
            this.銳化濾波ToolStripMenuItem.Click += new System.EventHandler(this.銳化濾波ToolStripMenuItem_Click);
            // 
            // 邊緣濾波ToolStripMenuItem
            // 
            this.邊緣濾波ToolStripMenuItem.Name = "邊緣濾波ToolStripMenuItem";
            this.邊緣濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.邊緣濾波ToolStripMenuItem.Text = "邊緣";
            this.邊緣濾波ToolStripMenuItem.Click += new System.EventHandler(this.邊緣濾波ToolStripMenuItem_Click);
            // 
            // 亮度增加ToolStripMenuItem
            // 
            this.亮度增加ToolStripMenuItem.Name = "亮度增加ToolStripMenuItem";
            this.亮度增加ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.亮度增加ToolStripMenuItem.Text = "增加";
            this.亮度增加ToolStripMenuItem.Click += new System.EventHandler(this.亮度增加ToolStripMenuItem_Click);
            // 
            // 亮度降低ToolStripMenuItem
            // 
            this.亮度降低ToolStripMenuItem.Name = "亮度降低ToolStripMenuItem";
            this.亮度降低ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.亮度降低ToolStripMenuItem.Text = "降低";
            this.亮度降低ToolStripMenuItem.Click += new System.EventHandler(this.亮度降低ToolStripMenuItem_Click);
            // 
            // 對比增強ToolStripMenuItem
            // 
            this.對比增強ToolStripMenuItem.Name = "對比增強ToolStripMenuItem";
            this.對比增強ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.對比增強ToolStripMenuItem.Text = "增強";
            this.對比增強ToolStripMenuItem.Click += new System.EventHandler(this.對比增強ToolStripMenuItem_Click);
            // 
            // 對比降低ToolStripMenuItem
            // 
            this.對比降低ToolStripMenuItem.Name = "對比降低ToolStripMenuItem";
            this.對比降低ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.對比降低ToolStripMenuItem.Text = "降低";
            this.對比降低ToolStripMenuItem.Click += new System.EventHandler(this.對比降低ToolStripMenuItem_Click);
            // 
            // oFileDlg
            // 
            this.oFileDlg.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // rGBtoGrayToolStripMenuItem
            // 
            this.rGBtoGrayToolStripMenuItem.Name = "rGBtoGrayToolStripMenuItem";
            this.rGBtoGrayToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rGBtoGrayToolStripMenuItem.Text = "灰階";
            this.rGBtoGrayToolStripMenuItem.Click += new System.EventHandler(this.灰階ToolStripMenuItem_Click);
            // 
            // DIPSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 433);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DIPSample";
            this.Text = "DIPSample";
            this.Load += new System.EventHandler(this.DIPSample_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stStripLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog oFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 負片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度調整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度增加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度降低ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 對比ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 對比增強ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 對比降低ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方圖ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方圖轉換ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方圖等化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平均濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中值濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 銳化濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 邊緣濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBtoGrayToolStripMenuItem;
    }
}
