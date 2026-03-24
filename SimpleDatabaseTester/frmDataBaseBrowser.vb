
Imports System
Imports System.Data
Imports System.Data.OleDb







Public Class frmDataBaseBrowser

    Public Class cColumn
        Private m_ColumnName As String

        Public Property ColumnName() As String
            Get
                Return m_ColumnName
            End Get
            Set(value As String)
                m_ColumnName = value
            End Set
        End Property


        Public Sub New(strColumnName As String)
            ColumnName = strColumnName
        End Sub

        Public Overrides Function ToString() As String
            Return m_ColumnName
        End Function
    End Class

    Public Class cIndex
        'Sub New()
        '    ' TODO: Complete member initialization 
        'End Sub
        Public Property IndexName As String
            Get
                Return m_IndexName
            End Get
            Set(value As String)
                m_IndexName = value
            End Set
        End Property
        Private m_IndexName As String

        Public Property PrimaryKey As Boolean
            Get
                Return m_PrimaryKey
            End Get
            Set(value As Boolean)
                m_PrimaryKey = value
            End Set
        End Property
        Private m_PrimaryKey As Boolean

        Public Property Unique As Boolean
            Get
                Return m_Unique
            End Get
            Set(value As Boolean)
                m_Unique = value
            End Set
        End Property
        Private m_Unique As Boolean

        Public Sub New(strIndexName As String, bPrimaryKey As Boolean, bUnique As Boolean)
            IndexName = strIndexName
            PrimaryKey = bPrimaryKey
            Unique = bUnique
        End Sub

        Public Overrides Function ToString() As String
            Return [String].Format("{0} (Primary Key={1}), (Unique={2})", IndexName, PrimaryKey.ToString(), Unique.ToString())
        End Function
    End Class

    Public Class cTable
        'get set properties
        Public Property TableName As String

            Get
                Return m_TableName
            End Get
            Set(value As String)
                m_TableName = value
            End Set
        End Property
        Private m_TableName As String

        Public Property Columns As List(Of cColumn)
            Get
                Return m_columns
            End Get
            Set(value As List(Of cColumn))
                m_columns = value
            End Set
        End Property
        Private m_columns As List(Of cColumn)

        Public Property Indexes As List(Of cIndex)
            Get
                Return m_indexes
            End Get
            Set(value As List(Of cIndex))
                m_indexes = value
            End Set
        End Property
        Private m_indexes As List(Of cIndex)

        Public Sub New(strTableName As String, ColumnList As List(Of cColumn), IndexList As List(Of cIndex))
            TableName = strTableName
            Columns = ColumnList
            Indexes = IndexList
        End Sub
    End Class

    Public Class cView
        Public Property ViewName As String
            Get
                Return m_ViewName
            End Get
            Set(value As String)
                m_ViewName = value
            End Set
        End Property
        Private m_ViewName As String
        Public Property ViewDefination As String
            Get
                Return m_ViewDefination
            End Get
            Set(value As String)
                m_ViewDefination = value
            End Set
        End Property
        Private m_ViewDefination As String

        Public Property Columns As List(Of cColumn)
            Get
                Return m_columns
            End Get
            Set(value As List(Of cColumn))
                m_columns = value
            End Set
        End Property
        Private m_columns As List(Of cColumn)

        Public Sub New(strViewName As String, strViewDefination As String, ColumnList As List(Of cColumn))
            ViewName = strViewName
            ViewDefination = strViewDefination
            Columns = ColumnList
        End Sub

        Public Overrides Function ToString() As String
            Return ViewName
        End Function
    End Class

#Region "Variables Available to All Functions in the Form"
    'globals
    Public gDataBaseConnection As OleDb.OleDbConnection
    Public WithEvents gDataRecordSet As OleDb.OleDbDataAdapter
    Public gMyCustomDataBaseAccess As cCustomDataBaseAccess
    Public WithEvents gMyDataTable As DataTable
    Public WithEvents gFailureReportBindingSource As System.Windows.Forms.BindingSource
    Public WithEvents gFailureReportBinding As Binding

    Private _DataBaseTableNameDataTable As DataTable = Nothing
    Private _DataBaseRestrictions As DataTable = Nothing
    Private _TablesList As List(Of cTable) = Nothing
    Private _Views As List(Of cView) = Nothing
    Private _MyFailureReportDataTable As DataTable
    Private _MyFailureReportDataAdaptor As OleDbDataAdapter

    Private _RecordIndexLower As Integer ' used for navigating the record
    Private _RecordIndexUpper As Integer ' used for navigating the record 
    Private _RecordPointer As Integer ' used for navigating the record Zerobased
    Private _RecordOffset As Integer ' used for navigating the record
    Private _RecordCount As Integer 'used to navigate the record
    Public _BindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker)

    Private WithEvents _MyTestBinding As Binding
