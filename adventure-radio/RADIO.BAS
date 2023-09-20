5 CLS:PRINT"The Case at KAXL":PRINT:PRINT"   This Adventure is destributed as Shareware. It's distribution through BBS's, PD Disks, and other public domain vehicles is encouraged. Ownership of the      Program is reserved in Douglas C. Rogers"
6 PRINT:PRINT"   You are invited to call the home BBS that created this adventure:":PRINT"ADVENTURELAND":PRINT:PRINT"(606) 271-0558":PRINT"300/1200 Baud 24 hrs a day":PRINT:PRINT"Many more adventure lie in wait for you there."
8 IF INKEY$="" THEN 8
10 NNNN=VAL(RIGHT$(TIME$,2)):RANDOMIZE NNNN:DIM D$(3),L$(28),L(28,3),O(60),O$(60),NO$(60),VB$(17):CLS:Q=1:DEF SEG=&H40:POKE &H17,PEEK(&H17) OR 64:INPUT"WHAT IS YOUR FIRST NAME";N$:IF N$="" THEN 10 ELSE N$=N$+".RAD"
20 CLS:PRINT TAB(30)"The Case at KAXL":LOCATE 24,1:PRINT TAB(20)"Copyright (c) 1982 by Doug Rogers";
30 RESTORE:X=0:L=22:Z1=0:Z=0:M=0:H=0:NO=60:NV=17:NN=NO:AU=120:CC=0:BF=0:FOR X= 0 TO 3:READ D$(X):NEXT:FOR X=1 TO 28:READ L$(X),L(X,0),L(X,1),L(X,2),L(X,3):NEXT
40 FOR X=1 TO NO:READ O$(X),O(X):NEXT:FOR X=1 TO NV:READ VB$(X):NEXT:FOR X=1 TO NN:READ NO$(X):NEXT:M$="00":H$="12":ID=1:L$="It's locked...":OP$="It's open...":CP=0
50 LOCATE 2,5:PRINT"A few minutes ago, you got a frantic phone call from your buddy Mike Mouth,the Disk Jockey. He was cut off in mid-sentence. You have just arrived at the Radio Station to investigate..."
60 PRINT"   If you've never been in a Radio Station before, we suggest that you look around carefully. There is a lot of equipment in the place, and some of it couldbe of use to you."
70 PRINT"   Note also that Radio Station managers and Program Directors are notorious memo writers and note posters. You can learn alot (that you'll need to know) about running a radio station just by keeping your eyes open."
80 PRINT:PRINT"   Some words I know that should be useful to you are LISTEN, GET, PUT, READ, EXAMINE, HELP, USE, INVENTORY (tells what you are holding), SAVE, LOAD, WAIT, and any other VERB-NOUN";
90 PRINT"combinations that seems appropriate.":PRINT"If one combination doesn't work, try another! I have a large vocabulary. Press <ENTER> to begin"
100 IF INKEY$="" THEN 100 ELSE CLS
110 IF CC<>0 THEN 490 ELSE AU=AU-1:IF AU<-6 THEN PH=1:IF AU<-10 THEN CC=1
120 LOCATE 1,1:PRINT"You are ";L$(L);STRING$(80-POS(0),32);:PRINT"You see: ";:FL=0:FOR X=1 TO NO:IF O(X)=L THEN 130 ELSE 150
130 IF POS(0)+LEN(O$(X))+3>80 THEN PRINT STRING$(80-POS(0),32);:PRINT"         ";
140 PRINT O$(X);". ";:FL=1
150 NEXT:IF FL=0 THEN PRINT"NOTHING";STRING$(80-POS(0),32);ELSE PRINT STRING$(80-POS(0),32);
160 IF SF=1 THEN SF=0:PRINT"There's a wisp of smoke in the air...";:IF G=0 THEN G=1:PRINT" moving, as if someone just ran out of the room...";STRING$(80-POS(0),32);:ELSE PRINT STRING$(80-POS(0),32);
170 FL=0:FOR X=0 TO 3:IF L(L,X)<>0 THEN FL=1
180 NEXT:IF FL=0 THEN 240 ELSE PRINT"YOU CAN GO: ";:FOR X=0 TO 3:IF L(L,X)<>0 THEN PRINT D$(X);
190 NEXT:PRINT STRING$(80-POS(0),32);:M=VAL(M$):M=M+1:IF M>59 THEN M$="00" ELSE M$=STR$(M):IF LEN(M$)<3 THEN M$="0"+RIGHT$(M$,1) ELSE M$=RIGHT$(M$,2)
200 H=VAL(H$):IF M>59 THEN H=H+1:ID=0:IF H>12 THEN H=1
210 H$=RIGHT$(STR$(H),2):PRINT"The time is "H$;":";M$;STRING$(80-POS(0),32);:IF ID=0 AND M=5 THEN PH=1
220 IF CP=0 THEN IF PH=1 THEN PRINT"There is a phone ringing somewhere...";STRING$(80-POS(0),32);:IF M=13 THEN CC=1
230 IF H=4 THEN GOSUB 520 ELSE IF H=5 THEN CC=1
240 PRINT STRING$(63,"-"):AN$="":VB=0:NB=0:PRINT"WHAT WOULD YOU LIKE TO DO";:HP=POS(0):VP=CSRLIN:PRINT STRING$(80-POS(0),32):GOSUB 1730:LOCATE VP,HP:STORAGE=VP:INPUT AN$:IF AN$="" THEN PRINT CHR$(30);CHR$(30);:GOTO 240
250 PRINT STRING$(240,32):PRINT STRING$(240,32):PRINT STRING$(240,32):LOCATE VP+1,1:IF L=22 AND (AN$="GO OUT" OR AN$="GO DOOR") THEN CC=2:GOTO 110
260 Z1=LEN(AN$):Z=INSTR(AN$," "):IF Z<>0 THEN 360 ELSE IF AN$="WAIT" THEN 1160
270 IF AN$="SCORE" THEN 580 ELSE IF LEFT$(AN$,3)="INV" THEN 320 ELSE IF AN$="QUIT" THEN 420 ELSE IF AN$="HELP" THEN PRINT"You might try examining EVERYTHING...":GOTO 110 ELSE IF AN$="SAVE" THEN 430 ELSE IF AN$="LOAD" THEN 460 ELSE IF AN$="LISTEN" THEN 880
280 FOR X=0 TO 3:IF AN$<>LEFT$(D$(X),1) THEN 300 ELSE IF L(L,X)=0 THEN 310 ELSE L=L(L,X):X=3:IF G=0 THEN SF=1
290 IF (H=4 OR H=5) THEN 110 ELSE IF INT(RND*12)=11 THEN SF=1:GOTO 110 ELSE 110
300 NEXT:PRINT"I'm afraid I don't understand what you want...":FOR X=1 TO 2000:NEXT:GOTO 110
310 PRINT"You can't go that way":GOTO 110
320 FL=0:PRINT"YOU ARE HOLDING:":FOR X=1 TO NO:IF O(X)<>-1 THEN 340 ELSE FL=1:IF 80-(POS(0)+2)< LEN(O$(X)) THEN PRINT STRING$(80-POS(0),32)
330 PRINT O$(X);". ";
340 NEXT:IF FL=0 THEN PRINT"NOTHING AT ALL" ELSE PRINT
350 GOTO 1710
360 VB$="":NO$="":VB$=LEFT$(AN$,3):NO$=MID$(AN$,Z+1,3):FOR X=1 TO NV:IF VB$(X)=VB$ THEN VB=X:X=NV ELSE NEXT:PRINT"I don't know how to ";LEFT$(AN$,Z-1);" something":GOTO 110
370 IF VB<>1 THEN FOR X=1 TO NN:IF NO$(X)=NO$ THEN NB=X:X=NN ELSE NEXT:PRINT"I don't know what a ";RIGHT$(AN$,LEN(AN$)-Z);" is.":GOTO 110
380 IF VB>2 THEN 410 ELSE IF VB=1 THEN 1140
390 IF NB=5 OR NB=6 OR NB=7 OR NB=9 OR NB=12 OR NB=14 OR NB=30 OR NB=31 OR NB=40 THEN 1180
400 IF O(NB)=-1 THEN 1180 ELSE IF O(NB)=L THEN 1180 ELSE 1290
410 ON VB GOTO 20,20,1110,1110,1130,1130,1070,1030,1030,900,1160,840,720,660,610,570,540
420 INPUT"WANT TO PLAY AGAIN";AN$:IF LEFT$(AN$,1)="Y" THEN 20 ELSE END
430 OPEN"O",1,N$
440 FOR X=1 TO NO:PRINT #Q,O(X):NEXT:PRINT #Q,L,CC,AU,ID,CP,H$:PRINT #Q,M$:CLOSE
450 PRINT"GAME SAVED":GOTO 110
460 OPEN"I",1,N$
470 FOR X=1 TO NO:INPUT #Q,O(X):NEXT:INPUT #Q,L,CC,AU,ID,CP,H$,M$:CLOSE
480 GOTO 110
490 CLS:IF CC=2 THEN PRINT"The police have just arrived..." ELSE PRINT"The police just broke the front door in..."
500 SB=1:GOSUB 580:IF SC<90 THEN PRINT"You keep telling them you didn't do it, but they aren't listening. After all, there's the body, and there you are without enough evidence to the contrary...":SB=0:GOTO 420
510 PRINT"They see all your evidence, and ask you to lead them to the murderer...":CP=1:CC=0:GOTO 1710
520 IF O(40)=0 THEN O(39)=0:GF=1
530 RETURN
540 IF L<>19 THEN 550 ELSE PRINT L$:GOTO 110
550 IF L<>25 THEN 560 ELSE IF O(43)<>L THEN PRINT L$:GOTO 110 ELSE IF O(55)<>-1 THEN 1170 ELSE O(43)=0:O(42)=L:L(25,0)=0:GOTO 110
560 IF L<>22 THEN 1290 ELSE IF O(41)<>L THEN PRINT L$:GOTO 110 ELSE IF O(55)<>-1 THEN 1170 ELSE O(41)=0:O(40)=L:GOTO 110
570 IF L<>26 THEN PRINT"There is nothing here to climb...":GOTO 110 ELSE PRINT"You slip on the cold metal and fall to your death!":GOTO 420
580 SC=0:FOR X=44 TO 48:GOSUB 600:NEXT:X=50:GOSUB 600:FOR X=58 TO 60:GOSUB 600:NEXT:SC=SC+BF
590 PRINT"You have found";SC;CHR$(28);"% of the evidence.":FOR X=1 TO 1000:NEXT:IF SB=1 THEN RETURN ELSE 110
600 IF O(X)=-1 THEN SC=SC+10:RETURN ELSE RETURN
610 IF L<>19 THEN 640 ELSE IF O(55)<>-1 THEN 1170 ELSE IF GF=1 THEN 630 ELSE IF CP=1 THEN PRINT"OK... Susan's inside with the knife. She sees the cop and gives up. You've solved it all!":GOTO 420 ELSE PRINT"As the door opens the murderer stabs YOU! ";
620 PRINT"You are dead!":GOTO 420
630 PRINT"The stall is empty. She got away. But with all your evidence, you've cleared yourself. Better luck next time.":GOTO 420
640 IF L<>25 THEN 650 ELSE IF O(42)<>L THEN PRINT OP$ ELSE IF O(55)<>-1 THEN 1170 ELSE O(42)=0:O(43)=L:L(25,0)=26:GOTO 110
650 IF L<>22 THEN 1290 ELSE IF O(40)<>L THEN PRINT OP$ ELSE IF O(55)<>-1 THEN 1170 ELSE O(40)=0:O(41)=L:GOTO 110
660 IF NB<>22 THEN 670 ELSE IF O(NB)=L THEN CLS:PRINT"Z  A  P  !":PRINT:PRINT:PRINT"THE HIGH VOLTAGE KILLED YOU!":GOTO 420 ELSE 1290
670 IF NB<>31 THEN 680 ELSE IF L=17 THEN PRINT OP$:GOTO 110 ELSE IF L=19 THEN PRINT L$:GOTO 110
680 IF NB<>40 THEN 1150 ELSE IF L=19 THEN PRINT L$ ELSE IF L<>22 THEN 700 ELSE IF O(41)=L THEN PRINT OP$ ELSE IF O(40)=L THEN PRINT L$
690 GOTO 110
700 IF L=25 THEN IF O(42)=L THEN PRINT L$ ELSE PRINT OP$
710 GOTO 110
720 IF NB=5 OR NB=6 OR NB=7 OR NB=9 OR NB=14 OR NB=40 THEN PRINT"How?":GOTO 110
730 IF O(NB)<>L AND O(NB)<>-1 THEN 1290 ELSE IF NB=3 OR NB=4 THEN 1030 ELSE IF NB<13 THEN 740 ELSE IF NB=13 THEN 760 ELSE IF NB<24 THEN 740 ELSE IF NB=24 THEN 780 ELSE IF NB<34 THEN 740 ELSE IF NB=34 THEN 790 ELSE IF NB<39 THEN 740 ELSE IF NB=39 THEN 800
740 IF NB<55 THEN PRINT"I don't think that will help...":GOTO 110
750 IF NB=55 THEN 810 ELSE IF NB<57 THEN 740 ELSE IF NB=57 THEN 820 ELSE IF NB=58 THEN 830 ELSE 740
760 INPUT"Who do you wish to call";AN$:IF INSTR(AN$,"COP") OR INSTR(AN$,"POL") THEN 770 ELSE 740
770 PRINT"The police are on the way...":CC=2:GOTO 1580
780 PRINT"You sure are out of practice!":GOTO 1580
790 PRINT"A-h-h-h-h-h":GOTO 110
800 PRINT"That's stealing!":GOTO 110
810 PRINT"I think you want to lock or unlock something":GOTO 1580
820 PRINT"Try ";CHR$(34);"THREAD REEL";CHR$(34):GOTO 1580
830 PRINT"OK... You look a little strange...":GOTO 1580
840 IF AX<>0 THEN PRINT"It's the Program Director wanting to know why the stationis off the air. ";:GOTO 870
850 IF ID=0 THEN PRINT"It's the Program Director wanting to know why the Station ID didn't play this hour. ";:GOTO 870
860 PRINT"Wrong number":GOTO 110
870 PRINT"He hung up when he heard your voice...":CC=1:GOTO 1570
880 IF AX=0 THEN PRINT"I hear music playing all over the station":GOTO 1580
890 PRINT"The station is silent.":GOTO 1580
900 IF NB=2 THEN 910 ELSE IF NB=14 THEN 1310 ELSE IF NB=44 THEN 930E LSE IF NB=46 THEN 940 ELSE IF NB=47 THEN 970 ELSE IF NB=50 THEN 990 ELSE IF NB=51 THEN 1000 ELSE IF NB=52 THEN 1010 ELSE IF NB=59 THEN 1020 ELSE PRINT"I see nothing to read here.":GOTO 1580
910 PRINT"It says: All announcers! Remember! The Station ID recorder is broken! We MUST make the ID manually at the beginning of each hour! NO EXCEPTIONS! Engineering has rigged the machine so we can give ";
920 PRINT"the ID by pushing":PRINT"        the RED button. But you'll also then have to restart the music by pressing the GREEN button! Don't Blow this!                                        Steve":GOTO 1710
930 IF O(44)<>-1 AND O(44)<>L THEN 1290 ELSE 1410
940 IF O(46)<>-1 AND O(46)<>L THEN 1290 ELSE IF O(47)<>-1 AND O(47)<>L THEN PRINT"It's the top half of a letter... It says:"ELSE PRINT"The two pieces read:"
950 PRINT"Dear Susan, I don't know an easy way to say this... We have meant so much to each other. You have been a really great kid, and I've really had a blast being with you, but";
960 IF O(47)<>-1 AND O(47)<>L THEN PRINT"AND THE REST IS MISSING...":GOTO 1710
970 IF O(47)<>-1 AND O(47)<>L THEN 1290 ELSE IF O(46)<>-1 AND O(46)<>L THEN PRINT"It's the botton half of a letter. It says:"
980 PRINT TAB(45);" the time has now come when we must part. I hope you will forgive me. Mike":GOTO 1710
990 IF O(50)<>-1 AND O(50)<>L THEN 1290 ELSE 1460
1000 IF O(51)<>-1 AND O(51)<>L THEN 1290 ELSE 1470
1010 IF O(52)<>-1 AND O(52)<>L THEN 1290 ELSE 1490
1020 IF O(59)<>-1 AND O(59)<>L THEN 1290 ELSE 1550
1030 IF L<>1 THEN 1590
1040 IF NB=3 THEN AX=AU:AU=0:ID=1:PRINT"The speaker says: This is KAXL, Compuberg":GOTO 1580
1050 IF NB=4 THEN AU=AX:PRINT"The music has started again":AX=0:GOTO 1580
1060 PRINT"Why would you want to do that???":GOTO 1580
1070 IF L<>23 THEN 1590
1080 IF NB<>57 THEN PRINT"You can only thread REELs on the Automation...":GOTO 1580
1090 IF O(57)<>-1 THEN PRINT"What are you going to thread onto the machine? You don't have any tape...":GOTO 1570
1100 O(57)=0:AU=181:GOTO 1160
1110 IF NB<44 THEN 1150 ELSE IF O(NB)=-1 THEN PRINT"You already have the ";O$(NB) ELSE IF O(NB)<>L THEN 1290 ELSE O(NB)=-1:GOTO 1160
1120 GOTO 1580
1130 IF O(NB)<>-1 THEN PRINT"You don't have the ";O$(NB):GOTO 1580 ELSE O(NB)=L:GOTO 1580
1140 FOR X=0 TO 3:IF NO$=LEFT$(D$(X),3) THEN AN$=LEFT$(NO$,1):X=3:GOTO 280 ELSE NEXT:PRINT"HUH?":GOTO 110
1150 PRINT"It is beyond my power to do that":GOTO 110
1160 PRINT"OK":GOTO 110
1170 PRINT"I can't do that...yet...":GOTO 110
1180 ON NB GOTO 1200,1210,1220,1220,1260,1270,1280,1230,1240,1250,20,1300,1220,1310,1330,1340,20,20,20,1350,20,1230,20,1360,20,20,1220,1220,20,1370,1380,20,20,1220,20,20,1390,1400,1220,1220,20,20,20
1190 ON NB-43 GOTO 1410,1420,1430,1440,1450,1220,1460,1470,1490,1500,1510,1520,1220,1530,1540,1550,1560
1200 PRINT"It's Mike all right... and he's got a knife wound in the back!":BF=10:IF O(60)<>-1 THEN O(60)=L:GOTO 1570 ELSE 1570
1210 PRINT"There's something there all right...":GOTO 110
1220 PRINT"I see nothing unusual":GOTO 110
1230 PRINT"There's too many switches and controls here for me to understand... leave it alone!":GOTO 1570
1240 IF L=3 OR L=4 OR L=12 THEN 1220 ELSE 1290
1250 IF O(45)<>-1 THEN O(45)=L:GOTO 1210 ELSE 1220
1260 IF L=1 OR L=7 OR L=12 OR L=13 THEN 1220 ELSE 1290
1270 IF L=1 OR L=8 THEN 1220 ELSE IF L=7 THEN IF O(48)<>-1 THEN PRINT"There's a record there...":O(48)=L:GOSUB 1580:GOTO 110 ELSE 1220 ELSE 1290
1280 IF L=1 OR L=7 OR L=2 THEN 1230 ELSE 1690
1290 PRINT"I don't see it here...":GOTO 110
1300 IF L=4 THEN 1220 ELSE IF L=14 THEN IF O(51)<>-1 THEN O(51)=L:GOTO 1210 ELSE 1220 ELSE 1290
1310 IF L=4 THEN PRINT"It says: Welcome to KAXL Please see receptionist for tour":GOTO 1570 ELSE IF L=22 THEN PRINT"It says: NO VISITORS AFTER 5 PM":GOTO 1570 ELSE IF L=23 THEN PRINT"Electric sign says: ";AU;"minutes till tape runout":GOTO 1570
1320 GOTO 1290
1330 IF O(47)<>-1 THEN O(47)=L:GOTO 1210 ELSE 1220
1340 IF O(46)<>-1 THEN O(46)=L:GOTO 1210 ELSE 1220
1350 PRINT"There must be thousands of old hits here!":FOR X=1 TO 3000:NEXT:IF O(49)<>-1 THEN PRINT"H-m-m-m...";:FOR X=1 TO 1000:NEXT:PRINT" and one empty sleeve... Seems a record is gone.":O(49)=L:GOTO 1570 ELSE 110
1360 PRINT"Looks kinda old... but I'm sure it still works.":GOTO 1570
1370 IF L=17 THEN 1220 ELSE IF L=19 THEN PRINT"There's a light red stain here...":GOTO 1580 ELSE 1290
1380 IF L=17 THEN PRINT OP$:GOTO 110 ELSE IF L=19 THEN PRINT L$:GOTO 110 ELSE 1290
1390 PRINT"It's a LONG WAY UP>>>>>>>>!":GOTO 1580
1400 PRINT"It's too high to climb... and there's no gate...":GOTO 1570
1410 PRINT"The notepad says: @ 11 remember to open door for Susan get record call cleaners @ 8 am Maybe it means something to you, but I don't understand...":GOTO 1710
1420 PRINT"It's a ";CHR$(34);"Virginia Slims";CHR$(34);" and there's lipstick on it.":GOTO 1570
1430 PRINT"It's the bottom half of a torn sheet.":GOTO 1580
1440 PRINT"It's the top half of a torn sheet.":GOTO 1580
1450 PRINT"It's broken... it says: ";CHR$(34);"MISTY";CHR$(34);"on the label...":GOTO 1570
1460 PRINT"It's the transmitter log. Mike's last entry was at 11:00 That could help establish time of death...":GOTO 1570
1470 PRINT"It's a memo from the manager: All Staff! This business with visitors after business hours is going to STOP! It's DANGEROUS to let people in this place! I'm going to can the next guy who pulls"
1480 PRINT"this! Steve":GOTO 1710 
1490 PRINT"It's a ratings book that shows the station to be number 1":GOTO 1570
1500 PRINT"They're too big to go in your radio...":GOTO 1580
1510 PRINT"I don't see a thing that's useful...":GOTO 1580
1520 PRINT"It's a man's keyring with several keys...":GOTO 1580
1530 PRINT"There's a three-hour reel of recorded music here.":GOTO 1580
1540 PRINT"It's just a cheap lipstick... it's been used some...":GOTO 1580
1550 PRINT"It's pretty mushy... Let's just say that some girl named Susan was really hot for our boy Mike...":GOTO 1570
1560 PRINT"It looks like a bead off a girl's necklace or ear-ring..."
1570 FOR X=1 TO 5000:NEXT:GOTO 110
1580 FOR X=1 TO 3000:NEXT:GOTO 110
1590 PRINT"You can't do that from HERE...":GOTO 1580
1600 DATA "NORTH ","EAST ","SOUTH ","WEST "
1610 DATA at the Control Board,0,0,0,2,in the Control Room,11,1,10,3,in the staff lounge,28,2,0,27,in the Lobby,10,14,22,15,in the News Room,27,10,0,6,at a TELETYPE MACHINE in the News Room,0,5,0,0,in the Production Room,0,0,0,11
1620 DATA in the Record Library,0,0,15,0,in the Transmitter Room,25,16,11,18,in a dark passage,2,13,4,5,in a dark passage,9,7,2,23,in a Large Studio,13,0,0,0,in a Small Studio,0,0,12,10,in the Manager's Office,0,0,0,4
1630 DATA in the Program Director's office,8,4,0,0,in the Supply Room,0,0,0,9,in the Men's Room,0,27,0,21,in the Workshop,0,9,0,0,in the Ladies' Room,0,0,27,0,in the stall,0,0,0,0,in the stall,0,17,0,0,at the Front Door,4,0,0,0
1640 DATA at the Automation System,0,11,0,24,in the Reel Storage area,0,23,0,0,at the Back Door,0,0,9,0,at the Tower,0,0,25,0,in a dark passage,19,3,5,17,at the staff Noteboxes in the Lounge,0,0,3,0
1650 DATA a body (DEAD I think!),1,bulletin board,1,Red switch,1,Green switch,1,Microphone,1,Turntables,1,Control Board,2,Equipment,2,Couch,3,Ash tray,3,Couch,4,Desk,4,Phone,4,sign,4,typewriter,5,wastebasket,5,Control Board,7,Turntables,7
1660 DATA Microphone,7,Shelves loaded with records,8,Turn table for audition of records,8,Transmitter,9,Microphones,12,Piano,12,a Large (rumpled) Couch,12,Microphones,13,Table,13,Chairs,13,Desk,14,Lavatory,17,Stall,17,Lavatory,19,Stall,19
1670 DATA john,21,sign,22,sign,23,tower,26,a heavy wire fence,26,a strange car,22,locked door,0,Unlocked Door,22,Locked door,25,unlocked door,0,Notepad,1,Cigarette butt,0,torn half of a letter,0,paper,0,record,0,empty record sleve,0
1680 DATA Transmitter Log sheet,9,Memo,0,Ratings book,15,Tubes,16,misc. parts,16,keys,4,test equipment,18,reel of recorded tape,24,lipstick,27,lovenote,28,bead,0
1690 DATA "GO ",EXA,GET,TAK,DRO,PUT,THR,PUS,PRE,REA,SIT,ANS,USE,OPE,UNL,CLI,LOC
1700 DATA BOD,BUL,RED,GRE,MIC,TUR,CON,EQU,COU,ASH,,DES,PHO,SIG,TYP,WAS,,,,SHE,,TRA,,PIA,,,TAB,CHA,,LAV,STA,,,JOH,,,TOW,FEN,CAR,DOO,,,,NOT,BUT,LET,PAP,REC,SLE,LOG,MEM,RAT,TUB,PAR,KEY,TES,REE,LIP,LOV,BEA
1710 PRINT"Press <enter>";
1720 IF INKEY$="" THEN 1720 ELSE 120
1730 IF VP=>STORAGE THEN RETURN
1740 TEMP=VP+1
1750 LOCATE TEMP,1:PRINT STRING$(80,32)
1760 IF TEMP=STORAGE THEN RETURN ELSE TEMP=TEMP+1:GOTO 1750
