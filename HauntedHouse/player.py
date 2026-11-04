import pickle
import json
import constants
from world import World

class Player:
    def __init__(self):
        self.score = 0
        self.v_RM = 32  # position 57 orig , 0-63
        self.v_LL = 60  # candle

        # obj carrying
        self.v_C = [0] * constants.NUM_OBJECTS

        # Flags: 1 = hidden obj | 0 = discovered obj
        # F(0) = flag for candle on/of
        self.v_F = [0] * constants.v_W
        self.v_F[2] = 1     # spells
        self.v_F[17] = 1    # candle
        self.v_F[18] = 1    # key
        self.v_F[23] = 1    # up/down
        self.v_F[26] = 1    # bats
        self.v_F[28] = 1    # drawer

        # object location
        # only first 18 objects are gettable
        # first index 0 is ignored
        self.v_L = [ 0,
            46, 38, 35, 50, 13,
            18, 28, 42, 10, 25,
            26, 4 ,  2,  7, 47,
            60, 43, 32
            ]

    def save_game_bin(self):
        game_state = [self.v_RM, self.v_L, self.v_C, self.v_F]

        with open('.\\data\\savedgame.bin', 'wb') as filehandle:
            pickle.dump(game_state, filehandle)

        print('file saved')
        return

    def load_game_bin(self):

        with open('.\\data\\savedgame.bin', 'rb') as filehandle:
            self.v_RM, self.v_L, self.v_C, self.v_F = pickle.load(filehandle)

        print('file loaded')
        return

    def save_game(self, num):
        game_state = [self.v_RM, self.v_L, self.v_C, self.v_F]

        fname = ".\\data\\savedgame-"+ str(num) +".json"
        with open(fname, 'w') as filehandle:
            json.dump(game_state, filehandle)

        print("] saved game as: " + fname)
        return

    def load_game(self, num):
        fname = ".\\data\\savedgame-"+ str(num) +".json"
        print(fname)
        with open(fname, 'r') as filehandle:
            data = json.load(filehandle)

        self.v_RM = data[0]
        self.v_L = data[1]
        self.v_C = data[2]
        self.v_F = data[3]

        print("] loaded game: " + fname)
        return

    def show_objects(self, playerLocation):
        for i in range(constants.v_G):
            if self.v_L[i] == playerLocation and self.v_F[i] == 0:
                print("> here you can see : ", constants.v_OS[i])

    #  REM SCORE
    def score_1510(self):
        S = 0
        for i in range(constants.v_G):
            if self.v_C[i] == 1:
                S = S + 1
        if S == 17 and self.v_C[15] != 1 and self.v_RM != 57:
            print("YOU HAVE EVERYTHING")
            print("RETURN TO THE GATE FOR FINAL SCORE")
        if S == 17 and self.v_RM == 57:
            print("DOUBLE SCORE FOR REACHING HERE")
            S = S * 2
        print("] YOUR SCORE = %s out of 17" % S)

        self.score = S
        if S > 18:
            print("WELL DONE! YOU FINISHED THE GAME")
            exit
        self.v_QSi = input("PRESS return TO CONTINUE")
        return

    #  REM INVENTORY
    def show_inventory(self):
        print("YOU ARE CARRYING : ")

        # crate a list
        objList = []
        carrying = []
        for i in range(constants.v_G):
            #print(self.v_C[i], self.v_OS[i])
            s = str(self.v_C[i]) +" - "+ constants.v_OS[i]
            objList.append(s)
            if self.v_C[i] == 1:
                carrying.append(constants.v_OS[i])
        print(carrying)
        print(objList)
        print("-------------------------------------------------")
        self.v_MS = ""
        return
