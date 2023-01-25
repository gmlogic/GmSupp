//Last Modified 08/11/2021 14:39
var status = 0;

function UnVisibleObjs() {

    //108 = ΑΠΟΘΗΚΗ ΘΕΣΣΑΛΟΝΙΚΗΣ
    //109 = ΑΠΟΘΗΚΗ ΠΥΡΓΟΥ
    //110 = ΑΠΟΘΗΚΗ ΑΣΠΡΟΠΥΡΓΟΥ
    //104 = ΑΠΟΘΗΚΗ ΚΑΒΑΛΑΣ
    //102 = WARE HOUSE MANAGMENT 
    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        X.SETPROPERTY('FIELD', 'ITELINES.PRICE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.DISC1PRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.LINEVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.NUM02', 'VISIBLE', 'FALSE');
        //gm
        X.SETPROPERTY('FIELD', 'ITELINES.ccCDiscPRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.ccCDiscVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.CCCSHIPVALUE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'MTRDOC.ccCTOTSHIPVALUE', 'VISIBLE', 'FALSE');

        X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE'); //Στοιχεία γραμμής - Γενικά - Τιμές, Αξίες & Εκπτώσεις
        X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE'); //RUNB_150001=Υπολογισμός πιστωτικού
        X.SETPROPERTY('PANEL', 'Μεταφορικά', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('PANEL', 'N_LockAX', 'VISIBLE', 'FALSE');

    }
    //X.WARNING(SALDOC.SERIES)
    if (SALDOC.SERIES != 1061 && SALDOC.SERIES != 2061 && SALDOC.SERIES != 3061 && SALDOC.SERIES != 1081 && SALDOC.SERIES != 2081 && SALDOC.SERIES != 3081) {
        //N_CancelQTY1COVnum04
        X.SETPROPERTY('PANEL', 'N_CancelQTY1COV', 'VISIBLE', 'FALSE'); //RUNB_150002=Ακύρωση εκτελεσμένων
    }

    if (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046) {
        if (MTRDOC.ccCLockShipValue == 1) {
            MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'TRUE');
        }
        else {
            MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'FALSE');
        }
    }

    //201 = Administrator
    if (X.SYS.GROUPS == 201 && (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046)) {
        X.SETPROPERTY('PANEL', 'N_CreateCarrierDoc', 'VISIBLE', 'TRUE'); //RUNB_150003=Δημιουργία Προχρέωσης μεταφορικών
    }
    else {
        X.SETPROPERTY('PANEL', 'N_CreateCarrierDoc', 'VISIBLE', 'FALSE');
    }

    //201 = Administrator, 100 = ΛΟΓΙΣΤΗΡΙΟ
    if (X.SYS.GROUPS == 201 || X.SYS.GROUPS == 100 ||
        SALDOC.FPRMS == 1000 ||
        SALDOC.FPRMS == 1001 ||
        SALDOC.FPRMS == 1003 ||
        SALDOC.FPRMS == 7046 ||
        SALDOC.FPRMS == 7047
    ) { //Pick, Δελτίο φόρτωσης, Εντολή Φόρτωσης, Δελτίο Ενδοδιακίνησης
        X.SETPROPERTY('PANEL', 'N_ChangeBranch', 'VISIBLE', 'TRUE'); //RUNB_150011=Αλλαγή Υποκ/μα
    }
    else {
        X.SETPROPERTY('PANEL', 'N_ChangeBranch', 'VISIBLE', 'FALSE');
    }
}

function ON_INSERT() {
    //X.WARNING('ON_INSERT')
    status = 2;
    UnVisibleObjs()

}
function ON_SALDOC_SERIES() {
    //X.WARNING(SALDOC.series)
    if (SALDOC.SERIES == 1003) //Εντολή Φόρτωσης
    {
        //23713500081	ΕΛΛΑΓΡΟΛΙΠ Α.Ε.Β.Ε.
        SALDOC.TRDR = 2371;
        //1708	2371 9000001	ΕΛΛΑΓΡΟΛΙΠ Α.Ε.Β.Ε (ΥΠ/ΜΑ ΘΕΣ/ΚΗΣ)
        //SALDOC.TRDBRANCH = 1708;
        //1000 Καθοδόν
        SALDOC.FINSTATES = 1000;
        //1000 Εδρα
        MTRDOC.BRANCHSEC = 1000;
        MTRDOC.WHOUSESEC = null;
    }
    //201 = Administrator, 100 = ΛΟΓΙΣΤΗΡΙΟ
    if (X.SYS.GROUPS == 201 || X.SYS.GROUPS == 100 ||
        SALDOC.FPRMS == 1000 ||
        SALDOC.FPRMS == 1001 ||
        SALDOC.FPRMS == 1003 ||
        SALDOC.FPRMS == 7046 ||
        SALDOC.FPRMS == 7047
    ) { //Pick, Δελτίο φόρτωσης, Εντολή Φόρτωσης, Δελτίο Ενδοδιακίνησης
        X.SETPROPERTY('PANEL', 'N_ChangeBranch', 'VISIBLE', 'TRUE'); //RUNB_150011=Αλλαγή Υποκ/μα
    }
    else {
        X.SETPROPERTY('PANEL', 'N_ChangeBranch', 'VISIBLE', 'FALSE');
    }
}




function ON_LOCATE() {
    status = 1;
    UnVisibleObjs()
    //108 = ΑΠΟΘΗΚΗ ΘΕΣΣΑΛΟΝΙΚΗΣ
    //109 = ΑΠΟΘΗΚΗ ΠΥΡΓΟΥ
    //110 = ΑΠΟΘΗΚΗ ΑΣΠΡΟΠΥΡΓΟΥ
    //104 = ΑΠΟΘΗΚΗ ΚΑΒΑΛΑΣ
    //102 = WARE HOUSE MANAGMENT 
    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        X.SETPROPERTY('FIELD', 'ITELINES.PRICE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.DISC1PRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.LINEVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.NUM02', 'VISIBLE', 'FALSE');
        //gm
        X.SETPROPERTY('FIELD', 'ITELINES.CCCSHIPVALUE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'MTRDOC.ccCTOTSHIPVALUE', 'VISIBLE', 'FALSE');

        X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE'); //Στοιχεία γραμμής - Γενικά - Τιμές, Αξίες & Εκπτώσεις
        X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE'); //RUNB_150001=Υπολογισμός πιστωτικού
        X.SETPROPERTY('PANEL', 'Μεταφορικά', 'VISIBLE', 'FALSE');

    }
    ////X.WARNING(SALDOC.SERIES)
    //if (SALDOC.SERIES != 1061 && SALDOC.SERIES != 2061 && SALDOC.SERIES != 3061) {
    //    //N_CancelQTY1COVnum04
    //    X.SETPROPERTY('PANEL', 'N_CancelQTY1COV', 'VISIBLE', 'FALSE'); //RUNB_150002=Ακύρωση εκτελεσμένων
    //}

    if (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046) {
        if (MTRDOC.ccCLockShipValue == 1) {
            MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'TRUE');
        }
        else {
            MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'FALSE');
        }
    }

    //201 = Administrator
    if (X.SYS.GROUPS == 201 && (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046)) {
        X.SETPROPERTY('PANEL', 'N_CreateCarrierDoc', 'VISIBLE', 'TRUE'); //RUNB_150003=Δημιουργία Προχρέωσης μεταφορικών
    }
    else {
        X.SETPROPERTY('PANEL', 'N_CreateCarrierDoc', 'VISIBLE', 'FALSE');
    }

}

