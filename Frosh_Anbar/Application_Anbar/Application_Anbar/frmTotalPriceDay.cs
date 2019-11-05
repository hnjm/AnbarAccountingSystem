using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class frmTotalPriceDay : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public frmTotalPriceDay()
        {
            InitializeComponent();
        }

        private void frmTotalPriceDay_Load(object sender, EventArgs e)
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
                //Date
                if (faDatePicker1.Text != "[Empty Value]" && faDatePicker1.Text != "")
                {
                    if (faDatePicker2.Text != "[Empty Value]" && faDatePicker2.Text != "")
                        strsearch += " and Date between '" + faDatePicker1.Text + "' And '" + faDatePicker2.Text + "'";
                    else
                        strsearch += " and Date>='" + faDatePicker1.Text + "'";
                }
                //Total
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
                if (objDataSet.Tables["TableTotalPriceOrdersDay"] != null) objDataSet.Tables["TableTotalPriceOrdersDay"].Clear();
                objDataAdapter.SelectCommand.CommandText = "Select * from View_TotalPriceOrdersDay" + strsearch;
                objDataAdapter.Fill(objDataSet, "TableTotalPriceOrdersDay");

                dataGridView1.DataSource = objDataSet.Tables["TableTotalPriceOrdersDay"];
                //Begin Style
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke;
                dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
                //HeaderCell
                dataGridView1.Columns[0].HeaderCell.Value = "تاريخ";
                dataGridView1.Columns[1].HeaderCell.Value = "مبلغ كل فروش در هر روز";
                //The End
            }
            catch
            {
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_search_Click(sender, e);
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 9;
            if (objDataSet.Tables["TableTotalPriceOrdersDay"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableTotalPriceOrdersDay"];
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

        private void frmTotalPriceDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender, e);
        }
    }
}
