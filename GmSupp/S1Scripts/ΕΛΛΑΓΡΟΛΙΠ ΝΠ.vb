Dim status As var = 0
'var TAB = "\t";
'var CR = "\r";
'var LF = "\n";
'var CRLF = "\r\n";
'var FF = "\f";
'var DQUOTE = '\"';
'var SQUOTE = "\'";
'var BACKSLASH = "\\";
'var BACKSPACE = "\b";


Private Function UnVisibleObjs() As function
        '108 = ������� ������������
        '109 = ������� ������
        '110 = ������� �����������
        '104 = ������� �������
        '102 = WARE HOUSE MANAGMENT 
        If ((X.SYS.GROUPS = 108) _
					OrElse ((X.SYS.GROUPS = 109) _
					OrElse ((X.SYS.GROUPS = 110) _
					OrElse ((X.SYS.GROUPS = 104) _
					OrElse (X.SYS.GROUPS = 102))))) Then
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		'gm
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(77), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(80), ChrW(80), ChrW(86), ChrW(70))
		'�Ŀ�ǵ�� ������� - ������ - �����, ����� & �����õ��
		X.SETPROPERTY(ChrW(80), ChrW(80), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
		'RUNB_150001=�������ü�� �����Ĺ���
		X.SETPROPERTY(ChrW(80), ChrW(65533), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
	End If

	'X.WARNING(SALDOC.SERIES)
	If ((SALDOC.SERIES <> 1061) _
					AndAlso ((SALDOC.SERIES <> 2061) _
					AndAlso ((SALDOC.SERIES <> 3061) _
					AndAlso ((SALDOC.SERIES <> 1081) _
					AndAlso ((SALDOC.SERIES <> 2081) _
					AndAlso (SALDOC.SERIES <> 3081)))))) Then
		'N_CancelQTY1COVnum04
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
		'RUNB_150002=�����÷ ��ĵ��ü��ɽ
	End If

	If ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046))) Then
		If (MTRDOC.ccCLockShipValue = 1) Then
			MTRDOC.SETREADONLY(ChrW(99), ChrW(84))
		Else
			MTRDOC.SETREADONLY(ChrW(99), ChrW(70))
		End If

	End If

	'201 = Administrator
	If ((X.SYS.GROUPS = 201) _
					AndAlso ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046)))) Then
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(84))
		'RUNB_150003=���������� �������÷� ��ıƿ���ν
	Else
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
	End If

	'201 = Administrator, 100 = ����������
	If ((X.SYS.GROUPS = 201) _
					OrElse ((X.SYS.GROUPS = 100) _
					OrElse ((SALDOC.FPRMS = 1000) _
					OrElse ((SALDOC.FPRMS = 1001) _
					OrElse ((SALDOC.FPRMS = 1003) _
					OrElse (SALDOC.FPRMS = 7046)))))) Then
		'Pick, ���į� �����÷�, ��Ŀ�� �����÷�, ���į� �����������÷�
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(84))
		'RUNB_150011=������ ����/��
	Else
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
	End If

End Function

Private Function ON_INSERT() As function
        'X.WARNING('ON_INSERT')
        status = 2
	UnVisibleObjs()
End Function

Private Function ON_SALDOC_SERIES() As function
        'X.WARNING(SALDOC.series)
        If (SALDOC.SERIES = 1003) Then
		'23713500081    ���������� �.�.�.�.
		SALDOC.TRDR = 2371
		'1708    2371 9000001    ���������� �.�.�.� (��/�� ���/���)
		'SALDOC.TRDBRANCH = 1708;
		'1000 �����̽
		SALDOC.FINSTATES = 1000
		'1000 ����
		MTRDOC.BRANCHSEC = 1000
		MTRDOC.WHOUSESEC = Nothing
	End If

	'201 = Administrator, 100 = ����������
	If ((X.SYS.GROUPS = 201) _
					OrElse ((X.SYS.GROUPS = 100) _
					OrElse ((SALDOC.FPRMS = 1000) _
					OrElse ((SALDOC.FPRMS = 1001) _
					OrElse ((SALDOC.FPRMS = 1003) _
					OrElse (SALDOC.FPRMS = 7046)))))) Then
		'Pick, ���į� �����÷�, ��Ŀ�� �����÷�, ���į� �����������÷�
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(84))
		'RUNB_150011=������ ����/��
	Else
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
	End If

End Function

