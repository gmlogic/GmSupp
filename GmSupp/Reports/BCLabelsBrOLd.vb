Imports System.Drawing

Public Class BCLabelsBrOLd
    Private Sub BCLabelsBr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtLabelNo.Text = 5
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim BCLabels As New List(Of Hglp.ccCBCLabel)
        Dim BCLabel As New Hglp.ccCBCLabel

        Dim ms As New IO.MemoryStream
        Dim img As Image = Nothing
        Dim b As New BarcodeLib.Barcode
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
        b.IncludeLabel = True

        Dim BRotateFlipType As RotateFlipType = RotateFlipType.Rotate90FlipNone '.Rotate180FlipXY
        Dim BLabelPosition As BarcodeLib.LabelPositions = BarcodeLib.LabelPositions.BOTTOMCENTER
        Dim ForeColor As System.Drawing.Color = Color.Black
        Dim BackColor As System.Drawing.Color = Color.White
        'b.Width = BarcodeLib.Barcode.Image.Width
        'b.Height = barcode.Image.Height
        b.RotateFlipType = BRotateFlipType
        'b.EncodedImage.LabelPositions = BLabelPosition

        Dim gt As BarcodeLib.TYPE
        gt = BarcodeLib.TYPE.CODE128
        Try
            BCLabel.BCLabel = "2105050533 25072020 28072020 B1"
            img = b.Encode(gt, BCLabel.BCLabel & "*", 3000, 2000)

            img.Save(ms, Imaging.ImageFormat.Jpeg)
            img.Save("e:\temp\img\001.jpg", Imaging.ImageFormat.Jpeg)
            BCLabel.Barcode = ms.ToArray
        Catch ex As Exception
            MsgBox("Error Barcode " & ex.Message)
        End Try

        For no As Integer = 1 To Me.txtLabelNo.Text
            BCLabels.Add(BCLabel)
        Next

        newPrintTable(BCLabels, "GmSupp.BCLabel2.rdlc")


    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim BCLabels As New List(Of Hglp.ccCBCLabel)
        Dim BCLabel As New Hglp.ccCBCLabel

        Dim ms As New IO.MemoryStream
        Dim img As Image = Nothing
        Dim b As New BarcodeLib.Barcode
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
        b.IncludeLabel = True

        Dim BRotateFlipType As RotateFlipType = RotateFlipType.Rotate180FlipXY
        Dim BLabelPosition As BarcodeLib.LabelPositions = BarcodeLib.LabelPositions.BOTTOMCENTER
        Dim ForeColor As System.Drawing.Color = Color.Black
        Dim BackColor As System.Drawing.Color = Color.White
        'b.Width = BarcodeLib.Barcode.Image.Width
        'b.Height = barcode.Image.Height
        b.RotateFlipType = BRotateFlipType
        'b.EncodedImage.LabelPositions = BLabelPosition

        Dim gt As BarcodeLib.TYPE
        gt = BarcodeLib.TYPE.CODE128
        Try
            BCLabel.BCLabel = "2105050533 25072020 28072020 B1"
            img = b.Encode(gt, BCLabel.BCLabel & "*", 350, 200)

            img.Save(ms, Imaging.ImageFormat.Jpeg)

            BCLabel.Barcode = ms.ToArray
        Catch ex As Exception
            MsgBox("Error Barcode " & ex.Message)
        End Try

        For no As Integer = 1 To Me.txtLabelNo.Text
            BCLabels.Add(BCLabel)
        Next

        newPrintTable(BCLabels, "GmSupp.Applicat01.rdlc")
    End Sub
    Private Sub newPrintTable(gmVMNewDataTable As List(Of Hglp.ccCBCLabel), ReportEmbeddedResource As String)
        Try
            'Dim drvMaster As DataRowView
            'drvMaster = Me.MasterBindingSource.Current()
            Dim WRptView As New ReportViewer
            Dim ReportDataSource1 As New Microsoft.Reporting.WinForms.ReportDataSource
            ReportDataSource1.Name = "DSccCBCLabel" 'TimPrint"

            ReportDataSource1.Value = gmVMNewDataTable 'ConvertSta5Rpt(MtrLineNew, Me.boxOption_Anektelestes.Text) 'gmVMNewDataTable.DefaultView 'dt.DefaultView 'bind 'TimPrintBindingSource 'bind 'dt.DataSet 'TimPrintTableAdapter.Fill(GenDataSet.TimPrint, 0, 0, 0, 0, 0) 'Me.DataTable1BindingSource
            'Dim rp As Microsoft.Reporting.WinForms.LocalReport = WRptView.ReportViewer1.LocalReport

            'Dim inf As Microsoft.Reporting.WinForms.ReportParameterInfoCollection = rp.GetParameters
            'file:E:\Gmapps.NET\GmWinNet\GmWinNet2008\bin\MyFiles\GmHead.JPG
            WRptView.ReportViewer1.LocalReport.DataSources.Clear()
            WRptView.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
            WRptView.ReportViewer1.LocalReport.ReportEmbeddedResource = ReportEmbeddedResource '"AluSet.ListImages01.rdlc" 'Parast06.rdlc"
            WRptView.ReportViewer1.LocalReport.EnableExternalImages = True
            'WRptView.ReportViewer1.LocalReport.ReportPath = My_AppPath() & "\08_Reports\Parast06.rdlc"

            ' Set the parameters for this report
            Dim paramList As New List(Of Microsoft.Reporting.WinForms.ReportParameter)()
            'paramList.Add(New ReportParameter("ImagePath", "file:" & My_AppPath() & "\MyFiles\GmHead.JPG"))
            Dim FPACount As Integer = 3
            Dim ar(FPACount - 1) As String
            ar = {"23", "13", "6.5"}
            'paramList.Add(New ReportParameter("ParFPA", ar))
            'paramList.Add(New ReportParameter("VisibleDetail", Not Me.CheckBoxSummary.Checked))
            'paramList.Add(New ReportParameter("DFrom", Me.DateTimePicker1.Value, True)) 'CDate(Format(DateTimePicker1.Value, "MM/dd/yyyy")), True))
            'paramList.Add(New ReportParameter("DTo", Me.DateTimePicker2.Value, True))
            'paramList.Add(New ReportParameter("VisibleColumnYP_VAL", True)) 'Hiden
            'Dim d As DataRowView = CType(ReportDataSource1.Value, DataView)(0) 'gmVMNewDataTable.DefaultView(0)
            'paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FooterTableChoice", d("MTRL_CODE").ToString.Substring(11, 1)))
            'WRptView.ReportViewer1.LocalReport.SetParameters(paramList)
            'Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Report1.rdlc"
            'Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
            'Me.ReportViewer1.Name = "ReportViewer1"
            'Me.ReportViewer1.Size = New System.Drawing.Size(565, 666)
            'Me.ReportViewer1.TabIndex = 0
            WRptView.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            'WRptView.ReportViewer1.ZoomMode = ZoomMode.Percent
            'WRptView.ReportViewer1.ZoomMode = ZoomMode.PageWidth
            'WRptView.ReportViewer1.ZoomPercent = 75

            'WRptView.ReportViewer1.SetDisplayMode(DisplayMode.Normal)
            WRptView.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            WRptView.ReportViewer1.RefreshReport()

            WRptView.WindowState = FormWindowState.Maximized
            WRptView.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class