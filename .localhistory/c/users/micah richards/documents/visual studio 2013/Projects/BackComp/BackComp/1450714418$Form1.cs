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
        String file;
        int MAXHEI = 300;
        int MAXWID = 300;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            file = openFileDialog1.FileName;
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

            Bitmap mypic = new Bitmap(file);
            int imwid = mypic.Width;
            int imhei = mypic.Height;
            int total = imwid * imhei;
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
        private void scan(String file)
        {

            //http://stackoverflow.com/questions/19586524/get-all-pixel-information-of-an-image-efficiently
            Bitmap mypic = new Bitmap(file);
            int imwid = mypic.Width;
            int imhei = mypic.Height;
            int total = imwid * imhei;
            BW = new Boolean[MAXHEI,MAXWID];
            for (double z = 0; z < imhei; z+=imhei/MAXHEI)
            {
                for (double i = 0; i < imwid; i+=imwid/MAXWID)
                {
                   
                    Color pixelColor = mypic.GetPixel((int)i, (int)z);
                    int value = (pixelColor.R+pixelColor.G+pixelColor.B)/3;
                    if (i < MAXWID && z < MAXHEI)
                    {
                        BW[(int)i, (int)z] = (value < 128);
                    }
                }
            }
        }

    }
}
