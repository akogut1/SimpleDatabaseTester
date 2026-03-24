''' <summary>
''' Class encapsulated the MeterSpec database Schema...this class must be aligned with the 
''' Database tables...
''' </summary>
''' <remarks></remarks>
Public Class cMeterSpecDBDef

    ''' <summary>
    ''' AMR table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public AMR As cAMR
    ''' <summary>
    ''' AMR_REV Table in METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public AMR_REV As cAMR_REV
    ''' <summary>
    ''' Approver_Type Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public APPROVER_DISCIPLINE As cAPPROVER_DISCIPLINE
    ''' <summary>
    ''' Approvers Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public APPROVERS As cAPPROVERS
    ''' <summary>
    ''' Base Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public BASE As cBASE
    ''' <summary>
    ''' Form Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public FORM As cFORM
    ''' <summary>
    ''' METER_FIRMWARE Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public METER_FIRMWARE As cMETER_FIRMWARE
    ''' <summary>
    ''' PAC TEST LEVEL Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public PAC_LEVEL As cPAC_LEVEL
    ''' <summary>
    ''' Project Information Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public PROJECT As cProject
    ''' <summary>
    ''' Standard test information i.e. ESD, EFTB, LOAD Performance...
    ''' </summary>
    ''' <remarks></remarks>
    Public STANDARD_TEST As cSTANDARD_TESTS
    ''' <summary>
    ''' TCC member Table in METER_SPECS database (no longer used)
    ''' </summary>
    ''' <remarks></remarks>
    Public TCC1 As New cTCC1
    ''' <summary>
    ''' TCC member Table in METER_SPECS database (no longer used)
    ''' </summary>
    ''' <remarks></remarks>
    Public TCC2 As New cTCC2
    ''' <summary>
    ''' TCC memeber Table in METER_SPECS database (no longer used)
    ''' </summary>
    ''' <remarks></remarks>
    Public TCC3 As New cTCC3
    ''' <summary>
    ''' TCC member Table in METER_SPECS database (no longer used)
    ''' </summary>
    ''' <remarks></remarks>
    Public TCC4 As New cTCC4
    ''' <summary>
    ''' TEST EQUIPMENT INFORMATIONTable in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public TEST_EQUIPMENT As cTEST_EQUIPMENT
    ''' <summary>
    ''' TEST Equipment Type (EMC, Acuracy, Safety etc...) Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public TEST_EQUIPMENT_TYPE As cTEST_EQUIPMENT_TYPE
    ''' <summary>
    ''' TEST TYPE ( Accuracy, EMC, Environmental etc...) Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public TEST_TYPE As cTEST_TYPE
    ''' <summary>
    ''' Tested by 
    ''' </summary>
    ''' <remarks></remarks>
    Public TESTED_BY As cTESTED_BY
    ''' <summary>
    ''' USERS INFORMATION Table in the METER_SPECS database
    ''' </summary>
    ''' <remarks></remarks>
    Public USERS As cUSERS
    ''' <summary>
    ''' Call this function to initial the Class Definations for Mapping the Failure Report Data tables tothe Failure Report Database tool...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub init()
        AMR = New cAMR
        AMR_REV = New cAMR_REV
        APPROVER_DISCIPLINE = New cAPPROVER_DISCIPLINE
        APPROVERS = New cAPPROVERS
        BASE = New cBASE
        FORM = New cFORM
        METER_FIRMWARE = New cMETER_FIRMWARE
        PAC_LEVEL = New cPAC_LEVEL
        PROJECT = New cProject
        STANDARD_TEST = New cSTANDARD_TESTS
        TCC1 = New cTCC1
        TCC2 = New cTCC2
        TCC3 = New cTCC3
        TCC4 = New cTCC4
        TEST_EQUIPMENT = New cTEST_EQUIPMENT
        TEST_EQUIPMENT_TYPE = New cTEST_EQUIPMENT_TYPE
        TEST_TYPE = New cTEST_TYPE
        TESTED_BY = New cTESTED_BY
        USERS = New cUSERS
    End Sub

    ''' <summary>
    ''' AMR TABLE SCHEMA, these are the column names and SQL data types...
    ''' THE AMR table holds the predfined values that the user my select
    ''' from the comboox drop downs, however the USER is not constrained to
    ''' Use the values.  The FR database stores the value selected or typed
    ''' in rather than a pointer to this table.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cAMR
        ''' <summary>
        ''' Name of the Table in METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "AMR"
        ''' <summary>
        ''' THE AMR MODEL column (nVarchar40) i.e. GRIDSTREAM, RIVA, PLX etc...
        ''' </summary>
        ''' <remarks></remarks>
        Public MODEL As String = "AMR"
        ''' <summary>
        ''' Primary Key for the Table (INT)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' AMR manufacture NAME (LANDIS+GYR or ACLARA or SSN etc...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public MANUFACTURER As String = "AMR_MANUFACTURER"
        ''' <summary>
        ''' The Type Field Cellular, Ethernet, PLC, RF,  (May not be defined 
        ''' </summary>
        ''' <remarks></remarks>
        Public TYPE As String = "AMR_TYPE"
        ''' <summary>
        ''' Series IV, Series V, PLX, 3G nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public SUBTYPE As String = "AMR_SUBTYPE"
        ''' <summary>
        ''' MOdular or Integrated  nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public SUBTYPEII As String = "AMR_SUBTYPEII"
        ''' <summary>
        ''' Proprietary or SBS nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public SUBTYPEIII As String = "AMR_SUBTYPEIII"
        ''' <summary>
        ''' True is active record that can be selected from (bit)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
    End Class
    ''' <summary>
    ''' List of revisions used on Firmware,  this table is no longer used for populating
    ''' AMR revision Field. This is legacy table that is mapped here for completeness
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cAMR_REV
        ''' <summary>
        ''' AMR Revision table name
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "AMR Rev"
        ''' <summary>
        ''' Revison nVarChar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public VALUE As String = "AMR Rev"
        ''' <summary>
        ''' Primary Key
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
    End Class
    ''' <summary>
    ''' This table holds a list of the Disciplines repesented by the approvers on the Test Compliance Committee (TCC)
    ''' In addition there is an ADMIN type that allowes such a user to approve for any discipline.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cAPPROVER_DISCIPLINE
        ''' <summary>
        ''' Name of the Table in the SQL Database METER_SPEC
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "APPROVER_TYPE"
        ''' <summary>
        ''' Column holding the DISCIPLINEs represented by the TCC vcahr(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public DISCIPLINE As String = "DISCIPLINE"
        ''' <summary>
        ''' Primary KEU (INT)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
    End Class
    ''' <summary>
    ''' Approver Information table
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cAPPROVERS
        ''' <summary>
        ''' Table name in METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "APPROVERS"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' First and LAst name of the approver nvarchar(255)  This is redundent data can could be normalized.
        ''' </summary>
        ''' <remarks></remarks>
        Public APPROVER_NAME As String = "APPROVER_NAME"
        ''' <summary>
        ''' Foreign Key into the USER Table in METER_SPEC database (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public USER_ID As String = "USER_ID"
        ''' <summary>
        ''' This is the Landis+gyr Discipline represetned by the Approver (Engineering, Product Managment etc...) nvarchar(255)
        ''' This is redundent info and could be normalized
        ''' </summary>
        ''' <remarks></remarks>
        Public DISCIPLINE As String = "DISCIPLINE"
        ''' <summary>
        ''' Foreign Key into the APPROVER_TYPE table (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public DISCIPLINE_TYPE_ID As String = "APPROVER_TYPE_ID"
        ''' <summary>
        ''' True if the Approver account is active, False otherwise
        ''' IF a user changes roles (switches from Engineering to Productmanagment) a new entry should be made, and the orginal should be made
        ''' inactove (bit)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
        ''' <summary>
        ''' True if the approver is a delegate (bit)  Delegates are assigned by the actual TCC members.
        ''' </summary>
        ''' <remarks></remarks>
        Public VOTING_DELEGATE As String = "DELEGATE"
        ''' <summary>
        ''' True if This is the TCC member holds voting rights; false otherwise
        ''' </summary>
        ''' <remarks></remarks>
        Public VOTING_MEMEBER As String = "VOTING_MEMBER"
    End Class
    ''' <summary>
    ''' This is table that holds the information on the Base type of the meter under test, for popualting the Drop down...
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cBASE
        ''' <summary>
        ''' Table Name
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "BASE"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' METER BASE type (A, B, K, S, SC) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public BASE As String = "BASE"
        ''' <summary>
        ''' True id active (bit)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
    End Class
    ''' <summary>
    ''' This is ANSI or L+G FORM Number for Standard Engergy Meters
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cFORM
        ''' <summary>
        ''' This is the Table name in the METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "FORM"
        ''' <summary>
        ''' The Meter Form Number 1,2, 3, 4 9, 16 Etc... nvarchar(12)
        ''' </summary>
        ''' <remarks></remarks>
        Public FORM As String = "FORM"
        ''' <summary>
        ''' Primary key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' True if the FOMR is active (bit)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
        ''' <summary>
        ''' COMMA DELIMITED LIST OF L+G METER BASES SUPPORTED nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_BASE_TYPES_SUPPORTED As String = "METER_BASE"  '
    End Class
    ''' <summary>
    ''' TABLE of Firmware revisions...obsolete...no longer used
    ''' This table was orginally used to populate the FIRMWARE rev drop down...
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cMETER_FIRMWARE
        ''' <summary>
        ''' TAble name in the METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "FW Ver"
        ''' <summary>
        ''' Fimrware revision (Obsolete Function) nvarchar(255) 
        ''' </summary>
        ''' <remarks></remarks>
        Public VERSION As String = "FW Ver"
        ''' <summary>
        ''' Primary key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
    End Class
    ''' <summary>
    ''' This table Holds the allowed definations for PAC LEVEL
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cPAC_LEVEL
        ''' <summary>
        ''' THe name of the Table in the METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "LEVEL"
        ''' <summary>
        ''' THE Test Level (PAC 1, PAC 2, CIT
        ''' </summary>
        ''' <remarks></remarks>
        Public VALUE As String = "LEVEL"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' True if it is an active record (bit)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
    End Class
    ''' <summary>
    ''' This Table holds standard menu drop down values for meters
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cMETER
        ''' <summary>
        ''' Table name for the MEter datatable in METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "METER"
        ''' <summary>
        ''' This is the meter model for L+G meters (E350, E351, E650 etc...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public MODEL As String = "METER"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        ''' <summary>
        ''' True means the record is active and will appear in the drop down when queried
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
        ''' <summary>
        ''' This is the Manufacturer of the meter (Landis+GYR)
        ''' </summary>
        ''' <remarks></remarks>
        Public MANUFACTURER As String = "METER_MANUFACTURER"
        ''' <summary>
        ''' METER TYPE (FOCUS AX, FOCUS AX EPS, FOCUS AX Poly, FOCUS AXe, S4x etc...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TYPE As String = "METER_TYPE"
        ''' <summary>
        ''' AXR, RXR, AXR SD ect nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public SUBTYPE As String = "METER_SUBTYPE"
        ''' <summary>
        ''' Modular or Integrated
        ''' </summary>
        ''' <remarks></remarks>
        Public SUBTYPEII As String = "METER_SUBTYPEII"
    End Class
    ''' <summary>
    ''' Project table...this table was never used.  There are project Name and project number fields in the 
    ''' Failure Report database, however the Drop downs if they exist are populated from past records of active
    ''' Query
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cProject
        ''' <summary>
        ''' Table Name in METER_SPEC
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "PROJECT"
        ''' <summary>
        ''' Primary Key
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
        Public ACTIVE As String = "ACTIVE"
        Public NUMBER As String = "NUMBER"
        Public NAME As String = "NAME"
    End Class 'NEVER USED
    ''' <summary>
    ''' NO longer used legacy Table for populating the TCC1 drop down for the orginal FR Database tool
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTCC1

    End Class 'No longer used
    ''' <summary>
    ''' NO longer used legacy Table for populating the TCC2 drop down for the orginal FR Database tool
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTCC2 ' nolonger used
    End Class
    ''' <summary>
    ''' NO longer used legacy Table for populating the TCC3 drop down for the orginal FR Database tool
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTCC3 'nolonger used

    End Class
    ''' <summary>
    ''' NO longer used legacy Table for populating the TCC4 drop down for the orginal FR Database tool
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTCC4 ' nolonger used
    End Class
    ''' <summary>
    ''' This table holds the information for all the standard tests run in the Lab
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cSTANDARD_TESTS
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "TEST STANDARDS"
        ''' <summary>
        ''' Test Name nvarchar(MAX)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_NAME As String = "TEST"
        ''' <summary>
        ''' This is thetype of test (EMC, Accuracy, ect...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_TYPE As String = "TEST_TYPE"
        ''' <summary>
        ''' True if active...
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
        ''' <summary>
        ''' Comma delimited list of Standard Orgamix=zation associated with the test... nvarchar(255) 
        ''' </summary>
        ''' <remarks></remarks>
        Public TAGS As String = "TAGS'"
        ''' <summary>
        ''' Primary Key...
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"
    End Class
    ''' <summary>
    ''' Schema for the TEST_EQUIPMENT Schema in the METER_SPEC database
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTEST_EQUIPMENT
        ''' <summary>
        ''' Table name
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME = "TEST_EQUIPMENT"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "INDEX"
        ''' <summary>
        ''' (int) UNIQUE TO EACH PIECE OF TEST EQUIPMENT, HOW EVER MULTIPLE RECORDS ARE ALLOWED FOR A SINGLE PIECE OF EQUIPMENT, This ID is used to 
        ''' Identify equipment as it is Calibrated, Removed from Service etc... only one instance of the equipment is allowed to
        ''' be active at one time. Use case A failure report used a piece of thest equipment that was calibrated bJune 2017 due June 2018.
        ''' All failure reorts that were tested, during that period will point to this record.  When wquipment is calibrated, a new record
        ''' is created, with the same Eqipment ID with the new calibration dates.  The Old record is set to inactive so that it cannot be selected 
        ''' by accident for a failure outside its calibration period, however older failure rpeorts will still point to it as they should.
        ''' </summary>
        ''' <remarks></remarks>
        Public EQUIP_ID As String = "ID" '
        ''' <summary>
        ''' Equipment Manufacturer nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public MANUFACTURER As String = "MANUFACTURER'"
        ''' <summary>
        ''' Equipment Model nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public MODEL As String = "MODEL"
        ''' <summary>
        ''' Equipment Discription nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public DESCRIPTION As String = "DESCRIPTION"
        ''' <summary>
        ''' Equipment Serial Number nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public SERIAL_NUMBER As String = "SERIAL NUMBER"
        ''' <summary>
        ''' Alternate Serial Number nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public ALT_SERIAL_NUMBER As String = "ALT SERIAL NUMBER"
        ''' <summary>
        ''' Date of last Calibration nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public LAST_CAL As String = "LAST CAL"
        ''' <summary>
        ''' Date next Calibration is Due by nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public NEXT_CAL As String = "NEXT CAL"
        ''' <summary>
        ''' Lab Identifaction Identification Tag nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public LAB_ID As String = "LAB ID"
        ''' <summary>
        ''' Foreign Key Identify the last user who updated the record
        ''' </summary>
        ''' <remarks></remarks>
        Public USER_ID As String = "USER_ID" 'FOREIGN KEY (int)
        ''' <summary>
        ''' Location of the Equipment 'nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public LOCATION As String = "LOCATION"
        ''' <summary>
        '''  Note Field 'nvarchar(MAX)
        ''' </summary>
        ''' <remarks></remarks>
        Public NOTE As String = "NOTE"
        ''' <summary>
        ''' The Revision of the Record 'nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public REV As String = "REV"
        ''' <summary>
        ''' nvarcahr(255) A Piece of test equipment used to test a meter may be comprised of a group of test equipment.
        '''  This is a semicolon delimitied list that points to the primary key 
        ''' for each piece of equipment part of the test group 
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_GROUP_MEMBERS As String = "TEST GROUP MEMBERS"
        ''' <summary>
        ''' EMC, Safety, Environmental etc... nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TYPE As String = "TYPE"
        ''' <summary>
        ''' True if the record id a TEST Group (BIT)
        ''' </summary>
        ''' <remarks></remarks>
        Public IS_TEST_GROUP As String = "TEST GROUP"
        ''' <summary>
        ''' TRue if the record id the ACTIVE revision (BIT)
        ''' </summary>
        ''' <remarks></remarks>
        Public IS_ACTIVE_REV As String = "ACTIVE REV"
        ''' <summary>
        ''' Set to true when the equipment is permanatly removed from service (BIT)
        ''' </summary>
        ''' <remarks></remarks>
        Public IS_OBSOLETE As String = "OBSOLETE"
        ''' <summary>
        ''' Set to true if the equipment requires calibration (BIT)
        ''' </summary>
        ''' <remarks></remarks>
        Public IS_CAL_REQ As String = "CAL REQ"
    End Class
    ''' <summary>
    ''' This is the Type of testing the equipment is generally desinged for...EMC, Accuracy, Saftey, ... 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTEST_EQUIPMENT_TYPE
        ''' <summary>
        ''' Name of the table in METER+SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "TEST_EQUIPMENT_TYPE"
        ''' <summary>
        ''' Primary Key
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID" 'PRIMARY KEY
        ''' <summary>
        ''' The type of Testing the test equipment is used for, used to populate a drop down or list box
        ''' </summary>
        ''' <remarks></remarks>
        Public VALUE As String = "TEST_TYPE"
        ''' <summary>
        ''' True if the Record is Active (Selectable)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
    End Class
    Public Class cTEST_TYPE
        Public TABLE_NAME As String = "TEST_TYPE"
        Public ID As String = "ID"
        Public VALUE As String = "TEST_TYPE"
        Public TEST_TYPE_NUMBER As String = "TEST_TYPE_NUMBER"
        Public ACTIVE As String = "ACTIVE"
    End Class
    ''' <summary>
    ''' Used to populate the Tested by drop down box
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cTESTED_BY
        ''' <summary>
        ''' Tablname in METER_SPEC database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "TESTED BY"
        ''' <summary>
        ''' Primary Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID"  'PKEY
        ''' <summary>
        ''' List of Testers...nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TESTED_BY As String = "TESTED BY"
        ''' <summary>
        ''' True if an ACTIVEE Test engineer, Tech, or Coop (BIT)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
    End Class
    ''' <summary>
    ''' User Information Schema
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cUSERS
        ''' <summary>
        ''' USER Information table name
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "USERS"
        ''' <summary>
        ''' Priamry Key (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "ID" 'PKEY
        ''' <summary>
        ''' This is the USER name (Desinged to be AM.BM.net login name nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public USERNAME As String = "USERNAME"
        ''' <summary>
        ''' Password, will check against the AM.BM.NET password then will check against internal password...nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public PASSWORD As String = "PASSWORD"
        ''' <summary>
        ''' Users First Name nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public FIRST_NAME As String = "FIRSTNAME"
        ''' <summary>
        ''' Users lastname nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public LAST_NAME As String = "LASTNAME"
        ''' <summary>
        ''' Users Access (Edit) level (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ACCESS_LEVEL As String = "ACCESSLEVEL"
        ''' <summary>
        ''' TRUE = USER is ACtive
        ''' </summary>
        ''' <remarks></remarks>
        Public ACTIVE As String = "ACTIVE"
        ''' <summary>
        ''' (BIT) Set true to indicate that the internal Passwor is reset.  GUI will
        ''' prompt the user to change the password.  This is not conencted
        ''' to the AM.BM.NET domain password.
        ''' </summary>
        ''' <remarks></remarks>
        Public PASSWORD_IS_RESET As String = "PASSWORDISRESET"
        ''' <summary>
        ''' First part of email address
        ''' </summary>
        ''' <remarks></remarks>
        Public email As String = "email"

    End Class
End Class

Public Class cFailureReportDBDef
    Public FR_DBDef As cFAILURE_REPORT

    Public Sub init()
        FR_DBDef = New cFAILURE_REPORT
    End Sub

    ''' <summary>
    ''' This is the table that stores the content of each Failure or Anomoly Report Generated by the 
    ''' Tool
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cFAILURE_REPORT
        ''' <summary>
        ''' The Table Name of the FAILURE_REPORT Database
        ''' </summary>
        ''' <remarks></remarks>
        Public TABLE_NAME As String = "FAILURE REPORT"
        ''' <summary>
        ''' Primary Key of the database (int)
        ''' </summary>
        ''' <remarks></remarks>
        Public ID As String = "INDEX" 'PKEY
        ''' <summary>
        ''' This is the Failure Report Number (int) Each FR number is unique 
        ''' </summary>
        ''' <remarks></remarks>
        Public FR_NUMBER As String = "NEW ID" 'MUST BE UNIQUE
        ''' <summary>
        ''' This is the Orginal Failure Report Number (No longer Used) int
        ''' This column was obsoleted by the orginal Auther Branden Lopez in 
        ''' 2011
        ''' </summary>
        ''' <remarks></remarks>
        Public ORGINAL_FR_NUMBER As String = "ORIGINAL ID" 'NO LONGER USED
        ''' <summary>
        ''' This is the test level for the test nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_LEVEL As String = "LEVEL"
        ''' <summary>
        ''' This the Type of EUT submitted for testng (AMI, METER ONLY, COMMS, Etc...nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public EUT_TYPE As String = "EUT_TYPE"
        'METER ATTRIBUTES
        ''' <summary>
        ''' Meter model nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_MODEL As String = "METER"
        ''' <summary>
        ''' Meter manufacturere nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_MANUFACTURER As String = "METER_MANUFACTURER"
        ''' <summary>
        ''' Meter Serial Number nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_SERIAL_NUMBER As String = "METER_SERIAL_NUMBER"
        ''' <summary>
        ''' Meter Type (S4x, AXE, etc... nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_TYPE As String = "METER_TYPE"
        ''' <summary>
        ''' Meter Subtype (AX. AXe RXR, RXRe, RXRe-SD, or user defined Etc.. nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_SUBTYPE As String = "METER_SUBTYPE"
        ''' <summary>
        ''' Meter Ubtype II  Modular or Integrated or userdefined.
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_SUBTYPEII As String = "METER_SUBTYPEII"
        ''' <summary>
        ''' METER DSP Revision  nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_DSP_REV As String = "METER_DSP_REV"
        ''' <summary>
        ''' METER PCBA Partnumber
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_PCBA As String = "METER_PCBA"
        ''' <summary>
        ''' Meter PCBA partnumber revision nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_PCBA_REV As String = "METER_PCBA_REV"
        ''' <summary>
        ''' METER softwware used for testing nvarchar(255) 
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_SOFTWARE As String = "METER_SOFTWARE"
        ''' <summary>
        ''' Meter Software Revision nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_SOFTWARE_REV As String = "METER_SOFTWARE_REV"
        ''' <summary>
        ''' Notes Field nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_NOTES As String = "METER_NOTES"
        ''' <summary>
        ''' Meter Voltage (Range)  nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_VOLTAGE As String = "METER_VOLTAGE"
        ''' <summary>
        ''' Meter Base A, S, K etc...
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_BASE As String = "METER_BASE"
        ''' <summary>
        ''' Landisgyr Project name nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public PROJECT As String = "PROJECT"
        ''' <summary>
        ''' Landis+Gyr Project Number nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public PROJECT_NUMBER As String = "PROJECT_NUMBER"
        ''' <summary>
        ''' Meter Firmware Revision nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_FW_VERSION As String = "FW VER"
        ''' <summary>
        ''' Meter Form (number) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public METER_FORM As String = "FORM"
        'AMR ATTRIBUTES
        ''' <summary>
        ''' AMR Model nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_MODEL As String = "AMR"
        ''' <summary>
        ''' AMR Firmware revision nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_FW_REV As String = "AMR_REV"
        ''' <summary>
        ''' AMR manufacturer nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_MANUFACTURER As String = "AMR_MANUFACTURER"
        ''' <summary>
        ''' AMR Serial Number if Applicable nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SERIAL_NUMBER As String = "AMR_SN"
        ''' <summary>
        ''' AMR Type (RF, Ethernet etc...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_TYPE As String = "AMR_TYPE"
        ''' <summary>
        ''' AMR SubType (Series IV, Series V, etc) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SUBTYPE As String = "AMR_SUBTYPE"
        ''' <summary>
        ''' AMR subtype II Modualr vs Integrated or Custom nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SUBTYPEII As String = "AMR_SUBTYPEII"
        ''' <summary>
        ''' Proprietary or SBS or Custom nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SUBTYPEIII As String = "AMR_SUBTYPEIII"
        ''' <summary>
        ''' AMR notes nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_NOTES As String = "AMR_NOTES"
        ''' <summary>
        ''' AMR PCBA PART Number nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_PCBA As String = "AMR_PCBA"
        ''' <summary>
        ''' AMR PCBA Revision nvarchar(255) 
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_PCBA_REV As String = "AMR_PCBA_REV"
        ''' <summary>
        ''' AMR Test Software nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SOFTWARE As String = "AMR_SOFTWARE"
        ''' <summary>
        ''' AMR TEST Software Revision nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_SOFTWARE_REV As String = "AMR_SOFTWARE_REV"
        ''' <summary>
        ''' IP or LAN ID nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_IP_LAN_ID As String = "AMR_IP_LAN_ID"
        ''' <summary>
        ''' AMR Voltage or Votlage Range nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public AMR_VOLTAGE As String = "AMR_VOLTAGE"
        ''' <summary>
        ''' TEST NAME nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_NAME As String = "TEST"
        ''' <summary>
        ''' TEST Type (EMC, Accuracy ...) nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_TYPE As String = "TEST_TYPE"
        ''' <summary>
        ''' DATE FAILED (datetime)
        ''' </summary>
        ''' <remarks></remarks>
        Public DATE_FAILED As String = "DATE FAILED"
        ''' <summary>
        ''' Test Engineer ot Tech or Coop nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_ENGINEER As String = "TESTED BY"
        ''' <summary>
        ''' Project Lead nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public PROJECT_LEAD As String = "ASSIGNED TO"
        ''' <summary>
        ''' Date the FAilure Report was closed (Legacy field) (datetime)
        ''' This field usually ends being the day the Project lead marks 
        ''' ready for review, or  the date the the failure report is 
        ''' approved
        ''' </summary>
        ''' <remarks></remarks>
        Public DATE_CLOSED As String = "DATE CLOSED"
        ''' <summary>
        ''' This is the First and Last Name of whom ever corrected the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public CORRECTED_BY As String = "CORRECTED BY"
        ''' <summary>
        ''' This is the date that the User marks the Report, ready for review...(datetime)
        ''' </summary>
        ''' <remarks></remarks>
        Public DATE_CORRECTED As String = "DATE CORRECTED"
        ''' <summary>
        ''' This field points to the ling where attachments are stored on the Enj_Proj Drive -nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public ATTACHMENTS As String = "ATTACHMENTS"
        ''' <summary>
        ''' This is the Failure Description -nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public FAILURE_DESCRIPTION As String = "FAILURE_DESCRIPTION"
        ''' <summary>
        ''' This the Coorective Action -nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public CORRECTIVE_ACTION As String = "CORRECTIVE ACTION"
        ''' <summary>
        ''' Engineering notes -nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public ENGINEERING_NOTES As String = "ENGINEERING NOTES"
        ''' <summary>
        ''' TCC comments -nvarchar(max)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_COMMENTS As String = "TCC COMMENTS"
        ''' <summary>
        ''' DATE the Failrue Report is approved oer the TCC procedure  (datetime)
        ''' </summary>
        ''' <remarks></remarks>
        Public DATE_APPROVED As String = "DATE APPROVED"
        ''' <summary>
        ''' TCC 1 nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_1 As String = "TCC 1"
        ''' <summary>
        ''' TCC 2 nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_2 As String = "TCC 2"
        ''' <summary>
        ''' TCC 3 nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_3 As String = "TCC 3"
        ''' <summary>
        ''' TCC 4 nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_4 As String = "TCC 4"
        ''' <summary>
        ''' TCC 5 nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_5 As String = "TCC 5"
        ''' <summary>
        ''' True = Anomly report, false = Failure Report nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public ANOMALY As String = "ANOMALY"
        ''' <summary>
        ''' Approved by field, manage if not TCC reviewable, else it is the TCC nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public APPROVED_BY As String = "APPROVED BY"
        ''' <summary>
        ''' True if TCC review us required nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public TCC_REVIEW_REQUIRED As String = "TCC_REVIEW_REQUIRED"
        ''' <summary>
        ''' True is the FR is ready to be reviewed nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public FR_READY_FOR_REVIEW As String = "FR_READY_FOR_REVIEW"
        ''' <summary>
        ''' TRUE if the FR corrective action has been approved nvarchar(255)
        ''' </summary>
        ''' <remarks></remarks>
        Public FR_APPROVED As String = "FR_APPROVED"
        ''' <summary>
        ''' This is a Semicolon Delimited list of all 
        ''' </summary>
        ''' <remarks></remarks>
        Public TEST_EQUIPMENT_ID As String = "TEST_EQUIPMENT_ID"

    End Class
End Class

