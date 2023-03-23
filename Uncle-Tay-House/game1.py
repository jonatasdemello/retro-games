import random

# 10 REM *** UNCLE TAYS HOUSE ADVENTURE ***
# REM *** VARIABLES ***
# DIM INWS[10]
# DIM INPTK[10]
# DIM VOCABS[60]
# DIM NULLWS[4)
# DIM IDESC$(30)
# DIM ILOC[30]
# DIM RNAMES[31]
# DIM RDESCS[31]
# DIM REXIT[31][6]
# DIM EXLOC[10][2]
# DIM EXDESCS[10]

DIROFF = 30
ITEMOFF = 33
LASTITEM = 27
IMMOFF = 20
# IMOFF ?? never declared

LWRD = 60
NXDESC = 7
SAFED = 0
LCL = 1
IC = 0
SP = 0
D2S = ""
SCORE = 50

INWRD = 0
WIDX = 0

COMM = 0
ARG = 0
MVARG = 0

# DIM VOCABS[60]
VOCABS = [
    "NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN", "N", "S", "E", "W", "U", "D",
    "I", "INVENTORY", "SCORE", "JUMP", "HELP",
    "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN",
    "MOVE", "OPEN", "TIE", "OIL", "PUT", "LEFT", "CENTER", "RIGHT",
    "NEWSPAPER", "TEDDYBEAR", "FUSE", "JACK", "PICTURE", "BUNGEE",
    "KEY", "TOP", "NOTE", "GAINESBURGER", "GLOVES", "BOXSPRING",
    "BRACE", "MAGAZINE", "OILCAN", "CHECKBOOK", "DIAMOND", "LOVERBOY",
    "INVESTMENT", "LOONS", "FRIDGE", "COUCH", "CLOTHES", "DOOR",
    "RAILING", "DUMBWAITER", "FUSEBOX"
]

print("VOCABS", len(VOCABS))
print(" VOCABS[17]", VOCABS[17])

# DIM NULLWS[4)
NULLWS = ["THE", "TO", "WITH", "USING"]

print("NULLWS", len(NULLWS))

# DIM IDESC$(30)
IDESCS = [
    "TAYS HOUSE UNLIKELY EVER TO BE SOLD.  TALES OF GUTTED STAIRWELLS and BOOBY TRAPS HAVE SPOOKED BUYERS...",
    "SOMEONE HAS BEEN PLAYING VERY ROUGH WITH THIS TOY",
    "OLD-FASHIONED ELECTRICAL FUSE",
    "TIRE JACK for LIFTING HEAVY OBJECTS LIKE CARS",
    "UNCLE TAYS IN ALL HIS SALLOW GLORY",
    "CORD for BUNGEE JUMPING",
    "A SMALL BRASS KEY",
    "A CHILD'S TOY",
    "THE WRITING IS REVERSED",
    "SUPPOSEDLY DOG FOOD, THOUGH IT APPEARS TO BE MADE OF PLASTIC",
    "RUBBER GLOVES USED for CLEANING",
    "A QUEEN-SIZED BOXSPRING",
    "A BACK BRACE",
    "TAYS' STRANGE INVENTIONS INCLUDE BOOBY-TRAPPED DOORS and TOYS THAT OPEN DOORS BY REMOTE CONTROL...",
    "THIS CAN CONTAINS FINE LUBRICATING OIL",
    "UNCLE TAYS' CHECKBOOK LISTS A BALANCE OF $220,000",
    "THIS DIAMOND'S BEAUTY STEMS FROM ALL THE GODDAMNED MONEY IT IS WORTH",
    "LOVERBOY'S FIRST ALBUM IN VINYL, WORTH AN INCALCULABLE SUM",
    "PRE-IPO SHARES OF APOLLO COMPUTING HAVE TO BE WORTH ... SOMETHING",
    "A THICK WAD OF CANADIAN NOTES",
    "THIS OLD REFRIGERATOR'S MOTOR LABORS HEAVILY",
    "AN OVERSTUFFED, DUSTY COUCH",
    "A DISGUSTING PILE OF SOILED LAUNDRY",
    "",
    "",
    "",
    "AN OLD-FASHIONED FUSEBOX.  THE FUSE MARKED 'ATTIC' IS MISSING."
]

