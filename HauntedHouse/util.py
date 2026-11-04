# helper functions
import random
from colorama import Fore, Back, Style
import util
import constants

def split(word):
    return [char for char in word]

def rnd(num):
    return random.randrange(1, num)

def console(str1 = "", str2 = ""):
    result = "\t" + str(str1)
    s = str(str2)
    result = result.ljust(30,".") +"\t"+ s
    print(Fore.CYAN + result + Style.RESET_ALL)
    return

def console_list(str1, list):
    # for (i, item) in enumerate(self.v_L, start=0):
    #     print(i, item)
    result = "\t" + str(str1)
    print(Fore.CYAN + result + Style.RESET_ALL)

    string_list = ["%i: %s" % (index, value) for index, value in enumerate(list)]
    print(string_list)

    # formatted_string = "\n".join(string_list)
    # print(formatted_string)

def trace(text1, text2 = ""):
    if(constants.show_trace):
        util.console(text1, text2)

