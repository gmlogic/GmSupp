Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Transactions
Imports GmSupp.Hglp
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
'Imports NPOI.HPSF
'Imports NPOI.HSSF.UserModel
'Imports Microsoft.Office.Interop
Imports Softone
Imports System.Reflection
Imports System.Data.OleDb
Imports GmSupp
''---- for GmS1Lib -----
'Imports GmS1Lib.Hglp
'Imports System.Windows.Forms
''---- for GmS1Lib -----

Public Class OpenItemBR
#Region "01-Declare Variables"
    'Dim df As GmData
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Dim fS1HiddenForm As New Form
    Dim conn As String
    Dim whLst As New List(Of Integer)
    Dim CurCheque As Integer
    Dim fvscs As New List(Of vsc)
    Dim vscs As New List(Of vsc)
    Dim chkVscL As List(Of vsc)
    Dim addVsc As New vsc
    Private CompanyT As Short = 1000
    Dim dt As New DataTable
    Dim conStr As String = ""
    Dim sheet As String = ""
    Dim CellSum As Double
    Dim impExcel As Boolean
#End Region
#Region "02-Declare Propertys"
    Public Property Series As Integer
    Public Property NSOSOURCE As Integer
    Public Property NSeries As Integer
#End Region
#Region "03-Load Form"
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
        DateTimePicker1.Value = CDate("01/01/" & Year(CTODate))
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))

        ''---- for GmS1Lib -----
        Dim s As Form = sender
        'If IsNothing(s.ParentForm) Then
        '        ? s.ParentForm.CompanyName
        '"GmSupp"
        '    Me.Tag = "Αντιστοιχίσεις"
        '    GenMenu.SetDBs(GenMenu.dbs.Hglp)
        'End If
        ''---- for GmS1Lib -----

        Dim Pass = ""
        'MsgBox("Καλή Χρονιά !!!" & vbCrLf & "  -- 2018 --", MsgBoxStyle.Information)
        Me.txtFields_CODE.Text = "*"
        Me.txtProFINCODE.Text = "0"

        If LocalIP = "192.168.10.108" Then
            'DateTimePicker1.Value = StartDate 'CDate("01/" & CTODate.Month & "/" & Year(CTODate))
            'DateTimePicker2.Value = CDate("30/03/2018")
            'Me.txtFields_CODE.Text = "2400020000*" '"2103030071*"
            'Me.TlSTxtWHOUSE.Text = "2"
            'Me.TlSTxtTPRMS.Text = "2041,2521,2523"
            'Me.radioBtnItemsStatement.Checked = True
        End If
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        ''---- for GmS1Lib -----
        'Me.ToolStripStatusLabel1.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID
        ''---- for GmS1Lib -----
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID '

        Me.lblH07.Text = "Σύν.Ποσότητας"
        Me.lblH08.Text = "Σύν.Ποσότ.Πιστ"
        Me.lblH09.Text = "Σύνολα"
        Me.lblH10.Text = "Σύνολα+ΦΠΑ"
        Me.radioBtnCreditInv.Enabled = False
        Me.TlSTxtTFPRMS.Text = "102,152"

        If {"dmalandrakis", "iantypa", "kvasilaki", "afarasoglou", "mourailidou", "vantza", "pplumidi", "dgkolfis", "garavantinos", "transports"}.Contains(CurUser) Then
            Me.radioBtnCreditInv.Enabled = True
            Me.radioBtnCreditInv.Checked = True
            Me.radioBtnOBal.Visible = True
        End If


        If Me.Tag = "Πελατών" Then
            Me.radioBtnΑggregate.Checked = True
            Me.radioBtnProCr.Visible = False 'Προχρεώσεις
        Else
            'Me.radioBtnProCr.Visible = True 'Προχρεώσεις
            Me.radioBtnProCr.Location = Me.radioBtnOBal.Location
            Me.radioBtnOBal.Visible = False
            Me.radioBtnSales.Visible = False
        End If

        If CurUser = "gmlogic" Then
            'Me.Panel1.Visible = True
            Me.radioBtnCreditInv.Enabled = True
            Me.TlSTxtTRDR.Text = "3000609" '"3000222" '"3000917" '"3000765" '"3000533" '"3000011" '"3000382" ' '"3000934" '"3000534" '"3000011" ' "3000153" '"5001104,5000386"
            Me.txtFINCODE.Text = "" ' "1ΤΙΜ-Α9916"
            Me.txtProFINCODE.Text = "1" '"ΠΡΠΤ-Α0001"
            Me.TlSTxtTFPRMS.Text = "102,152"
            'Me.txtBoxChequeCODE.Text = "ΕΠΕΠ000607"
            'Me.radioBtnOI.Checked = True
            Me.radioBtnOpenItem.Visible = True

            Me.radioBtnSales.Checked = True
            Me.radioBtnAnalytical.Checked = True
            Me.radioBtnCreditInv.Checked = True
            Me.DateTimePicker1.Value = CDate("28/01/2021")
            'Me.DateTimePicker2.Value = CDate("21/10/2019") 'CTODate ' CDate("31/03/2018")
            Me.BindingNavigatorSaveItem.Enabled = True
            Me.txtCOMMENTS.Text = "[{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":267.27,""docsPerCnt"":13,""docsTotPrice"":302.02},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":150.81,""docsPerCnt"":13,""docsTotPrice"":170.42},{""docs"":""1ΤΙΜ-Α8752"",""docsFindoc"":444346,""docsPrice"":170.75,""docsPerCnt"":13,""docsTotPrice"":192.95},{""docs"":""1ΤΙΜ-Α8660"",""docsFindoc"":441841,""docsPrice"":115.88,""docsPerCnt"":13,""docsTotPrice"":130.94}]"
        End If

        ' When the form loads, the KeyPreview property is set to True.
        ' This lets the form capture keyboard events before
        ' any other element in the form.
        Me.KeyPreview = True
        Me.txtTimeOut.Text = 300

        ' Display the ProgressBar control.
        Me.ToolStripProgressBar1.Visible = True
        ' Set Minimum to 1 to represent the first file being copied.
        Me.ToolStripProgressBar1.Minimum = 1
        ' Set Maximum to the total number of files to copy.
        Me.ToolStripProgressBar1.Maximum = 1 'filenames.Length
        ' Set the initial value of the ProgressBar.
        Me.ToolStripProgressBar1.Value = 1
        ' Set the Step property to a value of 1 to represent each file being copied.
        Me.ToolStripProgressBar1.Step = 1

        ' Display the ProgressBar control.
        Me.ToolStripProgressBar2.Visible = True
        ' Set Minimum to 1 to represent the first file being copied.
        Me.ToolStripProgressBar2.Minimum = 1
        ' Set Maximum to the total number of files to copy.
        Me.ToolStripProgressBar2.Maximum = 1 'filenames.Length
        ' Set the initial value of the ProgressBar.
        Me.ToolStripProgressBar2.Value = 1
        ' Set the Step property to a value of 1 to represent each file being copied.
        Me.ToolStripProgressBar2.Step = 1

    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview Is True, And this Is from the FORM.")
        End If
    End Sub

    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = (Not DataSafe())
    End Sub
