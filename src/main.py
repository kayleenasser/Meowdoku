from Board_Generator import *
from game import *
import copy
import os


def Clear_Console():
    command = 'clear'
    if os.name in ('nt', 'dos'):  # If Machine is running on Windows, use cls
        command = 'cls'
    os.system(command)
    Print_Board(board)


solution = Randomize_generation(solution_board)

board = copy.deepcopy(solution)  # Deep copy solution as a new array to make the game board on

board = Make_Game_Board(board)

if Solution_Board(solution, 0, 0):
    print("It is true that there is only one solution")

print("Solution:")
Print_Board(solution)

# Game Loop
Print_Board(board)
game_complete = Check_Game(board)
while not Check_Game(board):

    x = input("Enter row value, 0-8 from up to down:\n")
    while not (x.isnumeric() and 0 <= int(x) <= 8):
        Clear_Console()
        x = input("Invalid input, please give a number from 0 - 8:\n")
    x = int(x)

    y = input("Enter column value, 0-8 from left to right:\n")
    while not (y.isnumeric() and 0 <= int(y) <= 8):
        Clear_Console()
        y = input("Invalid input, please give a number from 0 - 8:\n")
    y = int(y)

    number = input("Enter value to place between 1-9:\n")
    while not (number.isnumeric() and 1 <= int(number) <= 9):
        Clear_Console()
        y = input("Invalid input, please give a number from 1 - 9:\n")
    number = int(number)
    Clear_Console()
    Placement(board, solution, number, x, y)
print("Sudoku Complete! Congrats!")
