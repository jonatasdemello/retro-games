# Variables 

*** VARIABLES *** 

	SP = 0		=> loading
	IC = 0		=> # Items Carried

	PLOC = 1	=> player location (LOC in Applesoft Basic)
	SAFED = 0	=> safe door (LEFT|CENTER|RIGHT)
		SAFED = (INPTK(2) - DIROFF) + 1
		DOORDIR = INPTK(2) - DIROFF
		
	LASTITEM = 27	=> number of game objetcs
	LWRD = 60		=> number of words in the game
	NXDESC = 7		=> number of extended locations (extra/hidden)
	
	DIROFF = 30		=> direction offset (LEFT|CENTER|RIGHT)
	ITEMOFF = 33	=> item offset (objects start at 33)
	IMMOFF = 20		=> index for objects
		~ ARG > IMMOFF	=> objects user can't take: fridge, couch, clothes, door, railing, dumbwaiter, fusebox
		~ ILOC(IMMOFF+7)=30 => 27=FUSEBOX  30=MID-AIR position
		~ IMMOFF + 4 => 24=DOOR
		~ IMMOFF + 5 => 25=RAILING
		~ IMMOFF + 6 => 26=DUMBWAITER
		~ IMMOFF + 7 => 27=FUSEBOX

 

Input: 

	COMM	=> INPTK(1)
	ARG		=> INPTK(2) - ITEMOFF
	MVARG	=> INPTK(3) - ITEMOFF

	INW$(10)	=> read input
	INPTK(10)	=> parse input
	VOCAB$(60)	=> all Verbs, Objects
	NULLW$(4)	=> ignore words
	IDESC$(30)	=> description for each location
	ILOC(30)	=> object location: ILOC(7) = 13   =>	7=KEY is located ar room 13=MASTER BEDROOM
	RNAME$(31)	=> room names: RNAME$(2) = KITCHEN
	RDESC$(31)	=> room description: RDESC$(31) = "COUNTERTOPS ARE DUSTY AND THERE ARE RUSTING POTS AND PANS"
	REXIT(31,6)	=> 6 exits for each location, meaning = 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
		example: "FOYER", 2, 31, 3, 4, 0, 0,
		position 1=FOYER has exits:
			1-NORTH => goes to room 2=KITCHEN
			2-SOUTH => goes to room 31=EXIT
			3-EAST  => goes to room 3=SITTING ROOM
			4-WEST  => goes to room 4=HALL
			5-UP    => no exit 0
			6-DOWN  => no exit 0
					
	EXLOC(10,2)	=> 
	EXDESC$(10)	=> extended description for each local
	

## Vocabulary, Verbs, Objects

The original version has arrays starting with 1, so 00-dummy was added.

This is IDX-33 (ITEMOFF)

|	IDX		|	VOCAB$(60) 	|	Type	|	x	|
|-----------|---------------|-----------|-------|
|	 00 	|	 -99 		|	dummy	|	.	|
|-----------|---------------|-----------|-------|
|	 01 	|	 NORTH		|	move	|	.	|
|	 02 	|	 SOUTH		|	move	|	.	|
|	 03 	|	 EAST		|	move	|	.	|
|	 04 	|	 WEST		|	move	|	.	|
|	 05 	|	 UP			|	move	|	.	|
|	 06 	|	 DOWN		|	move	|	.	|
|	 07 	|	 N			|	move	|	.	|
|	 08 	|	 S			|	move	|	.	|
|	 09 	|	 E			|	move	|	.	|
|	 10 	|	 W			|	move	|	.	|
|	 11 	|	 U			|	move	|	.	|
|	 12 	|	 D			|	move	|	.	|
|	 13 	|	 I			|	action	|	.	|
|	 14 	|	 INVENTORY	|	action	|	.	|
|	 15 	|	 SCORE		|	action	|	.	|
|	 16 	|	 JUMP		|	action	|	.	|
|	 17 	|	 HELP		|	action	|	.	|
|	 18 	|	 TAKE		|	action	|	1	|
|	 19 	|	 DROP		|	action	|	2	|
|	 20 	|	 LOOK		|	action	|	3	|
|	 21 	|	 READ		|	action	|	4	|
|	 22 	|	 EXAMINE	|	action	|	5	|
|	 23 	|	 UNLOCK		|	action	|	6	|
|	 24 	|	 EAT		|	action	|	7	|
|	 25 	|	 SPIN		|	action	|	8	|
|	 26 	|	 MOVE		|	action	|	9	|
|	 27 	|	 OPEN		|	action	|	10	|
|	 28 	|	 TIE		|	action	|	11	|
|	 29 	|	 OIL		|	action	|	12	|
|	 30 	|	 PUT		|	action	|	13	|
|-----------|---------------|-----------|-------|
|	 31 	|	 LEFT		|	doors	|	14	|
|	 32 	|	 CENTER		|	doors	|	15	|
|	 33 	|	 RIGHT		|	doors	|	16	|

