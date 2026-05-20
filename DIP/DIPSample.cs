using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DIP
{
    public partial class DIPSample : Form
    {
        private const string DllName = "dip_proc.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Negative(IntPtr input, IntPtr output, int length);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void GrayScale(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Brightness(IntPtr input, IntPtr output, int length, int value);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void Contrast(IntPtr input, IntPtr output, int length, double factor);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void BitPlaneSlice(IntPtr input, IntPtr output, int width, int height, int byteDepth, int bitPlane);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void HistogramEqualization(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SpatialFilter(IntPtr input, IntPtr output, int width, int height, int byteDepth, int filterType, int kernelSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void OtsuThreshold(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void LineDetection(IntPtr input, IntPtr output, int width, int height, int byteDepth, int lineType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void CannyEdgeDetection(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        private Bitmap NpBitmap;
        private int w, h;

        public DIPSample()
        {
            InitializeComponent();
        }

        private delegate void ImageOperation(IntPtr input, IntPtr output, int width, int height, int byteDepth, int length);

        private sealed class ImageContext
        {
            public int[] Input;
            public int ByteDepth;
            public PixelFormat PixelFormat;
            public ColorPalette Palette;
            public int Width;
            public int Height;
        }

        private void DIPSample_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
            stStripLabel.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oFileDlg.CheckFileExists = true;
            oFileDlg.CheckPathExists = true;
            oFileDlg.Title = "Open File - DIP Sample";
            oFileDlg.ValidateNames = true;
            oFileDlg.Filter = "bmp files (*.bmp)|*.bmp";
            oFileDlg.FileName = "";

            if (oFileDlg.ShowDialog() == DialogResult.OK)
            {
                MSForm childForm = new MSForm();
                childForm.MdiParent = this;
                childForm.pf1 = stStripLabel;
                NpBitmap = bmp_read(oFileDlg);
                childForm.pBitmap = NpBitmap;
                w = NpBitmap.Width;
                h = NpBitmap.Height;
                childForm.Show();
            }
        }

        private Bitmap bmp_read(OpenFileDialog oFileDlg)
        {
            Bitmap pBitmap = new Bitmap(oFileDlg.FileName);
            w = pBitmap.Width;
            h = pBitmap.Height;
            return pBitmap;
        }

        private int[] dyn_bmp2array(Bitmap myBitmap, ref int byteDepth, ref PixelFormat pixelFormat, ref ColorPalette palette)
        {
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                ImageLockMode.ReadOnly,
                myBitmap.PixelFormat);

            pixelFormat = myBitmap.PixelFormat;
            palette = myBitmap.Palette;
            byteDepth = Image.GetPixelFormatSize(myBitmap.PixelFormat) / 8;
            int[] imgData = new int[myBitmap.Width * myBitmap.Height * byteDepth];
            int byteOfSkip = byteArray.Stride - myBitmap.Width * byteDepth;

            unsafe
            {
                byte* imgPtr = (byte*)byteArray.Scan0;
                for (int y = 0; y < byteArray.Height; y++)
                {
                    for (int x = 0; x < byteArray.Width; x++)
                    {
                        for (int c = 0; c < byteDepth; c++)
                        {
                            imgData[(x + byteArray.Width * y) * byteDepth + c] = *imgPtr;
                            imgPtr++;
                        }
                    }
                    imgPtr += byteOfSkip;
                }
            }
            myBitmap.UnlockBits(byteArray);
            return imgData;
        }

        private static Bitmap dyn_array2bmp(int[] imgData, int width, int height, int byteDepth, PixelFormat pixelFormat, ColorPalette palette)
        {
            Bitmap myBitmap = new Bitmap(width, height, pixelFormat);
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                pixelFormat);

            try
            {
                myBitmap.Palette = palette;
            }
            catch
            {
            }

            int byteOfSkip = byteArray.Stride - myBitmap.Width * byteDepth;
            unsafe
            {
                byte* imgPtr = (byte*)byteArray.Scan0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int c = 0; c < byteDepth; c++)
                        {
                            *imgPtr = (byte)imgData[(x + width * y) * byteDepth + c];
                            imgPtr++;
                        }
                    }
                    imgPtr += byteOfSkip;
                }
            }
            myBitmap.UnlockBits(byteArray);
            return myBitmap;
        }

        private MSForm ActiveImageForm()
        {
            foreach (MSForm child in MdiChildren)
            {
                if (child.Focused || child == ActiveMdiChild)
                {
                    return child;
                }
            }
            return null;
        }

        private Bitmap GetActiveBitmapClone()
        {
            MSForm activeForm = ActiveImageForm();
            if (activeForm == null || activeForm.pBitmap == null)
            {
                MessageBox.Show("請先開啟並選取一張影像。", "DIP");
                return null;
            }
            return new Bitmap(activeForm.pBitmap);
        }

        private ImageContext GetActiveImageContext()
        {
            MSForm activeForm = ActiveImageForm();
            if (activeForm == null || activeForm.pBitmap == null)
            {
                MessageBox.Show("請先開啟並選取一張影像。", "DIP");
                return null;
            }

            int byteDepth = 0;
            PixelFormat pixelFormat = new PixelFormat();
            ColorPalette palette = null;
            return new ImageContext
            {
                Width = activeForm.pBitmap.Width,
                Height = activeForm.pBitmap.Height,
                Input = dyn_bmp2array(activeForm.pBitmap, ref byteDepth, ref pixelFormat, ref palette),
                ByteDepth = byteDepth,
                PixelFormat = pixelFormat,
                Palette = palette
            };
        }

        private Bitmap ProcessImage(ImageContext context, ImageOperation operation)
        {
            int[] output = new int[context.Input.Length];

            unsafe
            {
                fixed (int* f0 = context.Input)
                fixed (int* g0 = output)
                {
                    operation((IntPtr)f0, (IntPtr)g0, context.Width, context.Height, context.ByteDepth, context.Input.Length);
                }
            }

            return dyn_array2bmp(output, context.Width, context.Height, context.ByteDepth, context.PixelFormat, context.Palette);
        }

        private void ApplyImageOperation(ImageOperation operation)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            NpBitmap = ProcessImage(context, operation);
            ShowImage(NpBitmap);
        }

        private void ShowImage(Bitmap bitmap)
        {
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = bitmap;
            childForm.Show();
        }

        private int[] BuildHistogram(ImageContext context)
        {
            int[] histogram = new int[256];
            for (int y = 0; y < context.Height; y++)
            {
                for (int x = 0; x < context.Width; x++)
                {
                    int index = (y * context.Width + x) * context.ByteDepth;
                    int gray;
                    if (context.ByteDepth >= 3)
                    {
                        int b = context.Input[index];
                        int g = context.Input[index + 1];
                        int r = context.Input[index + 2];
                        gray = Clamp((int)(0.114 * b + 0.587 * g + 0.299 * r + 0.5));
                    }
                    else
                    {
                        gray = Clamp(context.Input[index]);
                    }
                    histogram[gray]++;
                }
            }
            return histogram;
        }

        private int[] BuildHistogram(Bitmap bitmap)
        {
            int byteDepth = 0;
            PixelFormat pixelFormat = new PixelFormat();
            ColorPalette palette = null;
            return BuildHistogram(new ImageContext
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                Input = dyn_bmp2array(bitmap, ref byteDepth, ref pixelFormat, ref palette),
                ByteDepth = byteDepth,
                PixelFormat = pixelFormat,
                Palette = palette
            });
        }

        private static int Clamp(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 255)
            {
                return 255;
            }
            return value;
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                GrayScale(input, output, width, height, byteDepth));
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                Negative(input, output, length));
        }

        private void bitSliceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            TrackPreviewForm dialog = new TrackPreviewForm(
                "位元切片",
                1,
                8,
                1,
                value => ProcessImage(context, (input, output, width, height, byteDepth, length) =>
                    BitPlaneSlice(input, output, width, height, byteDepth, value)),
                value => "Bit plane " + value + "（第 " + value + " 位元）");
            dialog.Show(this);
        }

        private void otsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                OtsuThreshold(input, output, width, height, byteDepth));
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            TrackPreviewForm dialog = new TrackPreviewForm(
                "亮度調整",
                -255,
                255,
                0,
                value => ProcessImage(context, (input, output, width, height, byteDepth, length) =>
                    Brightness(input, output, length, value)),
                value => "亮度：" + value);
            dialog.Show(this);
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            TrackPreviewForm dialog = new TrackPreviewForm(
                "對比",
                0,
                300,
                100,
                value =>
                {
                    double factor = value / 100.0;
                    return ProcessImage(context, (input, output, width, height, byteDepth, length) =>
                        Contrast(input, output, length, factor));
                },
                value => "對比：" + (value / 100.0).ToString("0.00"));
            dialog.Show(this);
        }

        private void histogramShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            HistogramForm dialog = new HistogramForm("直方圖", BuildHistogram(context));
            dialog.Show(this);
        }

        private void histogramEqualizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            NpBitmap = ProcessImage(context, (input, output, width, height, byteDepth, length) =>
                HistogramEqualization(input, output, width, height, byteDepth));
            ShowImage(NpBitmap);

            HistogramForm dialog = new HistogramForm("等化後直方圖", BuildHistogram(NpBitmap));
            dialog.Show(this);
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap source = GetActiveBitmapClone();
            if (source == null)
            {
                return;
            }

            using (source)
            using (ValueInputForm dialog = new ValueInputForm("放大縮小", "比例（%）：", "100"))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (double.TryParse(dialog.InputValue, out double percent) && percent > 0)
                {
                    NpBitmap = ScaleBitmap(source, percent / 100.0);
                    ShowImage(NpBitmap);
                }
                else
                {
                    MessageBox.Show("請輸入大於 0 的比例。", "DIP");
                }
            }
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap source = GetActiveBitmapClone();
            if (source == null)
            {
                return;
            }

            using (source)
            using (ValueInputForm dialog = new ValueInputForm("圖片旋轉", "角度：", "0"))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (double.TryParse(dialog.InputValue, out double angle))
                {
                    NpBitmap = RotateBitmap(source, angle);
                    ShowImage(NpBitmap);
                }
                else
                {
                    MessageBox.Show("請輸入有效角度。", "DIP");
                }
            }
        }

        private static Bitmap ScaleBitmap(Bitmap source, double scale)
        {
            int newWidth = Math.Max(1, (int)Math.Round(source.Width * scale));
            int newHeight = Math.Max(1, (int)Math.Round(source.Height * scale));
            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.White);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(source, new Rectangle(0, 0, newWidth, newHeight));
            }
            return result;
        }

        private static Bitmap RotateBitmap(Bitmap source, double angle)
        {
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));
            int newWidth = Math.Max(1, (int)Math.Ceiling(source.Width * cos + source.Height * sin));
            int newHeight = Math.Max(1, (int)Math.Ceiling(source.Width * sin + source.Height * cos));

            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.White);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TranslateTransform(newWidth / 2f, newHeight / 2f);
                g.RotateTransform((float)angle);
                g.TranslateTransform(-source.Width / 2f, -source.Height / 2f);
                g.DrawImage(source, new PointF(0, 0));
            }
            return result;
        }

        private void meanFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 0, 3));
        }

        private void medianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 1, 3));
        }

        private void sharpenFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 2, 3));
        }

        private void edgeFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 3, 3));
        }

        private void laplacianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 4, 3));
        }

        private void cannyEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                CannyEdgeDetection(input, output, width, height, byteDepth));
        }

        private void stStripLabel_Click(object sender, EventArgs e)
        {
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private sealed class TrackPreviewForm : Form
        {
            private readonly PictureBox pictureBox;
            private readonly TrackBar trackBar;
            private readonly Label valueLabel;
            private readonly Func<int, Bitmap> render;
            private readonly Func<int, string> describe;

            public TrackPreviewForm(string title, int minimum, int maximum, int value, Func<int, Bitmap> render, Func<int, string> describe)
            {
                this.render = render;
                this.describe = describe;

                Text = title;
                Width = 760;
                Height = 620;
                StartPosition = FormStartPosition.CenterParent;

                pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Black
                };

                valueLabel = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 32,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                trackBar = new TrackBar
                {
                    Dock = DockStyle.Bottom,
                    Minimum = minimum,
                    Maximum = maximum,
                    Value = value,
                    TickFrequency = Math.Max(1, (maximum - minimum) / 10),
                    SmallChange = 1,
                    LargeChange = Math.Max(1, (maximum - minimum) / 10)
                };
                trackBar.Scroll += delegate { RefreshPreview(); };
                trackBar.ValueChanged += delegate { RefreshPreview(); };

                Controls.Add(pictureBox);
                Controls.Add(valueLabel);
                Controls.Add(trackBar);

                RefreshPreview();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }
                base.Dispose(disposing);
            }

            private void RefreshPreview()
            {
                Image oldImage = pictureBox.Image;
                pictureBox.Image = render(trackBar.Value);
                if (oldImage != null)
                {
                    oldImage.Dispose();
                }
                valueLabel.Text = describe(trackBar.Value);
            }
        }

        private sealed class ValueInputForm : Form
        {
            private readonly TextBox textBox;
            private readonly Button okButton;
            private readonly Button cancelButton;

            public string InputValue
            {
                get { return textBox.Text; }
            }

            public ValueInputForm(string title, string labelText, string defaultValue)
            {
                Text = title;
                Width = 320;
                Height = 150;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                StartPosition = FormStartPosition.CenterParent;

                Label label = new Label
                {
                    Left = 16,
                    Top = 18,
                    Width = 260,
                    Text = labelText
                };

                textBox = new TextBox
                {
                    Left = 16,
                    Top = 44,
                    Width = 270,
                    Text = defaultValue
                };

                okButton = new Button
                {
                    Left = 130,
                    Top = 78,
                    Width = 75,
                    Text = "確認",
                    DialogResult = DialogResult.OK
                };

                cancelButton = new Button
                {
                    Left = 212,
                    Top = 78,
                    Width = 75,
                    Text = "取消",
                    DialogResult = DialogResult.Cancel
                };

                AcceptButton = okButton;
                CancelButton = cancelButton;

                Controls.Add(label);
                Controls.Add(textBox);
                Controls.Add(okButton);
                Controls.Add(cancelButton);
            }
        }

        private sealed class HistogramForm : Form
        {
            private readonly PictureBox pictureBox;

            public HistogramForm(string title, int[] histogram)
            {
                Text = title;
                Width = 760;
                Height = 460;
                StartPosition = FormStartPosition.CenterParent;

                pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.White,
                    Image = DrawHistogram(histogram)
                };

                Controls.Add(pictureBox);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }
                base.Dispose(disposing);
            }

            private static Bitmap DrawHistogram(int[] histogram)
            {
                int width = 640;
                int height = 360;
                int paddingLeft = 48;
                int paddingBottom = 36;
                int paddingTop = 20;
                int paddingRight = 20;
                int chartWidth = width - paddingLeft - paddingRight;
                int chartHeight = height - paddingTop - paddingBottom;

                int max = 1;
                for (int i = 0; i < histogram.Length; i++)
                {
                    if (histogram[i] > max)
                    {
                        max = histogram[i];
                    }
                }

                Bitmap bitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bitmap))
                using (Pen axisPen = new Pen(Color.Black))
                using (Pen barPen = new Pen(Color.FromArgb(40, 90, 170)))
                using (Font font = new Font("Arial", 9))
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    g.Clear(Color.White);
                    int x0 = paddingLeft;
                    int y0 = height - paddingBottom;
                    g.DrawLine(axisPen, x0, paddingTop, x0, y0);
                    g.DrawLine(axisPen, x0, y0, width - paddingRight, y0);

                    for (int i = 0; i < 256; i++)
                    {
                        int x = x0 + i * chartWidth / 256;
                        int barHeight = histogram[i] * chartHeight / max;
                        g.DrawLine(barPen, x, y0, x, y0 - barHeight);
                    }

                    g.DrawString("0", font, textBrush, x0 - 6, y0 + 8);
                    g.DrawString("255", font, textBrush, width - paddingRight - 24, y0 + 8);
                    g.DrawString(max.ToString(), font, textBrush, 4, paddingTop - 8);
                }

                return bitmap;
            }
        }
    }
}
