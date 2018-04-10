///////////////////////////////////////////////////////////////////////////////
//
// COMPANY : FTsafe, LTD
//
// Delphi implementation of winscard.h
//
///////////////////////////////////////////////////////////////////////////////

unit FTsafeModule;

interface
uses Windows,classes,StdCtrls, SysUtils, Menus;


Const SCARD_ATR_LENGTH = 33;  // ISO 7816-3 spec.
Const SCARD_S_SUCCESS = 0;

Type  LONG = LongInt;
      SCARDCONTEXT = ULONG;
      SCARDHANDLE = ULONG;
      PSCARDCONTEXT = ^SCARDCONTEXT;
      LPSCARDCONTEXT = ^SCARDCONTEXT;

      PSCARDHANDLE = ^SCARDHANDLE;
      LPSCARDHANDLE = ^SCARDHANDLE;

      LPDWORD = ^DWORD;
      LPCVOID = Pointer;
      LPBYTE = PByte;
      LPBOOL = ^Boolean;


type  SCARD_IO_REQUEST = Record
        dwProtocol :DWORD;   // Protocol identifier
        cbPciLength :DWORD;  // Protocol Control Information Length
      end;
      PSCARD_IO_REQUEST = ^SCARD_IO_REQUEST;
      LPSCARD_IO_REQUEST = ^SCARD_IO_REQUEST;
      LPCSCARD_IO_REQUEST = ^SCARD_IO_REQUEST;

      LPVOID = ^POINTER;

type  SCARD_READERSTATE = record
      szReader :LPCTSTR;
      pvUserData : LPVOID;
      dwCurrentState :DWORD;
      dwEventStates :DWORD;
      cbATR :DWORD;
      rgbATR :array[1..36] of BYTE;
end;
      LPSCARD_READERSTATE = ^SCARD_READERSTATE;

Type  TAPDUCommand = record
      CLA: Byte;
      INS: Byte;
      P1: Byte;
      P2: Byte;
      P3: Byte;
      DataIn: array [0..255] of Byte;
      DataOut: array [0..255] of Byte;
      Status: array [0..1] of Byte;
end;

///////////////////////////////////////////////////////////////////////////////
//  Memory Card type constants
///////////////////////////////////////////////////////////////////////////////
Const  CT_MCU          = $00;          // MCU
Const  CT_IIC_Auto     = $01;          // IIC (Auto Detect Memory Size)
Const  CT_IIC_1K       = $02;          // IIC (1K)
Const  CT_IIC_2K       = $03;          // IIC (2K)
Const  CT_IIC_4K       = $04;          // IIC (4K)
Const  CT_IIC_8K       = $05;          // IIC (8K)
Const  CT_IIC_16K      = $06;          // IIC (16K)
Const  CT_IIC_32K      = $07;          // IIC (32K)
Const  CT_IIC_64K      = $08;          // IIC (64K)
Const  CT_IIC_128K     = $09;          // IIC (128K)
Const  CT_IIC_256K     = $0A;          // IIC (256K)
Const  CT_IIC_512K     = $0B;          // IIC (512K)
Const  CT_IIC_1024K    = $0C;          // IIC (1024K)
Const  CT_AT88SC153    = $0D;          // AT88SC153
Const  CT_AT88SC1608   = $0E;          // AT88SC1608
Const  CT_SLE4418      = $0F;          // SLE4418
Const  CT_SLE4428      = $10;          // SLE4428
Const  CT_SLE4432      = $11;          // SLE4432
Const  CT_SLE4442      = $12;          // SLE4442
Const  CT_SLE4406      = $13;          // SLE4406
Const  CT_SLE4436      = $14;          // SLE4436
Const  CT_SLE5536      = $15;          // SLE5536
Const  CT_MCUT0        = $16;          // MCU T=0
Const  CT_MCUT1        = $17;          // MCU T=1
Const  CT_MCU_Auto     = $18;          // MCU Autodetect

