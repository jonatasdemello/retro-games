import unittest

# from world import World
from player import Player
from user_input import User_Input

class UserInputTest(unittest.TestCase):
    # tests must start with "test_"

    # def setUp(self):
    #     self.myInput = User_Input()

    # def tearDown(self):
    #     self.widget.dispose()

    def test_input_one_verb(self):
        myInput = User_Input()
        verb = "GET"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 9)
        self.assertTrue(myInput.v_OB == 0)

    def test_input_verb_object(self):
        myInput = User_Input()
        verb = "GET AXE"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 9)
        self.assertTrue(myInput.v_OB == 13)

    def test_input_wrong_verb(self):
        myInput = User_Input()
        verb = "xyz"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 0)
        self.assertTrue(myInput.v_OB == 0)

    def test_input_two_wrong_words(self):
        myInput = User_Input()
        verb = "xyz 123"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 0)
        self.assertTrue(myInput.v_OB == 0)

    def test_input_two_wrong_valid_words(self):
        myInput = User_Input()
        verb = "DOOR COAT"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 0)
        self.assertTrue(myInput.v_OB == 30)

    def test_input_two_wrong_valid_words1(self):
        myInput = User_Input()
        verb = "DOOR DOOR"
        myInput.decode_input(verb)
        self.assertTrue(myInput.v_VB == 0)
        self.assertTrue(myInput.v_OB == 25)

#if __name__ == "__main__":
#    unittest.main()

# if __name__ == '__main__':
#     unittest.main(verbosity=2)

# 0 (quiet): you just get the total numbers of tests executed and the global result
# 1 (default): you get the same plus a dot for every successful test or a F for every failure
# 2 (verbose): you get the help string of every test and the result

suite = unittest.TestLoader().loadTestsFromTestCase(UserInputTest)
unittest.TextTestRunner(verbosity=2).run(suite)
