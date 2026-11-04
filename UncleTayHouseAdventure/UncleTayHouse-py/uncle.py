#!/usr/bin/env python3
"""A closer Python port of Uncle Tays' House Adventure."""

from __future__ import annotations

import random
import re

DIRECTION_ORDER = ["north", "south", "east", "west", "up", "down"]

DIRECTION_INDEX = {
    "north": 0,
    "south": 1,
    "east": 2,
    "west": 3,
    "up": 4,
    "down": 5,
    "n": 0,
    "s": 1,
    "e": 2,
    "w": 3,
    "u": 4,
    "d": 5,
}

ROOM_NAMES = {
    1: "FOYER",
    2: "KITCHEN",
    3: "SITTING ROOM",
    4: "HALLWAY",
    5: "HALLWAY",
    6: "DEN",
    7: "BATHROOM",
    8: "LIBRARY",
    9: "SMALL BEDROOM",
    10: "GARAGE",
    11: "DINING ROOM",
    12: "BALCONY",
    13: "MASTER BEDROOM",
    14: "HALLWAY",
    15: "GAME ROOM",
    16: "CLOSET",
    17: "HALLWAY",
    18: "CHILD'S ROOM",
    19: "SECRET ROOM",
    20: "DANGEROUS HALL",
    21: "CORNER BEDROOM",
    22: "BATHROOM",
    23: "DUMBWAITER",
    24: "DUMBWAITER",
    25: "ATTIC",
    26: "STORAGE ROOM",
    27: "LAUNDRY",
    28: "WORKROOM",
    29: "BOTTOM OF STAIRS",
    30: "MID-AIR",
    31: "LEAVE THE HOUSE",
}

ROOM_DESCRIPTIONS = {
    1: "THE ENTRYWAY TO THE HOUSE.",
    2: "COUNTERTOPS ARE DUSTY AND THERE ARE RUSTING POTS AND PANS.",
    3: "THIS ROOM IS TWO STORIES HIGH AND CONTAINS ELEGANT CHAIRS AND COUCHES.",
    4: "A NARROW HALLWAY WHICH RUNS WEST OF THE FOYER.",
    5: "A NARROW HALLWAY AT THE WEST END OF THE HOUSE.",
    6: "THIS ROOM HAS AN ANCIENT TELEVISION.",
    7: "A DINGY BATHROOM WITH A CRACKED SINK.",
    8: "THIS WELL-FURNISHED LIBRARY IS LINED WITH BOOKS AND LEATHER FURNITURE.",
    9: "THIS SMALL BEDROOM HAS A TWIN BED AND CHAIR. IT LOOKS LITTLE USED.",
    10: "THE CAVERNOUS GARAGE HOLDS A NON-OPERATIONAL GREMLIN AND PILES OF JUNK.",
    11: "TROPHIES LINE THE WALLS. THERE ARE SIX CHAIRS AROUND A LONG TABLE.",
    12: "BALCONY ABOVE THE SITTING ROOM. A RAILING PROTECTS YOU FROM A 15-FOOT DROP.",
    13: "THIS LARGE CORNER BEDROOM HAS SOLID WALNUT FURNITURE AND A LARGE MIRROR.",
    14: "A HALLWAY WITH A LARGE ARCH ON ITS SOUTH SIDE.",
    15: "THIS ELEGANT GAME ROOM HAS A POOL TABLE AND MARBLE CHESSBOARD.",
    16: "A SPACIOUS CLOSET OFF THE GAMEROOM.",
    17: "A HALLWAY IN THE CENTER OF THE SECOND FLOOR.",
    18: "YOUR COUSIN'S ROOM IN HAPPIER TIMES, BEFORE HE RAN OFF TO JOIN THE BAATH PARTY.",
    19: "A DARK CHAMBER OFF THE BEDROOM.",
    20: "THIS EERIE HALL HAS THREE IDENTICAL DOORS ON THE WEST WALL.",
    21: "A COZY CORNER ROOM WITH WINDOWS ON TWO WALLS.",
    22: "AN ELEGANT BATH WITH A MIRROR OVER A MARBLE SINK.",
    23: "A CRAMPED DUMBWAITER.",
    24: "A CRAMPED DUMBWAITER.",
    25: "A DUSTY ATTIC WITH LOW SLOPING WALLS.",
    26: "A BARE ROOM USED TO STORE RANDOM EQUIPMENT AND FURNITURE.",
    27: "THIS ROOM HAS A WASHER AND DRYER, AS WELL AS A BOILER AND FURNACE.",
    28: "EQUIPMENT FOR WORKING WOOD AND METAL.",
    29: "STAIRS FROM BASEMENT TO KITCHEN.",
    30: "HANGING FROM A BUNGEE CORD.",
    31: "",
}