print("IDESCS", len(IDESCS))

# DIM RNAMES[31]
RNAMES = [
    "FOYER",
    "KITCHEN",
    "SITTING ROOM",
    "HALLWAY",
    "HALLWAY",
    "DEN",
    "BATHROOM",
    "LIBRARY",
    "SMALL BEDROOM",
    "GARAGE",
    "DINING ROOM",
    "BALCONY",
    "MASTER BEDROOM",
    "HALLWAY",
    "GAME ROOM",
    "CLOSET",
    "HALLWAY",
    "CHILD'S ROOM",
    "SECRET ROOM1",
    "DANGEROUS HALL",
    "CORNER BEDROOM",
    "BATHROOM",
    "DUMBWAITER",
    "DUMBWAITER",
    "ATTIC",
    "STORAGE ROOM",
    "LAUNDRY",
    "WORKROOM",
    "BOTTOM OF STAIRS",
    "MID-AIR",
    "LEAVE THE HOUSE (AND THE GAME)"
]

print("RNAMES", len(RNAMES))

# DIM RDESCS[31]
RDESCS = [
    "THE ENTRYWAY TO THE HOUSE",
    "COUNTERTOPS ARE DUSTY and THERE ARE RUSTING POTS and PANS",
    "THIS ROOM IS TWO STORIES HIGH and CONTAINS ELEGANT CHAIRS and COUCHES",
    "A NARROW HALLWAY WHICH RUNS WEST OF THE FOYER",
    "A NARROW HALLWAY AT THE WEST END OF THE HOUSE",
    "THIS ROOM HAS AN ANCIENT TELEVISION",
    "A DINGY BATHROOM WITH A CRACKED SINK",
    "THIS WELL-FURNISHED LIBRARY IS LINED WITH BOOKS and LEATHER FURNITURE",
    "THIS SMALL BEDROOM HAS A TWIN BED and CHAIR.  IT LOOKS LITTLE USED.",
    "THE CAVERNOUS GARAGE HOLDS A NON-OPERATIONAL GREMLIN and PILES OF JUNK",
    "TROPHIES LINE THE WALLS.  THERE ARE SIX CHAIRS AROUND A LONG TABLE.",
    "BALCONY ABOVE THE SITTING ROOM.  A RAILING PROTECTS YOU FROM A 15-FOOT DROP.",
    "THIS LARGE CORNER BEDROOM HAS SOLID WALNUT FURNITURE and A LARGE MIRROR",
    "A HALLWAY WITH A LARGE ARCH ON ITS SOUTH SIDE",
    "THIS ELEGANT GAME ROOM HAS A POOL TABLE and MARBLE CHESSBOARD",
    "A SPACIOUS CLOSET OFF THE GAMEROOM",
    "A HALLWAY IN THE CENTER OF THE SECOND FLOOR",
    "YOUR COUSIN'S ROOM IN HAPPIER TIMES, BEFORE HE RAN OFF TO JOIN THE BAATH PARTY",
    "A DARK CHAMBER OFF THE BEDROOM",
    "THIS EERIE HALL HAS THREE IDENTICAL DOORS ON THE WEST WALL",
    "A COZY CORNER ROOM WITH WINDOWS ON TWO WALLS",
    "AN ELEGANT BATH WITH A MIRROR OVER A MARBLE SINK",
    "A CRAMPED DUMBWAITER",
    "A CRAMPED DUMBWAITER",
    "A DUSTY ATTIC WITH LOW SLOPING WALLS",
    "A BARE ROOM USED TO STORE RANDOM EQUIPMENT and FURNITURE",
    "THIS ROOM HAS A WASHER and DRYER, AS WELL AS A BOILER and FURNACE",
    "EQUIPMENT for WORKING WOOD and METAL",
    "STAIRS FROM BASEMENT TO KITCHEN",
    "HANGING FROM A BUNGEE CORD",
    ""
]
print("RDESCS", len(RDESCS))

