Imports System.Threading
Imports System.Drawing.Imaging

Friend Class frmToS_AutoPick
    Inherits System.Windows.Forms.Form
    Dim Work As Thread
    Dim hwnd As Integer '�~��hwnd
    Dim hwndInterface As Integer '���Ohwnd
    Dim speed As Single '�ΥH�վ�t��
    Dim hDC As Integer '����
    Dim backup As Boolean '�O�_�O�s�Ӭ���
    Dim num As Integer '�����s��
    Dim colorTemp As Color '�ҧ���C��
    Dim rnd = New Random() '�ΥH���Ͷü�
    Dim rndNum As Integer '�üƼƦr
    Dim hWndShell, hWndTray, hWndPager, hWndToolBar As Integer '�t�Υ�hwnd
    Delegate Sub FunctionCallback() '���ܪ��A��call back sub
    Delegate Sub SetTextCallback(ByVal [text] As String) '�]�w���A��call back sub
    Dim timeLoop As Integer '�C���`���ɶ�
    Dim savePath As String '�x�s�ؿ�
    Dim syncPath As String '�P�B�ؿ�
    Dim maxLoop As Integer '�̤j�`���ɶ�
    Dim pInfo As New ProcessStartInfo '�Ұ�Android Debug Bridge(Adb.exe)���Ұʸ�T
    Dim tried As Integer = 0 '���ղM�����ơA����20���M�����ѫh����
    Dim ps As New Process '�Ұ�Adb�Τ�process
    Dim errorCount As Integer = 0 '�O�ɭ��Ҧ���
    Dim successPick As Integer = 0 '���\��d����
    Dim PtCard As Integer = 0 '�ժ��d����
    Dim induction As Boolean '�O�_�b�إ߱b���y�{��(�C���}�l���e�m�ǳƤu�@)

    Structure timeRecord
        Dim timeUsed As Integer '�ثe�����`���ɶ�(��/��)
        Dim recordCount As Integer '�ثe�w��������(��)
    End Structure
    Dim timeAvg As timeRecord '�����`���ɶ�

    'BlueStacks�����[�c (Caption, class)
    'Root: BlueStacks App Player for Windows (beta-1), WindowsForms10.Window.8.app.0.33c0d9d
    '  +: "", WindowsForms10.Window.8.app.0.33c0d9d
    '     +: "", WindowsForms10.Window.8.app.0.33c0d9d   �����U�����O
    '  +: "_ctl.Window", "BlueStacksApp"  ������������ϰ�
    '�Y�����Hroot�I�s�N��������ϰ�

    Private Sub Dowork()
        Dim I As Integer
        Do
            SetText("�ǳƱҰʯ��]����")
            timeLoop = 0 '�ɶ��k�s
            induction = True '�}�l�e�m�ǳ�(���O�����b�ɶ��k�s��~�i���ܦ��ܼơA�_�h�|�~�P)
            KillProcess() '�T�O�{���w��������(�j����B�z�{��)
            For I = 0 To 1 '���Ƨ��ܨ⦸IP�H��GUID�A�H����бb������
                If chkRas.Checked = True Then
                    SetText("���s�s��������" & I + 1 & "��")
                    '�_�u
                    Shell("rasdial.exe /disconnect", AppWinStyle.Hide, True)
                    '���s�s�u
                    Shell("rasdial.exe " & txtRasName.Text & " " & txtRasAcc.Text & " " & txtRasPW.Text, AppWinStyle.Hide, True)
                End If
                '����GUID
                SetText("���IMEI��" & I + 1 & "��")
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\BlueStacks", "USER_GUID", Guid.NewGuid.ToString)
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\BlueStacks\Guests\Android", "BootParameters", "noxsave noxsaveopt root=/dev/sda1 SRC=/android DATA=/dev/sdb1 SDCARD=/dev/sdc1 PREBUNDLEDAPPSFS=/dev/sdd1 HOST=WIN GUID=" & Guid.NewGuid.ToString & " VERSION=0.7.10.869 GlHotAttach=1 OEM=BlueStacks LANG=en_US armApps=true GlMode=1 P2DM=1")
            Next
            '�HBlueStacks��HD-RunApp.exe�t�X�ѼƱҰʯ��]����
            SetText("���]������J��...")
            Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.madhead.tos.zh com.unity3d.player.UnityPlayerActivity", AppWinStyle.NormalFocus, True, -1)
            '���ohwnd
            hwnd = FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)")
            SetText("�j������m��")
            SetWindowPos(hwnd, -1, txtX.Text, txtY.Text, 0, 0, SWP_NOSIZE) '�N�����û��m��̤W�h�H�K���
            Dim dwStyle As Integer
            dwStyle = GetWindowLong(hwnd, -16) '���o�ثe������T
            dwStyle = dwStyle And Not (WS_MINIMIZEBOX) '�h���̤p�ƫ��s
            SetWindowLong(hwnd, -16, dwStyle) '���s�]�w����
            SetForegroundWindow(hwnd) '�ϵ�����o�J�I
            '���o���Ohwnd
            hwndInterface = FindWindowEx(hwnd, 0, "WindowsForms10.Window.8.app.0.33c0d9d", "")
            hwndInterface = FindWindowEx(hwndInterface, 0, "WindowsForms10.Window.8.app.0.33c0d9d", "")
            Thread.Sleep(20000 * speed) '���ԹC�����J
            SetText("���ն}�l�C��")
            '�T�{�O�_�w�i�J�b����ܵe��
            WaitUntil(hwnd, 28, 187, Color.FromArgb(59, 92, 150), True)
            SetText("�إ߷s�b��")
            BgdMouseLClick(hwnd, 188 + 65536 * 482) '�����C��
            '�T�{�O�_�w�i�J��JID�e��
            WaitUntil(hwnd, 135, 290, Color.FromArgb(7, 205, 255), False)
            SetText("��JID")
            '��J�W�r
            For I = 0 To 5   '6�Ӧr��ID
                rndNum = rnd.Next(1, 26)
                BgdMyKeyPress(hwnd, 64 + rndNum)
                Thread.Sleep(50)
            Next
            Thread.Sleep(1200 * speed)
            BgdMouseLClick(hwnd, 338 + 65536 * 607) '"����"
            Thread.Sleep(1200 * speed)
            BgdMouseLClick(hwnd, 108 + 65536 * 287) '"�T�w"
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 111 + 65536 * 337) '"���нT�w"
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 61 + 65536 * 448) '��ܾH��(�]���w�]�]�l���ˮ`��)
            Thread.Sleep(1800 * speed)
            BgdMouseLClick(hwnd, 111 + 65536 * 435) '�T�w
            '�T�{�O�_�w�i�J�G�Ƥ��еe��
            WaitUntil(hwnd, 199, 440, Color.FromArgb(7, 96, 144), False)
            SetText("�}�l�C��")
            induction = False '�e�m�ǳƧ���
            BgdMouseLClick(hwnd, 180 + 65536 * 434) '�~��
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 178 + 65536 * 268) '��D��
            SetText("���d1-1")
            '�T�{�O�_�w�i�J�԰��e��
            WaitUntil(hwnd, 251, 282, Color.FromArgb(225, 119, 170), False)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '�T�{�O�_�w�����ܽd�e��(�X�{���v�ܩi)
            WaitUntil(hwnd, 179, 171, Color.FromArgb(225, 238, 119), True)
            BgdMouseLDown(hwnd, 210 + 65536 * 440) '����
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 88 + 65536 * 440)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 88 + 65536 * 440)
            ' 1-1���d����
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d1-2")
            ' 1-2���d�}�l
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 263 + 65536 * 377) '����
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 311 + 65536 * 376)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 311 + 65536 * 376)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 263 + 65536 * 377) '�A������
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 311 + 65536 * 376)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 311 + 65536 * 376)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 144 + 65536 * 504) '�ɦ�
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 32 + 65536 * 479)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 32 + 65536 * 479)
            '�T�{HP�O�_�w�^�_
            WaitUntil(hwnd, 220, 279, Color.FromArgb(255, 174, 204), False)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 149 + 65536 * 379)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 25 + 65536 * 381)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 25 + 65536 * 381)
            '1-2���d����
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d1-3")
            '�T�{���Y�N�O�_���� (�}�l�ܽd)
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '�T�{�O�_�X�{���Y�N (�ܽd����)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_���� 
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 209 + 65536 * 381)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 207 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 330 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 330 + 65536 * 499)
            ' 1-3���d����
            '�T�{�ù��U��O�_�X�{�������b�Y
            WaitUntil(hwnd, 29, 504, Color.FromArgb(221, 201, 118), True)
            SetText("�s�W�����")
            BgdMouseLClick(hwnd, 28 + 65536 * 553) ' �ﶤ��
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 253 + 65536 * 233)
            '�T�{�O�_�X�{���V�v�ܩi���b�Y
            WaitUntil(hwnd, 109, 266, Color.FromArgb(230, 222, 151), False)
            BgdMouseLClick(hwnd, 113 + 65536 * 230)
            '�T�{�O�_�X�{�u��ܡv�Ŧ���
            WaitUntil(hwnd, 214, 326, Color.FromArgb(4, 80, 117), False)
            BgdMouseLClick(hwnd, 180 + 65536 * 326)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_���� (���Ϳ�ܧ���)
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 180 + 65536 * 530) ' ��ܦa��
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 180 + 65536 * 289) '�D��
            '�T�{�O�_�X�{�u��ܡv�Ŧ���
            WaitUntil(hwnd, 205, 347, Color.FromArgb(2, 101, 150), True, 160, 202)
            BgdMouseLClick(hwnd, 179 + 65536 * 353) '���
            '�T�{�O�_�X�{�u�i�J�԰��v�Ŧ���
            WaitUntil(hwnd, 260, 484, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 302 + 65536 * 498) '�i�J�԰�
            '�T�{�O�_�X�{���Y�N (2-1���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d2-1")
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 183 + 65536 * 165) '�I���Ǫ�
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 149 + 65536 * 318) '���w�Ǫ�����
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 145 + 65536 * 442)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 29 + 65536 * 442)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 29 + 65536 * 442)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 29 + 65536 * 437) '�������
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 329 + 65536 * 437)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 329 + 65536 * 437)
            ' 2-1���d����
            '�T�{�O�_�X�{���Y�N (2-2���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d2-2")
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 311 + 65536 * 320)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 207 + 65536 * 323)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 207 + 65536 * 323)
            ' 2-2���d����
            '�T�{�O�_�X�{���Y�N (2-3���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d2-3")
            '�T�{���Y�N�O�_����
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
            ' 2-3���d����
            '�T�{�O�_�X�{��ͥӽЪ��u�����v���
            WaitUntil(hwnd, 270, 389, Color.FromArgb(5, 100, 147), True)
            BgdMouseLClick(hwnd, 247 + 65536 * 392) '����
            '�T�{�O�_�X�{���Y�N (�j�ƥd������)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '�T�{�O�_�X�{���V�I�]���b�Y
            WaitUntil(hwnd, 93, 511, Color.FromArgb(228, 227, 160), False)
            SetText("�j�ƥd��")
            BgdMouseLClick(hwnd, 89 + 65536 * 556) '�I�]
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 46 + 65536 * 229) '��ܾH��
            '�T�{�O�_�X�{�u�j�ƦX���v���
            WaitUntil(hwnd, 227, 322, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 177 + 65536 * 326)
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 40 + 65536 * 229) '�������1
            '�T�{�O�_�X�{���V�������b�Y
            WaitUntil(hwnd, 110, 240, Color.FromArgb(221, 220, 153), False)
            BgdMouseLClick(hwnd, 110 + 65536 * 200) '��ܯ���
            '�T�{�O�_�X�{�u��ܡv���
            WaitUntil(hwnd, 230, 320, Color.FromArgb(2, 84, 125), False)
            BgdMouseLClick(hwnd, 183 + 65536 * 324) '���
            '�T�{�O�_�X�{�u�T�w�v���
            WaitUntil(hwnd, 249, 487, Color.FromArgb(9, 106, 149), False)
            BgdMouseLClick(hwnd, 298 + 65536 * 497) '�T�w
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 167 + 65536 * 530) '��ܦa��
            Thread.Sleep(1000 * speed)
            BgdMouseLClick(hwnd, 167 + 65536 * 530) '��ܦa��(�קK�S�I��)
            '�T�{�O�_�X�{�u��ܡv�Ŧ���
            WaitUntil(hwnd, 205, 347, Color.FromArgb(2, 101, 150), True, 160, 202)
            BgdMouseLClick(hwnd, 179 + 65536 * 353) '���
            '�T�{�O�_�X�{�u�i�J�԰��v�Ŧ���
            WaitUntil(hwnd, 260, 484, Color.FromArgb(2, 101, 150), False)
            BgdMouseLClick(hwnd, 302 + 65536 * 498) '�i�J�԰�
            '�T�{�O�_�X�{���Y�N (3-1���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d3-1")
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLDown(hwnd, 87 + 65536 * 318)
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 91 + 65536 * 499)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 91 + 65536 * 499)
            ' 3-1���d����
            '�T�{�O�_�X�{���Y�N (3-2���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d3-2")
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 23 + 65536 * 246) '�D���ޯ�
            '�T�{�O�_�X�{�u�T�{�����v�B�\��ͩR��
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 108 + 65536 * 335) '�T�w
            ' 3-2���d����
            '�T�{�O�_�X�{���Y�N (3-3���d�}�l)
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            SetText("���d3-3")
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            BgdMouseLClick(hwnd, 84 + 65536 * 243) '�ޯ�1
            '�T�{�O�_�X�{�u�T�{�����v�B�\��ͩR��
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '�T�w
            Thread.Sleep(4450 * speed)
            BgdMouseLClick(hwnd, 145 + 65536 * 241) '�ޯ�2
            '�T�{�O�_�X�{�u�T�{�����v�B�\��ͩR��
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '�T�w
            Thread.Sleep(4450 * speed)
            BgdMouseLClick(hwnd, 215 + 65536 * 247) '�ޯ�3
            '�T�{�O�_�X�{�u�T�{�����v�B�\��ͩR��
            WaitUntil(hwnd, 222, 279, Color.FromArgb(31, 22, 23), False)
            BgdMouseLClick(hwnd, 107 + 65536 * 337) '�T�w
            Thread.Sleep(4450 * speed)
            BgdMouseLDown(hwnd, 328 + 65536 * 503) '����
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
            ' 3-3���d����
            '�T�{�O�_�X�{��ͥӽЪ��u�����v���
            WaitUntil(hwnd, 270, 389, Color.FromArgb(5, 100, 147), True)
            BgdMouseLClick(hwnd, 247 + 65536 * 392) '����
            '�T�{�O�_�X�{���Y�N
            WaitUntil(hwnd, 78, 504, Color.FromArgb(183, 142, 79), False)
            '�T�{���Y�N�O�_����
            WaitUntilNot(hwnd, 78, 504, Color.FromArgb(183, 142, 79), True)
            '�T�{�O�_�X�{���V�I�]���b�Y
            WaitUntil(hwnd, 266, 511, Color.FromArgb(249, 249, 169), False)
            SetText("��d")
            BgdMouseLClick(hwnd, 263 + 65536 * 561) '�ө�
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 173 + 65536 * 284) '�ʦL�d
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 173 + 65536 * 284) '�]�k��
            Thread.Sleep(1500 * speed)
            BgdMouseLClick(hwnd, 184 + 65536 * 381) '�T�w
            '�T�{�O�_�X�{��d�����
            WaitUntil(hwnd, 166, 88, Color.FromArgb(230, 226, 220), False)
            BgdMouseLDown(hwnd, 168 + 65536 * 126) '��d
            Thread.Sleep(300 * speed)
            BgdMouseMove(hwnd, 175 + 65536 * 502)
            Thread.Sleep(300 * speed)
            BgdMouseLUp(hwnd, 175 + 65536 * 502)
            Thread.Sleep(15000 * speed)
            successPick = successPick + 1 '���\����+1
            '�P�_�O�_���P
            GetWindowInfo(hwnd) '���o������T�A�H�K���
            hDC = GetDC(0) '���o�ୱhDC
            colorTemp = ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + 143, Client_y0 + 91))
            '����RGB = 102, 77, 28
            '�HColorMatch��ƧP�w�ۦ��סARGB�~�t�b15%�������ۦP�C��
            If ColorMatch(colorTemp, Color.FromArgb(102, 77, 28), 0.85) Then
                SetText("���P�H�U�A�����M��")
                backup = False '�S���P
            Else
                SetText("���P�H�W�A�}�l�ƥ�")
                PtCard = PtCard + 1 '�ժ��d����+1
                backup = True '���P�H�W
                '�s��
                GetWindowInfo(hwnd)
                Dim Screenshot As New Bitmap(360, 640, PixelFormat.Format32bppArgb)
                Dim Graph As Graphics = Graphics.FromImage(Screenshot)
                Graph.CopyFromScreen(Client_x0, Client_y0, 0, 0, New Size(360, 640), CopyPixelOperation.SourceCopy)
                Screenshot.Save(savePath & Format(num, "00000") & ".png")
                Thread.Sleep(300 * speed)
            End If
            RecordFlash() '��s����
            ReleaseDC(0, hDC) '����ୱhDC
            Thread.Sleep(300 * speed)
            If backup = True Then '�ƥ�����
                If chkRoot.Checked = True Then '��ROOT�v���A��������ADB���O
                    SetText("�HADB�ƥ�")
                    '�إ߳ƥ��θ�Ƨ�
                    pInfo.Arguments = "-s emulator-5554 shell mkdir /sdcard/ToSBackup"
                    ps.StartInfo = pInfo '�]�w�Ұʸ�T
                    ps.Start() '�إ߸�Ƨ�
                    ps.WaitForExit()
                    '�H���ت�tar�覡�ƥ������Y��gzip�A�ư��t�Τ��lib���i��ƥ�
                    pInfo.Arguments = "-s emulator-5554 shell su -c ""tar cvpzf - data/data/com.madhead.tos.zh --exclude=lib > /sdcard/ToSBackup/" & Format(num, "00000") & ".tar.gz"""
                    ps.StartInfo = pInfo '�]�w�Ұʸ�T
                    ps.Start() '�ƥ�
                    ps.WaitForExit() '���ݳƥ�����
                    If chkSync.Checked = True Then
                        pInfo.Arguments = "-s emulator-5554 pull /sdcard/ToSBackup/ " & syncPath
                        ps.StartInfo = pInfo '�]�w�Ұʸ�T
                        ps.Start() '�ƥ��쥻����Ƨ�
                        ps.WaitForExit()
                    End If
                    num = num + 1 '�ƥ��s��+1
                Else '�H��������ʧ@����MyBackupPro�ƥ�
                    SetText("�HMyBackupPro�ƥ�")
                    '�HBlueStacks��HD-RunApp.exe�t�X�ѼƱҰ�MyBackup Pro
                    Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.rerware.android.MyBackupPro com.rerware.android.MyBackupPro.MyBackup", AppWinStyle.NormalFocus, True, -1)
                    '�T�{�O�_�w�gŪ������(�H��m����)
                    'WaitUntil(hwnd, 65, 318, Color.FromArgb(172, 225, 250), False)
                    '����Ū������
                    Thread.Sleep(5000 * speed)
                    Thread.Sleep(300 * speed)
                    BgdMouseLClick(hwnd, 63 + 65536 * 96) '�I���ƥ�
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 75 + 65536 * 321) '�ƥ���SD�d
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 75 + 65536 * 321) '�ƥ��{��
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 29 + 65536 * 228) '�I�X�U�Կ��
                    Thread.Sleep(1500 * speed)
                    For I = 0 To 10 '�V�U�԰ʿ��
                        BgdMouseLDown(hwnd, 218 + 65536 * 389)
                        Thread.Sleep(20 * 1.5 * speed)
                        BgdMouseMove(hwnd, 218 + 65536 * 339)
                        Thread.Sleep(20 * 1.5 * speed)
                        BgdMouseLClick(hwnd, 218 + 65536 * 64)
                    Next
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 33 + 65536 * 147) '�I�ﯫ�]
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 100 + 65536 * 589) '�T�{
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 314 + 65536 * 330) '�I��H�s���r
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 300 + 65536 * 207) '�����r����
                    Thread.Sleep(1500 * speed)
                    For I = 0 To 30 '�M���ثe�Ҧ���r
                        BgdMyKeyPress(hwnd, System.Windows.Forms.Keys.Back)
                        Thread.Sleep(50 * speed)
                    Next
                    '��J�y����
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
                    BgdMouseLClick(hwnd, 110 + 65536 * 272) '�T�{
                    Thread.Sleep(1500 * speed)
                    BgdMouseLClick(hwnd, 179 + 65536 * 390) '�ƾ�
                    Thread.Sleep(4500 * speed)
                    BgdMouseLClick(hwnd, 183 + 65536 * 410) '�T�{
                    Thread.Sleep(1500 * speed)
                    num = num + 1
                    If chkSync.Checked = True Then
                        SetText("�P�B���")
                        pInfo.Arguments = "-s emulator-5554 pull /sdcard/rerware/MyBackup/AllAppsBackups " & syncPath
                        ps.StartInfo = pInfo '�]�w�Ұʸ�T
                        ps.Start() '�ƥ��쥻����Ƨ�
                        ps.WaitForExit()
                    End If
                End If
            End If
            SetText("�M�����")
            '���椺�ت�Android Debug Bridge(HD-adb.exe)�H�M���{�����
            '���B�Q��shell����pm clear <PACKAGE> �ӲM�����w�{�����
            '�Բӻ����Ԩ��Jhttp://developer.android.com/tools/help/adb.html
            pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '�]�w�ѼƬ��u�M�����]�����ơv
            ps.StartInfo = pInfo '�]�w�Ұʸ�T
            tried = 0 '���զ����k�s
            Do
                ps.Start() '�Ұ�
                tried = tried + 1
            Loop Until (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Or tried > 10
            SetText("����BlueStacks")
            '�HHD-Quit�����{��(���ؤ�k)
            Shell(Environ("ProgramFiles") & "\BlueStacks\HD-Quit.exe", AppWinStyle.NormalFocus, True, -1)
            SetText("���թٰ��q���ϰ�ϥ�")
            '�ٰ��q���ϰ�ϥ�
            hWndShell = FindWindow("Shell_TrayWnd", "") '���o�t�ιϥܰ�hwnd
            '���o�q���ϰ�hwnd
            If Environment.OSVersion.Version.Major = 5 AndAlso Environment.OSVersion.Version.Minor = 0 Then
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndToolBar = FindWindowEx(hWndTray, 0, "ToolbarWindow32", "")
            Else
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndPager = FindWindowEx(hWndTray, 0, "SysPager", "")
                hWndToolBar = FindWindowEx(hWndPager, 0, "ToolbarWindow32", "�ϥΪ̤ɯŪ��q���ϰ�")
            End If
            '���o���
            GetWindowInfo(hWndToolBar)
            Dim ibx, iby As Integer
            iby = CInt(ClientY / 2) * 65536
            '�HWM_MOUSEMOVE���h�L�Ĺϥ�
            For ibx = 1 To ClientX Step 1
                PostMessage(hWndToolBar, WM_MOUSEMOVE, 0, iby + ibx)
            Next
            SetText("��s�����ɶ�")
            '���������ϥήɶ��íp�⥭����
            timeAvg.timeUsed = (timeAvg.timeUsed * timeAvg.recordCount + timeLoop) / (timeAvg.recordCount + 1) '�p�⥭����
            timeAvg.recordCount = timeAvg.recordCount + 1 '�w�����`������+1
            ReflashTime() '��s�ɶ�
        Loop Until (num > 99999) '�����y����999����
        '����
        Beep()
        SetState() '�^�_��l���A
    End Sub

    Private Sub SetText(ByVal [text] As String) '�]�w���A��r
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '���p�����ID���P�A�ϥ�Callback��k�ާ@���
        If Me.lblStatus.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.lblStatus.Text = [text]
        End If
    End Sub

    Private Sub RecordFlash() '�^�_��l���A
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '���p�����ID���P�A�ϥ�Callback��k�ާ@���
        If Me.btnStart.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf RecordFlash)
            Me.Invoke(d)
        Else
            Me.lblSuccess.Text = successPick & "��"
            Me.lblPt.Text = PtCard & "��"
        End If
    End Sub

    Private Sub SetState() '�^�_��l���A
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '���p�����ID���P�A�ϥ�Callback��k�ާ@���
        If Me.btnStart.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf SetState)
            Me.Invoke(d)
        Else
            TimerAbort.Enabled = False '����p�ɾ�
            btnStart.Text = "�}�l" '�]�w���s��r
            lblStatus.Text = "Idle"
            btnStart.ForeColor = Color.Black '�]�w���s��r�C��
            txtNum.Text = num.ToString '��s�_�l�s��
        End If
    End Sub

    Private Sub ReflashTime()
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        '���p�����ID���P�A�ϥ�Callback��k�ާ@���
        If Me.lblTimeAvg.InvokeRequired Then
            Dim d As New FunctionCallback(AddressOf ReflashTime)
            Me.Invoke(d)
        Else
            lblTimeAvg.Text = timeAvg.timeUsed & "��/��"
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
        '�N�ܼƼg�J�]�w��
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
                Dim SettingFields As String() = Settingreader.ReadFields() 'Ū���]�w��
                Dim count As Integer = 0 '�P�_���
                '�`��Ū���]�w�ɡA�H,�P�_���
                For Each currentField As String In SettingFields
                    Select Case count
                        Case 0 '�t��
                            speed = currentField
                            txtSpeed.Text = Format(speed, ".00")
                        Case 1 '�_�l�s��
                            num = currentField
                            txtNum.Text = num
                        Case 2 '�̤j�`���ɶ�
                            Select Case currentField / 60 '�N���ഫ����
                                Case 10
                                    rad10.Checked = True
                                Case 15
                                    rad15.Checked = True
                                Case 20
                                    rad20.Checked = True
                            End Select
                        Case 3 '�x�s�ؿ�
                            savePath = currentField
                            txtPath.Text = savePath
                        Case 4 '�O�_�۰ʳs�u
                            If currentField = "True" Then
                                chkRas.Checked = True
                            Else
                                chkRas.Checked = False
                            End If
                        Case 5 '�s�u�W��
                            txtRasName.Text = currentField
                        Case 6 '�s�u�b��
                            txtRasAcc.Text = currentField
                        Case 7 '�s�u�K�X
                            txtRasPW.Text = currentField
                        Case 8 '�����y��X
                            txtX.Text = currentField
                        Case 9 '�����y��Y
                            txtY.Text = currentField
                        Case 10 '�O�_�۰ʦP�B
                            If currentField = "True" Then
                                chkSync.Checked = True
                            Else
                                chkSync.Checked = False
                            End If
                        Case 11 '�P�B�ؿ�
                            syncPath = currentField
                            txtSync.Text = syncPath
                        Case 12 '�O�_�HRoot�Ҧ��ƥ�
                            If currentField = "True" Then
                                chkRoot.Checked = True
                            Else
                                chkRoot.Checked = False
                            End If
                    End Select
                    count = count + 1 '�i�J�U�@�����
                Next
            Catch ex As Exception 'Ū���ɮץX�{���~�A���s�]�w�Ѽ�
                speed = 1.0 '�]�w�ܳt�Ѽ�
                txtSpeed.Text = "1.00" '����ܳt�Ѽ�
                num = 1 '�]�w�_�l�s��
                txtNum.Text = "1" '��ܰ_�l�s��
                savePath = Application.StartupPath & "\Picture\" '�]�w�^�ϥؿ�"
                txtPath.Text = savePath '����^�ϥؿ�
                syncPath = Application.StartupPath & "\Sync\" '�]�w�P�B�ؿ�"
                txtSync.Text = syncPath '��ܦP�B�ؿ�
                maxLoop = 900 '�w�]�̤j�`���ɶ���15��
                rad15.Checked = True '��̤ܳj�`���ɶ�
                txtX.Text = 23 '�w�]XY�y��
                txtY.Text = 45
            End Try
        Else 'Ū�������ɮסA���s�]�w�Ѽ�
            speed = 1.0 '�]�w�ܳt�Ѽ�
            txtSpeed.Text = "1.00" '����ܳt�Ѽ�
            num = 1 '�]�w�_�l�s��
            txtNum.Text = "1" '��ܰ_�l�s��
            savePath = Application.StartupPath & "\Picture\" '�]�w�^�ϥؿ�
            txtPath.Text = savePath '����^�ϥؿ�
            syncPath = Application.StartupPath & "\Sync\" '�]�w�P�B�ؿ�"
            txtSync.Text = syncPath '��ܦP�B�ؿ�
            maxLoop = 900 '�w�]�̤j�`���ɶ���15��
            rad15.Checked = True '��̤ܳj�`���ɶ�
            txtX.Text = 23 '�w�]XY�y��
            txtY.Text = 45
        End If
        '�]�w�Ұ�Android Debug Bridge��process�Ѽ�
        pInfo.FileName = Environ("ProgramFiles") & "\BlueStacks\HD-Adb.exe"
        pInfo.UseShellExecute = False
        pInfo.RedirectStandardInput = False
        pInfo.RedirectStandardOutput = True
        pInfo.CreateNoWindow = True
    End Sub

    Private Sub txtNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNum.KeyPress
        '�T���J�D�Ʀr
        If Asc(e.KeyChar) > 57 Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) < 48 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        If btnStart.Text = "�}�l" Then '���U�ɡA���pthread���ҰʡA�Ұ�thread
            btnStart.Text = "����..." '�]�w���s��r
            btnStart.ForeColor = Color.Red '�]�w���s��r�C��
            If txtNum.Text <> "" Then '�]�w�_�l�s��
                num = Integer.Parse(txtNum.Text) '�N��r�ର���
            Else
                num = 1 '�w�]�_�l�s��1
            End If
            Work = New Thread(AddressOf Me.Dowork) '�إ߷sthread
            Work.IsBackground = True
            TimerAbort.Enabled = True '�p�ɾ��}��
            Work.Start() '�}�lthread
        ElseIf btnStart.Text = "����..." Then '���U�ɡA���pthread���椤�A�פ�thread
            Work.Abort() '����thread
            SetState() '�^�_��l���A
        End If
    End Sub

    '������C��O�_�ۦ��Asimilar�ѼƥN��ۦ���(0.1-1.0)
    Private Function ColorMatch(sample As Color, standard As Color, similar As Single) As Boolean
        '�H��t�覡�p��ۦ���
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
        timeLoop = timeLoop + 1 '�C��+1
        lblTime.Text = timeLoop & "��g�L" '��ܸg�L�ɶ�
        '�Y�d�b�إ߷s�b�����A�A�h�W�L�`���ɶ����������@�j��}
        If timeLoop > maxLoop Or (induction = True And timeLoop > (maxLoop / 5)) Then '�j��]�w�ɶ��j����{��
            errorCount = errorCount + 1 '�O�ɦ���+1
            lblError.Text = errorCount & "��" '��ܹO�ɦ���
            SetText("�W�L�`���ɶ��A�j���")
            '�N�ɶ��k�s
            timeLoop = 0 '������p�ɾ���]���J���L�{�]���i��stuck�A�Ystuck�W�L�]�w�ɶ��]�|�۰ʭ���
            '�j����u�@ 
            Work.Abort()
            KillProcess() '�T�O�{���w��������(�����B�z�{��)
            '�M������
            SetText("�M�����")
            '�HBlueStacks��HD-RunApp.exe�t�X�ѼƱҰʯ��]����
            Shell("""" & Environ("ProgramFiles") & "\BlueStacks\HD-RunApp.exe"" Android com.madhead.tos.zh com.unity3d.player.UnityPlayerActivity", AppWinStyle.NormalFocus, True, -1)
            '���椺�ت�Android Debug Bridge(HD-adb.exe)�H�M���{�����
            '���B�Q��shell����pm clear <PACKAGE> �ӲM�����w�{�����
            '�Բӻ����Ԩ��Jhttp://developer.android.com/tools/help/adb.html
            pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '�]�w�ѼƬ��u�M�����]�����ơv
            ps.StartInfo = pInfo '�]�w�Ұʸ�T
            tried = 0 '���զ����k�s
            Do
                ps.Start() '�Ұ�
                tried = tried + 1
            Loop Until (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Or tried > 10
            '�HHD-Quit�����{��(���ؤ�k)
            SetText("����BlueStacks")
            Shell(Environ("ProgramFiles") & "\BlueStacks\HD-Quit.exe", AppWinStyle.NormalFocus, True, -1)
            '�ٰ��q���ϰ�ϥ�
            SetText("���թٰ��q���ϰ�ϥ�")
            hWndShell = FindWindow("Shell_TrayWnd", "") '���o�t�ιϥܰ�hwnd
            '���o�q���ϰ�hwnd
            If Environment.OSVersion.Version.Major = 5 AndAlso Environment.OSVersion.Version.Minor = 0 Then
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndToolBar = FindWindowEx(hWndTray, 0, "ToolbarWindow32", "")
            Else
                hWndTray = FindWindowEx(hWndShell, 0, "TrayNotifyWnd", "")
                hWndPager = FindWindowEx(hWndTray, 0, "SysPager", "")
                hWndToolBar = FindWindowEx(hWndPager, 0, "ToolbarWindow32", "�ϥΪ̤ɯŪ��q���ϰ�")
            End If
            '���o���
            GetWindowInfo(hWndToolBar)
            Dim ibx, iby As Integer
            iby = CInt(ClientY / 2) * 65536
            '�HWM_MOUSEMOVE���h�L�Ĺϥ�
            For ibx = 1 To ClientX Step 1
                PostMessage(hWndToolBar, WM_MOUSEMOVE, 0, iby + ibx)
            Next
            SetText("���s�}�l")
            '���s�}�l
            Work = New Threading.Thread(AddressOf Me.Dowork)
            Work.IsBackground = True
            Work.Start()
        End If
    End Sub

    '���ƴ`������targetColor�b(x, y)�X�{
    'click�N��O�_�n�I���ù��A�w�]�I���y��(180, 280)
    '�w�]���ǽT��0.85
    '�Ҧ��y�ХH�ӵ������ǡA�Y���W����(0, 0)
    Private Sub WaitUntil(hwnd As Integer, x As Integer, y As Integer, targetColor As Color, click As Boolean, Optional clickX As Integer = 180, Optional clickY As Integer = 280)
        hDC = GetDC(0) '���o�ୱhDC�H����
        Do
            If click = True Then
                BgdMouseLClick(hwnd, clickX + 65536 * clickY)
                Thread.Sleep(50)
            End If
            GetWindowInfo(hwnd) '���o������T�A�H�K���
        Loop Until ColorMatch(ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + x, Client_y0 + y)), targetColor, 0.9) = True
        ReleaseDC(0, hDC) '����ୱhDC
        Thread.Sleep(1000 * speed)
    End Sub

    '���ƴ`������targetColor���b(x, y)�X�{
    'click�N��O�_�n�I���ù��A�w�]�I���y��(180, 280)
    '�w�]���ǽT��0.85
    '�Ҧ��y�ХH�ӵ������ǡA�Y���W����(0, 0)
    Private Sub WaitUntilNot(hwnd As Integer, x As Integer, y As Integer, targetColor As Color, click As Boolean, Optional clickX As Integer = 180, Optional clickY As Integer = 280)
        hDC = GetDC(0) '���o�ୱhDC�H����
        Do
            If click = True Then
                BgdMouseLClick(hwnd, clickX + 65536 * clickY)
                Thread.Sleep(50)
            End If
            GetWindowInfo(hwnd) '���o������T�A�H�K���
        Loop Until ColorMatch(ColorTranslator.FromWin32(GetPixel(hDC, Client_x0 + x, Client_y0 + y)), targetColor, 0.9) = False
        ReleaseDC(0, hDC) '����ୱhDC
        Thread.Sleep(1000 * speed)
    End Sub

    Private Sub btnSetPath_Click(sender As System.Object, e As System.EventArgs) Handles btnSetPath.Click
        Dim pathDialog As New FolderBrowserDialog()
        Try
            pathDialog.RootFolder = Environment.SpecialFolder.MyComputer
            If pathDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                savePath = pathDialog.SelectedPath & "\" '�O�o�[�W\
                txtPath.Text = savePath
            End If
        Catch ex As Exception
            MsgBox("�г]�w�ؿ�")
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
        '�T���J�D�Ʀr
        If Asc(e.KeyChar) > 57 Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) < 48 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtY_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtY.KeyPress
        '�T���J�D�Ʀr
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
            MsgBox("�ж}��BlueStacks", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '�Ѱ������m��
        SetWindowPos(FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)"), -2, 0, 0, 0, 0, SWP_NOSIZE Or SWP_NOMOVE) '���������m��
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        If FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)") = 0 Then
            MsgBox("�ж}��BlueStacks�A�M�����", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If MsgBox("�O�_�T�w�M����ơA���ʧ@�N�L�k�٭�", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        '���椺�ت�Android Debug Bridge(HD-adb.exe)�H�M���{�����
        '���B�Q��shell����pm clear <PACKAGE> �ӲM�����w�{�����
        '�Բӻ����Ԩ��Jhttp://developer.android.com/tools/help/adb.html
        pInfo.Arguments = "-s emulator-5554 shell pm clear com.madhead.tos.zh" '�]�w�ѼƬ��u�M�����]�����ơv
        ps.StartInfo = pInfo '�]�w�Ұʸ�T
        ps.Start() '�Ұ�
        If (InStr(ps.StandardOutput.ReadLine, "Success") <> 0) Then
            MsgBox("���\�M����ơI")
        Else
            MsgBox("�M����ƥ��ѡI")
        End If
    End Sub

    Private Sub btnLock_Click(sender As System.Object, e As System.EventArgs) Handles btnLock.Click
        If FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)") = 0 Then
            MsgBox("�ж}��BlueStacks", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        hwnd = FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player for Windows (beta-1)")
        SetWindowPos(hwnd, -1, txtX.Text, txtY.Text, 0, 0, SWP_NOSIZE) '�N�����û��m��̤W�h�H�K���
        SetForegroundWindow(hwnd) '�ϵ�����o�J�I
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
            MsgBox("�г]�w�ؿ�")
        End Try
    End Sub

    Private Sub chkRoot_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRoot.CheckedChanged
        If chkRoot.Checked = True Then
            MsgBox("�нT�wBluestacks��Root�v���A�_�h�N�|�ƥ�����")
        End If
    End Sub

    Private Sub KillProcess() '�j�������Ҧ�BlueStacks�����B�z�{��
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