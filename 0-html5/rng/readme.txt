README

http://libxbgi.sourceforge.net/

OpenGl via GLUT

To install GLUT, open terminal and type sudo apt-get install freeglut3-dev.


Using SDL
If you want to use graphics.h on Ubuntu platform you need to compile and install libgraph. It is the implementation of turbo c graphics API on Linux using SDL.

It is not very powerful and suitable for production quality application, but it is simple and easy-to-use for learning purpose.

You can download it from here.
https://download.savannah.gnu.org/releases/libgraph/


First add the Universe repository (since some required packages are not available in main repository):

sudo add-apt-repository universe
sudo apt-get update
Second install build-essential and some additional packages:

For versions prior to 18.04:

sudo apt-get install libsdl-image1.2 libsdl-image1.2-dev guile-1.8 \
guile-1.8-dev libsdl1.2debian libart-2.0-dev libaudiofile-dev \
libesd0-dev libdirectfb-dev libdirectfb-extra libfreetype6-dev \
libxext-dev x11proto-xext-dev libfreetype6 libaa1 libaa1-dev \
libslang2-dev libasound2 libasound2-dev build-essential
For 18.04: From Ubuntu 18.04 guile-2.0 works and libesd0-dev is deprecated. For this you need to add repositories of xenial in sources.list.

sudo nano /etc/apt/sources.list
Add these lines:

deb http://us.archive.ubuntu.com/ubuntu/ xenial main universe
deb-src http://us.archive.ubuntu.com/ubuntu/ xenial main universe
Run sudo apt-get update. Then install packages using:

sudo apt-get install libsdl-image1.2 libsdl-image1.2-dev guile-2.0 \
guile-2.0-dev libsdl1.2debian libart-2.0-dev libaudiofile-dev \
libesd0-dev libdirectfb-dev libdirectfb-extra libfreetype6-dev \
libxext-dev x11proto-xext-dev libfreetype6 libaa1 libaa1-dev \
libslang2-dev libasound2 libasound2-dev
Now extract the downloaded libgraph-1.0.2.tar.gz file.

Go to the extracted folder and run the following command:

./configure
make
sudo make install
sudo cp /usr/local/lib/libgraph.* /usr/lib
Now you can use #include<graphics.h> on Ubuntu and the following line in your program:

int gd=DETECT,gm; 
initgraph(&gd,&gm,NULL);
Here is a sample program using graphics.h:

/*  demo.c */
#include <graphics.h>

int main()
{
   int gd = DETECT,gm,left=100,top=100,right=200,bottom=200,x= 300,y=150,radius=50;
   initgraph(&gd,&gm,NULL);
   rectangle(left, top, right, bottom);
   circle(x, y, radius);
   bar(left + 300, top, right + 300, bottom);
   line(left - 10, top + 150, left + 410, top + 150);
   ellipse(x, y + 200, 0, 360, 100, 50);
   outtextxy(left + 100, top + 325, "C Graphics Program");

   delay(5000);
   closegraph();
   return 0;
}
To compile it use

gcc demo.c -o demo -lgraph
To run type

./demo
Output of Demo 1

Using OpenGL (via GLUT)
Although OpenGL is basically made for 3D programming, drawing 2D shapes gives the basic outline and introduction to OpenGL and gives the idea about how to start drawing objects in OpenGL.

To install GLUT, open terminal and type sudo apt-get install freeglut3-dev.
Here is a simple graphics program using GLUT
/*  demo.c */
#include <GL/gl.h>
#include <GL/glut.h>
#include <GL/glu.h>

void setup() {   glClearColor(1.0f, 1.0f, 1.0f, 1.0f); }

void display()
   {
      glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
      glColor3f(0.0f, 0.0f, 0.0f);
      glRectf(-0.75f,0.75f, 0.75f, -0.75f);
      glutSwapBuffers();
   }

int main(int argc, char *argv[])
  {
     glutInit(&argc, argv);
     glutInitDisplayMode(GLUT_RGB | GLUT_DEPTH | GLUT_DOUBLE);
     glutInitWindowSize(800,600);
     glutCreateWindow("Hello World");

     setup();
     glutDisplayFunc(display);
     glutMainLoop();
     return 0;
  }
Compile it using

gcc demo.c -o demo -lglut -lGL

Run it using

./demo




libgraph expects libguile.h to be in the standard include paths which it is not. The autoconf script should really find the correct locations (which I consider a build system bug) but it doesn't, so you need to add its include path to the preprocessor and linker flags. The standard approach would be:

sudo apt install guile-2.0-dev  # In case you didn't install it earlier
CPPFLAGS="$CPPFLAGS $(pkg-config --cflags-only-I guile-2.0)" \
  CFLAGS="$CFLAGS $(pkg-config --cflags-only-other guile-2.0)" \
  LDFLAGS="$LDFLAGS $(pkg-config --libs guile-2.0)" \
  ./configure
make
Alternatively you can build libgraph without Guile module support:

./configure --disable-guile
make



Make sure that you have basic compilers installed. For this run the command:

sudo apt-get install build-essential

Install few packages that required. Run the command:

sudo apt-get install libsdl-image1.2 libsdl-image1.2-dev guile-1.8 guile-1.8-dev libsdl1.2debian libart-2.0-dev libaudiofile-dev libesd0-dev libdirectfb-dev libdirectfb-extra libfreetype6-dev libxext-dev x11proto-xext-dev libfreetype6 libaa1 libaa1-dev libslang2-dev libasound2 libasound2-dev

Now download libgraph. Copy libgraph-1.0.2.tar.gz to your home folder. Right click on it and select "Extract Here". Then run following commands one by one.

cd libgraph-1.0.2

./configure

sudo make

sudo make install

sudo cp /usr/local/lib/libgraph.* /usr/lib

Share
Improve this answer
Follow


