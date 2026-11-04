# UNCLE TAYS HOUSE ADVENTURE

C# version, created by Jonatas de Mello.

This game was found on the Internet (Interactive Fiction Database)

**Uncle Tays' House Adventure links:**

https://ifdb.org/viewgame?id=7zol897du6iup7t2

https://jimgerrie.blogspot.com/2014/02/uncle-tays-house-adventure-walkthrough.html

https://github.com/jggames


**An orignal version can be played online at:**

Select "Text Adventure (Floyd McWilliams)"

https://inexorabletash.github.io/jsbasic/


## Notes:

This is a simple port from BASIC to C#.
The goal is to keep ot simple, so it can be ported to Python or Javascript later.
I also added Unit tests, so I could make changes and test the game automatically.

I used C# because I am more familiar with, plus testing and debugging is easier.

BASIC arrarys start at 1, so I added a dummy -99 element to index 0 of each array make the convesion easier.

`arr[0] = -99;`


public int[] ILOC = [
/* 00 */    -99,     //dummy
/* 01 */     1,      // "NEWSPAPER",
/* 02 */    -1,      // "TEDDYBEAR", -1
/* 03 */    -1,      // "FUSE",
/* 04 */    10,      // "JACK", 10
/* 05 */    30,      // "PICTURE",
/* 06 */    10,      // "BUNGEE", 10
/* 07 */    13,      // "KEY", 13
/* 08 */    15,      // "SPINNINGTOP", 15
/* 09 */     9,      // "NOTE", 9
/* 10 */    16,      // "GAINESBURGER", 16
/* 11 */    22,      // "GLOVES", 22
/* 12 */    26,      // "BOXSPRING",
/* 13 */    25,      // "BRACE", 25
/* 14 */    25,      // "MAGAZINE",
/* 15 */    28,      // "OILCAN", 28
/* 16 */     8,      // "CHECKBOOK",
/* 17 */    -1,      // "DIAMOND",
/* 18 */    19,      // "LOVERBOY",
/* 19 */    21,      // "INVESTMENT",
/* 20 */    27,      // "LOONS",
/* 21 */     2,      // "FRIDGE",
/* 22 */     6,      // "COUCH",
/* 23 */     7,      // "CLOTHES",
/* 24 */    -1,      // "DOOR",
/* 25 */    12,      // "RAILING",
/* 26 */    -1,      // "DUMBWAITER",
/* 27 */    -1       // "FUSEBOX"
];