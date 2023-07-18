Public Class Form1

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As Integer = CInt(TextBox1.Text)
        Dim b As Integer = CInt(TextBox2.Text)
        Dim c As Integer = a - b
        TextBox3.Text = CStr(c)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim a As Integer = CInt(TextBox1.Text)
        Dim b As Integer = CInt(TextBox2.Text)
        Dim c As Integer = a * b
        TextBox3.Text = CStr(c)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As Integer = CInt(TextBox1.Text)
        Dim b As Integer = CInt(TextBox2.Text)
        Dim c As Integer = a + b
        TextBox3.Text = CStr(c)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim a As Integer = CInt(TextBox1.Text)
        Dim b As Integer = CInt(TextBox2.Text)
        Dim c As Integer = a / b
        TextBox3.Text = CStr(c)
    End Sub
End Class
