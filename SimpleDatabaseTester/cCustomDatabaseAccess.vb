Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO

''' <summary>
''' Custom class for manipulating and accessing database data
''' </summary>
''' <remarks>Frank Boudreau 2016</remarks>
Public Class cCustomDataBaseAccess

#Region "General SQL Variables"
    Public SQLCon As SqlConnection 'New SqlConnection With {.ConnectionString = "Data Source=USLAFNB365\SQLEXPRESS;Initial Catalog=TEST_MATRIX;Integrated Security=True"}
    Public SQLCmd As SqlCommand
    Public SQL_DataAdapter As SqlDataAdapter
    Public SQL_Dataset As DataSet
    Public SQL_Datatable As DataTable
    Public SQL_QueryResults As DataTable
    Public SQLParameters As New List(Of SqlParameter)
    ''' <summary>
    ''' List of Temporary Files (Path and Name), used for tracking and deleting the files 
    ''' </summary>
    ''' <remarks>Frank Boudreau 2017</remarks>
    Public TempFilesToCleanUp As New List(Of String)


    Public Enum eCollation
        None
        CaseSensitive
    End Enum
    Public Enum SQLErrors As Integer
        ''' <summary>
        ''' Returned when an error occurs trying to excute a SQL-Query
        ''' </summary>
        ''' <remarks></remarks>
        QueryError = -1
        ''' <summary>
        ''' Returned when an error occurs executing a SQL  Non-Query Action
        ''' </summary>
        ''' <remarks></remarks>
        NonQueryError = -2
        ''' <summary>
        ''' Error Returned if an Invalid path and/or file name is passed to function
        ''' </summary>
        ''' <remarks></remarks>
        FilePathInvalid = -3
        ''' <summary>
        ''' Returned if an error occurs while trying to open a file stored on the database
        ''' </summary>
        ''' <remarks></remarks>
        DatabaseFileRetrievalError = -4
    End Enum
#End Region 'General SQL Variables

