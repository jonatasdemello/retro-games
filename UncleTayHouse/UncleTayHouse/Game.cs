using System;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
namespace UncleTayHouse
{
	public partial class Game
	{
		public void Play()
		{
			FN_GAME();
		}
		public string INPUT(string prompt)
		{
			Console.Write(prompt);
			var x = Console.ReadLine();
			return x.ToUpper();
		}
		public int RNG(int max)
		{
			Random rnd = new Random();
			int value = rnd.Next(0, max);
			return value;
		}
		public void print(string text) => Console.WriteLine(text);
		public void print(string text1, string text2) => Console.WriteLine($"{text1} {text2}");
		public void print(string text1, string text2, string text3) => Console.WriteLine($"{text1}  {text2} {text3}");
		public void PrintDgb(string text)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.Write("----------------------------- " + text + " ----------------------------- ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ResetColor();
			Console.WriteLine();
		}
		public void PrintDgb1(string text)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write("----------------------------- " + text + " ----------------------------- ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ResetColor();
			Console.WriteLine();
		}

		public void FN_15000_INTRO()
		{
			print(" ");
			print("TAYS HOUSE ADVENTURE");
			print("--------------------");
			print("FIND TREASURES && VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE");
			print("TYPE SIMPLE COMMANDS:");
			print("   TO MOVE AROUND: NORTH, SOUTH, EAST, WEST, UP, DOWN (OR JUST N, S, E, W, U, D).");
			print("TAKE, DROP, INVENTORY, LOOK, READ, MOVE, AND SO ON.");
			print("SOME COMMANDS ARE COMPLEX: 'MOVE THE HUBCAP WITH THE SPANNER'");
			print(" ");
			print("commands: ");
			print("NORTH, SOUTH, EAST, WEST, UP, DOWN, N, S, E, W, U, D,");
			print("I, INVENTORY, SCORE, JUMP, HELP,");
			print("TAKE, DROP, LOOK, READ, EXAMINE, UNLOCK, EAT, SPIN,");
			print("MOVE, OPEN, TIE, OIL, PUT, LEFT, CENTER, RIGHT,");
			print(" ");
		}
		public void FN_9500_SCORE()
		{
			int SCORE = 50;
			// add points for collected items
			for (var i = 16; i < 20; i++)
			{
				if (ILOC[i] == 0) // Carrying?
				{
					SCORE += 10;
				}
			}
			// reduce points for non explored items
			for (var i = 3; i < 30; i++)
			{
				for (var j = 1; j < 6; j++)
				{
					if (LocationExit[i, j] == -1)
					{
						SCORE -= 5;
					}
				}
			}
			// reduce points for ?
			for (var i = 1; i < 15; i++)
			{
				if (ILOC[i] == -1)
				{
					SCORE -= 5;
				}
			}
			// show result
			print("YOUR SCORE IS " + SCORE + " OUT OF A POSSIBLE 100");
			if (SCORE == 100)
			{
				print("YOU HAVE WON THE GAME!");
			}
		}
		public void FN_19000_GAME_OVER()
		{
			print("YOU HAVE DIED");
			FN_19999_END_GAME();
		}
		public void FN_19999_END_GAME()
		{
			print("THANK YOU FOR PLAYING, BYE!");
			Environment.Exit(0);
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
				FN_9500_SCORE();
				FN_19999_END_GAME();
			}
			PrintDgb("LOCATION & DESCRIPTION");
			print("LCL: ", LCL.ToString());
			print("L: ", LocationName_RNAMES[LCL]);
			print("D: ", LocationDescription_RDESCS[LCL]);
			print(" ");
			PrintDgb("DIRECTIONS");
			for (var I = 1; I <= 6; I++)
			{
				int NEIGH = LocationExit[LCL, I];
				if (NEIGH > 0)
				{
					print(VOCABS[I], ": ", LocationName_RNAMES[NEIGH]);
				}
			}
			print(" ");
			PrintDgb("EXTENDED DESCRIPTIONS");
			int NXDESC = 7;
			for (var I = 1; I < NXDESC; I++)
			{
				int L1 = EXLOC[I, 1];
				int L2 = EXLOC[I, 2];
				if (LCL == L1 && LocationExit[L1, L2] <= 0)
				{
					print(ExtendedDescription[I]);
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
			for (var I = 1; I <= LASTITEM; I++)
			{
				if (ILOC[I] == LCL)
				{
					print("THERE IS A ", VOCABS[I + ITEMOFF], " HERE");
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
			if (LCL == 30)
			{
				TURN1 = 1;
			}
		}
		public void FN_5000_read_input()
		{
			if (TURN1 != 1 && LCL == 30)
			{
				print("...AND SPRING BACK");
				LCL = 12;
				FN_2500_local();
			}
			// # --- READ INPUT ---
			TURN1 = 0;
			WIDX = 0;
			string IS = INPUT("] ");
			if (IS == "EXIT")
			{
				FN_19999_END_GAME();
			}
			// split input:
			var words = IS.Split(" ");
			WIDX = words.Length;
			for (var i = 1; i < WIDX + 1; i++)
			{
				INWS[i] = words[i - 1].Trim();
			}
		}
		public void FN_5100()
		{
			int CURTOK = 1;
			for (var i = 1; i <= WIDX; i++)
			{
				int ISNULLW = 0;
				for (var j = 0; j < 4; j++)
				{
					if (INWS[i] == NULLWORDS[j])
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
					if (INWS[i] == VOCABS[k])
					{
						INPTK[CURTOK] = k;
						CURTOK++;
						break;
					}
				}
			}

			int NTOK = CURTOK - 1;
			COMM = INPTK[1]; // first word cmd

			//# ON NTOK+1 // # GOTO 5000, 6050, 6400, 7100;
			var X = NTOK + 1;
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
			if (INPTK[1] >= 1 && INPTK[1] <= 12)
			{
				FN_7000();
			}
			if (COMM <= 20)
			{
				// # ON COMM-12 GOTO 6100, 6100, 6200, 6300, 6350, 6099, 6099, 2500;
				// # "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK";
				var X = COMM - 12;
				if (X == 1)
				{
					FN_6100_INV();
				}
				else if (X == 2)
				{
					FN_6100_INV();
				}
				else if (X == 3)
				{
					FN_9500_SCORE();
				}
				else if (X == 4)
				{
					FN_6300_JUMP();
				}
				else if (X == 5)
				{
					// help
					FN_15000_INTRO();
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

		public void FN_6100_INV()
		{
			print(" ");
			print("Inventory: YOU ARE CARRYING:");
			for (var I = 1; I < LASTITEM; I++)
			{
				if (ILOC[I] == 0)
				{
					print(" - ", VOCABS[I + ITEMOFF]);
				}
			}
		}
		public void FN_6300_JUMP()
		{
			if (LCL != 12)
			{
				print("WHO ARE YOU, DAVID LEE ROTH?");
			}
			if (ILOC[6] != -12)
			{
				print("YOU FORGOT YOUR PARACHUTE");
			}
			print("YOU BUNGEE OFF THE BALCONY...");
			LCL = 30;
		}

		public void FN_6400()
		{
			ARG = INPTK[2] - ITEMOFF;
			if (ARG < 1 || ARG > LASTITEM)
			{
				print("HUH?");
			}
			if (COMM > 17 && COMM <= 27)
			{
				// #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200;
				// # "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN", "MOVE", "OPEN", "TIE",;
				var X = COMM - 17;
				if (X == 1)
				{
					FN_6500_TAKE();
				}
				else if (X == 2)
				{
					FN_6600();
				}
				else if (X == 3)
				{
					FN_6700();
				}
				else if (X == 4)
				{
					FN_6700();
				}
				else if (X == 5)
				{
					FN_6700();
				}
				else if (X == 6)
				{
					FN_6800_OPEN();
				}
				else if (X == 7)
				{
					FN_6900();
				}
				else if (X == 8)
				{
					FN_7600();
				}
				else if (X == 9)
				{
					FN_6950();
				}
				else if (X == 10)
				{
					FN_8200();
				}
				else
				{
					print("HUH?");
				}
			}
		}
		public void FN_6500_TAKE()
		{
			if (ILOC[ARG] == 0)
			{
				print("YOU ALREADY HAVE IT");
			}
			if (ILOC[ARG] == 30 && LCL == 3 && ARG == 5)
			{
				print("IT'S TOO HIGH");
			}
			if (ILOC[ARG] != LCL)
			{
				print("IT'S NOT HERE");
			}
			if (IC >= 8)
			{
				print("YOU'RE CARRYING TOO MUCH");
			}
			if (ARG > IMMOFF)
			{
				print("IT'S TOO HEAVY");
			}
			if (LCL == 29 && ARG == 12)
			{
				print("YOU CAN'T DO THAT");
			}
			IC = IC + 1;
			if (LCL == 30 && ARG == 5)
			{
				print("TAKING THE PICTURE REVEALS A FUSEBOX");
				ILOC[ARG] = 0;
				ILOC[IMMOFF + 7] = 30;
				// # GOTO 2500;
			}

			// if player is carrying object, ILOC[obj] == 0
			ILOC[ARG] = 0;
			print(VOCABS[INPTK[2]], ": TAKEN");
		}
		public void FN_6600()
		{
			if (ILOC[ARG] != 0)
			{
				print("YOU AREN'T CARRYING IT");
			}
			IC = IC - 1;
			if (LCL == 17 && ARG == 10 && LocationExit[17, 1] <= 0)
			{
				print("THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT.");
				// # GOTO 6690;
			}
			if (LCL == 17 && ARG == 2 && LocationExit[17, 1] <= 0)
			{
				print("THE DOG CHEWS HIS FAVORITE TOY && IS SOON ASLEEP");
				ILOC[ARG] = -999;
				LocationExit[17, 1] = 18;
				// # GOTO 2500;
			}
			if (LCL == 29 && ARG == 12 && LocationExit[29, 5] <= 0)
			{
				print("THE BOXSPRING COVERS THE GAP IN THE STAIRS");
				ILOC[ARG] = -999;
				LocationExit[29, 5] = 2;
				LocationExit[2, 6] = 29;
				// # GOTO 2500;
			}
			// # 6690;
			ILOC[ARG] = LCL;
			print(VOCABS[INPTK[2]], ": DROPPED");
		}
		public void FN_6700()
		{
			ARG = INPTK[2] - ITEMOFF;
			if (ILOC[ARG] != 0 && ILOC[ARG] != LCL)
			{
				print("IT'S NOT HERE");
			}
			if (ARG == 9 && (LCL == 13 || LCL == 22))
			{
				FN_8000();
				FN_8050();
			}

			// Print extended obj description
			if (IDESCS[ARG] == "")
			{
				print("THERE'S NOTHING SPECIAL ABOUT THE ", VOCABS[INPTK[2]]);
			}
			else
			{
				print(IDESCS[ARG]);
			}
		}
		public void FN_6800_OPEN()
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
		public void FN_8000()
		{
			if (SAFED != 0)
			{
				return;
				// # RETURN;
			}
			int rnd = RNG(3);
			SAFED = (rnd * 3) + 1;
			// 8020 SAFED = INT(RND(3) * 3) + 1: RETURN
			// # RETURN;
		}
		public void FN_8050()
		{
			var N1S = VOCABS[DIROFF + 1];
			var N2S = VOCABS[DIROFF + 3];
			if (SAFED == 1)
			{
				N1S = VOCABS[DIROFF + 2];
			}
			if (SAFED == 3)
			{
				N2S = VOCABS[DIROFF + 2];
			}
			print("EXPERIMENTS ON " + N1S);
			print(" AND " + N2S);
			print(" DOORS PROCEEDING WELL; FILE for PATENT");
		}
		public void FN_6899()
		{
			print("HUH?");
		}
		public void FN_6900()
		{
			if (ILOC[ARG] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			if (ARG != 10)
			{
				print("YOU CAN'T EAT THAT!");
			}
			print("THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER");
			ILOC[ARG] = -2;
			ILOC[17] = 0;
			// # GOTO 2500;
		}
		public void FN_6950()
		{
			AIMM = ARG - IMMOFF;
			if (AIMM >= 1 && AIMM <= 4)
			{
				// #ON AIMM GOTO 6970, 6975, 6980;
				var X = AIMM;
				if (X == 1)
				{
					// #FN_6970();
					print("IT'S TOO HEAVY for YOU TO MOVE");
				}
				else if (X == 2)
				{
					// #FN_6975();
					print("YOUR BACK IS ACTING UP");
				}
				else if (X == 3)
				{
					// #FN_6980();
					print("THAT SEEMS POINTLESS && UNSANITARY");
				}
				else
				{
					print("YOU CAN'T DO THAT");
				}
			}
		}
		public void FN_7000()
		{
			int GOARG = 0;
			GOARG = INPTK[1];
			// convert N,S,E,W,U,D to long form
			if (GOARG > 6)
			{
				GOARG = GOARG - 6;
			}

			if (LocationExit[LCL, GOARG] > 0)
			{
				// new position
				LCL = LocationExit[LCL, GOARG];
				// # GOTO 2500;
			}
			else if (LCL == 12 && GOARG == 5)
			{
				print("YOU'RE AFRAID OF THE DARK");
			}
			else if (LCL == 17 && GOARG == 1)
			{
				print("YOU NEVER DID LIKE THAT DOG");
			}
			else if (LCL == 23 && LocationExit[23, 6] <= 0)
			{
				print("THE DUMBWAITER MECHANISM IS CORRODED && WON'T MOVE");
			}
			else
			{
				print("YOU CAN'T GO THAT WAY");
			}
		}
		public void FN_7100()
		{
			if (COMM < 23 || COMM > 30)
			{
				FN_6899();
			}
			ARG = INPTK[2] - ITEMOFF;
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
				FN_6800_OPEN();
			}
			else if (X == 2)
			{
				FN_6899();
			}
			else if (X == 3)
			{
				FN_6899();
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
			MVARG = INPTK[3] - ITEMOFF;
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
			if (INPTK[3] - ITEMOFF != IMMOFF + 4)
			{
				print("HUH?");
			}
			int DOORDIR = 0;
			DOORDIR = INPTK[2] - DIROFF;
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
			SAFED = (INPTK[2] - DIROFF) + 1;
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
			if (INPTK[2] - ITEMOFF != 6)
			{
				print("YOU CAN'T TIE THAT");
			}
			if (INPTK[3] - ITEMOFF != (IMMOFF + 5))
			{
				print("YOU CAN'T TIE TO THAT");
			}
			if (ILOC[6] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			print("TIED");
			ILOC[6] = -12;
			IC = IC - 1;
			// # GOTO 2500;
		}
		public void FN_7600()
		{
			if (ILOC[8] != 0)
			{
				print("HUH?");
			}
			if (LCL == 18)
			{
				print("THERE IS A FLASH OF LIGHT && A CRACKING SOUND. AN OPENING APPEARS IN THE EAST WALL");
				LocationExit[18, 3] = 19;
				// # GOTO 2500;
			}
			// #INVERSE:;
			print("WHEE!");
			// # NORMAL:;
			print(" ");
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
			if (INPTK[2] - ITEMOFF != IMMOFF + 6)
			{
				print("HUH?");
			}
			print("THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY");
			LocationExit[23, 6] = 24;
		}
		public void FN_7800()
		{
			if (LCL != 30 || (INPTK[2] - ITEMOFF != 3))
			{
				print("YOU CAN'T DO THAT");
			}
			if (INPTK[3] - ITEMOFF != (IMMOFF + 7))
			{
				print("YOU CAN'T PUT IT THERE");
			}
			if (ILOC[3] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			print("YOU PUT THE FUSE IN THE BOX");
			ILOC[3] = -999;
			IC = IC - 1;
			LocationExit[12, 5] = 25;
		}
		public void FN_8200()
		{
			if (LCL == 20 && (INPTK[2] - ITEMOFF) == IMMOFF + 4)
			{
				print("PLEASE SPECIFY LEFT, CENTER, || RIGHT");
			}
			else
			{
				print("HUH?");
			}
			// # RETURN;
		}

		// # REM * START HERE;
		public void FN_GAME()
		{
			FN_15000_INTRO();
			while (true)
			{
				FN_2500_local();

				FN_5000_read_input();
				PrintDgb1(" ");

				FN_5100();
				PrintDgb1(" *** ");
			}
		}

	}
}