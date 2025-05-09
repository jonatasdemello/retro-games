10 REM *** UNCLE TAYS HOUSE ADVENTURE ***
REM # *** VARIABLES ***
    DIM INW$(10)
    DIM INPTK(10)
    DIM VOCAB$(60)
    DIM NULLW$(4)
    DIM IDESC$(30)
    DIM ILOC(30)    
    DIM RNAME$(31)
    DIM RDESC$(31)
    DIM REXIT(31,6)
    DIM EXDESC$(10)
    DIM EXLOC(10,2)

    LET DIROFF = 30
    LET ITEMOFF = 33
    LET LASTITEM = 27
    LET IMMOFF = 20
    LET LWRD = 60
    LET NXDESC = 7
    LET SAFED = 0
    LET LCL = 1
    LET IC = 0
    # LET SP = 0

REM **********************
REM * INITIALIZE VARIABLES
REM **********************
REM # -- READ DATA --
REM # *** DIM *** VOCAB$(60)
    FOR I = 1 TO LWRD
        READ D2$
        VOCAB$(I) = D2$
    NEXT
        REM # VOCAB$(60)
        DATA "NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN", "N", "S", "E", "W", "U", "D"
        DATA "I", "INVENTORY", "SCORE", "JUMP", "HELP"
        DATA "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN"
        DATA "MOVE", "OPEN", "TIE", "OIL", "PUT", "LEFT", "CENTER", "RIGHT"
        DATA "NEWSPAPER", "TEDDYBEAR", "FUSE", "JACK", "PICTURE", "BUNGEE"
        DATA "KEY", "TOP", "NOTE", "GAINESBURGER", "GLOVES", "BOXSPRING"
        DATA "BRACE", "MAGAZINE", "OILCAN", "CHECKBOOK", "DIAMOND", "LOVERBOY"
        DATA "INVESTMENT", "LOONS", "FRIDGE", "COUCH", "CLOTHES", "DOOR"
        DATA "RAILING", "DUMBWAITER", "FUSEBOX", 

REM # *** DIM *** NULLW$(4)
    FOR I = 1 TO 4
        READ D2$
        NULLW$(I) = D2$
    NEXT
        REM # NULLW$(4)
        DATA "THE", "TO", "WITH", "USING"

REM # *** DIM *** IDESC$(30)
    FOR I = 1 TO LASTITEM
        READ D2$
        IDESC$(I) = D2$
    NEXT
        REM # IDESC$(27)
        DATA "TAYS HOUSE UNLIKELY EVER TO BE SOLD.  TALES OF GUTTED STAIRWELLS AND BOOBY TRAPS HAVE SPOOKED BUYERS..."
        DATA "SOMEONE HAS BEEN PLAYING VERY ROUGH WITH THIS TOY", "OLD-FASHIONED ELECTRICAL FUSE"
        DATA "TIRE JACK FOR LIFTING HEAVY OBJECTS LIKE CARS", "UNCLE TAYS IN ALL HIS SALLOW GLORY"
        DATA "CORD FOR BUNGEE JUMPING", "A SMALL BRASS KEY", "A CHILD'S TOY", "THE WRITING IS REVERSED"
        DATA "SUPPOSEDLY DOG FOOD, THOUGH IT APPEARS TO BE MADE OF PLASTIC"
        DATA "RUBBER GLOVES USED FOR CLEANING", "A QUEEN-SIZED BOXSPRING", "A BACK BRACE"
        DATA "TAYS' STRANGE INVENTIONS INCLUDE BOOBY-TRAPPED DOORS AND TOYS THAT OPEN DOORS BY REMOTE CONTROL..."
        DATA "THIS CAN CONTAINS FINE LUBRICATING OIL", "UNCLE TAYS' CHECKBOOK LISTS A BALANCE OF $220,000"
        DATA "THIS DIAMOND'S BEAUTY STEMS FROM ALL THE GODDAMNED MONEY IT IS WORTH"
        DATA "LOVERBOY'S FIRST ALBUM IN VINYL, WORTH AN INCALCULABLE SUM"
        DATA "PRE-IPO SHARES OF APOLLO COMPUTING HAVE TO BE WORTH ... SOMETHING"
        DATA "A THICK WAD OF CANADIAN NOTES"
        DATA "THIS OLD REFRIGERATOR'S MOTOR LABORS HEAVILY", "AN OVERSTUFFED, DUSTY COUCH"
        DATA "A DISGUSTING PILE OF SOILED LAUNDRY", "", "", ""
        DATA "AN OLD-FASHIONED FUSEBOX.  THE FUSE MARKED 'ATTIC' IS MISSING."

