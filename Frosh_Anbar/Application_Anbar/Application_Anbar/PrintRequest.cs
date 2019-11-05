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
    public partial class PrintRequest : Form
    {
        public PrintRequest()
        {
            InitializeComponent();
        }

        private void PrintRequest_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            ReportDocument RepDoc = new ReportDocument();
            RepDoc.Load(@"E:\Poroject\Frosh_Anbar\Application_Anbar\Application_Anbar\rep1.rpt");

            AnbarDataSet anbards = new AnbarDataSet();

            //AnbarDataSetTableAdapters.ViewRequestProductTableAdapter viewRP = new Application_Anbar.AnbarDataSetTableAdapters.ViewRequestProductTableAdapter();
            //viewRP.Fill(anbards.ViewRequestProduct);

            RepDoc.SetDataSource((DataTable)Program.tablequery);//(DataTable)anbards.Requests);
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            string date = pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString();
            RepDoc.SetParameterValue(0, date);

            crystalReportViewer1.ReportSource = RepDoc;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

    }
}
