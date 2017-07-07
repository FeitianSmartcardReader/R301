Imports System.Runtime.InteropServices

Public Class AlcorEMV
    Public Const DEFAULT_SLOT_NUM = 0
    Public Const ASYNCHRONOUS_CARD_MODE = 1
    Public Const I2C_CARD_MODE = 2
    Public Const SYNCHRONOUS_CARD_SLE4428_MODE = 3
    Public Const SYNCHRONOUS_CARD_SLE4442_MODE = 4
    Public Const AT88SC_CARD_MODE = 5
    Public Const INPHONE_CARD_MODE = 6
    Public Const AT45D041_CARD_MODE = 7
    Public Const SLE6636E_CARD_MODE = 8
    Public Const AT88SC102_CARD_MODE = 9
    Public Const SUPPORTED_MAX_EX_CARD_NUM = 10

    <DllImport("AlcorEMV.dll", EntryPoint:="Alcor_GetDllVersion", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function Alcor_GetDllVersion( _
    ByVal bBufLen As Int16, _
    <MarshalAsAttribute(UnmanagedType.U1)> ByRef pVersion As Byte) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="Alcor_SwitchCardMode", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function Alcor_SwitchCardMode( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bCardMode As Byte) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_Verify", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_Verify( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal lngPinLen As Byte, _
    ByVal pPinData As Byte()) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_UpdateSecurityMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_UpdateSecurityMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bAddress As Byte, _
    ByVal bData As Byte) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_ReadMainMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_ReadMainMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bAddress As Byte, _
    ByVal bReadLen As Byte, _
    <MarshalAsAttribute(UnmanagedType.U1)> ByRef pRecvdData As Byte, _
    <MarshalAsAttribute(UnmanagedType.U4)> ByRef pbReturnLen As Integer) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_UpdateMainMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_UpdateMainMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bAddress As Byte, _
    ByVal bData As Byte) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_ReadProtectionMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_ReadProtectionMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bReadLen As Byte, _
    <MarshalAsAttribute(UnmanagedType.U1)> ByRef pRecvdData As Byte, _
    <MarshalAsAttribute(UnmanagedType.U4)> ByRef pbReturnLen As Integer) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_WriteProtectionMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_WriteProtectionMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bAddress As Byte, _
    ByVal bData As Byte) _
    As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_ReadSecurityMemory", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_ReadSecurityMemory( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bReadLen As Byte, _
    <MarshalAsAttribute(UnmanagedType.U1)> ByRef pRecvdData As Byte, _
    <MarshalAsAttribute(UnmanagedType.U4)> ByRef pbReturnLen As Integer) _
As Integer
    End Function

    <DllImport("AlcorEMV.dll", EntryPoint:="SLE4442Cmd_CompareVerificationData", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
Public Shared Function SLE4442Cmd_CompareVerificationData( _
    ByVal lngCard As IntPtr, _
    ByVal bSlotNum As Byte, _
    ByVal bAddress As Byte, _
    ByVal bData As Byte) _
As Integer
    End Function
End Class
