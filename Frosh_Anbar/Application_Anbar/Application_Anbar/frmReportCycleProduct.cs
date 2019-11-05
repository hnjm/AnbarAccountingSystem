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
    public partial class frmReportCycleProduct : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public frmReportCycleProduct()
        {
            InitializeComponent();
        }

        private void frmReportCycleProduct_Load(object sender, EventArgs e)
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
                //TeadadVarede
                if (textBox12.Text != "")
                {
                    if (textBox13.Text != "0" && textBox13.Text != "")
                        strsearch += " and TeadadVarede between " + Convert.ToInt32(textBox12.Text) + " And " + Convert.ToInt32(textBox13.Text) + "";
                    else if (textBox13.Text == "")
                        strsearch += " and TeadadVarede>=" + Convert.ToInt32(textBox12.Text) + "";
                }
                //TeadadSadere
                if (textBox8.Text != "")
                {
                    if (textBox9.Text != "0" && textBox9.Text != "")
                        strsearch += " and TeadadSadere between " + Convert.ToInt32(textBox8.Text) + " And " + Convert.ToInt32(textBox9.Text) + "";
                    else if (textBox9.Text == "")
                        strsearch += " and TeadadSadere>=" + Convert.ToInt32(textBox8.Text) + "";
                }
                //Mojodi 10,11
                if (textBox10.Text != "")
                {
                    if (textBox11.Text != "0" && textBox11.Text != "")
                        strsearch += " and Mojodi between " + Convert.ToInt32(textBox10.Text) + " And " + Convert.ToInt32(textBox11.Text) + "";
                    else if (textBox11.Text == "")
                        strsearch += " and Mojodi>=" + Convert.ToInt32(textBox10.Text) + "";
                }
                //
                if (strsearch != "")
                {
                    strsearch = strsearch.Substring(4);
                    strsearch = " Where " + strsearch;
                }
                //End Of Where
                if (objDataSet.Tables["TableCycleProduct"] != null) objDataSet.Tables["TableCycleProduct"].Clear();
                objDataAdapter.SelectCommand.CommandText = "select * from View_CycleProduct" + strsearch;
                objDataAdapter.Fill(objDataSet, "TableCycleProduct");

                dataGridView1.DataSource = objDataSet.Tables["TableCycleProduct"];
                //Begin Style
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke;
                dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
                //DataGridview HederText
                dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
                dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
                dataGridView1.Columns[2].HeaderCell.Value = "تعداد كل واردات";
                dataGridView1.Columns[3].HeaderCell.Value = "تعداد كل صادرات";
                dataGridView1.Columns[4].HeaderCell.Value = "موجودي";
                //The End
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

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox13.Focus();
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
            Program.myPrintCase = 10;
            if (objDataSet.Tables["TableCycleProduct"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableCycleProduct"];
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

        private void frmReportCycleProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender, e);
        }
    }
}
