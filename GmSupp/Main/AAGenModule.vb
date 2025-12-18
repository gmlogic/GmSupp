Imports VB = Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Text
Imports System.Linq
Module AAGenModule
    'Private Const CONNECT_STRING = _
    '    "data source=127.0.0.1;initial catalog=aluset;user id=sa"
    'Public Const CONNECT_STRING As String = _
    '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Gmapps.NET\GmWinNet\GmWin09.mdb;Persist Security Info=False"
    'Public Const CONNECT_STRING As String = _
    '"data source=192.168.111.90\S1MODEL;Initial Catalog=aluset;User ID=sa"
    ' DataAdapters.
    'Public m_daSqlGen As SqlDataAdapter
    'Public m_daOleDbGen As OleDbDataAdapter

    ' Objects that work with the database.
    Public m_DataSet As New DataSet
    ' DataTables.
    Public m_dvTemp As DataView

    Public Const GmYellow As Integer = &HC0FFFF
    Public Const CancelAction As Short = 0
    Public Const AddRecord As Short = 1
    Public Const ChangeRecord As Short = 2
    Public Const DeleteRecord As Short = 3
    Public Const SelectRecord As Short = 4
    Public Const Apo As Short = 1
    Public Const Pel As Short = 2
    Public Const Pro As Short = 3
    Public Const TimPel As Short = 4
    Public Const TimPro As Short = 5
    Public Enum Action As Short
        CancelAction = 0
        AddRecord = 1
        ChangeRecord = 2
        DeleteRecord = 3
        SelectRecord = 4
    End Enum

    Public sysProvider As String
    Public sysDB As String = "System.Data.SqlClient"
    Public sysDSN As String
    Public sysConnect As String
    Public CPath As String
    Public CFROM As String
    Public CTo As String
    Public sSQL As String
    Public exSQL As String
    Public RsFields As String
    Public RsTables As String
    Public RsWhere As String
    Public RsGroup As String
    Public RsOrder As String
    Public TmpName As String
    Public Msg As String
    Public CONNECT_STRING As String = _
