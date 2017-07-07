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
        Me.ComboBoxReaders = New System.Windows.Forms.ComboBox()
        Me.ButtonConnect = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripConnectStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ButtonSend = New System.Windows.Forms.Button()
        Me.RichTextBoxAPDU = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RichTextBoxLog = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxReaders
        '
        Me.ComboBoxReaders.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBoxReaders.FormattingEnabled = True
        Me.ComboBoxReaders.Location = New System.Drawing.Point(55, 13)
        Me.ComboBoxReaders.Name = "ComboBoxReaders"
        Me.ComboBoxReaders.Size = New System.Drawing.Size(466, 21)
        Me.ComboBoxReaders.TabIndex = 0
        '
        'ButtonConnect
        '
        Me.ButtonConnect.Location = New System.Drawing.Point(527, 12)
        Me.ButtonConnect.Name = "ButtonConnect"
        Me.ButtonConnect.Size = New System.Drawing.Size(144, 25)
        Me.ButtonConnect.TabIndex = 1
        Me.ButtonConnect.Text = "Connect"
        Me.ButtonConnect.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripConnectStatus, Me.ToolStripStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(229, 631)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(179, 22)
        Me.StatusStrip1.TabIndex = 38
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripConnectStatus
        '
        Me.ToolStripConnectStatus.Name = "ToolStripConnectStatus"
        Me.ToolStripConnectStatus.Size = New System.Drawing.Size(96, 17)
        Me.ToolStripConnectStatus.Text = "Connect Status : "
        '
        'ToolStripStatus
        '
        Me.ToolStripStatus.ForeColor = System.Drawing.Color.Red
        Me.ToolStripStatus.Name = "ToolStripStatus"
        Me.ToolStripStatus.Size = New System.Drawing.Size(66, 17)
        Me.ToolStripStatus.Text = "Disconnect"
        '
        'ButtonSend
        '
        Me.ButtonSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonSend.Location = New System.Drawing.Point(508, 19)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.Size = New System.Drawing.Size(145, 25)
        Me.ButtonSend.TabIndex = 2
        Me.ButtonSend.Text = "Send APDU"
        Me.ButtonSend.UseVisualStyleBackColor = True
        '
        'RichTextBoxAPDU
        '
        Me.RichTextBoxAPDU.Location = New System.Drawing.Point(43, 20)
        Me.RichTextBoxAPDU.Name = "RichTextBoxAPDU"
        Me.RichTextBoxAPDU.Size = New System.Drawing.Size(459, 26)
        Me.RichTextBoxAPDU.TabIndex = 3
        Me.RichTextBoxAPDU.Text = "0084000008"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(40, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Display"
        '
        'RichTextBoxLog
        '
        Me.RichTextBoxLog.Location = New System.Drawing.Point(43, 80)
        Me.RichTextBoxLog.Name = "RichTextBoxLog"
        Me.RichTextBoxLog.Size = New System.Drawing.Size(569, 441)
        Me.RichTextBoxLog.TabIndex = 41
        Me.RichTextBoxLog.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GroupBox1.Controls.Add(Me.RichTextBoxLog)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.RichTextBoxAPDU)
        Me.GroupBox1.Controls.Add(Me.ButtonSend)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(659, 551)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Operation Window"
        '
        'Form301
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 664)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonConnect)
        Me.Controls.Add(Me.ComboBoxReaders)
        Me.MaximizeBox = False
        Me.Name = "Form301"
        Me.Text = "301 Demo"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxReaders As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonConnect As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripConnectStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ButtonSend As System.Windows.Forms.Button
    Friend WithEvents RichTextBoxAPDU As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RichTextBoxLog As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
