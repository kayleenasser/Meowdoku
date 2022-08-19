using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public class Handle_Data_File
{
    public static char[] Read_String(string path)
    {

        TextAsset file = Resources.Load(path) as TextAsset;
        string text_string = file.ToString();
        string[] stringdata = text_string.Split('\n');

        char[] CharacterArray = new char[81];
        int ran_line = Random.Range(1, 10000);
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
        int[,] board_array = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                board_array[i, j] = board[counter++];
            }
        }

        Solve(board_array, 0, 0); // Checks if board is solvable 
        solution_int_array = Solution_Array(board_array);
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
            if (Check_Space(board, row, col, num)) // Call on the rule checking function
            {
                board[row, col] = num; // If it is safe, place the number there there
                if (Solve(board, row, col + 1))
                    return true;
                else board[row, col] = 0;
            }
        }
        return false;
    }
    private static bool Check_Space(int[,] board, int row, int col, int value)
    { 
        // Look at the 3 x 3 box, if the number is already there, return false
        int row_beginning = row - row % 3;
        int col_beginning = col - col % 3;
        for (int i = 0; i < 3; i++) 
        { 
            for (int j = 0; j < 3; j++)
            {
                if (board[i + row_beginning, j + col_beginning] == value)
                    return false;
            }
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
    public static List<Sudoku_Data.Sudoku_Board_Data> Get_Data()
    {
        
        char [] board_char = Handle_Data_File.Read_String("Easy");
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
        char[] board_char = Handle_Data_File.Read_String("Medium");
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

        char[] board_char = Handle_Data_File.Read_String("Hard");
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
        char[] board_char = Handle_Data_File.Read_String("Expert");
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
        sudoku_board.Add("Easy", SudokuEasyData.Get_Data());
        sudoku_board.Add("Medium", SudokuMediumData.getData());
        sudoku_board.Add("Hard", SudokuHardData.getData());
        sudoku_board.Add("Expert", SudokuExpertData.getData());
    }
}