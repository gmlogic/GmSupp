Imports System.ComponentModel
Imports System.Data.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Transactions
'Imports GmSupp.Hglp
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
'Imports NPOI.HPSF
'Imports NPOI.HSSF.UserModel
'Imports Microsoft.Office.Interop
Imports Softone
Imports System.Reflection

Public Class WhousesBR
#Region "01-Declare Variables"
    Dim db As New DataClassesHglpDataContext
    Dim myArrF As String()
    Dim myArrN As String()
    Private m_Series As Integer
    ' Declare a variable to indicate the commit scope.  
    ' Set this value to false to use cell-level commit scope.  
    Private rowScopeCommit As Boolean = True
    Dim fS1HiddenForm As New Form
    Dim conn As String
    Dim whLst As New List(Of Integer)
#End Region
#Region "02-Declare Propertys"
    Public Property Series As Integer
        Get
            Return m_Series
        End Get
        Set(ByVal value As Integer)
            m_Series = value
        End Set
    End Property

#End Region
#Region "03-Load Form"
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
        DateTimePicker2.Value = New Date(CTODate.Year, CTODate.Month, CTODate.Day, 23, 59, 59) 'CDate("01/01/" & Year(CTODate))

        StartDate = CDate("01/01/" & Year(CTODate))
        txtTimeOut.Text = 900

        Dim Pass = ""
        'MsgBox("Καλή Χρονιά !!!" & vbCrLf & "  -- 2018 --", MsgBoxStyle.Information)
        Me.TlSTxtMTRL.Text = "*"
        If LocalIP = "192.168.10.108" Then
            'DateTimePicker1.Value = CDate("18/04/2018") 'StartDate 'CDate("01/" & CTODate.Month & "/" & Year(CTODate))
            'DateTimePicker2.Value = CDate("18/04/2018")
            'Me.txtFields_CODE.Text = "2400020000*" '"2103030071*"
            'Me.TlSTxtWHOUSE.Text = "2"
            'Me.TlSTxtTPRMS.Text = "2041,2521,2523"
            Me.RadioBtnExportData.Checked = True
        End If
        Dim conString As New SqlConnectionStringBuilder
        conString.ConnectionString = My.Settings.Item("GenConnectionString") '"server=" & SERVER & ";user id=gm;" & "password=1mgergm++;initial catalog=" & DATABASE
        conn = conString.ConnectionString
        Me.TlSSTLabelConnStr.Text = "Data Source=" & conString.DataSource & ";Initial Catalog=" & conString.InitialCatalog & ";User ID=" & conString.UserID '
        Me.ToolStripComboBoxIndexes.Visible = False
        Me.ToolStripSeparator3.Visible = False
        'Dim tls       ToolStripComboBoxIndexes
        Dim ar() = ("Όλα,Εκκρεμείς παραγγελίες,Picking,Επιστροφές-Ακυρώσεις Παρημίν,Παραγγελίες Μήνα,Παραδόσεις Μήνα").Split(",")

        Me.ToolStripComboBoxIndexes.Items.AddRange(ar)
        Me.ToolStripComboBoxIndexes.SelectedItem = "Όλα"

        If {"akrokos", "thzachopoulos", "dkanellopoulos", "skamariaris", "skamariarisp"}.Contains(CurUser) Then
            Me.radioBtnPerLine.Visible = False
            Me.radioBtnAnalytical.Visible = False
            Me.radioBtnΑggregate.Visible = False
            Me.radioBtnReOrder.Visible = False
            Me.RadioBtnExportData.Visible = False
            Me.radioBtnDailyReport.Visible = False
            Me.Panel2.Visible = False
            Me.radioBtnItemsStatement.Location = Me.radioBtnPerLine.Location
            Me.radioBtnItemsStatement.Checked = True
            Me.TlSTxtWHOUSE.Text = "2"
            Me.TlSTxtTPRMS.Text = "2041,2521,2523"
            Me.chkBoxZero.Visible = False
        End If
        Me.radioBtnReOrder.Enabled = False
        If {"dmalandrakis", "iantypa", "kvasilaki", "afarasoglou", "mourailidou", "vantza", "pplumidi"}.Contains(CurUser) Then
            Me.radioBtnReOrder.Enabled = True
            Me.TlSTxtWHOUSE.Text = "4, 13, 8, 5, 539"
        End If
        If CurUser = "gmlogic" Then
            Me.Panel1.Visible = True
            Me.radioBtnReOrder.Enabled = True
            '"18,19,20,21,22,23,24,25,26" '
            Me.TlSTxtWHOUSE.Text = "18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,156,158,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,180,181,185,186,187,188,189,190,191,192,193,194,195,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,232,233,234,235,237,239,245,246,247,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,277,278,279,280,281,282,283,284,285,286,287,288,289,292,293,294,295,296,297,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,338,339,342,343,344,345,346,347,348,349,350,351,352,353,354,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,399,401,402,403,405,406,410,411,412,414,415,416,417,418,419,420,422,423,425,426,428,432,434,437,446,447,450,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,508,509,510,511,512,513,514,515,516,523,524,525,526,527,528,529,530,531,532,535,537,540,541,544,548,551,552,553,556,557,558,559,561,562,563,564,565,568,569,570,571,572,573,574,575,576,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600,601,602,604,605,607,608,609,610,611,612,613,614,615,616,618,619,620,621,622,626,627,628,629,630,631,632,633,634,636,637,640,641,642,644,645,646,649,651,653,654,658,659,661,664,668,670,671,689,690,691,692,693,702,703,704,705,706,708,709,710,711,712,713,714,715,716,717,718,719,720,721,722,723,724,725,726,727,728,729,730,731,732,734,735,740,741,742,743,744,745,746,747,754,755,756,759,760,766,767,768,774,790,797,813,827,828,829,832,835,836,837,838,839,840,841,842,843,844,845,846,847,848,850,851,852,853,854,855,859,860,861,866,867,868,869,870,871,872,873,874,875,876,877,878,879,880,881,882,883,884,885,886,887,888,890,891,892,893,894,895,896,897,898,899,904,908,909,913,917,918,919,920,921,922,923,924,925,926,927,928,929,930,931,932,933,934,935,936,938,939,943,946,947,948,949,951,952,953,960,961,962,965,972,973,974,977,978,981,982,983,986,987,988,991,995,997,1000,1005,1009,1010,1011,1013,1014,1015,1016,1017,1018,1019,1020,1022,1023,1029,1030,1031,1032,1033,1034,1035,1036,1037,1038,1039,1044,1049,1050,1051,1055,1056,1057,1058,1060,1061,1063,1064,1065,1066,1067,1068,1069,1070,1071,1072,1073,1074,1075,1076,1077,1078,1079,1080,1081,1082,1083,1084,1085,1086,1087,1088,1089,1090,1091,1095,1096,1097,1098,1099,1100,1101,1102,1103,1104,1105,1106,1107,1108,1109,1110,1111,1112,1113,1115,1116,1117,1118,1119,1120,1125,1128,1130,1131,1133,1134,1136,1137,1138,1139,1140,1141,1144,1145,1146,1147,1148,1151,1154,1155,1156,1157,1158,1159,1161,1162,1163,1164,1165,1166,1167,1168,1169,1170,1171,1172,1173,1176,1177,1180,1182,1183,1184,1185,1190,1191,1194,1195,1197,1199,1202,1203,1205,1206,1210,1211,1216,1218,1219,1221,1222,1225,1226,1230,1231,1236,1241,1242,1248,1251,1256,1258,1259,1261,1267,1269,1274,1276,1283,1284,1285,1286,1288,1294,1295,1297,1298,1299,1300,1301,1302,1303,1304,1305,1306,1310,1311,1312,1313,1314,1317,1318,1319,1320,1321,1322,1323,1327,1328,1329,1330,1331,1334,1335,1336,1337,1338,1339,1340,1341,1342,1350,1351,1352,1353,1354,1355,1360,1361,1362,1364,1370,1371,1372,1374,1376,1382,1384,1386,1387,1388,1393,1394,1397,1399,1400,1401,1402,1403,1404,1405,1406,1407,1409,1410,1411,1412,1413,1417,1418,1421,1424,1425,1426,1427,1428,1429,1430,1431,1432,1433,1434,1435,1438,1439,1440,1441,1442,1443,1444,1445,1450,1457,1459,1460,1464,1470,1471,1472,1473,1474,1475,1476,1477,1478,1479,1480,1481,1482,1483,1487,1493,1496,1503,1508,1511,1513,1514,1516,1517,1518,1520,1523,1534,1539,1540,1545,1546,1547,1554,1563,1564,1567,1575,1577,1581,1582,1586,1587,1588,1590,1591,1592,1593,1594,1595,1596,1597,1598,1599,1600,1601,1602,1603,1604,1605,1606,1607,1608,1609,1610,1611,1618,1619,1620,1621,1622,1623,1624,1625,1626,1627,1631,1632,1633,1634,1641,1642,1643,1649,1652,1653,1654,1656,1658,1659,1661,1664,1670,1677,1678,1679,1680,1681,1683,1687,1688,1689,1690,1691,1692,1693,1694,1695,1696,1697,1698,1699,1700,1701,1702,1703,1704,1705,1706,1707,1708,1709,1710,1711,1712,1713,1714,1715,1716,1717,1718,1719,1720,1721,1722,1723,1724,1725,1726,1727,1728,1729,1730,1731,1732,1733,1734,1735,1736,1737,1738,1739,1740,1741,1742,1743,1744,1745,1746,1747,1748,1749,1750,1751,1752,1753,1754,1755,1756,1757,1758,1759,1763,1764,1765,1766,1767,1768,1777,1779,1783,1784,1785,1786,1787,1789,1790,1791,1792,1793,1797,1798,1799,1801,1807,1809,1815,1821,1823,1827,1828,1829,1830,1831,1832,1833,1836,1837,1838,1841,1843,1844,1845,1846,1847,1848,1849,1850,1851,1852,1853,1855,1861,1862,1878,1879,1880,1881,1882,1887,1889,1895,1896,1897,1901,1902,1903,1904,1905,1906,1907,1908,1909,1910,1911,1912,1913,1914,1915,1916,1917,1918,1919,1920,1922,1923,1924,1925,1926,1927,1928,1929,1930,1931,1932,1933,1934,1935,1936,1937,1939,1940,1941,1942,1943,1944,1945,1946,1947,1948,1949,1950,1951,1952,1953,1954,1957,1958,1959,1960,1961,1963,1965,1967,1968,1969,1970,1971,1972,1973,1974,1975,1976,1977,1978,1979,1980,1981,1982,1983,1984,1985,1986,1987,1988,1990,1991,1992,1993,1996,1997,2001,2002,2003,2005,2006,2007,2011,2012,2013,2014,2015,2016,2018,2019,2020,2021,2022,2023,2024,2025,2026,2027,2028,2029,2030,2031,2032,2033,2034,2035,2036,2037,2038,2039,2040,2042,2043,2044,2045,2046,2047,2049,2050,2051,2052,2053,2056,2057,2058,2059,2060,2061,2062,2063,2064,2065,2067,2068,2071,2072,2074,2075,2076,2077,2078,2079,2081,2082,2083,2084,2085,2086,2087,2090,2091,2093,2097,2100" ' "18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444,446,447,448,449,450,451,452,453,454,455,456,457,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559,560,561,562,563,564,565,566,567,568,569,570,571,572,573,574,575,576,577,578,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600,601,602,603,604,605,606,607,608,609,610,611,612,613,614,615,616,617,618,619,620,621,622,623,624,625,626,627,628,629,630,631,632,633,634,635,636,637,638,639,640,641,642,643,644,645,646,647,648,649,650,651,652,653,654,655,656,657,658,659,660,661,662,663,664,665,666,667,668,669,670,671,672,673,674,675,676,677,678,679,680,681,682,683,684,685,686,687,688,689,690,691,692,693,694,695,696,697,698,699,700,701,702,703,704,705,706,707,708,709,710,711,712,713,714,715,716,717,718,719,720,721,722,723,724,725,726,727,728,729,730,731,732,733,734,735,736,737,738,739,740,741,742,743,744,745,746,747,748,749,750,751,752,753,754,755,756,757,758,759,760,761,762,763,764,765,766,767,768,769,770,771,772,773,774,775,776,777,778,779,780,781,782,783,784,785,786,787,788,789,790,791,792,793,794,795,796,797,798,799,800,801,802,803,804,805,806,807,808,809,810,811,812,813,814,815,816,818,819,820,821,822,823,824,825,826,827,828,829,830,831,832,833,834,835,836,837,838,839,840,841,842,843,844,845,846,847,848,849,850,851,852,853,854,855,856,857,858,859,860,861,862,863,864,865,866,867,868,869,870,871,872,873,874,875,876,877,878,879,880,881,882,883,884,885,886,887,888,889,890,891,892,893,894,895,896,897,898,899,900,901,902,903,904,905,906,907,908,909,910,911,912,913,914,915,916,917,918,919,920,921,922,923,924,925,926,927,928,929,930,931,932,933,934,935,936,937,938,939,940,941,942,943,944,945,946,947,948,949,950,951,952,953,954,955,956,957,958,959,960,961,962,963,964,965,966,967,968,969,970,971,972,973,974,975,976,977,978,979,980,981,982,983,984,985,986,987,988,989,990,991,992,993,994,995,996,997,998,1000,1001,1002,1003,1004,1005,1006,1007,1008,1009,1010,1011,1012,1013,1014,1015,1016,1017,1018,1019,1020,1021,1022,1023,1024,1025,1026,1027,1028,1029,1030,1031,1032,1033,1034,1035,1036,1037,1038,1039,1040,1041,1042,1043,1044,1045,1046,1047,1048,1049,1050,1051,1052,1053,1054,1055,1056,1057,1058,1059,1060,1061,1062,1063,1064,1065,1066,1067,1068,1069,1070,1071,1072,1073,1074,1075,1076,1077,1078,1079,1080,1081,1082,1083,1084,1085,1086,1087,1088,1089,1090,1091,1092,1093,1094,1095,1096,1097,1098,1099,1100,1101,1102,1103,1104,1105,1106,1107,1108,1109,1110,1111,1112,1113,1114,1115,1116,1117,1118,1119,1120,1121,1122,1123,1124,1125,1126,1127,1128,1129,1130,1131,1132,1133,1134,1135,1136,1137,1138,1139,1140,1141,1142,1143,1144,1145,1146,1147,1148,1149,1150,1151,1152,1153,1154,1155,1156,1157,1158,1159,1160,1161,1162,1163,1164,1165,1166,1167,1168,1169,1170,1171,1172,1173,1174,1175,1176,1177,1178,1179,1180,1181,1182,1183,1184,1185,1186,1187,1188,1189,1190,1191,1193,1194,1195,1196,1197,1198,1199,1200,1201,1202,1203,1204,1205,1206,1207,1208,1209,1210,1211,1212,1213,1214,1215,1216,1217,1218,1219,1220,1221,1222,1223,1224,1225,1226,1227,1228,1229,1230,1231,1232,1233,1234,1235,1236,1237,1238,1239,1240,1241,1242,1243,1244,1245,1246,1247,1248,1249,1250,1251,1252,1253,1254,1255,1256,1257,1258,1259,1260,1261,1262,1263,1264,1265,1266,1267,1268,1269,1270,1271,1272,1273,1274,1275,1276,1277,1278,1279,1280,1281,1282,1283,1284,1285,1286,1287,1288,1289,1290,1291,1292,1293,1294,1295,1296,1297,1298,1299,1300,1301,1302,1303,1304,1305,1306,1307,1308,1309,1310,1311,1312,1313,1314,1315,1316,1317,1318,1319,1320,1321,1322,1323,1324,1325,1326,1327,1328,1329,1330,1331,1332,1333,1334,1335,1336,1337,1338,1339,1340,1341,1342,1343,1344,1345,1346,1347,1348,1349,1350,1351,1352,1353,1354,1355,1356,1357,1358,1359,1360,1361,1362,1363,1364,1365,1366,1367,1368,1369,1370,1371,1372,1374,1375,1376,1377,1378,1379,1380,1381,1382,1383,1384,1385,1386,1387,1388,1389,1390,1391,1392,1393,1394,1395,1396,1397,1399,1400,1401,1402,1403,1404,1405,1406,1407,1408,1409,1410,1411,1412,1413,1414,1415,1416,1417,1418,1419,1420,1421,1422,1423,1424,1425,1426,1427,1428,1429,1430,1431,1432,1433,1434,1435,1436,1437,1438,1439,1440,1441,1442,1443,1444,1445,1446,1447,1448,1449,1450,1451,1452,1453,1454,1455,1456,1457,1458,1459,1460,1461,1462,1463,1464,1465,1466,1467,1468,1469,1470,1471,1472,1473,1474,1475,1476,1477,1478,1479,1480,1481,1482,1483,1484,1485,1486,1487,1488,1489,1490,1491,1492,1493,1494,1495,1496,1497,1498,1499,1500,1501,1502,1503,1504,1505,1506,1507,1508,1509,1510,1511,1512,1513,1514,1515,1516,1517,1518,1519,1520,1521,1522,1523,1524,1525,1526,1527,1528,1529,1530,1531,1532,1533,1534,1535,1536,1537,1538,1539,1540,1541,1542,1543,1544,1545,1546,1547,1548,1549,1550,1551,1552,1553,1554,1555,1556,1557,1558,1559,1560,1561,1562,1563,1564,1565,1566,1567,1568,1569,1570,1571,1572,1573,1574,1575,1576,1577,1578,1580,1581,1582,1583,1584,1585,1586,1587,1588,1589,1590,1591,1592,1593,1594,1595,1596,1597,1598,1599,1600,1601,1602,1603,1604,1605,1606,1607,1608,1609,1610,1611,1612,1613,1614,1615,1616,1617,1618,1619,1620,1621,1622,1623,1624,1625,1626,1627,1628,1629,1630,1631,1632,1633,1634,1635,1636,1637,1638,1639,1640,1641,1642,1643,1644,1645,1646,1647,1648,1649,1650,1651,1652,1653,1654,1655,1656,1657,1658,1659,1660,1661,1662,1663,1664,1665,1666,1667,1668,1669,1670,1671,1672,1673,1674,1675,1676,1677,1678,1679,1680,1681,1682,1683,1684,1685,1686,1687,1688,1689,1690,1691,1692,1693,1694,1695,1696,1697,1698,1699,1700,1701,1702,1703,1704,1705,1706,1707,1708,1709,1710,1711,1712,1713,1714,1715,1716,1717,1718,1719,1720,1721,1722,1723,1724,1725,1726,1727,1728,1729,1730,1731,1732,1733,1734,1735,1736,1737,1738,1739,1740,1741,1742,1743,1744,1745,1746,1747,1748,1749,1750,1751,1752,1753,1754,1755,1756,1757,1758,1759,1760,1761,1762,1763,1764,1765,1766,1767,1768,1769,1770,1771,1772,1773,1774,1775,1776,1777,1778,1779,1780,1781,1782,1783,1784,1785,1786,1787,1788,1789,1790,1791,1792,1793,1794,1795,1796,1797,1798,1799,1800,1801,1802,1803,1804,1805,1806,1807,1808,1809,1810,1811,1812,1813,1814,1815,1816,1817,1818,1819,1820,1821,1822,1823,1824,1825,1826,1827,1828,1829,1830,1831,1832,1833,1834,1835,1836,1837,1838,1839,1840,1841,1842,1843,1844,1845,1846,1847,1848,1849,1850,1851,1852,1853,1854,1855,1856,1857,1858,1859,1860,1861,1862,1863,1864,1865,1866,1867,1868,1869,1870,1871,1872,1873,1874,1875,1876,1877,1878,1879,1880,1881,1882,1883,1884,1885,1886,1887,1888,1889,1890,1891,1892,1893,1894,1895,1896,1897,1898,1899,1900,1901,1902,1903,1904,1905,1906,1907,1908,1909,1910,1911,1912,1913,1914,1915,1916,1917,1918,1919,1920,1921,1922,1923,1924,1925,1926,1927,1928,1929,1930,1931,1932,1933,1934,1935,1936,1937,1938,1939,1940,1941,1942,1943,1944,1945,1946,1947,1948,1949,1950,1951,1952,1953,1954,1955,1956,1957,1958,1959,1960,1961,1962,1963,1964,1965,1966,1967,1968,1969,1970,1971,1972,1973,1974,1975,1976,1977,1978,1979,1980,1981,1982,1983,1984,1985,1986,1987,1988,1989,1990,1991,1992,1993,1994,1995,1996,1997,1998,1999,2000,2001,2002,2003,2004,2005,2006,2007,2008,2009,2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020,2021,2022,2023,2024,2025,2026,2027,2028,2029,2030,2031,2032,2033,2034,2035,2036,2037,2038,2039,2040,2041,2042,2043,2044,2045,2046,2047,2048,2049,2050,2051,2052,2053,2054,2055,2056,2057,2058,2059,2060,2061,2062,2063,2064,2065,2066,2067,2068,2069,2070,2071,2072,2073,2074,2075,2076,2077,2078,2079,2080,2081,2082,2083,2084,2085,2086,2087,2088,2089,2090,2091,2092,2093,2094,2095,2096,2097,2098,2099,2100" '"1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,152,153,408,445,507,539,817,999,1192,1373,1398,1579" ' "4, 13, 8, 5, 539"
            Me.TlSTxtMTRL.Text = "210103041*"
            Me.radioBtnΑggregate.Checked = True
        End If

        Me.DateTimePicker1.Value = StartDate

        ' When the form loads, the KeyPreview property is set to True.
        ' This lets the form capture keyboard events before
        ' any other element in the form.
        Me.KeyPreview = True
        ' Display the ProgressBar control.
        Me.ToolStripProgressBar1.Visible = True
        ' Set Minimum to 1 to represent the first file being copied.
        Me.ToolStripProgressBar1.Minimum = 1
        ' Set Maximum to the total number of files to copy.
        Me.ToolStripProgressBar1.Maximum = 1 'filenames.Length
        ' Set the initial value of the ProgressBar.
        Me.ToolStripProgressBar1.Value = 1
        ' Set the Step property to a value of 1 to represent each file being copied.
        Me.ToolStripProgressBar1.Step = 1

        ' Display the ProgressBar control.
        Me.ToolStripProgressBar2.Visible = True
        ' Set Minimum to 1 to represent the first file being copied.
        Me.ToolStripProgressBar2.Minimum = 1
        ' Set Maximum to the total number of files to copy.
        Me.ToolStripProgressBar2.Maximum = 1 'filenames.Length
        ' Set the initial value of the ProgressBar.
        Me.ToolStripProgressBar2.Value = 1
        ' Set the Step property to a value of 1 to represent each file being copied.
        Me.ToolStripProgressBar2.Step = 1

    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.cmdSelect.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview is True, and this is from the FORM.")
        End If
    End Sub
    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = (Not DataSafe())
    End Sub
