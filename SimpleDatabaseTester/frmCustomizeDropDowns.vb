Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Math
Public Class frmCustomizeDropDowns
    Dim _MyMeterSpecDataTableSchema As DataTable
    Dim gMyOleDBConnection As OleDb.OleDbConnection
    Dim gMyOleDbAdaptor As OleDb.OleDbDataAdapter

    'DataSet to Hold all the Datatables
    Public gMyTestMatrixDataset As DataSet

    'DataTables to hold each table in the Database
    Public gMyProjectLeadTable As DataTable
    Public gMyTestLevelTable As DataTable
    Public gMyTestPlanTable As DataTable
    Public gMyTestMatrixTable As DataTable

    Private Sub frmCustomizeDropDowns_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'On form load instantiate the connection object
        gMyOleDBConnection = New OleDb.OleDbConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
        gMyOleDBConnection.Open()
        'Get the Schema for the user tables
        'When "Table" is added a restiction in Table Type, only users tables are returned
        Dim straRestrictions() As String = New String(3) {}
        straRestrictions(3) = "Table"
        _MyMeterSpecDataTableSchema = gMyOleDBConnection.GetSchema("Tables", straRestrictions)

        'Now add the table names to the combobox
        cbCustDropDowns.Items.Clear()
        cbCustDropDowns.Items.Add("")
        Dim i As Integer
        Dim j As Integer = 0
        For i = 0 To _MyMeterSpecDataTableSchema.Rows.Count - 1 Step i + 1

            For j = 0 To _MyMeterSpecDataTableSchema.Columns.Count - 1
                'Table Names
                If (Not cbCustDropDowns.Items.Contains(_MyMeterSpecDataTableSchema.Rows(i)(j).ToString().Trim)) And (j = 2) Then
                    cbCustDropDowns.Items.Add(_MyMeterSpecDataTableSchema.Rows(i)(j).ToString().Trim)
                End If

            Next
        Next

        'Create Query and Fill the tables
        gMyOleDbAdaptor = New OleDb.OleDbDataAdapter
        gMyTestMatrixDataset = New DataSet
        For i = 1 To cbCustDropDowns.Items.Count - 1
            Dim MyCommand As String = "SELECT * FROM [" + cbCustDropDowns.Items(i).ToString + "]"
            gMyOleDbAdaptor.SelectCommand = New OleDbCommand(MyCommand, gMyOleDBConnection)
            gMyTestMatrixDataset.Tables.Add((cbCustDropDowns.Items(i).ToString.Replace(" ", "")))
            gMyOleDbAdaptor.Fill(gMyTestMatrixDataset.Tables(cbCustDropDowns.Items(i).ToString.Replace(" ", "")))
            ' gMyOleDbAdaptor.Fill(gMyTestMatrixDataset.table, cbCustDropDowns.Items(i).ToString)
        Next


        'close and dispose connection
        gMyOleDBConnection.Close()
        gMyOleDBConnection.Dispose()
    End Sub

    Private Sub cbCustDropDowns_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCustDropDowns.SelectedIndexChanged
        lbHidden.Items.Clear()

        Try
            'Dim MyRow As DataRow = gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString).Rows(0)
            'Dim strDataItem = MyRow(0)

            Select Case cbCustDropDowns.SelectedItem.ToString
                Case "AMR"
                    For Each myrow As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(myrow(0))
                    Next
                Case "AMR Rev"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Form"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "FW Ver"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Level"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Meter"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Project"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "TCC 1"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "TCC 2"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "TCC 3"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "TCC 4"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Test Standards"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(1))
                    Next
                Case "Tested By"
                    For Each row As DataRow In gMyTestMatrixDataset.Tables(cbCustDropDowns.SelectedItem.ToString.Replace(" ", "")).Rows()
                        lbHidden.Items.Add(row(0))
                    Next
                Case "Users"
                    'Do nothing
            End Select

        Catch
        End Try

    End Sub
End Class