
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class frmSelectTestGroupMembers


    Public gTestEquipmentUniqueIndex As String
    Dim _MyDatabaseAccess As cCustomDataBaseAccess
    Dim _MyTestEquipmentBindingSource As BindingSource
    Dim _MyTestEquipmentDataAdaptor As SqlDataAdapter 'OleDb.OleDbDataAdapter
    Dim _TestEquipmentBindingRecord As List(Of cCustomDataBaseAccess.cDataBindingTracker)
    'Dim WithEvents _MyTestEquipmentBinding As Binding
    'Dim _MyFilter As String = ""

    Dim _MyTestEquipmentTable As New DataTable
    Dim _MySelectedTestEquipmentTable As DataTable
    Dim _MySelectedTestEquipmentBindingSource As BindingSource

    Private Sub RefreshList()
        Dim MyFilter As String = "SELECT * From [TEST_EQUIPMENT] WHERE [Test Group] = 0" 'False"

        If chkShowObsolete.Checked = False Then
            MyFilter = MyFilter + " AND [Obsolete] = 0" 'False"
        End If

        If ChkShowPastRevision.Checked = False Then
            MyFilter = MyFilter + " AND [Active Rev] = 1" 'True"
        End If

        MyFilter = MyFilter + " ORDER BY [ID] Desc"

        '_MyTestEquipmentDataAdaptor = New OleDb.OleDbDataAdapter("SELECT * From [TEST_EQUIPMENT] ORDER BY [ID] Desc Where [Active Rev] = True AND [Obsolete] = False AND [Test Group] = False", frmFailureBrowser.gMyMeterSpecOleDBConnection)
        '_MyTestEquipmentDataAdaptor = New OleDb.OleDbDataAdapter("SELECT * From [TEST_EQUIPMENT] WHERE [Active Rev] = True AND [Obsolete] = False AND [Test Group] = False ORDER BY [ID] Desc", frmFailureBrowser.gMyMeterSpecOleDBConnection)
        _MyTestEquipmentDataAdaptor = New SqlDataAdapter(MyFilter, frmFailureBrowser.gMyMeterSpecDBConnection)
        _MyTestEquipmentDataAdaptor.Fill(_MyTestEquipmentTable)
        _MyTestEquipmentBindingSource = New BindingSource
        _MyTestEquipmentBindingSource.DataSource = _MyTestEquipmentTable
        dgvAllTestGroupMembers.DataSource = _MyTestEquipmentBindingSource


        'Display allready slected test equipment in the 
        _MySelectedTestEquipmentTable = _MyTestEquipmentTable.Copy
        Try
            _MySelectedTestEquipmentBindingSource = New BindingSource
            _MySelectedTestEquipmentBindingSource.DataSource = _MySelectedTestEquipmentTable

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

        UpdateEquipmentList()
    End Sub

    Private Sub frmSelectTestGroupMembers_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ' Me.Close()
    End Sub

    Private Sub frmSelectTestGroupMembers_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _MyDatabaseAccess = New cCustomDataBaseAccess

        'initalize
        Me.DialogResult = Windows.Forms.DialogResult.None





    End Sub

    Public Function UpdateEquipmentList() As Boolean
        UpdateEquipmentList = True

        Dim strSelectedTestEquipment As String() = Nothing

        If gTestEquipmentUniqueIndex.Trim <> "" Then
            strSelectedTestEquipment = Me.gTestEquipmentUniqueIndex.Split(";")
        End If


        Dim myFilter As String = ""

        If strSelectedTestEquipment IsNot Nothing Then
            For i = 0 To strSelectedTestEquipment.Length - 1
                If i = 0 Then
                    myFilter = "[INDEX] = " + strSelectedTestEquipment(i)
                Else
                    myFilter = myFilter + " OR [INDEX] = " + strSelectedTestEquipment(i)
                End If
            Next
        Else
            myFilter = "[INDEX] = -1" 'Should Return Nothing
        End If
        Dim ThisFilter As String = myFilter
        _MySelectedTestEquipmentBindingSource.Filter = ThisFilter
        _MySelectedTestEquipmentBindingSource.Sort = "ID DESC"
        dgvTestGroupMembersSelected.DataSource = _MySelectedTestEquipmentBindingSource

    End Function

    Private Sub btnAddSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnAddSelected.Click

        'Append semi-colon delimited list to existing list
        For Each MyRow As DataGridViewRow In dgvAllTestGroupMembers.SelectedRows
            If MyRow.Cells("INDEX").Value IsNot Nothing Then
                If gTestEquipmentUniqueIndex.Trim = "" Then
                    gTestEquipmentUniqueIndex = MyRow.Cells("INDEX").Value.ToString
                Else
                    gTestEquipmentUniqueIndex = gTestEquipmentUniqueIndex + ";" + MyRow.Cells("INDEX").Value.ToString
                End If
            End If

        Next

        'create string array to hold new list of unique values
        Dim strSelectedTestEquipment As List(Of String) = New List(Of String)
        Try
            'now remove duplicates  
            If gTestEquipmentUniqueIndex.Trim <> "" Then
                strSelectedTestEquipment.AddRange(Me.gTestEquipmentUniqueIndex.Split(";").Distinct)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        'now repopulate delimited list
        If strSelectedTestEquipment.Count > 0 Then
            'Clear buffer
            gTestEquipmentUniqueIndex = ""

            For i = 0 To strSelectedTestEquipment.Count - 1
                If gTestEquipmentUniqueIndex.Trim = "" Then
                    gTestEquipmentUniqueIndex = strSelectedTestEquipment(i)
                Else
                    gTestEquipmentUniqueIndex = gTestEquipmentUniqueIndex + ";" + strSelectedTestEquipment(i)
                End If
            Next
        End If

        'update list
        UpdateEquipmentList()
    End Sub

    Private Sub btnRemoveSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelected.Click
        'Append semi-colon delimited list to existing list

        'create string array to hold new list  
        Dim strSelectedTestEquipment As List(Of String) = New List(Of String)

        'now populate the list
        If gTestEquipmentUniqueIndex.Trim <> "" Then
            strSelectedTestEquipment.AddRange(Me.gTestEquipmentUniqueIndex.Split(";"))
        End If

        For Each MyRow As DataGridViewRow In dgvTestGroupMembersSelected.SelectedRows
            If MyRow.Cells("INDEX").Value IsNot Nothing Then
                If gTestEquipmentUniqueIndex.Trim = "" Then
                    Exit For
                Else
                    For i = 0 To strSelectedTestEquipment.Count - 1
                        If MyRow.Cells("INDEX").Value.ToString = strSelectedTestEquipment.Item(i) Then
                            strSelectedTestEquipment.RemoveAt(i)
                            Exit For
                        End If
                    Next
                End If
            End If
        Next

        If strSelectedTestEquipment.Count > 0 Then
            'Clear buffer
            gTestEquipmentUniqueIndex = ""
            For i = 0 To strSelectedTestEquipment.Count - 1
                If gTestEquipmentUniqueIndex.Trim = "" Then
                    gTestEquipmentUniqueIndex = strSelectedTestEquipment(i)
                Else
                    gTestEquipmentUniqueIndex = gTestEquipmentUniqueIndex + ";" + strSelectedTestEquipment(i)
                End If
            Next
        ElseIf strSelectedTestEquipment.Count = 0 Then
            gTestEquipmentUniqueIndex = ""
        End If

        'update list
        UpdateEquipmentList()

    End Sub

    Private Sub btnApplyChanges_Click(sender As System.Object, e As System.EventArgs) Handles btnApplyChanges.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancelChanges_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelChanges.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSelectTestGroupMembers_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        dgvAllTestGroupMembers.ReadOnly = True
        dgvTestGroupMembersSelected.ReadOnly = True
        RefreshList()

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        Refresh()
    End Sub
End Class