import pickle
import json
from colorama import Fore, Back, Style

import util

#  REM HAUNTED HOUSE ADVENTURE
#  REM ***********************
#  REM THIS VERSION FOR "MICROSOFT" BASIC
#  REM *******************************

class Game:
    def __init__(self):

        self.show_trace = 0

        self.v_RM = 32   # position 57 orig , 0-63
        self.v_MS = ""  # user msg
        self.v_QSi = "" # user input
        self.v_VSi = "" # user input
        self.v_WSi = "OK" # user input
        self.v_VB = 0  # verb num
        self.v_OB = 0  # obj num

        self.v_LL = 60  # candle
        self.v_D = 0    # direction 1-6 "NSEWUD"

        self.v_L = [ 0, # first index 0 is ignored
            46, 38, 35, 50, 13, 18,
            28, 42, 10, 25, 26, 4 ,
            2 , 7 , 47, 60, 43, 32
            ] #18 (0-17) # obj pos

        #  DIM VS[V]  = 25 - VERB DESCRIPTION
        #'none', # first index 0 is ignored
        self.v_VS = [
            'HELP' , 'INV', 'GO'   , #0-2
            'N'    , 'S'        , 'W'    , 'E'      , 'U'    , 'D'  , #3-8
            'GET'  , 'TAKE'     , 'OPEN' , 'EXAMINE', 'READ' , 'SAY', 'DIG', #9-15
            'SWING', 'CLIMB'    , 'LIGHT', 'UNLIGHT', 'SPRAY', #16-20
            'USE'  , 'UNLOCK'   , 'LEAVE', 'SCORE'  , #21-24
            'EXIT' , 'QUIT'     , 'DEBUG', 'SAVE'   , 'LOAD'] #25-29

        self.v_V = len(self.v_VS) #28, (0-27)

        #  DIM OS[W] - OBJECT DESCRIPTION W = 36 (0-35)
        # first index 0 is ignored
        self.v_OS = [ 'none',
            'PAINTING' , 'RING'  , 'MAGICSPELLS', 'GOBLET' , 'SCROLL',
            'COINS'    , 'STATUE', 'CANDLESTICK' , 'MATCHES', 'VACUUM',
            'BATTERIES', 'SHOVEL', 'AXE'         , 'ROPE'   , 'BOAT'  ,
            'AEROSOL'  , 'CANDLE', 'KEY'         , 'NORTH'  , 'SOUTH' ,
            'WEST'     , 'EAST'  , 'UP'          , 'DOWN'   , 'DOOR'  ,
            'BATS'     , 'GHOSTS', 'DRAWER'      , 'DESK'   , 'COAT'  ,
            'RUBBISH'  , 'COFFIN', 'BOOKS'       , 'XZANFAR', 'WALL'  ,
            'SPELLS']

        self.v_W = len(self.v_OS) #36 (0-35)

        # 0-17 are gettable objects
        self.v_G = 19 #0-17

        self.v_C = [0] * self.v_W   # obj carrying

        # Flags: 1 = hidden obj | 0 = discovered obj
        # F(0) = flag for candle on/of
        self.v_F = [0] * self.v_W   # obj flag

        self.v_F[2] = 1     # spells
        self.v_F[17] = 1    # candle
        self.v_F[18] = 1    # key
        self.v_F[23] = 1    # up/down
        self.v_F[26] = 1    # bats
        self.v_F[28] = 1    # drawer

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

    def debug(self):
        util.console(" ------------------- debug -------------------")
        util.console(" v_RM: ", self.v_RM)
        util.console(" v_D: ", self.v_D)
        util.console(" v_MS: ", self.v_MS)
        util.console(" v_QSi: ", self.v_QSi)
        util.console(" v_VSi: ", self.v_VSi)
        util.console(" v_WSi: ", self.v_WSi)
        util.console(" v_VB: ", self.v_VB)
        util.console(" v_OB: ", self.v_OB)
        util.console(" v_V: ", self.v_V)
        util.console(" v_G: ", self.v_G)
        util.console(" v_W: ", self.v_W)

        util.console_list(" v_L: ", self.v_L)
        util.console_list(" v_C: ", self.v_C)
        util.console_list(" v_F: ", self.v_F)
        util.console(" ------------------- debug -------------------")

    def trace(self, text1, text2 = ""):
        if(self.show_trace):
            util.console(text1, text2)

    def play(self):
        self.show_intro()
        while True:
            self.show_location()
            self.show_exits()
            self.show_objects()
            self.print_response()
            self.read_input()
            self.perform_action()
            if self.v_QSi =='QUIT' or self.v_QSi =='EXIT':
                break

    def show_intro(self):
        intro =  "\n"
        intro += "-------------------------------------------------\n"
        intro += "--------------- HAUNTED HOUSE -------------------\n"
        intro += "-------------------------------------------------\n"
        print(intro)
        # print(Fore.MAGENTA + Back.WHITE + intro + Style.RESET_ALL)
        # print(" "+ Style.RESET_ALL)

    def show_location(self):
        # self.trace(" v_RM: ", self.v_RM)
        print("> you are in : %s" % self.v_RM)
        print("> you are in : %s" % (self.v_DS[self.v_RM]))

    def show_exits(self):
        exits = self.v_RS[self.v_RM]
        chars = util.split(exits)
        # print("> you can go : %s " % chars)
        ex = "+-----+\n"
        if "N" in chars:
            ex += "|  N  |\n"
        else:
            ex += "|     |\n"

        if "W" in chars:
            ex += "| W"
        else:
            ex += "|  "

        if "E" in chars:
            ex += " E |\n"
        else:
            ex += "   |\n"

        if "S" in chars:
            ex += "|  S  |\n"
        else:
            ex += "|     |\n"

        ex += "+-----+\n"
        print("> you can go :")
        print(ex)

    def show_objects(self):
        for i in range(self.v_G):
            if self.v_L[i] == self.v_RM and self.v_F[i] == 0:
                print("> here you can see : ", self.v_OS[i])

    def print_response(self):
        # print message/response
        print("-------------------------------------------------")
        print("] Response: ")
        print("] ", self.v_MS)
        print("-------------------------------------------------")
        print(" ")

    def read_input(self):
        self.v_MS = "WHAT?"
        self.v_VSi = ""
        self.v_WSi = ""
        self.v_VB = 0
        self.v_OB = 0
        # read user input
        self.v_QSi = input("WHAT WILL YOU DO NOW ? ")
        self.v_QSi = self.v_QSi.upper()
        self.v_QSi = self.v_QSi.strip(' ')

        # show help if empty input
        if not self.v_QSi:
            self.v_QSi = "HELP"

        words = self.v_QSi.split()
        if len(words) == 1:
            self.v_VSi = words[0]
            self.v_WSi = "" # words[0]
        else:
            self.v_VSi = words[0]
            self.v_WSi = words[1]

        self.trace("-------------------------------------------------")
        self.trace("...read input:")
        self.trace(" 1 read_input v_VSi", self.v_VSi) # verb

        if self.v_VSi in self.v_VS:
            self.v_VB = self.v_VS.index(self.v_VSi)

        self.trace(" 2 read_input v_VB", self.v_VB) # verb number
        self.trace(" 3 read_input v_WSi", self.v_WSi) # word

        if self.v_WSi in self.v_OS:
            self.v_OB = self.v_OS.index(self.v_WSi)

        self.trace(" 4 read_input v_OB", self.v_OB) # object number

        if self.v_VB == 0 and self.v_OB == 0:
            self.v_MS = "THAT'S SILLY, I don't understand"
            return

        if self.v_WSi == "" and self.v_VB < 24:
            self.v_MS = "I NEED TWO WORDS"

        if self.v_VB > self.v_V and self.v_OB > 0:
            self.v_MS = "YOU CAN'T [" + self.v_QSi + "]"

        if self.v_VB > self.v_V and self.v_OB == 0:
            self.v_MS = "YOU DON'T MAKE SENSE"

        if self.v_VB < self.v_V and self.v_OB > 0 and self.v_C[self.v_OB] == 0:
            self.v_MS = "YOU DON'T HAVE " + self.v_WSi

        rnd_num = util.rnd(4)
        if self.v_F[26] == 1 and self.v_RM == 13 and rnd_num != 3 and self.v_VB != 21:
            self.v_MS = "BATS ATTACKING!"
            return

        rnd_num = util.rnd(2)
        if self.v_RM == 44 and rnd_num == 1 and self.v_F[24] != 1:
            self.v_F[27] = 1

        # candle on/off
        if self.v_F[0] == 1:
            self.v_LL = self.v_LL - 1
        if self.v_LL < 1:
            self.v_F[0] = 0

        if self.v_LL == 10:
            self.v_MS = "YOUR CANDLE IS WANING!"
        if self.v_LL == 1:
            self.v_MS = "YOUR CANDLE IS OUT!"

        self.trace("...read input:")
        self.trace("-------------------------------------------------")

    def perform_action(self):
        self.trace("-------------------------------------------------")
        self.trace("...perform action:")

        self.trace(" 5 perform_action v_VB ", self.v_VB)

        # if self.v_VB == 0 and self.v_OB == 0:
        #     return
        if self.v_VB == 0:
            self.show_help()
        elif self.v_VB == 1:
            self.show_inventory()
        elif self.v_VB in [2,3,4,5,6,7,8]:
            self.action_move()
        elif self.v_VB == 9:
            self.get_980()
        elif self.v_VB == 10:
            self.get_980()
        elif self.v_VB == 11:
            self.open_1030()
        elif self.v_VB == 12:
            self.examine_1070()
        elif self.v_VB == 13:
            self.read_1140()
        elif self.v_VB == 14:
            self.say_1180()
        elif self.v_VB == 15:
            self.dig_1220()
        elif self.v_VB == 16:
            self.swing_1250()
        elif self.v_VB == 17:
            self.climb_1300()
        elif self.v_VB == 18:
            self.light_1340()
        elif self.v_VB == 19:
            self.unlight_1380()
        elif self.v_VB == 20:
            self.spray_1400()
        elif self.v_VB == 21:
            self.use_1430()
        elif self.v_VB == 22:
            self.unlock_1460()
        elif self.v_VB == 23:
            self.leave_1490()
        elif self.v_VB == 24:
            self.score_1510()
        elif self.v_VB == 25:
            self.exit_game()
        elif self.v_VB == 26:
            self.exit_game()
        elif self.v_VB == 27:
            self.debug()
        elif self.v_VB == 28:
            self.save_game()
        elif self.v_VB == 29:
            self.load_game()

        self.trace("...perform action:")
        self.trace("-------------------------------------------------")
        return

    # actions

    #  REM HELP
    def show_help(self):
        print(Fore.YELLOW + "-------------------------------------------------")
        print("WORDS I KNOW : ")
        # print(self.v_VS)
        for a,b,c in zip(self.v_VS[::3],self.v_VS[1::3],self.v_VS[2::3]):
            print('{:<20}{:<20}{:<}'.format(a,b,c))
        print("-------------------------------------------------" + Style.RESET_ALL)

        self.v_MS = ""
        return

    #  REM INVENTORY
    def show_inventory(self):
        print("YOU ARE CARRYING : ")

        # crate a list
        objList = []
        carrying = []
        for i in range(self.v_G):
            #print(self.v_C[i], self.v_OS[i])
            s = str(self.v_C[i]) +" - "+ self.v_OS[i]
            objList.append(s)
            if self.v_C[i] == 1:
                carrying.append(self.v_OS[i])
        print(carrying)
        #print(objList)
        # slice syntax is: start:stop:step
        for a,b,c in zip(objList[::3], objList[1::3], objList[2::3]):
             print('{:<20}{:<20}{:<}'.format(a,b,c))
        print("-------------------------------------------------")
        self.v_MS = ""
        return

    #  REM GO N:3 S:4 W:5 E:6 U:7 D:8
    def action_move(self):
        self.v_D = 0

        #if self.v_OB == 0:
        #    self.v_D = self.v_VB - 3

        self.trace(" 6 go v_VB", self.v_VB)
        self.trace(" 6 go v_OB", self.v_OB)

        # N
        if self.v_OB == 19 or self.v_VB == 3:
            self.v_D = 1
        # S
        if self.v_OB == 20 or self.v_VB == 4:
            self.v_D = 2
        # W
        if self.v_OB == 21 or self.v_VB == 5:
            self.v_D = 3
        # E
        if self.v_OB == 22 or self.v_VB == 6:
            self.v_D = 4
        # U
        if self.v_OB == 23 or self.v_VB == 7:
            self.v_D = 5
        # D
        if self.v_OB == 24 or self.v_VB == 8:
            self.v_D = 6

        self.trace(" 6 go v_D", self.v_D)

        if self.v_RM == 20 and self.v_D == 5: # up
            self.v_D = 1
        if self.v_RM == 20 and self.v_D == 6: # down
            self.v_D = 3
        if self.v_RM == 22 and self.v_D == 6: # down
            self.v_D = 2
        if self.v_RM == 22 and self.v_D == 5: # up
            self.v_D = 3
        if self.v_RM == 36 and self.v_D == 6: # down
            self.v_D = 1
        if self.v_RM == 36 and self.v_D == 5: # up
            self.v_D = 2

        # rope
        if self.v_F[14] == 1:
            self.v_MS = "CRASH! YOU FELL OUT OF THE TREE!"
            self.v_F[14] = 0
            return

        # ghosts
        if self.v_F[27] == 1 and self.v_RM == 52:
            self.v_MS = "GHOSTS WILL NOT LET YOU MOVE"
            return

        # XZANFAR
        if self.v_RM == 45 and self.v_C[1] == 1 and self.v_F[34] == 0:
            self.v_MS = "A MAGICAL BARRIER TO THE WEST"
            return

        # F(0) candle
        if (self.v_RM == 26 and self.v_F[0] == 0) and (self.v_D == 1 or self.v_D == 4):
            self.v_MS = "YOU NEED A LIGHT"
            return

        # if you come by the wrong side, you get stuck
        if self.v_RM == 54 and self.v_C[15] != 1:
            self.v_MS = "YOU'RE STUCK IN THE MARSH, there is nowere to go"
            return

        # if you are in a boat, you can only go to 53, 54, 55, 47
        if self.v_C[15] == 1 and not (self.v_RM == 53 or self.v_RM == 54 or self.v_RM == 55 or self.v_RM == 47):
            self.v_MS = "you came so far, but YOU CAN'T CARRY A BOAT!"
            return

        # F(0) candle
        if (self.v_RM > 26 and self.v_RM < 30) and self.v_F[0] == 0:
            self.v_MS = "TOO DARK TO MOVE"
            return

        # wall?
        self.v_F[35] = 0

        exits = self.v_RS[self.v_RM]
        chars = util.split(exits)
        for US in chars:
            if US == "N" and self.v_D == 1 and self.v_F[35] == 0:
                self.v_RM = self.v_RM - 8
                self.v_F[35] = 1
            if US == "S" and self.v_D == 2 and self.v_F[35] == 0:
                self.v_RM = self.v_RM + 8
                self.v_F[35] = 1
            if US == "W" and self.v_D == 3 and self.v_F[35] == 0:
                self.v_RM = self.v_RM - 1
                self.v_F[35] = 1
            if US == "E" and self.v_D == 4 and self.v_F[35] == 0:
                self.v_RM = self.v_RM + 1
                self.v_F[35] = 1
        self.v_MS = "OK"

        # wall?
        if self.v_F[35] == 0:
            self.v_MS = "CAN'T GO THAT WAY"

        if self.v_D < 1:
            self.v_MS = "GO WHERE?"

        if self.v_RM == 41 and self.v_F[23] == 1:
            self.v_RS[49] = "SW"
            self.v_MS = "THE DOOR SLAMS SHUT!"
            self.v_F[23] = 0 # up/down
        return

    #  REM GET, TAKE
    def get_980(self):
        # can get only 0-17
        if self.v_OB > self.v_G:
            self.v_MS = "I CAN'T GET " + self.v_WSi
            return

        if self.v_L[self.v_OB] != self.v_RM:
            self.v_MS = "THERE IS NO " + self.v_WSi + " HERE"

        if self.v_F[self.v_OB] != 0:
            self.v_MS = "WHAT " + self.v_WSi + "?"

        if self.v_C[self.v_OB] == 1:
            self.v_MS = "YOU ALREADY HAVE IT"

        if self.v_OB > 0 and self.v_L[self.v_OB] == self.v_RM and self.v_F[self.v_OB] == 0:
            self.v_C[self.v_OB] = 1
            self.v_L[self.v_OB] = 65 #carrying obj
            self.v_MS = "YOU HAVE THE " + self.v_WSi
        return

    #  REM OPEN
    def open_1030(self):
        # F(17) key
        if self.v_RM == 43 and (self.v_OB == 28 or self.v_OB == 29):
            self.v_F[17] = 0
            self.v_MS = "DRAWER OPEN"

        # 24 door
        if self.v_RM == 28 and self.v_OB == 25:
            self.v_MS = "IT'S LOCKED"

        # 31 coffin
        if self.v_RM == 38 and self.v_OB == 32:
            self.v_MS = "THAT'S CREEPY!"
            self.v_F[2] = 0
        return

    #  REM EXAMINE
    def examine_1070(self):
        # set default response
        self.v_MS = "nothing special, just a normal " + self.v_OS[self.v_OB]
        if self.v_OB == 0:
            self.v_MS = "Examine what?"
            return

        #if self.v_VB < self.v_V and self.v_OB > 0 and self.v_C[self.v_OB] == 0:
        # if self.v_C[self.v_OB] == 0 \
        #         and self.v_L[self.v_OB] != self.v_RM:
        #     self.v_MS = "YOU DON'T HAVE " + self.v_WSi
        #     return

        # coat
        if self.v_OB == 30:
            self.v_F[18] = 0 # key
            self.v_MS = "SOMETHING HERE!"

        # rubbish
        if self.v_OB == 31:
            self.v_MS = "THAT'S DISGUSTING!"

        # drawer, desk
        if self.v_OB == 28 or self.v_OB == 29:
            self.v_MS = "THERE IS A DRAWER"

        # book, scroll
        if self.v_OB == 33 or self.v_OB == 5:
            self.read_1140()

        # wall
        if self.v_OB == 35 and self.v_RM == 43:
            self.v_MS = "THERE IS SOMETHING BEYOND.."

        # coffin
        if self.v_OB == 32:
            self.open_1030()
        return

    #  REM READ
    def read_1140(self):
        # books
        if self.v_RM == 42 and self.v_OB == 33:
            self.v_MS = "THEY ARE DEMONIC WORKS"

        # spells
        if (self.v_OB == 3 or self.v_OB == 36) and self.v_C[3] == 1 and self.v_F[34] == 0:
            self.v_MS = "USE THIS WORD WITH CARE 'XZANFAR'"

        # scroll
        if self.v_C[5] == 1 and self.v_OB == 5:
            self.v_MS = "THE SCRIPT IS IN AN ALIEN TONGUE"
        return

    #  REM SAY
    def say_1180(self):
        self.v_MS = "OK '" + self.v_WSi + "'"
        # XZANFAR
        if self.v_C[3] == 1 and self.v_OB == 34:
            self.v_MS = "* MAGIC OCCUR *"
            if self.v_RM != 45:
                rnd_num = util.rnd(63)
                self.v_RM = rnd_num
        # XZANFAR
        if self.v_C[3] == 1 and self.v_OB == 34 and self.v_RM == 45:
            self.v_F[34] = 1
        return

    #  REM DIG
    def dig_1220(self):
        # shovel
        if self.v_C[12] == 1:
            self.v_MS = "YOU MADE A HOLE"
        # shovel
        if self.v_C[12] == 1 and self.v_RM == 30:
            self.v_MS = "DUG THE BARS OUT"
            self.v_DS[self.v_RM] = "HOLE IN WALL"
            self.v_RS[self.v_RM] = "NSE"
        return

    #  REM SWING
    def swing_1250(self):
        # rope
        if self.v_C[14] != 1 and self.v_RM == 7:
            self.v_MS = "THIS IS NO TIME TO PLAY GAMES"

        # rope
        if self.v_OB == 14 and self.v_C[14] == 1:
            self.v_MS = "YOU SWUNG IT"

        # axe
        if self.v_OB == 13 and self.v_C[13] == 1:
            self.v_MS = "WHOOSH!"

        # axe
        if self.v_OB == 13 and self.v_C[13] == 1 and self.v_RM == 43:
            self.v_RS[self.v_RM] = "WN"
            self.v_DS[self.v_RM] = "STUDY WITH SECRET ROOM"
            self.v_MS = "YOU BROKE THE THIN WALL"
        return

    #  REM CLIMB
    def climb_1300(self):
        # rope
        if self.v_OB == 14 and self.v_C[14] == 1:
            self.v_MS = "rope IT ISN'T ATTACHED TO ANYTHING!"

        if self.v_OB == 14 and self.v_C[14] != 1 and self.v_RM == 7 and self.v_F[14] == 0:
            self.v_MS = "YOU SEE THICK FOREST and CLIFF SOUTH"
            self.v_F[14] = 1
            return

        if self.v_OB == 14 and self.v_C[14] != 1 and self.v_RM == 7 and self.v_F[14] == 1:
            self.v_MS = "GOING DOWN"
            self.v_F[14] = 0
            return

    #  REM LIGHT
    def light_1340(self):
        # 16 candle , 7 candlestick, 8 matches
        if self.v_OB == 17 and self.v_C[17] == 1 and self.v_C[8] == 0:
            self.v_MS = "IT WILL BURN YOUR HANDS"

        if self.v_OB == 17 and self.v_C[17] == 1 and self.v_C[9] == 0:
            self.v_MS = "NOTHING TO LIGHT IT WITH"

        if self.v_OB == 17 and self.v_C[17] == 1 and self.v_C[9] == 1 and self.v_C[8] == 1:
            self.v_MS = "IT CASTS A FLICKERING LIGHT"
            self.v_F[0] = 1 # on/off
        return

    #  REM UNLIGHT
    def unlight_1380(self):
        # candle
        if self.v_F[0] == 1:
            self.v_F[0] = 0  # on/off
            self.v_MS = "EXTINGUISHED"
        return

    #  REM SPRAY
    def spray_1400(self):
        # 15 aerosol
        if self.v_OB == 26 and self.v_C[16] == 1:
            self.v_MS = "HISSSS"

        if self.v_OB == 26  and self.v_C[16] == 1 and self.v_F[26] == 1:
            self.v_F[26] = 0
            self.v_MS = "PFFT! GOT THEM"
        return

    #  REM USE
    def use_1430(self):
        # 9 vacuum, 10 batteries
        if self.v_OB == 10 and self.v_C[10] == 1 and self.v_C[11] == 1:
            self.v_MS = "SWITCHED ON"
            self.v_F[24] = 1
        # 26 ghosts
        if self.v_F[27] == 1 and self.v_F[24] == 1:
            self.v_MS = "WHIZZ- VACUUMED THE GHOSTS UP!"
            self.v_F[27] = 0

        # axe
        if self.v_OB == 13 and self.v_C[13] == 1:
            self.v_MS = "WHOOSH!"
        # axe
        if self.v_OB == 13 and self.v_C[13] == 1 and self.v_RM == 43:
            self.v_RS[self.v_RM] = "WN"
            self.v_DS[self.v_RM] = "STUDY WITH SECRET ROOM"
            self.v_MS = "YOU BROKE THE THIN WALL"

        return

    #  REM UNLOCK
    def unlock_1460(self):
        if self.v_RM == 43 and (self.v_OB == 27 or self.v_OB == 28):
            self.use_1430()

        if self.v_RM == 28 and self.v_OB == 25 and self.v_F[25] == 0 and self.v_C[18] == 1:
            self.v_F[25] = 1
            self.v_RS[self.v_RM] = "SEW"
            self.v_DS[self.v_RM] = "HUGE OPEN DOOR"
            self.v_MS = "THE KEY TURNS!"
        return

    #  REM LEAVE
    def leave_1490(self):
        if self.v_C[self.v_OB] == 1:
            self.v_C[self.v_OB] = 0
            self.v_L[self.v_OB] = self.v_RM
            self.v_MS = "object drop"
        return

    #  REM SCORE
    def score_1510(self):
        S = 0
        for i in range(self.v_G):
            if self.v_C[i] == 1:
                S = S + 1
        if S == 17 and self.v_C[15] != 1 and self.v_RM != 57:
            print("YOU HAVE EVERYTHING")
            print("RETURN TO THE GATE FOR FINAL SCORE")
        if S == 17 and self.v_RM == 57:
            print("DOUBLE SCORE FOR REACHING HERE")
            S = S * 2
        print("] YOUR SCORE = %s out of 17" % S)

        if S > 18:
            print("WELL DONE! YOU FINISHED THE GAME")
            exit
        self.v_QSi = input("PRESS return TO CONTINUE")
        return

    def save_game_bin(self):
        game_state = [self.v_RM, self.v_L, self.v_C, self.v_F]

        with open('savedgame.bin', 'wb') as filehandle:
            pickle.dump(game_state, filehandle)

        print('file saved')
        return

    def load_game_bin(self):

        with open('savedgame.bin', 'rb') as filehandle:
            self.v_RM, self.v_L, self.v_C, self.v_F = pickle.load(filehandle)

        print('file loaded')
        return


    def save_game(self):
        game_state = [self.v_RM, self.v_L, self.v_C, self.v_F]

        fname = "savedgame-"+ str(self.v_WSi) +".json"
        with open(fname, 'w') as filehandle:
            json.dump(game_state, filehandle)

        print("] saved game as: " + fname)
        return

    def load_game(self):
        fname = "savedgame-"+ str(self.v_WSi) +".json"
        print(fname)
        with open(fname, 'r') as filehandle:
            data = json.load(filehandle)

        self.v_RM = data[0]
        self.v_L = data[1]
        self.v_C = data[2]
        self.v_F = data[3]

        print("] loaded game: " + fname)
        return

    def exit_game(self):
        print("bye!")

#end

# game starts here
MyGame = Game()
MyGame.play()
