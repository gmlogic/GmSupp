<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportExcel
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportExcel))
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.DataGridViewLoanCategories = New System.Windows.Forms.DataGridView()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.ddlLoanCategories = New System.Windows.Forms.ComboBox()
        Me.txtboxResults = New System.Windows.Forms.TextBox()
        Me.btnGetExcel = New System.Windows.Forms.Button()
        Me.FileName = New System.Windows.Forms.TextBox()
        Me.btnOpenFileDialog = New System.Windows.Forms.Button()
        Me.ddlSheets = New System.Windows.Forms.ComboBox()
        Me.MasterDataGridView = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectLoanCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MasterBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MasterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdPelPro4 = New System.Windows.Forms.ToolStripButton()
        Me.LoanCategoriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewLoanCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.MasterBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MasterBindingNavigator.SuspendLayout()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoanCategoriesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(376, 6)
        '
        'SelectColumnsForNegativeEquityContractToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.ForeColor = System.Drawing.Color.Blue
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Name = "SelectColumnsForNegativeEquityContractToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityContractToolStripMenuItem.Text = "Select Columns for negative equity per contract"
        '
        'SelectColumnsForNegativeEquityContractStatusToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.ForeColor = System.Drawing.Color.Green
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Name = "SelectColumnsForNegativeEquityContractStatusToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Text = "Select Columns for negative equity Status per contract"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(376, 6)
        '
        'SelectColumnsForNegativeEquityAccountToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Name = "SelectColumnsForNegativeEquityAccountToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityAccountToolStripMenuItem.Text = "Select Columns for negative equity per account"
        '
        'SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem
        '
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Name = "SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem"
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Text = "Select Columns for negative equity Status per account"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCheck)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewLoanCategories)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlLoanCategories)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtboxResults)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGetExcel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FileName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnOpenFileDialog)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlSheets)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.MasterDataGridView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MasterBindingNavigator)
        Me.SplitContainer1.Size = New System.Drawing.Size(1744, 792)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 8
        '
        'btnCheck
        '
        Me.btnCheck.Enabled = False
        Me.btnCheck.Location = New System.Drawing.Point(252, 41)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(75, 23)
        Me.btnCheck.TabIndex = 15
        Me.btnCheck.Text = "Check"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'DataGridViewLoanCategories
        '
        Me.DataGridViewLoanCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewLoanCategories.Location = New System.Drawing.Point(440, 0)
        Me.DataGridViewLoanCategories.Name = "DataGridViewLoanCategories"
        Me.DataGridViewLoanCategories.Size = New System.Drawing.Size(806, 180)
        Me.DataGridViewLoanCategories.TabIndex = 14
        '
        'btnImport
        '
        Me.btnImport.Enabled = False
        Me.btnImport.Location = New System.Drawing.Point(12, 135)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(75, 23)
        Me.btnImport.TabIndex = 13
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'ddlLoanCategories
        '
        Me.ddlLoanCategories.Enabled = False
        Me.ddlLoanCategories.Items.AddRange(New Object() {"< Select LoanCategory >"})
        Me.ddlLoanCategories.Location = New System.Drawing.Point(12, 78)
        Me.ddlLoanCategories.Name = "ddlLoanCategories"
        Me.ddlLoanCategories.Size = New System.Drawing.Size(183, 21)
        Me.ddlLoanCategories.TabIndex = 9
        Me.ddlLoanCategories.Text = "Select"
        '
        'txtboxResults
        '
        Me.txtboxResults.Location = New System.Drawing.Point(1252, 0)
        Me.txtboxResults.Multiline = True
        Me.txtboxResults.Name = "txtboxResults"
        Me.txtboxResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtboxResults.Size = New System.Drawing.Size(200, 177)
        Me.txtboxResults.TabIndex = 1
        '
        'btnGetExcel
        '
        Me.btnGetExcel.Enabled = False
        Me.btnGetExcel.Location = New System.Drawing.Point(139, 38)
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.Size = New System.Drawing.Size(95, 23)
        Me.btnGetExcel.TabIndex = 7
        Me.btnGetExcel.Text = "Get Excel Sheet"
        Me.btnGetExcel.UseVisualStyleBackColor = True
        '
        'FileName
        '
        Me.FileName.Location = New System.Drawing.Point(12, 12)
        Me.FileName.Name = "FileName"
        Me.FileName.Size = New System.Drawing.Size(234, 20)
        Me.FileName.TabIndex = 1
        Me.FileName.Text = "Επιλογή Αρχείου"
        '
        'btnOpenFileDialog
        '
        Me.btnOpenFileDialog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.btnOpenFileDialog.Location = New System.Drawing.Point(252, 12)
        Me.btnOpenFileDialog.Name = "btnOpenFileDialog"
        Me.btnOpenFileDialog.Size = New System.Drawing.Size(27, 23)
        Me.btnOpenFileDialog.TabIndex = 2
        Me.btnOpenFileDialog.Text = "..."
        '
        'ddlSheets
        '
        Me.ddlSheets.Enabled = False
        Me.ddlSheets.Location = New System.Drawing.Point(12, 38)
        Me.ddlSheets.Name = "ddlSheets"
        Me.ddlSheets.Size = New System.Drawing.Size(121, 21)
        Me.ddlSheets.TabIndex = 3
        '
        'MasterDataGridView
        '
        Me.MasterDataGridView.AllowUserToAddRows = False
        Me.MasterDataGridView.AllowUserToDeleteRows = False
        Me.MasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MasterDataGridView.ContextMenuStrip = Me.ContextMenuStrip1
        Me.MasterDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasterDataGridView.Location = New System.Drawing.Point(0, 25)
        Me.MasterDataGridView.Name = "MasterDataGridView"
        Me.MasterDataGridView.ReadOnly = True
        Me.MasterDataGridView.Size = New System.Drawing.Size(1744, 534)
        Me.MasterDataGridView.TabIndex = 3
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
        'SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem
        '
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Name = "SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem"
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Size = New System.Drawing.Size(379, 22)
        Me.SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Text = "Select ΑΡ# ΣΥΜΒΑΣΗΣ"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 559)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1744, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'MasterBindingNavigator
        '
        Me.MasterBindingNavigator.AddNewItem = Nothing
        Me.MasterBindingNavigator.BindingSource = Me.MasterBindingSource
        Me.MasterBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.MasterBindingNavigator.DeleteItem = Nothing
        Me.MasterBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.BindingNavigatorSaveItem, Me.OpenToolStripButton, Me.PrintToolStripButton, Me.ToolStripSeparator1, Me.cmdPelPro4})
        Me.MasterBindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.MasterBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.MasterBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.MasterBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.MasterBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.MasterBindingNavigator.Name = "MasterBindingNavigator"
        Me.MasterBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.MasterBindingNavigator.Size = New System.Drawing.Size(1744, 25)
        Me.MasterBindingNavigator.TabIndex = 2
        Me.MasterBindingNavigator.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorSaveItem
        '
        Me.BindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorSaveItem.Image = CType(resources.GetObject("BindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorSaveItem.Name = "BindingNavigatorSaveItem"
        Me.BindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorSaveItem.Text = "Save Data"
        Me.BindingNavigatorSaveItem.Visible = False
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'LoanCategoriesBindingSource
        '
        Me.LoanCategoriesBindingSource.AllowNew = False
        '
        'ImportExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1744, 792)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "ImportExcel"
        Me.Text = "ImportExcel"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewLoanCategories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.MasterBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MasterBindingNavigator.ResumeLayout(False)
        Me.MasterBindingNavigator.PerformLayout()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoanCategoriesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents SelectColumnsForNegativeEquityContractToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectColumnsForNegativeEquityContractStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents SelectColumnsForNegativeEquityAccountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnCheck As Button
    Friend WithEvents DataGridViewLoanCategories As DataGridView
    Friend WithEvents btnImport As Button
    Friend WithEvents ddlLoanCategories As ComboBox
    Friend WithEvents txtboxResults As TextBox
    Friend WithEvents btnGetExcel As Button
    Friend WithEvents FileName As TextBox
    Friend WithEvents btnOpenFileDialog As Button
    Friend WithEvents ddlSheets As ComboBox
    Friend WithEvents MasterDataGridView As DataGridView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SelectLoanCategoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents MasterBindingNavigator As BindingNavigator
    Friend WithEvents MasterBindingSource As BindingSource
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents cmdPelPro4 As ToolStripButton
    Friend WithEvents LoanCategoriesBindingSource As BindingSource
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