function ON_MTRDOC_ccCLockShipValue() {
    if (MTRDOC.ccCLockShipValue == 1) {
        MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'TRUE');
    }
    else {
        MTRDOC.SETREADONLY('ccCTOTSHIPVALUE', 'FALSE');
    }
}

function ON_MTRDOC_WHOUSESEC() {
    if (SALDOC.FPRMS != 7040) //7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
    {
        return;
    }
    if (SALDOC.TRDR != 2371) //3500081 ΕΛΛΑΓΡΟΛΙΠ Α.Ε.Β.Ε.
    {
        return;
    }
    if (SALDOC.ISPRINT == 2371) //Εκτυπωμένο
    {
        return;
    }
    //2	9000000	1707
    //4	9000001	1708
    //5	9000002	1709
    //8	9000003	1710
    //13	9000004	1712
    //X.WARNING('SALDOC.FPRMS=' + SALDOC.FPRMS + ' SALDOC.TRDR=' + SALDOC.TRDR + ' MTRDOC.WHOUSESEC = ' + MTRDOC.WHOUSESEC)
    return;
    switch (parseInt(MTRDOC.WHOUSESEC)) {
        case 2://ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
            SALDOC.TRDBRANCH = 1707;//Κ.Δ Καβάλας
            break;
        //case 3://ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        //    TblDetail.COSTCNTR = 212;//Κ.Δ Καβάλας
        //    break;
        case 4://ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
            SALDOC.TRDBRANCH = 1708;//Κ.Δ Διαβατών Θεσ/κης
            break;
        case 5://ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            SALDOC.TRDBRANCH = 1709;//Κ.Δ Πύργου
            break;
        case 8://ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
            SALDOC.TRDBRANCH = 1710;//Κ.Δ Ασπροπύργου
            break;
        case 13://ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
            SALDOC.TRDBRANCH = 1712;//Κ.Δ Βαθύλακος
            break;
        //case 17://ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
        //    TblDetail.COSTCNTR = 208;//Κ.Δ Φυτοθρεπτική
    }
}


//Gm
function ON_POST() {
    //X.WARNING('status = ' + status);
    //X.WARNING(FINDOC.SERIES);
    //if (FINDOC.FPRMS == 7040 || FINDOC.FPRMS == 7041 || FINDOC.FPRMS == 7046) {
    //    if (SALDOC.ISPRINT == 1) {
    //        X.EXCEPTION('Προσοχή !!! Δεν μπορεί να μεταβληθεί εκτυπωμένο παραστατικό');
    //    }
    //}

    //108 = ΑΠΟΘΗΚΗ ΘΕΣΣΑΛΟΝΙΚΗΣ
    //109 = ΑΠΟΘΗΚΗ ΠΥΡΓΟΥ
    //110 = ΑΠΟΘΗΚΗ ΑΣΠΡΟΠΥΡΓΟΥ
    //104 = ΑΠΟΘΗΚΗ ΚΑΒΑΛΑΣ
    //102 = WARE HOUSE MANAGMENT
    visGroup = 1;
    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        visGroup = 0;
        //7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
        //7041	Δελτίο Αποστολής	Δελτίο αποστολής
        //7042	Δελτίο Αποστολής (Απ.Λιαν.)	Δελτίο αποστολής
        //7043	Δελτίο Αποστολής Απο Πελάτη	Δελτίο επιστροφής
        //7045	Δ Ποσοτικής Παραλαβής	Δελτίο ποσοτικής παραλαβής
        //7046	Δελτίο Αποστολής	Εσωτερική διακίνηση

        if (FINDOC.FPRMS == 7040 || FINDOC.FPRMS == 7041 || FINDOC.FPRMS == 7046) {
            if (!SALDOC.FINSTATES) {
                X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Κατάσταση:]');
            }
            if (!SALDOC.SHIPKIND) {
                X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Διακίνηση:]');
            }
        }
        //X.EXCEPTION(typeof SALDOC.FINSTATES + '-' + SALDOC.FINSTATES + '-' + SALDOC.SHIPKIND);
        //return;
    }

    if (FINDOC.FPRMS == 7040 || FINDOC.FPRMS == 7041 || FINDOC.FPRMS == 7046) {
        if (!SALDOC.TRDBRANCH) {
            X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Υποκ.πελ.:]');
        }
        if (!MTRDOC.SOCARRIER) {
            X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Μεταφορέας:]');
        }
    }

    //1001	Δελτίο φόρτωσης
    //7040	Δελτίο Διακίνησης + -
    //7041	Δελτίο Αποστολής
    //7046	Δελτίο Αποστολής Δελτίο Ενδοδιακίνησης + -
    if (SALDOC.FPRMS == 1001 || SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046) {
        //ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων'); 
        //X.WARNING(MTRDOC.ccCTOTSHIPVALUE);
        if (MTRDOC.SOCARRIER == 9999) { //ΜΕΤΑΦΟΡΙΚΑ ΠΕΛΑΤΗ
            return;
        }

        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            ITELINES.ccCSOCARRIER = MTRDOC.SOCARRIER;
            ITELINES.NEXT;
        }

        if (MTRDOC.ccCLockShipValue != 1) { //Not Lock
            MTRDOC.ccCTOTSHIPVALUE = 0;
            ITELINES.FIRST;
            while (!ITELINES.EOF) {
                if (ITELINES.CCCSHIPVALUE !== null && ITELINES.CCCSHIPVALUE != 0) {
                    //X.WARNING(ITELINES.CCCSHIPVALUE);
                    MTRDOC.ccCTOTSHIPVALUE = MTRDOC.ccCTOTSHIPVALUE + (ITELINES.QTY1 * ITELINES.CCCSHIPVALUE);
                }
                ITELINES.NEXT;
            }
        }

        if (MTRDOC.SOCARRIER == 8888) { //ΜΕΤΑΦΟΡΙΚΑ ΕΛΛΑΓΡΟΛΙΠ
            X.WARNING('Προσοχή !!! Επιλέξατε μεταφορέα ΜΕΤΑΦΟΡΙΚΑ ΕΛΛΑΓΡΟΛΙΠ');
        }

        if (visGroup == 1) {
            if (MTRDOC.ccCTOTSHIPVALUE == 0) {
                X.WARNING('Προσοχή !!! Λάθος κόμιστρο = ' + MTRDOC.ccCTOTSHIPVALUE);
            }
        }
    }
}



