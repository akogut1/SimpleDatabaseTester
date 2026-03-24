Module Removed_Code


   
    'Public Sub DisplayShadowControl(Optional bEnabled As Boolean = False)

    '    'Variable count the number of textboxes to keep track of which controls are enbled and visible
    '    'so that the user may edit
    '    Dim TextBoxCount As Integer = 0
    '    Dim CheckBoxCount As Integer = 0
    '    '   Set each control to edit view contained in the Panel Header.
    '    '   Select based on control type and cast to type to access properties 
    '    '   Set Read Only to False (For Text boxes)
    '    '   Set the background to white to que the user that the record is in edit mode
    '    '   Exchange the Shadow Readonly textboxes with the Combo, Date Time picker, or editable
    '    '   Checkbox so the user can now edit the record
    '    If Not bEnabled Then
    '        'PANEL_HEADER_CONTROLS:

    '        For Each i As Control In pnlReportHeader.Controls
    '            'Set the Textboxes state
    '            If TypeOf i Is TextBox Then
    '                Dim MyTextBox As TextBox = DirectCast(i, TextBox)
    '                If MyTextBox.ReadOnly = False Then
    '                    MyTextBox.BackColor = System.Drawing.Color.White
    '                Else
    '                    MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '                End If

    '            End If

    '            If TypeOf i Is RichTextBox Then
    '                Dim MyTextBox As RichTextBox = DirectCast(i, RichTextBox)
    '                If MyTextBox.ReadOnly = False Then
    '                    MyTextBox.BackColor = System.Drawing.Color.White
    '                Else
    '                    MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '                End If
    '            End If
    '            'Added 1.27.2017 to Bind Date Time..FJB
    '            If TypeOf i Is MaskedTextBox Then
    '                Dim MyTextBox As MaskedTextBox = DirectCast(i, MaskedTextBox)
    '                If MyTextBox.ReadOnly = False Then
    '                    MyTextBox.BackColor = System.Drawing.Color.White
    '                Else
    '                    MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '                End If
    '            End If
    '            'Replace with editable Combobox
    '            If TypeOf i Is ComboBox Then
    '                If i.Enabled = True Then
    '                    i.Show()
    '                    i.Visible = True '_myShadowTextBox(TextBoxCount).Visible
    '                    lbLog.Items.Add("Showing Control: " + i.Name.ToString + vbTab + Now.ToString)
    '                    _myShadowTextboxIndex(TextBoxCount).VisibleState = _myShadowTextBox(TextBoxCount).Visible
    '                    _myShadowTextBox(TextBoxCount).Hide()
    '                Else
    '                    i.Hide()
    '                    '_myShadowTextBox(TextBoxCount).Size = i.Size
    '                    '_myShadowTextBox(TextBoxCount).Location = i.Location
    '                    _myShadowTextBox(TextBoxCount).Text = i.Text
    '                    _myShadowTextBox(TextBoxCount).ReadOnly = True

    '                    'If _myShadowTextboxIndex(TextBoxCount).VisibleState = True Then
    '                    _myShadowTextBox(TextBoxCount).Visible = True
    '                    _myShadowTextBox(TextBoxCount).Show()
    '                    'If

    '                    _myShadowTextBox(TextBoxCount).BackColor = _ColorNonEditableBackGround
    '                    'TextBoxCount += 1
    '                End If

    '                TextBoxCount += 1

    '            End If

    '            '1.27.2017 Show the  Date TIme Picker drop down arrow in edit mode... 
    '            If TypeOf i Is DateTimePicker And i.Enabled = True Then
    '                i.Show()
    '            End If

    '            'Replace non-editable Checkbox with editable checkbox
    '            If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '                If i.Enabled = True Then
    '                    i.Show()
    '                    _myShadowCheckBox(CheckBoxCount).Hide()
    '                    CheckBoxCount += 1
    '                End If
    '            End If

    '        Next

    '        SetApproverState()

    '    Else
    '        'Replace editable controls with non-editable shadow controls
    '        'find and replace each Combo, Timepicker.  Make each existing textbox read only

    '        Dim counter As Integer = 1
    '        For Each i As Control In pnlReportHeader.Controls

    '            If TypeOf i Is TextBox Then
    '                DirectCast(i, TextBox).ReadOnly = True
    '                i.BackColor = _ColorNonEditableBackGround
    '                lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                counter = counter + 1
    '                i.Visible = True
    '            End If

    '            If TypeOf i Is RichTextBox Then
    '                DirectCast(i, RichTextBox).ReadOnly = True
    '                i.BackColor = _ColorNonEditableBackGround
    '                lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                counter = counter + 1
    '                i.Visible = True
    '            End If
    '            'added 1.27.2017, to Bind Date to FR DB ...FJB
    '            If TypeOf i Is MaskedTextBox Then
    '                DirectCast(i, MaskedTextBox).ReadOnly = True
    '                i.BackColor = _ColorNonEditableBackGround
    '                lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                counter = counter + 1
    '                i.Visible = True
    '            End If

    '            If TypeOf i Is ComboBox Then
    '                i.Hide()
    '                If i.Name = cbAccesslevel.Name Or i.Name = cbEditState.Name Then
    '                    i.Show()
    '                Else
    '                    '_myShadowTextBox(TextBoxCount).Size = i.Size
    '                    '_myShadowTextBox(TextBoxCount).Location = i.Location
    '                    _myShadowTextBox(TextBoxCount).Text = i.Text
    '                    _myShadowTextBox(TextBoxCount).ReadOnly = True

    '                    If _myShadowTextboxIndex(TextBoxCount).VisibleState = True Then
    '                        _myShadowTextBox(TextBoxCount).Show()
    '                    End If

    '                    _myShadowTextBox(TextBoxCount).BackColor = _ColorNonEditableBackGround

    '                End If
    '                TextBoxCount += 1
    '                lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                counter = counter + 1
    '            End If

    '            'Hide the Datepicker  Drop down button if not in an edit mode 
    '            If TypeOf i Is DateTimePicker Then
    '                i.Hide()
    '            End If

    '            'Find checkbox, and skip shadow checkbox...
    '            If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '                i.Hide()
    '                _myShadowCheckBox(CheckBoxCount).Show()
    '                CheckBoxCount += 1
    '                lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                counter = counter + 1
    '            ElseIf TypeOf i Is CheckBox Then
    '                ' MsgBox(i.Name)
    '            End If



    '        Next
    '    End If

    'End Sub



    '    ''' <summary>
    '    ''' This Sub sets the control state based on Access level and Edit State
    '    ''' </summary>
    '    ''' <param name="NewEditState">This is the New Edit State </param>
    '    ''' <remarks>Frank Boudreau 2015/2016</remarks>
    '    ''' 
    '    Private Sub SetEditState(ByVal NewEditState As eAccessState)

    '        gbFailure_ReportDataGridView_SafeToProcessEvents = False
    '        Try

    '            'Variable count the number of textboxes to keep track of which controls are enbled and visible
    '            'so that the user may edit
    '            Dim TextBoxCount As Integer = 0
    '            Dim CheckBoxCount As Integer = 0
    '            'Added the following three lines of code to try to improve graphics performance.
    '            Me.SuspendLayout()
    '            TableLayoutPanel1.SuspendLayout()
    '            pnlReportHeader.SuspendLayout()
    'EDIT:

    '            'Hide the read only textboxes and display the 
    '            'Require that the user has a certain access level to get into edit view
    '            ' If NewEditState = eAccessState.EDIT Then 'enter Edit mode
    '            If NewEditState = eAccessState.CR_EDIT And _CurrentUser.AccessLevel >= eAccessState.CREATE_NEW Then 'enter Edit mode

    '                'Disable Navigation buttons
    '                btnLastRecord.Enabled = False
    '                btnNextRecord.Enabled = False
    '                txtPosition.Enabled = False
    '                btnPreviousRecord.Enabled = False
    '                btnFirstRecord.Enabled = False

    '                'Enable Controls to Add and remove attachments            
    '                '  btnAddAttachment.Enabled = True
    '                '  btnRemoveAttachment.Enabled = True

    '                'Set the Editmode active Flag to true
    '                _EditState = eAccessState.CR_EDIT

    '                'Set the Record Saved flag to false
    '                _RecordSaved = False

    '                'Disable gridview so that the user cannot change records by clicking on the the gridview in edit mode
    '                Failure_ReportDataGridView.Enabled = False
    '                'But make it editable
    '                Failure_ReportDataGridView.ReadOnly = False

    '                Select Case _CurrentUser.AccessLevel

    '                    Case eAccessState.CREATE_NEW
    '                        'FR Creators are allowed to edit Headers and Failure descriptions...
    '                        Enable_EditTestInfo(True)
    '                        Enable_MeterGroup(False)
    '                        Enable_AMRGroup(False)
    '                        Enable_EDIT_Controls(False)

    '                    Case eAccessState.CR_EDIT
    '                        Enable_EDIT_Controls(True)

    '                        tsmFileNew.Visible = True
    '                        tsmFileNew.Enabled = False 'toggle off since in EditState

    '                        tsmAdminEdit.Visible = True
    '                        tsmAdminEdit.Enabled = False ' toggle off since in EditState

    '                        tsmFileUpdate.Visible = True
    '                        tsmFileUpdate.Enabled = True 'toggle to true to enable saveing

    '                        tsmFileSaveAndClose.Visible = True
    '                        tsmFileSaveAndClose.Enabled = True 'toggle to true to enable saveing

    '                        tsmCancel.Visible = True
    '                        tsmCancel.Enabled = True 'toggle to true allow cancle

    '                        tsmDelete.Visible = False 'Since access level is Edit
    '                        tsmDelete.Enabled = False 'Since access level is Edit

    '                    Case eAccessState.ADMIN
    '                        Enable_EDIT_Controls(True)
    '                        Enable_EditTestInfo(True)
    '                        ' Enable_MeterGroup(True)
    '                        ' Enable_AMRGroup(True)

    '                        dtpDateApproved.Enabled = True
    '                        mtxtDateApproved.ReadOnly = False
    '                        dtpDateClosed.Enabled = True
    '                        mtxtDateClosed.ReadOnly = False

    '                        cbTCC_1.Enabled = True
    '                        cbTCC_2.Enabled = True
    '                        cbTCC_2.Enabled = True
    '                        cbTCC_3.Enabled = True
    '                        cbTCC_5.Enabled = True

    '                        tsmFileNew.Visible = True
    '                        tsmFileNew.Enabled = False 'toggle off since in EditState

    '                        tsmAdminEdit.Visible = True
    '                        tsmAdminEdit.Enabled = False ' toggle off since in EditState

    '                        tsmFileUpdate.Visible = True
    '                        tsmFileUpdate.Enabled = True 'toggle to true to enable saveing

    '                        tsmFileSaveAndClose.Visible = True
    '                        tsmFileSaveAndClose.Enabled = True 'toggle to true to enable saveing

    '                        tsmCancel.Visible = True
    '                        tsmCancel.Enabled = True 'toggle to true allow cancle

    '                        tsmDelete.Visible = True  'Since access level is Admin
    '                        tsmDelete.Enabled = True 'Since access level is Admin
    '                End Select

    '                DisplayShadowControl()




    'READ_ONLY:

    '            ElseIf NewEditState = eAccessState.READ_ONLY Then
    '                gbFailure_ReportDataGridView_SafeToProcessEvents = False
    '                'First check to see if user wants to save data...

    '                If _RecordSaved = False Then
    '                    Dim Response
    '                    Response = MsgBox("Are you sure you want to disgard changes?", vbYesNo)

    '                    If Response = vbNo Then
    '                        'Stay in Editmode exit thgie sub
    '                        Exit Sub
    '                    End If
    '                    ' _RecordSaved = True
    '                End If


    '                'Set Control to Correct state
    '                Failure_ReportDataGridView.Enabled = True
    '                btnLastRecord.Enabled = True
    '                btnNextRecord.Enabled = True
    '                txtPosition.Enabled = True
    '                btnPreviousRecord.Enabled = True
    '                btnFirstRecord.Enabled = True
    '                '  btnAddAttachment.Enabled = False
    '                '  btnRemoveAttachment.Enabled = False

    '                tsmExit.Enabled = True
    '                tsmView.Enabled = True
    '                txtAssignedTo.Enabled = True
    '                txtCorrectedBy.Enabled = True
    '                txtApprovedBy.Enabled = True
    '                dtpDateApproved.Enabled = True
    '                dtpDateCorrected.Enabled = True
    '                dtpDateClosed.Enabled = True
    '                cbTCC_1.Enabled = True
    '                cbTCC_2.Enabled = True
    '                cbTCC_3.Enabled = True
    '                cbTCC_4.Enabled = True
    '                cbTCC_5.Enabled = True
    '                'Replace editable controls with non-editable shadow controls
    '                'find and replace each Combo, Timepicker.  Make each existing textbox read only
    '                _EditState = eAccessState.READ_ONLY
    '                Dim counter As Integer = 1
    '                For Each i As Control In pnlReportHeader.Controls

    '                    If TypeOf i Is TextBox Then
    '                        DirectCast(i, TextBox).ReadOnly = True
    '                        i.BackColor = _ColorNonEditableBackGround
    '                        lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                        counter = counter + 1
    '                        i.Visible = True
    '                    End If

    '                    If TypeOf i Is RichTextBox Then
    '                        DirectCast(i, RichTextBox).ReadOnly = True
    '                        i.BackColor = _ColorNonEditableBackGround
    '                        lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                        counter = counter + 1
    '                        i.Visible = True
    '                    End If
    '                    'added 1.27.2017, to Bind Date to FR DB ...FJB
    '                    If TypeOf i Is MaskedTextBox Then
    '                        DirectCast(i, MaskedTextBox).ReadOnly = True
    '                        i.BackColor = _ColorNonEditableBackGround
    '                        lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                        counter = counter + 1
    '                        i.Visible = True
    '                    End If

    '                    If TypeOf i Is ComboBox Then
    '                        i.Hide()
    '                        '_myShadowTextBox(TextBoxCount).Size = i.Size
    '                        '_myShadowTextBox(TextBoxCount).Location = i.Location
    '                        _myShadowTextBox(TextBoxCount).Text = i.Text
    '                        _myShadowTextBox(TextBoxCount).ReadOnly = True

    '                        If _myShadowTextboxIndex(TextBoxCount).VisibleState = True Then
    '                            _myShadowTextBox(TextBoxCount).Show()
    '                        End If

    '                        _myShadowTextBox(TextBoxCount).BackColor = _ColorNonEditableBackGround
    '                        TextBoxCount += 1
    '                        lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                        counter = counter + 1
    '                    End If

    '                    'Hide the Datepicker  Drop down button if not in an edit mode 
    '                    If TypeOf i Is DateTimePicker Then
    '                        i.Hide()
    '                    End If

    '                    'Find checkbox, and skip shadow checkbox...
    '                    If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '                        i.Hide()
    '                        _myShadowCheckBox(CheckBoxCount).Show()
    '                        CheckBoxCount += 1
    '                        lbLog.Items.Add(counter.ToString + ": " + i.Name)
    '                        counter = counter + 1
    '                    ElseIf TypeOf i Is CheckBox Then
    '                        ' MsgBox(i.Name)
    '                    End If



    '                Next

    '                'now turn on as appropriate controls based on access level and State
    '                'Read only acccess level...
    '                If _CurrentUser.AccessLevel = eAccessState.READ_ONLY Then

    '                    'hide and disabled
    '                    tsmFileNew.Visible = False
    '                    tsmFileNew.Enabled = False
    '                    tsmFileUpdate.Visible = False
    '                    tsmFileUpdate.Enabled = False
    '                    tsmFileSaveAndClose.Visible = False
    '                    tsmFileSaveAndClose.Enabled = False 'toggle to true to enable saveing
    '                    tsmCancel.Visible = False
    '                    tsmCancel.Enabled = False


    '                    'hide and disabled
    '                    tsmAdminEdit.Visible = False
    '                    tsmAdminEdit.Enabled = False

    '                    'hide and disabled
    '                    tsmDelete.Visible = False
    '                    tsmDelete.Enabled = False
    '                End If

    '                'Create Access Level, HQA test...
    '                If _CurrentUser.AccessLevel = eAccessState.CREATE_NEW Then

    '                    'Show and Enabled
    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = True


    '                    'Show and Disabled
    '                    tsmFileUpdate.Visible = True
    '                    tsmFileUpdate.Enabled = False
    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = False 'toggle to true to enable saveing

    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = False

    '                    'Hide and disabled
    '                    tsmAdminEdit.Visible = False
    '                    tsmAdminEdit.Enabled = False

    '                    'Hide and disabled
    '                    tsmDelete.Visible = False
    '                    tsmDelete.Enabled = False
    '                End If

    '                'Full edit rights, all Engineers
    '                If _CurrentUser.AccessLevel = eAccessState.CR_EDIT Then

    '                    'Show and Enabled
    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = True
    '                    tsmAdminEdit.Visible = True
    '                    tsmAdminEdit.Enabled = True

    '                    'Show and Disabled
    '                    tsmFileUpdate.Visible = True
    '                    tsmFileUpdate.Enabled = False
    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = False 'toggle to true to enable saveing
    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = False

    '                    'Hide and disabled
    '                    tsmDelete.Visible = False
    '                    tsmDelete.Enabled = False
    '                End If

    '                'Only an administator can delete a record
    '                If _CurrentUser.AccessLevel = eAccessState.ADMIN Then
    '                    'Show and Enabled
    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = True
    '                    tsmAdminEdit.Visible = True
    '                    tsmAdminEdit.Enabled = True

    '                    'Show and Disabled
    '                    tsmFileUpdate.Visible = True
    '                    tsmFileUpdate.Enabled = False
    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = False 'toggle to true to enable saveing
    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = False
    '                    tsmDelete.Visible = True
    '                    tsmDelete.Enabled = False
    '                End If



    '                rtxtFailureDescription.ReadOnly = True
    '                rtxtFailureDescription.BackColor = _ColorNonEditableBackGround
    '                rtxtCorrectiveAction.ReadOnly = True
    '                rtxtCorrectiveAction.BackColor = _ColorNonEditableBackGround
    '                rtxtEngineeringNotes.ReadOnly = True
    '                rtxtTCC_Comments.ReadOnly = True
    '                rtxtEngineeringNotes.BackColor = _ColorNonEditableBackGround
    '                rtxtTCC_Comments.BackColor = _ColorNonEditableBackGround
    '                Failure_ReportDataGridView.ReadOnly = True

    '                'do the update last so user doesn't accidently 
    '                'This restores the Database data to the the controls
    '                If _RecordSaved = False Then
    '                    Cursor = Cursors.WaitCursor
    '                    'cancel the edit
    '                    lbLog.Items.Add("Start Cancel Edit DB: " + Now)
    '                    gMyFailureReportBindingSource.CancelEdit()
    '                    lbLog.Items.Add("End Cancel Edit DB: " + Now)
    '                    'clear the datatable
    '                    'gMyFailureReportDataTable.Clear()
    '                    'reload the original data for the current record only...
    '                    lbLog.Items.Add("Start Database Access: " + Now)
    '                    gMyFailureReportOLEDBDataAdaptor.Fill(CInt(txtPosition.Text), 1, gMyFailureReportDataTable)
    '                    lbLog.Items.Add("Finish Database Access: " + Now)
    '                    'Set the saved flag
    '                    _RecordSaved = True
    '                    Cursor = Cursors.Default
    '                End If
    '                ' gbFailure_ReportDataGridView_SafeToProcessEvents = True
    '                Me.Refresh()

    'CREATE_NEW:
    '            ElseIf NewEditState = eAccessState.CREATE_NEW And _CurrentUser.AccessLevel > eAccessState.READ_ONLY Then
    '                'Disable Navigation buttons
    '                btnLastRecord.Enabled = False
    '                btnNextRecord.Enabled = False
    '                txtPosition.Enabled = False
    '                btnPreviousRecord.Enabled = False
    '                btnFirstRecord.Enabled = False
    '                'Set the Editmode active Flag to true
    '                ' _EditState = eControlState.EDIT
    '                'Set the Record Saved flag to false
    '                _RecordSaved = False
    '                'Disable gridview so that the user cannot change records by clicking on the the gridview
    '                'in edit mode
    '                Failure_ReportDataGridView.Enabled = False

    '                'Store all of the current data in each of the controls in the header. If the user decides to abort saving then
    '                'all the orignal data can be stored here.
    '                ReDim _strHeaderBackUpData(pnlReportHeader.Controls.Count)
    '                For j = 0 To pnlReportHeader.Controls.Count - 1
    '                    _strHeaderBackUpData(j) = pnlReportHeader.Controls.Item(j).Text
    '                Next

    '                'Set each control to edit view contained in the Panel Header.
    '                '   Select based on control type and cast to type to access properties 
    '                '   Set Read Only to False (For Text boxes)
    '                '   Set the background to white to que the user that the record is in edit mode
    '                '   Exchange the Shadow Readonly textboxes with the Combo, Date Time picker, or editable
    '                '   Checkbox so the user can now edit the record
    '                For Each i As Control In pnlReportHeader.Controls

    '                    'Set the Textboxes state
    '                    If TypeOf i Is TextBox Then
    '                        DirectCast(i, TextBox).ReadOnly = False
    '                        i.BackColor = System.Drawing.Color.White
    '                    End If

    '                    If TypeOf i Is RichTextBox Then
    '                        DirectCast(i, RichTextBox).ReadOnly = False
    '                        i.BackColor = System.Drawing.Color.White
    '                    End If

    '                    'added 1.27.2017 to bind Dates to FRDB ...FJB
    '                    If TypeOf i Is MaskedTextBox Then
    '                        DirectCast(i, MaskedTextBox).ReadOnly = False
    '                        i.BackColor = System.Drawing.Color.White
    '                    End If

    '                    'Replace with editable Combobox
    '                    If TypeOf i Is ComboBox Then
    '                        i.Show()
    '                        _myShadowTextboxIndex(TextBoxCount).VisibleState = _myShadowTextBox(TextBoxCount).Visible
    '                        _myShadowTextboxIndex(TextBoxCount).VisibleState = True
    '                        '_myShadowTextBoxes(TextBoxCount).Hide()
    '                        TextBoxCount += 1
    '                    End If
    '                    '1.27.2017 Display the Date picker drop doen button...FJB 
    '                    If TypeOf i Is DateTimePicker Then
    '                        i.Show()
    '                        'Removed 1.27.2017, DTP no longer bind DAtes to FRDB...FJB
    '                        'If i.Name <> dtpTestDateTime.Name Then
    '                        '    TextBoxCount += 1

    '                        '    _myShadowTextboxIndex(TextBoxCount).VisibleState = _myShadowTextBox(TextBoxCount).Visible
    '                        '    ' _myShadowTextBox(TextBoxCount).Hide()
    '                        '    '_myControlIndex(TextBoxCount).VisableState = False
    '                        '    TextBoxCount += 1
    '                        'End If
    '                    End If
    '                    'Replace non-editable Checkbox with editable checkbox
    '                    If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '                        i.Show()
    '                        _myShadowCheckBox(CheckBoxCount).Hide()
    '                        CheckBoxCount += 1
    '                    End If

    '                Next

    '                'The purpose of this code is to provide a means to prevent overwriting data, add
    '                'the author of the edit, and provide a means to restore the orginal text if the user chooses not save.
    '                'Get the current Date
    '                _CurrentDate = Now

    '                'Make only the Failure DesciptionTexbox editable when creating a new Failure Report

    '                rtxtFailureDescription.ReadOnly = False
    '                rtxtCorrectiveAction.ReadOnly = True
    '                rtxtEngineeringNotes.ReadOnly = True
    '                rtxtTCC_Comments.ReadOnly = True

    '                'This is the length of the orginal data in the textbox
    '                _myTxtReportDescriptionBackUpLength = rtxtFailureDescription.TextLength
    '                _myTxtCorrectiveActionBackupLength = rtxtCorrectiveAction.TextLength
    '                _myEngineeringNotesBackUpLength = rtxtEngineeringNotes.TextLength
    '                _myTCC_CommentsBackupLength = rtxtTCC_Comments.TextLength


    '                'Add user timestamp
    '                'This is the orginal data in the textbox with the current user and date appended. 
    '                _myTxtReportDescriptionBackUpEdit = rtxtFailureDescription.Text + vbCrLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbCrLf
    '                _myTxtReportDescriptionBackUp = _myTxtReportDescriptionBackUpEdit
    '                rtxtFailureDescription.BackColor = System.Drawing.Color.White

    '                _myTxtCorrectiveActionBackupEdit = rtxtCorrectiveAction.Text + vbCrLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbCrLf
    '                _myTxtCorrectiveActionBackUp = _myTxtCorrectiveActionBackupEdit
    '                rtxtCorrectiveAction.BackColor = System.Drawing.Color.White

    '                _myEngineeringNotesBackupEdit = rtxtEngineeringNotes.Text + vbCrLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbCrLf
    '                _myEngineeringNotesBackup = _myEngineeringNotesBackupEdit
    '                rtxtEngineeringNotes.BackColor = System.Drawing.Color.White

    '                _myTCC_CommentsBackupEdit = rtxtTCC_Comments.Text + vbCrLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbCrLf
    '                _myTCC_CommentsBackup = _myTCC_CommentsBackupEdit
    '                rtxtTCC_Comments.BackColor = System.Drawing.Color.White


    '                Failure_ReportDataGridView.ReadOnly = False



    '                'Switch to FR View
    '                tsmViewFR_View.PerformClick()

    '                'DisableControls that should not be editable when creating a new failure Report
    '                tsmFileNew.Enabled = False
    '                tsmExit.Enabled = False
    '                tsmView.Enabled = False
    '                txtAssignedTo.Enabled = True 'Project Lead
    '                txtCorrectedBy.Enabled = False
    '                txtApprovedBy.Enabled = False
    '                dtpDateApproved.Enabled = False
    '                dtpDateCorrected.Enabled = False
    '                dtpDateClosed.Enabled = False
    '                cbTCC_1.Enabled = False
    '                cbTCC_2.Enabled = False
    '                cbTCC_3.Enabled = False
    '                cbTCC_4.Enabled = False
    '                cbTCC_5.Enabled = False

    '                'explicitly Enable for now
    '                cbEUTType.Enabled = True
    '                cbEUTType.SelectedIndex = 0

    '                ''Readonly can;t get in here so comment out the code
    '                ''now turn on as appropriate controls based on access level and State
    '                'If _CurrentUser.AccessLevel = eEditState.READ_ONLY Then
    '                '    'hide and disabled
    '                '    tsmNew.Visible = False
    '                '    tsmNew.Enabled = False
    '                '    tsmSave.Visible = False
    '                '    tsmSave.Enabled = False
    '                '    tsmCancel.Visible = False
    '                '    tsmCancel.Enabled = False
    '                '    btnCreateNewFR.Hide()
    '                '    btnCreateNewFR.Enabled = False
    '                '    btnSaveNewFailureReport.Hide()
    '                '    btnSaveNewFailureReport.Enabled = False

    '                '    'hide and disabled
    '                '    tsmEdit.Visible = False
    '                '    tsmEdit.Enabled = False
    '                '    btnEditView.Hide()
    '                '    btnEditView.Enabled = False
    '                '    btnUpdateReport.Hide()
    '                '    btnUpdateReport.Enabled = False

    '                '    'hide and disabled
    '                '    tsmDelete.Visible = False
    '                '    tsmDelete.Enabled = False
    '                'End If

    '                'now turn on as appropriate controls based on access level and State
    '                If _CurrentUser.AccessLevel = eAccessState.CREATE_NEW Then

    '                    'Show and Enabled
    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = False 'Since alleady in Create State

    '                    tsmFileUpdate.Visible = True 'Since access level is Create_New
    '                    tsmFileUpdate.Enabled = True 'Since in create state

    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = True 'toggle to true to enable saveing

    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = True

    '                    '  btnAddAttachment.Enabled = True
    '                    '  btnRemoveAttachment.Enabled = True

    '                    'Hide and disabled since access level is CREATE_NEW
    '                    tsmAdminEdit.Visible = False
    '                    tsmAdminEdit.Enabled = False
    '                    tsmDelete.Visible = False
    '                    tsmDelete.Enabled = False
    '                End If

    '                'now turn on as appropriate controls based on access level and State
    '                'Full edit rights, all Engineers
    '                If _CurrentUser.AccessLevel = eAccessState.CR_EDIT Then


    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = False 'toggle off since in CreateState


    '                    tsmAdminEdit.Visible = True
    '                    tsmAdminEdit.Enabled = False ' toggle off since in CreateState

    '                    tsmFileUpdate.Visible = True
    '                    tsmFileUpdate.Enabled = True 'toggle to true to enable saveing

    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = True 'toggle to true to enable saveing

    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = True 'toggle to true allow cancle

    '                    '  btnAddAttachment.Enabled = True
    '                    ' btnRemoveAttachment.Enabled = True

    '                    tsmDelete.Visible = False 'Since access level is Edit
    '                    tsmDelete.Enabled = False 'Since access level is Edit
    '                End If
    '                'now turn on as appropriate controls based on access level and State
    '                'Only an administator can delete a record
    '                If _CurrentUser.AccessLevel = eAccessState.ADMIN Then

    '                    tsmFileNew.Visible = True
    '                    tsmFileNew.Enabled = False 'toggle off since in CreateState


    '                    tsmAdminEdit.Visible = True
    '                    tsmAdminEdit.Enabled = False ' toggle off since in CreateState


    '                    tsmFileUpdate.Visible = True
    '                    tsmFileUpdate.Enabled = True 'toggle to true to enable saveing

    '                    tsmFileSaveAndClose.Visible = True
    '                    tsmFileSaveAndClose.Enabled = True 'toggle to true to enable saveing

    '                    tsmCancel.Visible = True
    '                    tsmCancel.Enabled = True 'toggle to true allow cancle

    '                    '  btnAddAttachment.Enabled = True
    '                    ' btnRemoveAttachment.Enabled = True



    '                    tsmDelete.Visible = True 'Since access level is Edit
    '                    tsmDelete.Enabled = False 'Since state is create state
    '                End If


    '                If cbEUTType.Text.Trim = "" Then
    '                    SetSubState(eSubState.SET_EUT_DEFAULTS)
    '                End If


    '                'set Edit state
    '                _EditState = eAccessState.CREATE_NEW

    'ADMIN:
    '            ElseIf NewEditState = eAccessState.ADMIN And _CurrentUser.AccessLevel = eAccessState.ADMIN Then
    '                ''This state only enables the Delete button, Create State and Edit State control if the 
    '                'tsmDelete.Enabled = True 'Since state is create state
    '                '_EditState = eAccessState.ADMIN

    '            End If





    '        Catch ex As Exception
    '            MsgBox("Error Setting Edit State" + vbCrLf + ex.ToString)

    '        End Try
    '        'Sets the view depending on if TCC approval is needed or not
    '        gbFailure_ReportDataGridView_SafeToProcessEvents = True
    '        SetApproverState()
    '        'Resume updateing and drawing
    '        TableLayoutPanel1.ResumeLayout()
    '        pnlReportHeader.ResumeLayout()
    '        Me.ResumeLayout()

    '    End Sub 'set editstate
    ' Private Sub SetSubState(ByVal MySubState As eSubState)
    'If MySubState = eSubState.SET_EUT_DEFAULTS Then
    '    For Each i As Control In pnlReportHeader.Controls
    '        'Handle Combobox Enable/disable State
    '        'Replace with editable Combobox
    '        If TypeOf i Is ComboBox Then

    '            If InStr(i.Name.ToString, "AMR") > 0 And (cbEUTType.SelectedIndex = EUTtype.AMI Or cbEUTType.SelectedIndex = EUTtype.AMR_ONLY) Then
    '                i.Enabled = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 And cbEUTType.SelectedIndex = EUTtype.NONE_SELECTED Then
    '                i.Enabled = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 And cbEUTType.SelectedIndex = EUTtype.OTHER Then
    '                i.Enabled = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 Then
    '                i.Enabled = False
    '                i.Text = "N/A"
    '            End If

    '            If InStr(i.Name.ToString, "Meter") > 0 And (cbEUTType.SelectedIndex = EUTtype.METER_ONLY Or cbEUTType.SelectedIndex = EUTtype.AMI) Then
    '                i.Enabled = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 And cbEUTType.SelectedIndex = EUTtype.NONE_SELECTED Then
    '                i.Enabled = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 And cbEUTType.SelectedIndex = EUTtype.OTHER Then
    '                i.Enabled = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 Then
    '                i.Enabled = False
    '                i.Text = "N/A"
    '            End If
    '        ElseIf TypeOf i Is RichTextBox Then
    '            Dim MyTextBox As RichTextBox = DirectCast(i, RichTextBox)
    '            If InStr(i.Name.ToString, "AMR") > 0 And (cbEUTType.SelectedIndex = EUTtype.AMI Or cbEUTType.SelectedIndex = EUTtype.AMR_ONLY) Then
    '                MyTextBox.ReadOnly = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 And cbEUTType.SelectedIndex = EUTtype.NONE_SELECTED Then
    '                MyTextBox.ReadOnly = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 And cbEUTType.SelectedIndex = EUTtype.OTHER Then
    '                MyTextBox.ReadOnly = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "AMR") > 0 Then
    '                MyTextBox.ReadOnly = True
    '                i.Text = "N/A"
    '            End If

    '            If InStr(i.Name.ToString, "Meter") > 0 And (cbEUTType.SelectedIndex = EUTtype.METER_ONLY Or cbEUTType.SelectedIndex = EUTtype.AMI) Then
    '                MyTextBox.ReadOnly = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 And cbEUTType.SelectedIndex = EUTtype.NONE_SELECTED Then
    '                MyTextBox.ReadOnly = True
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 And cbEUTType.SelectedIndex = EUTtype.OTHER Then
    '                MyTextBox.ReadOnly = False
    '                i.Text = ""
    '            ElseIf InStr(i.Name.ToString, "Meter") > 0 Then
    '                MyTextBox.ReadOnly = True
    '                i.Text = "N/A"
    '            End If
    '        End If
    '        If cbEUTType.SelectedIndex = EUTtype.OTHER Then
    '            lblMeter.Text = "EUT 1"
    '            lblAMR.Text = "EUT 2"
    '        Else
    '            lblMeter.Text = "Meter"
    '            lblAMR.Text = "AMR"
    '        End If
    '    Next
    '    DisplayShadowControl()
    'End If

    '_SubState = eSubState.IDLE
    '  End Sub

    'Public Sub Enable_EDIT_Controls(bEnabled As Boolean)
    '    'Enable Failure Report Controls Controls if Disabled 
    '    txtAssignedTo.ReadOnly = Not bEnabled
    '    txtCorrectedBy.ReadOnly = Not bEnabled
    '    txtApprovedBy.ReadOnly = Not bEnabled

    '    dtpDateCorrected.Enabled = bEnabled
    '    mtxtDateCorrected.ReadOnly = Not bEnabled

    '    rtxtCorrectiveAction.ReadOnly = Not bEnabled
    '    rtxtEngineeringNotes.ReadOnly = Not bEnabled
    '    rtxtTCC_Comments.ReadOnly = Not bEnabled

    '    'Meter Only
    '    'AMI
    '    'AMR Only
    '    'OTHER EUT

    '    If cbEUTType.Text = "Meter Only" Then
    '        Enable_MeterGroup(bEnabled)
    '        Enable_AMRGroup(False)
    '        lblMeter.Text = "Meter"
    '        lblAMR.Text = "AMR"
    '    ElseIf cbEUTType.Text = "AMR Only" Then
    '        Enable_MeterGroup(False)
    '        Enable_AMRGroup(bEnabled)
    '        lblMeter.Text = "Meter"
    '        lblAMR.Text = "AMR"
    '    ElseIf cbEUTType.Text = "AMI" Then
    '        Enable_MeterGroup(bEnabled)
    '        Enable_AMRGroup(bEnabled)
    '        lblMeter.Text = "Meter"
    '        lblAMR.Text = "AMR"
    '    ElseIf cbEUTType.Text = "Other EUT" Then
    '        Enable_MeterGroup(bEnabled)
    '        Enable_AMRGroup(bEnabled)
    '        lblMeter.Text = "EUT 1"
    '        lblAMR.Text = "EUT 2"
    '    Else
    '        Enable_MeterGroup(False)
    '        Enable_AMRGroup(False)
    '        lblMeter.Text = "Meter"
    '        lblAMR.Text = "AMR"
    '    End If
    'End Sub
End Module
