<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm03
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label3 = New System.Windows.Forms.Label
        Me.faDatePickerConverter1 = New FarsiLibrary.Win.Controls.FADatePickerConverter
        Me.faDatePicker2 = New FarsiLibrary.Win.Controls.FADatePicker
        Me.faDatePicker1 = New FarsiLibrary.Win.Controls.FADatePicker
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(2, 62)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(260, 19)
        Me.label3.TabIndex = 13
        Me.label3.Text = "Select 1385/04/08 : "
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'faDatePickerConverter1
        '
        Me.faDatePickerConverter1.CalendarType = FarsiLibrary.Win.Enums.CalendarTypes.English
        Me.faDatePickerConverter1.Location = New System.Drawing.Point(268, 61)
        Me.faDatePickerConverter1.Name = "faDatePickerConverter1"
        Me.faDatePickerConverter1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.faDatePickerConverter1.Size = New System.Drawing.Size(227, 20)
        Me.faDatePickerConverter1.TabIndex = 12
        Me.faDatePickerConverter1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.faDatePickerConverter1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'faDatePicker2
        '
        Me.faDatePicker2.Location = New System.Drawing.Point(268, 35)
        Me.faDatePicker2.Name = "faDatePicker2"
        Me.faDatePicker2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.faDatePicker2.Size = New System.Drawing.Size(227, 20)
        Me.faDatePicker2.TabIndex = 9
        Me.faDatePicker2.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'faDatePicker1
        '
        Me.faDatePicker1.Location = New System.Drawing.Point(268, 9)
        Me.faDatePicker1.Name = "faDatePicker1"
        Me.faDatePicker1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.faDatePicker1.Size = New System.Drawing.Size(227, 20)
        Me.faDatePicker1.TabIndex = 8
        Me.faDatePicker1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(2, 35)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(260, 19)
        Me.label2.TabIndex = 11
        Me.label2.Text = "Select 20th Of a Month (Otherwise apply default)  : "
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(2, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(260, 19)
        Me.label1.TabIndex = 10
        Me.label1.Text = "Select a date prior to year 2000 : "
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frm03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 95)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.faDatePickerConverter1)
        Me.Controls.Add(Me.faDatePicker2)
        Me.Controls.Add(Me.faDatePicker1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "frm03"
        Me.Text = "frm03"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents faDatePickerConverter1 As FarsiLibrary.Win.Controls.FADatePickerConverter
    Private WithEvents faDatePicker2 As FarsiLibrary.Win.Controls.FADatePicker
    Private WithEvents faDatePicker1 As FarsiLibrary.Win.Controls.FADatePicker
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
