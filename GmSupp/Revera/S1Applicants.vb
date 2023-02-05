Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Transactions
Imports GmSupp
Imports GmSupp.Hglp

Public Class S1Applicants
#Region "01-Declare Variables"
    Dim df As GmData
    Dim db As New DataClassesReveraDataContext
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
    Dim CompanyS As Integer = 0

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
        'DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
        'DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        If Me.Tag = "REVERA" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Revera"
            CompanyS = 4000
        End If
        If Me.Tag = "SERTORIUS" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Revera"
            CompanyS = 5000
        End If
        If Me.Tag = "CENTROFARO" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Centro"
            CompanyS = 3000
        End If

        'conString.UserID = "sa"
        'conString.Password = "P@$$w0rd"
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID

        'Dim db = New DataClassesCentrofaroDataContext(conn)
        'Dim q = (From so In db.SOCARRIERs Select so.NAME, so.SOCARRIER).ToList
        'Dim q = db.SOCARRIERs.ToList
        'q = q.Where(Function(f) f.ISACTIVE > 0).ToList
        'q = q.OrderBy(Function(f) f.NAME).ToList
        'Dim emptySOCARRIER As SOCARRIER()
        'emptySOCARRIER = {New SOCARRIER With {.NAME = "<Επιλέγξτε>", .SOCARRIER = 0}}

        'Dim q1 = emptySOCARRIER.ToList.Union(q.ToList)



        'Dim bci = db.ccCTrdBCities.ToList
        'bci = bci.Where(Function(f) {26, 149, 574, 578, 586}.Contains(f.ccCTrdBCity) And f.ISACTIVE > 0).ToList

        'Dim emptyccCTrdBCity As ccCTrdBCity()
        'emptyccCTrdBCity = {New ccCTrdBCity With {.CITY = "<Επιλέγξτε>", .ccCTrdBCity = 0}}

        'Dim q2 = emptyccCTrdBCity.ToList.Union(bci.ToList)

        'CENTROFARO
        'Company TRDR	CODE	NAME
        '3000    12118	00006	PFIC LTD.
        '3000    25353	00011	ΛΙΠΑΣΜΑΤΑ ΝΕΑΣ ΚΑΡΒΑΛΗΣ Α.Ε.

        'REVERA
        'Company TRDR	CODE	NAME
        '4000    35465	00006	PFIC LTD.
        '4000    38236	00011	ΛΙΠΑΣΜΑΤΑ ΝΕΑΣ ΚΑΡΒΑΛΗΣ Α.Ε.

        If {"g.igglesis", "i.pilarinos"}.Contains(CurUser) Then
            CompanyT = 1002
        End If

        If CompName = "LK" Then
            If {"panagiotis", "katerina", "gkonstantatos"}.Contains(CurUser) Then
                CompanyT = 2001 '1001
            End If
        End If
        If CompName = "NVF" Then
            If {"avichou", "katerina"}.Contains(CurUser) Then
                CompanyT = 2002 '1001
            End If
        End If

        If CurUser = "gmlogic" Then
            conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
            Select Case conString.InitialCatalog
                Case "PFIC"
                    CompanyT = 1002
                Case "LNK"
                    CompanyT = 1001
                Case "LK"
                    CompanyT = 2001 '1001
                Case "NVF"
                    CompanyT = 2002 '1001
            End Select

        End If
        Me.KeyPreview = True
    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.KeyCode = Keys.F4 Then
            'Me.cmdPrint.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview Is True, And this Is from the FORM.")
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
            Me.Cursor = Cursors.NoMove2D
            LoadData()
            db.Log = Console.Out
            'Μεταφορείς:
            'Δρομολόγια:
            'CheckZIP:
            'Dim q = From cd In db.cccMultiCompDatas Join m In db.MTRLs On cd.mtrl Equals m.MTRL
            '        Where m.COMPANY = CompanyS
            '        Select cd.cccMultiCompData, cd.CompanyT, cd.mtrl, m.CODE, m.NAME, cd.CodeExp

            Dim q = db.ccCS1Applicants.Where(Function(f) f.COMPANY = Company And f.SOSOURCE = 1251)



            Dim qwh = q.Where(Function(f) f.SOSOURCE = 1251) 'And f {1351, 1253}.Contains(f.SOSOURCE) And f.ISCANCEL = 0 And f.APPRV = 1)

            'qwh = qwh.Where(Function(f) f.CompanyT = CompanyT)

            'If Not Me.TlSTxtMTRL.Text = "" Then
            '    qwh = qwh.Where(Function(f) f.CODE Like Me.TlSTxtMTRL.Text)
            'End If

            'If Me.chkBoxCodeExp.Checked Then
            '    qwh = qwh.Where(Function(f) f.CodeExp Is Nothing)
            'End If




            Me.MasterBindingSource.DataSource = qwh
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource

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
            If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
                If Not Conditions() Then
                    Exit Function
                End If
            End If
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

    Private Function Conditions() As Boolean
        Conditions = True
        Dim smsg As String = String.Empty
        'For Each tt In db.GetChangeSet.Updates
        '    smsg &= "Προσοχή !!!. Λάθος κωδικός" & vbCrLf

        'Next

        If Not smsg = String.Empty Then
            MsgBox(smsg, MsgBoxStyle.Critical)
            Return False
        End If

        'Throw New NotImplementedException
    End Function
