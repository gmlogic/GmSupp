Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Text

Public Class GmData
    Enum FileState
        create = 1
        read = 2
        write = 4
        delete = 8
    End Enum
    Private mConnection As DbConnection
    Private mConnectionString As String
    Private mdata_row_state As DataRowState
    Private mdataFactory As DbProviderFactory
    Private mdtChanges As DataTable
    Private msysDB As String
    Private mtransaction As DbTransaction
    Private mdt As DataTable
    ''' <summary>
    ''' sysDB,ConnString/msysDB = "System.Data.SqlClient" '"System.Data.OleDb" '"MSAccess"
    ''' </summary>
    ''' <param name="sysDB"></param>
    ''' <param name="ConnString"></param>
    ''' <remarks></remarks>
    Sub New(ByVal sysDB As String, ByVal ConnString As String)
        Try
            If DbProviderFactories.GetFactoryClasses.Rows.Count Then
                ' Retrieve the installed providers and factories.
                Dim table As DataTable = DbProviderFactories.GetFactoryClasses()
                msysDB = sysDB
                mConnectionString = ConnString
                mdataFactory = DbProviderFactories.GetFactory(msysDB)
            Else
                MsgBox("Προσοχή !!! DbProviderFactories.GetFactoryClasses.Rows.Count = " & DbProviderFactories.GetFactoryClasses.Rows.Count)
            End If

        Catch ex As Exception

        End Try
    End Sub
    '''' <summary>
    '''' Fill DataTables with table_name
    '''' </summary>
    '''' <param name="table_name"></param>
    '''' <remarks></remarks>
    'Sub New(ByVal table_name As String, ByVal connect_string As String)
    '    GmConnectionString = connect_string
    '    tblname = table_name
    '    mdataFactory = DbProviderFactories.GetFactory(msysDB)
    'End Sub
    Sub New()
        'mConnectionString = "data source=127.0.0.1;initial catalog=GMWIN;user id=sa"
        'msysDB = "System.Data.SqlClient" '"System.Data.OleDb" '"MSAccess"
        'mdataFactory = DbProviderFactories.GetFactory(msysDB)
    End Sub
    ' This example assumes a reference to System.Data.Common.
    Private Shared Function GetProviderFactoryClasses() As DataTable

        ' Retrieve the installed providers and factories.
        Dim table As DataTable = DbProviderFactories.GetFactoryClasses()
        Try
            ' Display each row and column value.
            Dim row As DataRow
            Dim column As DataColumn
            For Each row In table.Rows
                For Each column In table.Columns
                    Console.WriteLine(row(column))
                Next
            Next

        Catch ex As Exception

        End Try
        Return table
    End Function

    Public Function GmExecuteReader(ByVal select_statement As String) As DbDataReader
        GmExecuteReader = Nothing
        Try
            'mConnection.Open()
            ' Use an SqlCommand.
            'Dim command As New SqlClient.SqlCommand ' = mConnection.CreateCommand()
            Dim command As DbCommand = mdataFactory.CreateCommand '.Connection.CreateCommand()
            If IsNothing(mConnection) Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
                mConnection.Open()
            ElseIf mConnection.State = ConnectionState.Closed Then
                mConnection.Open()
            End If
            command.Connection = mConnection
            command.CommandText = select_statement
            command.Transaction = mtransaction

            ' Execute the command.
            command.CommandTimeout = mTimeOut
            GmExecuteReader = command.ExecuteReader()
        Catch exc As Exception
            ' Add an error message to the results.
            ' Display the exception's error message and ask
            ' the user if we should continue with other statements.
            Dim stop_early As Boolean = (MsgBox(exc.Message & vbCrLf &
                "Continue?",
               MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo,
                "SQL Error") = MsgBoxResult.No)
        End Try
        If IsNothing(mtransaction) Then
            If notCloseConnection = False Then
                mConnection.Close()
            End If

        End If
        'Return rows_affected
    End Function

    Public Function GmExecuteNonQuery(ByVal select_statement As String) As Long
        Dim rows_affected As Long
        Try
            'mConnection.Open()
            ' Use an SqlCommand.
            'Dim command As New SqlClient.SqlCommand ' = mConnection.CreateCommand()
            Dim command As DbCommand = mdataFactory.CreateCommand '.Connection.CreateCommand()
            If IsNothing(mConnection) Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
                mConnection.Open()
            ElseIf mConnection.State = ConnectionState.Closed Then
                mConnection.Open()
            End If
            command.Connection = mConnection
            command.CommandText = select_statement
            command.Transaction = mtransaction
            Dim params As New List(Of DbParameter)
            If Not IsNothing(newParameterValue) Then
                command.Parameters.Clear()
                For Each prm As DbParameter In newParameterValue
                    Dim cmdparam As DbParameter = mdataFactory.CreateParameter
                    With cmdparam
                        .DbType = prm.DbType
                        .Direction = prm.Direction
                        .ParameterName = prm.ParameterName
                        .Size = prm.Size
                        .SourceColumn = prm.SourceColumn
                        .SourceColumnNullMapping = prm.SourceColumnNullMapping
                        .SourceVersion = prm.SourceVersion
                        .Value = prm.Value
                    End With
                    command.Parameters.Add(cmdparam)
                Next
                'command.Parameters.AddRange(params)
            End If

            ' Execute the command.
            'For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command.
            'For all other types of statements, the return value is -1. If a rollback occurs, the return value is also -1.
            'For CREATE TABLE and DROP TABLE statements, the return value is 0
            command.CommandTimeout = mTimeOut
            rows_affected = command.ExecuteNonQuery()
            If Not rows_affected > 0 Then
                MsgBox("Προσοχή!!! Δεν ενημερώθηκε " & rows_affected.ToString & " Εγγραφή" & vbCrLf & command.CommandText, MsgBoxStyle.Exclamation, "SQL Error")
                OkTransaction = False
            End If
        Catch exc As Exception
            ' Add an error message to the results.
            ' Display the exception's error message and ask
            ' the user if we should continue with other statements.
            Dim stop_early As Boolean = (MsgBox(exc.Message & vbCrLf & _
                "Continue?", _
               MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, _
                "SQL Error") = MsgBoxResult.No)
            OkTransaction = False
        End Try
        If IsNothing(mtransaction) Then
            mConnection.Close()
        End If
        Return rows_affected
    End Function
    Public Function GmExecuteScalar(ByVal select_statement As String) As Object
        'Dim result As Object = Nothing
        GmExecuteScalar = Nothing
        Try
            'mConnection.Open()
            ' Use an SqlCommand.
            Dim command As DbCommand = mdataFactory.CreateCommand '.Connection.CreateCommand()
            If IsNothing(mConnection) Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
                mConnection.Open()
            ElseIf mConnection.State = ConnectionState.Closed Then
                mConnection.Open()
            End If
            command.Connection = mConnection
            command.CommandText = select_statement
            command.Transaction = mtransaction

            ' Execute the command.
            'Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. 
            command.CommandTimeout = mTimeOut
            GmExecuteScalar = command.ExecuteScalar
        Catch exc As Exception
            ' Add an error message to the results.
            ' Display the exception's error message and ask
            ' the user if we should continue with other statements.
            Dim stop_early As Boolean = (MsgBox(exc.Message & vbCrLf & _
                "Continue?", _
               MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, _
                "SQL Error") = MsgBoxResult.No)
            OkTransaction = False
        End Try
        If IsNothing(mtransaction) Then
            mConnection.Close()
        End If
        Return GmExecuteScalar 'result

        'mConnection = mdataFactory.CreateConnection()
        'mConnection.ConnectionString = mConnectionString
        'mConnection.Open()

        ''Me.Cursor = Cursors.WaitCursor
        'Dim result As String = ""
        'Try
        '    ' Use an SqlCommand.
        '    Dim command As DbCommand = mConnection.CreateCommand()
        '    'command.Connection = conn
        '    command.CommandText = select_statement

        '    ' Execute the command.
        '    'Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored. 
        '    result = IIf(IsDBNull(command.ExecuteScalar), "", command.ExecuteScalar)
        'Catch exc As Exception
        '    ' Add an error message to the results.
        '    ' Display the exception's error message and ask
        '    ' the user if we should continue with other statements.
        '    Dim stop_early As Boolean = (MsgBox(exc.Message & vbCrLf & _
        '        "Continue?", _
        '        MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, _
        '        "SQL Error") = MsgBoxResult.No)
        'End Try
        ''Me.Cursor = Cursors.Default
        'If OkTransaction = False Then
        '    mConnection.Close()
        'End If
        'Return result
    End Function
    Public Function GmFillDataSetold(ByRef m_ds As DataSet, ByVal m_dt As DataTable, ByVal table_name As String) As Boolean
        GmFillDataSetold = True
        If IsNothing(m_dt) Then
            Return False
        End If
        Try
            If Not IsNothing(m_ds.Tables.Item(table_name)) Then
                m_ds.Tables.Remove(table_name)
                m_ds.Tables.Add(m_dt)
            Else
                m_ds.Tables.Add(m_dt)
            End If
            Return True
        Catch ex As Exception
            MsgBox("GmFillDataSet" & ex.Message, , "GmError")
        End Try
        'Return m_ds
    End Function
    Friend Shared Function GmFillDataSet(ByVal m_ds As DataSet, ByVal m_dt As DataTable, ByVal table_name As String) As DataSet
        Dim ds As DataSet = m_ds
        Try
            If Not IsNothing(m_dt) AndAlso Not IsNothing(m_ds.Tables(table_name)) Then
                ds.Tables.Remove(table_name)
                ds.Tables.Add(m_dt)
            Else
                ds.Tables.Add(m_dt)
            End If
        Catch ex As Exception
            MsgBox("GmFillDataSet" & ex.Message, , "GmError")
        End Try
        Return ds
    End Function
    Friend Shared Function GetTableSQL(conn As String, CmdType As CommandType, CommandText As String, Optional cmd As SqlCommand = Nothing, Optional TableName As String = "Table1") As DataTable
        Dim ds As New DataTable
        ds.TableName = TableName
        Using cnn As New SqlConnection(conn)
            cnn.Open()
            Try
                If IsNothing(cmd) Then
                    cmd = cnn.CreateCommand()
                End If

                cmd.Connection = cnn
                cmd.CommandType = CmdType : cmd.CommandTimeout = 360
                cmd.CommandText = CommandText ' "FetchWhouses"

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(ds)
                End Using
            Catch ex As SqlException
                MsgBox("SQL Error: " + ex.Message)
            Catch e As Exception
                MsgBox("Error: 1" & e.Message & vbCrLf & e.Source & e.StackTrace)
            End Try
            cnn.Close()
        End Using

        Return ds

    End Function
    Friend Shared Function GetDataSetSQL(conn As String, CmdType As CommandType, CommandText As String, Optional cmd As SqlCommand = Nothing, Optional TableName As String = "Table1") As DataSet
        Dim dtb As New DataSet
        'dtb.TableName = TableName
        Using cnn As New SqlConnection(conn)
            cnn.Open()
            Try
                If IsNothing(cmd) Then
                    cmd = cnn.CreateCommand()
                End If

                cmd.Connection = cnn
                cmd.CommandType = CmdType : cmd.CommandTimeout = 360
                cmd.CommandText = CommandText ' "FetchWhouses"

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtb)
                End Using
            Catch ex As SqlException
                MsgBox("SQL Error: " + ex.Message)
            Catch e As Exception
                MsgBox("Error: 1" & e.Message & vbCrLf & e.Source & e.StackTrace)
            End Try
            cnn.Close()
        End Using

        Return dtb

    End Function
    Public Function GmFillTable(ByVal select_statement As String, ByVal table_name As String) As DataTable
        Using conn As DbConnection = mdataFactory.CreateConnection()
            'mConnection = mdataFactory.CreateConnection()
            'Dim conn As DbConnection = mdataFactory.CreateConnection()
            conn.ConnectionString = mConnectionString
            Dim da_Sql As DbDataAdapter = mdataFactory.CreateDataAdapter()
            da_Sql.SelectCommand = conn.CreateCommand()
            da_Sql.SelectCommand.CommandText = select_statement
            da_Sql.SelectCommand.CommandTimeout = mTimeOut
            'The Fill method retrieves rows from the data source using the SELECT statement specified by an associated SelectCommand property.
            'The connection object associated with the SELECT statement must be valid, but it does not need to be open. 
            'If the connection is closed before Fill is called, it is opened to retrieve data, then closed. 
            'If the connection is open before Fill is called, it remains open.
            'mConnection.Open()
            ' Map the default table name "Table" to
            ' the table's real name.
            GmFillTable = New DataTable
            GmFillTable.TableName = table_name
            ' Load the Table.
            Try
                da_Sql.Fill(GmFillTable)
            Catch ex As SqlException
                If ex.Number = 2 Then
                    MsgBox("Προσοχή !!!. Διακοπή Σύνδεσης", MsgBoxStyle.Critical)
                Else
                    MsgBox("aa " & ex.Message)
                End If
                GmFillTable = Nothing
            End Try
        End Using
    End Function
    Public Function GmFillTableReader(ByVal select_statement As String, ByVal table_name As String) As DataTable
        GmFillTableReader = Nothing
        Try
            'mConnection.Open()
            ' Use an SqlCommand.
            'Dim command As New SqlClient.SqlCommand ' = mConnection.CreateCommand()
            Dim command As DbCommand = mdataFactory.CreateCommand '.Connection.CreateCommand()
            If IsNothing(mConnection) Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
                mConnection.Open()
            ElseIf mConnection.State = ConnectionState.Closed Then
                mConnection.Open()
            End If
            command.Connection = mConnection
            command.CommandText = select_statement
            command.Transaction = mtransaction

            ' Execute the command.
            command.CommandTimeout = mTimeOut

            Dim da_Sql As DbDataAdapter = mdataFactory.CreateDataAdapter()
            da_Sql.SelectCommand = command 'mConnection.CreateCommand()
            'da_Sql.SelectCommand.CommandText = select_statement
            'da_Sql.SelectCommand.CommandTimeout = mTimeOut
            'da_Sql.SelectCommand.Transaction = mtransaction
            'The Fill method retrieves rows from the data source using the SELECT statement specified by an associated SelectCommand property.
            'The connection object associated with the SELECT statement must be valid, but it does not need to be open. 
            'If the connection is closed before Fill is called, it is opened to retrieve data, then closed. 
            'If the connection is open before Fill is called, it remains open.
            'mConnection.Open()
            ' Map the default table name "Table" to
            ' the table's real name.
            GmFillTableReader = New DataTable
            GmFillTableReader.TableName = table_name
            ' Load the Table.
            da_Sql.Fill(GmFillTableReader)
        Catch ex As Exception
            MsgBox(ex.Message)
            GmFillTableReader = Nothing
        End Try
        If IsNothing(mtransaction) Then
            mConnection.Close()
        End If
    End Function
    Public Function GmSave(ByVal dt As DataTable) As Boolean
        GmSave = True
        Try
            mdata_row_state = 0
            ' DELETE.
            mdt = dt.GetChanges(DataRowState.Deleted)
            If Not (mdt Is Nothing) Then
                mdata_row_state = DataRowState.Deleted
                SaveData()
                'Exit Function
            End If
            ' INSERT.
            mdt = dt.GetChanges(DataRowState.Added)
            If Not (mdt Is Nothing) Then
                mdata_row_state = DataRowState.Added
                SaveData()
                mdtChanges = mdt
                'Exit Function
            End If
            ' UPDATE.
            mdt = dt.GetChanges(DataRowState.Modified)
            If Not (mdt Is Nothing) Then
                mdata_row_state = DataRowState.Modified
                SaveData()
                'Exit Function
            End If
        Catch ex As SqlClient.SqlException
            OkTransaction = False
            Dim i As Integer
            Dim errorMessages As New StringBuilder()
            For i = 0 To ex.Errors.Count - 1
                errorMessages.Append("Index #" & i.ToString() & ControlChars.NewLine _
                    & "Message: " & ex.Errors(i).Message & ControlChars.NewLine _
                    & "LineNumber: " & ex.Errors(i).LineNumber & ControlChars.NewLine _
                    & "Source: " & ex.Errors(i).Source & ControlChars.NewLine _
                    & "Procedure: " & ex.Errors(i).Procedure & ControlChars.NewLine)
            Next i
            'Console.WriteLine(errorMessages.ToString())
            MsgBox(errorMessages.ToString()) 'ex.Message)
        End Try
        newsSQLValue = ""
        GmSave = OkTransaction
    End Function
    Public Function GmStartTransaction() As Boolean
        'msysDB = "System.Data.SqlClient" '"System.Data.OleDb" '"MSAccess"
        'mConnectionString = CONNECT_STRING
        'mdataFactory = DbProviderFactories.GetFactory(msysDB)
        If msysDB = "System.Data.SqlClient" Then
            mConnection = mdataFactory.CreateConnection()
        Else
            mConnection = New OleDb.OleDbConnection
        End If
        mConnection.ConnectionString = mConnectionString
        mConnection.Open()
        mtransaction = mConnection.BeginTransaction(IsolationLevel.ReadCommitted)
        OkTransaction = True
    End Function
    Public Function GmStopTransaction() As Boolean
        If OkTransaction = True Then
            Try
                If Not mtransaction Is Nothing Then
                    mtransaction.Commit()
                End If
                ' Mark the records as unmodified.
                'm_DataSet.AcceptChanges()
            Catch exc As Exception
                ' Try to rollback the transaction
                Try
                    mtransaction.Rollback()
                    MsgBox("Προσοχή...Ακύρωση όλων των τελευταίων καταχωρήσεων", MsgBoxStyle.Critical, "Rollback")
                Catch
                    ' Do nothing here; transaction is not active.
                    MsgBox(exc.Message)
                End Try
                MsgBox("Προσοχή...Χρειάζονται όλα τα Fields του Changes και του sSQL για να δουλέψει το command_builder" & vbCrLf & exc.Message + "   Rollback")
            End Try
        Else
            ' Try to rollback the transaction
            Try
                mtransaction.Rollback()
                MsgBox("Προσοχή...Ακύρωση όλων των τελευταίων καταχωρήσεων", MsgBoxStyle.Critical, "Rollback")
            Catch exc As Exception
                ' Do nothing here; transaction is not active.
                MsgBox(exc.Message)
            End Try
        End If
        mtransaction = Nothing
        mConnection.Close()
        mConnection = Nothing
    End Function
    Sub GmClose()
        Try
            If Not IsNothing(mtransaction) Then
                mtransaction.Commit()
            End If
            ' Mark the records as unmodified.
            'm_DataSet.AcceptChanges()
        Catch exc As Exception
            ' Try to rollback the transaction
            Try
                mtransaction.Rollback()
                'm_DataSet.RejectChanges()
            Catch
                ' Do nothing here; transaction is not active.
                MsgBox(exc.Message)
            End Try
            MsgBox("Προσοχή...Χρειάζονται όλα τα Fields του Changes και του sSQL για να δουλέψει το command_builder" & ControlChars.CrLf & exc.Message + "   Rollback")
        Finally
            mConnection.Close()
            mConnection.Dispose()
            mConnection = Nothing
        End Try
    End Sub

    Sub GmOpen()
        Try
            If IsNothing(mConnection) Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
                mConnection.Open()
            ElseIf mConnection.State = ConnectionState.Closed Then
                mConnection.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private newSCOPE_IDENTITYFieldValue As String
    Public Property GmSCOPE_IDENTITYField() As String
        Get
            Return newSCOPE_IDENTITYFieldValue
        End Get
        Set(ByVal value As String)
            newSCOPE_IDENTITYFieldValue = value
        End Set
    End Property


    Private newsSQLValue As String
    Public Property GmsSQL() As String
        Get
            Return newsSQLValue
        End Get
        Set(ByVal value As String)
            newsSQLValue = value
        End Set
    End Property
    Private Sub SaveData()
        'Προσοχή...Χρειάζονται όλα τα Fields του Changes και του sSQL για να δουλέψει το command_builder // megisto 97
        'Προσοχή...Στα size των πεδίων
        If newsSQLValue = "" Then
            newsSQLValue = "SELECT * FROM " & mdt.TableName        'sSQL = "SELECT * FROM PEL1"
        End If
        If msysDB = "System.Data.SqlClient" Then
            Dim cmd As DbCommand = mdataFactory.CreateCommand()
            cmd.Connection = mConnection
            cmd.CommandText = newsSQLValue
            cmd.Transaction = mtransaction
            cmd.CommandTimeout = mTimeOut

            Dim da_Sql As DbDataAdapter = mdataFactory.CreateDataAdapter()
            ' Create INSERT, UPDATE, and DELETE commands.
            Dim myCommandBuilder As DbCommandBuilder = mdataFactory.CreateCommandBuilder() ' = da_Sql
            da_Sql.SelectCommand = cmd
            myCommandBuilder.DataAdapter = da_Sql
            Select Case mdata_row_state
                Case DataRowState.Modified
                    myCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
                    da_Sql.UpdateCommand = myCommandBuilder.GetUpdateCommand(True)
                    Dim p As SqlClient.SqlParameterCollection = da_Sql.UpdateCommand.Parameters
                Case DataRowState.Added
                    Dim cmdBuilder As IDbCommand = myCommandBuilder.GetInsertCommand(True)
                    Dim p As SqlClient.SqlParameterCollection = cmdBuilder.Parameters
                    Dim cmdBuilderText As String = myCommandBuilder.GetInsertCommand.CommandText
                    Dim SCOPE_IDENTITYField = newSCOPE_IDENTITYFieldValue
                    If SCOPE_IDENTITYField = "" Then
                        SCOPE_IDENTITYField = 0
                    End If
                    cmdBuilder.CommandText = cmdBuilderText & "; SELECT * FROM " & mdt.TableName & " WHERE " & mdt.Columns(SCOPE_IDENTITYField).ColumnName & " = SCOPE_IDENTITY()"
                    cmdBuilder.UpdatedRowSource = UpdateRowSource.Both
                    myCommandBuilder.RefreshSchema()
                    da_Sql.InsertCommand = cmdBuilder
                Case DataRowState.Deleted
                    myCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges
                    da_Sql.DeleteCommand = myCommandBuilder.GetDeleteCommand(True)
            End Select
            Try
                Try
                    da_Sql.Update(mdt)
                Catch ex As DBConcurrencyException
                    MsgBox("Concurrency Exception ID = " & ex.Row(0).ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Concurrency Exception")
                    OkTransaction = False
                End Try
            Catch e As SqlException
                Dim msg As New StringBuilder
                msg.Append("An exception was thrown.")
                'Console.WriteLine("An exception was thrown.")
                msg.Append(e.Message)
                'Console.WriteLine(e.Message)
                If Not (e.Data Is Nothing) Then
                    msg.Append("  Extra details:")
                    'Console.WriteLine("  Extra details:")
                    Dim de As DictionaryEntry
                    For Each de In e.Data
                        msg.Append("    The key is " & de.Key & " and the value is: " & de.Value)
                        'Console.WriteLine("    The key is '{0}' and the value is: {1}", de.Key, de.Value)
                    Next de
                End If
                newExceptionValue = e
                MsgBox(msg.ToString)
                OkTransaction = False
            End Try
        Else
            Dim da_OleDb As New OleDb.OleDbDataAdapter(newsSQLValue, mConnection) ' mConnectionString)
            da_OleDb.SelectCommand.Transaction = mtransaction
            Dim command_builderOleDb As New OleDb.OleDbCommandBuilder(da_OleDb)
            Select Case mdata_row_state
                Case DataRowState.Modified
                    da_OleDb.UpdateCommand = command_builderOleDb.GetUpdateCommand()
                Case DataRowState.Added
                    da_OleDb.InsertCommand = command_builderOleDb.GetInsertCommand.Clone
                    AddHandler da_OleDb.RowUpdated, AddressOf RowUpdatedHandler
                Case DataRowState.Deleted
                    da_OleDb.DeleteCommand = command_builderOleDb.GetDeleteCommand()
            End Select
            da_OleDb.Update(mdt)
        End If
    End Sub
    Private newExceptionValue As Exception
    Public ReadOnly Property GmSaveDataException() As Exception
        Get
            Return newExceptionValue
        End Get
    End Property

    Private Sub RowUpdatedHandler(ByVal sender As Object, ByVal e As OleDb.OleDbRowUpdatedEventArgs)
        If e.Status = UpdateStatus.[Continue] AndAlso (e.StatementType = StatementType.Insert) Then
            Dim CommandRefresh As New OleDb.OleDbCommand '("SELECT @@IDENTITY", mConnection)
            CommandRefresh.Connection = mConnection
            CommandRefresh.Transaction = mtransaction
            CommandRefresh.CommandText = "SELECT @@IDENTITY"
            e.Row(mdt.Columns(0).ColumnName) = CInt(CommandRefresh.ExecuteScalar)
            e.Row.AcceptChanges()
        End If
    End Sub

    Public Property DataFactory() As DbProviderFactory
        Get
            Return mdataFactory
        End Get
        Set(ByVal value As DbProviderFactory)
            mdataFactory = value
            If Not mdataFactory Is Nothing Then
                mdataFactory = DbProviderFactories.GetFactory(msysDB)
            End If
        End Set
    End Property
    Public Property GmChanges() As DataTable
        Get
            Return mdtChanges
        End Get
        Set(ByVal value As DataTable)
            mdtChanges = value
        End Set

    End Property
    Public Property GmConnection() As DbConnection
        Get
            Return mConnection
        End Get
        Set(ByVal value As DbConnection)
            mConnection = value
            If mConnection Is Nothing Then
                mConnection = mdataFactory.CreateConnection()
                mConnection.ConnectionString = mConnectionString
            End If
        End Set
    End Property
    Public WriteOnly Property GmConnectionString() As String
        Set(ByVal value As String)
            mConnectionString = value
            If mConnectionString <> "" Then
                'GmConnection = mConnection
                'mConnection.ConnectionString = New mConnectionString
                ' Set the Connection to the DbConnection.
                'mdataFactory = DbProviderFactories.GetFactory(msysDB)
                'mConnection = mdataFactory.CreateConnection()
                'mConnection.ConnectionString = mConnectionString
                'mConnection.Open()
                'mtransaction = mConnection.BeginTransaction()
                msysDB = "System.Data.SqlClient"
                mdataFactory = DbProviderFactories.GetFactory(msysDB)
            End If
        End Set
    End Property
    Public Property GmDataRowState() As DataRowState
        Get
            Return mdata_row_state
        End Get
        Set(ByVal value As DataRowState)
            mdata_row_state = value
        End Set
    End Property
    Public WriteOnly Property GmSysDB() As String
        Set(ByVal value As String)
            msysDB = value
            If msysDB = "" Then
                msysDB = "System.Data.SqlClient" '"System.Data.OleDb" '"MSAccess"
            End If
        End Set
    End Property
    Public Property GmTransaction() As DbTransaction
        Get
            Return mtransaction
        End Get
        Set(ByVal value As DbTransaction)
            mtransaction = value
        End Set
    End Property
    Private OkTransaction As Boolean
    Public Property GmOkTransaction() As Boolean
        Get
            Return OkTransaction
        End Get
        Set(ByVal value As Boolean)
            OkTransaction = value
        End Set
    End Property
    Private notCloseConnection As Boolean
    Public Property GmNotCloseConnection() As Boolean
        Get
            Return notCloseConnection
        End Get
        Set(ByVal value As Boolean)
            notCloseConnection = value
        End Set
    End Property
    Private mTimeOut As Integer = 30
    Public Property GmTimeOut() As Integer
        Get
            Return mTimeOut
        End Get
        Set(ByVal value As Integer)
            mTimeOut = value
        End Set
    End Property
    Private newParameterValue As DbParameterCollection
    Public Property GmParameter() As DbParameterCollection
        Get
            Return newParameterValue
        End Get
        Set(ByVal value As DbParameterCollection)
            newParameterValue = value
        End Set
    End Property

End Class