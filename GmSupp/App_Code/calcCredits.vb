Imports GmSupp.Hglp
Imports Softone

Public Class calcCreditss

    Public Shared Property fS1HiddenForm As New Form


    Public Shared Function calcCredits(db As DataClassesHglpDataContext, drv As ccCVShipment) As IQueryable(Of ccCVShipment)
        Dim vs = db.ccCVShipments.AsQueryable.Where(Function(f) f.FINDOC = drv.FINDOC)
        Dim vships As New List(Of ccCVShipment)
        vships.AddRange(vs)
        'vships.Clear()
        'For Each v In vs
        '    vships.Add(v)
        'Next
        'Try
        '    'Me.Cursor = Cursors.WaitCursor
        '    Dim str As String = ""
        '    'str = IIf(iActiveObjType = 1351, "SALDOC", "") & "[AUTOLOCATE=" & me.MasterDataGridView.Rows(e.RowIndex).Cells("ID").Value.ToString & "]"

        '    str = "SALDOC[AUTOLOCATE=" & drv.FINDOC & "]"
        '    'str = "SALDOC[AUTOEXEC=2, FORCEVALUES=INT02:" & drv.FINDOC & "?SERIES:1001]"
        '    'XSupport.InitInterop(fS1HiddenForm.Handle)
        '    s1Conn.ExecS1Command(str, fS1HiddenForm)
        '    'Fillme.MasterDataGridView_gm(iActiveObjType)
        'Catch ex As Exception
        '    MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        'Finally
        '    'Me.Cursor = Cursors.Default
        'End Try
        'Exit Sub




        Dim ans = 0
        Dim vDate As Date
        Dim vQty As Double = 0
        Dim vLineVal As Double = 0
        Dim vList = 0
        Dim vGroupList
        Dim vGrList
        Dim vPrice As Double = 0
        Dim vPriceD As Double = 0
        Dim vPriceVal As Double = 0
        Dim vPis = 0
        Dim vWhouse = 0
        Dim recs = 0
        Dim vQtyY As Double = 0
        Dim vQtyI As Double = 0
        Dim vQtyA As Double = 0
        Dim vQtyMYD As Double = 0
        Dim cmd = 0
        Dim comp = 1000
        Dim dsc As Double = 0

        Dim dsTRDR As New DataSet
        'Dim ixTable As XTable

        'Dim Str As String = "Select TRDR As ID, Code, Name, Address, City, Phone01  FROM TRDR " &
        '            " WHERE SODTYPE=" & 13 & " And COMPANY=" & s1Conn.ConnectionInfo.CompanyId.ToString &
        '            " ORDER BY TRDR DESC"

        'ixTable = s1Conn.GetSQLDataSet(Str)
        'ixTable.TableName = "TRDR"
        'dsTRDR.Tables.Add(ixTable.CreateDataTable(True))
        ''dgTRDR.DataSource = dsTRDR.Tables("TRDR")

        'ixTable = s1Conn.GetSQLDataSet("select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=" & comp ')
        'dsTRDR.Tables.Add(ixTable.CreateDataTable(True))
        'Dim rset As XTable = s1Conn.GetSQLDataSet("select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=" & comp) ')
        Dim rset = db.cccSettings.Where(Function(f) f.Company = comp).FirstOrDefault
        'Dim rser As XTable = s1Conn.GetSQLDataSet("select seriescre from cccsettingslines where seriesinv=" & drv.SERIES) ')
        Dim rser = db.cccSettingsLines.Where(Function(f) f.SeriesInv = drv.SERIES).FirstOrDefault

