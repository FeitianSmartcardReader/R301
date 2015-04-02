// Simple_PCSCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Simple_PCSC.h"
#include "Simple_PCSCDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

SCARDCONTEXT		ContextHandle; // Context
SCARDHANDLE			CardHandle;    // Handle of SCReader
BYTE	pResponseBuffer[MAX_RESPONSE];
ULONG	ResponseLength;
CSimple_PCSCDlg* pThis = NULL;
SCARD_IO_REQUEST    IO_REQ;

///////////////////////////////////////////////////////////////////////////////
// hex to asc: 0x22 -> "22"
int Hex2Asc(char *Dest,char *Src,int SrcLen)
{
	int i;
	for ( i = 0; i < SrcLen; i ++ )
	{
		sprintf(Dest + i * 2,"%02X",(unsigned char)Src[i]);
	}
	Dest[i * 2] = 0;
	return TRUE;
}

///////////////////////////////////////////////////////////////////////////////
// asc to hex: "22" -> 0x22
int Asc2Hex(char *Dest,char *Src,int SrcLen)
{
	int i;
	for ( i = 0; i < SrcLen / 2; i ++ )
	{
		sscanf(Src + i * 2,"%02X",(unsigned char *)&Dest[i]);
	}
	return TRUE;
}

CString SCardGetLastError(DWORD lErr) ;
void CALLBACK MyTimerFunc (HWND, UINT, UINT, DWORD);

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSimple_PCSCDlg dialog

CSimple_PCSCDlg::CSimple_PCSCDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSimple_PCSCDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSimple_PCSCDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSimple_PCSCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSimple_PCSCDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CSimple_PCSCDlg, CDialog)
	//{{AFX_MSG_MAP(CSimple_PCSCDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BN_LISTREADER, OnBnListreader)
	ON_BN_CLICKED(IDC_BN_CONNECT, OnBnConnect)
	ON_BN_CLICKED(IDC_BN_TRANSMIT, OnBnTransmit)
	ON_BN_CLICKED(IDC_BN_STATUS, OnBnStatus)
	ON_BN_CLICKED(IDC_BN_STARTPOLLING, OnBnStartpolling)
	ON_BN_CLICKED(IDC_BN_STOPPOLLING, OnBnStoppolling)
	ON_BN_CLICKED(IDC_BN_DISCONNECT, OnBnDisconnect)
	ON_BN_CLICKED(IDC_BN_RELEASECONTEXT, OnBnReleasecontext)
	ON_BN_CLICKED(IDC_BN_CLEAR, OnBnClear)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSimple_PCSCDlg message handlers

BOOL CSimple_PCSCDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	m_pCtl = (CComboBox*)GetDlgItem(IDC_CMB_LIST);
	m_pListCtl = (CListBox*)GetDlgItem(IDC_LIST_RESULTS);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSimple_PCSCDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSimple_PCSCDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSimple_PCSCDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CSimple_PCSCDlg::OnBnListreader() 
{
	char *p = NULL;
	char *pDeviceName = NULL;
	DWORD nOffset = 0;
	DWORD dwLen = 0;
	CString	  errMsg = "" ;
	UpdateData(TRUE) ;

	// Clear the contents of List
	DWORD ret = SCardEstablishContext(SCARD_SCOPE_USER, 
										NULL, 
										NULL, 
										&ContextHandle);
	if (ret != SCARD_S_SUCCESS) 
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
		UpdateData(FALSE) ;
		return ;
	}

	m_pListCtl->AddString("SCardEstablishContext...OK");
	
	ZeroMemory(pResponseBuffer, sizeof(pResponseBuffer));
	ResponseLength = MAX_RESPONSE;
	ret = SCardListReaders(ContextHandle, 0, 
						(char *) pResponseBuffer, 
						&ResponseLength);

	if (ret != SCARD_S_SUCCESS) 
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
	}
	else 
	{
		pDeviceName = (char *)pResponseBuffer;	
		p = strchr(pDeviceName, '\0');
		
		m_pCtl ->ResetContent();				// Clear the contents of List
		
		while(p != NULL)
		{
			if (strlen(pDeviceName) > 0)
			{
				m_pCtl ->AddString(pDeviceName) ;
			}
			
			nOffset += ((p+1)-pDeviceName);
			
			if(nOffset >= ResponseLength)
			{
				break;
			}
			
			pDeviceName = p+1;
			p = strchr(pDeviceName, '\0');
		}
		m_pCtl -> SetCurSel(0);
	}
	m_pListCtl->AddString("SCardListreader...OK");

	GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(TRUE) ;

	UpdateData(FALSE) ;
	return ;
}

