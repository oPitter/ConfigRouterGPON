Option Compare Binary
Option Explicit On
Option Strict On

Imports System.IO
Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
'''   Hauptformular des Programms.
''' </summary>
Public Class main



    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "" Then
            MsgBox("Selecione um Fabricante!")
        End If
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

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Repo = "http://170.78.180.42/openwrt/"
        Dim passwd = "ITEC@WIFI"
        Dim empresa = "iTELECOM-"
        Dim senhaPPP = "1234"
        Dim run = 1
        Dim check = 0
        While run = 1
            ComboBox1.Enabled = False
            If ComboBox1.Text = "" Then
                MsgBox("Selecione um Fabricante!")
                run = 0
            Else
                check = check + 1
            End If
            ComboBox2.Enabled = False
            If ComboBox2.Text = "" And check = 1 Then
                MsgBox("Selecione um Modelo!")
                run = 0
            Else
                check = check + 1
            End If
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            If TextBox2.Text = "" And check = 2 Then
                MsgBox("Digite o usuário PPPoE do Cliente!")
                run = 0
            Else
                check = check + 1
            End If
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            TextBox6.Enabled = False
            TextBox7.Enabled = False
            TextBox8.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            CheckBox6.Enabled = False
            CheckBox7.Enabled = False



            If CheckBox1.Checked Then
                Dim proc = 0
                Dim conn = 1
                While conn = 1
                    If My.Computer.Network.Ping("192.168.00.01") Then
                        MsgBox("Server pinged successfully.")
                        If ComboBox2.Text = "WR-740Nv6" Then
                            Dim COOKIE = "Authorization=Basic%20YWRtaW46MjEyMzJmMjk3YTU3YTVhNzQzODk0YTBlNGE4MDFmYzM%3D"
                            Shell("cmd.exe /c curl --silent --cookie " + COOKIE + " http://192.168.0.1/userRpm/LoginRpm.htm?Save=Save | sed ""s|.*/\([A-Z]*\)/.*|\1|""")
                        Else
                            Shell("cmd.exe /c curl --user admin:admin  http://192.168.0.1/userRpm/SoftwareUpgradeRpm.htm > check.txt")
                        End If
                        Dim file As String = "check.txt"
                        Dim text As String = "router"
                        Dim lines() As String = Filter(System.IO.File.ReadAllLines(file), text)
                        If UBound(lines) = -1 Then
                            MsgBox("not found")
                            conn = 0
                        Else
                            MsgBox("found")
                            PictureBox2.Visible = True
                            proc = proc + 1
                            conn = 0
                        End If
                    Else
                        MsgBox("Roteador Não Identificado!")
                        conn = 0
                    End If

                    If My.Computer.Network.Ping("192.168.00.01") And proc = 1 Then
                        Shell("cmd.exe /c curl --user admin:admin --referer 'http://192.168.0.1/userRpm/SoftwareUpgradeRpm.htm' --form Filename=@" + ComboBox2.Text + ".bin -F 'Upgrade=Upgrade' http://192.168.0.1/incoming/Firmware.htm -o NUL")
                        Thread.Sleep(2000)
                        Shell("cmd.exe /c curl --max-time 2 --user admin:admin --referer 'http://192.168.0.1/incoming/Firmware.htm' http://192.168.0.1/userRpm/FirmwareUpdateTemp.htm -o NUL")
                        Thread.Sleep(85000)
                        If My.Computer.Network.Ping("192.168.01.01") Then
                            PictureBox3.Visible = True
                            proc = proc + 1
                        Else
                            MsgBox("Roteador ainda não responde!")
                        End If
                    End If
                End While
                If proc = 2 Then
                    MsgBox("Primeira etapa concluida com sucesso!")
                End If
            Else

            End If

            If CheckBox2.Checked Then
                If My.Computer.Network.Ping("192.168.01.01") Then
                    MsgBox("Roteador Identificado")
                    ' Criar a Quantidade de Linhas no Arquivo
                    Dim lines() As String = {"192.168.1.1 23",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""rm /tmp/update.bin \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""wget -c -P /tmp " + Repo + "" + ComboBox2.Text + "/update.bin \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""rm /www/backup.tar.gz \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""wget -c -P /www " + Repo + "" + ComboBox2.Text + "/backup.tar.gz \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""sysupgrade -r /www/backup.tar.gz \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""sysupgrade -v /tmp/update.bin \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""ls /etc/ \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""ls /etc/ \m""",
                                                 "WAIT ""root@OpenWrt:/#""",
                                                 "SEND ""exit \m"""}
                    ' Salva as indformações no arquivo script.txt".
                    Using outputFile As New StreamWriter(Convert.ToString("script.txt"))
                        For Each line As String In lines
                            outputFile.WriteLine(line)
                        Next
                    End Using
                    Shell("cmd.exe /c tst10.exe /r:script.txt /o:output.txt \m", vbMinimizedNoFocus, True, 80000)
                    Dim output As String = My.Computer.FileSystem.ReadAllText("output.txt")
                    If output.Contains("Login failed.") Or FileLen("output.txt") = 0 Then
                        'Dim myPasswd As String = Nothing
                        'myPasswd = InputBox("Digite a senha atual do Roteador!")
                        If TextBox3.Text = "" Then
                            TextBox3.Text = "" + empresa + "" + TextBox2.Text + ""
                            TextBox4.Text = "" + TextBox2.Text + "xx"
                            MsgBox(TextBox3.Text)
                        End If
                        ' Criar a Quantidade de Linhas no Arquivo
                        Dim linesss() As String = {"#!/bin/bash",
                                                   "uci set network.wan.proto=dhcp",
                                                   "uci commit network",
                                                   "rm /tmp/update.bin",
                                                   "wget -c -P /tmp " + Repo + "" + ComboBox2.Text + "/update.bin",
                                                   "rm /www/backup.tar.gz",
                                                   "wget -c -P /www " + Repo + "" + ComboBox2.Text + "/backup.tar.gz \m",
                                                   "sysupgrade -r /www/backup.tar.gz",
                                                   "sysupgrade -v /tmp/update.bin"}
                        ' Salva as indformações no arquivo script.txt".
                        Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                            For Each line As String In linesss
                                outputFile.WriteLine(line)
                            Next
                        End Using
                        Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbNormalFocus, True, 73000)
                        Thread.Sleep(83000)
                        PictureBox4.Visible = True
                    Else
                        Thread.Sleep(83000)
                        PictureBox4.Visible = True
                    End If
                Else
                    MsgBox("Roteador Não responde!")
                End If
            Else

            End If
            If CheckBox3.Checked Then
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linessss() As String = {"#!/bin/bash",
                                          "uci set system.@system[0].hostname=" + TextBox2.Text + "",
                                          "uci commit system",
                                          "uci set network.wan.username=" + TextBox2.Text + "",
                                          "uci set network.wan.password=" + senhaPPP + "",
                                          "uci commit network",
                                          "sysupgrade -b /www/backup.tar.gz",
                                          "opkg update",
                                          "wget -c -P /tmp " + Repo + "miniupnpd.ipk",
                                          "opkg install /tmp/miniupnpd.ipk",
                                          "opkg install luci-app-upnp",
                                          "/etc/init.d/miniupnpd enable",
                                          "/etc/init.d/miniupnpd start",
                                          "/etc/init.d/firewall restart",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linessss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
                PictureBox7.Visible = True
            Else
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linesssss() As String = {"#!/bin/bash",
                                          "uci set system.@system[0].hostname=" + TextBox2.Text + "",
                                          "uci set network.wan.username=" + TextBox2.Text + "",
                                          "uci set network.wan.password=" + passwd + "",
                                          "uci commit network",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linesssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
            End If
            If CheckBox7.Checked Then
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linessssss() As String = {"#!/bin/bash",
                                          "uci set wireless.@wifi-iface[0].ssid=" + TextBox3.Text + "",
                                          "uci set wireless.@wifi-iface[0].key=" + TextBox4.Text + "",
                                          "uci commit wireless",
                                          "sysupgrade -b /www/backup.tar.gz",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linessssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
                PictureBox6.Visible = True
            Else
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linesssssss() As String = {"#!/bin/bash",
                                          "uci set wireless.@wifi-iface[0].ssid=" + TextBox3.Text + "",
                                          "uci set wireless.@wifi-iface[0].key=" + TextBox4.Text + "",
                                          "uci commit wireless",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linesssssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
            End If
            If CheckBox6.Checked Then
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linessssssss() As String = {"echo ifconfig eth1 down >> /etc/init.d/boot",
                                          "echo ifconfig eth1 hw ether " + TextBox1.Text + ">> /etc/init.d/boot",
                                          "echo ifconfig eth1 up >> /etc/init.d/boot",
                                          "echo ifconfig eth1 >> /etc/init.d/boot",
                                          "sysupgrade -b /www/backup.tar.gz",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linessssssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
                PictureBox5.Visible = True
            End If
            If CheckBox5.Checked Then
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linesssssssss() As String = {"#!/bin/bash",
                                          "uci set wireless.@wifi-device[1].disabled=0",
                                          "uci commit wireless",
                                          "wifi",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linesssssssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
            Else
                ' Criar a Quantidade de Linhas no Arquivo
                Dim linessssssssss() As String = {"#!/bin/bash",
                                          "uci set wireless.@wifi-device[1].disabled=1",
                                          "uci commit wireless",
                                          "wifi",
                                          "exit"}
                ' Salva as indformações no arquivo script.txt".
                Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                    For Each line As String In linessssssssss
                        outputFile.WriteLine(line)
                    Next
                End Using
                Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbMinimizedFocus, True, 5000)
            End If
            ' Criar a Quantidade de Linhas no Arquivo
            Dim liness() As String = {"#!/bin/bash",
                                      "rm /etc/rc.button/reset",
                                      "wget -c -P /etc/rc.button/ " + Repo + "" + ComboBox2.Text + "/reset",
                                      "chmod 755 /etc/rc.button/reset",
                                      "opkg update",
                                      "rm /tmp/miniupnpd.ipk",
                                      "wget -c -P /tmp " + Repo + "miniupnpd.ipk",
                                      "opkg install /tmp/miniupnpd.ipk",
                                      "opkg install luci-app-upnp",
                                      "/etc/init.d/miniupnpd enable",
                                      "/etc/init.d/miniupnpd start",
                                      "/etc/init.d/firewall restart",
                                      "reboot"}
            ' Salva as indformações no arquivo script.txt".
            Using outputFile As New StreamWriter(Convert.ToString("script.sh"))
                For Each line As String In liness
                    outputFile.WriteLine(line)
                Next
            End Using
            Shell("cmd.exe /c echo yes | plink  -agent -pw " + passwd + " -m script.sh  root@192.168.1.1", vbNormalFocus, True, 5000)
            Thread.Sleep(10000)
            run = 0
        End While


        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = False
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox4.Enabled = True
        CheckBox5.Enabled = True
        CheckBox6.Enabled = True
        CheckBox7.Enabled = True
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Text = "Observação" Then
            TextBox6.Enabled = False
            TextBox7.Enabled = False
            TextBox8.Enabled = False
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Dim myObs As String = Nothing
            myObs = InputBox("Observações Técnicas:")
        Else
            TextBox6.Enabled = True
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            Button1.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            TextBox1.Enabled = True
        Else
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "WR-740Nv6" Then
            MsgBox("Faça o Upload do Arquivo Factory Manualmente!")
            CheckBox1.Enabled = False
        Else
            CheckBox1.Enabled = True
        End If
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

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
End Class
