# test split columns

def print_list(objList):
    #slice syntax is: start:stop:step
    for a,b,c in zip(objList[::3], objList[1::3], objList[2::3]):
            print('{:<20}{:<20}{:<}'.format(a,b,c))

print("One")
objList = [1]
print_list(objList)

print("Two")
objList = [1,2]
print_list(objList)

print("Three")
objList = [1,2,3]
print_list(objList)

print("Four")
objList = [1,2,3,4]
print_list(objList)
