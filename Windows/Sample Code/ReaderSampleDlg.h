
/****************************************************************************/
/*                                                                          */
/* Author         :	Feitian													*/
/* Creation date  : 07.10.2008												*/
/* Name           : ReaderSampleDlg.h  (header file)						*/
/*                                                                          */
/* Description    : Reader Sample for										*/
/*                  Windows NT4.0/5.0										*/
/*                                                                          */
/****************************************************************************/


#if !defined(AFX_READERSAMPLEDLG_H__56EC04C7_3EEB_11D1_996E_0080C82AE17C__INCLUDED_)
#define AFX_READERSAMPLEDLG_H__56EC04C7_3EEB_11D1_996E_0080C82AE17C__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000
//
#define NUMBER_OF_READERS	20
#define NAME_LENGTH			100
#define MAX_INPUT			1024
#define MAX_OUTPUT			4000
#define MAX_RESPONSE		2000
//#define MAX_RESPONSE		4000
/////////////////////////////////////////////////////////////////////////////
// CReaderSampleDlg dialog

class CReaderSampleDlg : public CDialog
{
// Construction
public:
	CReaderSampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CReaderSampleDlg)
	enum { IDD = IDD_READERSAMPLE_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CReaderSampleDlg)
	public:
	virtual void WinHelp(DWORD dwData, UINT nCmd = HELP_CONTEXT);
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON			m_hIcon;
	CString			MessageBuffer;
	long			ret;
	
	PBYTE			pInBuffer;
	long			InBufferLength;

	PBYTE			pOutBuffer;
	int				OutBufferLine;
	short      		line;

	short			act_Name;
	short			ReaderCount;
	char			ReaderName[NUMBER_OF_READERS][NAME_LENGTH];
	PBYTE			pResponseBuffer;
	unsigned long	ResponseLength;
	long			ProtocolType;

	void	UpdateMouseDisplay(void);
	short	AToHex(char *mhstr, BYTE *buf);
	void	GetErrorCode(long ret);

	// Generated message map functions
	//{{AFX_MSG(CReaderSampleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnCardDisconnect();
	afx_msg void OnConnect();
	afx_msg void OnTransmit();
	virtual void OnCancel();
	afx_msg void OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	afx_msg void OnDestroy();
	afx_msg void OnClose();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_READERSAMPLEDLG_H__56EC04C7_3EEB_11D1_996E_0080C82AE17C__INCLUDED_)
