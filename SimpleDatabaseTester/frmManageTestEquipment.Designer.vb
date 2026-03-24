<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageTestEquipment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageTestEquipment))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTestEquipmentFilter = New System.Windows.Forms.Label()
        Me.cbTestEquipmentTypeFilter = New System.Windows.Forms.ComboBox()
        Me.chkTestEquipmentShowInactiveRev = New System.Windows.Forms.CheckBox()
        Me.btnTestEquipmentRefresh = New System.Windows.Forms.Button()
        Me.chkShowPrevious = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAddSelelected = New System.Windows.Forms.Button()
        Me.btnSaveTestEquipmentRecord = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAddTestEquipment = New System.Windows.Forms.Button()
        Me.btnObsoleteTestEquipmentRecord = New System.Windows.Forms.Button()
        Me.btnEditTestEquipmentRecord = New System.Windows.Forms.Button()
        Me.panelTestEquipmentRecord = New System.Windows.Forms.Panel()
        Me.rcbEditModeTest = New ReadOnlyComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTestEquipmentObsoleteDate = New System.Windows.Forms.TextBox()
        Me.dtpTestEquipmentObsoleteDate = New System.Windows.Forms.DateTimePicker()
        Me.lblObsoleteDate = New System.Windows.Forms.Label()
        Me.txtTestEquipmentNextCalDate = New System.Windows.Forms.TextBox()
        Me.txtTestEquipmentLastCalDate = New System.Windows.Forms.TextBox()
        Me.xocbTestEquipmentType = New ReadOnlyComboBox()
        Me.btnTestReadOnly = New System.Windows.Forms.Button()
        Me.chkTestEquipmentCalReq = New System.Windows.Forms.CheckBox()
        Me.txtTestEquipmentNote = New System.Windows.Forms.RichTextBox()
        Me.lblTestEquipmentNote = New System.Windows.Forms.Label()
        Me.txtTestEquipmentLabIdentifier = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbTestEquipmentTestGroup = New System.Windows.Forms.GroupBox()
        Me.btnTestEquipmentTestGroupListMembers = New System.Windows.Forms.Button()
        Me.btnTestEquipmentTestGroupUpdate = New System.Windows.Forms.Button()
        Me.lblTestEquipmentTestGroupMembers = New System.Windows.Forms.Label()
        Me.txtTestEquipmentTestGroupMembers = New System.Windows.Forms.TextBox()
        Me.lbltestEquipmentType = New System.Windows.Forms.Label()
        Me.chkTestEquipmentIsTestGroup = New System.Windows.Forms.CheckBox()
        Me.txtTestEquipmentRev = New System.Windows.Forms.TextBox()
        Me.lblTestEquipmentRev = New System.Windows.Forms.Label()
        Me.chkTestEquipmentShowObsolete = New System.Windows.Forms.CheckBox()
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
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.panelTestEquipmentRecord.SuspendLayout()
        Me.gbTestEquipmentTestGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.panelTestEquipmentRecord, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.05882!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.94118!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1050, 535)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 286)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1044, 246)
        Me.DataGridView1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtTestEquipmentFilter)
        Me.Panel2.Controls.Add(Me.cbTestEquipmentTypeFilter)
        Me.Panel2.Controls.Add(Me.chkTestEquipmentShowInactiveRev)
        Me.Panel2.Controls.Add(Me.btnTestEquipmentRefresh)
        Me.Panel2.Controls.Add(Me.chkShowPrevious)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1044, 54)
        Me.Panel2.TabIndex = 3
        '
        'txtTestEquipmentFilter
        '
        Me.txtTestEquipmentFilter.AutoSize = True
        Me.txtTestEquipmentFilter.Location = New System.Drawing.Point(479, 11)
        Me.txtTestEquipmentFilter.Name = "txtTestEquipmentFilter"
        Me.txtTestEquipmentFilter.Size = New System.Drawing.Size(29, 13)
        Me.txtTestEquipmentFilter.TabIndex = 235
        Me.txtTestEquipmentFilter.Text = "Filter"
        '
        'cbTestEquipmentTypeFilter
        '
        Me.cbTestEquipmentTypeFilter.FormattingEnabled = True
        Me.cbTestEquipmentTypeFilter.Location = New System.Drawing.Point(514, 8)
        Me.cbTestEquipmentTypeFilter.Name = "cbTestEquipmentTypeFilter"
        Me.cbTestEquipmentTypeFilter.Size = New System.Drawing.Size(131, 21)
        Me.cbTestEquipmentTypeFilter.TabIndex = 236
        Me.cbTestEquipmentTypeFilter.Tag = "TEST_TYPE"
        '
        'chkTestEquipmentShowInactiveRev
        '
        Me.chkTestEquipmentShowInactiveRev.AutoSize = True
        Me.chkTestEquipmentShowInactiveRev.Location = New System.Drawing.Point(712, 22)
        Me.chkTestEquipmentShowInactiveRev.Name = "chkTestEquipmentShowInactiveRev"
        Me.chkTestEquipmentShowInactiveRev.Size = New System.Drawing.Size(94, 17)
        Me.chkTestEquipmentShowInactiveRev.TabIndex = 235
        Me.chkTestEquipmentShowInactiveRev.Text = "Show Inactive"
        Me.chkTestEquipmentShowInactiveRev.UseVisualStyleBackColor = True
        '
        'btnTestEquipmentRefresh
        '
        Me.btnTestEquipmentRefresh.Location = New System.Drawing.Point(414, 7)
        Me.btnTestEquipmentRefresh.Name = "btnTestEquipmentRefresh"
        Me.btnTestEquipmentRefresh.Size = New System.Drawing.Size(59, 22)
        Me.btnTestEquipmentRefresh.TabIndex = 2
        Me.btnTestEquipmentRefresh.Text = "Refresh"
        Me.btnTestEquipmentRefresh.UseVisualStyleBackColor = True
        '
        'chkShowPrevious
        '
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
        Me.TableLayoutPanel2.ColumnCount = 6
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.665!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.675!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.665!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.665!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.665!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.665!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnAddSelelected, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnSaveTestEquipmentRecord, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnAddTestEquipment, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnObsoleteTestEquipmentRecord, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnEditTestEquipmentRecord, 3, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(369, 26)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'btnAddSelelected
        '
        Me.btnAddSelelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddSelelected.Location = New System.Drawing.Point(3, 3)
        Me.btnAddSelelected.Name = "btnAddSelelected"
        Me.btnAddSelelected.Size = New System.Drawing.Size(55, 20)
        Me.btnAddSelelected.TabIndex = 0
        Me.btnAddSelelected.Text = "Select"
        Me.btnAddSelelected.UseVisualStyleBackColor = True
        '
        'btnSaveTestEquipmentRecord
        '
        Me.btnSaveTestEquipmentRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSaveTestEquipmentRecord.Location = New System.Drawing.Point(247, 3)
        Me.btnSaveTestEquipmentRecord.Name = "btnSaveTestEquipmentRecord"
        Me.btnSaveTestEquipmentRecord.Size = New System.Drawing.Size(55, 20)
        Me.btnSaveTestEquipmentRecord.TabIndex = 7
        Me.btnSaveTestEquipmentRecord.Text = "Save"
        Me.btnSaveTestEquipmentRecord.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(64, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(55, 20)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAddTestEquipment
        '
        Me.btnAddTestEquipment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddTestEquipment.Location = New System.Drawing.Point(125, 3)
        Me.btnAddTestEquipment.Name = "btnAddTestEquipment"
        Me.btnAddTestEquipment.Size = New System.Drawing.Size(55, 20)
        Me.btnAddTestEquipment.TabIndex = 3
        Me.btnAddTestEquipment.Text = "New"
        Me.btnAddTestEquipment.UseVisualStyleBackColor = True
        '
        'btnObsoleteTestEquipmentRecord
        '
        Me.btnObsoleteTestEquipmentRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnObsoleteTestEquipmentRecord.Location = New System.Drawing.Point(308, 3)
        Me.btnObsoleteTestEquipmentRecord.Name = "btnObsoleteTestEquipmentRecord"
        Me.btnObsoleteTestEquipmentRecord.Size = New System.Drawing.Size(58, 20)
        Me.btnObsoleteTestEquipmentRecord.TabIndex = 5
        Me.btnObsoleteTestEquipmentRecord.Text = "Obsolete"
        Me.btnObsoleteTestEquipmentRecord.UseVisualStyleBackColor = True
        '
        'btnEditTestEquipmentRecord
        '
        Me.btnEditTestEquipmentRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnEditTestEquipmentRecord.Location = New System.Drawing.Point(186, 3)
        Me.btnEditTestEquipmentRecord.Name = "btnEditTestEquipmentRecord"
        Me.btnEditTestEquipmentRecord.Size = New System.Drawing.Size(55, 20)
        Me.btnEditTestEquipmentRecord.TabIndex = 4
        Me.btnEditTestEquipmentRecord.Text = "Edit"
        Me.btnEditTestEquipmentRecord.UseVisualStyleBackColor = True
        '
        'panelTestEquipmentRecord
        '
        Me.panelTestEquipmentRecord.Controls.Add(Me.rcbEditModeTest)
        Me.panelTestEquipmentRecord.Controls.Add(Me.Button1)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.dtpTestEquipmentObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.lblObsoleteDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentNextCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.txtTestEquipmentLastCalDate)
        Me.panelTestEquipmentRecord.Controls.Add(Me.xocbTestEquipmentType)
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
        Me.panelTestEquipmentRecord.Controls.Add(Me.chkTestEquipmentShowObsolete)
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
        Me.panelTestEquipmentRecord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelTestEquipmentRecord.Location = New System.Drawing.Point(3, 63)
        Me.panelTestEquipmentRecord.Name = "panelTestEquipmentRecord"
        Me.panelTestEquipmentRecord.Size = New System.Drawing.Size(1044, 217)
        Me.panelTestEquipmentRecord.TabIndex = 4
        '
        'rcbEditModeTest
        '
        Me.rcbEditModeTest.FormattingEnabled = True
        Me.rcbEditModeTest.Location = New System.Drawing.Point(651, 137)
        Me.rcbEditModeTest.Name = "rcbEditModeTest"
        Me.rcbEditModeTest.Size = New System.Drawing.Size(155, 21)
        Me.rcbEditModeTest.TabIndex = 250
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(651, 170)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(59, 34)
        Me.Button1.TabIndex = 249
        Me.Button1.Text = "Change View"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentObsoleteDate
        '
        Me.txtTestEquipmentObsoleteDate.Location = New System.Drawing.Point(257, 113)
        Me.txtTestEquipmentObsoleteDate.Name = "txtTestEquipmentObsoleteDate"
        Me.txtTestEquipmentObsoleteDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentObsoleteDate.TabIndex = 248
        '
        'dtpTestEquipmentObsoleteDate
        '
        Me.dtpTestEquipmentObsoleteDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpTestEquipmentObsoleteDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipmentObsoleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipmentObsoleteDate.Location = New System.Drawing.Point(431, 113)
        Me.dtpTestEquipmentObsoleteDate.Name = "dtpTestEquipmentObsoleteDate"
        Me.dtpTestEquipmentObsoleteDate.RightToLeftLayout = True
        Me.dtpTestEquipmentObsoleteDate.Size = New System.Drawing.Size(15, 20)
        Me.dtpTestEquipmentObsoleteDate.TabIndex = 246
        Me.dtpTestEquipmentObsoleteDate.Tag = "Date Closed"
        '
        'lblObsoleteDate
        '
        Me.lblObsoleteDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblObsoleteDate.AutoSize = True
        Me.lblObsoleteDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObsoleteDate.Location = New System.Drawing.Point(343, 95)
        Me.lblObsoleteDate.Name = "lblObsoleteDate"
        Me.lblObsoleteDate.Size = New System.Drawing.Size(75, 13)
        Me.lblObsoleteDate.TabIndex = 247
        Me.lblObsoleteDate.Text = "ObsoleteDate:"
        Me.lblObsoleteDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTestEquipmentNextCalDate
        '
        Me.txtTestEquipmentNextCalDate.Location = New System.Drawing.Point(466, 32)
        Me.txtTestEquipmentNextCalDate.Name = "txtTestEquipmentNextCalDate"
        Me.txtTestEquipmentNextCalDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentNextCalDate.TabIndex = 245
        '
        'txtTestEquipmentLastCalDate
        '
        Me.txtTestEquipmentLastCalDate.Location = New System.Drawing.Point(466, 11)
        Me.txtTestEquipmentLastCalDate.Name = "txtTestEquipmentLastCalDate"
        Me.txtTestEquipmentLastCalDate.Size = New System.Drawing.Size(87, 20)
        Me.txtTestEquipmentLastCalDate.TabIndex = 244
        '
        'xocbTestEquipmentType
        '
        Me.xocbTestEquipmentType.FormattingEnabled = True
        Me.xocbTestEquipmentType.Location = New System.Drawing.Point(91, 113)
        Me.xocbTestEquipmentType.Name = "xocbTestEquipmentType"
        Me.xocbTestEquipmentType.Size = New System.Drawing.Size(155, 21)
        Me.xocbTestEquipmentType.TabIndex = 243
        '
        'btnTestReadOnly
        '
        Me.btnTestReadOnly.Location = New System.Drawing.Point(801, 170)
        Me.btnTestReadOnly.Name = "btnTestReadOnly"
        Me.btnTestReadOnly.Size = New System.Drawing.Size(59, 34)
        Me.btnTestReadOnly.TabIndex = 242
        Me.btnTestReadOnly.Text = "Read Only"
        Me.btnTestReadOnly.UseVisualStyleBackColor = True
        '
        'chkTestEquipmentCalReq
        '
        Me.chkTestEquipmentCalReq.AutoCheck = False
        Me.chkTestEquipmentCalReq.AutoSize = True
        Me.chkTestEquipmentCalReq.Location = New System.Drawing.Point(254, 32)
        Me.chkTestEquipmentCalReq.Name = "chkTestEquipmentCalReq"
        Me.chkTestEquipmentCalReq.Size = New System.Drawing.Size(127, 17)
        Me.chkTestEquipmentCalReq.TabIndex = 240
        Me.chkTestEquipmentCalReq.Text = "Calibration Required?"
        Me.chkTestEquipmentCalReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTestEquipmentCalReq.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentNote
        '
        Me.txtTestEquipmentNote.Location = New System.Drawing.Point(379, 137)
        Me.txtTestEquipmentNote.Name = "txtTestEquipmentNote"
        Me.txtTestEquipmentNote.Size = New System.Drawing.Size(254, 67)
        Me.txtTestEquipmentNote.TabIndex = 239
        Me.txtTestEquipmentNote.Text = ""
        '
        'lblTestEquipmentNote
        '
        Me.lblTestEquipmentNote.AutoSize = True
        Me.lblTestEquipmentNote.Location = New System.Drawing.Point(342, 140)
        Me.lblTestEquipmentNote.Name = "lblTestEquipmentNote"
        Me.lblTestEquipmentNote.Size = New System.Drawing.Size(35, 13)
        Me.lblTestEquipmentNote.TabIndex = 238
        Me.lblTestEquipmentNote.Text = "Notes"
        '
        'txtTestEquipmentLabIdentifier
        '
        Me.txtTestEquipmentLabIdentifier.Location = New System.Drawing.Point(91, 29)
        Me.txtTestEquipmentLabIdentifier.Name = "txtTestEquipmentLabIdentifier"
        Me.txtTestEquipmentLabIdentifier.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentLabIdentifier.TabIndex = 237
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 236
        Me.Label1.Text = "Lab ID"
        '
        'gbTestEquipmentTestGroup
        '
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.btnTestEquipmentTestGroupListMembers)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.btnTestEquipmentTestGroupUpdate)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.lblTestEquipmentTestGroupMembers)
        Me.gbTestEquipmentTestGroup.Controls.Add(Me.txtTestEquipmentTestGroupMembers)
        Me.gbTestEquipmentTestGroup.Location = New System.Drawing.Point(632, 8)
        Me.gbTestEquipmentTestGroup.Name = "gbTestEquipmentTestGroup"
        Me.gbTestEquipmentTestGroup.Size = New System.Drawing.Size(228, 93)
        Me.gbTestEquipmentTestGroup.TabIndex = 235
        Me.gbTestEquipmentTestGroup.TabStop = False
        Me.gbTestEquipmentTestGroup.Text = "Test Group"
        '
        'btnTestEquipmentTestGroupListMembers
        '
        Me.btnTestEquipmentTestGroupListMembers.Location = New System.Drawing.Point(100, 21)
        Me.btnTestEquipmentTestGroupListMembers.Name = "btnTestEquipmentTestGroupListMembers"
        Me.btnTestEquipmentTestGroupListMembers.Size = New System.Drawing.Size(83, 22)
        Me.btnTestEquipmentTestGroupListMembers.TabIndex = 238
        Me.btnTestEquipmentTestGroupListMembers.Text = "List Members"
        Me.btnTestEquipmentTestGroupListMembers.UseVisualStyleBackColor = True
        Me.btnTestEquipmentTestGroupListMembers.Visible = False
        '
        'btnTestEquipmentTestGroupUpdate
        '
        Me.btnTestEquipmentTestGroupUpdate.Location = New System.Drawing.Point(9, 21)
        Me.btnTestEquipmentTestGroupUpdate.Name = "btnTestEquipmentTestGroupUpdate"
        Me.btnTestEquipmentTestGroupUpdate.Size = New System.Drawing.Size(83, 22)
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
        Me.lbltestEquipmentType.AutoSize = True
        Me.lbltestEquipmentType.Location = New System.Drawing.Point(55, 116)
        Me.lbltestEquipmentType.Name = "lbltestEquipmentType"
        Me.lbltestEquipmentType.Size = New System.Drawing.Size(31, 13)
        Me.lbltestEquipmentType.TabIndex = 233
        Me.lbltestEquipmentType.Text = "Type"
        '
        'chkTestEquipmentIsTestGroup
        '
        Me.chkTestEquipmentIsTestGroup.AutoCheck = False
        Me.chkTestEquipmentIsTestGroup.AutoSize = True
        Me.chkTestEquipmentIsTestGroup.Location = New System.Drawing.Point(254, 51)
        Me.chkTestEquipmentIsTestGroup.Name = "chkTestEquipmentIsTestGroup"
        Me.chkTestEquipmentIsTestGroup.Size = New System.Drawing.Size(85, 17)
        Me.chkTestEquipmentIsTestGroup.TabIndex = 234
        Me.chkTestEquipmentIsTestGroup.Text = "Test Group?"
        Me.chkTestEquipmentIsTestGroup.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentRev
        '
        Me.txtTestEquipmentRev.Location = New System.Drawing.Point(178, 8)
        Me.txtTestEquipmentRev.Name = "txtTestEquipmentRev"
        Me.txtTestEquipmentRev.Size = New System.Drawing.Size(68, 20)
        Me.txtTestEquipmentRev.TabIndex = 231
        '
        'lblTestEquipmentRev
        '
        Me.lblTestEquipmentRev.AutoSize = True
        Me.lblTestEquipmentRev.Location = New System.Drawing.Point(149, 11)
        Me.lblTestEquipmentRev.Name = "lblTestEquipmentRev"
        Me.lblTestEquipmentRev.Size = New System.Drawing.Size(27, 13)
        Me.lblTestEquipmentRev.TabIndex = 230
        Me.lblTestEquipmentRev.Text = "Rev"
        '
        'chkTestEquipmentShowObsolete
        '
        Me.chkTestEquipmentShowObsolete.AutoCheck = False
        Me.chkTestEquipmentShowObsolete.AutoSize = True
        Me.chkTestEquipmentShowObsolete.Location = New System.Drawing.Point(254, 71)
        Me.chkTestEquipmentShowObsolete.Name = "chkTestEquipmentShowObsolete"
        Me.chkTestEquipmentShowObsolete.Size = New System.Drawing.Size(68, 17)
        Me.chkTestEquipmentShowObsolete.TabIndex = 229
        Me.chkTestEquipmentShowObsolete.Text = "Obsolete"
        Me.chkTestEquipmentShowObsolete.UseVisualStyleBackColor = True
        '
        'chkActiveRevision
        '
        Me.chkActiveRevision.AutoCheck = False
        Me.chkActiveRevision.AutoSize = True
        Me.chkActiveRevision.Location = New System.Drawing.Point(254, 11)
        Me.chkActiveRevision.Name = "chkActiveRevision"
        Me.chkActiveRevision.Size = New System.Drawing.Size(100, 17)
        Me.chkActiveRevision.TabIndex = 228
        Me.chkActiveRevision.Text = "Active Revision"
        Me.chkActiveRevision.UseVisualStyleBackColor = True
        '
        'txtTestEquipmentDescription
        '
        Me.txtTestEquipmentDescription.Location = New System.Drawing.Point(91, 137)
        Me.txtTestEquipmentDescription.Name = "txtTestEquipmentDescription"
        Me.txtTestEquipmentDescription.Size = New System.Drawing.Size(237, 67)
        Me.txtTestEquipmentDescription.TabIndex = 227
        Me.txtTestEquipmentDescription.Text = ""
        '
        'dtpTestEquipnextCalDate
        '
        Me.dtpTestEquipnextCalDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpTestEquipnextCalDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipnextCalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipnextCalDate.Location = New System.Drawing.Point(640, 32)
        Me.dtpTestEquipnextCalDate.Name = "dtpTestEquipnextCalDate"
        Me.dtpTestEquipnextCalDate.RightToLeftLayout = True
        Me.dtpTestEquipnextCalDate.Size = New System.Drawing.Size(15, 20)
        Me.dtpTestEquipnextCalDate.TabIndex = 223
        Me.dtpTestEquipnextCalDate.Tag = "Date Closed"
        '
        'lblTestEquipNextCalDate
        '
        Me.lblTestEquipNextCalDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTestEquipNextCalDate.AutoSize = True
        Me.lblTestEquipNextCalDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestEquipNextCalDate.Location = New System.Drawing.Point(478, 35)
        Me.lblTestEquipNextCalDate.Name = "lblTestEquipNextCalDate"
        Me.lblTestEquipNextCalDate.Size = New System.Drawing.Size(76, 13)
        Me.lblTestEquipNextCalDate.TabIndex = 224
        Me.lblTestEquipNextCalDate.Text = "Next Cal Date:"
        Me.lblTestEquipNextCalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTestEquipLastCalDate
        '
        Me.dtpTestEquipLastCalDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.dtpTestEquipLastCalDate.CustomFormat = " MM/dd/yyyy"
        Me.dtpTestEquipLastCalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTestEquipLastCalDate.Location = New System.Drawing.Point(640, 11)
        Me.dtpTestEquipLastCalDate.Name = "dtpTestEquipLastCalDate"
        Me.dtpTestEquipLastCalDate.RightToLeftLayout = True
        Me.dtpTestEquipLastCalDate.Size = New System.Drawing.Size(15, 20)
        Me.dtpTestEquipLastCalDate.TabIndex = 220
        Me.dtpTestEquipLastCalDate.Tag = "Date Closed"
        '
        'lblTestEquipLastCalDate
        '
        Me.lblTestEquipLastCalDate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTestEquipLastCalDate.AutoSize = True
        Me.lblTestEquipLastCalDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestEquipLastCalDate.Location = New System.Drawing.Point(478, 14)
        Me.lblTestEquipLastCalDate.Name = "lblTestEquipLastCalDate"
        Me.lblTestEquipLastCalDate.Size = New System.Drawing.Size(74, 13)
        Me.lblTestEquipLastCalDate.TabIndex = 221
        Me.lblTestEquipLastCalDate.Text = "Last Cal Date:"
        Me.lblTestEquipLastCalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestEquipmentDescription
        '
        Me.lblTestEquipmentDescription.AutoSize = True
        Me.lblTestEquipmentDescription.Location = New System.Drawing.Point(28, 137)
        Me.lblTestEquipmentDescription.Name = "lblTestEquipmentDescription"
        Me.lblTestEquipmentDescription.Size = New System.Drawing.Size(60, 13)
        Me.lblTestEquipmentDescription.TabIndex = 8
        Me.lblTestEquipmentDescription.Text = "Description"
        '
        'txttestEquipmentSerialNumber
        '
        Me.txttestEquipmentSerialNumber.Location = New System.Drawing.Point(91, 92)
        Me.txttestEquipmentSerialNumber.Name = "txttestEquipmentSerialNumber"
        Me.txttestEquipmentSerialNumber.Size = New System.Drawing.Size(155, 20)
        Me.txttestEquipmentSerialNumber.TabIndex = 7
        '
        'lbltestEquipmentSerialNumber
        '
        Me.lbltestEquipmentSerialNumber.AutoSize = True
        Me.lbltestEquipmentSerialNumber.Location = New System.Drawing.Point(16, 95)
        Me.lbltestEquipmentSerialNumber.Name = "lbltestEquipmentSerialNumber"
        Me.lbltestEquipmentSerialNumber.Size = New System.Drawing.Size(73, 13)
        Me.lbltestEquipmentSerialNumber.TabIndex = 6
        Me.lbltestEquipmentSerialNumber.Text = "Serial Number"
        '
        'txtTestEquipmentModel
        '
        Me.txtTestEquipmentModel.Location = New System.Drawing.Point(91, 71)
        Me.txtTestEquipmentModel.Name = "txtTestEquipmentModel"
        Me.txtTestEquipmentModel.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentModel.TabIndex = 5
        '
        'lblTestEquipmentModel
        '
        Me.lblTestEquipmentModel.AutoSize = True
        Me.lblTestEquipmentModel.Location = New System.Drawing.Point(54, 75)
        Me.lblTestEquipmentModel.Name = "lblTestEquipmentModel"
        Me.lblTestEquipmentModel.Size = New System.Drawing.Size(36, 13)
        Me.lblTestEquipmentModel.TabIndex = 4
        Me.lblTestEquipmentModel.Text = "Model"
        '
        'txtTestEquipmentManufacturer
        '
        Me.txtTestEquipmentManufacturer.Location = New System.Drawing.Point(91, 50)
        Me.txtTestEquipmentManufacturer.Name = "txtTestEquipmentManufacturer"
        Me.txtTestEquipmentManufacturer.Size = New System.Drawing.Size(155, 20)
        Me.txtTestEquipmentManufacturer.TabIndex = 3
        '
        'lblTestEquipmentManufacturer
        '
        Me.lblTestEquipmentManufacturer.AutoSize = True
        Me.lblTestEquipmentManufacturer.Location = New System.Drawing.Point(19, 53)
        Me.lblTestEquipmentManufacturer.Name = "lblTestEquipmentManufacturer"
        Me.lblTestEquipmentManufacturer.Size = New System.Drawing.Size(70, 13)
        Me.lblTestEquipmentManufacturer.TabIndex = 2
        Me.lblTestEquipmentManufacturer.Text = "Manufacturer"
        '
        'txtTestEquipmentID
        '
        Me.txtTestEquipmentID.Location = New System.Drawing.Point(91, 8)
        Me.txtTestEquipmentID.Name = "txtTestEquipmentID"
        Me.txtTestEquipmentID.Size = New System.Drawing.Size(51, 20)
        Me.txtTestEquipmentID.TabIndex = 1
        '
        'lblTestEquipmentID
        '
        Me.lblTestEquipmentID.AutoSize = True
        Me.lblTestEquipmentID.Location = New System.Drawing.Point(72, 11)
        Me.lblTestEquipmentID.Name = "lblTestEquipmentID"
        Me.lblTestEquipmentID.Size = New System.Drawing.Size(18, 13)
        Me.lblTestEquipmentID.TabIndex = 0
        Me.lblTestEquipmentID.Text = "ID"
        '
        'frmManageTestEquipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 535)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmManageTestEquipment"
        Me.Text = "frmManageTestEquipment"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.panelTestEquipmentRecord.ResumeLayout(False)
        Me.panelTestEquipmentRecord.PerformLayout()
        Me.gbTestEquipmentTestGroup.ResumeLayout(False)
        Me.gbTestEquipmentTestGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtTestEquipmentFilter As System.Windows.Forms.Label
    Friend WithEvents cbTestEquipmentTypeFilter As System.Windows.Forms.ComboBox
    Friend WithEvents chkTestEquipmentShowInactiveRev As System.Windows.Forms.CheckBox
    Friend WithEvents btnTestEquipmentRefresh As System.Windows.Forms.Button
    Friend WithEvents chkShowPrevious As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnAddSelelected As System.Windows.Forms.Button
    Friend WithEvents btnSaveTestEquipmentRecord As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddTestEquipment As System.Windows.Forms.Button
    Friend WithEvents btnObsoleteTestEquipmentRecord As System.Windows.Forms.Button
    Friend WithEvents btnEditTestEquipmentRecord As System.Windows.Forms.Button
    Friend WithEvents panelTestEquipmentRecord As System.Windows.Forms.Panel
    Friend WithEvents rcbEditModeTest As ReadOnlyComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtTestEquipmentObsoleteDate As System.Windows.Forms.TextBox
    Friend WithEvents dtpTestEquipmentObsoleteDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblObsoleteDate As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentNextCalDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestEquipmentLastCalDate As System.Windows.Forms.TextBox
    Friend WithEvents xocbTestEquipmentType As ReadOnlyComboBox
    Friend WithEvents btnTestReadOnly As System.Windows.Forms.Button
    Friend WithEvents chkTestEquipmentCalReq As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestEquipmentNote As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTestEquipmentNote As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentLabIdentifier As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbTestEquipmentTestGroup As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestEquipmentTestGroupListMembers As System.Windows.Forms.Button
    Friend WithEvents btnTestEquipmentTestGroupUpdate As System.Windows.Forms.Button
    Friend WithEvents lblTestEquipmentTestGroupMembers As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentTestGroupMembers As System.Windows.Forms.TextBox
    Friend WithEvents lbltestEquipmentType As System.Windows.Forms.Label
    Friend WithEvents chkTestEquipmentIsTestGroup As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestEquipmentRev As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentRev As System.Windows.Forms.Label
    Friend WithEvents chkTestEquipmentShowObsolete As System.Windows.Forms.CheckBox
    Friend WithEvents chkActiveRevision As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestEquipmentDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents dtpTestEquipnextCalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTestEquipNextCalDate As System.Windows.Forms.Label
    Friend WithEvents dtpTestEquipLastCalDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTestEquipLastCalDate As System.Windows.Forms.Label
    Friend WithEvents lblTestEquipmentDescription As System.Windows.Forms.Label
    Friend WithEvents txttestEquipmentSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lbltestEquipmentSerialNumber As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentModel As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentModel As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentManufacturer As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentManufacturer As System.Windows.Forms.Label
    Friend WithEvents txtTestEquipmentID As System.Windows.Forms.TextBox
    Friend WithEvents lblTestEquipmentID As System.Windows.Forms.Label
End Class
