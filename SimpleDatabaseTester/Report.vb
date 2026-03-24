Public Class frmReport
    Public gFailureReportBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker) 'custom class for to track and automate databindings to controls
    Public gMyFailureReportBindingSource As BindingSource
    Public gMyFailureReportDBBinding As Binding
    Public iAmLoaded As Boolean = False

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' gFailureReportBindingRecord = DirectCast(frmFailureBrowser.gFailureReportBindingRecord, List(Of cCustomDataBaseAccess.cDataBindingTracker))
        gFailureReportBindingRecord = New List(Of cCustomDataBaseAccess.cDataBindingTracker)
        gMyFailureReportBindingSource = DirectCast(frmFailureBrowser.gMyFailureReportBindingSource, BindingSource)
        gMyFailureReportDBBinding = DirectCast(frmFailureBrowser.gMyFailureReportDBBinding, Binding)
Textboxes:
        'textboxes
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtReportNumber, "Text", "New ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        'gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProjectLead, "Text", "Assigned To", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

        'Binding to text box instead...
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTest, "Text", "Test", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtApprovedBy, "Text", "Approved By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        '  gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtTCC_Comments, "Text", "TCC Comments", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtCorrectedBy, "Text", "Corrected By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtEngineeringNotes, "Text", "Engineering Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtMeterNotes, "Text", "Meter_Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtAMR_Notes, "Text", "AMR_Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProjectNumber, "Text", "Project_Number", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProject, "Text", "Project", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtAttachments, "Text", "Attachments", Me.gMyFailureReportBindingSource, Me.gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtCorrectiveAction, "Text", "Corrective Action", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtFailureDescription, "Text", "Failure Description", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtTestEquipmentIDlist, "Text", "Test_Equipment_ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        'Added 1.27.2017 to solve interface issue.  Nolonger directly binging DTPickeers to FR database...using a masked textbox 
        '                                           allows user to directly enter date and to have an empty date field.  DTP does 
        '                                           not allow empty value. 
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateFailed, "Text", "Date Failed", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        '  gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateCorrected, "Text", "Date Corrected", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateClosed, "Text", "Date Closed", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateApproved, "Text", "Date Approved", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
xboXComboBoxes:

        'xboXComboBoxes
cbMeterData:  '*****Meter Data*******
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterModel, "Text", "Meter", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterManufacturer, "Text", "Meter_Manufacturer", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterSerialNumber, "Text", "Meter_Serial_Number", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterType, "Text", "Meter_Type", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterSubType, "Text", "Meter_SubType", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterSubTypeII, "Text", "Meter_SubTypeII", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterDSP_Rev, "Text", "Meter_DSP_Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterPCBA, "Text", "Meter_PCBA", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterPCBA_Rev, "Text", "Meter_PCBA_Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterSoftware, "Text", "Meter_Software", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterSoftwareRev, "Text", "Meter_Software_Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterFW_Ver, "Text", "FW Ver", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterForm, "Text", "Form", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterBase, "Text", "Meter_Base", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbMeterVoltage, "Text", "Meter_Voltage", gMyFailureReportBindingSource, gMyFailureReportDBBinding))





cbAMR_Data:  '*****AMR Data******
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_Model, "Text", "AMR", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_FW_Rev, "Text", "AMR Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_Manufacturer, "Text", "AMR_Manufacturer", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_SerialNumber, "Text", "AMR_SN", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_TYPE, "Text", "AMR_Type", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_SubType, "Text", "AMR_SUBType", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_SUBtypeII, "Text", "AMR_SUBTypeII", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_SUBtypeIII, "Text", "AMR_SUBTypeIII", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_PCBA, "Text", "AMR_PCBA", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_PCBA_Rev, "Text", "AMR_PCBA_Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_Software, "Text", "AMR_Software", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_Software_Rev, "Text", "AMR_Software_Rev", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_IP_LAN_ID, "Text", "AMR_IP_LAN_ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbAMR_Voltage, "Text", "AMR_Voltage", gMyFailureReportBindingSource, gMyFailureReportDBBinding))



cbTestData:

        ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestName, "Text", "Test", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestType, "Text", "Test_Type", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestedBy, "Text", "Tested By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestLevel, "Text", "Level", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbEUTType, "Text", "EUT_TYPE", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbProjectLead, "Text", "Assigned To", gMyFailureReportBindingSource, gMyFailureReportDBBinding))



cbTCCData:
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_1_Compliance, "Text", "TCC 1", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_2_Engineering, "Text", "TCC 2", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_3_Manufacturing, "Text", "TCC 3", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_4_Product_Management, "Text", "TCC 4", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_5_Quality, "Text", "TCC 5", gMyFailureReportBindingSource, gMyFailureReportDBBinding))



        'checkboxes
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkAnomely, "CheckState", "Anomaly", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        'gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkFR_ReadyForReview, "CheckState", "FR_Ready_For_Review", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTCC_ApprovalRequired, "CheckState", "TCC_REview_Required", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
        'gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(ChkFR_Approved, "CheckState", "FR_Approved", gMyFailureReportBindingSource, gMyFailureReportDBBinding))



        'Excute the bindings to the database....


        'Call the function the creates the bindings
        frmFailureBrowser.gMyCustomDBAccess.BindControls(gFailureReportBindingRecord)
        iAmLoaded = True
        FormatShortDate(frmFailureBrowser.mtxtDateApproved, mtxtDateApproved)
        FormatShortDate(frmFailureBrowser.mtxtDateFailed, mtxtDateFailed)
        FormatShortDate(frmFailureBrowser.mtxtDateCorrected, mtxtDateCorrected)

    End Sub

   
    Public Sub FormatShortDate(ByVal SourceMaskedTextBox As MaskedTextBox, ByVal DestinationMaskedTextBox As MaskedTextBox)
        If iAmLoaded Then

            Try
                DestinationMaskedTextBox.Text = SourceMaskedTextBox.Text.Substring(0, 10)
            Catch ex As Exception
                DestinationMaskedTextBox.Text = SourceMaskedTextBox.Text
            End Try

        End If
    End Sub
End Class