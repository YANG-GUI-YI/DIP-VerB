using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DIP
{

        public partial class DIPSample : Form
        {
                public DIPSample()
                {
                        InitializeComponent();
                }

        [DllImport("dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]

        unsafe public static extern void encode_gray(int* f, int w, int h, int* g, int d);

                Bitmap NpBitmap;
                int[] f;
                int[] g;
                int w, h;

                private void DIPSample_Load(object sender, EventArgs e)
                {
                        this.IsMdiContainer = true;
                        this.WindowState = FormWindowState.Maximized;
                        this.stStripLabel.Text = "";
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
                        Bitmap pBitmap;
                        string fileloc = oFileDlg.FileName;
                        pBitmap = new Bitmap(fileloc);
                        w = pBitmap.Width;
                        h = pBitmap.Height;
                        return pBitmap;
                }

                private int[] dyn_bmp2array(Bitmap myBitmap, ref int ByteDepth, ref PixelFormat pixelFormat, ref ColorPalette palette)
                {
                        BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                                                      ImageLockMode.ReadWrite,
                                                      myBitmap.PixelFormat);
                        pixelFormat = myBitmap.PixelFormat;
                        palette = myBitmap.Palette;
                        ByteDepth = (int)(byteArray.Stride / myBitmap.Width);
                        int[] ImgData = new int[myBitmap.Width * myBitmap.Height * ByteDepth];
                        int ByteOfSkip = byteArray.Stride - byteArray.Width * ByteDepth;
                        unsafe
                        {
                                byte* imgPtr = (byte*)(byteArray.Scan0);
                                for (int y = 0; y < byteArray.Height; y++)
                                {
                                        for (int x = 0; x < byteArray.Width; x++)
                                        {
                                                for (int c = 0; c < ByteDepth; c++)
                                                {
                                                        ImgData[(x + byteArray.Height * y) * ByteDepth + c] = (int)*(imgPtr);
                                                        imgPtr += (int)(byteArray.Stride / myBitmap.Width) / ByteDepth;
                                                }
                                        }
                                        imgPtr += ByteOfSkip;
                                }
                        }
                        myBitmap.UnlockBits(byteArray);
                        return ImgData;
                }

                private static Bitmap dyn_array2bmp(int[] ImgData, int ByteDepth, PixelFormat pixelFormat, ColorPalette palette)//灰階
                {
                        int Width = (int)Math.Sqrt(ImgData.GetLength(0) / ByteDepth);
                        int Height = (int)Math.Sqrt(ImgData.GetLength(0) / ByteDepth);
                        Bitmap myBitmap = new Bitmap(Width, Height, pixelFormat);
                        BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                                       ImageLockMode.WriteOnly,
                                                       pixelFormat);
                        try
                        {
                                myBitmap.Palette = palette;
                        }
                        catch
                        {

                        }
                        //Padding bytes的長度
                        int ByteOfSkip = byteArray.Stride - myBitmap.Width * ByteDepth;
                        unsafe
                        {                                   // 指標取出影像資料
                                byte* imgPtr = (byte*)byteArray.Scan0;
                                for (int y = 0; y < Height; y++)
                                {
                                        for (int x = 0; x < Width; x++)
                                        {
                                                for (int c = 0; c < ByteDepth; c++)
                                                {
                                                        *imgPtr = (byte)ImgData[(x + Height * y) * ByteDepth + c];       //B
                                                        imgPtr += 1;
                                                }
                                        }
                                        imgPtr += ByteOfSkip; // 跳過Padding bytes
                                }
                        }
                        myBitmap.UnlockBits(byteArray);
                        return myBitmap;
                }

        private void 灰階ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = dyn_bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    g = new int[w * h * ByteDepth];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            //encode_gray(f0, w, h, g0,ByteDepth);
                            for (int i = 0; i < h; i++)
                                for (int j = 0; j < w; j++)
                                    //g0[i][j] = 255 - f0[i][j];
                                    g0[i * w + j] = 255 - f0[i * w + j];


                        }
                    }
                    NpBitmap = dyn_array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        [DllImport("DIP_proc.dll")]
        public static extern void ProcessInvert(IntPtr input, IntPtr output, int length);

        private void 負片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = dyn_bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    g = new int[w * h * ByteDepth];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int totalLength = w * h * ByteDepth;

                            ProcessInvert((IntPtr)f0, (IntPtr)g0, totalLength);
                        }
                    }
                    NpBitmap = dyn_array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();

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

        }
}