REM # *** DIM *** RNAME$(31), REXIT(31,6), RDESC$(31)
    FOR I = 1 TO 31
        READ D2$
        RNAME$(I) = D2$
        FOR J = 1 TO 6
            READ REXIT(I,J)
        NEXT 
        READ D2$
        RDESC$(I) = D2$
    NEXT
        REM # RNAME$(I), REXIT(I,J) x6, RDESC$(I) 
        DATA "FOYER", 2, 31, 3, 4, 0, 0, "THE ENTRYWAY TO THE HOUSE"
        DATA "KITCHEN", 0, 1, 0, 0, 0, -1, "COUNTERTOPS ARE DUSTY AND THERE ARE RUSTING POTS AND PANS"
        DATA "SITTING ROOM", 2, 0, 11, 1, 12, 0, "THIS ROOM IS TWO STORIES HIGH AND CONTAINS ELEGANT CHAIRS AND COUCHES"
        DATA "HALLWAY", 6, 7, 1, 5, 0, 0, "A NARROW HALLWAY WHICH RUNS WEST OF THE FOYER"
        DATA "HALLWAY", 0, 9, 4, 10, 0, 0, "A NARROW HALLWAY AT THE WEST END OF THE HOUSE"
        DATA "DEN", 0, 4, 0, 0, 0, 0, "THIS ROOM HAS AN ANCIENT TELEVISION"
        DATA "BATHROOM", 4, 0, 0, 0, 0, -1, "A DINGY BATHROOM WITH A CRACKED SINK"
        DATA "LIBRARY", 0, 0, 0, 24, 0, 0, "THIS WELL-FURNISHED LIBRARY IS LINED WITH BOOKS AND LEATHER FURNITURE"
        DATA "SMALL BEDROOM", 5, 0, 0, 0, 0, 0, "THIS SMALL BEDROOM HAS A TWIN BED AND CHAIR.  IT LOOKS LITTLE USED."
        DATA "GARAGE", 0, 0, 5, 0, 0, 0, "THE CAVERNOUS GARAGE HOLDS A NON-OPERATIONAL GREMLIN AND PILES OF JUNK"
        DATA "DINING ROOM", 0, 0, 0, 3, 0, 0, "TROPHIES LINE THE WALLS.  THERE ARE SIX CHAIRS AROUND A LONG TABLE."
        DATA "BALCONY", 13, 0, 0, 14, -1, 3, "BALCONY ABOVE THE SITTING ROOM.  A RAILING PROTECTS YOU FROM A 15-FOOT DROP."
        DATA "MASTER BEDROOM", 0, 12, 0, 0, 0, 0, "THIS LARGE CORNER BEDROOM HAS SOLID WALNUT FURNITURE AND A LARGE MIRROR"
        DATA "HALLWAY", 0, 15, 12, 17, 0, 0, "A HALLWAY WITH A LARGE ARCH ON ITS SOUTH SIDE"
        DATA "GAME ROOM", 14, 0, 0, 16, 0, 0, "THIS ELEGANT GAME ROOM HAS A POOL TABLE AND MARBLE CHESSBOARD"
        DATA "CLOSET", 0, 0, 15, 0, 0, 0, "A SPACIOUS CLOSET OFF THE GAMEROOM"
        DATA "HALLWAY", -1, 0, 14, -1, 0, 0, "A HALLWAY IN THE CENTER OF THE SECOND FLOOR"
        DATA "CHILD'S ROOM", 0, 17, -1, 0, 0, 0, "YOUR COUSIN'S ROOM IN HAPPIER TIMES, BEFORE HE RAN OFF TO JOIN THE BAATH PARTY"
        DATA "SECRET ROOM1", 0, 0, 0, 18, 0, 0, "A DARK CHAMBER OFF THE BEDROOM"
        DATA "DANGEROUS HALL", 21, 22, 17, -1, 0, 0, "THIS EERIE HALL HAS THREE IDENTICAL DOORS ON THE WEST WALL"
        DATA "CORNER BEDROOM", 0, 20, 0, 0, 0, 0, "A COZY CORNER ROOM WITH WINDOWS ON TWO WALLS"
        DATA "BATHROOM", 20, 0, 0, 0, 0, 0, "AN ELEGANT BATH WITH A MIRROR OVER A MARBLE SINK"
        DATA "DUMBWAITER", 0, 0, 20, 0, 0, -1, "A CRAMPED DUMBWAITER"
        DATA "DUMBWAITER", 0, 0, 8, 0, 23, 0, "A CRAMPED DUMBWAITER"
        DATA "ATTIC", 0, 0, 0, 0, 0, 12, "A DUSTY ATTIC WITH LOW SLOPING WALLS"
        DATA "STORAGE ROOM", 0, 0, 27, 0, 0, 0, "A BARE ROOM USED TO STORE RANDOM EQUIPMENT AND FURNITURE"
        DATA "LAUNDRY", 0, 0, 28, 26, 0, 0, "THIS ROOM HAS A WASHER AND DRYER, AS WELL AS A BOILER AND FURNACE"
        DATA "WORKROOM", 0, 0, 29, 27, 0, 0, "EQUIPMENT FOR WORKING WOOD AND METAL"
        DATA "BOTTOM OF STAIRS", 0, 0, 0, 28, -1, 0, "STAIRS FROM BASEMENT TO KITCHEN"
        DATA "MID-AIR", 0, 0, 0, 0, 0, 0, "HANGING FROM A BUNGEE CORD"
        DATA "LEAVE THE HOUSE (AND THE GAME)", 0, 0, 0, 0, 0, 0, ""

