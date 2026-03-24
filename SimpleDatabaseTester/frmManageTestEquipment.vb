

Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class frmManageTestEquipment
    Dim _MyDatabaseaccess As New cCustomDataBaseAccess
    Dim _MyTestEquipmentBindingSource As BindingSource
    Dim _MyTestEquipmentDataAdaptor As SqlDataAdapter 'OleDb.OleDbDataAdapter
    Public gSelectedTestEquipment As DataTable
    Dim _TestEquipmentBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker)
    Public WithEvents _MyTestEquipmentBinding As Binding
    Public _MyFilter As String = ""
    Dim _MyTestEquipmentTable As New DataTable
    Dim _CurrentState As eTestEquipmentState
    Dim _ColorNonEditableBackGround As System.Drawing.Color = System.Drawing.Color.LightYellow
    Dim _ColorEditableBackGround As System.Drawing.Color = System.Drawing.Color.White

    Enum eTestEquipmentState
        ReadOnlyViewDevice
        NewDevice
        CopyDevice
        ReviseDevice
        AdminEditDevice
        ObsoleteDevice
    End Enum

    Private Sub frmManageTestEquipment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '= _MyDatabaseaccess.GetData(frmFailureBrowser.gMyMeterSpecOleDBConnection, "TEST_EQUIPMENT", "WHERE Active = " + _SQL_TRUE)

        'initalize
        Me.DialogResult = Windows.Forms.DialogResult.None

        _MyTestEquipmentDataAdaptor = New SqlDataAdapter("SELECT * From [TEST_EQUIPMENT] ORDER BY [ID] Desc", frmFailureBrowser.gMyMeterSpecDBConnection)
        _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)
        _MyTestEquipmentBindingSource = New BindingSource
        '_MyBindingNavigator = New BindingNavigator
        _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable

        DataGridView1.DataSource = _MyTestEquipmentBindingSource
        ' BindingNavigatorTestEquipment = New BindingNavigator(_MyTestEquipmentBindingSource)
        ' BindingNavigatorTestEquipment.BindingSource.DataSource = dTestEquipmentTable


        'create automatic Queries
        'Create the commandbuilder object
        Dim DbCommandBuilder As SqlCommandBuilder = New SqlCommandBuilder(_MyTestEquipmentDataAdaptor)
        'BindingNavigatorTestEquipment.Enabled = True
        'This is necessary since some of the column names have spaces in them
        DbCommandBuilder.QuotePrefix = "["
        DbCommandBuilder.QuoteSuffix = "]"

        'Create Update, Delete, and Insert commands
        _MyTestEquipmentDataAdaptor.UpdateCommand = DbCommandBuilder.GetUpdateCommand
        _MyTestEquipmentDataAdaptor.DeleteCommand = DbCommandBuilder.GetDeleteCommand
        _MyTestEquipmentDataAdaptor.InsertCommand = DbCommandBuilder.GetInsertCommand

        'Use the custom bindingtracket class to create databindings for all of the controls
        'init Binding Record
        _TestEquipmentBindingRecord = New List(Of cCustomDataBaseAccess.cDataBindingTracker)
        _TestEquipmentBindingRecord.Clear()
        'Que up bindings ofr controls here

        'Text, Maskedtext, and Richtext boxes
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentID, "Text", "ID", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentManufacturer, "Text", "Manufacturer", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentModel, "Text", "Model", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentDescription, "Text", "Description", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txttestEquipmentSerialNumber, "Text", "Serial Number", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentLastCalDate, "Text", "Last Cal", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentNextCalDate, "Text", "Next Cal", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentLabIdentifier, "Text", "Lab Identifier", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentNote, "Text", "Note", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentRev, "Text", "Rev", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentTestGroupMembers, "Text", "Test Group Members", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        ' _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentTestType, "Text", "Type", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'combobox
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(xocbTestEquipmentType, "Text", "Type", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'check boxes
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkActiveRevision, "CheckState", "Active Rev", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentIsTestGroup, "CheckState", "Test Group", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentShowObsolete, "CheckState", "Obsolete", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentCalReq, "CheckState", "Cal Req", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'Excute the bindings to the database....


        'Call the function the creates the bindings
        _MyDatabaseaccess.BindControls(_TestEquipmentBindingRecord)

        'Default
        _MyFilter = "[Active Rev] = True AND [Obsolete] = False"
        _MyTestEquipmentBindingSource.Filter = _MyFilter

        'during load
        ' PopulateAutoCompleteTextBox(txtTestEquipmentTestType, frmFailureBrowser.gMyMeterSpecOleDBConnection, "Test_Type", "TEST_EQUIPMENT_TYPE", "WHERE Active = " + _SQL_TRUE)

        rcbEditModeTest.DataSource = [Enum].GetValues(GetType(eTestEquipmentState))
    End Sub
End Class