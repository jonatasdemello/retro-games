using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public class GameItems
    {
        // Describe game objects and their locations
        public List<GameItemModel> houseItems { get; } =
        [
            new() { id = 0,  location = -99, objId =  0,  name = "(dummy)",      desc = "(dummy)" },
            new() { id = 1,  location =  1,  objId = 34,  name = "NEWSPAPER",    desc = "Tays house unlikely ever to be sold. tales of gutted stairwells and booby traps have spooked buyers..." },
            new() { id = 2,  location = -1,  objId = 35,  name = "TEDDYBEAR",    desc = "Someone has been playing very rough with this toy" },
            new() { id = 3,  location = -1,  objId = 36,  name = "FUSE",         desc = "Old-fashioned electrical fuse" },
            new() { id = 4,  location = 10,  objId = 37,  name = "JACK",         desc = "Tire jack for lifting heavy objects like cars" },
            new() { id = 5,  location = 30,  objId = 38,  name = "PICTURE",      desc = "Uncle tays in all his sallow glory" },
            new() { id = 6,  location = 10,  objId = 39,  name = "BUNGEE",       desc = "Cord for bungee jumping" },
            new() { id = 7,  location = 13,  objId = 40,  name = "KEY",          desc = "A small brass key" },
            new() { id = 8,  location = 15,  objId = 41,  name = "SPINNINGTOP",  desc = "A child's toy (spinning top)" },
            new() { id = 9,  location =  9,  objId = 42,  name = "NOTE",         desc = "The writing is reversed. maybe there is a way to read it somewhere..." },
            new() { id = 10, location = 16,  objId = 43,  name = "GAINESBURGER", desc = "Supposedly dog food, though it appears to be made of plastic" },
            new() { id = 11, location = 22,  objId = 44,  name = "GLOVES",       desc = "Rubber gloves used for cleaning" },
            new() { id = 12, location = 26,  objId = 45,  name = "BOXSPRING",    desc = "A queen-sized boxspring" },
            new() { id = 13, location = 25,  objId = 46,  name = "BRACE",        desc = "A back brace" },
            new() { id = 14, location = 25,  objId = 47,  name = "MAGAZINE",     desc = "Tays' strange inventions include booby-trapped doors and toys that open doors by remote control..." },
            new() { id = 15, location = 28,  objId = 48,  name = "OILCAN",       desc = "This can contains fine lubricating oil" },
            new() { id = 16, location =  8,  objId = 49,  name = "CHECKBOOK",    desc = "Uncle tays' checkbook lists a balance of $220,000" },
            new() { id = 17, location = -1,  objId = 50,  name = "DIAMOND",      desc = "This diamond's beauty stems from all the goddamned money it is worth" },
            new() { id = 18, location = 19,  objId = 51,  name = "LOVERBOY",     desc = "Loverboy's first album in vinyl, worth an incalculable sum" },
            new() { id = 19, location = 21,  objId = 52,  name = "INVESTMENT",   desc = "Pre-ipo shares of apollo computing have to be worth ... something" },
            new() { id = 20, location = 27,  objId = 53,  name = "LOONS",        desc = "A thick wad of canadian notes" },
            new() { id = 21, location =  2,  objId = 54,  name = "FRIDGE",       desc = "This old refrigerator's motor labors heavily" },
            new() { id = 22, location =  6,  objId = 55,  name = "COUCH",        desc = "An overstuffed, dusty couch" },
            new() { id = 23, location =  7,  objId = 56,  name = "CLOTHES",      desc = "A disgusting pile of soiled laundry" },
            new() { id = 24, location = -1,  objId = 57,  name = "DOOR",         desc = "3 misterious doors" },
            new() { id = 25, location = 12,  objId = 58,  name = "RAILING",      desc = "A railing or guardrail, is a system designed to keep people or objects from falling off the balcony." },
            new() { id = 26, location = -1,  objId = 59,  name = "DUMBWAITER",   desc = "A dumbwaiter lift is a small freight elevator designed to transport goods, supplies, or food between different levels of a building." },
            new() { id = 27, location = -1,  objId = 60,  name = "FUSEBOX",      desc = "An old-fashioned fusebox. the fuse marked 'attic' is missing." },
            new() { id = 28, location = 22,  objId = 61,  name = "MIRROR",       desc = "A mirror in the wall" }
        ];
        public GameItemModel GetObject(string obj)
        {
            return houseItems.FirstOrDefault(i => i.name.Equals(obj, StringComparison.OrdinalIgnoreCase))
                   ?? new GameItemModel { id = -1, location = -1, objId = -1, name = "(not found)", desc = "(not found)" };
        }

        // extended description
        public ExtendedDescriptionModel[] extDesc { get; } =
        [
            new ExtendedDescriptionModel { location = 5, direction = 6, description = "There is a locked door to the north." },
            new ExtendedDescriptionModel { location = 8, direction = 6, description = "There is a locked door to the south." },
            new ExtendedDescriptionModel { location = 2, direction = 6, description = "Stairs lead down to a cellar. Several steps have collapsed, making the staircase unusable." },
            new ExtendedDescriptionModel { location = 29, direction = 5, description = "Stairs lead up. Several steps have collapsed, making the staircase unusable." },
            new ExtendedDescriptionModel { location = 12, direction = 5, description = "Dark stairs lead up to the attic." },
            new ExtendedDescriptionModel { location = 17, direction = 4, description = "A locked door to the WEST is labelled 'EXTREME DANGER'." },
            new ExtendedDescriptionModel { location = 17, direction = 1, description = "Your uncle's doberman is blocking a doorway to the north." }
        ];

        // Describe Rooms
        public List<GameMapModel> houseMap { get; } =
        [
            new() { id = 0,  rname = "(dymmy)", rdesc = "(dymmy)" }, // for now we need this, will remove later
            new() { id = 1,  rname = "FOYER (LOBBY)", rdesc = "The entryway to the house" },
            new() { id = 2,  rname = "KITCHEN", rdesc = "Countertops are dusty and there are rusting pots and pans" },
            new() { id = 3,  rname = "SITTING ROOM", rdesc = "This room is two stories high and contains elegant chairs and couches" },
            new() { id = 4,  rname = "HALLWAY", rdesc = "A narrow hallway which runs west of the foyer" },
            new() { id = 5,  rname = "HALLWAY", rdesc = "A narrow hallway at the west end of the house, a door to north" },
            new() { id = 6,  rname = "DEN", rdesc = "This room has an ancient television" },
            new() { id = 7,  rname = "BATHROOM", rdesc = "A dingy bathroom with a cracked sink" },
            new() { id = 8,  rname = "LIBRARY", rdesc = "This well-furnished library is lined with books and leather furniture" },
            new() { id = 9,  rname = "SMALL BEDROOM", rdesc = "This small bedroom has a twin bed and chair. It looks little used" },
            new() { id = 10, rname = "GARAGE", rdesc = "The cavernous garage holds a non-operational gremlin and piles of junk" },
            new() { id = 11, rname = "DINING ROOM", rdesc = "Trophies line the walls. there are six chairs around a long table" },
            new() { id = 12, rname = "BALCONY", rdesc = "Balcony above the sitting room. a railing protects you from a 15-foot drop" },
            new() { id = 13, rname = "MASTER BEDROOM", rdesc = "This large corner bedroom has solid walnut furniture and a large mirror" },
            new() { id = 14, rname = "HALLWAY", rdesc = "A hallway with a large arch on its south side" },
            new() { id = 15, rname = "GAME ROOM", rdesc = "This elegant game room has a pool table and marble chessboard" },
            new() { id = 16, rname = "CLOSET", rdesc = "A spacious closet off the gameroom" },
            new() { id = 17, rname = "HALLWAY", rdesc = "A hallway in the center of the second floor" },
            new() { id = 18, rname = "CHILD'S ROOM", rdesc = "Your cousin's room in happier times, before he ran off to join the baath party" },
            new() { id = 19, rname = "SECRET ROOM", rdesc = "A dark chamber off the bedroom" },
            new() { id = 20, rname = "DANGEROUS HALL", rdesc = "This eerie hall has three identical doors on the west wall (left, center, right)" },
            new() { id = 21, rname = "CORNER BEDROOM", rdesc = "A cozy corner room with windows on two walls" },
            new() { id = 22, rname = "BATHROOM", rdesc = "An elegant bath with a mirror over a marble sink" },
            new() { id = 23, rname = "DUMBWAITER", rdesc = "A jammed dumbwaiter (second floor)" },
            new() { id = 24, rname = "DUMBWAITER", rdesc = "A dumbwaiter (main floor)" },
            new() { id = 25, rname = "ATTIC", rdesc = "A dusty attic with low sloping walls" },
            new() { id = 26, rname = "STORAGE ROOM", rdesc = "A bare room used to store random equipment and furniture" },
            new() { id = 27, rname = "LAUNDRY", rdesc = "This room has a washer and dryer, as well as a boiler and furnace" },
            new() { id = 28, rname = "WORK ROOM", rdesc = "Equipment for working wood and metal" },
            new() { id = 29, rname = "BOTTOM OF STAIRS", rdesc = "Stairs from basement to kitchen" },
            new() { id = 30, rname = "MID-AIR", rdesc = "Hanging from a bungee cord" },
            new() { id = 31, rname = "LEAVE THE HOUSE", rdesc = "Leave the house and the game" }
        ];

        public int GetLocationExit(int location, int direction)
        {
            if (IsValid(location, direction))
            {
                return LocationExit[location, direction];
            }
            return -99;
        }

        public int SetLocationExit(int curLocation, int direction, int newLocation)
        {
            if (IsValid(curLocation, direction))
            {
                LocationExit[curLocation, direction] = newLocation;
                return newLocation;
            }
            return -99;
        }

        public bool IsValid(int location, int direction)
        {
            return (location >= 0 && location < LocationExit.GetLength(0) &&
                    direction >= 0 && direction < LocationExit.GetLength(1) &&
                    LocationExit[location, direction] != -99);
        }

        public bool IsExitHidden(int location, int direction)
        {
            int loc = GetLocationExit(location, direction);
            return loc <= 0;

        }

        public int[,] LocationExit { get; set; } = {
            // 1-NORTH 2-SOUTH 3-EAST 4-WEST 5-UP 6-DOWN
            // 0    N      S      E      W      U      D
            { 00,  -99  , -99  , -99  , -99  , -99  , -99  },//0-dummy
            { 01,    2  ,  31  ,   3  ,   4  ,   0  ,   0  },
            { 02,    0  ,   1  ,   0  ,   0  ,   0  ,  -1  }, // D: STAIRS TO BASEMENT is hidden until drop Boxspring
            { 03,    2  ,   0  ,  11  ,   1  ,  12  ,   0  },
            { 04,    6  ,   7  ,   1  ,   5  ,   0  ,   0  },
            { 05,    0  ,   9  ,   4  ,  10  ,   0  ,   0  },
            { 06,    0  ,   4  ,   0  ,   0  ,   0  ,   0  },
            { 07,    4  ,   0  ,   0  ,   0  ,   0  ,  -1  }, // D: move clothes with gloves
            { 08,    0  ,   0  ,   0  ,  24  ,   0  ,   0  },
            { 09,    5  ,   0  ,   0  ,   0  ,   0  ,   0  },
            { 10,    0  ,   0  ,   5  ,   0  ,   0  ,   0  },
            { 11,    0  ,   0  ,   0  ,   3  ,   0  ,   0  },
            { 12,   13  ,   0  ,   0  ,  14  ,  -1  ,   3  }, // W: STAIRS TO ATTIC25 is hidden until fuse is inserted
            { 13,    0  ,  12  ,   0  ,   0  ,   0  ,   0  },
            { 14,    0  ,  15  ,  12  ,  17  ,   0  ,   0  },
            { 15,   14  ,   0  ,   0  ,  16  ,   0  ,   0  },
            { 16,    0  ,   0  ,  15  ,   0  ,   0  ,   0  },
            { 17,   -1  ,   0  ,  14  ,  -1  ,   0  ,   0  }, // N: HALL, doverman blocks door until drop teddybear, W: unlock door
            { 18,    0  ,  17  ,  -1  ,   0  ,   0  ,   0  }, // E: secret room hidden until SPINNINGTOP
            { 19,    0  ,   0  ,   0  ,  18  ,   0  ,   0  },
            { 20,   21  ,  22  ,  17  ,  -1  ,   0  ,   0  }, // W: Dangerous Hall, open X door (after reading note)
            { 21,    0  ,  20  ,   0  ,   0  ,   0  ,   0  },
            { 22,   20  ,   0  ,   0  ,   0  ,   0  ,   0  },
            { 23,    0  ,   0  ,  20  ,   0  ,   0  ,  -1  }, // D: unlock with oilcan
            { 24,    0  ,   0  ,   8  ,   0  ,  23  ,   0  },
            { 25,    0  ,   0  ,   0  ,   0  ,   0  ,  12  },
            { 26,    0  ,   0  ,  27  ,   0  ,   0  ,   0  },
            { 27,    0  ,   0  ,  28  ,  26  ,   0  ,   0  },
            { 28,    0  ,   0  ,  29  ,  27  ,   0  ,   0  },
            { 29,    0  ,   0  ,   0  ,  28  ,  -1  ,   0  }, // U: unlock with drop boxspring
            { 30,    0  ,   0  ,   0  ,   0  ,   0  ,   0  },
            { 31,    0  ,   0  ,   0  ,   0  ,   0  ,   0  }
        };
    }
}