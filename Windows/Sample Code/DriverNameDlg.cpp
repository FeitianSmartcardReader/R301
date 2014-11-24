// DriverNameDlg.cpp : implementation file
//

#include "stdafx.h"
#include "ReaderSample.h"
#include "DriverNameDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDriverNameDlg dialog


CDriverNameDlg::CDriverNameDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDriverNameDlg::IDD, pParent)
{
	act_Name = 0;
	//{{AFX_DATA_INIT(CDriverNameDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
}


void CDriverNameDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDriverNameDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CDriverNameDlg, CDialog)
	//{{AFX_MSG_MAP(CDriverNameDlg)
	ON_CBN_SELCHANGE(IDC_DRIVER_NAME, OnSelchangeDriverName)
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDriverNameDlg message handlers

void CDriverNameDlg::OnSelchangeDriverName() 
{
	CComboBox *pbox = (CComboBox*)GetDlgItem(IDC_DRIVER_NAME);
	act_Name = pbox->GetCurSel();
	
}

BOOL CDriverNameDlg::OnInitDialog() 
{
	//  Associate a tab with the window so we can locate it later.
	::SetProp(GetSafeHwnd(), AfxGetApp()->m_pszExeName, (HANDLE)1);
	CDialog::OnInitDialog();
	
	CComboBox *pbox = (CComboBox*)GetDlgItem(IDC_DRIVER_NAME);
	pbox->ResetContent();
	for (int i=0; i<Count; i++)
		pbox->InsertString(i, Name+(i*100));                             
	pbox->EnableWindow(TRUE);
	pbox->SetItemData(0, act_Name);
	pbox->SetCurSel(act_Name);	                            

	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CDriverNameDlg::OnDestroy() 
{
	//   Remove   previous   instance   tag   from   window. 
	::RemoveProp(GetSafeHwnd(),   AfxGetApp()->m_pszExeName); 
	CDialog::OnDestroy();
	
}