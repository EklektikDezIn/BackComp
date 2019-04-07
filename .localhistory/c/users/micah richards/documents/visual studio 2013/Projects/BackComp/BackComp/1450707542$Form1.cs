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

        private void scan()
        {


            mypic = new Bitmap(pathname);
            int imwid = mypic.Width;
            int imhei = mypic.Height;
            int total = imwid * imhei;

            for (int z = 0; z < imhei; z++)
            {
                for (int i = 0; i < imwid; i++)
                {
                    Color pixelColor = mypic.GetPixel(i, z);

                    textBox2.AppendText("  " + pixelColor.R +
                        "     " + pixelColor.G +
                        "     " + pixelColor.B + "     " +
                        pixelColor.A +
                        Environment.NewLine);

                }
            }
        }
    }
}
