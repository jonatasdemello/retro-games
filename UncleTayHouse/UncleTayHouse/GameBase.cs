namespace UncleTayHouse
{
    public partial class Game
    {
        #region Variables

        // player: current location
        public int LCL = 1;
        public int SAFED = 0;
        public int TURN1 = 0;

        public int CMD = 0;
        public int ARG = 0;
        public int MVARG = 0;

        public int InputWordTotal = 0;
        const int MaxInput = 4;

        // # DIM INWS[10] : input words
        public string[] InputWordText_INWS = new string[MaxInput];

        // # DIM INPTK[10] : input processed keyword
        public int[] InputWordNum_INPTK = new int[MaxInput];

        // item offset - where objects start at Vocab
        public int ITEMOFF = 33;
        public int IMMOFF = 20;
        public int DIROFF = 30;
        public int LASTITEM = 27;


        // # DIM NULLWS[4] : words to be ignored
        public string[] NULLWORDS = {
            "THE", "TO", "WITH", "USING", "IN", "GO"
        };

        // NOTE: adding one more item to the arrays because BASIC arrays start at 1

        // # DIM VOCABS[60] , LWRD = 60;
        public string[] VOCABS = {
/* 00 */	 "-99", // 0-dummy

/* 01 */	 "NORTH",
/* 02 */	 "SOUTH",
/* 03 */	 "EAST",
/* 04 */	 "WEST",
/* 05 */	 "UP",
/* 06 */	 "DOWN",
/* 07 */	 "N",
/* 08 */	 "S",
/* 09 */	 "E",
/* 10 */	 "W",
/* 11 */	 "U",
/* 12 */	 "D",

/* 13 */	 "I",
/* 14 */	 "INVENTORY",
/* 15 */	 "SCORE",
/* 16 */	 "JUMP",
/* 17 */	 "HELP",
/* 18 */	 "TAKE",
/* 19 */	 "DROP",
/* 20 */	 "LOOK",

/* 21 */	 "READ",
/* 22 */	 "EXAMINE",
/* 23 */	 "UNLOCK",
/* 24 */	 "EAT",
/* 25 */	 "SPIN",
/* 26 */	 "MOVE",
/* 27 */	 "OPEN",
/* 28 */	 "TIE",
/* 29 */	 "OIL",
/* 30 */	 "PUT",

/* 31 */	 "LEFT",
/* 32 */	 "CENTER",
/* 33 */	 "RIGHT",

/* 34 */	 "NEWSPAPER",
/* 35 */	 "TEDDYBEAR",
/* 36 */	 "FUSE",
/* 37 */	 "JACK",
/* 38 */	 "PICTURE",
/* 39 */	 "BUNGEE",
/* 40 */	 "KEY",
/* 41 */	 "TOP",
/* 42 */	 "NOTE",
/* 43 */	 "GAINESBURGER",
/* 44 */	 "GLOVES",
/* 45 */	 "BOXSPRING",
/* 46 */	 "BRACE",
/* 47 */	 "MAGAZINE",
/* 48 */	 "OILCAN",
/* 49 */	 "CHECKBOOK",
/* 50 */	 "DIAMOND",
/* 51 */	 "LOVERBOY",
/* 52 */	 "INVESTMENT",
/* 53 */	 "LOONS",
/* 54 */	 "FRIDGE",
/* 55 */	 "COUCH",
/* 56 */	 "CLOTHES",
/* 57 */	 "DOOR",
/* 58 */	 "RAILING",
/* 59 */	 "DUMBWAITER",
/* 60 */	 "FUSEBOX"
            };


        // # DIM IDESC$[27]
        public string[] IDESCS = {
/* 00 */	"-99", // 0-dummy
/* 01 */	"TAYS HOUSE UNLIKELY EVER TO BE SOLD. TALES OF GUTTED STAIRWELLS AND BOOBY TRAPS HAVE SPOOKED BUYERS...",
/* 02 */	"SOMEONE HAS BEEN PLAYING VERY ROUGH WITH THIS TOY",
/* 03 */	"OLD-FASHIONED ELECTRICAL FUSE",
/* 04 */	"TIRE JACK for LIFTING HEAVY OBJECTS LIKE CARS",
/* 05 */	"UNCLE TAYS IN ALL HIS SALLOW GLORY",
/* 06 */	"CORD for BUNGEE JUMPING",
/* 07 */	"A SMALL BRASS KEY",
/* 08 */	"A CHILD'S TOY",
/* 09 */	"THE WRITING IS REVERSED. Maybe there is a way to read it somewhere...",
/* 10 */	"SUPPOSEDLY DOG FOOD, THOUGH IT APPEARS TO BE MADE OF PLASTIC",
/* 11 */	"RUBBER GLOVES USED for CLEANING",
/* 12 */	"A QUEEN-SIZED BOXSPRING",
/* 13 */	"A BACK BRACE",
/* 14 */	"TAYS' STRANGE INVENTIONS INCLUDE BOOBY-TRAPPED DOORS AND TOYS THAT OPEN DOORS BY REMOTE CONTROL...",
/* 15 */	"THIS CAN CONTAINS FINE LUBRICATING OIL",
/* 16 */	"UNCLE TAYS' CHECKBOOK LISTS A BALANCE OF $220,000",
/* 17 */	"THIS DIAMOND'S BEAUTY STEMS FROM ALL THE GODDAMNED MONEY IT IS WORTH",
/* 18 */	"LOVERBOY'S FIRST ALBUM IN VINYL, WORTH AN INCALCULABLE SUM",
/* 19 */	"PRE-IPO SHARES OF APOLLO COMPUTING HAVE TO BE WORTH ... SOMETHING",
/* 20 */	"A THICK WAD OF CANADIAN NOTES",
/* 21 */	"THIS OLD REFRIGERATOR'S MOTOR LABORS HEAVILY",
/* 22 */	"AN OVERSTUFFED, DUSTY COUCH",
/* 23 */	"A DISGUSTING PILE OF SOILED LAUNDRY",
/* 24 */	"",
/* 25 */	"",
/* 26 */	"",
/* 27 */	"AN OLD-FASHIONED FUSEBOX. THE FUSE MARKED 'ATTIC' IS MISSING."
        };

        // # DIM RNAMES[31] : LOCATION NAME
        public string[] LocationName_RNAMES = {
/* 00 */	"-99", // 0-dummy
/* 01 */	"FOYER (LOBBY)",
/* 02 */	"KITCHEN",
/* 03 */	"SITTING ROOM",
/* 04 */	"HALLWAY",
/* 05 */	"HALLWAY",
/* 06 */	"DEN",
/* 07 */	"BATHROOM",
/* 08 */	"LIBRARY",
/* 09 */	"SMALL BEDROOM",
/* 10 */	"GARAGE",
/* 11 */	"DINING ROOM",
/* 12 */	"BALCONY",
/* 13 */	"MASTER BEDROOM",
/* 14 */	"HALLWAY",
/* 15 */	"GAME ROOM",
/* 16 */	"CLOSET",
/* 17 */	"HALLWAY",
/* 18 */	"CHILD'S ROOM",
/* 19 */	"SECRET ROOM1",
/* 20 */	"DANGEROUS HALL",
/* 21 */	"CORNER BEDROOM",
/* 22 */	"BATHROOM",
/* 23 */	"DUMBWAITER",
/* 24 */	"DUMBWAITER",
/* 25 */	"ATTIC",
/* 26 */	"STORAGE ROOM",
/* 27 */	"LAUNDRY",
/* 28 */	"WORK ROOM",
/* 29 */	"BOTTOM OF STAIRS",
/* 30 */	"MID-AIR",
/* 31 */	"LEAVE THE HOUSE (AND THE GAME)"
        };

        // # DIM RDESCS[31] : LOCATION DESCRIPTION
        public string[] LocationDescription_RDESCS = {
/* 00 */	"-99", // 0-dummy
/* 01 */	"THE ENTRYWAY TO THE HOUSE",
/* 02 */	"COUNTERTOPS ARE DUSTY AND THERE ARE RUSTING POTS AND PANS",
/* 03 */	"THIS ROOM IS TWO STORIES HIGH AND CONTAINS ELEGANT CHAIRS AND COUCHES",
/* 04 */	"A NARROW HALLWAY WHICH RUNS WEST OF THE FOYER",
/* 05 */	"A NARROW HALLWAY AT THE WEST END OF THE HOUSE",
/* 06 */	"THIS ROOM HAS AN ANCIENT TELEVISION",
/* 07 */	"A DINGY BATHROOM WITH A CRACKED SINK",
/* 08 */	"THIS WELL-FURNISHED LIBRARY IS LINED WITH BOOKS AND LEATHER FURNITURE",
/* 09 */	"THIS SMALL BEDROOM HAS A TWIN BED AND CHAIR. IT LOOKS LITTLE USED.",
/* 10 */	"THE CAVERNOUS GARAGE HOLDS A NON-OPERATIONAL GREMLIN AND PILES OF JUNK",
/* 11 */	"TROPHIES LINE THE WALLS. THERE ARE SIX CHAIRS AROUND A LONG TABLE.",
/* 12 */	"BALCONY ABOVE THE SITTING ROOM. A RAILING PROTECTS YOU FROM A 15-FOOT DROP.",
/* 13 */	"THIS LARGE CORNER BEDROOM HAS SOLID WALNUT FURNITURE AND A LARGE MIRROR",
/* 14 */	"A HALLWAY WITH A LARGE ARCH ON ITS SOUTH SIDE",
/* 15 */	"THIS ELEGANT GAME ROOM HAS A POOL TABLE AND MARBLE CHESSBOARD",
/* 16 */	"A SPACIOUS CLOSET OFF THE GAMEROOM",
/* 17 */	"A HALLWAY IN THE CENTER OF THE SECOND FLOOR",
/* 18 */	"YOUR COUSIN'S ROOM IN HAPPIER TIMES, BEFORE HE RAN OFF TO JOIN THE BAATH PARTY",
/* 19 */	"A DARK CHAMBER OFF THE BEDROOM",
/* 20 */	"THIS EERIE HALL HAS THREE IDENTICAL DOORS ON THE WEST WALL (LEFT, CENTER, RIGHT)",
/* 21 */	"A COZY CORNER ROOM WITH WINDOWS ON TWO WALLS",
/* 22 */	"AN ELEGANT BATH WITH A MIRROR OVER A MARBLE SINK",
/* 23 */	"A CRAMPED DUMBWAITER",
/* 24 */	"A CRAMPED DUMBWAITER",
/* 25 */	"A DUSTY ATTIC WITH LOW SLOPING WALLS",
/* 26 */	"A BARE ROOM USED TO STORE RANDOM EQUIPMENT AND FURNITURE",
/* 27 */	"THIS ROOM HAS A WASHER AND DRYER, AS WELL AS A BOILER AND FURNACE",
/* 28 */	"EQUIPMENT for WORKING WOOD AND METAL",
/* 29 */	"STAIRS FROM BASEMENT TO KITCHEN",
/* 30 */	"HANGING FROM A BUNGEE CORD",
/* 31 */	""
        };

        // # DIM REXIT[31][6] : : LOCATION EXITS
        public int[,] LocationExit = {
			// 0  1="NORTH", 2="SOUTH", 3="EAST", 4="WEST", 5="UP", 6="DOWN"
/* 00 */	{-99, -99  , -99  , -99  , -99  , -99  , -99  },//0-dummy
/* 01 */	{-99,   2  ,  31  ,   3  ,   4  ,   0  ,   0  },
/* 02 */	{-99,   0  ,   1  ,   0  ,   0  ,   0  ,  -1  },
/* 03 */	{-99,   2  ,   0  ,  11  ,   1  ,  12  ,   0  },
/* 04 */	{-99,   6  ,   7  ,   1  ,   5  ,   0  ,   0  },
/* 05 */	{-99,   0  ,   9  ,   4  ,  10  ,   0  ,   0  },
/* 06 */	{-99,   0  ,   4  ,   0  ,   0  ,   0  ,   0  },
/* 07 */	{-99,   4  ,   0  ,   0  ,   0  ,   0  ,  -1  },
/* 08 */	{-99,   0  ,   0  ,   0  ,  24  ,   0  ,   0  },
/* 09 */	{-99,   5  ,   0  ,   0  ,   0  ,   0  ,   0  },
/* 10 */	{-99,   0  ,   0  ,   5  ,   0  ,   0  ,   0  },
/* 11 */	{-99,   0  ,   0  ,   0  ,   3  ,   0  ,   0  },
/* 12 */	{-99,  13  ,   0  ,   0  ,  14  ,  -1  ,   3  }, // STAIRS TO ATTIC is hidden until fuse is inserted
/* 13 */	{-99,   0  ,  12  ,   0  ,   0  ,   0  ,   0  },
/* 14 */	{-99,   0  ,  15  ,  12  ,  17  ,   0  ,   0  },
/* 15 */	{-99,  14  ,   0  ,   0  ,  16  ,   0  ,   0  },
/* 16 */	{-99,   0  ,   0  ,  15  ,   0  ,   0  ,   0  },
/* 17 */	{-99,  -1  ,   0  ,  14  ,  -1  ,   0  ,   0  },
/* 18 */	{-99,   0  ,  17  ,  -1  ,   0  ,   0  ,   0  },
/* 19 */	{-99,   0  ,   0  ,   0  ,  18  ,   0  ,   0  },
/* 20 */	{-99,  21  ,  22  ,  17  ,  -1  ,   0  ,   0  },
/* 21 */	{-99,   0  ,  20  ,   0  ,   0  ,   0  ,   0  },
/* 22 */	{-99,  20  ,   0  ,   0  ,   0  ,   0  ,   0  },
/* 23 */	{-99,   0  ,   0  ,  20  ,   0  ,   0  ,  -1  },
/* 24 */	{-99,   0  ,   0  ,   8  ,   0  ,  23  ,   0  },
/* 25 */	{-99,   0  ,   0  ,   0  ,   0  ,   0  ,  12  },
/* 26 */	{-99,   0  ,   0  ,  27  ,   0  ,   0  ,   0  },
/* 27 */	{-99,   0  ,   0  ,  28  ,  26  ,   0  ,   0  },
/* 28 */	{-99,   0  ,   0  ,  29  ,  27  ,   0  ,   0  },
/* 29 */	{-99,   0  ,   0  ,   0  ,  28  ,  -1  ,   0  },
/* 30 */	{-99,   0  ,   0  ,   0  ,   0  ,   0  ,   0  },
/* 31 */	{-99,   0  ,   0  ,   0  ,   0  ,   0  ,   0  }
        };

        // if ILOC[obj] == 0 => player is carrying object
        // if ILOC[obj] == -1 => object is hidden
        // # DIM ILOC[27]
        public int[] ILOC = {
/* 00 */	-99,//dummy
/* 01 */	 1,		 // "NEWSPAPER",
/* 02 */	-1,		 // "TEDDYBEAR",
/* 03 */	-1,		 // "FUSE",
/* 04 */	0,		 // "JACK", 10
/* 05 */	30,		 // "PICTURE",
/* 06 */	0,		 // "BUNGEE", 10
/* 07 */	0,		 // "KEY", 13
/* 08 */	15,		 // "TOP",
/* 09 */	 0,		 // "NOTE", 9
/* 10 */	16,		 // "GAINESBURGER",
/* 11 */	22,		 // "GLOVES",
/* 12 */	26,		 // "BOXSPRING",
/* 13 */	25,		 // "BRACE",
/* 14 */	25,		 // "MAGAZINE",
/* 15 */	28,		 // "OILCAN",
/* 16 */	 8,		 // "CHECKBOOK",
/* 17 */	-1,		 // "DIAMOND",
/* 18 */	19,		 // "LOVERBOY",
/* 19 */	21,		 // "INVESTMENT",
/* 20 */	27,		 // "LOONS",
/* 21 */	 2,		 // "FRIDGE",
/* 22 */	 6,		 // "COUCH",
/* 23 */	 7,		 // "CLOTHES",
/* 24 */	-1,		 // "DOOR",
/* 25 */	12,		 // "RAILING",
/* 26 */	-1,		 // "DUMBWAITER",
/* 27 */	-1		 // "FUSEBOX"
		};

        // # DIM EXDESCS[7] : EXTENDED DESCRIPTION
        public string[] ExtendedDescription = {
/* 00 */	"-99", // 0-dummy
/* 01 */	"THERE IS A LOCKED DOOR TO THE NORTH",
/* 02 */	"THERE IS A LOCKED DOOR TO THE SOUTH",
/* 03 */	"STAIRS LEAD DOWN TO A CELLAR. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
/* 04 */	"STAIRS LEAD UP. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
/* 05 */	"DARK STAIRS LEAD UP TO THE ATTIC (it is too DARK to see the way)",
/* 06 */	"A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'",
/* 07 */	"YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH"
        };

        // # DIM EXLOC[7][2] : EXTENDED location?
        public int[,] EXLOC = {
/* 00 */	{ -99, -99  , -99 }, // dummy
/* 01 */	{ -99,  5   ,  6  },
/* 02 */	{ -99,  8   ,  6  },
/* 03 */	{ -99,  2   ,  6  },
/* 04 */	{ -99,  29  ,  5  },
/* 05 */	{ -99,  12  ,  5  },
/* 06 */	{ -99,  17  ,  4  },
/* 07 */	{ -99,  17  ,  1  }
        };

        #endregion
    }
}