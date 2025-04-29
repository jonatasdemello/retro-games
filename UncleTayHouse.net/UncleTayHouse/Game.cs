namespace UncleTayHouse
{
    public partial class Game
    {
        public GameUserInput user = new();
        public GameState gameState = new();

        public void Play()
        {
            Screen.ClearScreen();

            ActionShowIntro();

            while (true)
            {
                ShowLocation();

                // read and process user input
                string userInput = UserInput.ReadInput();
                if (userInput == "EXIT" || userInput == "QUIT" || userInput == "END")
                {
                    break;
                }
                user = UserInput.ProcessInput(userInput);

                ActionProcessInput();
            }
            Screen.Print("Thanks for playing!");
        }

        public void ShowLocation()
        {
            // additional game logic for specific locations

            // first time after jump
            if (gameState.LOCAL == GameRooms.MIDAIR // 30 Mid-air
                && gameState.TURN1 == 0)
            {
                gameState.TURN1 = 1;
            }
            else if (gameState.LOCAL == GameRooms.MIDAIR // 30 Mid-air
                && gameState.TURN1 == 1) // second time
            {
                Screen.PrintResponse("... and bunge cord spring back");
                gameState.LOCAL = GameRooms.BALCONY; // 12 back to the balcony
            }

            if (gameState.LOCAL == GameRooms.EXIT) // 31 exit
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

        public void ActionProcessInput()
        {
            if (user.InputWordTotal < 1)
            {
                Screen.PrintResponse("You need 1 word to move, 2+ words (verb + noun) for actions.");
            }
            else if (user.InputWordTotal == 1)
            {
                ActionOneWord();
            }
            else if (user.InputWordTotal == 2)
            {
                ActionTwoWords();
            }
            else if (user.InputWordTotal == 3)
            {
                ActionThreeWords();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }

        public void ActionOneWord()
        {
            // 1-12 move N S E W U D
            if (user.CMD1 == GameVerbs.NORTH
                || user.CMD1 == GameVerbs.SOUTH
                || user.CMD1 == GameVerbs.EAST
                || user.CMD1 == GameVerbs.WEST
                || user.CMD1 == GameVerbs.UP
                || user.CMD1 == GameVerbs.DOWN
                || user.CMD1 == GameVerbs.N
                || user.CMD1 == GameVerbs.S
                || user.CMD1 == GameVerbs.E
                || user.CMD1 == GameVerbs.W
                || user.CMD1 == GameVerbs.U
                || user.CMD1 == GameVerbs.D)
            {
                ActionPlayerMove(user.CMD1);
            }
            else if (user.CMD1 == GameVerbs.INVENTORY || user.CMD1 == GameVerbs.I) // 1, 14 inventory
            {
                ActionInventory();
            }
            else if (user.CMD1 == GameVerbs.SCORE) // 15 score
            {
                ActionScore();
            }
            else if (user.CMD1 == GameVerbs.JUMP) // 16 jump
            {
                ActionJump();
            }
            else if (user.CMD1 == GameVerbs.HELP) // 17 help
            {
                ActionShowIntro();
            }
            else if (user.CMD1 == GameVerbs.TAKE) // 18 take
            {
                Screen.PrintResponse("Take what?");
            }
            else if (user.CMD1 == GameVerbs.DROP) // 19 drop
            {
                Screen.PrintResponse("Drop what?");
            }
            else if (user.CMD1 == GameVerbs.LOOK || user.CMD1 == GameVerbs.L) // 20, 62 look
            {
                ShowLocation();
            }
            else if (user.CMD1 > Constants.OBJECTOFFSET) // objects, not verbs
            {
                Screen.PrintResponse("Do what with " + Constants.VOCABS[user.CMD1] + "?");
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }
        public void ActionTwoWords()
        {
            if (user.CMD1 == GameVerbs.TAKE) // 18 take
            {
                ActionTake();
            }
            else if (user.CMD1 == GameVerbs.DROP) // 19 drop
            {
                ActionDrop();
            }
            else if (user.CMD1 == GameVerbs.LOOK
                || user.CMD1 == GameVerbs.READ
                || user.CMD1 == GameVerbs.EXAMINE
                || user.CMD1 == GameVerbs.X ) // 20 look, 21 read, 22 examine 64 X
            {
                ActionLook();
            }
            else if (user.CMD1 == GameVerbs.UNLOCK) // 23 unlock
            {
                ActionUnlock();
            }
            else if (user.CMD1 == GameVerbs.EAT) // 24 eat
            {
                ActionEat();
            }
            else if (user.CMD1 == GameVerbs.SPIN) // 25 spin
            {
                ActionSpin();
            }
            else if (user.CMD1 == GameVerbs.MOVE) // 26 move
            {
                ActionMoveObj();
            }
            else if (user.CMD1 == GameVerbs.OPEN) // 27 open (X door)
            {
                ActionOpen();
            }
            else if (user.CMD1 == GameVerbs.TIE) // 28 tie
            {
                ActionTieBungeeToRailing();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }
        public void ActionThreeWords()
        {
            if (user.CMD2 == GameObjects.DUMMY || user.CMD3 == GameObjects.DUMMY) // 0
            {
                Screen.PrintResponse("You need 3 words");
            }
            // read note in mirror
            else if ((user.CMD1 == GameVerbs.LOOK // 20 look
                || user.CMD1 == GameVerbs.READ // 21 read
                || user.CMD1 == GameVerbs.EXAMINE) // 22 examine
                && user.CMD2 == GameObjects.NOTE // 42 note
                && user.CMD3 == GameObjects.MIRROR) // 61 mirror
            {
                ActionReadNoteInMirror();
            }
            // move couch with brace
            else if (user.CMD1 == GameVerbs.MOVE // 26 move
                && user.CMD2 == GameObjects.COUCH // 55 couch
                && user.CMD3 == GameObjects.BRACE) // 46 brace
            {
                ActionMoveCouchWithBrace();
            }
            // move fridge with jack
            else if (user.CMD1 == GameVerbs.MOVE // 26 move
                && user.CMD2 == GameObjects.FRIDGE // 54 fridge
                && user.CMD3 == GameObjects.JACK) // 37 jack
            {
                ActionMoveFridgeWithJack();
            }
            // move clothes with gloves
            else if (user.CMD1 == GameVerbs.MOVE // 26 move
                && user.CMD2 == GameObjects.CLOTHES // 56 clothes
                && user.CMD3 == GameObjects.GLOVES) // 44 gloves
            {
                ActionMoveClothesWithGloves();
            }
            // open [direction not mentioned in note] door
            else if (user.CMD1 == GameVerbs.OPEN // 27 open
                && (user.CMD2 == GameVerbs.LEFT // 31 left
                || user.CMD2 == GameVerbs.CENTER // 32 center
                || user.CMD2 == GameVerbs.RIGHT // 33 right
                ) && user.CMD3 == GameObjects.DOOR) // 57 door
            {
                ActionOpen3Door();
            }
            // tie bungee to railing
            else if (user.CMD1 == GameVerbs.TIE // 28 tie
                && user.CMD2 == GameObjects.BUNGEE // 39 bungee
                && user.CMD3 == GameObjects.RAILING) // 58 railing
            {
                ActionTieBungeeToRailing();
            }
            // unlock|oil dumbwaiter with oilcan
            else if ((user.CMD1 == GameVerbs.OIL  // 29 oil
                || user.CMD1 == GameVerbs.UNLOCK) // 23 unlock
                && user.CMD2 == GameObjects.DUMBWAITER // 59 dumbwaiter
                && user.CMD3 == GameObjects.OILCAN) // 48 oilcan
            {
                ActionOilDumbwaiterWithOilcan();
            }
            // put fuse in fusebox
            else if (user.CMD1 == GameVerbs.PUT // 30 put
                && user.CMD2 == GameObjects.FUSE // 36 fuse
                && user.CMD3 == GameObjects.FUSEBOX) // 60 fusebox
            {
                ActionPutFuseInFusebox();
            }
            else
            {
                Screen.PrintResponse("I don't understand...");
            }
        }

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
            Screen.Print("    I, INVENTORY, SCORE, JUMP, HELP, EXIT");
            Screen.Print("    TAKE, DROP, LOOK, READ, EXAMINE, UNLOCK, EAT, SPIN,");
            Screen.Print("    MOVE, OPEN, TIE, OIL, PUT, LEFT, CENTER, RIGHT");
            Screen.Print(" ");
            Screen.Print("Are you ready?");
        }

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

        public void ActionExit()
        {
            Screen.PrintResponse("Thank you for playing, bye!");
            Environment.Exit(0);
        }

        public void ActionLocation()
        {
            Screen.PrintDgb("you are at");
            Screen.Print("    { " + gameState.LOCAL.ToString() + " } " + Constants.gameMapLocation[gameState.LOCAL].rname);
            Screen.Print("    " + Constants.gameMapLocation[gameState.LOCAL].rdesc);
        }

        public void ActionDirections()
        {
            Screen.PrintDgb("you can go");
            for (int i = 1; i <= 6; i++)
            {
                int exit = LocationExit[gameState.LOCAL, i];
                if (exit > 0)
                {
                    Screen.Print("    " + Constants.VOCABS[i] + "\t : ", Constants.gameMapLocation[exit].rname);
                }
            }
        }

        public void ActionExtendedDescriptions()
        {
            Screen.PrintDgb("extras");

            for (int i = 0; i < extDesc.Length; i++)
            {
                int loc = extDesc[i].location;
                int dir = extDesc[i].direction;
                if (gameState.LOCAL == loc && LocationExit[loc, dir] <= 0)
                {
                    Screen.PrintResponse(extDesc[i].description);
                }
            }

            if (gameState.LOCAL == GameRooms.HALLWAY4 // 17 hallway
                && LocationExit[17, 1] > 0)
            {
                Screen.PrintResponse("    Your uncle's doberman is snoring peacefully");
            }
            if (gameState.LOCAL == GameRooms.SITTINGROOM // 3 sitting room
                && ILOC[6] == -12)
            {
                Screen.PrintResponse("    A bungee cord dangles from the railing above");
            }
            if (gameState.LOCAL == 12
                && ILOC[6] == -12)
            {
                Screen.PrintResponse("    A bungee cord dangles from the railing");
            }
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == gameState.LOCAL)
                {
                    Screen.PrintResponse("    There is a " + Constants.VOCABS[i + Constants.OBJECTOFFSET] + " here");
                }
            }
            if (gameState.LOCAL == 2
                && ILOC[3] == -1)
            {
                Screen.PrintResponse("    Something is barely visible under the fridge");
            }
            if (gameState.LOCAL == 3
                && ILOC[5] == 30)
            {
                Screen.PrintResponse("    There is a picture high up on the wall");
            }
        }

        public void ActionTake()
        {
            // below 34 (verbs) above 33 (objects)
            int obj = user.CMD2 - Constants.OBJECTOFFSET;

            if (user.CMD2 < 34) // verbs
            {
                Screen.PrintResponse("Take what?");
                return;
            }

            if (ILOC[obj] == 0)
            {
                Screen.PrintResponse("You already have " + Constants.VOCABS[user.CMD2]);
                return;
            }

            // take picture from the wall
            // picture, sitting hall
            if (user.CMD2 == 32
                && ILOC[obj] == 30
                && gameState.LOCAL == 3)
            {
                Screen.PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // big objects that can't be taken
            if (user.CMD2 >= 54) // fridge, couch, door, railing...
            {
                Screen.PrintResponse("It's too heavy, you can't take that");
                return;
            }

            // bottom of stairs, boxspring
            if (gameState.LOCAL == 29
                && user.CMD2 == 45)
            {
                Screen.PrintResponse("It is better to leave it there");
                return;
            }

            // mid-air, picture
            if (gameState.LOCAL == 30
                && user.CMD2 == 38)
            {
                Screen.PrintResponse("Taking the picture reveals a fusebox");

                ILOC[5] = 0; // picture
                ILOC[obj] = 0;
                ILOC[27] = 30; // Fusebox
                return;
            }

            if (ILOC[obj] != gameState.LOCAL)
            {
                Screen.PrintResponse("There is no " + Constants.VOCABS[user.CMD2] + " here");
                return;
            }

            // if player is carrying object, ILOC[obj] == 0
            ILOC[obj] = 0;
            Screen.PrintResponse(Constants.VOCABS[user.CMD2] + ": taken");
        }
        public void ActionDrop()
        {
            int obj = user.CMD2 - Constants.OBJECTOFFSET;

            if (ILOC[obj] != 0)
            {
                Screen.PrintResponse("You aren't carrying " + Constants.VOCABS[user.CMD2]);
                return;
            }
            // gainesburger, hallway
            if (user.CMD2 == 43
                && gameState.LOCAL == 17
                && LocationExit[17, 1] <= 0)
            {
                Screen.PrintResponse("The dog looks disgusted. maybe you should eat it");
                return;
            }
            // teddybear, hallway
            if (user.CMD2 == 35
                && gameState.LOCAL == 17
                && LocationExit[17, 1] <= 0)
            {
                Screen.PrintResponse("The dog chews his favorite toy and is soon asleep");
                ILOC[obj] = -999;
                LocationExit[17, 1] = 18;
                return;
            }
            // boxpring, bottom of stairs
            if (user.CMD2 == 45
                && gameState.LOCAL == 29
                && LocationExit[29, 5] <= 0)
            {
                Screen.PrintResponse("The boxspring covers the gap in the stairs");
                ILOC[obj] = -999;
                LocationExit[29, 5] = 2;
                LocationExit[2, 6] = 29;
                return;
            }

            // if player is carrying object, ILOC[obj] == current gameState.LOCAL
            ILOC[obj] = gameState.LOCAL;
            Screen.PrintResponse(Constants.VOCABS[user.CMD2] + ": dropped");
        }
        public void ActionLook()
        {
            int obj = user.CMD2 - Constants.OBJECTOFFSET;

            // look at the picture on the wall
            if (ILOC[5] == 30
                && gameState.LOCAL == 3
                && user.CMD2 == 38)
            {
                Screen.PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != 0
                && ILOC[obj] != gameState.LOCAL)
            {
                Screen.PrintResponse("There is no " + Constants.VOCABS[user.CMD2] + " here");
                return;
            }

            // 42=note and (13=master bedroom OR 22=bathroom)
            if (user.CMD2 == 42
                && (gameState.LOCAL == 13
                || gameState.LOCAL == 22))
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

            Screen.PrintResponse("There's nothing special about the " + Constants.VOCABS[user.CMD2]);
        }

        public void ActionSafeDoor()
        {
            // decide which door is safe
            if (gameState.SAFEDoor == 0)
            {
                gameState.SAFEDoor = Utils.RNG(3);
            }

            string N1S = "LEFT";
            string N2S = "RIGHT";

            if (gameState.SAFEDoor == 1)
            {
                N1S = "CENTER";
            }
            if (gameState.SAFEDoor == 3)
            {
                N2S = "CENTER";
            }
            Screen.PrintResponse("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }
        public void ActionUnlock()
        {
            if (gameState.LOCAL != 20
                && gameState.LOCAL != 17)
            {
                Screen.PrintResponse("There is no door here!");
                return;
            }
            // do we have a key?
            if (ILOC[7] != 0) // 13 = master bedroom ? key ?
            {
                Screen.PrintResponse("You don't have a key!");
                return;
            }
            else // yes
            {
                // ckeck gameState.LOCALe
                if (gameState.LOCAL == 5) // Hallway?
                {
                    Screen.PrintResponse("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (gameState.LOCAL == 17
                    && ILOC[7] == 0) // Hallway?
                {
                    Screen.PrintResponse("You unlock the door. beware!");
                    LocationExit[17, 4] = 20;
                    return;
                }
            }
            Screen.PrintResponse("Nothing to unlock!");
        }
        public void ActionOpen()
        {
            // 2 words only

            // dangerous hall: open door
            if (gameState.LOCAL == 20
                && user.CMD2 == 57)
            {
                Screen.PrintResponse("Please specify LEFT, CENTER or RIGHT");
                return;
            }
            else
            {
                Screen.PrintResponse("Open what?");
            }
        }
        public void ActionOpen3Door()
        {
            if (gameState.LOCAL != 20) // dangerous hall
            {
                Screen.PrintResponse("You can't do that here");
                return;
            }

            if (user.CMD3 != 57) // door
            {
                Screen.PrintResponse("Open what?");
                return;
            }

            // open[direction not mentioned in note] door
            int DOORDIR = user.CMD2 - 30;
            if (DOORDIR < 1 || DOORDIR > 3)
            {
                Screen.PrintResponse("Which door?");
                return;
            }

            if (DOORDIR == gameState.SAFEDoor)
            {
                Screen.PrintResponse("Opening the door reveals a dumbwaiter");
                LocationExit[gameState.LOCAL, 4] = 23;
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
        public void ActionEat()
        {
            if (ILOC[10] != 0) //  GAINESBURGER
            {
                Screen.PrintResponse("You don't have it!");
                return;
            }
            else if (user.CMD2 != 42)
            {
                Screen.PrintResponse("You can't eat that!");
                return;
            }

            Screen.PrintResponse("There was a diamond hidden inside the gainesburger");

            ILOC[10] = -2; // GAINESBURGER
            ILOC[17] = 0; //  DIAMOND
        }
        public void ActionSpin()
        {
            if (ILOC[8] != 0) // Top
            {
                Screen.PrintResponse("Spin what?");
                return;
            }
            if (gameState.LOCAL == 18) // child's room
            {
                Screen.PrintResponse("There is a flash of light and a cracking sound! An opening appears in the east wall");
                LocationExit[18, 3] = 19;
                return;
            }

            Screen.PrintResponse("Whee!");
        }
        public void ActionMoveObj()
        {
            if (user.CMD2 == 54) // fridge
            {
                Screen.PrintResponse("It's too heavy for you to move alone (without any help)");
                return;
            }
            if (user.CMD2 == 55) // couch
            {
                Screen.PrintResponse("Your back is acting up, you will need some support");
                return;
            }
            if (user.CMD2 == 56) // clothes
            {
                Screen.PrintResponse("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            Screen.PrintResponse("You can't do that");
        }
        public void ActionJump()
        {
            // location 12 = BALCONY
            if (gameState.LOCAL != 12)
            {
                Screen.PrintResponse("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                return;
            }

            // BUNGEE cord is tied to the railing
            if (ILOC[6] != -12)
            {
                Screen.PrintResponse("You forgot your parachute. Or maybe something else...");
                return;
            }

            Screen.PrintResponse("You bungee off the balcony...");

            // set location to MID-AIR (so can take the picture)
            gameState.LOCAL = 30;
            gameState.TURN1 = 0; // reset for multiple jumps
        }
        public void ActionPlayerMove(int dir)
        {
            // convert N,S,E,W,U,D to long form
            if (dir > 6)
            {
                dir = dir - 6;
            }
            if (LocationExit[gameState.LOCAL, dir] > 0)
            {
                gameState.LOCAL = LocationExit[gameState.LOCAL, dir];
            }
            else if (gameState.LOCAL == 12
                && dir == 5) // up to attic
            {
                Screen.PrintResponse("You're afraid of the dark");
                return;
            }
            else if (gameState.LOCAL == 17
                && dir == 1)
            {
                Screen.PrintResponse("You never did like that dog");
                return;
            }
            else if (gameState.LOCAL == 23
                && LocationExit[23, 6] <= 0)
            {
                Screen.PrintResponse("The dumbwaiter mechanism is corroded and won't move");
                return;
            }
            else
            {
                Screen.PrintResponse("You can't go that way");
            }
        }
        public void ActionMoveFridgeWithJack()
        {
            if (user.CMD1 != 26 || user.CMD2 != 54 || user.CMD3 != 37) // move fridge jack
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("You jack up the fridge and find a fuse under it");
            ILOC[3] = gameState.LOCAL;
        }
        public void ActionMoveCouchWithBrace()
        {
            if (user.CMD1 != 26 || user.CMD2 != 55 || user.CMD3 != 46) // move couch brace
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("You move the couch and find a teddybear behind it");
            ILOC[2] = gameState.LOCAL;
        }
        public void ActionMoveClothesWithGloves()
        {
            if (user.CMD1 != 26 || user.CMD2 != 56 || user.CMD3 != 44) // move clothes gloves
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT");
            LocationExit[gameState.LOCAL, 6] = 27;
        }
        public void ActionTieBungeeToRailing()
        {
            // carrying bungee?
            if (ILOC[6] != 0)
            {
                Screen.PrintResponse("You don't have a bungee cord!");
                return;
            }
            // if not in the Balcony
            if (gameState.LOCAL != 12)
            {
                Screen.PrintResponse("There is nothing here to tie to");
                return;
            }
            // object is not BUNGEE cord
            if (user.CMD2 != 39)
            {
                Screen.PrintResponse("You can't tie that");
                return;
            }
            // rainling
            if (user.CMD3 != 58)
            {
                Screen.PrintResponse("Tie to what?");
                return;
            }

            Screen.PrintResponse("Bungee cord tied to Railing!");

            // BUNGEE cord is tied to the railing
            ILOC[6] = -12;
        }
        public void ActionOilDumbwaiterWithOilcan()
        {
            // not in the dumbwaiter
            if (gameState.LOCAL != 23)
            {
                Screen.PrintResponse("You can't do that here");
                return;
            }
            // 15 OILCAN
            if (ILOC[15] != 0)
            {
                Screen.PrintResponse("You don't have any oil");
                return;
            }
            // dumbwaiter
            if (user.CMD2 != 59)
            {
                Screen.PrintResponse("Oil what?");
                return;
            }

            Screen.PrintResponse("The dumbwaiter mechanism now runs smoothly");
            LocationExit[23, 6] = 24;
        }
        public void ActionPutFuseInFusebox()
        {
            if (gameState.LOCAL != 30 || user.CMD1 != 30 || user.CMD2 != 36)
            {
                Screen.PrintResponse("You can't do that");
                return;
            }
            if (user.CMD3 != 60) // fusebox
            {
                Screen.PrintResponse("You can't put it there");
                return;
            }
            // do we have the fuse?
            if (ILOC[3] != 0)
            {
                Screen.PrintResponse("You don't have it!");
            }

            Screen.PrintResponse("You put the fuse in the box. Power is restored in the Attic!");
            // mark fuse as used
            ILOC[3] = -999;

            // STAIRS TO ATTIC is hidden until fuse is inserted
            LocationExit[12, 5] = 25;
        }
        public void ActionReadNoteInMirror()
        {
            // 42=note and (13=master bedroom OR 22=bathroom)
            if (user.CMD2 == 42
                && (gameState.LOCAL == 13
                || gameState.LOCAL == 22))
            {
                ActionSafeDoor();
                return;
            }
            Screen.PrintResponse("I don't see a mirror here");
        }

    }
}