///////////////////////////////////////////////////////////////////////////////
//  Error Messages
///////////////////////////////////////////////////////////////////////////////
Const SCARD_F_INTERNAL_ERROR           = $80100001; //  An internal consistency check failed
Const SCARD_E_CANCELLED                = $80100002; //  The action was cancelled by an SCardCancel request
Const SCARD_E_INVALID_HANDLE           = $80100003; //  The supplied handle was invalid
Const SCARD_E_INVALID_PARAMETER        = $80100004; //  One or more of the supplied parameters could not be properly interpreted
Const SCARD_E_INVALID_TARGET           = $80100005; //  Registry startup information is missing or invalid
Const SCARD_E_NO_MEMORY                = $80100006; //  Not enough memory available to complete this command
Const SCARD_F_WAITED_TOO_LONG          = $80100007; //  An internal consistency timer has expired
Const SCARD_E_INSUFFICIENT_BUFFER      = $80100008; //  The data buffer to receive returned data is too small for the returned data
Const SCARD_E_UNKNOWN_READER           = $80100009; //  The specified reader name is not recognized
Const SCARD_E_TIMEOUT                  = $8010000A; //  The user-specified timeout value has expired
Const SCARD_E_SHARING_VIOLATION        = $8010000B; //  The smart card cannot be accessed because of other connections outstanding
Const SCARD_E_NO_SMARTCARD             = $8010000C; //  The operation requires a Smart Card, but no Smart Card is currently in the device
Const SCARD_E_UNKNOWN_CARD             = $8010000D; //  The specified smart card name is not recognized
Const SCARD_E_CANT_DISPOSE             = $8010000E; //  The system could not dispose of the media in the requested manner
Const SCARD_E_PROTO_MISMATCH           = $8010000F; //  The requested protocols are incompatible with the protocol currently in use with the smart card
Const SCARD_E_NOT_READY                = $80100010; //  The reader or smart card is not ready to accept commands
Const SCARD_E_INVALID_VALUE            = $80100011; //  One or more of the supplied parameters values could not be properly interpreted
Const SCARD_E_SYSTEM_CANCELLED         = $80100012; //  The action was cancelled by the system, presumably to log off or shut down
Const SCARD_F_COMM_ERROR               = $80100013; //  An internal communications error has been detected
Const SCARD_F_UNKNOWN_ERROR            = $80100014; //  An internal error has been detected, but the source is unknown
Const SCARD_E_INVALID_ATR              = $80100015; //  An ATR obtained from the registry is not a valid ATR string
Const SCARD_E_NOT_TRANSACTED           = $80100016; //  An attempt was made to end a non-existent transaction
Const SCARD_E_READER_UNAVAILABLE       = $80100017; //  The specified reader is not currently available for use
Const SCARD_P_SHUTDOWN                 = $80100018; //  PRIVATE -- Internal flag to force server termination
Const SCARD_E_PCI_TOO_SMALL            = $80100019; //  The PCI Receive buffer was too small
Const SCARD_E_READER_UNSUPPORTED       = $8010001A; //  The reader driver does not meet minimal requirements for support
Const SCARD_E_DUPLICATE_READER         = $8010001B; //  The reader driver did not produce a unique reader name
Const SCARD_E_CARD_UNSUPPORTED         = $8010001C; //  The smart card does not meet minimal requirements for support
Const SCARD_E_NO_SERVICE               = $8010001D; //  The Smart card resource manager is not running
Const SCARD_E_SERVICE_STOPPED          = $8010001E; //  The Smart card resource manager has shut down
Const SCARD_W_UNSUPPORTED_CARD         = $80100065; //  The reader cannot communicate with the smart card, due to ATR configuration conflicts
Const SCARD_W_UNRESPONSIVE_CARD        = $80100066; //  The smart card is not responding to a reset
Const SCARD_W_UNPOWERED_CARD           = $80100067; //  Power has been removed from the smart card, so that further communication is not possible
Const SCARD_W_RESET_CARD               = $80100068; //  The smart card has been reset, so any shared state information is invalid
Const SCARD_W_REMOVED_CARD             = $80100069; //  The smart card has been removed, so that further communication is not possible

///////////////////////////////////////////////////////////////////////////////
//      SHARE MODES
///////////////////////////////////////////////////////////////////////////////
Const SCARD_SCOPE_USER     =0;  // The context is a user context, and any
                                // database operations are performed within the
                                // domain of the user.
Const SCARD_SCOPE_TERMINAL =1;  // The context is that of the current terminal,
                                // and any database operations are performed
                                // within the domain of that terminal.  (The
                                // calling application must have appropriate
                                // access permissions for any database actions.)
Const SCARD_SCOPE_SYSTEM   =2;  // The context is the system context, and any
                                // database operations are performed within the
                                // domain of the system.  (The calling
                                // application must have appropriate access
                                // permissions for any database actions.)


///////////////////////////////////////////////////////////////////////////////
// Reader States
///////////////////////////////////////////////////////////////////////////////
Const  SCARD_UNKNOWN     =0;   // This value implies the driver is unaware
                              // of the current state of the reader.