REM # *** DIM *** ILOC(30)
    FOR I = 1 TO LASTITEM
        READ ILOC(I)
    NEXT
        DATA 1, -1, -1, 10, 30, 10, 13, 15, 9, 16,
        DATA 22, 26, 25, 25, 28, 8, -1, 19, 21, 27
        DATA 2, 6, 7, -1, 12, -1, -1

REM # *** DIM *** EXDESC$(10)
REM # *** DIM *** EXLOC(10,2)
    FOR I = 1 TO NXDESC
        READ EXLOC(I,1)
        READ EXLOC(I,2)
        READ D2$
        EXDESC$(I) = D2$
    NEXT
        REM # --- EXLOC(I,1), EXLOC(I,2), EXDESC$(7) ---
        DATA 5, 6, "THERE IS A LOCKED DOOR TO THE NORTH"
        DATA 8, 6, "THERE IS A LOCKED DOOR TO THE SOUTH"
        DATA 2, 6, "STAIRS LEAD DOWN TO A CELLAR.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE."
        DATA 29, 5, "STAIRS LEAD UP.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE."
        DATA 12, 5, "DARK STAIRS LEAD UP TO THE ATTIC"
        DATA 17, 4, "A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'"
        DATA 17, 1, "YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH"


--------------------------------------------------------------------------------------

REM ******************
REM * START HERE
REM ******************
def FN_GAME():
    CLS
    FN_15000_INTRO()
    LET GAME = TRUE
    WHILE GAME:
        REM # --- LOOP PRINCIPAL ---
        FN_2500()
        FN_2510()
        FN_5000()
        FN_5100()


def FN_2500():
    IF LCL = 30 THEN 
        D2$= "... AND SPRING BACK"
        print D2$
        LCL = 12
    IF LCL = 31 THEN 
        GOTO FN_19900_GAME_WIN()

def FN_2510():
    print RNAME$(LCL)
    print RDESC$(LCL)
    FOR I = 1 TO 6
        NEIGH = REXIT(LCL, I)
        IF NEIGH > 0 THEN 
            print VOCAB$(I) ; ": "; RNAME$(NEIGH)
    NEXT
    FOR I = 1 TO NXDESC
        IF LCL = EXLOC(I,1) AND REXIT(EXLOC(I,1),EXLOC(I,2)) <= 0 THEN 
            print EXDESC$(I)
    NEXT
    IF LCL = 17 AND REXIT(17,1) > 0 THEN
        D2$="YOUR UNCLE'S DOBERMAN IS SNORING PEACEFULLY"
        print D2$
    IF LCL = 3 AND ILOC(6) = -12 THEN
        D2$="A BUNGEE CORD DANGLES FROM THE RAILING ABOVE"
        print D2$
    IF LCL = 12 AND ILOC(6) = -12 THEN
        D2$="A BUNGEE CORD DANGLES FROM THE RAILING"
        print D2$
    FOR I = 1 TO LASTITEM
        IF ILOC(I) = LCL THEN 
            print "THERE IS A "; VOCAB$(I + ITEMOFF); " HERE"
    NEXT
    IF LCL = 2 AND ILOC(3) = -1 THEN
        D2$= "SOMETHING IS BARELY VISIBLE UNDER THE FRIDGE"
        print D2$
    IF LCL = 3 AND ILOC(5) = 30 THEN
        D2$= "THERE IS A PICTURE HIGH UP ON THE WALL"
        print D2$
    IF LCL = 30 THEN
        TURN1 = 1

