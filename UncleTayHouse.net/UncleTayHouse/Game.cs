using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public class Game
    {
        public GameResponseModel userInput = new();
        public GameStateModel gameState = new();
        public GameItems gameItems = new();

        public void Play()
        {
            Screen.ClearScreen();

            ActionShowIntro();

            // debug
            //gameState.PlayerAt = CteRooms.DUMBWAITER23;
            //gameItems.houseItems[CteObjects.OILCAN].TakeItem();

            while (!userInput.Exit)
            {
                gameState.ClearMessages();

                ShowLocation();
                gameState.PrintMessages();

                ActionReadAndProcessInput();

                ActionProcessInput();
                gameState.PrintMessages();
            }
        }

        private void ActionReadAndProcessInput()
        {
            string input = GameUserInput.ReadInput();
            userInput = GameUserInput.ProcessInput(input);
        }

        public void ShowLocation()
        {
            // additional game logic for specific locations:

            // first time after jump
            if (gameState.IsPlayerAt(CteRooms.MIDAIR30)
                && !gameState.PlayerJump) // first time is false
            {
                gameState.PlayerJump = true;
            }
            else if (gameState.IsPlayerAt(CteRooms.MIDAIR30)
                && gameState.PlayerJump) // second time is true
            {
                gameState.AddMessage("... and bunge cord spring back");
                gameState.PlayerAt = CteRooms.BALCONY12;
            }

            if (gameState.IsPlayerAt(CteRooms.EXIT31))
            {
                ActionScore();
                ActionExit();
            }

            // show where the player is
            ActionShowLocation();

            // show where the player can go
            ActionShowDirections();

            // some places have an extra description
            ActionShowExtendedDescriptions();
        }

        public void ActionProcessInput()
        {
            if (userInput.NumWords < 1)
            {
                gameState.AddMessage("You need 1 word to move, 2+ words (verb + noun) for actions.");
                return;
            }
            if (userInput.NumWords == 1)
            {
                ActionOneWord();
                return;
            }
            if (userInput.NumWords == 2)
            {
                ActionTwoWords();
                return;
            }
            if (userInput.NumWords == 3)
            {
                ActionThreeWords();
                return;
            }
            gameState.AddMessage("I don't understand...");
        }

        public void ActionOneWord()
        {
            // Move
            if (userInput.CMD1 == CteVerbs.NORTH
                || userInput.CMD1 == CteVerbs.SOUTH
                || userInput.CMD1 == CteVerbs.EAST
                || userInput.CMD1 == CteVerbs.WEST
                || userInput.CMD1 == CteVerbs.UP
                || userInput.CMD1 == CteVerbs.DOWN
                || userInput.CMD1 == CteVerbs.N
                || userInput.CMD1 == CteVerbs.S
                || userInput.CMD1 == CteVerbs.E
                || userInput.CMD1 == CteVerbs.W
                || userInput.CMD1 == CteVerbs.U
                || userInput.CMD1 == CteVerbs.D)
            {
                ActionPlayerMove(userInput.CMD1);
                return;
            }
            if (userInput.CMD1 == CteVerbs.INVENTORY || userInput.CMD1 == CteVerbs.I)
            {
                ActionInventory();
                return;
            }
            if (userInput.CMD1 == CteVerbs.SCORE)
            {
                ActionScore();
                return;
            }
            if (userInput.CMD1 == CteVerbs.JUMP)
            {
                ActionJump();
                return;
            }
            if (userInput.CMD1 == CteVerbs.HELP)
            {
                ActionShowIntro();
                return;
            }
            if (userInput.CMD1 == CteVerbs.TAKE)
            {
                gameState.AddMessage("Take what?"); // need 2 words
                return;
            }
            if (userInput.CMD1 == CteVerbs.DROP)
            {
                gameState.AddMessage("Drop what?"); // need 2 words
                return;
            }
            if (userInput.CMD1 == CteVerbs.LOOK
                || userInput.CMD1 == CteVerbs.L
                || userInput.CMD1 == CteVerbs.EXAMINE
                || userInput.CMD1 == CteVerbs.X)
            {
                ShowLocation();
                return;
            }
            if (userInput.CMD1 > Constants.OBJECTOFFSET) // objects, not verbs
            {
                gameState.AddMessage("Do what with " + Constants.AllWords[userInput.CMD1] + "?");
                return;
            }
            gameState.AddMessage("I don't understand... (1w)");
        }

        public void ActionTwoWords()
        {
            if (userInput.CMD2obj == 0)
            {
                gameState.AddMessage("You need 3 words");
                return;
            }
            if (userInput.CMD1 == CteVerbs.TAKE)
            {
                ActionTake();
                return;
            }
            if (userInput.CMD1 == CteVerbs.DROP)
            {
                ActionDrop();
                return;
            }
            if (userInput.CMD1 == CteVerbs.LOOK
                || userInput.CMD1 == CteVerbs.READ
                || userInput.CMD1 == CteVerbs.EXAMINE
                || userInput.CMD1 == CteVerbs.X)
            {
                ActionLook();
                return;
            }
            if (userInput.CMD1 == CteVerbs.UNLOCK)
            {
                ActionUnlock();
                return;
            }
            if (userInput.CMD1 == CteVerbs.EAT)
            {
                ActionEat();
                return;
            }
            if (userInput.CMD1 == CteVerbs.SPIN)
            {
                ActionSpin();
                return;
            }
            if (userInput.CMD1 == CteVerbs.MOVE)
            {
                ActionMoveObj();
                return;
            }
            if (userInput.CMD1 == CteVerbs.OPEN)
            {
                ActionOpen();
                return;
            }
            if (userInput.CMD1 == CteVerbs.TIE)
            {
                ActionTieBungeeToRailing();
                return;
            }
            gameState.AddMessage("I don't understand... (2w)");
        }

        public void ActionThreeWords()
        {
            if (userInput.CMD2 == 0 || userInput.CMD3 == 0)
            {
                gameState.AddMessage("You need 3 words");
            }
            // read note in mirror
            else if ((userInput.CMD1 == CteVerbs.LOOK // 20 look
                || userInput.CMD1 == CteVerbs.READ // 21 read
                || userInput.CMD1 == CteVerbs.EXAMINE) // 22 examine
                && userInput.CMD2obj == CteObjects.NOTE // 42 note = 9
                && userInput.CMD3obj == CteObjects.MIRROR) // 61 mirror = 28
            {
                ActionReadNoteInMirror();
            }
            // move couch with brace
            else if (userInput.CMD1 == CteVerbs.MOVE // 26 move
                && userInput.CMD2obj == CteObjects.COUCH // 55 couch = 22
                && userInput.CMD3obj == CteObjects.BRACE) // 46 brace = 13
            {
                ActionMoveCouchWithBrace();
            }
            // move couch with jack
            else if (userInput.CMD1 == CteVerbs.MOVE // 26 move
                && userInput.CMD2obj == CteObjects.COUCH // 55 couch = 22
                && userInput.CMD3obj == CteObjects.JACK)
            {
                ActionMoveCouchWithJack();
            }
            // move fridge with jack
            else if (userInput.CMD1 == CteVerbs.MOVE // 26 move
                && userInput.CMD2obj == CteObjects.FRIDGE // 54 fridge = 21
                && userInput.CMD3obj == CteObjects.JACK) // 37 jack = 4
            {
                ActionMoveFridgeWithJack();
            }
            // move clothes with gloves
            else if (userInput.CMD1 == CteVerbs.MOVE // 26 move
                && userInput.CMD2obj == CteObjects.CLOTHES // 56 clothes = 23
                && userInput.CMD3obj == CteObjects.GLOVES) // 44 gloves = 11
            {
                ActionMoveClothesWithGloves();
            }
            // open [direction not mentioned in note] door
            else if (userInput.CMD1 == CteVerbs.OPEN // 27 open
                && (userInput.CMD2 == CteVerbs.LEFT // 31 left
                || userInput.CMD2 == CteVerbs.CENTER // 32 center
                || userInput.CMD2 == CteVerbs.RIGHT // 33 right
                ) && userInput.CMD3obj == CteObjects.DOOR) // 57 door = 24
            {
                ActionOpen3Door();
            }
            // tie bungee to railing
            else if (userInput.CMD1 == CteVerbs.TIE // 28 tie
                && userInput.CMD2obj == CteObjects.BUNGEE // 39 bungee = 6
                && userInput.CMD3obj == CteObjects.RAILING) // 58 railing = 25
            {
                ActionTieBungeeToRailing();
            }
            // unlock|oil dumbwaiter with oilcan
            else if ((userInput.CMD1 == CteVerbs.OIL  // 29 oil
                || userInput.CMD1 == CteVerbs.UNLOCK) // 23 unlock
                && userInput.CMD2obj == CteObjects.DUMBWAITER // 59 dumbwaiter = 26
                && userInput.CMD3obj == CteObjects.OILCAN) // 48 oilcan = 15
            {
                ActionOilDumbwaiterWithOilcan();
            }
            // put fuse in fusebox
            else if (userInput.CMD1 == CteVerbs.PUT // 30 put
                && userInput.CMD2obj == CteObjects.FUSE // 36 fuse = 3
                && userInput.CMD3obj == CteObjects.FUSEBOX) // 60 fusebox = 27
            {
                ActionPutFuseInFusebox();
            }
            else
            {
                gameState.AddMessage("I don't understand...");
            }
        }

        public void ActionInventory()
        {
            gameState.AddMessage(" [Inventory] You are carrying:");
            int total = 0;
            foreach (var item in gameItems.houseItems.Where(item => item.IsCarrying()))
            {
                gameState.AddMessage("   -" + item.name);
                total++;
            }
            if (total == 0)
            {
                gameState.AddMessage("   - nothing yet!");
            }
        }

        public static void ActionShowIntro()
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
        }

        public void ActionScore()
        {
            int SCORE = 50;
            // reduce points for generic items not explored (1-15)
            for (int i = 1; i < 15; i++)
            {
                if (gameItems.houseItems[i].location == -1)
                {
                    SCORE -= 5;
                }
            }
            // add points for valuable items (16-20)
            for (int i = 16; i < 20; i++)
            {
                if (gameItems.houseItems[i].IsCarrying()) // Carrying?
                {
                    SCORE += 10;
                }
            }
            // reduce points for non explored (hidden) locations
            for (int i = 3; i < 30; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    if (gameItems.GetLocationExit(i, j) == -1)
                    {
                        SCORE -= 5;
                    }
                }
            }
            // show result
            gameState.AddMessage("Your score is " + SCORE + " out of a possible 100.");
            if (SCORE == 100)
            {
                gameState.AddMessage("You have won the game!");
            }
        }

        public void ActionExit()
        {
            gameState.AddMessage("Thank you for playing, bye!");
            Environment.Exit(0);
        }

        public void ActionShowLocation()
        {
            string mapId = gameState.PlayerAt.ToString();
            string mapName = gameItems.houseMap[gameState.PlayerAt].rname;
            string mapDesc = gameItems.houseMap[gameState.PlayerAt].rdesc;

            gameState.AddMessage("----------------------------- you are at ----------------------------- ");
            gameState.AddMessage("    { " + mapId + " } " + mapName + " - " + mapDesc);
        }

        public void ActionShowDirections()
        {
            gameState.AddMessage("----------------------------- you can go ----------------------------- ");
            for (int i = 1; i <= 6; i++) // 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
            {
                int exit = gameItems.GetLocationExit(gameState.PlayerAt, i);
                if (exit > 0)
                {
                    string msg = "    " + Constants.AllWords[i] + "\t : " + gameItems.houseMap[exit].rname;
                    gameState.AddMessage(msg);
                }
            }
        }

        public void ActionShowExtendedDescriptions()
        {
            gameState.AddMessage("----------------------------- extras ----------------------------- ");
            // some places have an extended description
            for (int i = 0; i < gameItems.extDesc.Length; i++)
            {
                int loc = gameItems.extDesc[i].location;
                int dir = gameItems.extDesc[i].direction;
                if (gameState.IsPlayerAt(loc)
                    && gameItems.IsExitHidden(loc, dir))
                {
                    gameState.AddMessage("    " + gameItems.extDesc[i].description);
                }
            }

            // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            if (gameState.IsPlayerAt(CteRooms.HALL17) // 17 hallway
                && !gameItems.IsExitHidden(CteRooms.HALL17, CteVerbs.NORTH))
            {
                gameState.AddMessage("    Your uncle's doberman is snoring peacefully");
            }

            // in sitting room, if bungee cord is tied
            if (gameState.IsPlayerAt(CteRooms.SITTINGROOM3) // 3 sitting room
                && gameItems.houseItems[CteObjects.BUNGEE].IsTied()) // 6 = bungee cord & -12 = tied
            {
                gameState.AddMessage("    A bungee cord dangles from the railing above");
            }

            // in balcony, if bungee cord is tied
            if (gameState.IsPlayerAt(CteRooms.BALCONY12) // 12 balcony
                && gameItems.houseItems[CteObjects.BUNGEE].IsTied()) // 6 = bungee cord & -12 = tied
            {
                gameState.AddMessage("    A bungee cord dangles from the railing");
            }

            // show objects in the current location
            foreach (var item in gameItems.houseItems.Where(item => item.IsAt(gameState.PlayerAt)))
            {
                gameState.AddMessage("    There is a " + item.name + " here");
            }

            if (gameState.IsPlayerAt(CteRooms.KITCHEN2) // 2 kitchen
                && gameItems.houseItems[CteObjects.FUSE].IsHidden()) // 3 fuse
            {
                gameState.AddMessage("    Something is barely visible under the fridge");
            }

            if (gameState.IsPlayerAt(CteRooms.SITTINGROOM3) // 3 Sitting room
                && gameItems.houseItems[CteObjects.PICTURE].IsAt(CteRooms.MIDAIR30)) // 5 picture & 30 = MIDAIR30
            {
                gameState.AddMessage("    There is a picture high up on the wall");
            }
        }

        public void ActionTake()
        {
            if (userInput.CMD1 != CteVerbs.TAKE)
            {
                gameState.AddMessage("wrong action");
                return;
            }

            // 1-33 (verbs) 34-61 (objects)
            //int obj = CmdSub(userInput.CMD2obj);
            int obj = userInput.CMD2obj;

            // cant take verbs
            //if (userInput.CMD2obj <= Constants.OBJECTOFFSET
            //    || obj > gameItems.houseItems.Count)
            //{
            //    gameState.AddMessage("Take what?");
            //    return;
            //}

            // check if is not already carrying it
            if (gameItems.houseItems[obj].IsCarrying())
            {
                gameState.AddMessage("You are already carrying " + gameItems.houseItems[obj].name);
                return;
            }

            // big objects that can't be taken
            if (userInput.CMD2obj == CteObjects.FRIDGE
                || userInput.CMD2obj == CteObjects.COUCH
                || userInput.CMD2obj == CteObjects.DOOR
                || userInput.CMD2obj == CteObjects.RAILING
                || userInput.CMD2obj == CteObjects.DUMBWAITER)
            {
                gameState.AddMessage("It's too heavy, you can't take that");
                return;
            }

            // clothes have to be moved with gloves
            if (userInput.CMD2obj == CteObjects.CLOTHES) // 56 clothes = 23
            {
                gameState.AddMessage("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            // take picture from the wall (3=sitting room)
            if (userInput.CMD2obj == CteObjects.PICTURE // 38 picture = 5
                && gameItems.houseItems[obj].IsAt(CteRooms.MIDAIR30) // 30 mid-air (means picture is in mid-air)
                && gameState.IsPlayerAt(CteRooms.SITTINGROOM3)) // 3 sitting room
            {
                gameState.AddMessage("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // take picture from mid-air
            if (userInput.CMD2obj == CteObjects.PICTURE // 38 picture = 5
                && gameState.IsPlayerAt(CteRooms.MIDAIR30)) // 30 mid-air
            {
                gameState.AddMessage("Taking the picture reveals a fusebox");

                gameItems.houseItems[CteObjects.PICTURE].TakeItem(); // picture is being carried
                gameItems.houseItems[CteObjects.FUSEBOX].LeaveItem(CteRooms.MIDAIR30); // fusebox is now in mid-air
                return;
            }

            // take boxspring from bottom of stairs
            if (userInput.CMD2obj == CteObjects.BOXSPRING // 45 boxspring = 12
                && gameState.IsPlayerAt(CteRooms.BOTTOMOFSTAIRS29)) // 29 bottom of stairs
            {
                gameState.AddMessage("It is better to leave it there");
                return;
            }

            // check if the object is here and not hidden
            if (!gameItems.houseItems[obj].IsAt(gameState.PlayerAt))
            {
                gameState.AddMessage("There is no " + Constants.AllWords[userInput.CMD2] + " here");
                return;
            }

            // player is carrying object
            gameItems.houseItems[obj].TakeItem();
            gameState.AddMessage(Constants.AllWords[userInput.CMD2] + ": taken");
        }

        public void ActionDrop()
        {
            int obj = userInput.CMD2obj;

            // check if is carrying it first
            if (!gameItems.houseItems[obj].IsCarrying())
            {
                gameState.AddMessage("You aren't carrying " + Constants.AllWords[userInput.CMD2]);
                return;
            }

            // drop gainesburger hallway
            if (userInput.CMD2obj == CteObjects.GAINESBURGER // 43 gainesburger = 10
                && gameState.IsPlayerAt(CteRooms.HALL17) // 17 hallway
                && gameItems.IsExitHidden(CteRooms.HALL17, CteVerbs.NORTH)) // north exit is not open yet
            {
                gameState.AddMessage("The dog looks disgusted. maybe you should eat it");
                return;
            }

            // drop teddybear hallway
            if (userInput.CMD2obj == CteObjects.TEDDYBEAR // 35 teddybear = 2
                && gameState.IsPlayerAt(CteRooms.HALL17) // 17 hallway
                && gameItems.IsExitHidden(CteRooms.HALL17, CteVerbs.NORTH)) // north exit is not open yet
            {
                gameState.AddMessage("The dog chews his favorite toy and is soon asleep");

                gameItems.houseItems[CteObjects.TEDDYBEAR].HideItem(); // teddybear is now hidden
                gameItems.SetLocationExit(CteRooms.HALL17, CteVerbs.NORTH, CteRooms.CHILDSROOM18); // reveal secret room north
                return;
            }

            // drop boxpring bottom of stairs
            if (userInput.CMD2obj == CteObjects.BOXSPRING // 45 boxspring = 12
                && gameState.IsPlayerAt(CteRooms.BOTTOMOFSTAIRS29) // 29 bottom of stairs
                && gameItems.IsExitHidden(CteRooms.BOTTOMOFSTAIRS29, CteVerbs.UP)) // 5
            {
                gameState.AddMessage("The boxspring covers the gap in the stairs");

                gameItems.houseItems[CteObjects.BOXSPRING].HideItem(); // 12 boxspring is now hidden
                // unlock stairs to basement (both ways)
                gameItems.SetLocationExit(CteRooms.BOTTOMOFSTAIRS29, CteVerbs.UP, CteRooms.KITCHEN2); // unlock up to kitchen
                gameItems.SetLocationExit(CteRooms.KITCHEN2, CteVerbs.DOWN, CteRooms.BOTTOMOFSTAIRS29); // unlock down to bottom of stairs
                return;
            }

            // leave the object in the current location
            gameItems.houseItems[obj].LeaveItem(gameState.PlayerAt);

            gameState.AddMessage(Constants.AllWords[userInput.CMD2] + ": dropped");
        }

        public void ActionLook()
        {
            int obj = userInput.CMD2obj;

            // check if the object is here and not hidden
            if (gameItems.houseItems[obj].IsHidden() // can't look at hidden object
                || !(gameItems.houseItems[obj].IsAt(gameState.PlayerAt) // is not here
                || gameItems.houseItems[obj].IsCarrying())) // or player is not carying it
            {
                gameState.AddMessage("There is no " + Constants.AllWords[userInput.CMD2] + " here");
                return;
            }

            // look at the picture on the wall
            if (userInput.CMD2obj == CteObjects.PICTURE // 38 picture = 5
                && gameState.IsPlayerAt(CteRooms.SITTINGROOM3) // 3 sitting room
                && gameItems.houseItems[CteObjects.PICTURE].IsAt(CteRooms.MIDAIR30)) // 30 mid-air (means picture is in mid-air)
            {
                gameState.AddMessage("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // look 42=note and (13=master bedroom OR 22=bathroom)
            if (userInput.CMD2obj == CteObjects.NOTE // 42 note = 9
                && (gameState.IsPlayerAt(CteRooms.MASTERBEDROOM13) // 13 master bedroom
                || gameState.IsPlayerAt(CteRooms.BATHROOM22))) // 22 bathroom
            {
                ActionSafeDoor();
                return;
            }

            // Print extended obj description
            if (!string.IsNullOrEmpty(gameItems.houseItems[obj].desc))
            {
                gameState.AddMessage(gameItems.houseItems[obj].desc);
                return;
            }

            gameState.AddMessage("There's nothing special about the " + Constants.AllWords[userInput.CMD2]);
        }

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
            gameState.AddMessage("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }

        public void ActionUnlock()
        {
            // door only exists in one of these locations
            if (!(gameState.IsPlayerAt(CteRooms.HALL5)
                || gameState.IsPlayerAt(CteRooms.LIBRARY8)
                || gameState.IsPlayerAt(CteRooms.HALL20)
                || gameState.IsPlayerAt(CteRooms.HALL17)))
            {
                gameState.AddMessage("There is no door here!");
                return;
            }
            // do we have a key?
            if (!gameItems.houseItems[CteObjects.KEY].IsCarrying()) // 7 key
            {
                gameState.AddMessage("You don't have a key!");
                return;
            }
            else // yes
            {
                // ckeck where we are
                if (gameState.IsPlayerAt(CteRooms.HALL5)) // 5 Hallway?
                {
                    gameState.AddMessage("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (gameState.IsPlayerAt(CteRooms.HALL17) // 17 hall
                    && gameItems.houseItems[CteObjects.KEY].IsCarrying()) // 7 key
                {
                    gameState.AddMessage("You unlock the door. Beware!");
                    gameItems.SetLocationExit(CteRooms.HALL17, CteVerbs.WEST, CteRooms.HALL20); // unlock door west to hall 20
                    return;
                }
            }
            gameState.AddMessage("Nothing to unlock!");
        }

        public void ActionOpen()
        {
            // open door == unlock door
            if (userInput.CMD2obj == CteObjects.DOOR) // 57 door = 2
            {
                ActionUnlock();
                return;
            }
            // open door in dangerous hall 20
            if (userInput.CMD2obj == CteObjects.DOOR // 57 door = 2
                && gameState.IsPlayerAt(CteRooms.HALL20)) // 20 hall
            {
                gameState.AddMessage("Please specify LEFT, CENTER or RIGHT");
                return;
            }
            gameState.AddMessage("Open what?");
        }

        public void ActionOpen3Door()
        {
            if (!gameState.IsPlayerAt(CteRooms.HALL20)) // 20 dangerous hall
            {
                gameState.AddMessage("You can't open that door here");
                return;
            }

            if (userInput.CMD3obj != CteObjects.DOOR) // 57 door
            {
                gameState.AddMessage("Open what?");
                return;
            }

            // open [direction not mentioned in note] door
            // 31 = left
            // 32 = center
            // 33 = right
            int DOORDIR = userInput.CMD2 - 30;
            if (DOORDIR < 1 || DOORDIR > 3)
            {
                gameState.AddMessage("Which door?");
                return;
            }

            if (DOORDIR == gameState.SafeDoor)
            {
                gameState.AddMessage("Opening the door reveals a dumbwaiter in the west room");
                gameItems.SetLocationExit(CteRooms.HALL20, CteVerbs.WEST, CteRooms.DUMBWAITER23); // reveal dumbwaiter in the west room
                return;
            }

            // Trap: if you haven't read the note before
            int rnd = Utils.RNG(100);
            if (rnd > 50)
            {
                gameState.AddMessage("BAM! A shot rings out! it was well-aimed too.");
                // you die
                return;
            }

            gameState.AddMessage("An ironing board slams onto your head");
            // you die
        }

        public void ActionEat()
        {
            if (!gameItems.houseItems[CteObjects.GAINESBURGER].IsCarrying()) // 10 GAINESBURGER
            {
                gameState.AddMessage("You don't have it!");
                return;
            }
            if (userInput.CMD2obj != CteObjects.GAINESBURGER)
            {
                gameState.AddMessage("You can't eat that!");
                return;
            }

            gameState.AddMessage("There was a diamond hidden inside the gainesburger");

            gameItems.houseItems[CteObjects.GAINESBURGER].HideItem(); // 10 gainesburger is now hidden
            gameItems.houseItems[CteObjects.DIAMOND].LeaveItem(gameState.PlayerAt); // 17 diamond is now in the player's location
        }

        public void ActionSpin()
        {
            if (!gameItems.houseItems[CteObjects.SPINNINGTOP].IsCarrying()) // 8 spinningtop
            {
                gameState.AddMessage("Spin what?");
                return;
            }
            if (gameState.IsPlayerAt(CteRooms.CHILDSROOM18)) // 18 child's room
            {
                gameState.AddMessage("There is a flash of light and a cracking sound! An opening appears in the east wall");
                gameItems.SetLocationExit(CteRooms.CHILDSROOM18, CteVerbs.EAST, CteRooms.SECRETROOM19); // reveal east secret dark room
                return;
            }

            gameState.AddMessage("Whee!");
        }

        public void ActionMoveObj()
        {
            if (userInput.CMD2obj == CteObjects.FRIDGE) // 54 fridge = 21
            {
                gameState.AddMessage("It's too heavy for you to move alone (without any help)");
                return;
            }
            if (userInput.CMD2obj == CteObjects.COUCH) // 55 couch = 22
            {
                gameState.AddMessage("Your back is acting up, you will need some support");
                return;
            }
            if (userInput.CMD2obj == CteObjects.CLOTHES) // 56 clothes = 23
            {
                gameState.AddMessage("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            gameState.AddMessage("You can't do that");
        }

        public void ActionJump()
        {
            // check if we are at the balcony
            if (!gameState.IsPlayerAt(CteRooms.BALCONY12)) // 12 Balcony
            {
                gameState.AddMessage("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                return;
            }

            // check if BUNGEE cord is tied to the railing
            if (!gameItems.houseItems[CteObjects.BUNGEE].IsTied()) // 6 bungee is tied to the railing
            {
                gameState.AddMessage("You forgot your parachute. Or maybe something else...");
                return;
            }

            gameState.AddMessage("You bungee off the balcony...");

            // set location to MID-AIR (so can take the picture)
            gameState.PlayerAt = CteRooms.MIDAIR30; // 30 mid-air
            gameState.PlayerJump = false; // reset for multiple jumps
        }

        public void ActionPlayerMove(int dir)
        {
            // convert N,S,E,W,U,D to long form
            if (dir > 6)
            {
                dir -= 6;
            }
            if (!gameItems.IsExitHidden(gameState.PlayerAt, dir))
            {
                gameState.PlayerAt = gameItems.GetLocationExit(gameState.PlayerAt, dir);
                return;
            }
            if (gameState.IsPlayerAt(CteRooms.BALCONY12) // 12 attic
                && dir == CteVerbs.UP) // 5 up to attic
            {
                gameState.AddMessage("You're afraid of the dark");
                return;
            }
            if (gameState.IsPlayerAt(CteRooms.HALL17) // 17 hall
                && dir == CteVerbs.NORTH) // 1 north
            {
                gameState.AddMessage("You never did like that dog, and he will not let you pass");
                return;
            }
            if (gameState.IsPlayerAt(CteRooms.DUMBWAITER23) // 23 dumbwaiter
                && gameItems.IsExitHidden(CteRooms.DUMBWAITER23, CteVerbs.DOWN)) // D: is blocked, unlock with oilcan
            {
                gameState.AddMessage("The dumbwaiter mechanism is corroded and won't move");
                return;
            }
            gameState.AddMessage("You can't go that way");
        }

        public void ActionMoveFridgeWithJack()
        {
            if (userInput.CMD1 != CteVerbs.MOVE // 26 move
                || userInput.CMD2obj != CteObjects.FRIDGE // 54 fridge
                || userInput.CMD3obj != CteObjects.JACK) // 37 jack
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("You jack up the fridge and find a fuse under it");

            gameItems.houseItems[CteObjects.FUSE].LeaveItem(gameState.PlayerAt); // reveal fuse
        }

        public void ActionMoveCouchWithBrace()
        {
            if (userInput.CMD1 != CteVerbs.MOVE // 26 move
                || userInput.CMD2obj != CteObjects.COUCH // 55 couch
                || userInput.CMD3obj != CteObjects.BRACE) // 46 brace
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("You move the couch and find a teddybear behind it");

            gameItems.houseItems[CteObjects.TEDDYBEAR].LeaveItem(gameState.PlayerAt); // reveal teddybear
        }
        public void ActionMoveCouchWithJack()
        {
            if (userInput.CMD1 != CteVerbs.MOVE
                || userInput.CMD2obj != CteObjects.COUCH
                || userInput.CMD3obj != CteObjects.JACK)
            {
                gameState.AddMessage("You can't move the couch with that");
                return;
            }

            gameState.AddMessage("You can't fit the jack there, maybe try something else");
        }

        public void ActionMoveClothesWithGloves()
        {
            if (userInput.CMD1 != CteVerbs.MOVE // 26 move
                || userInput.CMD2obj != CteObjects.CLOTHES // 56 clothes
                || userInput.CMD3obj != CteObjects.GLOVES) // 44 gloves
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("Moving the clothes reveals a laundry chute to the basement");
            gameItems.SetLocationExit(CteRooms.BATHROOM7, CteVerbs.DOWN, CteRooms.LAUNDRY27); // down to basement
        }

        public void ActionTieBungeeToRailing()
        {
            // is carrying a bungee cord?
            if (!gameItems.houseItems[CteObjects.BUNGEE].IsCarrying()) // 6 bungee
            {
                gameState.AddMessage("You don't have a bungee cord!");
                return;
            }
            // is in the Balcony
            if (!gameState.IsPlayerAt(CteRooms.BALCONY12)) // 12 balcony
            {
                gameState.AddMessage("There is nothing here to tie to");
                return;
            }
            // object is not BUNGEE cord
            if (userInput.CMD2obj != CteObjects.BUNGEE) // 39 bungee
            {
                gameState.AddMessage("You can't tie that");
                return;
            }
            // rainling
            if (userInput.CMD3obj != CteObjects.RAILING) // 58 railing
            {
                gameState.AddMessage("Tie to what?");
                return;
            }

            gameState.AddMessage("Bungee cord tied to Railing!");

            gameItems.houseItems[CteObjects.BUNGEE].TieItem(); // 6 bungee is tied to the railing
        }

        public void ActionOilDumbwaiterWithOilcan()
        {
            // is in the dumbwaiter?
            if (!gameState.IsPlayerAt(CteRooms.DUMBWAITER23)) // 23 dumbwaiter
            {
                gameState.AddMessage("You can't do that here");
                return;
            }
            if (!gameItems.houseItems[CteObjects.OILCAN].IsCarrying()) // 15 oilcan
            {
                gameState.AddMessage("You don't have any oil");
                return;
            }
            if (userInput.CMD2obj != CteObjects.DUMBWAITER) // 59 dumbwaiter
            {
                gameState.AddMessage("Oil what?");
                return;
            }

            gameState.AddMessage("The dumbwaiter mechanism now runs smoothly");
            gameItems.SetLocationExit(CteRooms.DUMBWAITER23, CteVerbs.DOWN, CteRooms.DUMBWAITER24); // reveal down to dumbwaiter24
        }

        public void ActionPutFuseInFusebox()
        {
            // check if we have the fuse
            if (!gameItems.houseItems[CteObjects.FUSE].IsCarrying())
            {
                gameState.AddMessage("You don't have it!");
                return;
            }

            if (userInput.CMD3obj != CteObjects.FUSEBOX) // 60 fusebox
            {
                gameState.AddMessage("You can't put it there");
                return;
            }

            if (userInput.CMD1 != CteVerbs.PUT // 30 put
                || userInput.CMD2obj != CteObjects.FUSE // 36 fuse
                || userInput.CMD3obj != CteObjects.FUSEBOX // 60 fusebox
                || !gameState.IsPlayerAt(CteRooms.MIDAIR30))// 30 mid-air
            {
                gameState.AddMessage("You can't do that here");
                return;
            }

            gameState.AddMessage("You put the fuse in the box. Power is restored in the Attic!");

            // mark fuse as hidden
            gameItems.houseItems[CteObjects.FUSE].HideItem();
            gameItems.SetLocationExit(CteRooms.BALCONY12, CteVerbs.UP, CteRooms.ATTIC25); // stairs to attic is hidden until fuse is inserted
        }

        public void ActionReadNoteInMirror()
        {
            // check if we have the note
            if (!gameItems.houseItems[CteObjects.NOTE].IsCarrying())
            {
                gameState.AddMessage("Which note?");
                return;
            }
            // we can only read the note in the mirror in 2 places
            if (userInput.CMD2obj == CteObjects.NOTE // 42 note = 9
                && (gameState.IsPlayerAt(CteRooms.MASTERBEDROOM13) // 13 master bedroom
                || gameState.IsPlayerAt(CteRooms.BATHROOM22))) // 22 bathroom
            {
                ActionSafeDoor();
                return;
            }
            gameState.AddMessage("I don't see a mirror here");
        }

    }
}