Const  SCARD_ABSENT      =1;   // This value implies there is no card in
                              // the reader.
Const  SCARD_PRESENT     =2;   // This value implies there is a card is
                              // present in the reader, but that it has
                              // not been moved into position for use.
Const  SCARD_SWALLOWED   =3;   // This value implies there is a card in the
                              // reader in position for use.  The card is
                              // not powered.
Const  SCARD_POWERED     =4;   // This value implies there is power is
                              // being provided to the card, but the
                              // Reader Driver is unaware of the mode of
                              // the card.
Const SCARD_NEGOTIABLE  =5;   // This value implies the card has been
                              // reset and is awaiting PTS negotiation.
Const SCARD_SPECIFIC    =6;   // This value implies the card has been
                              // reset and specific communication
                              // protocols have been established.

///////////////////////////////////////////////////////////////////////////////
//  Card/Reader Access Services
///////////////////////////////////////////////////////////////////////////////
Const SCARD_SHARE_EXCLUSIVE =1; // This application is not willing to share this
                                // card with other applications.
Const SCARD_SHARE_SHARED    =2; // This application is willing to share this
                                // card with other applications.
Const SCARD_SHARE_DIRECT    =3; // This application demands direct control of
                                // the reader, so it is not available to other
                                // applications.

///////////////////////////////////////////////////////////////////////////////
//  Card Disposition
///////////////////////////////////////////////////////////////////////////////
Const SCARD_LEAVE_CARD     = 0; // Don't do anything special on close
Const SCARD_RESET_CARD     = 1; // Reset the card on close
Const SCARD_UNPOWER_CARD   = 2; // Power down the card on close
Const SCARD_EJECT_CARD     = 3; // Eject the card on close

///////////////////////////////////////////////////////////////////////////////
//  Protocol Flag definitions
///////////////////////////////////////////////////////////////////////////////
Const SCARD_PROTOCOL_UNDEFINED    =$00000000;  // There is no active protocol.
Const SCARD_PROTOCOL_T0           =$00000001;  // T=0 is the active protocol.
Const SCARD_PROTOCOL_T1           =$00000002;  // T=1 is the active protocol.
Const SCARD_PROTOCOL_RAW          =$00010000;  // Raw is the active protocol.
Const SCARD_PROTOCOL_DEFAULT      =$80000000;  // Use implicit PTS.


///////////////////////////////////////////////////////////////////////////////
//  Protocol Flag definitions
///////////////////////////////////////////////////////////////////////////////
Const SCARD_STATE_UNAWARE     =$00000000;   // The application is unaware of the
                                            // current state, and would like to
                                            // know.  The use of this value
                                            // results in an immediate return
                                            // from state transition monitoring
                                            // services.  This is represented by
                                            // all bits set to zero.
Const SCARD_STATE_IGNORE      =$00000001;   // The application requested that
                                            // this reader be ignored.  No other
                                            // bits will be set.
Const SCARD_STATE_CHANGED     =$00000002;   // This implies that there is a
                                            // difference between the state
                                            // believed by the application, and
                                            // the state known by the Service
                                            // Manager.  When this bit is set,
                                            // the application may assume a
                                            // significant state change has
                                            // occurred on this reader.
Const SCARD_STATE_UNKNOWN     =$00000004;   // This implies that the given
                                            // reader name is not recognized by
                                            // the Service Manager.  If this bit
                                            // is set, then SCARD_STATE_CHANGED
                                            // and SCARD_STATE_IGNORE will also
                                            // be set.
Const SCARD_STATE_UNAVAILABLE =$00000008;   // This implies that the actual
                                            // state of this reader is not
                                            // available.  If this bit is set,
                                            // then all the following bits are
                                            // clear.
Const SCARD_STATE_EMPTY       =$00000010;   // This implies that there is not
                                            // card in the reader.  If this bit
                                            // is set, all the following bits
                                            // will be clear.
Const SCARD_STATE_PRESENT     =$00000020;   // This implies that there is a card
                                            // in the reader.
Const SCARD_STATE_ATRMATCH    =$00000040;   // This implies that there is a card
                                            // in the reader with an ATR
                                            // matching one of the target cards.
                                            // If this bit is set,
                                            // SCARD_STATE_PRESENT will also be
                                            // set.  This bit is only returned
                                            // on the SCardLocateCard() service.
