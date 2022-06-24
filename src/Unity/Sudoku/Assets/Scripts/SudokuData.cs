using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGen : MonoBehaviour {
    public int size = 9;
    public int grid_area = 81;
    public int[,] board;
    public int[,] solution;

    private int[,] empty_board = new int[,] {
    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    { 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    private int[,] solution_board = new int[,] {
    { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0},
    { 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0 },
    {0, 0, 0, 0, 0, 0, 0, 0, 0}
    };


    // Randomizes the board by adding some random inputs
    private int[,] Random_generation(int[,] correct_board)
    {
        for (int i = 0; i < size; i++)
        {
            int grid_x = Random.Range(0, size - 1);
            int grid_y = Random.Range(0, size - 1);
            int number = Random.Range(1, size);

            if (Check_space(correct_board, grid_x, grid_y, number))
                correct_board[grid_x, grid_y] = number;

        }
        if (Solution_Board(correct_board, 0, 0))
        {
            return correct_board;
        }
        else
        {
            print("No solutions exist");
            correct_board = Clear_board(correct_board);
            Random_generation(correct_board);
        }
        print("Error if code reaches this point");
        return null;
    }

    //Function that checks if space can be a valid input
    private bool Check_space(int[,] grid, int x, int y, int value)
    {
        // Look at the 3 x 3 box, if the number is already there, return false
        int row_box = x - x % 3;
        int col_box = y - y % 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i + row_box, j + col_box] == value)
                    return false;
            }
        }
        //If the number is already in that row, return false
        for (int i = 0; i < size; i++)
        {
            if (grid[x, i] == value)
                return false;
        }

        // If the number is already that column, return false
        for (int i = 0; i < size; i++)
        {
            if (grid[i, y] == value)
                return false;
        }

        return true;
    }

    // Clears the board
    private int[,] Clear_board(int [,] grid)
    {
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++)
            {
                grid[i,j] = 0;
            }
        }
        return grid;
    }

    // Devlopes a solution board with only one solution - O(9^(n*n)) in worst case
    private bool Solution_Board(int[,] grid, int x, int y)
    {
        //Check if 8th row and 9th column are reached
        if (x == 8 && y == size)
            return true;

        //If we are at the 9th column but not the 8th row, move to next row and reset column
        if (y == size)
        {
            x += 1; //Increase row value by 1
            y = 0; //Resets column value to 0
        }
        // If the grid is greater than 0
        if (grid[x, y] > 0)
            // Recursively iterate through the next column
            Solution_Board(grid, x, y + 1); // Column increased by 1

        // Must check if placing number in poition will break sudoku rules
        for (int n = 1; n < size + 1; n++)
        {
            if (Check_space(grid, x, y, n) == true)
            {
                grid[x, y] = n;
                if (Solution_Board(grid,x,y+1) == true)
                    return true;  
            }
            grid[x, y] = 0;
        }
        return false;
    }
    
    private int[,] Make_Game_Board(int[,] grid)
    {
        int empty_box = 60; // We want 60 empty cells to make the game challenging
        for (int i = 0; i < empty_box; i++)
        {
            //Remove the value at index [i//9][i%9], where i is a random number from 1-81, this guarantees that it will be random but calculated consistent index numbers
            grid[i / size, i % size] = 0;
        }
        return grid;
    }

    public int[,] Generate_Solution_Board()
    {
        solution = Random_generation(solution_board);

        if (Solution_Board(solution, 0, 0) == true)
            print("It is true that there is only one solution");
        return solution;
    }

    public int[,] Generate_Game_Board()
    {
        board = solution;  // Deep copy solution as a new array to make the game board on

        board = Make_Game_Board(board);
        return board;
    }
}