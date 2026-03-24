<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Me.lblBase = New System.Windows.Forms.Label()
        Me.cbMeterSoftwareRev = New xboXComboBox()
        Me.cbMeterSoftware = New xboXComboBox()
        Me.cbMeterVoltage = New xboXComboBox()
        Me.cbMeterPCBA_Rev = New xboXComboBox()
        Me.cbMeterFW_Ver = New xboXComboBox()
        Me.cbMeterManufacturer = New xboXComboBox()
        Me.lblMeterPCBA_Rev = New System.Windows.Forms.Label()
        Me.lblMeterSoftware = New System.Windows.Forms.Label()
        Me.lblMeterSoftwareRev = New System.Windows.Forms.Label()
        Me.cbMeterPCBA = New xboXComboBox()
        Me.lblMeterFW_Ver = New System.Windows.Forms.Label()
        Me.cbMeterForm = New xboXComboBox()
        Me.lblMeterManufacturer = New System.Windows.Forms.Label()
        Me.cbMeterModel = New xboXComboBox()
        Me.lblMeterVoltage = New System.Windows.Forms.Label()
        Me.cbMeterSubType = New xboXComboBox()
        Me.cbMeterType = New xboXComboBox()
        Me.lblMeter_DSP_Rev = New System.Windows.Forms.Label()
        Me.cbMeterDSP_Rev = New xboXComboBox()
        Me.lblMeterPCBA_PN = New System.Windows.Forms.Label()
        Me.lblForm = New System.Windows.Forms.Label()
        Me.lblMeterModel = New System.Windows.Forms.Label()
        Me.lblMeterType = New System.Windows.Forms.Label()
        Me.cbMeterSerialNumber = New xboXComboBox()
        Me.cbMeterBase = New xboXComboBox()
        Me.lblMeterSubType = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMeterSerialNumber = New System.Windows.Forms.Label()
        Me.cbMeterSubTypeII = New xboXComboBox()
        Me.lblMeter = New System.Windows.Forms.Label()
        Me.rtxtMeterNotes = New System.Windows.Forms.RichTextBox()
        Me.lblMeterNotes = New System.Windows.Forms.Label()
        Me.cbAMR_Voltage = New xboXComboBox()
        Me.cbAMR_Software_Rev = New xboXComboBox()
        Me.cbAMR_Software = New xboXComboBox()
        Me.cbAMR_PCBA_Rev = New xboXComboBox()
        Me.cbAMR_FW_Rev = New xboXComboBox()
        Me.lblAMR_Manufacturer = New System.Windows.Forms.Label()
        Me.cbAMR_IP_LAN_ID = New xboXComboBox()
        Me.lblAMR_IP_LAN_ID = New System.Windows.Forms.Label()
        Me.cbAMR_PCBA = New xboXComboBox()
        Me.lblAMR_PCBA_Rev = New System.Windows.Forms.Label()
        Me.lblAMR_Software = New System.Windows.Forms.Label()
        Me.lblAMR_Software_Rev = New System.Windows.Forms.Label()
        Me.lblAMR_FW_REV = New System.Windows.Forms.Label()
        Me.cbAMR_TYPE = New xboXComboBox()
        Me.lblAMRVoltage = New System.Windows.Forms.Label()
        Me.lblAMR_MODEL = New System.Windows.Forms.Label()
        Me.cbAMR_Manufacturer = New xboXComboBox()
        Me.lblAMR_Type = New System.Windows.Forms.Label()
        Me.lblAMR_SubType = New System.Windows.Forms.Label()
        Me.lblAMR_PCBA = New System.Windows.Forms.Label()
        Me.lblAMR_SubTypeII = New System.Windows.Forms.Label()
        Me.lblAMR_SUBtypeIII = New System.Windows.Forms.Label()
        Me.lblAMR_SerialNumber = New System.Windows.Forms.Label()
        Me.cbAMR_SubType = New xboXComboBox()
        Me.cbAMR_SerialNumber = New xboXComboBox()
        Me.cbAMR_Model = New xboXComboBox()
        Me.cbAMR_SUBtypeIII = New xboXComboBox()
        Me.cbAMR_SUBtypeII = New xboXComboBox()
        Me.lblAMR = New System.Windows.Forms.Label()
        Me.lblAMR_Notes = New System.Windows.Forms.Label()
        Me.rtxtAMR_Notes = New System.Windows.Forms.RichTextBox()
        Me.txtReportNumber = New System.Windows.Forms.TextBox()
        Me.lblReportNumber = New System.Windows.Forms.Label()
        Me.txtProject = New System.Windows.Forms.TextBox()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.lblTestName = New System.Windows.Forms.Label()
        Me.txtTest = New System.Windows.Forms.TextBox()
        Me.lblTestType = New System.Windows.Forms.Label()
        Me.cbTestType = New xboXComboBox()
        Me.cbProjectLead = New xboXComboBox()
        Me.lblAssignedTo = New System.Windows.Forms.Label()
        Me.txtProjectNumber = New System.Windows.Forms.TextBox()
        Me.lblProjectNumber = New System.Windows.Forms.Label()
        Me.cbEUTType = New xboXComboBox()
        Me.lblEUTType = New System.Windows.Forms.Label()
        Me.cbTestLevel = New xboXComboBox()
        Me.lblTestLevel = New System.Windows.Forms.Label()
        Me.lblTestedby = New System.Windows.Forms.Label()
        Me.cbTestedBy = New xboXComboBox()
        Me.lblDateTested = New System.Windows.Forms.Label()
        Me.mtxtDateFailed = New System.Windows.Forms.MaskedTextBox()
        Me.chkTCC_ApprovalRequired = New System.Windows.Forms.CheckBox()
        Me.txtCorrectedBy = New System.Windows.Forms.TextBox()
        Me.lblCorrectedBy = New System.Windows.Forms.Label()
        Me.mtxtDateCorrected = New System.Windows.Forms.MaskedTextBox()
        Me.lblDateCorrected = New System.Windows.Forms.Label()
        Me.lblFR_Approvals = New System.Windows.Forms.Label()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.lblTCC_Approvals = New System.Windows.Forms.Label()
        Me.txtApprovedBy = New System.Windows.Forms.TextBox()
        Me.mtxtDateApproved = New System.Windows.Forms.MaskedTextBox()
        Me.lblTCC_Compliance = New System.Windows.Forms.Label()
        Me.lblDateApproved = New System.Windows.Forms.Label()
        Me.cbTCC_1_Compliance = New xboXComboBox()
        Me.lblTCC_Engineering = New System.Windows.Forms.Label()
        Me.lblTCC_Manufacturing = New System.Windows.Forms.Label()
        Me.cbTCC_2_Engineering = New xboXComboBox()
        Me.cbTCC_5_Quality = New xboXComboBox()
        Me.lblTCC_Quality = New System.Windows.Forms.Label()
        Me.cbTCC_4_Product_Management = New xboXComboBox()
        Me.cbTCC_3_Manufacturing = New xboXComboBox()
        Me.lblTCC_ProductMangement = New System.Windows.Forms.Label()
        Me.chkAnomely = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblBase
        '
        Me.lblBase.AutoSize = True
        Me.lblBase.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBase.Location = New System.Drawing.Point(48, 537)
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(36, 15)
        Me.lblBase.TabIndex = 234
        Me.lblBase.Text = "Base"
        Me.lblBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterSoftwareRev
        '
        Me.cbMeterSoftwareRev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSoftwareRev.Location = New System.Drawing.Point(273, 479)
        Me.cbMeterSoftwareRev.MaxDropDownItems = 20
        Me.cbMeterSoftwareRev.Name = "cbMeterSoftwareRev"
        Me.cbMeterSoftwareRev.ReadOnly = True
        Me.cbMeterSoftwareRev.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterSoftwareRev.TabIndex = 20
        Me.cbMeterSoftwareRev.TabStop = False
        Me.cbMeterSoftwareRev.Tag = "Meter_Software_REv"
        '
        'cbMeterSoftware
        '
        Me.cbMeterSoftware.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSoftware.Location = New System.Drawing.Point(273, 451)
        Me.cbMeterSoftware.MaxDropDownItems = 20
        Me.cbMeterSoftware.Name = "cbMeterSoftware"
        Me.cbMeterSoftware.ReadOnly = True
        Me.cbMeterSoftware.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterSoftware.TabIndex = 19
        Me.cbMeterSoftware.TabStop = False
        Me.cbMeterSoftware.Tag = "Meter_Software"
        '
        'cbMeterVoltage
        '
        Me.cbMeterVoltage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterVoltage.Location = New System.Drawing.Point(273, 507)
        Me.cbMeterVoltage.MaxDropDownItems = 20
        Me.cbMeterVoltage.Name = "cbMeterVoltage"
        Me.cbMeterVoltage.ReadOnly = True
        Me.cbMeterVoltage.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterVoltage.TabIndex = 163
        Me.cbMeterVoltage.TabStop = False
        Me.cbMeterVoltage.Tag = "Meter_SubType"
        '
        'cbMeterPCBA_Rev
        '
        Me.cbMeterPCBA_Rev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterPCBA_Rev.Location = New System.Drawing.Point(273, 423)
        Me.cbMeterPCBA_Rev.MaxDropDownItems = 20
        Me.cbMeterPCBA_Rev.Name = "cbMeterPCBA_Rev"
        Me.cbMeterPCBA_Rev.ReadOnly = True
        Me.cbMeterPCBA_Rev.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterPCBA_Rev.TabIndex = 18
        Me.cbMeterPCBA_Rev.TabStop = False
        Me.cbMeterPCBA_Rev.Tag = "Meter_PCBA_Rev"
        '
        'cbMeterFW_Ver
        '
        Me.cbMeterFW_Ver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterFW_Ver.Location = New System.Drawing.Point(273, 367)
        Me.cbMeterFW_Ver.Name = "cbMeterFW_Ver"
        Me.cbMeterFW_Ver.ReadOnly = True
        Me.cbMeterFW_Ver.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterFW_Ver.TabIndex = 16
        Me.cbMeterFW_Ver.TabStop = False
        Me.cbMeterFW_Ver.Tag = "FW Ver"
        '
        'cbMeterManufacturer
        '
        Me.cbMeterManufacturer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterManufacturer.Location = New System.Drawing.Point(92, 339)
        Me.cbMeterManufacturer.MaxDropDownItems = 20
        Me.cbMeterManufacturer.Name = "cbMeterManufacturer"
        Me.cbMeterManufacturer.ReadOnly = True
        Me.cbMeterManufacturer.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterManufacturer.TabIndex = 10
        Me.cbMeterManufacturer.TabStop = False
        Me.cbMeterManufacturer.Tag = "Meter_Manufacturer"
        '
        'lblMeterPCBA_Rev
        '
        Me.lblMeterPCBA_Rev.AutoSize = True
        Me.lblMeterPCBA_Rev.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterPCBA_Rev.Location = New System.Drawing.Point(204, 425)
        Me.lblMeterPCBA_Rev.Name = "lblMeterPCBA_Rev"
        Me.lblMeterPCBA_Rev.Size = New System.Drawing.Size(63, 15)
        Me.lblMeterPCBA_Rev.TabIndex = 138
        Me.lblMeterPCBA_Rev.Text = "PCBA Rev"
        Me.lblMeterPCBA_Rev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMeterSoftware
        '
        Me.lblMeterSoftware.AutoSize = True
        Me.lblMeterSoftware.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSoftware.Location = New System.Drawing.Point(208, 453)
        Me.lblMeterSoftware.Name = "lblMeterSoftware"
        Me.lblMeterSoftware.Size = New System.Drawing.Size(59, 15)
        Me.lblMeterSoftware.TabIndex = 140
        Me.lblMeterSoftware.Tag = ""
        Me.lblMeterSoftware.Text = "Software"
        Me.lblMeterSoftware.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMeterSoftwareRev
        '
        Me.lblMeterSoftwareRev.AutoSize = True
        Me.lblMeterSoftwareRev.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSoftwareRev.Location = New System.Drawing.Point(216, 481)
        Me.lblMeterSoftwareRev.Name = "lblMeterSoftwareRev"
        Me.lblMeterSoftwareRev.Size = New System.Drawing.Size(51, 15)
        Me.lblMeterSoftwareRev.TabIndex = 142
        Me.lblMeterSoftwareRev.Tag = ""
        Me.lblMeterSoftwareRev.Text = "SW Rev"
        Me.lblMeterSoftwareRev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterPCBA
        '
        Me.cbMeterPCBA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterPCBA.Location = New System.Drawing.Point(273, 395)
        Me.cbMeterPCBA.MaxDropDownItems = 20
        Me.cbMeterPCBA.Name = "cbMeterPCBA"
        Me.cbMeterPCBA.ReadOnly = True
        Me.cbMeterPCBA.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterPCBA.TabIndex = 17
        Me.cbMeterPCBA.TabStop = False
        Me.cbMeterPCBA.Tag = "Meter_PCBA"
        '
        'lblMeterFW_Ver
        '
        Me.lblMeterFW_Ver.AutoSize = True
        Me.lblMeterFW_Ver.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterFW_Ver.Location = New System.Drawing.Point(218, 369)
        Me.lblMeterFW_Ver.Name = "lblMeterFW_Ver"
        Me.lblMeterFW_Ver.Size = New System.Drawing.Size(49, 15)
        Me.lblMeterFW_Ver.TabIndex = 136
        Me.lblMeterFW_Ver.Text = "FW Rev"
        Me.lblMeterFW_Ver.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterForm
        '
        Me.cbMeterForm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterForm.Location = New System.Drawing.Point(92, 507)
        Me.cbMeterForm.Name = "cbMeterForm"
        Me.cbMeterForm.ReadOnly = True
        Me.cbMeterForm.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterForm.TabIndex = 14
        Me.cbMeterForm.TabStop = False
        Me.cbMeterForm.Tag = "Form"
        '
        'lblMeterManufacturer
        '
        Me.lblMeterManufacturer.AutoSize = True
        Me.lblMeterManufacturer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterManufacturer.Location = New System.Drawing.Point(45, 341)
        Me.lblMeterManufacturer.Name = "lblMeterManufacturer"
        Me.lblMeterManufacturer.Size = New System.Drawing.Size(42, 15)
        Me.lblMeterManufacturer.TabIndex = 126
        Me.lblMeterManufacturer.Text = "Manuf"
        Me.lblMeterManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterModel
        '
        Me.cbMeterModel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterModel.Location = New System.Drawing.Point(92, 367)
        Me.cbMeterModel.MaxDropDownItems = 20
        Me.cbMeterModel.Name = "cbMeterModel"
        Me.cbMeterModel.ReadOnly = True
        Me.cbMeterModel.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterModel.TabIndex = 9
        Me.cbMeterModel.TabStop = False
        Me.cbMeterModel.Tag = "Meter"
        '
        'lblMeterVoltage
        '
        Me.lblMeterVoltage.AutoSize = True
        Me.lblMeterVoltage.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterVoltage.Location = New System.Drawing.Point(218, 509)
        Me.lblMeterVoltage.Name = "lblMeterVoltage"
        Me.lblMeterVoltage.Size = New System.Drawing.Size(49, 15)
        Me.lblMeterVoltage.TabIndex = 164
        Me.lblMeterVoltage.Tag = ""
        Me.lblMeterVoltage.Text = "Voltage"
        Me.lblMeterVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterSubType
        '
        Me.cbMeterSubType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSubType.Location = New System.Drawing.Point(92, 423)
        Me.cbMeterSubType.MaxDropDownItems = 20
        Me.cbMeterSubType.Name = "cbMeterSubType"
        Me.cbMeterSubType.ReadOnly = True
        Me.cbMeterSubType.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterSubType.TabIndex = 13
        Me.cbMeterSubType.TabStop = False
        Me.cbMeterSubType.Tag = "Meter_SubType"
        '
        'cbMeterType
        '
        Me.cbMeterType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterType.Location = New System.Drawing.Point(92, 395)
        Me.cbMeterType.MaxDropDownItems = 20
        Me.cbMeterType.Name = "cbMeterType"
        Me.cbMeterType.ReadOnly = True
        Me.cbMeterType.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterType.TabIndex = 12
        Me.cbMeterType.TabStop = False
        Me.cbMeterType.Tag = "Meter_Type"
        '
        'lblMeter_DSP_Rev
        '
        Me.lblMeter_DSP_Rev.AutoSize = True
        Me.lblMeter_DSP_Rev.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeter_DSP_Rev.Location = New System.Drawing.Point(212, 341)
        Me.lblMeter_DSP_Rev.Name = "lblMeter_DSP_Rev"
        Me.lblMeter_DSP_Rev.Size = New System.Drawing.Size(55, 15)
        Me.lblMeter_DSP_Rev.TabIndex = 134
        Me.lblMeter_DSP_Rev.Text = "DSP Rev"
        Me.lblMeter_DSP_Rev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterDSP_Rev
        '
        Me.cbMeterDSP_Rev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterDSP_Rev.Location = New System.Drawing.Point(273, 339)
        Me.cbMeterDSP_Rev.MaxDropDownItems = 20
        Me.cbMeterDSP_Rev.Name = "cbMeterDSP_Rev"
        Me.cbMeterDSP_Rev.ReadOnly = True
        Me.cbMeterDSP_Rev.Size = New System.Drawing.Size(111, 22)
        Me.cbMeterDSP_Rev.TabIndex = 15
        Me.cbMeterDSP_Rev.TabStop = False
        Me.cbMeterDSP_Rev.Tag = "Meter_DSP_REV"
        '
        'lblMeterPCBA_PN
        '
        Me.lblMeterPCBA_PN.AutoSize = True
        Me.lblMeterPCBA_PN.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterPCBA_PN.Location = New System.Drawing.Point(209, 397)
        Me.lblMeterPCBA_PN.Name = "lblMeterPCBA_PN"
        Me.lblMeterPCBA_PN.Size = New System.Drawing.Size(58, 15)
        Me.lblMeterPCBA_PN.TabIndex = 198
        Me.lblMeterPCBA_PN.Text = "PCBA PN"
        Me.lblMeterPCBA_PN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblForm
        '
        Me.lblForm.AutoSize = True
        Me.lblForm.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForm.Location = New System.Drawing.Point(48, 509)
        Me.lblForm.Name = "lblForm"
        Me.lblForm.Size = New System.Drawing.Size(36, 15)
        Me.lblForm.TabIndex = 128
        Me.lblForm.Text = "Form"
        Me.lblForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMeterModel
        '
        Me.lblMeterModel.AutoSize = True
        Me.lblMeterModel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterModel.Location = New System.Drawing.Point(46, 369)
        Me.lblMeterModel.Name = "lblMeterModel"
        Me.lblMeterModel.Size = New System.Drawing.Size(41, 15)
        Me.lblMeterModel.TabIndex = 128
        Me.lblMeterModel.Text = "Model"
        Me.lblMeterModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMeterType
        '
        Me.lblMeterType.AutoSize = True
        Me.lblMeterType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterType.Location = New System.Drawing.Point(54, 395)
        Me.lblMeterType.Name = "lblMeterType"
        Me.lblMeterType.Size = New System.Drawing.Size(33, 15)
        Me.lblMeterType.TabIndex = 130
        Me.lblMeterType.Tag = ""
        Me.lblMeterType.Text = "Type"
        Me.lblMeterType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterSerialNumber
        '
        Me.cbMeterSerialNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSerialNumber.Location = New System.Drawing.Point(92, 479)
        Me.cbMeterSerialNumber.MaxDropDownItems = 20
        Me.cbMeterSerialNumber.Name = "cbMeterSerialNumber"
        Me.cbMeterSerialNumber.ReadOnly = True
        Me.cbMeterSerialNumber.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterSerialNumber.TabIndex = 11
        Me.cbMeterSerialNumber.TabStop = False
        Me.cbMeterSerialNumber.Tag = "Meter_Serial_Number"
        '
        'cbMeterBase
        '
        Me.cbMeterBase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterBase.Items.AddRange(New Object() {"", "A", "S", "SC", "K"})
        Me.cbMeterBase.Location = New System.Drawing.Point(92, 535)
        Me.cbMeterBase.Name = "cbMeterBase"
        Me.cbMeterBase.ReadOnly = True
        Me.cbMeterBase.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterBase.TabIndex = 163
        Me.cbMeterBase.TabStop = False
        Me.cbMeterBase.Tag = "Meter_Base"
        '
        'lblMeterSubType
        '
        Me.lblMeterSubType.AutoSize = True
        Me.lblMeterSubType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSubType.Location = New System.Drawing.Point(29, 425)
        Me.lblMeterSubType.Name = "lblMeterSubType"
        Me.lblMeterSubType.Size = New System.Drawing.Size(58, 15)
        Me.lblMeterSubType.TabIndex = 132
        Me.lblMeterSubType.Tag = ""
        Me.lblMeterSubType.Text = "Sub Type"
        Me.lblMeterSubType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 453)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 213
        Me.Label1.Tag = ""
        Me.Label1.Text = "Sub TypeII"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMeterSerialNumber
        '
        Me.lblMeterSerialNumber.AutoSize = True
        Me.lblMeterSerialNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterSerialNumber.Location = New System.Drawing.Point(39, 481)
        Me.lblMeterSerialNumber.Name = "lblMeterSerialNumber"
        Me.lblMeterSerialNumber.Size = New System.Drawing.Size(47, 15)
        Me.lblMeterSerialNumber.TabIndex = 145
        Me.lblMeterSerialNumber.Text = "Serial#"
        Me.lblMeterSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbMeterSubTypeII
        '
        Me.cbMeterSubTypeII.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMeterSubTypeII.Location = New System.Drawing.Point(92, 451)
        Me.cbMeterSubTypeII.MaxDropDownItems = 20
        Me.cbMeterSubTypeII.Name = "cbMeterSubTypeII"
        Me.cbMeterSubTypeII.ReadOnly = True
        Me.cbMeterSubTypeII.Size = New System.Drawing.Size(109, 22)
        Me.cbMeterSubTypeII.TabIndex = 212
        Me.cbMeterSubTypeII.TabStop = False
        Me.cbMeterSubTypeII.Tag = "Meter_SubTypeII"
        '
        'lblMeter
        '
        Me.lblMeter.AutoSize = True
        Me.lblMeter.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeter.Location = New System.Drawing.Point(48, 309)
        Me.lblMeter.Name = "lblMeter"
        Me.lblMeter.Size = New System.Drawing.Size(51, 18)
        Me.lblMeter.TabIndex = 169
        Me.lblMeter.Text = "Meter"
        '
        'rtxtMeterNotes
        '
        Me.rtxtMeterNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtxtMeterNotes.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtMeterNotes.Location = New System.Drawing.Point(90, 567)
        Me.rtxtMeterNotes.MaxLength = 65535
        Me.rtxtMeterNotes.Name = "rtxtMeterNotes"
        Me.rtxtMeterNotes.Size = New System.Drawing.Size(294, 174)
        Me.rtxtMeterNotes.TabIndex = 21
        Me.rtxtMeterNotes.Tag = "Meter_Notes"
        Me.rtxtMeterNotes.Text = ""
        '
        'lblMeterNotes
        '
        Me.lblMeterNotes.AutoSize = True
        Me.lblMeterNotes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeterNotes.Location = New System.Drawing.Point(41, 573)
        Me.lblMeterNotes.Name = "lblMeterNotes"
        Me.lblMeterNotes.Size = New System.Drawing.Size(43, 15)
        Me.lblMeterNotes.TabIndex = 171
        Me.lblMeterNotes.Text = "Notes:"
        Me.lblMeterNotes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbAMR_Voltage
        '
        Me.cbAMR_Voltage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Voltage.Location = New System.Drawing.Point(669, 507)
        Me.cbAMR_Voltage.MaxDropDownItems = 20
        Me.cbAMR_Voltage.Name = "cbAMR_Voltage"
        Me.cbAMR_Voltage.ReadOnly = True
        Me.cbAMR_Voltage.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_Voltage.TabIndex = 183
        Me.cbAMR_Voltage.TabStop = False
        Me.cbAMR_Voltage.Tag = "AMR_Voltage"
        '
        'cbAMR_Software_Rev
        '
        Me.cbAMR_Software_Rev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Software_Rev.Location = New System.Drawing.Point(669, 479)
        Me.cbAMR_Software_Rev.MaxDropDownItems = 20
        Me.cbAMR_Software_Rev.Name = "cbAMR_Software_Rev"
        Me.cbAMR_Software_Rev.ReadOnly = True
        Me.cbAMR_Software_Rev.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_Software_Rev.TabIndex = 32
        Me.cbAMR_Software_Rev.TabStop = False
        Me.cbAMR_Software_Rev.Tag = "AMR_Software_Rev"
        '
        'cbAMR_Software
        '
        Me.cbAMR_Software.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Software.Location = New System.Drawing.Point(669, 451)
        Me.cbAMR_Software.MaxDropDownItems = 20
        Me.cbAMR_Software.Name = "cbAMR_Software"
        Me.cbAMR_Software.ReadOnly = True
        Me.cbAMR_Software.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_Software.TabIndex = 31
        Me.cbAMR_Software.TabStop = False
        Me.cbAMR_Software.Tag = "AMR_Software"
        '
        'cbAMR_PCBA_Rev
        '
        Me.cbAMR_PCBA_Rev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_PCBA_Rev.Location = New System.Drawing.Point(669, 423)
        Me.cbAMR_PCBA_Rev.MaxDropDownItems = 20
        Me.cbAMR_PCBA_Rev.Name = "cbAMR_PCBA_Rev"
        Me.cbAMR_PCBA_Rev.ReadOnly = True
        Me.cbAMR_PCBA_Rev.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_PCBA_Rev.TabIndex = 30
        Me.cbAMR_PCBA_Rev.TabStop = False
        Me.cbAMR_PCBA_Rev.Tag = "AMR_PCBA_Rev"
        '
        'cbAMR_FW_Rev
        '
        Me.cbAMR_FW_Rev.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_FW_Rev.Location = New System.Drawing.Point(669, 367)
        Me.cbAMR_FW_Rev.Name = "cbAMR_FW_Rev"
        Me.cbAMR_FW_Rev.ReadOnly = True
        Me.cbAMR_FW_Rev.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_FW_Rev.TabIndex = 28
        Me.cbAMR_FW_Rev.TabStop = False
        Me.cbAMR_FW_Rev.Tag = "AMR Rev"
        '
        'lblAMR_Manufacturer
        '
        Me.lblAMR_Manufacturer.AutoSize = True
        Me.lblAMR_Manufacturer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Manufacturer.Location = New System.Drawing.Point(435, 341)
        Me.lblAMR_Manufacturer.Name = "lblAMR_Manufacturer"
        Me.lblAMR_Manufacturer.Size = New System.Drawing.Size(42, 15)
        Me.lblAMR_Manufacturer.TabIndex = 174
        Me.lblAMR_Manufacturer.Text = "Manuf"
        Me.lblAMR_Manufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAMR_IP_LAN_ID
        '
        Me.cbAMR_IP_LAN_ID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_IP_LAN_ID.Location = New System.Drawing.Point(669, 339)
        Me.cbAMR_IP_LAN_ID.MaxDropDownItems = 20
        Me.cbAMR_IP_LAN_ID.Name = "cbAMR_IP_LAN_ID"
        Me.cbAMR_IP_LAN_ID.ReadOnly = True
        Me.cbAMR_IP_LAN_ID.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_IP_LAN_ID.TabIndex = 27
        Me.cbAMR_IP_LAN_ID.TabStop = False
        Me.cbAMR_IP_LAN_ID.Tag = "AMR_IP_LAN_ID"
        '
        'lblAMR_IP_LAN_ID
        '
        Me.lblAMR_IP_LAN_ID.AutoSize = True
        Me.lblAMR_IP_LAN_ID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_IP_LAN_ID.Location = New System.Drawing.Point(607, 341)
        Me.lblAMR_IP_LAN_ID.Name = "lblAMR_IP_LAN_ID"
        Me.lblAMR_IP_LAN_ID.Size = New System.Drawing.Size(56, 15)
        Me.lblAMR_IP_LAN_ID.TabIndex = 184
        Me.lblAMR_IP_LAN_ID.Text = "IP/Lan ID"
        Me.lblAMR_IP_LAN_ID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAMR_PCBA
        '
        Me.cbAMR_PCBA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_PCBA.Location = New System.Drawing.Point(669, 395)
        Me.cbAMR_PCBA.MaxDropDownItems = 20
        Me.cbAMR_PCBA.Name = "cbAMR_PCBA"
        Me.cbAMR_PCBA.ReadOnly = True
        Me.cbAMR_PCBA.Size = New System.Drawing.Size(109, 22)
        Me.cbAMR_PCBA.TabIndex = 29
        Me.cbAMR_PCBA.TabStop = False
        Me.cbAMR_PCBA.Tag = "AMR_PCBA"
        '
        'lblAMR_PCBA_Rev
        '
        Me.lblAMR_PCBA_Rev.AutoSize = True
        Me.lblAMR_PCBA_Rev.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_PCBA_Rev.Location = New System.Drawing.Point(600, 425)
        Me.lblAMR_PCBA_Rev.Name = "lblAMR_PCBA_Rev"
        Me.lblAMR_PCBA_Rev.Size = New System.Drawing.Size(63, 15)
        Me.lblAMR_PCBA_Rev.TabIndex = 187
        Me.lblAMR_PCBA_Rev.Text = "PCBA Rev"
        Me.lblAMR_PCBA_Rev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_Software
        '
        Me.lblAMR_Software.AutoSize = True
        Me.lblAMR_Software.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Software.Location = New System.Drawing.Point(604, 451)
        Me.lblAMR_Software.Name = "lblAMR_Software"
        Me.lblAMR_Software.Size = New System.Drawing.Size(59, 15)
        Me.lblAMR_Software.TabIndex = 189
        Me.lblAMR_Software.Tag = ""
        Me.lblAMR_Software.Text = "Software"
        Me.lblAMR_Software.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_Software_Rev
        '
        Me.lblAMR_Software_Rev.AutoSize = True
        Me.lblAMR_Software_Rev.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Software_Rev.Location = New System.Drawing.Point(612, 481)
        Me.lblAMR_Software_Rev.Name = "lblAMR_Software_Rev"
        Me.lblAMR_Software_Rev.Size = New System.Drawing.Size(51, 15)
        Me.lblAMR_Software_Rev.TabIndex = 191
        Me.lblAMR_Software_Rev.Tag = ""
        Me.lblAMR_Software_Rev.Text = "SW Rev"
        Me.lblAMR_Software_Rev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_FW_REV
        '
        Me.lblAMR_FW_REV.AutoSize = True
        Me.lblAMR_FW_REV.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_FW_REV.Location = New System.Drawing.Point(614, 369)
        Me.lblAMR_FW_REV.Name = "lblAMR_FW_REV"
        Me.lblAMR_FW_REV.Size = New System.Drawing.Size(49, 15)
        Me.lblAMR_FW_REV.TabIndex = 186
        Me.lblAMR_FW_REV.Text = "FW Rev"
        Me.lblAMR_FW_REV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAMR_TYPE
        '
        Me.cbAMR_TYPE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_TYPE.Location = New System.Drawing.Point(483, 395)
        Me.cbAMR_TYPE.MaxDropDownItems = 20
        Me.cbAMR_TYPE.Name = "cbAMR_TYPE"
        Me.cbAMR_TYPE.ReadOnly = True
        Me.cbAMR_TYPE.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_TYPE.TabIndex = 25
        Me.cbAMR_TYPE.TabStop = False
        Me.cbAMR_TYPE.Tag = "AMR_Type"
        '
        'lblAMRVoltage
        '
        Me.lblAMRVoltage.AutoSize = True
        Me.lblAMRVoltage.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMRVoltage.Location = New System.Drawing.Point(614, 509)
        Me.lblAMRVoltage.Name = "lblAMRVoltage"
        Me.lblAMRVoltage.Size = New System.Drawing.Size(49, 15)
        Me.lblAMRVoltage.TabIndex = 184
        Me.lblAMRVoltage.Tag = ""
        Me.lblAMRVoltage.Text = "Voltage"
        Me.lblAMRVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_MODEL
        '
        Me.lblAMR_MODEL.AutoSize = True
        Me.lblAMR_MODEL.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_MODEL.Location = New System.Drawing.Point(436, 369)
        Me.lblAMR_MODEL.Name = "lblAMR_MODEL"
        Me.lblAMR_MODEL.Size = New System.Drawing.Size(41, 15)
        Me.lblAMR_MODEL.TabIndex = 177
        Me.lblAMR_MODEL.Text = "Model"
        Me.lblAMR_MODEL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAMR_Manufacturer
        '
        Me.cbAMR_Manufacturer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Manufacturer.Location = New System.Drawing.Point(483, 339)
        Me.cbAMR_Manufacturer.MaxDropDownItems = 20
        Me.cbAMR_Manufacturer.Name = "cbAMR_Manufacturer"
        Me.cbAMR_Manufacturer.ReadOnly = True
        Me.cbAMR_Manufacturer.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_Manufacturer.TabIndex = 23
        Me.cbAMR_Manufacturer.TabStop = False
        Me.cbAMR_Manufacturer.Tag = "AMR_Manufacturer"
        '
        'lblAMR_Type
        '
        Me.lblAMR_Type.AutoSize = True
        Me.lblAMR_Type.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Type.Location = New System.Drawing.Point(444, 397)
        Me.lblAMR_Type.Name = "lblAMR_Type"
        Me.lblAMR_Type.Size = New System.Drawing.Size(33, 15)
        Me.lblAMR_Type.TabIndex = 180
        Me.lblAMR_Type.Tag = ""
        Me.lblAMR_Type.Text = "Type"
        Me.lblAMR_Type.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_SubType
        '
        Me.lblAMR_SubType.AutoSize = True
        Me.lblAMR_SubType.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SubType.Location = New System.Drawing.Point(419, 425)
        Me.lblAMR_SubType.Name = "lblAMR_SubType"
        Me.lblAMR_SubType.Size = New System.Drawing.Size(58, 15)
        Me.lblAMR_SubType.TabIndex = 182
        Me.lblAMR_SubType.Tag = ""
        Me.lblAMR_SubType.Text = "Sub Type"
        Me.lblAMR_SubType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_PCBA
        '
        Me.lblAMR_PCBA.AutoSize = True
        Me.lblAMR_PCBA.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_PCBA.Location = New System.Drawing.Point(605, 397)
        Me.lblAMR_PCBA.Name = "lblAMR_PCBA"
        Me.lblAMR_PCBA.Size = New System.Drawing.Size(58, 15)
        Me.lblAMR_PCBA.TabIndex = 200
        Me.lblAMR_PCBA.Text = "PCBA PN"
        Me.lblAMR_PCBA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_SubTypeII
        '
        Me.lblAMR_SubTypeII.AutoSize = True
        Me.lblAMR_SubTypeII.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SubTypeII.Location = New System.Drawing.Point(413, 453)
        Me.lblAMR_SubTypeII.Name = "lblAMR_SubTypeII"
        Me.lblAMR_SubTypeII.Size = New System.Drawing.Size(64, 15)
        Me.lblAMR_SubTypeII.TabIndex = 184
        Me.lblAMR_SubTypeII.Tag = "AMR_SUBtype_II"
        Me.lblAMR_SubTypeII.Text = "Sub TypeII"
        Me.lblAMR_SubTypeII.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_SUBtypeIII
        '
        Me.lblAMR_SUBtypeIII.AutoSize = True
        Me.lblAMR_SUBtypeIII.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SUBtypeIII.Location = New System.Drawing.Point(413, 481)
        Me.lblAMR_SUBtypeIII.Name = "lblAMR_SUBtypeIII"
        Me.lblAMR_SUBtypeIII.Size = New System.Drawing.Size(67, 15)
        Me.lblAMR_SUBtypeIII.TabIndex = 209
        Me.lblAMR_SUBtypeIII.Tag = "AMR_SUBtype_III"
        Me.lblAMR_SUBtypeIII.Text = "Sub TypeIII"
        Me.lblAMR_SUBtypeIII.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAMR_SerialNumber
        '
        Me.lblAMR_SerialNumber.AutoSize = True
        Me.lblAMR_SerialNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_SerialNumber.Location = New System.Drawing.Point(430, 509)
        Me.lblAMR_SerialNumber.Name = "lblAMR_SerialNumber"
        Me.lblAMR_SerialNumber.Size = New System.Drawing.Size(47, 15)
        Me.lblAMR_SerialNumber.TabIndex = 194
        Me.lblAMR_SerialNumber.Text = "Serial#"
        Me.lblAMR_SerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAMR_SubType
        '
        Me.cbAMR_SubType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SubType.Location = New System.Drawing.Point(483, 423)
        Me.cbAMR_SubType.MaxDropDownItems = 20
        Me.cbAMR_SubType.Name = "cbAMR_SubType"
        Me.cbAMR_SubType.ReadOnly = True
        Me.cbAMR_SubType.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_SubType.TabIndex = 26
        Me.cbAMR_SubType.TabStop = False
        Me.cbAMR_SubType.Tag = "AMR_SubType"
        '
        'cbAMR_SerialNumber
        '
        Me.cbAMR_SerialNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SerialNumber.Location = New System.Drawing.Point(483, 507)
        Me.cbAMR_SerialNumber.MaxDropDownItems = 20
        Me.cbAMR_SerialNumber.Name = "cbAMR_SerialNumber"
        Me.cbAMR_SerialNumber.ReadOnly = True
        Me.cbAMR_SerialNumber.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_SerialNumber.TabIndex = 24
        Me.cbAMR_SerialNumber.TabStop = False
        Me.cbAMR_SerialNumber.Tag = "AMR_SN"
        '
        'cbAMR_Model
        '
        Me.cbAMR_Model.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_Model.Location = New System.Drawing.Point(483, 367)
        Me.cbAMR_Model.Name = "cbAMR_Model"
        Me.cbAMR_Model.ReadOnly = True
        Me.cbAMR_Model.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_Model.TabIndex = 22
        Me.cbAMR_Model.TabStop = False
        Me.cbAMR_Model.Tag = "AMR"
        '
        'cbAMR_SUBtypeIII
        '
        Me.cbAMR_SUBtypeIII.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SUBtypeIII.Location = New System.Drawing.Point(483, 479)
        Me.cbAMR_SUBtypeIII.MaxDropDownItems = 20
        Me.cbAMR_SUBtypeIII.Name = "cbAMR_SUBtypeIII"
        Me.cbAMR_SUBtypeIII.ReadOnly = True
        Me.cbAMR_SUBtypeIII.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_SUBtypeIII.TabIndex = 208
        Me.cbAMR_SUBtypeIII.TabStop = False
        Me.cbAMR_SUBtypeIII.Tag = "AMR_SubTypeIII"
        '
        'cbAMR_SUBtypeII
        '
        Me.cbAMR_SUBtypeII.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAMR_SUBtypeII.Location = New System.Drawing.Point(483, 451)
        Me.cbAMR_SUBtypeII.MaxDropDownItems = 20
        Me.cbAMR_SUBtypeII.Name = "cbAMR_SUBtypeII"
        Me.cbAMR_SUBtypeII.ReadOnly = True
        Me.cbAMR_SUBtypeII.Size = New System.Drawing.Size(103, 22)
        Me.cbAMR_SUBtypeII.TabIndex = 183
        Me.cbAMR_SUBtypeII.TabStop = False
        Me.cbAMR_SUBtypeII.Tag = "AMR_SubTypeII"
        '
        'lblAMR
        '
        Me.lblAMR.AutoSize = True
        Me.lblAMR.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR.Location = New System.Drawing.Point(438, 309)
        Me.lblAMR.Name = "lblAMR"
        Me.lblAMR.Size = New System.Drawing.Size(46, 18)
        Me.lblAMR.TabIndex = 195
        Me.lblAMR.Text = "AMR"
        '
        'lblAMR_Notes
        '
        Me.lblAMR_Notes.AutoSize = True
        Me.lblAMR_Notes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAMR_Notes.Location = New System.Drawing.Point(435, 570)
        Me.lblAMR_Notes.Name = "lblAMR_Notes"
        Me.lblAMR_Notes.Size = New System.Drawing.Size(43, 15)
        Me.lblAMR_Notes.TabIndex = 197
        Me.lblAMR_Notes.Text = "Notes:"
        Me.lblAMR_Notes.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'rtxtAMR_Notes
        '
        Me.rtxtAMR_Notes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtxtAMR_Notes.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtAMR_Notes.Location = New System.Drawing.Point(482, 567)
        Me.rtxtAMR_Notes.MaxLength = 65535
        Me.rtxtAMR_Notes.Name = "rtxtAMR_Notes"
        Me.rtxtAMR_Notes.Size = New System.Drawing.Size(296, 177)
        Me.rtxtAMR_Notes.TabIndex = 33
        Me.rtxtAMR_Notes.Tag = "AMR_Notes"
        Me.rtxtAMR_Notes.Text = ""
        '
        'txtReportNumber
        '
        Me.txtReportNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtReportNumber.BackColor = System.Drawing.Color.White
        Me.txtReportNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReportNumber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportNumber.ForeColor = System.Drawing.Color.Red
        Me.txtReportNumber.Location = New System.Drawing.Point(91, 15)
        Me.txtReportNumber.Name = "txtReportNumber"
        Me.txtReportNumber.ReadOnly = True
        Me.txtReportNumber.Size = New System.Drawing.Size(86, 22)
        Me.txtReportNumber.TabIndex = 235
        Me.txtReportNumber.Tag = "New ID"
        '
        'lblReportNumber
        '
        Me.lblReportNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblReportNumber.AutoSize = True
        Me.lblReportNumber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportNumber.Location = New System.Drawing.Point(27, 17)
        Me.lblReportNumber.Name = "lblReportNumber"
        Me.lblReportNumber.Size = New System.Drawing.Size(61, 16)
        Me.lblReportNumber.TabIndex = 236
        Me.lblReportNumber.Text = "Report#:"
        '
        'txtProject
        '
        Me.txtProject.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProject.Location = New System.Drawing.Point(92, 67)
        Me.txtProject.Multiline = True
        Me.txtProject.Name = "txtProject"
        Me.txtProject.ReadOnly = True
        Me.txtProject.Size = New System.Drawing.Size(292, 38)
        Me.txtProject.TabIndex = 6
        Me.txtProject.Tag = "Project"
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectName.Location = New System.Drawing.Point(3, 70)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(83, 16)
        Me.lblProjectName.TabIndex = 207
        Me.lblProjectName.Text = "Proj. Name:"
        Me.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestName
        '
        Me.lblTestName.AutoSize = True
        Me.lblTestName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestName.Location = New System.Drawing.Point(14, 204)
        Me.lblTestName.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTestName.Name = "lblTestName"
        Me.lblTestName.Size = New System.Drawing.Size(78, 16)
        Me.lblTestName.TabIndex = 135
        Me.lblTestName.Text = "Test Name:"
        Me.lblTestName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTest
        '
        Me.txtTest.Location = New System.Drawing.Point(94, 198)
        Me.txtTest.Multiline = True
        Me.txtTest.Name = "txtTest"
        Me.txtTest.ReadOnly = True
        Me.txtTest.Size = New System.Drawing.Size(290, 54)
        Me.txtTest.TabIndex = 36
        Me.txtTest.Tag = "Test"
        '
        'lblTestType
        '
        Me.lblTestType.AutoSize = True
        Me.lblTestType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestType.Location = New System.Drawing.Point(25, 173)
        Me.lblTestType.Name = "lblTestType"
        Me.lblTestType.Size = New System.Drawing.Size(67, 16)
        Me.lblTestType.TabIndex = 214
        Me.lblTestType.Text = "Test Type"
        Me.lblTestType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTestType
        '
        Me.cbTestType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTestType.Location = New System.Drawing.Point(92, 171)
        Me.cbTestType.Name = "cbTestType"
        Me.cbTestType.ReadOnly = True
        Me.cbTestType.Size = New System.Drawing.Size(108, 24)
        Me.cbTestType.TabIndex = 215
        Me.cbTestType.TabStop = False
        Me.cbTestType.Tag = "Test_Type"
        '
        'cbProjectLead
        '
        Me.cbProjectLead.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectLead.Location = New System.Drawing.Point(91, 111)
        Me.cbProjectLead.Name = "cbProjectLead"
        Me.cbProjectLead.Size = New System.Drawing.Size(293, 24)
        Me.cbProjectLead.TabIndex = 229
        Me.cbProjectLead.Tag = "Project Lead"
        '
        'lblAssignedTo
        '
        Me.lblAssignedTo.AutoSize = True
        Me.lblAssignedTo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssignedTo.Location = New System.Drawing.Point(15, 114)
        Me.lblAssignedTo.Name = "lblAssignedTo"
        Me.lblAssignedTo.Size = New System.Drawing.Size(74, 16)
        Me.lblAssignedTo.TabIndex = 138
        Me.lblAssignedTo.Text = "Proj. Lead"
        Me.lblAssignedTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtProjectNumber
        '
        Me.txtProjectNumber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectNumber.Location = New System.Drawing.Point(273, 42)
        Me.txtProjectNumber.Name = "txtProjectNumber"
        Me.txtProjectNumber.ReadOnly = True
        Me.txtProjectNumber.Size = New System.Drawing.Size(109, 22)
        Me.txtProjectNumber.TabIndex = 5
        Me.txtProjectNumber.Tag = "Project_Number"
        '
        'lblProjectNumber
        '
        Me.lblProjectNumber.AutoSize = True
        Me.lblProjectNumber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectNumber.Location = New System.Drawing.Point(203, 44)
        Me.lblProjectNumber.Name = "lblProjectNumber"
        Me.lblProjectNumber.Size = New System.Drawing.Size(64, 16)
        Me.lblProjectNumber.TabIndex = 205
        Me.lblProjectNumber.Text = "Project #"
        Me.lblProjectNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbEUTType
        '
        Me.cbEUTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEUTType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEUTType.Items.AddRange(New Object() {"", "Meter Only", "AMI", "AMR Only", "OTHER EUT"})
        Me.cbEUTType.Location = New System.Drawing.Point(273, 140)
        Me.cbEUTType.Name = "cbEUTType"
        Me.cbEUTType.ReadOnly = True
        Me.cbEUTType.Size = New System.Drawing.Size(109, 24)
        Me.cbEUTType.TabIndex = 210
        Me.cbEUTType.TabStop = False
        Me.cbEUTType.Tag = "EUT_Type"
        '
        'lblEUTType
        '
        Me.lblEUTType.AutoSize = True
        Me.lblEUTType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEUTType.Location = New System.Drawing.Point(204, 147)
        Me.lblEUTType.Name = "lblEUTType"
        Me.lblEUTType.Size = New System.Drawing.Size(67, 16)
        Me.lblEUTType.TabIndex = 211
        Me.lblEUTType.Text = "EUT Type"
        Me.lblEUTType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTestLevel
        '
        Me.cbTestLevel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTestLevel.Location = New System.Drawing.Point(92, 140)
        Me.cbTestLevel.Name = "cbTestLevel"
        Me.cbTestLevel.ReadOnly = True
        Me.cbTestLevel.Size = New System.Drawing.Size(108, 24)
        Me.cbTestLevel.TabIndex = 3
        Me.cbTestLevel.TabStop = False
        Me.cbTestLevel.Tag = "Level"
        '
        'lblTestLevel
        '
        Me.lblTestLevel.AutoSize = True
        Me.lblTestLevel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestLevel.Location = New System.Drawing.Point(21, 143)
        Me.lblTestLevel.Name = "lblTestLevel"
        Me.lblTestLevel.Size = New System.Drawing.Size(72, 16)
        Me.lblTestLevel.TabIndex = 122
        Me.lblTestLevel.Text = "Test Level"
        Me.lblTestLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestedby
        '
        Me.lblTestedby.AutoSize = True
        Me.lblTestedby.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestedby.Location = New System.Drawing.Point(19, 265)
        Me.lblTestedby.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTestedby.Name = "lblTestedby"
        Me.lblTestedby.Size = New System.Drawing.Size(73, 16)
        Me.lblTestedby.TabIndex = 121
        Me.lblTestedby.Text = "Tested By:"
        Me.lblTestedby.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTestedBy
        '
        Me.cbTestedBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTestedBy.Location = New System.Drawing.Point(92, 262)
        Me.cbTestedBy.Margin = New System.Windows.Forms.Padding(1)
        Me.cbTestedBy.Name = "cbTestedBy"
        Me.cbTestedBy.ReadOnly = True
        Me.cbTestedBy.Size = New System.Drawing.Size(292, 24)
        Me.cbTestedBy.TabIndex = 35
        Me.cbTestedBy.TabStop = False
        Me.cbTestedBy.Tag = "Tested By"
        '
        'lblDateTested
        '
        Me.lblDateTested.AutoSize = True
        Me.lblDateTested.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateTested.Location = New System.Drawing.Point(3, 44)
        Me.lblDateTested.Name = "lblDateTested"
        Me.lblDateTested.Size = New System.Drawing.Size(86, 16)
        Me.lblDateTested.TabIndex = 120
        Me.lblDateTested.Text = "Date Tested:"
        Me.lblDateTested.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mtxtDateFailed
        '
        Me.mtxtDateFailed.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxtDateFailed.Location = New System.Drawing.Point(91, 40)
        Me.mtxtDateFailed.Name = "mtxtDateFailed"
        Me.mtxtDateFailed.ReadOnly = True
        Me.mtxtDateFailed.Size = New System.Drawing.Size(104, 22)
        Me.mtxtDateFailed.TabIndex = 219
        Me.mtxtDateFailed.Tag = "Date Failed"
        Me.mtxtDateFailed.ValidatingType = GetType(Date)
        '
        'chkTCC_ApprovalRequired
        '
        Me.chkTCC_ApprovalRequired.AutoSize = True
        Me.chkTCC_ApprovalRequired.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCC_ApprovalRequired.Location = New System.Drawing.Point(201, 175)
        Me.chkTCC_ApprovalRequired.Name = "chkTCC_ApprovalRequired"
        Me.chkTCC_ApprovalRequired.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTCC_ApprovalRequired.Size = New System.Drawing.Size(156, 19)
        Me.chkTCC_ApprovalRequired.TabIndex = 238
        Me.chkTCC_ApprovalRequired.Tag = "TCC_Review_Required"
        Me.chkTCC_ApprovalRequired.Text = "TCC Approval Required"
        Me.chkTCC_ApprovalRequired.UseVisualStyleBackColor = True
        '
        'txtCorrectedBy
        '
        Me.txtCorrectedBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCorrectedBy.Location = New System.Drawing.Point(482, 44)
        Me.txtCorrectedBy.Name = "txtCorrectedBy"
        Me.txtCorrectedBy.ReadOnly = True
        Me.txtCorrectedBy.Size = New System.Drawing.Size(296, 22)
        Me.txtCorrectedBy.TabIndex = 239
        Me.txtCorrectedBy.Tag = "Corrected By"
        '
        'lblCorrectedBy
        '
        Me.lblCorrectedBy.AutoSize = True
        Me.lblCorrectedBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCorrectedBy.Location = New System.Drawing.Point(388, 48)
        Me.lblCorrectedBy.Name = "lblCorrectedBy"
        Me.lblCorrectedBy.Size = New System.Drawing.Size(94, 16)
        Me.lblCorrectedBy.TabIndex = 240
        Me.lblCorrectedBy.Text = "Corrected By:"
        Me.lblCorrectedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mtxtDateCorrected
        '
        Me.mtxtDateCorrected.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxtDateCorrected.Location = New System.Drawing.Point(666, 16)
        Me.mtxtDateCorrected.Name = "mtxtDateCorrected"
        Me.mtxtDateCorrected.ReadOnly = True
        Me.mtxtDateCorrected.Size = New System.Drawing.Size(112, 22)
        Me.mtxtDateCorrected.TabIndex = 243
        Me.mtxtDateCorrected.Tag = "Date Corrected"
        '
        'lblDateCorrected
        '
        Me.lblDateCorrected.AutoSize = True
        Me.lblDateCorrected.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateCorrected.Location = New System.Drawing.Point(556, 17)
        Me.lblDateCorrected.Name = "lblDateCorrected"
        Me.lblDateCorrected.Size = New System.Drawing.Size(107, 16)
        Me.lblDateCorrected.TabIndex = 242
        Me.lblDateCorrected.Text = "Date Corrected:"
        Me.lblDateCorrected.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFR_Approvals
        '
        Me.lblFR_Approvals.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFR_Approvals.AutoSize = True
        Me.lblFR_Approvals.Font = New System.Drawing.Font("Arial", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFR_Approvals.ForeColor = System.Drawing.Color.Red
        Me.lblFR_Approvals.Location = New System.Drawing.Point(600, 68)
        Me.lblFR_Approvals.Name = "lblFR_Approvals"
        Me.lblFR_Approvals.Size = New System.Drawing.Size(80, 17)
        Me.lblFR_Approvals.TabIndex = 253
        Me.lblFR_Approvals.Tag = "lblFR_Approvals"
        Me.lblFR_Approvals.Text = "Approvals"
        Me.lblFR_Approvals.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.AutoSize = True
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedBy.Location = New System.Drawing.Point(386, 92)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(93, 16)
        Me.lblApprovedBy.TabIndex = 254
        Me.lblApprovedBy.Text = "Approved By:"
        Me.lblApprovedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTCC_Approvals
        '
        Me.lblTCC_Approvals.AutoSize = True
        Me.lblTCC_Approvals.Font = New System.Drawing.Font("Arial", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_Approvals.ForeColor = System.Drawing.Color.Red
        Me.lblTCC_Approvals.Location = New System.Drawing.Point(478, 119)
        Me.lblTCC_Approvals.Name = "lblTCC_Approvals"
        Me.lblTCC_Approvals.Size = New System.Drawing.Size(50, 23)
        Me.lblTCC_Approvals.TabIndex = 252
        Me.lblTCC_Approvals.Tag = "lblTCC_Approvals"
        Me.lblTCC_Approvals.Text = "TCC"
        Me.lblTCC_Approvals.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedBy.Location = New System.Drawing.Point(482, 88)
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.ReadOnly = True
        Me.txtApprovedBy.Size = New System.Drawing.Size(296, 22)
        Me.txtApprovedBy.TabIndex = 244
        Me.txtApprovedBy.Tag = "Approved By"
        '
        'mtxtDateApproved
        '
        Me.mtxtDateApproved.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxtDateApproved.Location = New System.Drawing.Point(668, 117)
        Me.mtxtDateApproved.Name = "mtxtDateApproved"
        Me.mtxtDateApproved.ReadOnly = True
        Me.mtxtDateApproved.Size = New System.Drawing.Size(110, 22)
        Me.mtxtDateApproved.TabIndex = 255
        Me.mtxtDateApproved.ValidatingType = GetType(Date)
        '
        'lblTCC_Compliance
        '
        Me.lblTCC_Compliance.AutoSize = True
        Me.lblTCC_Compliance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_Compliance.Location = New System.Drawing.Point(395, 148)
        Me.lblTCC_Compliance.Name = "lblTCC_Compliance"
        Me.lblTCC_Compliance.Size = New System.Drawing.Size(84, 16)
        Me.lblTCC_Compliance.TabIndex = 256
        Me.lblTCC_Compliance.Text = "Compliance"
        Me.lblTCC_Compliance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDateApproved
        '
        Me.lblDateApproved.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateApproved.Location = New System.Drawing.Point(560, 120)
        Me.lblDateApproved.Name = "lblDateApproved"
        Me.lblDateApproved.Size = New System.Drawing.Size(106, 16)
        Me.lblDateApproved.TabIndex = 251
        Me.lblDateApproved.Text = "Date Approved:"
        Me.lblDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTCC_1_Compliance
        '
        Me.cbTCC_1_Compliance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTCC_1_Compliance.Location = New System.Drawing.Point(482, 145)
        Me.cbTCC_1_Compliance.Name = "cbTCC_1_Compliance"
        Me.cbTCC_1_Compliance.ReadOnly = True
        Me.cbTCC_1_Compliance.Size = New System.Drawing.Size(296, 24)
        Me.cbTCC_1_Compliance.TabIndex = 246
        Me.cbTCC_1_Compliance.TabStop = False
        Me.cbTCC_1_Compliance.Tag = "TCC 1"
        '
        'lblTCC_Engineering
        '
        Me.lblTCC_Engineering.AutoSize = True
        Me.lblTCC_Engineering.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_Engineering.Location = New System.Drawing.Point(397, 178)
        Me.lblTCC_Engineering.Name = "lblTCC_Engineering"
        Me.lblTCC_Engineering.Size = New System.Drawing.Size(85, 16)
        Me.lblTCC_Engineering.TabIndex = 257
        Me.lblTCC_Engineering.Text = "Engineering"
        Me.lblTCC_Engineering.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTCC_Manufacturing
        '
        Me.lblTCC_Manufacturing.AutoSize = True
        Me.lblTCC_Manufacturing.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_Manufacturing.Location = New System.Drawing.Point(384, 207)
        Me.lblTCC_Manufacturing.Name = "lblTCC_Manufacturing"
        Me.lblTCC_Manufacturing.Size = New System.Drawing.Size(99, 16)
        Me.lblTCC_Manufacturing.TabIndex = 258
        Me.lblTCC_Manufacturing.Text = "Manufacturing"
        Me.lblTCC_Manufacturing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTCC_2_Engineering
        '
        Me.cbTCC_2_Engineering.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTCC_2_Engineering.Location = New System.Drawing.Point(483, 174)
        Me.cbTCC_2_Engineering.Name = "cbTCC_2_Engineering"
        Me.cbTCC_2_Engineering.ReadOnly = True
        Me.cbTCC_2_Engineering.Size = New System.Drawing.Size(295, 24)
        Me.cbTCC_2_Engineering.TabIndex = 247
        Me.cbTCC_2_Engineering.TabStop = False
        Me.cbTCC_2_Engineering.Tag = "TCC 2"
        '
        'cbTCC_5_Quality
        '
        Me.cbTCC_5_Quality.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTCC_5_Quality.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTCC_5_Quality.Location = New System.Drawing.Point(483, 262)
        Me.cbTCC_5_Quality.Name = "cbTCC_5_Quality"
        Me.cbTCC_5_Quality.ReadOnly = True
        Me.cbTCC_5_Quality.Size = New System.Drawing.Size(294, 24)
        Me.cbTCC_5_Quality.TabIndex = 250
        Me.cbTCC_5_Quality.TabStop = False
        Me.cbTCC_5_Quality.Tag = "TCC 5"
        '
        'lblTCC_Quality
        '
        Me.lblTCC_Quality.AutoSize = True
        Me.lblTCC_Quality.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_Quality.Location = New System.Drawing.Point(425, 265)
        Me.lblTCC_Quality.Name = "lblTCC_Quality"
        Me.lblTCC_Quality.Size = New System.Drawing.Size(57, 16)
        Me.lblTCC_Quality.TabIndex = 260
        Me.lblTCC_Quality.Text = "Quaility"
        Me.lblTCC_Quality.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbTCC_4_Product_Management
        '
        Me.cbTCC_4_Product_Management.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTCC_4_Product_Management.Location = New System.Drawing.Point(483, 233)
        Me.cbTCC_4_Product_Management.Name = "cbTCC_4_Product_Management"
        Me.cbTCC_4_Product_Management.ReadOnly = True
        Me.cbTCC_4_Product_Management.Size = New System.Drawing.Size(295, 24)
        Me.cbTCC_4_Product_Management.TabIndex = 249
        Me.cbTCC_4_Product_Management.TabStop = False
        Me.cbTCC_4_Product_Management.Tag = "TCC 4"
        '
        'cbTCC_3_Manufacturing
        '
        Me.cbTCC_3_Manufacturing.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTCC_3_Manufacturing.Location = New System.Drawing.Point(483, 204)
        Me.cbTCC_3_Manufacturing.Name = "cbTCC_3_Manufacturing"
        Me.cbTCC_3_Manufacturing.ReadOnly = True
        Me.cbTCC_3_Manufacturing.Size = New System.Drawing.Size(295, 24)
        Me.cbTCC_3_Manufacturing.TabIndex = 248
        Me.cbTCC_3_Manufacturing.TabStop = False
        Me.cbTCC_3_Manufacturing.Tag = "TCC 3"
        '
        'lblTCC_ProductMangement
        '
        Me.lblTCC_ProductMangement.AutoSize = True
        Me.lblTCC_ProductMangement.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCC_ProductMangement.Location = New System.Drawing.Point(402, 236)
        Me.lblTCC_ProductMangement.Name = "lblTCC_ProductMangement"
        Me.lblTCC_ProductMangement.Size = New System.Drawing.Size(81, 16)
        Me.lblTCC_ProductMangement.TabIndex = 259
        Me.lblTCC_ProductMangement.Text = "Prod. Mgmt"
        Me.lblTCC_ProductMangement.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkAnomely
        '
        Me.chkAnomely.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkAnomely.AutoSize = True
        Me.chkAnomely.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkAnomely.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnomely.Location = New System.Drawing.Point(246, 18)
        Me.chkAnomely.Name = "chkAnomely"
        Me.chkAnomely.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkAnomely.Size = New System.Drawing.Size(83, 20)
        Me.chkAnomely.TabIndex = 261
        Me.chkAnomely.Tag = "Anomaly"
        Me.chkAnomely.Text = "Anomaly"
        Me.chkAnomely.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAnomely.UseVisualStyleBackColor = True
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 767)
        Me.Controls.Add(Me.chkAnomely)
        Me.Controls.Add(Me.lblFR_Approvals)
        Me.Controls.Add(Me.lblApprovedBy)
        Me.Controls.Add(Me.lblTCC_Approvals)
        Me.Controls.Add(Me.txtApprovedBy)
        Me.Controls.Add(Me.mtxtDateApproved)
        Me.Controls.Add(Me.lblTCC_Compliance)
        Me.Controls.Add(Me.lblDateApproved)
        Me.Controls.Add(Me.cbTCC_1_Compliance)
        Me.Controls.Add(Me.lblTCC_Engineering)
        Me.Controls.Add(Me.lblTCC_Manufacturing)
        Me.Controls.Add(Me.cbTCC_2_Engineering)
        Me.Controls.Add(Me.cbTCC_5_Quality)
        Me.Controls.Add(Me.lblTCC_Quality)
        Me.Controls.Add(Me.cbTCC_4_Product_Management)
        Me.Controls.Add(Me.cbTCC_3_Manufacturing)
        Me.Controls.Add(Me.lblTCC_ProductMangement)
        Me.Controls.Add(Me.mtxtDateCorrected)
        Me.Controls.Add(Me.lblDateCorrected)
        Me.Controls.Add(Me.txtCorrectedBy)
        Me.Controls.Add(Me.lblCorrectedBy)
        Me.Controls.Add(Me.chkTCC_ApprovalRequired)
        Me.Controls.Add(Me.lblDateTested)
        Me.Controls.Add(Me.mtxtDateFailed)
        Me.Controls.Add(Me.lblTestedby)
        Me.Controls.Add(Me.cbTestedBy)
        Me.Controls.Add(Me.lblTestName)
        Me.Controls.Add(Me.txtTest)
        Me.Controls.Add(Me.lblTestType)
        Me.Controls.Add(Me.cbTestType)
        Me.Controls.Add(Me.cbProjectLead)
        Me.Controls.Add(Me.txtProject)
        Me.Controls.Add(Me.lblAssignedTo)
        Me.Controls.Add(Me.txtProjectNumber)
        Me.Controls.Add(Me.lblProjectName)
        Me.Controls.Add(Me.lblProjectNumber)
        Me.Controls.Add(Me.cbEUTType)
        Me.Controls.Add(Me.txtReportNumber)
        Me.Controls.Add(Me.lblEUTType)
        Me.Controls.Add(Me.lblReportNumber)
        Me.Controls.Add(Me.cbTestLevel)
        Me.Controls.Add(Me.lblAMR_Notes)
        Me.Controls.Add(Me.lblTestLevel)
        Me.Controls.Add(Me.rtxtAMR_Notes)
        Me.Controls.Add(Me.cbAMR_Voltage)
        Me.Controls.Add(Me.cbAMR_Software_Rev)
        Me.Controls.Add(Me.lblAMR)
        Me.Controls.Add(Me.cbAMR_Software)
        Me.Controls.Add(Me.rtxtMeterNotes)
        Me.Controls.Add(Me.cbAMR_PCBA_Rev)
        Me.Controls.Add(Me.lblBase)
        Me.Controls.Add(Me.cbAMR_FW_Rev)
        Me.Controls.Add(Me.lblMeterNotes)
        Me.Controls.Add(Me.lblAMR_Manufacturer)
        Me.Controls.Add(Me.cbAMR_IP_LAN_ID)
        Me.Controls.Add(Me.cbMeterSoftwareRev)
        Me.Controls.Add(Me.lblAMR_IP_LAN_ID)
        Me.Controls.Add(Me.lblMeter)
        Me.Controls.Add(Me.cbAMR_PCBA)
        Me.Controls.Add(Me.cbMeterSoftware)
        Me.Controls.Add(Me.lblAMR_PCBA_Rev)
        Me.Controls.Add(Me.lblMeterManufacturer)
        Me.Controls.Add(Me.lblAMR_Software)
        Me.Controls.Add(Me.cbMeterVoltage)
        Me.Controls.Add(Me.lblAMR_Software_Rev)
        Me.Controls.Add(Me.cbMeterSubTypeII)
        Me.Controls.Add(Me.lblAMR_FW_REV)
        Me.Controls.Add(Me.cbMeterPCBA_Rev)
        Me.Controls.Add(Me.cbAMR_TYPE)
        Me.Controls.Add(Me.lblMeterSerialNumber)
        Me.Controls.Add(Me.lblAMRVoltage)
        Me.Controls.Add(Me.cbMeterFW_Ver)
        Me.Controls.Add(Me.lblAMR_MODEL)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbAMR_Manufacturer)
        Me.Controls.Add(Me.cbMeterManufacturer)
        Me.Controls.Add(Me.lblAMR_Type)
        Me.Controls.Add(Me.lblMeterSubType)
        Me.Controls.Add(Me.lblAMR_SubType)
        Me.Controls.Add(Me.lblMeterPCBA_Rev)
        Me.Controls.Add(Me.lblAMR_PCBA)
        Me.Controls.Add(Me.cbMeterBase)
        Me.Controls.Add(Me.lblAMR_SubTypeII)
        Me.Controls.Add(Me.lblMeterSoftware)
        Me.Controls.Add(Me.lblAMR_SUBtypeIII)
        Me.Controls.Add(Me.cbMeterSerialNumber)
        Me.Controls.Add(Me.lblAMR_SerialNumber)
        Me.Controls.Add(Me.lblMeterSoftwareRev)
        Me.Controls.Add(Me.cbAMR_SubType)
        Me.Controls.Add(Me.lblMeterType)
        Me.Controls.Add(Me.cbAMR_SerialNumber)
        Me.Controls.Add(Me.cbMeterPCBA)
        Me.Controls.Add(Me.cbAMR_Model)
        Me.Controls.Add(Me.lblMeterModel)
        Me.Controls.Add(Me.cbAMR_SUBtypeIII)
        Me.Controls.Add(Me.lblMeterFW_Ver)
        Me.Controls.Add(Me.cbAMR_SUBtypeII)
        Me.Controls.Add(Me.lblForm)
        Me.Controls.Add(Me.cbMeterForm)
        Me.Controls.Add(Me.lblMeterPCBA_PN)
        Me.Controls.Add(Me.cbMeterDSP_Rev)
        Me.Controls.Add(Me.cbMeterModel)
        Me.Controls.Add(Me.lblMeter_DSP_Rev)
        Me.Controls.Add(Me.lblMeterVoltage)
        Me.Controls.Add(Me.cbMeterType)
        Me.Controls.Add(Me.cbMeterSubType)
        Me.Name = "frmReport"
        Me.Text = "Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblBase As System.Windows.Forms.Label
    Friend WithEvents cbMeterSoftwareRev As xboXComboBox
    Friend WithEvents cbMeterSoftware As xboXComboBox
    Friend WithEvents cbMeterVoltage As xboXComboBox
    Friend WithEvents cbMeterPCBA_Rev As xboXComboBox
    Friend WithEvents cbMeterFW_Ver As xboXComboBox
    Friend WithEvents cbMeterManufacturer As xboXComboBox
    Friend WithEvents lblMeterPCBA_Rev As System.Windows.Forms.Label
    Friend WithEvents lblMeterSoftware As System.Windows.Forms.Label
    Friend WithEvents lblMeterSoftwareRev As System.Windows.Forms.Label
    Friend WithEvents cbMeterPCBA As xboXComboBox
    Friend WithEvents lblMeterFW_Ver As System.Windows.Forms.Label
    Friend WithEvents cbMeterForm As xboXComboBox
    Friend WithEvents lblMeterManufacturer As System.Windows.Forms.Label
    Friend WithEvents cbMeterModel As xboXComboBox
    Friend WithEvents lblMeterVoltage As System.Windows.Forms.Label
    Friend WithEvents cbMeterSubType As xboXComboBox
    Friend WithEvents cbMeterType As xboXComboBox
    Friend WithEvents lblMeter_DSP_Rev As System.Windows.Forms.Label
    Friend WithEvents cbMeterDSP_Rev As xboXComboBox
    Friend WithEvents lblMeterPCBA_PN As System.Windows.Forms.Label
    Friend WithEvents lblForm As System.Windows.Forms.Label
    Friend WithEvents lblMeterModel As System.Windows.Forms.Label
    Friend WithEvents lblMeterType As System.Windows.Forms.Label
    Friend WithEvents cbMeterSerialNumber As xboXComboBox
    Friend WithEvents cbMeterBase As xboXComboBox
    Friend WithEvents lblMeterSubType As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMeterSerialNumber As System.Windows.Forms.Label
    Friend WithEvents cbMeterSubTypeII As xboXComboBox
    Friend WithEvents lblMeter As System.Windows.Forms.Label
    Friend WithEvents rtxtMeterNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents lblMeterNotes As System.Windows.Forms.Label
    Friend WithEvents cbAMR_Voltage As xboXComboBox
    Friend WithEvents cbAMR_Software_Rev As xboXComboBox
    Friend WithEvents cbAMR_Software As xboXComboBox
    Friend WithEvents cbAMR_PCBA_Rev As xboXComboBox
    Friend WithEvents cbAMR_FW_Rev As xboXComboBox
    Friend WithEvents lblAMR_Manufacturer As System.Windows.Forms.Label
    Friend WithEvents cbAMR_IP_LAN_ID As xboXComboBox
    Friend WithEvents lblAMR_IP_LAN_ID As System.Windows.Forms.Label
    Friend WithEvents cbAMR_PCBA As xboXComboBox
    Friend WithEvents lblAMR_PCBA_Rev As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Software As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Software_Rev As System.Windows.Forms.Label
    Friend WithEvents lblAMR_FW_REV As System.Windows.Forms.Label
    Friend WithEvents cbAMR_TYPE As xboXComboBox
    Friend WithEvents lblAMRVoltage As System.Windows.Forms.Label
    Friend WithEvents lblAMR_MODEL As System.Windows.Forms.Label
    Friend WithEvents cbAMR_Manufacturer As xboXComboBox
    Friend WithEvents lblAMR_Type As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SubType As System.Windows.Forms.Label
    Friend WithEvents lblAMR_PCBA As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SubTypeII As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SUBtypeIII As System.Windows.Forms.Label
    Friend WithEvents lblAMR_SerialNumber As System.Windows.Forms.Label
    Friend WithEvents cbAMR_SubType As xboXComboBox
    Friend WithEvents cbAMR_SerialNumber As xboXComboBox
    Friend WithEvents cbAMR_Model As xboXComboBox
    Friend WithEvents cbAMR_SUBtypeIII As xboXComboBox
    Friend WithEvents cbAMR_SUBtypeII As xboXComboBox
    Friend WithEvents lblAMR As System.Windows.Forms.Label
    Friend WithEvents lblAMR_Notes As System.Windows.Forms.Label
    Friend WithEvents rtxtAMR_Notes As System.Windows.Forms.RichTextBox
    Friend WithEvents txtReportNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblReportNumber As System.Windows.Forms.Label
    Friend WithEvents txtProject As System.Windows.Forms.TextBox
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents lblTestName As System.Windows.Forms.Label
    Friend WithEvents txtTest As System.Windows.Forms.TextBox
    Friend WithEvents lblTestType As System.Windows.Forms.Label
    Friend WithEvents cbTestType As xboXComboBox
    Friend WithEvents cbProjectLead As xboXComboBox
    Friend WithEvents lblAssignedTo As System.Windows.Forms.Label
    Friend WithEvents txtProjectNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblProjectNumber As System.Windows.Forms.Label
    Friend WithEvents cbEUTType As xboXComboBox
    Friend WithEvents lblEUTType As System.Windows.Forms.Label
    Friend WithEvents cbTestLevel As xboXComboBox
    Friend WithEvents lblTestLevel As System.Windows.Forms.Label
    Friend WithEvents lblTestedby As System.Windows.Forms.Label
    Friend WithEvents cbTestedBy As xboXComboBox
    Friend WithEvents lblDateTested As System.Windows.Forms.Label
    Friend WithEvents mtxtDateFailed As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkTCC_ApprovalRequired As System.Windows.Forms.CheckBox
    Friend WithEvents txtCorrectedBy As System.Windows.Forms.TextBox
    Friend WithEvents lblCorrectedBy As System.Windows.Forms.Label
    Friend WithEvents mtxtDateCorrected As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblDateCorrected As System.Windows.Forms.Label
    Friend WithEvents lblFR_Approvals As System.Windows.Forms.Label
    Friend WithEvents lblApprovedBy As System.Windows.Forms.Label
    Friend WithEvents lblTCC_Approvals As System.Windows.Forms.Label
    Friend WithEvents txtApprovedBy As System.Windows.Forms.TextBox
    Friend WithEvents mtxtDateApproved As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTCC_Compliance As System.Windows.Forms.Label
    Friend WithEvents lblDateApproved As System.Windows.Forms.Label
    Friend WithEvents cbTCC_1_Compliance As xboXComboBox
    Friend WithEvents lblTCC_Engineering As System.Windows.Forms.Label
    Friend WithEvents lblTCC_Manufacturing As System.Windows.Forms.Label
    Friend WithEvents cbTCC_2_Engineering As xboXComboBox
    Friend WithEvents cbTCC_5_Quality As xboXComboBox
    Friend WithEvents lblTCC_Quality As System.Windows.Forms.Label
    Friend WithEvents cbTCC_4_Product_Management As xboXComboBox
    Friend WithEvents cbTCC_3_Manufacturing As xboXComboBox
    Friend WithEvents lblTCC_ProductMangement As System.Windows.Forms.Label
    Friend WithEvents chkAnomely As System.Windows.Forms.CheckBox
End Class
