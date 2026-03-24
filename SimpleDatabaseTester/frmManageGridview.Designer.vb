<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageGridview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageGridview))
        Me.lbHiddenColumns = New System.Windows.Forms.ListBox()
        Me.lbVisibleColumns = New System.Windows.Forms.ListBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GetColumnInfo = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpSplitView = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.DetailViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.tlpSplitView.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbHiddenColumns
        '
        Me.lbHiddenColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbHiddenColumns.FormattingEnabled = True
        Me.lbHiddenColumns.HorizontalScrollbar = True
        Me.lbHiddenColumns.Location = New System.Drawing.Point(3, 3)
        Me.lbHiddenColumns.Name = "lbHiddenColumns"
        Me.lbHiddenColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbHiddenColumns.Size = New System.Drawing.Size(188, 355)
        Me.lbHiddenColumns.Sorted = True
        Me.lbHiddenColumns.TabIndex = 0
        '
        'lbVisibleColumns
        '
        Me.lbVisibleColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbVisibleColumns.FormattingEnabled = True
        Me.lbVisibleColumns.HorizontalScrollbar = True
        Me.lbVisibleColumns.Location = New System.Drawing.Point(297, 3)
        Me.lbVisibleColumns.Name = "lbVisibleColumns"
        Me.lbVisibleColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbVisibleColumns.Size = New System.Drawing.Size(189, 355)
        Me.lbVisibleColumns.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAdd.Location = New System.Drawing.Point(3, 150)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(88, 24)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "Add >>"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRemove.Location = New System.Drawing.Point(3, 180)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(88, 24)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "<<Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMoveDown.Location = New System.Drawing.Point(3, 210)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(88, 24)
        Me.btnMoveDown.TabIndex = 5
        Me.btnMoveDown.Text = "Move Down"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMoveUp.Location = New System.Drawing.Point(3, 120)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(88, 24)
        Me.btnMoveUp.TabIndex = 4
        Me.btnMoveUp.Text = "Move Up"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.Location = New System.Drawing.Point(3, 3)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(384, 355)
        Me.ListBox1.TabIndex = 6
        '
        'GetColumnInfo
        '
        Me.GetColumnInfo.Location = New System.Drawing.Point(393, 3)
        Me.GetColumnInfo.Name = "GetColumnInfo"
        Me.GetColumnInfo.Size = New System.Drawing.Size(75, 36)
        Me.GetColumnInfo.TabIndex = 7
        Me.GetColumnInfo.Text = "Get Column information"
        Me.GetColumnInfo.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbHiddenColumns, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbVisibleColumns, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(489, 361)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnMoveUp, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.btnAdd, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.btnMoveDown, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.btnRemove, 0, 3)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(197, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 6
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(94, 355)
        Me.TableLayoutPanel3.TabIndex = 10
        '
        'tlpSplitView
        '
        Me.tlpSplitView.ColumnCount = 2
        Me.tlpSplitView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpSplitView.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpSplitView.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.tlpSplitView.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.tlpSplitView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpSplitView.Location = New System.Drawing.Point(0, 24)
        Me.tlpSplitView.Name = "tlpSplitView"
        Me.tlpSplitView.RowCount = 1
        Me.tlpSplitView.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 367.0!))
        Me.tlpSplitView.Size = New System.Drawing.Size(991, 367)
        Me.tlpSplitView.TabIndex = 9
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.ListBox1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.GetColumnInfo, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(498, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(490, 361)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailViewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(991, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DetailViewToolStripMenuItem
        '
        Me.DetailViewToolStripMenuItem.CheckOnClick = True
        Me.DetailViewToolStripMenuItem.Name = "DetailViewToolStripMenuItem"
        Me.DetailViewToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.DetailViewToolStripMenuItem.Text = "Show Col Info"
        '
        'frmManageGridview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 391)
        Me.Controls.Add(Me.tlpSplitView)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmManageGridview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage FR Columns"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.tlpSplitView.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbHiddenColumns As System.Windows.Forms.ListBox
    Friend WithEvents lbVisibleColumns As System.Windows.Forms.ListBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents GetColumnInfo As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tlpSplitView As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents DetailViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
