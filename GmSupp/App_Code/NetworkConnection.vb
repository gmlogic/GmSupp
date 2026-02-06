Imports System.Runtime.InteropServices

Module NetworkConnection

    ' ===============================
    ' Win32 API
    ' ===============================
    <DllImport("mpr.dll", CharSet:=CharSet.Unicode)>
    Private Function WNetAddConnection2(
        ByRef netResource As NETRESOURCE,
        password As String,
        username As String,
        flags As Integer
    ) As Integer
    End Function

    <DllImport("mpr.dll", CharSet:=CharSet.Unicode)>
    Private Function WNetCancelConnection2(
        name As String,
        flags As Integer,
        force As Boolean
    ) As Integer
    End Function

    ' ===============================
    ' Structures / Consts
    ' ===============================
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        Public lpLocalName As String
        Public lpRemoteName As String
        Public lpComment As String
        Public lpProvider As String
    End Structure

    Private Const RESOURCETYPE_DISK As Integer = &H1
    Private Const CONNECT_TEMPORARY As Integer = &H4

    ' ===============================
    ' CONNECT
    ' ===============================
    Public Function ConnectToShare(
        uncPath As String,
        username As String,
        password As String
    ) As Integer

        Dim nr As New NETRESOURCE With {
            .dwType = RESOURCETYPE_DISK,
            .lpRemoteName = uncPath
        }

        Dim rc As Integer = WNetAddConnection2(
            nr,
            password,
            username,
            CONNECT_TEMPORARY
        )

        Return rc
    End Function

    ' ===============================
    ' DISCONNECT
    ' ===============================
    Public Function DisconnectShare(uncPath As String) As Integer
        Return WNetCancelConnection2(uncPath, 0, True)
    End Function

End Module
