Public Class Form1
    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click

        ' -------------------------
        ' Έλεγχοι
        ' -------------------------
        'If e.RowIndex < 0 Then Exit Sub
        'If MTRLINEsDataGridView.Columns(e.ColumnIndex).Name <> "btnDocs" Then Exit Sub

        Using ofd As New OpenFileDialog()
            ofd.Title = "Επιλογή αρχείου"
            ofd.Filter = "PDF files (*.pdf)|*.pdf|Όλα τα αρχεία (*.*)|*.*"
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim sourceFile As String = ofd.FileName

            ' -------------------------
            ' NAS PATH
            ' -------------------------
            Dim nasRoot As String = "\\192.168.10.12"
            Dim nasFolder As String '= GetNasFolderByFacility(facility)
            Dim Facilities As String = "KAVALA" ' Π.χ. από dropdown ή άλλο control
            Select Case Facilities
                Case "KAVALA"
                    nasFolder = "Soft1 Requests Kavala"
                Case "ATALANTI"
                    nasFolder = "Soft1 Requests Atalanti"
                Case "AYLIDA"
                    nasFolder = "Soft1 Requests Avlida"
                Case "VELESTINO"
                    nasFolder = "Soft1 Requests Velestino"
                Case Else
                    Throw New Exception("Άγνωστο εργοστάσιο: " & Facilities)
            End Select

            Dim nasPath As String = IO.Path.Combine(nasRoot, nasFolder)

            Dim destFile As String =
            IO.Path.Combine(nasPath, IO.Path.GetFileName(sourceFile))

            ' -------------------------
            ' SERVICE ACCOUNT (AD)
            ' -------------------------
            Dim adUser As String = "g.softonis"
            Dim adPass As String = "5$dOe)#nW3i@"

            Try
                ' Σύνδεση στο NAS (session-based)
                ' 🔌 Κλείσιμο σύνδεσης NAS
                'DisconnectShare(nasPath)
                Dim rc As Integer = ConnectToShare(nasPath, adUser, adPass)

                If rc <> 0 Then
                    Throw New Exception($"Αποτυχία σύνδεσης στο NAS. Error code: {rc}")
                End If

                ' -------------------------
                ' Έλεγχος overwrite
                ' -------------------------
                If IO.File.Exists(destFile) Then
                    Dim res = MessageBox.Show(
                    $"Το αρχείο '{IO.Path.GetFileName(destFile)}' υπάρχει ήδη." & vbCrLf &
                    "Θέλετε να αντικατασταθεί;",
                    "Υπάρχον αρχείο",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )

                    If res = DialogResult.No Then Exit Sub

                    IO.File.Copy(sourceFile, destFile, True)
                Else
                    IO.File.Copy(sourceFile, destFile)
                End If

                ' Μετά το File.Copy
                If Not IO.File.Exists(destFile) Then
                    Throw New Exception("Η αντιγραφή απέτυχε.")
                End If

                ' -------------------------
                ' ΕΜΦΑΝΙΣΗ ΣΤΟ GRID (μόνο filename)
                ' -------------------------
                Dim fileNameOnly As String = IO.Path.GetFileName(destFile)

                'MTRLINEsDataGridView.Rows(e.RowIndex).Cells("Docs").Value = fileNameOnly
                'With MTRLINEsDataGridView.Rows(e.RowIndex).Cells("Docs")
                '    .Value = fileNameOnly     ' τι βλέπει ο χρήστης
                'End With

                MessageBox.Show(
                "Το αρχείο αποθηκεύτηκε επιτυχώς.",
                "OK",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

            Catch ex As Exception
                MessageBox.Show(
                ex.Message,
                "Σφάλμα",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Finally
                ' 🔌 Κλείσιμο σύνδεσης NAS
                DisconnectShare(nasPath)
            End Try
        End Using

    End Sub
End Class
