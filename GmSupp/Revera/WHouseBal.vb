Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Transactions
Imports GmSupp
Imports GmSupp.Hglp
Imports GmSupp.Revera
Imports Microsoft.AspNet.Identity
Imports OfficeOpenXml
Imports OfficeOpenXml.Style

Public Class WHouseBal
#Region "01-Declare Variables"
    Dim db As New DataClassesReveraDataContext
    'Dim dbHglp As New DataClassesHglpDataContext

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
    'Private ccCChief As List(Of UFTBL01)
    'Private ccCManager As List(Of UFTBL01)
    Private wfm As New WHouseBalFR
    Private CRole As String
    'Private Αpprovs As String
    Dim aHighers As String = Nothing

    Dim CUserName As String
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
        If Me.Tag = "SERTORIUS" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Revera"
            CompanyS = 5000
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

        If CurUser = "gmlogic" Then
            conString.DataSource = "192.168.12.201,55555"
            conString.InitialCatalog = "Revera"
            CompanyS = 5000 'SERTORIUS
        End If

        'conString.UserID = "sa"
        'conString.Password = "P@$$w0rd"
        conn = conString.ConnectionString
        'GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID

        'If CurUser = "g.igglesis" Then
        '    CompanyT = 1002
        'End If

        'If {"panagiotis", "katerina", "gkonstantatos"}.Contains(CurUser) Then
        '    CompanyT = 2001 '1001
        'End If


        UserManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString
        RoleManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString

        Dim uss = UserManager.Users.Where(Function(f) Not f.UserName = "gmlogic").OrderBy(Function(f) f.UserName).ToList
        'If CurUserRole = "Admins" Then
        '    uss = uss.Where(Function(f) f.S1User = False).OrderBy(Function(f) f.UserName).ToList
        '    Dim gg = uss.Select(Function(f) f.Roles).ToList
        'End If

        Dim emptyUsers() As EntityFramework.IdentityUser
        emptyUsers = {New EntityFramework.IdentityUser With {.UserName = "<Επιλέγξτε>"}}

        uss = (emptyUsers.ToList.Union(uss.ToList)).ToList

        Me.ddlUsers.DisplayMember = "UserName" 'ddlUsers.SelectedIndexChanged
        Me.ddlUsers.ValueMember = "Id"
        Me.ddlUsers.DataSource = uss 'ddlUsers.SelectedIndexChanged


        Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
        If Not IsNothing(cuser) Then
            CUserName = cuser.Name
            Me.GmChkListBoxAplicant.TlStxtBox.Text = CUserName
        End If

        Dim roles = UserManager.GetRoles(cuser.Id)
        Dim inRole = roles.Where(Function(f) {"2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου"}.Contains(f)).FirstOrDefault
        If inRole IsNot Nothing Then
            CRole = inRole
            aHighers = CUserName
        End If


        Me.GmChkListBoxFprms.TlStxtBox.Text = 1000
        SetGmChkListBox()

        Me.Panel1.Visible = False
        Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width
        Me.SplitContainer2.Panel2.Visible = False
        Me.PanelChUsers.Visible = False

        If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
            Me.MasterDataGridView.ContextMenuStrip = ContextMenuStrip1
            Me.TlSBtnCheckDetails.Visible = False
            Me.ToolStripSeparator8.Visible = False
            Me.TlSBtnUnCheckDetails.Visible = False
            Me.ToolStripSeparator9.Visible = False
            Me.TlSBtnHigherEnd.Visible = False
            VisibleHigher(False)
            Me.GroupBoxStatus.Visible = False
        End If

        If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
            Me.TlsBtnClear.Visible = False
            Me.toolStripSeparator.Visible = False
            'Me.SplitContainer2.Panel2.Visible = False
            'Dim ctlrs = Me.BindingNavigatorNewDoc.Controls.Cast(Of ToolStripItem).Where(Function(f) f.Tag = 1)
            'If isApplicant Then
            Me.BindingNavigatorNewDoc.Items.Cast(Of ToolStripItem).Where(Function(f) f.Tag = 1).ForEach(Sub(f As ToolStripItem) f.Visible = False)
            'End If
            Me.PanelChUsers.Visible = True
            Me.SplitContainer2.Panel2.Visible = True
            Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width - (Me.SplitContainer2.Width / 2)
            Me.MasterDataGridView.ContextMenuStrip = Me.ContextMenuStrip2
            inRole = roles.Where(Function(f) {"Developer", "Admins"}.Contains(f)).FirstOrDefault
            If inRole Is Nothing Then
                Me.lblUsers.Visible = False
                Me.ddlUsers.Visible = False
            End If
        End If

        'If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Or Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then

        'End If


        'If {"ΣΩΤΗΡΟΠΟΥΛΟΥ Κ", "ΑΘΑΝΑΣΙΟΥ Σ", "ΠΕΤΡΑΤΟΣ Λ", "mchalas", "ateli", "ΟΙΚΟΝΟΜΟΥ Π", "ΚΑΦΑΣ Γ"}.Contains(CurUser) Or {"Admins", "Managers"}.Contains(CurUserRole) Then
        '    Me.Panel1.Visible = True
        'End If
        If CurUserRole = "" Then

        End If
        If CurUser = "gmlogic" Then
            conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
            'Select Case conString.InitialCatalog
            '    Case "PFIC"
            '        CompanyT = 1002
            '    Case "LNK"
            '        CompanyT = 1001
            '    Case "LK"
            '        CompanyT = 2001 '1001
            'End Select
            Me.txtBoxMtrlCode.Text = "" '"1A231F401N3*"
            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                Me.txtBoxName.Text = "*ssd*"
            End If

            'Me.txtBoxFinCode.Text = "ΑIT0000050" '"ΠΑΡ0001593"
            Me.Panel1.Visible = True
        End If
        Me.KeyPreview = True


        For Each ff In Me.BindingNavigatorMaster.Items
            If If(ff.Tag, 0) = 1 Then
                ff.Visible = False
            End If
        Next

    End Sub

    Private Sub SetGmChkListBox()
        Dim emptyApplicant() As Revera.UFTBL01
        emptyApplicant = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

        Applicants = (emptyApplicant.ToList.Union(db.UFTBL01s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList

        Dim emptyFromDep() As Revera.UFTBL02
        emptyFromDep = {New Revera.UFTBL02 With {.NAME = "<Επιλέγξτε>", .UFTBL02 = 0}}

        FromDep = (emptyFromDep.ToList.Union(db.UFTBL02s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList

        Dim emptyTrdr() As Revera.TRDR
        emptyTrdr = {New Revera.TRDR With {.NAME = "<Επιλέγξτε>", .TRDR = 0}}

        db.Log = Console.Out

        'Dim gg = db.TRDRs.Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 13 And f.ISACTIVE = 1 And f.TRDEXTRA.BOOL01 = True And db.cccTrdDeps.
        '                                Select(Function(f1) f1.trdr).Contains(f.TRDR)).OrderBy(Function(f) f.NAME).ToList

        Trdrs = (emptyTrdr.ToList.Union(db.TRDRs.
                                        Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 13 And f.ISACTIVE = 1 And f.TRDEXTRA.BOOL01 = 1 And db.cccTrdDeps.
                                        Select(Function(f1) f1.trdr).Contains(f.TRDR)).OrderBy(Function(f) f.NAME).ToList)).ToList

        Dim emptycccTrdDep() As Revera.cccTrdDep
        emptycccTrdDep = {New Revera.cccTrdDep With {.Name = "<Επιλέγξτε>", .cccTrdDep = 0}}

        cccTrdDeps = (emptycccTrdDep.ToList.Union(db.cccTrdDeps.Where(Function(f) Trdrs.
                                        Select(Function(f1) f1.TRDR).Contains(f.trdr)).OrderBy(Function(f) f.Name).ToList)).ToList

        Dim emptySupplier() As Revera.TRDR
        emptySupplier = {New Revera.TRDR With {.NAME = "<Επιλέγξτε>", .TRDR = 0}}

        Suppliers = (emptySupplier.ToList.Union(db.TRDRs.
                                        Where(Function(f) f.COMPANY = CompanyS And f.SODTYPE = 12 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList


        'Dim emptyccCChief() As Revera.UFTBL01
        'emptyccCChief = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

        'ccCChief = emptyApplicant.ToList

        'Dim Highers = GetHighers(CurUser) ' As New Dictionary(Of String, String)
        'Highers.Add("<Επιλέγξτε>", 0)



        'Dim usClaims = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher") '.FirstOrDefault

        'For Each cl In usClaims
        '    Dim u1 = UserManager.FindById(cl.Value)
        '    Dim u As GmIdentityUser = GmUserManager.ChkUser(u1.UserName)
        '    Highers.Add(u.Name, u.Id)
        'Next


        'Me.TlsddlΗighers.ComboBox.DisplayMember = "Key"
        'Me.TlsddlΗighers.ComboBox.ValueMember = "Value"
        'Me.TlsddlΗighers.ComboBox.DataSource = Highers.ToList

        Dim Highers As New Dictionary(Of String, String)
        Highers.Add("<Επιλέγξτε>", 0)
        Dim usss = GmUserManager.Create(New GmIdentityDbContext).Users '.Where(Function(f)  f..OrderBy(Function(f) f.Name).ToList
        For Each u In usss.OrderBy(Function(f) f.Name).ToList
            If u.Name.Trim = "" Then
                Continue For
            End If
            Dim rol = UserManager.GetRoles(u.Id).ToList '.Where(Function(f) {"2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου"}.Contains(f))
            For Each f1 In rol.Where(Function(f) {"2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου"}.Contains(f))
                Highers.Add(u.Name, u.Id)
            Next
        Next

        Me.ddlΗighers.DropDownStyle = ComboBoxStyle.DropDownList
        Me.ddlΗighers.DisplayMember = "Key"
        Me.ddlΗighers.ValueMember = "Value"
        Me.ddlΗighers.DataSource = Highers.ToList

        Dim emptycCCManager() As Revera.UFTBL01
        emptycCCManager = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

        'ccCManager = emptycCCManager.ToList

        'Me.ApplicantNAMEComboBox.DataSource = q1.ToList

        Dim Result As Dictionary(Of Short, String) = db.UFTBL01s.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251).OrderBy(Function(f) f.NAME).ToDictionary(Function(f) f.UFTBL01, Function(f) f.NAME)


        Debug.Print("Values inserted into dictionary:")
        For Each dic As KeyValuePair(Of Short, String) In Result
            Debug.Print([String].Format("English salute {0} is {1} in German", dic.Key, dic.Value))
        Next

        'Me.GmChkListBoxAplicant.Height = 33
        Me.GmChkListBoxAplicant.dgv.DataSource = Result.ToList
        Me.GmChkListBoxAplicant.dgv_Styling()
        'Me.GmChkListBoxAplicant.BringToFront()

        Result = db.FPRMs.Where(Function(f) f.COMPANY = CompanyS And f.SOSOURCE = 1251).ToDictionary(Function(f) f.FPRMS, Function(f) f.NAME)
        'Me.GmChkListBoxFprms.Height = 33
        Me.GmChkListBoxFprms.dgv.DataSource = Result.ToList
        Me.GmChkListBoxFprms.dgv_Styling()
        'Me.GmChkListBoxFprms.BringToFront()

        Result = db.RESTMODEs.Where(Function(f) f.COMPANY = CompanyS).ToDictionary(Function(f) f.RESTMODE, Function(f) f.NAME)
        'Me.GmChkListBoxRestMode.Height = 33
        Me.GmChkListBoxRestMode.dgv.DataSource = Result.ToList
        Me.GmChkListBoxRestMode.dgv_Styling()
        'Me.GmChkListBoxRestMode.BringToFront()

        'If Not Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
        '    Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(New List(Of Revera.GetPendingOrdersDetailsResult))
        '    Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
        '    'MTRLINEsDataGridView_Styling()
        'End If


        wfm.DateTimePicker1.Enabled = False
        wfm.DateTimePicker1.Value = CTODate
        wfm.DateTimePicker2.Value = CTODate
        wfm.ddlTrdr.DropDownStyle = ComboBoxStyle.DropDownList
        wfm.ddlTrdr.DataSource = Trdrs
        wfm.cccTrdDeps = cccTrdDeps
        wfm.ddlApplicant.DataSource = Applicants
        'wfm.ddlSuppliers.DataSource = Suppliers
        wfm.ddlFromcccTrdDep.DropDownStyle = ComboBoxStyle.DropDownList
        wfm.ddlFromcccTrdDep.DataSource = FromDep

        Highers = GetHighers(CurUser)
        wfm.ddlΗighers.DropDownStyle = ComboBoxStyle.DropDownList
        wfm.ddlΗighers.DisplayMember = "Key"
        wfm.ddlΗighers.ValueMember = "Value"
        wfm.ddlΗighers.DataSource = Highers.ToList

        'wfm.ddlcCCManager.DataSource = ccCManager
        wfm.txtBoxVARCHAR01.Text = ""
        wfm.txtBoxREMARKS.Text = ""
        wfm.txtBoxRequestNo.Text = ""
    End Sub
    Private Function ChkHigher(highers As String, UName As String) As String
        Dim res As String = Nothing
        For Each h In highers.Split("|")
            If h = "" Then Continue For
            If h.Split(":")(0) = UName Then
                res = h.Split(":")(1)
                Exit For
            End If
        Next
        'VisibleHigher(False)
        'If res = "STB" Then
        '    VisibleHigher(True)
        'End If
        Return res
        'Throw New NotImplementedException()
    End Function
    Private Function GetHighers(curUser As String) As Dictionary(Of String, String)
        Dim Highers As New Dictionary(Of String, String)
        Highers.Add("<Επιλέγξτε>", 0)

        UserManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString

        'Dim cuser As GmIdentityUser = GmUserManager.ChkUser(curUser.Replace("gmlogic", "gm"))
        'If Not IsNothing(cuser) Then
        '    Me.GmChkListBoxAplicant.TlStxtBox.Text = cuser.Name
        'End If
        Dim uid = UserManager.FindByName(curUser).Id
        Dim usClaims = UserManager.GetClaims(uid).Where(Function(f) f.Type = "Higher") '.FirstOrDefault

        For Each cl In usClaims
            Dim u1 = UserManager.FindById(cl.Value)
            Dim u As GmIdentityUser = GmUserManager.ChkUser(u1.UserName)
            Highers.Add(u.Name, u.Id)
        Next
        Return Highers
        'Throw New NotImplementedException()
    End Function

    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.KeyCode = Keys.F4 Then
            Me.cmdPrint.PerformClick()
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
        Try

            Dim chkLists = From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                           Where ce.Cells("Check").Value = True

            If chkLists.Count = 0 Then
                Exit Sub
            End If

            'Dim grp = chkLists.GroupBy(Function(f) f.Cells("MTRL").Value)

            'If Not Me.chkBoxAuto.Checked AndAlso Not grp.Count = 1 Then
            '    MsgBox("Προσοχή !!! Επιλέγξατε διαφορετικά είδη.", MsgBoxStyle.Critical, "AddingNew")
            '    Exit Sub
            'End If

            'Dim chkFi As List(Of Integer) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
            '                                 Where ce.Cells("Check").Value = True
            '                                 Select CType(ce.Cells("Findoc").Value, Integer)).ToList

            Dim chkMt As List(Of String) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
                                            Where ce.Cells("Check").Value = True
                                            Select CType(ce.Cells(1).Value, String)).ToList

            'Dim chkLinesNo As List(Of Integer) = (From ce In Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow)
            '                                      Where ce.Cells("Check").Value = True
            '                                      Select CType(ce.Cells("LinesNo").Value, Integer)).ToList

            Dim lst As New List(Of Revera.GetWHouseBalanceResult)
            lst = CType(Me.MasterBindingSource.DataSource, SortableBindingList(Of Revera.GetWHouseBalanceResult)).ToList

            Dim mtrls = lst.Where(Function(f) chkMt.Contains(f.CODE)).ToList

            'If Me.VscsBindingSource.DataSource Is Nothing Then
            '    Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(New List(Of Revera.GetPendingOrdersDetailsResult))
            '    MTRLINEsDataGridView_Styling()
            'End If

            If Me.VscsBindingSource.DataSource Is Nothing Then
                Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)
            End If
            For Each v In mtrls
                Dim Codes As String() = {"10024"}
                addVsc = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) f.CODE = v.CODE).FirstOrDefault '.SingleOrDefault
                If Not IsNothing(addVsc) Then
                    If MsgBox("Προσοχή !!!.Καταχωρημένος κωδικός: " & v.CODE, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "Προσοχή !!!") = MsgBoxResult.Ok Then
                        addVsc = Nothing
                    End If
                End If
                If IsNothing(addVsc) Then
                    Dim det = New Revera.GetPendingOrdersDetailsResult
                    det.MTRL = v.MTRL
                    det.CODE = v.CODE
                    det.NAME = v.NAME
                    det.REMARKS = v.REMARKS
                    'det.WHOUSE = 1000 '?
                    det.MTRUNIT = v.MTRUNIT1 '?
                    det.VAT = v.VAT ' 0 'Not Null

                    det.MTRUNITC = v.MTRUNITC
                    det.SODTYPE = v.SODTYPE
                    'UFTBL02, 106
                    'cccTrdDep, 55
                    'cccTrdr, 35465
                    addVsc = det
                    Me.VscsBindingSource.AddNew()
                End If

            Next
            If Me.MTRLINEsDataGridView.ColumnCount = 0 Then
                Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
                MTRLINEsDataGridView_Styling()
            End If

            For Each row As DataGridViewRow In chkLists
                'Set colors
                row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
            Next
            Me.TlSBtnUnCheck.PerformClick()

            Me.BindingNavigatorSaveItem.Enabled = True
            Me.SplitContainer2.Panel2.Visible = True
            Me.SplitContainer2.SplitterDistance = Me.SplitContainer2.Width - (Me.SplitContainer2.Width / 4)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            Dim cOMPANY As System.Nullable(Of Short)
            Dim sODTYPE As System.Nullable(Of Short)
            Dim wHOUSE As String
            Dim mTRACN As String
            Dim dFROM As System.Nullable(Of Date)
            Dim dTO As System.Nullable(Of Date)
            Dim fISCPRD As System.Nullable(Of Integer)
            Dim pERIOD As System.Nullable(Of Integer)
            Dim sOSOURCE As System.Nullable(Of Integer)
            Dim fPRMS As String
            Dim tFPRMS As String
            Dim tRDBUSINESS As String
            Dim iSCANCEL As System.Nullable(Of Short)
            Dim fULLYTRANSF As String
            Dim sLCODE As String
            Dim tRDRCODE As String
            Dim sLGROUP As String
            Dim fINCODE As String
            Dim aPplicant As String

            Dim aPending As String
            Dim rESTMODE As String
            Dim fOrderNo As Integer = 0
            Dim tOrderNo As Integer = 2147483647
            'Dim RequestNo As Integer?


            mTRL = 0 'Nothing
            mTRLCODE = If(Me.txtBoxMtrlCode.Text.Trim = "", Nothing, Me.txtBoxMtrlCode.Text.Replace("*", "%").Trim)
            nAME = If(Me.txtBoxName.Text.Trim = "", Nothing, Me.txtBoxName.Text.Replace("*", "%").Trim)
            rEMARKS = If(Me.txtBoxRemarks.Text.Trim = "", Nothing, Me.txtBoxRemarks.Text.Replace("*", "%").Trim)
            cOMPANY = CompanyS
            If Me.radioBtnMtrl.Checked Then
                sODTYPE = 51
            Else
                sODTYPE = 52
            End If

            'wHOUSE = Me.TlSTxtWHOUSE.Text
            mTRACN = "101,102,103,105" 'Λογιστική κατηγορία
            dFROM = Me.DateTimePicker1.Value.ToShortDateString
            dTO = Me.DateTimePicker2.Value.ToShortDateString
            fISCPRD = Me.DateTimePicker1.Value.Year
            pERIOD = Me.DateTimePicker1.Value.Month
            'sOSOURCE =1251' 1351
            'fPRMS = Me.TlSTxtFPRMS.Text
            tFPRMS = Me.txtBoxMtrlCode.Text
            'tRDBUSINESS = Me.TlSTxtΤRDBUSINES.Text
            iSCANCEL = 0
            'fULLYTRANSF = Me.TlSTxtFULLYTRANSF.Text
            'sLCODE = Me.TlSTxtPRSN.Text
            'tRDRCODE = Me.TlSTxtTRDR.Text.Replace("*", "%").Trim
            sLGROUP = ""
            fINCODE = If(Me.txtBoxFinCode.Text.Trim = "", Nothing, Me.txtBoxFinCode.Text.Replace("*", "%").Trim)


            fPRMS = If(Me.GmChkListBoxFprms.TlStxtBox.Text.Trim = "", Nothing, Me.GmChkListBoxFprms.TlStxtBox.Text)
            rESTMODE = If(Me.GmChkListBoxRestMode.TlStxtBox.Text.Trim = "", Nothing, Me.GmChkListBoxRestMode.TlStxtBox.Text)
            CUserName = Me.GmChkListBoxAplicant.TlStxtBox.Text
            aPplicant = If(CUserName.Trim = "", Nothing, CUserName)

            'aHighers = Nothing
            'If Me.ddlΗighers.SelectedItem IsNot Nothing Then
            '    If Not Me.ddlΗighers.SelectedItem.Key = "<Επιλέγξτε>" Then
            '        aHighers = Me.ddlΗighers.SelectedItem.Key
            '    End If
            'Else
            '    If Not Me.ddlΗighers.Text = "<Επιλέγξτε>" Then
            '        aHighers = Me.ddlΗighers.Text
            '    End If
            'End If
            'Κεσκεσιάδης Χρήστος:OK|GmLogic:STB
            If aHighers IsNot Nothing Then
                aHighers = "%" & aHighers.Replace("%", "") & "%"
            End If

            fOrderNo = If(Me.txtBoxOrderNo.Text.Trim = "", Nothing, Me.txtBoxOrderNo.Text)
            If fOrderNo > 0 Then
                tOrderNo = fOrderNo
            End If


            aPending = ""

            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                'Dim res As New List(Of Revera.GetWHouseBalanceResult)
                Dim Res = db.GetWHouseBalance(cOMPANY, sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList
                Me.MasterBindingSource.DataSource = New SortableBindingList(Of Revera.GetWHouseBalanceResult)(Res)
            End If
            'If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
            If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
                Dim res As IMultipleResults = db.GetPendingOrders(cOMPANY, mTRLCODE, 0, fOrderNo, tOrderNo, fINCODE, fPRMS, rESTMODE, aPplicant, aHighers, aPending, dFROM, dTO)
                Dim POrdHead = res.GetResult(Of Revera.GetPendingOrdersHeaderResult).ToList
                Dim POrdDet = res.GetResult(Of Revera.GetPendingOrdersDetailsResult).ToList
                If POrdHead.Count = 0 Then
                    'aPplicant = Nothing
                    'Dim res2 As IMultipleResults = db.GetPendingOrders(cOMPANY, mTRLCODE, 0, fOrderNo, tOrderNo, fINCODE, fPRMS, rESTMODE, aPplicant, aHighers, aPending, dFROM, dTO)
                    'Dim POrdHead2 = res2.GetResult(Of Revera.GetPendingOrdersHeaderResult).ToList
                    'Dim POrdDet2 = res2.GetResult(Of Revera.GetPendingOrdersDetailsResult).ToList
                    'If POrdHead.Count = 0 And POrdHead2.Count = 0 Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Προσοχή !!! Δεν βρέθηκαν εγγραφές.", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                'POrdHead = POrdHead.Union(POrdHead2).ToList
                'POrdDet = POrdDet.Union(POrdDet2).ToList
                'End If

                'Dim Applicant As Short?
                'Applicant = Me.ApplicantNAMEComboBox.SelectedValue
                'If Not Applicant = 0 Then
                '    POrdHead = POrdHead.Where(Function(f) If(f.Applicant, 0) = If(Applicant, 0)).ToList
                'End If
                'Dim v2 = POrdHead.FirstOrDefault.Highers


                'Dim Highers As New Dictionary(Of String, String)
                'Highers.Add("<Επιλέγξτε>", 0)

                'Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
                'If Not IsNothing(cuser) Then
                '    'Me.txtBoxName.Text = cuser.Name
                'End If

                'Dim usClaims = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher") '.FirstOrDefault

                'For Each cl In usClaims
                '    Dim u1 = UserManager.FindById(cl.Value)
                '    Dim u As GmIdentityUser = GmUserManager.ChkUser(u1.UserName)
                '    Highers.Add(u.Name, u.Id)
                'Next
                ''If v2 IsNot Nothing Then
                ''    Me.TlsddlΗighers.Text = v2.Split(":")(0)
                ''End If


                If Not Me.txtBoxMtrlCode.Text.Trim = "" Then
                    POrdDet = POrdDet.Where(Function(f) f.CODE Like mTRLCODE.Replace("%", "*")).ToList
                End If

                If Not Me.RadioBtnAll.Checked Then
                    If Me.RadioBtnToAproved.Checked Then
                        POrdHead = POrdHead.Where(Function(f) f.Highers IsNot Nothing AndAlso (System.Text.RegularExpressions.Regex.Matches(f.Highers, "STB").Count = 1 And Not f.Highers.Contains("OK"))).ToList
                    End If
                    If Me.RadioBtnPending.Checked Then
                        POrdHead = POrdHead.Where(Function(f) f.Highers IsNot Nothing AndAlso (f.Highers.Contains("STB") And f.Highers.Contains("OK"))).ToList
                    End If
                    If Me.RadioBtnApproved.Checked Then
                        POrdHead = POrdHead.Where(Function(f) f.Highers IsNot Nothing AndAlso Not f.Highers.Contains("STB")).ToList
                    End If

                End If
                'Dim POrds As New List(Of Revera.GetPendingOrdersDetailsResult)
                'POrds = (From hd In POrdHead Join dt In POrdDet On hd.NO_ Equals dt.NO_
                '         Select New Revera.GetPendingOrdersDetailsResult With {.NO_ = hd.NO_,
                '            .TRNDATE = hd.TRNDATE, .FINCODE = hd.FINCODE, .ApplicantNAME = hd.ApplicantNAME,
                '            .INSUSERNAME = hd.INSUSERNAME, .FPRMSNAME = hd.FPRMSNAME, .TRDRCODE = hd.CODE,
                '            .TRDRNAME = hd.NAME,
                '            .CODE = dt.CODE, .NAME = dt.NAME, .cccTrdr = dt.cccTrdr, .cccTrdDep = dt.cccTrdDep, .UFTBL02 = dt.UFTBL02,
                '            .QTY1 = dt.QTY1, .QTY1CANC = dt.QTY1CANC, .QTY1OPEN = dt.QTY1OPEN}
                '            ).ToList

                Me.MasterBindingSource.DataSource = POrdHead ' New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(POrds)
                If Me.MasterBindingSource.Current IsNot Nothing Then
                    Me.VscsBindingSource.DataSource = POrdDet

                    Dim findoc As Integer = Me.MasterBindingSource.Current.FINDOC
                    'Dim Res = db.ccCVMtrLines.Where(Function(f) f.FINDOC = findoc).ToList
                    Dim res1 = CType(Me.VscsBindingSource.DataSource, List(Of GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc).ToList
                    'Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.ccCVMtrLine)(Res)
                    Me.MTRLINEsDataGridView.DataSource = res1 'Me.VscsBindingSource
                    Me.BindingNavigatorNewDoc.BindingSource = New BindingSource With {.DataSource = res1}
                    MTRLINEsDataGridView_Styling()
                    'Me.MasterBindingSource.Position = -1
                    MasterBindingSource_PositionChanged(Me.MasterBindingSource, Nothing)
                End If

            End If


            'If Me.Text = "Αποθήκη - Αιτήσεις σε Εκρεμότητα" Then 'Obsolete
            '    'Dim res As New List(Of Revera.GetWHouseBalanceResult)
            '    'Dim Res = db.GetWHouseBalance(cOMPANY, sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList

            '    'Dim Res = db.ccCVMtrLines.Where(Function(f) f.COMPANY = cOMPANY And f.SOSOURCE = 1251 And fPRMS.Split(",").Contains(f.SERIES) And f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value).ToList ' And ., sODTYPE, mTRLCODE, nAME, Nothing, Nothing, Nothing, 0, 0, 0, rEMARKS, 0, 0, dFROM, dTO).ToList
            '    Dim Res = db.ccCVMtrLines.Where(Function(f) f.COMPANY = cOMPANY And f.SOSOURCE = 1251 And fPRMS.Split(",").Contains(f.SERIES) And f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value).
            '        Select(Function(f) New With {f.FINDOC, f.FINCODE, f.TRNDATE, f.RequestNo, f.UFTBL01, f.uf1Name, f.uf2Name, f.tdName, f.cTDpName}).Distinct.ToList
            '    'If Not Αpprovs = "<Επιλέγξτε>" Then
            '    '    Res = Res.Where(Function(f) Not f.VARCHAR02 Is Nothing AndAlso f.VARCHAR02.Contains(Αpprovs)).ToList
            '    'End If
            '    'If Not GmChkListBoxAplicant.TlStxtBox.Text = "" Then
            '    '    Dim Applicants As List(Of String) = Me.GmChkListBoxAplicant.TlStxtBox.Text.Split(",").ToList
            '    '    Res = Res.Where(Function(f) Applicants.Contains(f.UFTBL01.Value.ToString)).ToList
            '    'End If

            '    If aPplicant IsNot Nothing Then
            '        Res = Res.Where(Function(f) aPplicant.Split(",").Contains(f.UFTBL01)).ToList
            '    End If
            '    If Not Me.txtBoxFinCode.Text.Trim = "" Then
            '        Res = Res.Where(Function(f) f.FINCODE Like fINCODE.Replace("%", "*")).ToList
            '    End If
            '    'If Not Me.txtBoxMtrlCode.Text.Trim = "" Then
            '    '    Res = Res.Where(Function(f) f.CODE Like mTRLCODE.Replace("%", "*")).ToList
            '    'End If
            '    If Not Me.txtBoxOrderNo.Text.Trim = "" Then
            '        Res = Res.Where(Function(f) f.FINDOC = CInt(Me.txtBoxOrderNo.Text)).ToList
            '    End If

            '    'myArrF = ("NO_,TRNDATE,RequestNo,UFTBL01,uf1Name,uf2Name,tdName,cTDpName,CODE,mtName,muName,QTY1,IMPEXPQTY1,SUMORDERED,SUMRESERVED,MTRLBAL,REMAINLIMMIN,REMAINLIMMAX,REORDERLEVEL,REMARKS,mtRemarks,VARCHAR02,FINCODE").Split(",")
            '    'myArrN = ("A/A,Ημερ/νία,Αίτηση,ΑιτώνΚωδ,Αιτών,Από Τμήμα,Για Πελάτη,Για Τμήμα Πελάτη,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Υπολ.Πραγμ.,Αναμενόμενα,Δεσμευμένα,ΥΠΟΛΟΙΠΟ(υ+α-δ),Ελάχιστο όριο,Μέγιστο όριο,Όριο ανα παραγγελία,Παρατηρήσεις,Παρατηρήσεις,Εγκρίσεις,Παραστατικό").Split(",")

            '    'Dim q = From g1 In Res Group g1 By g1.FINDOC Into Group
            '    '        Select Group.FirstOrDefault.ccCChief, Group.FirstOrDefault.TRNDATE, Group.FirstOrDefault.cCCManager, Group.FirstOrDefault.cccTrdr ', Group.FirstOrDefault.FINCODE

            '    'Dim q = From g1 In Res Group g1 By g1.FINDOC Into Group
            '    '        Select Group.FirstOrDefault.TRNDATE,
            '    '           RequestNo_ = If(Group.FirstOrDefault.RequestNo.HasValue, Group.FirstOrDefault.RequestNo, 0),
            '    '            Group.FirstOrDefault.UFTBL01,
            '    '            Group.FirstOrDefault.uf1Name,
            '    '            Group.FirstOrDefault.uf2Name,
            '    '            Group.FirstOrDefault.tdName,
            '    '            Group.FirstOrDefault.cTDpName

            '    Dim q = Res.Select(Function(f) New With {f.TRNDATE, f.FINDOC, f.UFTBL01, f.uf1Name, f.uf2Name, f.tdName, f.cTDpName}).Distinct.ToList

            '    Me.MasterBindingSource.DataSource = Res ' New SortableBindingList(Of Revera.ccCVMtrLine)(Res)

            'End If

            'If Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then

            '    Dim mtrls = dbHglp.MTRLs.Where(Function(f) f.ccCCode IsNot Nothing)

            '    Dim lbs As New List(Of BCLabel)
            '    For Each ml In mtrls
            '        Dim lb As New BCLabel
            '        lb.mtrl = ml.MTRL
            '        lb.code = ml.CODE
            '        lb.Name = ml.NAME
            '        Dim dd As New ccCDescr
            '        dd.LBName = "ΑΝ 33,5"
            '        dd.Pack = 10
            '        dd.Weight = 10
            '        Dim gg = Newtonsoft.Json.JsonConvert.SerializeObject(dd)

            '        Dim gg1 = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ccCDescr)(gg)

            '        lb.LBName = gg1.LBName
            '        lb.Pack = gg1.Pack
            '        lb.Weight = gg1.Weight

            '        lbs.Add(lb)
            '    Next

            '    Me.MasterBindingSource.DataSource = New SortableBindingList(Of BCLabel)(lbs)
            'End If

            'If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Or Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then

            'End If

            Me.MasterDataGridView.DataSource = Me.MasterBindingSource

            MasterDataGridView_Styling()
            ''If Me.VscsBindingSource.DataSource Is Nothing Then
            'Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(New List(Of Revera.GetPendingOrdersDetailsResult))
            'Me.MTRLINEsDataGridView.DataSource = Me.VscsBindingSource
            'MTRLINEsDataGridView_Styling()
            ''End If

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
                    db = New DataClassesReveraDataContext(conn)
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
                    db = New DataClassesReveraDataContext(conn)
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
                    Dim NFINDOC As New Revera.FINDOC ' = db.GetChangeSet.Inserts.Where(Function(f) f.GetType.FullName = "GmSupp.Revera.FINDOC").SingleOrDefault
                    If Not db.GetChangeSet.Deletes.Count = 0 Then
                        '' Delete
                        For Each deleted As Object In db.GetChangeSet.Deletes
                            If deleted.GetType.ToString.Contains("FINDOC") Then
                                'Dim DMTRLINE As MTRLINE = deleted
                                Dim DFINDOC As Revera.FINDOC = deleted 'DMTRLINE.FINDOC1
                                'db.FINDOCs.DeleteOnSubmit(DFINDOC)
                                'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                                Dim snum As Revera.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = DFINDOC.COMPANY And f.SOSOURCE = DFINDOC.SOSOURCE And f.SERIES = DFINDOC.SERIES And f.FISCPRD = DFINDOC.FISCPRD).FirstOrDefault
                                If snum.SERIESNUM = DFINDOC.SERIESNUM Then
                                    snum.SERIESNUM -= 1
                                End If

                            End If
                        Next
                    Else
                        If Not db.GetChangeSet.Inserts.Count = 0 Then
                            'Dim TFINDOC As FINDOC = Me.MasterBindingSource.Current
                            Dim MTRLINES As Integer = 0
                            If NFINDOC.FINDOC > 0 Then
                                MTRLINES = db.MTRLINEs.Where(Function(f) f.FINDOC = NFINDOC.FINDOC).Max(Function(f) f.MTRLINES)
                            End If
                            For Each insertion As Object In db.GetChangeSet.Inserts
                                If insertion.GetType.ToString.Contains("FINDOC") Then
                                    NFINDOC = insertion
                                    'COMPANY=:1 AND SOSOURCE=:2 AND SERIES=:3 AND FISCPRD=:4-- 1, 1151, 1010,2014:
                                    Dim snum As Revera.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
                                    snum.SERIESNUM += 1
                                    NFINDOC.SERIESNUM = snum.SERIESNUM
                                    Dim fmt As String = "ΑIT0000000"
                                    NFINDOC.FINCODE = NFINDOC.SERIESNUM.ToString(fmt)
                                    NFINDOC.INSUSER = 8888
                                    NFINDOC.INSDATE = Now()
                                End If
                                If insertion.GetType.ToString.Contains("MTRLINE") Then
                                    Dim NMTRLINE As Revera.MTRLINE = insertion
                                    MTRLINES += 1
                                    NMTRLINE.MTRLINES = MTRLINES 'NFINDOC.MTRLINEs.Count 
                                    NMTRLINE.LINENUM = NMTRLINE.MTRLINES
                                End If
                            Next
                        End If
                    End If
                    If Not db.GetChangeSet.Updates.Count = 0 Then
                        For Each Changes As Object In db.GetChangeSet.Updates
                            If Changes.GetType.ToString.Contains("FINDOC") Then
                                NFINDOC = Changes
                                NFINDOC.UPDUSER = 99
                                NFINDOC.UPDDATE = Now()
                            End If
                            If Changes.GetType.ToString.Contains("MTRLINE") Then
                                Dim NMTRLINE As Revera.MTRLINE = Changes
                            End If
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
    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If s.Columns(s.CurrentCell.ColumnIndex).Name = "Search_Code" Then
            Exit Sub
        End If

        If s.Columns(s.CurrentCell.ColumnIndex).Name = "CodeExp" Then
            Exit Sub
        End If

        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    'Dim editableFields_MTRLINEsDataGridView() As String = gg1()

    Private Function editableFields_MTRLINEsDataGridView() As String()
        If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
            Return {"NUM03", "COMMENTS1"}
        End If

        If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
            Return {"QTY1", "COMMENTS1"}
        End If

        'Throw New NotImplementedException()
    End Function

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

            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                myArrF = ("CODE,NAME,MTRUNITC,MTRPLACE,IMPEXPQTY1,SUMORDERED,SUMRESERVED,MTRLBAL,REMAINLIMMIN,REMAINLIMMAX,REORDERLEVEL,REMARKS").Split(",")
                myArrN = ("Κωδικός,Περιγραφή,Μ.Μ,Συνήθης θέση αποθ.,Υπολ.Πραγμ.,Αναμενόμενα,Δεσμευμένα,ΥΠΟΛΟΙΠΟ(υ+α-δ),Ελάχιστο όριο,Μέγιστο όριο,Όριο ανα παραγγελία,Παρατηρήσεις Είδους").Split(",")
            End If

            'If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
            If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
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
                If CurUserRole = "Admins" Then
                    'myArrF = ("NO_,FINDOC,TRNDATE,FINCODE,ApplicantNAME,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME,CODE,NAME,MTRUNITC,QTY1,QTY1CANC,QTY1OPEN,OrderNo,Highers,FINSTATES,FINSTATESNAME").Split(",")
                    'myArrN = ("A/A,Αρ.Αίτησης,Ημερ/νία,Παραστατικό,Αιτών,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1,Παλ.Αριθ,Εγκρίνοντες,Κατάσταση,Περιγραφή").Split(",")
                    myArrF = ("NO_,TRNDATE,FINCODE,ApplicantNAME,Highers,FINSTATESNAME,AssignmentUser,FINDOC,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME,CODE,NAME,MTRUNITC,QTY1,QTY1CANC,QTY1OPEN,OrderNo").Split(",")
                    myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αιτών,Εγκρίνοντες,Κατάσταση,Ανάθεση,FinDoc,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1,Παλ.Αριθ").Split(",")
                Else
                    myArrF = ("NO_,TRNDATE,FINCODE,ApplicantNAME,Highers,FINSTATESNAME,AssignmentUser").Split(",")
                    myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αιτών,Εγκρίνοντες,Περιγραφή,Ανάθεση").Split(",")
                End If
            End If
            'If Me.Text = "Αποθήκη - Αιτήσεις σε Εκρεμότητα" Then
            '    'myArrF = ("NO_,TRNDATE,FINCODE,uf1Name,mtName,muName,MTRPLACE,OrderNo,INSUSERNAME,FPRMSNAME,TRDRCODE,TRDRNAME,CODE,NAME,MTRUNITC,QTY1,QTY1CANC,QTY1OPEN").Split(",")
            '    'myArrN = ("A/A,Ημερ/νία,Παραστατικό,Αιτών,Περιγραφή,Μ.Μ,Συνήθης θέση αποθ.,Αρ.Αίτησης,Χρήστης εισαγωγής,Τύπος,Κωδικός,Επωνυμία,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Ακυρ.Ποσ.,Εκκρεμή.Ποσ.1").Split(",")
            '    myArrF = ("NO_,TRNDATE,FINDOC,UFTBL01,uf1Name,uf2Name,tdName,cTDpName,CODE,mtName,muName,QTY1,IMPEXPQTY1,SUMORDERED,SUMRESERVED,MTRLBAL,REMAINLIMMIN,REMAINLIMMAX,REORDERLEVEL,REMARKS,mtRemarks,VARCHAR02,FINCODE").Split(",")
            '    myArrN = ("A/A,Ημερ/νία,Αρ.Αίτησης,ΑιτώνΚωδ,Αιτών,Από Τμήμα,Για Πελάτη,Για Τμήμα Πελάτη,Κωδικός,Περιγραφή,Μ.Μ,Ποσ.1,Υπολ.Πραγμ.,Αναμενόμενα,Δεσμευμένα,ΥΠΟΛΟΙΠΟ(υ+α-δ),Ελάχιστο όριο,Μέγιστο όριο,Όριο ανα παραγγελία,Παρατηρήσεις,Παρατηρήσεις,Εγκρίσεις,Παραστατικό").Split(",")
            'End If

            'If Me.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" Then
            '    myArrF = ("CODE,NAME,LBName,Pack,Weight,PrdDate,PackingDate,Shift,Shift").Split(",")
            '    myArrN = ("Κωδικός,Περιγραφή,Ονομασία Λιπάσματος,Τύπος Συσκευασίας,Βάρος σάκου,Ημ/νία Παραγωγής,Ημ/νία Συσκευασίας,Βάρδια,Μηχανή ενσάκισης").Split(",")
            'End If

            'If Me.Text = "Αποθήκη - Περιγραφή Ετικέτας" Then
            '    myArrF = ("CODE,NAME,LBName,Pack,Weight").Split(",")
            '    myArrN = ("Κωδικός,Περιγραφή,Ονομασία Λιπάσματος,Τύπος Συσκευασίας,Βάρος σάκου").Split(",")
            'End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For Each ff In Me.BindingNavigatorMaster.Items
                If If(ff.Tag, 0) = 1 Then
                    ff.Visible = False
                End If
            Next
            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then

                For Each ff In Me.BindingNavigatorMaster.Items
                    If {"ToolStripSeparator1", "ExcelToolStripButton", "ToolStripSeparator7", "cmdPrint"}.Contains(ff.Name) Then
                        Continue For
                    End If
                    If If(ff.Tag, 0) = 1 Then
                        ff.Visible = True
                    End If
                Next
                AddOutOfOfficeColumn(Me.MasterDataGridView)
            Else
                Dim ff = Me.BindingNavigatorMaster.Items.Find("cmdPrint", True).FirstOrDefault
                ff.Visible = True
            End If

            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next

            Me.MasterDataGridView.AutoResizeColumns()


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
                                If Col.DataPropertyName = "TRNDATE" Then
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

            For Each row As DataGridViewRow In MasterDataGridView.Rows
                Dim head As GetPendingOrdersHeaderResult = row.DataBoundItem
                If Not IsNothing(head) Then
                    Try
                        'Set colors
                        '10  Εγκεκριμένη
                        '16  Αίτηση πρός έγκριση
                        '17  Αρχική έγκριση
                        '18  Αίτηση πρός έγκριση ανωτέρου
                        'If head.FINSTATES = 17 Then
                        '    row.DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                        'End If
                        'If head.FINSTATES = 18 Then
                        '    row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                        'End If
                        'If head.FINSTATES = 10 Then
                        '    row.DefaultCellStyle.BackColor = System.Drawing.Color.LimeGreen
                        'End If
                        If head.Highers IsNot Nothing Then
                            Dim higs = head.Highers.Split("|")
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.Empty
                            If head.Highers.Contains("OK") And head.Highers.Contains("STB") Then
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                            End If
                            If head.Highers.Contains("OK") And Not head.Highers.Contains("STB") Then
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.LimeGreen
                            End If
                        End If

                        'Dim res As String = Nothing
                        'Dim HighCount = 0
                        'For Each h In head.Highers.Split("|")
                        '    If h = "" Then Continue For
                        '    HighCount += 1
                        '    If h.Split(":")(0) = aHighers.Replace("%", "") Then
                        '        res = h.Split(":")(1)
                        '        HighCount = 0
                        '    End If
                        'Next
                        'If HighCount = 0 Then
                        '    If res = "OK" Then
                        '        row.DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                        '    End If
                        'End If

                        'If head.FINSTATES = 10 Then
                        '    row.DefaultCellStyle.BackColor = System.Drawing.Color.LimeGreen
                        'End If

                    Catch ex As Exception

                    End Try
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MasterDataGridView_Sorted(sender As Object, e As EventArgs) Handles MasterDataGridView.Sorted
        MasterDataGridView_Styling()
    End Sub

    Private Sub MTRLINEsDataGridView_Styling()
        Try

            Me.MTRLINEsDataGridView.AutoGenerateColumns = True

            'ΚΩΔΙΚΟΣ SFT 1		ΠΟΣΟΤΗΤΑ	MM	ΠΕΡΙΓΡΑΦΗ 			
            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                myArrF = ("CODE,NUM03,MTRUNITC,NAME,REMARKS,COMMENTS1,FINDOC").Split(",")
                myArrN = ("Κωδικός,Αιτ.Ποσ,Μ.Μ,Περιγραφή,Παρατηρήσεις Είδους,Παρατηρήσεις,FinDoc").Split(",")
            End If
            If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
                If CurUserRole = "Admins" Then
                    myArrF = ("CODE,NUM03,QTY1,MTRUNITC,NAME,REMARKS,COMMENTS1,ApplicationLog,FINDOC,MTRLINES,ccCAFINDOC,ccCAMTRLINES").Split(",")
                    myArrN = ("Κωδικός,Αιτ.Ποσ,Εγκρ.Ποσ,Μ.Μ,Περιγραφή,Παρατηρήσεις Είδους,Παρατηρήσεις,Ιστορικό Αίτησης,FinDoc,MtrLines,ccCAFINDOC,ccCAMTRLINE").Split(",")
                Else
                    myArrF = ("CODE,NUM03,QTY1,MTRUNITC,NAME,REMARKS,COMMENTS1,ApplicationLog").Split(",")
                    myArrN = ("Κωδικός,Αιτ.Ποσ,Εγκρ.Ποσ,Μ.Μ,Περιγραφή,Παρατηρήσεις Είδους,Παρατηρήσεις,Ιστορικό Αίτησης").Split(",")
                End If
            End If

            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MTRLINEsDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)


            For i As Integer = 0 To MTRLINEsDataGridView.Columns.Count - 1
                Debug.Print(MTRLINEsDataGridView.Columns(i).DataPropertyName & vbTab & MTRLINEsDataGridView.Columns(i).Name)
                MTRLINEsDataGridView.Columns(i).ReadOnly = True
            Next
            For Each edf In editableFields_MTRLINEsDataGridView()
                Dim Col As DataGridViewColumn = Utility.GetNoColumnDataGridView(Me.MTRLINEsDataGridView, edf)
                If Not IsNothing(Col) Then
                    Col.ReadOnly = False
                End If
            Next


            If Not IsNothing(MTRLINEsDataGridView.Columns("Παρατηρήσεις")) Then
                'MTRLINEsDataGridView.Columns("Παρατηρήσεις").Width = 460
                MTRLINEsDataGridView.Columns("Παρατηρήσεις").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            End If
            If Not IsNothing(MTRLINEsDataGridView.Columns("Παρατηρήσεις Είδους")) Then
                'MTRLINEsDataGridView.Columns("Παρατηρήσεις").Width = 460
                MTRLINEsDataGridView.Columns("Παρατηρήσεις Είδους").DefaultCellStyle.WrapMode = DataGridViewTriState.True
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

            'Me.MTRLINEsDataGridView.AutoResizeRows()
            Me.MTRLINEsDataGridView.AutoResizeColumns()

        Catch ex As Exception

        End Try
    End Sub

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

    Private Sub TlSBtnCheckDetails_Click(sender As Object, e As EventArgs) Handles TlSBtnCheckDetails.Click, TlSBtnUnCheckDetails.Click
        Dim s As ToolStripButton = sender
        Dim findoc As Integer = Me.MasterBindingSource.Current.FINDOC
        'Dim Res = db.ccCVMtrLines.Where(Function(f) f.FINDOC = findoc).ToList
        If Me.MTRLINEsDataGridView.Rows.Count > 0 Then
            Dim res = CType(Me.VscsBindingSource.DataSource, List(Of GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc).ToList
            Me.BindingNavigatorSaveItem.Enabled = False
            For Each v In res
                If s.Name = "TlSBtnCheckDetails" Then
                    v.QTY1 = v.NUM03   'Εγκρ.Ποσ.1
                    Me.BindingNavigatorSaveItem.Enabled = True
                Else
                    v.QTY1 = 0
                End If
            Next
            Me.MTRLINEsDataGridView.Refresh()
        End If

    End Sub

    Private Sub TlSBtnHigherEnd_Click(sender As Object, e As EventArgs) Handles TlSBtnHigherEnd.Click
        If MsgBox("Προσοχή!!! Τελική έγκριση. Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
            Exit Sub
        End If
        Dim s As ToolStripButton = sender
        Dim findoc As Integer = Me.MasterBindingSource.Current.FINDOC
        Dim fin1 = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
        If fin1 Is Nothing Then
            MsgBox("Error findoc=" & findoc, MsgBoxStyle.Critical, "db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault")
            Exit Sub
        End If
        'Dim Qtys = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) f.QTY1 = 0).FirstOrDefault
        Dim Qtys = CType(Me.VscsBindingSource.DataSource, List(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc And f.QTY1 = 0).FirstOrDefault
        If Qtys IsNot Nothing Then
            If MsgBox("Προσοχή !!! Λάθος ποσότητα = 0", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        Dim hs As String = ChkHigher(fin1.VARCHAR02, CUserName)
        'If hs IsNot Nothing And hs = "STB" Then
        Dim res = CType(Me.VscsBindingSource.DataSource, List(Of GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc).ToList

        For Each v In res
            'If Not v.QTY1 = v.NUM03 Then 'Εγκρ.Ποσ.1
            Dim mtl = db.MTRLINEs.Where(Function(f) f.FINDOC = v.FINDOC And f.MTRLINES = v.MTRLINES And Not f.QTY1 = v.QTY1).FirstOrDefault

            If mtl IsNot Nothing Then
                'If If(v.NUM03, 0) = 0 Then 'Εγκρ.Ποσ.1
                '    v.NUM03 = v.QTY1
                'End If
                mtl.QTY1 = v.QTY1
            End If
            'End If
        Next
        fin1.VARCHAR02 = fin1.VARCHAR02.Replace(CUserName & ":STB", CUserName & ":OK")

        '10  Εγκεκριμένη
        '16  Αίτηση πρός έγκριση
        '17  Αρχική έγκριση
        '18  Αίτηση πρός έγκριση ανωτέρου
        fin1.FINSTATES = 10 'Εγκεκριμένη
        DataSafe()
        'VisibleHigher(False)
        'End If
        Cmd_Select()

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

    'Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
    '    Dim s As DataGridView = sender
    '    Try
    '        If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
    '            Dim cellc As DataGridViewCell = s.CurrentCell
    '            Dim CodeExp As String = cellc.EditedFormattedValue
    '            Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_Code")
    '            If celln.Value = 0 AndAlso CodeExp = String.Empty Then
    '                Exit Sub
    '            End If
    '            If Not cellc.FormattedValue.ToString = CodeExp Then
    '                Dim ml = Nothing
    '                Select Case CompanyT'and mtrl extra
    '                    Case 1002   'PFIC
    '                        ml = dbPFIC.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = CodeExp).ToList
    '                    Case 2001 'LK '1001  'LNK
    '                        'ml = dbLNK.MTRLs.Where(Function(f) f.COMPANY = CompanyT).OrderBy(Function(f) f.CODE).Where(Function(f) f.SODTYPE = 53 And f.CODE = MTRL1_CODE).ToList
    '                        ml = (From m In dbLNK.MTRLs Join ex In dbLNK.MTREXTRAs On m.COMPANY Equals ex.COMPANY And m.MTRL Equals ex.MTRL
    '                              Where m.SODTYPE = 53 And m.CODE = CodeExp And ex.BOOL04 = 1
    '                              Select m).ToList
    '                End Select

    '                s.Tag = ml

    '                If ml.Count = 0 Then
    '                    Exit Sub
    '                End If
    '                If ml.Count = 1 Then

    '                    Dim ccode As String = s.Rows(e.RowIndex).Cells("Κωδικός").Value
    '                    Dim mm As Centro.MTRL = db.MTRLs.Where(Function(f) f.COMPANY = CompanyS And f.CODE = ccode).FirstOrDefault
    '                    If Not IsNothing(mm) Then
    '                        Dim mtrl As Integer = mm.MTRL ' db.MTRLs.Where(Function(f) f.CODE = ccode).FirstOrDefault.MTRL
    '                        Dim companyT As Integer = s.Rows(e.RowIndex).Cells("Εταιρεία").Value
    '                        Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.CompanyT = companyT And f.mtrl = mtrl).FirstOrDefault
    '                        If Not IsNothing(mtr) Then
    '                            Dim mlf = Nothing
    '                            Select Case companyT
    '                                Case 1002   'PFIC
    '                                    mlf = CType(ml, List(Of PFIC.MTRL)).FirstOrDefault
    '                                Case 2001 'LK '1001  'LNK
    '                                    mlf = CType(ml, List(Of LNK.MTRL)).FirstOrDefault
    '                            End Select
    '                            mtr.CodeExp = mlf.code

    '                            mtr.UPDDATE = Now()
    '                            Dim cuser = 99 's1Conn.ConnectionInfo.UserId
    '                            mtr.UPDUSER = cuser

    '                            If db.GetChangeSet.Updates.Count > 0 Then
    '                                Me.BindingNavigatorSaveItem.Enabled = True
    '                            Else
    '                                Me.BindingNavigatorSaveItem.Enabled = False
    '                            End If

    '                            Dim dll As DataGridViewComboBoxCell = s.Rows(e.RowIndex).Cells("Search_Code")

    '                            Dim dllidx As Integer = 0

    '                            dllidx = dll.Items.IndexOf(mlf)

    '                            If Not dllidx = -1 Then
    '                                dll.Value = mlf.mtrl
    '                            Else
    '                                dll.Value = 0
    '                            End If

    '                        End If
    '                    End If

    '                End If
    '            End If
    '        End If

    '        If s.Columns(e.ColumnIndex).Name = "Search_Code" Then
    '            Dim cellc As DataGridViewCell = s.CurrentCell
    '            Dim Search_Code As String = cellc.EditedFormattedValue
    '            If Search_Code = "" Then
    '                Exit Sub
    '            End If
    '            If Not cellc.FormattedValue.ToString = Search_Code Then
    '                Dim cellCodeExp As DataGridViewCell = s.Rows(e.RowIndex).Cells("CodeExp")
    '                cellCodeExp.Value = Search_Code

    '                Dim item = s.Rows(e.RowIndex).DataBoundItem
    '                'Dim mtrl As Integer = item.mtrl
    '                Dim cccMultiCompData As Integer = item.cccMultiCompData
    '                Dim mtr = db.cccMultiCompDatas.Where(Function(f) f.cccMultiCompData = cccMultiCompData).FirstOrDefault
    '                If Not IsNothing(mtr) Then
    '                    If Search_Code = "<Επιλέγξτε>" Then
    '                        mtr.CodeExp = Nothing
    '                    Else
    '                        mtr.CodeExp = Search_Code
    '                    End If

    '                    mtr.UPDDATE = Now()
    '                    Dim cuser = 99 's1Conn.ConnectionInfo.UserId
    '                    mtr.UPDUSER = cuser

    '                    If db.GetChangeSet.Updates.Count > 0 Then
    '                        Me.BindingNavigatorSaveItem.Enabled = True
    '                    Else
    '                        Me.BindingNavigatorSaveItem.Enabled = False
    '                    End If

    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub MasterDataGridView_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles MasterDataGridView.CellValidated
    '    Dim s As DataGridView = sender
    '    If s.Columns(e.ColumnIndex).Name = "CodeExp" Then
    '        Dim cellc As DataGridViewCell = s.CurrentCell
    '        Dim CodeExp As String = cellc.EditedFormattedValue
    '        Dim celln As DataGridViewCell = s.Rows(e.RowIndex).Cells("Search_Code")
    '        If celln.Value = 0 AndAlso CodeExp = String.Empty Then
    '            Exit Sub
    '        End If

    '        Dim ml = s.Tag 'Nothing

    '        If Not IsNothing(ml) AndAlso ml.Count = 0 Then
    '            MsgBox("Λάθος Κωδικός", MsgBoxStyle.Critical, "Error")
    '            Dim item = s.Rows(cellc.RowIndex).DataBoundItem
    '            Dim chItem = db.GetChangeSet.Updates.Where(Function(f) f.cccMultiCompData = item.cccMultiCompData).FirstOrDefault
    '            If Not IsNothing(chItem) Then
    '                db.Refresh(RefreshMode.OverwriteCurrentValues, chItem)
    '            End If

    '            cellc.Value = Nothing
    '            celln.Value = 0

    '            If db.GetChangeSet.Updates.Count > 0 Then
    '                Me.BindingNavigatorSaveItem.Enabled = True
    '            Else
    '                Me.BindingNavigatorSaveItem.Enabled = False
    '            End If

    '        End If
    '    End If

    '    s.Tag = Nothing

    'End Sub


    Private Sub MTRLINEsDataGridView_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles MTRLINEsDataGridView.CellValidating
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).DataPropertyName = "QTY1" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim Qty1 As String = cell.EditedFormattedValue
            If Qty1 = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = Qty1 Then
                Dim item As Revera.GetPendingOrdersDetailsResult = s.Rows(e.RowIndex).DataBoundItem
                Dim ms = db.MTRLINEs.Where(Function(f) f.FINDOC = item.FINDOC And f.MTRLINES = item.MTRLINES And f.MTRL = item.MTRL).FirstOrDefault
                If Not IsNothing(ms) Then
                    If Qty1 = "0" Then
                        ms.QTY1 = Nothing
                    Else
                        ms.QTY1 = Qty1
                    End If
                    Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = item.FINDOC).FirstOrDefault
                    If Not IsNothing(fin) Then
                        fin.UPDDATE = Now()
                        Dim cuser = 8888 's1Conn.ConnectionInfo.UserId
                        fin.UPDUSER = cuser
                    End If
                    Me.BindingNavigatorSaveItem.Enabled = True
                End If
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

    Private Sub ddlΗighers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlΗighers.SelectedIndexChanged
        Dim s As ComboBox = sender
        'Αpprovs = s.SelectedItem.ToString()
        'Αpprovs = Αpprovs.Replace("Προιστ.", "").Replace("Διευθ.", "")
    End Sub

    Private Sub TlsddlΗighers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TlsddlΗighers.SelectedIndexChanged
        Dim s As ToolStripComboBox = sender
        Me.BindingNavigatorSaveItem.Enabled = False
        If s.SelectedItem IsNot Nothing AndAlso Not s.SelectedItem.Key = "<Επιλέγξτε>" Then
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
    End Sub
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        If Me.MasterDataGridView.SelectedCells.Count > 0 Then
            newPrintTable(New SortableBindingList(Of Revera.ccCVMtrLine), "GmSupp.Applicat01.rdlc") 'Report1.rdlc")
        End If
    End Sub
    Private Sub newPrintTable(gmVMNewDataTable As SortableBindingList(Of Revera.ccCVMtrLine), ReportEmbeddedResource As String)
        Try
            'Dim drvMaster As DataRowView
            'drvMaster = Me.MasterBindingSource.Current()
            Dim WRptView As New ReportViewer
            Dim ReportDataSource1 As New Microsoft.Reporting.WinForms.ReportDataSource
            ReportDataSource1.Name = "DSccCVMtrLine" ' "DataSet1" ' 'TimPrint"

            Dim SelFinDoc As Integer = Me.MasterDataGridView.Rows(Me.MasterDataGridView.SelectedCells(0).RowIndex).Cells("FinDoc").Value '"ΑIT0000006"
            'ReportDataSource1.Value = CType(Me.MasterBindingSource.DataSource, SortableBindingList(Of Revera.ccCVMtrLine)).Where(Function(f) f.FINCODE = SelFincode) 'gmVMNewDataTable 'ConvertSta5Rpt(MtrLineNew, Me.boxOption_Anektelestes.Text) 'gmVMNewDataTable.DefaultView 'dt.DefaultView 'bind 'TimPrintBindingSource 'bind 'dt.DataSet 'TimPrintTableAdapter.Fill(GenDataSet.TimPrint, 0, 0, 0, 0, 0) 'Me.DataTable1BindingSource
            ReportDataSource1.Value = db.ccCVMtrLines.Where(Function(f) f.FINDOC = SelFinDoc).ToList
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
            paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Company", "SERTORIUS LTD"))
            'paramList.Add(New ReportParameter("VisibleDetail", Not Me.CheckBoxSummary.Checked))
            'paramList.Add(New ReportParameter("DFrom", Me.DateTimePicker1.Value, True)) 'CDate(Format(DateTimePicker1.Value, "MM/dd/yyyy")), True))
            'paramList.Add(New ReportParameter("DTo", Me.DateTimePicker2.Value, True))
            'paramList.Add(New ReportParameter("VisibleColumnYP_VAL", True)) 'Hiden
            'Dim d As DataRowView = CType(ReportDataSource1.Value, DataView)(0) 'gmVMNewDataTable.DefaultView(0)
            'paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FooterTableChoice", d("MTRL_CODE").ToString.Substring(11, 1)))
            WRptView.ReportViewer1.LocalReport.SetParameters(paramList)
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
        db = New DataClassesReveraDataContext(conn) 'My.Settings.GenConnectionString)
        'dbHglp = New DataClassesHglpDataContext(conn)
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.ReveraConnectionString
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
        Cmd_Save()
    End Sub


    Private Sub Cmd_Save()
        If wfm.FrmCancel Then
            wfm.FrmCancel = False
            Exit Sub
        End If
        If Me.VscsBindingSource.Count > 0 Then
            Me.Validate()
            Me.VscsBindingSource.EndEdit()
            Dim fin As New Revera.FINDOC
            If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                Dim Qtys = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) If(f.NUM03, 0) = 0).FirstOrDefault
                If Not IsNothing(Qtys) Then
                    MsgBox("Προσοχή !!! Λάθος ποσότητα.", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If

                'wfm.DateTimePicker1.Value = CTODate
                'wfm.bindingSource.DataSource = New SortableBindingList(Of Revera.GetPendingOrdersDetailsResult)(dets)
                'wfm.ddlTrdr.DataSource = Trdrs
                'wfm.cccTrdDeps = cccTrdDeps
                'wfm.ddlApplicant.DataSource = Applicants
                SetGmChkListBox()
                wfm.ShowDialog()
                If wfm.FrmCancel Then
                    wfm.FrmCancel = False
                    Exit Sub
                End If
                'If wfm.ddlSuppliers.SelectedValue = 0 Then
                '    MsgBox("Υποχρεωτική επιλογή <Προμηθευτής>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                '    Exit Sub
                'End If
                If wfm.ddlFromcccTrdDep.SelectedValue = 0 Then
                    MsgBox("Υποχρεωτική επιλογή <Από Τμήμα>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                If wfm.ddlTrdr.SelectedValue = 0 Then
                    MsgBox("Υποχρεωτική επιλογή <Για Πελάτη>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                If wfm.ddlcccTrdDep.SelectedValue = 0 Then
                    MsgBox("Υποχρεωτική επιλογή <Για Τμήμα Πελάτη>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                'If wfm.ddlApplicant.SelectedValue = 0 Then
                '    MsgBox("Υποχρεωτική επιλογή <ο Αιτών>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                '    Exit Sub
                'End If
                'If wfm.txtBoxRequestNo.Text.Trim = "" Then
                '    MsgBox("Υποχρεωτικός αρ.Αίτησης", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                '    Exit Sub
                'End If
                'If MsgBox("Ολοκλήρωση Παραγγελίας;", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
                '    Exit Sub
                'End If
                If wfm.ddlΗighers.SelectedValue = "0" Then
                    MsgBox("Υποχρεωτική επιλογή <Έγκριση ανωτέρου>", MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If

                Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
                If cuser Is Nothing Then
                    MsgBox("Error User: " & CurUser, MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                Dim ccCUser = db.ccCS1Applicants.Where(Function(f) f.AspNetUsersName = cuser.Name).FirstOrDefault
                If ccCUser Is Nothing Then
                    MsgBox("Προσοχή !!! Δεν υπάρχει στο Softone o αιτών: " & cuser.Name, MsgBoxStyle.Critical, "BindingNavigatorSaveItem")
                    Exit Sub
                End If
                'do save

                fin.TRNDATE = wfm.DateTimePicker1.Value.ToShortDateString
                fin = Findoc_AddingNew(fin, 1251, 1000)
                fin.TRDR = 39611 'Προμηθευτής Αίτησης' wfm.ddlSuppliers.SelectedValue
                'fin.TRDR = wfm.ddlSuppliers.SelectedValue
                'fin.TRDBRANCH = db.trd.
                'fin.SALESMAN = 1006
                fin.SOCASH = 3101
                fin.PAYMENT = 1011
                'fin.INT01 = wfm.txtBoxRequestNo.Text
                fin.UFTBL01 = ccCUser.UFTBL01 'wfm.ddlApplicant.SelectedValue
                fin.UFTBL02 = wfm.ddlFromcccTrdDep.SelectedValue 'Από Τμήμα
                fin.REMARKS = wfm.txtBoxREMARKS.Text
                fin.VARCHAR01 = wfm.txtBoxVARCHAR01.Text 'Εσωτ.Παρατηρήσεις
                fin.VARCHAR02 = wfm.ddlΗighers.Text.Trim & ":STB" '& "|" & wfm.ddlcCCManager.Text 'ccCChief-cCCManager1,2,3
                'fin.VARCHAR02 = fin.VARCHAR02.Replace("<Επιλέγξτε>", "")
                fin.DATE01 = wfm.DateTimePicker2.Value

                '10  Εγκεκριμένη
                '16  Αίτηση πρός έγκριση
                '17  Αρχική έγκριση
                '18  Αίτηση πρός έγκριση ανωτέρου
                fin.FINSTATES = 16 'Αίτηση πρός έγκριση

                'If Not IsNothing(cuser) Then
                '    fin.ccCApplicant = cuser.Name
                'End If

                Dim mc As New Revera.MTRDOC
                mc.COMPANY = fin.COMPANY
                mc.WHOUSE = 1000 '!!!!!!!!!!!!!!!!!!!!!!Πρεπει να γινει ανα υποκατάστημα
                mc.SHIPPINGADDR = "ΘΕΣΗ ΣΚΛΗΡΗ"
                mc.SHPZIP = "19018"
                mc.SHPDISTRICT = "ΜΑΓΟΥΛΑ"
                mc.SHPCITY = "ΜΑΓΟΥΛΑ ΑΤΤΙΚΗΣ"
                '            QTY 5
                'QTY1    5
                'QTY2    5
                mc.QTY1S = 0
                mc.QTY1A = 0
                mc.WASTE = 0
                mc.COSTCOEF = 0
                mc.SALESCVAL = 0
                mc.QTY1H = 0
                mc.QTY2H = 0
                mc.BGINTCOUNTRY = 1000

                fin.MTRDOC = mc

                Dim fp As New Revera.FINPAYTERM

                fp.COMPANY = fin.COMPANY
                'FINDOC  88175
                fp.LINENUM = 1
                fp.FINPAY = 2
                fp.TRDR = 35487
                fp.SOCURRENCY = fin.SOCURRENCY ' 100
                fp.PAYDEMANDMD = 0
                fp.ISCANCEL = 0
                fp.APPRV = 1
                fp.FINALDATE = fin.TRNDATE '20200805'
                fp.TRNDATE = fin.TRNDATE '20200507'
                fp.TRDRRATE = fin.TRDRRATE ' 1
                fp.AMNT = 0
                fp.TAMNT = 0
                fp.LAMNT = 0
                fp.OPNTAMNT = 0
                fp.ISCLOSE = 0
                fp.COMMENTS = fin.FINCODE ' "ΑIT0000001"
                fp.PAYMENT = fin.PAYMENT ' 1011
                fp.INSMODE = 1

                'fin.FINPAYTERMs.Add(fp)


                Dim mls = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of Revera.GetPendingOrdersDetailsResult))
                For Each v In mls

                    v.UFTBL02 = 106 ' wfm.ddlTrdr.SelectedValue '= 106 'ΣΥΝΤ.ΟΡΓΑΝΑ
                    v.cccTrdDep = wfm.ddlcccTrdDep.SelectedValue '= 55 'ΤΜ.ΤΕΧΝΟΛ.ΕΛΕΓΧΟΥ
                    v.cccTrdr = wfm.ddlTrdr.SelectedValue '= 35465 'PFIC LTD.
                    v.WHOUSE = mc.WHOUSE
                    v.QTY1 = v.NUM03 'Αιτ.Ποσ
                    fin = MTRLINE_AddingNew(v, fin)
                Next


                fin.MTRDOC.QTY1 = fin.MTRLINEs.Sum(Function(f) f.QTY1)
                fin.MTRDOC.QTY = fin.MTRDOC.QTY1
                fin.MTRDOC.QTY2 = fin.MTRLINEs.Sum(Function(f) f.QTY2)

                db.FINDOCs.InsertOnSubmit(fin)

            End If

            If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
                Dim findoc As Integer = Me.MasterBindingSource.Current.FINDOC
                Dim Qtys = CType(Me.VscsBindingSource.DataSource, List(Of Revera.GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc And f.QTY1 = 0).FirstOrDefault
                If Qtys IsNot Nothing Then
                    If MsgBox("Προσοχή !!! Λάθος ποσότητα = 0", MsgBoxStyle.Exclamation, My.Application.Info.AssemblyName) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
                If Me.TlsddlΗighers.SelectedItem Is Nothing Or Me.TlsddlΗighers.Text = "<Επιλέγξτε>" Then
                    MsgBox("Προσοχή !!! Επιλέγξτε Ανώτερο για έγκριση.", MsgBoxStyle.Exclamation, My.Application.Info.AssemblyName)
                    Exit Sub
                End If

                Dim fin1 = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                If fin1 Is Nothing Then
                    MsgBox("Error findoc=" & findoc, MsgBoxStyle.Critical, "db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault")
                    Exit Sub
                End If
                Dim higs = Me.MasterBindingSource.Current.Highers.Split("|")
                Dim hs As String = ChkHigher(Me.MasterBindingSource.Current.Highers, CUserName)
                If hs IsNot Nothing And hs = "STB" Then
                    '    fin1.VARCHAR02 = Me.MasterBindingSource.Current.Highers & "|" & Me.TlsddlΗighers.Text.Trim & ":" & "STB"
                    '    VisibleHigher(False)
                    '    If hs = "STB" Then
                    '        VisibleHigher(True)
                    '    End If
                    'Else

                    ''Dim Res = db.ccCVMtrLines.Where(Function(f) f.FINDOC = findoc).ToList
                    Dim res = CType(Me.VscsBindingSource.DataSource, List(Of GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = findoc).ToList

                    For Each v In res
                        'If Not v.QTY1 = v.NUM03 Then 'Εγκρ.Ποσ.1
                        Dim mtl = db.MTRLINEs.Where(Function(f) f.FINDOC = v.FINDOC And f.MTRLINES = v.MTRLINES And Not f.QTY1 = v.QTY1).FirstOrDefault

                        If mtl IsNot Nothing Then
                            'If If(v.NUM03, 0) = 0 Then 'Εγκρ.Ποσ.1
                            '    v.NUM03 = v.QTY1
                            'End If
                            mtl.QTY1 = v.QTY1
                        End If
                        'End If
                    Next

                    fin1.VARCHAR02 = fin1.VARCHAR02.Replace(CUserName & ":STB", CUserName & ":OK") & "|" & Me.TlsddlΗighers.Text & ":STB"
                    '10  Εγκεκριμένη
                    '16  Αίτηση πρός έγκριση
                    '17  Αρχική έγκριση
                    '18  Αίτηση πρός έγκριση ανωτέρου
                    fin1.FINSTATES = 16 'Αίτηση πρός έγκριση
                    'hs = "OK"
                    'Me.TlsddlΗighers.ComboBox.DataSource = Nothing
                    'Me.TlsddlΗighers.ComboBox.DisplayMember = "Key"
                    'Me.TlsddlΗighers.ComboBox.ValueMember = "Value"

                    'If hs = "OK" Then
                    '    Dim Highers = GetHighers(CurUser)
                    '    Me.TlsddlΗighers.ComboBox.DataSource = Highers.ToList
                    '    VisibleHigher(True)
                    '    Me.BindingNavigatorSaveItem.Enabled = False
                    'End If
                    'higs = fin1.VARCHAR02.Split("|")
                    'Dim lkv = New List(Of KeyValuePair(Of String, String))
                    'For Each h In higs
                    '    If h = "" Then Continue For
                    '    lkv.Add(New KeyValuePair(Of String, String)(h.Split(":")(0), h.Split(":")(1)))
                    'Next
                    'fin1.VARCHAR02 = ""
                    'For Each l In lkv
                    '    If l.Key = aHighers.Replace("%", "") Then
                    '        If l.Value = "STB" Then
                    '            l = New KeyValuePair(Of String, String)(l.Key, "OK")
                    '        End If
                    '    End If
                    '    fin1.VARCHAR02 &= l.Key & ":" & l.Value & "|"
                    'Next
                    'If Not fin1.VARCHAR02 = "" Then
                    '    fin1.VARCHAR02 = fin1.VARCHAR02.Substring(0, fin1.VARCHAR02.Length - 1)
                    'End If
                End If
            End If

            Dim gg = db.GetChangeSet
            If Me.DataSafe() Then

                'SendEmail(wfm)
                If Me.Text = "Αποθήκη - Υπόλοιπα Ειδών/Αίτηση" Then
                    fin = db.FINDOCs.Where(Function(f) f.FINDOC = fin.FINDOC).FirstOrDefault
                    For Each mt In fin.MTRLINEs
                        mt.ccCAFINDOC = mt.FINDOC
                        mt.ccCAMTRLINES = mt.MTRLINES
                    Next
                    SaveData()
                    Me.TlsBtnClear.PerformClick()
                End If
                SetGmChkListBox() 'Clear Me.VscsBindingSource.DataSource 
            End If
            If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
                Cmd_Select()
                'Me.MasterBindingSource.Position = pos
                'Dim fin1 = db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault
                'If fin1 Is Nothing Then
                '    MsgBox("Error findoc=" & findoc, MsgBoxStyle.Critical, "db.FINDOCs.Where(Function(f) f.FINDOC = findoc).FirstOrDefault")
                '    Exit Sub
                'End If
                'Dim hs As String = ChkHigher(fin1.VARCHAR02, CUserName)
                ''If hs IsNot Nothing And hs = "OK" Then
                ''    fin1.VARCHAR02 = Me.MasterBindingSource.Current.Highers & "|" & Me.TlsddlΗighers.Text.Trim & ":" & "STB"
                ''End If
                'Me.TlsddlΗighers.ComboBox.DataSource = Nothing
                'Me.TlsddlΗighers.ComboBox.DisplayMember = "Key"
                'Me.TlsddlΗighers.ComboBox.ValueMember = "Value"

                'If hs = "OK" Then
                '    Dim Highers = GetHighers(CurUser)
                '    Me.TlsddlΗighers.ComboBox.DataSource = Highers.ToList
                '    VisibleHigher(True)
                'Else
                '    VisibleHigher(False)
                '    Me.BindingNavigatorSaveItem.Enabled = False
                'End If
            End If
        End If
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

            Next
            dets = dets.Distinct(Function(f) f.CODE).ToList

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
        'Throw New NotImplementedException()
    End Sub


    Private Sub SendEmail(wfm As WHouseBalFR)
        Dim ΤοEmail = wfm.txtBoxFrom.Text
        Dim message = New Net.Mail.MailMessage()

        message.To.Add(ΤοEmail)

        message.From = New Net.Mail.MailAddress(wfm.txtBoxFrom.Text)
        message.Subject = "Νέα αίτηση"
        message.Body = wfm.txtBoxEmailBody.Text

        Dim client = New Net.Mail.SmtpClient("mailer.kfertilizers.gr", 25)
        ''to authenticate we set the username and password properites on the SmtpClient 
        client.UseDefaultCredentials = False
        client.Credentials = New Net.NetworkCredential("erp@sertorius.gr", "1mgergm++") 'settings.Smtp.Network.UserName, settings.Smtp.Network.Password)

        Try
            client.SendAsync(message, Nothing)
            MsgBox("Ok")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Throw New NotImplementedException()
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
        If e.ListChangedType = ListChangedType.PropertyDescriptorChanged Then
            Dim nu = 1 ' As CCCCheckZip = MasterBindingSource.Current
            'nu.modifiedOn = Now()
            'Me.BindingNavigatorSaveItem.Enabled = True
        End If
    End Sub


    Private Sub MasterBindingSource_PositionChanged(sender As Object, e As EventArgs) Handles MasterBindingSource.PositionChanged
        If Me.Text = "Αποθήκη - Εκκρεμείς Αιτήσεις-Παραγγελίες" Then
            Dim s As BindingSource = sender
            If s.Current IsNot Nothing Then

                Dim finHeader As Revera.GetPendingOrdersHeaderResult = s.Current
                Try
                    If Me.VscsBindingSource.DataSource IsNot Nothing Then
                        'Dim Res = db.ccCVMtrLines.Where(Function(f) f.FINDOC = findoc).ToList
                        Dim res = CType(Me.VscsBindingSource.DataSource, List(Of GetPendingOrdersDetailsResult)).Where(Function(f) f.FINDOC = finHeader.FINDOC).ToList
                        'Me.VscsBindingSource.DataSource = New SortableBindingList(Of Revera.ccCVMtrLine)(Res)
                        res = GetApplicantLogs(res)
                        Me.MTRLINEsDataGridView.DataSource = res 'Me.VscsBindingSource
                        Me.BindingNavigatorNewDoc.BindingSource = New BindingSource With {.DataSource = res} 'Me.VscsBindingSource
                        MTRLINEsDataGridView_Styling()
                    End If


                    Me.txtBoxΗigher.Text = finHeader.Highers
                    Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
                    Me.BindingNavigatorNewDoc.Items.Cast(Of ToolStripItem).Where(Function(f) f.Tag = 1).ForEach(Sub(f As ToolStripItem) f.Visible = False)
                    Me.TlSBtnHigherEnd.Visible = False
                    If CRole = "5.Διευθυντής Εργοστασίου" Then
                        Me.TlSBtnCheckDetails.Visible = True
                        Me.ToolStripSeparator8.Visible = True
                        Me.TlSBtnUnCheckDetails.Visible = True
                        Me.MTRLINEsDataGridView.ReadOnly = False
                        Me.TlSBtnHigherEnd.Visible = True
                        Exit Sub
                    End If
                    Me.MTRLINEsDataGridView.ReadOnly = True
                    VisibleHigher(False)
                    If finHeader.ApplicantNAME Is Nothing Then
                        Exit Sub
                    End If
                    If Not cuser.Name = finHeader.ApplicantNAME Then ' Όχι ο αιτών.
                        Dim hs As String = ChkHigher(finHeader.Highers, If(aHighers, "%").Replace("%", ""))
                        If hs IsNot Nothing And hs = "OK" Then
                            'Dim res
                            Dim HighCount = 0
                            For Each h In finHeader.Highers.Split("|")
                                If h = "" Then Continue For
                                HighCount += 1
                                If h.Split(":")(0) = aHighers.Replace("%", "") Then
                                    'Res = h.Split(":")(1)
                                    HighCount = 0
                                End If
                            Next
                            If HighCount > 0 Then
                                Exit Sub
                            End If
                        End If
                        Me.BindingNavigatorNewDoc.Items.Cast(Of ToolStripItem).Where(Function(f) f.Tag = 1).ForEach(Sub(f As ToolStripItem) f.Visible = True)
                        Me.TlSBtnHigherEnd.Visible = False
                        Me.MTRLINEsDataGridView.ReadOnly = False
                        'Dim higs = finHeader.Highers.Split("|")
                        'Dim lkv = New List(Of KeyValuePair(Of String, String))
                        'For Each h In higs
                        '    If h = "" Then Continue For
                        '    lkv.Add(New KeyValuePair(Of String, String)(h.Split(":")(0), h.Split(":")(1)))
                        'Next
                        'VisibleHigher(False)
                        Me.TlsddlΗighers.ComboBox.DataSource = Nothing
                        Me.TlsddlΗighers.ComboBox.DisplayMember = "Key"
                        Me.TlsddlΗighers.ComboBox.ValueMember = "Value"

                        'If hs = "OK" Then
                        Dim Highers = GetHighers(CurUser)
                        Me.TlsddlΗighers.ComboBox.DataSource = Highers.ToList
                        VisibleHigher(True)
                        'Else
                        '    VisibleHigher(False)
                        '    Me.BindingNavigatorSaveItem.Enabled = False
                        'End If



                        'For Each l In lkv
                        '        If l.Key = Me.ddlΗighers.Text.Trim Then
                        '            If l.Value = "OK" Then
                        '                'fill higher
                        '                Dim Highers = GetHighers(CurUser)
                        '                Me.TlsddlΗighers.ComboBox.DataSource = Highers.ToList
                        '                VisibleHigher(True)
                        '            End If

                        '        End If
                        '    Next
                    End If


                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Function GetApplicantLogs(res As List(Of GetPendingOrdersDetailsResult)) As List(Of GetPendingOrdersDetailsResult)
        For Each re In res
            'Α) Αίτηση προσφοράς
            'Β) Παραγγελία 
            'Γ) Παραλαβή υλικού
            '2000    ΑΠΡΦ	Αίτηση Προσφοράς	2000
            '2021    ΠΑΡ	Παραγγελία Σε Προμηθευτή Παράδοση Καβάλα	2021
            '2022    ΠΑΡ ΕΔΡΑ	Παραγγελία Σε Προμηθευτή Παράδοση Έδρα	2021
            '2023    ΠΑΡ ΑΤΛ	Παραγγελία Σε Προμηθευτή Παράδοση Έδρα	2021
            '2041    ΔΠ	Δελτίο Αποστολής Προμηθευτή	2041
            '2045    ΔΠΠ	Δελτίο Ποσοτικής Παραλαβής	2045
            Dim msns = db.MTRLINEs.Where(Function(f) f.FINDOC1.COMPANY = Company And f.FINDOC1.SOSOURCE = 1251 And {2000, 2021, 2022, 2023, 2041, 2045}.Contains(f.FINDOC1.SERIES) And f.ccCAFINDOC = re.FINDOC And f.ccCAMTRLINES = re.MTRLINES)
            If msns.Count > 0 Then
                Dim APRF As String = ""
                Dim PAR As String = ""
                Dim DP As String = ""
                For Each msn In msns
                    Select Case msn.FINDOC1.SERIES
                        Case 2000
                            APRF = "[" & msn.FINDOC1.FINCODE & "" & "," & msn.FINDOC1.TRNDATE & "]"
                        Case 2021, 2022, 2023
                            PAR = "[" & msn.FINDOC1.FINCODE & "" & "," & msn.FINDOC1.TRNDATE & "]"
                        Case 2041, 2045
                            DP = "[" & msn.FINDOC1.FINCODE & "" & "," & msn.FINDOC1.TRNDATE & "]"
                    End Select
                Next
                re.ApplicationLog = "[" & APRF & "," & PAR & "," & DP & "]"
            End If
        Next

        Return res
        Throw New NotImplementedException()
    End Function

    Private Sub VisibleHigher(v As Boolean)
        Me.ToolStripSeparator10.Visible = v
        Me.ToolStripLabel2.Visible = v
        Me.TlsddlΗighers.Visible = v
        'Me.TlSBtnHigherEnd.Visible = v
        'Throw New NotImplementedException()
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


    Private Sub VscsBindingSource_CurrentItemChanged(sender As Object, e As EventArgs) Handles VscsBindingSource.CurrentItemChanged
        Dim s As BindingSource = sender
        Me.BindingNavigatorSaveItem.Enabled = False
        If Me.VscsBindingSource.Count > 0 Then
            'Dim lst As New List(Of vsc)
            'lst = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

            'Me.lblV08.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
            'Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))
            'Me.lblV10.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice) + lst.Sum(Function(f) f.TotPrice) * (lst.FirstOrDefault.PERCNT / 100))
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
    End Sub

    Private Sub VscsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles VscsBindingSource.CurrentChanged
        Dim s As BindingSource = sender
        Me.BindingNavigatorSaveItem.Enabled = False
        If Me.VscsBindingSource.Count > 0 Then
            'Dim lst As New List(Of vsc)
            'lst = CType(Me.VscsBindingSource.DataSource, SortableBindingList(Of vsc)).ToList

            'Me.lblV08.Text = String.Format("{0:N3}", lst.Sum(Function(f) f.Qty1))
            'Me.lblV09.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice))
            'Me.lblV10.Text = String.Format("{0:N2}", lst.Sum(Function(f) f.TotPrice) + lst.Sum(Function(f) f.TotPrice) * (lst.FirstOrDefault.PERCNT / 100))
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
    End Sub


    Private Sub VscsBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles VscsBindingSource.ListChanged
        Dim s As BindingSource = sender
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
        If e.ListChangedType = ListChangedType.ItemChanged Then
            Dim nu = Me.VscsBindingSource.Current
            '    'nu.modifiedOn = Now()
            Me.BindingNavigatorSaveItem.Enabled = True
        End If
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

    ''' <summary>
    ''' fin As Revera.FINDOC, NSoSource As Integer, NSeries As Short
    ''' </summary>
    ''' <param name="fin"></param>
    ''' <param name="NSoSource"></param>
    ''' <param name="NSeries"></param>
    ''' <returns>Revera.FINDOC</returns>
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
            NMTRLINE.SODTYPE = v.SODTYPE
            NMTRLINE.WHOUSE = v.WHOUSE
            NMTRLINE = ZeroMTRLINE(NMTRLINE, NFINDOC)
            NMTRLINE.MTRL = v.MTRL
            NMTRLINE.MTRUNIT = v.MTRUNIT
            NMTRLINE.VAT = v.VAT
            NMTRLINE.QTY1 = v.QTY1
            NMTRLINE.QTY = NMTRLINE.QTY1
            NMTRLINE.QTY2 = NMTRLINE.QTY1
            NMTRLINE.UFTBL02 = v.UFTBL02 ' 106
            NMTRLINE.cccTrdDep = v.cccTrdDep ' 55
            NMTRLINE.cccTrdr = v.cccTrdr ' 35465
            NMTRLINE.SALESMAN = NFINDOC.SALESMAN
            NMTRLINE.COMMENTS1 = v.COMMENTS1
            NMTRLINE.NUM03 = v.NUM03 'Αιτ.Ποσ

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
            Dim snum As Revera.SERIESNUM = db.SERIESNUMs.Where(Function(f) f.COMPANY = NFINDOC.COMPANY And f.SOSOURCE = NFINDOC.SOSOURCE And f.SERIES = NFINDOC.SERIES And f.FISCPRD = NFINDOC.FISCPRD).FirstOrDefault
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
            'NMTRLINE.SODTYPE = 51 'NFINDOC.SODTYPE
            NMTRLINE.MTRL = 0
            NMTRLINE.PENDING = 0
            NMTRLINE.SOSOURCE = NFINDOC.SOSOURCE
            NMTRLINE.SOREDIR = 0
            NMTRLINE.MTRTYPE = 0
            NMTRLINE.SOTYPE = 0
            'NMTRLINE.WHOUSE = 1000 '?
            'NMTRLINE.MTRUNIT = 101 '?
            'NMTRLINE.VAT = 1410 ' 0 'Not Null
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

    Private Sub ΕγκρίσειςToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ΕγκρίσειςToolStripMenuItem.Click
        MsgBox("Εγκρίσεις")
    End Sub

    Private Sub LnkLblSetUser_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkLblSetUser.LinkClicked
        'CurUser = Me.GmChkListBoxAplicant.TlStxtBox.Text
        'Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
        'If Not IsNothing(cuser) Then
        '    'ch.keskesiadis
        '    CUserName = cuser.UserName
        '    CurUser = cuser.UserName
        '    Me.GmChkListBoxAplicant.TlStxtBox.Text = cuser.Name
        '    Me.ddlΗighers.Text = "<Επιλέγξτε>"
        'End If
    End Sub

    Private Sub ddlUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUsers.SelectedIndexChanged
        If Me.ddlUsers.SelectedItem IsNot Nothing Then
            If Me.ddlUsers.SelectedItem.UserName = "<Επιλέγξτε>" Then
                Exit Sub
            End If
            CurUser = Me.ddlUsers.SelectedItem.UserName
        Else
            CurUser = Me.ddlUsers.Text
        End If
        Dim cuser As GmIdentityUser = GmUserManager.ChkUser(CurUser.Replace("gmlogic", "gm"))
        If Not IsNothing(cuser) Then
            'ch.keskesiadis
            CUserName = cuser.Name
            'CurUser = cuser.UserName
            Me.GmChkListBoxAplicant.TlStxtBox.Text = cuser.Name
            'Me.ddlΗighers.Text = "<Επιλέγξτε>"
            Dim roles = UserManager.GetRoles(cuser.Id)
            Dim inRole = roles.Where(Function(f) {"2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου"}.Contains(f)).FirstOrDefault
            aHighers = Nothing
            If inRole IsNot Nothing Then
                CRole = inRole
                aHighers = CUserName
            End If
        End If
        Cmd_Select()
    End Sub

    Private Sub RadioBtn_CheckedChanged(sender As Object, e As EventArgs) Handles RadioBtnAll.CheckedChanged, RadioBtnToAproved.CheckedChanged, RadioBtnPending.CheckedChanged, RadioBtnApproved.CheckedChanged
        If Me.MasterBindingSource.Count > 0 Then
            Cmd_Select()
        End If
    End Sub


#End Region

End Class