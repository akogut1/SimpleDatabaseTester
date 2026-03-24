

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectTestEquipment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectTestEquipment))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTestEquipmentFilter = New System.Windows.Forms.Label()
        Me.btnTestEquipmentRefresh = New System.Windows.Forms.Button()
        Me.cbTestEquipmentTypeFilter = New xboXComboBox()
        Me.chkTestEquipmentShowInactiveRev = New System.Windows.Forms.CheckBox()
        Me.chkShowPrevious = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.panelTestEquipmentRecord = New System.Windows.Forms.Panel()
        Me.rocbTestEquipmentUser = New ReadOnlyComboBox()
        Me.txtTestEquipmentAltSerialNumber = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentAltSerialNumber = New System.Windows.Forms.Label()
        Me.txtTestEquipmentLocation = New System.Windows.Forms.TextBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.rocbEditModeTest = New ReadOnlyComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTestEquipmentObsoleteDate = New System.Windows.Forms.TextBox()
        Me.dtpTestEquipmentObsoleteDate = New System.Windows.Forms.DateTimePicker()
        Me.lblObsoleteDate = New System.Windows.Forms.Label()
        Me.txtTestEquipmentNextCalDate = New System.Windows.Forms.TextBox()
        Me.txtTestEquipmentLastCalDate = New System.Windows.Forms.TextBox()
        Me.readonlycbTestEquipmentType = New ReadOnlyComboBox()
        Me.btnTestReadOnly = New System.Windows.Forms.Button()
        Me.chkTestEquipmentCalReq = New System.Windows.Forms.CheckBox()
        Me.txtTestEquipmentNote = New System.Windows.Forms.RichTextBox()
        Me.lblTestEquipmentNote = New System.Windows.Forms.Label()
        Me.txtTestEquipmentLabIdentifier = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbTestEquipmentTestGroup = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnEditTestGroup = New System.Windows.Forms.Button()
        Me.btnTestEquipmentTestGroupListMembers = New System.Windows.Forms.Button()
        Me.btnTestEquipmentTestGroupUpdate = New System.Windows.Forms.Button()
        Me.lblTestEquipmentTestGroupMembers = New System.Windows.Forms.Label()
        Me.txtTestEquipmentTestGroupMembers = New System.Windows.Forms.TextBox()
        Me.lbltestEquipmentType = New System.Windows.Forms.Label()
        Me.chkTestEquipmentIsTestGroup = New System.Windows.Forms.CheckBox()
        Me.txtTestEquipmentRev = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentRev = New System.Windows.Forms.Label()
        Me.chkTestEquipmentObsolete = New System.Windows.Forms.CheckBox()
        Me.chkActiveRevision = New System.Windows.Forms.CheckBox()
        Me.txtTestEquipmentDescription = New System.Windows.Forms.RichTextBox()
        Me.dtpTestEquipnextCalDate = New System.Windows.Forms.DateTimePicker()
        Me.lblTestEquipNextCalDate = New System.Windows.Forms.Label()
        Me.dtpTestEquipLastCalDate = New System.Windows.Forms.DateTimePicker()
        Me.lblTestEquipLastCalDate = New System.Windows.Forms.Label()
        Me.lblTestEquipmentDescription = New System.Windows.Forms.Label()
        Me.txttestEquipmentSerialNumber = New System.Windows.Forms.TextBox()
        Me.lbltestEquipmentSerialNumber = New System.Windows.Forms.Label()
        Me.txtTestEquipmentModel = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentModel = New System.Windows.Forms.Label()
        Me.txtTestEquipmentManufacturer = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentManufacturer = New System.Windows.Forms.Label()
        Me.txtTestEquipmentID = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentID = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmAdminEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSaveAndExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmRevise = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmObsolete = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddTestGroupMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvTestEquipment = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.panelTestEquipmentRecord.SuspendLayout()
        Me.gbTestEquipmentTestGroup.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvTestEquipment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.panelTestEquipmentRecord, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(875, 584)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtTestEquipmentFilter)
        Me.Panel2.Controls.Add(Me.btnTestEquipmentRefresh)
        Me.Panel2.Controls.Add(Me.cbTestEquipmentTypeFilter)
        Me.Panel2.Controls.Add(Me.chkTestEquipmentShowInactiveRev)
        Me.Panel2.Controls.Add(Me.chkShowPrevious)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(869, 54)
        Me.Panel2.TabIndex = 3
        '
        'txtTestEquipmentFilter
        '
        Me.txtTestEquipmentFilter.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentFilter.AutoSize = True
        Me.txtTestEquipmentFilter.Location = New System.Drawing.Point(479, 11)
        Me.txtTestEquipmentFilter.Name = "txtTestEquipmentFilter"
        Me.txtTestEquipmentFilter.Size = New System.Drawing.Size(29, 13)
        Me.txtTestEquipmentFilter.TabIndex = 235
        Me.txtTestEquipmentFilter.Text = "Filter"
        '
        'btnTestEquipmentRefresh
        '
        Me.btnTestEquipmentRefresh.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTestEquipmentRefresh.Location = New System.Drawing.Point(514, 31)
        Me.btnTestEquipmentRefresh.Name = "btnTestEquipmentRefresh"
        Me.btnTestEquipmentRefresh.Size = New System.Drawing.Size(59, 20)
        Me.btnTestEquipmentRefresh.TabIndex = 2
        Me.btnTestEquipmentRefresh.Text = "Refresh"
        Me.btnTestEquipmentRefresh.UseVisualStyleBackColor = True
        '
        'cbTestEquipmentTypeFilter
        '
        Me.cbTestEquipmentTypeFilter.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbTestEquipmentTypeFilter.Location = New System.Drawing.Point(514, 8)
        Me.cbTestEquipmentTypeFilter.Name = "cbTestEquipmentTypeFilter"
        Me.cbTestEquipmentTypeFilter.Size = New System.Drawing.Size(131, 21)
        Me.cbTestEquipmentTypeFilter.TabIndex = 236
        Me.cbTestEquipmentTypeFilter.Tag = "TEST_TYPE"
        '
        'chkTestEquipmentShowInactiveRev
        '
        Me.chkTestEquipmentShowInactiveRev.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTestEquipmentShowInactiveRev.AutoSize = True
        Me.chkTestEquipmentShowInactiveRev.Location = New System.Drawing.Point(712, 22)
        Me.chkTestEquipmentShowInactiveRev.Name = "chkTestEquipmentShowInactiveRev"
        Me.chkTestEquipmentShowInactiveRev.Size = New System.Drawing.Size(94, 17)
        Me.chkTestEquipmentShowInactiveRev.TabIndex = 235
        Me.chkTestEquipmentShowInactiveRev.Text = "Show Inactive"
        Me.chkTestEquipmentShowInactiveRev.UseVisualStyleBackColor = True
        '
        'chkShowPrevious
        '
        Me.chkShowPrevious.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkShowPrevious.AutoSize = True
        Me.chkShowPrevious.Location = New System.Drawing.Point(712, 4)
        Me.chkShowPrevious.Name = "chkShowPrevious"
        Me.chkShowPrevious.Size = New System.Drawing.Size(101, 17)
        Me.chkShowPrevious.TabIndex = 6
        Me.chkShowPrevious.Text = "Show Obsolete "
        Me.chkShowPrevious.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.985!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.015!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOK, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(405, 48)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(205, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(197, 42)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Location = New System.Drawing.Point(3, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(196, 42)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'panelTestEquipmentRecord
        '
        Me.panelTestEquipmentRecord.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.panelTestEquipmentRecord.Controls.Add(Me.rocbTestEquipmentUser)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentAltSerialNumber)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentAltSerialNumber)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentLocation)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblLocation)
        Me.panelTestEquipmentRecord.Controls.Add(Me.rocbEditModeTest)
        Me.panelTestEquipmentRecord.Controls.Add(Me.Button1)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.dtpTestEquipmentObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentNextCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentLastCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.readonlycbTestEquipmentType)
        Me.panelTestEquipmentRecord.Controls.Add(Me.btnTestReadOnly)
        Me.panelTestEquipmentRecord.Controls.Add(Me.chkTestEquipmentCalReq)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentNote)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentNote)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentLabIdentifier)
        Me.panelTestEquipmentRecord.Controls.Add(Me.Label1)
        Me.panelTestEquipmentRecord.Controls.Add(Me.gbTestEquipmentTestGroup)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lbltestEquipmentType)
        Me.panelTestEquipmentRecord.Controls.Add(Me.chkTestEquipmentIsTestGroup)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentRev)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentRev)
        Me.panelTestEquipmentRecord.Controls.Add(Me.chkTestEquipmentObsolete)
        Me.panelTestEquipmentRecord.Controls.Add(Me.chkActiveRevision)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentDescription)
        Me.panelTestEquipmentRecord.Controls.Add(Me.dtpTestEquipnextCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipNextCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.dtpTestEquipLastCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipLastCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentDescription)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txttestEquipmentSerialNumber)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lbltestEquipmentSerialNumber)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentModel)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentModel)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentManufacturer)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentManufacturer)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentID)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblTestEquipmentID)
        Me.panelTestEquipmentRecord.Controls.Add(Me.MenuStrip1)
        Me.panelTestEquipmentRecord.Location = New System.Drawing.Point(3, 63)
        Me.panelTestEquipmentRecord.Name = "panelTestEquipmentRecord"
        Me.panelTestEquipmentRecord.Size = New System.Drawing.Size(869, 256)
        Me.panelTestEquipmentRecord.TabIndex = 4
        '
        'rocbTestEquipmentUser
        '
        Me.rocbTestEquipmentUser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rocbTestEquipmentUser.FormattingEnabled = True
        Me.rocbTestEquipmentUser.Location = New System.Drawing.Point(482, 23)
        Me.rocbTestEquipmentUser.Name = "rocbTestEquipmentUser"
        Me.rocbTestEquipmentUser.Size = New System.Drawing.Size(155, 21)
        Me.rocbTestEquipmentUser.TabIndex = 256
        '
        'txtTestEquipmentAltSerialNumber
        '
        Me.txtTestEquipmentAltSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentAltSerialNumber.Location = New System.Drawing.Point(175, 59)
        Me.txtTestEquipmentAltSerialNumber.Name = "txtTestEquipmentAltSerialNumber"
        Me.txtTestEquipmentAltSerialNumber.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentAltSerialNumber.TabIndex = 255
        '
        'lblTestEquipmentAltSerialNumber
        '
        Me.lblTestEquipmentAltSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentAltSerialNumber.AutoSize = True
        Me.lblTestEquipmentAltSerialNumber.Location = New System.Drawing.Point(179, 43)
        Me.lblTestEquipmentAltSerialNumber.Name = "lblTestEquipmentAltSerialNumber"
        Me.lblTestEquipmentAltSerialNumber.Size = New System.Drawing.Size(78, 13)
        Me.lblTestEquipmentAltSerialNumber.TabIndex = 254
        Me.lblTestEquipmentAltSerialNumber.Text = "Alt Ser Number"
        '
        'txtTestEquipmentLocation
        '
        Me.txtTestEquipmentLocation.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentLocation.Location = New System.Drawing.Point(175, 23)
        Me.txtTestEquipmentLocation.Name = "txtTestEquipmentLocation"
        Me.txtTestEquipmentLocation.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentLocation.TabIndex = 253
        '
        'lblLocation
        '
        Me.lblLocation.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(179, 7)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(48, 13)
        Me.lblLocation.TabIndex = 252
        Me.lblLocation.Text = "Location"
        '
        'rocbEditModeTest
        '
        Me.rocbEditModeTest.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rocbEditModeTest.Enabled = False
        Me.rocbEditModeTest.FormattingEnabled = True
        Me.rocbEditModeTest.Location = New System.Drawing.Point(773, 124)
        Me.rocbEditModeTest.Name = "rocbEditModeTest"
        Me.rocbEditModeTest.Size = New System.Drawing.Size(32, 21)
        Me.rocbEditModeTest.TabIndex = 250
        Me.rocbEditModeTest.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(832, 116)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 34)
        Me.Button1.TabIndex = 249
        Me.Button1.Text = "Change View"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'txtTestEquipmentObsoleteDate
        '
        Me.txtTestEquipmentObsoleteDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentObsoleteDate.Location = New System.Drawing.Point(484, 135)
        Me.txtTestEquipmentObsoleteDate.Name = "txtTestEquipmentObsoleteDate"
        Me.txtTestEquipmentObsoleteDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentObsoleteDate.TabIndex = 248
        '
        'dtpTestEquipmentObsoleteDate
        '
        Me.dtpTestEquipmentObsoleteDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dtpTestEquipmentObsoleteDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipmentObsoleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipmentObsoleteDate.Location = New System.Drawing.Point(571, 135)
        Me.dtpTestEquipmentObsoleteDate.Name = "dtpTestEquipmentObsoleteDate"
        Me.dtpTestEquipmentObsoleteDate.RightToLeftLayout = True
        Me.dtpTestEquipmentObsoleteDate.Size = New System.Drawing.Size(12, 20)
        Me.dtpTestEquipmentObsoleteDate.TabIndex = 246
        Me.dtpTestEquipmentObsoleteDate.Tag = "Date Closed"
        '
        'lblObsoleteDate
        '
        Me.lblObsoleteDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblObsoleteDate.AutoSize = True
        Me.lblObsoleteDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObsoleteDate.Location = New System.Drawing.Point(487, 119)
        Me.lblObsoleteDate.Name = "lblObsoleteDate"
        Me.lblObsoleteDate.Size = New System.Drawing.Size(75, 13)
        Me.lblObsoleteDate.TabIndex = 247
        Me.lblObsoleteDate.Text = "ObsoleteDate:"
        Me.lblObsoleteDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTestEquipmentNextCalDate
        '
        Me.txtTestEquipmentNextCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentNextCalDate.Location = New System.Drawing.Point(338, 135)
        Me.txtTestEquipmentNextCalDate.Name = "txtTestEquipmentNextCalDate"
        Me.txtTestEquipmentNextCalDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentNextCalDate.TabIndex = 245
        '
        'txtTestEquipmentLastCalDate
        '
        Me.txtTestEquipmentLastCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentLastCalDate.Location = New System.Drawing.Point(339, 95)
        Me.txtTestEquipmentLastCalDate.Name = "txtTestEquipmentLastCalDate"
        Me.txtTestEquipmentLastCalDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentLastCalDate.TabIndex = 244
        '
        'readonlycbTestEquipmentType
        '
        Me.readonlycbTestEquipmentType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.readonlycbTestEquipmentType.FormattingEnabled = True
        Me.readonlycbTestEquipmentType.Location = New System.Drawing.Point(175, 95)
        Me.readonlycbTestEquipmentType.Name = "readonlycbTestEquipmentType"
        Me.readonlycbTestEquipmentType.Size = New System.Drawing.Size(155, 21)
        Me.readonlycbTestEquipmentType.TabIndex = 243
        '
        'btnTestReadOnly
        '
        Me.btnTestReadOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTestReadOnly.Enabled = False
        Me.btnTestReadOnly.Location = New System.Drawing.Point(815, 120)
        Me.btnTestReadOnly.Name = "btnTestReadOnly"
        Me.btnTestReadOnly.Size = New System.Drawing.Size(23, 34)
        Me.btnTestReadOnly.TabIndex = 242
        Me.btnTestReadOnly.Text = "Read Only"
        Me.btnTestReadOnly.UseVisualStyleBackColor = True
        Me.btnTestReadOnly.Visible = False
        '
        'chkTestEquipmentCalReq
        '
        Me.chkTestEquipmentCalReq.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTestEquipmentCalReq.AutoCheck = False
        Me.chkTestEquipmentCalReq.AutoSize = True
        Me.chkTestEquipmentCalReq.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chkTestEquipmentCalReq.Location = New System.Drawing.Point(346, 44)
        Me.chkTestEquipmentCalReq.Name = "chkTestEquipmentCalReq"
        Me.chkTestEquipmentCalReq.Size = New System.Drawing.Size(72, 31)
        Me.chkTestEquipmentCalReq.TabIndex = 240
        Me.chkTestEquipmentCalReq.Text = "Cal Required"
        Me.chkTestEquipmentCalReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTestEquipmentCalReq.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentNote
        '
        Me.txtTestEquipmentNote.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentNote.Location = New System.Drawing.Point(336, 176)
        Me.txtTestEquipmentNote.Name = "txtTestEquipmentNote"
        Me.txtTestEquipmentNote.Size = New System.Drawing.Size(284, 77)
        Me.txtTestEquipmentNote.TabIndex = 239
        Me.txtTestEquipmentNote.Text = ""
        '
        'lblTestEquipmentNote
        '
        Me.lblTestEquipmentNote.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentNote.AutoSize = True
        Me.lblTestEquipmentNote.Location = New System.Drawing.Point(340, 158)
        Me.lblTestEquipmentNote.Name = "lblTestEquipmentNote"
        Me.lblTestEquipmentNote.Size = New System.Drawing.Size(35, 13)
        Me.lblTestEquipmentNote.TabIndex = 238
        Me.lblTestEquipmentNote.Text = "Notes"
        '
        'txtTestEquipmentLabIdentifier
        '
        Me.txtTestEquipmentLabIdentifier.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentLabIdentifier.Location = New System.Drawing.Point(175, 135)
        Me.txtTestEquipmentLabIdentifier.Name = "txtTestEquipmentLabIdentifier"
        Me.txtTestEquipmentLabIdentifier.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentLabIdentifier.TabIndex = 237
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(181, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 236
        Me.Label1.Text = "Lab ID"
        '
        'gbTestEquipmentTestGroup
        '
        Me.gbTestEquipmentTestGroup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.Button3)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.btnEditTestGroup)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.btnTestEquipmentTestGroupListMembers)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.btnTestEquipmentTestGroupUpdate)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.lblTestEquipmentTestGroupMembers)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.txtTestEquipmentTestGroupMembers)
        Me.gbTestEquipmentTestGroup.Location = New System.Drawing.Point(631, 156)
        Me.gbTestEquipmentTestGroup.Name = "gbTestEquipmentTestGroup"
        Me.gbTestEquipmentTestGroup.Size = New System.Drawing.Size(228, 93)
        Me.gbTestEquipmentTestGroup.TabIndex = 235
        Me.gbTestEquipmentTestGroup.TabStop = False
        Me.gbTestEquipmentTestGroup.Text = "Test Group"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(120, 18)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(54, 22)
        Me.Button3.TabIndex = 240
        Me.Button3.Text = "Remove"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'btnEditTestGroup
        '
        Me.btnEditTestGroup.Location = New System.Drawing.Point(6, 18)
        Me.btnEditTestGroup.Name = "btnEditTestGroup"
        Me.btnEditTestGroup.Size = New System.Drawing.Size(52, 22)
        Me.btnEditTestGroup.TabIndex = 239
        Me.btnEditTestGroup.Text = "Edit"
        Me.btnEditTestGroup.UseVisualStyleBackColor = True
        Me.btnEditTestGroup.Visible = False
        '
        'btnTestEquipmentTestGroupListMembers
        '
        Me.btnTestEquipmentTestGroupListMembers.Location = New System.Drawing.Point(180, 19)
        Me.btnTestEquipmentTestGroupListMembers.Name = "btnTestEquipmentTestGroupListMembers"
        Me.btnTestEquipmentTestGroupListMembers.Size = New System.Drawing.Size(42, 22)
        Me.btnTestEquipmentTestGroupListMembers.TabIndex = 238
        Me.btnTestEquipmentTestGroupListMembers.Text = "List"
        Me.btnTestEquipmentTestGroupListMembers.UseVisualStyleBackColor = True
        Me.btnTestEquipmentTestGroupListMembers.Visible = False
        '
        'btnTestEquipmentTestGroupUpdate
        '
        Me.btnTestEquipmentTestGroupUpdate.Location = New System.Drawing.Point(64, 19)
        Me.btnTestEquipmentTestGroupUpdate.Name = "btnTestEquipmentTestGroupUpdate"
        Me.btnTestEquipmentTestGroupUpdate.Size = New System.Drawing.Size(54, 22)
        Me.btnTestEquipmentTestGroupUpdate.TabIndex = 237
        Me.btnTestEquipmentTestGroupUpdate.Text = "Update"
        Me.btnTestEquipmentTestGroupUpdate.UseVisualStyleBackColor = True
        Me.btnTestEquipmentTestGroupUpdate.Visible = False
        '
        'lblTestEquipmentTestGroupMembers
        '
        Me.lblTestEquipmentTestGroupMembers.AutoSize = True
        Me.lblTestEquipmentTestGroupMembers.Location = New System.Drawing.Point(8, 49)
        Me.lblTestEquipmentTestGroupMembers.Name = "lblTestEquipmentTestGroupMembers"
        Me.lblTestEquipmentTestGroupMembers.Size = New System.Drawing.Size(50, 13)
        Me.lblTestEquipmentTestGroupMembers.TabIndex = 236
        Me.lblTestEquipmentTestGroupMembers.Text = "Members"
        '
        'txtTestEquipmentTestGroupMembers
        '
        Me.txtTestEquipmentTestGroupMembers.Location = New System.Drawing.Point(8, 67)
        Me.txtTestEquipmentTestGroupMembers.Name = "txtTestEquipmentTestGroupMembers"
        Me.txtTestEquipmentTestGroupMembers.Size = New System.Drawing.Size(214, 20)
        Me.txtTestEquipmentTestGroupMembers.TabIndex = 235
        '
        'lbltestEquipmentType
        '
        Me.lbltestEquipmentType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbltestEquipmentType.AutoSize = True
        Me.lbltestEquipmentType.Location = New System.Drawing.Point(179, 81)
        Me.lbltestEquipmentType.Name = "lbltestEquipmentType"
        Me.lbltestEquipmentType.Size = New System.Drawing.Size(31, 13)
        Me.lbltestEquipmentType.TabIndex = 233
        Me.lbltestEquipmentType.Text = "Type"
        '
        'chkTestEquipmentIsTestGroup
        '
        Me.chkTestEquipmentIsTestGroup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTestEquipmentIsTestGroup.AutoCheck = False
        Me.chkTestEquipmentIsTestGroup.AutoSize = True
        Me.chkTestEquipmentIsTestGroup.Location = New System.Drawing.Point(632, 137)
        Me.chkTestEquipmentIsTestGroup.Name = "chkTestEquipmentIsTestGroup"
        Me.chkTestEquipmentIsTestGroup.Size = New System.Drawing.Size(85, 17)
        Me.chkTestEquipmentIsTestGroup.TabIndex = 234
        Me.chkTestEquipmentIsTestGroup.Text = "Test Group?"
        Me.chkTestEquipmentIsTestGroup.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentRev
        '
        Me.txtTestEquipmentRev.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentRev.Location = New System.Drawing.Point(111, 22)
        Me.txtTestEquipmentRev.Name = "txtTestEquipmentRev"
        Me.txtTestEquipmentRev.Size = New System.Drawing.Size(59, 20)
        Me.txtTestEquipmentRev.TabIndex = 231
        '
        'lblTestEquipmentRev
        '
        Me.lblTestEquipmentRev.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentRev.AutoSize = True
        Me.lblTestEquipmentRev.Location = New System.Drawing.Point(108, 6)
        Me.lblTestEquipmentRev.Name = "lblTestEquipmentRev"
        Me.lblTestEquipmentRev.Size = New System.Drawing.Size(27, 13)
        Me.lblTestEquipmentRev.TabIndex = 230
        Me.lblTestEquipmentRev.Text = "Rev"
        '
        'chkTestEquipmentObsolete
        '
        Me.chkTestEquipmentObsolete.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTestEquipmentObsolete.AutoCheck = False
        Me.chkTestEquipmentObsolete.AutoSize = True
        Me.chkTestEquipmentObsolete.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chkTestEquipmentObsolete.Location = New System.Drawing.Point(501, 83)
        Me.chkTestEquipmentObsolete.Name = "chkTestEquipmentObsolete"
        Me.chkTestEquipmentObsolete.Size = New System.Drawing.Size(53, 31)
        Me.chkTestEquipmentObsolete.TabIndex = 229
        Me.chkTestEquipmentObsolete.Text = "Obsolete"
        Me.chkTestEquipmentObsolete.UseVisualStyleBackColor = True
        '
        'chkActiveRevision
        '
        Me.chkActiveRevision.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkActiveRevision.AutoCheck = False
        Me.chkActiveRevision.AutoSize = True
        Me.chkActiveRevision.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chkActiveRevision.Location = New System.Drawing.Point(339, 7)
        Me.chkActiveRevision.Name = "chkActiveRevision"
        Me.chkActiveRevision.Size = New System.Drawing.Size(85, 31)
        Me.chkActiveRevision.TabIndex = 228
        Me.chkActiveRevision.Text = "Active Revision"
        Me.chkActiveRevision.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentDescription
        '
        Me.txtTestEquipmentDescription.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentDescription.Location = New System.Drawing.Point(15, 176)
        Me.txtTestEquipmentDescription.Name = "txtTestEquipmentDescription"
        Me.txtTestEquipmentDescription.Size = New System.Drawing.Size(315, 77)
        Me.txtTestEquipmentDescription.TabIndex = 227
        Me.txtTestEquipmentDescription.Text = ""
        '
        'dtpTestEquipnextCalDate
        '
        Me.dtpTestEquipnextCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dtpTestEquipnextCalDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipnextCalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipnextCalDate.Location = New System.Drawing.Point(423, 135)
        Me.dtpTestEquipnextCalDate.Name = "dtpTestEquipnextCalDate"
        Me.dtpTestEquipnextCalDate.RightToLeftLayout = True
        Me.dtpTestEquipnextCalDate.Size = New System.Drawing.Size(12, 20)
        Me.dtpTestEquipnextCalDate.TabIndex = 223
        Me.dtpTestEquipnextCalDate.Tag = "Date Closed"
        '
        'lblTestEquipNextCalDate
        '
        Me.lblTestEquipNextCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipNextCalDate.AutoSize = True
        Me.lblTestEquipNextCalDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestEquipNextCalDate.Location = New System.Drawing.Point(342, 120)
        Me.lblTestEquipNextCalDate.Name = "lblTestEquipNextCalDate"
        Me.lblTestEquipNextCalDate.Size = New System.Drawing.Size(76, 13)
        Me.lblTestEquipNextCalDate.TabIndex = 224
        Me.lblTestEquipNextCalDate.Text = "Next Cal Date:"
        Me.lblTestEquipNextCalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTestEquipLastCalDate
        '
        Me.dtpTestEquipLastCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dtpTestEquipLastCalDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipLastCalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipLastCalDate.Location = New System.Drawing.Point(425, 95)
        Me.dtpTestEquipLastCalDate.Name = "dtpTestEquipLastCalDate"
        Me.dtpTestEquipLastCalDate.RightToLeftLayout = True
        Me.dtpTestEquipLastCalDate.Size = New System.Drawing.Size(12, 20)
        Me.dtpTestEquipLastCalDate.TabIndex = 220
        Me.dtpTestEquipLastCalDate.Tag = "Date Closed"
        '
        'lblTestEquipLastCalDate
        '
        Me.lblTestEquipLastCalDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipLastCalDate.AutoSize = True
        Me.lblTestEquipLastCalDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestEquipLastCalDate.Location = New System.Drawing.Point(344, 81)
        Me.lblTestEquipLastCalDate.Name = "lblTestEquipLastCalDate"
        Me.lblTestEquipLastCalDate.Size = New System.Drawing.Size(74, 13)
        Me.lblTestEquipLastCalDate.TabIndex = 221
        Me.lblTestEquipLastCalDate.Text = "Last Cal Date:"
        Me.lblTestEquipLastCalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestEquipmentDescription
        '
        Me.lblTestEquipmentDescription.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentDescription.AutoSize = True
        Me.lblTestEquipmentDescription.Location = New System.Drawing.Point(19, 159)
        Me.lblTestEquipmentDescription.Name = "lblTestEquipmentDescription"
        Me.lblTestEquipmentDescription.Size = New System.Drawing.Size(60, 13)
        Me.lblTestEquipmentDescription.TabIndex = 8
        Me.lblTestEquipmentDescription.Text = "Description"
        '
        'txttestEquipmentSerialNumber
        '
        Me.txttestEquipmentSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txttestEquipmentSerialNumber.Location = New System.Drawing.Point(15, 135)
        Me.txttestEquipmentSerialNumber.Name = "txttestEquipmentSerialNumber"
        Me.txttestEquipmentSerialNumber.Size = New System.Drawing.Size(155, 20)
        Me.txttestEquipmentSerialNumber.TabIndex = 7
        '
        'lbltestEquipmentSerialNumber
        '
        Me.lbltestEquipmentSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbltestEquipmentSerialNumber.AutoSize = True
        Me.lbltestEquipmentSerialNumber.Location = New System.Drawing.Point(19, 118)
        Me.lbltestEquipmentSerialNumber.Name = "lbltestEquipmentSerialNumber"
        Me.lbltestEquipmentSerialNumber.Size = New System.Drawing.Size(73, 13)
        Me.lbltestEquipmentSerialNumber.TabIndex = 6
        Me.lbltestEquipmentSerialNumber.Text = "Serial Number"
        '
        'txtTestEquipmentModel
        '
        Me.txtTestEquipmentModel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentModel.Location = New System.Drawing.Point(15, 96)
        Me.txtTestEquipmentModel.Name = "txtTestEquipmentModel"
        Me.txtTestEquipmentModel.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentModel.TabIndex = 5
        '
        'lblTestEquipmentModel
        '
        Me.lblTestEquipmentModel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentModel.AutoSize = True
        Me.lblTestEquipmentModel.Location = New System.Drawing.Point(18, 81)
        Me.lblTestEquipmentModel.Name = "lblTestEquipmentModel"
        Me.lblTestEquipmentModel.Size = New System.Drawing.Size(36, 13)
        Me.lblTestEquipmentModel.TabIndex = 4
        Me.lblTestEquipmentModel.Text = "Model"
        '
        'txtTestEquipmentManufacturer
        '
        Me.txtTestEquipmentManufacturer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentManufacturer.Location = New System.Drawing.Point(15, 59)
        Me.txtTestEquipmentManufacturer.Name = "txtTestEquipmentManufacturer"
        Me.txtTestEquipmentManufacturer.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentManufacturer.TabIndex = 3
        '
        'lblTestEquipmentManufacturer
        '
        Me.lblTestEquipmentManufacturer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentManufacturer.AutoSize = True
        Me.lblTestEquipmentManufacturer.Location = New System.Drawing.Point(19, 43)
        Me.lblTestEquipmentManufacturer.Name = "lblTestEquipmentManufacturer"
        Me.lblTestEquipmentManufacturer.Size = New System.Drawing.Size(70, 13)
        Me.lblTestEquipmentManufacturer.TabIndex = 2
        Me.lblTestEquipmentManufacturer.Text = "Manufacturer"
        '
        'txtTestEquipmentID
        '
        Me.txtTestEquipmentID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTestEquipmentID.Location = New System.Drawing.Point(15, 22)
        Me.txtTestEquipmentID.Name = "txtTestEquipmentID"
        Me.txtTestEquipmentID.Size = New System.Drawing.Size(51, 20)
        Me.txtTestEquipmentID.TabIndex = 1
        '
        'lblTestEquipmentID
        '
        Me.lblTestEquipmentID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTestEquipmentID.AutoSize = True
        Me.lblTestEquipmentID.Location = New System.Drawing.Point(19, 6)
        Me.lblTestEquipmentID.Name = "lblTestEquipmentID"
        Me.lblTestEquipmentID.Size = New System.Drawing.Size(18, 13)
        Me.lblTestEquipmentID.TabIndex = 0
        Me.lblTestEquipmentID.Text = "ID"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(812, 3)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(47, 24)
        Me.MenuStrip1.TabIndex = 251
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmNew, Me.tsmCopy, Me.tsmAdminEdit, Me.tsmSave, Me.tsmSaveAndExit, Me.tsmCancel, Me.tsmRevise, Me.tsmObsolete, Me.AddTestGroupMembersToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'tsmNew
        '
        Me.tsmNew.Name = "tsmNew"
        Me.tsmNew.Size = New System.Drawing.Size(207, 22)
        Me.tsmNew.Text = "New"
        '
        'tsmCopy
        '
        Me.tsmCopy.Name = "tsmCopy"
        Me.tsmCopy.Size = New System.Drawing.Size(207, 22)
        Me.tsmCopy.Text = "Copy"
        '
        'tsmAdminEdit
        '
        Me.tsmAdminEdit.Name = "tsmAdminEdit"
        Me.tsmAdminEdit.Size = New System.Drawing.Size(207, 22)
        Me.tsmAdminEdit.Text = "Admin Edit"
        '
        'tsmSave
        '
        Me.tsmSave.Name = "tsmSave"
        Me.tsmSave.Size = New System.Drawing.Size(207, 22)
        Me.tsmSave.Text = "Save"
        '
        'tsmSaveAndExit
        '
        Me.tsmSaveAndExit.Name = "tsmSaveAndExit"
        Me.tsmSaveAndExit.Size = New System.Drawing.Size(207, 22)
        Me.tsmSaveAndExit.Text = "Save and Exit Edit Mode"
        '
        'tsmCancel
        '
        Me.tsmCancel.Name = "tsmCancel"
        Me.tsmCancel.Size = New System.Drawing.Size(207, 22)
        Me.tsmCancel.Text = "Cancel"
        '
        'tsmRevise
        '
        Me.tsmRevise.Name = "tsmRevise"
        Me.tsmRevise.Size = New System.Drawing.Size(207, 22)
        Me.tsmRevise.Text = "Revise Calibration Info"
        '
        'tsmObsolete
        '
        Me.tsmObsolete.Name = "tsmObsolete"
        Me.tsmObsolete.Size = New System.Drawing.Size(207, 22)
        Me.tsmObsolete.Text = "Retire Equipment"
        '
        'AddTestGroupMembersToolStripMenuItem
        '
        Me.AddTestGroupMembersToolStripMenuItem.Name = "AddTestGroupMembersToolStripMenuItem"
        Me.AddTestGroupMembersToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.AddTestGroupMembersToolStripMenuItem.Text = "Edit Test Group Members"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.dgvTestEquipment)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 325)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(869, 256)
        Me.Panel1.TabIndex = 5
        '
        'dgvTestEquipment
        '
        Me.dgvTestEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTestEquipment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestEquipment.Location = New System.Drawing.Point(0, 0)
        Me.dgvTestEquipment.Name = "dgvTestEquipment"
        Me.dgvTestEquipment.ReadOnly = True
        Me.dgvTestEquipment.Size = New System.Drawing.Size(869, 256)
        Me.dgvTestEquipment.TabIndex = 1
        '
        'frmSelectTestEquipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 584)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmSelectTestEquipment"
        Me.Text = "Test Equipment"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.panelTestEquipmentRecord.ResumeLayout(False)
        Me.panelTestEquipmentRecord.PerformLayout()
        Me.gbTestEquipmentTestGroup.ResumeLayout(False)
        Me.gbTestEquipmentTestGroup.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvTestEquipment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgvTestEquipment As System.Windows.Forms.DataGridView
    Friend WithEvents btnTestEquipmentRefresh As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents chkShowPrevious As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents panelTestEquipmentRecord As System.Windows.Forms.Panel
    Friend WithEvents lblTestEquipmentDescription As System.Windows.Forms.Label
    Friend WithEvents txttestEquipmentSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lbltestEquipmentSerialNumber As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentModel As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentModel As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentManufacturer As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentManufacturer As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentID As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentID As System.Windows.Forms.Label
    Friend WithEvents dtpTestEquipnextCalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTestEquipNextCalDate As System.Windows.Forms.Label
    Friend WithEvents dtpTestEquipLastCalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTestEquipLastCalDate As System.Windows.Forms.Label
    Friend WithEvents chkTestEquipmentObsolete As System.Windows.Forms.CheckBox
    Friend WithEvents chkActiveRevision As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestEquipmentDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents txtTestEquipmentFilter As System.Windows.Forms.Label
    Friend WithEvents cbTestEquipmentTypeFilter As xboXComboBox
    Friend WithEvents chkTestEquipmentShowInactiveRev As System.Windows.Forms.CheckBox
    Friend WithEvents chkTestEquipmentIsTestGroup As System.Windows.Forms.CheckBox
    Friend WithEvents lbltestEquipmentType As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentRev As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentRev As System.Windows.Forms.Label
    Friend WithEvents gbTestEquipmentTestGroup As System.Windows.Forms.GroupBox
    Friend WithEvents txtTestEquipmentLabIdentifier As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnTestEquipmentTestGroupListMembers As System.Windows.Forms.Button
    Friend WithEvents btnTestEquipmentTestGroupUpdate As System.Windows.Forms.Button
    Friend WithEvents lblTestEquipmentTestGroupMembers As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentTestGroupMembers As System.Windows.Forms.TextBox
    Friend WithEvents txtTestEquipmentNote As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTestEquipmentNote As System.Windows.Forms.Label
    Friend WithEvents chkTestEquipmentCalReq As System.Windows.Forms.CheckBox
    Friend WithEvents btnTestReadOnly As System.Windows.Forms.Button
    Friend WithEvents readonlycbTestEquipmentType As ReadOnlyComboBox
    Friend WithEvents txtTestEquipmentNextCalDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestEquipmentLastCalDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestEquipmentObsoleteDate As System.Windows.Forms.TextBox
    Friend WithEvents dtpTestEquipmentObsoleteDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblObsoleteDate As System.Windows.Forms.Label
    Friend WithEvents rocbEditModeTest As ReadOnlyComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmAdminEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmSaveAndExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmRevise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmObsolete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddTestGroupMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnEditTestGroup As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtTestEquipmentLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtTestEquipmentAltSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentAltSerialNumber As System.Windows.Forms.Label
    Friend WithEvents rocbTestEquipmentUser As ReadOnlyComboBox
End Class
