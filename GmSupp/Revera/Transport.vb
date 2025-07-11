﻿Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Transactions
Imports Softone
Imports GmSupp
Imports GmSupp.Hglp

Public Class Transport
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
    Dim LUserManager As GmUserManager = GmUserManager.Create(New GmIdentityDbContext)

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
        DateTimePicker1.Value = CDate("01/01/" & Year(CTODate))
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59)

        StartDate = CDate("01/01/" & Year(CTODate))
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE

        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID

        Me.ddlXCOs.Items.Clear()

        'Test XCOs
        Dim XCOs As List(Of String)
        XCOs = System.IO.Directory.GetFiles(S1Path, "*.xco").ToList
        If XCOs.Count = 0 Then
            MsgBox("Προσοχή !!!. Δεν υπάρχουν τα ανάλογα XCO", MsgBoxStyle.Critical, "Critical")
            Me.Close()
            Exit Sub
        End If
        Dim items As New List(Of String)
        For Each xco In XCOs
            items.Add(xco.Replace(S1Path, "").Replace(".xco".ToUpper, "").ToUpper)
        Next
        'loginf.CompName = items

        'items = items.Where(Function(f) {"REVERA", "SERTORIUS"}.Contains(f)).ToList
        For Each xco In items
            Me.ddlXCOs.Items.Add(IO.Path.GetFileNameWithoutExtension(xco))
        Next
        If items.Where(Function(f) f = "REVERA").Count = 1 Then
            Me.ddlXCOs.Items.Add("SERTORIUS")
        End If
        If items.Count > 1 Then
            Me.ddlXCOs.Text = "Επιλέγξτε"
            Me.ddlXCOs.Items.Insert(0, "Επιλέγξτε")
        Else
            Me.ddlXCOs.SelectedIndex = 0
        End If

        If Not CurUserRole = "Admins" Then
            Me.PanelPickDoc.Visible = False
            If CurUserRole = "Logistics" Then
                Me.PanelPickDoc.Visible = True
            End If
        End If
        Me.ddlPicks.Enabled = False
        Me.OK.Enabled = False

        'CurUserRole = "Logistics"
        If CurUser = "gmlogic" Then
            'conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
            'Select Case conString.InitialCatalog
            '    Case "PFIC"
            '        CompanyT = 1002
            '    Case "LNK"
            '        CompanyT = 1001
            '    Case "LK"
            '        CompanyT = 2001 '1001
            '    Case "NVF"
            '        CompanyT = 2002 '1001
            'End Select

        End If
        Me.KeyPreview = True

        Me.chkBoxIsActive.Checked = False
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
            Dim q = db.ccCTransports

            Dim qwh As IQueryable(Of Revera.ccCTransport) = q

            qwh = qwh.Where(Function(f) f.DeliveryDate >= DateTimePicker1.Value.Date And f.DeliveryDate <= DateTimePicker2.Value)

            If Not Me.chkBoxIsActive.Checked Then
                qwh = qwh.Where(Function(f) Not (If(f.TruckArrival, False) = True And If(f.EnterforLoad, False) = True And If(f.LeaveFactory, False) = True))
            End If

            If Not Me.chkBoxCancelled.Checked Then
                qwh = qwh.Where(Function(f) Not f.TruckTrailerPlate.ToUpper = "CANCELLED".ToUpper)
            End If

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
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") 'MeLabel)
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.No
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
    Dim editableFields_MasterDataGridView() As String = {"TruckArrival", "EnterforLoad", "LeaveFactory", "Comments"}
    Private uss As List(Of GmIdentityUser)

    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If editableFields_MasterDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "Search_User" Then
        '    Exit Sub
        'End If

        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "ExpccCUser" Then
        '    Exit Sub
        'End If


        'If s.Columns(s.CurrentCell.ColumnIndex).Name = "ISACTIVE" Then
        '    Exit Sub
        'End If

        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MasterDataGridView.CellClick
        Dim s As DataGridView = sender
        'Check to ensure that the row CheckBox is clicked.
        If e.ColumnIndex = -1 Then
            Exit Sub
        End If
        Dim colName = s.Columns(e.ColumnIndex).Name
        Debug.Print(colName)
        If e.RowIndex >= 0 AndAlso {"TruckArrival", "EnterforLoad", "LeaveFactory"}.Contains(s.Columns(e.ColumnIndex).Name) Then

            ''Reference the GridView Row.
            'Dim cell As DataGridViewCell = s.Rows(e.RowIndex).Cells(e.ColumnIndex)
            'Dim cellD As DataGridViewCell = s.Rows(e.RowIndex).Cells(e.ColumnIndex + 1)
            'If cell.Value Is Nothing Then
            '    cell.Value = False
            'End If
            ''Me.chkBoxIsActive.Checked = Not cell.Value
            'cellD.Value = Nothing
            'cellD.Style.BackColor = System.Drawing.Color.Empty
            'If Not cell.Value Then
            '    cellD.Value = Now
            '    Select Case s.Columns(e.ColumnIndex).Name
            '        Case "TruckArrival"
            '            cellD.Style.BackColor = System.Drawing.Color.LightBlue
            '        Case "EnterforLoad"
            '            cellD.Style.Back   Color = System.Drawing.Color.Orange
            '        Case "LeaveFactory"
            '            cellD.Style.BackColor = System.Drawing.Color.LightGreen
            '    End Select
            'End If
            'Me.txtBoxNotes.Text = String.Format("Row: {0} Col: {1} ColName: {2} CellValue: {3}", e.RowIndex, e.ColumnIndex, s.Columns(e.ColumnIndex).Name, cell.Value)
            ''cell.Selected = True
            SendKeys.Send(vbTab)
            'Me.txtBoxNotes.Text &= vbCrLf & String.Format("Row: {0} Col: {1} ColName: {2} CellValue: {3}", e.RowIndex, e.ColumnIndex, s.Columns(e.ColumnIndex).Name, cell.Value)
            ''cell.Value = cell.EditedFormattedValue
            'Me.chkBoxIsActive.Checked = cell.EditedFormattedValue 'Not cell.Value
            'Exit Sub
            'If cell.Value IsNot Nothing AndAlso cell.EditedFormattedValue Then
            '    cell.Value = Not cell.EditedFormattedValue
            'Else
            '    cell.Value = False
            'End If
            's.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = Nothing
            'If Not cell.Value Then
            '    s.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = Now
            'End If

            ''Set the CheckBox selection.
            'cell.Cells(colName).Value = Convert.ToBoolean(cell.Cells(colName).EditedFormattedValue)
            'cell = MasterDataGridView.Rows(e.RowIndex + 1)
            'cell.Cells(colName).Value = Nothing
            'If Not cell.Cells(colName).Value Then
            '    cell.Cells(colName).Value = Now
            'End If
            'If CheckBox is checked, display Message Box.
            'If Not Convert.ToBoolean(row.Cells(colName).Value) Then
            'MessageBox.Show(("Selected ID: " & row.Cells(3).Value) & " check:" & Not Convert.ToBoolean(row.Cells(colName).Value))
            'End If
        End If
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

            myArrF = ("DeliveryDate,Consignee,StatisticsAgencyNo,Destination,Driver,Fertiliser,Quantity,TruckType,PickDoc,TruckTrailerPlate,TruckArrivalTime,EnterforLoadTime,LeaveFactoryTime,Comments,findoc,ToCompany,ToFinDoc,createdOn,createdBy,modifiedOn,modifiedBy,ccCTransport").Split(",")
            myArrN = ("DeliveryDate,Consignee,StatisticsAgencyNo,Destination,Driver,Fertiliser,Quantity,TruckType,PickDoc,TruckTrailerPlate,TruckArrivalTime,EnterforLoadTime,LeaveFactoryTime,Comments,findoc,ToCompany,ToFinDoc,createdOn,createdBy,modifiedOn,modifiedBy,ccCTransport").Split(",")


            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)


            'Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            'With ColumnCheckBox
            '    .DataPropertyName = "ISACTIVE"
            '    .HeaderText = "ISACTIVE" 'ColumnName.OutOfOffice.ToString()
            '    .Name = "ISACTIVE" 'ColumnName.OutOfOffice.ToString()
            '    .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            '    .FlatStyle = FlatStyle.Standard
            '    .CellTemplate = New DataGridViewCheckBoxCell()
            '    .CellTemplate.Style.BackColor = Drawing.Color.Beige
            'End With
            ''Me.MasterDataGridView.Columns.Insert(0, column)
            'Me.MasterDataGridView.Columns.Add(ColumnCheckBox)


            Dim SetCols = ("TruckArrival,EnterforLoad,LeaveFactory").Split(",")
            Dim i = 8
            For Each hc In SetCols
                Dim col As New DataGridViewCheckBoxColumn '= Me.MasterDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = hc).FirstOrDefault
                With col
                    .DataPropertyName = hc
                    .HeaderText = hc 'ColumnName.OutOfOffice.ToString()
                    .Name = hc 'ColumnName.OutOfOffice.ToString()
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .FlatStyle = FlatStyle.Standard
                    .CellTemplate = New DataGridViewCheckBoxCell()
                    .CellTemplate.Style.BackColor = Drawing.Color.Beige
                End With
                i += 2
                Me.MasterDataGridView.Columns.Insert(i, col)
            Next

            Me.MasterDataGridView.Columns.Add("Time", "Arrival-LeaveFactory")

            Dim columnComboBox As New DataGridViewComboBoxColumn()
            'columnComboBox.DataPropertyName = "CCCPRIORITY"
            'Dim mtrs = Nothing

            'Dim emptyMTRL As Panel.MTRL() = Nothing
            'emptyMTRL = {New Panel.MTRL With {.CODE = "<Select a product>", .MTRL = 0}}
            'Dim mln As List(Of Panel.MTRL) = (From Empty In emptyMTRL).Union(
            '                                (From m1 In ml Order By m1.CODE)).ToList


            Dim HideCols = ("findoc,ToCompany,ToFinDoc,createdOn,createdBy,modifiedOn,modifiedBy,ccCTransport").Split(",")
            If Not CurUserRole = "Admins" Then
                For Each hc In HideCols
                    Dim col = MasterDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = hc).FirstOrDefault
                    If col IsNot Nothing Then
                        col.Visible = False
                    End If
                Next
            End If




            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows

                'Dim item As Revera.ccCTransport = row.DataBoundItem
                Dim item As Revera.ccCTransport = row.DataBoundItem
                'If Not IsNothing(item) Then
                Try
                    For Each coln As DataGridViewColumn In Me.MasterDataGridView.Columns
                        Dim colName = coln.DataPropertyName

                        If {"TruckArrival", "EnterforLoad", "LeaveFactory"}.Contains(colName) Then
                            If row.Cells(colName).Value Then
                                Select Case colName
                                    Case "TruckArrival"
                                        row.Cells(colName & "Time").Style.BackColor = System.Drawing.Color.LightBlue
                                    Case "EnterforLoad"
                                        row.Cells(colName & "Time").Style.BackColor = System.Drawing.Color.Orange
                                    Case "LeaveFactory"
                                        row.Cells(colName & "Time").Style.BackColor = System.Drawing.Color.LightGreen
                                End Select
                            End If
                        End If

                        If Not CurUserRole = "Admins" Then
                            row.Cells(colName).ReadOnly = True
                            Select Case CurUserRole
                                'Case "Logistics"
                                '    row.Cells("PickDoc").ReadOnly = False
                                Case "Pili"
                                    For Each cn In {"TruckArrival", "EnterforLoad", "LeaveFactory"}
                                        row.Cells(cn).ReadOnly = False
                                        row.Cells(cn & "Time").ReadOnly = False
                                    Next
                                    row.Cells("Comments").ReadOnly = False
                            End Select
                        End If
                        If item.TruckArrivalTime IsNot Nothing And item.LeaveFactoryTime IsNot Nothing Then
                            Dim myticks As Integer = System.Environment.TickCount
                            Dim MySpan As TimeSpan = TimeSpan.FromTicks(item.LeaveFactoryTime.Value.Subtract(item.TruckArrivalTime.Value).Ticks)
                            'Label1.Text = MySpan.Hours.ToString & " Hours, " & MySpan.Minutes.ToString & " Minutes, " & MySpan.Seconds.ToString & " Seconds"
                            'If MySpan.Hours > 0 Then
                            If Not (MySpan.Hours = 0 And MySpan.Minutes = 0) Then
                                row.Cells("Time").Value = String.Format("{0}:{1}", String.Format("{0:00}", MySpan.Hours), String.Format("{0:00}", MySpan.Minutes))
                            End If
                            'End If
                        End If
                        'item.LeaveFactoryTime.Value.Subtract(item.TruckArrivalTime.Value)
                    Next

                Catch ex As Exception

                End Try
                'End If

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
            Me.txtBoxNotes.Text = ""
            If {"TruckArrival", "EnterforLoad", "LeaveFactory"}.Contains(s.Columns(e.ColumnIndex).Name) Then
                Dim cellc As DataGridViewCheckBoxCell = s.CurrentCell
                Dim celle = cellc.EditedFormattedValue

                Me.txtBoxNotes.Text = String.Format("Row: {0} Col: {1} ColName: {2} CellValue: {3} CellValueN: {4}", e.RowIndex, e.ColumnIndex, s.Columns(e.ColumnIndex).Name, cellc.FormattedValue, celle)
                'cell.Selected = True

                If Not cellc.FormattedValue = celle Then
                    Me.txtBoxNotes.Text &= vbCrLf & String.Format("Row: {0} Col: {1} ColName: {2} CellValue: {3} CellValueN: {4}", e.RowIndex, e.ColumnIndex, s.Columns(e.ColumnIndex).Name, cellc.FormattedValue, celle)
                    Dim item As Revera.ccCTransport = s.Rows(e.RowIndex).DataBoundItem

                    If s.Columns(e.ColumnIndex).Name = "LeaveFactory" Then
                        If (item.TruckArrival Is Nothing OrElse Not item.TruckArrival) Or (item.EnterforLoad Is Nothing OrElse Not item.EnterforLoad) Then
                            MsgBox("Προσοχή !!! Λάθος καταχώρηση.", MsgBoxStyle.Critical, "MasterDataGridView_CellValidating")
                            cellc.EditingCellFormattedValue = False
                            Exit Sub
                        End If
                    End If
                    If s.Columns(e.ColumnIndex).Name = "EnterforLoad" Then
                        If (item.TruckArrival Is Nothing OrElse Not item.TruckArrival) Then
                            MsgBox("Προσοχή !!! Λάθος καταχώρηση.", MsgBoxStyle.Critical, "MasterDataGridView_CellValidating")
                            cellc.EditingCellFormattedValue = False
                            Exit Sub
                        End If
                        If (item.LeaveFactory IsNot Nothing AndAlso item.LeaveFactory) Then
                            MsgBox("Προσοχή !!! Λάθος καταχώρηση.", MsgBoxStyle.Critical, "MasterDataGridView_CellValidating")
                            cellc.EditingCellFormattedValue = True
                            Exit Sub
                        End If
                    End If

                    'Dim mtrl As Integer = item.mtrl
                    Dim id As Integer = item.ccCTransport
                    Dim ct = db.ccCTransports.Where(Function(f) f.COMPANY = item.COMPANY And f.ccCTransport = id).FirstOrDefault
                    If Not IsNothing(ct) Then
                        If celle Then
                            ct.GetType().GetProperty(s.Columns(e.ColumnIndex).Name).SetValue(ct, celle, Nothing)
                            ct.GetType().GetProperty(s.Columns(e.ColumnIndex).Name & "Time").SetValue(ct, Now, Nothing)
                        Else
                            ct.GetType().GetProperty(s.Columns(e.ColumnIndex).Name).SetValue(ct, Nothing, Nothing)
                            ct.GetType().GetProperty(s.Columns(e.ColumnIndex).Name & "Time").SetValue(ct, Nothing, Nothing)
                        End If

                        If db.GetChangeSet.Updates.Count > 0 Then
                            'Me.BindingNavigatorSaveItem.Enabled = True
                            If DataSafe() Then
                                If celle Then
                                    Select Case s.Columns(e.ColumnIndex).Name
                                        Case "TruckArrival"
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).Style.BackColor = System.Drawing.Color.LightBlue
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.LightBlue
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).ReadOnly = True
                                        Case "EnterforLoad"
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).Style.BackColor = System.Drawing.Color.Orange
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.Orange
                                        Case "LeaveFactory"
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).Style.BackColor = System.Drawing.Color.LightGreen
                                            s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.LightGreen
                                            's.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).ReadOnly = True
                                    End Select
                                    'If s.Rows(e.RowIndex).Cells("TruckArrival").Value And s.Rows(e.RowIndex).Cells("EnterforLoad").Value And s.Rows(e.RowIndex).Cells("LeaveFactory").Value Then
                                    '    s.Rows.Remove(s.Rows(e.RowIndex))
                                    'End If
                                Else
                                    s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name).Style.BackColor = Nothing
                                    s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = Nothing
                                End If
                            End If
                            'Cmd_Select()
                            'Me.BindingNavigatorSaveItem.PerformClick()
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
        'If s.Columns(e.ColumnIndex).Name = "ExpccCUser" Then
        If {"TruckArrival", "EnterforLoad", "LeaveFactory"}.Contains(s.Columns(e.ColumnIndex).Name) Then
            Dim cellc As DataGridViewCheckBoxCell = s.CurrentCell
            'Cmd_Select()
            Exit Sub
            If cellc.Value Then
                s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Value = Now
                Select Case s.Columns(e.ColumnIndex).Name
                    Case "TruckArrival"
                        s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.LightBlue
                    Case "EnterforLoad"
                        s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.Orange
                    Case "LeaveFactory"
                        s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = System.Drawing.Color.LightGreen
                End Select
            Else
                s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Value = Nothing
                s.Rows(e.RowIndex).Cells(s.Columns(e.ColumnIndex).Name & "Time").Style.BackColor = Nothing
            End If


            Exit Sub

            Dim ExpccCUser As String = cellc.EditedFormattedValue
            Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_User")
            If celln.Value = "0" AndAlso ExpccCUser = String.Empty Then
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
        Dim s As DataGridView = sender
        If editableFields_MasterDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If
        'If sender.Columns(e.ColumnIndex).Name = "Search_User" Then
        '    Exit Sub
        'End If

        'If sender.Columns(e.ColumnIndex).Name = "ISACTIVE" Then
        '    Exit Sub
        'End If

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
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
            DateTimePicker1.Value = CTODate
        Else
            DateTimePicker1.Value = "01/01/" & Year(CTODate)
        End If
    End Sub
    Dim CompId As Short = 0
    Private Sub ddlXCOs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlXCOs.SelectedIndexChanged
        Dim s As ComboBox = sender
        If Not s.SelectedItem = "Επιλέγξτε" Then
            'Dim trdr As Integer? = s.SelectedValue
            'Me.ddlPicks.DropDownStyle = ComboBoxStyle.DropDownList

            Dim emptyFinDoc() As Revera.FINDOC
            emptyFinDoc = {New Revera.FINDOC With {.FINCODE = "<Επιλέγξτε>", .FINDOC = 0}}

            Dim ConStr = ""
            Select Case s.SelectedItem
                Case "SERTORIUS"
                    ConStr = My.Settings.ReveraConnectionString.ToString
                    CompId = 5000
                Case "REVERA"
                    ConStr = My.Settings.ReveraConnectionString.ToString
                    CompId = 4000
                Case "HGLP"
                    ConStr = My.Settings.HglpConnectionString.ToString
                    CompId = 1000
                Case "LK"
                    ConStr = My.Settings.LKConnectionString.ToString
                    CompId = 2001
                Case "AGUSTINO"
                    ConStr = My.Settings.AgustinoConnectionString.ToString
                    CompId = 5001
                Case Else
                    MsgBox("Λάθος!!! Εταιρεία", MsgBoxStyle.Critical, "OK_Click")
                    Exit Sub
            End Select

            Dim fis As New List(Of Revera.FINDOC)
            Using dbn As New DataClassesReveraDataContext
                dbn.Connection.ConnectionString = ConStr
                DFrom = Me.DateTimePicker1.Value.Year & "/" & Me.DateTimePicker1.Value.Month & "/" & Me.DateTimePicker1.Value.Day
                Dto = Me.DateTimePicker1.Value.Year & "/" & Me.DateTimePicker1.Value.Month & "/" & Me.DateTimePicker1.Value.Day
                Dim sql = "Select A.FINDOC, A.FINCODE from FINDOC as A where (A.COMPANY = " & CompId & ") AND (A.SOSOURCE = 1351) AND (A.SOREDIR = 0) AND (A.TRNDATE >= " &
                    String.Format("'{0:yyyyMMdd}'", Me.DateTimePicker1.Value) & ") AND (A.TRNDATE <= " &
                    String.Format("'{0:yyyyMMdd}'", Me.DateTimePicker2.Value) & ") AND (A.SERIES = 1000) AND (A.SODTYPE = 13) AND (A.FULLYTRANSF IN (0, 2))"
                'AND (A.TRNDATE >= ""20230101"") AND (A.TRNDATE < ""20230801"")
                Dim catList = dbn.ExecuteQuery(Of Revera.FINDOC)(sql).ToList

                'fis = (emptyFinDoc.ToList.Union(dbn.FINDOCs.Where(Function(f) (f.COMPANY = CompId) And (f.SOSOURCE = 1351) And (f.SOREDIR = 0) And (f.SERIES = 1000) And (f.SODTYPE = 13) And {0, 2}.Contains(f.FULLYTRANSF)).ToList).ToList)
                fis = (emptyFinDoc.ToList.Union(catList.ToList).ToList)
            End Using

            'And (f.TRNDATE >= "20230701") And (f.TRNDATE < "20230801")
            Me.ddlPicks.DisplayMember = "FINCODE"
            Me.ddlPicks.ValueMember = "FINDOC"
            Me.ddlPicks.DataSource = fis
            Me.ddlPicks.Enabled = True
            Me.OK.Enabled = True
        Else
            Me.ddlPicks.Enabled = False
            Me.OK.Enabled = False
        End If
    End Sub
    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        If Me.ddlXCOs.SelectedItem = "Επιλέγξτε" Or Me.ddlPicks.SelectedValue = 0 Or Me.MasterDataGridView.SelectedRows.Count = 0 Then
            MsgBox("Λάθος!!! Επιλέξτε PICK ή Γραμμές για ενημέρωση με PICK", MsgBoxStyle.Critical, "OK_Click")
            Exit Sub
        End If
        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                ds.Cells("ToCompany").Value = CompId
                ds.Cells("ToFinDoc").Value = Me.ddlPicks.SelectedValue
                ds.Cells("PickDoc").Value = Me.ddlPicks.Text
            Next
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        If Me.MasterDataGridView.SelectedRows.Count = 0 Then
            MsgBox("Λάθος!!! Επιλέξτε γραμμές για εκτύπωση", MsgBoxStyle.Critical, "cmdPrint_Click")
            Exit Sub
        End If

        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
            If Not IO.File.Exists(S1Path & "XDll.dll") Then
                'Return Login'
                MsgBox("Connection Error:" & S1Path & "XDll.dll", MsgBoxStyle.Critical, strAppName)
            End If

            Dim PrinterName = ""
            Dim pd As New PrintDialog()

            ' Open the printer dialog box, and then allow the user to select a printer.
            pd.PrinterSettings = New Drawing.Printing.PrinterSettings()
            pd.PrinterSettings.Copies = 3
            If pd.ShowDialog = DialogResult.OK Then
                PrinterName = pd.PrinterSettings.PrinterName
            End If

            XSupport.InitInterop(0, S1Path & "XDll.dll")

            For Each ds As DataGridViewRow In DrSel
                Dim ToCompany As Short = ds.Cells("ToCompany").Value
                Dim ToFinDoc As Integer = ds.Cells("ToFinDoc").Value

                Dim XCOFile = "C:\Softone\REVERA.xco" 'S1Path & CompName.Replace("SERTORIUS", "REVERA").Replace("ReveraLite".ToUpper, "REVERA").Replace("HglpLite".ToUpper, "HGLP").ToUpper & ".xco"
                Select Case ToCompany
                    Case 5000
                        XCOFile = "C:\Softone\" & "SERTORIUS" & ".XCO"
                        If Not IO.File.Exists(XCOFile) Then
                            XCOFile = "C:\Softone\" & "REVERA" & ".XCO"
                        End If
                    Case 4000
                        XCOFile = "C:\Softone\" & "REVERA" & ".XCO"
                    Case 1000
                        XCOFile = "C:\Softone\" & "HGLP" & ".XCO"
                    Case 2001
                        XCOFile = "C:\Softone\" & "LK" & ".XCO"
                    Case 5001
                        XCOFile = "C:\Softone\" & "AGUSTINO" & ".XCO"
                    Case Else
                        MsgBox("Λάθος!!! Εταιρεία", MsgBoxStyle.Critical, "cmdPrint")
                        Exit Sub
                End Select
                If Not IO.File.Exists(XCOFile) Then
                    MsgBox("Λάθος!!! XCOFile", MsgBoxStyle.Critical, "cmdPrint")
                    Exit Sub
                End If

                Try
                    Dim Branch = 1000
                    Dim DTLogin = CTODate 'DateTime.Now
                    s1Conn = Nothing
                    s1Conn = XSupport.Login(XCOFile, "gmlogic", "1mgergm++",
                                               Company.ToString, Branch.ToString, DTLogin)

                    If s1Conn.ConnectionInfo IsNot Nothing Then
                        'Login = True
                        'UserId = s1Conn.ConnectionInfo.UserId

                        Dim S1obj As XModule
                        S1obj = s1Conn.CreateModule("SALDOC") 'Soft1Object

                        Dim myArray(4) As String
                        S1obj.LocateData(ToFinDoc)
                        Dim dt As DataTable = S1obj.GetTable("FINDOC").CreateDataTable(True)
                        myArray(0) = S1obj.Handle

                        Dim xt As Softone.XTable
                        Dim td As New DataTable
                        Dim sql = "SELECT se.TEMPLATES FROM FINDOC AS fi INNER JOIN SERIES AS se ON fi.COMPANY = se.COMPANY AND fi.SOSOURCE = se.SOSOURCE AND fi.SERIES = se.SERIES WHERE fi.FINDOC = :1" & ToFinDoc
                        xt = s1Conn.GetSQLDataSet(sql, {ToFinDoc})
                        td = xt.CreateDataTable(True)
                        Dim templ As Short = xt(0, "TEMPLATES")
                        myArray(1) = templ     'Print Form Code
                        myArray(2) = PrinterName '"PDF file" 'cmbPrinters.SelectedItem.ToString  'Printer Name or File Name Type (928, 437, EXCEL, WORD, METAFILE)
                        myArray(3) = "E:\temp\Data\" & dt(0)("FINCODE") & ".pdf"
                        Dim SysRequest As Object = s1Conn.GetStockObj("SysRequest", True)
                        'SoftOne Object
                        For no = 1 To pd.PrinterSettings.Copies
                            s1Conn.CallPublished(SysRequest, "PrintForm", myArray)
                        Next

                        'Soft1Conn.CallPublished("SysRequest.PrintForm', VarArray(vPURModule,100,'928', vFile,4));

                        'Dim sourcePath = "C:\test\" & "_" + item.FINDOC.ToString & ".PDF"
                        'Dim destinationPath = "C:\test\" & item.FINCODE & "_" & String.Format("{0:yyyyMMdd}", item.TRNDATE) & "_" & item.CODE & "_" + item.QTY1.ToString & ".PDF"
                        'Dim info As IO.FileInfo = New IO.FileInfo(sourcePath)
                        'If info.Exists Then
                        '    info.MoveTo(destinationPath)
                        'End If

                    Else
                        Me.Text = strAppName
                        MsgBox("Connection Error! s1Conn.ConnectionInfo Is Nothing", MsgBoxStyle.Critical, strAppName)
                    End If

                Catch ex As Exception
                    Me.Text = strAppName
                    MsgBox("Connection Error:" & vbCrLf & S1Path & vbCrLf & CurUser & vbCrLf & ex.ToString, MsgBoxStyle.Critical, strAppName)
                    'MsgBox(SERVER & vbCrLf & GetExceptionInfo(ex))

                Finally
                    'Me.Cursor = Cursors.Default
                End Try
            Next
            'XSupport.EndInterop()
        End If
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
            'Me.cmdSelect.PerformClick()
        End If
    End Sub

    Private Sub MasterBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MasterBindingSource.ListChanged
        If e.ListChangedType = ListChangedType.ItemChanged Then
            Dim nu ' As CCCCheckZip = MasterBindingSource.Current
            'nu.modifiedOn = Now()
            Me.BindingNavigatorSaveItem.Enabled = True
            'DataSafe()
            'SaveData()
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