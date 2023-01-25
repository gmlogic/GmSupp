
Public Class GmSearchCtrl
    'Private df As GmData

    ' DataTables.
    Private m_dt As DataTable
    'Private MeLabel As String
    Private CurrentDataRowView As DataRowView
    Private DeleteRecord As Boolean

    Private TlSItem As New ArrayList

    Dim TlSTxtWild As ToolStripTextBox
    Dim TlSLblWild As ToolStripLabel

    Public Property m_ds As New DataSet
    Public Property GmCheck As Boolean
    Public Property sSQL As String
    Public Property GmPelPro As Byte
    Public Property RsWhere As String
    Public Property RsOrder As String
    Public Property GmGroupSql As String
    Public Property GmGroupSqlField As String
    Public Property RsTables As String
    Public Property myArrF As String()
    Public Property myArrN As String()
    Public Property RetTBL As DataTable
    Public Property Conn As String

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
            'Me.Close()   'SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub SearchFR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = CType(ChrW(Keys.Enter), Char) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.KeyPreview = True
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

    End Sub
    ' Load the data.
    Private Sub LoadData()
        'df = New GmData(sysDB, CONNECT_STRING) '
        'RsWhere = "Company = " & Company
        'sSQL = "SELECT * FROM PEL1 WHERE " & RsWhere
        'sSQL = SelectPelPro(sSQL, PelPro)
        ''''''''''''''''''''df = New GmData(sysDB, Conn)
        'sqlFilter(RsWhere, RsOrder)
        If Me.GmCheck = True Then
            m_ds.Tables(Me.RsTables).Columns.Add("Check", GetType(Boolean))
        End If
        m_ds.Tables(Me.RsTables).AcceptChanges() 'Για να καταλαβένη τα Check
        m_dt = m_ds.Tables(Me.RsTables).Clone
        BindingSource1.DataSource = m_ds.Tables(Me.RsTables).DefaultView
        m_ds.Tables(Me.RsTables).DefaultView.AllowNew = False
        m_ds.Tables(Me.RsTables).DefaultView.AllowDelete = False
        Dim dt As DataTable = m_ds.Tables(Me.RsTables).Clone
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
            If m_ds.Tables(Me.RsTables).Rows.Count > 0 Then
                TlSBtnCheck.Enabled = False
                TlSBtnUnCheck.Enabled = False
                If Me.GmCheck = True Then
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
                    RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(Me.RsTables).DefaultView, False)
                    RemoveGridColumns(DataGridView2, Nothing, myArrF, myArrN, m_ds.Tables(Me.RsTables).DefaultView, False)
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
                If Me.GmCheck = True Then
                    AddOutOfOfficeColumn()
                End If
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ' The application is about to close. Make sure
    ' any changes to the data are safe.
    'Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    '    'If mCheck = True Then
    '    CloseForm()
    '    'End If
    '    'e.Cancel = (Not DataSafe())
    'End Sub

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
        Dim NoCol = e.Column.Index
        If Me.GmCheck = True And NoCol > 0 Then
            NoCol = e.Column.Index - 1
        End If
        DataGridView2.Columns(NoCol).Width = e.Column.Width
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        CurrentDataRowView = BindingSource1.Current()
        Try
            Dim row As DataRow = m_dt.NewRow()
            row.ItemArray = CurrentDataRowView.Row.ItemArray
            m_dt.Rows.Add(row)
        Catch ex As Exception

        End Try
        'Me.Close()
    End Sub

    Private Sub TlSBtnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnCheck.Click
        Me.Validate()
        DataGridView1.CurrentRow.Cells("Check").Value = True 'Επειδή m_ds.Tables(mTableName).GetChanges δεν καταλαβένη την αλλαγή του DataGridView1.CurrentRow
        For RCount = 0 To m_ds.Tables(Me.RsTables).DefaultView.Count - 1
            m_ds.Tables(Me.RsTables).DefaultView(RCount).Item("Check") = True
        Next
        Me.BindingSource1.ResetBindings(True)
        If Not myArrF Is Nothing Then
            RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(Me.RsTables).DefaultView, False)
        End If
        If Me.GmCheck = True Then
            AddOutOfOfficeColumn()
        End If
        'DataGridView1.RefreshEdit()
        'DataGridView1.Refresh()
    End Sub

    Private Sub TlSBtnUnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnUnCheck.Click
        Me.Validate()
        DataGridView1.CurrentRow.Cells("Check").Value = False
        For RCount = 0 To m_ds.Tables(Me.RsTables).DefaultView.Count - 1
            m_ds.Tables(Me.RsTables).DefaultView(RCount).Item("Check") = False
        Next
        Me.BindingSource1.ResetBindings(False)
        'DataGridView1.RefreshEdit()
        'DataGridView1.Refresh()
    End Sub
    Private Sub TlSBtnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnFind.Click
        Try
            BindingSource2.EndEdit()
            Dim sqlWhere As String = Me.RsWhere
            Dim sqlOrder As String = Me.RsOrder
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
            'If Me.GmPelPro = 0 Or Me.GmPelPro = 1 Then
            '    sqlWhere = Trim(sqlWhere) & " AND PELPRO = " & PelPro
            'End If
            If sqlWhere = "1=1" Then
                sqlWhere = ""
            End If
            Dim nSQL = Me.sSQL & IIf(sqlWhere = "", "", " WHERE " & sqlWhere) & IIf(sqlOrder = "", "", " ORDER BY " & sqlOrder)
            m_ds = GmDataN.GmFillDataSet(Conn, m_ds, GmDataN.GmFillTable(Conn, nSQL, Me.RsTables), Me.RsTables)

            'If m_ds.Tables(mTableName).Rows.Count > 0 Then
            If Me.GmCheck = True Then
                m_ds.Tables(Me.RsTables).Columns.Add("Check", GetType(Boolean))
            End If
            m_ds.Tables(Me.RsTables).AcceptChanges() 'Για να καταλαβένη τα Check
            m_dt = m_ds.Tables(Me.RsTables).Clone
            BindingSource1.DataSource = m_ds.Tables(Me.RsTables).DefaultView
            m_ds.Tables(Me.RsTables).DefaultView.AllowNew = False
            m_ds.Tables(Me.RsTables).DefaultView.AllowDelete = False
            If Not myArrF Is Nothing Then
                RemoveGridColumns(DataGridView1, Nothing, myArrF, myArrN, m_ds.Tables(Me.RsTables).DefaultView, False)
                RemoveGridColumns(DataGridView2, Nothing, myArrF, myArrN, m_ds.Tables(Me.RsTables).DefaultView, False)
            End If
            If Me.GmCheck = True Then
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
            .CellTemplate.Style.BackColor = Drawing.Color.Beige
        End With
        Me.DataGridView1.Columns.Insert(0, column)
    End Sub
    Private Sub CloseForm()
        If Me.GmCheck = True Then
            Me.Validate()
            m_ds.Tables(Me.RsTables).DefaultView.RowFilter = "Check = True "
            If m_ds.Tables(Me.RsTables).DefaultView.Count > 0 Then
                m_dt = m_ds.Tables(Me.RsTables).GetChanges
            End If
        End If
        Me.RetTBL = m_dt
    End Sub
    Private Sub TlSBtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TlSBtnExit.Click
        'Me.Close()
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
        Dim dt As DataTable = m_ds.Tables(Me.RsTables).Clone
        dt.Rows.Add(dt.NewRow())
        'BindingSource2.Clear()
        BindingSource2.DataSource = dt.DefaultView
        TlSBtnFind.PerformClick()
    End Sub

    ' Remove unwanted columns. Note that bad_columns
    ' should list the column indexes in descending order.
    Public Sub RemoveGridColumns(ByRef sender As Object, ByVal bad_columns() As Integer, ByVal myArrF() As String, ByVal myArrN() As String, ByVal DView As DataView, ByVal AllColumns As Boolean)
        If AllColumns = True Then
            Exit Sub
        End If
        Dim dgGen As DataGridView = sender
        Select Case sender.GetType.Name
            Case "DataGridView"
                'dgGen = New DataGridView
            Case "MyDataGridView", "GmDgView"
                'dgGen = New MyDataGridView
                Dim g As Integer = 1
        End Select
        '' Remove the unwanted columns.
        ''For i = 0 To bad_columns.GetUpperBound(0)
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(bad_columns(i))
        ''Next i
        ''For i = 0 To 4
        ''    ' Remove this column.
        ''    table_style.GridColumnStyles.RemoveAt(0)
        ''Next i
        'While dgGen.Columns.Count <> 0
        '    'If dgGen.Columns(0) <> System.Windows.Forms.DataGridViewCheckBoxColumn.Then Then
        '    'End If
        '    Try
        '        dgGen.Columns.Remove(dgGen.Columns(0)) 'dgTim4_CellValidating

        '    Catch ex As Exception

        '    End Try
        '    'If dgGen.Columns.Count > 0 Then
        '    '    Console.WriteLine(dgGen.Columns(0).DataPropertyName & "----" & dgGen.Columns(0).Name)
        '    'End If
        'End While
        Try
            For i As Integer = 0 To dgGen.Columns.Count - 1
                dgGen.Columns.RemoveAt(0) '.clear()
            Next
        Catch ex As Exception

        End Try
        ' Initialize and add a text box column.
        Dim column As DataGridViewColumn
        For i As Integer = 0 To myArrF.Length - 1 '.GetUpperBound(0)
            column = New DataGridViewTextBoxColumn()
            With column
                .DataPropertyName = myArrF(i)
                .Name = myArrN(i)
                'Console.WriteLine(myArrF(i) & " --- " & myArrN(i))
                Try
                    'DView.Table.Columns(txtWild.Tag).DataType.Name()
                    If DView.Table.Columns(myArrF(i)).DataType.Name = "Double" Then
                        .DefaultCellStyle.Format = "###0.00"
                    End If
                    If DView.Table.Columns(myArrF(i)).DataType.Name <> "String" Then
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Catch ex As Exception

                End Try
            End With
            dgGen.Columns.Add(column)
        Next
        With dgGen
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken
            With (dgGen.ColumnHeadersDefaultCellStyle)
                .BackColor = Drawing.Color.Orange '.Navy
                .ForeColor = Drawing.Color.Blue '.White
                '.Font = New Font(dgGen.Font, FontStyle.Bold)
            End With
        End With
    End Sub
End Class

