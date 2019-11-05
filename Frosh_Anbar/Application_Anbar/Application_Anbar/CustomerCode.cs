using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class CustomerCode : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter("",con);
        //SqlDataReader rdr;
        DataSet ds = new DataSet();

        public CustomerCode()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
        }
        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }
        private void CustomerCode_Load(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            //Date current
            string year, month, day;
            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            toolStripStatusLabel6.Text = year + "/" + month + "/" + day;
            //Disable Button
            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
            if (ds.Tables["customer"] == null)
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "viewcustomer";
                da.Fill(ds, "customer");
            }
            dataGridView1.DataSource = ds.Tables["customer"];
            clear();
            if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
            //DataGrid Style
            DataGridViewCellStyle objellStyle = new DataGridViewCellStyle();
            objellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objellStyle;
        }

        private void CustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btn_new.Enabled==true) btn_new_Click(sender, e);
            if (e.KeyCode == Keys.F5 && btn_save.Enabled == true) btn_save_Click(sender, e);
            if (e.KeyCode == Keys.Escape && btn_cancel.Enabled == true) btn_cancel_Click(sender, e);
            if (e.KeyCode == Keys.F6 && btn_delete.Enabled == true) btn_delete_Click(sender, e);
            if (e.KeyCode == Keys.F8 && btn_edit.Enabled == true) btn_edit_Click(sender, e);
            if (e.KeyCode == Keys.F7 && btn_search.Enabled == true) btn_search_Click(sender, e); 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
            btn_edit.Enabled = false;
            btn_search.Enabled = false;
            btn_delete.Enabled = false;
            clear();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            dataGridView1.Enabled = false;
            //categoryID
            if (ds.Tables["customerid"] == null)
            {
                var query = from table in anbar.Customers
                            select table.CustomerID;
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = query.ToString();
                da.Fill(ds, "customerid");
            }
            int i = ds.Tables["customerid"].Rows.Count;
            if (i == 0)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = Convert.ToString(Convert.ToInt32(ds.Tables["customerid"].Rows[i - 1][0]) + 1);
            }
        }

        public bool checkcustomername()
        {
            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == textBox2.Text)
                {
                    return true;
                }
                i++;
            }
            return false;
        }

        public void insertview()
        {
            AnbarDataContext anbar = new AnbarDataContext();
            //fill table productID
            if (ds.Tables["customerid"] != null) ds.Tables["customerid"].Clear();
            var query = from table in anbar.Customers
                        select table.CustomerID;
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = query.ToString();
            da.Fill(ds, "customerid");
            //Fill Table Persentation DataGridView
            if (ds.Tables["customer"] != null) ds.Tables["customer"].Clear();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandText = "viewcustomer";
            da.Fill(ds, "customer");
            dataGridView1.DataSource = ds.Tables["customer"];
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (checkcustomername() == false)
                {
                    Customer tcustomer = new Customer();
                    tcustomer.CustomerID = Convert.ToInt32(textBox1.Text);
                    tcustomer.CustomerName = textBox2.Text;
                    tcustomer.CompanyName = textBox3.Text;
                    tcustomer.City = textBox4.Text;
                    tcustomer.PostalCode = textBox5.Text;
                    tcustomer.Phonecompany = textBox6.Text;
                    tcustomer.phone = textBox7.Text;
                    tcustomer.Fax = textBox8.Text;
                    tcustomer.Address = textBox9.Text;
                    anbar.Customers.InsertOnSubmit(tcustomer);
                    anbar.SubmitChanges();
                    insertview();
                    clear();
                    btn_save.Enabled = false;
                    btn_cancel.Enabled = false;
                    btn_delete.Enabled = true;
                    btn_search.Enabled = true;
                    btn_edit.Enabled = true;
                    dataGridView1.Enabled = true;
                    if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(".این مشتری قبلا به سیستم معرفی شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".عملیات درج موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            btn_edit.Enabled = true;
            btn_search.Enabled = false;
            btn_delete.Enabled = true;
            clear();
            dataGridView1.Enabled = true;
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    DialogResult result1 = MessageBox.Show("آیا قصد حذف رکورد انتخاب شده را دارید ؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result1 == DialogResult.Yes)
                    {
                        var query = (from table in anbar.Customers
                                     where table.CustomerID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                                     select table).Single();
                        anbar.Customers.DeleteOnSubmit(query);
                        anbar.SubmitChanges();
                        insertview();
                        clear();
                        if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                    }
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

        private void btn_edit_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    int i = 0;
                    bool flag = false;
                l1: while (i < dataGridView1.Rows.Count)
                    {
                        if (i == dataGridView1.SelectedRows[0].Index) { i++; goto l1; }
                        if (textBox2.Text == dataGridView1.Rows[i].Cells[1].Value.ToString()) { flag = true; goto l2; }
                        i++;
                    }
                l2: if (flag == false)
                    {
                        var query = (from table in anbar.Customers
                                     where table.CustomerID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                                     select table).Single();
                        query.CustomerID = Convert.ToInt32(textBox1.Text);
                        query.CustomerName = textBox2.Text;
                        query.CompanyName = textBox3.Text;
                        query.City = textBox4.Text;
                        query.PostalCode = textBox5.Text;
                        query.Phonecompany = textBox6.Text;
                        query.phone = textBox7.Text;
                        query.Fax = textBox8.Text;
                        query.Address = textBox9.Text;
                        anbar.SubmitChanges();
                        insertview();
                        clear();
                        if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                    }
                    else
                    {
                        MessageBox.Show(".این مشتری قبلا به سیستم معرفی شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            searchcustomer frmsearchcustomer = new searchcustomer();
            frmsearchcustomer.ShowDialog();
        }

        private void CustomerCode_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchcustomer != 0)
            {
                int i = 0;
                while (i < dataGridView1.Rows.Count)
                {
                    if (Program.mysearchcustomer == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        dataGridView1.BindingContext[dataGridView1.DataSource].Position = i;
                        if (i == 0) dataGridView1.Rows[0].Selected = true;
                        goto l1;
                    }
                    i++;
                }
            l1:
                Program.mysearchcustomer = 0;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 45) e.Handled = !char.IsNumber(e.KeyChar);
        }

    }
}