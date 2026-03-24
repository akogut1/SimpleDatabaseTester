
Imports System.Windows.Forms
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class frmSelectTestEquipment
    Dim _MyDatabaseaccess As New cCustomDataBaseAccess
    Dim _MyTestEquipmentBindingSource As BindingSource
    Dim _MyUsersBindingSource As BindingSource '...Bindingsource to track User ForeignKey...
    Dim _MyTestEquipmentDataAdaptor As SqlDataAdapter
    Dim _MyUsersDataAdaptor As SqlDataAdapter
    Public gSelectedTestEquipment As DataTable
    Dim _TestEquipmentBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker)
    Dim _UsersBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker) '...binding Record to help create foreign key relationship...
    Public WithEvents _MyTestEquipmentBinding As Binding
    Public _MyFilter As String = ""
    Dim _MyTestEquipmentTable As New DataTable
    Dim _MyUsersTable As New DataTable
    Dim _CurrentState As eTestEquipmentState
    Dim _ColorNonEditableBackGround As System.Drawing.Color = System.Drawing.Color.LightYellow
    Dim _ColorEditableBackGround As System.Drawing.Color = System.Drawing.Color.White
    Dim _RecordToBeRevisedIndex As Integer 'This variable is used to keep track of which record is being revised.  In the event the user choose to cancel 
    Dim _SafeToProcessEvents As Boolean = False 'inhibit control envents until after the form has loaded and has been shown
    Dim _Delimiter As String = ";"
    Dim _User As frmFailureBrowser.User

    'The orignal record should be restored to an active state.
    Enum eTestEquipmentState
        ReadOnlyViewDevice
        NewDevice
        CopyDevice
        ReviseCalDevice
        AdminEditDevice
        ObsoleteDevice
    End Enum
