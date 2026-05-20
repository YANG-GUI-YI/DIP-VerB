using System;
using System.ComponentModel;
using System.Drawing;
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
        private static extern void GrayLevelSlice(IntPtr input, IntPtr output, int width, int height, int byteDepth, int low, int high, int highlight, int keepBackground);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void BitPlaneSlice(IntPtr input, IntPtr output, int width, int height, int byteDepth, int bitPlane);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void HistogramStretch(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void HistogramEqualization(IntPtr input, IntPtr output, int width, int height, int byteDepth);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SpatialFilter(IntPtr input, IntPtr output, int width, int height, int byteDepth, int filterType, int kernelSize);

        private Bitmap NpBitmap;
        private int w, h;

        public DIPSample()
        {
            InitializeComponent();
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

        private void 灰階ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                GrayScale(input, output, width, height, byteDepth));
        }

        private void 負片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                Negative(input, output, length));
        }

        private void 亮度增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBrightnessDialog();
        }

        private void 亮度降低ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBrightnessDialog();
        }

        private void 亮度調整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBrightnessDialog();
        }

        private void 對比增強ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowContrastDialog();
        }

        private void 對比降低ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowContrastDialog();
        }

        private void 對比ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowContrastDialog();
        }

        private void 切片ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 直方圖轉換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageContext context = GetActiveImageContext();
            if (context == null)
            {
                return;
            }

            HistogramForm dialog = new HistogramForm("直方圖", BuildHistogram(context));
            dialog.Show(this);
        }

        private void 直方圖等化ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 平均濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 0, 3));
        }

        private void 中值濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 1, 3));
        }

        private void 銳化濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 2, 3));
        }

        private void 邊緣濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyImageOperation((input, output, width, height, byteDepth, length) =>
                SpatialFilter(input, output, width, height, byteDepth, 3, 3));
        }

        private void ShowBrightnessDialog()
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

        private void ShowContrastDialog()
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