#Region "gg"
        'Έλεγχος Νομού
        If rset.District = 1 Then
            'Dim sd As XTable = s1Conn.GetSQLDataSet("SELECT DISTRICT1 FROM TRDBRANCH WHERE TRDBRANCH=" & drv.TRDBRANCH)
            Dim sd = db.TRDBRANCHes.Where(Function(f) f.TRDBRANCH = drv.TRDBRANCH).FirstOrDefault
            If sd.DISTRICT1 = 0 Then
                MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΝΟΜΟ ΠΑΡΑΛΗΠΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                Return Nothing
            End If
        End If


        'Έλεγχος εμπορικής κατηγορίας
        If rset.Trdbusiness = 1 Then
            'Dim sb As XTable = s1Conn.GetSQLDataSet("SELECT TRDBUSINESS FROM TRDR WHERE TRDR=" & drv.TRDR)
            Dim sb = db.TRDRs.Where(Function(f) f.TRDR = drv.TRDR).FirstOrDefault
            If sb.TRDBUSINESS = 0 Then
                MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΕΜΠΟΡΙΚΗ ΚΑΤΗΓΟΡΙΑ ΠΕΛΑΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                Return Nothing
            End If
        End If

        'Έλεγχος τρόπου αποστολής
        If rset.Shipment = 1 Then
            If drv.SHIPMENT = 0 Then
                MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                Return Nothing
            End If
        End If

        'Τιμοκατάλογος Master - Details
        'Dim spis As XTable = s1Conn.GetSQLDataSet(((("select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=" & drv.TRDR & " and p.sosource in (4,6) and l.shipment=") & drv.SHIPMENT & " and l.district1=(select district1 from trdbranch where trdr=") & drv.TRDR & " and trdbranch=") & drv.TRDBRANCH & ")")


        ''if(spis.RECORDCOUNT>0)
        ''{

        vDate = drv.SHIPDATE ' X.EVAL("SQLDate(SALDOC.TRNDATE)")

        'ITELINES.FIRST
        'While Not ITELINES.EOF
        For Each itelines In vships
            'Dim swh As XTable = s1Conn.GetSQLDataSet("select whouse from mtrdoc where findoc=" & itelines.FINDOCS)
            'Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
            'vWhouse = 0
            'If Not IsNothing(swh) Then
            '    vWhouse = swh.WHOUSE
            'End If
            'If vWhouse = 0 Then
            '    vWhouse = itelines.WHOUSE
            'End If
            ''sp4 = s1Conn.GetSQLDataSet("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=" & itelines.MTRL, null);
            ''Dim sp4 As XTable = s1Conn.GetSQLDataSet("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 1 = 1 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=" & itelines.MTRL & " and d.fromdate <= " & "'" & vDate.ToString("yyyyMMdd") & "'" & " and d.finaldate >= " & "'" & vDate.ToString("yyyyMMdd") & "'")
            'Dim sp4 = (From d In db.cccPriceListLines, h In db.cccPriceLists
            '           Where d.cccPriceList = h.cccPriceList And
            '                           h.Sosource = 7 And d.Whouse = vWhouse And d.Mtrl = itelines.MTRL And d.Fromdate <= vDate And d.Finaldate >= vDate).FirstOrDefault.d
            Dim sg = db.MTRLs.Where(Function(f) f.MTRL = itelines.MTRL).FirstOrDefault

            If Not IsNothing(sg) Then
                '101 ΜΗ Υδατοδιαλυτά

                'MTRGROUP    cccSumGroup	Name
                '100 100	Υδατοδιαλυτά
                '100 103	Α' Υλες
                '100 200	Καμία
                '101 103	Α' Υλες
                '102 102	Ιχνοστοιχεία 
                Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = If(itelines.FINDOCS, 0)).FirstOrDefault
                vWhouse = 0
                If Not IsNothing(swh) Then
                    vWhouse = swh.WHOUSE
                End If
                If vWhouse = 0 Then
                    vWhouse = itelines.WHOUSE
                End If

                '7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                Dim sp4 = (From l In db.cccPriceListLines, h In db.cccPriceLists
                           Where l.cccPriceList = h.cccPriceList And
                                       h.Sosource = 7 And l.Whouse = vWhouse And l.Mtrl = itelines.MTRL And l.Fromdate <= vDate And l.Finaldate >= vDate
                           Select New With {.igroup = l.cccSumGroup}).FirstOrDefault


                'spmyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' 
                'And l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=4)', null);

                If Not IsNothing(sp4) AndAlso sp4.igroup = 100 Then 'Υδατοδιαλυτά
                    vQtyY = vQtyY + itelines.QTY1
                ElseIf Not IsNothing(sp4) AndAlso sp4.igroup = 103 Then  'Α' Υλες
                    vQtyA = vQtyA + itelines.QTY1
                ElseIf Not IsNothing(sp4) AndAlso sp4.igroup = 102 Then 'Ιχνοστοιχεία
                    vQtyI = vQtyI + itelines.QTY1
                ElseIf sg.MTRGROUP = 101 Then 'ΜΗ Υδατοδιαλυτά και όχι Α' Υλες
                    If Not IsNothing(sp4) AndAlso Not sp4.igroup = 103 Then
                        vQtyMYD = vQtyMYD + itelines.QTY1
                    Else
                        vQtyMYD = vQtyMYD + itelines.QTY1
                    End If

                End If
                vQty = vQty + itelines.QTY1
            End If

            '    ITELINES.[NEXT]
            'End While

        Next

        'ITELINES.FIRST
        'While Not ITELINES.EOF
        'Dim MTRDOC_QTY1 = vships.Sum(Function(f) f.QTY1)
        For Each itelines In vships

            'Dim sg As XRow = s1Conn.GetSQLDataSet("select mtrgroup from mtrl where mtrl=" & itelines.MTRL).Current
            Dim sg = db.MTRLs.Where(Function(f) f.MTRL = itelines.MTRL).FirstOrDefault
            'X.WARNING(sg.mtrgroup);
            '7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
            'Dim sp4 = (From l In db.cccPriceListLines, h In db.cccPriceLists
            '           Where l.cccPriceList = h.cccPriceList And
            '                           h.Sosource = 7 And l.Whouse = vWhouse And l.Mtrl = itelines.MTRL And l.Fromdate <= vDate And l.Finaldate >= vDate
            '           Select New With {.igroup = l.cccSumGroup}).FirstOrDefault

            vGrList = Convert.ToString(rset.GroupYD & "," & Convert.ToString(rset.GroupMYD))
            'vGroupList = X.EVAL("InList(" & sg.mtrgroup & "," & vGrList & ")");
            'vGroupList = X.EVAL("InList(" & sg.mtrgroup & "," & Convert.ToString(rset.GroupMYD) & ")")
            vGroupList = Convert.ToString(rset.GroupMYD).Contains(sg.MTRGROUP)
            'X.WARNING(vGroupList);

            If vGroupList = True Then
                'If IsNothing(sp4) Or (Not IsNothing(sp4) AndAlso Not sp4.igroup = 103) Then
                '    Continue For
                'End If
                'ΜΗ Υδατοδιαλυτά
                'Dim swh As XTable = s1Conn.GetSQLDataSet("select whouse from mtrdoc where findoc=" & itelines.FINDOCS)
                Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = If(itelines.FINDOCS, 0)).FirstOrDefault
                vWhouse = 0
                If Not IsNothing(swh) Then
                    vWhouse = swh.WHOUSE
                End If
                If vWhouse = 0 Then
                    vWhouse = itelines.WHOUSE
                End If
                'Dim sp As XTable = s1Conn.GetSQLDataSet(((((("select isnull(dbo.fn_clDiscStep1(" & vDate & "," & vWhouse & ",") & drv.TRDBRANCH & ",") & itelines.MTRL & ",") & drv.TRDR & ",") & drv.TRDBUSINESS & ",") & drv.SHIPMENT & "),0) AS dsc")
                'Dim spp As XTable = s1Conn.GetSQLDataSet((((((("select isnull(dbo.fn_clDiscStep2(" & vDate & "," & vWhouse & ",") & drv.TRDBRANCH & ",") & itelines.MTRL & ",") & drv.TRDR & ",") & drv.TRDBUSINESS & ",") & drv.SHIPMENT & "," & """") & MTRDOC.QTY1 & """" & "),0) AS val")

                'Select Case@res=d.disc1prc 
                '   From cccPriceListLines d, cccPriceList h
                'Where d.cccpricelist = h.cccpricelist
                'And h.isactive=1
                'And h.sosource=6
                'And @tdate>=d.fromdate
                'And @tdate<=d.finaldate
                'And d.whouse=@whouse
                'And d.trdr=@trdr
                'And d.district1=(select district1 from trdbranch where trdbranch=@trdbranch And trdr=@trdr)
                'And d.Shipment=@ship
                'And d.mtrl=@item

                '6 Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης
                dsc = If(db.fn_clDiscStep1(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT), 0)

                'Select Case@res=d.disc1val 
                '   From cccPriceListLines d, cccPriceList h
                'Where d.cccpricelist = h.cccpricelist
                'And h.isactive=1
                'And h.sosource=4
                'And @tdate>=d.fromdate
                'And @tdate<=d.finaldate
                'And d.whouse=@whouse
                'And d.trdr=@trdr
                'And d.district1=(select district1 from trdbranch where trdbranch=@trdbranch And trdr=@trdr)
                'And d.Shipment=@ship
                'And d.mtrl=@item
                'And @qty>=d.qty1

                '4 Μη Υδατοδιαλυτά αξίες έκπτωσης

                Dim val = If(db.fn_clDiscStep2(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT, vQty), 0)

                vPrice = val * (-1)
                vPriceVal = itelines.PRICE - vPrice
                If dsc = 0 Then '4 Μη Υδατοδιαλυτά αξίες έκπτωσης (> QTY)
                    vPriceD = vPriceVal
                Else '6 Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης
                    vPriceD = vPriceVal - (vPriceVal * dsc / 100)
                End If
                vPrice = itelines.PRICE - vPriceD

                itelines.NUM02 = vPrice

                'Else
                '    'if(sg.mtrgroup==rset.GroupYD)
                '    'Dim swh As XTable = s1Conn.GetSQLDataSet("select whouse from mtrdoc where findoc=" & itelines.FINDOCS)
                '    Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                '    vWhouse = If(swh, 0)
                '    If vWhouse = 0 Then
                '        vWhouse = itelines.WHOUSE
                '    End If
                '    ''Dim sp As XTable = s1Conn.GetSQLDataSet(((("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") & itelines.MTRL & ",") & drv.TRDR & "," & """") & MTRDOC.QTY1 & """" & ") AS dsc")
                '    'Dim sp As XTable = s1Conn.GetSQLDataSet("select dbo.fn_clDiscStep3(" & "'" & vDate.ToString("yyyyMMdd") & "'" & "," & vWhouse & "," & itelines.MTRL & "," & drv.TRDR & "," & itelines.QTY1.ToString.Replace(",", ".") & ") AS dsc")
                '    '3000336 ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                '    'Πιστωτική πολιτική Υδατοδιαλυτών
                '    dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, drv.TRDR, MTRDOC_QTY1)
                '    itelines.NUM02 = itelines.PRICE * (dsc / 100)
                '    'X.WARNING("DISC STEP3: "+sp.dsc);
                '    If dsc <> 0 Then
                '        vPis = 1
                '    Else
                '        'sp = s1Conn.GetSQLDataSet((("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """") & MTRDOC.QTY1 & """" & ") AS dsc")
                '        'sp = s1Conn.GetSQLDataSet("select dbo.fn_clDiscStep4(" & "'" & vDate.ToString("yyyyMMdd") & "'" & "," & vWhouse & "," & itelines.MTRL & "," & itelines.QTY1.ToString.Replace(",", ".") & ") AS dsc")
                '        'Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                '        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, MTRDOC_QTY1)
                '        itelines.NUM02 = itelines.PRICE * (dsc / 100)
                '        'X.WARNING("DISC STEP4: "+sp.dsc);
                '        If dsc <> 0 Then
                '            vPis = 1
                '        End If
                '    End If
            End If
            '    ITELINES.[NEXT]
            'End While

        Next

        Try
            For Each itelines In vships
                Dim sg = db.MTRLs.Where(Function(f) f.MTRL = itelines.MTRL).FirstOrDefault.MTRGROUP

                '    ITELINES.FIRST
                '    While Not ITELINES.EOF
                'If Not sg = 101 Then 'IsNothing(itelines.NUM02) Then
                'Dim swh As XTable = s1Conn.GetSQLDataSet("select whouse from mtrdoc where findoc=" & itelines.FINDOCS)
                Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                vWhouse = 0
                If Not IsNothing(swh) Then
                    vWhouse = swh.WHOUSE
                End If
                If vWhouse = 0 Then
                    vWhouse = itelines.WHOUSE
                End If
                'sp4 = s1Conn.GetSQLDataSet("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=" & ITELINES.MTRL, null);
                'Dim sp4 As XTable = s1Conn.GetSQLDataSet(("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 2=2 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") & itelines.MTRL & " and d.fromdate <= " & "'" & vDate.ToString("yyyyMMdd") & "'" & " and d.finaldate >= " & "'" & vDate.ToString("yyyyMMdd") & "'")
                'X.WARNING(vQtyY & "-" & vQtyA & "-" & vQtyI & "-" & vWhouse & "-" & sp4.igroup & "-" & ITELINES.MTRL & "-" & vDate);
                db.Log = Console.Out
                '7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                Dim sp4 = (From l In db.cccPriceListLines, h In db.cccPriceLists
                           Where l.cccPriceList = h.cccPriceList And
                                   h.Sosource = 7 And l.Whouse = vWhouse And l.Mtrl = itelines.MTRL And l.Fromdate <= vDate And l.Finaldate >= vDate
                           Select New With {.igroup = l.cccSumGroup}).FirstOrDefault

                'Dim sp As XTable
                If Not IsNothing(sp4) Then
                    'MTRGROUP    cccSumGroup	Name
                    '100 100	Υδατοδιαλυτά
                    '100 103	Α' Υλες
                    '100 200	Καμία
                    '101 103	Α' Υλες
                    '102 102	Ιχνοστοιχεία                   

                    '103	Α' Υλες 
                    '100 ΥΔΑΤΟΔΙΑΛΥΤΑ
                    If sp4.igroup = 100 Then 'Υδατοδιαλυτά
                        'Dim scp As XTable = s1Conn.GetSQLDataSet("select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=" & drv.TRDR)
                        'If Not scp.Count = 0 AndAlso scp(0, "trdr") <> 0 Then
                        Dim scp = (From l In db.cccPriceListLines, h In db.cccPriceLists
                                   Where l.cccPriceList = h.cccPriceList And h.Sosource = 5 And l.Trdr = drv.TRDR
                                   Select New With {.Trdr = l.Trdr}).FirstOrDefault

                        If Not IsNothing(scp) AndAlso scp.Trdr <> 0 Then
                            'sp = s1Conn.GetSQLDataSet((("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") & itelines.MTRL & ",") & drv.TRDR & "," & """" & vQtyY & """" & ") AS dsc")
                            '3000336 ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                            dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, drv.TRDR, vQtyY)
                            'X.WARNING("DISC STEP3 2nd loop group 100: "+sp.dsc+" qty: "+vQtyY);
                            If dsc = 0 Then
                                'X.WARNING("DISC STEP4 2nd loop group 100: "+sp.dsc+" qty: "+vQtyY);
                                'sp = s1Conn.GetSQLDataSet(("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc")
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyY)
                            End If
                        Else

                            'Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                            'X.WARNING("DISC STEP4 2nd loop group 100: "+sp.dsc+" qty: "+vQtyY);
                            'X.WARNING(vQtyY & vWhouse & sp.dsc & vDate & "," & vWhouse & "," & ITELINES.MTRL & "," & vQtyY);
                            'sp = s1Conn.GetSQLDataSet(("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc")
                            'sp = s1Conn.GetSQLDataSet("select dbo.fn_clDiscStep4(" & "'" & vDate.ToString("yyyyMMdd") & "'" & "," & vWhouse & "," & itelines.MTRL & "," & vQtyY.ToString.Replace(",", ".") & ") AS dsc")
                            dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyY)
                        End If
                        itelines.NUM02 = itelines.PRICE * (dsc / 100)
                    ElseIf sp4.igroup = 103 Then 'Α' Υλες
                        'Dim scp As XTable = s1Conn.GetSQLDataSet("select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=" & drv.TRDR)
                        Dim scp = (From l In db.cccPriceListLines, h In db.cccPriceLists
                                   Where l.cccPriceList = h.cccPriceList And h.Sosource = 5 And l.Trdr = drv.TRDR
                                   Select New With {.Trdr = l.Trdr}).FirstOrDefault

                        If Not IsNothing(scp) AndAlso scp.Trdr <> 0 Then
                            'sp = s1Conn.GetSQLDataSet((("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") & itelines.MTRL & ",") & drv.TRDR & "," & """" & vQtyA & """" & ") AS dsc")
                            dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, drv.TRDR, vQtyA)
                            'X.WARNING("DISC STEP3 2nd loop group 103: "+sp.dsc+" qty: "+vQtyA);
                            If dsc = 0 Then
                                'X.WARNING("DISC STEP4 2nd loop group 103: "+sp.dsc+" qty: "+vQtyA);
                                'sp = s1Conn.GetSQLDataSet(("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc")
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                            End If
                        Else

                            'Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                            'X.WARNING("DISC STEP4 2nd loop group 103: "+sp.dsc+" qty: "+vQtyA);
                            'X.WARNING(vQtyY & vWhouse & sp.dsc & vDate & "," & vWhouse & "," & ITELINES.MTRL & "," & vQtyY);
                            'sp = s1Conn.GetSQLDataSet(("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc")
                            dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                        End If
                        itelines.NUM02 = itelines.PRICE * (dsc / 100)
                    ElseIf sp4.igroup = 102 Then 'Ιχνοστοιχεία
                        'sp = s1Conn.GetSQLDataSet(("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """" & vQtyI & """" & ") AS dsc")
                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyI)
                        'X.WARNING("DISC STEP4 2nd loop group 102: "+sp.dsc+" qty: "+vQtyI);

                        itelines.NUM02 = itelines.PRICE * (dsc / 100)
                    ElseIf sp4.igroup = 200 Then
                        'sp = s1Conn.GetSQLDataSet((("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") & itelines.MTRL & "," & """") & itelines.QTY1 & """" & ") AS dsc")
                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, itelines.QTY1)
                        'X.WARNING("DISC STEP4 2nd loop group 200: "+sp.dsc+" qty: "+vQtyI);
                        itelines.NUM02 = itelines.PRICE * (dsc / 100)
                        'sp=s1Conn.GetSQLDataSet("select dbo.fn_clDiscStep4("+vDate+","+vWhouse+","+ITELINES.MTRL+","+"\""+MTRDOC.QTY1+"\""+") AS dsc",null);
                        'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
                    End If
                End If

                'End If
                '        ITELINES.[NEXT]
                '    End While

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return vships.AsQueryable
        'Throw New NotImplementedException()
