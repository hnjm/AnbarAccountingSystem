<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm07
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
        Me.lblToWritten = New System.Windows.Forms.Label
        Me.btnDeductMonth = New System.Windows.Forms.Button
        Me.btnAddDays = New System.Windows.Forms.Button
        Me.btnAddYears = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        Me.btnToday = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblToWritten
        '
        Me.lblToWritten.Location = New System.Drawing.Point(12, 34)
        Me.lblToWritten.Name = "lblToWritten"
        Me.lblToWritten.Size = New System.Drawing.Size(310, 25)
        Me.lblToWritten.TabIndex = 11
        '
        'btnDeductMonth
        '
        Me.btnDeductMonth.Location = New System.Drawing.Point(15, 71)
        Me.btnDeductMonth.Name = "btnDeductMonth"
        Me.btnDeductMonth.Size = New System.Drawing.Size(307, 25)
        Me.btnDeductMonth.TabIndex = 10
        Me.btnDeductMonth.Text = "Add 2 Months to Today"
        Me.btnDeductMonth.UseVisualStyleBackColor = True
        '
        'btnAddDays
        '
        Me.btnAddDays.Location = New System.Drawing.Point(14, 102)
        Me.btnAddDays.Name = "btnAddDays"
        Me.btnAddDays.Size = New System.Drawing.Size(307, 25)
        Me.btnAddDays.TabIndex = 9
        Me.btnAddDays.Text = "Deduct 66 Days from Today"
        Me.btnAddDays.UseVisualStyleBackColor = True
        '
        'btnAddYears
        '
        Me.btnAddYears.Location = New System.Drawing.Point(14, 133)
        Me.btnAddYears.Name = "btnAddYears"
        Me.btnAddYears.Size = New System.Drawing.Size(307, 25)
        Me.btnAddYears.TabIndex = 8
        Me.btnAddYears.Text = "Add 10 Years to Today"
        Me.btnAddYears.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.Location = New System.Drawing.Point(12, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(310, 25)
        Me.lblMessage.TabIndex = 7
        '
        'btnToday
        '
        Me.btnToday.Location = New System.Drawing.Point(15, 164)
        Me.btnToday.Name = "btnToday"
        Me.btnToday.Size = New System.Drawing.Size(307, 25)
        Me.btnToday.TabIndex = 6
        Me.btnToday.Text = "Today"
        Me.btnToday.UseVisualStyleBackColor = True
        '
        'frm07
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 202)
        Me.Controls.Add(Me.lblToWritten)
        Me.Controls.Add(Me.btnDeductMonth)
        Me.Controls.Add(Me.btnAddDays)
        Me.Controls.Add(Me.btnAddYears)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnToday)
        Me.Name = "frm07"
        Me.Text = "frm07"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblToWritten As System.Windows.Forms.Label
    Private WithEvents btnDeductMonth As System.Windows.Forms.Button
    Private WithEvents btnAddDays As System.Windows.Forms.Button
    Private WithEvents btnAddYears As System.Windows.Forms.Button
    Private WithEvents lblMessage As System.Windows.Forms.Label
    Private WithEvents btnToday As System.Windows.Forms.Button
End Class
