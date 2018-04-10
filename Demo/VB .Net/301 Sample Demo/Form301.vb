Imports System.Text
Imports _301_Sample_Demo.modSmartCard

Public Class Form301

    Dim lContext As IntPtr = Nothing
    Dim lCard As IntPtr = Nothing
    Dim ret As Integer
    Dim sReaderName As String = String.Empty
    Dim pdwProtocol As Integer
    Dim pdwState As Integer
    Dim pszATR2 As String

    Private Sub Form301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListReaders()
        StatuDisconnect()
    End Sub

    Private Sub ComboBoxReaders_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxReaders.DropDown
        StatuDisconnect()
        ListReaders()

    End Sub

    Private Sub ButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConnect.Click
        sReaderName = ComboBoxReaders.Text
        ret = SCardConnect(lContext, sReaderName, SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0 Xor SCARD_PROTOCOL_T1, lCard, pdwProtocol)
        If ret <> SCARD_S_SUCCESS Then
            GoTo ERROR1
        End If

        ret = SCardStatus(lCard, pdwState, pdwProtocol, pszATR2) ' recupera lo stato della carta
        If ret <> SCARD_S_SUCCESS Then
            GoTo ERROR1
        End If

        RichTextBoxLog.Text = pszATR2
        GoTo SUCCESS

ERROR1:
        SCardDisconnect(lCard, SCARD_UNPOWER_CARD)
        SCardReleaseContext(lContext)
        lContext = Nothing
        MsgBox(SCardErrDescription(ret), MsgBoxStyle.Information, "ERROR")

        Return
SUCCESS:
        StatuConnect()
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
        Dim sGroup As String = Nothing
        Dim sReaderList As String
        Dim RdrCount As Long
        Dim MyArray
        Dim i As Integer = 0

        sReaderList = New System.String(vbNullChar, 512)
        RdrCount = sReaderList.Length

        ComboBoxReaders.Items.Clear()
        If lContext <> Nothing Then
            SCardReleaseContext(lContext)
        End If

        ret = SCardEstablishContext(SCARD_SCOPE_USER, &H0S, &H0S, lContext)
        If ret <> SCARD_S_SUCCESS Then
            GoTo ERROR1
        End If

        ret = SCardListReadersA(lContext, sGroup, sReaderList, RdrCount)
        If ret <> SCARD_S_SUCCESS Then
            GoTo ERROR1
        End If

        MyArray = sReaderList.Split(Chr(0))

        For i = 0 To UBound(MyArray)
            If MyArray(i) <> "" Then
                ComboBoxReaders.Items.Add(MyArray(i))
            Else
                Return
            End If
        Next
 
        GoTo SUCCESS
ERROR1:
        SCardDisconnect(lCard, SCARD_UNPOWER_CARD)
        SCardReleaseContext(lContext)
        lContext = Nothing
        MsgBox(SCardErrDescription(ret), MsgBoxStyle.Information, "ERROR")
        Return
SUCCESS:
    End Sub

    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click

        Dim pSendBuffer(0 To 32) As Byte
        Dim revBuff(0 To 4096) As Byte
        Dim revLength As Integer
        Dim pbSW1 As Byte
        Dim pbSW2 As Byte

        ret = StringToHex(RichTextBoxAPDU.Text.Replace(" ", ""), pSendBuffer)
        If ret = False Then
            MsgBox("Illegal character! Please Input  0 ~ 9 or A ~ F", MsgBoxStyle.Information, "Warning")
            Return
        End If

        ret = SCardIO(lCard, pSendBuffer, revBuff, revLength, pbSW1, pbSW2)
        If ret <> SCARD_S_SUCCESS Then
            GoTo ERROR1
        End If
        RichTextBoxLog.Text = HexToString(revBuff, revLength)

READ_AGAIN:
        If pbSW1 = (&H61) Then
            pSendBuffer(0) = (&H0)
            pSendBuffer(1) = (&HC0)
            pSendBuffer(2) = (&H0)
            pSendBuffer(3) = (&H0)
            pSendBuffer(4) = pbSW2
            ret = SCardIO(lCard, pSendBuffer, revBuff, revLength, pbSW1, pbSW2)
            If ret <> SCARD_S_SUCCESS Then
                GoTo ERROR1
            End If
            RichTextBoxLog.AppendText(HexToString(revBuff, revLength))
            If pbSW1 = (&H61) Then
                GoTo READ_AGAIN
            End If
        End If

        GoTo SUCCESS
ERROR1:
        SCardDisconnect(lCard, SCARD_UNPOWER_CARD) ' disconnette la carta
        SCardReleaseContext(lContext)
        lContext = Nothing
        MsgBox(SCardErrDescription(ret), MsgBoxStyle.Information, "ERROR")
        Return
SUCCESS:
    End Sub
End Class
