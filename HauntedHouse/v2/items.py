"""Describes the items in the game."""
__author__ = 'Phillip Johnson'

# bas class
class Item():
    """The base class for all items"""
    def __init__(self, id, name, description, value, location):
        self.id = id
        self.name = name
        self.description = description
        self.value = value
        self.location = location
        self.carrying = False

    def __str__(self):
        return "{}\n=====\n{}\nValue: {}\nLocation: {}\n"\
            .format(self.name, self.description, self.value, self.location)

    def use(self):
        """Each object should implement this method"""
        raise NotImplementedError()

# ojects starts here:

class Rope(Item):
    def __init__(self, loc):
        # location is sent when we initialize the world, so we can customize it
        # default object properties
        id = 14
        name = "Rope"
        value = 1
        description = "A 10m rope."
        super().__init__(id, name, description, value, loc)

    #def climb_1300(self):
    def use(self):
        # rope
        if self.v_C[14] == 1:
            self.v_MS = "rope IT ISN'T ATTACHED TO ANYTHING!"

        if self.v_C[14] != 1 and self.v_RM == 7 and self.v_F[14] == 0:
            self.v_MS = "YOU SEE THICK FOREST and CLIFF SOUTH"
            self.v_F[14] = 1
            return

        if self.v_C[14] != 1 and self.v_RM == 7 and self.v_F[14] == 1:
            self.v_MS = "GOING DOWN"
            self.v_F[14] = 0
            return

#
# class Weapon(Item):
#     def __init__(self, name, description, value, damage):
#         self.damage = damage
#         super().__init__(name, description, value,0)
# 
#     def __str__(self):
#         return "{}\n=====\n{}\nValue: {}\nDamage: {}".format(self.name, self.description, self.value, self.damage)
# 
# 
# class Gold(Item):
#     def __init__(self, loc):
#         self.location = loc
#         name="Gold"
#         description="A round coin with {} stamped on the front.".format(str(self.amt))
#         super().__init__(name, description,value,0)

class Painting(Item):
    def __init__(self, loc):
        id = 1
        name = "Painting"
        value = 1
        description = "Object: painting."
        super().__init__(id, name, description, value, loc)

class Ring(Item):
    def __init__(self, loc):
        id = 2
        name = "Ring"
        value = 1
        description = "Object: ring."
        super().__init__(id, name, description, value, loc)

class Magicspells(Item):
    def __init__(self, loc):
        id = 3
        name = "Magicspells"
        value = 1
        description = "Object: magicspells."
        super().__init__(id, name, description, value, loc)

class Goblet(Item):
    def __init__(self, loc):
        id = 4
        name = "Goblet"
        value = 1
        description = "Object: goblet."
        super().__init__(id, name, description, value, loc)

class Scroll(Item):
    def __init__(self, loc):
        id = 5
        name = "Scroll"
        value = 1
        description = "Object: scroll."
        super().__init__(id, name, description, value, loc)

class Coins(Item):
    def __init__(self, loc):
        id = 6
        name = "Coins"
        value = 10
        description = "Object: coins."
        super().__init__(id, name, description, value, loc)

class Statue(Item):
    def __init__(self, loc):
        id = 7
        name = "Statue"
        value = 15
        description = "Object: statue."
        super().__init__(id, name, description, value, loc)

class Candlestick(Item):
    def __init__(self, loc):
        id = 8
        name = "Candlestick"
        value = 1
        description = "Object: candlestick."
        super().__init__(id, name, description, value, loc)

class Matches(Item):
    def __init__(self, loc):
        id = 9
        name = "Matches"
        value = 1
        description = "Object: matches."
        super().__init__(id, name, description, value, loc)

class Vacuum(Item):
    def __init__(self, loc):
        id = 10
        name = "vacuum"
        value = 1
        description = "Object: vacuum."
        super().__init__(id, name, description, value, loc)

class Batteries(Item):
    def __init__(self, loc):
        id = 11
        name = "batteries"
        value = 1
        description = "Object: batteries."
        super().__init__(id, name, description, value, loc)

class Shovel(Item):
    def __init__(self, loc):
        id = 12
        name = "shovel"
        value = 1
        description = "Object: shovel."
        super().__init__(id, name, description, value, loc)

