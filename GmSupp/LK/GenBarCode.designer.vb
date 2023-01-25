<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GenBarCode
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GenBarCode))
        Me.label10 = New System.Windows.Forms.Label()
        Me.cbRotateFlip = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnSaveXML = New System.Windows.Forms.Button()
        Me.lblLabelLocation = New System.Windows.Forms.Label()
        Me.btnLoadXML = New System.Windows.Forms.Button()
        Me.label8 = New System.Windows.Forms.Label()
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.PageSetupDialogButton = New System.Windows.Forms.Button()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.textBoxAspectRatio = New System.Windows.Forms.TextBox()
        Me.textBoxBarWidth = New System.Windows.Forms.TextBox()
        Me.label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.PrintPreviewButton = New System.Windows.Forms.Button()
        Me.btnEncode = New System.Windows.Forms.Button()
        Me.txtHeight = New System.Windows.Forms.TextBox()
        Me.cbLabelLocation = New System.Windows.Forms.ComboBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtEncoded = New System.Windows.Forms.TextBox()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.btnBackColor = New System.Windows.Forms.Button()
        Me.cbBarcodeAlign = New System.Windows.Forms.ComboBox()
        Me.btnForeColor = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.lblEncodingTime = New System.Windows.Forms.Label()
        Me.chkGenerateLabel = New System.Windows.Forms.CheckBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.cbEncodeType = New System.Windows.Forms.ComboBox()
        Me.txtData = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.barcodeGroupBox = New System.Windows.Forms.GroupBox()
        Me.tslblCredits = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblLibraryVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslEncodedType = New System.Windows.Forms.ToolStripStatusLabel()
        Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.GmPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Location = New System.Drawing.Point(1, 102)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(39, 13)
        Me.label10.TabIndex = 53
        Me.label10.Text = "Rotate"
        '
        'cbRotateFlip
        '
        Me.cbRotateFlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRotateFlip.FormattingEnabled = True
        Me.cbRotateFlip.Items.AddRange(New Object() {"Center", "Left", "Right"})
        Me.cbRotateFlip.Location = New System.Drawing.Point(4, 118)
        Me.cbRotateFlip.Name = "cbRotateFlip"
        Me.cbRotateFlip.Size = New System.Drawing.Size(108, 21)
        Me.cbRotateFlip.TabIndex = 52
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(71, 285)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(61, 52)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "&Save As"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnSaveXML
        '
        Me.btnSaveXML.Location = New System.Drawing.Point(138, 285)
        Me.btnSaveXML.Name = "btnSaveXML"
        Me.btnSaveXML.Size = New System.Drawing.Size(76, 23)
        Me.btnSaveXML.TabIndex = 46
        Me.btnSaveXML.Text = "Save &XML"
        Me.btnSaveXML.UseVisualStyleBackColor = True
        '
        'lblLabelLocation
        '
        Me.lblLabelLocation.AutoSize = True
        Me.lblLabelLocation.Location = New System.Drawing.Point(121, 146)
        Me.lblLabelLocation.Name = "lblLabelLocation"
        Me.lblLabelLocation.Size = New System.Drawing.Size(77, 13)
        Me.lblLabelLocation.TabIndex = 48
        Me.lblLabelLocation.Text = "Label Location"
        '
        'btnLoadXML
        '
        Me.btnLoadXML.Location = New System.Drawing.Point(138, 314)
        Me.btnLoadXML.Name = "btnLoadXML"
        Me.btnLoadXML.Size = New System.Drawing.Size(77, 23)
        Me.btnLoadXML.TabIndex = 47
        Me.btnLoadXML.Text = "Load XML"
        Me.btnLoadXML.UseVisualStyleBackColor = True
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(121, 102)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(53, 13)
        Me.label8.TabIndex = 50
        Me.label8.Text = "Alignment"
        '
        'splitContainer1
        '
        Me.splitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.groupBox1)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.barcodeGroupBox)
        Me.splitContainer1.Size = New System.Drawing.Size(1015, 572)
        Me.splitContainer1.SplitterDistance = 218
        Me.splitContainer1.TabIndex = 39
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.PageSetupDialogButton)
        Me.groupBox1.Controls.Add(Me.textBox1)
        Me.groupBox1.Controls.Add(Me.textBoxAspectRatio)
        Me.groupBox1.Controls.Add(Me.textBoxBarWidth)
        Me.groupBox1.Controls.Add(Me.label13)
        Me.groupBox1.Controls.Add(Me.Label14)
        Me.groupBox1.Controls.Add(Me.PrintPreviewButton)
        Me.groupBox1.Controls.Add(Me.label10)
        Me.groupBox1.Controls.Add(Me.cbRotateFlip)
        Me.groupBox1.Controls.Add(Me.btnSave)
        Me.groupBox1.Controls.Add(Me.btnSaveXML)
        Me.groupBox1.Controls.Add(Me.lblLabelLocation)
        Me.groupBox1.Controls.Add(Me.btnLoadXML)
        Me.groupBox1.Controls.Add(Me.label8)
        Me.groupBox1.Controls.Add(Me.btnEncode)
        Me.groupBox1.Controls.Add(Me.txtHeight)
        Me.groupBox1.Controls.Add(Me.cbLabelLocation)
        Me.groupBox1.Controls.Add(Me.label4)
        Me.groupBox1.Controls.Add(Me.label6)
        Me.groupBox1.Controls.Add(Me.txtEncoded)
        Me.groupBox1.Controls.Add(Me.txtWidth)
        Me.groupBox1.Controls.Add(Me.label7)
        Me.groupBox1.Controls.Add(Me.label5)
        Me.groupBox1.Controls.Add(Me.btnBackColor)
        Me.groupBox1.Controls.Add(Me.cbBarcodeAlign)
        Me.groupBox1.Controls.Add(Me.btnForeColor)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Controls.Add(Me.lblEncodingTime)
        Me.groupBox1.Controls.Add(Me.chkGenerateLabel)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.cbEncodeType)
        Me.groupBox1.Controls.Add(Me.txtData)
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.label9)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox1.Location = New System.Drawing.Point(0, 0)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(218, 572)
        Me.groupBox1.TabIndex = 35
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "User Information"
        '
        'PageSetupDialogButton
        '
        Me.PageSetupDialogButton.Location = New System.Drawing.Point(7, 389)
        Me.PageSetupDialogButton.Name = "PageSetupDialogButton"
        Me.PageSetupDialogButton.Size = New System.Drawing.Size(75, 23)
        Me.PageSetupDialogButton.TabIndex = 84
        Me.PageSetupDialogButton.Text = "PageSetupDialog"
        Me.PageSetupDialogButton.UseVisualStyleBackColor = True
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(12, 449)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(101, 20)
        Me.textBox1.TabIndex = 83
        '
        'textBoxAspectRatio
        '
        Me.textBoxAspectRatio.Location = New System.Drawing.Point(95, 519)
        Me.textBoxAspectRatio.Name = "textBoxAspectRatio"
        Me.textBoxAspectRatio.Size = New System.Drawing.Size(35, 20)
        Me.textBoxAspectRatio.TabIndex = 82
        '
        'textBoxBarWidth
        '
        Me.textBoxBarWidth.Location = New System.Drawing.Point(38, 520)
        Me.textBoxBarWidth.Name = "textBoxBarWidth"
        Me.textBoxBarWidth.Size = New System.Drawing.Size(35, 20)
        Me.textBoxBarWidth.TabIndex = 81
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Location = New System.Drawing.Point(81, 503)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(65, 13)
        Me.label13.TabIndex = 80
        Me.label13.Text = "AspectRatio"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(30, 503)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 79
        Me.Label14.Text = "BarWidth"
        '
        'PrintPreviewButton
        '
        Me.PrintPreviewButton.Location = New System.Drawing.Point(143, 389)
        Me.PrintPreviewButton.Name = "PrintPreviewButton"
        Me.PrintPreviewButton.Size = New System.Drawing.Size(75, 23)
        Me.PrintPreviewButton.TabIndex = 54
        Me.PrintPreviewButton.TabStop = False
        Me.PrintPreviewButton.Text = "PrintPreview"
        Me.PrintPreviewButton.UseVisualStyleBackColor = True
        '
        'btnEncode
        '
        Me.btnEncode.Location = New System.Drawing.Point(4, 285)
        Me.btnEncode.Name = "btnEncode"
        Me.btnEncode.Size = New System.Drawing.Size(61, 52)
        Me.btnEncode.TabIndex = 3
        Me.btnEncode.Text = "&Encode"
        Me.btnEncode.UseVisualStyleBackColor = True
        '
        'txtHeight
        '
        Me.txtHeight.Location = New System.Drawing.Point(167, 76)
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(35, 20)
        Me.txtHeight.TabIndex = 44
        Me.txtHeight.Text = "300"
        '
        'cbLabelLocation
        '
        Me.cbLabelLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLabelLocation.FormattingEnabled = True
        Me.cbLabelLocation.Items.AddRange(New Object() {"BottomCenter", "BottomLeft", "BottomRight", "TopCenter", "TopLeft", "TopRight"})
        Me.cbLabelLocation.Location = New System.Drawing.Point(124, 162)
        Me.cbLabelLocation.Name = "cbLabelLocation"
        Me.cbLabelLocation.Size = New System.Drawing.Size(90, 21)
        Me.cbLabelLocation.TabIndex = 0
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(121, 191)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(88, 13)
        Me.label4.TabIndex = 38
        Me.label4.Text = "Foreground Color"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(165, 59)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(38, 13)
        Me.label6.TabIndex = 42
        Me.label6.Text = "Height"
        '
        'txtEncoded
        '
        Me.txtEncoded.Location = New System.Drawing.Point(4, 162)
        Me.txtEncoded.Multiline = True
        Me.txtEncoded.Name = "txtEncoded"
        Me.txtEncoded.ReadOnly = True
        Me.txtEncoded.Size = New System.Drawing.Size(108, 114)
        Me.txtEncoded.TabIndex = 14
        Me.txtEncoded.TabStop = False
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(124, 76)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(35, 20)
        Me.txtWidth.TabIndex = 43
        Me.txtWidth.Text = "600"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(121, 59)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(35, 13)
        Me.label7.TabIndex = 41
        Me.label7.Text = "Width"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(121, 237)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(92, 13)
        Me.label5.TabIndex = 39
        Me.label5.Text = "Background Color"
        '
        'btnBackColor
        '
        Me.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackColor.Location = New System.Drawing.Point(124, 253)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(90, 23)
        Me.btnBackColor.TabIndex = 37
        Me.btnBackColor.UseVisualStyleBackColor = True
        '
        'cbBarcodeAlign
        '
        Me.cbBarcodeAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBarcodeAlign.FormattingEnabled = True
        Me.cbBarcodeAlign.Items.AddRange(New Object() {"Center", "Left", "Right"})
        Me.cbBarcodeAlign.Location = New System.Drawing.Point(124, 118)
        Me.cbBarcodeAlign.Name = "cbBarcodeAlign"
        Me.cbBarcodeAlign.Size = New System.Drawing.Size(67, 21)
        Me.cbBarcodeAlign.TabIndex = 49
        '
        'btnForeColor
        '
        Me.btnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnForeColor.Location = New System.Drawing.Point(124, 207)
        Me.btnForeColor.Name = "btnForeColor"
        Me.btnForeColor.Size = New System.Drawing.Size(90, 23)
        Me.btnForeColor.TabIndex = 36
        Me.btnForeColor.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(1, 146)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 13)
        Me.label2.TabIndex = 33
        Me.label2.Text = "Encoded Value"
        '
        'lblEncodingTime
        '
        Me.lblEncodingTime.AutoSize = True
        Me.lblEncodingTime.Location = New System.Drawing.Point(84, 146)
        Me.lblEncodingTime.Name = "lblEncodingTime"
        Me.lblEncodingTime.Size = New System.Drawing.Size(0, 13)
        Me.lblEncodingTime.TabIndex = 45
        '
        'chkGenerateLabel
        '
        Me.chkGenerateLabel.AutoSize = True
        Me.chkGenerateLabel.Location = New System.Drawing.Point(108, 19)
        Me.chkGenerateLabel.Name = "chkGenerateLabel"
        Me.chkGenerateLabel.Size = New System.Drawing.Size(95, 17)
        Me.chkGenerateLabel.TabIndex = 40
        Me.chkGenerateLabel.Text = "Generate label"
        Me.chkGenerateLabel.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(1, 60)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(52, 13)
        Me.label3.TabIndex = 35
        Me.label3.Text = "Encoding"
        '
        'cbEncodeType
        '
        Me.cbEncodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEncodeType.FormattingEnabled = True
        Me.cbEncodeType.ItemHeight = 13
        Me.cbEncodeType.Items.AddRange(New Object() {"QR", "UPC-A", "UPC-E", "UPC 2 Digit Ext.", "UPC 5 Digit Ext.", "EAN-13", "JAN-13", "EAN-8", "ITF-14", "Interleaved 2 of 5", "Standard 2 of 5", "Codabar", "PostNet", "Bookland/ISBN", "Code 11", "Code 39", "Code 39 Extended", "Code 93", "Code 128", "Code 128-A", "Code 128-B", "Code 128-C", "LOGMARS", "MSI", "Telepen", "FIM", "Pharmacode"})
        Me.cbEncodeType.Location = New System.Drawing.Point(4, 76)
        Me.cbEncodeType.Name = "cbEncodeType"
        Me.cbEncodeType.Size = New System.Drawing.Size(108, 21)
        Me.cbEncodeType.TabIndex = 2
        '
        'txtData
        '
        Me.txtData.Location = New System.Drawing.Point(4, 36)
        Me.txtData.Name = "txtData"
        Me.txtData.Size = New System.Drawing.Size(194, 20)
        Me.txtData.TabIndex = 1
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(1, 20)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(86, 13)
        Me.label1.TabIndex = 29
        Me.label1.Text = "Value to Encode"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(158, 78)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(12, 13)
        Me.label9.TabIndex = 51
        Me.label9.Text = "x"
        '
        'barcodeGroupBox
        '
        Me.barcodeGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.barcodeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.barcodeGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.barcodeGroupBox.Name = "barcodeGroupBox"
        Me.barcodeGroupBox.Size = New System.Drawing.Size(793, 572)
        Me.barcodeGroupBox.TabIndex = 36
        Me.barcodeGroupBox.TabStop = False
        Me.barcodeGroupBox.Text = "Barcode Image"
        '
        'tslblCredits
        '
        Me.tslblCredits.Name = "tslblCredits"
        Me.tslblCredits.Size = New System.Drawing.Size(135, 19)
        Me.tslblCredits.Text = "Written by: Brad Barnhill"
        Me.tslblCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tslblLibraryVersion
        '
        Me.tslblLibraryVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.tslblLibraryVersion.Name = "tslblLibraryVersion"
        Me.tslblLibraryVersion.Size = New System.Drawing.Size(861, 19)
        Me.tslblLibraryVersion.Spring = True
        Me.tslblLibraryVersion.Text = "LibVersion"
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslEncodedType, Me.tslblLibraryVersion, Me.tslblCredits})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 572)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(1015, 24)
        Me.statusStrip1.SizingGrip = False
        Me.statusStrip1.TabIndex = 38
        Me.statusStrip1.Text = "statusStrip1"
        '
        'tsslEncodedType
        '
        Me.tsslEncodedType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.tsslEncodedType.Name = "tsslEncodedType"
        Me.tsslEncodedType.Size = New System.Drawing.Size(4, 19)
        '
        'errorProvider1
        '
        Me.errorProvider1.ContainerControl = Me
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'GmPrintDocument
        '
        '
        'GenBarCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 596)
        Me.Controls.Add(Me.splitContainer1)
        Me.Controls.Add(Me.statusStrip1)
        Me.Name = "GenBarCode"
        Me.Text = "GenBarCode"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents cbRotateFlip As System.Windows.Forms.ComboBox
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents btnSaveXML As System.Windows.Forms.Button
    Private WithEvents lblLabelLocation As System.Windows.Forms.Label
    Private WithEvents btnLoadXML As System.Windows.Forms.Button
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents btnEncode As System.Windows.Forms.Button
    Private WithEvents txtHeight As System.Windows.Forms.TextBox
    Private WithEvents cbLabelLocation As System.Windows.Forms.ComboBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents txtEncoded As System.Windows.Forms.TextBox
    Private WithEvents txtWidth As System.Windows.Forms.TextBox
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents btnBackColor As System.Windows.Forms.Button
    Private WithEvents cbBarcodeAlign As System.Windows.Forms.ComboBox
    Private WithEvents btnForeColor As System.Windows.Forms.Button
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents lblEncodingTime As System.Windows.Forms.Label
    Private WithEvents chkGenerateLabel As System.Windows.Forms.CheckBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cbEncodeType As System.Windows.Forms.ComboBox
    Private WithEvents txtData As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents tslblCredits As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents tslblLibraryVersion As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Private WithEvents tsslEncodedType As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents errorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents PrintPreviewButton As Button
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents GmPrintDocument As Drawing.Printing.PrintDocument
    Friend WithEvents PageSetupDialog1 As PageSetupDialog
    Private WithEvents textBoxAspectRatio As TextBox
    Private WithEvents textBoxBarWidth As TextBox
    Private WithEvents label13 As Label
    Private WithEvents Label14 As Label
    Private WithEvents textBox1 As TextBox
    Friend WithEvents PageSetupDialogButton As Button
    Private WithEvents barcodeGroupBox As GroupBox
End Class
