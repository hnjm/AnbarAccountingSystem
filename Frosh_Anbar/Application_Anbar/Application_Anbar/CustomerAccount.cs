using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;

namespace Application_Anbar
{
    public partial class frmCustomerAccount : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        SqlCommand objCommand = new SqlCommand();
        string year, month, day;

        public frmCustomerAccount()
        {
            InitializeComponent();
        }

        private void CustomerAccount_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            //Persian Calender
            FarsiLibrary.Win.Controls.FAMonthView fa = new FarsiLibrary.Win.Controls.FAMonthView();
            fa.DefaultCalendar = fa.PersianCalendar;
            fa.DefaultCulture = fa.PersianCulture;

            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;

            toolStripStatusLabel4.Text = year + "/" + month + "/" + day;
            //**********

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //
            //Tab 1
            //
            if (textBox1.Text != "")
            {
                Int64 myOrderID = 0;
                bool Flag = false;
                SqlDataReader DataReader;
                objCommand.CommandText = "select * From Orders where OrderID=" + Convert.ToInt64(textBox1.Text) + "";
                objCommand.Connection = objcon;
                objcon.Open();
                DataReader = objCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    myOrderID = Convert.ToInt64(DataReader[0]);
                    lbl_customercode.Text = Convert.ToString(DataReader[1]);
                    lbl_DateFact.Text = Convert.ToString(DataReader[2]);
                    Flag = true;
                }
                objCommand.Dispose();
                DataReader.Close();
                if (Flag == true)
                {
                    //*****CustomerName
                    objCommand.CommandText = "select CustomerName from Customers Where customerid=" + Convert.ToInt32(lbl_customercode.Text) + "";
                    DataReader = objCommand.ExecuteReader();
                    if (DataReader.Read()) lbl_customerName.Text = DataReader[0].ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                    //*****All Price of Product
                    Double mydouble = 0;
                    Int64 sellprice = 0;
                    Int32 teadad = 0;
                    float discount = 0;
                    objCommand.CommandText = "select * from OrderDetails where OrderID=" + myOrderID + "";
                    DataReader = objCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        sellprice = Convert.ToInt64(DataReader[2]);
                        teadad = Convert.ToInt32(DataReader[3]);
                        discount = Convert.ToSingle(DataReader[4]);
                        mydouble = mydouble + ((sellprice * ((100 - discount) / 100)) * teadad);
                    }
                    lbl_allprice.Text = mydouble.ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                }
                objCommand.Dispose();
                DataReader.Close();
                objcon.Close();
                if (Flag == false) textBox1.Focus();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext objBank = new AnbarDataContext();
                CustomerAccount objTableCustomerAccount = new CustomerAccount();
                objTableCustomerAccount.OrderID = Convert.ToInt64(textBox1.Text);
                objTableCustomerAccount.Date = lbl_DateFact.Text;
                objTableCustomerAccount.Debit = Convert.ToInt64(textBox3.Text);
                objTableCustomerAccount.Creditor = 0;
                objBank.CustomerAccounts.InsertOnSubmit(objTableCustomerAccount);
                objBank.SubmitChanges();
                textBox1_Leave(sender, e);
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                Int64 myOrderID = 0;
                bool Flag = false;
                SqlDataReader DataReader;
                objCommand.CommandText = "select * From Orders where OrderID=" + Convert.ToInt64(textBox7.Text) + "";
                objCommand.Connection = objcon;
                objcon.Open();
                DataReader = objCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    myOrderID = Convert.ToInt64(DataReader[0]);
                    lbl_CustomerCode2.Text = Convert.ToString(DataReader[1]);
                    faDatePicker3.Text = Convert.ToString(DataReader[2]);
                    Flag = true;
                }
                objCommand.Dispose();
                DataReader.Close();
                if (Flag == true)
                {
                    //*****CustomerName
                    objCommand.CommandText = "select CustomerName from Customers Where customerid=" + Convert.ToInt32(lbl_CustomerCode2.Text) + "";
                    DataReader = objCommand.ExecuteReader();
                    if (DataReader.Read()) lbl_CustomerName2.Text = DataReader[0].ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                    //*****AllDebit for Customer
                    Int64 myDebit = 0, myCreditor = 0;
                    objCommand.CommandText = "select OrderID,Debit,Creditor from CustomerAccount where OrderID=" + myOrderID + "";
                    DataReader = objCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        myDebit += Convert.ToInt64(DataReader[1]);
                        myCreditor += Convert.ToInt64(DataReader[2]);
                    }
                    lbl_PayPrice.Text = myCreditor.ToString();
                    lbl_AllDebitPrice.Text = myDebit.ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                }
                if (Flag == false) textBox7.Focus();
                DataReader.Close();
                objcon.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext objBank = new AnbarDataContext();
                CustomerAccount objTableCustomerAccount = new CustomerAccount();
                objTableCustomerAccount.OrderID = Convert.ToInt64(textBox7.Text);
                objTableCustomerAccount.Date = faDatePicker3.Text;
                objTableCustomerAccount.Debit = 0;
                objTableCustomerAccount.Creditor = Convert.ToInt64(textBox9.Text);
                objBank.CustomerAccounts.InsertOnSubmit(objTableCustomerAccount);
                objBank.SubmitChanges();
                textBox7_Leave(sender, e);
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.");
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_save_Click(sender, e);
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button4_Click(sender, e);
        }
    }
}
