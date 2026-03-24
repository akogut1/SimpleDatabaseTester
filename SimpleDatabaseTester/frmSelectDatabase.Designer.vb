<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectDatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectDatabase))
        Me.txtFRDataBaseDataSource = New System.Windows.Forms.TextBox()
        Me.txtFRDataBaseIntitialCatalog = New System.Windows.Forms.TextBox()
        Me.lblInitialCatalog = New System.Windows.Forms.Label()
        Me.lblFRDataBaseDataSource = New System.Windows.Forms.Label()
        Me.chkFRDataBaseIntegratedSecurityState = New System.Windows.Forms.CheckBox()
        Me.btnSelectFRDataBase = New System.Windows.Forms.Button()
        Me.btnFRApplyDataBaseChanges = New System.Windows.Forms.Button()
        Me.btnFRCancelChanges = New System.Windows.Forms.Button()
        Me.gbFailureReportDB = New System.Windows.Forms.GroupBox()
        Me.gbMeterSpecDB = New System.Windows.Forms.GroupBox()
        Me.btnSelectMeterSpecDataBase = New System.Windows.Forms.Button()
        Me.txtMeterSpecDataBaseDataSource = New System.Windows.Forms.TextBox()
        Me.txtMeterSpecInitialCatalog = New System.Windows.Forms.TextBox()
        Me.lblMeterSpecDataBasePassword = New System.Windows.Forms.Label()
        Me.lblMeterSpecDataBasePath = New System.Windows.Forms.Label()
        Me.chkMeterSpecDataBaseIntegratedSecurityState = New System.Windows.Forms.CheckBox()
        Me.EnableEdit = New System.Windows.Forms.Button()
        Me.gbFailureReportDB.SuspendLayout()
        Me.gbMeterSpecDB.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFRDataBaseDataSource
        '
        Me.txtFRDataBaseDataSource.Location = New System.Drawing.Point(8, 78)
        Me.txtFRDataBaseDataSource.Name = "txtFRDataBaseDataSource"
        Me.txtFRDataBaseDataSource.ReadOnly = True
        Me.txtFRDataBaseDataSource.Size = New System.Drawing.Size(513, 20)
        Me.txtFRDataBaseDataSource.TabIndex = 0
        Me.txtFRDataBaseDataSource.Text = "USLAFPC113\SQLEXPRESS_GELAB"
        '
        'txtFRDataBaseIntitialCatalog
        '
        Me.txtFRDataBaseIntitialCatalog.Location = New System.Drawing.Point(8, 122)
        Me.txtFRDataBaseIntitialCatalog.Name = "txtFRDataBaseIntitialCatalog"
        Me.txtFRDataBaseIntitialCatalog.ReadOnly = True
        Me.txtFRDataBaseIntitialCatalog.Size = New System.Drawing.Size(513, 20)
        Me.txtFRDataBaseIntitialCatalog.TabIndex = 1
        Me.txtFRDataBaseIntitialCatalog.Text = "FAILURE_REPORT"
        '
        'lblInitialCatalog
        '
        Me.lblInitialCatalog.AutoSize = True
        Me.lblInitialCatalog.Location = New System.Drawing.Point(10, 105)
        Me.lblInitialCatalog.Name = "lblInitialCatalog"
        Me.lblInitialCatalog.Size = New System.Drawing.Size(70, 13)
        Me.lblInitialCatalog.TabIndex = 2
        Me.lblInitialCatalog.Text = "Initial Catalog"
        '
        'lblFRDataBaseDataSource
        '
        Me.lblFRDataBaseDataSource.AutoSize = True
        Me.lblFRDataBaseDataSource.Location = New System.Drawing.Point(8, 61)
        Me.lblFRDataBaseDataSource.Name = "lblFRDataBaseDataSource"
        Me.lblFRDataBaseDataSource.Size = New System.Drawing.Size(136, 13)
        Me.lblFRDataBaseDataSource.TabIndex = 3
        Me.lblFRDataBaseDataSource.Text = "Failure Report Data Source"
        '
        'chkFRDataBaseIntegratedSecurityState
        '
        Me.chkFRDataBaseIntegratedSecurityState.AutoSize = True
        Me.chkFRDataBaseIntegratedSecurityState.Checked = True
        Me.chkFRDataBaseIntegratedSecurityState.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFRDataBaseIntegratedSecurityState.Enabled = False
        Me.chkFRDataBaseIntegratedSecurityState.Location = New System.Drawing.Point(11, 148)
        Me.chkFRDataBaseIntegratedSecurityState.Name = "chkFRDataBaseIntegratedSecurityState"
        Me.chkFRDataBaseIntegratedSecurityState.Size = New System.Drawing.Size(121, 17)
        Me.chkFRDataBaseIntegratedSecurityState.TabIndex = 4
        Me.chkFRDataBaseIntegratedSecurityState.Text = "Integrated Security?"
        Me.chkFRDataBaseIntegratedSecurityState.UseVisualStyleBackColor = True
        '
        'btnSelectFRDataBase
        '
        Me.btnSelectFRDataBase.Enabled = False
        Me.btnSelectFRDataBase.Location = New System.Drawing.Point(6, 19)
        Me.btnSelectFRDataBase.Name = "btnSelectFRDataBase"
        Me.btnSelectFRDataBase.Size = New System.Drawing.Size(100, 35)
        Me.btnSelectFRDataBase.TabIndex = 13
        Me.btnSelectFRDataBase.Text = "Select Database"
        Me.btnSelectFRDataBase.UseVisualStyleBackColor = True
        '
        'btnFRApplyDataBaseChanges
        '
        Me.btnFRApplyDataBaseChanges.Location = New System.Drawing.Point(298, 426)
        Me.btnFRApplyDataBaseChanges.Name = "btnFRApplyDataBaseChanges"
        Me.btnFRApplyDataBaseChanges.Size = New System.Drawing.Size(103, 41)
        Me.btnFRApplyDataBaseChanges.TabIndex = 14
        Me.btnFRApplyDataBaseChanges.Text = "Apply Changes"
        Me.btnFRApplyDataBaseChanges.UseVisualStyleBackColor = True
        '
        'btnFRCancelChanges
        '
        Me.btnFRCancelChanges.Location = New System.Drawing.Point(407, 426)
        Me.btnFRCancelChanges.Name = "btnFRCancelChanges"
        Me.btnFRCancelChanges.Size = New System.Drawing.Size(103, 41)
        Me.btnFRCancelChanges.TabIndex = 15
        Me.btnFRCancelChanges.Text = "Cancel Changes"
        Me.btnFRCancelChanges.UseVisualStyleBackColor = True
        '
        'gbFailureReportDB
        '
        Me.gbFailureReportDB.Controls.Add(Me.btnSelectFRDataBase)
        Me.gbFailureReportDB.Controls.Add(Me.txtFRDataBaseDataSource)
        Me.gbFailureReportDB.Controls.Add(Me.txtFRDataBaseIntitialCatalog)
        Me.gbFailureReportDB.Controls.Add(Me.lblInitialCatalog)
        Me.gbFailureReportDB.Controls.Add(Me.lblFRDataBaseDataSource)
        Me.gbFailureReportDB.Controls.Add(Me.chkFRDataBaseIntegratedSecurityState)
        Me.gbFailureReportDB.Location = New System.Drawing.Point(-2, 12)
        Me.gbFailureReportDB.Name = "gbFailureReportDB"
        Me.gbFailureReportDB.Size = New System.Drawing.Size(527, 193)
        Me.gbFailureReportDB.TabIndex = 16
        Me.gbFailureReportDB.TabStop = False
        Me.gbFailureReportDB.Text = "Failure Report Database"
        '
        'gbMeterSpecDB
        '
        Me.gbMeterSpecDB.Controls.Add(Me.btnSelectMeterSpecDataBase)
        Me.gbMeterSpecDB.Controls.Add(Me.txtMeterSpecDataBaseDataSource)
        Me.gbMeterSpecDB.Controls.Add(Me.txtMeterSpecInitialCatalog)
        Me.gbMeterSpecDB.Controls.Add(Me.lblMeterSpecDataBasePassword)
        Me.gbMeterSpecDB.Controls.Add(Me.lblMeterSpecDataBasePath)
        Me.gbMeterSpecDB.Controls.Add(Me.chkMeterSpecDataBaseIntegratedSecurityState)
        Me.gbMeterSpecDB.Location = New System.Drawing.Point(4, 211)
        Me.gbMeterSpecDB.Name = "gbMeterSpecDB"
        Me.gbMeterSpecDB.Size = New System.Drawing.Size(527, 193)
        Me.gbMeterSpecDB.TabIndex = 77
        Me.gbMeterSpecDB.TabStop = False
        Me.gbMeterSpecDB.Text = "Meter Specification"
        '
        'btnSelectMeterSpecDataBase
        '
        Me.btnSelectMeterSpecDataBase.Enabled = False
        Me.btnSelectMeterSpecDataBase.Location = New System.Drawing.Point(6, 19)
        Me.btnSelectMeterSpecDataBase.Name = "btnSelectMeterSpecDataBase"
        Me.btnSelectMeterSpecDataBase.Size = New System.Drawing.Size(100, 35)
        Me.btnSelectMeterSpecDataBase.TabIndex = 13
        Me.btnSelectMeterSpecDataBase.Text = "Select Database"
        Me.btnSelectMeterSpecDataBase.UseVisualStyleBackColor = True
        '
        'txtMeterSpecDataBaseDataSource
        '
        Me.txtMeterSpecDataBaseDataSource.Location = New System.Drawing.Point(8, 78)
        Me.txtMeterSpecDataBaseDataSource.Name = "txtMeterSpecDataBaseDataSource"
        Me.txtMeterSpecDataBaseDataSource.ReadOnly = True
        Me.txtMeterSpecDataBaseDataSource.Size = New System.Drawing.Size(513, 20)
        Me.txtMeterSpecDataBaseDataSource.TabIndex = 0
        Me.txtMeterSpecDataBaseDataSource.Text = "USLAFPC113\SQLEXPRESS_GELAB"
        '
        'txtMeterSpecInitialCatalog
        '
        Me.txtMeterSpecInitialCatalog.Location = New System.Drawing.Point(8, 142)
        Me.txtMeterSpecInitialCatalog.Name = "txtMeterSpecInitialCatalog"
        Me.txtMeterSpecInitialCatalog.ReadOnly = True
        Me.txtMeterSpecInitialCatalog.Size = New System.Drawing.Size(513, 20)
        Me.txtMeterSpecInitialCatalog.TabIndex = 1
        Me.txtMeterSpecInitialCatalog.Text = "METER_SPECS"
        '
        'lblMeterSpecDataBasePassword
        '
        Me.lblMeterSpecDataBasePassword.AutoSize = True
        Me.lblMeterSpecDataBasePassword.Location = New System.Drawing.Point(10, 125)
        Me.lblMeterSpecDataBasePassword.Name = "lblMeterSpecDataBasePassword"
        Me.lblMeterSpecDataBasePassword.Size = New System.Drawing.Size(160, 13)
        Me.lblMeterSpecDataBasePassword.TabIndex = 2
        Me.lblMeterSpecDataBasePassword.Text = "Meter Spec Database Password"
        '
        'lblMeterSpecDataBasePath
        '
        Me.lblMeterSpecDataBasePath.AutoSize = True
        Me.lblMeterSpecDataBasePath.Location = New System.Drawing.Point(8, 61)
        Me.lblMeterSpecDataBasePath.Name = "lblMeterSpecDataBasePath"
        Me.lblMeterSpecDataBasePath.Size = New System.Drawing.Size(188, 13)
        Me.lblMeterSpecDataBasePath.TabIndex = 3
        Me.lblMeterSpecDataBasePath.Text = "Meter Spec Database Path and Name"
        '
        'chkMeterSpecDataBaseIntegratedSecurityState
        '
        Me.chkMeterSpecDataBaseIntegratedSecurityState.AutoSize = True
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Checked = True
        Me.chkMeterSpecDataBaseIntegratedSecurityState.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Enabled = False
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Location = New System.Drawing.Point(11, 168)
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Name = "chkMeterSpecDataBaseIntegratedSecurityState"
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Size = New System.Drawing.Size(125, 17)
        Me.chkMeterSpecDataBaseIntegratedSecurityState.TabIndex = 4
        Me.chkMeterSpecDataBaseIntegratedSecurityState.Text = "Persist Security Info?"
        Me.chkMeterSpecDataBaseIntegratedSecurityState.UseVisualStyleBackColor = True
        '
        'EnableEdit
        '
        Me.EnableEdit.Location = New System.Drawing.Point(7, 426)
        Me.EnableEdit.Name = "EnableEdit"
        Me.EnableEdit.Size = New System.Drawing.Size(103, 41)
        Me.EnableEdit.TabIndex = 78
        Me.EnableEdit.Text = "Edit Database Info"
        Me.EnableEdit.UseVisualStyleBackColor = True
        '
        'frmSelectDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 479)
        Me.Controls.Add(Me.EnableEdit)
        Me.Controls.Add(Me.gbMeterSpecDB)
        Me.Controls.Add(Me.gbFailureReportDB)
        Me.Controls.Add(Me.btnFRCancelChanges)
        Me.Controls.Add(Me.btnFRApplyDataBaseChanges)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSelectDatabase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SelectDatabase"
        Me.TopMost = True
        Me.gbFailureReportDB.ResumeLayout(False)
        Me.gbFailureReportDB.PerformLayout()
        Me.gbMeterSpecDB.ResumeLayout(False)
        Me.gbMeterSpecDB.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtFRDataBaseDataSource As System.Windows.Forms.TextBox
    Friend WithEvents txtFRDataBaseIntitialCatalog As System.Windows.Forms.TextBox
    Friend WithEvents lblInitialCatalog As System.Windows.Forms.Label
    Friend WithEvents lblFRDataBaseDataSource As System.Windows.Forms.Label
    Friend WithEvents chkFRDataBaseIntegratedSecurityState As System.Windows.Forms.CheckBox
    Friend WithEvents btnSelectFRDataBase As System.Windows.Forms.Button
    Friend WithEvents btnFRApplyDataBaseChanges As System.Windows.Forms.Button
    Friend WithEvents btnFRCancelChanges As System.Windows.Forms.Button
    Friend WithEvents gbFailureReportDB As System.Windows.Forms.GroupBox
    Friend WithEvents gbMeterSpecDB As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectMeterSpecDataBase As System.Windows.Forms.Button
    Friend WithEvents txtMeterSpecDataBaseDataSource As System.Windows.Forms.TextBox
    Friend WithEvents txtMeterSpecInitialCatalog As System.Windows.Forms.TextBox
    Friend WithEvents lblMeterSpecDataBasePassword As System.Windows.Forms.Label
    Friend WithEvents lblMeterSpecDataBasePath As System.Windows.Forms.Label
    Friend WithEvents chkMeterSpecDataBaseIntegratedSecurityState As System.Windows.Forms.CheckBox
    Friend WithEvents EnableEdit As System.Windows.Forms.Button
End Class
