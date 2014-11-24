
/****************************************************************************/
/*                                                                          */
/* Author         :	Feitian													*/
/* Creation date  : 07.10.2008												*/
/* Name           : ReaderSampleDlg.cpp (implementation file)               */
/*                                                                          */
/* Description    : Reader Sample for										*/
/*                  Windows NT4.0/5.0										*/
/*                                                                          */
/****************************************************************************/


#include "stdafx.h"

//	include Reader definition
#include "winscard.h"

#include "ReaderSample.h"
#include "ReaderSampleDlg.h"
#include "DriverNameDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

SCARD_READERSTATE	ReaderState[NUMBER_OF_READERS];
SCARD_IO_REQUEST	IO_Request;
SCARDCONTEXT		ContextHandle;
SCARDHANDLE			CardHandle;

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
// CReaderSampleDlg dialog

CReaderSampleDlg::CReaderSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CReaderSampleDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CReaderSampleDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CReaderSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CReaderSampleDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CReaderSampleDlg, CDialog)
	//{{AFX_MSG_MAP(CReaderSampleDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_CARD_DISCONNECT, OnCardDisconnect)
	ON_BN_CLICKED(IDC_CONNECT, OnConnect)
	ON_BN_CLICKED(IDC_TRANSMIT, OnTransmit)
	ON_WM_VSCROLL()
	ON_WM_DESTROY()
	ON_WM_CLOSE()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CReaderSampleDlg message handlers

