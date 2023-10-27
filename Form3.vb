Imports System.IO

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim passwd = "*Stelecom#"
        Dim ip = "192.168.1.1"
        ProgressBar1.Increment(1)
        Label1.Text = ProgressBar1.Value & (" %")
        System.Threading.Thread.Sleep(1000)
        Select Case Label1.Text
            Case 1 To 49
                Label2.Text = ("Aguarde enquanto o Roteador é atualizado.")
            Case 50 To 75
                Label2.Text = ("Aguarde enquanto o Equipamento é Reiniciado.")
            Case 76 To 80
                Label2.Text = ("Inicializando o Roteador.")
            Case 81 To 99
                Label2.Text = ("Finalizando Atualização.")
        End Select
        If ProgressBar1.Value = 100 Then
            Me.Close()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub
End Class