Public Class frmSplash



    Private Sub frmSplash_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Cursor = Cursors.Default
    End Sub

    Private Sub frmSplash_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
       
    End Sub


    Private Sub frmSplash_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Cursor = Cursors.WaitCursor
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub



    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblSplash.Visible = Not lblSplash.Visible
    End Sub
End Class