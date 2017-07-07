Public Class WinScard
    ' Always return lResult as Int32 for x86/x64 compatibility.

    ' SCardConnect
    Public Const SCARD_S_SUCCESS = 0
    Public Const SCARD_LEAVE_CARD = 0
    Public Const SCARD_SHARE_DIRECT = 3
    Public Const IOCTL_SMARTCARD_POWER = 3211268
    Public Const SCARD_ATTR_CURRENT_PROTOCOL_TYPE = 524801
    Public Const SCARD_POWER_DOWN = 0
    Public Const SCARD_COLD_RESET = 1
    Public Const SCARD_WARM_RESET = 2
    Public Const SCARD_SCOPE_SYSTEM = 2
    Declare Ansi Function SCardConnect Lib "WinScard.dll" Alias "SCardConnectA" ( _
            ByVal hContext As IntPtr, _
            ByVal szReaderName As String, _
            ByVal dwShareMode As Int32, _
            ByVal dwPrefProtocol As Int32, _
            ByRef hCard As IntPtr, _
            ByRef ActiveProtocol As Int32) As Int32

    'SCardDisconnect
    Declare Ansi Function SCardDisconnect Lib "WinScard.dll" ( _
            ByVal hCard As IntPtr, _
            ByVal Disposition As Integer) As Int32

    'SCardEstablishContext (x86/x64 checked - dwScope as UInt32 / Return Int32)
    Declare Ansi Function SCardEstablishContext Lib "WinScard.dll" ( _
            ByVal dwScope As UInt32, _
            ByVal pvReserved1 As Integer, _
            ByVal pvReserved2 As Integer, _
            ByRef phContext As IntPtr) As Int32

    'ScardGetAttrib
    Declare Ansi Function SCardGetAttrib Lib "WinScard.dll" ( _
            ByVal hCard As IntPtr, _
            ByVal AttrId As Int32, _
            ByVal RecvBuff As Byte(), _
            ByRef RecvBuffLen As Integer) As Int32

    'SCardListReaders
    Declare Ansi Function SCardListReaders Lib "WinScard.dll" Alias "SCardListReadersA" ( _
            ByVal hContext As IntPtr, _
            ByVal mzGroup As String, _
            ByVal ReaderList As String, _
            ByRef pcchReaders As Integer) As Int32

    'SCardReleaseContext
    Declare Ansi Function SCardReleaseContext Lib "WinScard.dll" ( _
            ByVal hContext As IntPtr) As Int32

    'SCardStatus
    Declare Ansi Function SCardStatus Lib "WinScard.dll" Alias "SCardStatusA" ( _
            ByVal hCard As IntPtr, _
            ByVal szReaderName As String, _
            ByRef pcchReaderLen As Integer, _
            ByRef State As Integer, _
            ByRef Protocol As Int32, _
            ByVal ATR As Byte(), _
            ByRef ATRLen As Integer) As Int32

    'SCardTransmit
    Declare Ansi Function SCardTransmit Lib "WinScard.dll" ( _
            ByVal hCard As IntPtr, _
            ByVal pioSendRequest As Integer, _
            ByVal sendbuff As Byte(), _
            ByVal SendBuffLen As Integer, _
            ByVal pioRecvRequest As Integer, _
            ByVal RecvBuff As Byte(), _
            ByRef RecvBuffLen As Integer) As Int32

    'SCardControl
    Declare Ansi Function SCardControl Lib "WinScard.dll" ( _
            ByVal hCard As IntPtr, _
            ByVal dwControlCode As Integer, _
            ByVal sendbuff As Byte(), _
            ByVal SendBuffLen As Integer, _
            ByVal RecvBuff As Byte(), _
            ByVal RecvBuffLen As Integer, _
            ByRef RetBuffLen As Integer) As Int32
End Class
