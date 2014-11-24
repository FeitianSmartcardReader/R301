
/****************************************************************************/
/*                                                                          */
/* Author         : Feitian											        */
/* Creation date  : 07.10.2008												*/
/* Name           : ReaderSample.h (main header file for the				*/
/*								READERSAMPLE application)					*/
/*                                                                          */
/* Description    : Reader Sample for								        */
/*                  Windows NT4.0/5.0										*/
/*                                                                          */
/****************************************************************************/


#if !defined(AFX_READERSAMPLE_H__56EC04C5_3EEB_11D1_996E_0080C82AE17C__INCLUDED_)
#define AFX_READERSAMPLE_H__56EC04C5_3EEB_11D1_996E_0080C82AE17C__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CReaderSampleApp:
// See ReaderSample.cpp for the implementation of this class
//

class CReaderSampleApp : public CWinApp
{
public:
	CReaderSampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CReaderSampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CReaderSampleApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_READERSAMPLE_H__56EC04C5_3EEB_11D1_996E_0080C82AE17C__INCLUDED_)