def FN_5000():
    IF TURN1 <> 1 AND LCL = 30 THEN 
        D2$= "...AND SPRING BACK"
        print D2$
        LCL = 12
        GOTO 2500
    REM # --- READ INPUT ---
    TURN1 = 0
    INPUT "] "; I$
    INWRD = 0
    WIDX = 0
    FOR C = 1 TO LEN(I$)
        C$ = MID$(I$,C,1)
        IF C$ = " " AND INWRD = 1 THEN
            INWRD = 0
        IF C$ <> " " AND INWRD = 0 THEN
            WIDX = WIDX + 1
            INW$(WIDX) = ""
            INWRD = 1
        IF WIDX > 10 
            # GOTO 5100
            BREAK
        IF C$ <> " " THEN
            INW$(WIDX) = INW$(WIDX) + C$
    NEXT

def FN_5100():
    CURTOK = 1
    FOR TIDX = 1 TO WIDX
        ISNULLW = 0
        FOR XN = 1 TO 4
            IF INW$(TIDX) = NULLW$(XN) THEN 
                ISNULLW = 1
        NEXT
        IF ISNULLW = 1 THEN
            # GOTO 5170
            CONTINUE
        FOR CMDIDX = 1 TO LWRD
            IF INW$(TIDX) = VOCAB$(CMDIDX) THEN 
                INPTK(CURTOK) = CMDIDX
                CURTOK = CURTOK + 1
        NEXT CMDIDX
    NEXT TIDX # 5170

    NTOK = CURTOK - 1
    COMM = INPTK(1)

    ON NTOK+1 GOTO 5000, 6050, 6400, 7100
    X = NTOK+1
        IF X = 1 THEN FN_5000()
        IF X = 2 THEN FN_6050()
        IF X = 3 THEN FN_6400()
        IF X = 4 THEN FN_7100()

    D2$ = "YOU CAN'T DO THAT"
    print D2$
    # GOTO 5000
    # RETURN

def FN_6050():
    IF INPTK(1) >= 1 AND INPTK(1) <= 12 THEN 
        GOTO FN_7000()

    IF COMM <= 20 THEN 
        ON COMM-12 GOTO 6100, 6100, 6200, 6300, 6350, 6099, 6099, 2500

        # "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK"
        X = COMM-12
        IF X = 1 THEN FN_6100_INV()
        IF X = 2 THEN FN_6100_INV()
        IF X = 3 THEN FN_6200_SCORE()
        IF X = 4 THEN FN_6300_JUMP()
        IF X = 5 THEN FN_6350_HELP()
        IF X = 6 THEN FN_6099()
        IF X = 7 THEN FN_6099()
        IF X = 8 THEN FN_2500()

def FN_6099()
    # TAKE DROP?
    D2$ = "HUH?"
    print D2$
    # GOTO 5000

def FN_6100_INV():
    D2$ = "YOU ARE CARRYING:"
    print D2$
    FOR I = 1 TO LASTITEM
        IF ILOC(I) = 0 THEN 
            print "  "; VOCAB$(I+ITEMOFF)
    NEXT I
    # GOTO 5000

def FN_6200_SCORE():
    GOSUB FN_9500_SCORE()
    # GOTO 5000

def FN_6300_JUMP():
    IF LCL <> 12 THEN 
        D2$= "WHO ARE YOU, DAVID LEE ROTH?"
        print D2$
        GOTO 5000
    IF ILOC(6) <> -12 THEN
        D2$ = "YOU FORGOT YOUR PARACHUTE"
        print D2$
        GOTO 5000
    D2$ = "YOU BUNGEE OFF THE BALCONY..."
    print D2$
    LCL = 30
    # GOTO 2510

