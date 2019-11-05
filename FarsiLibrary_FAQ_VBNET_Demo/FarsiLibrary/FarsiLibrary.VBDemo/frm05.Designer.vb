<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm05
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm05))
        Me.lblPDMinValue = New System.Windows.Forms.Label
        Me.lblDTMinValue = New System.Windows.Forms.Label
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.lblCastFrom = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.lblCastTo = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.btnCalc = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblPDMinValue
        '
        Me.lblPDMinValue.Location = New System.Drawing.Point(321, 150)
        Me.lblPDMinValue.Name = "lblPDMinValue"
        Me.lblPDMinValue.Size = New System.Drawing.Size(198, 23)
        Me.lblPDMinValue.TabIndex = 19
        Me.lblPDMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDTMinValue
        '
        Me.lblDTMinValue.Location = New System.Drawing.Point(321, 127)
        Me.lblDTMinValue.Name = "lblDTMinValue"
        Me.lblDTMinValue.Size = New System.Drawing.Size(198, 23)
        Me.lblDTMinValue.TabIndex = 18
        Me.lblDTMinValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(15, 150)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(300, 23)
        Me.label6.TabIndex = 17
        Me.label6.Text = "Casting PersianDate.MinValue to DateTime instance : "
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(15, 127)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(300, 23)
        Me.label5.TabIndex = 16
        Me.label5.Text = "Casting DateTime.MaxValue to PersianDate instance : "
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCastFrom
        '
        Me.lblCastFrom.Location = New System.Drawing.Point(321, 104)
        Me.lblCastFrom.Name = "lblCastFrom"
        Me.lblCastFrom.Size = New System.Drawing.Size(198, 23)
        Me.lblCastFrom.TabIndex = 15
        Me.lblCastFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(15, 104)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(300, 23)
        Me.label3.TabIndex = 14
        Me.label3.Text = "Casting PersianDate.Now to DateTime instance : "
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCastTo
        '
        Me.lblCastTo.Location = New System.Drawing.Point(321, 81)
        Me.lblCastTo.Name = "lblCastTo"
        Me.lblCastTo.Size = New System.Drawing.Size(198, 23)
        Me.lblCastTo.TabIndex = 13
        Me.lblCastTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(15, 81)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(300, 23)
        Me.label2.TabIndex = 12
        Me.label2.Text = "Casting DateTime.Now to PersianDate instance :  "
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(12, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(516, 67)
        Me.label1.TabIndex = 11
        Me.label1.Text = resources.GetString("label1.Text")
        '
        'btnCalc
        '
        Me.btnCalc.Location = New System.Drawing.Point(15, 202)
        Me.btnCalc.Name = "btnCalc"
        Me.btnCalc.Size = New System.Drawing.Size(75, 23)
        Me.btnCalc.TabIndex = 10
        Me.btnCalc.Text = "Calculate"
        Me.btnCalc.UseVisualStyleBackColor = True
        '
        'frm05
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 237)
        Me.Controls.Add(Me.lblPDMinValue)
        Me.Controls.Add(Me.lblDTMinValue)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.lblCastFrom)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.lblCastTo)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnCalc)
        Me.Name = "frm05"
        Me.Text = "frm05"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblPDMinValue As System.Windows.Forms.Label
    Private WithEvents lblDTMinValue As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents lblCastFrom As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents lblCastTo As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnCalc As System.Windows.Forms.Button
End Class
