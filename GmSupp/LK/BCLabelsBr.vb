Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Transactions
Imports GmSupp
Imports GmSupp.Hglp
Imports GmSupp.Revera
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports QRCoder

Public Class BCLabelsBr
#Region "01-Declare Variables"
    Dim db As New DataClassesHglpDataContext
    'Dim dbRevera As New DataClassesReveraDataContext

    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    Dim conn As String
    Dim addVsc As New Revera.GetPendingOrdersDetailsResult
    'Dim CompanyT As Integer = 0
    Dim CompanyS As Integer = 0
    Dim detsOld As New List(Of Revera.GetPendingOrdersDetailsResult)
    Private Applicants As List(Of UFTBL01)
    Private FromDep As List(Of UFTBL02)
    Private Trdrs As List(Of Revera.TRDR)
    Private cccTrdDeps As List(Of cccTrdDep)
    Private Suppliers As List(Of Revera.TRDR)
    Private ccCChief As List(Of UFTBL01)
    Private ccCManager As List(Of UFTBL01)
    Private wfm As New WHouseBalFR
    Private Αpprovs As String
    Dim editableFields_MasterDataGridView As New List(Of String)
    Dim Shifts As String() = (",A,B,C").Split(",")
    Dim WithEvents PDoc As New System.Drawing.Printing.PrintDocument
    Private btnQRName As String
#End Region
#Region "02-Declare Propertys"
    Public Property Series As Integer
        Get
            Return m_Series
        End Get
        Set(ByVal value As Integer)
            m_Series = value
        End Set
    End Property

