Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddNamePriority
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetUsers", "Name", Function(c) c.String(nullable := False, maxLength := 128))
            AddColumn("dbo.AspNetUsers", "Priority", Function(c) c.Short())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AspNetUsers", "Priority")
            DropColumn("dbo.AspNetUsers", "Name")
        End Sub
    End Class
End Namespace
