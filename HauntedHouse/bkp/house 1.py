import random
import pickle
import json

# from list_verbs import VS_verb_list
# print(VS_verb_list)

#  REM HAUNTED HOUSE ADVENTURE
#  REM ***********************
#  REM THIS VERSION FOR "MICROSOFT" BASIC
#  REM *******************************
#
# helper functions
def split(word):
    return [char for char in word]

def rnd(num):
    return random.randrange(1, num)

def console(str1 = "", str2 = ""):
    result = "\t\t\t\t\t" + str(str1)
    if str2:
        result = result +"\t"+ str(str2)
    print(result)
    return

class Game:
    def __init__(self):
        self.RM_player_location = 32 # 57 orig
        self.MS_player_msg = ""
        self.QS_user_input = ""
        self.VS_verb_input = ""
        self.WS_word_input = "OK"
        self.VB_verb_number = 0
        self.LL_candle = 60
        self.D_move_direction = 0
        self.OB_object = 0
        self.L_location_obj = [
            46, 38, 35, 50, 13, 18,
            28, 42, 10, 25, 26, 4 ,
            2 , 7 , 47, 60, 43, 32
            ] #18 (0-17)

        #  DIM VS[V]  = 25 - VERBS
        self.VS_verb_list = [
            'HELP' , 'INVENTORY', 'GO'   , #0-2
            'N'    , 'S'        , 'W'    , 'E'      , 'U'    , 'D'  , #3-8
            'GET'  , 'TAKE'     , 'OPEN' , 'EXAMINE', 'READ' , 'SAY', 'DIG', #9-15
            'SWING', 'CLIMB'    , 'LIGHT', 'UNLIGHT', 'SPRAY', #16-20
            'USE'  , 'UNLOCK'   , 'LEAVE', 'SCORE'  , #21-24
            'EXIT' , 'QUIT'     , 'DEBUG', 'SAVE'   , 'LOAD'] #25-29

        self.V_n_verbs = len(self.VS_verb_list) #28, (0-27)

        #  DIM OS[W] - OBJECT DESCRIPTION W = 36 (0-35)
        self.OS_obj_list = [
            'PAINTING' , 'RING'  , 'MAGIC SPELLS', 'GOBLET' , 'SCROLL',
            'COINS'    , 'STATUE', 'CANDLESTICK' , 'MATCHES', 'VACUUM',
            'BATTERIES', 'SHOVEL', 'AXE'         , 'ROPE'   , 'BOAT'  ,
            'AEROSOL'  , 'CANDLE', 'KEY'         , 'NORTH'  , 'SOUTH' ,
            'WEST'     , 'EAST'  , 'UP'          , 'DOWN'   , 'DOOR'  ,
            'BATS'     , 'GHOSTS', 'DRAWER'      , 'DESK'   , 'COAT'  ,
            'RUBBISH'  , 'COFFIN', 'BOOKS'       , 'XZANFAR', 'WALL'  ,
            'SPELLS']

        self.W_n_objects = len(self.OS_obj_list) #36 (0-35)

        # 0-17 are gettable objects
        self.G_n_get_objects = 18

        self.C_carrying_obj = [0] * self.W_n_objects
        self.F_flag_obj = [0] * self.W_n_objects

        self.F_flag_obj[2] = 1
        self.F_flag_obj[17] = 1
        self.F_flag_obj[18] = 1
        self.F_flag_obj[23] = 1
        self.F_flag_obj[26] = 1
        self.F_flag_obj[28] = 1

        #  DIM R$[64] - ROUTES AT EACH LOCATION "NSEW"
        self.RS_route_desc = [
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
        self.DS_local_desc = ['DARK CORNER','OVERGROWN GARDEN','BY LARGE WOODPILE','YARD BY RUBBISH','WEEDPATCH','FOREST','THICK FOREST','BLASTED TREE',
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
        console("\n --- debug ---")
        console(" RM_player_location: ", self.RM_player_location)
        console(" MS_player_msg: ", self.MS_player_msg)
        console(" QS_user_input: ", self.QS_user_input)
        console(" VS_verb_input: ", self.VS_verb_input)
        console(" WS_word_input: ", self.WS_word_input)
        console(" VB_verb_number: ", self.VB_verb_number)
        console(" D_move_direction: ", self.D_move_direction)
        console(" LL_candle: ", self.LL_candle)
        console(" OB_object: ", self.OB_object)
        console(" V_n_verbs: ", self.V_n_verbs)
        console(" G_n_get_objects: ", self.G_n_get_objects)
        console(" W_n_objects:     ", self.W_n_objects)
        console(" L_location_obj:  ", self.L_location_obj)
        console(" C_carrying_obj:  ", self.C_carrying_obj)
        console(" F_flag_obj:      ", self.F_flag_obj)
        console(" --- debug ---")

    def play(self):
        self.show_intro()
        while True:
            self.show_location()
            self.show_exits()
            self.show_objects()
            self.read_input()
            self.perform_action()
            if self.QS_user_input =='QUIT' or self.QS_user_input =='EXIT':
                break

    def show_intro(self):
        print(" ")
        print("-----------------")
        print("- HAUNTED HOUSE -")
        print("-----------------")

    def show_location(self):
        console(" RM_player_location: ", self.RM_player_location)
        print("> you are in : %s" % (self.DS_local_desc[self.RM_player_location]))

    def show_exits(self):
        exits = self.RS_route_desc[self.RM_player_location]
        chars = split(exits)
        print("> you can go : %s " % chars)

    def show_objects(self):
        for i in range(self.G_n_get_objects):
            if self.L_location_obj[i] == self.RM_player_location and self.F_flag_obj[i] == 0:
                print("> here you can see : ")
                print("  - " , self.OS_obj_list[i])

    def read_input(self):
        print("] ", self.MS_player_msg)
        print("--------------------------------")

        self.MS_player_msg = "WHAT?"
        self.VS_verb_input = ""
        self.WS_word_input = ""
        self.VB_verb_number = 0
        self.OB_object = 0

        self.QS_user_input = input("WHAT WILL YOU DO NOW ? ")
        self.QS_user_input = self.QS_user_input.upper()
        self.QS_user_input = self.QS_user_input.strip(' ')

        # show help if empty input
        if not self.QS_user_input:
            self.QS_user_input = "HELP"

        words = self.QS_user_input.split()

        if len(words) == 1:
            self.VS_verb_input = words[0]
            self.WS_word_input = words[0]
        else:
            self.VS_verb_input = words[0]
            self.WS_word_input = words[1]

        console(" 1 read input VS_verb_input", self.VS_verb_input)

        if self.VS_verb_input in self.VS_verb_list:
            console(" 2 read input VS_verb_input index", self.VS_verb_list.index(self.VS_verb_input))

            self.VB_verb_number = self.VS_verb_list.index(self.VS_verb_input)
            console(" 3 read input VB_verb_number", self.VB_verb_number)

        console(" 4 read input WS_verb_input", self.WS_word_input)

        if self.WS_word_input in self.OS_obj_list:
            self.OB_object = self.OS_obj_list.index(self.WS_word_input)
            console(" 4 read input OB_object index", self.OB_object)

        if not self.WS_word_input and self.OB_object == 0:
            self.MS_player_msg = "THAT'S SILLY"

        #if self.VB_verb_number == 0:
        #    self.VB_verb_number = self.V_n_verbs + 1

        if self.WS_word_input == "":
            self.MS_player_msg = "I NEED TWO WORDS"

        if self.VB_verb_number > self.V_n_verbs and self.OB_object > 0:
            self.MS_player_msg = "YOU CAN'T [" + self.QS_user_input + "]"

        if self.VB_verb_number > self.V_n_verbs and self.OB_object == 0:
            self.MS_player_msg = "YOU DON'T MAKE SENSE"

        if self.VB_verb_number < self.V_n_verbs and self.OB_object > 0 and self.C_carrying_obj[self.OB_object] == 0:
            self.MS_player_msg = "YOU DON'T HAVE " + self.WS_word_input

        rnd_num = rnd(3)

        if self.F_flag_obj[26] == 1 and self.RM_player_location == 13 and rnd_num != 3 and self.VB_verb_number != 21:
            self.MS_player_msg = "BATS ATTACKING!"
            return

        rnd_num = rnd(2)
        if self.RM_player_location == 44 and rnd_num == 1 and self.F_flag_obj[24] != 1:
            self.F_flag_obj[27] = 1

        if self.F_flag_obj[0] == 1:
            self.LL_candle = self.LL_candle - 1
        if self.LL_candle < 1:
            self.F_flag_obj[0] = 0

        if self.LL_candle == 10:
            self.MS_player_msg = "YOUR CANDLE IS WANING!"
        if self.LL_candle == 1:
            self.MS_player_msg = "YOUR CANDLE IS OUT!"


    def perform_action(self):
        console(" 5 perform action VB_verb_number", self.VB_verb_number)
        if self.VB_verb_number == 0:
            self.show_help()
        elif self.VB_verb_number == 1:
            self.show_inventory()
        elif self.VB_verb_number in [2,3,4,5,6,7,8]:
            self.action_move()
        elif self.VB_verb_number == 9:
            self.get_980()
        elif self.VB_verb_number == 10:
            self.get_980()
        elif self.VB_verb_number == 11:
            self.open_1030()
        elif self.VB_verb_number == 12:
            self.examine_1070()
        elif self.VB_verb_number == 13:
            self.read_1140()
        elif self.VB_verb_number == 14:
            self.say_1180()
        elif self.VB_verb_number == 15:
            self.dig_1220()
        elif self.VB_verb_number == 16:
            self.swing_1250()
        elif self.VB_verb_number == 17:
            self.climb_1300()
        elif self.VB_verb_number == 18:
            self.light_1340()
        elif self.VB_verb_number == 19:
            self.unlight_1380()
        elif self.VB_verb_number == 20:
            self.spray_1400()
        elif self.VB_verb_number == 21:
            self.use_1430()
        elif self.VB_verb_number == 22:
            self.unlock_1460()
        elif self.VB_verb_number == 23:
            self.leave_1490()
        elif self.VB_verb_number == 24:
            self.score_1510()
        elif self.VB_verb_number == 25:
            self.exit_game()
        elif self.VB_verb_number == 26:
            self.exit_game()
        elif self.VB_verb_number == 27:
            self.debug()
        elif self.VB_verb_number == 28:
            self.save_game()
        elif self.VB_verb_number == 29:
            self.load_game()
        return

    # actions

    #  REM HELP
    def show_help(self):
        print("WORDS I KNOW : ")
        print(self.VS_verb_list)
        print("----")
        self.MS_player_msg = ""
        return

    #  REM INVENTORY
    def show_inventory(self):
        print("YOU ARE CARRYING : ")
        c = []
        for i in range(self.G_n_get_objects):
            if self.C_carrying_obj[i] == 1:
                c.append(self.OS_obj_list[i])
        print(c)
        print("----")
        self.MS_player_msg = ""
        return

    #  REM GO N:3 S:4 W:5 E:6 U:7 D:8
    def action_move(self):
        self.D_move_direction = 0

        #if self.OB_object == 0:
        #    self.D_move_direction = self.VB_verb_number - 3

        console(" 6 go VB_verb_number", self.VB_verb_number)
        console(" 6 go OB_object", self.OB_object)

        # N:3 S:4 W:5 E:6 U:7 D:8
        if self.OB_object == 18 or self.VB_verb_number == 3:
            self.D_move_direction = 1
        if self.OB_object == 19 or self.VB_verb_number == 4:
            self.D_move_direction = 2
        if self.OB_object == 20 or self.VB_verb_number == 5:
            self.D_move_direction = 3
        if self.OB_object == 21 or self.VB_verb_number == 6:
            self.D_move_direction = 4
        if self.OB_object == 22 or self.VB_verb_number == 7:
            self.D_move_direction = 5
        if self.OB_object == 23 or self.VB_verb_number == 8:
            self.D_move_direction = 6

        console(" 6 go D_move_direction", self.D_move_direction)

        if self.RM_player_location == 20 and self.D_move_direction == 5:
            self.D_move_direction = 1
        if self.RM_player_location == 20 and self.D_move_direction == 6:
            self.D_move_direction = 3
        if self.RM_player_location == 22 and self.D_move_direction == 6:
            self.D_move_direction = 2
        if self.RM_player_location == 22 and self.D_move_direction == 5:
            self.D_move_direction = 3
        if self.RM_player_location == 36 and self.D_move_direction == 6:
            self.D_move_direction = 1
        if self.RM_player_location == 36 and self.D_move_direction == 5:
            self.D_move_direction = 2

        if self.F_flag_obj[14] == 1:
            self.MS_player_msg = "CRASH! YOU FELL OUT OF THE TREE!"
            self.F_flag_obj[14] = 0
            return

        if self.F_flag_obj[27] == 1 and self.RM_player_location == 52:
            self.MS_player_msg = "GHOSTS WILL NOT LET YOU MOVE"
            return

        if self.RM_player_location == 45 and self.C_carrying_obj[0] == 1 and self.F_flag_obj[34] == 0:
            self.MS_player_msg = "A MAGICAL BARRIER TO THE WEST"
            return

        if (self.RM_player_location == 26 and self.F_flag_obj[0] == 0) and (self.D_move_direction == 1 or self.D_move_direction == 4):
            self.MS_player_msg = "YOU NEED A LIGHT"
            return

        # if you come by the wrong side, you get stuck
        if self.RM_player_location == 54 and self.C_carrying_obj[14] != 1:
            self.MS_player_msg = "YOU'RE STUCK IN THE MARSH, there is nowere to go"
            return

        # if you are in a boat, you can only go to 53, 54, 55, 47
        if self.C_carrying_obj[14] == 1 and not (self.RM_player_location == 53 or self.RM_player_location == 54 or self.RM_player_location == 55 or self.RM_player_location == 47):
            self.MS_player_msg = "you came so far, but YOU CAN'T CARRY A BOAT!"
            return

        if (self.RM_player_location > 26 and self.RM_player_location < 30) and self.F_flag_obj[0] == 0:
            self.MS_player_msg = "TOO DARK TO MOVE"
            return

        self.F_flag_obj[35] = 0
        exits = self.RS_route_desc[self.RM_player_location]
        chars = split(exits)

        # RL = len(self.RS_route_desc[self.RM_player_location])
        for US in chars:
            # U$ = MID$[self.RS_route_desc[self.RM_player_location],I,1]
            if US == "N" and self.D_move_direction == 1 and self.F_flag_obj[35] == 0:
                self.RM_player_location = self.RM_player_location - 8
                self.F_flag_obj[35] = 1
            if US == "S" and self.D_move_direction == 2 and self.F_flag_obj[35] == 0:
                self.RM_player_location = self.RM_player_location + 8
                self.F_flag_obj[35] = 1
            if US == "W" and self.D_move_direction == 3 and self.F_flag_obj[35] == 0:
                self.RM_player_location = self.RM_player_location - 1
                self.F_flag_obj[35] = 1
            if US == "E" and self.D_move_direction == 4 and self.F_flag_obj[35] == 0:
                self.RM_player_location = self.RM_player_location + 1
                self.F_flag_obj[35] = 1

        self.MS_player_msg = "OK"

        if self.F_flag_obj[35] == 0:
            self.MS_player_msg = "CAN'T GO THAT WAY"

        if self.D_move_direction < 1:
            self.MS_player_msg = "GO WHERE?"

        if self.RM_player_location == 41 and self.F_flag_obj[23] == 1:
            self.RS_route_desc[49] = "SW"
            self.MS_player_msg = "THE DOOR SLAMS SHUT!"
            self.F_flag_obj[23] = 0
        return

    #  REM GET, TAKE
    def get_980(self):
        if self.OB_object > self.G_n_get_objects:
            self.MS_player_msg = "I CAN'T GET " + self.WS_word_input
            return

        if self.L_location_obj[self.OB_object] != self.RM_player_location:
            self.MS_player_msg = "IT ISN'T HERE"

        if self.F_flag_obj[self.OB_object] != 0:
            self.MS_player_msg = "WHAT " + self.WS_word_input + "?"

        if self.C_carrying_obj[self.OB_object] == 1:
            self.MS_player_msg = "YOU ALREADY HAVE IT"

        if self.OB_object > 0 \
                and self.L_location_obj[self.OB_object] == self.RM_player_location \
                and self.F_flag_obj[self.OB_object] == 0:

            self.C_carrying_obj[self.OB_object] = 1
            self.L_location_obj[self.OB_object] = 65 #carrying obj
            self.MS_player_msg = "YOU HAVE THE " + self.WS_word_input
        return

    #  REM OPEN
    def open_1030(self):
        if self.RM_player_location == 43 and (self.OB_object == 27 or self.OB_object == 28):
            self.F_flag_obj[17] = 0
            self.MS_player_msg = "DRAWER OPEN"

        if self.RM_player_location == 28 and self.OB_object == 24:
            self.MS_player_msg = "IT'S LOCKED"

        if self.RM_player_location == 38 and self.OB_object == 31:
            self.MS_player_msg = "THAT'S CREEPY!"
            self.F_flag_obj[2] = 0
        return

    #  REM EXAMINE
    def examine_1070(self):
        if self.OB_object == 29:
            self.F_flag_obj[17] = 0
            self.MS_player_msg = "SOMETHING HERE!"

        if self.OB_object == 30:
            self.MS_player_msg = "THAT'S DISGUSTING!"

        if self.OB_object == 27 or self.OB_object == 28:
            self.MS_player_msg = "THERE IS A DRAWER"

        if self.OB_object == 32 or self.OB_object == 4:
            self.read_1140()

        if self.OB_object == 34 and self.RM_player_location == 43:
            self.MS_player_msg = "THERE IS SOMETHING BEYOND.."

        if self.OB_object == 31:
            self.open_1030()
        return

    #  REM READ
    def read_1140(self):
        if self.RM_player_location == 42 and self.OB_object == 33:
            self.MS_player_msg = "THEY ARE DEMONIC WORKS"
        if (self.OB_object == 3 or self.OB_object == 36) and self.C_carrying_obj[2] == 1 and self.F_flag_obj[34] == 0:
            self.MS_player_msg = "USE THIS WORD WITH CARE 'XZANFAR'"
        if self.C_carrying_obj[4] == 1 and self.OB_object == 5:
            self.MS_player_msg = "THE SCRIPT IS IN AN ALIEN TONGUE"
        return

    #  REM SAY
    def say_1180(self):
        self.MS_player_msg = "OK '" + self.WS_word_input + "'"
        if self.C_carrying_obj[2] == 1 and self.OB_object == 34:
            self.MS_player_msg = "*MAGIC OCCUR*"
            if self.RM_player_location != 45:
                rnd_num = rnd(63)
                self.RM_player_location = rnd_num
        if self.C_carrying_obj[2] == 1 and self.OB_object == 34 and self.RM_player_location == 45:
            self.F_flag_obj[34] = 1
        return

    #  REM DIG
    def dig_1220(self):
        if self.C_carrying_obj[11] == 1:
            self.MS_player_msg = "YOU MADE A HOLE"
        if self.C_carrying_obj[11] == 1 and self.RM_player_location == 30:
            self.MS_player_msg = "DUG THE BARS OUT"
            self.DS_local_desc[self.RM_player_location] = "HOLE IN WALL"
            self.RS_route_desc[self.RM_player_location] = "NSE"
        return

    #  REM SWING
    def swing_1250(self):
        if self.C_carrying_obj[13] != 1 and self.RM_player_location == 7:
            self.MS_player_msg = "THIS IS NO TIME TO PLAY GAMES"

        if self.OB_object == 13 and self.C_carrying_obj[13] == 1:
            self.MS_player_msg = "YOU SWUNG IT"

        if self.OB_object == 12 and self.C_carrying_obj[12] == 1:
            self.MS_player_msg = "WHOOSH!"

        if self.OB_object == 12 and self.C_carrying_obj[12] == 1 and self.RM_player_location == 43:
            self.RS_route_desc[self.RM_player_location] = "WN"
            self.DS_local_desc[self.RM_player_location] = "STUDY WITH SECRET ROOM"
            self.MS_player_msg = "YOU BROKE THE THIN WALL"
        return

    #  REM CLIMB
    def climb_1300(self):
        if self.OB_object == 14 and self.C_carrying_obj[13] == 1:
            self.MS_player_msg = "IT ISN'T ATTACHED TO ANYTHING!"
        if self.OB_object == 14 and self.C_carrying_obj[13] != 1 and self.RM_player_location == 7 and self.F_flag_obj[14] == 0:
            self.MS_player_msg = "YOU SEE THICK FOREST and CLIFF SOUTH"
            self.F_flag_obj[14] = 1
            return
        if self.OB_object == 14 and self.C_carrying_obj[13] != 1 and self.RM_player_location == 7 and self.F_flag_obj[14] == 1:
            self.MS_player_msg = "GOING DOWN"
            self.F_flag_obj[14] = 0
            return

    #  REM LIGHT
    def light_1340(self):
        # 16 candle , 8 matches , 7 candlestick
        if self.OB_object == 16 and self.C_carrying_obj[16] == 1 and self.C_carrying_obj[7] == 0:
            self.MS_player_msg = "IT WILL BURN YOUR HANDS"

        if self.OB_object == 16 and self.C_carrying_obj[16] == 1 and self.C_carrying_obj[8] == 0:
            self.MS_player_msg = "NOTHING TO LIGHT IT WITH"

        if self.OB_object == 16 and self.C_carrying_obj[16] == 1 and self.C_carrying_obj[8] == 1 and self.C_carrying_obj[7] == 1:
            self.MS_player_msg = "IT CASTS A FLICKERING LIGHT"
            self.F_flag_obj[0] = 1
        return

    #  REM UNLIGHT
    def unlight_1380(self):
        # candle
        if self.F_flag_obj[0] == 1:
            self.F_flag_obj[0] = 0
            self.MS_player_msg = "EXTINGUISHED"
        return

    #  REM SPRAY
    def spray_1400(self):
        # 15 aerosol
        if self.OB_object == 25 and self.C_carrying_obj[15] == 1:
            self.MS_player_msg = "HISSSS"

        if self.OB_object == 25 and self.C_carrying_obj[15] == 1 and self.F_flag_obj[26] == 1:
            self.F_flag_obj[26] = 0
            self.MS_player_msg = "PFFT! GOT THEM"
        return

    #  REM USE
    def use_1430(self):
        if self.OB_object == 10 and self.C_carrying_obj[10] == 1 and self.C_carrying_obj[11] == 1:
            self.MS_player_msg = "SWITCHED ON"
            self.F_flag_obj[24] = 1
        if self.F_flag_obj[27] == 1 and self.F_flag_obj[24] == 1:
            self.MS_player_msg = "WHIZZ- VACUUMED THE GHOSTS UP!"
            self.F_flag_obj[27] = 0
        return

    #  REM UNLOCK
    def unlock_1460(self):
        if self.RM_player_location == 43 and (self.OB_object == 27 or self.OB_object == 28):
            self.use_1430()
        if self.RM_player_location == 28 and self.OB_object == 25 and self.F_flag_obj[25] == 0 and self.C_carrying_obj[17] == 1:
            self.F_flag_obj[25] = 1
            self.RS_route_desc[self.RM_player_location] = "SEW"
            self.DS_local_desc[self.RM_player_location] = "HUGE OPEN DOOR"
            self.MS_player_msg = "THE TURNS!"
        return

    #  REM LEAVE
    def leave_1490(self):
        if self.C_carrying_obj[self.OB_object] == 1:
            self.C_carrying_obj[self.OB_object] = 0
            self.L_location_obj[self.OB_object] = self.RM_player_location
            self.MS_player_msg = "object drop"
        return

    #  REM SCORE
    def score_1510(self):
        S = 0
        for i in range(self.G_n_get_objects):
            if self.C_carrying_obj[i] == 1:
                S = S + 1
        if S == 17 and self.C_carrying_obj[15] != 1 and self.RM_player_location != 157:
            print("YOU HAVE EVERYTHING")
            print("RETURN TO THE GATE FOR FINAL SCORE")
        if S == 17 and self.RM_player_location == 57:
            print("DOUBLE SCORE FOR REACHING HERE")
            S = S * 2
            print("YOUR SCORE = ")
            print(S)
            if S > 18:
                print("WELL DONE! YOU FINISHED THE GAME")
                exit
        self.QS_user_input = input("PRESS return TO CONTINUE")
        return

    def save_game(self):
        game_state = [self.L_location_obj,]
        # open output file for writing
        with open('savedgame.bin', 'wb') as filehandle:
            pickle.dump(self.L_location_obj, filehandle)

        # with open('savedgame.json', 'w') as filehandle:
        #     json.dump(self.L_location_obj, filehandle)

        print('file saved')
        return

    def load_game(self):

        with open('savedgame.bin', 'rb') as filehandle:
            self.L_location_obj = pickle.load(filehandle)

        # with open('savedgame.json', 'r') as filehandle:
        #     self.L_location_obj = json.load(filehandle)

        print('file loaded')
        return

    def exit_game(self):
        print("bye!")

#end
