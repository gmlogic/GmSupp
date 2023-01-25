<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PriceList
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
        Dim Label6 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PriceList))
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
        Me.btnChangeFinalDate = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerNewFinaldate = New System.Windows.Forms.DateTimePicker()
        Me.tlsDISTRICT1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox3 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlsTxtDISTRICT1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSBtnDISTRICT1 = New System.Windows.Forms.ToolStripButton()
        Me.tlsTrdr = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSTxtTRDR = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSBtnTRDR = New System.Windows.Forms.ToolStripButton()
        Me.tlsWhouse = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSTxtWHOUSE = New System.Windows.Forms.ToolStripTextBox()
        Me.TlSBtnWHOUSE = New System.Windows.Forms.ToolStripButton()
        Me.ddlPriceLists = New System.Windows.Forms.ComboBox()
        Me.ToolStrip6 = New System.Windows.Forms.ToolStrip()
        Me.TlSComboBoxDate = New System.Windows.Forms.ToolStripComboBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label_99 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.tlsMtrl = New System.Windows.Forms.ToolStrip()
        Me.ToolStripComboBox2 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSTxtMTRL = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSBtnMTRL = New System.Windows.Forms.ToolStripButton()
        Me.chkBoxCodeExp = New System.Windows.Forms.CheckBox()
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
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripDropDownButton()
        Me.echelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdPelPro4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.TlSBtnCheck = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnUnCheck = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DataGridViewSearch = New System.Windows.Forms.DataGridView()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Label6 = New System.Windows.Forms.Label()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoanCategoriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tlsDISTRICT1.SuspendLayout()
        Me.tlsTrdr.SuspendLayout()
        Me.tlsWhouse.SuspendLayout()
        Me.ToolStrip6.SuspendLayout()
        Me.tlsMtrl.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MasterDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigatorMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigatorMaster.SuspendLayout()
        CType(Me.DataGridViewSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label6.ForeColor = System.Drawing.Color.Blue
        Label6.Location = New System.Drawing.Point(9, 60)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(102, 20)
        Label6.TabIndex = 250
        Label6.Text = "Τιμοκατάλογοι:"
        Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdSelect
        '
        Me.cmdSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdSelect.Image = CType(resources.GetObject("cmdSelect.Image"), System.Drawing.Image)
        Me.cmdSelect.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.cmdSelect.Location = New System.Drawing.Point(460, 15)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(38, 38)
        Me.cmdSelect.TabIndex = 232
        '
        'LoanCategoriesBindingSource
        '
        Me.LoanCategoriesBindingSource.AllowNew = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 514)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnChangeFinalDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DateTimePickerNewFinaldate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tlsDISTRICT1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tlsTrdr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tlsWhouse)
        Me.SplitContainer1.Panel1.Controls.Add(Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlPriceLists)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LinkLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label_99)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DateTimePicker2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.tlsMtrl)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkBoxCodeExp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSelect)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1284, 749)
        Me.SplitContainer1.SplitterDistance = 209
        Me.SplitContainer1.TabIndex = 10
        '
        'btnChangeFinalDate
        '
        Me.btnChangeFinalDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnChangeFinalDate.Location = New System.Drawing.Point(634, 6)
        Me.btnChangeFinalDate.Name = "btnChangeFinalDate"
        Me.btnChangeFinalDate.Size = New System.Drawing.Size(94, 47)
        Me.btnChangeFinalDate.TabIndex = 268
        Me.btnChangeFinalDate.Text = "Μαζική αλλαγή Ημ/νίας"
        Me.btnChangeFinalDate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(524, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(104, 17)
        Me.Label1.TabIndex = 264
        Me.Label1.Text = "Νέα"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DateTimePickerNewFinaldate
        '
        Me.DateTimePickerNewFinaldate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerNewFinaldate.Location = New System.Drawing.Point(524, 25)
        Me.DateTimePickerNewFinaldate.Name = "DateTimePickerNewFinaldate"
        Me.DateTimePickerNewFinaldate.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePickerNewFinaldate.TabIndex = 266
        Me.DateTimePickerNewFinaldate.Value = New Date(2014, 3, 23, 21, 26, 41, 0)
        '
        'tlsDISTRICT1
        '
        Me.tlsDISTRICT1.Dock = System.Windows.Forms.DockStyle.None
        Me.tlsDISTRICT1.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.tlsDISTRICT1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox3, Me.ToolStripSeparator1, Me.TlsTxtDISTRICT1, Me.ToolStripSeparator4, Me.TlSBtnDISTRICT1})
        Me.tlsDISTRICT1.Location = New System.Drawing.Point(421, 90)
        Me.tlsDISTRICT1.Name = "tlsDISTRICT1"
        Me.tlsDISTRICT1.Size = New System.Drawing.Size(372, 25)
        Me.tlsDISTRICT1.TabIndex = 262
        '
        'ToolStripComboBox3
        '
        Me.ToolStripComboBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripComboBox3.ForeColor = System.Drawing.Color.Red
        Me.ToolStripComboBox3.Name = "ToolStripComboBox3"
        Me.ToolStripComboBox3.Size = New System.Drawing.Size(121, 25)
        Me.ToolStripComboBox3.Text = "Νομός:"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TlsTxtDISTRICT1
        '
        Me.TlsTxtDISTRICT1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TlsTxtDISTRICT1.Name = "TlsTxtDISTRICT1"
        Me.TlsTxtDISTRICT1.Size = New System.Drawing.Size(200, 25)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'TlSBtnDISTRICT1
        '
        Me.TlSBtnDISTRICT1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnDISTRICT1.Image = CType(resources.GetObject("TlSBtnDISTRICT1.Image"), System.Drawing.Image)
        Me.TlSBtnDISTRICT1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnDISTRICT1.Name = "TlSBtnDISTRICT1"
        Me.TlSBtnDISTRICT1.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnDISTRICT1.Tag = ""
        Me.TlSBtnDISTRICT1.Text = "&Open"
        Me.TlSBtnDISTRICT1.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'tlsTrdr
        '
        Me.tlsTrdr.Dock = System.Windows.Forms.DockStyle.None
        Me.tlsTrdr.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.tlsTrdr.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox1, Me.ToolStripSeparator12, Me.TlSTxtTRDR, Me.ToolStripSeparator13, Me.TlSBtnTRDR})
        Me.tlsTrdr.Location = New System.Drawing.Point(9, 90)
        Me.tlsTrdr.Name = "tlsTrdr"
        Me.tlsTrdr.Size = New System.Drawing.Size(372, 25)
        Me.tlsTrdr.TabIndex = 261
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripComboBox1.ForeColor = System.Drawing.Color.Red
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(121, 25)
        Me.ToolStripComboBox1.Text = "Συναλλασόμενος:"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 25)
        '
        'TlSTxtTRDR
        '
        Me.TlSTxtTRDR.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TlSTxtTRDR.Name = "TlSTxtTRDR"
        Me.TlSTxtTRDR.Size = New System.Drawing.Size(200, 25)
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 25)
        '
        'TlSBtnTRDR
        '
        Me.TlSBtnTRDR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnTRDR.Image = CType(resources.GetObject("TlSBtnTRDR.Image"), System.Drawing.Image)
        Me.TlSBtnTRDR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnTRDR.Name = "TlSBtnTRDR"
        Me.TlSBtnTRDR.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnTRDR.Tag = ""
        Me.TlSBtnTRDR.Text = "&Open"
        Me.TlSBtnTRDR.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'tlsWhouse
        '
        Me.tlsWhouse.Dock = System.Windows.Forms.DockStyle.None
        Me.tlsWhouse.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel4, Me.ToolStripSeparator15, Me.TlSTxtWHOUSE, Me.TlSBtnWHOUSE})
        Me.tlsWhouse.Location = New System.Drawing.Point(9, 140)
        Me.tlsWhouse.Name = "tlsWhouse"
        Me.tlsWhouse.Size = New System.Drawing.Size(351, 25)
        Me.tlsWhouse.TabIndex = 260
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel4.ForeColor = System.Drawing.Color.Red
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(28, 22)
        Me.ToolStripLabel4.Text = "A.X:"
        Me.ToolStripLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(6, 25)
        '
        'TlSTxtWHOUSE
        '
        Me.TlSTxtWHOUSE.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TlSTxtWHOUSE.Name = "TlSTxtWHOUSE"
        Me.TlSTxtWHOUSE.Size = New System.Drawing.Size(280, 25)
        '
        'TlSBtnWHOUSE
        '
        Me.TlSBtnWHOUSE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnWHOUSE.Image = CType(resources.GetObject("TlSBtnWHOUSE.Image"), System.Drawing.Image)
        Me.TlSBtnWHOUSE.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnWHOUSE.Name = "TlSBtnWHOUSE"
        Me.TlSBtnWHOUSE.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnWHOUSE.Tag = ""
        Me.TlSBtnWHOUSE.Text = "&Open"
        Me.TlSBtnWHOUSE.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'ddlPriceLists
        '
        Me.ddlPriceLists.FormattingEnabled = True
        Me.ddlPriceLists.Location = New System.Drawing.Point(117, 59)
        Me.ddlPriceLists.Name = "ddlPriceLists"
        Me.ddlPriceLists.Size = New System.Drawing.Size(381, 21)
        Me.ddlPriceLists.TabIndex = 249
        '
        'ToolStrip6
        '
        Me.ToolStrip6.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TlSComboBoxDate})
        Me.ToolStrip6.Location = New System.Drawing.Point(9, 15)
        Me.ToolStrip6.Name = "ToolStrip6"
        Me.ToolStrip6.Size = New System.Drawing.Size(135, 25)
        Me.ToolStrip6.TabIndex = 248
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
        Me.LinkLabel1.Location = New System.Drawing.Point(144, 6)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(104, 17)
        Me.LinkLabel1.TabIndex = 244
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
        Me.Label_99.Location = New System.Drawing.Point(257, 6)
        Me.Label_99.Name = "Label_99"
        Me.Label_99.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label_99.Size = New System.Drawing.Size(104, 17)
        Me.Label_99.TabIndex = 245
        Me.Label_99.Text = "Έως"
        Me.Label_99.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(257, 22)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker2.TabIndex = 247
        Me.DateTimePicker2.Value = New Date(2014, 3, 23, 21, 26, 41, 0)
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(145, 22)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(104, 20)
        Me.DateTimePicker1.TabIndex = 246
        Me.DateTimePicker1.Value = New Date(2006, 10, 31, 0, 0, 0, 0)
        '
        'tlsMtrl
        '
        Me.tlsMtrl.Dock = System.Windows.Forms.DockStyle.None
        Me.tlsMtrl.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.tlsMtrl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBox2, Me.ToolStripSeparator8, Me.TlSTxtMTRL, Me.ToolStripSeparator9, Me.TlSBtnMTRL})
        Me.tlsMtrl.Location = New System.Drawing.Point(9, 115)
        Me.tlsMtrl.Name = "tlsMtrl"
        Me.tlsMtrl.Size = New System.Drawing.Size(372, 25)
        Me.tlsMtrl.TabIndex = 243
        '
        'ToolStripComboBox2
        '
        Me.ToolStripComboBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripComboBox2.ForeColor = System.Drawing.Color.Red
        Me.ToolStripComboBox2.Items.AddRange(New Object() {"Κωδικός  Είδους:", "Βοηθητ.Κλειδί:"})
        Me.ToolStripComboBox2.Name = "ToolStripComboBox2"
        Me.ToolStripComboBox2.Size = New System.Drawing.Size(121, 25)
        Me.ToolStripComboBox2.Text = "Κωδικός  Είδους:"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'TlSTxtMTRL
        '
        Me.TlSTxtMTRL.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TlSTxtMTRL.Name = "TlSTxtMTRL"
        Me.TlSTxtMTRL.Size = New System.Drawing.Size(200, 25)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'TlSBtnMTRL
        '
        Me.TlSBtnMTRL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnMTRL.Image = CType(resources.GetObject("TlSBtnMTRL.Image"), System.Drawing.Image)
        Me.TlSBtnMTRL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnMTRL.Name = "TlSBtnMTRL"
        Me.TlSBtnMTRL.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnMTRL.Tag = ""
        Me.TlSBtnMTRL.Text = "&Open"
        Me.TlSBtnMTRL.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'chkBoxCodeExp
        '
        Me.chkBoxCodeExp.AutoSize = True
        Me.chkBoxCodeExp.Checked = True
        Me.chkBoxCodeExp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBoxCodeExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.chkBoxCodeExp.ForeColor = System.Drawing.Color.Blue
        Me.chkBoxCodeExp.Location = New System.Drawing.Point(1123, 76)
        Me.chkBoxCodeExp.Name = "chkBoxCodeExp"
        Me.chkBoxCodeExp.Size = New System.Drawing.Size(158, 17)
        Me.chkBoxCodeExp.TabIndex = 242
        Me.chkBoxCodeExp.TabStop = False
        Me.chkBoxCodeExp.Text = "Χωρίς Λογ. λογιστικής"
        Me.chkBoxCodeExp.UseVisualStyleBackColor = True
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
        Me.SplitContainer3.Size = New System.Drawing.Size(1284, 514)
        Me.SplitContainer3.SplitterDistance = 465
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
        Me.MasterDataGridView.Size = New System.Drawing.Size(1284, 440)
        Me.MasterDataGridView.TabIndex = 4
        '
        'BindingNavigatorMaster
        '
        Me.BindingNavigatorMaster.AddNewItem = Nothing
        Me.BindingNavigatorMaster.BindingSource = Me.MasterBindingSource
        Me.BindingNavigatorMaster.CountItem = Me.BindingNavigatorCountItem1
        Me.BindingNavigatorMaster.DeleteItem = Nothing
        Me.BindingNavigatorMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem1, Me.BindingNavigatorMovePreviousItem1, Me.BindingNavigatorSeparator3, Me.BindingNavigatorPositionItem1, Me.BindingNavigatorCountItem1, Me.BindingNavigatorSeparator4, Me.BindingNavigatorMoveNextItem1, Me.BindingNavigatorMoveLastItem1, Me.BindingNavigatorSeparator5, Me.BindingNavigatorMasterAddNewItem, Me.BindingNavigatorMasterDeleteItem, Me.OpenToolStripButton, Me.toolStripSeparator, Me.BindingNavigatorSaveItem, Me.ToolStripButton1, Me.PrintToolStripButton, Me.ToolStripSeparator5, Me.cmdPelPro4, Me.ToolStripSeparator6, Me.TlSBtnCheck, Me.TlSBtnUnCheck, Me.ToolStripSeparator11, Me.ExcelToolStripButton})
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
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "&Open"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.echelToolStripMenuItem})
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(29, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'echelToolStripMenuItem
        '
        Me.echelToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.echelToolStripMenuItem.ForeColor = System.Drawing.Color.Blue
        Me.echelToolStripMenuItem.Name = "echelToolStripMenuItem"
        Me.echelToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.echelToolStripMenuItem.Tag = "GmInfo.Parag01.rdlc"
        Me.echelToolStripMenuItem.Text = "Export To Excell"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'cmdPelPro4
        '
        Me.cmdPelPro4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPelPro4.Image = CType(resources.GetObject("cmdPelPro4.Image"), System.Drawing.Image)
        Me.cmdPelPro4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPelPro4.Name = "cmdPelPro4"
        Me.cmdPelPro4.Size = New System.Drawing.Size(23, 22)
        Me.cmdPelPro4.Text = "ToolStripButton1"
        Me.cmdPelPro4.ToolTipText = "Οικονομικά Στοιχεία"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'TlSBtnCheck
        '
        Me.TlSBtnCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnCheck.Image = CType(resources.GetObject("TlSBtnCheck.Image"), System.Drawing.Image)
        Me.TlSBtnCheck.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.TlSBtnCheck.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnCheck.Name = "TlSBtnCheck"
        Me.TlSBtnCheck.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnCheck.Text = "ToolStripButton1"
        Me.TlSBtnCheck.ToolTipText = "Επιλογή Όλων"
        '
        'TlSBtnUnCheck
        '
        Me.TlSBtnUnCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnUnCheck.Image = CType(resources.GetObject("TlSBtnUnCheck.Image"), System.Drawing.Image)
        Me.TlSBtnUnCheck.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnUnCheck.Name = "TlSBtnUnCheck"
        Me.TlSBtnUnCheck.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnUnCheck.Text = "ToolStripButton2"
        Me.TlSBtnUnCheck.ToolTipText = "Καμία Επιλογή"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 25)
        '
        'ExcelToolStripButton
        '
        Me.ExcelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelToolStripButton.Image = Global.GmSupp.My.Resources.Resources.EXCEL_257
        Me.ExcelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelToolStripButton.Name = "ExcelToolStripButton"
        Me.ExcelToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.ExcelToolStripButton.Text = "Export to Excel"
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
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'PriceList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 749)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "PriceList"
        Me.Text = "ΕxpensesMtrl"
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
        Me.tlsDISTRICT1.ResumeLayout(False)
        Me.tlsDISTRICT1.PerformLayout()
        Me.tlsTrdr.ResumeLayout(False)
        Me.tlsTrdr.PerformLayout()
        Me.tlsWhouse.ResumeLayout(False)
        Me.tlsWhouse.PerformLayout()
        Me.ToolStrip6.ResumeLayout(False)
        Me.ToolStrip6.PerformLayout()
        Me.tlsMtrl.ResumeLayout(False)
        Me.tlsMtrl.PerformLayout()
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
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DataGridViewSearch As DataGridView
    Friend WithEvents chkBoxCodeExp As CheckBox
    Friend WithEvents tlsMtrl As ToolStrip
    Friend WithEvents ToolStripComboBox2 As ToolStripComboBox
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents TlSTxtMTRL As ToolStripTextBox
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents TlSBtnMTRL As ToolStripButton
    Friend WithEvents ToolStrip6 As ToolStrip
    Friend WithEvents TlSComboBoxDate As ToolStripComboBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Public WithEvents Label_99 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ddlPriceLists As ComboBox
    Friend WithEvents tlsWhouse As ToolStrip
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents ToolStripSeparator15 As ToolStripSeparator
    Friend WithEvents TlSTxtWHOUSE As ToolStripTextBox
    Friend WithEvents TlSBtnWHOUSE As ToolStripButton
    Friend WithEvents tlsTrdr As ToolStrip
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents TlSTxtTRDR As ToolStripTextBox
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents TlSBtnTRDR As ToolStripButton
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents tlsDISTRICT1 As ToolStrip
    Friend WithEvents ToolStripComboBox3 As ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TlsTxtDISTRICT1 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents TlSBtnDISTRICT1 As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripDropDownButton
    Friend WithEvents echelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents cmdPelPro4 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents TlSBtnCheck As ToolStripButton
    Friend WithEvents TlSBtnUnCheck As ToolStripButton
    Friend WithEvents ToolStripSeparator11 As ToolStripSeparator
    Friend WithEvents ExcelToolStripButton As ToolStripButton
    Public WithEvents Label1 As Label
    Friend WithEvents DateTimePickerNewFinaldate As DateTimePicker
    Friend WithEvents btnChangeFinalDate As Button
End Class
