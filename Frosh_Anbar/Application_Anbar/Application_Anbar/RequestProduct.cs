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
    public partial class RequestProduct : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter("", con);
        DataSet ds = new DataSet();
        FarsiLibrary.Win.Controls.FAMonthView famv = new FarsiLibrary.Win.Controls.FAMonthView();

        public RequestProduct()
        {
            InitializeComponent();
        }

        private void clear()
        {
            textBox1.Clear();
            textBox3.Clear();
            famv.DefaultCalendar = famv.PersianCalendar;
            famv.DefaultCulture = famv.PersianCulture;
            string year, month, day;
            year = famv.Year.ToString(); month = famv.Month.ToString();day=famv.Day.ToString();
            if(month.Length==1) month="0"+month;
            if(day.Length==1) day="0"+day;
            faDatePicker1.Text =  year+ "/" +month  + "/" +day ;
            //faDatePicker2.Text = faDatePicker1.Text;
        }

        private void RequestProduct_Load(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            string year, month, day;
            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            toolStripStatusLabel6.Text = year + "/" + month + "/" + day;
            //for month view
            FillGridView();
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            clear();
            if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
            dataGridView1.Columns[0].HeaderText = "کد کالا";
            dataGridView1.Columns[1].HeaderText = "نام کالا";
            dataGridView1.Columns[2].HeaderText = "تاریخ درخواست";
            dataGridView1.Columns[3].HeaderText = "تاریخ احتیاج";
            dataGridView1.Columns[4].HeaderText = "تعداد";
        }

        private void FillGridView()
        {
            if (ds.Tables["request"] != null) ds.Tables["request"].Clear();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandText = "viewrequest";
            da.Fill(ds, "request");
            //Program.tablequery = ds.Tables["request"];
            dataGridView1.DataSource = ds.Tables["request"];//ds.Tables["request"];
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
            if(e.KeyChar==13) btn_nk_Click(sender, e);
        }

        private void btn_nk_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            clear();
            if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
            btn_print.Enabled = false;
            btn_search.Enabled = false;
            dataGridView1.Enabled = false;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            AnbarDataContext anbar = new AnbarDataContext();
            try
            {
                Request trequest = new Request();
                trequest.ProductID =Convert.ToInt32(textBox1.Text);
                trequest.RequestDate = faDatePicker1.Text;
                trequest.NeedDate = faDatePicker2.Text;
                trequest.teadad=Convert.ToInt32(textBox3.Text);
                anbar.Requests.InsertOnSubmit(trequest);
                anbar.SubmitChanges();
                FillGridView();
                clear();
                if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
                btn_search.Enabled = true;
                btn_print.Enabled = true;
                dataGridView1.Enabled = true;
                btn_cancel.Enabled = false;
                btn_save.Enabled = false;
            }
            catch
            {
                MessageBox.Show(".عملیات درج موفقیت آمیز نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            clear();
            if (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
            btn_search.Enabled = true;
            btn_print.Enabled = true;
            dataGridView1.Enabled = true;
            btn_cancel.Enabled = false;
            btn_save.Enabled = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //AnbarDataContext anbar = new AnbarDataContext();
            //try
            //{
            //    if (textBox1.Text != "")
            //    {
            //        if (ds.Tables["productcode"] == null)
            //        {
            //            var query = from table in anbar.Products
            //                        select new { table.ProductID, table.ProductName };
            //            da.SelectCommand.CommandType = CommandType.Text;
            //            da.SelectCommand.CommandText = query.ToString();
            //            da.Fill(ds, "productcode");
            //        }
            //        int i = 0;
            //        while (i < ds.Tables["productcode"].Rows.Count)
            //        {
            //            if (Convert.ToInt32(ds.Tables["productcode"].Rows[i][0]) == Convert.ToInt32(textBox1.Text))
            //            {
            //                textBox2.Text = ds.Tables["productcode"].Rows[i][1].ToString();
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch {}
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //DataRow row1;
            //Program.table1.Columns.Add("ProductID");
            //Program.table1.Columns.Add("Teadad");
            //Program.table1.Columns.Add("productname");

            //row1 = Program.table1.NewRow();
            //row1[0] = "1";
            //row1[1] = "100";
            //row1[2] = "890606";
            //Program.table1.Rows.Add(row1);
            Program.tablequery =(DataTable)dataGridView1.DataSource;
            PrintRequest frmpr = new PrintRequest();
            frmpr.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            EditRequest FrmEditRequest = new EditRequest();
            FrmEditRequest.Show();
        }

        private void RequestProduct_Activated(object sender, EventArgs e)
        {
            FillGridView();
        }
    }
}