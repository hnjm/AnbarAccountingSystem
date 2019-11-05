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

namespace Application_Anbar
{
    public partial class frmTotalPriceOrders : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public frmTotalPriceOrders()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_search_Click(sender, e);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                strsearch = "";
                //OrderID 1,2,3
                if (textBox1.Text != "0" && textBox1.Text != "")
                    strsearch += " and OrderID=" + Convert.ToInt32(textBox1.Text) + "";

                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "0" && textBox3.Text != "")
                        strsearch += " and OrderID between " + Convert.ToInt32(textBox2.Text) + " And " + Convert.ToInt32(textBox3.Text) + "";
                    else if (textBox3.Text == "")
                        strsearch += " and OrderID>=" + Convert.ToInt32(textBox2.Text) + "";
                }
                //Date
                if (faDatePicker1.Text != "[Empty Value]" && faDatePicker1.Text != "")
                {
                    if (faDatePicker2.Text != "[Empty Value]" && faDatePicker2.Text != "")
                        strsearch += " and Date between '" + faDatePicker1.Text + "' And '" + faDatePicker2.Text + "'";
                    else
                        strsearch += " and Date>='" + faDatePicker1.Text + "'";
                }
                //CustomerName 4,5,6,7
                if (textBox4.Text != "")
                    strsearch += " and CustomerName='" + textBox4.Text + "'";
                if (textBox5.Text != "")
                    strsearch += " and CustomerName like '%" + textBox5.Text + "%'";
                if (textBox6.Text != "")
                    strsearch += " and CustomerName like '" + textBox6.Text + "%'";
                if (textBox7.Text != "")
                    strsearch += " and CustomerName like '%" + textBox7.Text + "'";
                //Total 8,9
                if (textBox8.Text != "")
                {
                    if (textBox9.Text != "0" && textBox9.Text != "")
                        strsearch += " and Total between " + Convert.ToInt32(textBox8.Text) + " And " + Convert.ToInt32(textBox9.Text) + "";
                    else if (textBox9.Text == "")
                        strsearch += " and Total>=" + Convert.ToInt32(textBox8.Text) + "";
                }
                //
                if (strsearch != "")
                {
                    strsearch = strsearch.Substring(4);
                    strsearch = " Where " + strsearch;
                }
                //End Of Where
                if (objDataSet.Tables["TableTotalPriceOrders"] != null) objDataSet.Tables["TableTotalPriceOrders"].Clear();
                objDataAdapter.SelectCommand.CommandText = "select * from View_TotalPriceOrders" + strsearch;
                objDataAdapter.Fill(objDataSet, "TableTotalPriceOrders");
                //
                dataGridView1.DataSource = objDataSet.Tables["TableTotalPriceOrders"];

                //Begin style
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke;
                dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
                //HeaderText
                dataGridView1.Columns[0].HeaderCell.Value = "شماره فاكتور";
                dataGridView1.Columns[1].HeaderCell.Value = "تاريخ صدور";
                dataGridView1.Columns[2].HeaderCell.Value = "نام مشتري";
                dataGridView1.Columns[3].HeaderCell.Value = "مبلغ كل فاكتور";
                //The End
            }
            catch
            {
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox3.Focus();
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void frmTotalPriceOrders_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            //Current Data
            PersianCalendar pcalender = new PersianCalendar();
            string year, month, day;
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            toolStripStatusLabel4.Text = year + "/" + month + "/" + day;
            //
            FarsiLibrary.Win.Controls.FAMonthView fa = new FarsiLibrary.Win.Controls.FAMonthView();
            fa.DefaultCalendar = fa.PersianCalendar;
            fa.DefaultCulture = fa.PersianCulture;
            //
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 8;
            if (objDataSet.Tables["TableTotalPriceOrders"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableTotalPriceOrders"];
            }
            else
            {
                Program.TableQuery = null;
            }
            FrmPrint frm = new FrmPrint();
            frm.Show();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTotalPriceOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender, e);
        }
    }
}
