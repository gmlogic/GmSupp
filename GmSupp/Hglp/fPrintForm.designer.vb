<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fPrintForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fPrintForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbForms = New System.Windows.Forms.ComboBox()
        Me.cmbPrinters = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnSaveFile = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BtnTestConnection = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(88, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Print Form :"
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(183, 111)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(76, 25)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "ΟΚ"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(279, 111)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 25)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(88, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Printer / File Type :"
        '
        'cmbForms
        '
        Me.cmbForms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForms.FormattingEnabled = True
        Me.cmbForms.Location = New System.Drawing.Point(195, 6)
        Me.cmbForms.Name = "cmbForms"
        Me.cmbForms.Size = New System.Drawing.Size(403, 21)
        Me.cmbForms.TabIndex = 7
        '
        'cmbPrinters
        '
        Me.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinters.FormattingEnabled = True
        Me.cmbPrinters.Location = New System.Drawing.Point(195, 39)
        Me.cmbPrinters.Name = "cmbPrinters"
        Me.cmbPrinters.Size = New System.Drawing.Size(267, 21)
        Me.cmbPrinters.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(88, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "File Name :"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(195, 71)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(151, 20)
        Me.txtFileName.TabIndex = 9
        '
        'btnSaveFile
        '
        Me.btnSaveFile.Location = New System.Drawing.Point(346, 71)
        Me.btnSaveFile.Name = "btnSaveFile"
        Me.btnSaveFile.Size = New System.Drawing.Size(24, 20)
        Me.btnSaveFile.TabIndex = 10
        Me.btnSaveFile.Text = "..."
        Me.btnSaveFile.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.GmSupp.My.Resources.Resources.Printer_Folder
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(70, 59)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'BtnTestConnection
        '
        Me.BtnTestConnection.Image = CType(resources.GetObject("BtnTestConnection.Image"), System.Drawing.Image)
        Me.BtnTestConnection.Location = New System.Drawing.Point(603, 114)
        Me.BtnTestConnection.Name = "BtnTestConnection"
        Me.BtnTestConnection.Size = New System.Drawing.Size(23, 22)
        Me.BtnTestConnection.TabIndex = 66
        Me.BtnTestConnection.UseVisualStyleBackColor = True
        '
        'fPrintForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(638, 148)
        Me.Controls.Add(Me.BtnTestConnection)
        Me.Controls.Add(Me.btnSaveFile)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.cmbPrinters)
        Me.Controls.Add(Me.cmbForms)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fPrintForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SoftOne Print Form"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbForms As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveFile As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BtnTestConnection As Button
End Class
