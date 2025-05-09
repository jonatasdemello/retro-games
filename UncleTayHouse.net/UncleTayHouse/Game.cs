﻿using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public partial class Game
    {
        public GameUserInput userInput = new();
        public GameState gameState = new();
        public GameItems gameItems = new();

        // adjust the id, adding offset
        public static int CmdAdd(int num) => num + Constants.OBJECTOFFSET;
        public static int CmdSub(int num) => num - Constants.OBJECTOFFSET;

        public void Play()
        {
            Screen.ClearScreen();

            ActionShowIntro();

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
            // read and process user input
            string input = UserInput.ReadInput();
            userInput = UserInput.ProcessInput(input);
        }

        public void ShowLocation()
        {
            // additional game logic for specific locations:

            // first time after jump
            if (gameState.IsAt(GameRooms.MIDAIR30)
                && !gameState.PlayerJump) // first time is false
            {
                gameState.PlayerJump = true;
            }
            else if (gameState.IsAt(GameRooms.MIDAIR30)
                && gameState.PlayerJump) // second time is true
            {
                gameState.AddMessage("... and bunge cord spring back");
                gameState.PlayerAt = GameRooms.BALCONY12;
            }

            if (gameState.IsAt(GameRooms.EXIT31))
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
        // ok
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
            else if (userInput.CMD1 == GameVerbs.INVENTORY || userInput.CMD1 == GameVerbs.I)
            {
                ActionInventory();
            }
            else if (userInput.CMD1 == GameVerbs.SCORE)
            {
                ActionScore();
            }
            else if (userInput.CMD1 == GameVerbs.JUMP)
            {
                ActionJump();
            }
            else if (userInput.CMD1 == GameVerbs.HELP)
            {
                ActionShowIntro();
            }
            else if (userInput.CMD1 == GameVerbs.TAKE)
            {
                gameState.AddMessage("Take what?"); // need 2 words
            }
            else if (userInput.CMD1 == GameVerbs.DROP)
            {
                gameState.AddMessage("Drop what?"); // need 2 words
            }
            else if (userInput.CMD1 == GameVerbs.LOOK
                || userInput.CMD1 == GameVerbs.L
                || userInput.CMD1 == GameVerbs.EXAMINE
                || userInput.CMD1 == GameVerbs.X)
            {
                ShowLocation();
            }
            else if (userInput.CMD1 > Constants.OBJECTOFFSET) // objects, not verbs
            {
                gameState.AddMessage("Do what with " + Texts.VOCABS[userInput.CMD1] + "?");
            }
            else
            {
                gameState.AddMessage("I don't understand...");
            }
        }
        // ok
        public void ActionTwoWords()
        {
            if (userInput.CMD1 == GameVerbs.TAKE)
            {
                ActionTake();
            }
            else if (userInput.CMD1 == GameVerbs.DROP)
            {
                ActionDrop();
            }
            else if (userInput.CMD1 == GameVerbs.LOOK
                || userInput.CMD1 == GameVerbs.READ
                || userInput.CMD1 == GameVerbs.EXAMINE
                || userInput.CMD1 == GameVerbs.X)
            {
                ActionLook();
            }
            else if (userInput.CMD1 == GameVerbs.UNLOCK)
            {
                ActionUnlock();
            }
            else if (userInput.CMD1 == GameVerbs.EAT)
            {
                ActionEat();
            }
            else if (userInput.CMD1 == GameVerbs.SPIN)
            {
                ActionSpin();
            }
            else if (userInput.CMD1 == GameVerbs.MOVE)
            {
                ActionMoveObj();
            }
            else if (userInput.CMD1 == GameVerbs.OPEN)
            {
                ActionOpen();
            }
            else if (userInput.CMD1 == GameVerbs.TIE)
            {
                ActionTieBungeeToRailing();
            }
            else
            {
                gameState.AddMessage("I don't understand...");
            }
        }
        // ok
        public void ActionThreeWords()
        {
            if (userInput.CMD2 == 0 || userInput.CMD3 == 0) // 0
            {
                gameState.AddMessage("You need 3 words");
            }
            // read note in mirror
            else if ((userInput.CMD1 == GameVerbs.LOOK // 20 look
                || userInput.CMD1 == GameVerbs.READ // 21 read
                || userInput.CMD1 == GameVerbs.EXAMINE) // 22 examine
                && userInput.CMD2 == CmdAdd(GameObjects.NOTE) // 42 note
                && userInput.CMD3 == CmdAdd(GameObjects.MIRROR)) // 61 mirror
            {
                ActionReadNoteInMirror();
            }
            // move couch with brace
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == CmdAdd(GameObjects.COUCH) // 55 couch
                && userInput.CMD3 == CmdAdd(GameObjects.BRACE)) // 46 brace
            {
                ActionMoveCouchWithBrace();
            }
            // move fridge with jack
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == CmdAdd(GameObjects.FRIDGE) // 54 fridge
                && userInput.CMD3 == CmdAdd(GameObjects.JACK)) // 37 jack
            {
                ActionMoveFridgeWithJack();
            }
            // move clothes with gloves
            else if (userInput.CMD1 == GameVerbs.MOVE // 26 move
                && userInput.CMD2 == CmdAdd(GameObjects.CLOTHES) // 56 clothes
                && userInput.CMD3 == CmdAdd(GameObjects.GLOVES)) // 44 gloves
            {
                ActionMoveClothesWithGloves();
            }
            // open [direction not mentioned in note] door
            else if (userInput.CMD1 == GameVerbs.OPEN // 27 open
                && (userInput.CMD2 == GameVerbs.LEFT // 31 left
                || userInput.CMD2 == GameVerbs.CENTER // 32 center
                || userInput.CMD2 == GameVerbs.RIGHT // 33 right
                ) && userInput.CMD3 == CmdAdd(GameObjects.DOOR)) // 57 door
            {
                ActionOpen3Door();
            }
            // tie bungee to railing
            else if (userInput.CMD1 == GameVerbs.TIE // 28 tie
                && userInput.CMD2 == CmdAdd(GameObjects.BUNGEE) // 39 bungee
                && userInput.CMD3 == CmdAdd(GameObjects.RAILING)) // 58 railing
            {
                ActionTieBungeeToRailing();
            }
            // unlock|oil dumbwaiter with oilcan
            else if ((userInput.CMD1 == GameVerbs.OIL  // 29 oil
                || userInput.CMD1 == GameVerbs.UNLOCK) // 23 unlock
                && userInput.CMD2 == CmdAdd(GameObjects.DUMBWAITER) // 59 dumbwaiter
                && userInput.CMD3 == CmdAdd(GameObjects.OILCAN)) // 48 oilcan
            {
                ActionOilDumbwaiterWithOilcan();
            }
            // put fuse in fusebox
            else if (userInput.CMD1 == GameVerbs.PUT // 30 put
                && userInput.CMD2 == CmdAdd(GameObjects.FUSE) // 36 fuse
                && userInput.CMD3 == CmdAdd(GameObjects.FUSEBOX)) // 60 fusebox
            {
                ActionPutFuseInFusebox();
            }
            else
            {
                gameState.AddMessage("I don't understand...");
            }
        }
        // ok
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
        }
        // ok
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
                    if (LocationExit[i, j] == -1)
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
        // ok
        public void ActionExit()
        {
            gameState.AddMessage("Thank you for playing, bye!");
            Environment.Exit(0);
        }
        // ok
        public void ActionShowLocation()
        {
            string mapId = gameState.PlayerAt.ToString();
            string mapName = gameItems.houseMap[gameState.PlayerAt].rname;
            string mapDesc = gameItems.houseMap[gameState.PlayerAt].rdesc;

            gameState.AddMessage("----------------------------- you are at ----------------------------- ");
            gameState.AddMessage("    { " + mapId + " } " + mapName + " - " + mapDesc);
        }
        // ok
        public void ActionShowDirections()
        {
            gameState.AddMessage("----------------------------- you can go ----------------------------- ");
            for (int i = 1; i <= 6; i++) // 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
            {
                int exit = LocationExit[gameState.PlayerAt, i];
                if (exit > 0)
                {
                    string msg = "    " + Texts.VOCABS[i] + "\t : " + gameItems.houseMap[exit].rname;
                    gameState.AddMessage(msg);
                }
            }
        }
        // ok
        public void ActionShowExtendedDescriptions()
        {
            gameState.AddMessage("----------------------------- extras ----------------------------- ");
            // some places have an extended description
            for (int i = 0; i < gameItems.extDesc.Length; i++)
            {
                int loc = gameItems.extDesc[i].location;
                int dir = gameItems.extDesc[i].direction;
                if (gameState.IsAt(loc) && LocationExit[loc, dir] <= 0)
                {
                    gameState.AddMessage("    " + gameItems.extDesc[i].description);
                }
            }

            // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            if (gameState.IsAt(GameRooms.HALL17) // 17 hallway
                && LocationExit[17, 1] > 0)
            {
                gameState.AddMessage("    Your uncle's doberman is snoring peacefully");
            }

            // in sitting room, if bungee cord is tied
            if (gameState.IsAt(GameRooms.SITTINGROOM3) // 3 sitting room
                && gameItems.houseItems[GameObjects.BUNGEE].IsTied()) // 6 = bungee cord & -12 = tied
            {
                gameState.AddMessage("    A bungee cord dangles from the railing above");
            }

            // in balcony, if bungee cord is tied
            if (gameState.IsAt(GameRooms.BALCONY12) // 12 balcony
                && gameItems.houseItems[GameObjects.BUNGEE].IsTied()) // 6 = bungee cord & -12 = tied
            {
                gameState.AddMessage("    A bungee cord dangles from the railing");
            }

            // show objects in the current location
            foreach (var item in gameItems.houseItems.Where(item => item.IsAt(gameState.PlayerAt)))
            {
                gameState.AddMessage("    There is a " + item.name + " here");
            }

            if (gameState.IsAt(GameRooms.KITCHEN2) // 2 kitchen
                && gameItems.houseItems[GameObjects.FUSE].IsHidden()) // 3 fuse
            {
                gameState.AddMessage("    Something is barely visible under the fridge");
            }

            if (gameState.IsAt(GameRooms.SITTINGROOM3) // 3 Sitting room
                && gameItems.houseItems[GameObjects.PICTURE].IsAt(GameRooms.MIDAIR30)) // 5 picture & 30 = MIDAIR30
            {
                gameState.AddMessage("    There is a picture high up on the wall");
            }
        }
        // ok
        public void ActionTake()
        {
            if (userInput.CMD1 != GameVerbs.TAKE)
            {
                gameState.AddMessage("wrong action");
                return;
            }

            // 1-33 (verbs) 34-61 (objects)
            int obj = CmdSub(userInput.CMD2);

            // cant take verbs
            if (userInput.CMD2 <= Constants.OBJECTOFFSET
                || obj > gameItems.houseItems.Count)
            {
                gameState.AddMessage("Take what?");
                return;
            }

            // check if is not already carrying it
            if (gameItems.houseItems[obj].IsCarrying())
            {
                gameState.AddMessage("You are already carrying " + gameItems.houseItems[obj].name);
                return;
            }

            // big objects that can't be taken
            if (userInput.CMD2 == GameObjects.FRIDGE
                || userInput.CMD2 == GameObjects.COUCH
                || userInput.CMD2 == GameObjects.DOOR
                || userInput.CMD2 == GameObjects.RAILING
                || userInput.CMD2 == GameObjects.DUMBWAITER)
            {
                gameState.AddMessage("It's too heavy, you can't take that");
                return;
            }

            // take picture from the wall (3=sitting room)
            if (userInput.CMD2 == CmdAdd(GameObjects.PICTURE) // 38 picture (38-33 = 5)
                && gameItems.houseItems[obj].IsAt(GameRooms.MIDAIR30) // 30 mid-air (means picture is in mid-air)
                && gameState.IsAt(GameRooms.SITTINGROOM3)) // 3 sitting room
            {
                gameState.AddMessage("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // take picture from mid-air
            if (userInput.CMD2 == CmdAdd(GameObjects.PICTURE) // 38 picture
                && gameState.IsAt(GameRooms.MIDAIR30)) // 30 mid-air
            {
                gameState.AddMessage("Taking the picture reveals a fusebox");

                gameItems.houseItems[GameObjects.PICTURE].TakeItem(); // picture is being carried
                gameItems.houseItems[GameObjects.FUSEBOX].LeaveItem(GameRooms.MIDAIR30); // fusebox is now in mid-air
                return;
            }

            // take boxspring from bottom of stairs
            if (userInput.CMD2 == CmdAdd(GameObjects.BOXSPRING) // 45 boxspring
                && gameState.IsAt(GameRooms.BOTTOMOFSTAIRS29)) // 29 bottom of stairs
            {
                gameState.AddMessage("It is better to leave it there");
                return;
            }

            // check if the object is here and not hidden
            if (!gameItems.houseItems[obj].IsAt(gameState.PlayerAt))
            {
                gameState.AddMessage("There is no " + Texts.VOCABS[userInput.CMD2] + " here");
                return;
            }

            // player is carrying object
            gameItems.houseItems[obj].TakeItem();
            gameState.AddMessage(Texts.VOCABS[userInput.CMD2] + ": taken");
        }
        // ok
        public void ActionDrop()
        {
            int obj = CmdSub(userInput.CMD2);

            // check if is carrying it first
            if (!gameItems.houseItems[obj].IsCarrying())
            {
                gameState.AddMessage("You aren't carrying " + Texts.VOCABS[userInput.CMD2]);
                return;
            }

            // drop gainesburger hallway
            if (userInput.CMD2 == CmdAdd(GameObjects.GAINESBURGER) // 43 gainesburger
                && gameState.IsAt(GameRooms.HALL17) // 17 hallway
                && LocationExit[17, 1] <= 0) // north exit is not open yet
            {
                gameState.AddMessage("The dog looks disgusted. maybe you should eat it");
                return;
            }

            // drop teddybear hallway
            if (userInput.CMD2 == CmdAdd(GameObjects.TEDDYBEAR) // 35 teddybear
                && gameState.IsAt(GameRooms.HALL17) // 17 hallway
                && LocationExit[17, 1] <= 0) // north exit is not open yet
            {
                gameState.AddMessage("The dog chews his favorite toy and is soon asleep");

                gameItems.houseItems[GameObjects.TEDDYBEAR].HideItem(); // teddybear is now hidden
                LocationExit[17, 1] = 18; // reveal secret room north
                return;
            }

            // drop boxpring bottom of stairs
            if (userInput.CMD2 == CmdAdd(GameObjects.BOXSPRING) // 45 boxspring
                && gameState.IsAt(GameRooms.BOTTOMOFSTAIRS29) // 29 bottom of stairs
                && LocationExit[29, 5] <= 0)
            {
                gameState.AddMessage("The boxspring covers the gap in the stairs");

                gameItems.houseItems[GameObjects.BOXSPRING].HideItem(); // 12 boxspring is now hidden

                LocationExit[29, 5] = 2; // U: unlock with drop boxspring
                LocationExit[2, 6] = 29; // D: STAIRS TO BASEMENT is hidden until drop Boxspring
                return;
            }

            // leave the object in the current location
            gameItems.houseItems[obj].LeaveItem(gameState.PlayerAt);

            gameState.AddMessage(Texts.VOCABS[userInput.CMD2] + ": dropped");
        }
        // ok
        public void ActionLook()
        {
            int obj = CmdSub(userInput.CMD2);

            // check if the object is here and not hidden
            if (gameItems.houseItems[obj].IsHidden()
                || !gameItems.houseItems[obj].IsAt(gameState.PlayerAt))
            {
                gameState.AddMessage("There is no " + Texts.VOCABS[userInput.CMD2] + " here");
                return;
            }

            // look at the picture on the wall
            if (userInput.CMD2 == CmdAdd(GameObjects.PICTURE) // 38 picture
                && gameState.IsAt(GameRooms.SITTINGROOM3) // 3 sitting room
                && gameItems.houseItems[GameObjects.PICTURE].IsAt(GameRooms.MIDAIR30)) // 30 mid-air (means picture is in mid-air)
            {
                gameState.AddMessage("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // look 42=note and (13=master bedroom OR 22=bathroom)
            if (userInput.CMD2 == CmdAdd(GameObjects.NOTE) // 42 note
                && (gameState.IsAt(GameRooms.MASTERBEDROOM13) // 13 master bedroom
                || gameState.IsAt(GameRooms.BATHROOM22))) // 22 bathroom
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

            gameState.AddMessage("There's nothing special about the " + Texts.VOCABS[userInput.CMD2]);
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
            gameState.AddMessage("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }
        // ok
        public void ActionUnlock()
        {
            if (!gameState.IsAt(GameRooms.HALL5)
                || !gameState.IsAt(GameRooms.LIBRARY8)
                || !gameState.IsAt(GameRooms.HALL20)
                || !gameState.IsAt(GameRooms.HALL17))
            {
                gameState.AddMessage("There is no door here!");
                return;
            }
            // do we have a key?
            if (!gameItems.houseItems[GameObjects.KEY].IsCarrying()) // 7 key
            // 13 = master bedroom ? key ?
            {
                gameState.AddMessage("You don't have a key!");
                return;
            }
            else // yes
            {
                // ckeck where we are
                if (gameState.IsAt(GameRooms.HALL5)) // 5 Hallway?
                {
                    gameState.AddMessage("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (gameState.IsAt(GameRooms.HALL17) // 17 hall
                    && gameItems.houseItems[GameObjects.KEY].IsCarrying()) // 7 key
                {
                    gameState.AddMessage("You unlock the door. beware!");
                    LocationExit[17, 4] = 20; // reveal north
                    return;
                }
            }
            gameState.AddMessage("Nothing to unlock!");
        }
        // ok
        public void ActionOpen()
        {
            // open door == unlock door
            if (userInput.CMD2 == CmdAdd(GameObjects.DOOR))
            {
                ActionUnlock();
                return;
            }
            // open door in dangerous hall 20
            if (userInput.CMD2 == CmdAdd(GameObjects.DOOR)// 57 door
                && gameState.IsAt(GameRooms.HALL20)) // 20 hall
            {
                gameState.AddMessage("Please specify LEFT, CENTER or RIGHT");
                return;
            }
            gameState.AddMessage("Open what?");
        }
        // ok
        public void ActionOpen3Door()
        {
            if (!gameState.IsAt(GameRooms.HALL20)) // 20 dangerous hall
            {
                gameState.AddMessage("You can't do that here");
                return;
            }

            if (userInput.CMD3 != CmdAdd(GameObjects.DOOR)) // 57 door
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
                gameState.AddMessage("Opening the door reveals a dumbwaiter");
                LocationExit[gameState.PlayerAt, 4] = 23;
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
        // ok
        public void ActionEat()
        {
            if (!gameItems.houseItems[GameObjects.GAINESBURGER].IsCarrying()) // 10 GAINESBURGER
            {
                gameState.AddMessage("You don't have it!");
                return;
            }
            else if (userInput.CMD2 != GameObjects.GAINESBURGER)
            {
                gameState.AddMessage("You can't eat that!");
                return;
            }

            gameState.AddMessage("There was a diamond hidden inside the gainesburger");

            gameItems.houseItems[GameObjects.GAINESBURGER].HideItem(); // 10 gainesburger is now hidden
            gameItems.houseItems[GameObjects.DIAMOND].LeaveItem(gameState.PlayerAt); // 17 diamond is now in the player's location
        }
        // ok
        public void ActionSpin()
        {
            if (!gameItems.houseItems[GameObjects.SPINNINGTOP].IsCarrying()) // 8 spinningtop
            {
                gameState.AddMessage("Spin what?");
                return;
            }
            if (gameState.IsAt(GameRooms.CHILDSROOM18)) // 18 child's room
            {
                gameState.AddMessage("There is a flash of light and a cracking sound! An opening appears in the east wall");
                LocationExit[18, 3] = GameRooms.SECRETROOM19; // 19 reveal east (dark room)
                return;
            }

            gameState.AddMessage("Whee!");
        }
        // ok
        public void ActionMoveObj()
        {
            if (userInput.CMD2 == CmdAdd(GameObjects.FRIDGE)) // 54 fridge
            {
                gameState.AddMessage("It's too heavy for you to move alone (without any help)");
                return;
            }
            if (userInput.CMD2 == CmdAdd(GameObjects.COUCH)) // 55 couch
            {
                gameState.AddMessage("Your back is acting up, you will need some support");
                return;
            }
            if (userInput.CMD2 == CmdAdd(GameObjects.CLOTHES)) // 56 clothes
            {
                gameState.AddMessage("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            gameState.AddMessage("You can't do that");
        }
        // ok
        public void ActionJump()
        {
            // check if we are at the balcony
            if (!gameState.IsAt(GameRooms.BALCONY12)) // 12 Balcony
            {
                gameState.AddMessage("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                return;
            }

            // check if BUNGEE cord is tied to the railing
            if (gameItems.houseItems[GameObjects.BUNGEE].IsTied()) // 6 bungee is tied to the railing
            {
                gameState.AddMessage("You forgot your parachute. Or maybe something else...");
                return;
            }

            gameState.AddMessage("You bungee off the balcony...");

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
                return;
            }
            if (gameState.IsAt(GameRooms.BALCONY12) // 12 attic
                && dir == GameVerbs.UP) // 5 up to attic
            {
                gameState.AddMessage("You're afraid of the dark");
                return;
            }
            if (gameState.IsAt(GameRooms.HALL17) // 17 hall
                && dir == GameVerbs.NORTH) // 1 north
            {
                gameState.AddMessage("You never did like that dog");
                return;
            }
            if (gameState.IsAt(GameRooms.DUMBWAITER23) // 23 dumbwaiter
                && LocationExit[23, 6] <= 0) // D: is blocked, unlock with oilcan
            {
                gameState.AddMessage("The dumbwaiter mechanism is corroded and won't move");
                return;
            }

            gameState.AddMessage("You can't go that way");
        }
        // ok
        public void ActionMoveFridgeWithJack()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != CmdAdd(GameObjects.FRIDGE) // 54 fridge
                || userInput.CMD3 != CmdAdd(GameObjects.JACK)) // 37 jack
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("You jack up the fridge and find a fuse under it");

            gameItems.houseItems[GameObjects.FUSE].LeaveItem(gameState.PlayerAt); // reveal fuse
        }
        // ok
        public void ActionMoveCouchWithBrace()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != CmdAdd(GameObjects.COUCH) // 55 couch
                || userInput.CMD3 != CmdAdd(GameObjects.BRACE)) // 46 brace
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("You move the couch and find a teddybear behind it");

            gameItems.houseItems[GameObjects.TEDDYBEAR].LeaveItem(gameState.PlayerAt); // reveal teddybear
        }
        // ok
        public void ActionMoveClothesWithGloves()
        {
            if (userInput.CMD1 != GameVerbs.MOVE // 26 move
                || userInput.CMD2 != CmdAdd(GameObjects.CLOTHES) // 56 clothes
                || userInput.CMD3 != CmdAdd(GameObjects.GLOVES)) // 44 gloves
            {
                gameState.AddMessage("You can't do that");
                return;
            }

            gameState.AddMessage("Moving the clothes reveals a laundry chute to the basement");
            LocationExit[gameState.PlayerAt, 6] = GameRooms.LAUNDRY27; // 27 laundry chute
        }
        // ok
        public void ActionTieBungeeToRailing()
        {
            // is carrying a bungee cord?
            if (!gameItems.houseItems[GameObjects.BUNGEE].IsCarrying()) // 6 bungee
            {
                gameState.AddMessage("You don't have a bungee cord!");
                return;
            }
            // is in the Balcony
            if (!gameState.IsAt(GameRooms.BALCONY12)) // 12 balcony
            {
                gameState.AddMessage("There is nothing here to tie to");
                return;
            }
            // object is not BUNGEE cord
            if (userInput.CMD2 != CmdAdd(GameObjects.BUNGEE)) // 39 bungee
            {
                gameState.AddMessage("You can't tie that");
                return;
            }
            // rainling
            if (userInput.CMD3 != CmdAdd(GameObjects.RAILING)) // 58 railing
            {
                gameState.AddMessage("Tie to what?");
                return;
            }

            gameState.AddMessage("Bungee cord tied to Railing!");

            gameItems.houseItems[GameObjects.BUNGEE].TieItem(); // 6 bungee is tied to the railing
        }
        // ok
        public void ActionOilDumbwaiterWithOilcan()
        {
            // is in the dumbwaiter?
            if (!gameState.IsAt(GameRooms.DUMBWAITER23)) // 23 dumbwaiter
            {
                gameState.AddMessage("You can't do that here");
                return;
            }
            if (!gameItems.houseItems[GameObjects.OILCAN].IsCarrying()) // 15 oilcan
            {
                gameState.AddMessage("You don't have any oil");
                return;
            }
            if (userInput.CMD2 != CmdAdd(GameObjects.DUMBWAITER)) // 59 dumbwaiter
            {
                gameState.AddMessage("Oil what?");
                return;
            }

            gameState.AddMessage("The dumbwaiter mechanism now runs smoothly");
            LocationExit[23, 6] = GameRooms.DUMBWAITER24; // reveal down to 24
        }
        // ok
        public void ActionPutFuseInFusebox()
        {
            // check if we have the fuse
            if (!gameItems.houseItems[GameObjects.FUSE].IsCarrying())
            {
                gameState.AddMessage("You don't have it!");
                return;
            }

            if (userInput.CMD3 != CmdAdd(GameObjects.FUSEBOX)) // 60 fusebox
            {
                gameState.AddMessage("You can't put it there");
                return;
            }

            if (userInput.CMD1 != GameVerbs.PUT // 30 put
                || userInput.CMD2 != CmdAdd(GameObjects.FUSE) // 36 fuse
                || userInput.CMD3 != CmdAdd(GameObjects.FUSEBOX) // 60 fusebox
                || !gameState.IsAt(GameRooms.MIDAIR30))// 30 mid-air
            {
                gameState.AddMessage("You can't do that here");
                return;
            }

            gameState.AddMessage("You put the fuse in the box. Power is restored in the Attic!");

            // mark fuse as hidden
            gameItems.houseItems[GameObjects.FUSE].HideItem();
            // stairs to attic is hidden until fuse is inserted
            LocationExit[12, 5] = GameRooms.ATTIC25; // 25 attic
        }
        // ok
        public void ActionReadNoteInMirror()
        {
            // check if we have the note
            if (!gameItems.houseItems[GameObjects.NOTE].IsCarrying())
            {
                gameState.AddMessage("Which note?");
                return;
            }
            // we can only read the note in the mirror in 2 places
            if (userInput.CMD2 == CmdAdd(GameObjects.NOTE) // 42 note
                && (gameState.IsAt(GameRooms.MASTERBEDROOM13) // 13 master bedroom
                || gameState.IsAt(GameRooms.BATHROOM22))) // 22 bathroom
            {
                ActionSafeDoor();
                return;
            }
            gameState.AddMessage("I don't see a mirror here");
        }

    }
}