"data source=192.168.111.90\S1MODEL;Initial Catalog=aluset;User ID=sa"
    Public StartBegin As Byte
    Public OptionNo As Byte
    Public TypeID As Byte
    Public UpdateMode As Byte
    'Public Action As Byte
    Public LookUpAction As Byte '?
    Public TmpAction As Byte '?
    Public TProgr As Byte
    Public PelPro As Byte
    Public RoundPoint As Byte
    Public ExitForm As Byte

    'False = 0 True = -1 ,<> 0
    Public TmpValidate As Boolean '?
    Public MsgCancel As Boolean
    Public FirstTime As Boolean
    Public txtChange As Boolean 'MAIN
    Public TError As Boolean
    Public PrProperties(6) As Boolean
    Public FrmUnload As Boolean

    Public StartDate As Date
    Public CTODate As Date = Now()
    Public DFrom As Date
    Public Dto As Date

    Public FKk As Short
    Public TKk As Short

    Public Company As Short = 0
    Public CompName As String '= "Hglp"
    Public LocalIP As String
    Public S1Path As String = ""
    Public SERVER As String = ""
    Public DATABASE As String = ""

    Public IdNo As Integer
    Public IdNo01 As Integer
    Public LookUpID As Integer
   Public RCount As Integer

    Public BAROS As Double
    Public LookUpDataRowView As DataRowView
    Public m_dtGen As DataTable
    Public m_dvGen As DataView
    Public LogSQL As String = String.Empty
    Public InitialCatalog As String = ""
    Public CurUser As String
    Public UserId As Integer
    Public CurPass As String
    Public Facilities As String

    Public GUser As New Revera.USER

    Public CurUserRole As String
    Public curGmEntityUser As GmIdentityUser ' Microsoft.AspNet.Identity.EntityFramework.IdentityUser = Nothing
    Public curRoleStore As New Microsoft.AspNet.Identity.EntityFramework.RoleStore(Of Microsoft.AspNet.Identity.EntityFramework.IdentityRole) '= Nothing
    Public RoleManagerStore As New Microsoft.AspNet.Identity.EntityFramework.RoleStore(Of Microsoft.AspNet.Identity.EntityFramework.IdentityRole)
    Public RoleManager As New Microsoft.AspNet.Identity.RoleManager(Of Microsoft.AspNet.Identity.EntityFramework.IdentityRole)(RoleManagerStore)
    Public UserManagerStore As New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of Microsoft.AspNet.Identity.EntityFramework.IdentityUser)
    Public UserManager As New Microsoft.AspNet.Identity.UserManager(Of Microsoft.AspNet.Identity.EntityFramework.IdentityUser)(UserManagerStore) 'New Microsoft.AspNet.Identity.EntityFramework.UserStore(Of Microsoft.AspNet.Identity.EntityFramework.IdentityUser))

    Public clientID As String = Nothing
    Public dep As TableDependency.SqlClient.SqlTableDependency(Of Utility.triggeredClass) = Nothing

    Sub Main()
        ' Declare variables.
        'sysDB = "MSAccess"
        'sysDB = "MSSQL"
        Dim separators As String = " "
        Dim commands As String = Microsoft.VisualBasic.Command()
        Dim args() As String = commands.Split(separators.ToCharArray)
        'gmnotes If Trim(commands) = "" Then
        '    'Dim AAGen_Menu As New AAGen_Menu
        '    'AAGen_Menu.ShowDialog()
        '    Sta5BRold.ShowDialog()
        'Else
        '    Select Case args(0) ' Evaluate Number.
        '        Case "TIM1"
        '            Sta5BRold.ShowDialog()
        '        Case "PEL1"
        '            Sta5BRold.ShowDialog()
        '        Case Else   ' Other values.
        '            'Dim Apo1Fr As New APO1FR
        '            'Apo1Fr.ShowDialog()
        '    End Select
        'End If
        ' The user wants to exit the application. Close everything down.
        Application.Exit()
    End Sub
    ' Load a table into the DataSet.
    Public Sub LoadTable(ByVal sysDB As String, ByVal m_DataSet As DataSet, ByVal table_name As String, _
        ByVal select_statement As String, ByVal child_column_name As String, ByVal parent_table_name As String, _
        ByVal parent_column_name As String, ByRef data_table As DataTable, _
        ByVal Action As Integer)
        Select Case Trim(sysDB)
            Case "MSSQL"
                Dim SqlData_adapter As New SqlDataAdapter( _
                    select_statement, CONNECT_STRING)
                If Action <> SelectRecord Then
                    ' Create INSERT, UPDATE, and DELETE commands.
                    ' Create INSERT, UPDATE, and DELETE commands.
                    Dim command_builder As New SqlCommandBuilder(SqlData_adapter)
                    '@
                    Try
                        Select Case Action
                            Case AddRecord
                                SqlData_adapter.InsertCommand = command_builder.GetInsertCommand()
                            Case ChangeRecord
                                SqlData_adapter.UpdateCommand = command_builder.GetUpdateCommand()
                            Case DeleteRecord
                                SqlData_adapter.DeleteCommand = command_builder.GetDeleteCommand()
                        End Select
                    Catch exc As Exception
                        MsgBox(exc.Message)
                    End Try
                End If
                '@
                Try
                    ' Map the default table name "Table" to 
                    ' the table's real name.
                    SqlData_adapter.TableMappings.Add("Table", table_name)
                    ' Load the DataSet.

                    SqlData_adapter.Fill(m_DataSet)
                    ' Save a reference to the new table.
                    data_table = m_DataSet.Tables(table_name)

                    ' Connect the tables with a foreign key constraint.
                    If child_column_name.Length > 0 Then
                        Dim parent_table As DataTable = m_DataSet.Tables(parent_table_name)
                        Dim foreign_key As New ForeignKeyConstraint( _
                            parent_table.Columns(parent_column_name), _
                            data_table.Columns(child_column_name))
                        data_table.Constraints.Add(foreign_key)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case "MSAccess"
                Dim OleDbData_adapter As New OleDbDataAdapter( _
                    select_statement, CONNECT_STRING)
                If Action <> SelectRecord Then
                    ' Create INSERT, UPDATE, and DELETE commands.
                    Dim command_builder As New OleDbCommandBuilder(OleDbData_adapter)
                    '@
                    Try
                        Select Case Action
                            Case AddRecord
                                OleDbData_adapter.InsertCommand = command_builder.GetInsertCommand()
                            Case ChangeRecord
                                OleDbData_adapter.UpdateCommand = command_builder.GetUpdateCommand()
                            Case DeleteRecord
                                OleDbData_adapter.DeleteCommand = command_builder.GetDeleteCommand()
                        End Select
                    Catch exc As Exception
                        MsgBox(exc.Message)
                    End Try
                End If
                Try
                    ' Map the default table name "Table" to 
                    ' the table's real name.
                    OleDbData_adapter.TableMappings.Add("Table", table_name)

                    ' Load the DataSet.
                    OleDbData_adapter.Fill(m_DataSet)

                    ' Save a reference to the new table.
                    data_table = m_DataSet.Tables(table_name)

                    ' Connect the tables with a foreign key constraint.
                    If child_column_name.Length > 0 Then
                        Dim parent_table As DataTable = m_DataSet.Tables(parent_table_name)
                        Dim foreign_key As New ForeignKeyConstraint( _
                            parent_table.Columns(parent_column_name), _
                            data_table.Columns(child_column_name))
                        data_table.Constraints.Add(foreign_key)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
    End Sub

    Function FillTable(ByVal sysDB As String, ByVal table_name As String, _
        ByVal select_statement As String, ByRef data_table As DataTable, ByVal Action As Integer) As Boolean
        FillTable = True

        Select Case Trim(sysDB)
            Case "MSSQL"
                Dim SqlData_adapter As New SqlDataAdapter( _
                     select_statement, CONNECT_STRING)
                Try
                    ' Map the default table name "Table" to 
                    ' the table's real name.
                    data_table = New DataTable
                    data_table.TableName = table_name
                    ' Load the Table.
                    SqlData_adapter.Fill(data_table)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Case "MSAccess"
                Try

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
        End Select
    End Function


    Function Transform(ByVal Stuff As Object, ByVal SType As String) As Object    'Object
        'Boolean  System.Boolean  2 bytes  True or False.  
        'Byte  System.Byte  1 byte  0 through 255 (unsigned).  
        'Char  System.Char  2 bytes  0 through 65535 (unsigned).  
        'Date  System.DateTime  8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.  
        'Decimal  System.Decimal  16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
        '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
        '+/-0.0000000000000000000000000001 (+/-1E-28).  
        'Double 
        '(double-precision floating-point)  System.Double  8 bytes  -1.79769313486231570E+308 through 
        '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
        'Integer  System.Int32  4 bytes  -2,147,483,648 through 2,147,483,647.  
        'Long 
        '(long integer)  System.Int64  8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
        'Object  System.Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
        'Short  System.Int16  2 bytes  -32,768 through 32,767.  
        'Single 
        '(single-precision floating-point)  System.Single  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
        'String 
        '(variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
        'User-Defined Type 
        '(structure)  (inherits from System.ValueType)  Depends on implementing platform  Each member of the structure has a range determined by its data type and independent of the ranges of the other members.  

        'VarType (varname)
        Dim FieldType As String
        If IsDBNull(Stuff) Then
            Transform = 0
            Select Case SType
                Case "System.Boolean" '2 bytes  True or False
                    FieldType = "System.Boolean"
                Case "System.Byte"  '1 byte  0 through 255 (unsigned).
                    FieldType = "System.Byte"
                Case "System.Char"  '2 bytes  0 through 65535 (unsigned).
                    FieldType = "System.Char"
                    Transform = ""
                Case "System.DateTime"  '8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.
                    FieldType = "System.DateTime"
                    Transform = CTODate '"99991231"
                Case "System.Decimal"  '16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
                    '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
                    '+/-0.0000000000000000000000000001 (+/-1E-28).  
                    FieldType = "System.Decimal"
                Case "System.Double"  '8 bytes  -1.79769313486231570E+308 through 
                    '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
                    FieldType = "System.Double"
                Case "System.Int32"    'Integer4 bytes  -2,147,483,648 through 2,147,483,647.  
                    FieldType = "System.Int32"
                Case "System.Int64" '(long integer)8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
                    FieldType = "System.Int64"
                Case "System.Object" 'Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
                    FieldType = "System.Object"
                Case "System.Int16"  'Short    2 bytes  -32,768 through 32,767. 
                    FieldType = "System.Int16"
                Case "System.Single"  'Single (single-precision floating-point)  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
                    FieldType = "System.Single"
                Case "System.String" 'String (variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
                    FieldType = "System.String"
                    Transform = ""
            End Select
        Else
            Transform = Val(Stuff)
            Select Case SType
                Case "System.Boolean" '2 bytes  True or False
                    FieldType = "System.Boolean"
                Case "System.Byte"  '1 byte  0 through 255 (unsigned).
                    FieldType = "System.Byte"
                Case "System.Char"  '2 bytes  0 through 65535 (unsigned).
                    FieldType = "System.Char"
                    Transform = Stuff
                    'Transform = ""
                Case "System.DateTime"  '8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.
                    FieldType = "System.DateTime"
                    'Transform = "99991231"
                Case "System.Decimal"  '16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
                    '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
                    '+/-0.0000000000000000000000000001 (+/-1E-28).  
                    FieldType = "System.Decimal"
                Case "System.Double"  '8 bytes  -1.79769313486231570E+308 through 
                    '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
                    FieldType = "System.Double"
                Case "System.Int32"    'Integer4 bytes  -2,147,483,648 through 2,147,483,647.  
                    FieldType = "System.Int32"
                Case "System.Int64" '(long integer)8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
                    FieldType = "System.Int64"
                Case "System.Object" 'Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
                    FieldType = "System.Object"
                Case "System.Int16"  'Short    2 bytes  -32,768 through 32,767. 
                    FieldType = "System.Int16"
                Case "System.Single"  'Single (single-precision floating-point)  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
                    FieldType = "System.Single"
                Case "System.String" 'String (variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
                    FieldType = "System.String"
                    Transform = Stuff
                    'Case adSmallInt '2
                    '    FieldType = "adSmallInt"
                    'Case adInteger '3
                    '    FieldType = "adInteger"
                    'Case adSingle '4
                    '    FieldType = "adSingle"
                    '    Transform = CCur(Stuff)
                    'Case adDouble '5
                    '    FieldType = "adDouble"
                    '    Transform = CCur(Stuff)
                    'Case adCurrency '6
                    '    FieldType = "adCurrency"
                    'Case adDate '7
                    '    FieldType = "adDate"
                    '    Transform = Stuff
                    'Case adBoolean '11
                    '    FieldType = "adBoolean"
                    '    Transform = Stuff
                    'Case adTinyInt '16
                    '    FieldType = "adTinyInt"
                    'Case adUnsignedTinyInt '17
                    '    FieldType = "adUnsignedTinyInt"
                    '    Transform = Val(Stuff)
                    'Case adUnsignedInt '19
                    '    FieldType = "adUnsignedInt"
                    'Case adGUID '72
                    '    FieldType = "adGuid"
                    'Case adBinary '128
                    '    FieldType = "adBinary"
                    'Case adChar '129 200
                    '    FieldType = "adChar"
                    '    Transform = Stuff
                    '    '        If Transform = "" Then Transform = "_"
                    'Case adWChar '130
                    '    FieldType = "adWChar"
                    '    Transform = Stuff
                    'Case adNumeric '131
                    '    FieldType = "adNumeric"
                    'Case adDBDate  '133
                    '    FieldType = "adDBDate"
                    '    Transform = Stuff
                    'Case adDBTimeStamp '135
                    '    FieldType = "adDBTimeStamp"
                    '    Transform = Stuff
                    'Case adVarChar '200
                    '    FieldType = "adVarChar"
                    '    Transform = Stuff
                    'Case adVarWChar '202
                    '    FieldType = "adVarWChar"
                    '    Transform = Stuff
                    '    '         If Transform = "" Then Transform = "_"
                    'Case adLongVarWChar '203
                    '    FieldType = "adLongVarWChar"
                    '    Transform = Stuff
                    '    '         If Transform = "" Then Transform = "_"
                    'Case adLongVarBinary '205
                    '    FieldType = "adLongVarBinary"
            End Select
        End If
    End Function
    Function RString(ByVal Source As String, ByVal Times As Integer) As _
    String
        Dim i As Integer
        Dim sb As New StringBuilder(Source.Length * Times)
        For i = 1 To Times
            sb.Append(Source)
        Next
        Return sb.ToString
    End Function
    Function TestConn(ByVal conStr As String) As String
        TestConn = Nothing
        Using myConn As New SqlConnection(conStr)
            Try
                myConn.Open()
                TestConn = "Ver: " & myConn.ServerVersion
            Catch ex As Exception
                Dim conn As New System.Data.SqlClient.SqlConnectionStringBuilder
                conn.ConnectionString = conStr
                TestConn = "Error" & vbCrLf &
                       " Data Source=" & conn.DataSource & ";Initial Catalog=" & conn.InitialCatalog & ";User ID=" & conn.UserID & vbCrLf &
                       ex.Message & vbCrLf & ex.StackTrace
            End Try
        End Using
    End Function
    ' Remove unwanted columns. Note that bad_columns
    ' should list the column indexes in descending order.
    Public Sub RemoveGridColumnsByCollection(ByVal dgGen1 As DataGridView, ByVal bad_columns() As Integer, ByVal myArrF() As String, ByVal myArrN() As String, ByVal AllColumns As Boolean)
        Try
            Dim DColumns As New List(Of DataGridViewColumn)
            For Each col As DataGridViewColumn In dgGen1.Columns
                DColumns.Add(col)
            Next
            If AllColumns = True Then
                'Exit Sub
            End If
            dgGen1.Columns.Clear()
            ' Initialize and add a text box column.
            Dim column As DataGridViewColumn
            Dim i As Integer
            If AllColumns = True Then
                For Each col As DataGridViewColumn In DColumns '( '.Table.Columns
                    If col.Name = "Check" Then Continue For
                    Dim dgrcol As New DataGridViewColumn
                    Dim cell As DataGridViewCell = _
                               New DataGridViewTextBoxCell()
                    'cell.Style.BackColor = Drawing.Color.Wheat
                    With dgrcol
                        .HeaderText = col.Name
                        .DataPropertyName = col.Name
                        .Name = col.Name
                        .ValueType = col.ValueType
                        .DefaultCellStyle = New DataGridViewCellStyle
                        .CellTemplate = cell
                    End With
                    'TimKinBr.TIMKINDataGridView.Columns.Add(dgrcol)
                    dgGen1.Columns.Add(dgrcol)
                Next
            Else
                For i = 0 To myArrF.Length - 1 '.GetUpperBound(0)

                    Dim col As DataGridViewColumn = (From c As DataGridViewColumn In DColumns Where c.DataPropertyName.ToUpper = myArrF(i).ToUpper Select c).FirstOrDefault
                    'Dim col As DataGridViewColumn = New DataGridViewColumn With {.Name = myArrF(i), .HeaderText = myArrN(i)}
                    Dim IdxCol As Integer = DColumns.IndexOf(col) 'New DataGridViewColumn With {.Name = myArrF(i), .HeaderText = myArrN(i)})
                    If IdxCol = -1 Then Continue For
                    column = New DataGridViewTextBoxColumn()
                    With column
                        .DataPropertyName = myArrF(i)
                        .Name = myArrN(i)
                        'Console.WriteLine(myArrF(i) & " --- " & myArrN(i))
                        Try
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .DefaultCellStyle.Format = "N2"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
                                     Then
                                    .DefaultCellStyle.Format = "N2"
                                End If
                                If col.ValueType.Name = "String" Then
                                    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                    '.Width = 200
                                End If
                                If col.ValueType.Name <> "String" Then
                                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                End If
                            End If
                        Catch ex As Exception
                            MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
                        End Try
                    End With
                    dgGen1.Columns.Add(column)
                Next
            End If
            With dgGen1
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken
                With (dgGen1.ColumnHeadersDefaultCellStyle)
                    .BackColor = Drawing.Color.Orange '.Navy
                    .ForeColor = Drawing.Color.Blue '.White
                    '.Font = New Font(dgGen1.Font.Name, dgGen1.Font.Size + 2.0F, FontStyle.Bold)
                End With
                .EnableHeadersVisualStyles = False
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' Remove unwanted columns. Note that bad_columns
    ' should list the column indexes in descending order.
    Public Sub RemoveGridColumns(ByRef sender As Object, ByVal bad_columns() As Integer, ByVal myArrF() As String, ByVal myArrN() As String, ByVal DView As DataView, ByVal AllColumns As Boolean)
        If AllColumns = True Then
            Exit Sub
        End If
        Dim dgGen As DataGridView = sender
        Select Case sender.GetType.Name
            Case "DataGridView"
                'dgGen = New DataGridView
            Case "MyDataGridView", "GmDgView"
                'dgGen = New MyDataGridView
                Dim g As Integer = 1
        End Select
        '' Remove the unwanted columns.
        ''For i = 0 To bad_columns.GetUpperBound(0)
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(bad_columns(i))
        ''Next i
        ''For i = 0 To 4
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(0)
        ''Next i
        'While dgGen.Columns.Count <> 0
        '    'If dgGen.Columns(0) <> System.Windows.Forms.DataGridViewCheckBoxColumn.Then Then
        '    'End If
        '    Try
        '        dgGen.Columns.Remove(dgGen.Columns(0)) 'dgTim4_CellValidating

        '    Catch ex As Exception

        '    End Try
        '    'If dgGen.Columns.Count > 0 Then
        '    '    Console.WriteLine(dgGen.Columns(0).DataPropertyName & "----" & dgGen.Columns(0).Name)
        '    'End If
        'End While
        Try
            For i As Integer = 0 To dgGen.Columns.Count - 1
                dgGen.Columns.RemoveAt(0) '.clear()
            Next
        Catch ex As Exception

        End Try
        ' Initialize and add a text box column.
        Dim column As DataGridViewColumn
        For i As Integer = 0 To myArrF.Length - 1 '.GetUpperBound(0)
            column = New DataGridViewTextBoxColumn()
            With column
                .DataPropertyName = myArrF(i)
                .Name = myArrN(i)
                'Console.WriteLine(myArrF(i) & " --- " & myArrN(i))
                Try
                    'DView.Table.Columns(txtWild.Tag).DataType.Name()
                    If DView.Table.Columns(myArrF(i)).DataType.Name = "Double" Then
                        .DefaultCellStyle.Format = "###0.00"
                    End If
                    If DView.Table.Columns(myArrF(i)).DataType.Name <> "String" Then
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Catch ex As Exception

                End Try
            End With
            dgGen.Columns.Add(column)
        Next
        With dgGen
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken
            With (dgGen.ColumnHeadersDefaultCellStyle)
                .BackColor = Drawing.Color.Orange '.Navy
                .ForeColor = Drawing.Color.Blue '.White
                '.Font = New Font(dgGen.Font, FontStyle.Bold)
            End With
        End With
    End Sub
    ' Remove unwanted columns. Note that bad_columns
    ' should list the column indexes in descending order.
    Public Sub RemoveGridColumns1(ByRef dgGen As DataGridView, ByVal bad_columns() As Integer, ByVal myArrF() As String, ByVal myArrN() As String, ByVal DView As DataView)
        '' Remove the unwanted columns.
        ''For i = 0 To bad_columns.GetUpperBound(0)
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(bad_columns(i))
        ''Next i
        ''For i = 0 To 4
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(0)
        ''Next i
        While dgGen.Columns.Count <> 0
            dgGen.Columns.Remove(dgGen.Columns(0))
        End While
        ' Initialize and add a text box column.
        Dim column As DataGridViewColumn
        Dim i As Integer
        For i = 0 To myArrF.Length - 1 '.GetUpperBound(0)
            column = New DataGridViewTextBoxColumn()
            With column
                .DataPropertyName = myArrF(i)
                .Name = myArrN(i)
                Try
                    If DView.Table.Columns(myArrF(i)).DataType.Name = "Double" Then
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .DefaultCellStyle.Format = "###0.00"
                    End If
                Catch ex As Exception

                End Try
            End With
            dgGen.Columns.Add(column)
        Next
        With dgGen.ColumnHeadersDefaultCellStyle
            .BackColor = Drawing.Color.Red '.Navy
            .ForeColor = Drawing.Color.White
            .Font = New Drawing.Font(dgGen.Font, Drawing.FontStyle.Bold)
        End With
    End Sub
    Function GmNull(ByVal Stuff As Object, ByVal EqualStuff As Type) As Object    'Object
        'Boolean  System.Boolean  2 bytes  True or False.  
        'Byte  System.Byte  1 byte  0 through 255 (unsigned).  
        'Char  System.Char  2 bytes  0 through 65535 (unsigned).  
        'Date  System.DateTime  8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.  
        'Decimal  System.Decimal  16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
        '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
        '+/-0.0000000000000000000000000001 (+/-1E-28).  
        'Double 
        '(double-precision floating-point)  System.Double  8 bytes  -1.79769313486231570E+308 through 
        '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
        'Integer  System.Int32  4 bytes  -2,147,483,648 through 2,147,483,647.  
        'Long 
        '(long integer)  System.Int64  8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
        'Object  System.Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
        'Short  System.Int16  2 bytes  -32,768 through 32,767.  
        'Single 
        '(single-precision floating-point)  System.Single  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
        'String 
        '(variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
        'User-Defined Type 
        '(structure)  (inherits from System.ValueType)  Depends on implementing platform  Each member of the structure has a range determined by its data type and independent of the ranges of the other members.  

        'VarType (varname)
        GmNull = Nothing
        Try
            Dim FieldType As String
            'If IsDBNull(Stuff) Then
            If Stuff Is DBNull.Value Then
                GmNull = 0
                Select Case EqualStuff.ToString
                    Case "System.Boolean" '2 bytes  True or False
                        FieldType = "System.Boolean"
                        GmNull = False
                    Case "System.Byte"  '1 byte  0 through 255 (unsigned).
                        FieldType = "System.Byte"
                    Case "System.Char"  '2 bytes  0 through 65535 (unsigned).
                        FieldType = "System.Char"
                        GmNull = ""
                    Case "System.DateTime"  '8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.
                        FieldType = "System.DateTime"
                        GmNull = CTODate '"99991231"
                    Case "System.Decimal"  '16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
                        '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
                        '+/-0.0000000000000000000000000001 (+/-1E-28).  
                        FieldType = "System.Decimal"
                    Case "System.Double"  '8 bytes  -1.79769313486231570E+308 through 
                        '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
                        FieldType = "System.Double"
                    Case "System.Int32"    'Integer4 bytes  -2,147,483,648 through 2,147,483,647.  
                        FieldType = "System.Int32"
                    Case "System.Int64" '(long integer)8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
                        FieldType = "System.Int64"
                    Case "System.Object" 'Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
                        FieldType = "System.Object"
                    Case "System.Int16"  'Short    2 bytes  -32,768 through 32,767. 
                        FieldType = "System.Int16"
                    Case "System.Single"  'Single (single-precision floating-point)  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
                        FieldType = "System.Single"
                    Case "System.String" 'String (variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
                        FieldType = "System.String"
                        GmNull = ""
                End Select
                Return GmNull
            Else
                If Not Stuff.GetType.ToString = EqualStuff.ToString Then
                    MsgBox("Error GmNull " & vbCrLf & "Not Stuff.GetType.ToString = EqualStuff.GetType.ToString", MsgBoxStyle.Critical)
                    GmNull = 0
                    Return GmNull
                End If
                Select Case EqualStuff.ToString
                    Case "System.Boolean" '2 bytes  True or False
                        FieldType = "System.Boolean"
                        Dim Result As Boolean = Stuff
                        Return Result
                    Case "System.Byte"  '1 byte  0 through 255 (unsigned).
                        FieldType = "System.Byte"
                        Dim Result As Byte = Stuff
                        Return Result
                    Case "System.Char"  '2 bytes  0 through 65535 (unsigned).
                        FieldType = "System.Char"
                        Dim Result As Char = Stuff
                        Return Result
                        'GmNull = Stuff
                        'Transform = ""
                    Case "System.DateTime"  '8 bytes  0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.
                        FieldType = "System.DateTime"
                        Dim Result As DateTime = Stuff
                        Return Result
                        'Transform = "99991231"
                    Case "System.Decimal"  '16 bytes  0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 
                        '0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest nonzero number is 
                        '+/-0.0000000000000000000000000001 (+/-1E-28).  
                        FieldType = "System.Decimal"
                        Dim Result As Decimal = Stuff
                        Return Result
                    Case "System.Double"  '8 bytes  -1.79769313486231570E+308 through 
                        '-4.94065645841246544E-324 for negative values; 4.94065645841246544E-324 through 1.79769313486231570E+308 for positive values.  
                        FieldType = "System.Double"
                        Dim Result As Double = Stuff
                        Return Result
                    Case "System.Int32"    'Integer4 bytes  -2,147,483,648 through 2,147,483,647.  
                        FieldType = "System.Int32"
                        Dim Result As Int32 = Stuff
                        Return Result
                    Case "System.Int64" '(long integer)8 bytes  -9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.  
                        FieldType = "System.Int64"
                        Dim Result As Int32 = Stuff
                        Return Result
                    Case "System.Object" 'Object (class)  4 bytes  Any type can be stored in a variable of type Object.  
                        FieldType = "System.Object"
                    Case "System.Int16"  'Short    2 bytes  -32,768 through 32,767. 
                        FieldType = "System.Int16"
                        Dim Result As Int16 = Stuff
                        Return Result
                    Case "System.Single"  'Single (single-precision floating-point)  4 bytes  -3.4028235E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.4028235E+38 for positive values.  
                        FieldType = "System.Single"
                        Dim Result As Single = Stuff
                        Return Result
                    Case "System.String" 'String (variable-length)  System.String (class)  Depends on implementing platform  0 to approximately 2 billion Unicode characters.  
                        FieldType = "System.String"
                        'GmNull = Stuff
                        Dim Result As String = Stuff
                        Return Result
                End Select
                'GmNull = Stuff
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try
    End Function
    Public Sub AddOutOfOfficeColumn(ByVal dg As DataGridView)
        Dim col As DataGridViewCheckBoxColumn = dg.Columns("Check")
        Try

            If IsNothing(col) Then
                col = New DataGridViewCheckBoxColumn
                With col
                    .DataPropertyName = "Check"
                    .HeaderText = "Check" 'ColumnName.OutOfOffice.ToString()
                    .Name = "Check" 'ColumnName.OutOfOffice.ToString()
                    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .FlatStyle = FlatStyle.Standard
                    .CellTemplate = New DataGridViewCheckBoxCell()
                    .CellTemplate.Style.BackColor = Drawing.Color.Beige
                    .SortMode = DataGridViewColumnSortMode.Automatic
                    .Width = 40

                End With
                For i As Integer = 0 To dg.Columns.Count - 1
                    Debug.Print(dg.Columns(i).Name)
                Next
                dg.Columns.Insert(0, col)
            End If
        Catch ex As Exception

        End Try

    End Sub
    <Runtime.CompilerServices.Extension()>
    Function WordCount(ByVal str As String) As Integer
        Return str.Split(New Char() {" "c, "."c, "?"c}, StringSplitOptions.RemoveEmptyEntries).Length
    End Function
End Module
