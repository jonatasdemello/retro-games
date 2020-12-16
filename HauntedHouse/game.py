"""
#  REM HAUNTED HOUSE ADVENTURE
#  REM ***********************
#  REM THIS VERSION FOR "MICROSOFT" BASIC
#  REM *******************************
"""
from world import World
from player import Player
from user_input import User_Input
import util
import constants
from colorama import Fore, Back, Style

class Game:
    def __init__(self):
        self.myWorld = World()
        self.myPlayer = Player()
        self.myInput = User_Input()

    # game starts here
    def play(self):
        self.show_intro()

        while True:
            playerLocation = self.myPlayer.v_RM
            self.myWorld.show_location(playerLocation)
            self.myWorld.show_exits(playerLocation)
            self.myPlayer.show_objects(playerLocation)
            self.myInput.print_response()

            self.myInput.read_input()
            self.perform_action()

            self.debug()

            if self.myInput.v_QSi =='QUIT' or self.myInput.v_QSi =='EXIT':
                self.exit_game()
                break

    def show_intro(self):
        intro =  "\n"
        intro += "-------------------------------------------------\n"
        intro += "--------------- HAUNTED HOUSE -------------------\n"
        intro += "-------------------------------------------------\n"
        print(intro)

    def perform_action(self):
        util.trace("-------------------------------------------------")
        util.trace("...perform action:")

