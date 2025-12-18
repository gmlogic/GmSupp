Imports System.Reflection
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
'Imports System.Reflection.Emit

Public Class Utility
    'Shared Sub ToggleConfigEncryption(ByVal exeConfigName As String)
    '    ' Takes the executable file name without the
    '    ' .config extension.
    '    Try
    '        ' Open the configuration file and retrieve 
    '        ' the connectionStrings section.
    '        Dim config As Configuration = ConfigurationManager. _
    '            OpenExeConfiguration(exeConfigName)

    '        Dim section As ConnectionStringsSection = DirectCast( _
    '            config.GetSection("connectionStrings"),  _
    '            ConnectionStringsSection)

    '        If section.SectionInformation.IsProtected Then
    '            ' Remove encryption.
    '            section.SectionInformation.UnprotectSection()
    '        Else
    '            ' Encrypt the section.
    '            section.SectionInformation.ProtectSection( _
    '              "DataProtectionConfigurationProvider")
    '        End If

    '        ' Save the current configuration.
    '        config.Save()

    '        Console.WriteLine("Protected={0}", _
    '        section.SectionInformation.IsProtected)

    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub
    ' ''' <summary>
    ' ''' GetToggleConfig
    ' ''' </summary>
    ' ''' <param name="exeConfigName"></param>
    ' ''' <returns>GetToggleConfig</returns>
    ' ''' <remarks>if thens</remarks>
    'Shared Function GetToggleConfig(ByVal exeConfigName As String) As Boolean
    '    GetToggleConfig = False
    '    ' Takes the executable file name without the
    '    ' .config extension.
    '    Try
    '        ' Open the configuration file and retrieve 
    '        ' the connectionStrings section.
    '        Dim config As Configuration = ConfigurationManager. _
    '            OpenExeConfiguration(exeConfigName)

    '        Dim section As ConnectionStringsSection = DirectCast( _
    '            config.GetSection("connectionStrings"),  _
    '            ConnectionStringsSection)

    '        If section.SectionInformation.IsProtected Then
    '            ' ProtectSection.
    '            GetToggleConfig = True
    '        Else
    '            ' UnprotectSection.
    '            GetToggleConfig = False
    '        End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Function