def FN_6350_HELP(): 
    GOSUB FN_15000_INTRO()
    # GOTO 5000

def FN_6400():
    ARG = INPTK(2) - ITEMOFF
    IF ARG < 1 or ARG > LASTITEM THEN 
        D2$ = "HUH?"
        print D2$
        GOTO 5000
    IF COMM > 17 AND COMM <= 27 THEN 
        ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200
        # "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN", "MOVE", "OPEN", "TIE",
        
        X = COMM-17 
        IF X = 1 THEN FN_6500()
        IF X = 1 THEN FN_6600()
        IF X = 1 THEN FN_6700()
        IF X = 1 THEN FN_6700()
        IF X = 1 THEN FN_6700()
        IF X = 1 THEN FN_6800()
        IF X = 1 THEN FN_6900()
        IF X = 1 THEN FN_7600()
        IF X = 1 THEN FN_6950()
        IF X = 1 THEN FN_8200()

    D2$ = "HUH?"
    print D2$
    GOTO 5000

def FN_6500_TAKE(): 
    IF ILOC(ARG) = 0 THEN
        D2$ = "YOU ALREADY HAVE IT"
        print D2$
        GOTO 5000
    IF ILOC(ARG) = 30 AND LCL = 3 AND ARG = 5 THEN
        D2$= "IT'S TOO HIGH"
        print D2$
        GOTO 5000
    IF ILOC(ARG) <> LCL THEN
		D2$ = "IT'S NOT HERE"
		print D2$
		 GOTO 5000
    IF IC >= 8 THEN
		D2$ = "YOU'RE CARRYING TOO MUCH"
		print D2$
		 GOTO 5000
    IF ARG > IMMOFF THEN
		D2$ = "IT'S TOO HEAVY"
		print D2$
		 GOTO 5000
    IF LCL = 29 AND ARG = 12 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		 GOTO 5000
    IC = IC + 1: IF LCL = 30 AND ARG = 5 THEN
		D2$ = "TAKING THE PICTURE REVEALS A FUSEBOX"
		print D2$
		 ILOC(ARG) = 0: ILOC(IMMOFF+7) = 30: GOTO 2500
    ILOC(ARG) = 0: print VOCAB$(INPTK(2)); ": TAKEN": GOTO 5000

def FN_6600_DROP();
    IF ILOC(ARG) <> 0 THEN
		D2$= "YOU AREN'T CARRYING IT"
		print D2$
		 GOTO 5000
    IC = IC - 1: IF LCL = 17 AND ARG = 10 AND REXIT(17,1) <= 0 THEN
		D2$= "THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT."
		print D2$
		 GOTO 6690
    IF LCL = 17 AND ARG = 2 AND REXIT(17,1) <= 0 THEN
		D2$= "THE DOG CHEWS HIS FAVORITE TOY AND IS SOON ASLEEP"
		print D2$
		 ILOC(ARG) = -999: REXIT(17,1)=18: GOTO 2500
    IF LCL = 29 AND ARG = 12 AND REXIT(29,5) <= 0 THEN
		D2$= "THE BOXSPRING COVERS THE GAP IN THE STAIRS"
		print D2$
		 ILOC(ARG) = -999: REXIT(29,5) = 2: REXIT(2,6) = 29: GOTO 2500
    # 6690 
    ILOC(ARG) = LCL: print VOCAB$(INPTK(2)); ": DROPPED": GOTO 5000

def FN_6700_LOOK():
    ARG = INPTK(2) - ITEMOFF
    IF ILOC(ARG) <> 0 AND ILOC(ARG) <> LCL THEN
		D2$= "IT'S NOT HERE"
		print D2$
		 GOTO 5000
    IF ARG = 9 AND (LCL = 13 OR LCL = 22) THEN GOSUB FN_8000(): GOSUB FN_8050(): GOTO 5000
    IF IDESC$(ARG) = "" THEN print "THERE'S NOTHING SPECIAL ABOUT THE "; VOCAB$(INPTK(2)): GOTO 5000
    print IDESC$(ARG): GOTO 5000

