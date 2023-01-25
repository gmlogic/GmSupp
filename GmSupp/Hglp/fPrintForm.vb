Imports System.Drawing.Printing
Imports GmSupp
Imports GmSupp.Hglp
Imports Softone

Public Class fPrintForm


    Public Property RecordID() As Integer
    Public Property Soft1Conn As XSupport
    Public Property Soft1Object() As String
    Public Property Soft1ObjectID() As String
    Public Property docs As List(Of ccCVShipment)
    Public Property db As DataClassesHglpDataContext
    Public Property CompanyT As Integer

    Private Sub fPrintForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillFormCombo(Soft1ObjectID)
        FillPrintersCombo()
    End Sub

    Public Sub FillFormCombo(ByVal ObjectID As Integer)
        Dim forms = From tmp In db.TEMPLATEs
                    Where tmp.COMPANY = CompanyT And tmp.SOSOURCE = ObjectID And tmp.ISACTIVE = 1
                    Order By tmp.TEMPLATES
                    Select tmp.TEMPLATES, NAME = tmp.TEMPLATES & " " & tmp.NAME

        cmbForms.DataSource = forms
        cmbForms.DisplayMember = "NAME"
        cmbForms.ValueMember = "TEMPLATES"
        cmbForms.SelectedIndex = cmbForms.FindString("3003")
    End Sub

    Private Sub FillPrintersCombo()
        Dim pInstalledPrinters As String
        'For Each pInstalledPrinters In PrinterSettings.InstalledPrinters
        '    If pInstalledPrinters = "Microsoft Print to PDF" Then
        '        ' generate a file name as the current date/time in unix timestamp format
        '        Dim file As String = DirectCast((DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1))).TotalSeconds.ToString(), String)

        '        ' the directory to store the output.
        '        Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        '        ' initialize PrintDocument object
        '        ' set the printer to 'Microsoft Print to PDF'

        '        ' tell the object this document will print to file

        '        ' set the filename to whatever you like (full path)
        '        Dim doc As New PrintDocument() With {
        '            .PrinterSettings = New PrinterSettings() With {.PrinterName = "Microsoft Print to PDF", .PrintToFile = True, .PrintFileName = IO.Path.Combine(directory, file & ".pdf")}}

        '        doc.Print()
        '    End If
        '    cmbPrinters.Items.Add(pInstalledPrinters)
        'Next
        cmbPrinters.Items.Insert(0, "PDF file")
        'cmbPrinters.Items.Add("928")
        'cmbPrinters.Items.Add("WORD")
        'cmbPrinters.Items.Add("EXCEL")
        'cmbPrinters.Items.Add("METAFILE")
        If cmbPrinters.Items.Count > 0 Then cmbPrinters.SelectedIndex = 0
    End Sub

    Private Sub btnSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFile.Click
        Dim fdlg As SaveFileDialog = New SaveFileDialog()
        fdlg.Title = "Save print file"
        fdlg.InitialDirectory = "C:\"
        fdlg.Filter = "All files (*.*)|*.*"
        fdlg.RestoreDirectory = True
        If fdlg.ShowDialog() = DialogResult.OK Then
            txtFileName.Text = fdlg.FileName
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'Dim myArray(4) As String
        'Dim SysRequest As Object = Soft1Conn.GetStockObj("SysRequest", True)
        'Dim S1obj As XModule
        'Try
        '    S1obj = Soft1Conn.CreateModule(Soft1Object)
        '    S1obj.LocateData(RecordID)
        '    myArray(0) = S1obj.Handle     'SoftOne Object
        '    myArray(1) = cmbForms.SelectedItem(0).ToString     'Print Form Code
        '    myArray(2) = cmbPrinters.SelectedItem.ToString  'Printer Name or File Name Type (928, 437, EXCEL, WORD, METAFILE)
        '    myArray(3) = txtFileName.Text  'File Name (used only when printing to file)
        '    Soft1Conn.CallPublished(SysRequest, "PrintForm", myArray)
        'Catch ex As Exception
        '    MsgBox("Print Error", MsgBoxStyle.Critical)
        'End Try

        Dim myArray(4) As String

        Dim SysRequest As Object = Soft1Conn.GetStockObj("SysRequest", True)
        Dim S1obj As XModule
        Try
            S1obj = Soft1Conn.CreateModule("SALDOC") 'Soft1Object)
            'S1obj.LocateData(26305) '32689) 'RecordID)
            'Dim dsCustomers As New DataSet
            'Dim myFinDocTbl As XTable
            'myFinDocTbl = S1obj.GetTable("SALDOC")
            'dsCustomers.Tables.Add(myFinDocTbl.CreateDataTable(True))
            'myArray(0) = S1obj.Handle     'SoftOne Object
            myArray(1) = cmbForms.SelectedValue     'Print Form Code
            myArray(2) = cmbPrinters.SelectedItem.ToString  'Printer Name or File Name Type (928, 437, EXCEL, WORD, METAFILE)
            'myArray(3) = "E:\temp\Data\" & "FinCode" & ".pdf" ' "test1" 'txtFileName.Text  'File Name (used only when printing to file)
            's1Conn.CallPublished(SysRequest, "PrintForm", myArray)



            For Each item In docs

                Dim chkFindoc As Integer = item.FINDOC
                Dim FinCode As String = item.FINCODE

                'myArray(3) = "E:\temp\Data\" & FinCode & ".pdf" 'txtFileName.Text  'File Name (used only when printing to file)
                S1obj.LocateData(chkFindoc) '32689) 'RecordID)

                Dim myFinDocTbl As XTable
                'myModule.InsertData()
                myFinDocTbl = S1obj.GetTable("FINDOC")
                Dim dsCustomers As New DataSet
                dsCustomers.Tables.Add(myFinDocTbl.CreateDataTable(True))
                myArray(0) = S1obj.Handle

                'SoftOne Object
                Soft1Conn.CallPublished(SysRequest, "PrintForm", myArray)
                'Soft1Conn.CallPublished("SysRequest.PrintForm', VarArray(vPURModule,100,'928', vFile,4));
                Dim sourcePath = "C:\test\" & "_" + item.FINDOC.ToString & ".PDF"
                Dim destinationPath = "C:\test\" & item.FINCODE & "_" & String.Format("{0:yyyyMMdd}", item.TRNDATE) & "_" & item.CODE & "_" + item.QTY1.ToString & ".PDF"
                Dim info As IO.FileInfo = New IO.FileInfo(sourcePath)
                If info.Exists Then
                    info.MoveTo(destinationPath)
                End If

            Next
        Catch ex As Exception
            MsgBox("Print Error", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub BtnTestConnection_Click(sender As Object, e As EventArgs) Handles BtnTestConnection.Click
        MsgBox("Company: " & Soft1Conn.Info.CompanyId & " Branch: " & Soft1Conn.Info.BranchId, MsgBoxStyle.Information)
    End Sub

    Private Sub cmbForms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbForms.SelectedIndexChanged

    End Sub
End Class