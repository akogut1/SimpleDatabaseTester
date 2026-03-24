<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddItemToTable
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
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelAMRTab = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanelAMRDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelAMRrNotes = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanelAMRTab.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(666, 12)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.TableLayoutPanelAMRTab)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(759, 472)
        Me.Panel1.TabIndex = 1
        '
        'TableLayoutPanelAMRTab
        '
        Me.TableLayoutPanelAMRTab.ColumnCount = 1
        Me.TableLayoutPanelAMRTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelAMRTab.Controls.Add(Me.TableLayoutPanelAMRDetails, 0, 1)
        Me.TableLayoutPanelAMRTab.Controls.Add(Me.Panel2, 0, 2)
        Me.TableLayoutPanelAMRTab.Location = New System.Drawing.Point(193, 61)
        Me.TableLayoutPanelAMRTab.Name = "TableLayoutPanelAMRTab"
        Me.TableLayoutPanelAMRTab.RowCount = 3
        Me.TableLayoutPanelAMRTab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanelAMRTab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelAMRTab.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRTab.Size = New System.Drawing.Size(416, 365)
        Me.TableLayoutPanelAMRTab.TabIndex = 1
        '
        'TableLayoutPanelAMRDetails
        '
        Me.TableLayoutPanelAMRDetails.AutoSize = True
        Me.TableLayoutPanelAMRDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanelAMRDetails.ColumnCount = 4
        Me.TableLayoutPanelAMRDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelAMRDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelAMRDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanelAMRDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanelAMRDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelAMRDetails.Location = New System.Drawing.Point(3, 25)
        Me.TableLayoutPanelAMRDetails.Name = "TableLayoutPanelAMRDetails"
        Me.TableLayoutPanelAMRDetails.RowCount = 8
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRDetails.Size = New System.Drawing.Size(410, 236)
        Me.TableLayoutPanelAMRDetails.TabIndex = 233
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.TableLayoutPanelAMRrNotes)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 267)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(410, 95)
        Me.Panel2.TabIndex = 234
        '
        'TableLayoutPanelAMRrNotes
        '
        Me.TableLayoutPanelAMRrNotes.ColumnCount = 2
        Me.TableLayoutPanelAMRrNotes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.5122!))
        Me.TableLayoutPanelAMRrNotes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.48781!))
        Me.TableLayoutPanelAMRrNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelAMRrNotes.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelAMRrNotes.Name = "TableLayoutPanelAMRrNotes"
        Me.TableLayoutPanelAMRrNotes.RowCount = 1
        Me.TableLayoutPanelAMRrNotes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAMRrNotes.Size = New System.Drawing.Size(410, 95)
        Me.TableLayoutPanelAMRrNotes.TabIndex = 234
        '
        'frmAddItemToTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 472)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmAddItemToTable"
        Me.Text = "frmAddItemToTable"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanelAMRTab.ResumeLayout(False)
        Me.TableLayoutPanelAMRTab.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelAMRTab As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanelAMRDetails As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelAMRrNotes As System.Windows.Forms.TableLayoutPanel
End Class
