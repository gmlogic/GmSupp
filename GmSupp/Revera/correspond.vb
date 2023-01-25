Imports GmSupp.Centro
Imports Softone

Public Class correspond

    Dim db As New DataClassesCentrofaroDataContext
    Dim fS1HiddenForm As New Form

    Private Sub correspond_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db = New DataClassesCentrofaroDataContext(My.Settings.CentroConnectionString & ";Connection Timeout=300")
        Me.btnCheck.Enabled = True
        Me.btnCheck.Enabled = True


        Dim txtUser = "gmlogic"
        Dim txtPass = "1mgergm++"
        Dim txtCompany = "1000"
        Dim txtBranch = "1000"
        Dim txtXCOFile = "C:\SOFTONE\GmHglp.XCO"
        Dim DTLogin = DateTime.Now

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        '      Select Case f.PAYDEMANDMD,f.FINPAYTERMS As vfinpayterms
        '	,f.FINPAYTERMSS AS vfinpaytermss
        '	,f.FINDOC AS vfindoc
        '	,f.TRDR AS vtrdr
        '	,ISNULL(f.TRDFLINES, 0) AS vtrdflines
        '	,f.TAMNT AS vtamnt
        'From FINPAYTERMS As f
        '      INNER Join TRDR AS t
        '      Left OUTER JOIN TRDEXTRA AS ex 
        'On t.COMPANY = ex.COMPANY 	And t.SODTYPE = ex.SODTYPE	And t.TRDR = ex.TRDR 
        'ON f.TRDR = t.TRDR 
        'WHERE(f.COMPANY = 3000)
        '	And (f.TRDR = 12161)
        '	And (t.SODTYPE = 12)
        '	And (1 = 1)
        '	--And (f.PAYDEMANDMD = - 2)
        '	And (f.TRNDATE <= '20170628')
        '	And (f.INSMODE IN (	3,1	))
        '	And (f.ISCANCEL = 0)
        '	And (f.APPRV = 1)
        '	And (ISNULL(f.FINDOCDIFF, 0) = 0)
        'ORDER BY vfinpayterms, vfinpaytermss
        '    Dim JoinedResult =
        'From t1 In Table1
        'Group Join t2 In Table2
        '   On t1.key Equals t2.key
        '   Into RightTableResults = Group
        'From t2 In RightTableResults.DefaultIfEmpty
        'Select t1.Prop1,
        '   t2.Prop2
        Try
            Dim q = From f In db.FINPAYTERMs Join t In db.TRDRs On f.TRDR Equals t.TRDR
                    Where (t.SODTYPE = 12)
                    Group Join ex In db.TRDEXTRAs On t.TRDR Equals ex.TRDR Into Group
                    Select f


            q = q.Where(Function(f) f.COMPANY = 3000 And (f.TRDR = 12161) And (1 = 1))

            'q = q.Where(Function(f) (f.PAYDEMANDMD = -2))
            q = q.Where(Function(f) (f.TRNDATE <= CDate("28/06/2017") And {3, 1}.Contains(f.INSMODE) And (f.ISCANCEL = 0) And (f.APPRV = 1) And If(f.FINDOCDIFF, 0) = 0))
            q = q.OrderBy(Function(f) f.FINPAYTERMS And f.FINPAYTERMSS)

            Dim q1 = From f In q
                     Select vfinpayterms = f.FINPAYTERMS, vfinpaytermss = f.FINPAYTERMSS, vfindoc = f.FINDOC, vtrdr = f.TRDR, vtrdflines = If(f.TRDFLINES, 0), vtamnt = f.TAMNT

            Me.MasterBindingSource.Filter = Nothing
            Me.MasterBindingSource.DataSource = q
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource
            Me.MasterDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim cnt = 0
        For Each f As FINPAYTERM In Me.MasterBindingSource
            Dim vfinpayterms = f.FINPAYTERMS
            Dim vfinpaytermss = f.FINPAYTERMSS
            Dim vfindoc = f.FINDOC
            Dim vtrdr = f.TRDR
            Dim vtrdflines = If(f.TRDFLINES, 0)
            Dim vtamnt = f.TAMNT
            cnt = cnt + 1
            If f.FINPAYTERMS = vfinpaytermss And f.PAYDEMANDMD = 1 Then
                f.OPNTAMNT = Math.Round((f.OPNTAMNT + vtamnt), 7)
            End If
            If f.FINDOC = vfindoc And f.TRDR = vtrdr And If(f.TRDFLINES, 0) = vtrdflines And f.PAYDEMANDMD = -1 Then
                f.OPNTAMNT = Math.Round((f.OPNTAMNT + vtamnt), 7)
            End If
            If f.FINPAYTERMS = vfinpayterms Then
                'delete
            End If

        Next
        '        fetch next from cr1
        '  into @vfinpayterms,@vfinpaytermss,@vfindoc,@vtrdr,@vtrdflines,@vtamnt
        'While (@@fetch_status <> -1)
        '	begin
        '            If @cnt=0 begin
        '		begin tran
        '		set @cnt=0
        '	  End
        '	  set @cnt=@cnt+1
        '	Update finpayterms
        '	set opntamnt=Round((opntamnt + @vtamnt),7)
        '	where finpayterms =@vfinpaytermss And paydemandmd=1
        '	Update finpayterms
        '	set opntamnt=Round((opntamnt + @vtamnt),7)
        '	where findoc =@vfindoc And trdr=@vtrdr And ISNULL(trdflines, 0)=@vtrdflines And paydemandmd=-1
        '	delete From finpayterms Where finpayterms =@vfinpayterms
        '	  If @cnt>=100 begin
        '		commit tran
        '		set @cnt=0
        '	  End
        '                    fetch next from cr1
        '		into @vfinpayterms,@vfinpaytermss,@vfindoc,@vtrdr,@vtrdflines,@vtamnt
        '	End
        '                    If @cnt>0 
        '	begin
        '                        commit tran
        '	  set @cnt=0
        '	End
        '                        Close cr1
        'deallocate cr1
        'End
        '                        End
        'For 
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim str As String = ""
            'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + dgFINDOC.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
            str = "SALDOC[AUTOLOCATE=110739]"
            s1Conn.ExecS1Command(str, fS1HiddenForm)
            'FilldgFINDOC_gm(iActiveObjType)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class