#Region "General SQL Classes, Functions and Subs"
    Public Sub SetSQLConnectionString(ConnectionString As String)
        If SQLCon IsNot Nothing Then
            SQLCon = New SqlConnection
        End If

        If SQLCon.State <> ConnectionState.Closed Then
            Try
                SQLCon.Close()
            Catch ex As SqlException
                MsgBox("Error or Warning reporting while closing Sql Connection" + vbCrLf + ex.ToString)
            End Try
        End If

        SQLCon.ConnectionString = ConnectionString
    End Sub

    Public Sub SetSQLConnectionString(strDataSource As String, strInitialCatalog As String, Optional bIntegeratedSecurity As Boolean = True)
        If SQLCon IsNot Nothing Then
            SQLCon = New SqlConnection
        End If

        If SQLCon.State <> ConnectionState.Closed Then
            Try
                SQLCon.Close()
            Catch ex As SqlException
                MsgBox("Error or Warning reporting while closing Sql Connection" + vbCrLf + ex.ToString)
            End Try
        End If

        Try
            SQLCon.ConnectionString = "Data Source=" + strDataSource + ";Initial Catalog=" + strInitialCatalog + ";Integrated Security=" + bIntegeratedSecurity.ToString
        Catch ex As Exception
            MsgBox("Error Setting SQL Connection Source" + vbCrLf + ex.ToString)
        End Try

    End Sub





    ''' <summary>
    ''' This Function Returns True if a Connection to the Database can be established, else False
    ''' </summary>
    ''' <returns>Retruns True of a Connection to the database can be established, else false</returns>
    ''' <remarks></remarks>
    Public Function HasConnection()
        Try
            SQLCon.Open()

            SQLCon.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            'try to close
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return False
        End Try
    End Function

    ''' <summary>
    ''' This Function Runs the Passed SQL Query and returns the Number of Rows Effected. If Key is unique it will return only one row.
    ''' </summary>
    ''' <param name="TableName">Table Name to Query</param>
    ''' <param name="KeyColumn"> Column to Query</param>
    ''' <param name="KeyValue"> Value to Query Against</param>
    ''' <returns>The number of rows effected, SQLErrors.QueryError on error, The results are put in Public Dataset 'SQL_Dataset'</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetNumberOfRows(TableName As String, KeyColumn As String, KeyValue As Integer) As Integer
        Dim Query As String
        Dim NumberOfRows As Integer = 0
        Query = "SELECT *" + _
                " FROM [" + TableName + "]" + _
                " WHERE [" + KeyColumn + "] = " + KeyValue.ToString
        Try
            'Open the connection
            SQLCon.Open()

            'Create new SqlCommnand 
            SQLCmd = New SqlCommand(Query, SQLCon)

            'Get data using adaptor and load into data grid
            SQL_DataAdapter = New SqlDataAdapter(SQLCmd)
            'Create new Dataset
            SQL_Dataset = New DataSet
            'Fill the Dataset
            NumberOfRows = SQL_DataAdapter.Fill(SQL_Dataset)

            'close the Connection
            SQLCon.Close()

            Return NumberOfRows
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try
    End Function

    ''' <summary>
    ''' This Function Runs the Passed SQL Query and returns the Number of Rows Effected. If Key is unique it will return only one row.
    ''' </summary>
    ''' <param name="Query">SQL text query</param>
    ''' <returns># of records, SQLErrors.QueryError on error... The results are put in Public Datatable 'SQL_QueryResults' </returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function RunQuery(Query As String) As Integer
        Dim NumberOfRows As Integer = 0
        Try
            'Open the connection
            SQLCon.Open()

            'Create new SqlCommnand 
            SQLCmd = New SqlCommand(Query, SQLCon)

            'Get data using adaptor and load into data grid
            SQL_DataAdapter = New SqlDataAdapter(SQLCmd)
            'Create new Dataset
            SQL_QueryResults = New DataTable
            'Fill the Dataset
            NumberOfRows = SQL_DataAdapter.Fill(SQL_QueryResults)

            'close the Connection
            SQLCon.Close()

            Return NumberOfRows
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try
    End Function

    ''' <summary>
    ''' This Function will return the number TableNames in a database, excluding the System tables
    ''' </summary>
    ''' <returns> # of rows (# of tables), SQLErrors.QueryError on error...The results are put in Public Datatable 'SQL_QueryResults''</returns>
    ''' <remarks>Frank Boudreau</remarks>
    Public Function GetTableNames() As Integer
        Try
            'SQL String to exclude sysdiagrams
            Dim Query As String = "SELECT S.[name] AS Owner, T.[name] AS TableName FROM sys.tables AS T JOIN sys.schemas AS S ON S.schema_id = T.schema_id WHERE T.is_ms_shipped = 0 AND T.[name] <> 'sysdiagrams'"
            Return RunQuery(Query)
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try
    End Function

    ''' <summary>
    ''' This Function Checks to see if a Record is Unique based on a single column and column value as string
    ''' </summary>
    ''' <param name="TableName">Table to Query</param>
    ''' <param name="ColumnOfUniqueValuesName">Column Name </param>
    ''' <param name="CellValue">Column Value (Passes as String</param>
    ''' <returns>True if now matching recoords found, False otherwise. Passes Exception on error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function IsUnique(TableName As String, ColumnOfUniqueValuesName As String, CellValue As String) As Boolean
        IsUnique = False 'assume it is not
        Try
            Dim Query As String = "SELECT [" + ColumnOfUniqueValuesName.Trim + "] FROM [" + TableName.Trim + "] WHERE [" + ColumnOfUniqueValuesName.Trim + "] LIKE '" + CellValue.Trim + "'"
            If RunQuery(Query) Then
                'If SQL_Dataset.Tables(0).Rows.Count > 0 Then
                IsUnique = False
            Else
                IsUnique = True
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
            If Not SQLCon Is Nothing Then
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
            End If
            Throw New Exception("Error Checking for Unique Record: " + vbCrLf + ex.ToString)
        End Try

    End Function

    ''' <summary>
    ''' This Function Checks to see if a Record is Unique based on a single column and column value as integer and Retruns true if unique
    ''' </summary>
    ''' <param name="TableName">Table to Query</param>
    ''' <param name="ColumnOfUniqueValuesName">Column Name </param>
    ''' <param name="CellValue">Column Value passed as integer</param>
    ''' <returns>True if now matching recoords found, False otherwise. Passes Exception on error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function IsUnique(TableName As String, ColumnOfUniqueValuesName As String, CellValue As Integer) As Boolean
        IsUnique = False 'assume it is not
        Dim Query As String = "SELECT [" + ColumnOfUniqueValuesName.Trim + "] FROM [" + TableName.Trim + "] WHERE [" + ColumnOfUniqueValuesName.Trim + "] = " + CellValue.ToString
        If RunQuery(Query) > 0 Then
            'If SQL_Dataset.Tables(0).Rows.Count > 0 Then
            IsUnique = False
        Else
            IsUnique = True
        End If

    End Function

    ''' <summary>
    ''' This Function hecks to see if a Record is Unique based on multiple columns, and associated values 
    ''' </summary>
    ''' <param name="TableName">DAtabase Tablename</param>
    ''' <param name="Columns">The columns with the Values to check</param>
    ''' <param name="CellValue">The Cell values to check</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function IsUnique(ByVal TableName As String, ByVal Columns As List(Of String), ByVal CellValue As List(Of String)) As Boolean
        Try

            Dim Query As String
            If Columns.Count = CellValue.Count And Columns.Count > 0 Then
                Query = "SELECT * FROM [" + TableName + "] WHERE " + Columns.Item(0) + " = " + CellValue.Item(0)
                For i = 1 To Columns.Count - 1

                    Query = Query + " AND " + Columns.Item(i) + " = " + CellValue.Item(i)
                Next


            Else
                Throw New Exception("There must be a Cellvalue for each Column, and at least one parameter must be specified")
            End If

            ' RunQuery(Query)
            If RunQuery(Query) > 0 Then
                IsUnique = False
            Else
                IsUnique = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' This function populates and returns a table of unique values in a column in a table
    ''' </summary>
    ''' <param name="strMyColumn">Column name with data</param>
    ''' <param name="strDataTableName">Table name to query</param>
    ''' <param name="MyFilter">Optional Filter</param>
    ''' <returns>Table with query values, Returns nothing on error...</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetDistinctData(ByRef strMyColumn As String, ByVal strDataTableName As String, Optional ByVal MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        Dim Query As String = "SELECT DISTINCT [" + strMyColumn + "] FROM [" + strDataTableName + "]" + MyFilter

        'Open the connection
        SQLCon.Open()

        'Create new SqlCommnand 
        SQLCmd = New SqlCommand(Query, SQLCon)

        'Get data using adaptor and load into data grid
        Dim MySQL_DataAdapter = New SqlDataAdapter(SQLCmd)
        'Create new Dataset
        Dim MyDataTable As New DataTable(strDataTableName)

        'close the Connection
        SQLCon.Close()

        'Bash the query results in the datatable
        Try
            MySQL_DataAdapter.Fill(MyDataTable)
            Return MyDataTable
        Catch ex As Exception
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            MsgBox(ex.ToString)
            Return Nothing
        End Try




    End Function

    ''' <summary>
    ''' This function will return a single table with data from multiple tables in a single query
    ''' </summary>
    ''' <param name="ColumnNames">List of Explict Columns: 'Table_Name.ColumnName' </param>
    ''' <param name="TableNames">List of Tables: 'Table_Name' </param>
    ''' <param name="Filters">List Of Filter Conditions Explicit Defination</param>
    ''' <returns># of Rows Found on success, SQLErrors.QueryError for Unknow error, -2 if Data input error. 
    ''' The Public Variable 'SQL_Dataset' holds the result of the Query  </returns>
    ''' <remarks></remarks>
    Public Function GetDataFromLinkedTables(ColumnNames As List(Of String), TableNames As List(Of String), Filters As List(Of String)) As Integer
        GetDataFromLinkedTables = 0 'init to 0
        Try
            'must be at least one column, two tables and one filter of each
            If ColumnNames.Count > 0 And TableNames.Count > 1 And Filters.Count > 0 _
                And ColumnNames(0).Trim <> "" And TableNames(0).Trim <> "" And TableNames(1).Trim <> "" And Filters(0).Trim <> "" Then

                Dim Query As String

                'Build 'SELECT' portion. These are the explict columns to return TABL_NAME.COLUMN_NAME
                Query = "SELECT " + ColumnNames(0)
                For i = 1 To ColumnNames.Count - 1
                    If ColumnNames(i).Trim <> "" Then
                        Query = Query + ", " + ColumnNames(i)
                    Else
                        MsgBox("Error Found in 'ColumnNames'. There must be at least one Valid column, two Valid tables, and one SQL filter defined to use this function")
                        Return -2
                    End If


                Next

                'Build The FROM portion (These are the Tables)
                Query = Query + " FROM " + TableNames(0)
                For i = 1 To TableNames.Count - 1
                    If TableNames(i).Trim <> "" Then
                        Query = Query + ", " + TableNames(i)
                    Else
                        MsgBox("Error Found in 'TableNames'. There must be at least one Valid column, two Valid tables, and one SQL filter defined to use this function")
                        Return -2
                    End If
                Next


                'Build The Filter

                Query = Query + " WHERE " + Filters(0)

                For i = 1 To Filters.Count - 1
                    If Filters(i).Trim <> "" Then
                        Query = Query + " AND " + Filters(i)
                    Else
                        MsgBox("Error found in 'Filters'. There must be at least one Valid column, two Valid tables, and one SQL filter defined to use this function")
                        Return -2
                    End If
                Next

                Return RunQuery(Query)

            Else
                Return -2
                MsgBox("There must be at least one Valid column, two Valid tables, and one SQL filter defined to use this function")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try
    End Function

    ''' <summary>
    ''' This Function Queries the Database for all of the Data in the named Table and returns the number of records found on success.
    ''' </summary>
    ''' <param name="TableName">Table Name (Required)</param>
    ''' <param name="FilterColumnName">Column to Filter On.  Multiple Values must be comma Delimited..</param>
    ''' <param name="FilterValue">Filter Value...Multiple Values must be comma Delimited..</param>
    ''' <returns> # of Records found, SQLErrors.QueryError on Error</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetTableData(TableName As String, Optional FilterColumnName As String = "", Optional FilterValue As String = "") As Integer
        Try
            Dim Query As String = ""

            If FilterColumnName = "" Or FilterValue = "" Then
                'Return Entire Table
                Query = "SELECT * FROM [" + TableName + "]"
            Else
                Dim ColumnName() As String = FilterColumnName.Split(",")
                Dim CellValue() As String = FilterValue.Split(",")

                If ColumnName.Count = CellValue.Count Then 'must have equal elements
                    Query = "SELECT * FROM [" + TableName + "] WHERE [" + ColumnName(0) + "] = " + CellValue(0)

                    For i = 1 To ColumnName.Count - 1
                        Query = Query + " AND [" + ColumnName(i) + "] = " + CellValue(i)
                    Next
                Else
                    'Return entire table
                    Query = "SELECT * FROM [" + TableName + "]"
                End If

            End If

            Return RunQuery(Query)
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try

    End Function

    ''' <summary>
    ''' This Function Queries the Database for all of the Data in the named Table and returns the number of records found on success.
    ''' </summary>
    ''' <param name="TableName">Table Name (Required)</param>
    ''' <param name="FilterColumnName">Columns to Filter On (String Array).</param>
    ''' <param name="FilterValue">Filter Values (String array) </param>
    ''' <returns> # of Records found, SQLErrors.QueryError on Error</returns>
    ''' <remarks></remarks>
    Public Overloads Function GetTableData(TableName As String, FilterColumnName() As String, FilterValue() As String) As Integer
        Try
            Dim Query As String = ""

            If FilterColumnName.Count <> FilterValue.Count Then
                'Return Entire Table, these must habe the same number elements
                Query = "SELECT * FROM [" + TableName + "]"
            Else
                Dim ColumnName() As String = FilterColumnName
                Dim CellValue() As String = FilterValue

                If ColumnName.Count = CellValue.Count Then 'must have equal elements
                    Query = "SELECT * FROM [" + TableName + "] WHERE [" + ColumnName(0) + "] = " + CellValue(0)

                    For i = 1 To ColumnName.Count - 1
                        Query = Query + " AND [" + ColumnName(i) + "] = " + CellValue(i)
                    Next
                Else
                    'Return entire table
                    Query = "SELECT * FROM [" + TableName + "]"
                End If

            End If

            Return RunQuery(Query)
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try

    End Function



    ''' <summary>
    ''' This Function Opens a connection, excutes a 'Non Query', closes the connection and returns the number of Rows Effected...
    ''' </summary>
    ''' <param name="NonQuery">SQL Non Query Command (String)</param>
    ''' <returns>Number of Rows effected</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function ExecuteNonQuery(NonQuery As String) As Integer
        Dim NumRowsEffected As Integer = 0
        Try
            SQLCon.Open()

            SQLCmd = New SqlCommand(NonQuery, SQLCon)
            NumRowsEffected = SQLCmd.ExecuteNonQuery()

            SQLCon.Close()
            Return NumRowsEffected
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.NonQueryError
        End Try

    End Function


    ''' <summary>
    ''' This is a helper function for inserting a new record into a database, returning the number of rows effected.
    ''' </summary>
    ''' <param name="TableName">Table to Insert data into</param>
    ''' <param name="Columns">This is the column names to insert data into for each column there should be a cell value</param>
    ''' <param name="CellValue">This is  the data value to insert into the database. Char values should be
    ''' inclosed in single quotes. 'Frank' for example if column was FIRST_NAME.  Booleans should be True or False
    ''' or 1.tostring / 0.tostring.  Other Integer values should be IntegerValue.tostring.  I am not sure yet how
    ''' insert blobs, pictures, other large files.</param>
    ''' <returns>Number of Rows effected, or SQLERRORS. ...</returns>
    ''' <remarks></remarks>
    Public Overloads Function Insert(TableName As String, Columns As List(Of String), CellValue As List(Of String)) As Integer
        Try
            Dim NumRowsEffected As Integer = 0
            Dim NonQuery As String
            If Columns.Count = CellValue.Count Then
                NonQuery = "INSERT into [" + TableName + "] (" + Columns.Item(0)
                For i = 1 To Columns.Count - 1

                    NonQuery = NonQuery + "," + Columns.Item(i)
                Next
                NonQuery = NonQuery + ") VALUES (" + CellValue.Item(0)
                For i = 1 To CellValue.Count - 1

                    NonQuery = NonQuery + "," + CellValue.Item(i)
                Next
                NonQuery = NonQuery + ")"

            Else
                Throw New Exception("There must be a Cellvalue for each Column")
            End If

            SQLCon.Open()
            SQLCmd = New SqlCommand(NonQuery, SQLCon)

            NumRowsEffected = SQLCmd.ExecuteNonQuery()

            SQLCon.Close()
            Return NumRowsEffected
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.NonQueryError
        End Try

    End Function


    ''' <summary>
    ''' This Function Updates a value in a databae table using a single KeyColumn and Key value, and returns the number of rows effected.
    ''' </summary>
    ''' <param name="Table">Table to Update</param>
    ''' <param name="TableColumn">Column to update</param>
    ''' <param name="CellValue">The value to Update</param>
    ''' <param name="KeyColumn">Key Column (Typically Primary key) If not a column of unique values than mutiple
    ''' records could be updated</param>
    ''' <param name="Key">The Value used to filter which rows will be uupdated.  If unnique or proamry key, only one
    ''' match should occur</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function UpdateData(Table As String, TableColumn As String, CellValue As String, KeyColumn As String, Key As Integer) As Integer
        Try
            SQLCon.Open()

            Dim Command As String = "UPDATE [" + Table + "] SET [" + TableColumn + "] = '" + CellValue.Trim + "' WHERE " + KeyColumn + " = " + Key.ToString

            SQLCmd = New SqlCommand(Command, SQLCon)

            Dim ChangeCount As Integer = SQLCmd.ExecuteNonQuery

            SQLCon.Close()
            Return ChangeCount
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            'Error code
            Return SQLErrors.QueryError
        End Try

    End Function

    ''' <summary>
    ''' Function to Update Multiple Columns as inidcated by the value in the Key coluimn, returns the number of rows effected.
    ''' </summary>
    ''' <param name="Table">Datatable to update example "USER"</param>
    ''' <param name="TableColumn">Column Table to update. Example "USER_NAME"</param>
    ''' <param name="CellValue">Value to insert in column, must be in order of column names
    ''' Strings must be wrapped with single Quotes example "'Frank'", Integers must be converted to string
    ''' example FrankAge.ToString.  "NULL" is used to set the Cell value to DBNull.Value </param>
    ''' <param name="KeyName">Column Name for the Primary Key of the Table</param>
    ''' <param name="KeyValue">Primary key of the row to be updated</param>
    ''' <returns>The number of rows Effected (One if Primary or Unique Key column used), else SQLErrors.QueryError on error</returns>
    ''' <remarks>Frank Boudreau Landis Gyr 2015</remarks>
    Public Overloads Function UpdateData(Table As String, TableColumn As List(Of String), CellValue As List(Of String), KeyName As String, KeyValue As Integer) As Integer
        Try

            If TableColumn.Count = CellValue.Count Then
                Dim Command As String = "UPDATE [" + Table + "] SET [" + TableColumn.Item(0) + "] = " + CellValue.Item(0)

                For i = 1 To TableColumn.Count - 1
                    Command = Command + ", [" + TableColumn.Item(i) + "] = " + CellValue.Item(i)
                Next
                Command = Command + " WHERE [" + KeyName + "] = " + KeyValue.ToString
                SQLCon.Open()
                SQLCmd = New SqlCommand(Command, SQLCon)



                Dim ChangeCount As Integer = SQLCmd.ExecuteNonQuery

                SQLCon.Close()

                Return ChangeCount
            Else
                Throw New Exception("There must be a Cellvalue for each Column")
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            'Error code
            Return SQLErrors.QueryError
        End Try

    End Function


    ''' <summary>
    ''' Query and return the max value of a column in a database table
    ''' </summary>
    ''' <param name="strColumnName">Column to Query</param>
    ''' <param name="strDataTableName">Table to Query</param>
    ''' <returns>Max value as integer</returns>
    ''' <remarks></remarks>
    Public Function GetMaxValueFromDatabaseColumn(ByVal strColumnName As String, ByRef strDataTableName As String) As String

        Dim MyQuery As String = "SELECT MAX " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMaxValueFromDatabaseColumn = ""

        RunQuery(MyQuery)

        Try
            GetMaxValueFromDatabaseColumn = SQL_Dataset.Tables(0).Rows(0).Item(strColumnName)

        Catch ex As Exception
            MsgBox(ex.ToString)
            Throw New Exception("Error Querying Max Value of Column " + strColumnName + " in Table" + strDataTableName + "." _
                                + vbCrLf + ex.ToString)
        End Try

        Return GetMaxValueFromDatabaseColumn

    End Function

    ''' <summary>
    ''' Query and return the min value of a column in a database table
    ''' </summary>
    ''' <param name="strColumnName">Column to Query</param>
    ''' <param name="strDataTableName">Table to Query</param>
    ''' <returns>Min value as string</returns>
    ''' <remarks></remarks>
    Public Function GetMinValueFromDatabaseColumn(ByVal strColumnName As String, ByRef strDataTableName As String) As String

        Dim MyQuery As String = "SELECT MIN " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMinValueFromDatabaseColumn = ""

        RunQuery(MyQuery)

        Try
            GetMinValueFromDatabaseColumn = SQL_Dataset.Tables(0).Rows(0).Item(strColumnName)

        Catch ex As Exception
            MsgBox(ex.ToString)
            Throw New Exception("Error Querying Max Value of Column " + strColumnName + " in Table" + strDataTableName + "." _
                                + vbCrLf + ex.ToString)
        End Try

        Return GetMinValueFromDatabaseColumn

    End Function


    ''' <summary>
    ''' This function takes a table with varbinary or image or another  column types incapabiliable with a DGV to data taht is compatable
    ''' The actual data is not displayed but instead the data types in theses cases
    ''' </summary>
    ''' <param name="MyDataTable">Datatable with types incapatiable with DGV</param>
    ''' <returns>Safe Datatable, or throws excepption on error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function MakeTableDGVSafe(MyDataTable As DataTable) As DataTable
        Try

            Dim MyClonedDataTable As DataTable = MyDataTable.Clone()

            For i As Integer = 0 To MyClonedDataTable.Columns.Count - 1
                MyClonedDataTable.Columns(i).DataType = GetType([String])
            Next


            For j As Integer = 0 To MyDataTable.Rows.Count - 1
                Dim newRow = MyClonedDataTable.NewRow()
                For i As Integer = 0 To MyDataTable.Columns.Count - 1
                    newRow.SetField(i, Convert.ToString(MyDataTable.Rows(j).Item(i)))
                Next

                MyClonedDataTable.Rows.Add(newRow)
            Next

            Return MyClonedDataTable
        Catch ex As Exception
            'MsgBox("Unable to Process DataTable. REturning Original Table")
            Throw New Exception("Unable to Process DataTable. REturning Original Table" + vbCrLf + ex.ToString)
            ' Return MyDataTable
        End Try
    End Function

#End Region 'General SQL Functions and Subs

#Region "Write and Read Files To SQL Database"

    ''' <summary>
    ''' This Function is used to write a PDF file to a Database Table
    ''' </summary>
    ''' <param name="TableName">Database Table Name</param>
    ''' <param name="PDFColumnName">Column to hold PDF file</param>
    ''' <param name="IDColumn">The Primary or other unique key column of the TAble</param>
    ''' <param name="ID_Value">The unique value to write to the Database</param>
    ''' <returns>'Number of Rows Effected, (Should be 1 or 0) else SQLErrors.QueryError </returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function Save_PDF_ToDatabase(TableName As String, PDFColumnName As String, IDColumn As String, ID_Value As Integer) As Integer

        Dim RowsEffected As Integer = 0
        Dim MyFileDialog As New OpenFileDialog()

        Try
            MyFileDialog.Filter = "pdf file|*.pdf"
            If MyFileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                'PdfDocument1.FilePath = fd.FileName
                Dim filebyte As Byte() = Nothing


                filebyte = System.IO.File.ReadAllBytes(MyFileDialog.FileName)
                'SQLCmd = New SqlCommand("Insert into pdftbl ( pdffld ) Values(@pdf)", SQLCon)
                SQLCmd = New SqlCommand
                SQLCmd.Connection = SQLCon

                SQLCmd.CommandText = "UPDATE " + TableName + _
                                     " SET " + PDFColumnName + " = @pPDFColumn" +
                                     " WHERE " + IDColumn + " = @pID"
                'cmd.Parameters.Add("@filepath", SqlType.VarChar).Value = txtfilepath.Text
                SQLCmd.Parameters.Add("@pPDFColumn", SqlDbType.Binary).Value = filebyte
                SQLCmd.Parameters.Add("@pID", SqlDbType.Int).Value = ID_Value
                SQLCmd.Connection.Open()
                RowsEffected = SQLCmd.ExecuteNonQuery()
                SQLCmd.Connection.Close()
                Interaction.MsgBox("File saved into database", MsgBoxStyle.Information)

            End If
            Return RowsEffected
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Exclamation)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.NonQueryError
        End Try


    End Function

    ''' <summary>
    ''' This Function will write a file and its extention to an existing Database record, the user is prompted
    ''' by a File Dialog box if no path and File name is supplied. Returns the number of Records effected.
    ''' which should be 1.
    ''' </summary>
    ''' <param name="TableName">Table Name</param>
    ''' <param name="FileColumn">Columnname to Write the file to</param>
    ''' <param name="FileExtColumn">Column to write the file extention to</param>
    ''' <param name="IDColumn">Unique Key Column (typically Primary Key)</param>
    ''' <param name="ID_Value">Unique Key Value to look for in ID Column </param>
    ''' <param name="PassedFilePath">Optional The File Path and Name</param>
    ''' <returns>The Number of Records effected (Should be 1 on success), else SQLErrors.NonQueryError on error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function Update_File_InDatabase(TableName As String, FileColumn As String, FileExtColumn As String, IDColumn As String, ID_Value As Integer, Optional PassedFilePath As String = Nothing) As Integer
        Try
            Dim MyFileDialog As New OpenFileDialog()
            Dim InsertFile As Boolean = False
            'Prompt user for File if none supplied...
            If PassedFilePath Is Nothing Then
                MyFileDialog.Filter = "Common File Types|*.pdf;*.doc;*.docx;*.txt;*.xlsx;*.xlsx;*.xml;*.html;*.htm;*.rtf" + _
                                  "|PDF|*.pdf|WORD|*.doc;*.docx|TEXT|*.txt|EXCEL|*.xlsx;*.xlsx|XML|*.xml|WEB|*.html;*.htm" + _
                                  "|RICH TEXT|*.rtf|ALL FILES|*.*"
                If MyFileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    InsertFile = True
                End If
            Else
                Try
                    MyFileDialog.FileName = PassedFilePath
                    InsertFile = True
                Catch
                    InsertFile = False
                End Try

            End If


            If InsertFile = True Then
                Dim RowsEffected As Integer = 0
                Dim FileByteArray As Byte() = Nothing
                Dim FileExtention As String = Path.GetExtension(MyFileDialog.FileName)

                FileByteArray = System.IO.File.ReadAllBytes(MyFileDialog.FileName)
                'SQLCmd = New SqlCommand("Insert into pdftbl ( pdffld ) Values(@pdf)", SQLCon)
                SQLCmd = New SqlCommand
                SQLCmd.Connection = SQLCon

                SQLCmd.CommandText = "UPDATE " + TableName + _
                                     " SET " + FileColumn + " = @pFile," + _
                                      " " + FileExtColumn + " = @pFileExt" + _
                                     " WHERE " + IDColumn + " = @pID"
                'cmd.Parameters.Add("@filepath", SqlType.VarChar).Value = txtfilepath.Text
                SQLCmd.Parameters.Add("@pFile", SqlDbType.Binary).Value = FileByteArray
                SQLCmd.Parameters.Add("@pFileExt", SqlDbType.NVarChar).Value = FileExtention
                SQLCmd.Parameters.Add("@pID", SqlDbType.Int).Value = ID_Value
                SQLCmd.Connection.Open()
                RowsEffected = SQLCmd.ExecuteNonQuery()
                SQLCmd.Connection.Close()
                Interaction.MsgBox("File saved into database", MsgBoxStyle.Information)
                Return RowsEffected
            Else
                Interaction.MsgBox("File NOT saved into database!", MsgBoxStyle.Information)
                Return SQLErrors.FilePathInvalid
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Exclamation)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.NonQueryError
        End Try


    End Function

    ''' <summary>
    ''' This Function will write a file and its extention to an new Database record, the user is prompted
    ''' by a File Dialog box if no path and File name is supplied. Returns the number of Records effected.
    ''' which should be 1.
    ''' </summary>
    ''' <param name="TableName">Database Table Name</param>
    ''' <param name="FileColumn">Column to insert File Data</param>
    ''' <param name="FileExtColumn">Column to insert file extention</param>
    ''' <param name="PassedFileName">Optional Filename and path to insert into database</param>
    ''' <returns>Number of rows effected (Should be 1 on success), Otherwise SQLERRORS....</returns>
    ''' <remarks></remarks>
    Public Function Insert_File_InDatabase(TableName As String, FileColumn As String, FileExtColumn As String, Optional PassedFileName As String = Nothing)
        Try
            Dim MyFileDialog As New OpenFileDialog()
            Dim InsertFile As Boolean = False
            If PassedFileName Is Nothing Then
                MyFileDialog.Filter = "Common File Types|*.pdf;*.doc;*.docx;*.txt;*.xlsx;*.xlsx;*.xml;*.html;*.htm;*.rtf" + _
                                  "|PDF|*.pdf|WORD|*.doc;*.docx|TEXT|*.txt|EXCEL|*.xlsx;*.xlsx|XML|*.xml|WEB|*.html;*.htm" + _
                                  "|RICH TEXT|*.rtf|ALL FILES|*.*"
                If MyFileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    InsertFile = True
                End If
            Else
                Try
                    MyFileDialog.FileName = PassedFileName
                    InsertFile = True
                Catch
                    InsertFile = False
                End Try

            End If


            If InsertFile = True Then

                Dim FileByteArray As Byte() = Nothing
                Dim FileExtention As String = Path.GetExtension(MyFileDialog.FileName)
                Dim RowsEffected As Integer = 0
                FileByteArray = System.IO.File.ReadAllBytes(MyFileDialog.FileName)
                'SQLCmd = New SqlCommand("Insert into pdftbl ( pdffld ) Values(@pdf)", SQLCon)
                SQLCmd = New SqlCommand
                SQLCmd.Connection = SQLCon

                SQLCmd.CommandText = "INSERT INTO " + TableName + _
                                      "(" + FileColumn + "," + FileExtColumn + ") VALUES (@pFile, @pFileExt)"
                '" SET " + FileColumn + " = @pFile," + _
                ' " " + FileExtColumn + " = @pFileExt"

                'cmd.Parameters.Add("@filepath", SqlType.VarChar).Value = txtfilepath.Text
                SQLCmd.Parameters.Add("@pFile", SqlDbType.Binary).Value = FileByteArray
                SQLCmd.Parameters.Add("@pFileExt", SqlDbType.NVarChar).Value = FileExtention
                SQLCmd.Connection.Open()
                RowsEffected = SQLCmd.ExecuteNonQuery()
                SQLCmd.Connection.Close()
                Interaction.MsgBox("File saved into database", MsgBoxStyle.Information)
                Return RowsEffected
            Else
                Interaction.MsgBox("File NOT saved into database!", MsgBoxStyle.Information)
                Return SQLErrors.FilePathInvalid
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Exclamation)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.NonQueryError
        End Try


    End Function
    ' load pdf file

    ''' <summary>
    ''' This Function Retireves a PDF stored in a database and writes it to a tempory file.  The File
    ''' Is Path and name is then returned as a string
    ''' </summary>
    ''' <param name="TableName">Database Table Name</param>
    ''' <param name="PDFColumnName">Column holding the PDF Data</param>
    ''' <param name="IDColumn">Unique Column (Typically Primary Key)</param>
    ''' <param name="ID_Value">Unqiue Value (Typicall primary key value)</param>
    ''' <returns>Path and name of temporary file with PDF data</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function Get_PDF_FromDB(TableName As String, PDFColumnName As String, IDColumn As String, ID_Value As Integer) As String
        Dim strsql As String = Nothing

        Try
            'strsql = "select pdffld from  pdftbl "
            strsql = "SELECT " + PDFColumnName + _
                    " FROM [" + TableName + "]" + _
                    " WHERE [" + IDColumn + "] = " + ID_Value.ToString
            SQL_DataAdapter = New SqlDataAdapter(strsql, SQLCon)
            SQL_DataAdapter.Fill(SQL_Dataset, "tbl")

            'Get image data from gridview column.
            Dim pdfData As Byte() = CType(SQL_Dataset.Tables("tbl").Rows(0)(0), Byte())

            'Initialize pdf  variable

            'Read pdf  data into a memory stream
            Using ms As New MemoryStream(pdfData, 0, pdfData.Length)
                Dim MyTempFileNameAndPath As String = Path.GetTempFileName
                Using MyFileStream As FileStream = File.OpenWrite(MyTempFileNameAndPath)

                    TempFilesToCleanUp.Add(MyTempFileNameAndPath)

                    ms.WriteTo(MyFileStream)

                    Return MyTempFileNameAndPath + ";pdf"


                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.DatabaseFileRetrievalError.ToString
        End Try
    End Function

    ''' <summary>
    ''' This function will retrieve a file and File ext stored in a sql database and write it to a temporary folder/file
    ''' </summary>
    ''' <param name="TableName">Table with the file</param>
    ''' <param name="FileColumnName">The column that holds the File data</param>
    ''' <param name="FileExtentionColumn">the Column that holds the extention</param>
    ''' <param name="IDColumn">Primary key of the Table</param>
    ''' <param name="ID_Value">Primary Key value of the table</param>
    ''' <returns>Tempory file name and path delimited with the file extention</returns>
    ''' <remarks>Frank Boudrau 2016</remarks>
    Public Function Get_File_FromDB(TableName As String, FileColumnName As String, FileExtentionColumn As String, IDColumn As String, ID_Value As Integer) As String
        Dim strsql As String = Nothing

        Try
            'Should be only one result
            If GetTableData(TableName, IDColumn, ID_Value) = 1 Then
                'Parse Data
                Dim FileData As Byte() = CType(SQL_Dataset.Tables(0).Rows(0)(FileColumnName), Byte())
                Dim FileExtention As String = ""
                Try
                    FileExtention = SQL_Dataset.Tables(0).Rows(0)(FileExtentionColumn).ToString()
                Catch
                    FileExtention = "" ' assume none
                End Try

                'Read FIle  data into a memory stream
                Using ms As New MemoryStream(FileData, 0, FileData.Length)

                    Dim MyTempFileNameAndPath As String
                    If FileExtention <> "" Then
                        MyTempFileNameAndPath = Path.GetTempFileName.Replace("tmp", FileExtention)
                    Else
                        MyTempFileNameAndPath = Path.GetTempFileName
                    End If

                    'store for cleanup
                    TempFilesToCleanUp.Add(MyTempFileNameAndPath)

                    Using MyFileStream As FileStream = File.OpenWrite(MyTempFileNameAndPath)

                        ms.WriteTo(MyFileStream)

                        'Return the path of the file and the file extention delimited with a colon
                        Return MyTempFileNameAndPath + ";" + FileExtention

                    End Using
                End Using
            Else
                MsgBox("Table retrieval error, aborting")
                Return SQLErrors.QueryError.ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.DatabaseFileRetrievalError.ToString
        End Try
    End Function


    ''' <summary>
    ''' This Function cleans up tempory files and deletes.  
    ''' </summary>
    ''' <returns>The number of files cleaned up</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function CleanTempFiles() As Integer
        Dim NumFilesCleaned As Integer = 0
        For Each item As String In TempFilesToCleanUp
            Try
                System.IO.File.Delete(item)
                NumFilesCleaned = NumFilesCleaned + 1
            Catch ex As Exception

            End Try
        Next
        Return NumFilesCleaned
    End Function
#End Region '"Write and Read Files"

    ''' <summary>
    ''' This Region has the orginal OLEDB helper functions written to assist with the 
    ''' Failure report database. WHere possible the functions have been overloaded 
    ''' for SQL Connection types as well as OLEDB connections
    ''' </summary>
    ''' <remarks>Frank Boudreau 2017</remarks>
#Region "SQL and OLEDB Helper Functions"


    Public Class cName
        Private m_Name As String

        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(value As String)
                m_Name = value
            End Set
        End Property


        Public Sub New(strName As String)
            Name = strName
        End Sub

        Public Overrides Function ToString() As String
            Return m_Name
        End Function
    End Class

    Public Class cValue
        Private m_Value As String

        Public Property Value() As String
            Get
                Return m_Value
            End Get
            Set(value As String)
                m_Value = value
            End Set
        End Property


        Public Sub New(strValue As String)
            Value = strValue
        End Sub

        Public Overrides Function ToString() As String
            Return m_Value
        End Function
    End Class

    Public Class cColumn
        Private m_ColumnName As String

        ''' <summary>
        ''' Column name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Name As String
            Get
                Return m_ColumnName
            End Get
            Set(value As String)
                m_ColumnName = value
            End Set
        End Property

        Private m_Columnvalue As Object

        ''' <summary>
        ''' This Data otr object to be stored in the cell
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Value As Object
            Get
                Return m_Columnvalue
            End Get
            Set(value As Object)
                m_Columnvalue = value
            End Set
        End Property

        Private m_ColumnDataType As Object
        ''' <summary>
        ''' This is the Data type that the Column will accept
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DataType As Object
            Get
                Return m_ColumnDataType
            End Get
            Set(value As Object)
                m_ColumnDataType = value
            End Set
        End Property

        Private m_PopulateField As Boolean
        ''' <summary>
        ''' Set to true if the Column Field should be Updated, else set to False
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PopulateCell As Boolean
            Get
                Return m_PopulateField
            End Get
            Set(value As Boolean)
                m_PopulateField = value
            End Set
        End Property

        Public Sub New(colName As String, colValue As Object, colDataType As Object, colPopulateField As Boolean)
            If colValue Is Nothing And colName Is Nothing Then
                Value = DBNull.Value
                Name = New String("")
            ElseIf colValue Is Nothing And colName IsNot Nothing Then
                Value = DBNull.Value
                Name = colName
            ElseIf colValue IsNot Nothing And colName Is Nothing Then
                Value = colValue
                Name = New String("")
            Else
                Name = colName
                Value = colValue
            End If
            DataType = colDataType
            PopulateCell = colPopulateField
        End Sub

        Public Overrides Function ToString() As String
            Return m_ColumnName
        End Function
    End Class

    Public Class cTable
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
    End Class

    ''' <summary>
    ''' Class to Encapsulate and Track Control Data Bindings.
    ''' </summary>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Class cDataBindingTracker
        ''' <summary>
        ''' Control that will be Bound
        ''' </summary>
        ''' <remarks>Frank Boudreau 2016</remarks>
        Public Control As Control
        ''' <summary>
        ''' Control Porperty to Bind the Data to.  (i.e. Text, CheckState etc...)
        ''' </summary>
        ''' <remarks></remarks>
        Public ControlProperty As String
        ''' <summary>
        ''' This is the Name of the column that in the Datasource that has the data to bind to the control
        ''' </summary>
        ''' <remarks></remarks>
        Public ColumnName As String
        ''' <summary>
        ''' This is the Datatable that hosts the column
        ''' </summary>
        ''' <remarks></remarks>
        Public DataTable As DataTable
        ''' <summary>
        ''' This is the Visual Studio Binging source binding source object to facilitate 
        ''' </summary>
        ''' <remarks></remarks>
        Public BindingSource As BindingSource
        ''' <summary>
        ''' This is the binding that is being tracked
        ''' </summary>
        ''' <remarks></remarks>
        Public Binding As Binding

        Public Sub New(ByRef MyControl As Control, ByVal MyControlPoperty As String, ByVal MyColumnName As String, ByRef MyDatatable As DataTable, ByRef MyBinding As Binding)
            Control = (MyControl)
            ControlProperty = MyControlPoperty
            ColumnName = MyColumnName
            DataTable = MyDatatable
            MyBinding = New Binding(ControlProperty, MyDatatable, ColumnName)
            Binding = MyBinding
        End Sub

        Public Sub New(ByRef MyControl As Control, ByVal MyControlPoperty As String, ByVal MyColumnName As String, ByRef MyBindingSource As BindingSource, ByRef MyBinding As Binding)
            Control = (MyControl)
            ControlProperty = MyControlPoperty
            ColumnName = MyColumnName
            BindingSource = MyBindingSource
            MyBinding = New Binding(ControlProperty, BindingSource, ColumnName)
            Binding = MyBinding
        End Sub
    End Class

    ''' <summary>
    ''' Function to build Connection string or if parameters are ommitted to return connection string from My.Settings
    ''' </summary>
    ''' <param name="MyConnectionNameAndPath">Name of the connectionString (path and Name) to retrieve</param>
    ''' <param name="strDBPassword">Database password</param>
    ''' <param name="bPersistentSecurityState">Is Persistent Security State</param>
    ''' <returns>Formatted Connection String</returns>
    ''' <remarks>Frank Boudreau 3/1/2016</remarks>
    Public Shared Function GetOLEDBConnectionString(Optional ByVal MyConnectionNameAndPath As String = "", Optional ByVal strDBPassword As String = "", Optional ByVal bPersistentSecurityState As Boolean = True) As String
        'variable to hold our connection string for returning it
        Dim strReturn As New String("")
        'check to see if the user provided a connection string name
        'this is for if your application has more than one connection string

        'If a database path and name is provided, build the connection string
        If (Not String.IsNullOrEmpty(MyConnectionNameAndPath)) Then
            Dim strPersistSecurityState As String
            If bPersistentSecurityState = True Then
                strPersistSecurityState = "True"
            Else
                strPersistSecurityState = "False"
            End If
            'a connection string name was provided
            'get the connection string by the name provided
            ' strReturn = ConfigurationManager.ConnectionStrings(conName).ConnectionString
            strReturn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + MyConnectionNameAndPath + ";Persist Security Info=" + strPersistSecurityState + ";Jet OLEDB:Database Password=" + strDBPassword
        Else
            'no connection string name was provided
            'get the default connection string
            strReturn = My.Settings.FailureReportDataBaseFullConnectionString
        End If
        'return the connection string to the calling method
        Return strReturn
    End Function


    ''' <summary>
    ''' This function creates a Dialog to get the path and name of an Access Database
    ''' </summary>
    ''' <returns>Returns the Path and database name if a Database is selected otherwise it returns "NOTHING"</returns>
    ''' <remarks>Frank Boudreau 3.1.2016</remarks>
    Public Function str_Get_AccessDB_PathAndNameDialog(Optional ByVal bVerbose = False) As String

        Dim strDataBasePath As String = Nothing
        Try
            'Dim result As New System.Text.StringBuilder

            Dim OpenDatabaseDialog As New OpenFileDialog

            OpenDatabaseDialog.CheckPathExists = True
            If My.Settings.FailureReportDataBaseDataSource <> "" Then
                OpenDatabaseDialog.InitialDirectory = My.Settings.FailureReportDataBaseDataSource 'My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\RadianLogger\"
            Else
                OpenDatabaseDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End If

            'set defaults
            OpenDatabaseDialog.DefaultExt = ".accdb"
            OpenDatabaseDialog.Filter = "Access 2007 (*.accdb)|*.accdb|Access 2003 (*.mdb)|*.mdb|Access 2007 Runtime(*.accdr)|*.accdr"
            OpenDatabaseDialog.FilterIndex = 0
            OpenDatabaseDialog.AddExtension = True
            OpenDatabaseDialog.SupportMultiDottedExtensions = True
            OpenDatabaseDialog.ValidateNames = True
            OpenDatabaseDialog.ShowDialog()




            strDataBasePath = OpenDatabaseDialog.FileName


            Dim i As Integer = 1
        Catch ex As Exception
            If bVerbose = True Then
                MsgBox("Error Selecting Database " + vbCrLf + ex.ToString)
            End If
            Return Nothing
        End Try

        Return strDataBasePath


    End Function

    ''' <summary>
    ''' Function to query data from database and return in a datatable
    ''' </summary>
    ''' <param name="MyDataBaseConnection">OLE database connection</param>
    ''' <param name="MyRecordIndex">Index of First Record to Retrieve</param>
    ''' <param name="MaxNumberOfRecordsToRetrieve">Max Number of record to to retireve Starting at MyRecordIndex </param>
    ''' <param name="strDataTableName">Datatable Name to pass to Query</param>
    ''' <param name="MyFilter">Optional Query Filter </param>
    ''' <returns>Datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetData(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByRef MyRecordIndex As Integer, ByRef MaxNumberOfRecordsToRetrieve As Integer, ByVal strDataTableName As String, Optional ByVal MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)

        'Use the data-adapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyRecordIndex, MaxNumberOfRecordsToRetrieve, MyDataTable)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function



    ''' <summary>
    ''' This is function retrieves all the data from a table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">OLE DB connectiion</param>
    ''' <param name="strDataTableName">Table name in database</param>
    ''' <param name="MyFilter">Optional Filter to include with Query</param>
    ''' <returns>A datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetData(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try

            MyOleDBDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function


    ''' <summary>
    ''' This is function retrieves all the data from a table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">SQL DB connectiion</param>
    ''' <param name="strDataTableName">Table name in database</param>
    ''' <param name="MyFilter">Optional Filter to include with Query</param>
    ''' <returns>A datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetData(ByRef MyDataBaseConnection As SqlConnection, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable

        'The sqlCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)

        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"

        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function



    ''' <summary>
    ''' This is function retrieves all the data from a table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">OLE DB connectiion</param>
    ''' <param name="strDataTableName">Table name in database</param>
    ''' <param name="MyFilter">Optional Filter to include with Query</param>
    ''' <returns>A datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetData(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByRef MyOleDBDataAdapter As OleDbDataAdapter, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        MyOleDBDataAdapter = New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function


    ''' <summary>
    ''' This is function retrieves all the data from a table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">SQL DB connection</param>
    ''' <param name="MySQLDataAdapter"> SQL Data Adaptor</param>
    ''' <param name="strDataTableName">Table name in database</param>
    ''' <param name="MyFilter">Optional Filter to include with Query</param>
    ''' <returns>A datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetData(ByRef MyDataBaseConnection As SqlConnection, ByRef MySQLDataAdapter As SqlDataAdapter, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        MySQLDataAdapter = New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MySQLDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function




    ''' <summary>
    ''' This is function retrieves batches of data of upto MaxRecords + 1 long.  
    ''' </summary>
    ''' <param name="MyDataBaseConnection">SQL DB connection</param>
    ''' <param name="MySQLDataAdapter"> SQL Data Adaptor</param>
    ''' <param name="strDataTableName">Table name in database</param>
    ''' <param name="IdColumn">This is the Key Column to for sorting the batched data...</param>
    ''' <param name="ReferenceRecordID">This is the Reference Record that must be returned.  The Query will try to return this record Centered on the 
    ''' the Maximum Records returned if possible...</param>
    ''' <param name="MaxRecords">Maximum Number of Records to Return</param>
    ''' <param name="MyFilter">Optional Filter to include with Query</param>
    ''' <returns>A datatable with the results of the Query</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetBatchedData(ByRef MyDataBaseConnection As SqlConnection, ByRef MySQLDataAdapter As SqlDataAdapter, _
                                      ByVal strDataTableName As String, _
                                      ByVal IdColumn As String, _
                                      ByVal ReferenceRecordID As Integer, _
                                      ByVal MaxRecords As Integer, _
                                      Optional ByVal MyFilter As String = "") As DataTable

        Dim LowerBound As Integer = ReferenceRecordID - MaxRecords / 2

        If LowerBound < 0 Then
            LowerBound = 0
        End If

        'The sqlCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT TOP " + MaxRecords.ToString + " * FROM [" + strDataTableName + "]" + " WHERE " + IdColumn + " > " + LowerBound.ToString '+ MyFilter"

        'Add Filter if it exists
        If MyFilter.Trim <> "" Then
            MyQuery = MyQuery + " AND " + MyFilter
        End If

        'Add order by command

        MyQuery = MyQuery + " ORDER BY " + IdColumn + " ASC"

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        MySQLDataAdapter = New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MySQLDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function





    ''' <summary>
    ''' This function retrieves table data from an OLE database returns the results in a datatable
    ''' </summary>
    ''' <param name="MyDataBaseConnection">OLE DB Connection</param>
    ''' <param name="MyQuery">User SQL Query</param>
    ''' <param name="strDataTableName">Name of the Table to Query in the Database</param>
    ''' <returns>A Data table with the query results</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetDataCustomQuery(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal MyQuery As String, ByVal strDataTableName As String) As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            'MyOleDBDataAdapter.Fill(MyRecordIndexLower, MyRecordIndexUpper, MyDataTable)
            MyOleDBDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function

    ''' <summary>
    ''' This function retrieves table data from an OLE database returns the results in a datatable
    ''' </summary>
    ''' <param name="MyDataBaseConnection">SQL Connection</param>
    ''' <param name="MyQuery">User SQL Query</param>
    ''' <param name="strDataTableName">Name of the Table to Query in the Database</param>
    ''' <returns>A Data table with the query results</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Function GetDataCustomQuery(ByRef MyDataBaseConnection As SqlConnection, ByVal MyQuery As String, ByVal strDataTableName As String) As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            'MyOleDBDataAdapter.Fill(MyRecordIndexLower, MyRecordIndexUpper, MyDataTable)
            MyDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function

    ''' <summary>
    ''' This function returns the Maximum value in a Column in Table in a database
    ''' </summary>
    ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name</param>
    ''' <returns>Returns Maximum Value as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetMaxValueFromDatabaseColumn(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT MAX " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMaxValueFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyDataTable)
            GetMaxValueFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetMaxValueFromDatabaseColumn

    End Function

    ''' <summary>
    ''' This function returns the Maximum value in a Column in Table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">Connection to SQl Database</param>
    ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name</param>
    ''' <returns>Returns Maximum Value as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetMaxValueFromDatabaseColumn(ByRef MyDataBaseConnection As SqlConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT MAX " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMaxValueFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyDataAdapter.Fill(MyDataTable)
            GetMaxValueFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetMaxValueFromDatabaseColumn

    End Function


    ''' <summary>
    ''' This function returns the minimum value in a Column in Table in a database
    ''' </summary>
   ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name</param>
    ''' <returns>Returns Minimun Value as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetMinValueFromDatabaseColumn(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT MIN " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMinValueFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyDataTable)
            GetMinValueFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetMinValueFromDatabaseColumn

    End Function

    ''' <summary>
    ''' This function returns the minimum value in a Column in Table in a database
    ''' </summary>
    ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name</param>
    ''' <returns>Returns Minimun Value as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetMinValueFromDatabaseColumn(ByRef MyDataBaseConnection As SqlConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT MIN " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetMinValueFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyDataAdapter.Fill(MyDataTable)
            GetMinValueFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetMinValueFromDatabaseColumn

    End Function


    ''' <summary>
    ''' This function returns the number of records in a Column in Table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">OLD DB connection</param>
    ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name</param>
    ''' <returns>Returns Count as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetRecordCountFromDatabaseColumn(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT COUNT " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetRecordCountFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyDataTable)
            GetRecordCountFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetRecordCountFromDatabaseColumn

    End Function

    ''' <summary>
    ''' This function returns the number of records in a Column in Table in a database
    ''' </summary>
    ''' <param name="MyDataBaseConnection">SQL connection</param>
    ''' <param name="strColumnName">Column Name</param>
    ''' <param name="strDataTableName">Datatable name (Spaces are allowed)</param>
    ''' <returns>Returns Count as a string</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetRecordCountFromDatabaseColumn(ByRef MyDataBaseConnection As SqlConnection, ByVal strColumnName As String, ByRef strDataTableName As String) As String
        'SELECT MAX ([New ID]) AS [New ID] FROM [Failure Report]
        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it
        Dim MyQuery As String = "SELECT COUNT " + "([" + strColumnName + "])" + "AS" + "[" + strColumnName + "]" + "FROM" + "[" + strDataTableName + "]"
        GetRecordCountFromDatabaseColumn = ""
        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyDataAdapter.Fill(MyDataTable)
            GetRecordCountFromDatabaseColumn = MyDataTable.Rows(0).Item(strColumnName)

        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try
        Return GetRecordCountFromDatabaseColumn

    End Function

    ''' <summary>
    ''' This function populates a table of unique values in a column in a table
    ''' </summary>
    ''' <param name="MyDataBaseConnection">The connection to the Database</param>
    ''' <param name="strMyColumn">Column name with data</param>
    ''' <param name="strDataTableName">Table name to query</param>
    ''' <param name="MyFilter">Optional Filter</param>
    ''' <returns>Table with query values</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetDistinctData(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByRef strMyColumn As String, ByVal strDataTableName As String, Optional ByVal MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        Dim MyQuery As String = "SELECT DISTINCT [" + strMyColumn + "] FROM [" + strDataTableName + "]" + MyFilter

        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyOleDBDataAdapter As New OleDb.OleDbDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyOleDBDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

       


        Return MyDataTable
    End Function

    ''' <summary>
    ''' This function populates a table of unique values in a column in a table
    ''' </summary>
    ''' <param name="MyDataBaseConnection">The connection to the Database</param>
    ''' <param name="strMyColumn">Column name with data</param>
    ''' <param name="strDataTableName">Table name to query</param>
    ''' <param name="MyFilter">Optional Filter</param>
    ''' <returns>Table with query values</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function GetDistinctData(ByRef MyDataBaseConnection As SqlConnection, ByRef strMyColumn As String, ByVal strDataTableName As String, Optional ByVal MyFilter As String = "") As DataTable

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        Dim MyQuery As String = "SELECT DISTINCT [" + strMyColumn + "] FROM [" + strDataTableName + "] " + MyFilter

        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)
        'Use the dataadapter object to use the results from the query and bash them in a "DataTable"
        'A datatable is an in memory table that is comparable with the adodb.recordset object you've
        'previously used in vb6 or vba. 

        Dim MyDataAdapter As New SqlDataAdapter(mySQL_Command)

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try
            MyDataAdapter.Fill(MyDataTable)
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function


    ''' <summary>
    ''' Function to populate a combobox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyComboBox">Combobox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Sub PopulateComboBox(ByRef MyDBConnection As OleDb.OleDbConnection, ByRef MyComboBox As ComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyComboBox.Text
        MyDatatable = GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)
        MyComboBox.DataSource = MyDatatable
        MyComboBox.DisplayMember = MycolumnName
        MyComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ''' <summary>
    ''' Function to populate a combobox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyComboBox">Combobox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Sub PopulateComboBox(ByRef MyDBConnection As SqlConnection, ByRef MyComboBox As ComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyComboBox.Text
        MyDatatable = GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)
        MyComboBox.DataSource = MyDatatable
        MyComboBox.DisplayMember = MycolumnName
        MyComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ''' <summary>
    ''' Function to populate a combobox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyComboBox">Combobox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Sub PopulateReadOnlyComboBox(ByRef MyDBConnection As OleDb.OleDbConnection, ByRef MyComboBox As ReadOnlyComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyComboBox.Text
        MyDatatable = GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)
        MyComboBox.DataSource = MyDatatable
        MyComboBox.DisplayMember = MycolumnName
        MyComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ''' <summary>
    ''' Function to populate a combobox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyComboBox">Combobox to populate (Read Only Custom Capable Combo-box)</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Sub PopulateReadOnlyComboBox(ByRef MyDBConnection As SqlConnection, ByRef MyComboBox As ReadOnlyComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyComboBox.Text
        MyDatatable = GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)
        MyComboBox.DataSource = MyDatatable
        MyComboBox.DisplayMember = MycolumnName
        MyComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ' ''' <summary>
    ' ''' Returns a BindingSource, which is used with, for example, a DataGridView control
    ' ''' </summary>
    ' ''' <param name="cmd">"pre-Loaded" command, ready to be executed</param>
    ' ''' <returns>BindingSource</returns>
    ' ''' <remarks>Use this function to ease populating controls that use a BindingSource
    ' ''' http://www.dreamincode.net/forums/topic/32392-sql-basics-in-vbnet/ </remarks>
    'Public Shared Function GetBindingSource(ByVal cmd As OleDb.OleDbCommand) As BindingSource
    '    'declare our binding source
    '    Dim oBindingSource As New BindingSource()
    '    ' Create a new data adapter based on the specified query.
    '    Dim daGet As New OleDb.OleDbDataAdapter(cmd)
    '    ' Populate a new data table and bind it to the BindingSource.
    '    Dim dtGet As New DataTable()
    '    'set the timeout of the SqlCommandObject
    '    cmd.CommandTimeout = 240
    '    dtGet.Locale = System.Globalization.CultureInfo.InvariantCulture
    '    Try
    '        'fill the DataTable with the SqlDataAdapter
    '        daGet.Fill(dtGet)
    '    Catch ex As Exception
    '        'check for errors
    '        MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error receiving data")
    '        Return Nothing
    '    End Try
    '    'set the DataSource for the BindingSource to the DataTable:
    '    oBindingSource.DataSource = dtGet

    '    'return the BindingSource to the calling method or control

    '    Return oBindingSource
    'End Function

    ' ''' <summary>
    ' ''' Method for handling the ConnectionState of
    ' ''' the connection object passed to it
    ' ''' </summary>
    ' ''' <param name="conn">The SqlConnection Object</param>
    ' ''' <remarks>http://www.dreamincode.net/forums/topic/32392-sql-basics-in-vbnet/</remarks>
    'Public Shared Sub HandleConnection(ByVal conn As OleDb.OleDbConnection)
    '    With conn
    '        'do a switch on the state of the connection
    '        Select Case .State
    '            Case ConnectionState.Open
    '                'the connection is open
    '                'Close the connection
    '                .Close()
    '                '.Open()
    '                Exit Select
    '            Case ConnectionState.Closed
    '                'connection is Closed
    '                'open the connection
    '                .Open()
    '                Exit Select
    '            Case Else
    '                'if it is any other state try closing and reopening
    '                .Close()
    '                .Open()
    '                Exit Select
    '        End Select
    '    End With
    'End Sub

    ''' <summary>
    ''' The Fuction gets the column schema information for the table and returns it as a DataTable
    ''' </summary>
    ''' <param name="MyDataBaseConnection">This is the Connectiion OLEDBConnection</param>
    ''' <param name="strDataTableName">This is name of the table in the database</param>
    ''' <param name="MyFilter">Optional Filter</param>
    ''' <returns>DataTable with Column Schema information for the Database Table Queried</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Shared Function GetColumnNames_DataType_And_Size(ByRef MyDataBaseConnection As OleDb.OleDbConnection, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable


        If MyDataBaseConnection.State = ConnectionState.Closed Then
            MyDataBaseConnection.Open()
        End If

        'First Build the Query
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New OleDb.OleDbCommand(MyQuery, MyDataBaseConnection)


        'Use the Data Reader object to read the schema
        Dim mySql_Reader As OleDb.OleDbDataReader

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try

            'The .KeyInfo forces the results to read flags like "IsUnique", "IsAutoIncrementing"
            mySql_Reader = mySQL_Command.ExecuteReader(CommandBehavior.KeyInfo)
            'Now put the results in the table.
            MyDataTable = mySql_Reader.GetSchemaTable
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function

    ''' <summary>
    ''' The Fuction gets the column schema information for the table and returns it as a DataTable
    ''' </summary>
    ''' <param name="MyDataBaseConnection">This is the Connectiion OLEDBConnection</param>
    ''' <param name="strDataTableName">This is name of the table in the database</param>
    ''' <param name="MyFilter">Optional Filter</param>
    ''' <returns>DataTable with Column Schema information for the Database Table Queried</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Overloads Shared Function GetColumnNames_DataType_And_Size(ByRef MyDataBaseConnection As SqlConnection, ByVal strDataTableName As String, Optional MyFilter As String = "") As DataTable


        If MyDataBaseConnection.State = ConnectionState.Closed Then
            MyDataBaseConnection.Open()
        End If

        'First Build the Query
        Dim MyQuery As String = "SELECT * FROM [" + strDataTableName + "]" + MyFilter

        'The oledb.oleDBCommand object allows you to shoot some query over a connection and -do-
        'something with it

        'Dim mySQL_Command As New OleDb.OleDbCommand("SELECT * FROM [" + strDataTableName + "]", MyDataBaseConnection)
        Dim mySQL_Command As New SqlCommand(MyQuery, MyDataBaseConnection)


        'Use the Data Reader object to read the schema
        Dim mySql_Reader As SqlDataReader

        'Create a datatable to house the results from the query
        Dim MyDataTable As New DataTable(strDataTableName)

        'Bash the query results in the datatable
        Try

            'The .KeyInfo forces the results to read flags like "IsUnique", "IsAutoIncrementing"
            mySql_Reader = mySQL_Command.ExecuteReader(CommandBehavior.KeyInfo)
            'Now put the results in the table.
            MyDataTable = mySql_Reader.GetSchemaTable
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
        Catch ex As Exception
            If MyDataBaseConnection.State = ConnectionState.Open Then
                MyDataBaseConnection.Close()
            End If
            MsgBox(ex.ToString)
        End Try

        Return MyDataTable
    End Function

    ''' <summary>
    ''' Predfined Function to assist inserting a new record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Shared Function InsertBlankRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As OleDb.OleDbConnection) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList Then 'Add All Fields
                ColumnNames = "[" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False Then 'Add All Fields
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "INSERT INTO [" + MyRecord.TableName + "] (" + ColumnNames + ") VALUES (" + ColumnValuesPointers + ")"

            'Create Inline SQL Command 
            Dim cmdOldeDBInsert As New OleDb.OleDbCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdOldeDBInsert.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdOldeDBInsert
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdOldeDBInsert.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception
                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to insert, aborting update...")
            Return False
        End If
    End Function


    ''' <summary>
    ''' Predfined Function to assist inserting a new record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Shared Function InsertBlankRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As SqlConnection) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList Then 'Add All Fields
                ColumnNames = "[" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False Then 'Add All Fields
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "INSERT INTO [" + MyRecord.TableName + "] (" + ColumnNames + ") VALUES (" + ColumnValuesPointers + ")"

            'Create Inline SQL Command 
            Dim cmdSQLDBInsert As New SqlCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdSQLDBInsert.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdSQLDBInsert
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdSQLDBInsert.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception
                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to insert, aborting update...")
            Return False
        End If
    End Function


    ''' <summary>
    ''' Predfined Function to assist inserting a new record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Shared Function InsertNewRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As OleDb.OleDbConnection) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "INSERT INTO [" + MyRecord.TableName + "] (" + ColumnNames + ") VALUES (" + ColumnValuesPointers + ")"

            'Create Inline SQL Command 
            Dim cmdOldeDBInsert As New OleDb.OleDbCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdOldeDBInsert.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdOldeDBInsert
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdOldeDBInsert.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to insert, aborting update...")
            Return False
        End If
    End Function

    ''' <summary>
    ''' Predfined Function to assist inserting a new record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Shared Function InsertNewRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As SqlConnection) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "]"
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "INSERT INTO [" + MyRecord.TableName + "] (" + ColumnNames + ") VALUES (" + ColumnValuesPointers + ")"

            'Create Inline SQL Command 
            Dim cmdSqlDBInsert As New SqlCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdSqlDBInsert.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdSqlDBInsert
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdSqlDBInsert.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to insert, aborting update...")
            Return False
        End If
    End Function


    ''' <summary>
    ''' Predfined Function to assist updating an existing record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <param name="RecordIndex">This identifies which record(s) column is is the Key for the update i.e "New ID"</param>
    ''' <param name="IndexValue">This identifies which record(s) Value in RecordIndex for the update i.e. "800" </param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Overloads Shared Function UpdateExistingRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As OleDb.OleDbConnection, ByRef RecordIndex As String, ByRef IndexValue As Object) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0

        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "UPDATE [" + MyRecord.TableName + "] SET " + ColumnNames + " WHERE [" + RecordIndex + "]=@" + RecordIndex.Replace(" ", "")

            'Create Inline SQL Command 
            Dim cmdOldeDBUpdate As New OleDb.OleDbCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdOldeDBUpdate.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdOldeDBUpdate
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If

                                Case "System.Boolean"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, Boolean.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    If IsDBNull(IndexValue) Or String.IsNullOrEmpty(IndexValue) Then
                        .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), DBNull.Value)
                    Else
                        Dim i16Result As Short
                        Dim bSuccess As Boolean = Int16.TryParse(IndexValue, i16Result)

                        If bSuccess <> 0 Then
                            .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), i16Result)
                        Else
                            .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), DBNull.Value) 'DBNull.Value.ToString)
                        End If

                    End If

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdOldeDBUpdate.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If iSqlStatus = 0 Then 'No Records were effected...
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If

            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to update, aborting update...")
            Return False
        End If
    End Function

    ''' <summary>
    ''' Predfined Function to assist updating an exisiting record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the SQL DB database connection</param>
    ''' <param name="RecordIndex">This identifies which record(s) column is is the Key for the update i.e "New ID"</param>
    ''' <param name="IndexValue">This identifies which record(s) Value in RecordIndex for the update i.e. "800" </param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Overloads Shared Function UpdateExistingRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As SqlConnection, ByRef RecordIndex As String, ByRef IndexValue As Object) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0

        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "UPDATE [" + MyRecord.TableName + "] SET " + ColumnNames + " WHERE [" + RecordIndex + "]=@" + RecordIndex.Replace(" ", "")

            'Create Inline SQL Command 
            Dim cmdOldeDBUpdate As New SqlCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdOldeDBUpdate.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdOldeDBUpdate
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If

                                Case "System.Boolean"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, Boolean.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next

                    If IsDBNull(IndexValue) Or String.IsNullOrEmpty(IndexValue) Then
                        .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), DBNull.Value)
                    Else
                        Dim i16Result As Short
                        Dim bSuccess As Boolean = Int16.TryParse(IndexValue, i16Result)

                        If bSuccess <> 0 Then
                            .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), i16Result)
                        Else
                            .Parameters.AddWithValue("@" + RecordIndex.Replace(" ", ""), DBNull.Value) 'DBNull.Value.ToString)
                        End If

                    End If

                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = No Records updated, Else the number of records updated)
                iSqlStatus = cmdOldeDBUpdate.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to update, aborting update...")
            Return False
        End If
    End Function


    ''' <summary>
    ''' Predfined Function to assist updating an existing record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <param name="RecordFilter">The user must supply the SQL Filter to be used to identify which Parameters to update</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Overloads Shared Function UpdateExistingRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As OleDb.OleDbConnection, ByRef RecordFilter As String) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "UPDATE [" + MyRecord.TableName + "] SET " + ColumnNames + " " + RecordFilter

            'Create Inline SQL Command 
            Dim cmdOldeDBUpdate As New OleDb.OleDbCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdOldeDBUpdate.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdOldeDBUpdate
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next


                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdOldeDBUpdate.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to update, aborting update...")
            Return False
        End If
    End Function

    ''' <summary>
    ''' Predfined Function to assist updating an existing record into a database.
    ''' </summary>
    ''' <param name="MyRecord">Custom class to encapsulate all the record data for the transaction</param>
    ''' <param name="MyDatabaseConnection">the OleDB database connection</param>
    ''' <param name="RecordFilter">The user must supply the SQL Filter to be used to identify which Parameters to update</param>
    ''' <returns>True = Success, False = Failed</returns>
    ''' <remarks>Frank Boudreau 3.3.2016  Thanks to: http://www.dreamincode.net/forums/topic/233014-inserting-null-values-into-sql-using-c%23/ </remarks>
    Public Overloads Shared Function UpdateExistingRecord(ByRef MyRecord As cTable, ByRef MyDatabaseConnection As SqlConnection, ByRef RecordFilter As String) As Boolean


        Dim strSQLCommand As New String("")
        Dim iSqlStatus As Integer
        'String Buffer to hold the Column names to be updated
        Dim ColumnNames As String = ""
        'String Buffer to hold the Column Value Pointer Aliases
        Dim ColumnValuesPointers As String = ""
        Dim i As Integer = 0
        Dim iNumberOfFieldsToUpdate = 0
        'Build Inline SQL statement
        Dim bFirstItemInList = True
        For Each cColumn In MyRecord.Columns
            If bFirstItemInList And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = "[" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                bFirstItemInList = False
                iNumberOfFieldsToUpdate += 1
            ElseIf bFirstItemInList = False And MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                ColumnNames = ColumnNames + ", [" + MyRecord.Columns(i).Name + "] = @" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                'ColumnValues = ColumnValues + ", " + "'" + MyRecord.Columns(i).Value + "'"
                'ColumnValuesPointers = ColumnValuesPointers + ", " + "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                iNumberOfFieldsToUpdate += 1
            End If
            i = i + 1
        Next

        If iNumberOfFieldsToUpdate > 0 Then

            'Inline sql needs to be structured like so
            strSQLCommand = "UPDATE [" + MyRecord.TableName + "] SET " + ColumnNames + " " + RecordFilter

            'Create Inline SQL Command 
            Dim cmdSqlDBUpdate As New SqlCommand(strSQLCommand, MyDatabaseConnection)

            'Clear any parameters
            cmdSqlDBUpdate.Parameters.Clear()
            Try
                'Set the SqlCommand Object Properties
                With cmdSqlDBUpdate
                    'Tell it what to execute
                    'Tell it its Text  
                    .CommandType = CommandType.Text

                    For i = 0 To MyRecord.Columns.Count - 1

                        If MyRecord.Columns(i).PopulateCell = True Then 'only update field if the populate field is set to true
                            Dim MyRecordPointer As String = "@" + MyRecord.Columns(i).Name.ToString.Replace(" ", "_")
                            Select Case MyRecord.Columns(i).DataType.ToString
                                Case "System.String"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                                Case "System.Int16"

                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim i16Result As Short
                                        Dim bSuccess As Boolean = Int16.TryParse(MyRecord.Columns(i).Value, i16Result)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, i16Result)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If
                                Case "System.Int32"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Dim iResult As Integer
                                        Dim bSuccess As Boolean = Int32.TryParse(MyRecord.Columns(i).Value, iResult)

                                        If bSuccess <> 0 Then
                                            .Parameters.AddWithValue(MyRecordPointer, iResult)
                                        Else
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End If
                                    End If

                                Case "System.DateTime"
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, DateTime.Parse(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value)
                                        End Try

                                    End If
                                Case Else 'assume string
                                    If IsDBNull(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    ElseIf String.IsNullOrEmpty(MyRecord.Columns(i).Value) Then
                                        .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                    Else
                                        Try
                                            .Parameters.AddWithValue(MyRecordPointer, CStr(MyRecord.Columns(i).Value))
                                        Catch
                                            .Parameters.AddWithValue(MyRecordPointer, DBNull.Value) 'DBNull.Value.ToString)
                                        End Try
                                    End If

                            End Select
                        End If
                    Next


                    .Connection = MyDatabaseConnection
                    If .Connection.State = ConnectionState.Closed Then
                        .Connection.Open()
                    End If
                End With

                'Now take care of the connection


                'Set the iSqlStatus to the ExecuteNonQuery
                'status of the insert (0 = success, 1 = failed)
                iSqlStatus = cmdSqlDBUpdate.ExecuteNonQuery

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                'Now check the status
                If Not iSqlStatus = 0 Then
                    'DO your failed messaging here
                    Return False
                Else
                    'Do your success work here
                    Return True
                End If
            Catch ex As Exception

                If MyDatabaseConnection.State = ConnectionState.Open Then
                    MyDatabaseConnection.Close()
                End If

                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error")
                Return False
            Finally
                'Now close the connection
                'cmdOldeDBInsert.Connection.Open()
            End Try
        Else

            MsgBox("There were no Field values flagged to update, aborting update...")
            Return False
        End If
    End Function

    Public Shared Function ToggleConnectionState(MyOleDBConnection As OleDbConnection) As Integer

        ToggleConnectionState = MyOleDBConnection.State
        Select Case MyOleDBConnection.State
            Case ConnectionState.Broken
                Try
                    MyOleDBConnection.Close()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to close the OleDB Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
                Try
                    MyOleDBConnection.Open()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to open the OleDB Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case ConnectionState.Closed
                Try
                    MyOleDBConnection.Open()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to open the OleDB Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case ConnectionState.Connecting
                'State not implemented
            Case ConnectionState.Executing
                'State not implemented
            Case ConnectionState.Fetching
                'State not implemented
            Case ConnectionState.Open
                Try
                    MyOleDBConnection.Close()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to close the OleDB Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case Else
        End Select

        ToggleConnectionState = MyOleDBConnection.State

    End Function

    Public Shared Function ToggleConnectionState(MySqlDBConnection As SqlConnection) As Integer

        ToggleConnectionState = MySqlDBConnection.State
        Select Case MySqlDBConnection.State
            Case ConnectionState.Broken
                Try
                    MySqlDBConnection.Close()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to close the SQL Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
                Try
                    MySqlDBConnection.Open()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to open the SQL Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case ConnectionState.Closed
                Try
                    MySqlDBConnection.Open()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to open the SQL Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case ConnectionState.Connecting
                'State not implemented
            Case ConnectionState.Executing
                'State not implemented
            Case ConnectionState.Fetching
                'State not implemented
            Case ConnectionState.Open
                Try
                    MySqlDBConnection.Close()
                Catch ex As Exception
                    MsgBox("An Error was encountered trying to close the SQL Connection." = vbCrLf + ex.ToString, MsgBoxStyle.Exclamation, "Error")
                End Try
            Case Else
        End Select

        ToggleConnectionState = MySqlDBConnection.State

    End Function

    ''' <summary>
    ''' Datasourcetype for binding data to a control
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DataSourceType
        DATA_TABLE
        BINDING_SOURCE
    End Enum


    ''' <summary>
    ''' This function binds data to a control based on criteria passed in the the Bindingrecord. It handles both a datatable or Databinding source for binding
    ''' </summary>
    ''' <param name="BindingRecord">This a list of Data binding tracker. Each control with its datasource per list item</param>
    ''' <param name="SourceType">Defaults to Datatable</param>
    ''' <returns>True = Success; False indicates and Error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function BindControls(ByRef BindingRecord As List(Of cDataBindingTracker), Optional SourceType As DataSourceType = DataSourceType.BINDING_SOURCE) As Boolean

        BindControls = True

        Dim i As Integer = 0
        For Each cDataBindingTracker In BindingRecord
            Select Case BindingRecord(i).Control.GetType

                Case GetType(CheckBox)
                    If BindingRecord(i).ControlProperty = "CheckState" Then
                        Dim MyCheckbox As CheckBox = DirectCast(BindingRecord(i).Control, CheckBox)
                        Select Case SourceType
                            Case DataSourceType.BINDING_SOURCE
                                Try
                                    'Without true on the last argument, it will not work properly!!!
                                    MyCheckbox.DataBindings.Clear()
                                    BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).BindingSource, BindingRecord.Item(i).ColumnName, True)
                                    MyCheckbox.DataBindings.Add(BindingRecord.Item(i).Binding)
                                Catch ex As Exception
                                    BindControls = False
                                    MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)

                                End Try
                            Case DataSourceType.DATA_TABLE
                                Try
                                    'Without true on the last argument, it will not work properly!!!
                                    MyCheckbox.DataBindings.Clear()
                                    BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).DataTable, BindingRecord.Item(i).ColumnName, True)
                                    MyCheckbox.DataBindings.Add(BindingRecord.Item(i).Binding)
                                Catch 'Check if it is a binding source instead
                                    BindControls = False
                                    MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)
                                End Try
                            Case Else

                        End Select



                    End If
                Case GetType(RadioButton)
                    If BindingRecord(i).ControlProperty = "CheckState" Then
                        Dim MyRadioButton As RadioButton = DirectCast(BindingRecord(i).Control, RadioButton)
                        Select Case SourceType
                            Case DataSourceType.BINDING_SOURCE
                                Try
                                    'Without true on the last argument, it will not work properly!!!
                                    MyRadioButton.DataBindings.Clear()
                                    BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).BindingSource, BindingRecord.Item(i).ColumnName, True)
                                    MyRadioButton.DataBindings.Add(BindingRecord.Item(i).Binding)
                                Catch ex As Exception
                                    BindControls = False
                                    MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)

                                End Try
                            Case DataSourceType.DATA_TABLE
                                Try
                                    'Without true on the last argument, it will not work properly!!!
                                    MyRadioButton.DataBindings.Clear()
                                    BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).DataTable, BindingRecord.Item(i).ColumnName, True)
                                    MyRadioButton.DataBindings.Add(BindingRecord.Item(i).Binding)
                                Catch 'Check if it is a binding source instead
                                    BindControls = False
                                    MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)
                                End Try
                            Case Else

                        End Select



                    End If
                Case Else
                    'Try 'Remove binding if it exists
                    '    BindingRecord.Item(i).Control.DataBindings.Remove(BindingRecord.Item(i).Binding)
                    'Catch

                    'End Try

                    Select Case SourceType
                        Case DataSourceType.BINDING_SOURCE
                            Try
                                BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).BindingSource, BindingRecord.Item(i).ColumnName)
                               
                                BindingRecord.Item(i).Control.DataBindings.Clear()
                                BindingRecord.Item(i).Control.DataBindings.Add(BindingRecord.Item(i).Binding)
                            Catch
                                BindControls = False
                                MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)
                            End Try
                        Case DataSourceType.DATA_TABLE
                            Try
                                'BindingRecord.Item(i).Control.DataBindings.Remove(BindingRecord.Item(i).Binding)
                                BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).DataTable, BindingRecord.Item(i).ColumnName)
                                BindingRecord.Item(i).Control.DataBindings.Clear()
                                BindingRecord.Item(i).Control.DataBindings.Add(BindingRecord.Item(i).Binding)
                            Catch 'Check if it is a binding source instead
                                BindControls = False
                                MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)
                            End Try
                        Case Else ' 
                            Try
                                BindingRecord.Item(i).Binding = New Binding(BindingRecord.Item(i).ControlProperty, BindingRecord.Item(i).BindingSource, BindingRecord.Item(i).ColumnName)
                                BindingRecord.Item(i).Control.DataBindings.Clear()
                                BindingRecord.Item(i).Control.DataBindings.Add(BindingRecord.Item(i).Binding)
                            Catch
                                BindControls = False
                                MsgBox("Unable to Bind Control number " + i.ToString + vbCrLf + vbCrLf + BindingRecord(i).Control.Name)
                            End Try

                    End Select



            End Select
            i = i + 1
        Next
    End Function

#End Region '"SQL and OLEDB Helper Functions"

End Class
