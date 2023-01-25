Imports System.IO
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

Public Class TestBarcode

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TextBox1.Text = "[02]L".Replace("[01]", Chr(1)).Replace("[02]", Chr(2)) & vbCrLf &
"n" & vbCrLf &
"H30" & vbCrLf &
"Q0001" & vbCrLf &
"1X1100000020016L279002" & vbCrLf &
"1X1100000300016L279002" & vbCrLf &
"1X1100000020012L004030" & vbCrLf &
"1X1100000020295L004030" & vbCrLf &
"1911A0600090035ABCDEFGHI" & vbCrLf &
"111100001050210Ref000001" & vbCrLf &
"111100001200210MRU00003" & vbCrLf &
"1911A0600320100123456" & vbCrLf &
"1O1102100550020ABC2345RTE" & vbCrLf &
"E" & vbCrLf
        '        [02]n
        '[02]c0100
        '[02]L
        '[02] O0080
        '        D11()
        '        Q0001()
        '1:      X1100000020016L279002()
        '1:      X1100000800016L279002()
        '1:      X1100000020012L004080()
        '1:      X1100000020295L004080()
        '1F3304000000000827495610932
        '        e()
        '-----------------------------



        '        ^XA
        '^FO150,100
        '^BY3,3,300
        '^BCR,500,Y,N,N
        '^FD2105050533 250720 280720 1^FS
        '^XZ
        '        ^XA
        '^FO700,50
        '^A0R,90,90
        '^FD2105050533 250720 280720 B1^FS
        '^FO150,100
        '^BY3
        '^BCR,500,Y,N,N
        '^FD2105050533 250720 280720 B1^FS
        '^XZ

        '        ^XA
        '^FO700,50
        '^A0R,90,90
        '^FD2105050533 250720 280720 B1^FS
        '^FO150,100
        '^BQN,2,10
        '^FDMM,A2105050533 25072020 28072020 14:35 ABCDEF$^FS
        '^XZ

        Me.QRCodeToolStripMenuItem.PerformClick()

        Me.KeyPreview = True
    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            'Me.cmdSelect.PerformClick()
        End If
        If e.KeyCode = Keys.F4 Then
            Me.PrintToolStripMenuItem1.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview Is True, And this Is from the FORM.")
        End If
    End Sub

    Dim m_Document As String = ""

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripMenuItem1.Click
        'Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = My_AppPath() & "\MyFiles"
        openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                m_Document = openFileDialog1.FileName
                LoadData()
                BindControls()
                'If (myStream IsNot Nothing) Then
                '    ' Insert code to read the stream here.
                'End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open.
                'If (myStream IsNot Nothing) Then
                '    myStream.Close()
                'End If
            End Try
        End If

    End Sub
    ' Load the data.
    Private Sub LoadData()

        If File.Exists(m_Document) Then
            Try
                'Fill or Create xml_dataset
                'Dim XMLEncrypt As New XMLEncryptor(False)
                'xml_dataset = XMLEncrypt.ReadEncryptedXML(m_Document)
                xml_dataset = New DataSet
                xml_dataset.ReadXml(m_Document)
                xml_dataset.AcceptChanges()
            Catch ex As FileNotFoundException
                'SqlSettingsForm.ShowDialog()
                ' Εαν Οχι τότε η εφαρμογή Διακόπτετε
                MsgBox(ex.Message)
                'If ExitForm = TypeExitForm.LoginError Then
                'ExitForm = TypeExitForm.LoginError
                Me.Close() 'End
            End Try
        Else
            Me.createConXml()
            xml_dataset.AcceptChanges()
        End If
    End Sub

    ' Bind the controls to the data tables.
    Private Sub BindControls()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource
        Me.BindingSource1.DataSource = xml_dataset
        Me.BindingSource1.DataMember = "TestBarCode"
        Me.BindingNavigator1.BindingSource = Me.BindingSource1
        Me.DataGridView1.DataSource = Me.BindingSource1
        Me.BindingSource1.Sort = "Type ASC,Line ASC"
    End Sub
    ' Save the data.
    Private Sub SaveData()
        Try
            Dim dt As DataTable = xml_dataset.Tables("TestBarCode")
            If xml_dataset.HasChanges = True Then
                'Dim XMLEncrypt As New XMLEncryptor(False)
                'XMLEncrypt.WriteEncryptedXML(xml_dataset, m_Document)
                xml_dataset.AcceptChanges()
                xml_dataset.WriteXml(m_Document) '"data.xml")
            End If
        Catch ex As Exception
            'ExitForm = 99
        End Try
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripMenuItem1.Click
        createConXml()
        BindControls()
        m_Document = My_AppPath() & "\MyFiles\TestBarCode1.xml"
    End Sub
    Private Sub createConXml()
        Dim tbl As New DataTable
        tbl.TableName = "TestBarCode"
        Dim col As DataColumn
        col = New DataColumn
        col.ColumnName = "Line"
        tbl.Columns.Add(col)
        '
        col = New DataColumn
        col.ColumnName = "Type"
        col.DefaultValue = "2B"
        tbl.Columns.Add(col)
        '
        col = New DataColumn
        col.ColumnName = "Prefix"
        col.DefaultValue = String.Empty
        tbl.Columns.Add(col)
        '
        col = New DataColumn
        col.ColumnName = "Text"
        col.DefaultValue = String.Empty
        tbl.Columns.Add(col)
        '
        xml_dataset = New DataSet
        xml_dataset.Tables.Add(tbl)

        'xml_dataset.AcceptChanges()
        'xml_dataset.WriteXml(m_Document) '"data.xml")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripMenuItem1.Click
        SaveData()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click, SaveAsToolStripMenuItem1.Click
        'Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.InitialDirectory = My_AppPath() & "\MyFiles"
        saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            m_Document = saveFileDialog1.FileName
            Me.BindingSource1.EndEdit()
            SaveData()
            'myStream = saveFileDialog1.OpenFile()
            'If (myStream IsNot Nothing) Then
            '    ' Code to write the stream goes here.
            '    myStream.Close()
            'End If
        End If

    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click, PrintToolStripMenuItem1.Click
        Dim s As String = TextBox1.Text
        Dim pd As New PrintDialog()

        ' You need a string to send.
        s = "Hello, this is a test"
        ' Open the printer dialog box, and then allow the user to select a printer.
        pd.PrinterSettings = New PrinterSettings()
        If pd.ShowDialog = DialogResult.OK Then
            Dim ss = Me.TextBox1.Text
            If Me.tlsCounter.Text > 1 Then
                For I As Integer = 0 To Me.tlsCounter.Text - 1
                    ss &= Me.TextBox1.Text
                Next
            End If
            RawPrinterHelper.gmSendStringToPrinter(pd.PrinterSettings.PrinterName, ss)
        End If
    End Sub


    Function My_AppPath() As String
        If My.Application.IsNetworkDeployed Then
            'm_Document = ApplicationDeployment.CurrentDeployment.DataDirectory & "\GmFiles\GenMenu.xml"
            'm_Document = Environment.CurrentDirectory.ToString & "\MyFiles\GmApp.xml"
            Return My.Application.Deployment.DataDirectory.ToString
        Else
            'm_Document = Application.StartupPath & "\GmApp.xml"
            Return My.Application.Info.DirectoryPath
        End If
    End Function

    Private Sub TlSBtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnRefresh.Click
    End Sub

    Private Sub TlSBtnResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnResult.Click
        TextBox1.Text = String.Empty
        Dim dv As DataView = xml_dataset.Tables("TestBarCode").DefaultView
        dv.Sort = "Type ASC,Line ASC"
        For Each drv As DataRowView In dv
            Select Case drv("Type")
                Case "1H"
                    TextBox1.Text &= drv("Prefix").ToString.Replace("[01]", Chr(1)).Replace("[02]", Chr(2)) & vbCrLf
                Case "2B"
                    Dim sb() As String = drv("Text").ToString.Split("]")
                    If sb.Length > 1 Then
                        Dim ss As String = ""
                        For j As Integer = 0 To sb.Length - 1
                            Dim TokenNo As Integer = sb(j).IndexOf("[")
                            If Not TokenNo = -1 Then
                                ss &= sb(j).Substring(0, sb(j).IndexOf("[")) + sb(j).Substring(sb(j).IndexOf("[") + 1)
                            Else
                                ss &= sb(j)
                            End If
                        Next
                        TextBox1.Text &= drv("Prefix") & ss & vbCrLf
                    Else
                        TextBox1.Text &= drv("Prefix") & sb(0) & vbCrLf
                    End If
                Case "3F"
                    TextBox1.Text &= drv("Prefix") & vbCrLf
            End Select
        Next

    End Sub

    Private Sub QRCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QRCodeToolStripMenuItem.Click

        '^FO command sets a field origin, relative to the label home (^LH) position. ^FO sets the upper-left
        'corner of the field area by defining points along the x-axis And y-axis independent of the rotation.
        'Format: ^FOx,y,z
        '^BQ command produces a matrix symbology consisting of an array of nominally square modules
        'arranged in an overall square pattern. A unique pattern at three of the symbol's four corners assists in
        'determining bar code size, position, And inclination.
        'A wide range Of symbol sizes Is possible, along with four levels of error correction. User-specified module
        'dimensions provide a wide variety of symbol production techniques.
        'QR Code Model 1 Is the original specification, while QR Code Model 2 Is an enhanced form of the
        'symbology.Model 2 provides additional features And can be automatically differentiated from Model 1.
        'Model 2 Is the recommended model And should normally be used.
        'This bar code Is printed using field data specified in a subsequent ^FD string.
        'Encodable character sets include numeric data, alphanumeric data, 8 - bit Byte data, And Kanji characters.
        'IMPORTANT: If additional Then information about this bar code Is required, go To www.aimglobal.org.
        'Format: ^BQa,b,c,d,e
        'a = field orientation Values: normal(^ FW has no effect On rotation)
        'b = model Values: 1 (original) And 2 (enhanced – recommended) Default:  2
        'c = magnification factor Values: 1 to 10
        'Default
        '1 on 150 dpi printers
        '2 on 200 dpi printers
        '3 on 300 dpi printers
        '6 on 600 dpi printers


        'QR Switches(formatted into the ^FD field data)
        'There are 4 switch fields that are allowed, some with associated parameters And some without. Two of
        'these fields are always present, one Is optional, And one's presence depends on the value of another. The
        '        switches are always placed In a fixed order. The four switches, in order are
        'Mixed mode < D > iijjxx,Optional (note that this switch ends with a comma “,”)
        'Error correction level <H, Q, M, L>Mandatory
        'Data Input() < A, M >, Mandatory(note that this switch ends With a comma “,”)
        'Character Mode < N, A, Bdddd, K > Conditional(present If data input Is M)
        '1 M = error correction level (standard-high reliability level
        '2 M, = manual input
        '3 A = alphanumeric data
        '4 AC-42 = data character string

        Me.TextBox1.Text = "^XA" & vbCrLf &
                    "^FO700,50" & vbCrLf &
        "^A0R,90,90" & vbCrLf &
        "^FD2105050533 250720 280720 B1^FS" & vbCrLf &
        "^FO150,100" & vbCrLf &
        "^BQN,2,10" & vbCrLf &
        "^FDMM,A2105050533 25072020 28072020 14:35 ABCDEF$^FS" & vbCrLf &
        "^XZ" & vbCrLf
    End Sub

    Private Sub BarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeToolStripMenuItem.Click

        '^XA command Is used at the beginning of ZPL II code. It Is the opening bracket And indicates the start
        'of a New label format. This command Is substituted with a single ASCII control character STX (control-B,
        'hexadecimal 2).
        'Format: ^XA
        'Comments: Valid ZPL II format requires that label formats should start with the ^XA command And end
        'With the ^ XZ command.

        '^FO command sets a field origin, relative to the label home (^LH) position. ^FO sets the upper-left
        'corner of the field area by defining points along the x-axis And y-axis independent of the rotation.
        'Format: ^FOx,y,z

        '^A command specifies the font to use in a text field. ^A designates the font for the current ^FD
        'statement Or field.The font specified by ^A Is used only once for that ^FD entry. If a value for ^A Is Not
        'specified again, the default
        'R = rotated 90 degrees (clockwise)
        'H = 90,W = 90

        '^BY command Is used To change the Default values For the Module width (In dots), the wide bar To
        'narrow bar width ratio And the bar code height (in dots). It can be used as often as necessary within a label
        'Format.
        'Format() : ^BYw,r,h

        '^BC command creates the Code 128 bar code, a high-density, variable length, continuous,
        'alphanumeric symbology. It was designed for complexly encoded product identification.
        'Code 128 has three subsets Of characters. There are 106 encoded printing characters In Each Set, And
        'each character can have up to three different meanings, depending on the character subset being used.
        'Each Code 128 character consists of six elements: three bars And three spaces.
        '• ^BC supports a fixed print ratio.
        '• Field data (^FD) Is limited to the width (Or length, if rotated) of the label.
        'IMPORTANT: If additional Then information about this bar code Is required, go To www.aimglobal.org.
        'Format: ^BCo,h,f,g,e,m
        'R = rotated 90 degrees (clockwise)
        'h = bar code height (in dots) = 500

        '^FD command defines the data string for a field. The field data can be any printable character except
        'those used as command prefixes (^ And ~).
        'In RFID printers, it can also be used to specify passwords to write to tags.
        'Format: ^FDa
        'a = Values: any data string up to 3072 bytes

        '^XZ command Is the ending (closing) bracket. It indicates the end of a label format. When
        'this Command() Is received, a label prints. This command can also be issued as a single ASCII control
        'character ETX(Control - C, hexadecimal 03).
        'Format:  ^XZ
        'Comments: Lab

        Me.TextBox1.Text = "^XA" & vbCrLf &
                    "^FO700,50" & vbCrLf &
        "^A0R,90,90" & vbCrLf &
        "^FD2105050533 250720 280720 B1^FS" & vbCrLf &
        "^FO150,100" & vbCrLf &
        "^BY3" & vbCrLf &
        "^BCR,500,Y,N,N" & vbCrLf &
        "^FD2105050533 250720 280720 1$^FS" & vbCrLf &
        "^XZ" & vbCrLf
    End Sub
End Class
