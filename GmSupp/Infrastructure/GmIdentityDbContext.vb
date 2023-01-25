Imports Microsoft.AspNet.Identity.EntityFramework

Public Class GmIdentityDbContext
    Inherits IdentityDbContext(Of GmIdentityUser)

    Public Sub New()
        MyBase.New("GmSupp.My.MySettings.ReveraConnectionString", throwIfV1Schema:=False)
        Configuration.ProxyCreationEnabled = False
        Configuration.LazyLoadingEnabled = False
    End Sub

    Public Shared Function Create() As GmIdentityDbContext
        Return New GmIdentityDbContext()
    End Function

End Class
