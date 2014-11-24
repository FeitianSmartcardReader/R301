#if !defined(AFX_DRIVERNAMEDLG_H__A7DE5121_F1D7_11D0_ABDD_A11D67D6F64E__INCLUDED_)
#define AFX_DRIVERNAMEDLG_H__A7DE5121_F1D7_11D0_ABDD_A11D67D6F64E__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000
// DriverNameDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CDriverNameDlg dialog

class CDriverNameDlg : public CDialog
{
// Construction
public:
	CDriverNameDlg(CWnd* pParent = NULL);   // standard constructor

	short	act_Name, Count;
	char	*Name;
	short	GetDriverName(void) { return act_Name; };
	void	SetCount(short act_Count) { Count = act_Count; };
	void	SetName(char *Act_Name) { Name = Act_Name; };
// Dialog Data
	//{{AFX_DATA(CDriverNameDlg)
	enum { IDD = IDD_DRIVER_NAME };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDriverNameDlg)
	
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CDriverNameDlg)
	afx_msg void OnSelchangeDriverName();
	afx_msg void OnDestroy();
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DRIVERNAMEDLG_H__A7DE5121_F1D7_11D0_ABDD_A11D67D6F64E__INCLUDED_)
