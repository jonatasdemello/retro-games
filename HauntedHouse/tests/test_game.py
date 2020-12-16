import unittest

# from world import World
from player import Player
from user_input import User_Input

class UserInputTest(unittest.TestCase):
    # tests must start with "test_"

    def test_input_one_verb(self):
        myInput = User_Input()
        verb = "GET"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 9)
        self.assertTrue(myInput.v_OB == 0)



#if __name__ == "__main__":
#    unittest.main()

# 0 (quiet): you just get the total numbers of tests executed and the global result
# 1 (default): you get the same plus a dot for every successful test or a F for every failure
# 2 (verbose): you get the help string of every test and the result

suite = unittest.TestLoader().loadTestsFromTestCase(UserInputTest)
unittest.TextTestRunner(verbosity=2).run(suite)
