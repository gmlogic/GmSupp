Imports System.IO

Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName

        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description & vbCrLf
        Dim AppFile As String = My.Application.Info.DirectoryPath & "\GmSupp.exe"
        'If File.Exists(AppFile) Then
        If My.Application.IsNetworkDeployed Then
            Me.TextBoxDescription.Text &= String.Format("CurrentVersion: {0}", My.Application.Deployment.CurrentVersion.ToString) & vbCrLf
            Me.TextBoxDescription.Text &= String.Format("Build: {0:dd/MM/yyyy}", File.GetLastWriteTime(AppFile).ToString) & vbCrLf
            Me.TextBoxDescription.Text &= String.Format("Path: {0}", AppFile.ToString) & vbCrLf
            Me.TextBoxDescription.Text &= String.Format("AppName: {0}", "GmSupp")
        End If
        'Else
        '    MsgBox("Προσοχή !!! Δεν Υπάρχει " & AppFile, MsgBoxStyle.Critical)
        'End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
