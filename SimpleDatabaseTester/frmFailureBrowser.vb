Imports System
Imports System.Data

Imports System.IO
Imports System.Management

Imports System.Text
Imports System.Drawing
Imports System.Collections
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Environment

Imports Microsoft.Win32 'For Regkey
Imports Microsoft.VisualBasic.Devices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports outlook = Microsoft.Office.Interop.Outlook
Imports System.Reflection

Public Class frmFailureBrowser

    Dim _SoftwareVersion As String ' = "2.0.0.22"
    ''' <summary>
    ''' Declare instant of Custom database access tools
    ''' </summary>
    ''' <remarks></remarks>
    Public gMyCustomDBAccess As cCustomDataBaseAccess
    ''' <summary>
    ''' Form used to selct Test equipment used for testing
    ''' </summary>
    ''' <remarks></remarks>
    Dim frmMySelectTestEquipment As frmSelectTestEquipment

    'Dim MyNewSplash As frmSplash = New frmSplash


#Region "Global Scoped Variables used for connecting, accessing, and binding to the failure Report Database"
    Public gMyFailureReportDBConnection As SqlConnection ' SqlDbConnection
    Public gMyFailureReportDBDataAdaptor As SqlDataAdapter ' .OleDbDataAdapter
    'Public gMyFailureReportDBDataReader As SqlDbDataReader
    Public gMyFailureReportDataTable As DataTable
    Public gMyFailureReportBindingSource As BindingSource
    Public gLimitRecordsForFastAccess As Boolean = False
    ''' <summary>
    ''' This is the Record holding all of the information necessary to bind data to a control
    ''' </summary>
    ''' <remarks></remarks>
    Public gFailureReportBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker) 'custom class for to track and automate databindings to controls

    Public WithEvents gMyFailureReportDBBinding As Binding
    ' Public gMyFailureReportDbDataSet As DataSet
    ''' <summary>
    ''' This flagged isused to track if it is safe to allow the The datagridview to process events like SelectionChanged, RowValidate
    ''' </summary>
    ''' <remarks></remarks>
    Public gb_ProcessEvents As Boolean = False
    ''' <summary>
    ''' True if a User has logged on...
    ''' </summary>
    ''' <remarks></remarks>
    Public gbUserLoggedOn As Boolean = False

    Public gbVerbose As Boolean
    Public gbWriteToLog As Boolean
    Public gVerboseLevel As Integer
    Public Enum eLogLevel
        OFF
        ERRORS_ONLY
        ERROR_AND_INFORMATION
        MAX
    End Enum

    Public Enum eDataBaseErrors
        NO_ERROR = 0
        CONNECTION_FAILED = 1
        OPEN_FAILED = 2
        FAILED_TO_RETRIEVE_SCHEMA = 4
        ERROR_POPULATING_DATASET_OR_DATATABLE = 8
        CLOSE_FAILED = 16
        BINDING_FAILED = 32
        UNKNOWN_ERROR = 64
        EXIT_PROGRAM = 128

        'CONNECTION_FAILED_MASK = &HFF - CONNECTION_FAILED
        'OPEN_FAILED_MASK = &HFF - OPEN_FAILED
        'FAILED_TO_RETRIEVE_SCHEMA_MASK = &HFF - FAILED_TO_RETRIEVE_SCHEMA
        'ERROR_POPULATING_DATASET_OR_DATATABLE_MASK = &HFF - ERROR_POPULATING_DATASET_OR_DATATABLE
        'CLOSE_FALIED_MASK = &HFF - CLOSE_FAILED
        'BINDING_FAILED_MASK = &HFF - BINDING_FAILED
        'UNKNOWN_ERROR_MASK = &HFF - UNKNOWN_ERROR
        'EXIT_PROGRAM_MASK = &HFF - EXIT_PROGRAM
    End Enum

    Public tsmVerbose_Value(3)

    Public Structure sFR_SQL_Commands
        Const SQL_GET_ALL_FAILURE_REPORTS As String = "SELECT * FROM [Failure Report] ORDER BY [New ID] Desc"
        Const SQL_GET_OPEN_FAILURE_REPORTS As String = "SELECT * FROM [Failure Report] WHERE [FR_Approved] <> 'Checked' ORDER BY [New ID] Desc"
        Const SQL_GET_CLOSED_FAILURE_REPORTS As String = "SELECT * FROM [Failure Report] WHERE [FR_Approved] = 'Checked' ORDER BY [New ID] Desc"
        Public ActiveFilter As String
    End Structure

    Public Const _SQL_TRUE As String = "1"
    Public Const _SQL_FALSE As String = "0"

    Dim _FR_SQL_Commands As String
#End Region

#Region "Global Scoped Variables used for connecting, accessing, and binding to the Meter_Spec Database"
    Public gMyMeterSpecDBConnection As SqlConnection ' SqlDbConnection
    Public gMyMeterSpecDBDataAdaptor As SqlDataAdapter 'SqlDbDataAdapter

    'Public gMyMeterSpecOleDBDataReader As SqlDbDataReader
    Public gMyMeterSpecDataTableSchema As DataTable
    Public gMyMeterSpecBindingSource As BindingSource
    Public gMyMeterSpecDbDataSet As DataSet 'Not used
#End Region

#Region "Class Scoped Variables used for tracking the Shadow Read-Only Textboxes used when the Records are in not editable"
    'Strucuture to store the name of the control, the numerical index of the control, and 
    'the Visible state of a control in order to manipulate the controls dynamically
    Structure sShadowControlIndex
        Dim name As String
        Dim index As Integer
        Dim VisibleState As Boolean
    End Structure

    'This is the background color for textboxs, combo-boxs etc...to indicate to the user that 
    'it is not editable...I might make it usable selectable
    Dim _ColorNonEditableBackGround As System.Drawing.Color = System.Drawing.Color.LightYellow

    ''Textbox array that are dynamically created for browsing data in view mode. They are dynamically are 
    ''swapped with the xboXComboBoxes and date pickers for viewing failures
    'Dim _myShadowTextBox() As TextBox


    'The control index is used for keeping track of the which textbox is linked with which Combox or 
    'DateTimePicker when switching from view to edit mode.
    ' Dim _myShadowTextboxIndex() As sShadowControlIndex
    'Dim _myShadowCheckBoxIndex() As sShadowControlIndex
    'This checkbox isused for swapping with the anomely check box when in Non-Edit/View mode
    'This allows changing it to a non editable view by setting the Autocheck to False
    'This is necessary as AutoCheck set to false prevents chkAnomely to update from the Datasource when 
    'browsing
    'Dim _myShadowCheckBox() As CheckBox

    'Used for Printing
    Public _PageNum As Integer = 0
    Public _PrintState As Integer = 0
    'Public _StringToPrint As String = ""
    Public _Header As String = ""
    Public _EutDetails As String = ""
    Public _ApprovalDetails As String

    Public _DescriptionString As String = ""
    Public _CorrectiveActionString As String = ""
    Public _EngineeringNotesString As String = ""
    Public _TCC_CommentsString As String = ""
    Public _Test_EquipmentString As String = ""

#End Region '"Variables used for tracking the Shadow Read-Only Textboxes.."

#Region "Class scoped variables for adding and editing records"

    ''' <summary>
    ''' Local buffer to store the info of who is logged in
    ''' </summary>
    ''' <remarks></remarks>
    Friend _CurrentUser As User

    ''' <summary>
    ''' 'Local buffer to hold the Current data and time
    ''' </summary>
    ''' <remarks></remarks>
    Dim _CurrentDate As DateTime

    ''' <summary>
    ''' Buffer to Hold the orginal content of the textbox before editing this is used to restore 
    ''' test if the user does not save changes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtReportDescriptionBackUp As String = ""
    ''' <summary>
    ''' Buffer to Hold the orginal content of the textbox before editing this is used to restore 
    ''' test if the user does not save changes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtCorrectiveActionBackUp As String = ""
    ''' <summary>
    ''' Buffer to Hold the orginal content of the textbox before editing this is used to restore 
    ''' test if the user does not save changes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myEngineeringNotesBackup As String = ""
    ''' <summary>
    ''' Buffer to Hold the orginal content of the textbox before editing this is used to restore 
    ''' test if the user does not save changes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myEngineeringNotesBackupLengthTest As Integer
    ''' <summary>
    ''' Buffer to Hold the orginal content of the textbox before editing this is used to restore 
    ''' test if the user does not save changes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTCC_CommentsBackup As String = ""


    ''' <summary>
    '''  buffer to hold the orginal LENGTH of data before it is edited.  This is used in conjuction with 
    ''' _my****BackUp to restore the text in a textbox if the user chooses to not save data. Used to test
    ''' if the content of a text has been changed in order to prevent overwriting previous data.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtReportDescriptionBackUpLength As Integer
    ''' <summary>
    '''  buffer to hold the orginal LENGTH of data before it is edited.  This is used in conjuction with 
    ''' _my****BackUp to restore the text in a textbox if the user chooses to not save data. Used to test
    ''' if the content of a text has been changed in order to prevent overwriting previous data.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtCorrectiveActionBackupLength As Integer
    ''' <summary>
    '''  buffer to hold the orginal LENGTH of data before it is edited.  This is used in conjuction with 
    ''' _my****BackUp to restore the text in a textbox if the user chooses to not save data. Used to test
    ''' if the content of a text has been changed in order to prevent overwriting previous data.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myEngineeringNotesBackUpLength As Integer
    ''' <summary>
    '''  buffer to hold the orginal LENGTH of data before it is edited.  This is used in conjuction with 
    ''' _my****BackUp to restore the text in a textbox if the user chooses to not save data. Used to test
    ''' if the content of a text has been changed in order to prevent overwriting previous data.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTCC_CommentsBackupLength As Integer


    ''' <summary>
    ''' This Buffer Holds the orginal content of the Textbox when the Failure report is changed to edit mode.
    '''  This allows the user to leave the text box and goto other controls and return before saving changes.  
    ''' This is necessary as a feature that automatically tracks changes by user and date time prefexing 
    '''  change was added. While the user is Editing.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtReportDescriptionBackUpEdit As String = ""
    ''' <summary>
    ''' Buffer to hold the edited text before it is saved to the database.  This allows the usere to leave 
    ''' the text box and goto other controls and return before saving changes.  This is necessary as a feature
    ''' that automatically tracks changes by user and date time prefexing change was added.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTxtCorrectiveActionBackupEdit As String = ""
    ''' <summary>
    ''' Buffer to hold the edited text before it is saved to the database.  This allows the usere to leave 
    ''' the text box and goto other controls and return before saving changes.  This is necessary as a feature
    ''' that automatically tracks changes by user and date time prefexing change was added.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myEngineeringNotesBackupEdit As String = ""
    ''' <summary>
    ''' Buffer to hold the edited text before it is saved to the database.  This allows the usere to leave 
    ''' the text box and goto other controls and return before saving changes.  This is necessary as a feature
    ''' that automatically tracks changes by user and date time prefexing change was added.
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myTCC_CommentsBackupEdit As String = ""

    'Flags used for Controlling editing of the failure report database
    ' ''' <summary>
    ' ''' 'True if a record has been edited or added
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Dim _RecordEdited As Boolean = False
    ''' <summary>
    ''' 'true if an edited or added  record has been saved
    ''' </summary>
    ''' <remarks></remarks>
    Dim _RecordSaved As Boolean = True

    ''' <summary>
    ''' Enumeration of the Type of Read and Edit status availble for the User Classifactions
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eAccessState
        ''' <summary>
        ''' 'User has no access rights 
        ''' </summary>
        ''' <remarks></remarks>
        NO_ACCESS
        ''' <summary>
        ''' Can only view results
        ''' </summary>
        ''' <remarks></remarks>
        READ_ONLY
        ''' <summary>
        ''' View allows user to create new test reports and edit failure report failure details
        ''' </summary>
        ''' <remarks></remarks>
        CREATE_NEW
        ''' <summary>
        ''' Edit Corrective Actions and Engineering Data, but cannot create new Failure Reports or Edit Failure Report Failure Details
        ''' </summary>
        ''' <remarks></remarks>
        CR_EDIT
        ''' <summary>
        ''' EDIT Corrective Action and Failure Description
        ''' </summary>
        ''' <remarks></remarks>
        POWER
        ''' <summary>
        ''' Allow Approving results and editing TCC comments
        ''' </summary>
        ''' <remarks></remarks>
        APPROVER
        ''' <summary>
        ''' View allows create edit and delete All  edit rights including
        ''' </summary>
        ''' <remarks></remarks>
        ADMIN
    End Enum

    ''' <summary>
    ''' Enumeration of Approver Disciplines.  In addtion to those discribed in the TCC procedure "None" and "Admin" have bee added
    ''' to restrict what Approver controls are available for Editing depending on the USER rights.  A user with ADMIN rights
    ''' Can edit any of the Approver Fields when in edit mode.  A User with No access rights "NONE" will never be able to edit
    ''' Approver Fields when the Report is in Edit mode
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eApproverDiscipline
        ''' <summary>
        ''' USER cannot approve a Failure Report
        ''' </summary>
        ''' <remarks></remarks>
        NONE = 0
        ''' <summary>
        ''' A user can approve a Report for the Compliance Discipline
        ''' </summary>
        ''' <remarks></remarks>
        Compliance = 1
        ''' <summary>
        ''' a user can approve a Report for the Engineering discipline
        ''' </summary>
        ''' <remarks></remarks>
        Engineering = 2
        ''' <summary>
        ''' a User can approve as Report for the Manufacturing Discipline
        ''' </summary>
        ''' <remarks></remarks>
        Manufacturing = 3
        ''' <summary>
        ''' a User can approve as Report for the Product Management Discipline
        ''' </summary>
        ''' <remarks></remarks>
        Product_Managment = 4
        ''' <summary>
        ''' a User can approve as Report for the Quality Discipline
        ''' </summary>
        ''' <remarks></remarks>
        Quality_Product_Delivery = 5
        ''' <summary>
        ''' User has ADMIN rights and can edit any approver Field
        ''' </summary>
        ''' <remarks></remarks>
        Admin = 6

        ''' <summary>
        ''' Approver has Systems approver Rights
        ''' </summary>
        ''' <remarks></remarks>
        SYSTEMS = 7
    End Enum

    ''' <summary>
    ''' Class ecapsulates all of the infromation for a Failure Report Database Tool User
    ''' </summary>
    ''' <remarks></remarks>
    Public Class User
        ''' <summary>
        ''' User ID (Primary Key) for USER table in MeterSpec Database
        ''' </summary>
        ''' <remarks></remarks>
        Private mUserID As Integer
        ''' <summary>
        ''' Password for USER table in MeterSpec Database
        ''' </summary>
        ''' <remarks></remarks>
        Private mPassword As String
        ''' <summary>
        ''' Access level for the USer 
        ''' </summary>
        ''' <remarks></remarks>
        Private mAccessLevel As eAccessState
        ''' <summary>
        ''' Login ID for the USer
        ''' </summary>
        ''' <remarks></remarks>
        Private mLogin As String
        ''' <summary>
        ''' User First name
        ''' </summary>
        ''' <remarks></remarks>
        Private mFirstName As String
        ''' <summary>
        ''' User Last Name
        ''' </summary>
        ''' <remarks></remarks>
        Private mLastName As String
        ''' <summary>
        ''' Approver Discipline
        ''' </summary>
        ''' <remarks></remarks>
        Private mApproverDiscipline As eApproverDiscipline

        ''' <summary>
        ''' Property to get/set the USER ID
        ''' </summary>
        ''' <value>USER ID</value>
        ''' <returns>USER ID</returns>
        ''' <remarks></remarks>
        Public Property UserID As String
            Get
                Return mUserID
            End Get
            Set(ByVal value As String)
                mUserID = value
            End Set
        End Property

        ''' <summary>
        ''' User Last name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LastName As String
            Get
                Return mLastName
            End Get
            Set(ByVal value As String)
                mLastName = value
            End Set
        End Property

        ''' <summary>
        ''' User First name
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FirstName As String
            Get
                Return mFirstName
            End Get
            Set(ByVal value As String)
                mFirstName = value
            End Set
        End Property

        Public Property Login As String
            Get
                Return mLogin
            End Get
            Set(ByVal value As String)
                mLogin = value
            End Set
        End Property

        ''' <summary>
        ''' User PAssword
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Password As String
            Get
                Return mPassword
            End Get
            Set(ByVal value As String)
                mPassword = value
            End Set
        End Property

        ''' <summary>
        ''' User Access Level (Type eAccessLevel)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AccessLevel As eAccessState
            Get
                Return mAccessLevel
            End Get
            Set(ByVal value As eAccessState)
                mAccessLevel = value
            End Set
        End Property

        ''' <summary>
        ''' Discipline that the USER is authorized to approve for (Each user may only be authorized to approve for one Discipline, with the acception of an administrator) 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ApproverDiscipline As eApproverDiscipline
            Get
                Return mApproverDiscipline
            End Get
            Set(ByVal value As eApproverDiscipline)
                mApproverDiscipline = value
            End Set
        End Property

        ''' <summary>
        ''' Initialize and Instance of the USER Class and sets default values
        ''' </summary>
        ''' <param name="strPassword"></param>
        ''' <param name="strLogin"></param>
        ''' <param name="strName"></param>
        ''' <param name="MyAccessLevel"></param>
        ''' <param name="MyApproverDiscipline"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal strPassword As String, ByVal strLogin As String, ByVal strName As String, ByVal MyAccessLevel As eAccessState, ByVal MyApproverDiscipline As eApproverDiscipline)
            UserID = -1 'Default out of range for a SQL index...
            Login = strLogin
            Password = strPassword
            AccessLevel = MyAccessLevel
            FirstName = strName
            ApproverDiscipline = MyApproverDiscipline
        End Sub

    End Class

    ''' <summary>
    ''' Enumeration defines the allowed control states for the GUI
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eEditState
        ''' <summary>
        ''' In this state the FR is read only, all editable controls are disabled for editing
        ''' </summary>
        ''' <remarks></remarks>
        READ_ONLY
        ''' <summary>
        ''' In this state the appropriate contols are enabled after creating a new Failure Report
        ''' In General the EUT and Failure Details may be edited, and typicall entered by a test tech or engineer
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_CREATE_NEW
        ''' <summary>
        ''' In this state a Failure Report Failure details may be edited.  Same AS Create New, however it allows editing
        ''' Failure details on an exsting failure. Typically an HQA Engineer, Tech, or Coop
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_FR_DETAILS
        ''' <summary>
        ''' In this state the controls that allow the user to edit the Corrective Action and Engineering notes are enabled.  A project lead, or
        ''' COE tasked with investigating and resolving a Failure or Anomoaly 
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_FR_CORRECTIVE_ACTION
        ''' <summary>
        ''' IN this state the contols that allow a user to edit a Corrective Action, Engineering Notes, or EUT details and corrective action. (Power USER)  this 
        ''' would be a typically Senior HQA engineer
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_POWER
        ''' <summary>
        ''' In this state the Contols editable state are set based on the Users Approval Access level.  This will be discipline specific
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_SET_APPROVALS
        ''' <summary>
        ''' In this state the Controls are set so that all editable fields may be edite by an administrator.  HQA supervisor)
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_ADMIN
        ''' <summary>
        ''' The same fields that are editable in EDIT NEW are abilable here
        ''' </summary>
        ''' <remarks></remarks>
        EDIT_COPY
    End Enum

    ''' <summary>
    ''' Classifaction of type of EUT tested
    ''' </summary>
    ''' <remarks></remarks>
    Enum EUTtype
        ''' <summary>
        ''' No EUT Type Selected
        ''' </summary>
        ''' <remarks></remarks>
        NONE_SELECTED
        ''' <summary>
        ''' Meter without a Comm or Auxillary Device
        ''' </summary>
        ''' <remarks></remarks>
        METER_ONLY
        ''' <summary>
        ''' A meter with an Auxullary Comm device add or integrated with the meter
        ''' </summary>
        ''' <remarks></remarks>
        AMI
        ''' <summary>
        ''' IF the EUT is a a Comm Device only
        ''' </summary>
        ''' <remarks></remarks>
        AMR_ONLY
        ''' <summary>
        ''' Any other type of EUT that does not fit into one of the predefined classifications
        ''' </summary>
        ''' <remarks></remarks>
        OTHER
    End Enum

    ''' <summary>
    ''' This is Class Scoped flag that indicated the GUI Controls EditState (eEditState)
    ''' </summary>
    ''' <remarks></remarks>
    Public _EditState As eEditState

    Friend MyTestSelector As frmSelectTest

    'Dim _strHeaderBackUpData() As String  'Array to back up changes to the Header incase the user wishes to disgard changes
#End Region '"Class scoped variables for adding and editing records"

#Region "Class Scoped Variables and Objects used to Track The forms various views"
    'A tabelpanel has been placed in the form to facilitate displaying different views
    'These variables are used to set the row styles of the Table layout panel for changing views
    Dim iNumberOfTableLayoutRows = 5 'TableLayoutPanel1.RowCount  '
    Dim _BrowserRowStyle(iNumberOfTableLayoutRows - 1) As RowStyle
    Dim _FR_RowStyle(iNumberOfTableLayoutRows - 1) As RowStyle
    Dim _HeaderAndGridRowStyle(iNumberOfTableLayoutRows - 1) As RowStyle
    Dim _HeaderGridAndDetailRowStyle(iNumberOfTableLayoutRows - 1) As RowStyle
    Dim _PreviousRowStyleView(iNumberOfTableLayoutRows - 1) As RowStyle

    'The Table panel has a collection of rows.  This is the enumeration
    Public Enum TablePanelRowIndex
        FailureReportHeader = 0
        FailureReportNavigation = 1
        FailureReportBody = 2
        FailureReportFilter = 3
        FailureReportGrid = 4
    End Enum
    'These variables hold the size of the form in the defined view
    Dim _BrowserViewFormSize As Size
    Dim _ViewFR_ViewSize As Size
    Dim _ViewHeaderGridAndDetailsSize As Size
    Dim _ViewHeaderAndGridSize As Size

    Public Enum eFailureReportView
        BROWSER_VIEW
        FR_VIEW
        HEADER_GRID_AND_DETAIL_VIEW
        HEADER_AND_GRID_VIEW
    End Enum

    Dim _FailureReportView As New eFailureReportView
#End Region '"Form View Variables and objects"

    ''' <summary>
    ''' Create class that encapsualtes 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _MYSQL_Control As cSQL_Control
    Public _dbMeterSpecSchema As cMeterSpecDBDef
    Public _dbFRSchema As cFailureReportDBDef
#Region "CONTROL STATE"

#Region "CONTROL STATE INIT"
    ''' <summary>
    ''' This Routine is designed to Create, Initialize, and Bind Textboxes to controls in the Failure Report GUI.  
    ''' That have been created in design view are the default for creating new Failure Reports and Editing existing Failure reports
    ''' Textboxes are created to "Shadow" xboXComboBoxs and Date Time pickers and are bound (Databinding) to the text displayed in
    ''' each control. This allows the user when "Viewing" to select and copy, however not to edit the data. A textbox has a "ReadOnly"
    ''' poperty that can be set to true to allow this. The other controls do not.  In addtion shadow textbox is also created to show
    ''' the states of Boolean values and not allow the user to edit the value.  Finally the FailureReport drid view is also set to readonly.
    ''' This is allows th euser to navigate and select via the grid but, only allows editing the database through the GUI fields.
    ''' REV 1 Converted the Combovoxs to a Readonly Enabled Combobox,  Removed the Shadow textboxes from init...
    ''' </summary>
    ''' <remarks>Frank Boudreau 2015  This Function Refactored Out 1/10/2018</remarks>
    ''' 
    Private Sub InitializeShadowControls() 'Removed, Functionality refactored 1/10/2018
        'dynamically determine how many shadow textboxes are needed to replace the editable controls
        'Dim HowManyTextBoxDoINeed As Integer = 0
        'Dim HowManyCheckBoxesDoINeed As Integer = 0
        'Editing is not allowed via the gridview however navigation is possible
        ' dgvFailureReportDataGridView.ReadOnly = True




        'Count the controls To Replace with a textbox to make read only
        'A shadow textbox will be created for each xboXComboBox found,
        'For Each i As Control In pnlReportHeader.Controls 'check each control
        '    'no longer required
        '    'If TypeOf i Is xboXComboBox Then
        '    '    HowManyTextBoxDoINeed += 1 'iincrment count
        '    'End If

        '    If TypeOf i Is CheckBox Then
        '        HowManyCheckBoxesDoINeed += 1 'increment Count
        '    End If
        'Next

        'Resize the textbox and CheckBox arrays to the number needed and shadow control indexer
        'to accomodate all the controls that will be shadowed
        ' ReDim _myShadowTextBox(HowManyTextBoxDoINeed)
        'ReDim _myShadowTextboxIndex(HowManyTextBoxDoINeed)

        'ReDim _myShadowCheckBox(HowManyCheckBoxesDoINeed)
        ' ReDim _myShadowCheckBoxIndex(HowManyCheckBoxesDoINeed)

        ''Create each instance of each indexer and shadow textbox and 
        '' add each textbox to the container 
        'For i = 0 To HowManyTextBoxDoINeed - 1
        '    _myShadowTextBox(i) = New TextBox
        '    _myShadowTextboxIndex(i) = New sShadowControlIndex
        '    pnlReportHeader.Controls.Add(_myShadowTextBox(i))
        'Next

        ''Create each instance of each indexer and shadow Checkbox and 
        '' add each Checkbox to the container 
        'For i = 0 To HowManyCheckBoxesDoINeed - 1
        '    _myShadowCheckBox(i) = New CheckBox
        '    _myShadowCheckBoxIndex(i) = New sShadowControlIndex
        '    pnlReportHeader.Controls.Add(_myShadowCheckBox(i))
        '    _myShadowCheckBox(i).Name = "Shadow" + i.ToString

        'Next

        'find and replace each Combo, Timepicker.  Make each existing textbox read only
        'replace the Editable Checkbox with the shadowcheckbox in the panel header
        'Dim iTextBoxCount As Integer = 0
        ' Dim iCheckBoxCount As Integer = 0
        'Process each control in the panel containter
        '***************Code Removed 1.10.2018******************
        '***************Shadow Control architecture refectored**************
        'For Each i As Control In pnlReportHeader.Controls

        '    'If TypeOf i Is TextBox Then
        '    '    DirectCast(i, TextBox).ReadOnly = True
        '    '    i.BackColor = _ColorNonEditableBackGround
        '    'End If

        '    'If TypeOf i Is RichTextBox Then
        '    '    DirectCast(i, RichTextBox).ReadOnly = True
        '    '    i.BackColor = _ColorNonEditableBackGround
        '    'End If

        '    ''added 1.27.2017 to bind Dates to FR database... FJB
        '    'If TypeOf i Is MaskedTextBox Then
        '    '    DirectCast(i, MaskedTextBox).ReadOnly = True
        '    '    i.BackColor = _ColorNonEditableBackGround
        '    'End If


        '    'If TypeOf i Is xboXComboBox Then
        '    '    i.Hide()
        '    '    If i.Name = cbAccesslevel.Name Or i.Name = cbEditState.Name Then
        '    '        i.Hide()
        '    '    End If
        '    '    _myShadowTextboxIndex(iTextBoxCount).index = iTextBoxCount
        '    '    _myShadowTextboxIndex(iTextBoxCount).name = "ShadowText" + i.Name
        '    '    _myShadowTextBox(iTextBoxCount).Name = "ShadowText" + i.Name
        '    '    _myShadowTextboxIndex(iTextBoxCount).VisibleState = True
        '    '    _myShadowTextBox(iTextBoxCount).Size = i.Size
        '    '    _myShadowTextBox(iTextBoxCount).Location = i.Location
        '    '    _myShadowTextBox(iTextBoxCount).Text = i.Text
        '    '    'Reflect the value of the xboXComboBox to the shadow text box using a binding
        '    '    _myShadowTextBox(iTextBoxCount).DataBindings.Add("Text", i, "Text")
        '    '    _myShadowTextBox(iTextBoxCount).ReadOnly = True
        '    '    _myShadowTextBox(iTextBoxCount).Show()
        '    '    _myShadowTextBox(iTextBoxCount).BackColor = _ColorNonEditableBackGround
        '    '    _myShadowTextBox(iTextBoxCount).Dock = i.Dock
        '    '    _myShadowTextBox(iTextBoxCount).Anchor = i.Anchor
        '    '    iTextBoxCount += 1


        '    'End If

        '    'If TypeOf i Is DateTimePicker Then
        '    '    'The Date Time Picker controls are no longer Bound to to Database.
        '    '    'They will only be use to select a date.
        '    '    Dim MyDataTimePicker As DateTimePicker = DirectCast(i, DateTimePicker)
        '    '    MyDataTimePicker.Format = DateTimePickerFormat.Custom
        '    '    MyDataTimePicker.CustomFormat = "MM/dd/yy"

        '    'End If

        '    'If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then

        '    '    i.Hide()
        '    '    Dim MyCheckBox As CheckBox = DirectCast(i, CheckBox)
        '    '    _myShadowCheckBox(iCheckBoxCount).CheckAlign = MyCheckBox.CheckAlign
        '    '    _myShadowCheckBoxIndex(iCheckBoxCount).index = iCheckBoxCount
        '    '    _myShadowCheckBoxIndex(iCheckBoxCount).name = i.Name
        '    '    _myShadowCheckBoxIndex(iCheckBoxCount).VisibleState = True
        '    '    _myShadowCheckBox(iCheckBoxCount).AutoCheck = False
        '    '    _myShadowCheckBox(iCheckBoxCount).Name = "Shadow" + i.Name 'rename checkbox
        '    '    _myShadowCheckBox(iCheckBoxCount).Size = i.Size
        '    '    _myShadowCheckBox(iCheckBoxCount).Location = i.Location
        '    '    _myShadowCheckBox(iCheckBoxCount).Text = i.Text
        '    '    _myShadowCheckBox(iCheckBoxCount).Font = i.Font
        '    '    _myShadowCheckBox(iCheckBoxCount).RightToLeft = i.RightToLeft
        '    '    _myShadowCheckBox(iCheckBoxCount).DataBindings.Add("CheckState", i, "CheckState")
        '    '    _myShadowCheckBox(iCheckBoxCount).Dock = i.Dock
        '    '    _myShadowCheckBox(iCheckBoxCount).Anchor = i.Anchor
        '    '    _myShadowCheckBox(iCheckBoxCount).Show()

        '    '    iCheckBoxCount += 1
        '    'End If
        'Next

        'Report Body-intialize the controls explicitly
        'Make each of the editable textboxes in the report body tab collection readonly
        'and change the background color to the readonly color
        'rtxtFailureDescription.ReadOnly = True
        'rtxtFailureDescription.BackColor = _ColorNonEditableBackGround
        'rtxtCorrectiveAction.ReadOnly = True
        'rtxtCorrectiveAction.BackColor = _ColorNonEditableBackGround
        'rtxtEngineeringNotes.ReadOnly = True
        'rtxtTCC_Comments.ReadOnly = True
        'rtxtEngineeringNotes.BackColor = _ColorNonEditableBackGround
        'rtxtTCC_Comments.BackColor = _ColorNonEditableBackGround
        ' dgvFailureReportDataGridView.ReadOnly = True



    End Sub 'InitializeShadowControls() 'Removed, Functionality refactored 1/10/2018
    Private Sub InitFailureReportFormViews()
        Try
            'This code setes up the different view and Form sizes for the the views
            For i = 0 To _HeaderGridAndDetailRowStyle.Length - 1
                'Set the default view to the Design view 
                _HeaderGridAndDetailRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                _HeaderGridAndDetailRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height

                If i = TablePanelRowIndex.FailureReportHeader Then 'Failure Rport Header Area in the Table Layout

                    'Browse view
                    _BrowserRowStyle(i).SizeType = SizeType.Absolute
                    _BrowserRowStyle(i).Height = 0
                    'FR VIew
                    _FR_RowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _FR_RowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                    'Split View
                    _HeaderAndGridRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _HeaderAndGridRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                ElseIf i = TablePanelRowIndex.FailureReportNavigation Then 'Navigation buttons in the Table Layout Panel
                    'Browse
                    _BrowserRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _BrowserRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                    'FRVIEW
                    _FR_RowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _FR_RowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                    'Split View
                    _HeaderAndGridRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _HeaderAndGridRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                ElseIf i = TablePanelRowIndex.FailureReportBody Then 'Failure Body Tabs in the Table Layout panel
                    'Browse
                    _BrowserRowStyle(i).SizeType = SizeType.Percent
                    _BrowserRowStyle(i).Height = 0
                    'FR View
                    _FR_RowStyle(i).SizeType = SizeType.Percent
                    _FR_RowStyle(i).Height = 100

                    'Split view
                    _HeaderAndGridRowStyle(i).SizeType = SizeType.Percent
                    _HeaderAndGridRowStyle(i).Height = 0
                ElseIf i = TablePanelRowIndex.FailureReportGrid Then 'The Failure report Grid view

                    'Browser View
                    _BrowserRowStyle(i).SizeType = SizeType.Percent
                    _BrowserRowStyle(i).Height = 100

                    'FR View
                    _FR_RowStyle(i).SizeType = SizeType.Percent
                    _FR_RowStyle(i).Height = 0

                    'Split View
                    _HeaderAndGridRowStyle(i).SizeType = SizeType.Percent
                    _HeaderAndGridRowStyle(i).Height = 100

                ElseIf i = TablePanelRowIndex.FailureReportFilter Then
                    _BrowserRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _BrowserRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height
                    'FR VIew
                    _FR_RowStyle(i).SizeType = SizeType.Percent
                    _FR_RowStyle(i).Height = 0

                    'Split View
                    _HeaderAndGridRowStyle(i).SizeType = TableLayoutPanel3Rows.RowStyles(i).SizeType
                    _HeaderAndGridRowStyle(i).Height = TableLayoutPanel3Rows.RowStyles(i).Height

                End If

            Next
            _BrowserViewFormSize.Height = 700
            _BrowserViewFormSize.Width = 1050
            _ViewHeaderGridAndDetailsSize = Me.Size
            _ViewFR_ViewSize = Me.Size
            _ViewHeaderAndGridSize = Me.Size

        Catch ex As Exception
            v_UpdateLog("Error Intializing Controls" + vbCrLf + ex.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("Error Intializing Controls" + vbCrLf + ex.ToString, eLogLevel.MAX)

        End Try

    End Sub
#End Region '"CONTROL STATE INIT"

    ''' <summary>
    ''' This Subroutine sets the control state when the view changes or the database is updated or a record changes. 
    ''' 1) Sets labels and visable controls based on Anomaly or Failure Report
    ''' 2) Sets labels and visbal controls based on TCC approval or not
    ''' </summary>
    ''' <remarks>Frank Boudreau September 2015</remarks>
    Private Sub SetApproverState(Optional ByVal bEditEnabled As Boolean = False)

        'Flag to hide TCC members, default is do not hide
        Dim bHideTCC As Boolean = False

        'Changed Heading, but did not 

        ''Label the tab based on the type of report
        'If chkAnomely.Checked = True Then
        '    tpDescription.Text = "DESCRIPTION" '"ANOMALY DESCRIPTION"
        '    ' lblReportNumber.Text = "AR#:"
        'Else
        '    tpDescription.Text = "DESCRIPTION" '"FAILURE DESCRIPTION"
        '    'lblReportNumber.Text = "FR#:"
        'End If

        ' If gb_ProcessEvents = True Then


        'Set the flag that indicates if the TCC members should be hid or not based on type of report.
        If (cbTestLevel.Text = "PAC 2" Or cbTestLevel.Text = "FPA" Or cbTestLevel.Text = "OEM") And chkTCC_ApprovalRequired.CheckState <> CheckState.Checked Then
            chkTCC_ApprovalRequired.Checked = True
            chkTCC_ApprovalRequired.CheckState = CheckState.Checked
            'bHideTCC = False
        ElseIf chkTCC_ApprovalRequired.CheckState = CheckState.Indeterminate Then
            chkTCC_ApprovalRequired.Checked = False
            chkTCC_ApprovalRequired.CheckState = CheckState.Unchecked
        End If

        ' End If

        If chkTCC_ApprovalRequired.CheckState = CheckState.Checked Then
            bHideTCC = False
        Else 'PAC 1 or Engineering and TCC over site has not been asked for.

            bHideTCC = True
        End If



        chkShowDelegates.Visible = Not bHideTCC
        'Hide the TCC controls and indicators 
        If bHideTCC Then
            Try
                'Hide the TCC approval label
                lblTCC_Approvals.Visible = False
                lblTCC_Compliance.Visible = False
                lblTCC_Engineering.Visible = False
                lblTCC_Manufacturing.Visible = False
                lblTCC_ProductMangement.Visible = False
                lblTCC_Quality_Product_Delivery.Visible = False
                lblTCC_SYSTEMS.Visible = False

                lblTCC_Approvals.Hide()
                lblTCC_Compliance.Hide()
                lblTCC_Engineering.Hide()
                lblTCC_Manufacturing.Hide()
                lblTCC_ProductMangement.Hide()
                lblTCC_Quality_Product_Delivery.Hide()
                lblTCC_SYSTEMS.Hide()
                If Not (dgvFailureReportDataGridView.CurrentRow.Cells("Approved By").Value Is DBNull.Value) Then
                    cbApprovedBy.Text = dgvFailureReportDataGridView.CurrentRow.Cells("Approved By").Value.ToString
                    Dim MyStop As Integer = 1
                Else
                    cbApprovedBy.Text = ""
                End If

                'added 1.5.2017 after changing to readonly comboboxes
                cbTCC_1_Compliance.Visible = False
                cbTCC_1_Compliance.Hide()
                cbTCC_2_Engineering.Visible = False
                cbTCC_2_Engineering.Hide()
                cbTCC_3_Manufacturing.Visible = False
                cbTCC_3_Manufacturing.Hide()
                cbTCC_4_Product_Management.Visible = False
                cbTCC_4_Product_Management.Hide()
                cbTCC_5_Quality_Product_Delivery.Visible = False
                cbTCC_5_Quality_Product_Delivery.Hide()
                cbTCC_6_SYSTEMS.Visible = False
                cbTCC_6_SYSTEMS.Hide()

            Catch
            End Try

            Try
                'If the approved by record exists then set the Date Approved  
                ' to the date closed record Value
                If cbApprovedBy.Text.Trim <> "" Then

                    If mtxtDateApproved.Text.Trim = "" Then
                        Try
                            'copy the date closed date over if it exists...
                            If Not (dgvFailureReportDataGridView.CurrentRow.Cells("Date Closed").Value Is DBNull.Value) Then
                                Dim MyString As String = (dgvFailureReportDataGridView.CurrentRow.Cells("Date Closed").Value.ToString)
                                dtpDateApproved.Value = MyString.Split(" ")(0)
                                mtxtDateApproved.Text = dtpDateApproved.Value.ToString.Split(" ")(0)
                            End If
                        Catch ex As NullReferenceException 'This needs fixed...FJB
                            v_UpdateLog("Error: " + ex.ToString)
                        Catch ex As Exception
                            MsgBox("Error: " + ex.ToString)
                        End Try
                    End If
                    'ChkFR_Approved.CheckState = CheckState.Checked
                Else 'otherwise it is not approved and the DateTimepicker and shadow textbox should be cleared
                    ' ChkFR_Approved.CheckState = CheckState.Unchecked
                End If
            Catch ex As Exception
                MsgBox("Error: " + ex.ToString)
            End Try

        Else 'Show the TCC members controls and indicators

            'TCC approval label
            lblTCC_Approvals.Visible = True
            lblTCC_Compliance.Visible = True
            lblTCC_Engineering.Visible = True
            lblTCC_Manufacturing.Visible = True
            lblTCC_ProductMangement.Visible = True
            lblTCC_Quality_Product_Delivery.Visible = True
            lblTCC_SYSTEMS.Visible = True

            lblTCC_Approvals.Show()
            lblTCC_Compliance.Show()
            lblTCC_Engineering.Show()
            lblTCC_Manufacturing.Show()
            lblTCC_ProductMangement.Show()
            lblTCC_Quality_Product_Delivery.Show()
            lblTCC_SYSTEMS.Show()

            cbApprovedBy.Text = "Test Compliance Committee"

            'added 1.5.2017 after changing to readonly comboboxes
            If bEditEnabled = True Then

                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.Compliance Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    cbTCC_1_Compliance.Show()
                    cbTCC_1_Compliance.ReadOnly = False
                Else
                    cbTCC_1_Compliance.Hide()
                End If

            Else
                cbTCC_1_Compliance.Show()
                cbTCC_1_Compliance.ReadOnly = True
            End If

            If bEditEnabled = True Then

                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.Engineering Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    cbTCC_2_Engineering.Show()
                    cbTCC_2_Engineering.ReadOnly = False
                Else
                    cbTCC_2_Engineering.Hide()
                End If
            Else
                cbTCC_2_Engineering.Show()
                cbTCC_2_Engineering.ReadOnly = True
            End If

            If bEditEnabled = True Then
                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.Manufacturing Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    cbTCC_3_Manufacturing.Show()
                    cbTCC_3_Manufacturing.ReadOnly = False
                Else
                    cbTCC_3_Manufacturing.Hide()
                End If
            Else
                cbTCC_3_Manufacturing.Show()
                cbTCC_3_Manufacturing.ReadOnly = True
            End If

            If bEditEnabled = True Then
                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.Product_Managment Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    cbTCC_4_Product_Management.Show()
                    cbTCC_4_Product_Management.ReadOnly = False
                Else
                    cbTCC_4_Product_Management.Hide()
                End If
            Else
                cbTCC_4_Product_Management.Show()
                cbTCC_4_Product_Management.ReadOnly = True
            End If


            If bEditEnabled = True Then
                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.Quality_Product_Delivery Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    cbTCC_5_Quality_Product_Delivery.Show()
                    cbTCC_5_Quality_Product_Delivery.ReadOnly = False
                Else
                    cbTCC_5_Quality_Product_Delivery.Hide()
                End If
            Else
                cbTCC_5_Quality_Product_Delivery.Show()
                cbTCC_5_Quality_Product_Delivery.ReadOnly = True
            End If

            If bEditEnabled = True Then
                If chkTCC_ApprovalRequired.Checked = True And (_CurrentUser.ApproverDiscipline = eApproverDiscipline.SYSTEMS Or _CurrentUser.ApproverDiscipline = eApproverDiscipline.Admin) Then
                    '_myShadowTextBox(i).Hide()
                    cbTCC_6_SYSTEMS.Show()
                    cbTCC_6_SYSTEMS.ReadOnly = False
                Else
                    ' _myShadowTextBox(i).Show()
                    cbTCC_6_SYSTEMS.Hide()
                End If
            Else
                '_myShadowTextBox(i).Show()
                cbTCC_6_SYSTEMS.Show()
                cbTCC_6_SYSTEMS.ReadOnly = True
            End If


        End If

        'The Failure Report # should always be read only.  This value only gets changed programically thorugh the GUI
        txtReportNumber.ReadOnly = True

        'textposition is not databound so it needs to be handled manually -  this is so the user can enter the Failure report

        Try
            txtPosition.Text = dgvFailureReportDataGridView.CurrentRow.Cells("New ID").Value.ToString 'implicit conversion
        Catch ex As NullReferenceException 'This Needs Fixed FJB
            v_UpdateLog(ex.ToString, eLogLevel.ERRORS_ONLY)
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try

    End Sub
    Public Sub SetAccessState(Optional ByVal bInAnEditMode As Boolean = False)

        'now turn on as appropriate controls based on access level and State
        'Read only acccess level...
        If _CurrentUser.AccessLevel = eAccessState.READ_ONLY Then

            'hide and disabled
            tsmFileNew.Visible = False
            tsmFileNew.Enabled = False
            tsmFileCopy.Visible = False
            tsmFileCopy.Enabled = False
            tsmFileTransfer.Visible = False
            tsmFileTransfer.Enabled = False
            tsmFileSaveFR_Data.Visible = False
            tsmFileSaveFR_Data.Enabled = False
            tsmFileSaveAndExitEditMode.Visible = False
            tsmFileSaveAndExitEditMode.Enabled = False 'toggle to true to enable saveing
            tsmFileEditCR.Visible = False
            tsmFileEditCR.Enabled = False
            tsmFileEditFailureDesciption.Visible = False
            tsmFileEditFailureDesciption.Enabled = False
            tsmFileEdit.Visible = False
            tsmFileEdit.Enabled = False
            tsmFileEnterApproval.Visible = False
            tsmFileEnterApproval.Enabled = False
            tsmFileCancel.Visible = False
            tsmFileCancel.Enabled = False
            tsmPrint.Enabled = True 'Never in Editmode so allow printing for this access level

            'hide and disabled
            tsmFileAdminEdit.Visible = False
            tsmFileAdminEdit.Enabled = False

            'hide and disabled
            tsmFileDelete.Visible = False
            tsmFileDelete.Enabled = False

            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "):  User Access - Read Only"
        End If

        'Create Access Level, HQA test...
        If _CurrentUser.AccessLevel = eAccessState.CREATE_NEW Then

            'Show and Enabled
            tsmFileNew.Visible = True
            tsmFileNew.Enabled = Not bInAnEditMode
            tsmFileCopy.Visible = True
            tsmFileCopy.Enabled = Not bInAnEditMode

            'Show and Disabled
            tsmFileSaveFR_Data.Visible = True
            tsmFileSaveFR_Data.Enabled = bInAnEditMode
            tsmFileSaveAndExitEditMode.Visible = True
            tsmFileSaveAndExitEditMode.Enabled = bInAnEditMode 'toggle to true to enable saveing
            tsmFileEditFailureDesciption.Visible = True
            tsmFileEditFailureDesciption.Enabled = Not bInAnEditMode
            tsmFileCancel.Visible = True
            tsmFileCancel.Enabled = bInAnEditMode
            tsmPrint.Enabled = Not bInAnEditMode 'only allow printing if not in Edit Mode

            'Hide and disabled
            tsmFileEditCR.Visible = False
            tsmFileEditCR.Enabled = False
            tsmFileEdit.Visible = False
            tsmFileEdit.Enabled = False
            tsmFileEnterApproval.Visible = False
            tsmFileEnterApproval.Enabled = False
            tsmFileAdminEdit.Visible = False
            tsmFileAdminEdit.Enabled = False
            tsmFileTransfer.Visible = False
            tsmFileTransfer.Enabled = False

            'Hide and disabled
            tsmFileDelete.Visible = False
            tsmFileDelete.Enabled = False

            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "):  User Access - HQA Tech"
        End If

        'Corrective Action Edit Rights
        If _CurrentUser.AccessLevel = eAccessState.CR_EDIT Then

            'Show and Enabled
            tsmFileEditCR.Visible = True
            tsmFileEditCR.Enabled = Not bInAnEditMode

            'Show and Disabled
            tsmFileSaveFR_Data.Visible = True
            tsmFileSaveFR_Data.Enabled = bInAnEditMode
            tsmFileSaveAndExitEditMode.Visible = True
            tsmFileSaveAndExitEditMode.Enabled = bInAnEditMode 'toggle to true to enable saveing
            tsmFileCancel.Visible = True
            tsmFileCancel.Enabled = bInAnEditMode
            tsmPrint.Enabled = Not bInAnEditMode 'only allow printing if not in Edit Mode

            'Hide and disabled
            tsmFileNew.Visible = False
            tsmFileNew.Enabled = False
            tsmFileCopy.Visible = False
            tsmFileCopy.Enabled = False
            tsmFileTransfer.Visible = False
            tsmFileTransfer.Enabled = False
            tsmFileAdminEdit.Visible = False
            tsmFileAdminEdit.Enabled = False
            tsmFileEditFailureDesciption.Visible = False
            tsmFileEditFailureDesciption.Enabled = False
            tsmFileEdit.Visible = False
            tsmFileEdit.Enabled = False
            tsmFileEnterApproval.Visible = False
            tsmFileEnterApproval.Enabled = False
            tsmFileDelete.Visible = False
            tsmFileDelete.Enabled = False

            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "):  User Access- Engineer"
        End If

        If _CurrentUser.AccessLevel = eAccessState.POWER Then

            'Show and Enabled
            tsmFileNew.Visible = True
            tsmFileNew.Enabled = Not bInAnEditMode
            tsmFileCopy.Visible = True
            tsmFileCopy.Enabled = Not bInAnEditMode
            tsmFileTransfer.Visible = False
            tsmFileTransfer.Enabled = bInAnEditMode
            tsmFileEdit.Visible = True
            tsmFileEdit.Enabled = Not bInAnEditMode

            'Show and Disabled
            tsmFileSaveFR_Data.Visible = True
            tsmFileSaveFR_Data.Enabled = bInAnEditMode
            tsmFileSaveAndExitEditMode.Visible = True
            tsmFileSaveAndExitEditMode.Enabled = bInAnEditMode 'toggle to true to enable saveing
            tsmFileCancel.Visible = True
            tsmFileCancel.Enabled = bInAnEditMode
            tsmPrint.Enabled = Not bInAnEditMode 'only allow printing if not in Edit Mode

            'Hide and disabled
            tsmFileEditCR.Visible = False
            tsmFileEditCR.Enabled = False
            tsmFileAdminEdit.Visible = False
            tsmFileAdminEdit.Enabled = False
            tsmFileEditFailureDesciption.Visible = False
            tsmFileEditFailureDesciption.Enabled = False

            tsmFileEnterApproval.Visible = False
            tsmFileEnterApproval.Enabled = False
            tsmFileDelete.Visible = False
            tsmFileDelete.Enabled = False
            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "):  User Access - Power User"
        End If

        If _CurrentUser.AccessLevel = eAccessState.APPROVER Then
            'Show and Enabled

            If chkFR_ReadyForReview.CheckState = CheckState.Checked Then
                'Conditionlly show the TCC approver edit mode...
                If _CurrentUser.ApproverDiscipline = eApproverDiscipline.NONE And chkTCC_ApprovalRequired.CheckState = CheckState.Checked Then
                    tsmFileEnterApproval.Visible = False
                    tsmFileEnterApproval.Enabled = bInAnEditMode
                Else
                    tsmFileEnterApproval.Visible = True
                    tsmFileEnterApproval.Enabled = Not bInAnEditMode
                End If
            Else
                tsmFileEnterApproval.Visible = False
                tsmFileEnterApproval.Enabled = bInAnEditMode
            End If

            tsmFileEditCR.Visible = True
            tsmFileEditCR.Enabled = Not bInAnEditMode

            'Show and Disabled
            tsmFileSaveFR_Data.Visible = True
            tsmFileSaveFR_Data.Enabled = bInAnEditMode
            tsmFileSaveAndExitEditMode.Visible = True
            tsmFileSaveAndExitEditMode.Enabled = bInAnEditMode 'toggle to true to enable saveing
            tsmFileCancel.Visible = True
            tsmFileCancel.Enabled = bInAnEditMode
            tsmPrint.Enabled = Not bInAnEditMode 'only allow printing if not in Edit Mode

            'Hide and disabled

            tsmFileNew.Visible = False
            tsmFileNew.Enabled = False
            tsmFileCopy.Visible = False
            tsmFileCopy.Enabled = False
            tsmFileTransfer.Visible = False
            tsmFileTransfer.Enabled = False
            tsmFileAdminEdit.Visible = False
            tsmFileAdminEdit.Enabled = False
            tsmFileEditFailureDesciption.Visible = False
            tsmFileEditFailureDesciption.Enabled = False
            tsmFileEdit.Visible = False
            tsmFileEdit.Enabled = False
            tsmFileDelete.Visible = False
            tsmFileDelete.Enabled = False

            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "):  User Access - Approver"
        End If

        'Only an administator can delete a record
        If _CurrentUser.AccessLevel = eAccessState.ADMIN Then
            'Show and Enabled
            tsmFileNew.Visible = True
            tsmFileNew.Enabled = Not bInAnEditMode
            tsmFileCopy.Visible = True
            tsmFileCopy.Enabled = Not bInAnEditMode
            tsmFileTransfer.Visible = True
            tsmFileTransfer.Enabled = Not bInAnEditMode
            tsmFileAdminEdit.Visible = True
            tsmFileAdminEdit.Enabled = Not bInAnEditMode
            tsmFileEditCR.Visible = False
            tsmFileEditCR.Enabled = Not bInAnEditMode
            tsmFileEdit.Visible = False
            tsmFileEdit.Enabled = Not bInAnEditMode
            tsmFileEditFailureDesciption.Visible = False
            tsmFileEditFailureDesciption.Enabled = Not bInAnEditMode
            tsmFileEnterApproval.Visible = False
            tsmFileEnterApproval.Enabled = Not bInAnEditMode
            tsmPrint.Enabled = Not bInAnEditMode 'only allow printing if not in Edit Mode

            'Show and Disabled
            tsmFileSaveFR_Data.Visible = True
            tsmFileSaveFR_Data.Enabled = bInAnEditMode
            tsmFileSaveAndExitEditMode.Visible = True
            tsmFileSaveAndExitEditMode.Enabled = bInAnEditMode 'toggle to true to enable saveing
            tsmFileCancel.Visible = True
            tsmFileCancel.Enabled = bInAnEditMode


            'Hide and disabled
            tsmFileDelete.Visible = True
            tsmFileDelete.Enabled = bInAnEditMode 'Not bInAnEditMode

            'update Form Border
            Me.Text = "Failure Report Database (Version " + _SoftwareVersion + "): User Access - Admin"
        End If
    End Sub

    Sub SetReadOnlyState(ByVal NextState As eEditState, ByRef CurrentState As eEditState, ByVal AccessLevel As eAccessState)
        'First check to see if user wants to save data...
        If _RecordSaved = False Then
            Dim Response
            Response = MsgBox("Are you sure you want to disgard changes?", vbYesNo)
            If Response = vbNo Then
                'Stay in Editmode exit thgie sub
                Exit Sub
            End If
        End If

        'inhibit events that are driven by text values changing in controls
        gb_ProcessEvents = False

        'Set Control to Correct state..
        'The Data Gridview
        dgvFailureReportDataGridView.Enabled = True
        dgvFailureReportDataGridView.ReadOnly = True

        Enable_Navigation(True)
        Enable_AttachmentControls(False)
        Enable_EditTestInfo(False)
        Enable_Edit_Corrective_Action(False)
        Enable_Edit_Approval(False)
        Enable_EUT_Group_Based_On_EUT_TYPE(False)
        Enable_EditMode_tsmFileMenu(False)
        'DisplayShadowControl(True)
        SetApproverState()
        SetAccessState(False)

        'do the update last so user doesn't accidently 
        'This restores the Database data to the the controls
        If _RecordSaved = False Then
            Cursor = Cursors.WaitCursor
            'cancel the edit
            v_UpdateLog("Start Cancel Edit DB: " + Now, eLogLevel.ERROR_AND_INFORMATION)
            gMyFailureReportBindingSource.CancelEdit()
            v_UpdateLog("End Cancel Edit DB: " + Now, eLogLevel.ERROR_AND_INFORMATION)
            'reload the original data for the current record only...
            v_UpdateLog("Start Database Access: " + Now, eLogLevel.ERROR_AND_INFORMATION)
            gMyFailureReportDBDataAdaptor.Fill(CInt(txtPosition.Text), 1, gMyFailureReportDataTable)
            v_UpdateLog("Finish Database Access: " + Now, eLogLevel.ERROR_AND_INFORMATION)
            'Set the saved flag
            _RecordSaved = True
            Cursor = Cursors.Default
        End If
        'Reenable Processing Events
        gb_ProcessEvents = True
        'Refresh GUI
        Me.Refresh()

        CurrentState = NextState
    End Sub


    Public Sub StateMachine(ByVal NextState As eEditState, ByRef CurrentState As eEditState, ByVal AccessLevel As eAccessState)
        If NextState <> CurrentState Then
            TableLayoutPanel3Rows.Hide()
        End If

        dgvFailureReportDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow

        Select Case NextState
            Case eEditState.READ_ONLY

                'First check to see if user wants to save data...
                If _RecordSaved = False Then
                    Dim Response
                    Response = MsgBox("Are you sure you want to disgard changes?", vbYesNo)
                    If Response = vbNo Then
                        'Stay in Editmode exit thgie sub
                        Exit Sub
                    End If
                End If

                'inhibit events that are driven by text values changing in controls
                gb_ProcessEvents = False



                'Set Control to Correct state..
                'The Data Gridview
                dgvFailureReportDataGridView.Enabled = True
                dgvFailureReportDataGridView.ReadOnly = True

                Enable_Navigation(True)
                Enable_AttachmentControls(False)
                Enable_EditTestInfo(False)
                Enable_Edit_Corrective_Action(False)
                Enable_Edit_Approval(False)
                Enable_EUT_Group_Based_On_EUT_TYPE(False)
                Enable_EditMode_tsmFileMenu(False)
                'DisplayShadowControl(True)
                SetApproverState()
                SetAccessState(False)


                'do the update last so user doesn't accidently 
                'This restores the Database data to the the controls
                If _RecordSaved = False Then
                    Cursor = Cursors.WaitCursor
                    'cancel the edit
                    v_UpdateLog("Start Cancel Edit DB: " + Now, eLogLevel.ERROR_AND_INFORMATION)
                    gMyFailureReportBindingSource.CancelEdit()
                    v_UpdateLog("End Cancel Edit DB: " + Now, eLogLevel.ERROR_AND_INFORMATION)
                    'reload the original data for the current record only...
                    v_UpdateLog("Start Database Access: " + Now, eLogLevel.ERROR_AND_INFORMATION)
                    gMyFailureReportDBDataAdaptor.Fill(CInt(txtPosition.Text), 1, gMyFailureReportDataTable)
                    v_UpdateLog("Finish Database Access: " + Now, eLogLevel.ERROR_AND_INFORMATION)
                    'Set the saved flag
                    _RecordSaved = True
                    Cursor = Cursors.Default
                End If

                gb_ProcessEvents = True


                Me.Refresh()




                CurrentState = NextState

            Case eEditState.EDIT_CREATE_NEW
                If AccessLevel = eAccessState.CREATE_NEW Or AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.POWER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False



                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_EditTestInfo(True) 'Allow Editing Failure Test Result Data 
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_Edit_Corrective_Action(False) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(False) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(True) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    'DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState() 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else

                    MsgBox("Insuffcient Privileges  to enter state: " + NextState.ToString)
                    Return
                End If
            Case eEditState.EDIT_COPY
                If AccessLevel = eAccessState.CREATE_NEW Or AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.POWER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False



                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_EditTestInfo(True) 'Allow Editing Failure Test Result Data 
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_Edit_Corrective_Action(False) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(False) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(True) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    ' DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState() 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else

                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If
            Case eEditState.EDIT_FR_DETAILS
                If AccessLevel = eAccessState.CREATE_NEW Or AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.POWER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False

                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_EditTestInfo(True) 'Allow Editing Failure Test Result Data 
                    Enable_Edit_Corrective_Action(False) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(False) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(True) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    ' DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState() 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else
                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If

            Case eEditState.EDIT_FR_CORRECTIVE_ACTION
                If AccessLevel = eAccessState.CR_EDIT Or AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.POWER Or AccessLevel = eAccessState.APPROVER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False

                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_EditTestInfo(False) 'Allow Editing Failure Test Result Data 
                    Enable_Edit_Corrective_Action(True) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(False) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(False) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    '   DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState() 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else
                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If

            Case eEditState.EDIT_POWER
                If AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.POWER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False

                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_EditTestInfo(True) 'Allow Editing Failure Test Result Data 
                    Enable_Edit_Corrective_Action(True) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(False) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(True) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    '   DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState() 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else
                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If

            Case eEditState.EDIT_SET_APPROVALS
                If AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.APPROVER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False

                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_AttachmentControls(False) 'Approvers Cannot Edit the Failure Report
                    Enable_EditTestInfo(False) 'Allow Editing Failure Test Result Data 
                    Enable_Edit_Corrective_Action(False) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(True) 'Can approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(False) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    ' DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState(True) 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else
                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If

            Case eEditState.EDIT_ADMIN
                If AccessLevel = eAccessState.ADMIN Or AccessLevel = eAccessState.APPROVER Then
                    'inhibit events that are driven by text values changing in controls
                    gb_ProcessEvents = False

                    'Set Control to Correct state..
                    'The Data Gridview
                    dgvFailureReportDataGridView.Enabled = False
                    dgvFailureReportDataGridView.ReadOnly = False

                    Enable_Navigation(False) 'Disable Navigation
                    Enable_AttachmentControls(True) 'Allow Adding and Removing Attachments
                    Enable_EditTestInfo(True) 'Allow Editing Failure Test Result Data 
                    Enable_Edit_Corrective_Action(True) 'Disable Failure Report Corrective ACtion Editing
                    Enable_Edit_Approval(True, True) 'Can't approve in this state
                    Enable_EUT_Group_Based_On_EUT_TYPE(True) 'Show hide EUT groups based on EUT Type 
                    Enable_EditMode_tsmFileMenu(True) 'Change menu items to Edit mode 
                    '  DisplayShadowControl(False) 'hide shadow controls 
                    SetApproverState(True) 'Show hide TCC controls
                    SetAccessState(True) 'Show hide controls based on User Access level
                    gb_ProcessEvents = True
                    Me.Refresh()
                    CurrentState = NextState
                Else
                    MsgBox("Insuffcient Priviliages to enter state: " + NextState.ToString)
                    Return
                End If

        End Select
        TableLayoutPanel3Rows.Show()
    End Sub
    ''' <summary>
    ''' Enable/Disable Navigation Controls
    ''' </summary>
    ''' <param name="bNavigationEnabled"></param>
    ''' <remarks>Frank Boudreau 2.5.2017</remarks>
    Public Sub Enable_Navigation(ByVal bNavigationEnabled As Boolean)
        'Navigation Controls
        btnOldestRecord.Enabled = bNavigationEnabled
        btnPrevRecord.Enabled = bNavigationEnabled
        txtPosition.Enabled = bNavigationEnabled
        btnNextRecord.Enabled = bNavigationEnabled
        btnNewestRecord.Enabled = bNavigationEnabled
    End Sub
    ''' <summary>
    ''' Enable / Disable Controls used for attaching and Removing attachments
    ''' </summary>
    ''' <param name="bEditEnabled"></param>
    ''' <remarks>Frank Boudreau 2.5.2017</remarks>
    Public Sub Enable_AttachmentControls(ByVal bEditEnabled As Boolean)
        btnAttachFile.Enabled = bEditEnabled
        btnRemoveAttachment.Enabled = bEditEnabled
    End Sub
    ''' <summary>
    ''' This Function is for Enabling/Disabling Controls Used for Creating a new FR
    ''' </summary>
    ''' <param name="bEditEnabled">TRUE = Enable, FALSE = Disable</param>
    ''' <remarks>Frank Boudreau 2.5.2017</remarks>
    Public Sub Enable_EditTestInfo(ByVal bEditEnabled As Boolean)
        'txtReportNumber.Enabled = True 'This is always read only any way...

        If bEditEnabled = True Then
            'Header Info

            'Header Buttons
            btnSelectTest.Enabled = True
            btnSelectTest.Visible = True

            'Header Datetimepicker
            dtpDateFailed.Enabled = True
            dtpDateFailed.Visible = True
            dtpDateFailed.Show()

            'Header textboxes
            mtxtDateFailed.ReadOnly = False
            mtxtDateFailed.BackColor = Color.White

            dtpDateFailedSampleReady.Enabled = True
            dtpDateFailedSampleReady.Visible = True
            dtpDateFailedSampleReady.Show()

            mtxtDateFailedSampleReady.ReadOnly = False
            mtxtDateFailedSampleReady.BackColor = Color.White

            txtProject.ReadOnly = False
            txtProject.BackColor = Color.White


            'txtProjectLead.ReadOnly = False          'Project Lead
            'txtProjectLead.BackColor = Color.White

            'Header CheckBoxes
            chkAnomely.AutoCheck = True
            chkTCC_ApprovalRequired.AutoCheck = True
            chkAnomely.BackColor = Color.White
            If chkTCC_ApprovalRequired.Checked Then
                chkTCC_ApprovalRequired.BackColor = Color.GreenYellow
            Else
                chkTCC_ApprovalRequired.BackColor = Color.White
            End If

            chkPass.AutoCheck = True
            chkFail.AutoCheck = True
            chkPass.BackColor = Color.White

            If chkFail.Checked = True Then
                chkFail.BackColor = Color.Red
                chkFail.ForeColor = Color.White
            Else
                chkFail.BackColor = Color.White
                chkFail.ForeColor = Color.Black
            End If

            chkFailedSampleReady.AutoCheck = True
            chkFailedSampleReady.BackColor = Color.White

            'Header xboXComboBoxes
            cbTestLevel.ReadOnly = False
            cbTestType.ReadOnly = False
            cbTestedBy.ReadOnly = False
            'cbTestName.ReadOnly = False
            cbEUTType.ReadOnly = False
            '   Dim TempString = cbProjectNumber.Text
            cbProjectLead.ReadOnly = False
            cbProjectNumber.ReadOnly = False
            'cbProjectNumber.DropDownStyle = ComboBoxStyle.DropDownList
            cbProjectNumber.ContextMenuStrip = cmsProject
            cbProjectNumber.ContextMenuStrip.Enabled = True
            ' XboXReadOnly.BackColor = _ColorNonEditableBackGround
            'EUT Info
            Enable_EUT_Group_Based_On_EUT_TYPE(True)
            '  cbProjectNumber.Text = TempString

            'Body info
            rtxtDescription.ReadOnly = False
            rtxtDescription.BackColor = Color.White
            rtxtTestEquipmentIDlist.ReadOnly = False
            rtxtTestEquipmentIDlist.BackColor = Color.White
            btnTestEquipment.Enabled = True

            'The Failure Report body handled seperately from header
            'The purpose of this code is to provide a means to prevent overwriting data, 
            'add the author of the edit, 
            'and provide a means to restore the orginal text if the user chooses not save.

            'Get the current Date
            _CurrentDate = Now

            'remove extra lf's <<<Band Aid>>>> - FJB 4.17.2017
            If rtxtDescription.Text.Trim = "" Then
                rtxtDescription.Text = ""
            Else
                rtxtDescription.Text = rtxtDescription.Text.Trim + vbLf
            End If

            'This is the length of the orginal data in the textbox
            _myTxtReportDescriptionBackUpLength = rtxtDescription.TextLength

            'Add user and time stamp
            'Changed all of the vbCRLF to vbLF.  The reason for this is the rixh text box counts the vbCRLF as one character when calculating start position, however, string.length counts it 
            'as two characters...  The LF goes to the nect line



            'This is the orginal data in the textbox with the current user and date appended. 
            _myTxtReportDescriptionBackUpEdit = rtxtDescription.Text + vbLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbLf
            'This is the orginal data in the textbox
            _myTxtReportDescriptionBackUp = _myTxtReportDescriptionBackUpEdit

            'Right click menus...
            cmsProject.Enabled = True

            'always non user editable...
            txtOrginalProjectNum.ReadOnly = True
            txtOrginalProjectNum.BackColor = _ColorNonEditableBackGround
            txtOrginalReportNum.ReadOnly = True
            txtOrginalReportNum.BackColor = _ColorNonEditableBackGround

        Else 'Non Editable....
            'Header Info

            'Header Buttons
            btnSelectTest.Enabled = False
            btnSelectTest.Visible = False

            'Date Time Picker...
            dtpDateFailed.Enabled = False
            dtpDateFailed.Visible = False
            dtpDateFailed.Hide()

            'Header textboxes
            mtxtDateFailed.ReadOnly = True
            mtxtDateFailed.BackColor = _ColorNonEditableBackGround

            dtpDateFailedSampleReady.Enabled = False
            dtpDateFailedSampleReady.Visible = False
            dtpDateFailedSampleReady.Hide()

            mtxtDateFailedSampleReady.ReadOnly = True
            mtxtDateFailedSampleReady.BackColor = _ColorNonEditableBackGround


            'always non user editable...
            txtOrginalProjectNum.ReadOnly = True
            txtOrginalProjectNum.BackColor = _ColorNonEditableBackGround
            txtOrginalReportNum.ReadOnly = True
            txtOrginalReportNum.BackColor = _ColorNonEditableBackGround

            'cbProjectNumber.BackColor = _ColorNonEditableBackGround
            txtProject.ReadOnly = True
            txtProject.BackColor = _ColorNonEditableBackGround
            cmsProject.Enabled = False

            'txtProjectLead.ReadOnly = True 'Project Lead
            'txtProjectLead.BackColor = _ColorNonEditableBackGround

            'Header CheckBoxes

            chkAnomely.AutoCheck = False
            chkTCC_ApprovalRequired.AutoCheck = False
            chkAnomely.BackColor = _ColorNonEditableBackGround
            'chkTCC_ApprovalRequired.BackColor = _ColorNonEditableBackGround
            If chkTCC_ApprovalRequired.Checked Then
                If ChkFR_Approved.Checked = False Then
                    chkTCC_ApprovalRequired.BackColor = Color.Yellow
                Else
                    chkTCC_ApprovalRequired.BackColor = Color.GreenYellow
                End If

            Else
                chkTCC_ApprovalRequired.BackColor = _ColorNonEditableBackGround
            End If
            chkPass.AutoCheck = False
            chkFail.AutoCheck = False


            If chkFail.Checked = True Then
                If ChkFR_Approved.Checked = False Then
                    chkFail.ForeColor = Color.White
                    chkFail.BackColor = Color.Red
                Else
                    chkFail.BackColor = Color.GreenYellow
                    chkFail.ForeColor = Color.Black
                End If


            Else
                chkFail.BackColor = _ColorNonEditableBackGround
                chkFail.ForeColor = Color.Black
            End If

            If chkAnomely.Checked = True Then

                If ChkFR_Approved.Checked = False Then
                    chkPass.BackColor = Color.Yellow
                Else
                    chkPass.BackColor = Color.GreenYellow
                End If
            Else
                chkPass.BackColor = _ColorNonEditableBackGround
            End If

            '12.9.2022
            chkFR_ReadyForReview.AutoCheck = False
            chkFailedSampleReady.BackColor = _ColorNonEditableBackGround


            'Header xboXComboBoxes
            cbTestLevel.ReadOnly = True
            cbTestType.ReadOnly = True
            cbTestedBy.ReadOnly = True
            ' cbTestName.ReadOnly = True
            cbEUTType.ReadOnly = True
            cbProjectNumber.ReadOnly = True
            cbProjectNumber.ContextMenuStrip = Nothing
            cbProjectLead.ReadOnly = True

            'EUT Info
            Enable_EUT_Group_Based_On_EUT_TYPE(False)

            'Body info
            txtTest.ReadOnly = True
            txtTest.BackColor = _ColorNonEditableBackGround
            rtxtDescription.ReadOnly = True
            rtxtDescription.BackColor = _ColorNonEditableBackGround
            rtxtTestEquipmentIDlist.ReadOnly = True
            rtxtTestEquipmentIDlist.BackColor = _ColorNonEditableBackGround
            btnTestEquipment.Enabled = False

            'Right click menus...
            cmsProject.Enabled = False
        End If



    End Sub
    Public Sub SetFR_VIEW(ByVal FR_VIEW As eFailureReportView)

        gb_ProcessEvents = False 'Suspend Control Events

        Select Case FR_VIEW
            Case eFailureReportView.FR_VIEW
                TableLayoutPanel3Rows.Hide()
                'TableLayoutPanel1.Hide()
                Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
                txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion
                For i = 0 To TableLayoutPanel3Rows.RowCount - 1
                    TableLayoutPanel3Rows.RowStyles(i).SizeType = _FR_RowStyle(i).SizeType
                    TableLayoutPanel3Rows.RowStyles(i).Height = _FR_RowStyle(i).Height
                Next
                pnlReportHeader.Show()
                tcReportBody.Show()
                Me.Size = _ViewFR_ViewSize
                TableLayoutPanel3Rows.Show()
            Case eFailureReportView.HEADER_AND_GRID_VIEW
                TableLayoutPanel3Rows.Hide()
                Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
                txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion
                For i = 0 To TableLayoutPanel3Rows.RowCount - 1
                    TableLayoutPanel3Rows.RowStyles(i).SizeType = _HeaderAndGridRowStyle(i).SizeType
                    TableLayoutPanel3Rows.RowStyles(i).Height = _HeaderAndGridRowStyle(i).Height
                Next

                pnlReportHeader.Show()
                tcReportBody.Hide()
                Me.Size = _ViewHeaderAndGridSize
                TableLayoutPanel3Rows.Show()
            Case eFailureReportView.BROWSER_VIEW
                TableLayoutPanel3Rows.Hide()
                Try
                    Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
                    txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion

                    For i = 0 To TableLayoutPanel3Rows.RowCount - 1
                        TableLayoutPanel3Rows.RowStyles(i).SizeType = _BrowserRowStyle(i).SizeType
                        TableLayoutPanel3Rows.RowStyles(i).Height = _BrowserRowStyle(i).Height
                    Next
                    pnlReportHeader.Hide()
                    'Panel2.Hide()
                    tcReportBody.Hide()
                    Me.Size = _BrowserViewFormSize
                Catch
                End Try
                TableLayoutPanel3Rows.Show()
            Case eFailureReportView.HEADER_GRID_AND_DETAIL_VIEW
                TableLayoutPanel3Rows.Hide()
                Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
                txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion
                For i = 0 To TableLayoutPanel3Rows.RowCount - 1
                    TableLayoutPanel3Rows.RowStyles(i).SizeType = _HeaderGridAndDetailRowStyle(i).SizeType
                    TableLayoutPanel3Rows.RowStyles(i).Height = _HeaderGridAndDetailRowStyle(i).Height
                Next
                pnlReportHeader.Show()
                'Panel2.Show()
                tcReportBody.Show()
                Me.Size = _ViewHeaderGridAndDetailsSize
                TableLayoutPanel3Rows.Show()
            Case Else

        End Select
        _FailureReportView = FR_VIEW
        gb_ProcessEvents = True 'Resume Control Events
    End Sub
    ''' <summary>
    ''' This Function is for Enabling/Disabling Controls Associated with Meter/EUT1 Info
    ''' </summary>
    ''' <param name="bEditEnabled">TRUE = Enable, FALSE = Disable</param>
    ''' <remarks>Frank Boudreau 2.5.2017</remarks>
    Public Sub Enable_MeterGroup(ByVal bEditEnabled As Boolean)
        cbMeterManufacturer.ReadOnly = Not bEditEnabled
        cbMeterModel.ReadOnly = Not bEditEnabled
        cbMeterType.ReadOnly = Not bEditEnabled
        cbMeterSubType.ReadOnly = Not bEditEnabled
        cbMeterSubTypeII.ReadOnly = Not bEditEnabled
        cbMeterSerialNumber.ReadOnly = Not bEditEnabled
        cbMeterForm.ReadOnly = Not bEditEnabled
        cbMeterBase.ReadOnly = Not bEditEnabled
        cbMeterDSP_Rev.ReadOnly = Not bEditEnabled
        cbMeterFW_Ver.ReadOnly = Not bEditEnabled
        cbMeterPCBA.ReadOnly = Not bEditEnabled
        cbMeterPCBA_Rev.ReadOnly = Not bEditEnabled
        cbMeterSoftware.ReadOnly = Not bEditEnabled
        cbMeterSoftwareRev.ReadOnly = Not bEditEnabled
        cbMeterVoltage.ReadOnly = Not bEditEnabled
        rtxtMeterNotes.ReadOnly = Not bEditEnabled
        If bEditEnabled Then
            rtxtMeterNotes.BackColor = Color.White
        Else
            rtxtMeterNotes.BackColor = _ColorNonEditableBackGround
        End If
    End Sub
    ''' <summary>
    ''' This Function is for Enabling/Disabling Controls Associated with AMR/EUT2 Info
    ''' </summary>
    ''' <param name="bEditEnabled">TRUE = Enable, FALSE = Disable</param>
    ''' <remarks>Frank Boudreau 2.5.2017</remarks>
    Public Sub Enable_AMRGroup(ByVal bEditEnabled As Boolean)
        cbAMR_Manufacturer.ReadOnly = Not bEditEnabled
        cbAMR_Model.ReadOnly = Not bEditEnabled
        cbAMR_TYPE.ReadOnly = Not bEditEnabled
        cbAMR_SubType.ReadOnly = Not bEditEnabled
        cbAMR_SUBtypeII.ReadOnly = Not bEditEnabled
        cbAMR_SUBtypeIII.ReadOnly = Not bEditEnabled
        cbAMR_SerialNumber.ReadOnly = Not bEditEnabled
        cbAMR_IP_LAN_ID.ReadOnly = Not bEditEnabled
        cbAMR_FW_Rev.ReadOnly = Not bEditEnabled
        cbAMR_PCBA.ReadOnly = Not bEditEnabled
        cbAMR_PCBA_Rev.ReadOnly = Not bEditEnabled
        cbAMR_Software.ReadOnly = Not bEditEnabled
        cbAMR_Software_Rev.ReadOnly = Not bEditEnabled
        cbAMR_Voltage.ReadOnly = Not bEditEnabled
        rtxtAMR_Notes.ReadOnly = Not bEditEnabled
        If bEditEnabled Then
            rtxtAMR_Notes.BackColor = Color.White
        Else
            rtxtAMR_Notes.BackColor = _ColorNonEditableBackGround
        End If
    End Sub
    Public Sub Enable_EUT_Group_Based_On_EUT_TYPE(ByVal bEditEnabled As Boolean)
        If cbEUTType.Text = "Meter Only" Then
            Enable_MeterGroup(bEditEnabled)
            Enable_AMRGroup(False)
            lblMeter.Text = "Meter"
            lblAMR.Text = "AMR"
        ElseIf cbEUTType.Text = "AMR Only" Then
            Enable_MeterGroup(False)
            Enable_AMRGroup(bEditEnabled)
            lblMeter.Text = "Meter"
            lblAMR.Text = "AMR"
        ElseIf cbEUTType.Text = "AMI" Then
            Enable_MeterGroup(bEditEnabled)
            Enable_AMRGroup(bEditEnabled)
            lblMeter.Text = "Meter"
            lblAMR.Text = "AMR"
        ElseIf cbEUTType.Text = "OTHER EUT" Then
            Enable_MeterGroup(bEditEnabled)
            Enable_AMRGroup(bEditEnabled)
            lblMeter.Text = "EUT 1"
            lblAMR.Text = "EUT 2"
        Else
            Enable_MeterGroup(False)
            Enable_AMRGroup(False)
            lblMeter.Text = "Meter"
            lblAMR.Text = "AMR"
        End If
    End Sub
    ''' <summary>
    ''' Enable or Disable controls associated with entering a Corrective Action for an FR
    ''' When a user enters corrective action they are allowed to 
    ''' Enter the Date it was corrected
    ''' Set the Flag that indicated if the FR is ready for review
    ''' Enable editing the Engineering notes
    ''' Enable Editing the Corrective action
    ''' </summary>
    ''' <param name="bEditEnabled">Enable for editing when True</param>
    ''' <remarks></remarks>
    Public Sub Enable_Edit_Corrective_Action(ByVal bEditEnabled As Boolean)
        If bEditEnabled = True Then
            'date Time Picker...
            dtpDateCorrected.Enabled = True
            dtpDateCorrected.Visible = True
            dtpDateCorrected.Show()

            'Header Textboxes
            cbCorrectedBy.ReadOnly = False
            cbCorrectedBy.BackColor = Color.White
            mtxtDateCorrected.ReadOnly = False
            mtxtDateCorrected.BackColor = Color.White

            'header Checkboxes
            chkFR_ReadyForReview.AutoCheck = True
            If chkFR_ReadyForReview.Checked Then
                chkFR_ReadyForReview.ForeColor = Color.Black
                If ChkFR_Approved.Checked Then
                    chkFR_ReadyForReview.BackColor = Color.GreenYellow
                Else
                    chkFR_ReadyForReview.BackColor = Color.Yellow
                End If

            Else
                chkFR_ReadyForReview.BackColor = Color.Red
                chkFR_ReadyForReview.ForeColor = Color.White

            End If
            '  chkFR_ReadyForReview.BackColor = Color.White
            'chkFR_ReadyForReview.Enabled = True

            'body Textboxes...
            rtxtCorrectiveAction.ReadOnly = False
            rtxtCorrectiveAction.BackColor = Color.White
            rtxtEngineeringNotes.ReadOnly = False
            rtxtEngineeringNotes.BackColor = Color.White


            'The Failure Report body handled seperately from header
            'The purpose of this code is to provide a means to prevent overwriting data, 
            'add the author of the edit, 
            'and provide a means to restore the orginal text if the user chooses not save.

            'Get the current Date
            _CurrentDate = Now

            'This is the length of the orginal data in the textbox
            Dim TempString As String = rtxtEngineeringNotes.Text
            _myTxtCorrectiveActionBackupLength = rtxtCorrectiveAction.TextLength
            _myEngineeringNotesBackUpLength = TempString.Length

            'Add user and time stamp
            'Changed all of the vbCRLF to vbLF.  The reason for this is the rich text box counts the vbCRLF as one character when calculating start position, however, string.length counts it 
            'as two characters...  The LF goes to the next line
            'This is the orginal data in the textbox with the current user and date appended. 

            _myTxtCorrectiveActionBackupEdit = rtxtCorrectiveAction.Text + vbLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbLf
            _myTxtCorrectiveActionBackUp = _myTxtCorrectiveActionBackupEdit


            _myEngineeringNotesBackupEdit = rtxtEngineeringNotes.Text + vbLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbLf
            _myEngineeringNotesBackup = _myEngineeringNotesBackupEdit

        Else
            'date Time Picker...
            dtpDateCorrected.Enabled = False
            dtpDateCorrected.Visible = False
            dtpDateCorrected.Hide()

            'Header Textboxes
            cbCorrectedBy.ReadOnly = True
            cbCorrectedBy.BackColor = _ColorNonEditableBackGround
            mtxtDateCorrected.ReadOnly = True
            mtxtDateCorrected.BackColor = _ColorNonEditableBackGround

            'header Checkboxes
            'chkFR_ReadyForReview.Enabled = False
            chkFR_ReadyForReview.AutoCheck = False
            If chkFR_ReadyForReview.Checked Then
                chkFR_ReadyForReview.ForeColor = Color.Black
                If ChkFR_Approved.Checked Then
                    chkFR_ReadyForReview.BackColor = Color.GreenYellow
                Else
                    chkFR_ReadyForReview.BackColor = Color.Yellow
                End If
            Else
                chkFR_ReadyForReview.BackColor = Color.Red
                chkFR_ReadyForReview.ForeColor = Color.White
            End If


            'body Textboxes...
            rtxtCorrectiveAction.ReadOnly = True
            rtxtCorrectiveAction.BackColor = _ColorNonEditableBackGround
            rtxtEngineeringNotes.ReadOnly = True
            rtxtEngineeringNotes.BackColor = _ColorNonEditableBackGround

        End If
    End Sub
    ''' <summary>
    ''' Enable and Disable Controls that allow an FR to be approved
    ''' </summary>
    ''' <param name="bEditEnable">True = Enable; False = Disable</param>
    ''' <param name="bShowTCC">When True Show the TCC Txt boxes</param>
    ''' <remarks></remarks>
    Public Sub Enable_Edit_Approval(ByVal bEditEnable As Boolean, Optional ByVal bShowTCC As Boolean = False)
        If bEditEnable Then

            'date time pickers
            dtpDateClosed.Enabled = True
            dtpDateApproved.Enabled = True
            dtpDateClosed.Visible = True
            dtpDateApproved.Visible = True
            dtpDateClosed.Show()
            dtpDateApproved.Show()

            'Header Textboxes
            mtxtDateClosed.ReadOnly = False
            mtxtDateApproved.BackColor = Color.White
            mtxtDateApproved.ReadOnly = False
            mtxtDateClosed.BackColor = Color.White

            'Only allow this field to be edited if Admin or TCC approval is not required...
            If chkTCC_ApprovalRequired.Checked = False Or _CurrentUser.AccessLevel = eAccessState.ADMIN Then
                cbApprovedBy.ReadOnly = False
                cbApprovedBy.BackColor = Color.White
            End If


            'header Checkboxes
            'chkShowDelegates.Enabled = True
            chkShowDelegates.AutoCheck = True
            chkShowDelegates.BackColor = Color.White
            chkShowDelegates.Visible = bShowTCC

            ChkFR_Approved.Enabled = True
            ChkFR_Approved.AutoCheck = True
            Dim CheckBoxFont As Font
            CheckBoxFont = ChkFR_Approved.Font
            'CheckBoxFont.
            If ChkFR_Approved.Checked Then
                ChkFR_Approved.BackColor = Color.GreenYellow
                ChkFR_Approved.ForeColor = Color.Black
            Else
                ChkFR_Approved.BackColor = Color.Red
                ChkFR_Approved.ForeColor = Color.White
                Dim MyFont As Font = ChkFR_Approved.Font
            End If
            ' ChkFR_Approved.BackColor = Color.White
            ChkFR_Approved.Visible = True





            'header xboXComboBoxes
            cbTCC_1_Compliance.ReadOnly = False
            cbTCC_2_Engineering.ReadOnly = False
            cbTCC_3_Manufacturing.ReadOnly = False
            cbTCC_4_Product_Management.ReadOnly = False
            cbTCC_5_Quality_Product_Delivery.ReadOnly = False
            cbTCC_6_SYSTEMS.ReadOnly = False

            'header xboXComboBoxes
            cbTCC_1_Compliance.Visible = bShowTCC
            cbTCC_2_Engineering.Visible = bShowTCC
            cbTCC_3_Manufacturing.Visible = bShowTCC
            cbTCC_4_Product_Management.Visible = bShowTCC
            cbTCC_5_Quality_Product_Delivery.Visible = bShowTCC
            cbTCC_6_SYSTEMS.Visible = bShowTCC

            'Body Textboxes
            rtxtTCC_Comments.ReadOnly = False
            rtxtTCC_Comments.BackColor = Color.White

            'The Failure Report body handled seperately from header
            'The purpose of this code is to provide a means to prevent overwriting data, 
            'add the author of the edit, 
            'and provide a means to restore the orginal text if the user chooses not save.

            'Get the current Date
            _CurrentDate = Now

            _myTCC_CommentsBackupLength = rtxtTCC_Comments.TextLength

            'Add user and time stamp
            'Changed all of the vbCRLF to vbLF.  The reason for this is the rixh text box counts the vbCRLF as one character when calculating start position, however, string.length counts it 
            'as two characters...  The LF goes to the next line
            'This is the orginal data in the textbox with the current user and date appended. 
            _myTCC_CommentsBackupEdit = rtxtTCC_Comments.Text + vbLf + "--->" + _CurrentUser.FirstName + " " + _CurrentUser.LastName + "   " + _CurrentDate.ToString + vbLf
            _myTCC_CommentsBackup = _myTCC_CommentsBackupEdit
        Else
            'date time pickers
            dtpDateClosed.Enabled = False
            dtpDateApproved.Enabled = False
            dtpDateClosed.Visible = False
            dtpDateApproved.Visible = False
            dtpDateClosed.Hide()
            dtpDateApproved.Hide()
            'Header Textboxes
            mtxtDateClosed.ReadOnly = True
            mtxtDateApproved.BackColor = _ColorNonEditableBackGround
            mtxtDateApproved.ReadOnly = True
            mtxtDateClosed.BackColor = _ColorNonEditableBackGround
            cbApprovedBy.ReadOnly = True
            cbApprovedBy.BackColor = _ColorNonEditableBackGround

            'header Checkboxes
            'chkShowDelegates.Enabled = False
            chkShowDelegates.AutoCheck = False
            chkShowDelegates.BackColor = _ColorNonEditableBackGround
            chkShowDelegates.Visible = False

            'ChkFR_Approved.Enabled = False
            ChkFR_Approved.AutoCheck = False
            'ChkFR_Approved.BackColor = _ColorNonEditableBackGround
            If ChkFR_Approved.Checked Then
                ChkFR_Approved.BackColor = Color.GreenYellow
                ChkFR_Approved.ForeColor = Color.Black
            Else
                ChkFR_Approved.BackColor = Color.Red
                ChkFR_Approved.ForeColor = Color.White
            End If
            ChkFR_Approved.Visible = True

            'header xboXComboBoxes
            cbTCC_1_Compliance.ReadOnly = True
            cbTCC_2_Engineering.ReadOnly = True
            cbTCC_3_Manufacturing.ReadOnly = True
            cbTCC_4_Product_Management.ReadOnly = True
            cbTCC_5_Quality_Product_Delivery.ReadOnly = True
            cbTCC_6_SYSTEMS.ReadOnly = True

            'always hide when disabled
            cbTCC_1_Compliance.Visible = False
            cbTCC_2_Engineering.Visible = False
            cbTCC_3_Manufacturing.Visible = False
            cbTCC_4_Product_Management.Visible = False
            cbTCC_5_Quality_Product_Delivery.Visible = False
            cbTCC_6_SYSTEMS.Visible = False


            'Body Textboxes
            rtxtTCC_Comments.ReadOnly = True
            rtxtTCC_Comments.BackColor = _ColorNonEditableBackGround

        End If
    End Sub
    ''' <summary>
    ''' Enable/Disable Menu Controls in Edit Mode...
    ''' </summary>
    ''' <param name="bEditEnabled"></param>
    ''' <remarks></remarks>
    Public Sub Enable_EditMode_tsmFileMenu(ByVal bEditEnabled As Boolean)
        If bEditEnabled Then
            'force into edit view...
            'tsmViewFR_View.PerformClick()
            SetFR_VIEW(eFailureReportView.FR_VIEW)

            'disable  menu commnads when in an edit mode
            tsmFilter.Enabled = False
            tsmView.Enabled = False
            tsmOptions.Enabled = False
            tsmFileNew.Enabled = False
            tsmFileLoginAsDifferentUser.Enabled = False
            tsmAdmin.Enabled = False
            tsmFileAdminEdit.Enabled = False
            tsmFileExit.Enabled = False
            tsmFileCopy.Enabled = False
            tsmFileTransfer.Enabled = False

            'enable menu commands when in an edit view.
            tsmFileSaveFR_Data.Enabled = True
            tsmFileSaveAndExitEditMode.Enabled = True
            tsmFileCancel.Enabled = True
            tsmFileDelete.Enabled = True
        Else
            'Enable menu commnads when in an edit mode
            tsmFilter.Enabled = True
            tsmView.Enabled = True
            tsmOptions.Enabled = True
            tsmFileNew.Enabled = True
            tsmFileLoginAsDifferentUser.Enabled = True
            tsmAdmin.Enabled = True
            tsmFileAdminEdit.Enabled = True
            tsmFileExit.Enabled = True
            tsmFileCopy.Enabled = True
            tsmFileTransfer.Enabled = True

            'Disable menu commands when in an edit view.
            tsmFileSaveFR_Data.Enabled = False
            tsmFileSaveAndExitEditMode.Enabled = False
            tsmFileCancel.Enabled = False
            tsmFileDelete.Enabled = False
        End If
    End Sub
    'Public Sub DisplayShadowControl(Optional ByVal bEnabled As Boolean = False)

    '    ''Variable count the number of textboxes to keep track of which controls are enbled and visible
    '    ''so that the user may edit
    '    'Dim TextBoxCount As Integer = 0
    '    ''Dim CheckBoxCount As Integer = 0
    '    ''   Set each control to edit view contained in the Panel Header.
    '    ''   Select based on control type and cast to type to access properties 
    '    ''   Set Read Only to False (For Text boxes)
    '    ''   Set the background to white to que the user that the record is in edit mode
    '    ''   Exchange the Shadow Readonly textboxes with the Combo, Date Time picker, or editable
    '    ''   Checkbox so the user can now edit the record
    '    'If Not bEnabled Then
    '    '    'PANEL_HEADER_CONTROLS:

    '    '    For Each i As Control In pnlReportHeader.Controls
    '    '        'Set the Textboxes state
    '    '        If TypeOf i Is TextBox Then
    '    '            Dim MyTextBox As TextBox = DirectCast(i, TextBox)
    '    '            If MyTextBox.ReadOnly = False Then
    '    '                MyTextBox.BackColor = System.Drawing.Color.White
    '    '            Else
    '    '                MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '    '            End If

    '    '        End If

    '    '        If TypeOf i Is RichTextBox Then
    '    '            Dim MyTextBox As RichTextBox = DirectCast(i, RichTextBox)
    '    '            If MyTextBox.ReadOnly = False Then
    '    '                MyTextBox.BackColor = System.Drawing.Color.White
    '    '            Else
    '    '                MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '    '            End If
    '    '        End If

    '    '        'Added 1.27.2017 to Bind Date Time..FJB
    '    '        If TypeOf i Is MaskedTextBox Then
    '    '            Dim MyTextBox As MaskedTextBox = DirectCast(i, MaskedTextBox)
    '    '            If MyTextBox.ReadOnly = False Then
    '    '                MyTextBox.BackColor = System.Drawing.Color.White
    '    '            Else
    '    '                MyTextBox.BackColor = System.Drawing.Color.LightYellow
    '    '            End If
    '    '        End If

    '    '        ''Replace with editable xboXComboBox
    '    '        'If TypeOf i Is xboXComboBox Then
    '    '        '    If i.Name = cbAccesslevel.Name Or i.Name = cbEditState.Name Then
    '    '        '        i.Hide()
    '    '        '        _myShadowTextBox(TextBoxCount).Visible = False
    '    '        '        _myShadowTextBox(TextBoxCount).Hide()
    '    '        '    ElseIf i.Enabled = True Then
    '    '        '        i.Show()
    '    '        '        i.Visible = True '_myShadowTextBox(TextBoxCount).Visible
    '    '        '        v_UpdateLog("Showing Control: " + i.Name.ToString + vbTab + Now.ToString, eLogLevel.ERROR_AND_INFORMATION)
    '    '        '        _myShadowTextboxIndex(TextBoxCount).VisibleState = _myShadowTextBox(TextBoxCount).Visible
    '    '        '        _myShadowTextBox(TextBoxCount).Hide()
    '    '        '    Else
    '    '        '        i.Hide()
    '    '        '        '_myShadowTextBox(TextBoxCount).Size = i.Size
    '    '        '        '_myShadowTextBox(TextBoxCount).Location = i.Location
    '    '        '        _myShadowTextBox(TextBoxCount).Text = i.Text
    '    '        '        _myShadowTextBox(TextBoxCount).ReadOnly = True

    '    '        '        'If _myShadowTextboxIndex(TextBoxCount).VisibleState = True Then
    '    '        '        _myShadowTextBox(TextBoxCount).Visible = True
    '    '        '        _myShadowTextBox(TextBoxCount).Show()
    '    '        '        'If

    '    '        '        _myShadowTextBox(TextBoxCount).BackColor = _ColorNonEditableBackGround
    '    '        '        'TextBoxCount += 1
    '    '        '    End If

    '    '        '    TextBoxCount += 1

    '    '        'End If

    '    '        'removed 1.10.2018 Functionality refactored to individual control Enable/Disable code
    '    '        ''1.27.2017 Show the  Date TIme Picker drop down arrow in edit mode... 
    '    '        'If TypeOf i Is DateTimePicker And i.Enabled = True Then
    '    '        '    i.Show()
    '    '        'End If

    '    '        'Remove Checkbox Shadow Controls
    '    '        ''Replace non-editable Checkbox with editable checkbox
    '    '        'If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '    '        '    If i.Enabled = True Then
    '    '        '        i.Show()
    '    '        '        _myShadowCheckBox(CheckBoxCount).Hide()
    '    '        '        CheckBoxCount += 1
    '    '        '        i.BackColor = Color.White
    '    '        '    End If
    '    '        'End If


    '    '    Next



    '    'Else 'Edit Enabled ...


    '    '    'Replace editable controls with non-editable shadow controls
    '    '    'find and replace each Combo, Timepicker.  Make each existing textbox read only

    '    '    Dim counter As Integer = 1
    '    '    For Each i As Control In pnlReportHeader.Controls

    '    '        If TypeOf i Is TextBox Then
    '    '            DirectCast(i, TextBox).ReadOnly = True
    '    '            i.BackColor = _ColorNonEditableBackGround
    '    '            v_UpdateLog(counter.ToString + ": " + i.Name, eLogLevel.ERROR_AND_INFORMATION)
    '    '            counter = counter + 1
    '    '            i.Visible = True
    '    '            If i.Name = "ShadowText" + cbAccesslevel.Name Or i.Name = "ShadowText" + cbEditState.Name Then
    '    '                i.Visible = False
    '    '                i.Hide()
    '    '            End If
    '    '        End If

    '    '        If TypeOf i Is RichTextBox Then
    '    '            DirectCast(i, RichTextBox).ReadOnly = True
    '    '            i.BackColor = _ColorNonEditableBackGround
    '    '            v_UpdateLog(counter.ToString + ": " + i.Name, eLogLevel.ERROR_AND_INFORMATION)
    '    '            counter = counter + 1
    '    '            i.Visible = True
    '    '        End If
    '    '        'added 1.27.2017, to Bind Date to FR DB ...FJB
    '    '        If TypeOf i Is MaskedTextBox Then
    '    '            DirectCast(i, MaskedTextBox).ReadOnly = True
    '    '            i.BackColor = _ColorNonEditableBackGround
    '    '            v_UpdateLog(counter.ToString + ": " + i.Name, eLogLevel.ERROR_AND_INFORMATION)
    '    '            counter = counter + 1
    '    '            i.Visible = True
    '    '        End If

    '    '        'If TypeOf i Is xboXComboBox Then
    '    '        '    i.Hide()
    '    '        '    If i.Name = cbAccesslevel.Name Or i.Name = cbEditState.Name Then
    '    '        '        i.Hide()
    '    '        '        _myShadowTextBox(TextBoxCount).Visible = False
    '    '        '        _myShadowTextBox(TextBoxCount).Hide()
    '    '        '        Dim MyName As String = _myShadowTextBox(TextBoxCount).Name
    '    '        '        Dim myStop As Integer = 1
    '    '        '    Else
    '    '        '        '_myShadowTextBox(TextBoxCount).Size = i.Size
    '    '        '        '_myShadowTextBox(TextBoxCount).Location = i.Location
    '    '        '        _myShadowTextBox(TextBoxCount).Text = i.Text
    '    '        '        _myShadowTextBox(TextBoxCount).ReadOnly = True

    '    '        '        If _myShadowTextboxIndex(TextBoxCount).VisibleState = True Then
    '    '        '            _myShadowTextBox(TextBoxCount).Show()
    '    '        '        End If

    '    '        '        _myShadowTextBox(TextBoxCount).BackColor = _ColorNonEditableBackGround

    '    '        '    End If
    '    '        '    TextBoxCount += 1
    '    '        '    v_UpdateLog(counter.ToString + ": " + i.Name, eLogLevel.ERROR_AND_INFORMATION)
    '    '        '    counter = counter + 1
    '    '        'End If
    '    '        'removed 1.10.2018 Functionality refactored to individual control Enable/Disable code
    '    '        ''Hide the Datepicker  Drop down button if not in an edit mode 
    '    '        'If TypeOf i Is DateTimePicker Then
    '    '        '    i.Hide()
    '    '        'End If

    '    '        ''Find checkbox, and skip shadow checkbox...
    '    '        'If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '    '        '    i.Hide()
    '    '        '    _myShadowCheckBox(CheckBoxCount).Show()
    '    '        '    _myShadowCheckBox(CheckBoxCount).BackColor = _ColorNonEditableBackGround
    '    '        '    CheckBoxCount += 1
    '    '        '    v_UpdateLog(counter.ToString + ": " + i.Name, eLogLevel.ERROR_AND_INFORMATION)
    '    '        '    counter = counter + 1
    '    '        'ElseIf TypeOf i Is CheckBox Then
    '    '        '    ' MsgBox(i.Name)
    '    '        'End If



    '    '    Next
    '    'End If

    'End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="cevent"></param>
    ''' <remarks></remarks>
    Public Sub FormatShortDate(ByVal sender As Object, ByVal cevent As ConvertEventArgs)
        If cevent.DesiredType IsNot GetType(String) Then
            Exit Sub
        End If

        Try
            Dim MyMaskedTextBox As MaskedTextBox = DirectCast(sender, MaskedTextBox)
            If MyMaskedTextBox.Text.Trim = "" Then
                Return
            End If
        Catch ex As Exception

        End Try
        Try
            cevent.Value = CType(cevent.Value, Date).ToString("d")
        Catch ex As Exception
            v_UpdateLog("Error Formating Date" + vbCrLf + ex.ToString, eLogLevel.ERRORS_ONLY)
        End Try


    End Sub



#Region "REFRESH"
    ''' <summary>
    '''   Makes a call to refresh the data bound to the Failure Report DataGridView Control from the source database
    ''' <param name="RemoveFilter">Set to True (Default) to remove any filters being used to sort the data</param> 
    ''' </summary>
    ''' <remarks>Frank Boudreau 2017</remarks>
    Public Overloads Sub RefreshFailureReportDataBase(Optional ByVal RemoveFilter As Boolean = True)
        If gb_ProcessEvents = True And _EditState = eEditState.READ_ONLY Then
            Try
                Cursor = Cursors.AppStarting
                'inhibit events...
                gb_ProcessEvents = False
                'hide the grid, speeds update and appears smoother...
                dgvFailureReportDataGridView.Hide()
                'must clear datatable first
                gMyFailureReportDataTable.Clear()
                'now re-fill the table using the table adaptor
                gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)
                If RemoveFilter Then
                    'remove the any filter from the binding source
                    gMyFailureReportBindingSource.RemoveFilter()
                End If
                'Refresh the datagridview
                dgvFailureReportDataGridView.Refresh()
                'show grid...
                dgvFailureReportDataGridView.Show()
                're-enable events
                gb_ProcessEvents = True
                'restore the cursor
                Cursor = Cursors.Default
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub








    ''' <summary>
    ''' This function refreshs the database
    ''' </summary>
    ''' <param name="Index">Index is the unique ID of the record to show after refreshing</param>
    ''' <param name="RemoveFilter">True removes any Filters, False Leave any filters intact...</param>
    ''' <remarks>Frank Boudreau 2019</remarks>
    Public Overloads Sub RefreshFailureReportDataBase(ByVal Index As String, Optional ByVal RemoveFilter As Boolean = True)
        If gb_ProcessEvents = True And _EditState = eEditState.READ_ONLY Then
            Try
                Cursor = Cursors.AppStarting
                'inhibit events...
                gb_ProcessEvents = False
                'hide the grid, speeds update and appears smoother...
                dgvFailureReportDataGridView.Hide()
                'must clear datatable first
                gMyFailureReportDataTable.Clear()
                'now re-fill the table using the table adaptor
                gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)
                'backup the filter to restore if needed
                ' Dim MyBackUpFilter As String = ""
                If RemoveFilter Then
                    'remove the any filter from the binding source
                    gMyFailureReportBindingSource.RemoveFilter()
                    ' MyBackUpFilter = gMyFailureReportBindingSource.Filter
                End If

                Try
                    gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", Index)
                Catch ex As Exception

                End Try
                'Refresh the datagridview
                dgvFailureReportDataGridView.Refresh()
                'show grid...
                dgvFailureReportDataGridView.Show()
                're-enable events
                gb_ProcessEvents = True
                'restore the cursor
                Cursor = Cursors.Default
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
    Private Sub Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        RefreshFailureReportDataBase()

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshFailureReportDataBase()
    End Sub

    Private Sub Refresh_MaskedTextbox_Text(ByVal MaskedTextBox As MaskedTextBox)
        Dim TempText As String = MaskedTextBox.Text
        MaskedTextBox.Clear()
        MaskedTextBox.Text = TempText
    End Sub
    'Private Sub btnResizeForm_Click(sender As System.Object, e As System.EventArgs) Handles btnResizeForm.Click
    '    ResizeControl(Me)
    'End Sub
#End Region '"REFRESH"

#End Region '"Control State"
#Region "DATABASE CONNECTION"


    ''' <summary>
    ''' This subroutine Creates and initialized a connection to the MeterSpec Database. It identifies each of the 
    ''' User Tables and stores and fills a globaldata set with the current values.
    ''' </summary>
    ''' <returns>0 for Success else, type of Database connection of Error(s)</returns>
    ''' <remarks>Frank Boudreau Updated 2017</remarks>
    Public Function MeterSpecDatabaseLoad() As eDataBaseErrors

        'Variable to hold a list of the Tables in the MeterSpec Database
        Dim MyListOfTableNames As New List(Of String)
        Dim Retries As Integer = 0
        Dim Status As eDataBaseErrors = eDataBaseErrors.NO_ERROR
CONNECT:
        While Retries < 3 'Make three attempts
            Try
                'gMyMeterSpecDBConnection = New SqlDbConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
                gMyMeterSpecDBConnection = New SqlConnection With {.ConnectionString = My.Settings.MeterSpecDataBaseFullConnectionString} '"Data Source=USLAFNB365\SQLEXPRESS;Initial Catalog=METER_SPECS;Integrated Security=True"} '(My.Settings.MeterSpecDataBaseFullConnectionString)
                Dim MyString As String = gMyMeterSpecDBConnection.ConnectionString
                ' MsgBox(MyString)
                Exit While
            Catch ex As Exception
                v_UpdateLog("Unable to connect to MeterSpec Database" + vbCrLf + ex.ToString)
                'frmSelectDatabase.ShowDialog()
                Retries += 1
                If Retries = 3 Then
                    Status = Status = Status Or eDataBaseErrors.CONNECTION_FAILED
                    If MsgBox("Unable to connect to Meter Spec Database.  Do you want to Exit the Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Status = Status = Status Or eDataBaseErrors.EXIT_PROGRAM
                        Return Status
                    Else
                        Return Status
                    End If
                End If
            End Try
        End While


OPEN:
        If Status = eDataBaseErrors.NO_ERROR Then


            Try

                If gMyMeterSpecDBConnection.State = ConnectionState.Closed Then
                    gMyMeterSpecDBConnection.Open()
                End If

            Catch ex As Exception
                Status = Status = Status Or eDataBaseErrors.OPEN_FAILED
                v_UpdateLog("Unable to Open the Meter Spec Database" + vbCrLf + ex.ToString)
                If MsgBox("Unable to Open Meter Spec Database.  Do you want to Exit The Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Status = Status = Status Or eDataBaseErrors.EXIT_PROGRAM
                End If

            End Try
        End If
GET_SCHEMA:

        If Status = eDataBaseErrors.NO_ERROR Then
            'Get the Schema for the user tables
            'When "Table" is added a restiction in Table Type, only users tables are returned
            Dim straRestrictions() As String = New String(3) {}
            straRestrictions(3) = "Table"

            Try
                gMyMeterSpecDataTableSchema = gMyMeterSpecDBConnection.GetSchema("Tables", straRestrictions)
            Catch ex As Exception
                Status = Status = Status Or eDataBaseErrors.FAILED_TO_RETRIEVE_SCHEMA
                v_UpdateLog("Failed to Retrieve Meter Spec Database Schema" + vbCrLf + ex.ToString)
                If MsgBox("Failed to Retrieve Meter Spec Database Schema.  Do you want to Exit The Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Status = Status = Status Or eDataBaseErrors.EXIT_PROGRAM
                End If
            End Try
        End If

POPULATE_TABLES_IN_DATA_SET:

        If Status = eDataBaseErrors.NO_ERROR Then
            'get the names of each table
            Dim i As Integer
            'command String to get table names
            Dim CommandString As String = "SELECT * FROM "

            'Set a side memory to hold the the tables and data in a dataset...
            gMyMeterSpecDbDataSet = New DataSet
            Try
                For i = 0 To gMyMeterSpecDataTableSchema.Rows.Count - 1 'Step i + 1
                    v_UpdateLog("TABLE " + i.ToString + ": " + gMyMeterSpecDataTableSchema.Rows(i)(2).ToString(), eLogLevel.MAX)
                    MyListOfTableNames.Add(New String(gMyMeterSpecDataTableSchema.Rows(i)(2).ToString()))
                Next

                For i = 0 To MyListOfTableNames.Count - 1
                    'Initialize the Data-adaptor and add Fill the Dataadaptor
                    gMyMeterSpecDBDataAdaptor = New SqlDataAdapter(CommandString + "[" + gMyMeterSpecDataTableSchema.Rows(i)(2).ToString() + "]", gMyMeterSpecDBConnection)
                    'Add the Table to the Dataset
                    gMyMeterSpecDBDataAdaptor.Fill(gMyMeterSpecDbDataSet, gMyMeterSpecDataTableSchema.Rows(i)(2).ToString())
                Next

            Catch ex As Exception
                Status = Status = Status Or eDataBaseErrors.ERROR_POPULATING_DATASET_OR_DATATABLE
                v_UpdateLog("Failed to Populate Tables in DataSet" + vbCrLf + ex.ToString)
                If MsgBox("Failed to Populate Tables in DataSet.  Do you want to Exit The Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Status = Status = Status Or eDataBaseErrors.EXIT_PROGRAM
                End If

            End Try
        End If

CLOSE_DB:

        'Now close the connection
        Try
            If gMyMeterSpecDBConnection.State = ConnectionState.Open Then
                gMyMeterSpecDBConnection.Close()
            End If

        Catch ex As Exception
            Status = Status Or eDataBaseErrors.CLOSE_FAILED
            v_UpdateLog("Failed to Close Connection to Meterspec Database during init." + vbCrLf + ex.ToString)
            If MsgBox("Failed to Close Connection to Meterspec Database during init.  Do you want to Exit The Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Status = Status = Status Or eDataBaseErrors.EXIT_PROGRAM
            End If
        End Try

RETURN_STATUS:
        'Variable so that the program can be halted here...
        Dim MyStop As Integer = 1

        Return Status
    End Function
    Public Function FailureReportDatabaseLoad() As Integer
        'Connect to each database
        'Connect to MeterSpec....
        Dim Status As eDataBaseErrors = eDataBaseErrors.NO_ERROR ' assume success
        Dim PackedStatus As Integer = 0

        Try

            ' App Starting cursor
            Cursor = Cursors.AppStarting

            Status = MeterSpecDatabaseLoad()
            If Status <> eDataBaseErrors.NO_ERROR Then
                If Status And eDataBaseErrors.EXIT_PROGRAM <> 0 Then
                    Return Status
                Else
                    PackedStatus = Status << 16 'leftshift into upper word
                End If
            End If
CONNECT:
            Dim Retries As Integer = 0

            While Retries < 3 'Make three attempts
                Try
                    'On form load instantiate the connection object
                    'gMyFailureReportDBConnection = New OleDb.OleDbConnection(My.Settings.FailureReportDataBaseFullConnectionString)
                    gMyFailureReportDBConnection = New SqlConnection With {.ConnectionString = My.Settings.FailureReportDataBaseFullConnectionString} '"Data Source=USLAFNB365\SQLEXPRESS;Initial Catalog=FAILURE_REPORT;Integrated Security=True"}
                    Dim MyString As String = gMyFailureReportDBConnection.ConnectionString

                    Exit While
                Catch ex As Exception

                    Retries += 1
                    If Retries = 3 Then
                        'update Status
                        Status = Status Or eDataBaseErrors.CONNECTION_FAILED
                        If MsgBox("Error Connecting to FR Database, Would you like to exit the Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Return Status
                        Else
                            Exit While
                        End If
                    End If
                End Try
            End While

GET_DATA:

            Try
                'Initialize the Data-adaptor
                gMyFailureReportDBDataAdaptor = New SqlDataAdapter(_FR_SQL_Commands, gMyFailureReportDBConnection)

                'Iniialize the Datatable to hold the data
                gMyFailureReportDataTable = New DataTable

                Try
                    gMyFailureReportDataTable.Clear()
                Catch ex As Exception

                End Try

                'Fill the DataTable  
                gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)


            Catch ex As Exception
                Status = Status Or eDataBaseErrors.ERROR_POPULATING_DATASET_OR_DATATABLE
                If MsgBox("Error Filling FR DataTable, Would you like to exit the Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Return Status
                End If
            End Try


BINDING:
            Try


                'Init the Binding source 
                gMyFailureReportBindingSource = New BindingSource

                'Set the Datasource for the Binding Source 
                gMyFailureReportBindingSource.DataSource = gMyFailureReportDataTable

                'Add the Binding source to the dataGridview
                dgvFailureReportDataGridView.DataSource = gMyFailureReportBindingSource

                'create automatic Queries
                'Create the commandbuilder object
                Dim DbCommandBuilder As SqlCommandBuilder = New SqlCommandBuilder(gMyFailureReportDBDataAdaptor)

                'This is necessary since some of the column names have spaces in them
                DbCommandBuilder.QuotePrefix = "["
                DbCommandBuilder.QuoteSuffix = "]"

                'Create Update, Delete, and Insert commands
                gMyFailureReportDBDataAdaptor.UpdateCommand = DbCommandBuilder.GetUpdateCommand
                gMyFailureReportDBDataAdaptor.DeleteCommand = DbCommandBuilder.GetDeleteCommand
                gMyFailureReportDBDataAdaptor.InsertCommand = DbCommandBuilder.GetInsertCommand

                'Use the custom bindingtracket class to create databindings for all of the controls
                'init Binding Record
                gFailureReportBindingRecord = New List(Of cCustomDataBaseAccess.cDataBindingTracker)
                gFailureReportBindingRecord.Clear()

                'Que up the Bindings for the controls here 

                'MainForm:
                '                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(Me, "Text", "New ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
Textboxes:
                'textboxes
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtReportNumber, "Text", "New ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                'gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProjectLead, "Text", "Assigned To", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                'Binding to text box instead...
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtTest, "Text", "Test", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtTCC_Comments, "Text", "TCC Comments", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtEngineeringNotes, "Text", "Engineering Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtMeterNotes, "Text", "Meter_Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtAMR_Notes, "Text", "AMR_Notes", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                'gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbProjectNumber, "Text", "Project_Number", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtProject, "Text", "Project", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtAttachments, "Text", "Attachments", Me.gMyFailureReportBindingSource, Me.gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtCorrectiveAction, "Text", "Corrective Action", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtDescription, "Text", "Failure Description", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(rtxtTestEquipmentIDlist, "Text", "Test_Equipment_ID", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                '2.27.2025 Adding Orginal Project Number (Project_Number_Org).  This Field is to the store the orignal Project number in the event that a Failure Report is transfered to
                'to a different Project.
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtOrginalProjectNum, "Text", "Original_Project_Num", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                '9/3/2025 Adding Orginal Report Number (Orginal_Report_Num).  This Field is to the store the orignal Reprot number in the event that a Failure Report is transfered to
                'to a different Project.
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(txtOrginalReportNum, "Text", "Original_Report_Num", gMyFailureReportBindingSource, gMyFailureReportDBBinding))


                'Added 1.27.2017 to solve interface issue.  Nolonger directly binging DTPickeers to FR database...using a masked textbox 
                '                                           allows user to directly enter date and to have an empty date field.  DTP does 
                '                                           not allow empty value. 
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateFailed, "Text", "Date Failed", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateCorrected, "Text", "Date Corrected", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateClosed, "Text", "Date Closed", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateApproved, "Text", "Date Approved", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                'Added 12.9.2022
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(mtxtDateFailedSampleReady, "Text", "Failed_Sample_Ready_Date", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

xboXComboBoxes:

                'xboXComboBoxes
cbMeterData:    '*****Meter Data*******
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


                'add validating for checking textdata vs items whencontrol loses focus
                AddHandler cbMeterModel.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterManufacturer.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterSerialNumber.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterType.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterSubType.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterSubTypeII.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterDSP_Rev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterPCBA.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterPCBA_Rev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterSoftware.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterSoftwareRev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterFW_Ver.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterForm.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterBase.Leave, AddressOf cbComboBox_Leave
                AddHandler cbMeterVoltage.Leave, AddressOf cbComboBox_Leave



cbAMR_Data:     '*****AMR Data******
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

                'add validating for checking textdata vs items when control loses focus
                AddHandler cbAMR_Model.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_FW_Rev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_Manufacturer.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_SerialNumber.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_TYPE.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_SubType.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_SUBtypeII.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_SUBtypeIII.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_PCBA.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_PCBA_Rev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_Software.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_Software_Rev.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_IP_LAN_ID.Leave, AddressOf cbComboBox_Leave
                AddHandler cbAMR_Voltage.Leave, AddressOf cbComboBox_Leave

cbTestData:

                ' gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestName, "Text", "Test", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestType, "Text", "Test_Type", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestedBy, "Text", "Tested By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTestLevel, "Text", "Level", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbEUTType, "Text", "EUT_TYPE", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbProjectLead, "Text", "Assigned To", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbProjectNumber, "Text", "Project_Number", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                'add validating for checking textdata vs items whencontrol loses focus
                ' AddHandler cbTestName.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTestType.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTestedBy.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTestLevel.Leave, AddressOf cbComboBox_Leave
                AddHandler cbEUTType.Leave, AddressOf cbComboBox_Leave
                AddHandler cbProjectLead.Leave, AddressOf cbComboBox_Leave
                AddHandler cbProjectNumber.Leave, AddressOf cbComboBox_Leave

cbCorrectiveAction:
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbCorrectedBy, "Text", "Corrected By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbApprovedBy, "Text", "Approved By", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                AddHandler cbCorrectedBy.Leave, AddressOf cbComboBox_Leave
                AddHandler cbApprovedBy.Leave, AddressOf cbComboBox_Leave
cbTCCData:
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_1_Compliance, "Text", "TCC 1", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_2_Engineering, "Text", "TCC 2", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_3_Manufacturing, "Text", "TCC 3", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_4_Product_Management, "Text", "TCC 4", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_5_Quality_Product_Delivery, "Text", "TCC 5", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(cbTCC_6_SYSTEMS, "Text", "TCC 6", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                'add validating for checking textdata vs items whencontrol loses focus
                AddHandler cbTCC_1_Compliance.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTCC_2_Engineering.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTCC_3_Manufacturing.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTCC_4_Product_Management.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTCC_5_Quality_Product_Delivery.Leave, AddressOf cbComboBox_Leave
                AddHandler cbTCC_6_SYSTEMS.Leave, AddressOf cbComboBox_Leave


                'checkboxes
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkAnomely, "CheckState", "Anomaly", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkFR_ReadyForReview, "CheckState", "FR_Ready_For_Review", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkTCC_ApprovalRequired, "CheckState", "TCC_REview_Required", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(ChkFR_Approved, "CheckState", "FR_Approved", gMyFailureReportBindingSource, gMyFailureReportDBBinding))
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkPass, "CheckState", "Pass", gMyFailureReportBindingSource, gMyFailureReportDBBinding))

                'added 12.9.2022
                gFailureReportBindingRecord.Add(New cCustomDataBaseAccess.cDataBindingTracker(chkFailedSampleReady, "CheckState", "Failed_Sample_Ready", gMyFailureReportBindingSource, gMyFailureReportDBBinding))


                'Excute the bindings to the database....


                'Call the function the creates the bindings
                gMyCustomDBAccess.BindControls(gFailureReportBindingRecord)

                ''***********************************************************************************************'
                ' '''''''''''''''''Create Additonal Bindings in order to force date to display as short''''''''''''
                ''***********************************************************************************************'
                'Dim DateFailedFormatBinding As Binding = DirectCast(mtxtDateFailed.DataBindings("Text"), Binding)
                'AddHandler DateFailedFormatBinding.Format, AddressOf FormatShortDate

                'Dim DateCorrectedFormatBinding As Binding = DirectCast(mtxtDateCorrected.DataBindings("Text"), Binding)
                'AddHandler DateCorrectedFormatBinding.Format, AddressOf FormatShortDate

                'Dim DateClosedFormatBinding As Binding = DirectCast(mtxtDateClosed.DataBindings("Text"), Binding)
                'AddHandler DateClosedFormatBinding.Format, AddressOf FormatShortDate

                'Dim DateApprovedFormatBinding As Binding = DirectCast(mtxtDateApproved.DataBindings("Text"), Binding)
                'AddHandler DateApprovedFormatBinding.Format, AddressOf FormatShortDate


            Catch ex As Exception
                Status = Status Or eDataBaseErrors.ERROR_POPULATING_DATASET_OR_DATATABLE
                If MsgBox("Error Binding Database to Controls, Would you like to exit the Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Return Status
                End If
            End Try


            '************************************************************************
            'All of the controls should now be bound to their repective data objects


            '************************************************************************
            'Moved the Login to earlier in the load procedure, however this check
            'cannot happen until after attaching to the database.
            '************************************************************************
            ''Get the login infromation
            If _CurrentUser.AccessLevel = eAccessState.NO_ACCESS Then
                Me.Close()
                Me.Dispose()
            End If
            'set global Flag
            gbUserLoggedOn = True
            Refresh_MaskedTextbox_Text(mtxtDateFailed)

            'hide wait cursor
            Cursor = Cursors.Default
        Catch ex As Exception
            Status = Status Or eDataBaseErrors.UNKNOWN_ERROR
            If MsgBox("Error Binding Database to Controls, Would you like to exit the Program?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Return Status
            End If
        End Try
        Return Status
    End Function
    ''' <summary>
    ''' This function will commit changes to the database 
    ''' I do not think it works ars orginally intended since, the controls are now nested in many different containers...it does not appear to do anything...
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndEditOnAllBindingSources()
        Try
            Dim BindingSourcesQuery = From bindingsources In Me.components.Components
                          Where (TypeOf bindingsources Is Windows.Forms.BindingSource)
                          Select bindingsources

            For Each bindingSource As Windows.Forms.BindingSource In BindingSourcesQuery
                bindingSource.EndEdit()
            Next
        Catch
        End Try

    End Sub

    ' ''' <summary>
    ' ''' This Routine is designed to Update Bindings for the controls to their shadowcontrols in the Failure Report GUI.  
    ' ''' That have been created in design view are the default for creating new Failure Reports and Editing existing Failure reports
    ' ''' Textboxes are created to "Shadow" xboXComboBoxs and Date Time pickers and are bound (Databinding) to the text displayed in
    ' ''' each control. This allows the user when "Viewing" to select and copy, however not to edit the data. A textbox has a "ReadOnly"
    ' ''' poperty that can be set to true to allow this. The other controls do not.  In addtion shadow textbox is also created to show
    ' ''' the states of Boolean values and not allow the user to edit the value.  Finally the FailureReport drid view is also set to readonly.
    ' ''' This is allows th euser to navigate and select via the grid but, only allows editing the database through the GUI fields.
    ' ''' </summary>
    ' ''' <remarks>Commented out 1.9.2017 as refactoring to remove Shadow Control Archtecture</remarks>
    ' ''' 
    'Private Sub UpdatebindingsShadowControls()
    '    ''dynamically determine how many shadow textboxes are needed to replace the editable controls
    '    'Dim HowManyTextBoxDoINeed As Integer = 0
    '    'Dim HowManyCheckBoxDoINeed As Integer = 0
    '    ''Editing is not allowed via the gridview however navigation is possible
    '    'dgvFailureReportDataGridView.ReadOnly = True

    '    ''Count the controls To Replace with a textbox to make read only
    '    ''A shadow textbox will be created for each xboXComboBox found,
    '    ''as well as each date picker found on the Report PAnel Header Container
    '    ''For Each i As Control In pnlReportHeader.Controls 'check each control
    '    ''    'If TypeOf i Is xboXComboBox Then
    '    ''    '    HowManyTextBoxDoINeed += 1 'increment count
    '    ''    'End If

    '    ''    ''Do not count shadow controls...
    '    ''    'If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '    ''    '    HowManyCheckBoxDoINeed += 1 'increment count
    '    ''    'End If
    '    ''Next

    '    ''Update Binding for each xboXComboBox
    '    ''Rebind the Editable Checkbox with the shadowcheckbox in the panel header
    '    'Dim iTextBoxCount As Integer = 0
    '    'Dim iCheckBoxCount As Integer = 0

    '    ''Process each control in the panel containter
    '    'For Each i As Control In pnlReportHeader.Controls

    '    '    If TypeOf i Is TextBox Then
    '    '        'do nothing 
    '    '        ' DirectCast(i, TextBox).ReadOnly = True
    '    '        ' i.BackColor = _ColorNonEditableBackGround
    '    '    End If

    '    '    If TypeOf i Is RichTextBox Then
    '    '        'do nothing
    '    '        'DirectCast(i, RichTextBox).ReadOnly = True
    '    '        'i.BackColor = _ColorNonEditableBackGround
    '    '    End If

    '    '    If TypeOf i Is MaskedTextBox Then
    '    '        'do nothing
    '    '        'DirectCast(i, RichTextBox).ReadOnly = True
    '    '        'i.BackColor = _ColorNonEditableBackGround
    '    '    End If


    '    '    If TypeOf i Is xboXComboBox Then
    '    '        'commented out when Readonly check boxes added...1.5.2017 ...FJB
    '    '        '_myShadowTextBox(iTextBoxCount).DataBindings.Clear()
    '    '        '_myShadowTextBox(iTextBoxCount).DataBindings.Add("Text", i, "Text")
    '    '        'iTextBoxCount += 1
    '    '    End If

    '    '    ''do not count shadow controls...
    '    '    'If TypeOf i Is CheckBox And InStr(i.Name.ToLower, "shadow") = 0 Then
    '    '    '    _myShadowCheckBox(iCheckBoxCount).DataBindings.Clear()
    '    '    '    _myShadowCheckBox(iCheckBoxCount).DataBindings.Add("CheckState", i, "CheckState")
    '    '    '    'Try Removing 2.7.2017 - FJB This looks like bug
    '    '    '    '_myShadowCheckBox(HowManyCheckBoxDoINeed).DataBindings.Clear()
    '    '    '    ' _myShadowCheckBox(HowManyCheckBoxDoINeed).DataBindings.Add("CheckState", i, "CheckState")
    '    '    '    iCheckBoxCount += 1
    '    '    'End If

    '    'Next
    'End Sub
#End Region '"DATABASE CONNECTION"
#Region "Main Form Events"
    Delegate Sub SplashShowForm()
    Public Sub v_SplashShowForm()
        If frmSplash.InvokeRequired Then
            Try
                Invoke(New SplashShowForm(AddressOf v_SplashShowForm))
            Catch ex As Exception

            End Try
        Else
            frmSplash.Show()
        End If
    End Sub

    Delegate Sub SplashCloseForm()
    Public Sub v_SplashCloseForm()
        If frmSplash.InvokeRequired Then
            Try
                Invoke(New SplashCloseForm(AddressOf v_SplashCloseForm))
            Catch ex As Exception

            End Try
        Else
            frmSplash.Close()
        End If
    End Sub
    Private Sub frmFailureBrowser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim ItsOkToExit = True

            'If _EditState = eAccessState.CREATE_NEW Or _EditState = eAccessState.CR_EDIT Or _EditState = eAccessState.ADMIN Then
            If _EditState <> eEditState.READ_ONLY Then
                'Get out of Edit mode first
                tsmFileCancel.PerformClick()
            End If

            If _CurrentUser.AccessLevel = eAccessState.NO_ACCESS Then ' exit without Nag window
                Dim GridViewColumnSettings As String

                GridViewColumnSettings = dgvFailureReportDataGridView.Columns(0).Name + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).Visible.ToString + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).DisplayIndex.ToString + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).Width.ToString

                For i = 1 To dgvFailureReportDataGridView.Columns.Count - 1
                    GridViewColumnSettings = GridViewColumnSettings + ";" + dgvFailureReportDataGridView.Columns(i).Name + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).Visible.ToString + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).DisplayIndex.ToString + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).Width.ToString
                Next
                Dim Teststring As String()
                Teststring = GridViewColumnSettings.Split(";")
                Dim MyStop As Integer = 1
                'If MsgBox("Are you sure you want to Exit?)", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                '    e.Cancel = True
                'End If
                My.Settings.GridViewColumnStates = GridViewColumnSettings
            ElseIf _EditState = eEditState.READ_ONLY And MsgBox("Are you sure you want to Exit?)", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim GridViewColumnSettings As String

                GridViewColumnSettings = dgvFailureReportDataGridView.Columns(0).Name + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).Visible.ToString + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).DisplayIndex.ToString + ","
                GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(0).Width.ToString

                For i = 1 To dgvFailureReportDataGridView.Columns.Count - 1
                    GridViewColumnSettings = GridViewColumnSettings + ";" + dgvFailureReportDataGridView.Columns(i).Name + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).Visible.ToString + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).DisplayIndex.ToString + ","
                    GridViewColumnSettings = GridViewColumnSettings + dgvFailureReportDataGridView.Columns(i).Width.ToString
                Next
                Dim Teststring As String()
                Teststring = GridViewColumnSettings.Split(";")
                Dim MyStop As Integer = 1
                'If MsgBox("Are you sure you want to Exit?)", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                '    e.Cancel = True
                'End If
                My.Settings.GridViewColumnStates = GridViewColumnSettings
            Else
                If MsgBox("Unable to update Database and Exit Editmode. Are you sure you want to quit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            End If
        Catch ex As Exception
            MsgBox("An Unknown Error occured while exiting.  Terminating Program")
            End
        End Try


    End Sub
    Private Sub frmFailureBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'First time run?
        Me.StartPosition = FormStartPosition.CenterScreen


        'init custom object class
        gMyCustomDBAccess = New cCustomDataBaseAccess

        _SoftwareVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString
        _SoftwareVersion = Application.ProductVersion.ToString

        'Get the login infromation
        _CurrentUser = New User("", "", "", eAccessState.NO_ACCESS, eApproverDiscipline.NONE)
        frmLogin.ShowDialog()


        If My.Settings.Prompt_To_Change_Database = True And _CurrentUser.AccessLevel <> eAccessState.NO_ACCESS Then
            'If the user access level is NO_ACCESS then they cancled out of the log in.   This prevents a NAG window.
            frmSelectDatabase.ShowDialog()
        End If


        _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS
        If FailureReportDatabaseLoad() <> eDataBaseErrors.NO_ERROR Then
            My.Settings.Prompt_To_Change_Database = True
            MsgBox("Error Intializing Program, Unable to Attach Database.  Exiting Program")
            Me.Close()
            Me.Dispose()
        End If




        ' Me.ReportViewer1.RefreshReport()
    End Sub
    Private Sub frmFailureBrowser_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'TableLayoutPanel1.Hide()
            'SetFR_VIEW(eFailureReportView.FR_VIEW)
            Me.StartPosition = FormStartPosition.CenterScreen
            Cursor = Cursors.AppStarting

            gb_ProcessEvents = False

            'init the Database Schemas
            _dbMeterSpecSchema = New cMeterSpecDBDef
            _dbFRSchema = New cFailureReportDBDef
            _MYSQL_Control = New cSQL_Control

            _dbMeterSpecSchema.init()
            _dbFRSchema.init()

            '***********************Shadow Control Architecture Removed1.10.2018*********************FJB 
            ''Initalize the shadow controls used to swap Readonly and editable controls
            'InitializeShadowControls()
            '***********************Shadow Control Architecture Removed1.10.2018*********************FJB 

            'set backgroundcolor for Failure/Anome;ly Report number...this will never change...
            txtReportNumber.BackColor = _ColorNonEditableBackGround

            'initialize the rowstyle pointers.  These are used to track and set the various form views
            For i = 0 To _HeaderGridAndDetailRowStyle.Length - 1
                _HeaderGridAndDetailRowStyle(i) = New RowStyle
                _BrowserRowStyle(i) = New RowStyle
                _FR_RowStyle(i) = New RowStyle
                _HeaderAndGridRowStyle(i) = New RowStyle
            Next

            'Set up the different browser views
            InitFailureReportFormViews()

            'Set to read only view
            'SetEditState(eAccessState.READ_ONLY)
            StateMachine(eEditState.READ_ONLY, _EditState, _CurrentUser.AccessLevel)

            'added to hold off processing events... 
            gb_ProcessEvents = False
            'now form has been loaded

            'Set the column name text of the Gridview do this First
            dgvFailureReportDataGridView.Columns("New ID").HeaderText = "Report #"
            'Freeze the Failure Report Column
            dgvFailureReportDataGridView.Columns("New ID").Frozen = True
            'Change header text from Level to Test Level
            dgvFailureReportDataGridView.Columns("Level").HeaderText = "Test Level"
            'Change Header text to Test Name
            dgvFailureReportDataGridView.Columns("Test").HeaderText = "Test Name"

            'Remove underscores from column names....
            For Each Mycolumn As DataGridViewColumn In dgvFailureReportDataGridView.Columns
                Mycolumn.HeaderText = Mycolumn.HeaderText.ToString.Replace("_", " ")
            Next

            'Replace column name with user friendly names....
            dgvFailureReportDataGridView.Columns("Meter").HeaderText = "Meter Model"
            'dgvFailureReportDataGridView.Columns("EUT_TYPE").HeaderText = "EUT Type"  'This is used
            dgvFailureReportDataGridView.Columns("Assigned To").HeaderText = "Project Lead"
            dgvFailureReportDataGridView.Columns("FW Ver").HeaderText = "Meter FW Ver"





            'The following Code was added to allow resizing the columns
            dgvFailureReportDataGridView.AutoSize = False
            dgvFailureReportDataGridView.AllowUserToResizeColumns = True
            dgvFailureReportDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
            For i = 0 To dgvFailureReportDataGridView.Columns.Count - 1
                dgvFailureReportDataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                dgvFailureReportDataGridView.Columns(i).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Next

            'Set the Visability and Position of each column in the Grid view based on Settings (Last Valid run)
            Dim GridViewSettings As String
            GridViewSettings = My.Settings.GridViewColumnStates
            Dim GridViewSettingsArray() As String
            GridViewSettingsArray = GridViewSettings.Split(";")

            For j = 0 To GridViewSettingsArray.Count - 1
                Dim ColumnSettings() As String = GridViewSettingsArray(j).Split(",")
                Dim ColumnName As String = ColumnSettings(0)
                Dim Visible As Boolean = False

                'Set Visibility
                If ColumnSettings(1).ToLower = "true" Then
                    Visible = True
                    'never show orginal ID
                    If ColumnName.Trim.ToLower = "index" Or ColumnName.Trim.ToLower = "original id" Then  'This is a Band Aid for now...FJB 
                        Visible = False
                    End If
                End If

                Try
                    'Set Position 
                    dgvFailureReportDataGridView.Columns(ColumnName).Visible = Visible
                    If ColumnSettings.Length = 3 Then
                        dgvFailureReportDataGridView.Columns(ColumnName).DisplayIndex = Val(ColumnSettings(2))
                    End If

                    If ColumnSettings.Length = 4 Then
                        '*****Try parsing the index*******
                        dgvFailureReportDataGridView.Columns(ColumnName).DisplayIndex = Val(ColumnSettings(2))
                        '*****Try Parsing the Index*******

                        dgvFailureReportDataGridView.Columns(ColumnName).Width = Val(ColumnSettings(3))
                    End If
                Catch ex As Exception
                    v_UpdateLog(ex.ToString, eLogLevel.ERRORS_ONLY)
                End Try

            Next
        Catch ex As Exception
            v_MessageBox("Error  Caught during Main Shown Event" + vbCrLf + ex.ToString, eLogLevel.MAX)
        End Try

        Cursor = Cursors.Default
        gb_ProcessEvents = True
        dgvFailureReportDataGridView.Select()
        dgvFailureReportDataGridView.Update()
        tsmAdmin.Visible = False
        tsmAdmin.Enabled = False
        SetFR_VIEW(eFailureReportView.FR_VIEW)



    End Sub
#End Region '"Main Form Events"
#Region "ComboBox Events"

    ''' <summary>
    ''' Function to populate a xboXComboBox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyxboXComboBox">xboXComboBox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Overloads Sub PopulatexboXComboBox(ByRef MyDBConnection As SqlConnection, ByRef MyxboXComboBox As xboXComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyxboXComboBox.Text
        MyDatatable = gMyCustomDBAccess.GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)
        MyxboXComboBox.DataSource = MyDatatable
        MyxboXComboBox.DisplayMember = MycolumnName
        MyxboXComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyxboXComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyxboXComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ''' <summary>
    ''' Function to populate a xboXComboBox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDataTable ">Datatable with data to be added to combo</param>
    ''' <param name="MyxboXComboBox">xboXComboBox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Overloads Sub PopulatexboXComboBox(ByRef MyDataTable As DataTable, ByRef MyxboXComboBox As xboXComboBox, ByVal MycolumnName As String, Optional ByVal MyFilter As String = "")
        Dim MyDistinctLocalDatatable As DataTable
        Dim MyDataView As DataView
        Dim MyLocalTable As DataTable = MyDataTable.Copy
        Dim strCurrentValue = MyxboXComboBox.Text
        'MyDataTable = gMyCustomDBAccess.GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter)

        MyDataView = MyLocalTable.DefaultView

        MyDataView.RowFilter = MyFilter
        MyDistinctLocalDatatable = MyDataView.ToTable(True, MycolumnName)
        MyDistinctLocalDatatable.DefaultView.Sort = MycolumnName + " ASC"
        MyxboXComboBox.DataSource = MyDistinctLocalDatatable
        ' MyxboXComboBox.DisplayMember = MycolumnName
        MyxboXComboBox.DisplayMember = MycolumnName
        MyxboXComboBox.Text = strCurrentValue
        MyDataView.RowFilter = ""
        'MyDataView.Dispose()
        'MyLocalDatatable.Dispose()
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyxboXComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyxboXComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    ''' <summary>
    ''' Function to populate a xboXComboBox by sending a Query to a database and 
    ''' </summary>
    ''' <param name="MyDBConnection">Database Connection</param>
    ''' <param name="MyxboXComboBox">xboXComboBox to populate</param>
    ''' <param name="MycolumnName">Column with the raw data to Query in the database, datatable</param>
    ''' <param name="MyDataTableName">The Datatable within the Database</param>
    ''' <param name="MyFilter">Optional Filter action to perform on the Data i.e "WHERE [FIELD NAME] = 'value', "WHERE [FIELD NAME]LIKE '%value' </param>
    ''' <remarks>Frank Boudreau</remarks>
    Public Sub PopulatexboXComboBoxAddValue(ByRef MyDBConnection As SqlConnection, ByRef MyxboXComboBox As xboXComboBox, ByVal MycolumnName As String, ByVal MyDataTableName As String, ByVal strValueToAdd As String, Optional ByVal MyFilter As String = "")
        Dim MyDatatable As DataTable
        Dim strCurrentValue = MyxboXComboBox.Text
        MyxboXComboBox.DataBindings.Clear()
        If MyxboXComboBox.DataSource IsNot Nothing Then
            MyxboXComboBox.DataSource = Nothing
        End If
        MyxboXComboBox.Items.Clear()
        MyDatatable = gMyCustomDBAccess.GetDistinctData(MyDBConnection, MycolumnName, MyDataTableName, MyFilter).Copy
        MyxboXComboBox.Items.Add(strValueToAdd)
        For Each Row As DataRow In MyDatatable.Rows
            MyxboXComboBox.Items.Add(Row(0))
        Next

        'MyxboXComboBox.DataSource = MyDatatable
        ' MyxboXComboBox.DisplayMember = MycolumnName
        MyxboXComboBox.Text = strCurrentValue
        'Dim MyAutoCompleteSource As New AutoCompleteStringCollection()
        'For Each row In MyDatatable.Rows
        '    MyAutoCompleteSource.Add(Convert.ToString(row(0)))
        'Next
        'MyxboXComboBox.AutoCompleteCustomSource = MyAutoCompleteSource
        'MyxboXComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    Private Sub cbComboBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles cbProjectLead.Leave
        Try
            'This code checks to see if the text matches any item in the combobox...

            Dim MyComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
            'If Not MyComboBox.Items.Contains(MyComboBox.Text.Trim) And MyComboBox.ReadOnly = False Then
            '    If MsgBox("Text does not match a value in the Combo Drop down, Are you sure that you want to proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '        MyComboBox.Focus()
            '        MyComboBox.SelectAll()
            '    End If
            'Else
            '    MyComboBox.Text = MyComboBox.Text.Trim 'Trim leading and lagging spaces --'FJB Removed causing issues...
            'End If
            MyComboBox.Text = MyComboBox.Text.Trim 'Trim leading and lagging spaces
        Catch ex As Exception

        End Try

    End Sub

#Region "AMR Combo-boxes"
    Public Class cAMRComboValues
        Public Manufacturer As String = ""
        Public Model As String = ""
        Public Type As String = ""
        Public SubType As String = ""
        Public SubTypeII As String = ""
        Public SubTypeIII As String = ""
        Public FW_REV As String = ""
        Public PCBA As String = ""
        Public PCBA_REV As String = ""
        Public Voltage As String = ""
        Public Software As String = ""
        Public Software_Rev As String = ""
        Public IP_LAN_ID As String = ""
        Public SerialNum As String = ""

    End Class

    Public _MyAMRComboValues As New cAMRComboValues

    Public Class cMeterComboValues
        Public Manufacturer As String = ""
        Public Model As String = ""
        Public Type As String = ""
        Public SubType As String = ""
        Public SubTypeII As String = ""
        Public SerialNum As String = ""
        Public FORM As String = ""
        Public BASE As String = ""
        Public DSP_REV As String = ""
        Public FW_REV As String = ""
        Public PCBA As String = ""
        Public PCBA_REV As String = ""
        Public Software As String = ""
        Public Software_Rev As String = ""
        Public Voltage As String = ""
    End Class

    Public _MyMeterComboValues As New cMeterComboValues

    Private Sub cbAMR_Rev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_FW_Rev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)



        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If

    End Sub
    Private Sub cbAMR_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_Model.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'AMR'
        Dim strFilter As String = ""
        If cbAMR_Manufacturer.Text.Trim <> "" Then
            strFilter = " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        End If

        'If cbAMR_Model.Text.Trim <> "" Then
        '    strFilter = " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        'End If

        If cbAMR_TYPE.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        End If

        If cbAMR_SubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeIII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        End If


        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)


    End Sub
    Private Sub cbAMR_Manufacturer_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_Manufacturer.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'AMR'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        Dim strFilter As String = ""

        'If cbAMR_Manufacturer.Text.Trim <> "" Then
        '    strFilter = " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        'End If

        If cbAMR_Model.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        End If

        If cbAMR_TYPE.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        End If

        If cbAMR_SubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeIII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        End If

        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub


    Private Sub cbAMR_SerialNumber_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_SerialNumber.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If

    End Sub
    Private Sub cbAMR_TYPE_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_TYPE.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Failure Report'
        Dim strFilter As String = ""

        If cbAMR_Manufacturer.Text.Trim <> "" Then
            strFilter = " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        End If

        If cbAMR_Model.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        End If

        'If cbAMR_TYPE.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        'End If

        If cbAMR_SubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeIII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        End If

        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub
    Private Sub cbAMR_SubType_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_SubType.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Failure Report'
        Dim strFilter As String = ""

        If cbAMR_Manufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        End If

        If cbAMR_Model.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        End If

        If cbAMR_TYPE.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        End If

        'If cbAMR_SubType.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        'End If

        If cbAMR_SUBtypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeIII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        End If

        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub
    Private Sub cbAMR_SUBtypeII_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_SUBtypeII.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Failure Report'
        Dim strFilter As String = ""
        If cbAMR_Manufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        End If

        If cbAMR_Model.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        End If

        If cbAMR_TYPE.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        End If

        If cbAMR_SubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        End If

        'If cbAMR_SUBtypeII.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        'End If

        If cbAMR_SUBtypeIII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        End If

        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub
    Private Sub cbAMR_SUBtypeIII_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_SUBtypeIII.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Failure Report'
        Dim strFilter As String = ""

        If cbAMR_Manufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_Manufacturer] = '" + cbAMR_Manufacturer.Text.Trim + "'"
        End If

        If cbAMR_Model.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR] = '" + cbAMR_Model.Text.Trim + "'"
        End If

        If cbAMR_TYPE.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_TYPE] = '" + cbAMR_TYPE.Text.Trim + "'"
        End If

        If cbAMR_SubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPE] = '" + cbAMR_SubType.Text.Trim + "'"
        End If

        If cbAMR_SUBtypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = '" + cbAMR_SUBtypeII.Text.Trim + "'"
        End If

        'If cbAMR_SUBtypeIII.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [AMR_SUBTYPEIII] = '" + cbAMR_SUBtypeIII.Text.Trim + "'"
        'End If

        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "AMR", "WHERE Active = " + _SQL_TRUE + strFilter)

    End Sub
    Private Sub cbAMR_IP_LAN_ID_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_IP_LAN_ID.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbAMR_PCBA_PN_DragDrop(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_PCBA.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbAMR_PCBA_Rev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_PCBA_Rev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbAMR_Software_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_Software.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbAMR_Software_Rev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_Software_Rev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbAMR_Voltage_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAMR_Voltage.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on AMR selections if all have been selected
        If cbAMR_Manufacturer.Text.Trim <> "" And cbAMR_Model.Text.Trim <> "" And cbAMR_TYPE.Text.Trim <> "" And cbAMR_SubType.Text.Trim <> "" And cbAMR_SUBtypeII.Text.Trim <> "" And cbAMR_SUBtypeIII.Text.Trim <> "" Then
            _MyAMRComboValues.Manufacturer = "'" + cbAMR_Manufacturer.Text.Trim + "'"
            _MyAMRComboValues.Model = "'" + cbAMR_Model.Text.Trim + "'"
            _MyAMRComboValues.Type = "'" + cbAMR_TYPE.Text.Trim + "'"
            _MyAMRComboValues.SubType = "'" + cbAMR_SubType.Text.Trim + "'"
            _MyAMRComboValues.SubTypeII = "'" + cbAMR_SUBtypeII.Text.Trim + "'"
            _MyAMRComboValues.SubTypeIII = "'" + cbAMR_SUBtypeIII.Text.Trim + "'"

            strFilter = " WHERE [AMR_Manufacturer] = " + _MyAMRComboValues.Manufacturer
            strFilter = strFilter + " AND [AMR] =" + _MyAMRComboValues.Model
            strFilter = strFilter + " AND [AMR_TYPE] = " + _MyAMRComboValues.Type
            strFilter = strFilter + " AND [AMR_SUBTYPE] = " + _MyAMRComboValues.SubType
            strFilter = strFilter + " AND [AMR_SUBTYPEII] = " + _MyAMRComboValues.SubTypeII
            strFilter = strFilter + " AND [AMR_SUBTYPEIII] = " + _MyAMRComboValues.SubTypeIII
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
#End Region ' AMR ComboBoxes

#Region "Test INFO xboXComboBoxes"
    Private Sub cbEUTType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEUTType.SelectedIndexChanged
        If gb_ProcessEvents Then
            ' Enable_EUT_Group_Based_On_EUT_TYPE(True)
            '  DisplayShadowControl(False)

            'force an update via the statemachine...Enable_EUT
            StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
        End If
    End Sub
    Private Sub cbTestedBy_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestedBy.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        'populate from MeterSpecList
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.TESTED_BY.TESTED_BY, _dbMeterSpecSchema.TESTED_BY.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub
    Private Sub cbTestLevel_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestLevel.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        gb_ProcessEvents = False
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.PAC_LEVEL.VALUE, _dbMeterSpecSchema.PAC_LEVEL.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
        gb_ProcessEvents = True
    End Sub

    Private Sub txtProjectNumber_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProjectNumber.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        gb_ProcessEvents = False
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.PROJECT.NUMBER, _dbMeterSpecSchema.PROJECT.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
        gb_ProcessEvents = True
    End Sub
    Private Sub cbTestType_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestType.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter = ""
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.TEST_TYPE.VALUE, _dbMeterSpecSchema.TEST_TYPE.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
    End Sub
    'Private Sub cbTestName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    cbTestName.DroppedDown = True
    'End Sub
    Private Sub cbTestLevel_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestLevel.SelectedValueChanged
        'Only process events when safe to do so...
        If gb_ProcessEvents = True Then

            'assume TCC approval is not required..
            chkTCC_ApprovalRequired.CheckState = CheckState.Unchecked

            'Always re-quire TCC approval for this type of testing....
            If cbTestLevel.Text.Trim = "FPA" Or cbTestLevel.Text.Trim = "PAC 2" Or cbTestLevel.Text.Trim = "OEM" Then
                chkTCC_ApprovalRequired.CheckState = CheckState.Checked


            ElseIf cbTestLevel.Text.Trim = "CIT" And MsgBox("Is TCC approval Required?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'prompt user to select if TCC approval is required if it is CIT testing
                chkTCC_ApprovalRequired.CheckState = CheckState.Checked

                'Else  'default to not requireing TCC approval if engineering testing
                '    chkTCC_ApprovalRequired.CheckState = CheckState.Unchecked
            End If
            SetApproverState()
        End If

    End Sub
    'Private Sub cbTestName_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestName.DropDown
    '    Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
    '    Dim strFilter = ""
    '    If cbTestType.Text.Trim <> "" Then

    '        'make sure that something has been selected...
    '        If cbTestType.SelectedIndex <> -1 Then
    '            'Return all standard tests defind in the MeterSpecDatabase
    '            If cbTestType.Text.Trim = "All" Or chkFilterTestsbyType.Checked = False Then
    '                strFilter = ""
    '                PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.STANDARD_TEST.TEST_NAME, _dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
    '            ElseIf cbTestType.Text.Trim = "Past Tests" Then
    '                'Get all tests from past tests from the failure report database
    '                PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, _dbFRSchema.FR_DBDef.TEST_NAME, _dbFRSchema.FR_DBDef.TABLE_NAME, "")
    '            Else
    '                strFilter = " AND Test_Type = '" + cbTestType.Text.Trim + "'"
    '                PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, _dbMeterSpecSchema.STANDARD_TEST.TEST_NAME, _dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, "WHERE Active = " + _SQL_TRUE + strFilter)
    '            End If

    '        End If
    '    End If


    '    Dim mystop As Boolean = True
    'End Sub


    'Private Sub cbTestName_SelectionComitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTestName.SelectionChangeCommitted

    '    Try
    '        'This code tries to force the TEST type field to agree with the Test Type of  the selected
    '        'Test Name
    '        'It only works if the Name exists in the Meter_SPEC database...
    '        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
    '        Dim MyDataRow As DataRowView = DirectCast(MyxboXComboBox.SelectedValue, DataRowView)

    '        Dim strFilter As String = " WHERE Test = '" + MyDataRow.Item(0).ToString.Trim + "'"
    '        Dim MyDatatable As DataTable

    '        'only return distinct matches (meaning do not return duplicates...)
    '        MyDatatable = gMyCustomDBAccess.GetDistinctData(gMyMeterSpecDBConnection, _dbMeterSpecSchema.STANDARD_TEST.TEST_TYPE, _dbMeterSpecSchema.STANDARD_TEST.TABLE_NAME, strFilter)

    '        If MyDatatable.Rows.Count > 0 Then
    '            'populate the first match
    '            cbTestType.Text = MyDatatable.Rows(0)(0).ToString
    '        Else
    '            'Assume that is a custom test...
    '            cbTestType.Text = "Custom"
    '        End If
    '    Catch
    '        'Silently catch error
    '    End Try

    '    ' End If

    'End Sub

    Private Sub cboProjectLead_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProjectLead.DropDown
        Try

            Dim dbTrue As String = "True"
            Dim FilterColumn As New List(Of String)
            Dim FilterValue As New List(Of String)

            FilterColumn.Add(_dbMeterSpecSchema.USERS.ACTIVE)
            FilterValue.Add(dbTrue)

            'Create a SQL connction object to connect to the METER_SPEC database
            _MYSQL_Control.SQLCon = New SqlConnection With {.ConnectionString = My.Settings.MeterSpecDataBaseFullConnectionString}
            'Query the database and return the USER table..the query will be returned as the first table in the DATASET in the _MYSQL_CONTROL object...
            _MYSQL_Control.GetTableData(_dbMeterSpecSchema.USERS.TABLE_NAME) ', "ORDER BY " + _dbMeterSpecSchema.USERS.LAST_NAME)
            'Create a list to hold all the Project leads...
            Dim ProjectLeadList As New List(Of String)

            'sort the table of Users by last name...
            Try
                _MYSQL_Control.SQL_Dataset.Tables(0).DefaultView.Sort = "LastName ASC"
            Catch ex As Exception
                MsgBox("Error Trying to sort Table" + vbCrLf + ex.ToString)
            End Try

            For Each Myrow As DataRow In _MYSQL_Control.SQL_Dataset.Tables(0).Rows
                'only add active users to the list of potential project leads
                If Myrow(_dbMeterSpecSchema.USERS.ACTIVE).ToString = "True" Then
                    ProjectLeadList.Add(Myrow(_dbMeterSpecSchema.USERS.LAST_NAME).ToString + ", " + Myrow(_dbMeterSpecSchema.USERS.FIRST_NAME).ToString)
                End If
            Next

            Dim MySource As New AutoCompleteStringCollection
            'add the list to the drop down...
            MySource.AddRange(ProjectLeadList.ToArray)
            cbProjectLead.Items.Clear()
            cbProjectLead.Items.AddRange(ProjectLeadList.ToArray)
            cbProjectLead.Sorted = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cbCorrectedBy_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCorrectedBy.DropDown
        Try

            Dim dbTrue As String = "True"
            Dim FilterColumn As New List(Of String)
            Dim FilterValue As New List(Of String)

            FilterColumn.Add(_dbMeterSpecSchema.USERS.ACTIVE)
            FilterValue.Add(dbTrue)

            'Create a SQL connction object to connect to the METER_SPEC database
            _MYSQL_Control.SQLCon = New SqlConnection With {.ConnectionString = My.Settings.MeterSpecDataBaseFullConnectionString}
            'Query the database and return the USER table..the query will be returned as the first table in the DATASET in the _MYSQL_CONTROL object...
            _MYSQL_Control.GetTableData(_dbMeterSpecSchema.USERS.TABLE_NAME) ', "ORDER BY " + _dbMeterSpecSchema.USERS.LAST_NAME)
            'Create a list to hold all the Project leads...
            Dim ProjectLeadList As New List(Of String)

            'sort the table of Users by last name...
            Try
                _MYSQL_Control.SQL_Dataset.Tables(0).DefaultView.Sort = "LastName ASC"
            Catch ex As Exception
                MsgBox("Error Trying to sort Table" + vbCrLf + ex.ToString)
            End Try

            For Each Myrow As DataRow In _MYSQL_Control.SQL_Dataset.Tables(0).Rows
                'only add active users to the list of potential project leads
                If Myrow(_dbMeterSpecSchema.USERS.ACTIVE).ToString = "True" Then
                    ProjectLeadList.Add(Myrow(_dbMeterSpecSchema.USERS.LAST_NAME).ToString + ", " + Myrow(_dbMeterSpecSchema.USERS.FIRST_NAME).ToString)
                End If
            Next

            Dim MySource As New AutoCompleteStringCollection
            'add the list to the drop down...
            MySource.AddRange(ProjectLeadList.ToArray)
            cbCorrectedBy.Items.Clear()
            cbCorrectedBy.Items.AddRange(ProjectLeadList.ToArray)
            cbCorrectedBy.Sorted = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cbApprovedBy_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbApprovedBy.DropDown
        Try

            Dim dbTrue As String = "True"
            Dim FilterColumn As New List(Of String)
            Dim FilterValue As New List(Of String)

            FilterColumn.Add(_dbMeterSpecSchema.APPROVERS.ACTIVE)
            FilterValue.Add(dbTrue)

            'Create a SQL connction object to connect to the METER_SPEC database
            _MYSQL_Control.SQLCon = New SqlConnection With {.ConnectionString = My.Settings.MeterSpecDataBaseFullConnectionString}
            'Query the database and return the USER table..the query will be returned as the first table in the DATASET in the _MYSQL_CONTROL object...
            _MYSQL_Control.GetTableData(_dbMeterSpecSchema.APPROVERS.TABLE_NAME)
            'Create a list to hold all the Project leads...
            Dim ApproverList As New List(Of String)

            ApproverList.Add(" Test Compliance Committee")

            'sort the table of Users by last name...
            Try
                _MYSQL_Control.SQL_Dataset.Tables(0).DefaultView.Sort = "APPROVER_NAME ASC"
            Catch ex As Exception
                MsgBox("Error Trying to sort Table" + vbCrLf + ex.ToString)
            End Try

            For Each Myrow As DataRow In _MYSQL_Control.SQL_Dataset.Tables(0).Rows
                'only add active users to the list of potential project leads
                If Myrow(_dbMeterSpecSchema.APPROVERS.ACTIVE).ToString = "True" Then
                    Dim MyApprover() As String = Myrow(_dbMeterSpecSchema.APPROVERS.APPROVER_NAME).ToString.Split(" ")
                    If MyApprover.Length = 2 Then
                        ApproverList.Add(MyApprover(1).ToString + ", " + MyApprover(0).ToString)
                    ElseIf MyApprover.Length = 1 Then
                        ApproverList.Add(MyApprover(0).ToString)
                    End If

                End If
            Next

            Dim MySource As New AutoCompleteStringCollection
            'add the list to the drop down...
            MySource.AddRange(ApproverList.ToArray)
            cbApprovedBy.Items.Clear()
            cbApprovedBy.Items.AddRange(ApproverList.ToArray)
            cbApprovedBy.Sorted = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

#End Region 'Test INFO xboXComboBoxes

#Region "Meter xboXComboBoxes"

    Private Sub cbMeterManufacturer_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterManufacturer.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Meter'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        Dim strFilter As String = ""

        'If cbMeterManufacturer.Text.Trim <> "" Then
        '    strFilter = " AND [Meter_Manufacturer] = '" + cbMeterManufacturer.Text.Trim + "'"
        'End If

        If cbMeterModel.Text.Trim <> "" Then
            strFilter = " AND [Meter] = '" + cbMeterModel.Text.Trim + "'"
        End If

        If cbMeterType.Text.Trim <> "" Then
            strFilter = " AND [Meter_TYPE] = '" + cbMeterType.Text.Trim + "'"
        End If

        If cbMeterSubType.Text.Trim <> "" Then
            strFilter = " AND [Meter_SUBTYPE] = '" + cbMeterSubType.Text.Trim + "'"
        End If

        If cbMeterSubTypeII.Text.Trim <> "" Then
            strFilter = " AND [Meter_SUBTYPEII] = '" + cbMeterSubTypeII.Text.Trim + "'"
        End If



        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Meter", "WHERE Active = " + _SQL_TRUE + strFilter)
        Try
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Meter"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub cbMeter_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterModel.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Failure Report'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")

        'If tsmOptionsManageDropDownsShowPastValues.Checked = True Then
        '    PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        'Else

        'populate xboXComboBox list with distinct values from table 'Meter'
        Dim strFilter As String = ""

        If cbMeterManufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_Manufacturer] = '" + cbMeterManufacturer.Text.Trim + "'"
        End If

        'If cbMeterModel.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [Meter] = '" + cbMeterModel.Text.Trim + "'"
        'End If

        If cbMeterType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_TYPE] = '" + cbMeterType.Text.Trim + "'"
        End If

        If cbMeterSubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPE] = '" + cbMeterSubType.Text.Trim + "'"
        End If

        If cbMeterSubTypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = '" + cbMeterSubTypeII.Text.Trim + "'"
        End If
        ' End If
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Meter", "WHERE Active = " + _SQL_TRUE + strFilter)
        Try
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Meter"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cbMeterType_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterType.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Meter'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        Dim strFilter As String = ""

        If cbMeterManufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_Manufacturer] = '" + cbMeterManufacturer.Text.Trim + "'"
        End If

        If cbMeterModel.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter] = '" + cbMeterModel.Text.Trim + "'"
        End If

        'If cbMeterType.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [Meter_TYPE] = '" + cbMeterType.Text.Trim + "'"
        'End If

        If cbMeterSubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPE] = '" + cbMeterSubType.Text.Trim + "'"
        End If

        If cbMeterSubTypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = '" + cbMeterSubTypeII.Text.Trim + "'"
        End If
        Try


            PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Meter", "WHERE Active = " + _SQL_TRUE + strFilter)
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Meter"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cbMeterSubType_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterSubType.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Meter'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        Dim strFilter As String = ""

        If cbMeterManufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_Manufacturer] = '" + cbMeterManufacturer.Text.Trim + "'"
        End If

        If cbMeterModel.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter] = '" + cbMeterModel.Text.Trim + "'"
        End If

        If cbMeterType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_TYPE] = '" + cbMeterType.Text.Trim + "'"
        End If

        'If cbMeterSubType.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [Meter_SUBTYPE] = '" + cbMeterSubType.Text.Trim + "'"
        'End If

        If cbMeterSubTypeII.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = '" + cbMeterSubTypeII.Text.Trim + "'"
        End If
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Meter", "WHERE Active = " + _SQL_TRUE + strFilter)
        Try
            ' PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Meter"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cbMeterSubTypeII_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterSubTypeII.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'populate xboXComboBox list with distinct values from table 'Meter'
        'PopulatexboXComboBox(gMyFailureReportOleDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report")
        Dim strFilter As String = ""

        If cbMeterManufacturer.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_Manufacturer] = '" + cbMeterManufacturer.Text.Trim + "'"
        End If

        If cbMeterModel.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter] = '" + cbMeterModel.Text.Trim + "'"
        End If

        If cbMeterType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_TYPE] = '" + cbMeterType.Text.Trim + "'"
        End If

        If cbMeterSubType.Text.Trim <> "" Then
            strFilter = strFilter + " AND [Meter_SUBTYPE] = '" + cbMeterSubType.Text.Trim + "'"
        End If

        'If cbMeterSubTypeII.Text.Trim <> "" Then
        '    strFilter = strFilter + " AND [Meter_SUBTYPEII] = '" + cbMeterSubTypeII.Text.Trim + "'"
        'End If
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Meter", "WHERE Active = " + _SQL_TRUE + strFilter)
        Try
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Meter"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbMeterSerialNumber_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterSerialNumber.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            Try
                'PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
            Catch ex As Exception

            End Try
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterDSP_Rev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterDSP_Rev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            'PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterPCBA_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterPCBA.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            ' PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)

        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterPCBA_Rev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterPCBA_Rev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            ' PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterSoftware_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterSoftware.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            'PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterSoftwareRev_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterSoftwareRev.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            ' PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub

    Private Sub cbMeterVoltage_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterVoltage.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        MyxboXComboBox.Tag = "Meter_Voltage"
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then

            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            ' PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbFW_Ver_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterFW_Ver.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        Dim strFilter As String = ""
        'Conditional Fill from past values in the Failure Report Database based on Meter selections if all have been selected
        If cbMeterManufacturer.Text.Trim <> "" And cbMeterModel.Text.Trim <> "" And cbMeterType.Text.Trim <> "" And cbMeterSubType.Text.Trim <> "" And cbMeterSubTypeII.Text.Trim <> "" Then
            _MyMeterComboValues.Manufacturer = "'" + cbMeterManufacturer.Text.Trim + "'"
            _MyMeterComboValues.Model = "'" + cbMeterModel.Text.Trim + "'"
            _MyMeterComboValues.Type = "'" + cbMeterType.Text.Trim + "'"
            _MyMeterComboValues.SubType = "'" + cbMeterSubType.Text.Trim + "'"
            _MyMeterComboValues.SubTypeII = "'" + cbMeterSubTypeII.Text.Trim + "'"
            ' _MyMeterComboValues.FORM = "'" + cbMeterForm.Text.Trim + "'"
            ' _MyMeterComboValues.BASE = "'" + cbMeterBase.Text.Trim + "'"

            strFilter = " WHERE [Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            'strFilter = "[Meter_Manufacturer] = " + _MyMeterComboValues.Manufacturer
            strFilter = strFilter + " AND [Meter] =" + _MyMeterComboValues.Model
            strFilter = strFilter + " AND [Meter_TYPE] = " + _MyMeterComboValues.Type
            strFilter = strFilter + " AND [Meter_SUBTYPE] = " + _MyMeterComboValues.SubType
            strFilter = strFilter + " AND [Meter_SUBTYPEII] = " + _MyMeterComboValues.SubTypeII
            'strFilter = strFilter + " AND [FORM] = " + _MyMeterComboValues.FORM
            ' strFilter = strFilter + " AND [Meter_Base] = " + _MyMeterComboValues.BASE
            PopulatexboXComboBox(gMyFailureReportDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Failure Report", strFilter)
            ' PopulatexboXComboBox(gMyFailureReportDataTable, MyxboXComboBox, MyxboXComboBox.Tag.ToString, strFilter)
        Else
            Dim MyDataArray(1) As String
            MyDataArray(0) = ""
            MyxboXComboBox.DataSource = MyDataArray
        End If
    End Sub
    Private Sub cbForm_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterForm.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Conditional Fill Based on if an AMR has been selected
        If cbMeterBase.Text.Trim <> "" Then
            Dim strMeterBase As String = cbMeterBase.Text.Trim
            Dim strFilter As String
            strFilter = " AND ([Meter_Base] LIKE '%," + strMeterBase + ",%'" _
                + " OR [Meter_Base] LIKE '%," + strMeterBase + "'" _
                + " OR [Meter_Base] LIKE '" + strMeterBase + ",%'" _
                + " OR [Meter_Base] = '" + strMeterBase + "')"
            PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Form", "WHERE Active = " + _SQL_TRUE + strFilter)

            Try
                ' PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Form"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
            Catch ex As Exception

            End Try
        Else
            'populate xboXComboBox list with distinct values from table 'Failure Report'
            PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Form", " WHERE Active = " + _SQL_TRUE)
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Form"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True")
        End If
    End Sub
    Private Sub cbBase_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMeterBase.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Conditional Fill Based on if an AMR has been selected
        If cbMeterForm.Text.Trim <> "" Then
            Dim strMeterForm As String = cbMeterForm.Text.Trim
            Dim strFilter As String
            strFilter = " WHERE [Form] = '" + strMeterForm + "'"
            'strFilter = " AND [Form] = '" + strMeterForm + "'"
            PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Form", strFilter + " AND Active = " + _SQL_TRUE)
            ' PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Form"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True" + strFilter)
            Dim MyString As String = ""
            Try
                For Each item In MyxboXComboBox.DataSource
                    MyString = MyString + item.ToString + ","
                Next
            Catch

            End Try
            'The Form numbers that have mutilple base configurations are stored comma delimited
            Dim mydatatable As New DataTable
            mydatatable = MyxboXComboBox.DataSource
            For Each Row In mydatatable.Rows
                MyString = MyString + Row(0).ToString + ","
            Next
            Dim MyStringArray() As String = MyString.Split(",")
            MyStringArray = MyStringArray.Distinct.ToArray

            MyxboXComboBox.DataSource = MyStringArray
        Else
            'populate xboXComboBox list with distinct values from table 'Failure Report'
            PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Form", " WHERE Active = " + _SQL_TRUE)
            'PopulatexboXComboBox(gMyMeterSpecDbDataSet.Tables("Form"), MyxboXComboBox, MyxboXComboBox.Tag.ToString, "Active = True")
            Dim MyString As String = ""

            'The Form numbers that have mutilple base configurations are stored comma delimited
            Dim mydatatable As New DataTable
            mydatatable = MyxboXComboBox.DataSource
            For Each Row In mydatatable.Rows
                MyString = MyString + Row(0).ToString + ","
            Next
            Dim MyStringArray() As String = MyString.Split(",")
            MyStringArray = MyStringArray.Distinct.ToArray

            MyxboXComboBox.DataSource = MyStringArray

        End If
    End Sub
#End Region 'Meter xboXComboBoxes

#Region "TCC xboXComboBoxes"
    Private Sub cbTCC_1_Compliance_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_1_Compliance.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE = 'Compliance'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
    Private Sub cbTCC_2_Engineering_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_2_Engineering.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE ='Development Engineering'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
    Private Sub cbTCC_3_Manufacturing_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_3_Manufacturing.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE = 'Manufacturing'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
    Private Sub cbTCC_4_Product_Management_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_4_Product_Management.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE  = 'Product Management'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
    Private Sub cbTCC_5_Quality_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_5_Quality_Product_Delivery.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE = 'Supplier Quality'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
    'SYSTEMS
    Private Sub cbTCC_6_HQA_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTCC_6_SYSTEMS.DropDown
        Dim MyxboXComboBox As xboXComboBox = DirectCast(sender, xboXComboBox)
        'Always Return Active, Voting Members
        Dim DelegateState As Integer = 0
        If chkShowDelegates.CheckState = CheckState.Checked Then
            DelegateState = 1
        End If
        Dim MyFilter As String = "WHERE Active = " + _SQL_TRUE + " AND (VOTING_MEMBER = " + _SQL_TRUE + " or DELEGATE = " + DelegateState.ToString + ") AND DISCIPLINE = 'Systems'"
        PopulatexboXComboBox(gMyMeterSpecDBConnection, MyxboXComboBox, "APPROVER_NAME", "APPROVERS", MyFilter)
    End Sub
#End Region 'TCC xboXComboBoxes

#End Region 'xboXComboBox Events
#Region "Tool Strip Menu Items Events"
#Region "Files"
    Private Sub tsmFileCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileCancel.Click
        Try
            gb_ProcessEvents = False
            'Get the maximum and minimum Failure Report number (FR Boundry Conditions) 
            Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing)
            Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([New ID])", Nothing)

            'Varaible to hold the current position
            Dim CurrentID As Integer = Val(txtPosition.Text.Trim)

            'point CurrentRow at the Bound Row in the data set, this should always be the new 
            'row that was added because the navigation of the form should be locked out
            'i.e. - the gridview is readonly, the Navigation buttons are disabled
            If dgvFailureReportDataGridView.CurrentRow Is Nothing Then
                gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", CurrentID)
            Else
                gMyFailureReportBindingSource.Position = dgvFailureReportDataGridView.CurrentRow.Index
            End If


            'Cast the new row from the binding source to DataRowView to ease minipulation 
            Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)

            If _EditState = eEditState.EDIT_CREATE_NEW Or _EditState = eEditState.EDIT_COPY Then
                If MsgBox("The New Failure Report will be completly Disgarded and the the failure report will be deleted. Is that OK?", vbYesNo, "OK") = vbYes Then

                    'First Delete Attachments...
                    Dim FR_ZIP_PATH As String

                    If txtAttachments.Text.Trim = "" Then
                        FR_ZIP_PATH = Path.GetDirectoryName(My.Settings.MeterSpecDataBaseDataSource)
                        Dim FolderName As String = "ID" + txtReportNumber.Text.Trim

                        'Convert a Mapped Drive Pathe to a UNC path
                        'This is currently hard coded in multiple locations...it should probably be in a table in the database...o
                        FR_ZIP_PATH = ConvertMappedFilePathToUNCPath(FR_ZIP_PATH + "\Attachments\" & FolderName & ".zip")
                        txtAttachments.Text = FR_ZIP_PATH
                    Else
                        FR_ZIP_PATH = txtAttachments.Text.Trim
                    End If

                    Dim MyResponse As Boolean = System.IO.File.Exists(FR_ZIP_PATH)

                    'If  Exists then delete the zipfile
                    If MyResponse = True Then
                        'Delete the fo
                        Try
                            System.IO.File.Delete(FR_ZIP_PATH)
                            MsgBox("Attachments Deleted!")
                        Catch ex As Exception
                            MsgBox("Error Deleting: " + vbCrLf _
                                   + FR_ZIP_PATH + vbCrLf + ex.ToString)
                        End Try

                    End If




                    'database call and delete
                    CurrentRow.Delete()
                    'This is necessary to leave edit mode without another nag window in change edit mode
                    'added these lines to get rid of concurrancy error.
                    Validate()

                    'Notification to Binding source to commit changes
                    gMyFailureReportBindingSource.EndEdit()

                    'update the database
                    gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

                    'should a Datatable.clear followed by a datatable fill be called here?  -FJB
                    'Set to most recently added failure report
                    CurrentID = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing) 'CInt((Val(txtPosition.Text - 1)))
                    If CurrentID > MaxID Or CurrentID < MinID Then
                        'do nothing right now, handle error...
                    Else
                        txtPosition.Text = CurrentID.ToString
                        gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
                    End If




                    _RecordSaved = True
                End If

            Else
                ' _RecordSaved = True
                'update the database
                'Notification to Binding source to commit changes
                gMyFailureReportBindingSource.CancelEdit()
                gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)
            End If

            'Revert to ReadOnly
            StateMachine(eEditState.READ_ONLY, _EditState, _CurrentUser.AccessLevel)
            txtPosition.Text = CurrentID.ToString
            gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
        Catch ex As Exception
            gb_ProcessEvents = True
            MsgBox("tsmCancel_Click " + vbCrLf + ex.ToString)
        End Try
        gb_ProcessEvents = True
    End Sub

    Private Function GetEmailAddress() As String
        Try

            Dim dbTrue As String = "True"
            Dim FilterColumn As New List(Of String)
            Dim FilterValue As New List(Of String)

            Dim EmailAddress As String = ""

            'Get the project lead name.  This assumes that the Project Leads name is the same as 
            'their email address FirstName.LastName@landisgyr.com  (There are exceptions) so a better name is needed.
            Dim ProjectLead() As String = cbProjectLead.Text.Split(",")
            Dim FirstName As String = ProjectLead(1).Trim
            Dim LastName As String = ProjectLead(0).Trim


            FilterColumn.Add(_dbMeterSpecSchema.USERS.ACTIVE)
            FilterValue.Add(dbTrue)

            FilterColumn.Add(_dbMeterSpecSchema.USERS.FIRST_NAME)
            FilterValue.Add(FirstName)

            FilterColumn.Add(_dbMeterSpecSchema.USERS.LAST_NAME)
            FilterValue.Add(LastName)

            'Create a SQL connction object to connect to the METER_SPEC database
            _MYSQL_Control.SQLCon = New SqlConnection With {.ConnectionString = My.Settings.MeterSpecDataBaseFullConnectionString}
            'Query the database and return the USER table..the query will be returned as the first table in the DATASET in the _MYSQL_CONTROL object...
            _MYSQL_Control.GetTableData(_dbMeterSpecSchema.USERS.TABLE_NAME) ', "ORDER BY " + _dbMeterSpecSchema.USERS.LAST_NAME)
            'Create a list to hold all the Project leads...
            Dim ProjectEmailList As New List(Of String)

            'sort the table of Users by last name...
            Try
                _MYSQL_Control.SQL_Dataset.Tables(0).DefaultView.Sort = "LastName ASC"
            Catch ex As Exception
                MsgBox("Error Trying to sort Table" + vbCrLf + ex.ToString)
            End Try

            For Each Myrow As DataRow In _MYSQL_Control.SQL_Dataset.Tables(0).Rows
                'only add active users to the list of potential project leads
                If Myrow(_dbMeterSpecSchema.USERS.ACTIVE).ToString = "True" And Myrow(_dbMeterSpecSchema.USERS.FIRST_NAME).ToString = FirstName And Myrow(_dbMeterSpecSchema.USERS.LAST_NAME).ToString = LastName Then
                    EmailAddress = Myrow(_dbMeterSpecSchema.USERS.email).ToString
                    ' ProjectEmailList.Add(Myrow(_dbMeterSpecSchema.USERS.LAST_NAME).ToString + ", " + Myrow(_dbMeterSpecSchema.USERS.FIRST_NAME).ToString)
                End If
            Next

            Return EmailAddress

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Function SaveRecord() As Boolean

        'Kick the user out if minimum Rights are not met....
        If _CurrentUser.AccessLevel = eAccessState.READ_ONLY Or _CurrentUser.AccessLevel = eAccessState.NO_ACCESS Then
            Return False
        End If

        Dim DialogBox As MsgBoxCheckMessageBox = New MsgBoxCheckMessageBox
        Dim MyDontShowAgain As DialogResult = Windows.Forms.DialogResult.OK
        Dim MyDialogResult As DialogResult = DialogBox.Show("Software\FailureReportDatabase\tsmSaveFR", "DontShowAgain", MyDontShowAgain, "Don't show this Again", "Are You sure that you want save, this cannot be undone?", "Commit Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Dim MyRegKey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\FailureReportDatabase\tsmSaveFR")
        Dim KeyValue = MyRegKey.GetValue("DontShowAgain")
        If MyDialogResult = Windows.Forms.DialogResult.No Then
            v_UpdateLog("Save Aborted")
            Return False
        End If

        'Limit maxlength for these text boxes...Developmental....plan to approve printing at some point to remove this limit...
        rtxtMeterNotes.MaxLength = 200
        rtxtAMR_Notes.MaxLength = 200

        ' Check in make sure that all of the required fields have data before saving....
        If _CurrentUser.AccessLevel = eAccessState.CREATE_NEW Or _CurrentUser.AccessLevel = eAccessState.POWER Then
            '                                                       Admin user can always save....
            '                                                       CR_Edit and Engineer and Approver Cannot edit header
            '                                                       So there is no reason to check.

            If Me.mtxtDateFailed.Text.Trim = "" Then

                MsgBox("Date Failed: Must be Filled in.")
                mtxtDateFailed.SelectAll()
                Return False
            End If

            If Me.cbProjectNumber.Text.Trim = "" Then
                MsgBox("Project Number: Must be Filled in.")
                cbProjectNumber.SelectAll()
                Return False
            End If

            If Me.txtProject.Text.Trim = "" Then
                MsgBox("Project Name: Must be Filled in.")
                txtProject.SelectAll()
                Return False
            End If

            If Me.cbTestLevel.Text = "[Level]" Or Me.cbTestLevel.Text.Trim = "" Then
                MsgBox("Test Level: Must be Filled in.")
                cbTestLevel.SelectAll()
                Return False
            End If

            If Me.cbTestedBy.Text.Trim = "[Tested By]" Or Me.cbTestedBy.Text.Trim = "" Then
                MsgBox("Tested By: Must be Filled in.")
                cbTestedBy.SelectAll()
                Return False
            End If

            If Me.cbProjectLead.Text.Trim = "" Then
                MsgBox("Project Lead: Must be Filled in.")
                cbProjectLead.SelectAll()
                Return False
            Else
                cbProjectLead.Text = cbProjectLead.Text.Trim 'remove leading space...
            End If


            If Me.txtTest.Text = "[ANSI & L+G Tests]" Or Me.txtTest.Text.Trim = "" Then
                MsgBox("Test Name: Must be Filled in.")
                btnSelectTest.Select()
                Return False
            End If

            If Me.cbEUTType.Text.Trim = "" Then
                MsgBox("EUT Type: Must be Filled in.")
                cbEUTType.SelectAll()
                Return False
            End If

            If cbEUTType.Text = "Meter Only" Or cbEUTType.Text = "AMI" Then

                If cbMeterManufacturer.Text.Trim = "" Then
                    MsgBox("Meter Manufacturer: Must be Filled in.")
                    cbMeterManufacturer.SelectAll()
                    Return False
                End If

                If Me.cbMeterModel.Text = "[Meter Type]" Or Me.cbMeterModel.Text.Trim = "" Then
                    MsgBox("Meter Model: Must be Filled in.")
                    cbMeterModel.SelectAll()
                    Return False
                End If
                If Me.cbMeterType.Text = "[Meter Type]" Or Me.cbMeterType.Text.Trim = "" Then
                    MsgBox("Meter Type: Must be Filled in.")
                    cbMeterType.SelectAll()
                    Return False
                End If

                If cbMeterSubType.Text.Trim = "" Then
                    MsgBox("Meter Sub-Type: Must be Filled in.")
                    cbMeterSubType.SelectAll()
                    Return False
                End If

                If cbMeterSubTypeII.Text.Trim = "" Then
                    MsgBox("Meter Sub-Type II: Must be Filled in.")
                    cbMeterSubTypeII.SelectAll()
                    Return False
                End If

                If cbMeterSerialNumber.Text.Trim = "" Then
                    MsgBox("Meter SN: Must be Filled in.")
                    cbMeterSerialNumber.SelectAll()
                    Return False
                End If

                If Me.cbMeterForm.Text = "[Meter Form]" Or Me.cbMeterForm.Text.Trim = "" Then
                    MsgBox("Meter Form: Must be Filled in.")
                    cbMeterForm.SelectAll()
                    Return False
                End If

                If cbMeterBase.Text.Trim = "" Then
                    MsgBox("Meter Base: Must be Filled in.")
                    cbMeterBase.SelectAll()
                    Return False
                End If

                If cbMeterDSP_Rev.Text.Trim = "" Then
                    MsgBox("Meter DSP: Must be Filled in.")
                    cbMeterDSP_Rev.SelectAll()
                    Return False
                End If

                If Me.cbMeterFW_Ver.Text = "[FW Ver]" Or Me.cbMeterFW_Ver.Text.Trim = "" Then
                    MsgBox("FW REV: Must be Filled in.")
                    cbMeterFW_Ver.SelectAll()
                    Return False
                End If

                If cbMeterPCBA.Text.Trim = "" Then
                    MsgBox("Meter PCBA PN: Must be Filled in.")
                    cbMeterPCBA.SelectAll()
                    Return False
                End If

                If cbMeterPCBA_Rev.Text.Trim = "" Then
                    MsgBox("Meter PCBA Rev: Must be Filled in.")
                    cbMeterPCBA_Rev.SelectAll()
                    Return False
                End If

                If cbMeterSoftware.Text.Trim = "" Then
                    MsgBox("Meter Software: Must be Filled in.")
                    cbMeterSoftware.SelectAll()
                    Return False
                End If

                If cbMeterSoftwareRev.Text.Trim = "" Then
                    MsgBox("Meter Software Rev: Must be Filled in.")
                    cbMeterSoftwareRev.SelectAll()
                    Return False
                End If

                If cbMeterVoltage.Text.Trim = "" Then
                    MsgBox("Meter Voltage: Must be Filled in.")
                    cbMeterVoltage.SelectAll()
                    Return False
                End If

            End If

            If cbEUTType.Text = "AMR Only" Or cbEUTType.Text = "AMI" Then

                If cbAMR_Manufacturer.Text.Trim = "" Then
                    MsgBox("AMR Manufacturer: Must be Filled in.")
                    cbAMR_Manufacturer.SelectAll()
                    Return False
                End If

                If Me.cbAMR_Model.Text = "[AMR Type]" Or Me.cbAMR_Model.Text.Trim = "" Then
                    MsgBox("AMR Type: Must be Filled in.")
                    cbAMR_Model.SelectAll()
                    Return False
                End If

                If Me.cbAMR_TYPE.Text = "[Meter Type]" Or Me.cbAMR_TYPE.Text.Trim = "" Then
                    MsgBox("AMR Type: Must be Filled in.")
                    cbAMR_TYPE.SelectAll()
                    Return False
                End If

                If cbAMR_SubType.Text.Trim = "" Then
                    MsgBox("AMR Sub-Type: Must be Filled in.")
                    cbAMR_SubType.SelectAll()
                    Return False
                End If

                If cbAMR_SUBtypeII.Text.Trim = "" Then
                    MsgBox("AMR Sub-Type II: Must be Filled in.")
                    cbAMR_SUBtypeII.SelectAll()
                    Return False
                End If

                If cbAMR_SUBtypeIII.Text.Trim = "" Then
                    MsgBox("AMR Sub-Type III: Must be Filled in.")
                    cbAMR_SUBtypeIII.SelectAll()
                    Return False
                End If

                If cbAMR_SerialNumber.Text.Trim = "" Then
                    MsgBox("AMR SN: Must be Filled in.")
                    cbAMR_SerialNumber.SelectAll()
                    Return False
                End If

                If Me.cbAMR_IP_LAN_ID.Text = "[AMR Rev]" Or Me.cbAMR_IP_LAN_ID.Text.Trim = "" Then
                    MsgBox("AMR LAN ID: Must be Filled in.")
                    cbAMR_IP_LAN_ID.SelectAll()
                    Return False
                End If

                If Me.cbAMR_FW_Rev.Text = "[AMR Rev]" Or Me.cbAMR_FW_Rev.Text.Trim = "" Then
                    MsgBox("AMR Rev: Must be Filled in.")
                    cbAMR_FW_Rev.SelectAll()
                    Return False
                End If

                If cbAMR_PCBA.Text.Trim = "" Then
                    MsgBox("AMR PCBA PN: Must be Filled in.")
                    cbAMR_PCBA.SelectAll()
                    Return False
                End If

                If cbAMR_PCBA_Rev.Text.Trim = "" Then
                    MsgBox("AMR PCBA Rev: Must be Filled in.")
                    cbAMR_PCBA_Rev.SelectAll()
                    Return False
                End If

                If cbAMR_Software.Text.Trim = "" Then
                    MsgBox("AMR Software: Must be Filled in.")
                    cbAMR_Software.SelectAll()
                    Return False
                End If

                If cbAMR_Software_Rev.Text.Trim = "" Then
                    MsgBox("AMR Software Rev: Must be Filled in.")
                    cbAMR_Software_Rev.SelectAll()
                    Return False
                End If

                If cbAMR_Voltage.Text.Trim = "" Then
                    MsgBox("AMR Voltage: Must be Filled in.")
                    cbAMR_Voltage.SelectAll()
                    Return False
                End If


                If cbEUTType.Text.ToLower = "other eut" Then
                    'Do nothing for now...
                    If MsgBox("Other EUT Selected! Has all EUT data as applicable in the EUT 1 and EUT 2 Fields?", MsgBoxStyle.YesNo, "Other EUT INFO Confirmation") = MsgBoxResult.No Then
                        Return False
                    End If

                End If
            End If

            If chkAnomely.CheckState = CheckState.Indeterminate Then
                MsgBox("Indeterminate State Not Allowed: detected in Anomely Checkbox.")
                Return False
            End If

            If chkTCC_ApprovalRequired.CheckState = CheckState.Indeterminate Then
                MsgBox("Indeterminate State Not Allowed: detected in TCC Review Required Checkbox.")
                Return False
            End If
        End If

        If chkAnomely.Checked = True Then

            If Me.rtxtDescription.Text.Trim = _myTxtReportDescriptionBackUp.Trim And _EditState = eEditState.EDIT_CREATE_NEW Then
                MsgBox("Anomaly Description: Must be Filled in.")
                rtxtDescription.SelectAll()
                Return False
            ElseIf (Me.rtxtDescription.Text.Trim) = _myTxtReportDescriptionBackUp.Trim Then
                'do nothing
            Else
                Me.rtxtDescription.Text = Me.rtxtDescription.Text + vbLf
            End If

        Else

            If (Me.rtxtDescription.Text.Trim) = _myTxtReportDescriptionBackUp.Trim And _EditState = eEditState.EDIT_CREATE_NEW Then
                MsgBox("Description: Must be Filled in.")
                rtxtDescription.SelectAll()
                Return False
            ElseIf (Me.rtxtDescription.Text.Trim) = _myTxtReportDescriptionBackUp.Trim Then
                'do nothing
            Else
                Me.rtxtDescription.Text = Me.rtxtDescription.Text + vbLf
            End If

        End If

        'Remove any invisable characters if  empty...

        If cbCorrectedBy.Text.Trim = "" Then
            cbCorrectedBy.Text = ""
        End If

        If rtxtEngineeringNotes.Text.Trim = "" Then
            rtxtEngineeringNotes.Text = ""
        End If

        If rtxtTCC_Comments.Text.Trim = "" Then
            rtxtTCC_Comments.Text = ""
        End If


        'make sure that all conditions are met for marking a FR approved when TCC approval is required 'cbTCC_6_HQA.Text.Trim <> "" And 
        If cbTCC_1_Compliance.Text.Trim <> "" And cbTCC_2_Engineering.Text.Trim <> "" And _
           cbTCC_3_Manufacturing.Text.Trim <> "" And cbTCC_4_Product_Management.Text.Trim <> "" And _
           cbTCC_5_Quality_Product_Delivery.Text.Trim <> "" And cbTCC_6_SYSTEMS.Text.Trim <> "" And _
           chkTCC_ApprovalRequired.CheckState = CheckState.Checked And _
           mtxtDateCorrected.Text.Trim <> "" Then

            ChkFR_Approved.Checked = True

            ChkFR_Approved.CheckState = CheckState.Checked

            'Assign approval date if empty...
            If mtxtDateApproved.Text.Trim = "" Then
                mtxtDateApproved.Text = Now.ToShortDateString
            End If

            'Close the FR if empty...
            If mtxtDateClosed.Text.Trim = "" Then
                mtxtDateClosed.Text = mtxtDateCorrected.Text
            End If

        ElseIf chkTCC_ApprovalRequired.CheckState = CheckState.Checked Then
            ChkFR_Approved.Checked = False
            ChkFR_Approved.CheckState = CheckState.Unchecked
        End If



        Try
            ' end edit on all binding sources...
            gMyFailureReportBindingSource.EndEdit()

            '' am not sure that this is needed...
            'Me.EndEditOnAllBindingSources()

            ' Force a control Validation not sure if this is needed - FJB
            Me.Validate()

            ' This Sends the changes out to the remote database
            gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

        Catch ex As Exception
            MsgBox("Error writing changes to database" + vbCrLf + ex.ToString)
            Return False
        End Try

        Return True

    End Function
    Private Sub tsmFileSaveFR_Data_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileSaveFR_Data.Click
        'Stop the DataGrid SelectionChange event code from running
        gb_ProcessEvents = False

        ' Set Saved Flag
        _RecordSaved = SaveRecord()

        'reenable the DataGrid Selection Change Code
        gb_ProcessEvents = True

        If _RecordSaved = True Then
            Dim mylocation As String = txtPosition.Text
            RefreshFailureReportDataBase(mylocation)
            txtPosition.Text = mylocation
            'gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
        End If
    End Sub

    ''' <summary>
    ''' This Sub Sends a message...
    ''' </summary>
    ''' <param name="Application"></param>
    ''' <remarks>Frank Boudreau 2023</remarks>
    Private Sub CreateSendItem(ByVal Application As outlook.Application, Optional ByVal SendEmail As Boolean = True)
        If SendEmail Then
            Dim mail As outlook.MailItem = Nothing
            Dim mailRecipients As outlook.Recipients = Nothing
            Dim mailRecipient As outlook.Recipient = Nothing
            Try
                mail = Application.CreateItem(outlook.OlItemType.olMailItem)
                mail.Subject = "A programatically generated Failure or Anomaly e-mail"
                Dim ReportPrefix As String = "FR"

                If chkAnomely.Checked = True Then
                    ReportPrefix = "AR"
                End If

                mail.Body = _CurrentUser.FirstName + " " + _CurrentUser.LastName + " has saved a change to " + ReportPrefix + txtPosition.Text + "."

                If chkFailedSampleReady.Checked = True And ChkFR_Approved.Checked = False Then
                    mail.Body += vbCrLf + ReportPrefix + txtPosition.Text + " has been generated for Project " + txtProject.Text + " (Proj# " + cbProjectNumber.Text + ")."
                    mail.Body += vbCrLf + "Test Name: " + txtTest.Text
                    mail.Body += vbCrLf + "Test Level " + cbTestLevel.Text
                    mail.Body += vbCrLf + ReportPrefix + " Description:"
                    mail.Body += vbCrLf + rtxtDescription.Text

                    Dim DateAvailable As Date
                    Dim DateDue As Date

                    If Date.TryParse(mtxtDateFailedSampleReady.Text, DateAvailable) = True Then
                        DateDue = DateAvailable.AddDays(30)
                        mail.Body += vbCrLf + "Reminder:"
                        mail.Body += vbCrLf + "First Reponse to the report is due " + DateAvailable.AddDays(7).ToString + ", and updates are due weekly until the Report is closed."
                        mail.Body += vbCrLf + "The target date for report closure is " + DateAvailable.AddDays(30).ToString + "."
                    End If

                    mail.Body += vbCrLf + vbCrLf + "Thanks,"
                    mail.Body += vbCrLf + "The General Engineering Lab"
                ElseIf ChkFR_Approved.Checked = False Then
                    mail.Body += vbCrLf + ReportPrefix + txtPosition.Text + " has been generated for Project """ + txtProject.Text + """, Proj# " + cbProjectNumber.Text + "."
                    mail.Body += vbCrLf + "Test Name: " + txtTest.Text
                    mail.Body += vbCrLf + "Test Level " + cbTestLevel.Text
                    mail.Body += vbCrLf + ReportPrefix + " Description:"
                    mail.Body += vbCrLf + rtxtDescription.Text
                    mail.Body += vbCrLf + "You will be notified when the Test Sample is available to be checked out of the lab."
                    mail.Body += vbCrLf + vbCrLf + "Thanks,"
                    mail.Body += vbCrLf + "The General Engineering Lab"
                ElseIf ChkFR_Approved.Checked = True Then
                    mail.Body += vbCrLf + ReportPrefix + txtPosition.Text + "Has been approved."
                End If

                mailRecipients = mail.Recipients

                Dim ProjectLead() As String = cbProjectLead.Text.Split(",")
                Dim FirstName As String = ProjectLead(1).Trim
                Dim LastName As String = ProjectLead(0).Trim

                'mailRecipient = mailRecipients.Add("Frank Boudreau")
                mailRecipient = mailRecipients.Add(FirstName + " " + LastName)
                mailRecipient.Resolve()
                Dim Myaddress As Object = mailRecipient.AddressEntry
                If (mailRecipient.Resolved) Then
                    mail.Send()
                Else
                    v_UpdateLog("There is no such record in your address book.")
                End If
            Catch ex As Exception
                v_UpdateLog(ex.Message + vbCrLf + "An exception is occured in the code of add-in.")
            Finally
                If Not IsNothing(mailRecipient) Then Marshal.ReleaseComObject(mailRecipient)
                If Not IsNothing(mailRecipients) Then Marshal.ReleaseComObject(mailRecipients)
                If Not IsNothing(mail) Then Marshal.ReleaseComObject(mail)
            End Try
        End If

    End Sub

    Private Sub tsmFileSaveAndExitEditMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileSaveAndExitEditMode.Click
        'Stop the DataGrid SelectionChange event code from running
        gb_ProcessEvents = False
        ' Set Saved Flag
        _RecordSaved = SaveRecord()

        'Set the Current record to the
        If _RecordSaved = True Then
            Dim mylocation As String = txtPosition.Text
            RefreshFailureReportDataBase(mylocation, RemoveFilterOnRefreshOrSaveToolStripMenuItem.Checked)
            txtPosition.Text = mylocation
        End If
        Dim EmailAddress As String = Nothing
        Try
            EmailAddress = GetEmailAddress() + "@landisgyr.com"
            v_UpdateLog(EmailAddress)
            v_UpdateLog(_CurrentUser.FirstName + " " + _CurrentUser.LastName + " Has updated FR" + txtPosition.Text)
            v_UpdateLog("Current State= " + _EditState.ToString)
            If chkAutoNotifyOnSave.Checked = True Then
                Dim App As outlook.Application = New outlook.Application
                CreateSendItem(App)
            End If

        Catch ex As Exception
            v_UpdateLog("Email Address returned nothing, tsmFileSaveAndExitEditMode_Click")
        End Try

        If Not (EmailAddress Is Nothing) Then


        End If



        ' Go back to Readonly mode
        Dim NewState As eEditState = eEditState.READ_ONLY
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
        'reenable the DataGrid Selection Change Code
        gb_ProcessEvents = True
    End Sub
    Private Sub tsmFileNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileNew.Click
        Try
            'Verify that this is the correct state...Only allowed to goto New from ReadOnly State
            If _EditState = eEditState.READ_ONLY Then




                'Disable events raised by controls....
                gb_ProcessEvents = False

                'Query Max and Min ID 
                'This needs to be perfromed from data base since other user may editing, this looks locally only...
                Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing)
                'Dim MinID As Integer ' = gMyFailureReportDataTable.Compute("MIN([New ID])", Nothing)

                Dim MaxID_OnSever As Integer = Val(gMyCustomDBAccess.GetMaxValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))
                'MinID = Val(gMyCustomDBAccess.GetMinValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))

                If MaxID_OnSever <> MaxID Then
                    'Get all the reports
                    MaxID = MaxID_OnSever
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS
                    FailureReportDatabaseLoad()

                End If

                'Create a new row in the Failure Report data table - This is handled in state machine
                gMyFailureReportDataTable.Rows.Add(gMyFailureReportDataTable.NewRow())

                'increment the NEW ID in the data table and set any default values
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("New ID") = (MaxID + 1).ToString
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("Anomaly") = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkFR_ReadyForReview.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkTCC_ApprovalRequired.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(ChkFR_Approved.Tag) = "Unchecked"


                'increment the position of the binding source (i'm not sure I need this)
                gMyFailureReportBindingSource.Position = MaxID + 1

                'end edit on all binding sources to create new record
                gMyFailureReportBindingSource.EndEdit()

                ' 'Not  sure Ineed this either - FJB
                ' Me.EndEditOnAllBindingSources()

                'Force a control Validation
                Me.Validate()

                'This Sends the changes out to the remote database
                gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

                'clear contents of local datatable
                gMyFailureReportDataTable.Clear()

                'Now refresh the datatable based on setting in options...
                If tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Checked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS Then
                    'refresh open reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS
                    '  FailureReportDatabaseLoad()
                ElseIf tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Unchecked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS Then
                    'refresh All Reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS
                    ' FailureReportDatabaseLoad()
                Else
                    'Resyncronize local datatable without reloading the Database
                    ' gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)
                End If



                FailureReportDatabaseLoad()

                'Sort the data 
                dgvFailureReportDataGridView.Sort(dgvFailureReportDataGridView.Columns("New ID"), ComponentModel.ListSortDirection.Descending)

                'set the position at the new failure report
                gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MaxID + 1)


                'Failure_ReportDataGridView.Rows(gMyFailureReportDataTable.Rows.Count - 1).Selected = True

                'This is handled in state machine
                'Failure_ReportDataGridView.ReadOnly = True
                'Failure_ReportDataGridView.Enabled = True


                'Me.cbTestName.Text = ""
                'Me.txtCorrectedBy.Text = ""

                Me.dtpDateCorrected.CustomFormat = " "
                Me.dtpDateClosed.CustomFormat = " "
                Me.dtpDateApproved.CustomFormat = " "
                Me.OpenFileDialog1.Reset()
                Me.txtAttachments.Text = ""

                'default to Failure report....
                chkFail.Checked = True
                chkPass.Checked = False

                'Populate text poistion manually
                txtPosition.Text = (MaxID + 1).ToString

                'Change Edit State - Move to Top 2.10.2017
                Dim NewState As eEditState
                NewState = eEditState.EDIT_CREATE_NEW
                StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)

                gb_ProcessEvents = True

            End If

        Catch ex As Exception
            gb_ProcessEvents = True
            MsgBox("Error Entering 'Create New Report Mode'" + vbCrLf + ex.ToString)
        End Try

    End Sub
    Class cCopyFailureReport
        Friend Function copy(ByVal globalState As eEditState) As Boolean
            Dim Success As Boolean = True
            If globalState = eEditState.READ_ONLY Then

            End If
            Return Success
        End Function

    End Class

    Private Sub tsmCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileCopy.Click
        Try

            'Verify that this is the correct state...Only allowed to goto New from ReadOnly State
            If _EditState = eEditState.READ_ONLY Then



                'First get the currentindex...need this so we know what to copy...
                Dim CurrentRowIndex As Integer

                If dgvFailureReportDataGridView.CurrentRow Is Nothing Then

                    Try
                        CurrentRowIndex = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
                    Catch ex As Exception
                        MsgBox("No Failure Report detected to copy; Switch to Gridview and select a Failure Report")
                        Exit Sub
                    End Try


                Else
                    CurrentRowIndex = dgvFailureReportDataGridView.CurrentRow.Index
                End If



                'Query Max and Min ID and the Current Selected row
                Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing)
                'Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([New ID])", Nothing)


                Dim MaxID_OnSever As Integer = Val(gMyCustomDBAccess.GetMaxValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))
                'MinID = Val(gMyCustomDBAccess.GetMinValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))

                'Get all of the reports
                If MaxID_OnSever <> MaxID Then
                    'Get all the reports
                    MaxID = MaxID_OnSever 'test to see if I need to do this...
                    ' _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS
                    ' FailureReportDatabaseLoad()
                End If

               

                'Create a new row in the Failure Report data table - This is handled in state machine
                gMyFailureReportDataTable.Rows.Add(gMyFailureReportDataTable.NewRow())

                'increment the NEW ID in the data table and set any default values
                'header
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("New ID") = (MaxID + 1).ToString
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("Anomaly") = gMyFailureReportDataTable.Rows(CurrentRowIndex)("Anomaly")
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkFR_ReadyForReview.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkTCC_ApprovalRequired.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(chkTCC_ApprovalRequired.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(ChkFR_Approved.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbProjectNumber.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbProjectNumber.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtProject.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtProject.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbProjectLead.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbProjectLead.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestedBy.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestedBy.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtTest.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtTest.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(mtxtDateFailed.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(mtxtDateFailed.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestLevel.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestLevel.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbEUTType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbEUTType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkTCC_ApprovalRequired.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(chkTCC_ApprovalRequired.Tag)
                'Meter /EUT 1 Info
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterManufacturer.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterManufacturer.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterModel.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterModel.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSubType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSubType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSubTypeII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSubTypeII.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterDSP_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterDSP_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterFW_Ver.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterFW_Ver.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterPCBA.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterPCBA.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterPCBA_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterPCBA_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSoftware.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSoftware.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSoftwareRev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSoftwareRev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtMeterNotes.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtMeterNotes.Tag)
                'AMR / EUT2 Info
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Manufacturer.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Manufacturer.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Model.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Model.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_TYPE.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_TYPE.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SubType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SubType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SUBtypeII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SUBtypeII.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SUBtypeIII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SUBtypeIII.Tag)
                'gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_IP_LAN_ID.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_IP_LAN_ID.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_FW_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_FW_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_PCBA.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_PCBA.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_PCBA_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_PCBA_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Software.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Software.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Software_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Software_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtAMR_Notes.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtAMR_Notes.Tag)

                'Failure Description
                'Changed to RTF here to acces to "Lines" Class
                'CRLF was not being preserved ...
                Dim rtfMyFailureReportDesciption As RichTextBox = New RichTextBox
                Dim strMyFailureReportDescription As String = ""
                'Dim MyFailureReportDescriptionSubString As String
                Dim bSuccess As Boolean = True

                '*******REMOVE*******1
                ' 'Copy the description
                ' If tmsIncludeOriginalAuthorOnCopy.CheckState = CheckState.Unchecked Then
                '     Try ' to replace with current user  
                '         ' this code Presumes that the Original Auther is on the Fist line, but somtimes this is blank.
                '         '                                                           Row Number       Column Name             convert to String   'Sub string starting at first Carriage Return/Line Feed      
                '         rtfMyFailureReportDesciption.Text = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag)

                '         'Recover vbCRLF into to string from richtextbox...
                '         'there must be a better way!
                '         For Each line In rtfMyFailureReportDesciption.Lines
                '             strMyFailureReportDescription = strMyFailureReportDescription + line + vbCrLf
                '         Next

                '         'Try extracting the first line...trouble shooting
                '         MyFailureReportDescriptionSubString = strMyFailureReportDescription.Substring(0, strMyFailureReportDescription.IndexOf("--->"))
                '         'elimiante everything to the first "--->"
                '         strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf("--->") + 4)
                '         'No eliminate the rest of the line up to the vbCRLF...
                '         strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf(vbCrLf) + 2)

                '         'no longer required...
                '         'While MyFailureReportDescriptionSubString.StartsWith("--->") Or MyFailureReportDescriptionSubString.StartsWith("---->") Or MyFailureReportDescriptionSubString.StartsWith("-->")
                '         '    '                                                           Row Number       Column Name             convert to String   'Sub string starting at first Carriage Return/Line Feed      
                '         '    strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf("--->") + 4)
                '         '    'extract the Fist line again...
                '         '    MyFailureReportDescriptionSubString = strMyFailureReportDescription.Substring(0, strMyFailureReportDescription.IndexOf(vbCrLf))
                '         'End While

                '         'now replace with current user...
                '         strMyFailureReportDescription = "---> " + _CurrentUser.FirstName + " " + _CurrentUser.LastName + " " + Now.ToString + vbCrLf + vbCrLf + vbCrLf + strMyFailureReportDescription
                '     Catch ex As Exception ' else append and preserve
                '         strMyFailureReportDescription = "Copied By " + _CurrentUser.FirstName + " " + _
                '         _CurrentUser.LastName(+" " + Now.ToString + vbCrLf + gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag))
                '         bSuccess = False
                '     End Try
                ' Else 'append and preserve...
                '     strMyFailureReportDescription = "Copied By " + _CurrentUser.FirstName + " " + _
                '_CurrentUser.LastName + " " + Now.ToString + vbCrLf + gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag)
                '     bSuccess = False
                ' End If

                ''move to after endedit so that it is editable...

                'If bSuccess = True Then
                '    'do this later
                '    'only insert current user
                '    ' gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtFailureDescription.Tag) = "---> " + _CurrentUser.FirstName + " " + _CurrentUser.LastName + " " + Now.ToString + vbCrLf
                'Else
                '    gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtFailureDescription.Tag) = strMyFailureReportDescription
                'End If



                'Test Equipment Used
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtTestEquipmentIDlist.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtTestEquipmentIDlist.Tag)

                'end edit on all binding sources to create new record
                gMyFailureReportBindingSource.EndEdit()

                'Force a control Validation
                Me.Validate()
                'This Sends the changes out to the remote database
                gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

                'clear contents of local datatable
                gMyFailureReportDataTable.Clear()

                'Now refresh the datatable based on setting in options...
                If tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Checked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS Then
                    'Set Flag to refresh *open* reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS

                ElseIf tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Unchecked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS Then
                    'Set flag to refresh *All* Reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS

                End If

                'Resyncronize local datatable
                'gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)
                FailureReportDatabaseLoad()
                'Sort the data 
                dgvFailureReportDataGridView.Sort(dgvFailureReportDataGridView.Columns("New ID"), ComponentModel.ListSortDirection.Descending)

                'set the position at the new failure report
                gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MaxID + 1)


                'Failure_ReportDataGridView.Rows(gMyFailureReportDataTable.Rows.Count - 1).Selected = True

                'This is handled in state machine
                'Failure_ReportDataGridView.ReadOnly = True
                'Failure_ReportDataGridView.Enabled = True


                'Me.cbTestName.Text = ""
                'Me.txtCorrectedBy.Text = ""

                Me.dtpDateCorrected.CustomFormat = " "
                Me.dtpDateClosed.CustomFormat = " "
                Me.dtpDateApproved.CustomFormat = " "
                Me.OpenFileDialog1.Reset()
                Me.txtAttachments.Text = ""

                'If chkAnomely.Checked = True Then
                '    tpDescription.Text = "ANOMALY DESCRIPTION"
                'Else
                '    tpDescription.Text = "FAILURE DESCRIPTION"
                'End If

                'Populate text poistion manually
                txtPosition.Text = (MaxID + 1).ToString

                'Change Edit State - Move to Top 2.10.2017
                Dim NewState As eEditState
                NewState = eEditState.EDIT_COPY
                StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)

                If bSuccess Then
                    rtxtDescription.Select(0, 0)
                    rtxtDescription.Text = strMyFailureReportDescription
                    rtxtDescription.Focus()
                    'rtxtFailureDescription.Select(strMyFailureReportDescription.IndexOf(vbCrLf) + 2, 0)

                End If


                gb_ProcessEvents = True

            End If

        Catch ex As Exception
            gb_ProcessEvents = True
            MsgBox("Error Entering 'Copty to New Report Mode'" + vbCrLf + ex.ToString)
        End Try
    End Sub

    Private Sub TransferFailureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileTransfer.Click
        Try

            'Step 



            'Verify that this is the correct state...Only allowed to goto New from ReadOnly State
            If _EditState = eEditState.READ_ONLY Then

                'get current index first
                Dim CurrentRowIndex As Integer

                If dgvFailureReportDataGridView.CurrentRow Is Nothing Then

                    Try
                        CurrentRowIndex = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
                    Catch ex As Exception
                        MsgBox("No Failure Report detected to copy; Switch to Gridview and select a Failure Report")
                        Exit Sub
                    End Try


                Else
                    CurrentRowIndex = dgvFailureReportDataGridView.CurrentRow.Index
                End If

                'Query Max and Min ID and the Current Selected row
                Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing)
                'Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([New ID])", Nothing)


                Dim MaxID_OnSever As Integer = Val(gMyCustomDBAccess.GetMaxValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))
                'MinID = Val(gMyCustomDBAccess.GetMinValueFromDatabaseColumn(gMyFailureReportDBConnection, "New ID", "Failure Report"))

                If MaxID_OnSever <> MaxID Then
                    'Get all the reports
                    MaxID = MaxID_OnSever
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS
                    FailureReportDatabaseLoad()
                End If



                'Create a new row in the Failure Report data table - This is handled in state machine
                gMyFailureReportDataTable.Rows.Add(gMyFailureReportDataTable.NewRow())

                'Preserve Current Report Number and Project Number
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtOrginalReportNum.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtReportNumber.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtOrginalProjectNum.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbProjectNumber.Tag)
                'increment the NEW ID in the data table and set any default values
                'header
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("New ID") = (MaxID + 1).ToString
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)("Anomaly") = gMyFailureReportDataTable.Rows(CurrentRowIndex)("Anomaly")
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkFR_ReadyForReview.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkTCC_ApprovalRequired.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(chkTCC_ApprovalRequired.Tag)
                'gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(ChkFR_Approved.Tag) = "Unchecked"
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbProjectNumber.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbProjectNumber.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtProject.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtProject.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbProjectLead.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbProjectLead.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestedBy.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestedBy.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtTest.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtTest.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(mtxtDateFailed.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(mtxtDateFailed.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbTestLevel.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbTestLevel.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbEUTType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbEUTType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(chkTCC_ApprovalRequired.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(chkTCC_ApprovalRequired.Tag)
                'Meter /EUT 1 Info
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterManufacturer.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterManufacturer.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterModel.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterModel.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSubType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSubType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSubTypeII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSubTypeII.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterDSP_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterDSP_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterFW_Ver.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterFW_Ver.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterPCBA.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterPCBA.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterPCBA_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterPCBA_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSoftware.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSoftware.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbMeterSoftwareRev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbMeterSoftwareRev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtMeterNotes.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtMeterNotes.Tag)
                'AMR / EUT2 Info
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Manufacturer.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Manufacturer.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Model.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Model.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_TYPE.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_TYPE.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SubType.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SubType.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SUBtypeII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SUBtypeII.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_SUBtypeIII.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_SUBtypeIII.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_IP_LAN_ID.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_IP_LAN_ID.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_FW_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_FW_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_PCBA.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_PCBA.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_PCBA_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_PCBA_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Software.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Software.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(cbAMR_Software_Rev.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(cbAMR_Software_Rev.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtAMR_Notes.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtAMR_Notes.Tag)
                'Failure Report Body
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtDescription.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtDescription.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtCorrectiveAction.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtCorrectiveAction.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtEngineeringNotes.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtEngineeringNotes.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(txtAttachments.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtAttachments.Tag)
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtTCC_Comments.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtTCC_Comments.Tag)

                'Failure Description
                'Changed to RTF here to acces to "Lines" Class
                'CRLF was not being preserved ...
                ' Dim rtfMyFailureReportDesciption As RichTextBox = New RichTextBox
                ' Dim strMyFailureReportDescription As String = ""
                'Dim MyFailureReportDescriptionSubString As String
                Dim bSuccess As Boolean = True


                Try

                    'Check to see if the Reprot being copoed has an attachment
                    If gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtAttachments.Tag).ToString.Trim = "" Then
                        MsgBox("No Attachment Detected")
                    Else
                        Dim Impersonator As New clsAuthenticator 'used for Read Write access to HQA File server
                        'impersaonate 'gelab' to get get read write access to HQA File Server
                        Impersonator.Start()

                        Try

                            Dim FR_ZIP_PATH As String

                            'Set the folder where are the Failure report attachement folder is located...
                            FR_ZIP_PATH = Path.GetDirectoryName(My.Settings.AttachmentFolder.Trim)
                            'Add the name of the specifc zip folder  "ID + ### + ".zip"
                            Dim ZipFolderName As String = "ID" + (MaxID + 1).ToString + ".zip"
                            'Convert a Mapped Drive Pathe to a UNC path
                            'This is currently hard coded in multiple locations...it should probably be in a table in the database...o
                            FR_ZIP_PATH = ConvertMappedFilePathToUNCPath(FR_ZIP_PATH + "\Attachments\" & ZipFolderName)
                            txtAttachments.Text = FR_ZIP_PATH

                            My.Computer.FileSystem.CopyFile(gMyFailureReportDataTable.Rows(CurrentRowIndex)(txtAttachments.Tag).ToString.Trim, FR_ZIP_PATH, overwrite:=True)
                            MessageBox.Show("Attachment copied successfully!")
                        Catch ex As Exception
                            MessageBox.Show("Error copying file: " & ex.Message)
                        End Try
                        Impersonator.Undo()
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error detecting Attachment: " & ex.Message)
                End Try
                '*******REMOVE*******1
                ' 'Copy the description
                ' If tmsIncludeOriginalAuthorOnCopy.CheckState = CheckState.Unchecked Then
                '     Try ' to replace with current user  
                '         ' this code Presumes that the Original Auther is on the Fist line, but somtimes this is blank.
                '         '                                                           Row Number       Column Name             convert to String   'Sub string starting at first Carriage Return/Line Feed      
                '         rtfMyFailureReportDesciption.Text = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag)

                '         'Recover vbCRLF into to string from richtextbox...
                '         'there must be a better way!
                '         For Each line In rtfMyFailureReportDesciption.Lines
                '             strMyFailureReportDescription = strMyFailureReportDescription + line + vbCrLf
                '         Next

                '         'Try extracting the first line...trouble shooting
                '         MyFailureReportDescriptionSubString = strMyFailureReportDescription.Substring(0, strMyFailureReportDescription.IndexOf("--->"))
                '         'elimiante everything to the first "--->"
                '         strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf("--->") + 4)
                '         'No eliminate the rest of the line up to the vbCRLF...
                '         strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf(vbCrLf) + 2)

                '         'no longer required...
                '         'While MyFailureReportDescriptionSubString.StartsWith("--->") Or MyFailureReportDescriptionSubString.StartsWith("---->") Or MyFailureReportDescriptionSubString.StartsWith("-->")
                '         '    '                                                           Row Number       Column Name             convert to String   'Sub string starting at first Carriage Return/Line Feed      
                '         '    strMyFailureReportDescription = strMyFailureReportDescription.Substring(strMyFailureReportDescription.IndexOf("--->") + 4)
                '         '    'extract the Fist line again...
                '         '    MyFailureReportDescriptionSubString = strMyFailureReportDescription.Substring(0, strMyFailureReportDescription.IndexOf(vbCrLf))
                '         'End While

                '         'now replace with current user...
                '         strMyFailureReportDescription = "---> " + _CurrentUser.FirstName + " " + _CurrentUser.LastName + " " + Now.ToString + vbCrLf + vbCrLf + vbCrLf + strMyFailureReportDescription
                '     Catch ex As Exception ' else append and preserve
                '         strMyFailureReportDescription = "Copied By " + _CurrentUser.FirstName + " " + _
                '         _CurrentUser.LastName(+" " + Now.ToString + vbCrLf + gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag))
                '         bSuccess = False
                '     End Try
                ' Else 'append and preserve...
                '     strMyFailureReportDescription = "Copied By " + _CurrentUser.FirstName + " " + _
                '_CurrentUser.LastName + " " + Now.ToString + vbCrLf + gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtFailureDescription.Tag)
                '     bSuccess = False
                ' End If

                ''move to after endedit so that it is editable...

                'If bSuccess = True Then
                '    'do this later
                '    'only insert current user
                '    ' gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtFailureDescription.Tag) = "---> " + _CurrentUser.FirstName + " " + _CurrentUser.LastName + " " + Now.ToString + vbCrLf
                'Else
                '    gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtFailureDescription.Tag) = strMyFailureReportDescription
                'End If



                'Test Equipment Used
                gMyFailureReportDataTable.Rows(gMyFailureReportDataTable.Rows.Count - 1)(rtxtTestEquipmentIDlist.Tag) = gMyFailureReportDataTable.Rows(CurrentRowIndex)(rtxtTestEquipmentIDlist.Tag)

                'end edit on all binding sources to create new record
                gMyFailureReportBindingSource.EndEdit()

                'Force a control Validation
                Me.Validate()
                'This Sends the changes out to the remote database
                gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

                'clear contents of local datatable
                gMyFailureReportDataTable.Clear()

                'Now refresh the datatable based on setting in options...
                If tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Checked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS Then
                    'Set Flag to refresh *open* reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS

                ElseIf tsmOptionsOnlyReturnOpenReports.CheckState = CheckState.Unchecked And _FR_SQL_Commands <> sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS Then
                    'Set flag to refresh *All* Reports
                    _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS

                End If

                'Resyncronize local datatable
                'gMyFailureReportDBDataAdaptor.Fill(gMyFailureReportDataTable)
                FailureReportDatabaseLoad()
                'Sort the data 
                dgvFailureReportDataGridView.Sort(dgvFailureReportDataGridView.Columns("New ID"), ComponentModel.ListSortDirection.Descending)

                'set the position at the new failure report
                gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MaxID + 1)


                'Failure_ReportDataGridView.Rows(gMyFailureReportDataTable.Rows.Count - 1).Selected = True

                'This is handled in state machine
                'Failure_ReportDataGridView.ReadOnly = True
                'Failure_ReportDataGridView.Enabled = True


                'Me.cbTestName.Text = ""
                'Me.txtCorrectedBy.Text = ""

                Me.dtpDateCorrected.CustomFormat = " "
                Me.dtpDateClosed.CustomFormat = " "
                Me.dtpDateApproved.CustomFormat = " "
                Me.OpenFileDialog1.Reset()
                Me.txtAttachments.Text = ""

                'If chkAnomely.Checked = True Then
                '    tpDescription.Text = "ANOMALY DESCRIPTION"
                'Else
                '    tpDescription.Text = "FAILURE DESCRIPTION"
                'End If

                'Populate text poistion manually
                txtPosition.Text = (MaxID + 1).ToString

                'Change Edit State - Move to Top 2.10.2017
                Dim NewState As eEditState
                NewState = eEditState.EDIT_COPY
                StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)

                If bSuccess Then
                    'rtxtDescription.Select(0, 0)
                    'rtxtDescription.Text = strMyFailureReportDescription
                    'rtxtDescription.Focus()
                    'rtxtFailureDescription.Select(strMyFailureReportDescription.IndexOf(vbCrLf) + 2, 0)

                End If


                gb_ProcessEvents = True

            End If

        Catch ex As Exception
            gb_ProcessEvents = True
            MsgBox("Error Entering 'Copty to New Report Mode'" + vbCrLf + ex.ToString)
        End Try
    End Sub

    Private Sub tsmFileLoginAsDifferentUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileLoginAsDifferentUser.Click
        Dim OldUser As New User("", "", "", eAccessState.READ_ONLY, eApproverDiscipline.NONE)
        OldUser.AccessLevel = _CurrentUser.AccessLevel
        OldUser.Login = _CurrentUser.Login
        OldUser.Password = _CurrentUser.Password
        OldUser.FirstName = _CurrentUser.FirstName
        Dim MyLoginForm As New frmLogin
        MyLoginForm.StartPosition = FormStartPosition.CenterParent
        Me.gb_ProcessEvents = False
        MyLoginForm.ShowDialog()


        If OldUser.AccessLevel > _CurrentUser.AccessLevel Then
            'if the new access rights are less than the old
            'access rights than safely transistion to readonly state

            'Set the _RecordSaved flag to true to prevent a Nag prompt
            _RecordSaved = True


            If _EditState = eEditState.EDIT_CREATE_NEW Then
                tsmFileCancel.PerformClick()
            End If

            'SetEditState(eAccessState.READ_ONLY)
            Dim NewState As eEditState = eEditState.READ_ONLY
            StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
        Else
            'SetEditState(_EditState)
            Dim NewState As eEditState = _EditState 'Keep the same
            StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
        End If

        Me.gb_ProcessEvents = True
    End Sub
    Private Sub tsmFileEditFailureDesciption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileEditFailureDesciption.Click
        'SetEditState(eAccessState.CR_EDIT)
        Dim NewState As eEditState = eEditState.EDIT_FR_DETAILS
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub tsmFileEditCR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileEditCR.Click
        'SetEditState(eAccessState.CR_EDIT)
        Dim NewState As eEditState = eEditState.EDIT_FR_CORRECTIVE_ACTION
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub tsmFileEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileEdit.Click
        'SetEditState(eAccessState.CR_EDIT)
        Dim NewState As eEditState = eEditState.EDIT_POWER
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub tsmFileEnterApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileEnterApproval.Click
        'SetEditState(eAccessState.CR_EDIT)
        Dim NewState As eEditState = eEditState.EDIT_SET_APPROVALS
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub tsmFileAdminEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileAdminEdit.Click
        'SetEditState(eAccessState.CR_EDIT)
        Dim NewState As eEditState = eEditState.EDIT_ADMIN
        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub tsmFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileDelete.Click
        gb_ProcessEvents = False
        Try
            'Get the maximum and minimum Failure Report number (FR Boundry Conditions) 
            Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing)
            Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([New ID])", Nothing)

            'Varaible to hold the current position
            Dim ID As Integer
            'point CurrentRow at the Bound Row in the data set, this should always be the new 
            'row that was added because the navigation of the form should be locked out
            'i.e. - the gridview is readonly, the Navigation buttons are disabled
            If dgvFailureReportDataGridView.CurrentRow Is Nothing Then
                gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
            Else
                gMyFailureReportBindingSource.Position = dgvFailureReportDataGridView.CurrentRow.Index
            End If


            'Cast the new row fro mthe binding source to DataRowView to ease minipulation 
            Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
            Dim bUpdateTheRecord = True
            If _EditState = eEditState.EDIT_ADMIN Then
                If MsgBox("The current record, FR" + txtPosition.Text + ", will be permenently deleted. Is that OK?", vbYesNo, "OK") = vbYes Then
                    'database call and delete

                    CurrentRow.Delete()
                    'This is necessary to leave edit mode without another nag window in change edit mode
                    _RecordSaved = True
                Else
                    bUpdateTheRecord = False
                End If

            End If

            If bUpdateTheRecord Then

                'If _EditState = eEditState.CREATE_NEW Then

                'End If
                ' 'This is necessary to leave edit mode without another nag window
                ' _RecordSaved = True

                'added these lines to get rid of concurrancy error.
                Validate()

                'Notification to Binding source to commit changes
                gMyFailureReportBindingSource.EndEdit()

                'update the database
                gMyFailureReportDBDataAdaptor.Update(gMyFailureReportDataTable)

                'should a Datatable.clear followed by a datatable fill be called here?  -FJB


                'Set to most recently added failure report
                ID = gMyFailureReportDataTable.Compute("MAX([New ID])", Nothing) 'CInt((Val(txtPosition.Text - 1)))
                If ID > MaxID Or ID < MinID Then
                    'do nothing right now, handle error...

                Else
                    txtPosition.Text = ID.ToString
                    gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
                End If

                'Revert to ReadOnly
                'ChangeEditMode(eEditState.READ_ONLY)

                StateMachine(eEditState.READ_ONLY, _EditState, _CurrentUser.AccessLevel)


            End If
        Catch ex As Exception
            gb_ProcessEvents = True
            MsgBox("tsmDelete_Click Error" + vbCrLf + ex.ToString)
        End Try
        gb_ProcessEvents = True
        'Set Controls based on if TCC approval is required
        ' SetApproverState()
    End Sub
    Private Sub tsmFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileExit.Click
        Me.Close()
    End Sub
    Private Sub tsmFileChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFileChangePassword.Click
        Dim MyChangePasswordForm As New frmChangePassWord
        MyChangePasswordForm.StartPosition = FormStartPosition.CenterParent
        MyChangePasswordForm.ShowDialog()
        If MyChangePasswordForm.DialogResult = Windows.Forms.DialogResult.OK Then
            MsgBox("Password Changed Successfully")
        Else
            v_UpdateLog("Error Changing Password" + vbCrLf + MyChangePasswordForm.Tag, eLogLevel.ERRORS_ONLY)
        End If
    End Sub
#End Region '"Files"
#Region "PRINTING"
#Region "Print Grid Variables"
    ' Const strConnectionString As String = "data source=localhost;Integrated Security=SSPI;Initial Catalog=Northwind;"
    Dim _strFormat As StringFormat ' //Used to format the grid rows.
    Dim _arrColumnLefts As ArrayList = New ArrayList() ';//Used to save left coordinates of columns
    Dim _arrColumnWidths As ArrayList = New ArrayList() ';//Used to save column widths
    Dim _iCellHeight As Integer = 0 '; //Used to get/set the datagridview cell height
    Dim _iTotalWidth As Integer = 0 ' //
    Dim _iRow As Integer = 0 ';//Used as counter
    Dim _bFirstPage As Boolean = False '; //Used to check whether we are printing first page
    Dim _bNewPage As Boolean = False ';// Used to check whether we are printing a new page
    Dim iHeaderHeight As Integer = 0 '; //Used for the header height
#End Region
    ''' <summary>
    ''' Handles the print page event for the test Equipment datagridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Print_TestEquipment_DataGridview(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Try
            'UpdateAttachmentList()
            'Draw the Heading above upper margin with FR number and the PageNumber
            '  Dim HeaderRect As New RectangleF(e.MarginBounds.Left + 10, 10, e.MarginBounds.Width - 20, 15)
            '  Dim pg As New Drawing.StringFormat(StringAlignment.Center)
            ' e.Graphics.DrawString("Report #" & txtReportNumber.Text, lblReportNumber.Font, Brushes.Black, HeaderRect)
            '  e.Graphics.DrawString("Page " & _PageNum, lblReportNumber.Font, Brushes.Black, HeaderRect, pg)


            'Set the left margin
            Dim iLeftMargin As Integer = e.MarginBounds.Left
            'Set the top margin
            Dim iTopMargin As Integer = e.MarginBounds.Top
            'Whether more pages have to print or not
            Dim bMorePagesToPrint As Boolean = False
            Dim iTmpWidth As Integer = 0

            'For the first page to print set the cell width and header height
            If _bFirstPage Then
                For Each GridCol As DataGridViewColumn In dgvTestEquipmentUsed.Columns
                    iTmpWidth = CInt(Math.Floor(CDbl(CDbl(GridCol.Width) / CDbl(_iTotalWidth) * CDbl(_iTotalWidth) * (CDbl(e.MarginBounds.Width) / CDbl(_iTotalWidth)))))

                    iHeaderHeight = CInt(e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11

                    ' Save width and height of headres
                    _arrColumnLefts.Add(iLeftMargin)
                    _arrColumnWidths.Add(iTmpWidth)
                    iLeftMargin += iTmpWidth
                Next
            End If
            'Loop till all the grid rows not get printed
            While _iRow <= dgvTestEquipmentUsed.Rows.Count - 1
                Dim GridRow As DataGridViewRow = dgvTestEquipmentUsed.Rows(_iRow)
                'Set the cell height
                _iCellHeight = GridRow.Height + 5
                Dim iCount As Integer = 0
                'Check whether the current page settings allo more rows to print
                If iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top Then
                    _bNewPage = True
                    _bFirstPage = False
                    bMorePagesToPrint = True
                    Exit While
                Else
                    If _bNewPage Then
                        'Draw Header
                        e.Graphics.DrawString("Test Equipment Used", New Font(dgvTestEquipmentUsed.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary", New Font(dgvTestEquipmentUsed.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13)

                        Dim strDate As [String] = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString()
                        'Draw Date
                        e.Graphics.DrawString(strDate, New Font(dgvTestEquipmentUsed.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, New Font(dgvTestEquipmentUsed.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary", New Font(New Font(dgvTestEquipmentUsed.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13)

                        'Draw Columns                 
                        iTopMargin = e.MarginBounds.Top
                        For Each GridCol As DataGridViewColumn In dgvTestEquipmentUsed.Columns
                            e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), New Rectangle(CInt(_arrColumnLefts(iCount)), iTopMargin, CInt(_arrColumnWidths(iCount)), iHeaderHeight))

                            e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(_arrColumnLefts(iCount)), iTopMargin, CInt(_arrColumnWidths(iCount)), iHeaderHeight))

                            e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, New SolidBrush(GridCol.InheritedStyle.ForeColor), New RectangleF(CInt(_arrColumnLefts(iCount)), iTopMargin, CInt(_arrColumnWidths(iCount)), iHeaderHeight), _strFormat)
                            iCount += 1
                        Next
                        _bNewPage = False
                        iTopMargin += iHeaderHeight
                    End If
                    iCount = 0
                    'Draw Columns Contents                
                    For Each Cel As DataGridViewCell In GridRow.Cells
                        If Cel.Value IsNot Nothing Then
                            e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(CInt(_arrColumnLefts(iCount)), CSng(iTopMargin), CInt(_arrColumnWidths(iCount)), CSng(_iCellHeight)), _strFormat)
                        End If
                        'Drawing Cells Borders 
                        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(_arrColumnLefts(iCount)), iTopMargin, CInt(_arrColumnWidths(iCount)), _iCellHeight))

                        iCount += 1
                    Next
                End If
                _iRow += 1
                iTopMargin += _iCellHeight
            End While

            'If more lines exist, print another page.
            If bMorePagesToPrint Then
                e.HasMorePages = True
                _PageNum += 1
            Else

                _PrintState = 0
                e.HasMorePages = False
                _PageNum = 0
            End If
        Catch exc As Exception
            MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub tsmPrintPrintGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrintPrintGrid.Click
        'PrintPreviewDialog1.Document = PrintDocument1
        'PrintPreviewDialog1.ShowDialog()
        Dim Oldview As New eFailureReportView
        Oldview = _FailureReportView
        SetFR_VIEW(eFailureReportView.BROWSER_VIEW)
        Dim Printer = New DGVPrinter
        Printer.Title = "Failure Report List" + Now.ToString
        Printer.SubTitle = "DATE: " + Now.ToShortDateString + " USER: " + _CurrentUser.FirstName + " " + _CurrentUser.LastName
        Printer.SubTitleFormatFlags = StringFormatFlags.LineLimit Or StringFormatFlags.NoClip
        Printer.PageNumbers = True
        Printer.PageNumberInHeader = False
        Printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional
        Printer.HeaderCellAlignment = StringAlignment.Near
        Printer.Footer = "Landis+Gyr"
        Printer.FooterSpacing = 15
        If Printer.DisplayPrintDialog = Windows.Forms.DialogResult.OK Then
            'My.Settings.DGVPageSettings = Printer.PageSettings
            Printer.PrintNoDisplay(dgvFailureReportDataGridView)
        End If
        SetFR_VIEW(Oldview)
        'Printer.PrintDataGridView(Me.Failure_ReportDataGridView)
    End Sub
    Private Sub tsmPrintSubMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrintSubMenu.Click
        'Set in printview
        Dim View As New eFailureReportView
        View = _FailureReportView
        SetFR_VIEW(eFailureReportView.FR_VIEW)

        'Make sure the grid has been drawn...
        Dim BackupIndex As Integer = tcReportBody.SelectedIndex
        tcReportBody.SelectTab("tpTestEquipment")

        'Clear the Print Buffers
        _DescriptionString = ""
        _CorrectiveActionString = ""
        _EngineeringNotesString = ""
        _TCC_CommentsString = ""
        _Test_EquipmentString = ""

        'Fill the Print Buffers, Prefacing each with Header Text....
        _DescriptionString = "DESCRIPTION:" + vbCrLf + rtxtDescription.Text.Trim + vbCrLf + vbCrLf
        _CorrectiveActionString = "CORRECTIVE ACTION:" + vbCrLf + rtxtCorrectiveAction.Text.Trim + vbCrLf + vbCrLf
        _EngineeringNotesString = "ENGINEERING NOTES:" + vbCrLf + rtxtEngineeringNotes.Text.Trim + vbCrLf + vbCrLf
        _TCC_CommentsString = "TCC COMMENTS:" + vbCrLf + rtxtTCC_Comments.Text.Trim
        ' _Test_EquipmentString = "TEST EQUIPMENT USED:" + vbCrLf + GridviewToFormatedString(dgvTestEquipmentUsed, 0, 2, "COURIER NEW", 8.25, 2)



        PrintDocumentFailureReport.Print()
        tcReportBody.SelectTab(BackupIndex)
        SetFR_VIEW(View)
    End Sub
    Public Overloads Sub SetTxtBxHeight(ByRef TXT As TextBox)
        With TXT
            Dim sz As New Size(.GetPreferredSize(.Size))
            .Height = sz.Height + 8
        End With
    End Sub
    Public Overloads Sub SetTxtBxHeight(ByRef RichTxtBox As RichTextBox)
        With RichTxtBox
            Dim sz As New Size(.GetPreferredSize(.Size))
            .Height = sz.Height + 8
        End With
    End Sub
    Private Sub PrintingStateMachine(ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        'Create Header Text
        'Not Used

    End Sub

    Sub DimPrintPageNew(ByRef TopOffset As Integer, _
                        ByRef MyAdditionalOffset As Integer, _
                        ByRef LeftOffset As Integer, _
                        ByRef My_Y_Location As Integer, _
                        ByRef MyPrintArguments As System.Drawing.Printing.PrintPageEventArgs)


        Dim MyDescriptionRichTextBox As RichTextBox
        Dim DescriptionString As String
        Dim MyBoxColor As Pen = New Pen(System.Drawing.Color.Gray)
        Dim PrintFont As Font
        Dim rectDraw As RectangleF '(e.MarginBounds.Left, e.MarginBounds.Top + My_Y_Location, e.MarginBounds.Width, e.MarginBounds.Height - (e.MarginBounds.Top + My_Y_Location))
        Dim rectDraw2 As Rectangle '(e.MarginBounds.Left, e.MarginBounds.Top + My_Y_Location, e.MarginBounds.Width, e.MarginBounds.Height - (e.MarginBounds.Top + My_Y_Location))
        Dim sizeMeasure As SizeF '(e.MarginBounds.Width, e.MarginBounds.Height - PrintFont.GetHeight(e.Graphics))
        Dim strFormat As New StringFormat()
        Dim numChars As Integer
        Dim numLines As Integer
        Dim stringForPage As String


        'Must use at least one page increment each time this event is called during a printing instance....
        _PageNum += 1
        If _PrintState = 1 Then


            MyDescriptionRichTextBox = DirectCast(rtxtDescription, RichTextBox)
            DescriptionString = _DescriptionString
            While DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbCr Or DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbLf
                ' MsgBox("Remove vbCrLf")
                DescriptionString = DescriptionString.Substring(0, DescriptionString.Length - 1)
            End While
            'Now add one back
            DescriptionString = DescriptionString + vbCrLf
            'Size of the area that is left that may be printed on
            'rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, MyPrintArguments.MarginBounds.Top + My_Y_Location, MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location))
            '  rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, TopOffset + My_Y_Location, MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (TopOffset + My_Y_Location))
            'Set the Printfont to that used in the Failure Description
            'PrintFont = MyDescriptionRichTextBox.Font
            PrintFont = New Font(MyDescriptionRichTextBox.Font.FontFamily, MyDescriptionRichTextBox.Font.Size, FontStyle.Bold)
            'Measure Ho many characters and lines will fit in the print area
            'sizeMeasure = New SizeF(MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location) - PrintFont.GetHeight(MyPrintArguments.Graphics))
            '   sizeMeasure = New SizeF(MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (TopOffset + My_Y_Location)) ' - PrintFont.GetHeight(MyPrintArguments.Graphics))
            'Set the trim style
            strFormat.Trimming = StringTrimming.Word
            'Measure how much space is required to print the current string
            MyPrintArguments.Graphics.MeasureString(DescriptionString, PrintFont, sizeMeasure, strFormat, numChars, numLines)
            'Force to a minimum so that a box is drawn...
            If numLines < 2 Then
                numLines = 2
            End If
            If numLines * PrintFont.Height > rectDraw.Height Then
                rectDraw.Height = numLines * PrintFont.Height
            End If
            'Get the maximum number of characters that will fit in print area
            stringForPage = DescriptionString.Substring(0, numChars)
            'Print to the print area
            MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
            'Draw a Rectange around the Body of the Text
            ' rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyPrintArguments.MarginBounds.Width, (numLines - 1) * PrintFont.Height)
            '  rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, TopOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
            MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
            'Check to see if everything has been printed
            If numChars < DescriptionString.Length Then
                'There is still more to print so...
                'Remove Text that has allready been printed and Preface remaining text with new Header...
                _DescriptionString = "DESCRIPTION (CONT..):" + vbCrLf + DescriptionString.Substring(numChars)
                MyPrintArguments.HasMorePages = True 'Set flag so the event fires again
                _PrintState = 1 'Continue printing the Failure Description
                _PageNum += 1 ' increment page count
                ' GoTo END_OF_SUB 'Exit Sub
            Else
                'Everything from Text box has been printed so...
                'Advance the top of the next print area past the bottom of previous Print area....

                'My_Y_Location = My_Y_Location + (numLines + 1) * PrintFont.Height
                My_Y_Location = rectDraw2.Bottom + PrintFont.Height
                _PrintState = 2 'advance to Corrective Action
            End If


            '2:              'Corrective Action
        End If
    End Sub




    Private Sub PrintDocumentFailureReport_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocumentFailureReport.PrintPage


        'MsgBox(sender.ToString)
        'Variabled used for printing the Failure Report Body
        Dim numChars As Integer
        Dim numLines As Integer
        Dim stringForPage As String
        Dim strFormat As New StringFormat()
        Dim PrintFont As Font
        Dim rectDraw As RectangleF '(e.MarginBounds.Left, e.MarginBounds.Top + My_Y_Location, e.MarginBounds.Width, e.MarginBounds.Height - (e.MarginBounds.Top + My_Y_Location))
        Dim rectDraw2 As Rectangle '(e.MarginBounds.Left, e.MarginBounds.Top + My_Y_Location, e.MarginBounds.Width, e.MarginBounds.Height - (e.MarginBounds.Top + My_Y_Location))
        Dim sizeMeasure As SizeF '(e.MarginBounds.Width, e.MarginBounds.Height - PrintFont.GetHeight(e.Graphics))
        Dim MyPrintArguments As System.Drawing.Printing.PrintPageEventArgs = DirectCast(e, System.Drawing.Printing.PrintPageEventArgs)
        Dim MyRichTextBox As RichTextBox
        Dim DescriptionString As String
        Dim MyBoxColor As Pen = New Pen(System.Drawing.Color.Gray)

        ''May get rid of this
        'SetTxtBxHeight(txtFailureDescription)
        'tcReportBody.SelectedIndex = 1
        'SetTxtBxHeight(txtCorrectiveAction)
        'tcReportBody.SelectedIndex = 2
        'SetTxtBxHeight(txtEngineeringNotes)
        'tcReportBody.SelectedIndex = 4
        'SetTxtBxHeight(rtxtTCC_Comments)
        'tcReportBody.SelectedIndex = 0

        Try

            'Y location for Header Text
            Dim topOffset As Integer = MyPrintArguments.MarginBounds.Top - 80

            If topOffset < 20 Then
                topOffset = 20
            End If

            'Center unless left margin is Violated 
            Dim MyAdditionalOffset As Integer = ((MyPrintArguments.MarginBounds.Width - Me.Width) \ 2)

            If MyAdditionalOffset < 0 Then
                MyAdditionalOffset = 0
            End If
            Dim leftOffset As Integer = MyPrintArguments.MarginBounds.Left + MyAdditionalOffset

            'Dim TabY As Integer = tcReportBody.Location.Y + topOffset
            ' Dim drawRect As New Rectangle
            Dim My_Y_Location As Integer

            'Must use at least one page increment each time this event is called during a printing instance....
            _PageNum += 1

            'Added 4.162019 to tryto keep everytjing on the page -FJB
            Dim MyMarginsWidth As Integer = MyPrintArguments.MarginBounds.Width ' - 80
            'Draw the Heading above upper margin with FR number and the PageNumber ... Subtracted 
            Dim HeaderRect As New RectangleF(MyPrintArguments.MarginBounds.Left + 10, 10, MyMarginsWidth - 80, 15)
            Dim pg As New Drawing.StringFormat(StringAlignment.Center)
            MyPrintArguments.Graphics.DrawString("Report #" & txtReportNumber.Text, lblReportNumber.Font, Brushes.Black, HeaderRect)
            MyPrintArguments.Graphics.DrawString("Page " & _PageNum, lblReportNumber.Font, Brushes.Black, HeaderRect, pg)
            My_Y_Location = txtReportNumber.Location.Y + topOffset + 3


            If _PrintState = 0 Then



                'IF the page number is Greater than 1 then Skip Failure Report Header Code....
                If _PageNum > 1 And _PrintState = 0 Then
                    'advance State and exit case statement This will stop repeating the header info
                    _PrintState = 1
                    My_Y_Location = txtReportNumber.Location.Y + topOffset + 3
                Else 'print the header

                    Dim Myheader As frmReport = New frmReport
                    Myheader.Show()
                    'Failure Report Header...
                    'Loop through each control and selectively Print
                    ' For Each i As Control In pnlReportHeader.Controls
                    For Each i As Control In Myheader.Controls
                        If TypeOf i Is Label And i.Visible = True Then
                            Dim MyLabel As New Label
                            MyLabel = DirectCast(i, Label)

                            MyPrintArguments.Graphics.DrawString(MyLabel.Text, MyLabel.Font, Brushes.Black, MyLabel.Location.X + leftOffset, MyLabel.Location.Y + topOffset)
                            If MyLabel.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = MyLabel.Location.Y + topOffset
                            End If
                        End If

                        If TypeOf i Is TextBox And i.Visible = True Then
                            Dim MyTextBox As New TextBox
                            MyTextBox = DirectCast(i, TextBox)
                            Dim Myfont As Font = New Font(MyTextBox.Font.FontFamily, MyTextBox.Font.Size, FontStyle.Bold)

                            ' MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3)
                            rectDraw2 = New Rectangle(MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3, MyTextBox.Width, MyTextBox.Height)
                            MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                            'Set the trim style
                            strFormat.Trimming = StringTrimming.Word
                            MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, rectDraw2, strFormat)
                            If i.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset
                            End If

                        End If

                        If TypeOf i Is RichTextBox And i.Visible = True Then
                            Dim MyTextBox As New RichTextBox
                            MyTextBox = DirectCast(i, RichTextBox)
                            Dim Myfont As Font = New Font(MyTextBox.Font.FontFamily, MyTextBox.Font.Size, FontStyle.Bold)
                            'Set the trim style
                            strFormat.Trimming = StringTrimming.Word

                            rectDraw2 = New Rectangle(MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3, MyTextBox.Width, MyTextBox.Height)
                            MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                            'Set the trim style
                            strFormat.Trimming = StringTrimming.Word
                            MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, rectDraw2, strFormat)
                            ' MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3)
                            'Added the height of the text box as well since I know that the Notes fields areat the bottom of the header -Frank Boudreau....
                            If i.Location.Y + topOffset + MyTextBox.Height > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset + MyTextBox.Height
                            End If

                        End If


                        If TypeOf i Is MaskedTextBox And i.Visible = True Then
                            Dim MyTextBox As New MaskedTextBox
                            MyTextBox = DirectCast(i, MaskedTextBox)

                            Dim Myfont As Font = New Font(MyTextBox.Font.FontFamily, MyTextBox.Font.Size, FontStyle.Bold)
                            'MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3)
                            rectDraw2 = New Rectangle(MyTextBox.Location.X + leftOffset, MyTextBox.Location.Y + topOffset + 3, MyTextBox.Width, MyTextBox.Height)
                            MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                            'Set the trim style
                            strFormat.Trimming = StringTrimming.Word
                            MyPrintArguments.Graphics.DrawString(MyTextBox.Text, Myfont, Brushes.Black, rectDraw2, strFormat)
                            If i.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset
                            End If

                        End If

                        If TypeOf i Is xboXComboBox And i.Visible = True Then
                            Dim MyxboXComboBox As New xboXComboBox
                            MyxboXComboBox = DirectCast(i, xboXComboBox)
                            'Now do what ever...
                            MyPrintArguments.Graphics.DrawString(MyxboXComboBox.Text, MyxboXComboBox.Font, Brushes.Black, MyxboXComboBox.Location.X + leftOffset, MyxboXComboBox.Location.Y + topOffset + 3)
                            If i.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset
                            End If
                        End If

                        If TypeOf i Is DateTimePicker And i.Visible = True Then
                            Dim MyDateTimePicker As New DateTimePicker
                            MyDateTimePicker = DirectCast(i, DateTimePicker)
                            'Now do what ever...
                            MyPrintArguments.Graphics.DrawString(MyDateTimePicker.Text, MyDateTimePicker.Font, Brushes.Black, MyDateTimePicker.Location.X + leftOffset, MyDateTimePicker.Location.Y + topOffset + 3)
                            If i.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset
                            End If
                        End If

                        If TypeOf i Is CheckBox And i.Visible = True Then
                            Dim MyCheckBox As New CheckBox
                            MyCheckBox = DirectCast(i, CheckBox)
                            If MyCheckBox.Name <> "Shadow" + chkShowDelegates.Name Then ' Do not print chkShowDelegates
                                'MyPrintArguments.Graphics.DrawString(MyCheckBox.Text, lblReportNumber.Font, Brushes.Black, MyCheckBox.Location.X + leftOffset, MyCheckBox.Location.Y + topOffset)
                                'MyPrintArguments.Graphics.DrawRectangle(Pens.Black, MyCheckBox.Location.X + leftOffset + 110, MyCheckBox.Location.Y + topOffset + 3, 10, 10)
                                If MyCheckBox.CheckState = CheckState.Checked Then
                                    MyPrintArguments.Graphics.DrawString(MyCheckBox.Text + "?: Yes", lblReportNumber.Font, Brushes.Black, MyCheckBox.Location.X + leftOffset, MyCheckBox.Location.Y + topOffset)
                                ElseIf MyCheckBox.CheckState = CheckState.Unchecked Then
                                    MyPrintArguments.Graphics.DrawString(MyCheckBox.Text + "?: No", lblReportNumber.Font, Brushes.Black, MyCheckBox.Location.X + leftOffset, MyCheckBox.Location.Y + topOffset)
                                Else
                                    MyPrintArguments.Graphics.DrawString(MyCheckBox.Text + "?: Ind", lblReportNumber.Font, Brushes.Black, MyCheckBox.Location.X + leftOffset, MyCheckBox.Location.Y + topOffset)
                                    'MyPrintArguments.Graphics.DrawLine(Pens.Black, MyCheckBox.Location.X + leftOffset + 110, MyCheckBox.Location.Y + topOffset + 3, MyCheckBox.Location.X + leftOffset + 120, MyCheckBox.Location.Y + topOffset + 13)
                                    'MyPrintArguments.Graphics.DrawLine(Pens.Black, MyCheckBox.Location.X + leftOffset + 110, MyCheckBox.Location.Y + topOffset + 13, MyCheckBox.Location.X + leftOffset + 120, MyCheckBox.Location.Y + topOffset + 3)
                                    'If i.Location.Y + topOffset > My_Y_Location Then
                                    '    My_Y_Location = i.Location.Y + topOffset
                                    'End If
                                End If
                            End If
                            If i.Location.Y + topOffset > My_Y_Location Then
                                My_Y_Location = i.Location.Y + topOffset
                            End If
                        End If

                    Next


                    'now advance the state
                    _PrintState = 1
                    Myheader.Close()
                    Myheader.Dispose()
                End If

            End If

            '1:      'Failure Description
            If _PrintState = 1 Then
                '  _PageNum = 0
                '   Exit Sub
                'Create pointer to curent Text box... used to get Font data...
                MyRichTextBox = DirectCast(rtxtDescription, RichTextBox)
                'Create pointer to class level String data to print....
                DescriptionString = _DescriptionString
                'Remove leading carraige Return or LineFeed....
                While DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbCr Or DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbLf
                    ' MsgBox("Remove vbCrLf")
                    DescriptionString = DescriptionString.Substring(0, DescriptionString.Length - 1)
                End While
                'Now add one back to the end....
                DescriptionString = DescriptionString + vbCrLf
                'Size of the area that is left that may be printed on
                'rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, MyPrintArguments.MarginBounds.Top + My_Y_Location, MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location))
                rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location, MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location))
                'Set the Printfont to that used in the Failure Description
                PrintFont = New Font(MyRichTextBox.Font.FontFamily, MyRichTextBox.Font.Size, FontStyle.Bold)
                'Measure Ho many characters and lines will fit in the print area
                'sizeMeasure = New SizeF(MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location) - PrintFont.GetHeight(MyPrintArguments.Graphics))
                sizeMeasure = New SizeF(MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location)) ' - PrintFont.GetHeight(MyPrintArguments.Graphics))
                'Set the trim style
                strFormat.Trimming = StringTrimming.Word
                'Measure how much space is required to print the current string
                MyPrintArguments.Graphics.MeasureString(DescriptionString, PrintFont, sizeMeasure, strFormat, numChars, numLines)

                'some how a negative height is being returned... so set to zero,  not sure why Negative print area does not casue problems - FJB 4.17.2019
                If sizeMeasure.Height < 0 Then
                    numChars = 0
                End If

                ''Force to a minimum so that a box is drawn...
                'If numLines < 2 Then
                '    numLines = 2
                'End If
                'If numLines * PrintFont.Height > rectDraw.Height Then
                '    rectDraw.Height = numLines * PrintFont.Height
                'End If



                'Draw a Rectangle around the Body of the Text if there is text in it... Box is drawn below the header...if only the header would be drawn then skip it...
                If numChars = 0 Then
                    'Don't print anything don't draw a box....
                ElseIf numLines = 1 And numChars = DescriptionString.Length Then
                    'Print If just the header...
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Still Size it...But don;' draw it...used for advancing location of text...
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                ElseIf numLines = 1 Then
                    'Don't print anything
                ElseIf numLines > 1 Then
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Size it
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                    'draw it
                    MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                End If


                'Check to see if everything has been printed
                'If everything has not been printed  
                'Or only the header would have been drawn
                If (numChars < DescriptionString.Length) Or numLines = 1 Then
                    'There is still more to print so...
                    'Remove Text that has allready been printed and Preface remaining text with new Header...
                    If InStr(DescriptionString, "DESCRIPTION:") Then
                        ' In this case Nothing has been written yet...so do not Change
                        ' _FailureDesciptionString = DescriptionString.Substring(numChars)
                    Else

                        _DescriptionString = "DESCRIPTION (CONT..):" + vbCrLf + DescriptionString.Substring(numChars)
                    End If

                    MyPrintArguments.HasMorePages = True 'Set flag so the event fires again
                    _PrintState = 1 'Continue printing the Failure Description
                    '    _PageNum += 1 ' increment page count
                    ' GoTo END_OF_SUB 'Exit Sub
                Else
                    'Everything from Text box has been printed so...
                    'Advance the top of the next print area past the bottom of previous Print area....

                    'My_Y_Location = My_Y_Location + (numLines + 1) * PrintFont.Height
                    My_Y_Location = rectDraw2.Bottom + PrintFont.Height
                    _PrintState = 2 'advance to Corrective Action
                End If


                '2:              'Corrective Action
            End If

            If _PrintState = 2 Then


                MyRichTextBox = DirectCast(rtxtCorrectiveAction, RichTextBox)
                DescriptionString = _CorrectiveActionString
                While DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbCr Or DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbLf
                    ' MsgBox("Remove vbCrLf")
                    DescriptionString = DescriptionString.Substring(0, DescriptionString.Length - 1)
                End While
                'Now add one back
                DescriptionString = DescriptionString + vbCrLf
                'Size of the area that is left that may be printed on
                'rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, MyPrintArguments.MarginBounds.Top + My_Y_Location, MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location))
                rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location, MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location))
                'Set the Printfont to that used in the Failure Description
                'PrintFont = MyDescriptionRichTextBox.Font
                PrintFont = New Font(MyRichTextBox.Font.FontFamily, MyRichTextBox.Font.Size, FontStyle.Bold)
                'Measure Ho many characters and lines will fit in the print area
                'sizeMeasure = New SizeF(MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location) - PrintFont.GetHeight(MyPrintArguments.Graphics))
                sizeMeasure = New SizeF(MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location)) ' - PrintFont.GetHeight(MyPrintArguments.Graphics))
                'Set the trim style
                strFormat.Trimming = StringTrimming.Word
                'Measure how much space is required to print the current string
                MyPrintArguments.Graphics.MeasureString(DescriptionString, PrintFont, sizeMeasure, strFormat, numChars, numLines)
                'some how a neagative height is being returned... so st to zero
                If sizeMeasure.Height < 0 Then
                    numChars = 0
                End If

                ''Force to a minimum so that a box is drawn...
                'If numLines < 2 Then
                '    numLines = 2
                'End If
                'If numLines * PrintFont.Height > rectDraw.Height Then
                '    rectDraw.Height = numLines * PrintFont.Height
                'End If



                If numChars = 0 Then
                    'Don't print anything don't draw a box....
                ElseIf numLines = 1 And numChars = DescriptionString.Length Then
                    'Print If just the header...
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Still Size it...But don;' draw it...used for advancing location of text...
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                ElseIf numLines = 1 Then
                    'Don't print anything
                ElseIf numLines > 1 Then
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Size it
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                    'draw it
                    MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                End If


                'Check to see if everything has been printed
                'If everything has not been printed  
                'Or only the header would have been drawn
                If (numChars < DescriptionString.Length) Then

                    'Remove Text that has allready been printed and Preface remaining text with new Header...
                    If InStr(DescriptionString, "CORRECTIVE ACTION:") Then
                        ' In this case Nothing has been written yet...so do nothing
                        ' _CorrectiveActionString = DescriptionString.Substring(numChars)
                    Else

                        _CorrectiveActionString = "CORRECTIVE ACTION (CONT..):" + vbCrLf + DescriptionString.Substring(numChars)
                    End If

                    MyPrintArguments.HasMorePages = True
                    '    _PageNum += 1 ' increment page count
                    '  GoTo END_OF_SUB
                Else
                    'Everything from Text box has been printed so...
                    'Advance the top of the next print area past the bottom of previous Print area....
                    ' My_Y_Location = My_Y_Location + (numLines + 1) * PrintFont.Height
                    My_Y_Location = rectDraw2.Bottom + PrintFont.Height
                    _PrintState = 3 'advance to Engineering Notes

                End If
            End If

            '3:              'Engineering Notes
            If _PrintState = 3 Then



                MyRichTextBox = DirectCast(rtxtEngineeringNotes, RichTextBox)
                DescriptionString = _EngineeringNotesString
                While DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbCr Or DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbLf
                    ' MsgBox("Remove vbCrLf")
                    DescriptionString = DescriptionString.Substring(0, DescriptionString.Length - 1)
                End While
                'Now add one back
                DescriptionString = DescriptionString + vbCrLf
                'Size of the area that is left that may be printed on
                'rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, MyPrintArguments.MarginBounds.Top + My_Y_Location, MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location))
                rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location, MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location))
                'Set the Printfont to that used in the Failure Description
                'PrintFont = MyDescriptionRichTextBox.Font
                PrintFont = New Font(MyRichTextBox.Font.FontFamily, MyRichTextBox.Font.Size, FontStyle.Bold)
                'Measure Ho many characters and lines will fit in the print area
                'sizeMeasure = New SizeF(MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location) - PrintFont.GetHeight(MyPrintArguments.Graphics))
                sizeMeasure = New SizeF(MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location)) ' - PrintFont.GetHeight(MyPrintArguments.Graphics))
                'Set the trim style
                strFormat.Trimming = StringTrimming.Word
                'Measure how much space is required to print the current string
                MyPrintArguments.Graphics.MeasureString(DescriptionString, PrintFont, sizeMeasure, strFormat, numChars, numLines)

                'some how a neagative height is being returned... so st to zero
                If sizeMeasure.Height < 0 Then
                    numChars = 0
                End If

                ''Force to a minimum so that a box is drawn...
                'If numLines < 2 Then
                '    numLines = 2
                'End If
                'If numLines * PrintFont.Height > rectDraw.Height Then
                '    rectDraw.Height = numLines * PrintFont.Height
                'End If



                If numChars = 0 Then
                    'Don't print anything don't draw a box....
                ElseIf numLines = 1 And numChars = DescriptionString.Length Then
                    'Print If just the header...
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Still Size it...But don;' draw it...used for advancing location of text...
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                ElseIf numLines = 1 Then
                    'Don't print anything
                ElseIf numLines > 1 Then
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Size it
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                    'draw it
                    MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                End If


                'Check to see if everything has been printed
                'If everything has not been printed  
                'Or only the header would have been drawn
                If (numChars < DescriptionString.Length) Then

                    'Remove Text that has allready been printed and Preface remaining text with new Header...
                    If InStr(DescriptionString, "ENGINEERING NOTES:") Then
                        ' In this case Nothing has been written yet...so do nothing
                        ' _EngineeringNotesString = DescriptionString.Substring(numChars)
                    Else

                        _EngineeringNotesString = "ENGINEERING NOTES (CONT..):" + vbCrLf + DescriptionString.Substring(numChars)
                    End If

                    MyPrintArguments.HasMorePages = True
                    '        _PageNum += 1 ' increment page count
                    ' GoTo END_OF_SUB
                Else
                    'Everything from Text box has been printed so...
                    'Advance the top of the next print area past the bottom of previous Print area....
                    'My_Y_Location = My_Y_Location + (numLines + 1) * PrintFont.Height
                    My_Y_Location = rectDraw2.Bottom + PrintFont.Height
                    _PrintState = 4 'advance to Corrective Action

                End If
            End If
            '4:              'TCC comments
            If _PrintState = 4 Then

                MyRichTextBox = DirectCast(rtxtTCC_Comments, RichTextBox)
                DescriptionString = _TCC_CommentsString
                While DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbCr Or DescriptionString.Substring(DescriptionString.Length - 1, 1) = vbLf
                    ' MsgBox("Remove vbCrLf")
                    DescriptionString = DescriptionString.Substring(0, DescriptionString.Length - 1)
                End While
                'Now add one back
                DescriptionString = DescriptionString + vbCrLf
                'Size of the area that is left that may be printed on
                'rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, MyPrintArguments.MarginBounds.Top + My_Y_Location, MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location))
                rectDraw = New RectangleF(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location, MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location))
                'Set the Printfont to that used in the Failure Description
                'PrintFont = MyDescriptionRichTextBox.Font
                PrintFont = New Font(MyRichTextBox.Font.FontFamily, MyRichTextBox.Font.Size, FontStyle.Bold)
                'Measure Ho many characters and lines will fit in the print area
                'sizeMeasure = New SizeF(MyPrintArguments.MarginBounds.Width, MyPrintArguments.MarginBounds.Height - (MyPrintArguments.MarginBounds.Top + My_Y_Location) - PrintFont.GetHeight(MyPrintArguments.Graphics))
                sizeMeasure = New SizeF(MyMarginsWidth, MyPrintArguments.MarginBounds.Bottom - (topOffset + My_Y_Location)) ' - PrintFont.GetHeight(MyPrintArguments.Graphics))
                'Set the trim style
                strFormat.Trimming = StringTrimming.Word
                'Measure how much space is required to print the current string
                MyPrintArguments.Graphics.MeasureString(DescriptionString, PrintFont, sizeMeasure, strFormat, numChars, numLines)

                'some how a neagative height is being returned... so st to zero
                If sizeMeasure.Height < 0 Then
                    numChars = 0
                End If

                ''Force to a minimum so that a box is drawn...
                'If numLines < 2 Then
                '    numLines = 2
                'End If
                'If numLines * PrintFont.Height > rectDraw.Height Then
                '    rectDraw.Height = numLines * PrintFont.Height
                'End If



                If numChars = 0 Then
                    'Don't print anything don't draw a box....
                ElseIf numLines = 1 And numChars = DescriptionString.Length Then
                    'Print If just the header...
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Still Size it...But don;' draw it...used for advancing location of text...
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                ElseIf numLines = 1 Then
                    'Don't print anything
                ElseIf numLines > 1 Then
                    'Get the maximum number of characters that will fit in print area
                    stringForPage = DescriptionString.Substring(0, numChars)
                    'Print to the print area
                    MyPrintArguments.Graphics.DrawString(stringForPage, PrintFont, Brushes.Black, rectDraw, strFormat)
                    'Size it
                    rectDraw2 = New Rectangle(MyPrintArguments.MarginBounds.Left, topOffset + My_Y_Location + PrintFont.Height, MyMarginsWidth, (numLines) * PrintFont.Height)
                    'draw it
                    MyPrintArguments.Graphics.DrawRectangle(MyBoxColor, rectDraw2)
                End If


                'Check to see if everything has been printed
                'If everything has not been printed  
                'Or only the header would have been drawn
                If (numChars < DescriptionString.Length) Then


                    'Remove Text that has allready been printed and Preface remaining text with new Header...
                    If InStr(DescriptionString, "TCC COMMENTS:") Then
                        ' In this case Nothing has been written yet...so do nothing
                        ' _TCC_CommentsString = DescriptionString.Substring(numChars)
                    Else
                        _TCC_CommentsString = "TCC COMMENTS (CONT..):" + vbCrLf + DescriptionString.Substring(numChars)
                    End If

                    MyPrintArguments.HasMorePages = True
                    '_PageNum += 1 ' increment page count
                    ' GoTo END_OF_SUB
                Else

                    'Everything from Text box has been printed so...
                    'Advance the top of the next print area past the bottom of previous Print area....
                    My_Y_Location = rectDraw2.Bottom + PrintFont.Height
                    'Reinitialize Class Scoped Variables...
                    _PrintState = 0
                    _PageNum = 0
                    _DescriptionString = ""
                    _CorrectiveActionString = ""
                    _EngineeringNotesString = ""
                    _TCC_CommentsString = ""
                    'Make sure that the event does not fire again
                    MyPrintArguments.HasMorePages = False
                    '************************New Code to add printing Test equipment info in the future**************************************
                    '_PrintState = 5 'advance to TestEquipment
                    '************************New Code to add printing Test equipment info in the future**************************************

                End If
            End If
            'Catch errors....
            If _PrintState > 4 Then
                _PrintState = 0
                _PageNum = 0
                _DescriptionString = ""
                _CorrectiveActionString = ""
                _EngineeringNotesString = ""
                _TCC_CommentsString = ""
                'Make sure that the event does not fire again
                MyPrintArguments.HasMorePages = False
            End If
            '************************New Code to add printing Test equipment info in the future**************************************
            'If _PrintState = 5 Then
            '    'Print nothing and Force a Pagebreak...
            '    MyPrintArguments.HasMorePages = False
            '    _PrintState = 6
            '    Exit Sub

            'End If

            ''Print the datagridview with the test equipment info...
            'If _PrintState = 6 Then
            '    Print_TestEquipment_DataGridview(sender, e)
            'End If
            '************************New Code to add printing Test equipment info in the future**************************************
        Catch ex As Exception
            MsgBox("Error printing Failure Report..." + vbCrLf + ex.ToString)
            _PrintState = 0
            _PageNum = 0
            _DescriptionString = ""
            _CorrectiveActionString = ""
            _EngineeringNotesString = ""
            _TCC_CommentsString = ""
            'Make sure that the event does not fire again
            MyPrintArguments.HasMorePages = False
        End Try

    End Sub
    Private Sub tsmPrintSetUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrintSetUp.Click
        'PrintSetup
        PrintDialogFailureReport.Document = PrintDocumentFailureReport
        PrintDialogFailureReport.ShowDialog()
    End Sub
    Private Sub tsmPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmPrintPreview.Click

        Dim View As New eFailureReportView
        View = _FailureReportView
        SetFR_VIEW(eFailureReportView.FR_VIEW)

        'Make sure the grid has been drawn...
        Dim BackupIndex As Integer = tcReportBody.SelectedIndex
        tcReportBody.SelectTab("tpTestEquipment")

        'Clear the Print Buffers
        _DescriptionString = ""
        _CorrectiveActionString = ""
        _EngineeringNotesString = ""
        _TCC_CommentsString = ""
        _Test_EquipmentString = ""
        _PrintState = 0
        _PageNum = 0
        'Fill the Print Buffers, Prefacing each with Header Text....
        _DescriptionString = "DESCRIPTION:" + vbCrLf + rtxtDescription.Text + vbCrLf + vbCrLf
        _CorrectiveActionString = "CORRECTIVE ACTION:" + vbCrLf + rtxtCorrectiveAction.Text + vbCrLf + vbCrLf
        _EngineeringNotesString = "ENGINEERING NOTES:" + vbCrLf + rtxtEngineeringNotes.Text + vbCrLf + vbCrLf
        _TCC_CommentsString = "TCC COMMENTS:" + vbCrLf + rtxtTCC_Comments.Text
        '_Test_EquipmentString = "TEST EQUIPMENT USED:" + vbCrLf + GridviewToFormatedString(dgvTestEquipmentUsed, 0, 2, "COURIER NEW", 8.25, 2)

        PrintPreviewDialogFailureReport.Document = PrintDocumentFailureReport
        PageSetupDialogFailureReport.Document = PrintDocumentFailureReport
        PageSetupDialogFailureReport.PageSettings.Margins.Left = 10
        PageSetupDialogFailureReport.PageSettings.Margins.Right = 10
        PageSetupDialogFailureReport.PageSettings.Margins.Top = 50
        PageSetupDialogFailureReport.PageSettings.Margins.Bottom = 50
        PageSetupDialogFailureReport.PageSettings.Landscape = True
        If PageSetupDialogFailureReport.ShowDialog() = Windows.Forms.DialogResult.OK Then

            ' MsgBox("PageSetupDialog1.PageSettings.Margins.Right = " + PageSetupDialogFailureReport.PageSettings.Margins.Right.ToString + vbCrLf + _
            ' "PageSetupDialog1.PageSettings.Margins.Left = " + PageSetupDialogFailureReport.PageSettings.Margins.Left.ToString + vbCrLf + _
            ' "PageSetupDialog1.PageSettings.Margins.Bottom = " + PageSetupDialogFailureReport.PageSettings.Margins.Bottom.ToString + vbCrLf + _
            ' "PageSetupDialog1.PageSettings.Margins.Top = " + PageSetupDialogFailureReport.PageSettings.Margins.Top.ToString + vbCrLf)

            PrintPreviewDialogFailureReport.Size = New System.Drawing.Size(Me.Width, Me.Height)
            PrintPreviewDialogFailureReport.ShowDialog()
        Else


        End If
        tcReportBody.SelectTab(BackupIndex)
        SetFR_VIEW(View)
    End Sub
#End Region 'PRINTING
#Region "FILTER"
    Private Sub tsmFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmFilter.Click

        Dim MyFilterForm As New frmFilter
        gb_ProcessEvents = False
        TableLayoutPanel3Rows.Hide()
        MyFilterForm.ShowDialog()
        TableLayoutPanel3Rows.Show()
        gb_ProcessEvents = True
    End Sub
#End Region '"FILTER"
#Region "VIEW"



    Private Sub tsmViewCustomizeGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewCustomizeGrid.Click
        Dim MyMangageGridForm As New frmManageGridview
        TableLayoutPanel3Rows.Hide()
        MyMangageGridForm.ShowDialog()
        TableLayoutPanel3Rows.Show()

    End Sub
    Private Sub tsmViewHeaderGridAndDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewHeaderGridAndDetails.Click
        SetFR_VIEW(eFailureReportView.HEADER_GRID_AND_DETAIL_VIEW)
    End Sub
    Private Sub tsmViewBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewDataGridBrowse.Click
        SetFR_VIEW(eFailureReportView.BROWSER_VIEW)
    End Sub
    Private Sub tsmViewFR_View_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewFR_View.Click
        SetFR_VIEW(eFailureReportView.FR_VIEW)
    End Sub
    Private Sub tsmViewHeaderAndGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewHeaderAndGrid.Click
        SetFR_VIEW(eFailureReportView.HEADER_AND_GRID_VIEW)
    End Sub
    Private Sub tsmViewRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmViewRefresh.Click
        RefreshFailureReportDataBase()
    End Sub
#End Region 'VIEW
#Region "OPTIONS"
    Private Sub tsmOptionsManageDropdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsManageDropdown.Click
        frmCustomizeDropDowns.ShowDialog()
    End Sub
    Private Sub tsmOptionsUsePastValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsUsePastValues.Click
        If tsmOptionsUsePastValues.Checked = True Then
            tsmOptionsUsePastValues.Checked = False
        Else
            tsmOptionsUsePastValues.Checked = True
        End If
    End Sub
    Private Sub tsmOptionsManageDropDownsShowStandardValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsManageDropDownsShowStandardValues.Click
        If tsmOptionsManageDropDownsShowStandardValues.Checked = True Then
            tsmOptionsManageDropDownsShowStandardValues.Checked = False
            tsmOptionsManageDropDownsShowPastValues.Checked = True
        Else
            tsmOptionsManageDropDownsShowStandardValues.Checked = True
            tsmOptionsManageDropDownsShowPastValues.Checked = False
        End If
    End Sub
    Private Sub tsmOptionsManageDropDownsShowPastValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsManageDropDownsShowPastValues.Click
        If tsmOptionsManageDropDownsShowPastValues.Checked = True Then
            tsmOptionsManageDropDownsShowPastValues.Checked = False
            tsmOptionsManageDropDownsShowStandardValues.Checked = True
        Else
            tsmOptionsManageDropDownsShowPastValues.Checked = True
            tsmOptionsManageDropDownsShowStandardValues.Checked = False
        End If
    End Sub
    Private Sub tsmOptionsBrowseDatabaseAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsBrowseDatabaseAdmin.Click
        frmDataBaseBrowser.Show()
    End Sub
    Private Sub tsmOptionsSelectDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsSelectDatabase.Click
        Dim Result As Windows.Forms.DialogResult = frmSelectDatabase.ShowDialog()

        'Add some re-init code here....
        If Result = Windows.Forms.DialogResult.OK Then

            FailureReportDatabaseLoad()
            TableLayoutPanel3Rows.Show()
        Else
            Dim mystop As Integer = 1
        End If

    End Sub
    Dim _DisableCheckEvent As Boolean = True
    Private Sub tsmOptionsVerbose_Value_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsVerbose_Value_0.Click, tsmOptionsVerbose_Value_1.Click, tsmOptionsVerbose_Value_2.Click, tsmOptionsVerbose_Value_3.Click

        Dim MyToolStripMenuItem As ToolStripItem = DirectCast(sender, ToolStripItem)
        _DisableCheckEvent = False
    End Sub
    Private Sub tsmOptionsOnlyReturnOpenReports_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsOnlyReturnOpenReports.CheckStateChanged

        'There may be a more legen way to do this, but it works for now...

        If gb_ProcessEvents = True Then
            If tsmOptionsOnlyReturnOpenReports.Checked = True Then
                _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_OPEN_FAILURE_REPORTS
                FailureReportDatabaseLoad()

            Else
                _FR_SQL_Commands = sFR_SQL_Commands.SQL_GET_ALL_FAILURE_REPORTS
                FailureReportDatabaseLoad()

            End If
        End If
    End Sub
    Private Sub tsmOptionsPromptForDatabaseAtLaunch_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsPromptForDatabaseAtLaunch.CheckStateChanged
        If tsmOptionsPromptForDatabaseAtLaunch.CheckState = CheckState.Checked Then
            My.Settings.Prompt_To_Change_Database = True
        Else
            My.Settings.Prompt_To_Change_Database = False
        End If
    End Sub
#End Region '"OPTIONS"
#End Region 'Tool Strip Menu Items Events
#Region "Failure Report Content"
#Region "     Failure Report Descripiton Tab"
    ''' <summary>
    ''' This event handler preserves the orginal content of the Failure discription by moving the cursor to the end of the orginal content 
    ''' in the vent that the Cursor is clicked anywhere in the orginal content
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtxtReportDescription_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtxtDescription.MouseClick
        Try
            If _EditState <> eEditState.READ_ONLY Then
                If _CurrentUser.AccessLevel = eAccessState.ADMIN Then
                    'Allow editing...

                Else
                    'backupedit length is the length of the length of the orginal data.
                    'if the current length is less that the orginal then..assume the user has mamged to delete
                    'some of it...so restore the orginal text...
                    If rtxtDescription.Text.Length < _myTxtReportDescriptionBackUpEdit.Length Then
                        rtxtDescription.Text = _myTxtReportDescriptionBackUpEdit
                    End If

                    'and force the cursor to the end of the orginal text...
                    If rtxtDescription.SelectionStart <= _myTxtReportDescriptionBackUp.Length Then
                        rtxtDescription.Select(_myTxtReportDescriptionBackUpEdit.Length, 0)
                    End If

                End If

            End If
        Catch ex As Exception
            v_UpdateLog("SUB rtxtReportDescription_MouseClick: ")
            v_UpdateLog(ex.ToString)
        End Try

    End Sub
    Private Sub rtxtReportDescription_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxtDescription.KeyDown
        If _EditState <> eEditState.READ_ONLY Then
            If _CurrentUser.AccessLevel = eAccessState.ADMIN Then
                'Allow editing...

            Else
                'Suppress the key stroke if the user trys to back space over the orginal text in the textbox
                If rtxtDescription.SelectionStart = _myTxtReportDescriptionBackUp.Length And e.KeyCode = Keys.Back Then
                    e.SuppressKeyPress = True
                    'Suppress the keystroke if the keystroke is within the orginal text region
                ElseIf rtxtDescription.SelectionStart < _myTxtReportDescriptionBackUp.Length Then
                    e.SuppressKeyPress = True
                Else
                    'valid key stroke indicated a change occured, set the Record edited Flag to True and the Record Saved flag to false...
                    e.SuppressKeyPress = False
                    '  _RecordEdited = True
                    _RecordSaved = False
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' This event handler evaluates if the user has changed the content of the text box and if the user has changed it updates the backupedit buffer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtReportDescription_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtDescription.Leave
        'Allow sub to execute if the edit state is not Read_Only
        If _EditState <> eEditState.READ_ONLY Then
            'if the user did not add any information then do not change, but strip any leading lagging spaces that may have been added...
            If Trim(rtxtDescription.Text.Trim) = _myTxtReportDescriptionBackUpEdit.Trim Then

                rtxtDescription.Text = _myTxtReportDescriptionBackUp.Substring(0, _myTxtReportDescriptionBackUpLength)
            Else
                'If the user changed the Discription then update the back up buffer...
                _myTxtReportDescriptionBackUpEdit = rtxtDescription.Text
            End If
        End If
    End Sub
#End Region '     Failure Report Descripiton Tab
#Region "     Corrective Action Tab"

    Private Sub rtxtCorrectiveAction_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtxtCorrectiveAction.MouseClick
        Try
            'First Back up everything...This may not be necessary as Data is recoverable from the database - FJB
            If _EditState <> eEditState.READ_ONLY Then

                'insert logging header only if it hasn't happened yet. Using Length to determine.
                If rtxtCorrectiveAction.Text.Length < _myTxtCorrectiveActionBackupEdit.Length Then
                    rtxtCorrectiveAction.Text = _myTxtCorrectiveActionBackupEdit
                End If

                'Do not allow the user to select previous data...
                If rtxtCorrectiveAction.SelectionStart <= _myTxtCorrectiveActionBackupEdit.Length Then
                    rtxtCorrectiveAction.Select(_myTxtCorrectiveActionBackupEdit.Length, 0)
                End If
            End If
        Catch

        End Try
    End Sub

    Private Sub rtxtCorrectiveAction_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxtCorrectiveAction.KeyDown

        'This supresses where the user is allowed to edit, so that the user cannot overwrite previous saved changes 
        If _EditState <> eEditState.READ_ONLY Then
            If rtxtCorrectiveAction.SelectionStart = _myTxtCorrectiveActionBackUp.Length And e.KeyCode = Keys.Back Then
                e.SuppressKeyPress = True
            ElseIf rtxtCorrectiveAction.SelectionStart < _myTxtCorrectiveActionBackUp.Length Then
                e.SuppressKeyPress = True
            Else
                e.SuppressKeyPress = False
                ' _RecordEdited = True
                _RecordSaved = False
            End If
        End If
    End Sub

    Private Sub rtxtCorrectiveAction_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtCorrectiveAction.Leave
        If _EditState <> eEditState.READ_ONLY Then
            If Trim(rtxtCorrectiveAction.Text) = _myTxtCorrectiveActionBackupEdit Then
                'if the user did not add any information then do not change
                rtxtCorrectiveAction.Text = _myTxtCorrectiveActionBackUp.Substring(0, _myTxtCorrectiveActionBackupLength)
            Else
                _myTxtCorrectiveActionBackupEdit = rtxtCorrectiveAction.Text
            End If
        End If
    End Sub
#End Region '"Engineering Notes Tab"
#Region "     Engineering Notes tab"
    Private Sub rtxtEngineeringNotes_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtxtEngineeringNotes.MouseClick
        Try
            'First Back up everything...This may not be necessary as Data is recoverable from the database - FJB
            If _EditState <> eEditState.READ_ONLY Then
                'insert logging header only if it hasn't happened yet. Using Length to determine.
                If rtxtEngineeringNotes.Text.Length < _myEngineeringNotesBackupEdit.Length Then
                    rtxtEngineeringNotes.Text = _myEngineeringNotesBackupEdit
                End If

                'Do not allow the user to select previous data...

                If rtxtEngineeringNotes.SelectionStart <= _myEngineeringNotesBackupEdit.Length Then
                    rtxtEngineeringNotes.Select(_myEngineeringNotesBackupEdit.Length, 0)
                End If
            End If
        Catch

        End Try
    End Sub

    Private Sub rtxtEngineeringNotes_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxtEngineeringNotes.KeyDown
        'This supresses where the user is allowed to edit, so that the user cannot overwrite previous saved changes 
        If _EditState <> eEditState.READ_ONLY Then
            If rtxtEngineeringNotes.SelectionStart = _myEngineeringNotesBackup.Length And e.KeyCode = Keys.Back Then
                e.SuppressKeyPress = True
            ElseIf rtxtEngineeringNotes.SelectionStart < _myEngineeringNotesBackup.Length Then
                e.SuppressKeyPress = True
            Else
                e.SuppressKeyPress = False
                '    _RecordEdited = True
                _RecordSaved = False
            End If
        End If
    End Sub

    Private Sub rtxtEngineeringNotes_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtEngineeringNotes.Leave
        If _EditState <> eEditState.READ_ONLY Then
            If Trim(rtxtEngineeringNotes.Text) = _myEngineeringNotesBackupEdit Then
                'if the user did not add any information then do not change
                rtxtEngineeringNotes.Text = _myEngineeringNotesBackup.Substring(0, _myEngineeringNotesBackUpLength)
            Else
                _myEngineeringNotesBackupEdit = rtxtEngineeringNotes.Text
            End If
        End If
    End Sub
#End Region '"Engineering Notes tab"
#Region "      TCC Comments Tab"
    Private Sub rtxtTCC_Comments_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtxtTCC_Comments.MouseClick
        Try
            'First Back up everything...This may not be necessary as Data is recoverable from the database - FJB
            If _EditState <> eEditState.READ_ONLY Then
                If _EditState <> eEditState.EDIT_ADMIN Then
                    'insert logging header only if it hasn't happened yet. Using Length to determine.
                    If rtxtTCC_Comments.Text.Length < _myTCC_CommentsBackupEdit.Length Then
                        rtxtTCC_Comments.Text = _myTCC_CommentsBackupEdit
                    End If

                    'Do not allow the user to select previous data...
                    If rtxtTCC_Comments.SelectionStart <= _myTCC_CommentsBackupEdit.Length Then
                        rtxtTCC_Comments.Select(_myTCC_CommentsBackupEdit.Length, 0)
                    End If
                Else
                    'allow full edit...
                End If
            End If

        Catch

        End Try
    End Sub

    Private Sub rtxtTCC_Comments_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxtTCC_Comments.KeyDown
        If _EditState <> eEditState.READ_ONLY Then
            If _EditState <> eEditState.EDIT_ADMIN Then
                If rtxtTCC_Comments.SelectionStart = _myTCC_CommentsBackup.Length And e.KeyCode = Keys.Back Then
                    e.SuppressKeyPress = True
                ElseIf rtxtTCC_Comments.SelectionStart < _myTCC_CommentsBackup.Length Then
                    e.SuppressKeyPress = True
                Else
                    e.SuppressKeyPress = False
                    '   _RecordEdited = True
                    _RecordSaved = False
                End If
            Else
                'allow full edit
            End If

        End If
    End Sub

    Private Sub rtxtTCC_Comments_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtTCC_Comments.Leave
        If _EditState <> eEditState.READ_ONLY Then
            ' If _EditState <> eEditState.EDIT_ADMIN Then
            If Trim(rtxtTCC_Comments.Text) = _myTCC_CommentsBackupEdit Then
                'if the user did not add any information then do not change
                rtxtTCC_Comments.Text = _myTCC_CommentsBackup.Substring(0, _myTCC_CommentsBackupLength)
            Else
                _myTCC_CommentsBackupEdit = rtxtTCC_Comments.Text
            End If
            'Else
            'allow full edit
            ' End If
        End If
    End Sub
#End Region '"TCC Comments Tab"
#Region "Attachment Tab"

    Private Sub Attach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachFile.Click

        Dim Impersonator As New clsAuthenticator 'used for Read Write access to HQA File server
        Try

            ' 'impersaonate 'gelab' to get get read write access to HQA File Server
            ' Impersonator.Start()

            'Get the Attachment....
            ' Set the initial Directory or Folder...Try the most recent folder directoy used to create attachment if exists 
            If My.Settings.RecentFolder.Trim <> "" Then
                Try
                    'Try to use recent...
                    OpenFileDialog1.InitialDirectory = Path.GetDirectoryName(My.Settings.RecentFolder.Trim) 'My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\RadianLogger\"
                Catch ex As Exception
                    'on error use MyDocuments folder...
                    OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                End Try

            Else
                'else default to My Documents Folder
                OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End If

            'Allow selecting multiple Files...
            OpenFileDialog1.Multiselect = True

            'Show the openfile dialog box...
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

                'store recent folder
                My.Settings.RecentFolder = Path.GetDirectoryName(OpenFileDialog1.FileName)

                'These are the name of the files to put in the zip folder...
                Dim files() As String = OpenFileDialog1.FileNames
                Dim FileList As List(Of String) = New List(Of String)



                For i = 0 To files.Length - 1
                    Dim ConvertedPath As String = ConvertMappedFilePathToUNCPath(files(i))
                    FileList.Add(ConvertedPath)
                Next
                'Repalce with UNC Files...
                files = FileList.ToArray

                Dim FR_ZIP_PATH As String
                If txtAttachments.Text.Trim = "" Then
                    'Set the folder where are the Failure report attachement folder is located...
                    FR_ZIP_PATH = Path.GetDirectoryName(My.Settings.AttachmentFolder.Trim)
                    'Add the name of the specifc zip folder  "ID + ### + ".zip"
                    Dim ZipFolderName As String = "ID" + txtReportNumber.Text.Trim + ".zip"

                    'Convert a Mapped Drive Pathe to a UNC path
                    'This is currently hard coded in multiple locations...it should probably be in a table in the database...o
                    FR_ZIP_PATH = ConvertMappedFilePathToUNCPath(FR_ZIP_PATH + "\Attachments\" & ZipFolderName)
                    txtAttachments.Text = FR_ZIP_PATH
                Else
                    'Set to existing path stored in txtAttachments and replace old path with new path if needed...
                    '  This was necessary after the Server was converted...
                    FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("uslafsv34", "am.bm.net\uslafdfs01")
                    FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("am.bm.net\uslafdfs01\eng_proj", "am.bm.net\uslafdfs01\HQA_Attachments")
                    FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("am.bm.net\uslafdfs01\HQA_Attachments\FailRptDB", "Uslafvs001038\HQA\Software\Failure Report Database")
                End If

                'impersaonate 'gelab' to get get read write access to HQA File Server
                Impersonator.Start()

                'Create new zip file...
                Dim Zip As ZipFile = New ZipFile

                'Does the File allready exist?
                Dim ZipFileExists As Boolean = System.IO.File.Exists(FR_ZIP_PATH)



                'If not Create the zipfile
                If ZipFileExists = False Then
                    'create the folder...
                    Zip.Save(FR_ZIP_PATH)
                End If

                'Now it is safe to read...
                Zip = ZipFile.Read(FR_ZIP_PATH)

                ' 'impersaonate 'gelab' to get get read write access to HQA File Server
                ' Impersonator.Start()

                Using Zip

                    ' Dim Files() As String = txtAttachments.Text.Split(vbCrLf)
                    ' Dim Files As ListBox.ObjectCollection  '=  clbAttachmentsAdd.Items
                    For i = 0 To files.Count - 1

                        'This code is getting the names of the files allready in the archive
                        Dim MyFileNames As System.Collections.Generic.ICollection(Of String)
                        MyFileNames = Zip.EntryFileNames

                        'Check in see if we are about to overwrite the files
                        If Zip.ContainsEntry(System.IO.Path.GetFileName(files(i).Trim)) = False Then
                            Zip.UpdateFile(files(i).Trim, "")
                        ElseIf MsgBox(files(i).ToString + " " + "Exists in Archive. Overwrite?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Zip.UpdateFile(files(i).Trim, "")
                        End If

                    Next
                    Zip.Save(FR_ZIP_PATH)
                End Using
                Impersonator.Undo()
            End If
            UpdateAttachmentList()

        Catch ex As Exception
            Try
                'make sure Read / Write access is removed....
                Impersonator.Undo()
            Catch ex2 As Exception

            End Try

            MsgBox("Error trying to add files to Archive" + vbCrLf + ex.ToString)
        End Try
    End Sub
    Private Sub UpdateAttachmentList()
        Cursor = Cursors.AppStarting
        Dim Impersonator As New clsAuthenticator 'used for Read Write access to HQA File server
        Try

            'impersaonate 'gelab' to get get read write access to HQA File Server
            Impersonator.Start()
            If txtAttachments.Text.Trim = "" Then
                chklbAttachmentsInArchive.Items.Clear()
                'Zip file does not exist

            Else

                'Make sure that Mapped Drive Path is a UNC path
                Dim FR_ZIP_PATH As String
                FR_ZIP_PATH = ConvertMappedFilePathToUNCPath(txtAttachments.Text.Trim).Replace("uslafsv34", "am.bm.net\uslafdfs01")
                FR_ZIP_PATH = (ConvertMappedFilePathToUNCPath(txtAttachments.Text)).Replace("am.bm.net\uslafdfs01\eng_proj", "am.bm.net\uslafdfs01\HQA_Attachments")
                FR_ZIP_PATH = (ConvertMappedFilePathToUNCPath(txtAttachments.Text.Trim)).Replace("am.bm.net\uslafdfs01\HQA_Attachments\FailRptDB", "Uslafvs001038\HQA\Software\Failure Report Database")


                'only populate the list if the tab attachment tab is showing...
                If tcReportBody.SelectedTab.Name = tpAttachments.Name Then
                    If System.IO.File.Exists(FR_ZIP_PATH) Then '+ "\Attachments\" & FolderName & ".zip") Then

                        'clear files from list
                        chklbAttachmentsInArchive.Items.Clear()
                        'open the zip file if possible
                        Dim zip As ZipFile = New ZipFile

                        Try
                            zip = ZipFile.Read(FR_ZIP_PATH)
                            'This code is getting the names of the files allready in the archive
                            Dim MyFileNames As System.Collections.Generic.ICollection(Of String)
                            MyFileNames = zip.EntryFileNames
                            zip.Dispose()
                            'Add Files to List Box, these will be added when saved

                            For j = 0 To MyFileNames.Count - 1
                                chklbAttachmentsInArchive.Items.Add(MyFileNames(j))
                            Next
                        Catch ex As Exception
                            zip.Dispose()
                            chklbAttachmentsInArchive.Items.Add("Unable to Open Archive")
                        End Try

                    Else 'There is no zip file...
                        chklbAttachmentsInArchive.Items.Clear()
                        chklbAttachmentsInArchive.Items.Add("No Files Found In Archive")

                    End If
                End If 'Is the attachment tab visible??



            End If 'Test to see if there is a zip attachment path stored in database
            Impersonator.Undo()
        Catch ex As Exception

            Try
                'make sure Read / Write access is removed....
                Impersonator.Undo()
            Catch ex2 As Exception

            End Try
            ''If bVerbose = True Then
            MsgBox("Error Selecting Folder to Attach " + vbCrLf + ex.ToString)
            ' End If

        End Try

        Cursor = Cursors.Default
    End Sub

    Private Sub tcReportBody_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcReportBody.SelectedIndexChanged
        UpdateAttachmentList()
    End Sub

    Private Sub btnOpenSelectedAttachments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenSelectedAttachments.Click
        'open attachment folder
        Dim Impersonator As New clsAuthenticator 'used for Read Write access to HQA File server
        Try

            Impersonator.Start()
            Dim FR_ZIP_PATH As String
            'Convert a Mapped Drive Pathe to a UNC path, update to new UNC path if needed
            FR_ZIP_PATH = (ConvertMappedFilePathToUNCPath(txtAttachments.Text)).Replace("uslafsv34", "am.bm.net\uslafdfs01")
            FR_ZIP_PATH = (ConvertMappedFilePathToUNCPath(txtAttachments.Text)).Replace("am.bm.net\uslafdfs01\eng_proj", "am.bm.net\uslafdfs01\HQA_Attachments")
            FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("am.bm.net\uslafdfs01\HQA_Attachments\FailRptDB", "Uslafvs001038\HQA\Software\Failure Report Database")

            Dim zip As New ZipFile
            zip = ZipFile.Read(FR_ZIP_PATH)
            Dim OrginalFileName As String
            For Each FileName As String In chklbAttachmentsInArchive.CheckedItems
                Try
                    'extract file

                    Dim FileInArchive As ZipEntry
                    For Each FileInArchive In zip
                        OrginalFileName = FileName

                        If FileInArchive.FileName = FileName Then
                            'create unique name
                            FileName = "~TMP" + Now.ToBinary.ToString + "_" + FileInArchive.FileName
                            'Change File in Archive to Match (This is temporary, if I do not call save...)
                            FileInArchive.FileName = FileName
                            FileInArchive.Extract(Path.GetTempPath, ExtractExistingFileAction.OverwriteSilently)
                            'openfile
                            System.Diagnostics.Process.Start(Path.GetTempPath + "\" + FileName)
                            FileInArchive.FileName = OrginalFileName
                            'exit the for now that a match was found
                            'if the program does not exit, an error as thrown due to renaming the file
                            Exit For
                        End If


                    Next

                Catch ex As IOException
                    MsgBox("File is Allready open or locked by another application." + vbCrLf + vbCrLf + ex.ToString)
                Catch ex As Exception
                    MsgBox("Unable to open " + FileName + vbCrLf + vbCrLf + ex.ToString)
                End Try

            Next

            zip.Dispose()
            Impersonator.Undo()
            'System.Diagnostics.Process.Start(FR_ZIP_PATH)
        Catch ex As Exception
            Try
                'make sure Read / Write access is removed....
                Impersonator.Undo()
            Catch ex2 As Exception

            End Try
            MsgBox("No attached Files Found!", vbExclamation, "Error")
        End Try
    End Sub


    Private Sub btnRemoveAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAttachment.Click
        If MsgBox("The following action cannot be undone. Are you sure you want to remove the selected items?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            'Create impersonator for read / write access
            Dim Impersonator As New clsAuthenticator
            Impersonator.Start()

            Dim FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("uslafsv34", "am.bm.net\uslafdfs01")
            'added once path moved...
            FR_ZIP_PATH = (ConvertMappedFilePathToUNCPath(txtAttachments.Text)).Replace("am.bm.net\uslafdfs01\eng_proj", "am.bm.net\uslafdfs01\HQA_Attachments")
            FR_ZIP_PATH = (txtAttachments.Text.Trim).Replace("am.bm.net\uslafdfs01\HQA_Attachments\FailRptDB", "Uslafvs001038\HQA\Software\Failure Report Database")

            Dim zip As New ZipFile
            Try
                zip = ZipFile.Read(FR_ZIP_PATH)
                Dim FilesToBeRemoved As New System.Collections.Generic.List(Of ZipEntry)
                For Each ArchiveItem In chklbAttachmentsInArchive.CheckedItems
                    For Each Entry As ZipEntry In zip
                        If Entry.FileName.Trim = ArchiveItem.trim Then
                            FilesToBeRemoved.Add(Entry)
                            Exit For
                        End If
                    Next
                Next

                For Each Entry As ZipEntry In FilesToBeRemoved
                    zip.RemoveEntry(Entry)
                Next
                zip.Save()
                zip.Dispose()
                'Remove Read Write Access to HQA Drive...
                Impersonator.Undo()
            Catch ex As Exception
                Try
                    'Remove read/Write Access to HQA Drive...
                    Impersonator.Undo()
                Catch ex2 As Exception

                End Try
                MsgBox("Error trying to remove files from Failure Report" + vbCrLf + ex.ToString)
                zip.Dispose()
            End Try

        End If
        UpdateAttachmentList()
    End Sub

    ''' <summary>
    ''' Function will take a file path and if it points to a
    ''' mapped drive then it will return the UNC path.
    ''' </summary>
    ''' <param name="strPath">Path to Map</param>
    ''' <returns>UNC Path</returns>
    ''' <remarks>Frank Boudreau 2016</remarks>
    Public Shared Function ConvertMappedFilePathToUNCPath(ByVal strPath As String) As String
        'First check if file path is already a UNC path.
        If strPath.Length > 2 Then
            If strPath.Substring(0, 2) = "\\" Then Return strPath 'If it is return it.
        Else
            'Path is too short so return strPath to stop app from crashing
            Return strPath
        End If
        'Path is not already a UNC path so use a
        'Windows Script Host Object Model to search all
        'network drives and record their letters and
        'paths in a hashtable.
        Dim htCurrentMappings As New Hashtable
        Dim objQuery As New WqlObjectQuery("select DriveType,DeviceID,ProviderName from Win32_LogicalDisk where DriveType=4")
        Dim objScope As New ManagementScope("\\.\root\CIMV2")
        objScope.Options.Impersonation = ImpersonationLevel.Impersonate
        objScope.Options.EnablePrivileges = True
        Dim objSearcher As New ManagementObjectSearcher(objScope, objQuery)
        For Each objManagementObject As ManagementObject In objSearcher.Get
            htCurrentMappings.Add(objManagementObject("DeviceID").ToString, objManagementObject("ProviderName").ToString)
        Next
        'Mapped drive letters and paths are now stored
        'in htCurrentMappings.

        'Get drive letter from strPath
        Dim strDriveLetter As String = strPath.Substring(0, 2)

        'Check if drive letter is a network drive
        If htCurrentMappings.Contains(strDriveLetter) Then
            'If it is return path with drive letter replaced by UNC path
            Return strPath.Replace(strDriveLetter, htCurrentMappings(strDriveLetter).ToString)
        Else
            'Else just return path as it is most likely local
            Return strPath
        End If
    End Function


    'Private Sub btnAddAttachment_Click(sender As System.Object, e As System.EventArgs) Handles btnAddAttachment.Click

    '    Dim strDataBasePath As String = Nothing
    '    Try



    '        'Go to the most recent folder directoy used to create attachment if exists 
    '        If My.Settings.RecentAttachmentFolder.Trim <> "" Then
    '            FolderDialogAddAttachments.SelectedPath = Path.GetDirectoryName(My.Settings.RecentAttachmentFolder.Trim) 'My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\RadianLogger\"
    '        Else
    '            'else default to My Documents Folder
    '            FolderDialogAddAttachments.SelectedPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '        End If


    '        If Me.FolderDialogAddAttachments.ShowDialog() = Windows.Forms.DialogResult.OK Then

    '            If txtAttachments.Text.Trim = "" Then
    '                txtAttachments.Text = ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)
    '            Else
    '                txtAttachments.Text = txtAttachments.Text + vbLf + ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)
    '            End If

    '        Else
    '            'dp nothing for now

    '        End If





    '        'strDataBasePath = ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)


    '        Dim i As Integer = 1
    '    Catch ex As Exception
    '        ''If bVerbose = True Then
    '        MsgBox("Error Selecting Folder to Attach " + vbCrLf + ex.ToString)
    '        ' End If

    '    End Try


    '    'txtAttachments.Text = strDataBasePath
    'End Sub

    'Private Sub btnAddAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAttachment.Click

    '    Dim strDataBasePath As String = Nothing
    '    Try


    '        'Go to the most recent folder directoy used to create attachment if exists 
    '        If My.Settings.AttachmentFolder.Trim <> "" Then
    '            Try
    '                'Try to use recent...
    '                FolderDialogAddAttachments.SelectedPath = Path.GetDirectoryName(My.Settings.AttachmentFolder.Trim) 'My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\RadianLogger\"
    '            Catch ex As Exception
    '                'on error use MyDocuments folder...
    '                FolderDialogAddAttachments.SelectedPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '            End Try

    '        Else
    '            'else default to My Documents Folder
    '            FolderDialogAddAttachments.SelectedPath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '        End If


    '        If Me.FolderDialogAddAttachments.ShowDialog() = Windows.Forms.DialogResult.OK Then

    '            If txtAttachments.Text.Trim = "" Then
    '                txtAttachments.Text = ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)
    '            Else
    '                txtAttachments.Text = txtAttachments.Text + vbCrLf + ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)
    '            End If

    '        Else
    '            'dp nothing for now

    '        End If





    '        'strDataBasePath = ConvertMappedFilePathToUNCPath(FolderDialogAddAttachments.SelectedPath)


    '        Dim i As Integer = 1
    '    Catch ex As Exception
    '        ''If bVerbose = True Then
    '        MsgBox("Error Selecting Folder to Attach " + vbCrLf + ex.ToString)
    '        ' End If

    '    End Try


    '    'txtAttachments.Text = strDataBasePath
    'End Sub

    'Private Sub RemoveAttachmentPathTextFromDatabase()

    '    Dim iColumnCount As Integer 'Column count of the Failure Report Database

    '    Dim MyRecord As New cCustomDataBaseAccess.cTable 'Record to hold database Changes

    '    'Datatable to hold the Database Table Schema inotmation ( i.E. Column Name, Datatype, Size etc...
    '    Dim MyLocalDatatable As New DataTable

    '    'Get the Schema Information
    '    MyLocalDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gMyFailureReportDBConnection, "Failure Report")

    '    'get the Column count
    '    iColumnCount = MyLocalDatatable.Rows.Count

    '    'Set the Table Name
    '    MyRecord.TableName = "Failure Report"

    '    'Find the correct row to update
    '    Dim RecordIndex As Integer = 0
    '    For Each row In MyLocalDatatable.Rows
    '        If MyLocalDatatable.Rows(RecordIndex).Item("ColumnName") = "Attachments" Then
    '            Exit For
    '        End If
    '        RecordIndex += 1
    '    Next

    '    'Initalize the columns 
    '    MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

    '    'populate the Record
    '    MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn("Attachments", "", MyLocalDatatable.Rows(RecordIndex).Item("DataType"), True))

    '    'Try to perform the update
    '    Try

    '        cCustomDataBaseAccess.UpdateExistingRecord(MyRecord, gMyFailureReportDBConnection, "New ID", txtReportNumber.Text.Trim)

    '    Catch ex As Exception 'Catch any errors
    '        MsgBox("Error Updating Record " + vbCrLf + ex.ToString)
    '    End Try


    'End Sub

    'Private Sub btnOpenAttachmentFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenAttachmentFolder.Click
    '    'open attachment folder
    '    Try

    '        Dim FR_ZIP_PATH As String ' = Path.GetDirectoryName(My.Settings.MeterSpecDataBaseFileDirectoryAndName)
    '        'Dim FolderName As String = "ID" + txtReportNumber.Text.Trim
    '        'Convert a Mapped Drive Pathe to a UNC path
    '        FR_ZIP_PATH = ConvertMappedFilePathToUNCPath(txtAttachments.Text)
    '        System.Diagnostics.Process.Start(FR_ZIP_PATH)
    '    Catch ex As Exception
    '        MsgBox("No attached Files Found!", vbExclamation, "Error")
    '    End Try
    'End Sub

    Private Sub btnClearLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLog.Click
        lbLogData.Items.Clear()
    End Sub
#End Region '"     Failure Report Attachment Tab"

    Private Sub rtxtMeterNotes_ContentsResized(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ContentsResizedEventArgs) Handles rtxtMeterNotes.ContentsResized
        rtxtMeterNotes.MaxLength = 255
        rtxtAMR_Notes.MaxLength = 255
        ' If e.NewRectangle.Width < rtxtMeterNotes.ClientSize.Width Then
        'Remove for now
        'While rtxtMeterNotes.Lines.Count > 3
        '    rtxtMeterNotes.Text = rtxtMeterNotes.Text.Substring(0, rtxtMeterNotes.Text.Length - 1)
        '    rtxtMeterNotes.SelectionStart = rtxtMeterNotes.Text.Length
        '    ' rtxtMeterNotes.SelectionLength = 0
        'End While
        ''End If

    End Sub
#End Region 'Failure Report Content
#Region "Database Navigation"
    Private Sub btnOldestRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOldestRecord.Click

        gb_ProcessEvents = False
        'Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([NEW ID])", Nothing)
        ' Failure report at the Bottom of the Sorted List (Highest Index)
        'Dim MinID As Integer = dgvFailureReportDataGridView.Rows(dgvFailureReportDataGridView.Rows.Count - 1).Cells("New ID").Value()
        Dim MinID As Integer
        If gMyFailureReportBindingSource.Filter IsNot Nothing Then
            Try
                MinID = gMyFailureReportDataTable.Compute("MIN([New ID])", gMyFailureReportBindingSource.Filter)
            Catch ex As Exception
                'catch silently...
                MinID = gMyFailureReportDataTable.Compute("MIN([New ID])", "")
            End Try
        Else
            MinID = gMyFailureReportDataTable.Compute("MIN([New ID])", "")
        End If

        gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MinID.ToString)
        txtPosition.Text = MinID.ToString
        gb_ProcessEvents = True
        If chkFailedSampleReady.CheckState = CheckState.Indeterminate Then
            chkFailedSampleReady.CheckState = CheckState.Unchecked
            Me.Refresh()
        End If
        SetApproverState()
        StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub btnPrevRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevRecord.Click
        Me.DoubleBuffered = True
        ' TableLayoutPanel1.Hide()
        Try
            gb_ProcessEvents = False
            'Determine a valid range 
            Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([NEW ID])", Nothing)
            Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([NEW ID])", Nothing)

            'This is the current index of the Gridview. It zero based with zero at the top and the Count - 1 at the bottom
            'regardless of how the data is sorted.
            Dim CurrentIndex As Integer
            If dgvFailureReportDataGridView.CurrentRow Is Nothing Then
                CurrentIndex = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
            Else
                CurrentIndex = dgvFailureReportDataGridView.CurrentRow.Index
            End If


            ' Current Failure Report Number - initialize to Current Value
            Dim FailureID As Integer = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value

            'This is the Failure Report Number of (moving down) position to move to on the grid
            Dim NewFailureID As Integer = 0

            'Try to get Next Failurlure report number Catch Error if it does not exist
            Try
                Dim MaxRowCountIndex As Integer = dgvFailureReportDataGridView.Rows.Count - 1
                If CurrentIndex >= 0 And CurrentIndex <= MaxRowCountIndex Then
                    NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex + 1).Cells("New ID").Value
                Else
                    'Set to "oldest value"
                    NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value
                End If

            Catch ex As Exception
                'Set to "out of range value"
                NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value
            End Try

            'Failure report numbers are in valid range
            If NewFailureID <= MaxID And NewFailureID >= MinID Then
                FailureID = NewFailureID
            End If

            'Set the Text to the Failure Report number
            txtPosition.Text = FailureID.ToString

            'Use the Binding source to select the record
            gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)

        Catch ex As Exception
            v_UpdateLog("Unhandled Exception clicking Next Record")
        End Try
        Me.Refresh()
        gb_ProcessEvents = True
        If chkFailedSampleReady.CheckState = CheckState.Indeterminate Then
            chkFailedSampleReady.CheckState = CheckState.Unchecked
            Me.Refresh()
        End If
        'Update the controls
        SetApproverState()
        StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
        ' TableLayoutPanel1.Show()
    End Sub
    Private Sub btnNextRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextRecord.Click
        gb_ProcessEvents = False
        'Determine a valid range 
        Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([NEW ID])", Nothing)
        Dim MinID As Integer = gMyFailureReportDataTable.Compute("MIN([NEW ID])", Nothing)
        'This is the current index of the Gridview. It zero based with zero at the top and the Count - 1 at the bottom
        'regardless of how the data is sorted.
        Dim CurrentIndex As Integer
        If dgvFailureReportDataGridView.CurrentRow Is Nothing Then
            CurrentIndex = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
        Else
            CurrentIndex = dgvFailureReportDataGridView.CurrentRow.Index
        End If


        ' Current Failure Report Number - initialize to Current Value
        Dim FailureID As Integer
        Try
            FailureID = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value
        Catch ex As Exception
            Exit Sub
        End Try




        'This is the Failure Report Number of (moving down) position to move to on the grid
        Dim NewFailureID As Integer

        'Try to get Next Failure report number, Catch Error if it does not exist
        Try
            If (CurrentIndex - 1) >= 0 Then
                NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex - 1).Cells("New ID").Value
            Else
                NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value
            End If
        Catch ex As Exception
            v_UpdateLog("Unhandled exeception Aquuiring 'Next Record'" + vbCrLf + ex.ToString)
            'Set back to "Current Value"
            NewFailureID = dgvFailureReportDataGridView.Rows(CurrentIndex).Cells("New ID").Value
        End Try

        'Failure report numbers must be valid range
        If NewFailureID <= MaxID And NewFailureID >= MinID Then
            FailureID = NewFailureID
        End If

        'Set the Text to the Failure Report number
        txtPosition.Text = FailureID.ToString

        'Use the Binding source to select the record
        gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)
        gb_ProcessEvents = True

        If chkFailedSampleReady.CheckState = CheckState.Indeterminate Then
            chkFailedSampleReady.CheckState = CheckState.Unchecked
            Me.Refresh()
        End If
        'Update the controls
        SetApproverState()
        StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub btnNewestRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewestRecord.Click
        'Dim MaxID As Integer = gMyFailureReportDataTable.Compute("MAX([NEW ID])", Nothing)
        gb_ProcessEvents = False
        Dim MaxID As Integer '= dgvFailureReportDataGridView.Rows(0).Cells("New ID").Value()
        If gMyFailureReportBindingSource.Filter IsNot Nothing Then
            Try
                MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", gMyFailureReportBindingSource.Filter)
            Catch ex As Exception
                'catch silently...
                MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", "")
            End Try
        Else
            MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", "")
        End If

        gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MaxID.ToString)
        txtPosition.Text = MaxID.ToString
        gb_ProcessEvents = True
        If chkFailedSampleReady.CheckState = CheckState.Indeterminate Then
            chkFailedSampleReady.CheckState = CheckState.Unchecked
            Me.Refresh()
        End If
        SetApproverState()
        StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
    End Sub
    Private Sub txtPosition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPosition.KeyDown
        If e.KeyCode = Keys.Enter Then
            gb_ProcessEvents = False
            e.SuppressKeyPress = True

            'Find the Max and Min ID
            Dim MaxID As Integer ' = gMyFailureReportDataTable.Compute("MAX([NEW ID])", Nothing)
            Dim MINID As Integer ' = gMyFailureReportDataTable.Compute("MIN([NEW ID])", Nothing)

            'Use the filter to contstrain the results if they exist..
            If gMyFailureReportBindingSource.Filter IsNot Nothing Then
                Try

                    MINID = gMyFailureReportDataTable.Compute("MIN([New ID])", gMyFailureReportBindingSource.Filter)
                    MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", gMyFailureReportBindingSource.Filter)
                Catch ex As Exception
                    'catch silently...remove Filter....
                    MINID = gMyFailureReportDataTable.Compute("MIN([New ID])", "")
                    MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", "")
                End Try
            Else
                MINID = gMyFailureReportDataTable.Compute("MIN([New ID])", "")
                MaxID = gMyFailureReportDataTable.Compute("MAX([New ID])", "")
            End If

            Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
            Dim ID As Integer

            ID = CInt((Val(txtPosition.Text)))
            Try
                If ID > MaxID Or ID < MINID Then
                    'If the user enters a value that is outside the current bounds, restore orginal position.
                    txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion
                Else
                    txtPosition.Text = ID.ToString
                    'Returns -1 if the record is not found...
                    Dim MyPosition As Integer = gMyFailureReportBindingSource.Find("New ID", txtPosition.Text)

                    If MyPosition >= 0 Then
                        gMyFailureReportBindingSource.Position = MyPosition
                    Else
                        MsgBox("Record not Found, remove any Filters and Try again.")
                        txtPosition.Text = CurrentRow.Item("New ID") 'implicit conversion
                    End If

                End If
            Catch
            End Try

            gb_ProcessEvents = True
            SetApproverState()
            StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
        End If

    End Sub
#End Region '"Database Navigation"
#Region "Failure_Report Datagridview Events"
    Private Sub Failure_ReportDataGridView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvFailureReportDataGridView.SelectionChanged



        Try
            If gb_ProcessEvents Then
                Me.SuspendLayout()
                TableLayoutPanel3Rows.SuspendLayout()
                pnlReportHeader.SuspendLayout()
                dgvFailureReportDataGridView.SuspendLayout()
                'setapproverstate()
                SetApproverState()
                dgvFailureReportDataGridView.ResumeLayout()
                Me.ResumeLayout()
                TableLayoutPanel3Rows.ResumeLayout()
                pnlReportHeader.ResumeLayout()
            End If
            'set the index to the most recent failure report at load

            Dim currentrow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)

            'gmyfailurereportbindingsource.position = gmyfailurereportbindingsource.find("new id", maxid.tostring)
            txtPosition.Text = currentrow.Item("new id")
            UpdateAttachmentList()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Failure_ReportDataGridView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvFailureReportDataGridView.Sorted
        '  gDataGridSelectionChangSafeToRunCode = True
        dgvFailureReportDataGridView.AllowUserToResizeColumns = True
        'dgvFailureReportDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
    End Sub
    Private Sub Failure_ReportDataGridView_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFailureReportDataGridView.MouseDown
        gb_ProcessEvents = False
    End Sub
    Private Sub Failure_ReportDataGridView_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvFailureReportDataGridView.MouseUp
        gb_ProcessEvents = True
        dgvFailureReportDataGridView.AllowUserToResizeColumns = True
        'dgvFailureReportDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
    End Sub
    Private Sub Failure_ReportDataGridView_ColumnWidthChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvFailureReportDataGridView.ColumnWidthChanged
        If e.Column.Width > 400 Then
            e.Column.Width = 400
        End If
        dgvFailureReportDataGridView.AllowUserToResizeColumns = True
        ' dgvFailureReportDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
    End Sub
    Private Sub Failure_ReportDataGridView_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles dgvFailureReportDataGridView.Scroll
        dgvFailureReportDataGridView.AllowUserToResizeColumns = True
        'dgvFailureReportDataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders)
    End Sub
    Private Sub Failure_ReportDataGridView_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFailureReportDataGridView.RowEnter


    End Sub
    Private Sub Failure_ReportDataGridView_RowValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFailureReportDataGridView.RowValidated
        Try
            If gb_ProcessEvents Then

                'Set the index to the most recent failure report at load
                Dim CurrentRow As DataRowView = DirectCast(gMyFailureReportBindingSource.Current, DataRowView)
                'gMyFailureReportBindingSource.Position = gMyFailureReportBindingSource.Find("New ID", MaxID.ToString)
                txtPosition.Text = CurrentRow.Item("New Id")
                UpdateAttachmentList()
                'Me.SuspendLayout()
                'TableLayoutPanel1.SuspendLayout()
                'pnlReportHeader.SuspendLayout()
                'Failure_ReportDataGridView.SuspendLayout()
                ''SetApproverState()
                'SetApproverState()
                'Failure_ReportDataGridView.ResumeLayout()
                'Me.ResumeLayout()
                'TableLayoutPanel1.ResumeLayout()
                'pnlReportHeader.ResumeLayout()


            End If


        Catch ex As Exception

        End Try
    End Sub
#End Region 'Failure_Report Datagridview Events
#Region "Checkboxes"
    Private Sub chkFR_ReadyForReview_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFR_ReadyForReview.CheckedChanged


        If chkFR_ReadyForReview.CheckState = CheckState.Checked And gb_ProcessEvents = True Then
            gb_ProcessEvents = False
            If mtxtDateCorrected.Text.Trim = "" Then
                MsgBox("'Date Corrected' cannot be blank")
                chkFR_ReadyForReview.CheckState = CheckState.Unchecked
            ElseIf cbCorrectedBy.Text.Trim = "" Then
                MsgBox("'Corrected By' cannot be blank")
                chkFR_ReadyForReview.CheckState = CheckState.Unchecked
            ElseIf rtxtCorrectiveAction.Text.Trim = "" Then
                MsgBox("Corrective Action cannot be Blank")
                chkFR_ReadyForReview.CheckState = CheckState.Unchecked
            Else
                ' ChkFR_Approved.BackColor = Color.Green
            End If
            gb_ProcessEvents = True
        Else
            ' chkFR_ReadyForReview.BackColor = Me.BackColor
        End If
    End Sub
    Private Sub chkTCC_ApprovalRequired_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If gb_ProcessEvents = True Then
            'DisplayShadowControl()
            'SetApproverState()
            StateMachine(_EditState, _EditState, _CurrentUser.AccessLevel)
        End If
    End Sub
    Private Sub chkAnomely_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnomely.CheckStateChanged
        chkPass.AutoCheck = False
        If chkAnomely.Checked = True Then
            chkPass.Checked = True
            chkFail.Checked = False
        Else
            chkPass.Checked = False
            chkFail.Checked = True
        End If
        If gb_ProcessEvents = True Then
            ' DisplayShadowControl()
            SetApproverState()
        End If
    End Sub

    Private Sub chkPass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPass.CheckedChanged
        Try
            chkPass.AutoCheck = False
            'If chkPass.Checked = True Then
            '    chkFail.CheckState = CheckState.Unchecked
            '    chkAnomely.Enabled = True
            '    chkAnomely.Checked = True
            'Else
            '    chkFail.CheckState = CheckState.Checked
            '    chkAnomely.CheckState = CheckState.Unchecked
            'End If
            'If gb_ProcessEvents = True Then
            '    SetApproverState()
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Fail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFail.CheckedChanged
        Try
            chkPass.AutoCheck = False
            If chkFail.Checked = True Then
                chkPass.CheckState = CheckState.Unchecked
                chkAnomely.CheckState = CheckState.Unchecked
            Else
                chkPass.CheckState = CheckState.Checked
                chkAnomely.CheckState = CheckState.Checked
            End If
            If gb_ProcessEvents Then
                SetApproverState()
            End If

        Catch ex As Exception

        End Try
    End Sub


#End Region '"Checkboxes"
#Region "DateTimePickers and Masked Textboxes"

    Private Sub dtpDateCorrected_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateCorrected.CloseUp
        If gb_ProcessEvents = True And dtpDateCorrected.Visible = True Then
            Dim MyDate As Date = dtpDateCorrected.Value
            mtxtDateCorrected.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateCorrected_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateCorrected.ValueChanged
        If gb_ProcessEvents = True And dtpDateCorrected.Visible = True Then
            Dim MyDate As Date = dtpDateCorrected.Value
            mtxtDateCorrected.Text = MyDate.ToShortDateString
        End If

    End Sub

    Private Sub dtpDateFailed_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFailed.CloseUp
        If gb_ProcessEvents = True And dtpDateFailed.Visible = True Then
            Dim MyDate As Date = dtpDateFailed.Value
            mtxtDateFailed.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateFailed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFailed.ValueChanged
        If gb_ProcessEvents = True And dtpDateFailed.Visible = True Then
            Dim MyDate As Date = dtpDateFailed.Value
            mtxtDateFailed.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateClosed_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateClosed.CloseUp
        If gb_ProcessEvents = True And dtpDateClosed.Visible = True Then
            Dim MyDate As Date = dtpDateClosed.Value
            mtxtDateClosed.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateClosed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateClosed.ValueChanged
        If gb_ProcessEvents = True And dtpDateClosed.Visible = True Then
            Dim MyDate As Date = dtpDateClosed.Value
            mtxtDateClosed.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateApproved_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateApproved.CloseUp
        If gb_ProcessEvents = True And dtpDateApproved.Visible = True Then
            Dim MyDate As Date = dtpDateApproved.Value
            mtxtDateApproved.Text = MyDate.ToShortDateString
        End If
    End Sub

    '************************Added 12.9.2022
    Private Sub dtpDateFailedSampleReady_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFailedSampleReady.CloseUp
        If gb_ProcessEvents = True And dtpDateFailedSampleReady.Visible = True Then
            Dim MyDate As Date = dtpDateFailedSampleReady.Value
            mtxtDateFailedSampleReady.Text = MyDate.ToShortDateString
        End If
    End Sub

    Private Sub dtpDateFailedSampleReady_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateFailedSampleReady.ValueChanged
        If gb_ProcessEvents = True And dtpDateFailedSampleReady.Visible = True Then
            Dim MyDate As Date = dtpDateFailedSampleReady.Value
            mtxtDateFailedSampleReady.Text = MyDate.ToShortDateString
        End If

    End Sub
    '**************************

    Private Sub dtpDateApproved_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateApproved.ValueChanged
        If gb_ProcessEvents = True And dtpDateApproved.Visible = True Then
            Dim MyDate As Date = dtpDateApproved.Value
            mtxtDateApproved.Text = MyDate.ToShortDateString
        End If
    End Sub
    Private Sub mtxtDateFailedSampleReady_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateFailedSampleReady.Validated
        If mtxtDateFailedSampleReady.Text.Trim = "" Then
            'dgvFailureReportDataGridView.Update()
            'Dim FieldName As String = mtxtDateClosed.DataBindings.Item("Text").BindingMemberInfo.BindingField
            mtxtDateFailedSampleReady.DataBindings.Item("Text").DataSourceNullValue = System.DBNull.Value
        End If

    End Sub
    Private Sub mtxtDateFailedSampleReady_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mtxtDateFailedSampleReady.Validating
        If mtxtDateFailedSampleReady.Text.Trim = "" Then
            dgvFailureReportDataGridView.Update()
            'Dim FieldName As String = mtxtDateClosed.DataBindings.Item("Text").BindingMemberInfo.BindingField
            mtxtDateFailedSampleReady.DataBindings.Item("Text").DataSourceNullValue = System.DBNull.Value
            '******************************************************************************************************
            Dim iColumnCount As Integer 'Column count of the Failure Report Database

            Dim MyRecord As New cCustomDataBaseAccess.cTable 'Record to hold database Changes

            'Datatable to hold the Database Table Schema inotmation ( i.E. Column Name, Datatype, Size etc...
            Dim MyLocalDatatable As New DataTable

            'Get the Schema Information
            MyLocalDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gMyFailureReportDBConnection, "Failure Report")

            'get the Column count
            iColumnCount = MyLocalDatatable.Rows.Count

            'Set the Table Name
            MyRecord.TableName = "Failure Report"

            'Find the correct row to update
            Dim RecordIndex As Integer = 0
            For Each row In MyLocalDatatable.Rows
                If MyLocalDatatable.Rows(RecordIndex).Item("ColumnName") = "Failed_Sample_Ready_Date" Then
                    Exit For
                End If
                RecordIndex += 1
            Next

            'Initalize the columns 
            MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

            'populate the Record
            MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn("Failed_Sample_Ready_Date", System.DBNull.Value, MyLocalDatatable.Rows(RecordIndex).Item("DataType"), True))

            'Try to perform the update
            Try

                cCustomDataBaseAccess.UpdateExistingRecord(MyRecord, gMyFailureReportDBConnection, "New ID", txtPosition.Text.Trim)

            Catch ex As Exception 'Catch any errors
                MsgBox("Error Updating Record " + vbCrLf + ex.ToString)
            End Try
        End If

    End Sub
    Private Sub mtxtDateClosed_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateClosed.Validated
        If mtxtDateClosed.Text.Trim = "" Then
            'Dim FieldName As String = mtxtDateClosed.DataBindings.Item("Text").BindingMemberInfo.BindingField
            mtxtDateClosed.DataBindings.Item("Text").DataSourceNullValue = System.DBNull.Value
        End If
    End Sub
    Private Sub mtxtDateClosed_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mtxtDateClosed.Validating
        If mtxtDateClosed.Text.Trim = "" Then
            dgvFailureReportDataGridView.Update()
            'Dim FieldName As String = mtxtDateClosed.DataBindings.Item("Text").BindingMemberInfo.BindingField
            mtxtDateClosed.DataBindings.Item("Text").DataSourceNullValue = System.DBNull.Value


            '******************************************************************************************************
            Dim iColumnCount As Integer 'Column count of the Failure Report Database

            Dim MyRecord As New cCustomDataBaseAccess.cTable 'Record to hold database Changes

            'Datatable to hold the Database Table Schema inotmation ( i.E. Column Name, Datatype, Size etc...
            Dim MyLocalDatatable As New DataTable

            'Get the Schema Information
            MyLocalDatatable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(gMyFailureReportDBConnection, "Failure Report")

            'get the Column count
            iColumnCount = MyLocalDatatable.Rows.Count

            'Set the Table Name
            MyRecord.TableName = "Failure Report"

            'Find the correct row to update
            Dim RecordIndex As Integer = 0
            For Each row In MyLocalDatatable.Rows
                If MyLocalDatatable.Rows(RecordIndex).Item("ColumnName") = "Date Closed" Then
                    Exit For
                End If
                RecordIndex += 1
            Next

            'Initalize the columns 
            MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

            'populate the Record
            MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn("Date Closed", System.DBNull.Value, MyLocalDatatable.Rows(RecordIndex).Item("DataType"), True))

            'Try to perform the update
            Try

                cCustomDataBaseAccess.UpdateExistingRecord(MyRecord, gMyFailureReportDBConnection, "New ID", txtPosition.Text.Trim)

            Catch ex As Exception 'Catch any errors
                MsgBox("Error Updating Record " + vbCrLf + ex.ToString)
            End Try
        End If
    End Sub
    Private Sub mtxtDateFailed_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateFailed.TextChanged
        Try
            Dim MyDate As DateTime

            If mtxtDateFailed.Text.Trim <> "" Then
                DateTime.TryParse(mtxtDateFailed.Text, MyDate)
                mtxtDateFailed.Text = MyDate
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub mtxtDateApproved_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateApproved.TextChanged
        Try
            Dim MyDate As DateTime

            If mtxtDateApproved.Text.Trim <> "" Then
                DateTime.TryParse(mtxtDateApproved.Text, MyDate)
                mtxtDateApproved.Text = MyDate
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub mtxtDateFailedSampleReady_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateFailedSampleReady.TextChanged
        Try
            Dim MyDate As DateTime

            If mtxtDateFailedSampleReady.Text.Trim <> "" Then
                DateTime.TryParse(mtxtDateFailedSampleReady.Text, MyDate)
                mtxtDateFailedSampleReady.Text = MyDate
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mtxtDateCorrected_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxtDateCorrected.TextChanged
        Try
            Dim MyDate As DateTime

            If mtxtDateCorrected.Text.Trim <> "" Then
                DateTime.TryParse(mtxtDateCorrected.Text, MyDate)
                mtxtDateCorrected.Text = MyDate
            End If

        Catch ex As Exception

        End Try

    End Sub
#End Region '""DateTimePickers and Masked Textboxes""
#Region "For Test Only"
    Private Sub btnSetAccessLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetAccessLevel.Click
        _CurrentUser.AccessLevel = cbAccesslevel.SelectedIndex
        If cbEditState.SelectedIndex > 0 Then
            SetAccessState(True)
        Else
            SetAccessState(False)
        End If

    End Sub

    Private Sub btnSetEditState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetEditState.Click
        Dim NewState As eEditState
        Select Case cbEditState.SelectedIndex
            Case 0
                NewState = eEditState.READ_ONLY
            Case 1
                NewState = eEditState.EDIT_CREATE_NEW
            Case 2
                NewState = eEditState.EDIT_FR_DETAILS
            Case 3
                NewState = eEditState.EDIT_FR_CORRECTIVE_ACTION
            Case 4
                NewState = eEditState.EDIT_POWER
            Case 5
                NewState = eEditState.EDIT_SET_APPROVALS
            Case 6
                NewState = eEditState.EDIT_ADMIN
            Case Else
                NewState = eEditState.READ_ONLY
        End Select

        StateMachine(NewState, _EditState, _CurrentUser.AccessLevel)
    End Sub
#End Region 'For Test Only
#Region "Info and Error Reporting"
    Public Sub v_MessageBox(ByVal Message As String, Optional ByVal VerboseLevel As Integer = 0)
        If VerboseLevel > gVerboseLevel Then
            MsgBox(Message)
        End If
    End Sub
    Public Delegate Sub UpDateLogDelegate(ByVal message As String)
    ''' <summary>
    '''  Thread safe UpdateLog sets Logdata's items.add property
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub v_UpdateLog(ByVal message As String, Optional ByVal VerboseLevel As eLogLevel = eLogLevel.MAX) 'assume worse case 
        ' if modifying Output data is not thread safe
        If lbLogData.InvokeRequired Then
            ' use inherited method Invoke to execute DisplayMessage
            ' via a Delegate
            Try
                Invoke(New UpDateLogDelegate(AddressOf v_UpdateLog), New Object() {message})

            Catch ex As Exception

            End Try

            ' OK to modify output data in current thread
            ' write everything in verbose mode else only if flagged   
        ElseIf gbVerbose = True Or gbWriteToLog = True Or VerboseLevel > gVerboseLevel Then

            Try
                'add to log

                If lbLogData.Items.Count > 1000 Then 'Save off after 1000 reached in a session
                    If SaveListboxData(lbLogData, False) = False Then 'prompt for save path
                        v_MessageBox("Unable to Save Log Data Back-up!", 4)
                        v_UpdateLog("Unable to Save Log Data Back Up" + Now.ToString)
                    Else
                        lbLogData.Items.Clear()
                    End If
                End If
                lbLogData.Items.Add(message)
            Catch ex As Exception
                MsgBox("Error Trying to add Message: << " + message + ">>, to the Log, clearing Log..." + vbCrLf + ex.ToString) ' Always Report with popup if unable to write to log...
                ' lbLogData.Items.Clear()
            End Try

        End If
    End Sub ' v_UpdateLog()
    Private Sub Failure_ReportDataGridView_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvFailureReportDataGridView.DataError
        'MessageBox.Show("Error:  " & e.Context.ToString())

        If (e.Context = DataGridViewDataErrorContexts.Commit) _
            Then
            v_UpdateLog("Commit error" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("Commit error", eLogLevel.MAX)
            Enable_Navigation(False)

        End If

        If (e.Context = DataGridViewDataErrorContexts.CurrentCellChange) Then
            v_UpdateLog("Cell Change Error" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("Cell Change Error", eLogLevel.MAX)
            Enable_Navigation(False)
        End If

        If (e.Context = DataGridViewDataErrorContexts.Parsing) Then
            v_UpdateLog("parsing error" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("parsing error", eLogLevel.MAX)
            Enable_Navigation(False)
        End If

        If (e.Context = DataGridViewDataErrorContexts.LeaveControl) Then
            v_UpdateLog("leave control error" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("leave control error", eLogLevel.MAX)
            Enable_Navigation(False)
        End If

        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"
            v_MessageBox("error", eLogLevel.MAX)
            Enable_Navigation(False)
            e.ThrowException = False
        ElseIf (e.GetType.ToString = "System.Windows.Forms.DataGridViewDataErrorEventArgs") Then
            Enable_Navigation(False)
            v_UpdateLog("No Results Returned" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            '  v_MessageBox("No Results Returned", eLogLevel.MAX)
        Else
            v_UpdateLog("Unknown Datagridview Error" + vbCrLf + e.ToString, eLogLevel.ERRORS_ONLY)
            v_MessageBox("Unknown Datagridview Error" + vbCrLf + e.ToString + vbCrLf + _
            "Error Type: " + ((e.GetType)).ToString, eLogLevel.MAX)
            Enable_Navigation(False)
        End If



    End Sub
    Private Function SaveListboxData(ByVal MyListBox As ListBox, Optional ByVal PromptForSave As Boolean = False) As Boolean

        Dim Success As Boolean = True 'Assume Success
        Dim Index As Integer
        Dim SaveDate As Date
        Dim result As New System.Text.StringBuilder
        Dim SavePath As String = Nothing
        Dim SaveDialog As New SaveFileDialog

        If PromptForSave = True Then
            SaveDialog.CheckPathExists = True
            SaveDialog.Filter = "Failure Report Log files (*.frlog)|*.frlog|Log files (*.log)|*.log|All files (*.*)|*.*"

            SaveDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\FR_DATABASE_LOG\"

            'Create Directory if needed
            If (Not System.IO.Directory.Exists(SaveDialog.InitialDirectory)) Then
                System.IO.Directory.CreateDirectory(SaveDialog.InitialDirectory)
            End If

            SaveDialog.ShowDialog()
            SavePath = SaveDialog.FileName
            If SavePath = Nothing Then
                Return False
            End If
        Else 'AutoSave
            SaveDialog.CheckPathExists = False
            SaveDialog.InitialDirectory = My.Computer.FileSystem.CurrentDirectory + "\FR_DATABASE_LOG\"

            'Create Directory if needed
            If (Not System.IO.Directory.Exists(SaveDialog.InitialDirectory)) Then
                System.IO.Directory.CreateDirectory(SaveDialog.InitialDirectory)
            End If

            'SaveDialog.FileName = "AutoLog" + Now.ToString.Replace("/", "_").Replace(":", "_") + "log"
            SavePath = My.Computer.FileSystem.CurrentDirectory + "\FR_DATABASE_LOG\" + "AutoLog" + Now.ToString.Replace("/", "_").Replace(":", "_") + ".frlog"
        End If
        'put all or displayed data array in big string
        Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(SavePath, False)
        SaveDate = Now
        result.Append("FR Database File")
        result.Append("Date: ").AppendLine(SaveDate.ToShortDateString)
        result.Append("Time: ").AppendLine(SaveDate.ToShortTimeString)
        result.Append("------------------------------------------" + vbNewLine)
        For Index = 0 To MyListBox.Items.Count - 1
            result.Append(MyListBox.Items(Index).ToString + vbNewLine)
        Next
        outFile.WriteLine(result)
        outFile.Close()
        Return Success
    End Function
    Private Sub btnSaveLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLog.Click
        If SaveListboxData(lbLogData, True) = False Then 'prompt for save path
            v_MessageBox("Unable to Save Log Data!", 4)
            v_UpdateLog("Unable to Save Log Data" + Now.ToString)
        End If

    End Sub
#End Region '"Info and Error Reporting"

#Region "Test Equipment Used"
    Private Sub btnTestEquipment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestEquipment.Click
        Try
            If frmMySelectTestEquipment IsNot Nothing Then
                frmMySelectTestEquipment = Nothing
                'TestEquipment = New frmTestEquipment
                'Else
                '  TestEquipment.btnAddSelelected.Enabled = False
            End If
            frmMySelectTestEquipment = New frmSelectTestEquipment

            If frmMySelectTestEquipment.ShowDialog = Windows.Forms.DialogResult.OK Then
                rtxtTestEquipmentIDlist.Text = frmMySelectTestEquipment.Tag
                ' dgvTestEquipmentUsed.DataSource = TestEquipment.gSelectedTestEquipment.Copy
                'dgvTestEquipmentUsed.ReadOnly = True
            End If
            frmMySelectTestEquipment.Dispose()
            frmMySelectTestEquipment = Nothing

        Catch ex As Exception
            MsgBox("Error adding Test Equiment to FR." + vbCrLf + ex.ToString)
        End Try

    End Sub
    Private Sub rtxtTestEquipmentIDlist_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtTestEquipmentIDlist.TextChanged
        Dim dTestEquipmentTable As New DataTable
        Dim MyFilter = ""
        Try
            If rtxtTestEquipmentIDlist.Text.Trim <> "" Then
                Dim TestIDs() As String = rtxtTestEquipmentIDlist.Text.ToString.Split(";")
                MyFilter = "WHERE INDEX = " + TestIDs(0)
                For i = 1 To TestIDs.Count - 1
                    MyFilter = MyFilter + " OR INDEX = " + TestIDs(i)

                Next
                dTestEquipmentTable = gMyCustomDBAccess.GetData(gMyMeterSpecDBConnection, "TEST_EQUIPMENT", MyFilter)
            End If

            dgvTestEquipmentUsed.DataSource = dTestEquipmentTable
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rtxtTestEquipmentIDlist_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtxtTestEquipmentIDlist.Validated
        Dim dTestEquipmentTable As New DataTable
        Dim MyFilter = ""
        Try
            If rtxtTestEquipmentIDlist.Text.Trim <> "" Then
                Dim TestIDs() As String = rtxtTestEquipmentIDlist.Text.ToString.Split(";")


                MyFilter = "WHERE INDEX = " + TestIDs(0)
                For i = 1 To TestIDs.Count - 1
                    MyFilter = MyFilter + "OR INDEX = " + TestIDs(i)

                Next
                dTestEquipmentTable = gMyCustomDBAccess.GetData(gMyMeterSpecDBConnection, "TEST_EQUIPMENT", MyFilter)
            End If

            dgvTestEquipmentUsed.DataSource = dTestEquipmentTable
        Catch ex As Exception

        End Try
    End Sub
#End Region '"Test Equipment Used"



    Private Function ExportDataGridView(ByVal MyDataGridView As DataGridView, Optional ByVal PromptForSave As Boolean = False) As Boolean

        Dim Success As Boolean = True 'Assume Success
        'Dim Index As Integer
        Dim SaveDate As Date
        Dim result As New System.Text.StringBuilder
        Dim SavePath As String = Nothing
        Dim SaveDialog As New SaveFileDialog

        If PromptForSave = True Then
            SaveDialog.CheckPathExists = True
            SaveDialog.Filter = "Failure Report CSV Export (*.csv)|*.csv|txt files (*.txt)|*.csv|All files (*.*)|*.*"

            SaveDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments + "\FR_DATA_EXPORT\"

            'Create Directory if needed
            If (Not System.IO.Directory.Exists(SaveDialog.InitialDirectory)) Then
                System.IO.Directory.CreateDirectory(SaveDialog.InitialDirectory)
            End If

            SaveDialog.ShowDialog()
            SavePath = SaveDialog.FileName
            If SavePath = Nothing Then
                Return False
            End If
        Else 'AutoSave
            SaveDialog.CheckPathExists = False
            SaveDialog.InitialDirectory = My.Computer.FileSystem.CurrentDirectory + "\FR_DATA_EXPORT\"

            'Create Directory if needed
            If (Not System.IO.Directory.Exists(SaveDialog.InitialDirectory)) Then
                System.IO.Directory.CreateDirectory(SaveDialog.InitialDirectory)
            End If

            'SaveDialog.FileName = "AutoLog" + Now.ToString.Replace("/", "_").Replace(":", "_") + "log"
            SavePath = My.Computer.FileSystem.CurrentDirectory + "\FR_DATA_EXPORT\" + "AutoExport" + Now.ToString.Replace("/", "_").Replace(":", "_") + ".csv"
        End If
        'put all or displayed data array in big string
        ' Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(SavePath, False)
        SaveDate = Now
        result.Append("FR Database Data Export")
        result.Append("Date: ").AppendLine(SaveDate.ToShortDateString)
        result.Append("Time: ").AppendLine(SaveDate.ToShortTimeString)
        result.Append("User: ").AppendLine(_CurrentUser.FirstName + " " + _CurrentUser.LastName)
        result.Append("------------------------------------------" + vbNewLine)

        Using Response As New IO.StreamWriter(SavePath)

            'Response.Clear()
            'Response.Buffer = True
            'Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.csv")
            'Response.Charset = ""
            'Response.ContentType = "application/text"

            'GridView1.AllowPaging = False
            'GridView1.DataBind()

            'Dim sb As New StringBuilder()
            For k As Integer = 0 To MyDataGridView.Columns.Count - 1
                'add separator
                If MyDataGridView.Columns(k).Visible = True Then
                    result.Append(MyDataGridView.Columns(k).HeaderText + ","c)
                End If

            Next
            'append new line
            result.Append(vbCr & vbLf)
            For i As Integer = 0 To MyDataGridView.Rows.Count - 1
                If MyDataGridView.Rows(i).Visible = True Then
                    For k As Integer = 0 To MyDataGridView.Columns.Count - 1
                        'add separator
                        If MyDataGridView.Columns(k).Visible = True Then
                            Dim MyString As String = MyDataGridView.Rows(i).Cells(k).Value.ToString.Replace(",", ";")
                            MyString = MyString.Replace(vbCr, " ")
                            MyString = MyString.Replace(vbLf, " ")
                            MyString = MyString.Replace(vbCrLf, " ")
                            result.Append(MyString + ","c)
                            'sb.Append(MyDataGridView.Rows(i).Cells(k).Value.ToString + ","c)
                        End If
                    Next
                    'append new line
                    result.Append(vbCrLf)
                End If

            Next
            Response.Write(result.ToString())
        End Using
        'Response.Flush()

        Try
            Process.Start(SavePath)
        Catch
        End Try


        'outFile.WriteLine(result)
        'outFile.Close()
        Return Success
    End Function
    Private Sub tsmExportDataGridItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmExportDataGridItems.Click

        If ExportDataGridView(dgvFailureReportDataGridView, True) = False Then
            MsgBox("Export aborted!")
        End If

    End Sub
    Private Sub tsmOptionsPromptBeforeSaving_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmOptionsPromptBeforeSaving.Click
        If tsmOptionsPromptBeforeSaving.CheckState = CheckState.Checked Then
            Dim MyRegKey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\FailureReportDatabase\tsmSaveFR")
            MyRegKey.SetValue("DontShowAgain", False)
        ElseIf tsmOptionsPromptBeforeSaving.CheckState = CheckState.Unchecked Then
            Dim MyRegKey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\FailureReportDatabase\tsmSaveFR")
            MyRegKey.SetValue("DontShowAgain", True)
        Else


        End If
    End Sub




    ''' <summary>
    ''' This function changes the Font size of the Body section of the failure report.
    ''' </summary>
    ''' <param name="MaxFontSize"></param>
    ''' <param name="MinFontSize"></param>
    ''' <param name="DefaultFontSize"></param>
    ''' <returns>True = Success; false = error</returns>
    ''' <remarks>Frank Boudreau 1.28.2019</remarks>

    Public Function ChangeFontSize(Optional ByVal MaxFontSize As Double = 72, Optional ByVal MinFontSize As Double = 6.0, Optional ByVal DefaultFontSize As Double = 10) As Boolean
        Dim bSuccess As Boolean = True
        Try


            Dim Fontsize As Double
            If IsNumeric(XboXFontSize.Text.Trim) Then
                Fontsize = Val(XboXFontSize.Text.Trim)
            Else
                Fontsize = DefaultFontSize
            End If

            Dim MyFont As Font = rtxtDescription.Font
            If Fontsize >= MinFontSize Or Fontsize <= MaxFontSize Then
                MyFont = New Font(rtxtDescription.Font.FontFamily, Fontsize, rtxtDescription.Font.Style, GraphicsUnit.Pixel)
            ElseIf Fontsize > MaxFontSize Then
                MyFont = New Font(rtxtDescription.Font.FontFamily, MaxFontSize, rtxtDescription.Font.Style, GraphicsUnit.Pixel)
            ElseIf Fontsize < MinFontSize Then
                MyFont = New Font(rtxtDescription.Font.FontFamily, MinFontSize, rtxtDescription.Font.Style, GraphicsUnit.Pixel)
            End If

            rtxtDescription.Font = MyFont
            rtxtCorrectiveAction.Font = MyFont
            rtxtEngineeringNotes.Font = MyFont
            rtxtTCC_Comments.Font = MyFont
        Catch ex As Exception
            bSuccess = False
        End Try
        Return bSuccess
    End Function


    Private Sub XboXFontSize_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles XboXFontSize.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChangeFontSize()
        End If
    End Sub



    Private Sub btnSelectTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectTest.Click
        If MyTestSelector Is Nothing Then
            MyTestSelector = New frmSelectTest
        End If
        MyTestSelector._bFilterTestsbyType = False
        MyTestSelector._TestType = cbTestType.Text
        'Dim MyDialogResult As DialogResult = MyTestSelector.ShowDialog()
        MyTestSelector.ShowDialog()
        If MyTestSelector._DialogResult = Windows.Forms.DialogResult.OK Then
            txtTest.Text = MyTestSelector.Testname
            cbTestType.Text = MyTestSelector.xcbTestType.Text
        Else
            'Now Test Selected
        End If

    End Sub


    Dim MyProjectSelector As frmProjectDialog


    Private Sub tmsProjectSlectProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmsProjectSelectProject.Click

        If MyProjectSelector Is Nothing Then
            MyProjectSelector = New frmProjectDialog
        End If

        Dim MyDialogResult As MsgBoxResult

        MyDialogResult = MyProjectSelector.ShowDialog()
        If Not MyProjectSelector.gSelectedProjectID Is Nothing Then
            cbProjectNumber.Text = MyProjectSelector.gSelectedProjectID
            txtProject.Text = MyProjectSelector.gSelectedProjectName
        Else
            MsgBox("No Project Number Selected")
        End If

    End Sub


    Private Sub btnShowTestLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowTestLevel.Click
        MsgBox(cbTestLevel.Text)
    End Sub


    Private Sub txtAttachments_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAttachments.TextChanged

    End Sub

    Private Sub MenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub



    Private Sub btnNotifyProjectLead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotifyProjectLead.Click
        Dim App As outlook.Application = New outlook.Application
        CreateSendItem(App)
    End Sub

    Private Sub pnlTestinfo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlTestinfo.Paint

    End Sub

    Private Sub TableLayoutPanel14_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel14.Paint

    End Sub

    Private Sub lblMeter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMeter.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Me.Scale(New SizeF(0.5, 0.5))
        Me.AutoScaleMode = AutoScaleMode.Font
        ' Me.Font = New Font("Arial", 20)
        Dim ScreenSize As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
        Dim MySize As New System.Drawing.Size
        MySize = Me.Size
        Me.Size = New System.Drawing.Size(Convert.ToInt32(0.5 * ScreenSize.Width), Convert.ToInt32(0.5 * ScreenSize.Height))
        Dim NewSize As New SizeF(ScreenSize.Height / Me.Size.Height, ScreenSize.Width / Me.Width * 0.9)
        'Me.Scale(NewSize)
        Dim MyFont As Font
        MyFont = Me.Font
        Me.Font = New Font("Arial", MyFont.Size * ScreenSize.Height / Me.Size.Height)


    End Sub

End Class