#End Region
#Region "04-Bas_Commands"
    Private Sub Cmd_Add()
        Try

            Dim chkLists = From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                           Where ce.Cells("Check").Value = True

            If chkLists.Count = 0 Then
                MsgBox("Προσοχή !!! Λάθος επιλογή παραστατικών.", MsgBoxStyle.Critical, "AddingNew")
                Exit Sub
            End If

            Dim grp = chkLists.GroupBy(Function(f) f.Cells("MTRL").Value)

            If Not Me.chkBoxAuto.Checked AndAlso Not grp.Count = 1 Then
                MsgBox("Προσοχή !!! Επιλέγξατε διαφορετικά είδη.", MsgBoxStyle.Critical, "AddingNew")
                Exit Sub
            End If

            Dim chkFi As List(Of Integer) = (From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                                             Where ce.Cells("Check").Value = True
                                             Select CType(ce.Cells("Findoc").Value, Integer)).ToList

            Dim chkMt As List(Of Integer) = (From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                                             Where ce.Cells("Check").Value = True
                                             Select CType(ce.Cells("mtrl").Value, Integer)).ToList

            Dim chkMtrTrn As List(Of Integer) = (From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                                                 Where ce.Cells("Check").Value = True
                                                 Select CType(ce.Cells("MtrTrn").Value, Integer)).ToList

            Dim chkLinesNo As List(Of Integer) = (From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                                                  Where ce.Cells("Check").Value = True
                                                  Select CType(ce.Cells("LinesNo").Value, Integer)).ToList

            Dim lst As New List(Of vsc)
            lst = CType(Me.StatBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

            Dim tims = lst.Where(Function(f) f.TFprms = 102 And chkLinesNo.Contains(f.LinesNo)).ToList 'And chkFi.Contains(f.Findoc) And chkMt.Contains(f.mtrl) And chkMtrTrn.Contains(f.MtrTrn)).ToList

            For Each v In tims
                v.newPrice = v.Price
                'Dim SerCre = (From cs In db.cccSettingsLines Select If(cs.SeriesCre, 0)).ToList
                'SerCre = SerCre.Union(From cs In db.cccSettingsLines Select If(cs.SeriesCreSec, 0)).ToList
                Dim pis As New List(Of vsc)
                If Me.chkBoxAuto.Checked Then
                    pis = lst.Where(Function(f) f.TFprms = 152 And If(f.findocs, 0) = v.Findoc And f.mtrl = v.mtrl).ToList
                Else
                    pis = lst.Where(Function(f) chkLinesNo.Contains(f.LinesNo) And f.TFprms = 152).ToList
                End If
                If Not pis.Count = 0 Then 'Έχει πιστωτικά
                    v.newPrice += pis.Sum(Function(f) f.Price)
                End If

                v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                v.TotPrice = v.Qty1 * v.LPrice

                v.ViewDocs = ""
                v.ViewDocs &= "[" & v.FinCode & "],"
                v.ViewdocsFindoc &= v.Findoc & ";"
                v.ViewdocsPrice &= v.Price.ToString & ";"
                v.ViewDocs = v.ViewDocs.Substring(0, Len(v.ViewDocs) - 1)
                v.ViewdocsFindoc = v.ViewdocsFindoc.Substring(0, Len(v.ViewdocsFindoc) - 1)
                v.ViewdocsPrice = v.ViewdocsPrice.Substring(0, Len(v.ViewdocsPrice) - 1)
                addVsc = v
                Me.VscsBindingSource.AddNew()
            Next

            For Each row As DataGridViewRow In chkLists
                'Set colors
                row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
            Next
            Me.TlSBtnUnCheck.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Edit()
        Try
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim str As String = ""
                'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + dgFINDOC.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
                Dim drv As ccCVShipment = Me.StatBindingSource.Current
                str = "SALDOC[AUTOLOCATE=" & drv.FINDOC & "]"
                s1Conn.ExecS1Command(str, fS1HiddenForm)
                'FilldgFINDOC_gm(iActiveObjType)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                Me.Cursor = Cursors.Default
            End Try
            'If Me.boxOption_Paragelies.Checked Then
            '    Exit Sub
            'End If
            'Dim TFormF
            'TFormF = New ParagFBR
            ''If Me.boxOption_Apografh.Checked Then
            ''    TFormF = New ParagFBRAPG
            ''End If
            'Dim drv As FINDOC_MTRLINE = Me.MasterBindingSource.Current
            'If drv.Series = 9590 Then '"ΑΠΟΓΡΑΦΗToolStripMenuItem" 
            '    TFormF = New ParagFBRDIAK 'ParagFBRAPG
            '    TFormF.NSeries = 9590
            '    TFormF.NSOSOURCE = 1151
            'End If
            ''If Not Array.IndexOf({9520, 9521, 9522, 9523}, CType(drv.Series, Integer)) = -1 Then ' = 9520 Or drv.Series = 9521 or drv.Series=9522 or drv Then
            'If {9520, 9521, 9522, 9523, 9524, 9526}.Contains(drv.Series) Then
            '    TFormF = New ParagFBRDIAK
            '    'ΕΣΩΤΕΡΙΚΗ ΔΙΑΚΙΝΗΣΗ - 9520 (-1095+1096)
            '    'TFormF.NSeries = 9520
            '    'TFormF.NSOSOURCE = 1151
            'End If
            ''TFormF.CurDrv = New MTRLINE
            'Dim indx As Integer = drv.FINDOC
            'TFormF.CurDrv = db.MTRLINEs.Where(Function(f) f.FINDOC = indx).FirstOrDefault
            'TFormF.P_MTRLINES = drv.MTRLINES
            'Dim Position As Integer = Me.MasterBindingSource.Position
            'TFormF.DgdvRefresh = False
            'TFormF.Text = Me.Tag & " - " & TFormF.Name
            'TFormF.ShowDialog()
            'If TFormF.DgdvRefresh = True Then
            '    'Me.DataGridViewMaster.Refresh()
            '    Cmd_Select()
            '    ' Set the Position property to the results of the Find method. 
            '    Dim rowFound As FINDOC_MTRLINE = (From g As FINDOC_MTRLINE In Me.MasterBindingSource Where g.FINDOC = TFormF.CurDrv.FINDOC).FirstOrDefault()
            '    If Not IsNothing(rowFound) Then
            '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
            '        Me.MasterBindingSource.Position = itemFound
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Cmd_Select()
        Me.StatDataGridView.DataSource = Nothing
        Dim str As String = ""
        Dim dsTRDR As New DataSet
        Dim ixTable As XTable
        Me.Cursor = Cursors.WaitCursor
        Try

            LoadData()
            db.Log = Console.Out


            Dim sodTyp = 0

            Dim runningTotal As Double = 0
            Dim getRunningTotal As Func(Of Double, Double) = Function(n)
                                                                 runningTotal += n
                                                                 Return runningTotal
                                                             End Function
            Dim LineNo As Integer = 0
            Dim LinesNo As Func(Of Integer, Integer) = Function(n)
                                                           LineNo += n
                                                           Return LineNo
                                                       End Function


            If {"Πελατών", "Αντιστοιχίσεις"}.Contains(Me.Tag) Then
                sodTyp = 13
                'ΚΩΔ,ΣΥΝΑΛΑΣΟΜΕΝΟΣ,ΗΜ/ΝΙΑ Δ/Α,Δ/Α,ΗΜ/ΝΙΑ,Κατάθεση-Φόρτωση,Χρέωση,Πίστωση,Υπόλοιπο,Ανεξόφλητα

                Me.lblH01.Text = ""
                Me.lblH02.Text = ""
                Me.lblH03.Text = ""
                Me.lblH04.Text = ""
                Me.lblH05.Text = ""
                Me.lblH06.Text = ""
                'Me.lblH07.Text = ""
                Me.lblV01.Text = ""
                Me.lblV02.Text = ""
                Me.lblV03.Text = ""
                Me.lblV04.Text = ""
                Me.lblV05.Text = ""
                Me.lblV06.Text = ""
                Me.lblV07.Text = ""


                Dim trdrCodes = ""
                If Not Me.TlSTxtTRDR.Text = "" Then
                    trdrCodes = Me.TlSTxtTRDR.Text
                End If
                Dim prsns = db.PRSNs.Where(Function(f) f.SODTYPE = 20).ToList
                If Not Me.TlSTxtPRSN.Text = "" Then

                    prsns = prsns.Where(Function(f) Me.TlSTxtPRSN.Text.Split(",").Contains(f.CODE)).ToList
                    Dim prsnids = prsns.Select(Function(f) f.PRSN).ToList
                    Dim codes = db.TRDRs.Where(Function(f) f.SODTYPE = sodTyp And prsnids.Contains(f.SALESMAN)).Select(Function(f) f.CODE).ToList

                    For Each ss In codes
                        trdrCodes &= "," & ss
                    Next

                End If


                Dim trdrs = db.TRDRs.Where(Function(f) trdrCodes.Split(",").Contains(f.CODE)).Select(Function(f) f.TRDR).ToList

                Dim trdExtratbl As New List(Of TRDEXTRA)
                If Me.radioBtnCreditInv.Checked Then
                    'Ημερ/νία	Παραστατικό	Κωδικός	Επωνυμία	Κωδικός	Περιγραφή	ΦΠΑ %	Ποσ.1 πώλησης	Τιμή	Συν.Αξία

                    Dim q = From vs In db.VMTRSTATs Join mt In db.MTRLINEs On vs.Findoc Equals mt.FINDOC And vs.MtrTrn Equals mt.MTRLINES And vs.Mtrl Equals mt.MTRL
                            Join td In db.TRDRs On vs.Trdr Equals td.TRDR Join m In db.MTRLs On vs.Mtrl Equals m.MTRL
                            Join v In db.VATs On vs.Vat Equals v.VAT Join fi In db.FINDOCs On fi.FINDOC Equals vs.Findoc
                            Select New vsc With {
                                .Company = vs.Company,
                                .SoSource = vs.SoSource,
                                .TFprms = vs.TFprms,
                                .TSodType = vs.TSodType,
                                .IsCancel = vs.IsCancel,
                                .Fprms = vs.Fprms,
                                .Findoc = vs.Findoc,
                                .TrnDate = vs.TrnDate,
                                .FinCode = vs.FinCode,
                                .series = vs.Series,
                                .MtrTrn = vs.MtrTrn,
                                .findocs = mt.FINDOCS,
                                .Trdr = vs.Trdr,
                                .TrdBranch = vs.TrdBranch,
                                .CODE = td.CODE,
                                .NAME = td.NAME,
                                .mtrl = vs.Mtrl,
                                .m_CODE = m.CODE,
                                .m_NAME = m.NAME,
                                .Vat = vs.Vat,
                                .Qty1 = vs.Qty1,
                                .Price = vs.Price * IIf(vs.TFprms = 102, 1, -1),
                                .VATNAME = v.NAME,
                                .PERCNT = v.PERCNT,
                                .comments = fi.COMMENTS,
                                .remarks = fi.REMARKS}

                    Dim CompanyT As Short = 1000
                    'WHERE(A.Company = 1000) And (A.Company = 1000) And (A.TrnDate >= '20190101') AND (A.TrnDate < '20200101') 
                    'AND (A.SoSource = 1351) AND (A.TFprms IN (102, 152)) AND (A.FinCode LIKE '1ΤΙΜ-Α9916%') AND (A.TSodType = 13) 
                    '     And (A.IsCancel = 0) And (A.Fprms < 9000)
                    Dim qwh = q.Where(Function(f) f.Company = CompanyT And f.SoSource = 1351 And f.TSodType = 13) ' And f.Fprms < 9000)

                    If Me.TlSComboBoxDate.Text = "Ημ/νία:" Then
                        qwh = qwh.Where(Function(f) f.TrnDate >= DateTimePicker1.Value.Date And f.TrnDate <= DateTimePicker2.Value)
                    End If

                    If Not Me.TlSTxtTFPRMS.Text = "" Then
                        qwh = qwh.Where(Function(f) Me.TlSTxtTFPRMS.Text.Split(",").Contains(f.TFprms)) 'Τύπος
                        qwh = qwh.Where(Function(f) (f.IsCancel = 0))
                    End If

                    If Not Me.TlSTxtTRDR.Text = "" Then
                        qwh = qwh.Where(Function(f) f.CODE Like Me.TlSTxtTRDR.Text)
                    End If

                    qwh = qwh.OrderBy(Function(f) f.m_CODE).ThenBy(Function(f) f.Findoc)

                    Dim qall = qwh.ToList
                    'var query =
                    'fruits.Select((fruit, Index) >=
                    '                  New { index, str = fruit.Substring(0, Index) });
                    qall = qall.Select(Function(f, Index) New vsc With {
                                .Company = f.Company,
                                .SoSource = f.SoSource,
                                .TFprms = f.TFprms,
                                .TSodType = f.TSodType,
                                .IsCancel = f.IsCancel,
                                .Fprms = f.Fprms,
                                .Findoc = f.Findoc,
                                .TrnDate = f.TrnDate,
                                .FinCode = f.FinCode,
                                .series = f.series,
                                .MtrTrn = f.MtrTrn,
                                .findocs = f.findocs,
                                .Trdr = f.Trdr,
                                .TrdBranch = f.TrdBranch,
                                .CODE = f.CODE,
                                .NAME = f.NAME,
                                .mtrl = f.mtrl,
                                .m_CODE = f.m_CODE,
                                .m_NAME = f.m_NAME,
                                .Vat = f.Vat,
                                .Qty1 = f.Qty1,
                                .Price = f.Price,
                                .VATNAME = f.VATNAME,
                                .PERCNT = f.PERCNT,
                                .comments = f.comments,
                                .LinesNo = Index + 1}).ToList

                    Dim nqall = qall.Where(Function(f) f.TFprms = 152).ToList
                    For Each v In nqall
                        Dim vv = qall.Where(Function(f) f.Findoc = If(v.findocs, 0) And f.mtrl = v.mtrl).ToList
                        If vv.Count = 0 Then
                            v.findocs = Nothing
                        End If
                    Next
                    Dim PisMiss = qall.Where(Function(f) f.TFprms = 152 And f.findocs Is Nothing).ToList
                    If PisMiss.Count > 0 Then
                        Me.chkBoxAuto.Checked = False
                    End If
                    impExcel = False

                    Me.StatBindingSource.DataSource = New SortableBindingList(Of vsc)(qall.ToList) ' qwht

                    Dim lst As New List(Of vsc)

                    lst = CType(Me.StatBindingSource.DataSource, SortableBindingList(Of vsc)).ToList
                    Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))

                    lst = lst.Where(Function(f) f.TFprms = 102).ToList

                    Me.lblV07.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
                    Me.lblV08.Text = ""

                    vscs = New List(Of vsc)
                    Me.VscsBindingSource.DataSource = New SortableBindingList(Of vsc)(vscs.ToList)
                    Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource

                End If

                If Me.radioBtnOpenItem.Checked Then
                    Dim TblHeader As XTable
                    Dim TblDetail As XTable

                    Dim ProgLibIntf As Object = s1Conn.GetStockObj("ProgLibIntf", True)
                    Dim ModuleIntf As Object = s1Conn.GetStockObj("ModuleIntf", True)
                    Dim cfnObj As XModule = Nothing
                    Try
                        Dim prm(4) As Object
                        cfnObj = s1Conn.CreateModule("SALDOC")
                        cfnObj.LocateData(490331) '453483)


                        TblHeader = cfnObj.GetTable("FINDOC")
                        TblDetail = cfnObj.GetTable("TRDFLINES")

                        Dim ds As New DataSet
                        ds.Tables.Add(TblHeader.CreateDataTable(True))

                        prm(0) = cfnObj.Handle
                        prm(1) = 1034 'Μερική αντιστοίχιση '1032
                        prm(2) = "480040;302.02,483098;170.42,487057;192.95,487060;130.94" '"453484" 'ds.Tables(0)("FINDOCS") '"453831"

                        '1034: Μερική αντιστοίχιση.
                        '      Δίνεται String με τα ID των γραμμών του πίνακα FINPAYTERMS, μαζί
                        'με την προς αντιστοίχιση αξία.
                        '      π.χ. 'FINPAYTERMS1;100,FINPAYTERMS2;300.15,FINPAYTERMS3;400'

                        'Π.χ.
                        'x = CallPublished('ModuleIntf.LocateModule',VarArray(vModule,fFindocNew,2));
                        'x = CallPublished('ProgLibIntf.ModuleCommand',VarArray(vModule,1034,'FINPAYTERMS1;100,FINPAYTERMS2;300.15,FINPAYTERMS3;400',3));
                        'x = CallPublished('ModuleIntf.PostModule',vModule);
                        '-------------------------------------------------------------------------------------------------------------
                        Dim x = Nothing

                        'x = s1Conn.CallPublished(ModuleIntf, "LocateModule", prm)
                        x = s1Conn.CallPublished(ProgLibIntf, "ModuleCommand", prm)
                        x = s1Conn.CallPublished(ModuleIntf, "PostModule", cfnObj.Handle)

                        's1Conn.CallPublished(s1Conn.GetStockObj("ProgLibIntf", True), "ModuleCommand", prm)
                    Catch ex As Exception

                    Finally
                        cfnObj.Dispose()

                    End Try

                End If
                'Me.StatBindingSource.DataSource = res4 ' New SortableBindingList(Of GetTrdrDetailResult)(res4.ToList)
            End If

            Me.StatDataGridView.DataSource = Me.StatBindingSource

            Me.lblV03.Text = ""

            StatDataGridView_Styling()
            MTRLINEsDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
            ixTable = Nothing
        End Try
    End Sub

    Private Sub Cmd_Delete()
        Dim rows = Me.StatDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True).ToList
        If rows.Count = 0 Then
            MsgBox("Προσοχή !!! Λάθος επιλογή για διαγραφή.", MsgBoxStyle.Critical, "BindingNavigatorDeleteItem_Click")
            Exit Sub
        End If
        Try
            For Each row As DataGridViewRow In rows
                Me.StatDataGridView.Rows.Remove(row)
            Next
            'fvscs = Me.StatBindingSource.DataSource
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetTrdrDetailBalance(ByRef curBals As List(Of GetTrdrBalanceAllResult), prsns As List(Of PRSN), ctrdr As Integer, trdrCodes As String, company As Integer, sodTyp As Integer, DTFrom As Date, DTTo As Date, detChk As Boolean) As List(Of GetTrdrDetailResult)
        Dim details As List(Of GetTrdrDetailResult) = Nothing

        Dim runningTotal As Double = 0
        Dim getRunningTotal As Func(Of Double, Double) = Function(n)
                                                             runningTotal += n
                                                             Return runningTotal
                                                         End Function

        'Dim res As IMultipleResults = db.GetTrdrBalance(Nothing, trdrCodes.Replace("*", "%").Trim, 1000, sodTyp, Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, True)
        Dim res As IMultipleResults = db.GetTrdrBalance(ctrdr, trdrCodes, company, sodTyp, Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, detChk)

        'myArrN = ("Ημερ/νία,Παραστατικό,Κίνηση,Α.Χ.,Επωνυμία,Ποσ.1,Ποσ.εισαγωγών,Αξία εισαγωγών,Ποσ.εξαγωγών,Αξία εξαγωγών,Υπόλοιπο ποσ.,Υπόλοιπο αξία,Αιτιολογία").Split(",")

        'Dim res1 = res.GetResult(Of GetItemsStatementsResult1).ToList
        Dim befYeardBal = res.GetResult(Of GetTrdrBalanceBefYearResult).ToList
        curBals = res.GetResult(Of GetTrdrBalanceAllResult).ToList
        Dim befPrdBal = res.GetResult(Of GetTrdrBalanceResult).ToList

        'myArrN = ("Ημερ/νία,Παραστατικό,Κίνηση,Α.Χ.,Επωνυμία,Ποσ.1,Ποσ.εισαγωγών,Αξία εισαγωγών,Ποσ.εξαγωγών,Αξία εξαγωγών,Υπόλοιπο ποσ.,Υπόλοιπο αξία,Αιτιολογία").Split(",")
        details = res.GetResult(Of GetTrdrDetailResult).ToList

        Dim afterPrdBal = res.GetResult(Of GetTrdrDetailAfterResult).ToList

        Dim trdrs = curBals.OrderBy(Function(f) f.TRDR).Distinct(Function(f) f.TRDR).Select(Function(f) f.TRDR).ToList
        ''---- for GmS1Lib -----
        'Dim trdrs = curBals.OrderBy(Function(f) f.TRDR).Select(Function(f) f.TRDR).ToList
        ''---- for GmS1Lib -----
        Dim dets As New List(Of GetTrdrDetailResult)

        Dim TrdrChqBal = res.GetResult(Of GetTrdrChequeOpenResult).ToList ' res.GetResult(Of GetTrdrChqueBalanceResult).ToList

        If trdrs.Count > 0 Then
            Me.ToolStripProgressBar1.Maximum = trdrs.Count
            ' Set the initial value of the ProgressBar.
            Me.ToolStripProgressBar1.Value = 1
        End If



        For Each td In trdrs
            Me.ToolStripProgressBar1.PerformStep()
            If td = 527 Then
                td = 527
            End If

            Dim det0 As New GetTrdrDetailResult
            det0.rowNo = 0
            det0.trdr = td
            det0.sosource = 1351
            Dim detCount = details.Where(Function(f) f.trdr = td).Count
            If detCount = 0 Then
                Dim trdr = db.TRDRs.Where(Function(f) f.TRDR = td).FirstOrDefault
                det0.code = trdr.CODE
                det0.name = trdr.NAME
                det0.tdSalesManName = prsns.Where(Function(f) f.PRSN = trdr.SALESMAN).FirstOrDefault.NAME2
            Else
                Dim det1 = details.Where(Function(f) f.trdr = td).FirstOrDefault
                det0.code = det1.code
                det0.name = det1.name
                det0.tdSalesMan = det1.tdSalesMan
                det0.vtSalesMan = det1.vtSalesMan
            End If

            det0.fincode = "Εκ μεταφοράς"
            det0.trndate = CDate("31/12/" & Me.DateTimePicker1.Value.Year - 1)
            If befYeardBal.Count > 0 Then
                det0.tdebit = befYeardBal.Where(Function(f) f.TRDR = td).Sum(Function(f) f.prdb)
                det0.tcredit = befYeardBal.Where(Function(f) f.TRDR = td).Sum(Function(f) f.prcr)
                det0.tturnovr = befYeardBal.Where(Function(f) f.TRDR = td).Sum(Function(f) f.prtturnover)
                det0.Vat = det0.tdebit - det0.tturnovr
                If detCount = 0 AndAlso curBals.Count = 1 Then
                    det0.tdebit = curBals.Where(Function(f) f.TRDR = td).Sum(Function(f) f.pdb)
                    det0.tcredit = curBals.Where(Function(f) f.TRDR = td).Sum(Function(f) f.pcr)
                    det0.tturnovr = curBals.Where(Function(f) f.TRDR = td).Sum(Function(f) f.ptturnover)
                    det0.Vat = det0.tdebit - det0.tturnovr
                End If
                det0.bal = det0.tdebit - det0.tcredit
                '3000493
                det0.mixBal = det0.bal + det0.oiBal 'Μικτό υπόλοιπο
                det0.oiBal = det0.bal
            End If
            details.Insert(0, det0)

            If detCount = 0 Then
                det0.oiBal = 0
                Continue For
            End If

            'Υπόλοιπα Συναλ/μένων
            If Me.radioBtnOBal.Checked Then
                Dim limitDate = details.Where(Function(f) f.trdr = td).Max(Function(f) f.trndate)
                If Me.DateTimePicker2.Value > limitDate Then
                    Dim detLast = details.Where(Function(f) f.trdr = td).LastOrDefault
                    Dim detNew As New GetTrdrDetailResult
                    For Each pi As PropertyInfo In GetType(GetTrdrDetailResult).GetProperties()
                        Try
                            Dim val = GetType(GetTrdrDetailResult).GetProperty(pi.Name).GetValue(detLast, Nothing)
                            If Not IsNothing(val) Then
                                GetType(GetTrdrDetailResult).GetProperty(pi.Name).SetValue(detNew, val, Nothing)
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    detNew.rowNo = details.Max(Function(f) f.rowNo) + 1
                    detNew.trndate = Me.DateTimePicker2.Value
                    detNew.tdebit = 0
                    detNew.tcredit = 0
                    detNew.mixBal = 0
                    detNew.fincode = "CHEQUE"
                    details.Add(detNew)
                    'End If
                End If
            End If

            dets = details.Where(Function(f) f.trdr = td).ToList

            runningTotal = 0
            For Each rr In dets
                rr.bal = getRunningTotal(If(rr.tdebit, 0) - If(rr.tcredit, 0))
            Next

            dets = dets.OrderByDescending(Function(f) f.rowNo).ToList


            'Τρέχων Ανοιχτό υπόλοιπο
            Dim bal = curBals.Where(Function(f) f.TRDR = td).FirstOrDefault.pbal.Value

            If afterPrdBal.Count > 0 Then
                bal -= afterPrdBal.Where(Function(f) f.trdr = td And f.sosource = 1351 And f.tfprms = 102).Sum(Function(f) f.tdebit)
            End If

            'Τρέχων Μικτό υπόλοιπο
            Dim tdChqBal = TrdrChqBal.Where(Function(f) f.TRDR = td).Sum(Function(f) f.LCHEQUEBAL)
            Dim mixBal = bal
            If Not IsNothing(tdChqBal) Then
                mixBal = mixBal + tdChqBal
            End If

            Dim MaxRowNo As Integer = 0
            If dets.Count > 0 Then
                MaxRowNo = dets.Max(Function(f) f.rowNo)
                Me.ToolStripProgressBar2.Maximum = dets.Count
                ' Set the initial value of the ProgressBar.
                Me.ToolStripProgressBar2.Value = 1
            End If


            For Each det In dets '.OrderByDescending(Function(f) f.rowNo).ToList
                Me.ToolStripProgressBar2.PerformStep()
                Try
                    det.tdSalesManName = prsns.Where(Function(f) f.PRSN = det.tdSalesMan).FirstOrDefault.NAME2

                    'Υπολογισμός Αξιογράφων στην οριακή Ημ/νία
                    If Me.radioBtnOBal.Checked Then 'Ανοιχτά υπόλοιπα
                        Dim cqs As New List(Of CHEQUE) 'Όλα τα αξιόγραφα για το q1.trdr
                        If ToolStripComboBox1.SelectedItem = "Εκδότης:" Then
                            cqs = db.CHEQUEs.Where(Function(f) f.TRDRPUBLISHER = det.trdr And f.CRTDATE <= det.trndate).ToList
                        End If

                        If ToolStripComboBox1.SelectedItem = "Αρχικός συναλ/νος:" Then
                            cqs = db.CHEQUEs.Where(Function(f) f.TRDR = det.trdr And f.CRTDATE <= det.trndate).ToList
                        End If

                        Dim obal As Double? = 0

                        For Each chq In cqs
                            Dim fnch = db.fnCheQueBal(chq.CHEQUE, det.trndate).Where(Function(f) Not f.TRNBALANCE = 0).ToList
                            obal += If(fnch.Sum(Function(f) f.TRNBALANCE), 0)
                        Next

                        det.oChkQue = If(obal, 0)
                    End If

                Catch ex As Exception

                End Try
                If Not det.tfprms = 102 Then
                    'Continue For
                End If

                Try

                    ' ------------- FIFO ----------------
                    det.oiBal = Nothing
                    det.oiMixBal = Nothing
                    'If Not Me.radioBtnΑggregate.Checked Then
                    If det.fincode = "2ΠΤΕΚ-Β120" Then '1ΑΤΙΜ-Α123" Then
                        det.oiBal = det.oiBal
                    End If

                    If det.rowNo = MaxRowNo Then
                        det.mixBal = mixBal
                    Else
                        'det.mixBal = det.bal + det.oChkQue 'Μικτό υπόλοιπο
                    End If


                    'det.turnovr = (det.flg03 * det.tturnovr)
                    If det.fincode = "1ΤΙΜ-Α9144" Then
                        det.fincode = "1ΤΙΜ-Α9144"
                    End If
                    det.Vat = If(det.tdebit, 0) - If(det.tturnovr, 0)



                    det.oDays = CTODate.Subtract(det.trndate).Days - 1

                    'q1.fprms 9961 Ακυρωτικό Τιμολογίου Πώλησης
                    '1351 det.sosource Πωλήσεις and 1351---102,152 Τιμολόγιο πώλησης,Πιστωτικό Τιμολόγιο --- and not 9961	Ακυρωτικό Τιμολογίου Πώλησης

                    Try
                        If {1351}.Contains(det.sosource) And det.tfprms = 102 And Not {9961}.Contains(det.fprms) Then
                            Dim tdebit As Double = det.tdebit
                            If Not IsNothing(det.ffindocs) Then
                                Dim tdet = dets.Where(Function(f) f.findoc = det.ffindocs).FirstOrDefault
                                If Not IsNothing(tdet) Then
                                    tdebit += tdet.tdebit
                                End If
                            End If

                            det.oiBal = tdebit
                            det.oiMixBal = tdebit
                            bal -= tdebit
                            mixBal -= tdebit
                            If bal < 0 Then
                                det.oiBal = tdebit + bal
                                'Exit For
                            End If

                            If mixBal < 0 Then
                                det.oiMixBal = tdebit + mixBal
                                'Exit For
                            End If
                        End If

                        If det.rowNo = 0 Then
                            det.oiBal = bal
                            det.oiMixBal = mixBal
                        End If
                        ' ------------- END FIFO ----------------

                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
            Next

            Dim maxRec = dets.Where(Function(f) Not f.fincode = "Εκ μεταφοράς").OrderByDescending(Function(f) f.mixBal).FirstOrDefault

            maxRec.oBalMax = maxRec.mixBal
            maxRec.oBalDate = maxRec.trndate
        Next


        details = details.OrderBy(Function(f) f.trdr).ThenBy(Function(f) f.rowNo).ToList
        Return details
        Throw New NotImplementedException()
    End Function
    Private Function GetOpenTrdrBalance(trim As String, sodTyp As Integer, value1 As Date, value2 As Date) As List(Of ccCVFinPayTerm)
        Throw New NotImplementedException()
    End Function


