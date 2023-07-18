Public Class pcis_main
    Dim cisla As New System.Collections.Generic.List(Of Integer)
    Dim klikcant As Boolean = True
    Dim klikcants(4) As Boolean
    Dim cislak(4) As Integer
    Dim stat As New statistics_form
    Dim rozehrano As Boolean = False


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim x As New Label()
        rozehrano = True
        label1.Text = ""
        Label2.Text = ""
        Label3.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        klikcant = False
        For i As Integer = 0 To 4
            klikcants(i) = False
        Next
        Timer1.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If rozehrano Then
            MsgBox("Nejdříve musíte dohrát jednu hru, pak začít další!", MsgBoxStyle.Information, "Pamatuj čísla!")
            Exit Sub
        End If
        cisla.Clear()
        Timer1.Enabled = False
        Label6.ForeColor = Color.DarkGreen
        Label6.Text = ""
        TrackBar1.Enabled = False
        label1.BackColor = (Color.WhiteSmoke)
        Label2.BackColor = (Color.WhiteSmoke)
        Label3.BackColor = (Color.WhiteSmoke)
        Label4.BackColor = (Color.WhiteSmoke)
        Label5.BackColor = (Color.WhiteSmoke)
        Dim nahodne_cislo As Integer
        Dim rnd As New Random
        nahodne_cislo = rnd.Next(1, 100)
        For i As Integer = 0 To 4

            While cisla.Contains(nahodne_cislo)
                nahodne_cislo = rnd.Next(1, 100)
            End While
            cisla.Add(nahodne_cislo)
            cislak(i) = nahodne_cislo
        Next

        label1.Text = cisla.Item(0)
        Label2.Text = cisla.Item(1)
        Label3.Text = cisla.Item(2)
        Label4.Text = cisla.Item(3)
        Label5.Text = cisla.Item(4)
        Timer1.Enabled = True
    End Sub

    Private Sub label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label1.Click
        If klikcant Then Exit Sub
        If klikcants(0) Then Exit Sub
        klikcants(0) = True
        Dim nejmensi As Integer = 250
        For i As Integer = 0 To 4
            If cisla.Item(i) < nejmensi Then nejmensi = cisla.Item(i)
        Next
        If cisla.Item(0) = nejmensi Then
            label1.Text = cisla.Item(0)
            cisla.Item(0) = 200
        Else
            label1.BackColor = Color.IndianRed
            prohra()
        End If
        If cisla.Item(0) = 200 And cisla.Item(1) = 200 And cisla.Item(2) = 200 And cisla.Item(3) = 200 And cisla.Item(4) = 200 Then
            vyhra()
        End If
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        If klikcant Then Exit Sub
        If klikcants(1) Then Exit Sub
        klikcants(1) = True
        Dim nejmensi As Integer = 250
        For i As Integer = 0 To 4
            If cisla.Item(i) < nejmensi Then nejmensi = cisla.Item(i)
        Next
        If cisla.Item(1) = nejmensi Then
            Label2.Text = cisla.Item(1)
            cisla.Item(1) = 200
        Else
            Label2.BackColor = Color.IndianRed
            prohra()
        End If
        If cisla.Item(0) = 200 And cisla.Item(1) = 200 And cisla.Item(2) = 200 And cisla.Item(3) = 200 And cisla.Item(4) = 200 Then
            vyhra()
        End If
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        If klikcant Then Exit Sub
        If klikcants(2) Then Exit Sub
        klikcants(2) = True
        Dim nejmensi As Integer = 250
        For i As Integer = 0 To 4
            If cisla.Item(i) < nejmensi Then nejmensi = cisla.Item(i)
        Next
        If cisla.Item(2) = nejmensi Then
            Label3.Text = cisla.Item(2)
            cisla.Item(2) = 200
        Else
            Label3.BackColor = Color.IndianRed
            prohra()
        End If
        If cisla.Item(0) = 200 And cisla.Item(1) = 200 And cisla.Item(2) = 200 And cisla.Item(3) = 200 And cisla.Item(4) = 200 Then
            vyhra()
        End If
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        If klikcant Then Exit Sub
        If klikcants(3) Then Exit Sub
        klikcants(3) = True
        Dim nejmensi As Integer = 250
        For i As Integer = 0 To 4
            If cisla.Item(i) < nejmensi Then nejmensi = cisla.Item(i)
        Next
        If cisla.Item(3) = nejmensi Then
            Label4.Text = cisla.Item(3)
            cisla.Item(3) = 200
        Else
            Label4.BackColor = Color.IndianRed
            prohra()
        End If
        If cisla.Item(0) = 200 And cisla.Item(1) = 200 And cisla.Item(2) = 200 And cisla.Item(3) = 200 And cisla.Item(4) = 200 Then
            vyhra()
        End If
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        If klikcant Then Exit Sub
        If klikcants(4) Then Exit Sub
        klikcants(4) = True
        Dim nejmensi As Integer = 250
        For i As Integer = 0 To 4
            If cisla.Item(i) < nejmensi Then nejmensi = cisla.Item(i)
        Next
        If cisla.Item(4) = nejmensi Then
            Label5.Text = cisla.Item(4)
            cisla.Item(4) = 200
        Else
            Label5.BackColor = Color.IndianRed
            prohra()
        End If
        If cisla.Item(0) = 200 And cisla.Item(1) = 200 And cisla.Item(2) = 200 And cisla.Item(3) = 200 And cisla.Item(4) = 200 Then
            vyhra()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox("Po stisknutí tlačítka nová hra se do rámečků vypíší náhodná čísla od 0 do 100 a za tři sekundy zmizí. Vaším úkolem je klikat na rámečky v pořadí jak v nich šla čísla od nejmenšího po největší. Při správně kliknutém rámečku se v něm odhalí číslo které tam bylo.", MsgBoxStyle.Information, "Návod")
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Select Case TrackBar1.Value
            Case 0
                Timer1.Interval = 5000
            Case 1
                Timer1.Interval = 4000
            Case 2
                Timer1.Interval = 3000
            Case 3
                Timer1.Interval = 2500
            Case 4
                Timer1.Interval = 1500
        End Select
    End Sub

    Private Sub vyhra()
        Label6.Text = "Vyhrál jsi!"
        klikcant = True
        Select Case TrackBar1.Value
            Case 0
                stat.hry(0) += 1
                stat.hry(1) += 1
            Case 1
                stat.hry(3) += 1
                stat.hry(4) += 1
            Case 2
                stat.hry(6) += 1
                stat.hry(7) += 1
            Case 3
                stat.hry(9) += 1
                stat.hry(10) += 1
            Case 4
                stat.hry(12) += 1
                stat.hry(13) += 1
        End Select
        TrackBar1.Enabled = True
        rozehrano = False
    End Sub

    Private Sub prohra()
        Label6.ForeColor = Color.DarkRed
        Label6.Text = "Prohrál jsi!"
        klikcant = True
        Label2.Text = cislak(1)
        label1.Text = cislak(0)
        Label3.Text = cislak(2)
        Label4.Text = cislak(3)
        Label5.Text = cislak(4)
        Select Case TrackBar1.Value
            Case 0
                stat.hry(0) += 1
                stat.hry(2) += 1
            Case 1
                stat.hry(3) += 1
                stat.hry(5) += 1
            Case 2
                stat.hry(6) += 1
                stat.hry(8) += 1
            Case 3
                stat.hry(9) += 1
                stat.hry(11) += 1
            Case 4
                stat.hry(12) += 1
                stat.hry(14) += 1
        End Select
        TrackBar1.Enabled = True
        rozehrano = False
    End Sub

    Private Sub pcis_main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim statW As New IO.StreamWriter("statistics_data.txt")
        For i As Integer = 0 To 14
            statW.WriteLine(stat.hry(i))
        Next
        statW.Close()
    End Sub

    Private Sub pcis_main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim statR As New IO.StreamReader("statistics_data.txt")
        For i As Integer = 0 To 14
            stat.hry(i) = statR.ReadLine()
        Next
        statR.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        stat.ShowDialog()
    End Sub
End Class
