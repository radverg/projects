Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim zadano As Double
        If Double.TryParse(TextBox1.Text, zadano) = False Then
            MsgBox("Musíš zadat číslo", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim a As Integer = CInt(zadano)
        If a = zadano Then
            TextBox2.Text = "Číslo je celé!"
        Else
            TextBox2.Text = "Číslo je desetinné!"
        End If

    End Sub
End Class