class Axe(Item):
    def __init__(self, loc):
        id = 13
        name = "axe"
        value = 1
        description = "Object: axe."
        super().__init__(id, name, description, value, loc)

class Rope(Item):
    def __init__(self, loc):
        id = 14
        name = "rope"
        value = 1
        description = "Object: rope."
        super().__init__(id, name, description, value, loc)

class Boat(Item):
    def __init__(self, loc):
        id = 15
        name = "boat"
        value = 1
        description = "Object: boat."
        super().__init__(id, name, description, value, loc)

class Aerosol(Item):
    def __init__(self, loc):
        id = 16
        name = "aerosol"
        value = 1
        description = "Object: aerosol."
        super().__init__(id, name, description, value, loc)

class Candle(Item):
    def __init__(self, loc):
        id = 17
        name = "candle"
        value = 1
        description = "Object: candle."
        super().__init__(id, name, description, value, loc)

class Key(Item):
    def __init__(self, loc):
        id = 18
        name = "key"
        value = 1
        description = "Object: key."
        super().__init__(id, name, description, value, loc)

class north(Item):
    def __init__(self, loc):
        id = 19
        name = "north"
        value = 1
        description = "Object: north."
        super().__init__(id, name, description, value, loc)

class south(Item):
    def __init__(self, loc):
        id = 20
        name = "south"
        value = 1
        description = "Object: south."
        super().__init__(id, name, description, value, loc)

class west(Item):
    def __init__(self, loc):
        id = 21
        name = "west"
        value = 1
        description = "Object: west."
        super().__init__(id, name, description, value, loc)

class east(Item):
    def __init__(self, loc):
        id = 22
        name = "east"
        value = 1
        description = "Object: east."
        super().__init__(id, name, description, value, loc)

class up(Item):
    def __init__(self, loc):
        id = 23
        name = "up"
        value = 1
        description = "Object: up."
        super().__init__(id, name, description, value, loc)

class down(Item):
    def __init__(self, loc):
        id = 24
        name = "down"
        value = 1
        description = "Object: down."
        super().__init__(id, name, description, value, loc)

class Door(Item):
    def __init__(self, loc):
        id = 25
        name = "door"
        value = 1
        description = "Object: door."
        super().__init__(id, name, description, value, loc)

class Bats(Item):
    def __init__(self, loc):
        id = 26
        name = "bats"
        value = 1
        description = "Object: bats."
        super().__init__(id, name, description, value, loc)

class Ghosts(Item):
    def __init__(self, loc):
        id = 27
        name = "ghosts"
        value = 1
        description = "Object: ghosts."
        super().__init__(id, name, description, value, loc)

class Drawer(Item):
    def __init__(self, loc):
        id = 28
        name = "drawer"
        value = 1
        description = "Object: drawer."
        super().__init__(id, name, description, value, loc)

class Desk(Item):
    def __init__(self, loc):
        id = 29
        name = "desk"
        value = 1
        description = "Object: desk."
        super().__init__(id, name, description, value, loc)

class Coat(Item):
    def __init__(self, loc):
        id = 30
        name = "coat"
        value = 1
        description = "Object: coat."
        super().__init__(id, name, description, value, loc)

class Rubbish(Item):
    def __init__(self, loc):
        id = 31
        name = "rubbish"
        value = 1
        description = "Object: rubbish."
        super().__init__(id, name, description, value, loc)

class Coffin(Item):
    def __init__(self, loc):
        id = 32
        name = "coffin"
        value = 1
        description = "Object: coffin."
        super().__init__(id, name, description, value, loc)

class Books(Item):
    def __init__(self, loc):
        id = 33
        name = "books"
        value = 1
        description = "Object: books."
        super().__init__(id, name, description, value, loc)

class Xzanfar(Item):
    def __init__(self, loc):
        id = 34
        name = "xzanfar"
        value = 1
        description = "Object: xzanfar."
        super().__init__(id, name, description, value, loc)

class Wall(Item):
    def __init__(self, loc):
        id = 35
        name = "wall"
        value = 1
        description = "Object: wall."
        super().__init__(id, name, description, value, loc)

class Spells(Item):
    def __init__(self, loc):
        id = 36
        name = "spells"
        value = 1
        description = "Object: spells."
        super().__init__(id, name, description, value, loc)

