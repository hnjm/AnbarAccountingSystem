using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Linq;

namespace Application_Anbar
{
    public partial class Gorfe : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter("",con);
        DataSet ds = new DataSet();
        public Gorfe()
        {
            InitializeComponent();
        }

        private void Gorfe_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            AnbarDataContext anbar = new AnbarDataContext();
            //date current
            string year, month, day;
            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            toolStripStatusLabel6.Text = year + "/" + month + "/" + day;

            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            if (ds.Tables["category"] == null)
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "viewcategory";
                da.Fill(ds, "category");
            }
            dataGridView1.DataSource = ds.Tables["category"];
            clear();
            if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
            //CellStyle
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
            btn_edit.Enabled = false;
            btn_search.Enabled = false;
            btn_remove.Enabled = false;
            clear();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            dataGridView1.Enabled = false;
            //categoryID
            if (ds.Tables["categoryid"]==null)
            {
            var query = from table in anbar.Categories
                        select table.CategoryID;
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = query.ToString();
            da.Fill(ds, "categoryid");
            }
            int i = ds.Tables["categoryid"].Rows.Count;
            if (i == 0)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = Convert.ToString(Convert.ToInt32(ds.Tables["categoryid"].Rows[i - 1][0]) + 1);
            }
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        public bool checkgorfeneme()
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
            if (ds.Tables["categoryid"] != null) ds.Tables["categoryid"].Clear();
            var query = from table in anbar.Categories
                        select table.CategoryID;
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = query.ToString();
            da.Fill(ds, "categoryid");
            //Fill Table Persentation DataGridView
            if (ds.Tables["category"] != null) ds.Tables["category"].Clear();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandText = "viewcategory";
            da.Fill(ds, "category");
            dataGridView1.DataSource = ds.Tables["category"];
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (checkgorfeneme() == false)
                {
                    Category tcategory = new Category();
                    tcategory.CategoryID = Convert.ToInt32(textBox1.Text);
                    tcategory.CategoryName = textBox2.Text;
                    tcategory.Description= textBox3.Text;
                    anbar.Categories.InsertOnSubmit(tcategory);
                    anbar.SubmitChanges();
                    insertview();
                    clear();
                    btn_save.Enabled = false;
                    btn_cancel.Enabled = false;
                    btn_remove.Enabled = true;
                    btn_search.Enabled = true;
                    btn_edit.Enabled = true;
                    dataGridView1.Enabled = true;
                    if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show(".این غرفه قبلا به سیستم معرفی شده است", "دقت کنید",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".عملیات درج موفقیت آمیز نیست", "خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_search.Enabled = true;
            dataGridView1.Enabled = true;
            clear();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    DialogResult result1 = MessageBox.Show("آیا قصد حذف رکورد انتخاب شده را دارید ؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result1 == DialogResult.Yes)
                    {
                        var query = (from table in anbar.Categories
                                     where table.CategoryID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                                     select table).Single();
                        anbar.Categories.DeleteOnSubmit(query);
                        anbar.SubmitChanges();
                        insertview();
                        clear();
                        if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
                    }
                }
                else
                {
                    MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(".عملیات حذف موفقیت آمیز نیست", "خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
                textBox1.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[0].Value);
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
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
l1:                 while (i < dataGridView1.Rows.Count)
                    {
                        if (i == dataGridView1.SelectedRows[0].Index) { i++; goto l1; }
                        if (textBox2.Text == dataGridView1.Rows[i].Cells[1].Value.ToString()) { flag = true; goto l2; }
                        i++;
                    }
l2:                 if (flag == false)
                    {
                        var query = (from table in anbar.Categories
                                     where table.CategoryID == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                                     select table).Single();
                        query.CategoryID = Convert.ToInt32(textBox1.Text);
                        query.CategoryName = textBox2.Text;
                        query.Description = textBox3.Text;
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
                MessageBox.Show(".عملیات ویرایش موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            SearchCategory frmsearchcategory = new SearchCategory();
            frmsearchcategory.ShowDialog();
        }

        private void Gorfe_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchcategory != 0)
            {
                int i=0;
                while (i < dataGridView1.Rows.Count)
                {
                    if (Program.mysearchcategory==Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        dataGridView1.BindingContext[dataGridView1.DataSource].Position = i;
                        if (i == 0) dataGridView1.Rows[0].Selected = true;
                        goto l1;
                    }
                    i++;
                }
            l1:
                Program.mysearchcategory = 0;
            }
        }

        private void Gorfe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btn_new.Enabled==true) button1_Click(sender, e);
            if (e.KeyCode == Keys.F5 && btn_save.Enabled == true) btn_save_Click(sender, e);
            if (e.KeyCode == Keys.Escape && btn_cancel.Enabled == true) btn_cancel_Click(sender, e);
            if (e.KeyCode == Keys.F6 && btn_remove.Enabled == true) btn_remove_Click(sender, e);
            if (e.KeyCode == Keys.F8 && btn_edit.Enabled == true) btn_edit_Click(sender, e);
            if (e.KeyCode == Keys.F7 && btn_search.Enabled == true) btn_search_Click(sender, e); 
        }
    }
}