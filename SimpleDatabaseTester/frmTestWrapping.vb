Public Class frmTestWrapping

    Private Sub btnWrapTest_Click(sender As System.Object, e As System.EventArgs) Handles btnWrapTest.Click
        Dim MyStringArray() As String = mStringWrappingFunctions.PadandWrapString(txtInput.Text, txtWrapLength.Text, txtLeftPad.Text, txtRightPad.Text)
        Try
            txtOutput.Font = New Font("COURIER NEW", 10)
            txtOutput.Text = String.Join(vbCrLf, MyStringArray)
            txtOutput.Text = txtOutput.Text + vbCrLf + txtOutput.Text.ToString.Replace(" ", "*")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnClearOutput_Click(sender As System.Object, e As System.EventArgs) Handles btnClearOutput.Click
        txtOutput.Clear()
    End Sub
End Class