BOOL CReaderSampleDlg::OnInitDialog()
{
	//Associate a tab with the window so we can locate it later.
	::SetProp(GetSafeHwnd(), AfxGetApp()->m_pszExeName, (HANDLE)1);
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
	
	// extra initialization 

	memset(ReaderName[0], 0, NAME_LENGTH);
	memset(ReaderName[1], 0, NAME_LENGTH);
	memset(ReaderName[2], 0, NAME_LENGTH);
	memset(ReaderName[3], 0, NAME_LENGTH);
	
	pOutBuffer = (PBYTE) malloc(MAX_OUTPUT);
	ASSERT( pOutBuffer );
	OutBufferLine = 0;

   	((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollRange(0, 6, FALSE);
   	line = 0;             
	((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollPos(line, TRUE);

	pResponseBuffer = (PBYTE) malloc( MAX_RESPONSE );
	ASSERT( pResponseBuffer );
	
	memset(pResponseBuffer, 0x00, MAX_RESPONSE);

	//
	//	Open a context which communication to the Reader
	//
	ret = SCardEstablishContext(SCARD_SCOPE_USER, NULL, NULL, &ContextHandle);

	if (ret != SCARD_S_SUCCESS) {
		MessageBuffer.Format("Function SCardEstablishContext returned 0x%X error code.", ret);
//		MessageBox((LPCTSTR) MessageBuffer, "ReaderSample", MB_OK| MB_ICONSTOP);
		SetDlgItemText(IDC_OUT, MessageBuffer);
		EndDialog(2);
		return(IDCANCEL);
	}
   	
	ReaderCount = 0;
	ResponseLength = MAX_RESPONSE;
	
	ret = SCardListReaders(ContextHandle, 0, (char *) pResponseBuffer, &ResponseLength);
	
	if (ret != SCARD_S_SUCCESS) {
		MessageBuffer.Format("Function SCardListReaders returned 0x%X error code.", ret);
//		MessageBox((LPCTSTR) MessageBuffer, "ReaderSample", MB_OK| MB_ICONSTOP);
		SetDlgItemText(IDC_OUT, MessageBuffer);
		EndDialog(2);
		return(IDCANCEL);
	}
	else {
		unsigned int		StringLen = 0;

		while ( ResponseLength > StringLen+1) {
			strcpy(ReaderName[ReaderCount], (LPCTSTR) pResponseBuffer+StringLen);
			DWORD	ActiveProtocol = 0;
			ret = SCardConnect(ContextHandle, ReaderName[ReaderCount], SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1, &CardHandle, &ActiveProtocol);
		//	if (ret != SCARD_E_UNKNOWN_READER)
				ReaderCount++;
			if (ret == SCARD_S_SUCCESS)
				SCardDisconnect(CardHandle, SCARD_EJECT_CARD);
			StringLen += strlen((LPCTSTR) pResponseBuffer+StringLen+1);
			StringLen += 2;
		}
	}

	if (ReaderCount == 0) {
//		MessageBox("No driver is available for use with the Reader Sample!", "Reader Sample", MB_ICONSTOP);
		SetDlgItemText(IDC_OUT, "No driver is available for use!");
		EndDialog(2);
		return (IDCANCEL);
	}

	CDriverNameDlg dlg;

	dlg.SetCount(ReaderCount);
	dlg.SetName((char *) ReaderName);

	if ( dlg.DoModal() != IDOK ) {
		EndDialog(2);
		return (IDCANCEL);
	}

	act_Name	= dlg.GetDriverName();

   	(CButton *)GetDlgItem(IDC_CARD_DISCONNECT)->EnableWindow(FALSE);                  
  	(CButton *)GetDlgItem(IDC_TRANSMIT)->EnableWindow(FALSE);                  

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CReaderSampleDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CReaderSampleDlg::OnPaint() 
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
HCURSOR CReaderSampleDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CReaderSampleDlg::OnConnect() 
{
	DWORD	ActiveProtocol = 0;

	ProtocolType = SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1;

	ret = SCardConnect(ContextHandle, ReaderName[act_Name], SCARD_SHARE_EXCLUSIVE, 
				ProtocolType, &CardHandle, &ActiveProtocol);

	if (ret != SCARD_S_SUCCESS){
		GetErrorCode(ret);
		return;
	}

	ProtocolType = ActiveProtocol;
	char sOut[300] = {0}, s[100] = {0};
	switch (ProtocolType) {
		case SCARD_PROTOCOL_T0: 
								sprintf(s, "Function SCardConnect ok\nProtocoltype = T0");
								break;
		case SCARD_PROTOCOL_T1: 
								sprintf(s, "Function SCardConnect ok\nProtocoltype = T1");
								break;
		default:
								sprintf((char *) pOutBuffer, "Function SCardConnect ok\n%.8x", ActiveProtocol);
								sprintf(s, "%s", (char *) pOutBuffer);
								break;
	}
	
	// È¡×´Ì¬
	DWORD namelen = 256, atrlen = 64, state;
	unsigned char atr[65] = {0};
	ret = SCardStatus(CardHandle, ReaderName[act_Name], &namelen, &state, &ActiveProtocol, atr, &atrlen);
	if (SCARD_S_SUCCESS != ret) 
	{
		SCardDisconnect(CardHandle, SCARD_EJECT_CARD);
		SCardReleaseContext(ContextHandle);
		return;
	}
	char st[200] = {0}, ss[4] = {0};
	for(int i = 0; i < atrlen; i++)
	{
		sprintf(ss, "%02x ", atr[i]);
		strcat(st, ss);
	}
	sprintf(sOut, "%s\n\nATR: %s", s, st);
	SetDlgItemText(IDC_OUT, sOut);
	OutBufferLine = 0;

   	(CButton *)GetDlgItem(IDC_CONNECT)->EnableWindow(FALSE);                  
   	(CButton *)GetDlgItem(IDCANCEL)->EnableWindow(FALSE);                  
   	(CButton *)GetDlgItem(IDC_CARD_DISCONNECT)->EnableWindow(TRUE);                  
  	(CButton *)GetDlgItem(IDC_TRANSMIT)->EnableWindow(TRUE);                  

}

void CReaderSampleDlg::OnTransmit() 
{
	char	mhstr[MAX_INPUT];
	char	buf[MAX_INPUT/2];

	PBYTE	pInBuffer;

	memset(mhstr, 0, MAX_INPUT);

	CString	com;
	((CComboBox*)GetDlgItem(IDC_IN))->GetWindowText(com);
	sprintf(mhstr , "%s", LPCTSTR(com));
	
	if (!AToHex((char *) &mhstr, (BYTE *) &buf))  {
		return;
	}

	IO_Request.dwProtocol = ProtocolType;
	IO_Request.cbPciLength = (DWORD) sizeof(SCARD_IO_REQUEST);

	pInBuffer = (PBYTE) malloc( MAX_INPUT );
	ASSERT( pInBuffer );

	memcpy(pInBuffer, buf, InBufferLength);
	
	ResponseLength = MAX_RESPONSE;

	ret = SCardTransmit(CardHandle, &IO_Request, pInBuffer, InBufferLength, 0, pResponseBuffer, &ResponseLength);
	
	if (ret != SCARD_S_SUCCESS){
		GetErrorCode(ret);
		free(pInBuffer);
		return;
	}

	PBYTE	pStartAddress = pOutBuffer;

	for (unsigned long i=0; i<ResponseLength; i++) {
		sprintf((char *) pOutBuffer, "%.2x ",(BYTE) pResponseBuffer[i]);
		pOutBuffer += 3;
		if (i > MAX_RESPONSE-3)
			break;
	}

	pOutBuffer = pStartAddress;

	OutBufferLine = (ResponseLength * 3)/35;
	OutBufferLine++;
	if (OutBufferLine > 5) {
		((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollRange(0, OutBufferLine, FALSE);
		line = 0;             
		((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollPos(line, TRUE);
	}	

	SetDlgItemText(IDC_OUT, (const char *) pOutBuffer);
	
	free(pInBuffer);
}

void CReaderSampleDlg::OnCardDisconnect() 
{
	ret = SCardDisconnect(CardHandle, SCARD_EJECT_CARD);

	if (ret != SCARD_S_SUCCESS){
		GetErrorCode(ret);
	}
	else
		SetDlgItemText(IDC_OUT, "Function SCardDisconnect ok");
	
   	(CButton *)GetDlgItem(IDC_CONNECT)->EnableWindow(TRUE);                  
   	(CButton *)GetDlgItem(IDCANCEL)->EnableWindow(TRUE);                  
   	(CButton *)GetDlgItem(IDC_CARD_DISCONNECT)->EnableWindow(FALSE);                  
  	(CButton *)GetDlgItem(IDC_TRANSMIT)->EnableWindow(FALSE);                  	
}

void CReaderSampleDlg::OnCancel() 
{
	if(ContextHandle != NULL)
	{
		
		ret = SCardReleaseContext(ContextHandle);
		
		if (ret != SCARD_S_SUCCESS){
			GetErrorCode(ret);
		}
		
		free(pOutBuffer);
		free(pResponseBuffer);
	}
	CDialog::OnCancel();
}

void CReaderSampleDlg::OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar) 
{
	switch (nSBCode) {
		case SB_LINEDOWN:   ++line;
							break;
		case SB_PAGEDOWN:   ++line;
							break;
		case SB_LINEUP:     --line;
							break;
		case SB_PAGEUP:     --line;
							break;
		case SB_THUMBPOSITION:
		case SB_THUMBTRACK:
		     				line = ((short) ResponseLength) - nPos;
							break;           
	}						
	UpdateMouseDisplay();
}

void CReaderSampleDlg::UpdateMouseDisplay()
{                   
   	if (pOutBuffer != NULL)	{
   	    if (OutBufferLine > 5) {
			if (line < 0 )  {                            
   				((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollPos(0, TRUE);
   				line = 0;
   			}	
   			else if (line > OutBufferLine)	{
   				((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollPos(OutBufferLine, TRUE);
   				line = OutBufferLine;
   			}	
   			else {	
	   			((CScrollBar *)GetDlgItem(IDC_SCROLLBAR))->SetScrollPos(line, TRUE);
   			}
   		}		
   	    if ((OutBufferLine >= line+4) && (OutBufferLine > 5))
			SetDlgItemText(IDC_OUT, (const char *) pOutBuffer+(line*35));  
   	}
   		
	UpdateWindow();
} 

short CReaderSampleDlg::AToHex(char *mhstr, BYTE *buf)
{

	InBufferLength = strlen(mhstr);                   
	if (InBufferLength < 3) {
		SetDlgItemText(IDC_OUT, "Input not complete!");
		GetDlgItem(IDC_IN)->SetFocus();
		return(0);
	}
	for (short i=0; i<InBufferLength; i++) {
		if (mhstr[i] == '\n') {
			mhstr[i] = 0;
		}
		else if (((mhstr[i] < '0') || (mhstr[i] > '9')) &&			
				((mhstr[i] != ' ') && (mhstr[i] != ',')) &&
				((mhstr[i] < 'a') || (mhstr[i] > 'f')) && 
				((mhstr[i] < 'A') || (mhstr[i] > 'F'))) { 
			SetDlgItemText(IDC_OUT, "Only hex. input allowed!");
			GetDlgItem(IDC_IN)->SetFocus();
			return(0);
		}	    
	}

	char *s;
	if (mhstr[1] == ',') {
		InBufferLength = (InBufferLength - 4)/2;
		s = mhstr+4;
	}
	else {
		InBufferLength = InBufferLength/2;
		s = mhstr;
	}
	memset(buf, 0, sizeof (buf));
	i = 0;

	while (isspace(*s))  s++;

	short k = FALSE;
	while (*s) {
		if (isxdigit(*s))  {
			buf[i] <<= 4;                 
			if (isalpha(*s))              
				buf[i] |= (tolower(*s) - 0x57); 
			else
				buf[i] |= (*s - 0x30);

			if (k) {
				i++; 
				k= FALSE;
			}
			else {	
				k= TRUE;
			}	
			s++;
		}
		else  break;
	}   
	return(1);
}
		

void CReaderSampleDlg::GetErrorCode(long ret)
{
	switch (ret) {
		case SCARD_E_CANCELLED:
							SetDlgItemText(IDC_OUT,"The action was cancelled by an SCardCancel request.");
							break;

		case SCARD_E_CANT_DISPOSE:
							SetDlgItemText(IDC_OUT,"The system could not dispose of the media in the requested manner.");
							break;
		case SCARD_E_CARD_UNSUPPORTED:
							SetDlgItemText(IDC_OUT,"The smart card does not meet minimal requirements for support.");
							break;
		case SCARD_E_DUPLICATE_READER:
							SetDlgItemText(IDC_OUT,"The reader driver didn't produce a unique reader name.");
							break;
		case SCARD_E_INSUFFICIENT_BUFFER:
							SetDlgItemText(IDC_OUT,"The data buffer to receive returned data is too small for the returned data.");
							break;
		case SCARD_E_INVALID_ATR:
							SetDlgItemText(IDC_OUT,"An ATR obtained from the registry is not a valid ATR string.");
							break;
		case SCARD_E_INVALID_HANDLE:
							SetDlgItemText(IDC_OUT,"The supplied handle was invalid.");
							break;
		case SCARD_E_INVALID_PARAMETER:
							SetDlgItemText(IDC_OUT,"One or more of the supplied parameters could not be properly interpreted.");
							break;
		case SCARD_E_INVALID_TARGET:
							SetDlgItemText(IDC_OUT,"Registry startup information is missing or invalid.");
							break;
		case SCARD_E_INVALID_VALUE:
							SetDlgItemText(IDC_OUT,"One or more of the supplied parameters’ values could not be properly interpreted.");
							break;
		case SCARD_E_NOT_READY:
							SetDlgItemText(IDC_OUT,"The reader or card is not ready to accept commands.");
							break;
		case SCARD_E_NOT_TRANSACTED:
							SetDlgItemText(IDC_OUT,"An attempt was made to end a non-existent transaction.");
							break;
		case SCARD_E_NO_MEMORY:
							SetDlgItemText(IDC_OUT,"Not enough memory available to complete this command.");
							break;
		case SCARD_E_NO_SERVICE:
							SetDlgItemText(IDC_OUT,"The Smart card reader sample is not running.");
							break;
		case SCARD_E_NO_SMARTCARD:
							SetDlgItemText(IDC_OUT,"The operation requires a smart card but no smart card is currently in the device.");
							break;
		case SCARD_E_PCI_TOO_SMALL:
							SetDlgItemText(IDC_OUT,"The PCI Receive buffer was too small.");
							break;
		case SCARD_E_PROTO_MISMATCH:
							SetDlgItemText(IDC_OUT,"The requested protocols are incompatible with the protocol currently in use with the card.");
							break;
		case SCARD_E_READER_UNAVAILABLE:
							SetDlgItemText(IDC_OUT,"The specified reader is not currently available for use.");
							break;
		case SCARD_E_READER_UNSUPPORTED:
							SetDlgItemText(IDC_OUT,"The reader driver does not meet minimal requirements for support.");
							break;
		case SCARD_E_SERVICE_STOPPED:
							SetDlgItemText(IDC_OUT,"The Smart card reader sample has shut down.");
							break;
		case SCARD_E_SHARING_VIOLATION:
							SetDlgItemText(IDC_OUT,"The card cannot be accessed because of other connections outstanding.");
							break;
		case SCARD_E_SYSTEM_CANCELLED:
							SetDlgItemText(IDC_OUT,"The action was cancelled by the system presumably to log off or shut down.");
							break;
		case SCARD_E_TIMEOUT:
							SetDlgItemText(IDC_OUT,"The user-specified timeout value has expired.");
							break;
		case SCARD_E_UNKNOWN_CARD:
							SetDlgItemText(IDC_OUT,"The specified card name is not recognized.");
							break;
		case SCARD_E_UNKNOWN_READER:
							SetDlgItemText(IDC_OUT,"The specified reader name is not recognized.");
							break;
		case SCARD_F_COMM_ERROR:
							SetDlgItemText(IDC_OUT,"An internal communications error has been detected.");
							break;
		case SCARD_F_INTERNAL_ERROR:
							SetDlgItemText(IDC_OUT,"An internal consistency check failed.");
							break;
		case SCARD_F_UNKNOWN_ERROR:
							SetDlgItemText(IDC_OUT,"An internal error has been detected but the source is unknown.");
							break;
		case SCARD_F_WAITED_TOO_LONG:
							SetDlgItemText(IDC_OUT,"An internal consistency timer has expired.");
							break;
		case SCARD_S_SUCCESS:
							SetDlgItemText(IDC_OUT,"OK");
							break;
		case SCARD_W_REMOVED_CARD:
							SetDlgItemText(IDC_OUT,"The card has been removed so that further communication is not possible.");
							break;
		case SCARD_W_RESET_CARD:
							SetDlgItemText(IDC_OUT,"The card has been reset so any shared state information is invalid.");
							break;
		case SCARD_W_UNPOWERED_CARD:
							SetDlgItemText(IDC_OUT,"Power has been removed from the card so that further communication is not possible.");
							break;
		case SCARD_W_UNRESPONSIVE_CARD:
							SetDlgItemText(IDC_OUT,"The card is not responding to a reset.");
							break;
		case SCARD_W_UNSUPPORTED_CARD:
							SetDlgItemText(IDC_OUT,"The reader cannot communicate with the card due to ATR configuration conflicts.");
							break;
		default:
			MessageBuffer.Format("Function returned 0x%X error code.", ret);
			SetDlgItemText(IDC_OUT, MessageBuffer);
			break;
	}
}

void CReaderSampleDlg::OnDestroy() 
{
	//Remove previous instance tag from window. 
	::RemoveProp(GetSafeHwnd(),   AfxGetApp()->m_pszExeName); 
	CDialog::OnDestroy();
	
}

void CReaderSampleDlg::OnClose() 
{	
	OnCancel();
	CDialog::OnClose();
}

void CReaderSampleDlg::WinHelp(DWORD dwData, UINT nCmd) 
{
	// TODO: Add your specialized code here and/or call the base class
	//ÎªÁËÆÁ±ÎF1¼ü,½«¸Ãº¯ÊýÖÃÎª²»Ö´ÐÐ,¿Õº¯Êý!
	//In order to shield the F1 Key, the functin must be NULL!
	//CDialog::WinHelp(dwData, nCmd);
}