# DIM REXIT[31][6]
REXIT = [
    [ 2 , 31, 3 , 4 , 0 , 0  ],
    [ 0 , 1 , 0 , 0 , 0 , -1 ],
    [ 2 , 0 , 11, 1 , 12, 0  ],
    [ 6 , 7 , 1 , 5 , 0 , 0  ],
    [ 0 , 9 , 4 , 10, 0 , 0  ],
    [ 0 , 4 , 0 , 0 , 0 , 0  ],
    [ 4 , 0 , 0 , 0 , 0 , -1 ],
    [ 0 , 0 , 0 , 24, 0 , 0  ],
    [ 5 , 0 , 0 , 0 , 0 , 0  ],
    [ 0 , 0 , 5 , 0 , 0 , 0  ],
    [ 0 , 0 , 0 , 3 , 0 , 0  ],
    [ 13, 0 , 0 , 14, -1, 3  ],
    [ 0 , 12, 0 , 0 , 0 , 0  ],
    [ 0 , 15, 12, 17, 0 , 0  ],
    [ 14, 0 , 0 , 16, 0 , 0  ],
    [ 0 , 0 , 15, 0 , 0 , 0  ],
    [ -1, 0 , 14, -1, 0 , 0  ],
    [ 0 , 17, -1, 0 , 0 , 0  ],
    [ 0 , 0 , 0 , 18, 0 , 0  ],
    [ 21, 22, 17, -1, 0 , 0  ],
    [ 0 , 20, 0 , 0 , 0 , 0  ],
    [ 20, 0 , 0 , 0 , 0 , 0  ],
    [ 0 , 0 , 20, 0 , 0 , -1 ],
    [ 0 , 0 , 8 , 0 , 23, 0  ],
    [ 0 , 0 , 0 , 0 , 0 , 12 ],
    [ 0 , 0 , 27, 0 , 0 , 0  ],
    [ 0 , 0 , 28, 26, 0 , 0  ],
    [ 0 , 0 , 29, 27, 0 , 0  ],
    [ 0 , 0 , 0 , 28, -1, 0  ],
    [ 0 , 0 , 0 , 0 , 0 , 0  ],
    [ 0 , 0 , 0 , 0 , 0 , 0  ]
]
print("REXIT", len(REXIT))
print(" REXIT[0]", len(REXIT[0]))

# DIM ILOC[30]
ILOC = [
    1, -1, -1, 10, 30, 10, 13, 15, 9, 16,
    22, 26, 25, 25, 28, 8, -1, 19, 21, 27,
    2, 6, 7, -1, 12, -1, -1
]
print("ILOC", len(ILOC))

# DIM EXDESCS[10]
EXDESCS =[
    "THERE IS A LOCKED DOOR TO THE NORTH",
    "THERE IS A LOCKED DOOR TO THE SOUTH",
    "STAIRS LEAD DOWN TO A CELLAR.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
    "STAIRS LEAD UP.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
    "DARK STAIRS LEAD UP TO THE ATTIC",
    "A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'",
    "YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH"
]
print("EXDESCS", len(EXDESCS))

# DIM EXLOC[10][2]
EXLOC = [
    [ 5 , 6],
    [ 8 , 6],
    [ 2 , 6],
    [ 29, 5],
    [ 12, 5],
    [ 17, 4],
    [ 17, 1],
]

print("EXLOC", len(EXLOC))
print(" EXLOC[0]", len(EXLOC[0]))

# DIM INWS[10)
INWS = [""] * 10
print("INWS", len(INWS))

# DIM INPTK[10]
INPTK = [0] * 10

print("INPTK", len(INPTK))

