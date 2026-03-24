Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
'Imports Microsoft.VisualBasic.
Public Class cSQL_Control

#Region "General SQL Variables"
    Public SQLCon As New SqlConnection With {.ConnectionString = "Data Source=USLAFNB365\SQLEXPRESS;Initial Catalog=TEST_MATRIX;Integrated Security=True"}
    Public SQLCmd As SqlCommand
    Public SQL_DataAdapter As SqlDataAdapter
    Public SQL_Dataset As DataSet
    Public SQLParameters As New List(Of SqlParameter)
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
    Public Overloads Function RunQuery(TableName As String, KeyColumn As String, KeyValue As Integer) As Integer
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
    ''' <returns># of records, SQLErrors.QueryError on error... The results are put in Public Dataset 'SQL_Dataset' </returns>
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
    ''' This Function will return the number TableNames in a database, excluding the System tables
    ''' </summary>
    ''' <returns> # of rows (# of tables), SQLErrors.QueryError on error...The results are put in Public Dataset 'SQL_Dataset'</returns>
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
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Throw New Exception("Error Checking for Unique Record: " + vbCrLf + ex.ToString)
        End Try

    End Function

    ''' <summary>
    ''' This Function Checks to see if a Record is Unique based on a single column and column value as integer and Returns true if unique
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
    ''' This Function checks to see if a Record is Unique based on multiple columns, and associated values 
    ''' </summary>
    ''' <param name="TableName">DAtabase Tablename</param>
    ''' <param name="Columns">The columns with the Values to check</param>
    ''' <param name="CellValue">The Cell values to check</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function IsUnique(ByVal TableName As String, Columns As List(Of String), CellValue As List(Of String)) As Boolean
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

            RunQuery(Query)
            If SQL_Dataset.Tables(0).Rows.Count = 0 Then
                Return True
            Else
                Return False
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
    ''' This is a helper function for inserting a new record into a datbase, returning the number of rows effected.
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
    ''' This function takes a table with varbinary or image or another  column types incapabiliable with a DGV to data that is compatable
    ''' The actual data is not displayed but instead the data types in theses cases
    ''' </summary>
    ''' <param name="MyDataTable">Datatable with types incapatiable with DGV</param>
    ''' <returns>Safe Datatable, or throws excepption on error</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Function MakeTableDGVSafe(MyDataTable As DataTable) As DataTable
        Try

            Dim MyClonedDataTable = MyDataTable.Clone()

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
            Throw New Exception("Unable to Process DataTable. Returning Original Table" + vbCrLf + ex.ToString)

        End Try
    End Function

#End Region 'General SQL Functions and Subs

#Region "Write and Read Files To Database"

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
    ''' <param name="ID_Value">Unique Key Value </param>
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
    ''' by a File Dialob box if no path and File name is supplied. Returns the number of Records effected.
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
    ''' <param name="FileNameColumn">The column that holds the File Name</param>
    ''' <param name="FileDataColumn">The column that holds the File data</param>
    ''' <param name="FileExtentionColumn">the Column that holds the extention</param>
    ''' <param name="IDColumn">Primary key of the Table</param>
    ''' <param name="ID_Value">Primary Key value of the table</param>
    ''' <returns>Tempory file name and path delimited with the file extention</returns>
    ''' <remarks>Frank Boudrau 2016</remarks>
    Public Function Get_File_FromDB(TableName As String, FileNameColumn As String, FileDataColumn As String, FileExtentionColumn As String, IDColumn As String, ID_Value As Integer) As String
        Dim strsql As String = Nothing

        Try
            'Should be only one result
            If GetTableData(TableName, IDColumn, ID_Value) = 1 Then
                'Parse Data
                Dim FileData As Byte() = CType(SQL_Dataset.Tables(0).Rows(0)(FileDataColumn), Byte())
                Dim FileExtention As String = ""
                Try
                    FileExtention = SQL_Dataset.Tables(0).Rows(0)(FileExtentionColumn).ToString()
                Catch
                    FileExtention = "" ' assume none
                End Try
                Dim FileName As String = ""
                Try
                    FileName = SQL_Dataset.Tables(0).Rows(0)(FileNameColumn).ToString()
                Catch ex As Exception

                End Try
                'Read File  data into a memory stream
                Using ms As New MemoryStream(FileData, 0, FileData.Length)

                    Dim MyTempFileNameAndPath As String
                    If FileExtention <> "" And FileName <> "" Then
                        MyTempFileNameAndPath = Path.GetTempPath + FileName + FileExtention

                    ElseIf FileExtention <> "" And FileName = "" Then
                        MyTempFileNameAndPath = Path.GetTempFileName.Replace("tmp", FileExtention)

                    ElseIf FileExtention = "" And FileName <> "" Then
                        MyTempFileNameAndPath = Path.GetTempPath
                        MyTempFileNameAndPath = MyTempFileNameAndPath + FileName
                    Else
                        MyTempFileNameAndPath = Path.GetTempFileName
                    End If

                    If File.Exists(MyTempFileNameAndPath) Then
                        MyTempFileNameAndPath = MyTempFileNameAndPath.Replace(FileName + FileExtention, FileName + "(0)" + FileExtention)
                    End If

                    Dim Count As Integer = 1

                    While File.Exists(MyTempFileNameAndPath)
                        MyTempFileNameAndPath = MyTempFileNameAndPath.Replace(FileName + "(" + (Count - 1).ToString + ")" + FileExtention, FileName + "(" + Count.ToString + ")" + FileExtention)
                        Count = Count + 1
                        'There needs to be way out of this 
                        If Count > 2 ^ 16 Then
                            MyTempFileNameAndPath = Path.GetTempFileName.Replace("tmp", FileExtention)
                        End If
                    End While


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


