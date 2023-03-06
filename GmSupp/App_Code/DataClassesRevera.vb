Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Reflection


Namespace Revera
    Partial Public Class GetPendingOrdersHeaderResult

        Public Property NO_ As Integer
        Public Property COMPANY As Short
        Public Property FINDOC As Integer
        Public Property COMPANY1 As System.Nullable(Of Short)
        Public Property MTRLINES As System.Nullable(Of Integer)
        Public Property SODTYPE As System.Nullable(Of Short)
        Public Property MTRL As System.Nullable(Of Integer)
        Public Property SOSOURCE As System.Nullable(Of Integer)
        Public Property SOREDIR As System.Nullable(Of Integer)
        Public Property SOSOURCE1 As Integer
        Public Property SOREDIR1 As Integer
        Public Property TRNDATE As Date
        Public Property SERIES As Short
        Public Property FPRMS As Short

        Public Property FPRMSNAME As String

        Public Property FINCODE As String
        Public Property SODTYPE1 As Short
        Public Property TRDR As System.Nullable(Of Integer)
        Public Property CODE As String
        Public Property NAME As String
        Public Property CMPMODE As System.Nullable(Of Short)
        Public Property ISPRINT As Short
        Public Property APPRV As Short

        ''' <summary>
        ''' A.INT01 OrderNo
        ''' </summary>
        ''' <returns></returns>
        Public Property OrderNo As System.Nullable(Of Integer)

        ''' <summary>
        ''' A.UFTBL01 Applicant
        ''' </summary>
        ''' <returns></returns>
        Public Property Applicant As System.Nullable(Of Short)

        ''' <summary>
        ''' uf.NAME from uftbl01
        ''' </summary>
        ''' <returns></returns>
        Public Property ApplicantNAME As String

        ''' <summary>
        ''' A.UFTBL02 AS FDeparment
        ''' </summary>
        ''' <returns></returns>
        Public Property FDeparment As System.Nullable(Of Short)

        ''' <summary>
        ''' uf.NAME from uftbl02
        ''' </summary>
        ''' <returns></returns>
        Public Property FDeparmentNAME As String

        Public Property INSUSER As System.Nullable(Of Short)

        Public Property INSUSERNAME As String

        'Public Property ccCApplicant As String

        ''' <summary>
        ''' A.VARCHAR02 as Highers
        ''' </summary>
        ''' <returns></returns>
        Public Property Highers As String

        ''' <summary>
        ''' A.FINSTATES
        ''' </summary>
        ''' <returns></returns>
        Public Property FINSTATES As System.Nullable(Of Short)

        ''' <summary>
        ''' A.FINSTATESNAME
        ''' </summary>
        ''' <returns></returns>
        Public Property FINSTATESNAME As String

    End Class
    Partial Public Class GetPendingOrdersDetailsResult

        Public Property NO_ As Integer
        Public Property FINDOC As Integer
        Public Property MTRLINES As Integer
        Public Property LINENUM As Integer
        Public Property TRDR As System.Nullable(Of Integer)
        Public Property CODE As String
        Public Property NAME As String
        Public Property MTRL As Integer
        Public Property SHIPDATE As System.Nullable(Of Date)
        Public Property DELIVDATE As System.Nullable(Of Date)
        Public Property COMMENTS As String
        Public Property COMMENTS1 As String
        'NMTRLINE.WHOUSE = 1000 '?
        'NMTRLINE.MTRUNIT = 101 '?
        'NMTRLINE.VAT = 1410 ' 0 'Not Null
        Public Property WHOUSE As System.Nullable(Of Short)
        Public Property VAT As Short
        Public Property MTRUNIT As System.Nullable(Of Short)

        Public Property MTRUNITC As String
        Public Property QTY As System.Nullable(Of Double)
        Public Property QTY1 As Double
        Public Property QTY1CANC As Double
        Public Property LINEVAL As System.Nullable(Of Double)
        Public Property RESTMODE As System.Nullable(Of Short)
        Public Property QTY1OPEN As Double
        Public Property LINEVALOPEN As Double
        Public Property CDIM1 As System.Nullable(Of Integer)
        Public Property CDIM2 As System.Nullable(Of Integer)
        Public Property CDIM3 As System.Nullable(Of Integer)
        Public Property CDIMNUSE1 As String
        Public Property CDIMNUSE2 As String
        Public Property CDIMNUSE3 As String

        ''' <summary>
        ''' RUNSQL('SELECT REMARKS FROM MTRL WHERE MTRL= ' + ITELINES.MTRL )
        ''' </summary>
        ''' <returns></returns>
        Public Property REMARKS As String

        Public Property SOANAL As String
        Public Property DISC1VAL As System.Nullable(Of Double)
        Public Property DISC2VAL As System.Nullable(Of Double)
        Public Property DISC3VAL As System.Nullable(Of Double)
        Public Property PRICE As System.Nullable(Of Double)
        Public Property PENDING As System.Nullable(Of Short)
        Public Property SODTYPE As Short
        Public Property ccCAFINDOC As System.Nullable(Of Integer)
        Public Property ccCAMTRLINES As System.Nullable(Of Integer)

        ''' <summary>
        ''' Εγκρ.Ποσ.1
        ''' </summary>
        ''' <returns></returns>
        Public Property NUM03 As Double?

        'NO_,TRNDATE,FINCODE,ApplicantNAME,OrderNo,INSUSERNAME,FPRMSNAME,CODE,NAME
        Public Property TRNDATE As Date
        Public Property FINCODE As String

        ''' <summary>
        ''' uf.NAME from uftbl01
        ''' </summary>
        ''' <returns></returns>
        Public Property ApplicantNAME As String

        '''' <summary>
        '''' A.INT01 RequestNo
        '''' </summary>
        '''' <returns></returns>
        'Public Property RequestNo As System.Nullable(Of Integer)
        Public Property INSUSERNAME As String
        Public Property FPRMSNAME As String
        Public Property TRDRCODE As String
        Public Property TRDRNAME As String

        ''' <summary>
        ''' Κωδικός πελάτη
        ''' </summary>
        ''' <returns></returns>
        Public Property cccTrdr As Integer?

        ''' <summary>
        ''' Κωδικός τμήματος
        ''' </summary>
        ''' <returns></returns>
        Public Property cccTrdDep As Integer?

        ''' <summary>
        ''' Τμήμα κόστους
        ''' </summary>
        ''' <returns></returns>
        Public Property UFTBL02 As Short?

        ''' <summary>
        ''' Calculated (fn_GmGetTransformsDocs,ccCSettingsLines 
        ''' </summary>
        ''' <returns></returns>
        Public Property ApplicationLog As String

    End Class
End Namespace
Partial Public Class DataClassesReveraDataContext
    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetPendingOrders")>
    <ResultType(GetType(Revera.GetPendingOrdersHeaderResult))>
    <ResultType(GetType(Revera.GetPendingOrdersDetailsResult))>
    Public Function GetPendingOrders(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="SmallInt")> ByVal cOMPANY As System.Nullable(Of Short),
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer),
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FOrderNo", DbType:="Int")> ByVal fOrderNo As System.Nullable(Of Integer),
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TOrderNo", DbType:="Int")> ByVal tOrderNo As System.Nullable(Of Integer),
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FINCODE", DbType:="VarChar(30)")> ByVal fINCODE As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FPRMS", DbType:="VarChar(MAX)")> ByVal fPRMS As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="RestMode", DbType:="VarChar(MAX)")> ByVal restMode As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="Applicant", DbType:="VarChar(MAX)")> ByVal applicant As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="Highers", DbType:="VarChar(MAX)")> ByVal highers As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="Pending", DbType:="VarChar(MAX)")> ByVal Pending As String,
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date),
                                     <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date)
                                     ) As IMultipleResults
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), cOMPANY, cODE, mTRL, fOrderNo, tOrderNo, fINCODE, fPRMS, restMode, applicant, highers, Pending, dFROM, dTO)
        'Return CType(result.ReturnValue, ISingleResult(Of Revera.GetPendingOrdersHeaderResult))
        Return CType(result.ReturnValue, IMultipleResults)
    End Function
    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetPendingOrders")>
    'Public Function GetPendingOrders(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="SmallInt")> ByVal cOMPANY As System.Nullable(Of Short), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FOrderNo", DbType:="Int")> ByVal fOrderNo As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TOrderNo", DbType:="Int")> ByVal tOrderNo As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FINCODE", DbType:="VarChar(30)")> ByVal fINCODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FPRMS", DbType:="VarChar(MAX)")> ByVal fPRMS As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="RestMode", DbType:="VarChar(MAX)")> ByVal restMode As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="Applicant", DbType:="VarChar(MAX)")> ByVal applicant As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date)) As ISingleResult(Of Revera.GetPendingOrdersResult)
    '    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), cOMPANY, cODE, mTRL, fOrderNo, tOrderNo, fINCODE, fPRMS, restMode, applicant, dFROM, dTO)
    '    Return CType(result.ReturnValue, ISingleResult(Of Revera.GetPendingOrdersResult))
    'End Function
End Class
