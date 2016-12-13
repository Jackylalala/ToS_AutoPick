Module modWindowInfo
    Public x0, y0, x1, y1 As Integer '視窗四角座標
    Public Client_x0, Client_y0 As Integer '視窗左上座標(不含標題邊框)
    Public ClientX, ClientY As Integer '視窗長寬(不含標題邊框)
    Public Border As Integer '視窗邊框寬度
    Public Title As Integer '視窗標題高度

    '設定視窗用API
    Public Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    '//聲明:
    'hWnd: HWND            窗口句柄}
    'hWndInsertAfter: HWND 窗口的 Z 順序}
    'X, Y: Integer         位置}
    'cx, cy: Integer       大小}
    'uFlags: UINT           選項}
    '//hWndInsertAfter 參數可選值:
    Public Const HWND_TOP As Integer = 0        '在前面
    Public Const HWND_BOTTOM As Integer = 1     '在後面
    Public Const HWND_TOPMOST As Integer = -1   '在前面, 位於任何頂部窗口的前面
    Public Const HWND_NOTOPMOST As Integer = -2 '在前面, 位於其他頂部窗口的後面

    '//uFlags 參數可選值:
    Public Const SWP_NOSIZE As Integer = 1                       '忽略 cx、cy, 保持大小
    Public Const SWP_NOMOVE As Integer = 2                       '忽略 X、Y, 不改變位置
    Public Const SWP_NOZORDER As Integer = 4                     '忽略 hWndInsertAfter, 保持 Z 順序
    Public Const SWP_NOREDRAW As Integer = 8                     '不重繪
    Public Const SWP_NOACTIVATE As Integer = &H10                '不激活
    Public Const SWP_FRAMECHANGED As Integer = &H20              '強制發送 WM_NCCALCSIZE 消息, 一般只是在改變大小時才發送此消息
    Public Const SWP_SHOWWINDOW As Integer = &H40                '顯示窗口
    Public Const SWP_HIDEWINDOW As Integer = &H80                '隱藏窗口
    Public Const SWP_NOCOPYBITS As Integer = &H100               '丟棄客戶區
    Public Const SWP_NOOWNERZORDER As Integer = &H200            '忽略 hWndInsertAfter, 不改變 Z 序列的所有者
    Public Const SWP_NOSENDCHANGING As Integer = &H400           '不發出 WM_WINDOWPOSCHANGING 消息
    Public Const SWP_DRAWFRAME As Integer = SWP_FRAMECHANGED     '畫邊框
    Public Const SWP_NOREPOSITION As Integer = SWP_NOOWNERZORDER '
    Public Const SWP_DEFERERASE As Integer = &H2000              '防止產生 WM_SYNCPAINT 消息
    Public Const SWP_ASYNCWINDOWPOS As Integer = &H4000          '若調用進程不擁有窗口, 系統會向擁有窗口的線程發出需求

    '使視窗獲得焦點
    Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Integer) As Integer
    '取得視窗資訊用API
    Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    '設定視窗用API
     Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    '定義視窗設定用常數
    'nIndex = -20 (GWL_EXSTYLE)：設定視窗額外的樣式
    Public Const WS_EX_ACCEPTFILES As Integer = &H10& '設定能接收檔案總管拖曳過來檔案的樣式
    Public Const WS_EX_TOPMOST As Integer = &H8 '設定最上層顯示(Top Most)
    Public Const WS_EX_TRANSPARENT As Integer = &H20& '設定視窗不會自動重繪，直到被視窗覆蓋下的視窗本身做重繪時才會跟著重繪
    Public Const WS_EX_NOACTIVATE As Integer = &H8000000 '設定縮小後，不會收到Taskbar裡
    Public Const WS_EX_APPWINDOW As Integer = &H40000 '設定縮小後，會收到Taskbar裡 
    'nIndex = -16 (GWL_STYLE)：設定視窗的樣式
    Public Const WS_BORDER As Integer = &H800000 '擁有邊框(Thin Border)
    Public Const WS_CAPTION As Integer = &HC00000 '擁有標題列(Title)，可讓視窗移動
    Public Const WS_CHILDWINDOW As Integer = &H40000000 '設定為子視窗(ChildWindow)
    Public Const WS_DISABLED As Integer = &H8000000 '設定為Disabled
    Public Const WS_THICKFRAME As Integer = &H40000 '設定為具有改變視窗大小的邊框
    Public Const WS_SYSMENU As Integer = &H80000 '設定有系統功能表，要與WS_CAPTION一起使用
    Public Const WS_MINIMIZEBOX As Integer = &H20000 '設定有最小化按鈕，要與WS_SYSMENU一起使用
    Public Const WS_MAXIMIZEBOX As Integer = &H10000 '設定有最大化按鈕，要與WS_SYSMENU一起使用
    Public Const WS_DLGFRAME As Integer = &H400000 '設定有邊框(Border)，但不可以搭配WS_CAPTION一起使用
    Public Const WS_HSCROLL As Integer = &H100000 '設定有水平捲軸
    Public Const WS_VSCROLL As Integer = &H200000 '設定有垂直捲軸 

    '找出視窗hwnd用API
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    'hWnd1若有帶值，表是從某個Window內找裡面的Window，若帶0，則找所有的Window；hWnd2：表示從那一個Window內的Window開始找起，若帶0，則由第一個開始找；lpsz1為Class名稱；lpsz2為WindowText（或是Form的Title）

    '取得視窗大小(含邊框，功能表)
    Declare Function GetWindowRect Lib "user32" (ByVal hWnd As Integer, ByRef rectangle As RECT) As Integer
    '取得視窗大小(不含邊框，功能表，左上角為0,0)
    Public Declare Function GetClientRect Lib "user32 " (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer
    '取得目前視窗hwnd
    Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As Integer
    '取得目前視窗某點顏色相關API
    Declare Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As Integer) As Integer
    Declare Function GetPixel Lib "gdi32" Alias "GetPixel" (ByVal hdc As Integer, ByVal x As Integer, ByVal y As Integer) As Integer
    Declare Function ReleaseDC Lib "user32" Alias "ReleaseDC" (ByVal hwnd As Integer, ByVal hdc As Integer) As Long


    Structure RECT
        Dim x1 As Integer
        Dim y1 As Integer
        Dim x2 As Integer
        Dim y2 As Integer
    End Structure

    '取得視窗資訊
    Sub GetWindowInfo()
        Dim R As RECT
        Dim RetVal As Integer
        RetVal = GetWindowRect(GetForegroundWindow(), R)
        x0 = R.x1
        x1 = R.x2
        y0 = R.y1
        y1 = R.y2
        RetVal = GetClientRect(GetForegroundWindow(), R)
        ClientX = R.x2
        ClientY = R.y2
        Border = ((x1 - x0) - ClientX) / 2
        Title = (y1 - y0) - ClientY - Border
        Client_x0 = x0 + Border
        Client_y0 = y0 + Title
    End Sub

    Sub GetWindowInfo(hwnd As Integer)
        Dim R As RECT
        Dim RetVal As Integer
        RetVal = GetWindowRect(hwnd, R)
        x0 = R.x1
        x1 = R.x2
        y0 = R.y1
        y1 = R.y2
        RetVal = GetClientRect(hwnd, R)
        ClientX = R.x2
        ClientY = R.y2
        Border = ((x1 - x0) - ClientX) / 2
        Title = (y1 - y0) - ClientY - Border
        Client_x0 = x0 + Border
        Client_y0 = y0 + Title
    End Sub
End Module
