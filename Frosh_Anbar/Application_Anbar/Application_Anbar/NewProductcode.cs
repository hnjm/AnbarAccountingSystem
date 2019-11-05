using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class NewProductCode : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter("", con);
        SqlCommand cmd = new SqlCommand("", con);
        DataSet ds = new DataSet();

        public NewProductCode()
        {
            InitializeComponent();
        }
        
        private void NewProductCode_Load(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
            //Date current
            string year, month, day;
            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            toolStripStatusLabel6.Text =year + "/" + month + "/" + day;
            //view information
            //var query = from tableproduct in anbar.Products
            //            select new { tableproduct.ProductID,tableproduct.Mojodi ,tableproduct.SellPrice };
            if (ds.Tables["products"] == null)
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "viewproduct";
                da.Fill(ds, "products");
            }
            dataGridView1.DataSource = ds.Tables["products"];
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.SelectedRows[0].Selected = false;

            } 
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            //dataGridView1.BindingContext[dataGridView1.DataSource].Position = -1;
            //DataGrid Style
            DataGridViewCellStyle objellStyle = new DataGridViewCellStyle();
            objellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objellStyle;
        }
        private void insertview()
        {
            AnbarDataContext anbar = new AnbarDataContext();
            ds.Tables["products"].Clear();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandText = "viewproduct";
            da.Fill(ds, "products");
            dataGridView1.DataSource = ds.Tables["products"];
            //productid
            var query = from tableproduct in anbar.Products
                        select tableproduct.ProductID;
            if (ds.Tables["productid"]!=null) ds.Tables["productid"].Clear();
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = query.ToString();
            da.Fill(ds, "productid");
        }

        private void clear()
        {
            textBox1.Text="";
            textBox2.Text="";
            textBox3.Text="0";
            textBox5.Text="0";
            textBox6.Text="0";
            textBox7.Text="0";
            textBox8.Text="0";
            comboBox1.Text="";
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            //disable search,delete and exit button durring insert.
            btn_delete.Enabled = false;
            btn_search.Enabled = false;
            btn_edit.Enabled = false;
            btn_cancel.Enabled = true;
            btn_save.Enabled = true;
            //clear textbox 
            clear();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            dataGridView1.Enabled = false;
            //product ID
            var query = from tproductID in anbar.Products
                        select tproductID.ProductID;
            if (ds.Tables["productid"] == null)
            {
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = query.ToString();
                da.Fill(ds, "productid");
            }
            int myandis = ds.Tables["productid"].Rows.Count;
            if (myandis == 0)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = Convert.ToString(Convert.ToInt32(ds.Tables["productid"].Rows[myandis - 1][0]) + 1);
            }
        }
        public bool checkproductname()
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
        private void btn_save_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (checkproductname() == false)
                {
                    Product tproduct = new Product();
                    tproduct.ProductID = Convert.ToInt32(textBox1.Text);
                    tproduct.ProductName = textBox2.Text;
                    tproduct.CategoryID = Convert.ToInt32(textBox3.Text);
                    tproduct.Unit = comboBox1.Text;
                    if (textBox5.Text != "") tproduct.BuyPrice = Convert.ToInt64(textBox5.Text);
                    if (textBox6.Text != "") tproduct.SellPrice = Convert.ToInt64(textBox6.Text);
                    if (textBox7.Text != "") tproduct.Mojodi = Convert.ToInt32(textBox7.Text);
                    if (textBox8.Text != "") tproduct.St_mojodi = Convert.ToInt16(textBox8.Text);
                    tproduct.Discountable = checkBox1.Checked;
                    anbar.Products.InsertOnSubmit(tproduct);
                    anbar.SubmitChanges();
                    insertview();
                    clear();
                    if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                    btn_delete.Enabled = true;
                    btn_search.Enabled = true;
                    btn_edit.Enabled = true;
                    btn_cancel.Enabled = false;
                    btn_save.Enabled = false;
                    dataGridView1.Enabled = true;
                    //dataGridView1.BindingContext[dataGridView1.DataSource].Position = -1;
                }
                else
                {
                    MessageBox.Show(".این کالا قبلا به سیستم معرفی شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".عملیات درج موفقیت آمیز نیست", "خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_delete.Enabled = true;
            btn_search.Enabled = true;
            btn_edit.Enabled = true;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            dataGridView1.Enabled = true;
            clear();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (Convert.ToInt32(dataGridView1.SelectedRows.Count) != 0)
                {
                    DialogResult result;
                    result = MessageBox.Show("آیا قصد حذف رکورد انتخاب شده را دارید ؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        var query = (from tproduct in anbar.Products
                                     where tproduct.ProductID == (int)dataGridView1.SelectedRows[0].Cells[0].Value
                                     select tproduct).Single();
                        anbar.Products.DeleteOnSubmit(query);
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
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox2.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[1].Value);
                textBox3.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[2].Value);
                comboBox1.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[3].Value);
                textBox5.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[4].Value);
                textBox6.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[5].Value);
                textBox7.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[6].Value);
                textBox8.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[7].Value);
                checkBox1.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[8].Value);
            }
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            searchproduct formsearchproduct = new searchproduct();
            formsearchproduct.ShowDialog();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
                try
                {
                    if (dataGridView1.SelectedRows.Count != 0)
                    {
                        int i = 0;
                        bool check = false;
                    l1: while (i < dataGridView1.Rows.Count)
                        {
                            if (i == dataGridView1.SelectedRows[0].Index) { i++; goto l1; }
                            if (dataGridView1.Rows[i].Cells[1].Value.ToString() == textBox2.Text)
                            {
                                check = true;
                                goto l2;
                            }
                            i++;
                        }
                    l2: if (check == false)
                        {
                            var query = (from table in anbar.Products
                                         where table.ProductID == Convert.ToInt32(textBox1.Text)
                                         select table).Single();
                            query.ProductID = Convert.ToInt32(textBox1.Text);
                            query.ProductName = textBox2.Text;
                            query.CategoryID = Convert.ToInt32(textBox3.Text);
                            query.Unit = comboBox1.Text;
                            if (textBox5.Text != "") query.BuyPrice = Convert.ToInt64(textBox5.Text);
                            if (textBox6.Text != "") query.SellPrice = Convert.ToInt64(textBox6.Text);
                            if (textBox7.Text != "") query.Mojodi = Convert.ToInt32(textBox7.Text);
                            if (textBox8.Text != "") query.St_mojodi = Convert.ToInt16(textBox8.Text);
                            query.Discountable = checkBox1.Checked;
                            anbar.SubmitChanges();
                            insertview();
                            clear();
                            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(".این کالا قبلا به سیستم معرفی شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show(".عملیات ویرایش موفقیت آمیز نیست", "خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
        }

        private void NewProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btn_new.Enabled == true) btn_new_Click(sender, e);
            if (e.KeyCode == Keys.F5 && btn_save.Enabled == true) btn_save_Click(sender, e);
            if (e.KeyCode == Keys.Escape && btn_cancel.Enabled == true) btn_cancel_Click(sender, e);
            if (e.KeyCode == Keys.F6 && btn_delete.Enabled == true) btn_delete_Click(sender, e);
            if (e.KeyCode == Keys.F8 && btn_edit.Enabled == true) btn_edit_Click(sender, e);
            if (e.KeyCode == Keys.F7 && btn_search.Enabled == true) btn_search_Click(sender, e); 

        }

        private void NewProductCode_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchproduct != 0)
            {
                int i = 0;
                while (i < dataGridView1.Rows.Count)
                {
                    if (Program.mysearchproduct == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        dataGridView1.BindingContext[dataGridView1.DataSource].Position = i;
                        if (i == 0) dataGridView1.Rows[0].Selected = true;
                        goto l1;
                    }
                    i++;
                }
            l1:
                Program.mysearchproduct = 0;
            }
            if (Program.mysearchcategory != 0)
            {
                textBox3.Text = Program.mysearchcategory.ToString();
                Program.mysearchcategory = 0;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmviewCategory frm = new frmviewCategory();
            frm.ShowDialog();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button8_Click(sender, e);
        }
    }
}