namespace UncleTayHouse
{
    public partial class Game
    {
        public UserInputResult user = new();

        // player: current location
        public int LOCAL = 1;

        public int SAFEDoor = 0;
        public int TURN1 = 0;

        public void Play()
        {
            Screen.ClearScreen();

            ShowIntro();

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
                Screen.PrintResponse("... and bunge cord spring back");
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
            string userInput = UserInput.ReadInput();
            user = UserInput.ProcessInput(userInput);
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
            if (user.CMD1 >= 1 && user.CMD1 <= 12) // move N S E W U D
            {
                ActionPlayerMove(user.CMD1);
            }
            else if (user.CMD1 == 13 || user.CMD1 == 14) // inventory
            {
                ActionInventory();
            }
            else if (user.CMD1 == 15) // score
            {
                ActionScore();
            }
            else if (user.CMD1 == 16) // jump
            {
                ActionJump();
            }
            else if (user.CMD1 == 17) // help
            {
                ShowIntro();
            }
            else if (user.CMD1 == 18) // take
            {
                Screen.PrintResponse("Take what?");
            }
            else if (user.CMD1 == 19) // drop
            {
                Screen.PrintResponse("Drop what?");
            }
            else if (user.CMD1 == 20 || user.CMD1 == 62) // look
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
            if (user.CMD1 == 18) // take
            {
                ActionTake();
            }
            else if (user.CMD1 == 19) // drop
            {
                ActionDrop();
            }
            else if (user.CMD1 == 20 || user.CMD1 == 21 || user.CMD1 == 22 || user.CMD1 == 64) // look, read, examine
            {
                ActionLook();
            }
            else if (user.CMD1 == 23) // unlock
            {
                ActionUnlock();
            }
            else if (user.CMD1 == 24) // eat
            {
                ActionEat();
            }
            else if (user.CMD1 == 25) // spin
            {
                ActionSpin();
            }
            else if (user.CMD1 == 26) // move
            {
                ActionMoveObj();
            }
            else if (user.CMD1 == 27) // open (X door)
            {
                ActionOpen();
            }
            else if (user.CMD1 == 28) // tie
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
            if (user.CMD2 == 0 || user.CMD3 == 0)
            {
                Screen.PrintResponse("You need 3 words");
                return;
            }
            // read note in mirror (look, read, examine)
            if ((user.CMD1 == 20 || user.CMD1 == 21 || user.CMD1 == 22) && user.CMD2 == 42 && user.CMD3 == 61)
            {
                ActionReadNoteInMirror();
                return;
            }
            // move couch with brace
            if (user.CMD1 == 26 && user.CMD2 == 55 && user.CMD3 == 46)
            {
                ActionMoveCouchWithBrace();
                return;
            }
            // move fridge with jack
            if (user.CMD1 == 26 && user.CMD2 == 54 && user.CMD3 == 37)
            {
                ActionMoveFridgeWithJack();
                return;
            }
            // move clothes with gloves
            if (user.CMD1 == 26 && user.CMD2 == 56 && user.CMD3 == 44)
            {
                ActionMoveClothesWithGloves();
                return;
            }
            // open[direction not mentioned in note] door
            if (user.CMD1 == 27 && (user.CMD2 >= 31 && user.CMD2 <= 33) && user.CMD3 == 57)
            {
                ActionOpen3Door();
                return;
            }
            // tie bungee to railing
            if (user.CMD1 == 28 && user.CMD2 == 39 && user.CMD3 == 58)
            {
                ActionTieBungeeToRailing();
                return;
            }
            // oil,unlock dumbwaiter with oilcan
            if ((user.CMD1 == 23 || user.CMD1 == 29) && user.CMD2 == 59 && user.CMD3 == 48)
            {
                ActionOilDumbwaiterWithOilcan();
                return;
            }
            // put fuse in fusebox
            if (user.CMD1 == 30 && user.CMD2 == 36 && user.CMD3 == 60)
            {
                ActionPutFuseInFusebox();
                return;
            }
        }
    }
}