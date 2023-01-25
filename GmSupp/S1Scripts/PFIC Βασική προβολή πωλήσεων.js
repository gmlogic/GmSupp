//Last Modified 21/09/2017 13:23

function ON_LOCATE() {

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
        //X.SETPROPERTY('FIELD', 'ITELINES.CCCSHIPVALUE', 'VISIBLE', 'FALSE');

        //X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE');
    }
}

function ON_NEW() {


    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        X.SETPROPERTY('FIELD', 'ITELINES.PRICE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.DISC1PRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.LINEVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.NUM02', 'VISIBLE', 'FALSE');
        //gm
        //X.SETPROPERTY('FIELD', 'ITELINES.CCCSHIPVALUE', 'VISIBLE', 'FALSE');

        //X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE');
    }
}

//Gm
function ON_POST() {

    if (X.SYS.GROUPS == 104) {
        //7040	Δελτίο Διακίνησης	Εσωτερική διακίνηση
        //7041	Δελτίο Αποστολής	Δελτίο αποστολής
        //7042	Δελτίο Αποστολής (Απ.Λιαν.)	Δελτίο αποστολής
        //7043	Δελτίο Αποστολής Απο Πελάτη	Δελτίο επιστροφής
        //7045	Δ Ποσοτικής Παραλαβής	Δελτίο ποσοτικής παραλαβής
        //7046	Δελτίο Αποστολής	Εσωτερική διακίνηση

        if (FINDOC.FPRMS == 7040 || FINDOC.FPRMS == 7041 || FINDOC.FPRMS == 7046)
            if (!SALDOC.FINSTATES) {
                X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Κατάσταση:]');
            }
        if (!SALDOC.SHIPKIND) {
            X.EXCEPTION('Προσοχή !!! Δεν βάλατε [Διακίνηση:]');
        }
        //X.EXCEPTION(typeof SALDOC.FINSTATES + '-' + SALDOC.FINSTATES + '-' + SALDOC.SHIPKIND);
    }
}



function ON_RESTOREEVENTS() {

    if (X.SYS.GROUPS == 108 || X.SYS.GROUPS == 109 || X.SYS.GROUPS == 110 || X.SYS.GROUPS == 104 || X.SYS.GROUPS == 102) {
        X.SETPROPERTY('FIELD', 'ITELINES.PRICE', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.DISC1PRC', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.LINEVAL', 'VISIBLE', 'FALSE');
        X.SETPROPERTY('FIELD', 'ITELINES.NUM02', 'VISIBLE', 'FALSE');
        //gm
        //X.SETPROPERTY('FIELD', 'ITELINES.CCCSHIPVALUE', 'VISIBLE', 'FALSE');

        //X.SETPROPERTY('PANEL', 'Panel702', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'Panel2', 'VISIBLE', 'FALSE');
        //X.SETPROPERTY('PANEL', 'N_225833848', 'VISIBLE', 'FALSE');
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
}
