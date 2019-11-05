Imports FarsiLibrary.Win.Enums
Imports FarsiLibrary.Win
Imports System.Windows.Forms.VisualStyles

Public Class frm01

    Dim currentTheme As Integer = ThemeTypes.Office2000

    Private Sub btnChangeTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeTheme.Click
        If (currentTheme > 2) Then
            currentTheme = 0
            faMonthView.Theme = ThemeTypes.Office2000
        Else
            currentTheme = currentTheme + 1
            Dim selectedTheme As ThemeTypes = CType(currentTheme, ThemeTypes)
            faMonthView.Theme = selectedTheme
        End If
    End Sub

    Private Sub btnVisualStyles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisualStyles.Click
        If Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled Then
            Application.VisualStyleState = VisualStyleState.NoneEnabled
        Else
            Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled
        End If
    End Sub

    Private Sub btnToggleBorder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggleBorder.Click
        faMonthView.ShowBorder = Not faMonthView.ShowBorder
    End Sub

    Private Sub btnToggleFocusRect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggleFocusRect.Click
        faMonthView.ShowFocusRect = Not faMonthView.ShowFocusRect
    End Sub

    Private Sub frm01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class