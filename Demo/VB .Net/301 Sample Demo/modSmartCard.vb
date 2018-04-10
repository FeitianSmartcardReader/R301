Option Strict Off
Option Explicit On

Imports System.Text.Encoding
Imports System.Text

Friend Module modSmartCard

    Public Structure SCARD_IO_REQUEST
        Public dwProtocol As Integer
        Public dbPciLength As Integer
    End Structure
    Friend Declare Function SCardEstablishContext Lib "winscard.dll" (ByVal dwScope As Integer, ByVal pvReserved1 As Integer, ByVal pvReserved2 As Integer, ByRef phContext As Integer) As Integer
    Friend Declare Function SCardReleaseContext Lib "winscard.dll" (ByVal hContext As Integer) As Integer
    Friend Declare Function SCardConnect Lib "winscard.dll" Alias "SCardConnectA" (ByVal hContext As Integer, ByVal szReader As String, ByVal dwShareMode As Integer, ByVal dwPreferredProtocols As Integer, ByRef phCard As Integer, ByRef pdwActiveProtocol As Integer) As Integer
    Friend Declare Function SCardDisconnect Lib "winscard.dll" (ByVal hCard As Integer, ByVal dwDisposition As Integer) As Integer
    Friend Declare Function SCardFreeMemory Lib "winscard.dll" (ByVal hContext As Integer, ByVal mszReaderName As String) As Integer
    Friend Declare Function SCardListReadersA Lib "winscard.dll" (ByVal hContext As Integer, ByVal mszGroups As String, ByVal mszReaders As String, ByRef pcchReaders As Integer) As Integer

    Private Declare Function SCardTransmit Lib "winscard.dll" (ByVal hCard As Integer, ByRef pioSendPci As SCARD_IO_REQUEST, ByRef pbSendBuffer As Byte, ByVal cbSendLength As Integer, ByRef pioRecvPci As SCARD_IO_REQUEST, ByRef pbRecvBuffer As Byte, ByRef pcbRecvLength As Integer) As Integer
    Private Declare Function SCardListCardsA Lib "winscard.dll" (ByVal hContext As Integer, ByVal ATRStringOrNull As String, ByVal rgguidInterfacesOrNull As Integer, ByVal cguidInterfaceCount As Integer, ByVal mszCardsOUT As String, ByRef bufferLen As Integer) As Integer
    Private Declare Function SCardStatusA Lib "winscard.dll" (ByVal hCard As Integer, ByVal mszReaderNames As String, ByRef pcchReaderLen As Integer, ByRef pdwState As Integer, ByRef pdwProtocol As Integer, ByVal pbAttr As String, ByRef pcbAtrLen As Integer) As Integer

    Private m_StatusWord As Integer
    Private doTransmit As Boolean = False

    Friend ReadOnly Property StatusWord() As Integer
        Get
            Return m_StatusWord
        End Get
    End Property

    Friend Function SCardIO(ByVal hCard As Long, ByRef aSendData() As Byte, ByRef aRecvData() As Byte, ByRef aRecvLength As Integer, ByRef pbSW1 As Byte, ByRef pbSW2 As Byte) As Long
        Dim ret As Integer
        Dim lRLen As Integer = aRecvData.Length
        Dim xIO As SCARD_IO_REQUEST

        pbSW1 = 0 ' inizializza il parametro SW
        pbSW2 = 0 ' inizializza il parametro SW
        ' imposta il protocollo
        xIO.dwProtocol = SCARD_PROTOCOL_T0  ' Or SCARD_PROTOCOL_T1 Or SCARD_PROTOCOL_RAW
        xIO.dbPciLength = Len(xIO)          ' imposta lunghezza struttura
        doTransmit = True                   ' imposta lo stato dell'inizio dell'operazione
        ret = SCardTransmit(hCard, xIO, aSendData(0), aSendData.Length, xIO, aRecvData(0), lRLen) ' trasmette i dati
        doTransmit = False               ' imposta lo stato di fine dell'operazione
        If ret = 0 And lRLen > 0 Then    ' controlla se ci sono effettivamente dei dati ricevuti
            aRecvLength = lRLen
            pbSW1 = aRecvData(lRLen - 2) ' restituisce lo statusword 1
            pbSW2 = aRecvData(lRLen - 1) ' restituisce lo statusword 2
            ' imposta lo status word
            m_StatusWord = GetStatusWord(pbSW1, pbSW2)
        End If
        Return ret ' restituisce l'eventuale errore
    End Function

    Friend Function SCardSend(ByVal hCard As Integer, ByVal pbCLA As Byte, ByVal pbINS As Byte, ByVal pbP1 As Byte, ByVal pbP2 As Byte, ByRef lpszSendData As String, ByRef pbSW1 As Byte, ByRef pbSW2 As Byte) As Integer
        Dim i As Integer
        Dim ret As Integer
        Dim lSLen As Integer = lpszSendData.Length + 4
        Dim aSend() As Byte = {pbCLA, pbINS, pbP1, pbP2, CByte(lpszSendData.Length)}
        Dim aRecv() As Byte = {0, 0}
        Dim lRLen As Integer = aRecv.GetLength(0)
        Dim xIO As SCARD_IO_REQUEST

        pbSW1 = 0 ' inizializza il parametro SW
        pbSW2 = 0 ' inizializza il parametro SW
        ' imposta il protocollo
        xIO.dwProtocol = SCARD_PROTOCOL_T0
        xIO.dbPciLength = Len(xIO) ' imposta lunghezza struttura
        ReDim Preserve aSend(lSLen) ' dimensiona il buffer di invio
        For i = 5 To lSLen ' compone il buffer
            'ASCII.GetBytes(lpszSendData, 0, lpszSendData.Length, aSend, 5)
            aSend(i) = Asc(Mid(lpszSendData, i - 4, 1)) ' riempiendolo con i car. ascii
        Next
        doTransmit = True ' imposta lo stato dell'inizio dell'operazione
        ret = SCardTransmit(hCard, xIO, aSend(0), lSLen + 1, xIO, aRecv(0), lRLen) ' trasmette i dati
        doTransmit = False      ' imposta lo stato di fine dell'operazione
        If lRLen > 0 Then       ' controlla se ci sono effettivamente dei dati ricevuti
            pbSW1 = aRecv(0)    ' restiuisce lo statusword 1
            pbSW2 = aRecv(1)    ' restituisce lo statusword 2
            ' imposta lo status word
            m_StatusWord = GetStatusWord(pbSW1, pbSW2)
        End If
        Return ret ' restituisce l'eventuale errore
    End Function

    Friend Function SCardRead(ByVal hCard As Integer, ByVal pbCLA As Byte, ByVal pbINS As Byte, ByVal pbP1 As Byte, ByVal pbP2 As Byte, ByVal pbLen As Byte, ByRef lpszRecvData As String, ByRef pbSW1 As Byte, ByRef pbSW2 As Byte) As Integer
        Dim ret As Integer
        Dim aSend() As Byte = {pbCLA, pbINS, pbP1, pbP2, pbLen}
        Dim aRecv() As Byte
        Dim lRLen As Integer = pbLen + 2
        Dim xIO As SCARD_IO_REQUEST

        pbSW1 = 0 ' inizializza il parametro SW
        pbSW2 = 0 ' inizializza il parametro SW
        lpszRecvData = vbNullString ' inizializza la stringa che conterr?i dati ricevuti
        ' imposta il protocollo
        xIO.dwProtocol = SCARD_PROTOCOL_T0
        xIO.dbPciLength = Len(xIO) ' imposta lunghezza struttura
        ReDim aRecv(lRLen - 1) ' dimensiona il buffer di ricezione
        doTransmit = True ' imposta lo stato dell'inizio dell'operazione
        ret = SCardTransmit(hCard, xIO, aSend(0), aSend.Length, xIO, aRecv(0), lRLen)  ' trasmette i dati
        doTransmit = False  ' imposta lo stato di fine dell'operazione
        If lRLen > 0 Then   ' controlla se ci sono effettivamente dei dati ricevuti
            lpszRecvData = ASCII.GetString(aRecv).Substring(0, pbLen)
            pbSW1 = aRecv(pbLen)        ' restiuisce lo statusword 1
            pbSW2 = aRecv(pbLen + 1)    ' restituisce lo statusword 2
            ' imposta lo status word
            m_StatusWord = GetStatusWord(pbSW1, pbSW2)
        End If
        Return ret ' restituisce l'eventuale errore
    End Function

    Friend Function SCardListReaders(ByVal hContext As Integer, ByRef mszGroups As String, ByRef mszReaders As String) As Integer
        Dim ret As Integer
        Dim pszRdr As String
        pszRdr = New String(vbNullChar, 255)
        ret = SCardListReadersA(hContext, mszGroups, pszRdr, Len(pszRdr))
        mszReaders = Replace(pszRdr, vbNullChar, vbNullString)
        Return ret
    End Function

    Friend Function SCardListCards(ByVal hContext As Integer, ByRef ATRStringOrNull As String, ByVal rgguidInterfacesOrNull As Integer, ByVal cguidInterfaceCount As Integer, ByRef mszCardsOUT As String) As Integer
        Dim ret As Integer
        Dim pszCrd As String
        pszCrd = New String(vbNullChar, 255)
        If Len(ATRStringOrNull) = 0 Then ATRStringOrNull = New String(vbNullChar, 255)
        ret = SCardListCardsA(hContext, ATRStringOrNull, rgguidInterfacesOrNull, cguidInterfaceCount, pszCrd, Len(pszCrd))
        mszCardsOUT = Replace(pszCrd, vbNullChar, vbNullString)
        Return ret
    End Function

    Friend Function SCardStatus(ByVal hCard As Integer, ByRef pdwState As Integer) As Integer
        If doTransmit Then Return 0
        Return SCardStatusA(hCard, Nothing, 0, pdwState, 0, Nothing, 0)
    End Function

    Friend Function SCardStatus(ByVal hCard As Integer, ByRef pdwState As Integer, ByRef pdwProtocol As Integer, ByRef pbAttr As String) As Integer
        Dim lpdwATRLen As Integer
        Dim lpdwReaderLen As Integer
        Dim pszReader As String
        Dim pszATR As String
        Dim ret As Integer

        If doTransmit Then Return 0
        pszATR = New String(vbNullChar, SCARD_ATR_LENGTH)
        lpdwATRLen = Len(pszATR)
        pszReader = New String(vbNullChar, 255)
        lpdwReaderLen = Len(pszReader)
        ret = SCardStatusA(hCard, pszReader, lpdwReaderLen, pdwState, pdwProtocol, pszATR, lpdwATRLen)
        pbAttr = ATRHex(pszATR)
        Return ret
    End Function

    Friend Function ReaderStatus(ByRef pdwState As Integer, ByRef pdwProtocol As Integer, ByRef pszATR As String, Optional ByRef pszReaderName As String = vbNullString) As Integer
        Dim lContext As Integer
        Dim ret As Integer
        Dim lCard As Integer
        Dim sReaderName As String = String.Empty

        ret = SCardEstablishContext(SCARD_SCOPE_USER, &H0S, &H0S, lContext) ' stabilisce un contesto
        If ret <> SCARD_S_SUCCESS Then GoTo FreeMemory_ReaderStatus ' se ?avventuo un errore, interrompe
        If Len(pszReaderName) = 0 Then ' se non ?stato specificato il nome del lettore
            ret = SCardListReaders(lContext, vbNullString, sReaderName) ' recupera il nome del primo lettore disponibilie
            If ret <> SCARD_S_SUCCESS Then GoTo FreeMemory_ReaderStatus ' se ?avventuo un errore, interrompe
        Else ' se invece ?stato specificato un lettore
            sReaderName = pszReaderName ' usa il lettore specificato
        End If
        ' si connette alla carta
        ret = SCardConnect(lContext, sReaderName, SCARD_SHARE_SHARED, SCARD_PROTOCOL_DEFAULT, lCard, pdwProtocol)
        If ret <> SCARD_S_SUCCESS Then GoTo FreeMemory_ReaderStatus ' se ?avventuo un errore, interrompe
        ret = SCardStatus(lCard, pdwState, pdwProtocol, pszATR) ' recupera lo stato della carta
        If ret <> SCARD_S_SUCCESS Then GoTo FreeMemory_ReaderStatus ' se ?avventuo un errore, interrompe

