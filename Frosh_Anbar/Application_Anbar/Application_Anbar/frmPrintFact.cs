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
    public partial class frmPrintFact : Form
    {
        public frmPrintFact()
        {
            InitializeComponent();
        }

        private void frmPrintFact_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            ReportDocument RepDoc = new ReportDocument();
            RepDoc.Load(@"..\..\repPrintFacts.rpt");
            
            RepDoc.SetDataSource((DataTable)Program.tablequeryEnterDetail);
            RepDoc.SetParameterValue("DateFact", Program.myDateFact);
            RepDoc.SetParameterValue("SellerName", Program.mySellerName);
            crystalReportViewer1.ReportSource = RepDoc;
        }
    }
}
