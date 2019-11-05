<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm01
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
        Me.btnToggleFocusRect = New System.Windows.Forms.Button
        Me.btnToggleBorder = New System.Windows.Forms.Button
        Me.btnVisualStyles = New System.Windows.Forms.Button
        Me.btnChangeTheme = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'faMonthView
        '
        Me.faMonthView.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.faMonthView.IsNull = False
        Me.faMonthView.Location = New System.Drawing.Point(12, 12)
        Me.faMonthView.Name = "faMonthView"
        Me.faMonthView.SelectedDateTime = New Date(2008, 10, 26, 16, 59, 18, 223)
        Me.faMonthView.TabIndex = 0
        '
        'btnToggleFocusRect
        '
        Me.btnToggleFocusRect.Location = New System.Drawing.Point(184, 106)
        Me.btnToggleFocusRect.Name = "btnToggleFocusRect"
        Me.btnToggleFocusRect.Size = New System.Drawing.Size(134, 25)
        Me.btnToggleFocusRect.TabIndex = 8
        Me.btnToggleFocusRect.Text = "Toggle Focus Rect"
        Me.btnToggleFocusRect.UseVisualStyleBackColor = True
        '
        'btnToggleBorder
        '
        Me.btnToggleBorder.Location = New System.Drawing.Point(184, 74)
        Me.btnToggleBorder.Name = "btnToggleBorder"
        Me.btnToggleBorder.Size = New System.Drawing.Size(134, 25)
        Me.btnToggleBorder.TabIndex = 7
        Me.btnToggleBorder.Text = "Toggle Border"
        Me.btnToggleBorder.UseVisualStyleBackColor = True
        '
        'btnVisualStyles
        '
        Me.btnVisualStyles.Location = New System.Drawing.Point(184, 43)
        Me.btnVisualStyles.Name = "btnVisualStyles"
        Me.btnVisualStyles.Size = New System.Drawing.Size(134, 25)
        Me.btnVisualStyles.TabIndex = 6
        Me.btnVisualStyles.Text = "Toggle VisualStyles"
        Me.btnVisualStyles.UseVisualStyleBackColor = True
        '
        'btnChangeTheme
        '
        Me.btnChangeTheme.Location = New System.Drawing.Point(184, 12)
        Me.btnChangeTheme.Name = "btnChangeTheme"
        Me.btnChangeTheme.Size = New System.Drawing.Size(134, 25)
        Me.btnChangeTheme.TabIndex = 5
        Me.btnChangeTheme.Text = "Change Theme"
        Me.btnChangeTheme.UseVisualStyleBackColor = True
        '
        'frm01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 187)
        Me.Controls.Add(Me.btnToggleFocusRect)
        Me.Controls.Add(Me.btnToggleBorder)
        Me.Controls.Add(Me.btnVisualStyles)
        Me.Controls.Add(Me.btnChangeTheme)
        Me.Controls.Add(Me.faMonthView)
        Me.Name = "frm01"
        Me.Text = "frm01"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents faMonthView As FarsiLibrary.Win.Controls.FAMonthView
    Private WithEvents btnToggleFocusRect As System.Windows.Forms.Button
    Private WithEvents btnToggleBorder As System.Windows.Forms.Button
    Private WithEvents btnVisualStyles As System.Windows.Forms.Button
    Private WithEvents btnChangeTheme As System.Windows.Forms.Button
End Class
