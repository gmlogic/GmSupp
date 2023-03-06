Imports System.Reflection
Imports GmSupp.Revera

Public Class WHouseBalFR
#Region "01-Declare Variables"
    Private myArrF As String()
    Private myArrN As String()
#End Region
#Region "02-Declare Propertys"
    'Public Property bindingSource As BindingSource
    Public Property cccTrdDeps As List(Of cccTrdDep)
    Public Property FrmCancel As Boolean
#End Region
#Region "03-Load Form"
    'Private Sub WHouseBalFR_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    '    Me.MasterDataGridView.DataSource = bindingSource.DataSource
    '    MasterDataGridView_Styling()
    'End Sub
    Private Sub MyBase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        'Me.BindingNavigatorSaveItem.Enabled = False
        'If Not bindingSource.Count = 0 Then
        '    Me.BindingNavigatorSaveItem.Enabled = True
        'End If
    End Sub
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()   'SendKeys.Send("{TAB}")
        End If
        If e.KeyCode = Keys.F3 Then
            'Me.cmdSelect.PerformClick()
        End If
        If e.Alt And e.KeyCode.ToString = "F" Then
            ' When the user presses both the 'ALT' key and 'F' key,
            ' KeyPreview is set to False, and a message appears.
            ' This message is only displayed when KeyPreview is set to True.
            Me.KeyPreview = False
            MsgBox("KeyPreview Is True, And this Is from the FORM.")
        End If
    End Sub
    Private Sub MyBase_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'e.Cancel = (Not DataSafe())
    End Sub
