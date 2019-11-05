Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class frm04

    Private Sub faMonthView_DrawCurrentDay(ByVal sender As System.Object, ByVal e As FarsiLibrary.Win.Events.CustomDrawDayEventArgs) Handles faMonthView.DrawCurrentDay
        If e.Date.DayOfWeek = DayOfWeek.Friday Then
            Dim br1 As New SolidBrush(Color.Wheat)
            Dim br2 As New LinearGradientBrush(e.Rectangle, Color.PaleVioletRed, Color.DarkRed, 45, True)
            Dim fnt As New Font("Tahoma", 8, FontStyle.Bold)
            Dim fmt As New StringFormat
            Dim dayNo As String = e.Day

            fmt.Alignment = StringAlignment.Center
            fmt.LineAlignment = StringAlignment.Center

            e.Graphics.FillRectangle(br2, e.Rectangle)
            e.Graphics.DrawString(dayNo, fnt, br1, e.Rectangle, fmt)

            br1.Dispose()
            br2.Dispose()
            fnt.Dispose()
            fmt.Dispose()

            e.Handled = True
        End If
    End Sub
End Class