def FN_6800_UNLOCK():
    IF ILOC(7) <> 0 THEN
		D2$ = "YOU DON'T HAVE A KEY!"
		print D2$
		 GOTO 5000
    IF LCL = 5 THEN
		D2$= "THE KEY DOESN'T FIT THE LOCK"
		print D2$
		 GOTO 5000
    IF LCL = 17 THEN
		D2$ = "YOU UNLOCK THE DOOR. BEWARE!"
		print D2$
		 REXIT(17,4) = 20: GOTO 2500

def FN_8000():
    IF SAFED <> 0 THEN # RETURN
    SAFED = INT(RND(3) * 3) + 1
    # RETURN

def FN_8050():
    N1$ = VOCAB$(DIROFF+1)
    N2$ = VOCAB$(DIROFF+3)
    IF SAFED = 1 THEN N1$ = VOCAB$(DIROFF+2)
    IF SAFED = 3 THEN N2$ = VOCAB$(DIROFF+2)
    D2$ = "EXPERIMENTS ON "
    print D2$
    NTMSG$ = D2$ + N1$
    D2$ = " AND "
    print D2$
    NTMSG$ = NTMSG$ + D2$ + N2$
    D2$ = " DOORS PROCEEDING WELL; FILE FOR PATENT"
    print D2$
    NTMSG$=NTMSG$+D2$
    print NTMSG$
    # RETURN

def FN_6899_HUH():
    D2$ = "HUH?"
		print D2$
		 GOTO 5000

def FN_6900_EAT():
    IF ILOC(ARG) <> 0 THEN
		D2$= "YOU DON'T HAVE IT!"
		print D2$
		 GOTO 5000
    IF ARG <> 10 THEN
		D2$= "YOU CAN'T EAT THAT!"
		print D2$
		 GOTO 5000
    D2$= "THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER"
		print D2$
		 ILOC(ARG) = -2: ILOC(17) = 0: GOTO 2500

def FN_6950_SPIN():
    AIMM = ARG - IMMOFF
    IF AIMM >= 1 AND AIMM <= 4 THEN
        ON AIMM GOTO 6970, 6975, 6980
        X = AIMM
        IF X = 8 THEN FN_6970()
        IF X = 8 THEN FN_6975()
        IF X = 8 THEN FN_6980()

    6970 D2$= "IT'S TOO HEAVY FOR YOU TO MOVE"
		print D2$
		 GOTO 5000
    6975 D2$= "YOUR BACK IS ACTING UP"
		print D2$
		 GOTO 5000
    6980 D2$= "THAT SEEMS POINTLESS AND UNSANITARY"
		print D2$
		 GOTO 5000
    
    D2$= "YOU CAN'T DO THAT"
		print D2$
		 GOTO 5000

def FN_7000():
    GOARG = INPTK(1): IF GOARG > 6 THEN GOARG = GOARG - 6
    IF REXIT(LCL, GOARG) > 0 THEN LCL = REXIT(LCL, GOARG): GOTO 2500
    IF LCL = 12 AND GOARG = 5 THEN
		D2$= "YOU'RE AFRAID OF THE DARK"
		print D2$
		 GOTO 5000
    IF LCL = 17 AND GOARG = 1 THEN
		D2$= "YOU NEVER DID LIKE THAT DOG"
		print D2$
		 GOTO 5000
    IF LCL = 23 AND REXIT(23, 6) <= 0 THEN
		D2$ = "THE DUMBWAITER MECHANISM IS CORRODED AND WON'T MOVE"
		print D2$
		 GOTO 5000
    D2$= "YOU CAN'T GO THAT WAY"
		print D2$
		 GOTO 5000

def FN_7100():
    IF COMM < 23 OR COMM > 30 THEN GOTO FN_6899_HUH()
    ARG = INPTK(2) - ITEMOFF: IF COMM <> 27 AND ARG < 1 or ARG > LASTITEM THEN
		D2$ = "HUH?"
		print D2$
		 GOTO 5000
    IF COMM <> 23 AND COMM <> 29 AND ILOC(ARG) <> LCL AND ILOC(ARG) <> 0 THEN
		D2$= "IT'S NOT HERE"
		print D2$
		 GOTO 5000
    ON COMM-22 GOTO 6800, 6899, 6899, 7200, 7400, 7500, 7700, 7800
    X = COMM-22 
    IF X = 1 THEN FN_6800
    IF X = 1 THEN FN_6899_HUH()
    IF X = 1 THEN FN_6899_HUH()
    IF X = 1 THEN FN_7200()
    IF X = 1 THEN FN_7400()
    IF X = 1 THEN FN_7500()
    IF X = 1 THEN FN_7700()
    IF X = 1 THEN FN_7800()

