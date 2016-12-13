Imports System.Threading
Imports System.Drawing.Imaging

Friend Class frmToS_AutoPick
    Inherits System.Windows.Forms.Form
    Dim Work As Thread
    Dim hwnd As Integer '外部hwnd
    Dim hwndInterface As Integer '面板hwnd
    Dim speed As Single '用以調整速度
    Dim hDC As Integer '抓色用
    Dim backup As Boolean '是否保存該紀錄
    Dim num As Integer '紀錄編號
    Dim colorTemp As Color '所抓取顏色
    Dim rnd = New Random() '用以產生亂數
    Dim rndNum As Integer '亂數數字
    Dim hWndShell, hWndTray, hWndPager, hWndToolBar As Integer '系統用hwnd
    Delegate Sub FunctionCallback() '改變狀態用call back sub
    Delegate Sub SetTextCallback(ByVal [text] As String) '設定狀態用call back sub
    Dim timeLoop As Integer '每次循環時間
    Dim savePath As String '儲存目錄
    Dim syncPath As String '同步目錄
    Dim maxLoop As Integer '最大循環時間
    Dim pInfo As New ProcessStartInfo '啟動Android Debug Bridge(Adb.exe)之啟動資訊
    Dim tried As Integer = 0 '嘗試清除次數，嘗試20次清除失敗則停止
    Dim ps As New Process '啟動Adb用之process
    Dim errorCount As Integer = 0 '逾時重啟次數
    Dim successPick As Integer = 0 '成功抽卡次數
    Dim PtCard As Integer = 0 '白金卡次數
    Dim induction As Boolean '是否在建立帳號流程中(遊戲開始之前置準備工作)

    Structure timeRecord
        Dim timeUsed As Integer '目前平均循環時間(秒/次)
        Dim recordCount As Integer '目前已紀錄筆數(次)
    End Structure
    Dim timeAvg As timeRecord '平均循環時間

    'BlueStacks視窗架構 (Caption, class)
    'Root: BlueStacks App Player for Windows (beta-1), WindowsForms10.Window.8.app.0.33c0d9d
    '  +: "", WindowsForms10.Window.8.app.0.33c0d9d
    '     +: "", WindowsForms10.Window.8.app.0.33c0d9d   此為下面面板
    '  +: "_ctl.Window", "BlueStacksApp"  此為中間執行區域
    '若直接以root呼叫代表中間執行區域

    Private Sub Dowork()
        Dim I As Integer
        Do
            SetText("準備啟動神魔之塔")
            timeLoop = 0 '時間歸零
            induction = True '開始前置準備(切記必須在時間歸零後才可改變此變數，否則會誤判)
            KillProcess() '確保程式已完全結束(強制結束處理程序)
            For I = 0 To 1 '重複改變兩次IP以及GUID，以防止創帳號封鎖
                If chkRas.Checked = True Then
                    SetText("重新連接網路第" & I + 1 & "次")
                    '斷線
                    Shell("rasdial.exe /disconnect", AppWinStyle.Hide, True)
                    '重新連線
                    Shell("rasdial.exe " & txtRasName.Text & " " & txtRasAcc.Text & " " & txtRasPW.Text, AppWinStyle.Hide, True)
                End If
                '改變GUID
                SetText("更改IMEI第" & I + 1 & "次")
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\BlueStacks", "USER_GUID", Guid.NewGuid.ToString)
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\BlueStacks\Guests\Android", "BootParameters", "noxsave noxsaveopt root=/dev/sda1 SRC=/android DATA=/dev/sdb1 SDCARD=/dev/sdc1 PREBUNDLEDAPPSFS=/dev/sdd1 HOST=WIN GUID=" & Guid.NewGuid.ToString & " VERSION=0.7.10.869 GlHotAttach=1 OEM=BlueStacks LANG=en_US armApps=true GlMode=1 P2DM=1")
            Next
            '以BlueStacks的HD-RunApp.exe配合參數啟動神魔之塔
            SetText("神魔之塔載入中...")
            Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.madhead.tos.zh com.unity3d.player.UnityPlayerActivity", AppWinStyle.NormalFocus, True, -1)
            '取得hwnd
            hwnd = FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)")
            SetText("強制視窗置頂")
            SetWindowPos(hwnd, -1, txtX.Text, txtY.Text, 0, 0, SWP_NOSIZE) '將視窗永遠置於最上層以便找色
            Dim dwStyle As Integer
            dwStyle = GetWindowLong(hwnd, -16) '取得目前視窗資訊
            dwStyle = dwStyle And Not (WS_MINIMIZEBOX) '去掉最小化按鈕
            SetWindowLong(hwnd, -16, dwStyle) '重新設定視窗
            SetForegroundWindow(hwnd) '使視窗獲得焦點
            '取得面板hwnd
            hwndInterface = FindWindowEx(hwnd, 0, "WindowsForms10.Window.8.app.0.33c0d9d", "")
            hwndInterface = FindWindowEx(hwndInterface, 0, "WindowsForms10.Window.8.app.0.33c0d9d", "")
            Thread.Sleep(20000 * speed) '等候遊戲載入
            SetText("嘗試開始遊戲")
            '確認是否已進入帳號選擇畫面
            WaitUntil(hwnd, 28, 187, Color.FromArgb(59, 92, 150), True)
            SetText("建立新帳號")
            BgdMouseLClick(hwnd, 188 + 65536 * 482) '直接遊戲
            '確認是否已進入輸入ID畫面
            WaitUntil(hwnd, 135, 290, Color.FromArgb(7, 205, 255), False)
            SetText("輸入ID")
            '輸入名字
            For I = 0 To 5   '6個字的ID
                rndNum = rnd.Next(1, 26)
                BgdMyKeyPress(hwnd, 64 + rndNum)
                Thread.Sleep(50)
            Next
            Thread.Sleep(1200 * speed)
            BgdMouseLClick(hwnd, 338 + 65536 * 607) '"完成"
            Thread.Sleep(1200 * speed)
            BgdMouseLClick(hwnd, 108 + 65536 * 287) '"確定"
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 111 + 65536 * 337) '"重覆確定"
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 61 + 65536 * 448) '選擇鄧肯(因為預設珠子之傷害高)
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 111 + 65536 * 435) '確定
            '確認是否已進入故事介紹畫面
            WaitUntil(hwnd, 199, 440, Color.FromArgb(7, 96, 144), False)
            SetText("開始遊戲")
            induction = False '前置準備完畢
            BgdMouseLClick(hwnd, 180 + 65536 * 434) '繼續
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 178 + 65536 * 268) '選主塔
            SetText("關卡1-1")
            '確認是否已進入戰鬥畫面
            WaitUntil(hwnd, 251, 282, Color.FromArgb(225, 119, 170), False)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '確認是否已結束示範畫面(出現光史萊姆)
            WaitUntil(hwnd, 179, 171, Color.FromArgb(225, 238, 119), True)
            BgdMouseLDown(hwnd, 210 + 65536 * 440) '攻擊
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 88 + 65536 * 440)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 88 + 65536 * 440)
            ' 1-1關卡結束
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡1-2")
            ' 1-2關卡開始
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 263 + 65536 * 377) '空轉
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 311 + 65536 * 376)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 311 + 65536 * 376)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 263 + 65536 * 377) '再次空轉
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 311 + 65536 * 376)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 311 + 65536 * 376)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 144 + 65536 * 504) '補血
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 32 + 65536 * 479)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 32 + 65536 * 479)
            '確認HP是否已回復
            WaitUntil(hwnd, 220, 279, Color.FromArgb(255, 174, 204), False)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 149 + 65536 * 379)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 25 + 65536 * 381)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 25 + 65536 * 381)
            '1-2關卡結束
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡1-3")
            '確認貓頭鷹是否消失 (開始示範)
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '確認是否出現貓頭鷹 (示範結束)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失 
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 209 + 65536 * 381)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 207 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 330 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 330 + 65536 * 499)
            ' 1-3關卡結束
            '確認螢幕下方是否出現隊伍欄位箭頭
            WaitUntil(hwnd, 29, 504, Color.FromArgb(221, 201, 118), True)
            SetText("新增隊伍成員")
            BgdMouseLClick(hwnd, 28 + 65536 * 553) ' 選隊伍
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 253 + 65536 * 233)
            '確認是否出現指向史萊姆的箭頭
            WaitUntil(hwnd, 109, 266, Color.FromArgb(230, 222, 151), False)
            BgdMouseLClick(hwnd, 113 + 65536 * 230)
            '確認是否出現「選擇」藍色方塊
            WaitUntil(hwnd, 214, 326, Color.FromArgb(4, 80, 117), False)
            BgdMouseLClick(hwnd, 180 + 65536 * 326)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失 (隊友選擇完畢)
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 180 + 65536 * 530) ' 選擇地區
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 180 + 65536 * 289) '主塔
            '確認是否出現「選擇」藍色方塊
            WaitUntil(hwnd, 205, 347, Color.FromArgb(2, 101, 150), True, 160, 202)
            BgdMouseLClick(hwnd, 179 + 65536 * 353) '選擇
            '確認是否出現「進入戰鬥」藍色方塊
            WaitUntil(hwnd, 260, 484, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 302 + 65536 * 498) '進入戰鬥
            '確認是否出現貓頭鷹 (2-1關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡2-1")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 183 + 65536 * 165) '點擊怪物
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 149 + 65536 * 318) '指定怪物攻擊
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 145 + 65536 * 442)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 29 + 65536 * 442)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 29 + 65536 * 442)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 29 + 65536 * 437) '全體攻擊
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 329 + 65536 * 437)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 329 + 65536 * 437)
            ' 2-1關卡結束
            '確認是否出現貓頭鷹 (2-2關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡2-2")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 311 + 65536 * 320)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 207 + 65536 * 323)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 207 + 65536 * 323)
            ' 2-2關卡結束
            '確認是否出現貓頭鷹 (2-3關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡2-3")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 33 + 65536 * 436)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 33 + 65536 * 495)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 91 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 94 + 65536 * 565)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 264 + 65536 * 588)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 268 + 65536 * 438)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 205 + 65536 * 411)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 212 + 65536 * 556)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 144 + 65536 * 559)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 144 + 65536 * 559)
            ' 2-3關卡結束
            '確認是否出現交友申請的「取消」方塊
            WaitUntil(hwnd, 270, 389, Color.FromArgb(5, 100, 147), True)
            BgdMouseLClick(hwnd, 247 + 65536 * 392) '取消
            '確認是否出現貓頭鷹 (強化卡片介紹)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '確認是否出現指向背包的箭頭
            WaitUntil(hwnd, 93, 511, Color.FromArgb(228, 227, 160), False)
            SetText("強化卡片")
            BgdMouseLClick(hwnd, 89 + 65536 * 556) '背包
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 46 + 65536 * 229) '選擇鄧肯
            '確認是否出現「強化合成」方塊
            WaitUntil(hwnd, 227, 322, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 177 + 65536 * 326)
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 40 + 65536 * 229) '素材欄位1
            '確認是否出現指向素材的箭頭
            WaitUntil(hwnd, 110, 240, Color.FromArgb(221, 220, 153), False)
            BgdMouseLClick(hwnd, 110 + 65536 * 200) '選擇素材
            '確認是否出現「選擇」方塊
            WaitUntil(hwnd, 230, 320, Color.FromArgb(2, 84, 125), False)
            BgdMouseLClick(hwnd, 183 + 65536 * 324) '選擇
            '確認是否出現「確定」方塊
            WaitUntil(hwnd, 249, 487, Color.FromArgb(9, 106, 149), False)
            BgdMouseLClick(hwnd, 298 + 65536 * 497) '確定
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 167 + 65536 * 530) '選擇地區
            Thread.Sleep(1000 * speed)
            BgdMouseLClick(hwnd, 167 + 65536 * 530) '選擇地區(避免沒點到)
            '確認是否出現「選擇」藍色方塊
            WaitUntil(hwnd, 205, 347, Color.FromArgb(2, 101, 150), True, 160, 202)
            BgdMouseLClick(hwnd, 179 + 65536 * 353) '選擇
            '確認是否出現「進入戰鬥」藍色方塊
            WaitUntil(hwnd, 260, 484, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 302 + 65536 * 498) '進入戰鬥
            '確認是否出現貓頭鷹 (3-1關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡3-1")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 87 + 65536 * 318)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 91 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 91 + 65536 * 499)
            ' 3-1關卡結束
            '確認是否出現貓頭鷹 (3-2關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡3-2")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 23 + 65536 * 246) '主角技能
            '確認是否出現「確認視窗」遮蓋住生命條
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 108 + 65536 * 335) '確定
            ' 3-2關卡結束
            '確認是否出現貓頭鷹 (3-3關卡開始)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("關卡3-3")
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 84 + 65536 * 243) '技能1
            '確認是否出現「確認視窗」遮蓋住生命條
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '確定
            Thread.Sleep(4450 * speed)
            BgdMouseLClick(hwnd, 145 + 65536 * 241) '技能2
            '確認是否出現「確認視窗」遮蓋住生命條
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '確定
            Thread.Sleep(4450 * speed)
            BgdMouseLClick(hwnd, 215 + 65536 * 247) '技能3
            '確認是否出現「確認視窗」遮蓋住生命條
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '確定
            Thread.Sleep(4450 * speed)
            BgdMouseLDown(hwnd, 328 + 65536 * 503) '攻擊
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 329 + 65536 * 441)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 273 + 65536 * 441)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 273 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 162 + 65536 * 500)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 155 + 65536 * 557)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 34 + 65536 * 557)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 34 + 65536 * 443)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 102 + 65536 * 440)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 97 + 65536 * 495)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 30 + 65536 * 495)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 32 + 65536 * 437)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 32 + 65536 * 437)
            Thread.Sleep(2000 * speed)
            ' 3-3關卡結束
            '確認是否出現交友申請的「取消」方塊
            WaitUntil(hwnd, 270, 389, Color.FromArgb(5, 100, 147), True)
            BgdMouseLClick(hwnd, 247 + 65536 * 392) '取消
            '確認是否出現貓頭鷹
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '確認貓頭鷹是否消失
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '確認是否出現指向背包的箭頭
            WaitUntil(hwnd, 266, 511, Color.FromArgb(249, 249, 169), False)
            SetText("抽卡")
            BgdMouseLClick(hwnd, 263 + 65536 * 561) '商店
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 173 + 65536 * 284) '封印卡
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 173 + 65536 * 284) '魔法石
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 184 + 65536 * 381) '確定
            '確認是否出現抽卡的手指
            WaitUntil(hwnd, 166, 88, Color.FromArgb(230, 226, 220), False)
            BgdMouseLDown(hwnd, 168 + 65536 * 126) '抽卡
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 175 + 65536 * 502)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 175 + 65536 * 502)
            Thread.Sleep(15000 * speed)
            successPick = successPick + 1 '成功次數+1
            '判斷是否五星
            GetWindowInfo(hwnd) '取得視窗資訊，以便找色
            hDC = GetDC(0) '取得桌面hDC
            colorTemp = ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + 143, Client_y0 + 91))
            '底色RGB = 102, 77, 28
            '以ColorMatch函數判定相似度，RGB誤差在15%內視為相同顏色
            If ColorMatch(colorTemp, Color.FromArgb(102, 77, 28), 0.85) Then
                SetText("五星以下，直接清除")
                backup = False '沒五星
            Else
                SetText("五星以上，開始備份")
                PtCard = PtCard + 1 '白金卡次數+1
                backup = True '五星以上
                '存圖
                GetWindowInfo(hwnd)
                Dim Screenshot As New Bitmap(360, 640, PixelFormat.Format32bppArgb)
                Dim Graph As Graphics = Graphics.FromImage(Screenshot)
                Graph.CopyFromScreen(Client_x0, Client_y0, 0, 0, New Size(360, 640), CopyPixelOperation.SourceCopy)
                Screenshot.Save(savePath & Format(num, "00000") & ".png")
                Thread.Sleep(300 * speed)
            End If
            RecordFlash() '更新紀錄
            ReleaseDC(0, hDC) '釋放桌面hDC
            Thread.Sleep(300 * speed)
            If backup = True Then '備份首抽
                If chkRoot.Checked = True Then '有ROOT權限，直接執行ADB指令
                    SetText("以ADB備份")
                    '建立備份用資料夾
                    pInfo.Arguments = "-s emulator-5554 shell mkdir /sdcard/ToSBackup"
                    ps.StartInfo = pInfo '設定啟動資訊
                    ps.Start() '建立資料夾
                    ps.WaitForExit()
                    '以內建的tar方式備份並壓縮成gzip，排除系統文件lib不進行備份
                    pInfo.Arguments = "-s emulator-5554 shell su -c ""tar cvpzf - data/data/com.madhead.tos.zh --exclude=lib > /sdcard/ToSBackup/" & Format(num, "00000") & ".tar.gz"""
                    ps.StartInfo = pInfo '設定啟動資訊
                    ps.Start() '備份
                    ps.WaitForExit() '等待備份完成
                    If chkSync.Checked = True Then
                        pInfo.Arguments = "-s emulator-5554 pull /sdcard/ToSBackup/ " & syncPath
                        ps.StartInfo = pInfo '設定啟動資訊
                        ps.Start() '備份到本機資料夾
                        ps.WaitForExit()
                    End If
                    num = num + 1 '備份編號+1
                Else '以模擬按鍵動作執行MyBackupPro備份
                    SetText("以MyBackupPro備份")
                    '以BlueStacks的HD-RunApp.exe配合參數啟動MyBackup Pro
                    Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.rerware.android.MyBackupPro com.rerware.android.MyBackupPro.MyBackup", AppWinStyle.NormalFocus, True, -1)
                    '確認是否已經讀取完畢(以色彩偵測)
                    'WaitUntil(hwnd, 65, 318, Color.FromArgb(172, 225, 250), False)
                    '等待讀取完畢
                    Thread.Sleep(5000 * speed)
                    Thread.Sleep(300 * speed)
                    BgdMouseLClick(hwnd, 63 + 65536 * 96) '點擊備份
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 75 + 65536 * 321) '備份到SD卡
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 75 + 65536 * 321) '備份程式
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 29 + 65536 * 228) '點出下拉選單
                    Thread.Sleep(1500 * speed)
                    For I = 0 To 10 '向下拉動選單
                        BgdMouseLDown(hwnd, 218 + 65536 * 389)
                        Thread.Sleep(20 * 1.5 * speed)
                        BgdMouseMove(hwnd, 218 + 65536 * 339)
                        Thread.Sleep(20 * 1.5 * speed)
                        BgdMouseLClick(hwnd, 218 + 65536 * 64)
                    Next
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 33 + 65536 * 147) '點選神魔
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 100 + 65536 * 589) '確認
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 314 + 65536 * 330) '點選以編輯文字
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 300 + 65536 * 207) '移到文字尾端
                    Thread.Sleep(1500 * speed)
                    For I = 0 To 30 '清除目前所有文字
                        BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.Back)
                        Thread.Sleep(50 * speed)
                    Next
                    '輸入流水號
                    For I = 4 To 0 Step -1
                        Select Case Int((num Mod 10 ^ (I + 1)) / (10 ^ I))
                            Case 0
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D0)
                            Case 1
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D1)
                            Case 2
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D2)
                            Case 3
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D3)
                            Case 4
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D4)
                            Case 5
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D5)
                            Case 6
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D6)
                            Case 7
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D7)
                            Case 8
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D8)
                            Case 9
                                BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.D9)
                        End Select
                        Thread.Sleep(75 * speed)
                    Next
                    BgdMouseLClick(hwnd, 110 + 65536 * 272) '確認
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 179 + 65536 * 390) '數據
                    Thread.Sleep(4500 * speed)
                    BgdMouseLClick(hwnd, 183 + 65536 * 410) '確認
                    Thread.Sleep(1500 * speed)
                    num = num + 1
                    If chkSync.Checked = True Then
                        SetText("同步資料")
                        pInfo.Arguments = "-s emulator-5554 pull /sdcard/rerware/MyBackup/AllAppsBackups " & syncPath
                        ps.StartInfo = pInfo '設定啟動資訊
                        ps.Start() '備份到本機資料夾
                        ps.WaitForExit()
                    End If
                End If
            End If
            SetText("清除資料")
            '執行內建的Android Debug Bridge(HD-adb.exe)以清除程式資料
            '此處利用shell執行pm clear <PACKAGE> 來清除指定程式資料
            '詳細說明詳見︰http://developer.android.com/tools/help/adb.html
            pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '設定參數為「清除神魔之塔資料」
            ps.StartInfo = pInfo '設定啟動資訊
            tried = 0 '嘗試次數歸零
            Do
                ps.Start() '啟動
                tried = tried + 1
            Loop Until (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Or tried > 10
            SetText("結束BlueStacks")
            '以HD-Quit結束程式(內建方法)
            Shell(Environ("ProgramFiles") & "\BlueStacks\HD-Quit.exe", AppWinStyle.NormalFocus, True, -1)
            SetText("嘗試抹除通知區域圖示")
            '抹除通知區域圖示
            hWndShell = FindWindow("Shell_TrayWnd", "") '取得系統圖示區hwnd
            '取得通知區域hwnd
            If Environment.OSVersion.Version.Major = 5 AndAlso Environment.OSVersion.Version.Minor = 0 Then
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndToolBar = FindWindowEx(hWndTray, 0, "ToolbarWindow32", "")
            Else
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndPager = FindWindowEx(hWndTray, 0, "SysPager", "")
                hWndToolBar = FindWindowEx(hWndPager, 0, "ToolbarWindow32", "使用者升級的通知區域")
            End If
            '取得邊界
            GetWindowInfo(hWndToolBar)
            Dim ibx, iby As Integer
            iby = CInt(ClientY / 2) * 65536
            '以WM_MOUSEMOVE消去無效圖示
            For ibx = 1 To ClientX Step 1
                PostMessage(hWndToolBar, WM_MOUSEMOVE, 0, iby + ibx)
            Next
            SetText("更新平均時間")
            '紀錄本次使用時間並計算平均值
            timeAvg.timeUsed = (timeAvg.timeUsed * timeAvg.recordCount + timeLoop) / (timeAvg.recordCount + 1) '計算平均值
            timeAvg.recordCount = timeAvg.recordCount + 1 '已完成循環次數+1
            ReflashTime() '更新時間
        Loop Until (num > 99999) '執行到流水號999為止
        '結束
        Beep()
        SetState() '回復初始狀態
    End Sub

    Private Sub SetText(ByVal [text] As String) '設定狀態文字
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '假如執行緒ID不同，使用Callback方法操作控制項
        If Me.lblStatus.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.lblStatus.Text = [text]
        End If
    End Sub

    Private Sub RecordFlash() '回復初始狀態
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '假如執行緒ID不同，使用Callback方法操作控制項
        If Me.btnStart.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf RecordFlash)
            Me.Invoke(d)
        Else
            Me.lblSuccess.Text = successPick & "次"
            Me.lblPt.Text = PtCard & "次"
        End If
    End Sub

    Private Sub SetState() '回復初始狀態
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '假如執行緒ID不同，使用Callback方法操作控制項
        If Me.btnStart.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf SetState)
            Me.Invoke(d)
        Else
            TimerAbort.Enabled = False '停止計時器
            btnStart.Text = "開始" '設定按鈕文字
            lblStatus.Text = "Idle"
            btnStart.ForeColor = Color.Black '設定按鈕文字顏色
            txtNum.Text = num.ToString '更新起始編號
        End If
    End Sub

    Private Sub ReflashTime()
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '假如執行緒ID不同，使用Callback方法操作控制項
        If Me.lblTimeAvg.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf ReflashTime)
            Me.Invoke(d)
        Else
            lblTimeAvg.Text = timeAvg.timeUsed & "秒/次"
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtSpeed.Text = Format(Single.Parse(txtSpeed.Text) + 0.05, ".00")
        speed = Single.Parse(txtSpeed.Text)
    End Sub

    Private Sub btnMiuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMiuns.Click
        If Single.Parse(txtSpeed.Text) > 1.0 Then
            txtSpeed.Text = Format(Single.Parse(txtSpeed.Text) - 0.05, ".00")
            speed = Single.Parse(txtSpeed.Text)
        End If
    End Sub

    Private Sub frmToS_AutoPick_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '將變數寫入設定檔
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", speed & ",", False)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtNum.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", maxLoop & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", savePath & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", chkRas.Checked.ToString & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtRasName.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtRasAcc.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtRasPW.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtX.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", txtY.Text & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", chkSync.Checked.ToString & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", syncPath & ",", True)
        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\ToS_AutoPick.txt", chkRoot.Checked.ToString & ",", True)
    End Sub

    Private Sub frmToS_AutoPick_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\ToS_AutoPick.txt") Then
            Try
                Dim Settingreader As Microsoft.VisualBasic.FileIO.TextFieldParser
                Settingreader = My.Computer.FileSystem.OpenTextFieldParser(Application.StartupPath & "\ToS_AutoPick.txt")
                Settingreader.SetDelimiters(",")
                Dim SettingFields As String() = Settingreader.ReadFields() '讀取設定檔
                Dim count As Integer = 0 '判斷欄位
                '循序讀取設定檔，以,判斷欄位
                For Each currentField As String In SettingFields
                    Select Case count
                        Case 0 '速度
                            speed = currentField
                            txtSpeed.Text = Format(speed, ".00")
                        Case 1 '起始編號
                            num = currentField
                            txtNum.Text = num
                        Case 2 '最大循環時間
                            Select Case currentField / 60 '將秒轉換為分
                                Case 10
                                    rad10.Checked = True
                                Case 15
                                    rad15.Checked = True
                                Case 20
                                    rad20.Checked = True
                            End Select
                        Case 3 '儲存目錄
                            savePath = currentField
                            txtPath.Text = savePath
                        Case 4 '是否自動連線
                            If currentField = "True" Then
                                chkRas.Checked = True
                            Else
                                chkRas.Checked = False
                            End If
                        Case 5 '連線名稱
                            txtRasName.Text = currentField
                        Case 6 '連線帳號
                            txtRasAcc.Text = currentField
                        Case 7 '連線密碼
                            txtRasPW.Text = currentField
                        Case 8 '視窗座標X
                            txtX.Text = currentField
                        Case 9 '視窗座標Y
                            txtY.Text = currentField
                        Case 10 '是否自動同步
                            If currentField = "True" Then
                                chkSync.Checked = True
                            Else
                                chkSync.Checked = False
                            End If
                        Case 11 '同步目錄
                            syncPath = currentField
                            txtSync.Text = syncPath
                        Case 12 '是否以Root模式備份
                            If currentField = "True" Then
                                chkRoot.Checked = True
                            Else
                                chkRoot.Checked = False
                            End If
                    End Select
                    count = count + 1 '進入下一個欄位
                Next
            Catch ex As Exception '讀取檔案出現錯誤，重新設定參數
                speed = 1.0 '設定變速參數
                txtSpeed.Text = "1.00" '顯示變速參數
                num = 1 '設定起始編號
                txtNum.Text = "1" '顯示起始編號
                savePath = Application.StartupPath & "\Picture\" '設定擷圖目錄"
                txtPath.Text = savePath '顯示擷圖目錄
                syncPath = Application.StartupPath & "\Sync\" '設定同步目錄"
                txtSync.Text = syncPath '顯示同步目錄
                maxLoop = 900 '預設最大循環時間為15分
                rad15.Checked = True '顯示最大循環時間
                txtX.Text = 23 '預設XY座標
                txtY.Text = 45
            End Try
        Else '讀取不到檔案，重新設定參數
            speed = 1.0 '設定變速參數
            txtSpeed.Text = "1.00" '顯示變速參數
            num = 1 '設定起始編號
            txtNum.Text = "1" '顯示起始編號
            savePath = Application.StartupPath & "\Picture\" '設定擷圖目錄
            txtPath.Text = savePath '顯示擷圖目錄
            syncPath = Application.StartupPath & "\Sync\" '設定同步目錄"
            txtSync.Text = syncPath '顯示同步目錄
            maxLoop = 900 '預設最大循環時間為15分
            rad15.Checked = True '顯示最大循環時間
            txtX.Text = 23 '預設XY座標
            txtY.Text = 45
        End If
        '設定啟動Android Debug Bridge之process參數
        pInfo.FileName = Environ("ProgramFiles") & "\BlueStacks\HD-Adb.exe"
        pInfo.UseShellExecute = False
        pInfo.RedirectStandardInput = False
        pInfo.RedirectStandardOutput = True
        pInfo.CreateNoWindow = True
    End Sub

    Private Sub txtNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNum.KeyPress
        '禁止輸入非數字
        If Asc(e.KeyChar) > 57 Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) < 48 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        If btnStart.Text = "開始" Then '按下時，假如thread未啟動，啟動thread
            btnStart.Text = "停止..." '設定按鈕文字
            btnStart.ForeColor = Color.Red '設定按鈕文字顏色
            If txtNum.Text <> "" Then '設定起始編號
                num = Integer.Parse(txtNum.Text) '將文字轉為整數
            Else
                num = 1 '預設起始編號1
            End If
            Work = New Thread(AddressOf Me.Dowork) '建立新thread
            Work.IsBackground = True
            TimerAbort.Enabled = True '計時器開啟
            Work.Start() '開始thread
        ElseIf btnStart.Text = "停止..." Then '按下時，假如thread執行中，終止thread
            Work.Abort() '停止thread
            SetState() '回復初始狀態
        End If
    End Sub

    '比對兩個顏色是否相似，similar參數代表相似度(0.1-1.0)
    Private Function ColorMatch(sample As Color, standard As Color, similar As Single) As Boolean
        '以色差方式計算相似度
        Dim varR As Integer
        Dim varG As Integer
        Dim varB As Integer
        varR = CInt(sample.R) - CInt(standard.R)
        varG = CInt(sample.G) - CInt(standard.G)
        varB = CInt(sample.B) - CInt(standard.B)
        If Math.Sqrt(varR ^ 2 + varG ^ 2 + varB ^ 2) < 441.67 * (1 - similar) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub TimerAbort_Tick(sender As Object, e As System.EventArgs) Handles TimerAbort.Tick
        timeLoop = timeLoop + 1 '每秒+1
        lblTime.Text = timeLoop & "秒經過" '顯示經過時間
        '若卡在建立新帳號狀態，則超過循環時間的五分之一強制重開
        If timeLoop > maxLoop Or (induction = True And timeLoop > (maxLoop / 5)) Then '大於設定時間強制結束程式
            errorCount = errorCount + 1 '逾時次數+1
            lblError.Text = errorCount & "次" '顯示逾時次數
            SetText("超過循環時間，強制中止")
            '將時間歸零
            timeLoop = 0 '不停止計時器原因為︰此過程也有可能stuck，若stuck超過設定時間也會自動重啟
            '強制結束工作 
            Work.Abort()
            KillProcess() '確保程式已完全結束(結束處理程序)
            '清除紀錄
            SetText("清除資料")
            '以BlueStacks的HD-RunApp.exe配合參數啟動神魔之塔
            Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.madhead.tos.zh com.unity3d.player.UnityPlayerActivity", AppWinStyle.NormalFocus, True, -1)
            '執行內建的Android Debug Bridge(HD-adb.exe)以清除程式資料
            '此處利用shell執行pm clear <PACKAGE> 來清除指定程式資料
            '詳細說明詳見︰http://developer.android.com/tools/help/adb.html
            pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '設定參數為「清除神魔之塔資料」
            ps.StartInfo = pInfo '設定啟動資訊
            tried = 0 '嘗試次數歸零
            Do
                ps.Start() '啟動
                tried = tried + 1
            Loop Until (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Or tried > 10
            '以HD-Quit結束程式(內建方法)
            SetText("結束BlueStacks")
            Shell(Environ("ProgramFiles") & "\BlueStacks\HD-Quit.exe", AppWinStyle.NormalFocus, True, -1)
            '抹除通知區域圖示
            SetText("嘗試抹除通知區域圖示")
            hWndShell = FindWindow("Shell_TrayWnd", "") '取得系統圖示區hwnd
            '取得通知區域hwnd
            If Environment.OSVersion.Version.Major = 5 AndAlso Environment.OSVersion.Version.Minor = 0 Then
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndToolBar = FindWindowEx(hWndTray, 0, "ToolbarWindow32", "")
            Else
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndPager = FindWindowEx(hWndTray, 0, "SysPager", "")
                hWndToolBar = FindWindowEx(hWndPager, 0, "ToolbarWindow32", "使用者升級的通知區域")
            End If
            '取得邊界
            GetWindowInfo(hWndToolBar)
            Dim ibx, iby As Integer
            iby = CInt(ClientY / 2) * 65536
            '以WM_MOUSEMOVE消去無效圖示
            For ibx = 1 To ClientX Step 1
                PostMessage(hWndToolBar, WM_MOUSEMOVE, 0, iby + ibx)
            Next
            SetText("重新開始")
            '重新開始
            Work = New Threading.Thread(AddressOf Me.Dowork)
            Work.IsBackground = True
            Work.Start()
        End If
    End Sub

    '重複循環直到targetColor在(x, y)出現
    'click代表是否要點擊螢幕，預設點擊座標(180, 280)
    '預設比對準確度0.85
    '所有座標以該視窗為準，即左上角為(0, 0)
    Private Sub WaitUntil(hwnd As Integer, x As Integer, y As Integer, targetColor As Color, click As Boolean, Optional clickX As Integer = 180, Optional clickY As Integer = 280)
        hDC = GetDC(0) '取得桌面hDC以偵測
        Do
            If click = True Then
                BgdMouseLClick(hwnd, clickX + 65536 * clickY)
                Thread.Sleep(50)
            End If
            GetWindowInfo(hwnd) '取得視窗資訊，以便找色
        Loop Until ColorMatch(ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + x, Client_y0 + y)), targetColor, 0.9) = True
        ReleaseDC(0, hDC) '釋放桌面hDC
        Thread.Sleep(1000 * speed)
    End Sub

    '重複循環直到targetColor不在(x, y)出現
    'click代表是否要點擊螢幕，預設點擊座標(180, 280)
    '預設比對準確度0.85
    '所有座標以該視窗為準，即左上角為(0, 0)
    Private Sub WaitUntilNot(hwnd As Integer, x As Integer, y As Integer, targetColor As Color, click As Boolean, Optional clickX As Integer = 180, Optional clickY As Integer = 280)
        hDC = GetDC(0) '取得桌面hDC以偵測
        Do
            If click = True Then
                BgdMouseLClick(hwnd, clickX + 65536 * clickY)
                Thread.Sleep(50)
            End If
            GetWindowInfo(hwnd) '取得視窗資訊，以便找色
        Loop Until ColorMatch(ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + x, Client_y0 + y)), targetColor, 0.9) = False
        ReleaseDC(0, hDC) '釋放桌面hDC
        Thread.Sleep(1000 * speed)
    End Sub

    Private Sub btnSetPath_Click(sender As System.Object, e As System.EventArgs) Handles btnSetPath.Click
        Dim pathDialog As New FolderBrowserDialog()
        Try
            pathDialog.RootFolder = Environment.SpecialFolder.MyComputer
            If pathDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                savePath = pathDialog.SelectedPath & "\" '記得加上\
                txtPath.Text = savePath
            End If
        Catch ex As Exception
            MsgBox("請設定目錄")
        End Try
    End Sub

    Private Sub rad10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rad10.CheckedChanged
        maxLoop = 600
    End Sub

    Private Sub rad15_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rad15.CheckedChanged
        maxLoop = 900
    End Sub

    Private Sub rad20_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rad20.CheckedChanged
        maxLoop = 1200
    End Sub

    Private Sub txtX_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtX.KeyPress
        '禁止輸入非數字
        If Asc(e.KeyChar) > 57 Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) < 48 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtY_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtY.KeyPress
        '禁止輸入非數字
        If Asc(e.KeyChar) > 57 Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) < 48 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnAbout_Click(sender As System.Object, e As System.EventArgs) Handles btnAbout.Click
        AboutBox1.Show()
    End Sub

    Private Sub btnRelease_Click(sender As System.Object, e As System.EventArgs) Handles btnRelease.Click
        If FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)") = 0 Then
            MsgBox("請開啟BlueStacks", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '解除視窗置頂
        SetWindowPos(FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)"), -2, 0, 0, 0, 0, SWP_NOSIZE Or SWP_NOMOVE) '取消視窗置頂
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        If FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)") = 0 Then
            MsgBox("請開啟BlueStacks再清除資料", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("是否確定清除資料，本動作將無法還原", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        '執行內建的Android Debug Bridge(HD-adb.exe)以清除程式資料
        '此處利用shell執行pm clear <PACKAGE> 來清除指定程式資料
        '詳細說明詳見︰http://developer.android.com/tools/help/adb.html
        pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '設定參數為「清除神魔之塔資料」
        ps.StartInfo = pInfo '設定啟動資訊
        ps.Start() '啟動
        If (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Then
            MsgBox("成功清除資料！")
        Else
            MsgBox("清除資料失敗！")
        End If
    End Sub

    Private Sub btnLock_Click(sender As System.Object, e As System.EventArgs) Handles btnLock.Click
        If FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)") = 0 Then
            MsgBox("請開啟BlueStacks", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        hwnd = FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)")
        SetWindowPos(hwnd, -1, txtX.Text, txtY.Text, 0, 0, SWP_NOSIZE) '將視窗永遠置於最上層以便找色
        SetForegroundWindow(hwnd) '使視窗獲得焦點
    End Sub

    Private Sub btnSetSync_Click(sender As System.Object, e As System.EventArgs) Handles btnSetSync.Click
        Dim pathDialog As New FolderBrowserDialog()
        Try
            pathDialog.RootFolder = Environment.SpecialFolder.MyComputer
            If pathDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                syncPath = pathDialog.SelectedPath
                txtSync.Text = syncPath
            End If
        Catch ex As Exception
            MsgBox("請設定目錄")
        End Try
    End Sub

    Private Sub chkRoot_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRoot.CheckedChanged
        If chkRoot.Checked = True Then
            MsgBox("請確定Bluestacks有Root權限，否則將會備份失敗")
        End If
    End Sub

    Private Sub KillProcess() '強制關閉所有BlueStacks相關處理程序
        Dim plist() As System.Diagnostics.Process
        Dim p As Process
        plist = System.Diagnostics.Process.GetProcessesByName("HD-Adb")
        For Each p In plist
            p.Kill()
        Next
        plist = System.Diagnostics.Process.GetProcessesByName("HD-Agent")
        For Each p In plist
            p.Kill()
        Next
        plist = System.Diagnostics.Process.GetProcessesByName("HD-Frontend")
        For Each p In plist
            p.Kill()
        Next
        plist = System.Diagnostics.Process.GetProcessesByName("HD-RunApp")
        For Each p In plist
            p.Kill()
        Next
        plist = System.Diagnostics.Process.GetProcessesByName("HD-Service")
        For Each p In plist
            p.Kill()
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'hwnd = FindWindow("MozillaWindowClass", "sendmessage - Send Message in C# - Stack Overflow - Mozilla Firefox")
        BgdMouseLDown(&H571F42, 100 + 100 * 65536)
        BgdMouseMove(&H571F42, 200 + 200 * 65536)
    End Sub
End Class