void CSimple_PCSCDlg::OnBnConnect() 
{
	int len = 0 ;
	int index = 0 ;
	CString str = "" ;
	CString	errMsg = "" ;
	UpdateData(TRUE) ;
	
	index = m_pCtl->GetCurSel() ;
	len = m_pCtl->GetLBTextLen( index );
	m_pCtl->GetLBText( index, str.GetBuffer(len) );
	
    DWORD ret = SCardConnect(ContextHandle, 
		str.GetBuffer(0), 
		SCARD_SHARE_SHARED, 
		SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1, 
		&CardHandle, 
		&IO_REQ.dwProtocol);
	if (ret != SCARD_S_SUCCESS)
    {
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
	}
	else
	{
		GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(FALSE) ;
		GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(FALSE) ;
		GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(TRUE) ;
		GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(TRUE) ;
		GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(FALSE) ;
		GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(FALSE) ;
		GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(TRUE) ;
		GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(TRUE) ;

		m_pListCtl->AddString("SCardConnect...OK");
	}

	UpdateData(FALSE) ;
	return ;
}

BOOL CSimple_PCSCDlg::SCardTransmit(const char* strApdu, const char* strResp, unsigned int *nSW)
{	
	char  SendData[MAX_APDU_LEN], ReceiveData[MAX_RESPONSE];
	ULONG nCmdLen, nResp;
	CString	errMsg = "" ;
	
    ZeroMemory(ReceiveData, MAX_APDU_LEN);
    nCmdLen = strlen(strApdu);
	
    Asc2Hex(SendData, (char*)strApdu, nCmdLen);			// string convertion
	
    nResp = MAX_RESPONSE;
	IO_REQ.dwProtocol ;
	IO_REQ.cbPciLength = (DWORD) sizeof(SCARD_IO_REQUEST);
	
	DWORD ret = ::SCardTransmit(CardHandle,				// SCard API
		&IO_REQ, 
		(PUCHAR)SendData, 
		nCmdLen / 2, 
		0, 
		(PUCHAR)ReceiveData, 
		&nResp);
	
	if (ret != SCARD_S_SUCCESS)
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
        return FALSE;
	}
	
    if(nResp != 0)
    {
		if (2 == nResp)
		{
			Hex2Asc((char*)strResp, ReceiveData, nResp);
			if (!memcmp(strResp, "9000", 4))
			{
				return TRUE ;
			}
			else
				return FALSE ;
		}
		else
		{
			Hex2Asc((char*)strResp, ReceiveData, nResp - 2);
			*nSW = ((unsigned char)(ReceiveData[nResp - 2]) * 256) + 
				(unsigned char)ReceiveData[nResp - 1];
			
			return TRUE;
		}
    }
	
	return FALSE;
}

void CSimple_PCSCDlg::OnBnTransmit() 
{	
	UINT nSW ;
	char Command[1024] = { 0 } ;
	char sResponse[1024] = { 0 } ;
	CString errMsg = "" ;
	UpdateData(TRUE) ;
	
	::GetDlgItemText(this->m_hWnd, IDC_EDIT_APDU, Command, MAX_RESPONSE);
	if (strlen(Command) <=0 )
	{
		m_pListCtl->AddString("fail no command !");
		UpdateData(FALSE) ;
		return ;
	}
	
	if(SCardTransmit(Command, sResponse, &nSW))		// Do Transmit commands
	{
		//SetDlgItemText(IDC_STATIC_RESULTS, sResponse) ;
		m_pListCtl->AddString(sResponse);
	}
	
	UpdateData(FALSE) ;
	return ;
}

void CSimple_PCSCDlg::OnBnStatus() 
{
	int index;
	int len = 0 ;
	BYTE ATRVal[128];
	DWORD ATRLen ;
	DWORD ReaderLen = 100, dwState;
	DWORD tempword;
	char tempstr[255]={0};
	char readerName[128]={0};
	CString errMsg = "" ;
	UpdateData(TRUE) ;

	index = m_pCtl->GetCurSel() ;
	len = m_pCtl->GetLBTextLen( index );
	m_pCtl->GetLBText( index, readerName );

	
	//Invoke SCardStatus using hCard handle and valid reader name
	tempword = 32;
	ATRLen = tempword;
	
	DWORD ret = SCardStatus( CardHandle,
		tempstr,
		&ReaderLen,
		&dwState,
		&IO_REQ.dwProtocol,
		ATRVal,
		&ATRLen );

	if( ret != SCARD_S_SUCCESS )
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
	}
	else
	{
		//Format ATRVal returned and display string as ATR value
		sprintf( tempstr, "> ATR Length: %d\n", ATRLen );
		m_pListCtl->AddString( tempstr );

		sprintf( tempstr, "> ATR Value: " );
		for( index = 0; index != ATRLen; index++ )
		{
			sprintf( tempstr, "%s%02X ", tempstr, ATRVal[index] );
		}
		sprintf( tempstr, "%s\n", tempstr );
		m_pListCtl->AddString( tempstr );
		
		//Interpret dwActProtocol returned and display as active protocol
		sprintf( tempstr, "> Active Protocol: " );
		switch( IO_REQ.dwProtocol )
		{
			
		case 1:
			if( strcmp( readerName, "ACS ACR128U PICC Interface 0" ) == 0 )
			{
				sprintf( tempstr, "%s T=CL\n", tempstr );		
			}
			else
			{
				sprintf( tempstr, "%s T=0\n", tempstr );
			}
			break;
		case 2:
			if( strcmp( readerName, "ACS ACR128U PICC Interface 0" ) == 0 )
			{
				sprintf( tempstr, "%s T=CL\n", tempstr );	
			}
			else
			{
				sprintf( tempstr, "%s T=1\n", tempstr );
			}
			break;
		default:
			sprintf( tempstr, "> No Protocol is defined\n" );
			
		}
		m_pListCtl->AddString( tempstr );
	}

	UpdateData(FALSE) ;
	
}

