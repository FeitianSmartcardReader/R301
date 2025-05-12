# Developer Manual(Pseudo-APDU):
## Revision History:

| Revision Date | Version | Revision Content |
| --- | --- | --- |
| August 13, 2024 | V1.0 | Initial Release |
| October 22, 2024 | V1.1 | Modified Generate UID logic |
| October 22, 2024 | V1.1 | Modified Erase UID logic |

### 1.0 Overview

This document introduces the user manual for the Feitian R301 card reader product, and it works for R301T1 at the moment, and in future, all new Reader from Feitian will support PPDU by default.

In this manual, the feature of obtaining reader information is executed on the Interface Device(IFD) by issuing commands through a Pseudo-Application Protocol Data Unit (PPDU), instead of using the ESCAPE API. On the Windows system, some customers are required to modify the registry to enable the escape channel. To avoid this, we use PPDUs. However, the ESCAPE command has the same functionality. You can still use the ESCAPE command to obtain reader information without any issues. In this manual, we only list the PPDU-related information. 

### 1.1 Conventions

Areas marked with "Note" in the document generally require special attention. In most cases, the content marked with "Note" should be followed. If there is any ambiguity, please consult with the product responsible personnel.

Default values mentioned in the document, if not specified, default to '00'.

Hexadecimal values in the document are represented by double or single quotes. For example, the CPLC tag is represented as "9F7F", and 0x00 is represented as '00'.

### 1.2 Feature Execution by Pseudo-APDU

A feature is commanded by special APDU's, called Pseudo APDU (PPDU). The Pseudo-APDU command is in a data format that closely resembles an APDU for cards:

This Pseudo-APDU is defined as a command header (CLA/INS/P1/P2) and an optional command body.

### Abbreviations and Terminology

| Abbreviation/Term | Explanation | Remarks |
| --- | --- | --- |
| PPDU | Pseudo-APDU |  |
| AES | Advanced Encryption Standard |  |
| APDU | Application Protocol Data Unit |  |
| CLA | Class byte of the command message |  |
| FT | Feitian Technologies Co., Ltd. |  |
| INS | Instruction byte of command message |  |
| Lc | Exact length of data in a case 3 or case 4 command |  |
| Le | Maximum length of data expected in response to a case 2 or case 4 command |  |
| P1 | Reference control parameter 1 |  |
| P2 | Reference control parameter 2 |  |
| SW1 | Status Word One |  |
| SW2 | Status Word Two |  |

### Chapter 2 Command Set

This chapter lists all private commands applicable to firmware versions V1.4 and later. The commands listed in this chapter can also be sent to the card reader via escape commands.

### 2.1 GET Commands

### 2.1.1 GET Vendor name

This command is used to obtain the Vendor string from the device descriptor.

### 2.1.1.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '01' | Vendor name |
| Lc | - | - |
| Data | - |  |
| Le | '00' | Expected data length in response |

Table 2-1 GET Vendor name Command Message

### 2.1.1.2 Response Message

### 2.1.1.2.1 Response Message Data Field

The data format is consistent with the Vendor string in the device descriptor. For example, when the device Vendor string is 'Vendor', the command returns: 56 00 65 00 6E 00 64 00 6F 00 72 00.

### 2.1.1.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-2 GET Vendor name Status Codes

### 2.1.2 GET VID

This command is used to obtain the VID from the device descriptor.

### 2.1.2.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '02' | VID |
| Lc | - | - |
| Data | - |  |
| Le | '02' | Expected data length in response |

Table 2-3 GET VID Command Message

### 2.1.2.2 Response Message

### 2.1.2.2.1 Response Message Data Field

2-byte VID in big-endian format. For example, when the device VID is '085D', the command returns: 08 5D.

### 2.1.2.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-4 GET VID Status Codes

### 2.1.3 GET Product name

This command is used to obtain the Product string from the device descriptor.

### 2.1.3.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '03' | Product name |
| Lc | - | - |
| Data | - |  |
| Le | '00' | Expected data length in response |

Table 2-5 GET Product name Command Message

### 2.1.3.2 Response Message

### 2.1.3.2.1 Response Message Data Field

The data format is consistent with the Product string in the device descriptor. For example, when the device Product string is 'Pro', the command returns: 50 00 72 00 6F 00.

### 2.1.3.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-6 GET Product name Status Codes

### 2.1.4 GET PID

This command is used to obtain the PID from the device descriptor.

### 2.1.4.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '04' | PID |
| Lc | - | - |
| Data | - |  |
| Le | '02' | Expected data length in response |

Table 2-7 GET PID Command Message

### 2.1.4.2 Response Message

### 2.1.4.2.1 Response Message Data Field

