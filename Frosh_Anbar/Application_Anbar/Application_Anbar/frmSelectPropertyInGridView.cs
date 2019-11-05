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
    public partial class frmSelectPropertyInGridView : Form
    {
        public frmSelectPropertyInGridView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false) Program.boolProductName = false; else Program.boolProductName = true;
            if (checkBox2.Checked == false) Program.boolCategoryCode = false; else Program.boolCategoryCode = true;
            if (checkBox3.Checked == false) Program.boolcategoryName = false; else Program.boolcategoryName = true;
            if (checkBox4.Checked == false) Program.boolUnit = false; else Program.boolUnit = true;
            if (checkBox5.Checked == false) Program.boolBuyprice = false; else Program.boolBuyprice = true;
            if (checkBox6.Checked == false) Program.boolsellprice = false; else Program.boolsellprice = true;
            if (checkBox7.Checked == false) Program.boolst_mojodi = false; else Program.boolst_mojodi = true;
            if (checkBox8.Checked == false) Program.boolmojodi = false; else Program.boolmojodi = true;
            if (checkBox9.Checked == false) Program.boolDiscountable = false; else Program.boolDiscountable = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSelectPropertyInGridView_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox10.Enabled = false;
        }

        private void frmSelectPropertyInGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(sender, e);
            if (e.KeyCode == Keys.Escape) button2_Click(sender, e);
        }
    }
}
