using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple_PCSC
{
    public partial class Simple_PCSC : Form
    {
        uint PRT;
        int ATRLen, ReaderLen, dwState;
        byte[] ATR = new byte[33];
        SCARD_IO_REQUEST IORequest;
        public int hCard, hContext;                     // hContext=10
        public int Protocol, ReaderCount;
        public string CardReaderDesc = "";              // Save the device descriptor string

        public Simple_PCSC()
        {
            InitializeComponent();
        }


        // Byte[] to HexString
        static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static string ToHexString(byte[] bytes, int BytesLength)
        {
            char[] chars = new char[bytes.Length * 5];
            for (int i = 0; i < BytesLength; i++)
            {
                int b = bytes[i];
                chars[i * 3 + 0] = hexDigits[b >> 4];
                chars[i * 3 + 1] = hexDigits[b & 0xF];
                chars[i * 3 + 2] = ' ';
            }
            return new string(chars);
        }

        // HexString to Byte[]
        private static byte[] ToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[(hexString.Length / 2)];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return returnBytes;
        }

        void DisplayOut(int mType, long msgCode, string PrintText)
        {
            switch (mType)
            {
                case 0:                                  // Notifications only
                    mMsg.SelectionColor = Color.Green;
                    break;
                case 1:                                  // Error Messages
                    mMsg.SelectionColor = Color.Red;
                    PrintText = ModWinsCard.GetScardErrMsg((int)msgCode);
                    break;
                case 2:                                  // Input data
                    mMsg.SelectionColor = Color.Black;
                    PrintText = "< " + PrintText;
                    break;
                case 3:                                  // Output data
                    mMsg.SelectionColor = Color.Black;
                    PrintText = "> " + PrintText;
                    break;
                case 4:                                  // Critical Errors
                    mMsg.SelectionColor = Color.Red;
                    break;
            }

            mMsg.SelectedText = PrintText + "\n";
            mMsg.SelectionStart = mMsg.Text.Length;
            mMsg.SelectionColor = Color.Black;
        }

        private bool ListCardReaderDesc()
        {
            int RetListCardReader;
            int PccHReaders = 0;


            //Establish Context
            RetListCardReader = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);

            if (RetListCardReader != ModWinsCard.SCARD_S_SUCCESS)
            {
                return false;
            }

            // 2. List PC/SC card readers installed in the system
            RetListCardReader = ModWinsCard.SCardListReaders(hContext, null, null, ref PccHReaders);

            if (RetListCardReader != ModWinsCard.SCARD_S_SUCCESS)
            {
                return false;
            }

            string ReaderList = "" + Convert.ToChar(0);
            int Index;
            string ReaderName = "";
            ReaderName = "";
            Index = 0;
            PccHReaders = 255;
            byte[] ReadersList = new byte[PccHReaders];

            // Fill reader list
            RetListCardReader = ModWinsCard.SCardListReaders(hContext, null, ReadersList, ref PccHReaders);

            if (RetListCardReader != ModWinsCard.SCARD_S_SUCCESS)
            {
                return false;
            }
            if (CardReaderDesc == "")
            {
                ReaderListComboBox.Items.Clear();
            }
            //Convert reader buffer to string
            while (ReadersList[Index] != 0)
            {

                while (ReadersList[Index] != 0)
                {
                    ReaderName = ReaderName + (char)ReadersList[Index];
                    Index = Index + 1;
                }

                if (CardReaderDesc == "")
                {
                    //Add reader name to list
                    //  if (string.Compare(ReaderName, bR301_ReaderName) == 0)
                    {
                        ReaderListComboBox.Items.Add(ReaderName);
                    }
                }
                else
                {
                    if (string.Compare(ReaderName, CardReaderDesc) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        ReaderListComboBox.Items.Clear();
                        ReaderListComboBox.Items.Add(ReaderName);
                        return true;
                    }
                }
                ReaderName = "";
                Index = Index + 1;
            }
            return true;
        }

        private void ReaderInitButton_Click(object sender, EventArgs e)
        {
            ReaderListComboBox.Items.Clear();
            ReaderListComboBox.Text = "";
            CardReaderDesc = "";


            if (ListCardReaderDesc())
            {
                ReaderListComboBox.SelectedIndex = 0;
                DisplayOut(0, 0, "SCardListReaders... OK");
                ReaderConnectButton.Enabled = true;
                StartPpollingButton.Enabled = true;
                StopPpollingButton.Enabled = true;

                ReaderReleaseContextbutton.Enabled = true;
            }
            else
            {
                DisplayOut(4, 0, "SCardListReaders... ERR");
            }
            
        }

        private void ReaderConnectButton_Click(object sender, EventArgs e)
        {
            CardReaderDesc = ReaderListComboBox.SelectedItem.ToString();

            // Don't use SCARD_SHARE_DIRECT , After Ppolling the transmit return 0x00000016 Unknown Error
            int retCode = ModWinsCard.SCardConnect(this.hContext, CardReaderDesc, ModWinsCard.SCARD_SHARE_SHARED,
                                                    ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                DisplayOut(1, retCode, "");

            else
            {
                DisplayOut(0, 0, "SCardConnect...OK");
                ReaderConnectButton.Enabled = false;
                StartPpollingButton.Enabled = false;
                StopPpollingButton.Enabled = false;

                ReaderTransmitButton.Enabled = true;
                ReaderDisconnectButton.Enabled = true;
                ReaderReleaseContextbutton.Enabled = true;
                ReaderStatusButton.Enabled = true;
            }
        }

        private void ReaderTransmitButton_Click(object sender, EventArgs e)
        {
            if (m_Apdu.Text == "")
            {
                DisplayOut(4, 0, "No data input");
                return;
            }

            m_Apdu.Text = (m_Apdu.Text.Replace(" ", ""));
            if (m_Apdu.Text.Length < 10)
            {
                DisplayOut(4, 0, "Insufficient data input");
                return;
            }

            if ((m_Apdu.Text.Length % 2) != 0)
            {
                DisplayOut(4, 0, "Invalid data input, uneven number of characters");
                m_Apdu.Focus();
                return;
            }

            byte[] SendBuff = new byte[1024];
            byte[] RecvBuff = new byte[1024];
            int SendLen = 1024;
            int RecvLen = 1024;

            SendLen = m_Apdu.Text.Length / 2;
            byte[] tempBuff = ToHexByte(m_Apdu.Text);
            Array.Copy(tempBuff, SendBuff, SendLen);

            IORequest.dwProtocol = Protocol;
            IORequest.cbPciLength = 8;
            int retCode = ModWinsCard.SCardTransmit(hCard, ref IORequest, ref SendBuff[0], SendLen, ref IORequest, ref RecvBuff[0], ref RecvLen);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                DisplayOut(1, retCode, "");
            }
            else
            {
                string tempStr = ToHexString(RecvBuff, RecvLen);
                DisplayOut(0, 0, tempStr);
            }
        }

        private void ReaderDisconnectButton_Click(object sender, EventArgs e)
        {
            int retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                DisplayOut(1, retCode, "");
            else
            {
                DisplayOut(0, 0, "SCardDisconnect...OK");
                ReaderConnectButton.Enabled = true;
                StartPpollingButton.Enabled = true;
                StopPpollingButton.Enabled = true;

                ReaderStatusButton.Enabled = false;
                ReaderTransmitButton.Enabled = false;
                ReaderDisconnectButton.Enabled = false;
            }
        }

        private void ReaderReleaseContextbutton_Click(object sender, EventArgs e)
        {
            CardReaderDesc = "";
            int retCode = ModWinsCard.SCardReleaseContext(hContext);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                DisplayOut(1, retCode, "");
            else
            {
                DisplayOut(0, 0, "SCardReleaseContext...OK");

                ReaderConnectButton.Enabled = false;
                StartPpollingButton.Enabled = false;
                StopPpollingButton.Enabled = false;

                ReaderTransmitButton.Enabled = false;
                ReaderDisconnectButton.Enabled = false;
                ReaderReleaseContextbutton.Enabled = false;
                ReaderStatusButton.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // perform if the card is inserted/not inserted on the reader 

            // Don't connection before to connect card reader.
            // If the connection card reader to make a card, and after make smart card device manager to suspend work, and can't get correct card slots.
            SCARD_READERSTATE ReaderState = new SCARD_READERSTATE();
            ReaderState.RdrName = ReaderListComboBox.SelectedItem.ToString();
            
            mMsg.Clear();

            int retCode = ModWinsCard.SCardGetStatusChange(this.hContext, -1, ref ReaderState, 1);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                DisplayOut(1, retCode, "");
            }
            else
            {
                if ((ReaderState.RdrEventState & ModWinsCard.SCARD_STATE_PRESENT) != 0)
                {
                    DisplayOut(0, 0, "Card Inserted");
                }
                else
                {
                    DisplayOut(0, 0, "Card Removed");
                }
            }
        }

        private void StartPpollingButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            ReaderConnectButton.Enabled = false;
            StartPpollingButton.Enabled = false;
            ReaderReleaseContextbutton.Enabled = false;
        }

        private void StopPpollingButton_Click(object sender, EventArgs e)
        {
            StartPpollingButton.Enabled = true;
            ReaderConnectButton.Enabled = true;
            ReaderReleaseContextbutton.Enabled = true;
            timer1.Enabled = false;
            DisplayOut(0, 0, "Stop Ppolling");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            short index;
            string Temp;
            ATRLen = 33;
            CardReaderDesc = ReaderListComboBox.SelectedItem.ToString();

            //perform the Card Status
            int retcode = ModWinsCard.SCardStatus(this.hCard, CardReaderDesc, ref ReaderLen, ref dwState, ref Protocol, ref ATR[0], ref ATRLen);

            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                DisplayOut(1, retcode, "");
            }
            else
            {
                DisplayOut(0, 0, "SCardStatus OK...");

                // select for the protocol
                switch (Protocol)
                {
                    case 0: PRT = ModWinsCard.SCARD_PROTOCOL_UNDEFINED;
                        DisplayOut(0, 0, "Active Protocol Undefined");
                        break;
                    case 1: PRT = ModWinsCard.SCARD_PROTOCOL_T0;
                        DisplayOut(0, 0, "Active Protocol T0");
                        break;
                    case 2: PRT = ModWinsCard.SCARD_PROTOCOL_T1;
                        DisplayOut(0, 0, "Active Protocol T1");
                        break;
                }

                Temp = "ATR:";

                for (index = 0; index < ATRLen; index++)
                    Temp = Temp + " " + string.Format("{0:X2}", ATR[index]);

                // Display ATR value 
                DisplayOut(0, 0, Temp);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mMsg.Clear();
        }

    }
}
