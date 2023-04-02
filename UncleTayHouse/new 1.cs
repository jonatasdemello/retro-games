// // # --- DISPLAY INTRO ---;
public void FN_15000_INTRO() {
    print(" ");
    print("TAYS HOUSE ADVENTURE");
    print("--------------------");
    print("FIND TREASURES && VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE");
    print("TYPE SIMPLE COMMANDS: NORTH, SOUTH, ETC. TO MOVE (OR JUST 'N', 'S').");
    print("TAKE && DROP, INVENTORY, LOOK, READ, MOVE, && SO ON.");
    print("SOME COMMANDS ARE COMPLEX: 'MOVE THE HUBCAP WITH THE SPANNER'");
    print(" ");
};
// // # -- SCORE --;
public void FN_9500_SCORE() {
    SCORE = 50;
    for (var I=16; I<20; I++) {
        if ( ILOC[I] == 0 ) {
            SCORE = SCORE + 10;
		}
	}
    for (var I=3; I<30; I++) ) {
        for (var J=1; J<6; J++) {
            if ( REXIT[I][J] == -1 ) {
                SCORE = SCORE -5;
			}
		}
	}
    for (var I=0; I<15; I++) ) {
        if ( ILOC[I] == -1 ) {
            SCORE = SCORE - 5;
		}
    print("YOUR SCORE IS "+ SCORE +" OUT OF A POSSIBLE 100");
    }
};
public void FN_11000_PRINT() {
    print(D2S);
};
public void FN_19000_GAME_OVER() {
    D2S = "YOU HAVE DIED";
    print(D2S);
    FN_19999_END_GAME();
};
public void FN_19900_GAME_WON() {
    FN_9500_SCORE();
    if ( SCORE == 100 ) {
        D2S = "YOU HAVE WON THE GAME!";
		print(D2S);
	}
};
public void FN_19999_END_GAME() {
    exit();
};
public void FN_2500() {
    if ( LCL == 30 ) {
        D2S = "... && SPRING BACK";
        print(D2S);
        LCL = 12;
	}
    if ( LCL == 31 ) {
        FN_19900_GAME_WON();
	}
};
public void FN_2510() {
    print(RNAMES[LCL]);
    print(RDESCS[LCL]);
    for (var I=0; I<6; I++) {
        NEIGH = REXIT[LCL][I];
        if ( NEIGH > 0 ) {
            print(VOCABS[I] , ": ", RNAMES[NEIGH]);
		}
	}
    for (var I=0; I<NXDESC; I++) {
        L1 = EXLOC[I][1];
        L2 = EXLOC[I][2];
        if ( LCL == L1 && REXIT[L1][L2] <= 0 ) {
            print(EXDESCS[I]);
		}
	}
    if ( LCL == 17 && REXIT[17][1] > 0 ) {
        D2S="YOUR UNCLE'S DOBERMAN IS SNORING PEACEFULLY";
        print(D2S);
	}
    if ( LCL == 3 && ILOC[6] == -12 ) {
        D2S="A BUNGEE CORD DANGLES FROM THE RAILING ABOVE";
        print(D2S);
	}
    if ( LCL == 12 && ILOC[6] == -12 ) {
        D2S="A BUNGEE CORD DANGLES FROM THE RAILING";
        print(D2S);
	}
    for (var I = 0; I < LASTITEM; I++) ) {
        if ( ILOC[I] == LCL ) {
            print("THERE IS A ", VOCABS[I + ITEMOFF], " HERE");
		}
    if ( LCL == 2 && ILOC[3] == -1 ) {
        D2S = "SOMETHING IS BARELY VISIBLE UNDER THE FRIDGE";
        print(D2S);
	}
    if ( LCL == 3 && ILOC[5] == 30 ) {
        D2S = "THERE IS A PICTURE HIGH UP ON THE WALL";
        print(D2S);
	}
    if ( LCL == 30 ) {
        TURN1 = 1;
	}
};
public void FN_5000() {
    if ( TURN1 != 1 && LCL == 30 ) {
        D2S = "...AND SPRING BACK";
        print(D2S);
        LCL = 12;
        FN_2500();
	}
    // // # --- READ INPUT ---;
    //// # INPUT "] "; I$;
    IS = input("] ");
    TURN1 = 0;
    INWRD = 0;
    WIDX = 0;
    for ( var C=0; C<len(IS); C++) {
        // # CS = mid(IS,C,1);
        CS = IS[C,1];
        if ( CS == " " && INWRD == 1 ) {
            INWRD = 0;
		}
        if ( CS != " " && INWRD == 0 ) {
            WIDX = WIDX + 1;
            INWS[WIDX] = "";
            INWRD = 1;
		}
        if ( WIDX > 10 ) {
            // # GOTO 5100;
            break;
		}
        if ( CS != " " ) {
            INWS[WIDX] = INWS[WIDX] + CS;
		}
};
public void FN_5100() {
    CURTOK = 1;
    for ( VAR TIDX=0; TIDX < WIDX; TIDX++) {
        ISNULLW = 0;
        for ( VAR XN = 0; XN < 4; XN++; ) {
            if ( INWS[TIDX] == NULLWS[XN] ) {
                ISNULLW = 1;
			}
		}
        if ( ISNULLW == 1 ) {
            // # GOTO 5170;
            continue;
		}
        for ( CMDIDX in range(1, LWRD) ) {
            if ( INWS[TIDX] == VOCABS[CMDIDX] ) {
                INPTK[CURTOK] = CMDIDX;
                CURTOK = CURTOK + 1;
			}
		}
	}
    #NEXT TIDX // # 5170;
    NTOK = CURTOK - 1;
    COMM = INPTK[1];
    #ON NTOK+1 // # GOTO 5000, 6050, 6400, 7100;
    X = NTOK+1;
    if ( X == 1 ) { FN_5000(); }
    if ( X == 2 ) { FN_6050(); }
    if ( X == 3 ) { FN_6400(); }
    if ( X == 4 ) { FN_7100(); }
    D2S = "YOU CAN'T DO THAT";
    print(D2S);
    // # GOTO 5000;
    // # RETURN;
};
public void FN_6050() {
    if ( INPTK[1] >= 1 && INPTK[1] <= 12 ) {
        FN_7000();
	}
    if ( COMM <= 20 ) {
        // # ON COMM-12 GOTO 6100, 6100, 6200, 6300, 6350, 6099, 6099, 2500;
        // # "I", "INVENTORY", "SCORE", "JUMP", "HELP", "TAKE", "DROP", "LOOK";
        X = COMM-12;
        if ( X == 1 ) { FN_6100_INV(); }
        if ( X == 2 ) { FN_6100_INV(); }
        if ( X == 3 ) { FN_6200_SCORE(); }
        if ( X == 4 ) { FN_6300_JUMP(); }
        if ( X == 5 ) { FN_6350_HELP(); }
        if ( X == 6 ) { FN_6099(); }
        if ( X == 7 ) { FN_6099(); }
        if ( X == 8 ) { FN_2500(); }
	}
};
public void FN_6099() {
    // # TAKE DROP?;
    D2S = "HUH?";
    print(D2S);
    // # GOTO 5000;
};
public void FN_6100_INV() {
    D2S = "YOU ARE CARRYING:";
    print(D2S);
    for (var I=0; I<LASTITEM; I++) ) {
        if ( ILOC[I] == 0 ) {
            print("  ", VOCABS[I+ITEMOFF]);
		}
	}
    // # GOTO 5000;
};
public void FN_6200_SCORE() {
    FN_9500_SCORE();
    // # GOTO 5000;
};
public void FN_6300_JUMP() {
    if ( LCL != 12 ) {
        D2S = "WHO ARE YOU, DAVID LEE ROTH?";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[6] != -12 ) {
        D2S = "YOU FORGOT YOUR PARACHUTE";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "YOU BUNGEE OFF THE BALCONY...";
    print(D2S);
    LCL = 30;
    // # GOTO 2510;
};
public void FN_6350_HELP() {
    FN_15000_INTRO();
    // # GOTO 5000;
};
public void FN_6400() {
    ARG = INPTK[2] - ITEMOFF;
    if ( ARG < 1 || ARG > LASTITEM ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    if ( COMM > 17 && COMM <= 27 ) {
        // #ON COMM-17 GOTO 6500, 6600, 6700, 6700, 6700, 6800, 6900, 7600, 6950, 8200;
        // # "TAKE", "DROP", "LOOK", "READ", "EXAMINE", "UNLOCK", "EAT", "SPIN", "MOVE", "OPEN", "TIE",;
        X = COMM-17;
        if ( X == 1 ) { FN_6500(); }
        if ( X == 2 ) { FN_6600(); }
        if ( X == 3 ) { FN_6700(); }
        if ( X == 4 ) { FN_6700(); }
        if ( X == 5 ) { FN_6700(); }
        if ( X == 6 ) { FN_6800(); }
        if ( X == 7 ) { FN_6900(); }
        if ( X == 8 ) { FN_7600(); }
        if ( X == 9 ) { FN_6950(); }
        if ( X == 10 ) { FN_8200(); }
	}
    D2S = "HUH?";
    print(D2S);
    // # GOTO 5000;
};
public void FN_6500() {
    if ( ILOC[ARG] == 0 ) {
        D2S = "YOU ALREADY HAVE IT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[ARG] == 30 && LCL == 3 && ARG == 5 ) {
        D2S = "IT'S TOO HIGH";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[ARG] != LCL ) {
        D2S = "IT'S NOT HERE";
        print(D2S);
        // # GOTO 5000;
	}
    if ( IC >= 8 ) {
        D2S = "YOU'RE CARRYING TOO MUCH";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ARG > IMMOFF ) {
        D2S = "IT'S TOO HEAVY";
        print(D2S);
        // # GOTO 5000;
	}
    if ( LCL == 29 && ARG == 12 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    IC = IC + 1;
    if ( LCL == 30 && ARG == 5 ) {
        D2S = "TAKING THE PICTURE REVEALS A FUSEBOX";
        print(D2S);
        ILOC[ARG] = 0;
        ILOC[IMMOFF+7] = 30;
        // # GOTO 2500;
	}
    ILOC[ARG] = 0;
    print(VOCABS[INPTK[2]], ": TAKEN");
    // # GOTO 5000;
};
public void FN_6600() {
    if ( ILOC[ARG] != 0 ) {
        D2S = "YOU AREN'T CARRYING IT";
        print(D2S);
        // # GOTO 5000;
	}
    IC = IC - 1;
    if ( LCL == 17 && ARG == 10 && REXIT[17][1] <= 0 ) {
        D2S = "THE DOG LOOKS DISGUSTED. MAYBE YOU SHOULD EAT IT.";
        print(D2S);
        // # GOTO 6690;
	}
    if ( LCL == 17 && ARG == 2 && REXIT[17][1] <= 0 ) {
        D2S = "THE DOG CHEWS HIS FAVORITE TOY && IS SOON ASLEEP";
        print(D2S);
        ILOC[ARG] = -999;
        REXIT[17][1] = 18;
        // # GOTO 2500;
	}
    if ( LCL == 29 && ARG == 12 && REXIT[29][5] <= 0 ) {
        D2S = "THE BOXSPRING COVERS THE GAP IN THE STAIRS";
        print(D2S);
        ILOC[ARG] = -999;
        REXIT[29][5] = 2;
        REXIT[2][6] = 29;
        // # GOTO 2500;
	}
    // # 6690;
    ILOC[ARG] = LCL;
    print(VOCABS[INPTK[2]], ": DROPPED");
    // # GOTO 5000;
};
public void FN_6700() {
    ARG = INPTK[2] - ITEMOFF;
    if ( ILOC[ARG] != 0 && ILOC[ARG] != LCL ) {
        D2S = "IT'S NOT HERE";
        print(D2S);
         // # GOTO 5000;
	}
    if ( ARG == 9 && (LCL == 13 || LCL == 22) ) {
        FN_8000();
        FN_8050();
        // # GOTO 5000;
	}
    if ( IDESCS[ARG] == "" ) {
        print("THERE'S NOTHING SPECIAL ABOUT THE ", VOCABS[INPTK[2]]);
        // # GOTO 5000;
	}
    print(IDESCS[ARG]);
    // # GOTO 5000;
};
#_UNLOCK() {
public void FN_6800() {
    if ( ILOC[7] != 0 ) {
        D2S = "YOU DON'T HAVE A KEY!";
        print(D2S);
         // # GOTO 5000;
	}
    if ( LCL == 5 ) {
        D2S = "THE KEY DOESN'T FIT THE LOCK";
        print(D2S);
         // # GOTO 5000;
	}
    if ( LCL == 17 ) {
        D2S = "YOU UNLOCK THE DOOR. BEWARE!";
        print(D2S);
        REXIT[17][4] = 20;
        // # GOTO 2500;
	}
};
public void FN_8000() {
    if ( SAFED != 0 ) {
        return;
        // # RETURN;
	}
    rnd = random.randint(0, 3);
    SAFED = int(rnd * 3) + 1;
    // # RETURN;
};
public void FN_8050() {
    N1S = VOCABS[DIROFF+1];
    N2S = VOCABS[DIROFF+3];
    if ( SAFED == 1 ) { N1S = VOCABS[DIROFF+2]; }
    if ( SAFED == 3 ) { N2S = VOCABS[DIROFF+2]; }
    D2S = "EXPERIMENTS ON ";
    print(D2S);
    NTMSGS = D2S + N1S;
    D2S = " && ";
    print(D2S);
    NTMSGS = NTMSGS + D2S + N2S;
    D2S = " DOORS PROCEEDING WELL; FILE for PATENT";
    print(D2S);
    NTMSGS = NTMSGS + D2S;
    print(NTMSGS);
    // # RETURN;
};
#_HUH() {
public void FN_6899() {
    D2S = "HUH?";
    print(D2S);
    // # GOTO 5000;
};
#_EAT() {
public void FN_6900() {
    if ( ILOC[ARG] != 0 ) {
        D2S = "YOU DON'T HAVE IT!";
        print(D2S);
         // # GOTO 5000;
	}
    if ( ARG != 10 ) {
        D2S = "YOU CAN'T EAT THAT!";
        print(D2S);
         // # GOTO 5000;
	}
    D2S = "THERE WAS A DIAMOND HIDDEN INSIDE THE GAINESBURGER";
    print(D2S);
    ILOC[ARG] = -2;
    ILOC[17] = 0;
    // # GOTO 2500;
};
#_SPIN() {
public void FN_6950() {
    AIMM = ARG - IMMOFF;
    if ( AIMM >= 1 && AIMM <= 4 ) {
        // #ON AIMM GOTO 6970, 6975, 6980;
        X = AIMM;
        if ( X == 1 ) {
            // #FN_6970();
            D2S = "IT'S TOO HEAVY for YOU TO MOVE";
		}
        if ( X == 2 ) {
            // #FN_6975();
            D2S = "YOUR BACK IS ACTING UP";
		}
        if ( X == 3 ) {
            // #FN_6980();
            D2S = "THAT SEEMS POINTLESS && UNSANITARY";
		}
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "YOU CAN'T DO THAT";
    print(D2S);
    // # GOTO 5000;
};
public void FN_7000() {
    GOARG = INPTK[1];
    if ( GOARG > 6 ) { 
		GOARG = GOARG - 6; 
	}
    if ( REXIT[LCL][ GOARG] > 0 ) {
        LCL = REXIT[LCL][ GOARG];
        // # GOTO 2500;
	}
    if ( LCL == 12 && GOARG == 5 ) {
        D2S = "YOU'RE AFRAID OF THE DARK";
        print(D2S);
        // # GOTO 5000;
	}
    if ( LCL == 17 && GOARG == 1 ) {
        D2S = "YOU NEVER DID LIKE THAT DOG";
        print(D2S);
        // # GOTO 5000;
	}
    if ( LCL == 23 && REXIT[23][ 6] <= 0 ) {
        D2S = "THE DUMBWAITER MECHANISM IS CORRODED && WON'T MOVE";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "YOU CAN'T GO THAT WAY";
    print(D2S);
    // # GOTO 5000;
};
public void FN_7100() {
    if ( COMM < 23 || COMM > 30 ) {
        FN_6899();
	}
    ARG = INPTK[2] - ITEMOFF;
    if ( COMM != 27 && ARG < 1 || ARG > LASTITEM ) {
        D2S = "HUH?";
        print(D2S);
         // # GOTO 5000;
	}
    if ( COMM != 23 && COMM != 29 && ILOC[ARG] != LCL && ILOC[ARG] != 0 ) {
        D2S = "IT'S NOT HERE";
        print(D2S);
         // # GOTO 5000;
	}
    #ON COMM-22 GOTO 6800, 6899, 6899, 7200, 7400, 7500, 7700, 7800;
    X = COMM-22;
    if ( X == 1 ) { FN_6800(); }
    if ( X == 2 ) { FN_6899(); }
    if ( X == 3 ) { FN_6899(); }
    if ( X == 4 ) { FN_7200(); }
    if ( X == 5 ) { FN_7400(); }
    if ( X == 6 ) { FN_7500(); }
    if ( X == 7 ) { FN_7700(); }
    if ( X == 8 ) { FN_7800(); }
};
public void FN_7200() {
    if ( ARG < IMMOFF ) {
        D2S = "YOU CAN JUST TAKE THAT";
        print(D2S);
         // # GOTO 5000;
	}
    AIMM = ARG - IMMOFF;
    MVARG = INPTK[3] - ITEMOFF;
    if ( AIMM < 1 || AIMM > 3 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
         // # GOTO 5000;
	}
    if ( ILOC[MVARG] != 0 ) {
        D2S = "YOU DON'T HAVE IT!";
        print(D2S);
         // # GOTO 5000;
	}
    #ON AIMM GOTO 7250, 7300, 7350;
    X = AIMM;
    if ( X == 1 ) { FN_7250(); }
    if ( X == 2 ) { FN_7300(); }
    if ( X == 3 ) { FN_7350(); }
};
public void FN_7250() {
    if ( MVARG != 4 || ILOC[3] >= 0 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
         // # GOTO 5000;
	}
    D2S = "YOU JACK UP THE FRIDGE && FIND A FUSE UNDER IT";
    print(D2S);
    ILOC[3] = LCL;
    // # GOTO 2500;
};
public void FN_7300() {
    if ( MVARG != 13 || ILOC[2] >= 0 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "YOU MOVE THE COUCH && FIND A TEDDYBEAR BEHIND IT";
    print(D2S);
    ILOC[2] = LCL;
    // # GOTO 2500;
};
public void FN_7350() {
    if ( MVARG != 11 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    D2S="MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT";
    print(D2S);
    REXIT[LCL][6] = 27;
    // # GOTO 2500;
};
public void FN_7400() {
    if ( LCL != 20 ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    if ( INPTK[3] - ITEMOFF != IMMOFF + 4 ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    DOORDIR = INPTK[2] - DIROFF;
    if ( DOORDIR < 1 || DOORDIR > 3 ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    FN_8025();
    if ( DOORDIR == SAFED ) {
        D2S = "OPENING THE DOOR REVEALS A DUMBWAITER";
        print(D2S);
        REXIT[LCL][4] = 23;
        // # GOTO 2500;
	}
    rnd = random.randint(0,2);
    if ( rnd > 1 ) {
        D2S = "A SHOT RINGS OUT! IT WAS WELL-AIMED TOO.";
        print(D2S);
        // # GOTO 19000;
	}
    D2S = "AN IRONING BOARD SLAMS ONTO YOUR HEAD";
    print(D2S);
    // # GOTO 19000;
};
public void FN_8025() {
    if ( SAFED != 0 ) {
        return;
        // # RETURN;
	}
    SAFED = (INPTK[2] - DIROFF) + 1;
    if ( SAFED > 3 ) { 
		SAFED = 1;
	}
    // # RETURN;
};
public void FN_7500() {
    if ( LCL != 12 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( INPTK[2] - ITEMOFF != 6 ) {
        D2S = "YOU CAN'T TIE THAT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( INPTK[3] - ITEMOFF != (IMMOFF + 5) ) {
        D2S = "YOU CAN'T TIE TO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[6] != 0 ) {
        D2S = "YOU DON'T HAVE IT!";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "TIED";
    print(D2S);
    ILOC[6] = -12;
    IC = IC - 1;
    // # GOTO 2500;
};
public void FN_7600() {
    if ( ILOC[8] != 0 ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    if ( LCL == 18 ) {
        D2S = "THERE IS A FLASH OF LIGHT && A CRACKING SOUND. AN OPENING APPEARS IN THE EAST WALL";
        print(D2S);
        REXIT[18][3] = 19;
        // # GOTO 2500;
	}
    // #INVERSE:;
    D2S = "WHEE!";
    print(D2S);
    // # NORMAL:;
    print(" ");
    // # GOTO 5000;
};
public void FN_7700() {
    if ( LCL != 20 ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[15] != 0 ) {
        D2S = "YOU DON'T HAVE ANY OIL";
        print(D2S);
        // # GOTO 5000;
	}
    if ( INPTK[2] - ITEMOFF != IMMOFF + 6 ) {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY";
    print(D2S);
    REXIT[23][6] = 24;
    // # GOTO 5000;
};
public void FN_7800() {
    if ( LCL != 30 || (INPTK[2] - ITEMOFF != 3) ) {
        D2S = "YOU CAN'T DO THAT";
        print(D2S);
        // # GOTO 5000;
	}
    if ( INPTK[3] - ITEMOFF != (IMMOFF + 7) ) {
        D2S = "YOU CAN'T PUT IT THERE";
        print(D2S);
        // # GOTO 5000;
	}
    if ( ILOC[3] != 0 ) {
        D2S = "YOU DON'T HAVE IT!";
        print(D2S);
        // # GOTO 5000;
	}
    D2S = "YOU PUT THE FUSE IN THE BOX";
    print(D2S);
    ILOC[3] = -999;
    IC = IC - 1;
    REXIT[12][5] = 25;
    // # GOTO 5000;
};
public void FN_8200() {
    if ( LCL == 20 && (INPTK[2] - ITEMOFF) == IMMOFF + 4 ) {
        D2S = "PLEASE SPECIFY LEFT, CENTER, || RIGHT";
        print(D2S);
        // # GOTO 5000;
	}
    else {
        D2S = "HUH?";
        print(D2S);
        // # GOTO 5000;
	}
	// # RETURN;
};
// # REM ******************;
// # REM * START HERE;
// # REM ******************;
public void FN_GAME() {
    // #clear;
    FN_15000_INTRO();
    while (true) {
        FN_2500();
        FN_2510();
        FN_5000();
        FN_5100();
	}
	FN_15000_INTRO();
	FN_2500();
	FN_2510();
	// # FN_5000();
	// # FN_5100();
};
