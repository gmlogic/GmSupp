function ON_LOCATE() {


    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        X.SETPROPERTY('FIELD', 'ITELINES.PRICE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.DISC1PRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.LINEVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.NUM02', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE');//Στοιχεία γραμμής - Γενικά - Τιμές, Αξίες & Εκπτώσεις
        X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE'); //RUNB_150001=Υπολογισμός πιστωτικού
    }

}

function ON_AFTERPOST() {

    var ans = 0;
    var vDate;
    var vQty = 0;
    var vLineVal = 0;
    var vList0;
    var vGroupList;
    var vGrList;
    var vPrice = 0;
    var vPriceD = 0;
    var vPriceVal = 0;
    var vWhouse = 0;
    var vPis = 0;
    var spis;
    var vQtyY = 0;
    var vQtyI = 0;
    var vQtyNU = 0;

    //======================= open item ========================

    sql1 = 'select tfprms from fprms where fprms=' + SALDOC.FPRMS + ' and company=' + X.SYS.COMPANY;
    res1 = X.GETSQLDATASET(sql1, '');

    if (res1.tfprms == 151 || res1.tfprms == 152) {

        cfnObj = 0;
        if (SALDOC.FINDOC < 0) {
            Z = X.NEWID();
        } else {
            Z = SALDOC.FINDOC;
        }

        SQL = 'SELECT DISTINCT FINDOCS FROM MTRLINES WHERE FINDOC=' + Z;
        RES = X.GETSQLDATASET(SQL, '');
        strIDs = X.EVAL('String(' + RES.FINDOCS + ')');

        try {
            cfnObj = X.CreateObj('SALDOC');
            cfnObj.DBLocate(Z);

            X.CALLPUBLISHED('ProgLibIntf.ModuleCommand', cfnObj.MODULE, 1032, strIDs);
        }
        catch (e) {
            X.WARNING(cfnObj.GETLASTERROR);
        }
        finally {
            cfnObj.FREE;
            cfnObj = 0;
        }
    }

    //======================= open item ========================



    rset = X.GETSQLDATASET('select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=' + X.SYS.COMPANY, null);
    rser = X.GETSQLDATASET('select seriescre from cccsettingslines where seriesinv=' + SALDOC.SERIES, null);


    if (SALDOC.FINDOC < 0) {
        iSALDOCID = X.NEWID();
    } else {
        DeleteSalDoc();
        iSALDOCID = SALDOC.FINDOC;
    }



    vList = X.EVAL('InList(SALDOC.SERIES,' + rset.priceseries + ')');
    if (SALDOC.SHIPMENT == '') {
        X.WARNING('ΔΕΝ ΕΧΕΤΕ ΕΠΙΛΕΞΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ...!');
        return;
    }
    spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);

    //if(spis.RECORDCOUNT>0)
    //{
    if (vList == 1) {

        if (rset.district == 1) {
            sd = X.GETSQLDATASET('SELECT DISTRICT1 FROM TRDBRANCH WHERE TRDBRANCH=' + SALDOC.TRDBRANCH, null);
            if (sd.DISTRICT1 == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΝΟΜΟ ΠΑΡΑΛΗΠΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }
        if (rset.trdbusiness == 1) {
            sb = X.GETSQLDATASET('SELECT TRDBUSINESS FROM TRDR WHERE TRDR=' + SALDOC.TRDR, null);
            if (sb.TRDBUSINESS == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΕΜΠΟΡΙΚΗ ΚΑΤΗΓΟΡΙΑ ΠΕΛΑΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }
        if (rset.shipment == 1) {
            if (SALDOC.SHIPMENT == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }

        vDate = X.EVAL('SQLDate(SALDOC.TRNDATE)');
        ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων');
        try {
            ObjSal.DBInsert;

            TblHeader = ObjSal.FindTable('FINDOC');
            TblDetail = ObjSal.FindTable('ITELINES');


            TblHeader.INSERT;
            TblHeader.SERIES = rser.seriescre;//rset.series;
            TblHeader.TRDR = SALDOC.TRDR;
            TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
            TblHeader.TRNDATE = SALDOC.TRNDATE;
            TblHeader.FINDOCS = iSALDOCID;
            TblHeader.COMMENTS = SALDOC.CMPFINCODE + ' - ' + SALDOC.TRDR_CUSTOMER_NAME;

            ITELINES.FIRST;
            while (!ITELINES.EOF) {
                sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);

                vGrList = rset.GroupYD + ',' + rset.GroupMYD;
                vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');

                if (vGroupList == 1) //Η ομάδα του είδους της γραμμής υπάρχει στις ρυθμίσεις για υδατοδιαλυτά και μη
                {
                    TblDetail.INSERT;
                    TblDetail.MTRL = ITELINES.MTRL;
                    TblDetail.QTY1 = ITELINES.QTY1;
                    vQty = ITELINES.QTY1;

                    vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + rset.GroupMYD + ')');

                    if (vGroupList == 1) //ΜΗ Υδατοδιαλυτά
                    {
                        swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                        if (vWhouse == '') {
                            vWhouse = MTRDOC.WHOUSE;
                        }
                        sp = X.GETSQLDATASET('select isnull(dbo.fn_clDiscStep1(' + vDate + ',' + vWhouse + ',' + SALDOC.TRDBRANCH + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + SALDOC.TRDR_CUSTOMER_TRDBUSINESS + ',' + SALDOC.SHIPMENT + '),0) AS dsc', null);
                        spp = X.GETSQLDATASET('select isnull(dbo.fn_clDiscStep2(' + vDate + ',' + vWhouse + ',' + SALDOC.TRDBRANCH + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + SALDOC.TRDR_CUSTOMER_TRDBUSINESS + ',' + SALDOC.SHIPMENT + ',' + '\'' + MTRDOC.QTY1 + '\'' + '),0) AS val', null);

                        vPrice = spp.val * (-1);
                        vPriceVal = ITELINES.PRICE - vPrice;
                        if (sp.dsc == 0 || sp.dsc == '') {
                            vPriceD = vPriceVal;
                        } else {
                            vPriceD = vPriceVal - (vPriceVal * (sp.dsc / 100));
                        }
                        vPrice = ITELINES.PRICE - vPriceD;

                        TblDetail.PRICE = vPrice;
                        if (vPrice != '') {
                            vPis = 1;
                        }
                        if (vPrice > 0) {
                            vPis = 1;
                        }
                    } else //Υδατοδιαλυτά και Ιχνοστοιχεία
                    {

                        scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                        if (scp.trdr != '') //Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                        {
                            spyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=5)', null);
                        } else {
                            spyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=100)', null);
                        }

                        spix = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)', null);
                        spnu = X.GETSQLDATASET('select l.qty1 as qty from mtrlines l, cccpricelistlines m, cccpricelist h where m.cccpricelist=h.cccpricelist and m.mtrl=l.mtrl and m.cccsumgroup=200 and l.findoc=' + iSALDOCID + ' and m.mtrl=' + ITELINES.MTRL, null);

                        vQtyY = spyd.qty;
                        vQtyI = spix.qty;
                        vQtyNU = spnu.qty;
                        swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                        vWhouse = swh.whouse;
                        if (vWhouse == '') {
                            vWhouse = MTRDOC.WHOUSE;
                        }
                        sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=' + ITELINES.MTRL, null);
                        if (sp4.igroup == 100) {
                            if (scp.trdr != '') //Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                            {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                            } else {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                            }
                            //X.WARNING('sum qty 100: '+vQtyY+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
                            TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100);
                        } else if (sp4.igroup == 102) {
                            sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyI + '\'' + ') AS dsc', null);
                            //X.WARNING('sum qty 102: '+vQtyI+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
                            TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100);

                        } else if (sp4.igroup == 200) {
                            sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyNU + '\'' + ') AS dsc', null);
                            //X.WARNING('sum qty 200: '+ITELINES.QTY1+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
                            TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100);
                        }
                        //sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
                        //ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
                        if (sp.dsc != '') {
                            vPis = 1;
                        }
                    }
                    TblDetail.FINDOCS = iSALDOCID;
                    TblDetail.POST;
                }
                ITELINES.NEXT;
            }


            if (vPis == 1) {
                ans = X.ASK('Πιστωτικό τιμολόγιο', 'Θέλετε να γίνει αυτόματη δημιουργία πιστωτικού τιμολογίου?');
                if (ans == 6) {
                    ObjSal.DBPost;
                }
            }
        }
        finally {
            ObjSal.FREE;
            ObjSal = null;
        }

    }
    //}
}