#Region "LinQ"
    Public Shared Function LINQToDataSet(ByVal myDB As DataClassesHglpDataContext, ByVal item As IQueryable) As DataSet
        Dim cmd As SqlCommand = TryCast(myDB.GetCommand(item), SqlCommand)

        Dim oDataTable As New DataTable()
        Dim oDataAdapter As New SqlDataAdapter(cmd)
        oDataAdapter.Fill(oDataTable)

        Dim oDataSet As New DataSet()
        oDataSet.Tables.Add(oDataTable)
        Return oDataSet
    End Function
    Public Shared Function LINQToDataTable(ByVal myDB As DataClassesHglpDataContext, ByVal item As IQueryable) As DataTable
        Dim cmd As SqlCommand = TryCast(myDB.GetCommand(item), SqlCommand)

        Dim oDataTable As New DataTable()
        Dim oDataAdapter As New SqlDataAdapter(cmd)
        oDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function
    Public Shared Function LINQToDataTable(Of T)(varlist As IEnumerable(Of T)) As DataTable
        Dim dtReturn As New DataTable()

        ' column names 
        Dim oProps As PropertyInfo() = Nothing

        If varlist Is Nothing Then
            Return dtReturn
        End If
        Dim dr As DataRow = Nothing
        Dim pi1 As PropertyInfo = Nothing
        Try
            For Each rec As T In varlist
                ' Use reflection to get property names, to create table, Only first time, others will follow 
                If oProps Is Nothing Then
                    oProps = DirectCast(rec.[GetType](), Type).GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Dim colType As Type = pi.PropertyType

                        If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() Is GetType(Nullable(Of ))) Then
                            colType = colType.GetGenericArguments()(0)
                        End If

                        dtReturn.Columns.Add(New DataColumn(pi.Name, colType))
                    Next
                End If

                dr = dtReturn.NewRow()

                For Each pi1 In oProps
                    If Not IsDBNull(dr("FINDOC")) AndAlso dr("FINDOC") = 34229 Then
                        dr("FINDOC") = 34229
                    End If
                    dr(pi1.Name) = If(pi1.GetValue(rec, Nothing) Is Nothing, DBNull.Value, pi1.GetValue(rec, Nothing))
                Next

                dtReturn.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dtReturn
    End Function

    'Public Overridable Function Find(expression As System.Linq.Expressions.Expression(Of Func(Of T, Boolean)), Optional maxHits As Integer = 100) As IEnumerable(Of T)
    '    Return Me._dbset.Where(expression).Take(maxHits)
    'End Function
    Public Shared Function DataTableFromIEnumerable(ien As IEnumerable) As DataTable
        Dim dt As New DataTable()
        Dim t As Type = Nothing
        Dim pis As PropertyInfo() = Nothing
        Dim pi As PropertyInfo = Nothing
        Try
            For Each obj As Object In ien
                t = obj.[GetType]()
                pis = t.GetProperties()
                If dt.Columns.Count = 0 Then
                    For Each pi In pis
                        dt.Columns.Add(pi.Name, pi.PropertyType)
                    Next
                End If

                Dim dr As DataRow = dt.NewRow()
                For Each pi In pis
                    Dim value As Object = pi.GetValue(obj, Nothing)
                    dr(pi.Name) = value
                Next

                dt.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dt
    End Function
    Private Sub Create_DATASET()
        'test1()
        Try
            'Αντί για Object μπαίνει η class πχ AvmResults από τη οποία θέλουμε να δημιουργήσουμε την DataSet
            Dim avm As New Object 'AvmResults
            Dim Type1 As Type = avm.GetType()
            Dim properties As PropertyInfo() = avm.GetType().GetProperties() 'Type1.GetProperties()

            For Each prop As PropertyInfo In properties
                '<xs:element name="DataColumn1" msprop:Generator_ColumnVarNameInTable="columnDataColumn1" msprop:Generator_ColumnPropNameInRow="DataColumn1" msprop:Generator_ColumnPropNameInTable="DataColumn1Column" msprop:Generator_UserColumnName="DataColumn1" type="xs:string" minOccurs="0" />
                Dim ss As String = ""
                ss = "<xs:element name=""" & prop.Name & """ msprop:Generator_ColumnVarNameInTable=""" & "column" & prop.Name & """ msprop:Generator_ColumnPropNameInRow=""" & prop.Name & """ msprop:Generator_ColumnPropNameInTable=""" & prop.Name & "Column" & """ msprop:Generator_UserColumnName=""" & prop.Name & """ type=""" & "xs:" & prop.PropertyType.Name.ToString.ToLower & """ minOccurs=""0"" />"
                'Debug.Print("Name: " + prop.Name + " Type:" + prop.PropertyType.Name) ' + ", Value: " + prop.GetValue(obj, null))
                Debug.Print(ss)
            Next


        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function ConvertDataTable(Of T)(dt As DataTable) As List(Of T)
        Dim data As New List(Of T)()
        For Each row As DataRow In dt.Rows
            Dim item As T = GetItem(Of T)(row)
            data.Add(item)
        Next
        Return data
    End Function
    Private Shared Function GetItem(Of T)(dr As DataRow) As T
        Dim temp As Type = GetType(T)
        Dim obj As T = Activator.CreateInstance(Of T)()

        For Each column As DataColumn In dr.Table.Columns
            For Each pi As PropertyInfo In GetType(DataRow).GetProperties()
                Dim name = pi.Name
                Try
                    If pi.Name = column.ColumnName Then
                        pi.SetValue(obj, dr(column.ColumnName), Nothing)
                    Else
                        Continue For
                    End If

                    'Dim val = GetType(DataRow).GetProperty(name).GetValue(dr, Nothing)
                    'If Not IsNothing(val) Then
                    '    'If {"TRDR", "TRNDATE", "FINCODE", "FINDOC", "SOSOURCE"}.Contains(name) Then
                    '    '    name = name.ToString.ToLower
                    '    'End If
                    '    'GetType(DataRow).GetProperty(name).SetValue(dr, val, Nothing)
                    '    pi.SetValue(obj, dr(column.ColumnName), Nothing)
                    'End If
                Catch ex As Exception

                End Try
            Next
            'For Each pro As PropertyInfo In temp.GetProperties()
            '    If pro.Name = column.ColumnName Then
            '        pro.SetValue(obj, dr(column.ColumnName), Nothing)
            '    Else
            '        Continue For
            '    End If
            'Next
        Next
        Return obj
    End Function

    Public Shared Function CreateClass(ByVal className As String, ByVal properties As Dictionary(Of String, Type)) As Type

        Dim myDomain As AppDomain = AppDomain.CurrentDomain
        Dim myAsmName As New AssemblyName("MyAssembly")
        Dim myAssembly As Emit.AssemblyBuilder = myDomain.DefineDynamicAssembly(myAsmName, Emit.AssemblyBuilderAccess.Run)

        Dim myModule As Emit.ModuleBuilder = myAssembly.DefineDynamicModule("MyModule")

        Dim myType As Emit.TypeBuilder = myModule.DefineType(className, TypeAttributes.Public)

        myType.DefineDefaultConstructor(MethodAttributes.Public)

        For Each o In properties

            Dim prop As Emit.PropertyBuilder = myType.DefineProperty(o.Key, Reflection.PropertyAttributes.HasDefault, o.Value, Nothing)

            Dim field As Emit.FieldBuilder = myType.DefineField("_" + o.Key, o.Value, FieldAttributes.[Private])

            Dim getter As Emit.MethodBuilder = myType.DefineMethod("get_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, o.Value, Type.EmptyTypes)
            Dim getterIL As Emit.ILGenerator = getter.GetILGenerator()
            getterIL.Emit(Emit.OpCodes.Ldarg_0)
            getterIL.Emit(Emit.OpCodes.Ldfld, field)
            getterIL.Emit(Emit.OpCodes.Ret)

            Dim setter As Emit.MethodBuilder = myType.DefineMethod("set_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, Nothing, New Type() {o.Value})
            Dim setterIL As Emit.ILGenerator = setter.GetILGenerator()
            setterIL.Emit(Emit.OpCodes.Ldarg_0)
            setterIL.Emit(Emit.OpCodes.Ldarg_1)
            setterIL.Emit(Emit.OpCodes.Stfld, field)
            setterIL.Emit(Emit.OpCodes.Ret)

            prop.SetGetMethod(getter)
            prop.SetSetMethod(setter)

        Next

        Return myType.CreateType()

    End Function

#End Region



    Public Shared Sub Changed(ByVal sender As Object, ByVal e As TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs(Of triggeredClass))

        Dim changedEntity = e.Entity
        If changedEntity.id Then

            Dim db1 As New DataClassesReveraDataContext(My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress"))
            Try
                If Now >= CTODate Then
                    'db.Connection.ConnectionString = My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
                    Dim q = (From so In db1.SOAUDITs Join us In db1.USERs On so.USERS Equals us.USERS
                             Where so.COMPANY = 1000
                             Select so.SOAUDIT, so.COMPANY, so.USERS,
                             usName = us.NAME, so.SOMODULE, so.NAME,
                             so.LOGINDATE, so.DBDATE, so.GROUPS, so.CDOMAIN, so.CMACHINE).ToList '.AsQueryable


                    Dim qwh = q.Where(Function(f) f.COMPANY = 1000)
                End If


            Catch ex As Exception

            End Try

            'qwh = qwh.Where(Function(f) f.DBDATE >= DateTimePicker1.Value.Date And f.DBDATE <= DateTimePicker2.Value)

            'Dim m As Member = db.Members.Where(Function(f) f.MembersID = changedEntity.MembersID).FirstOrDefault
            'If Not IsNothing(m) Then
            '    'check pliromes
            '    'ec.Status = Nothing 'Status = Nothing (Ληγμένα) ,Status = 1 (Ενεργά - Μη Ληγμένα)
            '    'Dim ec As ecContract = m.ecContracts.Where(Function(f) f.CustomerID = m.MembersID And (Not f.Status Is Nothing AndAlso f.Status = 1)).OrderBy(Function(f) f.ClosedDate).FirstOrDefault
            '    Dim ec As ecContract = m.ecContracts.Where(Function(f) f.CustomerID = m.MembersID And If(f.Status, 0) = 1).OrderBy(Function(f) f.ClosedDate).FirstOrDefault

            '    If IsNothing(ec) Then
            '        ec = m.ecContracts.Where(Function(f) f.CustomerID = m.MembersID And f.Status Is Nothing).OrderBy(Function(f) f.ClosedDate).LastOrDefault
            '        HasNotExpired = True
            '    Else
            '        'Ok
            '        HasNotExpired = False
            '    End If
            'Else
            'End If
        End If




        'If (HasNotExpired) Then
        '    MemberWarningModal.ShowDialog()

        'End If

        'MsgBox("Το συμβόλαιό σας έχει λήξει. " & vbCrLf & "Παρακαλώ ανανεώστε την συνδρομή σας.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")

        'Dim dd As New PlansBr
        'dd.ShowDialog()
        'Dim changedEntity = e.Entity
        'MsgBox("test")
        'Console.WriteLine("DML operation: " & e.ChangeType)
        'Console.WriteLine("ID: " & changedEntity.ID)
        'Console.WriteLine("Name: " & changedEntity.Name)
        'Console.WriteLine("Surame: " & changedEntity.Surname)
    End Sub

    Public Shared Function GetLocalIP() As String
        Dim _IP As String = Nothing
        Try
            Dim _IPHostEntry As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())

            For Each _IPAddress As System.Net.IPAddress In _IPHostEntry.AddressList
                If _IPAddress.AddressFamily.ToString() = "InterNetwork" Then
                    _IP = _IPAddress.ToString()
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try
        Return _IP
    End Function

    Public Class triggeredClass
        Property id As Integer
        Property triggeredField As String

        'Property soAudit As Integer
    End Class

    Public Shared Function GetNoColumnDataGridView(CurDataGridView As DataGridView, CDataPropertyName As String) As DataGridViewColumn
        Dim col As DataGridViewColumn = Nothing
        col = CurDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = CDataPropertyName).FirstOrDefault
        Return col
        'Throw New NotImplementedException()
    End Function
#Region "Revera"

    ''' <summary>
    ''' PASSWORDVALIDATE(userPWD, soPassword): Boolean
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="userPWD"></param>
    ''' <returns>soPassword</returns>
    ''' <remarks>PASSWORDVALIDATE(stringtoValidate: String, Password: String): Boolean Compares the Not encrypted string "StringtoValidate" with an encrypted password field (e.g. USERS.SOPASSWORD) And returns TRUE if they match.</remarks>
    Public Shared Function soPassword(userId As Integer, Optional userPWD As String = Nothing) As String
        soPassword = "errorValidate"
        Using db As New DataClassesReveraDataContext(My.Settings.GenConnectionString)
            Dim us = db.USERs.Where(Function(f) f.USERS = userId).FirstOrDefault
            If Not IsNothing(us) Then
                soPassword = us.SOPASSWORD
                'PASSWORDVALIDATE(stringtoValidate: String, Password: String): Boolean
                'Compares the Not encrypted string "StringtoValidate" with an encrypted password field (e.g. USERS.SOPASSWORD) And returns TRUE if they match.
                Dim chkPass = True
                If Not IsNothing(userPWD) Then
                    chkPass = s1Conn.PasswordValidate(userPWD, soPassword)
                End If
                If chkPass Then
                    Return soPassword
                End If
            End If
        End Using

    End Function

    Friend Shared Function ChkUserIfExistInDb(userName As String) As Boolean
        ChkUserIfExistInDb = False

        Dim us = GmUserManager.ChkUser(userName)
        If Not IsNothing(us) Then
            Dim encryptPass = us.encryptPass
            Dim decryptPass = CryptoUtils.Decrypt(encryptPass)
            If UserId = 0 Then ' Not S1User
                Return True
            End If
            Dim soPassword As String = Utility.soPassword(UserId, decryptPass)
            If Not soPassword = "errorValidate" Then
                ChkUserIfExistInDb = True
            End If
        End If

        Return ChkUserIfExistInDb
    End Function

    Public Shared Function GetRandomPassword(ByVal length As Integer) As String
        Dim rgb As Byte() = New Byte(length - 1) {}
        Dim rngCrypt As New Security.Cryptography.RNGCryptoServiceProvider
        rngCrypt.GetBytes(rgb)
        Return Convert.ToBase64String(rgb)
    End Function
#End Region

    Public Shared Async Function executeRequestAsync(requestData As String, Optional Method As String = "POST") As Task(Of String)

        Return Await Task.Run(Function()
                                  Return executeRequest(requestData, Method)
                              End Function)
    End Function

    Public Shared Function executeRequest(requestData As String, Optional Method As String = "POST", Optional JsDunction As String = Nothing) As String

        Dim request As Net.HttpWebRequest = Nothing
        Dim responseFromServer As String = ""

        Try
            If Method = "POST" Then
                request = CType(
                Net.WebRequest.Create("http://001-dekagro.oncloud.gr/s1services"),
                Net.HttpWebRequest
            )
            End If

            request.Method = Method
            request.ContentType = "application/x-www-form-urlencoded"
            request.AutomaticDecompression =
            Net.DecompressionMethods.GZip Or Net.DecompressionMethods.Deflate

            Dim byteArray = Text.Encoding.UTF8.GetBytes(requestData)
            request.ContentLength = byteArray.Length

            Using dataStream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using

            Dim Soft1Encoding = Text.Encoding.GetEncoding(1253)

            Using response = CType(request.GetResponse(), Net.HttpWebResponse)
                Using responseStream = response.GetResponseStream()
                    Using reader As New IO.StreamReader(responseStream, Soft1Encoding)
                        responseFromServer = reader.ReadToEnd()
                    End Using
                End Using
            End Using

        Catch ex As Exception
            responseFromServer = "Error: " & ex.Message
        End Try

        Return responseFromServer
    End Function

    Friend Shared Async Function LoginWS() As Task(Of String)
        Dim sStr As String = Newtonsoft.Json.Linq.JObject.FromObject(New With {
                                                                           .service = "login",
                                                                           .username = "gmlogic",
                                                                           .password = "gmlogic1",
                                                                           .appId = "1007",
                                                                           .COMPANY = "5000",
                                                                           .BRANCH = "1000",
                                                                           .MODULE = "0",
                                                                           .REFID = "9999"}).ToString
        'Dim result As Task(Of String) = Nothing
        'result = Utility.executeRequestAsync(sStr)
        Dim jsonResult As String = Await Utility.executeRequestAsync(sStr)
        Dim gg As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult)
        If gg("success").ToString = "False" Then
            MsgBox("Dekagro error connection ", MsgBoxStyle.Critical, "GmMySettings")
            Throw New Exception()
        End If
        Return gg.GetValue("clientID").ToString
        'Return clientID
        'Throw New NotImplementedException()
    End Function

End Class
