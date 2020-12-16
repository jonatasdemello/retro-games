import util
import constants
from world import World
from player import Player

class User_Input:
    def __init__(self):
        self.v_MS = ""  # user msg
        self.v_QSi = "" # user input
        self.v_VSi = "" # user input
        self.v_WSi = "OK" # user input

        self.v_VB = 0  # verb num
        self.v_OB = 0  # obj num
        self.v_D = 0   # direction 1-6 "NSEWUD"

    def get_input(self):
        # read user input
        result = input("WHAT WILL YOU DO NOW ? ")
        self.v_QSi = result.upper()
        return result

    def decode_input(self, text=""):
        self.v_MS = ""
        self.v_VSi = ""
        self.v_WSi = ""
        self.v_VB = 0
        self.v_OB = 0
        self.v_D = 0

        if text != "" and len(text) > 0:
            text = text.upper()
            text = text.strip(' ')
            self.v_QSi = text

        # show help if empty input
        if not self.v_QSi:
            self.v_QSi = "HELP"

        # split input: verb + word
        words = self.v_QSi.split()
        if len(words) == 1:
            self.v_VSi = words[0]
            self.v_WSi = "" # words[0]
        else:
            self.v_VSi = words[0]
            self.v_WSi = words[1]

        util.trace("-------------------------------------------------")
        util.trace("...read input:")
        util.trace(" 1 read_input v_VSi", self.v_VSi) # verb

        # get the verb number
        if self.v_VSi in constants.v_VS:
            self.v_VB = constants.v_VS.index(self.v_VSi)

        util.trace(" 2 read_input v_VB", self.v_VB) # verb number
        util.trace(" 3 read_input v_WSi", self.v_WSi) # word

        # get the object number
        if self.v_WSi in constants.v_OS:
            self.v_OB = constants.v_OS.index(self.v_WSi)

        util.trace(" 4 read_input v_OB", self.v_OB) # object number


        # now add user message
        #self.v_MS = "WHAT?"
        if self.v_VB == 0 and self.v_OB == 0:
            self.v_MS = "THAT'S SILLY, I don't understand"

        if self.v_VB > 8 and self.v_WSi == "":
            self.v_MS = "I NEED TWO WORDS"

        if self.v_VB > constants.v_V and self.v_OB > 0:
            self.v_MS = "YOU CAN'T [" + self.v_QSi + "]"

        if self.v_VB == 0 and self.v_OB > 0:
            self.v_MS = "YOU DON'T MAKE SENSE"

        # this should be somewhere else, it needs to validate carrying objects
        # if self.v_VB < constants.v_V and self.v_OB > 0 \
        #         and self.v_C[self.v_OB] == 0:
        #     self.v_MS = "YOU DON'T HAVE " + self.v_WSi

        util.trace(" 5 read_input v_MS", self.v_MS)
        util.trace("...read input:")
        util.trace("-------------------------------------------------")

    def read_input(self):
        self.get_input()
        self.decode_input()

    def print_response(self):
        # print message/response
        print("-------------------------------------------------")
        print("] Response: ")
        print("] ", self.v_MS)
        print("-------------------------------------------------")
        print(" ")

    #  REM GO N:3 S:4 W:5 E:6 U:7 D:8
    def action_move(self, player, world):
        self.v_D = 0

        util.trace("-------------------------------------------------")
        util.trace(" 6 go v_VB", self.v_VB)
        util.trace(" 6 go v_OB", self.v_OB)

        # vb = self.v_VB
        # ob = self.v_OB
        # playerLocation = player.v_RM

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

        util.trace(" 6 go v_D", self.v_D)

        if player.v_RM == 20 and self.v_D == 5: # up
            self.v_D = 1
        if player.v_RM == 20 and self.v_D == 6: # down
            self.v_D = 3
        if player.v_RM == 22 and self.v_D == 6: # down
            self.v_D = 2
        if player.v_RM == 22 and self.v_D == 5: # up
            self.v_D = 3
        if player.v_RM == 36 and self.v_D == 6: # down
            self.v_D = 1
        if player.v_RM == 36 and self.v_D == 5: # up
            self.v_D = 2

        # conditional checks:
        # rope
        if player.v_F[14] == 1:
            self.v_MS = "CRASH! YOU FELL OUT OF THE TREE!"
            player.v_F[14] = 0
            return

        # ghosts
        if player.v_F[27] == 1 and player.v_RM == 52:
            self.v_MS = "GHOSTS WILL NOT LET YOU MOVE"
            return

        # XZANFAR
        if player.v_RM == 45 and player.v_C[1] == 1 and player.v_F[34] == 0:
            self.v_MS = "A MAGICAL BARRIER TO THE WEST"
            return

        # F(0) candle
        if (player.v_RM == 26 and player.v_F[0] == 0) and (self.v_D == 1 or self.v_D == 4):
            self.v_MS = "YOU NEED A LIGHT"
            return

        # if you come by the wrong side, you get stuck
        if player.v_RM == 54 and player.v_C[15] != 1:
            self.v_MS = "YOU'RE STUCK IN THE MARSH, there is nowere to go"
            return

        # if you are in a boat, you can only go to 53, 54, 55, 47
        if player.v_C[15] == 1 and not (player.v_RM == 53 or player.v_RM == 54 or player.v_RM == 55 or player.v_RM == 47):
            self.v_MS = "you came so far, but YOU CAN'T CARRY A BOAT!"
            return

        # F(0) candle
        if (player.v_RM > 26 and player.v_RM < 30) and player.v_F[0] == 0:
            self.v_MS = "TOO DARK TO MOVE"
            return

        # wall?
        player.v_F[35] = 0

        exits = world.v_RS[player.v_RM]
        chars = util.split(exits)
        for US in chars:
            if US == "N" and self.v_D == 1 and player.v_F[35] == 0:
                player.v_RM = player.v_RM - 8
                player.v_F[35] = 1
            if US == "S" and self.v_D == 2 and player.v_F[35] == 0:
                player.v_RM = player.v_RM + 8
                player.v_F[35] = 1
            if US == "W" and self.v_D == 3 and player.v_F[35] == 0:
                player.v_RM = player.v_RM - 1
                player.v_F[35] = 1
            if US == "E" and self.v_D == 4 and player.v_F[35] == 0:
                player.v_RM = player.v_RM + 1
                player.v_F[35] = 1
        self.v_MS = "OK"

        # wall?
        if player.v_F[35] == 0:
            self.v_MS = "CAN'T GO THAT WAY"

        if self.v_D < 1:
            self.v_MS = "GO WHERE?"

        if player.v_RM == 41 and player.v_F[23] == 1:
            world.v_RS[49] = "SW"
            self.v_MS = "THE DOOR SLAMS SHUT!"
            player.v_F[23] = 0 # up/down
        return