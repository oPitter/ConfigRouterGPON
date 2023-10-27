Public Class Form1
    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Visible = True
        Dim procInfo As New ProcessStartInfo()
        procInfo.UseShellExecute = True
        procInfo.FileName = "lan.bat"
        procInfo.WorkingDirectory = Application.StartupPath
        procInfo.Verb = "runas"
        Process.Start(procInfo)
        ProgressBar1.Increment(1)
        TextBox2.Visible = True
        Shell("tftp 192.168.0.66")
        ProgressBar1.Increment(2)
        TextBox5.Visible = True
        MsgBox("Coloque o roteador em TFTP, em seguida precione OK.", vbOKOnly)
        ProgressBar1.Increment(3)
        TextBox4.Visible = True
        Timer1.Start()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(1)
        If ProgressBar1.Value = 95 Then
            Timer1.Stop()
            Dim procInfo As New ProcessStartInfo()
            procInfo.UseShellExecute = True
            procInfo.FileName = "dhcp.bat"
            procInfo.WorkingDirectory = Application.StartupPath
            procInfo.Verb = "runas"
            Process.Start(procInfo)
            System.Threading.Thread.Sleep(1000)
            Dim test As Integer
            test = 1
            Do While test = 1
                If My.Computer.Network.Ping("192.168.01.01") Then
                    ProgressBar1.Value = 100
                    TextBox3.Visible = True
                    Button1.Enabled = False
                    Button2.Visible = True
                    test = 0
                End If
            Loop
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class