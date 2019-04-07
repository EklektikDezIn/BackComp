using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackComp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Boolean[,] BW;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            String file = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(file);
            pictureBox2.Image = null;
            pictureBox2.BackColor = Color.White;
            scan(file);
            pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
        }

        private void pictureBox2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            //g.DrawString("This is a diagonal line drawn on the control",
              //  new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            Boolean[,] detail = simplify();
            for (int i = 0; i < BW.GetLength(0);i++)
            {
                for (int j = 0; j < BW.GetLength(1); j++)
                {
                    if (BW[i, j])
                    {
                        g.DrawLine(System.Drawing.Pens.Black, i, j, i + 1, j + 1);
                    }
                }
            }
        }

        private Boolean[,] simplify()
        {
            Boolean[,] detail = new Boolean[(int) numericUpDown1.Value,(int) numericUpDown2.Value];
            for (int i = 0; i < detail.GetLength(0); i++)
            {
                for (int j = 0; j < detail.GetLength(1); j++)
                {
                    detail[i, j] = BW[i * detail.GetLength(0) / 2, j * detail.GetLength(1) / 2];
                }
            }
            return detail;
        }
        private void scan(String file)
        {

            //http://stackoverflow.com/questions/19586524/get-all-pixel-information-of-an-image-efficiently
            Bitmap mypic = new Bitmap(file);
            int imwid = mypic.Width;
            int imhei = mypic.Height;
            int total = imwid * imhei;
           BW = new Boolean[imwid,imhei];
            for (int z = 0; z < imhei; z++)
            {
                for (int i = 0; i < imwid; i++)
                {
                    Color pixelColor = mypic.GetPixel(i, z);
                    int value = (pixelColor.R+pixelColor.G+pixelColor.B)/3;
                    BW[i,z] = (value<128);
                }
                Console.Out.WriteLine("Line " + z + " out of " + imhei);
            }
        }

    }
}
