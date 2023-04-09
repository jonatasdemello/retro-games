using System;
using System.Globalization;
using System.Net;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;
namespace UncleTayHouse
{
	public partial class Game
	{
		public void Play()
		{
			ActionIntro();

			while (true)
			{
				ShowLocale();

				ActionReadInput();

				PrintDgb1(" *** ");
			}
		}

		public void ShowLocale()
		{
			if (LCL == 30)
			{
				print("... AND SPRING BACK");
				LCL = 12;
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
			if (TURN1 != 1 && LCL == 30)
			{
				print("...AND SPRING BACK");
				LCL = 12;
				ShowLocale();
			}
			TURN1 = 0;

			string userInput = ReadInput("] ");
			ProcessInput(userInput);

			if (InputWords < 1)
			{
				print("You need 1 word to move, 2+ words (verb + noun) for actions.");
			}
			else if (InputWords == 1)
			{
				// "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK"
				ActionOneWord();
			}
			else if (InputWords == 2)
			{
				// 18"TAKE", 19"DROP", 20"LOOK", 21"READ", 22"EXAMINE",
				// 23"UNLOCK", 24"EAT", 25"SPIN", 26"MOVE", 27"OPEN", 28"TIE",
				ActionTwoWords();
			}
			else if (InputWords == 3)
			{
				ActionThreeWords();
			}
			else
			{
				print("YOU CAN'T DO THAT");
			}
		}
		public void ActionOneWord()
		{
			CMD = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

			if (CMD >= 1 && CMD <= 12)
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
				print("HUH?");
			}
			else if (CMD == 19) // DROP
			{
				print("HUH?");
			}
			else if (CMD == 20) // LOOK
			{
				ShowLocale();
			}
		}

		public void ActionTwoWords()
		{
			CMD = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

			if (ARG < 1 || ARG > LASTITEM)
			{
				print("HUH?");
			}

			if (CMD > 17 && CMD <= 27)
			{
				// #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200;
				// 18"TAKE", 19"DROP", 20"LOOK", 21"READ", 22"EXAMINE",
				// 23"UNLOCK", 24"EAT", 25"SPIN", 26"MOVE", 27"OPEN", 28"TIE",;
				var X = CMD - 17;
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
				else
				{
					ActionHuh();
				}
			}
		}

		public void ActionThreeWords()
		{
			CMD = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

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

			var X = CMD - 22;
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
				FN_7200();
			}
			else if (X == 5)
			{
				FN_7400();
			}
			else if (X == 6)
			{
				ActionTie();
			}
			else if (X == 7)
			{
				ActionOilDumbwaiterWithOilcan();
			}
			else if (X == 8)
			{
				PutFuseInFusebox();
			}
		}
		public void FN_7200()
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

			MVARG = InputWordNum_INPTK[3] - ITEMOFF;
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


		public void FN_7400()
		{
			if (LCL != 20)
			{
				print("HUH?");
			}
			if (InputWordNum_INPTK[3] - ITEMOFF != IMMOFF + 4)
			{
				print("HUH?");
			}
			int DOORDIR = InputWordNum_INPTK[2] - DIROFF;

			if (DOORDIR < 1 || DOORDIR > 3)
			{
				print("HUH?");
			}

			if (SAFED != 0)
			{
				return;
				// RETURN;
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
				// GOTO 2500;
			}
			int rnd = RNG(2);
			if (rnd > 1)
			{
				print("A SHOT RINGS OUT! IT WAS WELL-AIMED TOO.");
				// GOTO 19000;
			}
			print("AN IRONING BOARD SLAMS ONTO YOUR HEAD");
			// GOTO 19000;
		}

	}
}