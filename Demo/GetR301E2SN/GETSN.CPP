/*
* Support for Feitian OEM R301E2 Card Reader to get SN from USB description
*
* Copyright (C) Feitian 2016, Ben <ben@ftsafe.com>
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Lesser General Public
* License as published by the Free Software Foundation; either
* version 2.1 of the License, or (at your option) any later version.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Lesser General Public License for more details.
*
* You should have received a copy of the GNU Lesser General Public
* License along with this library; if not, write to the Free Software
* Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/


/*
*USB Device Path Format
*
*In most cases, Windows formats the USB device path as follows:
*
*\?usb#vid_vvvv&pid_pppp#ssss#{gggggggg-gggg-gggg-gggg-gggggggggggg}
*
*Where:
*vvvv is the USB vendor ID represented in 4 hexadecimal characters.
*pppp is the USB product ID represented in 4 hexadecimal characters.
*ssss is the USB serial string represented in n characters.
*gggggggg-gggg-gggg-gggg-gggggggggggg is the device interface GUID that is used to link applications to device with specific drivers loaded.
*
*The CP210x VCP driver GUID is:
*
*{A2A39220-39F4-4b88-AECB-3D86A35DC748}
*
*The GUID is assigned in the driver or driver INF file and is used by the application to filter devices that have loaded that driver.
*
*Composite USB Device Path Format For Interfaces
*
*For a composite device with multiple interfaces, the device path for each interface might look something like:
*
*\?usb#vid_vvvv&pid_pppp&mi_ii#aaaaaaaaaaaaaaaa#{gggggggg-gggg-gggg-gggg-gggggggggggg}
*
*Where: 
*vvvv is the USB vendor ID represented in 4 hexadecimal characters.
*pppp is the USB product ID represented in 4 hexadecimal characters.
*ii is the USB interface number.
*aaaaaaaaaaaaaaaa is a unique, Windows-generated string based on things such as the physical USB port address and/or interface number.
*gggggggg-gggg-gggg-gggg-gggggggggggg is the device interface GUID that is used to link applications to device with specific drivers loaded.
*
*/
#include <windows.h>
#include <stdio.h>
#include <Shlwapi.h>
#include <conio.h>
#include <setupapi.h> 

#pragma comment(lib,"setupapi.lib")

static /*const*/ GUID GUID_DEVINTERFACE_USB_DEVICE = 
{ 0xA5DCBF10L, 0x6530, 0x11D2, { 0x90, 0x1F, 0x00, 0xC0, 0x4F, 0xB9, 0x51, 0xED } };

int main()
{
	unsigned int idVendor = 0x00;	//VID
	unsigned int idProduct = 0x00;	//PID
	unsigned char iSerialNumber[256] = {0}; //SN
	BOOL bRet = FALSE;
	HDEVINFO hDevInfo;

	// Get device interface info set handle for all devices attached to system
	hDevInfo = SetupDiGetClassDevs(
		&GUID_DEVINTERFACE_USB_DEVICE, /* CONST GUID * ClassGuid - USB class GUID */
		NULL, /* PCTSTR Enumerator */
		NULL, /* HWND hwndParent */
		DIGCF_PRESENT | DIGCF_DEVICEINTERFACE /* DWORD Flags */
		);

	if (hDevInfo == INVALID_HANDLE_VALUE) {
		printf("SetupDiClassDevs failed. GetLastError() returns: 0x%x\n", GetLastError());
		return -1;
	}


	//Retrieve a context structure for a device interface of a device information set.
	DWORD dwIndex = 0;
	SP_DEVICE_INTERFACE_DATA devInterfaceData;
	ZeroMemory(&devInterfaceData, sizeof(SP_DEVICE_INTERFACE_DATA));
	devInterfaceData.cbSize = sizeof(SP_DEVICE_INTERFACE_DATA);
	ULONG neededLength, requiredLength;
	PSP_DEVICE_INTERFACE_DETAIL_DATA ClassDeviceData;

	while (TRUE) {
		bRet = SetupDiEnumDeviceInterfaces(
			hDevInfo, /* HDEVINFO DeviceInfoSet */
			NULL, /* PSP_DEVINFO_DATA DeviceInfoData */
			&GUID_DEVINTERFACE_USB_DEVICE, /* CONST GUID * InterfaceClassGuid */
			dwIndex,
			&devInterfaceData /* PSP_DEVICE_INTERFACE_DATA DeviceInterfaceData */
			);

		//If no more items, then break
		if(!bRet && (GetLastError() == ERROR_NO_MORE_ITEMS)){
			break;
		}
		else {
			SetupDiGetDeviceInterfaceDetail(hDevInfo, &devInterfaceData, NULL, 0, &requiredLength, NULL);
			neededLength = requiredLength;
			ClassDeviceData = (PSP_DEVICE_INTERFACE_DETAIL_DATA)malloc(neededLength);
			ClassDeviceData->cbSize = sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA);

			//Call SetupDiGetDeviceInterfaceDetail again to get required lenght
			if (!SetupDiGetDeviceInterfaceDetail(hDevInfo, &devInterfaceData, ClassDeviceData, neededLength, &requiredLength, NULL))
			{
				free(ClassDeviceData);
				SetupDiDestroyDeviceInfoList(hDevInfo);
				return   -1;
			}
			/*
			 *\?usb#vid_vvvv&pid_pppp#ssss#{gggggggg-gggg-gggg-gggg-gggggggggggg}
			 *vvvv is the USB vendor ID represented in 4 hexadecimal characters.
			 *pppp is the USB product ID represented in 4 hexadecimal characters.
			 *ssss is the USB serial string represented in n characters.
			 *gggggggg-gggg-gggg-gggg-gggggggggggg is the device interface GUID that is used to link applications to device with specific drivers loaded.
			 */
			//Get VID, PID, iSerialnumber, GUID
			sscanf(ClassDeviceData->DevicePath, ("\\\\?\\usb#vid_%04X&pid_%04X%#%[^#]"),&idVendor,&idProduct, iSerialNumber);
			if(idVendor == 0x096E && idProduct == 0x0503)
			{
				printf("%s\n", iSerialNumber);
				break;
			}else{
				printf("Detect USB device:\n%s\n", ClassDeviceData->DevicePath);
			}
			free(ClassDeviceData);
			
		}
		dwIndex++;
	}

	SetupDiDestroyDeviceInfoList(hDevInfo);

	return 0;
}

