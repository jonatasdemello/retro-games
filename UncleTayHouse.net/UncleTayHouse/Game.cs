using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public partial class Game
    {
        public GameUserInput userInput = new();
        public GameState gameState = new();

        public void Play()
        {
            Screen.ClearScreen();

            ActionShowIntro();

            while (true)
            {
                ShowLocation();

                // read and process userInput input
                string userInput = UserInput.ReadInput();
                if (userInput == "EXIT31" || userInput == "QUIT" || userInput == "END")
                {
                    break;
                }
                this.userInput = UserInput.ProcessInput(userInput);

                ActionProcessInput();
            }
            Screen.Print("Thanks for playing!");
        }

        public void ShowLocation()
        {
            // additional game logic for specific locations

            // first time after jump
            if (gameState.PlayerAt == GameRooms.MIDAIR30 // 30 Mid-air
                && !gameState.PlayerJump)
            {
                gameState.PlayerJump = true;
            }
            else if (gameState.PlayerAt == GameRooms.MIDAIR30 // 30 Mid-air
                && gameState.PlayerJump) // second time
            {
                Screen.PrintResponse("... and bunge cord spring back");
                gameState.PlayerAt = GameRooms.BALCONY12; // 12 back to the balcony
            }

            if (gameState.PlayerAt == GameRooms.EXIT31) // 31 exit
            {
                ActionScore();
                ActionExit();
            }

            // show where the player is
            ActionLocation();

            // show where the player can go
            ActionDirections();

            ActionExtendedDescriptions();
        }
        // ok
        public void ActionProcessInput()
        {
            if (userInput.InputWordTotal < 1)
            {
                Screen.PrintResponse("You need 1 word to move, 2+ words (verb + noun) for actions.");
            }
            else if (userInput.InputWordTotal == 1)
            {
                ActionOneWord();
            }
            else if (userInput.InputWordTotal == 2)
            {
                ActionTwoWords();
            }
            else if (userInput.InputWordTotal == 3)
            {
                ActionThreeWords();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }
        // ok
        public void ActionOneWord()
        {
            // 1-12 move N S E W U D
            if (userInput.CMD1 == GameVerbs.NORTH
                || userInput.CMD1 == GameVerbs.SOUTH
                || userInput.CMD1 == GameVerbs.EAST
                || userInput.CMD1 == GameVerbs.WEST
                || userInput.CMD1 == GameVerbs.UP
                || userInput.CMD1 == GameVerbs.DOWN
                || userInput.CMD1 == GameVerbs.N
                || userInput.CMD1 == GameVerbs.S
                || userInput.CMD1 == GameVerbs.E
                || userInput.CMD1 == GameVerbs.W
                || userInput.CMD1 == GameVerbs.U
                || userInput.CMD1 == GameVerbs.D)
            {
                ActionPlayerMove(userInput.CMD1);
            }
            else if (userInput.CMD1 == GameVerbs.INVENTORY || userInput.CMD1 == GameVerbs.I) // 1, 14 inventory
            {
                ActionInventory();
            }
            else if (userInput.CMD1 == GameVerbs.SCORE) // 15 score
            {
                ActionScore();
            }
            else if (userInput.CMD1 == GameVerbs.JUMP) // 16 jump
            {
                ActionJump();
            }
            else if (userInput.CMD1 == GameVerbs.HELP) // 17 help
            {
                ActionShowIntro();
            }
            else if (userInput.CMD1 == GameVerbs.TAKE) // 18 take
            {
                Screen.PrintResponse("Take what?"); // need 2 words
            }
            else if (userInput.CMD1 == GameVerbs.DROP) // 19 drop
            {
                Screen.PrintResponse("Drop what?"); // need 2 words
            }
            else if (userInput.CMD1 == GameVerbs.LOOK || userInput.CMD1 == GameVerbs.L) // 20, 62 look
            {
                ShowLocation();
            }
            else if (userInput.CMD1 > Constants.OBJECTOFFSET) // objects, not verbs
            {
                Screen.PrintResponse("Do what with " + Constants.VOCABS[userInput.CMD1] + "?");
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }
        // ok
        public void ActionTwoWords()
        {
            if (userInput.CMD1 == GameVerbs.TAKE) // 18 take
            {
                ActionTake();
            }
            else if (userInput.CMD1 == GameVerbs.DROP) // 19 drop
            {
                ActionDrop();
            }
            else if (userInput.CMD1 == GameVerbs.LOOK
                || userInput.CMD1 == GameVerbs.READ
                || userInput.CMD1 == GameVerbs.EXAMINE
                || userInput.CMD1 == GameVerbs.X) // 20 look, 21 read, 22 examine 64 X
            {
                ActionLook();
            }
            else if (userInput.CMD1 == GameVerbs.UNLOCK) // 23 unlock
            {
                ActionUnlock();
            }
            else if (userInput.CMD1 == GameVerbs.EAT) // 24 eat
            {
                ActionEat();
            }
            else if (userInput.CMD1 == GameVerbs.SPIN) // 25 spin
            {
                ActionSpin();
            }
            else if (userInput.CMD1 == GameVerbs.MOVE) // 26 move
            {
                ActionMoveObj();
            }
            else if (userInput.CMD1 == GameVerbs.OPEN) // 27 open (X door)
            {
                ActionOpen();
            }
            else if (userInput.CMD1 == GameVerbs.TIE) // 28 tie
            {
                ActionTieBungeeToRailing();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }
        // ok
        public void ActionThreeWords()
        {
            if (userInput.CMD2 == GameObjects.DUMMY || userInput.CMD3 == GameObjects.DUMMY) // 0
            {
                Screen.PrintResponse("You need 3 words");
            }
            // read note in mirror
            else if ((userInput.CMD1 == GameVerbs.LOOK // 20 look
                || userInput.CMD1 == GameVerbs.READ // 21 read
                || userInput.CMD1 == GameVerbs.EXAMINE) // 22 examine
                && userInput.CMD2 == GameObjects.NOTE // 42 note
                && userInput.CMD3 == GameObjects.MIRROR) // 61 mirror
            {
                ActionReadNoteInMirror();
            }
            // move couch with brace
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == GameObjects.COUCH // 55 couch
                && userInput.CMD3 == GameObjects.BRACE) // 46 brace
            {
                ActionMoveCouchWithBrace();
            }
            // move fridge with jack
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == GameObjects.FRIDGE // 54 fridge
                && userInput.CMD3 == GameObjects.JACK) // 37 jack
            {
                ActionMoveFridgeWithJack();
            }
            // move clothes with gloves
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == GameObjects.CLOTHES // 56 clothes
                && userInput.CMD3 == GameObjects.GLOVES) // 44 gloves
            {
                ActionMoveClothesWithGloves();
            }
            // open [direction not mentioned in note] door
            else if (userInput.CMD1 == GameVerbs.OPEN // 27 open
                && (userInput.CMD2 == GameVerbs.LEFT // 31 left
                || userInput.CMD2 == GameVerbs.CENTER // 32 center
                || userInput.CMD2 == GameVerbs.RIGHT // 33 right
                ) && userInput.CMD3 == GameObjects.DOOR) // 57 door
            {
                ActionOpen3Door();
            }
            // tie bungee to railing
            else if (userInput.CMD1 == GameVerbs.TIE // 28 tie
                && userInput.CMD2 == GameObjects.BUNGEE // 39 bungee
                && userInput.CMD3 == GameObjects.RAILING) // 58 railing
            {
                ActionTieBungeeToRailing();
            }
            // unlock|oil dumbwaiter with oilcan
            else if ((userInput.CMD1 == GameVerbs.OIL  // 29 oil
                || userInput.CMD1 == GameVerbs.UNLOCK) // 23 unlock
                && userInput.CMD2 == GameObjects.DUMBWAITER // 59 dumbwaiter
                && userInput.CMD3 == GameObjects.OILCAN) // 48 oilcan
            {
                ActionOilDumbwaiterWithOilcan();
            }
            // put fuse in fusebox
            else if (userInput.CMD1 == GameVerbs.PUT // 30 put
                && userInput.CMD2 == GameObjects.FUSE // 36 fuse
                && userInput.CMD3 == GameObjects.FUSEBOX) // 60 fusebox
            {
                ActionPutFuseInFusebox();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }

        // todo improve this
        public void ActionInventory()
        {
            Screen.Print(" [Inventory] You are carrying:");
            bool empty = true;
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == 0)
                {
                    Screen.Print("   - ", Constants.VOCABS[i + Constants.OBJECTOFFSET]);
                    empty = false;
                }
            }
            if (empty)
            {
                Screen.Print(" - nothing!");
            }
        }
        // ok
        public void ActionShowIntro()
        {
            Screen.Print(" ");
            Screen.Print("***********************************");
            Screen.Print("*** UNCLE TAY'S HOUSE ADVENTURE ***");
            Screen.Print("***********************************");
            Screen.Print(" ");
            Screen.Print("Find treasures and valuables in your mad uncle tays' house");
            Screen.Print("Type simple commands to move around:");
            Screen.Print("   NORTH, SOUTH, EAST, WEST, UP, DOWN");
            Screen.Print("   or just: S, E, W, U, D.");
            Screen.Print("Type two word commands (verb + action) to interact with objects:");
            Screen.Print("TAKE BOOK, DROP BOOK, INVENTORY, LOOK, READ, MOVE");
            Screen.Print("Some commands are complex:");
            Screen.Print(" 'MOVE THE HUBCAP WITH THE SPANNER'");
            Screen.Print(" ");
            Screen.Print("Possible commands: ");
            Screen.Print("    NORTH, SOUTH, EAST, WEST, UP, DOWN, N, S, E, W, U, D,");
            Screen.Print("    I, INVENTORY, SCORE, JUMP, HELP, EXIT31");
            Screen.Print("    TAKE, DROP, LOOK, READ, EXAMINE, UNLOCK, EAT, SPIN,");
            Screen.Print("    MOVE, OPEN, TIE, OIL, PUT, LEFT, CENTER, RIGHT");
            Screen.Print(" ");
            if (gameState.PlayerAt != GameRooms.FOYER1)
            {
                Screen.Print("Are you ready?");
            }
        }

        // todo improve this
        public void ActionScore()
        {
            int SCORE = 50;
            // reduce points for generic items not explored (1-15)
            for (int i = 1; i < 15; i++)
            {
                if (ILOC[i] == -1)
                {
                    SCORE -= 5;
                }
            }
            // add points for valuable items (16-20)
            for (int i = 16; i < 20; i++)
            {
                if (ILOC[i] == 0) // Carrying?
                {
                    SCORE += 10;
                }
            }
            // reduce points for non explored (hidden) location
            for (int i = 3; i < 30; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    if (LocationExit[i, j] == -1)
                    {
                        SCORE -= 5;
                    }
                }
            }

            // show result
            Screen.PrintResponse("Your score is " + SCORE + " out of a possible 100");

            if (SCORE == 100)
            {
                Screen.PrintResponse("You have won the game!");
            }
        }
        // ok
        public void ActionExit()
        {
            Screen.PrintResponse("Thank you for playing, bye!");
            Environment.Exit(0);
        }
        // ok
        public void ActionLocation()
        {
            Screen.PrintDgb("you are at");
            Screen.Print("    { " + gameState.PlayerAt.ToString() + " } " + Constants.gameMapLocation[gameState.PlayerAt].rname);
            Screen.Print("    " + Constants.gameMapLocation[gameState.PlayerAt].rdesc);
        }
        // ok
        public void ActionDirections()
        {
            Screen.PrintDgb("you can go");
            for (int i = 1; i <= 6; i++) // 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
            {
                int exit = LocationExit[gameState.PlayerAt, i];
                if (exit > 0)
                {
                    Screen.Print("    " + Constants.VOCABS[i] + "\t : ", Constants.gameMapLocation[exit].rname);
                }
            }
        }
        // ok
        public void ActionExtendedDescriptions()
        {
            Screen.PrintDgb("extras");
            // some places have an extended description
            for (int i = 0; i < extDesc.Length; i++)
            {
                int loc = extDesc[i].location;
                int dir = extDesc[i].direction;
                if (gameState.PlayerAt == loc && LocationExit[loc, dir] <= 0)
                {
                    Screen.PrintResponse("    " + extDesc[i].description);
                }
            }

            // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            if (gameState.PlayerAt == GameRooms.HALL17 // 17 hallway
                && LocationExit[17, 1] > 0)
            {
                Screen.PrintResponse("    Your uncle's doberman is snoring peacefully");
            }

            // in sitting room, if bungee cord is tied
            if (gameState.PlayerAt == GameRooms.SITTINGROOM3 // 3 sitting room
                && ILOC[6] == Constants.TIED) // ILOC[6] = bungee cord & -12 = tied
            {
                Screen.PrintResponse("    A bungee cord dangles from the railing above");
            }

            // in balcony, if bungee cord is tied
            if (gameState.PlayerAt == GameRooms.BALCONY12 // 12 balcony
                && ILOC[6] == Constants.TIED) // ILOC[6] = bungee cord & -12 = tied
            {
                Screen.PrintResponse("    A bungee cord dangles from the railing");
            }

            // show objects in the current location
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == gameState.PlayerAt)
                {
                    Screen.PrintResponse("    There is a " + Constants.VOCABS[i + Constants.OBJECTOFFSET] + " here");
                }
            }

            if (gameState.PlayerAt == GameRooms.KITCHEN2 // 2 kitchen
                && ILOC[3] == Constants.HIDDEN) // ILOC[3] = fuse & -1 = hidden
            {
                Screen.PrintResponse("    Something is barely visible under the fridge");
            }

            if (gameState.PlayerAt == GameRooms.SITTINGROOM3 // 3 Sitting room
                && ILOC[5] == GameRooms.MIDAIR30) // ILOC[5] = picture & 30 = MIDAIR30
            {
                Screen.PrintResponse("    There is a picture high up on the wall");
            }
        }
        // ok
        public void ActionTake()
        {
            // 1-33 (verbs) 34-61 (objects)
            int obj = userInput.CMD2 - Constants.OBJECTOFFSET;

            // cant take verbs
            if (userInput.CMD2 <= Constants.OBJECTOFFSET)
            {
                Screen.PrintResponse("Take what?");
                return;
            }

            // check if is not already carrying it
            if (ILOC[obj] == Constants.CARRYING)
            {
                Screen.PrintResponse("You already have " + Constants.VOCABS[userInput.CMD2]);
                return;
            }

            // big objects that can't be taken
            if (userInput.CMD2 >= 54) // fridge, couch, door, railing...
            {
                Screen.PrintResponse("It's too heavy, you can't take that");
                return;
            }

            // take picture from the wall (3=sitting room)
            if (userInput.CMD2 == GameObjects.PICTURE // 38 picture (38-33 = 5)
                && ILOC[obj] == GameRooms.MIDAIR30 // 30 mid-air (means picture is in mid-air)
                && gameState.PlayerAt == GameRooms.SITTINGROOM3) // 3 sitting room
            {
                Screen.PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // take picture from mid-air
            if (userInput.CMD2 == GameObjects.PICTURE // 38 picture
                && gameState.PlayerAt == GameRooms.MIDAIR30) // 30 mid-air
            {
                Screen.PrintResponse("Taking the picture reveals a fusebox");

                ILOC[5] = Constants.CARRYING; // picture is being carried
                ILOC[obj] = Constants.CARRYING; // picture is being carried
                ILOC[27] = GameRooms.MIDAIR30; // Fusebox is revealed in position 30 mid-air
                return;
            }

            // take boxspring from bottom of stairs
            if (userInput.CMD2 == GameObjects.BOXSPRING // 45 boxspring
                && gameState.PlayerAt == GameRooms.BOTTOMOFSTAIRS29) // 29 bottom of stairs
            {
                Screen.PrintResponse("It is better to leave it there");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != gameState.PlayerAt)
            {
                Screen.PrintResponse("There is no " + Constants.VOCABS[userInput.CMD2] + " here");
                return;
            }

            // player is carrying object
            ILOC[obj] = Constants.CARRYING;
            Screen.PrintResponse(Constants.VOCABS[userInput.CMD2] + ": taken");
        }
        // ok
        public void ActionDrop()
        {
            int obj = userInput.CMD2 - Constants.OBJECTOFFSET;

            // check if is carrying it first
            if (ILOC[obj] != Constants.CARRYING)
            {
                Screen.PrintResponse("You aren't carrying " + Constants.VOCABS[userInput.CMD2]);
                return;
            }

            // drop gainesburger hallway
            if (userInput.CMD2 == GameObjects.GAINESBURGER // 43 gainesburger
                && gameState.PlayerAt == GameRooms.HALL17 // 17 hallway
                && LocationExit[17, 1] <= 0) // north exit is not open yet
            {
                Screen.PrintResponse("The dog looks disgusted. maybe you should eat it");
                return;
            }

            // drop teddybear hallway
            if (userInput.CMD2 == GameObjects.TEDDYBEAR // 35 teddybear
                && gameState.PlayerAt == GameRooms.HALL17 // 17 hallway
                && LocationExit[17, 1] <= 0) // north exit is not open yet
            {
                Screen.PrintResponse("The dog chews his favorite toy and is soon asleep");
                ILOC[obj] = Constants.HIDDEN; // teddybear is now hidden
                LocationExit[17, 1] = 18; // reveal secret room north
                return;
            }

            // drop boxpring bottom of stairs
            if (userInput.CMD2 == GameObjects.BOXSPRING // 45 boxspring
                && gameState.PlayerAt == GameRooms.BOTTOMOFSTAIRS29 // 29 bottom of stairs
                && LocationExit[29, 5] <= 0)
            {
                Screen.PrintResponse("The boxspring covers the gap in the stairs");
                ILOC[obj] = Constants.HIDDEN; // boxspring is now hidden
                LocationExit[29, 5] = 2; // U: unlock with drop boxspring
                LocationExit[2, 6] = 29; // D: STAIRS TO BASEMENT is hidden until drop Boxspring
                return;
            }

            // if player is carrying object, ILOC[obj] == current gameState.PlayerAt
            ILOC[obj] = gameState.PlayerAt;
            Screen.PrintResponse(Constants.VOCABS[userInput.CMD2] + ": dropped");
        }
        // ok
        public void ActionLook()
        {
            int obj = userInput.CMD2 - Constants.OBJECTOFFSET;

            // look at the picture on the wall
            if (userInput.CMD2 == GameObjects.PICTURE // 38 picture
                && gameState.PlayerAt == GameRooms.SITTINGROOM3 // 3 sitting room
                && ILOC[5] == GameRooms.MIDAIR30) // 30 mid-air (means picture is in mid-air)
            {
                Screen.PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != 0
                && ILOC[obj] != gameState.PlayerAt)
            {
                Screen.PrintResponse("There is no " + Constants.VOCABS[userInput.CMD2] + " here");
                return;
            }

            // look 42=note and (13=master bedroom OR 22=bathroom)
            if (userInput.CMD2 == GameObjects.NOTE // 42 note
                && (gameState.PlayerAt == GameRooms.MASTERBEDROOM13 // 13 master bedroom
                || gameState.PlayerAt == GameRooms.BATHROOM22)) // 22 bathroom
            {
                ActionSafeDoor();
                return;
            }

            // Print extended obj description
            if (!string.IsNullOrEmpty(Constants.gameItemLocation[obj].desc))
            {
                Screen.PrintResponse(Constants.gameItemLocation[obj].desc);
                return;
            }

            Screen.PrintResponse("There's nothing special about the " + Constants.VOCABS[userInput.CMD2]);
        }
        // ok
        public void ActionSafeDoor()
        {
            // decide which door is safe
            if (gameState.SafeDoor == 0)
            {
                gameState.SafeDoor = Utils.RNG(3);
            }

            string N1S = "LEFT";
            string N2S = "RIGHT";

            if (gameState.SafeDoor == 1)
            {
                N1S = "CENTER";
            }
            if (gameState.SafeDoor == 3)
            {
                N2S = "CENTER";
            }
            Screen.PrintResponse("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }
        // ok
        public void ActionUnlock()
        {
            if (gameState.PlayerAt != GameRooms.HALL5
                && gameState.PlayerAt != GameRooms.LIBRARY8
                && gameState.PlayerAt != GameRooms.HALL20
                && gameState.PlayerAt != GameRooms.HALL17)
            {
                Screen.PrintResponse("There is no door here!");
                return;
            }
            // do we have a key?
            if (ILOC[7] != Constants.CARRYING) // 13 = master bedroom ? key ?
            {
                Screen.PrintResponse("You don't have a key!");
                return;
            }
            else // yes
            {
                // ckeck where we are
                if (gameState.PlayerAt == GameRooms.HALL5) // 5 Hallway?
                {
                    Screen.PrintResponse("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (gameState.PlayerAt == GameRooms.HALL17 // 17 hall
                    && ILOC[7] == 0) // Hallway?
                {
                    Screen.PrintResponse("You unlock the door. beware!");
                    LocationExit[17, 4] = 20; // reveal north
                    return;
                }
            }
            Screen.PrintResponse("Nothing to unlock!");
        }
        // ok
        public void ActionOpen()
        {
            // open door == unlock door
            if (userInput.CMD2 == GameObjects.DOOR)
            {
                ActionUnlock();
                return;
            }
            // open door in dangerous hall 20
            if (userInput.CMD2 == GameObjects.DOOR// 57 door
                && gameState.PlayerAt == GameRooms.HALL20) // 20 hall
            {
                Screen.PrintResponse("Please specify LEFT, CENTER or RIGHT");
                return;
            }
            Screen.PrintResponse("Open what?");
        }
        // ok
        public void ActionOpen3Door()
        {
            if (gameState.PlayerAt != GameRooms.HALL20) // 20 dangerous hall
            {
                Screen.PrintResponse("You can't do that here");
                return;
            }

            if (userInput.CMD3 != GameObjects.DOOR) // 57 door
            {
                Screen.PrintResponse("Open what?");
                return;
            }

            // open [direction not mentioned in note] door
            // 31 = left
            // 32 = center
            // 33 = right
            int DOORDIR = userInput.CMD2 - 30;
            if (DOORDIR < 1 || DOORDIR > 3)
            {
                Screen.PrintResponse("Which door?");
                return;
            }

            if (DOORDIR == gameState.SafeDoor)
            {
                Screen.PrintResponse("Opening the door reveals a dumbwaiter");
                LocationExit[gameState.PlayerAt, 4] = 23;
                return;
            }

            // Trap: if you haven't read the note before
            int rnd = Utils.RNG(100);
            if (rnd > 50)
            {
                Screen.PrintResponse("BAM! A shot rings out! it was well-aimed too.");
                // you die
                return;
            }

            Screen.PrintResponse("An ironing board slams onto your head");
            // you die
        }
        // ok
        public void ActionEat()
        {
            if (ILOC[10] != Constants.CARRYING) //  GAINESBURGER
            {
                Screen.PrintResponse("You don't have it!");
                return;
            }
            else if (userInput.CMD2 != 42)
            {
                Screen.PrintResponse("You can't eat that!");
                return;
            }

            Screen.PrintResponse("There was a diamond hidden inside the gainesburger");

            ILOC[10] = Constants.HIDDEN; // GAINESBURGER is hidden now -2
            ILOC[17] = gameState.PlayerAt; //  DIAMOND is revealed in player location
        }
        // ok
        public void ActionSpin()
        {
            if (ILOC[8] != Constants.CARRYING) // SPINNINGTOP
            {
                Screen.PrintResponse("Spin what?");
                return;
            }
            if (gameState.PlayerAt == GameRooms.CHILDSROOM18) // 18 child's room
            {
                Screen.PrintResponse("There is a flash of light and a cracking sound! An opening appears in the east wall");
                LocationExit[18, 3] = GameRooms.SECRETROOM19; // 19 reveal east (dark room)
                return;
            }

            Screen.PrintResponse("Whee!");
        }
        // ok
        public void ActionMoveObj()
        {
            if (userInput.CMD2 == GameObjects.FRIDGE) // 54 fridge
            {
                Screen.PrintResponse("It's too heavy for you to move alone (without any help)");
                return;
            }
            if (userInput.CMD2 == GameObjects.COUCH) // 55 couch
            {
                Screen.PrintResponse("Your back is acting up, you will need some support");
                return;
            }
            if (userInput.CMD2 == GameObjects.CLOTHES) // 56 clothes
            {
                Screen.PrintResponse("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            Screen.PrintResponse("You can't do that");
        }
        // ok
        public void ActionJump()
        {
            // check if we are at the balcony
            if (gameState.PlayerAt != GameRooms.BALCONY12) // 12 Balcony
            {
                Screen.PrintResponse("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                return;
            }

            // check if BUNGEE cord is tied to the railing
            if (ILOC[6] != Constants.TIED) // -12 tied
            {
                Screen.PrintResponse("You forgot your parachute. Or maybe something else...");
                return;
            }

            Screen.PrintResponse("You bungee off the balcony...");

            // set location to MID-AIR (so can take the picture)
            gameState.PlayerAt = GameRooms.MIDAIR30; // 30 mid-air
            gameState.PlayerJump = false; // reset for multiple jumps
        }
        // ok
        public void ActionPlayerMove(int dir)
        {
            // convert N,S,E,W,U,D to long form
            if (dir > 6)
            {
                dir = dir - 6;
            }
            if (LocationExit[gameState.PlayerAt, dir] > 0)
            {
                gameState.PlayerAt = LocationExit[gameState.PlayerAt, dir];
            }
            if (gameState.PlayerAt == GameRooms.BALCONY12 // 12 attic
                && dir == GameVerbs.UP) // 5 up to attic
            {
                Screen.PrintResponse("You're afraid of the dark");
                return;
            }
            if (gameState.PlayerAt == GameRooms.HALL17 // 17 hall
                && dir == GameVerbs.NORTH) // 1 north
            {
                Screen.PrintResponse("You never did like that dog");
                return;
            }
            if (gameState.PlayerAt == GameRooms.DUMBWAITER23 // 23 dumbwaiter
                && LocationExit[23, 6] <= 0) // D: is blocked, unlock with oilcan
            {
                Screen.PrintResponse("The dumbwaiter mechanism is corroded and won't move");
                return;
            }

            Screen.PrintResponse("You can't go that way");
        }
        // ok
        public void ActionMoveFridgeWithJack()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != GameObjects.FRIDGE // 54 fridge
                || userInput.CMD3 != GameObjects.JACK) // 37 jack
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("You jack up the fridge and find a fuse under it");
            ILOC[3] = gameState.PlayerAt; // reveal fuse
        }
        // ok
        public void ActionMoveCouchWithBrace()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != GameObjects.COUCH // 55 couch
                || userInput.CMD3 != GameObjects.BRACE) // 46 brace
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("You move the couch and find a teddybear behind it");
            ILOC[2] = gameState.PlayerAt; // reveal teddybear
        }
        // ok
        public void ActionMoveClothesWithGloves()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != GameObjects.CLOTHES // 56 clothes
                || userInput.CMD3 != GameObjects.GLOVES) // 44 gloves
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("Moving the clothes reveals a laundry chute to the basement");
            LocationExit[gameState.PlayerAt, 6] = GameRooms.LAUNDRY27; // 27 laundry chute
        }
        // ok
        public void ActionTieBungeeToRailing()
        {
            // is carrying a bungee cord?
            if (ILOC[6] != Constants.CARRYING)
            {
                Screen.PrintResponse("You don't have a bungee cord!");
                return;
            }
            // is in the Balcony
            if (gameState.PlayerAt != GameRooms.BALCONY12) // 12 balcony
            {
                Screen.PrintResponse("There is nothing here to tie to");
                return;
            }
            // object is not BUNGEE cord
            if (userInput.CMD2 != GameObjects.BUNGEE) // 39 bungee
            {
                Screen.PrintResponse("You can't tie that");
                return;
            }
            // rainling
            if (userInput.CMD3 != GameObjects.RAILING) // 58 railing
            {
                Screen.PrintResponse("Tie to what?");
                return;
            }

            Screen.PrintResponse("Bungee cord tied to Railing!");

            // BUNGEE cord is tied to the railing
            ILOC[6] = Constants.TIED; // ILOC[6] = bungee & -12 = tied
        }
        // ok
        public void ActionOilDumbwaiterWithOilcan()
        {
            // is in the dumbwaiter?
            if (gameState.PlayerAt != GameRooms.DUMBWAITER23) // 23 dumbwaiter
            {
                Screen.PrintResponse("You can't do that here");
                return;
            }
            // 15 OILCAN
            if (ILOC[15] != Constants.CARRYING)
            {
                Screen.PrintResponse("You don't have any oil");
                return;
            }
            // dumbwaiter
            if (userInput.CMD2 != GameObjects.DUMBWAITER) // 59 dumbwaiter
            {
                Screen.PrintResponse("Oil what?");
                return;
            }

            Screen.PrintResponse("The dumbwaiter mechanism now runs smoothly");
            LocationExit[23, 6] = GameRooms.DUMBWAITER24; // reveal down to 24
        }
        // ok
        public void ActionPutFuseInFusebox()
        {
            if (userInput.CMD1 != GameVerbs.PUT // 30 put
                || userInput.CMD2 != GameObjects.FUSE // 36 fuse
                || userInput.CMD3 != GameObjects.FUSEBOX // 60 fusebox
                || gameState.PlayerAt != GameRooms.MIDAIR30)// 30 mid-air
            {
                Screen.PrintResponse("You can't do that here");
                return;
            }
            if (userInput.CMD3 != GameObjects.FUSEBOX) // 60 fusebox
            {
                Screen.PrintResponse("You can't put it there");
                return;
            }
            // do we have the fuse?
            if (ILOC[3] != Constants.CARRYING)
            {
                Screen.PrintResponse("You don't have it!");
            }

            Screen.PrintResponse("You put the fuse in the box. Power is restored in the Attic!");
            // mark fuse as used
            ILOC[3] = Constants.HIDDEN;

            // stairs to attic is hidden until fuse is inserted
            LocationExit[12, 5] = GameRooms.ATTIC25; // 25 attic
        }
        // ok
        public void ActionReadNoteInMirror()
        {
            // 42=note and (13=master bedroom OR 22=bathroom)
            if (userInput.CMD2 == GameObjects.NOTE // 42 note
                && (gameState.PlayerAt == GameRooms.MASTERBEDROOM13 // 13 master bedroom
                || gameState.PlayerAt == GameRooms.BATHROOM22)) // 22 bathroom
            {
                ActionSafeDoor();
                return;
            }
            Screen.PrintResponse("I don't see a mirror here");
        }

    }
}