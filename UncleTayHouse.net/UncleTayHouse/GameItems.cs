using UncleTayHouse.Models;

namespace UncleTayHouse
{
    public class GameItems
    {
        // Describe Objects and their locations
        // ILOC[obj] => gameItems.houseItems[obj].location
        public List<GameItem> houseItems { get; } = new List<GameItem>
        {
            new GameItem { id = 0,  location = -99, objId =  0,  name = "(dummy)",      desc = "(dummy)" },
            new GameItem { id = 1,  location =  1,  objId = 34,  name = "NEWSPAPER",    desc = "Tays house unlikely ever to be sold. tales of gutted stairwells and booby traps have spooked buyers..." },
            new GameItem { id = 2,  location = -1,  objId = 35,  name = "TEDDYBEAR",    desc = "Someone has been playing very rough with this toy" },
            new GameItem { id = 3,  location = -1,  objId = 36,  name = "FUSE",         desc = "Old-fashioned electrical fuse" },
            new GameItem { id = 4,  location = 10,  objId = 37,  name = "JACK",         desc = "Tire jack for lifting heavy objects like cars" },
            new GameItem { id = 5,  location = 30,  objId = 38,  name = "PICTURE",      desc = "Uncle tays in all his sallow glory" },
            new GameItem { id = 6,  location = 10,  objId = 39,  name = "BUNGEE",       desc = "Cord for bungee jumping" },
            new GameItem { id = 7,  location = 13,  objId = 40,  name = "KEY",          desc = "A small brass key" },
            new GameItem { id = 8,  location = 15,  objId = 41,  name = "SPINNINGTOP",  desc = "A child's toy (spinning top)" },
            new GameItem { id = 9,  location =  9,  objId = 42,  name = "NOTE",         desc = "The writing is reversed. maybe there is a way to read it somewhere..." },
            new GameItem { id = 10, location = 16,  objId = 43,  name = "GAINESBURGER", desc = "Supposedly dog food, though it appears to be made of plastic" },
            new GameItem { id = 11, location = 22,  objId = 44,  name = "GLOVES",       desc = "Rubber gloves used for cleaning" },
            new GameItem { id = 12, location = 26,  objId = 45,  name = "BOXSPRING",    desc = "A queen-sized boxspring" },
            new GameItem { id = 13, location = 25,  objId = 46,  name = "BRACE",        desc = "A back brace" },
            new GameItem { id = 14, location = 25,  objId = 47,  name = "MAGAZINE",     desc = "Tays' strange inventions include booby-trapped doors and toys that open doors by remote control..." },
            new GameItem { id = 15, location = 28,  objId = 48,  name = "OILCAN",       desc = "This can contains fine lubricating oil" },
            new GameItem { id = 16, location =  8,  objId = 49,  name = "CHECKBOOK",    desc = "Uncle tays' checkbook lists a balance of $220,000" },
            new GameItem { id = 17, location = -1,  objId = 50,  name = "DIAMOND",      desc = "This diamond's beauty stems from all the goddamned money it is worth" },
            new GameItem { id = 18, location = 19,  objId = 51,  name = "LOVERBOY",     desc = "Loverboy's first album in vinyl, worth an incalculable sum" },
            new GameItem { id = 19, location = 21,  objId = 52,  name = "INVESTMENT",   desc = "Pre-ipo shares of apollo computing have to be worth ... something" },
            new GameItem { id = 20, location = 27,  objId = 53,  name = "LOONS",        desc = "A thick wad of canadian notes" },
            new GameItem { id = 21, location =  2,  objId = 54,  name = "FRIDGE",       desc = "This old refrigerator's motor labors heavily" },
            new GameItem { id = 22, location =  6,  objId = 55,  name = "COUCH",        desc = "An overstuffed, dusty couch" },
            new GameItem { id = 23, location =  7,  objId = 56,  name = "CLOTHES",      desc = "A disgusting pile of soiled laundry" },
            new GameItem { id = 24, location = -1,  objId = 57,  name = "DOOR",         desc = "3 doors" },
            new GameItem { id = 25, location = 12,  objId = 58,  name = "RAILING",      desc = "A railing or guardrail, is a system designed to keep people or objects from falling off the balcony." },
            new GameItem { id = 26, location = -1,  objId = 59,  name = "DUMBWAITER",   desc = "A dumbwaiter lift is a small freight elevator designed to transport goods, supplies, or food between different levels of a building." },
            new GameItem { id = 27, location = -1,  objId = 60,  name = "FUSEBOX",      desc = "An old-fashioned fusebox. the fuse marked 'attic' is missing." },
            new GameItem { id = 28, location = 22,  objId = 61,  name = "MIRROR",       desc = "A mirror in the wall" }
        };
        public GameItem GetObject(string obj)
        {
            return houseItems.FirstOrDefault(i => i.name.Equals(obj, StringComparison.OrdinalIgnoreCase));
        }

