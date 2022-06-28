using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class Handle_Data_File
{   
    public static char[] ReadString(string path)
    {
        char[] CharacterArray = new char[81];
        //Read the text from directly from the test.txt file
        int ran_line = Random.Range(1, 10000);
        Debug.Log(ran_line);

        string[] stringdata = File.ReadAllLines(path);
        string line_string = stringdata[ran_line];

        for (int i = 0; i < 81; i++)
        {
            CharacterArray[i] = line_string[i];
        }
        return CharacterArray;
    }
}

public class Sudoku_Solver : MonoBehaviour
{
    public static int[] Get_Solution(int[] board)
    {
        int[] solution_int_array = new int[81];
        int counter = 0;
        if (board == null || board.Length == 0)
            return null;
        int[,] boardarray = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                boardarray[i, j] = board[counter++];
            }
        }

        Solve(boardarray, 0, 0); // Checks if board is solvable 
        solution_int_array = Solution_Array(boardarray);
         return solution_int_array;
        
    }
    private static bool Solve(int[,] board, int row, int col)
    {
        if (col == 9)
        {
            col = 0; ++row;
            if (row == 9) return true;
        }

        // If the boards position is greater than 0
        if (board[row, col] > 0)
            //Recursively iterate through the next column
            return Solve(board, row, col + 1);

        // Check if placing a number here will break rules of sudoku 
        for(int num = 1; num < 10; num++) // Gets a number from 1-9 to see if its safe to place
        {
            if (Check_Valid(board, row, col, num)) // Call on the rule checking function
            {
                board[row, col] = num; // If it is safe, place the number there there
                if (Solve(board, row, col + 1))
                    return true;
                else board[row, col] = 0;
            }
        }
        return false;
    }
    private static bool Check_Valid(int[,] board, int row, int col, int value)
    {
        // Look at the 3 x 3 box, if the number is already there, return false

        for (int i = 0; i < 3; i++)
        {
            int row_box = 3 * (row / 3) + i / 3;
            int col_box = 3 * (col / 3) + i % 3;

            if (board[row_box, col_box] != 0 && board[row_box, col_box] == value)
                return false;
        }

        // If the number is already in that row, return false
        for (int i = 0; i < 9; i++)
        {
            if (board[row, i] == value && board[row, i] != 0)
                return false;
        }

        // If the number is already that column, return false
        for(int i = 0; i < 9; i++)
        {
            if (board[i, col] == value && board[i, col] != 0)
                return false;
        }
        return true;
    }

    private static int[] Solution_Array(int[,] board) // Convert 2D array back to 1D array
    {
        int[] solution_int = new int[81];
        int counter = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                solution_int[counter++] = board[i, j];
            }
        }
        return solution_int;
    }
}
public class SudokuEasyData : MonoBehaviour 
{
    public static List<Sudoku_Data.Sudoku_Board_Data> getData()
    {
        
        char [] board_char = Handle_Data_File.ReadString("Assets/Resources/Easy.txt");
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = Sudoku_Solver.Get_Solution(Sudoku_board);

        List<Sudoku_Data.Sudoku_Board_Data> data = new List<Sudoku_Data.Sudoku_Board_Data> ();

        data.Add(new Sudoku_Data.Sudoku_Board_Data(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuMediumData : MonoBehaviour
{
    public static List<Sudoku_Data.Sudoku_Board_Data> getData()
    {
        char[] board_char = Handle_Data_File.ReadString("Assets/Resources/Medium.txt");
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = Sudoku_Solver.Get_Solution(Sudoku_board);

        List<Sudoku_Data.Sudoku_Board_Data> data = new List<Sudoku_Data.Sudoku_Board_Data>();

        data.Add(new Sudoku_Data.Sudoku_Board_Data(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuHardData : MonoBehaviour
{
    public static List<Sudoku_Data.Sudoku_Board_Data> getData()
    {

        char[] board_char = Handle_Data_File.ReadString("Assets/Resources/Hard.txt");
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = Sudoku_Solver.Get_Solution(Sudoku_board);

        List<Sudoku_Data.Sudoku_Board_Data> data = new List<Sudoku_Data.Sudoku_Board_Data>();

        data.Add(new Sudoku_Data.Sudoku_Board_Data(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuExpertData : MonoBehaviour
{
    public static List<Sudoku_Data.Sudoku_Board_Data> getData()
    {
        char[] board_char = Handle_Data_File.ReadString("Assets/Resources/Expert.txt");
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = Sudoku_Solver.Get_Solution(Sudoku_board);

        List<Sudoku_Data.Sudoku_Board_Data> data = new List<Sudoku_Data.Sudoku_Board_Data>();

        data.Add(new Sudoku_Data.Sudoku_Board_Data(Sudoku_board, Sudoku_solution));
        return data;
    }
}


public class Sudoku_Data : MonoBehaviour
{
    public static Sudoku_Data Instance;

    public struct Sudoku_Board_Data
    {
        public int[] play_data;
        public int[] solved_data;

        public Sudoku_Board_Data(int[] board, int[] solution) : this()
        {
            this.play_data = board;
            this.solved_data = solution;
        }
    };

    public Dictionary<string, List<Sudoku_Board_Data>> sudoku_board = new();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

   void Start()
    {
        sudoku_board.Add("Easy", SudokuEasyData.getData());
        sudoku_board.Add("Medium", SudokuMediumData.getData());
        sudoku_board.Add("Hard", SudokuHardData.getData());
        sudoku_board.Add("Expert", SudokuExpertData.getData());
    }
}