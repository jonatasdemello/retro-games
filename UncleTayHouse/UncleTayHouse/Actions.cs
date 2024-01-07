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
            int value = rnd.Next(0, max);
            return value;
        }
        public void print(string text) => Console.WriteLine(text);
        public void print(string text1, string text2) => Console.WriteLine($"{text1} {text2}");

        public void PrintResponse(string text)
        {
            string sep = new string('-', 80);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(sep);
            Console.WriteLine(text);
            Console.WriteLine(sep);
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


        public void ActionInventory()
        {
            print("[Inventory] You are carrying:");
            bool empty = true;
            for (int i = 1; i < ILOC.Length; i++)
            {
                if (ILOC[i] == 0)
                {
                    print("  - ", VOCABS[i + ITEMOFF]);
                    empty = false;
                }
            }
            if (empty)
            {
                print("  - nothing!");
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
        public void ActionHuh()
        {
            PrintResponse("HUH?");
        }

        public void ActionLocation()
        {
            PrintDgb("location & description");
            print("    LCL : ", LCL.ToString());
            print("    L   : ", LocationName_RNAMES[LCL]);
            print("    D   : ", LocationDescription_RDESCS[LCL]);
        }
        public void ActionDirections()
        {
            PrintDgb("directions");
            print("    You can go:");
            for (int i = 1; i <= 6; i++)
            {
                int exit = LocationExit[LCL, i];
                if (exit > 0)
                {
                    print("    " + VOCABS[i] +"\t : ", LocationName_RNAMES[exit]);
                }
            }
        }
        public void ActionExtendedDescriptions()
        {
            PrintDgb("extended descriptions");

            for (int i = 1; i < ExtendedDescription.Length; i++)
            {
                int L1 = EXLOC[i, 1];
                int L2 = EXLOC[i, 2];
                if (LCL == L1 && LocationExit[L1, L2] <= 0)
                {
                    print(ExtendedDescription[i]);
                }
            }
            if (LCL == 17 && LocationExit[17, 1] > 0)
            {
                print("YOUR UNCLE'S DOBERMAN IS SNORING PEACEFULLY");
            }
            if (LCL == 3 && ILOC[6] == -12)
            {
                print("A BUNGEE CORD DANGLES FROM THE RAILING ABOVE");
            }
            if (LCL == 12 && ILOC[6] == -12)
            {
                print("A BUNGEE CORD DANGLES FROM THE RAILING");
            }
            for (int i = 1; i <= LASTITEM; i++)
            {
                if (ILOC[i] == LCL)
                {
                    print("THERE IS A " + VOCABS[i + ITEMOFF] + " HERE");
                }
            }
            if (LCL == 2 && ILOC[3] == -1)
            {
                print("SOMETHING IS BARELY VISIBLE UNDER THE FRIDGE");
            }
            if (LCL == 3 && ILOC[5] == 30)
            {
                print("THERE IS A PICTURE HIGH UP ON THE WALL");
            }
        }

        public void ActionTake(int obj)
        {
            if (obj < 0)
            {
                PrintResponse("Do what?");
                return;
            }
            // ARG is the object
            ARG = InputWordNum_INPTK[2] - ITEMOFF;

            if (ILOC[obj] == 0)
            {
                PrintResponse("You already have " + VOCABS[InputWordNum_INPTK[2]]);
                return;
            }
            // take picture from the wall
            if (ILOC[obj] == 30 && LCL == 3 && obj == 5)
            {
                PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            //if (IC >= 8)
            //{
            //	print("YOU'RE CARRYING TOO MUCH");
            //}
            if (obj > IMMOFF)
            {
                print("IT'S TOO HEAVY");
                return;
            }
            if (LCL == 29 && obj == 12)
            {
                print("YOU CAN'T DO THAT");
                return;
            }
            if (LCL == 30 && obj == 5)
            {
                print("TAKING THE PICTURE REVEALS A FUSEBOX");
                ILOC[obj] = 0;
                ILOC[IMMOFF + 7] = 30;
                // # GOTO 2500;
                return;
            }

            if (ILOC[obj] != LCL)
            {
                PrintResponse("There is no " + VOCABS[InputWordNum_INPTK[2]] + " here");
                return;
            }

            // if player is carrying object, ILOC[obj] == 0
            ILOC[obj] = 0;
            PrintResponse(VOCABS[InputWordNum_INPTK[2]] + ": taken");
        }


        public void ActionDrop(int obj)
        {
            if (ILOC[obj] != 0)
            {
                PrintResponse("You aren't carrying " + VOCABS[InputWordNum_INPTK[2]]);
                return;
            }
            // IC = IC - 1;
            if (LCL == 17 && obj == 10 && LocationExit[17, 1] <= 0)
            {
                print("THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT.");
                // # GOTO 6690;
            }
            if (LCL == 17 && obj == 2 && LocationExit[17, 1] <= 0)
            {
                print("THE DOG CHEWS HIS FAVORITE TOY AND IS SOON ASLEEP");
                ILOC[obj] = -999;
                LocationExit[17, 1] = 18;
                // # GOTO 2500;
            }
            if (LCL == 29 && obj == 12 && LocationExit[29, 5] <= 0)
            {
                print("THE BOXSPRING COVERS THE GAP IN THE STAIRS");
                ILOC[obj] = -999;
                LocationExit[29, 5] = 2;
                LocationExit[2, 6] = 29;
                // # GOTO 2500;
            }
            // # 6690;

            // if player is carrying object, ILOC[obj] == current local
            ILOC[obj] = LCL;
            PrintResponse(VOCABS[InputWordNum_INPTK[2]] + ": dropped");
        }
        public void ActionLook(int obj)
        {
            // look at the picture on the wall
            if (ILOC[obj] == 30 && LCL == 3 && obj == 5)
            {
                PrintResponse("The picture is hanging too high up on the wall, you have to find another way to reach it...");
                return;
            }

            // check if the object is here and not hidden
            if (ILOC[obj] != 0 && ILOC[obj] != LCL)
            {
                PrintResponse("There is no " + VOCABS[InputWordNum_INPTK[2]] + " here");
                return;
            }

            // 9=note and (13=master bedroom OR 22=bathroom)
            if (obj == 9 && (LCL == 13 || LCL == 22))
            {
                // decide which door is safe
                if (SAFED != 0)
                {
                    return;
                }
                //int rnd = RNG(3);
                //SAFED = (rnd * 3) + 1;
                SAFED = RNG(3);
                string N1S = VOCABS[DIROFF + 1];
                string N2S = VOCABS[DIROFF + 3];
                if (SAFED == 1)
                {
                    N1S = VOCABS[DIROFF + 2];
                }
                if (SAFED == 3)
                {
                    N2S = VOCABS[DIROFF + 2];
                }
                print("EXPERIMENTS ON " + N1S + " AND " + N2S + " DOORS PROCEEDING WELL; FILE FOR PATENT");
            }

            // Print extended obj description
            if (IDESCS[obj] == "")
            {
                PrintResponse("There's nothing special about the " + VOCABS[InputWordNum_INPTK[2]]);
            }
            else
            {
                PrintResponse(IDESCS[obj]);
            }
        }
        public void ActionUnlock(int obj)
        {
            if (LCL != 5 && LCL != 17)
            {
                print("THERE IS NO DOOR HERE!");
            }
            // do we have a key?
            if (ILOC[7] != 0) // 13 = master bedroom ? key ?
            {
                print("YOU DON'T HAVE A KEY!");
            }
            else // yes
            {
                // ckeck locale
                if (LCL == 5) // Hallway?
                {
                    print("THE KEY DOESN'T FIT THE LOCK");
                }
                // only unlock if is in the Hallway and have a key
                if (LCL == 17 && ILOC[7] == 0) // Hallway?
                {
                    print("YOU UNLOCK THE DOOR. BEWARE!");
                    LocationExit[17, 4] = 20;
                    // # GOTO 2500;
                }
            }
        }
        public void ActionEat(int obj)
        {
            if (ILOC[obj] != 0)
            {
                print("YOU DON'T HAVE IT!");
            }
            else if (obj != 10)
            {
                print("YOU CAN'T EAT THAT!");
            }

            print("THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER");

            ILOC[obj] = -2;
            ILOC[17] = 0;
        }
        public void ActionSpin()
        {
            if (ILOC[8] != 0)
            {
                print("HUH?");
            }
            if (LCL == 18)
            {
                print("THERE IS A FLASH OF LIGHT AND A CRACKING SOUND. AN OPENING APPEARS IN THE EAST WALL");
                LocationExit[18, 3] = 19;
                // # GOTO 2500;
            }
            // #INVERSE:;
            print("WHEE!");
            // # NORMAL:;
            print(" ");
        }

        public void ActionMove()
        {
            int AIMM = ARG - IMMOFF;
            if (AIMM >= 1 && AIMM <= 4)
            {
                // #ON AIMM GOTO 6970, 6975, 6980;
                int X = AIMM;
                if (X == 1)
                {
                    // #FN_6970();
                    print("IT'S TOO HEAVY FOR YOU TO MOVE");
                }
                else if (X == 2)
                {
                    // #FN_6975();
                    print("YOUR BACK IS ACTING UP");
                }
                else if (X == 3)
                {
                    // #FN_6980();
                    print("THAT SEEMS POINTLESS AND UNSANITARY");
                }
                else
                {
                    print("YOU CAN'T DO THAT");
                }
            }
        }

        public void ActionOpenDoor()
        {
            // hall: OPEN x Door
            if (LCL == 20 && (InputWordNum_INPTK[2] - ITEMOFF) == IMMOFF + 4)
            {
                print("PLEASE SPECIFY LEFT, CENTER OR RIGHT");
            }
            else
            {
                print("HUH?");
            }
            // # RETURN;
        }

        public void ActionJump()
        {
            // location 12 = BALCONY
            if (LCL != 12)
            {
                PrintResponse("You jump up and down a couple of times and feel more relaxed now, but nothing special happens.");
                // print("WHO ARE YOU, DAVID LEE ROTH? (Van Halen)");
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
            LCL = 30;
            TURN1 = 0; // reset for multiple jumps
        }

        public void ActionPlayerMove(int dir)
        {
            // convert N,S,E,W,U,D to long form
            if (dir > 6)
            {
                dir = dir - 6;
            }

            if (LocationExit[LCL, dir] > 0)
            {
                // new position
                LCL = LocationExit[LCL, dir];
                // # GOTO 2500;
            }
            else if (LCL == 12 && dir == 5)
            {
                print("YOU'RE AFRAID OF THE DARK");
            }
            else if (LCL == 17 && dir == 1)
            {
                print("YOU NEVER DID LIKE THAT DOG");
            }
            else if (LCL == 23 && LocationExit[23, 6] <= 0)
            {
                print("THE DUMBWAITER MECHANISM IS CORRODED AND WON'T MOVE");
            }
            else
            {
                print("YOU CAN'T GO THAT WAY");
            }
        }

        public void ActionMoveFridgeWithJack()
        {
            if (MVARG != 4 || ILOC[3] >= 0)
            {
                print("YOU CAN'T DO THAT");
            }
            else
            {
                print("YOU JACK UP THE FRIDGE AND FIND A FUSE UNDER IT");
                ILOC[3] = LCL;
            }
        }

        public void ActionMoveCouchWithBrace()
        {
            if (MVARG != 13 || ILOC[2] >= 0)
            {
                print("YOU CAN'T DO THAT");
            }
            else
            {
                print("YOU MOVE THE COUCH AND FIND A TEDDYBEAR BEHIND IT");
                ILOC[2] = LCL;
            }
        }

        public void ActionMoveClothesWithGloves()
        {
            if (MVARG != 11)
            {
                print("YOU CAN'T DO THAT");
            }
            else
            {
                print("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT");
                LocationExit[LCL, 6] = 27;
            }
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
            if (LCL != 12)
            {
                PrintResponse("There is nothing here to tie to");
                return;
            }

            // object is not BUNGEE cord
            if (ARG != 6)
            {
                PrintResponse("You can't tie to that");
                return;
            }

            // RAINLING?
            if (MVARG != (IMMOFF + 5))
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
            if (LCL != 20)
            {
                print("YOU CAN'T DO THAT");
            }
            if (ILOC[15] != 0)
            {
                print("YOU DON'T HAVE ANY OIL");
            }
            if (InputWordNum_INPTK[2] - ITEMOFF != IMMOFF + 6)
            {
                print("HUH?");
            }

            print("THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY");
            LocationExit[23, 6] = 24;
        }

        public void ActionPutFuseInFusebox()
        {
            if (LCL != 30 || (InputWordNum_INPTK[2] - ITEMOFF != 3))
            {
                print("YOU CAN'T DO THAT");
            }
            if (MVARG != (IMMOFF + 7))
            {
                print("YOU CAN'T PUT IT THERE");
            }
            if (ILOC[3] != 0)
            {
                print("YOU DON'T HAVE IT!");
            }

            print("YOU PUT THE FUSE IN THE BOX");
            // mark fuse as used
            ILOC[3] = -999;

            //IC--;
            // STAIRS TO ATTIC is hidden until fuse is inserted
            LocationExit[12, 5] = 25;
        }
    }
}
