Imports FarsiLibrary.Utils

Public Class frm06

    Private Sub frm06_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim pd As PersianDate = PersianDate.Now

        toStringD.Text = pd.ToString("d")
        toStringDD.Text = pd.ToString("D")
        toStringF.Text = pd.ToString("f")
        toStringFF.Text = pd.ToString("F")
        toStringG.Text = pd.ToString("g")
        toStringGG.Text = pd.ToString("G")
        toStringM.Text = pd.ToString("m")
        toStringMM.Text = pd.ToString("M")
        toStringS.Text = pd.ToString("s")
        toStringT.Text = pd.ToString("t")
        toStringTT.Text = pd.ToString("T")

    End Sub
End Class