FreeMemory_ReaderStatus:
        SCardDisconnect(lCard, SCARD_UNPOWER_CARD) ' disconnette la carta
        If Len(pszReaderName) = 0 Then SCardFreeMemory(lContext, sReaderName)
        SCardReleaseContext(lContext) ' libera il contesto
        Return ret
    End Function

    Friend Function GetATR(ByRef lpszReaderName As String) As String
        Dim iState As Integer
        Dim iProt As Integer
        Dim sATR As String = String.Empty

        ReaderStatus(iState, iProt, sATR, lpszReaderName)
        Return ATRHex(sATR)
    End Function

    Friend Function FirstReader(ByRef lpszReaderName As String) As Integer
        Dim ret As Integer
        Dim sRdrLst As String

        sRdrLst = New String(vbNullChar, 255) ' inizializza il buffer
        ret = SCardListReadersA(&H0S, vbNullString, sRdrLst, Len(sRdrLst)) ' recupera la lista di tutti i lettori installati
        SCardFreeMemory(&H0S, sRdrLst) ' libera la memoria
        lpszReaderName = Mid(sRdrLst, 1, InStr(sRdrLst, vbNullChar) - 1) ' recupera solo il primo lettore
        Return ret ' restituisce lo stato
    End Function

    Friend Function SCardErrDescription(ByVal lErrorNumber As Integer) As String
        Dim sMsg As String

        Select Case lErrorNumber
            Case SCARD_S_SUCCESS : sMsg = "No Errors"
            Case SCARD_F_INTERNAL_ERROR : sMsg = "Internal Error"
            Case SCARD_E_CANCELLED : sMsg = "Cancelled"
            Case SCARD_E_INVALID_HANDLE : sMsg = "Invalid Handle"
            Case SCARD_E_INVALID_PARAMETER : sMsg = "Ivalid Parameter"
            Case SCARD_E_INVALID_TARGET : sMsg = "Invalid Target"
            Case SCARD_E_NO_MEMORY : sMsg = "No Memory"
            Case SCARD_F_WAITED_TOO_LONG : sMsg = "Waited too long"
            Case SCARD_E_INSUFFICIENT_BUFFER : sMsg = "Insufficient Buffer"
            Case SCARD_E_UNKNOWN_READER : sMsg = "Unknown Reader"
            Case SCARD_E_TIMEOUT : sMsg = "Timout"
            Case SCARD_E_SHARING_VIOLATION : sMsg = "Sharing Violation"
            Case SCARD_E_NO_SMARTCARD : sMsg = "No SmartCard"
            Case SCARD_E_UNKNOWN_CARD : sMsg = "Unknown Card"
            Case SCARD_E_CANT_DISPOSE : sMsg = "Cant Dispose"
            Case SCARD_E_PROTO_MISMATCH : sMsg = "Protocol Mismatch"
            Case SCARD_E_NOT_READY : sMsg = "Not Ready"
            Case SCARD_E_INVALID_VALUE : sMsg = "Invalid Value"
            Case SCARD_E_SYSTEM_CANCELLED : sMsg = "System Cancelled"
            Case SCARD_F_COMM_ERROR : sMsg = "Comm Error"
            Case SCARD_F_UNKNOWN_ERROR : sMsg = "Unknown Error"
            Case SCARD_E_INVALID_ATR : sMsg = "Invalid ATR"
            Case SCARD_E_NOT_TRANSACTED : sMsg = "Not Transacted"
            Case SCARD_E_READER_UNAVAILABLE : sMsg = "Reader Unavailable"
            Case SCARD_P_SHUTDOWN : sMsg = "Shutdown"
            Case SCARD_E_PCI_TOO_SMALL : sMsg = "PCI too small"
            Case SCARD_E_READER_UNSUPPORTED : sMsg = "Reader Unsupported"
            Case SCARD_E_DUPLICATE_READER : sMsg = "Dublicate Reader"
            Case SCARD_E_CARD_UNSUPPORTED : sMsg = "Card Unsupported"
            Case SCARD_E_NO_SERVICE : sMsg = "No Service"
            Case SCARD_E_SERVICE_STOPPED : sMsg = "Service Stopped"
            Case SCARD_W_UNSUPPORTED_CARD : sMsg = "Unsupported Card"
            Case SCARD_W_UNRESPONSIVE_CARD : sMsg = "Unresponsive Card"
            Case SCARD_W_UNPOWERED_CARD : sMsg = "Unpowered Card"
            Case SCARD_W_RESET_CARD : sMsg = "Reset Card"
            Case SCARD_W_REMOVED_CARD : sMsg = "Removed Card"
            Case SCARD_E_UNEXPECTED : sMsg = "An unexpected card error has occurred"
            Case SCARD_E_ICC_INSTALLATION : sMsg = "No Primary Provider can be found for the smart card"
            Case SCARD_E_ICC_CREATEORDER : sMsg = "The requested order of object creation is not supported"
            Case SCARD_E_UNSUPPORTED_FEATURE : sMsg = "This smart card does not support the requested feature"
            Case SCARD_E_DIR_NOT_FOUND : sMsg = "The identified directory does not exist in the smart card"
            Case SCARD_E_FILE_NOT_FOUND : sMsg = "The identified file does not exist in the smart card"
            Case SCARD_E_NO_DIR : sMsg = "The supplied path does not represent a smart card directory"
            Case SCARD_E_NO_FILE : sMsg = "The supplied path does not represent a smart card file"
            Case SCARD_E_NO_ACCESS : sMsg = "Access is denied to this file"
            Case SCARD_E_WRITE_TOO_MANY : sMsg = "he smartcard does not have enough memory to store the information"
            Case SCARD_E_BAD_SEEK : sMsg = "There was an error trying to set the smart card file object pointer"
            Case SCARD_E_INVALID_CHV : sMsg = "The supplied PIN is incorrect"
            Case SCARD_E_UNKNOWN_RES_MNG : sMsg = "An unrecognized error code was returned from a layered component"
            Case SCARD_E_NO_SUCH_CERTIFICATE : sMsg = "The requested certificate does not exist"
            Case SCARD_E_CERTIFICATE_UNAVAILABLE : sMsg = "The requested certificate could not be obtained"
            Case SCARD_E_NO_READERS_AVAILABLE : sMsg = "Cannot find a smart card reader"
            Case SCARD_E_COMM_DATA_LOST : sMsg = "A communications error with the smart card has been detected.  Retry the operation"
            Case SCARD_E_NO_KEY_CONTAINER : sMsg = "The requested key container does not exist on the smart card"
            Case SCARD_E_SERVER_TOO_BUSY : sMsg = "The Smart card resource manager is too busy to complete this operation"
            Case SCARD_W_SECURITY_VIOLATION : sMsg = "Access was denied because of a security violation"
            Case SCARD_W_WRONG_CHV : sMsg = "The card cannot be accessed because the wrong PIN was presented"
            Case SCARD_W_CHV_BLOCKED : sMsg = "The card cannot be accessed because the maximum number of PIN entry attempts has been reached"
            Case SCARD_W_EOF : sMsg = "The end of the smart card file has been reached"
            Case SCARD_W_CANCELLED_BY_USER : sMsg = "The action was cancelled by the user"
            Case Else
                sMsg = "Unknown error"
        End Select
        Return sMsg
    End Function

    Friend Function GetStatusWord(ByVal sw1 As Integer, ByVal sw2 As Integer) As Integer
        Dim sHex As String
        sHex = Hex(sw2)
        If Len(sHex) < 2 Then sHex = "0" & sHex
        Return CInt("&H" & Hex(sw1) & sHex) - 65536
    End Function

    Friend Sub SetStatusWord(ByVal sw1 As Integer, ByVal sw2 As Integer)
        m_StatusWord = GetStatusWord(sw1, sw2)
    End Sub

    Friend Function ReaderList() As String()
        Dim i As Short
        Dim c As Short
        Dim ret As Integer
        Dim sRdrLst As String
        Dim aRdrLst() As String
        Dim Lst() As String = Array.CreateInstance(GetType(String), 0)

        c = -1
        sRdrLst = New String(vbNullChar, 255)   ' inizializza il buffer
        ' recupera la lista di tutti i lettori installati
        ret = SCardListReadersA(&H0S, vbNullString, sRdrLst, sRdrLst.Length)
        SCardFreeMemory(&H0S, sRdrLst)          ' libera la memoria
        aRdrLst = Split(sRdrLst, vbNullChar)    ' memorizza tutti i nomi in un array
        For i = 0 To UBound(aRdrLst) - 1        ' cicla l'array
            ' rimuove eventuali caratteri non validi
            aRdrLst(i) = aRdrLst(i).Replace(vbNullChar, vbNullString)
            If aRdrLst(i).Length > 0 Then   ' se c'?un lettore
                c += 1                      ' incrementa il contatore
                ReDim Preserve Lst(c)       ' ridimensiona il buffer
                Lst(c) = aRdrLst(i)         ' memorizza nel buffer il nome del lettore
            End If
        Next
        ' restituisce la lista dei lettori
        Return Lst
    End Function

    Private Sub Hex2Byte(ByRef aDest() As Byte, ByRef sHexOrigin As String)
        Dim i As Integer
        Dim c As Integer

        c = 0
        ReDim aDest((sHexOrigin.Length / 2) - 1)
        For i = 1 To sHexOrigin.Length Step 2
            aDest(c) = CInt("&h" & Mid(sHexOrigin, i, 2))
            c += 1
        Next
    End Sub

    Public Function Hex2Str(ByRef sHex As String) As String
        Dim i As Integer
        Dim s As String
        s = vbNullString
        For i = 1 To sHex.Length Step 2
            s += Chr(CInt("&h" & Mid(sHex, i, 2)))
        Next
        Return s
    End Function

    Private Function ATRHex(ByVal pszATR As String) As String
        Dim pbAttr As String = String.Empty

        ' restituisce l'ATR in stringa HEX
        pszATR = pszATR.TrimEnd(vbNullChar)
        For i As Integer = 0 To pszATR.Length - 1
            pbAttr += Hex(Asc(pszATR.Chars(i))).PadLeft(2, "0") & Space(1)
        Next
        Return pbAttr.TrimEnd
    End Function

    Public Function HexToString(ByVal byteArray As Byte(), ByVal len As Integer) As String
        Dim StrBuilder As StringBuilder = New StringBuilder("")
        For column As Integer = 0 To len - 1
            StrBuilder.Append(String.Format("{0:X2} ", byteArray(column)))
        Next
        Return StrBuilder.ToString()
    End Function

    Public Function StringToHex(ByVal str As String, ByRef byteRet As Byte()) As Boolean
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
End Module
