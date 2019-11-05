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
    public partial class ReportProduct : Form
    {
        static SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objCon);
        DataSet objDataSet = new DataSet();
        
        string strsearch = "";

        public ReportProduct()
        {
            InitializeComponent();
        }

        private void ReportProduct_Load(object sender, EventArgs e)
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
            comboBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            Program.boolProductName = true;
            Program.boolCategoryCode = true;
            Program.boolcategoryName = true;
            Program.boolUnit=true;
            Program.boolBuyprice=true;
            Program.boolsellprice=true;
            Program.boolst_mojodi=true;
            Program.boolmojodi=true;
            Program.boolDiscountable = true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
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
            //Category ID
            if (textBox14.Text != "0" && textBox14.Text != "")
                strsearch += " and CategoryID=" + Convert.ToInt32(textBox14.Text) + "";
            //CategoryName
            if (textBox15.Text != "")
                strsearch += " and CategoryName='" + textBox15.Text + "'";
            //Unit
            if (comboBox1.Text != "")
                strsearch += " and Unit='" + comboBox1.Text + "'";
            //buy Price
            if (textBox8.Text != "")
            {
                if (textBox9.Text != "0" && textBox9.Text != "")
                    strsearch += " and BuyPrice between " + Convert.ToInt32(textBox8.Text) + " And " + Convert.ToInt32(textBox9.Text) + "";
                else if (textBox9.Text == "")
                    strsearch += " and BuyPrice>=" + Convert.ToInt32(textBox8.Text) + "";
            }
            //Sell Price
            if (textBox10.Text != "")
            {
                if (textBox11.Text != "0" && textBox11.Text != "")
                    strsearch += " and SellPrice between " + Convert.ToInt32(textBox10.Text) + " And " + Convert.ToInt32(textBox11.Text) + "";
                else if (textBox11.Text == "")
                    strsearch += " and SellPrice>=" + Convert.ToInt32(textBox10.Text) + "";
            }
            //Mojodi
            if (textBox12.Text != "")
            {
                if (textBox13.Text != "0" && textBox13.Text != "")
                    strsearch += " and Mojodi between " + Convert.ToInt32(textBox12.Text) + " And " + Convert.ToInt32(textBox13.Text) + "";
                else if (textBox13.Text == "")
                    strsearch += " and Mojodi>=" + Convert.ToInt32(textBox12.Text) + "";
            }
            //Discount
            if (checkBox2.Checked == true)
                strsearch += " and Discountable=1";
            //Mojodi Standard
            if (checkBox1.Checked == true)
                strsearch += " and mojodi<=st_mojodi";
            
            if (strsearch!="")
            {
                strsearch = strsearch.Substring(4);
                strsearch = " Where "+strsearch;
            }
            //End Of Where
            if (objDataSet.Tables["TableProducts"] != null)
            {
                objDataSet.Tables["TableProducts"].Clear();
                objDataSet.Tables["TableProducts"].Columns.Clear();
            }
            objDataAdapter.SelectCommand.CommandText = "select * from view_Products" + strsearch;
            objDataAdapter.Fill(objDataSet, "TableProducts");
            dataGridView1.DataSource = objDataSet.Tables["TableProducts"];
            //Style DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //DataGridview HederText
            dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
            dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
            dataGridView1.Columns[2].HeaderCell.Value = "كد غرفه";
            dataGridView1.Columns[3].HeaderCell.Value = "نام غرفه";
            dataGridView1.Columns[4].HeaderCell.Value = "واحد اندازه گيري";
            dataGridView1.Columns[5].HeaderCell.Value = "قيمت خريد";
            dataGridView1.Columns[6].HeaderCell.Value = "قيمت فروش";
            dataGridView1.Columns[7].HeaderCell.Value = "موجودي كل";
            dataGridView1.Columns[8].HeaderCell.Value = "موجودي استاندارد";
            dataGridView1.Columns[9].HeaderCell.Value = "قابليت تخفيف";
            //End Of Style
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

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox11.Focus();
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBox13.Focus();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            //Fill Table 1
            if (Program.tablequeryproduct_1 != null) Program.tablequeryproduct_1.Clear();
            objDataAdapter.SelectCommand.CommandText = "select * from view_Products" + strsearch;
            objDataAdapter.Fill(Program.tablequeryproduct_1);
            //Fill Table 2
            if (Program.tablequeryproduct_2 != null) Program.tablequeryproduct_2.Clear();
            objDataAdapter.SelectCommand.CommandText = "select ProductID,ProductName,CategoryName,Unit,SellPrice,Discountable from view_Products" + strsearch;
            objDataAdapter.Fill(Program.tablequeryproduct_2);
            //Fill Table 3
            if (Program.tablequeryproduct_3 != null) Program.tablequeryproduct_3.Clear();
            objDataAdapter.SelectCommand.CommandText = "select ProductID,ProductName,CategoryName,Unit,BuyPrice,SellPrice,Discountable from view_Products" + strsearch;
            objDataAdapter.Fill(Program.tablequeryproduct_3);
            //Fill Table 4
            if (Program.tablequeryproduct_4 != null) Program.tablequeryproduct_4.Clear();
            objDataAdapter.SelectCommand.CommandText = "select ProductID,ProductName,CategoryName,Unit,St_mojodi,Mojodi from view_Products" + strsearch;
            objDataAdapter.Fill(Program.tablequeryproduct_4);
            //End Of Fill Table For Printing

            FrmSelectPropertyProduct frm = new FrmSelectPropertyProduct();
            frm.ShowDialog();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            frmSelectPropertyInGridView frm = new frmSelectPropertyInGridView();
            frm.ShowDialog();
        }

        private void ReportProduct_Activated(object sender, EventArgs e)
        {
            if (objDataSet.Tables["TableProducts"] != null)
            {
                objDataSet.Tables["TableProducts"].Clear();
                objDataSet.Tables["TableProducts"].Columns.Clear();
                objDataAdapter.SelectCommand.CommandText = "select * from view_Products" + strsearch;
                objDataAdapter.Fill(objDataSet, "TableProducts");

                dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
                dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
                dataGridView1.Columns[2].HeaderCell.Value = "كد غرفه";
                dataGridView1.Columns[3].HeaderCell.Value = "نام غرفه";
                dataGridView1.Columns[4].HeaderCell.Value = "واحد اندازه گيري";
                dataGridView1.Columns[5].HeaderCell.Value = "قيمت خريد";
                dataGridView1.Columns[6].HeaderCell.Value = "قيمت فروش";
                dataGridView1.Columns[7].HeaderCell.Value = "موجودي كل";
                dataGridView1.Columns[8].HeaderCell.Value = "موجودي استاندارد";
                dataGridView1.Columns[9].HeaderCell.Value = "قابليت تخفيف";
                //
                //End Of Fill In DataGridView
                //
                //ProductName
                if (Program.boolProductName == false)// && objDataSet.Tables["TableProducts"].Columns["ProductName"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("ProductName");
                //else if (Program.boolProductName==true && objDataSet.Tables["TableProducts"].Columns["ProductName"] == null)
                //    objDataSet.Tables["TableProducts"].Columns.Add(Program.tablequeryproduct_1.Columns[1]);
                //CategoryCode
                if (Program.boolCategoryCode == false)// && objDataSet.Tables["TableProducts"].Columns["CategoryID"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("CategoryID");
                //CategoryName
                if (Program.boolcategoryName == false)// && objDataSet.Tables["TableProducts"].Columns["CategoryName"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("CategoryName");
                //Unit
                if (Program.boolUnit == false)// && objDataSet.Tables["TableProducts"].Columns["Unit"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("Unit");
                //BuyPrice
                if (Program.boolBuyprice == false)// && objDataSet.Tables["TableProducts"].Columns["BuyPrice"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("Buyprice");
                //Sellprice
                if (Program.boolsellprice == false)// && objDataSet.Tables["TableProducts"].Columns["SellPrice"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("SellPrice");
                //Mojodi
                if (Program.boolmojodi == false)// && objDataSet.Tables["TableProducts"].Columns["Mojodi"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("Mojodi");
                //St_Mojodi
                if (Program.boolst_mojodi == false)// && objDataSet.Tables["TableProducts"].Columns["St_mojodi"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("st_mojodi");
                //Discountable
                if (Program.boolDiscountable == false)// && objDataSet.Tables["TableProducts"].Columns["Discountable"] != null)
                    objDataSet.Tables["TableProducts"].Columns.Remove("Discountable");
            }
        }

        private void ReportProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6) btn_search_Click(sender, e);
            if (e.KeyCode == Keys.F7) btn_select_Click(sender, e);
            if (e.KeyCode == Keys.F10) btn_preview_Click(sender, e);
            if (e.KeyCode == Keys.Escape) btn_Cancel_Click(sender,e);
        }
    }
}
