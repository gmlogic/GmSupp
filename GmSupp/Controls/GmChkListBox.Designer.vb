<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GmChkListBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GmChkListBox))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tlsTop = New System.Windows.Forms.ToolStrip()
        Me.TlStxtBox = New System.Windows.Forms.ToolStripTextBox()
        Me.TlStripBtn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.tlsBottom = New System.Windows.Forms.ToolStrip()
        Me.TlSBtnUnCheck = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnCheck = New System.Windows.Forms.ToolStripButton()
        Me.TlSBtnAdd = New System.Windows.Forms.ToolStripButton()
        Me.Panel1.SuspendLayout()
        Me.tlsTop.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlsBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.tlsTop)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.tlsBottom)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(300, 150)
        Me.Panel1.TabIndex = 266
        '
        'tlsTop
        '
        Me.tlsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TlStxtBox, Me.TlStripBtn, Me.ToolStripSeparator1})
        Me.tlsTop.Location = New System.Drawing.Point(0, 0)
        Me.tlsTop.Name = "tlsTop"
        Me.tlsTop.Size = New System.Drawing.Size(296, 25)
        Me.tlsTop.TabIndex = 263
        '
        'TlStxtBox
        '
        Me.TlStxtBox.AutoSize = False
        Me.TlStxtBox.Name = "TlStxtBox"
        Me.TlStxtBox.Size = New System.Drawing.Size(170, 25)
        '
        'TlStripBtn
        '
        Me.TlStripBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TlStripBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlStripBtn.Image = CType(resources.GetObject("TlStripBtn.Image"), System.Drawing.Image)
        Me.TlStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TlStripBtn.Name = "TlStripBtn"
        Me.TlStripBtn.Size = New System.Drawing.Size(23, 22)
        Me.TlStripBtn.Tag = ""
        Me.TlStripBtn.Text = "&Open"
        Me.TlStripBtn.ToolTipText = "Κωδικοί  Κίνησης"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgv)
        Me.Panel2.Location = New System.Drawing.Point(0, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(290, 87)
        Me.Panel2.TabIndex = 265
        '
        'MasterDataGridView
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.Name = "MasterDataGridView"
        Me.dgv.Size = New System.Drawing.Size(290, 87)
        Me.dgv.TabIndex = 267
        '
        'tlsBottom
        '
        Me.tlsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlsBottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsBottom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TlSBtnUnCheck, Me.TlSBtnCheck, Me.TlSBtnAdd})
        Me.tlsBottom.Location = New System.Drawing.Point(0, 121)
        Me.tlsBottom.Name = "tlsBottom"
        Me.tlsBottom.Size = New System.Drawing.Size(296, 25)
        Me.tlsBottom.TabIndex = 264
        Me.tlsBottom.Text = "ToolStrip3"
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
        'TlSBtnAdd
        '
        Me.TlSBtnAdd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TlSBtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TlSBtnAdd.Image = CType(resources.GetObject("TlSBtnAdd.Image"), System.Drawing.Image)
        Me.TlSBtnAdd.Name = "TlSBtnAdd"
        Me.TlSBtnAdd.RightToLeftAutoMirrorImage = True
        Me.TlSBtnAdd.Size = New System.Drawing.Size(23, 22)
        Me.TlSBtnAdd.Text = "Add new"
        '
        'GmChkListBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "GmChkListBox"
        Me.Size = New System.Drawing.Size(299, 33)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlsTop.ResumeLayout(False)
        Me.tlsTop.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlsBottom.ResumeLayout(False)
        Me.tlsBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents tlsTop As ToolStrip
    Friend WithEvents TlStxtBox As ToolStripTextBox
    Friend WithEvents TlStripBtn As ToolStripButton
    Friend WithEvents tlsBottom As ToolStrip
    Friend WithEvents TlSBtnUnCheck As ToolStripButton
    Friend WithEvents TlSBtnCheck As ToolStripButton
    Friend WithEvents TlSBtnAdd As ToolStripButton
    Friend WithEvents dgv As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
End Class
