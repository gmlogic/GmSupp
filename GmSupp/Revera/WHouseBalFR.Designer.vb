<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WHouseBalFR
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
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ddlFromcccTrdDep = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ddlcccTrdDep = New System.Windows.Forms.ComboBox()
        Me.ddlTrdr = New System.Windows.Forms.ComboBox()
        Me.txtBoxRequestNo = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ddlcCCManager1 = New System.Windows.Forms.ComboBox()
        Me.txtBoxFrom = New System.Windows.Forms.TextBox()
        Me.ddlApplicant = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBoxEmailBody = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ddlcCCManager = New System.Windows.Forms.ComboBox()
        Me.txtBoxREMARKS = New System.Windows.Forms.TextBox()
        Me.txtBoxVARCHAR01 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.ddlΗighers = New System.Windows.Forms.ComboBox()
        Me.GmChkListBoxRecipients = New GmSupp.GmChkListBox()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label1.ForeColor = System.Drawing.Color.Blue
        Label1.Location = New System.Drawing.Point(771, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(189, 31)
        Label1.TabIndex = 257
        Label1.Text = "Ημ/νία"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label2.ForeColor = System.Drawing.Color.Blue
        Label2.Location = New System.Drawing.Point(3, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(186, 31)
        Label2.TabIndex = 258
        Label2.Tag = ""
        Label2.Text = "Από Τμήμα"
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label3.ForeColor = System.Drawing.Color.Blue
        Label3.Location = New System.Drawing.Point(195, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(186, 31)
        Label3.TabIndex = 259
        Label3.Tag = "Trdr"
        Label3.Text = "Για Κωδ.Πελάτη"
        Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label5.ForeColor = System.Drawing.Color.Blue
        Label5.Location = New System.Drawing.Point(24, 20)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(245, 20)
        Label5.TabIndex = 259
        Label5.Text = "Χρόνος Επιθυμητής Παράδοσης :" & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9)
        Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label7.ForeColor = System.Drawing.Color.Blue
        Label7.Location = New System.Drawing.Point(33, 115)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(245, 48)
        Label7.TabIndex = 261
        Label7.Text = "ΑΙΤΙΟΛΟΓΗΣΗ ΔΑΠΑΝΗΣ  Εσωτ.Παρατηρήσεις :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Έως: 100 χαρακτήρες" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label8.ForeColor = System.Drawing.Color.Blue
        Label8.Location = New System.Drawing.Point(33, 71)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(245, 20)
        Label8.TabIndex = 262
        Label8.Text = "Παρατηρήσεις:"
        Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label9.ForeColor = System.Drawing.Color.Blue
        Label9.Location = New System.Drawing.Point(9, 10)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(76, 20)
        Label9.TabIndex = 263
        Label9.Text = "Ο ΑΙΤΩΝ :" & Global.Microsoft.VisualBasic.ChrW(9)
        Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label10.ForeColor = System.Drawing.Color.Blue
        Label10.Location = New System.Drawing.Point(296, 152)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(151, 20)
        Label10.TabIndex = 264
        Label10.Text = "Έγκριση ανωτέρου :"
        Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label11.ForeColor = System.Drawing.Color.Blue
        Label11.Location = New System.Drawing.Point(9, 194)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(172, 20)
        Label11.TabIndex = 265
        Label11.Text = "Διευθυντής Τμήματος :"
        Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label12.ForeColor = System.Drawing.Color.Blue
        Label12.Location = New System.Drawing.Point(387, 0)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(186, 31)
        Label12.TabIndex = 261
        Label12.Tag = "ITELINES.cccTrdDep_cccTrdDep_Name"
        Label12.Text = "Για Τμήμα Πελάτη"
        Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label4.ForeColor = System.Drawing.Color.Blue
        Label4.Location = New System.Drawing.Point(579, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(186, 31)
        Label4.TabIndex = 277
        Label4.Text = "ΑΡΙΘΜΟΣ ΑΙΤΗΣΗΣ"
        Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label6.ForeColor = System.Drawing.Color.Blue
        Label6.Location = New System.Drawing.Point(9, 228)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(172, 20)
        Label6.TabIndex = 281
        Label6.Text = "Διευθυντής  Εργοστασίου :"
        Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Label15.ForeColor = System.Drawing.Color.Blue
        Label15.Location = New System.Drawing.Point(288, 186)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(160, 20)
        Label15.TabIndex = 288
        Label15.Text = "Κοινοποίηση Αίτησης σε :"
        Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GmChkListBoxRecipients)
        Me.SplitContainer1.Panel2.Controls.Add(Label15)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtBoxREMARKS)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtBoxVARCHAR01)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePicker2)
        Me.SplitContainer1.Panel2.Controls.Add(Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Cancel)
        Me.SplitContainer1.Panel2.Controls.Add(Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.OK)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ddlΗighers)
        Me.SplitContainer1.Panel2.Controls.Add(Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Label10)
        Me.SplitContainer1.Size = New System.Drawing.Size(1028, 404)
        Me.SplitContainer1.SplitterDistance = 80
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ddlFromcccTrdDep, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Label3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Label1, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateTimePicker1, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Label12, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ddlcccTrdDep, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ddlTrdr, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Label4, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtBoxRequestNo, 3, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 22)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(963, 59)
        Me.TableLayoutPanel1.TabIndex = 256
        '
        'ddlFromcccTrdDep
        '
        Me.ddlFromcccTrdDep.DisplayMember = "NAME"
        Me.ddlFromcccTrdDep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ddlFromcccTrdDep.FormattingEnabled = True
        Me.ddlFromcccTrdDep.Location = New System.Drawing.Point(3, 34)
        Me.ddlFromcccTrdDep.Name = "ddlFromcccTrdDep"
        Me.ddlFromcccTrdDep.Size = New System.Drawing.Size(186, 21)
        Me.ddlFromcccTrdDep.TabIndex = 266
        Me.ddlFromcccTrdDep.ValueMember = "UFTBL02"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(771, 34)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(189, 20)
        Me.DateTimePicker1.TabIndex = 251
        Me.DateTimePicker1.Value = New Date(2006, 10, 31, 0, 0, 0, 0)
        '
        'ddlcccTrdDep
        '
        Me.ddlcccTrdDep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlcccTrdDep.DisplayMember = "NAME"
        Me.ddlcccTrdDep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ddlcccTrdDep.FormattingEnabled = True
        Me.ddlcccTrdDep.Location = New System.Drawing.Point(387, 34)
        Me.ddlcccTrdDep.Name = "ddlcccTrdDep"
        Me.ddlcccTrdDep.Size = New System.Drawing.Size(186, 21)
        Me.ddlcccTrdDep.TabIndex = 262
        Me.ddlcccTrdDep.ValueMember = "cccTrdDep"
        '
        'ddlTrdr
        '
        Me.ddlTrdr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlTrdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.ddlTrdr.DisplayMember = "NAME"
        Me.ddlTrdr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ddlTrdr.FormattingEnabled = True
        Me.ddlTrdr.Location = New System.Drawing.Point(195, 34)
        Me.ddlTrdr.Name = "ddlTrdr"
        Me.ddlTrdr.Size = New System.Drawing.Size(186, 21)
        Me.ddlTrdr.TabIndex = 263
        Me.ddlTrdr.ValueMember = "TRDR"
        '
        'txtBoxRequestNo
        '
        Me.txtBoxRequestNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtBoxRequestNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBoxRequestNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtBoxRequestNo.Location = New System.Drawing.Point(579, 34)
        Me.txtBoxRequestNo.Name = "txtBoxRequestNo"
        Me.txtBoxRequestNo.ReadOnly = True
        Me.txtBoxRequestNo.Size = New System.Drawing.Size(186, 20)
        Me.txtBoxRequestNo.TabIndex = 276
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Label9)
        Me.Panel1.Controls.Add(Me.ddlcCCManager1)
        Me.Panel1.Controls.Add(Me.txtBoxFrom)
        Me.Panel1.Controls.Add(Label6)
        Me.Panel1.Controls.Add(Me.ddlApplicant)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtBoxEmailBody)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Label11)
        Me.Panel1.Controls.Add(Me.ddlcCCManager)
        Me.Panel1.Location = New System.Drawing.Point(62, 297)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(611, 104)
        Me.Panel1.TabIndex = 287
        Me.Panel1.Visible = False
        '
        'ddlcCCManager1
        '
        Me.ddlcCCManager1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlcCCManager1.DisplayMember = "cCCManager"
        Me.ddlcCCManager1.FormattingEnabled = True
        Me.ddlcCCManager1.Location = New System.Drawing.Point(187, 227)
        Me.ddlcCCManager1.Name = "ddlcCCManager1"
        Me.ddlcCCManager1.Size = New System.Drawing.Size(249, 21)
        Me.ddlcCCManager1.TabIndex = 282
        Me.ddlcCCManager1.ValueMember = "UFTBL01"
        '
        'txtBoxFrom
        '
        Me.txtBoxFrom.Location = New System.Drawing.Point(91, 43)
        Me.txtBoxFrom.Name = "txtBoxFrom"
        Me.txtBoxFrom.Size = New System.Drawing.Size(213, 20)
        Me.txtBoxFrom.TabIndex = 286
        '
        'ddlApplicant
        '
        Me.ddlApplicant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlApplicant.DisplayMember = "NAME"
        Me.ddlApplicant.FormattingEnabled = True
        Me.ddlApplicant.Location = New System.Drawing.Point(91, 8)
        Me.ddlApplicant.Name = "ddlApplicant"
        Me.ddlApplicant.Size = New System.Drawing.Size(249, 21)
        Me.ddlApplicant.TabIndex = 266
        Me.ddlApplicant.ValueMember = "UFTBL01"
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(9, 40)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 20)
        Me.Label13.TabIndex = 285
        Me.Label13.Text = "From:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBoxEmailBody
        '
        Me.txtBoxEmailBody.Location = New System.Drawing.Point(9, 87)
        Me.txtBoxEmailBody.Multiline = True
        Me.txtBoxEmailBody.Name = "txtBoxEmailBody"
        Me.txtBoxEmailBody.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtBoxEmailBody.Size = New System.Drawing.Size(574, 86)
        Me.txtBoxEmailBody.TabIndex = 283
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(9, 67)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(76, 20)
        Me.Label14.TabIndex = 284
        Me.Label14.Text = "Email Body"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ddlcCCManager
        '
        Me.ddlcCCManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlcCCManager.DisplayMember = "cCCManager"
        Me.ddlcCCManager.FormattingEnabled = True
        Me.ddlcCCManager.Location = New System.Drawing.Point(187, 193)
        Me.ddlcCCManager.Name = "ddlcCCManager"
        Me.ddlcCCManager.Size = New System.Drawing.Size(249, 21)
        Me.ddlcCCManager.TabIndex = 272
        Me.ddlcCCManager.ValueMember = "UFTBL01"
        '
        'txtBoxREMARKS
        '
        Me.txtBoxREMARKS.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtBoxREMARKS.Location = New System.Drawing.Point(296, 58)
        Me.txtBoxREMARKS.Multiline = True
        Me.txtBoxREMARKS.Name = "txtBoxREMARKS"
        Me.txtBoxREMARKS.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtBoxREMARKS.Size = New System.Drawing.Size(728, 50)
        Me.txtBoxREMARKS.TabIndex = 280
        '
        'txtBoxVARCHAR01
        '
        Me.txtBoxVARCHAR01.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtBoxVARCHAR01.Location = New System.Drawing.Point(297, 129)
        Me.txtBoxVARCHAR01.Name = "txtBoxVARCHAR01"
        Me.txtBoxVARCHAR01.Size = New System.Drawing.Size(728, 20)
        Me.txtBoxVARCHAR01.TabIndex = 279
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(288, 20)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(189, 20)
        Me.DateTimePicker2.TabIndex = 278
        Me.DateTimePicker2.Value = New Date(2006, 10, 31, 0, 0, 0, 0)
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(837, 276)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(94, 23)
        Me.Cancel.TabIndex = 274
        Me.Cancel.Text = "&Cancel"
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(734, 276)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(94, 23)
        Me.OK.TabIndex = 273
        Me.OK.Text = "&OK"
        '
        'ddlΗighers
        '
        Me.ddlΗighers.DisplayMember = "ccCChief"
        Me.ddlΗighers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlΗighers.FormattingEnabled = True
        Me.ddlΗighers.Location = New System.Drawing.Point(453, 153)
        Me.ddlΗighers.Name = "ddlΗighers"
        Me.ddlΗighers.Size = New System.Drawing.Size(249, 21)
        Me.ddlΗighers.TabIndex = 271
        Me.ddlΗighers.ValueMember = "UFTBL01"
        '
        'GmChkListBoxRecipients
        '
        Me.GmChkListBoxRecipients.GmCheck = False
        Me.GmChkListBoxRecipients.Location = New System.Drawing.Point(453, 186)
        Me.GmChkListBoxRecipients.Name = "GmChkListBoxRecipients"
        Me.GmChkListBoxRecipients.Size = New System.Drawing.Size(300, 25)
        Me.GmChkListBoxRecipients.TabIndex = 289
        '
        'WHouseBalFR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 404)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "WHouseBalFR"
        Me.Text = "Αίτηση"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ddlFromcccTrdDep As ComboBox
    Friend WithEvents ddlcccTrdDep As ComboBox
    Friend WithEvents ddlTrdr As ComboBox
    Friend WithEvents ddlApplicant As ComboBox
    Friend WithEvents ddlcCCManager As ComboBox
    Friend WithEvents ddlΗighers As ComboBox
    Friend WithEvents Cancel As Button
    Friend WithEvents OK As Button
    Friend WithEvents txtBoxRequestNo As TextBox
    Friend WithEvents txtBoxVARCHAR01 As TextBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents txtBoxREMARKS As TextBox
    Friend WithEvents ddlcCCManager1 As ComboBox
    Friend WithEvents txtBoxFrom As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtBoxEmailBody As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GmChkListBoxRecipients As GmChkListBox
End Class
