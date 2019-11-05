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
    public partial class FrmReportSellerAccount : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();

        string strsearch = "";

        public FrmReportSellerAccount()
        {
            InitializeComponent();
        }

        private void FrmReportSellerAccount_Load(object sender, EventArgs e)
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
            //SellerID
            if (textBox1.Text != "" && textBox1.Text != "0")
                strsearch += " and SellerID=" + Convert.ToInt32(textBox1.Text) + "";
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "0" && textBox3.Text != "")
                    strsearch += " and SellerID between " + Convert.ToInt32(textBox2.Text) + " And " + Convert.ToInt32(textBox3.Text) + "";
                else if (textBox3.Text == "")
                    strsearch += " and SellerID>=" + Convert.ToInt32(textBox2.Text) + "";
            }
            //SellerName
            if (textBox4.Text != "")
                strsearch += " and SellerName='" + textBox4.Text + "'";
            if (textBox5.Text != "")
                strsearch += " and SellerName like '%" + textBox5.Text + "%'";
            if (textBox6.Text != "")
                strsearch += " and SellerName like '" + textBox6.Text + "%'";
            if (textBox7.Text != "")
                strsearch += " and SellerName like '%" + textBox7.Text + "'";
            //EnterID
            if (textBox10.Text != "" && textBox10.Text != "0")
                strsearch += " and EnterID=" + Convert.ToInt64(textBox10.Text) + "";
            if (textBox8.Text != "")
            {
                if (textBox9.Text != "0" && textBox9.Text != "")
                    strsearch += " and EnterID between " + Convert.ToInt64(textBox8.Text) + " And " + Convert.ToInt64(textBox9.Text) + "";
                else if (textBox9.Text == "")
                    strsearch += " and EnterID>=" + Convert.ToInt64(textBox8.Text) + "";
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
            if (objDataSet.Tables["TableSellerAccount"] != null)
            {
                objDataSet.Tables["TableSellerAccount"].Clear();
            }
            objDataAdapter.SelectCommand.CommandText = "Select * From View_SellerAccount" + strsearch;
            objDataAdapter.Fill(objDataSet, "TableSellerAccount");

            dataGridView1.DataSource = objDataSet.Tables["TableSellerAccount"];
            //Style DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //End Style
            dataGridView1.Columns[0].HeaderCell.Value = "شماره فروشنده";
            dataGridView1.Columns[1].HeaderCell.Value = "نام فروشنده";
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
            Program.myPrintCase = 4;
            Program.TableQuery = null;
            if (objDataSet.Tables["TableSellerAccount"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableSellerAccount"];
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

        private void btn_search2_Click(object sender, EventArgs e)
        {
            try
            {
                objDataAdapter.SelectCommand.CommandText = "Select SellerID,SellerName,sum(Debit)as [Debit],sum(Creditor) as [Creditor],sum(Debit)-sum(Creditor) as Remain From View_SellerAccount GrouP By SellerID,SellerName";
                if (objDataSet.Tables["TableSellerAccount2"] != null)
                {
                    objDataSet.Tables["TableSellerAccount2"].Clear();
                }
                objDataAdapter.Fill(objDataSet, "TableSellerAccount2");

                strsearch = "";

                //SellerID 15,16,17
                if (textBox15.Text != "" && textBox15.Text != "0")
                {
                    strsearch += " and SellerID=" + Convert.ToInt32(textBox15.Text) + "";
                }
                if (textBox16.Text != "")
                {
                    if (textBox17.Text != "0" && textBox17.Text != "")
                        strsearch += " and SellerID>=" + Convert.ToInt32(textBox16.Text) + " And SellerId<=" + Convert.ToInt32(textBox17.Text) + "";
                    else if (textBox17.Text == "")
                        strsearch += " and SellerID>=" + Convert.ToInt32(textBox16.Text) + "";
                }
                //SellerName 18,19,20,21
                if (textBox18.Text != "")
                    strsearch += " and SellerName='" + textBox18.Text + "'";
                if (textBox19.Text != "")
                    strsearch += " and SellerName like '%" + textBox19.Text + "%'";
                if (textBox20.Text != "")
                    strsearch += " and SellerName like '" + textBox20.Text + "%'";
                if (textBox21.Text != "")
                    strsearch += " and SellerName like '%" + textBox21.Text + "'";
                //Sum(Debit) 22,23
                if (textBox22.Text != "")
                {
                    if (textBox23.Text != "0" && textBox23.Text != "")
                        strsearch += " and Debit>=" + Convert.ToInt64(textBox22.Text) + " And Debit<=" + Convert.ToInt64(textBox23.Text) + "";
                    else if (textBox23.Text == "")
                        strsearch += " and Debit>=" + Convert.ToInt64(textBox22.Text) + "";
                }
                //sum(Creditor) 24,25
                if (textBox24.Text != "")
                {
                    if (textBox25.Text != "0" && textBox25.Text != "")
                        strsearch += " and Creditor>=" + Convert.ToInt64(textBox24.Text) + " And Creditor<=" + Convert.ToInt64(textBox25.Text) + "";
                    else if (textBox25.Text == "")
                        strsearch += " and Creditor>=" + Convert.ToInt64(textBox24.Text) + "";
                }
                //Remain [sum(Debit)-sum(Creditor)] 26,27
                if (textBox26.Text != "")
                {
                    if (textBox27.Text != "" && textBox27.Text != "0")
                        strsearch += " and Remain>=" + Convert.ToInt64(textBox26.Text) + " And Remain<=" + Convert.ToInt64(textBox27.Text) + "";
                    else if (textBox27.Text == "")
                        strsearch += " and Remain>=" + Convert.ToInt64(textBox26.Text) + "";
                }
                else
                {
                    if (textBox27.Text != "")
                        strsearch += " and Remain<=" + Convert.ToInt64(textBox27.Text) + "";
                }
                //
                if (strsearch != "")
                {
                    strsearch = strsearch.Substring(4);
                }
                //The End
                objDataSet.Tables["TableSellerAccount2"].DefaultView.RowFilter = strsearch;
                dataGridView2.DataSource = objDataSet.Tables["TableSellerAccount2"].DefaultView;

                dataGridView2.Columns[0].HeaderCell.Value = "شماره فروشنده";
                dataGridView2.Columns[1].HeaderCell.Value = "نام فروشنده";
                dataGridView2.Columns[2].HeaderCell.Value = "جمع مبلغ بدهي";
                dataGridView2.Columns[3].HeaderCell.Value = "جمع مبلغ بستانكاري";
                dataGridView2.Columns[4].HeaderCell.Value = "مانده حساب";
                //Style DataGridView
                DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
                objCellStyle.BackColor = Color.WhiteSmoke; ;
                dataGridView2.AlternatingRowsDefaultCellStyle = objCellStyle;
                //The End
            }
            catch
            {
            }
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox17.Focus();
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_search2_Click(sender, e);
        }

        private void textBox22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox23.Focus();
        }

        private void textBox24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox25.Focus();
        }

        private void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox27.Focus();
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar!=8 && e.KeyChar!=13 && e.KeyChar!='-')e.Handled=!char.IsNumber(e.KeyChar);
        }

        private void btn_preview2_Click(object sender, EventArgs e)
        {
            Program.myPrintCase = 5;
            Program.TableQuery = null;
            if (objDataSet.Tables["TableSellerAccount2"] != null)
            {
                Program.TableQuery = objDataSet.Tables["TableSellerAccount2"].DefaultView.ToTable();
            }
            else
            {
                Program.TableQuery = null;
            }
            FrmPrint frm = new FrmPrint();
            frm.Show();
        }

        private void FrmReportSellerAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (tabControl1.SelectedTab.Name == "tabPage1")
                    btn_search_Click(sender, e);
                else if (tabControl1.SelectedTab.Name == "tabPage2")
                    btn_search2_Click(sender, e);
            }
            if (e.KeyCode == Keys.F10)
            {
                if (tabControl1.SelectedTab.Name == "tabPage1")
                    btn_preview_Click(sender, e);
                else if (tabControl1.SelectedTab.Name == "tabPage2")
                    btn_preview2_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
