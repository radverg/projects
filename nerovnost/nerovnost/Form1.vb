Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As Integer = CInt(TextBox1.Text)
        Dim b As Integer = CInt(TextBox2.Text)
        Dim c As Integer = CInt(TextBox3.Text)
        If (a < b + c And b < a + c And c < a + b) Then Label2.Text = "Trojúhelník existuje" Else Label2.Text = "Trojúhelník neexistuje"

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class
