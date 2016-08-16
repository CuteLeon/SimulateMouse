Imports System.ComponentModel

'Win+菜单键： 开启或关闭模拟功能
'Win+W： 显示或隐藏窗体

'Ctrl+方向键： 大幅移动（250像素）
'方向键： 中幅移动（50像素）
'Shift+方向键： 小幅移动（10像素）
'Ctrl+Shift + 方向键： 微小移动（1像素）
'减号键(-)： 鼠标左键
'加号键(+)： 鼠标右键
'Shift+PageUp / PageDown： 滚轮上下移动

Public Class InfoForm
    '鼠标拖动窗体
    Private Declare Function ReleaseCapture Lib "user32" () As Integer
    Private Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer
    '防止激活抢占焦点
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    Private Const GWL_EXSTYLE = (-20)
    Private Const WS_DISABLED = &H8000000
    '键盘钩子
    Private Declare Function SetWindowsHookExA Lib "user32" (ByVal idHook As Int32, ByVal lpfn As MyDelegate, ByVal hmod As Int32, ByVal dwThreadId As Int32) As Int32
    Private Declare Function CallNextHookEx Lib "user32" (ByVal hHook As Int32, ByVal ncode As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
    Private Declare Function UnhookWindowsHookEx Lib "user32" (ByVal hHook As Int32) As Int32
    Private Declare Sub RtlMoveMemory Lib "kernel32" (ByRef Destination As KeyBoardEventMsg, ByVal Source As Int32, ByVal Length As Int32)
    Private Delegate Function MyDelegate(ByVal ncode As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
    Private Declare Function GetModuleHandleA Lib "kernel32" (ByVal lpModuleName As String) As Int32
    Private AddKeyboardHook As New MyDelegate(AddressOf MyKeyBoardHook) 'MyKeyBoardHook委托变量
    Private Structure KeyBoardEventMsg
        Dim VKey As Integer
        Dim SKey As Integer
        Dim Flag As Integer
        Dim Time As Integer
    End Structure
    Private Const HC_ACTION = 0
    Private Const WH_KEYBOARD_LL = 13           '低级键盘钩子
    Private Const WM_KEYUP = &H101               '按键抬起
    Private Const WM_KEYDOWN = &H100        '按键按下
    Private Const WM_SYSKEYUP = &H105         '系统按键抬起（Alt键）
    Private Const WM_SYSKEYDOWN = &H104  '系统按键按下（Alt键）
    Private Ctrl As Boolean, LCtrl As Boolean, RCtrl As Boolean
    Private Shift As Boolean, LShift As Boolean, RShift As Boolean
    Private Win As Boolean, LWin As Boolean, RWin As Boolean

    '模拟鼠标
    Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As VBPoint) As Integer
    Private Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Integer
    Private Const MOUSEEVENTF_LEFTDOWN = &H2
    Private Const MOUSEEVENTF_LEFTUP = &H4
    Private Const MOUSEEVENTF_RIGHTDOWN = &H8
    Private Const MOUSEEVENTF_RIGHTUP = &H10
    Private Const MOUSEEVENTF_WHEEL = &H800
    Private Const WHEEL_DELTA = 120
    Private Const MOUSEEVENTF_ABSOLUTE = &H8000
    Private Structure VBPoint
        Dim X As Integer
        Dim Y As Integer
    End Structure
    Private MousePoint As VBPoint
    Private Const LargeMove = 250 '大幅移动
    Private Const ModerateMove = 50 '中幅移动
    Private Const SmallMove = 10 '小幅移动
    Private Const MicroMove = 1 '微小移动
    Private MouseState As Boolean = False '模拟鼠标功能开关

    Private Sub SettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SwitchKeyBoardHook(True) '启动时注册键盘钩子，开始监听键盘热键
        '显示系统图标，并弹出气泡提示
        MouseNotify.Icon = Me.Icon
        MouseNotify.Visible = True
        MouseNotify.ShowBalloonTip(2000)
    End Sub

    Private Sub MouseNotify_Click(sender As Object, e As EventArgs) Handles MouseNotify.Click
        Me.Visible = Not Me.Visible '点击托盘图标显示或隐藏信息窗口
    End Sub

    Private Sub SettingForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '窗体关闭时
        SwitchKeyBoardHook(False) '卸载键盘钩子
        MouseNotify.Visible = False '隐藏托盘图标
        MouseNotify.Dispose() '销毁托盘图标
    End Sub

    Private Function MyKeyBoardHook(ByVal nCode As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
        Dim MyKeyBoardMsg As KeyBoardEventMsg
        Try '捕捉异常
            If nCode = HC_ACTION Then
                RtlMoveMemory(MyKeyBoardMsg, lParam, Len(MyKeyBoardMsg)) '内存级赋值

                If wParam = WM_SYSKEYDOWN Or wParam = WM_KEYDOWN Then '键盘按下
                    Select Case MyKeyBoardMsg.VKey '判断vKey
                        Case Keys.LWin : LWin = True
                        Case Keys.RWin : RWin = True
                        Case Keys.LControlKey : LCtrl = True
                        Case Keys.RControlKey : RCtrl = True
                        Case Keys.LShiftKey : LShift = True
                        Case Keys.RShiftKey : RShift = True
                        Case Else '其他键（控制键需要受MouseState开关状态控制）
                            '开关为关闭状态，正常返回消息
                            If (Not MouseState) Then Return CallNextHookEx(0, nCode, wParam, lParam)

                            GetCursorPos(MousePoint) '获取鼠标位置
                            Select Case MyKeyBoardMsg.VKey
                                Case Keys.Up '鼠标上移
                                    If (Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y - LargeMove) '大幅移动
                                    ElseIf (Not Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y - ModerateMove) '中幅移动
                                    ElseIf (Not Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y - SmallMove) '小幅移动
                                    ElseIf (Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y - MicroMove) '微小移动
                                    End If
                                Case Keys.Down '鼠标下移
                                    If (Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y + LargeMove) '大幅移动
                                    ElseIf (Not Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y + ModerateMove) '中幅移动
                                    ElseIf (Not Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y + SmallMove) '小幅移动
                                    ElseIf (Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X, MousePoint.Y + MicroMove) '微小移动
                                    End If
                                Case Keys.Left '鼠标左移
                                    If (Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X - LargeMove, MousePoint.Y) '大幅移动
                                    ElseIf (Not Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X - ModerateMove, MousePoint.Y) '中幅移动
                                    ElseIf (Not Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X - SmallMove, MousePoint.Y) '小幅移动
                                    ElseIf (Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X - MicroMove, MousePoint.Y) '微小移动
                                    End If
                                Case Keys.Right '右移鼠标
                                    If (Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X + LargeMove, MousePoint.Y) '大幅移动
                                    ElseIf (Not Ctrl And Not Shift) Then
                                        SetCursorPos(MousePoint.X + ModerateMove, MousePoint.Y) '中幅移动
                                    ElseIf (Not Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X + SmallMove, MousePoint.Y) '小幅移动
                                    ElseIf (Ctrl And Shift) Then
                                        SetCursorPos(MousePoint.X + MicroMove, MousePoint.Y) '微小移动
                                    End If
                                Case Keys.OemMinus '按下减号键，模拟鼠标右键按下
                                    mouse_event(MOUSEEVENTF_ABSOLUTE Or MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                                Case Keys.Oemplus  '按下加号键，模拟鼠标右键按下
                                    mouse_event(MOUSEEVENTF_ABSOLUTE Or MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                                Case Keys.PageUp 'Shift组合键，滚轮向上
                                    If (Shift) Then
                                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 120, 0)
                                    Else '没有Shift修饰键，正常返回消息
                                        Return CallNextHookEx(0, nCode, wParam, lParam)
                                    End If
                                Case Keys.PageDown 'Shift组合键，滚轮向下
                                    If (Shift) Then
                                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -120, 0)
                                    Else '没有Shift修饰键，正常返回消息
                                        Return CallNextHookEx(0, nCode, wParam, lParam)
                                    End If
                                Case Else '其他键不处理，正常返回消息
                                    Return CallNextHookEx(0, nCode, wParam, lParam)
                            End Select
                            Return 1 '以上键被捕获，消息全部吞掉
                    End Select
                    If (LCtrl Or RCtrl) Then Ctrl = True
                    If (LShift Or RShift) Then Shift = True
                    If (LWin Or RWin) Then Win = True
                    '按下单一的修饰键不处理，消息全部正常返回
                    Return CallNextHookEx(0, nCode, wParam, lParam)
                ElseIf wParam = WM_SYSKEYUP Or wParam = WM_KEYUP Then '键盘抬起
                    Select Case MyKeyBoardMsg.VKey
                        Case Keys.LWin : LWin = False
                        Case Keys.RWin : RWin = False
                        Case Keys.LControlKey : LCtrl = False
                        Case Keys.RControlKey : RCtrl = False
                        Case Keys.LShiftKey : LShift = False
                        Case Keys.RShiftKey : RShift = False
                        Case Keys.OemMinus '抬起减号键，模拟鼠标左键抬起
                            mouse_event(MOUSEEVENTF_ABSOLUTE Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
                            Return 1
                        Case Keys.Oemplus  '抬起加号键，模拟鼠标右键抬起
                            mouse_event(MOUSEEVENTF_ABSOLUTE Or MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                            Return 1
                        Case Keys.Apps '菜单键切换模拟鼠标的开关
                            '如果Win修饰键未按下，则不处理，并正常返回
                            If (Not Win) Then Return CallNextHookEx(0, nCode, wParam, lParam)
                            MouseState = Not MouseState '切换开关
                            MouseNotify.BalloonTipTitle = "小眼模拟鼠标状态提醒"
                            MouseNotify.BalloonTipText = "小眼模拟鼠标已" & IIf(MouseState, "开启！你可以按压方向键操作鼠标。", "关闭！")
                            MouseNotify.Text = "小眼模拟鼠标[状态：" & IIf(MouseState, "开启", "关闭") & "]"
                            MouseNotify.ShowBalloonTip(1500)
                            Return 1
                        Case Keys.W 'W键显示或隐藏设置窗体
                            '如果Win修饰键未按下，则不处理，并正常返回
                            If (Not Win) Then Return CallNextHookEx(0, nCode, wParam, lParam)
                            Me.Visible = Not Me.Visible '切换窗体可见性
                            Return 1
                    End Select
                    If Not (LCtrl Or RCtrl) Then Ctrl = False
                    If Not (LShift Or RShift) Then Shift = False
                    If Not (LWin Or RWin) Then Win = False
                    '按下单一的修饰键或者是其他键均不处理，消息全部正常返回
                    Return CallNextHookEx(0, nCode, wParam, lParam)
                Else '除了按下和抬起以外的其他消息，不处理，正常返回
                    Return CallNextHookEx(0, nCode, wParam, lParam)
                End If
            Else 'nCode除了HC_ACTION以外的其他值，不处理，正常返回
                Return CallNextHookEx(0, nCode, wParam, lParam)
            End If
        Catch ex As Exception
            '发生异常，吞掉消息。神不知，鬼不觉，不会有人知道这件事的[/坏笑]
            Return 1
        End Try
    End Function

    Private Sub LnkLbl_QQZone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkLbl_QQZone.LinkClicked
        System.Diagnostics.Process.Start("http://user.qzone.qq.com/2543280836")
    End Sub

    Private Sub SwitchKeyBoardHook(ByVal SwitchOn As Boolean) '键盘钩子的开关
        Static KeyBoardHookID As Int32 '钩子的句柄，静态变量，生命周期变长，防止外部操作

        If SwitchOn Then '开启键盘钩子
            If KeyBoardHookID <> 0 Then Exit Sub
            KeyBoardHookID = SetWindowsHookExA(WH_KEYBOARD_LL, AddKeyboardHook, GetModuleHandleA(Process.GetCurrentProcess().MainModule.ModuleName), 0)
        Else '关闭键盘钩子
            UnhookWindowsHookEx(KeyBoardHookID)
            KeyBoardHookID = 0
        End If
    End Sub

    '防止双击窗口标题栏后最大化或其他情况下最小化
    Private Sub SettingForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.WindowState = FormWindowState.Normal
    End Sub

    '防止激活抢占焦点
    Private Sub InfoForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        SetWindowLong(Me.Handle, GWL_EXSTYLE, GetWindowLong(Me.Handle, GWL_EXSTYLE) Or WS_DISABLED)
    End Sub

    '鼠标拖动窗体
    Private Sub MoveWindow(sender As Object, e As MouseEventArgs) Handles Lbl_Infos.MouseDown, Lbl_Keys.MouseDown, Me.MouseDown
        ReleaseCapture()
        SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub
End Class