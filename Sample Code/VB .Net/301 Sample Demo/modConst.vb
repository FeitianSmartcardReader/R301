Option Strict Off
Option Explicit On

Module modConsts

    ' Tipo di contesto (SCardEstabilishContext, dwScope)
    Public Const SCARD_AUTOALLOCATE As Short = -1
    Public Const SCARD_SCOPE_USER As Short = 0
    Public Const SCARD_SCOPE_TERMINAL As Short = 1
    Public Const SCARD_SCOPE_SYSTEM As Short = 2

    Public Const SCARD_STATE_UNAWARE As Short = 0
    Public Const SCARD_STATE_IGNORE As Short = 1
    Public Const SCARD_STATE_CHANGED As Short = 2
    Public Const SCARD_STATE_UNKNOWN As Short = 4
    Public Const SCARD_STATE_UNAVAILABLE As Short = 8
    Public Const SCARD_STATE_EMPTY As Short = &H10S
    Public Const SCARD_STATE_PRESENT As Short = &H20S
    Public Const SCARD_STATE_ATRMATCH As Short = &H40S
    Public Const SCARD_STATE_EXCLUSIVE As Short = &H80S
    Public Const SCARD_STATE_INUSE As Short = &H100S
    Public Const SCARD_STATE_MUTE As Short = &H200S
    Public Const SCARD_STATE_UNPOWERED As Short = &H400S

    ' Lunghezza massima dell'ATR
    Public Const SCARD_ATR_LENGTH As Short = 32

    ' Massimo numeri di lettori (SCardLocateCards)
    Public Const MAXIMUM_SMARTCARD_READERS As Short = 10

    ' stati del lettore/carta (SCardStatus, pdwState)
    Public Const SCARD_UNKNOWN As Short = 0
    Public Const SCARD_ABSENT As Short = 1
    Public Const SCARD_PRESENT As Short = 2
    Public Const SCARD_SWALLOWED As Short = 3
    Public Const SCARD_POWERED As Short = 4
    Public Const SCARD_NEGOTIABLE As Short = 5
    Public Const SCARD_SPECIFIC As Short = 6

    ' tipi di protocollo (SCardStatus, pdwProtocol)
    Public Const SCARD_PROTOCOL_UNDEFINED As Short = 0
    Public Const SCARD_PROTOCOL_T0 As Short = 1
    Public Const SCARD_PROTOCOL_T1 As Short = 2
    Public Const SCARD_PROTOCOL_RAW As Integer = &H10000
    Public Const SCARD_PROTOCOL_DEFAULT As Integer = &H80000000

    ' metodi di disconnessione (SCardDisconnect, dwDisposition)
    Public Const SCARD_LEAVE_CARD As Short = 1
    Public Const SCARD_RESET_CARD As Short = 2
    Public Const SCARD_UNPOWER_CARD As Short = 3
    Public Const SCARD_EJECT_CARD As Short = 4

    ' metodi di accesso (SCardEstablishContext, dwScope)
    Public Const SCARD_SHARE_EXCLUSIVE As Short = 1
    Public Const SCARD_SHARE_SHARED As Short = 2
    Public Const SCARD_SHARE_DIRECT As Short = 3

    ' valori statusword generici
    Public Const SWORD_E_SUCCESS As Integer = &H9000S
    Public Const SWORD_E_INVALID_CSC As Integer = &H6300S

    ' codici di errori Winscard
    Public Const SCARD_NO_ERROR As Integer = &H0S
    Public Const SCARD_S_SUCCESS As Integer = &H0S
    Public Const SCARD_F_INTERNAL_ERROR As Integer = &H80100001
    Public Const SCARD_E_CANCELLED As Integer = &H80100002
    Public Const SCARD_E_INVALID_HANDLE As Integer = &H80100003
    Public Const SCARD_E_INVALID_PARAMETER As Integer = &H80100004
    Public Const SCARD_E_INVALID_TARGET As Integer = &H80100005
    Public Const SCARD_E_NO_MEMORY As Integer = &H80100006
    Public Const SCARD_F_WAITED_TOO_LONG As Integer = &H80100007
    Public Const SCARD_E_INSUFFICIENT_BUFFER As Integer = &H80100008
    Public Const SCARD_E_UNKNOWN_READER As Integer = &H80100009
    Public Const SCARD_E_TIMEOUT As Integer = &H8010000A
    Public Const SCARD_E_SHARING_VIOLATION As Integer = &H8010000B
    Public Const SCARD_E_NO_SMARTCARD As Integer = &H8010000C
    Public Const SCARD_E_UNKNOWN_CARD As Integer = &H8010000D
    Public Const SCARD_E_CANT_DISPOSE As Integer = &H8010000E
    Public Const SCARD_E_PROTO_MISMATCH As Integer = &H8010000F
    Public Const SCARD_E_NOT_READY As Integer = &H80100010
    Public Const SCARD_E_INVALID_VALUE As Integer = &H80100011
    Public Const SCARD_E_SYSTEM_CANCELLED As Integer = &H80100012
    Public Const SCARD_F_COMM_ERROR As Integer = &H80100013
    Public Const SCARD_F_UNKNOWN_ERROR As Integer = &H80100014
    Public Const SCARD_E_INVALID_ATR As Integer = &H80100015
    Public Const SCARD_E_NOT_TRANSACTED As Integer = &H80100016
    Public Const SCARD_E_READER_UNAVAILABLE As Integer = &H80100017
    Public Const SCARD_P_SHUTDOWN As Integer = &H80100018
    Public Const SCARD_E_PCI_TOO_SMALL As Integer = &H80100019
    Public Const SCARD_E_READER_UNSUPPORTED As Integer = &H8010001A
    Public Const SCARD_E_DUPLICATE_READER As Integer = &H8010001B
    Public Const SCARD_E_CARD_UNSUPPORTED As Integer = &H8010001C
    Public Const SCARD_E_NO_SERVICE As Integer = &H8010001D
    Public Const SCARD_E_SERVICE_STOPPED As Integer = &H8010001E
    Public Const SCARD_E_UNEXPECTED As Integer = &H8010001F
    Public Const SCARD_E_ICC_INSTALLATION As Integer = &H80100020
    Public Const SCARD_E_ICC_CREATEORDER As Integer = &H80100021
    Public Const SCARD_E_UNSUPPORTED_FEATURE As Integer = &H80100022
    Public Const SCARD_E_DIR_NOT_FOUND As Integer = &H80100023
    Public Const SCARD_E_FILE_NOT_FOUND As Integer = &H80100024
    Public Const SCARD_E_NO_DIR As Integer = &H80100025
    Public Const SCARD_E_NO_FILE As Integer = &H80100026
    Public Const SCARD_E_NO_ACCESS As Integer = &H80100027
    Public Const SCARD_E_WRITE_TOO_MANY As Integer = &H80100028
    Public Const SCARD_E_BAD_SEEK As Integer = &H80100029
    Public Const SCARD_E_INVALID_CHV As Integer = &H8010002A
    Public Const SCARD_E_UNKNOWN_RES_MNG As Integer = &H8010002B
    Public Const SCARD_E_NO_SUCH_CERTIFICATE As Integer = &H8010002C
    Public Const SCARD_E_CERTIFICATE_UNAVAILABLE As Integer = &H8010002D
    Public Const SCARD_E_NO_READERS_AVAILABLE As Integer = &H8010002E
    Public Const SCARD_E_COMM_DATA_LOST As Integer = &H8010002F
    Public Const SCARD_E_NO_KEY_CONTAINER As Integer = &H80100030
    Public Const SCARD_E_SERVER_TOO_BUSY As Integer = &H80100031
    Public Const SCARD_W_UNSUPPORTED_CARD As Integer = &H80100065
    Public Const SCARD_W_UNRESPONSIVE_CARD As Integer = &H80100066
    Public Const SCARD_W_UNPOWERED_CARD As Integer = &H80100067
    Public Const SCARD_W_RESET_CARD As Integer = &H80100068
    Public Const SCARD_W_REMOVED_CARD As Integer = &H80100069
    Public Const SCARD_W_SECURITY_VIOLATION As Integer = &H8010006A
    Public Const SCARD_W_WRONG_CHV As Integer = &H8010006B
    Public Const SCARD_W_CHV_BLOCKED As Integer = &H8010006C
    Public Const SCARD_W_EOF As Integer = &H8010006D
    Public Const SCARD_W_CANCELLED_BY_USER As Integer = &H8010006E
End Module