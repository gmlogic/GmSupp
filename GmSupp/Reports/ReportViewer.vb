Public Class ReportViewer
    Private CurrentDataRowView As DataRowView
    Private df As GmData
    Private m_DataSet As DataSet
    Private m_dtPEL4 As DataTable
    Private mAction As Action
    Private MeLabel As String
    Private myArrF() As String
    Private myArrN() As String
    Dim ReturnFields As New ArrayList

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="drv"></param>
    ''' <param name="m_dsc"></param>
    ''' <param name="Action"></param>
    ''' <remarks></remarks>
    Sub Me_Load(ByVal drv As DataRowView, ByVal m_dsc As DataSet, ByVal Action As Short) 'As Boolean
        mAction = Action
        CurrentDataRowView = drv
        m_DataSet = m_dsc
        'Me.ShowDialog()
    End Sub
    Private Sub ReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GmParkDataSet.ViewPEL1_PEL4' table. You can move, or remove it, as needed.
        'Me.ViewPEL1_PEL4TableAdapter.Fill(Me.GmParkDataSet.ViewPEL1_PEL4)
        'ReportViewer1
        Me.ReportViewer1.RefreshReport()
        Exit Sub
        'Dim BindingSource1 As New System.Windows.Forms.BindingSource
        'BindingSource1.DataSource = m_DataSet.Tables("RptTable").DefaultView
        'Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        'Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        'ReportDataSource1.Name = "alusetDataSet_VMSTATCOLOR"
        'ReportDataSource1.Value = BindingSource1
        'Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        'Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "AluSet.Report1.rdlc"
        ''Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        ''Me.ReportViewer1.Name = "ReportViewer1"
        ''Me.ReportViewer1.Size = New System.Drawing.Size(638, 436)
        ''Me.ReportViewer1.TabIndex = 0
        ''Me.ReportViewer1.ZoomPercent = 75

        'Me.ReportViewer1.RefreshReport()
    End Sub
  


End Class