<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm04
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
        Me.faMonthView = New FarsiLibrary.Win.Controls.FAMonthView
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'faMonthView
        '
        Me.faMonthView.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.faMonthView.IsNull = False
        Me.faMonthView.Location = New System.Drawing.Point(12, 48)
        Me.faMonthView.Name = "faMonthView"
        Me.faMonthView.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.faMonthView.SelectedDateTime = New Date(2008, 10, 22, 16, 34, 0, 0)
        Me.faMonthView.TabIndex = 7
        Me.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 35)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "In this sample we're custom drawing Fridays in red"
        '
        'frm04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(186, 221)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.faMonthView)
        Me.Name = "frm04"
        Me.Text = "frm04"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents faMonthView As FarsiLibrary.Win.Controls.FAMonthView
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
