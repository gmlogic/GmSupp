function ON_POST() {
    try {
        var ans;
        ans = X.ASK('Μεταβολή Παραστατικών', 'Προσοχή!!! Μεταβολή παραστατικού' + '\r\n' + 'Συνέχεια ? '); // 6=Yes, 7=No, 2=Cancel 
        if ((ans == 7) || (ans == 2)) {
            X.EXCEPTION('Η αποθήκευση ακυρώθηκε από το χρήστη');
            return;
        }
        if (ccCDocsUpd.findoc != '') {
            if (ccCDocsUpd.trucksno != '') {
                X.RUNSQL('update mtrdoc set trucksno=' + '\'' + ccCDocsUpd.trucksno + '\'' + ' where findoc=' + ccCDocsUpd.findoc, null);
                X.RUNSQL('update findoc set upddate = getdate(), upduser=' + X.USER + ' where findoc=' + ccCDocsUpd.findoc, null);
            }
            if (ccCDocsUpd.DriverName != '') {
                X.RUNSQL('update findoc set varchar02=' + '\'' + ccCDocsUpd.DriverName + '\'' + ', upddate = getdate(), upduser=' + X.USER + ' where findoc=' + ccCDocsUpd.findoc, null);
            }
            if (ccCDocsUpd.remarks != '') {
                X.RUNSQL('update findoc set remarks=' + '\'' + ccCDocsUpd.remarks + '\'' + ', upddate = getdate(), upduser=' + X.USER + ' where findoc=' + ccCDocsUpd.findoc, null);
            }
            X.RUNSQL('truncate table ccCDocsUpd', null);
        }
    }
    catch (e) {
        X.WARNING(e);
    }
}