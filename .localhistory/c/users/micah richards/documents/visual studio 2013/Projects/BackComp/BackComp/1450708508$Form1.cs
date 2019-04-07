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
            //openFileDialog1.ShowDialog();
            //String file = openFileDialog1.FileName;
            //pictureBox1.Image = Image.FromFile(file);
              // Dock the PictureBox to the form and set its background to white.
            //pictureBox1.Dock = DockStyle.Fill;
            pictureBox2.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);

            // Add the PictureBox control to the Form.
            this.Controls.Add(pictureBox1);
        
        }
        private void pictureBox2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
                pictureBox1.Right, pictureBox1.Bottom);
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
