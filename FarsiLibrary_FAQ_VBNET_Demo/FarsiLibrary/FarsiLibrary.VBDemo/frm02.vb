Imports FarsiLibrary.Resources
Imports System.Globalization

Public Class frm02

    Private Sub btnFarsi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFarsi.Click
        faMonthView.DefaultCalendar = faMonthView.PersianCalendar
        faMonthView.DefaultCulture = faMonthView.PersianCulture
    End Sub

    Private Sub btnArabic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArabic.Click
        faMonthView.DefaultCalendar = faMonthView.HijriCalendar
        faMonthView.DefaultCulture = faMonthView.ArabicCulture
    End Sub

    Private Sub btnInvariant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvariant.Click
        faMonthView.DefaultCalendar = faMonthView.InvariantCalendar
        faMonthView.DefaultCulture = faMonthView.InvariantCulture
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        faMonthView.DefaultCalendar = Nothing
        faMonthView.DefaultCulture = Nothing
    End Sub

    Private Sub btnCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustom.Click
        FALocalizeManager.Instance.CustomCulture = New CultureInfo("es-ES")
        FALocalizeManager.Instance.CustomLocalizer = New ESLocalizer()

        faMonthView.SelectedDateTime = DateTime.Now
    End Sub

    Private Sub faDatePicker1_SelectedDateTimeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles faDatePicker1.SelectedDateTimeChanged

    End Sub

    Private Sub frm02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class