#End Region
#Region "96-MasterDataGridView"
    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If s.Columns(s.CurrentCell.ColumnIndex).Name = "Search_Code" Then
            Exit Sub
        End If

        If s.Columns(s.CurrentCell.ColumnIndex).Name = "CodeExp" Then
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
            Me.MasterDataGridView.AutoResizeColumns()
            Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect

            myArrF = ("COMPANY,SOSOURCE,UFTBL01,CODE,NAME,ISACTIVE,ccCUser,AspNetUsersName").Split(",")
            myArrN = ("COMPANY,SOSOURCE,UFTBL01,CODE,NAME,ISACTIVE,ccCUser,AspNetUsersName").Split(",")

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)
            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next

            Exit Sub

            'Dim SumShVALDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'SumShVALDataGridViewTextBoxColumn.DataPropertyName = "SumShVAL"
            'SumShVALDataGridViewTextBoxColumn.HeaderText = "SumShVAL"
            'SumShVALDataGridViewTextBoxColumn.Name = "SumShVAL"
            'MasterDataGridView.Columns.Add(SumShVALDataGridViewTextBoxColumn)

            Dim columnComboBox As New DataGridViewComboBoxColumn()
            'columnComboBox.DataPropertyName = "CCCPRIORITY"
            Dim mtrs = Nothing

            'Dim emptyMTRL As Panel.MTRL() = Nothing
            'emptyMTRL = {New Panel.MTRL With {.CODE = "<Select a product>", .MTRL = 0}}
            'Dim mln As List(Of Panel.MTRL) = (From Empty In emptyMTRL).Union(
            '                                (From m1 In ml Order By m1.CODE)).ToList

            Dim emptyMTRL = Nothing

            If CompanyT = 1002 Then 'PFIC
                emptyMTRL = {New PFIC.MTRL With {.CODE = "<Επιλέγξτε>", .MTRL = 0}}.ToList
                Dim mm = dbPFIC.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE.Substring(0, 1) = "6").ToList
                mtrs = (From Empty In CType(emptyMTRL, List(Of PFIC.MTRL)).Union(mm)).ToList

            End If

            If {2001, 2002}.Contains(CompanyT) Then '1001 Then 'LNK
                emptyMTRL = {New LNK.MTRL With {.CODE = "<Επιλέγξτε>", .MTRL = 0}}.ToList
                'Dim mm = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE.Substring(0, 1) = "6").ToList
                Dim mm = (From m In dbLNK.MTRLs Join ex In dbLNK.MTREXTRAs On m.COMPANY Equals ex.COMPANY And m.MTRL Equals ex.MTRL
                          Where ex.BOOL04 = 1
                          Select m).ToList
                mtrs = (From Empty In CType(emptyMTRL, List(Of LNK.MTRL)).Union(mm)).ToList
            End If


            'Search_Code
            '
            columnComboBox.DataSource = mtrs
            columnComboBox.DisplayMember = "CODE"
            columnComboBox.HeaderText = "Search_Code"
            columnComboBox.Name = "Search_Code"
            columnComboBox.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            columnComboBox.SortMode = DataGridViewColumnSortMode.Automatic
            columnComboBox.ValueMember = "mtrl"
            columnComboBox.Width = 120
            columnComboBox.FlatStyle = FlatStyle.Flat
            MasterDataGridView.Columns.Add(columnComboBox)


            Dim columnTxtBox As New DataGridViewTextBoxColumn
            'columnTxtBox.DataPropertyName = "CodeExp"
            columnTxtBox.HeaderText = "Κωδ.Λογιστικής"
            columnTxtBox.Name = "CodeExp"
            columnTxtBox.SortMode = DataGridViewColumnSortMode.Automatic
            Me.MasterDataGridView.Columns.Add(columnTxtBox)

            If Not IsNothing(MasterDataGridView.Columns("Περιγραφή")) Then
                MasterDataGridView.Columns("Περιγραφή").Width = 300
            End If

            If Not IsNothing(MasterDataGridView.Columns("Search_Code")) Then
                MasterDataGridView.Columns("Search_Code").ReadOnly = False
                'If CompanyT = 1002 Then 'PFIC
                '    columnComboBox.ReadOnly = True
                'End If
            End If

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                'Dim dll As DataGridViewComboBoxCell = row.Cells("Κωδ.Λογιστικής")

                'Dim MTRL As Integer = row.Cells("MTRL").Value

                'Dim m As PFIC.MTRL = db1.MTRLs.Where(Function(f) f.SODTYPE = 53 And f.CODE = "").FirstOrDefault

                'If Not IsNothing(m) Then
                '    dll.Items.Add(m)
                '    dll.Value = MTRL
                'End If

                Dim item = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        Dim dll As DataGridViewComboBoxCell = row.Cells("Search_Code")
                        If Not IsNothing(item.CodeExp) Then
                            dll.Value = item.CodeExp
                        Else
                            dll.Value = 0
                            'Dim code As String = item.CodeExp
                            'Dim m = Nothing
                            'If CompanyT = 1002 Then 'PFIC
                            '    m = dbPFIC.MTRLs.Where(Function(f) f.COMPANY = CompanyT And f.SODTYPE = 53 And f.CODE = code).FirstOrDefault
                            'End If

                            'If CompanyT = 1001 Then 'LNK
                            '    m = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT And f.SODTYPE = 53 And f.CODE = code).FirstOrDefault
                            'End If
                            'If Not IsNothing(m) Then
                            '    'dll.Items.Add(m)
                            '    'dll.Value = m.MTRL
                            'End If
                        End If

                        Dim dlltxt As DataGridViewTextBoxCell = row.Cells("CodeExp")
                        If Not IsNothing(item.CodeExp) Then
                            dlltxt.Value = item.CodeExp
                        End If


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
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click

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
        Try
            If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
                Dim cellc As DataGridViewCell = s.CurrentCell
                Dim CodeExp As String = cellc.EditedFormattedValue
                Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_Code")
                If celln.Value = 0 AndAlso CodeExp = String.Empty Then
                    Exit Sub
                End If
                If Not cellc.FormattedValue.ToString = CodeExp Then
                    Dim ml = Nothing
                    Select Case CompanyT'and mtrl extra
                        Case 1002   'PFIC
                            ml = dbPFIC.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = CodeExp).ToList
                        Case 2001 'LK '1001  'LNK
                            'ml = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = MTRL1_CODE).ToList
                            ml = (From m In dbLNK.MTRLs Join ex In dbLNK.MTREXTRAs On m.COMPANY Equals ex.COMPANY And m.MTRL Equals ex.MTRL
                                  Where m.SODTYPE = 53 And m.CODE = CodeExp And ex.BOOL04 = 1
                                  Select m).ToList
                    End Select

                    s.Tag = ml

                    If ml.Count = 0 Then
                        Exit Sub
                    End If
                    If ml.Count = 1 Then

                        Dim ccode As String = s.Rows(e.RowIndex).Cells("Κωδικός").Value
                        Dim mm As Centro.MTRL = Nothing 'db.MTRLs.Where(Function(f) f.COMPANY = CompanyS And f.CODE = ccode).FirstOrDefault
                        If Not IsNothing(mm) Then
                            Dim mtrl As Integer = mm.MTRL ' db.MTRLs.Where(Function(f) f.CODE = ccode).FirstOrDefault.MTRL
                            Dim companyT As Integer = s.Rows(e.RowIndex).Cells("Εταιρεία").Value
                            Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.CompanyT = companyT And f.mtrl = mtrl).FirstOrDefault
                            If Not IsNothing(mtr) Then
                                Dim mlf = Nothing
                                Select Case companyT
                                    Case 1002   'PFIC
                                        mlf = CType(ml, List(Of PFIC.MTRL)).FirstOrDefault
                                    Case 2001 'LK '1001  'LNK
                                        mlf = CType(ml, List(Of LNK.MTRL)).FirstOrDefault
                                End Select
                                mtr.CodeExp = mlf.code

                                mtr.UPDDATE = Now()
                                Dim cuser = 99 's1Conn.ConnectionInfo.UserId
                                mtr.UPDUSER = cuser

                                If db.GetChangeSet.Updates.Count > 0 Then
                                    Me.BindingNavigatorSaveItem.Enabled = True
                                Else
                                    Me.BindingNavigatorSaveItem.Enabled = False
                                End If

                                Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("Search_Code")

                                Dim dllidx As Integer = 0

                                dllidx = dll.Items.IndexOf(mlf)

                                If Not dllidx = -1 Then
                                    dll.Value = mlf.mtrl
                                Else
                                    dll.Value = 0
                                End If

                            End If
                        End If

                    End If
                End If
            End If

            If s.Columns(e.ColumnIndex).Name = "Search_Code" Then
                Dim cellc As DataGridViewCell = s.CurrentCell
                Dim Search_Code As String = cellc.EditedFormattedValue
                If Search_Code = "" Then
                    Exit Sub
                End If
                If Not cellc.FormattedValue.ToString = Search_Code Then
                    Dim cellCodeExp As DataGridViewCell = s.Rows(e.RowIndex).Cells("CodeExp")
                    cellCodeExp.Value = Search_Code

                    Dim item = s.Rows(e.RowIndex).DataBoundItem
                    'Dim mtrl As Integer = item.mtrl
                    Dim cccMultiCompData As Integer = item.cccMultiCompData
                    Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.cccMultiCompData = cccMultiCompData).FirstOrDefault
                    If Not IsNothing(mtr) Then
                        If Search_Code = "<Επιλέγξτε>" Then
                            mtr.CodeExp = Nothing
                        Else
                            mtr.CodeExp = Search_Code
                        End If

                        mtr.UPDDATE = Now()
                        Dim cuser = 99 's1Conn.ConnectionInfo.UserId
                        mtr.UPDUSER = cuser

                        If db.GetChangeSet.Updates.Count > 0 Then
                            Me.BindingNavigatorSaveItem.Enabled = True
                        Else
                            Me.BindingNavigatorSaveItem.Enabled = False
                        End If

                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub MasterDataGridView_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellValidated
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
            Dim cellc As DataGridViewCell = s.CurrentCell
            Dim CodeExp As String = cellc.EditedFormattedValue
            Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_Code")
            If celln.Value = 0 AndAlso CodeExp = String.Empty Then
                Exit Sub
            End If

            Dim ml = s.Tag 'Nothing

            If Not IsNothing(ml) AndAlso ml.Count = 0 Then
                MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
                Dim item = s.Rows(cellc.RowIndex).DataBoundItem
                Dim chItem = db.GetChangeSet.Updates.Where(Function(f) f.cccMultiCompData = item.cccMultiCompData).FirstOrDefault
                If Not IsNothing(chItem) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, chItem)
                End If

                cellc.Value = Nothing
                celln.Value = 0

                If db.GetChangeSet.Updates.Count > 0 Then
                    Me.BindingNavigatorSaveItem.Enabled = True
                Else
                    Me.BindingNavigatorSaveItem.Enabled = False
                End If

            End If
        End If

        s.Tag = Nothing

    End Sub
    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError
        If sender.Columns(e.ColumnIndex).Name = "Search_Code" Then
            Exit Sub
        End If
        If sender.Columns(e.ColumnIndex).Name = "Κωδ.Λογιστικής" Then
            Exit Sub
        End If
        If sender.Columns(e.ColumnIndex).Name = "PRIORITY" Then
            Exit Sub
        End If
        If sender.Columns(e.ColumnIndex).Name = "ccCLocked" Then
            Exit Sub
        End If

        MessageBox.Show("DataGridView1_DataError - Error happened " _
            & e.Context.ToString() & vbCrLf & "Row,Col:" & e.RowIndex & "," & sender.Columns(e.ColumnIndex).Name)

        MessageBox.Show("Error happened " _
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
        db = New DataClassesReveraDataContext(conn) 'My.Settings.GenConnectionString)
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360
            If CurUser = "g.igglesis" Then
                dbPFIC.Connection.ConnectionString = My.Settings.PFICConnectionString
            End If

            If {"panagiotis", "katerina", "gkonstantatos"}.Contains(CurUser) Then
                dbPFIC.Connection.ConnectionString = My.Settings.LKConnectionString
            End If
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            'Me.MasterBindingSource.DataSource = db.CCCCheckZips.Where(Function(f) f.ZIP = 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        If Me.DataSafe() Then
            Me.cmdSelect.PerformClick()
        End If
    End Sub

    Private Sub MasterBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MasterBindingSource.ListChanged
        If e.ListChangedType = ListChangedType.ItemChanged Then
            Dim nu ' As CCCCheckZip = MasterBindingSource.Current
            'nu.modifiedOn = Now()
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
        If e.ListChangedType = ListChangedType.ItemAdded Then
            Me.BindingNavigatorSaveItem.Enabled = True
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
#End Region

End Class