# --- DISPLAY INTRO ---
def FN_15000_INTRO():
    print(" ")
    print("TAYS HOUSE ADVENTURE")
    print("--------------------")
    print("FIND TREASURES and VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE")
    print("TYPE SIMPLE COMMANDS: NORTH, SOUTH, ETC. TO MOVE (OR JUST 'N', 'S').")
    print("TAKE and DROP, INVENTORY, LOOK, READ, MOVE, and SO ON.")
    print("SOME COMMANDS ARE COMPLEX: 'MOVE THE HUBCAP WITH THE SPANNER'")
    print(" ")

# -- SCORE --
def FN_9500_SCORE():
    SCORE = 50
    for I in range(16, 20):
        if ILOC[I] == 0:
            SCORE = SCORE + 10

    for I in range(3, 30):
        for J in range(1, 6):
            if REXIT[I][J] == -1 :
                SCORE = SCORE -5

    for I in range(1, 15):
        if ILOC[I] == -1 :
            SCORE = SCORE - 5

    print("YOUR SCORE IS "+ SCORE +" OUT OF A POSSIBLE 100")
    # RETURN

# REM FN_10000_DECODE()
def FN_11000_PRINT():
    print(D2S)
    # RETURN

def FN_19000_GAME_OVER():
    D2S = "YOU HAVE DIED"
    print(D2S)
    FN_19999_END_GAME()

def FN_19900_GAME_WON():
    FN_9500_SCORE()
    if SCORE == 100 :
        D2S = "YOU HAVE WON THE GAME!"
        print(D2S)

# --- END GAME ---
def FN_19999_END_GAME():
    exit()
    # END

def FN_2500():
    global LCL
    if LCL == 30:
        D2S = "... and SPRING BACK"
        print(D2S)
        LCL = 12
    if LCL == 31:
        FN_19900_GAME_WON()

def FN_2510():
    print(RNAMES[LCL])
    print(RDESCS[LCL])
    for I in range(1, 6):
        NEIGH = REXIT[LCL][ I]
        if NEIGH > 0:
            print(VOCABS[I] , ": ", RNAMES[NEIGH])

    for I in range(1, NXDESC):
        L1 = EXLOC[I][1]
        L2 = EXLOC[I][2]
        if LCL == L1 and REXIT[L1][L2] <= 0:
            print(EXDESCS[I])

    if LCL == 17 and REXIT[17][1] > 0:
        D2S="YOUR UNCLE'S DOBERMAN IS SNORING PEACEFULLY"
        print(D2S)
    if LCL == 3 and ILOC[6] == -12:
        D2S="A BUNGEE CORD DANGLES FROM THE RAILING ABOVE"
        print(D2S)
    if LCL == 12 and ILOC[6] == -12:
        D2S="A BUNGEE CORD DANGLES FROM THE RAILING"
        print(D2S)
    for I in range(1, LASTITEM):
        if ILOC[I] == LCL:
            print("THERE IS A ", VOCABS[I + ITEMOFF], " HERE")

    if LCL == 2 and ILOC[3] == -1:
        D2S = "SOMETHING IS BARELY VISIBLE UNDER THE FRIDGE"
        print(D2S)
    if LCL == 3 and ILOC[5] == 30:
        D2S = "THERE IS A PICTURE HIGH UP ON THE WALL"
        print(D2S)
    if LCL == 30:
        TURN1 = 1

def FN_5000():
    if TURN1 != 1 and LCL == 30:
        D2S = "...AND SPRING BACK"
        print(D2S)
        LCL = 12
        FN_2500()

    # --- READ INPUT ---
    # INPUT "] "; I$
    IS = input("] ")
    TURN1 = 0
    INWRD = 0
    WIDX = 0
    for C in range(1, len(IS)):
        # CS = mid(IS,C,1)
        CS = IS[C,1]
        if CS == " " and INWRD == 1:
            INWRD = 0
        if CS != " " and INWRD == 0:
            WIDX = WIDX + 1
            INWS[WIDX] = ""
            INWRD = 1
        if WIDX > 10:
            # GOTO 5100
            break
        if CS != " ":
            INWS[WIDX] = INWS[WIDX] + CS

