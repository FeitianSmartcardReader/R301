namespace Simple_PCSC
{
    partial class Simple_PCSC
    {
        /// <summary>
        /// 
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simple_PCSC));
            this.m_Apdu = new System.Windows.Forms.TextBox();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.ReaderReleaseContextbutton = new System.Windows.Forms.Button();
            this.ReaderDisconnectButton = new System.Windows.Forms.Button();
            this.ReaderTransmitButton = new System.Windows.Forms.Button();
            this.ReaderConnectButton = new System.Windows.Forms.Button();
            this.ReaderInitButton = new System.Windows.Forms.Button();
            this.fApdu = new System.Windows.Forms.GroupBox();
            this.StartPpollingButton = new System.Windows.Forms.Button();
            this.StopPpollingButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ReaderListComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReaderStatusButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fApdu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_Apdu
            // 
            this.m_Apdu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_Apdu.Location = new System.Drawing.Point(3, 21);
            this.m_Apdu.MaxLength = 0;
            this.m_Apdu.Name = "m_Apdu";
            this.m_Apdu.Size = new System.Drawing.Size(305, 21);
            this.m_Apdu.TabIndex = 8;
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(6, 23);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(305, 215);
            this.mMsg.TabIndex = 22;
            this.mMsg.Text = "";
            // 
            // ReaderReleaseContextbutton
            // 
            this.ReaderReleaseContextbutton.Enabled = false;
            this.ReaderReleaseContextbutton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderReleaseContextbutton.Location = new System.Drawing.Point(376, 424);
            this.ReaderReleaseContextbutton.Name = "ReaderReleaseContextbutton";
            this.ReaderReleaseContextbutton.Size = new System.Drawing.Size(181, 40);
            this.ReaderReleaseContextbutton.TabIndex = 19;
            this.ReaderReleaseContextbutton.Text = "SCardReleaseContext";
            this.ReaderReleaseContextbutton.Click += new System.EventHandler(this.ReaderReleaseContextbutton_Click);
            // 
            // ReaderDisconnectButton
            // 
            this.ReaderDisconnectButton.Enabled = false;
            this.ReaderDisconnectButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderDisconnectButton.Location = new System.Drawing.Point(376, 367);
            this.ReaderDisconnectButton.Name = "ReaderDisconnectButton";
            this.ReaderDisconnectButton.Size = new System.Drawing.Size(181, 40);
            this.ReaderDisconnectButton.TabIndex = 18;
            this.ReaderDisconnectButton.Text = "SCardDisconnect";
            this.ReaderDisconnectButton.Click += new System.EventHandler(this.ReaderDisconnectButton_Click);
            // 
            // ReaderTransmitButton
            // 
            this.ReaderTransmitButton.Enabled = false;
            this.ReaderTransmitButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderTransmitButton.Location = new System.Drawing.Point(376, 147);
            this.ReaderTransmitButton.Name = "ReaderTransmitButton";
            this.ReaderTransmitButton.Size = new System.Drawing.Size(181, 40);
            this.ReaderTransmitButton.TabIndex = 17;
            this.ReaderTransmitButton.Text = "SCardTransmit";
            this.ReaderTransmitButton.Click += new System.EventHandler(this.ReaderTransmitButton_Click);
            // 
            // ReaderConnectButton
            // 
            this.ReaderConnectButton.Enabled = false;
            this.ReaderConnectButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderConnectButton.Location = new System.Drawing.Point(375, 90);
            this.ReaderConnectButton.Name = "ReaderConnectButton";
            this.ReaderConnectButton.Size = new System.Drawing.Size(181, 40);
            this.ReaderConnectButton.TabIndex = 16;
            this.ReaderConnectButton.Text = "SCardConnect";
            this.ReaderConnectButton.Click += new System.EventHandler(this.ReaderConnectButton_Click);
            // 
            // ReaderInitButton
            // 
            this.ReaderInitButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReaderInitButton.Location = new System.Drawing.Point(375, 33);
            this.ReaderInitButton.Name = "ReaderInitButton";
            this.ReaderInitButton.Size = new System.Drawing.Size(181, 40);
            this.ReaderInitButton.TabIndex = 15;
            this.ReaderInitButton.Text = "SCardListReaders";
            this.ReaderInitButton.Click += new System.EventHandler(this.ReaderInitButton_Click);
            // 
            // fApdu
            // 
            this.fApdu.Controls.Add(this.m_Apdu);
            this.fApdu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fApdu.Location = new System.Drawing.Point(21, 113);
            this.fApdu.Name = "fApdu";
            this.fApdu.Size = new System.Drawing.Size(315, 56);
            this.fApdu.TabIndex = 21;
            this.fApdu.TabStop = false;
            this.fApdu.Text = "APDU Input";
            // 
            // StartPpollingButton
            // 
            this.StartPpollingButton.Enabled = false;
            this.StartPpollingButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.StartPpollingButton.Location = new System.Drawing.Point(375, 253);
            this.StartPpollingButton.Name = "StartPpollingButton";
            this.StartPpollingButton.Size = new System.Drawing.Size(181, 40);
            this.StartPpollingButton.TabIndex = 23;
            this.StartPpollingButton.Text = "StartPpolling";
            this.StartPpollingButton.UseVisualStyleBackColor = true;
            this.StartPpollingButton.Click += new System.EventHandler(this.StartPpollingButton_Click);
            // 
            // StopPpollingButton
            // 
            this.StopPpollingButton.Enabled = false;
            this.StopPpollingButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.StopPpollingButton.Location = new System.Drawing.Point(375, 310);
            this.StopPpollingButton.Name = "StopPpollingButton";
            this.StopPpollingButton.Size = new System.Drawing.Size(181, 40);
            this.StopPpollingButton.TabIndex = 24;
            this.StopPpollingButton.Text = "StopPpolling";
            this.StopPpollingButton.UseVisualStyleBackColor = true;
            this.StopPpollingButton.Click += new System.EventHandler(this.StopPpollingButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ReaderListComboBox
            // 
            this.ReaderListComboBox.Location = new System.Drawing.Point(24, 39);
            this.ReaderListComboBox.Name = "ReaderListComboBox";
            this.ReaderListComboBox.Size = new System.Drawing.Size(305, 20);
            this.ReaderListComboBox.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mMsg);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(21, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 244);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Output";
            // 
            // ReaderStatusButton
            // 
            this.ReaderStatusButton.Enabled = false;
            this.ReaderStatusButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.ReaderStatusButton.Location = new System.Drawing.Point(375, 200);
            this.ReaderStatusButton.Name = "ReaderStatusButton";
            this.ReaderStatusButton.Size = new System.Drawing.Size(181, 40);
            this.ReaderStatusButton.TabIndex = 26;
            this.ReaderStatusButton.Text = "SCardStatus";
            this.ReaderStatusButton.UseVisualStyleBackColor = true;
            this.ReaderStatusButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(27, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 40);
            this.button1.TabIndex = 27;
            this.button1.Text = "Clear Output window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Simple_PCSC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 504);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ReaderStatusButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReaderListComboBox);
            this.Controls.Add(this.StopPpollingButton);
            this.Controls.Add(this.StartPpollingButton);
            this.Controls.Add(this.ReaderReleaseContextbutton);
            this.Controls.Add(this.ReaderDisconnectButton);
            this.Controls.Add(this.ReaderTransmitButton);
            this.Controls.Add(this.ReaderConnectButton);
            this.Controls.Add(this.ReaderInitButton);
            this.Controls.Add(this.fApdu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Simple_PCSC";
            this.Text = "Simple_PCSC";
            this.fApdu.ResumeLayout(false);
            this.fApdu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox m_Apdu;
        internal System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.Button ReaderReleaseContextbutton;
        internal System.Windows.Forms.Button ReaderDisconnectButton;
        internal System.Windows.Forms.Button ReaderTransmitButton;
        internal System.Windows.Forms.Button ReaderConnectButton;
        internal System.Windows.Forms.Button ReaderInitButton;
        internal System.Windows.Forms.GroupBox fApdu;
        private System.Windows.Forms.Button StopPpollingButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StartPpollingButton;
        internal System.Windows.Forms.ComboBox ReaderListComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ReaderStatusButton;
        private System.Windows.Forms.Button button1;
    }
}

