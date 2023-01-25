//Dealer DAY-ONE
//Creation date 29-11-2017
//Modify date 26-11-2020


Form
{
    [TABLES]
    ImpTable =;;;; Master; 3; 0

    [ImpTable]
    vImpOK = 2; 15; 0; 1; 0; Επιβεβαίωση Εργασίας; $Y;; 1;
    vSeries = 3; 20; 1; 1; 0; Σειρά δαπανών; SERIES(F[SOSOURCE = 1253]);;;
    vSupplier = 3; 20; 1; 1; 0; Προμηθευτής; SUPPLIER;;;
    vFromDate = 11; 12; 1; 1; 0; Από ημ / νία;;;;
    vToDate = 11; 12; 1; 1; 0; Έως ημ / νία;;;;
    vCompany = 3; 20; 1; 1; 1; Εταιρία; COMPANY;; 1001;
    vImpMess = 16; 64000; 0; 1; 1; Μηνύματα μεταφοράς...;;;;



    [PANELS]
    PANEL11 = 0;; 0; 50
    PANEL12 = 4; Μηνύματα μεταφοράς...; 0; 100


    [PANEL11]
    ImpTable.vImpOK
    ImpTable.vSeries
    ImpTable.vSupplier
    ImpTable.vCompany
    ImpTable.vFromDate
    ImpTable.vToDate


    [PANEL12]
    ImpTable.vImpMess


    [STRINGS]

}


Converter ConvCountry(COUNTRY, SHORTCUT, COUNTRY);
Converter ConvCurrency(SOCURRENCY, SHORTCUT, SOCURRENCY);
//Converter ConvItem     (MTRL,      'COMPANY;CODE;SODTYPE=51', MTRL);
Converter ConvSeries(SERIES, 'CODE;SOSOURCE=1351', SERIES);
Converter ConvMtrLot(MTRLOT, 'CODE;MTRL', MTRLOT);
Converter ConvCustomer(TRDR, 'COMPANY;CODE;SODTYPE=13', TRDR);
Converter ConvSalesman(PRSN, 'COMPANY;CODE;SODTYPE=20', PRSN);
Converter ConvPayment(PAYMENT, SHORTCUT, PAYMENT);
Converter ConvItem(MTRL, 'COMPANY;CODE;SODTYPE=53', MTRL);

var
    vTmp, vSeries, vBranch, xdebug, vMtrl, vTrdr;


Import ImpDoc(sSupDoc, sItelines) into 'LINSUPDOC,IMPORT:1'
{
    Findoc sSupDoc
    {
        SERIES =: ImpTable.vSeries;
        TRDR =: ImpTable.vSupplier;
        TRNDATE = sSupDoc.trndate;
        FINCODE = sSupDoc.fincode;
        VATSTS = sSupDoc.vatsts;
        //VATSTS=sSupDoc.vatsts;
    }

    LinLines sItelines
    {
        MTRL = ConvItem(: X.SYS.COMPANY, sItelines.CodeExp);
        QTY1 = 1;
        LINEVAL = sItelines.lineval;
        COSTCNTR = sItelines.BU;
        COMMENTS1 = sItelines.mcode;
    }
}


Connect DBDriver DocData   {
    //      Driver, DBase, ServerDB, User, Password, DataBaseName     
    connect('XADODrv.bpl', 'MSSQL', '192.168.12.201,55555', 'gm', '1mgergm++', 'Revera');
    //connect ('XADODrv.bpl', 'MSSQL', 'PC-USER10', 'sa', 'softone', 'Centrofaro');   
    //trdr=38236 //25353
    sCheck = select DISTINCT m.MTRL, mt.code, mt.name
    FROM dbo.FINDOC AS f INNER JOIN
    dbo.MTRLINES AS m ON f.FINDOC = m.FINDOC INNER JOIN
    dbo.MTRL AS mt ON m.MTRL = mt.MTRL LEFT OUTER JOIN
    dbo.cccMultiCompCC AS k INNER JOIN
    dbo.cccTrdDep AS d ON k.CostCenterCode = d.Code ON m.cccTrdDep = d.cccTrdDep
    where f.trdr = 38236
    and f.sosource = 1351
    and f.tfprms in (102, 103)
    and f.trndate >=: ImpTable.vFromdate
    and f.trndate <=: ImpTable.vToDate
    and m.mtrl not in (select mtrl from cccMultiCompData where CompanyT =: ImpTable.vCompany);


    sExpCheck = SELECT distinct c.CodeExp
    FROM         cccMultiCompData AS c INNER JOIN
    cccMultiCompCC AS k INNER JOIN
    cccTrdDep AS d ON k.CostCenterCode = d.Code ON c.CompanyT = k.CompanyT RIGHT OUTER JOIN
    FINDOC AS f INNER JOIN
    MTRLINES AS m ON f.FINDOC = m.FINDOC INNER JOIN
    MTRL AS mt ON m.MTRL = mt.MTRL ON c.mtrl = m.MTRL  AND d.cccTrdDep = m.cccTrdDep
    where f.trdr = 38236
    and f.sosource = 1351
    and f.tfprms in (102, 103)
    and f.trndate >=: ImpTable.vFromdate
    and f.trndate <=: ImpTable.vToDate
    and m.mtrl not in (select mtrl from cccMultiCompData where CompanyT = 1001);

    sSupDoc = select distinct f.findoc,
        f.fincode,
        f.trndate,
        f.vatsts
    from findoc f
    where f.trndate >=: ImpTable.vFromdate
    and f.trndate <=: ImpTable.vToDate
    and f.trdr = 38236
    and f.sosource = 1351
    and f.tfprms in (102, 103)
    and f.company = 4000; //3000


    sItelines = SELECT      c.CompanyT, m.MTRL, c.CodeExp, m.LINEVAL, mt.CODE AS mcode,
        (SELECT        CostCenterComp
    FROM            cccMultiCompCC
    WHERE(CostCenterCode = d.Code) AND(CompanyT = 1001)) AS BU
    FROM            FINDOC AS f INNER JOIN
    MTRLINES AS m ON f.FINDOC = m.FINDOC INNER JOIN
    MTRL AS mt ON m.MTRL = mt.MTRL INNER JOIN
    cccMultiCompData AS c ON m.MTRL = c.mtrl LEFT OUTER JOIN
    cccTrdDep AS d ON m.cccTrdDep = d.cccTrdDep
    WHERE(f.FINDOC = : sSupDoc.findoc) AND(c.CompanyT = 1001);

}