def FN_5100():
    CURTOK = 1
    for TIDX in range(1, WIDX):
        ISNULLW = 0
        for XN in range(1, 4):
            if INWS[TIDX] == NULLWS[XN]:
                ISNULLW = 1

        if ISNULLW == 1:
            # GOTO 5170
            continue

        for CMDIDX in range(1, LWRD):
            if INWS[TIDX] == VOCABS[CMDIDX]:
                INPTK[CURTOK] = CMDIDX
                CURTOK = CURTOK + 1

    #NEXT TIDX # 5170

    NTOK = CURTOK - 1
    COMM = INPTK[1]

    #ON NTOK+1 # GOTO 5000, 6050, 6400, 7100
    X = NTOK+1
    if X == 1: FN_5000()
    if X == 2: FN_6050()
    if X == 3: FN_6400()
    if X == 4: FN_7100()

    D2S = "YOU CAN'T DO THAT"
    print(D2S)
    # GOTO 5000
    # RETURN

def FN_6050():
    if INPTK[1] >= 1 and INPTK[1] <= 12:
        FN_7000()

    if COMM <= 20:
        # ON COMM-12 GOTO 6100, 6100, 6200, 6300, 6350, 6099, 6099, 2500
        # "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK"
        X = COMM-12
        if X == 1: FN_6100_INV()
        if X == 2: FN_6100_INV()
        if X == 3: FN_6200_SCORE()
        if X == 4: FN_6300_JUMP()
        if X == 5: FN_6350_HELP()
        if X == 6: FN_6099()
        if X == 7: FN_6099()
        if X == 8: FN_2500()

def FN_6099():
    # TAKE DROP?
    D2S = "HUH?"
    print(D2S)
    # GOTO 5000

def FN_6100_INV():
    D2S = "YOU ARE CARRYING:"
    print(D2S)
    for I in range(1,LASTITEM):
        if ILOC[I] == 0:
            print("  ", VOCABS[I+ITEMOFF])
    # GOTO 5000

def FN_6200_SCORE():
    FN_9500_SCORE()
    # GOTO 5000

def FN_6300_JUMP():
    if LCL != 12:
        D2S = "WHO ARE YOU, DAVID LEE ROTH?"
        print(D2S)
        # GOTO 5000
    if ILOC[6] != -12:
        D2S = "YOU FORGOT YOUR PARACHUTE"
        print(D2S)
        # GOTO 5000
    D2S = "YOU BUNGEE OFF THE BALCONY..."
    print(D2S)
    LCL = 30
    # GOTO 2510

def FN_6350_HELP():
    FN_15000_INTRO()
    # GOTO 5000

def FN_6400():
    ARG = INPTK[2] - ITEMOFF
    if ARG < 1 or ARG > LASTITEM:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    if COMM > 17 and COMM <= 27:
        #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200
        # "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN", "MOVE", "OPEN", "TIE",

        X = COMM-17
        if X == 1: FN_6500()
        if X == 2: FN_6600()
        if X == 3: FN_6700()
        if X == 4: FN_6700()
        if X == 5: FN_6700()
        if X == 6: FN_6800()
        if X == 7: FN_6900()
        if X == 8: FN_7600()
        if X == 9: FN_6950()
        if X == 10: FN_8200()

    D2S = "HUH?"
    print(D2S)
    # GOTO 5000

#_TAKE():
def FN_6500():
    if ILOC[ARG] == 0:
        D2S = "YOU ALREADY HAVE IT"
        print(D2S)
        # GOTO 5000
    if ILOC[ARG] == 30 and LCL == 3 and ARG == 5:
        D2S = "IT'S TOO HIGH"
        print(D2S)
        # GOTO 5000
    if ILOC[ARG] != LCL:
        D2S = "IT'S NOT HERE"
        print(D2S)
         # GOTO 5000
    if IC >= 8:
        D2S = "YOU'RE CARRYING TOO MUCH"
        print(D2S)
         # GOTO 5000
    if ARG > IMMOFF:
        D2S = "IT'S TOO HEAVY"
        print(D2S)
         # GOTO 5000
    if LCL == 29 and ARG == 12:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
         # GOTO 5000
    IC = IC + 1
    if LCL == 30 and ARG == 5:
        D2S = "TAKING THE PICTURE REVEALS A FUSEBOX"
        print(D2S)
        ILOC[ARG] = 0
        ILOC[IMMOFF+7] = 30
        # GOTO 2500
    
    ILOC[ARG] = 0
    print(VOCABS[INPTK[2]], ": TAKEN")
    # GOTO 5000

