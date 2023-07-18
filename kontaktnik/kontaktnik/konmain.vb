Public Class konmain

    Private Sub konmain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim database As New IO.StreamWriter("data.txt")
        For i As Integer = 0 To ListView1.Items.Count - 1
            database.WriteLine(ListView1.Items(i).Text)
            database.WriteLine(ListView1.Items(i).SubItems(1).Text)
            database.WriteLine(ListView1.Items(i).SubItems(2).Text)
        Next
        database.Close()
    End Sub

    Private Sub konmain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim database As New IO.StreamReader("data.txt")
        Dim i As Integer
        While Not database.EndOfStream
            Dim jmeno As String = database.ReadLine()
            Dim mobil As String = database.ReadLine()
            Dim email As String = database.ReadLine()

            Dim polozka As New ListViewItem()
            polozka.Text = jmeno
            polozka.SubItems.Add(mobil)
            polozka.SubItems.Add(email)
            ListView1.Items.Add(polozka)
            ComboBox1.Items.Add(ListView1.Items(i).SubItems(0).Text)
            I += 1
        End While
        database.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Text = ListView1.Items(ComboBox1.SelectedIndex).SubItems(0).Text
        TextBox2.Text = ListView1.Items(ComboBox1.SelectedIndex).SubItems(1).Text
        TextBox3.Text = ListView1.Items(ComboBox1.SelectedIndex).SubItems(2).Text
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nv As New novy
        If nv.ShowDialog = DialogResult.OK Then
            Dim jmeno As String = nv.TextBox1.Text
            Dim mobil As String = nv.TextBox2.Text
            Dim email As String = nv.TextBox3.Text

            Dim polozka As New ListViewItem()
            polozka.Text = jmeno
            polozka.SubItems.Add(mobil)
            polozka.SubItems.Add(email)
            ListView1.Items.Add(polozka)
            ComboBox1.Items.Add(jmeno)

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.SelectedIndex = -1 Then Exit Sub
        Dim index As Integer = ComboBox1.SelectedIndex
        ListView1.Items.Remove(ListView1.Items(index))
        ComboBox1.Items.Remove(ComboBox1.SelectedItem)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

End Class