#Region "Control States"
    Private Function SetViewState() As Boolean
        Try

            readonlycbTestEquipmentType.ReadOnly = False
            readonlycbTestEquipmentType.DropDownStyle = ComboBoxStyle.DropDown
            readonlycbTestEquipmentType.BackColor = Color.White

            For Each Control In panelTestEquipmentRecord.Controls
                If Control.name = txtTestEquipmentID.Name Then
                    'do nothing this field is never editable by the user...
                ElseIf TypeOf Control Is TextBox Then
                    Dim MyTextbox As TextBox = DirectCast(Control, TextBox)

                    MyTextbox.ReadOnly = False
                    MyTextbox.BackColor = Color.White

                ElseIf TypeOf Control Is MaskedTextBox Then
                    Dim MyTextbox As MaskedTextBox = DirectCast(Control, MaskedTextBox)
                    If MyTextbox.Name = txtTestEquipmentLastCalDate.Name Or MyTextbox.Name = txtTestEquipmentNextCalDate.Name Then
                        MyTextbox.ReadOnly = True ' Keep it read only, only the datapicker can be used for entering a date...
                    Else
                        MyTextbox.ReadOnly = False
                    End If
                    MyTextbox.BackColor = Color.White
                ElseIf TypeOf Control Is RichTextBox Then
                    Dim MyTextbox As RichTextBox = DirectCast(Control, RichTextBox)
                    MyTextbox.ReadOnly = False
                    MyTextbox.BackColor = Color.White
                ElseIf TypeOf Control Is DateTimePicker Then
                    Dim MyDTP As DateTimePicker = DirectCast(Control, DateTimePicker)
                    MyDTP.Visible = True
                    MyDTP.BackColor = Color.White
                ElseIf TypeOf Control Is GroupBox Then
                    Dim MyControl As GroupBox = DirectCast(Control, GroupBox)
                    For Each GroupBoxControl In MyControl.Controls
                        If TypeOf GroupBoxControl Is TextBox Then
                            Dim MyTextbox As TextBox = DirectCast(GroupBoxControl, TextBox)
                            MyTextbox.ReadOnly = False
                            MyTextbox.BackColor = Color.White
                        ElseIf TypeOf GroupBoxControl Is Button Then
                            Dim MyButton As Button = DirectCast(GroupBoxControl, Button)
                            MyButton.Enabled = True
                        End If
                    Next
                ElseIf TypeOf Control Is CheckBox Then
                    Dim MyControl As CheckBox = DirectCast(Control, CheckBox)
                    MyControl.AutoCheck = False
                End If

            Next
            dgvTestEquipment.Enabled = False
            Return True
        Catch ex As Exception
            MsgBox("Error Setting State" + vbCrLf + ex.ToString)
            Return False
        End Try
    End Function
    Sub SetObsoleteControlState(Enabled As Boolean, MyColor As Color, Optional Visible As Boolean = True)
        If Visible Then

            txtTestEquipmentObsoleteDate.ReadOnly = Not Enabled
            txtTestEquipmentObsoleteDate.BackColor = MyColor
            dtpTestEquipmentObsoleteDate.Enabled = Enabled
            'Deal with form loading issues
            If chkTestEquipmentObsolete IsNot Nothing And txtTestEquipmentObsoleteDate IsNot Nothing Then
                txtTestEquipmentObsoleteDate.Visible = chkTestEquipmentObsolete.Checked
                dtpTestEquipmentObsoleteDate.Visible = chkTestEquipmentObsolete.Checked
                lblObsoleteDate.Visible = chkTestEquipmentObsolete.Checked
                chkTestEquipmentObsolete.AutoCheck = Enabled
                chkTestEquipmentObsolete.BackColor = MyColor
                chkTestEquipmentObsolete.Visible = True
            Else
                'default
                txtTestEquipmentObsoleteDate.Visible = False
                chkTestEquipmentObsolete.AutoCheck = False
                dtpTestEquipmentObsoleteDate.Visible = False
                lblObsoleteDate.Visible = False
            End If

        Else
            txtTestEquipmentObsoleteDate.Visible = False
            chkTestEquipmentObsolete.Visible = False
            dtpTestEquipmentObsoleteDate.Visible = False
            lblObsoleteDate.Visible = False
        End If
    End Sub
    Sub SetTestGroupControlstate(Enabled As Boolean, MyColor As Color, Optional Visible As Boolean = True)

        If chkTestEquipmentIsTestGroup IsNot Nothing And gbTestEquipmentTestGroup IsNot Nothing Then
            chkTestEquipmentIsTestGroup.AutoCheck = Enabled
            chkTestEquipmentIsTestGroup.BackColor = MyColor
            gbTestEquipmentTestGroup.Visible = chkTestEquipmentIsTestGroup.Checked
            gbTestEquipmentTestGroup.Enabled = chkTestEquipmentIsTestGroup.Checked
            If chkTestEquipmentIsTestGroup.Checked Then
                btnEditTestGroup.Visible = True
                btnEditTestGroup.Enabled = Enabled
                btnTestEquipmentTestGroupUpdate.Enabled = Enabled
                btnTestEquipmentTestGroupListMembers.Enabled = chkTestEquipmentIsTestGroup.Checked
                btnTestEquipmentTestGroupUpdate.Visible = False 'chkTestEquipmentIsTestGroup.Checked
                btnTestEquipmentTestGroupListMembers.Visible = False 'chkTestEquipmentIsTestGroup.Checked
                txtTestEquipmentTestGroupMembers.ReadOnly = True 'Not Enabled 'Always read only
                txtTestEquipmentTestGroupMembers.BackColor = MyColor
                txtTestEquipmentTestGroupMembers.Enabled = chkTestEquipmentIsTestGroup.Checked
                txtTestEquipmentTestGroupMembers.Visible = chkTestEquipmentIsTestGroup.Checked
            End If
        Else
            'default
            'btnEditTestGroup.Visible = True
            'btnEditTestGroup.Enabled =
            gbTestEquipmentTestGroup.Visible = False
            gbTestEquipmentTestGroup.Enabled = False
        End If
        If Visible Then

        Else
            gbTestEquipmentTestGroup.Visible = False
            gbTestEquipmentTestGroup.Enabled = False
        End If
    End Sub
    Sub SetCalibrationControlState(Enabled As Boolean, MyColor As Color, Optional Visible As Boolean = True)
        If Visible = True Then
            dtpTestEquipLastCalDate.Enabled = Enabled
            dtpTestEquipnextCalDate.Enabled = Enabled
            If chkTestEquipmentCalReq IsNot Nothing And txtTestEquipmentLastCalDate IsNot Nothing And txtTestEquipmentNextCalDate IsNot Nothing Then
                txtTestEquipmentLastCalDate.Visible = chkTestEquipmentCalReq.Checked
                txtTestEquipmentNextCalDate.Visible = chkTestEquipmentCalReq.Checked
                dtpTestEquipLastCalDate.Visible = chkTestEquipmentCalReq.Checked
                dtpTestEquipnextCalDate.Visible = chkTestEquipmentCalReq.Checked
                lblTestEquipLastCalDate.Visible = chkTestEquipmentCalReq.Checked
                lblTestEquipNextCalDate.Visible = chkTestEquipmentCalReq.Checked
                chkTestEquipmentCalReq.AutoCheck = Enabled 'Will change state when user clicks the check box when enabled
                chkTestEquipmentCalReq.BackColor = MyColor
                chkTestEquipmentCalReq.Visible = True
                txtTestEquipmentLastCalDate.BackColor = MyColor
                txtTestEquipmentNextCalDate.BackColor = MyColor
            Else
                'default
                txtTestEquipmentLastCalDate.Visible = False
                txtTestEquipmentNextCalDate.Visible = False
                dtpTestEquipLastCalDate.Visible = False
                dtpTestEquipnextCalDate.Visible = False
                lblTestEquipLastCalDate.Visible = False
                lblTestEquipNextCalDate.Visible = False
                chkTestEquipmentCalReq.AutoCheck = False 'Will not changes state when clicked
            End If


        Else
            txtTestEquipmentLastCalDate.Visible = False
            txtTestEquipmentNextCalDate.Visible = False
            dtpTestEquipLastCalDate.Visible = False
            dtpTestEquipnextCalDate.Visible = False
            lblTestEquipLastCalDate.Visible = False
            lblTestEquipNextCalDate.Visible = False
            chkTestEquipmentCalReq.Visible = False
        End If

    End Sub
    Sub SetTestEquipmentInfoControlState(Enabled As Boolean, MyColor As Color, Optional Visible As Boolean = True)


        'always read only
        txtTestEquipmentID.ReadOnly = True
        txtTestEquipmentID.BackColor = _ColorNonEditableBackGround
        'always readonly
        txtTestEquipmentRev.ReadOnly = True
        txtTestEquipmentRev.BackColor = _ColorNonEditableBackGround

        txtTestEquipmentLocation.ReadOnly = Not Enabled
        txtTestEquipmentLocation.BackColor = MyColor

        txtTestEquipmentLabIdentifier.ReadOnly = Not Enabled
        txtTestEquipmentLabIdentifier.BackColor = MyColor

        txtTestEquipmentManufacturer.ReadOnly = Not Enabled
        txtTestEquipmentManufacturer.BackColor = MyColor

        txtTestEquipmentModel.ReadOnly = Not Enabled
        txtTestEquipmentModel.BackColor = MyColor

        txttestEquipmentSerialNumber.ReadOnly = Not Enabled
        txttestEquipmentSerialNumber.BackColor = MyColor

        readonlycbTestEquipmentType.ReadOnly = Not Enabled
        readonlycbTestEquipmentType.BackColor = MyColor


        txtTestEquipmentLastCalDate.ReadOnly = Not Enabled
        txtTestEquipmentLastCalDate.BackColor = MyColor

        txtTestEquipmentNextCalDate.ReadOnly = Not Enabled
        txtTestEquipmentNextCalDate.BackColor = MyColor

        txtTestEquipmentNote.ReadOnly = Not Enabled
        txtTestEquipmentNote.BackColor = MyColor

        txtTestEquipmentDescription.ReadOnly = Not Enabled
        txtTestEquipmentDescription.BackColor = MyColor


    End Sub
    Private Function SetTestEquipmentState(NextState As eTestEquipmentState) As Boolean

        Try
            'Me.Hide()
            Me.DoubleBuffered = True
            _SafeToProcessEvents = False 'inhibit events as controls change states...

            'Admin, PowerUser, Lab Tech have Edit rights
            If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Or _User.AccessLevel = frmFailureBrowser.eAccessState.CREATE_NEW Then
                EditToolStripMenuItem.Visible = True

                If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
                    'Full access
                    tsmNew.Visible = True
                    tsmCopy.Visible = True
                    tsmObsolete.Visible = True
                    tsmAdminEdit.Visible = True
                    tsmRevise.Visible = True
                ElseIf _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
                    'Full trackable Access
                    tsmNew.Visible = True
                    tsmCopy.Visible = True
                    tsmObsolete.Visible = True
                    tsmAdminEdit.Visible = False
                    tsmRevise.Visible = True
                Else
                    'Revise Calibration Information
                    tsmObsolete.Visible = False
                    tsmNew.Visible = False
                    tsmCopy.Visible = False
                    tsmAdminEdit.Visible = False
                    tsmRevise.Visible = True
                End If
            Else
                EditToolStripMenuItem.Visible = False
            End If

            ' DataGridView1.Columns("Index").Visible = False

            Select Case NextState
                Case eTestEquipmentState.ReadOnlyViewDevice

                    cbTestEquipmentTypeFilter.Enabled = True

                    tsmNew.Enabled = True
                    tsmCopy.Enabled = True
                    tsmAdminEdit.Enabled = True
                    tsmSave.Enabled = False
                    tsmSaveAndExit.Enabled = False
                    tsmCancel.Enabled = False
                    tsmRevise.Enabled = True
                    tsmObsolete.Enabled = True
                    AddTestGroupMembersToolStripMenuItem.Enabled = False
                    AddTestGroupMembersToolStripMenuItem.Visible = False

                    Dim EnableState As Boolean = False
                    Dim Mycolor As System.Drawing.Color = _ColorNonEditableBackGround

                    SetTestEquipmentInfoControlState(EnableState, Mycolor, True)
                    SetCalibrationControlState(EnableState, Mycolor, True)
                    SetObsoleteControlState(EnableState, Mycolor, True)
                    SetTestGroupControlstate(EnableState, Mycolor, chkTestEquipmentIsTestGroup.Checked)


                    If chkActiveRevision IsNot Nothing Then
                        chkActiveRevision.AutoCheck = EnableState
                        chkActiveRevision.BackColor = Mycolor
                    End If

                    If chkTestEquipmentObsolete IsNot Nothing Then
                        If chkTestEquipmentObsolete.Checked = True Then
                            chkTestEquipmentObsolete.AutoCheck = EnableState
                            chkTestEquipmentObsolete.BackColor = Mycolor
                            chkTestEquipmentObsolete.Visible = True
                        Else
                            chkTestEquipmentObsolete.AutoCheck = EnableState
                            chkTestEquipmentObsolete.BackColor = Mycolor
                            chkTestEquipmentObsolete.Visible = False
                        End If
                    End If
                    dgvTestEquipment.Columns("Index").Width = 1
                    dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                    dgvTestEquipment.Enabled = True
                    dgvTestEquipment.ReadOnly = True
                    'done so set current state
                    Me.Text = "Select Test Equipment"
                    _CurrentState = NextState
                Case eTestEquipmentState.NewDevice
                    If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
                        cbTestEquipmentTypeFilter.Enabled = False

                        tsmNew.Enabled = False
                        tsmCopy.Enabled = False
                        tsmAdminEdit.Enabled = False
                        tsmSave.Enabled = True
                        tsmSaveAndExit.Enabled = True
                        tsmCancel.Enabled = True
                        tsmRevise.Enabled = False
                        tsmObsolete.Enabled = False
                        If chkTestEquipmentIsTestGroup.Checked Then
                            'AddTestGroupMembersToolStripMenuItem.Enabled = True
                        End If
                        'Change to Edit View
                        Dim EnableState As Boolean = True
                        Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

                        If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then

                            'Query Max and Min ID
                            Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", Nothing)
                            Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", Nothing)

                            'Create a new row in the Failure Report data table - This is handled in state machine
                            _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())

                            'increment the NEW ID in the data table and set any default values
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("ID") = (MaxID + 1).ToString
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active Rev") = True
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = 0
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Obsolete") = False
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = False
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = True

                            'increment the position of the binding source (i'm not sure I need this)
                            _MyTestEquipmentBindingSource.Position = MaxID + 1

                            'end edit on all binding sources to create new record
                            _MyTestEquipmentBindingSource.EndEdit()

                            'Force a control Validation
                            Me.Validate()

                            'This Sends the changes out to the remote database
                            _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

                            'clear contents of local datatable
                            _MyTestEquipmentTable.Clear()

                            'Resyncronize local datatable
                            _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)

                            'Sort the data 
                            dgvTestEquipment.Sort(dgvTestEquipment.Columns("ID"), System.ComponentModel.ListSortDirection.Descending)

                            'set the position at the new failure report

                            _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", MaxID + 1)
                        End If

                        SetTestEquipmentInfoControlState(EnableState, Mycolor, True)
                        SetCalibrationControlState(EnableState, Mycolor, True)
                        SetObsoleteControlState(EnableState, Mycolor, False)
                        SetTestGroupControlstate(EnableState, Mycolor, chkTestEquipmentIsTestGroup.Checked)





                        'Assume that this is the active Revision, however it is readonly... only set this programicly...
                        If chkActiveRevision IsNot Nothing Then
                            chkActiveRevision.AutoCheck = False
                            chkActiveRevision.BackColor = Mycolor
                            chkActiveRevision.Checked = True
                        End If

                        'assume that it is not obslolete so it is not visible or enabled for editing
                        If chkTestEquipmentObsolete IsNot Nothing Then
                            chkTestEquipmentObsolete.AutoCheck = Not EnableState
                            chkTestEquipmentObsolete.BackColor = Mycolor
                            chkTestEquipmentObsolete.Visible = False
                        End If
                        dgvTestEquipment.Columns("Index").Width = 1
                        dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                        'DataGridView1.AutoScrollOffset.
                        dgvTestEquipment.Enabled = False
                        Me.Text = "Edit Test Equipment - Add New Device"
                        _CurrentState = NextState
                    End If
                Case eTestEquipmentState.CopyDevice
                    If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then

                        cbTestEquipmentTypeFilter.Enabled = False

                        'Change to Edit View
                        tsmNew.Enabled = False
                        tsmCopy.Enabled = False
                        tsmAdminEdit.Enabled = False
                        tsmSave.Enabled = True
                        tsmSaveAndExit.Enabled = True
                        tsmCancel.Enabled = True
                        tsmRevise.Enabled = False
                        tsmObsolete.Enabled = False
                        If chkTestEquipmentIsTestGroup.Checked Then
                            AddTestGroupMembersToolStripMenuItem.Enabled = True
                        End If

                        'Change to Edit View
                        Dim EnableState As Boolean = True
                        Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround

                        If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then



                            'Query Max and Min ID
                            Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", Nothing)
                            Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", Nothing)
                            Dim CurrentRowIndex As Integer

                            If dgvTestEquipment.CurrentRow Is Nothing Then
                                MsgBox("No Testequipment detected to copy")
                                Return False
                            Else
                                CurrentRowIndex = dgvTestEquipment.CurrentRow.Index
                            End If



                            'Create a new row in the Failure Report data table - This is handled in state machine
                            _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())

                            'increment the NEW ID in the data table and set any default values
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("ID") = (MaxID + 1).ToString
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active Rev") = True
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = 0
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Obsolete") = False
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = ""
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group Members")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Manufacturer")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Model")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Cal Req")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = ""
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Description")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Type")

                            'increment the position of the binding source (i'm not sure I need this)
                            _MyTestEquipmentBindingSource.Position = MaxID + 1

                            'end edit on all binding sources to create new record
                            _MyTestEquipmentBindingSource.EndEdit()

                            'Force a control Validation
                            Me.Validate()

                            'This Sends the changes out to the remote database
                            _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

                            'clear contents of local datatable
                            _MyTestEquipmentTable.Clear()

                            'Resyncronize local datatable
                            _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)

                            'Sort the data 
                            dgvTestEquipment.Sort(dgvTestEquipment.Columns("ID"), System.ComponentModel.ListSortDirection.Descending)

                            'set the position at the new failure report
                            _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", MaxID + 1)
                        End If

                        SetTestEquipmentInfoControlState(EnableState, Mycolor, True)
                        SetCalibrationControlState(EnableState, Mycolor, True)
                        SetObsoleteControlState(EnableState, Mycolor, False)
                        SetTestGroupControlstate(EnableState, Mycolor, chkTestEquipmentIsTestGroup.Checked)



                        'Assume that this is the active Revision, however it is readonly... only set this programicly...
                        If chkActiveRevision IsNot Nothing Then
                            chkActiveRevision.AutoCheck = False
                            chkActiveRevision.BackColor = Mycolor
                            chkActiveRevision.Checked = True
                        End If

                        'Assume that this is the active Revision, however it is readonly... only set this programicly...
                        If chkTestEquipmentCalReq IsNot Nothing Then
                            chkActiveRevision.AutoCheck = False
                            chkTestEquipmentCalReq.BackColor = Mycolor
                        End If

                        'assume that it is not obslolete so it is not visible or enabled for editing
                        If chkTestEquipmentObsolete IsNot Nothing Then
                            chkTestEquipmentObsolete.AutoCheck = Not EnableState
                            chkTestEquipmentObsolete.BackColor = Mycolor
                            chkTestEquipmentObsolete.Visible = False
                        End If

                        dgvTestEquipment.Columns("Index").Width = 1
                        dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                        dgvTestEquipment.Enabled = False

                        Me.Text = "Edit Test Equipment - Add New Device (Copy)"
                        _CurrentState = NextState

                    End If

                Case eTestEquipmentState.ObsoleteDevice

                    If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Then
                        'Change to Edit View
                        cbTestEquipmentTypeFilter.Enabled = False
                        tsmNew.Enabled = False
                        tsmCopy.Enabled = False
                        tsmAdminEdit.Enabled = False
                        tsmSave.Enabled = True
                        tsmSaveAndExit.Enabled = True
                        tsmCancel.Enabled = True
                        tsmRevise.Enabled = False
                        tsmObsolete.Enabled = False

                        ' If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then
                        'Change to Edit View
                        Dim Enabled As Boolean = True
                        Dim Mycolor As System.Drawing.Color = _ColorEditableBackGround
                        'Set Obsolete Flag
                        SetObsoleteControlState(Enabled, Mycolor, False)

                        'must be set using code...
                        chkTestEquipmentObsolete.Checked = True
                        chkTestEquipmentObsolete.AutoCheck = False
                        chkTestEquipmentObsolete.Visible = True
                        lblObsoleteDate.Visible = True
                        txtTestEquipmentObsoleteDate.Visible = True
                        dtpTestEquipmentObsoleteDate.Visible = True
                        dtpTestEquipmentObsoleteDate.Enabled = True
                        txtTestEquipmentObsoleteDate.BackColor = _ColorEditableBackGround

                        'Allow the user to edit the Note as well...
                        txtTestEquipmentNote.ReadOnly = False
                        txtTestEquipmentNote.BackColor = _ColorEditableBackGround
                        'Show the Obsolete date Txt and Dtp
                        '   End If

                        'Assume that this is the active Revision, however it is readonly... only set this programicly...
                        If chkActiveRevision IsNot Nothing Then
                            chkActiveRevision.AutoCheck = False
                            chkActiveRevision.BackColor = _ColorNonEditableBackGround 'not editable when obseleting a piece of equipment....
                        End If

                        Me.Text = "Edit Test Equipment - Obsolete Current Device"

                        dgvTestEquipment.Columns("Index").Width = 1
                        dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                        dgvTestEquipment.Enabled = False
                        _CurrentState = NextState
                    End If
                Case eTestEquipmentState.ReviseCalDevice

                    If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Or _User.AccessLevel = frmFailureBrowser.eAccessState.POWER Or _User.AccessLevel = frmFailureBrowser.eAccessState.CREATE_NEW Then
                        'Copy active Device to buffer
                        'Change to Edit View 
                        If chkTestEquipmentCalReq.Checked = False Then
                            MsgBox("Selected Test Equipment Does not Require Calibration" + vbCrLf + "Admin Rights Required to Edit, Aborting")
                            Return False
                        End If
                        'Change to Edit View
                        cbTestEquipmentTypeFilter.Enabled = False
                        Dim EnabledState As Boolean = True
                        Dim MyEditcolor As System.Drawing.Color = _ColorEditableBackGround
                        Dim MyNonEditableColor As System.Drawing.Color = Panel2.BackColor
                        'Change to Edit View
                        tsmNew.Enabled = False
                        tsmCopy.Enabled = False
                        tsmAdminEdit.Enabled = False
                        tsmSave.Enabled = True
                        tsmSaveAndExit.Enabled = True
                        tsmCancel.Enabled = True
                        tsmRevise.Enabled = False
                        tsmObsolete.Enabled = False

                        If _CurrentState = eTestEquipmentState.ReadOnlyViewDevice Then
                            'Query Max and Min ID
                            Dim MaxID As Integer = _MyTestEquipmentTable.Compute("MAX([ID])", "[Active Rev] = True OR [Active REV] = " + (Not chkTestEquipmentShowInactiveRev.Checked).ToString)
                            Dim MinID As Integer = _MyTestEquipmentTable.Compute("MIN([ID])", "[Active Rev] = True OR [Active REV] = " + (Not chkTestEquipmentShowInactiveRev.Checked).ToString)
                            Dim CurrentRowIndex As Integer

                            If dgvTestEquipment.CurrentRow Is Nothing Then
                                MsgBox("No Testequipment Selected to Revise")
                                Return False
                            Else
                                CurrentRowIndex = dgvTestEquipment.CurrentRow.Index
                                Dim MyObject As Object = dgvTestEquipment.CurrentRow.Cells("Index").Value
                                'The value returned below will depend on the current values in the Datagrid...
                                CurrentRowIndex = _MyTestEquipmentBindingSource.Find("Index", dgvTestEquipment.CurrentRow.Cells("Index").Value)
                            End If


                            Dim myobject2 As Object = _MyTestEquipmentTable.Rows(0)("ID")

                            'Create a new row in the Failure Report data table - This is handled in state machine
                            _MyTestEquipmentTable.Rows.Add(_MyTestEquipmentTable.NewRow())
                            Dim myobject3 As Object = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Id")
                            'pass out of function in case of cancel
                            _RecordToBeRevisedIndex = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Index").Value

                            'increment the NEW ID in the data table and set any default values
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("ID") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("ID").Value

                            dgvTestEquipment.Rows(CurrentRowIndex).Cells("Active Rev").Value = False

                            'Since this data has been copied
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Active Rev") = True 'It is the new active record...

                            Try
                                _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Rev").Value + 1
                            Catch ex As Exception
                                _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Rev") = 1
                            End Try

                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Obsolete") = False

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Lab ID")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Lab ID") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Lab ID").Value

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Test Group").Value

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Test Group Members")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Test Group Members") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Test Group Members").Value

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Manufacturer")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Manufacturer") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Manufacturer").Value

                            ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Model")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Model") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Model").Value

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Serial Number")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Serial Number") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Serial Number").Value

                            ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Description")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Description") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Description").Value

                            '_MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Type")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Type") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Type").Value

                            ' _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = _MyTestEquipmentTable.Rows(CurrentRowIndex)("Cal Req")
                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Cal Req") = dgvTestEquipment.Rows(CurrentRowIndex).Cells("Cal Req").Value

                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Last Cal") = ""

                            _MyTestEquipmentTable.Rows(_MyTestEquipmentTable.Rows.Count - 1)("Next Cal") = ""

                            'end edit on all binding sources to create new record
                            _MyTestEquipmentBindingSource.EndEdit()

                            ''Not  sure Ineed this either - FJB
                            '  Me.EndEditOnAllBindingSources()

                            'Force a control Validation
                            Me.Validate()

                            'This Sends the changes out to the remote database
                            _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

                            'clear contents of local datatable
                            _MyTestEquipmentTable.Clear()

                            'Resyncronize local datatable
                            _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)

                            'Query Max and Min ID
                            Dim MaxIndex As Integer = _MyTestEquipmentTable.Compute("MAX([Index])", Nothing)


                            'filter By ID then Index
                            _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable
                            _MyTestEquipmentBindingSource.Sort = Nothing
                            'MsgBox(_MyTestEquipmentBindingSource.SupportsAdvancedSorting.ToString) 'for test
                            _MyTestEquipmentBindingSource.Sort = "ID DESC, Index DESC"
                            dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource

                            'set the position at the new failure report
                            _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", MaxIndex)



                        End If

                        SetTestEquipmentInfoControlState(Not EnabledState, MyNonEditableColor, True)
                        SetCalibrationControlState(EnabledState, MyEditcolor, True)
                        SetObsoleteControlState(Not EnabledState, MyNonEditableColor, False)
                        SetTestGroupControlstate(Not EnabledState, MyNonEditableColor, True)

                        'Assume that this is the active Revision, however it is readonly... only set this programicly...
                        If chkActiveRevision IsNot Nothing Then
                            chkActiveRevision.AutoCheck = False 'Override...do not allow to change...only revision date editing allowed...
                            chkActiveRevision.BackColor = MyEditcolor
                            chkActiveRevision.Checked = True
                        End If


                        'Allow the user to edit the Note as well...
                        txtTestEquipmentNote.ReadOnly = False
                        txtTestEquipmentNote.BackColor = _ColorEditableBackGround
                        dgvTestEquipment.Columns("Index").Width = 1
                        dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                        dgvTestEquipment.Enabled = False


                        Me.Text = "Edit Test Equipment - Update Calibration Date"
                        _CurrentState = NextState
                        'Copy Buffer to New Record
                        'increment Revision
                        'Set Active Revision Flag
                    End If

                Case eTestEquipmentState.AdminEditDevice

                    'Cannot enter EDIT state without rights....
                    If _User.AccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
                        cbTestEquipmentTypeFilter.Enabled = False
                        'Change to Edit View
                        tsmNew.Enabled = False
                        tsmCopy.Enabled = False
                        tsmAdminEdit.Enabled = False
                        tsmSave.Enabled = True
                        tsmSaveAndExit.Enabled = True
                        tsmCancel.Enabled = True
                        tsmRevise.Enabled = False
                        tsmObsolete.Enabled = False
                        'Change to Edit View
                        Dim EnabledState As Boolean = True
                        Dim MyEditcolor As System.Drawing.Color = _ColorEditableBackGround
                        Dim MyNonEditableColor As System.Drawing.Color = Panel2.BackColor


                        SetTestEquipmentInfoControlState(EnabledState, MyEditcolor, True)
                        SetCalibrationControlState(EnabledState, MyEditcolor, True)
                        SetObsoleteControlState(EnabledState, MyEditcolor, True)
                        SetTestGroupControlstate(EnabledState, MyEditcolor, True)
                        dgvTestEquipment.Columns("Index").Width = 1
                        dgvTestEquipment.Columns("Index").DisplayIndex = dgvTestEquipment.Columns.Count - 1
                        dgvTestEquipment.Enabled = False
                        ' End If

                        _CurrentState = NextState
                    End If

            End Select



            Me.Show()
            _SafeToProcessEvents = True
            Return True

        Catch ex As Exception
            'silently handle for now 
            MsgBox("Error Encounted during state change" + vbCrLf + ex.ToString)
            _SafeToProcessEvents = True 'reenable events
            Me.Show()
            Return False

        End Try



    End Function
