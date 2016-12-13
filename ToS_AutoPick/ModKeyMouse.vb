Imports System.Threading

Module modKeyMouse

    '抓Windows按鍵用API
    Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short
    '發送訊息到執行緒佇列
    Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    '定義常數
    Public Const WM_KEYDOWN As Integer = &H100
    Public Const WM_KEYUP As Integer = &H101
    Public Const VK_C As Integer = &H43
    Public Const WM_CLOSE As Integer = &H10

    Public Const WM_LBUTTONDBLCLK As Integer = &H203
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_LBUTTONUP As Integer = &H202
    Public Const WM_MBUTTONDBLCLK As Integer = &H209
    Public Const WM_MBUTTONDOWN As Integer = &H207
    Public Const WM_MBUTTONUP As Integer = &H208
    Public Const WM_RBUTTONDBLCLK As Integer = &H206
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_RBUTTONUP As Integer = &H205
    Public Const WM_MOUSEACTIVATE As Integer = &H21
    Public Const WM_MOUSEWHEEL As Integer = &H20A

    Public Const WM_MOUSEFIRST As Integer = &H200
    Public Const WM_MOUSELAST As Integer = &H209
    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_SETCURSOR As Integer = &H20

    '設定滑鼠座標API
    Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer
    '取得滑鼠座標API
    Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As PointAPI) As Integer
    Structure PointAPI '滑鼠座標資料結構
        Dim CurX As Integer
        Dim CurY As Integer
    End Structure

    Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer

    '產生掃瞄碼
    Function MakeKeyLparam(ByVal VirtualKey As Integer, ByVal flag As Integer) As Integer
        Dim s As String
        Dim Firstbyte As String 'lparam參數的24-31位
        If flag = WM_KEYDOWN Then '如果是按下鍵
            Firstbyte = "00"
        Else
            Firstbyte = "C0" '如果是釋放鍵
        End If
        Dim Scancode As Integer
        '獲得鍵的掃描碼
        Scancode = MapVirtualKey(VirtualKey, 0)
        Dim Secondbyte As String 'lparam參數的16-23位元，即虛擬鍵掃描碼
        Secondbyte = Right("00" & Hex(Scancode), 2)
        s = Firstbyte & Secondbyte & "0001" '0001為lparam參數的0-15位，即發送次數和其他擴展資訊
        MakeKeyLparam = Val("&H" & s)
    End Function

    Public Sub BgdMouseLClick(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_LBUTTONDOWN, 1, lparam)
        PostMessage(hwnd, WM_LBUTTONUP, 0, lparam)
    End Sub

    Public Sub BgdMouseLDown(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_LBUTTONDOWN, 1, lparam)
    End Sub

    Public Sub BgdMouseLUp(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_LBUTTONUP, 0, lparam)
    End Sub

    Public Sub BgdMouseRClick(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_RBUTTONDOWN, 2, lparam)
        PostMessage(hwnd, WM_RBUTTONUP, 0, lparam)
    End Sub

    Public Sub BgdMouseRDown(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_RBUTTONDOWN, 2, lparam)
    End Sub

    Public Sub BgdMouseRUp(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_RBUTTONUP, 0, lparam)
    End Sub

    Public Sub BgdMouseMove(hwnd As Integer, lparam As Long)
        PostMessage(hwnd, WM_MOUSEMOVE, 0, lparam)
    End Sub

    Public Sub BgdMyKeyPress(hwnd As Integer, vKeyCode As Integer)
        PostMessage(hwnd, WM_KEYDOWN, vKeyCode, MakeKeyLparam(vKeyCode, WM_KEYDOWN))
        Thread.Sleep(20)
        PostMessage(hwnd, WM_KEYUP, vKeyCode, MakeKeyLparam(vKeyCode, WM_KEYUP))
    End Sub

    Public Sub BgdMyKeyDown(hwnd As Integer, vKeyCode As Integer)
        PostMessage(hwnd, WM_KEYDOWN, vKeyCode, MakeKeyLparam(vKeyCode, WM_KEYDOWN))
    End Sub

    Public Sub BgdMyKeyUp(hwnd As Integer, vKeyCode As Integer)
        PostMessage(hwnd, WM_KEYUP, vKeyCode, MakeKeyLparam(vKeyCode, WM_KEYUP))
    End Sub

End Module