Const SCARD_STATE_EXCLUSIVE   =$00000080;   // This implies that the card in the
                                            // reader is allocated for exclusive
                                            // use by another application.  If
                                            // this bit is set,
                                            // SCARD_STATE_PRESENT will also be
                                            // set.
Const SCARD_STATE_INUSE       =$00000100;   // This implies that the card in the
                                            // reader is in use by one or more
                                            // other applications, but may be
                                            // connected to in shared mode.  If
                                            // this bit is set,
                                            // SCARD_STATE_PRESENT will also be
                                            // set.
Const SCARD_STATE_MUTE       =$00000200;    // This implies that the card in the
                                            // reader is unresponsive or not
                                            // supported by the reader or
                                            // software.
Const SCARD_STATE_UNPOWERED   =$00000400;   // This implies that the card in the
                                            // reader has not been powered up.

///////////////////////////////////////////////////////////////////////////////
//  SCardControl Constants
///////////////////////////////////////////////////////////////////////////////
Const IOCTL_SMARTCARD_DIRECT            =(($31 shl 16) + (2050 shl 2));
Const IOCTL_SMARTCARD_SELECT_SLOT       =(($31 shl 16) + (2051 shl 2));
Const IOCTL_SMARTCARD_DRAW_LCDBMP       =(($31 shl 16) + (2052 shl 2));
Const IOCTL_SMARTCARD_DISPLAY_LCD       =(($31 shl 16) + (2053 shl 2));
Const IOCTL_SMARTCARD_CLR_LCD           =(($31 shl 16) + (2054 shl 2));
Const IOCTL_SMARTCARD_READ_KEYPAD       =(($31 shl 16) + (2055 shl 2));
Const IOCTL_SMARTCARD_READ_RTC          =(($31 shl 16) + (2057 shl 2));
Const IOCTL_SMARTCARD_SET_RTC           =(($31 shl 16) + (2058 shl 2));
Const IOCTL_SMARTCARD_SET_OPTION        =(($31 shl 16) + (2059 shl 2));
Const IOCTL_SMARTCARD_SET_LED           =(($31 shl 16) + (2060 shl 2));
Const IOCTL_SMARTCARD_LOAD_KEY          =(($31 shl 16) + (2062 shl 2));
Const IOCTL_SMARTCARD_READ_EEPROM       =(($31 shl 16) + (2065 shl 2));
Const IOCTL_SMARTCARD_WRITE_EEPROM      =(($31 shl 16) + (2066 shl 2));
Const IOCTL_SMARTCARD_GET_VERSION       =(($31 shl 16) + (2067 shl 2));
Const IOCTL_SMARTCARD_GET_READER_INFO		=(($31 shl 16) + (2051 shl 2));
Const IOCTL_SMARTCARD_SET_CARD_TYPE 		=(($31 shl 16) + (2060 shl 2));

///////////////////////////////////////////////////////////////////////////////
//  Imported functions from Winscard.dll (WIN32 API)
///////////////////////////////////////////////////////////////////////////////
Function SCardEstablishContext(dwscope :DWORD;
                                pvReserved1: LPCVOID;
                                pvReserved2: LPCVOID;
                                phContext :LPSCARDCONTEXT):LONG; stdcall; external 'Winscard.dll';

Function SCardReleaseContext(hContext:SCARDCONTEXT):LONG; stdcall; external 'Winscard.dll';

Function SCardListReadersA(hContext : SCARDCONTEXT;
                           mszGroups:LPCSTR;
                           szReaders:LPSTR;
                           pcchReaders:LPDWORD):LONG; stdcall; external 'Winscard.dll';

