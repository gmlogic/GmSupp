Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.Transactions
Imports GmSupp
Imports GmSupp.Hglp

Public Class ManageCarrierPaypents
#Region "01-Declare Variables"
    Dim df As GmData
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    ' Declare a variable to indicate the commit scope.  
    ' Set this value to false to use cell-level commit scope.  
    Private rowScopeCommit As Boolean = True
    Dim fS1HiddenForm As New Form
    Dim conn As String
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
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        GenMenu.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID
        Dim db = New DataClassesHglpDataContext(conn)
        'Dim q = (From so In db.SOCARRIERs Select so.NAME, so.SOCARRIER).ToList
        Dim q = db.SOCARRIERs.ToList
        q = q.Where(Function(f) f.ISACTIVE > 0).ToList
        q = q.OrderBy(Function(f) f.NAME).ToList
        Dim emptySOCARRIER As SOCARRIER()
        emptySOCARRIER = {New SOCARRIER With {.NAME = "<Επιλέγξτε>", .SOCARRIER = 0}}

        Dim q1 = emptySOCARRIER.ToList.Union(q.ToList)

        Me.SONAMEComboBox.DataSource = q1.ToList
        Me.SONAMEComboBox.DisplayMember = "NAME"
        Me.SONAMEComboBox.ValueMember = "SOCARRIER"
        Me.SONAMEComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        Me.SONAMEComboBox.AutoCompleteSource = AutoCompleteSource.ListItems

        Dim bci = db.ccCTrdBCities.ToList
        bci = bci.Where(Function(f) {26, 149, 574, 578, 586}.Contains(f.ccCTrdBCity) And f.ISACTIVE > 0).ToList

        Dim emptyccCTrdBCity As ccCTrdBCity()
        emptyccCTrdBCity = {New ccCTrdBCity With {.CITY = "<Επιλέγξτε>", .ccCTrdBCity = 0}}

        Dim q2 = emptyccCTrdBCity.ToList.Union(bci.ToList)

        Me.SplitContainer2.SplitterDistance = 800
    End Sub



    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = (Not DataSafe())
    End Sub
