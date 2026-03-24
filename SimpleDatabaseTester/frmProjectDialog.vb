Imports System.Windows.Forms
Imports System.Data.Sql
Imports System.Data.SqlClient


''' <summary>
''' This class Creates a form that allows the user to Add, Edit, Select and Set Projects Active and Inactive in the Database
''' </summary>
''' <remarks>Frank Boudreau 5.2.2019</remarks>
Public Class frmProjectDialog
    Public gSelectedProjectID As String = ""
    Public gSelectedProjectName As String = ""
    Dim _MyDatabaseaccess As New cCustomDataBaseAccess
    Dim _MyProjectBindingSource As BindingSource
    Dim _MyUsersBindingSource As BindingSource '...Bindingsource to track User ForeignKey...
    Dim _MyProjectDataAdaptor As SqlDataAdapter
    Dim _MyUsersDataAdaptor As SqlDataAdapter
    Public gSelectedTestEquipment As DataTable
    Dim _MyProjectBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker)
    Dim _UsersBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker) '...binding Record to help create foreign key relationship...
    Public WithEvents _MyTestEquipmentBinding As Binding
    Public _MyFilter As String = ""
    Dim _MyTestEquipmentTable As New DataTable
    Dim _MyUsersTable As New DataTable
    'Dim _CurrentState As eTestEquipmentState
    Dim _ColorNonEditableBackGround As System.Drawing.Color = System.Drawing.Color.LightYellow
    Dim _ColorEditableBackGround As System.Drawing.Color = System.Drawing.Color.White
    Dim _RecordToBeRevisedIndex As Integer 'This variable is used to keep track of which record is being revised.  In the event the user choose to cancel 
    Dim _SafeToProcessEvents As Boolean = False 'inhibit control envents until after the form has loaded and has been shown
    Dim _Delimiter As String = ";"
    Dim _User As frmFailureBrowser.User

    Enum eProjectDialogEditState
        ReadOnlyView
        NewProject
        Edit
        AdminEdit
    End Enum
    Dim _CurrentState As eProjectDialogEditState

    Private Sub frmAdd_Edit_ProjectToDatabase_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        'Don't let the user exit if not in readonly state...
        If _CurrentState <> eProjectDialogEditState.ReadOnlyView Then
            e.Cancel = True
            Exit Sub
        End If

        Try

            If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
                ' e.Cancel = True
                gSelectedProjectID = Nothing
            ElseIf Me.DialogResult = Windows.Forms.DialogResult.OK Then

                '   Me.Hide()
            ElseIf MsgBox("Changes will lost, are you sure you want to Cancel?", MsgBoxStyle.YesNo, "Discard Changes?") = MsgBoxResult.No Then
                e.Cancel = True
                Exit Sub

            Else
                e.Cancel = True
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                gSelectedProjectID = Nothing
                Me.Hide()
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub RefreshProjectList()
        _MyTestEquipmentTable.Clear()
        If chkShowInactiveProjects.Checked = True Then
            _MyProjectBindingSource.RemoveFilter()
        Else
            _MyProjectBindingSource.Filter = "[Active] = 1"
        End If
        _MyProjectDataAdaptor.Fill(_MyTestEquipmentTable)
    End Sub
    
    Private Sub frmAdd_Edit_ProjectToDatabase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        'get the Current User, needed to determine edit/access rignts
        _User = frmFailureBrowser._CurrentUser
        'Create and fill data adaptor
        _MyTestEquipmentTable.Clear()
        _MyProjectDataAdaptor = New SqlDataAdapter("SELECT * From [Project] ", frmFailureBrowser.gMyMeterSpecDBConnection) 'Where [Active] = 1 ORDER BY [Number] Desc
        _MyProjectDataAdaptor.Fill(_MyTestEquipmentTable)

        'create and assigne binding source
        _MyProjectBindingSource = New BindingSource
        _MyProjectBindingSource.DataSource = _MyTestEquipmentTable
        _MyProjectBindingSource.Filter = "[Active] = 1"
        _MyProjectBindingSource.Sort = "[Number] DESC"
        'Set the gridview to the binding source
        dgvMyProject.DataSource = _MyProjectBindingSource

        'create automatic Queries
        'Create the commandbuilder object
        Dim DbCommandBuilder As SqlCommandBuilder = New SqlCommandBuilder(_MyProjectDataAdaptor)
        'BindingNavigatorTestEquipment.Enabled = True
        'This is necessary since some of the column names have spaces in them
        DbCommandBuilder.QuotePrefix = "["
        DbCommandBuilder.QuoteSuffix = "]"

        'Create Update, Delete, and Insert commands
        _MyProjectDataAdaptor.UpdateCommand = DbCommandBuilder.GetUpdateCommand
        _MyProjectDataAdaptor.DeleteCommand = DbCommandBuilder.GetDeleteCommand
        _MyProjectDataAdaptor.InsertCommand = DbCommandBuilder.GetInsertCommand

        'Use the custom bindingtracket class to create databindings for all of the controls
        'init Binding Record
        _MyProjectBindingRecord = New List(Of cCustomDataBaseAccess.cDataBindingTracker)
        _MyProjectBindingRecord.Clear()
        'Que up bindings ofr controls here

        'Text, Maskedtext, and Richtext boxes

        _MyProjectBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProjectName, "Text", "NAME", _MyProjectBindingSource, _MyTestEquipmentBinding))
        _MyProjectBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProjectNumber, "Text", "NUMBER", _MyProjectBindingSource, _MyTestEquipmentBinding))


        'checkboxes
        _MyProjectBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkProjectActive, "CheckState", "Active", _MyProjectBindingSource, _MyTestEquipmentBinding))

        'Excute the bindings to the database....

        'Call the function the creates the bindings
        _MyDatabaseaccess.BindControls(_MyProjectBindingRecord)

        dgvMyProject.AutoResizeColumns()
        dgvMyProject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub

    Private Sub frmAdd_Edit_ProjectToDatabase_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Initialize  the dialog result whenever it is shown...
        Me.DialogResult = Windows.Forms.DialogResult.None
        SetEditState(eProjectDialogEditState.ReadOnlyView)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Call the close event...The Close event will will be canceled and the form hidden instead
        If MsgBox("Changes will lost, are you sure you want to cancel and retrun to the Report?", MsgBoxStyle.YesNo, "Discard Changes?") = MsgBoxResult.No Then
            Me.DialogResult = Windows.Forms.DialogResult.None
        ElseIf Me.DialogResult <> Windows.Forms.DialogResult.Cancel Then
            Try
                CancleEdit()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                gSelectedProjectID = Nothing
                Me.Hide()
            Catch ex As Exception
                MsgBox("Error Canceling changes." + vbCrLf + ex.ToString)
            End Try
        End If
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtProjectNumber.Text.Trim <> "" Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            gSelectedProjectID = txtProjectNumber.Text.Trim
            gSelectedProjectName = txtProjectName.Text.Trim
            Me.Hide()
        Else
            MsgBox("A valid project must be selected before clicking 'OK'.")
        End If
    End Sub

    Sub SetProjectDialogTextBoxState(ByVal Enabled As Boolean, ByVal MyColor As Color, Optional ByVal Visible As Boolean = True)

        'if enabled then Turn ReadOnly
        txtProjectName.ReadOnly = Not Enabled
        txtProjectName.BackColor = MyColor
        txtProjectName.Visible = Visible

        txtProjectNumber.ReadOnly = Not Enabled
        txtProjectNumber.BackColor = MyColor
        txtProjectNumber.Visible = Visible


    End Sub

    Sub SetEditState(ByVal NextState As eProjectDialogEditState)
        'Me.Hide()
        Me.DoubleBuffered = True
        _SafeToProcessEvents = False 'inhibit events as controls change states...

        'Admin, PowerUser, Lab Tech have Edit rights
        If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Or _User.AccessLevel = frmFailureBrowser.eAccessState.CREATE_NEW Then
            EditToolStripMenuItem.Visible = True

            If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
                'Full access
                tsmNew.Visible = True
                tsmAdminEdit.Visible = True
                tsmEdit.Visible = True
            ElseIf _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
                'Full trackable Access
                tsmNew.Visible = True
                tsmAdminEdit.Visible = False
                tsmEdit.Visible = True
            ElseIf _User.AccessLevel = frmFailureBrowser.eAccessState.CREATE_NEW Then
                'Revise Calibration Information
                tsmNew.Visible = False
                tsmAdminEdit.Visible = False
                tsmEdit.Visible = True
            End If
        Else
            EditToolStripMenuItem.Visible = False
        End If


        Select Case NextState
            Case eProjectDialogEditState.ReadOnlyView
                SetReadonlyView()
                _CurrentState = NextState
            Case eProjectDialogEditState.NewProject
                If SetNewProjectStateView() = True Then
                    _CurrentState = NextState
                End If
            Case eProjectDialogEditState.Edit
                If SetEditProjectStateView() = True Then
                    _CurrentState = NextState
                End If

            Case eProjectDialogEditState.AdminEdit
                If SetAdminEditProjectStateView() = True Then
                    _CurrentState = NextState
                End If
            Case Else

        End Select
    End Sub

    ''' <summary>
    ''' This Subroutine sets the Control Edit state for readonly access
    ''' </summary>
    ''' <remarks>Frank Boudreau 5.2.2019</remarks>
    Function SetReadonlyView() As Boolean
        Dim bSuccess As Boolean = True
        Try
            tsmNew.Enabled = True
            tsmAdminEdit.Enabled = True
            tsmSave.Enabled = False
            tsmSaveAndExit.Enabled = False
            tsmCancelEdit.Enabled = False
            tsmEdit.Enabled = True
            btnSave.Visible = False
            btnDeleteProject.Visible = False
            btnOK.Visible = True

            Dim EnableState As Boolean = False
            Dim Mycolor As System.Drawing.Color = _ColorNonEditableBackGround

            SetProjectDialogTextBoxState(EnableState, Mycolor, True)

            If chkProjectActive IsNot Nothing Then
                chkProjectActive.AutoCheck = EnableState
                chkProjectActive.BackColor = Mycolor
            End If


            ' dgvMyProject.Columns("ID").Width = 1
            dgvMyProject.Columns("ID").DisplayIndex = dgvMyProject.Columns.Count - 1
            dgvMyProject.Enabled = True
            dgvMyProject.ReadOnly = True
            'done so set current state
            Me.Text = "Select Project"
        Catch ex As Exception
            MsgBox("Unknown Error Setting Radonly View" + vbCrLf + ex.ToString)
            bSuccess = False
        End Try
        Return bSuccess
    End Function

    ''' <summary>
    ''' This Subroutine sets the Control Edit state to Create a new project and adds a row to the database
    ''' </summary>
    ''' <remarks>Frank Boudreau 5.2.2019</remarks>
    Function SetNewProjectStateView() As Boolean
        Dim bSuccess As Boolean = True
        Try


            tsmNew.Enabled = False
            tsmAdminEdit.Enabled = False
            tsmSave.Enabled = True
            tsmSaveAndExit.Enabled = True
            tsmCancelEdit.Enabled = True
            tsmEdit.Enabled = False
            btnSave.Visible = True
            btnOK.Visible = False
            'Change to Edit View
            Dim EnableState As Boolean = True
            Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

            'Confirm that the current state is read onyl before crating a new Row and addign it to the database...
            If _CurrentState = eProjectDialogEditState.ReadOnlyView Then

                'Create a new row in the Failure Report data table - This is handled in state machine
                _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())

                'set any default values
                _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active") = True

                'end edit on all binding sources to create new record
                _MyProjectBindingSource.EndEdit()

                'Force a control Validation
                Me.Validate()

                'This Sends the changes out to the remote database
                _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)

                'clear contents of local datatable
                _MyTestEquipmentTable.Clear()

                'Resyncronize local datatable
                _MyProjectDataAdaptor.Fill(_MyTestEquipmentTable)

                'set the position at the new failure report
                _MyProjectBindingSource.Position = _MyTestEquipmentTable.Rows.Count - 1


                SetProjectDialogTextBoxState(EnableState, Mycolor, True)

                'Assume that this is the active Revision, however it is readonly... only set this programicly...
                If chkProjectActive IsNot Nothing Then
                    chkProjectActive.AutoCheck = False
                    chkProjectActive.BackColor = Mycolor
                    chkProjectActive.Checked = True
                End If


                'Put Primary key at the end
                dgvMyProject.Columns("ID").DisplayIndex = dgvMyProject.Columns.Count - 1
                dgvMyProject.Enabled = False
                Me.Text = "Edit Project List - Add New Project"
            End If
        Catch ex As Exception
            bSuccess = False
            MsgBox("Error Adding new Record to Project table in database" + vbCrLf + ex.ToString)
            SetReadonlyView()
        End Try

        Return bSuccess
    End Function
    ''' <summary>
    ''' This Subroutine sets the Control Edit state to Edit an existing
    ''' </summary>
    ''' <remarks>Frank Boudreau 5.2.2019</remarks>
    Function SetEditProjectStateView() As Boolean
        Dim bSuccess As Boolean = True
        'Change to Edit View
        Dim EnableState As Boolean = True
        Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

        Try
            'Confirm that the current state is read only before crating a new Row and addign it to the database...
            If _CurrentState = eProjectDialogEditState.ReadOnlyView Then

                tsmNew.Enabled = False
                tsmAdminEdit.Enabled = False
                tsmSave.Enabled = True
                tsmSaveAndExit.Enabled = True
                tsmCancelEdit.Enabled = True
                tsmEdit.Enabled = False
                btnSave.Visible = True
                btnOK.Visible = False

                'Enable to the checkboxes
                SetProjectDialogTextBoxState(EnableState, Mycolor, True)

                chkProjectActive.AutoCheck = True
                chkProjectActive.BackColor = Mycolor

                'Put Primary key at the end
                dgvMyProject.Columns("ID").DisplayIndex = dgvMyProject.Columns.Count - 1
                dgvMyProject.Enabled = False
                Me.Text = "Edit Project List"
            End If
        Catch ex As Exception
            bSuccess = False
            MsgBox("Error going to edit view" + vbCrLf + ex.ToString)
            SetReadonlyView()
        End Try

        Return bSuccess
    End Function
    ''' <summary>
    ''' This Subroutine sets the Control Edit state to Edit an existing
    ''' </summary>
    ''' <remarks>Frank Boudreau 5.2.2019</remarks>
    Function SetAdminEditProjectStateView() As Boolean
        Dim bSuccess As Boolean = True
        'Change to Edit View
        Dim EnableState As Boolean = True
        Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

        Try
            'Confirm that the current state is read only before crating a new Row and addign it to the database...
            If _CurrentState = eProjectDialogEditState.ReadOnlyView Then

                tsmNew.Enabled = False
                tsmAdminEdit.Enabled = False
                tsmSave.Enabled = True
                tsmSaveAndExit.Enabled = True
                tsmCancelEdit.Enabled = True
                tsmEdit.Enabled = False
                btnSave.Visible = True
                btnDeleteProject.Visible = True
                btnOK.Visible = False
                'Enable to the checkboxes
                SetProjectDialogTextBoxState(EnableState, Mycolor, True)

                chkProjectActive.AutoCheck = True
                chkProjectActive.BackColor = Mycolor

                'Put Primary key at the end
                dgvMyProject.Columns("ID").DisplayIndex = dgvMyProject.Columns.Count - 1
                dgvMyProject.Enabled = True
                Me.Text = "Admin Edit Project List"
            End If
        Catch ex As Exception
            bSuccess = False
            MsgBox("Error going to edit view" + vbCrLf + ex.ToString)
            SetReadonlyView()
        End Try

        Return bSuccess
    End Function
    'Private Function SetTestEquipmentState(ByVal NextState As eProjectDialogEditState) As Boolean

    '    Try



    '        Select Case NextState
    '            Case eProjectDialogEditState.ReadOnlyView 
    '                ' cbTestEquipmentTypeFilter.Enabled = True

    '            Case eProjectDialogEditState.CreateNew
    '                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
    '                    ' cbTestEquipmentTypeFilter.Enabled = False

    '            Case eTestEquipmentState.CopyDevice
    '                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then

    '                    cbTestEquipmentTypeFilter.Enabled = False

    '                    'Change to Edit View
    '                    tsmNew.Enabled = False
    '                    tsmCopy.Enabled = False
    '                    tsmAdminEdit.Enabled = False
    '                    tsmSave.Enabled = True
    '                    tsmSaveAndExit.Enabled = True
    '                    tsmCancel.Enabled = True
    '                    tsmRevise.Enabled = False
    '                    tsmObsolete.Enabled = False
    '                    If chkTestEquipmentIsTestGroup.Checked Then
    '                        AddTestGroupMembersToolStripMenuItem.Enabled = True
    '                    End If

    '                    'Change to Edit View
    '                    Dim EnableState As Boolean = True
    '                    Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

    '                    If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then



    '                        'Query Max and Min ID
    '                        Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", Nothing)
    '                        Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", Nothing)
    '                        Dim CurrentRowIndex As Integer

    '                        If dgvTestEquipment.CurrentRow Is Nothing Then
    '                            MsgBox("No Testequipment detected to copy")
    '                            Return False
    '                        Else
    '                            CurrentRowIndex = dgvTestEquipment.CurrentRow.Index
    '                        End If



    '                        'Create a new row in the Failure Report data table - This is handled in state machine
    '                        _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())

    '                        'increment the NEW ID in the data table and set any default values
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("ID") = (MaxID + 1).ToString
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active Rev") = True
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = 0
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Obsolete") = False
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = ""
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group Members")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Manufacturer")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Model")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Cal Req")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = ""
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Description")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Type")

    '                        'increment the position of the binding source (i'm not sure I need this)
    '                        _MyTestEquipmentBindingSource.Position = MaxID + 1

    '                        'end edit on all binding sources to create new record
    '                        _MyTestEquipmentBindingSource.EndEdit()

    '                        'Force a control Validation
    '                        Me.Validate()

    '                        'This Sends the changes out to the remote database
    '                        _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

    '                        'clear contents of local datatable
    '                        _MyTestEquipmentTable.Clear()

    '                        'Resyncronize local datatable
    '                        _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)

    '                        'Sort the data 
    '                        dgvTestEquipment.Sort(dgvTestEquipment.Columns("ID"), System.ComponentModel.ListSortDirection.Descending)

    '                        'set the position at the new failure report
    '                        _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", MaxID + 1)
    '                    End If

    '                    SetTestEquipmentInfoControlState(EnableState, Mycolor, True)
    '                    SetCalibrationControlState(EnableState, Mycolor, True)
    '                    SetObsoleteControlState(EnableState, Mycolor, False)
    '                    SetTestGroupControlstate(EnableState, Mycolor, chkTestEquipmentIsTestGroup.Checked)



    '                    'Assume that this is the active Revision, however it is readonly... only set this programicly...
    '                    If chkActiveRevision IsNot Nothing Then
    '                        chkActiveRevision.AutoCheck = False
    '                        chkActiveRevision.BackColor = Mycolor
    '                        chkActiveRevision.Checked = True
    '                    End If

    '                    'Assume that this is the active Revision, however it is readonly... only set this programicly...
    '                    If chkTestEquipmentCalReq IsNot Nothing Then
    '                        chkActiveRevision.AutoCheck = False
    '                        chkTestEquipmentCalReq.BackColor = Mycolor
    '                    End If

    '                    'assume that it is not obslolete so it is not visible or enabled for editing
    '                    If chkTestEquipmentObsolete IsNot Nothing Then
    '                        chkTestEquipmentObsolete.AutoCheck = Not EnableState
    '                        chkTestEquipmentObsolete.BackColor = Mycolor
    '                        chkTestEquipmentObsolete.Visible = False
    '                    End If

    '                    dgvTestEquipment.Columns("Index").Width = 1
    '                    dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
    '                    dgvTestEquipment.Enabled = False

    '                    Me.Text = "Edit Test Equipment - Add New Device (Copy)"
    '                    _CurrentState = NextState

    '                End If

    '            Case eTestEquipmentState.ObsoleteDevice

    '                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
    '                    'Change to Edit View
    '                    cbTestEquipmentTypeFilter.Enabled = False
    '                    tsmNew.Enabled = False
    '                    tsmCopy.Enabled = False
    '                    tsmAdminEdit.Enabled = False
    '                    tsmSave.Enabled = True
    '                    tsmSaveAndExit.Enabled = True
    '                    tsmCancel.Enabled = True
    '                    tsmRevise.Enabled = False
    '                    tsmObsolete.Enabled = False

    '                    ' If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then
    '                    'Change to Edit View
    '                    Dim Enabled As Boolean = True
    '                    Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround
    '                    'Set Obsolete Flag
    '                    SetObsoleteControlState(Enabled, Mycolor, False)

    '                    'must be set using code...
    '                    chkTestEquipmentObsolete.Checked = True
    '                    chkTestEquipmentObsolete.AutoCheck = False
    '                    chkTestEquipmentObsolete.Visible = True
    '                    lblObsoleteDate.Visible = True
    '                    txtTestEquipmentObsoleteDate.Visible = True
    '                    dtpTestEquipmentObsoleteDate.Visible = True
    '                    dtpTestEquipmentObsoleteDate.Enabled = True
    '                    txtTestEquipmentObsoleteDate.BackColor = _ColorEditableBackGround

    '                    'Allow the user to edit the Note as well...
    '                    txtTestEquipmentNote.ReadOnly = False
    '                    txtTestEquipmentNote.BackColor = _ColorEditableBackGround
    '                    'Show the Obsolete date Txt and Dtp
    '                    '   End If

    '                    'Assume that this is the active Revision, however it is readonly... only set this programicly...
    '                    If chkActiveRevision IsNot Nothing Then
    '                        chkActiveRevision.AutoCheck = False
    '                        chkActiveRevision.BackColor = _ColorNonEditableBackGround 'not editable when obseleting a piece of equipment....
    '                    End If

    '                    Me.Text = "Edit Test Equipment - Obsolete Current Device"

    '                    dgvTestEquipment.Columns("Index").Width = 1
    '                    dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
    '                    dgvTestEquipment.Enabled = False
    '                    _CurrentState = NextState
    '                End If
    '            Case eTestEquipmentState.ReviseCalDevice

    '                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Or _User.AccessLevel = frmFailureBrowser.eAccessState.CREATE_NEW Then
    '                    'Copy active Device to buffer
    '                    'Change to Edit View 
    '                    If chkTestEquipmentCalReq.Checked = False Then
    '                        MsgBox("Selected Test Equipment Does not Require Calibration" + vbCrLf + "Admin Rights Required to Edit, Aborting")
    '                        Return False
    '                    End If
    '                    'Change to Edit View
    '                    cbTestEquipmentTypeFilter.Enabled = False
    '                    Dim EnabledState As Boolean = True
    '                    Dim MyEditcolor As System.Drawing.Color = _ColorEditableBackGround
    '                    Dim MyNonEditableColor As System.Drawing.Color = Panel2.BackColor
    '                    'Change to Edit View
    '                    tsmNew.Enabled = False
    '                    tsmCopy.Enabled = False
    '                    tsmAdminEdit.Enabled = False
    '                    tsmSave.Enabled = True
    '                    tsmSaveAndExit.Enabled = True
    '                    tsmCancel.Enabled = True
    '                    tsmRevise.Enabled = False
    '                    tsmObsolete.Enabled = False

    '                    If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then
    '                        'Query Max and Min ID
    '                        Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", "[Active Rev] = True OR [Active REV] = " + (Not chkTestEquipmentShowInactiveRev.Checked).ToString)
    '                        Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", "[Active Rev] = True OR [Active REV] = " + (Not chkTestEquipmentShowInactiveRev.Checked).ToString)
    '                        Dim CurrentRowIndex As Integer

    '                        If dgvTestEquipment.CurrentRow Is Nothing Then
    '                            MsgBox("No Testequipment Selected to Revise")
    '                            Return False
    '                        Else
    '                            CurrentRowIndex = dgvTestEquipment.CurrentRow.Index
    '                            Dim MyObject As Object = dgvTestEquipment.CurrentRow.Cells("Index").Value
    '                            'The value returned below will depend on the current values in the Datagrid...
    '                            CurrentRowIndex = _MyTestEquipmentBindingSource.Find("Index", dgvTestEquipment.CurrentRow.Cells("Index").Value)
    '                        End If


    '                        Dim myobject2 As Object = _MyTestEquipmentTable.Rows(0)("ID")

    '                        'Create a new row in the Failure Report data table - This is handled in state machine
    '                        _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())
    '                        Dim myobject3 As Object = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Id")
    '                        'pass out of function in case of cancel
    '                        _RecordToBeRevisedIndex = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Index").Value

    '                        'increment the NEW ID in the data table and set any default values
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("ID") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("ID").Value

    '                        dgvTestEquipment.Rows(CurrentRowIndex).Cells("Active Rev").Value = False

    '                        'Since this data has been copied
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active Rev") = True 'It is the new active record...

    '                        Try
    '                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Rev").Value + 1
    '                        Catch ex As Exception
    '                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = 1
    '                        End Try

    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Obsolete") = False

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Lab ID")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Lab ID").Value

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Test Group").Value

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group Members")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Test Group Members").Value

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Manufacturer")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Manufacturer").Value

    '                        ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Model")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Model").Value

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Serial Number")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Serial Number").Value

    '                        ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Description")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Description").Value

    '                        '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Type")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Type").Value

    '                        ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Cal Req")
    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Cal Req").Value

    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Last Cal") = ""

    '                        _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Next Cal") = ""

    '                        'end edit on all binding sources to create new record
    '                        _MyTestEquipmentBindingSource.EndEdit()

    '                        ''Not  sure Ineed this either - FJB
    '                        '  Me.EndEditOnAllBindingSources()

    '                        'Force a control Validation
    '                        Me.Validate()

    '                        'This Sends the changes out to the remote database
    '                        _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

    '                        'clear contents of local datatable
    '                        _MyTestEquipmentTable.Clear()

    '                        'Resyncronize local datatable
    '                        _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)

    '                        'Query Max and Min ID
    '                        Dim MaxIndex As Integer = _MyTestEquipmentTable.Compute("MAX([Index])", Nothing)


    '                        'filter By ID then Index
    '                        _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable
    '                        _MyTestEquipmentBindingSource.Sort = Nothing
    '                        'MsgBox(_MyTestEquipmentBindingSource.SupportsAdvancedSorting.ToString) 'for test
    '                        _MyTestEquipmentBindingSource.Sort = "ID DESC, Index DESC"
    '                        dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource

    '                        'set the position at the new failure report
    '                        _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", MaxIndex)



    '                    End If

    '                    SetTestEquipmentInfoControlState(Not EnabledState, MyNonEditableColor, True)
    '                    SetCalibrationControlState(EnabledState, MyEditcolor, True)
    '                    SetObsoleteControlState(Not EnabledState, MyNonEditableColor, False)
    '                    SetTestGroupControlstate(Not EnabledState, MyNonEditableColor, True)

    '                    'Assume that this is the active Revision, however it is readonly... only set this programicly...
    '                    If chkActiveRevision IsNot Nothing Then
    '                        chkActiveRevision.AutoCheck = False 'Override...do not allow to change...only revision date editing allowed...
    '                        chkActiveRevision.BackColor = MyEditcolor
    '                        chkActiveRevision.Checked = True
    '                    End If


    '                    'Allow the user to edit the Note as well...
    '                    txtTestEquipmentNote.ReadOnly = False
    '                    txtTestEquipmentNote.BackColor = _ColorEditableBackGround
    '                    dgvTestEquipment.Columns("Index").Width = 1
    '                    dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
    '                    dgvTestEquipment.Enabled = False


    '                    Me.Text = "Edit Test Equipment - Update Calibration Date"
    '                    _CurrentState = NextState
    '                    'Copy Buffer to New Record
    '                    'increment Revision
    '                    'Set Active Revision Flag
    '                End If

    '            Case eTestEquipmentState.AdminEditDevice

    '                'Cannot enter EDIT state without rights....
    '                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
    '                    cbTestEquipmentTypeFilter.Enabled = False
    '                    'Change to Edit View
    '                    tsmNew.Enabled = False
    '                    tsmCopy.Enabled = False
    '                    tsmAdminEdit.Enabled = False
    '                    tsmSave.Enabled = True
    '                    tsmSaveAndExit.Enabled = True
    '                    tsmCancel.Enabled = True
    '                    tsmRevise.Enabled = False
    '                    tsmObsolete.Enabled = False
    '                    'Change to Edit View
    '                    Dim EnabledState As Boolean = True
    '                    Dim MyEditcolor As System.Drawing.Color = _ColorEditableBackGround
    '                    Dim MyNonEditableColor As System.Drawing.Color = Panel2.BackColor


    '                    SetTestEquipmentInfoControlState(EnabledState, MyEditcolor, True)
    '                    SetCalibrationControlState(EnabledState, MyEditcolor, True)
    '                    SetObsoleteControlState(EnabledState, MyEditcolor, True)
    '                    SetTestGroupControlstate(EnabledState, MyEditcolor, True)
    '                    dgvTestEquipment.Columns("Index").Width = 1
    '                    dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
    '                    dgvTestEquipment.Enabled = False
    '                    ' End If

    '                    _CurrentState = NextState
    '                End If

    '        End Select



    '        Me.Show()
    '        _SafeToProcessEvents = True
    '        Return True

    '    Catch ex As Exception
    '        'silently handle for now 
    '        MsgBox("Error Encounted during state change" + vbCrLf + ex.ToString)
    '        _SafeToProcessEvents = True 'reenable events
    '        Me.Show()
    '        Return False

    '    End Try



    'End Function
    ''' <summary>
    ''' This function verfies that all of the project information has been filled in
    ''' </summary>
    ''' <returns>True if Validated; False otherwise</returns>
    ''' <remarks>Frank Boudreau 5.20.2019 </remarks>
    Private Function ValidateTestEquipmentInformation() As Boolean
        Dim ReturnValue As Boolean = True 'Assume Success
        Dim ErrorMessage As String = "Check Required Information"
        'Check that all fields have been filled in
        If _CurrentState = eProjectDialogEditState.NewProject Then

            'check and make sure that all information has been filled in
            If txtProjectName.Text.Trim = "" Then
                'MsgBox("Project Name Must be completed")
                txtProjectName.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Project Name Must be completed."
                ReturnValue = False
            End If

            'check and make sure that all information has been filled in
            If txtProjectNumber.Text.Trim = "" Then
                txtProjectNumber.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Project Number Must be completed"
                ReturnValue = False
            End If
            'make sure the Project number is unique
            _MyDatabaseaccess.SQLCon = frmFailureBrowser.gMyMeterSpecDBConnection
            Dim TableName As String = "Project"
            Dim ColumnList As List(Of String) = New List(Of String)
            Dim ColumnValues As List(Of String) = New List(Of String)
            ColumnList.Add("Number")
            ColumnList.Add("Active")
            ColumnValues.Add("'" + txtProjectNumber.Text.Trim + "'")
            ColumnValues.Add("1")

            If (Not _MyDatabaseaccess.IsUnique(TableName, ColumnList, ColumnValues)) Then
                If MsgBox("Project Number allready exists in database, Add anyway?", MsgBoxStyle.YesNo, "Verify Project Number") = MsgBoxResult.Yes Then

                Else
                    txtProjectNumber.SelectAll()
                    ReturnValue = False
                End If

            End If

            If ReturnValue = False Then
                MsgBox("Please check to make sure that all Required fields have been completed accurately")
            Else
                'all new records are active after being created
                If chkProjectActive.Checked = False Then
                    chkProjectActive.Checked = True
                End If
            End If
        End If
        Return ReturnValue
    End Function

    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        SetEditState(eProjectDialogEditState.NewProject)
    End Sub

    Private Sub tsmSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmSave.Click

        If ValidateTestEquipmentInformation() = True Then
            Try
                ' end edit on all binding sources
                _MyProjectBindingSource.EndEdit()
                ' Force a control Validation not sure if this is needed - FJB
                Me.Validate()
                ' This Sends the changes out to the remote database
                _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)
                'Refresh View...
                RefreshProjectList()
            Catch ex As Exception
                MsgBox("Error Saving record to database" + vbCrLf + ex.ToString)
            End Try
        End If

    End Sub

    Private Sub tsmSaveAndExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmSaveAndExit.Click
        If ValidateTestEquipmentInformation() = True Then


            Try
                ' end edit on all binding sources
                _MyProjectBindingSource.EndEdit()
                ' Force a control Validation not sure if this is needed - FJB
                Me.Validate()
                ' This Sends the changes out to the remote database
                _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)
                RefreshProjectList()
            Catch ex As Exception
                MsgBox("Error Saving Record to Database" + vbCrLf + ex.ToString)
                Exit Sub
            End Try

            Try
                SetEditState(eProjectDialogEditState.ReadOnlyView)
            Catch ex As Exception
                MsgBox("Error Setting Read Only State" + vbCrLf + ex.ToString)
            End Try
        End If
    End Sub

    Private Sub CancleEdit()
        If _CurrentState = eProjectDialogEditState.NewProject Then
            Try
                'Query Max and Min ID
                Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", Nothing)
                Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", Nothing)

                'database call and delete
                'Varaible to hold the current position, default to 1...
                Dim CurrentID As Integer = 1

                'point CurrentRow at the Bound Row in the data set, this should always be the new 
                'row that was added because the navigation of the form should be locked out
                'i.e. - the gridview is readonly, the Navigation buttons are disabled
                If dgvMyProject.CurrentRow Is Nothing Then
                    ' _MyProjectBindingSource.Position = _MyProjectBindingSource.Find("ID", CurrentID)
                    _MyProjectBindingSource.Position = 1
                    SetEditState(eProjectDialogEditState.ReadOnlyView)
                    Exit Sub
                Else
                    'This presumes that the row to be removed has been added and is currently selected***How to verfy this
                    _MyProjectBindingSource.Position = dgvMyProject.CurrentRow.Index
                End If


                'Cast the new row from the binding source to DataRowView to ease minipulation 
                Dim CurrentRow As DataRowView = DirectCast(_MyProjectBindingSource.Current, DataRowView)
                CurrentRow.Delete()

                'Cancel edit for now
                _MyProjectBindingSource.EndEdit()
                _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)

                'Set to most recently added failure report
                _MyProjectBindingSource.Position = _MyProjectBindingSource.Position = _MyTestEquipmentTable.Rows.Count - 1

                SetEditState(eProjectDialogEditState.ReadOnlyView)
            Catch ex As Exception
                MsgBox("Error Canceling Edit" + vbCrLf + ex.ToString)
            End Try
        ElseIf _CurrentState = eProjectDialogEditState.Edit Then

            _MyProjectBindingSource.CancelEdit()
            _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)
            SetEditState(eProjectDialogEditState.ReadOnlyView)

        ElseIf _CurrentState = eProjectDialogEditState.AdminEdit Then
            _MyProjectBindingSource.CancelEdit()
            _MyProjectDataAdaptor.Update(_MyTestEquipmentTable)
            SetEditState(eProjectDialogEditState.ReadOnlyView)

        Else
            'for now
            'Do nothing
        End If
    End Sub

    Private Sub tsmCancelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmCancelEdit.Click
        If MsgBox("Changes will lost, are you sure you want to Cancel Editing?", MsgBoxStyle.YesNo, "Discard Changes?") = MsgBoxResult.Yes Then
            CancleEdit()
        End If

    End Sub

    Private Sub dgvMyProject_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvMyProject.SelectionChanged
        Dim i As Integer = 0
        Dim index As Integer = 0
        Dim RowsSelected = False

        For Each row As DataGridViewRow In dgvMyProject.SelectedRows
            If row.IsNewRow Then
                Exit Sub
            End If
            i += 1
            RowsSelected = True
            index = row.Index
        Next


        'if we get tot here then there is no selected rows...
        btnOK.Enabled = RowsSelected
    End Sub


    Private Sub tsmAdminEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmAdminEdit.Click
        SetEditState(eProjectDialogEditState.AdminEdit)
    End Sub

    Private Sub tsmEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmEdit.Click
        SetEditState(eProjectDialogEditState.Edit)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshProjectList()
    End Sub

    Private Sub chkShowInactiveProjects_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowInactiveProjects.CheckedChanged
        RefreshProjectList()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'CHeat!
        tsmSave.PerformClick()

    End Sub
End Class