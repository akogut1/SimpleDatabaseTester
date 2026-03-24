<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectTestMatrixID
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
        Me.cbSelectTestMatrix = New xboXComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbMatrixName = New xboXComboBox()
        Me.lblMatrixName = New System.Windows.Forms.Label()
        Me.lblProjectNumber = New System.Windows.Forms.Label()
        Me.cbProjectNumber = New xboXComboBox()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.cbProjectName = New xboXComboBox()
        Me.SuspendLayout()
        '
        'cbSelectTestMatrix
        '
        Me.cbSelectTestMatrix.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSelectTestMatrix.Location = New System.Drawing.Point(8, 36)
        Me.cbSelectTestMatrix.Margin = New System.Windows.Forms.Padding(1)
        Me.cbSelectTestMatrix.Name = "cbSelectTestMatrix"
        Me.cbSelectTestMatrix.Size = New System.Drawing.Size(76, 24)
        Me.cbSelectTestMatrix.TabIndex = 36
        Me.cbSelectTestMatrix.Tag = "Tested By"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Test Matrix ID"
        '
        'cbMatrixName
        '
        Me.cbMatrixName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMatrixName.Location = New System.Drawing.Point(8, 84)
        Me.cbMatrixName.Margin = New System.Windows.Forms.Padding(1)
        Me.cbMatrixName.Name = "cbMatrixName"
        Me.cbMatrixName.Size = New System.Drawing.Size(652, 24)
        Me.cbMatrixName.TabIndex = 38
        Me.cbMatrixName.Tag = ""
        '
        'lblMatrixName
        '
        Me.lblMatrixName.AutoSize = True
        Me.lblMatrixName.Location = New System.Drawing.Point(11, 70)
        Me.lblMatrixName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblMatrixName.Name = "lblMatrixName"
        Me.lblMatrixName.Size = New System.Drawing.Size(66, 13)
        Me.lblMatrixName.TabIndex = 39
        Me.lblMatrixName.Text = "Matrix Name"
        '
        'lblProjectNumber
        '
        Me.lblProjectNumber.AutoSize = True
        Me.lblProjectNumber.Location = New System.Drawing.Point(10, 123)
        Me.lblProjectNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProjectNumber.Name = "lblProjectNumber"
        Me.lblProjectNumber.Size = New System.Drawing.Size(80, 13)
        Me.lblProjectNumber.TabIndex = 41
        Me.lblProjectNumber.Text = "Project Number"
        '
        'cbProjectNumber
        '
        Me.cbProjectNumber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectNumber.Location = New System.Drawing.Point(8, 140)
        Me.cbProjectNumber.Margin = New System.Windows.Forms.Padding(1)
        Me.cbProjectNumber.Name = "cbProjectNumber"
        Me.cbProjectNumber.Size = New System.Drawing.Size(113, 24)
        Me.cbProjectNumber.TabIndex = 40
        Me.cbProjectNumber.Tag = "Tested By"
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Location = New System.Drawing.Point(10, 175)
        Me.lblProjectName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(68, 13)
        Me.lblProjectName.TabIndex = 43
        Me.lblProjectName.Text = "ProjectName"
        '
        'cbProjectName
        '
        Me.cbProjectName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectName.Location = New System.Drawing.Point(8, 193)
        Me.cbProjectName.Margin = New System.Windows.Forms.Padding(1)
        Me.cbProjectName.Name = "cbProjectName"
        Me.cbProjectName.Size = New System.Drawing.Size(652, 24)
        Me.cbProjectName.TabIndex = 42
        Me.cbProjectName.Tag = "Tested By"
        '
        'frmSelectTestMatrixID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 393)
        Me.Controls.Add(Me.lblProjectName)
        Me.Controls.Add(Me.cbProjectName)
        Me.Controls.Add(Me.lblProjectNumber)
        Me.Controls.Add(Me.cbProjectNumber)
        Me.Controls.Add(Me.lblMatrixName)
        Me.Controls.Add(Me.cbMatrixName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbSelectTestMatrix)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmSelectTestMatrixID"
        Me.Text = "Select Test Matrix"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbSelectTestMatrix As xboXComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents cbMatrixName As xboXComboBox
    Friend WithEvents lblMatrixName As System.Windows.Forms.Label
    Friend WithEvents lblProjectNumber As System.Windows.Forms.Label
    Friend WithEvents cbProjectNumber As xboXComboBox
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents cbProjectName As xboXComboBox
End Class
