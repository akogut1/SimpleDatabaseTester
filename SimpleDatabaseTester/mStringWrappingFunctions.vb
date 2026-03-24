''' <summary>
''' Collection of Helper Functions to assist formmating gridview columns into a string for displaying as  text or printing...
''' </summary>
''' <remarks>Frank Boudreau 5.3.2017</remarks>
Public Module mStringWrappingFunctions
    ''' <summary>
    ''' This Functionwraps a string to fit in a column.
    ''' </summary>
    ''' <param name="MyString">String to Wrap</param>
    ''' <param name="Length">Number of characters per line </param>
    ''' <param name="PadLeft">Addtional spaces to Pad to the left</param>
    ''' <param name="PadRight">Addtional spaces to Pad to the right</param>
    ''' <returns>String Array, which each element is a wrapped line</returns>
    ''' <remarks>Frank boudreau 5.3.2017</remarks>
    Public Function PadandWrapString(MyString As String, Length As Integer, PadLeft As Integer, PadRight As Integer) As String()
        'Minimum Length is 1
        If Length <= 0 Then
            Length = 1
        End If

        If MyString.Length + PadLeft + PadRight = Length Then
            MyString = MyString.PadRight(Length)
            MyString = MyString.PadLeft(PadLeft + MyString.Length)
            MyString = MyString.PadRight(PadRight + MyString.Length)
            Dim ReturnString() As String = {MyString}
            Return ReturnString
        ElseIf MyString.Length + PadLeft + PadRight < Length Then
            MyString = MyString.PadRight(Length)
            MyString = MyString.PadLeft(PadLeft + MyString.Length)
            MyString = MyString.PadRight(PadRight + MyString.Length)
            Dim ReturnString() As String = {MyString}
            Return ReturnString
        Else 'Need to Wrap

            Dim TempString As String  'Split based on spaces to start
            Dim TempStringList As New List(Of String)
            Dim RealLength = Length + PadLeft + PadRight
            Dim StartPosition = 0
            Try
                While StartPosition < MyString.Length
                    Dim MyLength As Integer = MyString.Length
                    Dim fragment As String
                    If MyString.Length - StartPosition >= Length + 1 Then
                        fragment = MyString.Substring(StartPosition, Length + 1)
                    Else
                        fragment = MyString.Substring(StartPosition, MyString.Length - StartPosition)
                        fragment = fragment + " " 'append a space to find since last section...
                    End If

                    Dim MyChar As String = ""
                    Dim SpaceFound As Boolean = False
                    For i = fragment.Length - 1 To 0 Step -1 'One-Based Referenced
                        MyChar = fragment.Chars(i)
                        If MyChar = " " Then
                            fragment = fragment.Substring(0, i + 1)
                            'TempStringList.Add(fragment.Substring(0, i - 1))
                            'StartPosition = StartPosition + i
                            SpaceFound = True
                            Exit For
                        End If
                    Next
                    'advance pointer
                    'remove 1 character from end for wrapping 
                    If Not SpaceFound Then
                        fragment = fragment.Substring(0, fragment.Length - 1)
                    End If
                    StartPosition = StartPosition + fragment.Length
                    'pad frement to Length + PadRight value to be displayed
                    fragment = fragment.PadRight(Length + PadRight)

                    'pad fragment with addtional spaces as required...
                    fragment = fragment.PadLeft(PadLeft + fragment.Length)


                    'now add it
                    TempStringList.Add(fragment)


                End While
            Catch ex As Exception
                'pass exception 
                Throw New Exception("Error Wrapping String" + vbCrLf + ex.ToString)
            End Try
            TempString = String.Join(";", TempStringList)
            Dim ReturnString() As String = TempString.Split(";")
            Return ReturnString
        End If

    End Function

    ''' <summary>
    ''' This function will calculate the number of characters that will fit in each column (cell) of a gridview row.  It defaults to Courier New 
    ''' a fixed width font and 10 PT Font.  It uses the Capital "W" to as a worse case for non-fixed width fonts, however there is no guarantee that it
    ''' will work with all Font's. The formatting for non fixed width font's will not be aligned as well... not recommended
    ''' </summary>
    ''' <param name="MyDataGridviewrow">This is the Datagridrow that is passed in order to measrure column width</param>
    ''' <param name="MyOutputFont">This is the output Font used to calculate the number of characters per Row</param>
    ''' <param name="FontName">Optional, This is the Font to use to calculate the Number of characters that fit in the column</param>
    ''' <param name="FontSize">Optional, This is the size of the font to use for calculation</param>
    ''' <param name="MinimumCharCount">Optional, Default is 2, this is the minimum number of Characters for a column</param>
    ''' <returns>Integer Array with the number of characters for each column of the DatagridRow</returns>
    ''' <remarks>Frank Boudreau 5.3.2017</remarks>
    Public Function CalculateCharsPerDGVColumnCourierNew(MydataGirdView As DataGridView, MyDataGridviewrow As DataGridViewRow, ByRef MyOutputFont As Font, Optional FontName As String = "COURIER NEW", Optional FontSize As Single = 10, Optional MinimumCharCount As Integer = 2) As Integer()

        'Use likely widest letter (This only matters for non fixed width font's ... not recommended...
        Dim MyString As String = "W"
        'set the output font 
        MyOutputFont = New Font(FontName, FontSize)
        'how wide is each letter
        Dim CharWidth As Integer
        'how manychars
        Dim CharCount As Integer
        'number of columns in row
        Dim NumColumns As Integer = MyDataGridviewrow.Cells.Count
        'Return Value Buffer
        Dim CharsPerColumn(NumColumns - 1) As Integer


        For i = 0 To NumColumns - 1
            Dim cell As DataGridViewCell = MyDataGridviewrow.Cells(i)

            'measure the size of a character
            Dim MyStringSize As Size = TextRenderer.MeasureText(MyString, MyOutputFont)

            Try
                CharWidth = MyStringSize.Width / MyString.Length
                'how many chars will fit in the current cell width
                Dim CellWidth As Integer = cell.Size.Width   'cell.GetContentBounds(cell.RowIndex).Width    
                Dim ColumnWidth As Single = MydataGirdView.Columns(i).Width '         
                CharCount = CellWidth / CharWidth

            Catch ex As Exception
                'Handle silently... 

            End Try
            'check to see if minimum
            If CharCount < MinimumCharCount Then
                CharCount = MinimumCharCount
            End If

            CharsPerColumn(i) = CharCount
        Next
        Return CharsPerColumn
    End Function


    ''' <summary>
    ''' This funtions formats the data from in DataGridViewRow for printing or displaying the text in columns in a textfile textbox
    ''' </summary>
    ''' <param name="MyDataGridviewrow">DataGridViewRow that has the Data to be formatted and wrapped</param>
    ''' <param name="CharsPerColumn">Integer Array, Number of Chars in a column. Each element maps to a column in the DGV row</param>
    ''' <param name="PadLeft">Integer array that defines the Left padding for each column. Element number must match number of gridviewrow columns</param>
    ''' <param name="Padright">Integer array that defines the Left padding for each column. Element number must match number of gridviewrow columns</param>
    ''' <returns>Array list array holding the each wrapped string. Each Element of the array is a column. The number items in each list is each line
    ''' of the wrapped string</returns>
    ''' <remarks>Frank Boudreau 5.3.2017</remarks>
    Public Function PadAndWrapEachGridViewRow(MyDataGridviewrow As DataGridViewRow, CharsPerColumn() As Integer, Optional PadLeft() As Integer = Nothing, Optional Padright() As Integer = Nothing) As ArrayList()
        'number of columns in row
        Dim NumColumns As Integer = MyDataGridviewrow.Cells.Count
        'Object to hold return value
        Dim MyArrayList(NumColumns - 1) As ArrayList

        If PadLeft Is Nothing Then
            'set to default
            ReDim PadLeft(NumColumns - 1)
            For i = 0 To NumColumns - 1
                PadLeft(i) = 0
            Next
        ElseIf PadLeft.Length <> NumColumns Then
            'handle silently, set to default
            ReDim PadLeft(NumColumns - 1)
            For i = 0 To NumColumns - 1
                PadLeft(i) = 0
            Next

        End If

        If Padright Is Nothing Then
            'Defatult to 1 character of padding..this creates at least one space after...
            ReDim Padright(NumColumns - 1)
            For i = 0 To NumColumns - 1
                Padright(i) = 1
            Next
        ElseIf Padright.Length <> NumColumns Then
            'handle silently, set to default
            ReDim Padright(NumColumns - 1)
            For i = 0 To NumColumns - 1
                Padright(i) = 1
            Next
        End If

        For i = 0 To NumColumns - 1
            'pointer to the current cell
            Dim cell As DataGridViewCell = MyDataGridviewrow.Cells(i)
            'get the cell value
            Dim MyTempString As String = ""
            Try
                If cell.Value Is Nothing = False Then '
                    'Get the Cell value...remove any combination carriagereturns, linefeeds
                    MyTempString = cell.Value.ToString.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")
                End If

            Catch ex As Exception
                'Handle expection and proceed...
                MyTempString = ""
            End Try

            'Adjust the character length for padding, Lower bound to 1 character.
            Dim Length = CharsPerColumn(i) ' - PadLeft(i) - Padright(i)
            If Length < 1 Then
                Length = 1
            End If

            'declare instance...
            MyArrayList(i) = New ArrayList
            'get the wrapped string as an array...
            Dim MyString() As String = mStringWrappingFunctions.PadandWrapString(MyTempString, Length, PadLeft(i), Padright(i))
            'add it to  the Arraylist
            MyArrayList(i).AddRange(MyString)
        Next

        MyArrayList = PadArrayListArray(MyArrayList)
        'Return value
        Return MyArrayList
    End Function

    ''' <summary>
    ''' This functions takes in ArrayList Array with elements with differing number of items in each elemeent and returns an Arraylist
    ''' with normalized number items in each element
    ''' </summary>
    ''' <param name="MyArrayList">ArryList to be normalized</param>
    ''' <returns>Normalized ArrayList</returns>
    ''' <remarks>Frank Boudreau 5.3.2017</remarks>
    Public Function PadArrayListArray(MyArrayList() As ArrayList) As ArrayList()
        Dim MaxNumberOfItems As Integer = 0
        Dim MyString As String = " "
        Dim PadRightValue As Integer = 0
        For i = 0 To MyArrayList.Length - 1
            If MaxNumberOfItems < MyArrayList(i).Count Then
                MaxNumberOfItems = MyArrayList(i).Count
            End If
        Next

        For i = 0 To MyArrayList.Length - 1
            While MyArrayList(i).Count < MaxNumberOfItems
                If MyArrayList(i).Item(0) Is Nothing Then
                    PadRightValue = 0
                Else
                    PadRightValue = MyArrayList(i).Item(0).ToString.Length
                End If
                MyArrayList(i).Add(MyString.PadRight(PadRightValue))
            End While
        Next
        Return MyArrayList
    End Function
    Function CreateTextColumnHeaderFromDGV(MyDataGridView As DataGridView, CharsPerColumn() As Integer, PadLeft() As Integer, Padright() As Integer) As ArrayList()
        Dim MyLocalDataGridview As New DataGridView
        'For Each Column As DataGridViewColumn In MyDataGridView.Columns
        '    MyLocalDataGridview.Columns.Add(New DataGridViewColumn)
        'Next
        MyLocalDataGridview.ColumnCount = MyDataGridView.Columns.Count
        MyLocalDataGridview.Rows.Add(MyDataGridView.Rows(0).Clone)
        'MyLocalDataGridview.Rows(0) = CType(MyDataGridView.Rows(0).Clone, DataGridViewRow)

        Dim Index As Integer = 0
        For Each cell As DataGridViewColumn In MyLocalDataGridview.Columns
            cell.Width = MyDataGridView.Columns(Index).Width
            MyLocalDataGridview.Rows(0).Cells(Index).Value = MyDataGridView.Columns(Index).Name.ToString
            Index += 1
        Next

        Dim Myarraylist() As ArrayList = PadAndWrapEachGridViewRow(MyLocalDataGridview.Rows(0), CharsPerColumn, PadLeft, Padright)
        Return Myarraylist

    End Function


    Public Function GridviewToFormatedString(MyDataGridView As DataGridView, PadLeft As Integer, PadRight As Integer, MyFontName As String, MyFontSize As Single, MinimumNumberOfCharactersPerColumn As Integer) As String
        Dim MyFont As Font = New Font(MyFontName, MyFontSize)
        Dim NumColumns As Integer = MyDataGridView.Columns.Count
        Dim NumRows As Integer = MyDataGridView.Rows.Count
        Dim MyPadRightArray(NumColumns - 1) As Integer
        Dim MyPadLeftArray(NumColumns - 1) As Integer
        Dim MyDataGridViewRow As DataGridViewRow = MyDataGridView.Rows(0) 'grab the first row t
        Dim MyArraylist(NumColumns - 1) As ArrayList
        'Calculate how many characters wiill fit in each column formatted text string
        Dim MyCharCountPerColumn() As Integer = CalculateCharsPerDGVColumnCourierNew(MyDataGridView, MyDataGridViewRow, MyFont, MyFontName, MyFontSize, MinimumNumberOfCharactersPerColumn)
        Dim OutputString As String = ""

        For i = 0 To NumColumns - 1
            MyPadLeftArray(i) = PadLeft
            MyPadRightArray(i) = PadRight
        Next

        'get Test equipment header...
        MyArraylist = CreateTextColumnHeaderFromDGV(MyDataGridView, MyCharCountPerColumn, MyPadLeftArray, MyPadRightArray)

        'addheader to ouput string...
        For m = 0 To MyArraylist(0).Count - 1
            For k = 0 To NumColumns - 1
                Try
                    OutputString = OutputString + MyArraylist(k).Item(m).ToString.Replace(vbCrLf, "")
                Catch ex As Exception
                    Dim TempString = MyCharCountPerColumn(k).ToString
                    OutputString = OutputString + TempString.PadRight(MyCharCountPerColumn(k))
                End Try

                If k = NumColumns - 1 Then
                    OutputString = OutputString + vbCrLf
                End If
            Next
        Next


        'Get the test equipment info
        For i = 0 To NumRows - 1 'number of grid view rows...
            MyDataGridViewRow = MyDataGridView.Rows(i)
            MyArraylist = (PadAndWrapEachGridViewRow(MyDataGridViewRow, MyCharCountPerColumn, MyPadLeftArray, MyPadRightArray))

            For m = 0 To MyArraylist(0).Count - 1
                For k = 0 To NumColumns - 1
                    Try
                        OutputString = OutputString + MyArraylist(k).Item(m).ToString.Replace(vbCrLf, "")
                    Catch ex As Exception
                        Dim TempString = MyCharCountPerColumn(k).ToString
                        OutputString = OutputString + TempString.PadRight(MyCharCountPerColumn(k))
                    End Try

                    If k = NumColumns - 1 Then
                        OutputString = OutputString + vbCrLf
                    End If
                Next
            Next
        Next

        Return OutputString


    End Function

End Module
