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
    public partial class frmViewProductCode : Form
    {
        static SqlConnection objcon = new SqlConnection(@"Data Source=.;Initial Catalog=Anbar;Integrated Security=True");
        SqlDataAdapter objDataAdapter = new SqlDataAdapter("", objcon);
        DataSet objDataSet = new DataSet();

        public frmViewProductCode()
        {
            InitializeComponent();
        }

        private void frmViewProductCode_Load(object sender, EventArgs e)
        {
            Program.mysearchproduct = 0;
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            AnbarDataContext objBank = new AnbarDataContext();

            var query = from table in objBank.Products
                        select new { table.ProductID, table.ProductName, table.Unit };
            objDataAdapter.SelectCommand.CommandType = CommandType.Text;
            objDataAdapter.SelectCommand.CommandText = query.ToString();
            objDataAdapter.Fill(objDataSet, "ViewProductCode");

            dataGridView1.DataSource = objDataSet.Tables["ViewProductCode"];
            dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
            dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
            dataGridView1.Columns[2].HeaderCell.Value = "واحد";

            DataGridViewCellStyle objCellStyle = new DataGridViewCellStyle();
            objCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.AlternatingRowsDefaultCellStyle = objCellStyle;
            dataGridView1.SelectedRows[0].Selected = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int myandis = -1;
            bool myfound = false;
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
                if (dataGridView1.SelectedRows.Count != 0) dataGridView1.SelectedRows[0].Selected = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView objDataView = new DataView();
            objDataView = objDataSet.Tables["ViewProductCode"].DefaultView;
            objDataView.RowFilter = "ProductName Like '" + textBox2.Text + "*'";

            dataGridView1.DataSource = objDataView;
            dataGridView1.Columns[0].HeaderCell.Value = "كد كالا";
            dataGridView1.Columns[1].HeaderCell.Value = "نام كالا";
            dataGridView1.Columns[2].HeaderCell.Value = "واحد";
        }

        private void frmViewProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    Program.mysearchproduct = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
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