function ON_RESTOREEVENTS() {
    status = 2;
    UnVisibleObjs()
    //X.WARNING('ON_RESTOREEVENTS')
    //Gm
    //X.WARNING(FINDOC.SERIES);
    if (FINDOC.SERIES == 1001) //Δελτίο φόρτωσης
    {
        //ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων'); 				
        try {

            ITELINES.FIRST;
            while (!ITELINES.EOF) {
                if (ITELINES.CCCQTY1PRO !== null && ITELINES.CCCQTY1PRO != 0) {
                    ITELINES.QTY1 = ITELINES.CCCQTY1PRO;
                }
                //Γ-Μεταφορέας,Γ-Πινακίδα,Γ-Κόμιστρο €,Γ-ADR
                //Κατά την καταχώρηση του Δελτίο φόρτωσης μεταφέρεται ο μεταφορέας γραμμής στο Header.
                if (ITELINES.CCCSOCARRIER !== null && ITELINES.CCCSOCARRIER != 0) {
                    MTRDOC.SOCARRIER = ITELINES.CCCSOCARRIER;
                }
                if (ITELINES.CCCTRUCKSNO !== null && ITELINES.CCCTRUCKSNO != '') {
                    MTRDOC.TRUCKSNO = ITELINES.CCCTRUCKSNO;
                }
                ITELINES.NEXT;
            }
        }
        catch (e) {
            X.WARNING('ON_RESTOREEVENTS' + '\r\n' + e);
        }
        finally {
            //ObjSal.FREE; 
            //ObjSal =null; 
        }
    }


    if (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046) {
        Z = ITELINES.FINDOCS;
        //Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος 
        SQL = 'SELECT DISTINCT FINSTATES FROM FINDOC WHERE FINDOC=' + Z;
        ds = X.GETSQLDATASET(SQL, null);
        if (ds.FINSTATES !== null) {
            SALDOC.FINSTATES = ds.FINSTATES;
        }
    }

    //8039 = Παραλαβή από διακίνηση(Θεσ/νίκης)
    if (SALDOC.SERIES == 8039) {
        var vfincode = '';
        //X.WARNING(ITELINES.FINDOC + '--' + ITELINES.FINDOCS);
        ITELINES.FIRST;
        while (!ITELINES.EOF) {

            //Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος 
            SQL = 'SELECT f.FINCODE FROM MTRLINES AS mt INNER JOIN FINDOC AS f ON mt.FINDOC = f.FINDOC WHERE mt.FINDOCS = ' + ITELINES.FINDOCS + ' AND mt.MTRLINES = ' + ITELINES.MTRLINES;
            ds = X.GETSQLDATASET(SQL, null);

            if (ds.FINCODE != '') {
                vfincode = vfincode + ds.FINCODE + '--'
            }
            ITELINES.NEXT;
        }

        if (vfincode != '') {
            X.WARNING('Προσοχή !!! Μετασχ/να παραστατικά ' + '\r\n' + vfincode);
        }

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
    //Not used
    //var spis;
    var vQtyY = 0;
    var vQtyI = 0;
    var vQtyA = 0;
    var vQtyNU = 0;
    /*
    //======================= open item ========================

    sql1 = 'select tfprms from fprms where fprms=' + SALDOC.FPRMS + ' and company=' + X.SYS.COMPANY;
    res1 = X.GETSQLDATASET(sql1, '');

    //Τύπου [Συμπεριφορά] 
    //151 Πιστωτικό τιμολόγιο επιστροφής
    //152 Πιστωτικό τιμολόγιο
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
    */

    //Σειρές που έχουν επιλεχθεί από τον cccsettings για αυτόματη έκδοση πιστωτικού.
    rset = X.GETSQLDATASET('select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=' + X.SYS.COMPANY, null);
    rser = X.GETSQLDATASET('select seriescre from cccsettingslines where seriesinv=' + SALDOC.SERIES, null);


    if (SALDOC.FINDOC < 0) {
        //Νέο Πιστωτικό
        iSALDOCID = X.NEWID();
    } else {
        //Διαγραφή πιστωτικών εάν έχουν εκδοθεί.
        DeleteSalDoc();
        iSALDOCID = SALDOC.FINDOC;
    }

    //rset.priceseries = 1060,1061,1761,2061
    vList = X.EVAL('InList(SALDOC.SERIES,' + rset.priceseries + ')');
    if (SALDOC.SHIPMENT == '') {
        X.WARNING('ΔΕΝ ΕΧΕΤΕ ΕΠΙΛΕΞΕΙ ΤΡΟΠΟ ΑΠΟΣΤΟΛΗΣ...!');
        return;
    }
    //Not used
    //spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);

    //if(spis.RECORDCOUNT>0)
    //{

    //Εάν σωστές Σειρές και προυποθέσεις τότε έκδοση πιστωτικού
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

        vDate = X.EVAL('SQLDate(MTRDOC.SHIPDATE)');
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

                //Gm
                //Διαβάζει whouse απ0ό το προηγούμενο παραστατικό πχ ΔΑ -> ΤΙΜ
                swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                vWhouse = swh.whouse;
                //X.WARNING('swh.whouse-' + swh.whouse)

                //if Νέο Παραστατικό
                if (vWhouse == '') {
                    vWhouse = MTRDOC.WHOUSE;
                }
                //X.WARNING('SALDOC.INT02-' + SALDOC.INT02)

                if (SALDOC.INT02 != 0) {
                    vWhouse = SALDOC.INT02;
                }
                //X.WARNING('vWhouse-' + vWhouse)

                sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);

                vGrList = rset.GroupYD + ',' + rset.GroupMYD;
                vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');

                if (vGroupList == 1) //Η ομάδα του είδους της γραμμής υπάρχει στις ρυθμίσεις για υδατοδιαλυτά και μη
                {
                    TblDetail.INSERT;
                    TblDetail.MTRL = ITELINES.MTRL;
                    TblDetail.QTY1 = ITELINES.QTY1;
                    vQty = ITELINES.QTY1;

                    vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + rset.GroupMYD + ')');
                    //X.WARNING('t1-' + sg.mtrgroup + '-' + vGrList + '-' + vGroupList + '-' + sp4.igroup);
                    if (vGroupList == 1 && sp4.igroup !== 103) //ΜΗ Υδατοδιαλυτά
                    {
                        spmyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=4)', null);

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
                    } else //Υδατοδιαλυτά και Ιχνοστοιχεία και Α΄Υλες
                    {
                        //sosource=5 Πιστωτική πολιτική Υδατοδιαλυτών
                        scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                        if (scp.trdr != '') //Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                        {
                            spyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=5)', null);

                            //Gm Α΄Υλες
                            spay = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)', null);

                        } else {
                            spyd = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=100)', null);
                            //Gm Α΄Υλες
                            spay = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=103)', null);
                        }
                        qq = 'select sum(sqty1) as sumqty1 \
                            from (select case when isnull(ex.num04, 0) = 0 then l.qty1 else l.qty1 * ex.num04 end as sqty1 \
                              from mtrlines as l left outer join \
                                mtrextra as ex on l.mtrl = ex.mtrl \
                              where (l.findoc = 214280) and (l.mtrl in \
                                (select ll.mtrl \
                                from cccpricelistlines as ll inner join \
                                  cccpricelist as hh on ll.cccpricelist = hh.cccpricelist \
                                where (hh.sosource = 7) and (ll.cccsumgroup = 102))) \
                              group by case when isnull(ex.num04, 0) = 0 then l.qty1 else l.qty1 * ex.num04 end) as s'
                        //spix = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)', null);
                        spix = X.GETSQLDATASET(qq, null);
                        spnu = X.GETSQLDATASET('select l.qty1 as qty from mtrlines l, cccpricelistlines m, cccpricelist h where m.cccpricelist=h.cccpricelist and m.mtrl=l.mtrl and m.cccsumgroup=200 and l.findoc=' + iSALDOCID + ' and m.mtrl=' + ITELINES.MTRL, null);

                        vQtyY = spyd.qty;
                        vQtyI = spix.sumqty1;
                        vQtyA = spay.qty;
                        vQtyNU = spnu.qty;
                        swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                        vWhouse = swh.whouse;
                        //X.WARNING('swh.whouse-' + swh.whouse)

                        //if Νέο Παραστατικό
                        if (vWhouse == '') {
                            vWhouse = MTRDOC.WHOUSE;
                        }
                        //X.WARNING('SALDOC.INT02-' + SALDOC.INT02)

                        if (SALDOC.INT02 != 0) {
                            vWhouse = SALDOC.INT02;
                        }
                        //X.WARNING('vWhouse-' + vWhouse)

                        //sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=' + ITELINES.MTRL, null);
                        //sosource=7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
                        sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
                        //X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate);
                        if (sp4.igroup == 100) {
                            if (scp.trdr != '') //Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                            {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                                if (sp.dsc == 0) {
                                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                                }
                            } else {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                            }
                            //X.WARNING('sum qty 100: '+vQtyY+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
                            TblDetail.PRICE = ITELINES.PRICE * (sp.dsc / 100);
                        } else if (sp4.igroup == 103) {
                            if (scp.trdr != '') //Έλεγχος ύπαρξης ειδικής πιστωτικής πολιτικής πελάτη
                            {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                                X.WARNING(sp.dsc);
                                if (sp.dsc == 0) {
                                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                                }
                            } else {
                                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                            }
                            //X.WARNING('sum qty 100: '+vQtyY+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
                            //X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate + ' dsc: ' + sp.dsc + ' price: ' + (ITELINES.PRICE * (sp.dsc / 100)));
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
                        if (typeof sp !== 'undefined' && sp.dsc != '') {
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
        catch (e) {
            X.WARNING('ON_AFTERPOST1' + '\r\n' + e);
        }
        finally {
            ObjSal.FREE;
            ObjSal = null;
        }

        CreateSecondCreditInv();

    }
    //}

    //X.WARNING('iSALDOCID = ' + iSALDOCID);
    if (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7041 || SALDOC.FPRMS == 7046) {
        //ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων'); 				
        try {
            if (MTRDOC.SOCARRIER != 9999) { // ΜΕΤΑΦΟΡΙΚΑ ΠΕΛΑΤΗ
                CreateCarrierDoc(0, iSALDOCID)
            }
        }
        catch (e) {
            X.WARNING('ON_AFTERPOST1' + '\r\n' + e);
        }
        finally {
            //ObjSal.FREE; 
            //ObjSal =null; 
        }
    }
}
function CreateSecondCreditInv() {
    //var ans = 0;
    //var vDate;
    //var vQty = 0;
    //var vLineVal = 0;
    //var vList0;
    //var vGroupList;
    //var vGrList;
    //var vPrice = 0;
    //var vPriceD = 0;
    //var vPriceVal = 0;
    //var vWhouse = 0;
    var vPis = 0;
    ////Not used
    ////var spis;
    //var vQtyY = 0;
    //var vQtyI = 0;
    //var vQtyA = 0;
    //var vQtyNU = 0;

    ITELINES.FIRST;
    while (!ITELINES.EOF) {
        if (ITELINES.ccCDiscPRC !== 0 || ITELINES.ccCDiscVAL !== 0) {
            vPis = 1;
            break;
        }
        ITELINES.NEXT;
    }

    //Σειρές που έχουν επιλεχθεί από τον cccsettings για αυτόματη έκδοση πιστωτικού.
    rset = X.GETSQLDATASET('select series,district,trdbusiness,priceseries,shipment,GroupYD,GroupMYD from cccsettings where company=' + X.SYS.COMPANY, null);
    rser = X.GETSQLDATASET('select SeriesCreSec from cccsettingslines where seriesinv=' + SALDOC.SERIES, null);

    if (rser.SeriesCreSec == '') {
        X.WARNING('Προσοχή δεν βρέθηκε Σειρά για το δεύτερο Πιστωτικό. Η ΔΙΑΔΙΚΑΣΙΑ ΕΚΔΟΣΗΣ ΔΕΥΤΕΡΟΥ ΠΙΣΤΩΤΙΚΟΥ ΘΑ ΔΙΑΚΟΠΕΙ!!!');
        return;
    }

    //Εάν σωστές Σειρές και προυποθέσεις τότε έκδοση πιστωτικού
    if (vPis == 1) {

        if (SALDOC.FINDOC < 0) {
            //Νέο Πιστωτικό
            iSALDOCID = X.NEWID();
        } else {
            //Διαγραφή πιστωτικών εάν έχουν εκδοθεί.
            //DeleteSalDoc(rser.SeriesCreSec);
            iSALDOCID = SALDOC.FINDOC;
        }

        vDate = X.EVAL('SQLDate(MTRDOC.SHIPDATE)');
        ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων');
        try {
            ObjSal.DBInsert;

            TblHeader = ObjSal.FindTable('FINDOC');
            TblDetail = ObjSal.FindTable('ITELINES');


            TblHeader.INSERT;
            TblHeader.SERIES = rser.SeriesCreSec;//rset.series;
            TblHeader.TRDR = SALDOC.TRDR;
            TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
            TblHeader.TRNDATE = SALDOC.TRNDATE;
            TblHeader.FINDOCS = iSALDOCID;
            TblHeader.COMMENTS = SALDOC.CMPFINCODE + ' - ' + SALDOC.TRDR_CUSTOMER_NAME;

            ITELINES.FIRST;
            while (!ITELINES.EOF) {
                if (ITELINES.ccCDiscPRC !== 0 || ITELINES.ccCDiscVAL !== 0) {
                    TblDetail.INSERT;
                    TblDetail.MTRL = ITELINES.MTRL;
                    TblDetail.QTY1 = ITELINES.QTY1;

                    if (ITELINES.ccCDiscPRC !== 0) {
                        TblDetail.PRICE = ITELINES.PRICE * (ITELINES.ccCDiscPRC / 100);
                    }

                    if (ITELINES.ccCDiscVAL !== 0) {
                        TblDetail.PRICE = ITELINES.ccCDiscVAL;
                    }

                    TblDetail.FINDOCS = iSALDOCID;
                    TblDetail.POST;

                }
                ITELINES.NEXT;
            }

            if (vPis == 1) {
                ans = X.ASK('Πιστωτικό τιμολόγιο', 'Θέλετε να γίνει αυτόματη δημιουργία δεύτερου πιστωτικού τιμολογίου?');
                if (ans == 6) {
                    ObjSal.DBPost;
                }
            }

        }
        catch (e) {
            X.WARNING('ON_AFTERPOST_CreateSecondCreditInv' + '\r\n' + e);
        }
        finally {
            ObjSal.FREE;
            ObjSal = null;
        }
    }
}

