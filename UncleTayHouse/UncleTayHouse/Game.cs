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
				FN_2500_local();

				FN_5000_read_input();

				PrintDgb1(" *** ");
			}
		}

		public void FN_2500_local()
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

		public void FN_5000_read_input()
		{
			if (TURN1 != 1 && LCL == 30)
			{
				print("...AND SPRING BACK");
				LCL = 12;
				FN_2500_local();
			}
			TURN1 = 0;

			string userInput = ReadInput("] ");
			ProcessInput(userInput);
			/*
			//string IS = ReadInput("] ");
			// split input:

			string IS = userInput;
			var words = IS.Split(" ");

			for (var i = 1; i < words.Length + 1; i++)
			{
				InputWords_INWS[i] = words[i - 1].Trim();
			}

			int CURTOK = 1;
			for (var i = 1; i <= words.Length; i++)
			{
				int ISNULLW = 0;
				for (var j = 0; j < 4; j++)
				{
					if (InputWords_INWS[i] == NULLWORDS[j])
					{
						ISNULLW = 1;
					}
				}
				if (ISNULLW == 1)
				{
					continue;
				}
				int LWRD = 60;
				for (var k = 0; k < LWRD; k++)
				{
					if (InputWords_INWS[i] == VOCABS[k])
					{
						InputWordNum_INPTK[CURTOK] = k;
						CURTOK++;
						break;
					}
				}
			}

			int NTOK = CURTOK - 1;
			var X = NTOK + 1;
			*/
			var X = InputWords + 1;
			if (X == 1)
			{
				FN_5000_read_input();
			}
			else if (X == 2)
			{
				FN_6050();
			}
			else if (X == 3)
			{
				FN_6400();
			}
			else if (X == 4)
			{
				FN_7100();
			}
			else
			{
				print("YOU CAN'T DO THAT");
			}
		}
		public void FN_6050()
		{
			COMM = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

			if (COMM >= 1 && COMM <= 12)
			{
				ActionPlayerMove(COMM);
			}
			if (COMM <= 20)
			{
				// # ON COMM-12 GOTO 6100, 6100, 6200, 6300, 6350, 6099, 6099, 2500;
				// # "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK";
				var X = COMM - 12;
				if (X == 1 || X == 2)
				{
					ActionInventory();
				}
				else if (X == 3)
				{
					ActionScore();
				}
				else if (X == 4)
				{
					ActionJump();
				}
				else if (X == 5)
				{
					ActionIntro(); // help
				}
				else if (X == 6)
				{
					print("HUH?");
				}
				else if (X == 7)
				{
					print("HUH?");
				}
				else if (X == 8)
				{
					FN_2500_local();
				}
			}
		}

		public void FN_6400()
		{
			COMM = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

			if (ARG < 1 || ARG > LASTITEM)
			{
				print("HUH?");
			}

			if (COMM > 17 && COMM <= 27)
			{
				// #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200;
				// 18"TAKE", 19"DROP", 20"LOOK", 21"READ", 22"EXAMINE",
				// 23"UNLOCK", 24"EAT", 25"SPIN", 26"MOVE", 27"OPEN", 28"TIE",;
				var X = COMM - 17;
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


		public void FN_7100()
		{
			COMM = InputWordNum_INPTK[1]; // first word cmd
			ARG = InputWordNum_INPTK[2] - ITEMOFF; // second word object

			if (COMM < 23 || COMM > 30)
			{
				ActionHuh();
			}
			if (COMM != 27 && ARG < 1 || ARG > LASTITEM)
			{
				print("HUH?");
			}
			if (COMM != 23 && COMM != 29 && ILOC[ARG] != LCL && ILOC[ARG] != 0)
			{
				print("IT'S NOT HERE");
			}
			// # ON COMM-22 GOTO 6800, 6899, 6899, 7200, 7400, 7500, 7700, 7800;
			var X = COMM - 22;
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
				FN_7500();
			}
			else if (X == 7)
			{
				FN_7700();
			}
			else if (X == 8)
			{
				FN_7800();
			}
		}
		public void FN_7200()
		{
			if (ARG < IMMOFF)
			{
				print("YOU CAN JUST TAKE THAT");
			}
			AIMM = ARG - IMMOFF;
			MVARG = InputWordNum_INPTK[3] - ITEMOFF;
			if (AIMM < 1 || AIMM > 3)
			{
				print("YOU CAN'T DO THAT");
			}
			if (ILOC[MVARG] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			// # ON AIMM GOTO 7250, 7300, 7350;
			var X = AIMM;
			if (X == 1)
			{
				FN_7250();
			}
			else if (X == 2)
			{
				FN_7300();
			}
			else if (X == 3)
			{
				FN_7350();
			}
		}
		public void FN_7250()
		{
			if (MVARG != 4 || ILOC[3] >= 0)
			{
				print("YOU CAN'T DO THAT");
			}
			else
			{
				print("YOU JACK UP THE FRIDGE && FIND A FUSE UNDER IT");
				ILOC[3] = LCL;
				// # GOTO 2500;
			}
		}
		public void FN_7300()
		{
			if (MVARG != 13 || ILOC[2] >= 0)
			{
				print("YOU CAN'T DO THAT");
			}
			else
			{
				print("YOU MOVE THE COUCH && FIND A TEDDYBEAR BEHIND IT");
				ILOC[2] = LCL;
				// # GOTO 2500;
			}
		}
		public void FN_7350()
		{
			if (MVARG != 11)
			{
				print("YOU CAN'T DO THAT");
			}
			else
			{
				print("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT");
				LocationExit[LCL, 6] = 27;
				// # GOTO 2500;
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
			int DOORDIR = 0;
			DOORDIR = InputWordNum_INPTK[2] - DIROFF;
			if (DOORDIR < 1 || DOORDIR > 3)
			{
				print("HUH?");
			}
			FN_8025();
			if (DOORDIR == SAFED)
			{
				print("OPENING THE DOOR REVEALS A DUMBWAITER");
				LocationExit[LCL, 4] = 23;
				// # GOTO 2500;
			}
			int rnd = RNG(2);
			if (rnd > 1)
			{
				print("A SHOT RINGS OUT! IT WAS WELL-AIMED TOO.");
				// # GOTO 19000;
			}
			print("AN IRONING BOARD SLAMS ONTO YOUR HEAD");
			// # GOTO 19000;
		}
		public void FN_8025()
		{
			if (SAFED != 0)
			{
				return;
				// # RETURN;
			}
			SAFED = (InputWordNum_INPTK[2] - DIROFF) + 1;
			if (SAFED > 3)
			{
				SAFED = 1;
			}
			// # RETURN;
		}
		public void FN_7500()
		{
			if (LCL != 12)
			{
				print("YOU CAN'T DO THAT");
			}
			if (InputWordNum_INPTK[2] - ITEMOFF != 6)
			{
				print("YOU CAN'T TIE THAT");
			}
			if (InputWordNum_INPTK[3] - ITEMOFF != (IMMOFF + 5))
			{
				print("YOU CAN'T TIE TO THAT");
			}
			if (ILOC[6] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			print("TIED");
			ILOC[6] = -12;
			IC--;
			// # GOTO 2500;
		}

		public void FN_7700()
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
		public void FN_7800()
		{
			if (LCL != 30 || (InputWordNum_INPTK[2] - ITEMOFF != 3))
			{
				print("YOU CAN'T DO THAT");
			}
			if (InputWordNum_INPTK[3] - ITEMOFF != (IMMOFF + 7))
			{
				print("YOU CAN'T PUT IT THERE");
			}
			if (ILOC[3] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			print("YOU PUT THE FUSE IN THE BOX");
			ILOC[3] = -999;
			IC--;
			LocationExit[12, 5] = 25;
		}



	}
}