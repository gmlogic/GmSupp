<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AAGenLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label9 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AAGenLogin))
        Me.lblUserName = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.txtboxConfirmPass = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.UsernameTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxOldPass = New System.Windows.Forms.TextBox()
        Me.lblOldPass = New System.Windows.Forms.Label()
        Me.ddlUsers = New System.Windows.Forms.ComboBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.txtBoxDecrypt = New System.Windows.Forms.TextBox()
        Me.ddlApplicant = New System.Windows.Forms.ComboBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnSetRoles = New System.Windows.Forms.Button()
        Me.PanelApplicant = New System.Windows.Forms.Panel()
        Me.lblRoles = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBoxName = New System.Windows.Forms.TextBox()
        Me.btnAddRole = New System.Windows.Forms.Button()
        Me.txtBoxRole = New System.Windows.Forms.TextBox()
        Me.ddlΗighers = New System.Windows.Forms.ComboBox()
        Me.txtBoxΗigher = New System.Windows.Forms.TextBox()
        Me.btnDeleteHigher = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblFacility = New System.Windows.Forms.Label()
        Me.BtnSetFacility = New System.Windows.Forms.Button()
        Me.GmChkListBoxRoles = New GmSupp.GmChkListBox()
        Me.ddlFacility = New System.Windows.Forms.ComboBox()
        Label9 = New System.Windows.Forms.Label()
        Me.PanelApplicant.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label9.ForeColor = System.Drawing.Color.Blue
        Label9.Location = New System.Drawing.Point(3, 9)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(68, 20)
        Label9.TabIndex = 267
        Label9.Text = "Ο ΑΙΤΩΝ :" & Global.Microsoft.VisualBasic.ChrW(9)
        Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUserName
        '
        Me.lblUserName.BackColor = System.Drawing.SystemColors.Control
        Me.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUserName.ForeColor = System.Drawing.Color.Blue
        Me.lblUserName.Location = New System.Drawing.Point(20, 89)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUserName.Size = New System.Drawing.Size(110, 23)
        Me.lblUserName.TabIndex = 5
        Me.lblUserName.Text = "&User:"
        Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.Color.Blue
        Me._lblLabels_1.Location = New System.Drawing.Point(20, 161)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(110, 23)
        Me._lblLabels_1.TabIndex = 7
        Me._lblLabels_1.Text = "&Κωδικός:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(403, 239)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(76, 26)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'txtboxConfirmPass
        '
        Me.txtboxConfirmPass.Location = New System.Drawing.Point(136, 187)
        Me.txtboxConfirmPass.Name = "txtboxConfirmPass"
        Me.txtboxConfirmPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtboxConfirmPass.Size = New System.Drawing.Size(343, 20)
        Me.txtboxConfirmPass.TabIndex = 3
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(136, 164)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(343, 20)
        Me.PasswordTextBox.TabIndex = 2
        '
        'UsernameTextBox
        '
        Me.UsernameTextBox.Location = New System.Drawing.Point(136, 92)
        Me.UsernameTextBox.Name = "UsernameTextBox"
        Me.UsernameTextBox.Size = New System.Drawing.Size(343, 20)
        Me.UsernameTextBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(20, 187)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(110, 23)
        Me.Label1.TabIndex = 184
        Me.Label1.Text = "&Επιβεβαίωση:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBoxOldPass
        '
        Me.txtBoxOldPass.Location = New System.Drawing.Point(136, 141)
        Me.txtBoxOldPass.Name = "txtBoxOldPass"
        Me.txtBoxOldPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtBoxOldPass.Size = New System.Drawing.Size(343, 20)
        Me.txtBoxOldPass.TabIndex = 1
        Me.txtBoxOldPass.Visible = False
        '
        'lblOldPass
        '
        Me.lblOldPass.BackColor = System.Drawing.SystemColors.Control
        Me.lblOldPass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOldPass.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOldPass.ForeColor = System.Drawing.Color.Blue
        Me.lblOldPass.Location = New System.Drawing.Point(20, 138)
        Me.lblOldPass.Name = "lblOldPass"
        Me.lblOldPass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOldPass.Size = New System.Drawing.Size(110, 23)
        Me.lblOldPass.TabIndex = 185
        Me.lblOldPass.Text = "Παλαιός Κωδικός:"
        Me.lblOldPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblOldPass.Visible = False
        '
        'ddlUsers
        '
        Me.ddlUsers.FormattingEnabled = True
        Me.ddlUsers.Location = New System.Drawing.Point(136, 92)
        Me.ddlUsers.Name = "ddlUsers"
        Me.ddlUsers.Size = New System.Drawing.Size(343, 21)
        Me.ddlUsers.TabIndex = 186
        '
        'cmdSelect
        '
        Me.cmdSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdSelect.Image = CType(resources.GetObject("cmdSelect.Image"), System.Drawing.Image)
        Me.cmdSelect.Location = New System.Drawing.Point(5, 239)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(38, 38)
        Me.cmdSelect.TabIndex = 188
        Me.cmdSelect.Visible = False
        '
        'txtBoxDecrypt
        '
        Me.txtBoxDecrypt.Location = New System.Drawing.Point(20, 213)
        Me.txtBoxDecrypt.Name = "txtBoxDecrypt"
        Me.txtBoxDecrypt.Size = New System.Drawing.Size(169, 20)
        Me.txtBoxDecrypt.TabIndex = 189
        '
        'ddlApplicant
        '
        Me.ddlApplicant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlApplicant.DisplayMember = "NAME"
        Me.ddlApplicant.FormattingEnabled = True
        Me.ddlApplicant.Location = New System.Drawing.Point(77, 7)
        Me.ddlApplicant.Name = "ddlApplicant"
        Me.ddlApplicant.Size = New System.Drawing.Size(202, 21)
        Me.ddlApplicant.TabIndex = 268
        Me.ddlApplicant.ValueMember = "UFTBL01"
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.SystemColors.Control
        Me.btnReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnReset.Location = New System.Drawing.Point(511, 163)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnReset.Size = New System.Drawing.Size(76, 26)
        Me.btnReset.TabIndex = 270
        Me.btnReset.Text = "Reset Pass"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'btnSetRoles
        '
        Me.btnSetRoles.BackColor = System.Drawing.SystemColors.Control
        Me.btnSetRoles.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSetRoles.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSetRoles.Location = New System.Drawing.Point(362, 9)
        Me.btnSetRoles.Name = "btnSetRoles"
        Me.btnSetRoles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSetRoles.Size = New System.Drawing.Size(76, 26)
        Me.btnSetRoles.TabIndex = 274
        Me.btnSetRoles.Text = "Set Roles"
        Me.btnSetRoles.UseVisualStyleBackColor = False
        '
        'PanelApplicant
        '
        Me.PanelApplicant.Controls.Add(Label9)
        Me.PanelApplicant.Controls.Add(Me.ddlApplicant)
        Me.PanelApplicant.Location = New System.Drawing.Point(518, 232)
        Me.PanelApplicant.Name = "PanelApplicant"
        Me.PanelApplicant.Size = New System.Drawing.Size(285, 39)
        Me.PanelApplicant.TabIndex = 275
        '
        'lblRoles
        '
        Me.lblRoles.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRoles.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRoles.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblRoles.ForeColor = System.Drawing.Color.Blue
        Me.lblRoles.Location = New System.Drawing.Point(5, 9)
        Me.lblRoles.Name = "lblRoles"
        Me.lblRoles.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRoles.Size = New System.Drawing.Size(56, 23)
        Me.lblRoles.TabIndex = 276
        Me.lblRoles.Text = "Roles:"
        Me.lblRoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(20, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(110, 23)
        Me.Label2.TabIndex = 278
        Me.Label2.Text = "Επωνυμία:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBoxName
        '
        Me.txtBoxName.Location = New System.Drawing.Point(136, 119)
        Me.txtBoxName.Name = "txtBoxName"
        Me.txtBoxName.Size = New System.Drawing.Size(343, 20)
        Me.txtBoxName.TabIndex = 279
        '
        'btnAddRole
        '
        Me.btnAddRole.BackColor = System.Drawing.SystemColors.Control
        Me.btnAddRole.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAddRole.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAddRole.Location = New System.Drawing.Point(362, 33)
        Me.btnAddRole.Name = "btnAddRole"
        Me.btnAddRole.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAddRole.Size = New System.Drawing.Size(76, 26)
        Me.btnAddRole.TabIndex = 280
        Me.btnAddRole.Text = "Add Role"
        Me.btnAddRole.UseVisualStyleBackColor = False
        Me.btnAddRole.Visible = False
        '
        'txtBoxRole
        '
        Me.txtBoxRole.Location = New System.Drawing.Point(187, 37)
        Me.txtBoxRole.Name = "txtBoxRole"
        Me.txtBoxRole.Size = New System.Drawing.Size(169, 20)
        Me.txtBoxRole.TabIndex = 281
        Me.txtBoxRole.Visible = False
        '
        'ddlΗighers
        '
        Me.ddlΗighers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlΗighers.DisplayMember = "NAME"
        Me.ddlΗighers.FormattingEnabled = True
        Me.ddlΗighers.Location = New System.Drawing.Point(136, 65)
        Me.ddlΗighers.Name = "ddlΗighers"
        Me.ddlΗighers.Size = New System.Drawing.Size(343, 21)
        Me.ddlΗighers.TabIndex = 283
        Me.ddlΗighers.ValueMember = "UFTBL01"
        '
        'txtBoxΗigher
        '
        Me.txtBoxΗigher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtBoxΗigher.Location = New System.Drawing.Point(485, 93)
        Me.txtBoxΗigher.Multiline = True
        Me.txtBoxΗigher.Name = "txtBoxΗigher"
        Me.txtBoxΗigher.ReadOnly = True
        Me.txtBoxΗigher.Size = New System.Drawing.Size(311, 45)
        Me.txtBoxΗigher.TabIndex = 284
        '
        'btnDeleteHigher
        '
        Me.btnDeleteHigher.BackColor = System.Drawing.SystemColors.Control
        Me.btnDeleteHigher.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnDeleteHigher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDeleteHigher.Image = CType(resources.GetObject("btnDeleteHigher.Image"), System.Drawing.Image)
        Me.btnDeleteHigher.Location = New System.Drawing.Point(576, 67)
        Me.btnDeleteHigher.Name = "btnDeleteHigher"
        Me.btnDeleteHigher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnDeleteHigher.Size = New System.Drawing.Size(27, 26)
        Me.btnDeleteHigher.TabIndex = 286
        Me.btnDeleteHigher.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(20, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 23)
        Me.Label3.TabIndex = 295
        Me.Label3.Text = "Ανώτεροι :" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(485, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 23)
        Me.Label4.TabIndex = 296
        Me.Label4.Text = "Ανώτερος :" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblFacility
        '
        Me.LblFacility.BackColor = System.Drawing.SystemColors.Control
        Me.LblFacility.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblFacility.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblFacility.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.LblFacility.ForeColor = System.Drawing.Color.Blue
        Me.LblFacility.Location = New System.Drawing.Point(444, 9)
        Me.LblFacility.Name = "LblFacility"
        Me.LblFacility.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblFacility.Size = New System.Drawing.Size(56, 23)
        Me.LblFacility.TabIndex = 299
        Me.LblFacility.Text = "Facility:"
        Me.LblFacility.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BtnSetFacility
        '
        Me.BtnSetFacility.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSetFacility.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSetFacility.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSetFacility.Location = New System.Drawing.Point(679, 9)
        Me.BtnSetFacility.Name = "BtnSetFacility"
        Me.BtnSetFacility.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnSetFacility.Size = New System.Drawing.Size(76, 26)
        Me.BtnSetFacility.TabIndex = 298
        Me.BtnSetFacility.Text = "Set Facility"
        Me.BtnSetFacility.UseVisualStyleBackColor = False
        '
        'GmChkListBoxRoles
        '
        Me.GmChkListBoxRoles.GmCheck = False
        Me.GmChkListBoxRoles.Location = New System.Drawing.Point(57, 6)
        Me.GmChkListBoxRoles.Name = "GmChkListBoxRoles"
        Me.GmChkListBoxRoles.Size = New System.Drawing.Size(300, 25)
        Me.GmChkListBoxRoles.TabIndex = 273
        '
        'ddlFacility
        '
        Me.ddlFacility.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlFacility.DisplayMember = "NAME"
        Me.ddlFacility.FormattingEnabled = True
        Me.ddlFacility.Location = New System.Drawing.Point(506, 9)
        Me.ddlFacility.Name = "ddlFacility"
        Me.ddlFacility.Size = New System.Drawing.Size(167, 21)
        Me.ddlFacility.TabIndex = 300
        Me.ddlFacility.ValueMember = "UFTBL01"
        '
        'AAGenLogin
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 283)
        Me.Controls.Add(Me.ddlFacility)
        Me.Controls.Add(Me.LblFacility)
        Me.Controls.Add(Me.BtnSetFacility)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnDeleteHigher)
        Me.Controls.Add(Me.txtBoxΗigher)
        Me.Controls.Add(Me.ddlΗighers)
        Me.Controls.Add(Me.txtBoxRole)
        Me.Controls.Add(Me.btnAddRole)
        Me.Controls.Add(Me.txtBoxName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblRoles)
        Me.Controls.Add(Me.GmChkListBoxRoles)
        Me.Controls.Add(Me.PanelApplicant)
        Me.Controls.Add(Me.btnSetRoles)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.txtBoxDecrypt)
        Me.Controls.Add(Me.cmdSelect)
        Me.Controls.Add(Me.ddlUsers)
        Me.Controls.Add(Me.txtBoxOldPass)
        Me.Controls.Add(Me.lblOldPass)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtboxConfirmPass)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UsernameTextBox)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me.cmdOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AAGenLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AAGenLogin"
        Me.PanelApplicant.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblUserName As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents txtboxConfirmPass As TextBox
    Friend WithEvents PasswordTextBox As TextBox
    Friend WithEvents UsernameTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBoxOldPass As TextBox
    Public WithEvents lblOldPass As Label
    Friend WithEvents ddlUsers As ComboBox
    Friend WithEvents cmdSelect As Button
    Friend WithEvents txtBoxDecrypt As TextBox
    Friend WithEvents ddlApplicant As ComboBox
    Public WithEvents btnReset As Button
    Friend WithEvents GmChkListBoxRoles As GmChkListBox
    Public WithEvents btnSetRoles As Button
    Friend WithEvents PanelApplicant As Panel
    Public WithEvents lblRoles As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBoxName As TextBox
    Public WithEvents btnAddRole As Button
    Friend WithEvents txtBoxRole As TextBox
    Friend WithEvents ddlΗighers As ComboBox
    Friend WithEvents txtBoxΗigher As TextBox
    Public WithEvents btnDeleteHigher As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Public WithEvents LblFacility As Label
    Public WithEvents BtnSetFacility As Button
    Friend WithEvents ddlFacility As ComboBox
End Class
