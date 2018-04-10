//
//  main.c
//  testpcsc
//
//  Created by Hongbin Wang on 06/02/2017.
//  Copyright © 2017 Hongbin Wang. All rights reserved.
//

#include <stdio.h>
#include <PCSC/wintypes.h>
#include <PCSC/winscard.h>


typedef unsigned int uint32_t;
#define PANIC 0
#define DONT_PANIC 1


#define SCARD_ATTR_ATR_STRING 590595

void test_rv(LONG rv, SCARDCONTEXT hContext, int dont_panic);
void test_rv(LONG rv, SCARDCONTEXT hContext, int dont_panic)
{
    if (rv != SCARD_S_SUCCESS)
    {
        if (dont_panic)
            printf( "%s (don't panic)\n" , pcsc_stringify_error(rv));
        else
        {
            printf( "%s\n" , pcsc_stringify_error(rv));
            SCardReleaseContext(hContext);
        }
    }
    else
        puts(pcsc_stringify_error(rv));
}

int main(int argc, char **argv)
{
    SCARDHANDLE hCard;
    SCARDCONTEXT hContext;
    SCARD_READERSTATE_A rgReaderStates[1];
    DWORD dwReaderLen, dwState, dwProt, dwAtrLen;
    DWORD dwPref, dwReaders = 0;
    char *pcReaders, *mszReaders;
    unsigned char pbAtr[MAX_ATR_SIZE];
    char *mszGroups;
    DWORD dwGroups = 0;
    LONG rv;
    DWORD i;
    int p, iReader;
    int iList[16];
    SCARD_IO_REQUEST pioRecvPci;
    SCARD_IO_REQUEST pioSendPci;
    unsigned char bSendBuffer[MAX_BUFFER_SIZE];
    unsigned char bRecvBuffer[MAX_BUFFER_SIZE];
    DWORD send_length, length;
    
    printf("\nMUSCLE PC/SC Lite unitary test Program\n\n");
    
    
    printf("Testing SCardEstablishContext\t: ");
    rv = SCardEstablishContext(SCARD_SCOPE_SYSTEM, NULL, NULL, &hContext);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardGetStatusChange \n");
    printf("Please insert a working reader\t: ");
    fflush(stdout);
    rv = SCardGetStatusChange(hContext, INFINITE, 0, 0);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardListReaderGroups\t: ");
    rv = SCardListReaderGroups(hContext, 0, &dwGroups);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardListReaderGroups\t: ");
    mszGroups = calloc(dwGroups, sizeof(char));
    rv = SCardListReaderGroups(hContext, mszGroups, &dwGroups);
    test_rv(rv, hContext, PANIC);
    
    /*
     * Have to understand the multi-string here
     */
    p = 0;
    for (i = 0; i+1 < dwGroups; i++)
    {
        ++p;
        printf( "Group %02d: %s\n" , p, &mszGroups[i]);
        iList[p] = i;
        while (mszGroups[++i] != 0) ;
    }
    
wait_for_card_again:
    printf("Testing SCardListReaders\t: ");
    
    mszGroups = NULL;
    rv = SCardListReaders(hContext, mszGroups, 0, &dwReaders);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardListReaders\t: ");
    mszReaders = calloc(dwReaders, sizeof(char));
    rv = SCardListReaders(hContext, mszGroups, mszReaders, &dwReaders);
    test_rv(rv, hContext, PANIC);
    
    /*
     * Have to understand the multi-string here
     */
    p = 0;
    for (i = 0; i+1 < dwReaders; i++)
    {
        ++p;
        printf( "Reader %02d: %s\n" , p, &mszReaders[i]);
        iList[p] = i;
        while (mszReaders[++i] != 0) ;
    }
    
    if (p > 1)
        do
        {
            char input[80];
            
            printf("Enter the reader number\t\t: ");
            fgets(input, sizeof(input), stdin);
            sscanf(input, "%d", &iReader);
            
            if (iReader > p || iReader <= 0)
                printf("Invalid Value - try again\n");
        }
    while (iReader > p || iReader <= 0);
    else
        iReader = 1;
    
    rgReaderStates[0].szReader = &mszReaders[iList[iReader]];
    rgReaderStates[0].dwCurrentState = SCARD_STATE_EMPTY;
    
    printf("Waiting for card insertion\t: ");
    fflush(stdout);
    rv = SCardGetStatusChange(hContext, INFINITE, rgReaderStates, 1);
    test_rv(rv, hContext, PANIC);
    if (SCARD_STATE_EMPTY == rgReaderStates[0].dwEventState)
    {
        printf("\nA reader has been connected/disconnected\n");
        goto wait_for_card_again;
    }
    
    printf("Testing SCardConnect\t\t: ");
    rv = SCardConnect(hContext, &mszReaders[iList[iReader]],
                      SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1,
                      &hCard, &dwPref);
    test_rv(rv, hContext, PANIC);
    
    switch(dwPref)
    {
        case SCARD_PROTOCOL_T0:
            pioSendPci = *SCARD_PCI_T0;
            break;
        case SCARD_PROTOCOL_T1:
            pioSendPci = *SCARD_PCI_T1;
            break;
        default:
            printf("Unknown protocol\n");
            return -1;
    }
    
    /* APDU select file */
    printf("Select file:");
    send_length = 7;
    memcpy(bSendBuffer, "\x00\xA4\x00\x00\x02\x3F\x00", send_length);
    for (i=0; i<send_length; i++)
        printf(" %02X", bSendBuffer[i]);
    printf("\n");
    length = sizeof(bRecvBuffer);
    
    printf("Testing SCardTransmit\t\t: ");
    rv = SCardTransmit(hCard, &pioSendPci, bSendBuffer, send_length,
                       &pioRecvPci, bRecvBuffer, &length);
    printf("%s\n", pcsc_stringify_error(rv));
    printf(" card response:" );
    for (i=0; i<length; i++)
        printf(" %02X", bRecvBuffer[i]);
    printf("\n" );
    
    /* APDU ramdom */
    printf("Get 8 bytes Ramdom Number:");
    send_length = 5;
    memcpy(bSendBuffer, "\x00\x84\x00\x00\x08", send_length);
    for (i=0; i<send_length; i++)
        printf(" %02X", bSendBuffer[i]);
    printf("\n");
    length = sizeof(bRecvBuffer);
    
    printf("Testing SCardTransmit\t\t: ");
    rv = SCardTransmit(hCard, &pioSendPci, bSendBuffer, send_length,
                       &pioRecvPci, bRecvBuffer, &length);
    printf("%s\n", pcsc_stringify_error(rv));
    printf(" card response:" );
    for (i=0; i<length; i++)
        printf(" %02X", bRecvBuffer[i]);
    printf("\n" );
    
    
    
    printf("Testing SCardGetAttrib\t\t: ");
    rv = SCardGetAttrib(hCard, SCARD_ATTR_ATR_STRING, NULL, &dwAtrLen);
    test_rv(rv, hContext, DONT_PANIC);
    if (rv == SCARD_S_SUCCESS)
        printf("SCARD_ATTR_ATR_STRING length: "  "%ld\n" , dwAtrLen);
    
    printf("Testing SCardGetAttrib\t\t: ");
    dwAtrLen = sizeof(pbAtr);
    rv = SCardGetAttrib(hCard, SCARD_ATTR_ATR_STRING, pbAtr, &dwAtrLen);
    test_rv(rv, hContext, DONT_PANIC);
    if (rv == SCARD_S_SUCCESS)
    {
        printf("SCARD_ATTR_ATR_STRING: " );
        for (i = 0; i < dwAtrLen; i++)
            printf("%02X ", pbAtr[i]);
        printf("\n" );
    }
    
    
    printf("Testing SCardStatus\t\t: ");
    
    dwReaderLen = 100;
    pcReaders   = malloc(sizeof(char) * 100);
    dwAtrLen    = MAX_ATR_SIZE;
    
    rv = SCardStatus(hCard, pcReaders, &dwReaderLen, &dwState, &dwProt,
                     pbAtr, &dwAtrLen);
    test_rv(rv, hContext, PANIC);
    
    printf("Current Reader Name\t\t: "  "%s\n" , pcReaders);
    printf("Current Reader State\t\t: "  "0x%.4x\n" , dwState);
    printf("Current Reader Protocol\t\t: T="  "%u\n" , dwProt - 1);
    printf("Current Reader ATR Size\t\t: "  "%u"  " bytes\n",
           dwAtrLen);
    printf("Current Reader ATR Value\t: " );
    
    for (i = 0; i < dwAtrLen; i++)
    {
        printf("%02X ", pbAtr[i]);
    }
    printf( "\n");
    
    if (rv != SCARD_S_SUCCESS)
    {
        SCardDisconnect(hCard, SCARD_RESET_CARD);
        SCardReleaseContext(hContext);
    }
    
    
    //sleep(1);
    printf("Testing SCardReconnect\t\t: ");
    rv = SCardReconnect(hCard, SCARD_SHARE_SHARED,
                        SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1, SCARD_UNPOWER_CARD, &dwPref);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardDisconnect\t\t: ");
    rv = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
    test_rv(rv, hContext, PANIC);
    
    printf("Testing SCardReleaseContext\t: ");
    rv = SCardReleaseContext(hContext);
    test_rv(rv, hContext, PANIC);
    
    printf("\n");
    printf("PC/SC Test Completed Successfully !\n");
    
    return 0;
}
