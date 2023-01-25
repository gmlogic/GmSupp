Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class GmIdentityUser
    Inherits IdentityUser

    Property Company As Short?

    <Required>
    <MaxLength(128)>
    Property CompanyName As String

    Property Users As Short?

    <Required>
    <MaxLength(256)>
    Property soPassword As String

    <Required>
    <MaxLength(128)>
    Property encryptPass As String
    'passwordFormat="Encrypted"

    <Required>
    Property CreateDate As DateTime

    Property S1User As Boolean?

    ''' <summary>
    ''' A.UFTBL01 Applicant
    ''' </summary>
    ''' <returns></returns>
    Property Applicant As System.Nullable(Of Short)

    <Required>
    <MaxLength(128)>
    Property Name As String

    Property Priority As System.Nullable(Of Short)

End Class
