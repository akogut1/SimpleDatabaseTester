<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectTestGroupMembers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectTestGroupMembers))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvAllTestGroupMembers = New System.Windows.Forms.DataGridView()
        Me.gbSelectTestGroupMembers = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ChkShowPastRevision = New System.Windows.Forms.CheckBox()
        Me.chkShowObsolete = New System.Windows.Forms.CheckBox()
        Me.btnCancelChanges = New System.Windows.Forms.Button()
        Me.btnApplyChanges = New System.Windows.Forms.Button()
        Me.btnRemoveSelected = New System.Windows.Forms.Button()
        Me.btnAddSelected = New System.Windows.Forms.Button()
        Me.dgvTestGroupMembersSelected = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgvAllTestGroupMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSelectTestGroupMembers.SuspendLayout()
        CType(Me.dgvTestGroupMembersSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgvAllTestGroupMembers, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.gbSelectTestGroupMembers, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgvTestGroupMembersSelected, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.53247!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.46753!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1141, 445)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'dgvAllTestGroupMembers
        '
        Me.dgvAllTestGroupMembers.AllowUserToOrderColumns = True
        Me.dgvAllTestGroupMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAllTestGroupMembers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAllTestGroupMembers.Location = New System.Drawing.Point(3, 63)
        Me.dgvAllTestGroupMembers.Name = "dgvAllTestGroupMembers"
        Me.dgvAllTestGroupMembers.Size = New System.Drawing.Size(1135, 177)
        Me.dgvAllTestGroupMembers.TabIndex = 0
        '
        'gbSelectTestGroupMembers
        '
        Me.gbSelectTestGroupMembers.Controls.Add(Me.btnRefresh)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.ChkShowPastRevision)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.chkShowObsolete)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.btnCancelChanges)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.btnApplyChanges)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.btnRemoveSelected)
        Me.gbSelectTestGroupMembers.Controls.Add(Me.btnAddSelected)
        Me.gbSelectTestGroupMembers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSelectTestGroupMembers.Location = New System.Drawing.Point(3, 3)
        Me.gbSelectTestGroupMembers.Name = "gbSelectTestGroupMembers"
        Me.gbSelectTestGroupMembers.Size = New System.Drawing.Size(1135, 54)
        Me.gbSelectTestGroupMembers.TabIndex = 1
        Me.gbSelectTestGroupMembers.TabStop = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(665, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(102, 30)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'ChkShowPastRevision
        '
        Me.ChkShowPastRevision.AutoSize = True
        Me.ChkShowPastRevision.Location = New System.Drawing.Point(508, 31)
        Me.ChkShowPastRevision.Name = "ChkShowPastRevision"
        Me.ChkShowPastRevision.Size = New System.Drawing.Size(121, 17)
        Me.ChkShowPastRevision.TabIndex = 5
        Me.ChkShowPastRevision.Text = "Show Past Revision"
        Me.ChkShowPastRevision.UseVisualStyleBackColor = True
        '
        'chkShowObsolete
        '
        Me.chkShowObsolete.AutoSize = True
        Me.chkShowObsolete.Location = New System.Drawing.Point(508, 14)
        Me.chkShowObsolete.Name = "chkShowObsolete"
        Me.chkShowObsolete.Size = New System.Drawing.Size(151, 17)
        Me.chkShowObsolete.TabIndex = 4
        Me.chkShowObsolete.Text = "Show Obsolete Equipment"
        Me.chkShowObsolete.UseVisualStyleBackColor = True
        '
        'btnCancelChanges
        '
        Me.btnCancelChanges.Location = New System.Drawing.Point(333, 14)
        Me.btnCancelChanges.Name = "btnCancelChanges"
        Me.btnCancelChanges.Size = New System.Drawing.Size(102, 30)
        Me.btnCancelChanges.TabIndex = 3
        Me.btnCancelChanges.Text = "Cancel Changes"
        Me.btnCancelChanges.UseVisualStyleBackColor = True
        '
        'btnApplyChanges
        '
        Me.btnApplyChanges.Location = New System.Drawing.Point(225, 14)
        Me.btnApplyChanges.Name = "btnApplyChanges"
        Me.btnApplyChanges.Size = New System.Drawing.Size(102, 30)
        Me.btnApplyChanges.TabIndex = 2
        Me.btnApplyChanges.Text = "Apply Changes"
        Me.btnApplyChanges.UseVisualStyleBackColor = True
        '
        'btnRemoveSelected
        '
        Me.btnRemoveSelected.Location = New System.Drawing.Point(117, 14)
        Me.btnRemoveSelected.Name = "btnRemoveSelected"
        Me.btnRemoveSelected.Size = New System.Drawing.Size(102, 30)
        Me.btnRemoveSelected.TabIndex = 1
        Me.btnRemoveSelected.Text = "Remove"
        Me.btnRemoveSelected.UseVisualStyleBackColor = True
        '
        'btnAddSelected
        '
        Me.btnAddSelected.Location = New System.Drawing.Point(9, 14)
        Me.btnAddSelected.Name = "btnAddSelected"
        Me.btnAddSelected.Size = New System.Drawing.Size(102, 30)
        Me.btnAddSelected.TabIndex = 0
        Me.btnAddSelected.Text = "Add"
        Me.btnAddSelected.UseVisualStyleBackColor = True
        '
        'dgvTestGroupMembersSelected
        '
        Me.dgvTestGroupMembersSelected.AllowUserToOrderColumns = True
        Me.dgvTestGroupMembersSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTestGroupMembersSelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestGroupMembersSelected.Location = New System.Drawing.Point(3, 246)
        Me.dgvTestGroupMembersSelected.Name = "dgvTestGroupMembersSelected"
        Me.dgvTestGroupMembersSelected.Size = New System.Drawing.Size(1135, 196)
        Me.dgvTestGroupMembersSelected.TabIndex = 2
        '
        'frmSelectTestGroupMembers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1141, 445)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSelectTestGroupMembers"
        Me.Text = "Select Test Group Members"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgvAllTestGroupMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSelectTestGroupMembers.ResumeLayout(False)
        Me.gbSelectTestGroupMembers.PerformLayout()
        CType(Me.dgvTestGroupMembersSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgvAllTestGroupMembers As System.Windows.Forms.DataGridView
    Friend WithEvents gbSelectTestGroupMembers As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelChanges As System.Windows.Forms.Button
    Friend WithEvents btnApplyChanges As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelected As System.Windows.Forms.Button
    Friend WithEvents btnAddSelected As System.Windows.Forms.Button
    Friend WithEvents dgvTestGroupMembersSelected As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ChkShowPastRevision As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowObsolete As System.Windows.Forms.CheckBox
End Class
