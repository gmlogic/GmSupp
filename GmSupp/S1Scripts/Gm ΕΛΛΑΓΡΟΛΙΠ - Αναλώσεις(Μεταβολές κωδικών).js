function ON_PRDDOC_INT02() {
    try {
        if (PRDDOC.INT02 == 0) {
            //X.WARNING('Πατήθηκε no');
            return;
        }
        if (PRDDOC.INT02 != '' && PRDDOC.INT02 == 1) {
            //X.WARNING('Πατήθηκε yes');
            //return;
            MPRDLINES.FIRST;
            while (!MPRDLINES.EOF) {
                /*
                MPRDLINES.MTRL_ITEM_CODE
                X.WARNING(MPRDLINES.MTRL);
                MPRDLINES.MTRL = 2187;
               
                X.WARNING(MPRDLINES.MTRL);
               
                1904	2187
                1906	4713
                1997	4714

                5768	7183
                5770	7184
                5765	7185

                */

                /*
                if (MPRDLINES.MTRL == 1904) {
                    MPRDLINES.MTRL = 2187;
                    PRDDOC.INT02 = 2;
                }
                if (MPRDLINES.MTRL == 1906) {
                    MPRDLINES.MTRL = 4713;
                    PRDDOC.INT02 = 2;
                }
                if (MPRDLINES.MTRL == 1997) {
                    MPRDLINES.MTRL = 4714;
                    PRDDOC.INT02 = 2;
                }

                */
                if (MPRDLINES.MTRL == 5768) {
                    MPRDLINES.MTRL = 7183;
                    PRDDOC.INT02 = 2;
                }
                if (MPRDLINES.MTRL == 5770) {
                    MPRDLINES.MTRL = 7184;
                    PRDDOC.INT02 = 2;
                }
                if (MPRDLINES.MTRL == 5765) {
                    MPRDLINES.MTRL = 7185;
                    PRDDOC.INT02 = 2;
                }

                MPRDLINES.NEXT;
            }
        } else {
            //X.CANCELEDITS;
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

