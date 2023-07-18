Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 1 To 20
            ComboBox1.Items.Add(i)
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListBox1.Items.Clear()
        For i = 1 To 10
            ListBox1.Items.Add(CInt(ComboBox1.SelectedItem) * i)

        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox2.Items.Clear()
        Dim vlozeno As Integer = CInt(TextBox1.Text)
        For i As Integer = vlozeno To 1
            If (vlozeno Mod i = 0) Then
                ListBox2.Items.Add(i)
            End If
        Next

    End Sub
End Class
