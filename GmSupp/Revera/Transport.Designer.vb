﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Transport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Transport))
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.MasterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LoanCategoriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectLoanCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkBoxCancelled = New System.Windows.Forms.CheckBox()
        Me.PanelPickDoc = New System.Windows.Forms.Panel()
        Me.lblTrnDate = New System.Windows.Forms.Label()
        Me.OK = New System.Windows.Forms.Button()
        Me.ddlXCOs = New System.Windows.Forms.ComboBox()
        Me.ddlPicks = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxNotes = New System.Windows.Forms.TextBox()
        Me.ToolStrip6 = New System.Windows.Forms.ToolStrip()
        Me.TlSComboBoxDate = New System.Windows.Forms.ToolStripComboBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label_99 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.chkBoxIsActive = New System.Windows.Forms.CheckBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MasterDataGridView = New System.Windows.Forms.DataGridView()
        Me.BindingNavigatorMaster = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem1 = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem1 = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMasterAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMasterDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.cmdPrint = New System.Windows.Forms.ToolStripButton()
        Me.DataGridViewSearch = New System.Windows.Forms.DataGridView()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoanCategoriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.PanelPickDoc.SuspendLayout()
        Me.ToolStrip6.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MasterDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorMaster.SuspendLayout()
        CType(Me.DataGridViewSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSelect
        '
        Me.cmdSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdSelect.Image = CType(resources.GetObject("cmdSelect.Image"), System.Drawing.Image)
        Me.cmdSelect.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.cmdSelect.Location = New System.Drawing.Point(400, 9)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(38, 38)
        Me.cmdSelect.TabIndex = 232
        '
        'MasterBindingSource
        '
        '
        'LoanCategoriesBindingSource
        '
        Me.LoanCategoriesBindingSource.AllowNew = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 594)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1284, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(119, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Name = "SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Text = "Select Columns for negative equity Status per account"
        '
        'SelectColumnsForNegativeEquityAccountToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Name = "SelectColumnsForNegativeEquityAccountToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Text = "Select Columns for negative equity per account"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(376, 6)
        '
        'SelectColumnsForNegativeEquityContractStatusToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.ForeColor = System.Drawing.Color.Green
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Name = "SelectColumnsForNegativeEquityContractStatusToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Text = "Select Columns for negative equity Status per contract"
        '
        'SelectColumnsForNegativeEquityContractToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.ForeColor = System.Drawing.Color.Blue
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Name = "SelectColumnsForNegativeEquityContractToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Text = "Select Columns for negative equity per contract"
        '
        'SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem
        '
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Name = "SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem"
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Text = "Select ΑΡ# ΣΥΜΒΑΣΗΣ"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(376, 6)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectLoanCategoryToolStripMenuItem, Me.ToolStripSeparator2, Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem, Me.SelectColumnsForNegativeEquityContractToolStripMenuItem, Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem, Me.ToolStripSeparator3, Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem, Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(380, 148)
        Me.ContextMenuStrip1.Text = "ΠΡΟΪΟΝ"
        '
        'SelectLoanCategoryToolStripMenuItem
        '
        Me.SelectLoanCategoryToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectLoanCategoryToolStripMenuItem.ForeColor = System.Drawing.Color.Red
        Me.SelectLoanCategoryToolStripMenuItem.Name = "SelectLoanCategoryToolStripMenuItem"
        Me.SelectLoanCategoryToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectLoanCategoryToolStripMenuItem.Text = "Select LoanCategory"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkBoxCancelled)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PanelPickDoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBoxNotes)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LinkLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label_99)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DateTimePicker2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkBoxIsActive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSelect)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1284, 749)
        Me.SplitContainer1.SplitterDistance = 129
        Me.SplitContainer1.TabIndex = 10
        '
        'chkBoxCancelled
        '
        Me.chkBoxCancelled.AutoSize = True
        Me.chkBoxCancelled.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.chkBoxCancelled.ForeColor = System.Drawing.Color.Blue
        Me.chkBoxCancelled.Location = New System.Drawing.Point(177, 67)
        Me.chkBoxCancelled.Name = "chkBoxCancelled"
        Me.chkBoxCancelled.Size = New System.Drawing.Size(93, 17)
        Me.chkBoxCancelled.TabIndex = 262
        Me.chkBoxCancelled.TabStop = False
        Me.chkBoxCancelled.Text = "+ Cancelled"
        Me.chkBoxCancelled.UseVisualStyleBackColor = True
        '
        'PanelPickDoc
        '
        Me.PanelPickDoc.Controls.Add(Me.lblTrnDate)
        Me.PanelPickDoc.Controls.Add(Me.OK)
        Me.PanelPickDoc.Controls.Add(Me.ddlXCOs)
        Me.PanelPickDoc.Controls.Add(Me.ddlPicks)
        Me.PanelPickDoc.Controls.Add(Me.Label1)
        Me.PanelPickDoc.Location = New System.Drawing.Point(36, 90)
        Me.PanelPickDoc.Name = "PanelPickDoc"
        Me.PanelPickDoc.Size = New System.Drawing.Size(429, 27)
        Me.PanelPickDoc.TabIndex = 261
        '
        'lblTrnDate
        '
        Me.lblTrnDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTrnDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTrnDate.ForeColor = System.Drawing.Color.Blue
        Me.lblTrnDate.Location = New System.Drawing.Point(1, 2)
        Me.lblTrnDate.Name = "lblTrnDate"
        Me.lblTrnDate.Size = New System.Drawing.Size(64, 20)
        Me.lblTrnDate.TabIndex = 256
        Me.lblTrnDate.Text = "Company:"
        Me.lblTrnDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(389, 2)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(30, 23)
        Me.OK.TabIndex = 260
        Me.OK.Text = "&OK"
        '
        'ddlXCOs
        '
        Me.ddlXCOs.FormattingEnabled = True
        Me.ddlXCOs.Location = New System.Drawing.Point(71, 2)
        Me.ddlXCOs.Name = "ddlXCOs"
        Me.ddlXCOs.Size = New System.Drawing.Size(121, 21)
        Me.ddlXCOs.TabIndex = 257
        '
        'ddlPicks
        '
        Me.ddlPicks.FormattingEnabled = True
        Me.ddlPicks.Location = New System.Drawing.Point(262, 2)
        Me.ddlPicks.Name = "ddlPicks"
        Me.ddlPicks.Size = New System.Drawing.Size(121, 21)
        Me.ddlPicks.TabIndex = 259
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(198, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 20)
        Me.Label1.TabIndex = 258
        Me.Label1.Text = "Pick:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBoxNotes
        '
        Me.txtBoxNotes.Location = New System.Drawing.Point(505, 12)
        Me.txtBoxNotes.Multiline = True
        Me.txtBoxNotes.Name = "txtBoxNotes"
        Me.txtBoxNotes.Size = New System.Drawing.Size(353, 63)
        Me.txtBoxNotes.TabIndex = 254
        '
        'ToolStrip6
        '
        Me.ToolStrip6.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip6.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TlSComboBoxDate})
        Me.ToolStrip6.Location = New System.Drawing.Point(9, 9)
        Me.ToolStrip6.Name = "ToolStrip6"
        Me.ToolStrip6.Size = New System.Drawing.Size(135, 25)
        Me.ToolStrip6.TabIndex = 253
        '
        'TlSComboBoxDate
        '
        Me.TlSComboBoxDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TlSComboBoxDate.ForeColor = System.Drawing.Color.Red
        Me.TlSComboBoxDate.Items.AddRange(New Object() {"Ημ/νία:", "Ανά Περίοδο:"})
        Me.TlSComboBoxDate.Name = "TlSComboBoxDate"
        Me.TlSComboBoxDate.Size = New System.Drawing.Size(121, 25)
        Me.TlSComboBoxDate.Text = "Ημ/νία:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LinkLabel1.Location = New System.Drawing.Point(147, 5)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(104, 17)
        Me.LinkLabel1.TabIndex = 249
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Από"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_99
        '
        Me.Label_99.BackColor = System.Drawing.SystemColors.Control
        Me.Label_99.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_99.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label_99.ForeColor = System.Drawing.Color.Blue
        Me.Label_99.Location = New System.Drawing.Point(260, 5)
        Me.Label_99.Name = "Label_99"
        Me.Label_99.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label_99.Size = New System.Drawing.Size(104, 17)
        Me.Label_99.TabIndex = 250
        Me.Label_99.Text = "Έως"
        Me.Label_99.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(260, 21)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker2.TabIndex = 252
        Me.DateTimePicker2.Value = New Date(2014, 3, 23, 21, 26, 41, 0)
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(148, 21)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 251
        Me.DateTimePicker1.Value = New Date(2006, 10, 31, 0, 0, 0, 0)
        '
        'chkBoxIsActive
        '
        Me.chkBoxIsActive.AutoSize = True
        Me.chkBoxIsActive.Checked = True
        Me.chkBoxIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBoxIsActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.chkBoxIsActive.ForeColor = System.Drawing.Color.Blue
        Me.chkBoxIsActive.Location = New System.Drawing.Point(36, 67)
        Me.chkBoxIsActive.Name = "chkBoxIsActive"
        Me.chkBoxIsActive.Size = New System.Drawing.Size(125, 17)
        Me.chkBoxIsActive.TabIndex = 242
        Me.chkBoxIsActive.TabStop = False
        Me.chkBoxIsActive.Text = "Όλες οι κινήσεις"
        Me.chkBoxIsActive.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MasterDataGridView)
        Me.SplitContainer3.Panel1.Controls.Add(Me.BindingNavigatorMaster)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.DataGridViewSearch)
        Me.SplitContainer3.Size = New System.Drawing.Size(1284, 594)
        Me.SplitContainer3.SplitterDistance = 545
        Me.SplitContainer3.TabIndex = 9
        '
        'MasterDataGridView
        '
        Me.MasterDataGridView.AllowUserToAddRows = False
        Me.MasterDataGridView.BackgroundColor = System.Drawing.SystemColors.Control
        Me.MasterDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.MasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MasterDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterDataGridView.Location = New System.Drawing.Point(0, 25)
        Me.MasterDataGridView.Name = "MasterDataGridView"
        Me.MasterDataGridView.Size = New System.Drawing.Size(1284, 520)
        Me.MasterDataGridView.TabIndex = 4
        '
        'BindingNavigatorMaster
        '
        Me.BindingNavigatorMaster.AddNewItem = Nothing
        Me.BindingNavigatorMaster.BindingSource = Me.MasterBindingSource
        Me.BindingNavigatorMaster.CountItem = Me.BindingNavigatorCountItem1
        Me.BindingNavigatorMaster.DeleteItem = Nothing
        Me.BindingNavigatorMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem1, Me.BindingNavigatorMovePreviousItem1, Me.BindingNavigatorSeparator3, Me.BindingNavigatorPositionItem1, Me.BindingNavigatorCountItem1, Me.BindingNavigatorSeparator4, Me.BindingNavigatorMoveNextItem1, Me.BindingNavigatorMoveLastItem1, Me.BindingNavigatorSeparator5, Me.BindingNavigatorMasterAddNewItem, Me.BindingNavigatorMasterDeleteItem, Me.OpenToolStripButton, Me.toolStripSeparator, Me.BindingNavigatorSaveItem, Me.cmdPrint})
        Me.BindingNavigatorMaster.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigatorMaster.MoveFirstItem = Me.BindingNavigatorMoveFirstItem1
        Me.BindingNavigatorMaster.MoveLastItem = Me.BindingNavigatorMoveLastItem1
        Me.BindingNavigatorMaster.MoveNextItem = Me.BindingNavigatorMoveNextItem1
        Me.BindingNavigatorMaster.MovePreviousItem = Me.BindingNavigatorMovePreviousItem1
        Me.BindingNavigatorMaster.Name = "BindingNavigatorMaster"
        Me.BindingNavigatorMaster.PositionItem = Me.BindingNavigatorPositionItem1
        Me.BindingNavigatorMaster.Size = New System.Drawing.Size(1284, 25)
        Me.BindingNavigatorMaster.TabIndex = 5
        Me.BindingNavigatorMaster.Text = "BindingNavigator2"
        '
        'BindingNavigatorCountItem1
        '
        Me.BindingNavigatorCountItem1.Name = "BindingNavigatorCountItem1"
        Me.BindingNavigatorCountItem1.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem1.Text = "of {0}"
        Me.BindingNavigatorCountItem1.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem1
        '
        Me.BindingNavigatorMoveFirstItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem1.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem1.Name = "BindingNavigatorMoveFirstItem1"
        Me.BindingNavigatorMoveFirstItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem1.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem1
        '
        Me.BindingNavigatorMovePreviousItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem1.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem1.Name = "BindingNavigatorMovePreviousItem1"
        Me.BindingNavigatorMovePreviousItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem1.Text = "Move previous"
        '
        'BindingNavigatorSeparator3
        '
        Me.BindingNavigatorSeparator3.Name = "BindingNavigatorSeparator3"
        Me.BindingNavigatorSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem1
        '
        Me.BindingNavigatorPositionItem1.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem1.AutoSize = False
        Me.BindingNavigatorPositionItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem1.Name = "BindingNavigatorPositionItem1"
        Me.BindingNavigatorPositionItem1.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem1.Text = "0"
        Me.BindingNavigatorPositionItem1.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator4
        '
        Me.BindingNavigatorSeparator4.Name = "BindingNavigatorSeparator4"
        Me.BindingNavigatorSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem1
        '
        Me.BindingNavigatorMoveNextItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem1.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem1.Name = "BindingNavigatorMoveNextItem1"
        Me.BindingNavigatorMoveNextItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem1.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem1
        '
        Me.BindingNavigatorMoveLastItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem1.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem1.Name = "BindingNavigatorMoveLastItem1"
        Me.BindingNavigatorMoveLastItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem1.Text = "Move last"
        '
        'BindingNavigatorSeparator5
        '
        Me.BindingNavigatorSeparator5.Name = "BindingNavigatorSeparator5"
        Me.BindingNavigatorSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMasterAddNewItem
        '
        Me.BindingNavigatorMasterAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMasterAddNewItem.Image = CType(resources.GetObject("BindingNavigatorMasterAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMasterAddNewItem.Name = "BindingNavigatorMasterAddNewItem"
        Me.BindingNavigatorMasterAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMasterAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMasterAddNewItem.Text = "Add new"
        Me.BindingNavigatorMasterAddNewItem.Visible = False
        '
        'BindingNavigatorMasterDeleteItem
        '
        Me.BindingNavigatorMasterDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMasterDeleteItem.Image = CType(resources.GetObject("BindingNavigatorMasterDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMasterDeleteItem.Name = "BindingNavigatorMasterDeleteItem"
        Me.BindingNavigatorMasterDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMasterDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMasterDeleteItem.Text = "Delete"
        Me.BindingNavigatorMasterDeleteItem.Visible = False
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        Me.OpenToolStripButton.Visible = False
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorSaveItem
        '
        Me.BindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorSaveItem.Enabled = False
        Me.BindingNavigatorSaveItem.Image = CType(resources.GetObject("BindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorSaveItem.Name = "BindingNavigatorSaveItem"
        Me.BindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorSaveItem.Text = "Save Data"
        '
        'cmdPrint
        '
        Me.cmdPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPrint.Image = Global.GmSupp.My.Resources.Resources.Printer_Folder
        Me.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(23, 22)
        Me.cmdPrint.Tag = "1"
        Me.cmdPrint.Text = "Εκτύπωση Αίτησης"
        '
        'DataGridViewSearch
        '
        Me.DataGridViewSearch.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridViewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewSearch.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewSearch.Name = "DataGridViewSearch"
        Me.DataGridViewSearch.Size = New System.Drawing.Size(1284, 45)
        Me.DataGridViewSearch.TabIndex = 6
        '
        'Transport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 749)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Transport"
        Me.Text = "Transport"
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoanCategoriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.PanelPickDoc.ResumeLayout(False)
        Me.ToolStrip6.ResumeLayout(False)
        Me.ToolStrip6.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MasterDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigatorMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigatorMaster.ResumeLayout(False)
        Me.BindingNavigatorMaster.PerformLayout()
        CType(Me.DataGridViewSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdSelect As Button
    Friend WithEvents MasterBindingSource As BindingSource
    Friend WithEvents DetailsBindingSource As BindingSource
    Friend WithEvents LoanCategoriesBindingSource As BindingSource
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectColumnsForNegativeEquityAccountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents SelectColumnsForNegativeEquityContractStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectColumnsForNegativeEquityContractToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SelectLoanCategoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents MasterDataGridView As DataGridView
    Friend WithEvents BindingNavigatorMaster As BindingNavigator
    Friend WithEvents BindingNavigatorCountItem1 As ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem1 As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem1 As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator3 As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem1 As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator4 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem1 As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem1 As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator5 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMasterAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorMasterDeleteItem As ToolStripButton
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents cmdPrint As ToolStripButton
    Friend WithEvents DataGridViewSearch As DataGridView
    Friend WithEvents chkBoxIsActive As CheckBox
    Friend WithEvents ToolStrip6 As ToolStrip
    Friend WithEvents TlSComboBoxDate As ToolStripComboBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Public WithEvents Label_99 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents txtBoxNotes As TextBox
    Friend WithEvents lblTrnDate As Label
    Friend WithEvents ddlXCOs As ComboBox
    Friend WithEvents ddlPicks As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents OK As Button
    Friend WithEvents PanelPickDoc As Panel
    Friend WithEvents chkBoxCancelled As CheckBox
End Class