void CSimple_PCSCDlg::OnBnStartpolling() 
{
	pThis = this ;
	this->SetTimer (1, 100, MyTimerFunc);

	GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(TRUE) ;

	UpdateData(FALSE) ;
	return ;
}

void CSimple_PCSCDlg::OnBnStoppolling() 
{
	pThis->KillTimer(1);

	m_pListCtl->ResetContent();
	m_pListCtl->AddString("Stop polling!") ;

	GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(TRUE) ;

	UpdateData(FALSE) ;
	return ;
}

void CSimple_PCSCDlg::OnBnDisconnect() 
{
	CString errMsg = "" ;
	DWORD ret = SCardDisconnect(CardHandle, SCARD_EJECT_CARD);
	if (ret != SCARD_S_SUCCESS)
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
	}
	else
		m_pListCtl->AddString("SCardDisconnect...OK");

	GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(TRUE) ;

	UpdateData(FALSE) ;
	return ;  
}

void CSimple_PCSCDlg::OnBnReleasecontext() 
{
	CString errMsg = "" ;
	pThis->KillTimer(1);

	DWORD ret = SCardReleaseContext(ContextHandle) ;
	if (ret != SCARD_S_SUCCESS)
	{
		errMsg = SCardGetLastError(ret) ;
		m_pListCtl->AddString(errMsg);
	}
	else
		m_pListCtl->AddString("SCardReleasecontext...OK");

	GetDlgItem(IDC_BN_LISTREADER)    ->EnableWindow(TRUE) ;
	GetDlgItem(IDC_BN_CONNECT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_TRANSMIT)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STATUS)	     ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STARTPOLLING)  ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_STOPPOLLING)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_DISCONNECT)	 ->EnableWindow(FALSE) ;
	GetDlgItem(IDC_BN_RELEASECONTEXT)->EnableWindow(FALSE) ;
	
	UpdateData(FALSE) ;
	return ;  
	
}

void CSimple_PCSCDlg::OnBnClear() 
{
	m_pListCtl->ResetContent();
}

void CALLBACK MyTimerFunc (HWND, UINT, UINT, DWORD)
{
	pThis->SCardGetStatusChange();
}

void CSimple_PCSCDlg::SCardGetStatusChange()
{
	UpdateData(TRUE) ;
	DWORD len = 0 ;
	DWORD index = 0 ;
	CString str = "" ;
	SCARD_READERSTATE RdrState;
	
	index = m_pCtl->GetCurSel() ;
	len = m_pCtl->GetLBTextLen( index );
	m_pCtl->GetLBText( index, str.GetBuffer(len) );
	
	m_pListCtl->ResetContent();
	
	RdrState.szReader = str.GetBuffer(0);
	DWORD ret = ::SCardGetStatusChange(ContextHandle, 0, &RdrState, 1);
	if (ret != SCARD_S_SUCCESS)
	{
		return ;
	}
	else{
		
		if (RdrState.dwEventState & SCARD_STATE_PRESENT)
		{
			m_pListCtl->AddString("Card is inserted!") ;
		}
		else{
			m_pListCtl->AddString("Card is removed!") ;
		}
	}
	
	UpdateData(FALSE) ;
	return ;  
}

