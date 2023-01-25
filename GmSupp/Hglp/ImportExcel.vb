Imports System.Data.Linq
Imports System.Data.OleDb
Imports System.Transactions
Imports GmSupp.Hglp

Public Class ImportExcel
    Dim dt As New DataTable
    Dim lsts As New List(Of ccCRouting)
    Dim conStr As String = ""
    Dim sheet As String = ""
    Dim db As New DataClassesHglpDataContext
    Dim ColsTable As DataColumnCollection = Nothing

    Private Sub ImportExcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db = New DataClassesHglpDataContext(My.Settings.GenConnectionString.ToString)
    End Sub

    Private Sub btnOpenFileDialog_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenFileDialog.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.InitialDirectory = "E:\Gm_Softone\Μεταφορείς"
        Dim xlsfilter As String = "Excel files(*.xls*)|*.xls*"
        openFileDialog1.Filter = xlsfilter '"xls files (*.xls)|*.txt|All files (*.*)|*.*" '"txt files (*.txt)|*.txt|All files (*.*)|*.*"
        'openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            'myStream = openFileDialog1.OpenFile()
            FileName.Text = openFileDialog1.FileName
            Me.ddlSheets.Enabled = True
            Me.btnGetExcel.Enabled = True
            GetExcelSheets(FileName.Text, "", "Yes")
        End If
    End Sub
    Private Sub GetExcelSheets(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Try
            ' open xls file
            'Get the Sheets in Excel WorkBoo 
            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"
            conStr = String.Format(conStr, FilePath, isHDR)
            Using connExcel As New OleDbConnection(conStr)
                Dim cmdExcel As New OleDbCommand()
                'Dim oda As New OleDbDataAdapter()
                cmdExcel.Connection = connExcel

                connExcel.Open()

                Dim dt1 As DataTable = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                'Bind the Sheets to DropDownList 
                'ddlSheets.Items.Clear()
                'Me.ddlSheets.Items.AddRange(New Object() {"1", "2", "3"})
                'ddlSheets.Items.Add(New ListItem("--Select Sheet--", ""))
                ddlSheets.DataSource = dt1 ' connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                ddlSheets.DisplayMember = "TABLE_NAME"
                ddlSheets.ValueMember = "TABLE_NAME"
                'ddlSheets.DataBind()


                'Dim myexceldataquery As String = "select * from [Coefs$]"

                'Dim oledbcmd As OleDbCommand = New OleDbCommand(myexceldataquery, connExcel)
                'Dim dr As OleDbDataReader = oledbcmd.ExecuteReader()
                'dt.Load(dr)
            End Using
        Catch ex As Exception

        End Try
        'Throw New NotImplementedException
    End Sub
    Private Sub btnGetExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnGetExcel.Click
        Try
            ' open xls file
            'Get the Sheets in Excel WorkBoo 

            If Not IsNothing(Me.ddlSheets.SelectedValue) Then
                sheet = Me.ddlSheets.SelectedValue

                Using connExcel As New OleDbConnection(conStr)
                    Try
                        Dim cmdExcel As New OleDbCommand()
                        'Dim oda As New OleDbDataAdapter()
                        cmdExcel.Connection = connExcel
                        connExcel.Open()

                        Dim myexceldataquery As String = "select * from [" & sheet & "]" 'Coefs$]"

                        Dim oledbcmd As OleDbCommand = New OleDbCommand(myexceldataquery, connExcel)
                        Dim dreader As OleDbDataReader = oledbcmd.ExecuteReader()
                        dt = New DataTable
                        dt.TableName = "Import"
                        dt.Load(dreader)
                    Catch ex As Exception

                    End Try
                End Using
                ColsTable = dt.Columns
                For Each cl As DataColumn In ColsTable
                    cl.ColumnName = cl.ColumnName.Replace("Μ", "M").Replace("Κ", "K")
                Next
                Me.MasterBindingSource.Filter = Nothing
                Me.MasterBindingSource.DataSource = dt
                'Me.MasterDataGridView = New DataGridView
                Me.MasterDataGridView.DataSource = Me.MasterBindingSource
                Me.MasterDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    'col.HeaderText = "[" & GetExcelColumnName(col.Index + 1) & "] " & vbCrLf & ColsTable(col.Index).ColumnName.Trim
                Next

                'Select Case sheet.Replace("$", "")
                '    Case "Index"
                '        TextBoxRange.Text = "A3:U48"
                '    Case "Coefs"
                '        TextBoxRange.Text = "B2:W140"
                '    Case "avmd"
                '        TextBoxRange.Text = "A1:N29847"
                '    Case "meta"
                '        TextBoxRange.Text = "A2:C64"
                'End Select
                'SetColsLoanCategories()
                Me.btnCheck.Enabled = True
            End If
            'Throw New NotImplementedException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Check()
        Me.btnImport.Enabled = True
    End Sub
    Private Sub btnImport_Click(sender As System.Object, e As System.EventArgs) Handles btnImport.Click
        Dim Title As String = "Import data"
        If MsgBox("lsts.Count = " & lsts.Count & " Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, Title) = MsgBoxResult.Yes Then
            Execute()
        End If
    End Sub
    Private Sub Check()
        Try
            Dim BeginCity As Integer = 0
            Dim EndCity As Integer = 0
            Dim SOCARRIER As Short = 0
            Dim SOCOST As Double = 0
            lsts.Clear()

            For Each dr In dt.Rows
                If IsDBNull(dr("(ccCTrdBCity)")) Then
                    Continue For
                End If
                Dim i = 0
                Try
                    'Χώρα	Περιοχή	Πόλη	[ccCTrdBCity]	T1	M1	MN1	K1	M2	MN2	K2	M3	MN3	K3	M4	MN4	K4	Μ5	ΜΝ5	Κ5	Μ6	ΜΝ6	Κ6	VER
                    '26  ΑΣΠΡΟΠΥΡΓΟΣ
                    '574 ΘΕΣΣΑΛΟΝΙΚΗ
                    '578 ΚΑΒΑΛΑ
                    '586 ΚΟΡΙΝΘΟΣ
                    '149 ΠΥΡΓΟΣ
                    Select Case dr("T1")
                        Case "ΑΣΠΡΟΠΥΡΓΟΣ"
                            BeginCity = 26
                        Case "ΘΕΣΣΑΛΟΝΙΚΗ"
                            BeginCity = 574
                        Case "ΚΑΒΑΛΑ"
                            BeginCity = 578
                        Case "ΚΟΡΙΝΘΟΣ"
                            BeginCity = 586
                        Case "ΠΥΡΓΟΣ"
                            BeginCity = 149
                    End Select
                    EndCity = dr("(ccCTrdBCity)")

                    For i = 1 To 6
                        If Not IsDBNull(dr("M" & i)) Then
                            SOCARRIER = CType(dr("M" & i), Short)

                            If Not IsDBNull(dr("K" & i)) Then
                                SOCOST = dr("K" & i)
                            End If

                            Dim rt As ccCRouting = db.ccCRoutings.Where(Function(f) f.BeginCity = BeginCity And f.EndCity = EndCity And f.SOCARRIER = SOCARRIER).FirstOrDefault
                            If IsNothing(rt) Then
                                rt = New ccCRouting
                                rt.COMPANY = 1000
                                rt.BeginCity = BeginCity
                                rt.EndCity = EndCity
                                rt.SOCARRIER = SOCARRIER
                                rt.SOCOST = SOCOST
                                rt.INSDATE = Now()
                                rt.INSUSER = 99
                                'ccCRouting.NAME=ccCRouting.BeginCity_ccCTrdBCity_CITY+'-'+ccCRouting.EndCity_ccCTrdBCity_CITY+'-'+X.EVAL('TableData('+'\''+'SOCARRIER'+'\''+','+ccCRouting.SOCARRIER+','+'\''+'NAME'+'\''+')') ;
                                'rt.NAME = db.ccCTrdBCities.Where(Function(f) f.)
                                Dim ls = lsts.Where(Function(f) f.BeginCity = BeginCity And f.EndCity = EndCity And f.SOCARRIER = SOCARRIER).FirstOrDefault
                                If IsNothing(ls) Then
                                    lsts.Add(rt)
                                Else
                                    Continue For
                                End If

                            End If
                        End If
                    Next
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Execute()
        Dim Title As String = "Imports Failed"
        Dim MsgBoxSty As New MsgBoxStyle
        MsgBoxSty = MsgBoxStyle.Critical
        Try
            Try
                If lsts.Count > 0 Then
                    db = New DataClassesHglpDataContext(My.Settings.GenConnectionString.ToString)
                    db.ccCRoutings.InsertAllOnSubmit(lsts)
                End If

            Catch ex As Exception

            End Try

            If DataSafe() Then
                Title = "Imports OK"
                MsgBoxSty = MsgBoxStyle.Information
            End If

        Catch ex As Exception

        End Try
        MsgBox(Title, MsgBoxSty)
    End Sub
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        'Me.MasterBindingSource.EndEdit()
        'Me.MTRLINEsBindingSource.EndEdit()
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
                    db = New DataClassesHglpDataContext(My.Settings.GenConnectionString.ToString)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
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
            Me.Cursor = Cursors.WaitCursor
            Dim tx As IO.TextWriter
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()

                    'LogSQL = sSQL
                    'db.Log = Console.Out
                    db.SubmitChanges()

                    'db.UPDATE_ALPHABANK_NEGATIVE_EQUITY_NEW(2, FileName.Text)
                    ' The Complete method commits the transaction. If an exception has been thrown, 
                    ' Complete is called and the transaction is rolled back.
                    scope.Complete()

                    SaveData = True
                    'DgdvRefresh = True
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
        Me.Cursor = Cursors.Default
    End Function
    Private Function Conditions() As Boolean
        Conditions = True
    End Function
#End Region
End Class