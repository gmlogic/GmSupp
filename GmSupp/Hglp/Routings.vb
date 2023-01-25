Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Transactions
Imports GmSupp.Hglp

Public Class Routings
#Region "01-Declare Variables"
    Dim df As GmData
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    ' Declare a variable to indicate the commit scope.  
    ' Set this value to false to use cell-level commit scope.  
    Private rowScopeCommit As Boolean = True
    Dim fS1HiddenForm As New Form
    Dim conn As String
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

        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID
        Dim db = New DataClassesHglpDataContext(conn)
        'Dim q = (From so In db.SOCARRIERs Select so.NAME, so.SOCARRIER).ToList
        Dim q = db.SOCARRIERs.ToList
        q = q.Where(Function(f) f.ISACTIVE > 0).ToList
        q = q.OrderBy(Function(f) f.NAME).ToList
        Dim emptySOCARRIER As SOCARRIER()
        emptySOCARRIER = {New SOCARRIER With {.NAME = "<Επιλέγξτε>", .SOCARRIER = 0}}

        Dim q1 = emptySOCARRIER.ToList.Union(q.ToList)

        Me.SONAMEComboBox.DataSource = q1.ToList
        Me.SONAMEComboBox.DisplayMember = "NAME"
        Me.SONAMEComboBox.ValueMember = "SOCARRIER"
        Me.SONAMEComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Me.SONAMEComboBox.AutoCompleteSource = AutoCompleteSource.ListItems

        Dim bci = db.ccCTrdBCities.ToList
        bci = bci.Where(Function(f) {26, 149, 574, 578, 586}.Contains(f.ccCTrdBCity) And f.ISACTIVE > 0).ToList

        Dim emptyccCTrdBCity As ccCTrdBCity()
        emptyccCTrdBCity = {New ccCTrdBCity With {.CITY = "<Επιλέγξτε>", .ccCTrdBCity = 0}}

        Dim q2 = emptyccCTrdBCity.ToList.Union(bci.ToList)

        '
        'CITYComboBoxBegin
        '
        Me.CITYComboBoxBegin.DataSource = q2.ToList
        Me.CITYComboBoxBegin.DisplayMember = "CITY"
        Me.CITYComboBoxBegin.ValueMember = "ccCTrdBCity"
        Me.CITYComboBoxBegin.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Me.CITYComboBoxBegin.AutoCompleteSource = AutoCompleteSource.ListItems
        Me.CITYComboBoxBegin.SelectedValue = 0


        Me.SplitContainer2.SplitterDistance = 800
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

            If Me.TlSComboBoxChoice.Text = "Δρομολόγια:" Then
                Dim TFormF As New RoutingsFM
                Dim drv = Me.MasterBindingSource.Current
                Dim ccCRouting As Integer = drv.ccCRouting
                TFormF.CurDrv = db.ccCRoutings.Where(Function(f) f.ccCRouting = ccCRouting).FirstOrDefault
                TFormF.P_ccCRouting = drv.ccCRouting
                Dim Position As Integer = Me.MasterBindingSource.Position
                TFormF.DgdvRefresh = False
                TFormF.Text = Me.Tag & " - " & TFormF.Name
                TFormF.ShowDialog()
                If TFormF.DgdvRefresh = True Then
                    'Me.DataGridViewMaster.Refresh()
                    Cmd_Select()
                    ' Set the Position property to the results of the Find method. 
                    Dim rowFound As ccCRouting = (From g As ccCRouting In Me.MasterBindingSource Where g.ccCRouting = TFormF.CurDrv.ccCRouting).FirstOrDefault()
                    If Not IsNothing(rowFound) Then
                        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
                        Me.MasterBindingSource.Position = itemFound
                    End If
                End If

            End If

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
            Dim qds = Nothing

            'If Me.TlSComboBoxChoice.Text = "Παραλήπτες:" Then
            '    Dim q = db.SOCARRIERs.AsQueryable
            '    Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            '    If Not Me.txtSONAME.Text = "" Then
            '        qwh = qwh.Where(Function(f) f.NAME Like Me.txtSONAME.Text)
            '    End If
            '    qwh = qwh.OrderBy(Function(f) f.NAME)
            '    qds = qwh
            'End If

            'If Me.TlSComboBoxChoice.Text = "Μεταφορείς:" Then
            '    Dim q = db.SOCARRIERs.AsQueryable
            '    Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            '    qwh = qwh.OrderBy(Function(f) f.NAME)
            '    qds = qwh
            'End If


            If Me.TlSComboBoxChoice.Text = "Δρομολόγια:" Then
                Dim q = From rt In db.ccCRoutings
                        Join bcct In db.ccCTrdBCities On rt.BeginCity Equals bcct.ccCTrdBCity
                        Join bdi In db.DISTRICTs On bcct.DISTRICT Equals bdi.DISTRICT
                        Join ecct In db.ccCTrdBCities On rt.EndCity Equals ecct.ccCTrdBCity
                        Join edi In db.DISTRICTs On bcct.DISTRICT Equals edi.DISTRICT
                        Join sc In db.SOCARRIERs On rt.SOCARRIER Equals sc.SOCARRIER
                        Select BegCity = bcct.CITY, EndDistr = edi.NAME, EndCity = ecct.CITY, SoCarrierName = sc.NAME, rt.SOCOST, rt.ccCRouting, rt.BeginCity, rt_EndCity = rt.EndCity, rt.ISACTIVE, sc.SOCARRIER

                Dim qrt = q.Where(Function(f) f.ISACTIVE > 0)
                If Me.CITYComboBoxBegin.SelectedValue > 0 Then
                    Dim begc As Integer = Me.CITYComboBoxBegin.SelectedValue
                    qrt = qrt.Where(Function(f) f.BeginCity = begc)
                End If
                If Me.SONAMEComboBox.SelectedValue > 0 Then
                    Dim soc As Integer = Me.SONAMEComboBox.SelectedValue
                    qrt = qrt.Where(Function(f) f.SOCARRIER = soc)
                End If
                If Not Me.txtEndCity.Text = "" Then
                    qrt = qrt.Where(Function(f) f.EndCity Like Me.txtEndCity.Text)
                End If
                'qwh = qwh.OrderBy(Function(f) f.NAME)
                qds = qrt
            End If



            Me.MasterBindingSource.DataSource = qds '.MasterBindingSource.DataSource = New SortableBindingList(Of FINDOC_MTRLINE)(nq) 'dt
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
        Exit Sub
        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MasterDataGridView.CellClick
        If Not Me.TlSComboBoxChoice.Text = "Μεταφορείς:" Then
            Exit Sub
        End If

        Dim drv As SOCARRIER = Me.MasterBindingSource.Current
        Me.DetailsBindingSource.Clear()

        MasterDataGridView_CellContentClick1(drv.SOCARRIER)
    End Sub
    Private Sub MasterDataGridView_CellContentClick1(SoCarrier As Short)

        Try

            Dim q = db.ccCRoutings.AsQueryable
            Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            qwh = qwh.Where(Function(f) f.SOCARRIER = SoCarrier)
            'qwh = qwh.OrderBy(Function(f) f.NAME)

            Me.DetailsBindingSource.DataSource = qwh
            Me.DetailDataGridView.DataSource = Me.DetailsBindingSource
            'AddOutOfOfficeColumn(Me.DetailDataGridView)

            'DetailDataGridView.Columns("DST_NAME").HeaderText = "ΠΕΡΙΟΧΗ"
            'DetailDataGridView.Columns("DST_NAME").Width = 150
            'DetailDataGridView.Columns("MORTGAGE_NAME").HeaderText = "ΟΝΟΜΑ"
            'DetailDataGridView.Columns("MORTGAGE_NAME").Width = 250

            DetailDataGridView_Styling()
            Dim aa = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        'End If
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            Me.MasterDataGridView.AutoResizeColumns()
            Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect


            If Me.TlSComboBoxChoice.Text = "Μεταφορείς:" Then
                myArrF = ("SOCARRIER,CODE,Name").Split(",")
                myArrN = ("SOCARRIER,CODE,Name").Split(",")
            End If

            If Me.TlSComboBoxChoice.Text = "Δρομολόγια:" Then
                'ccCRouting, COMPANY, NAME, ACNMASK, ISACTIVE, REMARKS, SOKM, SOCOST, SOCARRIER, BeginCity, EndCity, INSDATE, INSUSER, UPDDATE, UPDUSER
                'rt.SOCOST, BegCity = bcct.CITY, BegDistr = bdi.NAME, EndCity = ecct.CITY, EndDistr = edi.NAME, sc.SOCARRIER, SoCarrierName = sc.NAME
                'myArrF = ("ccCRouting,NAME,SOCOST,SOCARIER,BeginCity,EndCity").Split(",")
                myArrF = ("BegCity,EndCity,EndDistr,SoCarrierName,SOCOST,ccCRouting,BeginCity,rt_EndCity,ISACTIVE,SOCARRIER").Split(",")
                myArrN = ("Τόπος φόρτωσης,Πόλη παραλήπτη,Νομός Παραλήπτη,Μεταφορέας,Κόστος,ccCRouting,BeginCity,rt_EndCity,ISACTIVE,SOCARRIER").Split(",")
            End If

            If Me.TlSComboBoxChoice.Text = "CheckZIP:" Then
                myArrF = ("ΠΩΛΗΤΗΣ,ΚΩΔ_ΠΕΛΑΤΗ,ΕΠΩΝ_ΠΕΛΑΤΗ,ΚΩΔ_ΠΑΡΑΛΗΠΤΗ,ΕΠΩΝ_ΠΑΡΑΛΗΠΤΗ,CITY_S1,ZIP_S1,ZIP,CITY,ΝΟΜΟΣ").Split(",")
                myArrN = ("ΠΩΛΗΤΗΣ,ΚΩΔ_ΠΕΛΑΤΗ,ΕΠΩΝ_ΠΕΛΑΤΗ,ΚΩΔ_ΠΑΡΑΛΗΠΤΗ,ΕΠΩΝ_ΠΑΡΑΛΗΠΤΗ,CITY_S1,ZIP_S1,ZIP,CITY,ΝΟΜΟΣ").Split(",")
            End If



            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)
            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next

            If Not IsNothing(MasterDataGridView.Columns("Ποσότητα Αποθέματος")) Then
                MasterDataGridView.Columns("Ποσότητα Αποθέματος").DefaultCellStyle.Format = "N3"
            End If


            ''Add Unbound Columns
            'Dim DataGridViewComboBoxColumnMTRL_CODE As New DataGridViewComboBoxColumn
            ''DataGridViewComboBoxColumnMTRL_CODE.DataPropertyName = "MTRL"
            ''Me.DataGridViewComboBoxColumn1.DataSource = Me.MTRLBindingSource
            'DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CODE"
            'If f.SERIES = 1001 Then
            '    DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CCCDIESNAME"
            'End If
            'DataGridViewComboBoxColumnMTRL_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            'DataGridViewComboBoxColumnMTRL_CODE.Name = "DataGridViewComboBoxColumnMTRL_CODE"
            'DataGridViewComboBoxColumnMTRL_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'DataGridViewComboBoxColumnMTRL_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            'DataGridViewComboBoxColumnMTRL_CODE.ValueMember = "MTRL"
            'DataGridViewComboBoxColumnMTRL_CODE.Width = 150

            ''
            ''Dim DdlMTRL1_CODE As New DataGridViewComboBoxColumn
            ''dlMTRL1_CODE.DataPropertyName = "MTRL"
            ''Me.DdlMTRL1_CODE.DataSource = Me.MTRLBindingSource
            ''DdlMTRL1_CODE.DisplayMember = "CODE"
            ''DdlMTRL1_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            ''DdlMTRL1_CODE.Name = "DdlMTRL1_CODE"
            ''DdlMTRL1_CODE.ValueMember = "MTRL"
            ''DdlMTRL1_CODE.Width = 150
            ''MasterDataGridView.Columns.Insert(0, DdlMTRL1_CODE)

            'Dim DataGridViewTextBox_OLDMTRL As New DataGridViewTextBoxColumn
            'DataGridViewTextBox_OLDMTRL.HeaderText = "OLDMTRL"
            'DataGridViewTextBox_OLDMTRL.Name = "DataGridViewTextBox_OLDMTRL"


            ''Search_Code
            ''
            'Dim Search_Code As New DataGridViewTextBoxColumn
            'Search_Code.Name = "Search_Code"
            ''Me.MasterDataGridView.Columns.Insert(0, Search_Code)
            'Select Case f.SERIES
            '    Case 9526
            '        Dim DataGridViewComboBoxColumnSTATUS As New DataGridViewComboBoxColumn
            '        '******************************
            '        DataGridViewComboBoxColumnSTATUS.DataPropertyName = "CCCSTATUSID"
            '        DataGridViewComboBoxColumnSTATUS.DataSource = Me.CCCSTATUSBindingSource
            '        DataGridViewComboBoxColumnSTATUS.DisplayMember = "DESCR"
            '        DataGridViewComboBoxColumnSTATUS.HeaderText = "STATUS"
            '        DataGridViewComboBoxColumnSTATUS.Name = "DataGridViewComboBoxColumnSTATUS"
            '        DataGridViewComboBoxColumnSTATUS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '        DataGridViewComboBoxColumnSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '        DataGridViewComboBoxColumnSTATUS.ValueMember = "CCCSTATUS"
            '        MasterDataGridView.Columns.Insert(2, DataGridViewComboBoxColumnSTATUS)
            '        ''***************************************

            'End Select
            'For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
            '    Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            'Next

            'If Not IsNothing(MasterDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
            '    MasterDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΒΕΡΓΕΣ")) Then
            '    MasterDataGridView.Columns("ΒΕΡΓΕΣ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ")) Then
            '    MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ")) Then
            '    MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.")) Then
            '    MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ")) Then
            '    MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
            '    MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ")) Then
            '    MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m")) Then
            '    MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m").DefaultCellStyle.Format = "N6"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.PROFIL")) Then
            '    MasterDataGridView.Columns("ΤΕΜ.PROFIL").DefaultCellStyle.Format = "N0"
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

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                Dim item = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
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
    Private Sub DetailDataGridView_Styling()
        Try

            Me.DetailDataGridView.AutoGenerateColumns = True
            Me.DetailDataGridView.AutoResizeColumns()
            Me.DetailDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.DetailDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect


            'COMPANY, ROUTING, CODE, NAME, ACNMASK, ISACTIVE, REMARKS, SOKM, SOCOST, NUM01, NUM02, NUM03, CCCSOCARIER
            myArrF = ("ROUTING CODE,NAME,SOCOST,CCCSOCARIER").Split(",")
            myArrN = ("ROUTING CODE,NAME,SOCOST,CCCSOCARIER").Split(",")




            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(DetailDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.DetailDataGridView)
            For i As Integer = 0 To DetailDataGridView.Columns.Count - 1
                Debug.Print(DetailDataGridView.Columns(i).DataPropertyName & vbTab & DetailDataGridView.Columns(i).Name)
            Next

            If Not IsNothing(DetailDataGridView.Columns("Ποσότητα Αποθέματος")) Then
                DetailDataGridView.Columns("Ποσότητα Αποθέματος").DefaultCellStyle.Format = "N3"
            End If


            ''Add Unbound Columns
            'Dim DataGridViewComboBoxColumnMTRL_CODE As New DataGridViewComboBoxColumn
            ''DataGridViewComboBoxColumnMTRL_CODE.DataPropertyName = "MTRL"
            ''Me.DataGridViewComboBoxColumn1.DataSource = Me.MTRLBindingSource
            'DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CODE"
            'If f.SERIES = 1001 Then
            '    DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CCCDIESNAME"
            'End If
            'DataGridViewComboBoxColumnMTRL_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            'DataGridViewComboBoxColumnMTRL_CODE.Name = "DataGridViewComboBoxColumnMTRL_CODE"
            'DataGridViewComboBoxColumnMTRL_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'DataGridViewComboBoxColumnMTRL_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            'DataGridViewComboBoxColumnMTRL_CODE.ValueMember = "MTRL"
            'DataGridViewComboBoxColumnMTRL_CODE.Width = 150

            ''
            ''Dim DdlMTRL1_CODE As New DataGridViewComboBoxColumn
            ''dlMTRL1_CODE.DataPropertyName = "MTRL"
            ''Me.DdlMTRL1_CODE.DataSource = Me.MTRLBindingSource
            ''DdlMTRL1_CODE.DisplayMember = "CODE"
            ''DdlMTRL1_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            ''DdlMTRL1_CODE.Name = "DdlMTRL1_CODE"
            ''DdlMTRL1_CODE.ValueMember = "MTRL"
            ''DdlMTRL1_CODE.Width = 150
            ''DetailDataGridView.Columns.Insert(0, DdlMTRL1_CODE)

            'Dim DataGridViewTextBox_OLDMTRL As New DataGridViewTextBoxColumn
            'DataGridViewTextBox_OLDMTRL.HeaderText = "OLDMTRL"
            'DataGridViewTextBox_OLDMTRL.Name = "DataGridViewTextBox_OLDMTRL"


            ''Search_Code
            ''
            'Dim Search_Code As New DataGridViewTextBoxColumn
            'Search_Code.Name = "Search_Code"
            ''Me.DetailDataGridView.Columns.Insert(0, Search_Code)
            'Select Case f.SERIES
            '    Case 9526
            '        Dim DataGridViewComboBoxColumnSTATUS As New DataGridViewComboBoxColumn
            '        '******************************
            '        DataGridViewComboBoxColumnSTATUS.DataPropertyName = "CCCSTATUSID"
            '        DataGridViewComboBoxColumnSTATUS.DataSource = Me.CCCSTATUSBindingSource
            '        DataGridViewComboBoxColumnSTATUS.DisplayMember = "DESCR"
            '        DataGridViewComboBoxColumnSTATUS.HeaderText = "STATUS"
            '        DataGridViewComboBoxColumnSTATUS.Name = "DataGridViewComboBoxColumnSTATUS"
            '        DataGridViewComboBoxColumnSTATUS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '        DataGridViewComboBoxColumnSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '        DataGridViewComboBoxColumnSTATUS.ValueMember = "CCCSTATUS"
            '        DetailDataGridView.Columns.Insert(2, DataGridViewComboBoxColumnSTATUS)
            '        ''***************************************

            'End Select
            'For i As Integer = 0 To DetailDataGridView.Columns.Count - 1
            '    Debug.Print(DetailDataGridView.Columns(i).DataPropertyName & vbTab & DetailDataGridView.Columns(i).Name)
            'Next

            'If Not IsNothing(DetailDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
            '    DetailDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΒΕΡΓΕΣ")) Then
            '    DetailDataGridView.Columns("ΒΕΡΓΕΣ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ")) Then
            '    DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ")) Then
            '    DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.")) Then
            '    DetailDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΠΑΡΑΓΩΓΗ")) Then
            '    DetailDataGridView.Columns("ΠΑΡΑΓΩΓΗ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
            '    DetailDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΩΡΑ ΛΗΞΗ")) Then
            '    DetailDataGridView.Columns("ΩΡΑ ΛΗΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m")) Then
            '    DetailDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m").DefaultCellStyle.Format = "N6"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΤΕΜ.PROFIL")) Then
            '    DetailDataGridView.Columns("ΤΕΜ.PROFIL").DefaultCellStyle.Format = "N0"
            'End If
            'If f.SERIES = 9593 Then 'ΑΒΑΦΟ
            '    If Not IsNothing(DetailDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ")) Then
            '        DetailDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ").ReadOnly = True
            '    End If
            'End If



            ''Add Columns to DetailDataGridView
            'Me.DetailDataGridView.Columns.Insert(0, DataGridViewComboBoxColumnMTRL_CODE)
            'Me.DetailDataGridView.Columns.Insert(0, Search_Code)
            'Me.DetailDataGridView.Columns.Add(DataGridViewTextBox_OLDMTRL)
            'AddOutOfOfficeColumn(Me.DetailDataGridView)

            ''Fill Unbound Collumns
            'For Each row As DataGridViewRow In DetailDataGridView.Rows
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

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In DetailDataGridView.Rows
                Dim item 'As CCCCheckZip = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
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
        'If s.Rows.Count < 2 Then
        '    Exit Sub
        'End If
        If s.Columns(e.ColumnIndex).Name = "DepartmentDataGridViewComboBoxColumn" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim EditedVal As String = cell.EditedFormattedValue
            If Not cell.FormattedValue.ToString = EditedVal Then
                Dim item 'As CCCCheckZip = s.Rows(e.RowIndex).DataBoundItem
                'item.Department = cell.Value
                Dim ar As List(Of String) = ("--Επιλέγξτε--,ΔΙΚΗΓΟΡΟΣ,ΥΠΑΛΛΗΛΟΣ").Split(",").ToList
                'item.Department = ar.FindIndex(Function(f) f = EditedVal)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError

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
    'Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    '    If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
    '        DateTimePicker1.Value = CTODate
    '    Else
    '        DateTimePicker1.Value = "01/01/" & Year(CTODate)
    '    End If
    'End Sub

    'Private Sub TlSBtnWHOUSE_Click(sender As Object, e As EventArgs) Handles TlSBtnWHOUSE.Click
    '    Dim ee As New System.ComponentModel.CancelEventArgs
    '    ee.Cancel = False
    '    ToolStripTextBox_Validating(sender, ee)
    'End Sub
    'Private Sub ToolStripTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TlSTxtWHOUSE.Validating
    '    Dim s As Object = sender
    '    'e.Cancel = False
    '    Dim Valid As Boolean = False
    '    Dim SelectSender As String = ""
    '    Select Case sender.GetType.Name
    '        Case "TextBox"
    '            SelectSender = s.Name
    '        Case "ToolStripButton", "ToolStripTextBox"
    '            SelectSender = s.Name
    '        Case "MyDataGridView", "GmDgView"
    '            SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
    '    End Select
    '    Dim ReturnFields As New ArrayList
    '    Dim errorMsg As String = ""
    '    Dim View As Boolean = False
    '    Dim GmTitle As String = ""
    '    'Dim GmTableName As String = ""
    '    Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
    '    Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
    '    Dim GmCheck As Boolean = False
    '    Dim ValidField As String = ""
    '    Dim sender_TAG As String = ""
    '    Dim Visible As Boolean = False
    '    Dim GmPelPro As Byte = 0
    '    RsWhere = "1=1"
    '    RsOrder = ""
    '    Select Case SelectSender

    '        Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '            TlSTxtWHOUSE.Tag = "WHOUSE"
    '            'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
    '            ReturnFields.Add(TlSTxtWHOUSE)
    '            'ReturnFields.Add(TlSTxtTimKin_Descr)
    '            ''''''''''''''''''''''''''''''''''''''''
    '            GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
    '            RsTables = "WHOUSE"
    '            '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
    '            '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.ccCRouting.PENDING >= 1)
    '            Company = 1000

    '            RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
    '            RsWhere = Trim(RsWhere)
    '            RsOrder = "SHORTCUT"
    '            'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
    '            sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
    '            'GmPelPro = 3 'Δεν υπάρχη PelPro Field
    '            'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
    '            sender_TAG = ReturnFields(0).Tag
    '            myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
    '            myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
    '            View = True
    '    End Select
    '    Dim m_dtGen As DataTable = Nothing
    '    Try
    '        If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
    '            ValidField = Trim(ReturnFields(0).Text)
    '            If Not ValidField.IndexOf("*").Equals(-1) Then
    '                ValidField = ValidField.Replace("*", "%")
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
    '            ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
    '                ValidField = "'" & ValidField.Replace(",", "','") & "'"
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
    '            Else
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
    '            End If
    '            'Visible = True
    '        Else 'ReturnFields(0).Text = ""
    '            If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
    '                '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
    '                For i As Integer = 0 To ReturnFields.Count - 1
    '                    ReturnFields(i).Text = ""
    '                    ReturnFields(i).ToolTipText = ""
    '                Next

    '                'sender.Focus()
    '                e.Cancel = False
    '                Exit Sub
    '                'Visible = True
    '            End If
    '        End If
    '        'RsTables = SelectPelPro(RsTables, PelPro)
    '        'RsWhere = SelectPelPro(RsWhere, PelPro)
    '        'RsOrder = SelectPelPro(RsOrder, PelPro)
    '        Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)

    '        'df = New GmData(sysDB, conn) 'My.Settings.GenConnectionString)
    '        'm_dtGen = df.GmFillTable(mSql, RsTables)
    '        m_dtGen = GmData.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
    '        'Dim dtb As New DataTable
    '        'Using cnn As New SqlConnection(conn)
    '        '    cnn.Open()
    '        '    Using cmd As New SqlCommand(mSql, cnn)
    '        '        'cmd.Parameters.AddWithValue("@COMPANY", 1000)
    '        '        'cmd.Parameters.AddWithValue("@SODTYPE", 51) '51 Αποθήκη
    '        '        'cmd.Parameters.AddWithValue("@DFROM", CDate("01/07/2017")) 'make sure you assign a value To startdate
    '        '        'cmd.Parameters.AddWithValue("@DTO", CDate("01/08/2017")) 'make sure you assign a value To 

    '        '        ''cmd.Parameters.AddWithValue("@MTRL", Nothing) ' AS INTEGER = 2115 --63 --NULL --384 --NULL

    '        '        'cmd.Parameters.AddWithValue("@CODE", "2103030557") ''--'%305%'
    '        '        'cmd.Parameters.AddWithValue("@WHOUSE", Me.TlSTxtWHOUSE.Text.Replace(",", "|")) '"2|4")
    '        '        ''--DECLARE @MTRLS  AS VARCHAR(250) = ''


    '        '        'cmd.Parameters.AddWithValue("@FISCPRD", 2017)
    '        '        'cmd.Parameters.AddWithValue("@PERIOD", 7)

    '        '        Try
    '        '            Using dr As SqlDataReader = cmd.ExecuteReader()
    '        '                'Dim tb = New DataTable()
    '        '                dtb.Load(dr)
    '        '                'Return tb
    '        '            End Using
    '        '        Catch ex As Exception
    '        '            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
    '        '        End Try

    '        '    End Using



    '        '    'Using dad As New SqlDataAdapter(str, cnn)
    '        '    '    dad.Fill(dtb)
    '        '    'End Using
    '        '    cnn.Close()
    '        'End Using
    '        'm_dtGen = dtb
    '        'm_dtGen.TableName = "WHOUSE"
    '        If m_dtGen.Rows.Count = 0 And sender.GetType.Name = "ToolStripButton" Then ' If  "ToolStripButton" SearchFR = Όλα
    '            'RsWhere = "1=1"
    '            'If GmPelPro = 0 Or GmPelPro = 1 Then
    '            '    RsWhere = Trim(RsWhere) & " and PELPRO = " & PelPro
    '            'End If
    '            'mSql = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
    '            'm_dtGen = df.GmFillTable(mSql, RsTables)
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
    '    End Try
    '    m_dvGen = New DataView(m_dtGen)
    '    If m_dvGen.Count = 0 Then
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSBtnPOUDRA", "TlSTxtPROM", "TlSTxtPOUDRA"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '            Case "TlSBtnTimKin", "TlSTxtTimKin"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '        End Select
    '        MsgBox(errorMsg)
    '        For i As Integer = 0 To ReturnFields.Count - 1
    '            ReturnFields(i).Text = ""
    '            ReturnFields(i).ToolTipText = ""
    '        Next
    '        'sender.Focus()
    '        e.Cancel = False
    '        Exit Sub
    '        View = False
    '    ElseIf m_dvGen.Count = 1 Then
    '        View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
    '    End If
    '    If View Or sender.GetType.Name = "ToolStripButton" Then
    '        For i As Integer = 0 To ReturnFields.Count - 1
    '            ReturnFields(i).Text = ""
    '            ReturnFields(i).ToolTipText = ""
    '        Next
    '        If Not s.GetType.Name = "ToolStripButton" Then
    '            ErrorProvider1.SetError(s.Control, "")
    '        End If
    '        'If Visible Then
    '        Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
    '        Dim ar As New ArrayList
    '        Dim m_ds As New DataSet
    '        m_ds = GmData.GmFillDataSet(m_ds, m_dtGen, m_dtGen.TableName)
    '        'ar.Add(m_ds)
    '        'ar.Add(GmTitle)
    '        'ar.Add(GmCheck)
    '        'ar.Add(sSQL)
    '        'ar.Add(GmPelPro)
    '        'ar.Add(RsWhere)
    '        'ar.Add(RsOrder)
    '        'ar.Add(GmGroupSql)
    '        'ar.Add(GmGroupSqlField)
    '        'ar.Add(RsTables)
    '        'ar.Add(myArrF)
    '        'ar.Add(myArrN)
    '        'ar.Add(Point)
    '        'ar.Add(Visible)
    '        Dim TSearchFR As New SearchFR
    '        TSearchFR.Conn = conn
    '        TSearchFR.m_ds = m_ds
    '        TSearchFR.Text = GmTitle
    '        TSearchFR.GmCheck = GmCheck
    '        TSearchFR.sSQL = sSQL
    '        TSearchFR.GmPelPro = Nothing 'GmPelPro
    '        TSearchFR.RsWhere = RsWhere
    '        TSearchFR.RsOrder = RsOrder
    '        TSearchFR.GmGroupSql = GmGroupSql
    '        TSearchFR.GmGroupSqlField = GmGroupSqlField
    '        TSearchFR.RsTables = RsTables
    '        TSearchFR.myArrF = myArrF
    '        TSearchFR.myArrN = myArrN
    '        TSearchFR.Location = Point
    '        TSearchFR.GmCheck = True
    '        'TSearchFR.Visible = View
    '        'TSearchFR.RetTBL = New DataTable
    '        'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
    '        TSearchFR.ShowDialog()
    '        'Dim SearceArrayList As ArrayList = TSearchFR.m_ArrayList 'SearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
    '        Dim Dt As DataTable = TSearchFR.RetTBL
    '        If Not TSearchFR.RetTBL Is Nothing Then

    '            'ReturnFields.Add(TlSTxtPCODE)
    '            'ReturnFields.Add(TlSTxtPELNAME1)
    '            If Dt.Rows.Count > 0 Then
    '                If Dt.Rows.Count = 1 Then
    '                    'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
    '                    'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
    '                    m_dvGen = Dt.DefaultView 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
    '                Else
    '                    Dim Result As String = ""
    '                    For i As Integer = 0 To Dt.Rows.Count - 1
    '                        Result += Trim(Dt.Rows(i)(0)) & "," '("P1_PCODE")
    '                    Next
    '                    ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
    '                End If
    '                ReturnFields(0).ToolTipText = ReturnFields(0).Text
    '                'GmDgLookUp_FillNew = SearceArrayList
    '            Else
    '                '    MsgBox("Δεν υπάρχουν Εγγραφές")
    '                'If Not IsNumeric(TextBox1.Text) Then
    '                If Not s.GetType.Name = "ToolStripButton" Then
    '                    'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
    '                End If
    '                'Else
    '                '    ' Clear the error.
    '                '    ErrorProvider1.SetError(TextBox1, "")
    '                'End If
    '            End If
    '        Else
    '            MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
    '        End If
    '    End If

    '    If m_dvGen.Count = 1 Then
    '        'Dim data_row As DataRowView
    '        'data_row = bindingTim1.Current()
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSTxtPROM"
    '                ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
    '                'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
    '            Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
    '                ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
    '                ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
    '            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '                'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
    '                'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
    '                ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))
    '        End Select
    '        Dim data_row As DataRowView
    '        'data_row = BindingSource1.Current()
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSTxtPROM"
    '                data_row.Item("ETERIA") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer)) 'CInt(Me.GmDgLookUp1.GmReturnFields(2).Text)
    '            'Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
    '            '    If data_row.Item("ETERIA") Is DBNull.Value Then
    '            '        MsgBox("Προσοχή !!! Δεν Βρέθηκε Προμηθευτής", MsgBoxStyle.Critical, "GmError")
    '            '        For i As Integer = 0 To ReturnFields.Count - 1
    '            '            ReturnFields(i).Text = ""
    '            '            ReturnFields(i).ToolTipText = ""
    '            '        Next
    '            '        Exit Sub
    '            '    End If
    '            '    data_row.Item("COLOR") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer))
    '            '    data_row.Item("VAL") = GmNull(m_dvGen(0)("CCCPRICE"), GetType(Double))
    '            '    STOCKTextBox.Text = GmNull(m_dvGen(0)("CCCSSTOCK"), GetType(Double))
    '            '    VALTextBox.Text = data_row.Item("VAL")
    '            '    RsWhere = "Company = " & Company
    '            '    RsWhere = RsWhere & " AND COLOR = " & data_row.Item("COLOR")
    '            '    RsWhere = RsWhere & " AND ETERIA = " & data_row.Item("ETERIA")
    '            '    sSQL = "SELECT TQTY " &
    '            '           "FROM VTOTCCCPOUDRES AS VT " &
    '            '           "WHERE " & RsWhere
    '            '    Dim SumTable As DataTable = df.GmFillTable(sSQL, "VT")
    '            '    'Dim m_dvSum As DataView = New DataView(SumTable)
    '            '    If SumTable.Rows.Count = 1 Then
    '            '        TYPOLQTY = SumTable.Rows(0)("TQTY")
    '            '    Else
    '            '        TYPOLQTY = 0
    '            '    End If
    '            '    Me.txtCTQTY.Text = Format(TYPOLQTY, "#,###") 'Format(data_row("TQTY") - data_row("QTY"), "#,###.#0")
    '            Case "TlSBtnTimKin", "TlSTxtTimKin"
    '                data_row.Item("KK") = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
    '        End Select
    '    End If
    '    If ReturnFields.Count = 3 Then
    '        ReturnFields(ReturnFields.Count - 1).Focus()
    '    End If
    'End Sub
    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click

    End Sub

    Private Sub CITYComboBoxBegin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CITYComboBoxBegin.SelectedIndexChanged
        If Not Me.CITYComboBoxBegin.SelectedValue.GetType.Name = "ccCTrdBCity" Then
            Cmd_Select()
        End If
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        'LoadDataInit() 'For Bind Any Control
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
            'Me.MasterBindingSource.DataSource = db.CCCCheckZips.Where(Function(f) f.ZIP = 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Exit Sub
        Me.DataSafe()
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