#End Region
    End Function
    Public Shared Function calcON_AFTERPOSTNew(db As DataClassesHglpDataContext, drv As ccCVShipment) As IQueryable(Of ccCVShipment)
        Dim vs = db.ccCVShipments.AsQueryable.Where(Function(f) f.FINDOC = drv.FINDOC)
        Dim vships As New List(Of ccCVShipment)
        vships.AddRange(vs)

        Dim ans = 0
        Dim vDate As Date
        Dim vQty As Double = 0
        Dim vLineVal As Double = 0
        Dim vList = 0
        Dim vGroupList
        Dim vGrList
        Dim vPrice As Double = 0
        Dim vPriceD As Double = 0
        Dim vPriceVal As Double = 0
        Dim vWhouse = 0
        Dim vPis = 0
        Dim spis = 0
        Dim vQtyY As Double = 0
        Dim vQtyI As Double = 0
        Dim vQtyA As Double = 0
        Dim vQtyNU As Double = 0
        Dim comp = 1000

        '======================= open item ========================
        Dim SALDOC As ccCVShipment = GmGetXRows(s1Conn, "").FirstOrDefault
        'Dim ITELINES As ccCVShipment = GmGetXRows(s1Conn, "").FirstOrDefault
        Dim sql1 = ("select tfprms from fprms where fprms=" + SALDOC.FPRMS & " and company=") + comp

        ' res1 = GmGetXRows(s1Conn, sql1, "")
        Dim res1 = db.FPRMs.Where(Function(f) f.FPRMS = SALDOC.FPRMS And f.COMPANY = comp).FirstOrDefault

        'Τύπου [Συμπεριφορά]
        '151 Πιστωτικό τιμολόγιο επιστροφής
        '152 Πιστωτικό τιμολόγιο
        'If res1.tfprms = 151 OrElse res1.tfprms = 152 Then

        '    Dim cfnObj = 0
        '    Dim z
        '    If SALDOC.FINDOC < 0 Then
        '        z = Nothing ' X.NEWID()
        '    Else
        '        Z = SALDOC.FINDOC
        '    End If

        '    'Sql = "SELECT DISTINCT FINDOCS FROM MTRLINES WHERE FINDOC=" & z
        '    'Res = GmGetXRows(s1Conn, Sql, "")
        '    Dim Res = db.MTRLINEs.Where(Function(f) f.FINDOC = z).FirstOrDefault
        '    'strIDs = X.EVAL("string(" + Res.FINDOCS & ")")
        '    Dim strIDs = Res.FINDOCS

        '    Try
        '        cfnObj = X.CreateObj("SALDOC")
        '        cfnObj.DBLocate(Z)

        '        X.CALLPUBLISHED("ProgLibIntf.ModuleCommand", cfnObj.[MODULE], 1032, strIDs)
        '    Catch generatedExceptionName As e
        '       msgbox(cfnObj.GETLASTERROR)
        '    Finally
        '        cfnObj.FREE
        '        cfnObj = 0
        '    End Try
        'End If

        '======================= open item ========================


        'Σειρές που έχουν επιλεχθεί από τον cccsettings για αυτόματη έκδοση πιστωτικού.
        'RSet = GmGetXRows(s1Conn, "select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=" + comp, Nothing)
        Dim RSet = db.cccSettings.Where(Function(f) f.Company = comp).FirstOrDefault
        'rser = GmGetXRows(s1Conn, "select seriescre from cccsettingslines where seriesinv=" + SALDOC.SERIES, Nothing)
        Dim rser = db.cccSettingsLines.Where(Function(f) f.SeriesInv = SALDOC.SERIES).FirstOrDefault

        Dim iSALDOCID = 0
        If SALDOC.FINDOC < 0 Then
            iSALDOCID = Nothing 'X.NEWID() Επιστρέφει το ID της εγγραφής που καταχωρήθηκε. Χρησιμοποιείται στο after post event.
        Else
            'DeleteSalDoc()
            iSALDOCID = SALDOC.FINDOC
        End If



        'vList = X.EVAL("InList(SALDOC.SERIES, " + RSet.priceseries & ")")
        vList = RSet.PriceSeries.Contains(SALDOC.SERIES)
        If SALDOC.SHIPMENT = "" Then
            MsgBox("ΔΕΝ ΕΧΕΤΕ ΕΠΙΛΕΞΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ...!")
            Return Nothing
        End If
        ''spis = GmGetXRows(s1Conn, ((("Select l.trdr from cccpricelistLines l, cccPriceList p where l.cccpricelist=p.cccpricelist And l.trdr=" + SALDOC.TRDR & " And p.sosource In (4, 6) And l.shipment = ") + SALDOC.SHIPMENT & "
        ''and l.district1=(select district1 from trdbranch where trdr=") + SALDOC.TRDR & " and trdbranch=") + SALDOC.TRDBRANCH & ")", Nothing
        ''4 Μη Υδατοδιαλυτά αξίες έκπτωσης
        ''6 Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης
        'spis = From ps In db.cccPriceListLines, p In db.cccPriceLists
        '       Where ps.cccPriceList = p.cccPriceList And ps.Trdr = SALDOC.TRDR And {4, 6}.Contains(p.Sosource) And ps.Shipment = SALDOC.SHIPMENT And
        '           ps.District1 = (From tr In db.TRDBRANCHes Where tr.TRDR = SALDOC.TRDR And tr.TRDBRANCH = SALDOC.TRDBRANCH Select tr.DISTRICT1).FirstOrDefault Select ps.Trdr

        'if(spis.RECORDCOUNT>0)
        '{
        If vList = 1 Then

            If RSet.District = 1 Then
                'sd = GmGetXRows(s1Conn, "SELECT DISTRICT1 FROM TRDBRANCH WHERE TRDBRANCH=" + SALDOC.TRDBRANCH, Nothing)
                'If sd.DISTRICT1 = "" Then
                '    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΝΟΜΟ ΠΑΡΑΛΗΠΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                '    Return Nothing
                'End If
            End If
            If RSet.Trdbusiness = 1 Then
                'sb = GmGetXRows(s1Conn, "SELECT TRDBUSINESS FROM TRDR WHERE TRDR=" + SALDOC.TRDR, Nothing)
                'If sb.TRDBUSINESS = "" Then
                '    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΕΜΠΟΡΙΚΗ ΚΑΤΗΓΟΡΙΑ ΠΕΛΑΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                '    Return Nothing
                'End If
            End If
            If RSet.Shipment = 1 Then
                If SALDOC.SHIPMENT = "" Then
                    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                    Return Nothing
                End If
            End If

            'vDate = X.EVAL("SQLDate(SALDOC.TRNDATE)")
            vDate = SALDOC.SHIPDATE
            Dim ObjSal As New Object ' = X.CreateObj("SALDOC;Βασική προβολή πωλήσεων")
            Try
                ObjSal.DBInsert

                Dim TblHeader As Object = ObjSal.FindTable("FINDOC")
                Dim TblDetail As Object = ObjSal.FindTable("ITELINES")


                TblHeader.INSERT
                TblHeader.SERIES = rser.SeriesCre
                'rset.series;
                TblHeader.TRDR = SALDOC.TRDR
                TblHeader.TRDBRANCH = SALDOC.TRDBRANCH
                TblHeader.TRNDATE = SALDOC.TRNDATE
                TblHeader.FINDOCS = iSALDOCID
                'TblHeader.COMMENTS = SALDOC.CMPFINCODe + " - " + SALDOC.TRDR_CUSTOMER_NAME

                'ITELINES.FIRST
                'While Not ITELINES.EOF
                Dim MTRDOC_QTY1 = vships.Sum(Function(f) f.QTY1)
                For Each itelines In vships

                    'sg = GmGetXRows(s1Conn, "select mtrgroup from mtrl where mtrl=" + itelines.MTRL, Nothing)
                    Dim sg = db.MTRLs.Where(Function(f) f.MTRL = itelines.MTRL).FirstOrDefault

                    'Gm
                    '101 ΜΗ Υδατοδιαλυτά

                    'MTRGROUP    cccSumGroup	Name
                    '100 100	Υδατοδιαλυτά
                    '100 103	Α' Υλες
                    '100 200	Καμία
                    '101 103	Α' Υλες
                    '102 102	Ιχνοστοιχεία 
                    Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                    vWhouse = 0
                    If Not IsNothing(swh) Then
                        vWhouse = swh.WHOUSE
                    End If
                    If vWhouse = 0 Then
                        vWhouse = itelines.WHOUSE
                    End If

                    '7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                    Dim sp4 = (From l In db.cccPriceListLines, h In db.cccPriceLists
                               Where l.cccPriceList = h.cccPriceList And
                                       h.Sosource = 7 And l.Whouse = vWhouse And l.Mtrl = itelines.MTRL And l.Fromdate <= vDate And l.Finaldate >= vDate
                               Select New With {.igroup = l.cccSumGroup}).FirstOrDefault

                    vGrList = RSet.GroupYD + "," + RSet.GroupMYD
                    'vGroupList = X.EVAL("InList(" + sg.MTRGROUP & "," & vGrList & ")")
                    'vGroupList = X.EVAL("InList(" + sg.MTRGROUP & "," & vGrList & ")")
                    vGroupList = Convert.ToString(RSet.GroupMYD).Contains(sg.MTRGROUP)

                    If vGroupList = 1 Then
                        'Η ομάδα του είδους της γραμμής υπάρχει στις ρυθμίσεις για υδατοδιαλυτά και μη
                        TblDetail.INSERT
                        TblDetail.MTRL = itelines.MTRL
                        TblDetail.QTY1 = itelines.QTY1
                        vQty = itelines.QTY1

                        'vGroupList = X.EVAL(("InList(" + sg.MTRGROUP & ",") + RSet.GroupMYD & ")")
                        'X.WARNING("t1-" + sg.mtrgroup + "-" + vGrList + "-" + vGroupList + "-" + sp4.igroup);
                        '103	Α' Υλες
                        If vGroupList = 1 AndAlso Not sp4.igroup = 103 Then 'ΜΗ Υδατοδιαλυτά + not 103	Α' Υλες

                            'spmyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=4)', null);
                            Dim spmyd_qty = (From l In db.MTRLINEs
                                             Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                             Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 4
                                                                             Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)

                            'sp = GmGetXRows(s1Conn, ((((("select isnull(dbo.fn_clDiscStep1(" & vDate & "," & vWhouse & ",") + SALDOC.TRDBRANCH & ",") + itelines.MTRL & ",") + SALDOC.TRDR & ",") + SALDOC.TRDR_CUSTOMER_TRDBUSINESS & ",") + SALDOC.SHIPMENT & "),0) AS dsc", Nothing)
                            'spp = GmGetXRows(s1Conn, (((((("select isnull(dbo.fn_clDiscStep2(" & vDate & "," & vWhouse & ",") + SALDOC.TRDBRANCH & ",") + itelines.MTRL & ",") + SALDOC.TRDR & ",") + SALDOC.TRDR_CUSTOMER_TRDBUSINESS & ",") + SALDOC.SHIPMENT & "," & """") + MTRDOC.QTY1 & """" & "),0) AS val", Nothing)

                            Dim dsc = If(db.fn_clDiscStep1(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT), 0)
                            Dim val = If(db.fn_clDiscStep2(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT, spmyd_qty), 0)

                            vPrice = val * (-1)
                            vPriceVal = itelines.PRICE - vPrice
                            If dsc = 0 Then
                                vPriceD = vPriceVal
                            Else
                                vPriceD = vPriceVal - (vPriceVal * (dsc / 100))
                            End If
                            vPrice = itelines.PRICE - vPriceD

                            TblDetail.PRICE = vPrice
                            If vPrice <> 0 Then
                                vPis = 1
                            End If
                            If vPrice > 0 Then
                                vPis = 1
                            End If


                            '    vPrice = spp.val * (-1);
                            'vPriceVal = itelines.PRICE - vPrice;
                            'If (sp.dsc == 0 || sp.dsc == '') {
                            '    vPriceD = vPriceVal;
                            '} else {
                            '    vPriceD = vPriceVal - (vPriceVal * (sp.dsc / 100));
                            '}
                            'vPrice = itelines.PRICE - vPriceD;

                            'TblDetail.PRICE = vPrice;

                        Else
                            'Υδατοδιαλυτά και Ιχνοστοιχεία και Α΄Υλες

                            'scp = GmGetXRows(s1Conn, "select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=" + SALDOC.TRDR, Nothing)
                            'Sosource = 5 Πιστωτική πολιτική Υδατοδιαλυτών ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                            Dim scp = (From l In db.cccPriceListLines, h In db.cccPriceLists
                                       Where l.cccPriceList = h.cccPriceList And h.Sosource = 5 And l.Trdr = drv.TRDR
                                       Select New With {.Trdr = l.Trdr}).FirstOrDefault


                            Dim spydQ1 As Double = 0
                            Dim spayQ1 As Double = 0
                            Dim spixQ1 As Double = 0
                            Dim spnuQ1 As Double = 0

                            If scp.Trdr = 0 Then 'Όχι ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                                'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                'spyd = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=5)", Nothing)
                                'Sosource = 5 Πιστωτική πολιτική Υδατοδιαλυτών
                                spydQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 5
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                                'Gm Α΄Υλες


                                'spay = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
                                '100 Υδατοδιαλυτά	1
                                '102 Ιχνοστοιχεία	1
                                '103 Α' Υλες	1
                                '200 Καμία	1
                                'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                                spayQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 103
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                            Else 'ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                                'spyd = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=100)", Nothing)
                                '100 Υδατοδιαλυτά	1
                                'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                                spydQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 100
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)

                                'Gm Α΄Υλες
                                'spay = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
                                spayQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 103
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                            End If

                            'spix = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                            '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)", Nothing)

                            '102 Ιχνοστοιχεία	1
                            'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                            spixQ1 = (From l In db.MTRLINEs
                                      Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                      Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 102
                                                                      Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)

                            'spnu = GmGetXRows(s1Conn, ("select l.qty1 as qty from mtrlines l, cccpricelistlines m, cccpricelist h 
                            'where m.cccpricelist=h.cccpricelist and m.mtrl=l.mtrl and m.cccsumgroup=200 and l.findoc=" & iSALDOCID & " and m.mtrl=") + itelines.MTRL, Nothing)
                            spnuQ1 = (From l In db.MTRLINEs, m In db.cccPriceListLines, h In db.cccPriceLists
                                      Where m.cccPriceList = h.cccPriceList And m.Mtrl = l.MTRL And m.cccSumGroup = 200 And l.FINDOC = iSALDOCID And m.Mtrl = itelines.MTRL).FirstOrDefault.l.QTY1

                            vQtyY = spydQ1
                            vQtyI = spixQ1
                            vQtyA = spayQ1
                            vQtyNU = spnuQ1
                            'swh = GmGetXRows(s1Conn, "select whouse from mtrdoc where findoc=" + itelines.FINDOCS, Nothing)
                            swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                            vWhouse = swh.WHOUSE
                            If vWhouse = 0 Then
                                vWhouse = itelines.WHOUSE
                            End If
                            'sp4 =GmGetXRows(s1Conn,"select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=" + ITELINES.MTRL, null);
                            'sp4 = GmGetXRows(s1Conn, ("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h 
                            'where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") + itelines.MTRL & " and d.fromdate <= " & vDate & " and d.finaldate >= " & vDate, Nothing)
                            'sp4 = (From d In db.cccPriceListLines, h In db.cccPriceLists
                            '       Where d.cccPriceList = h.cccPriceList And
                            '           h.Sosource = 7 And d.Whouse = vWhouse And d.Mtrl = itelines.MTRL And d.Fromdate <= vDate And d.Finaldate >= vDate).FirstOrDefault.d

                            'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate);
                            Dim dsc As Double = 0
                            If sp4.igroup = 100 Then
                                If scp.Trdr <> 0 Then
                                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                    'sp = GmGetXRows(s1Conn, (("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") + itelines.MTRL & ",") + SALDOC.TRDR & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, SALDOC.TRDR, vQtyY)
                                    If dsc = 0 Then
                                        'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyY)
                                    End If
                                Else
                                    'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyY)
                                End If
                                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.igroup = 103 Then
                                If scp.Trdr <> 0 Then
                                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                    'sp = GmGetXRows(s1Conn, (("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") + itelines.MTRL & ",") + SALDOC.TRDR & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, SALDOC.TRDR, vQtyA)
                                    MsgBox(dsc)
                                    If dsc = 0 Then
                                        'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                                    End If
                                Else
                                    'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                                End If
                                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate + " dsc: " + sp.dsc + " price: " + (ITELINES.PRICE * (sp.dsc / 100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.igroup = 102 Then
                                'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyI & """" & ") AS dsc", Nothing)
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyI)
                                'X.WARNING("sum qty 102: "+vQtyI+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));

                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.igroup = 200 Then
                                'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyNU & """" & ") AS dsc", Nothing)
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyNU)
                                'X.WARNING("sum qty 200: "+ITELINES.QTY1+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            End If
                            'sp=X.GETSQLDATASET("select dbo.fn_clDiscStep4("+vDate+","+vWhouse+","+ITELINES.MTRL+","+"\""+MTRDOC.QTY1+"\""+") AS dsc",null);
                            'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
                            If dsc <> 0 Then
                                vPis = 1
                            End If
                        End If
                        TblDetail.FINDOCS = iSALDOCID
                        TblDetail.POST
                    End If
                    '    ITELINES.[NEXT]
                    'End While
                Next


                'If vPis = 1 Then
                '    ans = X.ASK("Πιστωτικό τιμολόγιο", "Θέλετε να γίνει αυτόματη δημιουργία πιστωτικού τιμολογίου?")
                '    If ans = 6 Then
                '        ObjSal.DBPost
                '    End If
                'End If
            Catch e As Exception ' generatedExceptionName As e
                MsgBox(e)
            Finally
                ObjSal.FREE
                ObjSal = Nothing

            End Try
        End If
        '}
        Return vships
    End Function

    Public Shared Function calcON_AFTERPOST(db As DataClassesHglpDataContext, drv As ccCVShipment) As IQueryable(Of ccCVShipment)
        Dim vs = db.ccCVShipments.AsQueryable.Where(Function(f) f.FINDOC = drv.FINDOC)
        Dim vships As New List(Of ccCVShipment)
        vships.AddRange(vs)

        Dim ans = 0
        Dim vDate As Date
        Dim vQty As Double = 0
        Dim vLineVal As Double = 0
        Dim vList = 0
        Dim vGroupList
        Dim vGrList
        Dim vPrice As Double = 0
        Dim vPriceD As Double = 0
        Dim vPriceVal As Double = 0
        Dim vWhouse = 0
        Dim vPis = 0
        Dim spis = 0
        Dim vQtyY As Double = 0
        Dim vQtyI As Double = 0
        Dim vQtyA As Double = 0
        Dim vQtyNU As Double = 0
        Dim comp = 1000

        '======================= open item ========================
        Dim SALDOC As ccCVShipment = GmGetXRows(s1Conn, "").FirstOrDefault
        'Dim ITELINES As ccCVShipment = GmGetXRows(s1Conn, "").FirstOrDefault
        Dim sql1 = ("select tfprms from fprms where fprms=" + SALDOC.FPRMS & " and company=") + comp

        ' res1 = GmGetXRows(s1Conn, sql1, "")
        Dim res1 = db.FPRMs.Where(Function(f) f.FPRMS = SALDOC.FPRMS And f.COMPANY = comp).FirstOrDefault

        'Τύπου [Συμπεριφορά]
        '151 Πιστωτικό τιμολόγιο επιστροφής
        '152 Πιστωτικό τιμολόγιο
        'If res1.tfprms = 151 OrElse res1.tfprms = 152 Then

        '    Dim cfnObj = 0
        '    Dim z
        '    If SALDOC.FINDOC < 0 Then
        '        z = Nothing ' X.NEWID()
        '    Else
        '        Z = SALDOC.FINDOC
        '    End If

        '    'Sql = "SELECT DISTINCT FINDOCS FROM MTRLINES WHERE FINDOC=" & z
        '    'Res = GmGetXRows(s1Conn, Sql, "")
        '    Dim Res = db.MTRLINEs.Where(Function(f) f.FINDOC = z).FirstOrDefault
        '    'strIDs = X.EVAL("string(" + Res.FINDOCS & ")")
        '    Dim strIDs = Res.FINDOCS

        '    Try
        '        cfnObj = X.CreateObj("SALDOC")
        '        cfnObj.DBLocate(Z)

        '        X.CALLPUBLISHED("ProgLibIntf.ModuleCommand", cfnObj.[MODULE], 1032, strIDs)
        '    Catch generatedExceptionName As e
        '       msgbox(cfnObj.GETLASTERROR)
        '    Finally
        '        cfnObj.FREE
        '        cfnObj = 0
        '    End Try
        'End If

        '======================= open item ========================


        'Σειρές που έχουν επιλεχθεί από τον cccsettings για αυτόματη έκδοση πιστωτικού.
        'RSet = GmGetXRows(s1Conn, "select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=" + comp, Nothing)
        Dim RSet = db.cccSettings.Where(Function(f) f.Company = comp).FirstOrDefault
        'rser = GmGetXRows(s1Conn, "select seriescre from cccsettingslines where seriesinv=" + SALDOC.SERIES, Nothing)
        Dim rser = db.cccSettingsLines.Where(Function(f) f.SeriesInv = SALDOC.SERIES).FirstOrDefault

        Dim iSALDOCID = 0
        If SALDOC.FINDOC < 0 Then
            iSALDOCID = Nothing 'X.NEWID() Επιστρέφει το ID της εγγραφής που καταχωρήθηκε. Χρησιμοποιείται στο after post event.
        Else
            'DeleteSalDoc()
            iSALDOCID = SALDOC.FINDOC
        End If



        'vList = X.EVAL("InList(SALDOC.SERIES, " + RSet.priceseries & ")")
        vList = RSet.PriceSeries.Contains(SALDOC.SERIES)
        If SALDOC.SHIPMENT = "" Then
            MsgBox("ΔΕΝ ΕΧΕΤΕ ΕΠΙΛΕΞΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ...!")
            Return Nothing
        End If
        ''spis = GmGetXRows(s1Conn, ((("Select l.trdr from cccpricelistLines l, cccPriceList p where l.cccpricelist=p.cccpricelist And l.trdr=" + SALDOC.TRDR & " And p.sosource In (4, 6) And l.shipment = ") + SALDOC.SHIPMENT & "
        ''and l.district1=(select district1 from trdbranch where trdr=") + SALDOC.TRDR & " and trdbranch=") + SALDOC.TRDBRANCH & ")", Nothing
        ''4 Μη Υδατοδιαλυτά αξίες έκπτωσης
        ''6 Πιστωτική πολιτική ΜΗ Υδατοδιαλυτών με ποσοστά έκπτωσης
        'spis = From ps In db.cccPriceListLines, p In db.cccPriceLists
        '       Where ps.cccPriceList = p.cccPriceList And ps.Trdr = SALDOC.TRDR And {4, 6}.Contains(p.Sosource) And ps.Shipment = SALDOC.SHIPMENT And
        '           ps.District1 = (From tr In db.TRDBRANCHes Where tr.TRDR = SALDOC.TRDR And tr.TRDBRANCH = SALDOC.TRDBRANCH Select tr.DISTRICT1).FirstOrDefault Select ps.Trdr

        'if(spis.RECORDCOUNT>0)
        '{
        If vList = 1 Then

            If RSet.District = 1 Then
                'sd = GmGetXRows(s1Conn, "SELECT DISTRICT1 FROM TRDBRANCH WHERE TRDBRANCH=" + SALDOC.TRDBRANCH, Nothing)
                'If sd.DISTRICT1 = "" Then
                '    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΝΟΜΟ ΠΑΡΑΛΗΠΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                '    Return Nothing
                'End If
            End If
            If RSet.Trdbusiness = 1 Then
                'sb = GmGetXRows(s1Conn, "SELECT TRDBUSINESS FROM TRDR WHERE TRDR=" + SALDOC.TRDR, Nothing)
                'If sb.TRDBUSINESS = "" Then
                '    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΕΜΠΟΡΙΚΗ ΚΑΤΗΓΟΡΙΑ ΠΕΛΑΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                '    Return Nothing
                'End If
            End If
            If RSet.Shipment = 1 Then
                If SALDOC.SHIPMENT = "" Then
                    MsgBox("ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!")
                    Return Nothing
                End If
            End If

            'vDate = X.EVAL("SQLDate(SALDOC.TRNDATE)")
            vDate = SALDOC.SHIPDATE
            Dim ObjSal As New Object ' = X.CreateObj("SALDOC;Βασική προβολή πωλήσεων")
            Try
                ObjSal.DBInsert

                Dim TblHeader As Object = ObjSal.FindTable("FINDOC")
                Dim TblDetail As Object = ObjSal.FindTable("ITELINES")


                TblHeader.INSERT
                TblHeader.SERIES = rser.SeriesCre
                'rset.series;
                TblHeader.TRDR = SALDOC.TRDR
                TblHeader.TRDBRANCH = SALDOC.TRDBRANCH
                TblHeader.TRNDATE = SALDOC.TRNDATE
                TblHeader.FINDOCS = iSALDOCID
                'TblHeader.COMMENTS = SALDOC.CMPFINCODe + " - " + SALDOC.TRDR_CUSTOMER_NAME

                'ITELINES.FIRST
                'While Not ITELINES.EOF
                Dim MTRDOC_QTY1 = vships.Sum(Function(f) f.QTY1)
                For Each itelines In vships

                    'sg = GmGetXRows(s1Conn, "select mtrgroup from mtrl where mtrl=" + itelines.MTRL, Nothing)
                    Dim sg = db.MTRLs.Where(Function(f) f.MTRL = itelines.MTRL).FirstOrDefault

                    'Gm
                    'swh = GmGetXRows(s1Conn, "select whouse from mtrdoc where findoc=" + itelines.FINDOCS, Nothing)
                    Dim swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                    vWhouse = swh.WHOUSE
                    If vWhouse = 0 Then
                        vWhouse = itelines.WHOUSE ' MTRDOC.WHOUSE
                    End If
                    'sp4 = GmGetXRows(s1Conn, ("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") + ITELINES.MTRL & " and d.fromdate <= " & vDate & " and d.finaldate >= " & vDate, Nothing)
                    '7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                    Dim sp4 = (From d In db.cccPriceListLines, h In db.cccPriceLists
                               Where d.cccPriceList = h.cccPriceList And
                                  h.Sosource = 7 And d.Whouse = vWhouse And d.Mtrl = itelines.MTRL And d.Fromdate <= vDate And d.Finaldate >= vDate).FirstOrDefault.d


                    vGrList = RSet.GroupYD + "," + RSet.GroupMYD
                    'vGroupList = X.EVAL("InList(" + sg.MTRGROUP & "," & vGrList & ")")
                    'vGroupList = X.EVAL("InList(" + sg.MTRGROUP & "," & vGrList & ")")
                    vGroupList = Convert.ToString(RSet.GroupMYD).Contains(sg.MTRGROUP)

                    If vGroupList = 1 Then
                        'Η ομάδα του είδους της γραμμής υπάρχει στις ρυθμίσεις για υδατοδιαλυτά και μη
                        TblDetail.INSERT
                        TblDetail.MTRL = itelines.MTRL
                        TblDetail.QTY1 = itelines.QTY1
                        vQty = itelines.QTY1

                        'vGroupList = X.EVAL(("InList(" + sg.MTRGROUP & ",") + RSet.GroupMYD & ")")
                        'X.WARNING("t1-" + sg.mtrgroup + "-" + vGrList + "-" + vGroupList + "-" + sp4.igroup);
                        '103	Α' Υλες
                        If vGroupList = 1 AndAlso Not sp4.cccSumGroup = 103 Then 'ΜΗ Υδατοδιαλυτά

                            'sp = GmGetXRows(s1Conn, ((((("select isnull(dbo.fn_clDiscStep1(" & vDate & "," & vWhouse & ",") + SALDOC.TRDBRANCH & ",") + itelines.MTRL & ",") + SALDOC.TRDR & ",") + SALDOC.TRDR_CUSTOMER_TRDBUSINESS & ",") + SALDOC.SHIPMENT & "),0) AS dsc", Nothing)
                            'spp = GmGetXRows(s1Conn, (((((("select isnull(dbo.fn_clDiscStep2(" & vDate & "," & vWhouse & ",") + SALDOC.TRDBRANCH & ",") + itelines.MTRL & ",") + SALDOC.TRDR & ",") + SALDOC.TRDR_CUSTOMER_TRDBUSINESS & ",") + SALDOC.SHIPMENT & "," & """") + MTRDOC.QTY1 & """" & "),0) AS val", Nothing)

                            Dim dsc = If(db.fn_clDiscStep1(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT), 0)
                            Dim val = If(db.fn_clDiscStep2(vDate, vWhouse, drv.TRDBRANCH, itelines.MTRL, drv.TRDR, drv.TRDBUSINESS, drv.SHIPMENT, MTRDOC_QTY1), 0)

                            vPrice = val * (-1)
                            vPriceVal = itelines.PRICE - vPrice
                            If dsc = 0 Then
                                vPriceD = vPriceVal
                            Else
                                vPriceD = vPriceVal - (vPriceVal * (dsc / 100))
                            End If
                            vPrice = itelines.PRICE - vPriceD

                            TblDetail.PRICE = vPrice
                            If vPrice <> 0 Then
                                vPis = 1
                            End If
                            If vPrice > 0 Then
                                vPis = 1
                            End If


                            '    vPrice = spp.val * (-1);
                            'vPriceVal = itelines.PRICE - vPrice;
                            'If (sp.dsc == 0 || sp.dsc == '') {
                            '    vPriceD = vPriceVal;
                            '} else {
                            '    vPriceD = vPriceVal - (vPriceVal * (sp.dsc / 100));
                            '}
                            'vPrice = itelines.PRICE - vPriceD;

                            'TblDetail.PRICE = vPrice;

                        Else
                            'Υδατοδιαλυτά και Ιχνοστοιχεία και Α΄Υλες

                            'scp = GmGetXRows(s1Conn, "select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=" + SALDOC.TRDR, Nothing)
                            'Sosource = 5 Πιστωτική πολιτική Υδατοδιαλυτών ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                            Dim scp = (From l In db.cccPriceListLines, h In db.cccPriceLists
                                       Where l.cccPriceList = h.cccPriceList And
                                          h.Sosource = 5 And l.Trdr = SALDOC.TRDR).FirstOrDefault.l

                            Dim spydQ1 As Double = 0
                            Dim spaydQ1 As Double = 0
                            Dim spixQ1 As Double = 0
                            Dim spnuQ1 As Double = 0

                            If scp.Trdr = 0 Then 'Όχι ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                                'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                'spyd = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=5)", Nothing)
                                'Sosource = 5 Πιστωτική πολιτική Υδατοδιαλυτών
                                spydQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 5
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                                'Gm Α΄Υλες


                                'spay = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
                                '100 Υδατοδιαλυτά	1
                                '102 Ιχνοστοιχεία	1
                                '103 Α' Υλες	1
                                '200 Καμία	1
                                'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                                spaydQ1 = (From l In db.MTRLINEs
                                           Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                           Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 103
                                                                           Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                            Else 'ΑΓΡΟΤΕΧΝΙΚΗ ΒΑΒΟΥΡΑΚΗΣ ΑΕ
                                'spyd = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=100)", Nothing)
                                '100 Υδατοδιαλυτά	1
                                'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                                spydQ1 = (From l In db.MTRLINEs
                                          Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                          Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 100
                                                                          Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)

                                'Gm Α΄Υλες
                                'spay = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                                '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
                                spaydQ1 = (From l In db.MTRLINEs
                                           Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                           Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 103
                                                                           Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)
                            End If

                            'spix = GmGetXRows(s1Conn, "select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in 
                            '(select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)", Nothing)

                            '102 Ιχνοστοιχεία	1
                            'Sosource = 7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                            spixQ1 = (From l In db.MTRLINEs
                                      Where l.FINDOC = iSALDOCID And (From ll In db.cccPriceListLines, hh In db.cccPriceLists
                                                                      Where ll.cccPriceList = hh.cccPriceList And hh.Sosource = 7 And ll.cccSumGroup = 102
                                                                      Select ll.Mtrl).Contains(l.MTRL)).Sum(Function(f) f.QTY1)

                            'spnu = GmGetXRows(s1Conn, ("select l.qty1 as qty from mtrlines l, cccpricelistlines m, cccpricelist h 
                            'where m.cccpricelist=h.cccpricelist and m.mtrl=l.mtrl and m.cccsumgroup=200 and l.findoc=" & iSALDOCID & " and m.mtrl=") + itelines.MTRL, Nothing)
                            spnuQ1 = (From l In db.MTRLINEs, m In db.cccPriceListLines, h In db.cccPriceLists
                                      Where m.cccPriceList = h.cccPriceList And m.Mtrl = l.MTRL And m.cccSumGroup = 200 And l.FINDOC = iSALDOCID And m.Mtrl = itelines.MTRL).FirstOrDefault.l.QTY1

                            vQtyY = spydQ1
                            vQtyI = spixQ1
                            vQtyA = spaydQ1
                            vQtyNU = spnuQ1
                            'swh = GmGetXRows(s1Conn, "select whouse from mtrdoc where findoc=" + itelines.FINDOCS, Nothing)
                            swh = db.MTRDOCs.Where(Function(f) f.FINDOC = itelines.FINDOCS).FirstOrDefault
                            vWhouse = swh.WHOUSE
                            If vWhouse = 0 Then
                                vWhouse = itelines.WHOUSE
                            End If
                            'sp4 =GmGetXRows(s1Conn,"select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=" + ITELINES.MTRL, null);
                            'sp4 = GmGetXRows(s1Conn, ("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h 
                            'where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") + itelines.MTRL & " and d.fromdate <= " & vDate & " and d.finaldate >= " & vDate, Nothing)
                            sp4 = (From d In db.cccPriceListLines, h In db.cccPriceLists
                                   Where d.cccPriceList = h.cccPriceList And
                                       h.Sosource = 7 And d.Whouse = vWhouse And d.Mtrl = itelines.MTRL And d.Fromdate <= vDate And d.Finaldate >= vDate).FirstOrDefault.d

                            'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate);
                            Dim dsc As Double = 0
                            If sp4.cccSumGroup = 100 Then
                                If scp.Trdr <> 0 Then
                                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                    'sp = GmGetXRows(s1Conn, (("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") + itelines.MTRL & ",") + SALDOC.TRDR & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, SALDOC.TRDR, itelines.QTY1)
                                    If dsc = 0 Then
                                        'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, itelines.QTY1)
                                    End If
                                Else
                                    'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, itelines.QTY1)
                                End If
                                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.cccSumGroup = 103 Then
                                If scp.Trdr <> 0 Then
                                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                                    'sp = GmGetXRows(s1Conn, (("select dbo.fn_clDiscStep3(" & vDate & "," & vWhouse & ",") + itelines.MTRL & ",") + SALDOC.TRDR & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep3(vDate, vWhouse, itelines.MTRL, SALDOC.TRDR, vQtyA)
                                    MsgBox(dsc)
                                    If dsc = 0 Then
                                        'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                        dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                                    End If
                                Else
                                    'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
                                    dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyA)
                                End If
                                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate + " dsc: " + sp.dsc + " price: " + (ITELINES.PRICE * (sp.dsc / 100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.cccSumGroup = 102 Then
                                'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyI & """" & ") AS dsc", Nothing)
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyI)
                                'X.WARNING("sum qty 102: "+vQtyI+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));

                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            ElseIf sp4.cccSumGroup = 200 Then
                                'sp = GmGetXRows(s1Conn, ("select dbo.fn_clDiscStep4(" & vDate & "," & vWhouse & ",") + itelines.MTRL & "," & """" & vQtyNU & """" & ") AS dsc", Nothing)
                                dsc = db.fn_clDiscStep4(vDate, vWhouse, itelines.MTRL, vQtyNU)
                                'X.WARNING("sum qty 200: "+ITELINES.QTY1+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
                                TblDetail.PRICE = itelines.PRICE * (dsc / 100)
                            End If
                            'sp=X.GETSQLDATASET("select dbo.fn_clDiscStep4("+vDate+","+vWhouse+","+ITELINES.MTRL+","+"\""+MTRDOC.QTY1+"\""+") AS dsc",null);
                            'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
                            If dsc <> 0 Then
                                vPis = 1
                            End If
                        End If
                        TblDetail.FINDOCS = iSALDOCID
                        TblDetail.POST
                    End If
                    '    ITELINES.[NEXT]
                    'End While
                Next


                'If vPis = 1 Then
                '    ans = X.ASK("Πιστωτικό τιμολόγιο", "Θέλετε να γίνει αυτόματη δημιουργία πιστωτικού τιμολογίου?")
                '    If ans = 6 Then
                '        ObjSal.DBPost
                '    End If
                'End If
            Catch e As Exception ' generatedExceptionName As e
                MsgBox(e)
            Finally
                ObjSal.FREE
                ObjSal = Nothing

            End Try
        End If
        '}
        Return vships
    End Function

    Private Shared Function GmGetXRows(s1Conn As XSupport, sql As String, Optional p As Object = Nothing) As List(Of ccCVShipment)
        Throw New NotImplementedException()
    End Function


End Class
