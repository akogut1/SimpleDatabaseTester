Public Class frmManageGridview

    Dim _MyWidth As Integer = Me.Width
    Dim _EnableEvents As Boolean = False
    Private m_ColumnName As String

    Public Property ColumnName() As String
        Get
            Return m_ColumnName
        End Get
        Set(value As String)
            m_ColumnName = value
        End Set
    End Property


    Public Class cColumnIdentifier
        Private mIndex As Integer
        Private mColumnName As String
        Private mHeader As String
        Private mFrozen As Boolean
        Private mDisplayIndex As Integer
        Private mVisible As Boolean

        Public Property Index() As Integer
            Get
                Return mIndex
            End Get
            Set(value As Integer)
                mIndex = value
            End Set
        End Property

        Public Property ColumnName As String
            Get
                Return mColumnName
            End Get
            Set(value As String)
                mColumnName = value
            End Set
        End Property
        Public Property Header As String
            Get
                Return mHeader
            End Get
            Set(value As String)
                mHeader = value
            End Set
        End Property
        Public Property Frozen As Boolean
            Get
                Return mFrozen
            End Get
            Set(value As Boolean)
                mFrozen = value
            End Set
        End Property
        Public Property DisplayIndex As Integer
            Get
                Return mDisplayIndex
            End Get
            Set(value As Integer)
                mDisplayIndex = value
            End Set
        End Property

        Public Property Visible As Boolean
            Get
                Return mVisible
            End Get
            Set(value As Boolean)
                mVisible = value
            End Set
        End Property

        Public Sub New(MyIndex As Integer, MyName As String, MyHeader As String, IsFrozen As Boolean, MyDisplayIndex As Integer, IsVisible As Boolean)

            Index = MyIndex
            ColumnName = MyName
            Header = MyHeader
            Frozen = IsFrozen
            DisplayIndex = MyDisplayIndex
            Visible = IsVisible
        End Sub

    End Class

    Public ColumnIndentifierList As New List(Of cColumnIdentifier)
    Public gMaximumVisibleDisplayIndex As Integer
    Private Sub GetColumnInformaton()
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        lbHiddenColumns.Items.Clear()
        lbVisibleColumns.Items.Clear()
        ColumnIndentifierList.Clear()
        ListBox1.Items.Clear()
        lbHiddenColumns.Sorted = True

        frmFailureBrowser.dgvFailureReportDataGridView.Columns(0).Frozen = False
        frmFailureBrowser.dgvFailureReportDataGridView.Columns("NEW ID").Frozen = True

        For i = 0 To frmFailureBrowser.dgvFailureReportDataGridView.Columns.Count - 1
            lbVisibleColumns.Items.Add("")
            ' lbHiddenColumns.Items.Add("")
        Next
        'Exit Sub
        'Start at 1 since Index (0) is Automatic Primary Key
        For i = 1 To frmFailureBrowser.dgvFailureReportDataGridView.Columns.Count - 1


            ColumnIndentifierList.Add(New cColumnIdentifier(frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Index, _
                                                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Name, _
                                                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).HeaderText, _
                                                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Frozen, _
                                                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).DisplayIndex, _
                                                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Visible))

            If frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Visible = True Then
                'lbVisibleColumns.Items.Add(frmFailureBrowser.Failure_ReportDataGridView.Columns(i).HeaderText)
                'lbVisibleColumns.Items.Insert(frmFailureBrowser.Failure_ReportDataGridView.Columns(i).DisplayIndex, _
                '                              frmFailureBrowser.Failure_ReportDataGridView.Columns(i).HeaderText)

                lbVisibleColumns.Items(frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).DisplayIndex) = frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).HeaderText

                'If frmFailureBrowser.Failure_ReportDataGridView.Columns(i).DisplayIndex > gMaximumVisibleDisplayIndex Then
                '    gMaximumVisibleDisplayIndex = frmFailureBrowser.Failure_ReportDataGridView.Columns(i).DisplayIndex
                'End If

            Else
                lbHiddenColumns.Items.Add(frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).HeaderText)

                'lbHiddenColumns.Items.Insert(frmFailureBrowser.Failure_ReportDataGridView.Columns(i).DisplayIndex, _
                '                          frmFailureBrowser.Failure_ReportDataGridView.Columns(i).HeaderText)

                '  lbHiddenColumns.Items(frmFailureBrowser.Failure_ReportDataGridView.Columns(i).DisplayIndex) = frmFailureBrowser.Failure_ReportDataGridView.Columns(i).HeaderText
            End If
        Next
        'Exit Sub
        ''Remove Empty items
        'For i = lbHiddenColumns.Items.Count - 1 To 0 Step -1
        '    If lbHiddenColumns.Items(i).ToString.Trim = "" Then
        '        lbHiddenColumns.Items.RemoveAt(i)
        '    End If
        'Next

        ' Exit Sub
        'remove empty items
        lbHiddenColumns.Sorted = True
        For i = lbVisibleColumns.Items.Count - 1 To 0 Step -1
            If lbVisibleColumns.Items(i).ToString.Trim = "" Then
                lbVisibleColumns.Items.RemoveAt(i)
            End If

        Next
        ' Exit Sub
        lbHiddenColumns.Sorted = True
        'Reindex the The Display indexes
        For i = 0 To lbVisibleColumns.Items.Count - 1
            For Each item As cColumnIdentifier In ColumnIndentifierList
                'ColumnIInfo = item
                If item.Header = lbVisibleColumns.Items(i).ToString.Trim Then
                    Try
                        frmFailureBrowser.dgvFailureReportDataGridView.Columns(item.ColumnName).DisplayIndex = i
                        'lbVisibleColumns.Items.Add(HeaderName)
                        'lbHiddenColumns.Items.Remove(lbHiddenColumns.SelectedItem)
                        Exit For
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        Exit For
                    End Try
                End If
            Next
        Next

        'Now Reorder nonVisible Columns now
        Dim MyDisplayIndex = lbVisibleColumns.Items.Count

        For i = 0 To frmFailureBrowser.dgvFailureReportDataGridView.Columns.Count - 1

            If frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Visible = False Then
                frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).DisplayIndex = MyDisplayIndex
                MyDisplayIndex += 1
            End If

        Next

        'Display new indexes
        Dim TestString As String
        For i = 0 To frmFailureBrowser.dgvFailureReportDataGridView.Columns.Count - 1

            TestString = "Ddx=" + frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).DisplayIndex.ToString + vbTab + _
                                   frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Name.ToString + vbTab + vbTab + _
                                   " IDx=" + frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Index.ToString + vbTab + _
                                     " FRZ=" + frmFailureBrowser.dgvFailureReportDataGridView.Columns(i).Frozen.ToString

            ListBox1.Items.Add(TestString)
        Next


        gMaximumVisibleDisplayIndex = lbVisibleColumns.Items.Count - 1
        frmFailureBrowser.dgvFailureReportDataGridView.Show()
    End Sub


    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        GetColumnInformaton()

        Dim mystop As Integer = 1
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        'Me.Hide()
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        Dim ColumnIInfo As New cColumnIdentifier(0, "", "", False, 0, False)
        Dim HeaderName As String
        Dim SelectedItems As New List(Of String)
        For Each ItemSelected In lbHiddenColumns.SelectedItems
            SelectedItems.Add(New String(ItemSelected.ToString.Trim))
        Next

        If SelectedItems.Count > 0 Then
            Try
                For Each ItemSelected In SelectedItems

                    HeaderName = ItemSelected.ToString.Trim 'lbHiddenColumns.SelectedItem.ToString.Trim


                    For Each item As cColumnIdentifier In ColumnIndentifierList
                        ColumnIInfo = item
                        If item.Header = HeaderName Then
                            frmFailureBrowser.dgvFailureReportDataGridView.Columns(item.ColumnName).Visible = True
                            ' frmFailureBrowser.Failure_ReportDataGridView.Columns(item.ColumnName).DisplayIndex = gMaximumVisibleDisplayIndex + 1

                            'lbVisibleColumns.Items.Add(HeaderName)
                            'lbHiddenColumns.Items.Remove(lbHiddenColumns.SelectedItem)
                            Exit For
                        End If
                    Next
                    GetColumnInformaton()
                Next
            Catch ex As NullReferenceException
                frmFailureBrowser.dgvFailureReportDataGridView.Show()
            Catch ex As Exception
                frmFailureBrowser.dgvFailureReportDataGridView.Show()
                MsgBox(ex.ToString)
            End Try

        End If
        frmFailureBrowser.dgvFailureReportDataGridView.Show()
        ' Me.Show()
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        'Dim ColumnIInfo As New cColumnIdentifier(0, "", "", False, 0, False)
        'frmFailureBrowser.Hide()
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        lbHiddenColumns.Hide()
        lbVisibleColumns.Hide()
        ListBox1.Hide()
        Dim HeaderName As String
        Dim SelectedItems As New List(Of String)
        For Each ItemSelected In lbVisibleColumns.SelectedItems
            SelectedItems.Add(New String(ItemSelected.ToString.Trim))
        Next
        If SelectedItems.Count > 0 Then
            Try
                For Each ItemSelected In SelectedItems

                    HeaderName = ItemSelected.ToString.Trim 'lbHiddenColumns.SelectedItem.ToString.Trim
                    'HeaderName = lbVisibleColumns.SelectedItem.ToString.Trim


                    'For Each item As cColumnIdentifier In ColumnIndentifierList
                    For i = 0 To ColumnIndentifierList.Count - 1
                        ' ColumnIInfo = item
                        If ColumnIndentifierList.Item(i).Header = HeaderName Then
                            If ColumnIndentifierList.Item(i).Frozen = False Then
                                frmFailureBrowser.dgvFailureReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).Visible = False
                                ' frmFailureBrowser.Failure_ReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).DisplayIndex = gMaximumVisibleDisplayIndex - 1 'move it far away GetColumnInfo() will fix it
                                ' lbHiddenColumns.Items.Add(HeaderName)
                                ' lbVisibleColumns.Items.Remove(lbVisibleColumns.SelectedItem)
                            Else
                                MsgBox("Report # May not be Hidden or moved")
                            End If
                        End If

                    Next
                    GetColumnInformaton()
                Next
            Catch ex As NullReferenceException
                frmFailureBrowser.dgvFailureReportDataGridView.Show()
                'lbLog.Items.Add(ex.ToString)
            Catch ex As Exception
                frmFailureBrowser.dgvFailureReportDataGridView.Show()
                MsgBox(ex.ToString)
            End Try
        End If
        lbHiddenColumns.Show()
        lbVisibleColumns.Show()
        ListBox1.Show()
        frmFailureBrowser.dgvFailureReportDataGridView.Show()
        '  frmFailureBrowser.Show()
    End Sub

    Private Sub btnMoveUp_Click(sender As System.Object, e As System.EventArgs) Handles btnMoveUp.Click
        'Dim ColumnIInfo As New cColumnIdentifier(0, "", "", False, 0, False)
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        Dim HeaderName As String
        Dim HeaderName2 As String
        Dim CurrentIndex As Integer
        Dim SwapIndex As Integer
        Try
            CurrentIndex = lbVisibleColumns.SelectedIndex
            HeaderName = lbVisibleColumns.SelectedItem.ToString.Trim
            SwapIndex = lbVisibleColumns.SelectedIndex - 1
            HeaderName2 = lbVisibleColumns.Items(SwapIndex).ToString
            If SwapIndex > 0 Then
                'For Each item As cColumnIdentifier In ColumnIndentifierList
                For i = 0 To ColumnIndentifierList.Count - 1
                    ' ColumnIInfo = item
                    If ColumnIndentifierList.Item(i).Header = HeaderName Then
                        frmFailureBrowser.dgvFailureReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).DisplayIndex = SwapIndex
                    End If

                    If ColumnIndentifierList.Item(i).Header = HeaderName2 Then
                        frmFailureBrowser.dgvFailureReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).DisplayIndex = CurrentIndex
                    End If

                Next

                GetColumnInformaton()
                lbVisibleColumns.SelectedIndex = SwapIndex
            Else
                MsgBox("Column cannot be moved any Further")
                lbVisibleColumns.SelectedIndex = CurrentIndex
            End If

            frmFailureBrowser.dgvFailureReportDataGridView.Show()
        Catch ex As NullReferenceException
            frmFailureBrowser.dgvFailureReportDataGridView.Show()
            MsgBox(ex.ToString)
        Catch ex As Exception
            frmFailureBrowser.dgvFailureReportDataGridView.Show()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnMoveDown_Click(sender As System.Object, e As System.EventArgs) Handles btnMoveDown.Click
        'Dim ColumnIInfo As New cColumnIdentifier(0, "", "", False, 0, False)
        frmFailureBrowser.dgvFailureReportDataGridView.Hide()
        Dim HeaderName As String
        Dim HeaderName2 As String
        Dim CurrentIndex As Integer
        Dim SwapIndex As Integer
        Try
            CurrentIndex = lbVisibleColumns.SelectedIndex
            HeaderName = lbVisibleColumns.SelectedItem.ToString.Trim
            SwapIndex = lbVisibleColumns.SelectedIndex + 1

            If SwapIndex <= gMaximumVisibleDisplayIndex Then
                HeaderName2 = lbVisibleColumns.Items(SwapIndex).ToString
                'For Each item As cColumnIdentifier In ColumnIndentifierList
                For i = 0 To ColumnIndentifierList.Count - 1
                    ' ColumnIInfo = item
                    If ColumnIndentifierList.Item(i).Header = HeaderName2 Then
                        frmFailureBrowser.dgvFailureReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).DisplayIndex = CurrentIndex
                    End If

                    If ColumnIndentifierList.Item(i).Header = HeaderName Then
                        frmFailureBrowser.dgvFailureReportDataGridView.Columns(ColumnIndentifierList.Item(i).ColumnName).DisplayIndex = SwapIndex
                    End If

                Next

                GetColumnInformaton()
                lbVisibleColumns.SelectedIndex = SwapIndex
            Else
                MsgBox("Column cannot be moved any further")
                lbVisibleColumns.SelectedIndex = CurrentIndex
            End If

            frmFailureBrowser.dgvFailureReportDataGridView.Show()
        Catch ex As NullReferenceException
            frmFailureBrowser.dgvFailureReportDataGridView.Show()
            MsgBox(ex.ToString)
        Catch ex As Exception
            frmFailureBrowser.dgvFailureReportDataGridView.Show()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub GetColumnInfo_Click(sender As System.Object, e As System.EventArgs) Handles GetColumnInfo.Click
        GetColumnInformaton()
    End Sub


    Private Sub DetailViewToolStripMenuItem_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles DetailViewToolStripMenuItem.CheckedChanged
        If _EnableEvents = True Then
            If DetailViewToolStripMenuItem.Checked = True Then
                tlpSplitView.ColumnStyles(1).SizeType = SizeType.Percent
                tlpSplitView.ColumnStyles(1).Width = 0
                tlpSplitView.ColumnStyles(0).SizeType = SizeType.Percent
                tlpSplitView.ColumnStyles(0).Width = 100
                Me.Width = _MyWidth / 2
                DetailViewToolStripMenuItem.Text = "Show Column Info"
            Else
                tlpSplitView.ColumnStyles(1).SizeType = SizeType.Percent
                tlpSplitView.ColumnStyles(1).Width = 50
                tlpSplitView.ColumnStyles(0).SizeType = SizeType.Percent
                tlpSplitView.ColumnStyles(0).Width = 50
                Me.Width = _MyWidth
                DetailViewToolStripMenuItem.Text = "Hide Column Info"
            End If
        End If

    End Sub


    Private Sub frmManageGridview_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        _MyWidth = (Me.Width)
        _EnableEvents = True
        DetailViewToolStripMenuItem.Checked = True
    End Sub
End Class