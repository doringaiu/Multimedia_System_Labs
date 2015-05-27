using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace MS_LAB1
{
    public partial class Form1 : Form
    {
        string imagePath;
        Image originalImage;
        Bitmap bmpOriginalImage;
        byte pxValueMax = 255;
        double numConstant = 0.5, pxValueMaxD = 255.0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            openImagePicker();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            MessageBox.Show("LAB1-MS", "About", MessageBoxButtons.OK);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (bmpOriginalImage != null)
            {
                pictureBox1.Image = (Image)setBrightness(bmpOriginalImage, trackBar1.Value);
                originalImage = pictureBox1.Image;
                GC.Collect();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if(bmpOriginalImage != null) 
            {
                pictureBox1.Image = (Image)contrast(bmpOriginalImage, trackBar2.Value);
                originalImage = pictureBox1.Image;
                GC.Collect();
            }      
        }

        private Bitmap contrast(Bitmap sourceBitmap, int threshold)
        {
           
            double blue = 0, green = 0, red = 0;

            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);


            for(int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                    blue = ((((pixelBuffer[k] / pxValueMaxD) - numConstant) * contrastLevel) + numConstant) * pxValueMaxD;
                    green = ((((pixelBuffer[k + 1] / pxValueMaxD) - numConstant) * contrastLevel) + numConstant) * pxValueMaxD;
                    red = ((((pixelBuffer[k + 2] / pxValueMaxD) - numConstant) * contrastLevel) + numConstant) * pxValueMaxD;

                    if (blue > pxValueMax)
                    { blue = pxValueMax; }
                    else if (blue < 0)
                    { blue = 0; }

                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }

                    if (red > 255)
                    { red = pxValueMax; }
                    else if (red < 0)
                    { red = 0; }

                    pixelBuffer[k] = (byte)blue;
                    pixelBuffer[k + 1] = (byte)green;
                    pixelBuffer[k + 2] = (byte)red;    
            };
          
            Bitmap adjustedBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            BitmapData adjustData = adjustedBitmap.LockBits(new Rectangle(0, 0, adjustedBitmap.Width,
                adjustedBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, adjustData.Scan0, pixelBuffer.Length);
            adjustedBitmap.UnlockBits(adjustData);
               
            return adjustedBitmap;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(imagePath.Length > 2)
            {
                System.IO.FileStream fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                this.originalImage.Save(fs, ImageFormat.Jpeg);
                fs.Close();
            }

            GC.Collect();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save Image As";
            saveFileDialog1.Filter = "jpg files (*.jpg*)|*.jpg|bmp files (*.bmp)|*.bmp|png files (*.png)|*.png";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.originalImage.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.originalImage.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.originalImage.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();

            }
            GC.Collect();
        }

        public Bitmap setBrightness(Bitmap sourceBitmap, int brightness)
        {
            double blue = 0, green = 0, red = 0;

            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);
          
            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = pixelBuffer[k] + brightness;
                green = pixelBuffer[k + 1] + brightness;
                red = pixelBuffer[k + 2] + brightness;

                if (blue > pxValueMax)
                { blue = pxValueMax; }
                else if (blue < 0)
                { blue = 0; } 

                if (green > pxValueMax)
                { green = pxValueMax; }
                else if (green < 0)
                { green = 0; }      

                if (red > pxValueMax)
                { red = pxValueMax; }
                else if (red < 0)
                { red = 0; }

                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }
            
            Bitmap adjustedBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            BitmapData adjustData = adjustedBitmap.LockBits(new Rectangle(0, 0, adjustedBitmap.Width,
                adjustedBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, adjustData.Scan0, pixelBuffer.Length);
            adjustedBitmap.UnlockBits(adjustData);

            return adjustedBitmap;
        }

        private void openImagePicker()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "jpg files (*.jpg*)|*.jpg|bmp files (*.bmp)|*.bmp|png files (*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                originalImage = Image.FromFile(dlg.FileName);
                imagePath = dlg.FileName;
                pictureBox1.Image = originalImage;
                bmpOriginalImage = new Bitmap(originalImage);
            }
            dlg.Dispose();
        }

    }
}