Table continues:

|	IDX		|	VOCAB$(60)	|	IDX-33 	| ILOC(27)	|	IDESC$(27)						|
|-----------|---------------|-----------|-----------|----------------------------------	|
|	 --		|		00 		|	-99		|	-		|	dummy							|
|-----------|---------------|-----------|-----------|-----------------------------------|
|	 34 	|	NEWSPAPER	|	 01 	|	 1		|	TAYS HOUSE UNLIKELY EVER TO BE SOLD. TALES OF GUTTED STAIRWELLS AND BOOBY TRAPS HAVE SPOOKED BUYERS...		|
|	 35 	|	TEDDYBEAR	|	 02 	|	-1		|	SOMEONE HAS BEEN PLAYING VERY ROUGH WITH THIS TOY		|
|	 36 	|	FUSE		|	 03 	|	-1		|	OLD-FASHIONED ELECTRICAL FUSE		|
|	 37 	|	JACK		|	 04 	|	10		|	TIRE JACK for LIFTING HEAVY OBJECTS LIKE CARS		|
|	 38 	|	PICTURE		|	 05 	|	30		|	UNCLE TAYS IN ALL HIS SALLOW GLORY		|
|	 39 	|	BUNGEE		|	 06 	|	10		|	CORD for BUNGEE JUMPING		|
|	 40 	|	KEY			|	 07 	|	13		|	A SMALL BRASS KEY		|
|	 41 	|	TOP			|	 08 	|	15		|	A CHILD'S TOY (spinning top)		|
|	 42 	|	NOTE		|	 09 	|	 9		|	THE WRITING IS REVERSED		|
|	 43 	|	GAINESBURGER|	 10 	|	16		|	SUPPOSEDLY DOG FOOD THOUGH IT APPEARS TO BE MADE OF PLASTIC		|
|	 44 	|	GLOVES		|	 11 	|	22		|	RUBBER GLOVES USED for CLEANING		|
|	 45 	|	BOXSPRING	|	 12 	|	26		|	A QUEEN-SIZED BOXSPRING		|
|	 46 	|	BRACE		|	 13 	|	25		|	A BACK BRACE		|
|	 47 	|	MAGAZINE	|	 14 	|	25		|	TAYS' STRANGE INVENTIONS INCLUDE BOOBY-TRAPPED DOORS AND TOYS THAT OPEN DOORS BY REMOTE CONTROL...		|
|	 48 	|	OILCAN		|	 15 	|	28		|	THIS CAN CONTAINS FINE LUBRICATING OIL		|
|	 49 	|	CHECKBOOK	|	 16 	|	 8		|	UNCLE TAYS' CHECKBOOK LISTS A BALANCE OF $220000		|
|	 50 	|	DIAMOND		|	 17 	|	-1		|	THIS DIAMOND'S BEAUTY STEMS FROM ALL THE GODDAMNED MONEY IT IS WORTH		|
|	 51 	|	LOVERBOY	|	 18 	|	19		|	LOVERBOY'S FIRST ALBUM IN VINYL WORTH AN INCALCULABLE SUM		|
|	 52 	|	INVESTMENT	|	 19 	|	21		|	PRE-IPO SHARES OF APOLLO COMPUTING HAVE TO BE WORTH ... SOMETHING		|
|	 53 	|	LOONS		|	 20 	|	27		|	A THICK WAD OF CANADIAN NOTES		|
|	 54 	|	FRIDGE		|	 21 	|	 2		|	THIS OLD REFRIGERATOR'S MOTOR LABORS HEAVILY		|
|	 55 	|	COUCH		|	 22 	|	 6		|	AN OVERSTUFFED DUSTY COUCH		|
|	 56 	|	CLOTHES		|	 23 	|	 7		|	A DISGUSTING PILE OF SOILED LAUNDRY		|
|	 57 	|	DOOR		|	 24 	|	-1		|	3 DOORS		|
|	 58 	|	RAILING		|	 25 	|	12		|	A railing or guardrail, is a system designed to keep people or objects from falling off the balcony.		|
|	 59 	|	DUMBWAITER	|	 26 	|	-1		|	A dumbwaiter lift is a small freight elevator designed to transport goods, supplies, or food between different levels of a building.		|
|	 60 	|	FUSEBOX		|	 27 	|	-1		|	AN OLD-FASHIONED FUSEBOX. THE FUSE MARKED 'ATTIC' IS MISSING.		|
|	 61 	|	MIRROR		|	 00		|	-		|	.	|
|	 62 	|	L			|	 00		|	-		|	.	|
|	 63 	|	G			|	 00		|	-		|	.	|
|	 64 	|	X			|	 00		|	-		|	.	|


