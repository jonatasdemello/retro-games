Test:
python.exe -m unittest
python3 -m unittest discover

python3 -m unittest discover -s stock_alerter
python3 -m unittest discover -s stock_alerter -t .

-s start_directory: Specify the start directory from where the discovery
should start. This defaults to the current directory.

-t top_directory: Specify the top-level directory. This is the directory from
which imports are performed. This is important if the start directory is inside
the package and you get errors due to incorrect imports. This defaults to the
start directory.

-p file_pattern: The file pattern that identifies test files. By default
it checks for python files that start with test. If we name our test files
something else (for example, stock_test.py), then we have to pass
in this parameter so that the file is correctly identified as a test file.




https://pypi.org/project/colorama/
https://www.geeksforgeeks.org/print-colors-python-terminal/

https://stackabuse.com/reading-and-writing-lists-to-a-file-in-python/

import json

dict1 = {}
dict2 = {}

with open('test.json', 'w') as test:
    json.dump([dict1,dict2], test)

with open('test.json','r') as test:
    x = json.load(test)

with open('test.json', 'w') as test:
    x.append(dict2)
    json.dump(x,test)
	
	