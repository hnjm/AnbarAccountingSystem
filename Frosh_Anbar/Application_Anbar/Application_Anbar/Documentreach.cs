using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
namespace Application_Anbar
{
    public partial class frmDocumentreach : Form
    {
        static SqlConnection objConnection = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objConnection);
        SqlCommand ObjCommand = new SqlCommand();
        DataSet objDataSet = new DataSet();
        BindingSource objBindingSource = new BindingSource();
        string day, month, year;//
        public frmDocumentreach()
        {
            InitializeComponent();
        }
        
        private void frmDocumentreach_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            
            AnbarDataContext Bank = new AnbarDataContext();
            var query = from table in Bank.Cheques
                        select table;
            objDataAdapter.SelectCommand.CommandText = query.ToString();
            objDataAdapter.Fill(objDataSet, "TableCheque");

            objBindingSource.DataSource = objDataSet.Tables["TableCheque"];
            bindingNavigator1.BindingSource = objBindingSource;
            //TextBox DataBinding
            textBox9.DataBindings.Add("Text", objBindingSource, "ChequeNumber");
            textBox2.DataBindings.Add("Text", objBindingSource, "ChequeQuantity");
            textBox8.DataBindings.Add("Text", objBindingSource, "OrderID");
            textBox4.DataBindings.Add("Text", objBindingSource, "AcountNumber");
            textBox3.DataBindings.Add("Text", objBindingSource, "BankName");
            textBox5.DataBindings.Add("Text", objBindingSource, "OwnerAcounnt");
            textBox1.DataBindings.Add("Text", objBindingSource, "DateCheque");
            comboBox1.DataBindings.Add("Text", objBindingSource, "Status");
            //
            //
            PersianCalendar pcalender = new PersianCalendar();
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;

            toolStripStatusLabel6.Text = year + "/" + month + "/" + day;
        }

        private void faDatePicker1_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            string DateNow = faDatePicker1.SelectedDateTime.ToShortDateString();
            PersianCalendar objCalender = new PersianCalendar();
            string Year = objCalender.GetYear(Convert.ToDateTime(DateNow)).ToString();
            string month = objCalender.GetMonth(Convert.ToDateTime(DateNow)).ToString();
            string Day = objCalender.GetDayOfMonth(Convert.ToDateTime(DateNow)).ToString();
            if (month.Length == 1) month = "0" + month;
            if (Day.Length == 1) Day = "0" + Day;
            textBox1.Text = Year + "/" + month + "/" + Day;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                AnbarDataContext Bank = new AnbarDataContext();
                var query = from TableOrder in Bank.Orders
                            join TableCustomer in Bank.Customers
                            on TableOrder.CustomerID equals TableCustomer.CustomerID
                            select TableCustomer.CustomerName;

                SqlDataReader DataReader;
                ObjCommand.CommandText = query.ToString() + " where OrderID=" + Convert.ToInt32(textBox8.Text) + "";
                ObjCommand.Connection = objConnection;
                objConnection.Open();
                DataReader = ObjCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    textBox7.Text = DataReader[0].ToString();
                }
                else
                {
                    textBox7.Text = "Not Define";
                }
                ObjCommand.Dispose();
                DataReader.Close();
                objConnection.Close();
            }
            else
            {
                textBox7.Clear();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                
                SqlCommandBuilder objBuilder = new SqlCommandBuilder(objDataAdapter);
                objDataAdapter.Update(objDataSet.Tables["TableCheque"]);
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.");
            }
        }
        //
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            objDataSet.Tables["TableCheque"].Clear();
            AnbarDataContext Bank = new AnbarDataContext();
            var query = from table in Bank.Cheques
                        select table;
            objDataAdapter.SelectCommand.CommandText = query.ToString();
            objDataAdapter.Fill(objDataSet, "TableCheque");

            objBindingSource.DataSource = objDataSet.Tables["TableCheque"];
            bindingNavigator1.BindingSource = objBindingSource;
            //TextBox DataBinding
            textBox9.DataBindings.Clear();
            textBox9.DataBindings.Add("Text", objBindingSource, "ChequeNumber");
            textBox2.DataBindings.Clear();
            textBox2.DataBindings.Add("Text", objBindingSource, "ChequeQuantity");
            textBox8.DataBindings.Clear();
            textBox8.DataBindings.Add("Text", objBindingSource, "OrderID");
            textBox4.DataBindings.Clear();
            textBox4.DataBindings.Add("Text", objBindingSource, "AcountNumber");
            textBox3.DataBindings.Clear();
            textBox3.DataBindings.Add("Text", objBindingSource, "BankName");
            textBox5.DataBindings.Clear();
            textBox5.DataBindings.Add("Text", objBindingSource, "OwnerAcounnt");
            textBox1.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", objBindingSource, "DateCheque");
            comboBox1.DataBindings.Clear();
            comboBox1.DataBindings.Add("Text", objBindingSource, "Status");
            textBox8_TextChanged(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int andis = 0;
            andis= objBindingSource.Find("ChequeNumber", textBox9.Text);
            objBindingSource.CurrencyManager.Position = andis;
        }

        private void frmDocumentreach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) objBindingSource.AddNew();
            if (e.KeyCode == Keys.F5) toolStripButton2_Click(sender, e);
            if (e.KeyCode == Keys.Escape) toolStripButton3_Click(sender, e);
            if (e.KeyCode == Keys.F6) objBindingSource.RemoveCurrent();
            if (e.KeyCode == Keys.F7) toolStripButton1_Click(sender, e);
        }
    }
}
