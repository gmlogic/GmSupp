Imports System.Security.Claims
Imports System.Threading.Tasks

Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
'Imports Microsoft.AspNet.Identity.Owin
'Imports Microsoft.Owin

Public Class GmUserManager
    Inherits UserManager(Of GmIdentityUser)

    Public Sub New(store As IUserStore(Of GmIdentityUser))
        MyBase.New(store)
    End Sub

    Public Shared Function Create(ByVal context As GmIdentityDbContext) As GmUserManager
        'Dim appDbContext = context.[Get](Of ApplicationDbContext)()

        'UserManager.UserValidator = New UserValidator(Of IdentityUser)(UserManager) With {.AllowOnlyAlphanumericUserNames = False}

        'UserManager.UserValidator = New UserValidator(Of IdentityUser)(UserManager) With {.AllowOnlyAlphanumericUserNames = False}
        'UserManager.PasswordValidator = New PasswordValidator() With {
        '    .RequireDigit = False,
        '    .RequiredLength = False,
        '    .RequireLowercase = False,
        '    .RequireNonLetterOrDigit = False,
        '    .RequireUppercase = False}




        Dim appUserManager = New GmUserManager(New UserStore(Of GmIdentityUser)(context))
        appUserManager.UserValidator = New UserValidator(Of GmIdentityUser)(appUserManager) With {
                .AllowOnlyAlphanumericUserNames = False,
                .RequireUniqueEmail = False
            }
        appUserManager.PasswordValidator = New PasswordValidator With {
                .RequireNonLetterOrDigit = False,
                .RequireDigit = False,
                .RequireLowercase = False,
                .RequireUppercase = False
            }
        'appUserManager.EmailService = New AspNetIdentity.WebApi.Services.EmailService()
        'Dim dataProtectionProvider = options.DataProtectionProvider

        'If dataProtectionProvider IsNot Nothing Then
        '    appUserManager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity")) With {
        '            .TokenLifespan = TimeSpan.FromHours(6)
        '        }
        'End If

        Return appUserManager
    End Function



    'Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationUserManager), ByVal context As IOwinContext) As ApplicationUserManager
    '    Dim appDbContext = context.Get(Of ApplicationDbContext)()
    '    Dim appUserManager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(appDbContext))
    '    'Rest of code Is removed for clarity
    '    appUserManager.EmailService = New AspNetIdentity.EmailService()
    '    Dim dataProtectionProvider = options.DataProtectionProvider
    '    If Not IsNothing(dataProtectionProvider) Then
    '        appUserManager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity")) With {
    '            .TokenLifespan = TimeSpan.FromHours(6)'Code for email confirmation And reset password life time
    '        }
    '    End If
    '    Return appUserManager
    'End Function

    'Rest of code Is removed for brevity
    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of GmIdentityUser), authenticationType As String) As Task(Of ClaimsIdentity)
        Dim userIdentity = Await manager.CreateIdentityAsync(Users, authenticationType)
        ' Add custom user claims here
        Return userIdentity
    End Function


    ''' <summary>
    ''' ChkUserIfExistInDb
    ''' </summary>
    ''' <param name="userName">userName in AspNetUsers</param>
    ''' <param name="Company">Optional Company in AspNetUsers</param>
    ''' <returns>GmEntityUser</returns>
    Friend Shared Function ChkUser(userName As String, Optional Company As Short? = Nothing) As GmIdentityUser
        ChkUser = Nothing

        Dim Context = New GmIdentityDbContext

        Dim GmUserManager1 = GmUserManager.Create(Context)

        ChkUser = GmUserManager1.Users.Where(Function(f) f.UserName = userName And If(Company, 0) = If(Company, 0)).SingleOrDefault
        'GmUserManager1.Update(ChkUser)

        '    'Dim context As New IdentityDbContext()
        '    'context.Database.Connection.ConnectionString = My.Settings.GenConnectionString

        '    'RoleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(context))
        '    'UserManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(context))

        '    Dim UserManager = GmEntityUserManager.Create(New GmEntityDbContext)

        '    Dim user As GmEntityUser = UserManager.Users.Where(Function(f) f.UserName = userName And If(f.Company, 0) = If(Company, 0)).FirstOrDefault
        '    If Not IsNothing(user) Then
        '        Dim usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm").FirstOrDefault
        '        Dim decryptPass = ""
        '        If Not IsNothing(usClaim) Then
        '            Dim encryptPass = usClaim.Value
        '            decryptPass = CryptoUtils.Decrypt(encryptPass)
        '            Dim soPassword As String = Utility.soPassword(UserId, decryptPass)
        '            If Not soPassword = "errorValidate" Then
        '                usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "S1" And f.Value = soPassword).FirstOrDefault
        '                If Not IsNothing(usClaim) Then
        '                    ChkUser = True
        '                Else
        '                    usClaim = New Security.Claims.Claim("S1", soPassword)
        '                    UserManager.AddClaim(user.Id, usClaim)
        '                    If Not IsNothing(usClaim) Then
        '                        ChkUser = True
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        Return ChkUser
    End Function

    Friend Shared Function CreateUser(Username As String, Password As String, Optional Name As String = Nothing, Optional roleName As String = Nothing, Optional Company As Short = Nothing, Optional CompName As String = Nothing, Optional Users As Short = Nothing) As IdentityResult
        Dim chkResult As IdentityResult = Nothing

        Dim Context = New GmIdentityDbContext

        Dim UserManager1 = GmUserManager.Create(Context)

        Dim user As GmIdentityUser = UserManager1.Users.Where(Function(f) f.UserName = Username).FirstOrDefault
        Dim gg = UserManager1.Users.Where(Function(f) f.UserName = Username)
        'Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Username).FirstOrDefault
        If IsNothing(user) Then
            user = New GmIdentityUser
            user.UserName = Username
            user.Email = "gmlogic@gmail.com"
            user.S1User = False
            If Not Users = 0 Then
                user.S1User = True 'For first time
            End If

            user.Users = Users
            user.Company = Company
            user.CompanyName = If(CompName, "SERTORIUS")
            user.Name = Name
            user.CreateDate = Now()

            Dim userPWD As String = Password
            user.encryptPass = CryptoUtils.Encrypt(userPWD)
            Dim decryptPass = CryptoUtils.Decrypt(user.encryptPass)
            Dim _UserID = UserId
            '_UserID = 0
            Dim soPassword As String = Utility.soPassword(_UserID, userPWD)
            'If Not soPassword = "errorValidate" Then

            'End If
            user.soPassword = soPassword
            Try
                chkResult = UserManager1.Create(user, userPWD)
            Catch ve As System.ComponentModel.DataAnnotations.ValidationException
                chkResult = IdentityResult.Failed({ve.Message})
            Catch ex As Exception
                chkResult = IdentityResult.Failed({ex.Message})
                'Dim g1 = UserManager1.EntityValidationErrors
            End Try


            'Add default User to Role Users
            If Not IsNothing(chkResult) AndAlso chkResult.Succeeded Then
                If Not IsNothing(roleName) Then
                    Dim RoleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(Context))
                    If Not RoleManager.RoleExists(roleName) Then
                        Dim role As New IdentityRole
                        role.Name = roleName '"Users"
                        Dim r As IdentityResult = RoleManager.Create(role)
                    End If
                    chkResult = UserManager1.AddToRole(user.Id, roleName)
                End If
            End If
        Else
            chkResult = IdentityResult.Failed({"User Exists:" & user.Name})

        End If

        Return chkResult
    End Function
    Friend Shared Function ChangePassword(CUserManager As GmUserManager, User As GmIdentityUser, OldPassword As String, Password As String, Optional roleName As String = Nothing) As IdentityResult
        Dim PassResult As IdentityResult = Nothing
        'Dim Context = New GmEntityDbContext

        'Dim UserManager = GmEntityUserManager.Create(Context)
        Dim chkPass = CUserManager.CheckPassword(User, Password)
        If Not chkPass Then
            PassResult = CUserManager.ChangePassword(User.Id, OldPassword, Password)
        End If

        If PassResult.Succeeded Then
            If Not IsNothing(roleName) Then
                Dim newRoleName As String = Nothing
                'Dim UsRole = UserManager.user.Roles.SingleOrDefault
                'If Not IsNothing(UsRole) Then
                '    UsRole.RoleId
                'End If

                Dim role = CUserManager.IsInRole(User.Id, roleName)
            End If
        End If
        Return PassResult
    End Function


    Friend Shared Function DeleteUser(Username As String) As IdentityResult
        Dim delResult As IdentityResult = Nothing
        'Dim Context = New GmIdentityDbContext

        ''Dim UserManager = GmUserManager.Create(Context)
        'Dim conString As New SqlClient.SqlConnectionStringBuilder
        'conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        'conString.DataSource = "192.168.12.201,55555"
        'conString.InitialCatalog = "Revera"
        'UserManagerStore.Context.Database.Connection.ConnectionString = conString.ConnectionString
        Dim user As IdentityUser = UserManager.FindByName(Username) '.Users.Where(Function(f) f.UserName = Username).FirstOrDefault
        If Not IsNothing(user) Then
            If MsgBox("Προσοχή !!! Διαγραφή Χρήστη: " & user.UserName, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Ok Then
                delResult = UserManager.Delete(user)
            End If
        End If
        Return delResult
    End Function

    Friend Shared Function SetEncryptPass(user As IdentityUser, PasswordTextBox As String) As Boolean
        Dim okk As Boolean
        Dim Context = New GmIdentityDbContext

        Dim UserManager1 = GmUserManager.Create(Context)

        Dim cuser As GmIdentityUser = UserManager1.FindById(user.Id)
        If Not IsNothing(cuser) Then
            cuser.encryptPass = CryptoUtils.Encrypt(PasswordTextBox)
            UserManager1.Update(cuser)
            cuser = UserManager1.FindById(user.Id)

            Dim decryptPass = CryptoUtils.Decrypt(cuser.encryptPass)

            If decryptPass = PasswordTextBox Then
                okk = True
            End If
        End If
        Return okk
        'Throw New NotImplementedException()
    End Function

    Friend Shared Function UpdateUser(Nuser As GmIdentityUser) As IdentityResult
        Dim Result1 As IdentityResult = Nothing
        Try
            Dim UserManager1 = GmUserManager.Create(New GmIdentityDbContext)

            Dim cuser As GmIdentityUser = UserManager1.FindById(Nuser.Id)
            If Not IsNothing(cuser) Then
                cuser.Name = Nuser.Name
                Result1 = UserManager1.Update(cuser)
            End If
        Catch ex As Exception
            Result1 = IdentityResult.Failed({ex.Message})
        End Try

        Return Result1
    End Function
End Class