#End Region
    'Private Sub BindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
    '    Me.txtBoxMtrlCode.Text = ""
    '    Me.bindingSource.Clear()
    '    Me.Close()
    'End Sub

    'Private Sub MasterDataGridView_Styling()
    '    Try

    '        Me.MasterDataGridView.AutoGenerateColumns = True
    '        'Me.MasterDataGridView.AutoResizeColumns()
    '        'Me.MasterDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
    '        'Me.MasterDataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect
    '        'ΚΩΔΙΚΟΣ SFT 1		ΠΟΣΟΤΗΤΑ	MM	ΠΕΡΙΓΡΑΦΗ 			

    '        myArrF = ("CODE,QTY1,MTRUNITC,NAME").Split(",")
    '        myArrN = ("Κωδικός,Ποσ.1,Μ.Μ,Περιγραφή").Split(",")

    '        'Add Bound Columns
    '        Dim bad_item_columns() As Integer = {1, 2, 3, 4}
    '        RemoveGridColumnsByCollection(MasterDataGridView, bad_item_columns, myArrF, myArrN, False) 'CheckBoxDetail.Checked)

    '        'AddOutOfOfficeColumn(Me.MasterDataGridView)


    '        For i As Integer = 0 To MasterDataGridView.Columns.Count - 1
    '            Debug.Print(MasterDataGridView.Columns(i).DataPropertyName & vbTab & MasterDataGridView.Columns(i).Name)
    '        Next

    '        'Me.MasterDataGridView.AutoResizeColumns()


    '        If Not IsNothing(MasterDataGridView.Columns("Παρατηρήσεις")) Then
    '            MasterDataGridView.Columns("Παρατηρήσεις").Width = 460
    '            MasterDataGridView.Columns("Παρατηρήσεις").DefaultCellStyle.WrapMode = DataGridViewTriState.True
    '        End If

    '        Me.MasterDataGridView.AutoResizeRows()

    '        Me.MasterDataGridView.AutoResizeColumns()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub MasterDataGridView_Sorted(sender As Object, e As EventArgs)
    '    MasterDataGridView_Styling()
    'End Sub

    Private Sub ddlTrdr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTrdr.SelectedIndexChanged
        Dim s As ComboBox = sender
        If Not s.SelectedValue = 0 Then
            Dim trdr As Integer? = s.SelectedValue
            Me.ddlcccTrdDep.DropDownStyle = ComboBoxStyle.DropDownList
            Me.ddlcccTrdDep.DataSource = cccTrdDeps.Where(Function(f) f.cccTrdDep = 0 Or If(f.trdr, 0) = trdr).Distinct(Function(f) f.Code).ToList
        End If
    End Sub

    'Private Sub ddlApplicant_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlApplicant.SelectedIndexChanged
    '    Dim s As ComboBox = sender
    '    If Not s.SelectedValue = 0 Then
    '        Dim Applicants As Integer? = CInt(s.SelectedValue)
    '        'Me.txtBoxRequestNo.Text = If(CType(s.SelectedItem, Revera.UFTBL01).cCCRequestNo, 0)
    '        'Me.ddlcccTrdDep.DataSource = cccTrdDeps.Where(Function(f) f.cccTrdDep = 0 Or If(f.trdr, 0) = Applicants).ToList
    '        Dim emptyccCChief() As Revera.UFTBL01
    '        emptyccCChief = {New Revera.UFTBL01 With {.ccCChief = "<Επιλέγξτε>", .UFTBL01 = 0}}
    '        Dim ccCChiefs = (emptyccCChief.ToList.Union(CType(Me.ddlApplicant.DataSource, List(Of UFTBL01)).Where(Function(f) f.UFTBL01 = Applicants And Not f.ccCChief Is Nothing).OrderBy(Function(f) f.ccCChief).ToList)).ToList
    '        Me.ddlΗighers.DataSource = ccCChiefs

    '        Dim cCCManagerL As New List(Of UFTBL01)
    '        Dim ccCChief = CType(s.SelectedItem, Revera.UFTBL01)
    '        Dim ChiefNew As New UFTBL01
    '        For Each pi As PropertyInfo In GetType(UFTBL01).GetProperties()
    '            Try
    '                Dim val = GetType(UFTBL01).GetProperty(pi.Name).GetValue(ccCChief, Nothing)
    '                If Not IsNothing(val) Then
    '                    GetType(UFTBL01).GetProperty(pi.Name).SetValue(ChiefNew, val, Nothing)
    '                End If
    '            Catch ex As Exception

    '            End Try
    '        Next
    '        If Not IsNothing(ccCChief.cCCManager) Then
    '            cCCManagerL.Add(ChiefNew)
    '        End If
    '        If Not IsNothing(ccCChief.cCCManager1) Then
    '            ChiefNew = New UFTBL01
    '            For Each pi As PropertyInfo In GetType(UFTBL01).GetProperties()
    '                Try
    '                    Dim val = GetType(UFTBL01).GetProperty(pi.Name).GetValue(ccCChief, Nothing)
    '                    If Not IsNothing(val) Then
    '                        GetType(UFTBL01).GetProperty(pi.Name).SetValue(ChiefNew, val, Nothing)
    '                    End If
    '                Catch ex As Exception

    '                End Try
    '            Next
    '            ChiefNew.cCCManager = ccCChief.cCCManager1
    '            cCCManagerL.Add(ChiefNew)
    '        End If
    '        If Not IsNothing(ccCChief.cCCManager2) Then
    '            ChiefNew = New UFTBL01
    '            For Each pi As PropertyInfo In GetType(UFTBL01).GetProperties()
    '                Try
    '                    Dim val = GetType(UFTBL01).GetProperty(pi.Name).GetValue(ccCChief, Nothing)
    '                    If Not IsNothing(val) Then
    '                        GetType(UFTBL01).GetProperty(pi.Name).SetValue(ChiefNew, val, Nothing)
    '                    End If
    '                Catch ex As Exception

    '                End Try
    '            Next
    '            ChiefNew.cCCManager = ccCChief.cCCManager2
    '            cCCManagerL.Add(ChiefNew)
    '        End If

    '        Dim emptycCCManager() As Revera.UFTBL01
    '        emptycCCManager = {New Revera.UFTBL01 With {.cCCManager = "<Επιλέγξτε>", .UFTBL01 = 0}}
    '        Dim cCCManagers = (emptycCCManager.ToList.Union(cCCManagerL.OrderBy(Function(f) f.cCCManager).ToList)).ToList
    '        Me.ddlcCCManager.DataSource = cCCManagers
    '    End If
    'End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        FrmCancel = True
        Me.Close()
    End Sub

End Class