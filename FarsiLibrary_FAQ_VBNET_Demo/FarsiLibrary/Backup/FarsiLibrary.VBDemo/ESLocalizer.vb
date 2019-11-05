Imports FarsiLibrary.Resources

Public Class ESLocalizer
    Inherits ENLocalizer

    Public Overrides Function GetLocalizedString(ByVal id As Resources.StringID) As String
        Select Case id
            Case StringID.Validation_NullText
                Return "<Ningun fecha esta seleccionada>"
            Case StringID.FAMonthView_None
                Return "Nada"
            Case StringID.FAMonthView_Today
                Return "¡Hoy!"
        End Select

        Return MyBase.GetLocalizedString(id)
    End Function
End Class
