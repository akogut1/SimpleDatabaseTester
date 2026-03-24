Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
Imports System.Environment
Imports System.DirectoryServices
Imports System.DirectoryServices.ActiveDirectory
Public Class frmLogin

    Private _CloseThisForm As Boolean = True
    Private Sub Cancel_Click(sender As System.Object, e As System.EventArgs) Handles btnExitCancel.Click
        If frmFailureBrowser.gbUserLoggedOn Then
            Me.Hide()
        Else 'Stop program execution

            Dim Result As Integer = MsgBox("Are You sure you want Exit?", MsgBoxStyle.YesNo)
            If Result = MsgBoxResult.Yes Then
                'For some reason the forms close event keep getting called and I have to catch it in the form closing event and use flags! - FJB
                _CloseThisForm = True
            Else
                'This flag will catch the form closing event and prevent the log in window from closing
                _CloseThisForm = False
            End If

        End If
    End Sub

    Private Sub tsmExitClick(sender As System.Object, e As System.EventArgs) Handles tsmExit.Click
        If frmFailureBrowser.gbUserLoggedOn Then
            Me.Hide()
        Else 'Stop program execution

            Dim Result As Integer = MsgBox("Are You sure you want Exit?", MsgBoxStyle.YesNo)
            If Result = MsgBoxResult.Yes Then
                Me.Close()
            End If
        End If

    End Sub
    Private Function GetCurrentDomainPath() As String
        Dim de As DirectoryEntry = New DirectoryEntry("LDAP://RootDSE")
        Return "LDAP://" & de.Properties("defaultNamingContext")(0).ToString()
    End Function

    Private Sub GetAdditionalUserInfo(UserName As String, Password As String)
        Dim results As SearchResultCollection
        Dim ds As DirectorySearcher = Nothing
        Dim CurrentDirectoryPath = GetCurrentDomainPath()
        Dim de As DirectoryEntry = New DirectoryEntry(CurrentDirectoryPath, UserName, Password)
        ds = New DirectorySearcher(de)
        ds.PropertiesToLoad.Add("name")
        ds.PropertiesToLoad.Add("mail")
        ds.PropertiesToLoad.Add("givenname")
        ds.PropertiesToLoad.Add("sn")
        ds.PropertiesToLoad.Add("userPrincipalName")
        ds.PropertiesToLoad.Add("distinguishedName")
        ds.Filter = "(samAccountName=" & UserName & ")" '"(&(objectCategory=User)(objectClass=person)(samAccountName=" & UserName & ")"
        results = ds.FindAll()
        Dim UserFound As System.DirectoryServices.SearchResult = ds.FindOne

        For Each sr As SearchResult In results
            If sr.Properties("name").Count > 0 Then
                '     Debug.WriteLine(sr.Properties("name")(0).ToString())
            End If
            Dim TempName = UserFound.Properties("name").Item(0)
            If sr.Properties("mail").Count > 0 Then
                Debug.WriteLine(sr.Properties("mail")(0).ToString())
                If sr.Properties("mail")(0).ToString().ToLower = "frank.boudreau@landisgyr.com" Then
                    Exit For
                End If
            End If

            ' If sr.Properties("givenname").Count > 0 Then Debug.WriteLine(sr.Properties("givenname")(0).ToString())
            ' If sr.Properties("sn").Count > 0 Then Debug.WriteLine(sr.Properties("sn")(0).ToString())
            ' If sr.Properties("userPrincipalName").Count > 0 Then Debug.WriteLine(sr.Properties("userPrincipalName")(0).ToString())
            ' If sr.Properties("distinguishedName").Count > 0 Then Debug.WriteLine(sr.Properties("distinguishedName")(0).ToString())
        Next
    End Sub

    Private Overloads Function ValidateUserOnActiveDomainServer(ByVal Domain As String, ByVal UserName As String, ByVal Password As String) As Boolean
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, UserName, Password)
        Dim MySearcher As DirectorySearcher
        MySearcher = New DirectorySearcher(Entry)
        Dim domainName As String = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName

        'MySearcher.SearchScope = DirectoryServices.SearchScope.Subtree
        MySearcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        '*****************
        MySearcher.PropertiesToLoad.Add("samAccountName")
        MySearcher.PropertiesToLoad.Add("DisplayName")
        MySearcher.PropertiesToLoad.Add("Description")
        MySearcher.PropertiesToLoad.Add("mail")
        MySearcher.PropertiesToLoad.Add("ProfilePath")
        MySearcher.PropertiesToLoad.Add("HomeDirectory")
        MySearcher.PropertiesToLoad.Add("HomeDrive")
        MySearcher.PropertiesToLoad.Add("GivenName")
        MySearcher.PropertiesToLoad.Add("sn")
        Try
            '  MySearcher.Filter = "(samAccountName=" & UserName & ")"
            Dim results As SearchResultCollection = MySearcher.FindAll()
            '*********************

            Dim UserFound As System.DirectoryServices.SearchResult = MySearcher.FindOne()
            If MySearcher.FindOne Is Nothing Then
                Success = False
            Else
                ' Dim UserFound As System.DirectoryServices.SearchResult = MySearcher.FindOne()
                Success = Not (UserFound Is Nothing)
                Dim MyPropertyList As List(Of String) = New List(Of String)
                MyPropertyList.Add("NAME: " + (UserFound.GetDirectoryEntry().Properties.Item("cn").Value))
                MyPropertyList.Add("MAIL: " + (UserFound.GetDirectoryEntry().Properties.Item("mail").Value))
                MyPropertyList.Add("END OF LIST")

                'Dim RootDSE As New DirectoryServices.DirectoryEntry("LDAP://RootDSE")
                'Dim DomainDN As String = RootDSE.Properties("DefaultNamingContext").Value
                'Dim ADEntry As New DirectoryServices.DirectoryEntry("LDAP://" & DomainDN)
                'Dim ADSearch As New System.DirectoryServices.DirectorySearcher(ADEntry)

                '' Dim ADSearchResult As System.DirectoryServices.SearchResult
                'ADSearch.Filter = ("(samAccountName=" & UserName & ")")
                'ADSearch.SearchScope = SearchScope.Subtree
                'If Not IsNothing(UserFound) Then
                '    MyPropertyList.Add("NAME: " + (UserFound.GetDirectoryEntry().Properties.Item("name").Value))
                '    MyPropertyList.Add("MAIL: " + (UserFound.GetDirectoryEntry().Properties.Item("mail").Value))
                '    MyPropertyList.Add("END OF LIST")
                'End If

                Dim de As System.DirectoryServices.DirectoryEntry = UserFound.GetDirectoryEntry()
                MyPropertyList.Add("COUNT: " + de.Properties.Count.ToString)
                For i = 0 To de.Properties.Count - 1
                    MyPropertyList.Add("Property " + i.ToString + ":" + de.Properties.Item("mail").ToString)
                Next
                Dim j As Integer = 1
                For Each strProperty In de.Properties.PropertyNames
                    MyPropertyList.Add("Property Name " + j.ToString + ":" + de.Properties.Item(strProperty).Value.ToString)
                    j += 1
                Next
                GetAdditionalUserInfo(UserName, Password)
                'MyPropertyList.Add("NAME: " + (UserFound.GetDirectoryEntry().Properties.Item("name").Value))
                'MyPropertyList.Add("MAIL: " + (UserFound.GetDirectoryEntry().Properties.Item("mail").Value))
                MyPropertyList.Add("END OF LIST")




                '  Throw New ApplicationException("Exception Occured") ' for developemt - fjb
            End If

        Catch ex As Exception
            Success = False
        End Try



        Return Success
    End Function

    Private Overloads Function LogOnToFR_Database(Optional ByVal UserValidatedAgainstDomain = False) As Boolean
        Dim MyConnection As SqlConnection
        Dim myTable As New DataTable
        Dim myTableAdaptor As SqlDataAdapter
        Dim DbUserID As Integer
        Dim DbLoginUserName As String = txtUserName.Text.Trim.Replace("%", "")  'trim and remove wild cards if present
        Dim DbPassword As String = "" 'txtPassword.Text.Trim
        Dim DbFirstName As String = ""
        Dim DbLastName As String = ""
        Dim DBAccessLevel As frmFailureBrowser.eAccessState = frmFailureBrowser.eAccessState.NO_ACCESS 'Default to no access Rights
        Dim DBApproverType As frmFailureBrowser.eApproverDiscipline = frmFailureBrowser.eApproverDiscipline.NONE 'Default to not an approver
        Dim DBPassWordIsResetFlag As Boolean = False
        Dim DbUserIsActive As Boolean = False
        Try
            MyConnection = New SqlConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
            MyConnection.Open()

            Dim MyCommandString As String = "SELECT * FROM [Users] WHERE username = '" + DbLoginUserName + "'"
            myTableAdaptor = New SqlDataAdapter(MyCommandString, MyConnection)
            myTableAdaptor.Fill(myTable)
            ' myTableAdaptor.Update(myTable)

            'Should Return a unique Row...
            If myTable.Rows.Count = 1 Then


                Try
                    DbPassword = myTable.Rows(0)("password").ToString.Trim
                Catch ex As Exception
                    'MsgBox("Check your username and password and try again")
                    Throw New Exception("Check your username and password and try again")
                    'Return False
                End Try

                Try
                    DbLoginUserName = myTable.Rows(0)("username").ToString.Trim
                Catch ex As Exception
                    ' MsgBox("Check your username and password and try again")
                    Throw New Exception("Check your username and password and try again")
                    'Return False
                End Try

                Try
                    DbUserID = myTable.Rows(0)("ID").ToString.Trim
                Catch ex As Exception
                    Throw New Exception("Error Retrieving User ID")
                End Try

                Try
                    DbFirstName = myTable.Rows(0)("FirstName").ToString.Trim
                Catch ex As Exception
                    Throw New Exception("Error Retrieving User Firstname")
                End Try

                Try
                    DbLastName = myTable.Rows(0)("LastName").ToString.Trim
                Catch ex As Exception
                    Throw New Exception("Error Retrieving User Lastname")
                    ' Return False
                End Try

                Try
                    DbUserIsActive = myTable.Rows(0)("Active")
                Catch ex As Exception
                    Throw New Exception("Error Retrieving User Active Status")
                End Try

                Try
                    DBPassWordIsResetFlag = myTable.Rows(0)("PassWordIsReset")

                Catch ex As Exception
                    Throw New Exception("Error Retrieving Password Reset Status")
                    'Return False
                End Try

                Try
                    DBAccessLevel = CInt(Val(myTable.Rows(0)("AccessLevel").ToString.Trim))
                Catch ex As Exception
                    DBAccessLevel = frmFailureBrowser.eAccessState.READ_ONLY 'default to admin for now
                    Throw New Exception("Error Retriving Accesslevel")
                    'Return False
                End Try



            ElseIf myTable.Rows.Count > 1 Then
                'Some kind of wild card slipped through?
                Throw New Exception("Check your username and password and try again or Select 'Browse Database' to view Failure Reports." + vbCrLf + "Select 'Setup Database' to Connect to the Failure Report Database")
                ' Return False
            Else
                Throw New Exception("Check your username and password and try again or Select 'Browse Database' to view Failure Reports." + vbCrLf + "Select 'Setup Database' to Connect to the Failure Report Database")
                ' Return False
            End If

            'Make sure that the password matches or that the user is validated against the domain....
            If (txtPassword.Text.Trim = DbPassword) Or UserValidatedAgainstDomain = True Then
                'Check to see if the User is active...
                If DbUserIsActive Then


                    If DBPassWordIsResetFlag Then
                        Me.Hide()
                        'Prompt to reset the password
                        Dim MyChangePasswordForm As New frmChangePassWord
                        'initialize the Dialog Result
                        MyChangePasswordForm.DialogResult = Windows.Forms.DialogResult.Cancel
                        MyChangePasswordForm.StartPosition = FormStartPosition.CenterParent
                        MyChangePasswordForm.ShowDialog()
                        If MyChangePasswordForm.DialogResult <> Windows.Forms.DialogResult.OK Then

                            Throw New Exception("Password Has not been changed, please try again.")
                            'Me.Show()
                            ' Return False
                        Else
                            'its not really an exception but by raising an exceptoion here the rest of the function is bypassed and
                            'the User will be prompted to lgin on again.
                            Throw New Exception("Password Sucessfully Changed!. Please enter your new password to log in.")

                            ' Me.Show()
                            ' Return False
                        End If
                    End If


                    'Set approver type
                    'PAC 2 Approvers must be identified in addtion to to being a Generic approver
                    'Generic Approvers are managers that can approve Failure Reports that do not require TCC review
                    'These are USERS with Access level 5
                    'If a user is acces level 5 they might also be a TCC approver
                    'Determine if the User is a TCC Approver...

                    Try
                        If DBAccessLevel = frmFailureBrowser.eAccessState.APPROVER Then


                            MyConnection = New SqlConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
                            MyConnection.Open()

                            MyCommandString = "SELECT * FROM [Approvers] WHERE USER_ID =  " + DbUserID.ToString
                            myTableAdaptor = New SqlDataAdapter(MyCommandString, MyConnection)
                            myTable.Clear()
                            myTableAdaptor.Fill(myTable)
                            'If a PAC 2 approver, should Return a unique Row...
                            If myTable.Rows.Count = 1 Then
                                Try
                                    DBApproverType = CInt(Val(myTable.Rows(0)("APPROVER_TYPE_ID").ToString.Trim))
                                Catch ex As Exception
                                    DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE
                                    Throw New Exception("Error Retrieving Approver type from Database")
                                End Try
                            ElseIf myTable.Rows.Count > 1 Then
                                'This should rarely happen
                                For i = 0 To myTable.Rows.Count - 1
                                    'If (myTable.Rows(0)("ACTIVE")) = True Then
                                    'Dim stMyTestString = Val(myTable.Rows(i)("ACTIVE").ToString.Trim)
                                    ' Dim intMyint = myTable.Rows(i)("ACTIVE")
                                    If ((myTable.Rows(i)("ACTIVE").ToString.Trim)) = "True" Then
                                        Try
                                            DBApproverType = CInt(Val(myTable.Rows(i)("APPROVER_TYPE_ID").ToString.Trim))
                                            Exit For
                                        Catch ex As Exception
                                            DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE
                                            Throw New Exception("Error Retrieving Approver type from Database")
                                        End Try
                                    Else
                                        DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE

                                    End If
                                Next
                                'If DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE Then
                                'Throw New Exception("Multiple Values returned when retrieving Approver Type, contact your database administrator")
                                'End If

                            Else 'No rows returned...which is valid
                                If DBAccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
                                    DBApproverType = frmFailureBrowser.eApproverDiscipline.Admin
                                Else
                                    DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE
                                End If
                            End If

                        Else 'admin users can edit all values...
                            If DBAccessLevel = frmFailureBrowser.eAccessState.ADMIN Then
                                DBApproverType = frmFailureBrowser.eApproverDiscipline.Admin
                            Else
                                DBApproverType = frmFailureBrowser.eApproverDiscipline.NONE
                            End If
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try



                    frmFailureBrowser._CurrentUser.Login = DbLoginUserName
                    frmFailureBrowser._CurrentUser.Password = DbPassword
                    frmFailureBrowser._CurrentUser.FirstName = DbFirstName
                    frmFailureBrowser._CurrentUser.LastName = DbLastName
                    frmFailureBrowser._CurrentUser.AccessLevel = DBAccessLevel
                    frmFailureBrowser._CurrentUser.ApproverDiscipline = DBApproverType
                    ' MsgBox("Success Logging On")
                    My.Settings.MyUserName = DbLoginUserName
                    My.Settings.Password = DbPassword
                    Me.CancelToolStripMenuItem.Enabled = True
                    Me.Hide()
                    'clear password
                    txtPassword.Text = ""



                Else
                    Throw New Exception("User Account is not active. Please check you Username and Password")
                    ' Return False
                End If

            Else
                Throw New Exception("Check your username and password and try again")
                'Return False
            End If

            Dim MyStop As Integer = 1
        Catch ex As Exception
            'Pass Error
            If Me.Visible = False Then
                Me.Show()
            End If
            MsgBox(ex.ToString)
            Return False
        End Try
        Return True
    End Function


    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If LogOnToFR_Database(ValidateUserOnActiveDomainServer("am.bm.net", txtUserName.Text, txtPassword.Text)) Then
            'User exists in Domain, now verify that the user has access rights to the Database...
        Else
            'Error Message
        End If
    End Sub

    Private Sub frmLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _CloseThisForm = False Then
            e.Cancel = True
        End If
        'frmFailureBrowser.gbFailure_ReportDataGridView_SafeToProcessEvents = True
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPassword.Text = My.Settings.Password
        txtPassword.SelectAll()
        txtUserName.Text = My.Settings.MyUserName
        ' frmFailureBrowser.gbFailure_ReportDataGridView_SafeToProcessEvents = False
        If frmFailureBrowser.gbUserLoggedOn = True Then 'A successful Login has allready occured, therefore show the cancel button
            btnExitCancel.Text = "Cancel"
        Else
            btnExitCancel.Text = "Exit"
        End If
        Me.Focus()
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        Me.Hide()
    End Sub


    Private Sub btnBrowseFailureReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseFailureReports.Click
        frmFailureBrowser._CurrentUser.Login = "Default"
        frmFailureBrowser._CurrentUser.Password = ""
        frmFailureBrowser._CurrentUser.FirstName = "Browser"
        frmFailureBrowser._CurrentUser.AccessLevel = frmFailureBrowser.eAccessState.READ_ONLY
        'MsgBox("")
        'My.Settings.MyUserName = DbLoginUserName
        Me.CancelToolStripMenuItem.Enabled = True
        Me.Hide()
        'clear password
        txtPassword.Text = ""
    End Sub

    Private Sub frmLogin_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.BringToFront()
    End Sub


    Private Sub txtPassword_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        Dim tmp As System.Windows.Forms.KeyPressEventArgs = e
        If tmp.KeyChar = ChrW(Keys.Enter) Then
            If LogOnToFR_Database(ValidateUserOnActiveDomainServer("am.bm.net", txtUserName.Text, txtPassword.Text)) = False Then
                'error message
            End If

        End If
    End Sub

    Private Sub SelectDatabase_Click(sender As System.Object, e As System.EventArgs) Handles SelectDatabase.Click
        If frmSelectDatabase.ShowDialog() = Windows.Forms.DialogResult.OK Then

            My.Settings.Prompt_To_Change_Database = False
        End If
    End Sub
End Class