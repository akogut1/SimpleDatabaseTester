<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectDialog
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
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.lblProjectNumber = New System.Windows.Forms.Label()
        Me.txtProjectNumber = New System.Windows.Forms.TextBox()
        Me.txtProjectName = New System.Windows.Forms.TextBox()
        Me.chkProjectActive = New System.Windows.Forms.CheckBox()
        Me.dgvMyProject = New System.Windows.Forms.DataGridView()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSaveAndExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmAdminEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCancelEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnDeleteProject = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.chkShowInactiveProjects = New System.Windows.Forms.CheckBox()
        CType(Me.dgvMyProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectName.Location = New System.Drawing.Point(11, 89)
        Me.lblProjectName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(71, 16)
        Me.lblProjectName.TabIndex = 47
        Me.lblProjectName.Text = "Project Name"
        '
        'lblProjectNumber
        '
        Me.lblProjectNumber.AutoSize = True
        Me.lblProjectNumber.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectNumber.Location = New System.Drawing.Point(11, 43)
        Me.lblProjectNumber.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblProjectNumber.Name = "lblProjectNumber"
        Me.lblProjectNumber.Size = New System.Drawing.Size(81, 16)
        Me.lblProjectNumber.TabIndex = 45
        Me.lblProjectNumber.Text = "Project Number"
        '
        'txtProjectNumber
        '
        Me.txtProjectNumber.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectNumber.Location = New System.Drawing.Point(12, 63)
        Me.txtProjectNumber.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProjectNumber.Name = "txtProjectNumber"
        Me.txtProjectNumber.ReadOnly = True
        Me.txtProjectNumber.Size = New System.Drawing.Size(110, 22)
        Me.txtProjectNumber.TabIndex = 48
        '
        'txtProjectName
        '
        Me.txtProjectName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProjectName.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectName.Location = New System.Drawing.Point(12, 109)
        Me.txtProjectName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.ReadOnly = True
        Me.txtProjectName.Size = New System.Drawing.Size(632, 22)
        Me.txtProjectName.TabIndex = 49
        '
        'chkProjectActive
        '
        Me.chkProjectActive.AutoCheck = False
        Me.chkProjectActive.AutoSize = True
        Me.chkProjectActive.Location = New System.Drawing.Point(128, 63)
        Me.chkProjectActive.Name = "chkProjectActive"
        Me.chkProjectActive.Size = New System.Drawing.Size(55, 20)
        Me.chkProjectActive.TabIndex = 50
        Me.chkProjectActive.Text = "Active"
        Me.chkProjectActive.UseVisualStyleBackColor = True
        '
        'dgvMyProject
        '
        Me.dgvMyProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMyProject.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvMyProject.Location = New System.Drawing.Point(0, 169)
        Me.dgvMyProject.Name = "dgvMyProject"
        Me.dgvMyProject.ReadOnly = True
        Me.dgvMyProject.Size = New System.Drawing.Size(656, 193)
        Me.dgvMyProject.TabIndex = 51
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(189, 58)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(69, 29)
        Me.btnOK.TabIndex = 54
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(256, 58)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(69, 29)
        Me.btnCancel.TabIndex = 55
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(47, 24)
        Me.MenuStrip1.TabIndex = 252
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmNew, Me.tsmSave, Me.tsmSaveAndExit, Me.tsmEdit, Me.tsmAdminEdit, Me.tsmCancelEdit, Me.RefreshToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'tsmNew
        '
        Me.tsmNew.Name = "tsmNew"
        Me.tsmNew.Size = New System.Drawing.Size(199, 22)
        Me.tsmNew.Text = "New"
        '
        'tsmSave
        '
        Me.tsmSave.Name = "tsmSave"
        Me.tsmSave.Size = New System.Drawing.Size(199, 22)
        Me.tsmSave.Text = "Save"
        '
        'tsmSaveAndExit
        '
        Me.tsmSaveAndExit.Name = "tsmSaveAndExit"
        Me.tsmSaveAndExit.Size = New System.Drawing.Size(199, 22)
        Me.tsmSaveAndExit.Text = "Save and Exit Edit Mode"
        '
        'tsmEdit
        '
        Me.tsmEdit.Name = "tsmEdit"
        Me.tsmEdit.Size = New System.Drawing.Size(199, 22)
        Me.tsmEdit.Text = "Edit"
        '
        'tsmAdminEdit
        '
        Me.tsmAdminEdit.Name = "tsmAdminEdit"
        Me.tsmAdminEdit.Size = New System.Drawing.Size(199, 22)
        Me.tsmAdminEdit.Text = "Admin Edit"
        '
        'tsmCancelEdit
        '
        Me.tsmCancelEdit.Name = "tsmCancelEdit"
        Me.tsmCancelEdit.Size = New System.Drawing.Size(199, 22)
        Me.tsmCancelEdit.Text = "Cancel"
        '
        'btnDeleteProject
        '
        Me.btnDeleteProject.Location = New System.Drawing.Point(390, 58)
        Me.btnDeleteProject.Name = "btnDeleteProject"
        Me.btnDeleteProject.Size = New System.Drawing.Size(69, 29)
        Me.btnDeleteProject.TabIndex = 253
        Me.btnDeleteProject.Text = "Delete"
        Me.btnDeleteProject.UseVisualStyleBackColor = True
        Me.btnDeleteProject.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(323, 58)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 29)
        Me.btnSave.TabIndex = 254
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.Visible = False
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'chkShowInactiveProjects
        '
        Me.chkShowInactiveProjects.AutoSize = True
        Me.chkShowInactiveProjects.Location = New System.Drawing.Point(513, 12)
        Me.chkShowInactiveProjects.Name = "chkShowInactiveProjects"
        Me.chkShowInactiveProjects.Size = New System.Drawing.Size(133, 20)
        Me.chkShowInactiveProjects.TabIndex = 255
        Me.chkShowInactiveProjects.Text = "Show Inactive Projects"
        Me.chkShowInactiveProjects.UseVisualStyleBackColor = True
        '
        'frmProjectDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 362)
        Me.Controls.Add(Me.chkShowInactiveProjects)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDeleteProject)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.dgvMyProject)
        Me.Controls.Add(Me.chkProjectActive)
        Me.Controls.Add(Me.txtProjectName)
        Me.Controls.Add(Me.txtProjectNumber)
        Me.Controls.Add(Me.lblProjectName)
        Me.Controls.Add(Me.lblProjectNumber)
        Me.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmProjectDialog"
        Me.Text = "Add/ Edit Project to Database"
        CType(Me.dgvMyProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents lblProjectNumber As System.Windows.Forms.Label
    Friend WithEvents txtProjectNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtProjectName As System.Windows.Forms.TextBox
    Friend WithEvents chkProjectActive As System.Windows.Forms.CheckBox
    Friend WithEvents dgvMyProject As System.Windows.Forms.DataGridView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmAdminEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSaveAndExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmCancelEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDeleteProject As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkShowInactiveProjects As System.Windows.Forms.CheckBox
End Class
