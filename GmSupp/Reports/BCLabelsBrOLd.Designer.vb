<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BCLabelsBrOLd
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtLabelNo = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me._Frame2_2 = New System.Windows.Forms.GroupBox()
        Me.radioBtnA7 = New System.Windows.Forms.RadioButton()
        Me.radioBtnA6 = New System.Windows.Forms.RadioButton()
        Me._Frame2_2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(141, 151)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtLabelNo
        '
        Me.txtLabelNo.Location = New System.Drawing.Point(27, 28)
        Me.txtLabelNo.Name = "txtLabelNo"
        Me.txtLabelNo.Size = New System.Drawing.Size(100, 20)
        Me.txtLabelNo.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(141, 197)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        '_Frame2_2
        '
        Me._Frame2_2.BackColor = System.Drawing.SystemColors.Control
        Me._Frame2_2.Controls.Add(Me.radioBtnA7)
        Me._Frame2_2.Controls.Add(Me.radioBtnA6)
        Me._Frame2_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me._Frame2_2.ForeColor = System.Drawing.Color.Blue
        Me._Frame2_2.Location = New System.Drawing.Point(299, 29)
        Me._Frame2_2.Name = "_Frame2_2"
        Me._Frame2_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Frame2_2.Size = New System.Drawing.Size(70, 62)
        Me._Frame2_2.TabIndex = 51
        Me._Frame2_2.TabStop = False
        '
        'radioBtnA7
        '
        Me.radioBtnA7.BackColor = System.Drawing.Color.Blue
        Me.radioBtnA7.ForeColor = System.Drawing.Color.White
        Me.radioBtnA7.Location = New System.Drawing.Point(6, 31)
        Me.radioBtnA7.Name = "radioBtnA7"
        Me.radioBtnA7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.radioBtnA7.Size = New System.Drawing.Size(57, 21)
        Me.radioBtnA7.TabIndex = 5
        Me.radioBtnA7.Text = "A7"
        Me.radioBtnA7.UseVisualStyleBackColor = False
        Me.radioBtnA7.Visible = False
        '
        'radioBtnA6
        '
        Me.radioBtnA6.BackColor = System.Drawing.Color.Blue
        Me.radioBtnA6.Checked = True
        Me.radioBtnA6.ForeColor = System.Drawing.Color.White
        Me.radioBtnA6.Location = New System.Drawing.Point(6, 10)
        Me.radioBtnA6.Name = "radioBtnA6"
        Me.radioBtnA6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.radioBtnA6.Size = New System.Drawing.Size(57, 21)
        Me.radioBtnA6.TabIndex = 3
        Me.radioBtnA6.TabStop = True
        Me.radioBtnA6.Text = "A6"
        Me.radioBtnA6.UseVisualStyleBackColor = False
        '
        'BCLabelsBr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me._Frame2_2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtLabelNo)
        Me.Controls.Add(Me.Button1)
        Me.Name = "BCLabelsBr"
        Me.Text = "BCLabelsBr"
        Me._Frame2_2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents txtLabelNo As TextBox
    Friend WithEvents Button2 As Button
    Public WithEvents _Frame2_2 As GroupBox
    Public WithEvents radioBtnA7 As RadioButton
    Public WithEvents radioBtnA6 As RadioButton
End Class
