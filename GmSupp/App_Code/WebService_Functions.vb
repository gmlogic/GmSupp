Imports Softone
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports GmSupp.Hglp

Module WebService_Functions



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xSup">s1Conn = XSupport.Login(txtXCOFile, curUser, pass,txtCompany.ToString, txtBranch.ToString, DTLogin)</param>
    ''' <param name="XServ">"service": "login"</param>
    ''' <param name="userName">username</param>
    ''' <param name="passWord">password</param>
    ''' <param name="appId">COMPANY</param>
    ''' <returns></returns>
    Public Function S1_WSlogin(xSup As XSupport, userName As String, passWord As String, appId As String, Optional ByVal XServ As String = "login") As String 'Login using Web Service Call
        S1_WSlogin = ""
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""

        'Dim authStr8 As String = """service"": ""getBrowserInfo"",""clientID"": ""{0}"", ""appid"":""myappid"", ""OBJECT"": ""CUST_STM"", ""LIST"": """", ""FILTERS"": ""CUSTOMER.TRDR=mycusttrdr"""
        'Dim rs8 As String = executeRequest("{" & String.Format(authStr8, clientID) & "}")
        'Dim jo8 As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(rs8)
        'Dim myreqid8 As String = Convert.ToString(jo8("reqID"))
        'Dim authStr9 As String = """service"": ""getBrowserData"",""clientID"": ""{0}"", ""appid"":""p"", ""reqID"": """ & myreqid8 & """, ""START"": ""0"", ""LIMIT"": ""999999"""
        'Dim rs9 As String = executeRequest("{" & String.Format(authStr9, clientID) & "}")
        'Dim jo9 As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(rs9)


        WS_CallRequest = String.Format("""service"":""{0}"",""username"":""{1}"",""password"":""{2}"",""appId"":""{3}""", XServ, userName, passWord, appId)
        WS_CallRequest = String.Format("""service"":""{0}"",""username"":""{1}"",""password"":""{2}""", XServ, userName, passWord)
        WS_CallRequest = String.Format("""service"":""{0}""", XServ)
        '{
        '  "service": "login",
        '  "username": "john",
        '  "password":"aitis",
        '  "appId": "2001",
        '  ---- optional ---
        '  "LOGINDATE": "2017-12-31 13:59:59",
        '  "TIMEZONEOFFSET": -120
        '}
        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                S1_WSlogin = "----FROM WEB SERVICE CALL----" + vbCrLf
                For Each item As JObject In WS_jsResponse("fields")
                    S1_WSlogin += item("name").ToString + ","
                Next
                S1_WSlogin = S1_WSlogin.Remove(S1_WSlogin.Length - 1)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Function



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xSup">s1Conn = XSupport.Login(txtXCOFile, curUser, pass,txtCompany.ToString, txtBranch.ToString, DTLogin)</param>
    ''' <param name="XServ">"service": "getTableFields"</param>
    ''' <param name="strObject">"OBJECT": "CUSTOMER"</param>
    ''' <param name="strTable">TABLE":"CUSTOMER"</param>
    ''' <returns></returns>
    Public Function S1_WSGetTableFields(xSup As XSupport, strObject As String, strTable As String, Optional ByVal XServ As String = "service") As String 'Get Fields using Web Service Call
        S1_WSGetTableFields = ""
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""

        WS_CallRequest = String.Format("""service"":""getTableFields"",""OBJECT"":""{0}"",""TABLE"":""{1}""", strObject, strTable)
        '{
        '    "service": "getTableFields",
        '    "clientID": "Wj8T3tvs...  ...tlrT8",
        '    "appId": "2001",
        '    "OBJECT": "CUSTOMER",
        '    "TABLE":"CUSTOMER"	
        '}
        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                S1_WSGetTableFields = "----FROM WEB SERVICE CALL----" + vbCrLf
                For Each item As JObject In WS_jsResponse("fields")
                    S1_WSGetTableFields += item("name").ToString + ","
                Next
                S1_WSGetTableFields = S1_WSGetTableFields.Remove(S1_WSGetTableFields.Length - 1)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Function
    '    getObjects
    'Returns all application Business Objects

    'Request
    '{
    '    "service": "getObjects",
    '    "clientID": "Wj8T3tvs...  ...tlrT8",
    '    "appId": "2001"
    '}
    'Response
    '{
    '    "success": true,
    '    "count": 1354,
    '    "objects": [
    '...
    '        {
    '             "name": "CUSTOMER",
    '            "type": "EditMaster",
    '            "caption": "Customers"
    '        },
    '...

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xSup">s1Conn = XSupport.Login(txtXCOFile, curUser, pass,txtCompany.ToString, txtBranch.ToString, DTLogin)</param>
    ''' <param name="XServ">"service": "getTableFields"</param>
    ''' <param name="strObject">"OBJECT": "CUSTOMER"</param>
    ''' <param name="strTable">TABLE":"CUSTOMER"</param>
    ''' <returns></returns>
    Public Function S1_WSgetObjects(xSup As XSupport, strObject As String, strTable As String, Optional ByVal XServ As String = "getObjects") As String 'Get Fields using Web Service Call
        S1_WSgetObjects = ""
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""

        WS_CallRequest = String.Format("""service"":""{0}"",""OBJECT"":""{1}"",""TABLE"":""{2}""", XServ, strObject, strTable)
        '{
        '    "service": "getObjects",
        '    "clientID": "Wj8T3tvs...  ...tlrT8",
        '    "appId": "2001"
        '}
        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                'S1_WSGetTableFields = "----FROM WEB SERVICE CALL----" + vbCrLf
                'For Each item As JObject In WS_jsResponse("fields")
                '    S1_WSGetTableFields += item("name").ToString + ","
                'Next
                'S1_WSGetTableFields = S1_WSGetTableFields.Remove(S1_WSGetTableFields.Length - 1)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

    End Function

    Public Function S1_WSGetBrowserData(xSup As XSupport, strObject As String, strList As String, strFilters As String) As DataTable
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""
        Dim newdt As New DataTable
        WS_CallRequest = String.Format("""service"":""getBrowserInfo"",""OBJECT"":""{0}"",""LIST"":""{1}"",""FILTERS"":""{2}""", strObject, strList, strFilters)
        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then

                If Convert.ToInt64(WS_jsResponse("totalcount")) > 0 Then
                    newdt.Columns.Add("ID") 'Add the first column manually
                    'Get Table Columns
                    For Each item As JObject In WS_jsResponse("columns")
                        newdt.Columns.Add(item("header").ToString)
                    Next

                    Dim reqID As String = Convert.ToString(WS_jsResponse("reqID"))
                    WS_CallRequest = String.Format("""service"":""getBrowserData"",""reqID"":""{0}""", reqID)

                    Try
                        WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
                        WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)
                        If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                            'newdt = JsonConvert.DeserializeObject(Of DataTable)(WS_jsResponse("rows"))
                            Dim curRow As DataRow
                            For Each item As JArray In WS_jsResponse("rows")
                                curRow = newdt.NewRow()
                                Dim iCol As Integer = 0
                                For Each itemvalue As JValue In item.Values
                                    If iCol = 0 Then
                                        curRow(iCol) = itemvalue.ToString.Split(";")(1)
                                    Else
                                        curRow(iCol) = itemvalue.ToString
                                    End If
                                    iCol = iCol + 1
                                Next
                                newdt.Rows.Add(curRow)
                            Next
                        End If
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try

        Return newdt

    End Function

    Public Function S1_WSSetData(xSup As XSupport, strObject As String, strKEY As String, iSeries As Integer, iTRDR As Integer _
                                 , iMTRL As Integer, dQTY As Decimal, dPrice As Decimal) As String
        S1_WSSetData = ""
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""

        WS_CallRequest = String.Format("""service"":""setData"",""OBJECT"":""{0}"",""KEY"":""{1}"",""DATA"":", strObject, strKEY)
        WS_CallRequest += "{ ""SALDOC"": [ { ""SERIES"": """ + iSeries.ToString + """ , ""TRDR"": """ + iTRDR.ToString + """ } ], "
        WS_CallRequest += """ITELINES"": [ { ""MTRL"": """ + iMTRL.ToString + """ , ""QTY1"": """ + dQTY.ToString + """, ""PRICE"": """ + dPrice.ToString + """ } ] }"
        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                MsgBox("Data inserted!!! New ID:" + (Convert.ToString(WS_jsResponse("id"))), MsgBoxStyle.Information, strAppName)
            Else
                MsgBox(Convert.ToString(WS_jsResponse("error")), MsgBoxStyle.Critical, strAppName)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try
    End Function

    Public Function S1_WSSetData(xSup As XSupport, strObject As String, strKEY As String, WS_CallRequest As String) As String
        S1_WSSetData = ""
        Dim WS_strResponse As String = ""
        Dim WS_jsResponse As Newtonsoft.Json.Linq.JObject
        'Dim WS_CallRequest As String = ""
        Dim WS_Format As String = ""

        Try
            WS_strResponse = xSup.CallWebService("{" + WS_CallRequest + "}", WS_Format)
            WS_jsResponse = Newtonsoft.Json.Linq.JObject.Parse(WS_strResponse)

            If (Convert.ToBoolean(WS_jsResponse("success")) = True) And (Convert.ToString(WS_jsResponse("error")) = "") Then  'Success=True then
                MsgBox("Data inserted!!! New ID:" + (Convert.ToString(WS_jsResponse("id"))), MsgBoxStyle.Information, strAppName)
            Else
                MsgBox(Convert.ToString(WS_jsResponse("error")), MsgBoxStyle.Critical, strAppName)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try
    End Function
End Module
