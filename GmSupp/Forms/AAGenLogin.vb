Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class AAGenLogin
    Dim db As New DataClassesReveraDataContext
    Property Action As String
    Private Applicants As List(Of Revera.UFTBL01)
    Dim LUserManager As GmUserManager = GmUserManager.Create(New GmIdentityDbContext)
    Private role1 As String
    Dim rols As New Dictionary(Of Short, String)
    Private Sub AAGenLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Dim RoleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(New GmEntityDbContext))

        'Dim user As GmEntityUser = UserManager.Users.Where(Function(f) f.UserName = CurUser).FirstOrDefault

        ''UserManager.Delete(user)
        'If Not IsNothing(user) Then
        '    curGmEntityUser = user
        '    Dim role = RoleManager.Roles.Where(Function(f) f.Users.FirstOrDefault.UserId = user.Id).FirstOrDefault
        '    If Not IsNothing(role) Then
        '        AAGenModule.CurUserRole = role.Name
        '    End If
        'End If

        'CurUserRole = UserManager.GetRoles(curGmEntityUser.Id).FirstOrDefault
        Dim uss = LUserManager.Users.Where(Function(f) Not f.UserName = "gmlogic").OrderBy(Function(f) f.UserName).ToList
        If CurUserRole = "Admins" Then
            uss = uss.Where(Function(f) f.S1User = False).OrderBy(Function(f) f.UserName).ToList
            Dim gg = uss.Select(Function(f) f.Roles).ToList
        End If

        Dim emptyUsers() As GmIdentityUser
        emptyUsers = {New GmIdentityUser With {.UserName = "<Επιλέγξτε>"}}

        uss = (emptyUsers.ToList.Union(uss.ToList)).ToList

        Me.ddlUsers.DataSource = uss 'ddlUsers.SelectedIndexChanged
        Me.ddlUsers.DisplayMember = "UserName" 'ddlUsers.SelectedIndexChanged
        Me.ddlUsers.ValueMember = "Id"

        Dim conString As New SqlClient.SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conString.DataSource = "192.168.12.201,55555"
        conString.InitialCatalog = "Revera"
        db.Connection.ConnectionString = conString.ConnectionString

        UserManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString
        RoleManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString


        Dim emptyApplicant() As Revera.UFTBL01
        emptyApplicant = {New Revera.UFTBL01 With {.NAME = "<Επιλέγξτε>", .UFTBL01 = 0}}

        Applicants = (emptyApplicant.ToList.Union(db.UFTBL01s.Where(Function(f) f.COMPANY = 5000 And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList
        Me.ddlApplicant.DataSource = Applicants

        Dim Result As Dictionary(Of Short, String) = db.UFTBL01s.Where(Function(f) f.COMPANY = 5000 And f.SOSOURCE = 1251).OrderBy(Function(f) f.NAME).ToDictionary(Function(f) f.UFTBL01, Function(f) f.NAME)
        Dim no As Integer = 0

        'For Each us In uss
        '    If us.UserName = "<Επιλέγξτε>" Then
        '        Continue For
        '    End If



        Dim roles As List(Of String) = RoleManager.Roles.Select(Function(f) f.Name).ToList ' UserManager.GetRoles(us.Id).ToList
        For Each ro In roles
            If rols.Count = 0 Then
                no += 1
                rols.Add(no, ro)
            End If

            Dim r1 = rols.Where(Function(f) f.Value = ro).FirstOrDefault
            If r1.Value Is Nothing Then
                no += 1
                rols.Add(no, ro)
            End If

        Next
        'Next


        Dim Highers As New Dictionary(Of String, String)
        Highers.Add("<Επιλέγξτε>", 0)
        Dim usss = LUserManager.Users '.Where(Function(f)  f..OrderBy(Function(f) f.Name).ToList
        For Each u In usss.OrderBy(Function(f) f.Name).ToList
            If u.Name.Trim = "" Then
                Continue For
            End If
            For Each f1 In UserManager.GetRoles(u.Id).Where(Function(f) {"2.Μηχανικός", "3.Προϊστάμενος", "4.Διευθυντής τμήματος", "5.Διευθυντής Εργοστασίου"}.Contains(f))
                Highers.Add(u.Name, u.Id)
            Next
        Next
        ''Highers = UserManager.Users.Select(.ge'= (Highers.Add(emptyHigher).ToList.Union(emptyHigher.ToList)).ToList 'db.UFTBL01s.Where(Function(f) f.COMPANY = 5000 And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList
        Me.ddlΗighers.DisplayMember = "Key"
        Me.ddlΗighers.ValueMember = "Value"
        Me.ddlΗighers.DataSource = Highers.ToList




        'Dim noduplicates As IEnumerable(Of Dictionary(Of Short, String)) = Result.Distinct(New ProductComparer())

        'For Each product In noduplicates
        '    Console.WriteLine(product.Name & " " + product.Code)
        'Next

        'Debug.Print("Values inserted into dictionary:")
        'For Each dic As KeyValuePair(Of Short, String) In Result
        '    Debug.Print([String].Format("English salute {0} is {1} in German", dic.Key, dic.Value))
        'Next




        'Me.GmChkListBoxAplicant.Height = 33


        Me.GmChkListBoxRoles.dgv.DataSource = rols.ToList ' Result.ToList
        Me.GmChkListBoxRoles.dgv_Styling()
        'Me.GmChkListBoxAplicant.BringToFront()


        Me.txtBoxDecrypt.Visible = False
        Me.PanelApplicant.Visible = False

        If Action = "Add" Then
            Me.ddlUsers.Visible = False
            Me.btnReset.Visible = False
            Me.lblRoles.Visible = False
            Me.GmChkListBoxRoles.Visible = False
            Me.btnSetRoles.Visible = False

            Me.Label3.Visible = False
            Me.Label4.visible = False
            Me.ddlΗighers.Visible = False
            Me.btnDeleteHigher.Visible = False
            Me.Text = "Προσθήκη Χρήστη"
        End If

        If Action = "Edit" Then
            Me.Text = "Διόρθωση Χρήστη"
            If CurUserRole = "Managers" Or CurUserRole = "Users" Then
                Me.ddlUsers.Visible = False
                Me.UsernameTextBox.Text = curGmEntityUser.UserName
                Me.lblUserName.Visible = False
                Me.UsernameTextBox.Visible = False
                Me.lblOldPass.Visible = True
                Me.txtBoxOldPass.Visible = True
            End If
            If CurUserRole = "Admins" Then
                'Me.ddlUsers.Visible = False
                'Me.UsernameTextBox.Text = curGmEntityUser.UserName
                'Me.lblUserName.Visible = False
                'Me.UsernameTextBox.Visible = False
                'Me.lblOldPass.Visible = True
                'Me.txtBoxOldPass.Visible = True
                'Me.GroupBoxRoles.Visible = False
                Me.txtBoxDecrypt.Visible = True
            End If
        End If

        If Action = "Delete" Then
            Me.lblOldPass.Visible = False
            Me.txtBoxOldPass.Visible = False
            Me._lblLabels_1.Visible = False
            Me.PasswordTextBox.Visible = False
            Me.Label1.Visible = False
            Me.txtboxConfirmPass.Visible = False
            Me.btnReset.Visible = False
            Me.PanelApplicant.Visible = False
            Me.lblRoles.Visible = False
            Me.GmChkListBoxRoles.Visible = False
            Me.btnSetRoles.Visible = False
            Me.Label3.Visible = False
            Me.Label4.Visible = False
            Me.ddlΗighers.Visible = False
            Me.btnDeleteHigher.Visible = False
            Me.Text = "Διαγραφή Χρήστη"
        End If

        If LocalIP = "192.168.10.1081" Or CurUserRole = "Developer" Then
            Me.cmdSelect.Visible = True
            'Action = "Edit"
            Dim s As String = "Hello Extension Methods"
            Dim i As Integer = s.WordCount()
            Me.txtBoxRole.Visible = True
            Me.btnAddRole.Visible = True
        End If

    End Sub
    Public Class test

        Property Check As Boolean
        Property idx As Integer

        Property Name As String

    End Class
    Private Sub Cmd_Select()
        My.Settings.Item("GenConnectionString") = My.Settings.ReveraConnectionString
        's1Conn
        'Dim UserManager = GmEntityUserManager.Create(New GmEntityDbContext)
        Dim users = LUserManager.Users.Where(Function(f) Not f.UserName = "gmlogic").OrderBy(Function(f) f.UserName).ToList
        Dim tt = ""
        For Each user In users
            If Not IsNothing(user) Then
                Dim usClaim = LUserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm").FirstOrDefault
                Dim decryptPass = ""
                decryptPass = CryptoUtils.Decrypt(user.encryptPass)

                If Not IsNothing(usClaim) Then
                    Dim encryptPass = usClaim.Value
                    decryptPass = CryptoUtils.Decrypt(encryptPass)

                    If Not user.encryptPass = encryptPass Then
                        user.encryptPass = usClaim.Value
                    End If
                    usClaim = LUserManager.GetClaims(user.Id).Where(Function(f) f.Type = "S1").FirstOrDefault
                    Dim soPassword As String = Utility.soPassword(user.Users, decryptPass)
                    If Not soPassword = usClaim.Value Then
                        user.soPassword = soPassword 'usClaim.Value
                    End If
                    'UserManager.Update(user)
                End If
                Try
                    Debug.Print(user.UserName & vbTab & decryptPass & vbTab & user.soPassword)
                    tt &= user.UserName & vbTab & decryptPass & vbTab & user.soPassword & vbCrLf
                Catch ex As Exception

                End Try

            End If
        Next
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try


            Dim uss As New List(Of Revera.USER)
            '9005    ΣΥΝΤΗΡΗΣΗ	ΣΥΝΤΗΡΗΣΗ	901	7845123
            '9007    g.kazoglou	g.kazoglou	901	Ka@Ge1744
            '9008    a.dimzas	a.dimzas	901	Di@Ap2687
            '9009    g.bampatzanis	g.bampatzanis	901	Ba@Ge7846
            '9010    k.chaitas	k.chaitas	901	Ch@Ko2124
            '9011    k.kontogeorgos	k.kontogeorgos	901	Ko@Ko9856
            '9012    a.zdratskidis	a.zdratskidis	901	Zd@At4247
            '9013    ch.vogiatzi	ch.vogiatzi	901	Vo@Ch1800
            '9014    a.zygoulas	a.zygoulas	901	Zy@An0249
            '9015    k.matakas	k.matakas	901	Ma@Ky9912
            '9016    p.gkoutziamanis	p.gkoutziamanis	901	Gk@Pa0245
            '9017    g.stilianidis	g.stilianidis	901	St@Ge4932
            '9018    l.soulis	l.soulis	901	So@La4682
            '9019    s.lazaridis	s.lazaridis	901	La@So8732
            '9020    g.belitsios	g.belitsios	901	Be@Io4873
            '9021    n.apostolidis	n.apostolidis	901	Ap@Ne5478
            '9022    st.lazaridis	st.lazaridis	901	La@St1776
            '9023    e.papadimitriou	e.papadimitriou	901	Pa@Ev1187
            '9024    ch.vogiatzis	ch.vogiatzis	901	Vo@Ch4873
            '9025    erg.organon	erg.organon	901	Erg@Org6529
            '9026    s.fourlis	s.fourlis	901	Fo@St2883
            '9027    a.karakasidis	a.karakasidis	901	Ka@An3412
            '9028    ch.thalassinos	ch.thalassinos	901	Th@Ch1313
            uss.Add(New Revera.USER With {.USERS = 9005, .CODE = "ΣΥΝΤΗΡΗΣΗ", .CODE1 = "7845123"})
            uss.Add(New Revera.USER With {.USERS = 9007, .CODE = "g.kazoglou", .CODE1 = "Ka@Ge1744"})
            uss.Add(New Revera.USER With {.USERS = 9008, .CODE = "a.dimzas", .CODE1 = "Di@Ap2687"})
            uss.Add(New Revera.USER With {.USERS = 9009, .CODE = "g.bampatsanis", .CODE1 = "Ba@Ge7846"})
            uss.Add(New Revera.USER With {.USERS = 9010, .CODE = "k.chaitas", .CODE1 = "Ch@Ko2124"})
            uss.Add(New Revera.USER With {.USERS = 9011, .CODE = "k.kontogeorgos", .CODE1 = "Ko@Ko9856"})
            uss.Add(New Revera.USER With {.USERS = 9012, .CODE = "a.zdratskidis", .CODE1 = "Zd@At4247"})
            uss.Add(New Revera.USER With {.USERS = 9013, .CODE = "ch.vogiatzi", .CODE1 = "Vo@Ch1800"})
            uss.Add(New Revera.USER With {.USERS = 9014, .CODE = "a.zygoulas", .CODE1 = "Zy@An0249"})
            uss.Add(New Revera.USER With {.USERS = 9015, .CODE = "k.matakas", .CODE1 = "Ma@Ky9912"})
            uss.Add(New Revera.USER With {.USERS = 9016, .CODE = "p.gkoutziamanis", .CODE1 = "Gk@Pa0245"})
            uss.Add(New Revera.USER With {.USERS = 9017, .CODE = "g.stilianidis", .CODE1 = "St@Ge4932"})
            uss.Add(New Revera.USER With {.USERS = 9018, .CODE = "l.soulis", .CODE1 = "So@La4682"})
            uss.Add(New Revera.USER With {.USERS = 9019, .CODE = "s.lazaridis", .CODE1 = "La@So8732"})
            uss.Add(New Revera.USER With {.USERS = 9020, .CODE = "g.belitsios", .CODE1 = "Be@Io4873"})
            uss.Add(New Revera.USER With {.USERS = 9021, .CODE = "n.apostolidis", .CODE1 = "Ap@Ne5478"})
            uss.Add(New Revera.USER With {.USERS = 9022, .CODE = "st.lazaridis", .CODE1 = "La@St1776"})
            uss.Add(New Revera.USER With {.USERS = 9023, .CODE = "e.papadimitriou", .CODE1 = "Pa@Ev1187"})
            uss.Add(New Revera.USER With {.USERS = 9024, .CODE = "ch.vogiatzis", .CODE1 = "Vo@Ch4873"})
            uss.Add(New Revera.USER With {.USERS = 9025, .CODE = "erg.organon", .CODE1 = "Erg@Org6529"})
            uss.Add(New Revera.USER With {.USERS = 9026, .CODE = "s.fourlis", .CODE1 = "Fo@St2883"})
            uss.Add(New Revera.USER With {.USERS = 9027, .CODE = "a.karakasidis", .CODE1 = "Ka@An3412"})
            uss.Add(New Revera.USER With {.USERS = 9028, .CODE = "ch.thalassinos", .CODE1 = "Th@Ch1313"})

            If Action = "Add" Then

                If Not Me.UsernameTextBox.Text = "" AndAlso Not Me.PasswordTextBox.Text = "" AndAlso Not Me.txtBoxName.Text = "" AndAlso Not Me.txtboxConfirmPass.Text = "" AndAlso Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text Then
                    Dim roleName = "Users"
                    'If Me.radioBtnManagers.Checked Then
                    '    roleName = Me.radioBtnManagers.Text
                    'End If
                    'If Me.radioBtnUsers.Checked Then
                    '    roleName = Me.radioBtnUsers.Text
                    'End If
                    'If Me.radioBtnS1Users.Checked Then
                    '    roleName = Me.radioBtnS1Users.Text
                    'End If

                    Dim result1 = GmUserManager.CreateUser(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text, Me.txtBoxName.Text, roleName, Company) 'Add with UserID,Users=0
                    If Not IsNothing(result1) AndAlso result1.Succeeded Then
                        MsgBox("Ok")
                        Me.UsernameTextBox.Text = ""
                        Me.PasswordTextBox.Text = ""
                        Me.txtBoxName.Text = ""
                        Me.txtboxConfirmPass.Text = ""
                        Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text
                    Else
                        Dim errs = ""
                        For Each er In result1.Errors
                            errs &= er & vbCrLf
                        Next
                        If MsgBox("Λάθος Όνομα, Κωδικός" & vbCrLf & errs, MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                            Exit Sub
                        End If
                    End If
                Else
                    If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                        Exit Sub
                    End If
                End If
            End If
            If Action = "Edit" Then
                If Not Me.UsernameTextBox.Text = "" AndAlso Not Me.PasswordTextBox.Text = "" AndAlso Not Me.txtboxConfirmPass.Text = "" AndAlso Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text Then
                    Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault
                    If Not IsNothing(user) Then
                        'If Not (role = "Managers" Or role = "Users") Then
                        'Dim usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm").FirstOrDefault
                        'If Not IsNothing(usClaim) Then
                        '    Dim decryptPass = CryptoUtils.Decrypt(usClaim.Value)
                        '    Me.txtBoxOldPass.Text = decryptPass 'user.GetPassword()
                        'Else
                        Dim cuser As GmIdentityUser = GmUserManager.ChkUser(Me.UsernameTextBox.Text)
                        If Not IsNothing(cuser) Then
                            Dim decryptPass = CryptoUtils.Decrypt(cuser.encryptPass)
                            Me.txtBoxOldPass.Text = decryptPass 'user.GetPassword()
                            If (CurUser = "gmlogic" Or CurUserRole = "Admins") Then
                                Me.txtBoxDecrypt.Visible = True
                                Me.txtBoxDecrypt.Text = decryptPass
                            End If
                            If Not Me.txtBoxName.Text = cuser.Name Then
                                cuser.Name = Me.txtBoxName.Text
                                GmUserManager.UpdateUser(cuser)
                            End If

                            Dim HigherS = Me.ddlΗighers.SelectedValue
                            If Not HigherS = "0" Then
                                Dim usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Higher" And f.Value = HigherS).FirstOrDefault

                                If usClaim Is Nothing Then
                                    usClaim = New Security.Claims.Claim("Higher", Me.ddlΗighers.SelectedValue)
                                    UserManager.AddClaim(user.Id, usClaim)
                                End If
                                Me.txtBoxΗigher.Text = ""
                                Dim usClaims = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher") '.FirstOrDefault
                                For Each cl In usClaims
                                    Dim u = LUserManager.FindById(cl.Value)
                                    If u IsNot Nothing Then
                                        Me.txtBoxΗigher.Text &= u.Name & "|"
                                    End If
                                Next
                                If Not Me.txtBoxΗigher.Text = "" Then
                                    Me.txtBoxΗigher.Text = Me.txtBoxΗigher.Text.Substring(0, Me.txtBoxΗigher.Text.Length - 1)
                                End If
                            End If





                        End If

                        'End If
                        'End If
                        If Not Me.txtBoxOldPass.Text = "" AndAlso Not Me.PasswordTextBox.Text = "" AndAlso Not Me.txtboxConfirmPass.Text = "" AndAlso Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text Then
                            Dim okk = UserManager.CheckPassword(user, Me.PasswordTextBox.Text)
                            If okk Then
                                MsgBox("Pass Ok")
                                Exit Sub
                            End If
                            okk = UserManager.CheckPassword(user, Me.txtBoxOldPass.Text)
                            okk = UserManager.HasPassword(user.Id)
                            Dim result1 As IdentityResult = UserManager.ChangePassword(user.Id, Me.txtBoxOldPass.Text, Me.PasswordTextBox.Text)
                            'result1 = UserManager.AddPassword(user.Id, Me.PasswordTextBox.Text)
                            'result1 = UserManager.RemovePassword(user.Id)
                            If result1.Succeeded Then
                                okk = GmUserManager.SetEncryptPass(user, Me.PasswordTextBox.Text)
                                'Dim usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm").FirstOrDefault
                                'If Not IsNothing(usClaim) Then
                                '    Dim encryptPass = CryptoUtils.Encrypt(Me.PasswordTextBox.Text)
                                '    UserManager.RemoveClaim(user.Id, usClaim)
                                '    usClaim = New Security.Claims.Claim("Gm", encryptPass)
                                '    UserManager.AddClaim(user.Id, usClaim)
                                '    Dim decryptPass = CryptoUtils.Decrypt(encryptPass)
                                '    'UserManager.RemoveClaim(user.Id, usClaim)
                                'End If
                                okk = UserManager.CheckPassword(user, Me.PasswordTextBox.Text)
                                If okk Then
                                    MsgBox("Pass Changed")
                                Else
                                    MsgBox("Error")
                                End If
                            Else
                                Dim errs = ""
                                For Each er In result1.Errors
                                    errs &= er & vbCrLf
                                Next
                                MsgBox(errs)
                            End If
                        Else
                            If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                        Exit Sub
                    End If
                End If
                Me.Close()
            End If
            If Action = "Edit1" Then
                Me.UsernameTextBox.Text = Me.ddlUsers.SelectedItem.UserName
                If Not Me.UsernameTextBox.Text = "" AndAlso Not Me.PasswordTextBox.Text = "" AndAlso Not Me.txtboxConfirmPass.Text = "" AndAlso Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text Then
                    Dim user As GmIdentityUser = Nothing 'GmEntityController.ChkUser(Me.UsernameTextBox.Text)
                    If Not IsNothing(user) Then
                        If Not (CurUserRole = "Managers" Or CurUserRole = "Users") Then
                            Dim decryptPass = CryptoUtils.Decrypt(user.encryptPass)
                            Me.txtBoxOldPass.Text = decryptPass 'user.GetPassword()
                        End If
                        Dim result1 As New IdentityResult
                        If Not Me.txtBoxOldPass.Text = "" AndAlso Not Me.PasswordTextBox.Text = "" AndAlso Not Me.txtboxConfirmPass.Text = "" AndAlso Me.PasswordTextBox.Text = Me.txtboxConfirmPass.Text Then
                            Dim roleName = "Users"
                            'If Me.radioBtnManagers.Checked Then
                            '    roleName = Me.radioBtnManagers.Text
                            'End If
                            'If Me.radioBtnUsers.Checked Then
                            '    roleName = Me.radioBtnUsers.Text
                            'End If
                            'If Me.radioBtnS1Users.Checked Then
                            '    roleName = Me.radioBtnS1Users.Text
                            'End If
                            'Dim newRoleName As String = Nothing
                            'Dim UsRole = user.Roles.SingleOrDefault
                            'If Not IsNothing(UsRole) Then
                            '    UsRole.RoleId
                            'End If

                            'Dim chkPass = UserManager.CheckPassword(user, "Ma@Di54089") '"Ma@Di54089") 'Me.txtBoxOldPass.Text)
                            'Dim PassResult As IdentityResult = Nothing
                            'Dim Context = New GmEntityDbContext

                            'Dim UserManager = GmEntityUserManager.Create(Context)
                            Dim result2 = LUserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, Me.txtBoxDecrypt.Text) '"Ma@Di55489")

                            Dim chkPass = LUserManager.CheckPassword(user, Me.txtBoxDecrypt.Text) ' "Ma@Di55489") '"Ma@Di54089") 'Me.PasswordTextBox.Text)
                            If Not chkPass Then
                                result1 = LUserManager.ChangePassword(user.Id, Me.txtBoxDecrypt.Text, Me.PasswordTextBox.Text) 'OldPassword, Password)
                            End If
                            'result1 = GmEntityController.ChangePassword(UserManager, user, Me.txtBoxOldPass.Text, Me.PasswordTextBox.Text, roleName)
                            If result1.Succeeded Then
                                user.encryptPass = CryptoUtils.Encrypt(Me.PasswordTextBox.Text)
                                MsgBox("Ok")
                            End If

                        Else
                            Dim errs = ""
                            For Each er In result1.Errors
                                errs &= er & vbCrLf
                            Next
                            MsgBox(errs)
                        End If
                    Else
                        If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                            Exit Sub
                        End If
                    End If
                End If
                Me.Close()
            End If

            If Action = "Delete" Then
                'For Each us In uss
                '    Dim chkPass = s1Conn.UserValidate(us.CODE, us.CODE1)
                '    If Not chkPass Then
                '        Dim result1 As IdentityResult = GmEntityController.DeleteUser(us.CODE)
                '        If result1.Succeeded Then
                '            'MsgBox("Ok")
                '        Else
                '            Dim errs = ""
                '            For Each er In result1.Errors
                '                errs &= er & vbCrLf
                '            Next
                '            MsgBox(errs)
                '        End If
                '    End If

                'Next

                If Not Me.UsernameTextBox.Text = "" Then
                    Dim result1 As IdentityResult = GmUserManager.DeleteUser(Me.UsernameTextBox.Text)
                    If Not IsNothing(result1) AndAlso result1.Succeeded Then
                        MsgBox("Ok")
                    Else
                        Dim errs = ""
                        If result1 IsNot Nothing Then
                            For Each er In result1.Errors
                                errs &= er & vbCrLf
                            Next
                        End If
                        MsgBox(errs)
                    End If
                    Me.Close()
                    '    'Dim u As MembershipUser = Membership.GetUser(Me.UsernameTextBox.Text)
                    '    Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault
                    '    If Not IsNothing(user) Then
                    '        If MsgBox("Προσοχή !!! Διαγραφή Χρήστη: " & user.UserName, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Ok Then
                    '            Dim result1 As IdentityResult = UserManager.Delete(user)
                    '            If result1.Succeeded Then
                    '                MsgBox("Ok")
                    '            Else
                    '                Dim errs = ""
                    '                For Each er In result1.Errors
                    '                    errs &= er & vbCrLf
                    '                Next
                    '                MsgBox(errs)
                    '            End If
                    '            Me.Close()
                    '        End If
                    '    Else
                    '        If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                    '            Exit Sub
                    '        End If
                    '    End If
                    'Else
                    '    If MsgBox("Λάθος Όνομα, Κωδικός", MsgBoxStyle.Critical Or MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
                    '        Exit Sub
                    '    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnAddRole_Click(sender As Object, e As EventArgs) Handles btnAddRole.Click

        Dim result1 = RoleManager.Create(New IdentityRole With {.Name = Me.txtBoxRole.Text})
        Dim errs = ""
        If result1.Succeeded Then
            errs = "Add Roles Ok"
        Else
            For Each er In result1.Errors
                errs &= er & vbCrLf
            Next
        End If
        MsgBox(errs)
    End Sub
    Private Sub btnSetRoles_Click(sender As Object, e As EventArgs) Handles btnSetRoles.Click
        If Not Me.UsernameTextBox.Text = "" Then
            Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault
            If user Is Nothing Then
                MsgBox("User Error:" & Me.UsernameTextBox.Text)
                Exit Sub
            End If
            'If getRoles(Me.UsernameTextBox.Text) Then
            Dim result1 = setRoles(Me.UsernameTextBox.Text)
            Dim errs = ""
            If result1.Succeeded Then
                errs = "Set Roles Ok"
            Else
                For Each er In result1.Errors
                    errs &= er & vbCrLf
                Next
            End If
            MsgBox(errs)
            'End If
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If Me.ddlUsers.SelectedItem IsNot Nothing Then
            Me.UsernameTextBox.Text = Me.ddlUsers.SelectedItem.UserName
        Else
            Me.UsernameTextBox.Text = Me.ddlUsers.Text
        End If
        If Action = "Edit" Then
            If Not Me.UsernameTextBox.Text = "" Then
                'Dim u As MembershipUser = Membership.GetUser(Me.UsernameTextBox.Text)
                Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault
                Dim okk As Boolean
                Dim errs = ""
                If Not IsNothing(user) Then
                    'If Not (role = "Managers" Or role = "Users") Then
                    Dim result1 As New IdentityResult
                    result1 = UserManager.RemovePassword(user.Id)

                    If result1.Succeeded Then
                        Me.PasswordTextBox.Text = Utility.GetRandomPassword(6)
                        result1 = UserManager.AddPassword(user.Id, Me.PasswordTextBox.Text)
                        If result1.Succeeded Then
                            okk = GmUserManager.SetEncryptPass(user, Me.PasswordTextBox.Text)
                            If okk Then
                                Me.txtboxConfirmPass.Text = Me.PasswordTextBox.Text
                                Me.txtBoxDecrypt.Text = Me.PasswordTextBox.Text
                            End If
                        Else
                            For Each er In result1.Errors
                                errs &= er & vbCrLf
                            Next
                        End If
                    Else
                        For Each er In result1.Errors
                            errs &= er & vbCrLf
                        Next
                    End If
                    'End If
                    okk = UserManager.CheckPassword(user, Me.PasswordTextBox.Text)
                    If okk Then
                        MsgBox("Pass Changed")
                    Else
                        MsgBox(errs)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnDeleteHigher_Click(sender As Object, e As EventArgs) Handles btnDeleteHigher.Click
        If Not Action = "Edit" Then
            Exit Sub
        End If

        Dim HigherS = Me.ddlΗighers.SelectedValue
        If HigherS = "0" Then
            MsgBox("Λάθος επιλογή", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Me.ddlUsers.SelectedItem IsNot Nothing Then
            Me.UsernameTextBox.Text = Me.ddlUsers.SelectedItem.UserName
        Else
            Me.UsernameTextBox.Text = Me.ddlUsers.Text
        End If
        If Me.UsernameTextBox.Text = "<Επιλέγξτε>" Then
            Exit Sub
        End If
        Dim cuser As GmIdentityUser = GmUserManager.ChkUser(Me.UsernameTextBox.Text)



        If Not IsNothing(cuser) Then
            Dim usClaim = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher" And f.Value = HigherS).FirstOrDefault

            If usClaim IsNot Nothing Then
                UserManager.RemoveClaim(cuser.Id, usClaim)
            End If

            Me.txtBoxΗigher.Text = ""
            Dim usClaims = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher") '.FirstOrDefault
            For Each cl In usClaims
                Dim u = LUserManager.FindById(cl.Value)
                If u IsNot Nothing Then
                    Me.txtBoxΗigher.Text &= u.Name & "|"
                End If
            Next
        End If

    End Sub
    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Cmd_Select()
    End Sub

    Private Sub ddlUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUsers.SelectedIndexChanged
        If Action = "Add" Then
            Exit Sub
        End If
        If Me.ddlUsers.SelectedItem IsNot Nothing Then
            Me.UsernameTextBox.Text = Me.ddlUsers.SelectedItem.UserName
        Else
            Me.UsernameTextBox.Text = Me.ddlUsers.Text
        End If
        If Me.UsernameTextBox.Text = "<Επιλέγξτε>" Then
            Exit Sub
        End If
        If getRoles(Me.UsernameTextBox.Text) Then

        End If
        Dim cuser As GmIdentityUser = GmUserManager.ChkUser(Me.UsernameTextBox.Text)
        If Not IsNothing(cuser) Then
            Me.txtBoxName.Text = cuser.Name
        End If


        Me.txtBoxΗigher.Text = ""
        Dim usClaims = UserManager.GetClaims(cuser.Id).Where(Function(f) f.Type = "Higher") '.FirstOrDefault
        For Each cl In usClaims
            Dim u = LUserManager.FindById(cl.Value)
            If u IsNot Nothing Then
                Me.txtBoxΗigher.Text &= u.Name & "|"
            End If
        Next





        ''Me.ddlΗigher.DataSource.Clear
        'Dim Highers As New Dictionary(Of String, String)
        'Highers.Add("<Επιλέγξτε>", 0)
        'Dim usss = LUserManager.Users '.Where(Function(f)  f..OrderBy(Function(f) f.Name).ToList
        'For Each u In usss.OrderBy(Function(f) f.Name).ToList
        '    If u.Name.Trim = "" Then
        '        'Continue For
        '    End If
        '    Dim ff = UserManager.GetRoles(cuser.Id)
        '    'Dim f2 = UserManager.GetRoles(u.Id).Where(Function(f) {"2.Διευθυντής Εργοστασίου", "3.Διευθυντής τμήματος", "4.Προϊστάμενος"}.Contains(f))
        '    'If f2.Count > 0 Then
        '    '    Highers.Add(u.Name, u.Id)
        '    'End If
        '    For Each f1 In UserManager.GetRoles(u.Id).Where(Function(f) {"2.Διευθυντής Εργοστασίου", "3.Διευθυντής τμήματος", "4.Προϊστάμενος"}.Contains(f))
        '        Highers.Add(u.Name, u.Id)
        '    Next

        '    ''Dim tt = RoleManager.Roles.Where(Function(f) f.Users.Contains(New IdentityUserRole With {.UserId = u.Id, .RoleId}))
        '    'For Each rs In {"2.Διευθυντής Εργοστασίου", "3.Διευθυντής τμήματος", "4.Προϊστάμενος"}

        '    '        Dim iro = UserManager.IsInRole(u.Id, rs)
        '    '        If Not iro Then

        '    '        End If
        '    '    Next
        '    '    Dim gg = RoleManager.Roles.ToList '.Select(Function(f) f.Users).ToList
        '    '    '.Contains()
        '    '    'gg = gg.Where(Function(f) f.Users.Contains(u)).ToList
        '    '    'If u.Roles.Where(Function(f) .Where(Function(f) RoleManager.Roles.Select(Function(f) f..Contains(f.RoleId)) Then

        '    '    Highers.Add(u.Name, u.Id)
        'Next
        ''Dim ff = UserManager.GetRoles(cuser.Id)
        ''Dim f1 = UserManager.GetRoles(cuser.Id).Where(Function(f) {"2.Διευθυντής Εργοστασίου", "3.Διευθυντής τμήματος", "4.Προϊστάμενος"}.Contains(f)).ToList
        ''If f1.Count > 0 Then
        ''    Highers.Add(cuser.Name, cuser.Id)
        ''End If
        ''Highers = UserManager.Users.Select(.ge'= (Highers.Add(emptyHigher).ToList.Union(emptyHigher.ToList)).ToList 'db.UFTBL01s.Where(Function(f) f.COMPANY = 5000 And f.SOSOURCE = 1251 And f.ISACTIVE = 1).OrderBy(Function(f) f.NAME).ToList)).ToList
        ''Me.ddlΗigher.DisplayMember = "Key"
        ''Me.ddlΗigher.ValueMember = "Value"
        'Me.ddlΗighers.DataSource = Highers.ToList









        If Not (CurUser = "gmlogic" Or CurUserRole = "Admins") Then
            Exit Sub
        End If

        Dim decryptPass = CryptoUtils.Decrypt(cuser.encryptPass)
        Me.txtBoxOldPass.Text = decryptPass 'user.GetPassword()
        If (CurUser = "gmlogic" Or CurUserRole = "Admins") Then
            Me.txtBoxDecrypt.Visible = True
            Me.txtBoxDecrypt.Text = decryptPass
        End If
        'Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault
        'If Not IsNothing(user) Then

        'End If
        Exit Sub






        'Dim s As ComboBox = sender
        'Dim nm = s.SelectedItem.UserName.ToString
        'My.Settings.Item("GenConnectionString") = My.Settings.ReveraConnectionString
        ''s1Conn
        ''Dim UserManager = GmEntityUserManager.Create(New GmEntityDbContext)
        'Dim user = LUserManager.Users.Where(Function(f) f.UserName = nm).FirstOrDefault '.OrderBy(Function(f) f.UserName).ToList
        'If Not IsNothing(user) Then
        '    Dim usClaim = LUserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm").FirstOrDefault
        '    Dim decryptPass = ""
        '    decryptPass = CryptoUtils.Decrypt(user.encryptPass)

        '    If Not IsNothing(usClaim) Then
        '        Dim encryptPass = usClaim.Value
        '        decryptPass = CryptoUtils.Decrypt(encryptPass)

        '        If Not user.encryptPass = encryptPass Then
        '            user.encryptPass = usClaim.Value
        '        End If
        '        usClaim = LUserManager.GetClaims(user.Id).Where(Function(f) f.Type = "S1").FirstOrDefault
        '        Dim soPassword As String = Utility.soPassword(user.Users, decryptPass)
        '        If usClaim IsNot Nothing AndAlso Not soPassword = usClaim.Value Then
        '            user.soPassword = soPassword 'usClaim.Value
        '        End If
        '        'UserManager.Update(user)
        '    End If
        '    Me.txtBoxDecrypt.Visible = True
        '    Me.txtBoxDecrypt.Text = decryptPass
        'End If
    End Sub

    Private Function getRoles(Optional UsernameTextBox As String = Nothing) As Boolean
        Dim okk As Boolean
        If UsernameTextBox IsNot Nothing Then
            Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Me.UsernameTextBox.Text).FirstOrDefault

            If Not IsNothing(user) Then

                Dim roles = UserManager.GetRoles(user.Id)
                For Each ff As DataGridViewRow In Me.GmChkListBoxRoles.dgv.Rows
                    ff.Cells(0).Value = Nothing
                Next

                Me.GmChkListBoxRoles.TlStxtBox.Text = Nothing
                For Each ff As DataGridViewRow In Me.GmChkListBoxRoles.dgv.Rows
                    Dim gg = roles.Where(Function(f) f = ff.Cells(2).Value).FirstOrDefault
                    If gg IsNot Nothing AndAlso gg = ff.Cells(2).Value Then
                        ff.Cells(0).Value = True
                        Me.GmChkListBoxRoles.TlStxtBox.Text &= ff.Cells(1).Value & ","
                    End If
                Next
                If Not Me.GmChkListBoxRoles.TlStxtBox.Text = "" Then
                    Me.GmChkListBoxRoles.TlStxtBox.Text = Me.GmChkListBoxRoles.TlStxtBox.Text.Substring(0, Me.GmChkListBoxRoles.TlStxtBox.Text.Length - 1)
                    okk = True
                End If
            End If
        End If
        Return okk
        'Throw New NotImplementedException()
    End Function
    Private Function setRoles(UsernameTextBox As String) As IdentityResult
        Dim result1 As New IdentityResult
        If UsernameTextBox IsNot Nothing Then
            UserManagerStore.Context.Database.Connection.ConnectionString = db.Connection.ConnectionString
            Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = UsernameTextBox).FirstOrDefault

            If Not IsNothing(user) Then
                Dim roles = UserManager.GetRoles(user.Id)
                Dim NRoles = Me.GmChkListBoxRoles.dgv.Rows.OfType(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value IsNot Nothing AndAlso f.Cells(0).Value = True).Select(Function(f) f.Cells(2).Value).OfType(Of String).ToArray

                result1 = UserManager.RemoveFromRoles(user.Id, roles.ToArray)
                If result1.Succeeded Then
                    result1 = UserManager.AddToRoles(user.Id, NRoles.ToArray)
                End If
            End If
        End If

        Return result1

    End Function

End Class