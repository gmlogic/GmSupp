<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GmSearchCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GmSearchCtrl))
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TlSBtnFind = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnExit = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnUnCheck = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnCheck = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorNextForm = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.CnMenuGmDgLookUp = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CnMenuGmDgLookUp1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CntxtGmDgLookUp = New System.Windows.Forms.ToolStripTextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CnMenuGmDgLookUp.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TlSBtnFind
        '
        Me.TlSBtnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnFind.Image = CType(resources.GetObject("TlSBtnFind.Image"), System.Drawing.Image)
        Me.TlSBtnFind.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnFind.Name = "TlSBtnFind"
        Me.TlSBtnFind.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnFind.Text = "ToolStripButton1"
        '
        'TlSBtnExit
        '
        Me.TlSBtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TlSBtnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnExit.Image = CType(resources.GetObject("TlSBtnExit.Image"), System.Drawing.Image)
        Me.TlSBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlSBtnExit.Name = "TlSBtnExit"
        Me.TlSBtnExit.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnExit.Text = "ToolStripButton3"
        Me.TlSBtnExit.ToolTipText = "Έξοδος"
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
        'BindingNavigatorNextForm
        '
        Me.BindingNavigatorNextForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorNextForm.Image = CType(resources.GetObject("BindingNavigatorNextForm.Image"), System.Drawing.Image)
        Me.BindingNavigatorNextForm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BindingNavigatorNextForm.Name = "BindingNavigatorNextForm"
        Me.BindingNavigatorNextForm.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorNextForm.Text = "BindingNavigatorNextForm"
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
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
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
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
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
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 46)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(658, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BindingNavigator1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(658, 416)
        Me.SplitContainer1.SplitterDistance = 344
        Me.SplitContainer1.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(658, 319)
        Me.DataGridView1.TabIndex = 1
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.BindingNavigatorNextForm, Me.ToolStripSeparator3, Me.TlSBtnCheck, Me.TlSBtnUnCheck, Me.ToolStripSeparator4, Me.TlSBtnExit, Me.TlSBtnFind, Me.ToolStripComboBox1})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(658, 25)
        Me.BindingNavigator1.TabIndex = 0
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
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
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(250, 25)
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(658, 68)
        Me.DataGridView2.TabIndex = 0
        '
        'CnMenuGmDgLookUp
        '
        Me.CnMenuGmDgLookUp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CnMenuGmDgLookUp1, Me.CntxtGmDgLookUp})
        Me.CnMenuGmDgLookUp.Name = "CnMenuGmDgLookUp"
        Me.CnMenuGmDgLookUp.Size = New System.Drawing.Size(182, 51)
        '
        'CnMenuGmDgLookUp1
        '
        Me.CnMenuGmDgLookUp1.Name = "CnMenuGmDgLookUp1"
        Me.CnMenuGmDgLookUp1.Size = New System.Drawing.Size(181, 22)
        Me.CnMenuGmDgLookUp1.Text = "ToolStripMenuItem1"
        '
        'CntxtGmDgLookUp
        '
        Me.CntxtGmDgLookUp.Name = "CntxtGmDgLookUp"
        Me.CntxtGmDgLookUp.Size = New System.Drawing.Size(100, 23)
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'GmSearchCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "GmSearchCtrl"
        Me.Size = New System.Drawing.Size(658, 416)
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CnMenuGmDgLookUp.ResumeLayout(False)
        Me.CnMenuGmDgLookUp.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BindingSource2 As BindingSource
    Friend WithEvents TlSBtnFind As ToolStripButton
    Friend WithEvents TlSBtnExit As ToolStripButton
    Friend WithEvents TlSBtnUnCheck As ToolStripButton
    Friend WithEvents TlSBtnCheck As ToolStripButton
    Friend WithEvents BindingNavigatorNextForm As ToolStripButton
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents BindingNavigator1 As BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents CnMenuGmDgLookUp As ContextMenuStrip
    Friend WithEvents CnMenuGmDgLookUp1 As ToolStripMenuItem
    Friend WithEvents CntxtGmDgLookUp As ToolStripTextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents BindingSource1 As BindingSource
End Class
