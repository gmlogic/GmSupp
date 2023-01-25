//MTRDOC.WHOUSE
//MPRDLINES.WHOUSE

function ON_MPRDLINES_POST() {

    //if (MPRDLINES.WHOUSE != MTRDOC.WHOUSE){
    //X.WARNING(MTRDOC.WHOUSE + ' ' + MPRDLINES.WHOUSE);
    //X.EXCEPTION('Προσοχή!!! Λάθος αποθηκευτικός χώρος');
    //PRDDOC.FINDOC_DPRDLINES_MTRTYPE 
    //PRDDOC.FINDOC_DPRDLINES_LINENUM 
    //}
}

//Εμφάνιση ερώτησης για αποθήκευση εγγραφής. 
function ON_POST() {
    //var ans; 
    //ans = X.ASK('Επιβεβαίωση αποθήκευσης', 'Συνέχεια ? '); // 6=Yes, 7=No, 2=Cancel 
    //if ((ans == 7) || (ans == 2)) {
    //	X.EXCEPTION('Η αποθήκευση ακυρώθηκε από το χρήστη'); 
    //	}else { 
    //	X.WARNING('Πατήθηκε Yes'); 
    //	} 

    if (MTRDOC.SPCS_SPCPRD_CODE == '') {
        X.EXCEPTION('Προσοχή!!! Απαγορεύετε η καταχώρηση' + '\r\n' + 'Παραγωγής άνευ Προδιαγραφής');
        return;
    }



    MPRDLINES.FIRST;
    while (!MPRDLINES.EOF) {
        //X.WARNING(MTRDOC.WHOUSE + ' ' + MPRDLINES.WHOUSE);
        if (MPRDLINES.WHOUSE != MTRDOC.WHOUSE) {
            //X.WARNING(MTRDOC.WHOUSE + ' ' + MPRDLINES.WHOUSE);
            X.EXCEPTION('Προσοχή!!! Λάθος αποθηκευτικός χώρος');
            //PRDDOC.FINDOC_DPRDLINES_MTRTYPE 
            //PRDDOC.FINDOC_DPRDLINES_LINENUM 

        }


        if (MPRDLINES.QTY1 > 0) {
            var vHouse = MTRDOC.WHOUSE;
            var vBal = 0;
            vBal = X.EVAL('FRemQty1PerWHouse(MPRDLINES.MTRL,MPRDLINES.WHOUSE,PRDDOC.TRNDATE)');
            if (MPRDLINES.QTY1 > vBal) {
                mtrxb4 = X.GETSQLDATASET('select bool04 from mtrextra as mx where mx.mtrl=' + MPRDLINES.MTRL, null);
                //X.WARNING(mtrxb4.bool04);
                if (mtrxb4.bool04 == 1) {
                    X.EXCEPTION('Προσοχή!!! Απαγορεύετε η ανάλωση με είδος χωρίς υπόλοιπο');
                }
            }
        }
        //sp4=X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl='+ITELINES.MTRL,null);
        //if(sp4.igroup==100)
        //{
        //	vQtyY=vQtyY+ITELINES.QTY1;
        //}else if(sp4.igroup==102)
        //{
        //	vQtyI=vQtyI+ITELINES.QTY1;
        //}
        MPRDLINES.NEXT;
    }
}

function ON_MPRDLINES_QTY1() {

    var vHouse = MTRDOC.WHOUSE;
    var vBal = 0;
    //if(SALDOC.TFPRMS==101 || SALDOC.TFPRMS==103)
    //{
    if (MPRDLINES.QTY1 > 0) {
        vBal = X.EVAL('FRemQty1PerWHouse(MPRDLINES.MTRL,MPRDLINES.WHOUSE,PRDDOC.TRNDATE)');
        if (MPRDLINES.QTY1 > vBal) {
            if (!(X.SYS.USER == 15)) //WAREHOUSEMA
            {
                X.WARNING('ΥΠΕΡΒΑΣΗ ΥΠΟΛΟΙΠΟΥ ΑΧ ΓΙΑ ΤΗΝ ΣΥΓΚΕΚΡΙΜΕΝΗ ΗΜΕΡΟΜΗΝΙΑ!');
            }
            //mtrxbool04 = X.GETSQLDATASET('select bool04 from mtrextra as mx where mx.mtrl=' + MPRDLINES.MTRL, null);
            //if (mtrxbool04 = 1) {
            //    X.EXCEPTION('Προσοχή!!! Απαγορεύετε η ανάλωση με είδος χωρίς υπόλοιπο');
            //}
        }
    }
    //}
}

function ON_AFTERPOST() {

    var Z;

    if (PRDDOC.FINDOC < 0) {
        Z = X.NEWID();
        X.RUNSQL('update findoc set origin=22 where sosource=7151 and findoc=' + Z, null);
    }
}

function EXECCOMMAND(cmd) {
    var vFlag = 0;
    if (cmd == 150010) {
        if (PRDDOC.FINDOC > 0) {
            vR = X.GETSQLDATASET('select origin from findoc where findoc=' + PRDDOC.FINDOC, null);
            vFlag = vR.origin;
            if (vFlag == 22) {
                X.RUNSQL('update findoc set origin=1 where sosource=7151 and findoc=' + PRDDOC.FINDOC, null);
            } else if (vFlag == 1) {
                X.RUNSQL('update findoc set origin=22 where sosource=7151 and findoc=' + PRDDOC.FINDOC, null);
            }
        }
    }
}



function ON_PRDDOC_UFTBL01() {

    if (PRDDOC.INT01 != '') {

        //SPCOSTANAL.FIRST;
        //while(!SPCOSTANAL.EOF)
        //{
        //	SPCOSTANAL.DELETE;
        //}
        //SPCOSTANAL.DELETE;
        //SPCOSTANAL.FIRST;
        //while(!SPCOSTANAL.EOF)
        //{
        SPCOSTANAL.INSERT;
        SPCOSTANAL.SPCOST = 1000;
        SPCOSTANAL.SOVAL = PRDLINE.QTY1;
        //	scost.NEXT;
        //}
        SPCOSTANAL.POST;
        SPCOSTANAL.FIRST;
    }
}

function ON_MPRDLINES_ccCQTY1PRO() {

    if (MPRDLINES.ccCQTY1PRO == 99) {
        X.WARNING(MPRDLINES.MTRL);
    }
}