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
    public partial class FrmPrint : Form
    {
        public FrmPrint()
        {
            InitializeComponent();
        }

        private void FrmPrint_Load(object sender, EventArgs e)
        {
            PersianCalendar pcalender = new PersianCalendar();
            string year, month, day, strDate;
            year = pcalender.GetYear(DateTime.Now).ToString();
            month = pcalender.GetMonth(DateTime.Now).ToString();
            day = pcalender.GetDayOfMonth(DateTime.Now).ToString();
            if (month.Length == 1) month = "0" + month;
            if (day.Length == 1) day = "0" + day;
            strDate = year + "/" + month + "/" + day;
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1];
            ReportDocument repDoc = new ReportDocument();

            if (Program.myPrintCase == 1)
            {
                repDoc.Load(@"..\..\repPrintCheque.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 2)
            {
                repDoc.Load(@"..\..\repPrintCustomerAccount.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 3)
            {
                repDoc.Load(@"..\..\repPrintCustomerAccount2.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 4)
            {
                repDoc.Load(@"..\..\repPrintSellerAccount.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 5)
            {
                repDoc.Load(@"..\..\repPrintSellerAccount2.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 6)
            {
                repDoc.Load(@"..\..\repPrintEnterDocuments.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 7)
            {
                repDoc.Load(@"..\..\repPrintOrders.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase ==8)
            {
                repDoc.Load(@"..\..\repPrintTotalPriceOrders.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 9)
            {
                repDoc.Load(@"..\..\repPrintTotalPriceOrdersDay.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
            else if (Program.myPrintCase == 10)
            {
                repDoc.Load(@"..\..\repPrintCycleProduct.rpt");

                repDoc.SetDataSource((DataTable)Program.TableQuery);
                repDoc.SetParameterValue("Date", strDate);
                crystalReportViewer1.ReportSource = repDoc;
            }
        }
    }
}
