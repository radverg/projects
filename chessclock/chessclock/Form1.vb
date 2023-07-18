Public Class Form1
    Dim minutesb As Integer = 30
    Dim minutesc As Integer = 30
    Dim secondsb As Double = 10
    Dim secondsc As Double = 10
    Dim nopaused As Boolean = False
    Dim turn As Integer = 1
    Dim firststart As Boolean = False
    Dim endgame As Boolean = True
    Dim secondsb2 As Integer
    Dim secondsc2 As Integer
    Dim minutesb2 As Integer = minutesb
    Dim minutesc2 As Integer = minutesc
    Dim moves As Double = 0
    Dim blick As Boolean = True

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If minutesb = 0 And secondsb = 0 Then
            Timer1.Enabled = False : Timer3.Enabled = True
            Button1.Enabled = False : Button3.Enabled = False
            secondsb = 1
            firststart = False
            Label4.Text = "Černý je vítěz!"
            Label7.Text = "Bílý prohrál."
        End If
        If secondsb <= 0 Then
            secondsb = 60
            minutesb -= 1
            If minutesb < 10 Then
                minutesbl.Text = 0 & minutesb
            Else
                minutesbl.Text = minutesb
            End If
        End If
        secondsb -= 0.1
        If secondsb < 10 Then
            secondsbl.Text = 0 & Math.Ceiling(secondsb)
        Else
            secondsbl.Text = Math.Ceiling(secondsb)
        End If


    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If minutesc = 0 And secondsc = 0 Then
            Timer2.Enabled = False : Timer3.Enabled = True
            Button3.Enabled = False : Button1.Enabled = False
            secondsc = 1
            firststart = False
            Label4.Text = "Bílý je vítěz!"
            Label7.Text = "Černý prohrál."
        End If
        If secondsc <= 0 Then
            secondsc = 60
            minutesc -= 1
            If minutesc < 10 Then
                minutescl.Text = 0 & minutesc
            Else
                minutescl.Text = minutesc
            End If
        End If
        secondsc -= 0.1
        If secondsc < 10 Then
            secondscl.Text = 0 & Math.Ceiling(secondsc)
        Else
            secondscl.Text = Math.Ceiling(secondsc)
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        endgame = False
        If turn = 1 Then
            If blick Then
                Label1.BackColor = Color.Red : minutesbl.BackColor = Color.Red
                secondsbl.BackColor = Color.Red : Label5.BackColor = Color.Red
                blick = False
            Else
                Label1.BackColor = Color.Gainsboro : minutesbl.BackColor = Color.Gainsboro
                secondsbl.BackColor = Color.Gainsboro : Label5.BackColor = Color.Gainsboro
                blick = True
            End If
        End If
        If turn = 2 Then
            If blick Then
                Label2.BackColor = Color.Red : minutescl.BackColor = Color.Red
                secondscl.BackColor = Color.Red : Label6.BackColor = Color.Red
                blick = False
            Else
                Label2.BackColor = Color.Gainsboro : minutescl.BackColor = Color.Gainsboro
                secondscl.BackColor = Color.Gainsboro : Label6.BackColor = Color.Gainsboro
                blick = True
            End If
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        prohodit()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        If firststart Then
            If MsgBox("Opravdu začít novou hru?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Nová hra") = MsgBoxResult.No Then
                If turn = 1 Then Timer1.Enabled = True
                If turn = 2 Then Timer2.Enabled = True
                Exit Sub
            End If
        End If
        moves = 0
        Label12.Text = moves.ToString()
        turn = 1 : endgame = True : firststart = True
        newset()
        minutesbl.ForeColor = Color.Blue : secondsbl.ForeColor = Color.Blue
        Label5.ForeColor = Color.Blue : Label9.ForeColor = Color.Blue
        Button1.Enabled = True : Button3.Enabled = True : Button5.Enabled = True : Button4.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If nopaused Then
            Label3.Text = ""
            Button3.Text = "Pozastavit hru"
            If turn = 1 Then Timer1.Enabled = True
            If turn = 2 Then Timer2.Enabled = True
            nopaused = False
            Exit Sub
        End If
        Timer1.Enabled = False
        Timer2.Enabled = False
        nopaused = True
        Label3.Text = "Hra pozastavena"
        Button3.Text = "Pokračovat ve hře"
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Timer1.Enabled = False
        Timer2.Enabled = False
        If endgame Then
            If MsgBox("Opravdu ukončit hru?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Konec hry") = MsgBoxResult.No Then
                If turn = 1 Then Timer1.Enabled = True
                If turn = 2 Then Timer2.Enabled = True
                Exit Sub
            End If
        End If
        newset()
        firststart = False
        Button1.Enabled = False : Button3.Enabled = False : Button5.Enabled = False : Button4.Enabled = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim sett As New settings
        sett.NumericUpDown1.Value = minutesb2
        sett.NumericUpDown2.Value = minutesc2
        sett.NumericUpDown4.Value = secondsb2
        sett.NumericUpDown3.Value = secondsc2
        If sett.ShowDialog = DialogResult.OK Then
            minutesb2 = sett.NumericUpDown1.Value
            minutesc2 = sett.NumericUpDown2.Value
            secondsb2 = sett.NumericUpDown4.Value
            secondsc2 = sett.NumericUpDown3.Value
        End If
        newset()
    End Sub

    Private Sub newset()
        minutescl.ForeColor = Color.Black : secondscl.ForeColor = Color.Black
        Label6.ForeColor = Color.Black : Label10.ForeColor = Color.Black
        minutesbl.ForeColor = Color.Black : secondsbl.ForeColor = Color.Black
        Label5.ForeColor = Color.Black : Label9.ForeColor = Color.Black
        Label1.BackColor = Color.Gainsboro : minutesbl.BackColor = Color.Gainsboro
        secondsbl.BackColor = Color.Gainsboro : Label5.BackColor = Color.Gainsboro
        Label2.BackColor = Color.Gainsboro : minutescl.BackColor = Color.Gainsboro
        secondscl.BackColor = Color.Gainsboro : Label6.BackColor = Color.Gainsboro
        Timer3.Enabled = False
        Label4.Text = "" : Label7.Text = "" : Label3.Text = "" : Button3.Text = "Pozastavit hru"
        minutesb = minutesb2
        minutesc = minutesc2
        secondsb = secondsb2
        secondsc = secondsc2
        secondsbl.Text = "00"
        secondscl.Text = "00"
        If minutesb2 < 10 Then
            minutesbl.Text = 0 & minutesb2
        Else
            minutesbl.Text = minutesb2
        End If

        If minutesc2 < 10 Then
            minutescl.Text = 0 & minutesc2
        Else
            minutescl.Text = minutesc2
        End If

        If secondsb2 < 10 Then
            secondsbl.Text = 0 & secondsb2
        Else
            secondsbl.Text = secondsb2
        End If

        If secondsc2 < 10 Then
            secondscl.Text = 0 & secondsc2
        Else
            secondscl.Text = secondsc2
        End If
    End Sub

    Private Sub prohodit()
        If Timer1.Enabled = True Then
            turn = 2
            Timer1.Enabled = False
            Timer2.Enabled = True
            minutescl.ForeColor = Color.Blue : secondscl.ForeColor = Color.Blue
            Label6.ForeColor = Color.Blue : Label10.ForeColor = Color.Blue
            minutesbl.ForeColor = Color.Black : secondsbl.ForeColor = Color.Black
            Label5.ForeColor = Color.Black : Label9.ForeColor = Color.Black
            moves = moves + 0.5
            Label12.Text = Math.Floor(moves).ToString()
            Exit Sub
        End If
        If Timer2.Enabled = True Then
            turn = 1
            Timer2.Enabled = False
            Timer1.Enabled = True
            minutescl.ForeColor = Color.Black : secondscl.ForeColor = Color.Black
            Label6.ForeColor = Color.Black : Label10.ForeColor = Color.Black
            minutesbl.ForeColor = Color.Blue : secondsbl.ForeColor = Color.Blue
            Label5.ForeColor = Color.Blue : Label9.ForeColor = Color.Blue
            moves = moves + 0.5
            Label12.Text = Math.Floor(moves).ToString()
            Exit Sub
        End If
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        prohodit()
    End Sub
End Class