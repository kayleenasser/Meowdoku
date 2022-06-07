from Board_Generator import Print_Board
from Board_Generator import size


def Placement(board, solution, num, x, y):
    for i in range(size):
        for j in range(size):
            if board[i][j] == 0 and solution[i][j] == num and i == x and j == y:
                board[x][y] = num
                Print_Board(board)
            elif solution[i][j] != num and solution[i][j] != 0 and i == x and j == y:
                print("Mistake made, try again")
    return board


def Check_Game(grid):
    for j in range(size):
        for k in range(size):
            if grid[j][k] == 0:
                finished = False
                return finished
    finished = True
    return finished

