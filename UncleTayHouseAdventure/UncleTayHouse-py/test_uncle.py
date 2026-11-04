import sys
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parent
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from uncle import UncleTaysHouseAdventure


class UncleTaysHouseAdventureTests(unittest.TestCase):
    def setUp(self):
        self.game = UncleTaysHouseAdventure()
        self.game.safe_door = "left"
        self.game.safe_door_index = {"left": 0, "center": 1, "right": 2}[self.game.safe_door]

    def test_initial_state_matches_room_one(self):
        self.assertEqual(self.game.player_room, 1)
        self.assertEqual(self.game.inventory, [])
        self.assertEqual(self.game.item_loc[1], 1)
        self.assertEqual(self.game.exits[1][0], 2)
        self.assertEqual(self.game.exits[1][2], 3)

    def test_take_item_updates_inventory(self):
        self.game.take_item("newspaper")

        self.assertIn("newspaper", self.game.inventory)
        self.assertEqual(self.game.item_loc[1], 0)

    def test_move_direction_changes_room(self):
        self.game.move_direction("north")

        self.assertEqual(self.game.player_room, 2)

    def test_jacking_the_fridge_reveals_fuse(self):
        self.game.player_room = 2
        self.game.move_item("fridge", "jack")

        self.assertEqual(self.game.item_loc[3], 2)

        self.game.take_item("fuse")
        self.assertIn("fuse", self.game.inventory)

    def test_put_fuse_in_fusebox_activates_balcony_exit(self):
        self.game.player_room = 12
        self.game.inventory = ["fuse"]
        self.game.item_loc[3] = 0
        self.game.put_in("fuse", "fusebox")

        self.assertTrue(self.game.fuse_installed)
        self.assertEqual(self.game.item_loc[3], -999)
        self.assertEqual(self.game.exits[12][4], 25)

    def test_bungee_jump_sequence(self):
        self.game.player_room = 12
        self.game.inventory = ["bungee"]
        self.game.item_loc[6] = 12

        self.game.tie_bungee("bungee", "railing")
        self.game.jump()

        self.assertEqual(self.game.player_room, 30)

        self.game.jump()
        self.assertEqual(self.game.player_room, 12)

    def test_safely_opening_the_left_door_opens_dumbwaiter(self):
        self.game.player_room = 17
        self.game.inventory = ["key"]
        self.game.item_loc[7] = 0

        self.game.unlock_door("door")
        self.assertEqual(self.game.exits[17][3], 20)

        self.game.player_room = 20
        self.game.open_door("left")
        self.assertEqual(self.game.exits[20][3], 23)

    def test_wrong_door_kills_the_player(self):
        self.game.player_room = 20
        self.game.safe_door = "right"
        self.game.safe_door_index = {"left": 0, "center": 1, "right": 2}[self.game.safe_door]

        self.game.open_door("left")

        self.assertTrue(self.game.dead)
        self.assertTrue(self.game.game_over)


if __name__ == "__main__":
    unittest.main()