#         rnd_num = util.rnd(4)
#         if self.v_F[26] == 1 and self.v_RM == 13 and rnd_num != 3 and self.v_VB != 21:
#             self.v_MS = "BATS ATTACKING!"
#             return
#
#         rnd_num = util.rnd(2)
#         if self.v_RM == 44 and rnd_num == 1 and self.v_F[24] != 1:
#             self.v_F[27] = 1
#
#         # candle on/off
#         if self.v_F[0] == 1:
#             self.v_LL = self.v_LL - 1
#         if self.v_LL < 1:
#             self.v_F[0] = 0
#
#         if self.v_LL == 10:
#             self.v_MS = "YOUR CANDLE IS WANING!"
#         if self.v_LL == 1:
#             self.v_MS = "YOUR CANDLE IS OUT!"


        util.trace(" 5 perform_action v_VB ", self.myInput.v_VB)

        verb_action = self.myInput.v_VB
        obj_action = self.myInput.v_OB

        if verb_action == 0:
            self.show_help()
        elif verb_action == 1:
            self.myPlayer.show_inventory()
        elif verb_action in [2,3,4,5,6,7,8]:
            self.myInput.action_move(self.myPlayer, self.myWorld)
        elif verb_action == 9:
            self.get_980()
        elif verb_action == 10:
            self.get_980()
        elif verb_action == 11:
            self.open_1030()
        elif verb_action == 12:
            self.examine_1070()
        elif verb_action == 13:
            self.read_1140()
        elif verb_action == 14:
            self.say_1180()
        elif verb_action == 15:
            self.dig_1220()
        elif verb_action == 16:
            self.swing_1250()
        elif verb_action == 17:
            self.climb_1300()
        elif verb_action == 18:
            self.light_1340()
        elif verb_action == 19:
            self.unlight_1380()
        elif verb_action == 20:
            self.spray_1400()
        elif verb_action == 21:
            self.use_1430()
        elif verb_action == 22:
            self.unlock_1460()
        elif verb_action == 23:
            self.leave_1490()
        elif verb_action == 24:
            self.myPlayer.score_1510()
        elif verb_action == 25:
            self.exit_game()
        elif verb_action == 26:
            self.exit_game()
        elif verb_action == 27:
            self.debug()
        elif verb_action == 28:
            self.myPlayer.save_game(obj_action)
        elif verb_action == 29:
            self.myPlayer.load_game(obj_action)

        util.trace("...perform action:")
        util.trace("-------------------------------------------------")
        return

    # actions
    # actions

    #  REM GET, TAKE
    def get_980(self):
        # can get only 0-17
        if self.myInput.v_OB > constants.v_G:
            self.myInput.v_MS = "I CAN'T GET " + self.myInput.v_WSi
            return

        if self.myPlayer.v_L[self.myInput.v_OB] != self.myPlayer.v_RM:
            self.myInput.v_MS = "THERE IS NO " + self.myInput.v_WSi + " HERE"

        if self.myPlayer.v_F[self.myInput.v_OB] != 0:
            self.myInput.v_MS = "WHAT " + self.myInput.v_WSi + "?"

        if self.myPlayer.v_C[self.myInput.v_OB] == 1:
            self.myInput.v_MS = "YOU ALREADY HAVE IT"

        if self.myInput.v_OB > 0 and self.myPlayer.v_L[self.myInput.v_OB] == self.myPlayer.v_RM and self.myPlayer.v_F[self.myInput.v_OB] == 0:
            self.myPlayer.v_C[self.myInput.v_OB] = 1
            self.myPlayer.v_L[self.myInput.v_OB] = 65 #carrying obj
            self.myInput.v_MS = "YOU HAVE THE " + self.myInput.v_WSi
        return

    #  REM OPEN
    def open_1030(self):
        # F(17) key
        if self.myPlayer.v_RM == 43 and (self.myInput.v_OB == 28 or self.myInput.v_OB == 29):
            self.myPlayer.v_F[17] = 0
            self.myInput.v_MS = "DRAWER OPEN"

        # 24 door
        if self.myPlayer.v_RM == 28 and self.myInput.v_OB == 25:
            self.myInput.v_MS = "IT'S LOCKED"

        # 31 coffin
        if self.myPlayer.v_RM == 38 and self.myInput.v_OB == 32:
            self.myInput.v_MS = "THAT'S CREEPY!"
            self.myPlayer.v_F[2] = 0
        return

    #  REM EXAMINE
    def examine_1070(self):
        # set default response
        self.myInput.v_MS = "nothing special, just a normal " + constants.v_OS[self.myInput.v_OB]
        if self.myInput.v_OB == 0:
            self.myInput.v_MS = "Examine what?"
            return

        #if self.v_VB < self.v_V and self.myInput.v_OB > 0 and self.myPlayer.v_C[self.myInput.v_OB] == 0:
        # if self.myPlayer.v_C[self.myInput.v_OB] == 0 \
        #         and self.myPlayer.v_L[self.myInput.v_OB] != self.myPlayer.v_RM:
        #     self.myInput.v_MS = "YOU DON'T HAVE " + self.myInput.v_WSi
        #     return

        # coat
        if self.myInput.v_OB == 30:
            self.myPlayer.v_F[18] = 0 # key
            self.myInput.v_MS = "SOMETHING HERE!"

        # rubbish
        if self.myInput.v_OB == 31:
            self.myInput.v_MS = "THAT'S DISGUSTING!"

        # drawer, desk
        if self.myInput.v_OB == 28 or self.myInput.v_OB == 29:
            self.myInput.v_MS = "THERE IS A DRAWER"

        # book, scroll
        if self.myInput.v_OB == 33 or self.myInput.v_OB == 5:
            self.read_1140()

        # wall
        if self.myInput.v_OB == 35 and self.myPlayer.v_RM == 43:
            self.myInput.v_MS = "THERE IS SOMETHING BEYOND.."

        # coffin
        if self.myInput.v_OB == 32:
            self.open_1030()
        return

    #  REM READ
    def read_1140(self):
        # books
        if self.myPlayer.v_RM == 42 and self.myInput.v_OB == 33:
            self.myInput.v_MS = "THEY ARE DEMONIC WORKS"

        # spells
        if (self.myInput.v_OB == 3 or self.myInput.v_OB == 36) and self.myPlayer.v_C[3] == 1 and self.myPlayer.v_F[34] == 0:
            self.myInput.v_MS = "USE THIS WORD WITH CARE 'XZANFAR'"

        # scroll
        if self.myPlayer.v_C[5] == 1 and self.myInput.v_OB == 5:
            self.myInput.v_MS = "THE SCRIPT IS IN AN ALIEN TONGUE"
        return

    #  REM SAY
    def say_1180(self):
        self.myInput.v_MS = "OK '" + self.myInput.v_WSi + "'"
        # XZANFAR
        if self.myPlayer.v_C[3] == 1 and self.myInput.v_OB == 34:
            self.myInput.v_MS = "* MAGIC OCCUR *"
            if self.myPlayer.v_RM != 45:
                rnd_num = util.rnd(63)
                self.myPlayer.v_RM = rnd_num
        # XZANFAR
        if self.myPlayer.v_C[3] == 1 and self.myInput.v_OB == 34 and self.myPlayer.v_RM == 45:
            self.myPlayer.v_F[34] = 1
        return

    #  REM DIG
    def dig_1220(self):
        # shovel
        if self.myPlayer.v_C[12] == 1:
            self.myInput.v_MS = "YOU MADE A HOLE"
        # shovel
        if self.myPlayer.v_C[12] == 1 and self.myPlayer.v_RM == 30:
            self.myInput.v_MS = "DUG THE BARS OUT"
            self.myWorld.v_DS[self.myPlayer.v_RM] = "HOLE IN WALL"
            self.myWorld.v_RS[self.myPlayer.v_RM] = "NSE"
        return

    #  REM SWING
    def swing_1250(self):
        # rope
        if self.myPlayer.v_C[14] != 1 and self.myPlayer.v_RM == 7:
            self.myInput.v_MS = "THIS IS NO TIME TO PLAY GAMES"

        # rope
        if self.myInput.v_OB == 14 and self.myPlayer.v_C[14] == 1:
            self.myInput.v_MS = "YOU SWUNG IT"

        # axe
        if self.myInput.v_OB == 13 and self.myPlayer.v_C[13] == 1:
            self.myInput.v_MS = "WHOOSH!"

        # axe
        if self.myInput.v_OB == 13 and self.myPlayer.v_C[13] == 1 and self.myPlayer.v_RM == 43:
            self.myWorld.v_RS[self.myPlayer.v_RM] = "WN"
            self.myWorld.v_DS[self.myPlayer.v_RM] = "STUDY WITH SECRET ROOM"
            self.myInput.v_MS = "YOU BROKE THE THIN WALL"
        return

    #  REM CLIMB
    def climb_1300(self):
        # rope
        if self.myInput.v_OB == 14 and self.myPlayer.v_C[14] == 1:
            self.myInput.v_MS = "rope IT ISN'T ATTACHED TO ANYTHING!"

        if self.myInput.v_OB == 14 and self.myPlayer.v_C[14] != 1 and self.myPlayer.v_RM == 7 and self.myPlayer.v_F[14] == 0:
            self.myInput.v_MS = "YOU SEE THICK FOREST and CLIFF SOUTH"
            self.myPlayer.v_F[14] = 1
            return

        if self.myInput.v_OB == 14 and self.myPlayer.v_C[14] != 1 and self.myPlayer.v_RM == 7 and self.myPlayer.v_F[14] == 1:
            self.myInput.v_MS = "GOING DOWN"
            self.myPlayer.v_F[14] = 0
            return

    #  REM LIGHT
    def light_1340(self):
        # 16 candle , 7 candlestick, 8 matches
        if self.myInput.v_OB == 17 and self.myPlayer.v_C[17] == 1 and self.myPlayer.v_C[8] == 0:
            self.myInput.v_MS = "IT WILL BURN YOUR HANDS"

        if self.myInput.v_OB == 17 and self.myPlayer.v_C[17] == 1 and self.myPlayer.v_C[9] == 0:
            self.myInput.v_MS = "NOTHING TO LIGHT IT WITH"

        if self.myInput.v_OB == 17 and self.myPlayer.v_C[17] == 1 and self.myPlayer.v_C[9] == 1 and self.myPlayer.v_C[8] == 1:
            self.myInput.v_MS = "IT CASTS A FLICKERING LIGHT"
            self.myPlayer.v_F[0] = 1 # on/off
        return

    #  REM UNLIGHT
    def unlight_1380(self):
        # candle
        if self.myPlayer.v_F[0] == 1:
            self.myPlayer.v_F[0] = 0  # on/off
            self.myInput.v_MS = "EXTINGUISHED"
        return

    #  REM SPRAY
    def spray_1400(self):
        # 15 aerosol
        if self.myInput.v_OB == 26 and self.myPlayer.v_C[16] == 1:
            self.myInput.v_MS = "HISSSS"

        if self.myInput.v_OB == 26  and self.myPlayer.v_C[16] == 1 and self.myPlayer.v_F[26] == 1:
            self.myPlayer.v_F[26] = 0
            self.myInput.v_MS = "PFFT! GOT THEM"
        return

    #  REM USE
    def use_1430(self):
        # 9 vacuum, 10 batteries
        if self.myInput.v_OB == 10 and self.myPlayer.v_C[10] == 1 and self.myPlayer.v_C[11] == 1:
            self.myInput.v_MS = "SWITCHED ON"
            self.myPlayer.v_F[24] = 1
        # 26 ghosts
        if self.myPlayer.v_F[27] == 1 and self.myPlayer.v_F[24] == 1:
            self.myInput.v_MS = "WHIZZ- VACUUMED THE GHOSTS UP!"
            self.myPlayer.v_F[27] = 0

        # axe
        if self.myInput.v_OB == 13 and self.myPlayer.v_C[13] == 1:
            self.myInput.v_MS = "WHOOSH!"
        # axe
        if self.myInput.v_OB == 13 and self.myPlayer.v_C[13] == 1 and self.myPlayer.v_RM == 43:
            self.myWorld.v_RS[self.myPlayer.v_RM] = "WN"
            self.myWorld.v_DS[self.myPlayer.v_RM] = "STUDY WITH SECRET ROOM"
            self.myInput.v_MS = "YOU BROKE THE THIN WALL"

        return

    #  REM UNLOCK
    def unlock_1460(self):
        if self.myPlayer.v_RM == 43 and (self.myInput.v_OB == 27 or self.myInput.v_OB == 28):
            self.use_1430()

        if self.myPlayer.v_RM == 28 and self.myInput.v_OB == 25 and self.myPlayer.v_F[25] == 0 and self.myPlayer.v_C[18] == 1:
            self.myPlayer.v_F[25] = 1
            self.myWorld.v_RS[self.myPlayer.v_RM] = "SEW"
            self.myWorld.v_DS[self.myPlayer.v_RM] = "HUGE OPEN DOOR"
            self.myInput.v_MS = "THE KEY TURNS!"
        return

    #  REM LEAVE
    def leave_1490(self):
        if self.myPlayer.v_C[self.myInput.v_OB] == 1:
            self.myPlayer.v_C[self.myInput.v_OB] = 0
            self.myPlayer.v_L[self.myInput.v_OB] = self.myPlayer.v_RM
            self.myInput.v_MS = "object drop"
        return


    #  REM HELP
    def show_help(self):
        print(Fore.YELLOW + "-------------------------------------------------")
        print("WORDS I KNOW : ")
        # print(self.v_VS)
        for a,b,c in zip(constants.v_VS[::3], constants.v_VS[1::3], constants.v_VS[2::3]):
            print('{:<20}{:<20}{:<}'.format(a,b,c))
        print("-------------------------------------------------" + Style.RESET_ALL)
        self.v_MS = ""
        return

    def exit_game(self):
        print("bye!")

    def debug(self):
        util.console(" ------------------- debug -------------------")
        util.console(" v_D: ", self.myInput.v_D)
        util.console(" v_MS: ", self.myInput.v_MS)
        util.console(" v_QSi: ", self.myInput.v_QSi)
        util.console(" v_VSi: ", self.myInput.v_VSi)
        util.console(" v_WSi: ", self.myInput.v_WSi)
        util.console(" v_VB: ", self.myInput.v_VB)
        util.console(" v_OB: ", self.myInput.v_OB)

        util.console(" v_V: ", constants.v_V)
        util.console(" v_G: ", constants.v_G)
        util.console(" v_W: ", constants.v_W)

        util.console(" v_RM: ", self.myPlayer.v_RM)

        util.console_list(" v_L: ", self.myPlayer.v_L)
        util.console_list(" v_C: ", self.myPlayer.v_C)
        util.console_list(" v_F: ", self.myPlayer.v_F)
        util.console(" ------------------- debug -------------------")
#end

MyGame = Game()
MyGame.play()