#Region "Failure Report DataBase Variables"

    'Aliases
    Public Const MIN_USER_NAME_LENGTH = 8
    Public Const MAX_USER_NAME_LENGTH = 50
    Public Const MIN_PASSWORD_NAME_LENGTH = 8
    Public Const MAX_PASSWORD_NAME_LENGTH = 50
    Public Const MAX_FIRST_NAME_LENGTH = 50
    Public Const MAX_LAST_NAME_LENGTH = 50
    Public Const MAX_EMAIL_LENGTH = 1024
    Public Const VALUE_NOT_SET = -1

    'Custom Classes
    Public LoggedOnUser As cLoggedOnUser
    Public LoggedOnUserBackup As cLoggedOnUser
    Public Class cLoggedOnUser
        Public User_ID As Integer
        Public UserName As String
        Public Password As String
        Public AccessLevel As String
        Public AccessLevel_ID As eACCESS_LEVEL
        Public Sub init()
            UserName = ""
            Password = ""
            AccessLevel = ""
            AccessLevel_ID = eACCESS_LEVEL.DEFAULT_LEVEL
            User_ID = -1
        End Sub
    End Class

    'Custom Enumerations
    ''' <summary>
    ''' Enumeration of the defined Access level rights for the database
    ''' Right now this has to manually be maintained with the ACCESS_LEVEL
    ''' Table in the 'TEST'Database is there a better way?
    ''' </summary>
    ''' <remarks>Frank Boudreau 2016.</remarks>
    Public Enum eACCESS_LEVEL
        ''' <summary>
        ''' 'All Rights Create, Edit, Edit Other Users, Delete, Edit Records, Edit Defaults etc..
        ''' </summary>
        ''' <remarks></remarks>
        ADMIN = 1
        ''' <summary>
        ''' No rights,  In active users should be set to this Access Level 
        ''' </summary>
        ''' <remarks></remarks>
        DEFAULT_LEVEL = 6 'No Rights
        ''' <summary>
        ''' ENGINEERING May Create, and edit failure Reports .  May change own password
        ''' </summary>
        ''' <remarks></remarks>
        ENGINEERING = 3
        ''' <summary>
        ''' READ_ONLY - Read Only Access, may change own password
        ''' </summary>
        ''' <remarks></remarks>
        READ_ONLY = 5
        ''' <summary>
        ''' APPROVER - Authorized to approve a Failure Report Resolution, + Engineering Rights
        ''' </summary>
        ''' <remarks></remarks>
        APPROVER = 2
        ''' <summary>
        ''' TEST - May create Failure reports and change own password
        ''' </summary>
        ''' <remarks></remarks>
        TEST = 4
    End Enum