ROOM_EXITS = {
    1: [2, 31, 3, 4, 0, 0],
    2: [0, 1, 0, 0, 0, -1],
    3: [2, 0, 11, 1, 12, 0],
    4: [6, 7, 1, 5, 0, 0],
    5: [0, 9, 4, 10, 0, 0],
    6: [0, 4, 0, 0, 0, 0],
    7: [4, 0, 0, 0, 0, -1],
    8: [0, 0, 0, 24, 0, 0],
    9: [0, 0, 0, 0, 0, 0],
    10: [0, 0, 5, 0, 0, 0],
    11: [0, 0, 0, 3, 0, 0],
    12: [13, 0, 0, 14, -1, 3],
    13: [0, 12, 0, 0, 0, 0],
    14: [0, 15, 12, 17, 0, 0],
    15: [14, 0, 0, 16, 0, 0],
    16: [0, 0, 15, 0, 0, 0],
    17: [-1, 0, 14, -1, 0, 0],
    18: [0, 17, -1, 0, 0, 0],
    19: [0, 0, 0, 18, 0, 0],
    20: [21, 22, 17, -1, 0, 0],
    21: [0, 20, 0, 0, 0, 0],
    22: [20, 0, 0, 0, 0, 0],
    23: [0, 0, 20, 0, 0, -1],
    24: [0, 0, 8, 0, 23, 0],
    25: [0, 0, 0, 0, 0, 12],
    26: [0, 0, 27, 0, 0, 0],
    27: [0, 0, 28, 26, 0, 0],
    28: [0, 0, 29, 27, 0, 0],
    29: [0, 0, 0, 28, -1, 0],
    30: [0, 0, 0, 0, 0, 0],
    31: [0, 0, 0, 0, 0, 0],
}

ITEMS = {
    "newspaper": 1,
    "teddybear": 2,
    "fuse": 3,
    "jack": 4,
    "picture": 5,
    "bungee": 6,
    "key": 7,
    "top": 8,
    "note": 9,
    "gainesburger": 10,
    "gainsburger": 10,
    "gaineburger": 10,
    "gainesbuger": 10,
    "gloves": 11,
    "boxspring": 12,
    "brace": 13,
    "magazine": 14,
    "oilcan": 15,
    "checkbook": 16,
    "diamond": 17,
    "loverboy": 18,
    "investment": 19,
    "loons": 20,
    "fridge": 21,
    "couch": 22,
    "clothes": 23,
    "door": 24,
    "railing": 25,
    "dumbwaiter": 26,
    "fusebox": 27,
}

ITEM_NAME = {
    1: "newspaper",
    2: "teddybear",
    3: "fuse",
    4: "jack",
    5: "picture",
    6: "bungee",
    7: "key",
    8: "top",
    9: "note",
    10: "gainesburger",
    11: "gloves",
    12: "boxspring",
    13: "brace",
    14: "magazine",
    15: "oilcan",
    16: "checkbook",
    17: "diamond",
    18: "loverboy",
    19: "investment",
    20: "loons",
    21: "fridge",
    22: "couch",
    23: "clothes",
    24: "door",
    25: "railing",
    26: "dumbwaiter",
    27: "fusebox",
}

