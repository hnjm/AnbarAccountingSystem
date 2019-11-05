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
    public partial class frmReportOrders : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public frmReportOrders()
        {
            InitializeComponent();
        }

        private void frmReportOrders_Load(object sender, EventArgs e)
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

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                strsearch = "";
                //ProductID
                if (textBox1.Text != "0" && textBox1.Text != "")
                    strsearch += " and ProductID=" + Convert.ToInt32(textBox1.Text) + "";

                if (textBox2.Text != "")
                {
                    if (textBox3.Text != "0" && textBox3.Text != "")
                        strsearch += " and productid between " + Convert.ToInt32(textBox2.Text) + " And " + Convert.ToInt32(textBox3.Text) + "";
                    else if (textBox3.Text == "")
                        strsearch += " and ProductID>=" + Convert.ToInt32(textBox2.Text) + "";
                }
                //ProductName
                if (textBox4.Text != "")
                    strsearch += " and ProductName='" + textBox4.Text + "'";
                if (textBox5.Text != "")
                    strsearch += " and ProductName like '%" + textBox5.Text + "%'";
                if (textBox6.Text != "")
                    strsearch += " and ProductName like '" + textBox6.Text + "%'";
                if (textBox7.Text != "")
                    strsearch += " and ProductName like '%" + textBox7.Text + "'";
                //OrderID 16,14,15
                if (textBox16.Text != "" && textBox16.Text != "0")
                    strsearch += " and OrderID=" + Convert.ToInt64(textBox16.Text) + "";
                if (textBox14.Text != "")
                {
                    if (textBox15.Text != "0" && textBox15.Text != "")
                        strsearch += " and OrderID between " + Convert.ToInt64(textBox14.Text) + " And " + Convert.ToInt64(textBox15.Text) + "";
                    else if (textBox15.Text == "")
                        strsearch += " and OrderID>=" + Convert.ToInt64(textBox14.Text) + "";
                }
                //CustomerName 18,17,11,10
                if (textBox18.Text != "")
                    strsearch += " and CustomerName='" + textBox18.Text + "'";
                if (textBox17.Text != "")
                    strsearch += " and CustomerName like '%" + textBox17.Text + "%'";
                if (textBox11.Text != "")
                    strsearch += " and CustomerName like '" + textBox11.Text + "%'";
                if (textBox10.Text != "")
                    strsearch += " and CustomerName like '%" + textBox10.Text + "'";
                //Date
                if (faDatePicker1.Text != "[Empty Value]" && faDatePicker1.Text != "")
                {
                    if (faDatePicker2.Text != "[Empty Value]" && faDatePicker2.Text != "")
                        strsearch += " and Date between '" + faDatePicker1.Text + "' And '" + faDatePicker2.Text + "'";
                    else
                        strsearch += " and Date>='" + faDatePicker1.Text + "'";
                }
                //SellPrice 8,9
                if (textBox8.Text != "")
                {
                    if (textBox9.Text != "0" && textBox9.Text != "")
                        strsearch += " and SellPrice between " + Convert.ToInt32(textBox8.Text) + " And " + Convert.ToInt32(textBox9.Text) + "";
                    else if (textBox9.Text == "")
                        strsearch += " and SellPrice>=" + Convert.ToInt32(textBox8.Text) + "";
                }
                //Teadad
                if (textBox12.Text != "")
                {
                    if (textBox13.Text != "0" && textBox13.Text != "")
                        strsearch += " and Teadad between " + Convert.ToInt32(textBox12.Text) + " And " + Convert.ToInt32(textBox13.Text) + "";
                    else if (textBox13.Text == "")
                        strsearch += " and Teadad>=" + Convert.ToInt32(textBox12.Text) + "";
                }
                //TotalPrice 19,20
                if (textBox19.Text != "")
                {
                    if (textBox20.Text != "0" && textBox20.Text != "")
                        strsearch += " and ((SellPrice * ((100 - Discount) / 100)) * Teadad) between " + Convert.ToInt32(textBox19.Text) + " And " + Convert.ToInt32(textBox20.Text) + "";
                    else if (textBox20.Text == "")
                        strsearch += " and ((SellPrice * ((100 - Discount) / 100)) * Teadad)>=" + Convert.ToInt32(textBox19.Text) + "";
                }
                //
                if (strsearch != "")
                {
                    strsearch = strsearch.Substring(4);
                    strsearch = " Where " + strsearch;
                }
                //End Of Where
                if (objDataSet.Tables["TableOrders"] != null) objDataSet.Tables["TableOrders"].Clear();
                objDataAdapter.SelectCommand.CommandText = "select *,((Sellprice * ((100 - Discount) / 100)) * Teadad) as [Total] from View_Orders" + strsearch;
                objDataAdapter.Fill(objDataSet, "TableOrders");

                dataGridView1.DataSource = objDataSet.Tables["TableOrders"];
                //Begin Style in dataGridView
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke;
                dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
                //DataGridview HederText
                dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
                dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
                dataGridView1.Columns[2].HeaderCell.Value = "شماره فاكتور";
                dataGridView1.Columns[3].HeaderCell.Value = "تاريخ صدور";
                dataGridView1.Columns[4].HeaderCell.Value = "نام مشتري";
                dataGridView1.Columns[5].HeaderCell.Value = "قيمت فروش";
                dataGridView1.Columns[6].HeaderCell.Value = "تعداد";
                dataGridView1.Columns[7].HeaderCell.Value = "ميزان تخفيف";
                dataGridView1.Columns[8].HeaderCell.Value = "قيمت كل";
            }
            catch
            {
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_search_Click(sender, e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox3.Focus();
        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox15.Focus();
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox13.Focus();
        }

        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox20.Focus();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 7;
            if (objDataSet.Tables["TableOrders"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableOrders"];
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

        private void frmReportOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender, e);
        }
    }
}