#End Region
#Region "03-Load Form"
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
        DateTimePicker1.Value = CDate("01/01/" & Year(CTODate))
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        If Me.Tag = "REVERA" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Revera"
            CompanyS = 4000
        End If
        'If Me.Tag = "CENTROFARO" Then
        '    conString.DataSource = "192.168.12.201,55555"
        '    conString.InitialCatalog = "Centro"
        '    CompanyS = 3000
        'End If

        If Me.Tag = "Hglp" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Hglp"
            CompanyS = 1000
        End If



        'conString.UserID = "sa"
        'conString.Password = "P@$$w0rd"
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID

        'If CurUser = "g.igglesis" Then
        '    CompanyT = 1002
        'End If

        'If {"panagiotis", "katerina", "gkonstantatos"}.Contains(CurUser) Then
        '    CompanyT = 2001 '1001
        'End If

        'SetGmChkListBox()

        Me.Panel1.Visible = False
        Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width
        Me.SplitContainer2.Panel2.Visible = False
        If Me.Text = "Αποθήκη - Νέα Αίτηση/Υπόλοιπα Ειδών" Then

        End If
        If Me.Text = "Αποθήκη - Εκκρεμείς Παραγγελίες" Then

            'Me.SplitContainer2.Panel2.Visible = False
        End If
        If Me.Text = "Αποθήκη - Αιτήσεις σε Εκρεμότητα" Then

        End If

        If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Or Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then
            Me.txtBoxMtrlCode.Text = "21*"
        End If

        If CurUser = "gmlogic" Then
            conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
            Me.txtBoxMtrlCode.Text = "2103050533*"
            'Select Case conString.InitialCatalog
            '    Case "PFIC"
            '        CompanyT = 1002
            '    Case "LNK"
            '        CompanyT = 1001
            '    Case "LK"
            '        CompanyT = 2001 '1001
            'End Select
            'Me.txtBoxMtrlCode.Text = "1A231F401N3*"
            'Me.txtBoxFinCode.Text = "ΠΑΡ0001593"
            'Me.Panel1.Visible = True
        End If

        editableFields_MasterDataGridView = If(Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode", {"PrdDate", "PackingDate", "Shift", "Machine", "LabelsNo"}, {"LBName", "LBCode", "Pack", "Weight"}).ToList

        Me.KeyPreview = True

    End Sub

    'Private Sub SetGmChkListBox()
    '    Dim emptyApplicant() As Revera.UFTBL01
    '    emptyApplicant = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

    '    Applicants = (emptyApplicant.ToList.Union(dbRevera.UFTBL01s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList

    '    Dim emptyFromDep() As Revera.UFTBL02
    '    emptyFromDep = {New Revera.UFTBL02 With {.NAME = "<Επιλέγξτε>", .UFTBL02 = 0}}

    '    FromDep = (emptyFromDep.ToList.Union(dbRevera.UFTBL02s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList

    '    Dim emptyTrdr() As Revera.TRDR
    '    emptyTrdr = {New Revera.TRDR With {.NAME = "<Επιλέγξτε>", .TRDR = 0}}

    '    dbRevera.Log = Console.Out

    '    'Dim gg = db.TRDRs.Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 13 And f.ISACTIVE = 1 And f.TRDEXTRA.BOOL01 = True And db.cccTrdDeps.
    '    '                                Select(Function(f1) f1.trdr).Contains(f.TRDR)).OrderBy(Function(f) f.NAME).ToList

    '    Trdrs = (emptyTrdr.ToList.Union(dbRevera.TRDRs.
    '                                    Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 13 And f.ISACTIVE = 1 And f.TRDEXTRA.BOOL01 = 1 And dbRevera.cccTrdDeps.
    '                                    Select(Function(f1) f1.trdr).Contains(f.TRDR)).OrderBy(Function(f) f.NAME).ToList)).ToList

    '    Dim emptycccTrdDep() As Revera.cccTrdDep
    '    emptycccTrdDep = {New Revera.cccTrdDep With {.Name = "<Επιλέγξτε>", .cccTrdDep = 0}}

    '    cccTrdDeps = (emptycccTrdDep.ToList.Union(dbRevera.cccTrdDeps.Where(Function(f) Trdrs.
    '                                    Select(Function(f1) f1.TRDR).Contains(f.trdr)).OrderBy(Function(f) f.Name).ToList)).ToList

    '    Dim emptySupplier() As Revera.TRDR
    '    emptySupplier = {New Revera.TRDR With {.NAME = "<Επιλέγξτε>", .TRDR = 0}}

    '    Suppliers = (emptySupplier.ToList.Union(dbRevera.TRDRs.
    '                                    Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 12 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList


    '    Dim emptyccCChief() As Revera.UFTBL01
    '    emptyccCChief = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

    '    ccCChief = emptyApplicant.ToList

    '    Dim emptycCCManager() As Revera.UFTBL01
    '    emptycCCManager = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

    '    ccCManager = emptycCCManager.ToList

    '    'Me.ApplicantNAMEComboBox.DataSource = q1.ToList

    '    Dim Result As Dictionary(Of Short, String) = dbRevera.UFTBL01s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251).OrderBy(Function(f) f.NAME).ToDictionary(Function(f) f.UFTBL01, Function(f) f.NAME)


    '    Debug.Print("Values inserted into dictionary:")
    '    For Each dic As KeyValuePair(Of Short, String) In Result
    '        Debug.Print([String].Format("English salute {0} is {1} in German", dic.Key, dic.Value))
    '    Next

    '    'Me.GmChkListBoxAplicant.Height = 33
    '    Me.GmChkListBoxAplicant.dgv.DataSource = Result.ToList
    '    Me.GmChkListBoxAplicant.dgv_Styling()
    '    'Me.GmChkListBoxAplicant.BringToFront()

    '    Result = dbRevera.FPRMs.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251).ToDictionary(Function(f) f.FPRMS, Function(f) f.NAME)
    '    'Me.GmChkListBoxFprms.Height = 33
    '    Me.GmChkListBoxFprms.dgv.DataSource = Result.ToList
    '    Me.GmChkListBoxFprms.dgv_Styling()
    '    'Me.GmChkListBoxFprms.BringToFront()

    '    Result = dbRevera.RESTMODEs.Where(Function(f) f.COMPANY = CompanyS).ToDictionary(Function(f) f.RESTMODE, Function(f) f.NAME)
    '    'Me.GmChkListBoxRestMode.Height = 33
    '    Me.GmChkListBoxRestMode.dgv.DataSource = Result.ToList
    '    Me.GmChkListBoxRestMode.dgv_Styling()
    '    'Me.GmChkListBoxRestMode.BringToFront()


    '    Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(New List(Of Revera.GetPendingOrdersDetailsResult))
    '    Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
    '    'MTRLINEsDataGridView_Styling()

    '    wfm.DateTimePicker1.Enabled = False
    '    wfm.DateTimePicker1.Value = CTODate
    '    wfm.DateTimePicker2.Value = CTODate
    '    wfm.ddlTrdr.DataSource = Trdrs
    '    wfm.cccTrdDeps = cccTrdDeps
    '    wfm.ddlApplicant.DataSource = Applicants
    '    'wfm.ddlSuppliers.DataSource = Suppliers
    '    wfm.ddlFromcccTrdDep.DataSource = FromDep
    '    'wfm.ddlccCChief.DataSource = ccCChief
    '    'wfm.ddlcCCManager.DataSource = ccCManager
    'End Sub

    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.KeyCode = Keys.F4 Then
            Me.cmdPrint.PerformClick()
        End If
        If e.Alt And e.KeyCode = Keys.S Then
            Me.BindingNavigatorSaveItem.PerformClick()
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
        If Me.MasterBindingSource.Count = 0 Then
            Exit Sub
        End If
        'Try

        '    Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
        '                   Where ce.Cells("Check").Value = True

        '    If chkLists.Count = 0 Then
        '        Exit Sub
        '    End If

        '    'Dim grp = chkLists.GroupBy(Function(f) f.Cells("MTRL").Value)

        '    'If Not Me.chkBoxAuto.Checked AndAlso Not grp.Count = 1 Then
        '    '    MsgBox("Προσοχή !!! Επιλέγξατε διαφορετικά είδη.", MsgBoxStyle.Critical, "AddingNew")
        '    '    Exit Sub
        '    'End If

        '    'Dim chkFi As List(Of Integer) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
        '    '                                 Where ce.Cells("Check").Value = True
        '    '                                 Select CType(ce.Cells("Findoc").Value, Integer)).ToList

        '    Dim chkMt As List(Of String) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
        '                                    Where ce.Cells("Check").Value = True
        '                                    Select CType(ce.Cells(1).Value, String)).ToList

        '    'Dim chkLinesNo As List(Of Integer) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
        '    '                                      Where ce.Cells("Check").Value = True
        '    '                                      Select CType(ce.Cells("LinesNo").Value, Integer)).ToList

        '    Dim lst As New List(Of Revera.GetWHouseBalanceResult)
        '    lst = CType(Me.MasterBindingSource.DataSource, SortableBindingList(Of Revera.GetWHouseBalanceResult)).ToList

        '    Dim mtrls = lst.Where(Function(f) chkMt.Contains(f.CODE)).ToList

        '    For Each v In mtrls
        '        addVsc = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) f.CODE = v.CODE).SingleOrDefault
        '        If IsNothing(addVsc) Then
        '            Dim det = New Revera.GetPendingOrdersDetailsResult
        '            det.MTRL = v.MTRL
        '            det.CODE = v.CODE
        '            det.NAME = v.NAME
        '            det.MTRUNITC = v.MTRUNITC
        '            'UFTBL02, 106
        '            'cccTrdDep, 55
        '            'cccTrdr, 35465
        '            addVsc = det
        '            Me.VscsBindingSource.AddNew()
        '        End If

        '    Next
        '    If Me.MTRLINEsDataGridView.ColumnCount > 4 Then
        '        MTRLINEsDataGridView_Styling()
        '    End If

        '    For Each row As DataGridViewRow In chkLists
        '        'Set colors
        '        row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
        '    Next
        '    Me.TlSBtnUnCheck.PerformClick()

        '    Me.BindingNavigatorSaveItem.Enabled = True
        '    Me.SplitContainer2.Panel2.Visible = True
        '    Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width - (Me.SplitContainer2.Width / 4)

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub
    Private Sub Cmd_Edit()
        Try
            'Exit Sub
            'Try
            '    Me.Cursor = Cursors.WaitCursor
            '    Dim str As String = ""
            '    'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + dgFINDOC.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
            '    Dim drv 'As CCCCheckZip = Me.MasterBindingSource.Current
            '    str = "SALDOC[AUTOLOCATE=" & drv.ZIP & "]"
            '    s1Conn.ExecS1Command(str, fS1HiddenForm)
            '    'FilldgFINDOC_gm(iActiveObjType)
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            'Finally
            '    Me.Cursor = Cursors.Default
            'End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Select()
        Try
            Me.Cursor = Cursors.NoMove2D
            LoadData()
            db.Log = Console.Out

            Dim mTRL As System.Nullable(Of Integer)
            Dim mTRLCODE As String
            Dim nAME As String
            Dim rEMARKS As String
            'Dim cOMPANY As System.Nullable(Of Short)
            'Dim sODTYPE As System.Nullable(Of Short)
            'Dim wHOUSE As String
            'Dim mTRACN As String
            'Dim dFROM As System.Nullable(Of Date)
            'Dim dTO As System.Nullable(Of Date)
            'Dim fISCPRD As System.Nullable(Of Integer)
            'Dim pERIOD As System.Nullable(Of Integer)
            'Dim sOSOURCE As System.Nullable(Of Integer)
            'Dim fPRMS As String
            'Dim tFPRMS As String
            'Dim tRDBUSINESS As String
            'Dim iSCANCEL As System.Nullable(Of Short)
            'Dim fULLYTRANSF As String
            'Dim sLCODE As String
            'Dim tRDRCODE As String
            'Dim sLGROUP As String
            'Dim fINCODE As String
            'Dim aPlicant As String
            'Dim rESTMODE As String

            mTRL = 0 'Nothing
            mTRLCODE = If(Me.txtBoxMtrlCode.Text.Trim = "", "*", Me.txtBoxMtrlCode.Text)
            nAME = If(Me.txtBoxName.Text.Trim = "", Nothing, Me.txtBoxName.Text.Replace("*", "%").Trim)
            rEMARKS = If(Me.txtBoxRemarks.Text.Trim = "", Nothing, Me.txtBoxRemarks.Text.Replace("*", "%").Trim)
            'cOMPANY = CompanyS
            'If Me.radioBtnMtrl.Checked Then
            '    sODTYPE = 51
            'Else
            '    sODTYPE = 52
            '    mTRLCODE = "1%"
            'End If

            ''wHOUSE = Me.TlSTxtWHOUSE.Text
            'mTRACN = "101,102,103,105" 'Λογιστική κατηγορία
            'dFROM = Me.DateTimePicker1.Value.ToShortDateString
            'dTO = Me.DateTimePicker2.Value.ToShortDateString
            'fISCPRD = Me.DateTimePicker1.Value.Year
            'pERIOD = Me.DateTimePicker1.Value.Month
            ''sOSOURCE =1251' 1351
            ''fPRMS = Me.TlSTxtFPRMS.Text
            'tFPRMS = Me.txtBoxMtrlCode.Text
            ''tRDBUSINESS = Me.TlSTxtΤRDBUSINES.Text
            'iSCANCEL = 0
            ''fULLYTRANSF = Me.TlSTxtFULLYTRANSF.Text
            ''sLCODE = Me.TlSTxtPRSN.Text
            ''tRDRCODE = Me.TlSTxtTRDR.Text.Replace("*", "%").Trim
            'sLGROUP = ""
            'fINCODE = If(Me.txtBoxFinCode.Text.Trim = "", Nothing, Me.txtBoxFinCode.Text.Replace("*", "%").Trim)


            'fPRMS = If(Me.GmChkListBoxFprms.TlStxtBox.Text.Trim = "", Nothing, Me.GmChkListBoxFprms.TlStxtBox.Text)
            'rESTMODE = If(Me.GmChkListBoxRestMode.TlStxtBox.Text.Trim = "", Nothing, Me.GmChkListBoxRestMode.TlStxtBox.Text)
            'aPlicant = If(Me.GmChkListBoxAplicant.TlStxtBox.Text.Trim = "", Nothing, Me.GmChkListBoxAplicant.TlStxtBox.Text)

            'If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
            '    'Dim res As New List(Of Revera.GetWHouseBalanceResult)
            '    Dim Res = dbRevera.GetWHouseBalance(cOMPANY, sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList
            '    Me.MasterBindingSource.DataSource = New SortableBindingList(Of Revera.GetWHouseBalanceResult)(Res)
            'End If
            'If Me.Text = "Αποθήκη - Εκκρεμείς Παραγγελίες" Then

            '    Dim res As IMultipleResults = dbRevera.GetPendingOrders(cOMPANY, mTRLCODE, 0, 0, 0, fINCODE, fPRMS, rESTMODE, aPlicant, dFROM, dTO)
            '    Dim POrdHead = res.GetResult(Of Revera.GetPendingOrdersHeaderResult).ToList
            '    Dim POrdDet = res.GetResult(Of Revera.GetPendingOrdersDetailsResult).ToList

            '    'Dim Applicant As Short?
            '    'Applicant = Me.ApplicantNAMEComboBox.SelectedValue
            '    'If Not Applicant = 0 Then
            '    '    POrdHead = POrdHead.Where(Function(f) If(f.Applicant, 0) = If(Applicant, 0)).ToList
            '    'End If

            '    If Not Me.txtBoxMtrlCode.Text.Trim = "" Then
            '        POrdDet = POrdDet.Where(Function(f) f.CODE Like mTRLCODE.Replace("%", "*")).ToList
            '    End If


            '    Dim POrds As New List(Of Revera.GetPendingOrdersDetailsResult)
            '    POrds = (From hd In POrdHead Join dt In POrdDet On hd.NO_ Equals dt.NO_
            '             Select New Revera.GetPendingOrdersDetailsResult With {.NO_ = hd.NO_,
            '                .TRNDATE = hd.TRNDATE, .FINCODE = hd.FINCODE, .ApplicantNAME = hd.ApplicantNAME, .RequestNo = hd.OrderNo,
            '                .INSUSERNAME = hd.INSUSERNAME, .FPRMSNAME = hd.FPRMSNAME, .TRDRCODE = hd.CODE,
            '                .TRDRNAME = hd.NAME,
            '                .CODE = dt.CODE, .NAME = dt.NAME, .cccTrdr = dt.cccTrdr, .cccTrdDep = dt.cccTrdDep, .UFTBL02 = dt.UFTBL02,
            '                .QTY1 = dt.QTY1, .QTY1CANC = dt.QTY1CANC, .QTY1OPEN = dt.QTY1OPEN}
            '                ).ToList

            '    Me.MasterBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(POrds)
            'End If


            'If Me.Text = "Αποθήκη - Αιτήσεις σε Εκρεμότητα" Then
            '    'Dim res As New List(Of Revera.GetWHouseBalanceResult)
            '    'Dim Res = db.GetWHouseBalance(cOMPANY, sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList
            '    Dim Res = dbRevera.ccCVMtrLines.Where(Function(f) f.COMPANY = cOMPANY And f.SOSOURCE = 1251 And f.SERIES = 1000 And f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value).ToList ' And ., sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList
            '    If Not Αpprovs = "<Επιλέγξτε>" Then
            '        Res = Res.Where(Function(f) f.VARCHAR02.Contains(Αpprovs)).ToList
            '    End If
            '    Me.MasterBindingSource.DataSource = New SortableBindingList(Of Revera.ccCVMtrLine)(Res)
            'End If


            If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Then
                Dim mtrls = db.MTRLs.Where(Function(f) f.ccCCode IsNot Nothing)
                mtrls = mtrls.Where(Function(f) f.CODE Like mTRLCODE)
                Dim lbs As New List(Of BCLabel)
                For Each ml In mtrls
                    Dim lb As New BCLabel
                    lb.mtrl = ml.MTRL
                    lb.code = ml.CODE
                    lb.Name = ml.NAME
                    'Dim dd As New ccCDescr
                    'dd.LBName = "ΑΝ 33,5"
                    'dd.Pack = 10
                    'dd.Weight = 10
                    'Dim gg = Newtonsoft.Json.JsonConvert.SerializeObject(dd)

                    If Not IsNothing(ml.ccCDescr) Then
                        Dim Descr = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ccCDescr)(ml.ccCDescr)

                        lb.LBName = Descr.LBName
                        lb.LBCode = Descr.LBCode
                        lb.Pack = Descr.Pack
                        lb.Weight = Descr.Weight
                    End If
                    lb.PackingDate = CTODate

                    lbs.Add(lb)
                Next
                'lbs(1).Shift = 0
                'lbs(2).Shift = 1
                'lbs(3).Shift = 2
                'lbs(4).Shift = 3

                Me.MasterBindingSource.DataSource = New SortableBindingList(Of BCLabel)(lbs)
            End If

            If Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then

                Dim mtrls = db.MTRLs.Where(Function(f) f.ccCCode IsNot Nothing)
                mtrls = mtrls.Where(Function(f) f.CODE Like mTRLCODE)
                Dim lbs As New List(Of BCLabel)
                For Each ml In mtrls
                    Dim lb As New BCLabel
                    lb.mtrl = ml.MTRL
                    lb.code = ml.CODE
                    lb.Name = ml.NAME
                    'Dim dd As New ccCDescr
                    'dd.LBName = "ΑΝ 33,5"
                    'dd.Pack = 10
                    'dd.Weight = 10
                    'Dim gg = Newtonsoft.Json.JsonConvert.SerializeObject(dd)

                    If Not IsNothing(ml.ccCDescr) Then
                        Dim Descr = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ccCDescr)(ml.ccCDescr)

                        lb.LBName = Descr.LBName
                        lb.LBCode = Descr.LBCode
                        lb.Pack = Descr.Pack
                        lb.Weight = Descr.Weight
                    End If


                    lbs.Add(lb)
                Next

                Me.MasterBindingSource.DataSource = New SortableBindingList(Of BCLabel)(lbs)
            End If



            Me.MasterDataGridView.DataSource = Me.MasterBindingSource

            MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)

        End Try
        Me.Cursor = Cursors.Default
    End Sub


