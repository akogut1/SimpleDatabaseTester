

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilter
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFilter))
        Me.chkFilterPAC1toApprove = New System.Windows.Forms.CheckBox()
        Me.dtpFilterClosedTo = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpFilterClosedFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpFilterApprovedTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFilterApprovedFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkPAC2ToApprove = New System.Windows.Forms.CheckBox()
        Me.cbFilterTests = New xboXComboBox()
        Me.cbFilterLevel = New xboXComboBox()
        Me.btnFilterReset = New System.Windows.Forms.Button()
        Me.btnFilterApply = New System.Windows.Forms.Button()
        Me.dtpFilterFailedTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTestName = New System.Windows.Forms.Label()
        Me.dtpFilterFailedFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterLevel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFilterNewIDFrom = New System.Windows.Forms.TextBox()
        Me.lblFilterReportNumber = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBoxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseFRHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseFRHistoryNoFilterToolStripMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseDefaultValuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManageDatagridviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.rbReportCatAll = New System.Windows.Forms.RadioButton()
        Me.rbFilterAnomely = New System.Windows.Forms.RadioButton()
        Me.rbFilterFailure = New System.Windows.Forms.RadioButton()
        Me.gbFilterReportCatagory = New System.Windows.Forms.GroupBox()
        Me.txtFilterNewIDTo = New System.Windows.Forms.TextBox()
        Me.lblFilterNewIDTo = New System.Windows.Forms.Label()
        Me.cbFilterAssignedTo = New xboXComboBox()
        Me.lblFilterProjectLead = New System.Windows.Forms.Label()
        Me.cbFilterCorrectedBy = New xboXComboBox()
        Me.lblFilterCorrectedBy = New System.Windows.Forms.Label()
        Me.btnFilterFailedDatesClear = New System.Windows.Forms.Button()
        Me.btnFilterApprovedDatesClear = New System.Windows.Forms.Button()
        Me.btnFilterClosedDatesClear = New System.Windows.Forms.Button()
        Me.cbFilterProjectNumber = New xboXComboBox()
        Me.lblFilterProjectNumber = New System.Windows.Forms.Label()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.cbFilterProjectName = New xboXComboBox()
        Me.chkFilterOpenReportsOnly = New System.Windows.Forms.CheckBox()
        Me.gbShowReportsToBeApproved = New System.Windows.Forms.GroupBox()
        Me.rbDisableReadyForReviewFilter = New System.Windows.Forms.RadioButton()
        Me.rbSelectReadyForReview = New System.Windows.Forms.RadioButton()
        Me.rbReviewAllREadyForReview = New System.Windows.Forms.RadioButton()
        Me.chkOEMToApprove = New System.Windows.Forms.CheckBox()
        Me.chkEngineeringToApporve = New System.Windows.Forms.CheckBox()
        Me.ChkCITtoApprove = New System.Windows.Forms.CheckBox()
        Me.chkFPAToApprove = New System.Windows.Forms.CheckBox()
        Me.cbMeterSubTypeII = New xboXComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbMeterVoltage = New xboXComboBox()
        Me.lblMeterVoltage = New System.Windows.Forms.Label()
        Me.cbMeterBase = New xboXComboBox()
        Me.cbMeterPCBA = New xboXComboBox()
        Me.lblMeterPCBA_PN = New System.Windows.Forms.Label()
        Me.lblMeterSerialNumber = New System.Windows.Forms.Label()
        Me.cbMeterSerialNumber = New xboXComboBox()
        Me.cbMeterSoftwareRev = New xboXComboBox()
        Me.lblMeterSoftwareRev = New System.Windows.Forms.Label()
        Me.cbMeterSoftware = New xboXComboBox()
        Me.lblMeterSoftware = New System.Windows.Forms.Label()
        Me.cbMeterPCBA_Rev = New xboXComboBox()
        Me.lblMeterPCBA_Rev = New System.Windows.Forms.Label()
        Me.lblMeterFW_Ver = New System.Windows.Forms.Label()
        Me.cbMeterDSP_Rev = New xboXComboBox()
        Me.lblMeter_DSP_Rev = New System.Windows.Forms.Label()
        Me.cbMeterSubType = New xboXComboBox()
        Me.lblMeterSubType = New System.Windows.Forms.Label()
        Me.cbMeterType = New xboXComboBox()
        Me.lblMeterType = New System.Windows.Forms.Label()
        Me.lblMeterModel = New System.Windows.Forms.Label()
        Me.cbMeterManufacturer = New xboXComboBox()
        Me.lblMeterManufacturer = New System.Windows.Forms.Label()
        Me.lblForm = New System.Windows.Forms.Label()
        Me.cbMeterFW_Ver = New xboXComboBox()
        Me.cbMeterModel = New xboXComboBox()
        Me.cbMeterForm = New xboXComboBox()
        Me.lblAMR_SUBtypeIII = New System.Windows.Forms.Label()
        Me.cbAMR_SUBtypeIII = New xboXComboBox()
        Me.cbAMR_SUBtypeII = New xboXComboBox()
        Me.lblAMR_SubTypeII = New System.Windows.Forms.Label()
        Me.cbAMR_Voltage = New xboXComboBox()
        Me.lblAMRVoltage = New System.Windows.Forms.Label()
        Me.cbAMR_PCBA_PN = New xboXComboBox()
        Me.lblAMR_PCBA = New System.Windows.Forms.Label()
        Me.lblAMR_SerialNumber = New System.Windows.Forms.Label()
        Me.cbAMR_SerialNumber = New xboXComboBox()
        Me.cbAMR_Software_Rev = New xboXComboBox()
        Me.lblAMR_Software_Rev = New System.Windows.Forms.Label()
        Me.cbAMR_Software = New xboXComboBox()
        Me.lblAMR_Software = New System.Windows.Forms.Label()
        Me.cbAMR_PCBA_Rev = New xboXComboBox()
        Me.lblAMR_PCBA_Rev = New System.Windows.Forms.Label()
        Me.lblAMR_FW_REV = New System.Windows.Forms.Label()
        Me.cbAMR_IP_LAN_ID = New xboXComboBox()
        Me.lblAMR_IP_LAN_ID = New System.Windows.Forms.Label()
        Me.cbAMR_SubType = New xboXComboBox()
        Me.lblAMR_SubType = New System.Windows.Forms.Label()
        Me.cbAMR_TYPE = New xboXComboBox()
        Me.lblAMR_Type = New System.Windows.Forms.Label()
        Me.lblAMR_MODEL = New System.Windows.Forms.Label()
        Me.cbAMR_Manufacturer = New xboXComboBox()
        Me.lblAMR_Manufacturer = New System.Windows.Forms.Label()
        Me.cbAMR_Model = New xboXComboBox()
        Me.cbAMR_FW_Rev = New xboXComboBox()
        Me.gbFilterEUT_1 = New System.Windows.Forms.GroupBox()
        Me.chkMeterVoltageAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterSoftwareRevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterSoftwareAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterPCBA_RevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterPCBAAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterFW_VerAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterDSP_RevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterFormAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterSerialNumberAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterSubTypeIIAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterSubTypeAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterTypeAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterModelAndOr = New System.Windows.Forms.CheckBox()
        Me.chkMeterManufacturerAndOr = New System.Windows.Forms.CheckBox()
        Me.gbFilterEUT2 = New System.Windows.Forms.GroupBox()
        Me.chkAMR_VoltageAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_Software_RevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_SoftwareAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_PCBA_RevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_PCBA_PNAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_FW_RevAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_IP_LAN_IDAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_SerialNumberAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_SUBtypeIIIAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_SubTypeIIAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_SubTypeAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_TYPEAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_ModelAndOr = New System.Windows.Forms.CheckBox()
        Me.chkAMR_ManufacturerAndOr = New System.Windows.Forms.CheckBox()
        Me.lblEUTType = New System.Windows.Forms.Label()
        Me.cbFilterEUTType = New xboXComboBox()
        Me.btnFilterClear = New System.Windows.Forms.Button()
        Me.btnFilterCorrectedDatesCleared = New System.Windows.Forms.Button()
        Me.dtpFilterCorrectedTo = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterCorrectedTo = New System.Windows.Forms.Label()
        Me.dtpFilterCorrectedFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterCorrectedFrom = New System.Windows.Forms.Label()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.cbApprovedBy = New xboXComboBox()
        Me.chkTestLevelAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterEUTTypeAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterCorrectedByAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterProjectNameAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterAssignedToAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterProjectNumberAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilteredFailedFromAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterCorrectedFromAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterClosedFromAndOr = New System.Windows.Forms.CheckBox()
        Me.chkApprovedByAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterApprovedFromAndOr = New System.Windows.Forms.CheckBox()
        Me.chkFilterTestsAndOr = New System.Windows.Forms.CheckBox()
        Me.cbFilterTestType = New xboXComboBox()
        Me.lblTestType = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkFilterTestTypeAndOr = New System.Windows.Forms.CheckBox()
        Me.chkPreserveFilter = New System.Windows.Forms.CheckBox()
        Me.chkFilterAllowAdvancedEditing = New System.Windows.Forms.CheckBox()
        Me.btnFilterBuild = New System.Windows.Forms.Button()
        Me.BtnFilterRemove = New System.Windows.Forms.Button()
        Me.ChkPreserveFilterAndOr = New System.Windows.Forms.CheckBox()
        Me.txtFilterDisplay = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gbFilterControls = New System.Windows.Forms.GroupBox()
        Me.chkTCCReviewRequired = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvFilterFailureReport = New System.Windows.Forms.DataGridView()
        Me.chkFilterTransferedReportsOnly = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1.SuspendLayout()
        Me.gbFilterReportCatagory.SuspendLayout()
        Me.gbShowReportsToBeApproved.SuspendLayout()
        Me.gbFilterEUT_1.SuspendLayout()
        Me.gbFilterEUT2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gbFilterControls.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.dgvFilterFailureReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkFilterPAC1toApprove
        '
        Me.chkFilterPAC1toApprove.AutoSize = True
        Me.chkFilterPAC1toApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterPAC1toApprove.Location = New System.Drawing.Point(7, 47)
        Me.chkFilterPAC1toApprove.Name = "chkFilterPAC1toApprove"
        Me.chkFilterPAC1toApprove.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkFilterPAC1toApprove.Size = New System.Drawing.Size(64, 20)
        Me.chkFilterPAC1toApprove.TabIndex = 146
        Me.chkFilterPAC1toApprove.Text = "PAC 1"
        Me.chkFilterPAC1toApprove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFilterPAC1toApprove.UseVisualStyleBackColor = True
        '
        'dtpFilterClosedTo
        '
        Me.dtpFilterClosedTo.CustomFormat = " "
        Me.dtpFilterClosedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterClosedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterClosedTo.Location = New System.Drawing.Point(275, 410)
        Me.dtpFilterClosedTo.Name = "dtpFilterClosedTo"
        Me.dtpFilterClosedTo.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterClosedTo.TabIndex = 145
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(243, 410)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 16)
        Me.Label15.TabIndex = 144
        Me.Label15.Text = "To:"
        '
        'dtpFilterClosedFrom
        '
        Me.dtpFilterClosedFrom.CustomFormat = " "
        Me.dtpFilterClosedFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterClosedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterClosedFrom.Location = New System.Drawing.Point(145, 410)
        Me.dtpFilterClosedFrom.Name = "dtpFilterClosedFrom"
        Me.dtpFilterClosedFrom.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterClosedFrom.TabIndex = 143
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(23, 412)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 16)
        Me.Label14.TabIndex = 142
        Me.Label14.Text = "Date Closed From:"
        '
        'dtpFilterApprovedTo
        '
        Me.dtpFilterApprovedTo.CustomFormat = " "
        Me.dtpFilterApprovedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterApprovedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterApprovedTo.Location = New System.Drawing.Point(275, 432)
        Me.dtpFilterApprovedTo.Name = "dtpFilterApprovedTo"
        Me.dtpFilterApprovedTo.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterApprovedTo.TabIndex = 141
        '
        'dtpFilterApprovedFrom
        '
        Me.dtpFilterApprovedFrom.CustomFormat = " "
        Me.dtpFilterApprovedFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterApprovedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterApprovedFrom.Location = New System.Drawing.Point(145, 432)
        Me.dtpFilterApprovedFrom.Name = "dtpFilterApprovedFrom"
        Me.dtpFilterApprovedFrom.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterApprovedFrom.TabIndex = 140
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(243, 435)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 16)
        Me.Label12.TabIndex = 139
        Me.Label12.Text = "To:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 434)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(137, 16)
        Me.Label13.TabIndex = 138
        Me.Label13.Text = "Date Approved From:"
        '
        'chkPAC2ToApprove
        '
        Me.chkPAC2ToApprove.AutoSize = True
        Me.chkPAC2ToApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPAC2ToApprove.Location = New System.Drawing.Point(7, 68)
        Me.chkPAC2ToApprove.Name = "chkPAC2ToApprove"
        Me.chkPAC2ToApprove.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPAC2ToApprove.Size = New System.Drawing.Size(64, 20)
        Me.chkPAC2ToApprove.TabIndex = 137
        Me.chkPAC2ToApprove.Text = "PAC 2"
        Me.chkPAC2ToApprove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkPAC2ToApprove.UseVisualStyleBackColor = True
        '
        'cbFilterTests
        '
        Me.cbFilterTests.DisplayMember = "TESTS"
        Me.cbFilterTests.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterTests.Location = New System.Drawing.Point(152, 50)
        Me.cbFilterTests.Name = "cbFilterTests"
        Me.cbFilterTests.Size = New System.Drawing.Size(548, 23)
        Me.cbFilterTests.TabIndex = 134
        Me.cbFilterTests.Tag = "Test"
        Me.ToolTip1.SetToolTip(Me.cbFilterTests, "Name of Test")
        '
        'cbFilterLevel
        '
        Me.cbFilterLevel.DisplayMember = "LEVEL"
        Me.cbFilterLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterLevel.Location = New System.Drawing.Point(609, 27)
        Me.cbFilterLevel.Name = "cbFilterLevel"
        Me.cbFilterLevel.Size = New System.Drawing.Size(90, 21)
        Me.cbFilterLevel.TabIndex = 132
        Me.cbFilterLevel.Tag = "Level"
        Me.ToolTip1.SetToolTip(Me.cbFilterLevel, "Test Level, PAC1 PAC2 CIT etc,,,")
        '
        'btnFilterReset
        '
        Me.btnFilterReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnFilterReset.Location = New System.Drawing.Point(206, 71)
        Me.btnFilterReset.Name = "btnFilterReset"
        Me.btnFilterReset.Size = New System.Drawing.Size(52, 34)
        Me.btnFilterReset.TabIndex = 127
        Me.btnFilterReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.btnFilterReset, "Reset Filter and Selections")
        Me.btnFilterReset.UseVisualStyleBackColor = True
        '
        'btnFilterApply
        '
        Me.btnFilterApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnFilterApply.Location = New System.Drawing.Point(96, 71)
        Me.btnFilterApply.Name = "btnFilterApply"
        Me.btnFilterApply.Size = New System.Drawing.Size(53, 34)
        Me.btnFilterApply.TabIndex = 126
        Me.btnFilterApply.Text = "Apply"
        Me.ToolTip1.SetToolTip(Me.btnFilterApply, "Apply the Filter to the Selection")
        Me.btnFilterApply.UseVisualStyleBackColor = True
        '
        'dtpFilterFailedTo
        '
        Me.dtpFilterFailedTo.CustomFormat = " "
        Me.dtpFilterFailedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterFailedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterFailedTo.Location = New System.Drawing.Point(275, 365)
        Me.dtpFilterFailedTo.Name = "dtpFilterFailedTo"
        Me.dtpFilterFailedTo.Size = New System.Drawing.Size(92, 22)
        Me.dtpFilterFailedTo.TabIndex = 124
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(243, 368)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 16)
        Me.Label3.TabIndex = 123
        Me.Label3.Text = "To:"
        '
        'lblTestName
        '
        Me.lblTestName.AutoSize = True
        Me.lblTestName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestName.Location = New System.Drawing.Point(72, 55)
        Me.lblTestName.Name = "lblTestName"
        Me.lblTestName.Size = New System.Drawing.Size(78, 16)
        Me.lblTestName.TabIndex = 121
        Me.lblTestName.Text = "Test Name:"
        '
        'dtpFilterFailedFrom
        '
        Me.dtpFilterFailedFrom.CustomFormat = " "
        Me.dtpFilterFailedFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterFailedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterFailedFrom.Location = New System.Drawing.Point(145, 365)
        Me.dtpFilterFailedFrom.Name = "dtpFilterFailedFrom"
        Me.dtpFilterFailedFrom.Size = New System.Drawing.Size(92, 22)
        Me.dtpFilterFailedFrom.TabIndex = 118
        '
        'lblFilterLevel
        '
        Me.lblFilterLevel.AutoSize = True
        Me.lblFilterLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterLevel.Location = New System.Drawing.Point(532, 31)
        Me.lblFilterLevel.Name = "lblFilterLevel"
        Me.lblFilterLevel.Size = New System.Drawing.Size(74, 16)
        Me.lblFilterLevel.TabIndex = 114
        Me.lblFilterLevel.Tag = "Level"
        Me.lblFilterLevel.Text = "Test Level:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 369)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 16)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Date Failed From:"
        '
        'txtFilterNewIDFrom
        '
        Me.txtFilterNewIDFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterNewIDFrom.Location = New System.Drawing.Point(152, 6)
        Me.txtFilterNewIDFrom.Name = "txtFilterNewIDFrom"
        Me.txtFilterNewIDFrom.Size = New System.Drawing.Size(74, 20)
        Me.txtFilterNewIDFrom.TabIndex = 112
        Me.txtFilterNewIDFrom.Tag = "New ID"
        Me.ToolTip1.SetToolTip(Me.txtFilterNewIDFrom, "Lower FR Limit")
        '
        'lblFilterReportNumber
        '
        Me.lblFilterReportNumber.AutoSize = True
        Me.lblFilterReportNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterReportNumber.Location = New System.Drawing.Point(14, 9)
        Me.lblFilterReportNumber.Name = "lblFilterReportNumber"
        Me.lblFilterReportNumber.Size = New System.Drawing.Size(137, 16)
        Me.lblFilterReportNumber.TabIndex = 111
        Me.lblFilterReportNumber.Text = "Report Number From:"
        Me.ToolTip1.SetToolTip(Me.lblFilterReportNumber, "Filter on a range of FR numbers")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem, Me.HelpToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1008, 24)
        Me.MenuStrip1.TabIndex = 125
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(124, 20)
        Me.CloseToolStripMenuItem.Text = "Close Filter Window"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ComboBoxToolStripMenuItem, Me.ManageDatagridviewToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ComboBoxToolStripMenuItem
        '
        Me.ComboBoxToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UseFRHistoryToolStripMenuItem, Me.UseFRHistoryNoFilterToolStripMenu, Me.UseDefaultValuesToolStripMenuItem})
        Me.ComboBoxToolStripMenuItem.Name = "ComboBoxToolStripMenuItem"
        Me.ComboBoxToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ComboBoxToolStripMenuItem.Text = "ComboBox "
        '
        'UseFRHistoryToolStripMenuItem
        '
        Me.UseFRHistoryToolStripMenuItem.Checked = True
        Me.UseFRHistoryToolStripMenuItem.CheckOnClick = True
        Me.UseFRHistoryToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.UseFRHistoryToolStripMenuItem.Name = "UseFRHistoryToolStripMenuItem"
        Me.UseFRHistoryToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.UseFRHistoryToolStripMenuItem.Text = "Use FR History - Filter"
        '
        'UseFRHistoryNoFilterToolStripMenu
        '
        Me.UseFRHistoryNoFilterToolStripMenu.CheckOnClick = True
        Me.UseFRHistoryNoFilterToolStripMenu.Name = "UseFRHistoryNoFilterToolStripMenu"
        Me.UseFRHistoryNoFilterToolStripMenu.Size = New System.Drawing.Size(226, 22)
        Me.UseFRHistoryNoFilterToolStripMenu.Text = "Use FR History - No Filter"
        '
        'UseDefaultValuesToolStripMenuItem
        '
        Me.UseDefaultValuesToolStripMenuItem.CheckOnClick = True
        Me.UseDefaultValuesToolStripMenuItem.Enabled = False
        Me.UseDefaultValuesToolStripMenuItem.Name = "UseDefaultValuesToolStripMenuItem"
        Me.UseDefaultValuesToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.UseDefaultValuesToolStripMenuItem.Text = "Use Default Values - No Filter"
        Me.UseDefaultValuesToolStripMenuItem.Visible = False
        '
        'ManageDatagridviewToolStripMenuItem
        '
        Me.ManageDatagridviewToolStripMenuItem.Name = "ManageDatagridviewToolStripMenuItem"
        Me.ManageDatagridviewToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ManageDatagridviewToolStripMenuItem.Text = "Manage Datagridview"
        '
        'rbReportCatAll
        '
        Me.rbReportCatAll.AutoSize = True
        Me.rbReportCatAll.Checked = True
        Me.rbReportCatAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbReportCatAll.Location = New System.Drawing.Point(9, 17)
        Me.rbReportCatAll.Name = "rbReportCatAll"
        Me.rbReportCatAll.Size = New System.Drawing.Size(41, 20)
        Me.rbReportCatAll.TabIndex = 147
        Me.rbReportCatAll.TabStop = True
        Me.rbReportCatAll.Tag = "ALL"
        Me.rbReportCatAll.Text = "All"
        Me.rbReportCatAll.UseVisualStyleBackColor = True
        '
        'rbFilterAnomely
        '
        Me.rbFilterAnomely.AutoSize = True
        Me.rbFilterAnomely.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterAnomely.Location = New System.Drawing.Point(56, 17)
        Me.rbFilterAnomely.Name = "rbFilterAnomely"
        Me.rbFilterAnomely.Size = New System.Drawing.Size(79, 20)
        Me.rbFilterAnomely.TabIndex = 149
        Me.rbFilterAnomely.Tag = "Anomaly"
        Me.rbFilterAnomely.Text = "Anomaly"
        Me.rbFilterAnomely.UseVisualStyleBackColor = True
        '
        'rbFilterFailure
        '
        Me.rbFilterFailure.AutoSize = True
        Me.rbFilterFailure.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterFailure.Location = New System.Drawing.Point(133, 17)
        Me.rbFilterFailure.Name = "rbFilterFailure"
        Me.rbFilterFailure.Size = New System.Drawing.Size(67, 20)
        Me.rbFilterFailure.TabIndex = 150
        Me.rbFilterFailure.Tag = "Failure"
        Me.rbFilterFailure.Text = "Failure"
        Me.rbFilterFailure.UseVisualStyleBackColor = True
        '
        'gbFilterReportCatagory
        '
        Me.gbFilterReportCatagory.Controls.Add(Me.rbReportCatAll)
        Me.gbFilterReportCatagory.Controls.Add(Me.rbFilterFailure)
        Me.gbFilterReportCatagory.Controls.Add(Me.rbFilterAnomely)
        Me.gbFilterReportCatagory.Location = New System.Drawing.Point(766, 1)
        Me.gbFilterReportCatagory.Name = "gbFilterReportCatagory"
        Me.gbFilterReportCatagory.Size = New System.Drawing.Size(235, 45)
        Me.gbFilterReportCatagory.TabIndex = 151
        Me.gbFilterReportCatagory.TabStop = False
        Me.gbFilterReportCatagory.Text = "Report Catagory"
        '
        'txtFilterNewIDTo
        '
        Me.txtFilterNewIDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterNewIDTo.Location = New System.Drawing.Point(254, 6)
        Me.txtFilterNewIDTo.Name = "txtFilterNewIDTo"
        Me.txtFilterNewIDTo.Size = New System.Drawing.Size(74, 20)
        Me.txtFilterNewIDTo.TabIndex = 152
        Me.txtFilterNewIDTo.Tag = "New ID"
        Me.ToolTip1.SetToolTip(Me.txtFilterNewIDTo, "Upper FR Limit")
        '
        'lblFilterNewIDTo
        '
        Me.lblFilterNewIDTo.AutoSize = True
        Me.lblFilterNewIDTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterNewIDTo.Location = New System.Drawing.Point(228, 8)
        Me.lblFilterNewIDTo.Name = "lblFilterNewIDTo"
        Me.lblFilterNewIDTo.Size = New System.Drawing.Size(28, 16)
        Me.lblFilterNewIDTo.TabIndex = 153
        Me.lblFilterNewIDTo.Text = "To:"
        '
        'cbFilterAssignedTo
        '
        Me.cbFilterAssignedTo.DisplayMember = "AMR"
        Me.cbFilterAssignedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterAssignedTo.Location = New System.Drawing.Point(152, 98)
        Me.cbFilterAssignedTo.Name = "cbFilterAssignedTo"
        Me.cbFilterAssignedTo.Size = New System.Drawing.Size(164, 21)
        Me.cbFilterAssignedTo.TabIndex = 155
        Me.cbFilterAssignedTo.Tag = "Assigned To"
        Me.ToolTip1.SetToolTip(Me.cbFilterAssignedTo, "Select Project Lead")
        '
        'lblFilterProjectLead
        '
        Me.lblFilterProjectLead.AutoSize = True
        Me.lblFilterProjectLead.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblFilterProjectLead.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterProjectLead.Location = New System.Drawing.Point(62, 102)
        Me.lblFilterProjectLead.Name = "lblFilterProjectLead"
        Me.lblFilterProjectLead.Size = New System.Drawing.Size(87, 16)
        Me.lblFilterProjectLead.TabIndex = 154
        Me.lblFilterProjectLead.Text = "Project Lead:"
        '
        'cbFilterCorrectedBy
        '
        Me.cbFilterCorrectedBy.DisplayMember = "AMR"
        Me.cbFilterCorrectedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterCorrectedBy.Location = New System.Drawing.Point(536, 98)
        Me.cbFilterCorrectedBy.Name = "cbFilterCorrectedBy"
        Me.cbFilterCorrectedBy.Size = New System.Drawing.Size(165, 21)
        Me.cbFilterCorrectedBy.TabIndex = 157
        Me.cbFilterCorrectedBy.Tag = "Corrected By"
        '
        'lblFilterCorrectedBy
        '
        Me.lblFilterCorrectedBy.AutoSize = True
        Me.lblFilterCorrectedBy.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblFilterCorrectedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterCorrectedBy.Location = New System.Drawing.Point(445, 101)
        Me.lblFilterCorrectedBy.Name = "lblFilterCorrectedBy"
        Me.lblFilterCorrectedBy.Size = New System.Drawing.Size(89, 16)
        Me.lblFilterCorrectedBy.TabIndex = 156
        Me.lblFilterCorrectedBy.Text = "Corrected By:"
        '
        'btnFilterFailedDatesClear
        '
        Me.btnFilterFailedDatesClear.Location = New System.Drawing.Point(373, 366)
        Me.btnFilterFailedDatesClear.Name = "btnFilterFailedDatesClear"
        Me.btnFilterFailedDatesClear.Size = New System.Drawing.Size(57, 22)
        Me.btnFilterFailedDatesClear.TabIndex = 159
        Me.btnFilterFailedDatesClear.Text = "<<Clear"
        Me.btnFilterFailedDatesClear.UseVisualStyleBackColor = True
        '
        'btnFilterApprovedDatesClear
        '
        Me.btnFilterApprovedDatesClear.Location = New System.Drawing.Point(373, 432)
        Me.btnFilterApprovedDatesClear.Name = "btnFilterApprovedDatesClear"
        Me.btnFilterApprovedDatesClear.Size = New System.Drawing.Size(57, 22)
        Me.btnFilterApprovedDatesClear.TabIndex = 160
        Me.btnFilterApprovedDatesClear.Text = "<<Clear"
        Me.btnFilterApprovedDatesClear.UseVisualStyleBackColor = True
        '
        'btnFilterClosedDatesClear
        '
        Me.btnFilterClosedDatesClear.Location = New System.Drawing.Point(373, 410)
        Me.btnFilterClosedDatesClear.Name = "btnFilterClosedDatesClear"
        Me.btnFilterClosedDatesClear.Size = New System.Drawing.Size(57, 22)
        Me.btnFilterClosedDatesClear.TabIndex = 161
        Me.btnFilterClosedDatesClear.Text = "<<Clear"
        Me.btnFilterClosedDatesClear.UseVisualStyleBackColor = True
        '
        'cbFilterProjectNumber
        '
        Me.cbFilterProjectNumber.DisplayMember = "AMR"
        Me.cbFilterProjectNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterProjectNumber.Location = New System.Drawing.Point(152, 75)
        Me.cbFilterProjectNumber.Name = "cbFilterProjectNumber"
        Me.cbFilterProjectNumber.Size = New System.Drawing.Size(164, 21)
        Me.cbFilterProjectNumber.TabIndex = 163
        Me.cbFilterProjectNumber.Tag = "Project_Number"
        Me.ToolTip1.SetToolTip(Me.cbFilterProjectNumber, "Select the Project Number")
        '
        'lblFilterProjectNumber
        '
        Me.lblFilterProjectNumber.AutoSize = True
        Me.lblFilterProjectNumber.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblFilterProjectNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterProjectNumber.Location = New System.Drawing.Point(86, 79)
        Me.lblFilterProjectNumber.Name = "lblFilterProjectNumber"
        Me.lblFilterProjectNumber.Size = New System.Drawing.Size(63, 16)
        Me.lblFilterProjectNumber.TabIndex = 162
        Me.lblFilterProjectNumber.Text = "Project #:"
        '
        'lblProjectName
        '
        Me.lblProjectName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectName.Location = New System.Drawing.Point(445, 79)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(93, 16)
        Me.lblProjectName.TabIndex = 209
        Me.lblProjectName.Text = "Project Name:"
        '
        'cbFilterProjectName
        '
        Me.cbFilterProjectName.DisplayMember = "AMR"
        Me.cbFilterProjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterProjectName.Location = New System.Drawing.Point(537, 75)
        Me.cbFilterProjectName.Name = "cbFilterProjectName"
        Me.cbFilterProjectName.Size = New System.Drawing.Size(164, 21)
        Me.cbFilterProjectName.TabIndex = 210
        Me.cbFilterProjectName.Tag = "PROJECT"
        '
        'chkFilterOpenReportsOnly
        '
        Me.chkFilterOpenReportsOnly.AutoSize = True
        Me.chkFilterOpenReportsOnly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkFilterOpenReportsOnly.Location = New System.Drawing.Point(69, 29)
        Me.chkFilterOpenReportsOnly.Name = "chkFilterOpenReportsOnly"
        Me.chkFilterOpenReportsOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkFilterOpenReportsOnly.Size = New System.Drawing.Size(163, 19)
        Me.chkFilterOpenReportsOnly.TabIndex = 211
        Me.chkFilterOpenReportsOnly.Text = "Show Open Reports Only"
        Me.chkFilterOpenReportsOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFilterOpenReportsOnly.UseVisualStyleBackColor = True
        '
        'gbShowReportsToBeApproved
        '
        Me.gbShowReportsToBeApproved.Controls.Add(Me.rbDisableReadyForReviewFilter)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.rbSelectReadyForReview)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.rbReviewAllREadyForReview)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.chkOEMToApprove)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.chkEngineeringToApporve)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.ChkCITtoApprove)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.chkFPAToApprove)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.chkPAC2ToApprove)
        Me.gbShowReportsToBeApproved.Controls.Add(Me.chkFilterPAC1toApprove)
        Me.gbShowReportsToBeApproved.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.gbShowReportsToBeApproved.Location = New System.Drawing.Point(471, 345)
        Me.gbShowReportsToBeApproved.Name = "gbShowReportsToBeApproved"
        Me.gbShowReportsToBeApproved.Size = New System.Drawing.Size(255, 146)
        Me.gbShowReportsToBeApproved.TabIndex = 212
        Me.gbShowReportsToBeApproved.TabStop = False
        Me.gbShowReportsToBeApproved.Text = "Show Reports Ready For Review"
        '
        'rbDisableReadyForReviewFilter
        '
        Me.rbDisableReadyForReviewFilter.AutoSize = True
        Me.rbDisableReadyForReviewFilter.Checked = True
        Me.rbDisableReadyForReviewFilter.Location = New System.Drawing.Point(139, 23)
        Me.rbDisableReadyForReviewFilter.Name = "rbDisableReadyForReviewFilter"
        Me.rbDisableReadyForReviewFilter.Size = New System.Drawing.Size(73, 20)
        Me.rbDisableReadyForReviewFilter.TabIndex = 153
        Me.rbDisableReadyForReviewFilter.TabStop = True
        Me.rbDisableReadyForReviewFilter.Text = "Disable"
        Me.rbDisableReadyForReviewFilter.UseVisualStyleBackColor = True
        '
        'rbSelectReadyForReview
        '
        Me.rbSelectReadyForReview.AutoSize = True
        Me.rbSelectReadyForReview.Location = New System.Drawing.Point(64, 22)
        Me.rbSelectReadyForReview.Name = "rbSelectReadyForReview"
        Me.rbSelectReadyForReview.Size = New System.Drawing.Size(64, 20)
        Me.rbSelectReadyForReview.TabIndex = 152
        Me.rbSelectReadyForReview.Text = "Select"
        Me.rbSelectReadyForReview.UseVisualStyleBackColor = True
        '
        'rbReviewAllREadyForReview
        '
        Me.rbReviewAllREadyForReview.AutoSize = True
        Me.rbReviewAllREadyForReview.Location = New System.Drawing.Point(10, 22)
        Me.rbReviewAllREadyForReview.Name = "rbReviewAllREadyForReview"
        Me.rbReviewAllREadyForReview.Size = New System.Drawing.Size(41, 20)
        Me.rbReviewAllREadyForReview.TabIndex = 151
        Me.rbReviewAllREadyForReview.Tag = "FR_READY_FOR_REVIEW"
        Me.rbReviewAllREadyForReview.Text = "All"
        Me.rbReviewAllREadyForReview.UseVisualStyleBackColor = True
        '
        'chkOEMToApprove
        '
        Me.chkOEMToApprove.AutoSize = True
        Me.chkOEMToApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOEMToApprove.Location = New System.Drawing.Point(157, 48)
        Me.chkOEMToApprove.Name = "chkOEMToApprove"
        Me.chkOEMToApprove.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkOEMToApprove.Size = New System.Drawing.Size(57, 20)
        Me.chkOEMToApprove.TabIndex = 150
        Me.chkOEMToApprove.Text = "OEM"
        Me.chkOEMToApprove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkOEMToApprove.UseVisualStyleBackColor = True
        '
        'chkEngineeringToApporve
        '
        Me.chkEngineeringToApporve.AutoSize = True
        Me.chkEngineeringToApporve.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEngineeringToApporve.Location = New System.Drawing.Point(158, 68)
        Me.chkEngineeringToApporve.Name = "chkEngineeringToApporve"
        Me.chkEngineeringToApporve.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkEngineeringToApporve.Size = New System.Drawing.Size(56, 20)
        Me.chkEngineeringToApporve.TabIndex = 149
        Me.chkEngineeringToApporve.Text = "ENG"
        Me.chkEngineeringToApporve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkEngineeringToApporve.UseVisualStyleBackColor = True
        '
        'ChkCITtoApprove
        '
        Me.ChkCITtoApprove.AutoSize = True
        Me.ChkCITtoApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCITtoApprove.Location = New System.Drawing.Point(95, 48)
        Me.ChkCITtoApprove.Name = "ChkCITtoApprove"
        Me.ChkCITtoApprove.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkCITtoApprove.Size = New System.Drawing.Size(48, 20)
        Me.ChkCITtoApprove.TabIndex = 148
        Me.ChkCITtoApprove.Text = "CIT"
        Me.ChkCITtoApprove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkCITtoApprove.UseVisualStyleBackColor = True
        '
        'chkFPAToApprove
        '
        Me.chkFPAToApprove.AutoSize = True
        Me.chkFPAToApprove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFPAToApprove.Location = New System.Drawing.Point(90, 68)
        Me.chkFPAToApprove.Name = "chkFPAToApprove"
        Me.chkFPAToApprove.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkFPAToApprove.Size = New System.Drawing.Size(53, 20)
        Me.chkFPAToApprove.TabIndex = 147
        Me.chkFPAToApprove.Text = "FPA"
        Me.chkFPAToApprove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFPAToApprove.UseVisualStyleBackColor = True
        '
        'cbMeterSubTypeII
        '
        Me.cbMeterSubTypeII.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterSubTypeII.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSubTypeII.Location = New System.Drawing.Point(68, 110)
        Me.cbMeterSubTypeII.MaxDropDownItems = 20
        Me.cbMeterSubTypeII.Name = "cbMeterSubTypeII"
        Me.cbMeterSubTypeII.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterSubTypeII.TabIndex = 241
        Me.cbMeterSubTypeII.Tag = "Meter_SubTypeII"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 242
        Me.Label1.Tag = ""
        Me.Label1.Text = "Sub TypeII:"
        '
        'cbMeterVoltage
        '
        Me.cbMeterVoltage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterVoltage.Location = New System.Drawing.Point(308, 154)
        Me.cbMeterVoltage.MaxDropDownItems = 20
        Me.cbMeterVoltage.Name = "cbMeterVoltage"
        Me.cbMeterVoltage.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterVoltage.TabIndex = 238
        Me.cbMeterVoltage.Tag = "Meter_SubType"
        '
        'lblMeterVoltage
        '
        Me.lblMeterVoltage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterVoltage.AutoSize = True
        Me.lblMeterVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterVoltage.Location = New System.Drawing.Point(259, 157)
        Me.lblMeterVoltage.Name = "lblMeterVoltage"
        Me.lblMeterVoltage.Size = New System.Drawing.Size(46, 13)
        Me.lblMeterVoltage.TabIndex = 239
        Me.lblMeterVoltage.Tag = ""
        Me.lblMeterVoltage.Text = "Voltage:"
        '
        'cbMeterBase
        '
        Me.cbMeterBase.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterBase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterBase.Items.AddRange(New Object() {"", "A", "S", "SC", "K"})
        Me.cbMeterBase.Location = New System.Drawing.Point(139, 154)
        Me.cbMeterBase.Name = "cbMeterBase"
        Me.cbMeterBase.Size = New System.Drawing.Size(69, 21)
        Me.cbMeterBase.TabIndex = 237
        Me.cbMeterBase.Tag = "Meter_Base"
        '
        'cbMeterPCBA
        '
        Me.cbMeterPCBA.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterPCBA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterPCBA.Location = New System.Drawing.Point(308, 66)
        Me.cbMeterPCBA.MaxDropDownItems = 20
        Me.cbMeterPCBA.Name = "cbMeterPCBA"
        Me.cbMeterPCBA.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterPCBA.TabIndex = 222
        Me.cbMeterPCBA.Tag = "Meter_PCBA"
        '
        'lblMeterPCBA_PN
        '
        Me.lblMeterPCBA_PN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterPCBA_PN.AutoSize = True
        Me.lblMeterPCBA_PN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterPCBA_PN.Location = New System.Drawing.Point(249, 69)
        Me.lblMeterPCBA_PN.Name = "lblMeterPCBA_PN"
        Me.lblMeterPCBA_PN.Size = New System.Drawing.Size(56, 13)
        Me.lblMeterPCBA_PN.TabIndex = 240
        Me.lblMeterPCBA_PN.Text = "PCBA PN:"
        '
        'lblMeterSerialNumber
        '
        Me.lblMeterSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterSerialNumber.AutoSize = True
        Me.lblMeterSerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSerialNumber.Location = New System.Drawing.Point(21, 135)
        Me.lblMeterSerialNumber.Name = "lblMeterSerialNumber"
        Me.lblMeterSerialNumber.Size = New System.Drawing.Size(43, 13)
        Me.lblMeterSerialNumber.TabIndex = 236
        Me.lblMeterSerialNumber.Text = "Serial#:"
        '
        'cbMeterSerialNumber
        '
        Me.cbMeterSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterSerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSerialNumber.Location = New System.Drawing.Point(68, 132)
        Me.cbMeterSerialNumber.MaxDropDownItems = 20
        Me.cbMeterSerialNumber.Name = "cbMeterSerialNumber"
        Me.cbMeterSerialNumber.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterSerialNumber.TabIndex = 216
        Me.cbMeterSerialNumber.Tag = "Meter_Serial_Number"
        '
        'cbMeterSoftwareRev
        '
        Me.cbMeterSoftwareRev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterSoftwareRev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSoftwareRev.Location = New System.Drawing.Point(308, 132)
        Me.cbMeterSoftwareRev.MaxDropDownItems = 20
        Me.cbMeterSoftwareRev.Name = "cbMeterSoftwareRev"
        Me.cbMeterSoftwareRev.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterSoftwareRev.TabIndex = 225
        Me.cbMeterSoftwareRev.Tag = "Meter_Software_REv"
        '
        'lblMeterSoftwareRev
        '
        Me.lblMeterSoftwareRev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterSoftwareRev.AutoSize = True
        Me.lblMeterSoftwareRev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSoftwareRev.Location = New System.Drawing.Point(255, 135)
        Me.lblMeterSoftwareRev.Name = "lblMeterSoftwareRev"
        Me.lblMeterSoftwareRev.Size = New System.Drawing.Size(51, 13)
        Me.lblMeterSoftwareRev.TabIndex = 235
        Me.lblMeterSoftwareRev.Tag = ""
        Me.lblMeterSoftwareRev.Text = "SW Rev:"
        '
        'cbMeterSoftware
        '
        Me.cbMeterSoftware.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterSoftware.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSoftware.Location = New System.Drawing.Point(308, 110)
        Me.cbMeterSoftware.MaxDropDownItems = 20
        Me.cbMeterSoftware.Name = "cbMeterSoftware"
        Me.cbMeterSoftware.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterSoftware.TabIndex = 224
        Me.cbMeterSoftware.Tag = "Meter_Software"
        '
        'lblMeterSoftware
        '
        Me.lblMeterSoftware.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterSoftware.AutoSize = True
        Me.lblMeterSoftware.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSoftware.Location = New System.Drawing.Point(254, 113)
        Me.lblMeterSoftware.Name = "lblMeterSoftware"
        Me.lblMeterSoftware.Size = New System.Drawing.Size(52, 13)
        Me.lblMeterSoftware.TabIndex = 234
        Me.lblMeterSoftware.Tag = ""
        Me.lblMeterSoftware.Text = "Software:"
        '
        'cbMeterPCBA_Rev
        '
        Me.cbMeterPCBA_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterPCBA_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterPCBA_Rev.Location = New System.Drawing.Point(308, 88)
        Me.cbMeterPCBA_Rev.MaxDropDownItems = 20
        Me.cbMeterPCBA_Rev.Name = "cbMeterPCBA_Rev"
        Me.cbMeterPCBA_Rev.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterPCBA_Rev.TabIndex = 223
        Me.cbMeterPCBA_Rev.Tag = "Meter_PCBA_Rev"
        '
        'lblMeterPCBA_Rev
        '
        Me.lblMeterPCBA_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterPCBA_Rev.AutoSize = True
        Me.lblMeterPCBA_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterPCBA_Rev.Location = New System.Drawing.Point(244, 91)
        Me.lblMeterPCBA_Rev.Name = "lblMeterPCBA_Rev"
        Me.lblMeterPCBA_Rev.Size = New System.Drawing.Size(61, 13)
        Me.lblMeterPCBA_Rev.TabIndex = 233
        Me.lblMeterPCBA_Rev.Text = "PCBA Rev:"
        '
        'lblMeterFW_Ver
        '
        Me.lblMeterFW_Ver.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterFW_Ver.AutoSize = True
        Me.lblMeterFW_Ver.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterFW_Ver.Location = New System.Drawing.Point(258, 47)
        Me.lblMeterFW_Ver.Name = "lblMeterFW_Ver"
        Me.lblMeterFW_Ver.Size = New System.Drawing.Size(50, 13)
        Me.lblMeterFW_Ver.TabIndex = 232
        Me.lblMeterFW_Ver.Text = "FW Rev:"
        '
        'cbMeterDSP_Rev
        '
        Me.cbMeterDSP_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterDSP_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterDSP_Rev.Location = New System.Drawing.Point(308, 22)
        Me.cbMeterDSP_Rev.MaxDropDownItems = 20
        Me.cbMeterDSP_Rev.Name = "cbMeterDSP_Rev"
        Me.cbMeterDSP_Rev.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterDSP_Rev.TabIndex = 220
        Me.cbMeterDSP_Rev.Tag = "Meter_DSP_REV"
        '
        'lblMeter_DSP_Rev
        '
        Me.lblMeter_DSP_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeter_DSP_Rev.AutoSize = True
        Me.lblMeter_DSP_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeter_DSP_Rev.Location = New System.Drawing.Point(250, 25)
        Me.lblMeter_DSP_Rev.Name = "lblMeter_DSP_Rev"
        Me.lblMeter_DSP_Rev.Size = New System.Drawing.Size(55, 13)
        Me.lblMeter_DSP_Rev.TabIndex = 231
        Me.lblMeter_DSP_Rev.Text = "DSP Rev:"
        '
        'cbMeterSubType
        '
        Me.cbMeterSubType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterSubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSubType.Location = New System.Drawing.Point(68, 88)
        Me.cbMeterSubType.MaxDropDownItems = 20
        Me.cbMeterSubType.Name = "cbMeterSubType"
        Me.cbMeterSubType.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterSubType.TabIndex = 218
        Me.cbMeterSubType.Tag = "Meter_SubType"
        '
        'lblMeterSubType
        '
        Me.lblMeterSubType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterSubType.AutoSize = True
        Me.lblMeterSubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSubType.Location = New System.Drawing.Point(9, 91)
        Me.lblMeterSubType.Name = "lblMeterSubType"
        Me.lblMeterSubType.Size = New System.Drawing.Size(56, 13)
        Me.lblMeterSubType.TabIndex = 230
        Me.lblMeterSubType.Tag = ""
        Me.lblMeterSubType.Text = "Sub Type:"
        '
        'cbMeterType
        '
        Me.cbMeterType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterType.Location = New System.Drawing.Point(68, 66)
        Me.cbMeterType.MaxDropDownItems = 20
        Me.cbMeterType.Name = "cbMeterType"
        Me.cbMeterType.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterType.TabIndex = 217
        Me.cbMeterType.Tag = "Meter_Type"
        '
        'lblMeterType
        '
        Me.lblMeterType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterType.AutoSize = True
        Me.lblMeterType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterType.Location = New System.Drawing.Point(32, 69)
        Me.lblMeterType.Name = "lblMeterType"
        Me.lblMeterType.Size = New System.Drawing.Size(34, 13)
        Me.lblMeterType.TabIndex = 229
        Me.lblMeterType.Tag = ""
        Me.lblMeterType.Text = "Type:"
        '
        'lblMeterModel
        '
        Me.lblMeterModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterModel.AutoSize = True
        Me.lblMeterModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterModel.Location = New System.Drawing.Point(27, 47)
        Me.lblMeterModel.Name = "lblMeterModel"
        Me.lblMeterModel.Size = New System.Drawing.Size(39, 13)
        Me.lblMeterModel.TabIndex = 228
        Me.lblMeterModel.Text = "Model:"
        '
        'cbMeterManufacturer
        '
        Me.cbMeterManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterManufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterManufacturer.Location = New System.Drawing.Point(68, 22)
        Me.cbMeterManufacturer.MaxDropDownItems = 20
        Me.cbMeterManufacturer.Name = "cbMeterManufacturer"
        Me.cbMeterManufacturer.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterManufacturer.TabIndex = 215
        Me.cbMeterManufacturer.Tag = "Meter_Manufacturer"
        '
        'lblMeterManufacturer
        '
        Me.lblMeterManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblMeterManufacturer.AutoSize = True
        Me.lblMeterManufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterManufacturer.Location = New System.Drawing.Point(27, 25)
        Me.lblMeterManufacturer.Name = "lblMeterManufacturer"
        Me.lblMeterManufacturer.Size = New System.Drawing.Size(40, 13)
        Me.lblMeterManufacturer.TabIndex = 226
        Me.lblMeterManufacturer.Text = "Manuf:"
        '
        'lblForm
        '
        Me.lblForm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblForm.AutoSize = True
        Me.lblForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForm.Location = New System.Drawing.Point(4, 157)
        Me.lblForm.Name = "lblForm"
        Me.lblForm.Size = New System.Drawing.Size(62, 13)
        Me.lblForm.TabIndex = 227
        Me.lblForm.Text = "Form/Base:"
        '
        'cbMeterFW_Ver
        '
        Me.cbMeterFW_Ver.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterFW_Ver.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterFW_Ver.Location = New System.Drawing.Point(308, 44)
        Me.cbMeterFW_Ver.Name = "cbMeterFW_Ver"
        Me.cbMeterFW_Ver.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterFW_Ver.TabIndex = 221
        Me.cbMeterFW_Ver.Tag = "FW Ver"
        '
        'cbMeterModel
        '
        Me.cbMeterModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterModel.Location = New System.Drawing.Point(68, 44)
        Me.cbMeterModel.MaxDropDownItems = 20
        Me.cbMeterModel.Name = "cbMeterModel"
        Me.cbMeterModel.Size = New System.Drawing.Size(141, 21)
        Me.cbMeterModel.TabIndex = 214
        Me.cbMeterModel.Tag = "Meter"
        '
        'cbMeterForm
        '
        Me.cbMeterForm.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbMeterForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterForm.Location = New System.Drawing.Point(68, 154)
        Me.cbMeterForm.Name = "cbMeterForm"
        Me.cbMeterForm.Size = New System.Drawing.Size(68, 21)
        Me.cbMeterForm.TabIndex = 219
        Me.cbMeterForm.Tag = "Form"
        '
        'lblAMR_SUBtypeIII
        '
        Me.lblAMR_SUBtypeIII.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_SUBtypeIII.AutoSize = True
        Me.lblAMR_SUBtypeIII.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SUBtypeIII.Location = New System.Drawing.Point(4, 137)
        Me.lblAMR_SUBtypeIII.Name = "lblAMR_SUBtypeIII"
        Me.lblAMR_SUBtypeIII.Size = New System.Drawing.Size(65, 13)
        Me.lblAMR_SUBtypeIII.TabIndex = 270
        Me.lblAMR_SUBtypeIII.Tag = "AMR_SUBtype_III"
        Me.lblAMR_SUBtypeIII.Text = "Sub TypeIII:"
        '
        'cbAMR_SUBtypeIII
        '
        Me.cbAMR_SUBtypeIII.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_SUBtypeIII.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SUBtypeIII.Location = New System.Drawing.Point(72, 133)
        Me.cbAMR_SUBtypeIII.MaxDropDownItems = 20
        Me.cbAMR_SUBtypeIII.Name = "cbAMR_SUBtypeIII"
        Me.cbAMR_SUBtypeIII.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_SUBtypeIII.TabIndex = 269
        Me.cbAMR_SUBtypeIII.Tag = "AMR_SubTypeIII"
        '
        'cbAMR_SUBtypeII
        '
        Me.cbAMR_SUBtypeII.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_SUBtypeII.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SUBtypeII.Location = New System.Drawing.Point(72, 111)
        Me.cbAMR_SUBtypeII.MaxDropDownItems = 20
        Me.cbAMR_SUBtypeII.Name = "cbAMR_SUBtypeII"
        Me.cbAMR_SUBtypeII.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_SUBtypeII.TabIndex = 259
        Me.cbAMR_SUBtypeII.Tag = "AMR_SubTypeII"
        '
        'lblAMR_SubTypeII
        '
        Me.lblAMR_SubTypeII.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_SubTypeII.AutoSize = True
        Me.lblAMR_SubTypeII.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SubTypeII.Location = New System.Drawing.Point(7, 115)
        Me.lblAMR_SubTypeII.Name = "lblAMR_SubTypeII"
        Me.lblAMR_SubTypeII.Size = New System.Drawing.Size(62, 13)
        Me.lblAMR_SubTypeII.TabIndex = 262
        Me.lblAMR_SubTypeII.Tag = "AMR_SUBtype_II"
        Me.lblAMR_SubTypeII.Text = "Sub TypeII:"
        '
        'cbAMR_Voltage
        '
        Me.cbAMR_Voltage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_Voltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Voltage.Location = New System.Drawing.Point(312, 157)
        Me.cbAMR_Voltage.MaxDropDownItems = 20
        Me.cbAMR_Voltage.Name = "cbAMR_Voltage"
        Me.cbAMR_Voltage.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_Voltage.TabIndex = 258
        Me.cbAMR_Voltage.Tag = "AMR_Voltage"
        '
        'lblAMRVoltage
        '
        Me.lblAMRVoltage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMRVoltage.AutoSize = True
        Me.lblAMRVoltage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMRVoltage.Location = New System.Drawing.Point(264, 160)
        Me.lblAMRVoltage.Name = "lblAMRVoltage"
        Me.lblAMRVoltage.Size = New System.Drawing.Size(46, 13)
        Me.lblAMRVoltage.TabIndex = 261
        Me.lblAMRVoltage.Tag = ""
        Me.lblAMRVoltage.Text = "Voltage:"
        '
        'cbAMR_PCBA_PN
        '
        Me.cbAMR_PCBA_PN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_PCBA_PN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_PCBA_PN.Location = New System.Drawing.Point(312, 68)
        Me.cbAMR_PCBA_PN.MaxDropDownItems = 20
        Me.cbAMR_PCBA_PN.Name = "cbAMR_PCBA_PN"
        Me.cbAMR_PCBA_PN.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_PCBA_PN.TabIndex = 250
        Me.cbAMR_PCBA_PN.Tag = "AMR_PCBA"
        '
        'lblAMR_PCBA
        '
        Me.lblAMR_PCBA.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_PCBA.AutoSize = True
        Me.lblAMR_PCBA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_PCBA.Location = New System.Drawing.Point(253, 71)
        Me.lblAMR_PCBA.Name = "lblAMR_PCBA"
        Me.lblAMR_PCBA.Size = New System.Drawing.Size(56, 13)
        Me.lblAMR_PCBA.TabIndex = 268
        Me.lblAMR_PCBA.Text = "PCBA PN:"
        '
        'lblAMR_SerialNumber
        '
        Me.lblAMR_SerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_SerialNumber.AutoSize = True
        Me.lblAMR_SerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SerialNumber.Location = New System.Drawing.Point(27, 160)
        Me.lblAMR_SerialNumber.Name = "lblAMR_SerialNumber"
        Me.lblAMR_SerialNumber.Size = New System.Drawing.Size(43, 13)
        Me.lblAMR_SerialNumber.TabIndex = 267
        Me.lblAMR_SerialNumber.Text = "Serial#:"
        '
        'cbAMR_SerialNumber
        '
        Me.cbAMR_SerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_SerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SerialNumber.Location = New System.Drawing.Point(72, 156)
        Me.cbAMR_SerialNumber.MaxDropDownItems = 20
        Me.cbAMR_SerialNumber.Name = "cbAMR_SerialNumber"
        Me.cbAMR_SerialNumber.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_SerialNumber.TabIndex = 245
        Me.cbAMR_SerialNumber.Tag = "AMR_SN"
        '
        'cbAMR_Software_Rev
        '
        Me.cbAMR_Software_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_Software_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Software_Rev.Location = New System.Drawing.Point(312, 135)
        Me.cbAMR_Software_Rev.MaxDropDownItems = 20
        Me.cbAMR_Software_Rev.Name = "cbAMR_Software_Rev"
        Me.cbAMR_Software_Rev.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_Software_Rev.TabIndex = 253
        Me.cbAMR_Software_Rev.Tag = "AMR_Software_Rev"
        '
        'lblAMR_Software_Rev
        '
        Me.lblAMR_Software_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_Software_Rev.AutoSize = True
        Me.lblAMR_Software_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Software_Rev.Location = New System.Drawing.Point(259, 138)
        Me.lblAMR_Software_Rev.Name = "lblAMR_Software_Rev"
        Me.lblAMR_Software_Rev.Size = New System.Drawing.Size(51, 13)
        Me.lblAMR_Software_Rev.TabIndex = 266
        Me.lblAMR_Software_Rev.Tag = ""
        Me.lblAMR_Software_Rev.Text = "SW Rev:"
        '
        'cbAMR_Software
        '
        Me.cbAMR_Software.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_Software.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Software.Location = New System.Drawing.Point(312, 113)
        Me.cbAMR_Software.MaxDropDownItems = 20
        Me.cbAMR_Software.Name = "cbAMR_Software"
        Me.cbAMR_Software.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_Software.TabIndex = 252
        Me.cbAMR_Software.Tag = "AMR_Software"
        '
        'lblAMR_Software
        '
        Me.lblAMR_Software.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_Software.AutoSize = True
        Me.lblAMR_Software.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Software.Location = New System.Drawing.Point(258, 116)
        Me.lblAMR_Software.Name = "lblAMR_Software"
        Me.lblAMR_Software.Size = New System.Drawing.Size(52, 13)
        Me.lblAMR_Software.TabIndex = 265
        Me.lblAMR_Software.Tag = ""
        Me.lblAMR_Software.Text = "Software:"
        '
        'cbAMR_PCBA_Rev
        '
        Me.cbAMR_PCBA_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_PCBA_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_PCBA_Rev.Location = New System.Drawing.Point(312, 90)
        Me.cbAMR_PCBA_Rev.MaxDropDownItems = 20
        Me.cbAMR_PCBA_Rev.Name = "cbAMR_PCBA_Rev"
        Me.cbAMR_PCBA_Rev.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_PCBA_Rev.TabIndex = 251
        Me.cbAMR_PCBA_Rev.Tag = "AMR_PCBA_Rev"
        '
        'lblAMR_PCBA_Rev
        '
        Me.lblAMR_PCBA_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_PCBA_Rev.AutoSize = True
        Me.lblAMR_PCBA_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_PCBA_Rev.Location = New System.Drawing.Point(249, 94)
        Me.lblAMR_PCBA_Rev.Name = "lblAMR_PCBA_Rev"
        Me.lblAMR_PCBA_Rev.Size = New System.Drawing.Size(61, 13)
        Me.lblAMR_PCBA_Rev.TabIndex = 264
        Me.lblAMR_PCBA_Rev.Text = "PCBA Rev:"
        '
        'lblAMR_FW_REV
        '
        Me.lblAMR_FW_REV.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_FW_REV.AutoSize = True
        Me.lblAMR_FW_REV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_FW_REV.Location = New System.Drawing.Point(260, 49)
        Me.lblAMR_FW_REV.Name = "lblAMR_FW_REV"
        Me.lblAMR_FW_REV.Size = New System.Drawing.Size(50, 13)
        Me.lblAMR_FW_REV.TabIndex = 263
        Me.lblAMR_FW_REV.Text = "FW Rev:"
        '
        'cbAMR_IP_LAN_ID
        '
        Me.cbAMR_IP_LAN_ID.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_IP_LAN_ID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_IP_LAN_ID.Location = New System.Drawing.Point(312, 24)
        Me.cbAMR_IP_LAN_ID.MaxDropDownItems = 20
        Me.cbAMR_IP_LAN_ID.Name = "cbAMR_IP_LAN_ID"
        Me.cbAMR_IP_LAN_ID.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_IP_LAN_ID.TabIndex = 248
        Me.cbAMR_IP_LAN_ID.Tag = "AMR_IP_LAN_ID"
        '
        'lblAMR_IP_LAN_ID
        '
        Me.lblAMR_IP_LAN_ID.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_IP_LAN_ID.AutoSize = True
        Me.lblAMR_IP_LAN_ID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_IP_LAN_ID.Location = New System.Drawing.Point(252, 27)
        Me.lblAMR_IP_LAN_ID.Name = "lblAMR_IP_LAN_ID"
        Me.lblAMR_IP_LAN_ID.Size = New System.Drawing.Size(57, 13)
        Me.lblAMR_IP_LAN_ID.TabIndex = 260
        Me.lblAMR_IP_LAN_ID.Text = "IP/Lan ID:"
        '
        'cbAMR_SubType
        '
        Me.cbAMR_SubType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_SubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SubType.Location = New System.Drawing.Point(72, 89)
        Me.cbAMR_SubType.MaxDropDownItems = 20
        Me.cbAMR_SubType.Name = "cbAMR_SubType"
        Me.cbAMR_SubType.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_SubType.TabIndex = 247
        Me.cbAMR_SubType.Tag = "AMR_SubType"
        '
        'lblAMR_SubType
        '
        Me.lblAMR_SubType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_SubType.AutoSize = True
        Me.lblAMR_SubType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SubType.Location = New System.Drawing.Point(14, 93)
        Me.lblAMR_SubType.Name = "lblAMR_SubType"
        Me.lblAMR_SubType.Size = New System.Drawing.Size(56, 13)
        Me.lblAMR_SubType.TabIndex = 257
        Me.lblAMR_SubType.Tag = ""
        Me.lblAMR_SubType.Text = "Sub Type:"
        '
        'cbAMR_TYPE
        '
        Me.cbAMR_TYPE.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_TYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_TYPE.Location = New System.Drawing.Point(72, 67)
        Me.cbAMR_TYPE.MaxDropDownItems = 20
        Me.cbAMR_TYPE.Name = "cbAMR_TYPE"
        Me.cbAMR_TYPE.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_TYPE.TabIndex = 246
        Me.cbAMR_TYPE.Tag = "AMR_Type"
        '
        'lblAMR_Type
        '
        Me.lblAMR_Type.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_Type.AutoSize = True
        Me.lblAMR_Type.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Type.Location = New System.Drawing.Point(36, 71)
        Me.lblAMR_Type.Name = "lblAMR_Type"
        Me.lblAMR_Type.Size = New System.Drawing.Size(34, 13)
        Me.lblAMR_Type.TabIndex = 256
        Me.lblAMR_Type.Tag = ""
        Me.lblAMR_Type.Text = "Type:"
        '
        'lblAMR_MODEL
        '
        Me.lblAMR_MODEL.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_MODEL.AutoSize = True
        Me.lblAMR_MODEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_MODEL.Location = New System.Drawing.Point(31, 49)
        Me.lblAMR_MODEL.Name = "lblAMR_MODEL"
        Me.lblAMR_MODEL.Size = New System.Drawing.Size(39, 13)
        Me.lblAMR_MODEL.TabIndex = 255
        Me.lblAMR_MODEL.Text = "Model:"
        '
        'cbAMR_Manufacturer
        '
        Me.cbAMR_Manufacturer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_Manufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Manufacturer.Location = New System.Drawing.Point(72, 23)
        Me.cbAMR_Manufacturer.MaxDropDownItems = 20
        Me.cbAMR_Manufacturer.Name = "cbAMR_Manufacturer"
        Me.cbAMR_Manufacturer.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_Manufacturer.TabIndex = 244
        Me.cbAMR_Manufacturer.Tag = "AMR_Manufacturer"
        '
        'lblAMR_Manufacturer
        '
        Me.lblAMR_Manufacturer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblAMR_Manufacturer.AutoSize = True
        Me.lblAMR_Manufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Manufacturer.Location = New System.Drawing.Point(31, 26)
        Me.lblAMR_Manufacturer.Name = "lblAMR_Manufacturer"
        Me.lblAMR_Manufacturer.Size = New System.Drawing.Size(40, 13)
        Me.lblAMR_Manufacturer.TabIndex = 254
        Me.lblAMR_Manufacturer.Text = "Manuf:"
        '
        'cbAMR_Model
        '
        Me.cbAMR_Model.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_Model.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Model.Location = New System.Drawing.Point(72, 45)
        Me.cbAMR_Model.Name = "cbAMR_Model"
        Me.cbAMR_Model.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_Model.TabIndex = 243
        Me.cbAMR_Model.Tag = "AMR"
        '
        'cbAMR_FW_Rev
        '
        Me.cbAMR_FW_Rev.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbAMR_FW_Rev.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_FW_Rev.Location = New System.Drawing.Point(312, 46)
        Me.cbAMR_FW_Rev.Name = "cbAMR_FW_Rev"
        Me.cbAMR_FW_Rev.Size = New System.Drawing.Size(141, 21)
        Me.cbAMR_FW_Rev.TabIndex = 249
        Me.cbAMR_FW_Rev.Tag = "AMR Rev"
        '
        'gbFilterEUT_1
        '
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterVoltageAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterSoftwareRevAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterSoftwareAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterPCBA_RevAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterPCBAAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterFW_VerAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterDSP_RevAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterFormAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterSerialNumberAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterSubTypeIIAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterSubTypeAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterTypeAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterModelAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.chkMeterManufacturerAndOr)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterManufacturer)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterForm)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterModel)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterFW_Ver)
        Me.gbFilterEUT_1.Controls.Add(Me.lblForm)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterManufacturer)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterModel)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterType)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterType)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterSubType)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterSubType)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeter_DSP_Rev)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterDSP_Rev)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterFW_Ver)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterPCBA_Rev)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterPCBA_Rev)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterSoftware)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterSoftware)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterSoftwareRev)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterSoftwareRev)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterSerialNumber)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterSerialNumber)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterPCBA_PN)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterPCBA)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterBase)
        Me.gbFilterEUT_1.Controls.Add(Me.lblMeterVoltage)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterVoltage)
        Me.gbFilterEUT_1.Controls.Add(Me.Label1)
        Me.gbFilterEUT_1.Controls.Add(Me.cbMeterSubTypeII)
        Me.gbFilterEUT_1.Location = New System.Drawing.Point(10, 156)
        Me.gbFilterEUT_1.Name = "gbFilterEUT_1"
        Me.gbFilterEUT_1.Size = New System.Drawing.Size(489, 183)
        Me.gbFilterEUT_1.TabIndex = 271
        Me.gbFilterEUT_1.TabStop = False
        Me.gbFilterEUT_1.Text = "Meter"
        '
        'chkMeterVoltageAndOr
        '
        Me.chkMeterVoltageAndOr.AutoSize = True
        Me.chkMeterVoltageAndOr.Location = New System.Drawing.Point(455, 156)
        Me.chkMeterVoltageAndOr.Name = "chkMeterVoltageAndOr"
        Me.chkMeterVoltageAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterVoltageAndOr.TabIndex = 306
        Me.chkMeterVoltageAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterVoltageAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterVoltageAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterSoftwareRevAndOr
        '
        Me.chkMeterSoftwareRevAndOr.AutoSize = True
        Me.chkMeterSoftwareRevAndOr.Location = New System.Drawing.Point(455, 135)
        Me.chkMeterSoftwareRevAndOr.Name = "chkMeterSoftwareRevAndOr"
        Me.chkMeterSoftwareRevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterSoftwareRevAndOr.TabIndex = 307
        Me.chkMeterSoftwareRevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterSoftwareRevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterSoftwareRevAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterSoftwareAndOr
        '
        Me.chkMeterSoftwareAndOr.AutoSize = True
        Me.chkMeterSoftwareAndOr.Location = New System.Drawing.Point(455, 113)
        Me.chkMeterSoftwareAndOr.Name = "chkMeterSoftwareAndOr"
        Me.chkMeterSoftwareAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterSoftwareAndOr.TabIndex = 308
        Me.chkMeterSoftwareAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterSoftwareAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterSoftwareAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterPCBA_RevAndOr
        '
        Me.chkMeterPCBA_RevAndOr.AutoSize = True
        Me.chkMeterPCBA_RevAndOr.Location = New System.Drawing.Point(455, 92)
        Me.chkMeterPCBA_RevAndOr.Name = "chkMeterPCBA_RevAndOr"
        Me.chkMeterPCBA_RevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterPCBA_RevAndOr.TabIndex = 291
        Me.chkMeterPCBA_RevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterPCBA_RevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterPCBA_RevAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterPCBAAndOr
        '
        Me.chkMeterPCBAAndOr.AutoSize = True
        Me.chkMeterPCBAAndOr.Location = New System.Drawing.Point(455, 71)
        Me.chkMeterPCBAAndOr.Name = "chkMeterPCBAAndOr"
        Me.chkMeterPCBAAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterPCBAAndOr.TabIndex = 292
        Me.chkMeterPCBAAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterPCBAAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterPCBAAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterFW_VerAndOr
        '
        Me.chkMeterFW_VerAndOr.AutoSize = True
        Me.chkMeterFW_VerAndOr.Location = New System.Drawing.Point(455, 48)
        Me.chkMeterFW_VerAndOr.Name = "chkMeterFW_VerAndOr"
        Me.chkMeterFW_VerAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterFW_VerAndOr.TabIndex = 293
        Me.chkMeterFW_VerAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterFW_VerAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterFW_VerAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterDSP_RevAndOr
        '
        Me.chkMeterDSP_RevAndOr.AutoSize = True
        Me.chkMeterDSP_RevAndOr.Location = New System.Drawing.Point(455, 27)
        Me.chkMeterDSP_RevAndOr.Name = "chkMeterDSP_RevAndOr"
        Me.chkMeterDSP_RevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterDSP_RevAndOr.TabIndex = 294
        Me.chkMeterDSP_RevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterDSP_RevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterDSP_RevAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterFormAndOr
        '
        Me.chkMeterFormAndOr.AutoSize = True
        Me.chkMeterFormAndOr.Location = New System.Drawing.Point(214, 156)
        Me.chkMeterFormAndOr.Name = "chkMeterFormAndOr"
        Me.chkMeterFormAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterFormAndOr.TabIndex = 295
        Me.chkMeterFormAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterFormAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterFormAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterSerialNumberAndOr
        '
        Me.chkMeterSerialNumberAndOr.AutoSize = True
        Me.chkMeterSerialNumberAndOr.Location = New System.Drawing.Point(214, 136)
        Me.chkMeterSerialNumberAndOr.Name = "chkMeterSerialNumberAndOr"
        Me.chkMeterSerialNumberAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterSerialNumberAndOr.TabIndex = 296
        Me.chkMeterSerialNumberAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterSerialNumberAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterSerialNumberAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterSubTypeIIAndOr
        '
        Me.chkMeterSubTypeIIAndOr.AutoSize = True
        Me.chkMeterSubTypeIIAndOr.Location = New System.Drawing.Point(214, 113)
        Me.chkMeterSubTypeIIAndOr.Name = "chkMeterSubTypeIIAndOr"
        Me.chkMeterSubTypeIIAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterSubTypeIIAndOr.TabIndex = 297
        Me.chkMeterSubTypeIIAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterSubTypeIIAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterSubTypeIIAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterSubTypeAndOr
        '
        Me.chkMeterSubTypeAndOr.AutoSize = True
        Me.chkMeterSubTypeAndOr.Location = New System.Drawing.Point(214, 92)
        Me.chkMeterSubTypeAndOr.Name = "chkMeterSubTypeAndOr"
        Me.chkMeterSubTypeAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterSubTypeAndOr.TabIndex = 298
        Me.chkMeterSubTypeAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterSubTypeAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterSubTypeAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterTypeAndOr
        '
        Me.chkMeterTypeAndOr.AutoSize = True
        Me.chkMeterTypeAndOr.Location = New System.Drawing.Point(214, 70)
        Me.chkMeterTypeAndOr.Name = "chkMeterTypeAndOr"
        Me.chkMeterTypeAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterTypeAndOr.TabIndex = 299
        Me.chkMeterTypeAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterTypeAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterTypeAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterModelAndOr
        '
        Me.chkMeterModelAndOr.AutoSize = True
        Me.chkMeterModelAndOr.Location = New System.Drawing.Point(214, 47)
        Me.chkMeterModelAndOr.Name = "chkMeterModelAndOr"
        Me.chkMeterModelAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterModelAndOr.TabIndex = 300
        Me.chkMeterModelAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterModelAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterModelAndOr.UseVisualStyleBackColor = True
        '
        'chkMeterManufacturerAndOr
        '
        Me.chkMeterManufacturerAndOr.AutoSize = True
        Me.chkMeterManufacturerAndOr.Location = New System.Drawing.Point(214, 26)
        Me.chkMeterManufacturerAndOr.Name = "chkMeterManufacturerAndOr"
        Me.chkMeterManufacturerAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkMeterManufacturerAndOr.TabIndex = 301
        Me.chkMeterManufacturerAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkMeterManufacturerAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkMeterManufacturerAndOr.UseVisualStyleBackColor = True
        '
        'gbFilterEUT2
        '
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_VoltageAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_Software_RevAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_SoftwareAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_PCBA_RevAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_PCBA_PNAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_FW_RevAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_IP_LAN_IDAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_SerialNumberAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_SUBtypeIIIAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_SubTypeIIAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_SubTypeAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_TYPEAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_ModelAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.chkAMR_ManufacturerAndOr)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_Manufacturer)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_FW_Rev)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_SUBtypeIII)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_Model)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_SUBtypeIII)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_Manufacturer)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_SUBtypeII)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_MODEL)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_SubTypeII)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_Type)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_Voltage)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_TYPE)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMRVoltage)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_SubType)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_PCBA_PN)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_SubType)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_PCBA)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_IP_LAN_ID)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_SerialNumber)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_IP_LAN_ID)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_SerialNumber)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_FW_REV)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_Software_Rev)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_PCBA_Rev)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_Software_Rev)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_PCBA_Rev)
        Me.gbFilterEUT2.Controls.Add(Me.cbAMR_Software)
        Me.gbFilterEUT2.Controls.Add(Me.lblAMR_Software)
        Me.gbFilterEUT2.Location = New System.Drawing.Point(506, 156)
        Me.gbFilterEUT2.Name = "gbFilterEUT2"
        Me.gbFilterEUT2.Size = New System.Drawing.Size(495, 183)
        Me.gbFilterEUT2.TabIndex = 272
        Me.gbFilterEUT2.TabStop = False
        Me.gbFilterEUT2.Text = "AMR"
        '
        'chkAMR_VoltageAndOr
        '
        Me.chkAMR_VoltageAndOr.AutoSize = True
        Me.chkAMR_VoltageAndOr.Location = New System.Drawing.Point(461, 159)
        Me.chkAMR_VoltageAndOr.Name = "chkAMR_VoltageAndOr"
        Me.chkAMR_VoltageAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_VoltageAndOr.TabIndex = 292
        Me.chkAMR_VoltageAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_VoltageAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_VoltageAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_Software_RevAndOr
        '
        Me.chkAMR_Software_RevAndOr.AutoSize = True
        Me.chkAMR_Software_RevAndOr.Location = New System.Drawing.Point(461, 138)
        Me.chkAMR_Software_RevAndOr.Name = "chkAMR_Software_RevAndOr"
        Me.chkAMR_Software_RevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_Software_RevAndOr.TabIndex = 293
        Me.chkAMR_Software_RevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_Software_RevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_Software_RevAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_SoftwareAndOr
        '
        Me.chkAMR_SoftwareAndOr.AutoSize = True
        Me.chkAMR_SoftwareAndOr.Location = New System.Drawing.Point(461, 116)
        Me.chkAMR_SoftwareAndOr.Name = "chkAMR_SoftwareAndOr"
        Me.chkAMR_SoftwareAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_SoftwareAndOr.TabIndex = 294
        Me.chkAMR_SoftwareAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_SoftwareAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_SoftwareAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_PCBA_RevAndOr
        '
        Me.chkAMR_PCBA_RevAndOr.AutoSize = True
        Me.chkAMR_PCBA_RevAndOr.Location = New System.Drawing.Point(461, 92)
        Me.chkAMR_PCBA_RevAndOr.Name = "chkAMR_PCBA_RevAndOr"
        Me.chkAMR_PCBA_RevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_PCBA_RevAndOr.TabIndex = 295
        Me.chkAMR_PCBA_RevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_PCBA_RevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_PCBA_RevAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_PCBA_PNAndOr
        '
        Me.chkAMR_PCBA_PNAndOr.AutoSize = True
        Me.chkAMR_PCBA_PNAndOr.Location = New System.Drawing.Point(461, 70)
        Me.chkAMR_PCBA_PNAndOr.Name = "chkAMR_PCBA_PNAndOr"
        Me.chkAMR_PCBA_PNAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_PCBA_PNAndOr.TabIndex = 296
        Me.chkAMR_PCBA_PNAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_PCBA_PNAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_PCBA_PNAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_FW_RevAndOr
        '
        Me.chkAMR_FW_RevAndOr.AutoSize = True
        Me.chkAMR_FW_RevAndOr.Location = New System.Drawing.Point(461, 47)
        Me.chkAMR_FW_RevAndOr.Name = "chkAMR_FW_RevAndOr"
        Me.chkAMR_FW_RevAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_FW_RevAndOr.TabIndex = 297
        Me.chkAMR_FW_RevAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_FW_RevAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_FW_RevAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_IP_LAN_IDAndOr
        '
        Me.chkAMR_IP_LAN_IDAndOr.AutoSize = True
        Me.chkAMR_IP_LAN_IDAndOr.Location = New System.Drawing.Point(461, 27)
        Me.chkAMR_IP_LAN_IDAndOr.Name = "chkAMR_IP_LAN_IDAndOr"
        Me.chkAMR_IP_LAN_IDAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_IP_LAN_IDAndOr.TabIndex = 298
        Me.chkAMR_IP_LAN_IDAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_IP_LAN_IDAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_IP_LAN_IDAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_SerialNumberAndOr
        '
        Me.chkAMR_SerialNumberAndOr.AutoSize = True
        Me.chkAMR_SerialNumberAndOr.Location = New System.Drawing.Point(219, 159)
        Me.chkAMR_SerialNumberAndOr.Name = "chkAMR_SerialNumberAndOr"
        Me.chkAMR_SerialNumberAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_SerialNumberAndOr.TabIndex = 299
        Me.chkAMR_SerialNumberAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_SerialNumberAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_SerialNumberAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_SUBtypeIIIAndOr
        '
        Me.chkAMR_SUBtypeIIIAndOr.AutoSize = True
        Me.chkAMR_SUBtypeIIIAndOr.Location = New System.Drawing.Point(219, 138)
        Me.chkAMR_SUBtypeIIIAndOr.Name = "chkAMR_SUBtypeIIIAndOr"
        Me.chkAMR_SUBtypeIIIAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_SUBtypeIIIAndOr.TabIndex = 300
        Me.chkAMR_SUBtypeIIIAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_SUBtypeIIIAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_SUBtypeIIIAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_SubTypeIIAndOr
        '
        Me.chkAMR_SubTypeIIAndOr.AutoSize = True
        Me.chkAMR_SubTypeIIAndOr.Location = New System.Drawing.Point(219, 115)
        Me.chkAMR_SubTypeIIAndOr.Name = "chkAMR_SubTypeIIAndOr"
        Me.chkAMR_SubTypeIIAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_SubTypeIIAndOr.TabIndex = 301
        Me.chkAMR_SubTypeIIAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_SubTypeIIAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_SubTypeIIAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_SubTypeAndOr
        '
        Me.chkAMR_SubTypeAndOr.AutoSize = True
        Me.chkAMR_SubTypeAndOr.Location = New System.Drawing.Point(219, 93)
        Me.chkAMR_SubTypeAndOr.Name = "chkAMR_SubTypeAndOr"
        Me.chkAMR_SubTypeAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_SubTypeAndOr.TabIndex = 302
        Me.chkAMR_SubTypeAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_SubTypeAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_SubTypeAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_TYPEAndOr
        '
        Me.chkAMR_TYPEAndOr.AutoSize = True
        Me.chkAMR_TYPEAndOr.Location = New System.Drawing.Point(219, 71)
        Me.chkAMR_TYPEAndOr.Name = "chkAMR_TYPEAndOr"
        Me.chkAMR_TYPEAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_TYPEAndOr.TabIndex = 303
        Me.chkAMR_TYPEAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_TYPEAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_TYPEAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_ModelAndOr
        '
        Me.chkAMR_ModelAndOr.AutoSize = True
        Me.chkAMR_ModelAndOr.Location = New System.Drawing.Point(219, 50)
        Me.chkAMR_ModelAndOr.Name = "chkAMR_ModelAndOr"
        Me.chkAMR_ModelAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_ModelAndOr.TabIndex = 304
        Me.chkAMR_ModelAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_ModelAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_ModelAndOr.UseVisualStyleBackColor = True
        '
        'chkAMR_ManufacturerAndOr
        '
        Me.chkAMR_ManufacturerAndOr.AutoSize = True
        Me.chkAMR_ManufacturerAndOr.Location = New System.Drawing.Point(219, 27)
        Me.chkAMR_ManufacturerAndOr.Name = "chkAMR_ManufacturerAndOr"
        Me.chkAMR_ManufacturerAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkAMR_ManufacturerAndOr.TabIndex = 305
        Me.chkAMR_ManufacturerAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkAMR_ManufacturerAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkAMR_ManufacturerAndOr.UseVisualStyleBackColor = True
        '
        'lblEUTType
        '
        Me.lblEUTType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEUTType.AutoSize = True
        Me.lblEUTType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEUTType.Location = New System.Drawing.Point(76, 130)
        Me.lblEUTType.Name = "lblEUTType"
        Me.lblEUTType.Size = New System.Drawing.Size(74, 16)
        Me.lblEUTType.TabIndex = 274
        Me.lblEUTType.Text = "EUT Type:"
        '
        'cbFilterEUTType
        '
        Me.cbFilterEUTType.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cbFilterEUTType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterEUTType.Items.AddRange(New Object() {"Any", "Meter Only", "Meter and AMR", "AMR Only", "Other EUT"})
        Me.cbFilterEUTType.Location = New System.Drawing.Point(151, 128)
        Me.cbFilterEUTType.Name = "cbFilterEUTType"
        Me.cbFilterEUTType.Size = New System.Drawing.Size(164, 21)
        Me.cbFilterEUTType.TabIndex = 273
        Me.cbFilterEUTType.Tag = "EUT_Type"
        Me.ToolTip1.SetToolTip(Me.cbFilterEUTType, "Select the EUT Type")
        '
        'btnFilterClear
        '
        Me.btnFilterClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnFilterClear.Location = New System.Drawing.Point(49, 71)
        Me.btnFilterClear.Name = "btnFilterClear"
        Me.btnFilterClear.Size = New System.Drawing.Size(49, 34)
        Me.btnFilterClear.TabIndex = 275
        Me.btnFilterClear.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.btnFilterClear, "Clear Filed Selections")
        Me.btnFilterClear.UseVisualStyleBackColor = True
        '
        'btnFilterCorrectedDatesCleared
        '
        Me.btnFilterCorrectedDatesCleared.Location = New System.Drawing.Point(373, 388)
        Me.btnFilterCorrectedDatesCleared.Name = "btnFilterCorrectedDatesCleared"
        Me.btnFilterCorrectedDatesCleared.Size = New System.Drawing.Size(57, 22)
        Me.btnFilterCorrectedDatesCleared.TabIndex = 280
        Me.btnFilterCorrectedDatesCleared.Text = "<<Clear"
        Me.btnFilterCorrectedDatesCleared.UseVisualStyleBackColor = True
        '
        'dtpFilterCorrectedTo
        '
        Me.dtpFilterCorrectedTo.CustomFormat = " "
        Me.dtpFilterCorrectedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterCorrectedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterCorrectedTo.Location = New System.Drawing.Point(275, 388)
        Me.dtpFilterCorrectedTo.Name = "dtpFilterCorrectedTo"
        Me.dtpFilterCorrectedTo.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterCorrectedTo.TabIndex = 279
        '
        'lblFilterCorrectedTo
        '
        Me.lblFilterCorrectedTo.AutoSize = True
        Me.lblFilterCorrectedTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterCorrectedTo.Location = New System.Drawing.Point(243, 390)
        Me.lblFilterCorrectedTo.Name = "lblFilterCorrectedTo"
        Me.lblFilterCorrectedTo.Size = New System.Drawing.Size(28, 16)
        Me.lblFilterCorrectedTo.TabIndex = 278
        Me.lblFilterCorrectedTo.Text = "To:"
        '
        'dtpFilterCorrectedFrom
        '
        Me.dtpFilterCorrectedFrom.CustomFormat = " "
        Me.dtpFilterCorrectedFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFilterCorrectedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFilterCorrectedFrom.Location = New System.Drawing.Point(145, 388)
        Me.dtpFilterCorrectedFrom.Name = "dtpFilterCorrectedFrom"
        Me.dtpFilterCorrectedFrom.Size = New System.Drawing.Size(92, 21)
        Me.dtpFilterCorrectedFrom.TabIndex = 277
        '
        'lblFilterCorrectedFrom
        '
        Me.lblFilterCorrectedFrom.AutoSize = True
        Me.lblFilterCorrectedFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterCorrectedFrom.Location = New System.Drawing.Point(7, 391)
        Me.lblFilterCorrectedFrom.Name = "lblFilterCorrectedFrom"
        Me.lblFilterCorrectedFrom.Size = New System.Drawing.Size(136, 16)
        Me.lblFilterCorrectedFrom.TabIndex = 276
        Me.lblFilterCorrectedFrom.Text = "Date Corrected From:"
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.AutoSize = True
        Me.lblApprovedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedBy.Location = New System.Drawing.Point(58, 461)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(90, 16)
        Me.lblApprovedBy.TabIndex = 282
        Me.lblApprovedBy.Text = "Approved By:"
        '
        'cbApprovedBy
        '
        Me.cbApprovedBy.DisplayMember = "AMR"
        Me.cbApprovedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbApprovedBy.Location = New System.Drawing.Point(145, 460)
        Me.cbApprovedBy.Name = "cbApprovedBy"
        Me.cbApprovedBy.Size = New System.Drawing.Size(164, 21)
        Me.cbApprovedBy.TabIndex = 283
        Me.cbApprovedBy.Tag = "Approved By"
        '
        'chkTestLevelAndOr
        '
        Me.chkTestLevelAndOr.AutoSize = True
        Me.chkTestLevelAndOr.Location = New System.Drawing.Point(703, 31)
        Me.chkTestLevelAndOr.Name = "chkTestLevelAndOr"
        Me.chkTestLevelAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkTestLevelAndOr.TabIndex = 285
        Me.chkTestLevelAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkTestLevelAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkTestLevelAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterEUTTypeAndOr
        '
        Me.chkFilterEUTTypeAndOr.AutoSize = True
        Me.chkFilterEUTTypeAndOr.Location = New System.Drawing.Point(318, 130)
        Me.chkFilterEUTTypeAndOr.Name = "chkFilterEUTTypeAndOr"
        Me.chkFilterEUTTypeAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterEUTTypeAndOr.TabIndex = 286
        Me.chkFilterEUTTypeAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterEUTTypeAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterEUTTypeAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterCorrectedByAndOr
        '
        Me.chkFilterCorrectedByAndOr.AutoSize = True
        Me.chkFilterCorrectedByAndOr.Location = New System.Drawing.Point(703, 101)
        Me.chkFilterCorrectedByAndOr.Name = "chkFilterCorrectedByAndOr"
        Me.chkFilterCorrectedByAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterCorrectedByAndOr.TabIndex = 287
        Me.chkFilterCorrectedByAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterCorrectedByAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterCorrectedByAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterProjectNameAndOr
        '
        Me.chkFilterProjectNameAndOr.AutoSize = True
        Me.chkFilterProjectNameAndOr.Location = New System.Drawing.Point(703, 77)
        Me.chkFilterProjectNameAndOr.Name = "chkFilterProjectNameAndOr"
        Me.chkFilterProjectNameAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterProjectNameAndOr.TabIndex = 288
        Me.chkFilterProjectNameAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterProjectNameAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterProjectNameAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterAssignedToAndOr
        '
        Me.chkFilterAssignedToAndOr.AutoSize = True
        Me.chkFilterAssignedToAndOr.Location = New System.Drawing.Point(318, 100)
        Me.chkFilterAssignedToAndOr.Name = "chkFilterAssignedToAndOr"
        Me.chkFilterAssignedToAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterAssignedToAndOr.TabIndex = 289
        Me.chkFilterAssignedToAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterAssignedToAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterAssignedToAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterProjectNumberAndOr
        '
        Me.chkFilterProjectNumberAndOr.AutoSize = True
        Me.chkFilterProjectNumberAndOr.Location = New System.Drawing.Point(318, 79)
        Me.chkFilterProjectNumberAndOr.Name = "chkFilterProjectNumberAndOr"
        Me.chkFilterProjectNumberAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterProjectNumberAndOr.TabIndex = 290
        Me.chkFilterProjectNumberAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterProjectNumberAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterProjectNumberAndOr.UseVisualStyleBackColor = True
        '
        'chkFilteredFailedFromAndOr
        '
        Me.chkFilteredFailedFromAndOr.AutoSize = True
        Me.chkFilteredFailedFromAndOr.Location = New System.Drawing.Point(433, 370)
        Me.chkFilteredFailedFromAndOr.Name = "chkFilteredFailedFromAndOr"
        Me.chkFilteredFailedFromAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilteredFailedFromAndOr.TabIndex = 307
        Me.chkFilteredFailedFromAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilteredFailedFromAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilteredFailedFromAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterCorrectedFromAndOr
        '
        Me.chkFilterCorrectedFromAndOr.AutoSize = True
        Me.chkFilterCorrectedFromAndOr.Location = New System.Drawing.Point(433, 392)
        Me.chkFilterCorrectedFromAndOr.Name = "chkFilterCorrectedFromAndOr"
        Me.chkFilterCorrectedFromAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterCorrectedFromAndOr.TabIndex = 308
        Me.chkFilterCorrectedFromAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterCorrectedFromAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterCorrectedFromAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterClosedFromAndOr
        '
        Me.chkFilterClosedFromAndOr.AutoSize = True
        Me.chkFilterClosedFromAndOr.Location = New System.Drawing.Point(433, 415)
        Me.chkFilterClosedFromAndOr.Name = "chkFilterClosedFromAndOr"
        Me.chkFilterClosedFromAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterClosedFromAndOr.TabIndex = 309
        Me.chkFilterClosedFromAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterClosedFromAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterClosedFromAndOr.UseVisualStyleBackColor = True
        '
        'chkApprovedByAndOr
        '
        Me.chkApprovedByAndOr.AutoSize = True
        Me.chkApprovedByAndOr.Location = New System.Drawing.Point(311, 463)
        Me.chkApprovedByAndOr.Name = "chkApprovedByAndOr"
        Me.chkApprovedByAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkApprovedByAndOr.TabIndex = 310
        Me.chkApprovedByAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkApprovedByAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkApprovedByAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterApprovedFromAndOr
        '
        Me.chkFilterApprovedFromAndOr.AutoSize = True
        Me.chkFilterApprovedFromAndOr.Location = New System.Drawing.Point(433, 437)
        Me.chkFilterApprovedFromAndOr.Name = "chkFilterApprovedFromAndOr"
        Me.chkFilterApprovedFromAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterApprovedFromAndOr.TabIndex = 311
        Me.chkFilterApprovedFromAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterApprovedFromAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterApprovedFromAndOr.UseVisualStyleBackColor = True
        '
        'chkFilterTestsAndOr
        '
        Me.chkFilterTestsAndOr.AutoSize = True
        Me.chkFilterTestsAndOr.Location = New System.Drawing.Point(703, 53)
        Me.chkFilterTestsAndOr.Name = "chkFilterTestsAndOr"
        Me.chkFilterTestsAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterTestsAndOr.TabIndex = 312
        Me.chkFilterTestsAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterTestsAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterTestsAndOr.UseVisualStyleBackColor = True
        '
        'cbFilterTestType
        '
        Me.cbFilterTestType.Location = New System.Drawing.Point(152, 28)
        Me.cbFilterTestType.Name = "cbFilterTestType"
        Me.cbFilterTestType.Size = New System.Drawing.Size(175, 21)
        Me.cbFilterTestType.TabIndex = 314
        Me.cbFilterTestType.Tag = "Test_Type"
        Me.ToolTip1.SetToolTip(Me.cbFilterTestType, "Test Catagory, Funtional, EMC, Safety etc...")
        '
        'lblTestType
        '
        Me.lblTestType.AutoSize = True
        Me.lblTestType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestType.Location = New System.Drawing.Point(78, 30)
        Me.lblTestType.Name = "lblTestType"
        Me.lblTestType.Size = New System.Drawing.Size(73, 16)
        Me.lblTestType.TabIndex = 313
        Me.lblTestType.Text = "Test Type:"
        Me.lblTestType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFilterTestTypeAndOr
        '
        Me.chkFilterTestTypeAndOr.AutoSize = True
        Me.chkFilterTestTypeAndOr.Location = New System.Drawing.Point(332, 31)
        Me.chkFilterTestTypeAndOr.Name = "chkFilterTestTypeAndOr"
        Me.chkFilterTestTypeAndOr.Size = New System.Drawing.Size(32, 17)
        Me.chkFilterTestTypeAndOr.TabIndex = 319
        Me.chkFilterTestTypeAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.chkFilterTestTypeAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.chkFilterTestTypeAndOr.UseVisualStyleBackColor = True
        '
        'chkPreserveFilter
        '
        Me.chkPreserveFilter.AutoSize = True
        Me.chkPreserveFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkPreserveFilter.Location = New System.Drawing.Point(9, 116)
        Me.chkPreserveFilter.Name = "chkPreserveFilter"
        Me.chkPreserveFilter.Size = New System.Drawing.Size(104, 19)
        Me.chkPreserveFilter.TabIndex = 320
        Me.chkPreserveFilter.Text = "Preserve Filter"
        Me.ToolTip1.SetToolTip(Me.chkPreserveFilter, "Checked: Everytime Build is Pressed the Filter is Appended." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "UnChecked: Everytime" & _
                " the Filter Is checked it is Replaced.")
        Me.chkPreserveFilter.UseVisualStyleBackColor = True
        '
        'chkFilterAllowAdvancedEditing
        '
        Me.chkFilterAllowAdvancedEditing.AutoSize = True
        Me.chkFilterAllowAdvancedEditing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkFilterAllowAdvancedEditing.Location = New System.Drawing.Point(153, 110)
        Me.chkFilterAllowAdvancedEditing.Name = "chkFilterAllowAdvancedEditing"
        Me.chkFilterAllowAdvancedEditing.Size = New System.Drawing.Size(111, 34)
        Me.chkFilterAllowAdvancedEditing.TabIndex = 321
        Me.chkFilterAllowAdvancedEditing.Text = "Allow Advanced" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Editing"
        Me.ToolTip1.SetToolTip(Me.chkFilterAllowAdvancedEditing, "Allows Directly editing the Filter Parameters")
        Me.chkFilterAllowAdvancedEditing.UseVisualStyleBackColor = True
        '
        'btnFilterBuild
        '
        Me.btnFilterBuild.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnFilterBuild.Location = New System.Drawing.Point(2, 71)
        Me.btnFilterBuild.Name = "btnFilterBuild"
        Me.btnFilterBuild.Size = New System.Drawing.Size(49, 34)
        Me.btnFilterBuild.TabIndex = 316
        Me.btnFilterBuild.Text = "Build"
        Me.ToolTip1.SetToolTip(Me.btnFilterBuild, "Build Filter Based On Selctions")
        Me.btnFilterBuild.UseVisualStyleBackColor = True
        '
        'BtnFilterRemove
        '
        Me.BtnFilterRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.BtnFilterRemove.Location = New System.Drawing.Point(148, 71)
        Me.BtnFilterRemove.Name = "BtnFilterRemove"
        Me.BtnFilterRemove.Size = New System.Drawing.Size(61, 34)
        Me.BtnFilterRemove.TabIndex = 317
        Me.BtnFilterRemove.Text = "Remove"
        Me.ToolTip1.SetToolTip(Me.BtnFilterRemove, "Remove the Filter formn the Selection")
        Me.BtnFilterRemove.UseVisualStyleBackColor = True
        '
        'ChkPreserveFilterAndOr
        '
        Me.ChkPreserveFilterAndOr.AutoSize = True
        Me.ChkPreserveFilterAndOr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.ChkPreserveFilterAndOr.Location = New System.Drawing.Point(114, 116)
        Me.ChkPreserveFilterAndOr.Name = "ChkPreserveFilterAndOr"
        Me.ChkPreserveFilterAndOr.Size = New System.Drawing.Size(33, 19)
        Me.ChkPreserveFilterAndOr.TabIndex = 322
        Me.ChkPreserveFilterAndOr.Text = "+"
        Me.ToolTip1.SetToolTip(Me.ChkPreserveFilterAndOr, "Unchecked = 'AND' (+)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Checked  = 'OR'  ( | )")
        Me.ChkPreserveFilterAndOr.UseVisualStyleBackColor = True
        '
        'txtFilterDisplay
        '
        Me.txtFilterDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFilterDisplay.Location = New System.Drawing.Point(3, 3)
        Me.txtFilterDisplay.Multiline = True
        Me.txtFilterDisplay.Name = "txtFilterDisplay"
        Me.txtFilterDisplay.ReadOnly = True
        Me.txtFilterDisplay.Size = New System.Drawing.Size(996, 40)
        Me.txtFilterDisplay.TabIndex = 317
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1008, 738)
        Me.TableLayoutPanel1.TabIndex = 318
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.gbFilterControls)
        Me.Panel1.Controls.Add(Me.chkFilterTestTypeAndOr)
        Me.Panel1.Controls.Add(Me.lblFilterReportNumber)
        Me.Panel1.Controls.Add(Me.txtFilterNewIDFrom)
        Me.Panel1.Controls.Add(Me.cbFilterTestType)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblTestType)
        Me.Panel1.Controls.Add(Me.lblFilterLevel)
        Me.Panel1.Controls.Add(Me.chkFilterTestsAndOr)
        Me.Panel1.Controls.Add(Me.dtpFilterFailedFrom)
        Me.Panel1.Controls.Add(Me.chkFilterApprovedFromAndOr)
        Me.Panel1.Controls.Add(Me.lblTestName)
        Me.Panel1.Controls.Add(Me.chkApprovedByAndOr)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.chkFilterClosedFromAndOr)
        Me.Panel1.Controls.Add(Me.dtpFilterFailedTo)
        Me.Panel1.Controls.Add(Me.chkFilterCorrectedFromAndOr)
        Me.Panel1.Controls.Add(Me.chkFilteredFailedFromAndOr)
        Me.Panel1.Controls.Add(Me.chkFilterProjectNumberAndOr)
        Me.Panel1.Controls.Add(Me.cbFilterLevel)
        Me.Panel1.Controls.Add(Me.chkFilterAssignedToAndOr)
        Me.Panel1.Controls.Add(Me.cbFilterTests)
        Me.Panel1.Controls.Add(Me.chkFilterProjectNameAndOr)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.chkFilterCorrectedByAndOr)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.chkFilterEUTTypeAndOr)
        Me.Panel1.Controls.Add(Me.dtpFilterApprovedFrom)
        Me.Panel1.Controls.Add(Me.chkTestLevelAndOr)
        Me.Panel1.Controls.Add(Me.dtpFilterApprovedTo)
        Me.Panel1.Controls.Add(Me.cbApprovedBy)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.lblApprovedBy)
        Me.Panel1.Controls.Add(Me.dtpFilterClosedFrom)
        Me.Panel1.Controls.Add(Me.btnFilterCorrectedDatesCleared)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.dtpFilterCorrectedTo)
        Me.Panel1.Controls.Add(Me.dtpFilterClosedTo)
        Me.Panel1.Controls.Add(Me.lblFilterCorrectedTo)
        Me.Panel1.Controls.Add(Me.gbFilterReportCatagory)
        Me.Panel1.Controls.Add(Me.dtpFilterCorrectedFrom)
        Me.Panel1.Controls.Add(Me.txtFilterNewIDTo)
        Me.Panel1.Controls.Add(Me.lblFilterCorrectedFrom)
        Me.Panel1.Controls.Add(Me.lblFilterNewIDTo)
        Me.Panel1.Controls.Add(Me.lblFilterProjectLead)
        Me.Panel1.Controls.Add(Me.lblEUTType)
        Me.Panel1.Controls.Add(Me.cbFilterAssignedTo)
        Me.Panel1.Controls.Add(Me.cbFilterEUTType)
        Me.Panel1.Controls.Add(Me.lblFilterCorrectedBy)
        Me.Panel1.Controls.Add(Me.gbFilterEUT2)
        Me.Panel1.Controls.Add(Me.cbFilterCorrectedBy)
        Me.Panel1.Controls.Add(Me.gbFilterEUT_1)
        Me.Panel1.Controls.Add(Me.btnFilterFailedDatesClear)
        Me.Panel1.Controls.Add(Me.gbShowReportsToBeApproved)
        Me.Panel1.Controls.Add(Me.btnFilterApprovedDatesClear)
        Me.Panel1.Controls.Add(Me.btnFilterClosedDatesClear)
        Me.Panel1.Controls.Add(Me.cbFilterProjectName)
        Me.Panel1.Controls.Add(Me.lblFilterProjectNumber)
        Me.Panel1.Controls.Add(Me.lblProjectName)
        Me.Panel1.Controls.Add(Me.cbFilterProjectNumber)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1002, 494)
        Me.Panel1.TabIndex = 0
        '
        'gbFilterControls
        '
        Me.gbFilterControls.Controls.Add(Me.chkFilterTransferedReportsOnly)
        Me.gbFilterControls.Controls.Add(Me.chkTCCReviewRequired)
        Me.gbFilterControls.Controls.Add(Me.ChkPreserveFilterAndOr)
        Me.gbFilterControls.Controls.Add(Me.chkFilterAllowAdvancedEditing)
        Me.gbFilterControls.Controls.Add(Me.chkPreserveFilter)
        Me.gbFilterControls.Controls.Add(Me.btnFilterBuild)
        Me.gbFilterControls.Controls.Add(Me.btnFilterClear)
        Me.gbFilterControls.Controls.Add(Me.BtnFilterRemove)
        Me.gbFilterControls.Controls.Add(Me.btnFilterReset)
        Me.gbFilterControls.Controls.Add(Me.btnFilterApply)
        Me.gbFilterControls.Controls.Add(Me.chkFilterOpenReportsOnly)
        Me.gbFilterControls.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.gbFilterControls.Location = New System.Drawing.Point(736, 345)
        Me.gbFilterControls.Name = "gbFilterControls"
        Me.gbFilterControls.Size = New System.Drawing.Size(259, 146)
        Me.gbFilterControls.TabIndex = 320
        Me.gbFilterControls.TabStop = False
        Me.gbFilterControls.Text = "Filter"
        '
        'chkTCCReviewRequired
        '
        Me.chkTCCReviewRequired.AutoSize = True
        Me.chkTCCReviewRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkTCCReviewRequired.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkTCCReviewRequired.Location = New System.Drawing.Point(89, 11)
        Me.chkTCCReviewRequired.Name = "chkTCCReviewRequired"
        Me.chkTCCReviewRequired.Size = New System.Drawing.Size(143, 19)
        Me.chkTCCReviewRequired.TabIndex = 321
        Me.chkTCCReviewRequired.Text = "Only TCC Reviewable"
        Me.chkTCCReviewRequired.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTCCReviewRequired.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtFilterDisplay, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.dgvFilterFailureReport, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 503)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.82759!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.17242!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1002, 232)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'dgvFilterFailureReport
        '
        Me.dgvFilterFailureReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFilterFailureReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFilterFailureReport.Location = New System.Drawing.Point(3, 49)
        Me.dgvFilterFailureReport.Name = "dgvFilterFailureReport"
        Me.dgvFilterFailureReport.Size = New System.Drawing.Size(996, 180)
        Me.dgvFilterFailureReport.TabIndex = 318
        '
        'chkFilterTransferedReportsOnly
        '
        Me.chkFilterTransferedReportsOnly.AutoSize = True
        Me.chkFilterTransferedReportsOnly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkFilterTransferedReportsOnly.Location = New System.Drawing.Point(40, 48)
        Me.chkFilterTransferedReportsOnly.Name = "chkFilterTransferedReportsOnly"
        Me.chkFilterTransferedReportsOnly.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkFilterTransferedReportsOnly.Size = New System.Drawing.Size(192, 19)
        Me.chkFilterTransferedReportsOnly.TabIndex = 323
        Me.chkFilterTransferedReportsOnly.Text = "Show Transfered Reports Only"
        Me.chkFilterTransferedReportsOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFilterTransferedReportsOnly.UseVisualStyleBackColor = True
        '
        'frmFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 762)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FR Filter                                                                        " & _
            "                                                                                " & _
            "                 "
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.gbFilterReportCatagory.ResumeLayout(False)
        Me.gbFilterReportCatagory.PerformLayout()
        Me.gbShowReportsToBeApproved.ResumeLayout(False)
        Me.gbShowReportsToBeApproved.PerformLayout()
        Me.gbFilterEUT_1.ResumeLayout(False)
        Me.gbFilterEUT_1.PerformLayout()
        Me.gbFilterEUT2.ResumeLayout(False)
        Me.gbFilterEUT2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbFilterControls.ResumeLayout(False)
        Me.gbFilterControls.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.dgvFilterFailureReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkFilterPAC1toApprove As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFilterClosedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpFilterClosedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpFilterApprovedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFilterApprovedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkPAC2ToApprove As System.Windows.Forms.CheckBox
    Friend WithEvents btnFilterReset As System.Windows.Forms.Button
    Friend WithEvents btnFilterApply As System.Windows.Forms.Button
    Friend WithEvents dtpFilterFailedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTestName As System.Windows.Forms.Label
    Friend WithEvents dtpFilterFailedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFilterLevel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFilterNewIDFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblFilterReportNumber As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rbReportCatAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterAnomely As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterFailure As System.Windows.Forms.RadioButton
    Friend WithEvents gbFilterReportCatagory As System.Windows.Forms.GroupBox
    Friend WithEvents txtFilterNewIDTo As System.Windows.Forms.TextBox
    Friend WithEvents lblFilterNewIDTo As System.Windows.Forms.Label
    Friend WithEvents lblFilterProjectLead As System.Windows.Forms.Label
    Friend WithEvents lblFilterCorrectedBy As System.Windows.Forms.Label
    Friend WithEvents btnFilterFailedDatesClear As System.Windows.Forms.Button
    Friend WithEvents btnFilterApprovedDatesClear As System.Windows.Forms.Button
    Friend WithEvents btnFilterClosedDatesClear As System.Windows.Forms.Button
    Friend WithEvents lblFilterProjectNumber As System.Windows.Forms.Label
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents chkFilterOpenReportsOnly As System.Windows.Forms.CheckBox
    Friend WithEvents gbShowReportsToBeApproved As System.Windows.Forms.GroupBox
    Friend WithEvents chkOEMToApprove As System.Windows.Forms.CheckBox
    Friend WithEvents chkEngineeringToApporve As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCITtoApprove As System.Windows.Forms.CheckBox
    Friend WithEvents chkFPAToApprove As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMeterVoltage As System.Windows.Forms.Label
    Friend WithEvents lblMeterPCBA_PN As System.Windows.Forms.Label
    Friend WithEvents lblMeterSerialNumber As System.Windows.Forms.Label
    Friend WithEvents lblMeterSoftwareRev As System.Windows.Forms.Label
    Friend WithEvents lblMeterSoftware As System.Windows.Forms.Label
    Friend WithEvents lblMeterPCBA_Rev As System.Windows.Forms.Label
    Friend WithEvents lblMeterFW_Ver As System.Windows.Forms.Label
    Friend WithEvents lblMeter_DSP_Rev As System.Windows.Forms.Label
    Friend WithEvents lblMeterSubType As System.Windows.Forms.Label
    Friend WithEvents lblMeterType As System.Windows.Forms.Label
    Friend WithEvents lblMeterModel As System.Windows.Forms.Label
    Friend WithEvents lblMeterManufacturer As System.Windows.Forms.Label
    Friend WithEvents lblForm As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SUBtypeIII As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SubTypeII As System.Windows.Forms.Label
    Friend WithEvents lblAMRVoltage As System.Windows.Forms.Label
    Friend WithEvents lblAMR_PCBA As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SerialNumber As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Software_Rev As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Software As System.Windows.Forms.Label
    Friend WithEvents lblAMR_PCBA_Rev As System.Windows.Forms.Label
    Friend WithEvents lblAMR_FW_REV As System.Windows.Forms.Label
    Friend WithEvents lblAMR_IP_LAN_ID As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SubType As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Type As System.Windows.Forms.Label
    Friend WithEvents lblAMR_MODEL As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Manufacturer As System.Windows.Forms.Label
    Friend WithEvents gbFilterEUT_1 As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilterEUT2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblEUTType As System.Windows.Forms.Label
    Friend WithEvents btnFilterClear As System.Windows.Forms.Button
    Friend WithEvents btnFilterCorrectedDatesCleared As System.Windows.Forms.Button
    Friend WithEvents dtpFilterCorrectedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFilterCorrectedTo As System.Windows.Forms.Label
    Friend WithEvents dtpFilterCorrectedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFilterCorrectedFrom As System.Windows.Forms.Label
    Friend WithEvents chkMeterVoltageAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterSoftwareRevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterSoftwareAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterPCBA_RevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterPCBAAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterFW_VerAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterDSP_RevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterFormAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterSerialNumberAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterSubTypeIIAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterSubTypeAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterTypeAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterModelAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkMeterManufacturerAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_VoltageAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_Software_RevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_SoftwareAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_PCBA_RevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_PCBA_PNAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_FW_RevAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_IP_LAN_IDAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_SerialNumberAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_SUBtypeIIIAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_SubTypeIIAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_SubTypeAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_TYPEAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_ModelAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkAMR_ManufacturerAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents lblApprovedBy As System.Windows.Forms.Label
    Friend WithEvents chkTestLevelAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterEUTTypeAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterCorrectedByAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterProjectNameAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterAssignedToAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterProjectNumberAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilteredFailedFromAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterCorrectedFromAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterClosedFromAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkApprovedByAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterApprovedFromAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterTestsAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents lblTestType As System.Windows.Forms.Label
    Friend WithEvents rbReviewAllREadyForReview As System.Windows.Forms.RadioButton
    Friend WithEvents rbDisableReadyForReviewFilter As System.Windows.Forms.RadioButton
    Friend WithEvents rbSelectReadyForReview As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComboBoxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseFRHistoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseDefaultValuesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UseFRHistoryNoFilterToolStripMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFilterBuild As System.Windows.Forms.Button
    Friend WithEvents txtFilterDisplay As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnFilterRemove As System.Windows.Forms.Button
    Friend WithEvents chkFilterTestTypeAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterControls As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterAllowAdvancedEditing As System.Windows.Forms.CheckBox
    Friend WithEvents chkPreserveFilter As System.Windows.Forms.CheckBox
    Friend WithEvents ChkPreserveFilterAndOr As System.Windows.Forms.CheckBox
    Friend WithEvents dgvFilterFailureReport As System.Windows.Forms.DataGridView
    Friend WithEvents ManageDatagridviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbFilterTests As xboXComboBox
    Friend WithEvents cbFilterLevel As xboXComboBox
    Friend WithEvents cbFilterAssignedTo As xboXComboBox
    Friend WithEvents cbFilterCorrectedBy As xboXComboBox
    Friend WithEvents cbFilterProjectNumber As xboXComboBox
    Friend WithEvents cbFilterProjectName As xboXComboBox
    Friend WithEvents cbMeterSubTypeII As xboXComboBox
    Friend WithEvents cbMeterVoltage As xboXComboBox
    Friend WithEvents cbMeterBase As xboXComboBox
    Friend WithEvents cbMeterPCBA As xboXComboBox
    Friend WithEvents cbMeterSerialNumber As xboXComboBox
    Friend WithEvents cbMeterSoftwareRev As xboXComboBox
    Friend WithEvents cbMeterSoftware As xboXComboBox
    Friend WithEvents cbMeterPCBA_Rev As xboXComboBox
    Friend WithEvents cbMeterDSP_Rev As xboXComboBox
    Friend WithEvents cbMeterSubType As xboXComboBox
    Friend WithEvents cbMeterType As xboXComboBox
    Friend WithEvents cbMeterManufacturer As xboXComboBox
    Friend WithEvents cbMeterModel As xboXComboBox
    Friend WithEvents cbMeterForm As xboXComboBox
    Friend WithEvents cbAMR_SUBtypeIII As xboXComboBox
    Friend WithEvents cbAMR_SUBtypeII As xboXComboBox
    Friend WithEvents cbAMR_Voltage As xboXComboBox
    Friend WithEvents cbAMR_PCBA_PN As xboXComboBox
    Friend WithEvents cbAMR_SerialNumber As xboXComboBox
    Friend WithEvents cbAMR_Software_Rev As xboXComboBox
    Friend WithEvents cbAMR_Software As xboXComboBox
    Friend WithEvents cbAMR_PCBA_Rev As xboXComboBox
    Friend WithEvents cbAMR_IP_LAN_ID As xboXComboBox
    Friend WithEvents cbAMR_SubType As xboXComboBox
    Friend WithEvents cbAMR_TYPE As xboXComboBox
    Friend WithEvents cbAMR_Manufacturer As xboXComboBox
    Friend WithEvents cbAMR_Model As xboXComboBox
    Friend WithEvents cbAMR_FW_Rev As xboXComboBox
    Friend WithEvents cbFilterEUTType As xboXComboBox
    Friend WithEvents cbMeterFW_Ver As xboXComboBox
    Friend WithEvents cbApprovedBy As xboXComboBox
    Friend WithEvents cbFilterTestType As xboXComboBox
    Friend WithEvents chkTCCReviewRequired As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterTransferedReportsOnly As System.Windows.Forms.CheckBox
End Class
