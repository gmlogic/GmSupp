Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports GmSupp.Hglp
Imports Softone

Public Class ShipmentsBR
#Region "01-Declare Variables"
    Dim df As GmData
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    ' Declare a variable to indicate the commit scope.  
    ' Set this value to false to use cell-level commit scope.  
    Private rowScopeCommit As Boolean = True
    Private CCCVShipms As List(Of ccCVShipment)
    Dim fS1HiddenForm As New Form
    Dim conn As String
    Dim tlsShipingAllValue As String = ""
    Dim CompanyT As Integer = 0
#End Region
#Region "02-Declare Propertys"
    Public Property Series As Integer
        Get
            Return m_Series
        End Get
        Set(ByVal value As Integer)
            m_Series = value
        End Set
    End Property

#End Region
#Region "03-Load Form"
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))

        'LoadDataInit()

        'Dim txtUser = "gmlogic"
        'Dim txtPass = "1mgergm++"
        'Dim txtCompany = "1000"
        'Dim txtBranch = "1000"
        'Dim txtXCOFile = "C:\Softone\softone.XCO" '"C:\SOFTONE\GmHglp.XCO"
        'Dim q = From line In IO.File.ReadAllLines(txtXCOFile, Encoding.GetEncoding("windows-1253"))

        'Dim SERVER As String = ""
        'Dim DATABASE As String = ""
        'For Each ss As String In q
        '    If ss.Contains("SERVER") Then
        '        SERVER = ss.Split("=")(1)
        '    End If
        '    If ss.Contains("DATABASE") Then
        '        DATABASE = ss.Split("=")(1)
        '    End If

        'Next

        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID '

        ''Let part = line.Split(";"c)
        ''Select New REC02_Rec With {.EXTC_INTCODE = part(0), .FILE_NAME = part(1), .IS_LEGAL = part(2), .FILE_ROW = part(3), .REASON = part(4)}
        'Dim DTLogin = DateTime.Now
        'Try
        '    XSupport.InitInterop(0, "C:\Softone\XDll.dll") 'E:\sc\GmSupp\GmSupp\bin\Debug C:\Softone\Xplorer.exe /host:192.168.12.201

        '    's1Conn = XSupport.Login(txtXCOFile, txtUser.ToString, txtPass.ToString,
        '    '                           txtCompany.ToString, txtBranch.ToString, DTLogin)
        '    's1Conn == XSupport.Login(xco, UserName, Password, Company, Branch, LoginDate);


        '    s1Conn = cnnnect(txtXCOFile, txtUser.ToString, txtPass.ToString,
        '                                   txtCompany.ToString, txtBranch.ToString, DTLogin)
        'Catch ex As Exception
        '    MsgBox("11 " & ex.Message)
        'End Try

        ''Exit Sub

        'Me.TlSTxtSERIES.Text = 1021
        'Me.TlSTxtFPRMS.Text = "1000,1061,1066,7001,7021,7023,7039,7040,7041,7042,7043,7045,7046,7060,7061,7062,7063,7064,7066,7067,7068,7069,7071,7072,7073,7076,7082,7083,7084,7127,7141,7143,7144"

        Me.chkLstBoxFULLYTRANSF.SetItemChecked(0, True)
        Me.chkLstBoxFULLYTRANSF.SetItemChecked(2, True)

        Dim emptySOCARRIER As SOCARRIER()
        emptySOCARRIER = {New SOCARRIER With {.NAME = "<Επιλέγξτε>", .SOCARRIER = 0}}

        Dim q1 = emptySOCARRIER.ToList.Union(db.SOCARRIERs.ToList)

        Me.NAMEComboBox.DataSource = q1.ToList

        'frm.ccCSOCARRIER = current.ccCSOCARRIER

        Me.NAMEComboBox.DisplayMember = "NAME"
        Me.NAMEComboBox.ValueMember = "SOCARRIER"
        Me.NAMEComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Me.NAMEComboBox.AutoCompleteSource = AutoCompleteSource.ListItems
        Me.tlsBtnAutoSoCarrier.Enabled = False

        ' When the form loads, the KeyPreview property is set to True.
        ' This lets the form capture keyboard events before
        ' any other element in the form.
        Me.KeyPreview = True
        If CurUser = "gmlogic" Then
            Me.txtFINCODE.Text = "ΧΔΑ Θ 1290;11ΕΝΔK22076;11ΕΝΔK21790*" ' "11ΔΙΑ-Κ1481" ' "ΠΑΡΑΓ01028" '"11ΔΙΑ-Κ026*" '"8ΔΑ-ΘΑ3764" '"1ΠΑΡ105753"
            Me.TlSTxtSERIES.Text = 7030
            'Me.TlSTxtMTRL.Text = "3103030464"
            Me.chkLstBoxFULLYTRANSF.SelectedItems.Clear()
            Me.chkLstBoxFULLYTRANSF.SetItemChecked(0, True)
            Me.Panel1.Visible = True
            Me.tlsBtnAutoSoCarrier.Enabled = True
            Me.btnLinked.Visible = True
        End If

    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.Alt And e.KeyCode = Keys.S Then
            Me.BindingNavigatorSaveItem.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview is True, and this is from the FORM.")
        End If
    End Sub

    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = DataSafe()
    End Sub
#End Region
#Region "04-Bas_Commands"
    Private Sub Cmd_Edit()
        Try
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim str As String = ""
                'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + me.MasterDataGridView.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
                Dim drv As ccCVShipment = Me.MasterBindingSource.Current
                str = "SALDOC[AUTOLOCATE=" & drv.FINDOC & "]"
                'str = "SALDOC[AUTOEXEC=2, FORCEVALUES=INT02:" & drv.FINDOC & "?SERIES:1001]"
                'XSupport.InitInterop(fS1HiddenForm.Handle)
                s1Conn.ExecS1Command(str, fS1HiddenForm)
                'Fillme.MasterDataGridView_gm(iActiveObjType)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                Me.Cursor = Cursors.Default
            End Try
            'If Me.boxOption_Paragelies.Checked Then
            '    Exit Sub
            'End If
            'Dim TFormF
            'TFormF = New ParagFBR
            ''If Me.boxOption_Apografh.Checked Then
            ''    TFormF = New ParagFBRAPG
            ''End If
            'Dim drv As FINDOC_MTRLINE = Me.MasterBindingSource.Current
            'If drv.Series = 9590 Then '"ΑΠΟΓΡΑΦΗToolStripMenuItem" 
            '    TFormF = New ParagFBRDIAK 'ParagFBRAPG
            '    TFormF.NSeries = 9590
            '    TFormF.NSOSOURCE = 1151
            'End If
            ''If Not Array.IndexOf({9520, 9521, 9522, 9523}, CType(drv.Series, Integer)) = -1 Then ' = 9520 Or drv.Series = 9521 or drv.Series=9522 or drv Then
            'If {9520, 9521, 9522, 9523, 9524, 9526}.Contains(drv.Series) Then
            '    TFormF = New ParagFBRDIAK
            '    'ΕΣΩΤΕΡΙΚΗ ΔΙΑΚΙΝΗΣΗ - 9520 (-1095+1096)
            '    'TFormF.NSeries = 9520
            '    'TFormF.NSOSOURCE = 1151
            'End If
            ''TFormF.CurDrv = New MTRLINE
            'Dim indx As Integer = drv.FINDOC
            'TFormF.CurDrv = db.MTRLINEs.Where(Function(f) f.FINDOC = indx).FirstOrDefault
            'TFormF.P_MTRLINES = drv.MTRLINES
            'Dim Position As Integer = Me.MasterBindingSource.Position
            'TFormF.DgdvRefresh = False
            'TFormF.Text = Me.Tag & " - " & TFormF.Name
            'TFormF.ShowDialog()
            'If TFormF.DgdvRefresh = True Then
            '    'Me.DataGridViewMaster.Refresh()
            '    Cmd_Select()
            '    ' Set the Position property to the results of the Find method. 
            '    Dim rowFound As FINDOC_MTRLINE = (From g As FINDOC_MTRLINE In Me.MasterBindingSource Where g.FINDOC = TFormF.CurDrv.FINDOC).FirstOrDefault()
            '    If Not IsNothing(rowFound) Then
            '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
            '        Me.MasterBindingSource.Position = itemFound
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Select()
        Try
            tlsShipingAllValue = ""
            Me.Cursor = Cursors.NoMove2D
            LoadData()
            db.Log = Console.Out

            Dim q = db.ccCVShipments.AsQueryable '.AsEnumerable

            'Dim cmdText As String = "select user_id from CCCVShipments where user_loginname='" + AddressOf Me.UserFName + "'"
            'q = q.Where(Function(f) f.dtEntry = DateTimePicker1.Value) ' And f.dtEntry.Value <= DateTimePicker2.Value).ToList
            'q = q.Where(Function(f) f.domUser = "ndavradou")
            'q = q.Where(Function(p) p.dtEntry >= DateTimePicker1.Value.Date And p.dtEntry <= DateTimePicker2.Value)
            'WHERE(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
            '              And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
            Dim qwh = q.Where(Function(f) f.COMPANY = CompanyT And f.SOSOURCE = 1351 And f.SOREDIR = 0 And f.SODTYPE = 13)
            'qwh = qwh.Where(Function(f) {201}.Contains(f.TFPRMS) And Me.TlSBtnFULLYTRANSF.Text.Split(",").Contains(f.FULLYTRANSF)) ' And f.PENDING >= 1)
            'q = q.OrderBy(Function(f) f.Department.ToString + f.user_gr_fullname)
            If Me.TlSComboBoxDate.Text = "Ημ/νία Παράδοσης:" Then
                qwh = qwh.Where(Function(f) f.ccCDELIVDATE >= DateTimePicker1.Value.Date And f.ccCDELIVDATE <= DateTimePicker2.Value)
            End If
            If Me.TlSComboBoxDate.Text = "Ημ/νία Παραστατικού:" Then
                qwh = qwh.Where(Function(f) f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value)
            End If

            'WHERE        (COMPANY = 1000) AND (SOSOURCE = 1351) AND (SOREDIR = 0) AND (TFPRMS IN (201)) AND (SODTYPE = 13) AND (FULLYTRANSF IN (0, 2)) AND (PENDING >= 1) AND (TRNDATE >= '20170101') AND (TRNDATE < '20170601')

            'Dim FPRMS() As Integer = {1000, 1061, 1066, 7001, 7021, 7023, 7039, 7040, 7041, 7042, 7043, 7045, 7046, 7060, 7061, 7062, 7063, 7064, 7066, 7067, 7068, 7069, 7071, 7072, 7073, 7076, 7082, 7083, 7084, 7127, 7141, 7143, 7144}
            If Not Me.TlSTxtSERIES.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtSERIES.Text.Split(",").Contains(f.SERIES)) 'Σειρά
            End If

            If Not Me.TlSTxtFPRMS.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtFPRMS.Text.Split(",").Contains(f.FPRMS)) 'Τύπος
            End If

            If Not Me.TlSTxtTFPRMS.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtTFPRMS.Text.Split(",").Contains(f.TFPRMS)) 'Τύπος
                qwh = qwh.Where(Function(f) (f.ISCANCEL = 0))
            End If

            Dim FULLYTRANSF = Me.chkLstBoxFULLYTRANSF.CheckedItems.Cast(Of String).Select(Function(f) f.Substring(0, 1)).ToList()

            If Not FULLYTRANSF.Count = 0 Then
                '0  Μετασχηματισμός(Όχι)
                '1  Μετασχηματισμός(Πλήρως)
                '2  Μετασχηματισμός(Μερικώς)
                '3  Μετασχηματισμένο
                qwh = qwh.Where(Function(f) FULLYTRANSF.Contains(f.FULLYTRANSF))
            End If

            If Not Me.TlSTxtPENDING.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtPENDING.Text.Split(",").Contains(f.PENDING))
            End If

            If Not Me.TlSTxtPRSN.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtPRSN.Text.Split(",").Contains(f.PRSNCODE))
            End If

            If Not Me.txtFINCODE.Text = "" Then
                qwh = q.Where(Function(f) f.COMPANY = CompanyT And f.SOSOURCE = 1351 And f.SOREDIR = 0 And f.SODTYPE = 13)
                If Me.txtFINCODE.Text.Contains(";") Then
                    Dim str = Me.txtFINCODE.Text.Replace("*", "")
                    Dim strs = str.Split(";")
                    qwh = qwh.Where(Function(f) strs.Contains(f.FINCODE)) '1ΠΑΡ105753
                Else
                    qwh = qwh.Where(Function(f) f.FINCODE Like Me.txtFINCODE.Text) '1ΠΑΡ105753
                End If

                qwh = qwh.Where(Function(f) f.TRNDATE.Year >= DateTimePicker1.Value.Year And f.TRNDATE.Year <= DateTimePicker2.Value.Year)
            End If

            If Not Me.txtTRUCKSNO.Text = "" Then
                qwh = q.Where(Function(f) f.COMPANY = CompanyT And f.SOSOURCE = 1351 And f.SOREDIR = 0 And f.SODTYPE = 13)
                qwh = qwh.Where(Function(f) f.TRUCKSNO Like Me.txtTRUCKSNO.Text)
            End If

            If Not Me.TlSTxtTRDR.Text = "" Then
                qwh = qwh.Where(Function(f) f.TRDRCODE Like Me.TlSTxtTRDR.Text)
            End If

            If Not Me.TlSTxtMTRL.Text = "" Then
                qwh = qwh.Where(Function(f) f.CODE Like Me.TlSTxtMTRL.Text)
            End If

            If Not Me.TlSTxtWHOUSE.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtWHOUSE.Text.Split(",").Contains(f.WHOUSE))
            End If

            If Not Me.txtSOCARRIERNAME.Text = "" Then
                qwh = qwh.Where(Function(f) f.SOCARRIERNAME Like Me.txtSOCARRIERNAME.Text)
            End If

            If Not Me.NAMEComboBox.SelectedValue = 0 Then
                qwh = qwh.Where(Function(f) f.SOCARRIER = CType(Me.NAMEComboBox.SelectedValue, Short))
            End If

            qwh = qwh.OrderBy(Function(f) f.TRNDATE)

            Me.MasterBindingSource.DataSource = qwh '.MasterBindingSource.DataSource = New SortableBindingList(Of FINDOC_MTRLINE)(nq) 'dt
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource


            'dim emptyorlog as orariolog() = nothing
            'emptyorlog = {new orariolog with {.domuser = "--επιλέγξτε--"}}
            'CCCVShipms = (from empty in emptyorlog).union(db.CCCVShipments.orderby(function(f) f.domuser).tolist).tolist

            'CCCVShipms = db.CCCVShipments.OrderBy(Function(f) f.domUser).Where(Function(f) f.dtEntry.Value > New Date(2016, 12, 1)).ToList
            Me.lblTQTY1.Text = ""
            Me.lblTQTY1PRO.Text = ""
            If qwh.Count > 0 Then
                Dim SumQty1 As Double = 0
                Dim SumCCCQty1Pro As Double = 0

                SumQty1 = qwh.Sum(Function(f) f.QTY1)
                SumCCCQty1Pro = qwh.Sum(Function(f) f.CCCQTY1PRO)
                Me.lbSumQTY1.Text = "Συν.Ποσότ"
                Me.lblTQTY1.Text = String.Format("{0:N3}", SumQty1)
                Me.lblSumQTY1PRO.Text = "Συν.Ποσότ.Φορτ"
                Me.lblTQTY1PRO.Text = String.Format("{0:N3}", SumCCCQty1Pro)
            End If


            MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)

        End Try
        Me.Cursor = Cursors.Default
    End Sub
