<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataBaseBrowser
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.lbSchema = New System.Windows.Forms.ListBox()
        Me.SchemaTreeView = New System.Windows.Forms.TreeView()
        Me.btnUpdateTreeView = New System.Windows.Forms.Button()
        Me.btnGetSchema = New System.Windows.Forms.Button()
        Me.cbTableType = New System.Windows.Forms.ComboBox()
        Me.lblTableType = New System.Windows.Forms.Label()
        Me.lblTableName = New System.Windows.Forms.Label()
        Me.cbTableName = New System.Windows.Forms.ComboBox()
        Me.btnGetData = New System.Windows.Forms.Button()
        Me.btnInsertData = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btnChangeDatabase = New System.Windows.Forms.Button()
        Me.cbNumberOfRecordsToRetrieve = New System.Windows.Forms.ComboBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.Q = New System.Windows.Forms.Label()
        Me.cbColumnName = New System.Windows.Forms.ComboBox()
        Me.lblColumnName = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.chkExactMatch = New System.Windows.Forms.CheckBox()
        Me.txtManualQuery = New System.Windows.Forms.TextBox()
        Me.btnManualQuery = New System.Windows.Forms.Button()
        Me.txtLowerRecord = New System.Windows.Forms.TextBox()
        Me.txtUpperRecord = New System.Windows.Forms.TextBox()
        Me.lblLowerRecord = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReportNumber = New System.Windows.Forms.TextBox()
        Me.cbTestBinding = New System.Windows.Forms.ComboBox()
        Me.btnGetColumnInfo = New System.Windows.Forms.Button()
        Me.btnUpdateData = New System.Windows.Forms.Button()
        Me.txtUpdateColumn = New System.Windows.Forms.TextBox()
        Me.lblUpdateColumn = New System.Windows.Forms.Label()
        Me.txtUpdateIndex = New System.Windows.Forms.TextBox()
        Me.lblUpdateIndex = New System.Windows.Forms.Label()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnShowFailureBrowser = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(12, 9)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.Size = New System.Drawing.Size(434, 191)
        Me.DataGridView1.TabIndex = 0
        '
        'lbSchema
        '
        Me.lbSchema.FormattingEnabled = True
        Me.lbSchema.Location = New System.Drawing.Point(12, 384)
        Me.lbSchema.Name = "lbSchema"
        Me.lbSchema.Size = New System.Drawing.Size(561, 121)
        Me.lbSchema.TabIndex = 1
        '
        'SchemaTreeView
        '
        Me.SchemaTreeView.Location = New System.Drawing.Point(599, 206)
        Me.SchemaTreeView.Name = "SchemaTreeView"
        Me.SchemaTreeView.Size = New System.Drawing.Size(284, 312)
        Me.SchemaTreeView.TabIndex = 2
        '
        'btnUpdateTreeView
        '
        Me.btnUpdateTreeView.Location = New System.Drawing.Point(694, 521)
        Me.btnUpdateTreeView.Name = "btnUpdateTreeView"
        Me.btnUpdateTreeView.Size = New System.Drawing.Size(100, 36)
        Me.btnUpdateTreeView.TabIndex = 3
        Me.btnUpdateTreeView.Text = "Update Tree View"
        Me.btnUpdateTreeView.UseVisualStyleBackColor = True
        '
        'btnGetSchema
        '
        Me.btnGetSchema.Location = New System.Drawing.Point(12, 238)
        Me.btnGetSchema.Name = "btnGetSchema"
        Me.btnGetSchema.Size = New System.Drawing.Size(77, 27)
        Me.btnGetSchema.TabIndex = 4
        Me.btnGetSchema.Text = "Get Schema"
        Me.btnGetSchema.UseVisualStyleBackColor = True
        '
        'cbTableType
        '
        Me.cbTableType.FormattingEnabled = True
        Me.cbTableType.Items.AddRange(New Object() {"ALL"})
        Me.cbTableType.Location = New System.Drawing.Point(104, 242)
        Me.cbTableType.Name = "cbTableType"
        Me.cbTableType.Size = New System.Drawing.Size(90, 21)
        Me.cbTableType.TabIndex = 5
        Me.cbTableType.Text = "ALL"
        '
        'lblTableType
        '
        Me.lblTableType.AutoSize = True
        Me.lblTableType.Location = New System.Drawing.Point(106, 223)
        Me.lblTableType.Name = "lblTableType"
        Me.lblTableType.Size = New System.Drawing.Size(61, 13)
        Me.lblTableType.TabIndex = 6
        Me.lblTableType.Text = "Table Type"
        '
        'lblTableName
        '
        Me.lblTableName.AutoSize = True
        Me.lblTableName.Location = New System.Drawing.Point(237, 217)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.Size = New System.Drawing.Size(65, 13)
        Me.lblTableName.TabIndex = 8
        Me.lblTableName.Text = "Table Name"
        '
        'cbTableName
        '
        Me.cbTableName.FormattingEnabled = True
        Me.cbTableName.Location = New System.Drawing.Point(240, 233)
        Me.cbTableName.Name = "cbTableName"
        Me.cbTableName.Size = New System.Drawing.Size(90, 21)
        Me.cbTableName.TabIndex = 7
        '
        'btnGetData
        '
        Me.btnGetData.Enabled = False
        Me.btnGetData.Location = New System.Drawing.Point(316, 260)
        Me.btnGetData.Name = "btnGetData"
        Me.btnGetData.Size = New System.Drawing.Size(67, 27)
        Me.btnGetData.TabIndex = 9
        Me.btnGetData.Text = "Get Data"
        Me.btnGetData.UseVisualStyleBackColor = True
        '
        'btnInsertData
        '
        Me.btnInsertData.Location = New System.Drawing.Point(12, 524)
        Me.btnInsertData.Name = "btnInsertData"
        Me.btnInsertData.Size = New System.Drawing.Size(73, 30)
        Me.btnInsertData.TabIndex = 10
        Me.btnInsertData.Text = "Insert Data"
        Me.btnInsertData.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView2.Location = New System.Drawing.Point(-1, 560)
        Me.DataGridView2.Name = "DataGridView2"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView2.Size = New System.Drawing.Size(884, 101)
        Me.DataGridView2.TabIndex = 11
        '
        'btnChangeDatabase
        '
        Me.btnChangeDatabase.Location = New System.Drawing.Point(518, 211)
        Me.btnChangeDatabase.Name = "btnChangeDatabase"
        Me.btnChangeDatabase.Size = New System.Drawing.Size(64, 37)
        Me.btnChangeDatabase.TabIndex = 12
        Me.btnChangeDatabase.Text = "Change Database"
        Me.btnChangeDatabase.UseVisualStyleBackColor = True
        '
        'cbNumberOfRecordsToRetrieve
        '
        Me.cbNumberOfRecordsToRetrieve.FormattingEnabled = True
        Me.cbNumberOfRecordsToRetrieve.Items.AddRange(New Object() {"10", "25", "50", "100", "ALL"})
        Me.cbNumberOfRecordsToRetrieve.Location = New System.Drawing.Point(357, 233)
        Me.cbNumberOfRecordsToRetrieve.Name = "cbNumberOfRecordsToRetrieve"
        Me.cbNumberOfRecordsToRetrieve.Size = New System.Drawing.Size(64, 21)
        Me.cbNumberOfRecordsToRetrieve.TabIndex = 13
        Me.cbNumberOfRecordsToRetrieve.Text = "25"
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(236, 260)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(66, 27)
        Me.btnPrev.TabIndex = 14
        Me.btnPrev.Text = "Previous"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(399, 260)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(62, 27)
        Me.btnNext.TabIndex = 15
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'Q
        '
        Me.Q.AutoSize = True
        Me.Q.Location = New System.Drawing.Point(354, 217)
        Me.Q.Name = "Q"
        Me.Q.Size = New System.Drawing.Size(29, 13)
        Me.Q.TabIndex = 16
        Me.Q.Text = "QTY"
        '
        'cbColumnName
        '
        Me.cbColumnName.FormattingEnabled = True
        Me.cbColumnName.Location = New System.Drawing.Point(104, 321)
        Me.cbColumnName.Name = "cbColumnName"
        Me.cbColumnName.Size = New System.Drawing.Size(90, 21)
        Me.cbColumnName.TabIndex = 17
        '
        'lblColumnName
        '
        Me.lblColumnName.AutoSize = True
        Me.lblColumnName.Location = New System.Drawing.Point(106, 305)
        Me.lblColumnName.Name = "lblColumnName"
        Me.lblColumnName.Size = New System.Drawing.Size(73, 13)
        Me.lblColumnName.TabIndex = 18
        Me.lblColumnName.Text = "Column Name"
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(104, 351)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(198, 20)
        Me.txtFilter.TabIndex = 19
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(41, 354)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(59, 13)
        Me.lblFilter.TabIndex = 20
        Me.lblFilter.Text = "Filter Value"
        '
        'chkExactMatch
        '
        Me.chkExactMatch.AutoSize = True
        Me.chkExactMatch.Location = New System.Drawing.Point(316, 354)
        Me.chkExactMatch.Name = "chkExactMatch"
        Me.chkExactMatch.Size = New System.Drawing.Size(86, 17)
        Me.chkExactMatch.TabIndex = 21
        Me.chkExactMatch.Text = "Exact Match"
        Me.chkExactMatch.UseVisualStyleBackColor = True
        '
        'txtManualQuery
        '
        Me.txtManualQuery.Location = New System.Drawing.Point(43, 722)
        Me.txtManualQuery.Multiline = True
        Me.txtManualQuery.Name = "txtManualQuery"
        Me.txtManualQuery.Size = New System.Drawing.Size(359, 74)
        Me.txtManualQuery.TabIndex = 22
        Me.txtManualQuery.Text = "SELECT * [Failure Report] ORDER BY [New ID] DESC"
        '
        'btnManualQuery
        '
        Me.btnManualQuery.Location = New System.Drawing.Point(44, 677)
        Me.btnManualQuery.Name = "btnManualQuery"
        Me.btnManualQuery.Size = New System.Drawing.Size(73, 30)
        Me.btnManualQuery.TabIndex = 23
        Me.btnManualQuery.Text = "Manual Query"
        Me.btnManualQuery.UseVisualStyleBackColor = True
        '
        'txtLowerRecord
        '
        Me.txtLowerRecord.Location = New System.Drawing.Point(423, 722)
        Me.txtLowerRecord.Multiline = True
        Me.txtLowerRecord.Name = "txtLowerRecord"
        Me.txtLowerRecord.Size = New System.Drawing.Size(71, 21)
        Me.txtLowerRecord.TabIndex = 24
        Me.txtLowerRecord.Text = "1"
        '
        'txtUpperRecord
        '
        Me.txtUpperRecord.Location = New System.Drawing.Point(511, 722)
        Me.txtUpperRecord.Multiline = True
        Me.txtUpperRecord.Name = "txtUpperRecord"
        Me.txtUpperRecord.Size = New System.Drawing.Size(71, 21)
        Me.txtUpperRecord.TabIndex = 25
        Me.txtUpperRecord.Text = "10"
        '
        'lblLowerRecord
        '
        Me.lblLowerRecord.AutoSize = True
        Me.lblLowerRecord.Location = New System.Drawing.Point(420, 706)
        Me.lblLowerRecord.Name = "lblLowerRecord"
        Me.lblLowerRecord.Size = New System.Drawing.Size(74, 13)
        Me.lblLowerRecord.TabIndex = 26
        Me.lblLowerRecord.Text = "Lower Record"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(508, 706)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Upper Record"
        '
        'txtReportNumber
        '
        Me.txtReportNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtReportNumber.BackColor = System.Drawing.Color.White
        Me.txtReportNumber.ForeColor = System.Drawing.Color.Black
        Me.txtReportNumber.Location = New System.Drawing.Point(12, 282)
        Me.txtReportNumber.Name = "txtReportNumber"
        Me.txtReportNumber.ReadOnly = True
        Me.txtReportNumber.Size = New System.Drawing.Size(44, 20)
        Me.txtReportNumber.TabIndex = 156
        '
        'cbTestBinding
        '
        Me.cbTestBinding.FormattingEnabled = True
        Me.cbTestBinding.Items.AddRange(New Object() {"ALL"})
        Me.cbTestBinding.Location = New System.Drawing.Point(472, 324)
        Me.cbTestBinding.Name = "cbTestBinding"
        Me.cbTestBinding.Size = New System.Drawing.Size(90, 21)
        Me.cbTestBinding.TabIndex = 157
        '
        'btnGetColumnInfo
        '
        Me.btnGetColumnInfo.Location = New System.Drawing.Point(292, 298)
        Me.btnGetColumnInfo.Name = "btnGetColumnInfo"
        Me.btnGetColumnInfo.Size = New System.Drawing.Size(91, 30)
        Me.btnGetColumnInfo.TabIndex = 158
        Me.btnGetColumnInfo.Text = "Get Column Info"
        Me.btnGetColumnInfo.UseVisualStyleBackColor = True
        '
        'btnUpdateData
        '
        Me.btnUpdateData.Location = New System.Drawing.Point(250, 524)
        Me.btnUpdateData.Name = "btnUpdateData"
        Me.btnUpdateData.Size = New System.Drawing.Size(80, 30)
        Me.btnUpdateData.TabIndex = 159
        Me.btnUpdateData.Text = "Update Data"
        Me.btnUpdateData.UseVisualStyleBackColor = True
        '
        'txtUpdateColumn
        '
        Me.txtUpdateColumn.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtUpdateColumn.BackColor = System.Drawing.Color.White
        Me.txtUpdateColumn.ForeColor = System.Drawing.Color.Black
        Me.txtUpdateColumn.Location = New System.Drawing.Point(357, 530)
        Me.txtUpdateColumn.Name = "txtUpdateColumn"
        Me.txtUpdateColumn.Size = New System.Drawing.Size(64, 20)
        Me.txtUpdateColumn.TabIndex = 160
        Me.txtUpdateColumn.Text = "New Index"
        '
        'lblUpdateColumn
        '
        Me.lblUpdateColumn.AutoSize = True
        Me.lblUpdateColumn.Location = New System.Drawing.Point(356, 514)
        Me.lblUpdateColumn.Name = "lblUpdateColumn"
        Me.lblUpdateColumn.Size = New System.Drawing.Size(42, 13)
        Me.lblUpdateColumn.TabIndex = 161
        Me.lblUpdateColumn.Text = "Column"
        '
        'txtUpdateIndex
        '
        Me.txtUpdateIndex.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtUpdateIndex.BackColor = System.Drawing.Color.White
        Me.txtUpdateIndex.ForeColor = System.Drawing.Color.Black
        Me.txtUpdateIndex.Location = New System.Drawing.Point(430, 530)
        Me.txtUpdateIndex.Name = "txtUpdateIndex"
        Me.txtUpdateIndex.Size = New System.Drawing.Size(64, 20)
        Me.txtUpdateIndex.TabIndex = 162
        Me.txtUpdateIndex.Text = "Index"
        '
        'lblUpdateIndex
        '
        Me.lblUpdateIndex.AutoSize = True
        Me.lblUpdateIndex.Location = New System.Drawing.Point(427, 514)
        Me.lblUpdateIndex.Name = "lblUpdateIndex"
        Me.lblUpdateIndex.Size = New System.Drawing.Size(33, 13)
        Me.lblUpdateIndex.TabIndex = 163
        Me.lblUpdateIndex.Text = "Index"
        '
        'DataGridView3
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView3.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridView3.Location = New System.Drawing.Point(452, 9)
        Me.DataGridView3.Name = "DataGridView3"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridView3.Size = New System.Drawing.Size(431, 191)
        Me.DataGridView3.TabIndex = 164
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.Location = New System.Drawing.Point(518, 298)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(44, 20)
        Me.TextBox1.TabIndex = 165
        '
        'btnShowFailureBrowser
        '
        Me.btnShowFailureBrowser.Location = New System.Drawing.Point(518, 254)
        Me.btnShowFailureBrowser.Name = "btnShowFailureBrowser"
        Me.btnShowFailureBrowser.Size = New System.Drawing.Size(64, 37)
        Me.btnShowFailureBrowser.TabIndex = 166
        Me.btnShowFailureBrowser.Text = "Open FR Browser"
        Me.btnShowFailureBrowser.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(397, 308)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(69, 37)
        Me.Button1.TabIndex = 167
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmDataBaseBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 808)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnShowFailureBrowser)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.lblUpdateIndex)
        Me.Controls.Add(Me.txtUpdateIndex)
        Me.Controls.Add(Me.lblUpdateColumn)
        Me.Controls.Add(Me.txtUpdateColumn)
        Me.Controls.Add(Me.btnUpdateData)
        Me.Controls.Add(Me.btnGetColumnInfo)
        Me.Controls.Add(Me.cbTestBinding)
        Me.Controls.Add(Me.txtReportNumber)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblLowerRecord)
        Me.Controls.Add(Me.txtUpperRecord)
        Me.Controls.Add(Me.txtLowerRecord)
        Me.Controls.Add(Me.btnManualQuery)
        Me.Controls.Add(Me.txtManualQuery)
        Me.Controls.Add(Me.chkExactMatch)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.lblColumnName)
        Me.Controls.Add(Me.cbColumnName)
        Me.Controls.Add(Me.Q)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.cbNumberOfRecordsToRetrieve)
        Me.Controls.Add(Me.btnChangeDatabase)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.btnInsertData)
        Me.Controls.Add(Me.btnGetData)
        Me.Controls.Add(Me.lblTableName)
        Me.Controls.Add(Me.cbTableName)
        Me.Controls.Add(Me.lblTableType)
        Me.Controls.Add(Me.cbTableType)
        Me.Controls.Add(Me.btnGetSchema)
        Me.Controls.Add(Me.btnUpdateTreeView)
        Me.Controls.Add(Me.SchemaTreeView)
        Me.Controls.Add(Me.lbSchema)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmDataBaseBrowser"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents lbSchema As System.Windows.Forms.ListBox
    Friend WithEvents SchemaTreeView As System.Windows.Forms.TreeView
    Friend WithEvents btnUpdateTreeView As System.Windows.Forms.Button
    Friend WithEvents btnGetSchema As System.Windows.Forms.Button
    Friend WithEvents cbTableType As System.Windows.Forms.ComboBox
    Friend WithEvents lblTableType As System.Windows.Forms.Label
    Friend WithEvents lblTableName As System.Windows.Forms.Label
    Friend WithEvents cbTableName As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetData As System.Windows.Forms.Button
    Friend WithEvents btnInsertData As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents btnChangeDatabase As System.Windows.Forms.Button
    Friend WithEvents cbNumberOfRecordsToRetrieve As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents Q As System.Windows.Forms.Label
    Friend WithEvents cbColumnName As System.Windows.Forms.ComboBox
    Friend WithEvents lblColumnName As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents chkExactMatch As System.Windows.Forms.CheckBox
    Friend WithEvents txtManualQuery As System.Windows.Forms.TextBox
    Friend WithEvents btnManualQuery As System.Windows.Forms.Button
    Friend WithEvents txtLowerRecord As System.Windows.Forms.TextBox
    Friend WithEvents txtUpperRecord As System.Windows.Forms.TextBox
    Friend WithEvents lblLowerRecord As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReportNumber As System.Windows.Forms.TextBox
    Friend WithEvents cbTestBinding As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetColumnInfo As System.Windows.Forms.Button
    Friend WithEvents btnUpdateData As System.Windows.Forms.Button
    Friend WithEvents txtUpdateColumn As System.Windows.Forms.TextBox
    Friend WithEvents lblUpdateColumn As System.Windows.Forms.Label
    Friend WithEvents txtUpdateIndex As System.Windows.Forms.TextBox
    Friend WithEvents lblUpdateIndex As System.Windows.Forms.Label
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnShowFailureBrowser As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
