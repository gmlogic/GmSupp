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
        Me.SuspendLayout()
        '
        'txtConnection
        '
        Me.txtConnection.Location = New System.Drawing.Point(12, 12)
        Me.txtConnection.Size = New System.Drawing.Size(760, 20)
        '
        'cmbSourceType
        '
        Me.cmbSourceType.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbSourceType.Items.AddRange(New Object() {"Table", "View", "Stored Procedure"})
        Me.cmbSourceType.Location = New System.Drawing.Point(12, 45)
        Me.cmbSourceType.Size = New System.Drawing.Size(150, 21)
        '
        'txtSourceName
        '
        Me.txtSourceName.Location = New System.Drawing.Point(170, 45)
        Me.txtSourceName.Size = New System.Drawing.Size(250, 20)
        '
        'txtClassName
        '
        Me.txtClassName.Location = New System.Drawing.Point(430, 45)
        Me.txtClassName.Size = New System.Drawing.Size(200, 20)
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(640, 43)
        Me.btnGenerate.Size = New System.Drawing.Size(60, 23)
        Me.btnGenerate.Text = "Generate"
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(710, 43)
        Me.btnCopy.Size = New System.Drawing.Size(60, 23)
        Me.btnCopy.Text = "Copy"
        '
        'txtOutput
        '
        Me.txtOutput.Font = New System.Drawing.Font("Consolas", 10.0!)
        Me.txtOutput.Location = New System.Drawing.Point(12, 80)
        Me.txtOutput.Multiline = True
        Me.txtOutput.ScrollBars = ScrollBars.Both
        Me.txtOutput.Size = New System.Drawing.Size(760, 370)
        Me.txtOutput.WordWrap = False
        '
        'FrmDtoGenerator
        '
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.AddRange(New Control() {
            Me.txtConnection, Me.cmbSourceType, Me.txtSourceName,
            Me.txtClassName, Me.btnGenerate, Me.btnCopy, Me.txtOutput
        })
        Me.Text = "DTO Generator (Table / View / SP)"
        Me.StartPosition = FormStartPosition.CenterScreen
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

End Class
