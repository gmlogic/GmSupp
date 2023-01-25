//Gm
function ON_POST() {
    if (PURDOC.FPRMS == 2021) { //Παραγγελία Σε Προμηθευτή
        //PURDOC.FINSTATES
        //3	ΑΚΥΡΩΘΗΚΕ
        //5	ΑΚΥΡΩΣΗ ΛΟΓΩ ΜΗ ΑΠΑΝΤΗΣΗΣ
        //6	ΑΚΥΡΩΣΗ ΛΟΓΩ ΜΗ ΔΙΑΘΕΣΙΜΟΤΗΤΑΣ
        if (PURDOC.FINSTATES == 3 || PURDOC.FINSTATES == 5 || PURDOC.FINSTATES == 6) {
            var ans;
            ans = X.ASK('Aρση εκκρεμότητας όλων των γραμμών', 'Προσοχή !!! Θα γίνει άρση εκκρεμότητας όλων των γραμμών' + '\r\n' + 'Συνέχεια ? '); // 6=Yes, 7=No, 2=Cancel 
            // 6=Yes, 7=No, 2=Cancel
            if  (ans == 2) {
                X.EXCEPTION('Η αποθήκευση ακυρώθηκε από το χρήστη');
                return;
            }
            if (ans == 6)  {
                ITELINES.FIRST;
                while (!ITELINES.EOF) {
                    ITELINES.QTY1CANC = ITELINES.QTY1;
                    ITELINES.NEXT;
                }
            }

        }
    }
}