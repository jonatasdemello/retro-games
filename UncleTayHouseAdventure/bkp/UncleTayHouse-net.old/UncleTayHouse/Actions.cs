namespace UncleTayHouse
{
    public partial class Game
    {

        public int GetObject(int x)
        {
            return x - Constants.OBJECTOFFSET;
        }
        public string GetString(int x)
        {
            return Constants.VOCABS[x + Constants.OBJECTOFFSET];
        }

        public void ActionInventory()
        {
            Screen.print(" [Inventory] You are carrying:");
            bool empty = true;
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == 0)
                {
                    Screen.print("   - ", Constants.VOCABS[i + Constants.OBJECTOFFSET]);
                    empty = false;
                }
            }
            if (empty)
            {
                Screen.print(" - nothing!");
            }
        }

        public void ShowIntro()
        {
            Screen.print(" ");
            Screen.print("***********************************");
            Screen.print("*** UNCLE TAY'S HOUSE ADVENTURE ***");
            Screen.print("***********************************");
            Screen.print(" ");
            Screen.print("Find treasures and valuables in your mad uncle tays' house");
            Screen.print("Type simple commands to move around:");
            Screen.print("   NORTH, SOUTH, EAST, WEST, UP, DOWN");
            Screen.print("   or just: S, E, W, U, D.");
            Screen.print("Type two word commands (verb + action) to interact with objects:");
            Screen.print("TAKE BOOK, DROP BOOK, INVENTORY, LOOK, READ, MOVE");
            Screen.print("Some commands are complex:");
            Screen.print(" 'MOVE THE HUBCAP WITH THE SPANNER'");
            Screen.print(" ");
            Screen.print("Possible commands: ");
            Screen.print("    NORTH, SOUTH, EAST, WEST, UP, DOWN, N, S, E, W, U, D,");
            Screen.print("    I, INVENTORY, SCORE, JUMP, HELP,");
            Screen.print("    TAKE, DROP, LOOK, READ, EXAMINE, UNLOCK, EAT, SPIN,");
            Screen.print("    MOVE, OPEN, TIE, OIL, PUT, LEFT, CENTER, RIGHT");
            Screen.print(" ");
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
            //Screen.PrintDgb("location & description");
            //Screen.print("  L : ", LOCAL.ToString());
            //Screen.print("  L : { " + LOCAL.ToString() +" } "+ LocationName_RNAMES[LOCAL]);
            //Screen.print("  D : " + LocationDescription_RDESCS[LOCAL]);

            Screen.print("  { " + LOCAL.ToString() + " } " + Constants.LocationName_RNAMES[LOCAL]);
            Screen.print("    " + Constants.LocationDescription_RDESCS[LOCAL]);

            //Screen.print("    LOCAL : " + LOCAL.ToString() +
            //"    L   : " + LocationName_RNAMES[LOCAL] +
            //"    D   : " + LocationDescription_RDESCS[LOCAL] );
        }
        public void ActionDirections()
        {
            Screen.PrintDgb("directions");
            // Screen.print("    You can go:");
            for (int i = 1; i <= 6; i++)
            {
                int exit = LocationExit[LOCAL, i];
                if (exit > 0)
                {
                    Screen.print("    " + Constants.VOCABS[i] +"\t : ", Constants.LocationName_RNAMES[exit]);
                }
            }
        }
        public void ActionExtendedDescriptions()
        {
            // Screen.PrintDgb("extended descriptions");

            for (int i = 1; i < Constants.ExtendedDescription.Length; i++)
            {
                int L1 = Constants.EXLOC[i, 1];
                int L2 = Constants.EXLOC[i, 2];
                if (LOCAL == L1 && LocationExit[L1, L2] <= 0)
                {
                    Screen.PrintResponse(Constants.ExtendedDescription[i]);
                }
            }
            if (LOCAL == 17 && LocationExit[17, 1] > 0)
            {
                Screen.PrintResponse("Your uncle's doberman is snoring peacefully");
            }
            if (LOCAL == 3 && ILOC[6] == -12)
            {
                Screen.PrintResponse("A bungee cord dangles from the railing above");
            }
            if (LOCAL == 12 && ILOC[6] == -12)
            {
                Screen.PrintResponse("A bungee cord dangles from the railing");
            }
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == LOCAL)
                {
                    Screen.PrintResponse("There is a " + Constants.VOCABS[i + Constants.OBJECTOFFSET] + " here");
                }
            }
            if (LOCAL == 2 && ILOC[3] == -1)
            {
                Screen.PrintResponse("Something is barely visible under the fridge");
            }
            if (LOCAL == 3 && ILOC[5] == 30)
            {
                Screen.PrintResponse("There is a picture high up on the wall");
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
            if (user.CMD2 == 32 && ILOC[obj] == 30 && LOCAL == 3)
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
            if (LOCAL == 29 && user.CMD2 == 45)
            {
                Screen.PrintResponse("It is better to leave it there");
                return;
            }

            // mid-air, picture
            if (LOCAL == 30 && user.CMD2 == 38)
            {
                Screen.PrintResponse("Taking the picture reveals a fusebox");

                ILOC[5] = 0; // picture
                ILOC[obj] = 0;
                ILOC[27] = 30; // Fusebox
                return;
            }

            if (ILOC[obj] != LOCAL)
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
            if (user.CMD2 == 43 && LOCAL == 17 && LocationExit[17, 1] <= 0)
            {
                Screen.PrintResponse("The dog looks disgusted. maybe you should eat it");
                return;
            }
            // teddybear, hallway
            if (user.CMD2 == 35 && LOCAL == 17 && LocationExit[17, 1] <= 0)
            {
                Screen.PrintResponse("The dog chews his favorite toy and is soon asleep");
                ILOC[obj] = -999;
                LocationExit[17, 1] = 18;
                return;
            }
            // boxpring, bottom of stairs
            if (user.CMD2 == 45 && LOCAL == 29 && LocationExit[29, 5] <= 0)
            {
                Screen.PrintResponse("The boxspring covers the gap in the stairs");
                ILOC[obj] = -999;
                LocationExit[29, 5] = 2;
                LocationExit[2, 6] = 29;
                return;
            }

            // if player is carrying object, ILOC[obj] == current local
            ILOC[obj] = LOCAL;
            Screen.PrintResponse(Constants.VOCABS[user.CMD2] + ": dropped");
        }
        public void ActionLook()
        {
            int obj = user.CMD2 - Constants.OBJECTOFFSET;

            // look at the picture on the wall
            if (ILOC[5] == 30 && LOCAL == 3 && user.CMD2 == 38)
            {
                Screen.PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != 0 && ILOC[obj] != LOCAL)
            {
                Screen.PrintResponse("There is no " + Constants.VOCABS[user.CMD2] + " here");
                return;
            }

            // 42=note and (13=master bedroom OR 22=bathroom)
            if (user.CMD2 == 42 && (LOCAL == 13 || LOCAL == 22))
            {
                ActionSafeDoor();
                return;
            }

            // Print extended obj description
            if (Constants.IDESCS[obj] != "")
            {
                Screen.PrintResponse(Constants.IDESCS[obj]);
                return;
            }
            Screen.PrintResponse("There's nothing special about the " + Constants.VOCABS[user.CMD2]);
        }
        public void ActionSafeDoor()
        {
            // decide which door is safe
            if (SAFEDoor == 0)
            {
                SAFEDoor = Utils.RNG(3);
            }

            string N1S = "LEFT";
            string N2S = "RIGHT";

            if (SAFEDoor == 1)
            {
                N1S = "CENTER";
            }
            if (SAFEDoor == 3)
            {
                N2S = "CENTER";
            }
            Screen.PrintResponse("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }
        public void ActionUnlock()
        {
            if (LOCAL != 20 && LOCAL != 17)
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
                // ckeck locale
                if (LOCAL == 5) // Hallway?
                {
                    Screen.PrintResponse("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (LOCAL == 17 && ILOC[7] == 0) // Hallway?
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
            if (LOCAL == 20 && user.CMD2 == 57)
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
            if (LOCAL != 20) // dangerous hall
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

            if (DOORDIR == SAFEDoor)
            {
                Screen.PrintResponse("Opening the door reveals a dumbwaiter");
                LocationExit[LOCAL, 4] = 23;
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
            if (LOCAL == 18) // child's room
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
            if (LOCAL != 12)
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
            LOCAL = 30;
            TURN1 = 0; // reset for multiple jumps
        }
        public void ActionPlayerMove(int dir)
        {
            // convert N,S,E,W,U,D to long form
            if (dir > 6)
            {
                dir = dir - 6;
            }
            if (LocationExit[LOCAL, dir] > 0)
            {
                LOCAL = LocationExit[LOCAL, dir];
            }
            else if (LOCAL == 12 && dir == 5) // up to attic
            {
                Screen.PrintResponse("You're afraid of the dark");
                return;
            }
            else if (LOCAL == 17 && dir == 1)
            {
                Screen.PrintResponse("You never did like that dog");
                return;
            }
            else if (LOCAL == 23 && LocationExit[23, 6] <= 0)
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
            ILOC[3] = LOCAL;
        }
        public void ActionMoveCouchWithBrace()
        {
            if (user.CMD1!= 26 || user.CMD2 != 55 || user.CMD3 != 46) // move couch brace
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("You move the couch and find a teddybear behind it");
            ILOC[2] = LOCAL;
        }
        public void ActionMoveClothesWithGloves()
        {
            if (user.CMD1 != 26 || user.CMD2 != 56 || user.CMD3 != 44) // move clothes gloves
            {
                Screen.PrintResponse("You can't do that");
                return;
            }

            Screen.PrintResponse("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT");
            LocationExit[LOCAL, 6] = 27;
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
            if (LOCAL != 12)
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
            if (LOCAL != 23)
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
            if (LOCAL != 30 || user.CMD1 != 30 || user.CMD2 != 36)
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
            if (user.CMD2 == 42 && (LOCAL == 13 || LOCAL == 22))
            {
                ActionSafeDoor();
                return;
            }
            Screen.PrintResponse("I don't see a mirror here");
        }
    }
}
