Imports System.Data.OleDb
Imports System.Transactions


Public Class Import_Negative_Equitys

    Dim dt As New DataTable
    'Dim lsts As New List(Of UA_NEGATIVE_EQUITY_XL)
    Dim conStr As String = ""
    Dim sheet As String = ""
    Dim ColsLoanCategories As New List(Of ColsLoanCategory)
    Dim db As New DataClassesCentrofaroDataContext
    Dim ColsTable As DataColumnCollection = Nothing
    Private Sub Import_Negative_Equitys_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db = New DataClassesCentrofaroDataContext(My.Settings.CentroConnectionString & ";Connection Timeout=300")

    End Sub

    Private Sub btnOpenFileDialog_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenFileDialog.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.InitialDirectory = "C:\Users\g.matzouranis.CORP\Documents\" '& Today.Year '"E:\HostingSpaces\epsavm\epsavm.gr\data\"
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

    Private Sub btnImport_Click(sender As System.Object, e As System.EventArgs) Handles btnImport.Click
        Dim Title As String = "Import data"
        'If MsgBox("lsts.Count = " & lsts.Count & " Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, Title) = MsgBoxResult.Yes Then
        Execute()
        'End If
    End Sub

    Private Sub Execute()
        Dim Title As String = "Imports Failed"
        Dim MsgBoxSty As New MsgBoxStyle
        MsgBoxSty = MsgBoxStyle.Critical
        'Dim dr As DataRow
        Try


            Try
                Dim head As String = ""
                Me.txtboxResults.Text = ""
                Dim aa As Integer = 1
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim tr = dt.Rows(i)
                    If Not IsDBNull(tr(0)) AndAlso IsNumeric(tr(0)) AndAlso tr(0) = aa Then
                        head = tr(1) & ";" & tr(2) & ";" & tr(3) & ";"
                        aa += 1
                    End If
                    If Not IsDBNull(tr(2)) AndAlso tr(2).ToString.Trim = "Σύνολα" Then
                        head &= tr(5) & ";" & tr(6)
                        Me.txtboxResults.Text &= head & vbCrLf
                        head = ""
                    End If
                Next
                'If lsts.Count > 0 Then
                '    db.UA_NEGATIVE_EQUITY_XLs.InsertAllOnSubmit(lsts)
                'End If

            Catch ex As Exception

            End Try

            'If DataSafe() Then
            '    Title = "Imports OK"
            '    MsgBoxSty = MsgBoxStyle.Information
            'End If
            'End If

        Catch ex As Exception

        End Try
        MsgBox(Title, MsgBoxSty)

    End Sub

    'Private Sub Check()
    '    Dim Title As String = "Imports Failed"
    '    Dim MsgBoxSty As New MsgBoxStyle
    '    MsgBoxSty = MsgBoxStyle.Critical
    '    'Dim dr As DataRow
    '    Try
    '        Dim ColsF = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
    '        'Check If Columns
    '        Dim ok As Boolean = True
    '        For Each cc In ColsF
    '            'If (cc.ColLoanCategory_No = 0 AndAlso IsNothing(cc.ColLoanCategory)) Or cc.COL_CONTRACT_NUMBER3_NO = 0 Or cc.COL_NEGATIVE_EQUITY_NO = 0 Then
    '            If cc.COL_CONTRACT_NUMBER3_NO = 0 Or cc.COL_NEGATIVE_CONTRACT_EQUITY_NO = 0 Then
    '                ok = False
    '                Title = "Columns Errror"
    '                Exit For
    '            End If
    '        Next
    '        If ok Then
    '            Me.txtboxResults.Text = ""
    '            Dim ColLoanCategory = ColsF(0).ColLoanCategory
    '            Dim COL_CONTRACT_NUMBER3 As String = ColsF(0).COL_CONTRACT_NUMBER3
    '            Dim contrs = dt.AsEnumerable().Select(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3)).Distinct()
    '            'Dim q = dt.AsEnumerable().GroupBy(Function(row) row.Item(COL_CONTRACT_NUMBER3))
    '            lsts.Clear()
    '            For Each contr As String In contrs
    '                Dim lst As New UA_NEGATIVE_EQUITY_XL
    '                Try
    '                    If IsNothing(contr) OrElse contr = "999" Then Continue For 'First Line

    '                    'Find LoanCategory
    '                    Dim LoanCategory As String
    '                    LoanCategory = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3) = contr).
    '                        Select(Function(f) f(ColLoanCategory)).FirstOrDefault

    '                    'Find Cols depends LoanCategory
    '                    Dim Coln = ColsLoanCategories.Where(Function(f) LoanCategory.Contains(f.LoanCategory)).FirstOrDefault
    '                    If IsNothing(Coln) Then Continue For

    '                    Dim CONTRACT_EQUITY
    '                    CONTRACT_EQUITY = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3) = contr And Not IsDBNull(f(Coln.COL_NEGATIVE_CONTRACT_EQUITY))).
    '                        Select(Function(f) f(Coln.COL_NEGATIVE_CONTRACT_EQUITY)).FirstOrDefault
    '                    If Not IsNumeric(CONTRACT_EQUITY) AndAlso CONTRACT_EQUITY = "#N/A" Then Continue For
    '                    'Dim SUM_ACCOUNT_EQUITY
    '                    'SUM_ACCOUNT_EQUITY = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3) = contr).
    '                    '    Sum(Function(f) f.Field(Of Nullable(Of Double))(ColsF(0).COL_NEGATIVE_ACCOUNT_EQUITY))
    '                    'If Not CONTRACT_EQUITY = SUM_ACCOUNT_EQUITY Then
    '                    '    SUM_ACCOUNT_EQUITY = SUM_ACCOUNT_EQUITY
    '                    '    ' save cif -contract- account-  NEGATIVE_CONTRACT_EQUITY- NEGATIVE_CONTRACT_EQUITY_STATUS- NEGATIVE_ACCOUNT_EQUITY- NEGATIVE_ACCOUNT_EQUITY_STATUS
    '                    'End If

    '                    'Dim LoanCategory As String
    '                    'LoanCategory = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3) = contr And Not IsDBNull(f(ColsF(0).COL_NEGATIVE_CONTRACT_EQUITY))).
    '                    '    Select(Function(f) f(ColsF(0).ColLoanCategory)).FirstOrDefault

    '                    'Dim cols = ColsF.Where(Function(f) f.LoanCategory = LoanCategory And Not f.LoanCategory = "< Select LoanCategory >").FirstOrDefault
    '                    'If IsNothing(cols) Then
    '                    '    Continue For
    '                    'End If


    '                    'If cols.LoanCategory = "< Select LoanCategory >" Then Continue For
    '                    lst.LoanCategory = Coln.LoanCat
    '                    lst.PRSCMPCITD_CONTRACT_NUMBER3 = contr
    '                    If lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001830664191" Then
    '                        lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001830664191"
    '                    End If

    '                    'If cols.COL_NEGATIVE_CONTRACT_EQUITY_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) AndAlso IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) Then
    '                    lst.PRSCMP_NEGATIVE_EQUITY = Math.Round(CType(CONTRACT_EQUITY, Decimal), 2, MidpointRounding.AwayFromZero) 'CType(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO), Double)
    '                    'Else
    '                    'lst.PRSCMP_NEGATIVE_EQUITY = 0
    '                    'End If

    '                    'If cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) = "#N/A" Then
    '                    If Not IsNothing(Coln.COL_NEGATIVE_CONTRACT_EQUITY_STATUS) Then
    '                        Dim CONTRACT_EQUITY_STATUS ' As String
    '                        CONTRACT_EQUITY_STATUS = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3) = contr And Not IsDBNull(f(Coln.COL_NEGATIVE_CONTRACT_EQUITY))).
    '                        Select(Function(f) f(Coln.COL_NEGATIVE_CONTRACT_EQUITY_STATUS)).FirstOrDefault
    '                        If Not IsDBNull(CONTRACT_EQUITY_STATUS) Then
    '                            lst.PRSCMP_NEGATIVE_EQUITY_STATUS = CONTRACT_EQUITY_STATUS 'dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) '.ToString.Substring(0, 50)
    '                        End If
    '                    End If

    '                    Dim PRSCMP_CaseItms As PERCOM_AS_CASE_ITEMS_DTL = db.PERCOM_AS_CASE_ITEMS_DTLs.Where(Function(f) f.PRSCMPCITD_CONTRACT_NUMBER3 = lst.PRSCMPCITD_CONTRACT_NUMBER3).FirstOrDefault
    '                    If IsNothing(PRSCMP_CaseItms) Then
    '                        Me.txtboxResults.Text &= lst.PRSCMPCITD_CONTRACT_NUMBER3 & vbCrLf
    '                        ok = False
    '                    Else
    '                        lsts.Add(lst)
    '                    End If
    '                Catch ex As Exception
    '                    ok = False
    '                    Title = ex.Message
    '                End Try
    '            Next

    '            If lsts.Count = 0 Then
    '                ok = False
    '                Title = "lsts.Count = 0"
    '            End If

    '            If ok Then
    '                Title = "Check OK"
    '                MsgBoxSty = MsgBoxStyle.Information
    '                Me.btnImport.Enabled = True
    '            End If

    '        End If





    '        'For Each dr In dt.Rows
    '        '    If IsDBNull(dr(ColLoanCategory)) Then Continue For
    '        '    Dim LoanCategory As String = dr(ColLoanCategory)
    '        '    Dim cols = ColsF.Where(Function(f) f.LoanCategory = LoanCategory And Not f.LoanCategory = "< Select LoanCategory >").FirstOrDefault
    '        '    If IsNothing(cols) Then
    '        '        Continue For
    '        '    End If
    '        '    If Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) AndAlso Not IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) AndAlso dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO) = "#N/A" Then
    '        '        Continue For
    '        '    End If
    '        '    Try
    '        '        Dim lst As New UA_NEGATIVE_EQUITY_XL
    '        '        'If cols.LoanCategory = "< Select LoanCategory >" Then Continue For
    '        '        lst.LoanCategory = cols.LoanCat
    '        '        lst.PRSCMPCITD_CONTRACT_NUMBER3 = If(dr(cols.COL_CONTRACT_NUMBER3_NO), "")
    '        '        If lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001852530628" Then
    '        '            lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001852530628"
    '        '        End If

    '        '        If cols.COL_NEGATIVE_CONTRACT_EQUITY_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) AndAlso IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) Then
    '        '            lst.PRSCMP_NEGATIVE_EQUITY = CType(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO), Double)
    '        '        Else
    '        '            lst.PRSCMP_NEGATIVE_EQUITY = 0
    '        '        End If

    '        '        If cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) = "#N/A" Then
    '        '            lst.PRSCMP_NEGATIVE_EQUITY_STATUS = dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) '.ToString.Substring(0, 50)
    '        '        End If
    '        '        db.UA_NEGATIVE_EQUITY_XLs.InsertOnSubmit(lst)
    '        '    Catch ex As Exception

    '        '    End Try
    '        'Next

    '        ''Dim UA_SB_UPDATE_NEGATIVE_EQUITY = From ua In UA_SB_NEGATIVE_EQUITY Join src In Me.MasterBindingSource.AsQueryable On
    '        ''                                   ua.PRSCMPCITD_CONTRACT_NUMBER3 Equals src.ToString

    '        'If DataSafe() Then
    '        '    Title = "Imports OK"
    '        '    MsgBoxSty = MsgBoxStyle.Information
    '        'End If
    '        'End If

    '    Catch ex As Exception
    '        Title & = ex.Message
    '    End Try
    '    MsgBox(Title, MsgBoxSty)
    '    '        Select Case DISTINCT a.[PRSCMP_ID],b.[SB_Negative_Equity per_Account], b.[SB_Negative_Equity per_Account Τύπος Εξασφάλισης]
    '    'INTO  dbo.[UA_SB_UPDATE_NEGATIVE_EQUITY_10_08_2015]
    '    'From dbo.[UA_SB_NEGATIVE_EQUITY_10_08_2015] a INNER Join [dbo].[TSAFARAS_SB_10_08_2015] b
    '    'On a.PRSCMPCITD_CONTRACT_NUMBER3 = b.[ΗΛΕΚΤΡΟΝΙΚΟΣ ΑΡ# ΣΥΜΒΑΣΗΣ]


    '    'Throw New NotImplementedException()
    'End Sub
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
                Me.MasterBindingSource.Filter = Nothing
                Me.MasterBindingSource.DataSource = dt
                'Me.MasterDataGridView = New DataGridView
                Me.MasterDataGridView.DataSource = Me.MasterBindingSource
                Me.MasterDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    col.HeaderText = "[" & GetExcelColumnName(col.Index + 1) & "] " & vbCrLf & ColsTable(col.Index).ColumnName.Trim
                Next

                Me.btnImport.Enabled = True

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
            End If
            'Throw New NotImplementedException
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SetColsLoanCategories()
        Dim dr As DataRow = dt(0)
        Dim FindLoanCategory As Boolean = False

        ColsLoanCategories.Clear()
        Dim lst As New ColsLoanCategory
        lst.LoanCategory = "< Select LoanCategory >"
        ColsLoanCategories.Add(lst)

        lst = New ColsLoanCategory
        lst.LoanCat = "SB"
        lst.LoanCategory = lst.LoanCat
        ColsLoanCategories.Add(lst)

        lst = New ColsLoanCategory
        lst.LoanCat = "ML"
        lst.LoanCategory = lst.LoanCat
        ColsLoanCategories.Add(lst)

        For i As Integer = 0 To dr.ItemArray.Count - 1
            If IsDBNull(dr(i)) Then Continue For
            Dim ls As New ColsLoanCategory
            Dim drValue As Double = IIf(IsNumeric(dr(i)), dr(i), 0)
            Select Case drValue
                Case 999
                    Dim ColsF = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
                    For Each cc In ColsF
                        cc.COL_CONTRACT_NUMBER3_NO = i
                        cc.COL_CONTRACT_NUMBER3 = dt.Columns(i).ColumnName
                    Next
                    FindLoanCategory = True
                Case 11, "1.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY = dt.Columns(i).ColumnName
                    End If
                Case 12, "1.2"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS = dt.Columns(i).ColumnName
                    End If
                Case 21, "2.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "ML").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY = dt.Columns(i).ColumnName
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO = 0
                    End If
                Case 31, "3.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY = dt.Columns(i).ColumnName
                    End If
                Case 32, "3.2"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS_NO = i
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS = dt.Columns(i).ColumnName
                    End If
            End Select
        Next
        If Not FindLoanCategory Then
            MsgBox("Προσοχή !!!. Δεν βρέθηκε Γραμμή 1 αναφοράς", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Me.ddlLoanCategories.DataSource = ColsLoanCategories
        Me.ddlLoanCategories.DisplayMember = "LoanCategory"
        Me.ddlLoanCategories.ValueMember = "LoanCategory"
        Me.ddlLoanCategories.Enabled = True
        Me.LoanCategoriesBindingSource.DataSource = Nothing
        Dim gg = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
        Me.LoanCategoriesBindingSource.DataSource = gg
        Me.LoanCategoriesBindingSource.ResetBindings(False)
        Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
        For Each col As DataGridViewColumn In Me.DataGridViewLoanCategories.Columns
            Debug.Print("col_no:" & col.DisplayIndex & " - " & col.Index & " col.Name:" & col.Name)
        Next
        Me.DataGridViewLoanCategories.Columns(1).HeaderText = "01"
        Me.DataGridViewLoanCategories.Columns(2).HeaderText = "Col_LoanCategory"
        Me.DataGridViewLoanCategories.Columns(3).HeaderText = "02"
        Me.DataGridViewLoanCategories.Columns(4).HeaderText = "COL_ΑΡ# ΣΥΜΒΑΣΗΣ"
        'Me.DataGridViewLoanCategories.Columns(5).HeaderText = "03"

        'Me.DataGridViewLoanCategories.Columns(7).HeaderText = "04"

        'Me.DataGridViewLoanCategories.Columns(0).HeaderText = "F3"
        'Me.DataGridViewLoanCategories.Columns(0).HeaderText = "F4"
        Me.DataGridViewLoanCategories.AutoResizeColumns()
        Me.MasterBindingSource.Filter = Nothing
        autoFillDataGridViewLoanCategories()
        Me.btnCheck.Enabled = True

        'Throw New NotImplementedException()
    End Sub

    Private Sub SelectLoanCategoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectLoanCategoryToolStripMenuItem.Click
        If IsNothing(Me.MasterDataGridView.CurrentCell) Then
            Exit Sub
        End If
        Me.txtboxResults.Text = Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
        Dim ColNo As Integer = Me.MasterDataGridView.CurrentCell.ColumnIndex '.HeaderText
        'SelectCols = SelectCols.Split("]")(1).Trim()
        Dim View As DataView = New DataView(dt)
        Dim distinctValues As DataTable = View.ToTable(True, dt.Columns(ColNo).ColumnName)

        ColsLoanCategories.Clear()
        Dim lst As New ColsLoanCategory
        For Each dr As DataRow In distinctValues.Rows
            If IsDBNull(dr(dt.Columns(ColNo).ColumnName)) Then Continue For
            lst = New ColsLoanCategory
            lst.LoanCategory = dr(dt.Columns(ColNo).ColumnName)
            lst.ColLoanCategory_No = ColNo
            lst.ColLoanCategory = dt.Columns(ColNo).ColumnName
            'lst.CONTRACT_NUMBER3 = ""
            'lst.NEGATIVE_EQUITY = ""
            'lst.NEGATIVE_EQUITY_STATUS = ""
            If lst.LoanCategory.Contains("SB") Then
                lst.LoanCategory = "SB"
            End If
            If lst.LoanCategory.Contains("ML") Then
                lst.LoanCategory = "ML"
            End If
            Select Case lst.LoanCategory
                Case "ΕΠΙΧΕΙΡΗΜΑΤΙΚΟ", "SB"
                    lst.LoanCat = "SB"
                Case "ΣΤΕΓΑΣΤΙΚΟ", "ML"
                    lst.LoanCat = "ML"
                Case Else 'ΣΤΕΓΑΣΤΙΚΟ - ΚΑΤΑΝΑΛΩΤΙΚΟ
                    Continue For
            End Select
            Dim ll = ColsLoanCategories.Where(Function(f) f.LoanCat = lst.LoanCat)
            If ll.Count = 0 Then
                ColsLoanCategories.Add(lst)
            End If
        Next
        If ColsLoanCategories.Count = 0 Then
            Exit Sub
        End If
        lst = New ColsLoanCategory
        lst.LoanCategory = "< Select LoanCategory >"
        ColsLoanCategories.Insert(0, lst)
        Me.ddlLoanCategories.DataSource = ColsLoanCategories
        Me.ddlLoanCategories.DisplayMember = "LoanCategory"
        Me.ddlLoanCategories.ValueMember = "LoanCategory"
        Me.ddlLoanCategories.Enabled = True
        Me.LoanCategoriesBindingSource.DataSource = Nothing
        Dim gg = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
        Me.LoanCategoriesBindingSource.DataSource = gg
        Me.LoanCategoriesBindingSource.ResetBindings(False)
        Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
        For Each col As DataGridViewColumn In Me.DataGridViewLoanCategories.Columns
            Debug.Print("col_no:" & col.DisplayIndex & " - " & col.Index & " col.Name:" & col.Name)
        Next
        Me.DataGridViewLoanCategories.Columns(1).HeaderText = "01"
        Me.DataGridViewLoanCategories.Columns(2).HeaderText = "Col_LoanCategory"
        Me.DataGridViewLoanCategories.Columns(3).HeaderText = "02"
        Me.DataGridViewLoanCategories.Columns(4).HeaderText = "COL_ΑΡ# ΣΥΜΒΑΣΗΣ"
        'Me.DataGridViewLoanCategories.Columns(5).HeaderText = "03"

        'Me.DataGridViewLoanCategories.Columns(7).HeaderText = "04"

        'Me.DataGridViewLoanCategories.Columns(0).HeaderText = "F3"
        'Me.DataGridViewLoanCategories.Columns(0).HeaderText = "F4"
        Me.DataGridViewLoanCategories.AutoResizeColumns()
        Me.MasterBindingSource.Filter = Nothing
        autoFillDataGridViewLoanCategories()
        Me.btnCheck.Enabled = True
    End Sub

    Private Sub autoFillDataGridViewLoanCategories()
        'If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
        'Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
        'If Not cat.LoanCategory = "< Select LoanCategory >" Then
        Dim dr As DataRow = dt(0)
        Dim FindLoanCategory As Boolean = False
        For i As Integer = 0 To dr.ItemArray.Count - 1
            If IsDBNull(dr(i)) Then Continue For
            Dim ls As New ColsLoanCategory
            Dim drValue As Double = IIf(IsNumeric(dr(i)), dr(i), 0)
            Select Case drValue
                Case 999
                    Dim ColsF = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
                    For Each cc In ColsF
                        cc.COL_CONTRACT_NUMBER3_NO = i
                        cc.COL_CONTRACT_NUMBER3 = dt.Columns(i).ColumnName
                    Next
                    FindLoanCategory = True
                Case 11, "1.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY = dt.Columns(i).ColumnName
                    End If
                Case 12, "1.2"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS = dt.Columns(i).ColumnName
                    End If
                Case 21, "2.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "ML").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY = dt.Columns(i).ColumnName
                        ColF.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO = 0
                    End If
                Case 31, "3.1"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_NO = i
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY = dt.Columns(i).ColumnName
                    End If
                Case 32, "3.2"
                    Dim ColF As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCat = "SB").FirstOrDefault
                    If Not IsNothing(ColF) Then
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS_NO = i
                        ColF.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS = dt.Columns(i).ColumnName
                    End If
            End Select
        Next
        If Not FindLoanCategory Then
            MsgBox("Προσοχή !!!. Δεν βρέθηκε Γραμμή 1 αναφοράς", MsgBoxStyle.Critical)
        End If
        'Me.LoanCategoriesBindingSource.ResetBindings(False)
        'Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
        'If Not IsNothing(lst) Then
        '    lst.COL_CONTRACT_NUMBER3_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex + 1
        '    lst.COL_CONTRACT_NUMBER3 = Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
        '    Me.LoanCategoriesBindingSource.ResetBindings(False)
        'End If

        'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
        'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
        '    End If
        'End If
        'Throw New NotImplementedException()
    End Sub

    Private Sub ddlLoanCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLoanCategories.SelectedIndexChanged
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            Me.MasterBindingSource.Filter = Nothing
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                'Me.MasterBindingSource.Filter = dt.Columns(cat.ColLoanCategory_No + 1).ColumnName & " = " & "'" & cat.LoanCategory & "'"
                Me.MasterBindingSource.Filter = "[" & dt.Columns(cat.ColLoanCategory_No).ColumnName & "]" & " = " & " '" & cat.LoanCategory & "'"
            End If
        End If
    End Sub

    Private Sub SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectΑΡΣΥΜΒΑΣΗΣToolStripMenuItem.Click
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
                If Not IsNothing(lst) Then
                    lst.COL_CONTRACT_NUMBER3_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex
                    lst.COL_CONTRACT_NUMBER3 = Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
                    Me.LoanCategoriesBindingSource.ResetBindings(False)
                End If

                'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
                'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
            End If
        End If
    End Sub

    Private Sub SelectColumnsForNegativeEquityContractToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnsForNegativeEquityContractToolStripMenuItem.Click
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
                If Not IsNothing(lst) Then
                    lst.COL_NEGATIVE_CONTRACT_EQUITY_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex
                    lst.COL_NEGATIVE_CONTRACT_EQUITY = Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
                    Me.LoanCategoriesBindingSource.ResetBindings(False)
                End If

                'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
                'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
            End If
        End If

    End Sub

    Private Sub SelectColumnsForNegativeEquityContractStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnsForNegativeEquityContractStatusToolStripMenuItem.Click
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
                If Not IsNothing(lst) Then
                    lst.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex
                    lst.COL_NEGATIVE_CONTRACT_EQUITY_STATUS = Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
                    Me.LoanCategoriesBindingSource.ResetBindings(False)
                End If

                'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
                'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
            End If
        End If
    End Sub

    Private Sub SelectColumnsForNegativeEquityAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnsForNegativeEquityAccountToolStripMenuItem.Click
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
                If Not IsNothing(lst) Then
                    lst.COL_NEGATIVE_ACCOUNT_EQUITY_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex
                    lst.COL_NEGATIVE_ACCOUNT_EQUITY = dt.Columns(lst.COL_NEGATIVE_ACCOUNT_EQUITY_NO).ColumnName ' Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
                    Me.LoanCategoriesBindingSource.ResetBindings(False)
                End If

                'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
                'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
            End If
        End If
    End Sub
    Private Sub SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectColumnsForNegativeEquityAccountStatusToolStripMenuItem.Click
        If Not IsNothing(Me.ddlLoanCategories.DataSource) Then
            Dim cat As ColsLoanCategory = Me.ddlLoanCategories.SelectedItem
            If Not cat.LoanCategory = "< Select LoanCategory >" Then
                Dim lst As ColsLoanCategory = ColsLoanCategories.Where(Function(f) f.LoanCategory = cat.LoanCategory).FirstOrDefault
                If Not IsNothing(lst) Then
                    lst.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS_NO = Me.MasterDataGridView.CurrentCell.ColumnIndex
                    lst.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS = dt.Columns(lst.COL_NEGATIVE_ACCOUNT_EQUITY_STATUS_NO).ColumnName 'Me.MasterDataGridView.Columns(Me.MasterDataGridView.CurrentCell.ColumnIndex).HeaderText
                    Me.LoanCategoriesBindingSource.ResetBindings(False)
                End If

                'Me.LoanCategoriesBindingSource.DataSource = LoanCategories
                'Me.DataGridViewLoanCategories.DataSource = Me.LoanCategoriesBindingSource
            End If
        End If
    End Sub

    Private Function GetExcelColumnName(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = [String].Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) \ 26)
        End While

        Return columnName
    End Function

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        'Check()
        ''dtsum = dt.DefaultView.ToTable(.c.Clone
        'Dim Cols = ColsLoanCategories.Where(Function(f) Not f.LoanCategory = "< Select LoanCategory >")
        ''dtsum = dt.AsEnumerable().GroupBy(Function(f) New With {
        ''        .Col1 = f(ColsF.FirstOrDefault.COL_CONTRACT_NUMBER3),
        ''        .Col2 = f(ColsF.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY)
        ''                                      }).Select(Function(g) g.OrderBy(Function(r) r(ColsF.FirstOrDefault.COL_CONTRACT_NUMBER3)).First()).CopyToDataTable()

        ''dtsum = dt.AsEnumerable().GroupBy(Function(f) f(ColsF.FirstOrDefault.COL_CONTRACT_NUMBER3)).
        ''    Select(Function(g) g.OrderBy(Function(r) r(ColsF.FirstOrDefault.COL_CONTRACT_NUMBER3)).First()).CopyToDataTable()

        'Dim COL_CONTRACT_NUMBER3 As String = Cols.FirstOrDefault.COL_CONTRACT_NUMBER3
        ''Get contract distinct
        'Try
        '    'Dim q = dt.AsEnumerable().Where(Function(w) Not IsNothing(w(ColsF.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY) AndAlso w(ColsF.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY) = 11)) '.GroupBy(Function(f) f(COL_CONTRACT_NUMBER3)).Select(Function(f) f(0)).Sum(Function(f) f(ColsF.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY)) '.CopyToDataTable
        '    'Dim q = dt.AsEnumerable().GroupBy(Function(f) f(ColsF.FirstOrDefault.COL_CONTRACT_NUMBER3))
        '    'dtsum = dt.AsEnumerable().GroupBy(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3)).
        '    '    Select(Function(g) g.FirstOrDefault).CopyToDataTable()
        '    'dtsum = dt.AsEnumerable().GroupBy(Function(f) f.Field(Of String)(COL_CONTRACT_NUMBER3)).
        '    'Select(Function(g) g.FirstOrDefault).Select(Function(w) w(COL_CONTRACT_NUMBER3), w(ColsF.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY)) '.CopyToDataTable()
        '    ndt = dt.DefaultView.ToTable(True, Cols.FirstOrDefault.COL_CONTRACT_NUMBER3, Cols.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY)
        '    Dim q = dt.AsEnumerable().Select(Function(f) f.Field(Of String)(Cols.FirstOrDefault.COL_CONTRACT_NUMBER3)).Distinct()
        '    For Each dr As DataRow In ndt.Rows

        '        ' Dim dtcontract

        '    Next
        '    Dim contrs = dt.AsEnumerable().Select(Function(f) f.Field(Of String)(Cols.FirstOrDefault.COL_CONTRACT_NUMBER3)).Distinct()
        '    For Each contr As String In contrs
        '        If contr = "999" Then Continue For
        '        Dim CONTRACT_EQUITY
        '        CONTRACT_EQUITY = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(Cols.FirstOrDefault.COL_CONTRACT_NUMBER3) = contr And Not IsDBNull(f(Cols.FirstOrDefault.COL_NEGATIVE_ACCOUNT_EQUITY))).
        '            Select(Function(f) f(Cols.FirstOrDefault.COL_NEGATIVE_CONTRACT_EQUITY)).FirstOrDefault
        '        Dim SUM_ACCOUNT_EQUITY
        '        SUM_ACCOUNT_EQUITY = dt.AsEnumerable().Where(Function(f) f.Field(Of String)(Cols.FirstOrDefault.COL_CONTRACT_NUMBER3) = contr).
        '            Sum(Function(f) f.Field(Of Nullable(Of Double))(Cols.FirstOrDefault.COL_NEGATIVE_ACCOUNT_EQUITY))
        '        If Not CONTRACT_EQUITY = SUM_ACCOUNT_EQUITY Then
        '            SUM_ACCOUNT_EQUITY = SUM_ACCOUNT_EQUITY
        '            ' save cif -contract- account-  NEGATIVE_CONTRACT_EQUITY- NEGATIVE_CONTRACT_EQUITY_STATUS- NEGATIVE_ACCOUNT_EQUITY- NEGATIVE_ACCOUNT_EQUITY_STATUS
        '        End If
        '        Try
        '            Dim lst As New UA_NEGATIVE_EQUITY_XL
        '            'If cols.LoanCategory = "< Select LoanCategory >" Then Continue For
        '            lst.LoanCategory = cols.LoanCat
        '            lst.PRSCMPCITD_CONTRACT_NUMBER3 = contr
        '            If lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001852530628" Then
        '                lst.PRSCMPCITD_CONTRACT_NUMBER3 = "001852530628"
        '            End If

        '            If cols.COL_NEGATIVE_CONTRACT_EQUITY_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) AndAlso IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO)) Then
        '                lst.PRSCMP_NEGATIVE_EQUITY = CType(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_NO), Double)
        '            Else
        '                lst.PRSCMP_NEGATIVE_EQUITY = 0
        '            End If

        '            If cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO > 0 AndAlso Not IsDBNull(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not IsNumeric(dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO)) AndAlso Not dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) = "#N/A" Then
        '                lst.PRSCMP_NEGATIVE_EQUITY_STATUS = dr(cols.COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO) '.ToString.Substring(0, 50)
        '            End If
        '            db.UA_NEGATIVE_EQUITY_XLs.InsertOnSubmit(lst)
        '        Catch ex As Exception

        '        End Try

        '    Next


        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub MasterDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellContentClick

    End Sub
