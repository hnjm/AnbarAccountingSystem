Imports System.Threading
Imports System.Globalization

Public Module Program
    <STAThread()> _
    Public Sub Main()
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("fa-ir")
        Thread.CurrentThread.CurrentCulture = New CultureInfo("fa-ir")

        Application.SetCompatibleTextRenderingDefault(True)
        Application.EnableVisualStyles()
        Application.Run(New MainForm())
    End Sub
End Module
