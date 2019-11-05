using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using FarsiLibrary.Utils;

namespace Application_Anbar
{
    public partial class frmDisplay : Form
    {
        public frmDisplay()
        {
            InitializeComponent();
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            try
            {
                p1.Image = System.Drawing.Image.FromFile(@"Picture\1.bmp");
                p2.Image = System.Drawing.Image.FromFile(@"Picture\2.jpg");
                p3.Image = System.Drawing.Image.FromFile(@"Picture\3.jpg");
                p4.Image = System.Drawing.Image.FromFile(@"Picture\4.jpg");
                p5.Image = System.Drawing.Image.FromFile(@"Picture\5.jpg");
                p6.Image = System.Drawing.Image.FromFile(@"Picture\6.jpg");
                p7.Image = System.Drawing.Image.FromFile(@"Picture\7.jpg");
                p8.Image = System.Drawing.Image.FromFile(@"Picture\8.jpg");
                pictureBox1.Image = Class1.frmmain.BackgroundImage;
                textBox1.BackColor = Class1.frmmain.BackColor;
            }
            catch
            {
            }
        }

        private void p6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p6.Image;
        }

        private void p5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p5.Image;
        }

        private void p8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p8.Image;
        }

        private void p7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p7.Image;
        }

        private void p1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p1.Image;
        }

        private void p2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p2.Image;
        }

        private void p3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p3.Image;
        }

        private void p4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = p4.Image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Dispose();
                openFileDialog1.Reset();
                openFileDialog1.InitialDirectory = "c:\\";
                //openFileDialog1.Filter = "Image File|*.jpg |*.bmp";
                openFileDialog1.Filter = "image files (*.Jpeg,*.bmp)|*.jpg;*.bmp|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string strname = openFileDialog1.FileName;
                    pictureBox1.Image = System.Drawing.Image.FromFile(strname);
                }
            }
            catch
            {
                MessageBox.Show("Error:Could Not Open This File","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            Class1.frmmain.BackgroundImage = pictureBox1.Image;
            Class1.frmmain.BackColor = textBox1.BackColor;
            if (radioButton1.Checked == true)
                Class1.frmmain.BackgroundImageLayout = ImageLayout.None;
            else if (radioButton2.Checked == true)
                Class1.frmmain.BackgroundImageLayout = ImageLayout.Tile;
            else if (radioButton3.Checked == true)
                Class1.frmmain.BackgroundImageLayout = ImageLayout.Stretch;
            else if (radioButton4.Checked == true)
                Class1.frmmain.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            btn_Apply_Click(sender, e);
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
            }
        }

    }
}
