Imports FarsiLibrary.Utils

Public Class frm07

    Private Sub btnDeductMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeductMonth.Click
        Dim calendar As New PersianCalendar()
        Dim pd As PersianDate = New PersianDate(calendar.AddMonths(DateTime.Now, 2))
        lblMessage.Text = pd.ToString()
        lblToWritten.Text = pd.ToWritten()
    End Sub

    Private Sub btnAddDays_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDays.Click
        Dim calendar As New PersianCalendar()
        Dim pd As New PersianDate(calendar.AddDays(DateTime.Now, -66))
        lblMessage.Text = pd.ToString()
        lblToWritten.Text = pd.ToWritten()
    End Sub

    Private Sub btnAddYears_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddYears.Click
        Dim calendar As New PersianCalendar()
        Dim pd = New PersianDate(calendar.AddYears(DateTime.Now, 10))
        lblMessage.Text = pd.ToString()
        lblToWritten.Text = pd.ToWritten()
    End Sub
End Class