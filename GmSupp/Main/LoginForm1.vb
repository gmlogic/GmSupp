Imports System.ComponentModel

Public Class LoginForm1
    Public Property Results As String
    'Public Property CompName As List(Of String)

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DateTimePicker1.Value = Now()
        Me.ddlXCOs.Items.Clear()

        'Test XCOs
        Dim XCOs As List(Of String)
        XCOs = System.IO.Directory.GetFiles(S1Path, "*.xco").ToList
        If XCOs.Count = 0 Then
            MsgBox("Προσοχή !!!. Δεν υπάρχουν τα ανάλογα XCO", MsgBoxStyle.Critical, "Critical")
            Me.Close()
            Exit Sub
        End If
        Dim items As New List(Of String)
        For Each xco In XCOs
            items.Add(xco.Replace(S1Path, "").Replace(".xco".ToUpper, "").ToUpper)
        Next
        'loginf.CompName = items

        'items = items.Where(Function(f) {"REVERA", "SERTORIUS"}.Contains(f)).ToList
        For Each xco In items
            Me.ddlXCOs.Items.Add(IO.Path.GetFileNameWithoutExtension(xco))
        Next
        If items.Where(Function(f) f = "REVERA").Count = 1 Then
            Me.ddlXCOs.Items.Add("SERTORIUS")
        End If
        If items.Count > 1 Then
            Me.ddlXCOs.Text = "Επιλέγξτε"
            Me.ddlXCOs.Items.Insert(0, "Επιλέγξτε")
        Else
            Me.ddlXCOs.SelectedIndex = 0
        End If

    End Sub

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If IsNothing(Me.ddlXCOs.SelectedItem) Then
            MsgBox("Προσοχή !!!. Λάθος στοιχεία (Επιλογή εταιρείας)", MsgBoxStyle.Critical, "Critical")
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Results = Me.UsernameTextBox.Text
    End Sub


End Class
