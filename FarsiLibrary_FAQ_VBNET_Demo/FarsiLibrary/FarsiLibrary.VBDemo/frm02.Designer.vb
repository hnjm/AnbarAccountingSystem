<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm02
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
        Me.faDatePicker1 = New FarsiLibrary.Win.Controls.FADatePicker
        Me.faMonthView = New FarsiLibrary.Win.Controls.FAMonthView
        Me.btnDefault = New System.Windows.Forms.Button
        Me.btnInvariant = New System.Windows.Forms.Button
        Me.btnArabic = New System.Windows.Forms.Button
        Me.btnFarsi = New System.Windows.Forms.Button
        Me.btnCustom = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'faDatePicker1
        '
        Me.faDatePicker1.Location = New System.Drawing.Point(12, 8)
        Me.faDatePicker1.Name = "faDatePicker1"
        Me.faDatePicker1.Size = New System.Drawing.Size(166, 20)
        Me.faDatePicker1.TabIndex = 7
        Me.faDatePicker1.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'faMonthView
        '
        Me.faMonthView.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.faMonthView.IsNull = False
        Me.faMonthView.Location = New System.Drawing.Point(12, 34)
        Me.faMonthView.Name = "faMonthView"
        Me.faMonthView.SelectedDateTime = New Date(2006, 5, 24, 16, 34, 43, 67)
        Me.faMonthView.TabIndex = 6
        Me.faMonthView.Theme = FarsiLibrary.Win.Enums.ThemeTypes.WindowsXP
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(185, 171)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(174, 25)
        Me.btnDefault.TabIndex = 11
        Me.btnDefault.Text = "System Default Culture"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'btnInvariant
        '
        Me.btnInvariant.Location = New System.Drawing.Point(185, 70)
        Me.btnInvariant.Name = "btnInvariant"
        Me.btnInvariant.Size = New System.Drawing.Size(174, 25)
        Me.btnInvariant.TabIndex = 10
        Me.btnInvariant.Text = "Invariant Culture"
        Me.btnInvariant.UseVisualStyleBackColor = True
        '
        'btnArabic
        '
        Me.btnArabic.Location = New System.Drawing.Point(185, 39)
        Me.btnArabic.Name = "btnArabic"
        Me.btnArabic.Size = New System.Drawing.Size(174, 25)
        Me.btnArabic.TabIndex = 9
        Me.btnArabic.Text = "Arabic Culture"
        Me.btnArabic.UseVisualStyleBackColor = True
        '
        'btnFarsi
        '
        Me.btnFarsi.Location = New System.Drawing.Point(185, 8)
        Me.btnFarsi.Name = "btnFarsi"
        Me.btnFarsi.Size = New System.Drawing.Size(174, 25)
        Me.btnFarsi.TabIndex = 8
        Me.btnFarsi.Text = "Farsi Culture"
        Me.btnFarsi.UseVisualStyleBackColor = True
        '
        'btnCustom
        '
        Me.btnCustom.Location = New System.Drawing.Point(185, 101)
        Me.btnCustom.Name = "btnCustom"
        Me.btnCustom.Size = New System.Drawing.Size(174, 25)
        Me.btnCustom.TabIndex = 12
        Me.btnCustom.Text = "Custom Culture"
        Me.btnCustom.UseVisualStyleBackColor = True
        '
        'frm02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 208)
        Me.Controls.Add(Me.btnCustom)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.btnInvariant)
        Me.Controls.Add(Me.btnArabic)
        Me.Controls.Add(Me.btnFarsi)
        Me.Controls.Add(Me.faDatePicker1)
        Me.Controls.Add(Me.faMonthView)
        Me.Name = "frm02"
        Me.Text = "frm02"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents faDatePicker1 As FarsiLibrary.Win.Controls.FADatePicker
    Private WithEvents faMonthView As FarsiLibrary.Win.Controls.FAMonthView
    Private WithEvents btnDefault As System.Windows.Forms.Button
    Private WithEvents btnInvariant As System.Windows.Forms.Button
    Private WithEvents btnArabic As System.Windows.Forms.Button
    Private WithEvents btnFarsi As System.Windows.Forms.Button
    Private WithEvents btnCustom As System.Windows.Forms.Button
End Class
