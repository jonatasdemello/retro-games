# README

Source:

http://winbgim.codecutter.org/

http://winbgim.codecutter.org/V6_0/doc/index.html

http://openbgi.sourceforge.net/

http://www.sandroid.org/TurboC/


http://libxbgi.sourceforge.net/


OpenGl via GLUT

To install GLUT, open terminal and type sudo apt-get install freeglut3-dev.



How to Use SDL_bgi and libXbgi

To compile a program using SDL_bgi, make sure that it includes the 'graphics.h' header file. Then:

gcc -o program program.c -lSDL_bgi -lSDL2

On MSYS2 + Mingw64:

gcc -o program.exe program.c -lmingw32 -L/mingw64/bin \
  -lSDL_bgi -lSDL2main -lSDL2 # -mwindows

Instructions are also provided for CodeBlocks and Dev-C++.

To compile a program using libXbgi, make sure that it includes the 'graphics.h' header file. Then:

gcc -o program program.c /usr/lib/libXbgi.a -lX11 -lm

In most cases, all you have to do to compile an old program is comment out the lines

#include <conio.h>
#include <dos.h>

if they're present. Alternatively, you may create dummy (empty) conio.h and dos.h file, if you wish.

Please consult the enclosed documentation for more information.


