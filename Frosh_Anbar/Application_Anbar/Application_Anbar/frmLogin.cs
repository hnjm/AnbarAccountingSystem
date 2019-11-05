using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Application_Anbar
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                AnbarDataContext Bank = new AnbarDataContext();
                var Query = from Table in Bank.Users
                            where Table.UserName == txtUserName.Text && Table.PassUser == txtUserPass.Text
                            select Table;
                if (Query.Count() != 0)
                {
                    foreach (var T in Query)
                    {
                        Class1.frmmain.toolStripLabel4.Text = "كاربر فعال : " + T.Name;
                        Permision.SaveImport = (bool)T.SaveImport;
                        Permision.SaveExport = (bool)T.SaveExport;
                        Permision.RemoveImort = (bool)T.RemoveImort;
                        Permision.RemoveExport = (bool)T.RemoveExport;
                        Permision.LockAccountDay = (bool)T.LockAccountDay;
                        Permision.AccountCustomer = (bool)T.AccountCustomer;
                        Permision.AccountSeller = (bool)T.AccountSeller;
                        Permision.ReachDocument = (bool)T.ReachDocument;
                        Permision.AllowReport = (bool)T.AllowReport;
                        Permision.manageUser = (bool)T.ManageUser;
                    }
                    Class1.frmmain.lblpermis.Text = "";
                    if (Permision.manageUser) Class1.frmmain.lblpermis.Text += "* مديريت تمام كاربران *" + "\n";
                    if (Permision.SaveImport) Class1.frmmain.lblpermis.Text += "* ثبت اطلاعات در ورود اطلاعات واسناد *" + "\n";
                    if (Permision.SaveExport) Class1.frmmain.lblpermis.Text += "* ثبت اطلاعات در فروش كالا به مشتريان *" + "\n";
                    if (Permision.RemoveImort) Class1.frmmain.lblpermis.Text += "* حذف فاكتورهاي وارده *" + "\n";
                    if (Permision.RemoveExport) Class1.frmmain.lblpermis.Text += "* حذف فاكتورهاي صادره *" + "\n";
                    if (Permision.LockAccountDay) Class1.frmmain.lblpermis.Text += "* بستن حساب هاي روزانه *" + "\n";
                    if (Permision.AccountCustomer) Class1.frmmain.lblpermis.Text += "* ثبت اطلاعات در صورت حساب مشتريان *" + "\n";
                    if (Permision.AccountSeller) Class1.frmmain.lblpermis.Text += "* ثبت اطلاعات در صورت حساب فروشندگان *" + "\n";
                    if (Permision.ReachDocument) Class1.frmmain.lblpermis.Text += "* ثبت اطلاعات اسناد دريافتني و چك *" + "\n";
                    if (Permision.AllowReport) Class1.frmmain.lblpermis.Text += "* طراحي گزارش از اطلاعات سيستم *" + "\n";
                    this.Hide();
                    Class1.frmmain.Show();
                }
                else
                {
                    MessageBox.Show(".رمز عبور يا نام كاربري وارد شده نادرست است", "خطا");
                }
            }
            catch
            {
                MessageBox.Show("خطا : در ورود به سيستم : لطفآ در وارد كردن رمز عبور و نام كاربري خود دقت كنيد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
        }

        private void txtUserPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_Login_Click(sender, e);
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtUserPass.Focus();
        }
    }
}
