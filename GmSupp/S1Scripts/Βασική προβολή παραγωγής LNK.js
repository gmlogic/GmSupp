var vH = 0;

function ON_LOCATE() {
    vH = PRDDOC.NUM01;
}

function ON_POST() {

    if (MTRDOC.SPCS_SPCPRD_CODE == '') {
        X.EXCEPTION('Προσοχή!!! Απαγορεύετε η καταχώρηση' + '\r\n' + 'Παραγωγής άνευ Προδιαγραφής');
        return;
    }

    var Hours;
    var FinHours;
    var ans;

    if (PRDDOC.NUM01 == '') {
        Hours = Math.abs(PRDDOC.cccToDate - PRDDOC.cccFromDate);
        FinHours = (((Hours / 60) / 60)) / 1000;
        //X.WARNING(FinHours);
        PRDDOC.NUM01 = FinHours;
    } else {
        Hours = Math.abs(PRDDOC.cccToDate - PRDDOC.cccFromDate);
        FinHours = (((Hours / 60) / 60)) / 1000;
        if (vH != FinHours) {
            ans = X.ASK('Ώρες λειτουργίας', 'Διαφοροποίηση ωρών κέντρου εργασίας. Να ξαναγίνει ενημέρωση των ωρών λειτουργίας;');
            if (ans == 6) {
                PRDDOC.NUM01 = FinHours;
            }
        }
    }
}

function ON_PRDDOC_INT01() {

    if (PRDDOC.INT01 != '') {
        sanal = X.GETSQLDATASET('select findoc,spcost from spcostanal where findoc=' + PRDDOC.FINDOC, null);
        if (sanal.RECORDCOUNT > 0) {
            SPCOSTANAL.FIRST;
            while (!SPCOSTANAL.EOF) {
                SPCOSTANAL.DELETE;
            }
            SPCOSTANAL.DELETE;
        }
        //SPCOSTANAL.POST;
        scost = X.GETSQLDATASET('select spcost from spcost where company=1001 and spcostgroup=' + PRDDOC.INT01 + ' order by spcost', null);
        scost.FIRST;
        while (!scost.EOF) {
            SPCOSTANAL.INSERT;
            SPCOSTANAL.SPCOST = scost.spcost;
            SPCOSTANAL.SOVAL = PRDDOC.NUM01;
            scost.NEXT;
        }
        SPCOSTANAL.POST;
        SPCOSTANAL.FIRST;
    }
}