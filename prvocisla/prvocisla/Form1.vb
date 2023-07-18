Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
        Dim i As Integer = 2
        Dim vlozeno As Integer
        If Integer.TryParse(TextBox1.Text, vlozeno) = False Then
            MsgBox("Musí být vyplněno číslo!", MsgBoxStyle.Critical, "Chyba!")
            Exit Sub
        End If
        While ListBox1.Items.Count < vlozeno
            If JePrvocislo(i) Then ListBox1.Items.Add(i)
            i += 1
        End While


    End Sub

    Function JePrvocislo(ByVal i As Integer) As Boolean
        For a As Integer = 2 To i - 1
            If i Mod a = 0 Then Return False
        Next
        Return True
    End Function


End Class
