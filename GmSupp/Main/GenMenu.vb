Imports System.Data.SqlClient
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Softone


'Imports System.Data.Entity
'Imports MySolution.Module
'Imports MySolution.Module.BusinessObjects


Public Class GenMenu
    Public Shared logger As log4net.ILog
    Dim fS1HiddenForm As New Form
    Enum dbs
        Test_Hglp = 11
        Test_LNK = 12
        Test_Centr = 13
        Test_PFIC = 14
        Test_LK = 15
        Test_REVERA = 16
        Test_NVF = 17
        HGLP = 21
        LNK = 22
        CENTR = 23
        PFIC = 24
        LK = 25
        REVERA = 26
        NVF = 27
    End Enum

    Private Sub GenMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'test
        'Dim p = DATABASE.SetInitializer(New MigrateDatabaseToLatestVersion(Of GmIdentityDbContext, Migrations.Configuration)())


        'Dim gg As New GmS1Lib.S1Init
        'Dim frm As New GmS1Lib.OpenItemBR
        ''frm.Tag = s.Text
        'frm.Show()
        'gg.Initialize()

        'Try
        '    logger = log4net.LogManager.GetLogger("GmSupp")
        '    logger.Info("Form1_Load() - Start")
        '    logger.Debug("Form1_Load() - Code Implementation goes here......")
        '    'Dim objTest As New Log4NetAssembly2.clsAssembly2
        'Catch ex As Exception
        '    logger.Error("Form1_Load() - " & ex.Message)
        'Finally
        '    logger.Info("Form1_Load() - Finish")
        'End Try

        'SetDBs(dbs.Hglp)
        Me.WindowState = FormWindowState.Normal
        Me.Text = "Αιτήσεις"
        'Dim message, title, defaultValue As String
        'Dim myValue As Object
        '' Set prompt.
        'message = "Enter Pass for Admin"
        '' Set title.
        'title = "InputBox Demo"
        'defaultValue = "" '"1"   ' Set default value.

        '' Display message, title, and default value.
        'myValue = InputBox(message, title, defaultValue)
        '' If user has clicked Cancel, set myValue to defaultValue
        LocalIP = Utility.GetLocalIP

        S1Path = "C:\Softone\"
        If Not System.IO.Directory.Exists(S1Path) Then
            'Throw New System.IO.DirectoryNotFoundException("The directory " & SrcPath & " does not exists")
            MsgBox("Προσοχή !!!. Δεν υπάρχει κατάλογος " & S1Path & vbCrLf & "Παρακαλώ δημιουργήστε τον και τοποθετήστε μέσα τα ανάλογα XCO", MsgBoxStyle.Critical, "Critical")
            Me.Close()
            Exit Sub
        End If


        'MsgBox("Καλή Χρονιά !!!" & vbCrLf & "  -- 2018 --", MsgBoxStyle.Information)
        If {"192.168.10.1081", "192.168.51.13"}.Contains(LocalIP) Then
            CurUser = "gmlogic"
            CurPass = "1mgergm++"
            CurUserRole = "Developer"

            'CurUser = "ch.keskesiadis"
            'CurPass = "radrad"
            'CurUserRole = "Admins"
            CompName = "SERTORIUS"
            'CompName = "HGLP"
            'CompName = "NVF"
            For Each mn As ToolStripItem In Me.MenuStrip.Items
                mn.Visible = False
            Next
            Me.HelpMenu.Visible = True


            'Me.ToolsMenu.Visible = True
            'Me.TestBarcodeToolStripMenuItem.PerformClick()

            'Dim frm As New BCLabelsBr
            'frm.Tag = "Hglp"
            'frm.Text = "Αποθήκη - Εκτύπωση Ετικετών Barcode" '"Αποθήκη - Περιγραφή Ετικέτας"
            ''SettingForms(sender, e, frm, Me.ΑποθήκηToolStripMenuItem.Text & " - " & "Εκτύπωση Ετικετών Barcode", 0, True)
            'Dim frm As New GroupBalance
            'frm.Tag = "Πελατών"
            'frm.ShowDialog()

            'AAGenLogin.ShowDialog()
            'Me.Close()
            'Exit Sub
        Else
            Dim loginf As New LoginForm1
            loginf.Text = "Εισαγωγή στοιχείων"
            'ch.keskesiadis radrad
            If {"192.168.10.108", "192.168.51.13"}.Contains(LocalIP) Then
                loginf.UsernameTextBox.Text = "t.andreoglou" '"ch.keskesiadis" '"d.makaridis" ' "th.naris" ' "a.giannikos" ' "m.siopis" '
                loginf.PasswordTextBox.Text = "An@Ti6912" '"radrad" '"Ma@Di55489" '"Na@Th3131" '"Gi@Al5879" '"Si@Ma1891" '"AV$2865" Si@Ma1891
                'loginf.UsernameTextBox.Text = "gmlogic" '"d.makaridis" '
                'loginf.PasswordTextBox.Text = "1mgergm++" '"Ma@Di55489" '
                'If loginf.UsernameTextBox.Text = "ch.keskesiadis" Then

                'End If
            End If


            loginf.ShowDialog()
            CurUser = loginf.UsernameTextBox.Text
            CurPass = loginf.PasswordTextBox.Text
            CTODate = loginf.DateTimePicker1.Value

            If {"192.168.10.108", "192.168.51.13"}.Contains(LocalIP) Then
                If loginf.ddlXCOs.SelectedIndex = -1 Then
                    loginf.ddlXCOs.SelectedIndex = loginf.ddlXCOs.Items.IndexOf("SERTORIUS") '"SERTORIUS"
                End If
            End If


            If Not IsNothing(loginf.ddlXCOs.SelectedItem) Then
                CompName = loginf.ddlXCOs.SelectedItem
            End If
            '            Enum dbs
            '    Test_Hglp = 11
            '    Test_LNK = 12
            '    Test_Centr = 13
            '    Test_PFIC = 14
            '    Test_LK = 15
            '    Test_REVERA = 16
            '    Hglp = 21
            '    LNK = 22
            '    Centr = 23
            '    PFIC = 24
            '    LK = 25
            '    REVERA = 26
            'End Enum
            If IsNothing(CompName) Or CurUser = "" Or CurPass = "" Then
                MsgBox("Προσοχή !!!. Λάθος στοιχεία. Η εφαρμογή διακόπτεται", MsgBoxStyle.Critical, "Critical")
                Me.Close()
                Exit Sub
            End If

            For Each mn As ToolStripItem In Me.MenuStrip.Items
                mn.Visible = False
            Next
            Me.HelpMenu.Visible = True
        End If
        'If CurUser = "" Then
        '    Me.Close()
        'End If
        'If Not (CurUser = "g.igglesis") Then 'Or CurUser.Contains("katerina"))
        Dim login As Boolean = Test_Login(CurUser, CurPass)
        If Not login Then
            MsgBox("Login Error " & CurUser, MsgBoxStyle.Critical, "GenMenu_Test_Login(CurUser, Pass)")
            Me.Close()
            Exit Sub
        End If
        ''End If
        'Dim gg = S1_WSGetTableFields(s1Conn, "CUSTOMER", "CUSTOMER")
        'Dim lg = S1_WSlogin(s1Conn, CurUser, Pass, Company)
        SetDBs(CType(System.Enum.Parse(GetType(dbs), CompName.ToUpper.Replace("SERTORIUS", "REVERA").Replace("ReveraLite".ToUpper, "REVERA").Replace("HglpLite".ToUpper, "HGLP").ToUpper), dbs)) 'dbs.Hglp)

        If Not CurUser = "gmlogic" Then
            'If Not Utility.ChkUserIfExistInDb(CurUser) Then ' If UserId = 0 Then ' Not S1User

            '    Dim chkUser = GmEntityController.CreateUser(CurUser, CurPass, "Users", Company, CompName, UserId)
            '    If Not IsNothing(chkUser) AndAlso chkUser.Succeeded Then
            '        CurUserRole = UserManager.GetRoles(curGmEntityUser.Id).SingleOrDefault
            '        'MsgBox("Ok")
            '    Else
            '        If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
            '            'Exit Sub
            '        End If
            '    End If
            'End If
        End If
        Me.Text = "Αιτήσεις"

        SetVisibleControls(CurUser, CurUserRole)

        Me.WindowsMenu.Visible = True
        'SettingForms(sender, e, New frmImportEAPDataFromPDF, "Import PDF", 0, True)
        'Dim conString As New SqlConnectionStringBuilder '(My.Settings.GenConnectionString.Replace("ECOLLECTOR_LGL_ALB", "eCollector_LGL_ALB_PCHCK_New"))
        'Me.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID 'CONNECT_STRING
        'Dim ds As New LegalNetworks.Legal.DTSClient.clsCAlphabank
        'Me.TlSSTLabelVer.Text = ds.GetType.AssemblyQualifiedName
        'Exit Sub
        'End If

        Me.WindowState = FormWindowState.Maximized
        Me.MenuStrip.Visible = True
        'Open First Form
        'SettingForms(sender, e, New Import_Negative_Equitys, "Import_Negative_Equitys", 0, True)
        'MsgBox("Welcome " & CurUser, MsgBoxStyle.Information, "GenMenu")
    End Sub

    Private Function Test_Login(CurUser As String, Pass As String) As Boolean

        Dim Login = False

        'Me.Cursor = Cursors.WaitCursor
        s1Conn = Nothing
        Company = 5000 'SERTORIUS
        Dim Branch = 1000 'Hglp

        Try
            'If {"avichou", "panagiotis", "katerina", "akrokos", "thzachopoulos", "dkanellopoulos", "skamariaris", "gkonstantatos"}.Contains(CurUser) Then
            '    'S1Path = "C:\Softone\"
            '    'If {"akrokos", "thzachopoulos", "dkanellopoulos"}.Contains(curUser) Then
            '    '    S1Path = "C:\Soft1_LNK\"
            '    'End If
            '    'xco = "LK.XCO"
            '    If CompName = "LK" Then
            '        Company = 1001
            '        Branch = 1000
            '    End If
            '    If CompName = "NVF" Then
            '        Company = 2002
            '        Branch = 1000
            '    End If
            '    'XCOFile = S1Path & "LK.XCO"

            'End If

            ''kbechrakis pass 1588
            'If {"g.igglesis", "kbechrakis", "i.pilarinos"}.Contains(CurUser) Then
            '    'S1Path = "C:\Softone\"
            '    'xco = "PFIC.XCO"
            '    CompName = "PFIC"
            '    'XCOFile = S1Path & "PFIC.XCO"
            '    Company = 1002
            '    Branch = 2000
            'End If

            'Dim XCOFile = S1Path & CompName.Replace("SERTORIUS", "REVERA").Replace("ReveraLite".ToUpper, "REVERA").Replace("HglpLite".ToUpper, "HGLP").ToUpper & ".xco"
            'If IO.File.Exists(XCOFile) Then

            '    Dim q = From line In IO.File.ReadAllLines(XCOFile, System.Text.Encoding.GetEncoding("windows-1253"))
            '    For Each ss As String In q
            '        If ss.Contains("SERVER") Then
            '            SERVER = ss.Split("=")(1)
            '        End If
            '        If ss.Contains("DATABASE") Then
            '            DATABASE = ss.Split("=")(1)
            '        End If
            '        'If ss.Contains("COMPANY") Then
            '        '    Company = ss.Split("=")(1)
            '        'End If
            '    Next
            'Else
            '    'MsgBox("Προσοχή !!!. Λάθος " & XCOFile & " . Η εφαρμογή διακόπτεται", MsgBoxStyle.Critical, "Critical")
            '    'Return False
            'End If
            'If CompName.ToUpper = "REVERA" Then
            '    Company = 4000
            'End If
            If CompName.ToUpper = "SERTORIUS" Then
                Company = 5000
            End If

            'If {"REVERA", "SERTORIUS"}.Contains(CompName.ToUpper) Then
            Login = False
            Dim us = CheckUsers(Company, CurUser, Pass)
            If Not IsNothing(us) Then
                'For first time us.S1User = True
                Dim decryptPass = CryptoUtils.Decrypt(us.encryptPass)
                If CurPass = decryptPass Then
                    If us.S1User = False Then
                        Return True
                    End If
                Else 'Wrong Pass
                    Return False
                End If



                'Check if S1User
                'SetDBs(dbs.REVERA)
                SetDBs(CType(System.Enum.Parse(GetType(dbs), CompName.ToUpper.Replace("SERTORIUS", "REVERA").Replace("ReveraLite".ToUpper, "REVERA").Replace("HglpLite".ToUpper, "HGLP").ToUpper), dbs))
                '    Dim uS1 As Revera.USER = Nothing
                '    Using db1 As New DataClassesReveraDataContext(My.Settings.GenConnectionString)
                '        uS1 = db1.USERs.Where(Function(f) f.CODE = CurUser).FirstOrDefault
                '        If IsNothing(uS1) Then 'For first time only
                '            Dim UserManager = GmUserManager.Create(New GmIdentityDbContext)
                '            Dim usU = UserManager.FindByName(CurUser)
                '            usU.S1User = False
                '            usU.Users = 0
                '            UserManager.Update(usU)
                '            Return True
                '        Else
                '            If Not IsNothing(uS1.DEFCOMPANY) Then
                '                Company = uS1.DEFCOMPANY
                '            End If
                '            If Not IsNothing(uS1.DEFBRANCH) Then
                '                Branch = uS1.DEFBRANCH
                '            End If
                '            If us.Users = 0 Then 'For first time
                '                Dim UserManager = GmUserManager.Create(New GmIdentityDbContext)
                '                Dim usU = UserManager.FindByName(CurUser)
                '                'usU.S1User = True
                '                usU.Users = uS1.USERS
                '                UserManager.Update(usU)
                '            End If
                '        End If
                '    End Using
            End If

            ''End If

            'Dim DTLogin = CTODate ' DateTime.Now


            'If Not IO.File.Exists(S1Path & "XDll.dll") Then
            '    Return Login
            'End If
            'XSupport.InitInterop(0, S1Path & "XDll.dll")

            ''Dim fS1HiddenForm As New Form 'Needed for Opening Form from SoftOne
            'XSupport.InitInterop(fS1HiddenForm.Handle)

            's1Conn = XSupport.Login(XCOFile, CurUser, Pass,
            '                           Company.ToString, Branch.ToString, DTLogin)

            'If s1Conn.ConnectionInfo IsNot Nothing Then
            '    Login = True
            '    UserId = s1Conn.ConnectionInfo.UserId
            'Else
            '    Me.Text = strAppName
            '    MsgBox("Connection Error! s1Conn.ConnectionInfo Is Nothing", MsgBoxStyle.Critical, strAppName)
            'End If
        Catch ex As Exception
            Me.Text = strAppName
            'MsgBox("Connection Error:" & vbCrLf & S1Path & vbCrLf & curUser & vbCrLf & ex.ToString, MsgBoxStyle.Critical, strAppName)
            MsgBox(SERVER & vbCrLf & GetExceptionInfo(ex))

        Finally
            'Me.Cursor = Cursors.Default
        End Try
        Return Login

        'Throw New NotImplementedException()
    End Function

    Private Function CheckUsers(_Company As Short, curUser As String, pass As String) As GmIdentityUser
        Dim us As GmIdentityUser = Nothing

        ''9005    ΣΥΝΤΗΡΗΣΗ	ΣΥΝΤΗΡΗΣΗ	901	7845123
        ''9007    g.kazoglou	g.kazoglou	901	Ka@Ge1744
        ''9008    a.dimzas	a.dimzas	901	Di@Ap2687
        ''9009    g.bampatzanis	g.bampatzanis	901	Ba@Ge7846
        ''9010    k.chaitas	k.chaitas	901	Ch@Ko2124
        ''9011    k.kontogeorgos	k.kontogeorgos	901	Ko@Ko9856
        ''9012    a.zdratskidis	a.zdratskidis	901	Zd@At4247
        ''9013    ch.vogiatzi	ch.vogiatzi	901	Vo@Ch1800
        ''9014    a.zygoulas	a.zygoulas	901	Zy@An0249
        ''9015    k.matakas	k.matakas	901	Ma@Ky9912
        ''9016    p.gkoutziamanis	p.gkoutziamanis	901	Gk@Pa0245
        ''9017    g.stilianidis	g.stilianidis	901	St@Ge4932
        ''9018    l.soulis	l.soulis	901	So@La4682
        ''9019    s.lazaridis	s.lazaridis	901	La@So8732
        ''9020    g.belitsios	g.belitsios	901	Be@Io4873
        ''9021    n.apostolidis	n.apostolidis	901	Ap@Ne5478
        ''9022    st.lazaridis	st.lazaridis	901	La@St1776
        ''9023    e.papadimitriou	e.papadimitriou	901	Pa@Ev1187
        ''9024    ch.vogiatzis	ch.vogiatzis	901	Vo@Ch4873
        ''9025    erg.organon	erg.organon	901	Erg@Org6529
        ''9026    s.fourlis	s.fourlis	901	Fo@St2883
        ''9027    a.karakasidis	a.karakasidis	901	Ka@An3412
        ''9028    ch.thalassinos	ch.thalassinos	901	Th@Ch1313
        'uss.Add(New Revera.USER With {.USERS = 9005, .CODE = "ΣΥΝΤΗΡΗΣΗ", .CODE1 = "7845123"})
        'uss.Add(New Revera.USER With {.USERS = 9007, .CODE = "g.kazoglou", .CODE1 = "Ka@Ge1744"})
        'uss.Add(New Revera.USER With {.USERS = 9008, .CODE = "a.dimzas", .CODE1 = "Di@Ap2687"})
        'uss.Add(New Revera.USER With {.USERS = 9009, .CODE = "g.bampatsanis", .CODE1 = "Ba@Ge7846"})
        'uss.Add(New Revera.USER With {.USERS = 9010, .CODE = "k.chaitas", .CODE1 = "Ch@Ko2124"})
        'uss.Add(New Revera.USER With {.USERS = 9011, .CODE = "k.kontogeorgos", .CODE1 = "Ko@Ko9856"})
        'uss.Add(New Revera.USER With {.USERS = 9012, .CODE = "a.zdratskidis", .CODE1 = "Zd@At4247"})
        'uss.Add(New Revera.USER With {.USERS = 9013, .CODE = "ch.vogiatzi", .CODE1 = "Vo@Ch1800"})
        'uss.Add(New Revera.USER With {.USERS = 9014, .CODE = "a.zygoulas", .CODE1 = "Zy@An0249"})
        'uss.Add(New Revera.USER With {.USERS = 9015, .CODE = "k.matakas", .CODE1 = "Ma@Ky9912"})
        'uss.Add(New Revera.USER With {.USERS = 9016, .CODE = "p.gkoutziamanis", .CODE1 = "Gk@Pa0245"})
        'uss.Add(New Revera.USER With {.USERS = 9017, .CODE = "g.stilianidis", .CODE1 = "St@Ge4932"})
        'uss.Add(New Revera.USER With {.USERS = 9018, .CODE = "l.soulis", .CODE1 = "So@La4682"})
        'uss.Add(New Revera.USER With {.USERS = 9019, .CODE = "s.lazaridis", .CODE1 = "La@So8732"})
        'uss.Add(New Revera.USER With {.USERS = 9020, .CODE = "g.belitsios", .CODE1 = "Be@Io4873"})
        'uss.Add(New Revera.USER With {.USERS = 9021, .CODE = "n.apostolidis", .CODE1 = "Ap@Ne5478"})
        'uss.Add(New Revera.USER With {.USERS = 9022, .CODE = "st.lazaridis", .CODE1 = "La@St1776"})
        'uss.Add(New Revera.USER With {.USERS = 9023, .CODE = "e.papadimitriou", .CODE1 = "Pa@Ev1187"})
        'uss.Add(New Revera.USER With {.USERS = 9024, .CODE = "ch.vogiatzis", .CODE1 = "Vo@Ch4873"})
        'uss.Add(New Revera.USER With {.USERS = 9025, .CODE = "erg.organon", .CODE1 = "Erg@Org6529"})
        'uss.Add(New Revera.USER With {.USERS = 9026, .CODE = "s.fourlis", .CODE1 = "Fo@St2883"})
        'uss.Add(New Revera.USER With {.USERS = 9027, .CODE = "a.karakasidis", .CODE1 = "Ka@An3412"})
        'uss.Add(New Revera.USER With {.USERS = 9028, .CODE = "ch.thalassinos", .CODE1 = "Th@Ch1313"})
        'v.karakeisoglou Ka@Va1496
        'g.igglesis hulio1980
        'afarasoglou 7822
        'pili 4017
        'ch.keskesiadis radrad
        'GUser = uss.Where(Function(f) f.CODE = curUser And f.CODE1 = pass).FirstOrDefault

        'If Not IsNothing(GUser) Then
        '    UserId = GUser.USERS
        '    Return True
        'End If


        'Revera, Sertorious
        'Get users only from Revera Company = 4000

        Dim UserManager = GmUserManager.Create(New GmIdentityDbContext)
        Try
            us = GmUserManager.ChkUser(curUser)
            'curGmEntityUser = UserManager.Users.Where(Function(f) f.UserName = curUser And f.Company = 4000).SingleOrDefault
            If IsNothing(us) Then
                'Dim chkUser = GmEntityController.CreateUser(curUser, CurPass, "Users", _Company, CompName, UserId)
                'If Not IsNothing(chkUser) AndAlso chkUser.Succeeded Then
                '    us = UserManager.FindByName(curUser) ' GmEntityController.ChkUser(curUser)
                '    'MsgBox("Ok")
                'Else
                MsgBox("IsNothing(us) " & curUser & vbCrLf, MsgBoxStyle.Critical, "CheckUsers(_Company,CurUser, Pass)")
                'End If
            End If
            If us IsNot Nothing Then
                'Dim result2 = UserManager.PasswordHasher.VerifyHashedPassword(us.PasswordHash, "1q2w3e") 'Ma@Di55489")

                'Dim chkPass = UserManager.PasswordValidator.ValidateAsync("Ma@Di551489") '"Ma@Di54089") 'Me.PasswordTextBox.Text)
                'If chkPass IsNot Nothing Then
                '    Dim result1 = UserManager.ChangePassword(us.Id, "1q2w3e", "Ma@Di55489") 'CurPass) 'OldPassword, Password)
                '    If result1.Succeeded Then
                '        us.encryptPass = CryptoUtils.Encrypt("Ma@Di55489")
                '    End If
                'End If
                CurUserRole = UserManager.GetRoles(us.Id).FirstOrDefault
                Dim gg = UserManager.GetRoles(us.Id).ToList
            End If


            'If Not IsNothing(curGmEntityUser) Then
            '    Dim validUser = UserManager.CheckPassword(curGmEntityUser, pass)
            '    If validUser Then

            '        CurUserRole = UserManager.GetRoles(curGmEntityUser.Id).SingleOrDefault '"Users"
            '        If IsNothing(curGmEntityUser.Users) Then
            '            If Not Utility.ChkUserIfExistInDb(curUser) Then ' If UserId = 0 Then ' Not S1User


            '            End If
            '        End If
            '        If Not curUser = "gmlogic" Then
            '            UserId = curGmEntityUser.Users
            '            If curGmEntityUser.S1User Then
            '                Return False
            '            End If
            '            Return True
            '        End If
            '        CurUserRole = "Developer"
            '    Else
            '        MsgBox("Invalid user " & curUser & vbCrLf, MsgBoxStyle.Critical, "Test_Login(XCOFile, CurUser, Pass, CompanyName)")
            '        Return False
            '    End If
            'Else
            '    'MsgBox("IsNothing(curGmEntityUser) " & curUser & vbCrLf, MsgBoxStyle.Critical, "Test_Login(CurUser, Pass)")
            'End If
        Catch ex As Exception
            MsgBox("192.168.12.201,55555" & vbCrLf & ex.Message)
        End Try
        Return us
    End Function

    Private Sub SetVisibleControls(curUser As String, curUserRole As String)
        If curUserRole = "Admins" Then
            Me.ΑποθήκηToolStripMenuItem.Visible = True
            Me.UsersToolStripMenuItem.Visible = True
            Me.S1ApplicantsToolStripMenuItem.Visible = True
            Me.TransportsToolStripMenuItem.Visible = True
            Exit Sub
        End If
        If CompName.Replace("SERTORIUS", "REVERA") = "REVERA" And ({"Users", "Managers", "1.Γραφέας", "2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου", "Logistics", "Managers", "Pili"}.Contains(curUserRole)) Then
            Me.ΑποθήκηToolStripMenuItem.Visible = True
            If {"Managers"}.Contains(curUserRole) Then
                Me.ΑποθήκηToolStripMenuItem.Visible = False
            End If
            Me.TransportsToolStripMenuItem.Visible = False
            If {"Logistics", "Managers", "Pili"}.Contains(curUserRole) Then
                Me.TransportsToolStripMenuItem.Visible = True
            End If

            Me.UsersToolStripMenuItem.Visible = False
            Me.ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem.Visible = True
            Me.BarCodeGeneratorToolStripMenuItem.Visible = False
            Me.ΔημιουργίαToolStripMenuItem.Visible = False
            Me.ΔιαγραφήToolStripMenuItem.Visible = False
            Me.S1ApplicantsToolStripMenuItem.Visible = False
            SetDBs(dbs.REVERA)
            Exit Sub
        End If

        If curUser = "gmlogic" Then
            'SetDBs(dbs.Test_Hglp)
            For Each mn As ToolStripItem In Me.MenuStrip.Items
                mn.Visible = True
            Next

        ElseIf {"ΣΥΝΤΗΡΗΣΗ"}.Contains(curUser) Then
            Me.Text = "Revera"
            Me.ΑποθήκηToolStripMenuItem.Visible = True
        Else
        End If


    End Sub

    Public Function GetExceptionInfo(ex As Exception) As String
        Dim Result As String
        Dim hr As Integer = Runtime.InteropServices.Marshal.GetHRForException(ex)
        Result = ex.GetType.ToString & "(0x" & hr.ToString("X8") & "): " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine
        Dim st As StackTrace = New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then
                Result &= "Line:" & sf.GetFileLineNumber() & " Filename: " & IO.Path.GetFileName(sf.GetFileName) & Environment.NewLine
            End If
        Next
        Return Result
    End Function
    Private Sub SetDBs(dbno As Integer)
        Select Case dbno
            Case dbs.Test_Centr
                My.Settings.Item("GenConnectionString") = My.Settings.CentroConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.Test_Hglp
                My.Settings.Item("GenConnectionString") = My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.Test_LNK
                My.Settings.Item("GenConnectionString") = My.Settings.LNKConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.Test_LK
                My.Settings.Item("GenConnectionString") = My.Settings.LKConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.Test_PFIC
                My.Settings.Item("GenConnectionString") = My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.Test_REVERA
                My.Settings.Item("GenConnectionString") = My.Settings.HglpConnectionString.ToString.Replace("192.168.12.201,55555", "192.168.10.108\SqlExpress")
            Case dbs.CENTR
                My.Settings.Item("GenConnectionString") = My.Settings.CentroConnectionString.ToString
            Case dbs.HGLP
                My.Settings.Item("GenConnectionString") = My.Settings.HglpConnectionString.ToString
            Case dbs.LNK
                My.Settings.Item("GenConnectionString") = My.Settings.LNKConnectionString.ToString
            Case dbs.LK
                My.Settings.Item("GenConnectionString") = My.Settings.LKConnectionString.ToString
            Case dbs.PFIC
                My.Settings.Item("GenConnectionString") = My.Settings.PFICConnectionString.ToString
            Case dbs.REVERA
                My.Settings.Item("GenConnectionString") = My.Settings.ReveraConnectionString.ToString
            Case dbs.NVF
                My.Settings.Item("GenConnectionString") = My.Settings.NVFConnectionString.ToString
        End Select
        'If My.Settings.GenConnectionString.Contains("192.168.12.201") Then
        '    My.Settings.Item("GenConnectionString") = My.Settings.GenConnectionString.Replace("Data Source=192.168.12.201", "Data Source=192.168.12.201,55555")
        'End If
        Dim conString As New SqlConnectionStringBuilder(My.Settings.GenConnectionString)

        Me.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID & " Company=" & CompName 'CONNECT_STRING
        Me.TlSSTLabelVer.Text = String.Format(" CurrentVersion: {0}", My.Application.Info.Version.ToString)
        If My.Application.IsNetworkDeployed Then
            Me.TlSSTLabelVer.Text = String.Format(" CurrentVersion: {0}", My.Application.Deployment.CurrentVersion.ToString)
        End If
        'Throw New NotImplementedException()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        'SettingForms(sender, e, New ImportExcel, "Import_Negative_Equitys", 0, True)
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    'Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
    '    Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    'End Sub

    'Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
    '    Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    'End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer = 0

    Private Sub SettingForms(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal TForm As Object, ByVal TForm_Text As String, ByVal TPelPro As Byte, ByVal AllowOpenForm As Boolean)
        If AllowOpenForm = False Then
            Exit Sub
        End If
        For Each ChildForm As Object In Me.MdiChildren
            If ChildForm.Name = TForm.Name And ChildForm.Text = TForm_Text Then
                ChildForm.Focus()
                Exit Sub
            End If
        Next
        Try
            TForm.ShowForm()
        Catch ex As Exception
            'MsgBox("ex")
        End Try
        TForm.MdiParent = Me
        TForm.WindowState = FormWindowState.Maximized
        TForm.Text = TForm_Text

        TForm.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub SearchFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchFilesToolStripMenuItem.Click
        'SettingForms(sender, e, New SearchFiles, "SearchFiles", 0, True)
    End Sub

    Private Sub SelectDBasesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles _
        HglpToolStripMenuItem.Click, HglpToolStripMenuItem1.Click, LNKToolStripMenuItem1.Click, LNKToolStripMenuItem.Click,
        CentrToolStripMenuItem1.Click, CentrToolStripMenuItem.Click, PFICToolStripMenuItem1.Click, PFICToolStripMenuItem.Click, LKToolStripMenuItem1.Click, LKToolStripMenuItem.Click, NVFToolStripMenuItem.Click

        Dim s As ToolStripMenuItem = sender
        SetDBs(s.Tag)
    End Sub

    Private Sub ImportNegativeEquitiesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        SettingForms(sender, e, New Import_Negative_Equitys, "Import_Negative_Equitys", 0, True)
    End Sub

    Private Sub ΕΝΟΠΟΙΗΣΗEXCELToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'SettingForms(sender, e, New UnificationExcel, "Unification Excel", 0, True)
    End Sub

    Private Sub ImportEAPToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'SettingForms(sender, e, New ImportEAP_TXT, "ImportEAP", 0, True)
    End Sub


    Private Sub CreateΠΑΝΔΕΚΤΗΣToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'SettingForms(sender, e, New Pandektis, "Create ΠΑΝΔΕΚΤΗΣ", 0, True)
    End Sub

    Private Sub CreateEAPToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'SettingForms(sender, e, New CreateEAP, "Create EAP", 0, True)
    End Sub

    Private Sub ImportN3869ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'SettingForms(sender, e, New ImportN3869, "Import N3869", 0, True)
    End Sub
    Private Sub TestConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestConnectionToolStripMenuItem.Click
        Dim TStrConn = TestConn(My.Settings.GenConnectionString)
        If Not TStrConn.Contains("Error") Then
            MsgBox("Eπιτυχής Σύνδεση " & TStrConn, MsgBoxStyle.Information)
        Else
            MsgBox("Προσοχή !!!. Διακοπή Σύνδεσης" & vbCrLf & TStrConn, MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub ΑποθήκηToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ΥπόλοιπαΕιδώνToolStripMenuItem.Click, ΕκκρεμείςΠαραγγελίεςToolStripMenuItem.Click, ΑίτησηΠαραγγελίαςToolStripMenuItem.Click
        Dim s As ToolStripMenuItem = sender
        Dim frm As New WHouseBal
        frm.Tag = CompName ' "REVERA"
        If s.Text = "Εκτύπωση Ετικετών Barcode" Or s.Text = "Περιγραφή Ετικέτας" Then
            frm.Tag = "Hglp"
        End If
        frm.Text = s.Text
        SettingForms(sender, e, frm, Me.ΑποθήκηToolStripMenuItem.Text & " - " & s.Text, 0, True)
    End Sub


    Private Sub ImportExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportExcelToolStripMenuItem.Click
        SettingForms(sender, e, New ImportExcel, ImportExcel.Text, 0, True)
    End Sub

    Private Sub ChangeSysDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSysDateToolStripMenuItem.Click
        Dim loginf As New LoginForm1
        loginf.Text = "Εισαγωγή στοιχείων"
        'loginf.UsernameTextBox.Text = "User"
        loginf.UsernameTextBox.Text = CurUser
        'Dim Pass = "1mgergm++"
        loginf.PasswordTextBox.Text = CurPass
        'loginf.CompName(0) = CompName ' XCOs.ToList
        loginf.ShowDialog()
        CurUser = loginf.UsernameTextBox.Text
        CurPass = loginf.PasswordTextBox.Text
        CTODate = loginf.DateTimePicker1.Value

        CompName = loginf.ddlXCOs.SelectedItem

        Dim login As Boolean = Test_Login(CurUser, CurPass)
        If Not login Then
            MsgBox("Login Error " & CurUser, MsgBoxStyle.Critical, "GenMenu_Test_Login(XCOFile, CurUser, CurPass, CompanyName)")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub ΔημιουργίαToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ΔημιουργίαToolStripMenuItem.Click, ΔιόρθωσηToolStripMenuItem.Click, ΔιαγραφήToolStripMenuItem.Click
        Dim s As ToolStripMenuItem = sender
        Dim loginf As New AAGenLogin
        loginf.Action = s.Tag.ToString
        loginf.ShowDialog()
    End Sub

    Private Sub ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) 'Handles ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem.Click
        SettingForms(sender, e, New BCLabelsBrOLd, Me.ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem.Text, 0, True)
    End Sub

    Private Sub BarCodeGeneratorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarCodeGeneratorToolStripMenuItem.Click
        GenBarCode.ShowDialog()
    End Sub

    Private Sub TestBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestBarcodeToolStripMenuItem.Click
        TestBarcode.ShowDialog()
    End Sub

    Private Sub ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ΕκτύπωσηΕτικετώνBarcodeToolStripMenuItem.Click, ΠεριγραφήΕτικέταςToolStripMenuItem.Click
        Dim s As ToolStripMenuItem = sender
        Dim frm As New BCLabelsBr
        'frm.Tag = "REVERA"
        'If s.Text = "Εκτύπωση Ετικετών Barcode" Or s.Text = "Περιγραφή Ετικέτας" Then
        frm.Tag = "Hglp"
        'End If
        frm.Text = s.Text
        SettingForms(sender, e, frm, Me.ΑποθήκηToolStripMenuItem.Text & " - " & s.Text, 0, True)
    End Sub

    Private Sub S1ApplicantsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles S1ApplicantsToolStripMenuItem.Click
        SettingForms(sender, e, New S1Applicants, Me.S1ApplicantsToolStripMenuItem.Text, 0, True)
    End Sub

    Private Sub TransportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransportsToolStripMenuItem.Click
        SettingForms(sender, e, New Transport, Me.TransportsToolStripMenuItem.Text, 0, True)
    End Sub


End Class