function ON_DELETE() {
    DeleteSalDoc()
}

function DeleteSalDoc() {
    s = 'SELECT FINDOC FROM FINDOC WHERE SOSOURCE=1351 AND FINDOCS=' + SALDOC.FINDOC;

    ds = X.GETSQLDATASET(s, null);

    if (ds.RECORDCOUNT > 0) {
        ObjPrdn = X.CreateObj('SALDOC');
        try {
            ds.FIRST;
            while (!ds.EOF()) {
                ObjPrdn.DBLocate(ds.FINDOC);
                ObjPrdn.DBDelete;
                ds.NEXT;
            }
        }
        finally {
            ObjPrdn.FREE;
            ObjPrdn = null;
        }
    }
}

function EXECCOMMAND(cmd) {


    var ans = 0;
    var vDate;
    var vQty = 0;
    var vLineVal = 0;
    var vList0;
    var vGroupList;
    var vGrList;
    var vPrice = 0;
    var vPriceD = 0;
    var vPriceVal = 0;
    var vPis = 0;
    var vWhouse = 0;
    var recs = 0;
    var vQtyY = 0;
    var vQtyI = 0;


    if (cmd == 150001) {
        rset = X.GETSQLDATASET('select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=' + X.SYS.COMPANY, null);
        rser = X.GETSQLDATASET('select seriescre from cccsettingslines where seriesinv=' + SALDOC.SERIES, null);


        if (rset.district == 1) {
            sd = X.GETSQLDATASET('SELECT DISTRICT1 FROM TRDBRANCH WHERE TRDBRANCH=' + SALDOC.TRDBRANCH, null);
            if (sd.DISTRICT1 == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΝΟΜΟ ΠΑΡΑΛΗΠΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }
        if (rset.trdbusiness == 1) {
            sb = X.GETSQLDATASET('SELECT TRDBUSINESS FROM TRDR WHERE TRDR=' + SALDOC.TRDR, null);
            if (sb.TRDBUSINESS == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΕΜΠΟΡΙΚΗ ΚΑΤΗΓΟΡΙΑ ΠΕΛΑΤΗ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }
        if (rset.shipment == 1) {
            if (SALDOC.SHIPMENT == '') {
                X.WARNING('ΔΕΝ ΕΧΕΤΕ ΣΥΜΠΛΗΡΩΣΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
                return;
            }
        }


        spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);


        //if(spis.RECORDCOUNT>0)
        //{

        vDate = X.EVAL('SQLDate(SALDOC.TRNDATE)');

        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);

            vGrList = rset.GroupYD + ',' + rset.GroupMYD;
            vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');

            if (vGroupList == 1) {

                vQty = ITELINES.QTY1;

                vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + rset.GroupMYD + ')');

                if (vGroupList == 1) //ΜΗ Υδατοδιαλυτά
                {
                    swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                    vWhouse = swh.whouse;
                    if (vWhouse == '') {
                        vWhouse = MTRDOC.WHOUSE;
                    }
                    sp = X.GETSQLDATASET('select isnull(dbo.fn_clDiscStep1(' + vDate + ',' + vWhouse + ',' + SALDOC.TRDBRANCH + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + SALDOC.TRDR_CUSTOMER_TRDBUSINESS + ',' + SALDOC.SHIPMENT + '),0) AS dsc', null);
                    spp = X.GETSQLDATASET('select isnull(dbo.fn_clDiscStep2(' + vDate + ',' + vWhouse + ',' + SALDOC.TRDBRANCH + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + SALDOC.TRDR_CUSTOMER_TRDBUSINESS + ',' + SALDOC.SHIPMENT + ',' + '\'' + MTRDOC.QTY1 + '\'' + '),0) AS val', null);

                    vPrice = spp.val * (-1);
                    vPriceVal = ITELINES.PRICE - vPrice;
                    if (sp.dsc == 0 || sp.dsc == '') {
                        vPriceD = vPriceVal;
                    } else {
                        vPriceD = vPriceVal - (vPriceVal * (sp.dsc / 100));
                    }
                    vPrice = ITELINES.PRICE - vPriceD;

                    ITELINES.NUM02 = vPrice;

                    vPis = 1;

                } else //if(sg.mtrgroup==rset.GroupYD)
                {
                    swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                    vWhouse = swh.whouse;
                    if (vWhouse == '') {
                        vWhouse = MTRDOC.WHOUSE;
                    }
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
                    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
                    if (sp.dsc != '') {
                        vPis = 1;
                    } else {
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
                        ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
                        if (sp.dsc != '') {
                            vPis = 1;
                        }
                    }
                }
            }
            ITELINES.NEXT;
        }


        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=' + ITELINES.MTRL, null);
            if (sp4.igroup == 100) {
                vQtyY = vQtyY + ITELINES.QTY1;
            } else if (sp4.igroup == 102) {
                vQtyI = vQtyI + ITELINES.QTY1;
            }
            ITELINES.NEXT;
        }

        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
            vWhouse = swh.whouse;
            if (vWhouse == '') {
                vWhouse = MTRDOC.WHOUSE;
            }
            sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=' + ITELINES.MTRL, null);
            if (sp4.igroup == 100) {
                scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                if (scp.trdr != '') {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                } else {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                }
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            } else if (sp4.igroup == 102) {
                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyI + '\'' + ') AS dsc', null);
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);

            } else if (sp4.igroup == 200) {
                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + ITELINES.QTY1 + '\'' + ') AS dsc', null);
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            }
            //sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
            //ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
            if (sp.dsc != '') {
                vPis = 1;
            }

            ITELINES.NEXT;
        }



        //}
    }
}