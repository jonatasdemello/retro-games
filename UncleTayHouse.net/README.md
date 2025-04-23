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

