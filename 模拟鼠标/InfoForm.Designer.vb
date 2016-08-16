<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InfoForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InfoForm))
        Me.MouseNotify = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.LnkLbl_QQZone = New System.Windows.Forms.LinkLabel()
        Me.Lbl_Keys = New System.Windows.Forms.Label()
        Me.Lbl_Infos = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'MouseNotify
        '
        Me.MouseNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.MouseNotify.BalloonTipText = "欢迎使用小眼模拟鼠标。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "按下Win键+菜单键开启模拟功能。"
        Me.MouseNotify.BalloonTipTitle = "小眼模拟鼠标准备就绪："
        Me.MouseNotify.Text = "小眼模拟鼠标[状态：关闭]"
        Me.MouseNotify.Visible = True
        '
        'LnkLbl_QQZone
        '
        Me.LnkLbl_QQZone.AutoSize = True
        Me.LnkLbl_QQZone.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LnkLbl_QQZone.LinkArea = New System.Windows.Forms.LinkArea(0, 10)
        Me.LnkLbl_QQZone.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LnkLbl_QQZone.Location = New System.Drawing.Point(77, 1)
        Me.LnkLbl_QQZone.Name = "LnkLbl_QQZone"
        Me.LnkLbl_QQZone.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.LnkLbl_QQZone.Size = New System.Drawing.Size(125, 15)
        Me.LnkLbl_QQZone.TabIndex = 1
        Me.LnkLbl_QQZone.TabStop = True
        Me.LnkLbl_QQZone.Text = "欢迎使用小眼模拟鼠标"
        Me.LnkLbl_QQZone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lbl_Keys
        '
        Me.Lbl_Keys.AutoSize = True
        Me.Lbl_Keys.CausesValidation = False
        Me.Lbl_Keys.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Lbl_Keys.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Lbl_Keys.Location = New System.Drawing.Point(10, 24)
        Me.Lbl_Keys.Name = "Lbl_Keys"
        Me.Lbl_Keys.Size = New System.Drawing.Size(143, 108)
        Me.Lbl_Keys.TabIndex = 2
        Me.Lbl_Keys.Text = "Win+菜单键：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Win+W：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+方向键：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "方向键：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift+方向键：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ctrl+Shift+方向键：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "减号键(-)：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "加号键(+)：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift+PageUp/PageDown："
        Me.Lbl_Keys.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lbl_Infos
        '
        Me.Lbl_Infos.AutoSize = True
        Me.Lbl_Infos.CausesValidation = False
        Me.Lbl_Infos.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Lbl_Infos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Lbl_Infos.Location = New System.Drawing.Point(145, 24)
        Me.Lbl_Infos.Name = "Lbl_Infos"
        Me.Lbl_Infos.Size = New System.Drawing.Size(113, 108)
        Me.Lbl_Infos.TabIndex = 3
        Me.Lbl_Infos.Text = "开启或关闭模拟功能" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "显示或隐藏窗体" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "大幅移动" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "中幅移动" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "小幅移动" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "微小移动" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "鼠标左键" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "鼠标右键" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "滚轮上下移动"
        Me.Lbl_Infos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'InfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(268, 139)
        Me.Controls.Add(Me.Lbl_Infos)
        Me.Controls.Add(Me.Lbl_Keys)
        Me.Controls.Add(Me.LnkLbl_QQZone)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "InfoForm"
        Me.Opacity = 0.8R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "小眼模拟鼠标：提示面板"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MouseNotify As NotifyIcon
    Friend WithEvents LnkLbl_QQZone As LinkLabel
    Friend WithEvents Lbl_Keys As Label
    Friend WithEvents Lbl_Infos As Label
End Class
