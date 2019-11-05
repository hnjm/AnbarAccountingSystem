using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;

namespace Application_Anbar
{
    public partial class frmSellerAccount : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        SqlCommand objCommand = new SqlCommand();
        string year, month, day;

        public frmSellerAccount()
        {
            InitializeComponent();
        }

        private void frmSellerAccount_Load(object sender, EventArgs e)
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
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //Tab 1
            //
            if (textBox1.Text != "")
            {
                Int64 myEnterID = 0;
                bool Flag = false;
                SqlDataReader DataReader;
                objCommand.CommandText = "select * From EnterDocuments where EnterID=" + Convert.ToInt64(textBox1.Text) + "";
                objCommand.Connection = objcon;
                objcon.Open();
                DataReader = objCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    myEnterID = Convert.ToInt64(DataReader[0]);
                    lbl_sellerCode.Text = Convert.ToString(DataReader[1]);
                    lbl_DateFact.Text = Convert.ToString(DataReader[2]);
                    Flag = true;
                }
                objCommand.Dispose();
                DataReader.Close();
                if (Flag == true)
                {
                    //*****CustomerName
                    objCommand.CommandText = "select SellerName from Sellers Where Sellerid=" + Convert.ToInt32(lbl_sellerCode.Text) + "";
                    DataReader = objCommand.ExecuteReader();
                    if (DataReader.Read()) lbl_SellerName.Text = DataReader[0].ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                    //*****All Price of Product
                    Double mydouble = 0;
                    Int64 buyPrice = 0;
                    Int32 teadad = 0;
                    float discount = 0;
                    objCommand.CommandText = "select * from EnterDetails where EnterID=" + myEnterID + "";
                    DataReader = objCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        buyPrice = Convert.ToInt64(DataReader[2]);
                        teadad = Convert.ToInt32(DataReader[3]);
                        discount = Convert.ToSingle(DataReader[4]);
                        mydouble = mydouble + ((buyPrice * ((100 - discount) / 100)) * teadad);
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
                AnbarDataContext Bank = new AnbarDataContext();
                SellerAccount objTableSellerAccount = new SellerAccount();
                objTableSellerAccount.EnterID = Convert.ToInt64(textBox1.Text);
                objTableSellerAccount.Date = lbl_DateFact.Text;
                objTableSellerAccount.Debit = Convert.ToInt64(textBox3.Text);
                objTableSellerAccount.Creditor = 0;
                Bank.SellerAccounts.InsertOnSubmit(objTableSellerAccount);
                Bank.SubmitChanges();
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.");
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                Int64 myEnterID = 0;
                bool Flag = false;
                SqlDataReader DataReader;
                objCommand.CommandText = "select * From EnterDocuments where EnterID=" + Convert.ToInt64(textBox7.Text) + "";
                objCommand.Connection = objcon;
                objcon.Open();
                DataReader = objCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    myEnterID = Convert.ToInt64(DataReader[0]);
                    lbl_SellerCode2.Text = Convert.ToString(DataReader[1]);
                    faDatePicker3.Text = Convert.ToString(DataReader[2]);
                    Flag = true;
                }
                objCommand.Dispose();
                DataReader.Close();
                if (Flag == true)
                {
                    //*****SellerName
                    objCommand.CommandText = "select SellerName from Sellers Where SellerID=" + Convert.ToInt32(lbl_SellerCode2.Text) + "";
                    DataReader = objCommand.ExecuteReader();
                    if (DataReader.Read()) lbl_SellerName2.Text = DataReader[0].ToString();
                    objCommand.Dispose();
                    DataReader.Close();
                    //*****AllDebit for SellerName
                    Int64 myDebit = 0, myCreditor = 0;
                    objCommand.CommandText = "select EnterID,Debit,Creditor from SellerAccount where EnterID=" + myEnterID + "";
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

        private void btn_save2_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext Bank = new AnbarDataContext();
                SellerAccount objTableSellerAccount = new SellerAccount();
                objTableSellerAccount.EnterID = Convert.ToInt64(textBox7.Text);
                objTableSellerAccount.Date = faDatePicker3.Text;
                objTableSellerAccount.Debit = 0;
                objTableSellerAccount.Creditor = Convert.ToInt64(textBox9.Text);
                Bank.SellerAccounts.InsertOnSubmit(objTableSellerAccount);
                Bank.SubmitChanges();
                textBox7_Leave(sender, e);
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.");
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }
    }
}
