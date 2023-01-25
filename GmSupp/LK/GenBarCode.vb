Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports BarcodeLib
Imports QRCoder

Public Class GenBarCode
    Private b As New BarcodeLib.Barcode()
    Dim WithEvents bb As New ToolStripButton

    Private Sub GenBarCode_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim temp As New Bitmap(1, 1)
        temp.SetPixel(0, 0, Me.BackColor)
        'barcode.Image = DirectCast(temp, Image)
        Me.barcodeGroupBox.BackgroundImageLayout = ImageLayout.Center
        Me.cbEncodeType.SelectedIndex = 17
        Me.cbBarcodeAlign.SelectedIndex = 0
        Me.cbLabelLocation.SelectedIndex = 0
        Me.chkGenerateLabel.Checked = True
        Me.cbRotateFlip.DataSource = System.[Enum].GetNames(GetType(RotateFlipType))

        Dim i As Integer = 0
        For Each o As Object In cbRotateFlip.Items
            If o.ToString().Trim().ToLower() = "rotatenoneflipnone" Then
                Exit For
            End If
            i += 1
        Next
        'foreach
        Me.cbRotateFlip.SelectedIndex = i

        'Show library version
        Me.tslblLibraryVersion.Text = "Barcode Library Version: " & BarcodeLib.Barcode.Version.ToString()

        Me.btnBackColor.BackColor = Me.b.BackColor
        Me.btnForeColor.BackColor = Me.b.ForeColor

        Me.txtData.Text = "2105050533 25072020 28072020 B1"

        bb.Image = CType(CType(PrintPreviewDialog1.Controls(1), ToolStrip).Items(0), ToolStripButton).Image ' Properties.Resources.PrintIcon
        'bb.DisplayStyle = ToolStripItemDisplayStyle.Image
        'b.Click += printPreview_PrintClick
        'Dim tls As ToolStrip = CType(printPreviewDialog1.Controls(1), ToolStrip) '..Item.Add(CType(bb, ToolStrip))
        'tls.Items.Add(bb)
        'bb = CType(CType(printPreviewDialog1.Controls(1), ToolStrip).Items(0), ToolStripButton)
        CType(PrintPreviewDialog1.Controls(1), ToolStrip).Items.RemoveAt(0)
        CType(PrintPreviewDialog1.Controls(1), ToolStrip).Items.Insert(0, bb)

    End Sub
    'Form1_Load

    Private Sub btnEncode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEncode.Click
        errorProvider1.Clear()
        'barcode.Image = Nothing
        Dim W As Integer = Convert.ToInt32(Me.txtWidth.Text.Trim())
        Dim H As Integer = Convert.ToInt32(Me.txtHeight.Text.Trim())
        b.Alignment = AlignmentPositions.CENTER

        Select Case cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower()
            Case "left"
                b.Alignment = AlignmentPositions.LEFT
            Case "right"
                b.Alignment = AlignmentPositions.RIGHT
            Case Else
                b.Alignment = AlignmentPositions.CENTER
        End Select

        Dim BType As BarcodeLib.TYPE = BarcodeLib.TYPE.UNSPECIFIED

        Select Case cbEncodeType.SelectedItem.ToString().Trim()
            Case "QR"
                SurroundingSub()
                Exit Sub
            Case "UPC-A"
                BType = BarcodeLib.TYPE.UPCA
            Case "UPC-E"
                BType = BarcodeLib.TYPE.UPCE
            Case "UPC 2 Digit Ext."
                BType = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_2DIGIT
            Case "UPC 5 Digit Ext."
                BType = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_5DIGIT
            Case "EAN-13"
                BType = BarcodeLib.TYPE.EAN13
            Case "JAN-13"
                BType = BarcodeLib.TYPE.JAN13
            Case "EAN-8"
                BType = BarcodeLib.TYPE.EAN8
            Case "ITF-14"
                BType = BarcodeLib.TYPE.ITF14
            Case "Codabar"
                BType = BarcodeLib.TYPE.Codabar
            Case "PostNet"
                BType = BarcodeLib.TYPE.PostNet
            Case "Bookland/ISBN"
                BType = BarcodeLib.TYPE.BOOKLAND
            Case "Code 11"
                BType = BarcodeLib.TYPE.CODE11
            Case "Code 39"
                BType = BarcodeLib.TYPE.CODE39
            Case "Code 39 Extended"
                BType = BarcodeLib.TYPE.CODE39Extended
            Case "Code 39 Mod 43"
                BType = BarcodeLib.TYPE.CODE39_Mod43
            Case "Code 93"
                BType = BarcodeLib.TYPE.CODE93
            Case "LOGMARS"
                BType = BarcodeLib.TYPE.LOGMARS
            Case "MSI"
                BType = BarcodeLib.TYPE.MSI_Mod10
            Case "Interleaved 2 of 5"
                BType = BarcodeLib.TYPE.Interleaved2of5
            'Case "Interleaved 2 of 5 Mod 10"
            '    BType = BarcodeLib.TYPE.Interleaved2of5_Mod10
            Case "Standard 2 of 5"
                BType = BarcodeLib.TYPE.Standard2of5
            'Case "Standard 2 of 5 Mod 10"
            '    BType = BarcodeLib.TYPE.Standard2of5_Mod10
            Case "Code 128"
                BType = BarcodeLib.TYPE.CODE128
            Case "Code 128-A"
                BType = BarcodeLib.TYPE.CODE128A
            Case "Code 128-B"
                BType = BarcodeLib.TYPE.CODE128B
            Case "Code 128-C"
                BType = BarcodeLib.TYPE.CODE128C
            Case "Telepen"
                BType = BarcodeLib.TYPE.TELEPEN
            Case "FIM"
                BType = BarcodeLib.TYPE.FIM
            Case "Pharmacode"
                BType = BarcodeLib.TYPE.PHARMACODE
            Case Else
                MessageBox.Show("Please specify the encoding type.")
        End Select

        Try

            If BType <> BarcodeLib.TYPE.UNSPECIFIED Then

                Try
                    b.BarWidth = If(textBoxBarWidth.Text.Trim().Length < 1, Nothing, CType(Convert.ToInt32(textBoxBarWidth.Text.Trim()), Integer?))
                Catch ex As Exception
                    Throw New Exception("Unable to parse BarWidth: " & ex.Message, ex)
                End Try

                Try
                    b.AspectRatio = If(textBoxAspectRatio.Text.Trim().Length < 1, Nothing, CType(Convert.ToDouble(textBoxAspectRatio.Text.Trim()), Double?))
                Catch ex As Exception
                    Throw New Exception("Unable to parse AspectRatio: " & ex.Message, ex)
                End Try

                b.IncludeLabel = Me.chkGenerateLabel.Checked
                b.RotateFlipType = CType([Enum].Parse(GetType(RotateFlipType), Me.cbRotateFlip.SelectedItem.ToString(), True), RotateFlipType)

                If Not String.IsNullOrEmpty(Me.textBox1.Text.Trim()) Then
                    b.AlternateLabel = Me.textBox1.Text
                Else
                    b.AlternateLabel = Me.txtData.Text
                End If

                Select Case Me.cbLabelLocation.SelectedItem.ToString().Trim().ToUpper()
                    Case "BOTTOMLEFT"
                        b.LabelPosition = LabelPositions.BOTTOMLEFT
                    Case "BOTTOMRIGHT"
                        b.LabelPosition = LabelPositions.BOTTOMRIGHT
                    Case "TOPCENTER"
                        b.LabelPosition = LabelPositions.TOPCENTER
                    Case "TOPLEFT"
                        b.LabelPosition = LabelPositions.TOPLEFT
                    Case "TOPRIGHT"
                        b.LabelPosition = LabelPositions.TOPRIGHT
                    Case Else
                        b.LabelPosition = LabelPositions.BOTTOMCENTER
                End Select

                barcodeGroupBox.BackgroundImage = b.Encode(BType, Me.txtData.Text.Trim(), Me.btnForeColor.BackColor, Me.btnBackColor.BackColor, W, H)

                Me.lblEncodingTime.Text = "(" & Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() & "ms)"
                txtEncoded.Text = b.EncodedValue
                tsslEncodedType.Text = "Encoding Type: " & b.EncodedType.ToString()
                If b.BarWidth.HasValue Then txtWidth.Text = b.Width.ToString()
                If b.AspectRatio.HasValue Then txtHeight.Text = b.Height.ToString()
            End If

            'barcodeGroupBox.Location = New Point((Me.barcodeGroupBox.Location.X + Me.barcodeGroupBox.Width / 2) - barcodeGroupBox.Width / 2, (Me.barcodeGroupBox.Location.Y + Me.barcodeGroupBox.Height / 2) - barcodeGroupBox.Height / 2)
            Me.barcodeGroupBox.Location = New Point((Me.barcodeGroupBox.Location.X + Me.barcodeGroupBox.Width / 2) - Me.barcodeGroupBox.Width / 2, (Me.barcodeGroupBox.Location.Y + Me.barcodeGroupBox.Height / 2) - Me.barcodeGroupBox.Height / 2)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnEncode1_Click(sender As System.Object, e As System.EventArgs)  ' Handles btnEncode.Click
        'errorProvider1.Clear()
        'Dim W As Integer = Convert.ToInt32(Me.txtWidth.Text.Trim())
        'Dim H As Integer = Convert.ToInt32(Me.txtHeight.Text.Trim())
        'b.Alignment = Me.barcodeGroupBoxLib.AlignmentPositions.CENTER

        ''barcode alignment
        'Select Case cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower()
        '    Case "left"
        '        b.Alignment = BarcodeLib.AlignmentPositions.LEFT
        '        Exit Select
        '    Case "right"
        '        b.Alignment = BarcodeLib.AlignmentPositions.RIGHT
        '        Exit Select
        '    Case Else
        '        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
        '        Exit Select
        'End Select
        ''switch
        'Dim type As BarcodeLib.TYPE = BarcodeLib.TYPE.UNSPECIFIED
        'Select Case cbEncodeType.SelectedItem.ToString().Trim()
        '    Case "UPC-A"
        '        type = BarcodeLib.TYPE.UPCA
        '        Exit Select
        '    Case "UPC-E"
        '        type = BarcodeLib.TYPE.UPCE
        '        Exit Select
        '    Case "UPC 2 Digit Ext."
        '        type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_2DIGIT
        '        Exit Select
        '    Case "UPC 5 Digit Ext."
        '        type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_5DIGIT
        '        Exit Select
        '    Case "EAN-13"
        '        type = BarcodeLib.TYPE.EAN13
        '        Exit Select
        '    Case "JAN-13"
        '        type = BarcodeLib.TYPE.JAN13
        '        Exit Select
        '    Case "EAN-8"
        '        type = BarcodeLib.TYPE.EAN8
        '        Exit Select
        '    Case "ITF-14"
        '        type = BarcodeLib.TYPE.ITF14
        '        Exit Select
        '    Case "Codabar"
        '        type = BarcodeLib.TYPE.Codabar
        '        Exit Select
        '    Case "PostNet"
        '        type = BarcodeLib.TYPE.PostNet
        '        Exit Select
        '    Case "Bookland/ISBN"
        '        type = BarcodeLib.TYPE.BOOKLAND
        '        Exit Select
        '    Case "Code 11"
        '        type = BarcodeLib.TYPE.CODE11
        '        Exit Select
        '    Case "Code 39"
        '        type = BarcodeLib.TYPE.CODE39
        '        Exit Select
        '    Case "Code 39 Extended"
        '        type = BarcodeLib.TYPE.CODE39Extended
        '        Exit Select
        '    Case "Code 93"
        '        type = BarcodeLib.TYPE.CODE93
        '        Exit Select
        '    Case "LOGMARS"
        '        type = BarcodeLib.TYPE.LOGMARS
        '        Exit Select
        '    Case "MSI"
        '        type = BarcodeLib.TYPE.MSI_Mod10
        '        Exit Select
        '    Case "Interleaved 2 of 5"
        '        type = BarcodeLib.TYPE.Interleaved2of5
        '        Exit Select
        '    Case "Standard 2 of 5"
        '        type = BarcodeLib.TYPE.Standard2of5
        '        Exit Select
        '    Case "Code 128"
        '        type = BarcodeLib.TYPE.CODE128
        '        Exit Select
        '    Case "Code 128-A"
        '        type = BarcodeLib.TYPE.CODE128A
        '        Exit Select
        '    Case "Code 128-B"
        '        type = BarcodeLib.TYPE.CODE128B
        '        Exit Select
        '    Case "Code 128-C"
        '        type = BarcodeLib.TYPE.CODE128C
        '        Exit Select
        '    Case "Telepen"
        '        type = BarcodeLib.TYPE.TELEPEN
        '        Exit Select
        '    Case "FIM"
        '        type = BarcodeLib.TYPE.FIM
        '        Exit Select
        '    Case "Pharmacode"
        '        type = BarcodeLib.TYPE.PHARMACODE
        '        Exit Select
        '    Case Else
        '        MessageBox.Show("Please specify the encoding type.")
        '        Exit Select
        'End Select
        ''switch
        'Try
        '    If type <> BarcodeLib.TYPE.UNSPECIFIED Then
        '        b.IncludeLabel = Me.chkGenerateLabel.Checked

        '        b.RotateFlipType = DirectCast([Enum].Parse(GetType(RotateFlipType), Me.cbRotateFlip.SelectedItem.ToString(), True), RotateFlipType)

        '        'label alignment and position
        '        Select Case Me.cbLabelLocation.SelectedItem.ToString().Trim().ToUpper()
        '            Case "BOTTOMLEFT"
        '                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT
        '                Exit Select
        '            Case "BOTTOMRIGHT"
        '                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMRIGHT
        '                Exit Select
        '            Case "TOPCENTER"
        '                b.LabelPosition = BarcodeLib.LabelPositions.TOPCENTER
        '                Exit Select
        '            Case "TOPLEFT"
        '                b.LabelPosition = BarcodeLib.LabelPositions.TOPLEFT
        '                Exit Select
        '            Case "TOPRIGHT"
        '                b.LabelPosition = BarcodeLib.LabelPositions.TOPRIGHT
        '                Exit Select
        '            Case Else
        '                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER
        '                Exit Select
        '        End Select
        '        'switch
        '        '===== Encoding performed here =====
        '        Barcode.Image = b.Encode(type, Me.txtData.Text.Trim(), Me.btnForeColor.BackColor, Me.btnBackColor.BackColor, W, H)
        '        '===================================
        '        Dim ms As New IO.MemoryStream
        '        Barcode.Image.Save(ms, Imaging.ImageFormat.Jpeg)

        '        'show the encoding time
        '        Me.lblEncodingTime.Text = "(" & Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() & "ms)"

        '        txtEncoded.Text = b.EncodedValue

        '        tsslEncodedType.Text = "Encoding Type: " & b.EncodedType.ToString()
        '    End If
        '    'if
        '    Barcode.Width = Barcode.Image.Width
        '    Barcode.Height = Barcode.Image.Height

        '    'reposition the barcode image to the middle
        '    Barcode.Location = New Point((Me.barcodeGroupBox.Location.X + Me.barcodeGroupBox.Width / 2) - Barcode.Width / 2, (Me.barcodeGroupBox.Location.Y + Me.barcodeGroupBox.Height / 2) - Barcode.Height / 2)
        '    'try
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        ''catch
    End Sub
    'btnEncode_Click
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tif)|*.tif"
        sfd.FilterIndex = 2
        sfd.AddExtension = True
        If sfd.ShowDialog() = DialogResult.OK Then
            Dim savetype As BarcodeLib.SaveTypes = BarcodeLib.SaveTypes.UNSPECIFIED
            Select Case sfd.FilterIndex
                Case 1
                    ' BMP 
                    savetype = BarcodeLib.SaveTypes.BMP
                    Exit Select
                Case 2
                    ' GIF 
                    savetype = BarcodeLib.SaveTypes.GIF
                    Exit Select
                Case 3
                    ' JPG 
                    savetype = BarcodeLib.SaveTypes.JPG
                    Exit Select
                Case 4
                    ' PNG 
                    savetype = BarcodeLib.SaveTypes.PNG
                    Exit Select
                Case 5
                    ' TIFF 
                    savetype = BarcodeLib.SaveTypes.TIFF
                    Exit Select
                Case Else
                    Exit Select
            End Select
            'switch
            b.SaveImage(sfd.FileName, savetype)
        End If
        'if
    End Sub
    'btnSave_Click
    Private Sub splitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs)
        barcodeGroupBox.Location = New Point((Me.barcodeGroupBox.Location.X + Me.barcodeGroupBox.Width / 2) - barcodeGroupBox.Width / 2, (Me.barcodeGroupBox.Location.Y + Me.barcodeGroupBox.Height / 2) - barcodeGroupBox.Height / 2)
    End Sub
    'splitContainer1_SplitterMoved
    Private Sub btnForeColor_Click(sender As Object, e As EventArgs) Handles btnForeColor.Click
        Using cdialog As New ColorDialog()
            cdialog.AnyColor = True
            If cdialog.ShowDialog() = DialogResult.OK Then
                Me.b.ForeColor = cdialog.Color
                Me.btnForeColor.BackColor = cdialog.Color
                'if
            End If
        End Using
        'using
    End Sub
    'btnForeColor_Click
    Private Sub btnBackColor_Click(sender As Object, e As EventArgs) Handles btnBackColor.Click
        Using cdialog As New ColorDialog()
            cdialog.AnyColor = True
            If cdialog.ShowDialog() = DialogResult.OK Then
                Me.b.BackColor = cdialog.Color
                Me.btnBackColor.BackColor = cdialog.Color
                'if
            End If
        End Using
        'using
    End Sub
    'btnBackColor_Click
    Private Sub btnSaveXML_Click(sender As Object, e As EventArgs) Handles btnSaveXML.Click
        btnEncode_Click(sender, e)

        Using sfd As New SaveFileDialog()
            sfd.Filter = "XML Files|*.xml"
            If sfd.ShowDialog() = DialogResult.OK Then
                Using sw As New System.IO.StreamWriter(sfd.FileName)
                    sw.Write(b.XML)
                    'using
                End Using
                'if
            End If
        End Using
        'using
    End Sub
    'btnGetXML_Click
    Private Sub btnLoadXML_Click(sender As Object, e As EventArgs) Handles btnLoadXML.Click
        Using ofd As New OpenFileDialog()
            ofd.Multiselect = False
            If ofd.ShowDialog() = DialogResult.OK Then
                Using XML As New BarcodeLib.BarcodeXML()
                    XML.ReadXml(ofd.FileName)

                    'load image from xml
                    Me.barcodeGroupBox.Width = XML.Barcode(0).ImageWidth
                    Me.barcodeGroupBox.Height = XML.Barcode(0).ImageHeight
                    Me.barcodeGroupBox.BackgroundImage = BarcodeLib.Barcode.GetImageFromXML(XML)

                    'populate the screen
                    Me.txtData.Text = XML.Barcode(0).RawData
                    Me.chkGenerateLabel.Checked = XML.Barcode(0).IncludeLabel

                    Select Case XML.Barcode(0).Type
                        Case "UCC12", "UPCA"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("UPC-A")
                            Exit Select
                        Case "UCC13", "EAN13"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("EAN-13")
                            Exit Select
                        Case "Interleaved2of5"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Interleaved 2 of 5")
                            Exit Select
                        Case "Industrial2of5", "Standard2of5"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Standard 2 of 5")
                            Exit Select
                        Case "LOGMARS"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("LOGMARS")
                            Exit Select
                        Case "CODE39"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 39")
                            Exit Select
                        Case "CODE39Extended"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 39 Extended")
                            Exit Select
                        Case "Codabar"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Codabar")
                            Exit Select
                        Case "PostNet"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("PostNet")
                            Exit Select
                        Case "ISBN", "BOOKLAND"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Bookland/ISBN")
                            Exit Select
                        Case "JAN13"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("JAN-13")
                            Exit Select
                        Case "UPC_SUPPLEMENTAL_2DIGIT"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("UPC 2 Digit Ext.")
                            Exit Select
                        Case "MSI_Mod10", "MSI_2Mod10", "MSI_Mod11", "MSI_Mod11_Mod10", "Modified_Plessey"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("MSI")
                            Exit Select
                        Case "UPC_SUPPLEMENTAL_5DIGIT"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("UPC 5 Digit Ext.")
                            Exit Select
                        Case "UPCE"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("UPC-E")
                            Exit Select
                        Case "EAN8"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("EAN-8")
                            Exit Select
                        Case "USD8", "CODE11"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 11")
                            Exit Select
                        Case "CODE128"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 128")
                            Exit Select
                        Case "CODE128A"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 128-A")
                            Exit Select
                        Case "CODE128B"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 128-B")
                            Exit Select
                        Case "CODE128C"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 128-C")
                            Exit Select
                        Case "ITF14"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("ITF-14")
                            Exit Select
                        Case "CODE93"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Code 93")
                            Exit Select
                        Case "FIM"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("FIM")
                            Exit Select
                        Case "Pharmacode"
                            Me.cbEncodeType.SelectedIndex = Me.cbEncodeType.FindString("Pharmacode")
                            Exit Select
                        Case Else

                            Throw New Exception("ELOADXML-1: Unsupported encoding type in XML.")
                    End Select
                    'switch
                    Me.txtEncoded.Text = XML.Barcode(0).EncodedValue
                    Me.btnForeColor.BackColor = ColorTranslator.FromHtml(XML.Barcode(0).Forecolor)
                    Me.btnBackColor.BackColor = ColorTranslator.FromHtml(XML.Barcode(0).Backcolor)


                    Me.txtWidth.Text = XML.Barcode(0).ImageWidth.ToString()
                    Me.txtHeight.Text = XML.Barcode(0).ImageHeight.ToString()

                    'populate the local object
                    btnEncode_Click(sender, e)

                    'reposition the barcode image to the middle
                    barcodeGroupBox.Location = New Point((Me.barcodeGroupBox.Location.X + Me.barcodeGroupBox.Width / 2) - barcodeGroupBox.Width / 2, (Me.barcodeGroupBox.Location.Y + Me.barcodeGroupBox.Height / 2) - barcodeGroupBox.Height / 2)
                    'using
                End Using
                'if
            End If
        End Using
        'using
    End Sub



    Private Sub PrintPreviewButton_Click(sender As Object, e As EventArgs) Handles PrintPreviewButton.Click
        'PrintDocument1.
        PrintPreviewDialog1.Size = New System.Drawing.Size(800, 500)
        PrintPreviewDialog1.Document = GmPrintDocument ' PreparePrintDocument()
        PrintPreviewDialog1.ShowDialog()
    End Sub

    ' Make and return a PrintDocument object that's ready
    ' to print the paragraphs.
    Private Function PreparePrintDocument() As PrintDocument
        ' Make the PrintDocument object.
        Dim print_document As New PrintDocument

        ' Install the PrintPage event handler.
        AddHandler print_document.PrintPage, AddressOf Print_PrintPage

        ' Return the object.
        Return print_document
    End Function

    ' Print the next page.
    Private Sub Print_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        ' Draw a rectangle at the margins.
        e.Graphics.DrawRectangle(Pens.Black, e.MarginBounds)

        ' Draw a thick, dashed ellipse.
        Dim dotted_pen As New Pen(Color.Black, 5)
        dotted_pen.DashStyle = Drawing2D.DashStyle.Dash
        e.Graphics.DrawEllipse(dotted_pen, e.MarginBounds)
        dotted_pen.Dispose()

        ' Draw a thick diamond.
        Dim x0 As Integer = e.MarginBounds.X
        Dim y0 As Integer = e.MarginBounds.Y
        Dim wid As Integer = e.MarginBounds.Width
        Dim hgt As Integer = e.MarginBounds.Height
        Dim pts() As Point = {
            New Point(x0, y0 + hgt \ 2),
            New Point(x0 + wid \ 2, y0),
            New Point(x0 + wid, y0 + hgt \ 2),
            New Point(x0 + wid \ 2, y0 + hgt)
        }
        e.Graphics.DrawPolygon(New Pen(Color.Black, 5), pts)

        ' There are no more pages.
        e.HasMorePages = False
    End Sub
    Private Sub bb_Click(sender As Object, e As EventArgs) Handles bb.Click
        Try
            PrintPreviewDialog1.Document = GmPrintDocument 'PreparePrintDocument()

            'If printPreviewDialog1.ShowDialog() = DialogResult.OK Then
            '    'printDocument1.Print()
            'End If

            PrintDialog1.Document = GmPrintDocument '.PrintPreviewDialog1.Document 'PreparePrintDocument()

            ' Display the print dialog.
            PrintDialog1.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message, ToString())
        End Try
    End Sub

    Private Sub GmPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles GmPrintDocument.PrintPage
        ' Draw a rectangle at the margins.
        'e.Graphics.DrawRectangle(Pens.Black, e.MarginBounds)
        'e.Graphics.VisibleClipBounds.Size = SizeF(700, 400)
        Dim pt As Point
        pt.X = 0
        pt.Y = 0
        e.Graphics.DrawImage(barcodeGroupBox.BackgroundImage, pt)

        '' Draw a thick, dashed ellipse.
        'Dim dotted_pen As New Pen(Color.Black, 5)
        'dotted_pen.DashStyle = Drawing2D.DashStyle.Dash
        'e.Graphics.DrawEllipse(dotted_pen, e.MarginBounds)
        'dotted_pen.Dispose()

        '' Draw a thick diamond.
        'Dim x0 As Integer = e.MarginBounds.X
        'Dim y0 As Integer = e.MarginBounds.Y
        'Dim wid As Integer = e.MarginBounds.Width
        'Dim hgt As Integer = e.MarginBounds.Height
        'Dim pts() As Point = {
        '    New Point(x0, y0 + hgt \ 2),
        '    New Point(x0 + wid \ 2, y0),
        '    New Point(x0 + wid, y0 + hgt \ 2),
        '    New Point(x0 + wid \ 2, y0 + hgt)
        '}
        'e.Graphics.DrawPolygon(New Pen(Color.Black, 5), pts)

        ' There are no more pages.
        e.HasMorePages = False
    End Sub

    Private Sub PageSetupDialogButton_Click(sender As Object, e As EventArgs) Handles PageSetupDialogButton.Click
        Me.PageSetupDialog1.Document = GmPrintDocument
        Me.PageSetupDialog1.ShowDialog()
    End Sub

    Private Sub SurroundingSub()
        Dim qrGenerator As QRCodeGenerator = New QRCodeGenerator()
        'Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q)
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(Me.txtData.Text, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As QRCode = New QRCode(qrCodeData)
        Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)
        barcodeGroupBox.BackgroundImage = qrCodeImage
    End Sub
End Class
