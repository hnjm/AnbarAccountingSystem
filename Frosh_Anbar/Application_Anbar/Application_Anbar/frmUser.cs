using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Application_Anbar
{
    public partial class frmUser : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("",objcon);
        SqlCommand objCommand = new SqlCommand();
        DataSet objDataSet = new DataSet();

        public frmUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Flag()
        {
            if (txtName1.Text != "" && txtUser1.Text != "" && txtPass1.Text != "")
            {
                return true;
            }
            return false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (Flag() == true)
                {
                    if (txtPass1.Text == txtRePass1.Text)
                    {
                        AnbarDataContext Bank = new AnbarDataContext();
                        User TableUser = new User();
                        TableUser.Name = txtName1.Text;
                        TableUser.UserName = txtUser1.Text;
                        TableUser.PassUser = txtPass1.Text;
                        TableUser.ManageUser = chkusers1.Checked;
                        TableUser.SaveImport = chkSaveImport1.Checked;
                        TableUser.SaveExport = chkSaveExport1.Checked;
                        TableUser.RemoveImort = chkRemoveImport1.Checked;
                        TableUser.RemoveExport = chkRemoveExport1.Checked;
                        TableUser.LockAccountDay = chkLockAccountDay.Checked;
                        TableUser.AccountCustomer = chkAccountCustomer1.Checked;
                        TableUser.AccountSeller = chkAccountSeller1.Checked;
                        TableUser.ReachDocument = chkReach1.Checked;
                        TableUser.AllowReport = chkReport1.Checked;
                        Bank.Users.InsertOnSubmit(TableUser);
                        Bank.SubmitChanges();
                        frmUser_Load(sender, e);
                        txtName1.Clear();
                        txtUser1.Clear();
                        txtPass1.Clear();
                        txtRePass1.Clear();
                        MessageBox.Show(".اطلاعات كاربر جديد با كوفقيت در سيستم ثبت شد");
                        //MessageBox.Show("/" + txtPass1.Text + "/");
                    }
                    else
                    {
                        MessageBox.Show(".رمز هاي وارد شده شما يكسان نيست", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(".عمليات ايجاد كاربر جديد ممكن نيست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(".عمليات ايجاد كاربر جديد ممكن نيست", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            AnbarDataContext Bank = new AnbarDataContext();
            var query = from Table in Bank.Users
                        select Table;
            objDataAdapter.SelectCommand.CommandText = query.ToString();
            if (objDataSet.Tables["TableUser"] != null) objDataSet.Tables["TableUser"].Clear();
            objDataAdapter.Fill(objDataSet, "TableUser");
            //comboUser.Items.Clear();
            comboUser.DataSource = objDataSet.Tables["TableUser"];
            comboUser.DisplayMember = "UserName";
            comoUser_SelectedIndexChanged(sender, e);
        }

        private void comoUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnbarDataContext Bank=new AnbarDataContext();
            SqlDataReader DataReader;
            if (comboUser.Text != "")
            {
                objCommand.CommandText = "Select * From Users Where UserName='" + comboUser.Text + "'";
                objCommand.Connection = objcon;
                objcon.Open();
                DataReader = objCommand.ExecuteReader();
                if (DataReader.Read())
                {
                    txtName2.Text = DataReader[0].ToString();
                    txtPass2.Text = DataReader[2].ToString();
                    txtRePass2.Text = DataReader[2].ToString();
                    chkUser2.Checked = Convert.ToBoolean(DataReader[12]);
                    chkSaveImport2.Checked = Convert.ToBoolean(DataReader[3]);
                    chkSaveExport2.Checked = Convert.ToBoolean(DataReader[4]);
                    ChkRemoveImort2.Checked = Convert.ToBoolean(DataReader[5]);
                    chkRemoveExport2.Checked = Convert.ToBoolean(DataReader[6]);
                    chkLockAccountDay2.Checked = Convert.ToBoolean(DataReader[7]);
                    ChkAccountCustomer2.Checked = Convert.ToBoolean(DataReader[8]);
                    chkaccountSeller2.Checked = Convert.ToBoolean(DataReader[9]);
                    chkReach2.Checked = Convert.ToBoolean(DataReader[10]);
                    chkReport2.Checked = Convert.ToBoolean(DataReader[11]);
                }
                objCommand.Dispose();
                DataReader.Close();
                objcon.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext Bank = new AnbarDataContext();
                var query = (from Table in Bank.Users
                             where Table.UserName == comboUser.Text
                             select Table).Single();
                if (query != null)
                {
                    if (txtPass2.Text == txtRePass2.Text)
                    {
                        query.Name = txtName2.Text;
                        query.PassUser = txtPass2.Text;
                        query.SaveImport = chkSaveImport2.Checked;
                        query.SaveExport = chkSaveExport2.Checked;
                        query.RemoveImort = ChkRemoveImort2.Checked;
                        query.RemoveExport = chkRemoveExport2.Checked;
                        query.LockAccountDay = chkLockAccountDay2.Checked;
                        query.AccountCustomer = ChkAccountCustomer2.Checked;
                        query.AccountSeller = chkaccountSeller2.Checked;
                        query.ReachDocument = chkReach2.Checked;
                        query.AllowReport = chkReport2.Checked;
                        query.ManageUser = chkUser2.Checked;
                        Bank.SubmitChanges();
                        frmUser_Load(sender, e);
                        //comoUser_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(".رمز هاي وارد شده شما يكسان نيست", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(".عمليات اصلاح كاربر جديد ممكن نيست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(".عمليات اصلاح كاربر جديد ممكن نيست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnbarDataContext Bank = new AnbarDataContext();
            var Query = (from Table in Bank.Users
                         where Table.UserName == comboUser.Text
                         select Table).Single();
            Bank.Users.DeleteOnSubmit(Query);
            Bank.SubmitChanges();
            frmUser_Load(sender, e);
            //comoUser_SelectedIndexChanged(sender, e);
        }
    }
}