ITEM_DESCRIPTION = {
    1: "TAYS HOUSE UNLIKELY EVER TO BE SOLD. TALES OF GUTTED STAIRWELLS AND BOOBY TRAPS HAVE SPOOKED BUYERS...",
    2: "SOMEONE HAS BEEN PLAYING VERY ROUGH WITH THIS TOY.",
    3: "AN OLD-FASHIONED ELECTRICAL FUSE.",
    4: "TIRE JACK FOR LIFTING HEAVY OBJECTS LIKE CARS.",
    5: "UNCLE TAYS IN ALL HIS SALLOW GLORY.",
    6: "CORD FOR BUNGEE JUMPING.",
    7: "A SMALL BRASS KEY.",
    8: "A CHILD'S TOY.",
    9: "THE WRITING IS REVERSED.",
    10: "SUPPOSEDLY DOG FOOD, THOUGH IT APPEARS TO BE MADE OF PLASTIC.",
    11: "RUBBER GLOVES USED FOR CLEANING.",
    12: "A QUEEN-SIZED BOXSPRING.",
    13: "A BACK BRACE.",
    14: "TAYS' STRANGE INVENTIONS INCLUDE BOOBY-TRAPPED DOORS AND TOYS THAT OPEN DOORS BY REMOTE CONTROL...",
    15: "THIS CAN CONTAINS FINE LUBRICATING OIL.",
    16: "UNCLE TAYS' CHECKBOOK LISTS A BALANCE OF $220,000.",
    17: "THIS DIAMOND'S BEAUTY STEMS FROM ALL THE GODDAMNED MONEY IT IS WORTH.",
    18: "LOVERBOY'S FIRST ALBUM IN VINYL, WORTH AN INCALCULABLE SUM.",
    19: "PRE-IPO SHARES OF APOLLO COMPUTING HAVE TO BE WORTH ... SOMETHING.",
    20: "A THICK WAD OF CANADIAN NOTES.",
    21: "THIS OLD REFRIGERATOR'S MOTOR LABORS HEAVILY.",
    22: "AN OVERSTUFFED, DUSTY COUCH.",
    23: "A DISGUSTING PILE OF SOILED LAUNDRY.",
    24: "THREE DOORS STARE BACK AT YOU.",
    25: "A RAILING PROTECTS YOU FROM A 15-FOOT DROP.",
    26: "A DUMBWAITER LIFT USED TO MOVE GOODS BETWEEN FLOORS.",
    27: "AN OLD-FASHIONED FUSEBOX. THE FUSE MARKED 'ATTIC' IS MISSING.",
}

ITEM_START_LOC = {
    1: 1,
    2: -1,
    3: -1,
    4: 10,
    5: 30,
    6: 10,
    7: 13,
    8: 15,
    9: 9,
    10: 16,
    11: 22,
    12: 26,
    13: 25,
    14: 25,
    15: 28,
    16: 8,
    17: -1,
    18: 19,
    19: 21,
    20: 27,
    21: 2,
    22: 6,
    23: 7,
    24: -1,
    25: 12,
    26: -1,
    27: -1,
}

EXTRA_DESCRIPTIONS = {
    5: "THERE IS A LOCKED DOOR TO THE NORTH.",
    8: "THERE IS A LOCKED DOOR TO THE SOUTH.",
    2: "STAIRS LEAD DOWN TO A CELLAR. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
    29: "STAIRS LEAD UP. SEVERAL STEPS HAVE COLLAPSED, MAKING THE STAIRCASE UNUSABLE.",
    12: "DARK STAIRS LEAD UP TO THE ATTIC.",
    17: "A LOCKED DOOR TO THE WEST IS LABELLED 'EXTREME DANGER'.",
    1: "YOUR UNCLE'S DOBERMAN BLOCKS A DOORWAY TO THE NORTH.",
}

IGNORED_WORDS = {"the", "to", "with", "using", "in", "into"}