#_DROP():
def FN_6600():
    if ILOC[ARG] != 0:
        D2S = "YOU AREN'T CARRYING IT"
        print(D2S)
         # GOTO 5000
    IC = IC - 1
    if LCL == 17 and ARG == 10 and REXIT[17][1] <= 0:
        D2S = "THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT."
        print(D2S)
        # GOTO 6690
    
    if LCL == 17 and ARG == 2 and REXIT[17][1] <= 0:
        D2S = "THE DOG CHEWS HIS FAVORITE TOY and IS SOON ASLEEP"
        print(D2S)
        ILOC[ARG] = -999
        REXIT[17][1] = 18
        # GOTO 2500
    
    if LCL == 29 and ARG == 12 and REXIT[29][5] <= 0:
        D2S = "THE BOXSPRING COVERS THE GAP IN THE STAIRS"
        print(D2S)
        ILOC[ARG] = -999
        REXIT[29][5] = 2
        REXIT[2][6] = 29
        # GOTO 2500
    # 6690
    ILOC[ARG] = LCL
    print(VOCABS[INPTK[2]], ": DROPPED")
    # GOTO 5000

#_LOOK():
def FN_6700():
    ARG = INPTK[2] - ITEMOFF
    if ILOC[ARG] != 0 and ILOC[ARG] != LCL:
        D2S = "IT'S NOT HERE"
        print(D2S)
         # GOTO 5000
    if ARG == 9 and (LCL == 13 or LCL == 22):
        FN_8000()
        FN_8050()
        # GOTO 5000
    
    if IDESCS[ARG] == "":
        print("THERE'S NOTHING SPECIAL ABOUT THE ", VOCABS[INPTK[2]])
        # GOTO 5000
    print(IDESCS[ARG])
    # GOTO 5000

#_UNLOCK():
def FN_6800():
    if ILOC[7] != 0:
        D2S = "YOU DON'T HAVE A KEY!"
        print(D2S)
         # GOTO 5000
    if LCL == 5:
        D2S = "THE KEY DOESN'T FIT THE LOCK"
        print(D2S)
         # GOTO 5000
    if LCL == 17:
        D2S = "YOU UNLOCK THE DOOR. BEWARE!"
        print(D2S)
        REXIT[17][4] = 20
        # GOTO 2500

def FN_8000():
    if SAFED != 0:
        return
        # RETURN
    rnd = random.randint(0, 3)
    SAFED = int(rnd * 3) + 1
    # RETURN

def FN_8050():
    N1S = VOCABS[DIROFF+1]
    N2S = VOCABS[DIROFF+3]
    if SAFED == 1: N1S = VOCABS[DIROFF+2]
    if SAFED == 3: N2S = VOCABS[DIROFF+2]
    D2S = "EXPERIMENTS ON "
    print(D2S)
    NTMSGS = D2S + N1S
    D2S = " and "
    print(D2S)
    NTMSGS = NTMSGS + D2S + N2S
    D2S = " DOORS PROCEEDING WELL; FILE for PATENT"
    print(D2S)
    NTMSGS = NTMSGS + D2S
    print(NTMSGS)
    # RETURN

#_HUH():
def FN_6899():
    D2S = "HUH?"
    print(D2S)
    # GOTO 5000

#_EAT():
def FN_6900():
    if ILOC[ARG] != 0:
        D2S = "YOU DON'T HAVE IT!"
        print(D2S)
         # GOTO 5000
    if ARG != 10:
        D2S = "YOU CAN'T EAT THAT!"
        print(D2S)
         # GOTO 5000
    D2S = "THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER"
    print(D2S)
    ILOC[ARG] = -2
    ILOC[17] = 0
    # GOTO 2500

