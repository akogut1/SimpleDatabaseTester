<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestWrapping
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
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.btnWrapTest = New System.Windows.Forms.Button()
        Me.txtWrapLength = New System.Windows.Forms.TextBox()
        Me.txtLeftPad = New System.Windows.Forms.TextBox()
        Me.txtRightPad = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RightPad = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.btnClearOutput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtInput
        '
        Me.txtInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInput.Location = New System.Drawing.Point(1, 152)
        Me.txtInput.Multiline = True
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(666, 115)
        Me.txtInput.TabIndex = 0
        Me.txtInput.Text = "This is the test text that will be used to test the wrapping capability of the fu" & _
    "nction.  123456789ABCDEFGabcdefg"
        '
        'btnWrapTest
        '
        Me.btnWrapTest.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnWrapTest.Location = New System.Drawing.Point(466, 60)
        Me.btnWrapTest.Name = "btnWrapTest"
        Me.btnWrapTest.Size = New System.Drawing.Size(73, 35)
        Me.btnWrapTest.TabIndex = 1
        Me.btnWrapTest.Text = "Wrap"
        Me.btnWrapTest.UseVisualStyleBackColor = True
        '
        'txtWrapLength
        '
        Me.txtWrapLength.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtWrapLength.Location = New System.Drawing.Point(305, 64)
        Me.txtWrapLength.Name = "txtWrapLength"
        Me.txtWrapLength.Size = New System.Drawing.Size(96, 20)
        Me.txtWrapLength.TabIndex = 2
        Me.txtWrapLength.Text = "20"
        '
        'txtLeftPad
        '
        Me.txtLeftPad.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLeftPad.Location = New System.Drawing.Point(305, 90)
        Me.txtLeftPad.Name = "txtLeftPad"
        Me.txtLeftPad.Size = New System.Drawing.Size(96, 20)
        Me.txtLeftPad.TabIndex = 3
        Me.txtLeftPad.Text = "2"
        '
        'txtRightPad
        '
        Me.txtRightPad.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtRightPad.Location = New System.Drawing.Point(305, 116)
        Me.txtRightPad.Name = "txtRightPad"
        Me.txtRightPad.Size = New System.Drawing.Size(96, 20)
        Me.txtRightPad.TabIndex = 4
        Me.txtRightPad.Text = "2"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(193, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Length"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(193, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Left Pad"
        '
        'RightPad
        '
        Me.RightPad.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.RightPad.AutoSize = True
        Me.RightPad.Location = New System.Drawing.Point(193, 119)
        Me.RightPad.Name = "RightPad"
        Me.RightPad.Size = New System.Drawing.Size(54, 13)
        Me.RightPad.TabIndex = 7
        Me.RightPad.Text = "Right Pad"
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.Location = New System.Drawing.Point(1, 282)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOutput.Size = New System.Drawing.Size(666, 115)
        Me.txtOutput.TabIndex = 8
        Me.txtOutput.WordWrap = False
        '
        'btnClearOutput
        '
        Me.btnClearOutput.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnClearOutput.Location = New System.Drawing.Point(466, 101)
        Me.btnClearOutput.Name = "btnClearOutput"
        Me.btnClearOutput.Size = New System.Drawing.Size(73, 35)
        Me.btnClearOutput.TabIndex = 9
        Me.btnClearOutput.Text = "Clear Output"
        Me.btnClearOutput.UseVisualStyleBackColor = True
        '
        'frmTestWrapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 517)
        Me.Controls.Add(Me.btnClearOutput)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.RightPad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtRightPad)
        Me.Controls.Add(Me.txtLeftPad)
        Me.Controls.Add(Me.txtWrapLength)
        Me.Controls.Add(Me.btnWrapTest)
        Me.Controls.Add(Me.txtInput)
        Me.Name = "frmTestWrapping"
        Me.Text = "frmTestWrapping"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents btnWrapTest As System.Windows.Forms.Button
    Friend WithEvents txtWrapLength As System.Windows.Forms.TextBox
    Friend WithEvents txtLeftPad As System.Windows.Forms.TextBox
    Friend WithEvents txtRightPad As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RightPad As System.Windows.Forms.Label
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents btnClearOutput As System.Windows.Forms.Button
End Class
