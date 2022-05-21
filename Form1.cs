using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_w_12
{
    public partial class Form1 : Form
    {
        SaveFileDialog save = new SaveFileDialog();
        Bitmap bmp;
        double i = 0.5;

        public double k(int size)
        {
            return (size / Convert.ToDouble(ClientSize.Width));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image files(*.BMP, *.JPG, *.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";

            if(op.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(op.FileName);

                if(image.Width >= ClientSize.Width || image.Height >= 460)
                {
                    i = k(Math.Max(image.Width, image.Height));
                    MessageBox.Show(ClientSize.Width.ToString());
                }
                
                int wid = Convert.ToInt32((image.Width) / (i+0.5));
                int h = Convert.ToInt32((image.Height) / (i+0.5));
                pictureBox1.Width = wid;
                pictureBox1.Height = h;

                bmp = new Bitmap(image, wid, h);
                
                pictureBox1.Location = new Point(((ClientSize.Width - wid)/2), (ClientSize.Height - h - (ClientSize.Height - 460)) /2);
                pictureBox1.Image = bmp;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int i = 0; i < bmp.Width; i++)
                    for (int j = 0; j < bmp.Height; j += 2)
                    {
                        int R = bmp.GetPixel(i, j).R;
                        int G = bmp.GetPixel(i, j).G;
                        int B = bmp.GetPixel(i, j).B;

                        int Gray = (R + G + B) / 3;

                        Color p = Color.FromArgb(255, Gray, Gray,
                        Gray);

                        bmp.SetPixel(i, j, p);
                    }

                Refresh();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();

                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Bitmap File(*.bmp)|*.bmp| GIF File(*.gif)|*.gif| JPEG File(*.jpg)|*.jpg| PNG File(*.png)|*.png";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = savedialog.FileName;
                    string strFilExtn = fileName.Remove(0, fileName.Length - 3);

                    switch (strFilExtn)
                    {
                        case "bmp":
                            bmp.Save(fileName,
                            System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case "jpg":
                            bmp.Save(fileName,
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case "gif":
                            bmp.Save(fileName,
                            System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case "tif":
                            bmp.Save(fileName,
                            System.Drawing.Imaging.ImageFormat.Tiff);
                            break;
                        case "png":
                            bmp.Save(fileName,
                            System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        default:
                            break;

                    }
                }
            }
        }
    }
}
