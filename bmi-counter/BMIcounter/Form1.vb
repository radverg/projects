Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim vaha As Double = CDbl(TextBox1.Text)
        Dim vyska1 As Double = CDbl(TextBox2.Text)
        Dim vyska2 As Double = vyska1 / 100
        Dim BMI As Double = vaha / (vyska2 * vyska2)
        If (BMI < 18.5) Then TextBox3.Text = "Máte podváhu!"
        If (BMI > 18.5 And BMI < 24.9) Then TextBox3.Text = "Máte optimální váhu!"
        If (BMI > 24.5 And BMI < 29.9) Then TextBox3.Text = "Máte nadváhu!"
        If (BMI > 29.9 And BMI < 34.9) Then TextBox3.Text = "Máte obezitu prvního stupně!"
        If (BMI > 34.9 And BMI < 39.9) Then TextBox3.Text = "Máte obezitu druhého stupně!"
        If (BMI > 39.9) Then TextBox3.Text = "Máte obezitu třetího stupně!"

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

End Class
