Imports GmSupp.Hglp
Imports Softone

Public Class SoCarrierFM
    Public Property conn As String
    Public Property FrmCancel As Boolean
    Public Property SenderName As String
    Public Property ccCVTrdBRoutings As List(Of ccCVTrdBRouting)
    Public Property ccCVShipment As ccCVShipment
    'Dim db As DataClassesHglpDataContext

    Private Sub SoCarrierFM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolStripConvert.Visible = False
        Me.NAMEComboBox.Enabled = False
        Me.txtBoxTRUCKSNO.Enabled = False
        Me.txtBoxccCSHIPVALUE.Enabled = False
        Me.chkBoxccCADR.Enabled = False
        Me.chkBoxccCLocked.Enabled = False

        If SenderName = "ToolStripMenuItemSOCARRIER" Then
            Me.NAMEComboBox.Enabled = True
            Me.txtBoxTRUCKSNO.Enabled = True
            Me.txtBoxccCSHIPVALUE.Enabled = True
            Me.chkBoxccCADR.Enabled = True
            Me.chkBoxccCLocked.Enabled = True

            'db = New DataClassesHglpDataContext(conn)

            ''Dim q = (From so In db.SOCARRIERs Select so.NAME, so.SOCARRIER).ToList
            'Dim q = db.ccCVTrdBRoutings.ToList
            'q = q.Where(Function(f) f.FINDOC = FindocID).ToList
            ''q = q.Where(Function(f) f.ISACTIVE > 0).ToList

            'q = q.OrderBy(Function(f) f.SOCOST).ToList
            Dim emptySOCARRIER As ccCVTrdBRouting()
            emptySOCARRIER = {New ccCVTrdBRouting With {.SOCARRIERNAME = "<Επιλέγξτε>", .SOCARRIER = 0}}

            Dim q1 = emptySOCARRIER.ToList.Union(ccCVTrdBRoutings.ToList)

            emptySOCARRIER = {New ccCVTrdBRouting With {.SOCARRIERNAME = "ΜΕΤΑΦΟΡΙΚΑ ΕΛΛΑΓΡΟΛΙΠ", .SOCARRIER = 8888}}

            q1 = q1.ToList.Union(emptySOCARRIER.ToList)

            emptySOCARRIER = {New ccCVTrdBRouting With {.SOCARRIERNAME = "ΜΕΤΑΦΟΡΙΚΑ ΠΕΛΑΤΗ", .SOCARRIER = 9999}}

            q1 = q1.ToList.Union(emptySOCARRIER.ToList)

            Me.NAMEComboBox.DataSource = q1.ToList

            'frm.ccCSOCARRIER = current.ccCSOCARRIER

            Me.NAMEComboBox.DisplayMember = "SOCARRIERNAME"
            Me.NAMEComboBox.ValueMember = "SOCARRIER"
            Me.NAMEComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.NAMEComboBox.AutoCompleteSource = AutoCompleteSource.ListItems
        End If

        If SenderName = "ToolStripMenuItemTRUCKSNO" Then
            Me.txtBoxTRUCKSNO.Enabled = True
        End If

        If SenderName = "ToolStripMenuItemShipingValue" Or SenderName = "ToolStripMenuItemShipingAllValue" Then
            Me.txtBoxccCSHIPVALUE.Enabled = True
            Me.chkBoxccCLocked.Enabled = True
        End If

        If SenderName = "ToolStripMenuItemADR" Then
            Me.chkBoxccCADR.Enabled = True
        End If
        If SenderName = "ToolStripMenuItemConvert" Then
            Me.ToolStripConvert.Visible = True
        End If

        If Not IsNothing(ccCVShipment) Then
            Me.NAMEComboBox.SelectedItem = Me.NAMEComboBox.Items.Cast(Of ccCVTrdBRouting).Where(Function(f) f.SOCARRIER = If(ccCVShipment.ccCSOCARRIER, 0)).FirstOrDefault
            Me.chkBoxccCADR.Checked = If(ccCVShipment.MTRLINESCCCADR, False)
            Me.chkBoxccCLocked.Checked = If(ccCVShipment.ccCLocked, False)
            Me.txtBoxccCSHIPVALUE.Text = If(ccCVShipment.MTRLINESCCCSHIPVALUE, 0)
            Me.txtBoxTRUCKSNO.Text = ccCVShipment.ccCTRUCKSNO
        End If




        Me.NAMEComboBox.Visible = Me.NAMEComboBox.Enabled
        Me.lblNAME.Visible = Me.NAMEComboBox.Enabled
        Me.txtBoxTRUCKSNO.Visible = Me.txtBoxTRUCKSNO.Enabled
        Me.lblTRUCKSNO.Visible = Me.txtBoxTRUCKSNO.Enabled
        Me.txtBoxccCSHIPVALUE.Visible = Me.txtBoxccCSHIPVALUE.Enabled
        Me.lblShipmentValue.Visible = Me.txtBoxccCSHIPVALUE.Enabled
        Me.chkBoxccCADR.Visible = Me.chkBoxccCADR.Enabled
        'Me.chkBoxccCLocked.Visible = Me.chkBoxccCLocked.Enabled
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        FrmCancel = True
        Me.Close()
    End Sub

    Private Sub SoCarrierFM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FrmCancel Then
            Exit Sub
        End If
        If SenderName = "ToolStripMenuItemSOCARRIER" Then
            If Me.NAMEComboBox.SelectedValue = 0 Then
                If MsgBox("Προσοχή !!! Δεν επιλέξατε μεταφορέα.", MsgBoxStyle.OkCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") = MsgBoxResult.Cancel Then
                    e.Cancel = True
                End If
            End If
        End If
        If SenderName = "ToolStripMenuItemConvert" Then
            Dim gg As Boolean = GoConvert(ccCVTrdBRoutings)
        End If
    End Sub

    Private Sub NAMEComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles NAMEComboBox.SelectedIndexChanged
        Dim s As ComboBox = sender
        Dim rt As ccCVTrdBRouting = s.SelectedItem
        If Not IsNothing(rt) Then
            'rt = db.ccCRoutings.Where(Function(f) f.ccCRouting = 1).FirstOrDefault
            Me.txtBoxccCSHIPVALUE.Text = rt.SOCOST
        End If

    End Sub
    Private Function GoConvert(findocs As List(Of ccCVTrdBRouting)) As Boolean


        Dim ans = 0
        Dim vDate
        Dim vQty = 0
        Dim vLineVal = 0
        Dim vList0
        Dim vGroupList
        Dim vGrList
        Dim vPrice = 0
        Dim vPriceD = 0
        Dim vPriceVal = 0
        Dim vWhouse = 0
        Dim vPis = 0
        Dim spis
        Dim vQtyY = 0
        Dim vQtyI = 0
        Dim vQtyA = 0
        Dim vQtyNU = 0

        Me.Cursor = Cursors.WaitCursor
        Dim ObjSal As XModule = Nothing

        Dim SalDoc As XModule = Nothing


        'vDate = x.EVAL("SQLDate(SALDOC.TRNDATE)")
        ''Dim ObjSal = x.CreateObj("SALDOC;Βασική προβολή πωλήσεων")
        Try
            Dim fins = findocs.Select(Function(f) f.FINDOC).Distinct
            For Each findocID In fins

                ObjSal = s1Conn.CreateModule("SALDOC;Βασική προβολή πωλήσεων")
                SalDoc = s1Conn.CreateModule("SALDOC;Βασική προβολή πωλήσεων")

                SalDoc.LocateData(findocID)
                'x.InsertData()
                'myTable = "TRDR")
                'ObjSal.InsertData()

                Dim TblHeader As XTable = SalDoc.GetTable("FINDOC")
                Dim TblMTRDOC As XTable = SalDoc.GetTable("MTRDOC")
                Dim TblDetail As XTable = SalDoc.GetTable("ITELINES")
                Dim find = "COMPANY,LOCKID,SOSOURCE,SOREDIR,TRNDATE,FISCPRD,PERIOD,SERIES,SERIESNUM,FPRMS,TFPRMS,FINCODE,BRANCH,SODTYPE,TRDR,TRDBRANCH,VATSTS,SOCURRENCY,TRDRRATE,LRATE,ORIGIN,GLUPD,SXUPD,PRDCOST,COMMENTS,ISCANCEL,ISPRINT,ISREADONLY,APPRVDATE,APPRVUSER,APPRV,CONVMODE,FULLYTRANSF,SHIPMENT,PAYMENT,PRCPOLICY,CRCONTROL,SALESMAN,SOCASH,LTYPE1,LTYPE2,LTYPE3,LTYPE4,SOTIME,TURNOVR,TTURNOVR,LTURNOVR,VATAMNT,TVATAMNT,LVATAMNT,EXPN,TEXPN,LEXPN,DISC1PRC,DISC1VAL,TDISC1VAL,LDISC1VAL,DISC2PRC,DISC2VAL,TDISC2VAL,LDISC2VAL,NETAMNT,TNETAMNT,LNETAMNT,SUMAMNT,SUMTAMNT,SUMLAMNT,FXDIFFVAL,KEPYOMD,KEPYOHANDMD,KEPYOQT,LKEPYOVAL,GSISMD,GSISPACKAGES,CHANGEVAL,INTVAL,INTVAT,ISTRIG,BGDOCDATE,INSDATEN,INPAYVAT,INSDATE,INSUSER,UPDDATE,UPDUSER"
                Dim mtrdoc = {"COMPANY,FINDOC,WHOUSE,SHIPPINGADDR,SHPZIP,SHPCITY,SHIPDATE,QTY,QTY1,QTY2,QTY1S,QTY1A,WASTE,COSTCOEF,SALESCVAL,QTY1H,QTY2H,BGINTCOUNTRY,ccCTOTSHIPVALUE,ccCLockShipValue,ccCShippingNo"}
                Dim mtrln = {"Company,FINDOC,MTRLINES,LINENUM,SODTYPE,MTRL,PENDING,RESTMODE,SOSOURCE,SOREDIR,MTRTYPE,SOTYPE,WHOUSE,MTRUNIT,VAT,SALESMAN,PRCPOLICY,QTY,QTY1,QTY2,QTY1COV,QTY1CANC,QTY1FCOV,SHIPDATE,WEIGHT,VOLUME,BGINTCOUNTRY,PRICE,PRICE1,LINEVAL,LLINEVAL,EXPVAL,LEXPVAL,NETLINEVAL,LNETLINEVAL,VATAMNT,LVATAMNT,LVATNOEXM,EFKVAL,COMMENTS,TRNLINEVAL,LTRNLINEVAL,PRCRULEDATA,FINDOCS,MTRLINESS,SXPERC,AUTOPRDDOC,ccCQTY1PRO,ccCLocked,ccCPRIORITY,ccCDELIVDATE,ccCTRUCKSNO,ccCADR,ccCSHIPVALUE,ccCSOCARRIER"}
                For i As Integer = 0 To TblDetail.Count - 1


                    Dim fin As New FINDOC
                    For Each field In TblHeader.GetFieldDefs
                        Try
                            'Debug.Print(field.FieldName)
                            If Not find.IndexOf(field.FieldName) = -1 Then
                                'If TblHeader.Current(field.FieldName) > 0 Then
                                Debug.Print("___" & field.FieldName & ";" & TblHeader.Current(field.FieldName))
                                'End If
                            End If
                        Catch ex As Exception
                            Debug.Print("_err_" & field.FieldName)
                        End Try
                    Next

                    For Each field In TblDetail.GetFieldDefs
                        Debug.Print(field.FieldName)
                        If Not find.IndexOf(field.FieldName) = -1 Then
                            Debug.Print("___" & field.FieldName)
                        End If

                    Next

                    For Each pr In fin.GetType.GetProperties()
                        Try
                            Dim gg = TblDetail.Item(i, pr.Name)
                            'Debug.Print(pr.Name & "=" & gg)
                        Catch ex As Exception
                            Debug.Print(pr.Name)
                        End Try
                    Next

                Next

                '--"service":"setData","OBJECT":"SALDOC","KEY":"","DATA":{ "SALDOC": [ { "SERIES": "1001" , "TRDR": "236" } ], "ITELINES": [ { "MTRL": "2643" , "QTY1": "10", "PRICE": "5" } ] }
                'SALDOC.SERIES
                'SALDOC.TRNDATE
                'MTRDOC.WHOUSE
                'MTRDOC.SHIPDATE
                'SALDOC.TRDR_CUSTOMER_CODE
                'SALDOC.TRDBRANCH_TRDBRANCH_CODE
                'SALDOC.SALESMAN_PRSNIN_CODE
                'SALDOC.SOCURRENCY Νόμισμα
                'SALDOC.LRATE Ισοτιμία
                'SALDOC.TRDRRATE Ισοτ.συναλ.
                'SALDOC.FINSTATES Κατάσταση
                'SALDOC.SHIPKIND Διακίνηση
                'SALDOC.PAYMENT Πληρωμή
                'SALDOC.VATSTS Καθ.Φ.Π.Α.
                'SALDOC.COMMENTS Αιτιολογία
                'MTRDOC.DELIVDATE Ημερ.παράδοσης
                'SalDoc.SHIPMENT Αποστολή
                ''MTRDOC.SOCARRIER Μεταφορέας
                ''MTRDOC.TRUCKSNO Αριθ.Μεταφ.μέσου

                'ITELINES.MTRL_ITEM_CODE Κωδικός
                'ITELINES.QTY1 Ποσ.Κ/ Μ

                'ITELINES.PRICE Τιμή
                'ITELINES.DISC1PRC Εκπτ.%1
                'ITELINES.LINEVAL Αξία
                'ITELINES.QTY1COV Εκτελ.
                'ITELINES.QTY1CANC Ακυρ.
                ''ITELINES.REST Υπόλοιπο
                'ITELINES.WHREST Υπόλοιπο Α.Χ.
                'ITELINES.WHORDERED Αναμενόμενα Α.Χ.
                ''ITELINES.WHRESERVED Δεσμευμένα Α.Χ.


                'ITELINES.CCCQTY1PRO Ποσότ.Φόρτωσης
                'ITELINES.CCCSOCARRIER Μεταφορέας
                'ITELINES.CCCTRUCKSNO Αριθ.Μεταφ.μέσου
                'ITELINES.CCCSHIPVALUE Κόμιστρο
                'ITELINES.CCCPRIORITY Προτεραιότητα


                ObjSal.InsertData()

                Dim TblHeaderIn As XTable = ObjSal.GetTable("FINDOC")
                Dim TblMTRDOCIn As XTable = ObjSal.GetTable("MTRDOC")
                Dim TblDetailIn As XTable = ObjSal.GetTable("ITELINES")

                'TblHeaderIn.Add()
                Dim finds = TblHeaderIn.Current("FINDOC")
                TblHeaderIn.Current("SERIES") = 1001
                TblHeaderIn.Current("TRDR") = TblHeader.Current("TRDR") '236
                TblHeaderIn.Current("TRDBRANCH") = TblHeader.Current("TRDBRANCH") '331
                TblHeaderIn.Current("FINSTATES") = TblHeader.Current("FINSTATES") '1002
                TblHeaderIn.Current("SHIPKIND") = TblHeader.Current("SHIPKIND") '1000
                'TblMTRDOCIn.Add()
                'Dim WHOUSE As Integer = TblMTRDOC.Current("WHOUSE")
                'WHOUSE = 4
                Dim fields = {"WHOUSE", "ccCTOTSHIPVALUE", "ccCLockShipValue", "ccCShippingNo"}
                For Each ss In fields
                    Try
                        If IsDBNull(TblMTRDOC.Current(ss)) Then
                            Continue For
                        End If
                        If TblMTRDOC.Current(ss).GetType.FullName = "System.Int16" Then
                            TblMTRDOCIn.Current(ss) = CInt(TblMTRDOC.Current(ss))
                        Else
                            TblMTRDOCIn.Current(ss) = TblMTRDOC.Current(ss)
                        End If
                    Catch ex As Exception

                    End Try
                Next
                'TblMTRDOCIn.Current("FINDOC") = 144977 'TblHeaderIn.Current("FINDOC")

                'TblMTRDOCIn.Current("WHOUSE") = GmS1Conv(TblMTRDOCIn.Current("WHOUSE"), TblMTRDOC.Current("WHOUSE"))
                ''TblMTRDOCIn.Current("WHOUSE") = CInt(TblMTRDOC.Current("WHOUSE"))
                'TblMTRDOCIn.Current("ccCTOTSHIPVALUE") = GmS1Conv(TblMTRDOCIn.Current("ccCTOTSHIPVALUE"), TblMTRDOC.Current("ccCTOTSHIPVALUE"))
                'TblMTRDOCIn.Current("ccCLockShipValue") = GmS1Conv(TblMTRDOCIn.Current("ccCLockShipValue"), TblMTRDOC.Current("ccCLockShipValue"))
                'TblMTRDOCIn.Current("ccCShippingNo") = GmS1Conv(TblMTRDOCIn.Current("ccCShippingNo"), TblMTRDOC.Current("ccCShippingNo"))



                For i As Integer = 0 To TblDetail.Count - 1
                    If TblDetail.Item(i, "PENDING") = 0 Then
                        Continue For
                    End If
                    TblDetailIn.Add()
                    'TblDetailIn.Current("MTRL") = TblDetail.Item(i, "MTRL") '2146 '2643
                    'TblDetailIn.Current("QTY1") = TblDetail.Item(i, "QTY1") '25.6
                    ''TblDetailIN.Current("QTY1COV") = 2.6
                    'TblDetailIn.Current("PRICE") = TblDetail.Item(i, "PRICE") '123.45
                    'TblDetailIn.Current("NUM02") = TblDetail.Item(i, "NUM02")

                    'TblDetailIn.Current("ccCQTY1PRO") = TblDetail.Item(i, "ccCQTY1PRO")

                    'Dim ccCLocked As Integer = GmNull(TblDetail.Item(i, "ccCLocked"), TblDetail.Item(i, "ccCLocked").GetType)
                    'TblDetailIn.Current("ccCLocked") = TblDetail.Item(i, "ccCLocked")
                    'TblDetailIn.Current("ccCPRIORITY") = TblDetail.Item(i, "ccCPRIORITY")
                    'TblDetailIn.Current("ccCDELIVDATE") = TblDetail.Item(i, "ccCDELIVDATE")
                    'TblDetailIn.Current("ccCTRUCKSNO") = TblDetail.Item(i, "ccCTRUCKSNO")
                    'TblDetailIn.Current("ccCADR") = TblDetail.Item(i, "ccCADR")
                    'TblDetailIn.Current("ccCSHIPVALUE") = TblDetail.Item(i, "ccCSHIPVALUE")
                    'TblDetailIn.Current("ccCSOCARRIER") = 4 'TblDetail.Item(i, "ccCSOCARRIER")

                    fields = {"MTRL", "QTY1", "PRICE", "NUM02", "ccCQTY1PRO", "ccCLocked", "ccCPRIORITY", "ccCDELIVDATE", "ccCTRUCKSNO", "ccCADR", "ccCSHIPVALUE", "ccCSOCARRIER"}

                    For Each ss In fields
                        Try
                            If IsDBNull(TblDetail.Item(i, ss)) Then
                                Continue For
                            End If
                            If TblDetail.Item(i, ss).GetType.FullName = "System.Int16" Then
                                TblDetailIn.Current(ss) = CInt(TblDetail.Item(i, ss))
                            Else
                                TblDetailIn.Current(ss) = TblDetail.Item(i, ss)
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    If Not IsDBNull(TblDetailIn.Current("ccCQTY1PRO")) AndAlso Not TblDetailIn.Current("ccCQTY1PRO") = 0 Then
                        TblDetailIn.Current("QTY1") = TblDetailIn.Current("ccCQTY1PRO")
                    End If

                    If Not IsDBNull(TblDetailIn.Current("CCCSOCARRIER")) AndAlso Not TblDetailIn.Current("CCCSOCARRIER") = 0 Then
                        TblMTRDOCIn.Current("SOCARRIER") = TblDetailIn.Current("CCCSOCARRIER")
                    End If

                    If Not IsDBNull(TblDetailIn.Current("CCCTRUCKSNO")) AndAlso Not TblDetailIn.Current("CCCTRUCKSNO") = "" Then
                        TblMTRDOCIn.Current("TRUCKSNO") = TblDetailIn.Current("CCCTRUCKSNO")
                    End If

                    TblDetailIn.Current("FINDOCS") = TblDetail.Item(i, "FINDOC")
                    TblDetailIn.Current("MTRLINESS") = TblDetail.Item(i, "MTRLINES")
                Next
                's1Conn.ExecuteSQL("UPDATE SERIESNUM SET SERIESNUM=SERIESNUM+1 WHERE COMPANY=1000 AND SOSOURCE=1351 AND SERIES=1001 AND FISCPRD=2017")
                'sp_executesql N'UPDATE SERIESNUM SET SERIESNUM=SERIESNUM+1 WHERE COMPANY=@P1 AND SOSOURCE=@P2 AND SERIES=@P3 AND FISCPRD=@P4',N'@P1 smallint,@P2 int,@P3 smallint,@P4 smallint',1000,1351,1001,2017
                ObjSal.PostData()
                Dim d = 1
            Next
            FrmCancel = False
        Catch ex As Exception
            'x.WARNING(e)
            MsgBox(ex.Message)
            FrmCancel = True
        Finally
            SalDoc.Dispose()
            SalDoc = Nothing

            ObjSal.Dispose()
            ObjSal = Nothing

        End Try
        Return FrmCancel
    End Function

    Private Function GmS1Conv(gg As Object, oldval As Object) As Object
        Dim newVal As New Object
        Try
            If oldval.GetType.FullName = "System.Int16" Then
                newVal = CInt(oldval)
                Return newVal
            End If
            If IsDBNull(oldval) Then
                newVal = DBNull.Value
            Else
                newVal = oldval
            End If

        Catch ex As Exception

        End Try
        Return newVal
        'Throw New NotImplementedException()
    End Function

    Private Function GoConvertold(findocID As Integer) As Boolean


        Dim ans = 0
        Dim vDate
        Dim vQty = 0
        Dim vLineVal = 0
        Dim vList0
        Dim vGroupList
        Dim vGrList
        Dim vPrice = 0
        Dim vPriceD = 0
        Dim vPriceVal = 0
        Dim vWhouse = 0
        Dim vPis = 0
        Dim spis
        Dim vQtyY = 0
        Dim vQtyI = 0
        Dim vQtyA = 0
        Dim vQtyNU = 0

        Me.Cursor = Cursors.WaitCursor
        Dim SalDoc As XModule
        SalDoc = s1Conn.CreateModule("SALDOC;Βασική προβολή πωλήσεων")

        Dim ObjSal As XModule
        ObjSal = s1Conn.CreateModule("SALDOC;Βασική προβολή πωλήσεων")

        'vDate = x.EVAL("SQLDate(SALDOC.TRNDATE)")
        ''Dim ObjSal = x.CreateObj("SALDOC;Βασική προβολή πωλήσεων")
        Try
            SalDoc.LocateData(findocID)
            'x.InsertData()
            'myTable = "TRDR")
            'ObjSal.InsertData()

            Dim TblHeader As XTable = SalDoc.GetTable("FINDOC")
            Dim TblDetail As XTable = SalDoc.GetTable("ITELINES")

            For i As Integer = 0 To TblDetail.Count - 1


                Dim fin As New FINDOC
                For Each field In TblDetail.GetFieldDefs
                    Debug.Print(field.FieldName)
                Next

                For Each pr In fin.GetType.GetProperties()
                    Dim gg = TblDetail.Item(i, pr.Name)
                    Debug.Print(pr.Name & "=" & gg)

                Next

            Next

            '--"service":"setData","OBJECT":"SALDOC","KEY":"","DATA":{ "SALDOC": [ { "SERIES": "1001" , "TRDR": "236" } ], "ITELINES": [ { "MTRL": "2643" , "QTY1": "10", "PRICE": "5" } ] }

            'TblHeader.INSERT
            'TblHeader.SERIES = rser.seriescre
            ''rset.series;
            'TblHeader.TRDR = SalDoc.TRDR
            'TblHeader.TRDBRANCH = SalDoc.TRDBRANCH
            'TblHeader.TRNDATE = SalDoc.TRNDATE
            'TblHeader.FINDOCS = iSALDOCID
            'TblHeader.COMMENTS = SalDoc.CMPFINCODE + " - " + SalDoc.TRDR_CUSTOMER_NAME

            'ITELINES.FIRST
            'While Not ITELINES.EOF
            '    sg = x.GETSQLDATASET("select mtrgroup from mtrl where mtrl=" + ITELINES.MTRL, Nothing)

            '    'Gm
            '    swh = x.GETSQLDATASET("select whouse from mtrdoc where findoc=" + ITELINES.FINDOCS, Nothing)
            '    vWhouse = swh.whouse
            '    If vWhouse Is "" Then
            '        vWhouse = MTRDOC.WHOUSE
            '    End If
            '    sp4 = x.GETSQLDATASET(("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") + ITELINES.MTRL & " and d.fromdate <= " & Convert.ToString(vDate) & " and d.finaldate >= " & Convert.ToString(vDate), Nothing)

            '    vGrList = RSet.GroupYD + "," + RSet.GroupMYD
            '    vGroupList = x.EVAL("InList(" + sg.mtrgroup & "," & Convert.ToString(vGrList) & ")")

            '    If vGroupList = 1 Then
            '        'Η ομάδα του είδους της γραμμής υπάρχει στις ρυθμίσεις για υδατοδιαλυτά και μη
            '        TblDetail.INSERT
            '        TblDetail.MTRL = ITELINES.MTRL
            '        TblDetail.QTY1 = ITELINES.QTY1
            '        vQty = ITELINES.QTY1

            '        vGroupList = x.EVAL(("InList(" + sg.mtrgroup & ",") + RSet.GroupMYD & ")")
            '        'X.WARNING("t1-" + sg.mtrgroup + "-" + vGrList + "-" + vGroupList + "-" + sp4.igroup);
            '        If vGroupList = 1 Then
            '            'ΜΗ Υδατοδιαλυτά

            '            sp = x.GETSQLDATASET(((((("select isnull(dbo.fn_clDiscStep1(" & Convert.ToString(vDate) & "," & vWhouse & ",") + SalDoc.TRDBRANCH & ",") + ITELINES.MTRL & ",") + SalDoc.TRDR & ",") + SalDoc.TRDR_CUSTOMER_TRDBUSINESS & ",") + SalDoc.SHIPMENT & "),0) AS dsc", Nothing)
            '            spp = x.GETSQLDATASET((((((("select isnull(dbo.fn_clDiscStep2(" & Convert.ToString(vDate) & "," & vWhouse & ",") + SalDoc.TRDBRANCH & ",") + ITELINES.MTRL & ",") + SalDoc.TRDR & ",") + SalDoc.TRDR_CUSTOMER_TRDBUSINESS & ",") + SalDoc.SHIPMENT & "," & """") + MTRDOC.QTY1 & """" & "),0) AS val", Nothing)

            '            vPrice = spp.val * (-1)
            '            vPriceVal = ITELINES.PRICE - vPrice
            '            If sp.dsc = 0 OrElse sp.dsc = "" Then
            '                vPriceD = vPriceVal
            '            Else
            '                vPriceD = vPriceVal - (vPriceVal * (sp.dsc / 100))
            '            End If
            '            vPrice = ITELINES.PRICE - vPriceD

            '            TblDetail.PRICE = vPrice
            '            If vPrice IsNot "" Then
            '                vPis = 1
            '            End If
            '            If vPrice > 0 Then
            '                vPis = 1
            '            End If
            '        Else
            '            'Υδατοδιαλυτά και Ιχνοστοιχεία και Α΄Υλες

            '            scp = x.GETSQLDATASET("select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=" + SalDoc.TRDR, Nothing)
            '            If scp.trdr <> "" Then
            '                'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
            '                spyd = x.GETSQLDATASET("select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=5)", Nothing)
            '                'Gm Α΄Υλες

            '                spay = x.GETSQLDATASET("select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
            '            Else
            '                spyd = x.GETSQLDATASET("select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=100)", Nothing)
            '                'Gm Α΄Υλες
            '                spay = x.GETSQLDATASET("select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)", Nothing)
            '            End If

            '            spix = x.GETSQLDATASET("select sum(l.qty1) as qty from mtrlines l where l.findoc=" & iSALDOCID & " and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)", Nothing)
            '            spnu = x.GETSQLDATASET(("select l.qty1 as qty from mtrlines l, cccpricelistlines m, cccpricelist h where m.cccpricelist=h.cccpricelist and m.mtrl=l.mtrl and m.cccsumgroup=200 and l.findoc=" & iSALDOCID & " and m.mtrl=") + ITELINES.MTRL, Nothing)

            '            vQtyY = spyd.qty
            '            vQtyI = spix.qty
            '            vQtyA = spay.qty
            '            vQtyNU = spnu.qty
            '            swh = x.GETSQLDATASET("select whouse from mtrdoc where findoc=" + ITELINES.FINDOCS, Nothing)
            '            vWhouse = swh.whouse
            '            If vWhouse Is "" Then
            '                vWhouse = MTRDOC.WHOUSE
            '            End If
            '            'sp4 = X.GETSQLDATASET("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=" + ITELINES.MTRL, null);
            '            sp4 = x.GETSQLDATASET(("select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=" & vWhouse & " and d.mtrl=") + ITELINES.MTRL & " and d.fromdate <= " & Convert.ToString(vDate) & " and d.finaldate >= " & Convert.ToString(vDate), Nothing)
            '            'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate);
            '            If sp4.igroup = 100 Then
            '                If scp.trdr <> "" Then
            '                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
            '                    sp = x.GETSQLDATASET((("select dbo.fn_clDiscStep3(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & ",") + SalDoc.TRDR & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
            '                    If sp.dsc = 0 Then
            '                        sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
            '                    End If
            '                Else
            '                    sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyY & """" & ") AS dsc", Nothing)
            '                End If
            '                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
            '                TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100)
            '            ElseIf sp4.igroup = 103 Then
            '                If scp.trdr <> "" Then
            '                    'Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
            '                    sp = x.GETSQLDATASET((("select dbo.fn_clDiscStep3(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & ",") + SalDoc.TRDR & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
            '                    x.WARNING(sp.dsc)
            '                    If sp.dsc = 0 Then
            '                        sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
            '                    End If
            '                Else
            '                    sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyA & """" & ") AS dsc", Nothing)
            '                End If
            '                'X.WARNING("sum qty 100: "+vQtyY+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
            '                'X.WARNING(vQtyY + "-" + vQtyA + "-" + vQtyI + "-" + vWhouse + "-" + sp4.igroup + "-" + ITELINES.MTRL + "-" + vDate + " dsc: " + sp.dsc + " price: " + (ITELINES.PRICE * (sp.dsc / 100)));
            '                TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100)
            '            ElseIf sp4.igroup = 102 Then
            '                sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyI & """" & ") AS dsc", Nothing)
            '                'X.WARNING("sum qty 102: "+vQtyI+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));

            '                TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100)
            '            ElseIf sp4.igroup = 200 Then
            '                sp = x.GETSQLDATASET(("select dbo.fn_clDiscStep4(" & Convert.ToString(vDate) & "," & vWhouse & ",") + ITELINES.MTRL & "," & """" & vQtyNU & """" & ") AS dsc", Nothing)
            '                'X.WARNING("sum qty 200: "+ITELINES.QTY1+" dsc: "+sp.dsc+" price: "+(ITELINES.PRICE*(sp.dsc/100)));
            '                TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100)
            '            End If
            '            'sp=X.GETSQLDATASET("select dbo.fn_clDiscStep4("+vDate+","+vWhouse+","+ITELINES.MTRL+","+"\""+MTRDOC.QTY1+"\""+") AS dsc",null);
            '            'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
            '            If sp.dsc <> "" Then
            '                vPis = 1
            '            End If
            '        End If
            '        TblDetail.FINDOCS = iSALDOCID
            '        TblDetail.POST
            '    End If
            '    ITELINES.[NEXT]
            'End While


            'If vPis = 1 Then
            '    ans = x.ASK("Πιστωτικό τιμολόγιο", "Θέλετε να γίνει αυτόματη δημιουργία πιστωτικού τιμολογίου?")
            '    If ans = 6 Then
            '        SalDoc.DBPost
            '    End If
            'End If
        Catch generatedExceptionName As Exception
            'x.WARNING(e)
        Finally
            SalDoc.Dispose()
            SalDoc = Nothing

            ObjSal.Dispose()
            ObjSal = Nothing

        End Try





        'If txtTRDRCode.Text <> "" And txtTRDRName.Text <> "" Then

        '    Try
        '        Dim myTable As XTable
        '        Dim newID As Integer = 0
        '        x.InsertData()
        '        myTable = x.GetTable("TRDR")
        '        myTable.Current("CODE") = txtTRDRCode.Text.ToString
        '        myTable.Current("NAME") = txtTRDRName.Text.ToString
        '        myTable.Current("CITY") = txtTRDRCity.Text.ToString
        '        myTable.Current("PHONE01") = txtTRDRPhone01.Text.ToString
        '        newID = x.PostData()

        '        MsgBox("Customer added With ID= " + newID.ToString, MsgBoxStyle.Information, strAppName)
        '        txtTRDRCode.Text = "*"
        '        txtTRDRName.Text = ""
        '        txtTRDRCity.Text = ""
        '        txtTRDRPhone01.Text = ""

        '        FilldgTRDR(iActiveObjType)
        '    Catch ex As Exception
        '        MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        '    Finally
        '        Me.Cursor = Cursors.Default
        '    End Try
        '    ControlsVisible(True)
        '    x.Dispose()
        'Else
        '    MsgBox("Please fill In 'Code' and 'Name'!!!", MsgBoxStyle.Critical, strAppName)
        'End If
        'Throw New NotImplementedException()
    End Function

End Class