; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CSimple_PCSCDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "Simple_PCSC.h"

ClassCount=4
Class1=CSimple_PCSCApp
Class2=CSimple_PCSCDlg
Class3=CAboutDlg

ResourceCount=3
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Resource3=IDD_SIMPLE_PCSC_DIALOG

[CLS:CSimple_PCSCApp]
Type=0
HeaderFile=Simple_PCSC.h
ImplementationFile=Simple_PCSC.cpp
Filter=N

[CLS:CSimple_PCSCDlg]
Type=0
HeaderFile=Simple_PCSCDlg.h
ImplementationFile=Simple_PCSCDlg.cpp
Filter=D
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=Simple_PCSCDlg.h
ImplementationFile=Simple_PCSCDlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_SIMPLE_PCSC_DIALOG]
Type=1
Class=CSimple_PCSCDlg
ControlCount=14
Control1=IDC_BN_LISTREADER,button,1342242816
Control2=IDC_BN_CONNECT,button,1476460544
Control3=IDC_BN_TRANSMIT,button,1476460544
Control4=IDC_BN_STATUS,button,1476460544
Control5=IDC_BN_STARTPOLLING,button,1476460544
Control6=IDC_BN_STOPPOLLING,button,1476460544
Control7=IDC_BN_DISCONNECT,button,1476460544
Control8=IDC_BN_RELEASECONTEXT,button,1476460544
Control9=IDC_EDIT_APDU,edit,1350631552
Control10=IDC_LIST_RESULTS,listbox,1352728833
Control11=IDC_BN_CLEAR,button,1342242816
Control12=IDC_CMB_LIST,combobox,1344340226
Control13=IDC_STATIC,button,1342177287
Control14=IDC_STATIC,button,1342177287

