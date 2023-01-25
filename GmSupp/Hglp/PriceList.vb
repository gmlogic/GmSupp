Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Transactions
Imports GmSupp
Imports GmSupp.Hglp
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class PriceList
#Region "01-Declare Variables"
    Dim df As GmData
    Dim db As New DataClassesHglpDataContext
    Dim dbPFIC As New DataClassesPFICDataContext
    Dim dbLNK As New DataClassesLNKDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    ' Declare a variable to indicate the commit scope.  
    ' Set this value to false to use cell-level commit scope.  
    Private rowScopeCommit As Boolean = True
    Dim fS1HiddenForm As New Form
    Dim conn As String
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
        Me.DateTimePickerNewFinaldate.Value = CTODate

        StartDate = CDate("01/01/" & Year(CTODate))

        Dim Pass = ""
        'MsgBox("Καλή Χρονιά !!!" & vbCrLf & "  -- 2018 --", MsgBoxStyle.Information)
        If LocalIP = "192.168.10.108" Then
            'DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
            'DateTimePicker2.Value = CDate("06/02/2018")
            'Me.txtFields_CODE.Text = "2103030139*" '"2103030071*"
        End If
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID '
        If CurUser = "gmlogic" Then
            'Me.Panel1.Visible = True
            Me.TlSTxtTRDR.Text = "3000160"
            Me.TlSTxtMTRL.Text = "2103050529,200003050529"
        End If
        'Sosource    Name	cccPriceList
        '1   Τιμοκατάλογος Νομού	4
        '2   Τιμοκατάλογος πελάτη	2
        '3   Τιμοκατάλογος έκπτωσης παραλήπτη	5
        '4   Μη Υδατοδιαλυτά αξίες έκπτωσης	3
        '5   Πιστωτική πολιτική Υδατοδιαλυτών	11
        '6   Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης	10
        '7   Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων	12
        ''
        ''ddlPriceLists
        ''
        'Me.ddlPriceLists.DataSource = Me.CccPriceListBindingSource
        'Me.ddlPriceLists.DisplayMember = "Name"
        'Me.ddlPriceLists.FormattingEnabled = True
        'Me.ddlPriceLists.Location = New System.Drawing.Point(578, 52)
        'Me.ddlPriceLists.Name = "ddlPriceLists"
        'Me.ddlPriceLists.Size = New System.Drawing.Size(346, 21)
        'Me.ddlPriceLists.TabIndex = 249
        'Me.ddlPriceLists.ValueMember = "Sosource"
        ''
        ''CccPriceListBindingSource
        ''
        'Me.CccPriceListBindingSource.DataSource = GetType(GmSupp.Hglp.cccPriceList)
        Dim emptyCccPriceList As New List(Of cccPriceList) '= Nothing
        emptyCccPriceList = {New cccPriceList With {.Name = "<Επιλέγξτε>", .Sosource = 0}}.ToList
        Dim cps = db.cccPriceLists.Where(Function(f) f.Company = 1000).OrderBy(Function(f) f.Sosource).ToList
        Dim cpss = (From Empty In emptyCccPriceList).Union(db.cccPriceLists.Where(Function(f) f.Company = 1000).OrderBy(Function(f) f.Sosource)).ToList

        Me.ddlPriceLists.DataSource = cpss
        Me.ddlPriceLists.DisplayMember = "Name"
        'Me.ddlPriceLists.FormattingEnabled = True
        'Me.ddlPriceLists.Location = New System.Drawing.Point(578, 52)
        Me.ddlPriceLists.Name = "ddlPriceLists"
        'Me.ddlPriceLists.Size = New System.Drawing.Size(346, 21)
        'Me.ddlPriceLists.TabIndex = 249
        Me.ddlPriceLists.ValueMember = "cccPriceList"
        Me.KeyPreview = True
    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
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
        e.Cancel = (Not DataSafe())
    End Sub
