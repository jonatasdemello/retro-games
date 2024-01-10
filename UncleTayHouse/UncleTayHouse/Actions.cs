namespace UncleTayHouse
{
    internal class Actions
    {
    }
    public partial class Game
    {
        public int RNG(int max)
        {
            Random rnd = new Random();
            int value = rnd.Next(1, max+1);
            return value;
        }

        public void print(string text) => Console.WriteLine(text);
        public void print(string text1, string text2) => Console.WriteLine($"{text1} {text2}");
        public void PrintResponse(string text)
        {
            string sep = new string('-', 80);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.WriteLine(sep);
            Console.WriteLine(text);
            //Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            //Console.ResetColor();
        }
        public void PrintDgb(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("    ----------------------------- " + text + " ----------------------------- ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            //Console.ResetColor();
        }
        public void PrintDgb1(string text)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("----------------------------- " + text + " ----------------------------- ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            //Console.ResetColor();
        }

        public int GetObject(int x)
        {
            return x - OBJECTOFFSET;
        }
        public string GetString(int x)
        {
            return VOCABS[x + OBJECTOFFSET];
        }

        public void ActionInventory()
        {
            print(" [Inventory] You are carrying:");
            bool empty = true;
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == 0)
                {
                    print("   - ", VOCABS[i + OBJECTOFFSET]);
                    empty = false;
                }
            }
            if (empty)
            {
                print(" - nothing!");
            }
        }
        public void ActionIntro()
        {
            print(" ");
            print("Uncle Tays' House Adventure");
            print("--------------------");
            print("Find treasures and valuables in your mad uncle tays' house");
            print("Type simple commands to move around:");
            print("   NORTH, SOUTH, EAST, WEST, UP, DOWN");
            print("   or just: S, E, W, U, D.");
            print("Type two word commands (verb + action) to interact with objects:");
            print("TAKE BOOK, DROP BOOK, INVENTORY, LOOK, READ, MOVE");
            print("Some commands are complex:");
            print(" 'MOVE THE HUBCAP WITH THE SPANNER'");
            print(" ");
            print("Possible commands: ");
            print("    NORTH, SOUTH, EAST, WEST, UP, DOWN, N, S, E, W, U, D,");
            print("    I, INVENTORY, SCORE, JUMP, HELP,");
            print("    TAKE, DROP, LOOK, READ, EXAMINE, UNLOCK, EAT, SPIN,");
            print("    MOVE, OPEN, TIE, OIL, PUT, LEFT, CENTER, RIGHT");
            print(" ");
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
            PrintResponse("Your score is " + SCORE + " out of a possible 100");

            if (SCORE == 100)
            {
                PrintResponse("You have won the game!");
            }
        }
        public void ActionExit()
        {
            PrintResponse("Thank you for playing, bye!");
            Environment.Exit(0);
        }

        public void ActionLocation()
        {
            //PrintDgb("location & description");
            //print("  L : ", LOCAL.ToString());
            //print("  L : { " + LOCAL.ToString() +" } "+ LocationName_RNAMES[LOCAL]);
            //print("  D : " + LocationDescription_RDESCS[LOCAL]);

            print("  { " + LOCAL.ToString() + " } " + LocationName_RNAMES[LOCAL]);
            print("  " + LocationDescription_RDESCS[LOCAL]);

            //print("    LOCAL : " + LOCAL.ToString() +
            //"    L   : " + LocationName_RNAMES[LOCAL] +
            //"    D   : " + LocationDescription_RDESCS[LOCAL] );
        }
        public void ActionDirections()
        {
            PrintDgb("directions");
            // print("    You can go:");
            for (int i = 1; i <= 6; i++)
            {
                int exit = LocationExit[LOCAL, i];
                if (exit > 0)
                {
                    print("    " + VOCABS[i] +"\t : ", LocationName_RNAMES[exit]);
                }
            }
        }
        public void ActionExtendedDescriptions()
        {
            // PrintDgb("extended descriptions");

            for (int i = 1; i < ExtendedDescription.Length; i++)
            {
                int L1 = EXLOC[i, 1];
                int L2 = EXLOC[i, 2];
                if (LOCAL == L1 && LocationExit[L1, L2] <= 0)
                {
                    PrintResponse(ExtendedDescription[i]);
                }
            }
            if (LOCAL == 17 && LocationExit[17, 1] > 0)
            {
                PrintResponse("Your uncle's doberman is snoring peacefully");
            }
            if (LOCAL == 3 && ILOC[6] == -12)
            {
                PrintResponse("A bungee cord dangles from the railing above");
            }
            if (LOCAL == 12 && ILOC[6] == -12)
            {
                PrintResponse("A bungee cord dangles from the railing");
            }
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == LOCAL)
                {
                    PrintResponse("There is a " + VOCABS[i + OBJECTOFFSET] + " here");
                }
            }
            if (LOCAL == 2 && ILOC[3] == -1)
            {
                PrintResponse("Something is barely visible under the fridge");
            }
            if (LOCAL == 3 && ILOC[5] == 30)
            {
                PrintResponse("There is a picture high up on the wall");
            }
        }

        public void ActionTake()
        {
            // below 34 (verbs) above 33 (objects)
            int obj = CMD2 - OBJECTOFFSET;

            if (CMD2 < 34) // verbs
            {
                PrintResponse("Take what?");
                return;
            }

            if (ILOC[obj] == 0)
            {
                PrintResponse("You already have " + VOCABS[CMD2]);
                return;
            }

            // take picture from the wall
            // picture, sitting hall
            if (CMD2 == 32 && ILOC[obj] == 30 && LOCAL == 3)
            {
                PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // big objects that can't be taken
            if (CMD2 >= 54) // fridge, couch, door, railing...
            {
                PrintResponse("It's too heavy, you can't take that");
                return;
            }

            // bottom of stairs, boxspring
            if (LOCAL == 29 && CMD2 == 45)
            {
                PrintResponse("It is better to leave it there");
                return;
            }

            // mid-air, picture
            if (LOCAL == 30 && CMD2 == 38)
            {
                PrintResponse("Taking the picture reveals a fusebox");

                ILOC[5] = 0; // picture
                ILOC[obj] = 0;
                ILOC[27] = 30; // Fusebox
                return;
            }

            if (ILOC[obj] != LOCAL)
            {
                PrintResponse("There is no " + VOCABS[CMD2] + " here");
                return;
            }

            // if player is carrying object, ILOC[obj] == 0
            ILOC[obj] = 0;
            PrintResponse(VOCABS[CMD2] + ": taken");
        }
        public void ActionDrop()
        {
            int obj = CMD2 - OBJECTOFFSET;

            if (ILOC[obj] != 0)
            {
                PrintResponse("You aren't carrying " + VOCABS[CMD2]);
                return;
            }
            // gainesburger, hallway
            if (CMD2 == 43 && LOCAL == 17 && LocationExit[17, 1] <= 0)
            {
                PrintResponse("The dog looks disgusted. maybe you should eat it");
                return;
            }
            // teddybear, hallway
            if (CMD2 == 35 && LOCAL == 17 && LocationExit[17, 1] <= 0)
            {
                PrintResponse("The dog chews his favorite toy and is soon asleep");
                ILOC[obj] = -999;
                LocationExit[17, 1] = 18;
            }
            // boxpring, bottom of stairs
            if (CMD2 == 45 && LOCAL == 29 && LocationExit[29, 5] <= 0)
            {
                PrintResponse("The boxspring covers the gap in the stairs");
                ILOC[obj] = -999;
                LocationExit[29, 5] = 2;
                LocationExit[2, 6] = 29;
            }

            // if player is carrying object, ILOC[obj] == current local
            ILOC[obj] = LOCAL;
            PrintResponse(VOCABS[CMD2] + ": dropped");
        }
        public void ActionLook()
        {
            int obj = CMD2 - OBJECTOFFSET;

            // look at the picture on the wall
            if (ILOC[5] == 30 && LOCAL == 3 && CMD2 == 38)
            {
                PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != 0 && ILOC[obj] != LOCAL)
            {
                PrintResponse("There is no " + VOCABS[CMD2] + " here");
                return;
            }

            // 42=note and (13=master bedroom OR 22=bathroom)
            if (CMD2 == 42 && (LOCAL == 13 || LOCAL == 22))
            {
                ActionSafeDoor();
                return;
            }

            // Print extended obj description
            if (IDESCS[obj] != "")
            {
                PrintResponse(IDESCS[obj]);
                return;
            }
            PrintResponse("There's nothing special about the " + VOCABS[CMD2]);
        }
        public void ActionSafeDoor()
        {
            // decide which door is safe
            if (SAFEDoor == 0)
            {
                SAFEDoor = RNG(3);
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
            PrintResponse("Experiments on " + N1S + " and " + N2S + " doors proceeding well; file for patent");
        }
        public void ActionUnlock()
        {
            if (LOCAL != 20 && LOCAL != 17)
            {
                PrintResponse("There is no door here!");
                return;
            }
            // do we have a key?
            if (ILOC[7] != 0) // 13 = master bedroom ? key ?
            {
                PrintResponse("You don't have a key!");
                return;
            }
            else // yes
            {
                // ckeck locale
                if (LOCAL == 5) // Hallway?
                {
                    PrintResponse("The key doesn't fit the lock");
                    return;
                }
                // only unlock if is in the Hallway and have a key
                if (LOCAL == 17 && ILOC[7] == 0) // Hallway?
                {
                    PrintResponse("You unlock the door. beware!");
                    LocationExit[17, 4] = 20;
                    return;
                }
            }
            PrintResponse("Nothing to unlock!");
        }
        public void ActionOpen()
        {
            // 2 words only

            // dangerous hall: open door
            if (LOCAL == 20 && CMD2 == 57)
            {
                PrintResponse("Please specify LEFT, CENTER or RIGHT");
                return;
            }
            else
            {
                PrintResponse("Open what?");
            }
        }
        public void ActionOpen3Door()
        {
            if (LOCAL != 20) // dangerous hall
            {
                PrintResponse("You can't do that here");
                return;
            }

            if (CMD3 != 57) // door
            {
                PrintResponse("Open what?");
                return;
            }

            // open[direction not mentioned in note] door
            int DOORDIR = CMD2 - 30;
            if (DOORDIR < 1 || DOORDIR > 3)
            {
                PrintResponse("Which door?");
                return;
            }

            if (DOORDIR == SAFEDoor)
            {
                PrintResponse("Opening the door reveals a dumbwaiter");
                LocationExit[LOCAL, 4] = 23;
                return;
            }

            // Trap: if you haven't read the note before
            int rnd = RNG(100);
            if (rnd > 50)
            {
                PrintResponse("BAM! A shot rings out! it was well-aimed too.");
                // you die
                return;
            }

            PrintResponse("An ironing board slams onto your head");
            // you die
        }
        public void ActionEat()
        {
            if (ILOC[10] != 0) //  GAINESBURGER
            {
                PrintResponse("You don't have it!");
                return;
            }
            else if (CMD2 != 42)
            {
                PrintResponse("You can't eat that!");
                return;
            }

            PrintResponse("There was a diamond hidden inside the gainesburger");

            ILOC[10] = -2; // GAINESBURGER
            ILOC[17] = 0; //  DIAMOND
        }
        public void ActionSpin()
        {
            if (ILOC[8] != 0) // Top
            {
                PrintResponse("Spin what?");
                return;
            }
            if (LOCAL == 18) // child's room
            {
                PrintResponse("There is a flash of light and a cracking sound! An opening appears in the east wall");
                LocationExit[18, 3] = 19;
            }
            PrintResponse("Whee!");
        }
        public void ActionMoveObj()
        {
            if (CMD2 == 54) // fridge
            {
                PrintResponse("It's too heavy for you to move alone (without any help)");
                return;
            }
            if (CMD2 == 55) // couch
            {
                PrintResponse("Your back is acting up, you will need some support");
                return;
            }
            if (CMD2 == 56) // clothes
            {
                PrintResponse("That seems pointless and unsanitary, they are too dirty!");
                return;
            }

            PrintResponse("You can't do that");
        }
        public void ActionJump()
        {
            // location 12 = BALCONY
            if (LOCAL != 12)
            {
                PrintResponse("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                return;
            }

            // BUNGEE cord is tied to the railing
            if (ILOC[6] != -12)
            {
                PrintResponse("You forgot your parachute. Or maybe something else...");
                return;
            }

            PrintResponse("You bungee off the balcony...");

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
                PrintResponse("You're afraid of the dark");
                return;
            }
            else if (LOCAL == 17 && dir == 1)
            {
                PrintResponse("You never did like that dog");
                return;
            }
            else if (LOCAL == 23 && LocationExit[23, 6] <= 0)
            {
                PrintResponse("The dumbwaiter mechanism is corroded and won't move");
                return;
            }
            else
            {
                PrintResponse("You can't go that way");
            }
        }
        public void ActionMoveFridgeWithJack()
        {
            if (CMD1 != 26 || CMD2 != 54 || CMD3 != 37) // move fridge jack
            {
                PrintResponse("You can't do that");
                return;
            }

            PrintResponse("You jack up the fridge and find a fuse under it");
            ILOC[3] = LOCAL;
        }
        public void ActionMoveCouchWithBrace()
        {
            if (CMD1!= 26 || CMD2 != 55 || CMD3 != 46) // move couch brace
            {
                PrintResponse("You can't do that");
                return;
            }

            PrintResponse("You move the couch and find a teddybear behind it");
            ILOC[2] = LOCAL;
        }
        public void ActionMoveClothesWithGloves()
        {
            if (CMD1 != 26 || CMD2 != 56 || CMD3 != 44) // move clothes gloves
            {
                PrintResponse("You can't do that");
                return;
            }

            PrintResponse("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT");
            LocationExit[LOCAL, 6] = 27;
        }
        public void ActionTieBungeeToRailing()
        {
            // carrying bungee?
            if (ILOC[6] != 0)
            {
                PrintResponse("You don't have a bungee cord!");
                return;
            }
            // if not in the Balcony
            if (LOCAL != 12)
            {
                PrintResponse("There is nothing here to tie to");
                return;
            }
            // object is not BUNGEE cord
            if (CMD2 != 39)
            {
                PrintResponse("You can't tie that");
                return;
            }
            // rainling
            if (CMD3 != 58)
            {
                PrintResponse("Tie to what?");
                return;
            }

            PrintResponse("Bungee cord tied to Railing!");

            // BUNGEE cord is tied to the railing
            ILOC[6] = -12;
        }
        public void ActionOilDumbwaiterWithOilcan()
        {
            // not in the dumbwaiter
            if (LOCAL != 23)
            {
                PrintResponse("You can't do that here");
                return;
            }
            // 15 OILCAN
            if (ILOC[15] != 0)
            {
                PrintResponse("You don't have any oil");
                return;
            }
            // dumbwaiter
            if (CMD2 != 59)
            {
                PrintResponse("Oil what?");
                return;
            }

            PrintResponse("The dumbwaiter mechanism now runs smoothly");
            LocationExit[23, 6] = 24;
        }
        public void ActionPutFuseInFusebox()
        {
            if (LOCAL != 30 || CMD1 != 30 || CMD2 != 36)
            {
                PrintResponse("You can't do that");
                return;
            }
            if (CMD3 != 60) // fusebox
            {
                PrintResponse("You can't put it there");
                return;
            }
            // do we have the fuse?
            if (ILOC[3] != 0)
            {
                PrintResponse("You don't have it!");
            }

            PrintResponse("You put the fuse in the box. Power is restored in the Attic!");
            // mark fuse as used
            ILOC[3] = -999;

            // STAIRS TO ATTIC is hidden until fuse is inserted
            LocationExit[12, 5] = 25;
        }
        public void ActionReadNoteInMirror()
        {
            // 42=note and (13=master bedroom OR 22=bathroom)
            if (CMD2 == 42 && (LOCAL == 13 || LOCAL == 22))
            {
                ActionSafeDoor();
                return;
            }
            PrintResponse("I don't see a mirror here");
        }
    }
}
