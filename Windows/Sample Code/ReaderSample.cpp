
/****************************************************************************/
/*                                                                          */
/* Author         :	Feitian													*/
/* Creation date  : 07.10.2008												*/
/* Name           : ReaderSample.cpp (Defines the class behaviors           */
/*								  for the application.)                     */
/*                                                                          */
/* Description    : Reader Sample for								        */
/*                  Windows NT4.0/5.0										*/
/*                                                                          */
/****************************************************************************/

#include "stdafx.h"
#include "ReaderSample.h"
#include "ReaderSampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CReaderSampleApp

BEGIN_MESSAGE_MAP(CReaderSampleApp, CWinApp)
	//{{AFX_MSG_MAP(CReaderSampleApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CReaderSampleApp construction

CReaderSampleApp::CReaderSampleApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CReaderSampleApp object

CReaderSampleApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CReaderSampleApp initialization

BOOL CReaderSampleApp::InitInstance()
{
	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.
	//****************************************************
	//Set-up only one app
	//Create a mutex object.    
	//Note that the mutex handle is automatically closed    
	//when the process terminates.
	::CreateMutex (NULL,TRUE,m_pszExeName);
	//If the mutex object already exists,    
	//then this is the second instance of the appliction.  
	if(GetLastError()==ERROR_ALREADY_EXISTS)
	{
		CWnd* pPrevWnd;
		//Find our previous application's main window.
		pPrevWnd = CWnd::GetDesktopWindow();
		pPrevWnd = pPrevWnd->GetWindow(GW_CHILD);
		while(pPrevWnd)
		{
			//Does this window have the previous instance tag set? 
			if(::GetProp(pPrevWnd->GetSafeHwnd(),m_pszExeName))
			{
				//Found window, now set focus to the window.  
				//First restore window if it is currently iconic.
				if(pPrevWnd->IsIconic())
					pPrevWnd->ShowWindow(SW_RESTORE);
				//If Window is not visible, show it.  
				if(!pPrevWnd->IsWindowVisible())  
					pPrevWnd->ShowWindow(SW_SHOW);
				//Set focus to main window. 
				pPrevWnd->SetForegroundWindow();
				//If Window has a pop-up window, set focus to pop-up. 
				pPrevWnd->GetLastActivePopup()->SetForegroundWindow();
				//Find previous instance main window, show it and exit  
				return FALSE;
			}
			//Did not find window, get next window in list.   
			pPrevWnd=pPrevWnd->GetWindow(GW_HWNDNEXT);
		}
		TRACE("Could not find previous instance main window!\n");
		return FALSE;
	} 

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif
	
	CReaderSampleDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
