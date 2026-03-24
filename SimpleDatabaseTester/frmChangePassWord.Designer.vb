<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassWord
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangePassWord))
        Me.txtOldPassWord = New System.Windows.Forms.TextBox()
        Me.txtNewPassWord = New System.Windows.Forms.TextBox()
        Me.txtRepeatNewPassWord = New System.Windows.Forms.TextBox()
        Me.lblOldPassword = New System.Windows.Forms.Label()
        Me.lblNewPassword = New System.Windows.Forms.Label()
        Me.lblRepeatNewPassWord = New System.Windows.Forms.Label()
        Me.btnChangePasswordOK = New System.Windows.Forms.Button()
        Me.btnChangePasswordCancel = New System.Windows.Forms.Button()
        Me.btnCheckPassword = New System.Windows.Forms.Button()
        Me.pbPasswordStrength = New System.Windows.Forms.ProgressBar()
        Me.lblWeak = New System.Windows.Forms.Label()
        Me.Strong = New System.Windows.Forms.Label()
        Me.txtConsecutiveCharsToCheckFor = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'txtOldPassWord
        '
        Me.txtOldPassWord.Location = New System.Drawing.Point(91, 15)
        Me.txtOldPassWord.MaxLength = 20
        Me.txtOldPassWord.Name = "txtOldPassWord"
        Me.txtOldPassWord.Size = New System.Drawing.Size(176, 20)
        Me.txtOldPassWord.TabIndex = 0
        Me.txtOldPassWord.UseSystemPasswordChar = True
        '
        'txtNewPassWord
        '
        Me.txtNewPassWord.Location = New System.Drawing.Point(91, 47)
        Me.txtNewPassWord.MaxLength = 20
        Me.txtNewPassWord.Name = "txtNewPassWord"
        Me.txtNewPassWord.Size = New System.Drawing.Size(176, 20)
        Me.txtNewPassWord.TabIndex = 1
        Me.txtNewPassWord.UseSystemPasswordChar = True
        '
        'txtRepeatNewPassWord
        '
        Me.txtRepeatNewPassWord.Location = New System.Drawing.Point(91, 81)
        Me.txtRepeatNewPassWord.MaxLength = 20
        Me.txtRepeatNewPassWord.Name = "txtRepeatNewPassWord"
        Me.txtRepeatNewPassWord.Size = New System.Drawing.Size(176, 20)
        Me.txtRepeatNewPassWord.TabIndex = 2
        Me.txtRepeatNewPassWord.UseSystemPasswordChar = True
        '
        'lblOldPassword
        '
        Me.lblOldPassword.AutoSize = True
        Me.lblOldPassword.Location = New System.Drawing.Point(13, 18)
        Me.lblOldPassword.Name = "lblOldPassword"
        Me.lblOldPassword.Size = New System.Drawing.Size(75, 13)
        Me.lblOldPassword.TabIndex = 3
        Me.lblOldPassword.Text = "Old Password:"
        '
        'lblNewPassword
        '
        Me.lblNewPassword.AutoSize = True
        Me.lblNewPassword.Location = New System.Drawing.Point(8, 49)
        Me.lblNewPassword.Name = "lblNewPassword"
        Me.lblNewPassword.Size = New System.Drawing.Size(81, 13)
        Me.lblNewPassword.TabIndex = 4
        Me.lblNewPassword.Text = "New Password:"
        '
        'lblRepeatNewPassWord
        '
        Me.lblRepeatNewPassWord.AutoSize = True
        Me.lblRepeatNewPassWord.Location = New System.Drawing.Point(8, 71)
        Me.lblRepeatNewPassWord.Name = "lblRepeatNewPassWord"
        Me.lblRepeatNewPassWord.Size = New System.Drawing.Size(81, 26)
        Me.lblRepeatNewPassWord.TabIndex = 5
        Me.lblRepeatNewPassWord.Text = "Repeat " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "New Password:"
        '
        'btnChangePasswordOK
        '
        Me.btnChangePasswordOK.Location = New System.Drawing.Point(32, 170)
        Me.btnChangePasswordOK.Name = "btnChangePasswordOK"
        Me.btnChangePasswordOK.Size = New System.Drawing.Size(108, 45)
        Me.btnChangePasswordOK.TabIndex = 6
        Me.btnChangePasswordOK.Text = "OK"
        Me.btnChangePasswordOK.UseVisualStyleBackColor = True
        '
        'btnChangePasswordCancel
        '
        Me.btnChangePasswordCancel.Location = New System.Drawing.Point(146, 170)
        Me.btnChangePasswordCancel.Name = "btnChangePasswordCancel"
        Me.btnChangePasswordCancel.Size = New System.Drawing.Size(108, 45)
        Me.btnChangePasswordCancel.TabIndex = 7
        Me.btnChangePasswordCancel.Text = "Cancel"
        Me.btnChangePasswordCancel.UseVisualStyleBackColor = True
        '
        'btnCheckPassword
        '
        Me.btnCheckPassword.Location = New System.Drawing.Point(337, 15)
        Me.btnCheckPassword.Name = "btnCheckPassword"
        Me.btnCheckPassword.Size = New System.Drawing.Size(108, 45)
        Me.btnCheckPassword.TabIndex = 8
        Me.btnCheckPassword.Text = "Check Password"
        Me.btnCheckPassword.UseVisualStyleBackColor = True
        '
        'pbPasswordStrength
        '
        Me.pbPasswordStrength.ForeColor = System.Drawing.Color.Lime
        Me.pbPasswordStrength.Location = New System.Drawing.Point(12, 120)
        Me.pbPasswordStrength.Maximum = 6
        Me.pbPasswordStrength.Name = "pbPasswordStrength"
        Me.pbPasswordStrength.Size = New System.Drawing.Size(255, 13)
        Me.pbPasswordStrength.TabIndex = 9
        '
        'lblWeak
        '
        Me.lblWeak.AutoSize = True
        Me.lblWeak.Location = New System.Drawing.Point(8, 136)
        Me.lblWeak.Name = "lblWeak"
        Me.lblWeak.Size = New System.Drawing.Size(36, 13)
        Me.lblWeak.TabIndex = 10
        Me.lblWeak.Text = "Weak"
        '
        'Strong
        '
        Me.Strong.AutoSize = True
        Me.Strong.Location = New System.Drawing.Point(237, 136)
        Me.Strong.Name = "Strong"
        Me.Strong.Size = New System.Drawing.Size(38, 13)
        Me.Strong.TabIndex = 11
        Me.Strong.Text = "Strong"
        '
        'txtConsecutiveCharsToCheckFor
        '
        Me.txtConsecutiveCharsToCheckFor.Location = New System.Drawing.Point(430, 149)
        Me.txtConsecutiveCharsToCheckFor.MaxLength = 20
        Me.txtConsecutiveCharsToCheckFor.Name = "txtConsecutiveCharsToCheckFor"
        Me.txtConsecutiveCharsToCheckFor.Size = New System.Drawing.Size(44, 20)
        Me.txtConsecutiveCharsToCheckFor.TabIndex = 12
        Me.txtConsecutiveCharsToCheckFor.Text = "3"
        Me.txtConsecutiveCharsToCheckFor.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(83, 145)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(105, 17)
        Me.CheckBox1.TabIndex = 13
        Me.CheckBox1.Text = "Password Visible"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'frmChangePassWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 227)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtConsecutiveCharsToCheckFor)
        Me.Controls.Add(Me.Strong)
        Me.Controls.Add(Me.lblWeak)
        Me.Controls.Add(Me.pbPasswordStrength)
        Me.Controls.Add(Me.btnCheckPassword)
        Me.Controls.Add(Me.btnChangePasswordCancel)
        Me.Controls.Add(Me.btnChangePasswordOK)
        Me.Controls.Add(Me.lblRepeatNewPassWord)
        Me.Controls.Add(Me.lblNewPassword)
        Me.Controls.Add(Me.lblOldPassword)
        Me.Controls.Add(Me.txtRepeatNewPassWord)
        Me.Controls.Add(Me.txtNewPassWord)
        Me.Controls.Add(Me.txtOldPassWord)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChangePassWord"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtOldPassWord As System.Windows.Forms.TextBox
    Friend WithEvents txtNewPassWord As System.Windows.Forms.TextBox
    Friend WithEvents txtRepeatNewPassWord As System.Windows.Forms.TextBox
    Friend WithEvents lblOldPassword As System.Windows.Forms.Label
    Friend WithEvents lblNewPassword As System.Windows.Forms.Label
    Friend WithEvents lblRepeatNewPassWord As System.Windows.Forms.Label
    Friend WithEvents btnChangePasswordOK As System.Windows.Forms.Button
    Friend WithEvents btnChangePasswordCancel As System.Windows.Forms.Button
    Friend WithEvents btnCheckPassword As System.Windows.Forms.Button
    Friend WithEvents pbPasswordStrength As System.Windows.Forms.ProgressBar
    Friend WithEvents lblWeak As System.Windows.Forms.Label
    Friend WithEvents Strong As System.Windows.Forms.Label
    Friend WithEvents txtConsecutiveCharsToCheckFor As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