#End Region '"Control States"
#Region "Main Form Events"

    Private Sub frmTestEquipment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '= _MyDatabaseaccess.GetData(frmFailureBrowser.gMyMeterSpecOleDBConnection, "TEST_EQUIPMENT", "WHERE Active = " + _SQL_TRUE)

TEST_EQUIPMENT_TABLE:

        'initalize
        Me.DialogResult = Windows.Forms.DialogResult.None

        _User = frmFailureBrowser._CurrentUser
        _MyTestEquipmentDataAdaptor = New SqlDataAdapter("SELECT * From [TEST_EQUIPMENT] ORDER BY [ID] Desc", frmFailureBrowser.gMyMeterSpecDBConnection)
        _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)
        _MyTestEquipmentBindingSource = New BindingSource
        '_MyBindingNavigator = New BindingNavigator
        _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable

        dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource

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

        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentManufacturer, "Text", "Manufacturer", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentModel, "Text", "Model", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentDescription, "Text", "Description", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txttestEquipmentSerialNumber, "Text", "Serial Number", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentLastCalDate, "Text", "Last Cal", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentNextCalDate, "Text", "Next Cal", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentLabIdentifier, "Text", "Lab ID", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentNote, "Text", "Note", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentRev, "Text", "Rev", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentTestGroupMembers, "Text", "Test Group Members", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentLocation, "Text", "Location", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))



        'xboXComboBox
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(readonlycbTestEquipmentType, "Text", "Type", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'check boxes
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkActiveRevision, "CheckState", "Active Rev", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentIsTestGroup, "CheckState", "Test Group", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentObsolete, "CheckState", "Obsolete", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTestEquipmentCalReq, "CheckState", "Cal Req", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'moved so it is  the last binding...Test ...FJB
        _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTestEquipmentID, "Text", "ID", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))


        ' _TestEquipmentBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rocbTestEquipmentUser, "Text", "ID", _MyTestEquipmentBindingSource, _MyTestEquipmentBinding))

        'rocbTestEquipmentUser.DataSource = _MyTestEquipmentBindingSource
        'rocbTestEquipmentUser.DisplayMember = ""

        'Excute the bindings to the database....

        'Call the function the creates the bindings
        _MyDatabaseaccess.BindControls(_TestEquipmentBindingRecord)

        'Default
        _MyFilter = "[Active Rev] = True AND [Obsolete] = False"
        _MyTestEquipmentBindingSource.Filter = _MyFilter

