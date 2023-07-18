Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pole1(4) As Integer
        pole1(0) = CInt(TextBox1.Text)
        pole1(1) = CInt(TextBox2.Text)
        pole1(2) = CInt(TextBox3.Text)
        pole1(3) = CInt(TextBox4.Text)
        pole1(4) = CInt(TextBox5.Text)



        Dim pole2(4) As Integer

        For i As Integer = 0 To 4
            Dim nejvetsi As Integer = pole1(0)
            For j As Integer = 0 To 4
                If pole1(j) < nejvetsi Then nejvetsi = pole1(j)
            Next
            pole2(i) = nejvetsi

            For k As Integer = 0 To 4
                If pole1(k) = nejvetsi Then pole1(k) = 1000000
            Next
        Next

        TextBox1.Text = pole2(0)
        TextBox2.Text = pole2(1)
        TextBox3.Text = pole2(2)
        TextBox4.Text = pole2(3)
        TextBox5.Text = pole2(4)
    End Sub
End Class
