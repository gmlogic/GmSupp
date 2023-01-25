Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Transactions
Imports System.Windows.Forms
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports Softone

Public Class ModifyDocBR

#Region "00-Declare Variables"
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()

    'Dim WithEvents FormMTRL1_CODE As Form
    'Dim WithEvents Search_Code As New System.Windows.Forms.DataGridViewTextBoxColumn
    'Dim DataGridViewTextBoxColumnMTRL1_CODE As New System.Windows.Forms.DataGridViewTextBoxColumn
    Dim conn As String '= My.Settings.PANELConnectionString
    Dim OldText As String
#End Region
#Region "001-Declare Propertys"
    Public Property MtrDocTable As XTable
    Public Property SalDocTable As XTable
    Public Property MtrLinesTable As XTable

    Private drvValue As Hglp.FINDOC
    Public Property CurDrv() As Hglp.FINDOC
        Get
            Return drvValue
        End Get
        Set(ByVal value As Hglp.FINDOC)
            drvValue = value
        End Set
    End Property

    Private RefreshValue As Boolean
    Public Property DgdvRefresh() As Boolean
        Get
            Return RefreshValue
        End Get
        Set(ByVal value As Boolean)
            RefreshValue = value
        End Set
    End Property


#End Region
#Region "01-Load Form"
    Private Sub Me_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()   'SendKeys.Send("{TAB}")
        End If
        'MessageBox.Show(this.ActiveControl.Name);
        Dim activeCtrl As Control = GetActiveControl(Me)
        If activeCtrl IsNot Nothing Then
            System.Diagnostics.Debug.WriteLine("Focused: " & activeCtrl.Name)
            'MessageBox.Show("Focused: " + activeCtrl.Name);
            System.Diagnostics.Debug.WriteLine("Active: " & Me.ActiveControl.Name)
        Else
            'MessageBox.Show("Focused: *** NONE ***");
            System.Diagnostics.Debug.WriteLine("Focused: *** NONE ***")
        End If

        If TypeOf activeCtrl Is TextBox Or TypeOf activeCtrl Is ComboBox Or TypeOf activeCtrl Is DateTimePicker Then
            Dim NextControl As Boolean = False
            Select Case e.KeyCode
                Case Keys.Enter
                    'PELNAME1ComboBox
                    ''Dim arCtrl() As String = ("PELNAME1ComboBox,APOKEYComboBox,C_QUALITYComboBox,C_COLORComboBox,C_ANDRNOTextBox,C_GYNNOTextBox,C_ANDRDESCRComboBox,C_ANDRDATETextBox,C_GYNDESCRComboBox,C_GYNDATETextBox,NOTESTextBox,C_GRAMTextBox,C_WEIGHTTextBox,C_WRITINGTextBox,C_SPECIFIC_COSTTextBox,C_PETRES_COSTTextBox,PackingChargesTextBox,OtherChargesTextBox,EKP_01TextBox,EKAXIATextBox,TR_AP_PINAKD_04ComboBox,AR_APODIKSHSTextBox,TR_PL_PINAKD_05ComboBox,PreviousBalanceTextBox,GrandTotalTextBox").Split(",")
                    ''For i As Integer = 0 To arCtrl.Length - 1
                    ''    If activeCtrl.Name = arCtrl(i) Then
                    ''        Dim ctrls() As Control = Me.Controls.Find(arCtrl(i + 1), True)
                    ''        If ctrls.Length > 0 Then
                    ''            ctrls(0).Focus()
                    ''        End If
                    ''    End If
                    ''Next
                    ''If activeCtrl.Name = "PayableTextBox" Then 'The Last TextBox
                    ''    Me.C_WEIGHTTextBox.Focus()
                    ''End If

                    'Dim nextCtrl As Control = GetNextControl(activeCtrl, True)
                    'ProcessControls(Me)
                    ''WhatControl(Me)
                    NextControl = True
                Case Keys.Up
                    'NextControl = False
                Case Keys.Down
                    'NextControl = True
                Case Else
                    Exit Sub
            End Select

            SelectNextControl(Me.ActiveControl, NextControl, True, True, True)
            'Me.Controls
            'SendKeys.Send("{TAB}")
        End If
        If TypeOf Me.ActiveControl Is DataGridView Then
            If e.KeyCode = Keys.Enter Then
                'SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Private Sub ProcessControls(ByVal ctrlContainer As Control)
        For Each ctrl As Control In ctrlContainer.Controls
            If TypeOf ctrl Is TextBox Then
                ' Do whatever to the TextBox
            End If
            ' If the control has children,
            ' recursively call this function
            If ctrl.HasChildren Then
                ProcessControls(ctrl)
            End If
        Next
    End Sub
    Public Sub WhatControl(ByVal controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            ' do something to ctrl
            MessageBox.Show(ctrl.Name)
            ' recurse through all child controls
            WhatControl(ctrl.Controls)
        Next
    End Sub
    Private Function GetActiveControl(ByVal parent As Control) As Control
        If parent.Focused Then
            Return parent
        End If

        For i As Integer = 0 To parent.Controls.Count - 1
            Dim activeCtrl As Control = GetActiveControl(parent.Controls(i))
            If activeCtrl IsNot Nothing Then
                Return activeCtrl
            End If
        Next

        Return Nothing
    End Function
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.KeyPreview = True
        ' Load the data.
        LoadData()

        'Me.TextBox1.Text = "AFTER CHANGE SALDOC_REMARKS " & SalDocTable(0, "FINDOC").ToString
        'FullName Is : MTRDOC.TRUCKSNO
        'Caption Is : Αριθ.Μεταφ.μέσου
        If Not IsDBNull(MtrDocTable(0, "TRUCKSNO")) Then
            OldText = MtrDocTable(0, "TRUCKSNO")
            Me.txtBoxTrucksno.Text = OldText ' MasterBindingSource - trucksno
        End If
        'FullName Is : Saldoc.VARCHAR02
        'Caption Is : Όνομα οδηγού
        If Not IsDBNull(SalDocTable(0, "VARCHAR02")) Then
            OldText = SalDocTable(0, "VARCHAR02")
            Me.txtBoxDriverName.Text = OldText 'MasterBindingSource - VARCHAR02
        End If
        'FullName Is : Saldoc.REMARKS
        'Caption Is : Παρατηρήσεις
        If Not IsDBNull(SalDocTable(0, "REMARKS")) Then
            OldText = SalDocTable(0, "REMARKS")
            Me.txtBoxRemarks.Text = OldText  'MasterBindingSource - REMARKS
        End If


        Me.KeyPreview = True

    End Sub
    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = (Not DataSafe())
    End Sub

#End Region
#Region "04-Bas_Commands"
    Private Sub Cmd_Add()
        Try
            Me.MasterBindingSource.AddNew()
            'For New Recode Standart Settings
            Me.MasterBindingSource.EndEdit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Select()

    End Sub
#End Region
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        Me.MasterBindingSource.EndEdit()
    End Sub
    Private Function DataSafe() As Boolean
        DataSafe = True
        ' Finish any current edits.
        EndAllEdits()

        If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function

        ' Ask the user if we should save the changes.
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) 'MeLabel)
            Case MsgBoxResult.No
                ' The data is not safe.
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                Return False
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
                If Not Conditions() Then
                    Exit Function
                End If
            End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            Dim tx As IO.TextWriter
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    'Dim NFINDOC As Panel.FINDOC = Me.MasterBindingSource.Current
                    'If Not db.GetChangeSet.Deletes.Count = 0 Then
                    '    '' Delete
                    '    For Each deleted As Object In db.GetChangeSet.Deletes
                    '        If deleted.GetType.ToString.Contains("FINDOC") Then
                    '            'Dim DMTRLINE As MTRLINE = deleted
                    '            Dim DFINDOC As Panel.FINDOC = deleted 'DMTRLINE.FINDOC1
                    '            'db.FINDOCs.DeleteOnSubmit(DFINDOC)
                    '            'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                    '            Dim snum As Panel.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = DFINDOC.COMPANY And f.SOSOURCE = DFINDOC.SOSOURCE And f.SERIES = DFINDOC.SERIES And f.FISCPRD = DFINDOC.FISCPRD).FirstOrDefault
                    '            If snum.SERIESNUM = DFINDOC.SERIESNUM Then
                    '                snum.SERIESNUM -= 1
                    '            End If
                    '            'Dim TMK As TIMKIN = db.TIMKINs.Where(Function(p) p.Company = Company And p.PELPRO = PelPro And p.CODE_KIN = CurDrv.CODE_KIN).SingleOrDefault
                    '            'If TMK.KIN_NO = CurDrv.TIMNO_ Then
                    '            '    TMK.KIN_NO -= 1
                    '            'End If

                    '            'Dim ser As GmGoldDataSet.TIMKINDataTable = TIMKINTableAdapter.GetDataBy(PelPro, mdt1.Rows(0)("CODE_KIN", DataRowVersion.Original)) 'CurrentDataRowView("PELPRO"),CurrentDataRowView("CODE_KIN")) '  drv("SOSOURCE"), drv("SERIES"), drv("FISCPRD"), drv("COMPANY"))
                    '            '    RsWhere = "Company = " & Company & " AND PELPRO = " & PelPro
                    '            '    If ser(0).KIN_NO = mdt1.Rows(0)("TIMNO_", DataRowVersion.Original) Then
                    '            '        df.GmExecuteNonQuery("UPDATE TIMKIN SET KIN_NO = KIN_NO - 1 WHERE " & RsWhere & " AND CODE_KIN = " & mdt1.Rows(0)("CODE_KIN", DataRowVersion.Original))
                    '            '    End If
                    '        End If
                    '    Next
                    'Else
                    '    'Dim T1 As Tim1Info = Me.MasterBindingSource.Current
                    '    'Dim TIM4ID As Integer = 0
                    '    'For Each T4 As Tim4Info In Me.DetailBindingSource
                    '    '    TIM4ID -= 1
                    '    '    If T4.TIM4ID = 0 Then
                    '    '        T4.TIM4ID = TIM4ID
                    '    '    End If
                    '    '    T1.Tim4Infos.Add(T4)
                    '    'Next
                    '    If Not db.GetChangeSet.Inserts.Count = 0 Then
                    '        'Dim TFINDOC As FINDOC = Me.MasterBindingSource.Current
                    '        Dim ccCRouting As Integer = 0
                    '        If NFINDOC.FINDOC > 0 Then
                    '            ccCRouting = db.ccCRouting.Where(Function(f) f.FINDOC = NFINDOC.FINDOC).Max(Function(f) f.ccCRouting)
                    '        End If
                    '        For Each insertion As Object In db.GetChangeSet.Inserts
                    '            If insertion.GetType.ToString.Contains("FINDOC") Then
                    '                NFINDOC = insertion
                    '                'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                    '                Dim snum As Panel.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
                    '                snum.SERIESNUM += 1
                    '                NFINDOC.SERIESNUM = snum.SERIESNUM
                    '                Dim fmt As String = ""
                    '                Select Case NSeries
                    '                    Case 9520
                    '                        fmt = "ΕΞΔ0000000"
                    '                    Case 9521
                    '                        fmt = "ΕΙΣΔ000000"
                    '                    Case 9522
                    '                        fmt = "ΕΞΧ0000000"
                    '                    Case 9523
                    '                        fmt = "ΕΙΣΧ000000"
                    '                    Case 9524
                    '                        fmt = "ΧΡΣΞ000000"
                    '                    Case 9590
                    '                        fmt = "ΑΠΓΡ000000"
                    '                End Select
                    '                Select Case ToTransform
                    '                    Case 9593
                    '                        fmt = "ΠΑΦ0000000"
                    '                    Case 9594
                    '                        fmt = "ΠΑΒ0000000"
                    '                End Select
                    '                NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
                    '                'Dim TMK As TIMKIN = db.TIMKINs.Where(Function(p) p.Company = Company And p.PELPRO = PelPro And p.CODE_KIN = NTim1.CODE_KIN).SingleOrDefault
                    '                'TMK.KIN_NO += 1
                    '                'NTim1.TIMNO_ = TMK.KIN_NO
                    '                If NSeries = 9524 Then

                    '                End If
                    '            End If
                    '            If insertion.GetType.ToString.Contains("MTRLINE") Then
                    '                Dim NMTRLINE As Panel.MTRLINE = insertion
                    '                ccCRouting += 1
                    '                NMTRLINE.ccCRouting = ccCRouting 'NFINDOC.ccCRouting.Count 
                    '                NMTRLINE.LINENUM = NMTRLINE.ccCRouting
                    '            End If
                    '        Next
                    '    End If
                    'End If
                    'If Not db.GetChangeSet.Updates.Count = 0 Then
                    '    'Dim TFINDOC As FINDOC = Me.MasterBindingSource.Current
                    '    'Dim ccCRouting As Integer = 0
                    '    'If TFINDOC.FINDOC > 0 Then
                    '    '    ccCRouting = db.ccCRouting.Where(Function(f) f.FINDOC = TFINDOC.FINDOC).Max(Function(f) f.ccCRouting)
                    '    'End If
                    '    For Each Changes As Object In db.GetChangeSet.Updates
                    '        If Changes.GetType.ToString.Contains("FINDOC") Then
                    '            NFINDOC = Changes
                    '        End If
                    '        If Changes.GetType.ToString.Contains("MTRLINE") Then
                    '            Dim NMTRLINE As Panel.MTRLINE = Changes
                    '            'ccCRouting += 1
                    '            'NMTRLINE.ccCRouting = ccCRouting
                    '            'NMTRLINE.LINENUM = NMTRLINE.ccCRouting
                    '        End If
                    '    Next
                    'End If
                    ''--------WHERE for Debug------
                    ''sSQL = db.GetCommand(q).CommandText
                    ''For Each n As DbParameter In db.GetCommand(q).Parameters
                    ''    Dim v As Object = Nothing
                    ''    If IsNumeric(n.Value) Then
                    ''        v = n.Value
                    ''    Else
                    ''        v = "'" & n.Value & "'"
                    ''    End If
                    ''    If n.DbType = DbType.DateTime Then
                    ''        Dim d As DateTime = n.Value
                    ''        'v = "CONVERT(DATETIME, '" & d.ToString("yyyy-MM-dd") & "', 102)"
                    ''        v = "'" & d.ToString("yyyyMMdd") & "'"
                    ''    End If
                    ''    'CONVERT(DATETIME, '2014-02-16 10:36:51', 102)) 
                    ''    sSQL = sSQL.Replace(n.ParameterName, v)
                    ''Next
                    LogSQL = sSQL
                    db.Log = Console.Out
                    db.SubmitChanges()

                    ' The Complete method commits the transaction. If an exception has been thrown, 
                    ' Complete is called and the transaction is rolled back.
                    scope.Complete()

                    SaveData = True
                    DgdvRefresh = True
                End Using
            Catch ex As TransactionAbortedException
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message)
            Catch ex As ApplicationException
                tx = Console.Out
                writer.WriteLine("ApplicationException Message: {0}", ex.Message)
            Catch ex As Exception
                writer.WriteLine("Exception Message: {0}", ex.Message)
            Finally
                ' Close the connection
                If db.Connection.State = ConnectionState.Open Then
                    db.Connection.Close()
                End If
            End Try
            ' Display messages.
            If Not writer.ToString() = String.Empty Then
                MsgBox(writer.ToString(), MsgBoxStyle.Exclamation, "Προσοχή !!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If SaveData = False Then
            MsgBox("Προσοχή !!!.Ακύρωση Αλλαγών", MsgBoxStyle.Exclamation, "Προσοχή !!!")
        End If
    End Function
    Private Function Conditions() As Boolean
        Conditions = True
        'Dim drv = Me.ccCRoutingBindingSource.List
        Dim drv As Hglp.FINDOC = Me.MasterBindingSource.Current
        Dim smsg As String = String.Empty
        'For Each m As ccCRouting In drv.ccCRouting
        '    If IsNothing(m.MTRL) OrElse m.MTRL = 0 Then
        '        smsg &= "Προσοχή !!!. Λάθος κωδικός" & vbCrLf
        '        'MsgBox("Προσοχή !!!. Λάθος κωδικός ΜΗΤΡΑΣ ", MsgBoxStyle.Critical)
        '        Dim errorMsg As String = "Απαραίτητο Πεδίο"
        '        'Me.ErrorProvider1.SetError(Me.CODEComboBox, errorMsg)
        '        'Return False
        '    End If
        '    'If m.QTY_L > 0 Then 'ΕΝΤΟΛΗ-ΛΕΥΚΟ
        '    '    Dim mtrl As Integer = m.MTRL
        '    '    Dim NMTRLL As MTRL = db.MTRLs.Where(Function(f) f.MTRL = mtrl).FirstOrDefault
        '    '    Dim LEYKO As String = NMTRLL.CCCLEYKO
        '    '    NMTRLL = db.MTRLs.Where(Function(f) f.CODE = LEYKO).FirstOrDefault
        '    '    If IsNothing(NMTRLL) Then
        '    '        smsg &= "Προσοχή!!! Λάθος Κωδικός Λευκού γιά " & NMTRLL.CODE & vbCrLf
        '    '        'MsgBox("Προσοχή!!! Λάθος Κωδικός Λευκού γιά " & m.MTRL1_CODE, MsgBoxStyle.Critical)
        '    '        Exit Function
        '    '    End If
        '    '    'Me.ErrorProvider1.SetError(Me.CODEComboBox, errorMsg)
        '    '    'Return False
        '    'End If
        '    If m.QTY2 > 0 Then
        '        Select Case drv.SERIES
        '            Case 9593
        '                'fmt = "ΠΑΦ0000000"
        '            Case 9594 'Λευκό
        '                'Update ΆΒΑΦΟ
        '                Dim FINDOCS As Integer = m.FINDOCS
        '                Dim MTRL As Integer = m.MTRL
        '                Dim CCCLEYKO As String = m.MTRL1.CODE
        '                'MTRL = db.MTRLs.Where(Function(f) f.CCCLEYKO = CCCLEYKO).Select(Function(f) f.MTRL).FirstOrDefault
        '                ''Find Abafo
        '                'Dim n As MTRLINE = db.ccCRouting.Where(Function(f) f.FINDOC = FINDOCS And f.MTRL = MTRL).FirstOrDefault
        '                'If Not IsNothing(n) Then
        '                '    n.QTY1COV = m.QTY2
        '                'End If
        '        End Select
        '    End If
        'Next
        If Not smsg = String.Empty Then
            MsgBox(smsg, MsgBoxStyle.Critical)
            Return False
        End If


        'If IsNothing(drv.SurName) OrElse drv.SurName.Trim = String.Empty Then
        '    MsgBox("Προσοχή !!!. Λάθος κωδικός Συναλλασσόμενου ", MsgBoxStyle.Critical)
        '    Dim errorMsg As String = "Απαραίτητο Πεδίο"
        '    Me.ErrorProvider1.SetError(Me.SurNameTextBox, errorMsg)
        '    Me.ErrorProvider1.SetError(Me.AgeTextBox, errorMsg)
        '    Return False
        'End If
        'If CheckAFM(TAX_NOTextBox.Text) = False Then
        '    If MsgBox("Προσοχή !!!. Μη έγκυρο Α.Φ.Μ", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '        Exit Sub
        '    End If
        'End If
        'Throw New NotImplementedException
    End Function
#End Region
    '#Region "96-MasterDataGridView"
    '    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '        'Cmd_Edit()
    '    End Sub
    '    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
    '        'Dim drv As DataRowView = Me.MasterBindingSource.Current

    '        'Dim status = Me.MasterDataGridView.Columns(e.ColumnIndex)
    '        'Me.StatusStrip1.Text = status
    '    End Sub
    '    Private Sub MasterDataGridView_Styling()
    '        Try

    '            'Me.MasterDataGridView.AutoGenerateColumns = True
    '            'Me.MasterDataGridView.AutoResizeColumns()

    '            'myArrF = ("MembersID,Name,SurName,Job,Age,Phone1,EMail,Area,IdentityCard,Photo").Split(",")
    '            'myArrN = ("MembersID,Επώνυμο,Όνομα,Job,Age,Τηλέφωνο,EMail,Area,IdentityCard,Photo").Split(",")
    '            'myArrF = ("SurName,Name,Job,Age,Phone1,EMail,Area,IdentityCard,Photo").Split(",")
    '            'myArrN = ("Επώνυμο,Όνομα,Job,Age,Τηλέφωνο,EMail,Area,IdentityCard,Photo").Split(",")


    '            'Dim dt As DataTable = Utility.LINQToDataTable(db, Me.MasterBindingSource.DataSource)
    '            'RemoveGridColumns(MasterDataGridView, Nothing, myArrF, myArrN, dt.DefaultView, False)
    '            'ΣΧΟΛΙΑ,ΚΩΔΙΚΟΣ ΜΗΤΡΑΣ &PROFIL,ΜΗΚΟΣ PROFIL,ΤΕΜ. PROFIL,ΩΡΑ ΕΝΑΡΞΗ ,ΩΡΑ ΛΗΞΗ ,ΤΕΜ. ΜΠΙΓ.,ΜΗΚΟΣ ΜΠΙΓ.,ΜΙΚΤΑ ΚΙΛΑ,ΤΕΜ.  PROFIL,ΜΗΚΟΣ PROFIL,ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m,Καθ.κιλά PROFIL,Σκραπ kg,%,ΠΑΡΑΤΗΡΗΣΕΙΣ,
    '            Dim f As Panel.FINDOC = Me.MasterBindingSource.Current
    '            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
    '                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
    '            Next
    '            Select Case f.SERIES
    '                Case 9520, 9521, 9522, 9523 'ΕΞΑΓΩΓΕΣ-ΕΙΣΑΓΩΓΕΣ-ΠΡΟΣ ΧΡΩΜΑΤΩΣΗ-ΑΠΟ ΧΡΩΜΑΤΩΣΗ
    '                    myArrF = ("FINCODE,FINDOC1_TRNDATE,MTRL1_CODE,QTY1,COMMENTS,COMMENTS1,MTRL,SERIES1_NAME,FINDOC1_SERIESNUM,ccCRouting").Split(",")
    '                    myArrN = ("ΠΑΡΑΣΤΑΤΙΚΟ,ΗΜ/ΝΙΑ,ΚΩΔΙΚΟΣ ΠΟΥΔΡΑΣ,ΚΙΛΑ,ΣΧΟΛΙΑ,ΠΑΡΑΤΗΡΗΣΕΙΣ,MTRL,ΣΕΙΡΑ,SERIESNUM,ccCRouting").Split(",")
    '                Case 9590
    '                    myArrF = ("FINCODE,FINDOC1_TRNDATE,MTRL1_CODE,QTY1,COMMENTS,COMMENTS1,MTRL,SERIES1_NAME,FINDOC1_SERIESNUM,ccCRouting").Split(",")
    '                    myArrN = ("ΠΑΡΑΣΤΑΤΙΚΟ,ΗΜ/ΝΙΑ,ΚΩΔΙΚΟΣ ΠΟΥΔΡΑΣ,ΚΙΛΑ,ΣΧΟΛΙΑ,ΠΑΡΑΤΗΡΗΣΕΙΣ,MTRL,ΣΕΙΡΑ,SERIESNUM,ccCRouting").Split(",")
    '                Case 9593
    '                    myArrF = ("FINCODE,FINDOC1_TRNDATE,MTRL1_CODE,QTY,QTY2,QTY1COV,COMMENTS,COMMENTS1,SERIES1_NAME,FINDOC1_SERIESNUM,ccCRouting").Split(",")
    '                    myArrN = ("ΠΑΡΑΣΤΑΤΙΚΟ,ΗΜ/ΝΙΑ,ΚΩΔΙΚΟΣ ΠΟΥΔΡΑΣ,ΕΝΤΟΛΗ-ΑΒΑΦΟ,ΑΒΑΦΟ,ΕΚΤΕΛΕΣΜΕΝΑ,ΣΧΟΛΙΑ,ΠΑΡΑΤΗΡΗΣΕΙΣ,ΣΕΙΡΑ,SERIESNUM,ccCRouting").Split(",")
    '                Case 9594
    '                    myArrF = ("FINCODE,FINDOC1_TRNDATE,MTRL1_CODE,QTY,QTY2,QTY1COV,COMMENTS,COMMENTS1,SERIES1_NAME,FINDOC1_SERIESNUM").Split(",")
    '                    myArrN = ("ΠΑΡΑΣΤΑΤΙΚΟ,ΗΜ/ΝΙΑ,ΚΩΔΙΚΟΣ ΠΟΥΔΡΑΣ,ΕΝΤΟΛΗ-ΛΕΥΚΟ,ΛΕΥΚΟ,ΣΥΣΚΕΥ,ΣΧΟΛΙΑ,ΠΑΡΑΤΗΡΗΣΕΙΣ,ΣΕΙΡΑ,SERIESNUM").Split(",")
    '                Case Else
    '                    myArrF = ("FINCODE,FINDOC1_TRNDATE,MTRL1_CODE,PRLENGTH,QTY,START,END,QTYBIGET,LENGTHBIGET,MIKTA_KILA,QTY2,PRLENGTH,BAROS_PROFIL_GR_METRO,KATHARA_KILA_PROFIL,SCRAP_KILA,SCRAP_100,COMMENTS,COMMENTS1,SERIES1_NAME,FINDOC1_SERIESNUM,ccCRouting").Split(",")
    '                    myArrN = ("ΠΑΡΑΣΤΑΤΙΚΟ,ΗΜ/ΝΙΑ,ΚΩΔΙΚΟΣ ΜΗΤΡΑΣ & PROFIL,ΜΗΚΟΣ PROFIL,ΤΕΜ.PROFIL_ΠΡ,ΩΡΑ ΕΝΑΡΞΗ,ΩΡΑ ΛΗΞΗ,ΤΕΜ.ΜΠΙΓ.,ΜΗΚΟΣ ΜΠΙΓ.,ΜΙΚΤΑ ΚΙΛΑ,ΤΕΜ.PROFIL,ΜΗΚΟΣ PROFIL,ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m,Καθ.κιλά PROFIL,Σκραπ kg,%,ΣΧΟΛΙΑ,ΠΑΡΑΤΗΡΗΣΕΙΣ,ΣΕΙΡΑ,SERIESNUM,ccCRouting").Split(",")
    '            End Select

    '            'Add Bound Columns
    '            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
    '            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
    '            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
    '                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
    '            Next

    '            'Add Unbound Columns
    '            Dim DataGridViewComboBoxColumnMTRL_CODE As New DataGridViewComboBoxColumn
    '            'DataGridViewComboBoxColumnMTRL_CODE.DataPropertyName = "MTRL"
    '            'Me.DataGridViewComboBoxColumn1.DataSource = Me.MTRLBindingSource
    '            DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CODE"
    '            DataGridViewComboBoxColumnMTRL_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
    '            DataGridViewComboBoxColumnMTRL_CODE.Name = "DataGridViewComboBoxColumnMTRL_CODE"
    '            DataGridViewComboBoxColumnMTRL_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    '            DataGridViewComboBoxColumnMTRL_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    '            DataGridViewComboBoxColumnMTRL_CODE.ValueMember = "MTRL"
    '            DataGridViewComboBoxColumnMTRL_CODE.Width = 150

    '            '
    '            'Dim DdlMTRL1_CODE As New DataGridViewComboBoxColumn
    '            ''DdlMTRL1_CODE.DataPropertyName = "MTRL"
    '            ''Me.DdlMTRL1_CODE.DataSource = Me.MTRLBindingSource
    '            'DdlMTRL1_CODE.DisplayMember = "CODE"
    '            'DdlMTRL1_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
    '            'DdlMTRL1_CODE.Name = "DdlMTRL1_CODE"
    '            'DdlMTRL1_CODE.ValueMember = "MTRL"
    '            'DdlMTRL1_CODE.Width = 150
    '            'MasterDataGridView.Columns.Insert(0, DdlMTRL1_CODE)

    '            Dim DataGridViewTextBox_OLDMTRL As New DataGridViewTextBoxColumn
    '            DataGridViewTextBox_OLDMTRL.HeaderText = "OLDMTRL"
    '            DataGridViewTextBox_OLDMTRL.Name = "DataGridViewTextBox_OLDMTRL"


    '            'Search_Code
    '            '
    '            Dim Search_Code As New DataGridViewTextBoxColumn
    '            Search_Code.Name = "Search_Code"
    '            'Me.MasterDataGridView.Columns.Insert(0, Search_Code)

    '            'Dim DataGridViewComboBoxColumnSTATUS As New DataGridViewComboBoxColumn
    '            ''******************************
    '            'DataGridViewComboBoxColumnSTATUS.DataPropertyName = "CCCSTATUSID"
    '            'DataGridViewComboBoxColumnSTATUS.DataSource = Me.CCCSTATUSBindingSource
    '            'DataGridViewComboBoxColumnSTATUS.DisplayMember = "DESCR"
    '            'DataGridViewComboBoxColumnSTATUS.HeaderText = "STATUS"
    '            'DataGridViewComboBoxColumnSTATUS.Name = "DataGridViewComboBoxColumnSTATUS"
    '            'DataGridViewComboBoxColumnSTATUS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
    '            'DataGridViewComboBoxColumnSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
    '            'DataGridViewComboBoxColumnSTATUS.ValueMember = "CCCSTATUS"
    '            'MasterDataGridView.Columns.Insert(2, DataGridViewComboBoxColumnSTATUS)
    '            ''***************************************

    '            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
    '                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
    '            Next

    '            If Not IsNothing(MasterDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
    '                MasterDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΒΕΡΓΕΣ")) Then
    '                MasterDataGridView.Columns("ΒΕΡΓΕΣ").DefaultCellStyle.Format = "N0"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ")) Then
    '                MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ").DefaultCellStyle.Format = "N0"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ")) Then
    '                MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ").DefaultCellStyle.Format = "N0"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.")) Then
    '                MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.").DefaultCellStyle.Format = "N0"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ")) Then
    '                MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ").DefaultCellStyle.Format = "d"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
    '                MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ")) Then
    '                MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ").DefaultCellStyle.Format = "t"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m")) Then
    '                MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m").DefaultCellStyle.Format = "N6"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.PROFIL")) Then
    '                MasterDataGridView.Columns("ΤΕΜ.PROFIL").DefaultCellStyle.Format = "N0"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΚΙΛΑ")) Then
    '                MasterDataGridView.Columns("ΚΙΛΑ").DefaultCellStyle.Format = "N3"
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("MTRL")) Then
    '                MasterDataGridView.Columns("MTRL").ReadOnly = True
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ΣΕΙΡΑ")) Then
    '                MasterDataGridView.Columns("ΣΕΙΡΑ").ReadOnly = True
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("SERIESNUM")) Then
    '                MasterDataGridView.Columns("SERIESNUM").ReadOnly = True
    '            End If
    '            If Not IsNothing(MasterDataGridView.Columns("ccCRouting")) Then
    '                MasterDataGridView.Columns("ccCRouting").ReadOnly = True
    '            End If
    '            If f.SERIES = 9593 Then 'ΑΒΑΦΟ
    '                If Not IsNothing(MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ")) Then
    '                    MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ").ReadOnly = True
    '                End If
    '            End If



    '            'Add Columns to MasterDataGridView
    '            Me.MasterDataGridView.Columns.Insert(0, DataGridViewComboBoxColumnMTRL_CODE)
    '            Me.MasterDataGridView.Columns.Insert(0, Search_Code)
    '            Me.MasterDataGridView.Columns.Add(DataGridViewTextBox_OLDMTRL)
    '            AddOutOfOfficeColumn(Me.MasterDataGridView)

    '            'Fill Unbound Collumns
    '            For Each row As DataGridViewRow In MasterDataGridView.Rows
    '                Dim dll As DataGridViewComboBoxCell = row.Cells("DataGridViewComboBoxColumnMTRL_CODE")
    '                Dim MTRL As Integer = row.Cells("MTRL").Value

    '                Dim m As Panel.MTRL = db.MTRLs.Where(Function(f1) f1.MTRL = MTRL).FirstOrDefault
    '                If Not IsNothing(m) Then
    '                    dll.Items.Add(m)
    '                    dll.Value = MTRL
    '                    row.Cells("DataGridViewTextBox_OLDMTRL").Value = MTRL
    '                End If
    '            Next

    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click
    '        Dim s As ToolStripButton = sender
    '        Dim check As Boolean = False
    '        If s.Name = "TlSBtnCheck" Then
    '            check = True
    '        Else
    '            check = False
    '        End If
    '        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
    '            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
    '            For Each ds As DataGridViewRow In DrSel
    '                If Not ds.Cells("Check").Value = check Then
    '                    ds.Cells("Check").Value = check
    '                End If
    '            Next
    '            'For i As Integer = 0 To DrSel.Count - 1
    '            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
    '            'Next
    '        Else
    '            For Each ds As DataGridViewRow In Me.MasterDataGridView.Rows
    '                ds.Cells("Check").Value = check
    '            Next
    '            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
    '            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
    '            'Next
    '        End If
    '    End Sub
    '    Private Sub MasterDataGridView_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MasterDataGridView.CellFormatting
    '        'Dim s As DataGridView = sender
    '        'If s.Columns(e.ColumnIndex).Name.Equals("MTRL1_CODE") Then
    '        '    ' Use helper method to get the string from lookup table
    '        '    Dim MTRL As Integer = s.Rows(e.RowIndex).Cells("MTRL").Value
    '        '    Dim m As MTRL = db.MTRLs.Where(Function(f) f.MTRL = MTRL).FirstOrDefault
    '        '    If Not IsNothing(m) Then
    '        '        e.Value = m.CODE 'GetWorkplaceNameLookupValue(dataGridViewScanDetails.Rows(e.RowIndex).Cells("UserWorkplaceID").Value)
    '        '    End If
    '        'End If
    '    End Sub
    '    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
    '        Dim s As GmDataGridView = sender
    '        'If s.Rows.Count < 2 Then
    '        '    Exit Sub
    '        'End If
    '        If s.Columns(e.ColumnIndex).Name = "Search_Code" Then
    '            Dim cell As DataGridViewCell = s.CurrentCell
    '            Dim MTRL1_CODE As String = cell.EditedFormattedValue
    '            If MTRL1_CODE = String.Empty Then
    '                'MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
    '                'cell.Value = Nothing
    '                'e.Cancel = True
    '                Exit Sub
    '            End If
    '            If cell.FormattedValue.ToString = MTRL1_CODE Then
    '                If MTRL1_CODE.IndexOf("*") = -1 Then
    '                    Exit Sub
    '                End If
    '            End If

    '            If MTRL1_CODE.IndexOf("*") = -1 Then
    '                MTRL1_CODE &= "*"
    '            End If

    '            'Search MTRL
    '            Dim ml As List(Of Panel.MTRL) = (From m In db.MTRLs Where m.MTRGROUP = 11009 And m.CODE Like MTRL1_CODE
    '                                             Order By m.CODE).ToList

    '            If ml.Count = 0 Then
    '                MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
    '                cell.Value = Nothing
    '                e.Cancel = True
    '                Exit Sub
    '            End If
    '            If ml.Count = 1 Then
    '                '    cell.Value = ml(0).CODE
    '                '    s.Rows(e.RowIndex).Cells("MTRL").Value = ml(0).MTRL
    '                Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("DataGridViewComboBoxColumnMTRL_CODE") 'row.Cells("DdlMTRL1_CODE")
    '                dll.Items.Clear()
    '                dll.Items.Add(ml.FirstOrDefault)
    '                dll.Value = ml.FirstOrDefault.MTRL
    '                s.Rows(e.RowIndex).Cells("MTRL").Value = ml.FirstOrDefault.MTRL
    '            Else
    '                Dim emptyMTRL As Panel.MTRL() = Nothing
    '                emptyMTRL = {New Panel.MTRL With {.CODE = "<Select a product>", .MTRL = 0}}
    '                Dim mln As List(Of Panel.MTRL) = (From Empty In emptyMTRL).Union(
    '                                            (From m1 In ml Order By m1.CODE)).ToList

    '                Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("DataGridViewComboBoxColumnMTRL_CODE") 'row.Cells("DdlMTRL1_CODE")
    '                dll.Items.Clear()
    '                If Not IsNothing(mln) Then
    '                    dll.Items.AddRange(mln.ToArray) '.FirstOrDefault)
    '                End If
    '                dll.Value = 0
    '            End If


    '            'End Select
    '        End If
    '        If s.Columns(e.ColumnIndex).Name = "DataGridViewComboBoxColumnMTRL_CODE" Then
    '            Dim cell As DataGridViewComboBoxCell = s.CurrentCell
    '            'Dim cc As DataGridViewComboBoxCell = s.CurrentCell
    '            Dim MTRL1_CODE As String = cell.EditedFormattedValue
    '            If MTRL1_CODE = String.Empty Then
    '                'MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
    '                'cell.Value = Nothing
    '                'e.Cancel = True
    '                Exit Sub
    '            End If
    '            If Not cell.FormattedValue.ToString = MTRL1_CODE Then
    '                Dim m As Panel.MTRL = db.MTRLs.Where(Function(f) f.CODE = MTRL1_CODE).FirstOrDefault
    '                If Not IsNothing(m) Then
    '                    s.Rows(e.RowIndex).Cells("MTRL").Value = m.MTRL
    '                    cell.Value = m.MTRL
    '                End If
    '            End If
    '        End If
    '    End Sub
    '    Private Sub MasterDataGridView_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MasterDataGridView.CellValueChanged
    '        Exit Sub
    '        'Dim s As DataGridView = sender
    '        'If s.CurrentCell.IsInEditMode = True Then 's.Rows(e.RowIndex).Cells(e.ColumnIndex).IsInEditMode = True Then
    '        '    Dim drvG As DataGridViewRow = s.CurrentRow
    '        '    If IsDBNull(s.CurrentCell.Value) Then
    '        '        drvG.Cells("ΠΕΡΙΓΡΑΦΗ").Value = ""
    '        '        Exit Sub
    '        '    End If
    '        '    Dim df As New Object
    '        '    Select Case s.Columns(e.ColumnIndex).DataPropertyName
    '        '        Case "AP1_APOCODE"
    '        '            RsWhere = "APO1.Company = " & Company
    '        '            Dim drv_Tim1 As DataRowView
    '        '            drv_Tim1 = Me.MasterBindingSource.Current
    '        '            If Not s.CurrentCell.Value.IndexOf("*") = -1 Then
    '        '                RsWhere = Trim(RsWhere) & " and APOKEY LIKE '" & s.CurrentCell.Value.ToString.Replace("*", "%") & "'"
    '        '            Else
    '        '                RsWhere = Trim(RsWhere) & " and APOKEY = '" & s.CurrentCell.Value & "'"
    '        '            End If
    '        '            RsWhere = Trim(RsWhere) & " AND GENDSCR.TYPE = 2 and substring(APOKEY,1,1) = '" & "_" & "'"
    '        '            If drv_Tim1("CODE_KIN") = 14 Then
    '        '                RsWhere = "APO1.Company = 0 and APOKEY = '_99999'" & " AND GENDSCR.TYPE = 2"
    '        '            End If
    '        '            RsOrder = "APOKEY"
    '        '            sSQL = "SELECT APOKEY,ADESCR,MON_METR,L_COST,L_PRICE,KAT_EID,FPA,GENDSCR.DESCR AS GENDSCR_DESCR,APO1ID,LOGCODEPOL,LOGCODEAGO FROM (APO1 INNER JOIN GENDSCR ON APO1.KAT_EID = GENDSCR.CODE)  WHERE " & RsWhere
    '        '            Dim dvReturn As DataView = df.GmFillTable(sSQL, "Return").DefaultView
    '        '            Dim drvReturn As DataRowView = Nothing
    '        '            If dvReturn.Count = 0 Then
    '        '                MsgBox("Δεν βρέθηκε η Εγγραφή.")
    '        '            ElseIf dvReturn.Count = 1 Then
    '        '                drvReturn = dvReturn(0)
    '        '            Else
    '        '                Dim TSearchFR As New SearchFRNew
    '        '                TSearchFR.VMasterTableName = "APO1ID"
    '        '                TSearchFR.VArrF = "APOKEY,ADESCR,APO1ID,LOGCODEAGO"
    '        '                TSearchFR.VArrN = "ΚΩΔΙΚΟΣ,ΠΕΡΙΓΡΑΦΗ,APO1ID,LOGCODEAGO"
    '        '                'TSearchFR.VCheck = True
    '        '                TSearchFR.VSize = New System.Drawing.Size(685, 430) 'New System.Drawing.Size(GmDgLookUp1.Size.Width, 130) '292
    '        '                TSearchFR.VLocation = New System.Drawing.Point(sender.Left + 5, sender.Top) '+ sender.Height)
    '        '                TSearchFR.VisibleFrm = False
    '        '                TSearchFR.VMeLabel = "Ευρετήριο Τραπεζών"
    '        '                TSearchFR.ShowDialog()
    '        '                If Not IsNothing(TSearchFR.dvReturn) Then
    '        '                    dvReturn = TSearchFR.dvReturn
    '        '                    If dvReturn.Count = 1 Then
    '        '                        drvReturn = dvReturn(0)
    '        '                    End If
    '        '                End If
    '        '            End If
    '        '            Dim drvTIM4 As DataRowView = Nothing ' Me.DetailBindingSource.Current
    '        '            If Not IsNothing(drvReturn) Then
    '        '                drvTIM4("APO1ID") = drvReturn("APO1ID")
    '        '                drvTIM4("AP1_APOCODE") = drvReturn("APOKEY")
    '        '                drvTIM4("AP1_ADESCR") = drvReturn("ADESCR")
    '        '                drvG.Cells("ΠΕΡΙΓΡΑΦΗ").Value = drvReturn("ADESCR")
    '        '                Dim CTAX_CODE As Short
    '        '                Dim drvTIM1 As DataRowView = Me.MasterBindingSource.Current
    '        '                If IsDBNull(drvTIM1("TAX_CODE")) Then
    '        '                    CTAX_CODE = 1
    '        '                Else
    '        '                    CTAX_CODE = drvTIM1("TAX_CODE")
    '        '                End If
    '        '                Select Case CTAX_CODE
    '        '                    Case 0 'Άνευ ΦΠΑ Δώρο
    '        '                        drvTIM4("FPA") = 0
    '        '                    Case 1 'Κανονικό ΦΠΑ
    '        '                        drvTIM4("FPA") = drvReturn("FPA")
    '        '                    Case 2 'Μειωμένο ΦΠΑ
    '        '                        sSQL = "SELECT VATS1 FROM VAT WHERE PERCNT = " & drvReturn("FPA")
    '        '                        Dim reducedFPA As Double = df.GmExecuteScalar(sSQL)
    '        '                        reducedFPA = df.GmExecuteScalar("SELECT PERCNT FROM VAT WHERE VAT = " & reducedFPA)
    '        '                        drvTIM4("FPA") = reducedFPA '13 'Μειωμένο ΦΠΑ
    '        '                End Select
    '        '                drvTIM4("FPA") = 0
    '        '                drvTIM4.EndEdit()
    '        '                'CalcFields(m_DataSet.Tables("TIM4").DefaultView, False, True)
    '        '            Else
    '        '                drvTIM4("APO1ID") = DBNull.Value
    '        '                drvTIM4("AP1_APOCODE") = DBNull.Value
    '        '                drvG.Cells("ΠΕΡΙΓΡΑΦΗ").Value = ""
    '        '            End If
    '        '        Case "L_PRICE"
    '        '            'CalcFields(m_DataSet.Tables("TIM4").DefaultView, False, True)
    '        '    End Select
    '        'End If
    '    End Sub
    '    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError

    '        MessageBox.Show("Error happened " _
    '            & e.Context.ToString())

    '        If (e.Context = DataGridViewDataErrorContexts.Commit) _
    '            Then
    '            MessageBox.Show("Commit error")
    '        End If
    '        If (e.Context = DataGridViewDataErrorContexts _
    '            .CurrentCellChange) Then
    '            MessageBox.Show("Cell change")
    '        End If
    '        If (e.Context = DataGridViewDataErrorContexts.Parsing) _
    '            Then
    '            MessageBox.Show("parsing error")
    '        End If
    '        If (e.Context =
    '            DataGridViewDataErrorContexts.LeaveControl) Then
    '            MessageBox.Show("leave control error")
    '        End If

    '        If (TypeOf (e.Exception) Is ConstraintException) Then
    '            Dim view As DataGridView = CType(sender, DataGridView)
    '            view.Rows(e.RowIndex).ErrorText = "an error"
    '            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
    '                .ErrorText = "an error"

    '            e.ThrowException = False
    '        End If
    '    End Sub
    '    Private Sub MasterDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MasterDataGridView.EditingControlShowing
    '        Dim s As GmDataGridView = sender
    '        Dim cell As DataGridViewCell = s.CurrentCell
    '        'Dim r = cell.OwningRow.Cells("")..Cells("MTRL")
    '        If cell.ColumnIndex = 2 Then
    '            'Dim c As ComboBox = CType(e.Control, ComboBox)
    '        End If

    '    End Sub
    '#End Region
