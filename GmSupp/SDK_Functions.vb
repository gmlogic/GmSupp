Imports Softone

Module SDK_Functions

    Public Function S1_GetParamValue(S1Object As String, ByVal s1PrmName As String) As String
        Dim ModuleIntf As Object = s1Conn.GetStockObj("ModuleIntf", True)
        Dim tmpMod As XModule = s1Conn.CreateModule(S1Object)
        Dim myArray(2) As Object
        myArray(0) = tmpMod.Handle
        myArray(1) = s1PrmName
        Return s1Conn.CallPublished(ModuleIntf, "GetParamValue", myArray).ToString
        tmpMod.Dispose()
    End Function

    Public Function S1_GetTableFields(strTableName As String) As String
        S1_GetTableFields = ""
        Dim SysRequest As Object = s1Conn.GetStockObj("SysRequest", True)
        Dim myArray(0) As Object
        myArray(0) = strTableName
        S1_GetTableFields = s1Conn.CallPublished(SysRequest, "GetTableFieldNames", myArray).ToString.Replace(vbCrLf, ",").Trim(",")
    End Function

    Public Sub S1_SetProperty(S1Object As String, ByVal s1ControlType As String, ByVal s1ControlName As String, ByVal s1Property As String, ByVal s1Value As String)
        'Example:  S1_SetProperty("CUSTOMER", "PANEL", "PANEL1NAME", "VISIBLE", "TRUE"')
        Dim ModuleIntf As Object = s1Conn.GetStockObj("ModuleIntf", True)
        Dim tmpMod As XModule = s1Conn.CreateModule(S1Object)
        Dim myArray(4) As Object
        myArray(0) = tmpMod.Handle
        myArray(1) = s1ControlType
        myArray(2) = s1ControlName
        myArray(3) = s1Property
        myArray(4) = s1Value
        s1Conn.CallPublished(ModuleIntf, "SetProperty", myArray)
        tmpMod.Dispose()
    End Sub



End Module
