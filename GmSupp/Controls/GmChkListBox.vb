Public Class GmChkListBox
    Dim myArrF As String()
    Dim myArrN As String()
    Public Property GmCheck As Boolean

    Public Event SomethingIsDone(sender As Object, e As EventArgs)

    Private Sub UserControl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Size = Me.Panel1.Size
        Me.Panel1.Height = Me.tlsTop.Height
        Me.TlStxtBox.Width = Me.Width - Me.TlStripBtn.Width - Me.ToolStripSeparator1.Width - 8
        'Me.SendToBack()
        'There might be an easier alternative, but you could use a ListView, set CheckBoxes to true, HeaderStyle To None, And View to List.

        'Correction:

        '        Should have been set View to Details.
        'Me.ListView.CheckBoxes = True
        'Me.ListView.HeaderStyle = ColumnHeaderStyle.None
        'Me.ListView.View = View.List
        Me.dgv.ScrollBars = ScrollBars.Vertical
        Me.dgv.ColumnHeadersVisible = False
        Me.dgv.RowHeadersVisible = False
        'MTRLINEsDataGridView_Styling()
        Me.Size = Me.Panel1.Size
    End Sub

    Private Sub TlStripBtn_Click(sender As Object, e As EventArgs) Handles TlStripBtn.Click
        'RaiseEvent SomethingIsDone(Me, e)
        If Me.Panel1.Height = Me.tlsTop.Height + Me.Panel2.Height + Me.tlsBottom.Height Then
            Me.SendToBack()
            Me.Panel1.Height = Me.tlsTop.Height
        Else
            Me.BringToFront()
            Me.Panel1.Height = Me.tlsTop.Height + Me.Panel2.Height + Me.tlsBottom.Height
        End If
        Me.Height = Me.Panel1.Height
        'Me.Refresh()
    End Sub

    Private Sub TlSBtnUnCheck_Click(sender As Object, e As EventArgs) Handles TlSBtnUnCheck.Click

    End Sub

    Private Sub TlSBtnAdd_Click(sender As Object, e As EventArgs) Handles TlSBtnAdd.Click
        Dim chkLists = dgv.Rows.OfType(Of DataGridViewRow).Where(Function(f) f.Cells("Check").Value = True)
        'Dim cnt = dgvRows.Count
        'Dim chkLists = From ce In Me.dgv.Rows.Cast(Of DataGridViewRow)
        '               Where ce.Cells("Check").Value = True


        'Dim chkFi As List(Of Integer) = (From ce In Me.dgv.Rows.Cast(Of DataGridViewRow)
        '                                 Where ce.Cells("Check").Value = True
        '                                 Select CType(ce.Cells("Key").Value, Integer)).ToList
        Dim chkStr = ""

        For Each chkro As DataGridViewRow In chkLists
            chkStr &= chkro.Cells("Key").Value & ","
        Next
        If Not chkStr = "" Then
            chkStr = chkStr.Substring(0, Len(chkStr) - 1)
        End If

        Me.TlStxtBox.Text = chkStr
        Me.TlStripBtn.PerformClick()

    End Sub
    Dim editableFields_MTRLINEsDataGridView() As String = {"DiscPrc", "ExpDiscVal", "Qty1", "newPrice"}

    Private Sub MTRLINEsDataGridView_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dgv.CurrentCellDirtyStateChanged
        Dim s As DataGridView = sender
        If editableFields_MTRLINEsDataGridView.Contains(s.Columns(s.CurrentCell.ColumnIndex).DataPropertyName) Then
            Exit Sub
        End If

        If dgv.IsCurrentCellDirty Then
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Public Sub dgv_Styling()
        Try

            Me.dgv.AutoGenerateColumns = True
            Me.dgv.AutoResizeColumns()

            myArrF = ("Key,Value").Split(", ")
            myArrN = ("Key,Value").Split(", ")


            'Add Bound Columns
            Dim bad_item_columns() As Integer = {1, 2, 3, 4}
            RemoveGridColumnsByCollection(dgv, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

            For i As Integer = 0 To dgv.Columns.Count - 1
                'Debug.Print(MTRLINEsDataGridView.Columns(i).DataPropertyName & vbTab & MTRLINEsDataGridView.Columns(i).Name)
                dgv.Columns(i).ReadOnly = True
            Next
            'For Each edf In editableFields_MTRLINEsDataGridView
            '    Dim Col As DataGridViewColumn = GetNoColumnDataGridView(Me.MTRLINEsDataGridView, edf)
            '    If Not IsNothing(Col) Then
            '        Col.ReadOnly = False
            '    End If
            'Next

            AddOutOfOfficeColumn(Me.dgv)



            For Each Col In dgv.Columns
                Try
                    Dim t As Type = Col.ValueType
                    If Not IsNothing(t) Then
                        With Col
                            If t.IsGenericType AndAlso t.GetGenericTypeDefinition = GetType(Nullable(Of )) Then
                                If Not t.FullName.IndexOf("System.Double") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    .DefaultCellStyle.Format = "N2"
                                    If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
                                        .DefaultCellStyle.Format = "N3"
                                    End If
                                End If
                                If Not t.FullName.IndexOf("System.DateTime, ") = -1 Or Not t.FullName.IndexOf("System.Decimal") = -1 Or Not t.FullName.IndexOf("System.Money") = -1 Then
                                    If Col.DataPropertyName = "TRNDATE" Then
                                        .DefaultCellStyle.Format = "dd/MM/yyyy HH: mm"
                                    End If

                                End If
                            End If
                            If t.Name = "Double" Or t.Name = "Decimal" Or t.Name = "Money" _
                                 Then
                                .DefaultCellStyle.Format = "N2"
                                If {"Qty1", "SumQty1"}.Contains(Col.DataPropertyName) Then
                                    .DefaultCellStyle.Format = "N3"
                                End If
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
            Me.dgv.AutoResizeColumns()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgv.DataError

        If editableFields_MTRLINEsDataGridView.Contains(sender.Columns(e.ColumnIndex).DataPropertyName) Then
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
    Private Sub MTRLINEsDataGridView_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating
        Dim s As DataGridView = sender
        If s.Columns(e.ColumnIndex).DataPropertyName = "Qty1" Then
            Dim cell As DataGridViewCell = s.CurrentCell
            Dim Qty1 As String = cell.EditedFormattedValue
            If Qty1 = "" Then
                Exit Sub
            End If
            If Not cell.FormattedValue.ToString = Qty1 Then
                'Dim v As vsc = s.Rows(e.RowIndex).DataBoundItem
                'v.Qty1 = Qty1
                'v.LPrice = 0
                'If Not v.ExpDiscVal = 0 Or Not v.DiscPrc = 0 Then
                '    'v.LPrice = v.ExpDiscVal + ((v.newPrice - v.ExpDiscVal) * (v.DiscPrc / 100))
                '    v.LPrice = v.ExpDiscVal + (IIf(v.DiscPrc = 0, v.ExpDiscVal, v.newPrice - v.ExpDiscVal)) * (v.DiscPrc / 100)
                'End If
                'v.TotPrice = v.Qty1 * v.LPrice
                'v.Opdocs.docsPrice = v.TotPrice
            End If
        End If

    End Sub

    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnUnCheck.Click, TlSBtnCheck.Click
        Dim s As ToolStripButton = sender
        Dim check As Boolean = False
        If s.Name = "TlSBtnCheck" Then
            check = True
        Else
            check = False
        End If
        If Me.dgv.SelectedRows.Count > 0 Then
            Dim DrSel As DataGridViewSelectedRowCollection = Me.dgv.SelectedRows
            For Each ds As DataGridViewRow In DrSel
                If Not ds.Cells("Check").Value = check Then
                    ds.Cells("Check").Value = check
                End If
            Next
            'For i As Integer = 0 To DrSel.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(DrSel(i).Index).Item("Check") = True
            'Next
        Else
            For Each ds As DataGridViewRow In Me.dgv.Rows
                ds.Cells("Check").Value = check
            Next
            'For i As Integer = 0 To m_DataSet.Tables(MasterTableName).DefaultView.Count - 1
            '    m_DataSet.Tables(MasterTableName).DefaultView(i).Item("Check") = True
            'Next
        End If
        Me.dgv.RefreshEdit()
    End Sub
End Class
