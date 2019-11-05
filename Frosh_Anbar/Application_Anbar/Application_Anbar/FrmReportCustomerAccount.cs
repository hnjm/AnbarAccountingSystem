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
    public partial class FrmReportCustomerAccount : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public FrmReportCustomerAccount()
        {
            InitializeComponent();
        }
        
        private void FrmReportCustomerAccount_Load(object sender, EventArgs e)
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
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox12.Text = "0";
            textBox13.Text = "0";
            textBox14.Text = "0";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            strsearch = "";
            //CustomerID
            if (textBox1.Text != "" && textBox1.Text != "0")
                strsearch += " and CustomerID=" + Convert.ToInt32(textBox1.Text) + "";
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "0" && textBox3.Text != "")
                    strsearch += " and CustomerID between " + Convert.ToInt32(textBox2.Text) + " And " + Convert.ToInt32(textBox3.Text) + "";
                else if (textBox3.Text == "")
                    strsearch += " and CustomerID>=" + Convert.ToInt32(textBox2.Text) + "";
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
            //OrderID
            if (textBox10.Text != "" && textBox10.Text != "0")
                strsearch += " and OrderId=" + Convert.ToInt64(textBox10.Text) + "";
            if (textBox8.Text != "")
            {
                if (textBox9.Text != "0" && textBox9.Text != "")
                    strsearch += " and OrderID between " + Convert.ToInt64(textBox8.Text) + " And " + Convert.ToInt64(textBox9.Text) + "";
                else if (textBox9.Text == "")
                    strsearch += " and OrderID>=" + Convert.ToInt64(textBox8.Text) + "";
            }
            //Date
            if (faDatePicker1.Text != "[Empty Value]" && faDatePicker1.Text != "")
            {
                if (faDatePicker2.Text != "[Empty Value]" && faDatePicker2.Text != "")
                    strsearch += " and Date between '" + faDatePicker1.Text + "' And '" + faDatePicker2.Text + "'";
                else
                    strsearch += " and Date>='" + faDatePicker1.Text + "'";
            }
            //Debit
            if (textBox11.Text != "")
            {
                if (textBox12.Text != "0" && textBox12.Text != "")
                    strsearch += " and Debit between " + Convert.ToInt64(textBox11.Text) + " And " + Convert.ToInt64(textBox12.Text) + "";
                else if (textBox12.Text == "")
                    strsearch += " and Debit>=" + Convert.ToInt64(textBox11.Text) + "";
            }
            //Creditor
            if (textBox13.Text != "")
            {
                if (textBox14.Text != "0" && textBox14.Text != "")
                    strsearch += " and Creditor between " + Convert.ToInt64(textBox13.Text) + " And " + Convert.ToInt64(textBox14.Text) + "";
                else if (textBox14.Text == "")
                    strsearch += " and Creditor>=" + Convert.ToInt64(textBox13.Text) + "";
            }
            //
            if (strsearch != "")
            {
                strsearch = strsearch.Substring(4);
                strsearch = " Where " + strsearch;
            }
            //End Of Where
            if (objDataSet.Tables["TableCustomerAccount"] != null)
            {
                objDataSet.Tables["TableCustomerAccount"].Clear();
            }
            objDataAdapter.SelectCommand.CommandText = "Select * From View_CustomerAccount" + strsearch;
            objDataAdapter.Fill(objDataSet, "TableCustomerAccount");

            dataGridView1.DataSource = objDataSet.Tables["TableCustomerAccount"];
            //Style DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //End Style
            dataGridView1.Columns[0].HeaderCell.Value = "شماره مشتري";
            dataGridView1.Columns[1].HeaderCell.Value = "نام مشتري";
            dataGridView1.Columns[2].HeaderCell.Value = "شماره فاكتور";
            dataGridView1.Columns[3].HeaderCell.Value = "تاريخ";
            dataGridView1.Columns[4].HeaderCell.Value = "مبلغ بدهي";
            dataGridView1.Columns[5].HeaderCell.Value = "مبلغ بستانكاري";
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

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox9.Focus();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox12.Focus();
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox14.Focus();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 2;
            if (objDataSet.Tables["TableCustomerAccount"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableCustomerAccount"];
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                objDataAdapter.SelectCommand.CommandText = "Select customerID,CustomerName,sum(Debit)as [Debit],sum(Creditor) as [Creditor],sum(Debit)-sum(Creditor) as Remain From View_CustomerAccount GrouP By CustomerID,CustomerName";
                if (objDataSet.Tables["TableCustomerAccount2"] != null)
                {
                    objDataSet.Tables["TableCustomerAccount2"].Clear();
                }
                objDataAdapter.Fill(objDataSet, "TableCustomerAccount2");

                strsearch = "";
                //CustomerID
                if (textBox42.Text != "" && textBox42.Text != "0")
                {
                    strsearch += " and CustomerID=" + Convert.ToInt32(textBox42.Text) + "";
                }
                if (textBox40.Text != "")
                {
                    if (textBox41.Text != "0" && textBox41.Text != "")
                        strsearch += " and CustomerID>=" + Convert.ToInt32(textBox40.Text) + " And CustomerID<=" + Convert.ToInt32(textBox41.Text) + "";
                    else if (textBox41.Text == "")
                        strsearch += " and CustomerID>=" + Convert.ToInt32(textBox40.Text) + "";
                }
                //CustomerName
                if (textBox39.Text != "")
                    strsearch += " and CustomerName='" + textBox39.Text + "'";
                if (textBox38.Text != "")
                    strsearch += " and CustomerName like '%" + textBox38.Text + "%'";
                if (textBox37.Text != "")
                    strsearch += " and CustomerName like '" + textBox37.Text + "%'";
                if (textBox36.Text != "")
                    strsearch += " and CustomerName like '%" + textBox36.Text + "'";
                //Debit
                if (textBox31.Text != "")
                {
                    if (textBox32.Text != "0" && textBox32.Text != "")
                        strsearch += " and Debit>=" + Convert.ToInt64(textBox31.Text) + " And Debit<=" + Convert.ToInt64(textBox32.Text) + "";
                    else if (textBox32.Text == "")
                        strsearch += " and Debit>=" + Convert.ToInt64(textBox31.Text) + "";
                }
                //Creditor
                if (textBox29.Text != "")
                {
                    if (textBox30.Text != "0" && textBox30.Text != "")
                        strsearch += " and Creditor>=" + Convert.ToInt64(textBox29.Text) + " And Creditor<=" + Convert.ToInt64(textBox30.Text) + "";
                    else if (textBox30.Text == "")
                        strsearch += " and Creditor>=" + Convert.ToInt64(textBox29.Text) + "";
                }
                //Remain    33,34
                if (textBox33.Text != "")
                {
                    if (textBox34.Text != "" && textBox34.Text!="0")
                        strsearch += " and Remain>=" + Convert.ToInt64(textBox33.Text) + " And Remain<=" + Convert.ToInt64(textBox34.Text) + "";
                    else if (textBox34.Text == "")
                        strsearch += " and Remain>=" + Convert.ToInt64(textBox33.Text) + "";
                }
                else
                {
                    if (textBox34.Text != "")
                        strsearch += " and Remain<=" + Convert.ToInt64(textBox34.Text) + "";
                }
                //
                if (strsearch != "")
                {
                    strsearch = strsearch.Substring(4);
                }
                //The End
                objDataSet.Tables["TableCustomerAccount2"].DefaultView.RowFilter = strsearch;
                dataGridView3.DataSource = objDataSet.Tables["TableCustomerAccount2"].DefaultView;

                dataGridView3.Columns[0].HeaderCell.Value = "شماره مشتري";
                dataGridView3.Columns[1].HeaderCell.Value = "نام مشتري";
                dataGridView3.Columns[2].HeaderCell.Value = "جمع مبلغ بدهي";
                dataGridView3.Columns[3].HeaderCell.Value = "جمع مبلغ بستانكاري";
                dataGridView3.Columns[4].HeaderCell.Value = "مانده حساب";
                //Style DataGridView
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke; ;
                dataGridView3.AlternatingRowsDefaultCellStyle = objCellStyle;
                //
            }
            catch
            {
            }
        }

        private void textBox42_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button6_Click(sender, e);
        }

        private void textBox40_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox41.Focus();
        }

        private void textBox31_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox32.Focus();
        }

        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox30.Focus();
        }

        private void textBox33_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox34.Focus();
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar!=8 && e.KeyChar!=13 && e.KeyChar!='-') e.Handled=!char.IsNumber(e.KeyChar);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 3;
            Program.TableQuery = null;
            if (objDataSet.Tables["TableCustomerAccount2"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableCustomerAccount2"].DefaultView.ToTable();
            }
            else
            {
                Program.TableQuery = null;
            }
            FrmPrint frm = new FrmPrint();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReportCustomerAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (tabControl1.SelectedTab.Name == "tabPage1")
                    btn_search_Click(sender, e);
                else if (tabControl1.SelectedTab.Name == "tabPage2")
                    button6_Click(sender, e);
            }
            if (e.KeyCode == Keys.F10)
            {
                if (tabControl1.SelectedTab.Name == "tabPage1")
                    btn_preview_Click(sender, e);
                else if (tabControl1.SelectedTab.Name == "tabPage2")
                    button5_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