USERS_TABLE:

        '_MyUsers()
        _MyUsersDataAdaptor = New SqlDataAdapter("SELECT * From [USERS] WHERE Active = 1 ORDER BY [ID] Desc", frmFailureBrowser.gMyMeterSpecDBConnection)
        _MyUsersDataAdaptor.Fill(_MyUsersTable)
        _MyUsersBindingSource = New BindingSource
        '_MyBindingNavigator = New BindingNavigator
        _MyUsersBindingSource.DataSource = _MyUsersTable

        ' dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource

        'create automatic Queries
        'Create the commandbuilder object
        DbCommandBuilder = New SqlCommandBuilder(_MyUsersDataAdaptor)
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




        'during load
        ' PopulateAutoCompleteTextBox(txtTestEquipmentTestType, frmFailureBrowser.gMyMeterSpecOleDBConnection, "Test_Type", "TEST_EQUIPMENT_TYPE", "WHERE Active = " + _SQL_TRUE)

        rocbEditModeTest.DataSource = [Enum].GetValues(GetType(eTestEquipmentState))
    End Sub
    Private Sub frmSelectTestEquipment_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        _SafeToProcessEvents = True
    End Sub
    Private Sub frmSelectTestEquipment_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
                If MsgBox("Changes will lost, are you sure you want to Cancel?", MsgBoxStyle.YesNo, "Discard Changes?") = MsgBoxResult.No Then
                    e.Cancel = True
                    ' Exit Sub
                Else
                    If _CurrentState <> eTestEquipmentState.ReadOnlyViewDevice Then
                        tsmCancel.PerformClick()
                    End If
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
#End Region '"Main Form Events"
#Region "For Testing"
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        SetTestEquipmentState(rocbEditModeTest.SelectedValue)
    End Sub
    Private Sub btnTestReadOnly_Click(sender As System.Object, e As System.EventArgs) Handles btnTestReadOnly.Click
        Me.Hide()
        If btnTestReadOnly.Text.ToLower = "READ ONLY".ToLower Then
            Dim MyTextValue As String = readonlycbTestEquipmentType.Text
            readonlycbTestEquipmentType.DataSource = Nothing
            'xocbTestEquipmentType.DropDownStyle = xboXComboBoxStyle.Simple
            readonlycbTestEquipmentType.Text = MyTextValue
            readonlycbTestEquipmentType.ReadOnly = True
            readonlycbTestEquipmentType.BackColor = Color.LightYellow

            For Each Control In panelTestEquipmentRecord.Controls

                If TypeOf Control Is TextBox Then
                    Dim MyTextbox As TextBox = DirectCast(Control, TextBox)
                    MyTextbox.ReadOnly = True
                    MyTextbox.BackColor = Color.LightYellow
                ElseIf TypeOf Control Is MaskedTextBox Then
                    Dim MyTextbox As MaskedTextBox = DirectCast(Control, MaskedTextBox)
                    MyTextbox.ReadOnly = True
                    MyTextbox.BackColor = Color.LightYellow
                ElseIf TypeOf Control Is RichTextBox Then
                    Dim MyTextbox As RichTextBox = DirectCast(Control, RichTextBox)
                    MyTextbox.ReadOnly = True
                    MyTextbox.BackColor = Color.LightYellow
                ElseIf TypeOf Control Is DateTimePicker Then
                    Dim MyDTP As DateTimePicker = DirectCast(Control, DateTimePicker)
                    MyDTP.Visible = False
                    MyDTP.BackColor = Color.LightYellow
                    'ElseIf TypeOf Control Is CheckBox Then
                    '    Dim MyChk As CheckBox = DirectCast(Control, CheckBox)
                    '    MyChk.Enabled = False
                    '    MyChk.BackColor = Color.LightYellow
                ElseIf TypeOf Control Is GroupBox Then
                    Dim MyControl As GroupBox = DirectCast(Control, GroupBox)
                    For Each GroupBoxControl In MyControl.Controls
                        If TypeOf GroupBoxControl Is TextBox Then
                            Dim MyTextbox As TextBox = DirectCast(GroupBoxControl, TextBox)
                            MyTextbox.ReadOnly = True
                            MyTextbox.BackColor = Color.LightYellow
                        ElseIf TypeOf GroupBoxControl Is Button Then
                            Dim MyButton As Button = DirectCast(GroupBoxControl, Button)
                            MyButton.Enabled = False
                        End If
                    Next
                ElseIf TypeOf Control Is CheckBox Then
                    Dim MyControl As CheckBox = DirectCast(Control, CheckBox)
                    MyControl.AutoCheck = True
                    MyControl.BackColor = Color.LightYellow
                End If

            Next
            dgvTestEquipment.Enabled = True
            btnTestReadOnly.Text = "Edit"
        Else
            readonlycbTestEquipmentType.ReadOnly = False
            readonlycbTestEquipmentType.DropDownStyle = ComboBoxStyle.DropDown
            readonlycbTestEquipmentType.BackColor = Color.White
            For Each Control In panelTestEquipmentRecord.Controls
                If Control.name = txtTestEquipmentID.Name Then
                    'do nothing this field is never editable by the user...
                ElseIf TypeOf Control Is TextBox Then
                    Dim MyTextbox As TextBox = DirectCast(Control, TextBox)

                    MyTextbox.ReadOnly = False
                    MyTextbox.BackColor = Color.White

                ElseIf TypeOf Control Is MaskedTextBox Then
                    Dim MyTextbox As MaskedTextBox = DirectCast(Control, MaskedTextBox)
                    If MyTextbox.Name = txtTestEquipmentLastCalDate.Name Or MyTextbox.Name = txtTestEquipmentNextCalDate.Name Then
                        MyTextbox.ReadOnly = True ' Keep it read only, only the datapicker can be used for entering a date...
                    Else
                        MyTextbox.ReadOnly = False
                    End If

                    MyTextbox.BackColor = Color.White
                ElseIf TypeOf Control Is RichTextBox Then
                    Dim MyTextbox As RichTextBox = DirectCast(Control, RichTextBox)
                    MyTextbox.ReadOnly = False
                    MyTextbox.BackColor = Color.White
                ElseIf TypeOf Control Is DateTimePicker Then
                    Dim MyDTP As DateTimePicker = DirectCast(Control, DateTimePicker)
                    MyDTP.Visible = True
                    MyDTP.BackColor = Color.White
                    'ElseIf TypeOf Control Is CheckBox Then
                    '    Dim MyChk As CheckBox = DirectCast(Control, CheckBox)
                    '    MyChk.Enabled = True
                    '    MyChk.BackColor = Color.White
                ElseIf TypeOf Control Is GroupBox Then
                    Dim MyControl As GroupBox = DirectCast(Control, GroupBox)
                    For Each GroupBoxControl In MyControl.Controls
                        If TypeOf GroupBoxControl Is TextBox Then
                            Dim MyTextbox As TextBox = DirectCast(GroupBoxControl, TextBox)
                            MyTextbox.ReadOnly = False
                            MyTextbox.BackColor = Color.White
                        ElseIf TypeOf GroupBoxControl Is Button Then
                            Dim MyButton As Button = DirectCast(GroupBoxControl, Button)
                            MyButton.Enabled = True
                        End If
                    Next
                ElseIf TypeOf Control Is CheckBox Then
                    Dim MyControl As CheckBox = DirectCast(Control, CheckBox)
                    MyControl.AutoCheck = False
                    MyControl.BackColor = Panel2.BackColor
                End If

            Next
            dgvTestEquipment.Enabled = False
            btnTestReadOnly.Text = "Read Only"
        End If
        Me.Show()

    End Sub

