Imports System
Imports System.IO
Imports System.Data
Imports System.Data.OleDb

Public Class frmSelectDatabase
    'Private gDataBaseConnection As OleDb.OleDbConnection
    Private Sub btnSelectDataBase_Click(sender As System.Object, e As System.EventArgs)
        'get the new Access database
        'Dim strDataBasePathAndName As String = str_OpenDataBase()

        ''Make sure that there is a database selected
        'If strDataBasePathAndName <> "" Then
        '    frmDataBaseBrowser.gDataBaseConnection.Close()
        '    frmDataBaseBrowser.gDataBaseConnection = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strDataBasePathAndName + ";Persist Security Info=True;Jet OLEDB:Database Password=baldts")
        '    Try
        '        'try to open the connection
        '        Call frmDataBaseBrowser.gDataBaseConnection.Open()
        '    Catch ex As Exception
        '        MessageBox.Show("Could not connect for some reason.... is the file on the right location? --> check connectionstring")
        '    End Try
        'End If

        'If frmDataBaseBrowser.gDataBaseConnection.State = ConnectionState.Open Then

        'End If

    End Sub

    Private Sub frmSelectDatabase_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Load from settings

        If My.Settings.Prompt_To_Change_Database = True Then
            My.Settings.FailureReportDataBaseDataSource = My.Settings.FailureReportDataBaseDataSource.Replace("|DataDirectory|", System.Windows.Forms.Application.StartupPath)
            My.Settings.MeterSpecDataBaseDataSource = My.Settings.MeterSpecDataBaseDataSource.Replace("|DataDirectory|", System.Windows.Forms.Application.StartupPath)
        Else

        End If
        txtFRDataBaseIntitialCatalog.Text = My.Settings.FailureReportDataInitialCatalog
        txtFRDataBaseDataSource.Text = My.Settings.FailureReportDataBaseDataSource
        txtMeterSpecInitialCatalog.Text = My.Settings.MeterSpecDataBaseInitialCatalog
        txtMeterSpecDataBaseDataSource.Text = My.Settings.MeterSpecDataBaseDataSource
    End Sub

    Private Sub btnSelectDataBase_Click_1(sender As System.Object, e As System.EventArgs) Handles btnSelectFRDataBase.Click

        'get the new Access database
        Try
            'Dim strDataBasePathAndName As String = frmDataBaseBrowser.gMyCustomDataBaseAccess.str_GetAccessDBPathAndNameDialog()
            Dim strDataBasePathAndName As String = frmFailureBrowser.gMyCustomDBAccess.str_Get_AccessDB_PathAndNameDialog()
            'gMyCustomDBAccess()
            '
            txtFRDataBaseDataSource.Text = strDataBasePathAndName
        Catch
        End Try



        Dim i As Integer = 1
    End Sub

    Public Overloads Function ApplyDataBaseChanges(ByRef MyConnection As OleDbConnection, MyConnectionString As String, bSecurityState As Boolean, Password As String) As String

        Dim MyFullConnectionString As String = ""
        Dim strPersistSecurityState As String

        If bSecurityState Then
            strPersistSecurityState = "True"
        Else
            strPersistSecurityState = "False"
        End If

        If MyConnection.State = ConnectionState.Open Then
            MyConnection.Close()
        End If

        MyFullConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + MyConnectionString + ";Persist Security Info=" + strPersistSecurityState + ";Jet OLEDB:Database Password=" + Password

        MyConnection = New OleDb.OleDbConnection(MyFullConnectionString)

        Try
            'try to open the connection
            MyConnection.Open()
        Catch ex As Exception
            MessageBox.Show("Could not connect for some reason.... is the file on the right location? --> check connectionstring")
            MsgBox("My Connection string:" + MyFullConnectionString)
            If MyConnection.State <> ConnectionState.Closed Then
                MyConnection.ResetState()
                MyConnection.Close()
            End If
            MsgBox("My Connection string:" + MyFullConnectionString)
            Return MyFullConnectionString

        End Try

        Return MyFullConnectionString

    End Function

    Public Overloads Function ApplyDataBaseChanges(ByRef MyConnection As Data.SqlClient.SqlConnection, MyConnectionString As String, initialCatalog As String, bIntegratedSecurity As Boolean) As String

        Dim MyFullConnectionString As String = ""
        Dim strPersistSecurityState As String

        If bIntegratedSecurity Then
            strPersistSecurityState = "True"
        Else
            strPersistSecurityState = "False"
        End If

        If MyConnection.State = ConnectionState.Open Then
            MyConnection.Close()
        End If

        MyFullConnectionString = "Data Source=" + MyConnectionString + ";Initial Catalog=" + initialCatalog + ";Integrated Security=" + strPersistSecurityState

        MyConnection = New Data.SqlClient.SqlConnection(MyFullConnectionString)

        Try
            'try to open the connection
            MyConnection.Open()
        Catch ex As Exception
            MessageBox.Show("Could not connect for some reason.... has the correct server been mapped? --> check connectionstring")
            MsgBox("My Connection string:" + MyFullConnectionString)
            If MyConnection.State <> ConnectionState.Closed Then
                'MyConnection.Close()
                MyConnection.Close()
            End If
            MsgBox("My Connection string:" + MyFullConnectionString)
            Return MyFullConnectionString

        End Try

        Return MyFullConnectionString

    End Function

    Private Sub btnApplyDataBaseChanges_Click(sender As System.Object, e As System.EventArgs) Handles btnFRApplyDataBaseChanges.Click

        Dim MyConnection As New Data.SqlClient.SqlConnection

        'Make sure that there is a database selected
        If txtFRDataBaseDataSource.Text <> "" Then

            'Dim strDBFullConnectionString As String = ApplyDataBaseChanges(frmDataBaseBrowser.gDataBaseConnection, txtFRDataBaseDirectoryAndName.Text.Trim, chkFRDataBaseSecurityInfoIsPersistant.Checked, txtFRDataBAsePassWord.Text.Trim)
            Dim strDBFullConnectionString As String = ApplyDataBaseChanges(MyConnection, txtFRDataBaseDataSource.Text.Trim, txtFRDataBaseIntitialCatalog.Text, chkFRDataBaseIntegratedSecurityState.Checked)
            If MyConnection.State = ConnectionState.Open Then
                Try
                    My.Settings.FailureReportDataBaseDataSource = txtFRDataBaseDataSource.Text.Trim
                    My.Settings.FailureReportDataInitialCatalog = txtFRDataBaseIntitialCatalog.Text.Trim
                    My.Settings.FailureReportDBIntegratedSecurityState = chkFRDataBaseIntegratedSecurityState.CheckState
                    My.Settings.FailureReportDataBaseFullConnectionString = strDBFullConnectionString
                Catch ex As Exception 'Catch error
                    MsgBox(ex.ToString)
                End Try
            End If
            MyConnection.Close()

            MyConnection = New Data.SqlClient.SqlConnection

            strDBFullConnectionString = ApplyDataBaseChanges(MyConnection, txtMeterSpecDataBaseDataSource.Text.Trim, txtMeterSpecInitialCatalog.Text.Trim, chkMeterSpecDataBaseIntegratedSecurityState.Checked)
            If MyConnection.State = ConnectionState.Open Then
                Try
                    My.Settings.MeterSpecDataBaseDataSource = txtMeterSpecDataBaseDataSource.Text.Trim
                    My.Settings.MeterSpecDataBaseInitialCatalog = txtMeterSpecInitialCatalog.Text.Trim
                    My.Settings.MeterSpecDBIntegratedSecurityState = chkMeterSpecDataBaseIntegratedSecurityState.CheckState
                    My.Settings.MeterSpecDataBaseFullConnectionString = strDBFullConnectionString
                Catch ex As Exception 'Catch error
                    MsgBox(ex.ToString)
                End Try
            End If
            MyConnection.Close()
            MyConnection.Dispose()
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancelChanges_Click(sender As System.Object, e As System.EventArgs) Handles btnFRCancelChanges.Click
        Me.Close()
    End Sub

    Private Sub btnSelectMeterSpecDataBase_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectMeterSpecDataBase.Click
        'get the new Access database
        Dim strDataBasePathAndName As String = frmFailureBrowser.gMyCustomDBAccess.str_Get_AccessDB_PathAndNameDialog()
        txtMeterSpecDataBaseDataSource.Text = strDataBasePathAndName
    End Sub

    Private Sub EnableEdit_Click(sender As System.Object, e As System.EventArgs) Handles EnableEdit.Click
        btnFRApplyDataBaseChanges.Enabled = True
        btnSelectFRDataBase.Enabled = True
        btnSelectMeterSpecDataBase.Enabled = True
        chkFRDataBaseIntegratedSecurityState.Enabled = True
        chkMeterSpecDataBaseIntegratedSecurityState.Enabled = True
        txtFRDataBaseIntitialCatalog.ReadOnly = False
        txtMeterSpecInitialCatalog.ReadOnly = False
        txtFRDataBaseDataSource.ReadOnly = False
        txtMeterSpecDataBaseDataSource.ReadOnly = False

    End Sub
End Class