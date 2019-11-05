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
    public partial class frmDeleteOrders : Form
    {
        public frmDeleteOrders()
        {
            InitializeComponent();
        }

        private void frmDeleteOrders_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
                    SqlCommand objCommand = new SqlCommand();
                    SqlDataReader DataReader;
                    bool Found = false;
                    objCommand.Connection = objCon;
                    objCommand.CommandText = "select * from Orders where OrderID=" + Convert.ToInt64(textBox1.Text) + "";
                    objCon.Open();
                    DataReader = objCommand.ExecuteReader();
                    if (DataReader.Read())
                    {
                        Found = true;
                    }
                    objCommand.Dispose();
                    DataReader.Close();
                    if (Found == true)
                    {
                        objCommand.CommandText = "Delete From OrderDetails Where OrderID=" + Convert.ToInt64(textBox1.Text) + "";
                        objCommand.ExecuteNonQuery();
                        objCommand.Dispose();
                        objCommand.CommandText = "Delete From Orders Where OrderId=" + Convert.ToInt64(textBox1.Text) + "";
                        objCommand.ExecuteNonQuery();
                        for (int i = progressBar1.Value; i < 100; i++) progressBar1.Value += 1;

                        MessageBox.Show("عمليات حذف سفارش شماره " + textBox1.Text + " با موفقييت انجام شد");
                    }
                    else
                    {
                        MessageBox.Show("سفارشي با شماره مورد نظر در سيستم موجود نمي باشد");
                    }
                    objCon.Close();
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
        }

    }
}