function ON_DELETE() {
    status = 3;
    DeleteSalDoc()
}

function DeleteSalDoc() {
    s = 'SELECT FINDOC FROM FINDOC WHERE SOSOURCE=1351 AND FINDOCS=' + SALDOC.FINDOC + ' ORDER BY SERIESNUM DESC';

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
    var vQtyA = 0;
    var vQtyMYD = 0;

    if (cmd == 150002) {
        //X.WARNING(SALDOC.FINDOC);
        CancelQTY1COV();
    }
    if (cmd == 150003) {
        //X.WARNING(SALDOC.FINDOC);
        CreateCarrierDoc(1, SALDOC.FINDOC);
    }

    if (cmd == 150001) {
        GetDiscountPrices()
        /*
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

        vDate = X.EVAL('SQLDate(MTRDOC.SHIPDATE)');

        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
            vWhouse = swh.whouse;

            if (vWhouse == '') {
                vWhouse = MTRDOC.WHOUSE;
            }
            //sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
            sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 1 = 1 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
            sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);

            //101 ΜΗ Υδατοδιαλυτά

            //MTRGROUP    cccSumGroup	Name
            //100 100	Υδατοδιαλυτά
            //100 103	Α' Υλες
            //100 200	Καμία
            //101 103	Α' Υλες
            //102 102	Ιχνοστοιχεία 

            if (sp4.igroup == 100) {//Υδατοδιαλυτά
                vQtyY = vQtyY + ITELINES.QTY1
            } else if (sp4.igroup == 103) {//Α' Υλες
                vQtyA = vQtyA + ITELINES.QTY1;
            } else if (sp4.igroup == 102) {//Ιχνοστοιχεία
                qty1 = ITELINES.QTY1;
                tr = X.GETSQLDATASET('select NUM04 from MTREXTRA where mtrl=' + ITELINES.MTRL, null);
                if (tr.NUM04 !== null && tr.NUM04 != 0) {
                    //Αναγωγή ton to litr
                    qty1 = qty1 * tr.NUM04;
                }
                vQtyI = vQtyI + qty1;
            } else if (sg.mtrgroup == 101 && sp4.igroup !== 103) { //ΜΗ Υδατοδιαλυτά και όχι Α' Υλες
                vQtyMYD = vQtyMYD + ITELINES.QTY1;
            }
            ITELINES.NEXT;
        }

        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 1 = 1 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
            sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);
            //X.WARNING(sg.mtrgroup);
            vGrList = rset.GroupYD + ',' + rset.GroupMYD;
            //vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');
            vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + rset.GroupMYD + ')');
            //X.WARNING(vGroupList);

            if (vGroupList == 1 && sp4.igroup !== 103) //ΜΗ Υδατοδιαλυτά
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
            }
            //else //if(sg.mtrgroup==rset.GroupYD)
            //{
            //    swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
            //    vWhouse = swh.whouse;
            //    if (vWhouse == '') {
            //        vWhouse = MTRDOC.WHOUSE;
            //    }
            //    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
            //    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            //    //X.WARNING('DISC STEP3: '+sp.dsc);
            //    if (sp.dsc != '') {
            //        vPis = 1;
            //    } else {
            //        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
            //        ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            //        //X.WARNING('DISC STEP4: '+sp.dsc);
            //        if (sp.dsc != '') {
            //            vPis = 1;
            //        }
            //    }

            //}
            ITELINES.NEXT;
        }

        try {
            ITELINES.FIRST;
            while (!ITELINES.EOF) {
                //if (ITELINES.NUM02 == '') {
                swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
                vWhouse = swh.whouse;

                if (vWhouse == '') {
                    vWhouse = MTRDOC.WHOUSE;
                }
                //sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
                sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 2=2 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
                //X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate);
                if (sp4.igroup == 100) {
                    scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                    if (scp.trdr != '') {
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP3 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                        if (sp.dsc == 0) {
                            sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                            //X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                        }
                    } else {

                        //Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                        //X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
                    }
                    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
                } else if (sp4.igroup == 103) {
                    scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                    if (scp.trdr != '') {
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP3 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                        if (sp.dsc == 0) {
                            sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                            //X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                        }
                    } else {

                        //Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                        //X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
                    }
                    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
                } else if (sp4.igroup == 102) {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyI + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP4 2nd loop group 102: '+sp.dsc+' qty: '+vQtyI);
                    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);

                } else if (sp4.igroup == 200) {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + ITELINES.QTY1 + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP4 2nd loop group 200: '+sp.dsc+' qty: '+vQtyI);
                    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
                }
                //sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
                //ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
                //}
                ITELINES.NEXT;
            }
        }
        catch (e) {
            X.WARNING(e);
        }
        finally {
            //ObjSal.FREE; 
            //ObjSal =null; 
        }


        //}
    }
    */

    }
}
function GetDiscountPrices() {
    var vQtyY = 0;
    var vQtyI = 0;
    var vQtyA = 0;
    var vQtyMYD = 0;

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

    //Not used
    //spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);


    //if(spis.RECORDCOUNT>0)
    //{

    vDate = X.EVAL('SQLDate(MTRDOC.SHIPDATE)');

    ITELINES.FIRST;
    while (!ITELINES.EOF) {
        //Διαβάζει whouse από το προηγούμενο παραστατικό πχ ΔΑ -> ΤΙΜ
        swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
        vWhouse = swh.whouse;
        //X.WARNING('swh.whouse-' + swh.whouse)

        //if Νέο Παραστατικό
        if (vWhouse == '') {
            vWhouse = MTRDOC.WHOUSE;
        }
        //X.WARNING('SALDOC.INT02-' + SALDOC.INT02)

        if (SALDOC.INT02 != 0) {
            vWhouse = SALDOC.INT02;
        }
        //X.WARNING('vWhouse-' + vWhouse)

        //sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
        //sosource=7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
        sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 1 = 1 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
        sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);

        //101 ΜΗ Υδατοδιαλυτά

        //MTRGROUP    cccSumGroup	Name
        //100 100	Υδατοδιαλυτά
        //100 103	Α' Υλες
        //100 200	Καμία
        //101 103	Α' Υλες
        //102 102	Ιχνοστοιχεία 

        if (sp4.igroup == 100) {//Υδατοδιαλυτά
            vQtyY = vQtyY + ITELINES.QTY1
        } else if (sp4.igroup == 103) {//Α' Υλες
            vQtyA = vQtyA + ITELINES.QTY1;
        } else if (sp4.igroup == 102) {//Ιχνοστοιχεία
            qty1 = ITELINES.QTY1;
            tr = X.GETSQLDATASET('select NUM04 from MTREXTRA where mtrl=' + ITELINES.MTRL, null);
            if (tr.NUM04 !== null && tr.NUM04 != 0) {
                //Αναγωγή ton to litr
                qty1 = qty1 * tr.NUM04;
            }
            vQtyI = vQtyI + qty1;
        } else if (sg.mtrgroup == 101 && sp4.igroup !== 103) { //ΜΗ Υδατοδιαλυτά και όχι Α' Υλες
            vQtyMYD = vQtyMYD + ITELINES.QTY1;
        }
        ITELINES.NEXT;
    }

    ITELINES.FIRST;
    while (!ITELINES.EOF) {
        //sosource=7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
        sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 1 = 1 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
        sg = X.GETSQLDATASET('select mtrgroup from mtrl where mtrl=' + ITELINES.MTRL, null);
        //X.WARNING(sg.mtrgroup);
        vGrList = rset.GroupYD + ',' + rset.GroupMYD;
        //vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');
        vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + rset.GroupMYD + ')');
        //X.WARNING(vGroupList);

        if (vGroupList == 1 && sp4.igroup !== 103) //ΜΗ Υδατοδιαλυτά
        {
            //Διαβάζει whouse από το προηγούμενο παραστατικό πχ ΔΑ -> ΤΙΜ
            swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
            vWhouse = swh.whouse;
            //X.WARNING('swh.whouse-' + swh.whouse)

            //if Νέο Παραστατικό
            if (vWhouse == '') {
                vWhouse = MTRDOC.WHOUSE;
            }
            //X.WARNING('SALDOC.INT02-' + SALDOC.INT02)

            if (SALDOC.INT02 != 0) {
                vWhouse = SALDOC.INT02;
            }
            //X.WARNING('vWhouse-' + vWhouse)

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
        }
        //else //if(sg.mtrgroup==rset.GroupYD)
        //{
        //    swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
        //    vWhouse = swh.whouse;
        //    if (vWhouse == '') {
        //        vWhouse = MTRDOC.WHOUSE;
        //    }
        //    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
        //    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
        //    //X.WARNING('DISC STEP3: '+sp.dsc);
        //    if (sp.dsc != '') {
        //        vPis = 1;
        //    } else {
        //        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
        //        ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
        //        //X.WARNING('DISC STEP4: '+sp.dsc);
        //        if (sp.dsc != '') {
        //            vPis = 1;
        //        }
        //    }

        //}
        ITELINES.NEXT;
    }

    try {
        ITELINES.FIRST;
        while (!ITELINES.EOF) {
            //if (ITELINES.NUM02 == '') {
            //Διαβάζει whouse από το προηγούμενο παραστατικό πχ ΔΑ -> ΤΙΜ
            swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
            vWhouse = swh.whouse;
            //X.WARNING('swh.whouse-' + swh.whouse)

            //if Νέο Παραστατικό
            if (vWhouse == '') {
                vWhouse = MTRDOC.WHOUSE;
            }
            //X.WARNING('SALDOC.INT02-' + SALDOC.INT02)

            if (SALDOC.INT02 != 0) {
                vWhouse = SALDOC.INT02;
            }
            //X.WARNING('vWhouse-' + vWhouse)

            //sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
            //sosource=7 Πιστωτική πολιτική Υδατοδιαλυτών - Ιχνοστοιχείων
            sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where 2=2 and d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL + ' and d.fromdate <= ' + vDate + ' and d.finaldate >= ' + vDate, null);
            //X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate);
            if (sp4.igroup == 100) {
                //sosource=5 Πιστωτική πολιτική Υδατοδιαλυτών
                scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                if (scp.trdr != '') {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP3 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                    if (sp.dsc == 0) {
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                    }
                } else {

                    //Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyY + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
                    //X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
                }
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            } else if (sp4.igroup == 103) {
                //sosource=5 Πιστωτική πολιτική Υδατοδιαλυτών
                scp = X.GETSQLDATASET('select l.trdr from cccpricelistlines l, cccpricelist h where l.cccpricelist=h.cccpricelist and h.sosource=5 and l.trdr=' + SALDOC.TRDR, null);
                if (scp.trdr != '') {
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP3 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                    if (sp.dsc == 0) {
                        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                        //X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                    }
                } else {

                    //Gm Αν προστίθεται cccSumGroup πρέπει να προστίθεται και στή fn_clDiscStep4
                    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyA + '\'' + ') AS dsc', null);
                    //X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
                    //X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
                }
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            } else if (sp4.igroup == 102) {
                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + vQtyI + '\'' + ') AS dsc', null);
                //X.WARNING('DISC STEP4 2nd loop group 102: '+sp.dsc+' qty: '+vQtyI);
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);

            } else if (sp4.igroup == 200) {
                sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + ITELINES.QTY1 + '\'' + ') AS dsc', null);
                //X.WARNING('DISC STEP4 2nd loop group 200: '+sp.dsc+' qty: '+vQtyI);
                ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
            }
            //sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
            //ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
            //}
            ITELINES.NEXT;
        }
    }
    catch (e) {
        X.WARNING(e);
    }
    finally {
        //ObjSal.FREE; 
        //ObjSal =null; 
    }

}