#End Region '"For Testing"
#Region "Date Time Pickers"
    Private Sub dtpTestEquipLastCalDate_CloseUp(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipLastCalDate.CloseUp
        txtTestEquipmentLastCalDate.Text = dtpTestEquipLastCalDate.Value.ToShortDateString
    End Sub

    Private Sub dtpTestEquipNextCalDate_CloseUp(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipnextCalDate.CloseUp
        txtTestEquipmentNextCalDate.Text = dtpTestEquipnextCalDate.Value.ToShortDateString
    End Sub

    Private Sub dtpTestEquipmentObsoleteDate_CloseUp(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipmentObsoleteDate.CloseUp
        txtTestEquipmentObsoleteDate.Text = dtpTestEquipmentObsoleteDate.Value.ToShortDateString
    End Sub

    Private Sub dtpTestEquipLastCalDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipLastCalDate.ValueChanged
        txtTestEquipmentLastCalDate.Text = dtpTestEquipLastCalDate.Value.ToShortDateString
    End Sub

    Private Sub dtpTestEquipnextCalDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipnextCalDate.ValueChanged
        txtTestEquipmentNextCalDate.Text = dtpTestEquipnextCalDate.Value.ToShortDateString
    End Sub

    Private Sub dtpTestEquipmentObsoleteDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpTestEquipmentObsoleteDate.ValueChanged
        txtTestEquipmentNextCalDate.Text = dtpTestEquipnextCalDate.Value.ToShortDateString
    End Sub
#End Region '"Date Time Pickers"
#Region "Tool Strip Menu"
    Private Sub tsmNew_Click(sender As System.Object, e As System.EventArgs) Handles tsmNew.Click
        SetTestEquipmentState(eTestEquipmentState.NewDevice)
        btnTestEquipmentRefresh.PerformClick()
    End Sub

    Private Sub tsmCancel_Click(sender As System.Object, e As System.EventArgs) Handles tsmCancel.Click
        If _CurrentState = eTestEquipmentState.NewDevice Or _CurrentState = eTestEquipmentState.CopyDevice Or _CurrentState = eTestEquipmentState.ReviseCalDevice Then
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
                If dgvTestEquipment.CurrentRow Is Nothing Then
                    _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", CurrentID)
                    SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
                    Exit Sub
                Else
                    _MyTestEquipmentBindingSource.Position = dgvTestEquipment.CurrentRow.Index
                End If

                'restore active state
                If _CurrentState = eTestEquipmentState.ReviseCalDevice Then
                    'DataGridView1.Rows(_RecordToBeRevisedIndex + 1).Cells("Active Rev").Value = True
                    ' Dim Mydatatable As DataTable = _MyDatabaseaccess.GetData(frmFailureBrowser.gMyMeterSpecOleDBConnection, "TEST EQUIPMENT", "WHERE [INDEX] = " + _RecordToBeRevisedIndex.ToString)
                    'Mydatatable.Rows(0)("Active Rev") = True
                    If frmFailureBrowser.gMyMeterSpecDBConnection.State = ConnectionState.Open Then
                        frmFailureBrowser.gMyMeterSpecDBConnection.Close()
                    End If
                    frmFailureBrowser.gMyMeterSpecDBConnection.Open()
                    Dim Command As String = "UPDATE [Test_Equipment] SET [Active Rev] = True WHERE [INDEX] = " + _RecordToBeRevisedIndex.ToString
                    Dim cmd As New SqlCommand(Command, frmFailureBrowser.gMyMeterSpecDBConnection)
                    cmd.ExecuteNonQuery()
                    frmFailureBrowser.gMyMeterSpecDBConnection.Close()
                End If

                'Cast the new row from the binding source to DataRowView to ease minipulation 
                Dim CurrentRow As DataRowView = DirectCast(_MyTestEquipmentBindingSource.Current, DataRowView)
                CurrentRow.Delete()
                'Cancel edit for now
                _MyTestEquipmentBindingSource.EndEdit()
                'gMyMeterSpecBindingSource.
                _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)

                'Set to most recently added failure report
                CurrentID = _MyTestEquipmentTable.Compute("MAX([ID])", Nothing) 'CInt((Val(txtPosition.Text - 1)))
                If CurrentID > MaxID Or CurrentID < MinID Then
                    'do nothing right now, handle error...
                Else
                    _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", CurrentID.ToString)
                End If
                SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
                btnTestEquipmentRefresh.PerformClick()
            Catch ex As Exception
                MsgBox("Error Canceling Edit" + vbCrLf + ex.ToString)
            End Try
        Else
            'for now
            Dim CurrentID As Integer = Val(txtTestEquipmentID.Text)
            btnTestEquipmentRefresh.PerformClick()
            _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", CurrentID.ToString)
            SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        End If

    End Sub

    Private Sub tsmCopy_Click(sender As System.Object, e As System.EventArgs) Handles tsmCopy.Click
        If SetTestEquipmentState(eTestEquipmentState.CopyDevice) = False Then
            SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        End If
        btnTestEquipmentRefresh.PerformClick()
    End Sub

    Private Sub tsmEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsmAdminEdit.Click
        If SetTestEquipmentState(eTestEquipmentState.AdminEditDevice) = False Then
            SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        End If
        ' btnTestEquipmentRefresh.PerformClick()
    End Sub

    Private Function ValidateTestEquipmentInformation() As Boolean
        Dim ReturnValue As Boolean = True 'Assume Success
        Dim ErrorMessage As String = "Check Required Information"
        'Check that all fields have been completed except for revise or obsolete
        'since these two states are entered by copying a previously calibrated record then only
        'their dates need validated
        If _CurrentState <> eTestEquipmentState.ReviseCalDevice And _CurrentState <> eTestEquipmentState.ObsoleteDevice Then


            'check and make sure that all equipment has been filled in
            If txtTestEquipmentLocation.Text.Trim = "" Then
                'MsgBox("Lab Identifier Must be completed")
                txtTestEquipmentLocation.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Location Must be completed"
                ReturnValue = False
            End If


            'check and make sure that all equipment has been filled in
            If txtTestEquipmentLabIdentifier.Text.Trim = "" Then
                'MsgBox("Lab Identifier Must be completed")
                txtTestEquipmentLabIdentifier.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Lab Identifier Must be completed"
                ReturnValue = False
            End If

            If txtTestEquipmentManufacturer.Text.Trim = "" Then
                'MsgBox("Manufacturer Must be completed")
                txtTestEquipmentManufacturer.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Manufacturer Must be completed"
                ReturnValue = False
            End If

            If txtTestEquipmentModel.Text.Trim = "" Then
                'MsgBox("Model Must be completed")
                txtTestEquipmentModel.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Model Must be completed"
                ReturnValue = False
            End If

            If txttestEquipmentSerialNumber.Text.Trim = "" Then
                'MsgBox("Serial Number Must be completed")
                txttestEquipmentSerialNumber.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Serial Number Must be completed"
                ReturnValue = False
            End If

            If readonlycbTestEquipmentType.Text.Trim = "" Then
                'MsgBox("Type Must be completed")
                readonlycbTestEquipmentType.Focus()
                ErrorMessage = ErrorMessage + vbCrLf + "Type Must be completed"
                ReturnValue = False
            End If

            If txtTestEquipmentDescription.Text.Trim = "" Then
                'MsgBox("Description Must be completed")
                txttestEquipmentSerialNumber.SelectAll()
                ErrorMessage = ErrorMessage + vbCrLf + "Description Must be completed"
                ReturnValue = False
            End If

            If chkTestEquipmentIsTestGroup.Checked Then
                If txtTestEquipmentTestGroupMembers.Text.Trim = "" Then
                    txtTestEquipmentTestGroupMembers.SelectAll()
                    'MsgBox("Warning No Test Equipment has Been added to this test Equipment group!")
                    ErrorMessage = ErrorMessage + vbCrLf + "Test Equipment must be added to this test Equipment group!"
                    ReturnValue = False
                End If
            End If
        End If

        'Check for all states...
        If chkTestEquipmentCalReq.Checked Then

            If txtTestEquipmentLastCalDate.Text.Trim = "" Then
                ' MsgBox("Last Calibration Date Must be completed")
                dtpTestEquipLastCalDate.Focus()
                ErrorMessage = ErrorMessage + vbCrLf + "Last Calibration Date Must be completed"
                ReturnValue = False
            End If

            If txtTestEquipmentNextCalDate.Text.Trim = "" Then
                ' MsgBox("Next Calibation Date Must be completed")
                dtpTestEquipnextCalDate.Focus()
                ErrorMessage = ErrorMessage + vbCrLf + "Next Calibation Date Must be completed"
                ReturnValue = False
            End If

        End If

        'only check for all states...
        If chkTestEquipmentObsolete.Checked Then
            If txtTestEquipmentObsoleteDate.Text.Trim = "" Then
                'MsgBox("Equipment Obsolete Date Must be completed")
                dtpTestEquipmentObsoleteDate.Focus()
                ErrorMessage = ErrorMessage + vbCrLf + "Equipment Obsolete Date Must be completed"
                ReturnValue = False
            End If
        End If

        If ReturnValue = False Then
            MsgBox(ErrorMessage)
        End If

        Return ReturnValue
    End Function

    Private Sub tsmSave_Click(sender As System.Object, e As System.EventArgs) Handles tsmSave.Click

        If ValidateTestEquipmentInformation() = True Then
            Try
                ' end edit on all binding sources
                _MyTestEquipmentBindingSource.EndEdit()
                ' Force a control Validation not sure if this is needed - FJB
                Me.Validate()
                ' This Sends the changes out to the remote database
                _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)
            Catch ex As Exception
                MsgBox("Error Saving record to database" + vbCrLf + ex.ToString)
            End Try
        End If

    End Sub

    Private Sub tsmSaveAndExit_Click(sender As System.Object, e As System.EventArgs) Handles tsmSaveAndExit.Click
        If ValidateTestEquipmentInformation() = True Then


            Try
                ' end edit on all binding sources
                _MyTestEquipmentBindingSource.EndEdit()
                ' Force a control Validation not sure if this is needed - FJB
                Me.Validate()
                ' This Sends the changes out to the remote database
                _MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)
            Catch ex As Exception
                MsgBox("Error Saving Record to Database" + vbCrLf + ex.ToString)
                Exit Sub
            End Try

            Try
                SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
            Catch ex As Exception
                MsgBox("Error Setting Read Only State" + vbCrLf + ex.ToString)
            End Try
        End If
    End Sub

    Private Sub tsmRevise_Click(sender As System.Object, e As System.EventArgs) Handles tsmRevise.Click
        '  SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        If SetTestEquipmentState(eTestEquipmentState.ReviseCalDevice) = False Then
            SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        End If
        btnTestEquipmentRefresh.PerformClick()
    End Sub

    Private Sub tsmObsolete_Click(sender As System.Object, e As System.EventArgs) Handles tsmObsolete.Click
        If SetTestEquipmentState(eTestEquipmentState.ObsoleteDevice) = False Then
            SetTestEquipmentState(eTestEquipmentState.ReadOnlyViewDevice)
        End If
        btnTestEquipmentRefresh.PerformClick()
    End Sub

    Private Sub AddTestGroupMembersToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddTestGroupMembersToolStripMenuItem.Click
        'SetTestEquipmentState(eTestEquipmentState.XXXXXX)
    End Sub

    Private Sub chkTestEquipmentCalReq_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTestEquipmentCalReq.CheckedChanged
        If _SafeToProcessEvents Then

            SetTestEquipmentState(_CurrentState)

        End If

    End Sub
