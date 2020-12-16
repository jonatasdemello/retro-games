import unittest

# from world import World
from player import Player
from user_input import User_Input

class PlayerTest(unittest.TestCase):
    # tests must start with "test_"

    def test_player_location(self):
        myPlayer = Player()
        location = myPlayer.v_RM
        self.assertIsNotNone(location)
        self.assertEqual(location, 32)

    # def test_score(self):
    #     myPlayer = Player()
    #     myPlayer.score_1510()
    #     self.assertTrue(myPlayer.score == 9)

#if __name__ == "__main__":
#    unittest.main()

# 0 (quiet): you just get the total numbers of tests executed and the global result
# 1 (default): you get the same plus a dot for every successful test or a F for every failure
# 2 (verbose): you get the help string of every test and the result

suite = unittest.TestLoader().loadTestsFromTestCase(PlayerTest)
unittest.TextTestRunner(verbosity=2).run(suite)
