Public Class SearchFRold
    Private df As GmData
    Private m_ds As New DataSet
    ' DataTables.
    Private m_dt As DataTable
    'Private MeLabel As String
    Private CurrentDataRowView As DataRowView
    Private DeleteRecord As Boolean
    Private myArrF() As String
    Private myArrN() As String
    Private TlSItem As New ArrayList
    Public m_ArrayList As New ArrayList
    Private mSql As String
    Private mGroupSql As String
    Private mGroupSqlField As String
    Private mTableName As String
    Private mCheck As Boolean
    Private mVisible As Boolean
    Private mPELPRO As Byte
    Dim TlSTxtWild As ToolStripTextBox
    Dim TlSLblWild As ToolStripLabel
    Function Me_Load(ByVal ar As ArrayList, ByVal GmLabel As String, ByVal GmCheck As Boolean, ByVal GmSql As String, ByVal GmRsWhere As String, ByVal GmRsOrder As String, ByVal GmGroupSql As String, ByVal GmGroupSqlField As String, ByVal GmTableName As String, ByVal GmArrF() As String, ByVal GmArrN() As String, ByVal Point As System.Drawing.Point, ByVal Visible As Boolean) As ArrayList
        Me_Load = Nothing
        Me.m_ds = ar(0) 'm_ds
        Me.Text = ar(1) 'GmTitle
        Me.mCheck = ar(2) 'GmCheck
        Me.mSql = ar(3) 'sSQL
        Me.mPELPRO = ar(4) 'GmPelPro
        RsWhere = ar(5) 'RsWhere
        RsOrder = ar(6) 'RsOrder
        Me.mGroupSql = ar(7) 'GmGroupSql
        Me.mGroupSqlField = ar(8) 'GmGroupSqlField
        Me.mTableName = ar(9) 'RsTables
        Me.myArrF = ar(10) ' myArrF
        Me.myArrN = ar(11) ' myArrN
        Me.Location = ar(12) 'Point
        Me.mVisible = ar(13) 'Visible

        '' Load the data.
        ''LoadData()
        '' Bind the controls to the data tables.
        ''BindControls()
        ''Me.TlSTxtWild.Clear()
        'Select Case BindingSource1.Count
        '    Case 0

        '        'Case 1
        '        '    CurrentDataRowView = BindingSource1.Current()
        '        '    Try
        '        '        Dim row As DataRow = m_dt.NewRow()
        '        '        row.ItemArray = CurrentDataRowView.Row.ItemArray
        '        '        m_dt.Rows.Add(row)
        '        '    Catch ex As Exception

        '        '    End Try
        '    Case Else
        '        'Me.ShowDialog()
        'End Select
        'm_ArrayList.Clear()
        'm_ArrayList.Add(m_dt)
        'Return m_ArrayList
    End Function
    Private mReturnFields As New Collection()
    ''' <summary>
    ''' ΓΙΑ DELETE ChRet = nothing
    ''' </summary>
    Public Property GmReturnFields() As Object
        Get
            Return mReturnFields
        End Get
        Set(ByVal value As Object)
            If value Is Nothing Then
                mReturnFields.Clear()
            Else
                mReturnFields.Add(value)
            End If
        End Set

    End Property
    Private Sub SearchFR_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DockChanged

    End Sub

    Private Sub Me_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If TypeOf Me.ActiveControl Is TextBox Then
            Dim NextControl As Boolean
            Select Case e.KeyCode
                Case Keys.Enter
                    NextControl = True
                Case Keys.Up
                    NextControl = False
                Case Keys.Down
                    NextControl = True
                Case Else
                    Exit Sub
            End Select
            SelectNextControl(Me.ActiveControl, NextControl, True, True, True)
        End If
        If TypeOf Me.ActiveControl Is DataGridView Then
            If e.KeyCode = Keys.Enter Then
                'SendKeys.Send("{TAB}")
            End If
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()   'SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub SearchFR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = CType(ChrW(Keys.Enter), Char) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ' Set restrictions on controls.
        RestrictControls()
        ' Load the data.
        LoadData()
        ' Bind the controls to the data tables.
        BindControls()
        'Display the details for the initially selected customer.
        'DisplayDetails()
        ' Make the name list as large as possible.
        'ArrangeControls()
        'Me.Controls.Add(GmDgLookUp1)
        'GmDgLookUp1.Visible = False
        'cmdClear.Visible = False

        'If DeleteRecord Then
        '    BindingNavigatorDeleteItem.Visible = True
        '    MeLabel = "Διαγραφή   Εγγραφής"
        '    BindingNavigatorDeleteItem_Click(Nothing, Nothing)
        '    Me.Close()
        'End If
        'Me.Text = MeLabel
        'PCODETextBox.Select()
    End Sub
    ' Set restrictions on the controls.
    Private Sub RestrictControls()
        'Me.GenDataSet.Tables("Status1").Clear()
        'Dim row As DataRow
        'row = Me.GenDataSet.Tables("Status1").NewRow
        'row("Value1") = 0
        'row("Descr1") = ""
        'Me.GenDataSet.Tables("Status1").Rows.Add(row)
        'row = Me.GenDataSet.Tables("Status1").NewRow
        'row("Value1") = 1
        'row("Descr1") = "+"
        'Me.GenDataSet.Tables("Status1").Rows.Add(row)
        'row = Me.GenDataSet.Tables("Status1").NewRow
        'row("Value1") = -1
        'row("Descr1") = "-"
        'Me.GenDataSet.Tables("Status1").Rows.Add(row)

        'Me.DRComboBox.Items.Clear()
        'DRComboBox.Items.Add("Αυξάνη")
        'DRComboBox.Items.Add("Μειώνη")
        'Dim USStates As New ArrayList()
        'USStates.Add(New USState("---", "0"))
        'USStates.Add(New USState("Αυξάνει", "1"))
        'USStates.Add(New USState("Μειώνει", "-1"))
        'AddHandler DRComboBox.SelectedValueChanged, AddressOf DRComboBox_SelectedValueChanged
        'DRComboBox.DataSource = Me.GenDataSet.Tables("Status1") 'USStates
        'DRComboBox.DisplayMember = "Descr1"
        'DRComboBox.ValueMember = "Value1"
    End Sub
    ' Load the data.
    Private Sub LoadData()
        'df = New GmData(sysDB, CONNECT_STRING) '
        'RsWhere = "Company = " & Company
        'sSQL = "SELECT * FROM PEL1 WHERE " & RsWhere
        'sSQL = SelectPelPro(sSQL, PelPro)
        df = New GmData(sysDB, CONNECT_STRING)
        'sqlFilter(RsWhere, RsOrder)
        If mCheck = True Then
            m_ds.Tables(mTableName).Columns.Add("Check", GetType(Boolean))
        End If
        m_ds.Tables(mTableName).AcceptChanges() 'Για να καταλαβένη τα Check
        m_dt = m_ds.Tables(mTableName).Clone
        BindingSource1.DataSource = m_ds.Tables(mTableName).DefaultView
        m_ds.Tables(mTableName).DefaultView.AllowNew = False
        m_ds.Tables(mTableName).DefaultView.AllowDelete = False
        Dim dt As DataTable = m_ds.Tables(mTableName).Clone
        For i As Integer = 0 To dt.Columns.Count - 1
            dt.Columns(i).DataType = System.Type.GetType("System.String")
        Next
        dt.Rows.Add(dt.NewRow())
        BindingSource2.DataSource = dt.DefaultView
    End Sub
    Sub InitiliazeDataGridView()
        Me.Controls.Remove(Me.DataGridView1)
        Me.DataGridView1 = Nothing
        Me.DataGridView2 = Nothing
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 28)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(642, 274)
        Me.DataGridView1.TabIndex = 6
        '
        'BindingSource1
        '
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(3, 2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(642, 46)
        Me.DataGridView2.TabIndex = 7
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(Me.DataGridView1)
        Me.ResumeLayout(False)
    End Sub
    ' Bind the controls to the data tables.
    Private Sub BindControls()
        TlSItem.Clear()
        Try
            ' Make a DataView for this table.
            If m_ds.Tables(mTableName).Rows.Count > 0 Then
                TlSBtnCheck.Enabled = False
                TlSBtnUnCheck.Enabled = False
                If mCheck = True Then
                    'm_ds.Tables(mTableName).Columns.Add("Check", GetType(Boolean))
                    TlSBtnCheck.Enabled = True
                    TlSBtnUnCheck.Enabled = True
                End If
                BindingNavigator1.BindingSource = BindingSource1
                BindingSource1.ResetBindings(False)
                'InitiliazeDataGridView()
                DataGridView1.DataSource = BindingSource1
                DataGridView2.DataSource = BindingSource2
                DataGridView2.ScrollBars = ScrollBars.Vertical
                If Not myArrF Is Nothing Then
                    RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(mTableName).DefaultView, False)
                    RemoveGridColumns(DataGridView2, Nothing, myArrF, myArrN, m_ds.Tables(mTableName).DefaultView, False)
                    'Dim ar(DataGridView1.Columns.Count) As String
                    'For i As Integer = 0 To DataGridView1.Columns.Count - 1
                    '    ar(i) = DataGridView1.Columns(i).DataPropertyName
                    'Next
                    'Dim ar1(DataGridView1.Columns.Count) As String
                    'For i As Integer = 0 To DataGridView1.Columns.Count - 1
                    '    ar1(i) = DataGridView1.Columns(i).HeaderText
                    'Next
                    'Me.TlSLblWild.Text = myArrN(0)
                    'Me.TlSTxtWild.Tag = myArrF(0)
                End If
                If mCheck = True Then
                    AddOutOfOfficeColumn()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' The application is about to close. Make sure
    ' any changes to the data are safe.
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'If mCheck = True Then
        CloseForm()
        'End If
        'e.Cancel = (Not DataSafe())
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If Not DataGridView1.CurrentCell Is Nothing Then
            If Not DataGridView1.Columns(DataGridView1.CurrentCell.ColumnIndex).DataPropertyName = "Check" Then
                'Me.TlSLblWild.Text = DataGridView1.Columns(DataGridView1.CurrentCell.ColumnIndex).HeaderText
                'Me.TlSTxtWild.Tag = DataGridView1.Columns(DataGridView1.CurrentCell.ColumnIndex).DataPropertyName
            End If
        Else
            'Me.TlSLblWild.Text = "Επιλογή"
        End If
    End Sub

    Private Sub DataGridView1_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged
        DataGridView2.Columns(e.Column.Index).Width = e.Column.Width
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        CurrentDataRowView = BindingSource1.Current()
        Try
            Dim row As DataRow = m_dt.NewRow()
            row.ItemArray = CurrentDataRowView.Row.ItemArray
            m_dt.Rows.Add(row)
        Catch ex As Exception

        End Try
        Me.Close()
    End Sub

    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnCheck.Click
        Me.Validate()
        DataGridView1.CurrentRow.Cells("Check").Value = True 'Επειδή m_ds.Tables(mTableName).GetChanges δεν καταλαβένη την αλλαγή του DataGridView1.CurrentRow
        For RCount = 0 To m_ds.Tables(mTableName).DefaultView.Count - 1
            m_ds.Tables(mTableName).DefaultView(RCount).Item("Check") = True
        Next
        Me.BindingSource1.ResetBindings(True)
        If Not myArrF Is Nothing Then
            RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(mTableName).DefaultView, False)
        End If
        If mCheck = True Then
            AddOutOfOfficeColumn()
        End If
        'DataGridView1.RefreshEdit()
        'DataGridView1.Refresh()
    End Sub

    Private Sub TlSBtnUnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnUnCheck.Click
        Me.Validate()
        DataGridView1.CurrentRow.Cells("Check").Value = False
        For RCount = 0 To m_ds.Tables(mTableName).DefaultView.Count - 1
            m_ds.Tables(mTableName).DefaultView(RCount).Item("Check") = False
        Next
        Me.BindingSource1.ResetBindings(False)
        'DataGridView1.RefreshEdit()
        'DataGridView1.Refresh()
    End Sub
    Private Sub TlSBtnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnFind.Click
        Try
            BindingSource2.EndEdit()
            Dim sqlWhere As String = RsWhere
            Dim sqlOrder As String = RsOrder
            Dim drv As DataRowView
            drv = BindingSource2.Item(0)
            For i As Integer = 0 To drv.DataView.Table.Columns.Count - 1
                If drv(i) Is DBNull.Value Then Continue For
                If [String].IsNullOrEmpty(drv(i)) Then Continue For
                Dim dtxt_Tag As String = CStr(drv.DataView.Table.Columns(i).ColumnName)
                dtxt_Tag = Replace(dtxt_Tag, "P1_", "", , , CompareMethod.Text)
                Dim ValidField As String = drv(i)
                'If Trim(ValidField) = "" Then ValidField = "*"
                If Not ValidField.IndexOf("*").Equals(-1) Then
                    ValidField = ValidField.Replace("*", "%")
                    sqlWhere = Trim(sqlWhere) & " AND " & dtxt_Tag & " LIKE '" & ValidField & "'"
                ElseIf Not ValidField.IndexOf(",").Equals(-1) Then
                    ValidField = "'" & ValidField.Replace(",", "','") & "'"
                    sqlWhere = Trim(sqlWhere) & " AND " & dtxt_Tag & " IN (" & ValidField & ")"
                Else
                    sqlWhere = Trim(sqlWhere) & " AND " & dtxt_Tag & " = '" & ValidField & "'" '30800929
                End If
                'If mPELPRO = 0 Or mPELPRO = 1 Then
                '    sqlWhere = Trim(sqlWhere) & " AND PELPRO = " & PelPro
                'End If
            Next
            If mPELPRO = 0 Or mPELPRO = 1 Then
                sqlWhere = Trim(sqlWhere) & " AND PELPRO = " & PelPro
            End If
            If sqlWhere = "1=1" Then
                sqlWhere = ""
            End If
            sSQL = mSql & IIf(sqlWhere = "", "", " WHERE " & sqlWhere) & IIf(sqlOrder = "", "", " ORDER BY " & sqlOrder)
            df.GmFillDataSet(m_ds, df.GmFillTable(sSQL, mTableName), mTableName)

            'If m_ds.Tables(mTableName).Rows.Count > 0 Then
            If mCheck = True Then
                m_ds.Tables(mTableName).Columns.Add("Check", GetType(Boolean))
            End If
            m_ds.Tables(mTableName).AcceptChanges() 'Για να καταλαβένη τα Check
            m_dt = m_ds.Tables(mTableName).Clone
            BindingSource1.DataSource = m_ds.Tables(mTableName).DefaultView
            m_ds.Tables(mTableName).DefaultView.AllowNew = False
            m_ds.Tables(mTableName).DefaultView.AllowDelete = False
            If Not myArrF Is Nothing Then
                RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(mTableName).DefaultView, False)
                RemoveGridColumns(DataGridView2, Nothing, myArrF, myArrN, m_ds.Tables(mTableName).DefaultView, False)
            End If
            If mCheck = True Then
                AddOutOfOfficeColumn()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddOutOfOfficeColumn()
        Dim column As New DataGridViewCheckBoxColumn()
        With column
            .DataPropertyName = "Check"
            .HeaderText = "Check" 'ColumnName.OutOfOffice.ToString()
            .Name = "Check" 'ColumnName.OutOfOffice.ToString()
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .FlatStyle = FlatStyle.Standard
            .CellTemplate = New DataGridViewCheckBoxCell()
            .CellTemplate.Style.BackColor = Color.Beige
        End With
        Me.DataGridView1.Columns.Insert(0, column)
    End Sub
    Private Sub CloseForm()
        If mCheck = True Then
            Me.Validate()
            m_ds.Tables(mTableName).DefaultView.RowFilter = "Check = True "
            If m_ds.Tables(mTableName).DefaultView.Count > 0 Then
                m_dt = m_ds.Tables(mTableName).GetChanges
            End If
        End If
        m_ArrayList.Clear()
        m_ArrayList.Add(m_dt)
    End Sub
    Private Sub TlSBtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnExit.Click
        Me.Close()
    End Sub
    Private Sub BindingSource1_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles BindingSource1.ListChanged
        Dim dd As String = e.ListChangedType.ToString
    End Sub
    Private Sub DataGridView1_RowHeadersWidthChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.RowHeadersWidthChanged
        Dim s As DataGridView = sender
        DataGridView1.RowHeadersWidth = s.RowHeadersWidth
    End Sub
    Private Sub DataGridView1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            DataGridView2.HorizontalScrollingOffset = DataGridView1.HorizontalScrollingOffset '_Scroll(DataGridView1, e)
        End If
    End Sub
    Private Sub DataGridView2_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        TlSBtnFind.PerformClick()
    End Sub
    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        Dim dt As DataTable = m_ds.Tables(mTableName).Clone
        dt.Rows.Add(dt.NewRow())
        'BindingSource2.Clear()
        BindingSource2.DataSource = dt.DefaultView
        TlSBtnFind.PerformClick()
    End Sub
End Class