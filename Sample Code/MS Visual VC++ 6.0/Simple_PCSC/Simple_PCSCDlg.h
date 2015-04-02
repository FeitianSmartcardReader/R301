// Simple_PCSCDlg.h : header file
//
#include "winscard.h"

#if !defined(AFX_SIMPLE_PCSCDLG_H__94FD1289_64C7_402D_BAE7_44727DC3DB11__INCLUDED_)
#define AFX_SIMPLE_PCSCDLG_H__94FD1289_64C7_402D_BAE7_44727DC3DB11__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#pragma comment (lib, "winscard.lib")

#define			MAX_RESPONSE		1024
#define			MAX_APDU_LEN		1024

/////////////////////////////////////////////////////////////////////////////
// CSimple_PCSCDlg dialog

class CSimple_PCSCDlg : public CDialog
{
// Construction
public:
	CSimple_PCSCDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CSimple_PCSCDlg)
	enum { IDD = IDD_SIMPLE_PCSC_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSimple_PCSCDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSimple_PCSCDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBnListreader();
	afx_msg void OnBnConnect();
	afx_msg void OnBnTransmit();
	afx_msg void OnBnStatus();
	afx_msg void OnBnStartpolling();
	afx_msg void OnBnStoppolling();
	afx_msg void OnBnDisconnect();
	afx_msg void OnBnReleasecontext();
	afx_msg void OnBnClear();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

public:
	CComboBox* m_pCtl;
	CListBox*  m_pListCtl;

	void SCardGetStatusChange();
	BOOL SCardTransmit(const char* strApdu, const char* strResp, unsigned int *nSW);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SIMPLE_PCSCDLG_H__94FD1289_64C7_402D_BAE7_44727DC3DB11__INCLUDED_)
