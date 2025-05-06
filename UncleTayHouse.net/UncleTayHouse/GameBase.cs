using UncleTayHouse.Models;

namespace UncleTayHouse
{

    public partial class Game
    {
        #region Variables

        // extended description
        public ExtDesc[] extDesc { get; } =
        [
            new ExtDesc { location = 5, direction = 6, description = "There is a locked door to the north." },
            new ExtDesc { location = 8, direction = 6, description = "There is a locked door to the south." },
            new ExtDesc { location = 2, direction = 6, description = "Stairs lead down to a cellar. Several steps have collapsed, making the staircase unusable." },
            new ExtDesc { location = 29, direction = 5, description = "Stairs lead up. Several steps have collapsed, making the staircase unusable." },
            new ExtDesc { location = 12, direction = 5, description = "Dark stairs lead up to the attic." },
            new ExtDesc { location = 17, direction = 4, description = "A locked door to the WEST is labelled 'EXTREME DANGER'." },
            new ExtDesc { location = 17, direction = 1, description = "Your uncle's doberman blocks a doorway to the north." }
        ];

        // # DIM REXIT[31][6] : : LOCATION EXITS
        public int[,] LocationExit { get; set; } = {
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
            /* 12 */    {-99,  13  ,   0  ,   0  ,  14  ,  -1  ,   3  }, // W: STAIRS TO ATTIC25 is hidden until fuse is inserted
            /* 13 */    {-99,   0  ,  12  ,   0  ,   0  ,   0  ,   0  },
            /* 14 */    {-99,   0  ,  15  ,  12  ,  17  ,   0  ,   0  },
            /* 15 */    {-99,  14  ,   0  ,   0  ,  16  ,   0  ,   0  },
            /* 16 */    {-99,   0  ,   0  ,  15  ,   0  ,   0  ,   0  },
            /* 17 */    {-99,  -1  ,   0  ,  14  ,  -1  ,   0  ,   0  }, // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            /* 18 */    {-99,   0  ,  17  ,  -1  ,   0  ,   0  ,   0  }, // E: secret room hidden until SPINNINGTOP
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
        // objects can be moved around

        public int[] ILOC = [
            /* 00 */    -99,     //dummy
            /* 01 */     1,      // "NEWSPAPER",
            /* 02 */    -1,      // "TEDDYBEAR", -1
            /* 03 */    -1,      // "FUSE",
            /* 04 */    10,      // "JACK", 10
            /* 05 */    30,      // "PICTURE",
            /* 06 */    10,      // "BUNGEE", 10
            /* 07 */    13,      // "KEY", 13
            /* 08 */    15,      // "SPINNINGTOP", 15
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
        ];

        #endregion Variables
    }
}