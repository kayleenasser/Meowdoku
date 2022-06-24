using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class HandleTextFile
{   
    public static char[] ReadString(string path)
    {
        char[] CharacterArrayEasy = new char[81];
        //Read the text from directly from the test.txt file
        int linenum = Random.Range(1, 10000);
        using (Stream stream = File.Open(path, FileMode.Open))
        {
            stream.Seek(81 * (linenum - 1), SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                string easydata = reader.ReadLine();
                // Copy character by character into array 
                for (int i = 0; i < easydata.Length; i++)
                {
                    CharacterArrayEasy[i] = easydata[i];
                }
            }
        }
        return CharacterArrayEasy;
    }
}

public class SudokuSolver : MonoBehaviour
{
    public static char[] solveSudoku(char[] board)
    {
        char[] sol_char;
        int counter = 0;
        if (board == null || board.Length == 0)
            return null;
        char[,] boardarray = new char[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                boardarray[i, j] = (char)board[counter];
                counter++;
            }
        }
        solve(boardarray);
        sol_char = SolutionArray(boardarray);
        return sol_char;
    }
    private static bool solve(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == '0')
                {
                    for (char c = '1'; c <= '9'; c++)
                    {
                        if (isValid(board, i, j, c))
                        {
                            board[i, j] = c;

                            if (solve(board))
                                return true;
                            else
                                board[i, j] = '.';
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }
    private static bool isValid(char[,] board, int row, int col, char c)
    {

        for (int i = 0; i < 9; i++)
        {
            //check row  
            if (board[i, col] != '0' && board[i, col] == c)
                return false;
            //check column  
            if (board[row, i] != '0' && board[row, i] == c)
                return false;
            //check 3*3 block  
            if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != '0' && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                return false;
        }
        return true;
    }

    private static char[] SolutionArray(char[,] board)
    {
        char[] solution_char = new char[81];
        int Counter = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                solution_char[Counter] = board[i, j];
            }
        }
        return solution_char;
    }
}
public class SudokuEasyData : MonoBehaviour 
{
    public static List<SudokuData.SudokuBoardData> getData()
    {
        
        char [] board_char = HandleTextFile.ReadString("Assets/Resources/Easy.txt");
        char[] solution_char = SudokuSolver.solveSudoku(board_char);
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution =solution_char.Select(a => a - '0').ToArray();

        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData> ();

        data.Add(new SudokuData.SudokuBoardData(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuMediumData : MonoBehaviour
{
    public static List<SudokuData.SudokuBoardData> getData()
    {

        char[] board_char = HandleTextFile.ReadString("Assets/Resources/Medium.txt");
        char[] solution_char = SudokuSolver.solveSudoku(board_char);
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = solution_char.Select(a => a - '0').ToArray();

        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();

        data.Add(new SudokuData.SudokuBoardData(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuHardData : MonoBehaviour
{
    public static List<SudokuData.SudokuBoardData> getData()
    {

        char[] board_char = HandleTextFile.ReadString("Assets/Resources/Hard.txt");
        char[] solution_char = SudokuSolver.solveSudoku(board_char);
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = solution_char.Select(a => a - '0').ToArray();

        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();

        data.Add(new SudokuData.SudokuBoardData(Sudoku_board, Sudoku_solution));
        return data;
    }
}

public class SudokuExpertData : MonoBehaviour
{
    public static List<SudokuData.SudokuBoardData> getData()
    {

        char[] board_char = HandleTextFile.ReadString("Assets/Resources/Expert.txt");
        char[] solution_char = SudokuSolver.solveSudoku(board_char);
        int[] Sudoku_board = board_char.Select(a => a - '0').ToArray();
        int[] Sudoku_solution = solution_char.Select(a => a - '0').ToArray();

        List<SudokuData.SudokuBoardData> data = new List<SudokuData.SudokuBoardData>();

        data.Add(new SudokuData.SudokuBoardData(Sudoku_board, Sudoku_solution));
        return data;
    }
}


public class SudokuData : MonoBehaviour
{
    public static SudokuData Instance;

    public struct SudokuBoardData
    {
        public int[] play_data;
        public int[] solved_data;

        public SudokuBoardData(int[] board, int[] solution) : this()
        {
            this.play_data = board;
            this.solved_data = solution;
        }
    };

    public Dictionary<string, List<SudokuBoardData>> sudoku_board = new Dictionary<string, List<SudokuBoardData>>();

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