#End Region


    Private Sub GetTables(MyConnection As OleDbConnection, Optional MyTableType As String = "Table")
        Dim MySchema As DataTable
        MySchema = MyConnection.GetSchema("Tables", New [String]() {Nothing, Nothing, Nothing, MyTableType})

        If MySchema IsNot Nothing And MySchema.Rows.Count > 0 Then
            _TablesList = New List(Of cTable)()
            For Each SchemaRow As DataRow In MySchema.Rows

                _TablesList.Add(New cTable(SchemaRow("TABLE_NAME").ToString().Trim(), GetColumns(MyConnection, SchemaRow("TABLE_NAME").ToString().Trim()), GetIndexes(MyConnection, SchemaRow("TABLE_NAME").ToString().Trim())))
            Next

        End If
    End Sub

    Public Function GetColumns(MyConnection As OleDbConnection, MyTableName As String) As List(Of cColumn)
        Dim ColumnList As List(Of cColumn) = Nothing

        Dim ColumnTable As DataTable = MyConnection.GetSchema("Columns", New [String]() {Nothing, Nothing, MyTableName, Nothing})
        If ColumnTable IsNot Nothing And ColumnTable.Rows.Count > 0 Then
            ColumnList = New List(Of cColumn)()
        End If

        For Each ColumnRow As DataRow In ColumnTable.Rows
            ColumnList.Add(New cColumn(ColumnRow("COLUMN_NAME").ToString().Trim()))
        Next

        Return ColumnList
    End Function

    Public Function GetIndexes(MyConnection As OleDbConnection, MyTableName As String) As List(Of cIndex)
        Dim IndexList As List(Of cIndex) = Nothing

        Dim IndexTable As DataTable = MyConnection.GetSchema("Indexes", New [String]() {Nothing, Nothing, Nothing, Nothing, MyTableName})
        If IndexTable IsNot Nothing And IndexTable.Rows.Count > 0 Then
            IndexList = New List(Of cIndex)()
        End If

        For Each IndexRow As DataRow In IndexTable.Rows
            IndexList.Add(New cIndex(IndexRow("INDEX_NAME").ToString().Trim(), Convert.ToBoolean(IndexRow("PRIMARY_KEY").ToString()), Convert.ToBoolean(IndexRow("UNIQUE").ToString())))
        Next

        Return IndexList
    End Function

    Private Sub GetViews(MyConnection As OleDbConnection)
        'Get all views associated to this database.
        Dim MyViewsTable As DataTable = MyConnection.GetSchema("Views")

        If MyViewsTable IsNot Nothing AndAlso MyViewsTable.Rows.Count > 0 Then
            _Views = New List(Of cView)()

            For Each MyViewRow As DataRow In MyViewsTable.Rows
                _Views.Add(New cView(MyViewRow("TABLE_NAME").ToString().Trim(), MyViewRow("VIEW_DEFINITION").ToString().Trim(), GetColumns(MyConnection, MyViewRow("TABLE_NAME").ToString().Trim())))
            Next
        End If
    End Sub

    Private Sub InitForm()
        gMyDataTable = New DataTable
        _BindingRecord = New List(Of cCustomDataBaseAccess.cDataBindingTracker)
        gMyCustomDataBaseAccess = New cCustomDataBaseAccess
        Try
            'on form load instantiate the connection object
            'frmDataBaseBrowser.gDataBaseConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\All_Failure_Reports_Database.accdb;Persist Security Info=True;Jet OLEDB:Database Password=baldts")
            'gDataBaseConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Meter_Specs.accdb;Persist Security Info=True;Jet OLEDB:Database Password=baldts")
            Try
                gDataBaseConnection = New OleDb.OleDbConnection(My.Settings.FailureReportDataBaseFullConnectionString)
                'test
                '   gDataBaseConnection.Open()
                '  gDataBaseConnection.Close()
            Catch
                ' frmSelectDatabase.ShowDialog()
            End Try


            _MyFailureReportDataAdaptor = New OleDbDataAdapter("SELECT * FROM [Failure Report] ORDER BY [New ID] Desc", My.Settings.FailureReportDataBaseFullConnectionString)
            _MyFailureReportDataAdaptor.Fill(gMyDataTable)
            DataGridView3.DataSource = gMyDataTable

            'gFailureReportBindingSource = New BindingSource(gMyDataTable, gMyDataTable.TableName)
            gFailureReportBinding = New Binding("Text", gMyDataTable, "New ID", True)


            TextBox1.DataBindings.Add(gFailureReportBinding)


            Try
                'try to open the connection
                Call gDataBaseConnection.Open()
            Catch ex As Exception
                frmSelectDatabase.Show()
                MessageBox.Show("Could not connect for some reason.... is the file on the right location? --> check connectionstring")
                End
            End Try

            If gDataBaseConnection.State = ConnectionState.Open Then
                'you've got an open connection!!! Whohooo
                'Do your magic over here... 

                'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
                'something with it
                '_MyFailureReportDataAdaptor(


                'When "Table" is added a restiction in Table Type, only users tables are reurned
                Dim straRestrictions() As String = New String(3) {}
                straRestrictions(3) = "Table"
                _DataBaseTableNameDataTable = gDataBaseConnection.GetSchema("Tables", straRestrictions)


                Dim i As Integer
                Dim j As Integer = 0
                Dim strDataBaseTableRow As String
                For i = 0 To _DataBaseTableNameDataTable.Rows.Count - 1 Step i + 1
                    strDataBaseTableRow = ""
                    'System.Console.WriteLine(_DataBaseRecordSet.Rows(i)(2).ToString())
                    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(0).ToString())
                    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(1).ToString())
                    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(2).ToString())
                    For j = 0 To _DataBaseTableNameDataTable.Columns.Count - 1
                        Dim strColumnValue As String = _DataBaseTableNameDataTable.Rows(i)(j).ToString() + vbTab
                        If strColumnValue = ("" + vbTab) Then
                            strColumnValue = "NOTHING" + vbTab
                        End If

                        'lbSchema.Items.Add("TABLE " + vbTab + "ROW: " + i.ToString + " COL: " + j.ToString + " - " + strColumnValue)
                        strDataBaseTableRow = strDataBaseTableRow + strColumnValue
                        If (Not cbTableType.Items.Contains(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)) And (j = 3) Then
                            cbTableType.Items.Add(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)
                        End If
                        'Table Names
                        If (Not cbTableName.Items.Contains(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)) And (j = 2) Then
                            cbTableName.Items.Add(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)
                        End If
                    Next
                    lbSchema.Items.Add("TABLE " + vbTab + "ROW: " + i.ToString + strDataBaseTableRow)
                Next


                cbTableName.SelectedIndex = 0

                ''test....
                ''get Tables without restrictions....
                '_DataBaseTableNameDataTable = gDataBaseConnection.GetSchema("Tables")


                'i = 0
                'j = 0
                'For i = 0 To _DataBaseTableNameDataTable.Rows.Count - 1 Step i + 1
                '    'System.Console.WriteLine(_DataBaseRecordSet.Rows(i)(2).ToString())
                '    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(0).ToString())
                '    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(1).ToString())
                '    ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(2).ToString())
                '    For j = 0 To _DataBaseTableNameDataTable.Columns.Count - 1
                '        lbSchema.Items.Add("TABLE No  RESTRIC:" + vbTab + "ROW: " + i.ToString + " COL: " + j.ToString + " - " + _DataBaseTableNameDataTable.Rows(i)(j).ToString())
                '        'Table Type
                '        If (Not cbTableType.Items.Contains(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)) And (j = 3) Then
                '            cbTableType.Items.Add(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)
                '        End If
                '        'Table Names
                '        If (Not cbTableName.Items.Contains(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)) And (j = 2) Then
                '            cbTableName.Items.Add(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)
                '        End If

                '    Next

                'Next


                'get a list of valid restrictions
                _DataBaseRestrictions = gDataBaseConnection.GetSchema("Restrictions")




                i = 0

                For i = 0 To _DataBaseRestrictions.Rows.Count - 1 Step i + 1
                    'System.Console.WriteLine(_DataBaseRecordSet.Rows(i)(2).ToString())
                    'ListBox1.Items.Add("Restriction " + i.ToString + ": " + _DataBaseRestrictions.Rows(i)(2).ToString())

                    For j = 0 To _DataBaseRestrictions.Columns.Count - 1
                        lbSchema.Items.Add("Restriction " + vbTab + "ROW: " + i.ToString + " COL: " + j.ToString + " - " + _DataBaseRestrictions.Rows(i)(j).ToString())
                    Next


                Next
                _RecordIndexUpper = CInt(gMyCustomDataBaseAccess.GetMaxValueFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))
                _RecordIndexLower = CInt(gMyCustomDataBaseAccess.GetMinValueFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))
                _RecordCount = CInt(gMyCustomDataBaseAccess.GetRecordCountFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))

                _RecordPointer = 0
                'Show the data from the first table automatically
                Dim strMyFirstTableName = cbTableName.SelectedItem.ToString.Trim
                btnNext.Enabled = False
                btnPrev.Enabled = False
                Dim strFilter As String = "ORDER BY [New ID] DESC" 'Sort highest to lowest

                _MyFailureReportDataTable = New DataTable()
                If cbNumberOfRecordsToRetrieve.SelectedItem = "ALL" Then
                    'get the datatable
                    _MyFailureReportDataTable = gMyCustomDataBaseAccess.GetDataCustomQuery(gDataBaseConnection, strMyFirstTableName, strFilter)
                    'Assign the Gridview's data source Source
                    DataGridView1.DataSource = _MyFailureReportDataTable
                    _RecordPointer = 0
                    _RecordOffset = _RecordIndexUpper
                Else
                    _RecordOffset = CInt(cbNumberOfRecordsToRetrieve.SelectedItem) + 1
                    _MyFailureReportDataTable = gMyCustomDataBaseAccess.GetData(gDataBaseConnection, _RecordPointer, _RecordOffset, strMyFirstTableName, strFilter)
                    DataGridView1.DataSource = _MyFailureReportDataTable
                End If

                If _RecordIndexUpper > _RecordOffset Then
                    btnPrev.Enabled = True
                End If

                btnGetData.Enabled = True
                'Try

                'Text Property DataBindings

                ' _MyTestBinding = New Binding("T", _MyFailureReportDataAdaptor, "New")

                '  _BindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtReportNumber, "Text", "New ID", _MyFailureReportDataTable, _MyTestBinding))



                '_MyTestBinding = New Binding("Text", _MyFailureReportDataTable, "New ID")
                'txtReportNumber.DataBindings.Add(_MyTestBinding)


                cbTestBinding.DataSource = gMyCustomDataBaseAccess.GetDistinctData(gDataBaseConnection, "AMR", "Failure Report", "ORDER BY [AMR] ASC")
                cbTestBinding.DisplayMember = "AMR"
                cbTestBinding.ValueMember = "AMR"




                'txtReportNumber.DataBindings.Add(MyTestBinding)

                'MyTestBinding = New Binding("Text", DataGridView1.DataSource, "AMR")
                'cbTestBinding.DataBindings.Add(New Binding("Text", DataGridView1.DataSource, "AMR"))

                'Catch
                'End Try
                'DataGridView1.Sort(DataGridView1.Columns("NEW ID"), System.ComponentModel.ListSortDirection.Descending)




                ''Dim SQL As New OleDb.OleDbCommand("SELECT * FROM [Failure Report]", frmDataBaseBrowser.gDataBaseConnection)
                'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM " + strMyFirstTableName, gDataBaseConnection)

                ''Use the dataadapter object to use the results from the query and bash them in a "DataTable"
                ''A datatable is an in memory table that is comparable with the adodb.recordset object you've
                ''previously used in vb6 or vba. 

                'Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

                ''Create a datatable to house the results from the query
                'Dim MyDataTable As New DataTable(cbTableName.SelectedItem.ToString.Trim)

                ''Bash the query results in the datatable
                'MyOleDBDataAdapter.Fill(MyDataTable)

                ''now show your datatable to the world. ^_^ I've placed a datagridview on the form called: DataGridView1
                'DataGridView1.DataSource = MyDataTable
                'DONE!

                'Update / Deletes additions, you name it all use the same technology. 

                'secondary grid
                DataGridView2.ColumnCount = DataGridView1.ColumnCount
                For i = 0 To DataGridView1.ColumnCount - 1
                    DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
                Next

                'Fill Filter Column combo box  names from the first filter
                cbColumnName.Items.Clear()
                cbColumnName.Items.Add("*")
                For i = 0 To DataGridView1.ColumnCount - 1
                    DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
                    cbColumnName.Items.Add(DataGridView1.Columns(i).Name)
                Next
            End If



        Catch ex As Exception
            'when there is "an" error, do something, then continue running the app
            MessageBox.Show(ex.ToString)
        End Try

        Try
            _BindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtReportNumber, "Text", "New ID", _MyFailureReportDataTable, _MyTestBinding))
            gMyCustomDataBaseAccess.BindControls(_BindingRecord, cCustomDataBaseAccess.DataSourceType.DATA_TABLE)
        Catch
        End Try

    End Sub


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            gDataBaseConnection = New OleDb.OleDbConnection(My.Settings.FailureReportDataBaseFullConnectionString)
            'test
            gDataBaseConnection.Open()
            gDataBaseConnection.Close()
            InitForm()
        Catch
            ' frmSelectDatabase.ShowDialog()
        End Try


    End Sub

    Private Sub btnUpdateTreeView_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateTreeView.Click

        Try
            GetTables(gDataBaseConnection)
            GetViews(gDataBaseConnection)

            'Create Tree View of Schema...
            SchemaTreeView.Nodes.Clear()
            Dim RootNode As TreeNode = New TreeNode("Schema")

            'Add Table, Colums, and Indexes...
            Dim TableNode As TreeNode = New TreeNode("Tables")

            For Each MyTable As cTable In _TablesList

                'Table
                Dim CurrentTableNode As TreeNode
                CurrentTableNode = TableNode.Nodes.Add(MyTable.TableName)

                'Current Table Columns
                Dim ColumnsNode As TreeNode
                ColumnsNode = New TreeNode("Columns")
                For Each MyColumn As cColumn In MyTable.Columns
                    ColumnsNode.Nodes.Add(MyColumn.ColumnName)
                Next
                CurrentTableNode.Nodes.Add(ColumnsNode)

                If MyTable.Indexes IsNot Nothing Then
                    Dim MyIndexesNode As New TreeNode("Indexes")

                    For Each MyIndex As cIndex In MyTable.Indexes
                        MyIndexesNode.Nodes.Add(MyIndex.ToString)
                    Next
                    CurrentTableNode.Nodes.Add(MyIndexesNode)
                End If

            Next

            'Add Views
            'NOTE for the Views we are going to add the View object to the tag property of the node so that
            'we can display the view definition when the view node is selected.
            'Views are queries that are stored in the database
            Dim ViewNode As TreeNode = New TreeNode("Views")
            If _Views IsNot Nothing Then
                For Each MyView As cView In _Views
                    Dim MyCurrentViewNode As TreeNode = New TreeNode(MyView.ViewName)
                    MyCurrentViewNode.Tag = MyView
                    ViewNode.Nodes.Add(MyCurrentViewNode)
                Next
            End If
            RootNode.Nodes.Add(TableNode)
            RootNode.Nodes.Add(ViewNode)
            SchemaTreeView.Nodes.Add(RootNode)
            'Now Add Views

        Catch ex As Exception

            'when there is "an" error, do something, then continue running the app
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnGetSchema_Click(sender As System.Object, e As System.EventArgs) Handles btnGetSchema.Click
        'When "Table" is added a restiction in Table Type, only users tables are reurned
        Dim straRestrictions() As String = New String(3) {}
        btnGetData.Enabled = False
        'Clear the listbox
        lbSchema.Items.Clear()

        'Clear out table names
        cbTableName.Items.Clear()
        'cbTableName.Items.Add("ALL")

        straRestrictions(3) = cbTableType.SelectedItem

        If straRestrictions(3) = cbTableType.Items(0) Then 'Get ALL Tables (No Restrictions)
            _DataBaseTableNameDataTable = gDataBaseConnection.GetSchema("Tables")
        Else 'only get the tablenames of the selected table type
            _DataBaseTableNameDataTable = gDataBaseConnection.GetSchema("Tables", straRestrictions)
        End If



        Dim i As Integer
        Dim j As Integer = 0
        For i = 0 To _DataBaseTableNameDataTable.Rows.Count - 1 Step i + 1
            'System.Console.WriteLine(_DataBaseRecordSet.Rows(i)(2).ToString())
            ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(0).ToString())
            ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(1).ToString())
            ' ListBox1.Items.Add("TABLE " + i.ToString + ": " + _DataBaseTableNameDataTable.Rows(i)(2).ToString())
            For j = 0 To _DataBaseTableNameDataTable.Columns.Count - 1
                lbSchema.Items.Add("TABLE " + vbTab + "ROW: " + i.ToString + " COL: " + j.ToString + " - " + _DataBaseTableNameDataTable.Rows(i)(j).ToString())
                'Table Names
                If (Not cbTableName.Items.Contains(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)) And (j = 2) Then
                    cbTableName.Items.Add(_DataBaseTableNameDataTable.Rows(i)(j).ToString().Trim)
                End If

            Next

        Next
        cbTableName.SelectedItem = cbTableName.Items(0)
        cbColumnName.Items.Clear()
        cbColumnName.Items.Add("*")
        btnGetData.Enabled = True
    End Sub

    ''' <summary>
    ''' This Function was written to allow the user to limit the number of results returned.  The idea was that it would speed up the transactions with the
    ''' the database by limting the number of records accessed by the user.  The size of the data base will ultimatly grow making the current, archectecture that 
    ''' makes a local copy of all records ultimatly unwieldable...  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetData.Click

        Dim strMyFilter As String
        btnGetData.Enabled = False
        Dim MyDataTable As New DataTable

        _RecordIndexUpper = CInt(gMyCustomDataBaseAccess.GetMaxValueFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))
        _RecordIndexLower = CInt(gMyCustomDataBaseAccess.GetMinValueFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))
        _RecordCount = CInt(gMyCustomDataBaseAccess.GetRecordCountFromDatabaseColumn(gDataBaseConnection, "New ID", "Failure Report"))
        _RecordPointer = 0

        btnNext.Enabled = False
        btnPrev.Enabled = False

        ''The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        ''something with it
        Dim strDataTable As String = cbTableName.SelectedItem

        'now show your datatable to the world. ^_^ I've placed a datagridview on the form called: DataGridView1
        Try

            If (cbColumnName.Text = "*") Or cbColumnName.Text.Trim = "" Then
                strMyFilter = " ORDER BY [New ID] DESC"
            ElseIf chkExactMatch.Checked Then
                strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + "Like '" + txtFilter.Text.Trim + "'" + " ORDER BY [New ID] DESC"
            Else
                strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + " Like '%" + txtFilter.Text.Trim + "%'" + " ORDER BY [New ID] DESC"
            End If


            If cbNumberOfRecordsToRetrieve.SelectedItem = "ALL" Then

                _MyFailureReportDataTable = gMyCustomDataBaseAccess.GetDataCustomQuery(gDataBaseConnection, strDataTable, strMyFilter)
                DataGridView1.DataSource = _MyFailureReportDataTable
                _RecordPointer = 0
                _RecordOffset = _RecordIndexUpper
                btnNext.Enabled = False
                btnPrev.Enabled = False
            Else
                _RecordOffset = CInt(cbNumberOfRecordsToRetrieve.SelectedItem) + 1
                _MyFailureReportDataTable = gMyCustomDataBaseAccess.GetData(gDataBaseConnection, _RecordPointer, _RecordOffset, strDataTable, strMyFilter)
                DataGridView1.DataSource = _MyFailureReportDataTable

            End If
            If _RecordIndexUpper > _RecordOffset Then
                btnPrev.Enabled = True
            End If

            btnGetData.Enabled = True

            'DataGridView1.Sort(DataGridView1.Columns("NEW ID"), System.ComponentModel.ListSortDirection.Descending)


        Catch ex As Exception
            MsgBox(ex.ToString)
            btnGetData.Enabled = True
        End Try
        'DONE!()


        'Bind the control to the datatable
        Try
            txtReportNumber.DataBindings.Clear()
        Catch
        End Try


        _MyTestBinding = New Binding("Text", _MyFailureReportDataTable, "New ID")
        txtReportNumber.DataBindings.Add(_MyTestBinding)


        'Update / Deletes additions, you name it all use the same technology. 
        DataGridView2.ColumnCount = DataGridView1.ColumnCount
        cbColumnName.Items.Clear()
        cbColumnName.Items.Add("*")

        For i = 0 To DataGridView1.ColumnCount - 1
            DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
            cbColumnName.Items.Add(DataGridView1.Columns(i).Name)
        Next

        Try
            For Each row As DataGridViewRow In DataGridView2.Rows
                DataGridView2.Rows.Remove(row)
            Next
        Catch
        End Try

        'DataGridView2.Rows.Add()
    End Sub

    Private Sub btnInsertData_Click(sender As System.Object, e As System.EventArgs) Handles btnInsertData.Click

        Dim strColumnNames As String
        Dim strColumnvalues As String
        Dim iColumnCount As Integer = DataGridView2.ColumnCount ' the column count from the grid that has the input data
        Dim strDataTable As String = cbTableName.SelectedItem
        Dim MyRecord As New cCustomDataBaseAccess.cTable
        'make sure that there are at least one columns


        'Datatable to hold the Database Table Schema inotmation ( i.E. Column Name, Datatype, Size etc...
        Dim MyLocalDatatable As New DataTable
        'Get the Schema Information
        MyLocalDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gDataBaseConnection, "Failure Report")

        ''Create a Gridview to access the Schema information (This is easier for me at this point
        'Dim MyLocalDataGridView As New DataGridView
        ''Copy the schema informatio into the Gridview
        'MyLocalDataGridView.DataSource = MyLocalDatatable

        'now process each column  Datagrid2 row 0 holds the data to insert
        'Row 1 Holds the data condition ("True" or "False")  if it should be updated or not
        If iColumnCount > 0 Then
            Try
                'Flag to indicate if a Field in the new row should be populated
                Dim bPopulateThisField As Boolean = True 'Default
                'Get the tablename to be updated
                MyRecord.TableName = cbTableName.SelectedItem
                'Inialize the columns 
                MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

                Dim IsAutoIncrement As Boolean = False

                Try
                    IsAutoIncrement = MyLocalDatatable.Rows(0).Item("IsAutoIncrement")
                Catch ex As Exception
                    IsAutoIncrement = False
                End Try

                Try 'in case there is not a row defined
                    If IsAutoIncrement Then 'can't populate an auto increment field
                        bPopulateThisField = False
                    ElseIf DataGridView2.Rows(0).Cells(0).Value Is DBNull.Value Then 'should be safe but why bother?
                        bPopulateThisField = False
                    ElseIf String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(0).Value) Then 'should be safe but why bother?
                        bPopulateThisField = False
                    ElseIf String.IsNullOrEmpty(DataGridView2.Rows(1).Cells(0).Value) Then 'Indeterminate populate flag! Check for data!

                        If String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(0).Value) Then 'No Data, Don't Bother
                            bPopulateThisField = False 'Don't ppulate
                        Else
                            bPopulateThisField = True ' There us Data, so populate the field
                        End If
                    ElseIf DataGridView2.Rows(1).Cells(0).Value.ToString.ToLower = "false" Then
                        bPopulateThisField = False
                    Else 'There is data! lets populate it!
                        bPopulateThisField = True 'Default
                    End If
                Catch ex As Exception
                    bPopulateThisField = False 'Assume the data is bad and do not populate
                End Try

                'Add the column Name, Value, Datatype, and Populate Flad to the New Database Record
                MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn(DataGridView2.Columns(0).Name, DataGridView2.Rows(0).Cells(0).Value, MyLocalDatatable.Rows(0).Item("DataType"), bPopulateThisField))

                strColumnNames = DataGridView2.Columns(0).Name
                strColumnvalues = "'" + DataGridView2.Rows(0).Cells(0).Value + "'"

                'check to see if there are more than one columns and continue to build column list
                If iColumnCount > 1 Then
                    For i = 1 To DataGridView2.ColumnCount - 1

                        Try
                            IsAutoIncrement = MyLocalDatatable.Rows(i).Item("IsAutoIncrement")
                        Catch ex As Exception
                            IsAutoIncrement = False
                        End Try

                        Try 'in case there is not a row defined
                            If IsAutoIncrement Then 'can't populate an auto increment field
                                bPopulateThisField = False
                            ElseIf DataGridView2.Rows(0).Cells(i).Value Is DBNull.Value Then 'should be safe but why bother?
                                bPopulateThisField = False
                            ElseIf String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(i).Value) Then 'should be safe but why bother?
                                bPopulateThisField = False
                            ElseIf String.IsNullOrEmpty(DataGridView2.Rows(1).Cells(i).Value) Then
                                If String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(i).Value) Then
                                    bPopulateThisField = False
                                Else
                                    bPopulateThisField = True
                                End If
                            ElseIf DataGridView2.Rows(1).Cells(i).Value.ToString.ToLower = "false" Then
                                bPopulateThisField = False
                            Else 'There is data! lets populate it!
                                bPopulateThisField = True 'Default
                            End If
                        Catch ex As Exception
                            bPopulateThisField = False 'Assume the data is bad and do not populate
                        End Try

                        strColumnNames = strColumnNames + ", " + DataGridView2.Columns(i).Name
                        strColumnvalues = strColumnvalues + ", '" + DataGridView2.Rows(0).Cells(i).Value + "'"
                        MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn(DataGridView2.Columns(i).Name, DataGridView2.Rows(0).Cells(i).Value, MyLocalDatatable.Rows(i).Item("DataType"), bPopulateThisField))
                    Next
                End If

                'Dim mySQL_Command As New OleDb.OleDbCommand("INSERT INTO [" + strDataTable + "]" + " (" + strColumnNames + ") VALUES (" + strColumnvalues + ");", gDataBaseConnection)

                cCustomDataBaseAccess.InsertNewRecord(MyRecord, gDataBaseConnection)


                'btnGetData().PerformClick()

            Catch ex As Exception
                MsgBox("Error Filling DataAdaptor " + vbCrLf + ex.ToString)
            End Try

        End If

        btnGetData.PerformClick()

    End Sub


    Private Sub btnChangeDatabase_Click(sender As System.Object, e As System.EventArgs) Handles btnChangeDatabase.Click
        frmSelectDatabase.Show()
        gDataBaseConnection.Close()
        gDataBaseConnection.Dispose()
        gDataBaseConnection = New OleDbConnection(My.Settings.FailureReportDataBaseFullConnectionString)
        gDataBaseConnection.Open()
    End Sub

    Private Sub btnPrev_Click(sender As System.Object, e As System.EventArgs) Handles btnPrev.Click
        Dim strDataTable As String = cbTableName.SelectedItem
        Dim strMyFilter As String = ""

        Try
            If cbNumberOfRecordsToRetrieve.SelectedItem = "ALL" Then
                DataGridView1.DataSource = gMyCustomDataBaseAccess.GetDataCustomQuery(gDataBaseConnection, strDataTable, " ORDER BY [New ID] DESC")
                _RecordPointer = 0
                _RecordOffset = _RecordIndexUpper
                btnNext.Enabled = False
                btnPrev.Enabled = False
            Else
                _RecordOffset = CInt(cbNumberOfRecordsToRetrieve.SelectedItem) + 1
                _RecordPointer = _RecordPointer + _RecordOffset - 1 '(The first recoed will be the prveious last record if subtract 1)
                If _RecordPointer + _RecordOffset - 1 >= _RecordIndexUpper Then
                    _RecordPointer = _RecordIndexUpper - _RecordOffset
                    btnPrev.Enabled = False
                    btnNext.Enabled = True
                Else
                    btnNext.Enabled = True
                End If
                'Filter results

                If (cbColumnName.Text = "*") Or cbColumnName.Text.Trim = "" Then
                    strMyFilter = " ORDER BY [New ID] DESC"
                ElseIf chkExactMatch.Checked Then
                    strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + "Like '" + txtFilter.Text.Trim + "'" + " ORDER BY [New ID] DESC"
                Else
                    strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + " Like '%" + txtFilter.Text.Trim + "%'" + " ORDER BY [New ID] DESC"
                End If
                DataGridView1.DataSource = gMyCustomDataBaseAccess.GetData(gDataBaseConnection, _RecordPointer, _RecordOffset, strDataTable, strMyFilter)
            End If
            'DataGridView1.Sort(DataGridView1.Columns("NEW ID"), System.ComponentModel.ListSortDirection.Descending)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'DONE!()
        'Try
        Try
            txtReportNumber.DataBindings.Clear()
        Catch
        End Try

        _MyTestBinding = New Binding("Text", DataGridView1.DataSource, "New ID")
        txtReportNumber.DataBindings.Add(_MyTestBinding)

        'Update / Deletes additions, you name it all use the same technology. 
        DataGridView2.ColumnCount = DataGridView1.ColumnCount
        For i = 0 To DataGridView1.ColumnCount - 1
            DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
        Next
        Try
            For Each row As DataGridViewRow In DataGridView2.Rows
                DataGridView2.Rows.Remove(row)
            Next
        Catch
        End Try
    End Sub

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        Dim strDataTable As String = cbTableName.SelectedItem
        Dim strMyFilter As String = ""

        Try
            If cbNumberOfRecordsToRetrieve.SelectedItem = "ALL" Then
                DataGridView1.DataSource = gMyCustomDataBaseAccess.GetDataCustomQuery(gDataBaseConnection, strDataTable, " ORDER BY [New ID] DESC")
                _RecordPointer = 0
                _RecordOffset = _RecordIndexUpper
                btnNext.Enabled = False
                btnPrev.Enabled = False
            Else
                _RecordOffset = CInt(cbNumberOfRecordsToRetrieve.SelectedItem) + 1
                _RecordPointer = _RecordPointer - _RecordOffset + 1 'add one to display previous highest record as lowest record
                If _RecordPointer < 0 Then
                    _RecordPointer = 0
                    btnNext.Enabled = False
                    btnPrev.Enabled = True
                Else
                    btnNext.Enabled = True
                    btnPrev.Enabled = True
                End If


                If (cbColumnName.Text = "*") Or cbColumnName.Text.Trim = "" Then
                    strMyFilter = " ORDER BY [New ID] DESC"
                ElseIf chkExactMatch.Checked Then
                    strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + "Like '" + txtFilter.Text.Trim + "'" + " ORDER BY [New ID] DESC"
                Else
                    strMyFilter = " WHERE " + "[" + cbColumnName.Text.Trim + "]" + " Like '%" + txtFilter.Text.Trim + "%'" + " ORDER BY [New ID] DESC"
                End If

                DataGridView1.DataSource = gMyCustomDataBaseAccess.GetData(gDataBaseConnection, _RecordPointer, _RecordOffset, strDataTable, strMyFilter)


                ' DataGridView1.Sort(DataGridView1.Columns("NEW ID"), System.ComponentModel.ListSortDirection.Descending)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'DONE!()
        Try
            txtReportNumber.DataBindings.Clear()
        Catch
        End Try
        _MyTestBinding = New Binding("Text", DataGridView1.DataSource, "New ID")

        txtReportNumber.DataBindings.Add(_MyTestBinding)

        'Update / Deletes additions, you name it all use the same technology. 
        DataGridView2.ColumnCount = DataGridView1.ColumnCount
        For i = 0 To DataGridView1.ColumnCount - 1
            DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
        Next
        Try
            For Each row As DataGridViewRow In DataGridView2.Rows
                DataGridView2.Rows.Remove(row)
            Next
        Catch
        End Try
    End Sub

    Private Sub btnManualQuery_Click(sender As System.Object, e As System.EventArgs) Handles btnManualQuery.Click



        _RecordIndexLower = 0
        _RecordIndexUpper = CInt(cbNumberOfRecordsToRetrieve.SelectedValue)
        ''The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        ''something with it
        Dim strDataTable As String = cbTableName.SelectedItem


        btnPrev.Enabled = False
        'now show your datatable to the world. ^_^ I've placed a datagridview on the form called: DataGridView1
        Try


            DataGridView1.DataSource = gMyCustomDataBaseAccess.GetData(gDataBaseConnection, txtManualQuery.Text, cbTableName.Text, txtLowerRecord.Text, txtUpperRecord.Text)


            DataGridView1.Sort(DataGridView1.Columns("NEW ID"), System.ComponentModel.ListSortDirection.Descending)


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'DONE!()

        'Update / Deletes additions, you name it all use the same technology. 
        DataGridView2.ColumnCount = DataGridView1.ColumnCount
        cbColumnName.Items.Clear()
        cbColumnName.Items.Add("*")

        For i = 0 To DataGridView1.ColumnCount - 1
            DataGridView2.Columns(i).Name = DataGridView1.Columns(i).Name
            cbColumnName.Items.Add(DataGridView1.Columns(i).Name)
        Next

        Try
            For Each row As DataGridViewRow In DataGridView2.Rows
                DataGridView2.Rows.Remove(row)
            Next
        Catch
        End Try

        'DataGridView2.Rows.Add()
    End Sub


    Private Sub btnGetColumnInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnGetColumnInfo.Click
        Dim MyDatatable As New DataTable
        MyDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gDataBaseConnection, "Failure Report")

        DataGridView1.DataSource = MyDatatable
    End Sub

    Private Sub btnUpdateData_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdateData.Click
        Dim strColumnNames As String
        Dim strColumnvalues As String

        'Datagridview 2 has th e same name and number of columns as gridview 1 which is bound to the database table to be updated
        Dim iColumnCount As Integer = DataGridView2.ColumnCount ' the column count from the grid that has the input data
        'This is the name of the Table that is currently bound to datagridview 1
        Dim strDataTable As String = cbTableName.SelectedItem
        'Create a buffer ot hold the data
        Dim MyRecord As New cCustomDataBaseAccess.cTable
        'make sure that there are at least one columns

        'Datatable to hold the Database Table Schema inotmation ( i.E. Column Name, Datatype, Size etc...
        Dim MyLocalDatatable As New DataTable

        'Get the Schema Information
        MyLocalDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gDataBaseConnection, "Failure Report")

        ''Create a Gridview to access the Schema information (This is easier for me at this point
        'Dim MyLocalDataGridView As New DataGridView
        ''Copy the schema informatio into the Gridview
        'MyLocalDataGridView.DataSource = MyLocalDatatable

        'now process each column  DataGridView2 row 0 holds the data to insert
        'Row 1 Holds the data condition ("True" or "False")  if it should be updated or not
        If iColumnCount > 0 Then 'Make sure that there is at least one column
            Try
                'Flag to indicate if a Field in the new row should be populated
                Dim bPopulateThisField As Boolean = True 'Default
                'Get the tablename to be updated
                MyRecord.TableName = cbTableName.SelectedItem
                'Inialize the columns 
                MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

                Dim IsAutoIncrement As Boolean = False

                Try
                    IsAutoIncrement = MyLocalDatatable.Rows(0).Item("IsAutoIncrement")
                Catch ex As Exception
                    IsAutoIncrement = False
                End Try

                Try 'in case there is not a row defined
                    If IsAutoIncrement Then 'can't populate an auto increment field
                        bPopulateThisField = False
                    ElseIf DataGridView2.Rows(0).Cells(0).Value Is DBNull.Value Then 'Sometimes want to remove data
                        bPopulateThisField = False
                    ElseIf String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(0).Value) Then 'should be safe but why bother?
                        bPopulateThisField = False
                    ElseIf String.IsNullOrEmpty(DataGridView2.Rows(1).Cells(0).Value) Then 'Indeterminate populate flag! Check for data!

                        If String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(0).Value) Then 'No Data, Don't Bother
                            bPopulateThisField = False 'Don't ppulate
                        Else
                            bPopulateThisField = True ' There is Data, so populate the field
                        End If
                    ElseIf DataGridView2.Rows(1).Cells(0).Value.ToString.ToLower = "false" Then
                        bPopulateThisField = False
                    Else 'There is data! lets populate it!
                        bPopulateThisField = True 'Default
                    End If
                Catch ex As Exception
                    bPopulateThisField = False 'Assume the data is bad and do not populate
                End Try

                'Add the column Name, Value, Datatype, and Populate Flad to the New Database Record
                MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn(DataGridView2.Columns(0).Name, DataGridView2.Rows(0).Cells(0).Value, MyLocalDatatable.Rows(0).Item("DataType"), bPopulateThisField))
                Dim ColumnName As String = MyLocalDatatable.Rows(0).Item("ColumnName")
                strColumnNames = DataGridView2.Columns(0).Name
                strColumnvalues = "'" + DataGridView2.Rows(0).Cells(0).Value + "'"

                'check to see if there are more than one columns and continue to build column list
                If iColumnCount > 1 Then
                    For i = 1 To DataGridView2.ColumnCount - 1

                        Try
                            IsAutoIncrement = MyLocalDatatable.Rows(i).Item("IsAutoIncrement")
                        Catch ex As Exception
                            IsAutoIncrement = False
                        End Try

                        Try 'in case there is not a row defined
                            If IsAutoIncrement Then 'can't populate an auto increment field
                                bPopulateThisField = False
                            ElseIf DataGridView2.Rows(0).Cells(i).Value Is DBNull.Value Then 'should be safe but why bother?
                                bPopulateThisField = False
                            ElseIf String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(i).Value) Then 'should be safe but why bother?
                                bPopulateThisField = False
                            ElseIf String.IsNullOrEmpty(DataGridView2.Rows(1).Cells(i).Value) Then
                                If String.IsNullOrEmpty(DataGridView2.Rows(0).Cells(i).Value) Then
                                    bPopulateThisField = False
                                Else
                                    bPopulateThisField = True
                                End If
                            ElseIf DataGridView2.Rows(1).Cells(i).Value.ToString.ToLower = "false" Then
                                bPopulateThisField = False
                            Else 'There is data! lets populate it!
                                bPopulateThisField = True 'Default
                            End If
                        Catch ex As Exception
                            bPopulateThisField = False 'Assume the data is bad and do not populate
                        End Try

                        strColumnNames = strColumnNames + ", " + DataGridView2.Columns(i).Name
                        strColumnvalues = strColumnvalues + ", '" + DataGridView2.Rows(0).Cells(i).Value + "'"
                        MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn(DataGridView2.Columns(i).Name, DataGridView2.Rows(0).Cells(i).Value, MyLocalDatatable.Rows(i).Item("DataType"), bPopulateThisField))
                        ColumnName = MyLocalDatatable.Rows(i).Item("ColumnName")
                        Dim MyStop As Integer = 1
                    Next
                End If

                'Dim mySQL_Command As New OleDb.OleDbCommand("INSERT INTO [" + strDataTable + "]" + " (" + strColumnNames + ") VALUES (" + strColumnvalues + ");", gDataBaseConnection)

                cCustomDataBaseAccess.UpdateExistingRecord(MyRecord, gDataBaseConnection, txtUpdateColumn.Text.Trim, txtUpdateIndex.Text)


                'btnGetData().PerformClick()

            Catch ex As Exception
                MsgBox("Error Filling DataAdaptor " + vbCrLf + ex.ToString)
            End Try

        End If

        btnGetData.PerformClick()
    End Sub


    Private Sub frmDataBaseBrowser_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
       
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnShowFailureBrowser.Click
        Try
            frmFailureBrowser.ShowDialog()
            ' gDataBaseConnection.Close()

        Catch ex As Exception

        End Try
    
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim MyConnection As OleDb.OleDbConnection
        Dim myTable As New DataTable
        Dim myTableAdaptor As OleDb.OleDbDataAdapter

        Dim DbUserName As String
        Dim DbPassword As String
        Try
            MyConnection = New OleDb.OleDbConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
            MyConnection.Open()

            myTableAdaptor = New OleDb.OleDbDataAdapter("SELECT * FROM [Users] WHERE username = 'TCCchair'", MyConnection)

            myTableAdaptor.Fill(myTable)
       
            If myTable.Rows.Count = 1 Then
                DbPassword = myTable.Rows(0)("password").ToString.Trim
                DbUserName = myTable.Rows(0)("username").ToString.Trim
            ElseIf myTable.Rows.Count > 1 Then
                'Some kind of wild card slipped through?
                MsgBox("Check your username and password and try again")
            Else
                MsgBox("Check your username and password and try again")
            End If

            Dim MyStop As Integer = 1
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

End Class
