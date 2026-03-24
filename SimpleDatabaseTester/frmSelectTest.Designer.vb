<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectTest
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.xcbSelectTest = New xboXComboBox()
        Me.xcbTestType = New xboXComboBox()
        Me.lblTestType = New System.Windows.Forms.Label()
        Me.lbl = New System.Windows.Forms.Label()
        Me.chkFilterTests = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(313, 24)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(98, 38)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(412, 24)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 38)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'xcbSelectTest
        '
        Me.xcbSelectTest.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xcbSelectTest.Location = New System.Drawing.Point(36, 72)
        Me.xcbSelectTest.Name = "xcbSelectTest"
        Me.xcbSelectTest.Size = New System.Drawing.Size(474, 24)
        Me.xcbSelectTest.TabIndex = 3
        '
        'xcbTestType
        '
        Me.xcbTestType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xcbTestType.Location = New System.Drawing.Point(36, 24)
        Me.xcbTestType.Name = "xcbTestType"
        Me.xcbTestType.Size = New System.Drawing.Size(139, 24)
        Me.xcbTestType.TabIndex = 4
        Me.xcbTestType.Text = "All"
        '
        'lblTestType
        '
        Me.lblTestType.AutoSize = True
        Me.lblTestType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestType.Location = New System.Drawing.Point(33, 5)
        Me.lblTestType.Name = "lblTestType"
        Me.lblTestType.Size = New System.Drawing.Size(67, 16)
        Me.lblTestType.TabIndex = 215
        Me.lblTestType.Text = "Test Type"
        Me.lblTestType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.Location = New System.Drawing.Point(33, 53)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(33, 16)
        Me.lbl.TabIndex = 216
        Me.lbl.Text = "Test"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFilterTests
        '
        Me.chkFilterTests.AutoSize = True
        Me.chkFilterTests.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterTests.Location = New System.Drawing.Point(181, 24)
        Me.chkFilterTests.Name = "chkFilterTests"
        Me.chkFilterTests.Size = New System.Drawing.Size(126, 19)
        Me.chkFilterTests.TabIndex = 217
        Me.chkFilterTests.Text = "Filter Test by Type"
        Me.chkFilterTests.UseVisualStyleBackColor = True
        '
        'frmSelectTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 104)
        Me.Controls.Add(Me.chkFilterTests)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.lblTestType)
        Me.Controls.Add(Me.xcbTestType)
        Me.Controls.Add(Me.xcbSelectTest)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSelectTest"
        Me.Text = "Select Test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents xcbSelectTest As xboXComboBox
    Friend WithEvents xcbTestType As xboXComboBox
    Friend WithEvents lblTestType As System.Windows.Forms.Label
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents chkFilterTests As System.Windows.Forms.CheckBox
End Class