function CancelQTY1COV() {
    var ans;
    ans = X.ASK('Aκύρωση εκτελεσμένων', 'Προσοχή!!! θα ακυρωθούν οι εκτελεσμένες ποσότητες στα Δ.Εσωτ.Διακίνησης (Παρημίν)' + '\r\n' + 'Συνέχεια ? '); // 6=Yes, 7=No, 2=Cancel 
    if ((ans == 7) || (ans == 2)) {
        X.EXCEPTION('Η αποθήκευση ακυρώθηκε από το χρήστη');
        return;
    }

    Z = SALDOC.FINDOC;
    //X.WARNING(Z
    //Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος 
    SQL = 'SELECT FINDOCS,MTRLINES,LINENUM,QTY1 FROM MTRLINES WHERE FINDOC=' + Z
    ds = X.GETSQLDATASET(SQL, '');
    strIDs = X.EVAL('String(' + ds.FINDOCS + ')');

    ObjSal = X.CreateObj('SALDOC;Βασική προβολή πωλήσεων');

    ObjSal.DBLocate(strIDs);

    try {
        TblHeader = ObjSal.FindTable('FINDOC');
        TblDetail = ObjSal.FindTable('ITELINES');
        //TblHeader.EDIT;
        //TblDetail.EDIT;

        //Κατάσταση 1003 Παρημίν
        //7046 Δελτίο  Εσωτερικής Διακίνησης
        if (TblHeader.FPRMS == 7046 && TblHeader.FINSTATES == 1003 && TblHeader.ISCANCEL == 0) {
            //X.WARNING('ds.FINDOCS-' + ds.FINDOCS);
            //X.WARNING('strIDs-' + strIDs);
            //0  Μετασχηματισμός(Όχι)
            //1  Μετασχηματισμός(Πλήρως)
            //2  Μετασχηματισμός(Μερικώς)
            //3  Μετασχηματισμένο //Όταν υπάρχει ακόμα εκρεμότητα - άρση εκρεμότητος
            X.RUNSQL('UPDATE FINDOC SET FULLYTRANSF = 3 WHERE FINDOC = ' + TblHeader.FINDOC, null);

            TblDetail.FIRST;
            while (!TblDetail.EOF()) //Loop για τα παραστατικά που βρέθηκαν και διαγραφή τους 
            {
                //X.WARNING('TblDetail.QTY1COV-' + TblDetail.QTY1COV);
                X.RUNSQL('UPDATE MTRLINES SET QTY1COV = 0 ,PENDING = 1  WHERE FINDOC = ' + TblDetail.FINDOC + ' AND MTRLINES = ' + TblDetail.MTRLINES, null);
                //X.WARNING('TblDetail.QTY1COV-' + TblDetail.QTY1COV);
                //TblDetail.POST;
                TblDetail.NEXT;
            }
            //ObjSal.DBPost;
            X.WARNING('Ok');
        } else {
            X.WARNING('Λάθος Παρ/κό για ακύρωση εκτελεσμένων (η κατάσταση δεν είναι Παρημίν)');
        }
    }
    catch (e) {
        X.WARNING(e);
    }
    finally {
        ObjSal.FREE;
        ObjSal = null;
    }
}

