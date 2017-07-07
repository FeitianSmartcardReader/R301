<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form301
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ComboBoxReaders = New System.Windows.Forms.ComboBox
        Me.ButtonConnect = New System.Windows.Forms.Button
        Me.ButtonInit = New System.Windows.Forms.Button
        Me.RichTextBoxATR = New System.Windows.Forms.RichTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxPin = New System.Windows.Forms.TextBox
        Me.ButtonVerify = New System.Windows.Forms.Button
        Me.ButtonChange = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.ButtonReadMM = New System.Windows.Forms.Button
        Me.ButtonUpdateMM = New System.Windows.Forms.Button
        Me.ButtonReadPM = New System.Windows.Forms.Button
        Me.ButtonWritePM = New System.Windows.Forms.Button
        Me.ButtonReadSM = New System.Windows.Forms.Button
        Me.ButtonUpdateSM = New System.Windows.Forms.Button
        Me.ButtonCompareVD = New System.Windows.Forms.Button
        Me.ReadMMAddr = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.ReadMMReplyLen = New System.Windows.Forms.TextBox
        Me.UpdateMMAddr = New System.Windows.Forms.TextBox
        Me.UpdateMMData = New System.Windows.Forms.TextBox
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.TextBox7 = New System.Windows.Forms.TextBox
        Me.TextBox8 = New System.Windows.Forms.TextBox
        Me.ReadPMReplyLen = New System.Windows.Forms.TextBox
        Me.WritePMAddr = New System.Windows.Forms.TextBox
        Me.WritePMData = New System.Windows.Forms.TextBox
        Me.TextBox12 = New System.Windows.Forms.TextBox
        Me.TextBox13 = New System.Windows.Forms.TextBox
        Me.TextBox14 = New System.Windows.Forms.TextBox
        Me.TextBox15 = New System.Windows.Forms.TextBox
        Me.UpdateSMAddr = New System.Windows.Forms.TextBox
        Me.UpdateSMData = New System.Windows.Forms.TextBox
        Me.TextBox18 = New System.Windows.Forms.TextBox
        Me.CompareVDAddr = New System.Windows.Forms.TextBox
        Me.CompareVDData = New System.Windows.Forms.TextBox
        Me.TextBox21 = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.RichTextBox = New System.Windows.Forms.RichTextBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripConnectStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxReaders
        '
        Me.ComboBoxReaders.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBoxReaders.FormattingEnabled = True
        Me.ComboBoxReaders.Location = New System.Drawing.Point(12, 12)
        Me.ComboBoxReaders.Name = "ComboBoxReaders"
        Me.ComboBoxReaders.Size = New System.Drawing.Size(509, 20)
        Me.ComboBoxReaders.TabIndex = 0
        '
        'ButtonConnect
        '
        Me.ButtonConnect.Location = New System.Drawing.Point(527, 11)
        Me.ButtonConnect.Name = "ButtonConnect"
        Me.ButtonConnect.Size = New System.Drawing.Size(144, 23)
        Me.ButtonConnect.TabIndex = 1
        Me.ButtonConnect.Text = "Connect"
        Me.ButtonConnect.UseVisualStyleBackColor = True
        '
        'ButtonInit
        '
        Me.ButtonInit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonInit.Location = New System.Drawing.Point(40, 20)
        Me.ButtonInit.Name = "ButtonInit"
        Me.ButtonInit.Size = New System.Drawing.Size(145, 23)
        Me.ButtonInit.TabIndex = 2
        Me.ButtonInit.Text = "Initialize"
        Me.ButtonInit.UseVisualStyleBackColor = True
        '
        'RichTextBoxATR
        '
        Me.RichTextBoxATR.Location = New System.Drawing.Point(121, 47)
        Me.RichTextBoxATR.Name = "RichTextBoxATR"
        Me.RichTextBoxATR.Size = New System.Drawing.Size(478, 24)
        Me.RichTextBoxATR.TabIndex = 3
        Me.RichTextBoxATR.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(40, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "ATR String:"
        '
        'TextBoxPin
        '
        Me.TextBoxPin.Location = New System.Drawing.Point(261, 85)
        Me.TextBoxPin.Name = "TextBoxPin"
        Me.TextBoxPin.Size = New System.Drawing.Size(100, 21)
        Me.TextBoxPin.TabIndex = 5
        Me.TextBoxPin.Text = "FF FF FF"
        Me.TextBoxPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonVerify
        '
        Me.ButtonVerify.Location = New System.Drawing.Point(384, 84)
        Me.ButtonVerify.Name = "ButtonVerify"
        Me.ButtonVerify.Size = New System.Drawing.Size(99, 23)
        Me.ButtonVerify.TabIndex = 6
        Me.ButtonVerify.Text = "Sle4442Verify"
        Me.ButtonVerify.UseVisualStyleBackColor = True
        '
        'ButtonChange
        '
        Me.ButtonChange.Location = New System.Drawing.Point(505, 83)
        Me.ButtonChange.Name = "ButtonChange"
        Me.ButtonChange.Size = New System.Drawing.Size(96, 23)
        Me.ButtonChange.TabIndex = 7
        Me.ButtonChange.Text = "Sle4442Change"
        Me.ButtonChange.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(172, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 21)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "PIN :  0x"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'ButtonReadMM
        '
        Me.ButtonReadMM.Location = New System.Drawing.Point(40, 143)
        Me.ButtonReadMM.Name = "ButtonReadMM"
        Me.ButtonReadMM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonReadMM.TabIndex = 9
        Me.ButtonReadMM.Text = "Read Main Memory"
        Me.ButtonReadMM.UseVisualStyleBackColor = True
        '
        'ButtonUpdateMM
        '
        Me.ButtonUpdateMM.Location = New System.Drawing.Point(39, 178)
        Me.ButtonUpdateMM.Name = "ButtonUpdateMM"
        Me.ButtonUpdateMM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonUpdateMM.TabIndex = 10
        Me.ButtonUpdateMM.Text = "Update Main Memory"
        Me.ButtonUpdateMM.UseVisualStyleBackColor = True
        '
        'ButtonReadPM
        '
        Me.ButtonReadPM.Location = New System.Drawing.Point(40, 216)
        Me.ButtonReadPM.Name = "ButtonReadPM"
        Me.ButtonReadPM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonReadPM.TabIndex = 11
        Me.ButtonReadPM.Text = "Read Protection Memory"
        Me.ButtonReadPM.UseVisualStyleBackColor = True
        '
        'ButtonWritePM
        '
        Me.ButtonWritePM.Location = New System.Drawing.Point(39, 255)
        Me.ButtonWritePM.Name = "ButtonWritePM"
        Me.ButtonWritePM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonWritePM.TabIndex = 12
        Me.ButtonWritePM.Text = "Write Protection Memory"
        Me.ButtonWritePM.UseVisualStyleBackColor = True
        '
        'ButtonReadSM
        '
        Me.ButtonReadSM.Location = New System.Drawing.Point(39, 294)
        Me.ButtonReadSM.Name = "ButtonReadSM"
        Me.ButtonReadSM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonReadSM.TabIndex = 13
        Me.ButtonReadSM.Text = "Read Security Memory"
        Me.ButtonReadSM.UseVisualStyleBackColor = True
        '
        'ButtonUpdateSM
        '
        Me.ButtonUpdateSM.Location = New System.Drawing.Point(38, 334)
        Me.ButtonUpdateSM.Name = "ButtonUpdateSM"
        Me.ButtonUpdateSM.Size = New System.Drawing.Size(168, 23)
        Me.ButtonUpdateSM.TabIndex = 14
        Me.ButtonUpdateSM.Text = "Update Security Memory"
        Me.ButtonUpdateSM.UseVisualStyleBackColor = True
        '
        'ButtonCompareVD
        '
        Me.ButtonCompareVD.Location = New System.Drawing.Point(39, 374)
        Me.ButtonCompareVD.Name = "ButtonCompareVD"
        Me.ButtonCompareVD.Size = New System.Drawing.Size(168, 23)
        Me.ButtonCompareVD.TabIndex = 15
        Me.ButtonCompareVD.Text = "Compare Verification Data"
        Me.ButtonCompareVD.UseVisualStyleBackColor = True
        '
        'ReadMMAddr
        '
        Me.ReadMMAddr.Location = New System.Drawing.Point(261, 143)
        Me.ReadMMAddr.Name = "ReadMMAddr"
        Me.ReadMMAddr.Size = New System.Drawing.Size(100, 21)
        Me.ReadMMAddr.TabIndex = 16
        Me.ReadMMAddr.Text = "00"
        Me.ReadMMAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(382, 143)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 21)
        Me.TextBox2.TabIndex = 17
        '
        'ReadMMReplyLen
        '
        Me.ReadMMReplyLen.Location = New System.Drawing.Point(503, 143)
        Me.ReadMMReplyLen.Name = "ReadMMReplyLen"
        Me.ReadMMReplyLen.Size = New System.Drawing.Size(100, 21)
        Me.ReadMMReplyLen.TabIndex = 18
        Me.ReadMMReplyLen.Text = "FF"
        Me.ReadMMReplyLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UpdateMMAddr
        '
        Me.UpdateMMAddr.Location = New System.Drawing.Point(261, 179)
        Me.UpdateMMAddr.Name = "UpdateMMAddr"
        Me.UpdateMMAddr.Size = New System.Drawing.Size(100, 21)
        Me.UpdateMMAddr.TabIndex = 19
        Me.UpdateMMAddr.Text = "00"
        Me.UpdateMMAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UpdateMMData
        '
        Me.UpdateMMData.Location = New System.Drawing.Point(382, 178)
        Me.UpdateMMData.Name = "UpdateMMData"
        Me.UpdateMMData.Size = New System.Drawing.Size(100, 21)
        Me.UpdateMMData.TabIndex = 20
        Me.UpdateMMData.Text = "00"
        Me.UpdateMMData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox6
        '
        Me.TextBox6.Enabled = False
        Me.TextBox6.Location = New System.Drawing.Point(503, 179)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 21)
        Me.TextBox6.TabIndex = 21
        '
        'TextBox7
        '
        Me.TextBox7.Enabled = False
        Me.TextBox7.Location = New System.Drawing.Point(260, 217)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(100, 21)
        Me.TextBox7.TabIndex = 22
        '
        'TextBox8
        '
        Me.TextBox8.Enabled = False
        Me.TextBox8.Location = New System.Drawing.Point(382, 217)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(100, 21)
        Me.TextBox8.TabIndex = 23
        '
        'ReadPMReplyLen
        '
        Me.ReadPMReplyLen.Enabled = False
        Me.ReadPMReplyLen.Location = New System.Drawing.Point(503, 217)
        Me.ReadPMReplyLen.Name = "ReadPMReplyLen"
        Me.ReadPMReplyLen.Size = New System.Drawing.Size(100, 21)
        Me.ReadPMReplyLen.TabIndex = 24
        Me.ReadPMReplyLen.Text = "04"
        Me.ReadPMReplyLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'WritePMAddr
        '
        Me.WritePMAddr.Location = New System.Drawing.Point(260, 255)
        Me.WritePMAddr.Name = "WritePMAddr"
        Me.WritePMAddr.Size = New System.Drawing.Size(100, 21)
        Me.WritePMAddr.TabIndex = 25
        Me.WritePMAddr.Text = "01"
        Me.WritePMAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'WritePMData
        '
        Me.WritePMData.Location = New System.Drawing.Point(382, 255)
        Me.WritePMData.Name = "WritePMData"
        Me.WritePMData.Size = New System.Drawing.Size(100, 21)
        Me.WritePMData.TabIndex = 26
        Me.WritePMData.Text = "FF"
        Me.WritePMData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox12
        '
        Me.TextBox12.Enabled = False
        Me.TextBox12.Location = New System.Drawing.Point(503, 253)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(100, 21)
        Me.TextBox12.TabIndex = 27
        '
        'TextBox13
        '
        Me.TextBox13.Enabled = False
        Me.TextBox13.Location = New System.Drawing.Point(260, 294)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(100, 21)
        Me.TextBox13.TabIndex = 28
        '
        'TextBox14
        '
        Me.TextBox14.Enabled = False
        Me.TextBox14.Location = New System.Drawing.Point(382, 294)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(100, 21)
        Me.TextBox14.TabIndex = 29
        '
        'TextBox15
        '
        Me.TextBox15.Enabled = False
        Me.TextBox15.Location = New System.Drawing.Point(503, 293)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New System.Drawing.Size(100, 21)
        Me.TextBox15.TabIndex = 30
        Me.TextBox15.Text = "04"
        Me.TextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UpdateSMAddr
        '
        Me.UpdateSMAddr.Location = New System.Drawing.Point(260, 333)
        Me.UpdateSMAddr.Name = "UpdateSMAddr"
        Me.UpdateSMAddr.Size = New System.Drawing.Size(100, 21)
        Me.UpdateSMAddr.TabIndex = 31
        Me.UpdateSMAddr.Text = "01"
        Me.UpdateSMAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UpdateSMData
        '
        Me.UpdateSMData.Location = New System.Drawing.Point(382, 332)
        Me.UpdateSMData.Name = "UpdateSMData"
        Me.UpdateSMData.Size = New System.Drawing.Size(100, 21)
        Me.UpdateSMData.TabIndex = 32
        Me.UpdateSMData.Text = "FF"
        Me.UpdateSMData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox18
        '
        Me.TextBox18.Enabled = False
        Me.TextBox18.Location = New System.Drawing.Point(503, 331)
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Size = New System.Drawing.Size(100, 21)
        Me.TextBox18.TabIndex = 33
        '
        'CompareVDAddr
        '
        Me.CompareVDAddr.Location = New System.Drawing.Point(260, 375)
        Me.CompareVDAddr.Name = "CompareVDAddr"
        Me.CompareVDAddr.Size = New System.Drawing.Size(100, 21)
        Me.CompareVDAddr.TabIndex = 34
        Me.CompareVDAddr.Text = "00"
        Me.CompareVDAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CompareVDData
        '
        Me.CompareVDData.Location = New System.Drawing.Point(383, 374)
        Me.CompareVDData.Name = "CompareVDData"
        Me.CompareVDData.Size = New System.Drawing.Size(100, 21)
        Me.CompareVDData.TabIndex = 35
        Me.CompareVDData.Text = "00"
        Me.CompareVDData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox21
        '
        Me.TextBox21.Enabled = False
        Me.TextBox21.Location = New System.Drawing.Point(504, 373)
        Me.TextBox21.Name = "TextBox21"
        Me.TextBox21.Size = New System.Drawing.Size(100, 21)
        Me.TextBox21.TabIndex = 36
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.RichTextBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.RichTextBoxATR)
        Me.GroupBox1.Controls.Add(Me.ButtonInit)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ButtonReadMM)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBox21)
        Me.GroupBox1.Controls.Add(Me.ButtonVerify)
        Me.GroupBox1.Controls.Add(Me.ButtonChange)
        Me.GroupBox1.Controls.Add(Me.ButtonUpdateMM)
        Me.GroupBox1.Controls.Add(Me.CompareVDData)
        Me.GroupBox1.Controls.Add(Me.TextBoxPin)
        Me.GroupBox1.Controls.Add(Me.ButtonReadPM)
        Me.GroupBox1.Controls.Add(Me.CompareVDAddr)
        Me.GroupBox1.Controls.Add(Me.ButtonWritePM)
        Me.GroupBox1.Controls.Add(Me.TextBox18)
        Me.GroupBox1.Controls.Add(Me.ButtonReadSM)
        Me.GroupBox1.Controls.Add(Me.UpdateSMData)
        Me.GroupBox1.Controls.Add(Me.ButtonUpdateSM)
        Me.GroupBox1.Controls.Add(Me.UpdateSMAddr)
        Me.GroupBox1.Controls.Add(Me.ButtonCompareVD)
        Me.GroupBox1.Controls.Add(Me.TextBox15)
        Me.GroupBox1.Controls.Add(Me.ReadMMAddr)
        Me.GroupBox1.Controls.Add(Me.TextBox14)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox13)
        Me.GroupBox1.Controls.Add(Me.ReadMMReplyLen)
        Me.GroupBox1.Controls.Add(Me.TextBox12)
        Me.GroupBox1.Controls.Add(Me.UpdateMMAddr)
        Me.GroupBox1.Controls.Add(Me.WritePMData)
        Me.GroupBox1.Controls.Add(Me.UpdateMMData)
        Me.GroupBox1.Controls.Add(Me.WritePMAddr)
        Me.GroupBox1.Controls.Add(Me.TextBox6)
        Me.GroupBox1.Controls.Add(Me.ReadPMReplyLen)
        Me.GroupBox1.Controls.Add(Me.TextBox7)
        Me.GroupBox1.Controls.Add(Me.TextBox8)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(659, 509)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "4442 Card"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(81, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 26)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Command"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(261, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 26)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Addr"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(382, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 26)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Data"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(507, 113)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 26)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "ReplyLen"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RichTextBox
        '
        Me.RichTextBox.Location = New System.Drawing.Point(38, 412)
        Me.RichTextBox.Name = "RichTextBox"
        Me.RichTextBox.Size = New System.Drawing.Size(569, 92)
        Me.RichTextBox.TabIndex = 41
        Me.RichTextBox.Text = ""
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripConnectStatus, Me.ToolStripStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(229, 582)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(193, 22)
        Me.StatusStrip1.TabIndex = 38
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripConnectStatus
        '
        Me.ToolStripConnectStatus.Name = "ToolStripConnectStatus"
        Me.ToolStripConnectStatus.Size = New System.Drawing.Size(105, 17)
        Me.ToolStripConnectStatus.Text = "Connect Status : "
        '
        'ToolStripStatus
        '
        Me.ToolStripStatus.ForeColor = System.Drawing.Color.Red
        Me.ToolStripStatus.Name = "ToolStripStatus"
        Me.ToolStripStatus.Size = New System.Drawing.Size(71, 17)
        Me.ToolStripStatus.Text = "Disconnect"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(264, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 12)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "0x"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(384, 123)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "0x"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(502, 123)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 12)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "0x"
        '
        'Form301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 613)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonConnect)
        Me.Controls.Add(Me.ComboBoxReaders)
        Me.MaximizeBox = False
        Me.Name = "Form301"
        Me.Text = "301 Demo"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxReaders As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonConnect As System.Windows.Forms.Button
    Friend WithEvents ButtonInit As System.Windows.Forms.Button
    Friend WithEvents RichTextBoxATR As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPin As System.Windows.Forms.TextBox
    Friend WithEvents ButtonVerify As System.Windows.Forms.Button
    Friend WithEvents ButtonChange As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonReadMM As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdateMM As System.Windows.Forms.Button
    Friend WithEvents ButtonReadPM As System.Windows.Forms.Button
    Friend WithEvents ButtonWritePM As System.Windows.Forms.Button
    Friend WithEvents ButtonReadSM As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdateSM As System.Windows.Forms.Button
    Friend WithEvents ButtonCompareVD As System.Windows.Forms.Button
    Friend WithEvents ReadMMAddr As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents ReadMMReplyLen As System.Windows.Forms.TextBox
    Friend WithEvents UpdateMMAddr As System.Windows.Forms.TextBox
    Friend WithEvents UpdateMMData As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents ReadPMReplyLen As System.Windows.Forms.TextBox
    Friend WithEvents WritePMAddr As System.Windows.Forms.TextBox
    Friend WithEvents WritePMData As System.Windows.Forms.TextBox
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox15 As System.Windows.Forms.TextBox
    Friend WithEvents UpdateSMAddr As System.Windows.Forms.TextBox
    Friend WithEvents UpdateSMData As System.Windows.Forms.TextBox
    Friend WithEvents TextBox18 As System.Windows.Forms.TextBox
    Friend WithEvents CompareVDAddr As System.Windows.Forms.TextBox
    Friend WithEvents CompareVDData As System.Windows.Forms.TextBox
    Friend WithEvents TextBox21 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripConnectStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