#End Region 'Failure Report Database Variables

#Region "Failure Report Database Functions and Subs"
    Public Sub AddUser(Username As String, Password As String, FirstName As String, LastName As String, Email As String, Active As Integer, AccessLevel As Integer)


        Dim NonQuery As String = "INSERT INTO [USER] (USER_NAME,ACCESS_LEVEL_ID,FIRST_NAME,LAST_NAME,EMAIL,ACTIVE,PASS_WORD) " + _
                                "VALUES (" + _
                                Username + "," + _
                                AccessLevel.ToString + "," + _
                                FirstName + "," + _
                                LastName + "," + _
                                Email + "," + _
                                Active.ToString + "," + _
                                Password + ")"
        ExecuteNonQuery(NonQuery)

    End Sub

    ''' <summary>
    ''' This function Queries USER and ACCESS LEVEL to validate the user and determine Access Rights.  A validated user 
    ''' is assigned to 'LoggedOnUser'  A validated User must have an 'Active' status in the database.
    ''' </summary>
    ''' <param name="Username">Username</param>
    ''' <param name="Password">Password</param>
    ''' <returns>True = Success, False = Invalid User or Password</returns>
    ''' <remarks>Frank Boudreau Landi+Gyr 2016</remarks>
    Public Function IsAuthenticated(Username As String, Password As String) As Boolean
        Try

            'Check to see if CurrentUser Exists and if so backup
            If LoggedOnUser IsNot Nothing Then
                LoggedOnUserBackup = New cLoggedOnUser
                LoggedOnUserBackup.init()
                LoggedOnUserBackup = LoggedOnUser
            End If

            'Clear the Dataset if exisits
            If SQL_Dataset IsNot Nothing Then
                SQL_Dataset.Clear()
            End If

            'Return the numbers of users with the Username and Password that are active
            RunQuery("SELECT * " + _
                         "FROM [USER] " + _
                         "WHERE [USER_NAME] ='" + Username.Trim + "' " + _
                         "AND [PASS_WORD] ='" + Password.Trim + "' COLLATE SQL_Latin1_General_CP1_CS_AS " + _
                         "AND [ACTIVE] = " + 1.ToString)

            'Should only be one row that meets criteria...
            If SQL_Dataset.Tables(0).Rows.Count = 1 Then
                LoggedOnUser = New cLoggedOnUser
                LoggedOnUser.init()
                LoggedOnUser.UserName = Username.Trim
                LoggedOnUser.Password = Password.Trim
                LoggedOnUser.AccessLevel_ID = Val(SQL_Dataset.Tables(0).Rows(0).Item("ACCESS_LEVEL_ID"))
                LoggedOnUser.User_ID = Val(SQL_Dataset.Tables(0).Rows(0).Item("USER_ID"))

                'Translate Access level from related table
                SQL_Dataset.Clear()
                RunQuery("SELECT ACCESS_LEVEL " + _
                         "FROM [ACCESS_LEVEL] " + _
                         "WHERE [ID] = " + Val(LoggedOnUser.AccessLevel_ID).ToString)

                LoggedOnUser.AccessLevel = SQL_Dataset.Tables(0).Rows(0).Item("ACCESS_LEVEL")

                Return True
            Else
                RunQuery("SELECT Count(USER_NAME) as UserCount " + _
                       "FROM [USER] " + _
                       "WHERE [USER_NAME] ='" + Username.Trim + "' " + _
                       "AND [PASS_WORD] ='" + Password.Trim + "' COLLATE SQL_Latin1_General_CP1_CS_AS")

                'If the Username and Password are valid....
                If SQL_Dataset.Tables(0).Rows(0).Item("UserCount") >= 1 Then
                    MsgBox("Your Account has been disabled. Please Contact your the Database Administrator.")
                Else
                    MsgBox("Invalid User Credentials.", MsgBoxStyle.Critical, "LOGIN FAILED")
                End If

                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If

            'Restore currentuser to previous user in case of error
            If LoggedOnUserBackup IsNot Nothing Then
                LoggedOnUser = LoggedOnUserBackup
            End If

            Return False
        End Try

    End Function

    ''' <summary>
    ''' This function validates and Parses the data to be updated in an existing USER Record.  The 
    ''' </summary>
    ''' <param name="User_ID">Primary key of [USER] table in Failure Report generator to edit</param>
    ''' <param name="Username">User name of Current User, Provides redunancey</param>
    ''' <param name="UserPassword">Current User Password, Provides Redunancy</param>
    ''' <param name="NewUserName">The updated User name of the record under edit</param>
    ''' <param name="NewUserPassword">The updated passowrd of the record under edit</param>
    ''' <param name="NewActiveStatus">The Active / Inactive status opf the User record under edit or
    '''  ALIAS VALUE_NOT_SET or an integer set to -1</param>
    ''' <param name="NewAccessLevel">The Access level for the user of the record under edit or 
    ''' ALIAS VALUE_NOT_SET or an integer set to -1</param>
    ''' <param name="NewFirstname">The Updated First name</param>
    ''' <param name="NewLastName">Updated Last name</param>
    ''' <param name="NewEmail">The updated Email</param>
    ''' <returns>True = Success, False</returns>
    ''' <remarks>Frank Boudreau Landis+Gyr 2016</remarks>
    Public Function ValidateAndParseUserInfo(User_ID As Integer, Username As String, _
                              UserPassword As String, ByRef ColumnName As List(Of String), ByRef CellValue As List(Of String), _
                              Optional NewUserName As String = Nothing, _
                              Optional NewUserPassword As String = Nothing, Optional NewActiveStatus As Integer = VALUE_NOT_SET, _
                              Optional NewAccessLevel As Integer = VALUE_NOT_SET, Optional NewFirstname As String = Nothing, _
                              Optional NewLastName As String = Nothing, Optional NewEmail As String = Nothing) As Boolean
        Try


            If ColumnName Is Nothing Then
                ColumnName = New List(Of String)
            End If
            ColumnName.Clear()

            If CellValue Is Nothing Then
                CellValue = New List(Of String)
            End If
            CellValue.Clear()

            'validate Username
            If NewUserName IsNot Nothing Then

                If NewUserName.Length >= MIN_USER_NAME_LENGTH And NewUserName.Length <= MAX_USER_NAME_LENGTH Then
                    If IsUnique("USER", "USER_NAME", NewUserName) = False Then
                        MsgBox(NewUserName + " is allready in use. Please select a different user name.")
                        Return False
                    End If
                Else
                    MsgBox("Username must be at least " + MIN_USER_NAME_LENGTH.ToString + " characters and " + _
                           +vbCrLf + "No longer than " + MAX_USER_NAME_LENGTH.ToString + " characters.")
                    Return False
                End If

                ColumnName.Add("USER_NAME")
                CellValue.Add("'" + NewUserName + "'")

            End If

            'validate password
            If NewUserPassword IsNot Nothing Then

                If NewUserPassword.Length < MIN_PASSWORD_NAME_LENGTH And NewUserPassword.Length > MAX_PASSWORD_NAME_LENGTH Then

                    MsgBox("Password must be at least " + MIN_PASSWORD_NAME_LENGTH.ToString + " characters and " + _
                            +vbCrLf + "No longer than " + MAX_PASSWORD_NAME_LENGTH.ToString + " characters.")
                    Return False
                End If
                ColumnName.Add("PASS_WORD")
                CellValue.Add("'" + NewUserPassword + "'")
            End If

            'validate NewActive Status
            If NewActiveStatus = VALUE_NOT_SET Then
                'Do nothing

            Else
                'make sure it is within the valid range
                If NewActiveStatus <> 1 Or NewActiveStatus <> 0 Then
                    MsgBox("Values for 'Active' must be '1 (True)' or '0 (False)")
                    Return False
                Else
                    ColumnName.Add("ACTIVE")
                    CellValue.Add(NewActiveStatus.ToString)
                End If

            End If

            'Validate New Access Level
            If NewAccessLevel = VALUE_NOT_SET Then
                'do nothing
            Else
                'check to see if it is valid exisits in table
                'function returns true if value does NOT exist
                If IsUnique("ACCESS_LEVEL", "ACCESS_LEVEL_ID", NewAccessLevel) = True Then
                    MsgBox("Access Level ID is outside of range")
                    Return False
                Else
                    ColumnName.Add("ACCESS_LEVEL_ID")
                    CellValue.Add(NewAccessLevel.ToString)
                End If
            End If

            'validate NewFirstName

            If NewFirstname IsNot Nothing Then

                If NewFirstname.Length > MAX_FIRST_NAME_LENGTH Then

                    MsgBox("First Name may be no longer than " + MAX_FIRST_NAME_LENGTH.ToString + " characters.")
                    Return False
                Else
                    ColumnName.Add("FIRST_NAME")
                    CellValue.Add("'" + NewFirstname + "'")
                End If

            End If

            'validate NewLastName
            If NewLastName IsNot Nothing Then

                If NewLastName.Length > MAX_LAST_NAME_LENGTH Then

                    MsgBox("Last Name may be no longer than " + MAX_LAST_NAME_LENGTH.ToString + " characters.")
                    Return False
                Else
                    ColumnName.Add("LAST_NAME")
                    CellValue.Add("'" + NewLastName + "'")

                End If

            End If
            'validate NewEmail
            If NewEmail IsNot Nothing Then

                If NewEmail.Length > MAX_EMAIL_LENGTH Then

                    MsgBox("Last Name may be no longer than " + MAX_EMAIL_LENGTH.ToString + " characters.")
                    Return False
                Else
                    ColumnName.Add("EMAIL")
                    CellValue.Add("'" + NewEmail + "'")
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return False
        End Try



    End Function

    Public Function GetUserID(Username As String, Optional RowCount As Integer = 0) As Integer
        Try
            Dim UserID As Integer = 0

            'Clear the Dataset if exisits
            If SQL_Dataset IsNot Nothing Then
                SQL_Dataset.Clear()
            Else
                SQL_Dataset = New DataSet
            End If

            RunQuery("SELECT *" + _
                        " FROM [USER]" + _
                        " WHERE [USER_NAME] ='" + Username.Trim + "'") ' + _
            ' " AND [PASS_WORD] ='" + Password.Trim + "' COLLATE SQL_Latin1_General_CP1_CS_AS" + _
            ' " AND [ACTIVE] = " + 1.ToString)

            If SQL_Dataset.Tables(0).Rows.Count = 1 Then
                RowCount = SQL_Dataset.Tables(0).Rows.Count
                UserID = Val(SQL_Dataset.Tables(0).Rows(0).Item("USER_ID"))
            ElseIf SQL_Dataset.Tables(0).Rows.Count > 1 Then
                RowCount = SQL_Dataset.Tables(0).Rows.Count
                Return -2
            End If

            Return UserID
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Return SQLErrors.QueryError
        End Try
    End Function


    ''' <summary>
    ''' This function updates USER infromation for a existing record.  For string values that do not need to b
    ''' be updated enter 'NOTHING'.  For integer values that do not need to be updated enter 
    ''' ALIAS VALUE_NOT_SET or an integer set to -1 
    ''' </summary>
    ''' <param name="User_ID">Primary key of [USER] table in Failure Report generator to edit</param>
    ''' <param name="Username">User name of Current User, Provides redunancey</param>
    ''' <param name="UserPassword">Current User Password, Provides Redunancy</param>
    ''' <param name="NewUserName">The updated User name of the record under edit</param>
    ''' <param name="NewUserPassword">The updated passowrd of the record under edit</param>
    ''' <param name="NewActiveStatus">The Active / Inactive status opf the User record under edit or
    '''  ALIAS VALUE_NOT_SET or an integer set to -1</param>
    ''' <param name="NewAccessLevel">The Access level for the user of the record under edit or 
    ''' ALIAS VALUE_NOT_SET or an integer set to -1</param>
    ''' <param name="NewFirstname">The Updated First name</param>
    ''' <param name="NewLastName">Updated Last name</param>
    ''' <param name="NewEmail">The updated Email</param>
    ''' <returns>Number of Records Updated ( 0 or 1) 
    ''' Error Codes:
    ''' -1 General Error
    ''' -2 Data Validation Error
    ''' -3 Insufficent Rights Error</returns>
    ''' <remarks></remarks>
    Public Function UpdateUserInfo(User_ID As Integer, Username As String, _
                              UserPassword As String, Optional NewUserName As String = Nothing, _
                              Optional NewUserPassword As String = Nothing, Optional NewActiveStatus As Integer = VALUE_NOT_SET, _
                              Optional NewAccessLevel As Integer = VALUE_NOT_SET, Optional NewFirstname As String = Nothing, _
                              Optional NewLastName As String = Nothing, Optional NewEmail As String = Nothing) As Integer

        Try
            'Validate permission to edit account
            'Check to amke sure that the user has logged on
            'It is possible to browse the database without logging
            'on this helps prevents in an escape in the GUI that 
            'allowing a user access to an edit screen...
            If LoggedOnUser Is Nothing Then
                MsgBox("You must login to edit account data")
                Return -3
            End If

            'Check the case where the User_ID to be edited is not the logged on user.
            If (LoggedOnUser.User_ID <> User_ID) Then

                'Must be an administrator 
                'An administrator may fully an account including USER_NAME, ACTIVE, and ACCESS_LEVEL,
                'however an administarator may not access this information on their own account!
                If LoggedOnUser.AccessLevel_ID = eACCESS_LEVEL.ADMIN And Username = LoggedOnUser.UserName And _
                    UserPassword = LoggedOnUser.Password Then

                    'buffers to hold the parsed command to pass to DataUpdate()
                    Dim ColumnName As New List(Of String)
                    Dim CellValue As New List(Of String)

                    'Validate and parse the data.
                    If ValidateAndParseUserInfo(User_ID, Username, UserPassword, _
                                     ColumnName, CellValue, _
                                     NewUserName, NewUserPassword, NewActiveStatus, _
                                     NewAccessLevel, NewFirstname, NewLastName, NewEmail) Then
                        Dim NumberOfRecordsUpdated = UpdateDataBasedOn_USER_ID("USER", ColumnName, CellValue, User_ID)

                        'On success this should be 1
                        Return NumberOfRecordsUpdated
                    Else
                        Return -2
                    End If




                Else
                    MsgBox("You must be an administrator to edit another users account")
                    Return -3
                End If


            Else 'Edit User editable fields only

                'Username validated to get here now validate passord...
                If LoggedOnUser.Password <> UserPassword Then
                    MsgBox("Unable to authentic user. Check you Username and password and try again")
                    Return -3
                End If

                'buffers to hold the parsed command to pass to DataUpdate()
                Dim ColumnName As New List(Of String)
                Dim CellValue As New List(Of String)
                'Validate and Parse the data...Note NewUserName, NewActiveStatus, and Access Level
                'Are masked off so that they cannot be edited in this branch. 
                If ValidateAndParseUserInfo(User_ID, Username, UserPassword, _
                                    ColumnName, CellValue, _
                                    Nothing, NewUserPassword, VALUE_NOT_SET, _
                                    VALUE_NOT_SET, NewFirstname, NewLastName, NewEmail) Then

                    Dim NumberOfRecordsUpdated = UpdateDataBasedOn_USER_ID("USER", ColumnName, CellValue, User_ID)

                    'Check to make sure that the Password has a value and the record was updated.
                    'Since this branch requires the Logged On user to be the same as the Password in the
                    'Record being updated it is safe to to this here!
                    If NewUserPassword IsNot Nothing And NumberOfRecordsUpdated = 1 Then
                        LoggedOnUser.Password = NewUserPassword
                    End If
                    'On success this should be 1
                    Return NumberOfRecordsUpdated

                Else
                    Return -2
                End If

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
    ''' This function will Query the database and return information on the Current User that is logged in from 
    ''' the program
    ''' </summary>
    ''' <returns>User Login In name, First Name, Last Name, and Email Address</returns>
    ''' <remarks>Frank Boudreau Landis Gyr 2016</remarks>
    Public Function GetCurrentUserInfo() As String
        Try
            'Make sure Object defined
            If LoggedOnUser IsNot Nothing Then
                'Makee sure User_ID has been set since initalized...
                If LoggedOnUser.User_ID > 0 Then

                    'Query the database
                    RunQuery("SELECT USER_NAME,FIRST_NAME,LAST_NAME,EMAIL " + _
                         "FROM [USER] " + _
                         "WHERE [USER_ID] = " + LoggedOnUser.User_ID.ToString)

                    'Buffer to hold data returned from query
                    Dim UserInfo As String = ""
                    For Each i As Object In SQL_Dataset.Tables(0).Rows
                        UserInfo = UserInfo + vbCrLf + _
                                    "User      : " + i.item("USER_NAME") + vbCrLf + _
                                    "First Name: " + i.item("FIRST_NAME") + vbCrLf + _
                                    "Last Name : " + i.item("LAST_NAME") + vbCrLf + _
                                    "Email     : " + i.item("EMAIL") + vbCrLf
                    Next

                    Return UserInfo
                Else
                    Return "Current User Not Available"
                End If
            Else
                Return "Current User Not Available"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            'Error code
            Return "Error Getting Current User Info"
        End Try

    End Function

    ''' <summary>
    ''' Function to Update Multiple Columns as inidcated by the value in USER_ID OF Fr Database, returns the number of rows effected.
    ''' </summary>
    ''' <param name="Table">Datatable to update example "USER"</param>
    ''' <param name="TableColumn">Column Table to update. Example "USER_NAME"</param>
    ''' <param name="CellValue">Value to insert in column, must be in order of column names
    ''' Strings must be wrapped with single Quotes example "'Frank'", Integers must be converted to string
    ''' example FrankAge.ToString.  "NULL" is used to set the Cell value to DBNull.Value </param>
    ''' <param name="PkeyValue">Primary key of the row to be updated</param>
    ''' <returns></returns>
    ''' <remarks>Frank Boudreau Landis Gyr 2015</remarks>
    Public Overloads Function UpdateDataBasedOn_USER_ID(Table As String, TableColumn As List(Of String), CellValue As List(Of String), PkeyValue As Integer) As Integer
        Try

            If TableColumn.Count = CellValue.Count Then
                Dim Command As String = "UPDATE [" + Table + "] SET [" + TableColumn.Item(0) + "] = " + CellValue.Item(0)

                For i = 1 To TableColumn.Count - 1
                    Command = Command + ", [" + TableColumn.Item(i) + "] = " + CellValue.Item(i)
                Next
                Command = Command + " WHERE [USER_ID] = " + PkeyValue.ToString
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
#End Region 'Failure Report Database Functions and Subs



End Class