End Class

Friend Class ColsLoanCategory
    ''' <summary>
    ''' LoanCategory
    ''' </summary>
    ''' <returns>LoanCategory</returns>
    Public Property LoanCategory As String

    Public Property ColLoanCategory_No As Integer

    ''' <summary>
    ''' ColLoanCategory
    ''' </summary>
    ''' <returns>ColLoanCategory</returns>
    Public Property ColLoanCategory As String

    Public Property COL_CONTRACT_NUMBER3_NO As Integer
    ''' <summary>
    ''' ΗΛΕΚΤΡΟΝΙΚΟΣ ΑΡ# ΣΥΜΒΑΣΗΣ [PERCOM_AS_CASE_ITEMS_DTLS].[PRSCMPCITD_CONTRACT_NUMBER3]
    ''' </summary>
    ''' <returns>PRSCMPCITD_CONTRACT_NUMBER3</returns>
    Public Property COL_CONTRACT_NUMBER3 As String

    Public Property COL_NEGATIVE_ACCOUNT_EQUITY_NO As Integer

    ''' <summary>
    ''' Contracts_Sum
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY</returns>
    Public Property COL_NEGATIVE_ACCOUNT_EQUITY As String

    Public Property COL_NEGATIVE_ACCOUNT_EQUITY_STATUS_NO As Integer

    ''' <summary>
    ''' SB_Negative_Equity per_Account Τύπος Εξασφάλισης Description
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY_STATUS</returns>
    Public Property COL_NEGATIVE_ACCOUNT_EQUITY_STATUS As String

    '----------
    Public Property COL_NEGATIVE_CONTRACT_EQUITY_NO As Integer

    ''' <summary>
    ''' Contracts_Sum
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY</returns>
    Public Property COL_NEGATIVE_CONTRACT_EQUITY As String

    Public Property COL_NEGATIVE_CONTRACT_EQUITY_STATUS_NO As Integer

    ''' <summary>
    ''' SB_Negative_Equity per_Account Τύπος Εξασφάλισης Description
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY_STATUS</returns>
    Public Property COL_NEGATIVE_CONTRACT_EQUITY_STATUS As String

    ''' <summary>
    ''' ΕΠΙΧΕΙΡΗΜΑΤΙΚΟ = SB ,ΣΤΕΓΑΣΤΙΚΟ ML '- ΚΑΤΑΝΑΛΩΤΙΚΟ = CL
    ''' </summary>
    ''' <returns>LoanCat</returns>
    Public Property LoanCat As String

End Class

Friend Class LoanCategory
    ''' <summary>
    ''' LoanCategory
    ''' </summary>
    ''' <returns>LoanCategory</returns>
    Public Property LoanCat As String

    ''' <summary>
    ''' ΗΛΕΚΤΡΟΝΙΚΟΣ ΑΡ# ΣΥΜΒΑΣΗΣ [PERCOM_AS_CASE_ITEMS_DTLS].[PRSCMPCITD_CONTRACT_NUMBER3]
    ''' </summary>
    ''' <returns>PRSCMPCITD_CONTRACT_NUMBER3</returns>
    Public Property CONTRACT_NUMBER3 As String

    ''' <summary>
    ''' Contracts_Sum
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY</returns>
    Public Property NEGATIVE_EQUITY As Double

    ''' <summary>
    ''' SB_Negative_Equity per_Account Τύπος Εξασφάλισης Description
    ''' </summary>
    ''' <returns>PRSCMP_NEGATIVE_EQUITY_STATUS</returns>
    Public Property NEGATIVE_EQUITY_STATUS As String

End Class
