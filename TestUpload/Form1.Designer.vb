<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnUpload = New System.Windows.Forms.Button()
        Me.BtnUploadAsync = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnUpload
        '
        Me.BtnUpload.Location = New System.Drawing.Point(26, 40)
        Me.BtnUpload.Name = "BtnUpload"
        Me.BtnUpload.Size = New System.Drawing.Size(75, 23)
        Me.BtnUpload.TabIndex = 300
        Me.BtnUpload.Text = "UpLoad"
        Me.BtnUpload.UseVisualStyleBackColor = True
        '
        'BtnUploadAsync
        '
        Me.BtnUploadAsync.Location = New System.Drawing.Point(119, 40)
        Me.BtnUploadAsync.Name = "BtnUploadAsync"
        Me.BtnUploadAsync.Size = New System.Drawing.Size(75, 23)
        Me.BtnUploadAsync.TabIndex = 301
        Me.BtnUploadAsync.Text = "UploadAsync"
        Me.BtnUploadAsync.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.BtnUploadAsync)
        Me.Controls.Add(Me.BtnUpload)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnUpload As Button
    Friend WithEvents BtnUploadAsync As Button
End Class