#End Region
#Region "04-Bas_Commands"
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
            db.Log = Nothing ' Console.Out
            'Μεταφορείς:
            'Δρομολόγια:
            'CheckZIP:
            Dim runningTotal As Double = 0
            Dim getRunningTotal As Func(Of Double, Double) = Function(n)
                                                                 runningTotal += n
                                                                 Return runningTotal
                                                             End Function

            Dim q = From fi In db.FINDOCs Join doc In db.MTRDOCs On fi.FINDOC Equals doc.FINDOC
                    Select fi.FINDOC, fi.COMPANY, fi.SOSOURCE, fi.FPRMS, fi.FINCODE, fi.TRNDATE, doc.ccCShippingNo, doc.ccCTOTSHIPVALUE, doc.SOCARRIER, fi.ISCANCEL, fi.APPRV, doc.ccCLockShipValue,
                        fi.TRDR, fi.TRDBRANCH, fi.SOCURRENCY, fi.TRDRRATE, fi.COMMENTS, fi.PAYMENT, fi.FINDOCS, fi.UPDDATE, fi.UPDUSER, fi.CNTR,
                        CredVal = If({8000, 8001}.Contains(fi.FPRMS), If(fi.MTRLINEs.Sum(Function(f) f.LINEVAL), 0), 0),
                        SumShVAL = If(doc.ccCLockShipValue = 1, doc.ccCTOTSHIPVALUE, If(fi.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE), 0)),
                        SumShTOT = If({7040, 7041, 7046}.Contains(fi.FPRMS), If(doc.ccCLockShipValue = 1, doc.ccCTOTSHIPVALUE, If(fi.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE), 0)) * 1.24, 0),
                        bal = If({8000, 8001}.Contains(fi.FPRMS), If(fi.MTRLINEs.Sum(Function(f) f.LINEVAL), 0), 0) - If({7040, 7041, 7046}.Contains(fi.FPRMS), If(doc.ccCLockShipValue = 1, doc.ccCTOTSHIPVALUE, If(fi.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE), 0)) * 1.24, 0)
            'runtot = getRunningTotal(If({8000, 8001}.Contains(fi.FPRMS), If(fi.MTRLINEs.Sum(Function(f) f.LINEVAL), 0), 0) - If({7040, 7041, 7046}.Contains(fi.FPRMS), If(fi.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE), 0) * 1.24, 0))


            Dim qwh = q.Where(Function(f) f.COMPANY = 1000 And {1351, 1253}.Contains(f.SOSOURCE) And f.ISCANCEL = 0 And f.APPRV = 1)


            If Me.TlSComboBoxDate.Text = "Ημ/νία Παραστατικού:" Then
                qwh = qwh.Where(Function(f) f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value)
            End If

            If Not SONAMEComboBox.SelectedValue = 0 Then
                Dim so As Short = SONAMEComboBox.SelectedValue
                '                From d In db.Receive
                '                Where ((d.SendType == "None" && d.Signed) || d.SendType != "None") && userid == 1)
                'Select d
                qwh = qwh.Where(Function(f) ({7040, 7041, 7046}.Contains(f.FPRMS) And f.SOCARRIER = so) Or ({8000, 8001}.Contains(f.FPRMS) And f.SOCARRIER Is Nothing))
            Else
                qwh = qwh.Where(Function(f) {7040, 7041, 7046, 8000, 8001}.Contains(f.FPRMS))
                'qwh = From qq In qwh Where (Function(f) If(f.SOCARRIER, f.SOCARRIER = so))
            End If


            Dim fin = qwh.FirstOrDefault.FINDOC
            ' Dim SumShVAL = qwh.Sum(Function(f) f.mmts..QTY1 * f.ccCSHIPVALUE)


            'For Each fin In q
            '    Dim mtrdoc = fin.MTRDOC
            '    If mtrdoc.ccCLockShipValue = True Then
            '        Continue For
            '    End If
            '    Dim ccCTOTSHIPVALUE = fin.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE)
            '    If Not ccCTOTSHIPVALUE = 0 Then
            '        mtrdoc.ccCTOTSHIPVALUE = ccCTOTSHIPVALUE
            '    End If


            'Next
            'If Me.TlSComboBoxChoice.Text = "Παραλήπτες:" Then
            '    Dim q = db.SOCARRIERs.AsQueryable
            '    Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            '    If Not Me.txtSONAME.Text = "" Then
            '        qwh = qwh.Where(Function(f) f.NAME Like Me.txtSONAME.Text)
            '    End If
            '    qwh = qwh.OrderBy(Function(f) f.NAME)
            '    qds = qwh
            'End If

            'If Me.TlSComboBoxChoice.Text = "Μεταφορείς:" Then
            '    Dim q = db.SOCARRIERs.AsQueryable
            '    Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            '    qwh = qwh.OrderBy(Function(f) f.NAME)
            '    qds = qwh
            'End If
            Dim tots = (From tt In qwh Select New RTotals With {.TRNDATE = tt.TRNDATE, .Bal = tt.bal}).ToList



            Dim results2 =
                tots.Zip(tots.Scan(0.00, Function(tt, t) tt + t.Bal), Function(r1, rt) New With {.num = r1.Bal, .running_total = rt})


            Dim m As New RTotals
            m.TRNDATE = Now
            m.Bal = 0.00
            m.RTots = 0.00


            Dim fpays As New List(Of FPTerms)
            Dim fpid = 0
            Dim q5 = From fi In db.FINDOCs Join doc In db.MTRDOCs On fi.FINDOC Equals doc.FINDOC Select fi
            For Each fp1 In qwh
                Dim fp As New FPTerms

                fp.COMPANY = fp1.COMPANY
                fpid += 1
                fp.FINPAYTERMS = fpid
                fp.FINDOC = fp1.FINDOC
                fp.LINENUM = 1
                'fp.FINPAY = fp1.FINPAY
                fp.TRDR = fp1.TRDR
                fp.TRDBRANCH = fp1.TRDBRANCH
                fp.SOCURRENCY = fp1.SOCURRENCY
                If {8000, 8001}.Contains(fp1.FPRMS) Then
                    fp.PAYDEMANDMD = -1 'Χρέωση Προμηθ - Επιταγές
                Else
                    fp.PAYDEMANDMD = 1 'Πίστωση Προμηθ - Τιμολ
                End If
                'fp.SOPAYTYPE = fp1.SOPAYTYPE
                fp.ISCANCEL = fp1.ISCANCEL
                fp.APPRV = fp1.APPRV
                'fp.FINALDATE = fp1.FINALDATE
                fp.TRNDATE = fp1.TRNDATE
                'fp.ENDDATE = fp1.ENDDATE
                fp.TRDRRATE = fp1.TRDRRATE
                If {8000, 8001}.Contains(fp1.FPRMS) Then
                    fp.AMNT = fp1.CredVal
                Else
                    fp.AMNT = fp1.SumShTOT
                End If
                fp.TAMNT = fp.AMNT
                fp.LAMNT = fp.AMNT
                fp.OPNTAMNT = fp.AMNT
                'fp.ISCLOSE = fp1.ISCLOSE
                fp.COMMENTS = fp1.COMMENTS
                fp.PAYMENT = fp1.PAYMENT
                'fp.PAYGRPVAL = fp1.PAYGRPVAL
                'fp.INSTALMENT = fp1.INSTALMENT
                'fp.COMMITION = fp1.COMMITION
                'fp.TRDFLINES = fp1.TRDFLINES
                'fp.FINPAYTERMSS = fp1.FINPAYTERMSS
                'fp.FINDOCS = fp1.FINDOCS
                fp.INSMODE = 1
                'fp.OPENMODE = fp1.OPENMODE
                fp.UPDDATE = fp1.UPDDATE
                fp.UPDUSER = fp1.UPDUSER
                'fp.FINDOCDIFF = fp1.FINDOCDIFF
                'fp.FXDIFF = fp1.FXDIFF
                fp.CNTR = fp1.CNTR 'Ειδική σύμβαση
                fp.SOCARRIER = fp1.SOCARRIER
                fpays.Add(fp)
            Next

            calcFPTerms(db, fpays)
            'var SE = From c In Shop.Sections
            '         Join c1 In obj.SectionObjects On c.SectionId Equals c1.SectionId
            '         Select c;

            'var SE = Shop.Sections.Where(s >= obj.SectionObjects
            '                           .Select(so >= so.SectionId)
            '.Contains(s.SectionId))
            '           .ToList();
            'Dim qwht = qwh

            'var SE = From c In Shop.Sections
            '         Where obj.SectionObjects.Select(z >= z.SectionId).Contains(c.SectionId)
            '         Select c; 
            'Dim nfps = From qw In qwh Where fpays.Select(Function(f) f.FINDOC).Contains(qw.FINDOC) '.ToList Join fp In fpays On qw.FINDOC Equals fp.FINDOC

            Dim qwht = qwh.ToList
            Dim nfps = From qw In qwht Join fp In fpays On qw.FINDOC Equals fp.FINDOC
                       Select qw.FINCODE, qw.TRNDATE, ccCShippingNo = If(qw.ccCShippingNo, "-"), ccCTOTSHIPVALUE = If(qw.ccCTOTSHIPVALUE, 0), qw.ccCLockShipValue, qw.SOCARRIER, qw.CredVal,
                           SumShTOT = If(qw.SumShTOT, 0), qw.bal, qw.SumShVAL, qw.ISCANCEL, qw.APPRV,
                           fp.TAMNT, fp.FINPAYTERMS, fp.FINDOC, fp.FINPAYTERMSS, fp.FINDOCS, qw_FINDOC = qw.FINDOC, qw_FINDOCS = qw.FINDOCS

            Me.MasterBindingSource.DataSource = nfps 'fpays 'qwh '.MasterBindingSource.DataSource = New SortableBindingList(Of FINDOC_MTRLINE)(nq) 'dt
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource

            'MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message & ex.StackTrace)

        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub calcFPTerms(db As DataClassesHglpDataContext, fpays As List(Of FPTerms))
        Try
            'Dim vfinpayterms As Integer
            'Dim vtrdr As Integer
            'Dim vopntamnt As Double
            'Dim vfindoc As Integer
            'Dim vsocurrency As Short
            Dim vopitemtvalinslns As Double
            Dim vfinpaytermsOne As Integer
            Dim vopntamntdebitins As Double
            Dim vopntamntdebitinslns As Double
            Dim vlockid As Integer
            Dim vfindocs As Integer

            Dim trndate As Date = Me.DateTimePicker2.Value
            'Dim trdr As Integer = 134

            LoadData()
            db.Log = Console.Out
            'Ακύρωση FIFO
            Dim Cfifo = From f In fpays
                        Where (f.COMPANY = 1000) And (f.PAYDEMANDMD = -2) And
                        (f.TRNDATE <= trndate) And {3, 1}.Contains(f.INSMODE) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And If(f.FINDOCDIFF, 0) = 0
                        Order By f.FINPAYTERMS, f.FINPAYTERMSS
                        Select vfinpayterms = f.FINPAYTERMS, vfinpaytermss = f.FINPAYTERMSS,
                            vfindoc = f.FINDOC, vtrdr = f.TRDR, vtrdflines = If(f.TRDFLINES, 0), vtamnt = f.TAMNT

            For Each q1 In Cfifo
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpaytermss And f.PAYDEMANDMD = 1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINDOC = q1.vfindoc And f.TRDR = q1.vtrdr And If(f.TRDFLINES, 0) = q1.vtrdflines And f.PAYDEMANDMD = -1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault

                db.FINPAYTERMs.DeleteOnSubmit(finp)
            Next

            Dim Cfifo1 = From f In fpays
                         Where (f.COMPANY = 1000) And {-2, -1, 1}.Contains(f.PAYDEMANDMD) And
                        (f.TRNDATE <= trndate)
                         Select vfinpayterms = f.FINPAYTERMS, vfindoc = f.FINDOC, f.OPNTAMNT

            For Each q1 In Cfifo1
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                If Not IsNothing(finp) Then
                    finp.ISCLOSE = If(q1.OPNTAMNT <> 0, 0, 1)
                End If
            Next

            'INSMODE
            '+Manual f.insmode in ( 3, 1 )
            'f.insmode in ( 3, 0 )
            'Dim fifo = From f In db.FINPAYTERMs, t In db.TRDRs
            '           Where (f.COMPANY = 1000) And f.TRDR = t.TRDR And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -2) And
            '            (f.TRNDATE <= trndate) And {3, 0}.Contains(f.INSMODE) And
            '            (f.ISCANCEL = 0) And (f.APPRV = 1) And If(f.FINDOCDIFF, 0) = 0
            '           Order By f.FINPAYTERMS, f.FINPAYTERMSS
            '           Select vfinpayterms = f.FINPAYTERMS, vfinpaytermss = f.FINPAYTERMSS, vfindoc = f.FINDOC, vtrdr = f.TRDR, vtrdflines = If(f.TRDFLINES, 0), vtamnt = f.TAMNT

            '            Select  f.finpayterms       As vfinpayterms
            ',f.finpaytermss      as vfinpaytermss
            ',f.findoc            as vfindoc
            ',f.trdr              as vtrdr
            ',ISNULL(f.trdflines,0) as vtrdflines
            ',f.tamnt             as vtamnt
            'From finpayterms f, trdr t left outer Join trdextra ex on t.company=ex.company And t.sodtype=ex.sodtype And t.trdr=ex.trdr
            'Where f.company = 1000
            'And f.trdr=t.trdr
            'And t.trdr=1624 And t.sodtype=12
            'And 1=1
            'And f.paydemandmd=-2
            'And f.trndate    <='20171129'
            'And f.insmode in ( 3, 1 )
            'And f.iscancel=0
            'And f.apprv=1
            'And ISNULL(f.findocdiff,0)=0
            'order by 1, 2

            Dim fifo = From f In fpays
                       Where (f.COMPANY = 1000) And (f.PAYDEMANDMD = -2) And
                        (f.TRNDATE <= trndate) And {3, 1}.Contains(f.INSMODE) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And If(f.FINDOCDIFF, 0) = 0
                       Order By f.FINPAYTERMS, f.FINPAYTERMSS
                       Select vfinpayterms = f.FINPAYTERMS, vfinpaytermss = f.FINPAYTERMSS, vfindoc = f.FINDOC, vtrdr = f.TRDR, vtrdflines = If(f.TRDFLINES, 0), vtamnt = f.TAMNT

            For Each q1 In fifo
                '                Update finpayterms
                'set opntamnt=Round((opntamnt + @vtamnt),7)
                'where finpayterms =@vfinpaytermss
                'And paydemandmd=1
                'Update finpayterms
                'set opntamnt=Round((opntamnt + @vtamnt),7)
                'where FINDOC =@vfindoc
                'And trdr=@vtrdr
                'And ISNULL(trdflines,0)=@vtrdflines
                'And paydemandmd=-1
                'delete From finpayterms
                'Where finpayterms =@vfinpayterms
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpaytermss And f.PAYDEMANDMD = 1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINDOC = q1.vfindoc And f.TRDR = q1.vtrdr And If(f.TRDFLINES, 0) = q1.vtrdflines And f.PAYDEMANDMD = -1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault

                db.FINPAYTERMs.DeleteOnSubmit(finp)
            Next

            ''2'''''''''''''''''''''''''''''''
            'Dim fifo1 = From f In fpays
            '            Where (f.COMPANY = 1000) And (f.PAYDEMANDMD = -1) And
            '            (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
            '            (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (Not f.CNTR Is Nothing) Or
            '            (f.PAYDEMANDMD = -1) And
            '            (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
            '            (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (0 = 0)
            '            Order By f.TRDR, f.TRNDATE, f.FINDOC, f.LINENUM
            '            Select vfinpayterms = f.FINPAYTERMS, vtrdr = f.TRDR, vopntamnt = Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero), vfindoc = f.FINDOC, vsocurrency = f.SOCURRENCY

            '            Select f.finpayterms       As vfinpayterms
            ',f.trdr              as vtrdr
            ',Round(f.opntamnt,7) as vopntamnt
            ',f.findoc            as vfindoc
            ',f.socurrency        as vsocurrency
            'From findoc fin, finpayterms f, trdr t left outer Join trdextra ex on t.company=ex.company And t.sodtype=ex.sodtype And t.trdr=ex.trdr
            'Where f.company = 1000
            'And f.findoc=fin.findoc
            'And f.trdr=t.trdr
            'And t.trdr=1624 And t.sodtype=12
            'And f.paydemandmd=-1
            'And f.trndate           <='20171129'
            'And Round(f.tamnt,7)    !=0
            'And Round(f.opntamnt,7) !=0
            'And f.iscancel=0
            'And f.apprv=1
            'And 0=0
            'And ((fin.cntr Is Not null) Or (0=0))
            'order by f.trdr, f.trndate, f.findoc, f.linenum

            Dim fifo1 = From f In fpays
                        Where (f.PAYDEMANDMD = -1) And (f.TRNDATE <= trndate)
                        Order By f.TRDR, f.TRNDATE, f.FINDOC, f.LINENUM
                        Select vfinpayterms = f.FINPAYTERMS, vtrdr = f.TRDR, vtamnt = Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero),
                            vopntamnt = Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero), vfindoc = f.FINDOC, vsocurrency = f.SOCURRENCY

            fifo1 = fifo1.Where(Function(f) f.vtamnt <> 0 And f.vopntamnt <> 0)

            Dim fid = fpays.Count
            For Each q1 In fifo1
                vopitemtvalinslns = q1.vopntamnt
                While vopitemtvalinslns <> 0
                    vfinpaytermsOne = -1

                    'Dim q2 = (From fin In db.FINDOCs, f In db.FINPAYTERMs
                    '          Where f.FINDOC = fin.FINDOC And f.PAYDEMANDMD = 1 And f.TRDR = q1.vtrdr And
                    '                      f.TRNDATE <= trndate And Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0 And f.ISCANCEL = 0 And f.APPRV = 1 And
                    '                      f.COMPANY = 1000 And f.SOCURRENCY = q1.vsocurrency And ((Not fin.CNTR Is Nothing) Or (0 = 0))
                    '          Order By f.TRNDATE).FirstOrDefault
                    '                    Select  Top 1 @vfinpaytermsOne=f.finpayterms
                    'From findoc fin, finpayterms f with (index=xi_finpayterms_paydemandmd, nolock)
                    'Where f.findoc = fin.findoc
                    'And f.paydemandmd=1
                    'And f.trdr=@vtrdr
                    'And f.trndate<='20171129'
                    'And Round(f.opntamnt,7)!=0
                    'And f.iscancel=0
                    'And f.apprv=1
                    'And f.company=1000
                    'And f.socurrency=@vsocurrency
                    'And ((fin.cntr Is Not null) Or (0=0))
                    'order by f.trndate --NOTIMELIMT
                    Dim q2 = (From f In fpays
                              Where f.PAYDEMANDMD = 1 And
                                          f.TRNDATE <= trndate And Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0
                              Order By f.TRNDATE).FirstOrDefault

                    If Not IsNothing(q2) Then
                        vfinpaytermsOne = q2.FINPAYTERMS '- - DECLARE NOTIMELIMT
                    End If

                    If vfinpaytermsOne <> -1 Then
                        vopntamntdebitins = 0
                        Dim finp = fpays.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        If Not IsNothing(finp) Then
                            vfindocs = finp.FINDOC
                            vopntamntdebitins = Math.Round(finp.OPNTAMNT, 7, MidpointRounding.AwayFromZero)
                        End If
                    End If

                    If vopntamntdebitins > vopitemtvalinslns Then
                        vopntamntdebitinslns = vopitemtvalinslns
                        vopitemtvalinslns = 0
                    Else
                        vopntamntdebitinslns = vopntamntdebitins
                        vopitemtvalinslns = vopitemtvalinslns - vopntamntdebitins
                    End If

                    If vfinpaytermsOne <> -1 Then

                        Dim finp = fpays.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7)

                        finp = fpays.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                        If Not IsNothing(finp) Then
                            finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7, MidpointRounding.AwayFromZero)
                        End If


                        Dim fin = fpays.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        If Not IsNothing(fin) Then
                            vlockid = fin.LOCKID
                            vlockid = vlockid + 1
                        End If


                        finp = fpays.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms And Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero) <> 0).FirstOrDefault

                        If Not IsNothing(finp) Then
                            Dim finpn = New FPTerms
                            fid += 1
                            finpn.FINPAYTERMS = fid
                            finpn.COMPANY = finp.COMPANY
                            finpn.FINDOC = finp.FINDOC
                            finpn.LINENUM = finp.LINENUM
                            finpn.TRDR = finp.TRDR
                            finpn.TRDBRANCH = finp.TRDBRANCH
                            finpn.SOCURRENCY = finp.SOCURRENCY
                            finpn.PAYDEMANDMD = -2
                            finpn.SOPAYTYPE = finp.SOPAYTYPE
                            finpn.ISCANCEL = finp.ISCANCEL
                            finpn.APPRV = finp.APPRV
                            finpn.FINALDATE = finp.FINALDATE
                            finpn.TRNDATE = finp.TRNDATE
                            finpn.ENDDATE = finp.ENDDATE
                            finpn.AMNT = 0
                            finpn.TAMNT = Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero)
                            finpn.LAMNT = 0
                            finpn.OPNTAMNT = 0
                            finpn.ISCLOSE = finp.ISCLOSE
                            finpn.COMMENTS = finp.COMMENTS
                            finpn.PAYMENT = finp.PAYMENT
                            finpn.PAYGRPVAL = finp.PAYGRPVAL
                            finpn.INSTALMENT = finp.INSTALMENT
                            finpn.COMMITION = finp.COMMITION
                            finpn.TRDFLINES = finp.TRDFLINES
                            finpn.FINPAYTERMSS = vfinpaytermsOne
                            finpn.FINDOCS = vfindocs
                            finpn.INSMODE = 3
                            finpn.FINPAY = vlockid
                            finpn.TRDRRATE = finp.TRDRRATE
                            finpn.OPENMODE = finp.OPENMODE
                            finpn.UPDDATE = finp.UPDDATE
                            finpn.UPDUSER = finp.UPDUSER
                            finpn.FINDOCDIFF = finp.FINDOCDIFF
                            finpn.FXDIFF = finp.FXDIFF

                            fpays.Add(finpn)
                        End If


                        Dim find = fpays.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        find.LOCKID = vlockid 'update
                    Else
                        vopitemtvalinslns = 0
                    End If
                    If vopntamntdebitinslns = 0 Then
                        vopitemtvalinslns = 0
                    End If

                End While
            Next

            ''3''''''''''''''''''''''''''''''''''''''''''''''''''''''' ISNULL(f.trdbranch,0) !=0
            '            Select f.finpayterms       As vfinpayterms
            ',f.trdr              as vtrdr
            ',f.trdbranch         as vtrdbranch
            ',Round(f.opntamnt,7) as vopntamnt
            ',f.findoc            as vfindoc
            ',f.socurrency        as vsocurrency
            'From findoc fin, finpayterms f, trdr t left outer Join trdextra ex on t.company=ex.company And t.sodtype=ex.sodtype And t.trdr=ex.trdr
            'Where f.company = 1000
            'And f.findoc=fin.findoc
            'And f.trdr=t.trdr ?????????????????
            'And t.trdr=1624 And t.sodtype=12
            'And f.paydemandmd=-1
            'And f.trndate           <='20171129'
            'And Round(f.tamnt,7)    !=0
            'And Round(f.opntamnt,7) !=0
            'And f.iscancel=0
            'And f.apprv=1
            'And 0=1
            'And ISNULL(f.trdbranch,0) !=0
            'And ((fin.cntr Is Not null) Or (0=0))
            'order by f.trdr, f.trdbranch, f.trndate, f.findoc, f.linenum
            Dim fifo3 = From f In fpays
                        Where (f.COMPANY = 1000) And (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (Not f.CNTR Is Nothing) Or
                        (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (0 = 0)
                        Order By f.TRDR, f.TRNDATE, f.FINDOC, f.LINENUM
                        Select vfinpayterms = f.FINPAYTERMS, vtrdr = f.TRDR, vtrdbranch = f.TRDBRANCH, vopntamnt = Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero),
                            vfindoc = f.FINDOC, vsocurrency = f.SOCURRENCY


            For Each q1 In fifo3


                vopitemtvalinslns = q1.vopntamnt
                While vopitemtvalinslns <> 0
                    vfinpaytermsOne = -1
                    '                    Select Top 1 @vfinpaytermsOne=f.finpayterms
                    'From findoc fin, finpayterms f with (index=xi_finpayterms_paydemandmd, nolock)
                    'Where fin.findoc = f.findoc
                    'And f.paydemandmd=1
                    'And f.trdr=@vtrdr
                    'And f.trdbranch=@vtrdbranch
                    'And f.trndate<='20171129'
                    'And Round(f.opntamnt,7)!=0
                    'And f.iscancel=0
                    'And f.apprv=1
                    'And f.company=1000
                    'And f.socurrency=@vsocurrency
                    'And ((fin.cntr Is Not null) Or (0=0))
                    'order by f.trndate --NOTIMELIMIT
                    Dim q2 = (From fin In db.FINDOCs, f In db.FINPAYTERMs
                              Where f.FINDOC = fin.FINDOC And f.PAYDEMANDMD = 1 And f.TRDR = q1.vtrdr And f.TRDBRANCH = q1.vtrdbranch And
                                          f.TRNDATE <= trndate And Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0 And f.ISCANCEL = 0 And f.APPRV = 1 And
                                          f.COMPANY = 1000 And f.SOCURRENCY = q1.vsocurrency And ((Not fin.CNTR Is Nothing) Or (0 = 0))
                              Order By f.TRNDATE).FirstOrDefault '- - DECLARE NOTIMELIMT


                    If Not IsNothing(q2) Then
                        vfinpaytermsOne = q2.f.FINPAYTERMS '- - DECLARE NOTIMELIMT
                    End If


                    If vfinpaytermsOne <> -1 Then
                        vopntamntdebitins = 0
                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        If Not IsNothing(finp) Then
                            vfindocs = finp.FINDOC
                            vopntamntdebitins = Math.Round(finp.OPNTAMNT, 7, MidpointRounding.AwayFromZero)
                        End If
                    End If

                    If vopntamntdebitins > vopitemtvalinslns Then
                        vopntamntdebitinslns = vopitemtvalinslns
                        vopitemtvalinslns = 0
                    Else
                        vopntamntdebitinslns = vopntamntdebitins
                        vopitemtvalinslns = vopitemtvalinslns - vopntamntdebitins
                    End If

                    If vfinpaytermsOne <> -1 Then

                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7)

                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7, MidpointRounding.AwayFromZero)

                        vlockid = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault.LOCKID
                        vlockid = vlockid + 1

                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms And Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero) <> 0).FirstOrDefault

                        Dim finpn = New FINPAYTERM

                        finpn.COMPANY = finp.COMPANY
                        finpn.FINDOC = finp.FINDOC
                        finpn.LINENUM = finp.LINENUM
                        finpn.TRDR = finp.TRDR
                        finpn.TRDBRANCH = finp.TRDBRANCH
                        finpn.SOCURRENCY = finp.SOCURRENCY
                        finpn.PAYDEMANDMD = -2
                        finpn.SOPAYTYPE = finp.SOPAYTYPE
                        finpn.ISCANCEL = finp.ISCANCEL
                        finpn.APPRV = finp.APPRV
                        finpn.FINALDATE = finp.FINALDATE
                        finpn.TRNDATE = finp.TRNDATE
                        finpn.ENDDATE = finp.ENDDATE
                        finpn.AMNT = 0
                        finpn.TAMNT = Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero)
                        finpn.LAMNT = 0
                        finpn.OPNTAMNT = 0
                        finpn.ISCLOSE = finp.ISCLOSE
                        finpn.COMMENTS = finp.COMMENTS
                        finpn.PAYMENT = finp.PAYMENT
                        finpn.PAYGRPVAL = finp.PAYGRPVAL
                        finpn.INSTALMENT = finp.INSTALMENT
                        finpn.COMMITION = finp.COMMITION
                        finpn.TRDFLINES = finp.TRDFLINES
                        finpn.FINPAYTERMSS = vfinpaytermsOne
                        finpn.FINDOCS = vfindocs
                        finpn.INSMODE = 3
                        finpn.FINPAY = vlockid
                        finpn.TRDRRATE = finp.TRDRRATE
                        finpn.OPENMODE = finp.OPENMODE
                        finpn.UPDDATE = finp.UPDDATE
                        finpn.UPDUSER = finp.UPDUSER
                        finpn.FINDOCDIFF = finp.FINDOCDIFF
                        finpn.FXDIFF = finp.FXDIFF

                        db.FINPAYTERMs.InsertOnSubmit(finpn)

                        Dim find = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        find.LOCKID = vlockid
                    Else
                        vopitemtvalinslns = 0
                    End If
                    If vopntamntdebitinslns = 0 Then
                        vopitemtvalinslns = 0
                    End If

                End While
            Next

            ''4''''''''''''''''''''''''
            '            Select f.finpayterms As vfinpayterms
            ',f.findoc      as vfindoc
            'From finpayterms f, trdr t left outer Join trdextra ex on t.company=ex.company And t.sodtype=ex.sodtype And t.trdr=ex.trdr
            'Where f.company = 1000
            'And f.trdr=t.trdr
            'And t.trdr=1624 And t.sodtype=12
            'And f.paydemandmd in ( -2, -1, 1 )
            'And f.trndate<='20171129'

            Dim fifo4 = From f In fpays
                        Where (f.COMPANY = 1000) And ({-2, -1, 1}.Contains(f.PAYDEMANDMD)) And
                        (f.TRNDATE <= trndate)
                        Select vfinpayterms = f.FINPAYTERMS, vfindoc = f.FINDOC


            For Each q1 In fifo4
                '                Update finpayterms
                'set isclose=(case when opntamnt !=0 then 0 else 1 end)
                'where finpayterms =@vfinpayterms
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                finp.ISCLOSE = If(finp.OPNTAMNT <> 0, 0, 1)
            Next



        Catch ex As Exception

        End Try
        'Throw New NotImplementedException()
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
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
                Return False
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            'If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
            '    If Not Conditions() Then
            '        Exit Function
            '    End If
            'End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    'LogSQL = sSQL
                    db.Log = Nothing ' Console.Out
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
#End Region
#Region "96-MasterDataGridView"
    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Exit Sub
        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
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
    Private Sub MasterDataGridView_CellContentClick1(SoCarrier As Short)

        Try

            Dim q = db.ccCRoutings.AsQueryable
            Dim qwh = q.Where(Function(f) f.ISACTIVE > 0)
            qwh = qwh.Where(Function(f) f.SOCARRIER = SoCarrier)
            'qwh = qwh.OrderBy(Function(f) f.NAME)

            Me.DetailsBindingSource.DataSource = qwh
            Me.DetailDataGridView.DataSource = Me.DetailsBindingSource
            'AddOutOfOfficeColumn(Me.DetailDataGridView)

            'DetailDataGridView.Columns("DST_NAME").HeaderText = "ΠΕΡΙΟΧΗ"
            'DetailDataGridView.Columns("DST_NAME").Width = 150
            'DetailDataGridView.Columns("MORTGAGE_NAME").HeaderText = "ΟΝΟΜΑ"
            'DetailDataGridView.Columns("MORTGAGE_NAME").Width = 250

            DetailDataGridView_Styling()
            Dim aa = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        'End If
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            Me.MasterDataGridView.AutoResizeColumns()
            Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect



            myArrF = ("FINCODE,TRNDATE,ccCShippingNo,ccCTOTSHIPVALUE,ccCLockShipValue,SOCARRIER,CredVal,SumShTOT,bal,runtot,SumShVAL,ISCANCEL,APPRV,test,test1").Split(",")
            myArrN = ("FINCODE,TRNDATE,ccCShippingNo,ccCTOTSHIPVALUE,ccCLockShipValue,SOCARRIER,CredVal,SumShTOT,bal,runtot,SumShVAL,ISCANCEL,APPRV,test,test1").Split(",")




            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)
            For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            Next

            If Not IsNothing(MasterDataGridView.Columns("Ποσότητα Αποθέματος")) Then
                MasterDataGridView.Columns("Ποσότητα Αποθέματος").DefaultCellStyle.Format = "N3"
            End If


            'Add Unbound Columns

            'Dim SumShVALDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            'SumShVALDataGridViewTextBoxColumn.DataPropertyName = "SumShVAL"
            'SumShVALDataGridViewTextBoxColumn.HeaderText = "SumShVAL"
            'SumShVALDataGridViewTextBoxColumn.Name = "SumShVAL"
            'MasterDataGridView.Columns.Add(SumShVALDataGridViewTextBoxColumn)

            'Dim DataGridViewComboBoxColumnMTRL_CODE As New DataGridViewComboBoxColumn
            ''DataGridViewComboBoxColumnMTRL_CODE.DataPropertyName = "MTRL"
            ''Me.DataGridViewComboBoxColumn1.DataSource = Me.MTRLBindingSource
            'DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CODE"
            'If f.SERIES = 1001 Then
            '    DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CCCDIESNAME"
            'End If
            'DataGridViewComboBoxColumnMTRL_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            'DataGridViewComboBoxColumnMTRL_CODE.Name = "DataGridViewComboBoxColumnMTRL_CODE"
            'DataGridViewComboBoxColumnMTRL_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'DataGridViewComboBoxColumnMTRL_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            'DataGridViewComboBoxColumnMTRL_CODE.ValueMember = "MTRL"
            'DataGridViewComboBoxColumnMTRL_CODE.Width = 150

            ''
            ''Dim DdlMTRL1_CODE As New DataGridViewComboBoxColumn
            ''dlMTRL1_CODE.DataPropertyName = "MTRL"
            ''Me.DdlMTRL1_CODE.DataSource = Me.MTRLBindingSource
            ''DdlMTRL1_CODE.DisplayMember = "CODE"
            ''DdlMTRL1_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            ''DdlMTRL1_CODE.Name = "DdlMTRL1_CODE"
            ''DdlMTRL1_CODE.ValueMember = "MTRL"
            ''DdlMTRL1_CODE.Width = 150
            ''MasterDataGridView.Columns.Insert(0, DdlMTRL1_CODE)

            'Dim DataGridViewTextBox_OLDMTRL As New DataGridViewTextBoxColumn
            'DataGridViewTextBox_OLDMTRL.HeaderText = "OLDMTRL"
            'DataGridViewTextBox_OLDMTRL.Name = "DataGridViewTextBox_OLDMTRL"


            ''Search_Code
            ''
            'Dim Search_Code As New DataGridViewTextBoxColumn
            'Search_Code.Name = "Search_Code"
            ''Me.MasterDataGridView.Columns.Insert(0, Search_Code)
            'Select Case f.SERIES
            '    Case 9526
            '        Dim DataGridViewComboBoxColumnSTATUS As New DataGridViewComboBoxColumn
            '        '******************************
            '        DataGridViewComboBoxColumnSTATUS.DataPropertyName = "CCCSTATUSID"
            '        DataGridViewComboBoxColumnSTATUS.DataSource = Me.CCCSTATUSBindingSource
            '        DataGridViewComboBoxColumnSTATUS.DisplayMember = "DESCR"
            '        DataGridViewComboBoxColumnSTATUS.HeaderText = "STATUS"
            '        DataGridViewComboBoxColumnSTATUS.Name = "DataGridViewComboBoxColumnSTATUS"
            '        DataGridViewComboBoxColumnSTATUS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '        DataGridViewComboBoxColumnSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '        DataGridViewComboBoxColumnSTATUS.ValueMember = "CCCSTATUS"
            '        MasterDataGridView.Columns.Insert(2, DataGridViewComboBoxColumnSTATUS)
            '        ''***************************************

            'End Select
            'For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
            '    Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
            'Next

            'If Not IsNothing(MasterDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
            '    MasterDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
            'End If
            If Not IsNothing(MasterDataGridView.Columns("SumShVAL")) Then
                MasterDataGridView.Columns("SumShVAL").DefaultCellStyle.Format = "N2"
            End If
            If Not IsNothing(MasterDataGridView.Columns("SumShTOT")) Then
                MasterDataGridView.Columns("SumShTOT").DefaultCellStyle.Format = "N2"
            End If

            'If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ")) Then
            '    MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ")) Then
            '    MasterDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.")) Then
            '    MasterDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ")) Then
            '    MasterDataGridView.Columns("ΠΑΡΑΓΩΓΗ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
            '    MasterDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ")) Then
            '    MasterDataGridView.Columns("ΩΡΑ ΛΗΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m")) Then
            '    MasterDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m").DefaultCellStyle.Format = "N6"
            'End If
            'If Not IsNothing(MasterDataGridView.Columns("ΤΕΜ.PROFIL")) Then
            '    MasterDataGridView.Columns("ΤΕΜ.PROFIL").DefaultCellStyle.Format = "N0"
            'End If
            'If f.SERIES = 9593 Then 'ΑΒΑΦΟ
            '    If Not IsNothing(MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ")) Then
            '        MasterDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ").ReadOnly = True
            '    End If
            'End If



            ''Add Columns to MasterDataGridView
            'Me.MasterDataGridView.Columns.Insert(0, DataGridViewComboBoxColumnMTRL_CODE)
            'Me.MasterDataGridView.Columns.Insert(0, Search_Code)
            'Me.MasterDataGridView.Columns.Add(DataGridViewTextBox_OLDMTRL)
            'AddOutOfOfficeColumn(Me.MasterDataGridView)

            ''Fill Unbound Collumns
            'For Each row As DataGridViewRow In MasterDataGridView.Rows
            '    Dim dll As DataGridViewComboBoxCell = row.Cells("DataGridViewComboBoxColumnMTRL_CODE")
            '    Dim MTRL As Integer = row.Cells("MTRL").Value

            '    Dim m As MTRL = db.MTRLs.Where(Function(f1) f1.MTRL = MTRL).FirstOrDefault
            '    If Not IsNothing(m) Then
            '        dll.Items.Add(m)
            '        dll.Value = MTRL
            '        row.Cells("DataGridViewTextBox_OLDMTRL").Value = MTRL
            '    End If
            'Next


            ''DepartmentDataGridViewComboBoxColumn
            ''
            'Me.DepartmentDataGridViewComboBoxColumn.DataPropertyName = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.HeaderText = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
            'Me.DepartmentDataGridViewComboBoxColumn.Name = "DepartmentDataGridViewComboBoxColumn"
            'Me.DepartmentDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.DepartmentDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            ''
            ''StateDataGridViewComboBoxColumn
            ''
            'Me.StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            'Me.StateDataGridViewComboBoxColumn.HeaderText = "State"
            'Me.StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            'Me.StateDataGridViewComboBoxColumn.Name = "StateDataGridViewComboBoxColumn"
            'Me.StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                Dim item = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        'Dim dll As DataGridViewComboBoxCell = row.Cells("DepartmentDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Items.AddRange(New Object() {"ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
                        ''If Not IsNothing(item.Department) Then
                        ''    dll.Value = dll.Items(item.Department)
                        ''End If


                        'dll = row.Cells("domUserDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Value = dll.Items(0)
                        'If Not IsNothing(CCCVShipms) Then
                        '    'Dim OrLgs = CCCVShipms.
                        '    '    Where(Function(f) Not item.user_loginname = Nothing AndAlso f.domUser.Contains(item.user_loginname)).
                        '    '    Select(Function(f) f.domUser).Distinct.ToList
                        '    'dll.Items.AddRange(OrLgs.ToArray)

                        '    'If OrLgs.Count = 1 Then
                        '    '    dll.Value = OrLgs.FirstOrDefault
                        '    'End If
                        'End If


                        'dll = row.Cells("CapacityDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        ''If Not IsNothing(item.Capacity) Then
                        ''    For Each st In item.Capacity.Split("|")
                        ''        dll.Items.Add(st)
                        ''    Next
                        ''    dll.Value = item.Capacity.Split("|")(0)
                        ''Else
                        ''    dll.Items.AddRange(New Object() {"ALB", "EFG", "PIR"})
                        ''End If


                    Catch ex As Exception

                    End Try
                End If

            Next


        Catch ex As Exception

        End Try
    End Sub
    Private Sub DetailDataGridView_Styling()
        Try

            Me.DetailDataGridView.AutoGenerateColumns = True
            Me.DetailDataGridView.AutoResizeColumns()
            Me.DetailDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.DetailDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect


            'COMPANY, ROUTING, CODE, NAME, ACNMASK, ISACTIVE, REMARKS, SOKM, SOCOST, NUM01, NUM02, NUM03, CCCSOCARIER
            myArrF = ("FINCODE,TRNDATE,ccCShippingNo,ccCTOTSHIPVALUE,SOCARRIER,SumShVAL").Split(",")
            myArrN = ("FINCODE,TRNDATE,ccCShippingNo,ccCTOTSHIPVALUE,SOCARRIER,SumShVAL").Split(",")




            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(DetailDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            'AddOutOfOfficeColumn(Me.DetailDataGridView)
            For i As Integer = 0 To DetailDataGridView.Columns.Count - 1
                Debug.Print(DetailDataGridView.Columns(i).DataPropertyName & vbTab & DetailDataGridView.Columns(i).Name)
            Next

            If Not IsNothing(DetailDataGridView.Columns("Ποσότητα Αποθέματος")) Then
                DetailDataGridView.Columns("Ποσότητα Αποθέματος").DefaultCellStyle.Format = "N3"
            End If


            ''Add Unbound Columns

            Dim SumShVALDataGridViewTextBoxColumn As New DataGridViewTextBoxColumn
            SumShVALDataGridViewTextBoxColumn.DataPropertyName = "SumShVAL"
            SumShVALDataGridViewTextBoxColumn.HeaderText = "SumShVAL"
            SumShVALDataGridViewTextBoxColumn.Name = "SumShVAL"
            MasterDataGridView.Columns.Add(SumShVALDataGridViewTextBoxColumn)







            'Dim DataGridViewComboBoxColumnMTRL_CODE As New DataGridViewComboBoxColumn
            ''DataGridViewComboBoxColumnMTRL_CODE.DataPropertyName = "MTRL"
            ''Me.DataGridViewComboBoxColumn1.DataSource = Me.MTRLBindingSource
            'DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CODE"
            'If f.SERIES = 1001 Then
            '    DataGridViewComboBoxColumnMTRL_CODE.DisplayMember = "CCCDIESNAME"
            'End If
            'DataGridViewComboBoxColumnMTRL_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            'DataGridViewComboBoxColumnMTRL_CODE.Name = "DataGridViewComboBoxColumnMTRL_CODE"
            'DataGridViewComboBoxColumnMTRL_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'DataGridViewComboBoxColumnMTRL_CODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            'DataGridViewComboBoxColumnMTRL_CODE.ValueMember = "MTRL"
            'DataGridViewComboBoxColumnMTRL_CODE.Width = 150

            ''
            ''Dim DdlMTRL1_CODE As New DataGridViewComboBoxColumn
            ''dlMTRL1_CODE.DataPropertyName = "MTRL"
            ''Me.DdlMTRL1_CODE.DataSource = Me.MTRLBindingSource
            ''DdlMTRL1_CODE.DisplayMember = "CODE"
            ''DdlMTRL1_CODE.HeaderText = "ΚΩΔΙΚΟΣ"
            ''DdlMTRL1_CODE.Name = "DdlMTRL1_CODE"
            ''DdlMTRL1_CODE.ValueMember = "MTRL"
            ''DdlMTRL1_CODE.Width = 150
            ''DetailDataGridView.Columns.Insert(0, DdlMTRL1_CODE)

            'Dim DataGridViewTextBox_OLDMTRL As New DataGridViewTextBoxColumn
            'DataGridViewTextBox_OLDMTRL.HeaderText = "OLDMTRL"
            'DataGridViewTextBox_OLDMTRL.Name = "DataGridViewTextBox_OLDMTRL"


            ''Search_Code
            ''
            'Dim Search_Code As New DataGridViewTextBoxColumn
            'Search_Code.Name = "Search_Code"
            ''Me.DetailDataGridView.Columns.Insert(0, Search_Code)
            'Select Case f.SERIES
            '    Case 9526
            '        Dim DataGridViewComboBoxColumnSTATUS As New DataGridViewComboBoxColumn
            '        '******************************
            '        DataGridViewComboBoxColumnSTATUS.DataPropertyName = "CCCSTATUSID"
            '        DataGridViewComboBoxColumnSTATUS.DataSource = Me.CCCSTATUSBindingSource
            '        DataGridViewComboBoxColumnSTATUS.DisplayMember = "DESCR"
            '        DataGridViewComboBoxColumnSTATUS.HeaderText = "STATUS"
            '        DataGridViewComboBoxColumnSTATUS.Name = "DataGridViewComboBoxColumnSTATUS"
            '        DataGridViewComboBoxColumnSTATUS.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '        DataGridViewComboBoxColumnSTATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '        DataGridViewComboBoxColumnSTATUS.ValueMember = "CCCSTATUS"
            '        DetailDataGridView.Columns.Insert(2, DataGridViewComboBoxColumnSTATUS)
            '        ''***************************************

            'End Select
            'For i As Integer = 0 To DetailDataGridView.Columns.Count - 1
            '    Debug.Print(DetailDataGridView.Columns(i).DataPropertyName & vbTab & DetailDataGridView.Columns(i).Name)
            'Next

            'If Not IsNothing(DetailDataGridView.Columns("ΗΜ/ΝΙΑ")) Then
            '    DetailDataGridView.Columns("ΗΜ/ΝΙΑ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΒΕΡΓΕΣ")) Then
            '    DetailDataGridView.Columns("ΒΕΡΓΕΣ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ")) Then
            '    DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΑΒΑΦΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ")) Then
            '    DetailDataGridView.Columns("ΕΝΤΟΛΗ-ΛΕΥΚΟ").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.")) Then
            '    DetailDataGridView.Columns("ΤΕΜ.ΜΠΙΓ.").DefaultCellStyle.Format = "N0"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΠΑΡΑΓΩΓΗ")) Then
            '    DetailDataGridView.Columns("ΠΑΡΑΓΩΓΗ").DefaultCellStyle.Format = "d"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ")) Then
            '    DetailDataGridView.Columns("ΩΡΑ ΕΝΑΡΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΩΡΑ ΛΗΞΗ")) Then
            '    DetailDataGridView.Columns("ΩΡΑ ΛΗΞΗ").DefaultCellStyle.Format = "t"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m")) Then
            '    DetailDataGridView.Columns("ΒΑΡΟΣ ΠΡΟΦΙΛ gr/m").DefaultCellStyle.Format = "N6"
            'End If
            'If Not IsNothing(DetailDataGridView.Columns("ΤΕΜ.PROFIL")) Then
            '    DetailDataGridView.Columns("ΤΕΜ.PROFIL").DefaultCellStyle.Format = "N0"
            'End If
            'If f.SERIES = 9593 Then 'ΑΒΑΦΟ
            '    If Not IsNothing(DetailDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ")) Then
            '        DetailDataGridView.Columns("ΕΚΤΕΛΕΣΜΕΝΑ").ReadOnly = True
            '    End If
            'End If



            ''Add Columns to DetailDataGridView
            'Me.DetailDataGridView.Columns.Insert(0, DataGridViewComboBoxColumnMTRL_CODE)
            'Me.DetailDataGridView.Columns.Insert(0, Search_Code)
            'Me.DetailDataGridView.Columns.Add(DataGridViewTextBox_OLDMTRL)
            'AddOutOfOfficeColumn(Me.DetailDataGridView)

            ''Fill Unbound Collumns
            'For Each row As DataGridViewRow In DetailDataGridView.Rows
            '    Dim dll As DataGridViewComboBoxCell = row.Cells("DataGridViewComboBoxColumnMTRL_CODE")
            '    Dim MTRL As Integer = row.Cells("MTRL").Value

            '    Dim m As MTRL = db.MTRLs.Where(Function(f1) f1.MTRL = MTRL).FirstOrDefault
            '    If Not IsNothing(m) Then
            '        dll.Items.Add(m)
            '        dll.Value = MTRL
            '        row.Cells("DataGridViewTextBox_OLDMTRL").Value = MTRL
            '    End If
            'Next


            ''DepartmentDataGridViewComboBoxColumn
            ''
            'Me.DepartmentDataGridViewComboBoxColumn.DataPropertyName = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.HeaderText = "Department"
            'Me.DepartmentDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
            'Me.DepartmentDataGridViewComboBoxColumn.Name = "DepartmentDataGridViewComboBoxColumn"
            'Me.DepartmentDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.DepartmentDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            ''
            ''StateDataGridViewComboBoxColumn
            ''
            'Me.StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            'Me.StateDataGridViewComboBoxColumn.HeaderText = "State"
            'Me.StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            'Me.StateDataGridViewComboBoxColumn.Name = "StateDataGridViewComboBoxColumn"
            'Me.StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            'Me.StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In DetailDataGridView.Rows
                Dim item 'As CCCCheckZip = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        'Dim dll As DataGridViewComboBoxCell = row.Cells("DepartmentDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Items.AddRange(New Object() {"ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
                        ''If Not IsNothing(item.Department) Then
                        ''    dll.Value = dll.Items(item.Department)
                        ''End If


                        'dll = row.Cells("domUserDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Value = dll.Items(0)
                        'If Not IsNothing(CCCVShipms) Then
                        '    'Dim OrLgs = CCCVShipms.
                        '    '    Where(Function(f) Not item.user_loginname = Nothing AndAlso f.domUser.Contains(item.user_loginname)).
                        '    '    Select(Function(f) f.domUser).Distinct.ToList
                        '    'dll.Items.AddRange(OrLgs.ToArray)

                        '    'If OrLgs.Count = 1 Then
                        '    '    dll.Value = OrLgs.FirstOrDefault
                        '    'End If
                        'End If


                        'dll = row.Cells("CapacityDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        ''If Not IsNothing(item.Capacity) Then
                        ''    For Each st In item.Capacity.Split("|")
                        ''        dll.Items.Add(st)
                        ''    Next
                        ''    dll.Value = item.Capacity.Split("|")(0)
                        ''Else
                        ''    dll.Items.AddRange(New Object() {"ALB", "EFG", "PIR"})
                        ''End If


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
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click

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

    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
        Dim s As DataGridView = sender
        'If s.Rows.Count < 2 Then
        '    Exit Sub
        'End If
        If s.Columns(e.ColumnIndex).Name = "DepartmentDataGridViewComboBoxColumn" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim EditedVal As String = cell.EditedFormattedValue
            If Not cell.FormattedValue.ToString = EditedVal Then
                Dim item 'As CCCCheckZip = s.Rows(e.RowIndex).DataBoundItem
                'item.Department = cell.Value
                Dim ar As List(Of String) = ("--Επιλέγξτε--,ΔΙΚΗΓΟΡΟΣ,ΥΠΑΛΛΗΛΟΣ").Split(",").ToList
                'item.Department = ar.FindIndex(Function(f) f = EditedVal)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError

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
        Try
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim str As String = ""
                'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + me.MasterDataGridView.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
                Dim drv = Me.MasterBindingSource.Current
                str = "ccCRouting[AUTOLOCATE=" & drv.ccCRouting & "]"
                'str = "SALDOC[AUTOEXEC=2, FORCEVALUES=INT02:" & drv.FINDOC & "?SERIES:1001]"
                'XSupport.InitInterop(fS1HiddenForm.Handle)
                s1Conn.ExecS1Command(str, fS1HiddenForm)
                'Fillme.MasterDataGridView_gm(iActiveObjType)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    '    If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
    '        DateTimePicker1.Value = CTODate
    '    Else
    '        DateTimePicker1.Value = "01/01/" & Year(CTODate)
    '    End If
    'End Sub

    'Private Sub TlSBtnWHOUSE_Click(sender As Object, e As EventArgs) Handles TlSBtnWHOUSE.Click
    '    Dim ee As New System.ComponentModel.CancelEventArgs
    '    ee.Cancel = False
    '    ToolStripTextBox_Validating(sender, ee)
    'End Sub
    'Private Sub ToolStripTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TlSTxtWHOUSE.Validating
    '    Dim s As Object = sender
    '    'e.Cancel = False
    '    Dim Valid As Boolean = False
    '    Dim SelectSender As String = ""
    '    Select Case sender.GetType.Name
    '        Case "TextBox"
    '            SelectSender = s.Name
    '        Case "ToolStripButton", "ToolStripTextBox"
    '            SelectSender = s.Name
    '        Case "MyDataGridView", "GmDgView"
    '            SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
    '    End Select
    '    Dim ReturnFields As New ArrayList
    '    Dim errorMsg As String = ""
    '    Dim View As Boolean = False
    '    Dim GmTitle As String = ""
    '    'Dim GmTableName As String = ""
    '    Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
    '    Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
    '    Dim GmCheck As Boolean = False
    '    Dim ValidField As String = ""
    '    Dim sender_TAG As String = ""
    '    Dim Visible As Boolean = False
    '    Dim GmPelPro As Byte = 0
    '    RsWhere = "1=1"
    '    RsOrder = ""
    '    Select Case SelectSender

    '        Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '            TlSTxtWHOUSE.Tag = "WHOUSE"
    '            'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
    '            ReturnFields.Add(TlSTxtWHOUSE)
    '            'ReturnFields.Add(TlSTxtTimKin_Descr)
    '            ''''''''''''''''''''''''''''''''''''''''
    '            GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
    '            RsTables = "WHOUSE"
    '            '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
    '            '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.ccCRouting.PENDING >= 1)
    '            Company = 1000

    '            RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
    '            RsWhere = Trim(RsWhere)
    '            RsOrder = "SHORTCUT"
    '            'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
    '            sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
    '            'GmPelPro = 3 'Δεν υπάρχη PelPro Field
    '            'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
    '            sender_TAG = ReturnFields(0).Tag
    '            myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
    '            myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
    '            View = True
    '    End Select
    '    Dim m_dtGen As DataTable = Nothing
    '    Try
    '        If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
    '            ValidField = Trim(ReturnFields(0).Text)
    '            If Not ValidField.IndexOf("*").Equals(-1) Then
    '                ValidField = ValidField.Replace("*", "%")
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
    '            ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
    '                ValidField = "'" & ValidField.Replace(",", "','") & "'"
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
    '            Else
    '                RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
    '            End If
    '            'Visible = True
    '        Else 'ReturnFields(0).Text = ""
    '            If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
    '                '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
    '                For i As Integer = 0 To ReturnFields.Count - 1
    '                    ReturnFields(i).Text = ""
    '                    ReturnFields(i).ToolTipText = ""
    '                Next

    '                'sender.Focus()
    '                e.Cancel = False
    '                Exit Sub
    '                'Visible = True
    '            End If
    '        End If
    '        'RsTables = SelectPelPro(RsTables, PelPro)
    '        'RsWhere = SelectPelPro(RsWhere, PelPro)
    '        'RsOrder = SelectPelPro(RsOrder, PelPro)
    '        Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)

    '        'df = New GmData(sysDB, conn) 'My.Settings.GenConnectionString)
    '        'm_dtGen = df.GmFillTable(mSql, RsTables)
    '        m_dtGen = GmData.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
    '        'Dim dtb As New DataTable
    '        'Using cnn As New SqlConnection(conn)
    '        '    cnn.Open()
    '        '    Using cmd As New SqlCommand(mSql, cnn)
    '        '        'cmd.Parameters.AddWithValue("@COMPANY", 1000)
    '        '        'cmd.Parameters.AddWithValue("@SODTYPE", 51) '51 Αποθήκη
    '        '        'cmd.Parameters.AddWithValue("@DFROM", CDate("01/07/2017")) 'make sure you assign a value To startdate
    '        '        'cmd.Parameters.AddWithValue("@DTO", CDate("01/08/2017")) 'make sure you assign a value To 

    '        '        ''cmd.Parameters.AddWithValue("@MTRL", Nothing) ' AS INTEGER = 2115 --63 --NULL --384 --NULL

    '        '        'cmd.Parameters.AddWithValue("@CODE", "2103030557") ''--'%305%'
    '        '        'cmd.Parameters.AddWithValue("@WHOUSE", Me.TlSTxtWHOUSE.Text.Replace(",", "|")) '"2|4")
    '        '        ''--DECLARE @MTRLS  AS VARCHAR(250) = ''


    '        '        'cmd.Parameters.AddWithValue("@FISCPRD", 2017)
    '        '        'cmd.Parameters.AddWithValue("@PERIOD", 7)

    '        '        Try
    '        '            Using dr As SqlDataReader = cmd.ExecuteReader()
    '        '                'Dim tb = New DataTable()
    '        '                dtb.Load(dr)
    '        '                'Return tb
    '        '            End Using
    '        '        Catch ex As Exception
    '        '            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
    '        '        End Try

    '        '    End Using



    '        '    'Using dad As New SqlDataAdapter(str, cnn)
    '        '    '    dad.Fill(dtb)
    '        '    'End Using
    '        '    cnn.Close()
    '        'End Using
    '        'm_dtGen = dtb
    '        'm_dtGen.TableName = "WHOUSE"
    '        If m_dtGen.Rows.Count = 0 And sender.GetType.Name = "ToolStripButton" Then ' If  "ToolStripButton" SearchFR = Όλα
    '            'RsWhere = "1=1"
    '            'If GmPelPro = 0 Or GmPelPro = 1 Then
    '            '    RsWhere = Trim(RsWhere) & " and PELPRO = " & PelPro
    '            'End If
    '            'mSql = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
    '            'm_dtGen = df.GmFillTable(mSql, RsTables)
    '        End If
    '    Catch ex As Exception
    '        MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
    '    End Try
    '    m_dvGen = New DataView(m_dtGen)
    '    If m_dvGen.Count = 0 Then
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSBtnPOUDRA", "TlSTxtPROM", "TlSTxtPOUDRA"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '            Case "TlSBtnTimKin", "TlSTxtTimKin"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '                errorMsg = "Δεν βρέθηκε η Εγγραφή."
    '        End Select
    '        MsgBox(errorMsg)
    '        For i As Integer = 0 To ReturnFields.Count - 1
    '            ReturnFields(i).Text = ""
    '            ReturnFields(i).ToolTipText = ""
    '        Next
    '        'sender.Focus()
    '        e.Cancel = False
    '        Exit Sub
    '        View = False
    '    ElseIf m_dvGen.Count = 1 Then
    '        View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
    '    End If
    '    If View Or sender.GetType.Name = "ToolStripButton" Then
    '        For i As Integer = 0 To ReturnFields.Count - 1
    '            ReturnFields(i).Text = ""
    '            ReturnFields(i).ToolTipText = ""
    '        Next
    '        If Not s.GetType.Name = "ToolStripButton" Then
    '            ErrorProvider1.SetError(s.Control, "")
    '        End If
    '        'If Visible Then
    '        Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
    '        Dim ar As New ArrayList
    '        Dim m_ds As New DataSet
    '        m_ds = GmData.GmFillDataSet(m_ds, m_dtGen, m_dtGen.TableName)
    '        'ar.Add(m_ds)
    '        'ar.Add(GmTitle)
    '        'ar.Add(GmCheck)
    '        'ar.Add(sSQL)
    '        'ar.Add(GmPelPro)
    '        'ar.Add(RsWhere)
    '        'ar.Add(RsOrder)
    '        'ar.Add(GmGroupSql)
    '        'ar.Add(GmGroupSqlField)
    '        'ar.Add(RsTables)
    '        'ar.Add(myArrF)
    '        'ar.Add(myArrN)
    '        'ar.Add(Point)
    '        'ar.Add(Visible)
    '        Dim TSearchFR As New SearchFR
    '        TSearchFR.Conn = conn
    '        TSearchFR.m_ds = m_ds
    '        TSearchFR.Text = GmTitle
    '        TSearchFR.GmCheck = GmCheck
    '        TSearchFR.sSQL = sSQL
    '        TSearchFR.GmPelPro = Nothing 'GmPelPro
    '        TSearchFR.RsWhere = RsWhere
    '        TSearchFR.RsOrder = RsOrder
    '        TSearchFR.GmGroupSql = GmGroupSql
    '        TSearchFR.GmGroupSqlField = GmGroupSqlField
    '        TSearchFR.RsTables = RsTables
    '        TSearchFR.myArrF = myArrF
    '        TSearchFR.myArrN = myArrN
    '        TSearchFR.Location = Point
    '        TSearchFR.GmCheck = True
    '        'TSearchFR.Visible = View
    '        'TSearchFR.RetTBL = New DataTable
    '        'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
    '        TSearchFR.ShowDialog()
    '        'Dim SearceArrayList As ArrayList = TSearchFR.m_ArrayList 'SearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
    '        Dim Dt As DataTable = TSearchFR.RetTBL
    '        If Not TSearchFR.RetTBL Is Nothing Then

    '            'ReturnFields.Add(TlSTxtPCODE)
    '            'ReturnFields.Add(TlSTxtPELNAME1)
    '            If Dt.Rows.Count > 0 Then
    '                If Dt.Rows.Count = 1 Then
    '                    'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
    '                    'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
    '                    m_dvGen = Dt.DefaultView 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
    '                Else
    '                    Dim Result As String = ""
    '                    For i As Integer = 0 To Dt.Rows.Count - 1
    '                        Result += Trim(Dt.Rows(i)(0)) & "," '("P1_PCODE")
    '                    Next
    '                    ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
    '                End If
    '                ReturnFields(0).ToolTipText = ReturnFields(0).Text
    '                'GmDgLookUp_FillNew = SearceArrayList
    '            Else
    '                '    MsgBox("Δεν υπάρχουν Εγγραφές")
    '                'If Not IsNumeric(TextBox1.Text) Then
    '                If Not s.GetType.Name = "ToolStripButton" Then
    '                    'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
    '                End If
    '                'Else
    '                '    ' Clear the error.
    '                '    ErrorProvider1.SetError(TextBox1, "")
    '                'End If
    '            End If
    '        Else
    '            MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
    '        End If
    '    End If

    '    If m_dvGen.Count = 1 Then
    '        'Dim data_row As DataRowView
    '        'data_row = bindingTim1.Current()
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSTxtPROM"
    '                ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
    '                'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
    '            Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
    '                ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
    '                ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
    '            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
    '                'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
    '                'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
    '                ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))
    '        End Select
    '        Dim data_row As DataRowView
    '        'data_row = BindingSource1.Current()
    '        Select Case SelectSender
    '            Case "TlSBtnPROM", "TlSTxtPROM"
    '                data_row.Item("ETERIA") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer)) 'CInt(Me.GmDgLookUp1.GmReturnFields(2).Text)
    '            'Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
    '            '    If data_row.Item("ETERIA") Is DBNull.Value Then
    '            '        MsgBox("Προσοχή !!! Δεν Βρέθηκε Προμηθευτής", MsgBoxStyle.Critical, "GmError")
    '            '        For i As Integer = 0 To ReturnFields.Count - 1
    '            '            ReturnFields(i).Text = ""
    '            '            ReturnFields(i).ToolTipText = ""
    '            '        Next
    '            '        Exit Sub
    '            '    End If
    '            '    data_row.Item("COLOR") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer))
    '            '    data_row.Item("VAL") = GmNull(m_dvGen(0)("CCCPRICE"), GetType(Double))
    '            '    STOCKTextBox.Text = GmNull(m_dvGen(0)("CCCSSTOCK"), GetType(Double))
    '            '    VALTextBox.Text = data_row.Item("VAL")
    '            '    RsWhere = "Company = " & Company
    '            '    RsWhere = RsWhere & " AND COLOR = " & data_row.Item("COLOR")
    '            '    RsWhere = RsWhere & " AND ETERIA = " & data_row.Item("ETERIA")
    '            '    sSQL = "SELECT TQTY " &
    '            '           "FROM VTOTCCCPOUDRES AS VT " &
    '            '           "WHERE " & RsWhere
    '            '    Dim SumTable As DataTable = df.GmFillTable(sSQL, "VT")
    '            '    'Dim m_dvSum As DataView = New DataView(SumTable)
    '            '    If SumTable.Rows.Count = 1 Then
    '            '        TYPOLQTY = SumTable.Rows(0)("TQTY")
    '            '    Else
    '            '        TYPOLQTY = 0
    '            '    End If
    '            '    Me.txtCTQTY.Text = Format(TYPOLQTY, "#,###") 'Format(data_row("TQTY") - data_row("QTY"), "#,###.#0")
    '            Case "TlSBtnTimKin", "TlSTxtTimKin"
    '                data_row.Item("KK") = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
    '        End Select
    '    End If
    '    If ReturnFields.Count = 3 Then
    '        ReturnFields(ReturnFields.Count - 1).Focus()
    '    End If
    'End Sub
    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click

    End Sub


    Private Sub tlsBtnUpdateccCTOTSHIPVALUE_Click(sender As Object, e As EventArgs) Handles tlsBtnUpdateccCTOTSHIPVALUE.Click
        Dim q = db.FINDOCs.Where(Function(f) f.COMPANY = 1000 And f.SOSOURCE = 1351 And {7040, 7041, 7046}.Contains(f.FPRMS))
        Try
            For Each fin In q
                Dim mtrdoc = fin.MTRDOC
                If mtrdoc.ccCLockShipValue = True Then
                    Continue For
                End If
                Dim ccCTOTSHIPVALUE = fin.MTRLINEs.Sum(Function(f) f.QTY1 * f.ccCSHIPVALUE)
                If Not ccCTOTSHIPVALUE = 0 Then
                    mtrdoc.ccCTOTSHIPVALUE = ccCTOTSHIPVALUE
                End If


            Next
        Catch ex As Exception

        End Try
    End Sub


    Private Sub tlsBtnCreateFIFO_Click(sender As Object, e As EventArgs) Handles tlsBtnCreateFIFO.Click
        Try
            'Dim vfinpayterms As Integer
            'Dim vtrdr As Integer
            'Dim vopntamnt As Double
            'Dim vfindoc As Integer
            'Dim vsocurrency As Short
            Dim vopitemtvalinslns As Double
            Dim vfinpaytermsOne As Integer
            Dim vopntamntdebitins As Double
            Dim vopntamntdebitinslns As Double
            Dim vlockid As Integer
            Dim vfindocs As Integer

            Dim trndate As Date = Me.DateTimePicker1.Value
            Dim trdr As Integer = 134

            LoadData()
            db.Log = Console.Out
            'INSMODE
            '+Manual f.insmode in ( 3, 1 )
            'f.insmode in ( 3, 0 )
            Dim fifo = From f In db.FINPAYTERMs, t In db.TRDRs
                       Where (f.COMPANY = 1000) And f.TRDR = t.TRDR And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -2) And
                        (f.TRNDATE <= trndate) And {3, 0}.Contains(f.INSMODE) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And If(f.FINDOCDIFF, 0) = 0
                       Order By f.FINPAYTERMS, f.FINPAYTERMSS
                       Select vfinpayterms = f.FINPAYTERMS, vfinpaytermss = f.FINPAYTERMSS, vfindoc = f.FINDOC, vtrdr = f.TRDR, vtrdflines = If(f.TRDFLINES, 0), vtamnt = f.TAMNT


            For Each q1 In fifo
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpaytermss And f.PAYDEMANDMD = 1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINDOC = q1.vfindoc And f.TRDR = q1.vtrdr And If(f.TRDFLINES, 0) = q1.vtrdflines And f.PAYDEMANDMD = -1).FirstOrDefault
                finp.OPNTAMNT = Math.Round(finp.OPNTAMNT + q1.vtamnt, 7, MidpointRounding.AwayFromZero)

                finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault

                db.FINPAYTERMs.DeleteOnSubmit(finp)
            Next

            ''2'''''''''''''''''''''''''''''''
            Dim fifo1 = From fin In db.FINDOCs Join f In db.FINPAYTERMs On fin.FINDOC Equals f.FINDOC
                        Join t In db.TRDRs On f.TRDR Equals t.TRDR
                        Where (f.COMPANY = 1000) And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (Not fin.CNTR Is Nothing) Or
                        (f.COMPANY = 1000) And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (0 = 0)
                        Order By f.TRDR, f.TRNDATE, f.FINDOC, f.LINENUM
                        Select vfinpayterms = f.FINPAYTERMS, vtrdr = f.TRDR, vopntamnt = Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero), vfindoc = f.FINDOC, vsocurrency = f.SOCURRENCY


            For Each q1 In fifo1
                vopitemtvalinslns = q1.vopntamnt
                While vopitemtvalinslns <> 0
                    vfinpaytermsOne = -1

                    Dim q2 = (From fin In db.FINDOCs, f In db.FINPAYTERMs
                              Where f.FINDOC = fin.FINDOC And f.PAYDEMANDMD = 1 And f.TRDR = q1.vtrdr And
                                          f.TRNDATE <= trndate And Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0 And f.ISCANCEL = 0 And f.APPRV = 1 And
                                          f.COMPANY = 1000 And f.SOCURRENCY = q1.vsocurrency And ((Not fin.CNTR Is Nothing) Or (0 = 0))
                              Order By f.TRNDATE).FirstOrDefault

                    If Not IsNothing(q2) Then
                        vfinpaytermsOne = q2.f.FINPAYTERMS '- - DECLARE NOTIMELIMT
                    End If

                    If vfinpaytermsOne <> -1 Then
                        vopntamntdebitins = 0
                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        If Not IsNothing(finp) Then
                            vfindocs = finp.FINDOC
                            vopntamntdebitins = Math.Round(finp.OPNTAMNT, 7, MidpointRounding.AwayFromZero)
                        End If
                    End If

                    If vopntamntdebitins > vopitemtvalinslns Then
                        vopntamntdebitinslns = vopitemtvalinslns
                        vopitemtvalinslns = 0
                    Else
                        vopntamntdebitinslns = vopntamntdebitins
                        vopitemtvalinslns = vopitemtvalinslns - vopntamntdebitins
                    End If

                    If vfinpaytermsOne <> -1 Then

                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7)

                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7, MidpointRounding.AwayFromZero)

                        Dim fin = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        If Not IsNothing(fin) Then
                            vlockid = fin.LOCKID
                            vlockid = vlockid + 1
                        End If


                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms And Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero) <> 0).FirstOrDefault

                        If Not IsNothing(finp) Then
                            Dim finpn = New FINPAYTERM

                            finpn.COMPANY = finp.COMPANY
                            finpn.FINDOC = finp.FINDOC
                            finpn.LINENUM = finp.LINENUM
                            finpn.TRDR = finp.TRDR
                            finpn.TRDBRANCH = finp.TRDBRANCH
                            finpn.SOCURRENCY = finp.SOCURRENCY
                            finpn.PAYDEMANDMD = -2
                            finpn.SOPAYTYPE = finp.SOPAYTYPE
                            finpn.ISCANCEL = finp.ISCANCEL
                            finpn.APPRV = finp.APPRV
                            finpn.FINALDATE = finp.FINALDATE
                            finpn.TRNDATE = finp.TRNDATE
                            finpn.ENDDATE = finp.ENDDATE
                            finpn.AMNT = 0
                            finpn.TAMNT = Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero)
                            finpn.LAMNT = 0
                            finpn.OPNTAMNT = 0
                            finpn.ISCLOSE = finp.ISCLOSE
                            finpn.COMMENTS = finp.COMMENTS
                            finpn.PAYMENT = finp.PAYMENT
                            finpn.PAYGRPVAL = finp.PAYGRPVAL
                            finpn.INSTALMENT = finp.INSTALMENT
                            finpn.COMMITION = finp.COMMITION
                            finpn.TRDFLINES = finp.TRDFLINES
                            finpn.FINPAYTERMSS = vfinpaytermsOne
                            finpn.FINDOCS = vfindocs
                            finpn.INSMODE = 3
                            finpn.FINPAY = vlockid
                            finpn.TRDRRATE = finp.TRDRRATE
                            finpn.OPENMODE = finp.OPENMODE
                            finpn.UPDDATE = finp.UPDDATE
                            finpn.UPDUSER = finp.UPDUSER
                            finpn.FINDOCDIFF = finp.FINDOCDIFF
                            finpn.FXDIFF = finp.FXDIFF

                            db.FINPAYTERMs.InsertOnSubmit(finpn)
                        End If


                        Dim find = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        find.LOCKID = vlockid 'update
                    Else
                        vopitemtvalinslns = 0
                    End If
                    If vopntamntdebitinslns = 0 Then
                        vopitemtvalinslns = 0
                    End If

                End While
            Next

            ''3''''''''''''''''''''''''''''
            Dim fifo3 = From fin In db.FINDOCs Join f In db.FINPAYTERMs On fin.FINDOC Equals f.FINDOC
                        Join t In db.TRDRs On f.TRDR Equals t.TRDR
                        Where (f.COMPANY = 1000) And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (Not fin.CNTR Is Nothing) Or
                        (f.COMPANY = 1000) And (t.TRDR = trdr) And (t.SODTYPE = 13) And (f.PAYDEMANDMD = -1) And
                        (f.TRNDATE <= trndate) And (Math.Round(f.TAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And (Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0) And
                        (f.ISCANCEL = 0) And (f.APPRV = 1) And (0 = 0) And (0 = 0)
                        Order By f.TRDR, f.TRNDATE, f.FINDOC, f.LINENUM
                        Select vfinpayterms = f.FINPAYTERMS, vtrdr = f.TRDR, vtrdbranch = f.TRDBRANCH, vopntamnt = Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero),
                            vfindoc = f.FINDOC, vsocurrency = f.SOCURRENCY


            For Each q1 In fifo3


                vopitemtvalinslns = q1.vopntamnt
                While vopitemtvalinslns <> 0
                    vfinpaytermsOne = -1

                    Dim q2 = (From fin In db.FINDOCs, f In db.FINPAYTERMs
                              Where f.FINDOC = fin.FINDOC And f.PAYDEMANDMD = 1 And f.TRDR = q1.vtrdr And f.TRDBRANCH = q1.vtrdbranch And
                                          f.TRNDATE <= trndate And Math.Round(f.OPNTAMNT, 7, MidpointRounding.AwayFromZero) <> 0 And f.ISCANCEL = 0 And f.APPRV = 1 And
                                          f.COMPANY = 1000 And f.SOCURRENCY = q1.vsocurrency And ((Not fin.CNTR Is Nothing) Or (0 = 0))
                              Order By f.TRNDATE).FirstOrDefault '- - DECLARE NOTIMELIMT


                    If Not IsNothing(q2) Then
                        vfinpaytermsOne = q2.f.FINPAYTERMS '- - DECLARE NOTIMELIMT
                    End If


                    If vfinpaytermsOne <> -1 Then
                        vopntamntdebitins = 0
                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        If Not IsNothing(finp) Then
                            vfindocs = finp.FINDOC
                            vopntamntdebitins = Math.Round(finp.OPNTAMNT, 7, MidpointRounding.AwayFromZero)
                        End If
                    End If

                    If vopntamntdebitins > vopitemtvalinslns Then
                        vopntamntdebitinslns = vopitemtvalinslns
                        vopitemtvalinslns = 0
                    Else
                        vopntamntdebitinslns = vopntamntdebitins
                        vopitemtvalinslns = vopitemtvalinslns - vopntamntdebitins
                    End If

                    If vfinpaytermsOne <> -1 Then

                        Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = vfinpaytermsOne).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7)

                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                        finp.OPNTAMNT = Math.Round((finp.OPNTAMNT - vopntamntdebitinslns), 7, MidpointRounding.AwayFromZero)

                        vlockid = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault.LOCKID
                        vlockid = vlockid + 1

                        finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms And Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero) <> 0).FirstOrDefault

                        Dim finpn = New FINPAYTERM

                        finpn.COMPANY = finp.COMPANY
                        finpn.FINDOC = finp.FINDOC
                        finpn.LINENUM = finp.LINENUM
                        finpn.TRDR = finp.TRDR
                        finpn.TRDBRANCH = finp.TRDBRANCH
                        finpn.SOCURRENCY = finp.SOCURRENCY
                        finpn.PAYDEMANDMD = -2
                        finpn.SOPAYTYPE = finp.SOPAYTYPE
                        finpn.ISCANCEL = finp.ISCANCEL
                        finpn.APPRV = finp.APPRV
                        finpn.FINALDATE = finp.FINALDATE
                        finpn.TRNDATE = finp.TRNDATE
                        finpn.ENDDATE = finp.ENDDATE
                        finpn.AMNT = 0
                        finpn.TAMNT = Math.Round(vopntamntdebitinslns, 7, MidpointRounding.AwayFromZero)
                        finpn.LAMNT = 0
                        finpn.OPNTAMNT = 0
                        finpn.ISCLOSE = finp.ISCLOSE
                        finpn.COMMENTS = finp.COMMENTS
                        finpn.PAYMENT = finp.PAYMENT
                        finpn.PAYGRPVAL = finp.PAYGRPVAL
                        finpn.INSTALMENT = finp.INSTALMENT
                        finpn.COMMITION = finp.COMMITION
                        finpn.TRDFLINES = finp.TRDFLINES
                        finpn.FINPAYTERMSS = vfinpaytermsOne
                        finpn.FINDOCS = vfindocs
                        finpn.INSMODE = 3
                        finpn.FINPAY = vlockid
                        finpn.TRDRRATE = finp.TRDRRATE
                        finpn.OPENMODE = finp.OPENMODE
                        finpn.UPDDATE = finp.UPDDATE
                        finpn.UPDUSER = finp.UPDUSER
                        finpn.FINDOCDIFF = finp.FINDOCDIFF
                        finpn.FXDIFF = finp.FXDIFF

                        db.FINPAYTERMs.InsertOnSubmit(finpn)

                        Dim find = db.FINDOCs.Where(Function(f) f.FINDOC = q1.vfindoc).FirstOrDefault
                        find.LOCKID = vlockid
                    Else
                        vopitemtvalinslns = 0
                    End If
                    If vopntamntdebitinslns = 0 Then
                        vopitemtvalinslns = 0
                    End If

                End While
            Next

            ''4''''''''''''''''''''''''
            Dim fifo4 = From f In db.FINPAYTERMs, t In db.TRDRs
                        Where (f.COMPANY = 1000) And f.TRDR = t.TRDR And (t.TRDR = trdr) And (t.SODTYPE = 13) And ({-2, -1, 1}.Contains(f.PAYDEMANDMD)) And
                        (f.TRNDATE <= trndate)
                        Select vfinpayterms = f.FINPAYTERMS, vfindoc = f.FINDOC


            For Each q1 In fifo4
                Dim finp = db.FINPAYTERMs.Where(Function(f) f.FINPAYTERMS = q1.vfinpayterms).FirstOrDefault
                finp.ISCLOSE = If(finp.OPNTAMNT <> 0, 0, 1)
            Next



        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        'LoadDataInit() 'For Bind Any Control
    End Sub
    ' Load the data.
    Private Sub LoadData()
        db = New DataClassesHglpDataContext(conn) 'My.Settings.GenConnectionString)
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            'Me.MasterBindingSource.DataSource = db.CCCCheckZips.Where(Function(f) f.ZIP = 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Exit Sub
        Me.DataSafe()
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