## Room Name + Description

|	IDX		|	RNAME$(31)			| 	RDESC$(31)						|
|-----------|-----------------------|-----------------------------------|
|	 00 	|	dummy				|	dummy							|
|-----------|-----------------------|-----------------------------------|
|	 01 	|	FOYER (LOBBY)		|	THE ENTRYWAY TO THE HOUSE		|
|	 02 	|	KITCHEN				|	COUNTERTOPS ARE DUSTY AND THERE ARE RUSTING POTS AND PANS		|
|	 03 	|	SITTING ROOM		|	THIS ROOM IS TWO STORIES HIGH AND CONTAINS ELEGANT CHAIRS AND COUCHES		|
|	 04 	|	HALLWAY				|	A NARROW HALLWAY WHICH RUNS WEST OF THE FOYER		|
|	 05 	|	HALLWAY				|	A NARROW HALLWAY AT THE WEST END OF THE HOUSE		|
|	 06 	|	DEN					|	THIS ROOM HAS AN ANCIENT TELEVISION		|
|	 07 	|	BATHROOM			|	A DINGY BATHROOM WITH A CRACKED SINK		|
|	 08 	|	LIBRARY				|	THIS WELL-FURNISHED LIBRARY IS LINED WITH BOOKS AND LEATHER FURNITURE		|
|	 09 	|	SMALL BEDROOM		|	THIS SMALL BEDROOM HAS A TWIN BED AND CHAIR. IT LOOKS LITTLE USED.		|
|	 10 	|	GARAGE				|	THE CAVERNOUS GARAGE HOLDS A NON-OPERATIONAL GREMLIN AND PILES OF JUNK		|
|	 11 	|	DINING ROOM			|	TROPHIES LINE THE WALLS. THERE ARE SIX CHAIRS AROUND A LONG TABLE.		|
|	 12 	|	BALCONY				|	BALCONY ABOVE THE SITTING ROOM. A RAILING PROTECTS YOU FROM A 15-FOOT DROP.		|
|	 13 	|	MASTER BEDROOM		|	THIS LARGE CORNER BEDROOM HAS SOLID WALNUT FURNITURE AND A LARGE MIRROR		|
|	 14 	|	HALLWAY				|	A HALLWAY WITH A LARGE ARCH ON ITS SOUTH SIDE		|
|	 15 	|	GAME ROOM			|	THIS ELEGANT GAME ROOM HAS A POOL TABLE AND MARBLE CHESSBOARD		|
|	 16 	|	CLOSET				|	A SPACIOUS CLOSET OFF THE GAMEROOM		|
|	 17 	|	HALLWAY				|	A HALLWAY IN THE CENTER OF THE SECOND FLOOR		|
|	 18 	|	CHILD'S ROOM		|	YOUR COUSIN'S ROOM IN HAPPIER TIMES BEFORE HE RAN OFF TO JOIN THE BAATH PARTY		|
|	 19 	|	SECRET ROOM1		|	A DARK CHAMBER OFF THE BEDROOM		|
|	 20 	|	DANGEROUS HALL		|	THIS EERIE HALL HAS THREE IDENTICAL DOORS ON THE WEST WALL		|
|	 21 	|	CORNER BEDROOM		|	A COZY CORNER ROOM WITH WINDOWS ON TWO WALLS		|
|	 22 	|	BATHROOM			|	AN ELEGANT BATH WITH A MIRROR OVER A MARBLE SINK		|
|	 23 	|	DUMBWAITER 2		|	A CRAMPED DUMBWAITER (SECOND FLOOR)		|
|	 24 	|	DUMBWAITER 1		|	A CRAMPED DUMBWAITER (MAIN FLOOR)		|
|	 25 	|	ATTIC				|	A DUSTY ATTIC WITH LOW SLOPING WALLS		|
|	 26 	|	STORAGE ROOM		|	A BARE ROOM USED TO STORE RANDOM EQUIPMENT AND FURNITURE		|
|	 27 	|	LAUNDRY				|	THIS ROOM HAS A WASHER AND DRYER AS WELL AS A BOILER AND FURNACE		|
|	 28 	|	WORK ROOM			|	EQUIPMENT for WORKING WOOD AND METAL		|
|	 29 	|	BOTTOM OF STAIRS	|	STAIRS FROM BASEMENT TO KITCHEN		|
|	 30 	|	MID-AIR				|	HANGING FROM A BUNGEE CORD		|
|	 31 	|	LEAVE THE HOUSE 	|	LEAVE THE HOUSE AND THE GAME		|


