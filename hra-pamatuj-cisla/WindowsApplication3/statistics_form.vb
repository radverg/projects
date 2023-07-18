Public Class statistics_form
    Public hry(14) As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i As Integer = 0 To 10
            Me.Close()
        Next
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Select Case ListBox1.SelectedIndex
            Case 0
                Label6.Text = hry(0)
                Label7.Text = hry(1)
                Label8.Text = hry(2)
            Case 1
                Label6.Text = hry(3)
                Label7.Text = hry(4)
                Label8.Text = hry(5)
            Case 2
                Label6.Text = hry(6)
                Label7.Text = hry(7)
                Label8.Text = hry(8)
            Case 3
                Label6.Text = hry(9)
                Label7.Text = hry(10)
                Label8.Text = hry(11)
            Case 4
                Label6.Text = hry(12)
                Label7.Text = hry(13)
                Label8.Text = hry(14)
        End Select
    End Sub

    Private Sub statistics_form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBoxResult.Yes = MsgBox("Vynulování je nenávratné! Opravdu vynulovat všechny výsledky?", MsgBoxStyle.YesNo, "Varování!") Then
            For i As Integer = 0 To 14
                hry(i) = 0
                Label6.Text = "0"
                Label7.Text = "0"
                Label8.Text = "0"
            Next
        End If
        

    End Sub
End Class