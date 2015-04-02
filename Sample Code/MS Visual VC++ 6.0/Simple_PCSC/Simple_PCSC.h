// Simple_PCSC.h : main header file for the SIMPLE_PCSC application
//

#if !defined(AFX_SIMPLE_PCSC_H__B228273C_BA3C_49B8_A0A7_E583A6CDFB6A__INCLUDED_)
#define AFX_SIMPLE_PCSC_H__B228273C_BA3C_49B8_A0A7_E583A6CDFB6A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSimple_PCSCApp:
// See Simple_PCSC.cpp for the implementation of this class
//

class CSimple_PCSCApp : public CWinApp
{
public:
	CSimple_PCSCApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSimple_PCSCApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSimple_PCSCApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SIMPLE_PCSC_H__B228273C_BA3C_49B8_A0A7_E583A6CDFB6A__INCLUDED_)