//Note : ScardConnectA is for non-UNICODE characters which is only one byte.
//       For UNICODE characters it is SCardConnectW. Special processing is
//       required for UNICODE. Be careful!
Function SCardConnectA(hContext : SCARDCONTEXT;
                       szReaders:LPSTR;
                       dwShareMode : DWORD;
                       dwPreferredProtocols : DWORD;
                       phCard : LPSCARDHANDLE;
                       pdwActiveProtocols:LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardReconnect(hCard : SCARDHANDLE;
                        dwShareMode : DWORD;
                        dwPreferredProtocols : DWORD;
                        dwInitialization : DWORD;
                        pdwActiveProtocols:LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardDisconnect(hCard : SCARDHANDLE;
                         dwDisposition :DWORD):LONG; stdcall; external 'Winscard.dll';


Function SCardBeginTransaction(hCard : SCARDHANDLE):LONG; stdcall; external 'Winscard.dll';

Function SCardEndTransaction(hCard : SCARDHANDLE;
                             dwDisposition :DWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardState(hCard : SCARDHANDLE;
                    pdwState :LPDWORD;
                    pdwProtocol :LPDWORD;
                    pbATR :LPBYTE;
                    pcbAtrLen :LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardGetStatusChangeA(hContext : SCARDHANDLE;
                    dwTimeout :DWORD;
                    rgReaderStates :LPSCARD_READERSTATE;
                    cReaders :DWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardStatusA(hCard : SCARDHANDLE;
                      mszReaderNames :LPTSTR;
                      pcchReaderLen : LPDWORD;
                      pdwState :LPDWORD;
                      pdwProtocol :LPDWORD;
                      pbATR :LPBYTE;
                      pcbAtrLen :LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardTransmit(hCard : SCARDHANDLE;
                       pioSendPci : LPCSCARD_IO_REQUEST;
                       pbSendBuffer : LPBYTE;
                       cbSendLength : DWORD;
                       pioRecvPci : LPCSCARD_IO_REQUEST;
                       pbRecvBuffer : LPBYTE;
                       pcbRecvLength:LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function SCardControl(hCard : SCARDHANDLE;
                       dwControlCode : DWORD;
                       lpInBuffer : LPBYTE;
                       nInBufferSize : DWORD;
                       lpOutBuffer : LPBYTE;
                       nOutBufferSize : DWORD;
                       lpBytesReturned : LPDWORD):LONG; stdcall; external 'Winscard.dll';

Function ShellExecuteA(handle : DWORD;
                       lpOperation : String;
                       lpTargetFile : String;
                       lpParam : String;
                       lpDirectory : String;
                       ShowMode : Integer):LONG; stdcall; external 'SHELL32.dll';


Procedure ParseReaderList(var List : TStringList; Buffer :PChar; BuffLen : integer);
Procedure LoadListToControl(var ComboBoxControl : TComboBox; Buffer :PChar; BuffLen : integer);
Procedure LoadListToMenu(var MenuItemControl : TMenuItem; Buffer :PChar; BuffLen : integer);
Function GetScardErrMsg(ErrCode : DWORD):string;
Function GetShareModeStrMsg(Code : DWORD):string;
Function GetReaderStateStrMsg(Code : DWORD):string;
Function GetDisposistionStrMsg(Code : DWORD):string;
Function GetProtocolStrMsg(Code : DWORD):string;
Function GetProtocolFlagStrMsg(Code : DWORD):string;


implementation

/////////////////////////////////////////////////////////////////////////////
//  Procedure   : ParseReaderList
//  Inputs      : TStringList - Resulting list of the parsed data.
//                Buffer - should contain readerlist separated by Null character.
//                        List is terminated by Null.
//                BuffLen - Length of Buffer including Null character(s).
//  Description : This function parses readerlist into into individual
//                readers. Buffer should contain the result after a
//                SCardReaderList function. TStringList must be created
//                before passing it to the routine. BuffLen should contain
                //                the number of characters contained in Buffer.
//
//                Example :  Procedure SomeProcedure();
//                           var List :TStringList;
//                           Buffer : array[0..255] of char;
//                           begin
//                               Buffer := 'Ftsafe READER 1'#0#0; {Len is 14, include 2 null at the end}
//                               List := TStringList.Create;
//                               ParseReaderList(List,Buffer,14);
//                               ComboBoxControl.Items.Assign(List); {Assign content of List to Combobox}
//                               List.Free;
//                           end;
////////////////////////////////////////////////////////////////////////////
procedure ParseReaderList(var List : TStringList; Buffer :PChar; BuffLen : integer);
var indx : integer;
    sReader:string;
begin
     indx := 0;
     while (Buffer[indx] <> #0) do begin
       sReader := '';
       while (Buffer[indx] <> #0) do begin
       sReader := sReader + Buffer[indx];
       inc(indx);
       end; // while loop
       sReader := sReader + Buffer[indx];
       List.Add(sReader);
       inc(indx);
     end; // while loop
end;

/////////////////////////////////////////////////////////////////////////////
//  Procedure   : LoadListToControl
//  Inputs      : ComboBox - Dropdownlist box where list is loaded.
//                Buffer - should contain readerlist separated by Null character.
//                        List is terminated by Null.
//                BuffLen - Length of Buffer including Null character(s).
//  Description : This function loads the reader list in the combobox control.
//                Note : that previous entries to the control prior to the calling
//                       of this routine will clear the entries.
//                Example :  Procedure SomeProcedure();
//                           var Buffer : array[0..255] of char;
//                           begin
//                               Buffer := 'FTsafe READER 1'#0#0; {Len is 14, include 2 null at the end}
//                               LoadListToControl(ComboBox1,Buffer,14);
//                           end;
////////////////////////////////////////////////////////////////////////////

Procedure LoadListToControl(var ComboBoxControl : TComboBox; Buffer :PChar; BuffLen : integer);
var List : TStringList;
begin
     List := TStringList.Create;
     ParseReaderList(List,Buffer,BuffLen);
     ComboBoxControl.Clear;
     ComboBoxControl.Items.Assign(List);
     List.Free;
end;

/////////////////////////////////////////////////////////////////////////////
//  Procedure   : LoadListToMenu
//  Inputs      : MenuItem - Menu Item where list is loaded.
//                Buffer - should contain readerlist separated by Null character.
//                        List is terminated by Null.
//                BuffLen - Length of Buffer including Null character(s).
//  Description : This function loads the reader list in the menu item control.
//                Note : that previous entries to the control prior to the calling
//                       of this routine will clear the entries.
//                Example :  Procedure SomeProcedure();
//                           var Buffer : array[0..255] of char;
//                           begin
//                               Buffer := 'FTsafe READER 1'#0#0; {Len is 14, include 2 null at the end}
//                               LoadListToMenu(MenuItem1,Buffer,14);
//                           end;
////////////////////////////////////////////////////////////////////////////

Procedure LoadListToMenu(var MenuItemControl : TMenuItem; Buffer :PChar; BuffLen : integer);
var NewItem: TMenuItem;
    I : integer;
    List : TStringList;
begin
     List := TStringList.Create;
     ParseReaderList(List,Buffer,BuffLen);
     for  I := 0 to List.Count-1 do
     begin
       NewItem := TMenuItem.Create(NewItem);
       NewItem.Caption := List.Strings[I];
       NewItem.AutoCheck := True;
       NewItem.RadioItem := True;
       MenuItemControl.Add(NewItem);
     end;

end;


////////////////////////////////////////////////////////////////////////////
//  Function    : GetScardErrMsg
//  Inputs      : ErrCode - DWORD code.
//  Description : This function returns the string message of the
//                error code.
//
////////////////////////////////////////////////////////////////////////////
Function GetScardErrMsg(ErrCode : DWORD):string;
begin
    Case ErrCode of
      SCARD_F_INTERNAL_ERROR       : GetScardErrMsg := 'An internal consistency check failed.';
      SCARD_E_CANCELLED            : GetScardErrMsg := 'The action was cancelled by an SCardCancel request.';
      SCARD_E_INVALID_HANDLE       : GetScardErrMsg := 'The supplied handle was invalid.';
      SCARD_E_INVALID_PARAMETER    : GetScardErrMsg := 'One or more of the supplied parameters could not be properly interpreted.';
      SCARD_E_INVALID_TARGET       : GetScardErrMsg := 'Registry startup information is missing or invalid.';
      SCARD_E_NO_MEMORY            : GetScardErrMsg := 'Not enough memory available to complete this command.';
      SCARD_F_WAITED_TOO_LONG      : GetScardErrMsg := 'An internal consistency timer has expired.';
      SCARD_E_INSUFFICIENT_BUFFER  : GetScardErrMsg := 'The data buffer to receive returned data is too small for the returned data.';
      SCARD_E_UNKNOWN_READER       : GetScardErrMsg := 'The specified reader name is not recognized.';
      SCARD_E_TIMEOUT              : GetScardErrMsg := 'The user-specified timeout value has expired.';
      SCARD_E_SHARING_VIOLATION    : GetScardErrMsg := 'The smart card cannot be accessed because of other connections outstanding.';
      SCARD_E_NO_SMARTCARD         : GetScardErrMsg := 'The operation requires a Smart Card, but no Smart Card is currently in the device.';
      SCARD_E_UNKNOWN_CARD         : GetScardErrMsg := 'The specified smart card name is not recognized.';
      SCARD_E_CANT_DISPOSE         : GetScardErrMsg := 'The system could not dispose of the media in the requested manner.';
      SCARD_E_PROTO_MISMATCH       : GetScardErrMsg := 'The requested protocols are incompatible with the protocol currently in use with the smart card.';
      SCARD_E_NOT_READY            : GetScardErrMsg := 'The reader or smart card is not ready to accept commands.';
      SCARD_E_INVALID_VALUE        : GetScardErrMsg := 'One or more of the supplied parameters values could not be properly interpreted.';
      SCARD_E_SYSTEM_CANCELLED     : GetScardErrMsg := 'The action was cancelled by the system, presumably to log off or shut down.';
      SCARD_F_COMM_ERROR           : GetScardErrMsg := 'An internal communications error has been detected.';
      SCARD_F_UNKNOWN_ERROR        : GetScardErrMsg := 'An internal error has been detected, but the source is unknown.';
      SCARD_E_INVALID_ATR          : GetScardErrMsg := 'An ATR obtained from the registry is not a valid ATR string.';
      SCARD_E_NOT_TRANSACTED       : GetScardErrMsg := 'An attempt was made to end a non-existent transaction.';
      SCARD_E_READER_UNAVAILABLE   : GetScardErrMsg := 'The specified reader is not currently available for use.';
      SCARD_P_SHUTDOWN             : GetScardErrMsg := 'PRIVATE -- Internal flag to force server termination.';
      SCARD_E_PCI_TOO_SMALL        : GetScardErrMsg := 'The PCI Receive buffer was too small.';
      SCARD_E_READER_UNSUPPORTED   : GetScardErrMsg := 'The reader driver does not meet minimal requirements for support.';
      SCARD_E_DUPLICATE_READER     : GetScardErrMsg := 'The reader driver did not produce a unique reader name.';
      SCARD_E_CARD_UNSUPPORTED     : GetScardErrMsg := 'The smart card does not meet minimal requirements for support.';
      SCARD_E_NO_SERVICE           : GetScardErrMsg := 'The Smart card resource manager is not running.';
      SCARD_E_SERVICE_STOPPED      : GetScardErrMsg := 'The Smart card resource manager has shut down.';
      SCARD_W_UNSUPPORTED_CARD     : GetScardErrMsg := 'The reader cannot communicate with the smart card, due to ATR configuration conflicts.';
      SCARD_W_UNRESPONSIVE_CARD    : GetScardErrMsg := 'The smart card is not responding to a reset.';
      SCARD_W_UNPOWERED_CARD       : GetScardErrMsg := 'Power has been removed from the smart card, so that further communication is not possible.';
      SCARD_W_RESET_CARD           : GetScardErrMsg := 'The smart card has been reset, so any shared state information is invalid.';
      SCARD_W_REMOVED_CARD         : GetScardErrMsg := 'The smart card has been removed, so that further communication is not possible.';
    else
      GetScardErrMsg := 'Unknown Error: ' + Format('%d', [ErrCode]);
    end; // case statement
end;

////////////////////////////////////////////////////////////////////////////
//   GetShareModeStrMsg - displays the meaning or gives a brief description
////////////////////////////////////////////////////////////////////////////
Function GetShareModeStrMsg(Code : DWORD):string;
begin
   case Code of
SCARD_SCOPE_USER     : GetShareModeStrMsg := 'The context is a user context, and any database operations are performed within the domain of the user.';
SCARD_SCOPE_TERMINAL : GetShareModeStrMsg := 'The context is that of the current terminal, and any database operations are performed within the domain of that terminal.  (The calling application must have appropriate access permissions for any database actions.)';
SCARD_SCOPE_SYSTEM   : GetShareModeStrMsg := 'The context is the system context, and any database operations are performed within the domain of the system.  (The calling application must have appropriate access permissions for any database actions.)';
    end; // case statement
end;


////////////////////////////////////////////////////////////////////////////
//   GetReaderStateStrMsg - displays the meaning or gives a brief description
////////////////////////////////////////////////////////////////////////////
Function GetReaderStateStrMsg(Code : DWORD):string;
begin
   case Code of
SCARD_UNKNOWN    : GetReaderStateStrMsg := 'This value implies the driver is unaware of the current state of the reader.';
SCARD_ABSENT     : GetReaderStateStrMsg := 'This value implies there is no card in the reader.';
SCARD_PRESENT    : GetReaderStateStrMsg := 'This value implies there is a card is present in the reader, but that it has not been moved into position for use.';
SCARD_SWALLOWED  : GetReaderStateStrMsg := 'This value implies there is a card in the reader in position for use.  The card is not powered.';
SCARD_POWERED    : GetReaderStateStrMsg := 'This value implies there is power is being provided to the card, but the Reader Driver is unaware of the mode of the card.';
SCARD_NEGOTIABLE : GetReaderStateStrMsg := 'This value implies the card has been reset and is awaiting PTS negotiation.';
SCARD_SPECIFIC   : GetReaderStateStrMsg := 'This value implies the card has been reset and specific communication protocols have been established.';
    end; // case statement
end;


////////////////////////////////////////////////////////////////////////////
//   GetDisposistionStrMsg - displays the meaning or gives a brief description
////////////////////////////////////////////////////////////////////////////
Function GetDisposistionStrMsg(Code : DWORD):string;
begin
   case Code of
SCARD_LEAVE_CARD     : GetDisposistionStrMsg := 'Don''t do anything special on close';
SCARD_RESET_CARD     : GetDisposistionStrMsg := 'Reset the card on close';
SCARD_UNPOWER_CARD   : GetDisposistionStrMsg := 'Power down the card on close';
SCARD_EJECT_CARD     : GetDisposistionStrMsg := 'Eject the card on close';
    end; // case statement
end;


////////////////////////////////////////////////////////////////////////////
//   GetProtocolStrMsg - displays the meaning or gives a brief description
////////////////////////////////////////////////////////////////////////////
Function GetProtocolStrMsg(Code : DWORD):string;
begin
   case Code of
SCARD_PROTOCOL_UNDEFINED    :GetProtocolStrMsg := 'There is no active protocol.';
SCARD_PROTOCOL_T0           :GetProtocolStrMsg := 'T=0 is the active protocol.';
SCARD_PROTOCOL_T1           :GetProtocolStrMsg := 'T=1 is the active protocol.';
SCARD_PROTOCOL_RAW          :GetProtocolStrMsg := 'Raw is the active protocol.';
SCARD_PROTOCOL_DEFAULT      :GetProtocolStrMsg := 'Use implicit PTS.';
    end; // case statement
end;

////////////////////////////////////////////////////////////////////////////
//   GetProtocolFlagStrMsg - displays the meaning or gives a brief description
////////////////////////////////////////////////////////////////////////////
Function GetProtocolFlagStrMsg(Code : DWORD):string;
begin
   case Code of
SCARD_STATE_UNAWARE    :GetProtocolFlagStrMsg := 'The application is unaware of the current state, and would like to know.  The use of this value results in an immediate return from state transition monitoring services.  This is represented by all bits set to zero.';
SCARD_STATE_IGNORE     :GetProtocolFlagStrMsg := 'The application requested that this reader be ignored.  No other bits will be set.';
SCARD_STATE_CHANGED    :GetProtocolFlagStrMsg := 'This implies that there is a difference between the state believed by the application, and the state known by the Service Manager.  When this bit is set, the application may assume a significant state change has occurred on this reader.';
SCARD_STATE_UNKNOWN    :GetProtocolFlagStrMsg := 'This implies that the given reader name is not recognized by the Service Manager.  If this bit is set, then SCARD_STATE_CHANGED and SCARD_STATE_IGNORE will also be set.';
SCARD_STATE_UNAVAILABLE:GetProtocolFlagStrMsg := 'This implies that the actual state of this reader is not available.  If this bit is set, then all the following bits are clear.';
SCARD_STATE_EMPTY      :GetProtocolFlagStrMsg := 'This implies that there is not card in the reader.  If this bit is set, all the following bits will be clear.';
SCARD_STATE_PRESENT    :GetProtocolFlagStrMsg := 'This implies that there is a card in the reader.';
SCARD_STATE_ATRMATCH   :GetProtocolFlagStrMsg := 'This implies that there is a card in the reader with an ATR matching one of the target cards. If this bit is set, SCARD_STATE_PRESENT will also be set.  This bit is only returned on the SCardLocateCard() service.';
SCARD_STATE_EXCLUSIVE  :GetProtocolFlagStrMsg := 'This implies that the card in the reader is allocated for exclusive use by another application.  If this bit is set, SCARD_STATE_PRESENT will also be set.';
SCARD_STATE_INUSE      :GetProtocolFlagStrMsg := 'This implies that the card in the reader is in use by one or more other applications, but may be connected to in shared mode.  If this bit is set, SCARD_STATE_PRESENT will also be set.';
SCARD_STATE_MUTE       :GetProtocolFlagStrMsg := 'This implies that the card in the reader is unresponsive or not supported by the reader or software.';
SCARD_STATE_UNPOWERED  :GetProtocolFlagStrMsg := 'This implies that the card in the reader has not been powered up.';
    end; // case statement
end;


end.







