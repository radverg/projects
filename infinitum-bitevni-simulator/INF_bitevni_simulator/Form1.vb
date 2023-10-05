Public Class Form1
    Dim h1 As skupina_jednotek
    Dim h2 As skupina_jednotek
    Dim prubeh_smycky As Boolean = True
    Dim smycka As Boolean
    Dim strela As Integer = 1
    Dim vysl As New vysledek
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nutno As Integer
        If ComboBox2.SelectedIndex = -1 Or ComboBox1.SelectedIndex = -1 Then MsgBox("Nejsou vyplněny všechny typy jednotek!", MsgBoxStyle.Exclamation) : Exit Sub
        If TextBox3.Text = "" Or TextBox4.Text = "" Then MsgBox("Nejsou vyplněny všechny počty jednotek!", MsgBoxStyle.Exclamation) : Exit Sub
        If CheckBox1.Checked = False And CheckBox2.Checked = False Then MsgBox("Alespoň jedna armáda musí střílet!", MsgBoxStyle.Exclamation) : Exit Sub
        If Integer.TryParse(TextBox3.Text, nutno) = False Or Integer.TryParse(TextBox4.Text, nutno) = False Then MsgBox("Počet jednotek musí bý vyjádřen číslem a číslo musí mít rozumnou velikost!", MsgBoxStyle.Exclamation) : Exit Sub
        ListView1.Items.Clear()
        Button2.Enabled = False
        strela = 1
        Dim hrac1 As New skupina_jednotek(ComboBox1.SelectedItem, ComboBox1.SelectedIndex, CInt(TextBox3.Text))
        Dim hrac2 As New skupina_jednotek(ComboBox2.SelectedItem, ComboBox2.SelectedIndex, CInt(TextBox4.Text))
        h1 = hrac1
        h2 = hrac2

        Label1.Text = "Probíhá boj..."
        If smycka = True Then
            smyckaa()
        Else
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        simulace()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        smycka = False
        Select Case ComboBox3.SelectedIndex
            Case 0
                Timer1.Interval = 10000
            Case 1
                Timer1.Interval = 10000 / 2
            Case 2
                Timer1.Interval = 10000 / 4
            Case 3
                Timer1.Interval = 10000 / 5
            Case 4
                Timer1.Interval = 10000 / 10
            Case 5
                Timer1.Interval = 10000 / 20
            Case 6
                Timer1.Interval = 10000 / 30
            Case 7
                Timer1.Interval = 10000 / 50
            Case 8
                Timer1.Interval = 10000 / 100
            Case 9
                smycka = True
        End Select
    End Sub
    Private Sub smyckaa()
        prubeh_smycky = True
        While prubeh_smycky
            simulace()
        End While
    End Sub

    Private Sub simulace()
        Dim gettedPZM1 As Double
        Dim gettedPZM2 As Double
        Dim polozka As New ListViewItem
        polozka.Text = strela
        polozka.SubItems.Add("1")
        polozka.SubItems.Add(h1.nazev)
        polozka.SubItems.Add(CInt(h1.pocet_jednotekpo))
        If CheckBox2.Checked = True Then
            gettedPZM1 = h1.zasah(h2.PZMcelk / 6, h2.typ)
        Else
            gettedPZM1 = 0
        End If
        polozka.SubItems.Add(CInt(h1.pocet_jednotekpo))
        If h1.pocet_jednotekpo < 0 Then polozka.SubItems(4).Text = 0
        polozka.SubItems.Add(CInt(polozka.SubItems(3).Text - polozka.SubItems(4).Text))
        If gettedPZM1 < 10 And gettedPZM1 > 0.01 Then
            polozka.SubItems.Add(CInt(gettedPZM1 * 1000) & "k")
        ElseIf gettedPZM1 < 0.01 Then
            polozka.SubItems.Add(CInt(gettedPZM1 * 1000000))
        Else
            polozka.SubItems.Add(CInt(gettedPZM1) & "m")
        End If

        If h1.pocet_jednotekpo <= 0 Then Timer1.Enabled = False : Label1.Text = "Boj byl dokončen" : prubeh_smycky = False : vysledky("Hráč2")
        ListView1.Items.Add(polozka)

        Dim polozka2 As New ListViewItem
        polozka2.Text = strela
        polozka2.SubItems.Add("2")
        polozka2.SubItems.Add(h2.nazev)
        polozka2.SubItems.Add(CInt(h2.pocet_jednotekpo))
        If CheckBox1.Checked = True Then
            gettedPZM2 = h2.zasah(h1.PZMcelk / 6, h1.typ)
        Else
            gettedPZM2 = 0
        End If
        polozka2.SubItems.Add(CInt(h2.pocet_jednotekpo))
        If h2.pocet_jednotekpo < 0 Then polozka2.SubItems(4).Text = 0
        polozka2.SubItems.Add(CInt(polozka2.SubItems(3).Text - polozka2.SubItems(4).Text))

        If gettedPZM2 < 10 And gettedPZM2 > 0.01 Then
            polozka2.SubItems.Add(CInt(gettedPZM2 * 1000) & "k")
        ElseIf gettedPZM2 < 0.01 Then
            polozka2.SubItems.Add(CInt(gettedPZM2 * 1000000))
        Else
            polozka2.SubItems.Add(CInt(gettedPZM2) & "m")
        End If
        If h2.pocet_jednotekpo <= 0 Then Timer1.Enabled = False : Label1.Text = "Boj byl dokončen" : prubeh_smycky = False : vysledky("Hráč1")
        If polozka2.SubItems(3).Text = 0 And polozka.SubItems(3).Text = 0 Then Timer1.Enabled = False : Label1.Text = "Boj byl dokončen" : prubeh_smycky = False : vysledky("Nerozhodně")
        ListView1.Items.Add(polozka2)

        h1.PZMcelk = h1.PZM * h1.pocet_jednotekpo
        h2.PZMcelk = h2.PZM * h2.pocet_jednotekpo
        strela += 1
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBox3.SelectedIndex = 9
        ComboBox2.SelectedIndex = 5
        ComboBox1.SelectedIndex = 5
    End Sub

    Public Sub vysledky(ByVal vytez)
        Dim celkcas As Double
        Dim celkcasmin As Double
        Dim celkcassec As Integer
        celkcas = strela * 10
        '   MsgBox(strela)
        '   MsgBox(celkcas)

        celkcasmin = celkcas / 60
        If celkcasmin < CInt(celkcasmin) Then
            celkcasmin = CInt(celkcasmin - 1)
        Else
            celkcasmin = CInt(celkcas / 60)
        End If
        celkcassec = celkcas - (celkcasmin * 60)
        Label13.Text = "Celkový čas boje: " & celkcasmin & "m " & celkcassec & "s"
        Label3.Text = "Vítěz: " & vytez
        vysl.Label1.Text = "Hráč1 - " & h1.nazev
        vysl.Label22.Text = "Hráč2 - " & h2.nazev
        vysl.Label3.Text = "Vítěz: " & vytez
        vysl.Label4.Text = "Celkový čas boje: " & celkcasmin & "m " & celkcassec & "s"
        vysl.Label5.Text = "Jednotka: " & h1.nazev & " - " & h1.pocet_jednotek
        vysl.Label20.Text = "Jednotka: " & h2.nazev & " - " & h2.pocet_jednotek
        vysl.Label6.Text = "PZM: " & CInt(h1.PZM * h1.pocet_jednotek) & "m"
        vysl.Label19.Text = "PZM: " & CInt(h2.PZM * h2.pocet_jednotek) & "k"
        vysl.Label7.Text = "Odolnost: " & CInt(h1.odolnost * h1.pocet_jednotek * 1000) & "k"
        vysl.Label18.Text = "Odolnost: " & CInt(h2.odolnost * h2.pocet_jednotek * 1000) & "k"
        If CInt(h1.pocet_jednotekpo) < 0 Then
            vysl.Label8.Text = "Zabito: " & h1.pocet_jednotek
        Else
            vysl.Label8.Text = "Zabito: " & h1.pocet_jednotek - CInt(h1.pocet_jednotekpo)
        End If
        If CInt(h2.pocet_jednotekpo) < 0 Then
            vysl.Label17.Text = "Zabito: " & h2.pocet_jednotek
        Else
            vysl.Label17.Text = "Zabito: " & h2.pocet_jednotek - CInt(h2.pocet_jednotekpo)
        End If
        vysl.Label9.Text = "Ztraceno životů: " & CInt((h1.pocet_jednotek - h1.pocet_jednotekpo) * h1.odolnost * 1000) & "k"
        vysl.Label16.Text = "Ztraceno životů: " & CInt((h2.pocet_jednotek - h2.pocet_jednotekpo) * h2.odolnost * 1000) & "k"
        If h1.pocet_jednotekpo < 0 Then
            vysl.Label10.Text = "Zbylo: 0"
        Else
            vysl.Label10.Text = "Zbylo: " & CInt(h1.pocet_jednotekpo)
        End If
        If h2.pocet_jednotekpo < 0 Then
            vysl.Label15.Text = "Zbylo: 0"
        Else
            vysl.Label15.Text = "Zbylo: " & CInt(h2.pocet_jednotekpo)
        End If
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        vysl.ShowDialog()
    End Sub
End Class
