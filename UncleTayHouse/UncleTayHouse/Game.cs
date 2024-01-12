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

            ActionIntro();

            while (true)
            {
                ShowLocation();

                ActionReadInput();

                ActionProcessInput();
            }
        }
        public void ShowLocation()
        {
            if (LOCAL == 30 && TURN1 == 0) // first time after jump
            {
                TURN1 = 1;
            }
            else if (LOCAL == 30 && TURN1 == 1) // second time
            {
                PrintResponse("... and bunge cord spring back");
                LOCAL = 12; // back to the balcony
            }

            if (LOCAL == 31) // exit
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
        }
        public void ActionProcessInput()
        {
            if (InputWordTotal < 1)
            {
                PrintResponse("You need 1 word to move, 2+ words (verb + noun) for actions.");
            }
            else if (InputWordTotal == 1)
            {
                ActionOneWord();
            }
            else if (InputWordTotal == 2)
            {
                ActionTwoWords();
            }
            else if (InputWordTotal == 3)
            {
                ActionThreeWords();
            }
            else
            {
                PrintResponse("I don't understand...");
            }
        }
        public void ActionOneWord()
        {
            if (CMD1 >= 1 && CMD1 <= 12) // move N S E W U D
            {
                ActionPlayerMove(CMD1);
            }
            else if (CMD1 == 13 || CMD1 == 14) // inventory
            {
                ActionInventory();
            }
            else if (CMD1 == 15) // score
            {
                ActionScore();
            }
            else if (CMD1 == 16) // jump
            {
                ActionJump();
            }
            else if (CMD1 == 17) // help
            {
                ActionIntro();
            }
            else if (CMD1 == 18) // take
            {
                PrintResponse("Take what?");
            }
            else if (CMD1 == 19) // drop
            {
                PrintResponse("Drop what?");
            }
            else if (CMD1 == 20 || CMD1 == 62) // look
            {
                ShowLocation();
            }
            else if (CMD1 > OBJECTOFFSET) // objects, not verbs
            {
                PrintResponse("Do what with " + VOCABS[CMD1] + "?");
            }
            else
            {
                PrintResponse("I don't understand...");
            }
        }
        public void ActionTwoWords()
        {
            if (CMD1 == 18) // take
            {
                ActionTake();
            }
            else if (CMD1 == 19) // drop
            {
                ActionDrop();
            }
            else if (CMD1 == 20 || CMD1 == 21 || CMD1 == 22 || CMD1 == 64) // look, read, examine
            {
                ActionLook();
            }
            else if (CMD1 == 23) // unlock
            {
                ActionUnlock();
            }
            else if (CMD1 == 24) // eat
            {
                ActionEat();
            }
            else if (CMD1 == 25) // spin
            {
                ActionSpin();
            }
            else if (CMD1 == 26) // move
            {
                ActionMoveObj();
            }
            else if (CMD1 == 27) // open (X door)
            {
                ActionOpen();
            }
            else if (CMD1 == 28) // tie
            {
                ActionTieBungeeToRailing();
            }
            else
            {
                PrintResponse("I don't understand...");
            }
        }
        public void ActionThreeWords()
        {
            if (CMD2 == 0 || CMD3 == 0)
            {
                PrintResponse("You need 3 words");
                return;
            }
            // read note in mirror (look, read, examine)
            if ((CMD1 == 20 || CMD1 == 21 || CMD1 == 22) && CMD2 == 42 && CMD3 == 61)
            {
                ActionReadNoteInMirror();
                return;
            }
            // move couch with brace
            if (CMD1 == 26 && CMD2 == 55 && CMD3 == 46)
            {
                ActionMoveCouchWithBrace();
                return;
            }
            // move fridge with jack
            if (CMD1 == 26 && CMD2 == 54 && CMD3 == 37)
            {
                ActionMoveFridgeWithJack();
                return;
            }
            // move clothes with gloves
            if (CMD1 == 26 && CMD2 == 56 && CMD3 == 44)
            {
                ActionMoveClothesWithGloves();
                return;
            }

            // open[direction not mentioned in note] door
            if (CMD1 == 27 && (CMD2 >= 31 && CMD2 <= 33) && CMD3 == 57)
            {
                ActionOpen3Door();
                return;
            }

            // tie bungee to railing
            if (CMD1 == 28 && CMD2 == 39 && CMD3 == 58)
            {
                ActionTieBungeeToRailing();
                return;
            }
            // oil,unlock dumbwaiter with oilcan
            if ((CMD1 == 23 || CMD1 == 29) && CMD2 == 59 && CMD3 == 48)
            {
                ActionOilDumbwaiterWithOilcan();
                return;
            }
            // put fuse in fusebox
            if (CMD1 == 30 && CMD2 == 36 && CMD3 == 60)
            {
                ActionPutFuseInFusebox();
                return;
            }
        }
    }
}