def FN_7200():
    IF ARG < IMMOFF THEN
		D2$= "YOU CAN JUST TAKE THAT"
		print D2$
		 GOTO 5000
    AIMM = ARG - IMMOFF: MVARG = INPTK(3) - ITEMOFF:
    IF AIMM < 1 OR AIMM > 3 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		 GOTO 5000
    IF ILOC(MVARG) <> 0 THEN
		D2$ = "YOU DON'T HAVE IT!"
		print D2$
		 GOTO 5000
    ON AIMM GOTO 7250, 7300, 7350
    X = AIMM
    IF X = 1 THEN FN_7250()
    IF X = 1 THEN FN_7300()
    IF X = 1 THEN FN_7350()

def FN_7250():
    IF MVARG <> 4 OR ILOC(3) >= 0 THEN
		D2$= "YOU CAN'T DO THAT"
		print D2$
		 GOTO 5000
    D2$= "YOU JACK UP THE FRIDGE AND FIND A FUSE UNDER IT"
		print D2$
		ILOC(3)=LCL
        GOTO 2500

def FN_7300():
    IF MVARG <> 13 OR ILOC(2) >= 0 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		GOTO 5000
    D2$="YOU MOVE THE COUCH AND FIND A TEDDYBEAR BEHIND IT"
		print D2$
		ILOC(2)=LCL
        GOTO 2500

def FN_7350():
    IF MVARG <> 11 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		GOTO 5000
    D2$="MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT"
		print D2$
		REXIT(LCL,6) = 27
        GOTO 2500

def FN_7400():
    IF LCL <> 20 THEN
		D2$ = "HUH?"
		print D2$
		GOTO 5000
    IF INPTK(3) - ITEMOFF <> IMOFF + 4 THEN
		D2$ = "HUH?"
		print D2$
		GOTO 5000
    DOORDIR = INPTK(2) - DIROFF: IF DOORDIR < 1 OR DOORDIR > 3 THEN
		D2$ = "HUH?"
		print D2$
		GOTO 5000
    GOSUB FN_8025()
    IF DOORDIR = SAFED THEN
		D2$= "OPENING THE DOOR REVEALS A DUMBWAITER"
		print D2$
		REXIT(LCL, 4) = 23
        GOTO 2500
    IF INT(RND(2)) > 1 THEN
		D2$= "A SHOT RINGS OUT! IT WAS WELL-AIMED TOO."
		print D2$
		GOTO 19000
    D2$= "AN IRONING BOARD SLAMS ONTO YOUR HEAD"
		print D2$
		GOTO 19000

def FN_8025():
    IF SAFED <> 0 THEN # RETURN
    SAFED = (INPTK(2) - DIROFF) + 1
    IF SAFED > 3 THEN SAFED = 1
    # RETURN

def FN_7500():
    IF LCL <> 12 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		GOTO 5000
    IF INPTK(2) - ITEMOFF <> 6 THEN
		D2$= "YOU CAN'T TIE THAT"
		print D2$
		GOTO 5000
    IF INPTK(3) - ITEMOFF <> (IMOFF + 5) THEN
		D2$= "YOU CAN'T TIE TO THAT"
		print D2$
		GOTO 5000
    IF ILOC(6) <> 0 THEN
		D2$ = "YOU DON'T HAVE IT!"
		print D2$
		GOTO 5000
    D2$= "TIED": print D2$
    ILOC(6) = -12
    IC = IC - 1
    GOTO 2500

def FN_7600():
    IF ILOC(8) <> 0 THEN
		D2$ = "HUH?"
		print D2$
		GOTO 5000
    IF LCL = 18 THEN
		D2$= "THERE IS A FLASH OF LIGHT AND A CRACKING SOUND. AN OPENING APPEARS IN THE EAST WALL"
		print D2$
		REXIT(18, 3) = 19
        GOTO 2500
    INVERSE: 
        D2$= "WHEE!"
		print D2$
		NORMAL: print ""
        GOTO 5000

