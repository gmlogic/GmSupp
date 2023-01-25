<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModifyDocBR
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
        Dim DriverNameLabel As System.Windows.Forms.Label
        Dim TrucksnoLabel As System.Windows.Forms.Label
        Dim RemarksLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModifyDocBR))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.MTRLINEBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.MasterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtBoxTrucksno = New System.Windows.Forms.TextBox()
        Me.FindocTextBox = New System.Windows.Forms.TextBox()
        Me.txtBoxRemarks = New System.Windows.Forms.TextBox()
        Me.txtBoxDriverName = New System.Windows.Forms.TextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MTRLINEDataGridView = New System.Windows.Forms.DataGridView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.AboutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Label6 = New System.Windows.Forms.Label()
        DriverNameLabel = New System.Windows.Forms.Label()
        TrucksnoLabel = New System.Windows.Forms.Label()
        RemarksLabel = New System.Windows.Forms.Label()
        CType(Me.BindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator.SuspendLayout()
        CType(Me.MTRLINEBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MTRLINEDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label6.ForeColor = System.Drawing.Color.Blue
        Label6.Location = New System.Drawing.Point(4, 16)
        Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(127, 25)
        Label6.TabIndex = 39
        Label6.Text = "Παραστατικό:"
        Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Label6.Visible = False
        '
        'DriverNameLabel
        '
        DriverNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DriverNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        DriverNameLabel.ForeColor = System.Drawing.Color.Blue
        DriverNameLabel.Location = New System.Drawing.Point(9, 63)
        DriverNameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        DriverNameLabel.Name = "DriverNameLabel"
        DriverNameLabel.Size = New System.Drawing.Size(145, 25)
        DriverNameLabel.TabIndex = 40
        DriverNameLabel.Text = "Αρ.Οχήματος:"
        DriverNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TrucksnoLabel
        '
        TrucksnoLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        TrucksnoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        TrucksnoLabel.ForeColor = System.Drawing.Color.Blue
        TrucksnoLabel.Location = New System.Drawing.Point(9, 101)
        TrucksnoLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        TrucksnoLabel.Name = "TrucksnoLabel"
        TrucksnoLabel.Size = New System.Drawing.Size(145, 25)
        TrucksnoLabel.TabIndex = 41
        TrucksnoLabel.Text = "Όνομα οδηγού:"
        TrucksnoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RemarksLabel
        '
        RemarksLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        RemarksLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        RemarksLabel.ForeColor = System.Drawing.Color.Blue
        RemarksLabel.Location = New System.Drawing.Point(9, 139)
        RemarksLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        RemarksLabel.Name = "RemarksLabel"
        RemarksLabel.Size = New System.Drawing.Size(145, 25)
        RemarksLabel.TabIndex = 42
        RemarksLabel.Text = "Παρατηρήσεις:"
        RemarksLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BindingNavigator
        '
        Me.BindingNavigator.AddNewItem = Nothing
        Me.BindingNavigator.BindingSource = Me.MTRLINEBindingSource
        Me.BindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator.DeleteItem = Nothing
        Me.BindingNavigator.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.BindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.BindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator.Name = "BindingNavigator"
        Me.BindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator.Size = New System.Drawing.Size(807, 31)
        Me.BindingNavigator.TabIndex = 0
        Me.BindingNavigator.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(45, 28)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(65, 27)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(29, 24)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        Me.BindingNavigatorAddNewItem.Visible = False
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'MasterBindingSource
        '
        '
        'txtBoxTrucksno
        '
        Me.txtBoxTrucksno.Location = New System.Drawing.Point(163, 64)
        Me.txtBoxTrucksno.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBoxTrucksno.Name = "txtBoxTrucksno"
        Me.txtBoxTrucksno.Size = New System.Drawing.Size(295, 20)
        Me.txtBoxTrucksno.TabIndex = 2
        '
        'FindocTextBox
        '
        Me.FindocTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MasterBindingSource, "findoc", True))
        Me.FindocTextBox.Location = New System.Drawing.Point(144, 16)
        Me.FindocTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.FindocTextBox.Name = "FindocTextBox"
        Me.FindocTextBox.Size = New System.Drawing.Size(132, 20)
        Me.FindocTextBox.TabIndex = 4
        Me.FindocTextBox.Visible = False
        '
        'txtBoxRemarks
        '
        Me.txtBoxRemarks.Location = New System.Drawing.Point(163, 139)
        Me.txtBoxRemarks.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBoxRemarks.Multiline = True
        Me.txtBoxRemarks.Name = "txtBoxRemarks"
        Me.txtBoxRemarks.Size = New System.Drawing.Size(295, 118)
        Me.txtBoxRemarks.TabIndex = 6
        '
        'txtBoxDriverName
        '
        Me.txtBoxDriverName.Location = New System.Drawing.Point(163, 102)
        Me.txtBoxDriverName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBoxDriverName.Name = "txtBoxDriverName"
        Me.txtBoxDriverName.Size = New System.Drawing.Size(295, 20)
        Me.txtBoxDriverName.TabIndex = 8
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 337)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1296, 26)
        Me.StatusStrip1.TabIndex = 43
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 31)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBoxDriverName)
        Me.SplitContainer1.Panel1.Controls.Add(RemarksLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBoxRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(TrucksnoLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FindocTextBox)
        Me.SplitContainer1.Panel1.Controls.Add(DriverNameLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBoxTrucksno)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.MTRLINEDataGridView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BindingNavigator)
        Me.SplitContainer1.Size = New System.Drawing.Size(1296, 306)
        Me.SplitContainer1.SplitterDistance = 485
        Me.SplitContainer1.TabIndex = 44
        '
        'MTRLINEDataGridView
        '
        Me.MTRLINEDataGridView.AllowUserToAddRows = False
        Me.MTRLINEDataGridView.AutoGenerateColumns = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.6!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MTRLINEDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MTRLINEDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MTRLINEDataGridView.DataSource = Me.MTRLINEBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.6!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MTRLINEDataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.MTRLINEDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MTRLINEDataGridView.Location = New System.Drawing.Point(0, 31)
        Me.MTRLINEDataGridView.Name = "MTRLINEDataGridView"
        Me.MTRLINEDataGridView.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.6!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MTRLINEDataGridView.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.MTRLINEDataGridView.RowHeadersWidth = 51
        Me.MTRLINEDataGridView.RowTemplate.Height = 24
        Me.MTRLINEDataGridView.Size = New System.Drawing.Size(807, 275)
        Me.MTRLINEDataGridView.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorSaveItem, Me.AboutToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1296, 31)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BindingNavigatorSaveItem
        '
        Me.BindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorSaveItem.Enabled = False
        Me.BindingNavigatorSaveItem.Image = CType(resources.GetObject("BindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorSaveItem.Name = "BindingNavigatorSaveItem"
        Me.BindingNavigatorSaveItem.Size = New System.Drawing.Size(29, 28)
        Me.BindingNavigatorSaveItem.Text = "Save Data"
        '
        'AboutToolStripButton
        '
        Me.AboutToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.AboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.AboutToolStripButton.Image = CType(resources.GetObject("AboutToolStripButton.Image"), System.Drawing.Image)
        Me.AboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AboutToolStripButton.Name = "AboutToolStripButton"
        Me.AboutToolStripButton.Size = New System.Drawing.Size(67, 28)
        Me.AboutToolStripButton.Text = "&About ..."
        '
        'ModifyDocBR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1296, 363)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ModifyDocBR"
        Me.Text = "Form1"
        CType(Me.BindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator.ResumeLayout(False)
        Me.BindingNavigator.PerformLayout()
        CType(Me.MTRLINEBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MTRLINEDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MasterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtBoxTrucksno As System.Windows.Forms.TextBox
    Friend WithEvents FindocTextBox As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxDriverName As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MTRLINEBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MTRLINEDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BindingNavigatorSaveItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents AboutToolStripButton As System.Windows.Forms.ToolStripButton
End Class