#End Region '"Tool Strip Menu"
#Region "xboXComboBoxes"
    Private Sub xocbTestEquipmentType_DropDown(sender As System.Object, e As System.EventArgs) Handles readonlycbTestEquipmentType.DropDown
        If readonlycbTestEquipmentType.ReadOnly = True Then
            'Do Nothing for now...
        Else
            'Populate
            readonlycbTestEquipmentType.DropDownStyle = ComboBoxStyle.DropDown
            _MyDatabaseaccess.PopulateReadOnlyComboBox(frmFailureBrowser.gMyMeterSpecDBConnection, readonlycbTestEquipmentType, "TEST_TYPE", "TEST_EQUIPMENT_TYPE", "WHERE [Active] = 1")  'True")
        End If
    End Sub
    Private Sub cbTestEquipmentTypeFilter_DropDown(sender As System.Object, e As System.EventArgs) Handles cbTestEquipmentTypeFilter.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""

        frmFailureBrowser.PopulatexboXComboBox(frmFailureBrowser.gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Test_Equipment_Type", " WHERE Active = 1" + strFilter) 'True" + strFilter)

    End Sub
#End Region '"xboXComboBoxes"
#Region "Checkboxes"
    Private Sub chkTestEquipmentObsolete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTestEquipmentObsolete.CheckedChanged
        If _SafeToProcessEvents Then
            SetTestEquipmentState(_CurrentState)
        End If
    End Sub
    Private Sub chkTestEquipmentIsTestGroup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTestEquipmentIsTestGroup.CheckedChanged
        If _SafeToProcessEvents Then
            SetTestEquipmentState(_CurrentState)
        End If
    End Sub
