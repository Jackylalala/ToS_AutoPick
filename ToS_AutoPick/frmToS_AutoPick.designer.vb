<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmToS_AutoPick
#Region "Windows Form 設計工具產生的程式碼 "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        '此為 Windows Form 設計工具所需的呼叫。
        InitializeComponent()
    End Sub
    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer
    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToS_AutoPick))
        Me.btnMiuns = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtSpeed = New System.Windows.Forms.TextBox()
        Me.txtNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TimerAbort = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRasPW = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRasAcc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRasName = New System.Windows.Forms.TextBox()
        Me.chkRas = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSetPath = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rad20 = New System.Windows.Forms.RadioButton()
        Me.rad15 = New System.Windows.Forms.RadioButton()
        Me.rad10 = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtY = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtX = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTimeAvg = New System.Windows.Forms.Label()
        Me.btnRelease = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.btnLock = New System.Windows.Forms.Button()
        Me.chkSync = New System.Windows.Forms.CheckBox()
        Me.lblError = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblSuccess = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPt = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkRoot = New System.Windows.Forms.CheckBox()
        Me.txtSync = New System.Windows.Forms.TextBox()
        Me.btnSetSync = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnMiuns
        '
        Me.btnMiuns.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnMiuns.ForeColor = System.Drawing.Color.Black
        Me.btnMiuns.Location = New System.Drawing.Point(175, 90)
        Me.btnMiuns.Name = "btnMiuns"
        Me.btnMiuns.Size = New System.Drawing.Size(36, 23)
        Me.btnMiuns.TabIndex = 32
        Me.btnMiuns.Text = "-"
        Me.btnMiuns.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("微軟正黑體", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.Black
        Me.btnAdd.Location = New System.Drawing.Point(137, 90)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(36, 23)
        Me.btnAdd.TabIndex = 31
        Me.btnAdd.Text = "+"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtSpeed
        '
        Me.txtSpeed.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtSpeed.Location = New System.Drawing.Point(137, 57)
        Me.txtSpeed.MaxLength = 3
        Me.txtSpeed.Name = "txtSpeed"
        Me.txtSpeed.ReadOnly = True
        Me.txtSpeed.Size = New System.Drawing.Size(72, 29)
        Me.txtSpeed.TabIndex = 30
        Me.txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNum
        '
        Me.txtNum.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtNum.Location = New System.Drawing.Point(105, 121)
        Me.txtNum.MaxLength = 5
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(104, 29)
        Me.txtNum.TabIndex = 34
        Me.txtNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(10, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "起始編號："
        '
        'TimerAbort
        '
        Me.TimerAbort.Interval = 1000
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtRasPW)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRasAcc)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtRasName)
        Me.GroupBox1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(14, 218)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(197, 147)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "自動連線設定"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(7, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(57, 20)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "密碼："
        '
        'txtRasPW
        '
        Me.txtRasPW.Location = New System.Drawing.Point(61, 106)
        Me.txtRasPW.Name = "txtRasPW"
        Me.txtRasPW.Size = New System.Drawing.Size(121, 29)
        Me.txtRasPW.TabIndex = 41
        Me.txtRasPW.UseSystemPasswordChar = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(7, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 20)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "帳號："
        '
        'txtRasAcc
        '
        Me.txtRasAcc.Location = New System.Drawing.Point(61, 71)
        Me.txtRasAcc.Name = "txtRasAcc"
        Me.txtRasAcc.Size = New System.Drawing.Size(121, 29)
        Me.txtRasAcc.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(7, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(57, 20)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "名稱："
        '
        'txtRasName
        '
        Me.txtRasName.Location = New System.Drawing.Point(61, 36)
        Me.txtRasName.Name = "txtRasName"
        Me.txtRasName.Size = New System.Drawing.Size(121, 29)
        Me.txtRasName.TabIndex = 0
        '
        'chkRas
        '
        Me.chkRas.AutoSize = True
        Me.chkRas.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkRas.Location = New System.Drawing.Point(14, 156)
        Me.chkRas.Name = "chkRas"
        Me.chkRas.Size = New System.Drawing.Size(124, 24)
        Me.chkRas.TabIndex = 0
        Me.chkRas.Text = "自動重新連線"
        Me.chkRas.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(121, 20)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "延遲時間(ms)："
        '
        'btnSetPath
        '
        Me.btnSetPath.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSetPath.Location = New System.Drawing.Point(229, 171)
        Me.btnSetPath.Name = "btnSetPath"
        Me.btnSetPath.Size = New System.Drawing.Size(141, 28)
        Me.btnSetPath.TabIndex = 38
        Me.btnSetPath.Text = "設定擷圖目錄"
        Me.btnSetPath.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtPath.Location = New System.Drawing.Point(231, 230)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(137, 23)
        Me.txtPath.TabIndex = 39
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(231, 204)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(137, 20)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "↓當前擷圖目錄↓"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rad20)
        Me.GroupBox2.Controls.Add(Me.rad15)
        Me.GroupBox2.Controls.Add(Me.rad10)
        Me.GroupBox2.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(14, 371)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(197, 65)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "循環時間(分)"
        '
        'rad20
        '
        Me.rad20.AutoSize = True
        Me.rad20.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rad20.Location = New System.Drawing.Point(142, 33)
        Me.rad20.Name = "rad20"
        Me.rad20.Size = New System.Drawing.Size(40, 20)
        Me.rad20.TabIndex = 2
        Me.rad20.TabStop = True
        Me.rad20.Text = "20"
        Me.rad20.UseVisualStyleBackColor = True
        '
        'rad15
        '
        Me.rad15.AutoSize = True
        Me.rad15.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rad15.Location = New System.Drawing.Point(81, 32)
        Me.rad15.Name = "rad15"
        Me.rad15.Size = New System.Drawing.Size(40, 20)
        Me.rad15.TabIndex = 1
        Me.rad15.TabStop = True
        Me.rad15.Text = "15"
        Me.rad15.UseVisualStyleBackColor = True
        '
        'rad10
        '
        Me.rad10.AutoSize = True
        Me.rad10.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.rad10.Location = New System.Drawing.Point(20, 33)
        Me.rad10.Name = "rad10"
        Me.rad10.Size = New System.Drawing.Size(40, 20)
        Me.rad10.TabIndex = 0
        Me.rad10.TabStop = True
        Me.rad10.Text = "10"
        Me.rad10.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtY)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtX)
        Me.GroupBox3.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(229, 56)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(141, 109)
        Me.GroupBox3.TabIndex = 43
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "視窗起始座標"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(21, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(35, 20)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Y："
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(61, 71)
        Me.txtY.MaxLength = 4
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(49, 29)
        Me.txtY.TabIndex = 39
        Me.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(21, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(35, 20)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "X："
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(61, 36)
        Me.txtX.MaxLength = 4
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(49, 29)
        Me.txtX.TabIndex = 0
        Me.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnStart
        '
        Me.btnStart.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnStart.Location = New System.Drawing.Point(229, 261)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(137, 32)
        Me.btnStart.TabIndex = 36
        Me.btnStart.Text = "開始"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnAbout
        '
        Me.btnAbout.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnAbout.Location = New System.Drawing.Point(229, 299)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(137, 32)
        Me.btnAbout.TabIndex = 46
        Me.btnAbout.Text = "關於本程式"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(229, 344)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(137, 20)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "↓平均循環時間↓"
        '
        'lblTimeAvg
        '
        Me.lblTimeAvg.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeAvg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTimeAvg.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTimeAvg.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTimeAvg.ForeColor = System.Drawing.Color.Black
        Me.lblTimeAvg.Location = New System.Drawing.Point(229, 370)
        Me.lblTimeAvg.Name = "lblTimeAvg"
        Me.lblTimeAvg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTimeAvg.Size = New System.Drawing.Size(137, 20)
        Me.lblTimeAvg.TabIndex = 48
        Me.lblTimeAvg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRelease
        '
        Me.btnRelease.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnRelease.Location = New System.Drawing.Point(385, 110)
        Me.btnRelease.Name = "btnRelease"
        Me.btnRelease.Size = New System.Drawing.Size(137, 32)
        Me.btnRelease.TabIndex = 49
        Me.btnRelease.Text = "解除BlueStacks置頂"
        Me.btnRelease.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(14, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(197, 29)
        Me.lblStatus.TabIndex = 50
        Me.lblStatus.Text = "Idle"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnClear.Location = New System.Drawing.Point(229, 404)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(137, 32)
        Me.btnClear.TabIndex = 51
        Me.btnClear.Text = "清除神魔之塔資料"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lblTime
        '
        Me.lblTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTime.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Green
        Me.lblTime.Location = New System.Drawing.Point(217, 8)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTime.Size = New System.Drawing.Size(153, 29)
        Me.lblTime.TabIndex = 52
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnLock
        '
        Me.btnLock.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnLock.Location = New System.Drawing.Point(385, 148)
        Me.btnLock.Name = "btnLock"
        Me.btnLock.Size = New System.Drawing.Size(137, 32)
        Me.btnLock.TabIndex = 53
        Me.btnLock.Text = "BlueStacks強制置頂"
        Me.btnLock.UseVisualStyleBackColor = True
        '
        'chkSync
        '
        Me.chkSync.AutoSize = True
        Me.chkSync.Font = New System.Drawing.Font("微軟正黑體", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkSync.Location = New System.Drawing.Point(376, 12)
        Me.chkSync.Name = "chkSync"
        Me.chkSync.Size = New System.Drawing.Size(157, 21)
        Me.chkSync.TabIndex = 55
        Me.chkSync.Text = "備份同步到本機資料夾"
        Me.chkSync.UseVisualStyleBackColor = True
        '
        'lblError
        '
        Me.lblError.BackColor = System.Drawing.Color.Transparent
        Me.lblError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblError.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblError.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblError.ForeColor = System.Drawing.Color.Black
        Me.lblError.Location = New System.Drawing.Point(385, 217)
        Me.lblError.Name = "lblError"
        Me.lblError.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblError.Size = New System.Drawing.Size(137, 20)
        Me.lblError.TabIndex = 57
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(385, 191)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(137, 20)
        Me.Label12.TabIndex = 56
        Me.Label12.Text = "↓本回逾時次數↓"
        '
        'lblSuccess
        '
        Me.lblSuccess.BackColor = System.Drawing.Color.Transparent
        Me.lblSuccess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSuccess.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuccess.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblSuccess.ForeColor = System.Drawing.Color.Black
        Me.lblSuccess.Location = New System.Drawing.Point(385, 272)
        Me.lblSuccess.Name = "lblSuccess"
        Me.lblSuccess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuccess.Size = New System.Drawing.Size(137, 20)
        Me.lblSuccess.TabIndex = 59
        Me.lblSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(385, 246)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(137, 20)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "↓本回抽卡次數↓"
        '
        'lblPt
        '
        Me.lblPt.BackColor = System.Drawing.Color.Transparent
        Me.lblPt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPt.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.lblPt.ForeColor = System.Drawing.Color.Black
        Me.lblPt.Location = New System.Drawing.Point(385, 331)
        Me.lblPt.Name = "lblPt"
        Me.lblPt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPt.Size = New System.Drawing.Size(137, 20)
        Me.lblPt.TabIndex = 61
        Me.lblPt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(385, 305)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(137, 20)
        Me.Label15.TabIndex = 60
        Me.Label15.Text = "↓本回白金次數↓"
        '
        'chkRoot
        '
        Me.chkRoot.AutoSize = True
        Me.chkRoot.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.chkRoot.Location = New System.Drawing.Point(14, 186)
        Me.chkRoot.Name = "chkRoot"
        Me.chkRoot.Size = New System.Drawing.Size(128, 24)
        Me.chkRoot.TabIndex = 62
        Me.chkRoot.Text = "Root備份模式"
        Me.chkRoot.UseVisualStyleBackColor = True
        '
        'txtSync
        '
        Me.txtSync.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.txtSync.Location = New System.Drawing.Point(385, 77)
        Me.txtSync.Name = "txtSync"
        Me.txtSync.ReadOnly = True
        Me.txtSync.Size = New System.Drawing.Size(137, 23)
        Me.txtSync.TabIndex = 54
        '
        'btnSetSync
        '
        Me.btnSetSync.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.btnSetSync.Location = New System.Drawing.Point(385, 41)
        Me.btnSetSync.Name = "btnSetSync"
        Me.btnSetSync.Size = New System.Drawing.Size(137, 28)
        Me.btnSetSync.TabIndex = 56
        Me.btnSetSync.Text = "設定本機資料夾"
        Me.btnSetSync.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(425, 381)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 42)
        Me.Button1.TabIndex = 63
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmToS_AutoPick
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(541, 445)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSetSync)
        Me.Controls.Add(Me.chkRoot)
        Me.Controls.Add(Me.txtSync)
        Me.Controls.Add(Me.lblPt)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lblSuccess)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.chkSync)
        Me.Controls.Add(Me.btnLock)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnRelease)
        Me.Controls.Add(Me.lblTimeAvg)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.btnSetPath)
        Me.Controls.Add(Me.chkRas)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNum)
        Me.Controls.Add(Me.btnMiuns)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtSpeed)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.Name = "frmToS_AutoPick"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "神魔之塔全自動抽卡"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnMiuns As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtSpeed As System.Windows.Forms.TextBox
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TimerAbort As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkRas As System.Windows.Forms.CheckBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtRasPW As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRasAcc As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRasName As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSetPath As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rad20 As System.Windows.Forms.RadioButton
    Friend WithEvents rad15 As System.Windows.Forms.RadioButton
    Friend WithEvents rad10 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtY As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblTimeAvg As System.Windows.Forms.Label
    Friend WithEvents btnRelease As System.Windows.Forms.Button
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Public WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents btnLock As System.Windows.Forms.Button
    Friend WithEvents chkSync As System.Windows.Forms.CheckBox
    Public WithEvents lblError As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblSuccess As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPt As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkRoot As System.Windows.Forms.CheckBox
    Friend WithEvents txtSync As System.Windows.Forms.TextBox
    Friend WithEvents btnSetSync As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
#End Region
End Class