#End Region

End Class

Friend Class RTotals
    Public Property Bal As Double
    Public Property RTots As Double
    Public Property TRNDATE As Date
End Class

Friend Class FPTerms
    Property COMPANY As Short

    Property FINPAYTERMS As Integer

    Property FINDOC As Integer

    Property LINENUM As Integer

    Property FINPAY As Integer

    Property TRDR As Integer

    Property TRDBRANCH As System.Nullable(Of Integer)

    Property SOCURRENCY As Short

    Property PAYDEMANDMD As Short

    Property SOPAYTYPE As System.Nullable(Of Short)

    Property ISCANCEL As Short

    Property APPRV As Short

    Property FINALDATE As Date

    Property TRNDATE As Date

    Property ENDDATE As System.Nullable(Of Date)

    Property TRDRRATE As Double

    Property AMNT As Double

    Property TAMNT As Double

    Property LAMNT As Double

    Property OPNTAMNT As Double

    Property ISCLOSE As Short

    Property COMMENTS As String

    Property PAYMENT As System.Nullable(Of Short)

    Property PAYGRPVAL As System.Nullable(Of Integer)

    Property INSTALMENT As System.Nullable(Of Short)

    Property COMMITION As System.Nullable(Of Double)

    Property TRDFLINES As System.Nullable(Of Integer)

    Property FINPAYTERMSS As System.Nullable(Of Integer)

    Property FINDOCS As System.Nullable(Of Integer)

    Property INSMODE As Short

    Property OPENMODE As System.Nullable(Of Short)

    Property UPDDATE As System.Nullable(Of Date)

    Property UPDUSER As System.Nullable(Of Short)

    Property FINDOCDIFF As System.Nullable(Of Integer)

    Property FXDIFF As System.Nullable(Of Short)

    Public Property CNTR As System.Nullable(Of Integer)
    Public Property LOCKID As Integer
    Public Property SOCARRIER As Short?
End Class
