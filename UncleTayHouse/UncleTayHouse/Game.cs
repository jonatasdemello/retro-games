namespace UncleTayHouse
{
    public partial class Game
    {
        public void Play()
        {
            Console.Clear();
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            // disable for now
            // ActionIntro();

            while (true)
            {
                ShowLocation();

                ActionReadInput();

                ActionProcessInput();

                PrintDgb1(" next round ");
            }
        }
        public void ShowLocation()
        {
            if (LCL == 30 && TURN1 == 0) // first time after jump
                TURN1 = 1;
            else if (LCL == 30 && TURN1 == 1) // second time
            {
                PrintResponse("... and bunge cord spring back");
                LCL = 12; // back to the balcony
            }

            if (LCL == 31)
            {
                ActionScore();
                ActionExit();
            }
            ActionLocation();
            ActionDirections();
            ActionExtendedDescriptions();
        }
        public void ActionReadInput()
        {
            string userInput = ReadInput();
            ProcessInput(userInput);

            CMD = InputWordNum_INPTK[1]; // first word cmd
            ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object
            MVARG = InputWordNum_INPTK[3] - ITEMOFF; // third word object
        }
        public void ActionProcessInput()
        {
            print("*");
            if (InputWordTotal < 1)
            {
                PrintResponse("You need 1 word to move, 2+ words (verb + noun) for actions.");
            }
            else if (InputWordTotal == 1)
            {
                // "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK"
                ActionOneWord();
            }
            else if (InputWordTotal == 2)
            {
                // 18"TAKE", 19"DROP", 20"LOOK", 21"READ", 22"EXAMINE",
                // 23"UNLOCK", 24"EAT", 25"SPIN", 26"MOVE", 27"OPEN", 28"TIE",
                ActionTwoWords();
            }
            else if (InputWordTotal == 3)
            {
                ActionThreeWords();
            }
            else
            {
                PrintResponse("You can't do that.");
            }
            print("*");
        }
        public void ActionOneWord()
        {
            if (CMD > 33)
            {
                PrintResponse("Do what with "+ VOCABS[CMD] +"?");
            }
            else if (CMD >= 1 && CMD <= 12)
            {
                ActionPlayerMove(CMD);
            }
            else if (CMD == 13 || CMD == 14)
            {
                ActionInventory();
            }
            else if (CMD == 15)
            {
                ActionScore();
            }
            else if (CMD == 16)
            {
                ActionJump();
            }
            else if (CMD == 17) // HELP
            {
                ActionIntro();
            }
            else if (CMD == 18) // TAKE
            {
                PrintResponse("Take what?");
            }
            else if (CMD == 19) // DROP
            {
                PrintResponse("Drop what?");
            }
            else if (CMD == 20) // LOOK
            {
                ShowLocation();
            }
        }
        public void ActionTwoWords()
        {
            if (ARG < 1 || ARG > LASTITEM)
            {
                print("HUH?");
            }

            if (CMD > 17 && CMD <= 28)
            {
                // #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200;
                // 18"TAKE", 19"DROP", 20"LOOK", 21"READ", 22"EXAMINE",
                // 23"UNLOCK", 24"EAT", 25"SPIN", 26"MOVE", 27"OPEN", 28"TIE",;
                int X = CMD - 17;
                if (X == 1)
                {
                    ActionTake(ARG); // 18"TAKE"
                }
                else if (X == 2)
                {
                    ActionDrop(ARG); // 19"DROP"
                }
                else if (X >= 3 && X <= 5)
                {
                    ActionLook(ARG); // 20"LOOK", 21"READ", 22"EXAMINE"
                }
                else if (X == 6)
                {
                    ActionUnlock(ARG); // 23"UNLOCK"
                }
                else if (X == 7)
                {
                    ActionEat(ARG); // 24"EAT"
                }
                else if (X == 8)
                {
                    ActionSpin(); // 25"SPIN",
                }
                else if (X == 9)
                {
                    ActionMove(); // 26"MOVE"
                }
                else if (X == 10)
                {
                    ActionOpenDoor(); // 27"OPEN x door"
                }
                else if (X == 11)
                {
                    ActionTieBungeeToRailing(); // 28"TIE bungee"
                }
                else
                {
                    ActionHuh();
                }
            }
        }
        public void ActionThreeWords()
        {
            if (ARG  < 0)
            {
                ARG = InputWordNum_INPTK[2] - DIROFF; // second word object
            }
            if (CMD < 23 || CMD > 30)
            {
                ActionHuh();
            }
            if (CMD != 27 && ARG < 1 || ARG > LASTITEM)
            {
                print("HUH?");
            }
            if (CMD != 23 && CMD != 29 && ILOC[ARG] != LCL && ILOC[ARG] != 0)
            {
                print("IT'S NOT HERE");
            }

            int X = CMD - 22;
            if (X == 1)
            {
                ActionUnlock(ARG);
            }
            else if (X == 2)
            {
                ActionHuh();
            }
            else if (X == 3)
            {
                ActionHuh();
            }
            else if (X == 4)
            {
                Action3Move();
            }
            else if (X == 5)
            {
                Action3Open();
            }
            else if (X == 6)
            {
                ActionTieBungeeToRailing();
            }
            else if (X == 7)
            {
                ActionOilDumbwaiterWithOilcan();
            }
            else if (X == 8)
            {
                ActionPutFuseInFusebox();
            }
        }
        public void Action3Move()
        {
            if (ARG < IMMOFF)
            {
                print("YOU CAN JUST TAKE THAT");
            }

            int AIMM = ARG - IMMOFF;
            if (AIMM < 1 || AIMM > 3)
            {
                print("YOU CAN'T DO THAT");
            }

            if (ILOC[MVARG] != 0)
            {
                print("YOU DON'T HAVE IT!");
            }

            if (AIMM == 1)
            {
                ActionMoveFridgeWithJack();
            }
            else if (AIMM == 2)
            {
                ActionMoveCouchWithBrace();
            }
            else if (AIMM == 3)
            {
                ActionMoveClothesWithGloves();
            }
        }
        public void Action3Open()
        {
            if (LCL != 20)
            {
                print("HUH?");
                return;
            }
            if (MVARG != IMMOFF + 4)
            {
                print("HUH?");
                return;
            }
            int DOORDIR = InputWordNum_INPTK[2] - DIROFF;
            if (DOORDIR < 1 || DOORDIR > 3)
            {
                print("HUH?");
                return;
            }

            if (SAFED != 0)
            {
                return;
            }
            SAFED = (InputWordNum_INPTK[2] - DIROFF) + 1;
            if (SAFED > 3)
            {
                SAFED = 1;
            }

            if (DOORDIR == SAFED)
            {
                print("OPENING THE DOOR REVEALS A DUMBWAITER");
                LocationExit[LCL, 4] = 23;
                return;
            }

            int rnd = RNG(100);
            if (rnd > 50)
            {
                print("A SHOT RINGS OUT! IT WAS WELL-AIMED TOO.");
                // you die
                // GOTO 19000;
                return;
            }
            print("AN IRONING BOARD SLAMS ONTO YOUR HEAD");
            // you die
            // GOTO 19000;
        }

    }
}