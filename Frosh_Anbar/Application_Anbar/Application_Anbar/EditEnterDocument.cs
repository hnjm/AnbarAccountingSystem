using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Linq;

namespace Application_Anbar
{
    public partial class EditEnterDocument : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        DataSet objDataSet = new DataSet();
        SqlCommand objCommand = new SqlCommand();
        DataTable objTable = new DataTable();
        string myProductName = "";
        public EditEnterDocument()
        {
            InitializeComponent();
        }

        public void clear()
        {
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }
        private void EditEnterDocument_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];

            FarsiLibrary.Win.Controls.FAMonthView fa = new FarsiLibrary.Win.Controls.FAMonthView();
            fa.DefaultCalendar = fa.PersianCalendar;
            fa.DefaultCulture = fa.PersianCulture;
            //Current Data
            PersianCalendar pcalender = new PersianCalendar();
            string year, month , day;
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            toolStripStatusLabel7.Text = year + "/" + month + "/" + day;
            //set EnterDetails in DataGridView
            AnbarDataContext objBank=new AnbarDataContext();
            var query = from table in objBank.EnterDetails
                        join table2 in objBank.Products on table.ProductID equals table2.ProductID
                        select new {table.EnterID,table.ProductID,table2.ProductName,table.BuyPrice,table.Teadad,table.Discount};
            objDataAdapter.SelectCommand.CommandType = CommandType.Text;
            objDataAdapter.SelectCommand.CommandText = query.ToString()+"where EnterId="+Program.myEnterDocID+"";
            objDataAdapter.Fill(objDataSet, "EnterDetail");

            dataGridView1.DataSource = objDataSet.Tables["EnterDetail"];
            //Prsentation information in DataGridView
            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            //Column Text to Persian
            dataGridView1.Columns[0].HeaderCell.Value = "شماره فاكتور";
            dataGridView1.Columns[1].HeaderCell.Value = "كد كالا";
            dataGridView1.Columns[2].HeaderCell.Value = "نام كالا";
            dataGridView1.Columns[3].HeaderCell.Value = "قيمت خريد";
            dataGridView1.Columns[4].HeaderCell.Value = "تعداد وارده";
            dataGridView1.Columns[5].HeaderCell.Value = "درصد تخفيف";
            if (dataGridView1.SelectedRows.Count!=0)dataGridView1.SelectedRows[0].Selected = false;
            //
            //Seller Code
            SqlDataReader objDataReader;
            Int32 sellerid=0;
            objCommand.CommandText="select sellerId,[Date] from EnterDocuments where EnterID="+Program.myEnterDocID+"";
            objCommand.Connection = objcon;
            objcon.Open();
            objDataReader = objCommand.ExecuteReader();
            if (objDataReader.Read())
            {
                sellerid = Convert.ToInt32(objDataReader[0]);
                textBox3.Text = objDataReader[0].ToString();
                faDatePicker1.Text = objDataReader[1].ToString();
            }
            objDataReader.Close();
            objCommand.Dispose();
            //
            //seller name
            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "select sellername from sellers where sellerid="+sellerid+"";
            objDataReader = objCommand.ExecuteReader();
            if (objDataReader.Read())
            {
                lbl_sellername.Text = objDataReader[0].ToString();
            }
            objDataReader.Close();
            objCommand.Dispose();
            objcon.Close();
            //call computemablaghkol
            compute();
            //call clear
            clear();
            ////Create Table
            //DataColumn objcol;
            ////column 1
            //objcol = new DataColumn("ProductID");//, "System.Int32");
            //objcol.DataType = System.Type.GetType("System.Int32");
            //objTable.Columns.Add(objcol);
            ////column 2
            //objcol = new DataColumn("Teadad");//, "System.Int32");
            //objcol.DataType = System.Type.GetType("System.Int32");
            //objTable.Columns.Add(objcol);
            ////Fill Table
            //FillTable();
        }

        public void FillTable()
        {
            DataRow objRow;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                objRow = objTable.NewRow();
                objRow[0] = dataGridView1.Rows[i].Cells[1].Value;
                objRow[1] = dataGridView1.Rows[i].Cells[4].Value;
                objTable.Rows.Add(objRow);
            }
        }
        public void compute()
        {
            //Compute ghematKol
            Double mydouble = 0;
            Int64 buyprice = 0;
            Int32 teadad = 0;
            float discount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                buyprice = Convert.ToInt64(dataGridView1.Rows[i].Cells[3].Value);
                teadad = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                discount = Convert.ToSingle(dataGridView1.Rows[i].Cells[5].Value);
                mydouble = mydouble + ((buyprice * ((100 - discount) / 100)) * teadad);
            }
            lbl_ghematkol.Text = mydouble.ToString();
            //objCommand.CommandType = CommandType.Text;
            //objCommand.CommandText = "select * from EnterDetails where EnterID=" + Program.myEnterDocID + "";
            //objCommand.Connection = objcon;
            //objcon.Open();
            //objDataReader = objCommand.ExecuteReader();
            //while (objDataReader.Read())
            //{
            //    buyprice = Convert.ToInt64(objDataReader[2]);
            //    teadad = Convert.ToInt32(objDataReader[3]);
            //    discount = Convert.ToSingle(objDataReader[4]);
            //    mydouble = mydouble + ((buyprice * ((100 - discount) / 100)) * teadad);
            //}
            //lbl_ghematkol.Text = mydouble.ToString();
            //objDataReader.Close();
            //objcon.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmviewseller objfrm = new frmviewseller();
            objfrm.ShowDialog();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar!='.') e.Handled = !char.IsNumber(e.KeyChar);

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void EditEnterDocument_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchseller != 0)
            {
                textBox3.Text = Program.mysearchseller.ToString();
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox3.Text != "")
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.Sellers
                            select new { table.SellerID, table.SellerName };

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
                        lbl_sellername.Text = objDataReader[1].ToString();
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

        private void button8_Click(object sender, EventArgs e)
        {
            frmViewProductCode objfrm = new frmViewProductCode();
            objfrm.ShowDialog();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button8_Click(sender, e);
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox5.Text != "")
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.Products
                            select new { table.ProductID,table.ProductName, table.BuyPrice };
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
                        break;
                    }
                }
                objDataReader.Dispose();
                objDataReader.Close();
                objcon.Close();
                if (myfound == false) textBox5.Focus();
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
            }
        }

        private void btn_SaveChenge_Click(object sender, EventArgs e)
        {
            //dataGridView2.DataSource = objDataSet.Tables["EnterDetail"];
            try
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.EnterDetails
                            select table;
                objDataAdapter.SelectCommand.CommandText = query.ToString() + "where EnterId=" + Program.myEnterDocID + "";
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(objDataAdapter);
                objDataAdapter.Update(objDataSet, "EnterDetail");
                ////
                ////Update the mojode in table product code
                ////step 1(search in ObjTable For Deleted Rows
                //bool found = false;
                //for (int i = 0; i < objTable.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                //    {
                //        if (objTable.Rows[i][0] == dataGridView1.Rows[j].Cells[1].Value) { found = true; break; };
                //    }
                //    if (found == false)
                //    {//means that row i Deletet SO I had to mojodi-teadad
                //       var query2 = (from table in objBank.Products
                //                     where table.ProductID == Convert.ToInt32(objTable.Rows[i][0])
                //                     select table).Single();
                //        query2.Mojodi = query2.Mojodi - Convert.ToInt32(objTable.Rows[i][1]);
                //        objBank.SubmitChanges();
                //    }
                //}
                ////
                ////step 2(search the ever of rows in Datagrid with the all rows in ObjTable
                //found = false;
                
                //for (int i = 0,j=0; i < dataGridView1.Rows.Count; i++)
                //{
                //    for (j = 0; j < objTable.Rows.Count; j++)
                //    {
                //        if (dataGridView1.Rows[i].Cells[1].Value == objTable.Rows[j][0]) { found = true; break;};
                //    }
                //    var query2 = (from table in objBank.Products
                //                  where table.ProductID == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value)
                //                  select table).Single();
                //    if (found == true)
                //    {//means rows find in objTable
                //        int myint = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) - Convert.ToInt32(objTable.Rows[j][0]);
                //        if (myint > 0)
                //            query2.Mojodi = query2.Mojodi + myint;
                //        else if (myint < 0)
                //            query2.Mojodi = query2.Mojodi - myint;
                //    }
                //    else
                //    {
                //        query2.Mojodi = query2.Mojodi + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                //    }
                //    objBank.SubmitChanges();
                //}
                ////End Of Edit Mojodi In Table Product
                //objTable.Clear();
                //FillTable();
                compute();
                //Edit EnterDocument Table
                var query3 = (from table in objBank.EnterDocs
                             where table.EnterID == Program.myEnterDocID
                             select table).Single();
                query3.Date = faDatePicker1.Text;
                query3.SellerID = Convert.ToInt32(textBox3.Text);
                objBank.SubmitChanges();
                MessageBox.Show("Commit Transaction.");
            }
            catch
            {
                MessageBox.Show("RollBack Transaction.");
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
                    DataRow objRow = objDataSet.Tables["EnterDetail"].NewRow();
                    objRow[0] = Program.myEnterDocID;
                    objRow[1] = Convert.ToInt32(textBox5.Text);
                    objRow[2] = myProductName;
                    objRow[3] = Convert.ToInt64(textBox6.Text);
                    objRow[4] = Convert.ToInt32(textBox7.Text);
                    objRow[5] = Convert.ToSingle(textBox8.Text);
                    objDataSet.Tables["EnterDetail"].Rows.Add(objRow);
                    //call compute the mablagh kol.
                    compute();
                    clear();
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
                        objDataSet.Tables["EnterDetail"].Rows[position].Delete();
                        //call clear textBoxes
                        clear();
                        compute();
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
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int myandis = dataGridView1.BindingContext[objDataSet.Tables["EnterDetail"]].Position;
                    bool found = false;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (i == dataGridView1.BindingContext[dataGridView1.DataSource].Position) continue;
                        if (Convert.ToInt32(textBox5.Text) == Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value)) { found = true; break; };
                    }
                    if (found == false)
                    {
                        string ProductName="";
                        SqlDataReader objDataReader;
                        if (Convert.ToInt32(textBox5.Text) != Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value))
                        {
                            objCommand.CommandText = "select ProductID,ProductName from Products where productID=" + Convert.ToInt32(textBox5.Text) + "";
                            objCommand.Connection = objcon;
                            objcon.Open();
                            objDataReader = objCommand.ExecuteReader();
                            if (objDataReader.Read()) ProductName = objDataReader[1].ToString();
                            objDataReader.Close();
                            objcon.Close();
                            //dataGridView1.SelectedRows[0].Cells[2].Value = ProductName;
                            objDataSet.Tables["EnterDetail"].Rows[myandis][2] = ProductName;
                        }
                        objDataSet.Tables["EnterDetail"].Rows[myandis][1] = Convert.ToInt32(textBox5.Text);
                        objDataSet.Tables["EnterDetail"].Rows[myandis][3] = Convert.ToInt64(textBox6.Text);
                        objDataSet.Tables["EnterDetail"].Rows[myandis][4] = Convert.ToInt32(textBox7.Text);
                        objDataSet.Tables["EnterDetail"].Rows[myandis][5] = Convert.ToSingle(textBox8.Text);
                        //محاسبه مبلغ كل
                        compute();
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

        private void btn_print_Click(object sender, EventArgs e)
        {
            Program.myDateFact = faDatePicker1.Text;
            Program.mySellerName = lbl_sellername.Text;
            //Program.tablequeryEnterDetail.Clear();
            Program.tablequeryEnterDetail = objDataSet.Tables["EnterDetail"];
            frmPrintFact frm = new frmPrintFact();
            frm.Show();
        }

        private void EditEnterDocument_KeyDown(object sender, KeyEventArgs e)
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
            this.clear();
        }
    }
}