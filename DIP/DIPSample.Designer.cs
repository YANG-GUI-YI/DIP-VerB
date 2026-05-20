namespace DIP
{
    partial class DIPSample
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitSliceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otsuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjustToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramEqualizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geometryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterDetectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laplacianFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cannyEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.basicToolStripMenuItem,
            this.adjustToolStripMenuItem,
            this.geometryToolStripMenuItem,
            this.filterDetectToolStripMenuItem});
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // basicToolStripMenuItem
            // 
            this.basicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayToolStripMenuItem,
            this.negativeToolStripMenuItem,
            this.bitSliceToolStripMenuItem,
            this.otsuToolStripMenuItem});
            this.basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            this.basicToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.basicToolStripMenuItem.Text = "基本處理";
            // 
            // grayToolStripMenuItem
            // 
            this.grayToolStripMenuItem.Name = "grayToolStripMenuItem";
            this.grayToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.grayToolStripMenuItem.Text = "灰階";
            this.grayToolStripMenuItem.Click += new System.EventHandler(this.grayToolStripMenuItem_Click);
            // 
            // negativeToolStripMenuItem
            // 
            this.negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            this.negativeToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.negativeToolStripMenuItem.Text = "負片";
            this.negativeToolStripMenuItem.Click += new System.EventHandler(this.negativeToolStripMenuItem_Click);
            // 
            // bitSliceToolStripMenuItem
            // 
            this.bitSliceToolStripMenuItem.Name = "bitSliceToolStripMenuItem";
            this.bitSliceToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.bitSliceToolStripMenuItem.Text = "位元切片";
            this.bitSliceToolStripMenuItem.Click += new System.EventHandler(this.bitSliceToolStripMenuItem_Click);
            // 
            // otsuToolStripMenuItem
            // 
            this.otsuToolStripMenuItem.Name = "otsuToolStripMenuItem";
            this.otsuToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.otsuToolStripMenuItem.Text = "Otsu分割";
            this.otsuToolStripMenuItem.Click += new System.EventHandler(this.otsuToolStripMenuItem_Click);
            // 
            // adjustToolStripMenuItem
            // 
            this.adjustToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightnessToolStripMenuItem,
            this.contrastToolStripMenuItem,
            this.histogramToolStripMenuItem});
            this.adjustToolStripMenuItem.Name = "adjustToolStripMenuItem";
            this.adjustToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.adjustToolStripMenuItem.Text = "調整與直方圖";
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.brightnessToolStripMenuItem.Text = "亮度調整";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.brightnessToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.contrastToolStripMenuItem.Text = "對比";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramShowToolStripMenuItem,
            this.histogramEqualizeToolStripMenuItem});
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.histogramToolStripMenuItem.Text = "直方圖";
            // 
            // histogramShowToolStripMenuItem
            // 
            this.histogramShowToolStripMenuItem.Name = "histogramShowToolStripMenuItem";
            this.histogramShowToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.histogramShowToolStripMenuItem.Text = "顯示";
            this.histogramShowToolStripMenuItem.Click += new System.EventHandler(this.histogramShowToolStripMenuItem_Click);
            // 
            // histogramEqualizeToolStripMenuItem
            // 
            this.histogramEqualizeToolStripMenuItem.Name = "histogramEqualizeToolStripMenuItem";
            this.histogramEqualizeToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.histogramEqualizeToolStripMenuItem.Text = "等化";
            this.histogramEqualizeToolStripMenuItem.Click += new System.EventHandler(this.histogramEqualizeToolStripMenuItem_Click);
            // 
            // geometryToolStripMenuItem
            // 
            this.geometryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scaleToolStripMenuItem,
            this.rotateToolStripMenuItem});
            this.geometryToolStripMenuItem.Name = "geometryToolStripMenuItem";
            this.geometryToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.geometryToolStripMenuItem.Text = "幾何";
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.scaleToolStripMenuItem.Text = "放大縮小";
            this.scaleToolStripMenuItem.Click += new System.EventHandler(this.scaleToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.rotateToolStripMenuItem.Text = "圖片旋轉";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotateToolStripMenuItem_Click);
            // 
            // filterDetectToolStripMenuItem
            // 
            this.filterDetectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meanFilterToolStripMenuItem,
            this.medianFilterToolStripMenuItem,
            this.sharpenFilterToolStripMenuItem,
            this.edgeFilterToolStripMenuItem,
            this.laplacianFilterToolStripMenuItem,
            this.cannyEdgeToolStripMenuItem});
            this.filterDetectToolStripMenuItem.Name = "filterDetectToolStripMenuItem";
            this.filterDetectToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.filterDetectToolStripMenuItem.Text = "濾波與偵測";
            // 
            // meanFilterToolStripMenuItem
            // 
            this.meanFilterToolStripMenuItem.Name = "meanFilterToolStripMenuItem";
            this.meanFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.meanFilterToolStripMenuItem.Text = "平均濾波";
            this.meanFilterToolStripMenuItem.Click += new System.EventHandler(this.meanFilterToolStripMenuItem_Click);
            // 
            // medianFilterToolStripMenuItem
            // 
            this.medianFilterToolStripMenuItem.Name = "medianFilterToolStripMenuItem";
            this.medianFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.medianFilterToolStripMenuItem.Text = "中值濾波";
            this.medianFilterToolStripMenuItem.Click += new System.EventHandler(this.medianFilterToolStripMenuItem_Click);
            // 
            // sharpenFilterToolStripMenuItem
            // 
            this.sharpenFilterToolStripMenuItem.Name = "sharpenFilterToolStripMenuItem";
            this.sharpenFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.sharpenFilterToolStripMenuItem.Text = "銳化";
            this.sharpenFilterToolStripMenuItem.Click += new System.EventHandler(this.sharpenFilterToolStripMenuItem_Click);
            // 
            // edgeFilterToolStripMenuItem
            // 
            this.edgeFilterToolStripMenuItem.Name = "edgeFilterToolStripMenuItem";
            this.edgeFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.edgeFilterToolStripMenuItem.Text = "邊緣偵測";
            this.edgeFilterToolStripMenuItem.Click += new System.EventHandler(this.edgeFilterToolStripMenuItem_Click);
            // 
            // laplacianFilterToolStripMenuItem
            // 
            this.laplacianFilterToolStripMenuItem.Name = "laplacianFilterToolStripMenuItem";
            this.laplacianFilterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.laplacianFilterToolStripMenuItem.Text = "拉普拉斯濾波";
            this.laplacianFilterToolStripMenuItem.Click += new System.EventHandler(this.laplacianFilterToolStripMenuItem_Click);
            // 
            // cannyEdgeToolStripMenuItem
            // 
            this.cannyEdgeToolStripMenuItem.Name = "cannyEdgeToolStripMenuItem";
            this.cannyEdgeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cannyEdgeToolStripMenuItem.Text = "Canny Edge Detection";
            this.cannyEdgeToolStripMenuItem.Click += new System.EventHandler(this.cannyEdgeToolStripMenuItem_Click);
            // 
            // oFileDlg
            // 
            this.oFileDlg.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
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
        private System.Windows.Forms.ToolStripMenuItem basicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitSliceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otsuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjustToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramEqualizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geometryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterDetectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meanFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laplacianFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cannyEdgeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog oFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
