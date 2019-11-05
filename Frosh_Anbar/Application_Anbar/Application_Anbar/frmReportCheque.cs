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
    public partial class frmReportCheque : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public frmReportCheque()
        {
            InitializeComponent();
        }

        private void frmReportCheque_Load(object sender, EventArgs e)
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
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox14.Text = "0";
            textBox15.Text = "0";
            textBox16.Text = "0";
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox12.Text = "";
            textBox13.Text = "";
            comboBox1.Text = "";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            strsearch = "";
            //OrderID
            if (textBox1.Text != "" && textBox1.Text != "0")
                strsearch += " and OrderId=" + Convert.ToInt64(textBox1.Text) + "";
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "0" && textBox3.Text != "")
                    strsearch += " and OrderID between " + Convert.ToInt64(textBox2.Text) + " And " + Convert.ToInt64(textBox3.Text) + "";
                else if (textBox3.Text == "")
                    strsearch += " and OrderID>=" + Convert.ToInt64(textBox2.Text) + "";
            }
            //CustomerName
            if (textBox4.Text != "")
                strsearch += " and CustomerName='" + textBox4.Text + "'";
            if (textBox5.Text != "")
                strsearch += " and CustomerName like '%" + textBox5.Text + "%'";
            if (textBox6.Text != "")
                strsearch += " and CustomerName like '" + textBox6.Text + "%'";
            if (textBox7.Text != "")
                strsearch += " and CustomerName like '%" + textBox7.Text + "'";
            //ChequeNumber
            if (textBox14.Text != "" && textBox14.Text != "0")
                strsearch += " and ChequeNumber=" + Convert.ToInt64(textBox14.Text) + "";
            if (textBox15.Text != "")
            {
                if (textBox16.Text != "0" && textBox16.Text != "")
                    strsearch += " and ChequeNumber between " + Convert.ToInt64(textBox15.Text) + " And " + Convert.ToInt64(textBox16.Text) + "";
                else if (textBox16.Text == "")
                    strsearch += " and ChequeNumber>=" + Convert.ToInt64(textBox15.Text) + "";
            }
            //ChequeQuantity
            if (textBox8.Text != "")
            {
                if (textBox9.Text != "0" && textBox9.Text != "")
                    strsearch += " and ChequeQuantity between " + Convert.ToInt64(textBox8.Text) + " And " + Convert.ToInt64(textBox9.Text) + "";
                else if (textBox9.Text == "")
                    strsearch += " and ChequeQuantity>=" + Convert.ToInt64(textBox8.Text) + "";
            }
            //AcountNumber
            if (textBox10.Text != "")
            {
                if (textBox11.Text != "0" && textBox11.Text != "")
                    strsearch += " and AcountNumber between " + Convert.ToInt64(textBox10.Text) + " And " + Convert.ToInt64(textBox11.Text) + "";
                else if (textBox11.Text == "")
                    strsearch += " and AcountNumber>=" + Convert.ToInt64(textBox10.Text) + "";
            }
            //OwnerAcounnt
            if (textBox13.Text != "")
                strsearch += " and OwnerAcounnt='" + textBox13.Text + "'";
            //BankName
            if (textBox12.Text != "")
                strsearch += " and BankName='" + textBox12.Text + "'";
            //StatusCheque
            if (comboBox1.Text != "")
                strsearch += " and Status='" + comboBox1.Text + "'";
            //DateCheque
            if (faDatePicker1.Text != "[Empty Value]" && faDatePicker1.Text!="")
            {
                if (faDatePicker2.Text != "[Empty Value]" && faDatePicker2.Text != "")
                    strsearch += " and DateCheque between '" + faDatePicker1.Text + "' And '" + faDatePicker2.Text + "'";
                else
                    strsearch += " and DateCheque>='" + faDatePicker1.Text + "'";
            }
            //
            if (strsearch != "")
            {
                strsearch = strsearch.Substring(4);
                strsearch = " Where " + strsearch;
            }
            //End Of Where
            if (objDataSet.Tables["TableCheque"] != null)
            {
                objDataSet.Tables["TableCheque"].Clear();
            }
            objDataAdapter.SelectCommand.CommandText = "Select * From View_Cheque" + strsearch;
            objDataAdapter.Fill(objDataSet, "TableCheque");

            dataGridView1.DataSource = objDataSet.Tables["TableCheque"];
            //Style DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //HeaderText For Column in DataGridView
            dataGridView1.Columns[0].HeaderCell.Value = "شماره فاكتور";
            dataGridView1.Columns[1].HeaderCell.Value = "شماره چك";
            dataGridView1.Columns[2].HeaderCell.Value = "مبلغ چك";
            dataGridView1.Columns[3].HeaderCell.Value = "نام مشتري";
            dataGridView1.Columns[4].HeaderCell.Value = "شماره حساب";
            dataGridView1.Columns[5].HeaderCell.Value = "صاحب حساب";
            dataGridView1.Columns[6].HeaderCell.Value = "نام بانك";
            dataGridView1.Columns[7].HeaderCell.Value = "تاريخ چك";
            dataGridView1.Columns[8].HeaderCell.Value = "وضعيت چك";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //The End
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

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox16.Focus();
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox11.Focus();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 1;
            Program.TableQuery = null;
            //if (Program.TableQuery != null) Program.TableQuery.Clear();
            if (objDataSet.Tables["TableCheque"]!=null)
                Program.TableQuery = objDataSet.Tables["TableCheque"];
            FrmPrint frm = new FrmPrint();
            frm.Show();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReportCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender, e);
        }

    }
}
