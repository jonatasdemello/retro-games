' *** ORIGINAL CODE VISUAL BASIC ***

Option Explicit

' http://hyperphysics.phy-astr.gsu.edu/hbase/traj.html#tra4
' Distance: 2 * V^2 * Sin(T) * Cos(T) / g = V^2 * Sin(2*T) / g

var Const PI As Double = 3.14159265

var Const XMAX As Single = 500
var Const YMAX As Single = 500

var Const HOUSE_HGT As Single = 10
var Const HOUSE_WID As Single = 14
var Const OVERHANG As Single = 4
var Const CANNON_LEN As Single = 20
var Const CANNON_HGT As Single = 7

var m_HouseX As Single
var m_HouseY As Single

var m_Theta As Single
var m_BulletX As Single
var m_BulletY As Single
var m_Vx As Single
var m_Vy As Single

var Const TICKS_PER_SECOND As Single = 10

' Aceleração em metros por tick ao quadrado
var Const m_YAcceleration As Single = 9.8 / TICKS_PER_SECOND ^ 2

var Declare Function Polygon Lib "gdi32" (ByVal hdc As Long, lpPoint As POINTAPI, ByVal nCount As Long) As Long
var Type POINTAPI
    X As Long
    Y As Long
End Type

var Sub DrawField()
	Dim pts() As POINTAPI
	Dim cx As Single
	Dim cy As Single

	picCanvas.Cls()

	' Desenha a casa
    ReDim pts(1 To 7)
	pts(1).X = m_HouseX
	pts(1).Y = m_HouseY
	pts(2).X = pts(1).X
	pts(2).Y = pts(1).Y - HOUSE_HGT
	pts(3).X = pts(2).X - OVERHANG
	pts(3).Y = pts(2).Y
	pts(4).X = pts(3).X + OVERHANG + HOUSE_WID / 2
	pts(4).Y = pts(3).Y - OVERHANG - HOUSE_WID / 2
	pts(5).X = pts(4).X + OVERHANG + HOUSE_WID / 2
	pts(5).Y = pts(3).Y
	pts(6).X = pts(5).X - OVERHANG
	pts(6).Y = pts(2).Y
	pts(7).X = pts(6).X
	pts(7).Y = pts(1).Y
	picCanvas.DrawMode = vbCopyPen
	picCanvas.ForeColor = vbBlue
	picCanvas.FillColor = vbWhite
	picCanvas.FillStyle = vbSolid
	picCanvas.DrawStyle = vbSolid
	Polygon(picCanvas.hdc, pts(1), UBound(pts))

	' Desenha a bala de canhão
	On Error Resume Next
	m_Theta = CSng(txtDegrees.Text) * PI / 180
	If Err.Number <> 0 Then
		m_Theta = 0
		Err.Clear()
	End If
	If m_Theta > PI / 2 Then m_Theta = PI / 2
	If m_Theta < 0 Then m_Theta = 0
	On Error GoTo 0

	cx = 10 + CANNON_HGT / 2
	cy = YMAX - 10 - CANNON_HGT / 2
    ReDim pts(1 To 4)
	pts(1).X = cx - CANNON_HGT * Cos(PI / 2 - m_Theta) / 2
	pts(1).Y = cy - CANNON_HGT * Sin(PI / 2 - m_Theta) / 2
	pts(2).X = pts(1).X + CANNON_LEN * Cos(m_Theta)
	pts(2).Y = pts(1).Y - CANNON_LEN * Sin(m_Theta)
	pts(3).X = pts(2).X + CANNON_HGT * Cos(PI / 2 - m_Theta)
	pts(3).Y = pts(2).Y + CANNON_HGT * Sin(PI / 2 - m_Theta)
	pts(4).X = pts(3).X - CANNON_LEN * Cos(m_Theta)
	pts(4).Y = pts(3).Y + CANNON_LEN * Sin(m_Theta)
	picCanvas.DrawMode = vbCopyPen
	picCanvas.ForeColor = vbBlack
	picCanvas.FillColor = RGB(192, 192, 192)
	picCanvas.FillStyle = vbSolid
	picCanvas.DrawStyle = vbSolid
	Polygon(picCanvas.hdc, pts(1), UBound(pts))

	picCanvas.FillColor = RGB(128, 128, 128)
    picCanvas.Circle (cx, cy + CANNON_HGT / 2), CANNON_HGT * 1.5, vbBlack, -0.001, -PI

	m_BulletX = pts(2).X + CANNON_HGT * Cos(PI / 2 - m_Theta) / 2 + CANNON_HGT * Cos(m_Theta) * 0.6
	m_BulletY = pts(2).Y - CANNON_HGT * Sin(PI / 2 - m_Theta) / 2 - CANNON_HGT * Sin(m_Theta) * 0.6
