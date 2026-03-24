
Imports System
Imports System.Windows.Forms.Screen

Public Module mResizeControls

    Public Class cScreenResolution
        Public DesignScreenSize As Rectangle
        Public CurrentScreenSize As Rectangle
        Public LastScreenSize As Rectangle
        Public WidthRatio As Double
        Public HeightRatio As Double
        Public InitComplete As Boolean = False
        Public RatioComputed As Boolean = False
        Public Sub init()
            Try
                DesignScreenSize.Height = 1080
                DesignScreenSize.Width = 1920
                LastScreenSize = DesignScreenSize
                CurrentScreenSize = Screen.PrimaryScreen.Bounds
                WidthRatio = CDbl(CurrentScreenSize.Width) / CDbl(DesignScreenSize.Width)
                HeightRatio = CDbl(CurrentScreenSize.Height) / CDbl(DesignScreenSize.Height)
                InitComplete = True
            Catch ex As Exception
                InitComplete = False
                MsgBox("Unable to Initialize Instant of cScreenResolution" + vbCrLf + ex.ToString)
            End Try

        End Sub

        Public Sub ComputeRatio(MyForm)
            If InitComplete = False Then
                init()
            End If
            If InitComplete = True Then
                Try
                    CurrentScreenSize = GetBounds(MyForm)
                    WidthRatio = CDbl(CurrentScreenSize.Width) / CDbl(LastScreenSize.Width)
                    HeightRatio = CDbl(CurrentScreenSize.Height) / CDbl(LastScreenSize.Height)
                    RatioComputed = True
                Catch ex As Exception
                    RatioComputed = False
                    MsgBox("Unable to Compute Ratio" + vbCrLf + ex.ToString)
                End Try

            End If


        End Sub
    End Class

    Public ScreenResolution As New cScreenResolution


    'Public Function ResizeForm(ByRef MyForm As Form) As Boolean
    '    ScreenResolution.ComputeRatio(MyForm)

    '    Return True
    'End Function
   
    Public Function ResizeChildControl(ByRef ChildControl As Control)
        For Each Control As Control In ChildControl.Controls
            If Control.HasChildren Then
                ResizeChildControl(Control)
            Else
                Control.Width = CInt(Control.Width * ScreenResolution.WidthRatio)
                Control.Height = CInt(Control.Height * ScreenResolution.HeightRatio)
                Control.Location = New Point(CInt(Control.Location.X * ScreenResolution.WidthRatio), CInt(Control.Location.Y * ScreenResolution.HeightRatio))
                Dim MyFont As New Font(Control.Font.OriginalFontName, Control.Font.Size * CSng(ScreenResolution.WidthRatio * ScreenResolution.HeightRatio), Control.Font.Style)
                Control.Font = MyFont
            End If
        Next
        Return True
    End Function


    Public Function ResizeControl(ByRef MyForm As Form) As Boolean
        If ScreenResolution.RatioComputed = False Then
            ScreenResolution.ComputeRatio(MyForm)
        End If

        For Each Control As Control In MyForm.Controls
            If Control.HasChildren Then
                ResizeChildControl(Control)
            Else
                Control.Width = CInt(Control.Width * ScreenResolution.WidthRatio)
                Control.Height = CInt(Control.Height * ScreenResolution.HeightRatio)
                Control.Location = New Point(CInt(Control.Location.X * ScreenResolution.WidthRatio), CInt(Control.Location.Y * ScreenResolution.HeightRatio))

                Dim MyFont As New Font(Control.Font.OriginalFontName, Control.Font.Size * CSng(ScreenResolution.WidthRatio * ScreenResolution.HeightRatio), Control.Font.Style)
                Control.Font = MyFont

            End If
        Next

        MyForm.Width = CInt(MyForm.Width * ScreenResolution.WidthRatio)
        MyForm.Height = CInt(MyForm.Height * ScreenResolution.HeightRatio)
        MyForm.Location = New Point(CInt(MyForm.Location.X * ScreenResolution.WidthRatio), CInt(MyForm.Location.Y * ScreenResolution.HeightRatio))
        Dim FormFont As New Font(MyForm.Font.OriginalFontName, MyForm.Font.Size * CSng(ScreenResolution.WidthRatio * ScreenResolution.HeightRatio), MyForm.Font.Style)
        MyForm.Font = FormFont
        ScreenResolution.LastScreenSize = ScreenResolution.CurrentScreenSize
        ScreenResolution.RatioComputed = False
        Return True
    End Function

End Module
