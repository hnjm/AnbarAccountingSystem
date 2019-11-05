using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class searchproduct : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rdr;
        AnbarDataContext anbar = new AnbarDataContext();
        public searchproduct()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select productid from products where productname='" + Convert.ToString(textBox2.Text) + "'";
                cmd.Connection = con;
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    Program.mysearchproduct = Convert.ToInt32(rdr[0]);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(".رکوردی با این مشخصه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show(".عملیات جستجو موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar !=13) e.Handled = !char.IsNumber(e.KeyChar);
            if(e.KeyChar==13) btn1_Click(sender,e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) btn2_Click(sender,e);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select productid from products where productid=" + Convert.ToInt32(textBox1.Text) + "";
                    cmd.Connection = con;
                    con.Open();
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        Program.mysearchproduct = Convert.ToInt32(rdr[0]);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی با این مشخصه وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show(".متد جستجو را انتخاب کنید", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".عملیات جستجو موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