#End Region
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        Me.MasterBindingSource.EndEdit()
    End Sub
    Private Function DataSafe() As Boolean
        DataSafe = True
        ' Finish any current edits.
        EndAllEdits()

        If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function

        ' Ask the user if we should save the changes.
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") 'MeLabel)
            Case MsgBoxResult.No
                ' The data is not safe.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db = New DataClassesHglpDataContext(conn)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db = New DataClassesHglpDataContext(conn)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    'db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
                Return False
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
                If Not Conditions() Then
                    Exit Function
                End If
            End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    'Dim NFINDOC As New Revera.FINDOC ' = db.GetChangeSet.Inserts.Where(Function(f) f.GetType.FullName = "GmSupp.Revera.FINDOC").SingleOrDefault
                    'If Not db.GetChangeSet.Deletes.Count = 0 Then
                    '    '' Delete
                    '    For Each deleted As Object In db.GetChangeSet.Deletes
                    '        If deleted.GetType.ToString.Contains("FINDOC") Then
                    '            'Dim DMTRLINE As MTRLINE = deleted
                    '            Dim DFINDOC As Revera.FINDOC = deleted 'DMTRLINE.FINDOC1
                    '            'db.FINDOCs.DeleteOnSubmit(DFINDOC)
                    '            'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                    '            Dim snum As Revera.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = DFINDOC.COMPANY And f.SOSOURCE = DFINDOC.SOSOURCE And f.SERIES = DFINDOC.SERIES And f.FISCPRD = DFINDOC.FISCPRD).FirstOrDefault
                    '            If snum.SERIESNUM = DFINDOC.SERIESNUM Then
                    '                snum.SERIESNUM -= 1
                    '            End If

                    '        End If
                    '    Next
                    'Else
                    '    If Not db.GetChangeSet.Inserts.Count = 0 Then
                    '        'Dim TFINDOC As FINDOC = Me.MasterBindingSource.Current
                    '        Dim MTRLINES As Integer = 0
                    '        If NFINDOC.FINDOC > 0 Then
                    '            MTRLINES = db.MTRLINEs.Where(Function(f) f.FINDOC = NFINDOC.FINDOC).Max(Function(f) f.MTRLINES)
                    '        End If
                    '        For Each insertion As Object In db.GetChangeSet.Inserts
                    '            If insertion.GetType.ToString.Contains("FINDOC") Then
                    '                NFINDOC = insertion
                    '                'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                    '                Dim snum As Revera.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
                    '                snum.SERIESNUM += 1
                    '                NFINDOC.SERIESNUM = snum.SERIESNUM
                    '                Dim fmt As String = "ΑIT0000000"
                    '                NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
                    '                NFINDOC.INSUSER = 99
                    '                NFINDOC.INSDATE = Now()
                    '            End If
                    '            If insertion.GetType.ToString.Contains("MTRLINE") Then
                    '                Dim NMTRLINE As Revera.MTRLINE = insertion
                    '                MTRLINES += 1
                    '                NMTRLINE.MTRLINES = MTRLINES 'NFINDOC.MTRLINEs.Count 
                    '                NMTRLINE.LINENUM = NMTRLINE.MTRLINES
                    '            End If
                    '        Next
                    '    End If
                    'End If
                    'If Not db.GetChangeSet.Updates.Count = 0 Then
                    '    For Each Changes As Object In db.GetChangeSet.Updates
                    '        If Changes.GetType.ToString.Contains("FINDOC") Then
                    '            NFINDOC = Changes
                    '            NFINDOC.UPDUSER = 99
                    '            NFINDOC.UPDDATE = Now()
                    '        End If
                    '        If Changes.GetType.ToString.Contains("MTRLINE") Then
                    '            Dim NMTRLINE As Revera.MTRLINE = Changes
                    '        End If
                    '    Next
                    'End If
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

    Private Function Conditions() As Boolean
        Conditions = True
        Dim smsg As String = String.Empty
        'For Each tt In db.GetChangeSet.Updates
        '    smsg &= "Προσοχή !!!. Λάθος κωδικός" & vbCrLf

        'Next

        If Not smsg = String.Empty Then
            MsgBox(smsg, MsgBoxStyle.Critical)
            Return False
        End If

        'Throw New NotImplementedException
    End Function
