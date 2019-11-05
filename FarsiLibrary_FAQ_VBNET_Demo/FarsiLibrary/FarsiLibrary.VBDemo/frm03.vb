Imports FarsiLibrary.Utils

Public Class frm03

    Private Sub faDatePicker1_SelectedDateTimeChanging(ByVal sender As System.Object, ByVal e As FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs) Handles faDatePicker1.SelectedDateTimeChanging
        If e.NewValue.Year > 2000 And Not faDatePicker1.IsNull Then
            e.Message = "This is a custom error message."
        End If
    End Sub

    Private Sub faDatePicker2_SelectedDateTimeChanging(ByVal sender As System.Object, ByVal e As FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs) Handles faDatePicker2.SelectedDateTimeChanging
        Dim pd As PersianDate = e.NewValue

        If pd.Day <> 20 And Not faDatePicker2.IsNull Then
            e.Message = "Invalid date. Default Date is applied."
            e.NewValue = New DateTime(2010, 1, 20, 0, 0, 0)
        End If
    End Sub

    Private Sub faDatePickerConverter1_SelectedDateTimeChanging(ByVal sender As System.Object, ByVal e As FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs) Handles faDatePickerConverter1.SelectedDateTimeChanging
        Dim pd As PersianDate = e.NewValue

        If pd.Day <> 8 Or pd.Month <> 4 Or pd.Year <> 1385 Then
            e.Cancel = True
        End If
    End Sub
End Class