#_SPIN():
def FN_6950():
    AIMM = ARG - IMMOFF
    if AIMM >= 1 and AIMM <= 4:
        #ON AIMM GOTO 6970, 6975, 6980
        X = AIMM
        if X == 1:
            #FN_6970()
            D2S = "IT'S TOO HEAVY for YOU TO MOVE"
        if X == 2: 
            #FN_6975()
            D2S = "YOUR BACK IS ACTING UP"
        if X == 3: 
            #FN_6980()
            D2S = "THAT SEEMS POINTLESS and UNSANITARY"
        print(D2S)
        # GOTO 5000

    D2S = "YOU CAN'T DO THAT"
    print(D2S)
    # GOTO 5000

def FN_7000():
    GOARG = INPTK[1]
    if GOARG > 6: GOARG = GOARG - 6
    if REXIT[LCL][ GOARG] > 0:
        LCL = REXIT[LCL][ GOARG]
        # GOTO 2500
    if LCL == 12 and GOARG == 5:
        D2S = "YOU'RE AFRAID OF THE DARK"
        print(D2S)
        # GOTO 5000
    if LCL == 17 and GOARG == 1:
        D2S = "YOU NEVER DID LIKE THAT DOG"
        print(D2S)
        # GOTO 5000
    if LCL == 23 and REXIT[23][ 6] <= 0:
        D2S = "THE DUMBWAITER MECHANISM IS CORRODED and WON'T MOVE"
        print(D2S)
        # GOTO 5000
    D2S = "YOU CAN'T GO THAT WAY"
    print(D2S)
    # GOTO 5000

def FN_7100():
    if COMM < 23 or COMM > 30:
        FN_6899()
    
    ARG = INPTK[2] - ITEMOFF
    if COMM != 27 and ARG < 1 or ARG > LASTITEM:
        D2S = "HUH?"
        print(D2S)
         # GOTO 5000
    if COMM != 23 and COMM != 29 and ILOC[ARG] != LCL and ILOC[ARG] != 0:
        D2S = "IT'S NOT HERE"
        print(D2S)
         # GOTO 5000
    #ON COMM-22 GOTO 6800, 6899, 6899, 7200, 7400, 7500, 7700, 7800
    X = COMM-22
    if X == 1: FN_6800()
    if X == 2: FN_6899()
    if X == 3: FN_6899()
    if X == 4: FN_7200()
    if X == 5: FN_7400()
    if X == 6: FN_7500()
    if X == 7: FN_7700()
    if X == 8: FN_7800()

def FN_7200():
    if ARG < IMMOFF:
        D2S = "YOU CAN JUST TAKE THAT"
        print(D2S)
         # GOTO 5000
    AIMM = ARG - IMMOFF
    MVARG = INPTK[3] - ITEMOFF

    if AIMM < 1 or AIMM > 3:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
         # GOTO 5000
    if ILOC[MVARG] != 0:
        D2S = "YOU DON'T HAVE IT!"
        print(D2S)
         # GOTO 5000
    
    #ON AIMM GOTO 7250, 7300, 7350
    X = AIMM
    if X == 1: FN_7250()
    if X == 2: FN_7300()
    if X == 3: FN_7350()

def FN_7250():
    if MVARG != 4 or ILOC[3] >= 0:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
         # GOTO 5000
    D2S = "YOU JACK UP THE FRIDGE and FIND A FUSE UNDER IT"
    print(D2S)
    ILOC[3] = LCL
    # GOTO 2500

def FN_7300():
    if MVARG != 13 or ILOC[2] >= 0:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
        # GOTO 5000
    D2S = "YOU MOVE THE COUCH and FIND A TEDDYBEAR BEHIND IT"
    print(D2S)
    ILOC[2] = LCL
    # GOTO 2500

def FN_7350():
    if MVARG != 11:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
        # GOTO 5000
    D2S="MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT"
    print(D2S)
    REXIT[LCL][6] = 27
    # GOTO 2500

