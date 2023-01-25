//Τιμοκατάλογος Νομού
//Βασική προβολή κανόνα Α

function ON_AFTERPOST(){

    u=X.RUNSQL('update cccpricelist set upddate=getdate(), upduser='+X.USER+' where cccpricelist='+cccPriceList.cccPriceList,null);
}

function ON_cccPriceList_V1(){
	
    vFilter='{cccPriceListLines.Mtrl_ITEM_CODE}='+String.fromCharCode(39)+cccPriceList.V1+String.fromCharCode(39);

    if(cccPriceList.V1=='')
    {
        cccPriceListLines.FILTERED=0;
    }else
    {
        cccPriceListLines.FILTER='('+vFilter+')';
        cccPriceListLines.FILTERED=1;
    }
}

function ON_cccPriceList_V2(){
	
    vFilter='{cccPriceListLines.District1}='+String.fromCharCode(39)+cccPriceList.V2+String.fromCharCode(39);

    if(cccPriceList.V1!='')
    {
        vFilter=vFilter+' and '+'{cccPriceListLines.Mtrl_ITEM_CODE}='+String.fromCharCode(39)+cccPriceList.V1+String.fromCharCode(39);
    }

    if(cccPriceList.V2=='')
    {
        cccPriceListLines.FILTERED=0;
    }else
    {
        cccPriceListLines.FILTER='('+vFilter+')';
        cccPriceListLines.FILTERED=1;
    }
}

function ON_cccPriceList_V3(){
	
    vFilter='{cccPriceListLines.Whouse}='+String.fromCharCode(39)+cccPriceList.V3+String.fromCharCode(39);

    if(cccPriceList.V1!='')
    {
        vFilter=vFilter+' and '+'{cccPriceListLines.Mtrl_ITEM_CODE}='+String.fromCharCode(39)+cccPriceList.V1+String.fromCharCode(39);
    }
    if(cccPriceList.V2!='')
    {
        vFilter=vFilter+' and '+'{cccPriceListLines.District1}='+String.fromCharCode(39)+cccPriceList.V2+String.fromCharCode(39);
    }
    if(cccPriceList.V3=='')
    {
        cccPriceListLines.FILTERED=0;
    }else
    {
        cccPriceListLines.FILTER='('+vFilter+')';
        cccPriceListLines.FILTERED=1;
    }
}

function ON_POST(){
    cccPriceListLines.FILTERED=0;
}

function ON_CANCEL(){
    cccPriceListLines.FILTERED=0;
}

//Τιμοκατάλογος πελάτη
//Βασική προβολή κανόνα Β

function ON_AFTERPOST(){

    u=X.RUNSQL('update cccpricelist set upddate=getdate(), upduser='+X.USER+' where cccpricelist='+cccPriceList.cccPriceList,null);
}


function ON_cccPriceList_V1(){
	
    vFilter='{cccPriceListLines.Mtrl_ITEM_CODE}='+String.fromCharCode(39)+cccPriceList.V1+String.fromCharCode(39);

    if(cccPriceList.V1=='')
    {
        cccPriceListLines.FILTERED=0;
    }else
    {
        cccPriceListLines.FILTER='('+vFilter+')';
        cccPriceListLines.FILTERED=1;
    }
	
}

function ON_cccPriceList_V2(){
	

    vFilter='{cccPriceListLines.Trdr_CUSTOMER_CODE}='+String.fromCharCode(39)+cccPriceList.V2+String.fromCharCode(39);
    if(cccPriceList.V1!='')
    {
        vFilter=vFilter+' and '+'{cccPriceListLines.Mtrl_ITEM_CODE}='+String.fromCharCode(39)+cccPriceList.V1+String.fromCharCode(39);
    }

    if(cccPriceList.V2=='')
    {
        cccPriceListLines.FILTERED=0;
    }else
    {

        cccPriceListLines.FILTER='('+vFilter+')';
        cccPriceListLines.FILTERED=1;
    }
}

function ON_POST(){
    cccPriceListLines.FILTERED=0;
}

function ON_CANCEL(){
    cccPriceListLines.FILTERED=0;
}

//Τιμοκατάλογος έκπτωσης παραλήπτη
//Βασική προβολή κανόνα Γ

function ON_AFTERPOST(){

    u=X.RUNSQL('update cccpricelist set upddate=getdate(), upduser='+X.USER+' where cccpricelist='+cccPriceList.cccPriceList,null);
}
