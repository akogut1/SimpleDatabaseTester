<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomizeDropDowns
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
        Me.cbCustDropDowns = New System.Windows.Forms.ComboBox()
        Me.lbHidden = New System.Windows.Forms.ListBox()
        Me.lbVisible = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'cbCustDropDowns
        '
        Me.cbCustDropDowns.FormattingEnabled = True
        Me.cbCustDropDowns.Location = New System.Drawing.Point(12, 24)
        Me.cbCustDropDowns.Name = "cbCustDropDowns"
        Me.cbCustDropDowns.Size = New System.Drawing.Size(129, 21)
        Me.cbCustDropDowns.TabIndex = 0
        '
        'lblHidden
        '
        Me.lbHidden.FormattingEnabled = True
        Me.lbHidden.Location = New System.Drawing.Point(12, 62)
        Me.lbHidden.Name = "lblHidden"
        Me.lbHidden.Size = New System.Drawing.Size(152, 355)
        Me.lbHidden.TabIndex = 1
        '
        'lbVisible
        '
        Me.lbVisible.FormattingEnabled = True
        Me.lbVisible.Location = New System.Drawing.Point(211, 62)
        Me.lbVisible.Name = "lbVisible"
        Me.lbVisible.Size = New System.Drawing.Size(152, 355)
        Me.lbVisible.TabIndex = 2
        '
        'frmCustomizeDropDowns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 485)
        Me.Controls.Add(Me.lbVisible)
        Me.Controls.Add(Me.lbHidden)
        Me.Controls.Add(Me.cbCustDropDowns)
        Me.Name = "frmCustomizeDropDowns"
        Me.Text = "Customize Drop Downs"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbCustDropDowns As System.Windows.Forms.ComboBox
    Friend WithEvents lbHidden As System.Windows.Forms.ListBox
    Friend WithEvents lbVisible As System.Windows.Forms.ListBox
End Class
