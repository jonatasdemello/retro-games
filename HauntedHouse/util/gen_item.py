objs = [ 'none', # first index 0 is ignored
'PAINTING' , 'RING'  , 'MAGICSPELLS', 'GOBLET' , 'SCROLL',
'COINS'    , 'STATUE', 'CANDLESTICK' , 'MATCHES', 'VACUUM',
'BATTERIES', 'SHOVEL', 'AXE'         , 'ROPE'   , 'BOAT'  ,
'AEROSOL'  , 'CANDLE', 'KEY'         , 'NORTH'  , 'SOUTH' ,
'WEST'     , 'EAST'  , 'UP'          , 'DOWN'   , 'DOOR'  ,
'BATS'     , 'GHOSTS', 'DRAWER'      , 'DESK'   , 'COAT'  ,
'RUBBISH'  , 'COFFIN', 'BOOKS'       , 'XZANFAR', 'WALL'  ,
'SPELLS']

ix = 0
for i in objs:
    code = "class "+ i.lower() +"(Item):\n" + \
        "    def __init__(self, loc):\n"+ \
        "        id = "+ str(ix) +"\n"+ \
        "        name = \""+ i.lower() +"\"\n"+ \
        "        value = 1\n"+ \
        "        description = \"Object: "+ i.lower() +".\"\n"+ \
        "        super().__init__(id, name, description, value, loc)\n"
    print(code)
    ix += 1