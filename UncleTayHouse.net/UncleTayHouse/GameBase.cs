namespace UncleTayHouse
{
    public partial class Game
    {
        #region Variables

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

        #endregion Variables
    }
}