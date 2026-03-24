Imports System.Drawing.Printing
Imports ICSharpCode.SharpZipLib.Zip

Public Class DGVPrinter
    Public Property Title As String
    Public Property SubTitle As String
    Public Property SubTitleFormatFlags As System.Drawing.StringFormatFlags
    Public Property PageNumbers As Boolean
    Public Property PageNumberInHeader As Boolean
    Public Property ColumnWidth As ColumnWidthSetting
    Public Property HeaderCellAlignment As System.Drawing.StringAlignment
    Public Property Footer As String
    Public Property FooterSpacing As Single
    Public Function DisplayPrintDialog() As Boolean
        Return True
    End Function
    Public Sub PrintNoDisplay(grid As System.Windows.Forms.DataGridView)
    End Sub
    Public Sub PrintPreviewDataGridView(grid As System.Windows.Forms.DataGridView)
    End Sub
    Public Sub PrintDataGridView(grid As System.Windows.Forms.DataGridView)
    End Sub
    Public Enum ColumnWidthSetting
        Automatic
        DataColumnsOnly
        AllColumns
        Porportional
    End Enum
End Class
Public Class MsgBoxCheckMessageBox
    Public Function Show(registryKey As String, dontShowKey As String, ByRef dontShowState As Boolean, dontShowMsg As String, message As String, title As String, buttons As System.Windows.Forms.MessageBoxButtons, icon As System.Windows.Forms.MessageBoxIcon) As System.Windows.Forms.DialogResult
        Return System.Windows.Forms.MessageBox.Show(message, title, buttons, icon)
    End Function
End Class

Public Class xboXComboBox
    Inherits System.Windows.Forms.ComboBox
    Public Sub New()
        MyBase.New()
        Me.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    End Sub
    Public Property [ReadOnly] As Boolean
        Get
            Return Not Me.Enabled
        End Get
        Set(value As Boolean)
            Me.Enabled = Not value
        End Set
    End Property
    Public Shadows Event DropDown(sender As Object, e As System.EventArgs)
    Public Shadows Event MouseEnter(sender As Object, e As System.EventArgs)
    Public Shadows Event KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
    Public Shadows Event SelectionChangeCommitted(sender As Object, e As System.EventArgs)
    Public Shadows Event SelectedValueChanged(sender As Object, e As System.EventArgs)
End Class

Public Class ReadOnlyComboBox
    Inherits System.Windows.Forms.ComboBox
    Public Sub New()
        MyBase.New()
        Me.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    End Sub
    Public Property [ReadOnly] As Boolean
        Get
            Return Not Me.Enabled
        End Get
        Set(value As Boolean)
            Me.Enabled = Not value
        End Set
    End Property
    Public Shadows Event DropDown(sender As Object, e As System.EventArgs)
    Public Shadows Event MouseEnter(sender As Object, e As System.EventArgs)
    Public Shadows Event KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
    Public Shadows Event SelectionChangeCommitted(sender As Object, e As System.EventArgs)
    Public Shadows Event SelectedValueChanged(sender As Object, e As System.EventArgs)
End Class

Public Class ZipEntry
    Public Property FileName As String
    Public Sub Extract(path As String)
    End Sub
    Public Sub Extract(path As String, action As ExtractExistingFileAction)
    End Sub
End Class

Public Enum ExtractExistingFileAction
    OverwriteSilently
    DoNotOverwrite
    InvokeExtractProgressEvent
End Enum
Public Class ZipFile
    Implements IDisposable
    Implements IEnumerable(Of ZipEntry)
    Private _entries As New List(Of ZipEntry)

    Public Shared Function Read(fileName As String) As ZipFile
        Return New ZipFile(fileName)
    End Function
    Public ReadOnly Property EntryFileNames As IEnumerable(Of String)
        Get
            Return New List(Of String)()
        End Get
    End Property
    Public Function ContainsEntry(fileName As String) As Boolean
        Return False
    End Function
    Public Sub UpdateFile(fileName As String)
    End Sub
    Public Sub UpdateFile(fileName As String, directoryPathInArchive As String)
    End Sub
    Public Sub RemoveEntry(fileName As String)
    End Sub
    Public Sub RemoveEntry(entry As ZipEntry)
    End Sub
    Public Sub New()
    End Sub
    Public Sub New(fileName As String)
    End Sub
    Public Sub AddFile(fileName As String)
    End Sub
    Public Sub AddFile(fileName As String, directoryPathInArchive As String)
    End Sub
    Public Sub Save()
    End Sub
    Public Sub Save(fileName As String)
    End Sub
    Public Function GetEnumerator() As IEnumerator(Of ZipEntry) Implements IEnumerable(Of ZipEntry).GetEnumerator
        Return _entries.GetEnumerator()
    End Function
    Private Function GetEnumerator2() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _entries.GetEnumerator()
    End Function
    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub
End Class
Public Class LegacyPrintForm
    Inherits System.ComponentModel.Component
    Public Property DocumentName As String
    Public Property Form As System.Windows.Forms.Form
    Public Property PrintAction As System.Drawing.Printing.PrintAction
    Public Property PrinterSettings As New System.Drawing.Printing.PrinterSettings()
    Public Property PrintFileName As String
    Public Sub New()
    End Sub
    Public Sub New(owner As Object)
    End Sub
End Class