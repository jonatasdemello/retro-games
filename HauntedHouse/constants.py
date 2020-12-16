# Constants

# DIM V=25 : REM  NUBMER OF VERBS
# DIM W=36 : REM  NUMBER OF OBJECTS
# DIM G=18 : REM  NUMBER OF GETTABLE OBJECTS
# DIM D  : REM  DIRECTION TO MOVE (1=N, 2=S, 3=W, 4=E, 5=UP, 6=DN)

show_trace = 1

# v_W : 37 number of objects
NUM_VERBS = 25
NUM_OBJECTS = 37
NUM_GET_OBJ = 19

#  DIM VS[V]  = 25 - VERB DESCRIPTION
v_VS = [
    'HELP','INV','GO','N','S',
    'W','E','U','D','GET',
    'TAKE','OPEN','EXAMINE','READ','SAY',
    'DIG','SWING','CLIMB','LIGHT','UNLIGHT',
    'SPRAY','USE','UNLOCK','LEAVE','SCORE',
    'EXIT','QUIT','DEBUG','SAVE','LOAD'
    ]

#  DIM OS[W] - OBJECT DESCRIPTION W = 36 (0-35)
# first index 0 is ignored
v_OS = [ 'none',
    'PAINTING','RING','MAGICSPELLS','GOBLET','SCROLL',
    'COINS','STATUE','CANDLESTICK','MATCHES','VACUUM',
    'BATTERIES','SHOVEL','AXE','ROPE','BOAT',
    'AEROSOL','CANDLE','KEY','NORTH','SOUTH',
    'WEST','EAST','UP','DOWN','DOOR',
    'BATS','GHOSTS','DRAWER','DESK','COAT',
    'RUBBISH','COFFIN','BOOKS','XZANFAR','WALL',
    'SPELLS']

# number of verbs
v_V = len(v_VS) #28, (0-27)

# number of objects
v_W = len(v_OS) #36 (0-35)

# 0-17 are gettable objects
v_G = 19 #0-17


# object	obj_list	Obj-Loc
#  0	none
#  1	PAINTING	46
#  2	RING	38
#  3	MAGIC SPELLS	35
#  4	GOBLET	50
#  5	SCROLL	13
#  6	COINS	18
#  7	STATUE	28
#  8	CANDLESTICK	42
#  9	MATCHES	10
# 10	VACUUM	25
# 11	BATTERIES	26
# 12	SHOVEL	4
# 13	AXE	2
# 14	ROPE	7
# 15	BOAT	47
# 16	AEROSOL	60
# 17	CANDLE	43
# 18	KEY	32