#End Region '"Checkboxes"
#Region "Buttons"
    Private Sub btnTestEquipmentTestGroupListMembers_Click(sender As System.Object, e As System.EventArgs) Handles btnTestEquipmentTestGroupListMembers.Click
        Try
            _SafeToProcessEvents = False
            _MyFilter = ""

            If chkTestEquipmentShowInactiveRev.Checked = False Then
                _MyFilter = "([ID] = " + txtTestEquipmentID.Text.Trim + " AND [Active Rev] = True)"
            End If

            Dim straGroupMembers As List(Of String) = New List(Of String)
            straGroupMembers.AddRange(txtTestEquipmentTestGroupMembers.Text.Split(_Delimiter))
            Dim i As Integer = 0
            For i = 0 To straGroupMembers.Count - 1
                _MyFilter = _MyFilter + " OR [Index] = " + straGroupMembers(i)
            Next

            _MyFilter = _MyFilter + " ORDER BY CASE [ID] WHEN '" + txtTestEquipmentID.Text.Trim + "' THEN 1"

            For i = 0 To straGroupMembers.Count - 1
                _MyFilter = _MyFilter + "  WHEN '" + straGroupMembers(i) + "' THEN " + (i + 2).ToString
            Next

            _MyFilter = _MyFilter + " ELSE " + (i + 2).ToString + " END, [INDEX]"



            ''_MyTestEquipmentDataAdaptor.Update(_MyTestEquipmentTable)
            '_MyTestEquipmentTable.Rows.Clear() 'clear the table
            '_MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable) 'refill the table
            'DataGridView1.DataSource = Nothing 'reset the datasource
            'DataGridView1.DataSource = _MyTestEquipmentBindingSource ' reattache the datasource
            _MyTestEquipmentBindingSource.Filter = _MyFilter 'apply the filter


            'set the position at the new failure report
            _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", txtTestEquipmentID.Text.Trim)


            '_MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable
            '_MyTestEquipmentBindingSource.Sort = Nothing
            '' MsgBox(_MyTestEquipmentBindingSource.SupportsAdvancedSorting.ToString)
            '_MyTestEquipmentBindingSource.Sort = "ID DESC, Index DESC"
            'DataGridView1.DataSource = _MyTestEquipmentBindingSource
            '_MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", MaxIndex)
            _SafeToProcessEvents = True
        Catch ex As Exception
            MsgBox("Error refreshing Data:" + vbCrLf + ex.ToString)
            _SafeToProcessEvents = True
        End Try
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        gSelectedTestEquipment = New DataTable
        '_SelectedTestEquipment = DirectCast(DataGridView1.DataSource, DataTable).Clone()
        Me.Tag = ""

        For Each Mycolumnc As DataGridViewColumn In dgvTestEquipment.Columns
            gSelectedTestEquipment.Columns.Add(Mycolumnc.Name)

        Next
        Dim index As Integer = 0

        For Each row As DataGridViewRow In dgvTestEquipment.SelectedRows
            If row.IsNewRow Then
                'skip
            Else
                Dim dataBoundItem As DataRow = DirectCast(row.DataBoundItem, DataRowView).Row
                gSelectedTestEquipment.ImportRow(dataBoundItem)

                If index = 0 Then
                    Me.Tag = gSelectedTestEquipment.Rows(index)("INDEX").ToString

                Else
                    Me.Tag = Me.Tag + ";" + gSelectedTestEquipment.Rows(index)("INDEX").ToString
                End If
                index += 1

                'dataBoundItem("
            End If

        Next

        'Me.Tag = DataGridView1.SelectedRows

        'For i = 1 To DataGridView1.SelectedRows.Count - 1
        '    Me.Tag = Me.Tag + ";" + DataGridView1.SelectedRows("ID").ToString
        'Next

        'For Each row As DataGridViewRow In DataGridView1.SelectedRows
        '    Dim MyRow As String = ""
        '    For Each Mycell As DataGridViewCell In row.Cells
        '        MyRow = MyRow + Mycell.Value.ToString + vbTab
        '    Next
        '    Me.Tag = Me.Tag + MyRow + vbCrLf
        '    ' _SelectedTestEquipment.ImportRow(DirectCast(DataGridView1.DataSource, DataTable).Rows(row.Index))
        'Next
        ' _SelectedTestEquipment.AcceptChanges()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub
    Private Sub btnEditTestGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTestGroup.Click
        Dim SelectTestGroupmembers As New frmSelectTestGroupMembers
        SelectTestGroupmembers.gTestEquipmentUniqueIndex = txtTestEquipmentTestGroupMembers.Text.Trim
        SelectTestGroupmembers.ShowDialog()

        If SelectTestGroupmembers.DialogResult = Windows.Forms.DialogResult.OK Then
            txtTestEquipmentTestGroupMembers.Text = SelectTestGroupmembers.gTestEquipmentUniqueIndex
        End If
        SelectTestGroupmembers = Nothing
        'SelectTestGroupmembers.Dispose()

    End Sub
    Private Sub btnTestEquipmentRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEquipmentRefresh.Click
        Try
            _SafeToProcessEvents = False
            _MyFilter = ""
            Dim CurrentID As Integer = Val(txtTestEquipmentID.Text)
            If chkTestEquipmentShowInactiveRev.Checked = False Then
                _MyFilter = "[Active Rev] = True"
            End If

            If _MyFilter <> "" Then 'Append Filter
                If chkShowPrevious.Checked = False Then
                    _MyFilter = _MyFilter + " AND [Obsolete] = False"
                End If
            Else 'only Filter so far
                If chkShowPrevious.Checked = False Then
                    _MyFilter = "[Obsolete] = False"
                End If
            End If

            If cbTestEquipmentTypeFilter.Text = "ALL" Or cbTestEquipmentTypeFilter.Text.Trim = "" Then 'Do not attempt to filter on Type if All is selected

            Else

                If _MyFilter <> "" Then 'Append Filter
                    _MyFilter = _MyFilter + " AND [TYPE] Like '%" + cbTestEquipmentTypeFilter.Text.Trim + "%'"
                Else 'Only Filter so far
                    _MyFilter = "[TYPE] Like '%" + cbTestEquipmentTypeFilter.Text.Trim + "%'"
                End If
            End If

            _MyTestEquipmentTable.Rows.Clear() 'clear the table
            _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable) 'refill the table
            dgvTestEquipment.DataSource = Nothing 'reset the datasource
            dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource ' reattache the datasource
            _MyTestEquipmentBindingSource.Filter = _MyFilter 'apply the filter


            'Query Max and Min ID
            Dim MaxIndex As Integer = _MyTestEquipmentTable.Compute("MAX([Index])", Nothing)

            'set the position at the new failure report
            '_MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", MaxIndex)


            _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable

            dgvTestEquipment.DataSource = _MyTestEquipmentBindingSource
            Try
                _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("ID", CurrentID.ToString)
            Catch
                _MyTestEquipmentBindingSource.Position = _MyTestEquipmentBindingSource.Find("Index", MaxIndex)
                _MyTestEquipmentBindingSource.Sort = Nothing
                _MyTestEquipmentBindingSource.Sort = "ID DESC, Index DESC"
            End Try
            SetTestEquipmentState(_CurrentState)
            _SafeToProcessEvents = True
        Catch ex As Exception
            MsgBox("Error refreshing Data:" + vbCrLf + ex.ToString)
            _SafeToProcessEvents = True
        End Try

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
#End Region '"Buttons"
#Region "DateGridView"
    Private Sub dgvTestEquipment_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvTestEquipment.SelectionChanged
        Dim i As Integer = 0
        Dim index As Integer = 0
        Dim RowsSelected = False

        For Each row As DataGridViewRow In dgvTestEquipment.SelectedRows
            If row.IsNewRow Then
                Exit Sub
            End If
            i += 1
            RowsSelected = True
            index = row.Index
        Next


        'if we get tot here then there is no slected rows...
        btnOK.Enabled = RowsSelected
    End Sub
