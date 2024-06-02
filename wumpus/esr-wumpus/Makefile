#
# Makefile for `Hunt the Wumpus' and `Superhack' games
#
VERS=1.8

prefix?=/usr

CFLAGS = -Wall

all: wumpus superhack

wumpus: wumpus.c
	$(CC) $(CFLAGS) wumpus.c -o wumpus

superhack: superhack.c
	$(CC) $(CFLAGS) superhack.c -o superhack

clean:
	rm -f wumpus wumpus.o superhack superhack.o TAGS 
	rm -f *~ *.1 *.html *.tar.gz MANIFEST

TAGS:
	etags wumpus.c

wumpus.6: wumpus.xml
	xmlto man wumpus.xml 

wumpus.html: wumpus.xml
	xmlto html-nochunks wumpus.xml 

SOURCES = README COPYING NEWS control Makefile wumpus.c superhack.c wumpus.xml wumpus.png wumpus.desktop superhack.desktop

install: wumpus superhack wumpus.6
	mkdir -p $(prefix)/bin
	mkdir -p $(prefix)/share/man/man6
	cp wumpus superhack $(prefix)/bin/
	cp wumpus.6 $(prefix)/share/man/man6/

uninstall:
	rm $(prefix)/bin/wumpus $(prefix)/bin/superhack
	rm $(prefix)/share/man/man6/wumpus.6

CPPCHECKOPTS =
cppcheck:
	cppcheck -DREVISION=$(VERS) $(CPPCHECKOPTS) wumpus.c
	cppcheck -DREVISION=$(VERS) $(CPPCHECKOPTS) superhack.c

wumpus-$(VERS).tar.gz: $(SOURCES) wumpus.6
	@ls $(SOURCES) wumpus.6 | sed s:^:wumpus-$(VERS)/: >MANIFEST
	@(cd ..; ln -s wumpus wumpus-$(VERS))
	(cd ..; tar -czf wumpus/wumpus-$(VERS).tar.gz `cat wumpus/MANIFEST`)
	@(cd ..; rm wumpus-$(VERS))

dist: wumpus-$(VERS).tar.gz

release: wumpus-$(VERS).tar.gz wumpus.html
	shipper version=$(VERS) | sh -e -x

refresh: wumpus.html
	shipper -N -w version=$(VERS) | sh -e -x