function CreateCarrierDoc(ask, iSALDOCID) {
    var ans;
    //ans = X.ASK('Δημιουργία - Διόρθωση Προχρεώσεις μεταφορέων', 'Προσοχή!!! Θα δημιουργηθούν Προχρεώσεις μεταφορέων' + '\r\n' + 'Συνέχεια ? '); // 6=Yes, 7=No, 2=Cancel 
    //if ((ans == 7) || (ans == 2)) {
    //    X.EXCEPTION('Η αποθήκευση ακυρώθηκε από το χρήστη');
    //    return;
    //}
    var trdr;
    var findocs;

    //Find Μεταφορέα-Προμηθευτή
    //X.WARNING('MTRDOC.SOCARRIER=' + MTRDOC.SOCARRIER);
    ds = X.GETSQLDATASET('SELECT so.CODE FROM SOCARRIER AS so WHERE so.SOCARRIER = ' + MTRDOC.SOCARRIER, null);
    if (ds.RECORDCOUNT != 1) {
        X.WARNING('Προσοχή!!!. Δεν βρέθηκε Μεταφορέας. Η διαδικασία θα διακοπεί!!!');
        return;
    } else {
        ds = X.GETSQLDATASET('SELECT TRDR,CODE FROM TRDR AS t WHERE SODTYPE = 12 AND CODE = ' + '\'' + ds.CODE + '\'', null);
        if (ds.RECORDCOUNT != 1) {
            X.WARNING('Προσοχή!!!. Δεν βρέθηκε αντίστοιχος Προμηθευτής - Μεταφορέας. ' + ds.CODE + '\r\n' + ' Η αυτόματη έκδοση προχρέωσης μεταφορέα θα διακοπεί!!!');
            return;
        } else {
            trdr = ds.TRDR;
        }
    }
    //X.WARNING('MTRDOC.SOCARRIER_CODE=' + ds.CODE);

    Z = iSALDOCID;//SALDOC.FINDOC;
    //X.WARNING(Z)
    //Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος 
    SQL = 'SELECT mt.FINDOC, mt.FINDOCS, mt.MTRLINES, mt.LINENUM, mt.QTY1 FROM MTRLINES AS mt INNER JOIN  FINDOC ON mt.FINDOC = FINDOC.FINDOC WHERE (FINDOC.SOSOURCE = 1253) AND mt.FINDOCS = ' + Z
    ds = X.GETSQLDATASET(SQL, '');
    findocs = ds.FINDOC;
    //X.WARNING('findocs ' + findocs)
    //strIDs = X.EVAL('String(' + ds.FINDOC + ')');
    // LINSUPDOC.SERIES 

    //Ειδικές συναλλαγές προμηθευτών
    ObjLinSupDoc = X.CreateObj('LINSUPDOC;Ειδικές συναλλαγές προμηθευτών New');
    //if (ObjLinSupDoc === null){
    //    X.WARNING('ObjLinSupDoc == null');
    //}




    //X.WARNING('ObjLinSupDoc' + ObjLinSupDoc);// + ObjLinSupDoc);
    //return;
    try {
        if (findocs != 0) {
            ObjLinSupDoc.DBLocate(findocs);
        } else {
            ObjLinSupDoc.DBInsert;
        }


        TblHeader = ObjLinSupDoc.FindTable('FINDOC');
        TblDetail = ObjLinSupDoc.FindTable('LINLINES');//ITELINES');

        if (findocs == 0) {
            TblHeader.INSERT;
            TblHeader.SERIES = 8000;
            //TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
            TblHeader.TRNDATE = SALDOC.TRNDATE;
        }

        TblHeader.TRDR = trdr;//1624;//SALDOC.TRDR; ΓΚΑΤΖΟΥΛΗΣ ΧΡΗΣΤΟΣ  MTRDOC.SOCARRIER SOCARRIER.CODE
        //X.WARNING(trdr);
        TblHeader.FINDOCS = iSALDOCID;

        ITELINES.FIRST;
        first_MTRLINES = ITELINES.MTRLINES;
        tccCSHIPVALUE = ITELINES.ccCSHIPVALUE;
        tRemarks = "*======= " + TblHeader.FINCODE + " =======*" + '\r\n';
        tRemarks = tRemarks + 'Κωδικός' + "\t" + 'Ποσότ' + "\t" + 'Κόμιστρο' + '\r\n';
        while (!ITELINES.EOF) {
            if (!tccCSHIPVALUE == ITELINES.ccCSHIPVALUE) {
                X.WARNING('Λάθος κόμιστρο. Η διαδικασία θα διακοπεί!!!');
                return;
            }
            ds = X.GETSQLDATASET('select code from mtrl where mtrl=' + ITELINES.MTRL, null);
            tRemarks = tRemarks + ds.code + "\t" + ITELINES.QTY1 + "\t" + ITELINES.ccCSHIPVALUE + '\r\n';

            ITELINES.NEXT;
        }

        //Παρατηρήσεις
        TblHeader.REMARKS = tRemarks;

        //Ειδικές προμηθευτών  sosource=1253   

        mtrlNew = 0;
        //7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
        //7041	Δελτίο Αποστολής	Δελτίο αποστολής
        //7046	Δελτίο Αποστολής	Εσωτερική διακίνηση
        if (SALDOC.FPRMS == 7040 || SALDOC.FPRMS == 7046) {
            mtrlNew = 1818;	//64.07.05.0024	Έξοδα διακινήσ.εσωτ.υλικών-αγαθών με μεταφ.μέσα τρίτων με ΦΠΑ24%
        }
        if (SALDOC.FPRMS == 7041) {
            mtrlNew = 1816;	//64.07.04.0024	Έξοδα μεταφ.υλικών-αγαθών πωλήσεων με μετ.μέσα τρίτων με ΦΠΑ 24%
        }

        if (findocs == 0) {
            TblDetail.INSERT;
        }

        TblDetail.MTRL = mtrlNew;
        TblDetail.QTY1 = 1;
        TblDetail.LINEVAL = MTRDOC.ccCTOTSHIPVALUE;
        TblDetail.FINDOCS = iSALDOCID;
        TblDetail.MTRLINESS = first_MTRLINES;

        //Κωδικός	Περιγραφή	ΑΧ	Αριθμός ΑΧ
        //204	Κ.Δ Διαβατών Θεσ/κης	ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος	4
        //205	Κ.Δ Πύργου	ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	5
        //207	Κ.Δ Ασπροπύργου	ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος	8
        //208	Κ.Δ Φυτοθρεπτική	ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους	17
        //209	Κ.Δ Βαθύλακος	ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους	13
        //212	Κ.Δ Καβάλας	ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος	2,3


        //WHOUSE	NAME
        //2     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
        //3     212 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
        //4 	204 ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
        //5 	205 ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        //8 	207 ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
        //13	209 ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
        //17	208 ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους

        switch (parseInt(MTRDOC.WHOUSE)) {
            case 2://ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος
                TblDetail.COSTCNTR = 212;//Κ.Δ Καβάλας
                break;
            case 3://ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Εναπ.Υλ.Καθοδόν
                TblDetail.COSTCNTR = 212;//Κ.Δ Καβάλας
                break;
            case 4://ΥΠΜΑ ΙΩΝΙΑ ΘΕΣ/ΝΙΚΗΣ Κεντρ.Αποθ.Χώρος
                TblDetail.COSTCNTR = 204;//Κ.Δ Διαβατών Θεσ/κης
                break;
            case 5://ΠΡΑΣΙΝΟ ΙΑΡΔΑΝΟΥ ΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                TblDetail.COSTCNTR = 205;//Κ.Δ Πύργου
                break;
            case 8://ΘΕΣΗ ΜΑΥΡΗ ΩΡΑ - ΑΣΠΡΟΠΥΡΓΟΣ Κεντ.Αποθ.Χώρος
                TblDetail.COSTCNTR = 207;//Κ.Δ Ασπροπύργου
                break;
            case 13://ΙΜΕ ΒΑΘΥΛΑΚΟΣ Ε.Π.Ε Σε τρίτους
                TblDetail.COSTCNTR = 209;//Κ.Δ Βαθύλακος
                break;
            case 17://ΦΥΤΟΘΡΕΠΤΙΚΗ ΑΒΕΕ Σε τρίτους
                TblDetail.COSTCNTR = 208;//Κ.Δ Φυτοθρεπτική
        }

        TblDetail.POST;

        if (ask == 1) {
            ans = X.ASK('Δημιουργία - Διόρθωση Προχρεώσεις μεταφορέων', 'Δημιουργία - Διόρθωση Προχρεώσεις μεταφορέων' + '\r\n' + 'Συνέχεια ?');
            if (ans == 6) {
                ObjLinSupDoc.DBPost;
            }
        } else {
            ObjLinSupDoc.DBPost;
        }

    }
    catch (e) {
        X.WARNING('CreateCarrierDoc' + '\r\n' + e);
    }
    finally {
        ObjLinSupDoc.FREE;
        ObjLinSupDoc = null;
    }
}

