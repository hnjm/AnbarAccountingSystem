using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Application_Anbar
{
    static class Program
    {
        public static int mysearchcategory=0;
        public static int mysearchproduct = 0;
        public static int mysearchcustomer = 0;
        public static int mysearchseller = 0;
        public static int mysearchCheque = 0;
        public static int myEnterDocID = 0;
        public static int myOrderID = 0;
        public static int myPrintProductCase = 1;
        public static int myPrintCase = 0;
        public static string myDateFact = "";
        public static string mySellerName = "";
        public static string myCustomerName = "";
        public static bool boolProductName = true, boolCategoryCode = true, boolcategoryName = true,boolUnit=true,boolBuyprice=true,boolsellprice=true,boolst_mojodi=true,boolmojodi=true,boolDiscountable=true;


        public static System.Data.DataTable tablequery = new System.Data.DataTable();

        public static System.Data.DataTable tablequeryproduct_1 = new System.Data.DataTable();
        public static System.Data.DataTable tablequeryproduct_2 = new System.Data.DataTable();
        public static System.Data.DataTable tablequeryproduct_3 = new System.Data.DataTable();
        public static System.Data.DataTable tablequeryproduct_4 = new System.Data.DataTable();

        public static System.Data.DataTable tablequeryEnterDetail = new System.Data.DataTable();
        public static System.Data.DataTable tablequeryOrderDetail = new System.Data.DataTable();
        public static System.Data.DataTable TableQuery = new System.Data.DataTable();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Class1.frmLogin);
          
        }
    }
}