Private Function ON_LOCATE() As function
        status = 1
	UnVisibleObjs()
	'108 = ������� ������������
	'109 = ������� ������
	'110 = ������� �����������
	'104 = ������� �������
	'102 = WARE HOUSE MANAGMENT 
	If ((X.SYS.GROUPS = 108) _
					OrElse ((X.SYS.GROUPS = 109) _
					OrElse ((X.SYS.GROUPS = 110) _
					OrElse ((X.SYS.GROUPS = 104) _
					OrElse (X.SYS.GROUPS = 102))))) Then
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		'gm
		X.SETPROPERTY(ChrW(70), ChrW(73), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(70), ChrW(77), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(80), ChrW(80), ChrW(86), ChrW(70))
		'�Ŀ�ǵ�� ������� - ������ - �����, ����� & �����õ��
		X.SETPROPERTY(ChrW(80), ChrW(80), ChrW(86), ChrW(70))
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
		'RUNB_150001=�������ü�� �����Ĺ���
		X.SETPROPERTY(ChrW(80), ChrW(65533), ChrW(86), ChrW(70))
	End If

	'''/X.WARNING(SALDOC.SERIES)
	'if (SALDOC.SERIES != 1061 && SALDOC.SERIES != 2061 && SALDOC.SERIES != 3061) {
	'    //N_CancelQTY1COVnum04
	'    X.SETPROPERTY('PANEL', 'N_CancelQTY1COV', 'VISIBLE', 'FALSE'); //RUNB_150002=�����÷ ��ĵ��ü��ɽ
	'}
	If ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046))) Then
		If (MTRDOC.ccCLockShipValue = 1) Then
			MTRDOC.SETREADONLY(ChrW(99), ChrW(84))
		Else
			MTRDOC.SETREADONLY(ChrW(99), ChrW(70))
		End If

	End If

	'201 = Administrator
	If ((X.SYS.GROUPS = 201) _
					AndAlso ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046)))) Then
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(84))
		'RUNB_150003=���������� �������÷� ��ıƿ���ν
	Else
		X.SETPROPERTY(ChrW(80), ChrW(78), ChrW(86), ChrW(70))
	End If

End Function

Private Function ON_MTRDOC_ccCLockShipValue() As function
        If (MTRDOC.ccCLockShipValue = 1) Then
		MTRDOC.SETREADONLY(ChrW(99), ChrW(84))
	Else
		MTRDOC.SETREADONLY(ChrW(99), ChrW(70))
	End If

End Function

Private Function ON_MTRDOC_WHOUSESEC() As function
        If (SALDOC.FPRMS <> 7040) Then
		Return
	End If

	If (SALDOC.TRDR <> 2371) Then
		Return
	End If

	If (SALDOC.ISPRINT = 2371) Then
		Return
	End If

	'2    9000000    1707
	'4    9000001    1708
	'5    9000002    1709
	'8    9000003    1710
	'13    9000004    1712
	'X.WARNING('SALDOC.FPRMS=' + SALDOC.FPRMS + ' SALDOC.TRDR=' + SALDOC.TRDR + ' MTRDOC.WHOUSESEC = ' + MTRDOC.WHOUSESEC)
	Return
	Select Case (parseInt(MTRDOC.WHOUSESEC))
		Case 2
			'���� �.�������� �����.����.�����
			SALDOC.TRDBRANCH = 1707
				'�.� �������
		Case 4
			'���� ����� ���/����� �����.����.�����
			SALDOC.TRDBRANCH = 1708
				'�.� ������ν ���/���
		Case 5
			'������� �������� ������ ����.����.�����
			SALDOC.TRDBRANCH = 1709
				'�.� ������
		Case 8
			'���� ����� ��� - ����������� ����.����.�����
			SALDOC.TRDBRANCH = 1710
				'�.� �����������
		Case 13
			'��� ��������� �.�.� �� ���Ŀ��
			SALDOC.TRDBRANCH = 1712
			'�.� ���ͻ����
	End Select

End Function

'Gm
Private Function ON_POST() As function
        'X.WARNING('status = ' + status);
        'X.WARNING(FINDOC.SERIES);
        'if (FINDOC.FPRMS == 7040 || FINDOC.FPRMS == 7041 || FINDOC.FPRMS == 7046) {
        '    if (SALDOC.ISPRINT == 1) {
        '        X.EXCEPTION('���ÿǮ !!! ��� ������ �� ��ı������ �����ɼ��� �����ıĹ��');
        '    }
        '}
        '108 = ������� ������������
        '109 = ������� ������
        '110 = ������� �����������
        '104 = ������� �������
        '102 = WARE HOUSE MANAGMENT
        visGroup = 1
	If ((X.SYS.GROUPS = 108) _
					OrElse ((X.SYS.GROUPS = 109) _
					OrElse ((X.SYS.GROUPS = 110) _
					OrElse ((X.SYS.GROUPS = 104) _
					OrElse (X.SYS.GROUPS = 102))))) Then
		visGroup = 0
		'7040    ���į� �������÷�    ���ĵ���� �������÷
		'7041    ���į� ����Ŀ���    ���į� ����Ŀ���
		'7042    ���į� ����Ŀ��� (��.����.)    ���į� ����Ŀ���
		'7043    ���į� ����Ŀ��� ��� ����ķ    ���į� �������Ʈ�
		'7045    � ��ÿĹ��� ���������    ���į� ��ÿĹ��� ���������
		'7046    ���į� ����Ŀ���    ���ĵ���� �������÷
		If ((FINDOC.FPRMS = 7040) _
						OrElse ((FINDOC.FPRMS = 7041) _
						OrElse (FINDOC.FPRMS = 7046))) Then
			If Not SALDOC.FINSTATES Then
				X.EXCEPTION(ChrW(65533))
			End If

			If Not SALDOC.SHIPKIND Then
				X.EXCEPTION(ChrW(65533))
			End If

		End If

		'X.EXCEPTION(typeof SALDOC.FINSTATES + '-' + SALDOC.FINSTATES + '-' + SALDOC.SHIPKIND);
		'return;
	End If

	If ((FINDOC.FPRMS = 7040) _
					OrElse ((FINDOC.FPRMS = 7041) _
					OrElse (FINDOC.FPRMS = 7046))) Then
		If Not SALDOC.TRDBRANCH Then
			X.EXCEPTION(ChrW(65533))
		End If

		If Not MTRDOC.SOCARRIER Then
			X.EXCEPTION(ChrW(65533))
		End If

	End If

	'1001    ���į� �����÷�
	'7040    ���į� �������÷� + -
	'7041    ���į� ����Ŀ���
	'7046    ���į� ����Ŀ��� ���į� �����������÷� + -
	If ((SALDOC.FPRMS = 1001) _
					OrElse ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046)))) Then
		'ObjSal = X.CreateObj('SALDOC;��ù�� ������� �ɻ�õɽ'); 
		'X.WARNING(MTRDOC.ccCTOTSHIPVALUE);
		If (MTRDOC.SOCARRIER = 9999) Then
			'���������� ������
			Return
		End If

		ITELINES.FIRST

		While Not ITELINES.EOF
			ITELINES.ccCSOCARRIER = MTRDOC.SOCARRIER
			ITELINES.NEXT

		End While

		If (MTRDOC.ccCLockShipValue <> 1) Then
			'Not Lock
			MTRDOC.ccCTOTSHIPVALUE = 0
			ITELINES.FIRST

			While Not ITELINES.EOF
                    (Nothing  _
                                AndAlso (ITELINES.CCCSHIPVALUE <> 0))
                    'X.WARNING(ITELINES.CCCSHIPVALUE);
                    MTRDOC.ccCTOTSHIPVALUE = (MTRDOC.ccCTOTSHIPVALUE _
								+ (ITELINES.QTY1 * ITELINES.CCCSHIPVALUE))
				ITELINES.NEXT

			End While

		End If

		If (MTRDOC.SOCARRIER = 8888) Then
			'���������� ����������
			X.WARNING(ChrW(65533))
		End If

		If (visGroup = 1) Then
			If (MTRDOC.ccCTOTSHIPVALUE = 0) Then
				X.WARNING((ChrW(65533) + MTRDOC.ccCTOTSHIPVALUE))
			End If

		End If

	End If

End Function

Private Function ON_RESTOREEVENTS() As function
        status = 2
	UnVisibleObjs()
	'X.WARNING('ON_RESTOREEVENTS')
	'Gm
	'X.WARNING(FINDOC.SERIES);
	If (FINDOC.SERIES = 1001) Then
		'ObjSal = X.CreateObj('SALDOC;��ù�� ������� �ɻ�õɽ');                 
		Try
			ITELINES.FIRST

			While Not ITELINES.EOF
                    (Nothing  _
                                AndAlso (ITELINES.CCCQTY1PRO <> 0))
                    ITELINES.QTY1 = ITELINES.CCCQTY1PRO
                    '�-��ıƿ����,�-��������,�-�̼����� �,�-ADR
                    '��Ĭ ķ� ��ı����÷ Ŀ� ���į� �����÷� ��ıƭ��ı� � ��ıƿ���� ������� �Ŀ Header.
                    (Nothing  _
                                AndAlso (ITELINES.CCCSOCARRIER <> 0))
                    MTRDOC.SOCARRIER = ITELINES.CCCSOCARRIER
				MTRDOC.TRUCKSNO = ITELINES.CCCTRUCKSNO
				ITELINES.NEXT

			End While

		Catch  As e
                X.WARNING((ChrW(79) + (vbCr + e)))
		Finally
			'ObjSal.FREE; 
			'ObjSal =null; 
		End Try

	End If

	If ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046))) Then
		Z = ITELINES.FINDOCS
		'Query ��� ����÷ �ɽ ��ı�Ƿ��Ĺü��ɽ �����ıĹ�ν ��õ� Ŀ� ���ǿ�Ŀ� 
		SQL = (ChrW(83) + Z)
		ds = X.GETSQLDATASET(SQL, Nothing)
            Nothing
            SALDOC.FINSTATES = ds.FINSTATES
	End If

	'8039 = �������� ��� �������÷(���/�����)
	If (SALDOC.SERIES = 8039) Then
		Dim vfincode
		'X.WARNING(ITELINES.FINDOC + '--' + ITELINES.FINDOCS);
		ITELINES.FIRST

		While Not ITELINES.EOF
			'Query ��� ����÷ �ɽ ��ı�Ƿ��Ĺü��ɽ �����ıĹ�ν ��õ� Ŀ� ���ǿ�Ŀ� 
			SQL = (ChrW(83) _
							+ (ITELINES.FINDOCS + (ChrW(32) + ITELINES.MTRLINES)))
			ds = X.GETSQLDATASET(SQL, Nothing)
			vfincode = (vfincode _
							+ (ds.FINCODE + ChrW(45)))
			ITELINES.NEXT

		End While

		X.WARNING((ChrW(65533) + (vbCr + vfincode)))
	End If

End Function

Private Function ON_AFTERPOST() As function
    Dim ans = 0
	Dim vDate
	Dim vQty = 0
	Dim vLineVal = 0
	Dim vList0
	Dim vGroupList
	Dim vGrList
	Dim vPrice = 0
	Dim vPriceD = 0
	Dim vPriceVal = 0
	Dim vWhouse = 0
	Dim vPis = 0
	'Not used
	'var spis;
	Dim vQtyY = 0
	Dim vQtyI = 0
	Dim vQtyA = 0
	Dim vQtyNU = 0
	'������ ��� �ǿŽ �����Ǹ�� ��� Ŀ� cccsettings ��� ���̼�ķ ����÷ �����Ĺ���.
	rset = X.GETSQLDATASET((ChrW(115) + X.SYS.COMPANY), Nothing)
	rser = X.GETSQLDATASET((ChrW(115) + SALDOC.SERIES), Nothing)
	If (SALDOC.FINDOC < 0) Then
		'��� �����Ĺ��
		iSALDOCID = X.NEWID
	Else
		'������Ʈ �����Ĺ�ν ��� �ǿŽ �������.
		DeleteSalDoc()
		iSALDOCID = SALDOC.FINDOC
	End If

	'rset.priceseries = 1060,1061,1761,2061
	vList = X.EVAL((ChrW(73) _
						+ (rset.priceseries + ChrW(41))))
	X.WARNING(ChrW(65533))
	Return
	'Not used
	'spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);
	'if(spis.RECORDCOUNT>0)
	'{
	'��� ���ĭ� ������ ��� ��������õ�� ��ĵ ����÷ �����Ĺ���
	If (vList = 1) Then
		If (rset.district = 1) Then
			sd = X.GETSQLDATASET((ChrW(83) + SALDOC.TRDBRANCH), Nothing)
			X.WARNING(ChrW(65533))
			Return
		End If

		If (rset.trdbusiness = 1) Then
			sb = X.GETSQLDATASET((ChrW(83) + SALDOC.TRDR), Nothing)
			X.WARNING(ChrW(65533))
			Return
		End If

		If (rset.shipment = 1) Then
			X.WARNING(ChrW(65533))
			Return
		End If

		vDate = X.EVAL(ChrW(83))
		ObjSal = X.CreateObj(ChrW(83))
		Try
			ObjSal.DBInsert
			TblHeader = ObjSal.FindTable(ChrW(70))
			TblDetail = ObjSal.FindTable(ChrW(73))
			TblHeader.INSERT
			TblHeader.SERIES = rser.seriescre
			'rset.series;
			TblHeader.TRDR = SALDOC.TRDR
			TblHeader.TRDBRANCH = SALDOC.TRDBRANCH
			TblHeader.TRNDATE = SALDOC.TRNDATE
			TblHeader.FINDOCS = iSALDOCID
			TblHeader.COMMENTS = (SALDOC.CMPFINCODE + (ChrW(32) + SALDOC.TRDR_CUSTOMER_NAME))
			ITELINES.FIRST

			While Not ITELINES.EOF
				sg = X.GETSQLDATASET((ChrW(115) + ITELINES.MTRL), Nothing)
				'Gm
				'�������� whouse ��0� Ŀ ������ͼ��� �����ıĹ�� �� �� -> ���
				swh = X.GETSQLDATASET((ChrW(115) + ITELINES.FINDOCS), Nothing)
				vWhouse = swh.whouse
				'X.WARNING('swh.whouse-' + swh.whouse)
				'if ��� �����ıĹ��
				vWhouse = MTRDOC.WHOUSE
				'X.WARNING('SALDOC.INT02-' + SALDOC.INT02)
				If (SALDOC.INT02 <> 0) Then
					vWhouse = SALDOC.INT02
				End If

				'X.WARNING('vWhouse-' + vWhouse)
				sp4 = X.GETSQLDATASET((ChrW(115) _
									+ (vWhouse + (ChrW(32) _
									+ (ITELINES.MTRL + (ChrW(32) _
									+ (vDate + (ChrW(32) + vDate))))))), Nothing)
				vGrList = (rset.GroupYD + (ChrW(44) + rset.GroupMYD))
				vGroupList = X.EVAL((ChrW(73) _
									+ (sg.mtrgroup + (ChrW(44) _
									+ (vGrList + ChrW(41))))))
				If (vGroupList = 1) Then
					TblDetail.INSERT
					TblDetail.MTRL = ITELINES.MTRL
					TblDetail.QTY1 = ITELINES.QTY1
					vQty = ITELINES.QTY1
					vGroupList = X.EVAL((ChrW(73) _
										+ (sg.mtrgroup + (ChrW(44) _
										+ (rset.GroupMYD + ChrW(41))))))
					'X.WARNING('t1-' + sg.mtrgroup + '-' + vGrList + '-' + vGroupList + '-' + sp4.igroup);
103
						'�� ���Ŀ�����Ĭ
					spmyd = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + ChrW(32))), Nothing)
					sp = X.GETSQLDATASET((ChrW(115) _
										+ (vDate + (ChrW(44) _
										+ (vWhouse + (ChrW(44) _
										+ (SALDOC.TRDBRANCH + (ChrW(44) _
										+ (ITELINES.MTRL + (ChrW(44) _
										+ (SALDOC.TRDR + (ChrW(44) _
										+ (SALDOC.TRDR_CUSTOMER_TRDBUSINESS + (ChrW(44) _
										+ (SALDOC.SHIPMENT + ChrW(41))))))))))))))), Nothing)
					spp = X.GETSQLDATASET((ChrW(115) _
										+ (vDate + (ChrW(44) _
										+ (vWhouse + (ChrW(44) _
										+ (SALDOC.TRDBRANCH + (ChrW(44) _
										+ (ITELINES.MTRL + (ChrW(44) _
										+ (SALDOC.TRDR + (ChrW(44) _
										+ (SALDOC.TRDR_CUSTOMER_TRDBUSINESS + (ChrW(44) _
										+ (SALDOC.SHIPMENT + (ChrW(44) + ChrW(92)))))))))))))))), , ChrW(41), Nothing)
					vPrice = (spp.val * -1)
					vPriceVal = (ITELINES.PRICE - vPrice)
					vPriceD = vPriceVal
					vPriceD = (vPriceVal _
									- (vPriceVal _
									* (sp.dsc / 100)))
					vPrice = (ITELINES.PRICE - vPriceD)
					TblDetail.PRICE = vPrice
					vPis = 1
					If (vPrice > 0) Then
						vPis = 1
					End If

					'���Ŀ�����Ĭ ��� �ǽ��Ŀ�ǵ�� ��� ������
					'sosource=5 �����Ĺ�� ����Ĺ�� ���Ŀ������ν
					scp = X.GETSQLDATASET((ChrW(115) + SALDOC.TRDR), Nothing)
					'����ǿ� ������� ������� �����Ĺ��� ����Ĺ��� ����ķ
					spyd = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + ChrW(32))), Nothing)
					'Gm ������
					spay = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + ChrW(32))), Nothing)
					spyd = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + ChrW(32))), Nothing)
					'Gm ������
					spay = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + ChrW(32))), Nothing)
					qq = ChrW(115)
					CType(from(select, case, when, isnull(ex.num04, 0Unknown=0, then, l.qty1, else, (l.qty1 * ex.num04), CType(end, sqty1), from, CType(mtrlines, l), left, outer, join, CType(mtrextra, ex), on, l.mtrl = ex.mtrl, where(l.findoc = 214280), And (l.mtrl, in(select, ll.mtrl, from, CType(cccpricelistlines, ll), inner, join, CType(cccpricelist, hh), on, ll.cccpricelist = hh.cccpricelist, where(hh.sosource = 7), And (ll.cccsumgroup = 102))), group, by, case, when, isnull(ex.num04,0Unknown= 0, Then, l.qty1, Else, (l.qty1 * ex.num04), End),s)
                        'spix = X.GETSQLDATASET('select sum(l.qty1) as qty from mtrlines l where l.findoc=' + iSALDOCID + ' and l.mtrl in (select ll.mtrl from cccpricelistlines ll, cccPriceList hh where ll.cccPriceList=hh.cccPriceList and hh.Sosource=7 and ll.cccSumGroup=102)', null);
                        spix = X.GETSQLDATASET(qq, Nothing)
					spnu = X.GETSQLDATASET((ChrW(115) _
										+ (iSALDOCID + (ChrW(32) + ITELINES.MTRL))), Nothing)
					vQtyY = spyd.qty
					vQtyI = spix.sumqty1
					vQtyA = spay.qty
					vQtyNU = spnu.qty
					swh = X.GETSQLDATASET((ChrW(115) + ITELINES.FINDOCS), Nothing)
					vWhouse = swh.whouse
					'X.WARNING('swh.whouse-' + swh.whouse)
					'if ��� �����ıĹ��
					vWhouse = MTRDOC.WHOUSE
					'X.WARNING('SALDOC.INT02-' + SALDOC.INT02)
					If (SALDOC.INT02 <> 0) Then
						vWhouse = SALDOC.INT02
					End If

					'X.WARNING('vWhouse-' + vWhouse)
					'sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.mtrl=' + ITELINES.MTRL, null);
					'sosource=7 �����Ĺ�� ����Ĺ�� ���Ŀ������ν - �ǽ��Ŀ�ǵ�ɽ
					sp4 = X.GETSQLDATASET((ChrW(115) _
										+ (vWhouse + (ChrW(32) _
										+ (ITELINES.MTRL + (ChrW(32) _
										+ (vDate + (ChrW(32) + vDate))))))), Nothing)
					'X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate);
					If (sp4.igroup = 100) Then
						'����ǿ� ������� ������� �����Ĺ��� ����Ĺ��� ����ķ
						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) _
											+ (SALDOC.TRDR + (ChrW(44) + ChrW(92)))))))))), , ChrW(41), Nothing)
						If (sp.dsc = 0) Then
							sp = X.GETSQLDATASET((ChrW(115) _
												+ (vDate + (ChrW(44) _
												+ (vWhouse + (ChrW(44) _
												+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						End If

						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						'X.WARNING('sum qty 100: '+vQtyY+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
						TblDetail.PRICE = (ITELINES.PRICE _
										* (sp.dsc / 100))
					ElseIf (sp4.igroup = 103) Then
						'����ǿ� ������� ������� �����Ĺ��� ����Ĺ��� ����ķ
						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) _
											+ (SALDOC.TRDR + (ChrW(44) + ChrW(92)))))))))), , ChrW(41), Nothing)
						X.WARNING(sp.dsc)
						If (sp.dsc = 0) Then
							sp = X.GETSQLDATASET((ChrW(115) _
												+ (vDate + (ChrW(44) _
												+ (vWhouse + (ChrW(44) _
												+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						End If

						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						'X.WARNING('sum qty 100: '+vQtyY+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
						'X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate + ' dsc: ' + sp.dsc + ' price: ' + (ITELINES.PRICE * (sp.dsc / 100)));
						TblDetail.PRICE = (ITELINES.PRICE _
										* (sp.dsc / 100))
					ElseIf (sp4.igroup = 102) Then
						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						'X.WARNING('sum qty 102: '+vQtyI+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
						TblDetail.PRICE = (ITELINES.PRICE _
										* (sp.dsc / 100))
					ElseIf (sp4.igroup = 200) Then
						sp = X.GETSQLDATASET((ChrW(115) _
											+ (vDate + (ChrW(44) _
											+ (vWhouse + (ChrW(44) _
											+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
						'X.WARNING('sum qty 200: '+ITELINES.QTY1+' dsc: '+sp.dsc+' price: '+(ITELINES.PRICE*(sp.dsc/100)));
						TblDetail.PRICE = (ITELINES.PRICE _
										* (sp.dsc / 100))
					End If

					'sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
					'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
					If TypeOf Then
                        End If

					vPis = 1
					TblDetail.FINDOCS = iSALDOCID
					TblDetail.POST
				End If

				ITELINES.NEXT

			End While

			If (vPis = 1) Then
				ans = X.ASK(ChrW(65533), ChrW(65533))
				If (ans = 6) Then
					ObjSal.DBPost
				End If

			End If

		Catch  As e
                X.WARNING((ChrW(79) + (vbCr + e)))
		Finally
			ObjSal.FREE
			ObjSal = Nothing
		End Try

		CreateSecondCreditInv()
	End If

	'}
	'X.WARNING('iSALDOCID = ' + iSALDOCID);
	If ((SALDOC.FPRMS = 7040) _
					OrElse ((SALDOC.FPRMS = 7041) _
					OrElse (SALDOC.FPRMS = 7046))) Then
		'ObjSal = X.CreateObj('SALDOC;��ù�� ������� �ɻ�õɽ');                 
		Try
			If (MTRDOC.SOCARRIER <> 9999) Then
				' ���������� ������
				CreateCarrierDoc(0, iSALDOCID)
			End If

		Catch  As e
                X.WARNING((ChrW(79) + (vbCr + e)))
		Finally
			'ObjSal.FREE; 
			'ObjSal =null; 
		End Try

	End If

End Function

Private Function CreateSecondCreditInv() As function
        'var ans = 0;
        'var vDate;
        'var vQty = 0;
        'var vLineVal = 0;
        'var vList0;
        'var vGroupList;
        'var vGrList;
        'var vPrice = 0;
        'var vPriceD = 0;
        'var vPriceVal = 0;
        'var vWhouse = 0;
        Dim vPis = 0
	'''/Not used
	'''/var spis;
	'var vQtyY = 0;
	'var vQtyI = 0;
	'var vQtyA = 0;
	'var vQtyNU = 0;
	ITELINES.FIRST

	While Not ITELINES.EOF
0
			vPis = 1
		Exit While
		ITELINES.NEXT

	End While

	'������ ��� �ǿŽ �����Ǹ�� ��� Ŀ� cccsettings ��� ���̼�ķ ����÷ �����Ĺ���.
	rset = X.GETSQLDATASET((ChrW(115) + X.SYS.COMPANY), Nothing)
	rser = X.GETSQLDATASET((ChrW(115) + SALDOC.SERIES), Nothing)
	X.WARNING(ChrW(65533))
	Return
	'��� ���ĭ� ������ ��� ��������õ�� ��ĵ ����÷ �����Ĺ���
	If (vPis = 1) Then
		If (SALDOC.FINDOC < 0) Then
			'��� �����Ĺ��
			iSALDOCID = X.NEWID
		Else
			'������Ʈ �����Ĺ�ν ��� �ǿŽ �������.
			'DeleteSalDoc(rser.SeriesCreSec);
			iSALDOCID = SALDOC.FINDOC
		End If

		vDate = X.EVAL(ChrW(83))
		ObjSal = X.CreateObj(ChrW(83))
		Try
			ObjSal.DBInsert
			TblHeader = ObjSal.FindTable(ChrW(70))
			TblDetail = ObjSal.FindTable(ChrW(73))
			TblHeader.INSERT
			TblHeader.SERIES = rser.SeriesCreSec
			'rset.series;
			TblHeader.TRDR = SALDOC.TRDR
			TblHeader.TRDBRANCH = SALDOC.TRDBRANCH
			TblHeader.TRNDATE = SALDOC.TRNDATE
			TblHeader.FINDOCS = iSALDOCID
			TblHeader.COMMENTS = (SALDOC.CMPFINCODE + (ChrW(32) + SALDOC.TRDR_CUSTOMER_NAME))
			ITELINES.FIRST

			While Not ITELINES.EOF
0
					TblDetail.INSERT
				TblDetail.MTRL = ITELINES.MTRL
				TblDetail.QTY1 = ITELINES.QTY1
0
					TblDetail.PRICE = (ITELINES.PRICE _
								* (ITELINES.ccCDiscPRC / 100))
0
					TblDetail.PRICE = ITELINES.ccCDiscVAL
				TblDetail.FINDOCS = iSALDOCID
				TblDetail.POST
				ITELINES.NEXT

			End While

			If (vPis = 1) Then
				ans = X.ASK(ChrW(65533), ChrW(65533))
				If (ans = 6) Then
					ObjSal.DBPost
				End If

			End If

		Catch  As e
                X.WARNING((ChrW(79) + (vbCr + e)))
		Finally
			ObjSal.FREE
			ObjSal = Nothing
		End Try

	End If

End Function

Private Function ON_DELETE() As function
        status = 3
	DeleteSalDoc()
End Function

Private Function DeleteSalDoc() As function
        s = (ChrW(83) _
					+ (SALDOC.FINDOC + ChrW(32)))
	ds = X.GETSQLDATASET(s, Nothing)
	If (ds.RECORDCOUNT > 0) Then
		ObjPrdn = X.CreateObj(ChrW(83))
		Try
			ds.FIRST

			While Not ds.EOF
				ObjPrdn.DBLocate(ds.FINDOC)
				ObjPrdn.DBDelete
				ds.NEXT

			End While

		Finally
			ObjPrdn.FREE
			ObjPrdn = Nothing
		End Try

	End If

End Function

Private Function EXECCOMMAND(ByVal Unknown As cmd) As function
        Dim ans = 0
	Dim vDate
	Dim vQty = 0
	Dim vLineVal = 0
	Dim vList0
	Dim vGroupList
	Dim vGrList
	Dim vPrice = 0
	Dim vPriceD = 0
	Dim vPriceVal = 0
	Dim vPis = 0
	Dim vWhouse = 0
	Dim recs = 0
	Dim vQtyY = 0
	Dim vQtyI = 0
	Dim vQtyA = 0
	Dim vQtyMYD = 0
	If (cmd = 150002) Then
		'X.WARNING(SALDOC.FINDOC);
		CancelQTY1COV()
	End If

	If (cmd = 150003) Then
		'X.WARNING(SALDOC.FINDOC);
		CreateCarrierDoc(1, SALDOC.FINDOC)
	End If

	If (cmd = 150001) Then
		GetDiscountPrices()
	End If

End Function

Private Function GetDiscountPrices() As function
        Dim vQtyY = 0
	Dim vQtyI = 0
	Dim vQtyA = 0
	Dim vQtyMYD = 0
	rset = X.GETSQLDATASET((ChrW(115) + X.SYS.COMPANY), Nothing)
	rser = X.GETSQLDATASET((ChrW(115) + SALDOC.SERIES), Nothing)
	If (rset.district = 1) Then
		sd = X.GETSQLDATASET((ChrW(83) + SALDOC.TRDBRANCH), Nothing)
		X.WARNING(ChrW(65533))
		Return
	End If

	If (rset.trdbusiness = 1) Then
		sb = X.GETSQLDATASET((ChrW(83) + SALDOC.TRDR), Nothing)
		X.WARNING(ChrW(65533))
		Return
	End If

	If (rset.shipment = 1) Then
		X.WARNING(ChrW(65533))
		Return
	End If

	'Not used
	'spis = X.GETSQLDATASET('select l.trdr from cccpricelistLines l,cccpricelist p where l.cccpricelist=p.cccpricelist and l.trdr=' + SALDOC.TRDR + ' and p.sosource in (4,6) and l.shipment=' + SALDOC.SHIPMENT + ' and l.district1=(select district1 from trdbranch where trdr=' + SALDOC.TRDR + ' and trdbranch=' + SALDOC.TRDBRANCH + ')', null);
	'if(spis.RECORDCOUNT>0)
	'{
	vDate = X.EVAL(ChrW(83))
	ITELINES.FIRST

	While Not ITELINES.EOF
		'�������� whouse ��� Ŀ ������ͼ��� �����ıĹ�� �� �� -> ���
		swh = X.GETSQLDATASET((ChrW(115) + ITELINES.FINDOCS), Nothing)
		vWhouse = swh.whouse
		'X.WARNING('swh.whouse-' + swh.whouse)
		'if ��� �����ıĹ��
		vWhouse = MTRDOC.WHOUSE
		'X.WARNING('SALDOC.INT02-' + SALDOC.INT02)
		If (SALDOC.INT02 <> 0) Then
			vWhouse = SALDOC.INT02
		End If

		'X.WARNING('vWhouse-' + vWhouse)
		'sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
		'sosource=7 �����Ĺ�� ����Ĺ�� ���Ŀ������ν - �ǽ��Ŀ�ǵ�ɽ
		sp4 = X.GETSQLDATASET((ChrW(115) _
							+ (vWhouse + (ChrW(32) _
							+ (ITELINES.MTRL + (ChrW(32) _
							+ (vDate + (ChrW(32) + vDate))))))), Nothing)
		sg = X.GETSQLDATASET((ChrW(115) + ITELINES.MTRL), Nothing)
		'101 �� ���Ŀ�����Ĭ
		'MTRGROUP    cccSumGroup    Name
		'100 100    ���Ŀ�����Ĭ
		'100 103    �' ����
		'100 200    �����
		'101 103    �' ����
		'102 102    �ǽ��Ŀ�ǵ�� 
		If (sp4.igroup = 100) Then
			'���Ŀ�����Ĭ
			vQtyY = (vQtyY + ITELINES.QTY1)
		ElseIf (sp4.igroup = 103) Then
			'�' ����
			vQtyA = (vQtyA + ITELINES.QTY1)
		ElseIf (sp4.igroup = 102) Then
			'�ǽ��Ŀ�ǵ��
			qty1 = ITELINES.QTY1
			tr = X.GETSQLDATASET((ChrW(115) + ITELINES.MTRL), Nothing)
                (Nothing  _
                            AndAlso (tr.NUM04 <> 0))
                '����ɳ� ton to litr
                qty1 = (qty1 * tr.NUM04)
			vQtyI = (vQtyI + qty1)
		End If

103
			'�� ���Ŀ�����Ĭ ��� �ǹ �' ����
		vQtyMYD = (vQtyMYD + ITELINES.QTY1)
		ITELINES.NEXT

	End While

	ITELINES.FIRST

	While Not ITELINES.EOF
		'sosource=7 �����Ĺ�� ����Ĺ�� ���Ŀ������ν - �ǽ��Ŀ�ǵ�ɽ
		sp4 = X.GETSQLDATASET((ChrW(115) _
							+ (vWhouse + (ChrW(32) _
							+ (ITELINES.MTRL + (ChrW(32) _
							+ (vDate + (ChrW(32) + vDate))))))), Nothing)
		sg = X.GETSQLDATASET((ChrW(115) + ITELINES.MTRL), Nothing)
		'X.WARNING(sg.mtrgroup);
		vGrList = (rset.GroupYD + (ChrW(44) + rset.GroupMYD))
		'vGroupList = X.EVAL('InList(' + sg.mtrgroup + ',' + vGrList + ')');
		vGroupList = X.EVAL((ChrW(73) _
							+ (sg.mtrgroup + (ChrW(44) _
							+ (rset.GroupMYD + ChrW(41))))))
		'X.WARNING(vGroupList);
103
			'�� ���Ŀ�����Ĭ
		'�������� whouse ��� Ŀ ������ͼ��� �����ıĹ�� �� �� -> ���
		swh = X.GETSQLDATASET((ChrW(115) + ITELINES.FINDOCS), Nothing)
		vWhouse = swh.whouse
		'X.WARNING('swh.whouse-' + swh.whouse)
		'if ��� �����ıĹ��
		vWhouse = MTRDOC.WHOUSE
		'X.WARNING('SALDOC.INT02-' + SALDOC.INT02)
		If (SALDOC.INT02 <> 0) Then
			vWhouse = SALDOC.INT02
		End If

		'X.WARNING('vWhouse-' + vWhouse)
		sp = X.GETSQLDATASET((ChrW(115) _
							+ (vDate + (ChrW(44) _
							+ (vWhouse + (ChrW(44) _
							+ (SALDOC.TRDBRANCH + (ChrW(44) _
							+ (ITELINES.MTRL + (ChrW(44) _
							+ (SALDOC.TRDR + (ChrW(44) _
							+ (SALDOC.TRDR_CUSTOMER_TRDBUSINESS + (ChrW(44) _
							+ (SALDOC.SHIPMENT + ChrW(41))))))))))))))), Nothing)
		spp = X.GETSQLDATASET((ChrW(115) _
							+ (vDate + (ChrW(44) _
							+ (vWhouse + (ChrW(44) _
							+ (SALDOC.TRDBRANCH + (ChrW(44) _
							+ (ITELINES.MTRL + (ChrW(44) _
							+ (SALDOC.TRDR + (ChrW(44) _
							+ (SALDOC.TRDR_CUSTOMER_TRDBUSINESS + (ChrW(44) _
							+ (SALDOC.SHIPMENT + (ChrW(44) + ChrW(92)))))))))))))))), , ChrW(41), Nothing)
		vPrice = (spp.val * -1)
		vPriceVal = (ITELINES.PRICE - vPrice)
		vPriceD = vPriceVal
		vPriceD = (vPriceVal _
						- (vPriceVal _
						* (sp.dsc / 100)))
		vPrice = (ITELINES.PRICE - vPriceD)
		ITELINES.NUM02 = vPrice
		'else //if(sg.mtrgroup==rset.GroupYD)
		'{
		'    swh = X.GETSQLDATASET('select whouse from mtrdoc where findoc=' + ITELINES.FINDOCS, null);
		'    vWhouse = swh.whouse;
		'    if (vWhouse == '') {
		'        vWhouse = MTRDOC.WHOUSE;
		'    }
		'    sp = X.GETSQLDATASET('select dbo.fn_clDiscStep3(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + SALDOC.TRDR + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
		'    ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
		'    //X.WARNING('DISC STEP3: '+sp.dsc);
		'    if (sp.dsc != '') {
		'        vPis = 1;
		'    } else {
		'        sp = X.GETSQLDATASET('select dbo.fn_clDiscStep4(' + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + '\'' + MTRDOC.QTY1 + '\'' + ') AS dsc', null);
		'        ITELINES.NUM02 = ITELINES.PRICE * (sp.dsc / 100);
		'        //X.WARNING('DISC STEP4: '+sp.dsc);
		'        if (sp.dsc != '') {
		'            vPis = 1;
		'        }
		'    }
		'}
		ITELINES.NEXT

	End While

	Try
		ITELINES.FIRST

		While Not ITELINES.EOF
			'if (ITELINES.NUM02 == '') {
			'�������� whouse ��� Ŀ ������ͼ��� �����ıĹ�� �� �� -> ���
			swh = X.GETSQLDATASET((ChrW(115) + ITELINES.FINDOCS), Nothing)
			vWhouse = swh.whouse
			'X.WARNING('swh.whouse-' + swh.whouse)
			'if ��� �����ıĹ��
			vWhouse = MTRDOC.WHOUSE
			'X.WARNING('SALDOC.INT02-' + SALDOC.INT02)
			If (SALDOC.INT02 <> 0) Then
				vWhouse = SALDOC.INT02
			End If

			'X.WARNING('vWhouse-' + vWhouse)
			'sp4 = X.GETSQLDATASET('select d.cccSumGroup as igroup from cccpricelistlines d, cccpricelist h where d.cccpricelist=h.cccpricelist and h.sosource=7 and d.Whouse=' + vWhouse + ' and d.mtrl=' + ITELINES.MTRL, null);
			'sosource=7 �����Ĺ�� ����Ĺ�� ���Ŀ������ν - �ǽ��Ŀ�ǵ�ɽ
			sp4 = X.GETSQLDATASET((ChrW(115) _
								+ (vWhouse + (ChrW(32) _
								+ (ITELINES.MTRL + (ChrW(32) _
								+ (vDate + (ChrW(32) + vDate))))))), Nothing)
			'X.WARNING(vQtyY + '-' + vQtyA + '-' + vQtyI + '-' + vWhouse + '-' + sp4.igroup + '-' + ITELINES.MTRL + '-' + vDate);
			If (sp4.igroup = 100) Then
				'sosource=5 �����Ĺ�� ����Ĺ�� ���Ŀ������ν
				scp = X.GETSQLDATASET((ChrW(115) + SALDOC.TRDR), Nothing)
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) _
									+ (SALDOC.TRDR + (ChrW(44) + ChrW(92)))))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP3 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
				If (sp.dsc = 0) Then
					sp = X.GETSQLDATASET((ChrW(115) _
										+ (vDate + (ChrW(44) _
										+ (vWhouse + (ChrW(44) _
										+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
					'X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
				End If

				'Gm �� ����į��ı� cccSumGroup ������ �� ����į��ı� ��� �Į fn_clDiscStep4
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP4 2nd loop group 100: '+sp.dsc+' qty: '+vQtyY);
				'X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
				ITELINES.NUM02 = (ITELINES.PRICE _
								* (sp.dsc / 100))
			ElseIf (sp4.igroup = 103) Then
				'sosource=5 �����Ĺ�� ����Ĺ�� ���Ŀ������ν
				scp = X.GETSQLDATASET((ChrW(115) + SALDOC.TRDR), Nothing)
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) _
									+ (SALDOC.TRDR + (ChrW(44) + ChrW(92)))))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP3 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
				If (sp.dsc = 0) Then
					sp = X.GETSQLDATASET((ChrW(115) _
										+ (vDate + (ChrW(44) _
										+ (vWhouse + (ChrW(44) _
										+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
					'X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
				End If

				'Gm �� ����į��ı� cccSumGroup ������ �� ����į��ı� ��� �Į fn_clDiscStep4
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP4 2nd loop group 103: '+sp.dsc+' qty: '+vQtyA);
				'X.WARNING(vQtyY + vWhouse + sp.dsc + vDate + ',' + vWhouse + ',' + ITELINES.MTRL + ',' + vQtyY);
				ITELINES.NUM02 = (ITELINES.PRICE _
								* (sp.dsc / 100))
			ElseIf (sp4.igroup = 102) Then
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP4 2nd loop group 102: '+sp.dsc+' qty: '+vQtyI);
				ITELINES.NUM02 = (ITELINES.PRICE _
								* (sp.dsc / 100))
			ElseIf (sp4.igroup = 200) Then
				sp = X.GETSQLDATASET((ChrW(115) _
									+ (vDate + (ChrW(44) _
									+ (vWhouse + (ChrW(44) _
									+ (ITELINES.MTRL + (ChrW(44) + ChrW(92)))))))), , ChrW(41), Nothing)
				'X.WARNING('DISC STEP4 2nd loop group 200: '+sp.dsc+' qty: '+vQtyI);
				ITELINES.NUM02 = (ITELINES.PRICE _
								* (sp.dsc / 100))
			End If

			'sp=X.GETSQLDATASET('select dbo.fn_clDiscStep4('+vDate+','+vWhouse+','+ITELINES.MTRL+','+'\''+MTRDOC.QTY1+'\''+') AS dsc',null);
			'ITELINES.NUM02=ITELINES.PRICE*(sp.dsc/100);
			'}
			ITELINES.NEXT

		End While

	Catch  As e
            X.WARNING(e)
	Finally
		'ObjSal.FREE; 
		'ObjSal =null; 
	End Try

End Function

Private Function CancelQTY1COV() As function
        Dim ans
	ans = X.ASK(ChrW(65), (ChrW(65533) + (vbCr + ChrW(65533))))
	' 6=Yes, 7=No, 2=Cancel 
	If ((ans = 7) _
					OrElse (ans = 2)) Then
		X.EXCEPTION(ChrW(65533))
		Return
	End If

	Z = SALDOC.FINDOC
	'X.WARNING(Z
	'Query ��� ����÷ �ɽ ��ı�Ƿ��Ĺü��ɽ �����ıĹ�ν ��õ� Ŀ� ���ǿ�Ŀ� 
	SQL = (ChrW(83) + Z)
	ds = X.GETSQLDATASET(SQL, )
	strIDs = X.EVAL((ChrW(83) _
						+ (ds.FINDOCS + ChrW(41))))
	ObjSal = X.CreateObj(ChrW(83))
	ObjSal.DBLocate(strIDs)
	Try
		TblHeader = ObjSal.FindTable(ChrW(70))
		TblDetail = ObjSal.FindTable(ChrW(73))
		'TblHeader.EDIT;
		'TblDetail.EDIT;
		'��Ĭ�ı÷ 1003 �������
		'7046 ���į�  ���ĵ����� �������÷�
		If ((TblHeader.FPRMS = 7046) _
						AndAlso ((TblHeader.FINSTATES = 1003) _
						AndAlso (TblHeader.ISCANCEL = 0))) Then
			'X.WARNING('ds.FINDOCS-' + ds.FINDOCS);
			'X.WARNING('strIDs-' + strIDs);
			'0  ��ı�Ƿ��Ĺü��(�ǹ)
			'1  ��ı�Ƿ��Ĺü��(������)
			'2  ��ı�Ƿ��Ĺü��(�������)
			'3  ��ı�Ƿ��Ĺü��� //�ı� ����ǵ� ��̼� ������ķı - ��÷ ������ķĿ�
			X.RUNSQL((ChrW(85) + TblHeader.FINDOC), Nothing)
			TblDetail.FIRST

			While Not TblDetail.EOF
				'X.WARNING('TblDetail.QTY1COV-' + TblDetail.QTY1COV);
				X.RUNSQL((ChrW(85) _
									+ (TblDetail.FINDOC + (ChrW(32) + TblDetail.MTRLINES))), Nothing)
				'X.WARNING('TblDetail.QTY1COV-' + TblDetail.QTY1COV);
				'TblDetail.POST;
				TblDetail.NEXT

			End While

			'ObjSal.DBPost;
			X.WARNING(ChrW(79))
		Else
			X.WARNING(ChrW(65533))
		End If

	Catch  As e
            X.WARNING(e)
	Finally
		ObjSal.FREE
		ObjSal = Nothing
	End Try

End Function

Private Function CreateCarrierDoc(ByVal , As ask, ByVal Unknown As iSALDOCID) As function
        Dim ans
	'ans = X.ASK('���������� - ������÷ �������õ�� ��ıƿ��ɽ', '���ÿǮ!!! �� �����������ͽ �������õ�� ��ıƿ��ɽ' + '\r\n' + '�Ž�ǵ�� ? '); // 6=Yes, 7=No, 2=Cancel 
	'if ((ans == 7) || (ans == 2)) {
	'    X.EXCEPTION('� ��������÷ ����θ��� ��� Ŀ ����ķ');
	'    return;
	'}
	Dim trdr
	Dim findocs
	'Find ��ıƿ���-��������Į
	'X.WARNING('MTRDOC.SOCARRIER=' + MTRDOC.SOCARRIER);
	ds = X.GETSQLDATASET((ChrW(83) + MTRDOC.SOCARRIER), Nothing)
	If (ds.RECORDCOUNT <> 1) Then
		X.WARNING(ChrW(65533))
		Return
	Else
		ds = X.GETSQLDATASET((ChrW(83) + ChrW(92)), , Nothing)
		If (ds.RECORDCOUNT <> 1) Then
			X.WARNING((ChrW(65533) _
								+ (ds.CODE + (vbCr + ChrW(32)))))
			Return
		Else
			trdr = ds.TRDR
		End If

	End If

	'X.WARNING('MTRDOC.SOCARRIER_CODE=' + ds.CODE);
	Z = iSALDOCID
	'SALDOC.FINDOC;
	'X.WARNING(Z)
	'Query ��� ����÷ �ɽ ��ı�Ƿ��Ĺü��ɽ �����ıĹ�ν ��õ� Ŀ� ���ǿ�Ŀ� 
	SQL = (ChrW(83) + Z)
	ds = X.GETSQLDATASET(SQL, )
	findocs = ds.FINDOC
	'X.WARNING('findocs ' + findocs)
	'strIDs = X.EVAL('String(' + ds.FINDOC + ')');
	' LINSUPDOC.SERIES 
	'������� �Ž������� ���������ν
	ObjLinSupDoc = X.CreateObj(ChrW(76))
	'if (ObjLinSupDoc === null){
	'    X.WARNING('ObjLinSupDoc == null');
	'}
	'X.WARNING('ObjLinSupDoc' + ObjLinSupDoc);// + ObjLinSupDoc);
	'return;
	Try
		If (findocs <> 0) Then
			ObjLinSupDoc.DBLocate(findocs)
		Else
			ObjLinSupDoc.DBInsert
		End If

		TblHeader = ObjLinSupDoc.FindTable(ChrW(70))
		TblDetail = ObjLinSupDoc.FindTable(ChrW(76))
		'ITELINES');
		If (findocs = 0) Then
			TblHeader.INSERT
			TblHeader.SERIES = 8000
			'TblHeader.TRDBRANCH = SALDOC.TRDBRANCH;
			TblHeader.TRNDATE = SALDOC.TRNDATE
		End If

		TblHeader.TRDR = trdr
		'1624;//SALDOC.TRDR; ���������� �������  MTRDOC.SOCARRIER SOCARRIER.CODE
		'X.WARNING(trdr);
		TblHeader.FINDOCS = iSALDOCID
		ITELINES.FIRST
		first_MTRLINES = ITELINES.MTRLINES
		tccCSHIPVALUE = ITELINES.ccCSHIPVALUE
		tRemarks = ("*======= " _
						+ (TblHeader.FINCODE + (" =======*" + vbCr)))
		tRemarks = (tRemarks + (ChrW(65533) + ("" & vbTab + (ChrW(65533) + ("" & vbTab + (ChrW(65533) + vbCr))))))

		While Not ITELINES.EOF
			If (Not tccCSHIPVALUE _
							= ITELINES.ccCSHIPVALUE) Then
				X.WARNING(ChrW(65533))
				Return
			End If

			ds = X.GETSQLDATASET((ChrW(115) + ITELINES.MTRL), Nothing)
			tRemarks = (tRemarks _
							+ (ds.code + ("" & vbTab _
							+ (ITELINES.QTY1 + ("" & vbTab _
							+ (ITELINES.ccCSHIPVALUE + vbCr))))))
			ITELINES.NEXT

		End While

		'����ķ��õ��
		TblHeader.REMARKS = tRemarks
		'������� ���������ν  sosource=1253   
		mtrlNew = 0
		'7040    ���į� �������÷�    ���ĵ���� �������÷
		'7041    ���į� ����Ŀ���    ���į� ����Ŀ���
		'7046    ���į� ����Ŀ���    ���ĵ���� �������÷
		If ((SALDOC.FPRMS = 7040) _
						OrElse (SALDOC.FPRMS = 7046)) Then
			mtrlNew = 1818
			'64.07.05.0024    ����� ��������.����.Ż��ν-����ν �� ��ı�.��ñ ����ɽ �� ���24%
		End If

		If (SALDOC.FPRMS = 7041) Then
			mtrlNew = 1816
			'64.07.04.0024    ����� ��ı�.Ż��ν-����ν �ɻ�õɽ �� ���.��ñ ����ɽ �� ��� 24%
		End If

		If (findocs = 0) Then
			TblDetail.INSERT
		End If

		TblDetail.MTRL = mtrlNew
		TblDetail.QTY1 = 1
		TblDetail.LINEVAL = MTRDOC.ccCTOTSHIPVALUE
		TblDetail.FINDOCS = iSALDOCID
		TblDetail.MTRLINESS = first_MTRLINES
		'�ɴ����    �������Ʈ    ��    ������� ��
		'204    �.� ������ν ���/���    ���� ����� ���/����� �����.����.�����    4
		'205    �.� ������    ������� �������� ������ ����.����.�����    5
		'207    �.� �����������    ���� ����� ��� - ����������� ����.����.�����    8
		'208    �.� ��Ŀ����Ĺ��    ������������ ���� �� ���Ŀ��    17
		'209    �.� ���ͻ����    ��� ��������� �.�.� �� ���Ŀ��    13
		'212    �.� �������    ���� �.�������� �����.����.�����    2,3
		'WHOUSE    NAME
		'2     212 ���� �.�������� �����.����.�����
		'3     212 ���� �.�������� ����.��.�����̽
		'4     204 ���� ����� ���/����� �����.����.�����
		'5     205 ������� �������� ������ ����.����.�����
		'8     207 ���� ����� ��� - ����������� ����.����.�����
		'13    209 ��� ��������� �.�.� �� ���Ŀ��
		'17    208 ������������ ���� �� ���Ŀ��
		Select Case (parseInt(MTRDOC.WHOUSE))
			Case 2
				'���� �.�������� �����.����.�����
				TblDetail.COSTCNTR = 212
					'�.� �������
			Case 3
				'���� �.�������� ����.��.�����̽
				TblDetail.COSTCNTR = 212
					'�.� �������
			Case 4
				'���� ����� ���/����� �����.����.�����
				TblDetail.COSTCNTR = 204
					'�.� ������ν ���/���
			Case 5
				'������� �������� ������ ����.����.�����
				TblDetail.COSTCNTR = 205
					'�.� ������
			Case 8
				'���� ����� ��� - ����������� ����.����.�����
				TblDetail.COSTCNTR = 207
					'�.� �����������
			Case 13
				'��� ��������� �.�.� �� ���Ŀ��
				TblDetail.COSTCNTR = 209
					'�.� ���ͻ����
			Case 17
				'������������ ���� �� ���Ŀ��
				TblDetail.COSTCNTR = 208
				'�.� ��Ŀ����Ĺ��
		End Select

		TblDetail.POST
		If (ask = 1) Then
			ans = X.ASK(ChrW(65533), (ChrW(65533) + (vbCr + ChrW(65533))))
			If (ans = 6) Then
				ObjLinSupDoc.DBPost
			End If

		Else
			ObjLinSupDoc.DBPost
		End If

	Catch  As e
            X.WARNING((ChrW(67) + (vbCr + e)))
	Finally
		ObjLinSupDoc.FREE
		ObjLinSupDoc = Nothing
	End Try

End Function

Private Function ON_ITELINES_QTY1() As function
        Dim vHouse = MTRDOC.WHOUSE
	Dim vBal = 0
	If ((SALDOC.TFPRMS = 101) _
				OrElse (SALDOC.TFPRMS = 103)) Then
		If (ITELINES.QTY1 > 0) Then
			vBal = X.EVAL(ChrW(70))
			If (ITELINES.QTY1 > vBal) Then
				X.WARNING(ChrW(65533))
			End If

		End If

	End If

End Function

Private Function DelConvertedDocs() As function
        'Query ��� ����÷ �ɽ ��ı�Ƿ��Ĺü��ɽ �����ıĹ�ν ��õ� Ŀ� ���ǿ�Ŀ� 
        strqry = (ChrW(83) + (ChrW(65) _
					+ (X.SYS.COMPANY + (ChrW(32) + SALDOC.FINDOC))))
	ds = X.GETSQLDATASET(strqry, Nothing)
	ds.FIRST

	While Not ds.EOF
		ObjConv = X.CreateObj(ChrW(83))
		'���������� Object �ɻ�õɽ 
		Try
			ObjConv.DBLocate(ds.FINDOC)
			'Locate �ķ� �����Ʈ 
			'ObjConv.DBDelete;//������Ʈ �����Ʈ� 
		Finally
			ObjConv.FREE
			ObjConv = Nothing
		End Try

		ds.NEXT

	End While

End Function