Extended description for each local:


|	IDX		|	EXLOC(I,1)	|	EXLOC(I,1)	|	EXDESC$(7)				|
|-----------|---------------|---------------|---------------------------|
|	 00 	|		-		|		-		|	dummy					|
|-----------|---------------|---------------|---------------------------|
|	 01		|		5		|		6		|	THERE IS A LOCKED DOOR TO THE NORTH.		|
|	 02		|		8		|		6		|	THERE IS A LOCKED DOOR TO THE SOUTH.		|
|	 03		|		2		|		6		|	STAIRS LEAD DOWN TO A CELLAR. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.		|
|	 04		|		29		|		5		|	STAIRS LEAD UP. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.		|
|	 05		|		12		|		5		|	DARK STAIRS LEAD UP TO THE ATTIC.		|
|	 06		|		17		|		4		|	A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'.		|
|	 07		|		17		|		1		|	YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH.		|


Exits for each location: EXLOC(I,1) = 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN

	
-------------------------------------------------------------------------------------------------------------------------------
## 0 Ignore Words - NULLW$(4)

	THE
	TO
	WITH
	USING

## 1 Word:

	NORTH
	SOUTH
	EAST
	WEST
	UP
	DOWN
	N
	S
	E
	W
	U
	D
	I
	INVENTORY
	SCORE
	JUMP
	HELP

## 2 Words:

	TAKE x
	DROP x
	LOOK x
	READ x
	EXAMINE x
	UNLOCK x
	EAT x
	SPIN x
	MOVE x
	OPEN x
	TIE x
	OIL x
	PUT x

## 3 Words:

	MOVE COUCH WITH BRACE
	MOVE FRIDGE WITH JACK
	MOVE CLOTHES WITH GLOVES
	OPEN [DIRECTION] DOOR
	OIL DUMBWAITER WITH OILCAN
	TIE BUNGEE TO RAILING
	PUT FUSE IN FUSEBOX
	READ NOTE IN MIRROR
	
	