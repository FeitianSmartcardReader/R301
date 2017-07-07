Imports _301_Sample_Demo.AlcorEMV
Imports _301_Sample_Demo.WinScard
Imports System.Text

Public Class Form301

    Dim lngContext As IntPtr = Nothing
    Dim hCard As IntPtr = Nothing

    Private Sub Form301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReadMMAddr.MaxLength = 2
        ReadMMReplyLen.MaxLength = 2
        UpdateMMAddr.MaxLength = 2
        UpdateMMData.MaxLength = 2
        ReadPMReplyLen.MaxLength = 2
        WritePMAddr.MaxLength = 2
        WritePMData.MaxLength = 2
        UpdateSMAddr.MaxLength = 2
        UpdateSMData.MaxLength = 2
        CompareVDAddr.MaxLength = 2
        CompareVDData.MaxLength = 2
        ListReaders()
        StatuDisconnect()
    End Sub

    Private Sub ComboBoxReaders_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxReaders.DropDown
        StatuDisconnect()
        ListReaders()

    End Sub

    Private Sub ButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConnect.Click
        Dim lngresult As Int32 = 0
        Dim ActiveProtocol As Int32 = 1
        If hCard <> Nothing Then
            lngresult = SCardDisconnect(hCard, SCARD_LEAVE_CARD)
        End If
        lngresult = SCardConnect(lngContext, ComboBoxReaders.Text, SCARD_SHARE_DIRECT, 0, _
                                 hCard, _
                                 ActiveProtocol)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        StatuConnect()
        GoTo SUCCESS
ERORR1:
        lngresult = SCardDisconnect(hCard, SCARD_LEAVE_CARD)
        hCard = Nothing
        MsgBox("Connect failed !", MsgBoxStyle.Information, "Connect")
        StatuDisconnect()
SUCCESS:
    End Sub

    Private Sub ButtonInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonInit.Click
        Dim lngresult As Int32 = 0
        Dim sendbuff(0 To 3) As Byte
        Dim RecvBuff(0 To 63) As Byte
        Dim SendBuffLen As Integer = 0
        'Dim RetBuffLen(0 To 1) As Integer
        Dim RetBuffLen As Integer = 0
        lngresult = Alcor_SwitchCardMode(hCard, DEFAULT_SLOT_NUM, SYNCHRONOUS_CARD_SLE4442_MODE)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If

        sendbuff(0) = SCARD_POWER_DOWN
        'RetBuffLen(0) = 0
        lngresult = SCardControl(hCard, IOCTL_SMARTCARD_POWER, sendbuff, 4, RecvBuff, 0, RetBuffLen)

        sendbuff(0) = SCARD_COLD_RESET
        lngresult = SCardControl(hCard, IOCTL_SMARTCARD_POWER, sendbuff, 4, RecvBuff, 64, RetBuffLen)

        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        RichTextBoxATR.Text = HexToString(RecvBuff, RetBuffLen)

        GoTo SUCCESS
ERORR1:
        MsgBox("Initialize failed !", MsgBoxStyle.Information, "Initialize")
        RichTextBoxATR.Text = ""
SUCCESS:
    End Sub

    Private Sub ButtonVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonVerify.Click
        Dim lngresult As Int32 = 0
        Dim pPinDat As Byte() = {&H0}
        Dim ret As Boolean = False
        ret = StringToHex(TextBoxPin.Text.Replace(" ", ""), pPinDat)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        lngresult = SLE4442Cmd_Verify(hCard, DEFAULT_SLOT_NUM, 3, pPinDat)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        RichTextBox.Text = "Verify pin SUCCESS! "
        GoTo SUCCESS

ERORR1:
        MsgBox("Verify pin failed  !", MsgBoxStyle.Information, "Verify")
        RichTextBox.Text = "Verify pin failed ! "