connect Xplorer Softone {
    connect();


}



var
    XX, x, z, vTot, vRowCancel, UserResp, vMess, vCurRec, vDoc, vCnt, SQL, vCus, vCusCount, vRes, vVat, vSum, vS, vLvat, vCode, vExp;

{
    if (: ImpTable.vImpOk = 0)
    {
        UserResp = SendResponse(0, 0, 0, 'Επιλέξτε επιβεβαίωση (Ναί)...', 'RESULTS.TOTREC;RESULTS.CURREC;RESULTS.CANREC;RESULTS.LABELTEXT');
    }else
    {

        vMess = vMess + '=================== Έναρξη εργασίας =================' + #13 + #10;
        UserResp = SendResponse(vMess, 'ImpTable.vImpMess');


        vCurRec = 0;
        vTot = 0;
        vRowCancel = 0;

        fetch sCheck
        {
            x = ExecSql('DocData', 'insert into cccMultiCompData (mtrl,companyt) values (' + VarToStr(sCheck.mtrl) + ',' + VarToStr(: ImpTable.vCompany) + ')', null);
            vMess = vMess + 'Το είδος: ' + VartoStr(sCheck.code) + ' ' + VarToStr(sCheck.name) + ' δεν είναι καταχωρημένο στην αντιστοίχιση δαπανών και έγινε καταχώρησή του' +#13 +#10;
            x = SendResponse(vMess, 'ImpTable.vImpMess');
        }

        fetch sExpCheck
        {
            SQL = 'select count(*) from mtrl where code=' + QuotedStr(VarToStr(sExpCheck.CodeExp));
            vExp = GetQueryResults('Softone', SQL, null);
            if (vExp = 0) {
                vMess = vMess + 'Ο κωδικός χρεωπίστωσης: ' + VartoStr(sExpCheck.CodeExp) + ' δεν είναι καταχωρημένος' +#13 +#10;
            }
        }

        fetch sSupDoc
        {
            vDoc = sSupDoc.fincode;
            SQL = 'select count(*) from findoc where fincode=' + QuotedStr(VarToStr(vDoc)) + ' and trdr=' + VarToStr(: ImpTable.vSupplier) + ' and fiscprd = ' + VarToStr(YearOfDate(: ImpTable.vFromdate));
            //SQL='select count(*) from findoc where fincode='+QuotedStr(VarToStr(vDoc))+' and trdr='+VarToStr(:ImpTable.vSupplier);
            vRes = GetQueryResults('Softone', SQL, null);

            if (vRes > 0) {
                vCurRec = vCurRec + 1;
                vMess = vMess + 'Το Παραστατικό: ' + VartoStr(sSupDoc.fincode) + ' είνει ήδη καταχωρημένο' +#13 +#10;
                x = SendResponse(vMess, 'ImpTable.vImpMess');
            } else {

                vTot = vTot + 1;
                ImpDoc(sSupDoc, sItelines);
                if (ImportError = 0) {
                    vCurRec = vCurRec + 1;
                    vMess = vMess + 'Το Παραστατικό: ' + VartoStr(sSupDoc.fincode) + ' καταχωρήθηκε' +#13 +#10;

                } else {

                    vRowCancel = vRowCancel + 1;
                    vMess = vMess + 'Το Παραστατικό: ' + VartoStr(sSupDoc.fincode) + ' δεν καταχωρήθηκε' +#13 +#10;
                }
                x = SendResponse(vTot, vCurRec, vRowCancel, vMess, 'RESULTS.TOTREC;RESULTS.CURREC;RESULTS.CANREC;ImpTable.vImpMess');
            }
        }
        vMess = vMess + '=================== Λήξη εργασίας ===================' + #13 + #10;
        UserResp = SendResponse(vMess, 'ImpTable.vImpMess');
    }
}