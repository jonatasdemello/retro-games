using System.Diagnostics;

namespace Wumpus
{
    public static class TextPresentation
    {
        public static readonly string APP_NAME = Process.GetCurrentProcess().ProcessName;

        public const int INPUT_FILE_ERROR = 1;
        public const int GAME_ERROR = 2;

        public const string BEADS_FILENAME = "data\\StringOfBeads.xml";
        public const string DENDRITE_FILENAME = "data\\DendriteWithDegeneracies.xml";
        public const string DODECAHEDRON_FILENAME = "data\\Dodecahedron.xml";
        public const string LATTICE_FILENAME = "data\\OneWayLattice.xml";
        public const string MOBIUS_FILENAME = "data\\MobiusStrip.xml";
        public const string TORUS_FILENAME = "data\\HexNetOnTorus.xml";

        public const string GOT_BY_WUMPUS_STRING = "TSK TSK TSK- WUMPUS GOT YOU!\n";

        public const string INSTRUCTIONS =
            "WELCOME TO WUMPUS II\n" +
            "THIS VERSION HAS THE SAME RULES AS 'HUNT THE WUMPUS'.\n" +
            "HOWEVER, YOU NOW HAVE A CHOICE OF CAVES TO PLAY IN.\n" +
            "SOME CAVES ARE EASIER THAN OTHERS. ALL [default] CAVES HAVE 20\n" +
            "ROOMS AND 3 TUNNELS LEADING FROM ONE ROOM TO OTHER ROOMS.\n" +
            "THE CAVES ARE:\n" +
            "  0  -  DODECAHEDRON   THE ROOMS OF THIS CAVE ARE ON A\n" +
            "        12-SIDED OBJECT, EACH FORMING A PENTAGON.\n" +
            "        THE ROOMS ARE AT THE CORNERS OF THE PENTAGONS.\n" +
            "        EACH ROOM HAVING TUNNELS THAT LEAD TO 3 OTHER ROOMS.\n" +
            "\n" +
            "  1  -  MOBIUS STRIP   THIS CAVE IS TWO ROOMS\n" +
            "        WIDE AND 10 ROOMS AROUND (LIKE A BELT)\n" +
            "        YOU WILL NOTICE THERE IS A HALF TWIST\n" +
            "        SOMEWHERE.\n" +
            "\n" +
            "  2  -  STRING OF BEADS    FIVE BEADS IN A CIRCLE.\n" +
            "        EACH BEAD IS A DIAMOND WITH A VERTICAL\n" +
            "        CROSS-BAR. THE RIGHT & LEFT CORNERS LEAD\n" +
            "        TO NEIGHBORING BEADS. (THIS ONE IS DIFFICULT\n" +
            "        TO PLAY.\n" +
            "\n" +
            "  3  -  HEX NETWORK        IMAGINE A HEX TILE FLORE.\n" +
            "        TAKE A RECTANGLE WITH 20 POINTS (INTERSECTIONS)\n" +
            "        INSIDE (4X4). JOIN RIGHT & LEFT SIDES TO MAKE A\n" +
            "        CYLINDER. THEN JOIN TOP & BOTTOM TO FORM A \n" +
            "        TORUS (DOUGHNUT).\n" +
            "        HAVE FUN IMAGINING THIS ONE!!\n" +
            "\n" +
            " CAVES 1-3 ARE REGULAR IN A SENSE THAT EACH ROOM\n" +
            "GOES TO THREE OTHER ROOMS & TUNNELS ALLOW TWO-\n" +
            "WAY TRAFFIC. HERE ARE SOME 'IRREGULAR' CAVES:\n" +
            "\n" +
            "  4  -  DENDRITE WITH DEGENERACIES   PULL A PLANT FROM\n" +
            "        THE GROUND. THE ROOTS & BRANCHES FORM A \n" +
            "        DENDRITE. IE., THERE ARE NO LOOPING PATHS\n" +
            "        DEGENERACIES MEANS A) SOME ROOMS CONNECT TO\n" +
            "        THEMSELVES AND B) SOME ROOMS HAVE MORE THAN ONE\n" +
            "        TUNNEL TO THE SAME ROOM IE, 12 HAS \n" +
            "        TWO TUNNELS TO 13.\n" +
            "\n" +
            "  5  -  ONE WAY LATTICE     HERE ALL TUNNELS GO ONE\n" +
            "        WAY ONLY. TO RETURN, YOU MUST GO AROUND THE CAVE\n" +
            "        (AROUND 5 MOVES).\n" +
            "\n" +
            "  6  -  ENTER YOUR OWN CAVE    The computer will ask you \n" +
            "        for a filename.  It should point to an XML file with \n" +
            "        a schema, identical to that, used by the included \n" +
            "        Wumpus XML files.  The nodes can have non-numeric \n" +
            "        names and zero or more linked nodes.  The only \n" +
            "        requirement is that you include sufficient nodes for \n" +
            "        the player, wumpus, pits, and bats.\n" +
            "  HAPPY HUNTING!\n";
    }
}