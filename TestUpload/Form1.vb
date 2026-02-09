Imports System.IO

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
            Dim nasRoot As String = "\\nas0"
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

                Dim files = IO.Directory.GetFiles(nasPath, "*.*", IO.SearchOption.AllDirectories)

                For Each f In files
                    'IO.File.Delete(f)
                    Debug.WriteLine(f)   ' full path
                Next

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


    Private Async Sub BtnUploadAsync_Click(sender As Object, e As EventArgs) Handles BtnUploadAsync.Click


        'If e.RowIndex < 0 Then Exit Sub
        'If MTRLINEsDataGridView.Columns(e.ColumnIndex).Name <> "btnDocs" Then Exit Sub

        Using ofd As New OpenFileDialog()
            ofd.Title = "Επιλογή αρχείου"
            ofd.Filter = "Όλα τα αρχεία (*.*)|*.*"
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim filePath = ofd.FileName

            Try
                Await UploadFileToApiAsync(filePath, "KAVALA", 9999, 1) ' MTRLINEsDataGridView.Rows(e.RowIndex).Cells("MTRLINES").Value)

                'MTRLINEsDataGridView.Rows(e.RowIndex).Cells("Docs").Value = Path.GetFileName(filePath)

                MessageBox.Show("Το αρχείο στάλθηκε επιτυχώς στο σύστημα.",
                            "OK",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Σφάλμα",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Async Function UploadFileToApiAsync(filePath As String, facility As String, findoc As Integer, mtrlines As Integer) As Task

        Using client As New Net.Http.HttpClient()

            client.Timeout = TimeSpan.FromMinutes(10)
            client.DefaultRequestHeaders.Add("X-API-KEY", "test123")

            Using content As New Net.Http.MultipartFormDataContent()

                ' --- metadata ---
                content.Add(New Net.Http.StringContent(facility), "facility")
                content.Add(New Net.Http.StringContent(findoc.ToString()), "findoc")
                content.Add(New Net.Http.StringContent(mtrlines.ToString()), "mtrlines")

                ' --- file stream ---
                Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    Dim fileContent As New Net.Http.StreamContent(fs)
                    fileContent.Headers.ContentType =
                    New Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream")

                    content.Add(fileContent, "file", Path.GetFileName(filePath))

                    'Dim resp = Await client.PostAsync("https://gmapi.kfertilizers.gr:19581/api/files/upload", content)
                    Dim resp = Await client.PostAsync("http://192.168.10.108:29581/api/files/upload", content)

                    If Not resp.IsSuccessStatusCode Then
                        Throw New Exception(
                        $"Upload failed: {resp.StatusCode} - {Await resp.Content.ReadAsStringAsync()}"
                    )
                    End If
                End Using
            End Using
        End Using
    End Function


End Class
