using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_Anbar
{
    class Class1
    {
        public static Main frmmain =new Main();
        public static string strimag = "";
        public static string strcolor = "";
        public static frmLogin frmLogin = new frmLogin();
    }
    class Permision
    {
        public static bool SaveImport = false, SaveExport = false, RemoveImort = false, RemoveExport = false, LockAccountDay = false, AccountCustomer = false, AccountSeller = false, ReachDocument = false, AllowReport = false,manageUser=false;
    }
}