SUCCESS:
    End Sub

    Private Sub ButtonReadMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadMM.Click
        Dim lngresult As Int32 = 0
        Dim bAddress As Byte = &H0
        Dim bReadlen As Byte = &H0
        Dim RecvBuff(0 To 255) As Byte
        Dim RecvLen As Integer = 0
        Dim ret As Boolean = False
        Dim handl() As Byte = {0}
        ret = StringToHex(ReadMMAddr.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bAddress = handl(0)
        ret = StringToHex(ReadMMReplyLen.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bReadlen = handl(0)
        lngresult = SLE4442Cmd_ReadMainMemory(hCard, DEFAULT_SLOT_NUM, bAddress, bReadlen, RecvBuff(0), RecvLen)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        RichTextBox.Text = HexToString(RecvBuff, RecvLen)
        GoTo SUCCESS
ERORR1:
        MsgBox("Read Main Memory failed  !", MsgBoxStyle.Information, "Read")
        RichTextBox.Text = "Read Main Memory failed  !"
SUCCESS:
    End Sub

    Private Sub ButtonUpdateMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdateMM.Click
        Dim lngresult As Int32 = 0
        Dim bAddress As Byte = &H0
        Dim bData As Byte = &H0
        Dim ret As Boolean = False
        Dim handl() As Byte = {0}
        ret = StringToHex(UpdateMMAddr.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bAddress = handl(0)
        ret = StringToHex(UpdateMMData.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bData = handl(0)
        lngresult = SLE4442Cmd_UpdateMainMemory(hCard, DEFAULT_SLOT_NUM, bAddress, bData)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        MsgBox("Update Main Memory SUCCESS  !", MsgBoxStyle.Information, "Update")
        GoTo SUCCESS
ERORR1:
        MsgBox("Update Main Memory failed  !", MsgBoxStyle.Information, "Update")
SUCCESS:
    End Sub

    Private Sub ButtonReadPM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadPM.Click
        Dim lngresult As Int32 = 0
        Dim bReadLen As Byte = &H4
        Dim RecvBuff(0 To 3) As Byte
        Dim RecvLen As Integer = 0
        lngresult = SLE4442Cmd_ReadProtectionMemory(hCard, DEFAULT_SLOT_NUM, bReadLen, RecvBuff(0), RecvLen)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        RichTextBox.Text = HexToString(RecvBuff, RecvLen)
        GoTo SUCCESS
ERORR1:
        MsgBox("Read Protection Memory failed  !", MsgBoxStyle.Information, "Read")
SUCCESS:
    End Sub

    Private Sub ButtonWritePM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonWritePM.Click
        Dim lngresult As Int32 = 0
        Dim bAddress As Byte = &H0
        Dim bData As Byte = &HFF
        Dim ret As Boolean = False
        Dim handl() As Byte = {0}
        ret = StringToHex(WritePMAddr.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bAddress = handl(0)
        ret = StringToHex(WritePMData.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bData = handl(0)
        lngresult = SLE4442Cmd_WriteProtectionMemory(hCard, DEFAULT_SLOT_NUM, bAddress, bData)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        MsgBox("Write Protection Memory SUCCESS  !", MsgBoxStyle.Information, "Write")
        GoTo SUCCESS
ERORR1:
        MsgBox("Write Protection Memory failed  !", MsgBoxStyle.Information, "Write")
SUCCESS:
    End Sub

    Private Sub ButtonReadSM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadSM.Click
        Dim lngresult As Int32 = 0
        Dim bReadLen As Byte = &H4
        Dim RecvBuff(0 To 3) As Byte
        Dim RecvLen As Integer = 0
        lngresult = SLE4442Cmd_ReadSecurityMemory(hCard, DEFAULT_SLOT_NUM, bReadLen, RecvBuff(0), RecvLen)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        RichTextBox.Text = HexToString(RecvBuff, RecvLen)
        GoTo SUCCESS
ERORR1:
        MsgBox("Read Security Memory failed  !", MsgBoxStyle.Information, "Read")
SUCCESS:
    End Sub

    Private Sub ButtonUpdateSM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdateSM.Click
        Dim lngresult As Int32 = 0
        Dim bAddress As Byte = &H0
        Dim bData As Byte = &H0
        Dim ret As Boolean = False
        Dim handl() As Byte = {0}
        ret = StringToHex(UpdateSMAddr.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bAddress = handl(0)
        ret = StringToHex(UpdateSMData.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bData = handl(0)
        lngresult = SLE4442Cmd_UpdateSecurityMemory(hCard, DEFAULT_SLOT_NUM, bAddress, bData)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        MsgBox("Update Security Memory SUCCESS  !", MsgBoxStyle.Information, "Update")
        GoTo SUCCESS
ERORR1:
        MsgBox("Update Security Memory failed  !", MsgBoxStyle.Information, "Update")
SUCCESS:
    End Sub

    Private Sub ButtonCompareVD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCompareVD.Click
        Dim lngresult As Int32 = 0
        Dim bAddress As Byte = &H0
        Dim bData As Byte = &HFF
        Dim ret As Boolean = False
        Dim handl() As Byte = {0}
        ret = StringToHex(UpdateSMAddr.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bAddress = handl(0)
        ret = StringToHex(UpdateSMData.Text.Replace(" ", ""), handl)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        bData = handl(0)
        lngresult = SLE4442Cmd_CompareVerificationData(hCard, DEFAULT_SLOT_NUM, bAddress, bData)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If
        MsgBox("Compare Verification Data SUCCESS  !", MsgBoxStyle.Information, "Compare")
        GoTo SUCCESS
ERORR1:
        MsgBox("Compare Verification Data failed  !", MsgBoxStyle.Information, "Compare")
SUCCESS:
    End Sub

    Private Sub ButtonChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChange.Click
        Dim lngresult As Int32 = 0
        Dim bData As Byte() = {&H0}
        Dim ret As Boolean = False
        Dim RecvBuff(0 To 3) As Byte
        Dim RecvLen As Integer = 0
        ret = StringToHex(TextBoxPin.Text.Replace(" ", ""), bData)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If
        For column As Integer = 0 To 2
            lngresult = SLE4442Cmd_UpdateSecurityMemory(hCard, DEFAULT_SLOT_NUM, column + 1, bData(column))
            If lngresult <> SCARD_S_SUCCESS Then
                lngresult = SLE4442Cmd_ReadSecurityMemory(hCard, DEFAULT_SLOT_NUM, 4, RecvBuff(0), RecvLen)
                If lngresult <> SCARD_S_SUCCESS Then
                    GoTo ERORR1
                End If
                If RecvBuff(column + 1) = bData(column) Then
                    Continue For
                End If
                GoTo ERORR1
            End If
        Next
        MsgBox("Sle4442 Change PIN Data SUCCESS  !", MsgBoxStyle.Information, "Change")
        GoTo SUCCESS
ERORR1:
        MsgBox("Sle4442 Change PIN Data failed  !", MsgBoxStyle.Information, "Change")
SUCCESS:
    End Sub

    Private Sub StatuDisconnect()
        ButtonConnect.Enabled = True
        GroupBox1.Enabled = False
        ToolStripStatus.Text = "Disconnect"
    End Sub

    Private Sub StatuConnect()
        ButtonConnect.Enabled = False
        GroupBox1.Enabled = True
        ToolStripStatus.Text = "Connected"
    End Sub

    Private Sub ListReaders()
        Dim lngresult As Integer
        Dim sGroup As String = Nothing
        Dim sReaderList As String
        Dim RdrCount As Long
        Dim MyArray, Index
        sReaderList = New System.String(vbNullChar, 64)
        RdrCount = sReaderList.Length

        ComboBoxReaders.Items.Clear()
        If lngContext <> Nothing Then
            SCardReleaseContext(lngContext)
        End If

        lngresult = SCardEstablishContext(SCARD_SCOPE_SYSTEM, 0, 0, lngContext)
        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If

        lngresult = SCardListReaders(lngContext, sGroup, sReaderList, RdrCount)

        If lngresult <> SCARD_S_SUCCESS Then
            GoTo ERORR1
        End If

        MyArray = Split(sReaderList, "", -1, 1)
        Index = 0
        While Index < UBound(MyArray) + 1
            ComboBoxReaders.Items.Add(MyArray(Index))
            Index += 1
        End While

        If ComboBoxReaders.Items.Count > 0 Then
            ComboBoxReaders.SelectedIndex = 0
        End If
        GoTo SUCCESS
ERORR1:
        SCardReleaseContext(lngContext)
        lngContext = Nothing
        MsgBox("ListReaders failed !", MsgBoxStyle.Information, "ListReaders")
SUCCESS:
    End Sub

    Private Function HexToString(ByVal byteArray As Byte(), ByVal len As Integer) As String
        Dim StrBuilder As StringBuilder = New StringBuilder("")
        For column As Integer = 0 To len - 1
            StrBuilder.Append(String.Format("{0:X2} ", byteArray(column)))
        Next
        Return StrBuilder.ToString()
    End Function

    Private Function StringToHex(ByVal str As String, ByRef byteRet As Byte()) As Boolean
        Dim descBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(str)
        If (descBytes.Length Mod 2) = 1 Then
            Return False
        End If
        Dim length As Integer = CInt(descBytes.Length / 2)
        For column As Integer = 0 To descBytes.Length - 1
            If descBytes(column) >= &H30 And descBytes(column) <= &H39 Then
                descBytes(column) = descBytes(column) - &H30
            ElseIf descBytes(column) >= &H41 And descBytes(column) <= &H46 Then
                descBytes(column) = descBytes(column) - &H41 + 10
            ElseIf descBytes(column) >= &H61 And descBytes(column) <= &H66 Then
                descBytes(column) = descBytes(column) - &H61 + 10
            Else
                Return False
            End If

        Next

        Dim lastBytes(0 To length - 1) As Byte
        For column As Integer = 0 To length - 1
            lastBytes(column) = descBytes(2 * column) * 16 + descBytes(2 * column + 1)
        Next
        ReDim byteRet(0 To length - 1)
        Array.Copy(lastBytes, byteRet, length)
        Return True
    End Function

End Class
