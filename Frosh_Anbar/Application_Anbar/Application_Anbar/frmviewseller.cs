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
    public partial class frmviewseller : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("",objcon);
        DataSet objDataSet = new DataSet();

        public frmviewseller()
        {
            InitializeComponent();
        }

        private void frmviewseller_Load(object sender, EventArgs e)
        {
            Program.mysearchseller = 0;
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            AnbarDataContext objAnbarDataContext=new AnbarDataContext();
            
            var query = from table in objAnbarDataContext.Sellers
                        select new { table.SellerID, table.SellerName };
            
            objDataAdapter.SelectCommand.CommandText = query.ToString();
            objDataAdapter.Fill(objDataSet, "ViewSellerCode");

            dataGridView1.DataSource = objDataSet.Tables["ViewSellerCode"];
            dataGridView1.Columns[0].HeaderCell.Value = "كد فروشنده";
            dataGridView1.Columns[1].HeaderCell.Value = "نام فروشنده";
            DataGridViewCellStyle objDataGridViewCellStyle = new DataGridViewCellStyle();
            objDataGridViewCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objDataGridViewCellStyle;
            dataGridView1.SelectedRows[0].Selected = false;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int myandis=-1;
            bool myfound=false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (textBox1.Text == dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    myandis = i;
                    myfound = true;
                    break;
                }
            }
            if (myfound == true)
            {
                dataGridView1.BindingContext[dataGridView1.DataSource].Position = myandis;
            }
            else
            {
                if (dataGridView1.SelectedRows.Count!=0) dataGridView1.SelectedRows[0].Selected = false;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView objDataView = new DataView();
            objDataView = objDataSet.Tables["ViewSellerCode"].DefaultView;
            objDataView.RowFilter="SellerName Like '"+textBox2.Text+"*'";
            
            dataGridView1.DataSource = objDataView;
            dataGridView1.Columns[0].HeaderCell.Value = "كد فروشنده";
            dataGridView1.Columns[1].HeaderCell.Value = "نام فروشنده";
        }

        private void frmviewseller_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    Program.mysearchseller = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    this.Close();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 13) e.Handled = !char.IsNumber(e.KeyChar);
        }
    }
}