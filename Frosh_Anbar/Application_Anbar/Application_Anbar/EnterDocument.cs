using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace Application_Anbar
{
    public partial class EnterDocument : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        SqlCommand objCommand = new SqlCommand();
        DataSet objDataSet = new DataSet();
        DataTable objtable = new DataTable();
        DataColumn objcol;
        DataRow objrow;
        string year, month, day;

        public EnterDocument()
        {
            InitializeComponent();
        }
        void clear()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            faDatePicker1.Text = year + "/" + month + "/" + day;
        }
        private void EnterDocument_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];

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
            faDatePicker1.Text = year + "/" + month + "/" + day;
            clear();
            //Disable Controls
            btn_newproduct.Enabled = false;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
            //colomn 1
            objcol = new DataColumn("ProductID");
            objcol.DataType = System.Type.GetType("System.Int32");
            objtable.Columns.Add(objcol);
            //column 2
            objcol = new DataColumn("BuyPrice");
            objcol.DataType = System.Type.GetType("System.Int64");
            objtable.Columns.Add(objcol);
            //column 3
            objcol = new DataColumn("Teadad");
            objcol.DataType = System.Type.GetType("System.Int32");
            objtable.Columns.Add(objcol);
            //column 4
            objcol = new DataColumn("Discount");
            objcol.DataType = System.Type.GetType("System.Single");
            objcol.DefaultValue = 0;
            objtable.Columns.Add(objcol);
            //set Primary kay on ProductID
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = objtable.Columns["ProductID"];
            objtable.PrimaryKey = PrimaryKeyColumns;
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            //disable controls
            textBox1.ReadOnly = true;
            btn_newfact.Enabled = false;
            btn_vieweditfact.Enabled = false;
            btn_exit.Enabled = false;
            
            clear();
            Int64 myint=0;
            objCommand.Connection = objcon;
            objCommand.CommandText = "select EnterID from EnterDocuments";
            objcon.Open();
            objDataReader = objCommand.ExecuteReader();
            if (objDataReader.HasRows == true)
            {
                while (objDataReader.Read())
                {
                    myint =Convert.ToInt64(objDataReader[0]);
                }
                myint++;
                textBox1.Text = myint.ToString();
            }
            else
            {
                textBox1.Text = "1";
            }
            objDataReader.Close();
            objcon.Close();
            faDatePicker1.Focus();
            //Clear Table
            objtable.Clear();
            //Enable Controls
            btn_newproduct.Enabled = true;
            btn_save.Enabled = true;
            btn_cancel.Enabled = true;
        }

        private void btn_newproduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == "") textBox8.Text = "0";
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
                {
                    objrow = objtable.NewRow();
                    objrow[0] =Convert.ToInt32(textBox5.Text);
                    objrow[1] =Convert.ToInt64(textBox6.Text);
                    objrow[2] =Convert.ToInt32(textBox7.Text);
                    objrow[3] =Convert.ToSingle(textBox8.Text);
                    objtable.Rows.Add(objrow);
                    //Clear TextBoxs
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox5.Focus();
                }
                else
                {
                    MessageBox.Show(".لطفا در وارد كردن مولفه هاي نا تهي(نام،قيمت خريد و تعداد)دقت كنيد", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(".(لطفا در وارد كردن اقلام كالا دقت كنيد(از جمله وارد كردن كالاهاي تكراري", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            frmviewseller objfrm = new frmviewseller();
            objfrm.ShowDialog();
        }

        private void EnterDocument_Activated(object sender, EventArgs e)
        {
            if (Program.mysearchseller != 0)
            {
                textBox3.Text = Program.mysearchseller.ToString();
                textBox5.Focus();
                textBox3_Leave(sender, e);
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
            SqlConnection objcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Anbar;Integrated Security=True");
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
                        myfound = true;
                        textBox4.Text = objDataReader[1].ToString();
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

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox5.Text != "")
            {
                AnbarDataContext objBank = new AnbarDataContext();
                var query = from table in objBank.Products
                            select new { table.ProductID, table.BuyPrice };
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = query.ToString();
                objCommand.Connection = objcon;

                objcon.Open();
                objDataReader = objCommand.ExecuteReader();

                bool myfound = false;
                while(objDataReader.Read())
                {
                    if (textBox5.Text == objDataReader[0].ToString())
                    {
                        myfound = true;
                        textBox6.Text = objDataReader[1].ToString();
                        break;
                    }
                }
                objDataReader.Dispose();
                objDataReader.Close();
                objcon.Close();
                if (myfound == false) textBox5.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext objBank = new AnbarDataContext();
                EnterDetail objTableEnterDetails;// = new EnterDetail[objtable.Rows.Count];
                EnterDoc objTableEnterDoc = new EnterDoc();
                //Insert in Table EnterDocument
                objTableEnterDoc.EnterID = Convert.ToInt64(textBox1.Text);
                objTableEnterDoc.Date = Convert.ToString(faDatePicker1.Text);
                objTableEnterDoc.SellerID = Convert.ToInt32(textBox3.Text);
                objTableEnterDoc.Validate = true;
                objBank.EnterDocs.InsertOnSubmit(objTableEnterDoc);
                //Insert Into Table EnterDetails
                for (int i = 0; i < objtable.Rows.Count; i++)
                {
                    objTableEnterDetails = new EnterDetail();
                    objTableEnterDetails.EnterID = Convert.ToInt64(textBox1.Text);
                    objTableEnterDetails.ProductID = Convert.ToInt32(objtable.Rows[i][0]);
                    objTableEnterDetails.BuyPrice = Convert.ToInt64(objtable.Rows[i][1]);
                    objTableEnterDetails.Teadad = Convert.ToInt32(objtable.Rows[i][2]);
                    objTableEnterDetails.Discount = Convert.ToSingle(objtable.Rows[i][3]);
                    objBank.EnterDetails.InsertOnSubmit(objTableEnterDetails);
                }
                objBank.SubmitChanges();
                MessageBox.Show("Commit Transaction.","موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //شروع مجدد
                clear();
                textBox1.ReadOnly = false;
                btn_newfact.Enabled = true;
                btn_exit.Enabled = true;
                btn_vieweditfact.Enabled = true;
                btn_newproduct.Enabled = false;
                btn_save.Enabled = false;
                btn_cancel.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Rollback Transaction.","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //شروع مجدد
            clear();
            objtable.Clear();
            textBox1.ReadOnly = false;
            btn_newfact.Enabled = true;
            btn_exit.Enabled = true;
            btn_vieweditfact.Enabled = true;
            btn_newproduct.Enabled = false;
            btn_save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar!='.') e.Handled = !char.IsNumber(e.KeyChar);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_vieweditfact_Click(sender, e);
        }

        private void btn_vieweditfact_Click(object sender, EventArgs e)
        {
            SqlDataReader objDataReader;
            if (textBox1.Text != "")
            {
                Program.myEnterDocID = Convert.ToInt32(textBox1.Text);
                //check the Validate Document
                objCommand.CommandText = "select EnterID,Validate from EnterDocuments where EnterID=" + Program.myEnterDocID + "";
                objCommand.Connection = objcon;
                objcon.Open();
                objDataReader = objCommand.ExecuteReader();
                if (objDataReader.Read())
                {
                    if (Convert.ToInt32(objDataReader[1]) == 1)
                    {
                        EditEnterDocument editenterdocument = new EditEnterDocument();
                        editenterdocument.Show();
                    }
                    else
                    {
                        MessageBox.Show(".شما نمي توانيد اطلاعات مربوط به اين سند را مورد مشاهده و ويرايش قرار دهيد", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(".كاربر گرامي فاكتوري با اين شماره در سيستم موجود نيست", "دقت کنید", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                }
                objcon.Close();
                objDataReader.Close();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btn_newfact.Enabled == true) button1_Click(sender, e);
            if (e.KeyCode == Keys.F3 && btn_newproduct.Enabled == true) btn_newproduct_Click(sender, e);
            if (e.KeyCode == Keys.F5 && btn_save.Enabled == true) btn_save_Click(sender, e);
            if (e.KeyCode == Keys.Escape && btn_cancel.Enabled == true) btn_cancel_Click(sender, e);
            if (e.KeyCode == Keys.F10 && btn_vieweditfact.Enabled == true) btn_vieweditfact_Click(sender, e);
            if (e.KeyCode == Keys.F4 && btn_exit.Enabled == true) btn_exit_Click(sender, e);
        }
    }
}