class UncleTaysHouseAdventure:
    def __init__(self) -> None:
        self.player_room = 1
        self.inventory: list[str] = []
        self.item_loc = dict(ITEM_START_LOC)
        self.exits = {room: list(exits) for room, exits in ROOM_EXITS.items()}
        self.safe_door = random.choice(["left", "center", "right"])
        self.safe_door_index = {"left": 0, "center": 1, "right": 2}[self.safe_door]
        self.game_over = False
        self.dead = False
        self.bungee_tied = False
        self.fuse_installed = False
        self.oil_applied = False

    def intro(self) -> None:
        print()
        print("TAYS HOUSE ADVENTURE")
        print("--------------------")
        print("FIND TREASURES AND VALUABLES IN YOUR MAD UNCLE TAYS' HOUSE.")
        print("TYPE SIMPLE COMMANDS: NORTH, SOUTH, ETC. TO MOVE (OR JUST 'N', 'S').")
        print("TAKE AND DROP, INVENTORY, LOOK, READ, MOVE, AND SO ON.")
        print("SOME COMMANDS ARE COMPLEX: 'MOVE THE HUBCAP WITH THE SPANNER'")
        print()

    def item_id_for(self, raw_name: str) -> int | None:
        key = raw_name.strip().lower()
        if key in ITEMS:
            return ITEMS[key]
        for name, idx in ITEMS.items():
            if name.startswith(key):
                return idx
        return None

    def normalize_tokens(self, command: str) -> list[str]:
        words = re.findall(r"[A-Za-z]+", command.lower())
        return [w for w in words if w not in IGNORED_WORDS]

    def list_inventory(self) -> None:
        if not self.inventory:
            print("YOU ARE CARRYING NOTHING.")
            return
        print("YOU ARE CARRYING:")
        for item in self.inventory:
            print(f" - {item.upper()}")

    def score(self) -> int:
        total = 50
        for idx in range(16, 21):
            if self.item_loc.get(idx) == 0:
                total += 10
        for exits in self.exits.values():
            for target in exits:
                if target == -1:
                    total -= 5
        for idx in range(1, 16):
            if self.item_loc.get(idx) == -1:
                total -= 5
        return max(0, total)

    def show_score(self) -> None:
        print(f"YOUR SCORE IS {self.score()} OUT OF A POSSIBLE 100.")

    def show_room(self) -> None:
        room = self.player_room
        print(f"[{room}] {ROOM_NAMES.get(room, 'ROOM')}: {ROOM_DESCRIPTIONS.get(room, '')}")

        for idx, direction in enumerate(DIRECTION_ORDER):
            target = self.exits.get(room, [0, 0, 0, 0, 0, 0])[idx]
            if target > 0:
                print(f" > {direction.upper()}: {ROOM_NAMES.get(target, 'UNKNOWN')}")

        for special_room, text in EXTRA_DESCRIPTIONS.items():
            if special_room != room:
                continue
            if room == 5 and self.exits[5][0] <= 0:
                print(text)
            elif room == 8 and self.exits[8][1] <= 0:
                print(text)
            elif room == 2 and self.exits[2][5] <= 0:
                print(text)
            elif room == 29 and self.exits[29][4] <= 0:
                print(text)
            elif room == 12 and self.exits[12][4] <= 0:
                print(text)
            elif room == 17 and self.exits[17][3] <= 0:
                print(text)
            elif room == 1 and self.exits[1][0] <= 0:
                print(text)

        if room == 17 and self.exits[17][0] > 0:
            print("YOUR UNCLE'S DOBERMAN IS SNORING PEACEFULLY.")

        if room == 3 and self.item_loc.get(6) == -12:
            print("A BUNGEE CORD DANGLES FROM THE RAILING ABOVE.")
        if room == 12 and self.item_loc.get(6) == -12:
            print("A BUNGEE CORD DANGLES FROM THE RAILING.")

        for item_id in sorted(self.item_loc):
            if self.item_loc[item_id] == room and item_id not in {21, 22, 23, 24, 25, 26, 27}:
                print(f"THERE IS A {ITEM_NAME[item_id].upper()} HERE.")

        if room == 2 and self.item_loc.get(3) == -1:
            print("SOMETHING IS BARELY VISIBLE UNDER THE FRIDGE.")
        if room == 3 and self.item_loc.get(5) == 30:
            print("THERE IS A PICTURE HIGH UP ON THE WALL.")

    def take_item(self, item_name: str) -> None:
        item_id = self.item_id_for(item_name)
        if item_id is None:
            print("HUH?")
            return
        if item_id in {21, 22, 23, 24, 25, 26, 27}:
            print("YOU CAN JUST TAKE THAT.")
            return
        if self.item_loc.get(item_id) == 0:
            print("YOU ALREADY HAVE IT.")
            return
        if self.item_loc.get(item_id) not in {self.player_room, -1}:
            print("IT'S NOT HERE.")
            return
        if item_id == 5 and self.player_room == 3:
            print("IT'S TOO HIGH.")
            return

        self.item_loc[item_id] = 0
        self.inventory.append(item_name)
        print(f"TAKEN: {item_name.upper()}.")

    def drop_item(self, item_name: str) -> None:
        item_id = self.item_id_for(item_name)
        if item_id is None:
            print("HUH?")
            return
        if item_name not in self.inventory:
            print("YOU DON'T HAVE IT!")
            return

        self.inventory.remove(item_name)
        self.item_loc[item_id] = self.player_room
        print(f"DROPPED: {item_name.upper()}.")

        if item_name == "teddybear" and self.player_room == 17 and self.exits[17][0] <= 0:
            self.exits[17][0] = 18
            print("THE DOG CHEWS HIS FAVORITE TOY AND IS SOON ASLEEP.")

        if item_name == "boxspring" and self.player_room == 29 and self.exits[29][4] <= 0:
            self.exits[29][4] = 2
            self.exits[2][5] = 29
            print("THE BOXSPRING COVERS THE GAP IN THE STAIRS.")

    def read_item(self, item_name: str) -> None:
        item_id = self.item_id_for(item_name)
        if item_id is None:
            print("HUH?")
            return
        if self.item_loc.get(item_id) not in {0, self.player_room}:
            print("IT'S NOT HERE.")
            return

        if item_name == "note":
            if self.safe_door == "left":
                clue = "CENTER AND RIGHT"
            elif self.safe_door == "center":
                clue = "LEFT AND RIGHT"
            else:
                clue = "LEFT AND CENTER"
            print(f"EXPERIMENTS ON {clue} DOORS PROCEEDING WELL; FILE FOR PATENT.")
            return

        print(ITEM_DESCRIPTION[item_id])

    def move_item(self, target_name: str, tool_name: str) -> None:
        target_id = self.item_id_for(target_name)
        tool_id = self.item_id_for(tool_name)
        if target_id is None or tool_id is None:
            print("HUH?")
            return

        if target_name == "fridge" and tool_name == "jack" and self.player_room == 2:
            if self.item_loc.get(3) != -1:
                print("YOU CAN'T DO THAT.")
                return
            self.item_loc[3] = 2
            print("YOU JACK UP THE FRIDGE AND FIND A FUSE UNDER IT.")
            return

        if target_name == "couch" and tool_name == "brace" and self.player_room == 6:
            if self.item_loc.get(2) != -1:
                print("YOU CAN'T DO THAT.")
                return
            self.item_loc[2] = 6
            print("YOU MOVE THE COUCH AND FIND A TEDDYBEAR BEHIND IT.")
            return

        if target_name == "clothes" and tool_name == "gloves" and self.player_room == 7:
            self.exits[7][5] = 27
            print("MOVING THE CLOTHES REVEALS A LAUNDRY CHUTE TO THE BASEMENT.")
            return

        print("YOU CAN'T DO THAT.")

    def unlock_door(self, object_name: str) -> None:
        if object_name != "door":
            print("YOU CAN'T UNLOCK THAT.")
            return
        if self.player_room != 17:
            print("YOU CAN'T DO THAT.")
            return
        if self.item_id_for("key") not in {self.item_id_for(name) for name in self.inventory}:
            print("YOU DON'T HAVE A KEY!")
            return
        self.exits[17][3] = 20
        print("YOU UNLOCK THE DOOR. BEWARE!")

    def open_door(self, direction: str) -> None:
        if self.player_room != 20:
            print("YOU CAN'T DO THAT.")
            return

        door_name = "center" if direction == "middle" else direction
        if door_name not in {"left", "center", "right"}:
            print("PLEASE SPECIFY LEFT, CENTER, OR RIGHT.")
            return

        door_index = {"left": 0, "center": 1, "right": 2}[door_name]
        if door_index == self.safe_door_index:
            self.exits[20][3] = 23
            print("OPENING THE DOOR REVEALS A DUMBWAITER.")
            return

        print("A SHOT RINGS OUT! IT WAS WELL-AIMED TOO.")
        print("YOU HAVE DIED.")
        self.dead = True
        self.game_over = True

    def oil_dumbwaiter(self, item_name: str) -> None:
        if item_name != "dumbwaiter":
            print("YOU CAN'T DO THAT.")
            return
        if self.player_room != 23:
            print("YOU CAN'T DO THAT.")
            return
        if "oilcan" not in self.inventory:
            print("YOU DON'T HAVE ANY OIL.")
            return
        self.exits[23][5] = 24
        self.oil_applied = True
        print("THE DUMBWAITER MECHANISM NOW RUNS SMOOTHLY.")

    def tie_bungee(self, item_name: str, target_name: str) -> None:
        if item_name != "bungee" or target_name != "railing":
            print("YOU CAN'T TIE TO THAT.")
            return
        if self.player_room != 12:
            print("YOU CAN'T TIE THAT.")
            return
        if "bungee" not in self.inventory:
            print("YOU DON'T HAVE IT!")
            return

        self.bungee_tied = True
        self.inventory.remove("bungee")
        self.item_loc[6] = -12
        print("TIED.")

    def put_in(self, item_name: str, destination: str) -> None:
        if item_name != "fuse" or destination != "fusebox":
            print("YOU CAN'T PUT IT THERE.")
            return
        if "fuse" not in self.inventory:
            print("YOU DON'T HAVE IT!")
            return
        if self.player_room not in {12, 30}:
            print("YOU CAN'T DO THAT.")
            return

        self.inventory.remove("fuse")
        self.item_loc[3] = -999
        self.fuse_installed = True
        self.exits[12][4] = 25
        print("YOU PUT THE FUSE IN THE BOX.")

    def jump(self) -> None:
        if self.player_room == 12 and self.bungee_tied:
            self.player_room = 30
            print("YOU BUNGEE OFF THE BALCONY.")
            return
        if self.player_room == 30:
            print("...AND SPRING BACK.")
            self.player_room = 12
            return
        print("YOU CAN'T DO THAT.")

    def eat(self, item_name: str) -> None:
        if item_name != "gainesburger":
            print("YOU CAN'T EAT THAT.")
            return
        if "gainesburger" not in self.inventory:
            print("YOU DON'T HAVE IT.")
            return
        self.inventory.remove("gainesburger")
        print("YOU EAT THE GAINESBURGER. IT TASTES LIKE A SWEET, FATTY MEMORY.")

    def spin_top(self, item_name: str) -> None:
        if item_name != "top":
            print("YOU CAN'T SPIN THAT.")
            return
        if self.player_room != 18:
            print("YOU CAN'T DO THAT.")
            return
        self.exits[18][2] = 19
        print("THE TOP SPINS, AND A SECRET DOOR SWINGS OPEN.")

    def move_direction(self, direction: str) -> None:
        direction_key = DIRECTION_INDEX.get(direction)
        if direction_key is None:
            print("HUH?")
            return

        target = self.exits.get(self.player_room, [0, 0, 0, 0, 0, 0])[direction_key]
        if target > 0:
            self.player_room = target
            print(f"YOU MOVE {direction.upper()}.")
            if self.player_room == 31:
                self.game_over = True
                self.show_score()
                if self.score() == 100:
                    print("YOU HAVE WON THE GAME!")
                return
            self.show_room()
            return

        if self.player_room == 12 and direction == "up" and not self.fuse_installed:
            print("YOU'RE AFRAID OF THE DARK.")
            return
        if self.player_room == 17 and direction == "north":
            print("YOU NEVER DID LIKE THAT DOG.")
            return
        if self.player_room == 23 and direction == "down" and not self.oil_applied:
            print("THE DUMBWAITER MECHANISM IS CORRODED AND WON'T MOVE.")
            return
        print("YOU CAN'T GO THAT WAY.")

    def help(self) -> None:
        self.intro()
        print("Try: NORTH, TAKE KEY, READ NOTE, MOVE FRIDGE WITH JACK, OPEN CENTER DOOR, SCORE.")

    def process(self, raw_input: str) -> None:
        if self.game_over:
            return

        tokens = self.normalize_tokens(raw_input)
        if not tokens:
            return

        head = tokens[0]

        if head in DIRECTION_INDEX:
            self.move_direction(head)
            return
        if head in {"i", "inventory"}:
            self.list_inventory()
            return
        if head == "score":
            self.show_score()
            return
        if head == "help":
            self.help()
            return
        if head == "look":
            if len(tokens) > 1:
                self.read_item(tokens[1])
            else:
                self.show_room()
            return
        if head in {"read", "examine"}:
            if len(tokens) < 2:
                print("READ WHAT?")
                return
            self.read_item(tokens[1])
            return
        if head == "take":
            if len(tokens) < 2:
                print("TAKE WHAT?")
                return
            self.take_item(tokens[1])
            return
        if head == "drop":
            if len(tokens) < 2:
                print("DROP WHAT?")
                return
            self.drop_item(tokens[1])
            return
        if head == "move":
            if len(tokens) < 2:
                print("MOVE WHAT?")
                return
            if len(tokens) >= 4 and tokens[2] in {"with", "using"}:
                self.move_item(tokens[1], tokens[3])
                return
            if len(tokens) >= 3:
                self.move_item(tokens[1], tokens[2])
                return
            print("YOU CAN'T MOVE THAT.")
            return
        if head == "unlock":
            if len(tokens) < 2:
                print("UNLOCK WHAT?")
                return
            self.unlock_door(tokens[1])
            return
        if head == "open":
            if len(tokens) < 2:
                print("OPEN WHAT?")
                return
            if tokens[1] in {"left", "center", "right", "middle"}:
                self.open_door(tokens[1])
            else:
                print("PLEASE SPECIFY LEFT, CENTER, OR RIGHT.")
            return
        if head == "oil":
            if len(tokens) < 2:
                print("OIL WHAT?")
                return
            self.oil_dumbwaiter(tokens[1])
            return
        if head == "tie":
            if len(tokens) < 3:
                print("TIE WHAT TO WHAT?")
                return
            self.tie_bungee(tokens[1], tokens[2])
            return
        if head == "put":
            if len(tokens) < 3:
                print("PUT WHAT WHERE?")
                return
            destination = tokens[2]
            if len(tokens) >= 4 and tokens[2] in {"in", "into"}:
                destination = tokens[3]
            self.put_in(tokens[1], destination)
            return
        if head == "jump":
            self.jump()
            return
        if head == "eat":
            if len(tokens) < 2:
                print("EAT WHAT?")
                return
            self.eat(tokens[1])
            return
        if head == "spin":
            if len(tokens) < 2:
                print("SPIN WHAT?")
                return
            self.spin_top(tokens[1])
            return

        print("HUH?")

    def play(self) -> None:
        self.intro()
        self.show_room()
        while not self.game_over:
            try:
                raw = input("] ")
            except EOFError:
                print()
                break
            if not raw.strip():
                continue
            self.process(raw)
            if self.dead:
                self.game_over = True
                return


def main() -> None:
    game = UncleTaysHouseAdventure()
    game.play()


if __name__ == "__main__":
    main()
