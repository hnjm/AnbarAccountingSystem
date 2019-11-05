using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32;
using System.IO;

namespace Application_Anbar
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void بستنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void تعریفکالاهایجدیدالورودToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewProductCode objnewproductcode = new NewProductCode();
            objnewproductcode.Show();
        }

        private void بستنToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Class1.frmLogin.Close();
            
        }

        private void تعریفغرفههاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gorfe objGorfe = new Gorfe();
            objGorfe.Show();
        }

        private void ایجادکدمشتریانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerCode objcustomercode = new CustomerCode();
            objcustomercode.Show();
        }

        private void ایجادکدفروشندگانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SellerCode objsellercode = new SellerCode();
            objsellercode.Show();
        }

       private void درخواستکالاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestProduct objRequestProduct = new RequestProduct();
            objRequestProduct.Show();
        }

        private void وروداطلاعاتواسنادبهانبارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.SaveImport)
            {
                EnterDocument objEnterDocument = new EnterDocument();
                objEnterDocument.Show();
            }
            else
            {
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد","اخطار",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        private void فروشکالابهمشتریانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.SaveExport)
            {
                Orders objorders = new Orders();
                objorders.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void صورتحسابمشتریانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AccountCustomer)
            {
                frmCustomerAccount objcustomeraccount = new frmCustomerAccount();
                objcustomeraccount.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void صورتحسابفروشندگانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AccountSeller)
            {
                frmSellerAccount objselleraccount = new frmSellerAccount();
                objselleraccount.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Permision.ReachDocument)
            {
                frmDocumentreach objdocumentreach = new frmDocumentreach();
                objdocumentreach.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void sample2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDisplay sample1 = new frmDisplay();
            sample1.Show();
        }

        private void گزارشمشخصاتکالاToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //Persentation
                if (File.Exists("wal.jpg"))
                {
                    File.Copy("wal.jpg", "waltemp.jpg", true);
                    this.BackgroundImage = System.Drawing.Image.FromFile("waltemp.jpg");
                }
                //string strlayut = "", strColor = "";
                //strlayut = Registry.GetValue("HKEY_CURRENT_USER\\WalpaperAnbar", "layut", "strech").ToString();
                //object colorr = Registry.GetValue("HKEY_CURRENT_USER\\WalpaperAnbar", "color", "Black");

                //this.BackColor =(Color) colorr;
                //
                int myday = Convert.ToInt16(DateTime.Now.DayOfWeek);
                string strday = "";
                switch (myday)
                {
                    case 0: { strday = "يك شنبه"; break; }
                    case 1: { strday = "دوشنبه"; break; }
                    case 2: { strday = "سه شنبه"; break; }
                    case 3: { strday = "چهارشنبه"; break; }
                    case 4: { strday = "پنج شنبه"; break; }
                    case 5: { strday = "جمعه"; break; };
                    case 6: { strday = "شنبه"; break; }
                }
                PersianCalendar pcalender = new PersianCalendar();
                string year, month, day;
                year = pcalender.GetYear(DateTime.Now).ToString();
                month = pcalender.GetMonth(DateTime.Now).ToString();
                day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
                if (month.Length == 1) month = "0" + month;
                if (day.Length == 1) day = "0" + day;
                toolStripLabel3.Text = toolStripLabel3.Text + strday + " " + year + "/" + month + "/" + day;
            }
            catch
            {
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            EditRequest frmeditrequest = new EditRequest();
            frmeditrequest.Show();
        }

        private void گزارشمشحخصاتكالاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                ReportProduct frmReportProduct = new ReportProduct();
                frmReportProduct.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void بستنحسابروزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.LockAccountDay)
            {
                frmExitAccountDay frm = new frmExitAccountDay();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void حذففاکتورواردهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.RemoveImort)
            {
                frmDeleteEnters frm = new frmDeleteEnters();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void حذففاکتورسفارشاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.RemoveExport)
            {
                frmDeleteOrders frm = new frmDeleteOrders();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void طراحيگزارشازاسناددريافتنيچكToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmReportCheque frm = new frmReportCheque();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void گزارشصورتحسابمشترريانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                FrmReportCustomerAccount frm = new FrmReportCustomerAccount();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void گزارشصورتحسابفروشندگانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                FrmReportSellerAccount frm = new FrmReportSellerAccount();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void طراحيگزارشازوروداطلاعاتواسنادبهسيستمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmReportEnterDocuments frm = new frmReportEnterDocuments();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void طراحيگزارشازفروشكالابهمشتريانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmReportOrders frm = new frmReportOrders();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void سرجمعرياليفاكتورهايصادرهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmTotalPriceOrders frm = new frmTotalPriceOrders();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void گزارشسرجمعرياليفاكتورهايصادرهدزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmTotalPriceDay frm = new frmTotalPriceDay();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void طراحيگزارشكليگردشكالادرفروشگاهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Permision.AllowReport)
            {
                frmReportCycleProduct frm = new frmReportCycleProduct();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel2.Text ="ساعت "+ DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            frmDisplay frm = new frmDisplay();
            frm.Show();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show(Class1.frmmain.BackgroundImage.);
            this.BackgroundImage.Save("wal.jpg");
            //Registry.SetValue("HKEY_CURRENT_USER\\WalpaperAnbar", "Color", this.BackColor.Name);
            //Registry.SetValue("HKEY_CURRENT_USER\\WalpaperAnbar", "Layut", this.BackgroundImageLayout.ToString());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Permision.manageUser)
            {
                frmUser frm = new frmUser();
                frm.Show();
            }
            else
            {
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Class1.frmLogin.txtUserName.Text = "";
            Class1.frmLogin.txtUserPass.Text = "";
            Class1.frmLogin.Show();
            Class1.frmLogin.txtUserName.Focus();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (Permision.LockAccountDay)
            {
                frmExitAccountDay frm = new frmExitAccountDay();
                frm.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Permision.SaveImport)
            {
                EnterDocument objEnterDocument = new EnterDocument();
                objEnterDocument.Show();
            }
            else
            {
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (Permision.SaveExport)
            {
                Orders objorders = new Orders();
                objorders.Show();
            }
            else
                MessageBox.Show(".كاربر گرامي شما مجوز چنين عملي را در سيستم نداريد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class1.frmLogin.Close();
        }

    }
}