#End Region
#Region "04-Bas_Commands"
    Private Sub Cmd_Edit()
        Try
            'Try
            '    Me.Cursor = Cursors.WaitCursor
            '    Dim str As String = ""
            '    'str = IIf(iActiveObjType = 1351, "SALDOC", "") + "[AUTOLOCATE=" + dgFINDOC.Rows(e.RowIndex).Cells("ID").Value.ToString + "]"
            '    Dim drv As ccCVShipment = Me.MasterBindingSource.Current
            '    str = "SALDOC[AUTOLOCATE=" & drv.FINDOC & "]"
            '    s1Conn.ExecS1Command(str, fS1HiddenForm)
            '    'FilldgFINDOC_gm(iActiveObjType)
            'Catch ex As Exception
            '    MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            'Finally
            '    Me.Cursor = Cursors.Default
            'End Try
            'If Me.boxOption_Paragelies.Checked Then
            '    Exit Sub
            'End If
            'Dim TFormF
            'TFormF = New ParagFBR
            ''If Me.boxOption_Apografh.Checked Then
            ''    TFormF = New ParagFBRAPG
            ''End If
            'Dim drv As FINDOC_MTRLINE = Me.MasterBindingSource.Current
            'If drv.Series = 9590 Then '"ΑΠΟΓΡΑΦΗToolStripMenuItem" 
            '    TFormF = New ParagFBRDIAK 'ParagFBRAPG
            '    TFormF.NSeries = 9590
            '    TFormF.NSOSOURCE = 1151
            'End If
            ''If Not Array.IndexOf({9520, 9521, 9522, 9523}, CType(drv.Series, Integer)) = -1 Then ' = 9520 Or drv.Series = 9521 or drv.Series=9522 or drv Then
            'If {9520, 9521, 9522, 9523, 9524, 9526}.Contains(drv.Series) Then
            '    TFormF = New ParagFBRDIAK
            '    'ΕΣΩΤΕΡΙΚΗ ΔΙΑΚΙΝΗΣΗ - 9520 (-1095+1096)
            '    'TFormF.NSeries = 9520
            '    'TFormF.NSOSOURCE = 1151
            'End If
            ''TFormF.CurDrv = New MTRLINE
            'Dim indx As Integer = drv.FINDOC
            'TFormF.CurDrv = db.MTRLINEs.Where(Function(f) f.FINDOC = indx).FirstOrDefault
            'TFormF.P_MTRLINES = drv.MTRLINES
            'Dim Position As Integer = Me.MasterBindingSource.Position
            'TFormF.DgdvRefresh = False
            'TFormF.Text = Me.Tag & " - " & TFormF.Name
            'TFormF.ShowDialog()
            'If TFormF.DgdvRefresh = True Then
            '    'Me.DataGridViewMaster.Refresh()
            '    Cmd_Select()
            '    ' Set the Position property to the results of the Find method. 
            '    Dim rowFound As FINDOC_MTRLINE = (From g As FINDOC_MTRLINE In Me.MasterBindingSource Where g.FINDOC = TFormF.CurDrv.FINDOC).FirstOrDefault()
            '    If Not IsNothing(rowFound) Then
            '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
            '        Me.MasterBindingSource.Position = itemFound
            '    End If
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Cmd_Select()
        If Me.radioBtnPerLine.Checked = False Then
            Cmd_SelectLINQ()
            Exit Sub
        End If
        'me.MasterDataGridView.DataSource = Nothing
        'PanelFINDOCGrid.Visible = True
        ''Use WebService Calls
        'me.MasterDataGridView.DataSource = S1_WSGetBrowserData(s1Conn, "SALDOC", "", "FINDOC.TRNDATE=2014-01-01&FINDOC.TRNDATE_TO=2014-12-31")
        'lblFINDOCRecords.Text = lblFINDOCRecords.Tag + me.MasterDataGridView.Rows.Count.ToString
        Me.MasterDataGridView.DataSource = Nothing

        Me.Cursor = Cursors.WaitCursor
        Try
            LoadData()
            db.Log = Console.Out 'Nothing '

            Dim q = db.FetchWhouses(0, Me.TlSTxtMTRL.Text.Replace("*", "%"), 1000, 51, Me.TlSTxtWHOUSE.Text, CDate("01/" & Me.DateTimePicker2.Value.Month & "/" & Me.DateTimePicker2.Value.Year), Me.DateTimePicker2.Value, Me.DateTimePicker2.Value.Year, Me.DateTimePicker2.Value.Month).ToList

            Me.MasterBindingSource.DataSource = New SortableBindingList(Of Hglp.FetchWhousesResult)(q)

            'Me.MasterBindingSource.DataSource = dtb
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource.DataSource


            Me.MasterDataGridView.AutoGenerateColumns = True
            Me.lblTQTY2NCOV.Text = ""

            If q.Count > 0 Then
                Me.lblTQTY2NCOV.Text = q.Sum(Function(f) f.TBAL)
            End If

            'lblFINDOCRecords.Text = lblFINDOCRecords.Tag + Me.MasterDataGridView.Rows.Count.ToString
            MasterDataGridView_Styling()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub Cmd_SelectLINQ()
        'me.MasterDataGridView.DataSource = Nothing
        'PanelFINDOCGrid.Visible = True
        ''Use WebService Calls
        'me.MasterDataGridView.DataSource = S1_WSGetBrowserData(s1Conn, "SALDOC", "", "FINDOC.TRNDATE=2014-01-01&FINDOC.TRNDATE_TO=2014-12-31")
        'lblFINDOCRecords.Text = lblFINDOCRecords.Tag + me.MasterDataGridView.Rows.Count.ToString
        Me.MasterDataGridView.DataSource = Nothing
        Dim str As String = ""
        Dim dsTRDR As New DataSet
        Dim ixTable As XTable
        Me.Cursor = Cursors.WaitCursor
        Dim nowt = Now
        Try

            LoadData()
            db.Log = Console.Out 'Nothing '


            'Export Data
            If Me.RadioBtnExportData.Checked Then
                Dim mTRL As System.Nullable(Of Integer)
                Dim cODE As String
                Dim cOMPANY As System.Nullable(Of Integer)
                Dim sODTYPE As System.Nullable(Of Integer)
                Dim wHOUSE As String
                Dim mTRACN As String
                Dim dFROM As System.Nullable(Of Date)
                Dim dTO As System.Nullable(Of Date)
                Dim fISCPRD As System.Nullable(Of Integer)
                Dim pERIOD As System.Nullable(Of Integer)
                Dim sOSOURCE As System.Nullable(Of Integer)
                Dim fPRMS As String
                Dim tFPRMS As String
                Dim tRDBUSINESS As String
                Dim iSCANCEL As System.Nullable(Of Short)
                Dim fULLYTRANSF As String
                Dim sLCODE As String
                Dim tRDRCODE As String
                Dim sLGROUP As String

                mTRL = Nothing
                cODE = Me.TlSTxtMTRL.Text.Replace("*", "%").Trim
                cOMPANY = 1000
                sODTYPE = 13
                wHOUSE = Me.TlSTxtWHOUSE.Text
                mTRACN = "101,102,103,105" 'Λογιστική κατηγορία
                dFROM = Me.DateTimePicker1.Value.ToShortDateString
                dTO = Me.DateTimePicker2.Value.ToShortDateString
                fISCPRD = Me.DateTimePicker1.Value.Year
                pERIOD = Me.DateTimePicker1.Value.Month
                sOSOURCE = 1351
                fPRMS = Me.TlSTxtFPRMS.Text
                tFPRMS = Me.TlSTxtTFPRMS.Text
                tRDBUSINESS = Me.TlSTxtΤRDBUSINES.Text
                iSCANCEL = 0
                fULLYTRANSF = Me.TlSTxtFULLYTRANSF.Text
                sLCODE = Me.TlSTxtPRSN.Text
                tRDRCODE = Me.TlSTxtTRDR.Text.Replace("*", "%").Trim
                sLGROUP = ""

                'Me.TlSTxtWHOUSE.Text = ""
                'Me.TlSTxtFPRMS.Text = ""
                'Me.TlSTxtTFPRMS.Text = ""
                ''0  Μετασχηματισμός(Όχι)
                ''1  Μετασχηματισμός(Πλήρως)
                ''2  Μετασχηματισμός(Μερικώς)
                ''3  Μετασχηματισμένο


                ''Εκκρεμείς Παραγγελίες
                ''PICKING
                ''Κατάσταση παραδόσεων
                ''Επιστροφές
                'Select Case Me.ToolStripComboBoxIndexes.SelectedItem
                '    Case "Όλα"
                '        Exit Sub
                '    Case "Εκκρεμείς Παραγγελίες"
                '        Me.TlSTxtTFPRMS.Text = "201"
                '        Me.TlSTxtFULLYTRANSF.Text = "0,2"
                '    Case "PICKING"
                '        Me.TlSTxtFPRMS.Text = "1000"
                '        Me.TlSTxtFULLYTRANSF.Text = "0,2"
                '    Case "Κατάσταση παραδόσεων"
                '        Me.TlSTxtWHOUSE.Text = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,408,445"
                '        Me.TlSTxtFPRMS.Text = "1000,1061,1066,7001,7021,7023,7039,7040,7041,7042,7043,7045,7046,7060,7061,7062,7063,7064,7066,7067,7068,7069,7071,7072,7073,7076,7082,7083,7084,7127,7141,7143,7144"
                '        Me.TlSTxtTFPRMS.Text = "101,301"
                '    Case "Επιστροφές"
                'End Select

                Dim res As New List(Of Hglp.FetchWhousesDailyResult)
                'res = db.FetchWhousesDaily(Me.ToolStripComboBoxIndexes.SelectedItem, Nothing, Me.txtFields_CODE.Text.Replace("*", "%").Trim, 1000, 13, Me.TlSTxtWHOUSE.Text, "101,102,103",
                '                     Me.DateTimePicker1.Value, Me.DateTimePicker2.Value.ToShortDateString, Me.DateTimePicker1.Value.Year, Me.DateTimePicker1.Value.Month, 1351, Me.TlSTxtFPRMS.Text, Me.TlSTxtTFPRMS.Text, 10, 0, Me.TlSTxtFULLYTRANSF.Text).ToList

                res = db.FetchWhousesDaily(mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, mTRACN, dFROM, dTO, fISCPRD, pERIOD, sOSOURCE, fPRMS, tFPRMS, tRDBUSINESS, iSCANCEL, fULLYTRANSF, sLCODE, tRDRCODE, sLGROUP).ToList

                Me.MasterBindingSource.DataSource = New SortableBindingList(Of Hglp.FetchWhousesDailyResult)(res)

                If Me.ToolStripComboBoxIndexes.SelectedItem.ToString = "Όλα" Then
                    Exit Sub
                End If

                Me.lblHSumQty1.Text = "Ποσ.1"
                Me.lblTQty1.Text = String.Format("{0:N3}", res.Sum(Function(f) f.QTY1))
                Me.lblTQTY1COV.Text = "Εκτελεσμένα"
                Me.lblTQTY1COV.Text = String.Format("{0:N3}", res.Sum(Function(f) f.QTY1COV))
                Me.lblΤQty1NCOV.Text = "Διαφορά"
                Me.lblΤQty1NCOV.Text = String.Format("{0:N3}", res.Sum(Function(f) f.DIAFORA))
                Me.lblTQTY1CANC.Text = "Ακυρωμένα"
                Me.lblTQTY1CANC.Text = String.Format("{0:N3}", res.Sum(Function(f) f.QTY1CANC))


            End If

            'ΕπανΤαξινόμηση
            If Me.radioBtnReOrder.Checked Then

                'Dim q = From m In db.MTRLs Join x In db.MTREXTRAs On m.MTRL Equals x.MTRL Join un In db.MTRUNITs On m.MTRUNIT1 Equals un.MTRUNIT
                '        Select New _reorder With {.company = m.COMPANY,
                '            .mtrl = m.MTRL,
                '            .code = m.CODE,
                '            .name = m.NAME,
                '            .mtracn = m.MTRACN,
                '            .un_Name = un.NAME,
                '            .num05 = x.NUM05,
                '            .reorder = Nothing
                '            }

                'Dim wh = q.Where(Function(f) {101, 102, 103}.Contains(f.mtracn))

                'If Me.TlSTxtMTRL.Text = "" Then
                '    Me.TlSTxtMTRL.Text = "*"
                '    wh = wh.Where(Function(f) f.code Like Me.TlSTxtMTRL.Text)
                'End If

                'Me.MasterBindingSource.DataSource = wh

            End If

            'Καρτέλες ειδών
            If Me.radioBtnItemsStatement.Checked Then
                Dim res As IMultipleResults = db.GetItemsStatements(Nothing, Me.TlSTxtMTRL.Text.Replace("*", "%").Trim, 1000, 51, Me.TlSTxtWHOUSE.Text, "101,102,103",
                                     Me.DateTimePicker1.Value,
                                    Me.DateTimePicker2.Value)
                'ncorrect number of arguments supplied for call to method 'System.Data.Linq.IMultipleResults GetItemsStatements(System.Nullable`1[System.Int32], System.String, System.Nullable`1[System.Int32], System.Nullable`1[System.Int32], System.String, System.String, System.Nullable`1[System.DateTime], System.Nullable`1[System.DateTime], Int32)'

                'Dim res1 = res.GetResult(Of GetItemsStatementsResult1).ToList
                Dim res1 = res.GetResult(Of Hglp.GetItemsStatementsResult1).ToList

                'myArrN = ("Ημερ/νία,Παραστατικό,Κίνηση,Α.Χ.,Επωνυμία,Ποσ.1,Ποσ.εισαγωγών,Αξία εισαγωγών,Ποσ.εξαγωγών,Αξία εξαγωγών,Υπόλοιπο ποσ.,Υπόλοιπο αξία,Αιτιολογία").Split(",")
                Dim res2 = res.GetResult(Of Hglp.GetItemsStatementsResult2).ToList
                Dim impq1 As Double = res2.Sum(Function(f) f.IMPQTY1)
                Dim expq1 As Double = res2.Sum(Function(f) f.EXPQTY1)
                Dim bal = impq1 - expq1
                Dim AA = 0


                Dim tprms = db.TPRMs.ToList
                '1   FLG01	Ποσ.εισαγ.
                '2   FLG02	Αξίες εισαγ.
                '3   FLG03	Τιμολογ.ποσ.εισαγ.
                '4   FLG04	Ποσ.εξαγ.
                '5   FLG05	Αξίες εξαγ.
                '6   FLG06	Τιμολογ.ποσ.εξαγ.
                '7   FLG07	Ποσ.αγορών
                '8   FLG08	Αξία αγορών
                '9   FLG09	Ποσ.πωλήσεων
                '10  FLG10	Αξία πωλήσεων
                '11  FLG11	Ποσ.παραγωγής
                '12  FLG12	Αξία παραγωγής
                '13  FLG13	Ποσ.ανάλωσης
                '14  FLG14	Αξία ανάλωσης
                '15  FLG15	Κόστος υλικών
                '16  FLG16	Εργατικά
                '17  FLG17	ΓΒΕ
                '18  FLG18	Έξοδα πωλήσεων
                '19  FLG19	Τιμολογ.αξίες εισαγ.
                '20  FLG20	Τιμολογ.αξίες εξαγ.
                '25  FLG25	Ενημέρωση τιμών

                Dim lss As New List(Of Hglp.GetItemsStatementsView)
                For Each rs In res1
                    Dim ls As New Hglp.GetItemsStatementsView
                    AA += 1
                    ls.AA = AA
                    ls.trndate = rs.TRNDATE
                    ls.fincode = rs.FINCODE
                    ls.tprms = rs.TPRMS
                    Dim tprm = tprms.Where(Function(f) f.COMPANY = rs.COMPANY And f.TPRMS = ls.tprms And f.SODTYPE = rs.SODTYPE).FirstOrDefault
                    ls.x_tprmsname = rs.X_TPRMSNAME
                    ls.whouse = rs.WHOUSE
                    ls.x_name = rs.X_NAME
                    ls.qty1 = rs.QTY1
                    ls.flg01 = tprm.FLG01
                    ls.flg04 = tprm.FLG04
                    If Not ls.flg01 = 0 Then 'Ποσ.εισαγ.
                        ls.impqty1 = rs.QTY1 'Ποσ.εισαγωγών
                        ls.impval = rs.LTRNVAL
                        ls.expqty1 = 0 'Ποσ.εξαγωγών
                        ls.expval = 0
                    End If
                    If Not ls.flg04 = 0 Then 'Ποσ.εισαγ.
                        ls.impqty1 = 0 'Ποσ.εισαγωγών
                        ls.impval = 0
                        ls.expqty1 = rs.QTY1 'Ποσ.εξαγωγών
                        ls.expval = rs.LTRNVAL
                    End If
                    If Not (ls.flg01 = 0 And ls.flg04 = 0) Then
                        bal += ls.impqty1 - ls.expqty1
                    End If
                    ls.remq = bal
                    ls.remv = 0
                    ls.comments = ""
                    lss.Add(ls)
                Next

                'If Not Me.TlSTxtSERIES.Text = "" Then
                '    lss = lss.Where(Function(f) Me.TlSTxtSERIES.Text.Split(",").Contains(f..SERIES)) 'Σειρά
                'End If

                If Not Me.TlSTxtTPRMS.Text = "" Then
                    lss = lss.Where(Function(f) Me.TlSTxtTPRMS.Text.Split(",").Contains(f.tprms)).ToList 'Τύπος
                End If

                lss = lss.OrderBy(Function(f) f.trndate).ThenByDescending(Function(f) f.flg01).ThenBy(Function(f) f.flg04).ToList

                ''TRNDATE,FINCODE,TPRMS,WHOUSE,X_NAME,QTY1,IMPQTY1,IMPVAL,EXPQTY1,EXPVAL,REMQ,REMV,COMMENTS
                ''For Each re In res1

                ''Next
                ''Dim ids As New List(Of GetItemsStatementsResult1)

                ''Dim gg = New GetItemsStatementsResult1
                ''gg.COMMENTS = "Απογραφή"
                ''ids.Add(gg)

                ''Dim dd As New List(Of GetItemsStatementsResult1)
                ''dd = ids.Union(res1.ToList)
                Dim rs1 As New List(Of Hglp.GetItemsStatementsView)
                rs1.Add(New Hglp.GetItemsStatementsView)
                rs1(0).fincode = "Εκ Μεταφοράς"

                rs1(0).whouse = Nothing
                rs1(0).impqty1 = impq1 '703.42
                rs1(0).expqty1 = expq1 '703.42
                rs1(0).remq = impq1 - expq1
                Me.MasterTopDataGridView.DataSource = New SortableBindingList(Of Hglp.GetItemsStatementsView)(rs1.ToList)

                Me.MasterBindingSource.DataSource = New SortableBindingList(Of Hglp.GetItemsStatementsView)(lss.ToList)

            End If

            If Me.radioBtnAnalytical.Checked Or Me.radioBtnΑggregate.Checked Then

                'str = File.ReadAllText(Application.StartupPath & "\SqlFiles\" & "Get WHOUSE Bal1.sql")
                'str = str.Substring(str.IndexOf("SELECT"), str.Length - str.IndexOf("SELECT"))

                ' strSql As String = "SELECT EmpCode,EmpID,EmpName FROM dbo.Employee"

                'Dim dtb As New DataTable
                'Dim cmd As New SqlCommand
                'cmd.Parameters.AddWithValue("@COMPANY", 1000)
                'cmd.Parameters.AddWithValue("@SODTYPE", 51) '51 Αποθήκη
                'cmd.Parameters.AddWithValue("@DFROM", CDate("01/" & Me.DateTimePicker2.Value.Month & "/" & Me.DateTimePicker2.Value.Year)) 'CDate("01/07/2017")) 'make sure you assign a value To startdate
                'cmd.Parameters.AddWithValue("@DTO", Me.DateTimePicker2.Value) ' CDate("01/08/2017")) 'make sure you assign a value To 
                ''cmd.Parameters.AddWithValue("@MTRL", Nothing) ' AS INTEGER = 2115 --63 --NULL --384 --NULL
                'If Me.txtFields_CODE.Text = "" Then
                '    Me.txtFields_CODE.Text = "*"
                'End If
                'cmd.Parameters.AddWithValue("@CODE", Me.txtFields_CODE.Text.Replace("*", "%")) '2103030557") ''--'%305%' 2103050533*
                'If Not Me.TlSTxtWHOUSE.Text = "" Then
                '    cmd.Parameters.AddWithValue("@WHOUSE", Me.TlSTxtWHOUSE.Text) '.Replace(",", "|")) '"2|4")
                'End If
                ''--DECLARE @MTRLS  AS VARCHAR(250) = ''
                'cmd.Parameters.AddWithValue("@FISCPRD", Me.DateTimePicker2.Value.Year)
                'cmd.Parameters.AddWithValue("@PERIOD", Me.DateTimePicker2.Value.Month)

                'Dim q As ISingleResult(Of Hglp.FetchWhousesResult) = db.FetchWhouses(Nothing, Me.txtFields_CODE.Text.Replace("*", "%").Trim, 1000, 51, If(Me.TlSTxtWHOUSE.Text = "", Nothing, Me.TlSTxtWHOUSE.Text),
                '                        CDate("01/" & Me.DateTimePicker2.Value.Month & "/" & Me.DateTimePicker2.Value.Year),
                '                        Me.DateTimePicker2.Value,
                '                        Me.DateTimePicker2.Value.Year,
                '                        Me.DateTimePicker2.Value.Month)

                'Dim q = db.testFetchWhouses(Nothing, Me.txtFields_CODE.Text.Replace("*", "%").Trim, 1000, 51, If(Me.TlSTxtWHOUSE.Text = "", Nothing, Me.TlSTxtWHOUSE.Text), "101,102,103",
                '                        CDate("01/" & Me.DateTimePicker2.Value.Month & "/" & Me.DateTimePicker2.Value.Year),
                '                        Me.DateTimePicker2.Value,
                '                        Me.DateTimePicker2.Value.Year,
                '                        Me.DateTimePicker2.Value.Month)

                'Dim q1 As IEnumerable(Of Hglp.testFetchWhousesResult) = q.GetResult(Of Hglp.testFetchWhousesResult)
                'Dim q2 = q.GetResult(Of Hglp.MTRL)
                'Dim ds As New DataSet '= GmData.GetDataSetSQL(conn, CommandType.StoredProcedure, "FetchWhouses", cmd,)
                'dtb = ds.Tables(0)
                'dtb.Columns.Add("test", GetType(Integer))

                '            Στην αναλυτική μορφή, θα πρέπει να παρέχεται ανάλυση σε τρείς γενικές κατηγορίες: Αποθέματα, Εκκρεμείς Παραγγελίες και Διαθέσιμα Λιπάσματα. Η κάθε κατηγορία θα πρέπει να αναλύεται στις εγκαταστάσεις ως εξής
                'Για κάθε εγκατάσταση τα αποθέματα θα απαρτίζονται από την στήλη Απόθεμα συστήματος, 
                'Καθοδόν προς την εγκατάσταση, 
                'Σχισμένα σακιά στην εγκατάσταση, 
                'Δελτία φορτώσεων και Χειρόγραφα
                ' (Τα χειρόγραφα θα συμπεριλαμβάνουν είτε χειρόγραφες ενδοδιακινήσεις (Θετικές όταν λαμβάνει λίπασμα η εγκατάσταση ή αρνητικές όταν διώχνει) και τις χειρόγραφες πωλήσεις (αρνητικό πρόσημο) 
                ' που θα πραγματοποιούνται από την εγκατάσταση, η οποία θα έχει πάρει χειρόγραφα κάποια λίπασμα). 
                ' Για να προκύψει το απόθεμα κάθε εγκατάστασης θα πρέπει να αθροίζονται τα αποθέματα της εγκατάστασης με τα καθοδόν και τα χειρόγραφα και θα αφαιρούνται τα δελτία φορτώσεων και τα σχισμένα σακιά. 
                '  Εδώ να σημειωθεί ότι τα χειρόγραφα θα προκύπτουν από την παραλαβή λιπάσματος μείον την αποστολή λιπαμαστος σε άλλη εγκατάσταση μείον την χειρόγραφη πώληση του λιπάσματος.
                '
                'Οι εκκρεμείς παραγγελίες θα χωρίζονται πάλι σε εκκρεμείς παραγγελίες συστήματος, δελτία φορτώσεων και χειρόγραφα (που θα περιλαμβάνουν την μείωση των εκκρεμών παραγγελιών λόγω χειρόγραφων πωλήσεων).
                ' Για να προκύψουν οι εκκρεμείς παραγγελίες της εγκατάστασης ανά πάσα στιγμή θα πρέπει να αφαιρούνται οι εκκρεμείς παραγγελίες του συστήματος με τα χειρόγραφα δελτία . 
                '
                'Τέλος στην διαθεσιμότητα των εγκαταστάσεων θα πρέπει να υπάρχει μια στήλη ανά εγκατάσταση που θα έχει ανά λίπασμα το αποτέλεσμα της αφαίρεσης των αποθεμάτων με τις εκκρεμείς παραγγελίες 
                '(τόσο για τα αποθέματα όσο και για τις παραγγελίες θα πρέπει να αθροίζονται όλες οι στήλες ανά εγκατάσταση όπως περιγράφθησαν άνωθεν.
                '
                'Τόσο για τα αποθέματα όσο και οι εκκρεμείς θέλουμε ανά λίπασμα να αθροίζονται το σύνολο των εγκαταστάσεων, όπως ακριβώς έχουμε στο επισυναπτόμενο συγκεντρωτικό αρχείο.
                'Η συνοπτική εικόνα θα παρουσιάζει μόνο: το συνολικό απόθεμα ανά εγκατάσταση, το σύνολο των εκκρεμών για κάθε εγκατάσταση και το διαθέσιμο ανά εγκατάσταση (που είναι αφαίρεση των στηλών των αποθεμάτων με τις εκκρεμείς.

                'Αποθέματα 3 / 8 / 2017		Απόθεμα Αποθήκης					Εκκρεμείς Παραγγελίες					Διαθέσιμο Απόθεμα				
                'Κωδικός Περιγραφή Είδους	MM	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο
                '2103030071  ΛΙΠ.0-0-30(17S)+10MgO 40kg MASTERKALI 	ΤΟΝ	371,92 	0,00 	16,00 	16,00 	403,92 	0,00 	0,00 	0,00 	0,00 	0,00 	371,92 	0,00 	16,00 	16,00 	403,92 

                'Κωδικός	Περιγραφή Είδους	Μον. Μετρησης	Απόθεμα  Θεσ/νίκης 	 Καθοδόν Προς Θεσ/νίκης 	 Σκισμένα Θεσ/νίκης(1002 Δελτίο προς επιστροφή) 	 Δελτια Φορτώσεων Θεσ/νίκης 	 Εκκρεμέις Θεσ/νίκης 

                'Dim whRp As New whRpt
                'whRp.mtrl = 0
                'whRp.code = ""
                'whRp.name = ""
                'whRp.mtrunit = ""
                ''2,4,5,8,13
                ''2 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος 4 Θεσσαλονίκη   13 Βαθύλακκος	8 Ασπρόπυργος	5 Πύργος
                'whRp.whouse = New List(Of Integer)
                'Dim gg() As Double
                'whRp.qty_4 = 0.00
                'whRp.openOrder = 0.00
                'whRp.onwaydif_4 = 0.00
                ''     Υλικό	Περιγραφή Είδους	ΜΜ	 LC Βαθύλακος 	 Εκκρεμεις 	 Διαθέσιμα 


                whLst.Clear()
                If Me.TlSTxtWHOUSE.Text = "" Then
                    whLst.AddRange({4, 13, 8, 5, 539})
                Else
                    For Each wh As Integer In Me.TlSTxtWHOUSE.Text.Split(",")
                        whLst.Add(wh)
                    Next
                End If

                Dim dt As New DataTable
                'myArrF = ("trdr,code,name,afm,tdSalesManName,oibal00,oibal01,oibal02,oibal03,oibal04,oibal05,oibal06,oibal07,oibal08,oibal09,oibal10,oibal11,oibal12,oibal13").Split(",")
                'myArrN = ("trdr,ΚΩΔ,ΣΥΝΑΛΑΣΟΜΕΝΟΣ,ΑΦΜ,Πωλητής,ΕκΜεταφ,Ιανουάριος,Φεβρουάριος,Μάρτιος,Απρίλιος,Μάιος,Ιούνιος,Ιούλιος,Αύγουστος,Σεπτέμβριος,Οκτώβριος,Νοέμβριος,Δεκέμβριος,ΣυνΥπολ").Split(",")

                Dim col As New DataColumn
                dt.Columns.Add("mtrl", GetType(Integer))

                dt.Columns.Add("code", GetType(String))
                dt.Columns.Item(dt.Columns.IndexOf("code")).Caption = "Κωδικός"
                dt.Columns.Add("name", GetType(String))
                dt.Columns.Item(dt.Columns.IndexOf("name")).Caption = "Περιγραφή"


                Dim whs = db.WHOUSEs.Where(Function(f) whLst.Contains(f.WHOUSE)).ToList

                For Each dd In whs
                    dt.Columns.Add("Qty_" & String.Format("{0:00}", dd.WHOUSE), GetType(System.Double))
                    dt.Columns.Item(dt.Columns.IndexOf("Qty_" & String.Format("{0:00}", dd.WHOUSE))).Caption = dd.NAME ' "Απόθεμα Αποθήκης" & vbCrLf & dd.NAME
                Next
                Dim PrimaryKey(0) As DataColumn
                PrimaryKey(0) = dt.Columns("MTRL")
                dt.PrimaryKey = PrimaryKey

                ' Set the initial value of the ProgressBar.
                Me.ToolStripProgressBar1.Value = 1
                Me.ToolStripProgressBar1.Maximum = whs.Count


                If Me.TlSTxtMTRL.Text.Trim = "" Then
                    Me.TlSTxtMTRL.Text = "21*"
                End If
                If Me.TlSTxtMTRACN.Text.Trim = "" Then
                    Me.TlSTxtMTRACN.Text = "101,102,103,104,105,106,107"
                End If

                Dim whRptZs As New List(Of whRptN)
                Dim whRpts As New List(Of whRptN)
                For Each wh In whLst
                    'Dim qq = db.testFetchWhouses(Nothing, Me.txtFields_CODE.Text.Replace("*", "%").Trim, 1000, 51, wh, "101,102,103",
                    '                CDate("01/" & Me.DateTimePicker2.Value.Month & "/" & Me.DateTimePicker2.Value.Year),
                    '                Me.DateTimePicker2.Value,
                    '                Me.DateTimePicker2.Value.Year,
                    '                Me.DateTimePicker2.Value.Month)
                    Dim qq = db.FetchWhousesBal(Nothing, Me.TlSTxtMTRL.Text.Replace("*", "%").Trim, 1000, 51, wh, Me.TlSTxtMTRACN.Text,
                                        Me.DateTimePicker1.Value,
                                        Me.DateTimePicker2.Value,
                                        Me.DateTimePicker1.Value.Year,
                                        Me.DateTimePicker1.Value.Month).ToList

                    If qq.Count = 0 Then
                        Me.ToolStripProgressBar1.PerformStep()
                        Continue For
                    End If
                    If qq.Count > 0 Then

                        ' Set the initial value of the ProgressBar.
                        Me.ToolStripProgressBar2.Value = 1
                        Me.ToolStripProgressBar2.Maximum = qq.Count

                    End If

                    Dim dr As DataRow = Nothing

                    For Each fwhr In qq
                        Dim whR = whRptZs.Where(Function(f) f.mtrl = fwhr.MTRL).FirstOrDefault
                        If IsNothing(whR) Then

                            whR = New whRptN
                            whR.mtrl = fwhr.MTRL
                            whR.code = fwhr.CODE
                            whR.name = fwhr.NAME
                            whR.mtrunit = fwhr.MTRUNIT1
                            'whR.mx_num05 = fwhr.MX_NUM05
                            whRptZs.Add(whR)
                            dr = dt.NewRow
                            dr("mtrl") = fwhr.MTRL
                            dr("code") = fwhr.CODE
                            dr("name") = fwhr.NAME
                            dt.Rows.Add(dr)
                            Me.ToolStripStatusLabel1.Text = "Κωδικός:" & fwhr.CODE
                        Else
                            dr = dt.Rows.Find(fwhr.MTRL)
                        End If

                        'SUM(ISNULL(IMPQTY1, 0) - ISNULL(EXPQTY1, 0)) AS OPNWH
                        'SUM(TP.FLG01 * ISNULL(T.QTY1, 0) - TP.FLG04 * ISNULL(T.QTY1, 0)) AS TRNWH
                        'Απόθεμα συστήματος, 
                        'Καθοδόν προς την εγκατάσταση, 
                        'Σχισμένα σακιά στην εγκατάσταση, 
                        'Δελτία φορτώσεων και Χειρόγραφα
                        'Απόθεμα Αποθήκης					Εκκρεμείς Παραγγελίες					Διαθέσιμο Απόθεμα

                        'whR.qty_4 = If(fwhr.OPNWH, 0) + If(fwhr.TRNWH, 0) 'Απόθεμα Αποθήκης
                        'whR.ekrdif_4 = If(fwhr.EKRDIF, 0) 'Εκκρεμείς Παραγγελίες A.TFPRMS IN (201) Συμπεριφορά Παραγγελία
                        'whR.dfdif_4 = If(fwhr.DFDIF, 0) 'A.FPRMS IN (1001) ΔΦΡΤ Δελτίο φόρτωσης
                        'whR.onwaydif_4 = If(fwhr.ONWAYDIF, 0) 'A.FINSTATES In (1000) Καθοδόν A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία

                        'whR.skisdif_4 = If(fwhr.SKISDIF, 0) 'A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
                        'whR.avlqty_4 = whR.qty_4 - whR.ekrdif_4
                        Dim qty As Double = 0
                        Dim oneway As Double = 0
                        Dim dfdif As Double = 0
                        Dim skisdif As Double = 0
                        Dim ekrdif As Double = 0

                        If Me.radioBtnAnalytical.Checked Then
                            qty = If(fwhr.OPNWH, 0) + If(fwhr.TRNWH, 0)
                            'oneway = If(fwhr.ONWAYDIF, 0)
                            'dfdif = If(fwhr.DFDIF, 0)
                            'skisdif = If(fwhr.SKISDIF, 0)
                        End If
                        If Me.radioBtnΑggregate.Checked Then
                            qty = If(fwhr.OPNWH, 0) + If(fwhr.TRNWH, 0) '+ If(fwhr.ONWAYDIF, 0) - If(fwhr.DFDIF, 0) - If(fwhr.SKISDIF, 0)
                            If Not Me.chkBoxZero.Checked Then
                                qty = Math.Round(qty, 3)
                            End If
                            If Not qty = 0 Then
                                Debug.Print(wh & "-" & qty)
                                If dr IsNot Nothing Then
                                    dr("Qty_" & String.Format("{0:00}", wh)) = qty
                                End If
                            End If

                            oneway = 0
                            dfdif = 0
                            skisdif = 0
                        End If



                        'ekrdif = If(fwhr.EKRDIF, 0)

                        'whR.GetType().GetProperty("qty_" & wh).SetValue(whR, qty, Nothing)
                        ''
                        'whR.GetType().GetProperty("onwaydif_" & wh).SetValue(whR, oneway, Nothing)
                        'whR.GetType().GetProperty("dfdif_" & wh).SetValue(whR, dfdif, Nothing)
                        'whR.GetType().GetProperty("skisdif_" & wh).SetValue(whR, skisdif, Nothing)
                        ''
                        'whR.GetType().GetProperty("ekrdif_" & wh).SetValue(whR, ekrdif, Nothing)
                        Me.ToolStripProgressBar2.PerformStep()
                    Next
                    Me.ToolStripProgressBar1.PerformStep()
                Next
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
                'myArrF = dt.Columns.Cast(Of DataColumn).Select(Function(f) f.ColumnName).ToArray()
                'myArrN = dt.Columns.Cast(Of DataColumn).Select(Function(f) f.Caption).ToArray()

                myArrF = dt.Columns.Cast(Of DataColumn).Where(Function(f) {"mtrl", "code", "name"}.Contains(f.ColumnName)).Select(Function(f) f.ColumnName).ToArray()
                myArrN = dt.Columns.Cast(Of DataColumn).Where(Function(f) {"mtrl", "code", "name"}.Contains(f.ColumnName)).Select(Function(f) f.Caption).ToArray()
                Dim myLstF As New List(Of String)
                Dim myLstN As New List(Of String)
                myLstF.AddRange(myArrF)
                myLstN.AddRange(myArrN)
                Try
                    For Each dd In whLst
                        Dim qty As Double = 0
                        Debug.Print(dd)
                        ' KilaPoudra = m_dvTemp.Table.Compute("Sum(CCCTPOUDRA)", Nothing)
                        If dt.Compute("Sum(" & "Qty_" & String.Format("{0:00}", dd) & ")", Nothing) IsNot DBNull.Value Then
                            qty = dt.Compute("Sum(" & "Qty_" & String.Format("{0:00}", dd) & ")", Nothing)
                        End If
                        If Not Me.chkBoxZero.Checked Then
                            qty = Math.Round(qty, 3)
                        End If
                        If Not qty = 0 Then
                            myLstF.Add("Qty_" & String.Format("{0:00}", dd))
                            myLstN.Add("(" & dd & ")" & dt.Columns.Cast(Of DataColumn).Where(Function(f) f.ColumnName = "Qty_" & String.Format("{0:00}", dd)).Select(Function(f) f.Caption).FirstOrDefault)
                        End If
                    Next
                    Me.MasterBindingSource.DataSource = dt 'q1 'dtb
                    myArrF = myLstF.ToArray
                    myArrN = myLstN.ToArray
                    Me.ToolStripProgressBar2.Value = 1
                    Me.ToolStripProgressBar2.Maximum = dt.Columns.Count
                    Me.MasterDataGridView.DataSource = Me.MasterBindingSource
                Catch ex As Exception

                End Try




            End If




            'Me.MasterDataGridView.AutoGenerateColumns = True
            Me.lblTQTY2NCOV.Text = ""
            'If dtb.Rows.Count > 0 Then
            '    Me.lblTQTY2NCOV.Text = dtb.Compute("Sum(TBAL)", Nothing) ', "EmpID = 5")
            'End If

            'lblFINDOCRecords.Text = lblFINDOCRecords.Tag + Me.MasterDataGridView.Rows.Count.ToString
            MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox("Timeout=" & Now.Subtract(nowt).ToString & vbCrLf & ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
            ixTable = Nothing
        End Try
        Me.ToolStripStatusLabel1.Text = "Timeout=" & Now.Subtract(nowt).ToString
    End Sub
    Private Sub Cmd_SelectS1()

        Me.Cursor = Cursors.WaitCursor
        Dim myModule As XModule
        myModule = s1Conn.CreateModule("SALDOC")
        Try

            myModule.LocateData("FINDOC:248")
            '            GetDataSet(ModuleHandle, Name);
            'Επιστρέφει το Dataset του Module
            'GetParamValue(ModuleHandle, PrmName);
            'Παράδειγμα fCustomer = GetDataSet(fModule,'CUSTOMER');
            'Dim ds As DataSet = myModule.GetDataSet(myModule) 'fModule,'CUSTOMER')

            Dim myTable As XTable
            Dim newID As Integer = 0
            ' myModule.InsertData()
            Dim list As List(Of KeyValuePair(Of String, Integer)) =
            New List(Of KeyValuePair(Of String, Integer))
            list.Add(New KeyValuePair(Of String, Integer)("FINDOC.FINDOC", 1))
            'list.Add(New KeyValuePair(Of String, Integer)("net", 2))
            'list.Add(New KeyValuePair(Of String, Integer)("perls", 3))

            myModule.LocateData(list)
            myTable = myModule.GetTable("MTRL")
            Dim ds As New DataSet
            ds.Tables.Add(myTable.CreateDataTable(True))
            'myTable.Current("CODE") = txtTRDRCode.Text.ToString
            'myTable.Current("NAME") = txtTRDRName.Text.ToString
            'myTable.Current("CITY") = txtTRDRCity.Text.ToString
            'myTable.Current("PHONE01") = txtTRDRPhone01.Text.ToString
            'newID = myModule.PostData()

            'MsgBox("Customer added With ID= " + newID.ToString, MsgBoxStyle.Information, strAppName)
            'txtTRDRCode.Text = "*"
            'txtTRDRName.Text = ""
            'txtTRDRCity.Text = ""
            'txtTRDRPhone01.Text = ""

            'FilldgTRDR(iActiveObjType)
            Me.MasterBindingSource.DataSource = ds
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource.DataSource


            Me.MasterDataGridView.AutoGenerateColumns = True

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
        Finally
            Me.Cursor = Cursors.Default
        End Try

        myModule.Dispose()

    End Sub
    Private Sub Cmd_Select1()
        Try
            LoadData()
            db.Log = Console.Out

            Dim q = db.ccCVShipments.AsQueryable '.AsEnumerable
            'Dim cmdText As String = "select user_id from CCCVShipments where user_loginname='" + AddressOf Me.UserFName + "'"
            'q = q.Where(Function(f) f.dtEntry = DateTimePicker1.Value) ' And f.dtEntry.Value <= DateTimePicker2.Value).ToList
            'q = q.Where(Function(f) f.domUser = "ndavradou")
            'q = q.Where(Function(p) p.dtEntry >= DateTimePicker1.Value.Date And p.dtEntry <= DateTimePicker2.Value)
            'WHERE(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
            '              And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
            q = q.Where(Function(f) f.COMPANY = 1000 And f.SOSOURCE = 1351 And f.SOREDIR = 0 And f.SODTYPE = 13 And f.PENDING >= 1)
            q = q.Where(Function(f) {201}.Contains(f.TFPRMS) And {0, 2}.Contains(f.FULLYTRANSF))
            'q = q.OrderBy(Function(f) f.Department.ToString + f.user_gr_fullname)

            q = q.Where(Function(f) f.TRNDATE >= DateTimePicker1.Value.Date And f.TRNDATE <= DateTimePicker2.Value)

            Me.MasterBindingSource.DataSource = q '.MasterBindingSource.DataSource = New SortableBindingList(Of FINDOC_MTRLINE)(nq) 'dt
            Me.MasterDataGridView.DataSource = Me.MasterBindingSource


            'dim emptyorlog as orariolog() = nothing
            'emptyorlog = {new orariolog with {.domuser = "--επιλέγξτε--"}}
            'CCCVShipms = (from empty in emptyorlog).union(db.CCCVShipments.orderby(function(f) f.domuser).tolist).tolist

            'CCCVShipms = db.CCCVShipments.OrderBy(Function(f) f.domUser).Where(Function(f) f.dtEntry.Value > New Date(2016, 12, 1)).ToList

            MasterDataGridView_Styling()
            Me.BindingNavigatorSaveItem.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Me.Cursor = Cursors.Default
    End Sub
#End Region
#Region "02-Save Data"
    ' Finish any current edits.
    Private Sub EndAllEdits()
        Me.Validate()
        Me.MasterBindingSource.EndEdit()
    End Sub
    Private Function DataSafe() As Boolean
        DataSafe = True
        ' Finish any current edits.
        EndAllEdits()

        If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function

        ' Ask the user if we should save the changes.
        Select Case MsgBox("Να αποθηκευθούν οι αλλαγές;", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2, "") 'MeLabel)
            Case MsgBoxResult.No
                ' The data is not safe.
            Case MsgBoxResult.Yes
                ' Save the changes.
                DataSafe = SaveData()
            Case MsgBoxResult.Cancel
                ' The user wants to cancel this operation.
                ' Do not let the program discard the data.
                If Not (db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0) Then
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Inserts)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Updates)
                    db.Refresh(RefreshMode.OverwriteCurrentValues, db.GetChangeSet.Deletes)
                End If
                Return False
        End Select
    End Function
    ' Save changes to the database.
    Friend Function SaveData() As Boolean
        SaveData = False
        Try
            'If db.GetChangeSet.Deletes.Count = 0 Then 'Not Delete Action
            '    If Not Conditions() Then
            '        Exit Function
            '    End If
            'End If
            If db.GetChangeSet.Inserts.Count = 0 And db.GetChangeSet.Updates.Count = 0 And db.GetChangeSet.Deletes.Count = 0 Then Exit Function
            ' Initialize the return value to zero and create a StringWriter to display results. 
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter
            Try
                ' Create the TransactionScope to execute the commands, guaranteeing 
                '  that both commands can commit or roll back as a single unit of work. 
                Using scope As New TransactionScope()
                    'LogSQL = sSQL
                    db.Log = Nothing ' Console.Out
                    db.SubmitChanges()

                    ' The Complete method commits the transaction. If an exception has been thrown, 
                    ' Complete is called and the transaction is rolled back.
                    scope.Complete()
                    SaveData = True
                End Using
            Catch ex As TransactionAbortedException
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message)
            Catch ex As ApplicationException
                writer.WriteLine("ApplicationException Message: {0}", ex.Message)
            Catch ex As Exception
                writer.WriteLine("Exception Message: {0}", ex.Message)
            Finally
                ' Close the connection
                If db.Connection.State = ConnectionState.Open Then
                    db.Connection.Close()
                End If
            End Try
            ' Display messages.
            If Not writer.ToString() = String.Empty Then
                MsgBox(writer.ToString(), MsgBoxStyle.Exclamation, "Προσοχή !!!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        If SaveData = False Then
            MsgBox("Προσοχή !!!.Ακύρωση Αλλαγών", MsgBoxStyle.Exclamation, "Προσοχή !!!")
        End If
    End Function
#End Region
#Region "96-MasterDataGridView"
    Private Sub MasterDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles MasterDataGridView.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If s.Columns(s.CurrentCell.ColumnIndex).Name = "ReOrder" Then
            Exit Sub
        End If
        If MasterDataGridView.IsCurrentCellDirty Then
            MasterDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub MasterDataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        'Cmd_Edit()
    End Sub
    Private Sub DataGridViewMaster_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Dim drv As DataRowView = Me.MasterBindingSource.Current

        'Dim status = Me.MasterDataGridView.Columns(e.ColumnIndex)
        'Me.StatusStrip1.Text = status
    End Sub

    Private Sub MasterDataGridView_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles MasterDataGridView.ColumnAdded
        e.Column.FillWeight = 1
        Me.ToolStripProgressBar2.PerformStep()
    End Sub
    Private Sub MasterDataGridView_Styling()
        Try

            Me.MasterDataGridView.AutoGenerateColumns = True
            'Me.MasterDataGridView.AutoResizeColumns()
            Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect

            If Me.radioBtnPerLine.Checked Then
                myArrF = ("WHOUSE,SHORTCUT,WHNAME,CODE,NAME,MTRUNITNAME,TBAL,CODEWB,NAMETD,NAME2").Split(",")
                myArrN = ("ΑΧ,Εγκατάσταση (Σύντμηση),Ονομασία Εγκατάστασης,Κωδικός Υλικού,Περιγραφή Υλικού,Μονάδα Μέτρησης,Ποσότητα Αποθέματος,Πελάτης,Όνομα,Πωλητής").Split(",")
            End If

            'Αποθέματα 21 / 8 / 2017            		Απόθεμα Αποθήκης				                	Εκκρεμείς Παραγγελίες			                		Διαθέσιμο Απόθεμα				
            'Κωδικός Περιγραφή Είδους	MM	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο	Θεσσαλονίκη	Βαθύλακκος	Ασπρόπυργος	Πύργος	Σύνολο
#Region "radioBtnΑggregate_radioBtnAnalytical"
            '4, 13, 8, 5, 539
            'If Me.radioBtnΑggregate.Checked Or Me.radioBtnAnalytical.Checked Then
            '    'myArrF = ("code,name,mu1name,qty_4,qty_13,qty_8,qty_5,qty_tot,ekrdif_4,ekrdif_13,ekrdif_8,ekrdif_5,ekrdif_tot,avlqty_4,avlqty_13,avlqty_8,avlqty_5,avlqty_tot").Split(",")
            '    'myArrN = ("Κωδικός Υλικού,Περιγραφή Υλικού,Μονάδα Μέτρησης,Απόθεμα Αποθήκης Θεσ/νίκη,Απόθεμα Αποθήκης Βαθύλακκος,Απόθεμα Αποθήκης Ασπρόπυργος,Απόθεμα Αποθήκης Πύργος,Απόθεμα Αποθήκης Σύνολο,Εκκρεμείς Παραγγελίες Θεσ/νίκη,Εκκρεμείς Παραγγελίες Βαθύλακκος,Εκκρεμείς Παραγγελίες Ασπρόπυργος,Εκκρεμείς Παραγγελίες Πύργος,Εκκρεμείς Παραγγελίες Σύνολο,Διαθέσιμο Απόθεμα Θεσ/νίκη,Διαθέσιμο Απόθεμα Βαθύλακκος,Διαθέσιμο Απόθεμα Ασπρόπυργος,Διαθέσιμο Απόθεμα Πύργος,Διαθέσιμο Απόθεμα Σύνολο").Split(",")
            '    Dim sF As String = ""
            '    Dim sN As String = ""
            '    sF = "mx_num05,code,name,mu1name"
            '    sN = "A/A,Κωδικός,Περιγραφή Είδους,MM"
            '    For Each wh In whLst
            '        sF &= ",qty_" & wh
            '        sN &= ",Απόθεμα Αποθήκης "
            '        Select Case wh
            '            Case 4
            '                sN &= "Θεσ/νίκη"
            '            Case 13
            '                sN &= "Βαθύλακκος"
            '            Case 8
            '                sN &= "Ασπρόπυργος"
            '            Case 5
            '                sN &= "Πύργος"
            '            Case 539
            '                sN &= "Φυτοθρεπτική"
            '        End Select
            '    Next
            '    If Me.radioBtnAnalytical.Checked Then
            '        For Each wh In whLst
            '            sF &= ",onwaydif_" & wh
            '            sN &= ",Καθ'οδόν "
            '            Select Case wh
            '                Case 4
            '                    sN &= "Θεσ/νίκη"
            '                Case 13
            '                    sN &= "Βαθύλακκος"
            '                Case 8
            '                    sN &= "Ασπρόπυργος"
            '                Case 5
            '                    sN &= "Πύργος"
            '                Case 539
            '                    sN &= "Φυτοθρεπτική"
            '            End Select
            '        Next
            '        For Each wh In whLst
            '            sF &= ",dfdif_" & wh
            '            sN &= ",Δελτ.Φόρτωσης "
            '            Select Case wh
            '                Case 4
            '                    sN &= "Θεσ/νίκη"
            '                Case 13
            '                    sN &= "Βαθύλακκος"
            '                Case 8
            '                    sN &= "Ασπρόπυργος"
            '                Case 5
            '                    sN &= "Πύργος"
            '                Case 539
            '                    sN &= "Φυτοθρεπτική"
            '            End Select
            '        Next
            '        For Each wh In whLst
            '            sF &= ",skisdif_" & wh
            '            sN &= ",Επιστροφές "
            '            Select Case wh
            '                Case 4
            '                    sN &= "Θεσ/νίκη"
            '                Case 13
            '                    sN &= "Βαθύλακκος"
            '                Case 8
            '                    sN &= "Ασπρόπυργος"
            '                Case 5
            '                    sN &= "Πύργος"
            '                Case 539
            '                    sN &= "Φυτοθρεπτική"
            '            End Select
            '        Next
            '    End If
            '    sF &= ",qty_tot"
            '    sN &= ",Απόθεμα Αποθήκης Σύνολο"
            '    For Each wh In whLst
            '        sF &= ",ekrdif_" & wh
            '        sN &= ",Εκκρεμείς Παραγγελίες "
            '        Select Case wh
            '            Case 4
            '                sN &= "Θεσ/νίκη"
            '            Case 13
            '                sN &= "Βαθύλακκος"
            '            Case 8
            '                sN &= "Ασπρόπυργος"
            '            Case 5
            '                sN &= "Πύργος"
            '            Case 539
            '                sN &= "Φυτοθρεπτική"
            '        End Select
            '    Next
            '    sF &= ",ekrdif_tot"
            '    sN &= ",Εκκρεμείς Παραγγελίες Σύνολο"
            '    For Each wh In whLst
            '        sF &= ",avlqty_" & wh
            '        sN &= ",Διαθέσιμο Απόθεμα "
            '        Select Case wh
            '            Case 4
            '                sN &= "Θεσ/νίκη"
            '            Case 13
            '                sN &= "Βαθύλακκος"
            '            Case 8
            '                sN &= "Ασπρόπυργος"
            '            Case 5
            '                sN &= "Πύργος"
            '            Case 539
            '                sN &= "Φυτοθρεπτική"
            '        End Select
            '    Next
            '    sF &= ",avlqty_tot,name"
            '    sN &= ",Διαθέσιμο Απόθεμα Σύνολο,Περιγραφή Είδους"
            '    myArrF = sF.Split(",")
            '    myArrN = sN.Split(",")
            'End If
#End Region

            If Me.radioBtnReOrder.Checked Then
                myArrF = ("MTRL,CODE,NAME,un_Name,NUM05,reorder").Split(",")
                myArrN = ("MTRL,Κωδικός,Περιγραφή Είδους,MM,A/A,ReOrder").Split(",")
            End If

            Me.MasterTopDataGridView.Visible = False
            Me.MasterBottomDataGridView.Visible = False

            If Me.radioBtnItemsStatement.Checked Then
                'myArrF = ("TRNDATE,FINCODE,TPRMS,WHOUSE,X_NAME,QTY1,IMPQTY1,IMPVAL,EXPQTY1,EXPVAL,REMQ,REMV,COMMENTS").Split(",")
                'myArrN = ("Ημερ/νία,Παραστατικό,Κίνηση,Α.Χ.,Επωνυμία,Ποσ.1,Ποσ.εισαγωγών,Αξία εισαγωγών,Ποσ.εξαγωγών,Αξία εξαγωγών,Υπόλοιπο ποσ.,Υπόλοιπο αξία,Αιτιολογία").Split(",")
                myArrF = ("AA,TRNDATE,FINCODE,TPRMS,X_TPRMSNAME,WHOUSE,X_NAME,QTY1,IMPQTY1,EXPQTY1,REMQ,COMMENTS").Split(",")
                myArrN = ("A/A,Ημερ/νία,Παραστατικό,TPRMS,Κίνηση,Α.Χ.,Επωνυμία,Ποσ.1,Ποσ.εισαγωγών,Ποσ.εξαγωγών,Υπόλοιπο ποσ.,Αιτιολογία").Split(",")
                Me.MasterTopDataGridView.Visible = True
                'Me.MasterBottomDataGridView.Visible = True
                Me.MasterTopDataGridView.ScrollBars = ScrollBars.Vertical
            End If

            If Me.radioBtnDailyReport.Checked Then
                myArrF = ("SHORTCUT,WHNAME,CODE,NAME,MTRUNITNAME,TBAL,test").Split(",")
                myArrN = ("Παραγγελίες Τρεχ.Ημέρας(18/04),Παραγγελίες Τρεχ.Μηνός(01-18/04),Φορτώσεις Τρεχ.Ημέρας(18/04),Φορτώσεις Τρεχ.Μηνός(01-18/04),Υπάρχον,Ενσακκισμένο Λίπασμα,Χύμα Λίπασμα,Φυσικό Απόθεμα,Εκκρεμείς Παραγγελίες(Χωρίς τα Καράβια),Διαθέσιμα Αποθέματα(Σύνολο)18/04,Φορτώσεις Καλλιεργητικού 1/7/17-18/04/18,Περιγραφή Είδους").Split(",")
            End If


            If Me.RadioBtnExportData.Checked Then
                'COMPANY,FINDOC,TRNDATE,INSDATE,FPRMS,FP_NAME,FINCODE,SL_DNAME,TRDR,P_CODE,P_NAME,TRDBRANCH,TB_CODE,TB_NAME,SHORTCUT,DISTRICT1,DI_NAME,MTRL,M_CODE,M_NAME,MTRUNIT1,MU_SHORTCUT,QTY1,QTY1COV,QTY1CANC
                myArrF = ("TRNDATE,INSDATE,FPRMS,FP_NAME,FINCODE,SL_DNAME,P_CODE,P_NAME,TB_CODE,TB_NAME,SHORTCUT,DI_NAME,M_CODE,M_NAME,MU_SHORTCUT,QTY1,QTY1COV,QTY1CANC,DIAFORA").Split(",")
                myArrN = ("Ημερ/νία,Ημερ.εισαγωγής,Τύπος,Τύπος Περιγραφή,Παραστατικό,Πωλητής,Πελάτης,Επωνυμία πελάτη,Παραλήπτης,Επωνυμία Παραλήπτη,Εγκατάσταση,Νομός,Κωδικός,Περιγραφή,Μ.Μ.(Τ),Ποσ.1,Εκτελ.,Ακυρ.,Διαφορά").Split(",")
            End If


            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
            If Not Me.radioBtnItemsStatement.Checked Then
                AddOutOfOfficeColumn(Me.MasterDataGridView)
                For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
                    Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
                Next
            End If

            Me.MasterDataGridView.ColumnHeadersVisible = True
            If Me.radioBtnItemsStatement.Checked Then
                Me.MasterDataGridView.ColumnHeadersVisible = False
                'Dim qq As New List(Of Hglp.GetItemsStatementsResult1)
                'qq.Add(New Hglp.GetItemsStatementsResult1)
                'qq(0).QTY1 = 703.42

                'Dim MasterTopBindingSource As New BindingSource
                'MasterTopBindingSource.DataSource = qq

                'Me.MasterTopDataGridView.DataSource = qq
                RemoveGridColumnsByCollection(MasterTopDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
                'AddOutOfOfficeColumn(Me.MasterTopDataGridView)

                Me.MasterBottomDataGridView.DataSource = New Hglp.GetItemsStatementsResult1
                RemoveGridColumnsByCollection(MasterBottomDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)
                'AddOutOfOfficeColumn(Me.MasterBottomDataGridView)
                Me.MasterBottomDataGridView.ColumnHeadersVisible = False
                If Not IsNothing(MasterDataGridView.Columns("Επωνυμία")) Then
                    'MasterDataGridView.Columns("Επωνυμία").HeaderText = "Επωνυμία" & vbCrLf & "Απογραφή"
                    'Dim mm As DataGridViewTextBoxCell = MasterDataGridView.Rows(-1).Cells("Επωνυμία")

                End If


            End If

            If Me.radioBtnReOrder.Checked Then
                'Dim columnTxtBox As New DataGridViewTextBoxColumn
                ''columnTxtBox.DataPropertyName = "CodeExp"
                'columnTxtBox.HeaderText = "ReOrder"
                'columnTxtBox.Name = "ReOrder"
                'columnTxtBox.SortMode = DataGridViewColumnSortMode.Automatic
                'columnTxtBox.DefaultCellStyle.Format = "N0"
                'Me.MasterDataGridView.Columns.Add(columnTxtBox)
            End If

            For Each col In MasterDataGridView.Columns
                Try
                    Dim t As Type = col.ValueType
                    If Not IsNothing(t) Then
                        With col
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    .DefaultCellStyle.Format = "N3"
                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                .DefaultCellStyle.Format = "N3"
                            End If
                            'If col.ValueType.Name = "String" Then
                            '    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            '    '.Width = 200
                            'End If
                            'If col.ValueType.Name <> "String" Then
                            '    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            'End If
                        End With
                    End If
                Catch ex As Exception
                    MsgBox("Public Sub RemoveGridColumns" & vbCrLf & ex.Message)
                End Try
            Next


            If Not IsNothing(MasterDataGridView.Columns("Ποσότητα Αποθέματος")) Then
                MasterDataGridView.Columns("Ποσότητα Αποθέματος").DefaultCellStyle.Format = "N3"
            End If

            If Not IsNothing(MasterDataGridView.Columns("ReOrder")) Then
                MasterDataGridView.Columns("ReOrder").DefaultCellStyle.Format = "N0"
            End If

            If Not IsNothing(MasterDataGridView.Columns("A/A")) Then
                MasterDataGridView.Columns("A/A").ReadOnly = False
                MasterDataGridView.Columns("A/A").Width = 50
            End If

            If Not IsNothing(MasterDataGridView.Columns("ΑΧ")) Then
                MasterDataGridView.Columns("ΑΧ").Width = 50
            End If

            Me.MasterDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Me.MasterDataGridView.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 9.75F, FontStyle.Bold)

            'Fill Unbound Collumns
            For Each row As DataGridViewRow In MasterDataGridView.Rows
                Dim item = row.DataBoundItem
                If Not IsNothing(item) Then
                    Try
                        'Dim dll As DataGridViewComboBoxCell = row.Cells("DepartmentDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Items.AddRange(New Object() {"ΔΙΚΗΓΟΡΟΣ", "ΥΠΑΛΛΗΛΟΣ"})
                        ''If Not IsNothing(item.Department) Then
                        ''    dll.Value = dll.Items(item.Department)
                        ''End If


                        'dll = row.Cells("domUserDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        'dll.Value = dll.Items(0)
                        'If Not IsNothing(CCCVShipms) Then
                        '    'Dim OrLgs = CCCVShipms.
                        '    '    Where(Function(f) Not item.user_loginname = Nothing AndAlso f.domUser.Contains(item.user_loginname)).
                        '    '    Select(Function(f) f.domUser).Distinct.ToList
                        '    'dll.Items.AddRange(OrLgs.ToArray)

                        '    'If OrLgs.Count = 1 Then
                        '    '    dll.Value = OrLgs.FirstOrDefault
                        '    'End If
                        'End If


                        'dll = row.Cells("CapacityDataGridViewComboBoxColumn")
                        'dll.Items.Clear()
                        'dll.Items.Add("--Επιλέγξτε--")
                        ''If Not IsNothing(item.Capacity) Then
                        ''    For Each st In item.Capacity.Split("|")
                        ''        dll.Items.Add(st)
                        ''    Next
                        ''    dll.Value = item.Capacity.Split("|")(0)
                        ''Else
                        ''    dll.Items.AddRange(New Object() {"ALB", "EFG", "PIR"})
                        ''End If


                    Catch ex As Exception

                    End Try
                End If

            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub MasterDataGridView_Sorted(sender As Object, e As EventArgs) Handles MasterDataGridView.Sorted
        MasterDataGridView_Styling()
    End Sub
    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles TlSBtnCheck.Click, TlSBtnUnCheck.Click
        Dim s As ToolStripButton = sender
        Dim check As Boolean = False
        If s.Name = "TlSBtnCheck" Then
            check = True
        Else
            check = False
        End If
        If Me.MasterDataGridView.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.MasterDataGridView.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                If Not ds.Cells("Check").Value = check Then
                    ds.Cells("Check").Value = check
                End If
            Next
            'For i As Integer = 0 To DrSel.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
            'Next
        Else
            For Each ds As DataGridViewRow In Me.MasterDataGridView.Rows
                ds.Cells("Check").Value = check
            Next
            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
            'Next
        End If
    End Sub

    Private Sub MasterDataGridView_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MasterTopDataGridView.CellFormatting, MasterDataGridView.CellFormatting
        If e.ColumnIndex > 0 AndAlso Not IsNothing(e.Value) Then
            If e.Value.ToString = "0" Then
                e.Value = ""
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub MasterDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MasterDataGridView.CellValidating
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).Name = "ReOrder" Then
            'Dim cell As DataGridViewCell = s.CurrentCell
            'Dim reOrder As String = cell.EditedFormattedValue
            'If reOrder = "" Then
            '    Exit Sub
            'End If
            'If Not cell.FormattedValue.ToString = reOrder Then
            '    Dim count = Me.MasterDataGridView.Rows.Cast(Of DataGridViewRow).Where(Function(f) Not f.Cells("ReOrder").EditedFormattedValue = Nothing).Count

            '    Dim q As IQueryable(Of _reorder) = Me.MasterBindingSource.DataSource

            '    If Not count = 1 Then
            '        MsgBox("Προσοχή !!! Δεν επιτρέπεται πολλαπλή επιλογή για ReOrder.", MsgBoxStyle.Critical, "")
            '        Exit Sub
            '    End If
            '    Dim item As _reorder = s.Rows(e.RowIndex).DataBoundItem

            '    q = q.Where(Function(f) f.num05 >= reOrder)
            '    Dim mx As New MTREXTRA
            '    For Each q1 In q
            '        mx = db.MTREXTRAs.Where(Function(f) f.MTRL = q1.mtrl).FirstOrDefault
            '        mx.NUM05 += 1
            '    Next
            '    mx = db.MTREXTRAs.Where(Function(f) f.MTRL = item.mtrl).FirstOrDefault
            '    mx.NUM05 = reOrder
            '    Me.BindingNavigatorSaveItem.Enabled = True
            'End If
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles MasterDataGridView.DataError

        If sender.Columns(e.ColumnIndex).Name = "ReOrder" Then
            Exit Sub
        End If

        MessageBox.Show("Error happened " _
            & e.Context.ToString() & vbCrLf & "Row,Col:" & e.RowIndex & "," & sender.Columns(e.ColumnIndex).Name)

        If (e.Context = DataGridViewDataErrorContexts.Commit) _
            Then
            MessageBox.Show("Commit error")
        End If
        If (e.Context = DataGridViewDataErrorContexts _
            .CurrentCellChange) Then
            MessageBox.Show("Cell change")
        End If
        If (e.Context = DataGridViewDataErrorContexts.Parsing) _
            Then
            MessageBox.Show("parsing error")
        End If
        If (e.Context =
            DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If

        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"

            e.ThrowException = False
        End If
    End Sub
    Private Sub MasterDataGridView_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MasterDataGridView.EditingControlShowing
        'Dim s As GmDataGridView = sender
        'Dim cell As DataGridViewCell = s.CurrentCell
        ''Dim r = cell.OwningRow.Cells("")..Cells("MTRL")
        'If cell.ColumnIndex = 2 Then
        '    'Dim c As ComboBox = CType(e.Control, ComboBox)
        'End If

    End Sub
    Private Sub MasterDataGridView_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MasterDataGridView.CellMouseDown
        If e.Button = MouseButtons.Right Then
            '''''Dim hti = MasterDataGridView.HitTest(e.X, e.Y)
            '''''MasterDataGridView.ClearSelection()
            '''''MasterDataGridView.Rows(e.RowIndex).Selected = True
            ''''Dim fabs As New FormUsersSet
            '''''fabs.Conn = FormMain.Conn
            ''''fabs.OrUs = MasterDataGridView.Rows(e.RowIndex).DataBoundItem

            '''''Dim sts As New List(Of CCCSTATUS)
            '''''Dim st As New CCCSTATUS
            '''''Dim count = 0
            '''''For Each cc In ("--Επιλέγξτε--,ΕΡΓΑΣΙΑ,ΑΣΘΕΝΕΙΑ,ΑΔΕΙΑ,REPO,ΑΛΛΟ").Split(",")
            '''''    st = New CCCSTATUS
            '''''    st.ID = count
            '''''    count += 1
            '''''    If cc = "ΕΡΓΑΣΙΑ" Then
            '''''        Continue For
            '''''    End If
            '''''    st.DESCR = cc
            '''''    sts.Add(st)
            '''''Next
            '''''Me.StateBindingSource.DataSource = GetState()

            ''''''
            ''''''StateDataGridViewComboBoxColumn
            ''''''
            '''''Dim ddlState = fabs.ddlState
            ''''''StateDataGridViewComboBoxColumn.DataPropertyName = "State"
            '''''ddlState.DataSource = Me.StateBindingSource
            '''''ddlState.DisplayMember = "DESCR"
            ''''''StateDataGridViewComboBoxColumn.HeaderText = "State"
            ''''''StateDataGridViewComboBoxColumn.Items.AddRange(New Object() {"--Επιλέγξτε--", "ΕΡΓΑΣΙΑ", "ΑΣΘΕΝΕΙΑ", "ΑΔΕΙΑ", "REPO", "ΑΛΛΟ"})
            '''''ddlState.Name = "StateComboBox"
            ''''''StateDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            ''''''StateDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
            '''''ddlState.ValueMember = "ID"

            ''''fabs.ShowDialog()
            '''''Cmd_Select()
        End If
    End Sub
    Private Sub DataGridViewMaster_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles MasterDataGridView.ColumnWidthChanged
        If Me.MasterTopDataGridView.Columns.Count > 0 Then
            Me.MasterTopDataGridView.Columns(e.Column.Index).Width = e.Column.Width
        End If
    End Sub
    Private Sub MasterTopDataGridView_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles MasterTopDataGridView.ColumnWidthChanged
        If Me.MasterDataGridView.Columns.Count > 0 Then
            Me.MasterDataGridView.Columns(e.Column.Index).Width = e.Column.Width
        End If
    End Sub
    Private Sub DataGridViewMaster_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles MasterDataGridView.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            Me.MasterTopDataGridView.HorizontalScrollingOffset = Me.MasterDataGridView.HorizontalScrollingOffset '_Scroll(DataGridView1, e)
        End If
    End Sub
    Private Sub MasterTopDataGridView_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles MasterTopDataGridView.Sorted
        If Not MasterTopDataGridView.SortedColumn Is Nothing Then
            Me.MasterBindingSource.Sort = MasterTopDataGridView.SortedColumn.DataPropertyName & " " & IIf(MasterDataGridView.SortOrder = 1, "ASC", "DESC") '"RDATE" 
            'MasterBindingSource_PositionChanged(Me.MasterBindingSource, Nothing)
        End If
    End Sub
    Private Sub MasterDataGridView_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles MasterDataGridView.CellPainting
        If e.RowIndex = -1 And e.ColumnIndex = 5 Then
            'Dim cl As DataGridViewCell = Me.MasterDataGridView.Rows(-1).Cells(5)
            'e.Graphics.FillRectangle(Brushes.Blue, e.CellBounds)
            'e.PaintContent(e.ClipBounds)
            'e.Handled = True
        End If
    End Sub

#End Region
#Region "97- Control Events"
    'Private Sub BindingNavigatorAddNewItem_Click(sender As System.Object, e As System.EventArgs) Handles ΑΠΟΓΡΑΦΗToolStripMenuItem.Click, ΕΞΑΓΩΓΕΣToolStripMenuItem.Click, ΕΙΣΑΓΩΓΕΣToolStripMenuItem.Click
    '    Cmd_Add(sender)
    'End Sub
    'Private Sub BindingNavigatorDeleteItem_Click(sender As System.Object, e As System.EventArgs) Handles BindingNavigatorDeleteItem.Click
    '    Cmd_Delete()
    'End Sub
    'Private Sub BindingNavigatorSaveItem_Click(sender As System.Object, e As System.EventArgs)
    '    Me.Validate()
    '    Me.MasterBindingSource.EndEdit()
    'End Sub
    Private Sub cmdSelect_Click(sender As System.Object, e As System.EventArgs) Handles cmdSelect.Click
        Cmd_Select()
    End Sub
    Private Sub OpenToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripButton.Click
        Cmd_Edit()
    End Sub
    'Private Sub txtBoxLName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBoxLName.TextChanged
    '    Dim s As TextBox = sender
    '    Dim rowFound As Cross1.Member = (From g As Cross1.Member In Me.MasterBindingSource Where g.Name.ToString.ToUpper Like s.Text.Trim.ToUpper & "*").FirstOrDefault()
    '    If Not IsNothing(rowFound) Then
    '        'Dim itemFound As Integer = Me.MasterBindingSource.Find("Name", row3.Name.ToString)
    '        Dim itemFound As Integer = Me.MasterBindingSource.IndexOf(rowFound)
    '        Me.MasterBindingSource.Position = itemFound
    '    End If
    'End Sub
    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click
        Try
            'Dim q = Me.MasterBindingSource.List.OfType(Of ccCVShipment).ToList().Where(Function(f) f.FINDOC = 0).FirstOrDefault
            'If IsNothing(q) Then
            '    Me.MasterBindingSource.AddNew()
            '    Dim nu As ccCVShipment = Me.MasterBindingSource.Current
            '    'nu.user_type = "User"
            '    'nu.createdOn = Now()
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If DateTimePicker1.Value = "01/01/" & Year(CTODate) Then
            DateTimePicker1.Value = CTODate
        Else
            DateTimePicker1.Value = "01/01/" & Year(CTODate)
        End If
    End Sub
    Private Sub TlSBtn_Click(sender As Object, e As EventArgs) Handles TlSBtnSERIES.Click, TlSBtnPRSN.Click, TlSBtnWHOUSE.Click, TlSBtnWHOUSEG.Click, TlSBtnFPRMS.Click, TlSBtnTRDR.Click, TlSBtnMTRL.Click
        Dim ee As New System.ComponentModel.CancelEventArgs
        ee.Cancel = False
        TlSTextBox_Validating(sender, ee)
    End Sub
    Private Sub TlSTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TlSTxtSERIES.Validating, TlSTxtPRSN.Validating
        Dim s As Object = sender
        'e.Cancel = False
        Dim Valid As Boolean = False
        Dim SelectSender As String = ""
        Select Case sender.GetType.Name
            Case "TextBox"
                SelectSender = s.Name
            Case "ToolStripButton", "ToolStripTextBox"
                SelectSender = s.Name
            Case "MyDataGridView", "GmDgView"
                SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
        End Select
        Dim ReturnFields As New ArrayList
        Dim errorMsg As String = ""
        Dim View As Boolean = False
        Dim GmTitle As String = ""
        'Dim GmTableName As String = ""
        Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
        Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
        Dim GmCheck As Boolean = False
        Dim ValidField As String = ""
        Dim sender_TAG As String = ""
        Dim Visible As Boolean = False
        Dim GmPelPro As Byte = 0
        RsWhere = "1=1"
        RsOrder = ""
        Select Case SelectSender
            Case "TlSBtnSERIES", "TlSTxtSERIES"
                TlSTxtSERIES.Tag = "SERIES"
                ReturnFields.Add(TlSTxtSERIES)
                GmTitle = "Σειρές"
                RsTables = "SERIES"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "SERIES"
                'SELECT A.COMPANY,A.SOSOURCE,A.SOREDIR,A.SERIES,A.CODE,A.FPRMS,A.NAME,A.ISACTIVE,A.BRANCH,A.WHOUSE FROM SERIES A WHERE A.COMPANY=1000 AND A.COMPANY IN (1000) AND A.SOSOURCE=1351 AND A.ISACTIVE=1 ORDER BY A.SERIES,A.COMPANY,A.SOSOURCE
                sSQL = "SELECT SERIES,CODE,NAME FROM SERIES "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("SERIES,CODE,NAME").Split(",")
                myArrN = ("Σειρά,Σύντμηση,Περιγραφή").Split(",")
                GmCheck = True

            Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                TlSTxtFPRMS.Tag = "FPRMS"
                ReturnFields.Add(TlSTxtFPRMS)
                GmTitle = "Τύποι"
                RsTables = "FPRMS"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SOSOURCE = 1351 AND ISACTIVE=1"
                RsOrder = "FPRMS"
                sSQL = "SELECT FPRMS, NAME FROM FPRMS "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("FPRMS,NAME").Split(",")
                myArrN = ("Τύπος,Περιγραφή").Split(",")
                GmCheck = True

            Case "TlSBtnTRDR", "TlSTxtTRDR"
                TlSTxtTRDR.Tag = "CODE"
                ReturnFields.Add(TlSTxtTRDR)
                GmTitle = "Ευρετήριο Πελατών"
                RsTables = "TRDR"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SODTYPE=13 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM TRDR "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Επωνυμία").Split(",")

            Case "TlSBtnMTRL", "TlSTxtMTRL"
                TlSTxtMTRL.Tag = "CODE"
                ReturnFields.Add(TlSTxtMTRL)
                GmTitle = "Ευρετήριο Ειδών"
                RsTables = "MTRL"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SODTYPE=51 AND ISACTIVE=1" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE"

                sSQL = "SELECT CODE, NAME FROM MTRL "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME").Split(",")
                myArrN = ("Κωδικός,Περιγραφή").Split(",")

            Case "TlSBtnPRSN", "TlSTxtPRSN"
                TlSTxtPRSN.Tag = "CODE"
                ReturnFields.Add(TlSTxtPRSN)
                GmTitle = "Ευρετήριο Πωλητών"
                RsTables = "PRSN"

                Company = 1000

                RsWhere = "Company = " & Company & " AND SODTYPE=20 AND TPRSN=0" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsOrder = "CODE,PRSN"
                'SELECT A.COMPANY,A.SODTYPE,A.PRSN,A.CODE,A.NAME,A.NAME2,A.NAME3,A.ISACTIVE,A.TPRSN,A.AFM,A.IDENTITYNUM FROM PRSN A WHERE A.COMPANY=1000 AND A.SODTYPE=20 AND A.TPRSN=0 ORDER BY A.CODE,A.PRSN
                sSQL = "SELECT CODE,NAME,NAME2 FROM PRSN "
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("CODE,NAME2,NAME").Split(",")
                myArrN = ("Κωδικός,Επώνυμο,Όνομα").Split(",")
                GmCheck = True

            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                TlSTxtWHOUSE.Tag = "WHOUSE"
                'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
                ReturnFields.Add(TlSTxtWHOUSE)
                'ReturnFields.Add(TlSTxtTimKin_Descr)
                ''''''''''''''''''''''''''''''''''''''''
                GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
                RsTables = "WHOUSE"
                '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
                '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
                Company = 1000

                RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsWhere = Trim(RsWhere)
                RsOrder = "SHORTCUT"
                'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
                sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
                'GmPelPro = 3 'Δεν υπάρχη PelPro Field
                'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
                myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
                GmCheck = True

            Case "TlSBtnWHOUSEG", "TlSTxtWHOUSEG"
                TlSTxtWHOUSEG.Tag = "WHOUSEG"
                'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
                ReturnFields.Add(TlSTxtWHOUSEG)
                'ReturnFields.Add(TlSTxtTimKin_Descr)
                ''''''''''''''''''''''''''''''''''''''''
                GmTitle = "Ευρετήριο Ομάδων"
                RsTables = "WHOUSEG"
                '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
                '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
                Company = 1000

                RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsWhere = Trim(RsWhere)
                RsOrder = "WHOUSEG"
                'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
                sSQL = "SELECT WHOUSEG, NAME, ISACTIVE FROM dbo.WHOUSEG"
                'GmPelPro = 3 'Δεν υπάρχη PelPro Field
                'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("WHOUSEG,NAME").Split(",")
                myArrN = ("Ομάδα A.X,Ονομασία").Split(",")
                GmCheck = True
        End Select
        Dim m_dtGen As DataTable = Nothing
        Try
            If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
                ValidField = Trim(ReturnFields(0).Text)
                If Not ValidField.IndexOf("*").Equals(-1) Then
                    ValidField = ValidField.Replace("*", "%")
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
                ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
                    ValidField = "'" & ValidField.Replace(",", "','") & "'"
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
                Else
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
                End If
                'Visible = True
            Else 'ReturnFields(0).Text = ""
                If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
                    '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
                    For i As Integer = 0 To ReturnFields.Count - 1
                        ReturnFields(i).Text = ""
                        ReturnFields(i).ToolTipText = ""
                    Next

                    'sender.Focus()
                    e.Cancel = False
                    Exit Sub
                    'Visible = True
                End If
            End If
            'RsTables = SelectPelPro(RsTables, PelPro)
            'RsWhere = SelectPelPro(RsWhere, PelPro)
            'RsOrder = SelectPelPro(RsOrder, PelPro)
            Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
            m_dtGen = GmDataN.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
        Catch ex As Exception
            MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
        End Try
        m_dvGen = New DataView(m_dtGen)
        If m_dvGen.Count = 0 Then
            Select Case SelectSender
                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnWHOUSEG", "TlSTxtWHOUSEG"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
            End Select
            MsgBox(errorMsg)
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            'sender.Focus()
            e.Cancel = False
            Exit Sub
            View = False
        ElseIf m_dvGen.Count = 1 Then
            View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
        End If
        If View Or sender.GetType.Name = "ToolStripButton" Then
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            If Not s.GetType.Name = "ToolStripButton" Then
                ErrorProvider1.SetError(s.Control, "")
            End If

            Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
            Dim ar As New ArrayList
            Dim m_ds As New DataSet
            m_ds = GmDataN.GmFillDataSet(conn, m_ds, m_dtGen, m_dtGen.TableName)

            Dim TSearchFR As New SearchNewFR
            TSearchFR.Conn = conn
            TSearchFR.m_ds = m_ds
            TSearchFR.Text = GmTitle
            TSearchFR.GmCheck = GmCheck
            TSearchFR.sSQL = sSQL
            TSearchFR.GmPelPro = Nothing 'GmPelPro
            TSearchFR.RsWhere = RsWhere
            TSearchFR.RsOrder = RsOrder
            TSearchFR.GmGroupSql = GmGroupSql
            TSearchFR.GmGroupSqlField = GmGroupSqlField
            TSearchFR.RsTables = RsTables
            TSearchFR.myArrF = myArrF
            TSearchFR.myArrN = myArrN
            TSearchFR.Location = Point
            'TSearchFR.Visible = View
            'TSearchFR.RetTBL = New DataTable
            'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            TSearchFR.ShowDialog()

            Dim Dt As DataTable = TSearchFR.RetTBL
            If Not TSearchFR.RetTBL Is Nothing Then

                'ReturnFields.Add(TlSTxtPCODE)
                'ReturnFields.Add(TlSTxtPELNAME1)
                If TSearchFR.GmCheck = True Then
                    Dt.DefaultView.RowFilter = "Check = True "
                End If
                Dim dv = Dt.DefaultView
                If dv.Count > 0 Then
                    If dv.Count = 1 Then
                        'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
                        'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
                        m_dvGen = dv 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
                    Else
                        Dim Result As String = ""
                        For i As Integer = 0 To dv.Count - 1
                            Result += Trim(dv(i)(0)) & "," '("P1_PCODE")
                        Next
                        ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
                        Select Case SelectSender
                            Case "TlSBtnWHOUSEG", "TlSTxtWHOUSEG"
                                'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                                'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                                'ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSEG"), GetType(Short))
                                Dim gg As String() = ReturnFields(0).Text.ToString.Split(",")
                                Dim whs = db.WHOUSEs.Where(Function(f) gg.Contains(f.WHOUSEG)).ToList
                                If whs.Count > 0 Then
                                    Me.TlSTxtWHOUSE.Text = ""
                                    For Each wh In whs
                                        Me.TlSTxtWHOUSE.Text &= wh.WHOUSE & ","
                                    Next
                                    Me.TlSTxtWHOUSE.Text = Me.TlSTxtWHOUSE.Text.Substring(0, Me.TlSTxtWHOUSE.Text.Length - 1)
                                End If
                        End Select
                    End If
                    ReturnFields(0).ToolTipText = ReturnFields(0).Text
                    'GmDgLookUp_FillNew = SearceArrayList
                Else
                    '    MsgBox("Δεν υπάρχουν Εγγραφές")
                    'If Not IsNumeric(TextBox1.Text) Then
                    If Not s.GetType.Name = "ToolStripButton" Then
                        'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
                    End If
                    'Else
                    '    ' Clear the error.
                    '    ErrorProvider1.SetError(TextBox1, "")
                    'End If
                End If
            Else
                MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
            End If
        End If

        If m_dvGen.Count = 1 Then
            'Dim data_row As DataRowView
            'data_row = bindingTim1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
                Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))

                Case "TlSBtnSERIES", "TlSTxtSERIES"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("SERIES"), GetType(Short))
                Case "TlSBtnFPRMS", "TlSTxtFPRMS"
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("FPRMS"), GetType(Short))
                Case "TlSBtnTRDR", "TlSTxtTRDR"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnMTRL", "TlSTxtMTRL"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnPRSN", "TlSTxtPRSN"
                    ReturnFields(0).Text = If(m_dvGen(0)("CODE"), "")
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))
                Case "TlSBtnWHOUSEG", "TlSTxtWHOUSEG"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSEG"), GetType(Short))
                    Dim gg As String() = ReturnFields(0).Text.ToString.Split(",")
                    Dim whs = db.WHOUSEs.Where(Function(f) gg.Contains(f.WHOUSEG)).ToList
                    If whs.Count > 0 Then
                        Me.TlSTxtWHOUSE.Text = ""
                        For Each wh In whs
                            Me.TlSTxtWHOUSE.Text &= wh.WHOUSE & ","
                        Next
                        Me.TlSTxtWHOUSE.Text = Me.TlSTxtWHOUSE.Text.Substring(0, Me.TlSTxtWHOUSE.Text.Length - 1)
                    End If

            End Select
        End If
        If ReturnFields.Count = 3 Then
            ReturnFields(ReturnFields.Count - 1).Focus()
        End If
    End Sub

    Private Sub ToolStripTextBox_Validatingold(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) ' Handles TlSTxtWHOUSE.Validating
        Dim s As Object = sender
        'e.Cancel = False
        Dim Valid As Boolean = False
        Dim SelectSender As String = ""
        Select Case sender.GetType.Name
            Case "TextBox"
                SelectSender = s.Name
            Case "ToolStripButton", "ToolStripTextBox"
                SelectSender = s.Name
            Case "MyDataGridView", "GmDgView"
                SelectSender = s.Columns(s.CurrentCell.ColumnIndex).Name
        End Select
        Dim ReturnFields As New ArrayList
        Dim errorMsg As String = ""
        Dim View As Boolean = False
        Dim GmTitle As String = ""
        'Dim GmTableName As String = ""
        Dim GmGroupSql As String = "" ' "SELECT DISTINCT CODE, DESCR as GENDSCR_DESCR, TYPE FROM GENDSCR WHERE Type = 2 ORDER BY TYPE,CODE"
        Dim GmGroupSqlField As String = "" ' "GENDSCR_DESCR"
        Dim GmCheck As Boolean = False
        Dim ValidField As String = ""
        Dim sender_TAG As String = ""
        Dim Visible As Boolean = False
        Dim GmPelPro As Byte = 0
        RsWhere = "1=1"
        RsOrder = ""
        Select Case SelectSender

            Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                TlSTxtWHOUSE.Tag = "WHOUSE"
                'TlSTxtTimKin_Descr.Tag = "TPRMS_NAME"
                ReturnFields.Add(TlSTxtWHOUSE)
                'ReturnFields.Add(TlSTxtTimKin_Descr)
                ''''''''''''''''''''''''''''''''''''''''
                GmTitle = "Ευρετήριο ΑΠΟΘΗΚΩΝ"
                RsTables = "WHOUSE"
                '(dbo.FINDOC.COMPANY = 1000) And (dbo.FINDOC.SOSOURCE = 1351) And (dbo.FINDOC.SOREDIR = 0) And (dbo.FINDOC.TRNDATE >= '20170701') AND (dbo.FINDOC.TRNDATE < '20170801') AND (dbo.FINDOC.TFPRMS IN (201))
                '         And (dbo.FINDOC.SODTYPE = 13) And (dbo.FINDOC.FULLYTRANSF IN (0, 2)) And (dbo.MTRLINES.PENDING >= 1)
                Company = 1000

                RsWhere = "Company = " & Company '& " AND SOSOURCE = 1351" 'SODTYPE = 13" ' AND TPRMS IN (2001, 2002, 5011, 9051)"
                RsWhere = Trim(RsWhere)
                RsOrder = "SHORTCUT"
                'sSQL = "SELECT TPRMS, NAME AS TPRMS_NAME FROM TPRMS"
                sSQL = "SELECT WHOUSE, SHORTCUT, NAME FROM dbo.WHOUSE"
                'GmPelPro = 3 'Δεν υπάρχη PelPro Field
                'sender_TAG = Replace(ReturnFields(0).Tag, "P1_", "", , , CompareMethod.Text)
                sender_TAG = ReturnFields(0).Tag
                myArrF = ("WHOUSE,SHORTCUT,NAME").Split(",")
                myArrN = ("A.X,Εγκατάσταση,Ονομασία").Split(",")
                View = True
        End Select
        Dim m_dtGen As DataTable = Nothing
        Try
            If Not ReturnFields(0).Text = "" Then 'Εαν records > 1 Να ανοίξη την SearchFR με κρητίρια
                ValidField = Trim(ReturnFields(0).Text)
                If Not ValidField.IndexOf("*").Equals(-1) Then
                    ValidField = ValidField.Replace("*", "%")
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " LIKE '" & ValidField & "'"
                ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
                    ValidField = "'" & ValidField.Replace(",", "','") & "'"
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " IN (" & ValidField & ")"
                Else
                    RsWhere = Trim(RsWhere) & " AND " & sender_TAG & " = '" & ValidField & "'"
                End If
                'Visible = True
            Else 'ReturnFields(0).Text = ""
                If Not sender.GetType.Name = "ToolStripButton" Then 'sender.GetType.Name <> "ToolStripButton"
                    '    'Δηλαδή  If ReturnFields(0).Text = "" αλλά δεν προέρχετε από ToolStripButton να μή κάνη τίποτα
                    For i As Integer = 0 To ReturnFields.Count - 1
                        ReturnFields(i).Text = ""
                        ReturnFields(i).ToolTipText = ""
                    Next

                    'sender.Focus()
                    e.Cancel = False
                    Exit Sub
                    'Visible = True
                End If
            End If
            'RsTables = SelectPelPro(RsTables, PelPro)
            'RsWhere = SelectPelPro(RsWhere, PelPro)
            'RsOrder = SelectPelPro(RsOrder, PelPro)
            Dim mSql As String = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)

            'df = New GmData(sysDB, conn) 'My.Settings.GenConnectionString)
            'm_dtGen = df.GmFillTable(mSql, RsTables)
            m_dtGen = GmDataN.GetTableSQL(conn, CommandType.Text, mSql, , RsTables)
            'Dim dtb As New DataTable
            'Using cnn As New SqlConnection(conn)
            '    cnn.Open()
            '    Using cmd As New SqlCommand(mSql, cnn)
            '        'cmd.Parameters.AddWithValue("@COMPANY", 1000)
            '        'cmd.Parameters.AddWithValue("@SODTYPE", 51) '51 Αποθήκη
            '        'cmd.Parameters.AddWithValue("@DFROM", CDate("01/07/2017")) 'make sure you assign a value To startdate
            '        'cmd.Parameters.AddWithValue("@DTO", CDate("01/08/2017")) 'make sure you assign a value To 

            '        ''cmd.Parameters.AddWithValue("@MTRL", Nothing) ' AS INTEGER = 2115 --63 --NULL --384 --NULL

            '        'cmd.Parameters.AddWithValue("@CODE", "2103030557") ''--'%305%'
            '        'cmd.Parameters.AddWithValue("@WHOUSE", Me.TlSTxtWHOUSE.Text.Replace(",", "|")) '"2|4")
            '        ''--DECLARE @MTRLS  AS VARCHAR(250) = ''


            '        'cmd.Parameters.AddWithValue("@FISCPRD", 2017)
            '        'cmd.Parameters.AddWithValue("@PERIOD", 7)

            '        Try
            '            Using dr As SqlDataReader = cmd.ExecuteReader()
            '                'Dim tb = New DataTable()
            '                dtb.Load(dr)
            '                'Return tb
            '            End Using
            '        Catch ex As Exception
            '            MsgBox(ex.ToString, MsgBoxStyle.Critical, strAppName)
            '        End Try

            '    End Using



            '    'Using dad As New SqlDataAdapter(str, cnn)
            '    '    dad.Fill(dtb)
            '    'End Using
            '    cnn.Close()
            'End Using
            'm_dtGen = dtb
            'm_dtGen.TableName = "WHOUSE"
            If m_dtGen.Rows.Count = 0 And sender.GetType.Name = "ToolStripButton" Then ' If  "ToolStripButton" SearchFR = Όλα
                'RsWhere = "1=1"
                'If GmPelPro = 0 Or GmPelPro = 1 Then
                '    RsWhere = Trim(RsWhere) & " and PELPRO = " & PelPro
                'End If
                'mSql = sSQL & IIf(RsWhere = "", "", " WHERE " & RsWhere) & IIf(RsOrder = "", "", " ORDER BY " & RsOrder)
                'm_dtGen = df.GmFillTable(mSql, RsTables)
            End If
        Catch ex As Exception
            MsgBox("Error" & vbCrLf & ex.Message & vbCrLf & ex.Source & ex.StackTrace)
        End Try
        m_dvGen = New DataView(m_dtGen)
        If m_dvGen.Count = 0 Then
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSBtnPOUDRA", "TlSTxtPROM", "TlSTxtPOUDRA"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnTimKin", "TlSTxtTimKin"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    errorMsg = "Δεν βρέθηκε η Εγγραφή."
            End Select
            MsgBox(errorMsg)
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            'sender.Focus()
            e.Cancel = False
            Exit Sub
            View = False
        ElseIf m_dvGen.Count = 1 Then
            View = False 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1 
        End If
        If View Or sender.GetType.Name = "ToolStripButton" Then
            For i As Integer = 0 To ReturnFields.Count - 1
                ReturnFields(i).Text = ""
                ReturnFields(i).ToolTipText = ""
            Next
            If Not s.GetType.Name = "ToolStripButton" Then
                ErrorProvider1.SetError(s.Control, "")
            End If
            'If Visible Then
            Dim Point As System.Drawing.Point = New System.Drawing.Point(s.GetCurrentParent.Left + 5, s.GetCurrentParent.Top)
            Dim ar As New ArrayList
            Dim m_ds As New DataSet
            m_ds = GmDataN.GmFillDataSet(conn, m_ds, m_dtGen, m_dtGen.TableName)
            'ar.Add(m_ds)
            'ar.Add(GmTitle)
            'ar.Add(GmCheck)
            'ar.Add(sSQL)
            'ar.Add(GmPelPro)
            'ar.Add(RsWhere)
            'ar.Add(RsOrder)
            'ar.Add(GmGroupSql)
            'ar.Add(GmGroupSqlField)
            'ar.Add(RsTables)
            'ar.Add(myArrF)
            'ar.Add(myArrN)
            'ar.Add(Point)
            'ar.Add(Visible)
            Dim TSearchFR As New SearchNewFR
            TSearchFR.Conn = conn
            TSearchFR.m_ds = m_ds
            TSearchFR.Text = GmTitle
            TSearchFR.GmCheck = GmCheck
            TSearchFR.sSQL = sSQL
            TSearchFR.GmPelPro = Nothing 'GmPelPro
            TSearchFR.RsWhere = RsWhere
            TSearchFR.RsOrder = RsOrder
            TSearchFR.GmGroupSql = GmGroupSql
            TSearchFR.GmGroupSqlField = GmGroupSqlField
            TSearchFR.RsTables = RsTables
            TSearchFR.myArrF = myArrF
            TSearchFR.myArrN = myArrN
            TSearchFR.Location = Point
            TSearchFR.GmCheck = True
            'TSearchFR.Visible = View
            'TSearchFR.RetTBL = New DataTable
            'TSearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            TSearchFR.ShowDialog()
            'Dim SearceArrayList As ArrayList = TSearchFR.m_ArrayList 'SearchFR.Me_Load(ar, GmTitle, GmCheck, sSQL, RsWhere, RsOrder, GmGroupSql, GmGroupSqlField, RsTables, myArrF, myArrN, Point, True)
            Dim Dt As DataTable = TSearchFR.RetTBL
            If Not TSearchFR.RetTBL Is Nothing Then

                'ReturnFields.Add(TlSTxtPCODE)
                'ReturnFields.Add(TlSTxtPELNAME1)
                If TSearchFR.GmCheck = True Then
                    Dt.DefaultView.RowFilter = "Check = True "
                End If
                Dim dv = Dt.DefaultView
                If dv.Count > 0 Then
                    If dv.Count = 1 Then
                        'ReturnFields(0).Text = Trim(Dt.Rows(0)(0)) '("P1_PCODE")
                        'ReturnFields(1).Text = Trim(Dt.Rows(0)(1)) '("P1_PELNAME1")
                        m_dvGen = dv 'Ενιαία επιλογή με SearchFR εάν επιστρέψη record = 1
                    Else
                        Dim Result As String = ""
                        For i As Integer = 0 To dv.Count - 1
                            Result += Trim(dv(i)(0)) & "," '("P1_PCODE")
                        Next
                        ReturnFields(0).Text = Trim(Mid(Result, 1, Len(Trim(Result)) - 1))
                    End If
                    ReturnFields(0).ToolTipText = ReturnFields(0).Text
                    'GmDgLookUp_FillNew = SearceArrayList
                Else
                    '    MsgBox("Δεν υπάρχουν Εγγραφές")
                    'If Not IsNumeric(TextBox1.Text) Then
                    If Not s.GetType.Name = "ToolStripButton" Then
                        'ErrorProvider1.SetError(s.Control, "Δέν βρέθηκε Εγγραφή")
                    End If
                    'Else
                    '    ' Clear the error.
                    '    ErrorProvider1.SetError(TextBox1, "")
                    'End If
                End If
            Else
                MsgBox("error:SearceArrayList", MsgBoxStyle.Critical)
            End If
        End If

        If m_dvGen.Count = 1 Then
            'Dim data_row As DataRowView
            'data_row = bindingTim1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    'ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
                Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                    ReturnFields(0).Text = Trim(IIf(m_dvGen(0)("CODE").Equals(DBNull.Value), "", m_dvGen(0)("CODE")))
                    ReturnFields(1).Text = Trim(IIf(m_dvGen(0)("NAME").Equals(DBNull.Value), "", m_dvGen(0)("NAME")))
                Case "TlSBtnWHOUSE", "TlSTxtWHOUSE"
                    'ReturnFields(0).Text = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
                    'ReturnFields(1).Text = GmNull(m_dvGen(0)("TPRMS_NAME"), GetType(String
                    ReturnFields(0).Text = GmNull(m_dvGen(0)("WHOUSE"), GetType(Short))
            End Select
            Dim data_row As DataRowView
            'data_row = BindingSource1.Current()
            Select Case SelectSender
                Case "TlSBtnPROM", "TlSTxtPROM"
                    data_row.Item("ETERIA") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer)) 'CInt(Me.GmDgLookUp1.GmReturnFields(2).Text)
                'Case "TlSBtnPOUDRA", "TlSTxtPOUDRA"
                '    If data_row.Item("ETERIA") Is DBNull.Value Then
                '        MsgBox("Προσοχή !!! Δεν Βρέθηκε Προμηθευτής", MsgBoxStyle.Critical, "GmError")
                '        For i As Integer = 0 To ReturnFields.Count - 1
                '            ReturnFields(i).Text = ""
                '            ReturnFields(i).ToolTipText = ""
                '        Next
                '        Exit Sub
                '    End If
                '    data_row.Item("COLOR") = GmNull(m_dvGen(0)("CDIMLINES"), GetType(Integer))
                '    data_row.Item("VAL") = GmNull(m_dvGen(0)("CCCPRICE"), GetType(Double))
                '    STOCKTextBox.Text = GmNull(m_dvGen(0)("CCCSSTOCK"), GetType(Double))
                '    VALTextBox.Text = data_row.Item("VAL")
                '    RsWhere = "Company = " & Company
                '    RsWhere = RsWhere & " AND COLOR = " & data_row.Item("COLOR")
                '    RsWhere = RsWhere & " AND ETERIA = " & data_row.Item("ETERIA")
                '    sSQL = "SELECT TQTY " &
                '           "FROM VTOTCCCPOUDRES AS VT " &
                '           "WHERE " & RsWhere
                '    Dim SumTable As DataTable = df.GmFillTable(sSQL, "VT")
                '    'Dim m_dvSum As DataView = New DataView(SumTable)
                '    If SumTable.Rows.Count = 1 Then
                '        TYPOLQTY = SumTable.Rows(0)("TQTY")
                '    Else
                '        TYPOLQTY = 0
                '    End If
                '    Me.txtCTQTY.Text = Format(TYPOLQTY, "#,###") 'Format(data_row("TQTY") - data_row("QTY"), "#,###.#0")
                Case "TlSBtnTimKin", "TlSTxtTimKin"
                    data_row.Item("KK") = GmNull(m_dvGen(0)("TPRMS"), GetType(Short))
            End Select
        End If
        If ReturnFields.Count = 3 Then
            ReturnFields(ReturnFields.Count - 1).Focus()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cmd_SelectLINQ()
    End Sub

    Private Sub RadioBtnExportData_CheckedChanged(sender As Object, e As EventArgs) Handles RadioBtnExportData.CheckedChanged
        Dim s As RadioButton = sender
        Me.ToolStripComboBoxIndexes.Visible = s.Checked
        Me.ToolStripSeparator3.Visible = s.Checked
    End Sub
    Private Sub RadioBtnExportData_Validated(sender As Object, e As EventArgs) Handles RadioBtnExportData.Validated
        Dim s As RadioButton = sender
        Me.ToolStripComboBoxIndexes.Visible = s.Visible
        Me.ToolStripSeparator3.Visible = s.Visible
    End Sub

    Private Sub ToolStripComboBoxIndexes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBoxIndexes.SelectedIndexChanged
        SelectedIdx(Me.ToolStripComboBoxIndexes.SelectedItem.ToString)
    End Sub

    Private Sub SelectedIdx(selectedItem_Text As String)
        Me.DateTimePicker1.Value = CTODate
        Me.TlSTxtWHOUSE.Text = ""
        Me.TlSTxtFPRMS.Text = ""
        Me.TlSTxtTFPRMS.Text = ""
        Me.TlSTxtFULLYTRANSF.Text = Nothing
        Me.TlSTxtΤRDBUSINES.Text = "10"
        Me.TlSTxtMTRACN.Text = "101,102,103,104,105,106,107"
        '0  Μετασχηματισμός(Όχι)
        '1  Μετασχηματισμός(Πλήρως)
        '2  Μετασχηματισμός(Μερικώς)
        '3  Μετασχηματισμένο


        'Εκκρεμείς παραγγελίες
        'Picking
        'Επιστροφές-Ακυρώσεις Παρημίν
        'Παραγγελίες Μήνα
        'Παραδόσεις Μήνα
        Select Case selectedItem_Text
            Case "Όλα"
                Exit Sub
            Case "Εκκρεμείς παραγγελίες"
                Me.DateTimePicker1.Value = CDate("01/01/2016")
                Me.TlSTxtTFPRMS.Text = "201"
                Me.TlSTxtFULLYTRANSF.Text = "0,2"
            Case "Picking"
                Me.DateTimePicker1.Value = CDate("01/01/2016")
                Me.TlSTxtFPRMS.Text = "1000"
                Me.TlSTxtFULLYTRANSF.Text = "0,2"
            Case "Επιστροφές-Ακυρώσεις Παρημίν"
                Me.DateTimePicker1.Value = CDate("01/01/2016")
                Me.TlSTxtFPRMS.Text = "1000,1061,1066,7001,7021,7023,7039,7040,7041,7042,7043,7045,7046,7047,7060,7061,7062,7063,7064,7066,7067,7068,7069,7071,7072,7073,7076,7082,7083,7084,7127,7141,7143,7144"
                Me.TlSTxtTFPRMS.Text = "154"
            Case "Παραγγελίες Μήνα"
                DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
                Me.TlSTxtTFPRMS.Text = "201"
            Case "Παραδόσεις Μήνα"
                DateTimePicker1.Value = CDate("01/" & CTODate.Month & "/" & Year(CTODate))
                Me.TlSTxtWHOUSE.Text = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,408,445,539"
                Me.TlSTxtFPRMS.Text = "1000,1061,1066,7001,7021,7023,7039,7040,7041,7042,7043,7045,7046,7060,7061,7062,7063,7064,7066,7067,7068,7069,7071,7072,7073,7076,7082,7083,7084,7127,7141,7143,7144"
                Me.TlSTxtTFPRMS.Text = "101,301"
        End Select
        'Throw New NotImplementedException()
    End Sub

#End Region
#Region "99-Start-GetData"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.
        'LoadDataInit() 'For Bind Any Control
    End Sub
    ' Load the data.
    Private Sub LoadData()
        'Data Source=192.168.12.201,55555;Initial Catalog=Softone;Persist Security Info=True;User ID=gm;Password=1mgergm++;Connect Timeout=300
        db = New DataClassesHglpDataContext(conn & ";Connect Timeout=" & Me.txtTimeOut.Text) 'My.Settings.GenConnectionString)
        db.CommandTimeout = Me.txtTimeOut.Text
    End Sub
    Private Sub LoadDataInit()
        Try
            'dbp = New DataClassesDataContext(CONNECT_STRING) 'My.Settings.ALFAConnectionString)
            Dim conString As New SqlConnectionStringBuilder
            db.Connection.ConnectionString = My.Settings.GenConnectionString
            db.CommandTimeout = 360
            'Data Source=192.168.1.102;Initial Catalog=Orario;Persist Security Info=True;User ID=ecollgl;Password=_ecollgl_
            'Data Source=.\SqlExpress;Initial Catalog=Orario;Integrated Security=True
            Me.MasterBindingSource.DataSource = db.ccCVShipments.Where(Function(f) f.FINDOC = 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorSaveItem.Click
        Me.DataSafe()
        Cmd_Select()
    End Sub

    Private Sub MasterBindingSource_ListChanged(sender As Object, e As ListChangedEventArgs) Handles MasterBindingSource.ListChanged
        'If e.ListChangedType = ListChangedType.ItemChanged Then
        '    Dim nu As ccCVShipment = MasterBindingSource.Current
        '    'nu.modifiedOn = Now()
        '    Me.BindingNavigatorSaveItem.Enabled = True
        'End If
        'If e.ListChangedType = ListChangedType.ItemAdded Then
        '    Me.BindingNavigatorSaveItem.Enabled = True
        'End If
    End Sub

    Private Sub MasterBindingSource_AddingNew(sender As Object, e As AddingNewEventArgs) Handles MasterBindingSource.AddingNew

    End Sub

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorDeleteItem.Click

    End Sub

    Private Sub ExcelToolStripButton_Click(sender As Object, e As EventArgs) Handles ExcelToolStripButton.Click
        'Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True
        Dim filename As String = ""
        If Me.radioBtnAnalytical.Checked Then
            filename = "Αναλυτικό "
        End If
        If Me.radioBtnΑggregate.Checked Then
            filename = "Συγκεντρωτικό "
        End If
        If Me.radioBtnItemsStatement.Checked Then
            filename = "Καρτέλες ειδών "
        End If
        If Me.RadioBtnExportData.Checked Then
            filename = "ExportData "
        End If
        saveFileDialog1.FileName = filename & Today().ToShortDateString.Replace("/", "-")
        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            Select Case filename
                Case "Καρτέλες ειδών "
                    ExportToExcelItemsStatement(saveFileDialog1.FileName)
                Case "ExportData "
                    ExportDataToExcel(saveFileDialog1.FileName)
                Case "Συγκεντρωτικό "
                    ExportDataToExcel(saveFileDialog1.FileName, Me.Text)
                Case Else
                    ExportToExcel(saveFileDialog1.FileName)
            End Select

            'myStream = saveFileDialog1.OpenFile()
            'If (myStream IsNot Nothing) Then
            '    ' Code to write the stream goes here.
            '    myStream.Close()
            'End If
        End If
    End Sub
    ''' <summary>
    ''' Creates the data table.
    ''' </summary>
    ''' <returns>DataTable</returns>
    Private Shared Function CreateDataTable() As DataTable
        Dim dt As New DataTable()
        For i As Integer = 0 To 9
            dt.Columns.Add(i.ToString())
        Next

        For i As Integer = 0 To 9
            Dim dr As DataRow = dt.NewRow()
            For Each dc As DataColumn In dt.Columns
                dr(dc.ToString()) = i
            Next

            dt.Rows.Add(dr)
        Next
        Return dt
    End Function
    Private Sub ExportToExcelItemsStatement(fileName As String)
        Using p As New ExcelPackage()
            'Here setting some document properties
            p.Workbook.Properties.Author = "GmLogic"
            p.Workbook.Properties.Title = "Καρτέλες ειδών"

            'Create a sheet
            p.Workbook.Worksheets.Add("Sheet1")
            Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Sheet1")
            'ws.Name = "Sample Worksheet"
            'Setting Sheet's name
            ws.Cells.Style.Font.Size = 11
            'Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"
            'Default Font name for whole sheet

            'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
            'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
            'dt = Utility.LINQToDataTable(db, q)
            'My Function which generates DataTable
            'Merging cells and create a center heading for out table
            'ws.Cells(1, 1).Value = "Sample DataTable Export"
            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            ws.Cells(rowIndex, 2, 1, 3).Merge = True
            ws.Cells(rowIndex, 2).Value = "Καρτέλες ειδών " & Me.DateTimePicker2.Value.ToShortDateString
            ws.Row(rowIndex).Style.Font.Size = 14
            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
            ws.Row(rowIndex).Height = 35.25
            'ws.Column(2).Width = 44.57

            Dim ColNLst As New List(Of String)
            For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                If col.Name = "Check" Then
                    Continue For
                End If
                ColNLst.Add(col.Name)
            Next

            Dim mergeStart As Integer = 4
            Dim mergeEnd As Integer = 0
            For Each ss In ColNLst.Where(Function(f) f.Contains("Απόθεμα Αποθήκης"))
                mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
            Next
            mergeEnd += 1
            'ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
            'ws.Cells(rowIndex, mergeStart).Value = "Απόθεμα Αποθήκης"

            'mergeStart = mergeEnd + 1
            'For Each ss In ColNLst.Where(Function(f) f.Contains("Εκκρεμείς Παραγγελίες"))
            '    mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
            'Next
            'mergeEnd += 1
            'ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
            'ws.Cells(rowIndex, mergeStart).Value = "Εκκρεμείς Παραγγελίες"

            'mergeStart = mergeEnd + 1
            'For Each ss In ColNLst.Where(Function(f) f.Contains("Διαθέσιμο Απόθεμα"))
            '    mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
            'Next
            'mergeEnd += 1
            'ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
            'ws.Cells(rowIndex, mergeStart).Value = "Διαθέσιμο Απόθεμα"


            rowIndex += 1
            For Each col In ColNLst
                'Creating Headings
                Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                Dim cell = ws.Cells(rowIndex, colIndex)
                'Setting the background color of header cells to Gray
                Dim fillpr = cellpr.Style.Fill
                Dim fill = cell.Style.Fill
                fillpr.PatternType = ExcelFillStyle.Solid
                fill.PatternType = ExcelFillStyle.Solid
                fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)
                fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                If col.Contains("Απόθεμα Αποθήκης") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                    fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End If
                If col.Contains("Εκκρεμείς Παραγγελίες") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(226, 239, 218))
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(226, 239, 218))
                End If
                If col.Contains("Διαθέσιμο Απόθεμα") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(221, 235, 247))
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(221, 235, 247))
                End If



                'Setting Top/left,right/bottom borders.
                Dim border = cell.Style.Border
                border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                'Setting Value in cell
                cell.Value = col.Replace("Απόθεμα Αποθήκης ", "").Replace("Εκκρεμείς Παραγγελίες ", "").Replace("Διαθέσιμο Απόθεμα ", "")

                colIndex += 1
            Next

            'ws.Column(ColNLst.Count).Width = 44.57

            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center


            'For Each dc As DataColumn In dt.Columns
            '    'Creating Headings
            '    Dim cell = ws.Cells(rowIndex, colIndex)

            '    'Setting the background color of header cells to Gray
            '    Dim fill = cell.Style.Fill
            '    fill.PatternType = ExcelFillStyle.Solid
            '    fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)


            '    'Setting Top/left,right/bottom borders.
            '    Dim border = cell.Style.Border
            '    border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

            '    'Setting Value in cell
            '    cell.Value = "Heading " + dc.ColumnName

            '    colIndex += 1
            'Next

            'Top
            For Each ro As DataGridViewRow In Me.MasterTopDataGridView.Rows
                ' Adding Data into rows
                colIndex = 1
                rowIndex += 1

                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    If col.Name = "Check" Then
                        Continue For
                    End If
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    cell.Value = ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                    'Setting Value in cell
                    Try
                        With cell
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .Value = CType(ro.Cells(col.Name).Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                                '    If col.ValueType.Name = "String" Then
                                '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                '        '.Width = 200
                                '    End If
                                '    If col.ValueType.Name <> "String" Then
                                '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                '    End If
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    'Setting borders of cell
                    Dim border = cell.Style.Border
                    border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                    colIndex += 1
                Next

            Next

            'Details
            For Each ro As DataGridViewRow In Me.MasterDataGridView.Rows
                ' Adding Data into rows
                colIndex = 1
                rowIndex += 1

                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    If col.Name = "Check" Then
                        Continue For
                    End If
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    cell.Value = ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                    'Setting Value in cell
                    Try
                        With cell
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .Value = CType(ro.Cells(col.Name).Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                                If t.Name = "DateTime" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Date)
                                    .Style.Numberformat.Format = "dd-mm-yyyy"
                                End If

                                '    If col.ValueType.Name = "String" Then
                                '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                '        '.Width = 200
                                '    End If
                                '    If col.ValueType.Name <> "String" Then
                                '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                '    End If
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    'Setting borders of cell
                    Dim border = cell.Style.Border
                    border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                    colIndex += 1
                Next

            Next
            'For Each dr As DataRow In dt.Rows
            '    ' Adding Data into rows
            '    colIndex = 1
            '    rowIndex += 1
            '    For Each dc As DataColumn In dt.Columns
            '        Dim cell = ws.Cells(rowIndex, colIndex)
            '        'Setting Value in cell
            '        cell.Value = Convert.ToInt32(dr(dc.ColumnName)) 'dr(dc.ColumnName) 

            '        'Setting borders of cell
            '        Dim border = cell.Style.Border
            '        border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
            '        colIndex += 1
            '    Next
            'Next

            'colIndex = 0
            'For Each dc As DataColumn In dt.Columns
            '    'Creating Headings
            '    colIndex += 1
            '    Dim cell = ws.Cells(rowIndex, colIndex)

            '    'Setting Sum Formula
            '    cell.Formula = ("Sum(" + ws.Cells(3, colIndex).Address & ":") + ws.Cells(rowIndex - 1, colIndex).Address & ")"

            '    'Setting Background fill color to Gray
            '    cell.Style.Fill.PatternType = ExcelFillStyle.Solid
            '    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
            'Next
            For i As Integer = 9 To ColNLst.Count - 2
                Dim cellsum = ws.Cells(rowIndex + 1, i)
                With cellsum
                    'Setting Sum Formula
                    .Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex, i).Address & ")"
                    .Style.Numberformat.Format = "#,##0.000"

                    'Setting Background fill color to Gray
                    .Style.Fill.PatternType = ExcelFillStyle.Solid
                    .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End With
            Next



            'Generate A File with Random name
            Dim bin As [Byte]() = p.GetAsByteArray()
            Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
            Try
                File.WriteAllBytes(file__1, bin)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub
    Private Sub ExportToExcel(fileName As String)
        Using p As New ExcelPackage()
            'Here setting some document properties
            p.Workbook.Properties.Author = "GmLogic"
            p.Workbook.Properties.Title = "Αναφορά Αποθεμάτων"

            'Create a sheet
            p.Workbook.Worksheets.Add("Sheet1")
            Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Sheet1")
            'ws.Name = "Sample Worksheet"
            'Setting Sheet's name
            ws.Cells.Style.Font.Size = 11
            'Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"
            'Default Font name for whole sheet

            'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
            'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
            'dt = Utility.LINQToDataTable(db, q)
            'My Function which generates DataTable
            'Merging cells and create a center heading for out table
            'ws.Cells(1, 1).Value = "Sample DataTable Export"
            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            ws.Cells(rowIndex, 2, 1, 3).Merge = True
            ws.Cells(rowIndex, 2).Value = "Αποθέματα " & Me.DateTimePicker2.Value.ToShortDateString
            ws.Row(rowIndex).Style.Font.Size = 14
            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
            ws.Row(rowIndex).Height = 35.25
            ws.Column(2).Width = 44.57

            Dim ColNLst As New List(Of String)
            For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                If col.Name = "Check" Then
                    Continue For
                End If
                ColNLst.Add(col.Name)
            Next
            If Not radioBtnPerLine.Checked Then
                Dim mergeStart As Integer = 4
                Dim mergeEnd As Integer = 0
                For Each ss In ColNLst.Where(Function(f) f.Contains("Απόθεμα Αποθήκης"))
                    mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
                Next
                mergeEnd += 1
                ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
                ws.Cells(rowIndex, mergeStart).Value = "Απόθεμα Αποθήκης"

                mergeStart = mergeEnd + 1
                For Each ss In ColNLst.Where(Function(f) f.Contains("Εκκρεμείς Παραγγελίες"))
                    mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
                Next
                mergeEnd += 1
                ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
                ws.Cells(rowIndex, mergeStart).Value = "Εκκρεμείς Παραγγελίες"

                mergeStart = mergeEnd + 1
                For Each ss In ColNLst.Where(Function(f) f.Contains("Διαθέσιμο Απόθεμα"))
                    mergeEnd = ColNLst.FindIndex(Function(f) f = ss)
                Next
                mergeEnd += 1
                ws.Cells(rowIndex, mergeStart, 1, mergeEnd).Merge = True
                ws.Cells(rowIndex, mergeStart).Value = "Διαθέσιμο Απόθεμα"

            End If

            rowIndex += 1
            For Each col In ColNLst
                'Creating Headings
                Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                Dim cell = ws.Cells(rowIndex, colIndex)
                'Setting the background color of header cells to Gray
                Dim fillpr = cellpr.Style.Fill
                Dim fill = cell.Style.Fill
                fillpr.PatternType = ExcelFillStyle.Solid
                fill.PatternType = ExcelFillStyle.Solid
                fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)
                fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                If col.Contains("Απόθεμα Αποθήκης") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                    fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End If
                If col.Contains("Εκκρεμείς Παραγγελίες") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(226, 239, 218))
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(226, 239, 218))
                End If
                If col.Contains("Διαθέσιμο Απόθεμα") Then
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(221, 235, 247))
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(221, 235, 247))
                End If



                'Setting Top/left,right/bottom borders.
                Dim border = cell.Style.Border
                border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                'Setting Value in cell
                cell.Value = col.Replace("Απόθεμα Αποθήκης ", "").Replace("Εκκρεμείς Παραγγελίες ", "").Replace("Διαθέσιμο Απόθεμα ", "")

                colIndex += 1
            Next

            ws.Column(ColNLst.Count).Width = 44.57

            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center


            'For Each dc As DataColumn In dt.Columns
            '    'Creating Headings
            '    Dim cell = ws.Cells(rowIndex, colIndex)

            '    'Setting the background color of header cells to Gray
            '    Dim fill = cell.Style.Fill
            '    fill.PatternType = ExcelFillStyle.Solid
            '    fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)


            '    'Setting Top/left,right/bottom borders.
            '    Dim border = cell.Style.Border
            '    border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

            '    'Setting Value in cell
            '    cell.Value = "Heading " + dc.ColumnName

            '    colIndex += 1
            'Next

            For Each ro As DataGridViewRow In Me.MasterDataGridView.Rows
                ' Adding Data into rows
                colIndex = 1
                rowIndex += 1

                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    If col.Name = "Check" Then
                        Continue For
                    End If
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    cell.Value = ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                    'Setting Value in cell
                    Try
                        With cell
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .Value = CType(ro.Cells(col.Name).Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                                '    If col.ValueType.Name = "String" Then
                                '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                '        '.Width = 200
                                '    End If
                                '    If col.ValueType.Name <> "String" Then
                                '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                '    End If
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    'Setting borders of cell
                    Dim border = cell.Style.Border
                    border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                    colIndex += 1
                Next

            Next
            'For Each dr As DataRow In dt.Rows
            '    ' Adding Data into rows
            '    colIndex = 1
            '    rowIndex += 1
            '    For Each dc As DataColumn In dt.Columns
            '        Dim cell = ws.Cells(rowIndex, colIndex)
            '        'Setting Value in cell
            '        cell.Value = Convert.ToInt32(dr(dc.ColumnName)) 'dr(dc.ColumnName) 

            '        'Setting borders of cell
            '        Dim border = cell.Style.Border
            '        border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
            '        colIndex += 1
            '    Next
            'Next

            'colIndex = 0
            'For Each dc As DataColumn In dt.Columns
            '    'Creating Headings
            '    colIndex += 1
            '    Dim cell = ws.Cells(rowIndex, colIndex)

            '    'Setting Sum Formula
            '    cell.Formula = ("Sum(" + ws.Cells(3, colIndex).Address & ":") + ws.Cells(rowIndex - 1, colIndex).Address & ")"

            '    'Setting Background fill color to Gray
            '    cell.Style.Fill.PatternType = ExcelFillStyle.Solid
            '    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
            'Next
            For i As Integer = 4 To ColNLst.Count - 1
                Dim cellsum = ws.Cells(rowIndex + 1, i)
                With cellsum
                    'Setting Sum Formula
                    .Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex, i).Address & ")"
                    .Style.Numberformat.Format = "#,##0.000"

                    'Setting Background fill color to Gray
                    .Style.Fill.PatternType = ExcelFillStyle.Solid
                    .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End With
            Next
            ws.Cells(ws.Dimension.Address).AutoFitColumns()


            'Generate A File with Random name
            Dim bin As [Byte]() = p.GetAsByteArray()
            Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
            Try
                File.WriteAllBytes(file__1, bin)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
    Private Sub ExportDataToExcel(fileName As String, title As String)

        Using p As New ExcelPackage()
            Try

                'Here setting some document properties
                p.Workbook.Properties.Author = "GmLogic"
                p.Workbook.Properties.Title = fileName '"ExprotData"

                'Create a sheet
                p.Workbook.Worksheets.Add(title)

                For Each ws As ExcelWorksheet In p.Workbook.Worksheets
                    Dim lst = Me.MasterDataGridView.Rows ' Me.MasterBindingSource.DataSource
                    'Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Εκκρεμείς παραγγελίες")
                    'ws.Name = "Sample Worksheet"
                    'Setting Sheet's name

                    ws.Cells.Style.Font.Size = 11
                    'Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"
                    'Default Font name for whole sheet
                    'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
                    'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
                    'dt = Utility.LINQToDataTable(db, q)
                    'My Function which generates DataTable
                    'Merging cells and create a center heading for out table
                    'ws.Cells(1, 1).Value = "Sample DataTable Export"
                    Dim colIndex As Integer = 1
                    Dim rowIndex As Integer = 1

                    'ws.Cells(rowIndex, colIndex, rowIndex, 15).Merge = True
                    'ws.Cells(rowIndex, colIndex).Value = "ΛΝΚ Α.Ε"
                    ws.Row(rowIndex).Style.Font.Size = 14
                    'Creating Headings
                    'Dim cellprH = ws.Cells(rowIndex - 1, colIndex)
                    Dim cellH = ws.Cells(rowIndex, colIndex)
                    'Setting the background color of header cells to Gray
                    'Dim fillprH = cellprH.Style.Fill
                    Dim fillH = cellH.Style.Fill
                    'fillprH.PatternType = ExcelFillStyle.Solid
                    fillH.PatternType = ExcelFillStyle.Solid
                    'fillprH.BackgroundColor.SetColor(System.Drawing.Color.White)

                    fillH.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

                    'Setting Top/left,right/bottom borders.
                    Dim borderH = cellH.Style.Border
                    borderH.Bottom.Style = InlineAssignHelper(borderH.Top.Style, InlineAssignHelper(borderH.Left.Style, InlineAssignHelper(borderH.Right.Style, ExcelBorderStyle.Thin)))

                    'Setting Value in cell
                    cellH.Value = Me.Text & " Από " & Me.DateTimePicker1.Value.ToShortDateString & " έως " & Me.DateTimePicker2.Value.ToShortDateString '"Export Data " & Me.DateTimePicker2.Value.ToShortDateString

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                    ws.Row(rowIndex).Height = 35.25
                    ws.Cells(rowIndex, colIndex, rowIndex, myArrN.Length).Merge = True
                    cellH.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                    rowIndex += 1
                    colIndex = 1
                    For Each col In myArrN
                        'Creating Headings
                        Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                        Dim cell = ws.Cells(rowIndex, colIndex)
                        cell.Style.WrapText = True
                        'Setting the background color of header cells to Gray
                        Dim fillpr = cellpr.Style.Fill
                        Dim fill = cell.Style.Fill
                        fillpr.PatternType = ExcelFillStyle.Solid
                        fill.PatternType = ExcelFillStyle.Solid
                        fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)

                        fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206)) 'Color.Orange)

                        'Setting Top/left,right/bottom borders.
                        Dim border = cell.Style.Border
                        border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                        'Setting Value in cell
                        cell.Value = col
                        'cell.AutoFitColumns()
                        colIndex += 1
                    Next
                    'ws.Column(ColNLst.Count).Width = 44.57

                    ws.Row(rowIndex).Style.Font.Bold = True
                    ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center

                    Dim rowDataIndex = rowIndex + 1

                    'If Me.radioBtnSales.Checked Then
                    '    lst = CType(lst, DataTable).AsEnumerable
                    'End If

                    For Each ro As DataGridViewRow In lst ' DataGridViewRow In Me.MasterDataGridView.Rows
                        ' Adding Data into rows
                        colIndex = 1
                        rowIndex += 1

                        For Each col_Name In myArrF 'As  DataGridViewColumn In Me.MasterDataGridView.Columns
                            If col_Name = "Check" Then
                                Continue For
                            End If
                            Dim cell = ws.Cells(rowIndex, colIndex)
                            Try
                                'If Not Me.radioBtnSales.Checked Then
                                Dim col = Me.MasterDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = col_Name).FirstOrDefault
                                If col Is Nothing Then
                                    Continue For
                                End If
                                Dim idx = col.Index
                                cell.Value = ro.Cells(idx).Value 'ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                                'Else
                                'cell.Value = ro(col_Name)
                                'End If

                                'Setting Value in cell
                                With cell
                                    'If Not Me.radioBtnSales.Checked Then
                                    Dim t As Type = Me.MasterDataGridView.Columns(idx).ValueType 'ro.GetType().GetProperty(col_Name).PropertyType 
                                    'Else
                                    't = ro(col_Name).GetType
                                    'End If

                                    If Not IsNothing(t) Then
                                        If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                            If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                                .Value = CType(cell.Value, Double)
                                                .Style.Numberformat.Format = "#,##0.000"
                                            End If
                                            If Not t.FullName.IndexOf("System.DateTime") Then
                                                If {"INSDATE", "TRNDATE", "BFDATE", "LDATE"}.Contains(col_Name) Then
                                                    .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                                Else
                                                    .Style.Numberformat.Format = "dd/MM/yyyy"
                                                End If
                                            End If
                                        End If

                                        If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                            If cell.Value IsNot DBNull.Value Then
                                                .Value = CType(cell.Value, Double)
                                                .Style.Numberformat.Format = "#,##0.000"
                                            End If
                                        End If
                                        If t.Name = "DateTime" Then
                                            '.Value = CType(ro.Cells(col.Name).Value, Double)
                                            If col_Name = "INSDATE" Then
                                                .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                            Else
                                                .Style.Numberformat.Format = "dd/MM/yyyy"
                                            End If

                                        End If
                                    End If
                                    'If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    '    If Not t.FullName.IndexOf("System.Decimal") Then
                                    '        .Value = CType(cell.Value, Double)
                                    '        .Style.Numberformat.Format = "#,##0.000"
                                    '    End If
                                    'End If
                                    'If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    '    If cell.Value IsNot DBNull.Value Then
                                    '        .Value = CType(cell.Value, Double)
                                    '        .Style.Numberformat.Format = "#,##0.000"
                                    '    End If
                                    'End If
                                End With
                            Catch ex As Exception
                                MsgBox("Error col_Name:" & col_Name & vbCrLf & ex.Message)
                            End Try


                            'Setting borders of cell
                            Dim border = cell.Style.Border
                            border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                            'ws.Cells(rowIndex, colIndex).AutoFitColumns()
                            colIndex += 1
                        Next
                    Next

                    rowIndex += 1
                    Dim cellFoot = ws.Cells(rowIndex, 1)
                    cellFoot.Value = "Σύνολα"
                    cellFoot.Style.Fill.PatternType = ExcelFillStyle.Solid
                    cellFoot.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(208, 206, 206))
                    'Dim startCol = 1
                    'Dim endCol = myArrF.Count
                    'Dim criteria = ws.Cells(rowIndex, 2)
                    'criteria.Value = "<0"
                    'For i As Integer = startCol + 1 To endCol ' ColNLst.Count
                    '    Dim cellsum = ws.Cells(rowIndex, i)
                    '    Dim cellsumpr = ws.Cells(rowIndex - 1, i)
                    'Setting Value in cell
                    'Dim col_Name =myArrF(i)
                    Dim i = 1
                    For Each col_Name In myArrF 'As  DataGridViewColumn In Me.MasterDataGridView.Columns
                        If col_Name = "Check" Then
                            i += 1
                            Continue For
                        End If

                        Dim cellsum = ws.Cells(rowIndex, i)
                        If cellsum.Value = "Σύνολα" Then
                            i += 1
                            Continue For
                        End If
                        Dim cellsumpr = ws.Cells(rowIndex - 1, i)




                        'Dim cell = ws.Cells(rowIndex, colIndex)
                        Try
                                'If Not Me.radioBtnSales.Checked Then
                                Dim col = Me.MasterDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = col_Name).FirstOrDefault
                                If col Is Nothing Then
                                    Continue For
                                End If
                                Dim idx = col.Index
                            'cell.Value = ro.Cells(idx).Value 'ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                            'Else
                            'cell.Value = ro(col_Name)
                            'End If

                            'Setting Value in cell
                            With cellsum

                                'If Not Me.radioBtnSales.Checked Then
                                Dim t As Type = Me.MasterDataGridView.Columns(idx).ValueType 'ro.GetType().GetProperty(col_Name).PropertyType 
                                'Else
                                't = ro(col_Name).GetType
                                'End If

                                If Not IsNothing(t) Then
                                    If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                        If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                            .Value = CType(cellsum.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.000"
                                        End If
                                        If Not t.FullName.IndexOf("System.DateTime") Then
                                            If {"INSDATE", "TRNDATE", "BFDATE", "LDATE"}.Contains(col_Name) Then
                                                .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                            Else
                                                .Style.Numberformat.Format = "dd/MM/yyyy"
                                            End If
                                        End If
                                    End If

                                    If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                        If cellsum.Value IsNot DBNull.Value Then
                                            .Value = CType(cellsum.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.000"
                                        End If
                                    End If
                                    If t.Name = "DateTime" Then
                                        '.Value = CType(ro.Cells(col.Name).Value, Double)
                                        If col_Name = "INSDATE" Then
                                            .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                        Else
                                            .Style.Numberformat.Format = "dd/MM/yyyy"
                                        End If

                                    End If
                                End If
                                'If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                '    If Not t.FullName.IndexOf("System.Decimal") Then
                                '        .Value = CType(cell.Value, Double)
                                '        .Style.Numberformat.Format = "#,##0.000"
                                '    End If
                                'End If
                                'If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                '    If cell.Value IsNot DBNull.Value Then
                                '        .Value = CType(cell.Value, Double)
                                '        .Style.Numberformat.Format = "#,##0.000"
                                '    End If
                                'End If
                            End With
                        Catch ex As Exception
                                MsgBox("Error col_Name:" & col_Name & vbCrLf & ex.Message)
                            End Try


                        'Setting borders of cell
                        Dim border = cellsum.Style.Border
                        border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                        'ws.Cells(rowIndex, colIndex).AutoFitColumns()
                        'colIndex += 1
                        'Next








                        If Not cellsum.Style.Numberformat.Format = "#,##0.000" Then
                            i += 1
                            Continue For
                        End If

                        With cellsum
                            'Setting Sum Formula 
                            '.Formula = ("Sum(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                            '=SUMIF(F2:F9;">0";F2:F9)

                            'Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & ws.Cells(rowIndex, 2).Address & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'Spoiler: The solution is to use "," instead of ";" when working with formulas in your code.
                            Dim formula As String = "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<>0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'If Me.radioBtnΑggregate.Checked Then
                            'formula =              "SUMIF(" + ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & "," & """<0""" & "," & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            'End If
                            .Formula = formula '("SUMIF(" + ws.Cells(3, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ";" & Chr(34) & "<0" & Chr(34) & ";" & ws.Cells(3, i).Address & ":" + ws.Cells(rowIndex - 1, i).Address & ")"
                            .Style.Numberformat.Format = "#,##0.00"

                            'Setting Background fill color to Gray
                            .Style.Fill.PatternType = ExcelFillStyle.Solid
                            .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                        End With
                        i += 1
                    Next
                    ws.Cells(ws.Dimension.Address).AutoFitColumns()

                Next

                'Generate A File with Random name
                Dim bin As [Byte]() = p.GetAsByteArray()
                Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
                Try
                    File.WriteAllBytes(file__1, bin)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub
    Private Sub ExportDataToExcel(fileName As String)

        Using p As New ExcelPackage()
            'Here setting some document properties
            p.Workbook.Properties.Author = "GmLogic"
            p.Workbook.Properties.Title = "ExprotData"

            'Create a sheet
            'p.Workbook.Worksheets.Add("Sheet1") 'ΠΑΡΑΔΟΣΕΙΣ
            'Εκκρεμείς παραγγελίες
            'Picking
            'Επιστροφές-Ακυρώσεις Παρημίν
            'Παραγγελίες Μήνα
            'Παραδόσεις Μήνα
            If Me.ToolStripComboBoxIndexes.SelectedItem.ToString = "Όλα" Then
                For Each ss In Me.ToolStripComboBoxIndexes.Items
                    If ss = "Όλα" Then
                        Continue For
                    End If
                    p.Workbook.Worksheets.Add(ss)
                Next
            Else
                p.Workbook.Worksheets.Add(Me.ToolStripComboBoxIndexes.SelectedItem.ToString)
            End If

            For Each ws As ExcelWorksheet In p.Workbook.Worksheets
                If Me.ToolStripComboBoxIndexes.SelectedItem.ToString = "Όλα" Then
                    SelectedIdx(ws.Name) 'Εδώ επιλέγει ποιό sheet γεμίσει.
                    Cmd_Select()
                End If

                Dim lst = Me.MasterDataGridView.Rows ' List(Of DataGridViewRow) SortableBindingList(Of Hglp.FetchWhousesDailyResult) = Me.MasterBindingSource.DataSource
                'Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Εκκρεμείς παραγγελίες")
                'ws.Name = "Sample Worksheet"
                'Setting Sheet's name
                ws.Cells.Style.Font.Size = 11
                'Default font size for whole sheet
                ws.Cells.Style.Font.Name = "Calibri"
                'Default Font name for whole sheet
                'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
                'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
                'dt = Utility.LINQToDataTable(db, q)
                'My Function which generates DataTable
                'Merging cells and create a center heading for out table
                'ws.Cells(1, 1).Value = "Sample DataTable Export"
                Dim colIndex As Integer = 1
                Dim rowIndex As Integer = 1

                ws.Cells(rowIndex, 1, 1, 7).Merge = True
                ws.Cells(rowIndex, 1).Value = ws.Name & " Από " & Me.DateTimePicker1.Value.ToShortDateString & " έως " & Me.DateTimePicker2.Value.ToShortDateString '"Export Data " & Me.DateTimePicker2.Value.ToShortDateString
                ws.Row(rowIndex).Style.Font.Size = 14
                ws.Row(rowIndex).Style.Font.Bold = True
                ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
                ws.Row(rowIndex).Height = 35.25
                'ws.Column(2).Width = 44.57

                'Dim ColNLst As New List(Of String)
                'For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                '    If col.Name = "Check" Then
                '        Continue For
                '    End If
                '    ColNLst.Add(col.Name)
                'Next
                'myArrF = ("TRNDATE,INSDATE,FPRMS,FP_NAME,FINCODE,SL_DNAME,P_CODE,P_NAME,TB_CODE,TB_NAME,SHORTCUT,DI_NAME,M_CODE,M_NAME,MU_SHORTCUT,QTY1,QTY1COV,QTY1CANC,DIAFORA").Split(",")
                'myArrN = ("Ημερ/νία,Ημερ.εισαγωγής,Τύπος,Τύπος Περιγραφή,Παραστατικό,Πωλητής,Πελάτης,Επωνυμία πελάτη,Παραλήπτης,Επωνυμία Παραλήπτη,Εγκατάσταση,Νομός,Κωδικός,Περιγραφή,Μ.Μ.(Τ),Ποσ.1,Εκτελ.,Ακυρ.,Διαφορά").Split(",")

                rowIndex += 1
                For Each col In myArrN
                    'Creating Headings
                    Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    'Setting the background color of header cells to Gray
                    Dim fillpr = cellpr.Style.Fill
                    Dim fill = cell.Style.Fill
                    fillpr.PatternType = ExcelFillStyle.Solid
                    fill.PatternType = ExcelFillStyle.Solid
                    fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)
                    fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                    'Setting Top/left,right/bottom borders.
                    Dim border = cell.Style.Border
                    border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                    'Setting Value in cell
                    cell.Value = col

                    colIndex += 1
                Next
                ws.Cells(rowIndex).AutoFitColumns()
                'ws.Column(ColNLst.Count).Width = 44.57

                ws.Row(rowIndex).Style.Font.Bold = True
                ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center

                Dim rowDataIndex = rowIndex + 1


                For Each ro As DataGridViewRow In lst ' DataGridViewRow In Me.MasterDataGridView.Rows
                    ' Adding Data into rows
                    colIndex = 1
                    rowIndex += 1

                    For Each col_Name In myArrF 'As  DataGridViewColumn In Me.MasterDataGridView.Columns
                        If col_Name = "Check" Then
                            Continue For
                        End If
                        Dim cell = ws.Cells(rowIndex, colIndex)
                        Dim idx = Me.MasterDataGridView.Columns.Cast(Of DataGridViewColumn).Where(Function(f) f.DataPropertyName = col_Name).FirstOrDefault.Index
                        cell.Value = ro.Cells(idx).Value 'ro.GetType().GetProperty(col_Name).GetValue(ro) 'ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                        'Setting Value in cell
                        Try
                            With cell
                                Dim t As Type = Me.MasterDataGridView.Columns(cell.Columns).ValueType 'ro.GetType().GetProperty(col_Name).PropertyType 
                                If Not IsNothing(t) Then
                                    If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                        If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                            .Value = CType(cell.Value, Double)
                                            .Style.Numberformat.Format = "#,##0.000"
                                        End If
                                        If Not t.FullName.IndexOf("System.DateTime") Then
                                            If col_Name = "INSDATE" Then
                                                .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                            Else
                                                .Style.Numberformat.Format = "dd/MM/yyyy"
                                            End If
                                        End If
                                    End If

                                    If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                        .Value = CType(cell.Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                    If t.Name = "DateTime" Then
                                        '.Value = CType(ro.Cells(col.Name).Value, Double)
                                        If col_Name = "INSDATE" Then
                                            .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                        Else
                                            .Style.Numberformat.Format = "dd/MM/yyyy"
                                        End If

                                    End If



                                    '    If col.ValueType.Name = "String" Then
                                    '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                    '        '.Width = 200
                                    '    End If
                                    '    If col.ValueType.Name <> "String" Then
                                    '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                    '    End If
                                End If
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Decimal") Then
                                        .Value = CType(cell.Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                End If
                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(cell.Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                            End With
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try


                        'Setting borders of cell
                        Dim border = cell.Style.Border
                        border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                        colIndex += 1
                    Next
                    ws.Cells(rowIndex).AutoFitColumns()
                Next

                rowIndex += 1
                For i As Integer = 15 + 1 To myArrF.Count ' ColNLst.Count
                    Dim cellsum = ws.Cells(rowIndex, i)
                    With cellsum
                        'Setting Sum Formula
                        .Formula = ("Sum(" + ws.Cells(rowDataIndex, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                        .Style.Numberformat.Format = "#,##0.000"

                        'Setting Background fill color to Gray
                        .Style.Fill.PatternType = ExcelFillStyle.Solid
                        .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                    End With
                Next
                ws.Cells(rowIndex).AutoFitColumns()

            Next
            'Generate A File with Random name
            Dim bin As [Byte]() = p.GetAsByteArray()
            Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
            Try
                File.WriteAllBytes(file__1, bin)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub
    Private Sub ExportDataToExcelnew(fileName As String)
        If Me.ToolStripComboBoxIndexes.SelectedItem.ToString = "Όλα" Then
            '            Όλα
            '            Παραγγελίες
            '            Εκκρεμείς Παραγγελίες
            'PICKING
            '            Κατάσταση παραδόσεων
            'Επιστροφές
            '            •	Εκκρεμείς παραγγελίες
            '•	Picking
            '•	Επιστροφές-Ακυρώσεις Παρημίν
            '•	Παραγγελίες Μήνα
            '•	Παραδόσεις Μήνα

        End If
        Using p As New ExcelPackage()
            'Here setting some document properties
            p.Workbook.Properties.Author = "GmLogic"
            p.Workbook.Properties.Title = "ExprotData"

            'Create a sheet
            p.Workbook.Worksheets.Add("Sheet1") 'ΠΑΡΑΔΟΣΕΙΣ
            Dim ws As ExcelWorksheet = p.Workbook.Worksheets("Sheet1")
            'ws.Name = "Sample Worksheet"
            'Setting Sheet's name
            ws.Cells.Style.Font.Size = 11
            'Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"
            'Default Font name for whole sheet
            'Dim dt As DataTable = CreateDataTable() ' Me.MasterBindingSource.DataSource 
            'Dim q As IQueryable(Of whRpt) = CType(Me.MasterBindingSource.DataSource, List(Of whRpt)).AsQueryable
            'dt = Utility.LINQToDataTable(db, q)
            'My Function which generates DataTable
            'Merging cells and create a center heading for out table
            'ws.Cells(1, 1).Value = "Sample DataTable Export"
            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            ws.Cells(rowIndex, 2, 1, 4).Merge = True
            ws.Cells(rowIndex, 2).Value = "Export Data " & Me.DateTimePicker2.Value.ToShortDateString
            ws.Row(rowIndex).Style.Font.Size = 14
            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center
            ws.Row(rowIndex).Height = 35.25
            'ws.Column(2).Width = 44.57

            Dim ColNLst As New List(Of String)
            For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                If col.Name = "Check" Then
                    Continue For
                End If
                ColNLst.Add(col.Name)
            Next

            rowIndex += 1
            For Each col In ColNLst
                'Creating Headings
                Dim cellpr = ws.Cells(rowIndex - 1, colIndex)
                Dim cell = ws.Cells(rowIndex, colIndex)
                'Setting the background color of header cells to Gray
                Dim fillpr = cellpr.Style.Fill
                Dim fill = cell.Style.Fill
                fillpr.PatternType = ExcelFillStyle.Solid
                fill.PatternType = ExcelFillStyle.Solid
                fillpr.BackgroundColor.SetColor(System.Drawing.Color.White)
                fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                'Setting Top/left,right/bottom borders.
                Dim border = cell.Style.Border
                border.Bottom.Style = InlineAssignHelper(border.Top.Style, InlineAssignHelper(border.Left.Style, InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)))

                'Setting Value in cell
                cell.Value = col

                colIndex += 1
            Next
            ws.Cells(rowIndex).AutoFitColumns()
            'ws.Column(ColNLst.Count).Width = 44.57

            ws.Row(rowIndex).Style.Font.Bold = True
            ws.Row(rowIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Row(rowIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Center

            Dim rowDataIndex = rowIndex + 1
            For Each ro As DataGridViewRow In Me.MasterDataGridView.Rows
                ' Adding Data into rows
                colIndex = 1
                rowIndex += 1

                For Each col As DataGridViewColumn In Me.MasterDataGridView.Columns
                    If col.Name = "Check" Then
                        Continue For
                    End If
                    Dim cell = ws.Cells(rowIndex, colIndex)
                    cell.Value = ro.Cells(col.Name).Value 'dr(dc.ColumnName) 
                    'Setting Value in cell
                    Try
                        With cell
                            Dim t As Type = col.ValueType
                            If Not IsNothing(t) Then
                                If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                    If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                        .Value = CType(ro.Cells(col.Name).Value, Double)
                                        .Style.Numberformat.Format = "#,##0.000"
                                    End If
                                    If Not t.FullName.IndexOf("System.DateTime") Then
                                        If col.Name = "Ημερ.εισαγωγής" Then
                                            .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                        Else
                                            .Style.Numberformat.Format = "dd/MM/yyyy"
                                        End If
                                    End If
                                End If

                                If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                                If t.Name = "DateTime" Then
                                    '.Value = CType(ro.Cells(col.Name).Value, Double)
                                    If col.Name = "Ημερ.εισαγωγής" Then
                                        .Style.Numberformat.Format = "dd/MM/yyyy HH:mm"
                                    Else
                                        .Style.Numberformat.Format = "dd/MM/yyyy"
                                    End If

                                End If



                                '    If col.ValueType.Name = "String" Then
                                '        '.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                                '        '.Width = 200
                                '    End If
                                '    If col.ValueType.Name <> "String" Then
                                '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                '    End If
                            End If
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Decimal") Then
                                    .Value = CType(ro.Cells(col.Name).Value, Double)
                                    .Style.Numberformat.Format = "#,##0.000"
                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" Then
                                .Value = CType(ro.Cells(col.Name).Value, Double)
                                .Style.Numberformat.Format = "#,##0.000"
                            End If
                        End With
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    'Setting borders of cell
                    Dim border = cell.Style.Border
                    border.Left.Style = InlineAssignHelper(border.Right.Style, ExcelBorderStyle.Thin)
                    colIndex += 1
                Next
                ws.Cells(rowIndex).AutoFitColumns()
            Next

            rowIndex += 1
            For i As Integer = 16 To ColNLst.Count
                Dim cellsum = ws.Cells(rowIndex, i)
                With cellsum
                    'Setting Sum Formula
                    .Formula = ("Sum(" + ws.Cells(rowDataIndex, i).Address & ":") + ws.Cells(rowIndex - 1, i).Address & ")"
                    .Style.Numberformat.Format = "#,##0.000"

                    'Setting Background fill color to Gray
                    .Style.Fill.PatternType = ExcelFillStyle.Solid
                    .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                End With
            Next
            ws.Cells(rowIndex).AutoFitColumns()


            'Generate A File with Random name
            Dim bin As [Byte]() = p.GetAsByteArray()
            Dim file__1 As String = fileName ' "e:\temp\" & Guid.NewGuid().ToString() & ".xlsx"
            Try
                File.WriteAllBytes(file__1, bin)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub


#End Region
End Class

Friend Class _reorderN
    Public Property code As String
    Public Property company As Short
    Public Property mtracn As Short?
    Public Property mtrl As Integer
    Public Property name As String
    Public Property num05 As Double?
    Public Property un_Name As String
    Public Property reorder As Double?
End Class
''' <summary>
''' 4 , 5, 8, 13
''' 2 ΥΠΜΑ Ν.ΚΑΡΒΑΛΗΣ Κεντρ.Αποθ.Χώρος 
''' 4 Θεσσαλονίκη   
''' 13 Βαθύλακκος	
''' 8 Ασπρόπυργος	
''' 5 Πύργος
''' </summary>
Class whRptN
    Public Property code As String
    Public Property mtrl As Integer
    Public Property mtrunit As Short
    Public Property mx_num05 As Double?
    Public Property mu1name As String
    Public Property name As String
    Public Property whouse As List(Of Integer)

    ''' <summary>
    ''' Απόθεμα συστήματος 4
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_4 As Double

    ''' <summary>
    ''' Απόθεμα συστήματος 5
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_5 As Double

    ''' <summary>
    ''' Απόθεμα συστήματος 8
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_8 As Double

    ''' <summary>
    ''' Απόθεμα συστήματος 13
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_13 As Double

    ''' <summary>
    ''' Απόθεμα συστήματος 539
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_539 As Double

    ''' <summary>
    ''' Απόθεμα συστήματος tot
    ''' </summary>
    ''' <returns></returns>
    Public Property qty_tot As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες 4
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_4 As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες 5
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_5 As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες 8
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_8 As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες 13
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_13 As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες 539
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_539 As Double

    ''' <summary>
    ''' Εκκρεμείς Παραγγελίες tot
    ''' </summary>
    ''' <returns></returns>
    Public Property ekrdif_tot As Double

    ''' <summary>
    ''' Διαθέσιμο Απόθεμα 4
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_4 As Double

    ''' <summary>
    ''' Διαθέσιμο Απόθεμα 5
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_5 As Double

    ''' <summary>
    ''' Διαθέσιμο Απόθεμα 8
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_8 As Double

    ''' <summary>
    ''' Διαθέσιμο Απόθεμα 13
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_13 As Double

    ''' <summary>
    ''' Διαθέσιμο Απόθεμα 539
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_539 As Double


    ''' <summary>
    ''' Διαθέσιμο Απόθεμα tot
    ''' </summary>
    ''' <returns></returns>
    Public Property avlqty_tot As Double

    ''' <summary>
    ''' Δελτία φορτώσεων και Χειρόγραφα 4
    ''' </summary>
    ''' <returns></returns>
    Public Property dfdif_4 As Double

    ''' <summary>
    ''' Δελτία φορτώσεων και Χειρόγραφα 5
    ''' </summary>
    ''' <returns></returns>
    Public Property dfdif_5 As Double

    ''' <summary>
    ''' Δελτία φορτώσεων και Χειρόγραφα 8
    ''' </summary>
    ''' <returns></returns>
    Public Property dfdif_8 As Double

    ''' <summary>
    ''' Δελτία φορτώσεων και Χειρόγραφα 13
    ''' </summary>
    ''' <returns></returns>
    Public Property dfdif_13 As Double

    ''' <summary>
    ''' Δελτία φορτώσεων και Χειρόγραφα 539
    ''' </summary>
    ''' <returns></returns>
    Public Property dfdif_539 As Double

    ''' <summary>
    ''' Καθοδόν προς την εγκατάσταση 4
    ''' A.FINSTATES IN (1000) Καθοδόν
	''' A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία
    ''' </summary>
    ''' <returns></returns>
    Public Property onwaydif_4 As Double

    ''' <summary>
    ''' Καθοδόν προς την εγκατάσταση 5
    ''' A.FINSTATES IN (1000) Καθοδόν
    ''' A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία
    ''' </summary>
    ''' <returns></returns>
    Public Property onwaydif_5 As Double

    ''' <summary>
    ''' Καθοδόν προς την εγκατάσταση 8
    ''' A.FINSTATES IN (1000) Καθοδόν
    ''' A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία
    ''' </summary>
    ''' <returns></returns>
    Public Property onwaydif_8 As Double

    ''' <summary>
    ''' Καθοδόν προς την εγκατάσταση 13
    ''' A.FINSTATES IN (1000) Καθοδόν
    ''' A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία
    ''' </summary>
    ''' <returns></returns>
    Public Property onwaydif_13 As Double

    ''' <summary>
    ''' Καθοδόν προς την εγκατάσταση 539
    ''' A.FINSTATES IN (1000) Καθοδόν
    ''' A.SHIPKIND IN (1001, 1007, 1005) 1001 Προς Αποθήκευση, 1007 Ενδοδιακίνηση, 1005 Αποστολή για επεξεργασία
    ''' </summary>
    ''' <returns></returns>
    Public Property onwaydif_539 As Double

    ''' <summary>
    ''' Σχισμένα σακιά στην εγκατάσταση 4
    ''' A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
    ''' </summary>
    ''' <returns></returns>
    Public Property skisdif_4 As Double

    ''' <summary>
    ''' Σχισμένα σακιά στην εγκατάσταση 5
    ''' A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
    ''' </summary>
    ''' <returns></returns>
    Public Property skisdif_5 As Double

    ''' <summary>
    ''' Σχισμένα σακιά στην εγκατάσταση 8
    ''' A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
    ''' </summary>
    ''' <returns></returns>
    Public Property skisdif_8 As Double

    ''' <summary>
    ''' Σχισμένα σακιά στην εγκατάσταση 13
    ''' A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
    ''' </summary>
    ''' <returns></returns>
    Public Property skisdif_13 As Double

    ''' <summary>
    ''' Σχισμένα σακιά στην εγκατάσταση 539
    ''' A.FPRMS IN (1002) ΔΠΕΠΙ Δελτίο προς επιστροφή
    ''' </summary>
    ''' <returns></returns>
    Public Property skisdif_539 As Double

End Class
