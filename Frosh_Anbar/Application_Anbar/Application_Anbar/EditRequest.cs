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
    public partial class EditRequest : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter("", con);
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand("", con);

        public EditRequest()
        {
            InitializeComponent();
        }
      
        private void EditRequest_Load(object sender, EventArgs e)
        {
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = "select * from viewrequestproduct";
            da.Fill(ds, "request");
            Program.tablequery = ds.Tables["request"];
            dataGridView1.DataSource = Program.tablequery;
            dataGridView1.Columns[0].HeaderText = "کد کالا";
            dataGridView1.Columns[1].HeaderText = "نام کالا";
            dataGridView1.Columns[2].HeaderText = "تاریخ درخواست";
            dataGridView1.Columns[3].HeaderText = "تاریخ احتیاج";
            dataGridView1.Columns[4].HeaderText = "تعداد";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            var query = from table in anbar.Requests
                        where table.NeedDate == Convert.ToString(faDatePicker3.Text)
                        select table;
            dataGridView1.DataSource = query;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //var query = from tableview in anbar.ViewRequestProducts
            //            where tableview.ProductID == "@p0"
            //            select tableview;
            try
            {
                if (textBox1.Text != "")
                {
                    if (ds.Tables["request"] != null) ds.Tables["request"].Clear();

                    da.SelectCommand.CommandType = CommandType.Text;
                    da.SelectCommand.CommandText = "select * from viewrequestproduct where productid=" + Convert.ToInt32(textBox1.Text) + "";
                    da.Fill(ds.Tables["request"]);
                    dataGridView1.DataSource = ds.Tables["request"];
                    tabControl1.SelectTab(1);
                }
                else
                {
                    MessageBox.Show(".متد جستجو را انتخاب کنید", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            { }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
                if (e.KeyChar == 13) button6_Click(sender, e);
            }
            catch { }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) button7_Click(sender, e);
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //var query = from tableview in anbar.ViewRequestProducts
            //            where tableview.ProductName == Convert.ToString(textBox2.Text)
            //            select tableview;
            try
            {
                if (ds.Tables["request"] != null) ds.Tables["request"].Clear();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select * from viewrequestproduct where productname='" + Convert.ToString(textBox2.Text) + "'";
                da.Fill(ds.Tables["request"]);

                //Program.tablequery = ds.Tables["request"];
                dataGridView1.DataSource = ds.Tables["request"];
                tabControl1.SelectTab(1);
            }
            catch
            { }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //var query = from tableview in anbar.ViewRequestProducts
            //            where tableview.RequestDate == Convert.ToString(faDatePicker3.Text)
            //            select tableview;
            try
            {
                if (ds.Tables["request"] != null) ds.Tables["request"].Clear();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select * from viewrequestproduct where requestdate='" + Convert.ToString(faDatePicker3.Text) + "'";
                da.Fill(ds.Tables["request"]);

                //Program.tablequery = ds.Tables["request"];
                dataGridView1.DataSource = ds.Tables["request"];
                tabControl1.SelectTab(1);
            }
            catch
            { }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //var query = from tableview in anbar.ViewRequestProducts
            //            where tableview.NeedDate == Convert.ToString(faDatePicker4.Text)
            //            select tableview;
            try
            {
                if (ds.Tables["request"] != null) ds.Tables["request"].Clear();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select * from viewrequestproduct where needdate='" + Convert.ToString(faDatePicker4.Text) + "'";
                da.Fill(ds.Tables["request"]);

                //Program.tablequery = ds.Tables["request"];
                dataGridView1.DataSource = ds.Tables["request"];
                tabControl1.SelectTab(1);
            }
            catch
            { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Program.tablequery =(DataTable)dataGridView1.DataSource;
            PrintRequest frmprintrequest = new PrintRequest();
            frmprintrequest.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                txtcode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtteadad.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                faDatePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                faDatePicker2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }

        }
        private void clear()
        {
            txtcode.Clear();
            txtteadad.Clear();
            faDatePicker1.Text = "";
            faDatePicker2.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    AnbarDataContext anbar = new AnbarDataContext();
                    var query = (from table in anbar.Requests
                                 where table.ProductID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value) && table.RequestDate == dataGridView1.SelectedRows[0].Cells[2].Value.ToString()
                                 select table).Single();
                    query.ProductID = Convert.ToInt32(txtcode.Text);
                    query.teadad = Convert.ToInt32(txtteadad.Text);
                    query.RequestDate = faDatePicker1.Text;
                    query.NeedDate = faDatePicker2.Text;
                    anbar.SubmitChanges();
                    clear();
                    if (ds.Tables["request"] != null) ds.Tables["request"].Clear();
                    da.Fill(ds, "request");
                    dataGridView1.DataSource = ds.Tables["request"];
                }
                else
                {
                    MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch
            {
                MessageBox.Show(".عملیات ویرایش موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void delete(int id,string ReqDate)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //var query = (from table in anbar.Requests
            //             where table.ProductID == id
            //             && table.RequestDate == ReqDate
            //             select table).Single();
            //anbar.Requests.DeleteOnSubmit(query);
            //anbar.SubmitChanges();
            cmd.CommandText = "delete from Requests where productid=" + id + " and requestdate='"+ReqDate+"'";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                while (i < dataGridView1.Rows.Count)
                {
                    delete(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value), Convert.ToString(dataGridView1.Rows[i].Cells[2].Value));
                    i++;
                }
                clear();
                if (ds.Tables["request"] != null) ds.Tables["request"].Clear();
                da.Fill(ds, "request");
                dataGridView1.DataSource = ds.Tables["request"];
            }
            catch
            {
                MessageBox.Show(".عملیات حذف موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    delete(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value), Convert.ToString(dataGridView1.SelectedRows[0].Cells[2].Value));
                    clear();
                    if (ds.Tables["request"] != null) ds.Tables["request"].Clear();
                    da.Fill(ds, "request");
                    dataGridView1.DataSource = ds.Tables["request"];
                }
                else
                {
                    MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch
            {
                MessageBox.Show(".عملیات حذف موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtteadad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8) e.Handled = !char.IsNumber(e.KeyChar);
        }

    }
}