#End Region
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        Me.StatBindingSource.EndEdit()
    End Sub
    Private Function DataSafe() As Boolean
        DataSafe = True
        ' Finish any current edits.
        EndAllEdits()

        If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function

        ' Ask the user if we should save the changes.
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) 'MeLabel)
            Case MsgBoxResult.No
                ' The data is not safe.
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
                Return False
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            'If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
            '    If Not Conditions() Then
            '        Exit Function
            '    End If
            'End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    Dim MTRLINES As Integer = 0
                    'If NFINDOC.FINDOC > 0 Then
                    '    MTRLINES = db.MTRLINEs.Where(Function(f) f.FINDOC = NFINDOC.FINDOC).Max(Function(f) f.MTRLINES)
                    'End If
                    If Not db.GetChangeSet.Inserts.Count = 0 Then
                        Dim NFINDOC As New Hglp.FINDOC
                        For Each insertion As Object In db.GetChangeSet.Inserts
                            If insertion.GetType.ToString.Contains("FINDOC") Then
                                NFINDOC = insertion
                                'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                                Dim snum As Hglp.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
                                snum.SERIESNUM += 1
                                NFINDOC.SERIESNUM = snum.SERIESNUM
                                Dim fmt As String = ""
                                Select Case NSeries
                                    Case 1004
                                        fmt = "ΠΡΠΤ-Α0000"
                                End Select
                                'ΑΠΓΡ
                                NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
                            End If
                            'If insertion.GetType.ToString.Contains("MTRLINE") Then
                            '    Dim NMTRLINE As Hglp.MTRLINE = insertion
                            '    MTRLINES += 1
                            '        NMTRLINE.MTRLINES = MTRLINES 'NFINDOC.MTRLINEs.Count 
                            '        NMTRLINE.LINENUM = NMTRLINE.MTRLINES
                            '    'NMTRLINE.FINDOC = NFINDOC.FINDOC

                            'End If
                        Next
                    End If


                    'LogSQL = sSQL
                    db.Log = Console.Out
                    db.SubmitChanges()

                    ' The Complete method commits the transaction. If an exception has been thrown, 
                    ' Complete is called and the transaction is rolled back.
                    scope.Complete()
                    SaveData = True
                End Using
            Catch ex As TransactionAbortedException
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message)
            Catch ex As ApplicationException
                writer.WriteLine("ApplicationException Message: {0}", ex.Message)
            Catch ex As Exception
                writer.WriteLine("Exception Message: {0}", ex.Message)
            Finally
                ' Close the connection
                If db.Connection.State = ConnectionState.Open Then
                    db.Connection.Close()
                End If
            End Try
            ' Display messages.
            If Not writer.ToString() = String.Empty Then
                MsgBox(writer.ToString(), MsgBoxStyle.Exclamation, "Προσοχή !!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If SaveData = False Then
            MsgBox("Προσοχή !!!.Ακύρωση Αλλαγών", MsgBoxStyle.Exclamation, "Προσοχή !!!")
        End If
    End Function
#End Region
#Region "96-MasterDataGridView"
    Dim editableFields_MTRLINEsDataGridView() As String = {"DiscPrc", "ExpDiscVal", "Qty1", "newPrice"}
    Private Sub StatDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles StatDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        Dim fi = {"Price"}
        If fi.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        If StatDataGridView.IsCurrentCellDirty Then
            StatDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MTRLINEsDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MTRLINEsDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If editableFields_MTRLINEsDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        If StatDataGridView.IsCurrentCellDirty Then
            StatDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MTRLINEsDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Dim drv As DataRowView = Me.MasterBindingSource.Current

        'Dim status = Me.MasterDataGridView.Columns(e.ColumnIndex)
        'Me.StatusStrip1.Text = status
    End Sub
    Private Sub StatDataGridView_Styling()
        Try

            Me.StatDataGridView.AutoGenerateColumns = True
            'Me.MasterDataGridView.AutoResizeColumns()

            If {"Πελατών", "Αντιστοιχίσεις"}.Contains(Me.Tag) Then

                If Me.radioBtnOpenItem.Checked Then
                    'myArrF = ("TFPRMS,FINDOC,fn2_FINDOCS,TRNDATE,SERIES,FINCODE,f2_FINCODE,f2_NETAMNT,FPRMS,TRDR,NAME,COMPANY,FINPAYTERMS,fn1_FINDOC,LINENUM,FINPAY,fn1_TRDR,TRDBRANCH,SOCURRENCY,PAYDEMANDMD,SOPAYTYPE,ISCANCEL,APPRV,FINALDATE,fn_TRNDATE,ENDDATE,TRDRRATE,AMNT,TAMNT,LAMNT,OPNTAMNT,ISCLOSE,COMMENTS,PAYMENT,PAYGRPVAL,INSTALMENT,COMMITION,TRDFLINES,FINPAYTERMSS,FINDOCS,INSMODE,OpenMode,UPDDATE,UPDUSER,FINDOCDIFF,FXDIFF").Split(", ")
                    'myArrN = ("TFPRMS,FINDOC,fn2_FINDOCS,TRNDATE,SERIES,FINCODE,f2_FINCODE,f2_NETAMNT,FPRMS,TRDR,NAME,COMPANY,FINPAYTERMS,fn1_FINDOC,LINENUM,FINPAY,fn1_TRDR,TRDBRANCH,SOCURRENCY,PAYDEMANDMD,SOPAYTYPE,ISCANCEL,APPRV,FINALDATE,fn_TRNDATE,ENDDATE,TRDRRATE,AMNT,TAMNT,LAMNT,OPNTAMNT,ISCLOSE,COMMENTS,PAYMENT,PAYGRPVAL,INSTALMENT,COMMITION,TRDFLINES,FINPAYTERMSS,FINDOCS,INSMODE,OpenMode,UPDDATE,UPDUSER,FINDOCDIFF,FXDIFF").Split(",")

                    myArrF = ("TRNDATE,FINALDATE,FINCODE,AMNT,SOSOURCE,TRDRRATE,fn2_TRNDATE,fn2_FINALDATE,fin2_FINCODE,fin2_SOSOURCE,fn2_TRDRRATE,fn2_SOCURRENCY,fn2_TAMNT,0").Split(", ")
                    myArrN = ("Ημ/νία παρ/κού,Ημ/νία απαίτησης,Παρ/κό,AMNT,Ενότητα,Ισοτιμία,Ημ/νία παρ/κού,Ημ/νία κάλυψης,Παρ/κό,Ενότητα,Ισοτιμία,Νόμισμα,Αξία,διαφορά").Split(", ")
                End If

                'Credits Reports
                If Me.radioBtnCreditInv.Checked Then
                    'Ημερ/νία	Παραστατικό	Κωδικός	Επωνυμία	Κωδικός	Περιγραφή	ΦΠΑ %	Ποσ.1 πώλησης	Τιμή	Συν.Αξία
                    If Me.radioBtnΑggregate.Checked Then
                        myArrF = ("TrnDate,FinCode,CODE,NAME,m_CODE,m_NAME,PERCNT,Qty1,Price,SumQty1,docs,mtrl").Split(",")
                        myArrN = ("Ημερ/νία,Παραστατικό,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,ΦΠΑ %,Ποσ.1 πώλησης,Τιμή,Συν.Αξία,docs,mtrl").Split(",")
                    Else
                        myArrF = ("m_CODE,m_NAME,PERCNT,Qty1,LPrice,DiscPrc,ExpDiscVal,Price,docs,docsFindoc,docsPrice").Split(",")
                        myArrN = ("Κωδικός,Περιγραφή,ΦΠΑ %,Ποσ.1,Τιμή,Έκπτ %,Εκπτωση,Καθ.Αξία,docs,docsFindoc,docsPrice").Split(",")

                        myArrF = ("TrnDate,FinCode,CODE,comments,m_CODE,m_NAME,PERCNT,Qty1,Price,DiscPrc,ExpDiscVal,NAME,LPrice,docs,Vat,mtrl,TFprms,findocs,Findoc,MtrTrn,Series,pisPrice,newPrice,LinesNo").Split(",")
                        myArrN = ("Ημερ/νία,Παραστατικό,Κωδικός,Αιτιολογία,Κωδικός Είδοuς,Περιγραφή,ΦΠΑ %,Ποσ.1,Τιμή,Μετρητοίς,Eμπορική πολιτική,Επωνυμία,LPrice,Συν.Αξία,Vat,mtrl,TFprms,findocs,Findoc,MtrTrn,Series,pisPrice,newPrice,LinesNo").Split(",")
                    End If
                End If
            End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(StatDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For i As Integer = 0 To StatDataGridView.Columns.Count - 1
                Debug.Print(StatDataGridView.Columns(i).DataPropertyName & vbTab & StatDataGridView.Columns(i).Name)
                StatDataGridView.Columns(i).ReadOnly = True
            Next
            If Me.radioBtnCreditInv.Checked Then
                AddOutOfOfficeColumn(Me.StatDataGridView)
            End If



            For Each col In StatDataGridView.Columns
                Try
                    Dim t As Type = col.ValueType
                    If Not IsNothing(t) Then
                        With col
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    .DefaultCellStyle.Format = "N2"
                                    If {"Qty1", "SumQty1"}.Contains(col.DataPropertyName) Then
                                        .DefaultCellStyle.Format = "N3"
                                    End If
                                End If
                                If Not t.FullName.IndexOf("System.DateTime, ") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    If col.DataPropertyName = "TRNDATE" Then
                                        .DefaultCellStyle.Format = "dd/MM/yyyy HH: mm"
                                    End If

                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
                                 Then
                                .DefaultCellStyle.Format = "N2"
                                If {"Qty1", "SumQty1"}.Contains(col.DataPropertyName) Then
                                    .DefaultCellStyle.Format = "N3"
                                End If
                            End If
                            'If col.ValueType.Name = "String" Then
                            '    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            '    '.Width = 200
                            'End If
                            'If col.ValueType.Name <> "String" Then
                            '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            'End If
                        End With
                    End If
                Catch ex As Exception
                    MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
                End Try
            Next

            Dim chkLists = From ce In Me.StatDataGridView.Rows.Cast(Of DataGridViewRow)
                           Where ce.Cells("TFprms").Value = 152 And IsNothing(ce.Cells("findocs").Value)

            For Each row As DataGridViewRow In chkLists
                'Set colors
                'Dim csty As New DataGridViewCellStyle
                'csty = row.Cells("findocs").Style
                'csty.BackColor = System.Drawing.Color.YellowGreen
                'row.Cells("findocs").Style = New DataGridViewCellStyle(csty) ';..DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen
                '    If IsNothing(item.findocs) Then
                '        row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                '    End If
                'End If
                row.DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen
            Next


            Me.StatDataGridView.AutoResizeColumns()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub MTRLINEsDataGridView_Styling()
        Try

            Me.MTRLINEsDataGridView.AutoGenerateColumns = True
            'Me.MasterDataGridView.AutoResizeColumns()

            If {"Πελατών", "Αντιστοιχίσεις"}.Contains(Me.Tag) Then


                If Me.radioBtnOpenItem.Checked Then
                    'myArrF = ("TFPRMS,FINDOC,fn2_FINDOCS,TRNDATE,SERIES,FINCODE,f2_FINCODE,f2_NETAMNT,FPRMS,TRDR,NAME,COMPANY,FINPAYTERMS,fn1_FINDOC,LINENUM,FINPAY,fn1_TRDR,TRDBRANCH,SOCURRENCY,PAYDEMANDMD,SOPAYTYPE,ISCANCEL,APPRV,FINALDATE,fn_TRNDATE,ENDDATE,TRDRRATE,AMNT,TAMNT,LAMNT,OPNTAMNT,ISCLOSE,COMMENTS,PAYMENT,PAYGRPVAL,INSTALMENT,COMMITION,TRDFLINES,FINPAYTERMSS,FINDOCS,INSMODE,OpenMode,UPDDATE,UPDUSER,FINDOCDIFF,FXDIFF").Split(", ")
                    'myArrN = ("TFPRMS,FINDOC,fn2_FINDOCS,TRNDATE,SERIES,FINCODE,f2_FINCODE,f2_NETAMNT,FPRMS,TRDR,NAME,COMPANY,FINPAYTERMS,fn1_FINDOC,LINENUM,FINPAY,fn1_TRDR,TRDBRANCH,SOCURRENCY,PAYDEMANDMD,SOPAYTYPE,ISCANCEL,APPRV,FINALDATE,fn_TRNDATE,ENDDATE,TRDRRATE,AMNT,TAMNT,LAMNT,OPNTAMNT,ISCLOSE,COMMENTS,PAYMENT,PAYGRPVAL,INSTALMENT,COMMITION,TRDFLINES,FINPAYTERMSS,FINDOCS,INSMODE,OpenMode,UPDDATE,UPDUSER,FINDOCDIFF,FXDIFF").Split(",")

                    myArrF = ("TRNDATE,FINALDATE,FINCODE,AMNT,SOSOURCE,TRDRRATE,fn2_TRNDATE,fn2_FINALDATE,fin2_FINCODE,fin2_SOSOURCE,fn2_TRDRRATE,fn2_SOCURRENCY,fn2_TAMNT,0").Split(", ")
                    myArrN = ("Ημ/νία παρ/κού,Ημ/νία απαίτησης,Παρ/κό,AMNT,Ενότητα,Ισοτιμία,Ημ/νία παρ/κού,Ημ/νία κάλυψης,Παρ/κό,Ενότητα,Ισοτιμία,Νόμισμα,Αξία,διαφορά").Split(", ")
                End If

                'Credits Reports
                If Me.radioBtnCreditInv.Checked Then
                    'Ημερ/νία	Παραστατικό	Κωδικός	Επωνυμία	Κωδικός	Περιγραφή	ΦΠΑ %	Ποσ.1 πώλησης	Τιμή	Συν.Αξία
                    myArrF = ("m_CODE,m_NAME,PERCNT,Qty1,newPrice,ExpDiscVal,DiscPrc,LPrice,TotPrice,ViewDocs,ViewdocsFindoc,ViewdocsPrice").Split(",")
                    myArrN = ("Κωδικός Είδοuς,Περιγραφή,ΦΠΑ %,Ποσ.1,Τιμή-ΑυτΠιστ,Έκπτωση,Έκπτ %,Τιμή Πιστ/κού,Συν Έκπτωση,ViewDocs,ViewdocsFindoc,ViewdocsPrice").Split(",")
                End If
            End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MTRLINEsDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For i As Integer = 0 To MTRLINEsDataGridView.Columns.Count - 1
                Debug.Print(MTRLINEsDataGridView.Columns(i).DataPropertyName & vbTab & MTRLINEsDataGridView.Columns(i).Name)
                MTRLINEsDataGridView.Columns(i).ReadOnly = True
            Next
            For Each edf In editableFields_MTRLINEsDataGridView
                Dim Col As DataGridViewColumn = GetNoColumnDataGridView(Me.MTRLINEsDataGridView, edf)
                If Not IsNothing(Col) Then
                    Col.ReadOnly = False
                End If
            Next
            If Me.radioBtnCreditInv.Checked Then
                'AddOutOfOfficeColumn(Me.MTRLINEsDataGridView)
            End If



            For Each Col In MTRLINEsDataGridView.Columns
                Try
                    Dim t As Type = Col.ValueType
                    If Not IsNothing(t) Then
                        With Col
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    .DefaultCellStyle.Format = "N2"
                                    If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
                                        .DefaultCellStyle.Format = "N3"
                                    End If
                                End If
                                If Not t.FullName.IndexOf("System.DateTime, ") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    If Col.DataPropertyName = "TRNDATE" Then
                                        .DefaultCellStyle.Format = "dd/MM/yyyy HH: mm"
                                    End If

                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
                                 Then
                                .DefaultCellStyle.Format = "N2"
                                If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
                                    .DefaultCellStyle.Format = "N3"
                                End If
                            End If
                            'If col.ValueType.Name = "String" Then
                            '    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            '    '.Width = 200
                            'End If
                            'If col.ValueType.Name <> "String" Then
                            '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            'End If
                        End With
                    End If
                Catch ex As Exception
                    MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
                End Try
            Next
            Me.MTRLINEsDataGridView.AutoResizeColumns()
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetNoColumnDataGridView(CurDataGridView As DataGridView, CDataPropertyName As String) As DataGridViewColumn
        Dim col As DataGridViewColumn = Nothing
        col = CurDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = CDataPropertyName).FirstOrDefault
        Return col
        'Throw New NotImplementedException()
    End Function

    Private Sub MTRLINEsDataGridView_Sorted(sender As Object, e As EventArgs) Handles MTRLINEsDataGridView.Sorted
        MTRLINEsDataGridView_Styling()
    End Sub
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnUnCheck.Click, TlSBtnCheck.Click
        Dim s As ToolStripButton = sender
        Dim check As Boolean = False
        If s.Name = "TlSBtnCheck" Then
            check = True
        Else
            check = False
        End If
        If Me.StatDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.StatDataGridView.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                If Not ds.Cells("Check").Value = check Then
                    ds.Cells("Check").Value = check
                End If
            Next
            'For i As Integer = 0 To DrSel.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
            'Next
        Else
            For Each ds As DataGridViewRow In Me.StatDataGridView.Rows
                ds.Cells("Check").Value = check
            Next
            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
            'Next
        End If
        Me.StatDataGridView.RefreshEdit()
    End Sub

    Private Sub MTRLINEsDataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MTRLINEsDataGridView.CellFormatting
        If e.ColumnIndex > 0 AndAlso Not IsNothing(e.Value) Then
            If e.Value.ToString = "0" Then
                e.Value = ""
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub MTRLINEsDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MTRLINEsDataGridView.CellValidating
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).DataPropertyName = "Qty1" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim Qty1 As String = cell.EditedFormattedValue
            If Qty1 = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = Qty1 Then
                Dim v As vsc = s.Rows(e.RowIndex).DataBoundItem
                v.Qty1 = Qty1
                v.LPrice = 0
                If Not v.ExpDiscVal = 0 Or Not v.DiscPrc = 0 Then
                    'v.LPrice = v.ExpDiscVal + ((v.newPrice - v.ExpDiscVal) * (v.DiscPrc / 100))
                    v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                End If
                v.TotPrice = v.Qty1 * v.LPrice
                'v.Opdocs.docsPrice = v.TotPrice
            End If
        End If
        If s.Columns(e.ColumnIndex).DataPropertyName = "DiscPrc" Then 'DiscPrc,ExpDiscVal
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim DiscPrc As String = cell.EditedFormattedValue
            If DiscPrc = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = DiscPrc Then
                Dim v As vsc = s.Rows(e.RowIndex).DataBoundItem
                v.DiscPrc = DiscPrc
                v.LPrice = 0
                If Not v.ExpDiscVal = 0 Or Not v.DiscPrc = 0 Then
                    'v.LPrice = v.ExpDiscVal + ((v.newPrice - v.ExpDiscVal) * (v.DiscPrc / 100))
                    v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                End If
                v.TotPrice = v.Qty1 * v.LPrice
                'v.Opdocs.docsPrice = v.TotPrice
            End If
        End If
        If s.Columns(e.ColumnIndex).DataPropertyName = "ExpDiscVal" Then 'DiscPrc,ExpDiscVal
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim ExpDiscVal As String = cell.EditedFormattedValue
            If ExpDiscVal = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = ExpDiscVal Then
                Dim v As vsc = s.Rows(e.RowIndex).DataBoundItem
                v.ExpDiscVal = ExpDiscVal
                v.LPrice = 0
                If Not v.ExpDiscVal = 0 Or Not v.DiscPrc = 0 Then
                    'v.LPrice = ((v.newPrice - v.ExpDiscVal) * (IIf(v.DiscPrc = 0, 100, v.DiscPrc) / 100))
                    v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                End If
                v.TotPrice = v.Qty1 * v.LPrice
                'v.Opdocs.docsPrice = v.TotPrice
            End If
        End If

        'Τιμή-ΑυτΠιστ newPrice
        If s.Columns(e.ColumnIndex).DataPropertyName = "newPrice" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim newPrice As String = cell.EditedFormattedValue
            If newPrice = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = newPrice Then
                Dim v As vsc = s.Rows(e.RowIndex).DataBoundItem
                v.newPrice = newPrice
                v.LPrice = 0
                If Not v.ExpDiscVal = 0 Or Not v.DiscPrc = 0 Then
                    'v.LPrice = v.ExpDiscVal + ((v.newPrice - v.ExpDiscVal) * (v.DiscPrc / 100))
                    v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                End If
                v.TotPrice = v.Qty1 * v.LPrice
                'v.Opdocs.docsPrice = v.TotPrice
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MTRLINEsDataGridView.DataError

        If editableFields_MTRLINEsDataGridView.Contains(sender.Columns(e.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        MessageBox.Show("Error happened " _
            & e.Context.ToString() & vbCrLf & "Row,Col:" & e.RowIndex & "," & sender.Columns(e.ColumnIndex).Name)

        If (e.Context = DataGridViewDataErrorContexts.Commit) _
            Then
            MessageBox.Show("Commit error")
        End If
        If (e.Context = DataGridViewDataErrorContexts _
            .CurrentCellChange) Then
            MessageBox.Show("Cell change")
        End If
        If (e.Context = DataGridViewDataErrorContexts.Parsing) _
            Then
            MessageBox.Show("parsing error")
        End If
        If (e.Context =
            DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If

        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"

            e.ThrowException = False
        End If
    End Sub
    Private Sub MTRLINEsDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MTRLINEsDataGridView.EditingControlShowing
        'Dim s As GmDataGridView = sender
        'Dim cell As DataGridViewCell = s.CurrentCell
        ''Dim r = cell.OwningRow.Cells("")..Cells("MTRL")
        'If cell.ColumnIndex = 2 Then
        '    'Dim c As ComboBox = CType(e.Control, ComboBox)
        'End If

    End Sub
    Private Sub MTRLINEsDataGridView_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MTRLINEsDataGridView.CellMouseDown
        If e.Button = MouseButtons.Right Then
            '''''Dim hti = MasterDataGridView.HitTest(e.X, e.Y)
            '''''MasterDataGridView.ClearSelection()
            '''''MasterDataGridView.Rows(e.RowIndex).Selected = True
            ''''Dim fabs As New FormUsersSet
            '''''fabs.Conn = FormMain.Conn
            ''''fabs.OrUs = MasterDataGridView.Rows(e.RowIndex).DataBoundItem

            '''''Dim sts As New List(Of CCCSTATUS)
            '''''Dim st As New CCCSTATUS
            '''''Dim count = 0
            '''''For Each cc In ("--Επιλέγξτε--,ΕΡΓΑΣΙΑ,ΑΣΘΕΝΕΙΑ,ΑΔΕΙΑ,REPO,ΑΛΛΟ").Split(",")
            '''''    st = New CCCSTATUS
            '''''    st.ID = count
            '''''    count += 1
            '''''    If cc = "ΕΡΓΑΣΙΑ" Then
            '''''        Continue For
            '''''    End If
            '''''    st.DESCR = cc
            '''''    sts.Add(st)
            '''''Next
            '''''Me.StateBindingSource.DataSource = GetState()

            ''''''
            ''''''StateDataGridViewComboBoxColumn
            ''''''
            '''''Dim ddlState = fabs.ddlState
            ''''''StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            '''''ddlState.DataSource = Me.StateBindingSource
            '''''ddlState.DisplayMember = "DESCR"
            ''''''StateDataGridViewComboBoxColumn.HeaderText = "State"
            ''''''StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            '''''ddlState.Name = "StateComboBox"
            ''''''StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            ''''''StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '''''ddlState.ValueMember = "ID"

            ''''fabs.ShowDialog()
            '''''Cmd_Select()
        End If
    End Sub
    Private Sub DataGridViewMaster_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles MTRLINEsDataGridView.ColumnWidthChanged
        'If Me.MasterTopDataGridView.Columns.Count > 0 Then
        '    Me.MasterTopDataGridView.Columns(e.Column.Index).Width = e.Column.Width
        'End If
    End Sub
    Private Sub DataGridViewMaster_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles MTRLINEsDataGridView.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            'Me.MasterTopDataGridView.HorizontalScrollingOffset = Me.MasterDataGridView.HorizontalScrollingOffset '_Scroll(DataGridView1, e)
        End If
    End Sub
    Private Sub MTRLINEsDataGridView_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles MTRLINEsDataGridView.CellPainting
        If e.RowIndex = -1 And e.ColumnIndex = 5 Then
            'Dim cl As DataGridViewCell = Me.MasterDataGridView.Rows(-1).Cells(5)
            'e.Graphics.FillRectangle(Brushes.Blue, e.CellBounds)
            'e.PaintContent(e.ClipBounds)
            'e.Handled = True
        End If
    End Sub

#End Region
#Region "97- Control Events"
    'Private Sub BindingNavigatorAddNewItem_Click(sender As System.Object, e As System.EventArgs) Handles ΑΠΟΓΡΑΦΗToolStripMenuItem.Click, ΕΞΑΓΩΓΕΣToolStripMenuItem.Click, ΕΙΣΑΓΩΓΕΣToolStripMenuItem.Click
    '    Cmd_Add(sender)
    'End Sub
    Private Sub BindingNavigatorDeleteItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorDeleteItem.Click
        Cmd_Delete()
    End Sub
    'Private Sub BindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs)
    '    Me.Validate()
    '    Me.MasterBindingSource.EndEdit()
    'End Sub
    Private Sub cmdSelect_Click(sender As System.Object, e As System.EventArgs) Handles cmdSelect.Click
        Cmd_Select()
    End Sub
    Private Sub OpenToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripButton.Click
        Cmd_Edit()
    End Sub
    'Private Sub txtBoxLName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBoxLName.TextChanged
    '    Dim s As TextBox = sender
    '    Dim rowFound As Cross1.Member = (From g As Cross1.Member In Me.MasterBindingSource Where g.Name.ToString.ToUpper Like s.Text.Trim.ToUpper & "*").FirstOrDefault()
    '    If Not IsNothing(rowFound) Then
    '        'Dim itemFound As Integer = Me.MasterBindingSource.Find("Name", row3.Name.ToString)
    '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
    '        Me.MasterBindingSource.Position = itemFound
    '    End If
    'End Sub
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        Cmd_Add()
        'Try
        '    Dim q = Me.StatBindingSource.List.OfType(Of ccCVShipment).ToList().Where(Function(f) f.FINDOC = 0).FirstOrDefault
        '    If IsNothing(q) Then
        '        Me.StatBindingSource.AddNew()
        '        Dim nu As ccCVShipment = Me.StatBindingSource.Current
        '        'nu.user_type = "User"
        '        'nu.createdOn = Now()
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
            DateTimePicker1.Value = CTODate
        Else
            DateTimePicker1.Value = "01/01/" & Year(CTODate)
        End If
    End Sub

    Private Sub TlSBtn_Click(sender As Object, e As EventArgs) Handles TlSBtnCheque.Click, TlSBtnTRDR.Click, TlSBtnPRSN.Click, TlSBtnMTRL.Click
        Dim ee As New System.ComponentModel.CancelEventArgs
        ee.Cancel = False
        TlSTextBox_Validating(sender, ee)
    End Sub

    Private Sub TlSTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim s As Object = sender
        'e.Cancel = False
        Dim Valid As Boolean = False
        Dim SelectSender As String = ""
        Select Case sender.GetType.Name
            Case "TextBox"
                SelectSender = s.Name
            Case "ToolStripButton", "ToolStripTextBox"
                SelectSender = s.Name
            Case "MyDataGridView", "GmDgView"
                SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
        End Select
        Dim ReturnFields As New ArrayList
        Dim errorMsg As String = ""
        Dim View As Boolean = False
        Dim GmTitle As String = ""
        'Dim GmTableName As String = ""
        Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
        Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
        Dim GmCheck As Boolean = False
        Dim ValidField As String = ""
        Dim sender_TAG As String = ""
        Dim Visible As Boolean = False
        Dim GmPelPro As Byte = 0
        RsWhere = "1=1"
        RsOrder = ""
        Select Case SelectSender
            'Case "TlSBtnSERIES", "TlSTxtSERIES"
            '    TlSTxtSERIES.Tag = "SERIES"
            '    ReturnFields.Add(TlSTxtSERIES)
            '    GmTitle = "Σειρές"
            '    RsTables = "SERIES"

            '    Company = CompanyT

            '    RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
            '    RsOrder = "SERIES"
            '    'SELECT A.COMPANY,A.SOSOURCE,A.SOREDIR,A.SERIES,A.CODE,A.FPRMS,A.NAME,A.ISACTIVE,A.BRANCH,A.WHOUSE FROM SERIES A WHERE A.COMPANY=1000 AND A.COMPANY IN (1000) AND A.SOSOURCE=1351 AND A.ISACTIVE=1 ORDER BY A.SERIES,A.COMPANY,A.SOSOURCE
            '    sSQL = "SELECT SERIES,CODE,NAME FROM SERIES "
            '    sender_TAG = ReturnFields(0).Tag
            '    myArrF = ("SERIES,CODE,NAME").Split(",")
            '    myArrN = ("Σειρά,Σύντμηση,Περιγραφή").Split(",")
            '    GmCheck = True

            'Case "TlSBtnFPRMS", "TlSTxtFPRMS"
            '    TlSTxtFPRMS.Tag = "FPRMS"
            '    ReturnFields.Add(TlSTxtFPRMS)
            '    GmTitle = "Τύποι"
            '    RsTables = "FPRMS"

            '    Company = CompanyT

            '    RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1"
            '    RsOrder = "FPRMS"
            '    sSQL = "SELECT FPRMS, NAME FROM FPRMS "
            '    sender_TAG = ReturnFields(0).Tag
            '    myArrF = ("FPRMS,NAME").Split(",")
            '    myArrN = ("Τύπος,Περιγραφή").Split(",")
            '    GmCheck = True

            Case "TlSBtnTRDR", "TlSTxtTRDR"
                TlSTxtTRDR.Tag = "CODE"
                ReturnFields.Add(TlSTxtTRDR)
                GmTitle = "Ευρετήριο Πελατών"
                RsTables = "TRDR"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=13 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM TRDR "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Επωνυμία").Split(",")

            Case "TlSBtnMTRL", "TlSTxtMTRL"
                TlSTxtMTRL.Tag = "CODE"
                ReturnFields.Add(TlSTxtMTRL)
                GmTitle = "Ευρετήριο Ειδών"
                RsTables = "MTRL"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=51 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM MTRL "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Περιγραφή").Split(",")

            Case "TlSBtnPRSN", "TlSTxtPRSN"
                TlSTxtPRSN.Tag = "CODE"
                ReturnFields.Add(TlSTxtPRSN)
                GmTitle = "Ευρετήριο Πωλητών"
                RsTables = "PRSN"

                Company = CompanyT

                RsWhere = "Company = " & Company & " AND SODTYPE=20 AND TPRSN=0" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE,PRSN"
                'SELECT A.COMPANY,A.SODTYPE,A.PRSN,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TPRSN,A.AFM,A.IDENTITYNUM FROM PRSN A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TPRSN=0 ORDER BY A.CODE,A.PRSN
                sSQL = "SELECT CODE,NAME,NAME2 FROM PRSN "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME2,NAME").Split(",")
                myArrN = ("Κωδικός,Επώνυμο,Όνομα").Split(",")
                GmCheck = True

                'Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                '    TlSTxtWHOUSE.Tag = "WHOUSE"
                '    'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
                '    ReturnFields.Add(TlSTxtWHOUSE)
                '    'ReturnFields.Add(TlSTxtTimKin_Descr)
                '    ''''''''''''''''''''''''''''''''''''''''
                '    GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
                '    RsTables = "WHOUSE"
                '    '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
                '    '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
                '    Company = CompanyT

                '    RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                '    RsWhere = Trim(RsWhere)
                '    RsOrder = "SHORTCUT"
                '    'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
                '    sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
                '    'GmPelPro = 3 'Δεν υπάρχη PelPro Field
                '    'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
                '    sender_TAG = ReturnFields(0).Tag
                '    myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
                '    myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
                '    GmCheck = True

        End Select
        Dim m_dtGen As DataTable = Nothing
        Try
            If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
                ValidField = Trim(ReturnFields(0).Text)
                If Not ValidField.IndexOf("*").Equals(-1) Then
                    ValidField = ValidField.Replace("*", "%")
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
                ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
                    ValidField = "'" & ValidField.Replace(",", "','") & "'"
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
                Else
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
                End If
                'Visible = True
            Else 'ReturnFields(0).Text = ""
                If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
                    '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
                    For i As Integer = 0 To ReturnFields.Count - 1
                        ReturnFields(i).Text = ""
                        ReturnFields(i).ToolTipText = ""
                    Next

                    'sender.Focus()
                    e.Cancel = False
                    Exit Sub
                    'Visible = True
                End If
            End If
            'RsTables = SelectPelPro(RsTables, PelPro)
            'RsWhere = SelectPelPro(RsWhere, PelPro)
            'RsOrder = SelectPelPro(RsOrder, PelPro)
            Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
            m_dtGen = GmDataN.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
        Catch ex As Exception
            MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
        End Try
        m_dvGen = New DataView(m_dtGen)
        If m_dvGen.Count = 0 Then
            Select Case SelectSender
                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
            End Select
            MsgBox(errorMsg)
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            'sender.Focus()
            e.Cancel = False
            Exit Sub
            View = False
        ElseIf m_dvGen.Count = 1 Then
            View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
        End If
        If View Or sender.GetType.Name = "ToolStripButton" Then
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            If Not s.GetType.Name = "ToolStripButton" Then
                ErrorProvider1.SetError(s.Control, "")
            End If

            Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
            Dim ar As New ArrayList
            Dim m_ds As New DataSet
            m_ds = GmDataN.GmFillDataSet(conn, m_ds, m_dtGen, m_dtGen.TableName)

            Dim TSearchFR As New SearchNewFR
            TSearchFR.Conn = conn
            TSearchFR.m_ds = m_ds
            TSearchFR.Text = GmTitle
            TSearchFR.GmCheck = GmCheck
            TSearchFR.sSQL = sSQL
            TSearchFR.GmPelPro = Nothing 'GmPelPro
            TSearchFR.RsWhere = RsWhere
            TSearchFR.RsOrder = RsOrder
            TSearchFR.GmGroupSql = GmGroupSql
            TSearchFR.GmGroupSqlField = GmGroupSqlField
            TSearchFR.RsTables = RsTables
            TSearchFR.myArrF = myArrF
            TSearchFR.myArrN = myArrN
            TSearchFR.Location = Point
            'TSearchFR.Visible = View
            'TSearchFR.RetTBL = New DataTable
            'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            TSearchFR.ShowDialog()

            Dim Dt As DataTable = TSearchFR.RetTBL
            If Not TSearchFR.RetTBL Is Nothing Then

                'ReturnFields.Add(TlSTxtPCODE)
                'ReturnFields.Add(TlSTxtPELNAME1)
                If TSearchFR.GmCheck = True Then
                    Dt.DefaultView.RowFilter = "Check = True "
                End If
                Dim dv = Dt.DefaultView
                If dv.Count > 0 Then
                    If dv.Count = 1 Then
                        'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
                        'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
                        m_dvGen = dv 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
                    Else
                        Dim Result As String = ""
                        For i As Integer = 0 To dv.Count - 1
                            Result += Trim(dv(i)(0)) & "," '("P1_PCODE")
                        Next
                        ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
                    End If
                    ReturnFields(0).ToolTipText = ReturnFields(0).Text
                    'GmDgLookUp_FillNew = SearceArrayList
                Else
                    '    MsgBox("Δεν υπάρχουν Εγγραφές")
                    'If Not IsNumeric(TextBox1.Text) Then
                    If Not s.GetType.Name = "ToolStripButton" Then
                        'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
                    End If
                    'Else
                    '    ' Clear the error.
                    '    ErrorProvider1.SetError(TextBox1, "")
                    'End If
                End If
            Else
                MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
            End If
        End If

        If m_dvGen.Count = 1 Then
            'Dim data_row As DataRowView
            'data_row = bindingTim1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
                Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))

                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("SERIES"), GetType(Short))
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("FPRMS"), GetType(Short))
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))

            End Select
        End If
        If ReturnFields.Count = 3 Then
            ReturnFields(ReturnFields.Count - 1).Focus()
        End If
    End Sub

    Private Sub CreateCarrierDoc(changeSet As ChangeSet)
        '8ΔΑ-ΘΑ3764
        Dim chSalDoc As New Hglp.FINDOC
        Dim chMtrDoc As New Hglp.MTRDOC
        For Each Changes As Object In changeSet.Updates
            If Changes.GetType.ToString.Contains("FINDOC") Then
                chSalDoc = Changes
            End If
            If Changes.GetType.ToString.Contains("MTRDOC") Then
                chMtrDoc = Changes
            End If
        Next

        If chSalDoc.FINDOC = 0 Then
            Exit Sub
        End If

        'If IsNothing(salDoc) Then
        '    MsgBox("Not salDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
        '    Exit Sub
        'End If
        If chSalDoc.TRNDATE < CDate("01/08/2018") Then
            Exit Sub
        End If

        If Not (chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7041 Or chSalDoc.FPRMS = 7046) Then
            Exit Sub
        End If
        Dim mtrDoc As Hglp.MTRDOC = db.MTRDOCs.Where(Function(f) f.FINDOC = chSalDoc.FINDOC).FirstOrDefault
        If IsNothing(mtrDoc) Then
            MsgBox("Not mtrDoc", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        If Not mtrDoc.ccCLockShipValue = 0 Then
            Exit Sub
        End If
        '2 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
        '3 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        '4 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
        '5 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '8 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        '13 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
        '16 ΧΡΗΣΤΟΣ ΜΕΓΚΛΑΣ ΑΒΕΕ Σε τρίτους
        '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

        '1003="Παρημίν"
        If chSalDoc.FINSTATES = 1003 And {2, 3, 4, 5, 8, 13, 16, 17}.Contains(mtrDoc.WHOUSE) Then
            Exit Sub
        End If
        Dim soCarrier As Hglp.SOCARRIER = db.SOCARRIERs.Where(Function(f) f.SOCARRIER = mtrDoc.SOCARRIER).FirstOrDefault
        If IsNothing(soCarrier) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε Μεταφορέας. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If
        Dim suplCode = soCarrier.CODE
        Dim supTrdr As Hglp.TRDR = db.TRDRs.Where(Function(f) f.SODTYPE = 12 And f.CODE = suplCode).FirstOrDefault
        If IsNothing(supTrdr) Then
            MsgBox("Προσοχή!!!. Δεν βρέθηκε αντίστοιχος Προμηθευτής - Μεταφορέας. " & suplCode & vbCrLf & " Η αυτόματη έκδοση προχρέωσης μεταφορέα θα διακοπεί!!!", MsgBoxStyle.Critical, "CreateCarrierDoc")
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Dim myModule As XModule
        myModule = s1Conn.CreateModule("LINSUPDOC;Ειδικές συναλλαγές προμηθευτών New")

        Dim newID As Integer = 0
        Try
            Dim TblHeader As XTable
            Dim TblDetail As XTable

            'Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος
            Dim LinSupDoc = db.FINDOCs.Where(Function(f) f.SOSOURCE = 1253 And f.FINDOCS = chSalDoc.FINDOC).FirstOrDefault

            TblHeader = myModule.GetTable("FINDOC")
            TblDetail = myModule.GetTable("MTRLINES")

            If IsNothing(LinSupDoc) Then
                myModule.InsertData()
                TblHeader.Current("SERIES") = 8000
                'TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
                TblHeader.Current("TRNDATE") = chSalDoc.TRNDATE
            Else
                Dim id As Integer = LinSupDoc.FINDOC
                LinSupDoc = Nothing
                myModule.LocateData(id)
            End If

            TblHeader.Current("TRDR") = supTrdr.TRDR
            TblHeader.Current("FINDOCS") = chSalDoc.FINDOC

            '    ITELINES.FIRST;
            'first_MTRLINES = ITELINES.MTRLINES;
            Dim tccCSHIPVALUE = chSalDoc.MTRLINEs.FirstOrDefault.ccCSHIPVALUE
            Dim tRemarks = "======= " & chSalDoc.FINCODE + " =======" & vbCrLf
            tRemarks &= "Κωδικός" & vbTab + "Ποσότ" & vbTab + "Κόμιστρο" & vbCrLf
            For Each ITELINES In chSalDoc.MTRLINEs
                If Not tccCSHIPVALUE = ITELINES.ccCSHIPVALUE Then
                    MsgBox("Λάθος κόμιστρο. Η διαδικασία θα διακοπεί!!!", MsgBoxStyle.Critical, strAppName)
                    myModule.Dispose()
                    Exit Sub
                End If
                tRemarks = tRemarks + ITELINES.MTRL1.CODE & vbTab & ITELINES.QTY1 & vbTab & ITELINES.ccCSHIPVALUE & vbCrLf
            Next

            'Παρατηρήσεις
            TblHeader.Current("REMARKS") = tRemarks

            'Ειδικές προμηθευτών  sosource=1253   

            Dim mtrlNew = 0
            '7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
            '7041	Δελτίο Αποστολής	Δελτίο αποστολής
            '7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
            If chSalDoc.FPRMS = 7040 Or chSalDoc.FPRMS = 7046 Then
                mtrlNew = 1818 '64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
            End If
            If chSalDoc.FPRMS = 7041 Then
                mtrlNew = 1816 '64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
            End If


            TblDetail.Current("MTRL") = mtrlNew
            TblDetail.Current("QTY1") = 1.0
            TblDetail.Current("LINEVAL") = mtrDoc.ccCTOTSHIPVALUE
            TblDetail.Current("FINDOCS") = chSalDoc.FINDOC
            TblDetail.Current("MTRLINESS") = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES

            'Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
            '204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
            '205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
            '207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
            '208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
            '209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
            '212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


            'WHOUSE	NAME
            '2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
            '3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
            '4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
            '5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            '13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
            '17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

            Select Case mtrDoc.WHOUSE
                Case 2 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
                Case 3 'ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                    TblDetail.Current("COSTCNTR") = 212'Κ.Δ Καβάλας
                Case 4 'ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 204'Κ.Δ Διαβατών Θεσ/κης
                Case 5 'ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 205'Κ.Δ Πύργου
                Case 8 'ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                    TblDetail.Current("COSTCNTR") = 207'Κ.Δ Ασπροπύργου
                Case 13 'ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                    TblDetail.Current("COSTCNTR") = 209'Κ.Δ Βαθύλακος
                Case 17 'ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
                    TblDetail.Current("COSTCNTR") = 208 'Κ.Δ Φυτοθρεπτική
            End Select

            'TblDetail.Add()
            'TblDetail.Current("MTRL") = 1816
            'TblDetail.Current("LINEVAL") = 128.0
            'TblDetail.Current("VAT") = 1410

            newID = myModule.PostData()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'ControlsVisible(True)
        myModule.Dispose()
        'Throw New NotImplementedException()
    End Sub

    Private Sub cmdPelPro4_Click(sender As Object, e As EventArgs) Handles cmdPelPro4.Click
        '[{"docs":["1ΤΙΜ-Α8660"],"docsFindoc":[441841],"docsPrice":[115.86900000000001],"docsVat":[1131]}
        ',{"docs":["1ΤΙΜ-Α8500"],"docsFindoc":[438809],"docsPrice":[115.86900000000001],"docsVat":[1131]}
        ',{"docs":["1ΤΙΜ-Α8751"],"docsFindoc":[444344],"docsPrice":[188.08960000000002],"docsVat":[1131]}
        ',{"docs":["1ΤΙΜ-Α8751"],"docsFindoc":[444344],"docsPrice":[79.152],"docsVat":[1131]}
        ',{"docs":["1ΤΙΜ-Α8500"],"docsFindoc":[438809],"docsPrice":[34.920000000000009],"docsVat":[1131]}
        ',{"docs":["1ΤΙΜ-Α8752"],"docsFindoc":[444346],"docsPrice":[170.72000000000003],"docsVat":[1131]}]
        'Me.txtCOMMENTS.Text = "[{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":79.152,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":34.920000000000009,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8752"",""docsFindoc"":444346,""docsPrice"":170.72000000000003,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8660"",""docsFindoc"":441841,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8660"",""docsFindoc"":441841,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8774"",""docsFindoc"":444417,""docsPrice"":669.1545000000001,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8774"",""docsFindoc"":444417,""docsPrice"":667.845,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":108.0896,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":108.0896,""docsVat"":1131}]"
        Dim opDocs As List(Of OpenItemDocs) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of OpenItemDocs))(Me.txtCOMMENTS.Text)
        If Not IsNothing(opDocs) Then
            'Dim q = From c1 In opDocs Group c1 By c1.docsFindoc Into Group
            '        Select FinCode = Group.FirstOrDefault.docs, Findoc = Group.FirstOrDefault.docsFindoc, PerCnt = Group.FirstOrDefault.docsPerCnt, Price = Group.Sum(Function(f) f.docsPrice), TotPrice = Group.Sum(Function(f) f.docsTotPrice)
            Dim aa = ""
            For Each op In opDocs
                'op.docsTotPrice = Math.Round(op.docsPrice + ((op.docsPrice * op.docsPerCnt) / 100), 2)
                aa &= "FinCode: " & op.docs & " FinDoc: " & op.docsFindoc & " docsVat: " & op.docsPerCnt & " docsPrice: " & op.docsPrice & " docsTotPrice: " & op.docsTotPrice & vbCrLf
            Next
            MsgBox("cmd= " & vbCrLf & aa & vbCrLf & opDocs.Sum(Function(f) f.docsPrice) & vbCrLf & Math.Round(opDocs.Sum(Function(f) f.docsPrice) + ((opDocs.Sum(Function(f) f.docsPrice) * opDocs.FirstOrDefault.docsPerCnt) / 100), 2))
        End If
        'vscs(0).Findoc = 13333
        'Me.StatBindingSource.DataSource = New SortableBindingList(Of vsc)(vscs)
    End Sub

    Private Sub btnOpenFileDialog_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenFileDialog.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.InitialDirectory = "E:\Gm_Softone\Μεταφορείς"
        Dim xlsfilter As String = "Excel files(*.xls*)|*.xls*"
        openFileDialog1.Filter = xlsfilter '"xls files (*.xls)|*.txt|All files (*.*)|*.*" '"txt files (*.txt)|*.txt|All files (*.*)|*.*"
        'openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            'myStream = openFileDialog1.OpenFile()
            FileName.Text = openFileDialog1.FileName
            Me.ddlSheets.Enabled = True
            Me.btnGetExcel.Enabled = True
            GetExcelSheets(FileName.Text, "", "Yes")
        End If
    End Sub
    Private Sub GetExcelSheets(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Try
            ' open xls file
            'Get the Sheets in Excel WorkBook 
            'conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"
            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR={1}'"

            conStr = String.Format(conStr, FilePath, isHDR)
            Using connExcel As New OleDbConnection(conStr)
                Dim cmdExcel As New OleDbCommand()
                'Dim oda As New OleDbDataAdapter()
                cmdExcel.Connection = connExcel
                Try
                    connExcel.Open()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & ex.StackTrace)
                End Try


                Dim dt1 As DataTable = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                'Bind the Sheets to DropDownList 
                'ddlSheets.Items.Clear()
                'Me.ddlSheets.Items.AddRange(New Object() {"1", "2", "3"})
                'ddlSheets.Items.Add(New ListItem("--Select Sheet--", ""))
                ddlSheets.DataSource = dt1 ' connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                ddlSheets.DisplayMember = "TABLE_NAME"
                ddlSheets.ValueMember = "TABLE_NAME"
                'ddlSheets.DataBind()


                'Dim myexceldataquery As String = "Select * from [Coefs$]"

                'Dim oledbcmd As OleDbCommand = New OleDbCommand(myexceldataquery, connExcel)
                'Dim dr As OleDbDataReader = oledbcmd.ExecuteReader()
                'dt.Load(dr)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace)
        End Try
        'Throw New NotImplementedException
    End Sub
    Private Sub btnGetExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnGetExcel.Click
        Try
            ' open xls file
            'Get the Sheets in Excel WorkBoo 

            If Not IsNothing(Me.ddlSheets.SelectedValue) Then
                sheet = Me.ddlSheets.SelectedValue

                Using connExcel As New OleDbConnection(conStr)
                    Try
                        Dim cmdExcel As New OleDbCommand()
                        'Dim oda As New OleDbDataAdapter()
                        cmdExcel.Connection = connExcel
                        connExcel.Open()

                        Dim myexceldataquery As String = "Select * from [" & sheet & "]" 'Coefs$]"

                        Dim oledbcmd As OleDbCommand = New OleDbCommand(myexceldataquery, connExcel)
                        Dim dreader As OleDbDataReader = oledbcmd.ExecuteReader()
                        dt = New DataTable
                        dt.TableName = "Import"
                        dt.Load(dreader)

                        Dim names = dt.Columns.Cast(Of DataColumn).Select(Function(f) f.ColumnName).ToList
                        '(0): "Ημερ/νία"
                        '(1): "Παραστατικό"
                        '(2): "Αιτιολογία"
                        '(3): "Κωδικός"
                        '(4): "Επωνυμία"
                        '(5): "Κωδικός1"
                        '(6): "Περιγραφή"
                        '(7): "ΦΠΑ %"
                        '(8): "Ποσ#1 πώλησης"
                        '(9): "Τιμή"
                        '(10): "Αξία πώλησης"
                        '(11): "Συν#Αξία"
                        '(12): "Ομάδα"
                        '(13): "Οικογένεια"
                        '(14): "Ονομασία/εμπορική Περιγραφή"
                        '(15): "Μετασχ/κε σε"

                        '(0): "Ημερ/νία"
                        '(1): "Παραστατικό"
                        '(2): "Κωδικός"
                        '(3): "Επωνυμία"
                        '(4): "Κωδικός1"
                        '(5): "Περιγραφή"
                        '(6): "ΦΠΑ %"
                        '(7): "Ποσ#1 πώλησης"
                        '(8): "Τιμή"
                        '(9): "μετρητοίς"
                        '(10): "εμπορική πολιτική"


                        '(0): "Ημερ/νία"
                        '(1): "Παραστατικό"
                        '(2): "Αιτιολογία"
                        '(3): "Κωδικός"
                        '(4): "Επωνυμία"
                        '(5): "Είδος"
                        '(6): "Περιγραφή"
                        '(7): "ΦΠΑ %"
                        '(8): "Ποσ#1 πώλησης"
                        '(9): "Τιμή"
                        '(10): "Αξία πώλησης"
                        '(11): "Μετασχ/κε σε"
                        '(12): "Σχετικά Παραστατικά"
                        '(13): "Μετρητοίς"
                        '(14): "Eμπορική πολιτική
                        'Dim HCols = ("Ημερ/νία,Παραστατικό,Αιτιολογία,Κωδικός,Επωνυμία,Είδος,Περιγραφή,ΦΠΑ %,Ποσ#1 πώλησης,Τιμή,Μετρητοίς,Eμπορική πολιτική").Split(",")
                        Dim HCols = ("Ημερ/νία,Παραστατικό,Κωδικός,Επωνυμία,Είδος,Περιγραφή,ΦΠΑ %,Ποσ#1 πώλησης,Τιμή,Μετρητοίς,Eμπορική πολιτική").Split(",")
                        Dim CError = ""
                        Dim col As DataColumn = dt.Columns.Cast(Of DataColumn).Where(Function(f) f.ColumnName.ToLower = "κωδικός1").FirstOrDefault
                        If Not IsNothing(col) Then
                            col.ColumnName = "Είδος"
                        End If
                        col = dt.Columns.Cast(Of DataColumn).Where(Function(f) f.ColumnName.ToLower = "μετρητοίς").FirstOrDefault
                        If Not IsNothing(col) Then
                            col.ColumnName = "Μετρητοίς"
                        End If
                        col = dt.Columns.Cast(Of DataColumn).Where(Function(f) f.ColumnName.ToLower = "εμπορική πολιτική").FirstOrDefault
                        If Not IsNothing(col) Then
                            col.ColumnName = "Eμπορική πολιτική"
                        End If

                        For Each hc In HCols
                            col = dt.Columns.Cast(Of DataColumn).Where(Function(f) f.ColumnName = hc).FirstOrDefault
                            If IsNothing(col) Then
                                CError &= hc & ","
                            End If
                        Next

                        If Not CError = "" Then
                            CError = CError.Substring(0, Len(CError) - 1)
                            MsgBox("Προσοχή !!! Λάθος Στήλες." & vbCrLf & CError, MsgBoxStyle.Critical, "btnGetExcel")
                            Exit Sub
                        End If
                        'db.Log = Console.Out
                        CError = ""
                        vscs = New List(Of vsc)
                        For Each dr As DataRow In dt.Rows
                            If Not dr("Ημερ/νία").GetType.FullName = "System.DateTime" Then
                                Continue For
                            End If
                            Dim vs As New vsc
                            vs.TrnDate = dr("Ημερ/νία")
                            vs.FinCode = dr("Παραστατικό")
                            'Dim fi = db.FINDOCs.Where(Function(f) f.TRNDATE = vs.TrnDate And f.FINCODE = vs.FinCode).FirstOrDefault
                            'If IsNothing(fi) Then
                            '    CError &= vs.FinCode & ","
                            '    Continue For
                            'End If
                            'vs.Findoc = fi.FINDOC
                            'vs.TFprms = fi.TFPRMS
                            'vs.comments = fi.COMMENTS
                            vs.CODE = dr("Κωδικός")
                            vs.NAME = dr("Επωνυμία")
                            vs.m_CODE = dr("Είδος")
                            vs.m_NAME = dr("Περιγραφή")
                            vs.PERCNT = dr("ΦΠΑ %")
                            vs.Qty1 = IIf(dr("Ποσ#1 πώλησης").Equals(DBNull.Value), 0, dr("Ποσ#1 πώλησης"))
                            'If vs.TFprms = 152 Then
                            '    Dim mts = fi.MTRLINEs.Where(Function(f) f.FINDOC = vs.Findoc).ToList
                            '    vs.mtrl = mts.Where(Function(f) f.MTRL1.CODE = vs.m_CODE).FirstOrDefault.MTRL
                            '    vs.findocs = mts.Where(Function(f) f.MTRL = vs.mtrl).FirstOrDefault.FINDOCS
                            'End If
                            vs.Price = IIf(dr("Τιμή").Equals(DBNull.Value), 0, dr("Τιμή"))
                            vs.DiscPrc = IIf(dr("Μετρητοίς").Equals(DBNull.Value), 0, dr("Μετρητοίς")) * 100
                            vs.ExpDiscVal = IIf(dr("Eμπορική πολιτική").Equals(DBNull.Value), 0, dr("Eμπορική πολιτική"))

                            vscs.Add(vs)
                        Next


                        If vscs.Count > 0 Then
                            Me.ToolStripProgressBar2.Maximum = vscs.Count
                            ' Set the initial value of the ProgressBar.
                            Me.ToolStripProgressBar2.Value = 1
                        End If


                        'Dim fis = From fi In db.FINDOCs Join vs In vscs On fi.TRNDATE Equals vs.TrnDate And fi.FINCODE Equals vs.FinCode
                        Dim lno As Integer = 0
                        For Each v In vscs
                            Me.ToolStripProgressBar2.PerformStep()

                            Dim fi = db.FINDOCs.Where(Function(f) f.TRNDATE = v.TrnDate And f.FINCODE = v.FinCode).FirstOrDefault
                            If IsNothing(fi) Then
                                CError &= v.FinCode & ","
                                Continue For
                            End If

                            'vs.TrnDate = dr("Ημερ/νία")
                            'vs.FinCode = dr("Παραστατικό")
                            'vs.CODE = dr("Κωδικός")
                            'vs.NAME = dr("Επωνυμία")
                            'vs.m_CODE = dr("Είδος")
                            'vs.m_NAME = dr("Περιγραφή")
                            'vs.PERCNT = dr("ΦΠΑ %")
                            'vs.Qty1 = IIf(dr("Ποσ#1 πώλησης").Equals(DBNull.Value), 0, dr("Ποσ#1 πώλησης"))
                            'vs.Price = IIf(dr("Τιμή").Equals(DBNull.Value), 0, dr("Τιμή"))
                            'vs.DiscPrc = IIf(dr("Μετρητοίς").Equals(DBNull.Value), 0, dr("Μετρητοίς")) * 100
                            'vs.ExpDiscVal = IIf(dr("Eμπορική πολιτική").Equals(DBNull.Value), 0, dr("Eμπορική πολιτική"))

                            With v
                                .Company = 1000
                                .SoSource = fi.SOSOURCE
                                .TFprms = fi.TFPRMS
                                '.TSodType = fi.TSodType
                                .IsCancel = fi.ISCANCEL
                                .Fprms = fi.FPRMS
                                .Findoc = fi.FINDOC
                                .TrnDate = fi.TRNDATE
                                .FinCode = fi.FINCODE
                                .series = fi.SERIES
                                .Trdr = fi.TRDR
                                .TrdBranch = fi.TRDBRANCH
                                '.CODE = td.CODE 'dr("Κωδικός")
                                '.NAME = td.NAME 'dr("Επωνυμία")
                                '.mtrl = fi.Mtrl 'below in mts 
                                '.m_CODE = m.CODE 'dr("Είδος")
                                '.m_NAME = m.NAME 'dr("Περιγραφή")
                                '.Vat = fi.Vat 'below in mts
                                '.Qty1 = fi.Qty1 ' IIf(dr("Ποσ#1 πώλησης").Equals(DBNull.Value), 0, dr("Ποσ#1 πώλησης"))
                                '.Price = fi.Price 'IIf(dr("Τιμή").Equals(DBNull.Value), 0, dr("Τιμή"))
                                '.VATNAME = v.NAME
                                '.PERCNT = v.PERCNT 'dr("ΦΠΑ %")
                                .comments = fi.COMMENTS
                                lno += 1
                                v.LinesNo = lno
                            End With

                            'v.Findoc = fi.FINDOC
                            'v.TFprms = fi.TFPRMS
                            'v.Trdr = fi.TRDR
                            'v.TrdBranch = fi.TRDBRANCH
                            'v.comments = fi.COMMENTS

                            Dim mts = fi.MTRLINEs.Where(Function(f) f.FINDOC = v.Findoc).ToList
                            Try
                                v.mtrl = mts.Where(Function(f) f.MTRL1.CODE = v.m_CODE).FirstOrDefault.MTRL
                                v.Vat = mts.Where(Function(f) f.MTRL1.CODE = v.m_CODE).FirstOrDefault.VAT
                                If v.TFprms = 152 Then
                                    v.findocs = mts.Where(Function(f) f.MTRL = v.mtrl).FirstOrDefault.FINDOCS
                                    Dim mtss = mts.Where(Function(f) f.MTRL = v.mtrl).FirstOrDefault
                                    v.findocs = mtss.FINDOCS
                                    'v.pisPrice = mtss.PRICE
                                End If
                                v.DiscPrc = Math.Abs(v.DiscPrc)
                                v.ExpDiscVal = Math.Abs(v.ExpDiscVal)
                            Catch ex As Exception
                                MsgBox("Προσοχή !!! Λάθος είδος: " & v.m_CODE & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "btnGetExcel")
                            End Try

                        Next

                        Dim PisMiss = vscs.Where(Function(f) f.TFprms = 152 And f.findocs Is Nothing).ToList
                        If PisMiss.Count > 0 Then
                            Me.chkBoxAuto.Checked = False
                        End If
                        impExcel = True
                        'CalcPistold(vscs)
                        'Dim tims = vscs.Where(Function(f) f.TFprms = 102)
                        'Dim SerCre = (From cs In db.cccSettingsLines Select If(cs.SeriesCre, 0)).ToList
                        'SerCre = SerCre.Union(From cs In db.cccSettingsLines Select If(cs.SeriesCreSec, 0)).ToList

                        'For Each v In tims
                        '    Dim pis = vscs.Where(Function(f) f.TFprms = 152 And If(f.findocs, 0) = v.Findoc And f.mtrl = v.mtrl And SerCre.Contains(If(f.series, 0))).ToList
                        '    If Not pis.Count = 0 Then
                        '        Dim pisPrice As Double = 0
                        '        For Each pi In pis
                        '            pisPrice -= pi.Price
                        '        Next
                        '        v.pisPrice = pisPrice
                        '        v.newPrice = v.Price - v.pisPrice
                        '    End If
                        'Next


                        If Not CError = "" Then
                            CError = CError.Substring(0, Len(CError) - 1)
                            MsgBox("Προσοχή !!! Λάθος Στήλες." & vbCrLf & CError, MsgBoxStyle.Critical, "btnGetExcel")
                            Exit Sub
                        End If
                        fvscs = vscs 'btnCheck.Click
                        Me.StatBindingSource.DataSource = New SortableBindingList(Of vsc)(vscs)

                        Dim lst As New List(Of vsc)

                        lst = CType(Me.StatBindingSource.DataSource, SortableBindingList(Of vsc)).ToList
                        Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))

                        lst = lst.Where(Function(f) f.TFprms = 102).ToList

                        Me.lblV07.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
                        Me.lblV08.Text = ""
                        Me.StatDataGridView.DataSource = Me.StatBindingSource
                        StatDataGridView_Styling()

                        Me.VscsBindingSource.DataSource = New SortableBindingList(Of vsc)(New List(Of vsc))
                        Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
                        MTRLINEsDataGridView_Styling()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End Using
                Me.btnCheck.Enabled = True
            End If
            'Throw New NotImplementedException
        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If Me.TlSTxtTRDR.Text = "" Then
            vscs = fvscs
        Else
            vscs = vscs.Where(Function(F) F.CODE = Me.TlSTxtTRDR.Text).ToList
        End If


        Me.StatBindingSource.DataSource = New SortableBindingList(Of vsc)(vscs)
        Me.StatDataGridView.DataSource = Me.StatBindingSource
        StatDataGridView_Styling()
    End Sub