        // Describe Rooms
        public List<GameMap> houseMap { get; } = new List<GameMap>
        {
            new GameMap{ id = 0,  rname = "(dymmy)", rdesc = "(dymmy)" }, // for now we need this, will remove later
            new GameMap{ id = 1,  rname = "FOYER (LOBBY)", rdesc = "The entryway to the house" },
            new GameMap{ id = 2,  rname = "KITCHEN", rdesc = "Countertops are dusty and there are rusting pots and pans" },
            new GameMap{ id = 3,  rname = "SITTING ROOM", rdesc = "This room is two stories high and contains elegant chairs and couches" },
            new GameMap{ id = 4,  rname = "HALLWAY", rdesc = "A narrow hallway which runs west of the foyer" },
            new GameMap{ id = 5,  rname = "HALLWAY", rdesc = "A narrow hallway at the west end of the house, a door to north" },
            new GameMap{ id = 6,  rname = "DEN", rdesc = "This room has an ancient television" },
            new GameMap{ id = 7,  rname = "BATHROOM", rdesc = "A dingy bathroom with a cracked sink" },
            new GameMap{ id = 8,  rname = "LIBRARY", rdesc = "This well-furnished library is lined with books and leather furniture" },
            new GameMap{ id = 9,  rname = "SMALL BEDROOM", rdesc = "This small bedroom has a twin bed and chair. It looks little used" },
            new GameMap{ id = 10, rname = "GARAGE", rdesc = "The cavernous garage holds a non-operational gremlin and piles of junk" },
            new GameMap{ id = 11, rname = "DINING ROOM", rdesc = "Trophies line the walls. there are six chairs around a long table" },
            new GameMap{ id = 12, rname = "BALCONY", rdesc = "Balcony above the sitting room. a railing protects you from a 15-foot drop" },
            new GameMap{ id = 13, rname = "MASTER BEDROOM", rdesc = "This large corner bedroom has solid walnut furniture and a large mirror" },
            new GameMap{ id = 14, rname = "HALLWAY", rdesc = "A hallway with a large arch on its south side" },
            new GameMap{ id = 15, rname = "GAME ROOM", rdesc = "This elegant game room has a pool table and marble chessboard" },
            new GameMap{ id = 16, rname = "CLOSET", rdesc = "A spacious closet off the gameroom" },
            new GameMap{ id = 17, rname = "HALLWAY", rdesc = "A hallway in the center of the second floor" },
            new GameMap{ id = 18, rname = "CHILD'S ROOM", rdesc = "Your cousin's room in happier times, before he ran off to join the baath party" },
            new GameMap{ id = 19, rname = "SECRET ROOM", rdesc = "A dark chamber off the bedroom" },
            new GameMap{ id = 20, rname = "DANGEROUS HALL", rdesc = "This eerie hall has three identical doors on the west wall (left, center, right)" },
            new GameMap{ id = 21, rname = "CORNER BEDROOM", rdesc = "A cozy corner room with windows on two walls" },
            new GameMap{ id = 22, rname = "BATHROOM", rdesc = "An elegant bath with a mirror over a marble sink" },
            new GameMap{ id = 23, rname = "DUMBWAITER", rdesc = "A cramped dumbwaiter" },
            new GameMap{ id = 24, rname = "DUMBWAITER", rdesc = "A cramped dumbwaiter" },
            new GameMap{ id = 25, rname = "ATTIC", rdesc = "A dusty attic with low sloping walls" },
            new GameMap{ id = 26, rname = "STORAGE ROOM", rdesc = "A bare room used to store random equipment and furniture" },
            new GameMap{ id = 27, rname = "LAUNDRY", rdesc = "This room has a washer and dryer, as well as a boiler and furnace" },
            new GameMap{ id = 28, rname = "WORK ROOM", rdesc = "Equipment for working wood and metal" },
            new GameMap{ id = 29, rname = "BOTTOM OF STAIRS", rdesc = "Stairs from basement to kitchen" },
            new GameMap{ id = 30, rname = "MID-AIR", rdesc = "Hanging from a bungee cord" },
            new GameMap{ id = 31, rname = "LEAVE THE HOUSE", rdesc = "Leave the house and the game" }
        };
    }
}