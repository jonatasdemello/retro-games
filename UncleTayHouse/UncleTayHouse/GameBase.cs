namespace UncleTayHouse
{
	public partial class Game
	{
		#region Variables

		// int SP = 0;

		int DIROFF = 30;
		int ITEMOFF = 33;
		int LASTITEM = 27;
		int IMMOFF = 20;
		int LWRD = 60;
		int NXDESC = 7;
		int SAFED = 0;
		int LCL = 1;
		int IC = 0;
		int SCORE = 50;
		int INWRD = 0;
		int WIDX = 0;
		int COMM = 0;
		int ARG = 0;
		int MVARG = 0;
		int NEIGH = 0;
		int TURN1 = 0;
		int CURTOK = 0;
		int ISNULLW = 0;
		int NTOK = 0;
		int AIMM = 0;
		int GOARG = 0;
		int DOORDIR = 0;

		string D2S = "";
		string IS = "";
		string CS = "";
		string NTMSGS = "";

		// NOTE: adding one more item to the arrays because BASIC arrays start at 1

		// # DIM VOCABS[60]
		string[] VOCABS = {
			"-99", // 0-dummy
            "NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN", "N", "S", "E", "W", "U", "D",
			"I", "INVENTORY", "SCORE", "JUMP", "HELP",
			"TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN",
			"MOVE", "OPEN", "TIE", "OIL", "PUT", "LEFT", "CENTER", "RIGHT",
			"NEWSPAPER", "TEDDYBEAR", "FUSE", "JACK", "PICTURE", "BUNGEE",
			"KEY", "TOP", "NOTE", "GAINESBURGER", "GLOVES", "BOXSPRING",
			"BRACE", "MAGAZINE", "OILCAN", "CHECKBOOK", "DIAMOND", "LOVERBOY",
			"INVESTMENT", "LOONS", "FRIDGE", "COUCH", "CLOTHES", "DOOR",
			"RAILING", "DUMBWAITER", "FUSEBOX"
		};

		// # DIM NULLWS[4]
		string[] NULLWS = {
			"THE", "TO", "WITH", "USING"
		};

		// # DIM IDESC$[27]
		string[] IDESCS = {
			"-99", // 0-dummy
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
		};

		// # DIM RNAMES[31] : LOCATION NAME
		string[] RNAMES = {
			"-99", // 0-dummy
            "FOYER (LOBBY)",
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
		};

		// # DIM RDESCS[31] : LOCATION DESCRIPTION
		string[] RDESCS = {
			"-99", // 0-dummy
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
		};

		// # DIM REXIT[31][6] : : LOCATION EXITS
		int[,] REXIT = {
            // NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN",
			{ -99, -99, -99, -99 , -99 , -99 , -99  }, //  0-dummy
            { -99,  2 , 31, 3 , 4 , 0 , 0  }, //  00
            { -99,  0 , 1 , 0 , 0 , 0 , -1 }, //  01
            { -99,  2 , 0 , 11, 1 , 12, 0  }, //  02
            { -99,  6 , 7 , 1 , 5 , 0 , 0  }, //  03
            { -99,  0 , 9 , 4 , 10, 0 , 0  }, //  04
            { -99,  0 , 4 , 0 , 0 , 0 , 0  }, //  05
            { -99,  4 , 0 , 0 , 0 , 0 , -1 }, //  06
            { -99,  0 , 0 , 0 , 24, 0 , 0  }, //  07
            { -99,  5 , 0 , 0 , 0 , 0 , 0  }, //  08
            { -99,  0 , 0 , 5 , 0 , 0 , 0  }, //  09
            { -99,  0 , 0 , 0 , 3 , 0 , 0  }, //  10
            { -99,  13, 0 , 0 , 14, -1, 3  }, //  11
            { -99,  0 , 12, 0 , 0 , 0 , 0  }, //  12
            { -99,  0 , 15, 12, 17, 0 , 0  }, //  13
            { -99,  14, 0 , 0 , 16, 0 , 0  }, //  14
            { -99,  0 , 0 , 15, 0 , 0 , 0  }, //  15
            { -99,  -1, 0 , 14, -1, 0 , 0  }, //  16
            { -99,  0 , 17, -1, 0 , 0 , 0  }, //  17
            { -99,  0 , 0 , 0 , 18, 0 , 0  }, //  18
            { -99,  21, 22, 17, -1, 0 , 0  }, //  19
            { -99,  0 , 20, 0 , 0 , 0 , 0  }, //  20
            { -99,  20, 0 , 0 , 0 , 0 , 0  }, //  21
            { -99,  0 , 0 , 20, 0 , 0 , -1 }, //  22
            { -99,  0 , 0 , 8 , 0 , 23, 0  }, //  23
            { -99,  0 , 0 , 0 , 0 , 0 , 12 }, //  24
            { -99,  0 , 0 , 27, 0 , 0 , 0  }, //  25
            { -99,  0 , 0 , 28, 26, 0 , 0  }, //  26
            { -99,  0 , 0 , 29, 27, 0 , 0  }, //  27
            { -99,  0 , 0 , 0 , 28, -1, 0  }, //  28
            { -99,  0 , 0 , 0 , 0 , 0 , 0  }, //  29
            { -99,  0 , 0 , 0 , 0 , 0 , 0  }  //  30
        };

		// # DIM ILOC[27]
		int[] ILOC = {
			-99, // dummy
			1 , -1, -1, 10, 30,
			10, 13, 15,  9, 16,
			22, 26, 25, 25, 28,
			8 , -1, 19, 21, 27,
			2 ,  6,  7, -1, 12,
			-1, -1
		};

		// # DIM EXDESCS[7] : EXTENDED DESCRIPTION
		string[] EXDESCS = {
			"-99", // 0-dummy
            "THERE IS A LOCKED DOOR TO THE NORTH",
			"THERE IS A LOCKED DOOR TO THE SOUTH",
			"STAIRS LEAD DOWN TO A CELLAR.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
			"STAIRS LEAD UP.  SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
			"DARK STAIRS LEAD UP TO THE ATTIC",
			"A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'",
			"YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH"
		};

		// # DIM EXLOC[7][2] : EXTENDED ?
		int[,] EXLOC = {
			{ -99, -99, -99 }, // dummy
			{ -99,  5 , 6 },
			{ -99,  8 , 6 },
			{ -99,  2 , 6 },
			{ -99,  29, 5 },
			{ -99,  12, 5 },
			{ -99,  17, 4 },
			{ -99,  17, 1 },
		};

		// # DIM INWS[10)
		string[] INWS = new string[10];

		// # DIM INPTK[10]
		int[] INPTK = new int[10];

		#endregion
	}
}