function ON_ITELINES_QTY1() {

    var vHouse = MTRDOC.WHOUSE;
    var vBal = 0;
    if (SALDOC.TFPRMS == 101 || SALDOC.TFPRMS == 103) {
        if (ITELINES.QTY1 > 0) {
            vBal = X.EVAL('FRemQty1PerWHouse(ITELINES.MTRL,ITELINES.WHOUSE,SALDOC.TRNDATE)');
            if (ITELINES.QTY1 > vBal) {
                X.WARNING('ΥΠΕΡΒΑΣΗ ΥΠΟΛΟΙΠΟΥ ΑΧ ΓΙΑ ΤΗΝ ΣΥΓΚΕΚΡΙΜΕΝΗ ΗΜΕΡΟΜΗΝΙΑ!');
            }
        }
    }
}

function DelConvertedDocs()//--- Βρίσkει και διαγράφει τα μετασχηματισμένα παραστατικά. 
{
    //Query για εύρεση των μετασχηματισμένων παραστατικών βάσει του τρέχοντος 
    strqry = 'SELECT DISTINCT FINDOC AS FINDOC FROM MTRLINES WHERE SOSOURCE=1351 ' + 'AND COMPANY=' + X.SYS.COMPANY + ' AND FINDOCS=' + SALDOC.FINDOC;
    ds = X.GETSQLDATASET(strqry, null);
    ds.FIRST;
    while (!ds.EOF())//Loop για τα παραστατικά που βρέθηκαν και διαγραφή τους 
    {
        ObjConv = X.CreateObj('SALDOC');//Δημοιυργία Object Πωλήσεων 
        try {
            ObjConv.DBLocate(ds.FINDOC);//Locate στην εγγραφή 
            //ObjConv.DBDelete;//Διαγραφή εγγραφής 
        } finally {
            ObjConv.FREE;
            ObjConv = null;
        }
        ds.NEXT;
    }
}


//var TAB = "\t";
//var CR = "\r";
//var LF = "\n";
//var CRLF = "\r\n";
//var FF = "\f";
//var DQUOTE = '\"';
//var SQUOTE = "\'";
//var BACKSLASH = "\\";
//var BACKSPACE = "\b";