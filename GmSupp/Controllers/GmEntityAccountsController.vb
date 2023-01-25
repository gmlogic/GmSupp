Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class GmEntityController1

    ''' <summary>
    ''' ChkUserIfExistInDb
    ''' </summary>
    ''' <param name="userName">userName in AspNetUsers</param>
    ''' <param name="Company">Optional Company in AspNetUsers</param>
    ''' <returns>GmEntityUser</returns>
    Friend Shared Function ChkUser(userName As String, Optional Company As Short? = Nothing) As GmIdentityUser
        ChkUser = Nothing

        Dim Context = New GmIdentityDbContext

        Dim UserManager = GmUserManager.Create(Context)

        ChkUser = UserManager.Users.Where(Function(f) f.UserName = userName And If(Company, 0) = If(Company, 0)).SingleOrDefault

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

    Friend Shared Function CreateUser(Username As String, Password As String, Optional roleName As String = Nothing, Optional Company As Short = Nothing, Optional CompName As String = Nothing, Optional Users As Short = Nothing) As IdentityResult
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
            user.S1User = True 'For first time
            user.Users = Users
            user.Company = Company
            user.CompanyName = If(CompName, "SERTORIUS")
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
            Catch ex As Exception
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


        End If

        Return chkResult
    End Function
    Friend Shared Function ChangePassword(UserManager As GmUserManager, User As GmIdentityUser, OldPassword As String, Password As String, Optional roleName As String = Nothing) As IdentityResult
        Dim PassResult As IdentityResult = Nothing
        'Dim Context = New GmEntityDbContext

        'Dim UserManager = GmEntityUserManager.Create(Context)
        Dim chkPass = UserManager.CheckPassword(User, Password)
        If Not chkPass Then
            PassResult = UserManager.ChangePassword(User.Id, OldPassword, Password)
        End If

        If PassResult.Succeeded Then
            If Not IsNothing(roleName) Then
                Dim newRoleName As String = Nothing
                'Dim UsRole = UserManager.user.Roles.SingleOrDefault
                'If Not IsNothing(UsRole) Then
                '    UsRole.RoleId
                'End If

                Dim role = UserManager.IsInRole(User.Id, roleName)
            End If
        End If
        Return PassResult
    End Function
    'Friend Shared Function CreateUserOld(Username As String, Password As String, Optional roleName As String = Nothing) As IdentityResult
    '    Dim chkResult As IdentityResult = Nothing
    '    Dim user As GmEntityUser = UserManager.Users.Where(Function(f) f.UserName = Username).FirstOrDefault
    '    If IsNothing(user) Then
    '        user = New GmEntityUser
    '        user.UserName = Username
    '        user.Email = "gmlogic@gmail.com"

    '        Dim userPWD As String = Password

    '        chkResult = UserManager.Create(user, userPWD)

    '        'Add default User to Role Users
    '        If chkResult.Succeeded Then
    '            Dim encryptPass = CryptoUtils.Encrypt(userPWD)
    '            Dim usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "Gm" And f.Value = encryptPass).FirstOrDefault
    '            'Dim claims = UserManager.GetClaims(user.Id)
    '            'Dim claim = claims.Where(Function(f) f.Type = "Gm" And f.Value = "test").FirstOrDefault
    '            If IsNothing(usClaim) Then
    '                usClaim = New Security.Claims.Claim("Gm", encryptPass)
    '                UserManager.AddClaim(user.Id, usClaim)
    '                Dim decryptPass = CryptoUtils.Decrypt(encryptPass)
    '                'UserManager.RemoveClaim(user.Id, usClaim)
    '            End If

    '            Dim soPassword As String = Utility.soPassword(UserId, userPWD)
    '            If Not soPassword = "errorValidate" Then
    '                usClaim = UserManager.GetClaims(user.Id).Where(Function(f) f.Type = "S1" And f.Value = soPassword).FirstOrDefault
    '                If IsNothing(usClaim) Then
    '                    usClaim = New Security.Claims.Claim("S1", soPassword)
    '                    UserManager.AddClaim(user.Id, usClaim)
    '                End If
    '            End If

    '            ' first we create Admin rool
    '            If Not IsNothing(roleName) Then
    '                If Not RoleManager.RoleExists(roleName) Then
    '                    Dim role As New IdentityRole
    '                    role.Name = roleName '"Users"
    '                    Dim r As IdentityResult = RoleManager.Create(role)
    '                End If
    '                chkResult = UserManager.AddToRole(user.Id, roleName)
    '            End If
    '        End If
    '    End If
    '    Return chkResult
    'End Function

    Friend Shared Function DeleteUser(Username As String) As IdentityResult
        Dim delResult As IdentityResult = Nothing
        Dim Context = New GmIdentityDbContext

        Dim UserManager = GmUserManager.Create(Context)

        Dim user As IdentityUser = UserManager.Users.Where(Function(f) f.UserName = Username).FirstOrDefault
        If Not IsNothing(user) Then
            If MsgBox("Προσοχή !!! Διαγραφή Χρήστη: " & user.UserName, MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Ok Then
                delResult = UserManager.Delete(user)
            End If
        End If
        Return delResult
    End Function

End Class
