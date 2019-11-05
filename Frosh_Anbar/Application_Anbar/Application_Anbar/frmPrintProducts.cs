using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
namespace Application_Anbar
{
    public partial class frmPrintProducts : Form
    {
        public frmPrintProducts()
        {
            InitializeComponent();
        }

        private void frmPrintProducts_Load(object sender, EventArgs e)
        {
            //Compute Date Current
            PersianCalendar pcalender = new PersianCalendar();
            string year, month, day,strDate;
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            strDate = year + "/" + month + "/" + day;
            //
            //Load Data In CrystalReportViewer
            //
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            ReportDocument repDoc = new ReportDocument();

            if (Program.myPrintProductCase == 1)
            {
                repDoc.Load(@"..\..\repPrintProducts_1.rpt");

                repDoc.SetDataSource((DataTable)Program.tablequeryproduct_1);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintProductCase == 2)
            {
                repDoc.Load(@"..\..\repPrintProducts_2.rpt");

                repDoc.SetDataSource((DataTable)Program.tablequeryproduct_2);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintProductCase == 3)
            {
                repDoc.Load(@"..\..\repPrintProducts_3.rpt");

                repDoc.SetDataSource((DataTable)Program.tablequeryproduct_3);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintProductCase == 4)
            {
                repDoc.Load(@"..\..\repPrintProducts_4.rpt");

                repDoc.SetDataSource((DataTable)Program.tablequeryproduct_4);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
        }
    }
}