#Region "02- Control Events"
    Private Sub BindingNavigatorAddNewItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorAddNewItem.Click
        Cmd_Add()
    End Sub
    'Private Sub CODEComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
    '    Try
    '        Dim s As ComboBox = sender
    '        If s.Focused Then
    '            Dim ddlMTRL = s.SelectedItem
    '            If Not IsNothing(ddlMTRL) Then
    '                Dim CMTRLINE As Panel.MTRLINE = Me.MasterBindingSource.Current
    '                'CMTRLINE.MTRL = ddlMTRL.MTRL
    '                'this.DataContext.Customers.First(i => i.Key == 1)
    '                Dim mtrlnew As Integer = ddlMTRL.MTRL
    '                CMTRLINE.MTRL1 = db.MTRLs.Where(Function(f) f.MTRL = mtrlnew).SingleOrDefault
    '                CMTRLINE.MTRL = ddlMTRL.MTRL
    '                'Me.MTRLTextBox.Text = ddlMTRL.MTRL
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub SurNameTextBox_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) ' Handles SurNameTextBox.Validating
        Dim s As TextBox = sender
        If s.Text.Trim = String.Empty Then
            ' Cancel the event and select the text to be corrected by the user.
            'e.Cancel = True
            ' Set the ErrorProvider error with the text to display. 
            Dim errorMsg As String = "Απαραίτητο Πεδίο"
            Me.ErrorProvider1.SetError(s, errorMsg)
        End If
    End Sub
    Private Sub SurNameTextBox_Validated(sender As System.Object, e As System.EventArgs) ' Handles SurNameTextBox.Validated
        Dim s As TextBox = sender
        ' If all conditions have been met, clear the error provider of errors.
        If Not s.Text.Trim = String.Empty Then
            ErrorProvider1.SetError(s, "")
        End If
    End Sub
    Private Function ValidEmailAddress(ByVal emailAddress As String, ByRef errorMessage As String, s As TextBox) As Boolean
        ' Confirm there is text in the control. 
        If s.Text.Length = 0 Then
            errorMessage = "E-mail address is required."
            Return False

        End If

        ' Confirm that there is an "@" and a "." in the e-mail address, and in the correct order. 
        If emailAddress.IndexOf("@") > -1 Then
            If (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@")) Then
                errorMessage = ""
                Return True
            End If
        End If

        errorMessage = "E-mail address must be valid e-mail address format." + ControlChars.Cr +
          "For example 'someone@example.com' "
        Return False
    End Function
    Private Sub EMailTextBox_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) ' Handles EMailTextBox.Validating
        Dim s As TextBox = sender
        Dim errorMsg As String = ""
        If Not ValidEmailAddress(s.Text, errorMsg, s) Then
            ' Cancel the event and select the text to be corrected by the user.
            e.Cancel = True
            s.Select(0, s.Text.Length)

            ' Set the ErrorProvider error with the text to display.  
            Me.ErrorProvider1.SetError(s, errorMsg)
        End If
    End Sub
    Private Sub EMailTextBox_Validated(sender As System.Object, e As System.EventArgs) ' Handles EMailTextBox.Validated
        Dim s As TextBox = sender
        ' If all conditions have been met, clear the error provider of errors.
        ErrorProvider1.SetError(s, "")
    End Sub
    'Private Sub ComboBoxMTRL1_CODE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxMTRL1_CODE.SelectedIndexChanged
    '    Me.FormMTRL1_CODE.Close()
    'End Sub
    'Private Sub FormMTRL1_CODE_Leave(sender As Object, e As System.EventArgs) Handles FormMTRL1_CODE.Leave
    '    FormMTRL1_CODE.Close()
    'End Sub

    Private Sub AboutToolStripButton_Click(sender As Object, e As EventArgs)
        Dim about As New AboutBox1
        about.ShowDialog()
    End Sub

    Private Sub txtBoxTrucksno_GotFocus(sender As Object, e As EventArgs) Handles txtBoxTrucksno.GotFocus, txtBoxDriverName.GotFocus, txtBoxRemarks.GotFocus
        Dim s As TextBox = sender
        OldText = s.Text
    End Sub
    Private Sub txtBoxtxtBoxTrucksno_TextChanged(sender As Object, e As EventArgs) Handles txtBoxTrucksno.TextChanged, txtBoxDriverName.TextChanged, txtBoxRemarks.TextChanged
        Dim s As TextBox = sender

        Dim newText = s.Text

        'Compare OldText And newText here
        If Not OldText = newText Then
            Dim findoc As Hglp.FINDOC = Me.MasterBindingSource.Current
            If Not IsNothing(findoc) Then
                If s.Name = "txtBoxTrucksno" Then
                    If Me.txtBoxTrucksno.Text.Length > 20 Then
                        MsgBox("Προσοχή:!!!" & vbCrLf & "Χαρακτήρες για Αρ.Οχήματος περισσότεροι των 20", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                    findoc.MTRDOC.TRUCKSNO = Me.txtBoxTrucksno.Text
                End If
                If s.Name = "txtBoxDriverName" Then
                    findoc.VARCHAR02 = Me.txtBoxDriverName.Text
                End If
                If s.Name = "txtBoxRemarks" Then
                    findoc.REMARKS = Me.txtBoxRemarks.Text
                End If

                findoc.UPDDATE = Now()
                Dim cuser '= xconinf.UserId
                findoc.UPDUSER = cuser

                Me.BindingNavigatorSaveItem.Enabled = True

            End If
        End If
    End Sub
    Private Sub ExportDataToExcel(fileName As String, title As String)

        Using p As New ExcelPackage()
            Try

                'Here setting some document properties
                p.Workbook.Properties.Author = "GmLogic"
                p.Workbook.Properties.Title = fileName '"ExprotData"

                'Create a sheet
                p.Workbook.Worksheets.Add(title)

                For Each ws As ExcelWorksheet In p.Workbook.Worksheets
                    Dim lst = Me.MasterBindingSource.DataSource
                    'Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Εκκρεμείς παραγγελίες")
                    'ws.Name = "Sample Worksheet"
                    'Setting Sheet's name

                    ws.Cells.Style.Font.Size = 11
                    'Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"
                    'Default Font name for whole sheet
                    'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
                    'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
                    'dt = Utility.LINQToDataTable(db, q)
                    'My Function which generates DataTable
                    'Merging cells and create a center heading for out table
                    'ws.Cells(1, 1).Value = "Sample DataTable Export"
                    Dim colIndex As Integer = 1
                    Dim rowIndex As Integer = 1

                    'ws.Cells(rowIndex, colIndex, rowIndex, 15).Merge = True
                    'ws.Cells(rowIndex, colIndex).Value = "ΛΝΚ Α.Ε"
                    ws.Row(rowIndex).Style.Font.Size = 14
                    'Creating Headings
                    'Dim cellprH = ws.Cells(rowIndex - 1, colIndex)
                    Dim cellH = ws.Cells(rowIndex, colIndex)
                    'Setting the background color of header cells to Gray
                    'Dim fillprH = cellprH.Style.Fill
                    Dim fillH = cellH.Style.Fill
                    'fillprH.PatternType = ExcelFillStyle.Solid
                    fillH.PatternType = ExcelFillStyle.Solid
                    'fillprH.BackgroundColor.SetColor(Color.White)

                    fillH.BackgroundColor.SetColor(Color.FromArgb(208, 206, 206)) 'Color.Orange)

                    'Setting Top/left,right/bottom borders.
                    Dim borderH = cellH.Style.Border
                    borderH.Bottom.Style = InlineAssignHelper(borderH.Top.Style, InlineAssignHelper(borderH.Left.Style, InlineAssignHelper(borderH.Right.Style, ExcelBorderStyle.Thin)))

                    'Setting Value in cell
                    cellH.Value = Me.Text

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                    ws.Row(rowIndex).Height = 35.25
                    ws.Cells(rowIndex, colIndex, rowIndex, myArrN.Length).Merge = True
                    cellH.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                    rowIndex += 1
                    colIndex = 1
                    For Each col In myArrN
                        'Creating Headings
                        Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                        Dim cell = ws.Cells(rowIndex, colIndex)
                        'Setting the background color of header cells to Gray
                        Dim fillpr = cellpr.Style.Fill
                        Dim fill = cell.Style.Fill
                        fillpr.PatternType = ExcelFillStyle.Solid
                        fill.PatternType = ExcelFillStyle.Solid
                        fillpr.BackgroundColor.SetColor(Color.White)

                        fill.BackgroundColor.SetColor(Color.FromArgb(208, 206, 206)) 'Color.Orange)

                        'Setting Top/left,right/bottom borders.
                        Dim border = cell.Style.Border
                        border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                        'Setting Value in cell
                        cell.Value = col
                        'cell.AutoFitColumns()
                        colIndex += 1
                    Next
                    'ws.Column(ColNLst.Count).Width = 44.57

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center

                    Dim rowDataIndex = rowIndex + 1


                    For Each ro In lst ' DataGridViewRow In Me.MasterDataGridView.Rows
                        ' Adding Data into rows
                        colIndex = 1
                        rowIndex += 1

                        For Each col_Name In myArrF 'As  DataGridViewColumn In Me.MasterDataGridView.Columns
                            If col_Name = "Check" Then
                                Continue For
                            End If
                            Dim cell = ws.Cells(rowIndex, colIndex)
                            Try
                                cell.Value = ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                                'Setting Value in cell
                                With cell
                                    Dim t As Type = ro.GetType().GetProperty(col_Name).PropertyType 'col.ValueType
                                    If Not IsNothing(t) Then
                                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                                .Value = CType(cell.Value, Double)
                                                .Style.Numberformat.Format = "#,##0.00"
                                            End If
                                            If Not t.FullName.IndexOf("System.DateTime") Then
                                                If col_Name = "INSDATE" Then
                                                    .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                                Else
                                                    .Style.Numberformat.Format = "dd/MM/yyyy"
                                                End If
                                            End If
                                        End If

                                        If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                            .Value = CType(cell.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.00"
                                        End If
                                        If t.Name = "DateTime" Then
                                            '.Value = CType(ro.Cells(col.Name).Value, Double)
                                            If col_Name = "INSDATE" Then
                                                .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                            Else
                                                .Style.Numberformat.Format = "dd/MM/yyyy"
                                            End If

                                        End If
                                    End If
                                    If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                        If Not t.FullName.IndexOf("System.Decimal") Then
                                            .Value = CType(cell.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.00"
                                        End If
                                    End If
                                    If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                        .Value = CType(cell.Value, Double)
                                        .Style.Numberformat.Format = "#,##0.00"
                                    End If
                                End With
                            Catch ex As Exception
                                MsgBox("Error col_Name:" & col_Name & vbCrLf & ex.Message)
                            End Try


                            'Setting borders of cell
                            Dim border = cell.Style.Border
                            border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                            'ws.Cells(rowIndex, colIndex).AutoFitColumns()
                            colIndex += 1
                        Next
                    Next

                    rowIndex += 1
                    Dim cellFoot = ws.Cells(rowIndex, 1)
                    cellFoot.Value = "Σύνολα"
                    cellFoot.Style.Fill.PatternType = ExcelFillStyle.Solid
                    cellFoot.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(208, 206, 206))
                    Dim startCol = 1
                    Dim endCol = myArrF.Count
                    'Dim criteria = ws.Cells(rowIndex, 2)
                    'criteria.Value = "<0"
                    For i As Integer = startCol + 1 To endCol ' ColNLst.Count
                        Dim cellsum = ws.Cells(rowIndex, i)
                        Dim cellsumpr = ws.Cells(rowIndex - 1, i)
                        If Not cellsumpr.Style.Numberformat.Format = "#,##0.00" Then
                            Continue For
                        End If

                        With cellsum
                            'Setting Sum Formula 
                            '.Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                            '=SUMIF(F2:F9;">0";F2:F9)

                            'Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & ws.Cells(rowIndex, 2).Address & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'Spoiler: The solution is to use "," instead of ";" when working with formulas in your code.
                            Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<>0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'If Me.radioBtnΑggregate.Checked Then
                            '    formula = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'End If
                            .Formula = formula '("SUMIF(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ";" & Chr(34) & "<0" & Chr(34) & ";" & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            .Style.Numberformat.Format = "#,##0.00"

                            'Setting Background fill color to Gray
                            .Style.Fill.PatternType = ExcelFillStyle.Solid
                            .Style.Fill.BackgroundColor.SetColor(Color.Yellow)
                        End With
                    Next
                    ws.Cells(ws.Dimension.Address).AutoFitColumns()

                Next

                'Generate A File with Random name
                Dim bin As [Byte]() = p.GetAsByteArray()
                Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
                Try
                    File.WriteAllBytes(file__1, bin)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
#End Region
#Region "98-Local Methods"
    Friend Sub Cmd_Delete()
        'Me.Visible = False
        Try
            LoadData()
            Dim f1 As Hglp.FINDOC = Me.MasterBindingSource.Current
            'Select Case f1.SERIES
            '    Case 9594
            '        'Dim ccCRouting = f1.ccCRouting
            '        'For Each m As Panel.MTRLINE In ccCRouting
            '        '    Dim FINDOCS As Integer = m.FINDOCS
            '        '    Dim MTRL As Integer = m.MTRL
            '        '    Dim CCCLEYKO As String = m.MTRL1.CODE
            '        '    MTRL = dbp.MTRLs.Where(Function(f) f.CCCLEYKO = CCCLEYKO).Select(Function(f) f.MTRL).FirstOrDefault
            '        '    'Find Abafo
            '        '    Dim n As Panel.MTRLINE = dbp.ccCRouting.Where(Function(f) f.FINDOC = FINDOCS And f.MTRL = MTRL).FirstOrDefault
            '        '    If Not IsNothing(n) Then
            '        '        n.QTY1COV = 0
            '        '    End If
            '        'Next
            'End Select
            Me.MasterBindingSource.RemoveCurrent()
            'EndAllEdits()
            DataSafe()
        Catch ex As Exception

        End Try
        'Throw New NotImplementedException
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()
        Try

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            LoadDataInit() 'For Bind Any Control
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadDataInit()
        Try
            Dim conString As New SqlConnectionStringBuilder
            'My.Settings.Item("GenConnectionString") = My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.12.201,55555")
            'Select Case xconinf.CompanyId
            '    Case 1000 'ΕΛΛΑΓΡΟΛΙΠ ΑΕΒΕ
            '        'Hellagrolip 192.168.12.201,55555
            '        My.Settings.Item("GenConnectionString") = "Data Source=192.168.12.201,55555;Initial Catalog=Hglp;Persist Security Info=True;User ID=gm;Password=1mgergm++"
            '    Case 1002 'PFIC
            '        'PFIC 192.168.12.201,55555
            '        My.Settings.Item("GenConnectionString") = "Data Source=192.168.12.201,55555;Initial Catalog=PFIC;Persist Security Info=True;User ID=gm;Password=1mgergm++"
            '    Case 4000 'REVERA
            '        'Revera 192.168.12.201,55555
            '        My.Settings.Item("GenConnectionString") = "Data Source=192.168.12.201,55555;Initial Catalog=Revera;Persist Security Info=True;User ID=gm;Password=1mgergm++"
            'End Select
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            conString.ConnectionString = My.Settings.GenConnectionString.ToString
            Me.ToolStripStatusLabel1.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID
            'db = New DataClassesHglpDataContext(conn) 'My.Settings.PANELConnectionString)
            ''dbp.Connection.ConnectionString = CONNECT_STRING
            'Dim InitQuery = (From f In db.ccCRoutings Where f.ccCRouting = 0)
            Me.MasterBindingSource.DataSource = db.FINDOCs.Where(Function(f) f.FINDOC = 0)

            ''Dim emptyStatus As CCCSTATUS() = _
            ''          {New CCCSTATUS With {.DESCR = "<Select Status>", .CCCSTATUS = 0}}
            ''Me.CCCSTATUSBindingSource.DataSource = (From Empty In emptyStatus).Union( _
            ''                                      From s In db.CCCSTATUS _
            ''                                      Order By s.CCCSTATUS)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Me.DataSafe()
    End Sub

    ' Load the data.
    Private Sub LoadData()
        Try
            db.Log = Console.Out
            'BindControls() ' Fill Gen
            CurDrv = New Hglp.FINDOC
            CurDrv.FINDOC = SalDocTable(0, "FINDOC")
            If IsNothing(CurDrv) Then 'Add record
                Me.MasterBindingSource.AddNew()
                CurDrv = Me.MasterBindingSource.Current
                'For New Recode Standart Settings
                'Me.MasterBindingSource.EndEdit()
                'Me.ccCRoutingBindingSource.EndEdit()
            Else
                'Dim q As IQueryable(Of ccCRouting) = (From f In db.ccCRoutings Where f.ccCRouting = CurDrv.ccCRouting)
                'If ToTransform = 0 Then
                '    Me.MasterBindingSource.DataSource = q
                '    Dim rowFound As ccCRouting = (From g As ccCRouting In Me.MasterBindingSource Where g.ccCRouting = P_ccCRouting).FirstOrDefault()
                '    If Not IsNothing(rowFound) Then
                '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
                '        Me.MasterBindingSource.Position = itemFound
                '    End If
                'End If

                Dim findoc = db.FINDOCs.Where(Function(f) f.FINDOC = CurDrv.FINDOC).FirstOrDefault
                Me.MasterBindingSource.DataSource = findoc

                Dim mtlRow = MtrLinesTable.Current
                Dim mtrLs As Integer = mtlRow("MTRLINES")
                Dim mtrl As Integer = mtlRow("MTRL")
                Dim mtl = findoc.MTRLINEs.Where(Function(f) f.MTRLINES = mtrLs And f.MTRL = mtrl).FirstOrDefault
                Dim lst As New List(Of String)
                If mtl.ccCIDLabels IsNot Nothing Then
                    lst = mtl.ccCIDLabels.Split(",").ToList
                End If
                Me.MTRLINEBindingSource.DataSource = lst
                Me.MTRLINEDataGridView.AutoGenerateColumns = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub MasterBindingSource_AddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles MasterBindingSource.AddingNew
        Try
            Dim NFINDOC As New Hglp.FINDOC
            ''NFINDOC.COMPANY = 1
            ''NFINDOC.LOCKID = 1
            'NFINDOC.FINDOC = 0
            'NFINDOC.SOSOURCE = NSOSOURCE '1151
            ''NFINDOC.SOREDIR = 0
            ''NFINDOC.TRNDATE = Now()
            ''NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            ''NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            'NFINDOC.SERIES = NSeries '9590
            ''Dim snum As SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            ''NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            'NFINDOC.FPRMS = NSeries '9590
            'If NSeries = 9522 Then
            '    NFINDOC.FPRMS = 9520
            'End If
            'If NSeries = 9523 Then
            '    NFINDOC.FPRMS = 9521
            'End If
            'NFINDOC.TFPRMS = 100 '100
            'Dim fmt As String = ""
            'Select Case NSeries
            '    Case 9520
            '        fmt = "ΕΞΔ0000000"
            '    Case 9521
            '        fmt = "ΕΙΣΔ000000"
            '    Case 9522
            '        fmt = "ΕΞΧ0000000"
            '    Case 9523
            '        fmt = "ΕΙΣΧ000000"
            '    Case 9524
            '        fmt = "ΧΡΣΞ000000"
            '    Case 9590
            '        fmt = "ΑΠΓΡ000000"
            'End Select
            ''ΑΠΓΡ
            'NFINDOC.FINCODE = fmt
            'NFINDOC = ZeroFindoc(NFINDOC)
            'If ToTransform = 0 Then
            '    Dim NMTRLINE As New Panel.MTRLINE
            '    Dim MTRLINES As Integer = 1 'NFINDOC.MTRLINEs.Count + 1
            '    Dim LINENUM As Integer = 1
            '    NMTRLINE = ZeroMTRLINE(NMTRLINE, NFINDOC)
            '    NFINDOC.MTRLINEs.Add(NMTRLINE)
            'End If
            e.NewObject = NFINDOC
        Catch ex As Exception

        End Try
    End Sub
    Private Function ZeroFindoc(NFINDOC As Hglp.FINDOC) As Hglp.FINDOC
        Try
            'NFINDOC.COMPANY = 1
            'NFINDOC.LOCKID = 1
            ''NFINDOC.FINDOC = 0
            ''NFINDOC.SOSOURCE = 1171
            'NFINDOC.SOREDIR = 0
            'NFINDOC.TRNDATE = Now()
            'NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            'NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            'Dim snum As Panel.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            'NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            ''NFINDOC.FPRMS = 1001
            ''NFINDOC.TFPRMS = 100
            'Dim fmt As String = NFINDOC.FINCODE
            'NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
            'NFINDOC.BRANCH = 1
            'NFINDOC.SODTYPE = 11
            'NFINDOC.SOCURRENCY = 1
            'NFINDOC.TRDRRATE = 0
            'NFINDOC.LRATE = 1
            'NFINDOC.ORIGIN = 1
            'NFINDOC.GLUPD = 0
            'NFINDOC.SXUPD = 0
            'NFINDOC.PRDCOST = 0
            'NFINDOC.ISCANCEL = 0
            'NFINDOC.ISPRINT = 0
            'NFINDOC.ISREADONLY = 0
            'NFINDOC.APPRV = 1
            'NFINDOC.FULLYTRANSF = 0
            'NFINDOC.LTYPE1 = 1
            'NFINDOC.LTYPE2 = 0
            'NFINDOC.LTYPE3 = 0
            'NFINDOC.LTYPE4 = 0
            'NFINDOC.TURNOVR = 0
            'NFINDOC.TTURNOVR = 0
            'NFINDOC.LTURNOVR = 0
            'NFINDOC.VATAMNT = 0
            'NFINDOC.TVATAMNT = 0
            'NFINDOC.LVATAMNT = 0
            'NFINDOC.EXPN = 0
            'NFINDOC.TEXPN = 0
            'NFINDOC.LEXPN = 0
            'NFINDOC.DISC1PRC = 0
            'NFINDOC.DISC1VAL = 0
            'NFINDOC.TDISC1VAL = 0
            'NFINDOC.LDISC1VAL = 0
            'NFINDOC.DISC2PRC = 0
            'NFINDOC.DISC2VAL = 0
            'NFINDOC.TDISC2VAL = 0
            'NFINDOC.LDISC2VAL = 0
            'NFINDOC.NETAMNT = 0
            'NFINDOC.TNETAMNT = 0
            'NFINDOC.LNETAMNT = 0
            'NFINDOC.SUMAMNT = 0
            'NFINDOC.SUMTAMNT = 0
            'NFINDOC.SUMLAMNT = 0
            'NFINDOC.FXDIFFVAL = 0
            'NFINDOC.KEPYOQT = 0
            'NFINDOC.LKEPYOVAL = 0
            'NFINDOC.CHANGEVAL = 0
            'NFINDOC.ISTRIG = 0
            'NFINDOC.KEPYOMD = 0
        Catch ex As Exception

        End Try
        Return NFINDOC
        'Throw New NotImplementedException
    End Function

#End Region
End Class