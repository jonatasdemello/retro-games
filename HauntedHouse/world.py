import util

class World:
    def __init__(self):
        #  DIM R$[64] - ROUTES AT EACH LOCATION "NSEW"
        self.v_RS = [
            'SE','WE' ,'WE' ,'SWE','WE'  ,'WE' ,'SWE' ,'WS' ,
            'NS','SE' ,'WE' ,'NW' ,'SE'  ,'W'  ,'NE'  ,'NSW',
            'NS','NS' ,'SE' ,'WE' ,'NWUD','SE' ,'WSUD','NS' ,
            'N' ,'NS' ,'NSE','WE' ,'WE'  ,'NSW','NS'  ,'NS' ,
            'S' ,'NSE','NSW','S'  ,'NSUD','N'  ,'N'   ,'NS' ,
            'NE','NW' ,'NE' ,'W'  ,'NSE' ,'WE' ,'W'   ,'NS' ,
            'SE','NSW','E'  ,'WE' ,'NW'  ,'S'  ,'SW'  ,'NW' ,
            'NE','NWE','WE' ,'WE' ,'WE'  ,'NWE','NWE' ,'W'
            ] #8x8 (0-64)

        #  DIM D$[64] - DESCRIPTIONS FOR EACH LOCATION
        self.v_DS = ['DARK CORNER','OVERGROWN GARDEN','BY LARGE WOODPILE','YARD BY RUBBISH','WEEDPATCH','FOREST','THICK FOREST','BLASTED TREE',
            'CORNER OF HOUSE','ENTRANCE TO KITCHEN','KITCHEN & GRIMY COOKER','SCULLERY DOOR','ROOM WITH INCHES OF DUST','REAR TURRET ROOM','CLEARING BY HOUSE','PATH',
            'SIDE OF HOUSE','BACK OF HALLWAY','DARK ALCOVE','SMALL DARK ROOM','BOTTOM OF SPIRAL STAIRCASE','WIDE PASSAGE','SLIPPERY STEPS','CLIFFTOP',
            'NEAR CRUMBLING WALL','GLOOMY PASSAGE','POOL OF LIGHT','IMPRESSIVE VAULTED HALLWAY','HALL BY THICK WOODEN DOOR','TROPHY ROOM','CELLAR WITH BARRED WINDOW','CLIFF PATH',
            'CUPBOARD WITH HANGING COAT','FRONT HALL','SITTING ROOM','SECRET ROOM','STEEP MARBLE STAIRS','DINING ROOM','DEEP CELLAR WITH COFFIN','CLIFF PATH',
            'CLOSET','FRONT LOBBY','LIBRARY OF EVIL BOOKS','STUDY WITH DESK & HOLE IN WALL','WEIRD COBWEBBY ROOM','VERY COLD CHAMBER','SPOOKY ROOM','CLIFF PATH BY MARSH',
            'RUBBLE-STREWN VERANDAH','FRONT PORCH','FRONT TOWER','SLOPING CORRIDOR','UPPER GALLERY','MARSH BY WALL','MARSH','SOGGY PATH',
            'BY TWISTED RAILING','PATH THROUGH IRON GATE','BY RAILINGS','BENEATH FRONT TOWER','DEBRIS FROM CRUMBLING FACADE','LARGE FALLEN BRICKWORK','ROTTING STONE ARCH','CRUMBLING CLIFFTOP'
            ]
    #end int

    def show_location(self, playerLocation):
        # self.trace(" v_RM: ", self.v_RM)
        print("> you are in : %s" % playerLocation)
        print("> you are in : %s" % (self.v_DS[playerLocation]))

    def show_exits(self, playerLocation):
        exits = self.v_RS[playerLocation]
        chars = util.split(exits)
        # print("> you can go : %s " % chars)
        ex = "\t\t+-----+\n"
        if "N" in chars:
            ex += "\t\t|  N  |\n"
        else:
            ex += "\t\t|     |\n"

        if "W" in chars:
            ex += "\t\t| W"
        else:
            ex += "\t\t|  "

        if "E" in chars:
            ex += " E |\n"
        else:
            ex += "   |\n"

        if "S" in chars:
            ex += "\t\t|  S  |\n"
        else:
            ex += "\t\t|     |\n"

        ex += "\t\t+-----+\n"
        print("> you can go :")
        print(ex)