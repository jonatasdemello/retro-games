using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


		public void ActionInventory()
		{
			print(" ");
			print("Inventory: YOU ARE CARRYING:");
			for (var i = 1; i < ILOC.Length; i++)
			{
				if (ILOC[i] == 0)
				{
					print(" - ", VOCABS[i + ITEMOFF]);
				}
			}
		}
		public void ActionIntro()
		{
			print(" ");
			print("TAYS HOUSE ADVENTURE");
			print("--------------------");
			print("FIND TREASURES AND VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE");
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
		public void ActionScore()
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

		public void ActionExit()
		{
			print("THANK YOU FOR PLAYING, BYE!");
			Environment.Exit(0);
		}
		public void ActionHuh()
		{
			print("HUH?");
		}

		public void ActionLocation()
		{
			print(" ");
			PrintDgb("LOCATION & DESCRIPTION");
			print("LCL: ", LCL.ToString());
			print("L: ", LocationName_RNAMES[LCL]);
			print("D: ", LocationDescription_RDESCS[LCL]);
			print(" ");
		}
		public void ActionDirections()
		{
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
		}
		public void ActionExtendedDescriptions()
		{
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
					print("THERE IS A " + VOCABS[I + ITEMOFF] + " HERE");
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

		public void ActionTake(int obj)
		{
			// ARG is the object
			ARG = InputWordNum_INPTK[2] - ITEMOFF;

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
			//if (IC >= 8)
			//{
			//	print("YOU'RE CARRYING TOO MUCH");
			//}
			if (ARG > IMMOFF)
			{
				print("IT'S TOO HEAVY");
			}
			if (LCL == 29 && ARG == 12)
			{
				print("YOU CAN'T DO THAT");
			}
			// IC++;

			if (LCL == 30 && ARG == 5)
			{
				print("TAKING THE PICTURE REVEALS A FUSEBOX");
				ILOC[ARG] = 0;
				ILOC[IMMOFF + 7] = 30;
				// # GOTO 2500;
			}

			// if player is carrying object, ILOC[obj] == 0
			ILOC[ARG] = 0;
			print(VOCABS[InputWordNum_INPTK[2]], ": TAKEN");
		}


		public void ActionDrop(int obj)
		{
			if (ILOC[ARG] != 0)
			{
				print("YOU AREN'T CARRYING IT");
			}
			// IC = IC - 1;
			if (LCL == 17 && ARG == 10 && LocationExit[17, 1] <= 0)
			{
				print("THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT.");
				// # GOTO 6690;
			}
			if (LCL == 17 && ARG == 2 && LocationExit[17, 1] <= 0)
			{
				print("THE DOG CHEWS HIS FAVORITE TOY AND IS SOON ASLEEP");
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
			print(VOCABS[InputWordNum_INPTK[2]], ": DROPPED");
		}
		public void ActionLook(int obj)
		{
			ARG = InputWordNum_INPTK[2] - ITEMOFF;
			if (ILOC[ARG] != 0 && ILOC[ARG] != LCL)
			{
				print("IT'S NOT HERE");
			}
			if (ARG == 9 && (LCL == 13 || LCL == 22))
			{
				// FN_8000();
				if (SAFED != 0)
				{
					return;
					// # RETURN;
				}
				int rnd = RNG(3);
				SAFED = (rnd * 3) + 1;
				// 8020 SAFED = INT(RND(3) * 3) + 1: RETURN
				// # RETURN;

				//FN_8050();

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

			// Print extended obj description
			if (IDESCS[ARG] == "")
			{
				print("THERE'S NOTHING SPECIAL ABOUT THE ", VOCABS[InputWordNum_INPTK[2]]);
			}
			else
			{
				print(IDESCS[ARG]);
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
			if (ILOC[ARG] != 0)
			{
				print("YOU DON'T HAVE IT!");
			}
			else if (ARG != 10)
			{
				print("YOU CAN'T EAT THAT!");
			}

			print("THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER");

			ILOC[ARG] = -2;
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
				var X = AIMM;
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
		public void ActionTie()
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

			//IC--;
			// GOTO 2500;
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

		public void PutFuseInFusebox()
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

			//IC--;
			LocationExit[12, 5] = 25;
		}
	}
}
