

Public Class frmFilter

    Dim _EnableControls As Boolean = False
    Dim _FilterList As List(Of String)

    Private Sub CloseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub btnFilterCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterReset.Click

        ResetFilter()
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        frmFailureBrowser.gMyFailureReportBindingSource.RemoveFilter()
        frmFailureBrowser.dgvFailureReportDataGridView.Show()
        txtFilterDisplay.Text = ""
      
    End Sub

    Private Sub Initialize_Filter_Elements(Optional ByVal PreserveCurrentFilter As Boolean = False)
        Dim CurrentFilter As String = txtFilterDisplay.Text

        For Each i As Control In Panel1.Controls
            If TypeOf i Is xboXComboBox Then
                Dim MYxboXComboBox As New xboXComboBox
                MYxboXComboBox = DirectCast(i, xboXComboBox)
                i.Text = ""
                Try
                    'Try removing the Handler... 
                    RemoveHandler MYxboXComboBox.DragDrop, AddressOf cbFilterComboDropDown_Enter
                Catch ex As Exception
                    Dim MyStop As Integer = 1
                End Try

                AddHandler MYxboXComboBox.DropDown, AddressOf cbFilterComboDropDown_Enter
            ElseIf TypeOf i Is TextBox Then
                Dim myTextbox As New TextBox
                myTextbox = DirectCast(i, TextBox)
                i.Text = ""
            End If
        Next

        For Each i As Control In gbFilterEUT_1.Controls
            If TypeOf i Is xboXComboBox Then
                Dim MYxboXComboBox As New xboXComboBox
                MYxboXComboBox = DirectCast(i, xboXComboBox)
                i.Text = ""

                Try
                    RemoveHandler MYxboXComboBox.DragDrop, AddressOf cbFilterComboDropDown_Enter
                Catch ex As Exception
                    Dim MyStop As Integer = 1
                End Try
                AddHandler MYxboXComboBox.DropDown, AddressOf cbFilterComboDropDown_Enter
            ElseIf TypeOf i Is TextBox Then
                Dim myTextbox As New TextBox
                myTextbox = DirectCast(i, TextBox)
                i.Text = ""
            End If
        Next

        For Each i As Control In gbFilterEUT2.Controls
            If TypeOf i Is xboXComboBox Then
                Dim MYxboXComboBox As New xboXComboBox
                MYxboXComboBox = DirectCast(i, xboXComboBox)
                i.Text = ""
                Try
                    RemoveHandler MYxboXComboBox.DragDrop, AddressOf cbFilterComboDropDown_Enter
                Catch ex As Exception
                    Dim MyStop As Integer = 1
                End Try
                AddHandler MYxboXComboBox.DropDown, AddressOf cbFilterComboDropDown_Enter
            ElseIf TypeOf i Is TextBox Then
                Dim myTextbox As New TextBox
                myTextbox = DirectCast(i, TextBox)
                i.Text = ""
            End If
        Next

        dtpFilterFailedFrom.CustomFormat = " "
        dtpFilterFailedTo.CustomFormat = " "

        dtpFilterClosedFrom.CustomFormat = " "
        dtpFilterClosedTo.CustomFormat = " "

        dtpFilterCorrectedFrom.CustomFormat = " "
        dtpFilterCorrectedFrom.CustomFormat = " "

        dtpFilterApprovedFrom.CustomFormat = " "
        dtpFilterApprovedTo.CustomFormat = " "

        ' rbDisableReadyForReviewFilter.Checked = True
        '  rbReportCatAll.Checked = True

        If PreserveCurrentFilter Then
            txtFilterDisplay.Text = CurrentFilter
        End If

    End Sub

    Private Sub ResetFilter(Optional PreserveCurrentFilter As Boolean = False)
        Dim CurrentFilter As String = txtFilterDisplay.Text

        For Each i As Control In Panel1.Controls
            If TypeOf i Is xboXComboBox Then
                i.Text = ""
            ElseIf TypeOf i Is TextBox Then
                i.Text = ""
            End If
        Next

        For Each i As Control In gbFilterEUT_1.Controls
            If TypeOf i Is xboXComboBox Then
                i.Text = ""
            ElseIf TypeOf i Is TextBox Then
                i.Text = ""
            End If
        Next

        For Each i As Control In gbFilterEUT2.Controls
            If TypeOf i Is xboXComboBox Then
                i.Text = ""
            ElseIf TypeOf i Is TextBox Then
                i.Text = ""
            End If
        Next

        dtpFilterFailedFrom.CustomFormat = " "
        dtpFilterFailedTo.CustomFormat = " "

        dtpFilterClosedFrom.CustomFormat = " "
        dtpFilterClosedTo.CustomFormat = " "

        dtpFilterCorrectedFrom.CustomFormat = " "
        dtpFilterCorrectedFrom.CustomFormat = " "

        dtpFilterApprovedFrom.CustomFormat = " "
        dtpFilterApprovedTo.CustomFormat = " "

        ' rbDisableReadyForReviewFilter.Checked = True
        '  rbReportCatAll.Checked = True

        If PreserveCurrentFilter Then
            txtFilterDisplay.Text = CurrentFilter
        End If
    End Sub

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Initialize_Filter_Elements()

        For Each Control In Panel1.Controls
            If TypeOf Control Is CheckBox Then
                If Control.name <> chkFilterOpenReportsOnly.Name Then
                    Dim MyCheckbox As CheckBox = DirectCast(Control, CheckBox)
                    AddHandler MyCheckbox.CheckedChanged, AddressOf And_OR_Check_Changed_CheckedChanged
                End If
            End If

        Next

        For Each Control In gbFilterEUT_1.Controls
            If TypeOf Control Is CheckBox Then
                Dim MyCheckbox As CheckBox = DirectCast(Control, CheckBox)
                AddHandler MyCheckbox.CheckedChanged, AddressOf And_OR_Check_Changed_CheckedChanged
            End If
        Next

        For Each Control In gbFilterEUT2.Controls
            If TypeOf Control Is CheckBox Then

                Dim MyCheckbox As CheckBox = DirectCast(Control, CheckBox)
                AddHandler MyCheckbox.CheckedChanged, AddressOf And_OR_Check_Changed_CheckedChanged

            End If
        Next

        For Each Control As Control In gbFilterControls.Controls
            If TypeOf Control Is CheckBox And Control.Name = ChkPreserveFilterAndOr.Name Then

                Dim MyCheckbox As CheckBox = DirectCast(Control, CheckBox)
                AddHandler MyCheckbox.CheckedChanged, AddressOf And_OR_Check_Changed_CheckedChanged

            End If
        Next

        Dim MyStop As Integer = 1

    End Sub

    Private Function SelectAndOr(ControlName As String) As String
        Select Case ControlName
            Case cbFilterLevel.Name
                If chkTestLevelAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterTestType.Name
                If chkFilterTestsAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterTests.Name
                If chkFilterTestsAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterProjectNumber.Name
                If chkFilterProjectNumberAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterProjectName.Name
                If chkFilterProjectNameAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterAssignedTo.Name
                If chkFilterAssignedToAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterCorrectedBy.Name
                If chkFilterCorrectedByAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbFilterEUTType.Name
                If chkFilterEUTTypeAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterManufacturer.Name
                If chkMeterManufacturerAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterModel.Name
                If chkMeterModelAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterType.Name
                If chkMeterTypeAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterSubType.Name
                If chkMeterSubTypeAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterSubTypeII.Name
                If chkMeterSubTypeIIAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterSerialNumber.Name
                If chkMeterSerialNumberAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterForm.Name
                If chkMeterFormAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterBase.Name
                'If chkTestLevelAndOr.CheckState = CheckState.Checked Then
                '    Return " OR "
                'Else
                '    Return " AND "
                'End If

                Return " AND "
            Case cbMeterDSP_Rev.Name
                If chkMeterDSP_RevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterFW_Ver.Name
                If chkMeterFW_VerAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterPCBA.Name
                If chkMeterPCBAAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterPCBA_Rev.Name
                If chkMeterPCBA_RevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterSoftware.Name
                If chkMeterSoftwareAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterSoftwareRev.Name
                If chkMeterSoftwareRevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbMeterVoltage.Name
                If chkMeterVoltageAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_Manufacturer.Name
                If chkAMR_ManufacturerAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_Model.Name
                If chkAMR_ModelAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_TYPE.Name
                If chkAMR_TYPEAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_SubType.Name
                If chkAMR_SubTypeAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_SUBtypeII.Name
                If chkAMR_SubTypeIIAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_SUBtypeIII.Name
                If chkAMR_SUBtypeIIIAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_SerialNumber.Name
                If chkAMR_SerialNumberAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_IP_LAN_ID.Name
                If chkAMR_IP_LAN_IDAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_FW_Rev.Name
                If chkAMR_FW_RevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_PCBA_PN.Name
                If chkAMR_PCBA_PNAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_PCBA_Rev.Name
                If chkAMR_PCBA_RevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_Software.Name
                If chkAMR_SoftwareAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_Software_Rev.Name
                If chkAMR_Software_RevAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbAMR_Voltage.Name
                If chkAMR_VoltageAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case cbApprovedBy.Name
                If chkApprovedByAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If
            Case ChkPreserveFilterAndOr.Name
                If ChkPreserveFilterAndOr.CheckState = CheckState.Checked Then
                    Return " OR "
                Else
                    Return " AND "
                End If

            Case Else
                Return " AND "

        End Select
    End Function


    ''' <summary>
    ''' This function Returns the Filter Text based on Combo box selection.  Since some Comboboxes are 
    ''' in group boxes, it will search thorough each combobox and add the filter for each combobox
    ''' The Tag of each combo box must be the name of the column in the FR table in the database
    ''' </summary>
    ''' <param name="MyFilter">Filter Text process from control, Used to Filter Information into the control</param>
    ''' <param name="MyControl">The Control sent to the </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddFilterElementFromComboBoxes(ByRef MyFilter As String, ByVal MyControl As Control) As String

        If TypeOf MyControl Is xboXComboBox Then
            Dim MYxboXComboBox As New xboXComboBox
            MYxboXComboBox = DirectCast(MyControl, xboXComboBox)

            'Handle special case
            If (MYxboXComboBox.Name = cbMeterForm.Name Or MYxboXComboBox.Name = cbMeterBase.Name) Then

                'Do not Process here, Special case for main function

            ElseIf MYxboXComboBox.Text.Trim <> "" Then
                'If MyFilter is not empty then append and 'AND' or an 'OR'
                If MyFilter <> "" Then
                    MyFilter = MyFilter + SelectAndOr(MYxboXComboBox.Name) '" AND "
                End If

                'Now add the filter...
                If MYxboXComboBox.Text.Trim = "[EMPTY]" Then
                    MyFilter = MyFilter + "([" + MYxboXComboBox.Tag + "] IS NULL" + " OR Trim([" + MYxboXComboBox.Tag + "]) = '')"
                Else
                    MyFilter = MyFilter + "[" + MYxboXComboBox.Tag + "] = '" + MYxboXComboBox.Text + "'"
                End If

            End If

        End If

        If TypeOf MyControl Is GroupBox Then
            Dim MyGroupBox As GroupBox = DirectCast(MyControl, GroupBox)

            For Each j As Control In MyGroupBox.Controls
                If TypeOf j Is xboXComboBox Then
                    Dim MYxboXComboBox As New xboXComboBox
                    MYxboXComboBox = DirectCast(j, xboXComboBox)
                    If MYxboXComboBox.Text.Trim <> "" Then
                        If MyFilter <> "" Then
                            MyFilter = MyFilter + SelectAndOr(MYxboXComboBox.Name) '" AND "
                        End If
                        If MYxboXComboBox.Text.Trim = "[EMPTY]" Then
                            MyFilter = "([" + MYxboXComboBox.Tag + "] IS NULL" + " OR Trim([" + MYxboXComboBox.Tag + "]) = '')"
                        Else
                            MyFilter = MyFilter + "[" + MYxboXComboBox.Tag + "] = '" + MYxboXComboBox.Text + "'"
                        End If
                    End If
                End If

            Next
        End If

        Return MyFilter
    End Function

    Private Function Filter_BuildFilter() As String
        Dim DBFilt As String = ""

        'add all of the filters due to the combo boxes
        For Each i As Control In Panel1.Controls
            AddFilterElementFromComboBoxes(DBFilt, i)
        Next

        'Filter on Anomely, FR, or Both types
        If rbReportCatAll.Checked Then
            'do nothing
        ElseIf rbFilterAnomely.Checked Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " AND "
            End If
            DBFilt = DBFilt & "Anomaly = 'Checked'"
        ElseIf rbFilterFailure.Checked Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " AND "
            End If
            DBFilt = DBFilt & "Anomaly NOT = 'Checked'"
        End If

        If txtFilterNewIDFrom.Text.Trim <> "" Or txtFilterNewIDTo.Text.Trim <> "" Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " AND "
            End If
            If txtFilterNewIDFrom.Text.Trim <> "" And txtFilterNewIDTo.Text.Trim <> "" Then
                DBFilt = DBFilt & "[New ID] >= '" & Me.txtFilterNewIDFrom.Text & "' AND [New ID] <= '" & Me.txtFilterNewIDTo.Text & "'"
            ElseIf txtFilterNewIDFrom.Text.Trim <> "" And txtFilterNewIDTo.Text.Trim = "" Then
                DBFilt = DBFilt & "[New ID] >= '" & Me.txtFilterNewIDFrom.Text & "'"
            ElseIf txtFilterNewIDFrom.Text.Trim = "" And txtFilterNewIDTo.Text.Trim <> "" Then
                DBFilt = DBFilt & "[New ID] <= '" & Me.txtFilterNewIDTo.Text & "'"
            End If

        End If

        If dtpFilterFailedFrom.CustomFormat <> " " Or dtpFilterFailedTo.CustomFormat <> " " Then
            If DBFilt <> "" Then
                If chkFilteredFailedFromAndOr.Checked Then
                    DBFilt = DBFilt & " OR "
                Else
                    DBFilt = DBFilt & " AND "
                End If
            End If

            If dtpFilterFailedFrom.CustomFormat <> " " And dtpFilterFailedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "([Date Failed] >= '" & dtpFilterFailedFrom.Text & "' AND  [Date Failed] <= '" & dtpFilterFailedTo.Text & "')"
            ElseIf dtpFilterFailedFrom.CustomFormat <> " " And dtpFilterFailedTo.CustomFormat = " " Then
                DBFilt = DBFilt & "[Date Failed] >= '" & dtpFilterFailedFrom.Text & "'"
            ElseIf dtpFilterFailedFrom.CustomFormat = " " And dtpFilterFailedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "[Date Failed] <= '" & dtpFilterFailedTo.Text & "'"
            End If

        End If

        If dtpFilterCorrectedFrom.CustomFormat <> " " Or dtpFilterCorrectedTo.CustomFormat <> " " Then
            If DBFilt <> "" Then
                If chkFilterCorrectedFromAndOr.Checked Then
                    DBFilt = DBFilt & " OR "
                Else
                    DBFilt = DBFilt & " AND "
                End If
            End If

            If dtpFilterCorrectedFrom.CustomFormat <> " " And dtpFilterCorrectedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "([Date Corrected] >= '" & dtpFilterCorrectedFrom.Text & "' AND  [Date Corrected] <= '" & dtpFilterCorrectedTo.Text & "')"
            ElseIf dtpFilterCorrectedFrom.CustomFormat <> " " And dtpFilterCorrectedTo.CustomFormat = " " Then
                DBFilt = DBFilt & "[Date Corrected] >= '" & dtpFilterCorrectedFrom.Text & "'"
            ElseIf dtpFilterCorrectedFrom.CustomFormat = " " And dtpFilterCorrectedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "[Date Corrected] <= '" & dtpFilterCorrectedTo.Text & "'"
            End If

        End If

        If dtpFilterApprovedFrom.CustomFormat <> " " Or dtpFilterApprovedTo.CustomFormat <> " " Then
            If DBFilt <> "" Then
                If chkFilterApprovedFromAndOr.Checked Then
                    DBFilt = DBFilt & " OR "
                Else
                    DBFilt = DBFilt & " AND "
                End If
            End If

            If dtpFilterApprovedFrom.CustomFormat <> " " And dtpFilterApprovedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "([Date Approved] >= '" & dtpFilterApprovedFrom.Text & "' AND  [Date Approved] <= '" & dtpFilterApprovedTo.Text & "')"
            ElseIf dtpFilterApprovedFrom.CustomFormat <> " " And dtpFilterApprovedTo.CustomFormat = " " Then
                DBFilt = DBFilt & "[Date Approved] >= '" & dtpFilterApprovedFrom.Text & "'" '
            ElseIf dtpFilterApprovedFrom.CustomFormat = " " And dtpFilterApprovedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "[Date Approved] <= '" & dtpFilterApprovedTo.Text & "'"
            End If

        End If

        If dtpFilterClosedFrom.CustomFormat <> " " Or dtpFilterClosedTo.CustomFormat <> " " Then
            If DBFilt <> "" Then
                If chkFilterClosedFromAndOr.Checked Then
                    DBFilt = DBFilt & " OR "
                Else
                    DBFilt = DBFilt & " AND "
                End If
            End If

            If dtpFilterClosedFrom.CustomFormat <> " " And dtpFilterClosedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "([Date Closed] >= '" & dtpFilterClosedFrom.Text & "' AND  [Date Closed] <= '" & dtpFilterClosedTo.Text & "')"
            ElseIf dtpFilterClosedFrom.CustomFormat <> " " And dtpFilterClosedTo.CustomFormat = " " Then
                DBFilt = DBFilt & "[Date Closed] >= '" & dtpFilterClosedFrom.Text & "'"
            ElseIf dtpFilterClosedFrom.CustomFormat = " " And dtpFilterClosedTo.CustomFormat <> " " Then
                DBFilt = DBFilt & "[Date Closed] <= '" & dtpFilterClosedTo.Text & "'"
            End If

        End If
        If rbDisableReadyForReviewFilter.Checked = False Then


            If chkPAC2ToApprove.Checked = True And chkPAC2ToApprove.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If
                'DBFilt = DBFilt & "([Date Approved] is NULL AND ([Corrective Action] is NOT NULL AND (Level = 'PAC 2%' OR Level = 'FPA%' or Level = 'OEM') or (TCC_REVIEW_REQUIRED = 'Checked' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked')))"
                DBFilt = DBFilt & "(Level LIKE 'PAC 2%' AND TCC_REVIEW_REQUIRED = 'Checked' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked')"
            End If

            If chkFilterPAC1toApprove.Checked = True And chkFilterPAC1toApprove.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If

                Dim AppendFilter As String = ""
                If chkTCCReviewRequired.Checked Then
                    AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
                End If

                DBFilt = DBFilt & "(Level LIKE 'PAC 1%' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"

               
            End If

            If ChkCITtoApprove.Checked = True And ChkCITtoApprove.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If

                Dim AppendFilter As String = ""
                If chkTCCReviewRequired.Checked Then
                    AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
                End If

                DBFilt = DBFilt & "(Level LIKE 'CIT%' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"

                
            End If

            If chkFPAToApprove.Checked = True And chkFPAToApprove.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If

                Dim AppendFilter As String = ""
                If chkTCCReviewRequired.Checked Then
                    AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
                End If

                DBFilt = DBFilt & "(Level LIKE 'FPA%' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"

            End If

            If chkEngineeringToApporve.Checked = True And chkEngineeringToApporve.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If

                Dim AppendFilter As String = ""
                If chkTCCReviewRequired.Checked Then
                    AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
                End If

                DBFilt = DBFilt & "(Level LIKE 'ENG%' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"

            End If

            If chkOEMToApprove.Checked = True And chkOEMToApprove.Enabled = True Then
                If DBFilt <> "" Then
                    DBFilt = DBFilt & " OR "
                End If

                Dim AppendFilter As String = ""
                If chkTCCReviewRequired.Checked Then
                    AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
                End If

                DBFilt = DBFilt & "(Level LIKE 'OEM%' AND FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"
               
            End If
           
        End If

        If rbReviewAllREadyForReview.Checked = True Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " OR "
            End If
            Dim AppendFilter As String = ""
            If chkTCCReviewRequired.Checked Then
                AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
            End If
            DBFilt = DBFilt & "(FR_READY_FOR_REVIEW = 'Checked' AND FR_APPROVED = 'Unchecked'" & AppendFilter & ")"
           
        End If

        If chkFilterOpenReportsOnly.Checked = True Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " AND "
            End If

            Dim AppendFilter As String = ""
            If chkTCCReviewRequired.Checked Then
                AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
            End If

            DBFilt = DBFilt & "(FR_READY_FOR_REVIEW = 'Unchecked'" & AppendFilter & ")"
           
        End If

        If chkFilterTransferedReportsOnly.Checked = True Then
            If DBFilt <> "" Then
                DBFilt = DBFilt & " AND "
            End If

            Dim AppendFilter As String = ""
            If chkTCCReviewRequired.Checked Then
                AppendFilter = " AND TCC_REVIEW_REQUIRED = 'Checked'"
            End If

            DBFilt = DBFilt & "(Original_Report_Num IS NOT NULL AND TRIM(Original_Report_Num) <> '' " & AppendFilter & ")"

        End If


        'Meter base is habdled as a pair...Always hase special case where it iwll return FRs with BASE not filled in...
        If cbMeterBase.Text.Trim <> "" And cbMeterForm.Text.Trim <> "" Then
            If DBFilt <> "" Then
                DBFilt = DBFilt + SelectAndOr(chkMeterFormAndOr.Name)
            End If

            Dim FormFilter As String
            Dim BaseFilter As String

            If cbMeterForm.Text.Trim = "[EMPTY]" Then

                FormFilter = "([" + cbMeterForm.Tag + "] IS NULL" + " OR Trim([" + cbMeterForm.Tag + "]) = '')"
            Else
                FormFilter = "[" + cbMeterForm.Tag + "] = '" + cbMeterForm.Text + "'"
            End If

            If cbMeterBase.Text.Trim = "[EMPTY]" Then
                BaseFilter = "([" + cbMeterBase.Tag + "] IS NULL" + " OR Trim([" + cbMeterBase.Tag + "]) = '')"
            Else
                BaseFilter = "[" + cbMeterBase.Tag + "] = '" + cbMeterBase.Text + "'"
            End If

            DBFilt = DBFilt + "(" + FormFilter + " AND " + BaseFilter + ")"

        ElseIf cbMeterForm.Text.Trim <> "" Then

            If DBFilt <> "" Then
                DBFilt = DBFilt + SelectAndOr(chkMeterFormAndOr.Name)
            End If

            If cbMeterForm.Text.Trim = "[EMPTY]" Then

                DBFilt = DBFilt + "([" + cbMeterForm.Tag + "] IS NULL" + " OR Trim([" + cbMeterForm.Tag + "]) = '')"
            Else
                DBFilt = DBFilt + "[" + cbMeterForm.Tag + "] = '" + cbMeterForm.Text + "'"
            End If

        ElseIf cbMeterBase.Text.Trim <> "" Then
            If DBFilt <> "" Then
                DBFilt = DBFilt + SelectAndOr(chkMeterFormAndOr.Name)
            End If

            If cbMeterBase.Text.Trim = "[EMPTY]" Then

                DBFilt = DBFilt + "([" + cbMeterBase.Tag + "] IS NULL" + " OR Trim([" + cbMeterBase.Tag + "]) = '')"
            Else
                DBFilt = DBFilt + "[" + cbMeterBase.Tag + "] = '" + cbMeterBase.Text + "'"
            End If
        End If

        '***** Remove for Now this was attempt to find names and such when slightly mis matched... Intelligent search possible?

        'If cbFilterAssignedTo.Text.Trim <> "" And cbFilterAssignedTo.Text.Trim <> "[EMPTY]" Then
        '    If DBFilt <> "" Then
        '        DBFilt = DBFilt & " OR "
        '    End If
        '    DBFilt = DBFilt & "[Assigned To] Like '" & cbFilterAssignedTo.Text + "%'"
        'End If

        'If cbFilterCorrectedBy.Text.Trim <> "" Then
        '    If DBFilt <> "" Then
        '        DBFilt = DBFilt & " OR "
        '    End If
        '    DBFilt = DBFilt & "[Corrected By] Like '" & cbFilterCorrectedBy.Text + "%'"
        'End If
        'txtFilterDisplay.Text = DBFilt
        Return DBFilt
        'Me.Hide()
    End Function

    Private Sub btnFilterBuild_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterBuild.Click
        Dim MyFilter As String = Filter_BuildFilter().Trim
        If chkPreserveFilter.Checked = True And txtFilterDisplay.Text.Trim <> "" And MyFilter <> "" Then
            txtFilterDisplay.Text = "(" + txtFilterDisplay.Text + ")" + SelectAndOr(ChkPreserveFilterAndOr.Name) + "(" + MyFilter + ")"
        ElseIf chkPreserveFilter.Checked = True And txtFilterDisplay.Text.Trim <> "" Then
            'do nothing
        Else
            txtFilterDisplay.Text = MyFilter
        End If
        'Initialize_Filter_Elements()
        ' ResetFilter()
    End Sub

    Private Sub BtnFilterRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnFilterRemove.Click
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        frmFailureBrowser.gMyFailureReportBindingSource.RemoveFilter()
        frmFailureBrowser.dgvFailureReportDataGridView.Show()
    End Sub

    Private Sub btnFilterSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterApply.Click

        'Old search formula: Z:\PAC\Failure Report Data Base\Archive\FailReporter vb2010 Final 2013\WindowsApplication1\WindowsApplication1\OldSearch.txt
        Try
            'frmFailureBrowser.Failure_ReportDataGridView.Refresh()
            frmFailureBrowser.dgvFailureReportDataGridView.Hide()
            frmFailureBrowser.gMyFailureReportBindingSource.Filter = txtFilterDisplay.Text
            frmFailureBrowser.dgvFailureReportDataGridView.Show()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub InitGridView(MyFilterFailureGridview As DataGridView)
        MyFilterFailureGridview.Hide()
        'Set the Data Source to the Parent Binding Source...
        MyFilterFailureGridview.DataSource = frmFailureBrowser.gMyFailureReportBindingSource

        'Set the column name text of the Gridview do this First
        MyFilterFailureGridview.Columns("New ID").HeaderText = "Report #"
        'Freeze the Failure Report Column
        MyFilterFailureGridview.Columns("New ID").Frozen = True
        'Change header text from Level to Test Level
        MyFilterFailureGridview.Columns("Level").HeaderText = "Test Level"
        'Change Header text to Test Name
        MyFilterFailureGridview.Columns("Test").HeaderText = "Test Name"

        MyFilterFailureGridview.AllowUserToOrderColumns = True

        MyFilterFailureGridview.ReadOnly = True

        For Each Mycolumn As DataGridViewColumn In MyFilterFailureGridview.Columns
            Mycolumn.HeaderText = Mycolumn.HeaderText.ToString.Replace("_", " ")
        Next

        MyFilterFailureGridview.Columns("Meter").HeaderText = "Meter Model"
        'MyFilterFailureGridview.Columns("EUT TYPE").HeaderText = "Meter Type"  'This is used
        MyFilterFailureGridview.Columns("Assigned To").HeaderText = "Project Lead"
        MyFilterFailureGridview.Columns("FW Ver").HeaderText = "Meter FW Ver"

        'The following Code was added to allow resizing the columns
        MyFilterFailureGridview.AutoSize = False
        MyFilterFailureGridview.AllowUserToResizeColumns = True
        MyFilterFailureGridview.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
        For i = 0 To MyFilterFailureGridview.Columns.Count - 1
            MyFilterFailureGridview.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
            MyFilterFailureGridview.Columns(i).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Next

        'Set the Visability and Position of each column in the FailureReportGridView in the main form

        For Each column As DataGridViewColumn In frmFailureBrowser.dgvFailureReportDataGridView.Columns
            MyFilterFailureGridview.Columns(column.Name).Visible = column.Visible
            MyFilterFailureGridview.Columns(column.Name).DisplayIndex = column.DisplayIndex
            MyFilterFailureGridview.Columns(column.Name).Width = column.Width
            MyFilterFailureGridview.Columns(column.Name).HeaderText = column.HeaderText
        Next

        MyFilterFailureGridview.Show()

    End Sub


    Private Sub dtpFilterFailedFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFilterFailedFrom.ValueChanged
        Me.dtpFilterFailedFrom.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpFilterFailedTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFilterFailedTo.ValueChanged
        Me.dtpFilterFailedTo.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpFilterApprovedFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFilterApprovedFrom.ValueChanged
        Me.dtpFilterApprovedFrom.CustomFormat = "MM/dd/yyyy"
    End Sub
    Private Sub dtpFilterApprovedTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFilterApprovedTo.ValueChanged
        Me.dtpFilterApprovedTo.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpFilterClosedFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFilterClosedFrom.ValueChanged
        Me.dtpFilterClosedFrom.CustomFormat = "MM/dd/yyyy"
    End Sub
    Private Sub dtpFilterClosedTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFilterClosedTo.ValueChanged
        Me.dtpFilterClosedTo.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub chkFilterAnomely_Click(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub HelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        'help6.show()
    End Sub

    ''' <summary>
    ''' This function is used to handle the Dropdown event for the all the Comboboxes. The Handlers are added programmicaly in "Initialize_Filter_Elements()"  
    ''' </summary>
    ''' <param name="sender">This is xboXcomboBoxx</param>
    ''' <param name="e">Standard event arguments...</param>
    ''' <remarks>Frank Boudreau 2015</remarks>
    Private Sub cbFilterComboDropDown_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles cbMeterManufacturer.DropDown, cbMeterBase.DropDown
        If _EnableControls Then
            'Cast Sender to Combo...
            Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
            Dim strFilter As String = ""
            MyxboXComboBox.Text = ""

            If chkPreserveFilter.Checked Then
                strFilter = BuildFilterForComboDropDowns("Where " + txtFilterDisplay.Text.Trim)
            Else
                strFilter = BuildFilterForComboDropDowns()
            End If

            ' txtFilterDisplay.Text = strFilter
            If UseFRHistoryToolStripMenuItem.Checked = True Then
                frmFailureBrowser.PopulatexboXComboBoxAddValue(frmFailureBrowser.gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", "[EMPTY]", strFilter)
            ElseIf UseFRHistoryNoFilterToolStripMenu.Checked = True Then
                frmFailureBrowser.PopulatexboXComboBoxAddValue(frmFailureBrowser.gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", "[EMPTY]", "")
            End If
        End If


    End Sub


    Public Overloads Function BuildFilterForComboDropDowns(Optional InputFilter As String = "") As String
        'Right now filters on other xboXComboBox selections and Report Catagory

        Dim MyFilter As String = InputFilter

        'Added the following code to deal wit hthe special case that there is only one filter element and that the user has requested that it be 'OR'd with any
        'Elements that follow it.  In this special case it needs to be detected.  No xboXComboBox filtering should occur until the second Filter Element is added.
        Dim NumberOfFilterElements As Integer = 0
        Dim AND_or_OR As String = " AND "

        For Each i As Control In Panel1.Controls



            If TypeOf i Is xboXComboBox Then
                Dim MyxboXComboBox As xboXComboBox = DirectCast(i, xboXComboBox)
                If MyxboXComboBox.Text <> "" Then
                    NumberOfFilterElements += 1
                    AND_or_OR = SelectAndOr(MyxboXComboBox.Name)
                    If MyFilter = "" Then
                        MyFilter = " WHERE "
                    Else
                        MyFilter = MyFilter + SelectAndOr(MyxboXComboBox.Name)
                    End If

                    If MyxboXComboBox.Text.Trim = "[EMPTY]" Then
                        MyFilter = MyFilter + "([" + MyxboXComboBox.Tag + "] IS NULL" + " OR Trim([" + MyxboXComboBox.Tag + "]) = '')"
                    Else
                        MyFilter = MyFilter + "[" + MyxboXComboBox.Tag + "] = '" + MyxboXComboBox.Text + "'"
                    End If

                End If
            End If

            If TypeOf i Is GroupBox Then
                Dim MyGroupBox As GroupBox = DirectCast(i, GroupBox)

                For Each j As Control In MyGroupBox.Controls
                    If TypeOf j Is xboXComboBox Then

                        Dim MyxboXComboBox As xboXComboBox = DirectCast(j, xboXComboBox)
                        If MyxboXComboBox.Text <> "" Then
                            NumberOfFilterElements += 1
                            AND_or_OR = SelectAndOr(MyxboXComboBox.Name)
                            If MyFilter = "" Then
                                MyFilter = " WHERE "
                            Else
                                MyFilter = MyFilter + SelectAndOr(MyxboXComboBox.Name)
                            End If

                            If MyxboXComboBox.Text.Trim = "[EMPTY]" Then
                                MyFilter = MyFilter + "([" + MyxboXComboBox.Tag + "] IS NULL" + " OR Trim([" + MyxboXComboBox.Tag + "]) = '')"
                            Else
                                MyFilter = MyFilter + "[" + MyxboXComboBox.Tag + "] = '" + MyxboXComboBox.Text + "'"
                            End If

                        End If
                    End If



                    If TypeOf i Is RadioButton Then
                        Dim MyRadioButton As RadioButton = DirectCast(i, RadioButton)
                        Select Case i.Tag
                            Case i.Tag = rbReportCatAll.Tag

                                'Do nothing do not filter based on Anomely vs Failure

                            Case i.Tag = rbFilterAnomely.Tag
                                If MyRadioButton.Checked Then
                                    NumberOfFilterElements += 1
                                    If MyFilter = "" Then
                                        MyFilter = " WHERE [Anomaly] = 'Checked'"
                                    Else
                                        MyFilter = MyFilter + " AND [Anomaly] = 'Checked'"
                                    End If
                                End If

                            Case i.Tag = rbFilterFailure.Tag
                                If MyRadioButton.Checked Then
                                    NumberOfFilterElements += 1
                                    If MyFilter = "" Then
                                        MyFilter = " WHERE [Anomaly] NOT = 'Checked'"
                                    Else
                                        MyFilter = MyFilter + " AND [Anomaly] NOT = 'Checked'"
                                    End If
                                End If

                        End Select

                    End If
                Next
            End If

        Next

        If NumberOfFilterElements = 1 And AND_or_OR = " OR " Then
            'suppress Filter
            MyFilter = ""
        End If


        Dim MyStop As Integer = 1
        Return MyFilter
    End Function

    Private Sub cbFilterAssignedTo_MouseEnter(sender As System.Object, e As System.EventArgs) Handles cbFilterAssignedTo.MouseEnter
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        Dim strTempstring = MyxboXComboBox.Text
        MyxboXComboBox.Text = ""
        strFilter = BuildFilterForComboDropDowns()

        ' frmFailureBrowser.PopulatexboXComboBox(frmFailureBrowser.gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        frmFailureBrowser.PopulatexboXComboBoxAddValue(frmFailureBrowser.gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", "EMPTY", strFilter)


        MyxboXComboBox.Text = strTempstring
        MyxboXComboBox.SelectAll()
    End Sub

    Private Sub cbFilterCorrectedBy_MouseEnter(sender As System.Object, e As System.EventArgs) Handles cbFilterCorrectedBy.MouseEnter
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        MyxboXComboBox.SelectAll()
        ' MyxboXComboBox.Text = ""
        strFilter = BuildFilterForComboDropDowns()
        frmFailureBrowser.PopulatexboXComboBox(frmFailureBrowser.gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
    End Sub

    Private Sub btnFilterFailedDatesClear_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterFailedDatesClear.Click
        dtpFilterFailedFrom.CustomFormat = " "
        dtpFilterFailedTo.CustomFormat = " "

    End Sub

    Private Sub btnFilterApprovedDatesClear_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterApprovedDatesClear.Click
        dtpFilterApprovedFrom.CustomFormat = " "
        dtpFilterApprovedTo.CustomFormat = " "
    End Sub

    Private Sub btnFilterClosedDatesClear_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterClosedDatesClear.Click
        dtpFilterClosedFrom.CustomFormat = " "
        dtpFilterClosedTo.CustomFormat = " "
    End Sub

    Private Sub btnFilterClear_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterClear.Click
        'Old search formula: Z:\PAC\Failure Report Data Base\Archive\FailReporter vb2010 Final 2013\WindowsApplication1\WindowsApplication1\OldSearch.txt
        Try
            ResetFilter()
            'Initialize_Filter_Elements()

            'frmFailureBrowser.Failure_ReportDataGridView.Refresh()
            'frmFailureBrowser.gMyFailureReportBindingSource.RemoveFilter()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilterCorrectedDatesCleared_Click(sender As System.Object, e As System.EventArgs) Handles btnFilterCorrectedDatesCleared.Click
        dtpFilterCorrectedFrom.CustomFormat = " "
        dtpFilterCorrectedTo.CustomFormat = " "

    End Sub

    Private Sub dtpFilterCorrectedFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFilterCorrectedFrom.ValueChanged
        Me.dtpFilterCorrectedFrom.CustomFormat = "MM/dd/yyyy"
    End Sub

    Private Sub dtpFilterCorrectedTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFilterCorrectedTo.ValueChanged
        Me.dtpFilterCorrectedTo.CustomFormat = "MM/dd/yyyy"
    End Sub


    Private Sub frmFilter_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        InitGridView(dgvFilterFailureReport)
        _EnableControls = True
    End Sub


    Private Sub rbNoneSelected_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbDisableReadyForReviewFilter.CheckedChanged
        If rbDisableReadyForReviewFilter.Checked = True Then
            chkPAC2ToApprove.Enabled = False
            chkOEMToApprove.Enabled = False
            ChkCITtoApprove.Enabled = False
            chkFPAToApprove.Enabled = False
            chkEngineeringToApporve.Enabled = False
            chkFilterPAC1toApprove.Enabled = False

            chkPAC2ToApprove.Visible = False
            chkOEMToApprove.Visible = False
            ChkCITtoApprove.Visible = False
            chkFPAToApprove.Visible = False
            chkEngineeringToApporve.Visible = False
            chkFilterPAC1toApprove.Visible = False
        End If
    End Sub

    Private Sub rbReviewAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbReviewAllREadyForReview.CheckedChanged
        If rbReviewAllREadyForReview.Checked = True Then
            chkPAC2ToApprove.Enabled = False
            chkOEMToApprove.Enabled = False
            ChkCITtoApprove.Enabled = False
            chkFPAToApprove.Enabled = False
            chkEngineeringToApporve.Enabled = False
            chkFilterPAC1toApprove.Enabled = False
            chkPAC2ToApprove.Visible = False
            chkOEMToApprove.Visible = False
            ChkCITtoApprove.Visible = False
            chkFPAToApprove.Visible = False
            chkEngineeringToApporve.Visible = False
            chkFilterPAC1toApprove.Visible = False
        End If
    End Sub

    Private Sub rbReviewSelect_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSelectReadyForReview.CheckedChanged
        If rbSelectReadyForReview.Checked = True Then
            chkPAC2ToApprove.Enabled = True
            chkOEMToApprove.Enabled = True
            ChkCITtoApprove.Enabled = True
            chkFPAToApprove.Enabled = True
            chkEngineeringToApporve.Enabled = True
            chkFilterPAC1toApprove.Enabled = True

            chkPAC2ToApprove.Visible = True
            chkOEMToApprove.Visible = True
            ChkCITtoApprove.Visible = True
            chkFPAToApprove.Visible = True
            chkEngineeringToApporve.Visible = True
            chkFilterPAC1toApprove.Visible = True
        End If
    End Sub

    Private Sub And_OR_Check_Changed_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If _EnableControls = True Then
            Dim MyCheckbox As CheckBox = DirectCast(sender, CheckBox)
            If MyCheckbox.Text = "+" Then
                MyCheckbox.Text = "|"
            Else
                MyCheckbox.Text = "+"
            End If
        End If
    End Sub


    Private Sub UseDefaultValuesToolStripMenuItem_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles UseDefaultValuesToolStripMenuItem.CheckStateChanged
        'If _EnableControls = True Then
        '    _EnableControls = False
        '    If UseDefaultValuesToolStripMenuItem.CheckState = True Then
        '        UseFRHistoryToolStripMenuItem.CheckState = False
        '        UseFRHistoryNoFilterToolStripMenu.CheckState = False
        '    Else
        '        UseDefaultValuesToolStripMenuItem.CheckState = True
        '        UseFRHistoryToolStripMenuItem.CheckState = False
        '        UseFRHistoryNoFilterToolStripMenu.CheckState = False
        '    End If
        '    _EnableControls = True
        'End If
    End Sub


    Private Sub UseFRHistoryNoFilterToolStripMenu_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles UseFRHistoryNoFilterToolStripMenu.CheckStateChanged
        If _EnableControls = True Then
            _EnableControls = False
            If UseFRHistoryNoFilterToolStripMenu.Checked = True Then
                UseFRHistoryToolStripMenuItem.Checked = False
                ' UseDefaultValuesToolStripMenuItem.CheckState = False
            Else
                UseFRHistoryNoFilterToolStripMenu.Checked = True
                UseFRHistoryToolStripMenuItem.Checked = False
                '  UseDefaultValuesToolStripMenuItem.CheckState = False
            End If
            _EnableControls = True
        End If
    End Sub



    Private Sub UseFRHistoryToolStripMenuItem_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles UseFRHistoryToolStripMenuItem.CheckStateChanged
        If _EnableControls = True Then
            _EnableControls = False
            If UseFRHistoryToolStripMenuItem.Checked = True Then
                UseFRHistoryNoFilterToolStripMenu.Checked = False
                ' UseDefaultValuesToolStripMenuItem.CheckState = False
            Else
                UseFRHistoryToolStripMenuItem.Checked = True
                UseFRHistoryNoFilterToolStripMenu.Checked = False
                '  UseDefaultValuesToolStripMenuItem.CheckState = False
            End If
            _EnableControls = True
        End If
    End Sub

    Private Sub chkFilterAllowAdvancedEditing_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFilterAllowAdvancedEditing.CheckedChanged

        If txtFilterDisplay IsNot Nothing Then
            If chkFilterAllowAdvancedEditing.Checked = True Then
                txtFilterDisplay.ReadOnly = False
            Else
                txtFilterDisplay.ReadOnly = True
            End If
        End If
    End Sub


    Private Sub ManageDatagridviewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ManageDatagridviewToolStripMenuItem.Click
        Dim MyMangageGridForm As New frmManageGridview
        'Me.Hide()
        MyMangageGridForm.ShowDialog()
        InitGridView(dgvFilterFailureReport)
        ' Me.Show()
    End Sub
End Class