#End Region
#Region "98 - Methods"
    Private Sub CalcPistold(ByRef vscs As List(Of vsc))
        Dim tims = vscs.Where(Function(f) f.TFprms = 102)
        Dim SerCre = (From cs In db.cccSettingsLines Select If(cs.SeriesCre, 0)).ToList
        SerCre = SerCre.Union(From cs In db.cccSettingsLines Select If(cs.SeriesCreSec, 0)).ToList

        For Each v In tims
            Dim pis = vscs.Where(Function(f) f.TFprms = 152 And If(f.findocs, 0) = v.Findoc And f.mtrl = v.mtrl And SerCre.Contains(If(f.series, 0))).ToList
            v.newPrice = v.Price
            If Not pis.Count = 0 Then
                Dim pisPrice As Double = 0
                For Each pi In pis
                    pisPrice += pi.Price
                Next
                v.pisPrice = pisPrice
                v.newPrice = v.Price - IIf(impExcel, v.pisPrice * -1, v.pisPrice * 1)
            End If
        Next
        'Throw New NotImplementedException()
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        'LoadDataInit() 'For Bind Any Control
    End Sub
    ' Load the data.
    Private Sub LoadData()
        db = New DataClassesHglpDataContext(conn) 'My.Settings.GenConnectionString)
        db.CommandTimeout = Me.txtTimeOut.Text
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            Me.StatBindingSource.DataSource = db.ccCVShipments.Where(Function(f) f.FINDOC = 0)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddNewSales()
        'If cmbSeries.Text <> "" And cmbCustomer.Text <> "" And cmbItem.Text <> "" _
        '    And txtQTY.Text <> "" And txtPrice.Text <> "" And IsNumeric(txtQTY.Text) And IsNumeric(txtPrice.Text) Then
        '    Me.Cursor = Cursors.WaitCursor
        '    Try
        '        S1_WSSetData(s1Conn, lblActiveObject.Tag, "", cmbSeries.SelectedItem(0), cmbCustomer.SelectedItem(0), cmbItem.SelectedItem(0), Convert.ToDecimal(txtQTY.Text), Convert.ToDecimal(txtPrice.Text))
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        '    Finally
        '        Me.Cursor = Cursors.Default
        '    End Try
        '    ControlsVisible(True)
        'Else
        '    MsgBox("Please fill In all the fields!!!", MsgBoxStyle.Critical, strAppName)
        'End If
        ' lblActiveObject.Tag = "SALDOC"
        Me.Cursor = Cursors.WaitCursor
        Try
            S1_WSSetData(s1Conn, "SALDOC", "", 1004, 252, 2020, Convert.ToDecimal(10), Convert.ToDecimal(10))
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        If Me.VscsBindingSource.Count > 0 Then
            If MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim hd As New vsc
            Dim nlst As New List(Of vsc)

            Me.Cursor = Cursors.WaitCursor
            Try
                hd.series = 1004
                hd.TrnDate = Now()
                Dim st As vsc = Me.StatBindingSource.Current
                hd.Trdr = st.Trdr
                hd.TrdBranch = st.TrdBranch
                'Αιτιολογία
                hd.comments = Me.txtCOMMENTS.Text 'st.comments
                hd.remarks = Me.txtCOMMENTS.Text


                Dim lst As New List(Of vsc)
                lst = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

                Dim opDocs As New List(Of OpenItemDocs)

                For Each ls In lst
                    Dim ExpDiscVal = ls.ExpDiscVal
                    Dim DiscPrc = ls.DiscPrc

                    Dim l1 = New vsc
                    For Each pi As PropertyInfo In GetType(vsc).GetProperties()
                        Dim name = pi.Name
                        Try
                            Dim val = GetType(vsc).GetProperty(name).GetValue(ls, Nothing)
                            If Not IsNothing(val) Then
                                GetType(vsc).GetProperty(name).SetValue(l1, val, Nothing)
                            End If
                        Catch ex As Exception

                        End Try
                    Next

                    If Not ls.ExpDiscVal = 0 Then
                        l1.LPrice = ExpDiscVal
                    End If
                    If Not DiscPrc = 0 Then
                        l1.DiscPrc = 0
                    End If

                    nlst.Add(l1)

                    If Not ExpDiscVal = 0 And Not DiscPrc = 0 Then
                        l1 = New vsc
                        For Each pi As PropertyInfo In GetType(vsc).GetProperties()
                            Dim name = pi.Name
                            Try
                                Dim val = GetType(vsc).GetProperty(name).GetValue(ls, Nothing)
                                If Not IsNothing(val) Then
                                    GetType(vsc).GetProperty(name).SetValue(l1, val, Nothing)
                                End If
                            Catch ex As Exception

                            End Try
                        Next
                        If Not ExpDiscVal = 0 Then
                            l1.ExpDiscVal = 0
                        End If
                        'l1.LPrice = ((l1.Price - ExpDiscVal) * (DiscPrc / 100))
                        l1.DiscValPrice = l1.newPrice - ExpDiscVal
                        nlst.Add(l1)
                    End If
                Next

                For Each ls In nlst
                    Dim ExpDiscVal = ls.ExpDiscVal
                    Dim DiscPrc = ls.DiscPrc

                    If Not ExpDiscVal = 0 Then
                        ls.LPrice = ExpDiscVal
                    End If
                    If Not ls.DiscPrc = 0 Then
                        Dim Price = ls.newPrice
                        If Not ls.DiscValPrice = 0 Then
                            Price = ls.DiscValPrice
                        End If
                        'ls.LPrice = ExpDiscVal + ((ls.Price - ExpDiscVal) * (DiscPrc / 100))
                        ls.LPrice = Price * (DiscPrc / 100)
                    End If
                    Dim op As New OpenItemDocs
                    'op.docs = ls.Opdocs.docs
                    'op.docsFindoc = ls.Opdocs.docsFindoc
                    'op.docsPrice = ls.Opdocs.docsPrice
                    'op = ls.Opdocs
                    op.docsFindoc = ls.Findoc ' ls.Opdocs.docsFindoc
                    op.docs = ls.FinCode ' ls.Opdocs.docs
                    op.docsPrice = Math.Round(ls.Qty1 * Math.Round(ls.LPrice, 2), 2)
                    op.docsPerCnt = ls.PERCNT ' ls.Opdocs.docsVat
                    op.docsTotPrice = Math.Round(op.docsPrice + ((op.docsPrice * ls.PERCNT) / 100), 2)
                    opDocs.Add(op)
                Next

                Dim opDocsTot As New List(Of OpenItemDocs)
                If Not IsNothing(opDocs) Then
                    Dim q2 = From c1 In opDocs Group c1 By c1.docsFindoc Into Group
                             Select FinCode = Group.FirstOrDefault.docs, Findoc = Group.FirstOrDefault.docsFindoc, PerCnt = Group.FirstOrDefault.docsPerCnt, Price = Group.Sum(Function(f) f.docsPrice), TotPrice = Group.Sum(Function(f) f.docsTotPrice)

                    For Each op In q2
                        Dim op1 As New OpenItemDocs
                        Dim fn = db.FINPAYTERMs.Where(Function(f) f.FINDOC = op.Findoc).FirstOrDefault
                        If IsNothing(fn) Then
                            MsgBox("Προσοχή!!! Δεν υπάρχει στον db.FINPAYTERMs", MsgBoxStyle.Critical, strAppName)
                            Exit Sub
                        End If
                        op1.docsFindoc = fn.FINPAYTERMS ' ls.Opdocs.docsFindoc
                        op1.docs = op.FinCode ' ls.Opdocs.docs
                        op1.docsPrice = op.Price
                        op1.docsPerCnt = op.PerCnt ' ls.Opdocs.docsVat
                        op1.docsTotPrice = Math.Round(op1.docsPrice + ((op1.docsPrice * op1.docsPerCnt) / 100), 2)
                        opDocsTot.Add(op1)
                    Next
                End If

                Dim ccCOpenItem = Newtonsoft.Json.JsonConvert.SerializeObject(opDocsTot)

                Dim mts = ""
                'For Each ls In nlst
                '    mts += "{ ""MTRL"":  """ + ls.mtrl.ToString + """ , ""QTY1"": """ + ls.Qty1.ToString + """, ""PRICE"": """ + ls.LPrice.ToString + """ } " + ","
                'Next
                'If Not mts = "" Then
                '    mts = mts.Substring(0, Len(mts) - 1)
                'End If

                Dim WS_CallRequest As String = ""
                'WS_CallRequest = String.Format("""service"":""setData"",""OBJECT"":""{0}"",""KEY"":""{1}"",""DATA"":", strObject, strKEY)
                'WS_CallRequest += "{ ""SALDOC"": [ { ""SERIES"": """ + iSeries.ToString + """ , ""TRDR"": """ + iTRDR.ToString + """ } ], "
                'WS_CallRequest += """ITELINES"": [ { ""MTRL"": """ + iMTRL.ToString + """ , ""QTY1"": """ + dQTY.ToString + """, ""PRICE"": """ + dPrice.ToString + """ } ] }"




                'ccCOpenItem = Chr(34) & ccCOpenItem & Chr(34)
                'hd.comments = "gm1 " & ccCOpenItem & " gm2"
                'ccCOpenItem = ""


                Dim json1 As String = "
                    {
                        Name: ""test1"",
                        Items: {
                            Name: ""test1items""
                        }
                    }"
                Dim json2 As String = "
                    {
                        ""SomeField"": ""SomeData""
                    }"
                Dim obj1 = Newtonsoft.Json.Linq.JObject.Parse(json1)
                Dim obj2 = Newtonsoft.Json.Linq.JObject.Parse(json2)
                obj1("Items")("Data") = obj2
                Dim newJson = obj1.ToString()



                WS_CallRequest = String.Format("""service"":""setData"",""OBJECT"":""{0}"",""KEY"":""{1}"",""DATA"":", "SALDOC", "")
                WS_CallRequest += "{ ""SALDOC"": [ { ""SERIES"": """ + hd.series.ToString + """ , ""TRDR"": """ + hd.Trdr.ToString + """ , ""TrdBranch"": """ + hd.TrdBranch.ToString + """ , ""comments"": """ + hd.comments.ToString + """, ""ccCOpenItem"": """ + ccCOpenItem.ToString + """ } ], "
                WS_CallRequest += """ITELINES"": ["

                WS_CallRequest += mts + "] }"

                '' , ""ccCOpenItem"": """ + ccCOpenItem.ToString + ""

                'obj1 = Newtonsoft.Json.Linq.JObject.Parse(WS_CallRequest)
                'obj2 = Newtonsoft.Json.Linq.JObject.Parse(ccCOpenItem)

                'obj1("SALDOC")("ccCOpenItem") = obj2
                'newJson = obj1.ToString()

                Dim nc As New gmsal
                nc.service = "setData"
                nc.OBJECT = "SALDOC"
                nc.KEY = ""
                Dim h As New gmh
                h.SERIES = hd.series
                h.TRDR = hd.Trdr
                h.TrdBranch = hd.TrdBranch
                h.comments = hd.comments
                'opDocs = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of OpenItemDocs))(ccCOpenItem)
                h.ccCOpenItem = ccCOpenItem 'opDocs
                nc.DATA = h
                Dim ms As New List(Of gmm)
                For Each ls In nlst
                    Dim m As New gmm
                    m.MTRL = ls.mtrl
                    m.QTY1 = ls.Qty1
                    m.PRICE = ls.LPrice
                    ms.Add(m)
                    'Exit for
                Next


                opDocs = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of OpenItemDocs))(ccCOpenItem)
                Dim aa = "[{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":79.152,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":34.920000000000009,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8752"",""docsFindoc"":444346,""docsPrice"":170.72000000000003,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8660"",""docsFindoc"":441841,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8660"",""docsFindoc"":441841,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8500"",""docsFindoc"":438809,""docsPrice"":70.869,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8774"",""docsFindoc"":444417,""docsPrice"":669.1545000000001,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8774"",""docsFindoc"":444417,""docsPrice"":667.845,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":108.0896,""docsVat"":1131},{""docs"":""1ΤΙΜ-Α8751"",""docsFindoc"":444344,""docsPrice"":108.0896,""docsVat"":1131}]"

                Dim q = From g1 In opDocs Group g1 By g1.docs.FirstOrDefault Into Group
                        Select Group.FirstOrDefault.docs.FirstOrDefault, Group.FirstOrDefault.docsFindoc ', Group.Sum(Function(f) f.docsPrice.FirstOrDefault))

                For Each q1 In q
                    ' q1.FirstO
                Next
                'h.ccCOpenItem.Clear()

                WS_CallRequest = String.Format("""service"":""setData"",""OBJECT"":""{0}"",""KEY"":""{1}"",""DATA"":", "SALDOC", "") + "{ ""SALDOC"": [ " + Newtonsoft.Json.JsonConvert.SerializeObject(h) + " ], "
                'WS_CallRequest += "{ ""SALDOC"": [ { ""SERIES"": """ + hd.series.ToString + """ , ""TRDR"": """ + hd.Trdr.ToString + """ , ""TrdBranch"": """ + hd.TrdBranch.ToString + """ , ""comments"": """ + hd.comments.ToString + """, ""ccCOpenItem"": """ + ccCOpenItem.ToString + """ } ], "
                WS_CallRequest += """ITELINES"": " + Newtonsoft.Json.JsonConvert.SerializeObject(ms)

                WS_CallRequest += " }"

                Dim test = Newtonsoft.Json.JsonConvert.SerializeObject(nc)
                Dim gg = Newtonsoft.Json.JsonConvert.DeserializeObject(Of gmsal)(test)

                S1_WSSetData(s1Conn, "SALDOC", "", WS_CallRequest)

                Me.lblV08.Text = ""

                Me.VscsBindingSource.DataSource = New SortableBindingList(Of vsc)(New List(Of vsc))
                Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
                MTRLINEsDataGridView_Styling()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                Me.Cursor = Cursors.Default
            End Try

        End If
    End Sub
    Class gmsal
        Public Property service As String
        Public Property [OBJECT] As String
        Public Property KEY As String
        Public Property DATA As gmh
    End Class
    Class gmh
        Public Property SERIES As Integer?
        Public Property TRDR As Integer?
        Public Property TrdBranch As Integer?
        Public Property comments As String
        Public Property ccCOpenItem As String 'As List(Of OpenItemDocs)
    End Class
    Class gmm
        Public Property MTRL As Integer
        Public Property QTY1 As Double
        Public Property PRICE As Double
    End Class
    Private Sub BindingNavigatorSaveItemold_Click(sender As Object, e As EventArgs) 'Handles BindingNavigatorSaveItem.Click
        AddNewSales()
        Exit Sub
        If Me.VscsBindingSource.Count > 0 Then
            If MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.No Then
                Exit Sub
            End If
            Dim myModule As XModule
            myModule = s1Conn.CreateModule("SALDOC")

            Dim newID As Integer = 0
            Try
                Dim TblHeader As XTable
                Dim TblDetail As XTable

                TblHeader = myModule.GetTable("SALDOC")
                TblDetail = myModule.GetTable("ITELINES")

                myModule.InsertData()
                TblHeader.Current("SERIES") = 1004
                TblHeader.Current("TRNDATE") = Now()
                Dim st As vsc = Me.StatBindingSource.Current
                TblHeader.Current("TRDR") = st.Trdr
                TblHeader.Current("TRDBRANCH") = st.TrdBranch
                'Αιτιολογία
                TblHeader.Current("COMMENTS") = Me.txtCOMMENTS.Text 'st.comments

                Dim aa = 0
                'aa = TblDetail.Add()
                ''TblDetail.Current.Post()

                'TblDetail.Current("MTRL") = st.mtrl
                'TblDetail.Current("QTY1") = 1.0
                'TblDetail.Current("VAT") = CInt(st.Vat)
                'TblDetail.Current("LINEVAL") = st.Price 'MTRDOC.ccCTOTSHIPVALUE
                'TblDetail.Current("MTRLINES") = 1
                'TblDetail.Current("LINENUM") = 1
                ''TblDetail.Current.Post()

                'newID = myModule.PostData()

                Dim lst As SortableBindingList(Of vsc) = Me.VscsBindingSource.DataSource
                'Dim lst As List(Of vsc) = Me.VscsBindingSource.DataSource
                Dim opDocs As New List(Of OpenItemDocs)

                'TblDetail.Current("FINDOCS") = chSalDoc.FINDOC
                'TblDetail.Current("MTRLINESS") = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES

                Dim detNew As New Hglp.MTRLINE

                Dim dt As DataTable = TblDetail.CreateDataTable(True)
                'For cnt As Integer = 0 To lst.Count - 1
                '    'aa = TblDetail.Add()
                Dim nlst As New List(Of vsc)

                For Each ls In lst
                    Dim ExpDiscVal = ls.ExpDiscVal
                    Dim DiscPrc = ls.DiscPrc

                    Dim l1 = New vsc
                    For Each pi As PropertyInfo In GetType(vsc).GetProperties()
                        Dim name = pi.Name
                        Try
                            Dim val = GetType(vsc).GetProperty(name).GetValue(ls, Nothing)
                            If Not IsNothing(val) Then
                                GetType(vsc).GetProperty(name).SetValue(l1, val, Nothing)
                            End If
                        Catch ex As Exception

                        End Try
                    Next



                    If Not ls.ExpDiscVal = 0 Then
                        l1.LPrice = ExpDiscVal
                    End If
                    If Not DiscPrc = 0 Then
                        l1.DiscPrc = 0
                    End If

                    nlst.Add(l1)

                    If Not ExpDiscVal = 0 And Not DiscPrc = 0 Then
                        l1 = New vsc
                        For Each pi As PropertyInfo In GetType(vsc).GetProperties()
                            Dim name = pi.Name
                            Try
                                Dim val = GetType(vsc).GetProperty(name).GetValue(ls, Nothing)
                                If Not IsNothing(val) Then
                                    GetType(vsc).GetProperty(name).SetValue(l1, val, Nothing)
                                End If
                            Catch ex As Exception

                            End Try
                        Next
                        If Not ExpDiscVal = 0 Then
                            l1.ExpDiscVal = 0
                        End If
                        'l1.LPrice = ((l1.Price - ExpDiscVal) * (DiscPrc / 100))
                        l1.DiscValPrice = l1.Price - ExpDiscVal
                        nlst.Add(l1)
                    End If
                Next

                For Each ls In nlst
                    Dim ExpDiscVal = ls.ExpDiscVal
                    Dim DiscPrc = ls.DiscPrc

                    If Not ExpDiscVal = 0 Then
                        ls.LPrice = ExpDiscVal
                    End If
                    If Not ls.DiscPrc = 0 Then
                        Dim Price = ls.Price
                        If Not ls.DiscValPrice = 0 Then
                            Price = ls.DiscValPrice
                        End If
                        'ls.LPrice = ExpDiscVal + ((ls.Price - ExpDiscVal) * (DiscPrc / 100))
                        ls.LPrice = Price * (DiscPrc / 100)
                    End If

                Next

                For Each ls In nlst
                    'Dim ls = lst(cnt)
                    '"mtrl", "Qty1", "LPrice"
                    TblDetail.Current("MTRL") = ls.mtrl
                    TblDetail.Current("QTY1") = ls.Qty1
                    'TblDetail.Current("VAT") = CInt(ls.Vat)
                    TblDetail.Current("PRICE") = ls.LPrice
                    'TblDetail.Current("LINEVAL") = ls.LPrice
                    'TblDetail.Current("LINENUM") = 1
                    'TblDetail.Current("FINDOCS") = chSalDoc.FINDOC
                    'TblDetail.Current("MTRLINESS") = chSalDoc.MTRLINEs.FirstOrDefault.MTRLINES
                    'TblDetail.Current.Post()
                    Dim op As New OpenItemDocs
                    'op.docs = ls.Opdocs.docs
                    'op.docsFindoc = ls.Opdocs.docsFindoc
                    'op.docsPrice = ls.Opdocs.docsPrice
                    'op = ls.Opdocs
                    opDocs.Add(op)

                    aa = TblDetail.Add()

                Next
                Dim gg2 = Newtonsoft.Json.JsonConvert.SerializeObject(opDocs, Newtonsoft.Json.Formatting.None)

                Dim ccCOpenItem = Newtonsoft.Json.JsonConvert.SerializeObject(opDocs)

                'Dim ccCOpenItem = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of OpenItemDocs))(myContent)


                TblHeader.Current("ccCOpenItem") = ccCOpenItem

                opDocs = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of OpenItemDocs))(TblHeader.Current("ccCOpenItem"))


                dt = TblDetail.CreateDataTable(True)


                newID = myModule.PostData()

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                'Me.Cursor = Cursors.Default
            End Try
            'ControlsVisible(True)
            myModule.Dispose()
        End If
        vscs = New List(Of vsc)
        Cmd_Select()
    End Sub
    Private Sub VscsBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles VscsBindingSource.ListChanged
        Me.lblV09.Text = 0
        Me.lblV10.Text = 0
        Me.BindingNavigatorSaveItem.Enabled = False
        If Me.VscsBindingSource.Count > 0 Then
            Dim lst As New List(Of vsc)
            lst = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

            Me.lblV08.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
            Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))
            Dim TotPriceFPA As Double = 0
            For Each ls In lst
                TotPriceFPA += ls.TotPrice + (ls.TotPrice * ls.PERCNT / 100)
            Next
            Me.lblV10.Text = String.Format("{0:N2}", TotPriceFPA)
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
        'If e.ListChangedType = ListChangedType.ItemChanged Then
        '    Dim nu As ccCVShipment = MasterBindingSource.Current
        '    'nu.modifiedOn = Now()
        '    Me.BindingNavigatorSaveItem.Enabled = True
        'End If
        'If e.ListChangedType = ListChangedType.ItemAdded Then
        '    Me.BindingNavigatorSaveItem.Enabled = True
        'End If
    End Sub

    Private Sub VscsBindingSource_AddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles VscsBindingSource.AddingNew
        Try
            e.NewObject = addVsc
            'Dim v = New vsc

            ''myArrF = ("TrnDate,FinCode,CODE,NAME,m_CODE,m_NAME,PERCNT,Qty1,Price,Findoc,TFprms").Split(",")
            ''myArrN = ("Ημερ/νία,Παραστατικό,Κωδικός,Επωνυμία,Κωδικός Είδοuς,Περιγραφή,ΦΠΑ %,Ποσ.1,Τιμή,Findoc,TFprms").Split(",")



            'Dim tims = chkVscL.Where(Function(f) f.TFprms = 102)




            'v.mtrl = chkVscL.FirstOrDefault.mtrl
            'v.m_CODE = chkVscL.FirstOrDefault.m_CODE '"200003050532"
            'v.m_NAME = chkVscL.FirstOrDefault.m_NAME
            'v.PERCNT = chkVscL.FirstOrDefault.PERCNT
            'v.Qty1 = chkVscL.FirstOrDefault.Qty1
            'v.Vat = chkVscL.FirstOrDefault.Vat
            ''v.Price = chkVscL.Sum(Function(f) f.Price * IIf(f.TFprms = 152, -1.0, 1.0))
            'For Each l In chkVscL
            '    v.Price += l.Price * IIf(l.TFprms = 152, -1, 1)
            'Next
            ''v.DiscPrc = 0
            ''v.ExpDiscVal = 0
            ''v.LPrice = v.Price * (v.DiscPrc / 100)

            'v.ViewDocs = ""
            'chkVscL = chkVscL.Where(Function(f) f.TFprms = 102).ToList 'Τιμολόγια
            'v.Opdocs = chkVscL.FirstOrDefault.Opdocs
            'If chkVscL.Count > 0 Then
            '    For Each l In chkVscL
            '        v.ViewDocs &= "[" & l.FinCode & "],"
            '        v.ViewdocsFindoc &= l.Findoc & ";"
            '        v.ViewdocsPrice &= l.Price.ToString & ";"
            '        'l.Opdocs = New OpenItemDocs
            '        'l.Opdocs.docs = New List(Of String)
            '        'l.Opdocs.docsFindoc = New List(Of Integer)
            '        'l.Opdocs.docsPrice = New List(Of Double)
            '        'l.Opdocs.docs.Add(l.FinCode)
            '        'l.Opdocs.docsFindoc.Add(l.Findoc)
            '        'l.Opdocs.docsPrice.Add(l.Price)
            '    Next
            '    v.ViewDocs = v.ViewDocs.Substring(0, Len(v.ViewDocs) - 1)
            '    v.ViewdocsFindoc = v.ViewdocsFindoc.Substring(0, Len(v.ViewdocsFindoc) - 1)
            '    v.ViewdocsPrice = v.ViewdocsPrice.Substring(0, Len(v.ViewdocsPrice) - 1)
            'End If







            ''''Dim NFINDOC As New Hglp.FINDOC
            '''''NFINDOC.COMPANY = 1
            '''''NFINDOC.LOCKID = 1
            ''''NFINDOC.FINDOC = 0
            ''''NFINDOC.SOSOURCE = NSOSOURCE '1351
            '''''NFINDOC.SOREDIR = 0
            '''''NFINDOC.TRNDATE = Now()
            '''''NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            '''''NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            ''''NFINDOC.SERIES = NSeries '1004
            '''''Dim snum As SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            '''''NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            ''''NFINDOC.FPRMS = NSeries '1004
            ''''NFINDOC.TFPRMS = 100 '100
            ''''Dim fmt As String = ""
            ''''Select Case NSeries
            ''''    Case 1004
            ''''        fmt = "ΠΡΠΤ-Α0000"
            ''''End Select
            '''''ΑΠΓΡ
            ''''NFINDOC.FINCODE = fmt
            ''''NFINDOC.TRDR = 0
            ''''NFINDOC = ZeroFindoc(NFINDOC)

            ''''e.NewObject = NFINDOC
            'e.NewObject = v
        Catch ex As Exception

        End Try
    End Sub
    Private Function ZeroFindoc(NFINDOC As Hglp.FINDOC) As Hglp.FINDOC
        Try
            NFINDOC.COMPANY = NFINDOC.COMPANY
            NFINDOC.LOCKID = 1
            'NFINDOC.FINDOC = 0
            'NFINDOC.SOSOURCE = 1171
            NFINDOC.SOREDIR = 0
            NFINDOC.TRNDATE = Now()
            NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            Dim snum As Hglp.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            'NFINDOC.FPRMS = 1001
            'NFINDOC.TFPRMS = 100
            Dim fmt As String = NFINDOC.FINCODE
            NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
            NFINDOC.BRANCH = NFINDOC.BRANCH
            NFINDOC.SODTYPE = 13
            NFINDOC.SOCURRENCY = 1
            NFINDOC.TRDRRATE = 0
            NFINDOC.LRATE = 1
            NFINDOC.ORIGIN = 1
            NFINDOC.GLUPD = 0
            NFINDOC.SXUPD = 0
            NFINDOC.PRDCOST = 0
            NFINDOC.ISCANCEL = 0
            NFINDOC.ISPRINT = 0
            NFINDOC.ISREADONLY = 0
            NFINDOC.APPRV = 1
            NFINDOC.FULLYTRANSF = 0
            NFINDOC.LTYPE1 = 1
            NFINDOC.LTYPE2 = 0
            NFINDOC.LTYPE3 = 0
            NFINDOC.LTYPE4 = 0
            NFINDOC.TURNOVR = 0
            NFINDOC.TTURNOVR = 0
            NFINDOC.LTURNOVR = 0
            NFINDOC.VATAMNT = 0
            NFINDOC.TVATAMNT = 0
            NFINDOC.LVATAMNT = 0
            NFINDOC.EXPN = 0
            NFINDOC.TEXPN = 0
            NFINDOC.LEXPN = 0
            NFINDOC.DISC1PRC = 0
            NFINDOC.DISC1VAL = 0
            NFINDOC.TDISC1VAL = 0
            NFINDOC.LDISC1VAL = 0
            NFINDOC.DISC2PRC = 0
            NFINDOC.DISC2VAL = 0
            NFINDOC.TDISC2VAL = 0
            NFINDOC.LDISC2VAL = 0
            NFINDOC.NETAMNT = 0
            NFINDOC.TNETAMNT = 0
            NFINDOC.LNETAMNT = 0
            NFINDOC.SUMAMNT = 0
            NFINDOC.SUMTAMNT = 0
            NFINDOC.SUMLAMNT = 0
            NFINDOC.FXDIFFVAL = 0
            NFINDOC.KEPYOQT = 0
            NFINDOC.LKEPYOVAL = 0
            NFINDOC.CHANGEVAL = 0
            NFINDOC.ISTRIG = 0
            NFINDOC.KEPYOMD = 0
        Catch ex As Exception

        End Try
        Return NFINDOC
        'Throw New NotImplementedException
    End Function
    Private Sub MTRLINEsBindingSource_AddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles MTRLINEsBindingSource.AddingNew
        Try
            Dim NFINDOC As Hglp.FINDOC = Me.VscsBindingSource.Current
            Dim NMTRLINE As New Hglp.MTRLINE
            Dim MTRLINES As Integer = 0
            If NFINDOC.MTRLINEs.Count > 0 Then
                MTRLINES = NFINDOC.MTRLINEs.Max(Function(f) f.MTRLINES)
            End If
            'Dim LINENUM As Integer = NMTRLINE.MTRLINES
            NMTRLINE.MTRLINES = MTRLINES + 1
            NMTRLINE.LINENUM = NMTRLINE.MTRLINES
            NMTRLINE = ZeroMTRLINE(NMTRLINE, NFINDOC)

            'NMTRLINE.COMPANY = NFINDOC.COMPANY
            'NMTRLINE.FINDOC = NFINDOC.FINDOC
            'NMTRLINE.MTRLINES = NFINDOC.MTRLINEs.Count + 1
            'NMTRLINE.LINENUM = NMTRLINE.MTRLINES
            'NMTRLINE.SODTYPE = NFINDOC.SODTYPE
            'NMTRLINE.MTRL = 0
            'NMTRLINE.PENDING = 0
            'NMTRLINE.SOSOURCE = NFINDOC.SOSOURCE
            'NMTRLINE.SOREDIR = 0
            'NMTRLINE.MTRTYPE = 0
            'NMTRLINE.SOTYPE = 0
            'NMTRLINE.VAT = 0
            'NMTRLINE.QTY1 = 0
            'NMTRLINE.QTY2 = 0
            'NMTRLINE.QTY1COV = 0
            'NMTRLINE.QTY1CANC = 0
            'NMTRLINE.QTY1FCOV = 0
            'NMTRLINE.LEXPVAL = 0
            'NMTRLINE.NETLINEVAL = 0
            'NMTRLINE.LNETLINEVAL = 0
            'NMTRLINE.VATAMNT = 0
            'NMTRLINE.LVATAMNT = 0
            'NMTRLINE.EFKVAL = 0
            'NMTRLINE.AUTOPRDDOC = 0
            'NMTRLINE.DELIVDATE = NFINDOC.TRNDATE
            NFINDOC.MTRLINEs.Add(NMTRLINE)
            e.NewObject = NMTRLINE
        Catch ex As Exception

        End Try
    End Sub
    Private Function ZeroMTRLINE(NMTRLINE As Hglp.MTRLINE, NFINDOC As Hglp.FINDOC) As Hglp.MTRLINE
        Try
            'IS NULL
            'COMPANY,FINDOC,MTRLINES,LINENUM,SODTYPE,MTRL,PENDING,SOSOURCE,SOREDIR,MTRTYPE,SOTYPE,VAT,
            'QTY1, QTY2, QTY1COV, QTY1CANC, QTY1FCOV, LEXPVAL, NETLINEVAL, LNETLINEVAL, VATAMNT, LVATAMNT, EFKVAL, AUTOPRDDOC
            NMTRLINE.COMPANY = NFINDOC.COMPANY
            NMTRLINE.FINDOC = NFINDOC.FINDOC
            'NMTRLINE.MTRLINES = MTRLINES
            'NMTRLINE.LINENUM = LINENUM
            NMTRLINE.SODTYPE = NFINDOC.SODTYPE
            NMTRLINE.MTRL = 0
            NMTRLINE.PENDING = 0
            NMTRLINE.SOSOURCE = NFINDOC.SOSOURCE
            NMTRLINE.SOREDIR = 0
            NMTRLINE.MTRTYPE = 0
            NMTRLINE.SOTYPE = 0
            NMTRLINE.VAT = 0 'Not Null
            NMTRLINE.QTY1 = 0
            NMTRLINE.QTY2 = 0
            NMTRLINE.QTY1COV = 0
            NMTRLINE.QTY1CANC = 0
            NMTRLINE.QTY1FCOV = 0
            NMTRLINE.LEXPVAL = 0
            NMTRLINE.NETLINEVAL = 0
            NMTRLINE.LNETLINEVAL = 0
            NMTRLINE.VATAMNT = 0
            NMTRLINE.LVATAMNT = 0
            NMTRLINE.EFKVAL = 0
            NMTRLINE.AUTOPRDDOC = 0
            NMTRLINE.DELIVDATE = NFINDOC.TRNDATE
        Catch ex As Exception

        End Try
        Return NMTRLINE
        'Throw New NotImplementedException
    End Function

    Private Sub ExcelToolStripButton_Click(sender As Object, e As EventArgs) Handles ExcelToolStripButton.Click
        'Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True
        Dim filename As String = Me.Text

        saveFileDialog1.FileName = filename & " " & Today().ToShortDateString.Replace("/", "-")
        If saveFileDialog1.ShowDialog() = DialogResult.OK Then

            ExportDataToExcel(saveFileDialog1.FileName, Me.Text)

            'myStream = saveFileDialog1.OpenFile()
            'If (myStream IsNot Nothing) Then
            '    ' Code to write the stream goes here.
            '    myStream.Close()
            'End If
        End If
    End Sub
    ''' <summary>
    ''' Creates the data table.
    ''' </summary>
    ''' <returns>DataTable</returns>
    Private Shared Function CreateDataTable() As DataTable
        Dim dt As New DataTable()
        For i As Integer = 0 To 9
            dt.Columns.Add(i.ToString())
        Next

        For i As Integer = 0 To 9
            Dim dr As DataRow = dt.NewRow()
            For Each dc As DataColumn In dt.Columns
                dr(dc.ToString()) = i
            Next

            dt.Rows.Add(dr)
        Next
        Return dt
    End Function
    Private Sub ExportDataToExcel(fileName As String, title As String)

        Using p As New ExcelPackage()
            Try

                'Here setting some document properties
                p.Workbook.Properties.Author = "GmLogic"
                p.Workbook.Properties.Title = fileName '"ExprotData"

                'Create a sheet
                p.Workbook.Worksheets.Add(title)

                For Each ws As ExcelWorksheet In p.Workbook.Worksheets
                    Dim lst = Me.StatBindingSource.DataSource
                    'Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Εκκρεμείς παραγγελίες")
                    'ws.Name = "Sample Worksheet"
                    'Setting Sheet's name

                    ws.Cells.Style.Font.Size = 11
                    'Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"
                    'Default Font name for whole sheet
                    'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
                    'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
                    'dt = Utility.LINQToDataTable(db, q)
                    'My Function which generates DataTable
                    'Merging cells and create a center heading for out table
                    'ws.Cells(1, 1).Value = "Sample DataTable Export"
                    Dim colIndex As Integer = 1
                    Dim rowIndex As Integer = 1

                    'ws.Cells(rowIndex, colIndex, rowIndex, 15).Merge = True
                    'ws.Cells(rowIndex, colIndex).Value = "ΛΝΚ Α.Ε"
                    ws.Row(rowIndex).Style.Font.Size = 14
                    'Creating Headings
                    'Dim cellprH = ws.Cells(rowIndex - 1, colIndex)
                    Dim cellH = ws.Cells(rowIndex, colIndex)
                    'Setting the background color of header cells to Gray
                    'Dim fillprH = cellprH.Style.Fill
                    Dim fillH = cellH.Style.Fill
                    'fillprH.PatternType = ExcelFillStyle.Solid
                    fillH.PatternType = ExcelFillStyle.Solid
                    'fillprH.BackgroundColor.SetColor(System.Drawing.Color.White)

                    fillH.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

                    'Setting Top/left,right/bottom borders.
                    Dim borderH = cellH.Style.Border
                    borderH.Bottom.Style = InlineAssignHelper(borderH.Top.Style, InlineAssignHelper(borderH.Left.Style, InlineAssignHelper(borderH.Right.Style, ExcelBorderStyle.Thin)))

                    'Setting Value in cell
                    cellH.Value = Me.Text

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                    ws.Row(rowIndex).Height = 35.25
                    ws.Cells(rowIndex, colIndex, rowIndex, myArrN.Length).Merge = True
                    cellH.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                    rowIndex += 1
                    colIndex = 1
                    For Each col In myArrN
                        'Creating Headings
                        Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                        Dim cell = ws.Cells(rowIndex, colIndex)
                        'Setting the background color of header cells to Gray
                        Dim fillpr = cellpr.Style.Fill
                        Dim fill = cell.Style.Fill
                        fillpr.PatternType = ExcelFillStyle.Solid
                        fill.PatternType = ExcelFillStyle.Solid
                        fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)

                        fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

                        'Setting Top/left,right/bottom borders.
                        Dim border = cell.Style.Border
                        border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                        'Setting Value in cell
                        cell.Value = col
                        'cell.AutoFitColumns()
                        colIndex += 1
                    Next
                    'ws.Column(ColNLst.Count).Width = 44.57

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center

                    Dim rowDataIndex = rowIndex + 1

                    If Me.radioBtnSales.Checked Then
                        lst = CType(lst, DataTable).AsEnumerable
                    End If

                    For Each ro In lst ' DataGridViewRow In Me.MasterDataGridView.Rows
                        ' Adding Data into rows
                        colIndex = 1
                        rowIndex += 1

                        For Each col_Name In myArrF 'As  DataGridViewColumn In Me.MasterDataGridView.Columns
                            If col_Name = "Check" Then
                                Continue For
                            End If
                            Dim cell = ws.Cells(rowIndex, colIndex)
                            Try
                                If Not Me.radioBtnSales.Checked Then
                                    cell.Value = ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                                Else
                                    cell.Value = ro(col_Name)
                                End If

                                'Setting Value in cell
                                With cell
                                    Dim t As Type
                                    If Not Me.radioBtnSales.Checked Then
                                        t = ro.GetType().GetProperty(col_Name).PropertyType 'col.ValueType
                                    Else
                                        t = ro(col_Name).GetType
                                    End If

                                    If Not IsNothing(t) Then
                                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                                .Value = CType(cell.Value, Double)
                                                .Style.Numberformat.Format = "#,##0.00"
                                            End If
                                            If Not t.FullName.IndexOf("System.DateTime") Then
                                                If {"INSDATE", "TRNDATE"}.Contains(col_Name) Then
                                                    .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                                Else
                                                    .Style.Numberformat.Format = "dd/MM/yyyy"
                                                End If
                                            End If
                                        End If

                                        If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                            .Value = CType(cell.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.00"
                                        End If
                                        If t.Name = "DateTime" Then
                                            '.Value = CType(ro.Cells(col.Name).Value, Double)
                                            If col_Name = "INSDATE" Then
                                                .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                            Else
                                                .Style.Numberformat.Format = "dd/MM/yyyy"
                                            End If

                                        End If
                                    End If
                                    If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                        If Not t.FullName.IndexOf("System.Decimal") Then
                                            .Value = CType(cell.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.00"
                                        End If
                                    End If
                                    If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                        .Value = CType(cell.Value, Double)
                                        .Style.Numberformat.Format = "#,##0.00"
                                    End If
                                End With
                            Catch ex As Exception
                                MsgBox("Error col_Name:" & col_Name & vbCrLf & ex.Message)
                            End Try


                            'Setting borders of cell
                            Dim border = cell.Style.Border
                            border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                            'ws.Cells(rowIndex, colIndex).AutoFitColumns()
                            colIndex += 1
                        Next
                    Next

                    rowIndex += 1
                    Dim cellFoot = ws.Cells(rowIndex, 1)
                    cellFoot.Value = "Σύνολα"
                    cellFoot.Style.Fill.PatternType = ExcelFillStyle.Solid
                    cellFoot.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206))
                    Dim startCol = 1
                    Dim endCol = myArrF.Count
                    'Dim criteria = ws.Cells(rowIndex, 2)
                    'criteria.Value = "<0"
                    For i As Integer = startCol + 1 To endCol ' ColNLst.Count
                        Dim cellsum = ws.Cells(rowIndex, i)
                        Dim cellsumpr = ws.Cells(rowIndex - 1, i)
                        If Not cellsumpr.Style.Numberformat.Format = "#,##0.00" Then
                            Continue For
                        End If

                        With cellsum
                            'Setting Sum Formula 
                            '.Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                            '=SUMIF(F2:F9;">0";F2:F9)

                            'Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & ws.Cells(rowIndex, 2).Address & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'Spoiler: The solution is to use "," instead of ";" when working with formulas in your code.
                            Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<>0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            If Me.radioBtnΑggregate.Checked Then
                                formula = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            End If
                            .Formula = formula '("SUMIF(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ";" & Chr(34) & "<0" & Chr(34) & ";" & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            .Style.Numberformat.Format = "#,##0.00"

                            'Setting Background fill color to Gray
                            .Style.Fill.PatternType = ExcelFillStyle.Solid
                            .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                        End With
                    Next
                    ws.Cells(ws.Dimension.Address).AutoFitColumns()

                Next

                'Generate A File with Random name
                Dim bin As [Byte]() = p.GetAsByteArray()
                Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
                Try
                    File.WriteAllBytes(file__1, bin)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub
    Private Sub ExportToExcelold(fileName As String)
        Using p As New ExcelPackage()
            'Here setting some document properties
            p.Workbook.Properties.Author = "GmLogic"
            p.Workbook.Properties.Title = Me.Text ' "Υπόλοιπα ομάδων"

            'Create a sheet
            p.Workbook.Worksheets.Add("Sheet1")
            Dim ws As ExcelWorksheet = p.Workbook.Worksheets(1)
            'ws.Name = "Sample Worksheet"
            'Setting Sheet's name
            ws.Cells.Style.Font.Size = 11
            'Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"
            'Default Font name for whole sheet

            'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
            'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
            'dt = Utility.LINQToDataTable(db, q)
            'My Function which generates DataTable
            'Merging cells and create a center heading for out table
            'ws.Cells(1, 1).Value = "Sample DataTable Export"
            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            ws.Cells(rowIndex, 2, 1, 3).Merge = True
            ws.Cells(rowIndex, 2).Value = Me.Text & " " & Me.DateTimePicker2.Value.ToShortDateString
            ws.Row(rowIndex).Style.Font.Size = 14
            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
            ws.Row(rowIndex).Height = 35.25
            ws.Column(2).Width = 44.57

            Dim ColNLst As New List(Of String)
            For Each col As DataGridViewColumn In Me.StatDataGridView.Columns
                If col.Name = "Check" Then
                    Continue For
                End If
                ColNLst.Add(col.Name)
            Next

            rowIndex += 1
            For Each col In ColNLst
                'Creating Headings
                Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                Dim cell = ws.Cells(rowIndex, colIndex)
                'Setting the background color of header cells to Gray
                Dim fillpr = cellpr.Style.Fill
                Dim fill = cell.Style.Fill
                fillpr.PatternType = ExcelFillStyle.Solid
                fill.PatternType = ExcelFillStyle.Solid
                fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)
                fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                'Setting Top/left,right/bottom borders.
                Dim border = cell.Style.Border
                border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                'Setting Value in cell
                cell.Value = col

                colIndex += 1
            Next

            'ws.Column(ColNLst.Count).Width = 44.57

            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center


            For Each ro As DataGridViewRow In Me.StatDataGridView.Rows
                ' Adding Data into rows
                colIndex = 1
                rowIndex += 1

                For Each col As DataGridViewColumn In Me.StatDataGridView.Columns
                    If col.Name = "Check" Then
                        Continue For
                    End If
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    cell.Value = ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                    'Setting Value in cell
                    Try
                        With cell
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .Value = CType(ro.Cells(col.Name).Value, Double)
                                        .Style.Numberformat.Format = "#,##0.00"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.00"
                                End If
                                '    If col.ValueType.Name = "String" Then
                                '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                '        '.Width = 200
                                '    End If
                                '    If col.ValueType.Name <> "String" Then
                                '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                '    End If
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    'Setting borders of cell
                    Dim border = cell.Style.Border
                    border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                    colIndex += 1
                Next

            Next
            For i As Integer = 5 To ColNLst.Count
                Dim cellsum = ws.Cells(rowIndex + 1, i)
                With cellsum
                    'Setting Sum Formula
                    .Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex, i).Address & ")"
                    .Style.Numberformat.Format = "#,##0.00"

                    'Setting Background fill color to Gray
                    .Style.Fill.PatternType = ExcelFillStyle.Solid
                    .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End With
            Next



            'Generate A File with Random name
            Dim bin As [Byte]() = p.GetAsByteArray()
            Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
            Try
                File.WriteAllBytes(file__1, bin)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub
    Private Sub ExportToExcel1(fileName As String)
        'Using p As New ExcelPackage()
        '    'Here setting some document properties
        '    p.Workbook.Properties.Author = "Zeeshan Umar"
        '    p.Workbook.Properties.Title = "Office Open XML Sample"

        '    'Create a sheet
        '    p.Workbook.Worksheets.Add("Sample WorkSheet")
        '    Dim ws As ExcelWorksheet = p.Workbook.Worksheets(1)
        '    ws.Name = "Sample Worksheet"
        '    'Setting Sheet's name
        '    ws.Cells.Style.Font.Size = 11
        '    'Default font size for whole sheet
        '    ws.Cells.Style.Font.Name = "Calibri"
        '    'Default Font name for whole sheet

        '    Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
        '    Dim q As IQueryable(Of whRpt) = CType(Me.StatBindingSource.DataSource, List(Of whRpt)).AsQueryable
        '    dt = Utility.LINQToDataTable(db, q)
        '    'My Function which generates DataTable
        '    'Merging cells and create a center heading for out table
        '    ws.Cells(1, 1).Value = "Sample DataTable Export"
        '    ws.Cells(1, 1, 1, dt.Columns.Count).Merge = True
        '    ws.Cells(1, 1, 1, dt.Columns.Count).Style.Font.Bold = True
        '    ws.Cells(1, 1, 1, dt.Columns.Count).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

        '    Dim colIndex As Integer = 1
        '    Dim rowIndex As Integer = 2

        '    For Each dc As DataColumn In dt.Columns
        '        'Creating Headings
        '        Dim cell = ws.Cells(rowIndex, colIndex)

        '        'Setting the background color of header cells to Gray
        '        Dim fill = cell.Style.Fill
        '        fill.PatternType = ExcelFillStyle.Solid
        '        fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)


        '        'Setting Top/left,right/bottom borders.
        '        Dim border = cell.Style.Border
        '        border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

        '        'Setting Value in cell
        '        cell.Value = "Heading " + dc.ColumnName

        '        colIndex += 1
        '    Next

        '    For Each dr As DataRow In dt.Rows
        '        ' Adding Data into rows
        '        colIndex = 1
        '        rowIndex += 1
        '        For Each dc As DataColumn In dt.Columns
        '            Dim cell = ws.Cells(rowIndex, colIndex)
        '            'Setting Value in cell
        '            cell.Value = Convert.ToInt32(dr(dc.ColumnName)) 'dr(dc.ColumnName) 

        '            'Setting borders of cell
        '            Dim border = cell.Style.Border
        '            border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
        '            colIndex += 1
        '        Next
        '    Next

        '    colIndex = 0
        '    For Each dc As DataColumn In dt.Columns
        '        'Creating Headings
        '        colIndex += 1
        '        Dim cell = ws.Cells(rowIndex, colIndex)

        '        'Setting Sum Formula
        '        cell.Formula = ("Sum(" + ws.Cells(3, colIndex).Address & ":") + ws.Cells(rowIndex - 1, colIndex).Address & ")"

        '        'Setting Background fill color to Gray
        '        cell.Style.Fill.PatternType = ExcelFillStyle.Solid
        '        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
        '    Next

        '    'Generate A File with Random name
        '    Dim bin As [Byte]() = p.GetAsByteArray()
        '    Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
        '    File.WriteAllBytes(file__1, bin)
        'End Using
    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function

    Private Sub txtFINCODE_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFINCODE.KeyDown
        Console.WriteLine("KeyValue: " & e.KeyValue.ToString & "    KeyCode: " & e.KeyCode.ToString & " KeyData: " & e.KeyData.ToString)

        If e.Shift And e.KeyCode = Keys.D8 Then
            MessageBox.Show(sender.Text)
        End If
    End Sub

    'Private Sub StatDataGridView_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StatDataGridView.CellMouseDown
    '    Dim s As DataGridView = sender
    '    CellSum = 0
    '    If s.Columns(e.ColumnIndex).DataPropertyName = "Price" Then
    '        For Each ce As DataGridViewCell In s.SelectedCells
    '            If ce.ColumnIndex = e.ColumnIndex Then
    '                CellSum += ce.Value
    '            End If

    '        Next
    '    End If
    '    'Me.lblV01.Text = CellSum
    'End Sub

    'Private Sub StatDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles StatDataGridView.CellValidating
    '    Dim s As DataGridView = sender
    '    CellSum = 0
    '    If s.Columns(e.ColumnIndex).DataPropertyName = "Price" Then
    '        For Each ce As DataGridViewCell In s.SelectedCells
    '            'CellSum += ce.Value
    '        Next
    '    End If
    '    'Me.lblV01.Text = CellSum
    'End Sub

    'Private Sub StatDataGridView_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles StatDataGridView.CellValidated
    '    Dim s As DataGridView = sender
    '    CellSum = 0
    '    If s.Columns(e.ColumnIndex).DataPropertyName = "Price" Then
    '        For Each ce As DataGridViewCell In s.SelectedCells
    '            If ce.ColumnIndex = e.ColumnIndex Then
    '                CellSum += ce.Value
    '            End If

    '        Next
    '    End If
    '    'Me.lblV01.Text = CellSum
    'End Sub

    Private Sub StatDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles StatDataGridView.SelectionChanged
        Dim s As DataGridView = sender
        CellSum = 0
        If s.SelectedCells.Count > 0 Then
            For Each ce As DataGridViewCell In s.SelectedCells
                If s.Columns(ce.ColumnIndex).DataPropertyName = "Price" Then
                    CellSum += ce.Value
                End If
            Next
        End If
        'For i As Integer = 0 To dataGridView1.SelectedCells.Count - 1

        '    If Not dataGridView1.SelectedCells.Contains(dataGridView1.Rows(i).Cells("cLoadName")) Then
        '        Dim nextNumber As Single = 0
        '        Dim sumNumbers As Single = 0
        '        If Single.TryParse(dataGridView1.SelectedCells(i).FormattedValue.ToString(), nextNumber) Then sumNumbers += nextNumber
        '        tsslSumSelected.Text = "ჯამი: " & sumNumbers
        '        tsslTotalSelected.Text = "მონიშნული: " & dataGridView1.SelectedCells.Count.ToString()
        '    Else
        '    End If
        'Next
        Me.lblV01.Text = String.Format("{0:N2}", CellSum)
    End Sub

#End Region

End Class
'Commands για αντιστοιχίσεις:
'-------------------------------------------

'Παραστατικά που η κίνηση του συναλλασσόμενου γίνεται μόνο από το
'Header
'(π.χ. Πωλήσεις, Αγορές, Εισπράξεις, Πληρωμές, Ειδικές συναλλαγές)
'-----------------------------------------------------------------------
'Βήματα:
'1) locate στο παραστατικό
'2) εκτέλεση command
'3) post το παραστατικό

'Commands:
'-------------------------------------------------------------------------------------------------------------
'1032: FiFo αντιστοίχιση(Πλήρης).
'      Δίνεται String με τα ID των παραστατικών χωρισμένα με κόμμα.
'      Τα παραστατικά κλείνονται FiFo,
'      μέχρι να καλυφθεί το ποσό του παραστατικού που αντιστοιχίζεται.
'      π.χ. 'FINDOC1,FINDOC2,FINDOC3'


'Π.χ.
'x = CallPublished('ModuleIntf.LocateModule',VarArray(vModule,fFindocNew,2));
'x = CallPublished('ProgLibIntf.ModuleCommand',VarArray(vModule,1032,'FINDOC1,FINDOC2,FINDOC3',3));
'x = CallPublished('ModuleIntf.PostModule',vModule);
'-------------------------------------------------------------------------------------------------------------
'1033: Διαγραφή των αντιστοιχίσεων του παραστατικού.
'-------------------------------------------------------------------------------------------------------------

'1034: Μερική αντιστοίχιση.
'      Δίνεται String με τα ID των γραμμών του πίνακα FINPAYTERMS, μαζί
'με την προς αντιστοίχιση αξία.
'      π.χ. 'FINPAYTERMS1;100,FINPAYTERMS2;300.15,FINPAYTERMS3;400'

'Π.χ.
'x = CallPublished('ModuleIntf.LocateModule',VarArray(vModule,fFindocNew,2));
'x = CallPublished('ProgLibIntf.ModuleCommand',VarArray(vModule,1034,'FINPAYTERMS1;100,FINPAYTERMS2;300.15,FINPAYTERMS3;400',3));
'x = CallPublished('ModuleIntf.PostModule',vModule);
'-------------------------------------------------------------------------------------------------------------

'Παραστατικά που η κίνηση του συναλλασσόμενου γίνεται από τις γραμμές
'(π.χ. Εμβάσματα, Συμψηφισμοί, Ταμειακά παραστατικά)
'--------------------------------------------------------------------
'Βήματα:
'1) locate στο παραστατικό
'2) locate στη γραμμή
'3) εκτέλεση command
'4) post το παραστατικό

'Commands:
'----------------------------------------------------------------------------------------------
'Γραμμές: TRDTLINES(Εμβάσματα, Συμψηφισμοί)
'----------------------------------------------------------------------------------------------
'1035: Δίνεται String με τα ID των γραμμών του πίνακα FINPAYTERMS, μαζί
'με την προς αντιστοίχιση αξία.
'      π.χ. 'FINPAYTERMS1;100,FINPAYTERMS2;300.15,FINPAYTERMS3;400'

'----------------------------------------------------------------------------------------------
'1036: Διαγραφή των αντιστοιχίσεων του παραστατικού. (Απαιτείται και
'εδώ locate σε κάθε γραμμή) (ΠΡΟΣΟΧΗ ΜΟΝΟ TRDTLINES)
'----------------------------------------------------------------------------------------------

'Γραμμές: TRDFLINES(Ταμειακά Παραστατικά)
'----------------------------------------------------------------------------------------------
'1037: Δίνεται String με τα ID των παραστατικών χωρισμένα με κόμμα.
'----------------------------------------------------------------------------------------------

'*Αν απαιτείται αντιστοίχιση και για την κίνηση της γραμμής αλλά και
'για την κίνηση του header
' θα πρέπει να εκτελεστούν και τα 2 command (π.χ.: 1034 για το header και 1035 για τις γραμμές.