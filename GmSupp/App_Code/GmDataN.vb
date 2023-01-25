Imports System.Data.SqlClient

Public Class GmDataN

    Friend Shared Function GmFillDataSet(ByVal constr As String, ByVal m_ds As DataSet, ByVal m_dt As DataTable, ByVal table_name As String) As DataSet
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

    Public Shared Function GmFillTable(ByVal constr As String, ByVal select_statement As String, ByVal table_name As String) As DataTable
        Dim dt As New DataTable()
        dt.TableName = table_name
        Dim sql As String = select_statement ' "SELECT * FROM Customers"
        Using conn As New SqlConnection(constr)
            Using cmd As New SqlCommand(sql)
                cmd.Connection = conn
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
        'Using conn As DbConnection = mdataFactory.CreateConnection()
        '    'mConnection = mdataFactory.CreateConnection()
        '    'Dim conn As DbConnection = mdataFactory.CreateConnection()
        '    conn.ConnectionString = mConnectionString
        '    Dim da_Sql As DbDataAdapter = mdataFactory.CreateDataAdapter()
        '    da_Sql.SelectCommand = conn.CreateCommand()
        '    da_Sql.SelectCommand.CommandText = select_statement
        '    da_Sql.SelectCommand.CommandTimeout = mTimeOut
        '    'The Fill method retrieves rows from the data source using the SELECT statement specified by an associated SelectCommand property.
        '    'The connection object associated with the SELECT statement must be valid, but it does not need to be open. 
        '    'If the connection is closed before Fill is called, it is opened to retrieve data, then closed. 
        '    'If the connection is open before Fill is called, it remains open.
        '    'mConnection.Open()
        '    ' Map the default table name "Table" to
        '    ' the table's real name.
        '    GmFillTable = New DataTable
        '    GmFillTable.TableName = table_name
        '    ' Load the Table.
        '    Try
        '        da_Sql.Fill(GmFillTable)
        '    Catch ex As SqlException
        '        If ex.Number = 2 Then
        '            MsgBox("Προσοχή !!!. Διακοπή Σύνδεσης", MsgBoxStyle.Critical)
        '        Else
        '            MsgBox("aa " & ex.Message)
        '        End If
        '        GmFillTable = Nothing
        '    End Try
        'End Using
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
End Class
