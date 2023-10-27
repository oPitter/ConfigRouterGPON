Option Compare Binary
Option Explicit On
Option Strict On

Imports System.IO

''' <summary>
'''   Hauptformular des Programms.
''' </summary>


Public Class mainSThh
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            TextBox2.Enabled = True
            TextBox7.Enabled = True
        Else
            TextBox2.Enabled = False
            TextBox7.Enabled = False
        End If
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Repo = "http://10.44.44.2/openwrt/st/"
        Dim passwd = "*Stelecom#"
        Dim empresa = "Soultelecom-"
        Dim senhaPPP = "wda.@23"
        Dim ip = "192.168.1.1"
        Dim run = 1
        Dim check = 0
        While run = 1

            'Verificação da Marca'

            ComboBox1.Enabled = False
            If ComboBox1.Text = "" Then
                MsgBox("Selecione um Fabricante!")
                run = 0
            Else
                check = check + 1
            End If

            'Verificação do Modelo'

            ComboBox2.Enabled = False
            If ComboBox2.Text = "" And check = 1 Then
                MsgBox("Selecione um Modelo!")
                run = 0
            Else
                check = check + 1
            End If

            'Verificação do PPPoE'

            TextBox2.Enabled = False
            TextBox7.Enabled = False
            If TextBox2.Text = "" And CheckBox3.Checked And check = 2 Then
                MsgBox("Digite o usuário PPPoE do Cliente!")
                run = 0
            Else
                check = check + 1
            End If
            CheckBox3.Enabled = False

            'Verificação do WIFI2G'

            TextBox3.Enabled = False
            TextBox4.Enabled = False
            If TextBox3.Text = "" And TextBox4.Text = "" And CheckBox7.Checked And check = 3 Then
                MsgBox("Verifique o Nome da Rede WI-FI 2G ou a Senha!")
                run = 0
            Else
                check = check + 1
            End If
            CheckBox7.Enabled = False

            'Verificação do WIFI5G'

            TextBox1.Enabled = False
            TextBox5.Enabled = False
            If TextBox1.Text = "" And TextBox5.Text = "" And CheckBox6.Checked And check = 4 Then
                MsgBox("Verifique o Nome da Rede WI-FI 5G ou a Senha!")
                run = 0
            Else
                check = check + 1
            End If
            CheckBox6.Enabled = False

            'Verificação da VLAN'

            TextBox6.Enabled = False
            ComboBox4.Enabled = False
            If TextBox6.Text = "" And ComboBox4.Text = "" And CheckBox2.Checked And check = 5 Then
                MsgBox("Verifique a VLAN e VLAN Priority")
                run = 0
            Else
                check = check + 1
            End If
            CheckBox2.Enabled = False

            'Verificação da Ethernet'

            ComboBox3.Enabled = False
            If ComboBox3.Text = "" And CheckBox4.Checked And check = 6 Then
                MsgBox("Verifique a Quantidade de Usuários")
                run = 0
            Else
                check = check + 1
            End If
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False

            'pscp.exe -scp root@192.168.1.1:/www/teste d:'

            If CheckBox1.Checked Then

            Else

            End If

            If run <> 0 Then
                If CheckBox2.Checked Then
                    If My.Computer.Network.Ping(ip) Then
                        MsgBox("Roteador Identificado")
                        ' Configura VLAN
                        Dim lines() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.X_CT-COM_WANGponLinkConfig.Mode 2 \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.X_CT-COM_WANGponLinkConfig.VLANIDMark " + TextBox6.Text + "  \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.X_CT-COM_WANGponLinkConfig.P_802-1pMark " + ComboBox4.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.WANPPPConnection.1.X_CT-COM_LanInterface InternetGatewayDevice.LANDevice.1.LANEthernetInterfaceConfig.4,InternetGatewayDevice.LANDevice.1.LANEthernetInterfaceConfig.3,InternetGatewayDevice.LANDevice.1.LANEthernetInterfaceConfig.2,InternetGatewayDevice.LANDevice.1.LANEthernetInterfaceConfig.1,InternetGatewayDevice.LANDevice.1.WLANConfiguration.1,InternetGatewayDevice.LANDevice.1.WLANConfiguration.5 \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                        ' Salva as informações no arquivo script.txt".
                        Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                            For Each line As String In lines
                                outputFile.WriteLine(line)
                            Next
                        End Using
                        Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                        Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                    Else
                        MsgBox("Roteador Não Respondeu!")
                        Exit While
                    End If
                Else

                End If
                If CheckBox3.Checked Then
                    ' CONFIGURA PPPOE
                    Dim linesss() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.WANPPPConnection.1.Password " + TextBox7.Text + "  \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.WANPPPConnection.1.Username " + TextBox2.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.WANDevice.1.WANConnectionDevice.1.WANPPPConnection.1.Username " + TextBox2.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In linesss
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                Else

                End If
                If CheckBox7.Checked Then
                    ' CONFIGURA WIFI 2G
                    Dim linessss() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.LANDevice.1.WLANConfiguration.1.SSID SoulTelecom.2G-" + TextBox3.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.LANDevice.1.WLANConfiguration.1.PreSharedKey.1.PreSharedKey " + TextBox4.Text + "  \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In linessss
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                Else

                End If
                If CheckBox6.Checked Then
                    ' CONFIGURA WIFI 5G
                    Dim linesssss() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.LANDevice.1.WLANConfiguration.5.SSID SoulTelecom.5G-" + TextBox1.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.LANDevice.1.WLANConfiguration.5.PreSharedKey.1.PreSharedKey " + TextBox5.Text + "  \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In linesssss
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                Else

                End If

                If CheckBox5.Checked Then
                    ' Gerencia Remota
                    Dim linessssss() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -f -s InternetGatewayDevice.X_ASB_COM_PreConfig.X_ASB_COM_ExternalWebAccess true \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In linessssss
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                Else

                End If
                If CheckBox4.Checked Then
                    ' Quantidade de Clientes
                    Dim linessssssss() As String = {"" + ip + " 23",
                                             "WAIT ""/sbin #""",
                                             "SEND ""cfgcli -s InternetGatewayDevice.Services.X_CT-COM_MWBAND.TotalTerminalNumber " + ComboBox3.Text + " \m""",
                                             "WAIT ""/sbin #""",
                                             "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In linessssssss
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                Else

                End If
            End If

            run = 0

        End While

        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        TextBox2.Enabled = True
        TextBox7.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox1.Enabled = True
        TextBox5.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox7.Enabled = True
        CheckBox6.Enabled = True
        CheckBox5.Enabled = True
        CheckBox4.Enabled = True
        TextBox6.Enabled = True
        ComboBox4.Enabled = True
        ComboBox3.Enabled = True

    End Sub

    Private Function Filter(p As System.Object, text As System.String) As System.String()
        Throw New NotImplementedException()
    End Function

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            TextBox1.Enabled = True
            TextBox5.Enabled = True
        Else
            TextBox1.Enabled = False
            TextBox5.Enabled = False
        End If
    End Sub


    Private Sub btnSend_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text <> "" Then
            TextBox4.Enabled = True
        Else
            TextBox4.Enabled = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            ComboBox3.Enabled = True
        Else
            ComboBox3.Enabled = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            TextBox3.Enabled = True
            TextBox4.Enabled = True
        Else
            TextBox3.Enabled = False
            TextBox4.Enabled = False
        End If
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked Then
            TextBox6.Enabled = True
        Else
            TextBox6.Enabled = False
        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            TextBox6.Enabled = True
            ComboBox4.Enabled = True
        Else
            TextBox6.Enabled = False
            ComboBox4.Enabled = False
        End If
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        If MessageBox.Show("Deseja mesmo sair?", My.Application.Info.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class