#End Region
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        Me.MasterBindingSource.EndEdit()
    End Sub
    Private Function DataSafe() As Boolean
        DataSafe = False
        ' Finish any current edits.
        EndAllEdits()

        If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function

        ' Ask the user if we should save the changes.
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") 'MeLabel)
            Case MsgBoxResult.No
                ' The data is not safe.
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            'If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
            '    If Not Conditions() Then
            '        Exit Function
            '    End If
            'End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    'LogSQL = sSQL
                    db.Log = Console.Out
                    db.SubmitChanges()

                    ' The Complete method commits the transaction. If an exception has been thrown, 
                    ' Complete is called and the transaction is rolled back.
                    scope.Complete()
                    SaveData = True
                End Using
            Catch ex As TransactionAbortedException
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message)
            Catch ex As ApplicationException
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
#End Region
#Region "96-MasterDataGridView"
    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender

        Dim ar() As String = {"Ποσότ.Φορτ", "Εκτελ", "PRIORITY", "Locked", "Μετασχηματισμός", "Εκρεμότητα", "Κατάσταση", "FFINDOCS", "FINDOCS", "MTRLINESS"}
        For Each aa In ar
            If s.Columns(s.CurrentCell.ColumnIndex).Name = aa Then
                Exit Sub
            End If
        Next

        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "Εκτελ" Then
        '    Exit Sub
        'End If
        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "PRIORITY" Then
        '    Exit Sub
        'End If
        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "Locked" Then
        '    Exit Sub
        'End If
        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "Μετασχηματισμός" Then
        '    Exit Sub
        'End If
        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "Κατάσταση" Then
        '    Exit Sub
        'End If

        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellValueChanged
        If MasterDataGridView.Columns(e.ColumnIndex).Name = "Check" Then
            'Dim buttonCell As DataGridViewDisableButtonCell =
            '    CType(dataGridView1.Rows(e.RowIndex).Cells("Buttons"),
            '    DataGridViewDisableButtonCell)

            Dim checkCell As DataGridViewCheckBoxCell =
                CType(MasterDataGridView.Rows(e.RowIndex).Cells("Check"),
                DataGridViewCheckBoxCell)
            'checkCell
            'buttonCell.Enabled = Not CType(checkCell.Value, [Boolean])

            'dataGridView1.Invalidate()
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Dim drv As DataRowView = Me.MasterBindingSource.Current

        'Dim status = Me.MasterDataGridView.Columns(e.ColumnIndex)
        'Me.StatusStrip1.Text = status
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            Me.MasterDataGridView.AutoResizeColumns()

            'myArrF = ("NAME2,WHOUSENAME,TRDRNAME,NAME,QTY1,SHIPDATE,FINCODE,VARCHAR01,SERIES,TRDR,CITY,FPRMS,SOCARRIERNAME,VARCHAR02,FINDOC").Split(",")
            'myArrF = ("NAME2,WHOUSENAME,TRDRNAME,NAME,QTY1,MTRUNIT,QTY1PRO,SHIPDATE,FINCODE,FINCODE,FINCODE,VARCHAR01,SERIES,SHIPVALUE,CITY,FPRMS,SOCARRIERNAME,TRUCKSNO,FINDOC").Split(",")
            'myArrN = ("ΠΩΛΗΤΗΣ,ΑΠΟ,ΠΡΟΣ,Λίπασμα,Ποσότητα,Μ/Μ,Ποσότ.Φορτ,Ημ/νία Φόρτωσης,Αρ.Παραγγελίας,Αρ.Δελτ.Φόρτωσης,Αρ.Δελτ.Αποστολής,Αρ.Φορτωτικής,Κόστος Φορτωτικής,Κόμιστρο €,Προορισμός,Φορτίο,Μεταφορέας,Πινακίδα,FINDOC").Split(",")


            myArrF = ("NAME2,WHOUSENAME,TRDBRANCHNAME,NAME,QTY1,MTRUNITSHORTCUT,CCCQTY1PRO,CCCDELIVDATE,MTRLINESCCCSHIPVALUE,CITY,SOCARRIERMTRLINESNAME,CCCTRUCKSNO,SOCARRIERNAME,TRUCKSNO,QTY1COV,CCCQTY1DIAF,ccCTOTSHIPVALUE,ccCShippingNo,DELIVDATE,TRNDATE,FINDOC,MTRLINES,LINENUM,MTRL,Series,FPRMS,TFPRMS,FULLYTRANSF,PENDING,RESTMODE,PRICE,NUM02,FINSTATES,FFINDOCS,FINDOCS,MTRLINESS,TRDRNAME,CODE").Split(",")
            myArrN = ("ΠΩΛΗΤΗΣ,ΑΠΟ,ΠΡΟΣ(Υποκ),Λίπασμα,Ποσότητα,Μ/Μ,Ποσότ.Φορτ,Γ-Ημ/νία Παράδοσης,Γ-Κόμιστρο €,Προορισμός,Γ-Μεταφορέας,Γ-Πινακίδα,Μεταφορέας,Πινακίδα,Εκτελ,Διαφορά,Συν-Κόμι €,Αρ.Φορτωτικής,Ημ/νία Παράδοσης,Ημ/νία Παρ/κού,FINDOC,MTRLINES,LINENUM,MTRL,Σειρά,Τύπος,Συμπεριφορά,Μετασχηματισμός,Εκρεμότητα,RESTMODE,Τιμή,Εκπτ.1,Κατάσταση,FFINDOCS,FINDOCS,MTRLINESS,TRDRNAME,CODE").Split(",")

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
                MasterDataGridView.Columns(i).ReadOnly = True
            Next
            AddOutOfOfficeColumn(Me.MasterDataGridView)

            'Αρ.Παραγγελίας,Αρ.Δελτ.Φόρτωσης,Αρ.Δελτ.Αποστολής
            'Dim ARPARAG As String = "" 'Αρ.Παραγγελίας
            'Dim ARDFO As String = "" 'Αρ.Δελτ.Φόρτωσης
            'Dim ARDA As String =

            'DataGridViewComboBoxColumn1
            '
            Dim columnComboBox As New DataGridViewComboBoxColumn()
            'columnComboBox.DataPropertyName = "CCCPRIORITY"
            Dim prs = db.PRIORITies.ToList
            Dim pr As New PRIORITY
            pr.PRIORITY = 0
            pr.NAME = "Επιλέγξτε"
            prs.Insert(0, pr)
            columnComboBox.DataSource = prs
            columnComboBox.DisplayMember = "NAME"
            columnComboBox.HeaderText = "Προτεραιότητα"
            columnComboBox.Name = "PRIORITY"
            columnComboBox.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            columnComboBox.SortMode = DataGridViewColumnSortMode.Automatic
            columnComboBox.ValueMember = "PRIORITY"
            columnComboBox.Width = 80
            columnComboBox.FlatStyle = FlatStyle.Flat
            MasterDataGridView.Columns.Insert(9, columnComboBox)

            Dim ARPARAGDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'ARPARAGDataGridViewTextBoxColumn.DataPropertyName = "FINDOC"
            ARPARAGDataGridViewTextBoxColumn.HeaderText = "Αρ.Παραγγελίας"
            ARPARAGDataGridViewTextBoxColumn.Name = "Αρ.Παραγγελίας"
            MasterDataGridView.Columns.Insert(10, ARPARAGDataGridViewTextBoxColumn)

            Dim ARDFODataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'ARDFODataGridViewTextBoxColumn.DataPropertyName = "FINDOC"
            ARDFODataGridViewTextBoxColumn.HeaderText = "Αρ.Δελτ.Φόρτωσης"
            ARDFODataGridViewTextBoxColumn.Name = "Αρ.Δελτ.Φόρτωσης"
            MasterDataGridView.Columns.Insert(11, ARDFODataGridViewTextBoxColumn)

            Dim ARDADataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'ARDADataGridViewTextBoxColumn.DataPropertyName = "FINDOC"
            ARDADataGridViewTextBoxColumn.HeaderText = "Αρ.Δελτ.Αποστολής"
            ARDADataGridViewTextBoxColumn.Name = "Αρ.Δελτ.Αποστολής"
            MasterDataGridView.Columns.Insert(12, ARDADataGridViewTextBoxColumn)

            Dim column As New DataGridViewCheckBoxColumn()

            With column
                .DataPropertyName = "MTRLINESCCCADR"
                .HeaderText = "Γ-ADR" 'ColumnName.OutOfOffice.ToString()
                .Name = "MTRLINESCCCADR" 'ColumnName.OutOfOffice.ToString()
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .FlatStyle = FlatStyle.Standard
                .CellTemplate = New DataGridViewCheckBoxCell()
                .CellTemplate.Style.BackColor = Drawing.Color.Beige
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With
            MasterDataGridView.Columns.Insert(15, column)

            column = New DataGridViewCheckBoxColumn()
            With column
                .DataPropertyName = "ccCLocked"
                .HeaderText = "Locked" 'ColumnName.OutOfOffice.ToString()
                .Name = "ccCLocked" 'ColumnName.OutOfOffice.ToString()
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .FlatStyle = FlatStyle.Standard
                .CellTemplate = New DataGridViewCheckBoxCell()
                .CellTemplate.Style.BackColor = Drawing.Color.Beige
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With
            MasterDataGridView.Columns.Insert(17, column)

            column = New DataGridViewCheckBoxColumn()
            With column
                .DataPropertyName = "ccCLockShipValue"
                .HeaderText = "LockedΜετ"
                .Name = "ccCLockShipValue"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .FlatStyle = FlatStyle.Standard
                .CellTemplate = New DataGridViewCheckBoxCell()
                .CellTemplate.Style.BackColor = Drawing.Color.Beige
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With
            MasterDataGridView.Columns.Insert(24, column)

            Dim DSCDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'ARDFODataGridViewTextBoxColumn.DataPropertyName = "FINDOC"
            DSCDataGridViewTextBoxColumn.HeaderText = "Έκπτωση%"
            DSCDataGridViewTextBoxColumn.Name = "Έκπτωση%"
            MasterDataGridView.Columns.Insert(39, DSCDataGridViewTextBoxColumn)




            'For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
            '    Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            'Next

            'If Not IsNothing(MasterDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
            '    MasterDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
            'End If


            If Not IsNothing(MasterDataGridView.Columns("Ποσότητα")) Then
                MasterDataGridView.Columns("Ποσότητα").DefaultCellStyle.Format = "N3"
            End If
            If Not IsNothing(MasterDataGridView.Columns("Ποσότ.Φορτ")) Then
                MasterDataGridView.Columns("Ποσότ.Φορτ").DefaultCellStyle.Format = "N3"
                'MasterDataGridView.Columns("Ποσότ.Φορτ").ReadOnly = False
                'MasterDataGridView.Columns("Ποσότ.Φορτ").DefaultCellStyle.For = False
            End If

            If Not IsNothing(MasterDataGridView.Columns("Εκτελ")) Then
                MasterDataGridView.Columns("Εκτελ").DefaultCellStyle.Format = "N3"
                'If CurUser = "gmlogic" Then
                '    MasterDataGridView.Columns("Εκτελ").ReadOnly = False
                'End If
                'MasterDataGridView.Columns("Ποσότ.Φορτ").DefaultCellStyle.For = False
            End If
            If Not IsNothing(MasterDataGridView.Columns("Μ/Μ")) Then
                MasterDataGridView.Columns("Μ/Μ").Width = 50
            End If

            Dim ar() As String = {"Ποσότ.Φορτ", "Εκτελ", "Μετασχηματισμός", "Εκρεμότητα", "Κατάσταση", "FFINDOCS", "FINDOCS", "MTRLINESS"}
            For Each aa In ar
                If Not IsNothing(MasterDataGridView.Columns(aa)) Then
                    If CurUser = "gmlogic" Then
                        MasterDataGridView.Columns(aa).ReadOnly = False
                    Else
                        If aa = "Ποσότ.Φορτ" Then
                            MasterDataGridView.Columns(aa).ReadOnly = False
                        End If
                    End If
                End If
            Next
            'If Not IsNothing(MasterDataGridView.Columns("Μετασχηματισμός")) Then
            '    If CurUser = "gmlogic" Then
            '        MasterDataGridView.Columns("Μετασχηματισμός").ReadOnly = False
            '    End If
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("Εκρεμότητα")) Then
            '    If CurUser = "gmlogic" Then
            '        MasterDataGridView.Columns("Εκρεμότητα").ReadOnly = False
            '    End If
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("Κατάσταση")) Then
            '    If CurUser = "gmlogic" Then
            '        MasterDataGridView.Columns("Κατάσταση").ReadOnly = False
            '    End If
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("Κατάσταση")) Then
            '    If CurUser = "gmlogic" Then
            '        MasterDataGridView.Columns("Κατάσταση").ReadOnly = False
            '    End If
            'End If



            'Τιμή,Εκπτ.1
            If Not IsNothing(MasterDataGridView.Columns("Τιμή")) Then
                MasterDataGridView.Columns("Τιμή").DefaultCellStyle.Format = "N2"
            End If
            If Not IsNothing(MasterDataGridView.Columns("Έκπτωση%")) Then
                MasterDataGridView.Columns("Έκπτωση%").DefaultCellStyle.Format = "N5"
                MasterDataGridView.Columns("Έκπτωση%").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            End If
            If Not IsNothing(MasterDataGridView.Columns("Εκπτ.1")) Then
                MasterDataGridView.Columns("Εκπτ.1").DefaultCellStyle.Format = "N2"
            End If

            'Μετασχηματισμός,Εκρεμότητα

            'If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
            '    MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
            'End If

            'If f.SERIES = 9593 Then 'ΑΒΑΦΟ
            '    If Not IsNothing(MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ")) Then
            '        MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ").ReadOnly = True
            '    End If
            'End If



            ''Add Columns to MasterDataGridView
            'Me.MasterDataGridView.Columns.Insert(0, DataGridViewComboBoxColumnMTRL_CODE)
            'Me.MasterDataGridView.Columns.Insert(0, Search_Code)
            'Me.MasterDataGridView.Columns.Add(DataGridViewTextBox_OLDMTRL)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)

            ''Fill Unbound Collumns
            'For Each row As DataGridViewRow In MasterDataGridView.Rows
            '    Dim dll As DataGridViewComboBoxCell = row.Cells("DataGridViewComboBoxColumnMTRL_CODE")
            '    Dim MTRL As Integer = row.Cells("MTRL").Value

            '    Dim m As MTRL = db.MTRLs.Where(Function(f1) f1.MTRL = MTRL).FirstOrDefault
            '    If Not IsNothing(m) Then
            '        dll.Items.Add(m)
            '        dll.Value = MTRL
            '        row.Cells("DataGridViewTextBox_OLDMTRL").Value = MTRL
            '    End If
            'Next


            ''DepartmentDataGridViewComboBoxColumn
            ''
            'Me.DepartmentDataGridViewComboBoxColumn.DataPropertyName = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.HeaderText = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
            'Me.DepartmentDataGridViewComboBoxColumn.Name = "DepartmentDataGridViewComboBoxColumn"
            'Me.DepartmentDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.DepartmentDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            ''
            ''StateDataGridViewComboBoxColumn
            ''
            'Me.StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            'Me.StateDataGridViewComboBoxColumn.HeaderText = "State"
            'Me.StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            'Me.StateDataGridViewComboBoxColumn.Name = "StateDataGridViewComboBoxColumn"
            'Me.StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic


            For RCount = 1 To 6
                'With MasterDataGridView.Columns(RCount)
                '    ' Don't allow the column to be resizable.
                '    .Resizable = DataGridViewTriState.False
                '    ' Make the check box column frozen so it is always visible.
                '    .Frozen = True
                '    ' Put an extra border to make the frozen column more visible
                '    .DividerWidth = 1
                'End With
            Next

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                Dim item As ccCVShipment = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        Dim ARPARAG As String = "" 'Αρ.Παραγγελίας
                        Dim ARDFO As String = "" 'Αρ.Δελτ.Φόρτωσης
                        Dim ARDA As String = "" 'Αρ.Δελτ.Αποστολής


                        If Me.ToolStripComboBoxIndexes.SelectedItem = "Εκκρεμείς Παραγγελίες" Then
                            ARPARAG = item.FINCODE
                        End If

                        If Me.ToolStripComboBoxIndexes.SelectedItem = "Εκκρεμή Δελτία φόρτωσης" Then
                            ARDFO = item.FINCODE
                        End If

                        If Me.ToolStripComboBoxIndexes.SelectedItem = "Κατάσταση παραδόσεων" Then
                            ARDA = item.FINCODE
                            Dim q = db.ccCVShipments.Where(Function(f) f.FINDOCS = item.FINDOC).FirstOrDefault
                            If Not IsNothing(q) Then
                                ARDFO = q.FINCODE
                            End If

                        End If

                        'ARPARAG = item.FINCODE
                        'ARDFO = item.FINCODE
                        'ARDFO = item.FINCODE

                        row.Cells("Αρ.Παραγγελίας").Value = Nothing
                        row.Cells("Αρ.Δελτ.Φόρτωσης").Value = Nothing
                        row.Cells("Αρ.Δελτ.Αποστολής").Value = Nothing

                        If Not ARPARAG = "" Then
                            row.Cells("Αρ.Παραγγελίας").Value = ARPARAG
                        End If
                        If Not ARDFO = "" Then
                            row.Cells("Αρ.Δελτ.Φόρτωσης").Value = ARDFO
                        End If
                        If Not ARDA = "" Then
                            row.Cells("Αρ.Δελτ.Αποστολής").Value = ARDA
                        End If

                        Dim dll As DataGridViewComboBoxCell = row.Cells("PRIORITY")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Items.AddRange(New Object() {"ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
                        If Not IsNothing(item.ccCPRIORITY) Then
                            dll.Value = dll.Items.Cast(Of PRIORITY).Where(Function(f) f.PRIORITY = item.ccCPRIORITY).FirstOrDefault.NAME
                        Else
                            dll.Value = dll.Items.Cast(Of PRIORITY).Where(Function(f) f.PRIORITY = 0).FirstOrDefault.NAME
                        End If

                        'Set colors
                        If Not IsNothing(item.ccCSOCARRIER) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.YellowGreen
                            If IsNothing(item.ccCTRUCKSNO) Then
                                row.DefaultCellStyle.BackColor = Drawing.Color.Yellow
                            End If
                        End If

                        If Not IsNothing(item.MTRLINESCCCADR) Then
                            row.Cells("MTRLINESCCCADR").Style.BackColor = Drawing.Color.Beige
                            If item.MTRLINESCCCADR Then
                                row.Cells("MTRLINESCCCADR").Style.BackColor = Drawing.Color.Red
                            End If
                        End If

                        If Not IsNothing(item.ccCPRIORITY) Then
                            If item.ccCPRIORITY = 104 Then
                                row.Cells("PRIORITY").Style.BackColor = Drawing.Color.Orange
                                'row.Cells("PRIORITY").Style.ForeColor = Drawing.Color.White
                            End If
                            If item.ccCPRIORITY = 105 Then
                                row.Cells("PRIORITY").Style.BackColor = Drawing.Color.Red
                                'row.Cells("PRIORITY").Style.ForeColor = Drawing.Color.White
                            End If
                        End If


                        'row.Cells("Κόστος Φορτωτικής").Value = Nothing ',Κόμιστρο €,Προορισμός,Φορτίο
                        'row.Cells("Φορτίο").Value = Nothing ' "ΑΠΛΟ" 'ADR  ΑΠΛΟ

                        'Dim dll As DataGridViewComboBoxCell = row.Cells("DepartmentDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Items.AddRange(New Object() {"ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
                        ''If Not IsNothing(item.Department) Then
                        ''    dll.Value = dll.Items(item.Department)
                        ''End If


                        'dll = row.Cells("domUserDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Value = dll.Items(0)
                        'If Not IsNothing(CCCVShipms) Then
                        '    'Dim OrLgs = CCCVShipms.
                        '    '    Where(Function(f) Not item.user_loginname = Nothing AndAlso f.domUser.Contains(item.user_loginname)).
                        '    '    Select(Function(f) f.domUser).Distinct.ToList
                        '    'dll.Items.AddRange(OrLgs.ToArray)

                        '    'If OrLgs.Count = 1 Then
                        '    '    dll.Value = OrLgs.FirstOrDefault
                        '    'End If
                        'End If


                        'dll = row.Cells("CapacityDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        ''If Not IsNothing(item.Capacity) Then
                        ''    For Each st In item.Capacity.Split("|")
                        ''        dll.Items.Add(st)
                        ''    Next
                        ''    dll.Value = item.Capacity.Split("|")(0)
                        ''Else
                        ''    dll.Items.AddRange(New Object() {"ALB", "EFG", "PIR"})
                        ''End If


                    Catch ex As Exception

                    End Try
                End If

            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub MasterDataGridView_Sorted(sender As Object, e As EventArgs) Handles MasterDataGridView.Sorted
        MasterDataGridView_Styling()
    End Sub
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click ' Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click
        Dim s As ToolStripButton = sender
        Dim check As Boolean = False
        If s.Name = "TlSBtnCheck" Then
            check = True
        Else
            check = False
        End If
        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                If Not ds.Cells("Check").Value = check Then
                    ds.Cells("Check").Value = check
                End If
            Next
            'For i As Integer = 0 To DrSel.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
            'Next
        Else
            For Each ds As DataGridViewRow In Me.MasterDataGridView.Rows
                ds.Cells("Check").Value = check
            Next
            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
            'Next
        End If
        Me.MasterDataGridView.RefreshEdit()
    End Sub
    Private Sub MasterDataGridView_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MasterDataGridView.CellFormatting
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name.Equals("Ποσότ.Φορτ") Then
            '    ' Use helper method to get the string from lookup table
            '    Dim MTRL As Integer = s.Rows(e.RowIndex).Cells("MTRL").Value
            '    Dim m As MTRL = db.MTRLs.Where(Function(f) f.MTRL = MTRL).FirstOrDefault
            '    If Not IsNothing(m) Then
            '        e.Value = m.CODE 'GetWorkplaceNameLookupValue(dataGridViewScanDetails.Rows(e.RowIndex).Cells("UserWorkplaceID").Value)
            '    End If
            'Dim col As DataGridViewTextBoxColumn
            'col.
            'Dim cell As DataGridViewCell = s.CurrentCell
            'cell.Style. = AutoCompleteMode.None
            If e.Value = 0 Then
                e.Value = String.Empty
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub MasterDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellClick
        Exit Sub
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name = "PRIORITY" Then
            Dim cell As DataGridViewComboBoxCell = s.CurrentCell
            Me.txtFINCODE.Text = cell.FormattedValue
            Me.txtTRUCKSNO.Text = cell.EditedFormattedValue
            Exit Sub
            Dim QTY1COV As String = cell.EditedFormattedValue
            If String.IsNullOrEmpty(cell.FormattedValue.ToString()) Or QTY1COV = "" Then
                'Exit Sub
            End If
            If Not cell.FormattedValue.ToString = QTY1COV Then
                Exit Sub
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If QTY1COV = "0" Then
                        ms.QTY1COV = Nothing
                    Else
                        ms.QTY1COV = QTY1COV
                    End If

                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If
    End Sub
    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name = "Ποσότ.Φορτ" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim CCCQTY1PRO As String = cell.EditedFormattedValue
            If CCCQTY1PRO = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = CCCQTY1PRO Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If CCCQTY1PRO = "0" Then
                        ms.ccCQTY1PRO = Nothing
                    Else
                        ms.ccCQTY1PRO = CCCQTY1PRO
                    End If
                    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                    If Not IsNothing(fin) Then
                        fin.UPDDATE = Now()
                        Dim cuser = s1Conn.ConnectionInfo.UserId
                        fin.UPDUSER = cuser
                    End If
                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If
        'Για να γυρίσουν εκτελεσμένα στο παρόν παραστατικό πχ 1021 Παρραγελία πρέπει στον τύπο του παρόντος παραστατικού να επιλεγεί εκρεμότητα.
        If s.Columns(e.ColumnIndex).Name = "Εκτελ" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim QTY1COV As String = cell.EditedFormattedValue
            If QTY1COV = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = QTY1COV Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If QTY1COV = "0" Then
                        ms.QTY1COV = Nothing
                    Else
                        ms.QTY1COV = QTY1COV
                    End If
                    If (ms.QTY1 - ms.QTY1COV - ms.QTY1CANC) = 0 Then
                        ms.PENDING = 0
                    Else
                        ms.PENDING = 1
                    End If
                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If
        'PENDING 0,1
        '0
        '1
        '-2 akyrvmena
        '-1 not aproved
        'QTY1 - SALDOC.FINDOC_ITELINES_QTY1COV -  SALDOC.FINDOC_ITELINES_QTY1CANC
        If s.Columns(e.ColumnIndex).Name = "Εκρεμότητα" Then '
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim pending As String = cell.EditedFormattedValue
            If pending = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = pending Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If pending = "0" Then
                        ms.QTY1COV = Nothing
                        ms.PENDING = 0
                    Else
                        ms.PENDING = pending
                    End If

                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If

        '0  Μετασχηματισμός(Όχι)
        '1  Μετασχηματισμός(Πλήρως)
        '2  Μετασχηματισμός(Μερικώς)
        '3  Μετασχηματισμένο --Όταν υπάρχει ακόμα εκρεμότητα - άρση εκρεμότητος
        If s.Columns(e.ColumnIndex).Name = "Μετασχηματισμός" Then '
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim fullytransfer As String = cell.EditedFormattedValue
            If fullytransfer = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = fullytransfer Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                If Not IsNothing(fin) Then
                    fin.FULLYTRANSF = fullytransfer
                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If

        If s.Columns(e.ColumnIndex).Name = "PRIORITY" Then
            Dim cell As DataGridViewComboBoxCell = s.CurrentCell
            Dim prior As String = cell.EditedFormattedValue
            If prior = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = prior Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If prior = "Επιλέγξτε" Then
                        ms.ccCPRIORITY = Nothing
                        cell.Style.BackColor = Nothing
                    Else
                        ms.ccCPRIORITY = cell.Items.Cast(Of PRIORITY).Where(Function(f) f.NAME = prior).FirstOrDefault.PRIORITY
                        cell.Style.BackColor = Nothing
                        If ms.ccCPRIORITY = 104 Then
                            cell.Style.BackColor = Drawing.Color.Orange
                            'row.Cells("PRIORITY").Style.ForeColor = Drawing.Color.White
                        End If
                        If ms.ccCPRIORITY = 105 Then
                            cell.Style.BackColor = Drawing.Color.Red
                            'row.Cells("PRIORITY").Style.ForeColor = Drawing.Color.White
                        End If
                    End If

                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If
        If s.Columns(e.ColumnIndex).Name = "Κατάσταση" Then '
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim finstates As String = cell.EditedFormattedValue
            If finstates = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = finstates Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                If Not IsNothing(fin) Then
                    If finstates = "0" Then
                        fin.FINSTATES = Nothing
                    Else
                        fin.FINSTATES = finstates
                    End If

                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If

        If s.Columns(e.ColumnIndex).Name = "ccCLockShipValue" Then
            Dim cell As DataGridViewCheckBoxCell = s.CurrentCell
            Dim ccCLocked As Boolean = cell.EditedFormattedValue
            If Not cell.FormattedValue = ccCLocked Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                'Dim mdoc = db.MTRDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                'If Not IsNothing(mdoc) Then
                '    mdoc.ccCLockShipValue = ccCLocked
                '    Me.BindingNavigatorSaveItem.Enabled = True
                'End If
                ''If Not IsNothing(MasterDataGridView.Columns("Συν-Κόμι €")) Then
                ''    MasterDataGridView.Columns("Συν-Κόμι €").ReadOnly = Not ccCLocked
                ''End If
            End If
        End If
        If s.Columns(e.ColumnIndex).Name = "Συν-Κόμι €" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim CCCQTY1PRO As String = cell.EditedFormattedValue
            If CCCQTY1PRO = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = CCCQTY1PRO Then
                Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                'Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                'If Not IsNothing(ms) Then
                '    If CCCQTY1PRO = "0" Then
                '        ms.ccCQTY1PRO = Nothing
                '    Else
                '        ms.ccCQTY1PRO = CCCQTY1PRO
                '    End If
                '    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                '    If Not IsNothing(fin) Then
                '        fin.UPDDATE = Now()
                '        Dim cuser = s1Conn.ConnectionInfo.UserId
                '        fin.UPDUSER = cuser
                '    End If
                '    Me.BindingNavigatorSaveItem.Enabled = True
                'End If
            End If
        End If

        Dim ar() As String = {"FFINDOCS", "FINDOCS", "MTRLINESS"}
        For Each aa In ar
            If s.Columns(e.ColumnIndex).Name = aa Then '
                Dim cell As DataGridViewCell = s.CurrentCell
                Dim aaField As String = cell.EditedFormattedValue
                If aaField = "" Then
                    Exit Sub
                End If
                If Not cell.FormattedValue.ToString = aaField Then
                    Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
                    If aa = "FFINDOCS" Then
                        Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                        If Not IsNothing(fin) Then
                            If aaField = "0" Then
                                fin.FINDOCS = Nothing
                            Else
                                fin.FINDOCS = aaField
                            End If

                            Me.BindingNavigatorSaveItem.Enabled = True
                        End If
                    End If
                    If aa = "FINDOCS" Or aa = "MTRLINESS" Then
                        Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
                        If Not IsNothing(ms) Then
                            If aaField = "0" Then
                                ms.GetType().GetProperty(aa).SetValue(ms, Nothing, Nothing)
                            Else
                                ms.GetType().GetProperty(aa).SetValue(ms, CInt(aaField), Nothing)
                            End If

                            Me.BindingNavigatorSaveItem.Enabled = True
                        End If
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError
        Dim ar() As String = {"Ποσότ.Φορτ", "Εκτελ", "PRIORITY", "Locked", "Μετασχηματισμός", "Εκρεμότητα", "Κατάσταση", "FFINDOCS", "FINDOCS", "MTRLINESS"}
        For Each aa In ar
            If sender.Columns(e.ColumnIndex).Name = aa Then
                Exit Sub
            End If
        Next
        'If sender.Columns(e.ColumnIndex).Name = "Ποσότ.Φορτ" Then
        '    Exit Sub
        'End If
        'If sender.Columns(e.ColumnIndex).Name = "PRIORITY" Then
        '    Exit Sub
        'End If
        'If sender.Columns(e.ColumnIndex).Name = "ccCLocked" Then
        '    Exit Sub
        'End If
        'If sender.Columns(e.ColumnIndex).Name = "Κατάσταση" Then
        '    Exit Sub
        'End If
        'If sender.Columns(e.ColumnIndex).Name = "ccCLockShipValue" Then
        '    Exit Sub
        'End If
        'If sender.Columns(e.ColumnIndex).Name = "ccCTOTSHIPVALUE" Then
        '    Exit Sub
        'End If
        MessageBox.Show("DataGridView1_DataError - Error happened " _
            & e.Context.ToString() & vbCrLf & "Row,Col:" & e.RowIndex & "," & sender.Columns(e.ColumnIndex).Name)

        If (e.Context = DataGridViewDataErrorContexts.Commit) _
            Then
            MessageBox.Show("Commit error")
        End If
        If (e.Context = DataGridViewDataErrorContexts _
            .CurrentCellChange) Then
            MessageBox.Show("Cell change")
        End If
        If (e.Context = DataGridViewDataErrorContexts.Parsing) _
            Then
            MessageBox.Show("parsing error")
        End If
        If (e.Context =
            DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If

        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"

            e.ThrowException = False
        End If
    End Sub
    Private Sub MasterDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MasterDataGridView.EditingControlShowing
        'Dim s As GmDataGridView = sender
        'Dim cell As DataGridViewCell = s.CurrentCell
        ''Dim r = cell.OwningRow.Cells("")..Cells("MTRL")
        'If cell.ColumnIndex = 2 Then
        '    'Dim c As ComboBox = CType(e.Control, ComboBox)
        'End If
        If TypeOf e.Control Is TextBox Then
            Dim cc As DataGridViewTextBoxEditingControl = e.Control
            cc.AutoCompleteMode = AutoCompleteMode.None
            'If sender.Columns(e..ColumnIndex).Name = "Ποσότ.Φορτ" Then
            'With DirectCast(e.Control, TextBox)
            '    .AutoCompleteMode = AutoCompleteMode.None
            'End With
            'End If
        End If
    End Sub
    Private Sub MasterDataGridView_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MasterDataGridView.CellMouseDown
        If e.Button = MouseButtons.Right Then
            '''''Dim hti = MasterDataGridView.HitTest(e.X, e.Y)
            '''''MasterDataGridView.ClearSelection()
            '''''MasterDataGridView.Rows(e.RowIndex).Selected = True
            ''''Dim fabs As New FormUsersSet
            '''''fabs.Conn = FormMain.Conn
            ''''fabs.OrUs = MasterDataGridView.Rows(e.RowIndex).DataBoundItem

            '''''Dim sts As New List(Of CCCSTATUS)
            '''''Dim st As New CCCSTATUS
            '''''Dim count = 0
            '''''For Each cc In ("--Επιλέγξτε--,ΕΡΓΑΣΙΑ,ΑΣΘΕΝΕΙΑ,ΑΔΕΙΑ,REPO,ΑΛΛΟ").Split(",")
            '''''    st = New CCCSTATUS
            '''''    st.ID = count
            '''''    count += 1
            '''''    If cc = "ΕΡΓΑΣΙΑ" Then
            '''''        Continue For
            '''''    End If
            '''''    st.DESCR = cc
            '''''    sts.Add(st)
            '''''Next
            '''''Me.StateBindingSource.DataSource = GetState()

            ''''''
            ''''''StateDataGridViewComboBoxColumn
            ''''''
            '''''Dim ddlState = fabs.ddlState
            ''''''StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            '''''ddlState.DataSource = Me.StateBindingSource
            '''''ddlState.DisplayMember = "DESCR"
            ''''''StateDataGridViewComboBoxColumn.HeaderText = "State"
            ''''''StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            '''''ddlState.Name = "StateComboBox"
            ''''''StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            ''''''StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '''''ddlState.ValueMember = "ID"

            ''''fabs.ShowDialog()
            '''''Cmd_Select()
        End If
    End Sub
#End Region
#Region "97-Control Events"
    'Private Sub BindingNavigatorAddNewItem_Click(sender As System.Object, e As System.EventArgs) Handles ΑΠΟΓΡΑΦΗToolStripMenuItem.Click, ΕΞΑΓΩΓΕΣToolStripMenuItem.Click, ΕΙΣΑΓΩΓΕΣToolStripMenuItem.Click
    '    Cmd_Add(sender)
    'End Sub
    'Private Sub BindingNavigatorDeleteItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorDeleteItem.Click
    '    Cmd_Delete()
    'End Sub
    'Private Sub BindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs)
    '    Me.Validate()
    '    Me.MasterBindingSource.EndEdit()
    'End Sub
    Private Sub cmdSelect_Click(sender As System.Object, e As System.EventArgs) Handles cmdSelect.Click
        Cmd_Select()
    End Sub
    Private Sub OpenToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripButton.Click
        Cmd_Edit()
    End Sub
    'Private Sub txtBoxLName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBoxLName.TextChanged
    '    Dim s As TextBox = sender
    '    Dim rowFound As Cross1.Member = (From g As Cross1.Member In Me.MasterBindingSource Where g.Name.ToString.ToUpper Like s.Text.Trim.ToUpper & "*").FirstOrDefault()
    '    If Not IsNothing(rowFound) Then
    '        'Dim itemFound As Integer = Me.MasterBindingSource.Find("Name", row3.Name.ToString)
    '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
    '        Me.MasterBindingSource.Position = itemFound
    '    End If
    'End Sub
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        Try
            Dim q = Me.MasterBindingSource.List.OfType(Of ccCVShipment).ToList().Where(Function(f) f.FINDOC = 0).FirstOrDefault
            If IsNothing(q) Then
                Me.MasterBindingSource.AddNew()
                Dim nu As ccCVShipment = Me.MasterBindingSource.Current
                'nu.user_type = "User"
                'nu.createdOn = Now()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
            DateTimePicker1.Value = CTODate
        Else
            DateTimePicker1.Value = "01/01/" & Year(CTODate)
        End If
    End Sub


    Private Sub TlSBtn_Click(sender As Object, e As EventArgs) Handles TlSBtnSERIES.Click, TlSBtnPRSN.Click, TlSBtnWHOUSE.Click, TlSBtnFPRMS.Click, TlSBtnTRDR.Click, TlSBtnMTRL.Click
        Dim ee As New System.ComponentModel.CancelEventArgs
        ee.Cancel = False
        TlSTextBox_Validating(sender, ee)
    End Sub
    Private Sub TlSTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TlSTxtSERIES.Validating, TlSTxtPRSN.Validating
        Dim s As Object = sender
        'e.Cancel = False
        Dim Valid As Boolean = False
        Dim SelectSender As String = ""
        Select Case sender.GetType.Name
            Case "TextBox"
                SelectSender = s.Name
            Case "ToolStripButton", "ToolStripTextBox"
                SelectSender = s.Name
            Case "MyDataGridView", "GmDgView"
                SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
        End Select
        Dim ReturnFields As New ArrayList
        Dim errorMsg As String = ""
        Dim View As Boolean = False
        Dim GmTitle As String = ""
        'Dim GmTableName As String = ""
        Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
        Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
        Dim GmCheck As Boolean = False
        Dim ValidField As String = ""
        Dim sender_TAG As String = ""
        Dim Visible As Boolean = False
        Dim GmPelPro As Byte = 0
        RsWhere = "1=1"
        RsOrder = ""
        Select Case SelectSender
            Case "TlSBtnSERIES", "TlSTxtSERIES"
                TlSTxtSERIES.Tag = "SERIES"
                ReturnFields.Add(TlSTxtSERIES)
                GmTitle = "Σειρές"
                RsTables = "SERIES"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "SERIES"
                'SELECT A.COMPANY,A.SOSOURCE,A.SOREDIR,A.SERIES,A.CODE,A.FPRMS,A.NAME,A.ISACTIVE,A.BRANCH,A.WHOUSE FROM SERIES A WHERE A.COMPANY=1000 AND A.COMPANY IN (1000) AND A.SOSOURCE=1351 AND A.ISACTIVE=1 ORDER BY A.SERIES,A.COMPANY,A.SOSOURCE
                sSQL = "SELECT SERIES,CODE,NAME FROM SERIES "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("SERIES,CODE,NAME").Split(",")
                myArrN = ("Σειρά,Σύντμηση,Περιγραφή").Split(",")
                GmCheck = True

            Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                TlSTxtFPRMS.Tag = "FPRMS"
                ReturnFields.Add(TlSTxtFPRMS)
                GmTitle = "Τύποι"
                RsTables = "FPRMS"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1"
                RsOrder = "FPRMS"
                sSQL = "SELECT FPRMS, NAME FROM FPRMS "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("FPRMS,NAME").Split(",")
                myArrN = ("Τύπος,Περιγραφή").Split(",")
                GmCheck = True

            Case "TlSBtnTRDR", "TlSTxtTRDR"
                TlSTxtTRDR.Tag = "CODE"
                ReturnFields.Add(TlSTxtTRDR)
                GmTitle = "Ευρετήριο Πελατών"
                RsTables = "TRDR"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=13 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM TRDR "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Επωνυμία").Split(",")

            Case "TlSBtnMTRL", "TlSTxtMTRL"
                TlSTxtMTRL.Tag = "CODE"
                ReturnFields.Add(TlSTxtMTRL)
                GmTitle = "Ευρετήριο Ειδών"
                RsTables = "MTRL"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=51 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM MTRL "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Περιγραφή").Split(",")

            Case "TlSBtnPRSN", "TlSTxtPRSN"
                TlSTxtPRSN.Tag = "CODE"
                ReturnFields.Add(TlSTxtPRSN)
                GmTitle = "Ευρετήριο Πωλητών"
                RsTables = "PRSN"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=20 AND TPRSN=0" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE,PRSN"
                'SELECT A.COMPANY,A.SODTYPE,A.PRSN,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TPRSN,A.AFM,A.IDENTITYNUM FROM PRSN A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TPRSN=0 ORDER BY A.CODE,A.PRSN
                sSQL = "SELECT CODE,NAME,NAME2 FROM PRSN "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME2,NAME").Split(",")
                myArrN = ("Κωδικός,Επώνυμο,Όνομα").Split(",")
                GmCheck = True

            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                TlSTxtWHOUSE.Tag = "WHOUSE"
                'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
                ReturnFields.Add(TlSTxtWHOUSE)
                'ReturnFields.Add(TlSTxtTimKin_Descr)
                ''''''''''''''''''''''''''''''''''''''''
                GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
                RsTables = "WHOUSE"
                '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
                '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
                Company = CompanyT

                RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsWhere = Trim(RsWhere)
                RsOrder = "SHORTCUT"
                'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
                sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
                'GmPelPro = 3 'Δεν υπάρχη PelPro Field
                'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
                myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
                GmCheck = True

        End Select
        Dim m_dtGen As DataTable = Nothing
        Try
            If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
                ValidField = Trim(ReturnFields(0).Text)
                If Not ValidField.IndexOf("*").Equals(-1) Then
                    ValidField = ValidField.Replace("*", "%")
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
                ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
                    ValidField = "'" & ValidField.Replace(",", "','") & "'"
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
                Else
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
                End If
                'Visible = True
            Else 'ReturnFields(0).Text = ""
                If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
                    '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
                    For i As Integer = 0 To ReturnFields.Count - 1
                        ReturnFields(i).Text = ""
                        ReturnFields(i).ToolTipText = ""
                    Next

                    'sender.Focus()
                    e.Cancel = False
                    Exit Sub
                    'Visible = True
                End If
            End If
            'RsTables = SelectPelPro(RsTables, PelPro)
            'RsWhere = SelectPelPro(RsWhere, PelPro)
            'RsOrder = SelectPelPro(RsOrder, PelPro)
            Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
            m_dtGen = GmData.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
        Catch ex As Exception
            MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
        End Try
        m_dvGen = New DataView(m_dtGen)
        If m_dvGen.Count = 0 Then
            Select Case SelectSender
                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
            End Select
            MsgBox(errorMsg)
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            'sender.Focus()
            e.Cancel = False
            Exit Sub
            View = False
        ElseIf m_dvGen.Count = 1 Then
            View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
        End If
        If View Or sender.GetType.Name = "ToolStripButton" Then
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            If Not s.GetType.Name = "ToolStripButton" Then
                ErrorProvider1.SetError(s.Control, "")
            End If

            Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
            Dim ar As New ArrayList
            Dim m_ds As New DataSet
            m_ds = GmData.GmFillDataSet(m_ds, m_dtGen, m_dtGen.TableName)

            Dim TSearchFR As New SearchFR
            TSearchFR.Conn = conn
            TSearchFR.m_ds = m_ds
            TSearchFR.Text = GmTitle
            TSearchFR.GmCheck = GmCheck
            TSearchFR.sSQL = sSQL
            TSearchFR.GmPelPro = Nothing 'GmPelPro
            TSearchFR.RsWhere = RsWhere
            TSearchFR.RsOrder = RsOrder
            TSearchFR.GmGroupSql = GmGroupSql
            TSearchFR.GmGroupSqlField = GmGroupSqlField
            TSearchFR.RsTables = RsTables
            TSearchFR.myArrF = myArrF
            TSearchFR.myArrN = myArrN
            TSearchFR.Location = Point
            'TSearchFR.Visible = View
            'TSearchFR.RetTBL = New DataTable
            'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            TSearchFR.ShowDialog()

            Dim Dt As DataTable = TSearchFR.RetTBL
            If Not TSearchFR.RetTBL Is Nothing Then

                'ReturnFields.Add(TlSTxtPCODE)
                'ReturnFields.Add(TlSTxtPELNAME1)
                If TSearchFR.GmCheck = True Then
                    Dt.DefaultView.RowFilter = "Check = True "
                End If
                Dim dv = Dt.DefaultView
                If dv.Count > 0 Then
                    If dv.Count = 1 Then
                        'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
                        'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
                        m_dvGen = dv 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
                    Else
                        Dim Result As String = ""
                        For i As Integer = 0 To dv.Count - 1
                            Result += Trim(dv(i)(0)) & "," '("P1_PCODE")
                        Next
                        ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
                    End If
                    ReturnFields(0).ToolTipText = ReturnFields(0).Text
                    'GmDgLookUp_FillNew = SearceArrayList
                Else
                    '    MsgBox("Δεν υπάρχουν Εγγραφές")
                    'If Not IsNumeric(TextBox1.Text) Then
                    If Not s.GetType.Name = "ToolStripButton" Then
                        'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
                    End If
                    'Else
                    '    ' Clear the error.
                    '    ErrorProvider1.SetError(TextBox1, "")
                    'End If
                End If
            Else
                MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
            End If
        End If

        If m_dvGen.Count = 1 Then
            'Dim data_row As DataRowView
            'data_row = bindingTim1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
                Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))

                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("SERIES"), GetType(Short))
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("FPRMS"), GetType(Short))
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))

            End Select
        End If
        If ReturnFields.Count = 3 Then
            ReturnFields(ReturnFields.Count - 1).Focus()
        End If
    End Sub

    Private Sub ToolStripComboBoxIndexes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxIndexes.SelectedIndexChanged
        Dim s As ToolStripComboBox = sender
        Me.TlSTxtSERIES.Text = ""
        Me.TlSTxtFPRMS.Text = ""
        Me.TlSTxtTFPRMS.Text = ""
        Me.TlSTxtPENDING.Text = ""

        If Me.TlSComboBoxDate.Items.Count > 0 Then
            Me.TlSComboBoxDate.SelectedIndex = 1
        End If

        If s.SelectedItem = "ΕΛΛΑΓΡΟΛΙΠ" Then
            Exit Sub
        End If

        If s.SelectedItem = "Εκκρεμείς Παραγγελίες" Then
            If Me.TlSComboBoxDate.Items.Count > 0 Then
                Me.TlSComboBoxDate.SelectedIndex = 0
            End If

            'Me.TlSTxtSERIES.Text = 1021
            Me.TlSTxtTFPRMS.Text = "201"
            Me.TlSTxtPENDING.Text = 1
        End If

        If s.SelectedItem = "Εκκρεμή Δελτία φόρτωσης" Then
            Me.TlSTxtSERIES.Text = 1001
            If {"kbechrakis"}.Contains(CurUser) Then
                Me.TlSTxtSERIES.Text = ""
                Me.TlSTxtFPRMS.Text = 7041
            End If
        End If

        If s.SelectedItem = "Κατάσταση παραδόσεων" Then
            Me.TlSTxtTFPRMS.Text = "101,301"
            Me.TlSTxtFPRMS.Text = "1000,1061,1066,7001,7021,7023,7039,7040,7041,7042,7043,7045,7046,7060,7061,7062,7063,7064,7066,7067,7068,7069,7071,7072,7073,7076,7082,7083,7084,7127,7141,7143,7144"
        End If

        If s.SelectedItem = "Λίστα Παρημίν" Then
            Me.TlSTxtSERIES.Text = 8042
        End If

    End Sub
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        Dim q = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).Select(Function(f) f.Cells("findoc").Value)
        If q.Count = 0 Then
            e.Cancel = True
        End If
    End Sub
    Private Sub ToolStripMenuItemShipingAllValue_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemShipingAllValue.Click
        Dim s As ToolStripMenuItem = sender
        tlsShipingAllValue = s.Name

        Dim errorStr As String = ""

        Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                       Where ce.Cells("Check").Value = True

        ''Dim chkFindocs = chkLists.Select(Function(f) f.Cells("FINDOC").Value)

        'Dim chkFindoc As Integer = chkFindocs(0)

        'For Each chkFin In chkFindocs
        '    If Not chkFindoc = chkFin Then
        '        MsgBox("Προσοχή !!! Λάθος επιλογή παραστατικών.", MsgBoxStyle.Critical, "ToolStripMenuItemSOCARRIER_Click")
        '        Exit Sub
        '    End If
        'Next

        Dim frm As New SoCarrierFM
        frm.Text = "Μεταφορικά"
        frm.conn = conn
        frm.SenderName = s.Name 'ToolStripMenuItemTRUCKSNO ToolStripMenuItemShipingValue

        frm.ShowDialog()

        'MTRDOC.SOCARRIER
        'MTRDOC.TRUCKSNO
        If frm.FrmCancel = False Then
            Try
                For Each qf In chkLists
                    Dim chkFindoc As Integer = qf.Cells("FINDOC").Value
                    Dim linenum As Integer = qf.Cells("LINENUM").Value
                    Dim mtrl As Integer = qf.Cells("MTRL").Value

                    Dim mtrdoc = db.MTRDOCs.Where(Function(f) f.FINDOC = chkFindoc).FirstOrDefault

                    If mtrdoc.ccCLockShipValue = 0 Then
                        mtrdoc.ccCTOTSHIPVALUE = 0
                    End If

                    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = chkFindoc).FirstOrDefault
                    If Not IsNothing(fin) Then
                        fin.UPDDATE = Now()
                        Dim cuser = s1Conn.ConnectionInfo.UserId
                        fin.UPDUSER = cuser
                    End If


                    Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = chkFindoc And f.LINENUM = linenum And f.MTRL = mtrl).FirstOrDefault

                    If s.Name = "ToolStripMenuItemShipingAllValue" Then

                        If Not frm.chkBoxccCLocked.Checked Then
                            ms.ccCLocked = Nothing
                        Else
                            ms.ccCLocked = frm.chkBoxccCLocked.Checked
                        End If
                        mtrdoc.ccCLockShipValue = frm.chkBoxccCLocked.Checked

                        If frm.txtBoxccCSHIPVALUE.Text = "" Then
                            ms.ccCSHIPVALUE = Nothing
                        Else
                            ms.ccCSHIPVALUE = frm.txtBoxccCSHIPVALUE.Text
                            If mtrdoc.ccCLockShipValue = 0 Then
                                mtrdoc.ccCTOTSHIPVALUE += ms.QTY1 * ms.ccCSHIPVALUE
                            End If
                        End If


                    End If

                    'Update Προχρεώσεις

                    'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
                    Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1253 And f.FINDOCS = fin.FINDOC).FirstOrDefault

                    'TblHeader = myModule.GetTable("FINDOC")
                    'TblDetail = myModule.GetTable("MTRLINES")

                    If IsNothing(LinSupDoc) Then
                        'myModule.InsertData()
                        'TblHeader.Current("SERIES") = 8000
                        ''TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
                        'TblHeader.Current("TRNDATE") = chSalDoc.TRNDATE
                        errorStr &= "Προσοχή!!!. Δεν βρέθηκε προχρέωση για το " & fin.FINCODE & vbCrLf & "Η διαδικασία θα διακοπεί !!!"
                        Exit For
                    Else
                        Dim id As Integer = LinSupDoc.FINDOC
                        'LinSupDoc = Nothing
                        'myModule.LocateData(id)
                    End If

                    Dim soCarrier As Hglp.SOCARRIER = db.SOCARRIERs.Where(Function(f) f.SOCARRIER = mtrdoc.SOCARRIER).FirstOrDefault
                    If IsNothing(soCarrier) Then
                        errorStr &= "Προσοχή!!!. Δεν βρέθηκε Μεταφορέας για το " & fin.FINCODE & vbCrLf & "Η διαδικασία θα διακοπεί !!!" & vbCrLf
                        Exit For
                    End If

                    Dim suplCode = soCarrier.CODE
                    Dim supTrdr As Hglp.TRDR = db.TRDRs.Where(Function(f) f.SODTYPE = 12 And f.CODE = suplCode).FirstOrDefault

                    LinSupDoc.TRDR = supTrdr.TRDR
                    LinSupDoc.FINDOCS = fin.FINDOC 'chSalDoc.FINDOC

                    '    ITELINES.FIRST;
                    'first_MTRLINES = ITELINES.MTRLINES;
                    Dim tccCSHIPVALUE = fin.MTRLINEs.FirstOrDefault.ccCSHIPVALUE
                    Dim tRemarks = "======= " & fin.FINCODE + " =======(Από μαζική μεταβολή)" & vbCrLf
                    tRemarks &= "Κωδικός" & vbTab + "Ποσότ" & vbTab + "Κόμιστρο" & vbCrLf
                    For Each ITELINES In fin.MTRLINEs
                        If Not tccCSHIPVALUE = ITELINES.ccCSHIPVALUE Then
                            errorStr &= "Λάθος κόμιστρο για το " & fin.FINCODE & vbCrLf & "Η διαδικασία θα διακοπεί !!!" & vbCrLf
                            Exit For
                        End If
                        tRemarks = tRemarks + ITELINES.MTRL1.CODE & vbTab & ITELINES.QTY1 & vbTab & ITELINES.ccCSHIPVALUE & vbCrLf
                    Next
                    If Not errorStr = "" Then
                        Exit For
                    End If
                    'Παρατηρήσεις
                    LinSupDoc.REMARKS = tRemarks

                    'Ειδικές προμηθευτών  sosource=1253   

                    Dim mtrlNew = 0
                    '7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
                    '7041	Δελτίο Αποστολής	Δελτίο αποστολής
                    '7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
                    If fin.FPRMS = 7040 Or fin.FPRMS = 7046 Then
                        mtrlNew = 1818 '64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
                    End If
                    If fin.FPRMS = 7041 Then
                        mtrlNew = 1816 '64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
                    End If

                    For Each mm In LinSupDoc.MTRLINEs
                        mm.MTRL = mtrlNew
                        mm.QTY1 = 1.0
                        mm.LINEVAL = mtrdoc.ccCTOTSHIPVALUE
                        mm.FINDOCS = fin.FINDOC
                        mm.MTRLINESS = fin.MTRLINEs.FirstOrDefault.MTRLINES

                        'Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
                        '204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
                        '205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
                        '207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
                        '208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
                        '209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
                        '212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


                        'WHOUSE	NAME
                        '2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                        '3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                        '4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                        '5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                        '8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                        '13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                        '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

                        Select Case mtrdoc.WHOUSE
                            Case 2 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                                mm.COSTCNTR = 212'Κ.Δ Καβάλας
                            Case 3 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                                mm.COSTCNTR = 212'Κ.Δ Καβάλας
                            Case 4 'ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                                mm.COSTCNTR = 204'Κ.Δ Διαβατών Θεσ/κης
                            Case 5 'ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                                mm.COSTCNTR = 205'Κ.Δ Πύργου
                            Case 8 'ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                                mm.COSTCNTR = 207'Κ.Δ Ασπροπύργου
                            Case 13 'ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                                mm.COSTCNTR = 209'Κ.Δ Βαθύλακος
                            Case 17 'ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
                                mm.COSTCNTR = 208 'Κ.Δ Φυτοθρεπτική
                        End Select

                        'TblDetail.Add()
                        'TblDetail.Current("MTRL") = 1816
                        'TblDetail.Current("LINEVAL") = 128.0
                        'TblDetail.Current("VAT") = 1410

                    Next
                Next

                If errorStr = "" Then
                    BindingNavigatorSaveItem_Click(Nothing, Nothing)
                Else
                    MsgBox(errorStr, MsgBoxStyle.Critical, strAppName)
                End If

            Catch ex As Exception

            End Try
        End If
        Cmd_Select()
    End Sub
    Private Sub ToolStripMenuItemSOCARRIER_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSOCARRIER.Click, ToolStripMenuItemTRUCKSNO.Click, ToolStripMenuItemShipingValue.Click, ToolStripMenuItemADR.Click, ToolStripMenuItemConvert.Click
        Dim s As ToolStripMenuItem = sender

        Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                       Where ce.Cells("Check").Value = True

        Dim chkFindocs = chkLists.Select(Function(f) f.Cells("FINDOC").Value)

        Dim chkFindoc As Integer = chkFindocs(0)

        For Each chkFin In chkFindocs
            If Not chkFindoc = chkFin Then
                MsgBox("Προσοχή !!! Λάθος επιλογή παραστατικών.", MsgBoxStyle.Critical, "ToolStripMenuItemSOCARRIER_Click")
                Exit Sub
            End If
        Next

        Dim frm As New SoCarrierFM
        frm.Text = "Μεταφορικά"
        frm.conn = conn
        frm.SenderName = s.Name 'ToolStripMenuItemTRUCKSNO ToolStripMenuItemShipingValue



        Dim q = db.ccCVTrdBRoutings.Where(Function(f) f.FINDOC = chkFindoc).ToList
        'q = q.Where(Function(f) f.FINDOC = chkFindoc).ToList
        'q = q.Where(Function(f) f.ISACTIVE > 0).ToList
        q = q.OrderBy(Function(f) f.SOCOST).ToList
        If q.Count = 0 And s.Name = "ToolStripMenuItemConvert" Then
            MsgBox("Προσοχή !!! Λάθος δρομολόγιο.", MsgBoxStyle.Critical, "ToolStripMenuItemSOCARRIER_Click")
            Exit Sub
        End If
        frm.ccCVTrdBRoutings = q

        If Not IsNothing(chkLists) AndAlso chkLists.Count = 1 Then
            Dim chkList = chkLists.FirstOrDefault
            'Dim findoc As Integer = chkList.Cells("FINDOC").Value
            Dim linenum As Integer = chkList.Cells("LINENUM").Value
            frm.ccCVShipment = Me.MasterBindingSource.List.Cast(Of ccCVShipment).ToList.Where(Function(f) f.FINDOC = chkFindoc And f.LINENUM = linenum).FirstOrDefault
            'Dim CurFindoc As Integer = 0
            'CurFindoc = chkLists.FirstOrDefault.Cells("FINDOC").Value
        End If
        'Dim current As ccCVShipment = Me.MasterBindingSource.Current
        'frm.FindocID = CurFindoc
        'frm.ccCSOCARRIER = current.ccCSOCARRIER
        'frm.chkBoxccCADR.Checked = If(current.MTRLINESCCCADR, False)
        'frm.chkBoxccCLocked.Checked = If(current.ccCLocked, False)
        frm.ShowDialog()
        If s.Name = "ToolStripMenuItemConvert" And frm.FrmCancel = False Then
            'PENDING 0,1
            '0
            '1
            '-2 akyrvmena
            '-1 not aproved
            Try

                Dim mts = db.MTRLINEs.Where(Function(f) f.FINDOC = chkFindoc)

                For Each mt In mts
                    If mt.PENDING = 0 Then
                        Continue For
                    End If
                    Dim QTY1COV As Double = mt.QTY1
                    If If(mt.ccCQTY1PRO, 0) > 0 Then
                        QTY1COV = mt.ccCQTY1PRO
                    End If

                    mt.QTY1COV = QTY1COV

                    If (mt.QTY1 - mt.QTY1COV - mt.QTY1CANC) = 0 Then
                        mt.PENDING = 0
                    Else
                        mt.PENDING = 1
                    End If

                Next

                Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = chkFindoc).FirstOrDefault
                If Not IsNothing(fin) Then
                    Dim pending = mts.Where(Function(f) Not f.PENDING = 0).FirstOrDefault
                    If Not IsNothing(pending) Then
                        '0  Μετασχηματισμός(Όχι)
                        '1  Μετασχηματισμός(Πλήρως)
                        '2  Μετασχηματισμός(Μερικώς)
                        '3  Μετασχηματισμένο
                        fin.FULLYTRANSF = 2
                    Else
                        fin.FULLYTRANSF = 1 '1  Μετασχηματισμός(Πλήρως)
                    End If
                    fin.UPDDATE = Now()
                    Dim cuser = s1Conn.ConnectionInfo.UserId
                    fin.UPDUSER = cuser
                End If



            Catch ex As Exception

            End Try

            'QTY1 - SALDOC.FINDOC_ITELINES_QTY1COV -  SALDOC.FINDOC_ITELINES_QTY1CANC
            'If s.Columns(e.ColumnIndex).Name = "Εκρεμότητα" Then '
            '    Dim cell As DataGridViewCell = s.CurrentCell
            '    Dim pending As String = cell.EditedFormattedValue
            '    If pending = "" Then
            '        Exit Sub
            '    End If
            '    If Not cell.FormattedValue.ToString = pending Then
            '        Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
            '        Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
            '        If Not IsNothing(ms) Then
            '            If pending = "0" Then
            '                ms.QTY1COV = Nothing
            '            Else
            '                ms.PENDING = pending
            '            End If

            '            Me.BindingNavigatorSaveItem.Enabled = True
            '        End If
            '    End If
            'End If




            Exit Sub
        End If

        'MTRDOC.SOCARRIER
        'MTRDOC.TRUCKSNO
        If frm.FrmCancel = False Then
            Try


                'Select ce.Cells("FINDOC").Value, ce.Cells("LINENUM").Value, ce.Cells("MTRL").Value

                'Dim q1 = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).
                'Select(Function(f) f.Cells("MTRL").Value)

                'Dim q2 = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).
                'Select(Function(f) f.Cells("FINDOC").Value).
                'Select(Function(f) f.Cells("MTRL").Value)
                ''Dim q1 = q.GroupBy(Function(f) f)
                ''                Select(Function(f) f.Cells("LINENUM").Value).
                'db.Log = Console.Out
                Dim mtrdoc = db.MTRDOCs.Where(Function(f) f.FINDOC = chkFindoc).FirstOrDefault

                If mtrdoc.ccCLockShipValue = 0 Then
                    mtrdoc.ccCTOTSHIPVALUE = 0
                End If

                For Each qf In chkLists
                    'Dim findoc As Integer = qf.Cells("FINDOC").Value
                    Dim linenum As Integer = qf.Cells("LINENUM").Value
                    Dim mtrl As Integer = qf.Cells("MTRL").Value

                    'Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault

                    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = chkFindoc).FirstOrDefault
                    If Not IsNothing(fin) Then
                        fin.UPDDATE = Now()
                        Dim cuser = s1Conn.ConnectionInfo.UserId
                        fin.UPDUSER = cuser
                    End If

                    'PENDING 0,1
                    '0
                    '1
                    '-2 akyrvmena
                    '-1 not aproved
                    '
                    '
                    'MTRLINES.RESTMODE
                    'Ποσοτικές εκκρεμότητες             
                    'Α/ Α Κωδικός	Περιγραφή	Ενεργή	Κατηγορία
                    '1   4	Δεσμευμένα απο Υπηρεσίες	1	Δεσμευμένα
                    '2   8	Αναμενομενα Απο Παραγωγη	1	Αναμενόμενα
                    '3   9	Δεσμευμενα Απο Παραγωγή	1	Δεσμευμένα
                    '4   11	Αναμενόμενα	1	Αναμενόμενα
                    '5   21	Δεσμευμένα Παραγγελίες (Πωλ)	1	Δεσμευμένα
                    '6   22	Δεσμευμένα (Picking)	1	Δεσμευμένα
                    '7   24	Προς Τιμολόγηση (Πωλ)	1	Αδιάφορο
                    '8   25	Προς Τιμολόγηση (Αγορές)	1	Αδιάφορο
                    '9   26	Προς αποστολή (ΠΑΡΗΜΙΝ)	1	Αδιάφορο
                    '10  27	Προς επιστροφή	1	Αδιάφορο
                    '11  30	Προς επισκευή	1	Αδιάφορο

                    'Για μετ/σμό πρέπει AND (ISNULL(MTRLINES.RESTMODE, 0) <> 0) AND (MTRLINES.PENDING = 1)
                    Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = chkFindoc And f.LINENUM = linenum And f.MTRL = mtrl).FirstOrDefault

                    If s.Name = "ToolStripMenuItemSOCARRIER" Then
                        If frm.NAMEComboBox.SelectedValue = 0 Then
                            ms.ccCSOCARRIER = Nothing
                        Else
                            Dim CCCSOCARRIER As Integer = frm.NAMEComboBox.SelectedValue
                            ms.ccCSOCARRIER = CCCSOCARRIER
                        End If

                        mtrdoc.SOCARRIER = ms.ccCSOCARRIER

                        If Not frm.chkBoxccCLocked.Checked Then
                            ms.ccCLocked = Nothing
                        Else
                            ms.ccCLocked = frm.chkBoxccCLocked.Checked
                        End If
                        mtrdoc.ccCLockShipValue = frm.chkBoxccCLocked.Checked

                        ''Check MTRDOC.SOCARRIER
                        'Dim mts = db.MTRLINEs.Where(Function(f) f.FINDOC = chkFindoc)
                        'Dim soCarrier = mts.FirstOrDefault.ccCSOCARRIER
                        'For Each ITELINES In mts
                        '    If Not soCarrier = ITELINES.ccCSOCARRIER Then
                        '        MsgBox("Προσοχή !!! Διαφορετικός μεταφορέας ανά γραμμή παραστατικού", MsgBoxStyle.Critical, "ToolStripMenuItemSOCARRIER_Click")
                        '        'Exit Sub
                        '    End If
                        'Next



                        'If IsNothing(mtrdoc.SOCARRIER) Then
                        '    mtrdoc.SOCARRIER = soCarrier
                        'Else
                        '    If mtrdoc.SOCARRIER = 8888 Then 'ΜΕΤΑΦΟΡΙΚΑ ΕΛΛΑΓΡΟΛΙΠ
                        '        mtrdoc.SOCARRIER = soCarrier
                        '    End If
                        '    If Not mtrdoc.SOCARRIER = soCarrier Then
                        '        MsgBox("Προσοχή !!! Διαφορετικός μεταφορέας σε σχέση με μεταφορέα ανά γραμμή παραστατικού", MsgBoxStyle.Critical, "ToolStripMenuItemSOCARRIER_Click")
                        '        Exit Sub
                        '    End If
                        'End If
                        If frm.txtBoxccCSHIPVALUE.Text = "" Then
                            ms.ccCSHIPVALUE = Nothing
                        Else
                            ms.ccCSHIPVALUE = frm.txtBoxccCSHIPVALUE.Text
                            If mtrdoc.ccCLockShipValue = 0 Then
                                mtrdoc.ccCTOTSHIPVALUE += ms.QTY1 * ms.ccCSHIPVALUE
                            End If
                        End If
                    End If


                    If s.Name = "ToolStripMenuItemShipingValue" Then

                        If Not frm.chkBoxccCLocked.Checked Then
                            ms.ccCLocked = Nothing
                        Else
                            ms.ccCLocked = frm.chkBoxccCLocked.Checked
                        End If
                        mtrdoc.ccCLockShipValue = frm.chkBoxccCLocked.Checked

                        If frm.txtBoxccCSHIPVALUE.Text = "" Then
                            ms.ccCSHIPVALUE = Nothing
                        Else
                            ms.ccCSHIPVALUE = frm.txtBoxccCSHIPVALUE.Text
                            If mtrdoc.ccCLockShipValue = 0 Then
                                mtrdoc.ccCTOTSHIPVALUE += ms.QTY1 * ms.ccCSHIPVALUE
                            End If
                        End If


                    End If

                    If frm.txtBoxTRUCKSNO.Enabled Then
                        If frm.txtBoxTRUCKSNO.Text = "" Then
                            ms.ccCTRUCKSNO = Nothing
                        Else
                            ms.ccCTRUCKSNO = frm.txtBoxTRUCKSNO.Text
                        End If
                    End If

                    If frm.chkBoxccCADR.Enabled Then
                        If Not frm.chkBoxccCADR.Checked Then
                            ms.ccCADR = Nothing
                        Else
                            ms.ccCADR = frm.chkBoxccCADR.Checked
                        End If
                    End If

                Next

                BindingNavigatorSaveItem_Click(Nothing, Nothing)

            Catch ex As Exception

            End Try
            Cmd_Select()
        End If
    End Sub


    Private Sub PrintAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintAllToolStripMenuItem.Click

        Dim xm = s1Conn.CreateModule("GENERAL")
        If Not IsNothing(xm) Then
            'MsgBox(xm.EvalFormula("Script(1,'function RUN(){return X.DIR(''SDK'');}'), RUN"))
        End If

        Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                       Where ce.Cells("Check").Value = True

        If Not chkLists.Count = 0 Then
            Dim docs As List(Of ccCVShipment) = chkLists.Select(Of ccCVShipment)(Function(f) f.DataBoundItem).ToList
            Dim PrnForm As New fPrintForm
            PrnForm.db = db
            PrnForm.CompanyT = CompanyT
            PrnForm.docs = docs
            PrnForm.Soft1Conn = s1Conn
            If CurUser = "gmlogic" Then
                PrnForm.Soft1Conn = XSupport.Login("E:\Soft1_500_519_11226\pfic.XCO", "gmlogic", "1mgergm++", 1002, 2000, CTODate)
            End If
            PrnForm.Soft1Object = "SALDOC"
            PrnForm.Soft1ObjectID = 1351
            PrnForm.ShowDialog()
        End If


    End Sub

    Private Sub cmdPelPro4_Click(sender As Object, e As EventArgs) Handles cmdPelPro4.Click
        Try
            If MsgBox("Προσοχή !!! Επικίνδυνη αλλάγη δεδομένων.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Ok Then

                For Each qf As ccCVShipment In Me.MasterBindingSource.DataSource
                    'Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                    'Dim mtrdoc = db.MTRDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                    Dim mss = db.MTRLINEs.Where(Function(f) f.FINDOC = qf.FINDOC)

                    For Each ms1 In mss
                        ms1.ccCDELIVDATE = qf.DELIVDATE
                    Next

                Next

                BindingNavigatorSaveItem_Click(Nothing, Nothing)
            End If


        Catch ex As Exception

        End Try
        Cmd_Select()
    End Sub

    Private Sub tlsBtnAutoSoCarrier_Click(sender As Object, e As EventArgs) Handles tlsBtnAutoSoCarrier.Click
        If MsgBox("Προσοχή !!! Μαζική αλλαγή Μεταφορέων." & vbCrLf & "Θέλετε να συνεχίσετε;", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim ModulePurDoc As XModule
        ModulePurDoc = s1Conn.CreateModule("PURDOC;ΑΓΟΡΕΣ")

        Dim newID As Integer = 0
        Try
            Dim PurDocXTable As XTable
            Dim SrvLinesXTable As XTable
            Dim dtPurDoc As New DataTable
            Dim dtSrvLines As New DataTable

            'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
            'Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1253 And f.FINDOCS = chSalDoc.FINDOC).FirstOrDefault

            PurDocXTable = ModulePurDoc.GetTable("PURDOC")
            SrvLinesXTable = ModulePurDoc.GetTable("mtrLINES")

            Dim id As Integer = 495519
            ModulePurDoc.LocateData(id)

            dtPurDoc = PurDocXTable.CreateDataTable(True)
            dtSrvLines = SrvLinesXTable.CreateDataTable(True)

            Dim ff = SrvLinesXTable.Find("mtrlines", 3 + 1)

            For i As Integer = 0 To SrvLinesXTable.Count - 1
                SrvLinesXTable.Current.RecNo = i
                Dim xr = SrvLinesXTable.Current
                Dim gg = CDbl(xr("mtrlines"))

                'LinLinesXTable.Current.RecNo = i
                'Dim xrNew = LinLinesXTable.Current

                'xr("QTY1COV") = xrNew("QTY1") - xr("QTY1CANC")
                'xr.Post()
            Next

            Dim fultr As Integer = 1
            For Each ro In dtSrvLines.Rows
                If Not ro("QTY1COV") = ro("QTY1") - ro("QTY1CANC") Then
                    fultr = 3
                    Exit For
                End If
            Next

            PurDocXTable.SetAsInteger(0, "FULLYTRANSF", fultr)

            'newID = ModulePurDoc.PostData()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Throw New Exception("Προσοχή !!! Ακύρωση αλλαγών (GmLib)")
        Finally
            ModulePurDoc.Dispose()
        End Try
        Exit Sub

        Try

            Me.Cursor = Cursors.NoMove2D
            Dim q = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                    Where ce.Cells("Check").Value = True And ce.Cells("ccCLocked").Value = False
            'Select ce.Cells("FINDOC").Value, ce.Cells("LINENUM").Value, ce.Cells("MTRL").Value

            'Dim q1 = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).
            'Select(Function(f) f.Cells("MTRL").Value)

            'Dim q2 = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).
            'Select(Function(f) f.Cells("FINDOC").Value).
            'Select(Function(f) f.Cells("MTRL").Value)
            ''Dim q1 = q.GroupBy(Function(f) f)
            ''                Select(Function(f) f.Cells("LINENUM").Value).

            For Each qf In q
                Dim findoc As Integer = qf.Cells("FINDOC").Value
                Dim linenum As Integer = qf.Cells("LINENUM").Value
                Dim mtrl As Integer = qf.Cells("MTRL").Value

                'Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                'Dim mtrdoc = db.MTRDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = findoc And f.LINENUM = linenum And f.MTRL = mtrl).FirstOrDefault

                Dim rts = db.ccCVTrdBRoutings.ToList
                rts = rts.Where(Function(f) f.FINDOC = findoc).ToList
                If rts.Count = 1 Then
                    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                    If Not IsNothing(fin) Then
                        fin.UPDDATE = Now()
                        Dim cuser = s1Conn.ConnectionInfo.UserId
                        fin.UPDUSER = cuser
                    End If
                    ms.ccCSOCARRIER = rts.FirstOrDefault.SOCARRIER
                    ms.ccCSHIPVALUE = rts.FirstOrDefault.SOCOST
                    ms.ccCLocked = 1

                    'qf.Cells("Γ-Μεταφορέας").Value = rts.FirstOrDefault.SOCARRIERNAME
                    'qf.Cells("Γ-Κόμιστρο €").Value = rts.FirstOrDefault.SOCOST
                    'qf.Cells("ccCLocked").Value = 1
                End If
            Next

            BindingNavigatorSaveItem_Click(Nothing, Nothing)

        Catch ex As Exception

        End Try
        Cmd_Select()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub BtnTestConnection_Click(sender As Object, e As EventArgs) Handles BtnTestConnection.Click
        Dim gm As New GenMenu
        gm.TestConnectionToolStripMenuItem.PerformClick()
    End Sub

    Private Sub btnCalcCredits_Click(sender As Object, e As EventArgs) Handles btnCalcCredits.Click
        LoadData()
        db.Log = Console.Out
        ' ITELINES.NUM02
        'ITELINES.PRICE
        '(ITELINES.NUM02 *100)/ITELINES.PRICE
        Dim ccCVShipments As IQueryable(Of ccCVShipment) = calcCreditss.calcCredits(db, Me.MasterBindingSource.Current)
        For Each vsp In ccCVShipments
            Dim qry = From row As DataGridViewRow In Me.MasterDataGridView.Rows, theCell As DataGridViewCell In row.Cells

            Dim q = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("FINDOC").Value = vsp.FINDOC And f.Cells("MTRLINES").Value = vsp.MTRLINES And f.Cells("LINENUM").Value = vsp.LINENUM).FirstOrDefault
            q.Cells("Εκπτ.1").Value = vsp.NUM02
            q.Cells("Έκπτωση%").Value = vsp.NUM02 * 100 / vsp.PRICE
            'Where theCell.Value.ToString.ToUpper = searchText
            'Select theCell
            Dim aa = 1

        Next
    End Sub

    Private Sub btnTestS1_Click(sender As Object, e As EventArgs) Handles btnUpdateFindoc.Click
        'AddNewTRDR()
        UpdateFindoc()
    End Sub

    Private Sub btnCopyPRDDOC_Click(sender As Object, e As EventArgs) Handles btnCopyPRDDOC.Click
        CopyPRDDOC()
    End Sub

    Private Sub btnInsertFindoc_Click(sender As Object, e As EventArgs) Handles btnInsertFindoc.Click
        Dim gg = db.GetChangeSet
        InsertFindoc(gg)
    End Sub

    Private Sub InsertFindoc(changeSet As ChangeSet)

        '8ΔΑ-ΘΑ3764
        Dim chSalDoc As New Hglp.FINDOC
        Dim chMtrDoc As New Hglp.MTRDOC
        For Each Changes As Object In changeSet.Updates
            If Changes.GetType.ToString.Contains("FINDOC") Then
                chSalDoc = Changes
            End If
            If Changes.GetType.ToString.Contains("MTRDOC") Then
                chMtrDoc = Changes
            End If
        Next

        Dim drv As ccCVShipment = Me.MasterBindingSource.Current

        chSalDoc = db.FINDOCs.Where(Function(f) f.FINDOC = drv.FINDOC).FirstOrDefault
        chMtrDoc = db.MTRDOCs.Where(Function(f) f.FINDOC = chSalDoc.FINDOC).FirstOrDefault
        If chSalDoc.FINDOC = 0 Then
            Exit Sub
        End If

        'If IsNothing(salDoc) Then
        '    MsgBox("Not salDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
        '    Exit Sub
        'End If
        If chSalDoc.TRNDATE < CDate("01/08/2018") Then
            Exit Sub
        End If

        If Not (chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7041 Or chSalDoc.FPRMS = 7046) Then
            Exit Sub
        End If
        Dim mtrDoc As Hglp.MTRDOC = db.MTRDOCs.Where(Function(f) f.FINDOC = chSalDoc.FINDOC).FirstOrDefault
        If IsNothing(mtrDoc) Then
            MsgBox("Not mtrDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        If Not mtrDoc.ccCLockShipValue = 0 Then
            Exit Sub
        End If
        '2 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
        '3 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        '4 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
        '5 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '8 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '13 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
        '16 ΧΡΗΣΤΟΣ ΜΕΓΚΛΑΣ ΑΒΕΕ Σε τρίτους
        '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

        '1003="Παρημίν"
        If chSalDoc.FINSTATES = 1003 And {2, 3, 4, 5, 8, 13, 16, 17}.Contains(mtrDoc.WHOUSE) Then
            Exit Sub
        End If
        Dim soCarrier As Hglp.SOCARRIER = db.SOCARRIERs.Where(Function(f) f.SOCARRIER = mtrDoc.SOCARRIER).FirstOrDefault
        If IsNothing(soCarrier) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε Μεταφορέας. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        Dim suplCode = soCarrier.CODE
        Dim supTrdr As Hglp.TRDR = db.TRDRs.Where(Function(f) f.SODTYPE = 12 And f.CODE = suplCode).FirstOrDefault
        If IsNothing(supTrdr) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε αντίστοιχος Προμηθευτής - Μεταφορέας. " & suplCode & vbCrLf & " Η αυτόματη έκδοση προχρέωσης μεταφορέα θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If

        Dim findoc As New FINDOC

        findoc.SERIES = 8000
        'findoc.TRNDATE = chSalDoc.TRNDATE
        'findoc.FISCPRD = findoc.TRNDATE.Year
        'findoc.PERIOD = findoc.TRNDATE.Month

        Dim ser = db.SERIESNUMs.Where(Function(f) f.COMPANY = chSalDoc.COMPANY And f.SOSOURCE = 1253 And f.SERIES = 8000 And f.FISCPRD = 2019).FirstOrDefault '  -- 1000, 1253, 8000, 2019)
        findoc.FINCODE = "ΠΧΜΕΤ" & ser.SERIESNUM + 1



        findoc.COMPANY = 1000
        findoc.LOCKID = 2
        findoc.SOSOURCE = 1253
        findoc.SOREDIR = 0
        findoc.TRNDATE = chSalDoc.TRNDATE
        findoc.FISCPRD = findoc.TRNDATE.Year
        findoc.PERIOD = findoc.TRNDATE.Month
        findoc.SERIES = 8000
        findoc.SERIESNUM = ser.SERIESNUM + 1
        findoc.FPRMS = 8000
        findoc.TFPRMS = 100
        findoc.FINCODE = findoc.FINCODE
        findoc.BRANCH = 1000
        findoc.SODTYPE = 12
        findoc.SOCURRENCY = 100
        findoc.TRDRRATE = 1
        findoc.LRATE = 1
        findoc.ORIGIN = 1
        findoc.GLUPD = 0
        findoc.SXUPD = 0
        findoc.PRDCOST = 0
        findoc.ISCANCEL = 0
        findoc.ISPRINT = 0
        findoc.ISREADONLY = 0
        findoc.APPRV = 1
        findoc.FULLYTRANSF = 0
        findoc.LTYPE1 = 0
        findoc.LTYPE2 = 0
        findoc.LTYPE3 = 1
        findoc.LTYPE4 = 0
        findoc.TURNOVR = 2217.6
        findoc.TTURNOVR = 2217.6
        findoc.LTURNOVR = 2217.6
        findoc.VATAMNT = 532.22
        findoc.TVATAMNT = 532.22
        findoc.LVATAMNT = 532.22
        findoc.EXPN = 0
        findoc.TEXPN = 0
        findoc.LEXPN = 0
        findoc.DISC1PRC = 0
        findoc.DISC1VAL = 0
        findoc.TDISC1VAL = 0
        findoc.LDISC1VAL = 0
        findoc.DISC2PRC = 0
        findoc.DISC2VAL = 0
        findoc.TDISC2VAL = 0
        findoc.LDISC2VAL = 0
        findoc.NETAMNT = 2217.6
        findoc.TNETAMNT = 2217.6
        findoc.LNETAMNT = 2217.6
        findoc.SUMAMNT = 2749.82
        findoc.SUMTAMNT = 2749.82
        findoc.SUMLAMNT = 2749.82
        findoc.FXDIFFVAL = 0
        findoc.KEPYOMD = 1
        findoc.KEPYOQT = 0
        findoc.LKEPYOVAL = 0
        findoc.CHANGEVAL = 0
        findoc.ISTRIG = 0


        findoc.TRDR = supTrdr.TRDR
        findoc.FINDOCS = chSalDoc.FINDOC


        '    ITELINES.FIRST;
        'first_MTRLINES = ITELINES.MTRLINES;
        Dim tccCSHIPVALUE = chSalDoc.MTRLINEs.FirstOrDefault.ccCSHIPVALUE
        Dim tRemarks = "======= " & chSalDoc.FINCODE + " =======" & vbCrLf
        tRemarks &= "Κωδικός" & vbTab + "Ποσότ" & vbTab + "Κόμιστρο" & vbCrLf
        For Each ITELINES In chSalDoc.MTRLINEs
            If Not tccCSHIPVALUE = ITELINES.ccCSHIPVALUE Then
                MsgBox("Λάθος κόμιστρο. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, strAppName)
                'myModule.Dispose()
                Exit Sub
            End If
            tRemarks = tRemarks + ITELINES.MTRL1.CODE & vbTab & ITELINES.QTY1 & vbTab & ITELINES.ccCSHIPVALUE & vbCrLf
        Next

        'Παρατηρήσεις
        findoc.REMARKS = tRemarks

        'Ειδικές προμηθευτών  sosource=1253   

        Dim mtrlNew = 0
        '7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
        '7041	Δελτίο Αποστολής	Δελτίο αποστολής
        '7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
        If chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7046 Then
            mtrlNew = 1818 '64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
        End If
        If chSalDoc.FPRMS = 7041 Then
            mtrlNew = 1816 '64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
        End If

        Dim mts As New MTRLINE

        mts.MTRL = mtrlNew
        mts.QTY1 = 1.0
        mts.LINEVAL = mtrDoc.ccCTOTSHIPVALUE
        mts.FINDOCS = chSalDoc.FINDOC
        mts.MTRLINESS = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES



        mts.COMPANY = 1000
        mts.FINDOC = 0 ' 388274
        mts.MTRLINES = 1
        mts.LINENUM = 1
        mts.SODTYPE = 53
        mts.MTRL = mtrlNew
        mts.PENDING = 0
        mts.SOSOURCE = 1253
        mts.SOREDIR = 0
        mts.MTRTYPE = 0
        mts.SOTYPE = 0
        mts.VAT = 1410
        mts.QTY1 = 1
        mts.QTY2 = 1
        mts.QTY1COV = 0
        mts.QTY1CANC = 0
        mts.QTY1FCOV = 0
        mts.LEXPVAL = 0
        mts.NETLINEVAL = 2217.6
        mts.LNETLINEVAL = 2217.6
        mts.VATAMNT = 532.22
        mts.LVATAMNT = 532.22
        mts.EFKVAL = 0
        mts.AUTOPRDDOC = 1

        'Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
        '204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
        '205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
        '207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
        '208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
        '209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
        '212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


        'WHOUSE	NAME
        '2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
        '3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        '4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
        '5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
        '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

        Select Case mtrDoc.WHOUSE
            Case 2 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                mts.COSTCNTR = 212'Κ.Δ Καβάλας
            Case 3 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                mts.COSTCNTR = 212'Κ.Δ Καβάλας
            Case 4 'ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                mts.COSTCNTR = 204'Κ.Δ Διαβατών Θεσ/κης
            Case 5 'ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                mts.COSTCNTR = 205'Κ.Δ Πύργου
            Case 8 'ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                mts.COSTCNTR = 207'Κ.Δ Ασπροπύργου
            Case 13 'ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                mts.COSTCNTR = 209'Κ.Δ Βαθύλακος
            Case 17 'ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
                mts.COSTCNTR = 208 'Κ.Δ Φυτοθρεπτική
        End Select

        findoc.MTRLINEs.Add(mts)
        db.FINDOCs.InsertOnSubmit(findoc)
        'db.MTRLINEs.InsertOnSubmit(mts)

        DataSafe()
    End Sub

    Private Sub CopyPRDDOC()
        '        Κανένας έλεγχος
        'Προειδοποίηση
        '        Απαγόρευση
        Dim chkPRINTDOC = 0

        Select Case Me.CHKPRINTDOCComboBox.SelectedItem
            Case "Κανένας έλεγχος"
                chkPRINTDOC = 0
            Case "Προειδοποίηση"
                chkPRINTDOC = 1
            Case "Απαγόρευση"
                chkPRINTDOC = 2
        End Select

        'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
        'WHERE a.fiscprd=2019 and A.SOSOURCE=7151 AND A.FINcode='ΠΑΡΑΓ01028'
        Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.FISCPRD = 2019 And f.SOSOURCE = 7151 And f.FINCODE Like Me.txtFINCODE.Text).FirstOrDefault
        Dim LinMtrDoc = db.MTRDOCs.Where(Function(f) f.FINDOC = LinSupDoc.FINDOC).FirstOrDefault
        Dim mtrLines = db.MTRLINEs.Where(Function(f) f.FINDOC = LinSupDoc.FINDOC)
        Dim spCostAnals = db.SPCOSTANALs.Where(Function(f) f.FINDOC = LinSupDoc.FINDOC)

        Dim series = db.SERIES.Where(Function(f) f.COMPANY = LinSupDoc.COMPANY And f.SOSOURCE = LinSupDoc.SOSOURCE And f.SERIES = LinSupDoc.SERIES).FirstOrDefault ' 11040)
        'series.CHKPRINTDOC = chkPRINTDOC

        'BindingNavigatorSaveItem_Click(Nothing, Nothing)


        Dim myModule As XModule
        '"E:     \Softone\softone_local.XCO-gmlogic-1mgergm++-1000-1000-16/04/2019 2:56:10 μμ"
        s1Conn = XSupport.Login("E:\Softone\softone_local.XCO", "gmlogic", "1mgergm++",
                                       1000, 1000, CTODate)

        If s1Conn.ConnectionInfo IsNot Nothing Then
            'Login = True

            'Return s1Conn
            'MsgBox("Connected to SoftOne", MsgBoxStyle.Information, strAppName)
            'Dim formMain As New fMain
            'formMain.Show()
            'Me.Hide()
        Else
            Me.Text = strAppName
            MsgBox("Connection Error! s1Conn.ConnectionInfo Is Nothing", MsgBoxStyle.Critical, strAppName)
        End If
        myModule = s1Conn.CreateModule("PRDDOC") ';ΕΛΛΑΓΡΟΛΙΠ")

        Dim newID As Integer = 0
        Try
            Dim TblHeader As XTable
            Dim TblMTRDOC As XTable
            Dim TblDetail As XTable
            Dim TblSPCOSTANAL As XTable

            myModule.InsertData()

            TblHeader = myModule.GetTable("FINDOC")
            TblHeader.Add()
            TblMTRDOC = myModule.GetTable("MTRDOC")
            TblMTRDOC.Add()

            TblHeader.Current("SERIES") = CType(LinSupDoc.SERIES, Int32)
            'TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
            TblHeader.Current("TRNDATE") = CTODate


            TblMTRDOC.Current("MTRL") = LinMtrDoc.MTRL
            TblMTRDOC.Current("SPCS") = LinMtrDoc.SPCS
            TblMTRDOC.Current("QTY1") = LinMtrDoc.QTY1

            'TblDetail = myModule.GetTable("MTRLINES")

            'For i As Integer = 0 To TblDetail.Count - 1
            '    TblDetail.Remove(i)
            'Next

            newID = myModule.PostData()

            myModule.LocateData(newID)

            Dim mlnsNew = db.MTRLINEs.Where(Function(f) f.FINDOC = newID)

            For Each msn In mlnsNew
                Dim q = mtrLines.Where(Function(f) f.MTRTYPE = msn.MTRTYPE And f.MTRLINES = msn.MTRLINES And f.LINENUM = msn.LINENUM).FirstOrDefault
                msn.QTY1 = q.QTY1
                msn.QTY2 = q.QTY2
            Next

            BindingNavigatorSaveItem_Click(Nothing, Nothing)

            newID = myModule.PostData()




            'TblDetail.Add()

            'TblDetail.Current("MTRTYPE") = 1
            'TblDetail.Current("MTRL") = LinMtrDoc.MTRL
            'TblDetail.Current("QTY1") = LinMtrDoc.QTY1

            'TblSPCOSTANAL = myModule.GetTable("SPCOSTANAL")

            ''myModule.InsertData()

            ''ΠΡΟΣΟΧΗ! H ποσότητα παραγομένου πρέπει να είναι διαφορετική του μηδενός (0). MTRDOC.QTY1
            ''Δεν έχετε συμπληρώσει το πεδίο 'Είδος' MTRDOC.MTRL
            ''Προσοχή!!! Απαγορεύετε η καταχώρηση Παραγωγής άνευ Προδιαγραφής MTRDOC.SPCS


            ''TblMTRDOC.Current("MTRL") = LinMtrDoc.MTRL
            ''TblMTRDOC.Current("SPCS") = LinMtrDoc.SPCS
            ''TblMTRDOC.Current("QTY") = LinMtrDoc.QTY
            ''TblMTRDOC.Current("QTY1") = LinMtrDoc.QTY1
            ''TblMTRDOC.Current("QTY2") = LinMtrDoc.QTY2
            ''TblDetail.Add()

            ''TblDetail.Item(0, "MTRTYPE") = 1
            ''TblDetail.Current("MTRL") = LinMtrDoc.MTRL
            ''TblDetail.Current("QTY1") = LinMtrDoc.QTY1


            ''For Each msn In mtrLines
            ''    Dim nqty1 = mtrLines.Where(Function(f) f.MTRTYPE = msn.MTRTYPE And f.MTRLINES = msn.MTRLINES And f.LINENUM = msn.LINENUM).FirstOrDefault.QTY1
            ''    msn.QTY1 = nqty1
            ''Next

            ''For i As Integer = 0 To TblDetail.Count - 1
            'TblDetail.Sort("MTRLINES", True, True)
            'For Each msn In mtrLines
            '    'For i As Integer = 0 To TblDetail.Count - 1
            '    Dim id As Integer = TblDetail.Find("MTRLINES", msn.MTRLINES)
            '    If id = -1 Then
            '        Continue For
            '    End If
            '    Dim mty As Short = TblDetail.Item(id, "MTRTYPE")
            '    Dim mtrlin As Integer = TblDetail.Item(id, "MTRLINES")
            '    Dim lm As Integer = TblDetail.Item(id, "LINENUM")
            '    Dim nqty1 = mtrLines.Where(Function(f) f.MTRTYPE = mty And f.MTRLINES = mtrlin And f.LINENUM = lm).FirstOrDefault.QTY1
            '    TblDetail.Item(id, "qty1") = nqty1 + 10
            'Next

            ''TblDetail.Current("MTRL") = LinMtrDoc.MTRL
            ''TblDetail.Current("MTRTYPE") = 1


            'newID = myModule.PostData()

            'myModule.LocateData(newID)

            'Dim mlnsNew = db.MTRLINEs.Where(Function(f) f.FINDOC = newID)

            'For Each msn In mlnsNew
            '    Dim nqty1 = mtrLines.Where(Function(f) f.MTRTYPE = msn.MTRTYPE And f.MTRLINES = msn.MTRLINES And f.LINENUM = msn.LINENUM).FirstOrDefault.QTY1
            '    msn.QTY1 = nqty1
            'Next

            'BindingNavigatorSaveItem_Click(Nothing, Nothing)

            'TblDetail = myModule.GetTable("MTRLINES")

            'TblMTRDOC = myModule.GetTable("mtrdoc")
            'TblMTRDOC.Item(0, "qty") = CDbl(LinMtrDoc.QTY)
            'TblMTRDOC.Item(0, "qty1") = CDbl(LinMtrDoc.QTY1) + 10
            'TblMTRDOC.Item(0, "qty2") = CDbl(LinMtrDoc.QTY1) * 0.667
            'For i As Integer = 0 To TblDetail.Count - 1
            '    Dim mtrlin As Integer = TblDetail.Item(i, "mtrlines")
            '    Dim nqty1 = mtrLines.Where(Function(f) f.MTRLINES = mtrlin).FirstOrDefault.QTY1
            '    TblDetail.Item(i, "qty1") = nqty1 + 10
            'Next


            'newID = myModule.PostData()


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'ControlsVisible(True)
        myModule.Dispose()
    End Sub

    Private Sub UpdateFindoc()
        Dim meModule As XModule = Nothing
        Try
            s1Conn = XSupport.Login("C:\Softone\Revera.XCO", "gmlogic", "1mgergm++",
                                       5000, 1000, CTODate)

            meModule = s1Conn.CreateModule("LINSUPDOC;ΔΑΠΑΝΕΣ") '"SALDOC;Βασική προβολή πωλήσεων")

            Dim LinSupDocXTable As XTable
            Dim MtrDocXTable As XTable
            Dim LinLinesXTable As XTable

            LinSupDocXTable = meModule.GetTable("LINSUPDOC")
            MtrDocXTable = meModule.GetTable("MtrDoc")
            LinLinesXTable = meModule.GetTable("LINLINES")

            meModule.LocateData(154671)

            Dim dtLinSupDoc As New DataTable

            Dim dtMtrDoc As New DataTable
            Dim dtLinLines As New DataTable

            dtLinSupDoc = LinSupDocXTable.CreateDataTable(True)
            dtMtrDoc = MtrDocXTable.CreateDataTable(True)
            dtLinLines = LinLinesXTable.CreateDataTable(True)


            'Dim TblHeader As XTable
            'Dim TblDetail As XTable


            'TblHeader = meModule.GetTable("FINDOC")
            'TblDetail = meModule.GetTable("MTRLINES")

            'Dim dt As DataTable = TblDetail.CreateDataTable(True)


            Dim ids As List(Of Integer) = dtLinLines.AsEnumerable().ToList.
                Where(Function(f) f.Field(Of Nullable(Of Integer))("FINDOCS") IsNot Nothing).
                Select(Function(f) f.Field(Of Integer)("FINDOCS")).Distinct.ToList '.Distinct.ToList
            ids = Nothing
        Catch ex As Exception
        Finally
            If Not IsNothing(meModule) Then
                meModule.Dispose()
            End If
        End Try
        Exit Sub
        '        Κανένας έλεγχος
        'Προειδοποίηση
        '        Απαγόρευση
        Dim chkPRINTDOC = 0

        Select Case Me.CHKPRINTDOCComboBox.SelectedItem
            Case "Κανένας έλεγχος"
                chkPRINTDOC = 0
            Case "Προειδοποίηση"
                chkPRINTDOC = 1
            Case "Απαγόρευση"
                chkPRINTDOC = 2
        End Select

        'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
        Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1351 And f.FINCODE Like Me.txtFINCODE.Text).FirstOrDefault

        Dim series = db.SERIES.Where(Function(f) f.COMPANY = LinSupDoc.COMPANY And f.SOSOURCE = LinSupDoc.SOSOURCE And f.SERIES = LinSupDoc.SERIES).FirstOrDefault ' 11040)
        series.CHKPRINTDOC = chkPRINTDOC

        BindingNavigatorSaveItem_Click(Nothing, Nothing)


        Dim myModule As XModule
        '"E:\Softone\softone_local.XCO-gmlogic-1mgergm++-1000-1000-16/04/2019 2:56:10 μμ"
        s1Conn = XSupport.Login("E:\Softone\softone_local.XCO", "gmlogic", "1mgergm++",
                                       1000, 1000, CTODate)

        If s1Conn.ConnectionInfo IsNot Nothing Then
            'Login = True

            'Return s1Conn
            'MsgBox("Connected to SoftOne", MsgBoxStyle.Information, strAppName)
            'Dim formMain As New fMain
            'formMain.Show()
            'Me.Hide()
        Else
            Me.Text = strAppName
            MsgBox("Connection Error! s1Conn.ConnectionInfo Is Nothing", MsgBoxStyle.Critical, strAppName)
        End If
        myModule = s1Conn.CreateModule("SALDOC;Βασική προβολή πωλήσεων")

        Dim newID As Integer = 0
        Try
            Dim TblHeader As XTable
            Dim TblDetail As XTable


            TblHeader = myModule.GetTable("FINDOC")
            TblDetail = myModule.GetTable("MTRLINES")

            If IsNothing(LinSupDoc) Then
                'myModule.InsertData()
                'TblHeader.Current("SERIES") = 8000
                ''TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
                'TblHeader.Current("TRNDATE") = chSalDoc.TRNDATE
            Else
                Dim id As Integer = LinSupDoc.FINDOC
                LinSupDoc = Nothing
                myModule.LocateData(id)
            End If

            'TblHeader.Current("TRDR") = supTrdr.TRDR
            'TblHeader.Current("FINDOCS") = chSalDoc.FINDOC
            TblHeader.Current("COMMENTS") = Me.txtTRUCKSNO.Text '"test"

            ''    ITELINES.FIRST;
            ''first_MTRLINES = ITELINES.MTRLINES;
            'Dim tccCSHIPVALUE = chSalDoc.MTRLINEs.FirstOrDefault.ccCSHIPVALUE
            'Dim tRemarks = "======= " & chSalDoc.FINCODE + " =======" & vbCrLf
            'tRemarks &= "Κωδικός" & vbTab + "Ποσότ" & vbTab + "Κόμιστρο" & vbCrLf
            'For Each ITELINES In chSalDoc.MTRLINEs
            '    If Not tccCSHIPVALUE = ITELINES.ccCSHIPVALUE Then
            '        MsgBox("Λάθος κόμιστρο. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, strAppName)
            '        myModule.Dispose()
            '        Exit Sub
            '    End If
            '    tRemarks = tRemarks + ITELINES.MTRL1.CODE & vbTab & ITELINES.QTY1 & vbTab & ITELINES.ccCSHIPVALUE & vbCrLf
            'Next

            ''Παρατηρήσεις
            'TblHeader.Current("REMARKS") = tRemarks

            ''Ειδικές προμηθευτών  sosource=1253   

            'Dim mtrlNew = 0
            ''7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
            ''7041	Δελτίο Αποστολής	Δελτίο αποστολής
            ''7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
            'If chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7046 Then
            '    mtrlNew = 1818 '64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
            'End If
            'If chSalDoc.FPRMS = 7041 Then
            '    mtrlNew = 1816 '64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
            'End If


            'TblDetail.Current("MTRL") = mtrlNew
            'TblDetail.Current("QTY1") = 1.0
            'TblDetail.Current("LINEVAL") = MTRDOC.ccCTOTSHIPVALUE
            'TblDetail.Current("FINDOCS") = chSalDoc.FINDOC
            'TblDetail.Current("MTRLINESS") = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES

            ''Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
            ''204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
            ''205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
            ''207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
            ''208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
            ''209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
            ''212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


            ''WHOUSE	NAME
            ''2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
            ''3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
            ''4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
            ''5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            ''8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            ''13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
            ''17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

            'Select Case MTRDOC.WHOUSE
            '    Case 2 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
            '        TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
            '    Case 3 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
            '        TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
            '    Case 4 'ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
            '        TblDetail.Current("COSTCNTR") = 204'Κ.Δ Διαβατών Θεσ/κης
            '    Case 5 'ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '        TblDetail.Current("COSTCNTR") = 205'Κ.Δ Πύργου
            '    Case 8 'ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '        TblDetail.Current("COSTCNTR") = 207'Κ.Δ Ασπροπύργου
            '    Case 13 'ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
            '        TblDetail.Current("COSTCNTR") = 209'Κ.Δ Βαθύλακος
            '    Case 17 'ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
            '        TblDetail.Current("COSTCNTR") = 208 'Κ.Δ Φυτοθρεπτική
            'End Select

            ''TblDetail.Add()
            ''TblDetail.Current("MTRL") = 1816
            ''TblDetail.Current("LINEVAL") = 128.0
            ''TblDetail.Current("VAT") = 1410

            newID = myModule.PostData()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'ControlsVisible(True)
        myModule.Dispose()
    End Sub

    Private Sub CreateCarrierDoc(changeSet As ChangeSet)
        '8ΔΑ-ΘΑ3764
        Dim chSalDoc As New Hglp.FINDOC
        Dim chMtrDoc As New Hglp.MTRDOC
        For Each Changes As Object In changeSet.Updates
            If Changes.GetType.ToString.Contains("FINDOC") Then
                chSalDoc = Changes
            End If
            If Changes.GetType.ToString.Contains("MTRDOC") Then
                chMtrDoc = Changes
            End If
        Next

        If chSalDoc.FINDOC = 0 Then
            Exit Sub
        End If

        'If IsNothing(salDoc) Then
        '    MsgBox("Not salDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
        '    Exit Sub
        'End If
        If chSalDoc.TRNDATE < CDate("01/08/2018") Then
            Exit Sub
        End If

        If Not (chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7041 Or chSalDoc.FPRMS = 7046) Then
            Exit Sub
        End If
        Dim mtrDoc As Hglp.MTRDOC = db.MTRDOCs.Where(Function(f) f.FINDOC = chSalDoc.FINDOC).FirstOrDefault
        If IsNothing(mtrDoc) Then
            MsgBox("Not mtrDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        If Not mtrDoc.ccCLockShipValue = 0 Then
            Exit Sub
        End If
        '2 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
        '3 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        '4 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
        '5 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '8 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '13 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
        '16 ΧΡΗΣΤΟΣ ΜΕΓΚΛΑΣ ΑΒΕΕ Σε τρίτους
        '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

        '1003="Παρημίν"
        If chSalDoc.FINSTATES = 1003 And {2, 3, 4, 5, 8, 13, 16, 17}.Contains(mtrDoc.WHOUSE) Then
            Exit Sub
        End If
        Dim soCarrier As Hglp.SOCARRIER = db.SOCARRIERs.Where(Function(f) f.SOCARRIER = mtrDoc.SOCARRIER).FirstOrDefault
        If IsNothing(soCarrier) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε Μεταφορέας. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        Dim suplCode = soCarrier.CODE
        Dim supTrdr As Hglp.TRDR = db.TRDRs.Where(Function(f) f.SODTYPE = 12 And f.CODE = suplCode).FirstOrDefault
        If IsNothing(supTrdr) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε αντίστοιχος Προμηθευτής - Μεταφορέας. " & suplCode & vbCrLf & " Η αυτόματη έκδοση προχρέωσης μεταφορέα θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Dim myModule As XModule
        myModule = s1Conn.CreateModule("LINSUPDOC;Ειδικές συναλλαγές προμηθευτών New")

        Dim newID As Integer = 0
        Try
            Dim TblHeader As XTable
            Dim TblDetail As XTable

            'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
            Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1253 And f.FINDOCS = chSalDoc.FINDOC).FirstOrDefault

            TblHeader = myModule.GetTable("FINDOC")
            TblDetail = myModule.GetTable("MTRLINES")

            If IsNothing(LinSupDoc) Then
                myModule.InsertData()
                TblHeader.Current("SERIES") = 8000
                'TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
                TblHeader.Current("TRNDATE") = chSalDoc.TRNDATE
            Else
                Dim id As Integer = LinSupDoc.FINDOC
                LinSupDoc = Nothing
                myModule.LocateData(id)
            End If

            TblHeader.Current("TRDR") = supTrdr.TRDR
            TblHeader.Current("FINDOCS") = chSalDoc.FINDOC

            '    ITELINES.FIRST;
            'first_MTRLINES = ITELINES.MTRLINES;
            Dim tccCSHIPVALUE = chSalDoc.MTRLINEs.FirstOrDefault.ccCSHIPVALUE
            Dim tRemarks = "======= " & chSalDoc.FINCODE + " =======" & vbCrLf
            tRemarks &= "Κωδικός" & vbTab + "Ποσότ" & vbTab + "Κόμιστρο" & vbCrLf
            For Each ITELINES In chSalDoc.MTRLINEs
                If Not tccCSHIPVALUE = ITELINES.ccCSHIPVALUE Then
                    MsgBox("Λάθος κόμιστρο. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, strAppName)
                    myModule.Dispose()
                    Exit Sub
                End If
                tRemarks = tRemarks + ITELINES.MTRL1.CODE & vbTab & ITELINES.QTY1 & vbTab & ITELINES.ccCSHIPVALUE & vbCrLf
            Next

            'Παρατηρήσεις
            TblHeader.Current("REMARKS") = tRemarks

            'Ειδικές προμηθευτών  sosource=1253   

            Dim mtrlNew = 0
            '7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
            '7041	Δελτίο Αποστολής	Δελτίο αποστολής
            '7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
            If chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7046 Then
                mtrlNew = 1818 '64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
            End If
            If chSalDoc.FPRMS = 7041 Then
                mtrlNew = 1816 '64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
            End If

            Dim dt As DataTable = TblDetail.CreateDataTable(True)
            TblDetail.Add()

            TblDetail.Current("MTRL") = mtrlNew
            TblDetail.Current("QTY1") = 1.0
            TblDetail.Current("LINEVAL") = mtrDoc.ccCTOTSHIPVALUE
            TblDetail.Current("FINDOCS") = chSalDoc.FINDOC
            TblDetail.Current("MTRLINESS") = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES

            'Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
            '204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
            '205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
            '207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
            '208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
            '209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
            '212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


            'WHOUSE	NAME
            '2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
            '3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
            '4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
            '5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
            '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

            Select Case mtrDoc.WHOUSE
                Case 2 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
                Case 3 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                    TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
                Case 4 'ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 204'Κ.Δ Διαβατών Θεσ/κης
                Case 5 'ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 205'Κ.Δ Πύργου
                Case 8 'ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 207'Κ.Δ Ασπροπύργου
                Case 13 'ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                    TblDetail.Current("COSTCNTR") = 209'Κ.Δ Βαθύλακος
                Case 17 'ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
                    TblDetail.Current("COSTCNTR") = 208 'Κ.Δ Φυτοθρεπτική
            End Select

            'TblDetail.Add()
            'TblDetail.Current("MTRL") = 1816
            'TblDetail.Current("LINEVAL") = 128.0
            'TblDetail.Current("VAT") = 1410

            newID = myModule.PostData()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'ControlsVisible(True)
        myModule.Dispose()
        'Throw New NotImplementedException()
    End Sub
    Private Sub AddNewTRDR2()
        '8ΔΑ-ΘΑ3764
        Me.Cursor = Cursors.WaitCursor
        Dim myModule As XModule
        myModule = s1Conn.CreateModule("LINSUPDOC;Ειδικές συναλλαγές προμηθευτών New")
        Dim newID As Integer = 0
        Try
            Dim myFinDocTbl As XTable
            Dim myMtrLinesTbl As XTable
            Dim ff() As Integer = {272378}
            Dim ar() As String = myModule.Params()
            ar(30) = "FINDOC:272378"
            myModule.LocateData(ar(30))

            'myModule.InsertData()
            myFinDocTbl = myModule.GetTable("FINDOC")
            'myFinDocTbl.Current("SERIES") = 8000
            myFinDocTbl.Current("TRDR") = 1692
            myFinDocTbl.Current("TRNDATE") = Now
            myMtrLinesTbl = myModule.GetTable("MTRLINES")
            Dim gg = myMtrLinesTbl.Item(0, "LINEVAL")
            myMtrLinesTbl.Item(0, "LINEVAL") = 444.0
            myMtrLinesTbl.Current("MTRL") = 1816
            myMtrLinesTbl.Current("LINEVAL") = 145.0
            myMtrLinesTbl.Current("VAT") = 1410
            myMtrLinesTbl.Add()
            myMtrLinesTbl.Current("MTRL") = 1816
            myMtrLinesTbl.Current("LINEVAL") = 128.0
            myMtrLinesTbl.Current("VAT") = 1410

            newID = myModule.PostData()

            '    If (findocs == 0) Then {
            '    TblHeader.INSERT;
            '    TblHeader.SERIES = 8000;
            '    //TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
            '    TblHeader.TRNDATE = SALDOC.TRNDATE;
            '}
            ' ds = X.GETSQLDATASET('SELECT so.CODE FROM SOCARRIER AS so WHERE so.SOCARRIER = ' + MTRDOC.SOCARRIER, null);
            '      ds = X.GETSQLDATASET('SELECT TRDR,CODE FROM TRDR AS t WHERE SODTYPE = 12 AND CODE = ' + '\'' + ds.CODE + '\'', null);
            'If(ds.RECORDCOUNT!= 1) {
            '      X.WARNING('Προσοχή!!!. Δεν βρέθηκε αντίστοιχος Προμηθευτής - Μεταφορέας. ' + ds.CODE + '\r\n' + ' Η διαδικασία θα διακοπεί!!!');
            '      return;
            '  } else {
            '      TRDR = ds.TRDR;
            '  }

            'TblHeader.TRDR = TRDR;//1624;//SALDOC.TRDR; ΓΚΑΤΖΟΥΛΗΣ ΧΡΗΣΤΟΣ  MTRDOC.SOCARRIER SOCARRIER.CODE
            '//X.WARNING(trdr);
            'TblHeader.FINDOCS = iSALDOCID;




            '    myTable.Current("CODE") = txtTRDRCode.Text.ToString
            '        myTable.Current("NAME") = txtTRDRName.Text.ToString
            '        myTable.Current("CITY") = txtTRDRCity.Text.ToString
            '        myTable.Current("PHONE01") = txtTRDRPhone01.Text.ToString
            '        newID = myModule.PostData()

            '        MsgBox("Customer added With ID= " + newID.ToString, MsgBoxStyle.Information, strAppName)
            '        txtTRDRCode.Text = "*"
            '        txtTRDRName.Text = ""
            '        txtTRDRCity.Text = ""
            '        txtTRDRPhone01.Text = ""

            'FilldgTRDR(iActiveObjType)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'ControlsVisible(True)
        myModule.Dispose()

    End Sub
    Private Sub AddNewTRDR()
        '8ΔΑ-ΘΑ3764
        Me.Cursor = Cursors.WaitCursor
        Dim myModule As XModule
        myModule = s1Conn.CreateModule("LINSUPDOC") ';Ειδικές συναλλαγές προμηθευτών New") '"SALDOC") ')
        Dim newID As Integer = 0
        Try
            'Dim myFinDocTbl As XTable
            'Dim myMtrLinesTbl As XTable
            Dim TblHeader As XTable
            Dim TblDetail As XTable
            'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
            'Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1253 And f.FINDOCS = salDoc.FINDOC).FirstOrDefault



            Dim id As Integer? = 268839 '267484 '272378 '
            myModule.LocateData(id)

            TblHeader = myModule.GetTable("FINDOC")
            TblDetail = myModule.GetTable("MTRLINES")

            'Dim ff = TblHeader.Current("TRDR")

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
            myModule.Dispose()
        End Try
        'ControlsVisible(True)


    End Sub

    Private Sub btnLinked_Click(sender As Object, e As EventArgs) Handles btnLinked.Click
        Dim str = Me.txtFINCODE.Text.Replace("*", "")
        Dim strs = str.Split(";")
        Dim qwh As IQueryable(Of ccCVShipment) = Me.MasterBindingSource.DataSource

        'Dim ar() As String = {"FFINDOCS", "FINDOCS", "MTRLINESS"}
        'For Each aa In ar
        '    If s.Columns(e.ColumnIndex).Name = aa Then '
        '        Dim cell As DataGridViewCell = s.CurrentCell
        '        Dim aaField As String = cell.EditedFormattedValue
        '        If aaField = "" Then
        '            Exit Sub
        '        End If
        '        If Not cell.FormattedValue.ToString = aaField Then
        '            Dim item As ccCVShipment = s.Rows(e.RowIndex).DataBoundItem
        '            If aa = "FFINDOCS" Then
        '                Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
        '                If Not IsNothing(fin) Then
        '                    If aaField = "0" Then
        '                        fin.FINDOCS = Nothing
        '                    Else
        '                        fin.FINDOCS = aaField
        '                    End If

        '                    Me.BindingNavigatorSaveItem.Enabled = True
        '                End If
        '            End If
        '            If aa = "FINDOCS" Or aa = "MTRLINESS" Then
        '                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.LINENUM = item.LINENUM And f.MTRL = item.MTRL).FirstOrDefault
        '                If Not IsNothing(ms) Then
        '                    If aaField = "0" Then
        '                        ms.GetType().GetProperty(aa).SetValue(ms, Nothing, Nothing)
        '                    Else
        '                        ms.GetType().GetProperty(aa).SetValue(ms, CInt(aaField), Nothing)
        '                    End If

        '                    Me.BindingNavigatorSaveItem.Enabled = True
        '                End If
        '            End If
        '        End If
        '    End If
        'Next
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        LoadDataInit() 'For Bind Any Control
    End Sub
    ' Load the data.
    Private Sub LoadData()
        db = New DataClassesHglpDataContext(conn) 'My.Settings.GenConnectionString)
    End Sub
    Private Sub LoadDataInit()
        Try
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360

            Dim connBuilder As New SqlConnectionStringBuilder
            connBuilder.ConnectionString = db.Connection.ConnectionString
            CompanyT = 1000
            Select Case connBuilder.InitialCatalog
                Case "PFIC"
                    CompanyT = 1002
                Case "LNK"
                    CompanyT = 1001
            End Select
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            Me.MasterBindingSource.DataSource = db.ccCVShipments.Where(Function(f) f.FINDOC = 0)

        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace) '.Substring(ex.StackTrace.IndexOf(" in ")))
        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Dim gg = db.GetChangeSet
        If Me.DataSafe() Then
            If Not tlsShipingAllValue = "ToolStripMenuItemShipingAllValue" Then
                CreateCarrierDoc(gg)
            End If
        End If

        Cmd_Select()
    End Sub


    Private Sub MasterBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MasterBindingSource.ListChanged
        If e.ListChangedType = ListChangedType.ItemChanged Then
            Dim nu As ccCVShipment = MasterBindingSource.Current
            'nu.modifiedOn = Now()
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
        If e.ListChangedType = ListChangedType.ItemAdded Then
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
    End Sub

    Private Sub Label_99_Click(sender As Object, e As EventArgs) Handles Label_99.Click

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub


#End Region
End Class