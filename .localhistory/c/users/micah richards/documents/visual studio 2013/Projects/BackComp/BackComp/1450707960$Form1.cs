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

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            String file = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(file);
        }

        private void scan(String file, System.Windows.Forms.PaintEventArgs e)
        {

            //http://stackoverflow.com/questions/19586524/get-all-pixel-information-of-an-image-efficiently
            Bitmap mypic = new Bitmap(file);
            int imwid = mypic.Width;
            int imhei = mypic.Height;
            int total = imwid * imhei;
            Boolean[][] BW = new Boolean[imwid][];
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            for (int z = 0; z < imhei; z++)
            {
                for (int i = 0; i < imwid; i++)
                {
                    Color pixelColor = mypic.GetPixel(i, z);
                    int value = (pixelColor.R+pixelColor.G+pixelColor.B)/3;
                    BW[i][z] = value<128;
                    e.Graphics.DrawLine(pen, 20, 10, 300, 100);
                }
            }
        }
    }
}
