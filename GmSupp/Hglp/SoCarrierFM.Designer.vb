<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SoCarrierFM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SoCarrierFM))
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.NAMEComboBox = New System.Windows.Forms.ComboBox()
        Me.txtBoxTRUCKSNO = New System.Windows.Forms.TextBox()
        Me.txtBoxccCSHIPVALUE = New System.Windows.Forms.TextBox()
        Me.lblNAME = New System.Windows.Forms.Label()
        Me.lblTRUCKSNO = New System.Windows.Forms.Label()
        Me.lblShipmentValue = New System.Windows.Forms.Label()
        Me.chkBoxccCADR = New System.Windows.Forms.CheckBox()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSTxtSERIES = New System.Windows.Forms.ToolStripTextBox()
        Me.TlSBtnSERIES = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripConvert = New System.Windows.Forms.ToolStrip()
        Me.chkBoxccCLocked = New System.Windows.Forms.CheckBox()
        Me.ToolStripConvert.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(243, 87)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(94, 23)
        Me.Cancel.TabIndex = 7
        Me.Cancel.Text = "&Cancel"
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(140, 87)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(94, 23)
        Me.OK.TabIndex = 6
        Me.OK.Text = "&OK"
        '
        'NAMEComboBox
        '
        Me.NAMEComboBox.FormattingEnabled = True
        Me.NAMEComboBox.Location = New System.Drawing.Point(113, 9)
        Me.NAMEComboBox.Name = "NAMEComboBox"
        Me.NAMEComboBox.Size = New System.Drawing.Size(224, 21)
        Me.NAMEComboBox.TabIndex = 9
        '
        'txtBoxTRUCKSNO
        '
        Me.txtBoxTRUCKSNO.Location = New System.Drawing.Point(113, 55)
        Me.txtBoxTRUCKSNO.Name = "txtBoxTRUCKSNO"
        Me.txtBoxTRUCKSNO.Size = New System.Drawing.Size(131, 20)
        Me.txtBoxTRUCKSNO.TabIndex = 41
        '
        'txtBoxccCSHIPVALUE
        '
        Me.txtBoxccCSHIPVALUE.Location = New System.Drawing.Point(113, 35)
        Me.txtBoxccCSHIPVALUE.Name = "txtBoxccCSHIPVALUE"
        Me.txtBoxccCSHIPVALUE.Size = New System.Drawing.Size(64, 20)
        Me.txtBoxccCSHIPVALUE.TabIndex = 42
        '
        'lblNAME
        '
        Me.lblNAME.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNAME.ForeColor = System.Drawing.Color.Blue
        Me.lblNAME.Location = New System.Drawing.Point(12, 9)
        Me.lblNAME.Name = "lblNAME"
        Me.lblNAME.Size = New System.Drawing.Size(95, 20)
        Me.lblNAME.TabIndex = 38
        Me.lblNAME.Text = "Μεταφορέας:"
        Me.lblNAME.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTRUCKSNO
        '
        Me.lblTRUCKSNO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTRUCKSNO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTRUCKSNO.ForeColor = System.Drawing.Color.Blue
        Me.lblTRUCKSNO.Location = New System.Drawing.Point(12, 54)
        Me.lblTRUCKSNO.Name = "lblTRUCKSNO"
        Me.lblTRUCKSNO.Size = New System.Drawing.Size(95, 20)
        Me.lblTRUCKSNO.TabIndex = 39
        Me.lblTRUCKSNO.Text = "Πινακίδα:"
        Me.lblTRUCKSNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblShipmentValue
        '
        Me.lblShipmentValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShipmentValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblShipmentValue.ForeColor = System.Drawing.Color.Blue
        Me.lblShipmentValue.Location = New System.Drawing.Point(12, 34)
        Me.lblShipmentValue.Name = "lblShipmentValue"
        Me.lblShipmentValue.Size = New System.Drawing.Size(95, 20)
        Me.lblShipmentValue.TabIndex = 40
        Me.lblShipmentValue.Text = "Κόμιστρο:"
        Me.lblShipmentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkBoxccCADR
        '
        Me.chkBoxccCADR.AutoSize = True
        Me.chkBoxccCADR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.chkBoxccCADR.ForeColor = System.Drawing.Color.Blue
        Me.chkBoxccCADR.Location = New System.Drawing.Point(285, 53)
        Me.chkBoxccCADR.Name = "chkBoxccCADR"
        Me.chkBoxccCADR.Size = New System.Drawing.Size(52, 17)
        Me.chkBoxccCADR.TabIndex = 43
        Me.chkBoxccCADR.Text = "ADR"
        Me.chkBoxccCADR.UseVisualStyleBackColor = True
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel4.ForeColor = System.Drawing.Color.Red
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(122, 22)
        Me.ToolStripLabel4.Text = "Μετασχ/μός σε Σειρά:"
        Me.ToolStripLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(6, 25)
        '
        'TlSTxtSERIES
        '
        Me.TlSTxtSERIES.Name = "TlSTxtSERIES"
        Me.TlSTxtSERIES.Size = New System.Drawing.Size(150, 25)
        '
        'TlSBtnSERIES
        '
        Me.TlSBtnSERIES.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnSERIES.Image = CType(resources.GetObject("TlSBtnSERIES.Image"), System.Drawing.Image)
        Me.TlSBtnSERIES.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnSERIES.Name = "TlSBtnSERIES"
        Me.TlSBtnSERIES.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnSERIES.Tag = ""
        Me.TlSBtnSERIES.Text = "&Open"
        Me.TlSBtnSERIES.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'ToolStripConvert
        '
        Me.ToolStripConvert.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripConvert.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.ToolStripConvert.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel4, Me.ToolStripSeparator15, Me.TlSTxtSERIES, Me.TlSBtnSERIES})
        Me.ToolStripConvert.Location = New System.Drawing.Point(9, 9)
        Me.ToolStripConvert.Name = "ToolStripConvert"
        Me.ToolStripConvert.Size = New System.Drawing.Size(315, 25)
        Me.ToolStripConvert.TabIndex = 53
        '
        'chkBoxccCLocked
        '
        Me.chkBoxccCLocked.AutoSize = True
        Me.chkBoxccCLocked.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.chkBoxccCLocked.ForeColor = System.Drawing.Color.Blue
        Me.chkBoxccCLocked.Location = New System.Drawing.Point(9, 113)
        Me.chkBoxccCLocked.Name = "chkBoxccCLocked"
        Me.chkBoxccCLocked.Size = New System.Drawing.Size(68, 17)
        Me.chkBoxccCLocked.TabIndex = 54
        Me.chkBoxccCLocked.Text = "Locked"
        Me.chkBoxccCLocked.UseVisualStyleBackColor = True
        '
        'SoCarrierFM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(355, 132)
        Me.Controls.Add(Me.chkBoxccCLocked)
        Me.Controls.Add(Me.ToolStripConvert)
        Me.Controls.Add(Me.chkBoxccCADR)
        Me.Controls.Add(Me.lblShipmentValue)
        Me.Controls.Add(Me.lblTRUCKSNO)
        Me.Controls.Add(Me.lblNAME)
        Me.Controls.Add(Me.txtBoxccCSHIPVALUE)
        Me.Controls.Add(Me.txtBoxTRUCKSNO)
        Me.Controls.Add(Me.NAMEComboBox)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Name = "SoCarrierFM"
        Me.ToolStripConvert.ResumeLayout(False)
        Me.ToolStripConvert.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Cancel As Button
    Friend WithEvents OK As Button
    Friend WithEvents NAMEComboBox As ComboBox
    Friend WithEvents txtBoxTRUCKSNO As TextBox
    Friend WithEvents txtBoxccCSHIPVALUE As TextBox
    Friend WithEvents lblNAME As Label
    Friend WithEvents lblTRUCKSNO As Label
    Friend WithEvents lblShipmentValue As Label
    Friend WithEvents chkBoxccCADR As CheckBox
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents TlSTxtSERIES As ToolStripTextBox
    Friend WithEvents TlSBtnSERIES As ToolStripButton
    Friend WithEvents ToolStripConvert As ToolStrip
    Friend WithEvents chkBoxccCLocked As CheckBox
End Class