#End Region '"DateGridView"
#Region "Removed Code"
    Private Sub txtTestEquipmentTestType_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        'If txtTestEquipmentTestType.Text.Trim = "" And txtTestEquipmentTestType.Text.Length > 1 Then
        '    txtTestEquipmentTestType.Text = " " '+ txtTestEquipmentTestType.Text.Trim
        '    txtTestEquipmentTestType.SelectionStart = 1
        '    ' ElseIf txtTestEquipmentTestType.Text.Trim <> "" Then
        '    '   txtTestEquipmentTestType.Text = " " '+ txtTestEquipmentTestType.Text.Trim
        '    '  txtTestEquipmentTestType.SelectionStart = 1

        'End If
    End Sub
    Private Sub PopulateAutoCompleteTextBox(MyTextBox As TextBox, MyConnection As OleDb.OleDbConnection, MyColumn As String, MyTableName As String, Optional MyFilter As String = "")

        Dim MyTable As DataTable = _MyDatabaseaccess.GetDistinctData(frmFailureBrowser.gMyMeterSpecDBConnection, MyColumn, MyTableName, MyFilter)
        'Dim MyTable As DataTable = _MyDatabaseaccess.GetDistinctData(frmFailureBrowser.gMyMeterSpecOleDBConnection, "Test_Type", "TEST_EQUIPMENT_TYPE", "WHERE Active = " + _SQL_TRUE)

        Dim SuggestionList As New List(Of String)
        For Each Myrow As DataRow In MyTable.Rows
            SuggestionList.Add(" " + Myrow(MyColumn).ToString)
        Next
        MyTextBox.AutoCompleteMode = AutoCompleteMode.Suggest
        Dim MySource As New AutoCompleteStringCollection
        MySource.AddRange(SuggestionList.ToArray)
        MyTextBox.AutoCompleteCustomSource = MySource
        MyTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub
    Private Sub txtTestEquipmentTestType_Enter(sender As System.Object, e As System.EventArgs)


        'If txtTestEquipmentTestType.Text = "" Then
        '    SendKeys.Send(" ")
        '    ' txtTestEquipmentTestType.Text = " "
        '    txtTestEquipmentTestType.SelectionStart = 1
        ' End If
    End Sub
    Private Sub txtTestEquipmentTestType_TextChanged(sender As System.Object, e As System.EventArgs)
        'If txtTestEquipmentTestType.Text.Trim = "" And txtTestEquipmentTestType.Text.Length > 1 Then
        '    txtTestEquipmentTestType.Text = " " '+ txtTestEquipmentTestType.Text.Trim
        '    txtTestEquipmentTestType.SelectionStart = 1 = " " '+ txtTestEquipmentTestType.Text.Trim
        '    txtTestEquipmentTestType.SelectionStart = 1
        'ElseIf txtTestEquipmentTestType.Text.Trim <> "" Then
        '    txtTestEquipmentTestType.Text = txtTestEquipmentTestType.Text.Trim
        'End If
        'Try
        '    'txtTestEquipmentTestType.SelectionStart = 0
        '    ' txtTestEquipmentTestType.SelectionLength = txtTestEquipmentTestType.Text.Length

        '    If txtTestEquipmentTestType.Text.Trim <> "" Then
        '        ' txtTestEquipmentTestType.Text = txtTestEquipmentTestType.Text.Trim
        '        ' txtTestEquipmentTestType.SelectedText = txtTestEquipmentTestType.Text.Trim
        '    End If

        '    If txtTestEquipmentTestType.AutoCompleteCustomSource.Contains(" " + txtTestEquipmentTestType.Text) = False Then
        '        txtTestEquipmentTestType.Text = " " '+ txtTestEquipmentTestType.Text.Trim
        '        txtTestEquipmentTestType.SelectionStart = 1
        '    End If
        'Catch ex As Exception
        '    MsgBox("Error checking value." + vbCrLf + ex.ToString)
        'End Try

    End Sub
    Private Sub txtTestEquipmentTestType_Leave(sender As System.Object, e As System.EventArgs)
        'If txtTestEquipmentTestType.Text.Trim = "" Then
        '    txtTestEquipmentTestType.Text = ""
        'End If
    End Sub
    Private Sub txtTestEquipmentTestType_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        'If txtTestEquipmentTestType.Text.Trim = "" And txtTestEquipmentTestType.Text.Length > 1 Then
        '     txtTestEquipmentTestType.Text = " " '+ txtTestEquipmentTestType.Text.Trim
        '     txtTestEquipmentTestType.SelectionStart = 1
        ' End If
    End Sub
#End Region 'Removed Code

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click

    End Sub
End Class