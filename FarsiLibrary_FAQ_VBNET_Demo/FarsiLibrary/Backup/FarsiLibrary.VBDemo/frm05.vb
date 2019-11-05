Imports FarsiLibrary.Utils

Public Class frm05

    Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        lblCastTo.Text = CType(DateTime.Now, PersianDate).ToString("G")
        lblCastFrom.Text = CType(PersianDate.Now, DateTime).ToString("G")

        lblDTMinValue.Text = CType(DateTime.MaxValue, PersianDate).ToString("G")
        lblPDMinValue.Text = CType(PersianDate.MinValue, DateTime).ToString("G")
    End Sub
End Class