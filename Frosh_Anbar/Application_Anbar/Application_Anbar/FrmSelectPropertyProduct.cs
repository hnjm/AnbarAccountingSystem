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
    public partial class FrmSelectPropertyProduct : Form
    {
        public FrmSelectPropertyProduct()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSelectPropertyProduct_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                Program.myPrintProductCase = 1;
            else if(radioButton2.Checked==true)
                Program.myPrintProductCase = 2;
            else if (radioButton3.Checked==true)
                Program.myPrintProductCase = 3;
            else if (radioButton4.Checked==true)
                Program.myPrintProductCase = 4;
            //frm.show
            frmPrintProducts frm = new frmPrintProducts();
            frm.Show();
        }
    }
}
