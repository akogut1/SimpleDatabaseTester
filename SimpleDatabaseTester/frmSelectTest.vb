
Public Class frmSelectTest

    Public _TestType As String
    Dim pTestName As String
    Public ReadOnly Property Testname As String
        Get
            Return pTestName
        End Get
    End Property
    Public _bFilterTestsbyType As Boolean = False
    Public _SQL_TRUE As String = "1"
    Public _SQL_FALSE As String = "0"
    Public _DialogResult As DialogResult

    Private Sub frmSelectTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkFilterTests.Checked = _bFilterTestsbyType
        If _TestType <> "" Then
            xcbTestType.Text = _TestType
        End If
    End Sub

    Private Sub xcbSelectTest_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xcbSelectTest.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        If xcbTestType.Text.Trim = "" Then
            xcbTestType.Text = "All"
        End If
        _TestType = xcbTestType.Text.Trim
        _bFilterTestsbyType = chkFilterTests.Checked
        With frmFailureBrowser
            'make sure that something has been selected...
            If _TestType <> "" Then
                'Return all standard tests defind in the MeterSpecDatabase
                If _TestType.Trim = "All" Or _bFilterTestsbyType = False Then
                    strFilter = ""
                    .PopulatexboXComboBox(.gMyMeterSpecDBConnection, MyxboXComboBox, ._dbMeterSpecSchema.STANDARD_TEST.TEST_NAME, ._dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
                ElseIf _TestType.Trim = "Past Tests" Then
                    'Get all tests from past tests from the failure report database
                    .PopulatexboXComboBox(.gMyFailureReportDBConnection, MyxboXComboBox, ._dbFRSchema.FR_DBDef.TEST_NAME, ._dbFRSchema.FR_DBDef.TABLE_NAME, "")
                Else
                    strFilter = " AND Test_Type = '" + _TestType.Trim + "'"
                    .PopulatexboXComboBox(.gMyMeterSpecDBConnection, MyxboXComboBox, ._dbMeterSpecSchema.STANDARD_TEST.TEST_NAME, ._dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
                End If

            End If
        End With
        Dim mystop As Boolean = True
    End Sub

    Private Sub cbTestName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles xcbSelectTest.KeyDown
        xcbSelectTest.DroppedDown = True
    End Sub


    Private Sub cbTestName_SelectionCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xcbSelectTest.SelectionChangeCommitted

        Try
            'This code tries to force the TEST type field to agree with the Test Type of  the selected
            'Test Name
            'It only works if the Name exists in the Meter_SPEC database...
            Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
            Dim MyDataRow As DataRowView = DirectCast(MyxboXComboBox.SelectedValue, DataRowView)

            Dim strFilter As String = " WHERE Test = '" + MyDataRow.Item(0).ToString.Trim + "'"
            Dim MyDatatable As DataTable
            With frmFailureBrowser
                'only return distinct matches (meaning do not return duplicates...)
                MyDatatable = .gMyCustomDBAccess.GetDistinctData(.gMyMeterSpecDBConnection, ._dbMeterSpecSchema.STANDARD_TEST.TEST_TYPE, ._dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, strFilter)
            End With

            If MyDatatable.Rows.Count > 0 Then
                'populate the first match
                xcbTestType.Text = MyDatatable.Rows(0)(0).ToString
            Else
                'Assume that is a custom test...
                xcbTestType.Text = "Custom"
            End If
        Catch
            'Silently catch error
        End Try

        ' End If

    End Sub


    Private Sub cbTestType_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xcbTestType.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        With frmFailureBrowser
            .PopulatexboXComboBox(.gMyMeterSpecDBConnection, MyxboXComboBox, ._dbMeterSpecSchema.TEST_TYPE.VALUE, ._dbMeterSpecSchema.TEST_TYPE.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
        End With
    End Sub

   
    Private Sub frmSelectTest_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Cancel the action
        e.Cancel = True
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'Hide the Form
        Me.Hide()

    End Sub

    
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        pTestName = xcbSelectTest.Text
        If pTestName.Trim = "" Then
            MsgBox("Please Select a test, and then Click on 'OK'. Select 'Cancel to return to form with no selection", "Selection Error")
        Else
            _DialogResult = Windows.Forms.DialogResult.OK
            Me.Hide()
        End If
      
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'Hide the Form
        Me.Hide()
    End Sub
End Class