#End Region
#Region "04-Bas_Commands"
    Private Sub Cmd_Edit()
        Try
            'Exit Sub
            'Try
            '    Me.Cursor = Cursors.WaitCursor
            '    Dim str As String = ""
            '    'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + dgFINDOC.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
            '    Dim drv 'As CCCCheckZip = Me.MasterBindingSource.Current
            '    str = "SALDOC[AUTOLOCATE=" & drv.ZIP & "]"
            '    s1Conn.ExecS1Command(str, fS1HiddenForm)
            '    'FilldgFINDOC_gm(iActiveObjType)
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            'Finally
            '    Me.Cursor = Cursors.Default
            'End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Select()
        Try
            'If Me.ddlPriceLists.SelectedValue = 0 Then
            '    Exit Sub
            'End If
            Me.Cursor = Cursors.NoMove2D
            LoadData()
            db.Log = Console.Out
            'Μεταφορείς:
            'Δρομολόγια:
            'CheckZIP:

            Dim xt As Softone.XTable
            Dim td As DataTable

            Dim sql As String
            sql = "select * from series s where s.company=" & 1000 & " and s.sosource=" & 1351 & " and s.series=" & 1129
            sql = "select * from series s where s.company=:1 and s.sosource=:2 and s.series=:3"
            'td = S1_WSGetBrowserData(s1Conn, "SALDOC", "", "FINDOC.TRNDATE=2020-03-27&FINDOC.TRNDATE_TO=2020-03-28")
            td = S1_WSGetBrowserData(s1Conn, "SALDOC", "", "SALDOC.FINDOC=519097")
            Try
                xt = s1Conn.GetSQLDataSet(sql, {})
                xt = s1Conn.GetSQLDataSet(sql, {1000, 1351, 1129})
                If xt.Count > 0 Then
                    Dim gg = xt.Current.Item("CHKPRINTDOC")
                    td = xt.CreateDataTable(True)
                End If

            Catch ex As Exception

            End Try

            'Dim seriesObj As XModule = Nothing
            'Try
            '    's1Conn = XSupport

            '    'seriesObj = s1Conn.CreateModule("SERIES")


            '    'SeriesTable = seriesObj.GetTable("SERIES")

            '    SeriesTable = XSupport.GetSQLDataSet("", {})
            '    dt = SeriesTable.CreateDataTable(True)

            'Catch ex As Exception
            'Finally
            '    seriesObj.Dispose()
            'End Try


            Dim cccPriceList1 As Integer = Me.ddlPriceLists.SelectedValue

            Dim qwh = db.ccCVPriceListLines.Where(Function(f) f.cccPriceList = cccPriceList1)
            qwh = qwh.Where(Function(f) f.Fromdate >= DateTimePicker1.Value.Date)
            If Me.DateTimePicker2.Value.Year = CTODate.Year Then
                qwh = qwh.Where(Function(f) f.Finaldate <= DateTimePicker2.Value)
            End If

            Dim pc As cccPriceList = ddlPriceLists.SelectedItem
            If pc.Name = "Τιμοκατάλογος Νομού" Then
                'qwh = qwh.Where(Function(f) f.COUNTRY = 1000)
            End If
            If pc.Name = "Τιμοκατάλογος πελάτη" Then
                'qwh = qwh.Where(Function(f) f.COUNTRY = 1000)
            End If
            If pc.Name = "Τιμοκατάλογος έκπτωσης παραλήπτη" Then

            End If
            If pc.Name = "Μη Υδατοδιαλυτά αξίες έκπτωσης" Then
                'qwh = qwh.Where(Function(f) f.COUNTRY = 1000)
            End If
            If pc.Name = "Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης" Then

            End If
            If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών" Then

            End If
            If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων" Then

            End If

            If Not Me.TlSTxtTRDR.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtTRDR.Text.Split(",").Contains(f.trCODE))
            End If


            If Not Me.TlSTxtWHOUSE.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtWHOUSE.Text.Split(",").Contains(f.Whouse))
            End If

            If Not Me.TlsTxtDISTRICT1.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlsTxtDISTRICT1.Text.Split(",").Contains(f.District1))
            End If

            If Not Me.TlSTxtMTRL.Text = "" Then
                qwh = qwh.Where(Function(f) Me.TlSTxtMTRL.Text.Split(",").Contains(f.mCODE))
            End If

            'If Me.chkBoxCodeExp.Checked Then
            '    qwh = qwh.Where(Function(f) f.CodeExp Is Nothing)
            'End If
            '200003050529    2103050529
            '4623    2156

            'Dim nl As New List(Of cccPriceListLine)
            'For Each pl As cccPriceListLine In qwh
            '    Dim plss = db.cccPriceListLines

            '    plss = plss.Where(Function(f) f.Mtrl = 4623 And f.cccPriceList = pl.cccPriceList And f.Fromdate = pl.Fromdate And pl.Finaldate = f.Finaldate)
            '    plss = plss.Where(Function(f) f.Whouse = pl.Whouse And f.District1 = pl.District1 And f.Trdr = pl.Trdr And f.Shipment = pl.Shipment)
            '    Dim pls = plss.FirstOrDefault
            '    If IsNothing(pls) Then
            '        pls = pl
            '        pls.Mtrl = 4623
            '        nl.Add(pls)
            '    End If

            'Next




            Me.MasterBindingSource.DataSource = qwh '.Cast(Of cccPriceListLine).ToList 'nl 'qwh
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource
            MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)

        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub Cmd_Delete()
        Try
            'DataGridViewRow Row = dgv.Rows
            '.Cast<DataGridViewRow>()
            '.Where(r => r.Cells["SystemId"].Value.ToString().Equals(searchValue))
            '.First()

            Dim rows = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True)
            Dim idcps = rows.Select(Of Integer)(Function(f) f.Cells("cccPriceListLines").Value)
            Dim cps = db.cccPriceListLines.Where(Function(f) idcps.Contains(f.cccPriceListLines))
            If MsgBox("Προσοχή !!! Θα γίνει οριστική διαγραφή εγγραφών: " & cps.Count, MsgBoxStyle.OkCancel + MsgBoxStyle.Critical + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Ok Then
                db.cccPriceListLines.DeleteAllOnSubmit(Of cccPriceListLine)(cps)
                DataSafe()
            End If
        Catch ex As Exception
            MsgBox("Προσοχή !!! Η διαγραφή δεν εκτελέστηκε.", MsgBoxStyle.Critical)
        End Try
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
                Return False
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
                    db.Log = Nothing ' Console.Out
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
        Dim fi = {"Fromdate", "Finaldate", "Price", "Disc1prc"}
        If fi.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If
        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MasterDataGridView.CellClick

        'Dim drv As SOCARRIER = Me.MasterBindingSource.Current
        'Me.DetailsBindingSource.Clear()

        'MasterDataGridView_CellContentClick1(drv.SOCARRIER)
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            'Me.MasterDataGridView.AutoResizeColumns()
            Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect

            myArrF = ("cccPriceListLines,cccPriceList,Fromdate,Finaldate,Whouse,whNAME,COUNTRY,District1,diNAME,Mtrl,mCODE,mNAME,Trdr,trCODE,trNAME,Trdrbranch,trbCODE,trbNAME,Shipment,shCODE,shNAME,Price,Disc1prc,Disc1val,qty1,cccSumGroup,sgrName,trdbusiness,createdOn,createdBy,modifiedOn,modifiedBy").Split(",")
            myArrN = ("cccPriceListLines,cccPriceList,Fromdate,Finaldate,Whouse,whNAME,COUNTRY,District1,diNAME,Mtrl,mCODE,mNAME,Trdr,trCODE,trNAME,Trdrbranch,trbCODE,trbNAME,Shipment,shCODE,shNAME,Price,Disc1prc,Disc1val,qty1,cccSumGroup,sgrName,trdbusiness,createdOn,createdBy,modifiedOn,modifiedBy").Split(",")

            Dim pc As cccPriceList = ddlPriceLists.SelectedItem
            If pc.Name = "Τιμοκατάλογος Νομού" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,District1,diNAME,mCODE,mNAME,Price,Disc1prc").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,ΠερΚωδ,Περιοχή,Υλικό,Περιγραφή,Price,% έκπτωση").Split(",")
            End If
            If pc.Name = "Τιμοκατάλογος πελάτη" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,mCODE,mNAME,trCODE,trNAME,Shipment,shNAME,District1,diNAME,Price").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,Υλικό,Περιγραφή,Πελάτης,Επωνυμία,ΤρΑποΚωδ,Τρόπος Αποστολής,ΠερΚωδ,Περιοχή,Price").Split(",")
            End If
            If pc.Name = "Τιμοκατάλογος έκπτωσης παραλήπτη" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,trCODE,trNAME,trbCODE,trbNAME,Disc1val").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,Πελάτης,Επωνυμία,Παραλήπτης,Επωνυμία Παραλήπτη,Αξία Εκπτ").Split(",")
            End If
            If pc.Name = "Μη Υδατοδιαλυτά αξίες έκπτωσης" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,trCODE,trNAME,Whouse,whNAME,District1,diNAME,Shipment,shNAME,qty1,Disc1val").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,Πελάτης,Επωνυμία,Whouse,ΑπΧώρος,ΠερΚωδ,Περιοχή,ΤρΑποΚωδ,Τρόπος Αποστολής,Ποσοτ>από,Αξία Εκπτ").Split(",")
            End If
            If pc.Name = "Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,mCODE,mNAME,trCODE,trNAME,Whouse,whNAME,District1,diNAME,Shipment,shNAME,Disc1prc").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Υλικό,Περιγραφή,Πελάτης,Επωνυμία,Whouse,ΑπΧώρος,ΠερΚωδ,Περιοχή,ΤρΑποΚωδ,Τρόπος Αποστολής,% έκπτωση").Split(",")
            End If
            If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,trCODE,trNAME,mCODE,mNAME,qty1,Disc1prc").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,Πελάτης,Επωνυμία,Υλικό,Περιγραφή,Ποσοτ>από,% έκπτωση").Split(",")
            End If
            If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων" Then
                myArrF = ("cccPriceListLines,Fromdate,Finaldate,Whouse,whNAME,mCODE,mNAME,qty1,Disc1prc,cccSumGroup").Split(",")
                myArrN = ("cccPriceListLines,Από ημ/νία,Έως ημ/νία,Whouse,ΑπΧώρος,Υλικό,Περιγραφή,Ποσότητα,% έκπτωση,Ομάδα άθροισης").Split(",")
            End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            AddOutOfOfficeColumn(Me.MasterDataGridView)
            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next
            If Not IsNothing(MasterDataGridView.Columns("cccPriceListLines")) Then
                MasterDataGridView.Columns("cccPriceListLines").Visible = False
            End If


            For Each col In MasterDataGridView.Columns
                Try
                    Dim t As Type = col.ValueType
                    If Not IsNothing(t) Then
                        With col
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    .DefaultCellStyle.Format = "N2"
                                End If
                                If Not t.FullName.IndexOf("System.DateTime, ") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    If col.DataPropertyName = "TRNDATE" Then
                                        .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                                    End If

                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
                                 Then
                                .DefaultCellStyle.Format = "N2"
                            End If
                            'If col.ValueType.Name = "String" Then
                            '    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            '    '.Width = 200
                            'End If
                            'If col.ValueType.Name <> "String" Then
                            '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            'End If
                        End With
                    End If
                Catch ex As Exception
                    MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
                End Try
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
        'Dim s As DataGridView = sender
        'If s.Columns(e.ColumnIndex).Name.Equals("MTRL1_CODE") Then
        '    ' Use helper method to get the string from lookup table
        '    Dim MTRL As Integer = s.Rows(e.RowIndex).Cells("MTRL").Value
        '    Dim m As MTRL = db.MTRLs.Where(Function(f) f.MTRL = MTRL).FirstOrDefault
        '    If Not IsNothing(m) Then
        '        e.Value = m.CODE 'GetWorkplaceNameLookupValue(dataGridViewScanDetails.Rows(e.RowIndex).Cells("UserWorkplaceID").Value)
        '    End If
        'End If
    End Sub

    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
        Dim s As DataGridView = sender
        Dim fi = {"Fromdate", "Finaldate", "Price", "Disc1prc"}
        If fi.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Dim ColName = s.Columns(e.ColumnIndex).DataPropertyName
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim CellEdited As String = cell.EditedFormattedValue
            If CellEdited = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = CellEdited Then
                Dim rec As ccCVPriceListLine = s.Rows(e.RowIndex).DataBoundItem
                Dim cccPriceListLinesID As Integer = rec.cccPriceListLines
                Dim cccPL = db.cccPriceListLines.Where(Function(f) f.cccPriceListLines = cccPriceListLinesID).FirstOrDefault
                If Not IsNothing(cccPL) Then
                    Dim value = Nothing
                    Dim t As Type
                    t = cccPL.GetType().GetProperty(ColName).PropertyType
                    If Not IsNothing(t) Then
                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                value = CType(CellEdited, Double)
                            End If
                            If Not t.FullName.IndexOf("System.DateTime") Then
                                value = Convert.ToDateTime(CellEdited)
                            End If
                        End If

                        If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                            value = CType(CellEdited, Double)
                        End If
                        If t.Name = "DateTime" Then
                            value = Convert.ToDateTime(CellEdited)
                        End If
                    End If

                    cccPL.GetType().GetProperty(ColName).SetValue(cccPL, value)
                    cccPL.modifiedOn = Now()
                    Dim cuser = s1Conn.ConnectionInfo.UserId
                    cccPL.modifiedBy = cuser
                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
            End If
        End If
        If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim MTRL1_CODE As String = cell.EditedFormattedValue
            If MTRL1_CODE = String.Empty Then
                Exit Sub
            End If
            Dim ml = Nothing
            Select Case CompanyT
                Case 1002   'PFIC
                    ml = dbPFIC.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = MTRL1_CODE).ToList
                Case 1001  'LNK
                    ml = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = MTRL1_CODE).ToList
            End Select

            If ml.Count = 0 Then
                MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
                cell.Value = Nothing
                e.Cancel = True
                Exit Sub
            End If
            If ml.Count = 1 Then
                '    cell.Value = ml(0).CODE
                '    s.Rows(e.RowIndex).Cells("MTRL").Value = ml(0).MTRL
                'Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("Κωδ.Λογιστικής") 'row.Cells("DdlMTRL1_CODE")
                'dll.Items.Clear()
                'dll.Items.Add(ml.FirstOrDefault)
                'dll.Value = ml.FirstOrDefault.CODE

                'Dim ccode As String = s.Rows(e.RowIndex).Cells("Κωδικός").Value
                'Dim mm As Centro.MTRL = db.MTRLs.Where(Function(f) f.COMPANY = 3000 And f.CODE = ccode).FirstOrDefault
                'If Not IsNothing(mm) Then
                '    Dim mtrl As Integer = mm.MTRL ' db.MTRLs.Where(Function(f) f.CODE = ccode).FirstOrDefault.MTRL
                '    Dim companyT As Integer = s.Rows(e.RowIndex).Cells("Εταιρεία").Value
                '    Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.CompanyT = companyT And f.mtrl = mtrl).FirstOrDefault
                '    If Not IsNothing(mtr) Then
                '        Dim code As String = ""
                '        Select Case companyT
                '            Case 1002   'PFIC
                '                code = CType(ml, List(Of PFIC.MTRL)).FirstOrDefault.CODE
                '            Case 1001  'LNK
                '                code = CType(ml, List(Of LNK.MTRL)).FirstOrDefault.CODE
                '        End Select
                '        mtr.CodeExp = code

                '        mtr.UPDDATE = Now()
                '        Dim cuser = 99 's1Conn.ConnectionInfo.UserId
                '        mtr.UPDUSER = cuser

                '        Me.BindingNavigatorSaveItem.Enabled = True
                '    End If
                'End If


            End If
        End If

        'If s.Columns(e.ColumnIndex).Name = "Search_Code1" Then
        '    Dim cell As DataGridViewCell = s.CurrentCell
        '    Dim MTRL1_CODE As String = cell.EditedFormattedValue
        '    If MTRL1_CODE = String.Empty Then
        '        'MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
        '        'cell.Value = Nothing
        '        'e.Cancel = True
        '        Exit Sub
        '    End If
        '    If cell.FormattedValue.ToString = MTRL1_CODE Then
        '        If MTRL1_CODE.IndexOf("*") = -1 Then
        '            Exit Sub
        '        End If
        '    End If

        '    If MTRL1_CODE.IndexOf("*") = -1 Then
        '        MTRL1_CODE &= "*"
        '    End If

        '    'Search MTRL
        '    'Dim ml As List(Of MTRL) = (From m In db.MTRLs Where m.MTRCATEGORY = 7 And m.CODE Like MTRL1_CODE
        '    '                           Order By m.CODE).ToList
        '    Dim ml As List(Of PFIC.MTRL) = dbPFIC.MTRLs.OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE Like MTRL1_CODE).ToList

        '    If ml.Count = 0 Then
        '        MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
        '        cell.Value = Nothing
        '        e.Cancel = True
        '        Exit Sub
        '    End If
        '    If ml.Count = 1 Then
        '        '    cell.Value = ml(0).CODE
        '        '    s.Rows(e.RowIndex).Cells("MTRL").Value = ml(0).MTRL
        '        Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("Κωδ.Λογιστικής") 'row.Cells("DdlMTRL1_CODE")
        '        dll.Items.Clear()
        '        dll.Items.Add(ml.FirstOrDefault)
        '        dll.Value = ml.FirstOrDefault.CODE

        '        's.Rows(e.RowIndex).Cells("MTRL").Value = ml.FirstOrDefault.MTRL
        '    Else
        '        Dim emptyMTRL As PFIC.MTRL() = Nothing
        '        emptyMTRL = {New PFIC.MTRL With {.CODE = "<Επιλέγξτε>", .MTRL = 0}}
        '        Dim mln As List(Of PFIC.MTRL) = (From Empty In emptyMTRL).Union(
        '                                    (From m1 In ml Order By m1.CODE)).ToList

        '        Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("Κωδ.Λογιστικής") 'row.Cells("DdlMTRL1_CODE")
        '        dll.Items.Clear()
        '        If Not IsNothing(mln) Then
        '            dll.Items.AddRange(mln.ToArray) '.FirstOrDefault)
        '        End If
        '        dll.Value = 0
        '    End If

        'End If

        If s.Columns(e.ColumnIndex).Name = "Search_Code" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim CodeExp As String = cell.EditedFormattedValue
            If CodeExp = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = CodeExp Then
                Dim item = s.Rows(e.RowIndex).DataBoundItem
                Dim mtrl As Integer = item.mtrl
                'Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.mtrl = mtrl).FirstOrDefault
                'If Not IsNothing(mtr) Then
                '    If CodeExp = "<Επιλέγξτε>" Then
                '        mtr.CodeExp = Nothing
                '    Else
                '        mtr.CodeExp = CodeExp
                '    End If

                '    mtr.UPDDATE = Now()
                '    Dim cuser = 99 's1Conn.ConnectionInfo.UserId
                '    mtr.UPDUSER = cuser

                '    Me.BindingNavigatorSaveItem.Enabled = True
                'End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError
        Dim fi = {"Fromdate", "Finaldate", "Price", "Disc1prc"}
        If fi.Contains(sender.Columns(e.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

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
#Region "97- Control Events"
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
    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        'save
        Me.DataSafe()
        Cmd_Select()
    End Sub

    Private Sub BindingNavigatorMasterDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMasterDeleteItem.Click
        Cmd_Delete()
    End Sub

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
    Private Sub BindingNavigatorMasterAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMasterAddNewItem.Click
        Try
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim str As String = ""
                'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + me.MasterDataGridView.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
                Dim drv = Me.MasterBindingSource.Current
                str = "ccCRouting[AUTOLOCATE=" & drv.ccCRouting & "]"
                'str = "SALDOC[AUTOEXEC=2, FORCEVALUES=INT02:" & drv.FINDOC & "?SERIES:1001]"
                'XSupport.InitInterop(fS1HiddenForm.Handle)
                s1Conn.ExecS1Command(str, fS1HiddenForm)
                'Fillme.MasterDataGridView_gm(iActiveObjType)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TlSBtn_Click(sender As Object, e As EventArgs) Handles TlSBtnWHOUSE.Click, TlSBtnTRDR.Click, TlSBtnMTRL.Click, TlSBtnDISTRICT1.Click
        Dim ee As New System.ComponentModel.CancelEventArgs
        ee.Cancel = False
        TlSTextBox_Validating(sender, ee)
    End Sub
    Private Sub TlSTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
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
            'Case "TlSBtnSERIES", "TlSTxtSERIES"
            '    TlSTxtSERIES.Tag = "SERIES"
            '    ReturnFields.Add(TlSTxtSERIES)
            '    GmTitle = "Σειρές"
            '    RsTables = "SERIES"

            '    Company = 1000

            '    RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
            '    RsOrder = "SERIES"
            '    'SELECT A.COMPANY,A.SOSOURCE,A.SOREDIR,A.SERIES,A.CODE,A.FPRMS,A.NAME,A.ISACTIVE,A.BRANCH,A.WHOUSE FROM SERIES A WHERE A.COMPANY=1000 AND A.COMPANY IN (1000) AND A.SOSOURCE=1351 AND A.ISACTIVE=1 ORDER BY A.SERIES,A.COMPANY,A.SOSOURCE
            '    sSQL = "SELECT SERIES,CODE,NAME FROM SERIES "
            '    sender_TAG = ReturnFields(0).Tag
            '    myArrF = ("SERIES,CODE,NAME").Split(",")
            '    myArrN = ("Σειρά,Σύντμηση,Περιγραφή").Split(",")
            '    GmCheck = True

            'Case "TlSBtnFPRMS", "TlSTxtFPRMS"
            '    TlSTxtFPRMS.Tag = "FPRMS"
            '    ReturnFields.Add(TlSTxtFPRMS)
            '    GmTitle = "Τύποι"
            '    RsTables = "FPRMS"

            '    Company = 1000

            '    RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1"
            '    RsOrder = "FPRMS"
            '    sSQL = "SELECT FPRMS, NAME FROM FPRMS "
            '    sender_TAG = ReturnFields(0).Tag
            '    myArrF = ("FPRMS,NAME").Split(",")
            '    myArrN = ("Τύπος,Περιγραφή").Split(",")
            '    GmCheck = True

            Case "TlSBtnTRDR", "TlSTxtTRDR"
                TlSTxtTRDR.Tag = "CODE"
                ReturnFields.Add(TlSTxtTRDR)
                GmTitle = "Ευρετήριο Πελατών"
                RsTables = "TRDR"

                Company = 1000

                Dim sodTyp = 0
                'If Me.Tag = "Πελατών" Then
                sodTyp = 13
                'End If
                'If Me.Tag = "Προμηθευτών" Or Me.Tag = "Διαχείρηση πληρωμών Μεταφορέων" Then
                '    sodTyp = 12
                'End If
                RsWhere = "Company = " & Company & " AND SODTYPE=" & sodTyp & " AND ISACTIVE=1" & IIf(sodTyp = 12, " AND JOBTYPE = 1054", "") 'TPRMS IN (2001, 2002, 5011, 9051)"

                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM TRDR "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Επωνυμία").Split(",")
                GmCheck = True
            Case "TlSBtnMTRL", "TlSTxtMTRL"
                TlSTxtMTRL.Tag = "CODE"
                ReturnFields.Add(TlSTxtMTRL)
                GmTitle = "Ευρετήριο Ειδών"
                RsTables = "MTRL"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SODTYPE=51 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM MTRL "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Περιγραφή").Split(",")
                GmCheck = True
                'Case "TlSBtnPRSN", "TlSTxtPRSN"
                '    TlSTxtPRSN.Tag = "CODE"
                '    ReturnFields.Add(TlSTxtPRSN)
                '    GmTitle = "Ευρετήριο Πωλητών"
                '    RsTables = "PRSN"

                '    Company = 1000

                '    RsWhere = "Company = " & Company & " AND SODTYPE=20 AND TPRSN=0" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                '    RsOrder = "CODE,PRSN"
                '    'SELECT A.COMPANY,A.SODTYPE,A.PRSN,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TPRSN,A.AFM,A.IDENTITYNUM FROM PRSN A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TPRSN=0 ORDER BY A.CODE,A.PRSN
                '    sSQL = "SELECT CODE,NAME,NAME2 FROM PRSN "
                '    sender_TAG = ReturnFields(0).Tag
                '    myArrF = ("CODE,NAME2,NAME").Split(",")
                '    myArrN = ("Κωδικός,Επώνυμο,Όνομα").Split(",")
                '    GmCheck = True
            Case "TlSBtnDISTRICT1", "TlSTxtDISTRICT1"
                TlsTxtDISTRICT1.Tag = "CODE"
                ReturnFields.Add(TlsTxtDISTRICT1)
                GmTitle = "Ευρετήριο Νομών"
                RsTables = "DISTRICT"

                Company = 1000

                RsWhere = "COUNTRY=1000"
                RsOrder = "CODE"
                'SELECT A.COMPANY,A.SODTYPE,A.DISTRICT1,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TDISTRICT1,A.AFM,A.IDENTITYNUM FROM DISTRICT1 A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TDISTRICT1=0 ORDER BY A.CODE,A.DISTRICT1
                sSQL = "SELECT CODE,NAME FROM DISTRICT "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Όνομα").Split(",")
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
                Company = 1000

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
                Case "TlSBtnDISTRICT1", "TlSTxtDISTRICT1"
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
                Case "TlSBtnDISTRICT1", "TlSTxtDISTRICT1"
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

    'Private Sub TlSBtnWHOUSE_Click(sender As Object, e As EventArgs) Handles TlSBtnWHOUSE.Click
    '    Dim ee As New System.ComponentModel.CancelEventArgs
    '    ee.Cancel = False
    '    ToolStripTextBox_Validating(sender, ee)
    'End Sub
    Private Sub ToolStripTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TlSTxtWHOUSE.Validating
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
                Company = 1000

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
                View = True
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

            'df = New GmData(sysDB, conn) 'My.Settings.GenConnectionString)
            'm_dtGen = df.GmFillTable(mSql, RsTables)
            m_dtGen = GmData.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
            'Dim dtb As New DataTable
            'Using cnn As New SqlConnection(conn)
            '    cnn.Open()
            '    Using cmd As New SqlCommand(mSql, cnn)
            '        'cmd.Parameters.AddWithValue("@COMPANY", 1000)
            '        'cmd.Parameters.AddWithValue("@SODTYPE", 51) '51 Αποθήκη
            '        'cmd.Parameters.AddWithValue("@DFROM", CDate("01/07/2017")) 'make sure you assign a value To startdate
            '        'cmd.Parameters.AddWithValue("@DTO", CDate("01/08/2017")) 'make sure you assign a value To 

            '        ''cmd.Parameters.AddWithValue("@MTRL", Nothing) ' AS INTEGER = 2115 --63 --NULL --384 --NULL

            '        'cmd.Parameters.AddWithValue("@CODE", "2103030557") ''--'%305%'
            '        'cmd.Parameters.AddWithValue("@WHOUSE", Me.TlSTxtWHOUSE.Text.Replace(",", "|")) '"2|4")
            '        ''--DECLARE @MTRLS  AS VARCHAR(250) = ''


            '        'cmd.Parameters.AddWithValue("@FISCPRD", 2017)
            '        'cmd.Parameters.AddWithValue("@PERIOD", 7)

            '        Try
            '            Using dr As SqlDataReader = cmd.ExecuteReader()
            '                'Dim tb = New DataTable()
            '                dtb.Load(dr)
            '                'Return tb
            '            End Using
            '        Catch ex As Exception
            '            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            '        End Try

            '    End Using



            '    'Using dad As New SqlDataAdapter(str, cnn)
            '    '    dad.Fill(dtb)
            '    'End Using
            '    cnn.Close()
            'End Using
            'm_dtGen = dtb
            'm_dtGen.TableName = "WHOUSE"
            If m_dtGen.Rows.Count = 0 And sender.GetType.Name = "ToolStripButton" Then ' If  "ToolStripButton" SearchFR = Όλα
                'RsWhere = "1=1"
                'If GmPelPro = 0 Or GmPelPro = 1 Then
                '    RsWhere = Trim(RsWhere) & " and PELPRO = " & PelPro
                'End If
                'mSql = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
                'm_dtGen = df.GmFillTable(mSql, RsTables)
            End If
        Catch ex As Exception
            MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
        End Try
        m_dvGen = New DataView(m_dtGen)
        If m_dvGen.Count = 0 Then
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSBtnPOUDRA", "TlSTxtPROM", "TlSTxtPOUDRA"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnTimKin", "TlSTxtTimKin"
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
            'If Visible Then
            Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
            Dim ar As New ArrayList
            Dim m_ds As New DataSet
            m_ds = GmData.GmFillDataSet(m_ds, m_dtGen, m_dtGen.TableName)
            'ar.Add(m_ds)
            'ar.Add(GmTitle)
            'ar.Add(GmCheck)
            'ar.Add(sSQL)
            'ar.Add(GmPelPro)
            'ar.Add(RsWhere)
            'ar.Add(RsOrder)
            'ar.Add(GmGroupSql)
            'ar.Add(GmGroupSqlField)
            'ar.Add(RsTables)
            'ar.Add(myArrF)
            'ar.Add(myArrN)
            'ar.Add(Point)
            'ar.Add(Visible)
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
            TSearchFR.GmCheck = True
            'TSearchFR.Visible = View
            'TSearchFR.RetTBL = New DataTable
            'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            TSearchFR.ShowDialog()
            'Dim SearceArrayList As ArrayList = TSearchFR.m_ArrayList 'SearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
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
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))
            End Select
            Dim data_row As DataRowView
            'data_row = BindingSource1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    data_row.Item("ETERIA") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer)) 'CInt(Me.GmDgLookUp1.GmReturnFields(2).Text)
                'Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                '    If data_row.Item("ETERIA") Is DBNull.Value Then
                '        MsgBox("Προσοχή !!! Δεν Βρέθηκε Προμηθευτής", MsgBoxStyle.Critical, "GmError")
                '        For i As Integer = 0 To ReturnFields.Count - 1
                '            ReturnFields(i).Text = ""
                '            ReturnFields(i).ToolTipText = ""
                '        Next
                '        Exit Sub
                '    End If
                '    data_row.Item("COLOR") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer))
                '    data_row.Item("VAL") = GmNull(m_dvGen(0)("CCCPRICE"), GetType(Double))
                '    STOCKTextBox.Text = GmNull(m_dvGen(0)("CCCSSTOCK"), GetType(Double))
                '    VALTextBox.Text = data_row.Item("VAL")
                '    RsWhere = "Company = " & Company
                '    RsWhere = RsWhere & " AND COLOR = " & data_row.Item("COLOR")
                '    RsWhere = RsWhere & " AND ETERIA = " & data_row.Item("ETERIA")
                '    sSQL = "SELECT TQTY " &
                '           "FROM VTOTCCCPOUDRES AS VT " &
                '           "WHERE " & RsWhere
                '    Dim SumTable As DataTable = df.GmFillTable(sSQL, "VT")
                '    'Dim m_dvSum As DataView = New DataView(SumTable)
                '    If SumTable.Rows.Count = 1 Then
                '        TYPOLQTY = SumTable.Rows(0)("TQTY")
                '    Else
                '        TYPOLQTY = 0
                '    End If
                '    Me.txtCTQTY.Text = Format(TYPOLQTY, "#,###") 'Format(data_row("TQTY") - data_row("QTY"), "#,###.#0")
                Case "TlSBtnTimKin", "TlSTxtTimKin"
                    data_row.Item("KK") = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
            End Select
        End If
        If ReturnFields.Count = 3 Then
            ReturnFields(ReturnFields.Count - 1).Focus()
        End If
    End Sub

    Private Sub ddlPriceLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPriceLists.SelectedIndexChanged
        Dim s As ComboBox = sender
        Dim pc As cccPriceList = s.SelectedItem
        If pc.Name = "<Επιλέγξτε>" Then
            Me.tlsMtrl.Visible = False
            Me.tlsDISTRICT1.Visible = False
            Me.tlsWhouse.Visible = False
            Me.tlsTrdr.Visible = False
            If Not CurUser = "gmlogic" Then
                Me.TlSTxtMTRL.Text = ""
                Me.TlsTxtDISTRICT1.Text = ""
                Me.TlSTxtWHOUSE.Text = ""
                Me.TlSTxtTRDR.Text = ""
            End If
        End If
        If pc.Name = "Τιμοκατάλογος Νομού" Then
            Me.tlsMtrl.Visible = True
            Me.tlsDISTRICT1.Visible = True
            Me.tlsWhouse.Visible = True
            'Me.lblMtrl_ITEM_CODE.Visible = True
            'Me.lblDISTRICT1.Visible = True
            'Me.lblWhouse.Visible = True
            'Me.lblTrdr_CUSTOMER_CODE.Visible = True
        End If
        If pc.Name = "Τιμοκατάλογος πελάτη" Then
            Me.tlsMtrl.Visible = True
            Me.tlsTrdr.Visible = True
            'Me.lblMtrl_ITEM_CODE.Visible = True
            'Me.lblTrdr_CUSTOMER_CODE.Visible = True
        End If
        If pc.Name = "Τιμοκατάλογος έκπτωσης παραλήπτη" Then

        End If
        If pc.Name = "Μη Υδατοδιαλυτά αξίες έκπτωσης" Then
            Me.tlsMtrl.Visible = True
            Me.tlsTrdr.Visible = True
            Me.tlsDISTRICT1.Visible = True
        End If
        If pc.Name = "Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης" Then
            Me.tlsMtrl.Visible = True
            Me.tlsTrdr.Visible = True
            Me.tlsDISTRICT1.Visible = True
        End If
        If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών" Then
            Me.tlsMtrl.Visible = True
            Me.tlsTrdr.Visible = True
        End If
        If pc.Name = "Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων" Then
            Me.tlsMtrl.Visible = True
            Me.tlsWhouse.Visible = True
        End If
    End Sub

    Private Sub ExcelToolStripButton_Click(sender As Object, e As EventArgs) Handles ExcelToolStripButton.Click
        'Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True
        Dim filename As String = Me.ddlPriceLists.SelectedItem.Name ' Me.Text

        saveFileDialog1.FileName = filename & " " & Today().ToShortDateString.Replace("/", "-")
        If saveFileDialog1.ShowDialog() = DialogResult.OK Then

            ExportDataToExcel(saveFileDialog1.FileName, filename)

            'myStream = saveFileDialog1.OpenFile()
            'If (myStream IsNot Nothing) Then
            '    ' Code to write the stream goes here.
            '    myStream.Close()
            'End If
        End If
    End Sub
    ''' <summary>
    ''' Creates the data table.
    ''' </summary>
    ''' <returns>DataTable</returns>
    Private Shared Function CreateDataTable() As DataTable
        Dim dt As New DataTable()
        For i As Integer = 0 To 9
            dt.Columns.Add(i.ToString())
        Next

        For i As Integer = 0 To 9
            Dim dr As DataRow = dt.NewRow()
            For Each dc As DataColumn In dt.Columns
                dr(dc.ToString()) = i
            Next

            dt.Rows.Add(dr)
        Next
        Return dt
    End Function
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
                    'fillprH.BackgroundColor.SetColor(System.Drawing.Color.White)

                    fillH.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

                    'Setting Top/left,right/bottom borders.
                    Dim borderH = cellH.Style.Border
                    borderH.Bottom.Style = InlineAssignHelper(borderH.Top.Style, InlineAssignHelper(borderH.Left.Style, InlineAssignHelper(borderH.Right.Style, ExcelBorderStyle.Thin)))

                    'Setting Value in cell
                    cellH.Value = title

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
                        fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)

                        fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

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

                    'If Me.radioBtnSales.Checked Then
                    '    lst = CType(lst, DataTable).AsEnumerable
                    'End If

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
                                'If Not Me.radioBtnSales.Checked Then
                                cell.Value = ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                                'Else
                                '    cell.Value = ro(col_Name)
                                'End If

                                'Setting Value in cell
                                With cell
                                    Dim t As Type
                                    'If Not Me.radioBtnSales.Checked Then
                                    t = ro.GetType().GetProperty(col_Name).PropertyType 'col.ValueType
                                    'Else
                                    '    t = ro(col_Name).GetType
                                    'End If

                                    If Not IsNothing(t) Then
                                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                                .Value = CType(cell.Value, Double)
                                                .Style.Numberformat.Format = "#,##0.00"
                                            End If
                                            If Not t.FullName.IndexOf("System.DateTime") Then
                                                If {"INSDATE", "TRNDATE"}.Contains(col_Name) Then
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

                    'rowIndex += 1
                    'Dim cellFoot = ws.Cells(rowIndex, 1)
                    'cellFoot.Value = "Σύνολα"
                    'cellFoot.Style.Fill.PatternType = ExcelFillStyle.Solid
                    'cellFoot.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206))
                    'Dim startCol = 1
                    'Dim endCol = myArrF.Count
                    ''Dim criteria = ws.Cells(rowIndex, 2)
                    ''criteria.Value = "<0"
                    'For i As Integer = startCol + 1 To endCol ' ColNLst.Count
                    '    Dim cellsum = ws.Cells(rowIndex, i)
                    '    Dim cellsumpr = ws.Cells(rowIndex - 1, i)
                    '    If Not cellsumpr.Style.Numberformat.Format = "#,##0.00" Then
                    '        Continue For
                    '    End If

                    '    With cellsum
                    '        'Setting Sum Formula 
                    '        '.Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                    '        '=SUMIF(F2:F9;">0";F2:F9)

                    '        'Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & ws.Cells(rowIndex, 2).Address & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                    '        'Spoiler: The solution is to use "," instead of ";" when working with formulas in your code.
                    '        Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<>0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                    '        If Me.radioBtnΑggregate.Checked Then
                    '            formula = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                    '        End If
                    '        .Formula = formula '("SUMIF(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ";" & Chr(34) & "<0" & Chr(34) & ";" & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                    '        .Style.Numberformat.Format = "#,##0.00"

                    '        'Setting Background fill color to Gray
                    '        .Style.Fill.PatternType = ExcelFillStyle.Solid
                    '        .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                    '    End With
                    'Next
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

    Private Sub btnChangeFinalDate_Click(sender As Object, e As EventArgs) Handles btnChangeFinalDate.Click

        Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                       Where ce.Cells("Check").Value = True

        Dim chkPLs As List(Of Integer) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                                          Where ce.Cells("Check").Value = True
                                          Select CType(ce.Cells("cccPriceListLines").Value, Integer)).ToList
        If chkPLs.Count = 0 Then
            Exit Sub
        End If
        If MsgBox("Προσοχή !!! Μαζική αλλαγή Ημ/νιών " & chkPLs.Count & vbCrLf & "Θέλετε να συνεχίσετε;", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim cccPLs = db.cccPriceListLines.Where(Function(f) chkPLs.Contains(f.cccPriceListLines)).ToList

        'Dim chkFindocs = chkLists.Select(Function(f) f.Cells("cccPriceListLines").Value)

        'Dim chkFindoc As Integer = chkFindocs(0)

        For Each cccPL In cccPLs
            cccPL.Finaldate = CDate(Me.DateTimePickerNewFinaldate.Value.ToShortDateString) 'New Date(Me.DateTimePickerNewFinaldate.Value.Year, Me.DateTimePickerNewFinaldate.Value.Month, Me.DateTimePickerNewFinaldate.Value.Day)
            cccPL.modifiedOn = Now()
            Dim cuser = s1Conn.ConnectionInfo.UserId
            cccPL.modifiedBy = cuser
        Next
        Me.BindingNavigatorSaveItem.Enabled = True
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
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            Me.MasterBindingSource.DataSource = db.ccCVShipments.Where(Function(f) f.FINDOC = 0)

        Catch ex As Exception

        End Try
    End Sub


#End Region

End Class