2-byte PID in big-endian format. For example, when the device PID is '086F', the command returns: 08 6F.

### 2.1.4.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-8 GET PID Status Codes

### 2.1.5 GET Sequence

This command is used to obtain the product sequence string from the device descriptor.

### 2.1.5.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '05' | Sequence number |
| Lc | - | - |
| Data | - |  |
| Le | '00' | Expected data length in response |

Table 2-9 GET Sequence Command Message

### 2.1.5.2 Response Message

### 2.1.5.2.1 Response Message Data Field

The data format is consistent with the product sequence string in the device descriptor. For example, when the device product sequence string is '1234', the command returns: 31 00 32 00 33 00 34 00.

### 2.1.5.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-10 GET Sequence Status Codes

### 2.1.6 GET Firmware version

This command is used to obtain the firmware version number.

### 2.1.6.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '06' | Firmware version |
| Lc | - | - |
| Data | - |  |
| Le | '02' | Expected data length in response |

Table 2-11 GET Firmware version Command Message

### 2.1.6.2 Response Message

### 2.1.6.2.1 Response Message Data Field

2-byte version number. The first byte is the major version, and the second byte is the minor version. For example, when the return value is 0104, the version should be parsed as 01.04.

### 2.1.6.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-12 GET Firmware version Status Codes

### 2.1.7 GET Reader type

This command is used to obtain the card reader type.

### 2.1.7.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | '07' | Reader type |
| Lc | - | - |
| Data | - |  |
| Le | '04' | Expected data length in response |

Table 2-13 GET Reader type Command Message

### 2.1.7.2 Response Message

### 2.1.7.2.1 Response Message Data Field

4-byte ASCII code representing the card reader type number. This card reader will always return '312E3130', which is the CCID protocol version number: 1.10.

### 2.1.7.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-14 GET Reader type Status Codes

### 2.1.8 GET UID

This command is used to obtain the UID. The UID is an 8-byte data string. UID stands for User ID, and its purpose is to allow users to generate custom IDs to manage their card readers. For example, distributors can use UIDs to control users, such as binding relationships between software apps, ensuring that only specified users can use particular app software.

### 2.1.8.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '01' | GET command |
| P2 | 'FF' | UID |
| Lc | - | - |
| Data | - |  |
| Le | '08' | Expected data length in response |

Table 2-15 GET UID Command Message

### 2.1.8.2 Response Message

### 2.1.8.2.1 Response Message Data Field

8-byte UID.

### 2.1.8.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6E' | '31' | Unknown error |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '90' | '00' | Success |

Table 2-16 GET UID Status Codes

### 2.2 Generate UID

This command is used to generate a UID. If a UID already exists (i.e., the UID value is not 'FFFFFFFFFFFFFFFF'), the command will fail, and the UID must be erased first.

### 2.2.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '02' | SET command |
| P2 | 'FF' | Generate UID |
| Lc | 'lc' | Seed length, range [0,48] |
| Data | 'xx...XX' | Seed |
| Le | - | - |

Table 2-17 Generate UID Command Message

### 2.2.1.1 Command Message Data Field

The data in the Lc byte is used as the seed for generating the UID.

### 2.2.2 Response Message

### 2.2.2.1 Response Message Data Field

None.

### 2.2.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '6E' | '30' | UID not erased or illegal seed length |
| '90' | '00' | Success |

Table 2-18 Generate UID Status Codes

### 2.3 Erase UID

This command is used to erase the UID. After erasure, the UID becomes FFFFFFFFFFFFFFFF. The default seed cannot be erased. By default, customers need to generate a new UID with a new seed. The erase command requires the old seed to erase, which is the seed used to calculate the existing UID (i.e., the seed used in section 2.2).

### 2.3.1 Command Message

| Code | Value | Meaning |
| --- | --- | --- |
| CLA | 'FF' | Default |
| INS | '9A' | Default |
| P1 | '02' | SET command |
| P2 | 'FE' | Erase UID |
| Lc | 'lc' | Data length |
| Data | 'xx...XX' | Seed |
| Le | - | - |

Table 2-19 Erase UID Command Message

### 2.3.1.1 Command Message Data Field

Seed.

### 2.3.2 Response Message

### 2.3.2.1 Response Message Data Field

None.

### 2.3.2.2 Response Message Status

Successful execution should be indicated by the status byte '9000'.

This command may return the following error statuses:

| SW1 | SW2 | Meaning |
| --- | --- | --- |
| '6D' | '00' | CLA or INS does not meet requirements |
| '6A' | '86' | P1 or P2 does not meet requirements |
| '6E' | '32' | Seed mismatch |
| '90' | '00' | Success |

Table 2-20 Erase UID Status Codes