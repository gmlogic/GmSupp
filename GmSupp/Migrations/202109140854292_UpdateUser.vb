Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class UpdateUser
        Inherits DbMigration

        Public Overrides Sub Up()
            'AddColumn("dbo.AspNetUsers", "Company", Function(c) c.Short())
            'AddColumn("dbo.AspNetUsers", "CompanyName", Function(c) c.String(nullable := False, maxLength := 128))
            'AddColumn("dbo.AspNetUsers", "Users", Function(c) c.Short())
            'AddColumn("dbo.AspNetUsers", "S1User", Function(c) c.Boolean())
            'AddColumn("dbo.AspNetUsers", "Applicant", Function(c) c.Short())

            AddColumn("dbo.AspNetUsers", "Name", Function(c) c.String(nullable:=False, maxLength:=128))
            AddColumn("dbo.AspNetUsers", "Priority", Function(c) c.Short())

        End Sub

        Public Overrides Sub Down()
            DropColumn("dbo.AspNetUsers", "Applicant")
            DropColumn("dbo.AspNetUsers", "S1User")
            DropColumn("dbo.AspNetUsers", "Users")
            DropColumn("dbo.AspNetUsers", "CompanyName")
            DropColumn("dbo.AspNetUsers", "Company")
        End Sub
    End Class
End Namespace
