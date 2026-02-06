<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDtoGenerator
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtConnection = New System.Windows.Forms.TextBox()
        Me.cmbSourceType = New System.Windows.Forms.ComboBox()
        Me.txtSourceName = New System.Windows.Forms.TextBox()
        Me.txtClassName = New System.Windows.Forms.TextBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.BtnCopyToNas = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtConnection
        '
        Me.txtConnection.Location = New System.Drawing.Point(12, 12)
        Me.txtConnection.Name = "txtConnection"
        Me.txtConnection.Size = New System.Drawing.Size(760, 20)
        Me.txtConnection.TabIndex = 0
        '
        'cmbSourceType
        '
        Me.cmbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSourceType.Items.AddRange(New Object() {"Table", "View", "Stored Procedure"})
        Me.cmbSourceType.Location = New System.Drawing.Point(12, 45)
        Me.cmbSourceType.Name = "cmbSourceType"
        Me.cmbSourceType.Size = New System.Drawing.Size(150, 21)
        Me.cmbSourceType.TabIndex = 1
        '
        'txtSourceName
        '
        Me.txtSourceName.Location = New System.Drawing.Point(170, 45)
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.Size = New System.Drawing.Size(250, 20)
        Me.txtSourceName.TabIndex = 2
        '
        'txtClassName
        '
        Me.txtClassName.Location = New System.Drawing.Point(430, 45)
        Me.txtClassName.Name = "txtClassName"
        Me.txtClassName.Size = New System.Drawing.Size(200, 20)
        Me.txtClassName.TabIndex = 3
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(640, 43)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(60, 23)
        Me.btnGenerate.TabIndex = 4
        Me.btnGenerate.Text = "Generate"
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(710, 43)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(60, 23)
        Me.btnCopy.TabIndex = 5
        Me.btnCopy.Text = "Copy"
        '
        'txtOutput
        '
        Me.txtOutput.Font = New System.Drawing.Font("Consolas", 10.0!)
        Me.txtOutput.Location = New System.Drawing.Point(12, 80)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOutput.Size = New System.Drawing.Size(760, 370)
        Me.txtOutput.TabIndex = 6
        Me.txtOutput.WordWrap = False
        '
        'BtnCopyToNas
        '
        Me.BtnCopyToNas.Location = New System.Drawing.Point(776, 42)
        Me.BtnCopyToNas.Name = "BtnCopyToNas"
        Me.BtnCopyToNas.Size = New System.Drawing.Size(75, 23)
        Me.BtnCopyToNas.TabIndex = 7
        Me.BtnCopyToNas.Text = "CopyToNas"
        Me.BtnCopyToNas.UseVisualStyleBackColor = True
        '
        'FrmDtoGenerator
        '
        Me.ClientSize = New System.Drawing.Size(856, 461)
        Me.Controls.Add(Me.BtnCopyToNas)
        Me.Controls.Add(Me.txtConnection)
        Me.Controls.Add(Me.cmbSourceType)
        Me.Controls.Add(Me.txtSourceName)
        Me.Controls.Add(Me.txtClassName)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtOutput)
        Me.Name = "FrmDtoGenerator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DTO Generator (Table / View / SP)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtConnection As TextBox
    Friend WithEvents cmbSourceType As ComboBox
    Friend WithEvents txtSourceName As TextBox
    Friend WithEvents txtClassName As TextBox
    Friend WithEvents btnGenerate As Button
    Friend WithEvents btnCopy As Button
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents BtnCopyToNas As Button
End Class
