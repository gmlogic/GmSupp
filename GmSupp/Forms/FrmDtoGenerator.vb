Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class FrmDtoGenerator

    Private Sub FrmDtoGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtConnection.Text = My.Settings.ReveraConnectionString
        'txtConnection.ReadOnly = True
        cmbSourceType.SelectedIndex = 0   ' Table
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        If txtSourceName.Text.Trim = "" Then
            MessageBox.Show("Δώσε όνομα Table / View / Stored Procedure")
            Exit Sub
        End If

        Try
            Dim code As String = ""

            Select Case cmbSourceType.SelectedItem.ToString()
                Case "Table", "View"
                    code = GenerateFromTableOrView(
                        txtConnection.Text,
                        txtSourceName.Text.Trim(),
                        txtClassName.Text.Trim()
                    )

                Case "Stored Procedure"
                    code = GenerateFromStoredProcedure(
                        txtConnection.Text,
                        txtSourceName.Text.Trim(),
                        txtClassName.Text.Trim()
                    )
            End Select

            txtOutput.Text = code

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        If txtOutput.Text <> "" Then
            Clipboard.SetText(txtOutput.Text)
            MessageBox.Show("Copied to clipboard ✔")
        End If
    End Sub

    ' =========================
    ' TABLE / VIEW
    ' =========================
    Private Function GenerateFromTableOrView(
        connStr As String,
        sourceName As String,
        className As String) As String

        Dim sb As New StringBuilder()

        Using cn As New SqlConnection(connStr)
            cn.Open()

            Dim sql =
                "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE " &
                "FROM INFORMATION_SCHEMA.COLUMNS " &
                "WHERE TABLE_NAME = @NAME " &
                "ORDER BY ORDINAL_POSITION"

            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@NAME", sourceName)

                Using rd = cmd.ExecuteReader()

                    Dim finalClass =
                        If(className = "",
                           sourceName & "Dto",
                           className)

                    sb.AppendLine("Public Class " & finalClass)

                    While rd.Read()
                        Dim colName = rd("COLUMN_NAME").ToString()
                        Dim sqlType = rd("DATA_TYPE").ToString()
                        Dim isNullable = rd("IS_NULLABLE").ToString() = "YES"

                        sb.AppendLine("    Public Property " &
                                      colName & " As " &
                                      MapType(sqlType, isNullable))
                    End While

                    sb.AppendLine("End Class")
                End Using
            End Using
        End Using

        Return sb.ToString()
    End Function

    ' =========================
    ' STORED PROCEDURE
    ' =========================
    Private Function GenerateFromStoredProcedure(
        connStr As String,
        spName As String,
        className As String) As String

        Dim sb As New StringBuilder()

        Using cn As New SqlConnection(connStr)
            Using cmd As New SqlCommand("sys.sp_describe_first_result_set", cn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@tsql", "EXEC " & spName)

                cn.Open()

                Using rd = cmd.ExecuteReader()

                    Dim finalClass =
                        If(className = "",
                           spName.Replace(".", "_") & "Dto",
                           className)

                    sb.AppendLine("Public Class " & finalClass)

                    While rd.Read()
                        If rd("name") Is DBNull.Value Then Continue While

                        Dim colName = rd("name").ToString()
                        Dim sqlType = rd("system_type_name").ToString()
                        Dim isNullable = CBool(rd("is_nullable"))

                        sb.AppendLine("    Public Property " &
                                      colName & " As " &
                                      MapType(sqlType, isNullable))
                    End While

                    sb.AppendLine("End Class")
                End Using
            End Using
        End Using

        Return sb.ToString()
    End Function

    ' =========================
    ' TYPE MAPPING
    ' =========================
    Private Function MapType(sqlType As String, isNullable As Boolean) As String

        Dim t = sqlType.ToLower()
        Dim vbType As String

        If t.Contains("smallint") Then
            vbType = "Short"
        ElseIf t.Contains("bigint") Then
            vbType = "Long"
        ElseIf t.Contains("int") Then
            vbType = "Integer"
        ElseIf t.Contains("bit") Then
            vbType = "Boolean"
        ElseIf t.Contains("float") Then
            vbType = "Double"
        ElseIf t.Contains("decimal") OrElse t.Contains("numeric") OrElse t.Contains("money") Then
            vbType = "Decimal"
        ElseIf t.Contains("date") OrElse t.Contains("time") Then
            vbType = "Date"
        Else
            vbType = "String"
        End If

        If vbType <> "String" AndAlso isNullable Then
            vbType &= "?"
        End If

        Return vbType
    End Function

End Class
