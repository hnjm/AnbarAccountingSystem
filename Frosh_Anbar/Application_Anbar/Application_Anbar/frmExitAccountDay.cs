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
    public partial class frmExitAccountDay : Form
    {
        public frmExitAccountDay()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection objCon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
            SqlCommand objCommand = new SqlCommand();
            //update EnterDocuments
            objCommand.CommandText = "Update EnterDocuments set Validate=0 Where Date='" + faDatePicker1.Text + "'";
            objCommand.Connection = objCon;
            objCon.Open();
            objCommand.ExecuteNonQuery();
            //update Orders Validate
            objCommand.Dispose();
            objCommand.CommandText = "Update Orders set Validate=0 Where Date='" + faDatePicker1.Text + "'";
            objCommand.ExecuteNonQuery();
            objCommand.Clone();
            for (int i = progressBar1.Value; i < 100; i++) progressBar1.Value += 1;
            MessageBox.Show(".با موفقييت به پايان رسيد " + faDatePicker1.Text + " كاربر گرامي عمليات بستن حساب روز","موفق");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void faDatePicker1_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
        }

        private void frmExitAccountDay_Load(object sender, EventArgs e)
        {
            FarsiLibrary.Win.Controls.FAMonthView fa = new FarsiLibrary.Win.Controls.FAMonthView();
            fa.DefaultCalendar = fa.PersianCalendar;
            fa.DefaultCulture = fa.PersianCulture;
        }
    }
}
