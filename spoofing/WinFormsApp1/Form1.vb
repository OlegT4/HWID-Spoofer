Imports Microsoft.Win32
Imports System.IO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToParent()
    End Sub
    Public Function getMacAddress() As String

        Dim objStreamReader As StreamReader
        Const FILENAME As String = "macaddress.txt"
        Dim line As String
        Dim allWords As New List(Of String)
        Dim generator As New Random
        Dim n As Integer

        objStreamReader = New StreamReader(FILENAME)

        line = objStreamReader.ReadLine
        Do While (Not line Is Nothing)
            allWords.Add(line)
            Console.WriteLine(line)
            line = objStreamReader.ReadLine
        Loop
        objStreamReader.Close()

        n = generator.Next(1, allWords.Count)

        Return allWords(n)

    End Function

    Public Function getDesktopName() As String

        Dim objStreamReader As StreamReader
        Const FILENAME As String = "host.txt"
        Dim line As String
        Dim allWords As New List(Of String)
        Dim generator As New Random
        Dim n As Integer

        objStreamReader = New StreamReader(FILENAME)

        line = objStreamReader.ReadLine
        Do While (Not line Is Nothing)
            allWords.Add(line)
            Console.WriteLine(line)
            line = objStreamReader.ReadLine
        Loop
        objStreamReader.Close()

        n = generator.Next(1, allWords.Count)

        Return allWords(n)

    End Function

    Public Function getRandomID() As String

        Dim objStreamReader As StreamReader
        Const FILENAME As String = "volumeids.txt"
        Dim line As String
        Dim allWords As New List(Of String)
        Dim generator As New Random
        Dim n As Integer

        objStreamReader = New StreamReader(FILENAME)

        line = objStreamReader.ReadLine
        Do While (Not line Is Nothing)
            allWords.Add(line)
            Console.WriteLine(line)
            line = objStreamReader.ReadLine
        Loop
        objStreamReader.Close()

        n = generator.Next(1, allWords.Count)

        Return allWords(n)

    End Function

    Public Function GetSerialNumber() As String
        Dim serialGuid As Guid = Guid.NewGuid()
        Dim uniqueSerial As String = serialGuid.ToString("N")
        Dim uniqueSerialLength As String = uniqueSerial.Substring(0, 16).ToUpper()

        Dim serialArray As Char() = uniqueSerialLength.ToCharArray()
        Dim finalSerialNumber As String = ""

        Dim j As Integer = 0
        For i As Integer = 0 To 15
            For j = i To 4 + (i - 1)
                finalSerialNumber += serialArray(j)
            Next
            If j = 16 Then
                Exit For
            Else
                i = (j) - 1
                finalSerialNumber += "-"
            End If
        Next


        Return finalSerialNumber
    End Function


    Dim NewGUID As String = System.Guid.NewGuid().ToString()
    Dim NewGUID2 As String = System.Guid.NewGuid().ToString()
    Dim NewGUID3 As String = System.Guid.NewGuid().ToString()
    Dim NewGUID4 As String = System.Guid.NewGuid().ToString()
    Dim NewGUID5 As String = System.Guid.NewGuid().ToString()


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\SQMClient\", True)
            key.SetValue("MachineId", "{" & NewGUID.ToUpper & "}")
            key.Close()
            Dim key2 As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\SQMClient\", True)
            key2.SetValue("UserId", "{" & NewGUID2.ToUpper & "}")
            key2.Close()
            Dim key3 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001\", True)
            key3.SetValue("HwProfileGuid", "{" & NewGUID3 & "}")
            key3.Close()
            Dim key4 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Cryptography\", True)
            key4.SetValue("MachineGuid", NewGUID3)
            key4.Close()
            Dim key5 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\SystemInformation\", True)
            key5.SetValue("ComputerHardwareId", "{" & NewGUID4 & "}")
            key5.Close()
            Dim key6 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\SystemInformation\", True)
            key6.SetValue("ComputerHardwareId", "{" & NewGUID4 & "}")
            key6.Close()
            Dim key7 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\", True)
            key7.SetValue("SusClientId", NewGUID5)
            key7.Close()
            Dim key8 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\", True)
            key8.SetValue("ProductID", GetSerialNumber())
            key8.Close()
            Dim key9 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0001\", True)
            key9.SetValue("NetworkAddress", getMacAddress())
            key9.Close()

            Dim proc As New System.Diagnostics.Process()
            proc.StartInfo.CreateNoWindow = True
            proc.StartInfo.FileName = "Volumeid.exe"
            proc.StartInfo.Arguments = " C: " & getRandomID()
            proc.Start()
            proc.Close()

            Dim power As New System.Diagnostics.Process()
            power.StartInfo.CreateNoWindow = True
            power.StartInfo.FileName = "powershell.exe"
            power.StartInfo.Arguments = " Rename-Computer -NewName " & getDesktopName()
            power.Start()
            power.Close()

            Button1.Enabled = False
            Button1.Text = "Spoofed"

            If MsgBox("Reboot computer now?", MsgBoxStyle.YesNo, "HWID Spoofer") = MsgBoxResult.Yes Then
                Dim reboot As New System.Diagnostics.Process()
                reboot.StartInfo.CreateNoWindow = True
                reboot.StartInfo.FileName = "shutdown"
                reboot.StartInfo.Arguments = " -r -t 00"
                reboot.Start()
                reboot.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class