def FN_7700():
    IF LCL <> 20 THEN
		D2$ = "YOU CAN'T DO THAT"
		print D2$
		GOTO 5000
    IF ILOC(15) <> 0 THEN
		D2$= "YOU DON'T HAVE ANY OIL"
		print D2$
		GOTO 5000
    IF INPTK(2) - ITEMOFF <> IMOFF + 6 THEN
		D2$ = "HUH?"
		print D2$
		GOTO 5000
    D2$= "THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY"
		print D2$
		REXIT(23,6) = 24
        GOTO 5000

def FN_7800():
    IF LCL <> 30 OR (INPTK(2) - ITEMOFF <> 3) THEN 
        D2$ = "YOU CAN'T DO THAT"
		print D2$
		GOTO 5000
    IF INPTK(3) - ITEMOFF <> (IMOFF + 7) THEN 
        D2$= "YOU CAN'T PUT IT THERE"
		print D2$
		GOTO 5000
    IF ILOC(3) <> 0 THEN 
        D2$ = "YOU DON'T HAVE IT!"
		print D2$
		GOTO 5000
    D2$= "YOU PUT THE FUSE IN THE BOX"
    print D2$
    ILOC(3) = -999
    IC = IC - 1
    REXIT(12,5) = 25
    GOTO 5000

def FN_8200():
    IF LCL = 20 AND (INPTK(2) - ITEMOFF) = IMOFF + 4 THEN 
        D2$ = "PLEASE SPECIFY LEFT, CENTER, OR RIGHT"
        print D2$
        GOTO 5000
    ELSE
        D2$ = "HUH?"
        print D2$
        GOTO 5000
    # RETURN

REM # --- DISPLAY INTRO ---
def FN_15000_INTRO():
    print "TAYS HOUSE ADVENTURE"
    print "FIND TREASURES AND VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE"
    print "TYPE SIMPLE COMMANDS: NORTH, SOUTH, ETC. TO MOVE (OR JUST 'N', 'S')."
    print "TAKE AND DROP, INVENTORY, LOOK, READ, MOVE, AND SO ON."
    print "SOME COMMANDS ARE COMPLEX: 'MOVE THE HUBCAP WITH THE SPANNER'"
    # RETURN

REM # -- SCORE --
def FN_9500_SCORE():
    SCORE = 50
    FOR I = 16 TO 20
        IF ILOC(I) = 0 THEN 
            SCORE = SCORE + 10
    NEXT
    FOR I = 3 TO 30
        FOR J = 1 TO 6
            IF REXIT(I,J) = -1 THEN 
                SCORE = SCORE -5
    NEXT J, I
    FOR I = 1 TO 15
        IF ILOC(I) = -1 THEN
            SCORE = SCORE - 5
    NEXT
    print "YOUR SCORE IS "; SCORE; " OUT OF A POSSIBLE 100":
    # RETURN

REM # --- DECODE D$ INTO D2$ ---
def FN_10000_DECODE():
    D2$ = "" 
    IF D$ = "" THEN 
        IF SP = 1 THEN print "."; REM # loading
    CS = 0
    OFT = ASC(MID$(D$,1,1))-48
    FOR DC = 2 TO LEN(D$)-1
        D2AS = ASC(MID$(D$,DC,1))
        IF D2AS >= 65 AND D2AS <= 90 THEN D2AS=D2AS - OFT
        IF D2AS < 65 THEN D2AS = D2AS+26
        D2$ = D2$ + CHR$(D2AS)
        IF D2AS >= 65 AND D2AS <= 90 THEN CS = CS + D2AS
        IF CS > 9 THEN CS = CS-((INT(CS/10)) * 10)
    NEXT
    IF D$ <> "" THEN 
        IF ASC(MID$(D$, LEN(D$), 1))-48 <> CS THEN 
            print "BAD CHECKSUM FOR "; D$; ", FOUND "; CS
    GOTO FN_19999_END_GAME()
    # RETURN

REM # --- print MESSAGE D2$ ---
def FN_11000_PRINT():
    REM GOSUB FN_10000_DECODE()
    print D2$
    # RETURN

def FN_19000_GAME_OVER():
    D2$ = "YOU HAVE DIED"
    print D2$
    GOTO FN_19999_END_GAME()

def FN_19900_GAME_WON():
    GOSUB FN_9500_SCORE()
    IF SCORE = 100 THEN
		D2$ = "YOU HAVE WON THE GAME!"
    print D2$

def FN_19999_END_GAME():
    REM # --- END GAME ---
    EXIT
    END

