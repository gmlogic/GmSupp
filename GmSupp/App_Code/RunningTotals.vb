Imports System
Imports Microsoft.SqlServer.Server
Imports System.Runtime.Remoting.Messaging
Imports System.Data.SqlTypes
Imports System.Collections.Generic

''' <summary>
''' Class contains CLR scalar functions for calculation of running totals
''' </summary>
Public Class RunningTotals
    ''' <summary>
    ''' Storage Structure for holding actual Total and row number for security check.
    ''' </summary>
    ''' <typeparam name="T">Totals Data Type</typeparam>
    Private Structure RtStorage(Of T As Structure)
        Public Total As T
        Public RowNo As Integer
    End Structure

    ''' <summary>
    ''' Calculates a running totals on TinyInt (byte) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlByte representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalTinyInt(val As SqlByte, id As SqlByte, rowNo As Integer, nullValue As SqlByte) As SqlByte
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlByte)), New RtStorage(Of SqlByte)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on SmallInt (Int) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlInt16 representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalSmallInt(val As SqlInt16, id As SqlByte, rowNo As Integer, nullValue As SqlInt16) As SqlInt16
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlInt16)), New RtStorage(Of SqlInt16)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on Int (Int32) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlInt32 representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalInt(val As SqlInt32, id As SqlByte, rowNo As Integer, nullValue As SqlInt32) As SqlInt32
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlInt32)), New RtStorage(Of SqlInt32)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on BigInt (Int64) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlInt64 representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalBigInt(val As SqlInt64, id As SqlByte, rowNo As Integer, nullValue As SqlInt64) As SqlInt64
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlInt64)), New RtStorage(Of SqlInt64)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on Float (Double) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlDouble representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalFloat(val As SqlDouble, id As SqlByte, rowNo As Integer, nullValue As SqlDouble) As SqlDouble
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlDouble)), New RtStorage(Of SqlDouble)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on Real (Single) data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlSingle representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalReal(val As SqlSingle, id As SqlByte, rowNo As Integer, nullValue As SqlSingle) As SqlSingle
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlSingle)), New RtStorage(Of SqlSingle)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on Money data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlMoney representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalMoney(val As SqlMoney, id As SqlByte, rowNo As Integer, nullValue As SqlMoney) As SqlMoney
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlMoney)), New RtStorage(Of SqlMoney)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function

    ''' <summary>
    ''' Calculates a running totals on Decimal data type
    ''' </summary>
    ''' <param name="val">Value of current row</param>
    ''' <param name="id">ID of the function in single query</param>
    ''' <param name="rowNo">Specifies expecter rowNo. It is for security check to ensure correctness of running totals</param>
    ''' <param name="nullValue">Value to be used for NULL values</param>
    ''' <returns>SqlDecimal representing running total</returns>
    <SqlFunction(IsDeterministic:=True)>
    Public Shared Function RunningTotalDecimal(val As SqlDecimal, id As SqlByte, rowNo As Integer, nullValue As SqlDecimal) As SqlDecimal
        Dim dataName As String = String.Format("MultiSqlRt_{0}", If(id.IsNull, 0, id.Value))

        Dim lastSum As Object = CallContext.GetData(dataName)

        Dim storage = If(lastSum IsNot Nothing, CType(lastSum, RtStorage(Of SqlDecimal)), New RtStorage(Of SqlDecimal)())
        storage.RowNo += 1

        If storage.RowNo <> rowNo Then
            Throw New System.InvalidOperationException(String.Format("Rows were processed out of expected order. Expected RowNo: {0}, received RowNo: {1}", storage.RowNo, rowNo))
        End If

        If Not val.IsNull Then
            storage.Total = If(storage.Total.IsNull, val, storage.Total + val)
        Else
            storage.Total = If(storage.Total.IsNull, nullValue, (If(nullValue.IsNull, storage.Total, storage.Total + nullValue)))
        End If

        CallContext.SetData(dataName, storage)

        Return storage.Total
    End Function
End Class
