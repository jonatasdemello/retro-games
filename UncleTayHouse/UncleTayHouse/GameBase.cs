namespace UncleTayHouse
{
    public partial class Game
    {
        #region Variables

        // player: current location
        public int LOCAL = 1;

        public int SAFEDoor = 0;
        public int TURN1 = 0;

        public int CMD1 = 0;
        public int CMD2 = 0;
        public int CMD3 = 0;

        public int InputWordTotal = 0;

        // item offset - where objects start in Vocab array
        public const int OBJECTOFFSET = 33;

        // words to be ignored
        public string[] NULLWORDS = {
            "THE", "TO", "WITH", "USING", "IN", "GO"
        };

        // NOTE: adding one more item to the arrays because BASIC arrays start at 1

        // All Verbs and Objects
        public string[] VOCABS = {
            /* 00 */     "-99", // 0-dummy

            /* 01 */     "NORTH",
            /* 02 */     "SOUTH",
            /* 03 */     "EAST",
            /* 04 */     "WEST",
            /* 05 */     "UP",
            /* 06 */     "DOWN",
            /* 07 */     "N",
            /* 08 */     "S",
            /* 09 */     "E",
            /* 10 */     "W",
            /* 11 */     "U",
            /* 12 */     "D",

            /* 13 */     "I",
            /* 14 */     "INVENTORY",
            /* 15 */     "SCORE",
            /* 16 */     "JUMP",
            /* 17 */     "HELP",
            /* 18 */     "TAKE",
            /* 19 */     "DROP",
            /* 20 */     "LOOK",

            /* 21 */     "READ",
            /* 22 */     "EXAMINE",
            /* 23 */     "UNLOCK",
            /* 24 */     "EAT",
            /* 25 */     "SPIN",
            /* 26 */     "MOVE",
            /* 27 */     "OPEN",
            /* 28 */     "TIE",
            /* 29 */     "OIL",
            /* 30 */     "PUT",

            /* 31 */     "LEFT",
            /* 32 */     "CENTER",
            /* 33 */     "RIGHT",

            /* 34 */     "NEWSPAPER",
            /* 35 */     "TEDDYBEAR",
            /* 36 */     "FUSE",
            /* 37 */     "JACK",
            /* 38 */     "PICTURE",
            /* 39 */     "BUNGEE",
            /* 40 */     "KEY",
            /* 41 */     "TOP",
            /* 42 */     "NOTE",
            /* 43 */     "GAINESBURGER",
            /* 44 */     "GLOVES",
            /* 45 */     "BOXSPRING",
            /* 46 */     "BRACE",
            /* 47 */     "MAGAZINE",
            /* 48 */     "OILCAN",
            /* 49 */     "CHECKBOOK",
            /* 50 */     "DIAMOND",
            /* 51 */     "LOVERBOY",
            /* 52 */     "INVESTMENT",
            /* 53 */     "LOONS",
            /* 54 */     "FRIDGE",
            /* 55 */     "COUCH",
            /* 56 */     "CLOTHES",
            /* 57 */     "DOOR",
            /* 58 */     "RAILING",
            /* 59 */     "DUMBWAITER",
            /* 60 */     "FUSEBOX",
            /* 61 */     "MIRROR",
            /* 62 */     "L", // look
            /* 63 */     "G", // get
            /* 64 */     "X", // examine


            };

        // Location description
        public string[] IDESCS = {
            /* 00 */    "-99", // 0-dummy
            /* 01 */    "Tays house unlikely ever to be sold. tales of gutted stairwells and booby traps have spooked buyers...",
            /* 02 */    "Someone has been playing very rough with this toy",
            /* 03 */    "Old-fashioned electrical fuse",
            /* 04 */    "Tire jack for lifting heavy objects like cars",
            /* 05 */    "Uncle tays in all his sallow glory",
            /* 06 */    "Cord for bungee jumping",
            /* 07 */    "A small brass key",
            /* 08 */    "A child's toy (spinning top)",
            /* 09 */    "The writing is reversed. maybe there is a way to read it somewhere...",
            /* 10 */    "Supposedly dog food, though it appears to be made of plastic",
            /* 11 */    "Rubber gloves used for cleaning",
            /* 12 */    "A queen-sized boxspring",
            /* 13 */    "A back brace",
            /* 14 */    "Tays' strange inventions include booby-trapped doors and toys that open doors by remote control...",
            /* 15 */    "This can contains fine lubricating oil",
            /* 16 */    "Uncle tays' checkbook lists a balance of $220,000",
            /* 17 */    "This diamond's beauty stems from all the goddamned money it is worth",
            /* 18 */    "Loverboy's first album in vinyl, worth an incalculable sum",
            /* 19 */    "Pre-ipo shares of apollo computing have to be worth ... something",
            /* 20 */    "A thick wad of canadian notes",
            /* 21 */    "This old refrigerator's motor labors heavily",
            /* 22 */    "An overstuffed, dusty couch",
            /* 23 */    "A disgusting pile of soiled laundry",
            /* 24 */    "",
            /* 25 */    "A railing or guardrail, is a system designed to keep people or objects from falling off the balcony.",
            /* 26 */    "A dumbwaiter lift is a small freight elevator designed to transport goods, supplies, or food between different levels of a building.",
            /* 27 */    "An old-fashioned fusebox. the fuse marked 'attic' is missing."
                    };

        // location name
        public string[] LocationName_RNAMES = {
            /* 00 */    "-99", // 0-dummy
            /* 01 */    "FOYER (LOBBY)",
            /* 02 */    "KITCHEN",
            /* 03 */    "SITTING ROOM",
            /* 04 */    "HALLWAY",
            /* 05 */    "HALLWAY",
            /* 06 */    "DEN",
            /* 07 */    "BATHROOM",
            /* 08 */    "LIBRARY",
            /* 09 */    "SMALL BEDROOM",
            /* 10 */    "GARAGE",
            /* 11 */    "DINING ROOM",
            /* 12 */    "BALCONY",
            /* 13 */    "MASTER BEDROOM",
            /* 14 */    "HALLWAY",
            /* 15 */    "GAME ROOM",
            /* 16 */    "CLOSET",
            /* 17 */    "HALLWAY",
            /* 18 */    "CHILD'S ROOM",
            /* 19 */    "SECRET ROOM1",
            /* 20 */    "DANGEROUS HALL",
            /* 21 */    "CORNER BEDROOM",
            /* 22 */    "BATHROOM",
            /* 23 */    "DUMBWAITER",
            /* 24 */    "DUMBWAITER",
            /* 25 */    "ATTIC",
            /* 26 */    "STORAGE ROOM",
            /* 27 */    "LAUNDRY",
            /* 28 */    "WORK ROOM",
            /* 29 */    "BOTTOM OF STAIRS",
            /* 30 */    "MID-AIR",
            /* 31 */    "LEAVE THE HOUSE (AND THE GAME)"
        };

        // location extended description
        public string[] LocationDescription_RDESCS = {
            /* 00 */    "-99", // 0-dummy
            /* 01 */    "The entryway to the house",
            /* 02 */    "Countertops are dusty and there are rusting pots and pans",
            /* 03 */    "This room is two stories high and contains elegant chairs and couches",
            /* 04 */    "A narrow hallway which runs west of the foyer",
            /* 05 */    "A narrow hallway at the west end of the house",
            /* 06 */    "This room has an ancient television",
            /* 07 */    "A dingy bathroom with a cracked sink",
            /* 08 */    "This well-furnished library is lined with books and leather furniture",
            /* 09 */    "This small bedroom has a twin bed and chair. It looks little used",
            /* 10 */    "The cavernous garage holds a non-operational gremlin and piles of junk",
            /* 11 */    "Trophies line the walls. there are six chairs around a long table",
            /* 12 */    "Balcony above the sitting room. a railing protects you from a 15-foot drop",
            /* 13 */    "This large corner bedroom has solid walnut furniture and a large mirror",
            /* 14 */    "A hallway with a large arch on its south side",
            /* 15 */    "This elegant game room has a pool table and marble chessboard",
            /* 16 */    "A spacious closet off the gameroom",
            /* 17 */    "A hallway in the center of the second floor",
            /* 18 */    "Your cousin's room in happier times, before he ran off to join the baath party",
            /* 19 */    "A dark chamber off the bedroom",
            /* 20 */    "This eerie hall has three identical doors on the west wall (left, center, right)",
            /* 21 */    "A cozy corner room with windows on two walls",
            /* 22 */    "An elegant bath with a mirror over a marble sink",
            /* 23 */    "A cramped dumbwaiter",
            /* 24 */    "A cramped dumbwaiter",
            /* 25 */    "A dusty attic with low sloping walls",
            /* 26 */    "A bare room used to store random equipment and furniture",
            /* 27 */    "This room has a washer and dryer, as well as a boiler and furnace",
            /* 28 */    "Equipment for working wood and metal",
            /* 29 */    "Stairs from basement to kitchen",
            /* 30 */    "Hanging from a bungee cord",
            /* 31 */    ""
        };

        // # DIM REXIT[31][6] : : LOCATION EXITS
        public int[,] LocationExit = {
                        // 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
                        //      N      S      E      W      U      D
            /* 00 */    {-99, -99  , -99  , -99  , -99  , -99  , -99  },//0-dummy
            /* 01 */    {-99,   2  ,  31  ,   3  ,   4  ,   0  ,   0  },
            /* 02 */    {-99,   0  ,   1  ,   0  ,   0  ,   0  ,  -1  }, // D: STAIRS TO BASEMENT is hidden until drop Boxspring
            /* 03 */    {-99,   2  ,   0  ,  11  ,   1  ,  12  ,   0  },
            /* 04 */    {-99,   6  ,   7  ,   1  ,   5  ,   0  ,   0  },
            /* 05 */    {-99,   0  ,   9  ,   4  ,  10  ,   0  ,   0  },
            /* 06 */    {-99,   0  ,   4  ,   0  ,   0  ,   0  ,   0  },
            /* 07 */    {-99,   4  ,   0  ,   0  ,   0  ,   0  ,  -1  }, // D: move clothes with gloves
            /* 08 */    {-99,   0  ,   0  ,   0  ,  24  ,   0  ,   0  },
            /* 09 */    {-99,   5  ,   0  ,   0  ,   0  ,   0  ,   0  },
            /* 10 */    {-99,   0  ,   0  ,   5  ,   0  ,   0  ,   0  },
            /* 11 */    {-99,   0  ,   0  ,   0  ,   3  ,   0  ,   0  },
            /* 12 */    {-99,  13  ,   0  ,   0  ,  14  ,  -1  ,   3  }, // W: STAIRS TO ATTIC is hidden until fuse is inserted
            /* 13 */    {-99,   0  ,  12  ,   0  ,   0  ,   0  ,   0  },
            /* 14 */    {-99,   0  ,  15  ,  12  ,  17  ,   0  ,   0  },
            /* 15 */    {-99,  14  ,   0  ,   0  ,  16  ,   0  ,   0  },
            /* 16 */    {-99,   0  ,   0  ,  15  ,   0  ,   0  ,   0  },
            /* 17 */    {-99,  -1  ,   0  ,  14  ,  -1  ,   0  ,   0  }, // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            /* 18 */    {-99,   0  ,  17  ,  -1  ,   0  ,   0  ,   0  }, // E: secret room hidden until sping top
            /* 19 */    {-99,   0  ,   0  ,   0  ,  18  ,   0  ,   0  },
            /* 20 */    {-99,  21  ,  22  ,  17  ,  -1  ,   0  ,   0  }, // W: Dangerous Hall, open X door (after reading note)
            /* 21 */    {-99,   0  ,  20  ,   0  ,   0  ,   0  ,   0  },
            /* 22 */    {-99,  20  ,   0  ,   0  ,   0  ,   0  ,   0  },
            /* 23 */    {-99,   0  ,   0  ,  20  ,   0  ,   0  ,  -1  }, // D: unlock with oilcan
            /* 24 */    {-99,   0  ,   0  ,   8  ,   0  ,  23  ,   0  },
            /* 25 */    {-99,   0  ,   0  ,   0  ,   0  ,   0  ,  12  },
            /* 26 */    {-99,   0  ,   0  ,  27  ,   0  ,   0  ,   0  },
            /* 27 */    {-99,   0  ,   0  ,  28  ,  26  ,   0  ,   0  },
            /* 28 */    {-99,   0  ,   0  ,  29  ,  27  ,   0  ,   0  },
            /* 29 */    {-99,   0  ,   0  ,   0  ,  28  ,  -1  ,   0  }, // U: unlock with drop boxspring
            /* 30 */    {-99,   0  ,   0  ,   0  ,   0  ,   0  ,   0  },
            /* 31 */    {-99,   0  ,   0  ,   0  ,   0  ,   0  ,   0  }
        };

        // if ILOC[obj] == 0 => player is carrying object
        // if ILOC[obj] == -1 => object is hidden

        // each item location and if player is carrying it
        public int[] ILOC = {
            /* 00 */    -99,     //dummy
            /* 01 */     1,      // "NEWSPAPER",
            /* 02 */    -1,      // "TEDDYBEAR", -1
            /* 03 */    -1,      // "FUSE",
            /* 04 */    10,      // "JACK", 10
            /* 05 */    30,      // "PICTURE",
            /* 06 */    10,      // "BUNGEE", 10
            /* 07 */    13,      // "KEY", 13
            /* 08 */    15,      // "TOP", 15
            /* 09 */     9,      // "NOTE", 9
            /* 10 */    16,      // "GAINESBURGER", 16
            /* 11 */    22,      // "GLOVES", 22
            /* 12 */    26,      // "BOXSPRING",
            /* 13 */    25,      // "BRACE", 25
            /* 14 */    25,      // "MAGAZINE",
            /* 15 */    28,      // "OILCAN", 28
            /* 16 */     8,      // "CHECKBOOK",
            /* 17 */    -1,      // "DIAMOND",
            /* 18 */    19,      // "LOVERBOY",
            /* 19 */    21,      // "INVESTMENT",
            /* 20 */    27,      // "LOONS",
            /* 21 */     2,      // "FRIDGE",
            /* 22 */     6,      // "COUCH",
            /* 23 */     7,      // "CLOTHES",
            /* 24 */    -1,      // "DOOR",
            /* 25 */    12,      // "RAILING",
            /* 26 */    -1,      // "DUMBWAITER",
            /* 27 */    -1       // "FUSEBOX"
        };

        // extended description
        public string[] ExtendedDescription = {
            /* 00 */    "-99", // 0-dummy
            /* 01 */    "There is a locked door to the NORTH",
            /* 02 */    "There is a locked door to the SOUTH",
            /* 03 */    "Stairs lead down to a cellar. Several steps have collapsed, making the staircase unusable",
            /* 04 */    "Stairs lead up. several steps have collapsed, making the staircase unusable",
            /* 05 */    "Dark stairs lead up to the attic",
            /* 06 */    "A locked door to the WEST is labelled 'EXTREME DANGER'",
            /* 07 */    "Your uncle's doberman blocks a doorway to the north"
        };

        // # DIM EXLOC[7][2] : EXTENDED location? (map to LocationExit)
        // pos1 = location
        // pos3 = 1-NORTH, 2-SOUTH, 3-EAST, 4-WEST, 5-UP, 6-DOWN
        public int[,] EXLOC = {
            /* 00 */    { -99, -99  , -99 }, // dummy
            /* 01 */    { -99,  5   ,  6  }, // hallway
            /* 02 */    { -99,  8   ,  6  }, // library
            /* 03 */    { -99,  2   ,  6  }, // kitchen
            /* 04 */    { -99,  29  ,  5  }, // bottom of stairs
            /* 05 */    { -99,  12  ,  5  }, // balcony
            /* 06 */    { -99,  17  ,  4  }, // hallway end floor
            /* 07 */    { -99,  17  ,  1  }  // hallway end floor
        };

        #endregion Variables
    }
}