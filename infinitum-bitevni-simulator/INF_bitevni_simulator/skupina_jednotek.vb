Public Class skupina_jednotek
    Public nazev As String
    Public PZM As Double
    Public PZMcelk As Double
    Public odolnostcelk As Double
    Public odolnost As Double
    Public zbroj_p As Byte
    Public zbroj_l As Byte
    Public zbroj_e As Byte
    Public zbroj_v As Byte
    Public typ As Byte
    Public pocet_jednotek As Double
    Public pocet_jednotekpo As Double
    Private data As String

    Sub New(ByVal jmeno, ByVal jednotka, ByVal pocetjednotek)
        Dim rd As New IO.StreamReader("jednotky_data.txt")
        For i As Integer = 0 To jednotka
            data = rd.ReadLine()
        Next
        nazev = jmeno
        PZM = CDbl(data.Substring(0, 8))
        ' MsgBox(PZM)
        odolnost = CDbl(data.Substring(9, 8))
        'MsgBox(odolnost)
        zbroj_p = CByte(data.Substring(18, 2))
        zbroj_l = CByte(data.Substring(21, 2))
        zbroj_e = CByte(data.Substring(24, 2))
        zbroj_v = CByte(data.Substring(27, 2))
        typ = CByte(data.Substring(30, 1))
        pocet_jednotek = pocetjednotek
        PZMcelk = pocet_jednotek * PZM
        pocet_jednotekpo = pocet_jednotek
        odolnostcelk = pocet_jednotek * odolnost
    End Sub

    Public Function zasah(ByVal PZMget As Double, ByVal typget As Integer)
        Select Case typget
            Case 1
                odolnostcelk = odolnostcelk - PZMget / zbroj_p
                pocet_jednotekpo = odolnostcelk / odolnost
                Return PZMget / zbroj_p
            Case 2
                odolnostcelk = odolnostcelk - PZMget / zbroj_l
                pocet_jednotekpo = odolnostcelk / odolnost
                Return PZMget / zbroj_l
            Case 3
                odolnostcelk = odolnostcelk - PZMget / zbroj_e
                pocet_jednotekpo = odolnostcelk / odolnost
                Return PZMget / zbroj_e
            Case 4
                odolnostcelk = odolnostcelk - PZMget / zbroj_v
                pocet_jednotekpo = odolnostcelk / odolnost
                Return PZMget / zbroj_v
        End Select
    End Function
End Class