def FN_7400():
    if LCL != 20:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    if INPTK[3] - ITEMOFF != IMMOFF + 4:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    DOORDIR = INPTK[2] - DIROFF
    if DOORDIR < 1 or DOORDIR > 3:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    FN_8025()
    if DOORDIR == SAFED:
        D2S = "OPENING THE DOOR REVEALS A DUMBWAITER"
        print(D2S)
        REXIT[LCL][ 4] = 23
        # GOTO 2500
    rnd = random.randint(0,2)
    if rnd > 1:
        D2S = "A SHOT RINGS OUT! IT WAS WELL-AIMED TOO."
        print(D2S)
        # GOTO 19000
    D2S = "AN IRONING BOARD SLAMS ONTO YOUR HEAD"
    print(D2S)
    # GOTO 19000

def FN_8025():
    if SAFED != 0:
        return
        # RETURN
    SAFED = (INPTK[2] - DIROFF) + 1
    if SAFED > 3: SAFED = 1
    # RETURN

def FN_7500():
    if LCL != 12:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
        # GOTO 5000
    if INPTK[2] - ITEMOFF != 6:
        D2S = "YOU CAN'T TIE THAT"
        print(D2S)
        # GOTO 5000
    if INPTK[3] - ITEMOFF != (IMMOFF + 5):
        D2S = "YOU CAN'T TIE TO THAT"
        print(D2S)
        # GOTO 5000
    if ILOC[6] != 0:
        D2S = "YOU DON'T HAVE IT!"
        print(D2S)
        # GOTO 5000
    D2S = "TIED"
    print(D2S)
    ILOC[6] = -12
    IC = IC - 1
    #GOTO 2500

def FN_7600():
    if ILOC[8] != 0:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    if LCL == 18:
        D2S = "THERE IS A FLASH OF LIGHT and A CRACKING SOUND. AN OPENING APPEARS IN THE EAST WALL"
        print(D2S)
        REXIT[18][ 3] = 19
        #GOTO 2500
    #INVERSE:
    D2S = "WHEE!"
    print(D2S)
    # NORMAL: 
    print(" ")
    # GOTO 5000

def FN_7700():
    if LCL != 20:
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
        # GOTO 5000
    if ILOC[15] != 0:
        D2S = "YOU DON'T HAVE ANY OIL"
        print(D2S)
        # GOTO 5000
    if INPTK[2] - ITEMOFF != IMMOFF + 6:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    D2S = "THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY"
    print(D2S)
    REXIT[23][6] = 24
    # GOTO 5000

def FN_7800():
    if LCL != 30 or (INPTK[2] - ITEMOFF != 3):
        D2S = "YOU CAN'T DO THAT"
        print(D2S)
        # GOTO 5000
    if INPTK[3] - ITEMOFF != (IMMOFF + 7):
        D2S = "YOU CAN'T PUT IT THERE"
        print(D2S)
        # GOTO 5000
    if ILOC[3] != 0:
        D2S = "YOU DON'T HAVE IT!"
        print(D2S)
        # GOTO 5000
    D2S = "YOU PUT THE FUSE IN THE BOX"
    print(D2S)
    ILOC[3] = -999
    IC = IC - 1
    REXIT[12][5] = 25
    # GOTO 5000

def FN_8200():
    if LCL == 20 and (INPTK[2] - ITEMOFF) == IMMOFF + 4:
        D2S = "PLEASE SPECIFY LEFT, CENTER, or RIGHT"
        print(D2S)
        # GOTO 5000
    else:
        D2S = "HUH?"
        print(D2S)
        # GOTO 5000
    # RETURN

# REM ******************
# REM * START HERE
# REM ******************
def FN_GAME():
    #clear
    FN_15000_INTRO()
    while True:
        # --- LOOP PRINCIPAL ---
        FN_2500()
        FN_2510()
        FN_5000()
        FN_5100()

FN_15000_INTRO()
FN_2500()

FN_2510()
# FN_5000()
# FN_5100()
