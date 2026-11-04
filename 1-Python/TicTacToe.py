from random import randrange
import sys

board = [[1,2,3],[4,'X',6],[7,8,9]]
print(type(board[1][0]))

def display_board(board):
    print('+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('|',' ',' ',' ',board[0][0],' ',' ',' ','|',' ',' ',' ',board[0][1],' ',' ',' ','|',' ',' ',' ',board[0][2],' ',' ',' ','|')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('|',' ',' ',' ',board[1][0],' ',' ',' ','|',' ',' ',' ',board[1][1],' ',' ',' ','|',' ',' ',' ',board[1][2],' ',' ',' ','|')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('|',' ',' ',' ',board[2][0],' ',' ',' ','|',' ',' ',' ',board[2][1],' ',' ',' ','|',' ',' ',' ',board[2][2],' ',' ',' ','|')
    print('|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|',' ',' ',' ',' ',' ',' ',' ','|')
    print('+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+','-','-','-','-','-','-','-','+')

def enter_move(board):
    display_board(board)
    found = False
    # The function accepts the board current status, asks the user about their move, 
    # checks the input and updates the board according to the user's decision.
    # tentando achar o numero na lista. se achar, mude a posicao
    move = int(input("what is your move? (between 1 and 9): "))
    if move <= 0 or move > 9:
        move = int(input("invalid move, try again. (between 1 and 9): "))
    for i in range(3):
        for  j in range(3):
            if board[i][j] == move:
                board[i][j] = 'O'
                found = True
    if found == False:
        enter_move(board) 
    victory_for(board)
    draw_move(board)
    

def make_list_of_free_fields(board):
    global free 
    free = []
    for i in range(3):
        for j in range(3):
            if type(board[i][j]) is int:
                free.append(board[i][j])
    #print(free)
    return free

def victory_for(board):
    # The function analyzes the board status in order to check if 
    # the player using 'O's or 'X's has won the game
    if board[0] == ['O','O','O'] or board[1] == ['O','O','O'] or board[2] == ['O','O','O'] or (board[0][0] == board[1][0] == board[2][0] == 'O') or (board[0][1] == board[1][1] == board[2][1] == 'O') or (board[2][0] == board[2][1] == board[2][2] == 'O') or (board[0][0] == board[1][1] == board[2][2] == 'O')  or (board[2][0] == board[1][1] == board[0][2] == 'O'):
        display_board(board)
        print('Victory for Player(O)')
        sys.exit()
    elif board[0] == ['X','X','X'] or board[1] == ['X','X','X'] or board[2] == ['X','X','X'] or (board[0][0] == board[1][0] == board[2][0] == 'X') or (board[0][1] == board[1][1] == board[2][1] == 'X') or (board[2][0] == board[2][1] == board[2][2] == 'X') or (board[0][0] == board[1][1] == board[2][2] == 'X')  or (board[2][0] == board[1][1] == board[0][2] == 'X'):
        display_board(board)
        print('Victory for Computer(X)')
        sys.exit()

    make_list_of_free_fields(board)
    if len(free) == 0:
        display_board(board)
        print("It's a tie!!!")
        sys.exit()

    # tie = True
    # for i in range(3):
    #     for j in range(3):
    #         if type(board[i][j]) == int:
    #             tie = False
    # if tie == True: 
    #     display_board(board)
    #     print("It's a tie!!!")
    #     sys.exit()
    
def draw_move(board):
    # The function draws the computer's move and updates the board.
    found = False
    move_pc = randrange(1,10)
    for i in range(3):
        for j in range(3):
            if board[i][j] == move_pc:
                board[i][j] = 'X'
                found = True
    if found == False:
        draw_move(board)
    victory_for(board)
    enter_move(board)

enter_move(board)