#End Region
#Region "96-MasterDataGridView"
    'Dim editableFields_MasterDataGridView() As String = If(Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode", {"PrdDate", "PackingDate", "Shift", "Machine"}, {"LBName", "Pack", "Weight"})

    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If editableFields_MasterDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Dim editableFields_MTRLINEsDataGridView() As String = {"LBName", "Pack", "Weight"}


    Private Sub MTRLINEsDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MTRLINEsDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If editableFields_MTRLINEsDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        If MTRLINEsDataGridView.IsCurrentCellDirty Then
            MTRLINEsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MasterDataGridView.CellClick

        'Dim drv As SOCARRIER = Me.MasterBindingSource.Current
        'Me.DetailsBindingSource.Clear()

        'MasterDataGridView_CellContentClick1(drv.SOCARRIER)
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            'Me.MasterDataGridView.AutoResizeColumns()
            'Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect

            '            Company SODTYPE	MTRL	COMPANY	BMTRL	FISCPRD	QTY1	SODTYPE	BMTRL	NUM02	CODE	NAME	CODE1	CODE2	MTRTYPE	MTRTYPE1	MTRUNIT1	MTRPLACE	REMAINLIMMIN	REMAINLIMMAX	REORDERLEVEL	REMARKS	IMPEXPQTY1	SUMORDERED	SUMRESERVED
            '4000    51	206001	4000	206001	2020	0	NULL	NULL	0	1A2PP0320100	GLOBE VALVE,DN 100,PN 16	NULL	NULL	0	0	101	61 A 3	0	0	0	GLOBE VALVE,DN 100,PN 16 FACE TO FACE : DIN 3202, F1 FLANGED : PN 16 DIN 2501 FACING TYPE C DIN 2526 MATERIAL: BODY : 1.0460/1.0619 TRIM:13% CR	0	0	0

            If Me.Text = "Αποθήκη - Νέα Αίτηση/Υπόλοιπα Ειδών" Then
                myArrF = ("CODE,NAME,MTRUNITC,MTRPLACE,IMPEXPQTY1,SUMORDERED,SUMRESERVED,MTRLBAL,REMAINLIMMIN,REMAINLIMMAX,REORDERLEVEL,REMARKS").Split(",")
                myArrN = ("Κωδικός,Περιγραφή,Μ.Μ,Συνήθης θέση αποθ.,Υπολ.Πραγμ.,Αναμενόμενα,Δεσμευμένα,ΥΠΟΛΟΙΠΟ(υ+α-δ),Ελάχιστο όριο,Μέγιστο όριο,Όριο ανα παραγγελία,Παρατηρήσεις").Split(",")
            End If
            If Me.Text = "Αποθήκη - Εκκρεμείς Παραγγελίες" Then
                '            NO_ Company	FINDOC	COMPANY	MTRLINES	SODTYPE	MTRL	SOSOURCE	SOREDIR	SOSOURCE	SOREDIR	TRNDATE	SERIES	FPRMS	FINCODE	SODTYPE	TRDR	CODE	NAME	CMPMODE	ISPRINT	APPRV	OrderNo	Applicant	INSUSER
                '1   4000	82006	4000	1	51	207120	1251	0	1251	0	2020-04-01 00:00:00.000	2021	2021	ΠΑΡ0001593	12	35896	000478	ΧΑΤΖΟΠΟΥΛΟΣ ΚΥΡΙΑΚΟΣ Α.Ε.	18	1	1	175	87	1600

                '            NO_ FINDOC	TRDR	CODE	NAME	MTRL	SHIPDATE	DELIVDATE	COMMENTS	COMMENTS1	QTY1	QTY1CANC	LINEVAL	RESTMODE	QTY1OPEN	LINEVALOPEN	CDIM1	CDIM2	CDIM3	CDIMNUSE1	CDIMNUSE2	CDIMNUSE3	SOANAL	DISC1VAL	DISC2VAL	DISC3VAL	PRICE
                '1   82006	35896	0000432	ΡΟΥΛΜΑΝ 33013	209327	NULL	2020-04-13 00:00:00.000	ΓΙΑ ΤΗΝ ΕΠΙΣΚΕΥΗ: J-1524 Α/Β ή J-1534 A/B  ΠΗΝΙΟ ΚΙΝΗΣΗΣ J-1502 A/B	ΡΟΥΛΜΑΝ 33013	4	0	67, 44	11	4	67, 44	NULL	NULL	NULL	NULL	NULL	NULL	NULL	NULL	NULL	NULL	16, 86


                '            Αρχ.Εκκρεμών        Εκκρεμή
                'A/ A Ημερ/νία	Παραστατικό	Αρ.Αίτησης	Χρήστης εισαγωγής	Τύπος	Κωδικός	Επωνυμία	Ποσ.1	Ακυρ.ποσ.	Ποσ.1
                '1   01/04/2020	ΠΑΡ0001593	175	ΟΙΚΟΝΟΜΟΥ Π	Παραγγελία Σε Προμηθευτή	000478	ΧΑΤΖΟΠΟΥΛΟΣ ΚΥΡΙΑΚΟΣ Α.Ε.	14,00		14,00
                '		Είδους      Αρχική						
                '    A/ A Κωδικός	Περιγραφή	Ποσ.1	Ακυρ.ποσ.	Εκκρεμή_Ποσ.1				
                '	1   0000432	ΡΟΥΛΜΑΝ 33013	4,00		4,00				

                'myArrF = ("NO_,TRNDATE,FINCODE,OrderNo,INSUSER,FPRMS,CODE,NAME,QTY1,QTY1CANC,QTY1OPEN").Split(",")
                'myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αρ.Αίτησης,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Ποσ.1,Ακυρ.ποσ.,Εκκρεμή_Ποσ.1").Split(",")

                'myArrF = ("NO_,TRNDATE,FINCODE,OrderNo,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME").Split(",")
                'myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αρ.Αίτησης,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία").Split(",")

                'myArrF = ("NO_,CODE,NAME,QTY1,QTY1CANC,QTY1OPEN").Split(",")
                'myArrN = ("A/A,Κωδικός,Περιγραφή,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1").Split(",")

                myArrF = ("NO_,TRNDATE,FINCODE,ApplicantNAME,OrderNo,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME,CODE,NAME,MTRUNITC,QTY1,QTY1CANC,QTY1OPEN").Split(",")
                myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αιτών,Αρ.Αίτησης,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1").Split(",")

            End If
            If Me.Text = "Αποθήκη - Αιτήσεις σε Εκρεμότητα" Then
                'myArrF = ("NO_,TRNDATE,FINCODE,uf1Name,mtName,muName,MTRPLACE,OrderNo,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME,CODE,NAME,MTRUNITC,QTY1,QTY1CANC,QTY1OPEN").Split(",")
                'myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αιτών,Περιγραφή,Μ.Μ,Συνήθης θέση αποθ.,Αρ.Αίτησης,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1").Split(",")
                myArrF = ("NO_,TRNDATE,RequestNo,uf1Name,uf2Name,tdName,cTDpName,CODE,mtName,muName,QTY1,IMPEXPQTY1,SUMORDERED,SUMRESERVED,MTRLBAL,REMAINLIMMIN,REMAINLIMMAX,REORDERLEVEL,REMARKS,mtRemarks,VARCHAR02,FINCODE").Split(",")
                myArrN = ("A/A,Ημερ/νία,Αίτηση,Αιτών,Από Τμήμα,Για Πελάτη,Για Τμήμα Πελάτη,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Υπολ.Πραγμ.,Αναμενόμενα,Δεσμευμένα,ΥΠΟΛΟΙΠΟ(υ+α-δ),Ελάχιστο όριο,Μέγιστο όριο,Όριο ανα παραγγελία,Παρατηρήσεις,Παρατηρήσεις,Εγκρίσεις,Παραστατικό").Split(",")
            End If

            If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Then
                myArrF = ("CODE,NAME,LBName,LBCode,Pack,Weight,PrdDate,PackingDate,Machine,LabelsNo,PrintedLabels,CanceledLabels").Split(",")
                myArrN = ("Είδος,Περιγραφή,Λιπάσμα,Κωδ.Λιπάσματος,Τύπος Συσκευασίας,Βάρος σάκου,Ημ/νία Παραγωγής,Ημ/νία Συσκευασίας,Μηχανή ενσάκισης,Πλήθος,PrintedLabels,CanceledLabels").Split(",")
            End If

            If Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then
                myArrF = ("CODE,NAME,LBName,LBCode,Pack,Weight").Split(",")
                myArrN = ("Είδος,Περιγραφή,Λιπάσμα,Κωδ.Λιπάσματος,Τύπος Συσκευασίας,Βάρος σάκου").Split(",")
            End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
                MasterDataGridView.Columns(i).ReadOnly = True
            Next
            If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Or Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then
                For Each edf In editableFields_MasterDataGridView
                    Dim Col As DataGridViewColumn = GetNoColumnDataGridView(Me.MasterDataGridView, edf)
                    If Not IsNothing(Col) Then
                        Col.ReadOnly = False
                    End If
                Next
            End If
            If Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then
                Exit Sub
            End If
            If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Then
                AddOutOfOfficeColumn(Me.MasterDataGridView)
            End If

            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next

            Me.MasterDataGridView.AutoResizeColumns()


            'Dim emptyMTRL = {New LNK.MTRL With {.CODE = "<Επιλέγξτε>", .MTRL = 0}}.ToList
            ''Dim mm = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE.Substring(0, 1) = "6").ToList
            'Dim mm = (From m In dbLNK.MTRLs Join ex In dbLNK.MTREXTRAs On m.COMPANY Equals ex.COMPANY And m.MTRL Equals ex.MTRL
            '          Where ex.BOOL04 = 1
            '          Select m).ToList
            'mtrs = (From Empty In CType(emptyMTRL, List(Of LNK.MTRL)).Union(mm)).ToList


            Dim Result As New Dictionary(Of Short, String) ' = db.UFTBL01s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251).OrderBy(Function(f) f.NAME).ToDictionary(Function(f) f.UFTBL01, Function(f) f.NAME)

            'Dim emptyMTRL = {New LNK.MTRL With {.CODE = "<Επιλέγξτε>", .MTRL = 0}}.ToList


            'With Result
            '    .Add(New With {.CODE = 0, MTRL = 0})
            '    .

            'End With
            Result.Add(0, "<Επιλέγξτε>")
            Result.Add(1, "A")
            Result.Add(2, "B")
            Result.Add(3, "C")

            'Debug.Print("Values inserted into dictionary:")
            'For Each dic As KeyValuePair(Of Short, String) In Result
            '    Debug.Print([String].Format("English salute {0} is {1} in German", dic.Key, dic.Value))
            'Next

            ''Me.GmChkListBoxAplicant.Height = 33
            'Me.GmChkListBoxAplicant.dgv.DataSource = Result.ToList



            Dim columnComboBox As New DataGridViewComboBoxColumn()
            columnComboBox.DataSource = Result.ToList '{"<Επιλέγξτε>", "A", "B", "C"}
            columnComboBox.DisplayMember = "Value"
            columnComboBox.HeaderText = "Βάρδια"
            columnComboBox.Name = "Shift"
            columnComboBox.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            columnComboBox.SortMode = DataGridViewColumnSortMode.Automatic
            columnComboBox.ValueMember = "Key"
            'columnComboBox.Items.AddRange({"A", "B", "C"})
            columnComboBox.Width = 120
            'columnComboBox.AutoComplete = True
            columnComboBox.FlatStyle = FlatStyle.Flat
            MasterDataGridView.Columns.Insert(9, columnComboBox)


            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                'Dim dll As DataGridViewComboBoxCell = row.Cells("Κωδ.Λογιστικής")

                'Dim MTRL As Integer = row.Cells("MTRL").Value

                'Dim m As PFIC.MTRL = db1.MTRLs.Where(Function(f) f.SODTYPE = 53 And f.CODE = "").FirstOrDefault

                'If Not IsNothing(m) Then
                '    dll.Items.Add(m)
                '    dll.Value = MTRL
                'End If

                Dim item As BCLabel = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        Dim dll As DataGridViewComboBoxCell = row.Cells("Shift")
                        'If Not IsNothing(item.Shift) AndAlso item.Shift > 0 Then
                        dll.Value = item.Shift
                        'Else
                        '    dll.Value = 0
                        'End If


                    Catch ex As Exception

                    End Try
                End If

            Next

            If Not IsNothing(MasterDataGridView.Columns("Παρατηρήσεις")) Then
                MasterDataGridView.Columns("Παρατηρήσεις").Width = 460
                MasterDataGridView.Columns("Παρατηρήσεις").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End If

            For Each Col In MasterDataGridView.Columns
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
                                If Not t.FullName.IndexOf("System.DateTime") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
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
                            If Not t.FullName.IndexOf("System.DateTime") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                If {"PrdDate", "PackingDate"}.Contains(Col.DataPropertyName) Then
                                    .DefaultCellStyle.Format = "dd/MM/yyyy" ' HH: mm"
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


            'Me.MasterDataGridView.AutoResizeRows()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub MasterDataGridView_Sorted(sender As Object, e As EventArgs) Handles MasterDataGridView.Sorted
        MasterDataGridView_Styling()
    End Sub

    'Private Sub MTRLINEsDataGridView_Styling()
    '    Try

    '        Me.MTRLINEsDataGridView.AutoGenerateColumns = True

    '        'ΚΩΔΙΚΟΣ SFT 1		ΠΟΣΟΤΗΤΑ	MM	ΠΕΡΙΓΡΑΦΗ 			

    '        myArrF = ("CODE,QTY1,MTRUNITC,NAME,COMMENTS1").Split(",")
    '        myArrN = ("Κωδικός,Ποσ.1,Μ.Μ,Περιγραφή,Παρατηρήσεις").Split(",")

    '        'Add Bound Columns
    '        Dim bad_item_columns() As Integer = {1, 2, 3, 4}
    '        RemoveGridColumnsByCollection(MTRLINEsDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)


    '        For i As Integer = 0 To MTRLINEsDataGridView.Columns.Count - 1
    '            Debug.Print(MTRLINEsDataGridView.Columns(i).DataPropertyName & vbTab & MTRLINEsDataGridView.Columns(i).Name)
    '            MTRLINEsDataGridView.Columns(i).ReadOnly = True
    '        Next
    '        For Each edf In editableFields_MTRLINEsDataGridView
    '            Dim Col As DataGridViewColumn = GetNoColumnDataGridView(Me.MTRLINEsDataGridView, edf)
    '            If Not IsNothing(Col) Then
    '                Col.ReadOnly = False
    '            End If
    '        Next





    '        If Not IsNothing(MTRLINEsDataGridView.Columns("Παρατηρήσεις")) Then
    '            'MTRLINEsDataGridView.Columns("Παρατηρήσεις").Width = 460
    '            MTRLINEsDataGridView.Columns("Παρατηρήσεις").DefaultCellStyle.WrapMode = DataGridViewTriState.True
    '        End If

    '        For Each Col In MTRLINEsDataGridView.Columns
    '            Try
    '                Dim t As Type = Col.ValueType
    '                If Not IsNothing(t) Then
    '                    With Col
    '                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
    '                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
    '                                .DefaultCellStyle.Format = "N2"
    '                                If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
    '                                    .DefaultCellStyle.Format = "N3"
    '                                End If
    '                            End If
    '                            If Not t.FullName.IndexOf("System.DateTime, ") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
    '                                If Col.DataPropertyName = "TRNDATE" Then
    '                                    .DefaultCellStyle.Format = "dd/MM/yyyy HH: mm"
    '                                End If

    '                            End If
    '                        End If
    '                        If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
    '                             Then
    '                            .DefaultCellStyle.Format = "N2"
    '                            If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
    '                                .DefaultCellStyle.Format = "N3"
    '                            End If
    '                        End If
    '                        'If col.ValueType.Name = "String" Then
    '                        '    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
    '                        '    '.Width = 200
    '                        'End If
    '                        'If col.ValueType.Name <> "String" Then
    '                        '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '                        'End If
    '                    End With
    '                End If
    '            Catch ex As Exception
    '                MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
    '            End Try
    '        Next

    '        'Me.MTRLINEsDataGridView.AutoResizeRows()
    '        Me.MTRLINEsDataGridView.AutoResizeColumns()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function GetNoColumnDataGridView(CurDataGridView As DataGridView, CDataPropertyName As String) As DataGridViewColumn
        Dim col As DataGridViewColumn = Nothing
        col = CurDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = CDataPropertyName).FirstOrDefault
        Return col
        'Throw New NotImplementedException()
    End Function
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnUnCheck.Click, TlSBtnCheck.Click
        Dim s As ToolStripButton = sender
        Dim check As Boolean = False
        If s.Name = "TlSBtnCheck" Then
            check = True
        Else
            check = False
        End If
        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                If Not ds.Cells("Check").Value = check Then
                    ds.Cells("Check").Value = check
                End If
            Next
            'For i As Integer = 0 To DrSel.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
            'Next
        Else
            For Each ds As DataGridViewRow In Me.MasterDataGridView.Rows
                ds.Cells("Check").Value = check
            Next
            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
            'Next
        End If
        Me.MasterDataGridView.RefreshEdit()
    End Sub
    Private Sub MasterDataGridView_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MasterDataGridView.CellFormatting
        'Dim s As DataGridView = sender
        'If s.Columns(e.ColumnIndex).Name.Equals("MTRL1_CODE") Then
        '    ' Use helper method to get the string from lookup table
        '    Dim MTRL As Integer = s.Rows(e.RowIndex).Cells("MTRL").Value
        '    Dim m As MTRL = db.MTRLs.Where(Function(f) f.MTRL = MTRL).FirstOrDefault
        '    If Not IsNothing(m) Then
        '        e.Value = m.CODE 'GetWorkplaceNameLookupValue(dataGridViewScanDetails.Rows(e.RowIndex).Cells("UserWorkplaceID").Value)
        '    End If
        'End If
    End Sub

    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
        Dim s As DataGridView = sender
        Try
            If {"LBName", "LBCode", "Pack", "Weight"}.Contains(s.Columns(e.ColumnIndex).DataPropertyName) Then
                Dim cellc As DataGridViewCell = s.CurrentCell
                Dim Search_Code As String = cellc.EditedFormattedValue
                If Search_Code = "" Then
                    Exit Sub
                End If
                If Not cellc.FormattedValue.ToString = Search_Code Then
                    'Dim cellCodeExp As DataGridViewCell = s.Rows(e.RowIndex).Cells("CodeExp")
                    'cellCodeExp.Value = Search_Code

                    Dim item As BCLabel = s.Rows(e.RowIndex).DataBoundItem
                    Dim mtrl As Integer = item.mtrl
                    Dim mtr = db.MTRLs.Where(Function(f) f.MTRL = mtrl).FirstOrDefault
                    If Not IsNothing(mtr) Then
                        '    If Search_Code = "<Επιλέγξτε>" Then
                        '        mtr.CodeExp = Nothing
                        '    Else
                        '        mtr.CodeExp = Search_Code
                        '    End If

                        Dim dscr As New ccCDescr
                        dscr.mtrl = item.mtrl
                        dscr.LBName = item.LBName
                        dscr.LBCode = item.LBCode
                        dscr.Pack = item.Pack
                        dscr.Weight = item.Weight

                        If {"LBName"}.Contains(s.Columns(e.ColumnIndex).DataPropertyName) Then
                            dscr.LBName = Search_Code
                        End If
                        If {"LBCode"}.Contains(s.Columns(e.ColumnIndex).DataPropertyName) Then
                            dscr.LBCode = Search_Code
                        End If
                        If {"Pack"}.Contains(s.Columns(e.ColumnIndex).DataPropertyName) Then
                            dscr.Pack = Search_Code
                        End If
                        If {"Weight"}.Contains(s.Columns(e.ColumnIndex).DataPropertyName) Then
                            dscr.Weight = Search_Code
                        End If
                        Dim cDescr As String = Newtonsoft.Json.JsonConvert.SerializeObject(dscr)

                        'Dim gg1 = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ccCDescr)(cDescr)

                        mtr.ccCDescr = cDescr

                        mtr.UPDDATE = Now()
                        Dim cuser = 99 's1Conn.ConnectionInfo.UserId
                        mtr.UPDUSER = cuser

                        If db.GetChangeSet.Updates.Count > 0 Then
                            Me.BindingNavigatorSaveItem.Enabled = True
                        Else
                            Me.BindingNavigatorSaveItem.Enabled = False
                        End If

                    End If
                End If
            End If
            If {"Shift"}.Contains(s.Columns(e.ColumnIndex).Name) Then
                Dim cellc As DataGridViewCell = s.CurrentCell
                Dim Search_Code As String = cellc.EditedFormattedValue
                If Search_Code = "" Then
                    Exit Sub
                End If
                If Not cellc.FormattedValue.ToString = Search_Code Then

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub MasterDataGridView_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellValidated
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
            Dim cellc As DataGridViewCell = s.CurrentCell
            Dim CodeExp As String = cellc.EditedFormattedValue
            Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_Code")
            If celln.Value = 0 AndAlso CodeExp = String.Empty Then
                Exit Sub
            End If

            Dim ml = s.Tag 'Nothing

            If Not IsNothing(ml) AndAlso ml.Count = 0 Then
                MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
                Dim item = s.Rows(cellc.RowIndex).DataBoundItem
                Dim chItem = db.GetChangeSet.Updates.Where(Function(f) f.cccMultiCompData = item.cccMultiCompData).FirstOrDefault
                If Not IsNothing(chItem) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, chItem)
                End If

                cellc.Value = Nothing
                celln.Value = 0

                If db.GetChangeSet.Updates.Count > 0 Then
                    Me.BindingNavigatorSaveItem.Enabled = True
                Else
                    Me.BindingNavigatorSaveItem.Enabled = False
                End If

            End If
        End If

        s.Tag = Nothing

    End Sub

    Private Sub MasterDataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError

        If editableFields_MasterDataGridView.Contains(sender.Columns(e.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        MessageBox.Show("Error happened " _
            & e.Context.ToString() & vbCrLf & "Row, Col: " & e.RowIndex & "," & sender.Columns(e.ColumnIndex).Name)

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

    Private Sub MasterDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MasterDataGridView.EditingControlShowing
        'Dim s As GmDataGridView = sender
        'Dim cell As DataGridViewCell = s.CurrentCell
        ''Dim r = cell.OwningRow.Cells("")..Cells("MTRL")
        'If cell.ColumnIndex = 2 Then
        '    'Dim c As ComboBox = CType(e.Control, ComboBox)
        'End If

    End Sub
    Private Sub MasterDataGridView_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MasterDataGridView.CellMouseDown
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

#End Region
#Region "97- Control Events"
    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        'Dim q = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True)
        'If q.Count = 0 Then
        '    e.Cancel = True
        '    Exit Sub
        'End If
        'Dim q1 = q.Select(Function(f) f.Cells(1).Value)
        'Dim wfm As New WHouseBalFR
        'wfm.bindingSource = New BindingSource
        'Dim dets As New List(Of Revera.GetPendingOrdersDetailsResult)
        'For Each q2 As DataGridViewRow In q
        '    Dim det = New Revera.GetPendingOrdersDetailsResult
        '    det.CODE = q2.Cells(1).Value
        '    det.NAME = q2.Cells(2).Value
        '    dets.Add(det)
        '    wfm.txtBoxMtrlCode.Text &= det.CODE & ","
        'Next
        'wfm.DateTimePicker1.Value = CTODate
        'wfm.bindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(dets)
        'wfm.ShowDialog()
        'Me.BindingNavigatorMasterAddNewItem.PerformClick()
    End Sub
    'Private Sub BindingNavigatorAddNewItem_Click(sender As System.Object, e As System.EventArgs) Handles ΑΠΟΓΡΑΦΗToolStripMenuItem.Click, ΕΞΑΓΩΓΕΣToolStripMenuItem.Click, ΕΙΣΑΓΩΓΕΣToolStripMenuItem.Click
    '    Cmd_Add(sender)
    'End Sub
    'Private Sub BindingNavigatorDeleteItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorDeleteItem.Click
    '    Cmd_Delete()
    'End Sub
    'Private Sub BindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs)
    '    Me.Validate()
    '    Me.MasterBindingSource.EndEdit()
    'End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
            DateTimePicker1.Value = CTODate
        Else
            DateTimePicker1.Value = "01/01/" & Year(CTODate)
        End If
    End Sub
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
    Private Sub BindingNavigatorMasterAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMasterAddNewItem.Click
        Cmd_Add()
    End Sub


    Private Sub TlSBtn_Click(sender As Object, e As EventArgs)  ',TlSBtnCheque.Click, TlSBtnTRDR.Click, TlSBtnPRSN.Click,
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
                'TlSTxtTRDR.Tag = "CODE"
                'ReturnFields.Add(TlSTxtTRDR)
                'GmTitle = "Ευρετήριο Πελατών"
                'RsTables = "TRDR"

                'Company = CompanyT

                'RsWhere = "Company = " & Company & " AND SODTYPE=13 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                'RsOrder = "CODE"

                'sSQL = "SELECT CODE, NAME FROM TRDR "
                'sender_TAG = ReturnFields(0).Tag
                'myArrF = ("CODE,NAME").Split(",")
                'myArrN = ("Κωδικός,Επωνυμία").Split(",")

            Case "TlSBtnMTRL", "TlSTxtMTRL"
                'TlSTxtMTRL.Tag = "CODE"
                'ReturnFields.Add(TlSTxtMTRL)
                'GmTitle = "Ευρετήριο Ειδών"
                'RsTables = "MTRL"

                'Company = CompanyT

                'RsWhere = "Company = " & Company & " AND SODTYPE=51 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                'RsOrder = "CODE"

                'sSQL = "SELECT CODE, NAME FROM MTRL "
                'sender_TAG = ReturnFields(0).Tag
                'myArrF = ("CODE,NAME").Split(",")
                'myArrN = ("Κωδικός,Περιγραφή").Split(",")

            Case "TlSBtnPRSN", "TlSTxtPRSN"
                'TlSTxtPRSN.Tag = "CODE"
                'ReturnFields.Add(TlSTxtPRSN)
                'GmTitle = "Ευρετήριο Πωλητών"
                'RsTables = "PRSN"

                'Company = CompanyT

                'RsWhere = "Company = " & Company & " AND SODTYPE=20 AND TPRSN=0" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                'RsOrder = "CODE,PRSN"
                ''SELECT A.COMPANY,A.SODTYPE,A.PRSN,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TPRSN,A.AFM,A.IDENTITYNUM FROM PRSN A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TPRSN=0 ORDER BY A.CODE,A.PRSN
                'sSQL = "SELECT CODE,NAME,NAME2 FROM PRSN "
                'sender_TAG = ReturnFields(0).Tag
                'myArrF = ("CODE,NAME2,NAME").Split(",")
                'myArrN = ("Κωδικός,Επώνυμο,Όνομα").Split(",")
                'GmCheck = True

            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
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
    Private Sub ExportDataToExcel(fileName As String, title As String)

        Using p As New ExcelPackage()
            Try

                'Here setting some document properties
                p.Workbook.Properties.Author = "GmLogic"
                p.Workbook.Properties.Title = fileName '"ExprotData"

                'Create a sheet
                p.Workbook.Worksheets.Add(title)

                For Each ws As ExcelWorksheet In p.Workbook.Worksheets
                    Dim lst = Me.MasterBindingSource.DataSource
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

                    'If Me.radioBtnSales.Checked Then
                    '    lst = CType(lst, DataTable).AsEnumerable
                    'End If

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
                                'If Not Me.radioBtnSales.Checked Then
                                cell.Value = ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                                'Else
                                '    cell.Value = ro(col_Name)
                                'End If

                                'Setting Value in cell
                                With cell
                                    Dim t As Type
                                    'If Not Me.radioBtnSales.Checked Then
                                    t = ro.GetType().GetProperty(col_Name).PropertyType 'col.ValueType
                                    'Else
                                    '    t = ro(col_Name).GetType
                                    'End If

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
                            'If Me.radioBtnΑggregate.Checked Then
                            '    formula = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'End If
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

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim s As ComboBox = sender
        Αpprovs = s.SelectedItem.ToString()
        Αpprovs = Αpprovs.Replace("Προιστ.", "").Replace("Διευθ.", "")
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Dim s As ToolStripButton = sender
        Try
            Dim chkLists1 = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                             Where ce.Cells("Check").Value = True).FirstOrDefault

            Dim chkLists As List(Of BCLabel) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                                                Where ce.Cells("Check").Value = True
                                                Select New BCLabel With {.code = ce.Cells("Είδος").Value,
                                                    .LBCode = ce.Cells("Κωδ.Λιπάσματος").Value,
                                                    .Pack = ce.Cells("Τύπος Συσκευασίας").Value,
                                                    .Weight = ce.Cells("Βάρος σάκου").Value,
                                                    .PrdDate = ce.Cells("Ημ/νία Παραγωγής").Value,
                                                    .PackingDate = ce.Cells("Ημ/νία Συσκευασίας").Value,
                                                    .Shift = ce.Cells("Shift").Value,
                                                    .Machine = ce.Cells("Μηχανή ενσάκισης").Value,
                                                    .LabelsNo = ce.Cells("Πλήθος").Value}).ToList

            If chkLists.Count = 0 Then
                Exit Sub
            End If

            'For Each ch In chkLists
            '    ch.Ticks = Now.Ticks
            '    'Dim Line1 As String = ch.LBCode & " " & ch.Pack & " " & ch.Weight ' & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine
            '    'Dim Line2 As String = "" 'String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine
            '    'Dim Line3 As String = String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine
            '    'Dim Line4 As String = String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine

            '    Dim qr As String = ch.code & " " & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine & "{" & ch.Ticks & "}" & "$" '"2105050533 25072020 28072020 14:35 ABCDEF$"
            '    Dim pd As New PrintDialog()
            '    '2103050533
            '    ' Open the printer dialog box, and then allow the user to select a printer.
            '    pd.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            '    If pd.ShowDialog = DialogResult.OK Then
            '        For i As Integer = 0 To ch.LabelsNo - 1
            '            'Dim rawNew = raw.Replace("[Line1]", Line1).Replace("[Line2]", Line2).Replace("[Line3]", Line3).Replace("[QrCode]", qr)
            '            'RawPrinterHelper.gmSendStringToPrinter(pd.PrinterSettings.PrinterName, rawNew)
            '        Next
            '    End If
            'Next
            'If s.Name = "btnQR" Then
            '    Exit Sub
            'End If

            '^FO command sets a field origin, relative to the label home (^LH) position. ^FO sets the upper-left
            'corner of the field area by defining points along the x-axis And y-axis independent of the rotation.
            'Format: ^FOx,y,z
            Dim raw = "^XA" & vbCrLf &
                        "^FO500,100" & vbCrLf &
                        "^BQN,2,10" & vbCrLf &
                        "^FDMM,A[QrCode1]^FS" & vbCrLf &
                        "^FO550,640" & vbCrLf &
                        "^A0R,150,150" & vbCrLf &
                        "^FDCE^FS" & vbCrLf &
                        "^FO400,450" & vbCrLf &
                        "^A0R,150,150" & vbCrLf &
                        "^FD[Line1]^FS" & vbCrLf &
                        "^FO300,50" & vbCrLf &
                        "^A0R,90,90" & vbCrLf &
                        "^FD[Line2]^FS" & vbCrLf &
                        "^A0R,90,90" & vbCrLf &
                        "^FO200,50" & vbCrLf &
                        "^FD[Line3]^FS" & vbCrLf &
                        "^A0R,50,50" & vbCrLf &
                        "^FO140,50" & vbCrLf &
                        "^FD[Line4]^FS" & vbCrLf &
                        "^FO80,950" & vbCrLf &
                        "^BQN,2,5" & vbCrLf &
                        "^FDMM,A[QrCode2]^FS" & vbCrLf &
                        "^XZ" & vbCrLf

            For Each ch In chkLists
                Dim Line1 As String = ch.LBCode & " " & ch.Pack & " " & ch.Weight ' & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine
                Dim Line2 As String = "" 'String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine
                Dim Line3 As String = String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine


                Dim qr As String = ch.code & " " & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine & "-Ticks-" & "$" '"2105050533 25072020 28072020 14:35 ABCDEF$"
                Dim pd As New PrintDialog()

                ' Open the printer dialog box, and then allow the user to select a printer.
                pd.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
                If pd.ShowDialog = DialogResult.OK Then
                    Dim OldTicks = Now.ToString("yyMMddHHmmssff") ' Now.Ticks
                    qr = qr.Replace("Ticks", OldTicks)
                    For i As Integer = 0 To ch.LabelsNo - 1
                        Dim NewTicks = Now.ToString("yyMMddHHmmssff") 'Now.Ticks
                        Dim Line4 As String = NewTicks
                        qr = qr.Replace(OldTicks, NewTicks)
                        Dim qr1 = qr.Replace("$", "%")
                        OldTicks = NewTicks
                        Dim rawNew = raw.Replace("[Line1]", Line1).
                            Replace("[Line2]", Line2).Replace("[Line3]", Line3).
                            Replace("[Line4]", Line4).Replace("[QrCode1]", qr).Replace("[QrCode2]", qr1)
                        RawPrinterHelper.gmSendStringToPrinter(pd.PrinterSettings.PrinterName, rawNew)
                    Next
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnQR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQR1.Click, btnQR2.Click
        Dim s As ToolStripButton = sender
        btnQRName = s.Name
        'Dim pd As New PrintDialog()
        'pd.AllowSomePages = True
        'pd.ShowHelp = True
        'pd.Document = PDoc
        'Dim result As DialogResult = pd.ShowDialog()

        'If result = DialogResult.OK Then
        '    PDoc.Print()
        'End If
        Dim pdPr As New PrintPreviewDialog()
        'pdPr.ClientSize = New System.Drawing.Size(400, 300)
        'pdPr.Location = New System.Drawing.Point(29, 29)
        'pdPr.Name = "PrintPreviewDialog1"
        ''document.PrintPage += New System.Drawing.Printing.PrintPageEventHandler(AddressOf document_PrintPage)
        'pdPr.MinimumSize = New System.Drawing.Size(375, 250)
        'pdPr.UseAntiAlias = True

        'Dim Scl As PrinterSettings = Nothing
        'PDoc.PrinterSettings = Scl
        pdPr.Document = PDoc
        pdPr.ShowDialog()
    End Sub

    Private Sub document_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim text As String = "In document_PrintPage method."
        Dim printFont As System.Drawing.Font = New System.Drawing.Font("Arial", 35, System.Drawing.FontStyle.Regular)
        e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, 10, 10)
    End Sub


    Private Sub PDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDoc.PrintPage
        Dim chkLists As List(Of BCLabel) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                                            Where ce.Cells("Check").Value = True
                                            Select New BCLabel With {.code = ce.Cells("Είδος").Value,
                                                    .LBCode = ce.Cells("Κωδ.Λιπάσματος").Value,
                                                    .Pack = ce.Cells("Τύπος Συσκευασίας").Value,
                                                    .Weight = ce.Cells("Βάρος σάκου").Value,
                                                    .PrdDate = ce.Cells("Ημ/νία Παραγωγής").Value,
                                                    .PackingDate = ce.Cells("Ημ/νία Συσκευασίας").Value,
                                                    .Shift = ce.Cells("Shift").Value,
                                                    .Machine = ce.Cells("Μηχανή ενσάκισης").Value,
                                                    .LabelsNo = ce.Cells("Πλήθος").Value}).ToList

        If chkLists.Count = 0 Then
            Exit Sub
        End If
        If chkLists IsNot Nothing Then
            For Each ch In chkLists
                Dim Line1 As String = ch.LBCode & " " & ch.Pack & " " & ch.Weight ' & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine
                Dim Line2 As String = "" 'String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine
                Dim Line3 As String = String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & Shifts(ch.Shift) & " " & ch.Machine

                Dim qr As String = ch.code & " " & String.Format("{0:dd/MM/yy}", ch.PrdDate) & " " & String.Format("{0:dd/MM/yy}", ch.PackingDate) & " " & ch.Shift & " " & ch.Machine & "-Ticks-" & "$" '"2105050533 25072020 28072020 14:35 ABCDEF$"
                qr = qr.Replace("Ticks", Now.ToString("yyMMddHHmmssff")) 'Now.Ticks)
                Dim str_split As List(Of String) = qr.Split(" ").ToList
                Dim MCode As String = str_split(0)
                Dim Ticks As String = str_split.Where(Function(f) f.Contains("-")).FirstOrDefault.Split("-")(1) 'str_split.Where(Function(f) f.Contains("{")).FirstOrDefault.Split("{")(1).Replace("}", "").Replace("$", "")

                If btnQRName = "btnQR2" Then
                    qr = qr.Replace("$", "%")
                End If

                Dim img As System.Drawing.Image = GetQR(qr) 'System.Drawing.Image.FromFile("E:\sc\GmSupp\XamarinPDA\XamarinPDA.Android\Resources\drawable\viber_image01.jpg")) 'D:\Foto.jpg")

                Dim loc As New System.Drawing.Point(10, 10)
                e.Graphics.DrawImage(img, loc)
            Next
        End If

    End Sub

    Private Function GetQR(DocText As String) As System.Drawing.Image
        Dim qrGenerator As New QRCodeGenerator()
        'Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q)
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(DocText, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As QRCode = New QRCode(qrCodeData)
        Dim qrCodeImage As System.Drawing.Bitmap = qrCode.GetGraphic(15)
        Return qrCodeImage
        Throw New NotImplementedException()
    End Function

    Private Sub newPrintTable(gmVMNewDataTable As SortableBindingList(Of Revera.ccCVMtrLine), ReportEmbeddedResource As String)
        Try
            'Dim drvMaster As DataRowView
            'drvMaster = Me.MasterBindingSource.Current()
            Dim WRptView As New ReportViewer
            Dim ReportDataSource1 As New Microsoft.Reporting.WinForms.ReportDataSource
            ReportDataSource1.Name = "DSccCVMtrLine" ' "DataSet1" ' 'TimPrint"

            Dim SelFincode As String = Me.MasterDataGridView.Rows(Me.MasterDataGridView.SelectedCells(0).RowIndex).Cells("Παραστατικό").Value '"ΑIT0000006"
            ReportDataSource1.Value = CType(Me.MasterBindingSource.DataSource, SortableBindingList(Of Revera.ccCVMtrLine)).Where(Function(f) f.FINCODE = SelFincode) 'gmVMNewDataTable 'ConvertSta5Rpt(MtrLineNew, Me.boxOption_Anektelestes.Text) 'gmVMNewDataTable.DefaultView 'dt.DefaultView 'bind 'TimPrintBindingSource 'bind 'dt.DataSet 'TimPrintTableAdapter.Fill(GenDataSet.TimPrint, 0, 0, 0, 0, 0) 'Me.DataTable1BindingSource
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
    Private Sub SurroundingSub()
        Dim qrGenerator As QRCodeGenerator = New QRCodeGenerator()
        'Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q)
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode("", QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As QRCode = New QRCode(qrCodeData)
        Dim qrCodeImage As System.Drawing.Bitmap = qrCode.GetGraphic(20)
        'barcodeGroupBox.BackgroundImage = qrCodeImage
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        LoadDataInit() 'For Bind Any Control
    End Sub
    ' Load the data.
    Private Sub LoadData()
        db = New DataClassesHglpDataContext(conn)
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.HglpConnectionString
            db.CommandTimeout = 360

            'If {"panagiotis", "katerina", "gkonstantatos"}.Contains(CurUser) Then
            '    dbPFIC.Connection.ConnectionString = My.Settings.LKConnectionString
            'End If
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            'Me.MasterBindingSource.DataSource = db.CCCCheckZips.Where(Function(f) f.ZIP = 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Dim gg = db.GetChangeSet
        If Me.DataSafe() Then
            'If Not tlsShipingAllValue = "ToolStripMenuItemShipingAllValue" Then
            '    CreateCarrierDoc(gg)
            'End If
        End If

        Cmd_Select()
    End Sub


    Private Sub BindingNavigatorSaveItemOld_Click(sender As Object, e As EventArgs)
        Me.BindingNavigatorSaveItem.Enabled = False
        Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width
        Me.SplitContainer2.Panel2.Visible = False
        Exit Sub
        If Me.VscsBindingSource.Count > 0 Then
            If MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim q = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True)
            If q.Count = 0 Then
                'e.Cancel = True
                'Exit Sub
            End If
            Dim q1 = q.Select(Function(f) f.Cells(1).Value)
            Dim dets As New List(Of Revera.GetPendingOrdersDetailsResult)
            If Not detsOld.Count = 0 Then
                dets.AddRange(detsOld)
            End If

            'wfm.bindingSource = New BindingSource
            For Each q2 As DataGridViewRow In q
                Dim det = New Revera.GetPendingOrdersDetailsResult
                det.CODE = q2.Cells(1).Value
                det.NAME = q2.Cells(2).Value
                det.MTRUNITC = q2.Cells(3).Value

                dets.Add(det)
                'wfm.txtBoxMtrlCode.Text &= det.CODE & ","
            Next
            dets = dets.Distinct(Function(f) f.CODE).ToList
            wfm.DateTimePicker1.Value = CTODate
            'wfm.bindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(dets)
            wfm.ddlTrdr.DataSource = Trdrs
            wfm.cccTrdDeps = cccTrdDeps
            wfm.ddlApplicant.DataSource = Applicants
            wfm.ShowDialog()
            If dets.Count = 0 Then
                wfm = New WHouseBalFR
                Me.TlSBtnUnCheck.PerformClick()
            End If
            If Not detsOld Is dets Then
                detsOld = dets
            End If
            Exit Sub
        End If



        'If Me.DataSafe() Then
        '    Me.cmdSelect.PerformClick()
        'End If
    End Sub


    Private Sub TlsBtnClear_Click(sender As Object, e As EventArgs) Handles TlsBtnClear.Click
        Me.VscsBindingSource.Clear()

        wfm.DateTimePicker1.Value = CTODate
        'wfm.bindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(dets)
        wfm.ddlTrdr.SelectedIndex = 0
        If wfm.ddlcccTrdDep.Items.Count > 0 Then
            wfm.ddlcccTrdDep.SelectedIndex = 0
        End If
        wfm.ddlApplicant.SelectedIndex = 0
        'wfm.ddlSuppliers.SelectedIndex = 0

        Me.BindingNavigatorSaveItem.Enabled = False
        Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width
        Me.SplitContainer2.Panel2.Visible = False
        Me.cmdSelect.PerformClick()
    End Sub

    Private Sub MasterBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MasterBindingSource.ListChanged
        If e.ListChangedType = ListChangedType.ItemChanged Then
            Dim nu ' As CCCCheckZip = MasterBindingSource.Current
            'nu.modifiedOn = Now()
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
        If e.ListChangedType = ListChangedType.ItemAdded Then
            Me.BindingNavigatorSaveItem.Enabled = True
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

    Private Sub VscsBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles VscsBindingSource.ListChanged
        'Me.lblV09.Text = 0
        'Me.lblV10.Text = 0
        Me.BindingNavigatorSaveItem.Enabled = False
        If Me.VscsBindingSource.Count > 0 Then
            'Dim lst As New List(Of vsc)
            'lst = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

            'Me.lblV08.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
            'Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))
            'Me.lblV10.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice) + lst.Sum(Function(f) f.TotPrice) * (lst.FirstOrDefault.PERCNT / 100))
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
        Catch ex As Exception

        End Try
    End Sub

    Private Function Findoc_AddingNew(fin As Revera.FINDOC, NSoSource As Integer, NSeries As Short) As Revera.FINDOC
        Dim NFINDOC As New Revera.FINDOC
        Try

            NFINDOC.COMPANY = CompanyS
            'NFINDOC.LOCKID = 1
            NFINDOC.FINDOC = 0
            NFINDOC.SOSOURCE = NSoSource '1151
            'NFINDOC.SOREDIR = 0
            NFINDOC.TRNDATE = fin.TRNDATE
            'NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            'NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            NFINDOC.SERIES = NSeries '9590
            'Dim snum As SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            'NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            NFINDOC.FPRMS = NSeries '9590

            NFINDOC.TFPRMS = 100 'Αδιάφορο
            Dim fmt As String = "ΑIT0000000"
            NFINDOC.FINCODE = fmt
            NFINDOC = ZeroFindoc(NFINDOC)
            Return NFINDOC
        Catch ex As Exception

        End Try
    End Function

    Private Function MTRLINE_AddingNew(v As Revera.GetPendingOrdersDetailsResult, NFINDOC As Revera.FINDOC) As Revera.FINDOC
        Try

            'Dim NFINDOC As Revera.FINDOC = Me.MasterBindingSource.Current
            Dim NMTRLINE As New Revera.MTRLINE
            Dim MTRLINES As Integer = 0
            If NFINDOC.MTRLINEs.Count > 0 Then
                MTRLINES = NFINDOC.MTRLINEs.Max(Function(f) f.MTRLINES)
            End If
            'Dim LINENUM As Integer = NMTRLINE.MTRLINES
            NMTRLINE.MTRLINES = MTRLINES + 1
            NMTRLINE.LINENUM = NMTRLINE.MTRLINES
            NMTRLINE = ZeroMTRLINE(NMTRLINE, NFINDOC)
            NMTRLINE.MTRL = v.MTRL
            NMTRLINE.QTY1 = v.QTY1
            NMTRLINE.QTY = NMTRLINE.QTY1
            NMTRLINE.QTY2 = NMTRLINE.QTY1
            NMTRLINE.UFTBL02 = v.UFTBL02 ' 106
            NMTRLINE.cccTrdDep = v.cccTrdDep ' 55
            NMTRLINE.cccTrdr = v.cccTrdr ' 35465
            NMTRLINE.SALESMAN = NFINDOC.SALESMAN
            NMTRLINE.COMMENTS1 = v.COMMENTS1

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
            ''NFINDOC.MTRLINEs.Add(NMTRLINE)
            'e.NewObject = NMTRLINE
            NFINDOC.MTRLINEs.Add(NMTRLINE)
        Catch ex As Exception

        End Try
        Return NFINDOC
    End Function

    Private Function ZeroFindoc(NFINDOC As Revera.FINDOC) As Revera.FINDOC
        Try
            'NFINDOC.COMPANY = 0
            NFINDOC.LOCKID = 1
            'NFINDOC.FINDOC = 0
            'NFINDOC.SOSOURCE = 1171
            NFINDOC.SOREDIR = 0
            NFINDOC.TRNDATE = NFINDOC.TRNDATE
            NFINDOC.FISCPRD = NFINDOC.TRNDATE.Year
            NFINDOC.PERIOD = NFINDOC.TRNDATE.Month
            Dim snum As Revera.SERIESNUM '= dbRevera.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
            NFINDOC.SERIESNUM = snum.SERIESNUM + 1
            'NFINDOC.FPRMS = 1001
            'NFINDOC.TFPRMS = 100
            Dim fmt As String = NFINDOC.FINCODE
            NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
            NFINDOC.BRANCH = 1000 '1
            NFINDOC.SODTYPE = 12 '11
            'NFINDOC.TRDR'?
            'NFINDOC.TRDBRANCH'?
            NFINDOC.VATSTS = 1 '?
            NFINDOC.SOCURRENCY = 100 '1
            NFINDOC.TRDRRATE = 1 '0
            NFINDOC.LRATE = 1
            NFINDOC.ORIGIN = 1
            NFINDOC.GLUPD = 0
            NFINDOC.SXUPD = 0
            NFINDOC.PRDCOST = 0
            NFINDOC.ISCANCEL = 0
            NFINDOC.ISPRINT = 0
            NFINDOC.ISREADONLY = 0
            NFINDOC.APPRVDATE = NFINDOC.TRNDATE '?
            NFINDOC.APPRVUSER = 99 '?
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
            NFINDOC.SOTIME = NFINDOC.TRNDATE '?
            NFINDOC.KEPYOHANDMD = 0 '?
            NFINDOC.GSISMD = 1 '?
            NFINDOC.GSISPACKAGES = 0 '?
            NFINDOC.INTVAL = 0 '?
            NFINDOC.INTVAT = 0 '?
            NFINDOC.BGDOCDATE = NFINDOC.TRNDATE '?
            NFINDOC.INSDATEN = NFINDOC.TRNDATE '?
            NFINDOC.INPAYVAT = 0 '?
            NFINDOC.BOOL01 = 0 '?
            NFINDOC.BOOL02 = 0 '?
        Catch ex As Exception

        End Try
        Return NFINDOC
        'Throw New NotImplementedException
    End Function
    Private Function ZeroMTRLINE(NMTRLINE As Revera.MTRLINE, NFINDOC As Revera.FINDOC) As Revera.MTRLINE
        Try
            'IS NULL
            'COMPANY,FINDOC,MTRLINES,LINENUM,SODTYPE,MTRL,PENDING,SOSOURCE,SOREDIR,MTRTYPE,SOTYPE,VAT,
            'QTY1, QTY2, QTY1COV, QTY1CANC, QTY1FCOV, LEXPVAL, NETLINEVAL, LNETLINEVAL, VATAMNT, LVATAMNT, EFKVAL, AUTOPRDDOC
            NMTRLINE.COMPANY = NFINDOC.COMPANY
            NMTRLINE.FINDOC = NFINDOC.FINDOC
            'NMTRLINE.MTRLINES = MTRLINES
            'NMTRLINE.LINENUM = LINENUM
            NMTRLINE.SODTYPE = 51 'NFINDOC.SODTYPE
            NMTRLINE.MTRL = 0
            NMTRLINE.PENDING = 0
            NMTRLINE.SOSOURCE = NFINDOC.SOSOURCE
            NMTRLINE.SOREDIR = 0
            NMTRLINE.MTRTYPE = 0
            NMTRLINE.SOTYPE = 0
            NMTRLINE.WHOUSE = 1000 '?
            NMTRLINE.MTRUNIT = 101 '?
            NMTRLINE.VAT = 1410 ' 0 'Not Null
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
            NMTRLINE.DELIVDATE = NFINDOC.TRNDATE '?
            NMTRLINE.WEIGHT = 0 '?
            NMTRLINE.VOLUME = 0 '?
            NMTRLINE.BGINTCOUNTRY = 1000 '?
            NMTRLINE.PRICE = 0 '?
            NMTRLINE.PRICE1 = 0 '?
            NMTRLINE.LINEVAL = 0 '?
            NMTRLINE.LLINEVAL = 0 '?
            NMTRLINE.EXPVAL = 0 '?
            NMTRLINE.LVATNOEXM = 0 '?
            NMTRLINE.TRNLINEVAL = 0 '?
            NMTRLINE.LNETLINEVAL = 0 '?
            NMTRLINE.SXPERC = 0 '?
            NMTRLINE.BGLEXCISE = 0 '?
            NMTRLINE.AUTOPRDDOC = 0 '?
        Catch ex As Exception

        End Try
        Return NMTRLINE
        'Throw New NotImplementedException
    End Function





#End Region

End Class