///////////////////////////////////////////////////////////////////////////////
// Get Last error
CString SCardGetLastError(DWORD lErr)
{

	switch(lErr)
	{
        case 0x80100001:
                return "SCARD_F_INTERNAL_ERROR";
                break;

        case 0x80100002:
                return "SCARD_E_CANCELLED";
                break;

        case 0x80100003:
                return "SCARD_E_INVALID_HANDLE";
                break;

        case 0x80100004:
                return "SCARD_E_INVALID_PARAMETER";
                break;

        case 0x80100005:
                return "SCARD_E_INVALID_TARGET";
                break;

        case 0x80100006:
                return "SCARD_E_NO_MEMORY";
                break;

        case 0x80100007:
                return "SCARD_F_WAITED_TOO_LONG";
                break;

        case 0x80100008:
                return "SCARD_E_INSUFFICIENT_BUFFER";
                break;

        case 0x80100009:
                return "SCARD_E_UNKNOWN_READER";
                break;

        case 0x8010000A:
                return "SCARD_E_TIMEOUT";
                break;

        case 0x8010000B:
                return "SCARD_E_SHARING_VIOLATION";
                break;

        case 0x8010000C:
                return "SCARD_E_NO_SMARTCARD";
                break;

        case 0x8010000D:
                return "SCARD_E_UNKNOWN_CARD";
                break;

        case 0x8010000E:
                return "SCARD_E_CANT_DISPOSE";
                break;

        case 0x8010000F:
                return "SCARD_E_PROTO_MISMATCH";
                break;

        case 0x80100010:
                return "SCARD_E_NOT_READY";
                break;

        case 0x80100011:
                return "SCARD_E_INVALID_VALUE";
                break;

        case 0x80100012:
                return "SCARD_E_SYSTEM_CANCELLED";
                break;

        case 0x80100013:
                return "SCARD_F_COMM_ERROR";
                break;

        case 0x80100014:
                return "SCARD_F_UNKNOWN_ERROR";
                break;

        case 0x80100015:
                return "SCARD_E_INVALID_ATR";
                break;

        case 0x80100016:
                return "SCARD_E_NOT_TRANSACTED";
                break;

        case 0x80100017:
                return "SCARD_E_READER_UNAVAILABLE";
                break;

        case 0x80100018:
                return "SCARD_P_SHUTDOWN";
                break;

        case 0x80100019:
                return "SCARD_E_PCI_TOO_SMALL";
                break;

        case 0x8010001A:
                return "SCARD_E_READER_UNSUPPORTED";
                break;

        case 0x8010001B:
                return "SCARD_E_DUPLICATE_READER";
                break;

        case 0x8010001C:
                return "SCARD_E_CARD_UNSUPPORTED";
                break;

        case 0x8010001D:
                return "SCARD_E_NO_SERVICE";
                break;

        case 0x8010001E:
                return "SCARD_E_SERVICE_STOPPED";
                break;

        case 0x8010001F:
                return "SCARD_E_UNEXPECTED";
                break;

        case 0x80100020:
                return "SCARD_E_ICC_INSTALLATION";
                break;

        case 0x80100021:
                return "SCARD_E_ICC_CREATEORDER";
                break;

        case 0x80100022:
                return "SCARD_E_UNSUPPORTED_FEATURE";
                break;

        case 0x80100023:
                return "SCARD_E_DIR_NOT_FOUND";
                break;

        case 0x80100024:
                return "SCARD_E_FILE_NOT_FOUND";
                break;

        case 0x80100025:
                return "SCARD_E_NO_DIR";
                break;

        case 0x80100026:
                return "SCARD_E_NO_FILE";
                break;

        case 0x80100027:
                return "SCARD_E_NO_ACCESS";
                break;

        case 0x80100028:
                return "SCARD_E_WRITE_TOO_MANY";
                break;

        case 0x80100029:
                return "SCARD_E_BAD_SEEK";
                break;

        case 0x8010002A:
                return "SCARD_E_INVALID_CHV";
                break;

        case 0x8010002B:
                return "SCARD_E_UNKNOWN_RES_MNG";
                break;

        case 0x8010002C:
                return "SCARD_E_NO_SUCH_CERTIFICATE";
                break;

        case 0x8010002D:
                return "SCARD_E_CERTIFICATE_UNAVAILABLE";
                break;

        case 0x8010002E:
                return "SCARD_E_NO_READERS_AVAILABLE";
                break;

        case 0x80100065:
                return "SCARD_W_UNSUPPORTED_CARD";
                break;

        case 0x80100066:
                return "SCARD_W_UNRESPONSIVE_CARD";
                break;

        case 0x80100067:
                return "SCARD_W_UNPOWERED_CARD";
                break;

        case 0x80100068:
                return "SCARD_W_RESET_CARD";
                break;

        case 0x80100069:
                return "SCARD_W_REMOVED_CARD";
                break;

        case 0x8010006A:
                return "SCARD_W_SECURITY_VIOLATION";
                break;

        case 0x8010006B:
                return "SCARD_W_WRONG_CHV";
                break;

        case 0x8010006C:
                return "SCARD_W_CHV_BLOCKED";
                break;

        case 0x8010006D:
                return "SCARD_W_EOF";
                break;

        case 0x8010006E:
                return "SCARD_W_CANCELLED_BY_USER";
                break;

        case 0x0000007B:
                return "INACCESSIBLE_BOOT_DEVICE";
                break;

	default:
		return "Invalid Error Code";

	}

}

