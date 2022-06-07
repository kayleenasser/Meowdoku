import random as ran

# Initialize an empty board
empty_board = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0]
]

# Initialize solution board
solution_board = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0]
]

# Global Variables:
size = 9
grid_area = size * size


# Function that prints the board in the correct format
def Print_Board(grid):
    ends = " ----------------------------------- "
    print(ends)
    for i, row in enumerate(grid):
        print(("|" + " {}   {}   {} |" * 3).format(*[x if x != 0 else " " for x in row]))  # Print nothing if empty
        if i % 3 == 2:
            if i == 8:  # If i is 8, remainder is 2 meaning we are at the end of the grid, print end row
                print(ends)
            else:
                print("+" + "---+" * size)
        else:
            print("+" + "   +" * size)
    return


# Function that checks if space can be a valid input
def Check_space(grid, x, y, value):
    # Look at the 3 x 3 box, if the number is already there, return false
    row_box = x - x % 3
    col_box = y - y % 3
    for i in range(3):
        for j in range(3):
            if grid[i + row_box][j + col_box] == value:
                return False

    # If the number is already in that row, return false
    for i in range(size):
        if grid[x][i] == value:
            return False

    # If the number is already that column, return false
    for i in range(size):
        if grid[i][y] == value:
            return False

    return True


# Devlopes a solution board with only one solution - O(9^(n*n)) in the worse case
def Solution_Board(grid, x, y):
    # Check if 8th row and 9th column are reached
    if x == 8 and y == size:
        # If so, we want to stop backtracking
        return True  # Return true
    # If we are at the 9th column but not the 8th row, move to next row and reset column
    if y == size:
        x += 1  # Increase row value by 1
        y = 0  # Resets column value to 0

    # If the grids position is greater than 0
    if grid[x][y] > 0:
        # Recursively iterate through the next column
        return Solution_Board(grid, x, y + 1)  # Column is increased by 1

    # Must check if placing a number here will break the rules of sudoku
    for n in range(1, size + 1, 1):  # Gets a number from 1-9 to see if its safe to place
        if Check_space(grid, x, y, n):  # Call on the rule checking function
            grid[x][y] = n  # If it is safe, place n there
            if Solution_Board(grid, x, y + 1):
                return True
        grid[x][y] = 0
    return False


# Randomizes the board by adding some random inputs
def Randomize_generation(board_correct):
    # To randomize the generation, put in 5 random numbers and build solutions around that
    for i in range(size):
        grid_x = ran.randint(0, 8)  # Generate random x value
        grid_y = ran.randint(0, 8)  # Generate random y value
        num = ran.randint(1, size)

        if Check_space(board_correct, grid_x, grid_y, num):
            board_correct[grid_x][grid_y] = num

    if Solution_Board(board_correct, 0, 0):
        return board_correct
    else:
        print("No solutions exist")
        board_correct = Clear_board(board_correct)
        Randomize_generation(board_correct)


# Clears the board
def Clear_board(grid):
    for i in range(size):
        for j in range(size):
            grid[i][j] = 0
    return grid


# Removes numbers to make it a playable game
def Make_Game_Board(grid):
    empty_box = grid_area * 3 // 4  # We want 60 empty cells to make the game challenging
    for i in ran.sample(range(grid_area), empty_box):  # Samples up to 60 points from the grid
        # Remove the value at index [i//9][i%9], where i is a random number from 1-81, this guarantees that it will be
        # random but calculated consistent index numbers
        grid[i // size][i % size] = 0
    return grid


