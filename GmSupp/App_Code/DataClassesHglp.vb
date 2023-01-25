Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Reflection

Namespace Hglp

    Partial Public Class GetItemsStatementsResult1

        Private _COMPANY As Short

        Private _FINDOC As Integer

        Private _MTRTRN As Integer

        Private _X_SOANAL As String

        Private _LINENUM As Integer

        Private _PERIOD As Short

        Private _BRANCH As Short

        Private _SODTYPE As Short

        Private _MTRL As Integer

        Private _SOSOURCE As Integer

        Private _SOREDIR As Integer

        Private _TPRMS As Short

        Private _X_TPRMSNAME As String

        Private _X_FLG04 As System.Nullable(Of Short)

        Private _TRNDATE As Date

        Private _FINCODE As String

        Private _COMMENTS As String

        Private _TRDR As System.Nullable(Of Integer)

        Private _X_CODE As String

        Private _X_NAME As String

        Private _WHOUSE As Short

        Private _QTY1 As Double

        Private _QTY2 As Double

        Private _PRICE As Double

        Private _DISCPRC As Double

        Private _DISCVAL As Double

        Private _LTRNVAL As Double

        Private _LCOSTVAL As Double

        Public Sub New()
            MyBase.New
        End Sub

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_COMPANY", DbType:="SmallInt NOT NULL")>
        Public Property COMPANY() As Short
            Get
                Return Me._COMPANY
            End Get
            Set
                If ((Me._COMPANY = Value) _
                            = False) Then
                    Me._COMPANY = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_FINDOC", DbType:="Int NOT NULL")>
        Public Property FINDOC() As Integer
            Get
                Return Me._FINDOC
            End Get
            Set
                If ((Me._FINDOC = Value) _
                            = False) Then
                    Me._FINDOC = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MTRTRN", DbType:="Int NOT NULL")>
        Public Property MTRTRN() As Integer
            Get
                Return Me._MTRTRN
            End Get
            Set
                If ((Me._MTRTRN = Value) _
                            = False) Then
                    Me._MTRTRN = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_X_SOANAL", DbType:="Text", UpdateCheck:=UpdateCheck.Never)>
        Public Property X_SOANAL() As String
            Get
                Return Me._X_SOANAL
            End Get
            Set
                If (String.Equals(Me._X_SOANAL, Value) = False) Then
                    Me._X_SOANAL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LINENUM", DbType:="Int NOT NULL")>
        Public Property LINENUM() As Integer
            Get
                Return Me._LINENUM
            End Get
            Set
                If ((Me._LINENUM = Value) _
                            = False) Then
                    Me._LINENUM = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PERIOD", DbType:="SmallInt NOT NULL")>
        Public Property PERIOD() As Short
            Get
                Return Me._PERIOD
            End Get
            Set
                If ((Me._PERIOD = Value) _
                            = False) Then
                    Me._PERIOD = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_BRANCH", DbType:="SmallInt NOT NULL")>
        Public Property BRANCH() As Short
            Get
                Return Me._BRANCH
            End Get
            Set
                If ((Me._BRANCH = Value) _
                            = False) Then
                    Me._BRANCH = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SODTYPE", DbType:="SmallInt NOT NULL")>
        Public Property SODTYPE() As Short
            Get
                Return Me._SODTYPE
            End Get
            Set
                If ((Me._SODTYPE = Value) _
                            = False) Then
                    Me._SODTYPE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MTRL", DbType:="Int NOT NULL")>
        Public Property MTRL() As Integer
            Get
                Return Me._MTRL
            End Get
            Set
                If ((Me._MTRL = Value) _
                            = False) Then
                    Me._MTRL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SOSOURCE", DbType:="Int NOT NULL")>
        Public Property SOSOURCE() As Integer
            Get
                Return Me._SOSOURCE
            End Get
            Set
                If ((Me._SOSOURCE = Value) _
                            = False) Then
                    Me._SOSOURCE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_SOREDIR", DbType:="Int NOT NULL")>
        Public Property SOREDIR() As Integer
            Get
                Return Me._SOREDIR
            End Get
            Set
                If ((Me._SOREDIR = Value) _
                            = False) Then
                    Me._SOREDIR = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TPRMS", DbType:="SmallInt NOT NULL")>
        Public Property TPRMS() As Short
            Get
                Return Me._TPRMS
            End Get
            Set
                If ((Me._TPRMS = Value) _
                            = False) Then
                    Me._TPRMS = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_X_TPRMSNAME", DbType:="VarChar(50)")>
        Public Property X_TPRMSNAME() As String
            Get
                Return Me._X_TPRMSNAME
            End Get
            Set
                If (String.Equals(Me._X_TPRMSNAME, Value) = False) Then
                    Me._X_TPRMSNAME = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_X_FLG04", DbType:="SmallInt")>
        Public Property X_FLG04() As System.Nullable(Of Short)
            Get
                Return Me._X_FLG04
            End Get
            Set
                If (Me._X_FLG04.Equals(Value) = False) Then
                    Me._X_FLG04 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TRNDATE", DbType:="DateTime NOT NULL")>
        Public Property TRNDATE() As Date
            Get
                Return Me._TRNDATE
            End Get
            Set
                If ((Me._TRNDATE = Value) _
                            = False) Then
                    Me._TRNDATE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_FINCODE", DbType:="VarChar(30) NOT NULL", CanBeNull:=False)>
        Public Property FINCODE() As String
            Get
                Return Me._FINCODE
            End Get
            Set
                If (String.Equals(Me._FINCODE, Value) = False) Then
                    Me._FINCODE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_COMMENTS", DbType:="VarChar(255)")>
        Public Property COMMENTS() As String
            Get
                Return Me._COMMENTS
            End Get
            Set
                If (String.Equals(Me._COMMENTS, Value) = False) Then
                    Me._COMMENTS = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_TRDR", DbType:="Int")>
        Public Property TRDR() As System.Nullable(Of Integer)
            Get
                Return Me._TRDR
            End Get
            Set
                If (Me._TRDR.Equals(Value) = False) Then
                    Me._TRDR = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_X_CODE", DbType:="VarChar(25)")>
        Public Property X_CODE() As String
            Get
                Return Me._X_CODE
            End Get
            Set
                If (String.Equals(Me._X_CODE, Value) = False) Then
                    Me._X_CODE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_X_NAME", DbType:="VarChar(128)")>
        Public Property X_NAME() As String
            Get
                Return Me._X_NAME
            End Get
            Set
                If (String.Equals(Me._X_NAME, Value) = False) Then
                    Me._X_NAME = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_WHOUSE", DbType:="SmallInt NOT NULL")>
        Public Property WHOUSE() As Short
            Get
                Return Me._WHOUSE
            End Get
            Set
                If ((Me._WHOUSE = Value) _
                            = False) Then
                    Me._WHOUSE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_QTY1", DbType:="Float NOT NULL")>
        Public Property QTY1() As Double
            Get
                Return Me._QTY1
            End Get
            Set
                If ((Me._QTY1 = Value) _
                            = False) Then
                    Me._QTY1 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_QTY2", DbType:="Float NOT NULL")>
        Public Property QTY2() As Double
            Get
                Return Me._QTY2
            End Get
            Set
                If ((Me._QTY2 = Value) _
                            = False) Then
                    Me._QTY2 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PRICE", DbType:="Float NOT NULL")>
        Public Property PRICE() As Double
            Get
                Return Me._PRICE
            End Get
            Set
                If ((Me._PRICE = Value) _
                            = False) Then
                    Me._PRICE = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DISCPRC", DbType:="Float NOT NULL")>
        Public Property DISCPRC() As Double
            Get
                Return Me._DISCPRC
            End Get
            Set
                If ((Me._DISCPRC = Value) _
                            = False) Then
                    Me._DISCPRC = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_DISCVAL", DbType:="Float NOT NULL")>
        Public Property DISCVAL() As Double
            Get
                Return Me._DISCVAL
            End Get
            Set
                If ((Me._DISCVAL = Value) _
                            = False) Then
                    Me._DISCVAL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LTRNVAL", DbType:="Float NOT NULL")>
        Public Property LTRNVAL() As Double
            Get
                Return Me._LTRNVAL
            End Get
            Set
                If ((Me._LTRNVAL = Value) _
                            = False) Then
                    Me._LTRNVAL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LCOSTVAL", DbType:="Float NOT NULL")>
        Public Property LCOSTVAL() As Double
            Get
                Return Me._LCOSTVAL
            End Get
            Set
                If ((Me._LCOSTVAL = Value) _
                            = False) Then
                    Me._LCOSTVAL = Value
                End If
            End Set
        End Property
    End Class

    Partial Public Class GetItemsStatementsResult2

        Private _MTRL As Integer

        Private _PERIOD As Short

        Private _IMPQTY1 As System.Nullable(Of Double)

        Private _IMPQTY2 As System.Nullable(Of Double)

        Private _IMPVAL As System.Nullable(Of Double)

        Private _EXPQTY1 As System.Nullable(Of Double)

        Private _EXPQTY2 As System.Nullable(Of Double)

        Private _EXPVAL As System.Nullable(Of Double)

        Private _IMPCOST As System.Nullable(Of Double)

        Private _EXPCOST As System.Nullable(Of Double)

        Public Sub New()
            MyBase.New
        End Sub

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MTRL", DbType:="Int NOT NULL")>
        Public Property MTRL() As Integer
            Get
                Return Me._MTRL
            End Get
            Set
                If ((Me._MTRL = Value) _
                                = False) Then
                    Me._MTRL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PERIOD", DbType:="SmallInt NOT NULL")>
        Public Property PERIOD() As Short
            Get
                Return Me._PERIOD
            End Get
            Set
                If ((Me._PERIOD = Value) _
                                = False) Then
                    Me._PERIOD = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IMPQTY1", DbType:="Float")>
        Public Property IMPQTY1() As System.Nullable(Of Double)
            Get
                Return Me._IMPQTY1
            End Get
            Set
                If (Me._IMPQTY1.Equals(Value) = False) Then
                    Me._IMPQTY1 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IMPQTY2", DbType:="Float")>
        Public Property IMPQTY2() As System.Nullable(Of Double)
            Get
                Return Me._IMPQTY2
            End Get
            Set
                If (Me._IMPQTY2.Equals(Value) = False) Then
                    Me._IMPQTY2 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IMPVAL", DbType:="Float")>
        Public Property IMPVAL() As System.Nullable(Of Double)
            Get
                Return Me._IMPVAL
            End Get
            Set
                If (Me._IMPVAL.Equals(Value) = False) Then
                    Me._IMPVAL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EXPQTY1", DbType:="Float")>
        Public Property EXPQTY1() As System.Nullable(Of Double)
            Get
                Return Me._EXPQTY1
            End Get
            Set
                If (Me._EXPQTY1.Equals(Value) = False) Then
                    Me._EXPQTY1 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EXPQTY2", DbType:="Float")>
        Public Property EXPQTY2() As System.Nullable(Of Double)
            Get
                Return Me._EXPQTY2
            End Get
            Set
                If (Me._EXPQTY2.Equals(Value) = False) Then
                    Me._EXPQTY2 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EXPVAL", DbType:="Float")>
        Public Property EXPVAL() As System.Nullable(Of Double)
            Get
                Return Me._EXPVAL
            End Get
            Set
                If (Me._EXPVAL.Equals(Value) = False) Then
                    Me._EXPVAL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IMPCOST", DbType:="Float")>
        Public Property IMPCOST() As System.Nullable(Of Double)
            Get
                Return Me._IMPCOST
            End Get
            Set
                If (Me._IMPCOST.Equals(Value) = False) Then
                    Me._IMPCOST = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EXPCOST", DbType:="Float")>
        Public Property EXPCOST() As System.Nullable(Of Double)
            Get
                Return Me._EXPCOST
            End Get
            Set
                If (Me._EXPCOST.Equals(Value) = False) Then
                    Me._EXPCOST = Value
                End If
            End Set
        End Property
    End Class

    Partial Public Class GetItemsStatementsResult3

        Private _MTRL As Integer

        Private _IQ1 As System.Nullable(Of Double)

        Private _IQ2 As System.Nullable(Of Double)

        Private _IVL As System.Nullable(Of Double)

        Private _EQ1 As System.Nullable(Of Double)

        Private _EQ2 As System.Nullable(Of Double)

        Private _EVL As System.Nullable(Of Double)

        Private _ICV As System.Nullable(Of Double)

        Private _ECV As System.Nullable(Of Double)

        Public Sub New()
            MyBase.New
        End Sub

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MTRL", DbType:="Int NOT NULL")>
        Public Property MTRL() As Integer
            Get
                Return Me._MTRL
            End Get
            Set
                If ((Me._MTRL = Value) _
                            = False) Then
                    Me._MTRL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IQ1", DbType:="Float")>
        Public Property IQ1() As System.Nullable(Of Double)
            Get
                Return Me._IQ1
            End Get
            Set
                If (Me._IQ1.Equals(Value) = False) Then
                    Me._IQ1 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IQ2", DbType:="Float")>
        Public Property IQ2() As System.Nullable(Of Double)
            Get
                Return Me._IQ2
            End Get
            Set
                If (Me._IQ2.Equals(Value) = False) Then
                    Me._IQ2 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_IVL", DbType:="Float")>
        Public Property IVL() As System.Nullable(Of Double)
            Get
                Return Me._IVL
            End Get
            Set
                If (Me._IVL.Equals(Value) = False) Then
                    Me._IVL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EQ1", DbType:="Float")>
        Public Property EQ1() As System.Nullable(Of Double)
            Get
                Return Me._EQ1
            End Get
            Set
                If (Me._EQ1.Equals(Value) = False) Then
                    Me._EQ1 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EQ2", DbType:="Float")>
        Public Property EQ2() As System.Nullable(Of Double)
            Get
                Return Me._EQ2
            End Get
            Set
                If (Me._EQ2.Equals(Value) = False) Then
                    Me._EQ2 = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EVL", DbType:="Float")>
        Public Property EVL() As System.Nullable(Of Double)
            Get
                Return Me._EVL
            End Get
            Set
                If (Me._EVL.Equals(Value) = False) Then
                    Me._EVL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ICV", DbType:="Float")>
        Public Property ICV() As System.Nullable(Of Double)
            Get
                Return Me._ICV
            End Get
            Set
                If (Me._ICV.Equals(Value) = False) Then
                    Me._ICV = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ECV", DbType:="Float")>
        Public Property ECV() As System.Nullable(Of Double)
            Get
                Return Me._ECV
            End Get
            Set
                If (Me._ECV.Equals(Value) = False) Then
                    Me._ECV = Value
                End If
            End Set
        End Property
    End Class

    Partial Public Class GetItemsStatementsResult4

        Private _MTRL As Integer

        Private _MPRICE As Double

        Public Sub New()
            MyBase.New
        End Sub

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MTRL", DbType:="Int NOT NULL")>
        Public Property MTRL() As Integer
            Get
                Return Me._MTRL
            End Get
            Set
                If ((Me._MTRL = Value) _
                            = False) Then
                    Me._MTRL = Value
                End If
            End Set
        End Property

        <Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_MPRICE", DbType:="Float NOT NULL")>
        Public Property MPRICE() As Double
            Get
                Return Me._MPRICE
            End Get
            Set
                If ((Me._MPRICE = Value) _
                            = False) Then
                    Me._MPRICE = Value
                End If
            End Set
        End Property
    End Class


    Partial Public Class GetItemsStatementsView
        Public Property comments As String
        Public Property expqty1 As Double
        Public Property expval As Double
        Public Property fincode As String
        Public Property impqty1 As Double
        Public Property impval As Double
        Public Property qty1 As Double
        Public Property remq As Double
        Public Property remv As Double
        Public Property x_tprmsname As String
        Public Property tprms As String
        Public Property trndate As Date
        Public Property whouse As Integer
        Public Property x_name As String
        Public Property AA As Integer
        Public Property flg01 As Short
        Public Property flg04 As Short
    End Class

    Partial Public Class GetTrdrBalanceAllResult
        Public Property TRDR As Integer
        'Public Property FISCPRD As Short
        'Public Property PERIOD As Short
        Public Property pdb As System.Nullable(Of Double)
        Public Property pcr As System.Nullable(Of Double)
        Public Property pbal As System.Nullable(Of Double)
        Public Property plturnover As System.Nullable(Of Double)
        Public Property ptturnover As System.Nullable(Of Double)
        Public Property MaxFiscPrd As System.Nullable(Of Short)
        Public Property MaxPeriod As System.Nullable(Of Short)
    End Class
    Partial Public Class GetTrdrBalanceResult
        Public Property TRDR As Integer
        'Public Property FISCPRD As Short
        'Public Property PERIOD As Short
        Public Property pdb As System.Nullable(Of Double)
        Public Property pcr As System.Nullable(Of Double)
        Public Property pbal As System.Nullable(Of Double)
        Public Property plturnover As System.Nullable(Of Double)
        Public Property ptturnover As System.Nullable(Of Double)
    End Class
    Partial Public Class GetTrdrDetailResult
        Public Property rowNo As System.Nullable(Of Long)
        Public Property findoc As Integer
        Public Property company As Short
        Public Property sodtype As Short
        Public Property sosource As Integer
        Public Property series As Short
        Public Property fprms As Short
        Public Property tfprms As System.Nullable(Of Short)
        Public Property tprms As Short
        Public Property trdr As Integer
        Public Property code As String
        Public Property name As String
        Public Property tdSalesMan As System.Nullable(Of Integer)
        Public Property vtSalesMan As System.Nullable(Of Integer)
        Public Property tdSalesManName As String
        Public Property vtSalesManName As String
        Public Property trndateDa As Date
        Public Property fincodeDa As String
        Public Property ffindocs As System.Nullable(Of Integer)
        Public Property trndate As Date
        Public Property fincode As String
        Public Property tdebit As System.Nullable(Of Double)
        Public Property tcredit As System.Nullable(Of Double)
        Public Property flg03 As Short
        Public Property lturnovr As System.Nullable(Of Double)
        Public Property tturnovr As System.Nullable(Of Double)
        Public Property ltrnval As System.Nullable(Of Double)
        Public Property ttrnval As System.Nullable(Of Double)

        ''' <summary>
        ''' Ανοιχτό Υπόλοιπο
        ''' </summary>
        ''' <returns></returns>
        Public Property bal As Decimal 'Local Field

        ''' <summary>
        ''' Ανεξόφλητα
        ''' </summary>
        ''' <returns></returns>
        Public Property oiBal As Decimal 'Local Field

        ''' <summary>
        ''' Μικτό Ανεξόφλητα
        ''' </summary>
        ''' <returns></returns>
        Public Property oiMixBal As Decimal 'Local Field

        ''' <summary>
        ''' Ανοιχτά Αξιόγραφα
        ''' </summary>
        ''' <returns></returns>
        Public Property oChkQue As Decimal 'Local Field

        ''' <summary>
        ''' Μικτό υπόλοιπο
        ''' </summary>
        ''' <returns></returns>
        Public Property mixBal As Decimal 'Local Field

        Public Property oBalMax As Decimal 'Local Field
        Public Property oBalDate As Date 'Local Field
        Public Property oDays As Integer 'Local Field
        ''' <summary>
        ''' Τζίρος
        ''' </summary>
        ''' <returns></returns>
        Public Property turnovr As Decimal 'Local Field

        ''' <summary>
        ''' ΦΠΑ
        ''' </summary>
        ''' <returns></returns>
        Public Property Vat As Decimal 'Local Field
        Public Property afm As String
        ''' <summary>
        ''' ΧΑΡΑΚΤΗΡΙΣΜΟΣ ΑΣΦΑΛΙΣΗΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property InsuranceChoice As String 'Local Field

        ''' <summary>
        ''' ΑΣΦΑΛΙΣΤΙΚΟ ΟΡΙΟ
        ''' </summary>
        ''' <returns></returns>
        Public Property ExNum01 As Double? 'Local Field

        ''' <summary>
        ''' ΛΗΞΗ ΑΣΦΑΛΙΣΤΙΚΟΥ ΟΡΙΟΥ
        ''' </summary>
        ''' <returns></returns>
        Public Property ExDate01 As Date? 'Local Field

        ''' <summary>
        ''' ΠΙΣΤΩΤΙΚΟ ΟΡΙΟ 1
        ''' </summary>
        ''' <returns></returns>
        Public Property CRDLIMIT1 As Double? 'Local Field

        ''' <summary>
        ''' ΠΙΣΤΩΤΙΚΟ ΟΡΙΟ 2
        ''' </summary>
        ''' <returns></returns>
        Public Property CRDLIMIT2 As Double? 'Local Field

        ''' <summary>
        ''' ΛΗΞΗ ΠΙΣΤ ΟΡΙΟΥ 1
        ''' </summary>
        ''' <returns></returns>
        Public Property ExDate02 As Date? 'Local Field

        ''' <summary>
        ''' ΗΜΕΡΕΣ ΠΙΣΤΩΣΗΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property ExNum02 As Double? 'Local Field

        ''' <summary>
        ''' ΜΧΕ
        ''' </summary>
        ''' <returns></returns>
        Public Property mxe As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΕΡΒΑΣΗ ΑΣΦΑΛΙΣΤΙΚΟΥ ΟΡΙΟΥ
        ''' </summary>
        ''' <returns></returns>
        Public Property InsuranceLimitException As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΕΡΒΑΣΗ ΠΙΣΤΩΤΙΚΟΥ ΟΡΙΟΥ
        ''' </summary>
        ''' <returns></returns>
        Public Property CreditLimitException As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΕΡΒΑΣΗ ΠΙΣΤΩΣΗΣ ΠΕΛΑΤΗ
        ''' </summary>
        ''' <returns></returns>
        Public Property CreditCustomerException As Decimal 'Local Field

        ',ΥΠΟΛΟΙΠΟ 2 ΜΗΝΕΣ ΠΑΛΑΙΟΤΕΡΟ,ΥΠΟΛΟΙΠΟ 3 ΜΗΝΕΣ ΠΑΛΑΙΟΤΕΡΟ,ΥΠΟΛΟΙΠΟ 4 ΜΗΝΕΣ ΠΑΛΑΙΟΤΕΡΟ,ΥΠΟΛΟΙΠΟ 5 ΜΗΝΕΣ ΠΑΛΑΙΟΤΕΡΟ,ΥΠΟΛΟΙΠΟ 6 ΜΗΝΕΣ ΠΑΛΑΙΟΤΕΡΟ,ΥΠΟΛΟΙΠΟ > ΤΩΝ 6 ΜΗΝΩΝ
        'ΥΠΟΛΟΙΠΟ 1-30 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ 31-60 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ 61-90 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ 91-120 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ 121-150 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ 151-180 ΗΜΕΡΕΣ,ΥΠΟΛΟΙΠΟ > 181 ΗΜΕΡΕΣ
        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 1-30 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal001_030 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 31-60 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal031_060 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 61-90 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal061_090 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 91-120 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal091_120 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 121-150 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal121_150 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ 151-180 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property Bal151_180 As Decimal 'Local Field

        ''' <summary>
        ''' ΥΠΟΛΟΙΠΟ > 181 ΗΜΕΡΕΣ
        ''' </summary>
        ''' <returns></returns>
        Public Property BalMoreThan181 As Decimal 'Local Field


        'Public Property TRDR As Integer
        'Public Property TRNDATE As Date
        Public Property FINALDATE As System.Nullable(Of Date)
        'Public Property FINCODE As String
        'Public Property FINDOC As Integer
        'Public Property SOSOURCE As System.Nullable(Of Integer)
        Public Property SOREDIR As System.Nullable(Of Integer)
        Public Property TVAL As System.Nullable(Of Double)
        Public Property TRDRRATE As System.Nullable(Of Double)
        Public Property SALESMAN As System.Nullable(Of Integer)
        Public Property SALCODE As String
        Public Property SALNAME As String
        Public Property COLCODE As String
        Public Property COLNAME As String
        Public Property BUSUNITS As System.Nullable(Of Short)
        Public Property PJCODE As String
        Public Property PJNAME As String
        Public Property TRCODE As String
        Public Property TRNAME As String
        Public Property PAYMENT As System.Nullable(Of Short)
        Public Property SHIPMENT As System.Nullable(Of Short)
        Public Property SOCARRIER As System.Nullable(Of Short)
        Public Property OPNTVAL As System.Nullable(Of Double)
        Public Property SOCURRENCY As Short
        Public Property OITYPE As System.Nullable(Of Short)
        Public Property STAMNT As System.Nullable(Of Double)
        Public Property SOPNTAMNT As System.Nullable(Of Double)


    End Class
    Partial Public Class GetTrdrFinPaytermsResult

        Public Property TRDR As Integer
        Public Property TRNDATE As Date
        Public Property FINALDATE As System.Nullable(Of Date)
        Public Property FINCODE As String
        Public Property FINDOC As Integer
        Public Property SOSOURCE As System.Nullable(Of Integer)
        Public Property SOREDIR As System.Nullable(Of Integer)
        Public Property TRDRRATE As System.Nullable(Of Double)
        Public Property SALESMAN As System.Nullable(Of Integer)
        Public Property SALCODE As String
        Public Property SALNAME As String
        Public Property COLCODE As String
        Public Property COLNAME As String
        Public Property BUSUNITS As System.Nullable(Of Short)
        Public Property PJCODE As String
        Public Property PJNAME As String
        Public Property TRCODE As String
        Public Property TRNAME As String
        Public Property PAYMENT As System.Nullable(Of Short)
        Public Property SHIPMENT As System.Nullable(Of Short)
        Public Property SOCARRIER As System.Nullable(Of Short)
        Public Property TVAL As System.Nullable(Of Double)
        Public Property OPNTVAL As System.Nullable(Of Double)
        Public Property SOCURRENCY As Short
        Public Property OITYPE As System.Nullable(Of Short)
        Public Property STAMNT As System.Nullable(Of Double)
        Public Property SOPNTAMNT As System.Nullable(Of Double)

    End Class
    ''' <summary>
    ''' afterBal  AND (vt.trndate > @DTO) AND (vt.trndate μικρότερη getdate())
    ''' </summary>
    Partial Public Class GetTrdrDetailAfterResult

        Public Property rowNo As System.Nullable(Of Long)
        Public Property findoc As Integer
        Public Property company As Short
        Public Property sodtype As Short
        Public Property sosource As Integer
        Public Property series As Short
        Public Property fprms As Short
        Public Property tfprms As System.Nullable(Of Short)
        Public Property tprms As Short
        Public Property trdr As Integer
        Public Property trndate As Date
        Public Property fincode As String
        Public Property tdebit As System.Nullable(Of Double)
        Public Property tcredit As System.Nullable(Of Double)
        Public Property flg03 As Short
        Public Property lturnovr As System.Nullable(Of Double)
        Public Property tturnovr As System.Nullable(Of Double)
        Public Property ltrnval As System.Nullable(Of Double)
        Public Property ttrnval As System.Nullable(Of Double)
        Public Property bal As Decimal
        Public Property oibal As Decimal

    End Class
    Partial Public Class GetTrdrBalanceBefYearResult

        Public Property TRDR As Integer
        Public Property prdb As System.Nullable(Of Double)
        Public Property prcr As System.Nullable(Of Double)
        Public Property prbal As System.Nullable(Of Double)
        Public Property prlturnover As System.Nullable(Of Double)
        Public Property prtturnover As System.Nullable(Of Double)

    End Class

    Partial Public Class GetTrdrChequeOpenResult
        Public Property COMPANY As Short
        Public Property SODTYPE As Short
        Public Property TRDR As System.Nullable(Of Integer)
        Public Property TRDRPOSSESSOR As System.Nullable(Of Integer)
        Public Property CHEQUE As Integer
        Public Property CHEQUENUMBER As String
        Public Property CODE As String
        Public Property CRTDATE As Date
        Public Property FINALDATE As Date
        Public Property FPRMS As Short
        Public Property CHEQUESTATES As Short
        Public Property LCHEQUEVAL As Double
        Public Property LCHEQUEBAL As Double
        Public Property TRDRPUBLISHER As System.Nullable(Of Integer)
        Public Property SODTYPEPOSSESSOR As System.Nullable(Of Short)
        Public Property HOLDERNAME As String
        Public Property SODTYPEPUBLISHER As Short
        Public Property ISCANCEL As Short
    End Class

    Partial Public Class vsc
        Public Property Company As Short
        Public Property SoSource As Integer
        Public Property TFprms As Short
        Public Property TSodType As Short
        Public Property IsCancel As Short
        Public Property Fprms As Short
        Public Property Findoc As Integer
        Public Property series As Integer?
        Public Property TrnDate As Date
        Public Property FinCode As String
        Public Property Trdr As Integer?
        Public Property TrdBranch As Integer?
        Public Property CODE As String
        Public Property NAME As String
        Public Property comments As String
        Public Property mtrl As Integer
        Public Property m_CODE As String '
        Public Property m_NAME As String
        Public Property findocs As Integer?
        Public Property MtrTrn As Integer
        Public Property VATNAME As String
        Public Property Vat As Short '
        Public Property PERCNT As Double '
        Public Property Qty1 As Double '
        Public Property Price As Double
        Public Property LPrice As Double '
        Public Property DiscValPrice As Double
        Public Property TotPrice As Double
        Public Property DiscPrc As Double
        Public Property ExpDiscVal As Double
        Public Property ViewDocs As String
        Public Property ViewdocsFindoc As String
        Public Property ViewdocsPrice As String

        'Public Property Opdocs As OpenItemDocs
        Public Property pisPrice As Double?
        Public Property newPrice As Double?
        Public Property LinesNo As Integer
        Public Property MTRUNITC As String
        Public Property remarks As String
    End Class
    Partial Public Class OpenItemDocs
        Public Property docs As String
        Public Property docsFindoc As Integer
        Public Property docsPrice As Double
        Public Property docsPerCnt As Short
        Public Property docsTotPrice As Double
    End Class

    Partial Public Class ccCBCLabel
        Public Property ccCBCLabel As Integer
        Public Property MTRL As Integer
        Public Property PrdDate As Date
        Public Property PackingDate As Date
        Public Property Shift As Short
        Public Property LabelsNo As Integer
        Public Property PrintedLabels As Integer
        Public Property CanceledLabels As Integer
        Public Property Barcode As Byte()
        Public Property BCLabel As String
        Public Property Machine As Short
    End Class

    Partial Public Class ccCDescr
        Public Property mtrl As Integer
        Public Property LBName As String
        Public Property LBCode As Short
        Public Property Pack As Short
        Public Property Weight As Short
    End Class

    Partial Public Class BCLabel
        Public Property mtrl As Integer
        Public Property code As String
        Public Property Name As String
        Public Property LBName As String
        Public Property LBCode As Short
        Public Property Pack As Short
        Public Property Weight As Short
        Public Property PrdDate As Date
        Public Property PackingDate As Date
        Public Property Shift As Short
        Public Property LabelsNo As Integer
        Public Property PrintedLabels As Integer
        Public Property CanceledLabels As Integer
        Public Property Machine As Short
    End Class

End Namespace

Partial Public Class DataClassesHglpDataContext

    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.testFetchWhouses")>
    '<ResultType(GetType(Hglp.testFetchWhousesResult))>
    '<ResultType(GetType(Hglp.MTRL))>
    'Public Function testFetchWhousesMu(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WHOUSE", DbType:="VarChar(MAX)")> ByVal wHOUSE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FISCPRD", DbType:="Int")> ByVal fISCPRD As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="PERIOD", DbType:="Int")> ByVal pERIOD As System.Nullable(Of Integer)) As IMultipleResults
    '    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, dFROM, dTO, fISCPRD, pERIOD)
    '    Return CType(result.ReturnValue, IMultipleResults)
    'End Function


    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetItemsStatements")>
    '<ResultType(GetType(Hglp.cccPriceList))>
    '<ResultType(GetType(Hglp.MTRL))>
    'Public Function GetItemsStatements(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WHOUSE", DbType:="VarChar(MAX)")> ByVal wHOUSE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRACN", DbType:="VarChar(MAX)")> ByVal mTRACN As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date))
    '    Dim result As IMultipleResults = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, mTRACN, dFROM, dTO)
    '    Return CType(result.ReturnValue, IMultipleResults(Of Hglp.GetItemsStatements))
    'End Function


    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetItemsStatements")>
    'Public Function GetItemsStatements(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WHOUSE", DbType:="VarChar(MAX)")> ByVal wHOUSE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRACN", DbType:="VarChar(MAX)")> ByVal mTRACN As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date)) As ISingleResult(Of Hglp.GetItemsStatementsResult)
    '    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, mTRACN, dFROM, dTO)
    '    Return CType(result.ReturnValue, ISingleResult(Of Hglp.GetItemsStatementsResult))
    'End Function

    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetItemsStatements")>
    <ResultType(GetType(Hglp.GetItemsStatementsResult1))>
    <ResultType(GetType(Hglp.GetItemsStatementsResult2))>
    <ResultType(GetType(Hglp.GetItemsStatementsResult3))>
    <ResultType(GetType(Hglp.GetItemsStatementsResult4))>
    Public Function GetItemsStatements(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer),
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String,
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer),
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer),
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WHOUSE", DbType:="VarChar(MAX)")> ByVal wHOUSE As String,
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRACN", DbType:="VarChar(MAX)")> ByVal mTRACN As String,
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date),
                                       <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date)) As IMultipleResults
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, mTRACN, dFROM, dTO)
        Return CType(result.ReturnValue, IMultipleResults)
        'Return New Object
    End Function

    '    Όλα
    'Εκκρεμείς Παραγγελίες
    'PICKING
    'Κατάσταση παραδόσεων
    'ΕΠΙΣΤΡΟΦΕΣ

    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.FetchWhousesDaily")>
    '<ResultType(GetType(Hglp.FetchWhousesDailyResult1))>'Κατάσταση παραδόσεων
    '<ResultType(GetType(Hglp.FetchWhousesDailyResult1))>'Εκκρεμείς Παραγγελίες
    '<ResultType(GetType(Hglp.FetchWhousesDailyResult1))>'PICKING
    '<ResultType(GetType(Hglp.FetchWhousesDailyResult1))>
    'Public Function FetchWhousesDaily(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRL", DbType:="Int")> ByVal mTRL As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="WHOUSE", DbType:="VarChar(MAX)")> ByVal wHOUSE As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="MTRACN", DbType:="VarChar(MAX)")> ByVal mTRACN As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FISCPRD", DbType:="Int")> ByVal fISCPRD As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="PERIOD", DbType:="Int")> ByVal pERIOD As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SOSOURCE", DbType:="Int")> ByVal sOSOURCE As System.Nullable(Of Integer),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FPRMS", DbType:="VarChar(MAX)")> ByVal fPRMS As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TFPRMS", DbType:="VarChar(MAX)")> ByVal tFPRMS As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TRDBUSINESS", DbType:="VarChar(MAX)")> ByVal tRDBUSINESS As String,
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="ISCANCEL", DbType:="SmallInt")> ByVal iSCANCEL As System.Nullable(Of Short),
    '                                  <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="FULLYTRANSF", DbType:="VarChar(MAX)")> ByVal fULLYTRANSF As String) As IMultipleResults
    '    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), mTRL, cODE, cOMPANY, sODTYPE, wHOUSE, mTRACN, dFROM, dTO, fISCPRD, pERIOD, sOSOURCE, fPRMS, tFPRMS, tRDBUSINESS, iSCANCEL)
    '    Return CType(result.ReturnValue, IMultipleResults)
    'End Function

    <Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetTrdrBalance")>
    <ResultType(GetType(Hglp.GetTrdrBalanceBefYearResult))>
    <ResultType(GetType(Hglp.GetTrdrBalanceAllResult))>
    <ResultType(GetType(Hglp.GetTrdrBalanceResult))>
    <ResultType(GetType(Hglp.GetTrdrDetailResult))>
    <ResultType(GetType(Hglp.GetTrdrDetailAfterResult))>
    <ResultType(GetType(Hglp.GetTrdrChequeOpenResult))>
    <ResultType(GetType(Hglp.GetTrdrFinPaytermsResult))>
    Public Function GetTrdrBalance(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TRDR", DbType:="Int")> ByVal tRDR As System.Nullable(Of Integer),
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(MAX)")> ByVal cODE As String,
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer),
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer),
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date),
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date),
                                   <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="Details", DbType:="Bit")> ByVal details As System.Nullable(Of Boolean)
                                   ) As IMultipleResults
        Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), tRDR, cODE, cOMPANY, sODTYPE, dFROM, dTO, details)
        'Return CType(result.ReturnValue, ISingleResult(Of Hglp.GetTrdrBalanceResult))
        Return CType(result.ReturnValue, IMultipleResults)
    End Function
    '<Global.System.Data.Linq.Mapping.FunctionAttribute(Name:="dbo.GetTrdrBalance")> _
    'Public Function GetTrdrBalance(<Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="TRDR", DbType:="Int")> ByVal tRDR As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="CODE", DbType:="VarChar(25)")> ByVal cODE As String, <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="COMPANY", DbType:="Int")> ByVal cOMPANY As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="SODTYPE", DbType:="Int")> ByVal sODTYPE As System.Nullable(Of Integer), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DFROM", DbType:="DateTime")> ByVal dFROM As System.Nullable(Of Date), <Global.System.Data.Linq.Mapping.ParameterAttribute(Name:="DTO", DbType:="DateTime")> ByVal dTO As System.Nullable(Of Date)) As ISingleResult(Of Hglp.GetTrdrBalanceResult)
    '    Dim result As IExecuteResult = Me.ExecuteMethodCall(Me, CType(MethodInfo.GetCurrentMethod, MethodInfo), tRDR, cODE, cOMPANY, sODTYPE, dFROM, dTO)
    '    Return CType(result.ReturnValue, ISingleResult(Of Hglp.GetTrdrBalanceResult))
    'End Function
End Class


