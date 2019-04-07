﻿using System;
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
        double MAXHEI;
        double MAXWID;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            file = openFileDialog1.FileName;

            pictureBox1.Image = Image.FromFile(file);
            trackBar1.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            Draw();
        }
        private void Draw()
        {
            MAXHEI = (double)numericUpDown1.Value;
            MAXWID = MAXHEI;
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
            for (int i = 0; i < BW.GetLength(0); i++)
            {
                for (int j = 0; j < BW.GetLength(1); j++)
                {

                    if (BW[i, j])
                    {
                        if (j % 2 == 0)
                        {
                            g.DrawLine(System.Drawing.Pens.Black, i, j, i + 1, j + 1);
                        }
                        else
                        {
                            g.DrawLine(System.Drawing.Pens.Black, i, j, i - 1, j - 1);
                        }
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
            int x = 0, y = 0;
            BW = new Boolean[(int)MAXWID, (int)MAXHEI];
            for (double i = 0; i < imwid; i += imwid / MAXWID)
            {
                y = 0;
                for (double j = 0; j < imhei; j += imhei / MAXHEI)
                {
                    Color pixelColor = mypic.GetPixel((int)i, (int)j);
                    int value = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    if (x < MAXWID && y < MAXHEI)
                    {
                        BW[x, y] = (value < trackBar1.Value);
                    }
                    y++;
                }


                x++; y = (int)MAXHEI - 1; i += imwid / MAXWID;
                try
                {
                    for (double j = imhei-1; j > 0; j -= imhei / MAXHEI)
                    {
                        Color pixelColor = Color.Black;
                        try
                        {
                            pixelColor = mypic.GetPixel((int)i, (int)j);
                        }
                        catch (Exception e) { System.Console.WriteLine("try 2 failed" + e); };
                            int value = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                            if (x < MAXWID && y < MAXHEI)
                            {
                                BW[x, y] = (value < trackBar1.Value);
                            }
                            y--;

                        
                    }

                }
                catch (Exception e) { System.Console.WriteLine("try 2 failed"); };
                x++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String temp = "G28\r\nG90\r\nG92\r\n";
            double distance = 0;
            double distance2 = 0;
            Boolean pos = true; //down false = up
            for (int i = 0; i < BW.GetLength(0); i++)
            {
                for (int j = 0; j < BW.GetLength(1); j++)
                {
                    if (pos != BW[i, j] && BW[i, j])
                    {
                        temp += "G1 X" + distance + "\r\nG1 Z0\r\n";
                        pos = true;
                    }
                    else if (pos != BW[i, j] && !BW[i, j])
                    {
                        temp += "G1 X" + distance + " \r\nG1 Z1\r\n";
                        pos = false;
                    }
                    distance += (double)numericUpDown2.Value;
                }
                distance2 += (double)numericUpDown2.Value;
                temp += "G0 Z1 Y" + distance2 + "\r\n";
                pos = false;
            }
            textBox1.Text = temp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();

        }

    }
}