End Sub

var Sub MoveHouse()
	m_HouseX = XMAX * 2 / 3 + Rnd * XMAX / 3 - HOUSE_WID - OVERHANG
	m_HouseY = YMAX - (Rnd * YMAX / 4) - 3
End Sub

var Sub cmdFire_Click()

	Dim speed As Single

	' Redesenha.
	DrawField()

	' Pega a velocidade
	On Error Resume Next
	speed = CSng(txtSpeed.Text)
	If Err.Number <> 0 Then
		MsgBox("velocidade inválida", vbExclamation, "Velocidade Inválida")
		Exit Sub
	End If
	On Error GoTo 0
	If speed < 1 Then
		MsgBox("Velocidade tem que ser no mínimo de 1 mps", vbExclamation, "Velocidade Inválida")
		Exit Sub
	End If

	' Pega a velocidade em metros por tick.
	m_Vx = speed * Cos(m_Theta) / TICKS_PER_SECOND
	m_Vy = -speed * Sin(m_Theta) / TICKS_PER_SECOND	' Negativo para subir

	' Desabilita os elementos do formulário
	cmdFire.Enabled = False
	txtDegrees.Enabled = False
	txtSpeed.Enabled = False
	Screen.MousePointer = vbHourglass
	DoEvents()

#If DEBUGGING Then
    ' Desenha a localização onte bala
    ' irá passar na posição Y onde iniciou
    ' Distance = 2 * V^2 * Sin(T) * Cos(T) / g = V^2 * Sin(2*T) / g
    picCanvas.FillColor = vbBlue
    picCanvas.Circle (m_BulletX + 2 * speed ^ 2 * Sin(m_Theta) * Cos(m_Theta) / 9.8, m_BulletY), CANNON_HGT / 2, vbBlue
#End If

	' Inicia o movimento da bola do canhão
	tmrMoveShot.Enabled = True
End Sub

var Sub Form_Load()
	tmrMoveShot.Enabled = False
	tmrMoveShot.Interval = 1 / TICKS_PER_SECOND * 1000
	Randomize()
	MoveHouse()
	DrawField()
End Sub

var Sub tmrMoveShot_Timer()
	' Apaga a posição anterior da bala de canhão
	picCanvas.FillColor = picCanvas.BackColor
    picCanvas.Circle (m_BulletX, m_BulletY), CANNON_HGT / 2, picCanvas.BackColor

	' movimenta a bola de canhão
	m_Vy = m_Vy + m_YAcceleration
	m_BulletX = m_BulletX + m_Vx
	m_BulletY = m_BulletY + m_Vy

	' Desenha a  nova bala de canhão
    picCanvas.Circle (m_BulletX, m_BulletY), CANNON_HGT / 2, vbBlack

	' Verifica se podemos parar
	If (m_BulletY > picCanvas.ScaleHeight) Or (m_BulletX > picCanvas.ScaleWidth) Then _
	 ' Para de executar
		tmrMoveShot.Enabled = False

		' habilita os controles
		cmdFire.Enabled = True
		txtDegrees.Enabled = True
		txtSpeed.Enabled = True
		Screen.MousePointer = vbDefault
	End If
End Sub

var Sub txtDegrees_Change()
	DrawField()
End Sub

var Sub txtSpeed_Change()
	DrawField()
End Sub
