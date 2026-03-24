
Imports System.Text.RegularExpressions
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class frmChangePassWord



    Enum PasswordScore As Integer
        BLANK = 0
        LESS_THAN_MINIMUM = 0
        INVALID_CHARACTER = 0
        MINIMUM_LENGTH_MET = 1
        EXCEEDS_MINIMUM_LENGTH = 2
        HAS_LOWER_CASE = 4
        HAS_NUMBER = 8
        HAS_UPPER_CASE = 16
        HAS_NON_ALPHA_NUMERIC = 32
        MAX_CONSEQ_CHARS_NOT_EXCEEDED = 64
    End Enum

    Public Function ComputeScore(PassWordVal As Integer) As Integer
        Dim Score As Integer = 0
        For i = 1 To 32
            Score = Score + (PassWordVal And i) / i
            i = i * 2 - 1
        Next
        Return (Score)
    End Function

    Public Shared Function hasConsecutiveCharacters(pwd As String, Optional iNumberOfConsecutiveChars As Integer = 3) As Boolean
        Dim letter As String() '= pwd.AsEnumerable().ToArray() 'pwd.Split("")

        'Put password into an array...
        ReDim letter(Len(pwd) - 1)

        For i = 1 To Len(pwd)
            letter(i - 1) = Mid$(pwd, i, 1)
        Next


        ' here you get each letter in to a string array
        For i As Integer = 0 To letter.Length - 3
            Dim bConsecutiveDetected As Boolean = False
            'If letter(i).Equals(letter(i + 1)) AndAlso letter(i + 1).Equals(letter(i + 2)) Then
            '    'return true as it has 3 consecutive same character
            '    Return True
            'End If
            For j = 0 To iNumberOfConsecutiveChars - 2
                If letter(i).Equals(letter(i + 1 + j)) Then
                    'set to true every time the condition is detected
                    'if true every time through inner loop then condition exists!
                    bConsecutiveDetected = True
                Else
                    'Reset any time it is not detected and exit the inner loop!
                    bConsecutiveDetected = False
                    Exit For
                End If
            Next
            If bConsecutiveDetected Then
                ' MsgBox(iNumberOfConsecutiveChars.ToString + " or more consecutive characters detected.")
                Return True
            End If


        Next
        Return False
        'If you reach here that means there are no 3 consecutive characters therefore return false.
    End Function


    ''' <summary>Determines if a password is sufficiently complex. seeded From MSDN</summary>
    ''' <param name="strPassword">Password to validate</param>
    ''' <param name="intPassWordMinimumLength">Minimum number of password characters.</param>
    ''' <param name="intUpperCharCount">Minimum number of uppercase characters.</param>
    ''' <param name="intLowerCharCount">Minimum number of lowercase characters.</param>
    ''' <param name="IntNumberCharCount">Minimum number of numeric characters.</param>
    ''' <param name="IntSpecialCharCount">Minimum number of special characters.</param>
    ''' <returns>True if the password is sufficiently complex.</returns>
    Function ValidatePassword(ByVal strPassword As String,
        Optional ByVal bSuppressMessage As Boolean = True,
        Optional ByVal intPassWordMinimumLength As Integer = 8,
        Optional ByVal intUpperCharCount As Integer = 0,
        Optional ByVal intLowerCharCount As Integer = 0,
        Optional ByVal IntNumberCharCount As Integer = 0,
        Optional ByVal IntSpecialCharCount As Integer = 0) As Integer

        Dim iScore As Integer = 0
        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim regxUpperCase As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim regxLowerCaseRegEdit As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim regxNumeric As New System.Text.RegularExpressions.Regex("[0-9]")

        ' Special is "none of the above".
        Dim regxSpecialChars As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

        ' Check the length.
        If strPassword.Length < 1 Then
            If Not bSuppressMessage Then
                MsgBox("Password cannot be Blank")
            End If
            Return PasswordScore.BLANK
        ElseIf strPassword.Length < intPassWordMinimumLength Then
            If Not bSuppressMessage Then
                MsgBox("Password Must have at least " + intPassWordMinimumLength.ToString + " Characters")
            End If
            iScore = PasswordScore.LESS_THAN_MINIMUM
        Else
            iScore = PasswordScore.MINIMUM_LENGTH_MET
        End If

        If strPassword.Length > intPassWordMinimumLength Then
            iScore = iScore + PasswordScore.EXCEEDS_MINIMUM_LENGTH
        End If

        ' Check for minimum number of occurrences.
        If regxUpperCase.Matches(strPassword).Count < intUpperCharCount Then
            If Not bSuppressMessage Then
                MsgBox("Password must have at least " + intUpperCharCount.ToString + " upper case letters.")
            End If

            Return PasswordScore.LESS_THAN_MINIMUM
        ElseIf regxUpperCase.Matches(strPassword).Count >= 1 Then
            iScore = iScore + PasswordScore.HAS_UPPER_CASE
        End If
        If regxLowerCaseRegEdit.Matches(strPassword).Count < intLowerCharCount Then
            If Not bSuppressMessage Then
                MsgBox("Password must have at least " + intLowerCharCount.ToString + " Lowercase letters.")
            End If

            Return PasswordScore.LESS_THAN_MINIMUM
        ElseIf regxLowerCaseRegEdit.Matches(strPassword).Count >= 1 Then
            iScore = iScore + PasswordScore.HAS_LOWER_CASE
        End If

        If regxNumeric.Matches(strPassword).Count < IntNumberCharCount Then
            If Not bSuppressMessage Then
                MsgBox("Password must have at least " + IntNumberCharCount.ToString + " Numbers.")
            End If

            Return PasswordScore.LESS_THAN_MINIMUM
        ElseIf regxNumeric.Matches(strPassword).Count >= 1 Then
            iScore = iScore + PasswordScore.HAS_NUMBER
        End If

        If regxSpecialChars.Matches(strPassword).Count < IntSpecialCharCount Then
            If Not bSuppressMessage Then
                MsgBox("Password must have at least " + IntSpecialCharCount.ToString + " Special Characters.")
            End If
            Return PasswordScore.LESS_THAN_MINIMUM
        ElseIf regxSpecialChars.Matches(strPassword).Count >= 1 Then
            iScore = iScore + PasswordScore.HAS_NON_ALPHA_NUMERIC
        End If

        'Check for repeating Characters...
        If Not (hasConsecutiveCharacters(strPassword, Val(txtConsecutiveCharsToCheckFor.Text.Trim))) Then
            If strPassword.Length >= Val(txtConsecutiveCharsToCheckFor.Text.Trim) Then

                iScore = iScore + PasswordScore.MAX_CONSEQ_CHARS_NOT_EXCEEDED
            End If
        End If

        ' Return Password Score.
        Return (ComputeScore(iScore))
    End Function

    Private Sub frmChangePassWord_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Default to Cancel on Load...
        '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnChangePasswordCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnChangePasswordCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub btnChangePasswordOK_Click(sender As System.Object, e As System.EventArgs) Handles btnChangePasswordOK.Click
        Dim MyConnection As SqlConnection
        Dim myTable As New DataTable
        Dim myTableAdaptor As SqlDataAdapter
        Dim DbLoginUserName As String = frmLogin.txtUserName.Text.Trim.Replace("%", "")  'trim and remove wild cards if present
        Dim DbPassword As String
        Dim DBAccessLevel As frmFailureBrowser.eAccessState = frmFailureBrowser.eAccessState.NO_ACCESS 'Default to no access Rights
        Dim DBPassWordIsResetFlag As Boolean = False
        'Dim MyCustomDBAccess As cCustomDataBaseAccess
        Dim DBId As String
        Dim MyRecord As New cCustomDataBaseAccess.cTable 'Record to hold database Changes
        Dim MyLocalDataTable As DataTable
        Dim iColumnCount As Integer 'Column count of the Failure Report Database
        Try
            MyConnection = New SqlConnection(My.Settings.MeterSpecDataBaseFullConnectionString)
            MyConnection.Open()
            Dim MyCommandString As String = "SELECT * FROM [Users] WHERE username = '" + DbLoginUserName + "'"
            myTableAdaptor = New SqlDataAdapter(MyCommandString, MyConnection)
            myTableAdaptor.Fill(myTable)

            If myTable.Rows.Count = 1 Then
                Try
                    DbPassword = myTable.Rows(0)("password").ToString.Trim
                Catch ex As Exception
                    MsgBox("Check your username and password and try again")
                    Throw ex
                End Try

                Try
                    DBId = myTable.Rows(0)("ID").ToString.Trim
                Catch ex As Exception
                    MsgBox("Check your username and password and try again")
                     Throw ex
                End Try

                If txtOldPassWord.Text.Trim = DbPassword Then

                    'Confirm that New passwords agree...
                    If txtNewPassWord.Text.Trim <> txtRepeatNewPassWord.Text.Trim Then
                        MsgBox("New password and Repeat New password must be the same")
                        Me.DialogResult = Windows.Forms.DialogResult.Abort
                        Exit Sub
                    End If

                    If ValidatePassword(txtNewPassWord.Text.Trim, False, 8, 1, 1, 1, 1) < 2 Then
                        MsgBox("Password must be at least 8 characters and include:" + vbCrLf + _
                            "1 UPPERCASE, 1 lowercase, 1 $pecial, and 1 Numeric character")
                        Me.DialogResult = Windows.Forms.DialogResult.Abort
                        Exit Sub
                    End If


                    'Change the Password
                    'Get the Schema Information for the Users Table
                    MyLocalDataTable = cCustomDataBaseAccess.GetColumnNames_DataType_And_Size(MyConnection, "Users")

                    'get the Column count of the table, each row is a column
                    iColumnCount = MyLocalDataTable.Rows.Count

                    'Set the Table Name
                    MyRecord.TableName = "Users"
                    'Initalize the columns 
                    MyRecord.Columns = New List(Of cCustomDataBaseAccess.cColumn)

                    'Find the password row to update
                    Dim RecordIndex As Integer = 0
                    For Each row In MyLocalDataTable.Rows
                        If MyLocalDataTable.Rows(RecordIndex).Item("ColumnName") = "password" Then
                            Exit For
                        End If
                        RecordIndex += 1
                    Next

                    'populate the Record
                    MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn("password", txtNewPassWord.Text.Trim, MyLocalDataTable.Rows(RecordIndex).Item("DataType"), True))

                    'Find the password row to update
                    RecordIndex = 0
                    For Each row In MyLocalDataTable.Rows
                        If MyLocalDataTable.Rows(RecordIndex).Item("ColumnName") = "PassWordIsReset" Then
                            Exit For
                        End If
                        RecordIndex += 1
                    Next
                    'populate the Record
                    MyRecord.Columns.Add(New cCustomDataBaseAccess.cColumn("PassWordIsReset", False, MyLocalDataTable.Rows(RecordIndex).Item("DataType"), True))

                    'Try to perform the update
                    Try

                        If cCustomDataBaseAccess.UpdateExistingRecord(MyRecord, MyConnection, "ID", DBId) = False Then
                            Me.DialogResult = Windows.Forms.DialogResult.Abort
                            Exit Sub
                        End If

                    Catch ex As Exception 'Catch any errors
                        MsgBox("Error Updating Password " + vbCrLf + ex.ToString)
                         Throw ex
                    End Try
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    txtNewPassWord.Text = ""
                    txtOldPassWord.Text = ""
                    txtRepeatNewPassWord.Text = ""
                Else
                    MsgBox("Check your old password and try again")
                End If




            ElseIf myTable.Rows.Count > 1 Then
                'Some kind of wild card slipped through?
                MsgBox("Check your username and password and try again")
                Me.DialogResult = Windows.Forms.DialogResult.Abort
                Exit Sub
            Else
                MsgBox("Check your username and password and try again")
                Me.DialogResult = Windows.Forms.DialogResult.Abort
                Exit Sub
            End If
        Catch ex As Exception
            'Throw ex
            MsgBox("Error Updating password." + vbCrLf + ex.ToString)
            Me.Tag = ex.ToString
            Me.DialogResult = Windows.Forms.DialogResult.Abort
        End Try


    End Sub

    Private Sub btnCheckPassword_Click(sender As System.Object, e As System.EventArgs) Handles btnCheckPassword.Click, txtNewPassWord.TextChanged
        Try
            ' txtNewPassWord.UseSystemPasswordChar = False
            'pbPasswordStrength.Value = ComputeScore(CheckStrength(txtNewPassWord.Text.Trim))
            Dim intPasswordScore As Integer = ValidatePassword(txtNewPassWord.Text.Trim, 8) ', 1, 1, 1, 1)

            If intPasswordScore > 0 Then
                'MsgBox("Validated")
                If intPasswordScore = 1 Then
                    pbPasswordStrength.ForeColor = Color.Red
                ElseIf intPasswordScore = 2 Then
                    pbPasswordStrength.ForeColor = Color.Red
                ElseIf intPasswordScore = 3 Or intPasswordScore = 4 Then
                    pbPasswordStrength.ForeColor = Color.Yellow
                ElseIf intPasswordScore = 5 Then
                    pbPasswordStrength.ForeColor = Color.Lime
                ElseIf intPasswordScore = 6 Then
                    pbPasswordStrength.ForeColor = Color.Lime
                ElseIf intPasswordScore > 6 Then
                    pbPasswordStrength.ForeColor = Color.Green
                End If
                pbPasswordStrength.Value = intPasswordScore
            ElseIf intPasswordScore = 0 And txtNewPassWord.Text.Trim.Length > 0 Then
                'MsgBox("Not Validated")
                pbPasswordStrength.Value = 1
                pbPasswordStrength.ForeColor = Color.Red
            Else
                pbPasswordStrength.Value = 0
                pbPasswordStrength.ForeColor = Color.Red
            End If


        Catch ex As Exception
            pbPasswordStrength.Value = 0
        End Try


    End Sub



    Private Sub CheckBox1_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckStateChanged
        txtNewPassWord.UseSystemPasswordChar = Not CheckBox1.Checked
        txtOldPassWord.UseSystemPasswordChar = Not CheckBox1.Checked
        txtRepeatNewPassWord.UseSystemPasswordChar = Not CheckBox1.Checked

    End Sub


End Class