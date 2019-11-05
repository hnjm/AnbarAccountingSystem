using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class EditOrders : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        DataSet objDataSet = new DataSet();
        SqlCommand objCommand = new SqlCommand();
        string myProductName = "";
        public EditOrders()
        {
            InitializeComponent();
        }

        private void EditOrders_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];

            FarsiLibrary.Win.Controls.FAMonthView fa = new FarsiLibrary.Win.Controls.FAMonthView();
            fa.DefaultCalendar = fa.PersianCalendar;
            fa.DefaultCulture = fa.PersianCulture;
            //Current Data
            PersianCalendar pcalender = new PersianCalendar();
            string year, month, day;
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            toolStripStatusLabel7.Text = year + "/" + month + "/" + day;
            
            //
            //set EnterDetails in DataGridView
            AnbarDataContext objBank = new AnbarDataContext();
            var query = from table in objBank.OrderDetails
                        join table2 in objBank.Products on table.ProductID equals table2.ProductID
                        select new { table.OrderID, table.ProductID, table2.ProductName, table.SellPrice, table.teadad, table.Discount };
            objDataAdapter.SelectCommand.CommandType = CommandType.Text;
            objDataAdapter.SelectCommand.CommandText = query.ToString() + "where OrderID=" + Program.myOrderID + "";
            objDataAdapter.Fill(objDataSet, "OrderDetail");

            dataGridView1.DataSource = objDataSet.Tables["OrderDetail"];

            //Prsentation information in DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //Column Text to Persian
            dataGridView1.Columns[0].HeaderCell.Value = "شماره فاكتور";
            dataGridView1.Columns[1].HeaderCell.Value = "كد كالا";
            dataGridView1.Columns[2].HeaderCell.Value = "نام كالا";
            dataGridView1.Columns[3].HeaderCell.Value = "قيمت فروش";
            dataGridView1.Columns[4].HeaderCell.Value = "تعداد صادره";
            dataGridView1.Columns[5].HeaderCell.Value = "درصد تخفيف";
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            //
            //Customer Code
            SqlDataReader objDataReader;
            Int32 CustomerID = 0;
            objCommand.CommandText = "select CustomerID,[Date] from Orders where OrderID=" + Program.myOrderID + "";
            objCommand.Connection = objcon;
            objcon.Open();
            objDataReader = objCommand.ExecuteReader();
            if (objDataReader.Read())
            {
                CustomerID = Convert.ToInt32(objDataReader[0]);
                textBox3.Text = objDataReader[0].ToString();
                faDatePicker1.Text = objDataReader[1].ToString();
            }
            objDataReader.Close();
            objCommand.Dispose();
            //
            //Customer Name
            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "select CustomerName from Customers where CustomerID=" + CustomerID + "";
            objDataReader = objCommand.ExecuteReader();
            if (objDataReader.Read())
            {
                lbl_customername.Text = objDataReader[0].ToString();
            }
            objDataReader.Close();
            objCommand.Dispose();
            objcon.Close();
            if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            //call computemablaghkol and Clear TextBoxes
            ComputePrice();
            Clear();
        }

        private void Clear()
        {
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void ComputePrice()
        {
            Double mydouble = 0;
            Int64 sellprice = 0;
            Int32 teadad = 0;
            float discount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sellprice = Convert.ToInt64(dataGridView1.Rows[i].Cells[3].Value);
                teadad = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                discount = Convert.ToSingle(dataGridView1.Rows[i].Cells[5].Value);
                mydouble = mydouble + ((sellprice * ((100 - discount) / 100)) * teadad);
            }
            lbl_ghematkol.Text = mydouble.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmViewCustomer frm = new frmViewCustomer();
            frm.ShowDialog();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button7_Click(sender, e);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox3.Text != "")
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.Customers
                            select new { table.CustomerID, table.CustomerName };

                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = query.ToString();
                objCommand.Connection = objcon;
                objcon.Open();
                objDataReader = objCommand.ExecuteReader();
                bool myfound = false;
                while (objDataReader.Read())
                {
                    if (textBox3.Text == objDataReader[0].ToString())
                    {
                        lbl_customername.Text = objDataReader[1].ToString();
                        myfound = true;
                        break;
                    }
                }
                objDataReader.Dispose();
                objDataReader.Close();
                objcon.Close();
                if (myfound == false) textBox3.Focus();
            }
        }

        private void EditOrders_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchcustomer != 0)
            {
                textBox3.Text = Program.mysearchcustomer.ToString();
                textBox3_Leave(sender, e);
                faDatePicker1.Focus();
            }
            if (Program.mysearchproduct != 0)
            {
                textBox5.Text = Program.mysearchproduct.ToString();
                textBox5_Leave(sender, e);
                textBox6.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmViewProductCode frm = new frmViewProductCode();
            frm.ShowDialog();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button8_Click(sender, e);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar!='.') e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox5.Text != "")
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.Products
                            select new { table.ProductID,table.ProductName, table.SellPrice, table.Discountable };
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = query.ToString();
                objCommand.Connection = objcon;

                objcon.Open();
                objDataReader = objCommand.ExecuteReader();

                bool myfound = false;
                while (objDataReader.Read())
                {
                    if (textBox5.Text == objDataReader[0].ToString())
                    {
                        myfound = true;
                        myProductName = objDataReader[1].ToString();
                        textBox6.Text = objDataReader[2].ToString();
                        if (Convert.ToBoolean(objDataReader[3]) == false)
                        { textBox8.Text = "0"; textBox8.ReadOnly = true; }
                        else
                        { textBox8.Clear(); textBox8.ReadOnly = false; };
                        break;
                    }
                }
                objDataReader.Dispose();
                objDataReader.Close();
                objcon.Close();
                if (myfound == false) textBox5.Focus();
            }
        }

        private void btn_newproduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == "") textBox8.Text = "0";
                //چك كردن اينكه كالاي تكراي در جدول نباشد
                bool Found = false;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (textBox5.Text == dataGridView1.Rows[i].Cells[1].Value.ToString()) { Found = true; break; };
                }
                //
                if (Found == false)
                {
                    //Check for the mojodi minus
                    bool Flag = false;
                    SqlDataReader objDataReader;
                    objCommand.CommandText = "select productid,mojodi from products where productid=" + Convert.ToInt32(textBox5.Text) + "";
                    objCommand.Connection = objcon;
                    objcon.Open();
                    objDataReader = objCommand.ExecuteReader();
                    if (objDataReader.Read())
                        if ((Convert.ToInt32(objDataReader[1]) - Convert.ToInt32(textBox7.Text)) >= 0) Flag = true;
                    objDataReader.Close();
                    objcon.Close();
                    if (Flag == true)
                    {
                        DataRow objRow = objDataSet.Tables["OrderDetail"].NewRow();
                        objRow[0] = Program.myOrderID;
                        objRow[1] = Convert.ToInt32(textBox5.Text);
                        objRow[2] = myProductName;
                        objRow[3] = Convert.ToInt64(textBox6.Text);
                        objRow[4] = Convert.ToInt32(textBox7.Text);
                        objRow[5] = Convert.ToSingle(textBox8.Text);
                        objDataSet.Tables["OrderDetail"].Rows.Add(objRow);
                        //call compute the mablagh kol.
                        ComputePrice();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show(".كاربر گرامي موجودي شما كافي نيست", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(".اين كالا در فاكتور مذكور ذكر شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".لطفا در وارد كردن مولفه هاي نا تهي(نام،قيمت خريد و تعداد)دقت كنيد", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result;
                result = MessageBox.Show("آیا قصد حذف رکورد انتخاب شده را دارید ؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    int position = dataGridView1.BindingContext[dataGridView1.DataSource].Position;
                    objDataSet.Tables["OrderDetail"].Rows[position].Delete();
                    //call clear textBoxes
                    Clear();
                    ComputePrice();
                }
            }
            else
            {
                MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader objDataReader;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (textBox8.Text == "") textBox8.Text = "0";
                    int myandis = dataGridView1.BindingContext[objDataSet.Tables["OrderDetail"]].Position;
                    bool found = false;
                    
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (i == dataGridView1.BindingContext[dataGridView1.DataSource].Position) continue;
                        if (Convert.ToInt32(textBox5.Text) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value)) { found = true; break; };
                    }
                    if (found == false)
                    {
                        //
                        //چك كردن براي انكه ناسازگاري دادها در موجودي منفي از بين نرود
                        //
                        bool Flag = true;
                        if (Convert.ToInt32(textBox5.Text) != Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value))
                        {//insert new productd instand of select product
                            objCommand.CommandText = "Select productID,mojodi from products where productid=" + Convert.ToInt32(textBox5.Text) + "";
                            objCommand.Connection = objcon;
                            objcon.Open();
                            objDataReader = objCommand.ExecuteReader();
                            if (objDataReader.Read())
                            {
                                int mojodi = Convert.ToInt32(objDataReader[1]);
                                if ((mojodi - Convert.ToInt32(textBox7.Text)) < 0) Flag = false;
                            }
                            objDataReader.Close();
                            objcon.Close();
                        }
                        else
                        {
                            int mydeffrent = Convert.ToInt32(textBox7.Text) - Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);
                            if (mydeffrent > 0)
                            {//means that increase teadad then i had to check for mojodi
                                objCommand.CommandText = "select productID,mojodi from products where productid=" + Convert.ToInt32(textBox5.Text) + "";
                                objCommand.Connection = objcon;
                                objcon.Open();
                                objDataReader = objCommand.ExecuteReader();
                                if (objDataReader.Read())
                                {
                                    int mojodi = Convert.ToInt32(objDataReader[1]);
                                    if ((mojodi - mydeffrent) < 0) Flag = false;
                                }
                                objDataReader.Close();
                                objcon.Close();
                            }
                        }
                        //
                        //End OF Check For Mojodi.
                        //
                        if (Flag == true)
                        {
                            string ProductName = "";
                            if (Convert.ToInt32(textBox5.Text) != Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value))
                            {
                                objCommand.CommandText = "select ProductID,ProductName from Products where productID=" + Convert.ToInt32(textBox5.Text) + "";
                                objCommand.Connection = objcon;
                                objcon.Open();
                                objDataReader = objCommand.ExecuteReader();
                                if (objDataReader.Read())
                                {
                                    ProductName = objDataReader[1].ToString();
                                }
                                objDataReader.Close();
                                objcon.Close();
                                //dataGridView1.SelectedRows[0].Cells[2].Value = ProductName;
                                objDataSet.Tables["OrderDetail"].Rows[myandis][2] = ProductName;
                            }
                            objDataSet.Tables["OrderDetail"].Rows[myandis][1] = Convert.ToInt32(textBox5.Text);
                            objDataSet.Tables["OrderDetail"].Rows[myandis][3] = Convert.ToInt64(textBox6.Text);
                            objDataSet.Tables["OrderDetail"].Rows[myandis][4] = Convert.ToInt32(textBox7.Text);
                            objDataSet.Tables["OrderDetail"].Rows[myandis][5] = Convert.ToSingle(textBox8.Text);
                            //محاسبه مبلغ كل
                            ComputePrice();
                        }
                        else
                        {
                            MessageBox.Show(".كاربر گرامي موجودي شما كافي نيست", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(".اين كالا در فاكتور مذكور ذكر شده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(".هیج رکوردی انتخاب نشده است", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".عمليات ويرايش امكان پزير نيست", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                SqlDataReader objDataReader;
                objCommand.CommandText = "select productID,Discountable from Products Where ProductID=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value) + "";
                objCommand.Connection = objcon;
                objcon.Open();
                objDataReader = objCommand.ExecuteReader();
                if (objDataReader.Read())
                    if (Convert.ToBoolean(objDataReader[1]) == false)
                    {
                        textBox8.ReadOnly = true;
                    }
                    else
                    {
                        textBox8.ReadOnly = false;
                    }
                objDataReader.Close();
                objcon.Close();
            }
        }

        private void btn_SaveChenge_Click(object sender, EventArgs e)
        {
            try
            {
                //Update OrderDetails Table
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.OrderDetails
                            select table;
                objDataAdapter.SelectCommand.CommandText = query.ToString() + "where OrderID=" + Program.myOrderID + "";
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(objDataAdapter);
                objDataAdapter.Update(objDataSet, "OrderDetail");
                
                //Update Orders Table
                var query2 = (from table in objBank.Orders
                             where table.OrderID == Program.myOrderID
                             select table).Single();
                query2.CustomerID = Convert.ToInt32(textBox3.Text);
                query2.Date = faDatePicker1.Text;
                objBank.SubmitChanges();
                
                //End Of Update
                ComputePrice();
                MessageBox.Show("Commit Transaction.");
            }
            catch { MessageBox.Show("RollBack Transaction."); }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            Program.myCustomerName = lbl_customername.Text;
            Program.myDateFact = faDatePicker1.Text;
            Program.tablequeryOrderDetail = objDataSet.Tables["OrderDetail"];
            //
            frmPrintFactOrders frm = new frmPrintFactOrders();
            frm.Show();
        }

        private void EditOrders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 && btn_newproduct.Enabled == true) btn_newproduct_Click(sender, e);
            if (e.KeyCode == Keys.F6 && btn_Delete.Enabled == true) btn_Delete_Click(sender, e);
            if (e.KeyCode == Keys.F8 && btn_Edit.Enabled == true) btn_Edit_Click(sender, e);
            if (e.KeyCode == Keys.F5 && btn_SaveChenge.Enabled == true) btn_SaveChenge_Click(sender, e);
            if (e.KeyCode == Keys.F9 && btn_print.Enabled == true) btn_print_Click(sender, e);
            if (e.KeyCode == Keys.F4 && btn_exit.Enabled == true) btn_exit_Click(sender, e);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
