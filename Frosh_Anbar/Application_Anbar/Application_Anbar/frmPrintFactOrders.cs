using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Application_Anbar
{
    public partial class frmPrintFactOrders : Form
    {
        public frmPrintFactOrders()
        {
            InitializeComponent();
        }

        private void frmPrintFactOrders_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            ReportDocument RepDoc = new ReportDocument();
            RepDoc.Load(@"..\..\repPrintFactsOrder.rpt");

            RepDoc.SetDataSource((DataTable)Program.tablequeryOrderDetail);
            RepDoc.SetParameterValue("DateFact", Program.myDateFact);
            RepDoc.SetParameterValue("CustomerName", Program.myCustomerName);
            crystalReportViewer1.ReportSource = RepDoc;
            crystalReportViewer1.Zoom(100);
        }
    }
}
