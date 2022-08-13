using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public static Highlight instance;

    private int[,] highlight_line = new int[9, 9] { 
        {0, 1, 2,   3, 4, 5,    6, 7, 8 },
        {9,10,11,   12,13,14,   15,16,17},
        {18,19,20,  21,22,23,   24,25,26},

        {27,28,29,  30,31,32,   33,34,35},
        {36,37,38,  39,40,41,   42,43,44},
        {45,46,47,  48,49,50,   51,52,53},

        {54,55,56,  57,58,59,   60,61,62},
        {63,64,65,  66,67,68,   69,70,71},
        {72,73,74,  75,76,77,   78,79,80}
    };

    private int[] highlight_line_flat = new int[81] {
        0, 1, 2,   3, 4, 5,    6, 7, 8,
        9, 10,11,  12,13,14,   15,16,17,
        18,19,20,  21,22,23,   24,25,26,

        27,28,29,  30,31,32,   33,34,35,
        36,37,38,  39,40,41,   42,43,44,
        45,46,47,  48,49,50,   51,52,53,

        54,55,56,  57,58,59,   60,61,62,
        63,64,65,  66,67,68,   69,70,71,
        72,73,74,  75,76,77,   78,79,80
    };

    private int[,] three_box = new int[9, 9]
    {
        {0,1,2,9,10,11,18,19,20},
        {3,4,5,12,13,14,21,22,23},
        {6,7,8,15,16,17,24,25,26},
        {27,28,29,36,37,38,45,46,47},
        {30,31,32,39,40,41,48,49,50},
        {33,34,35,42,43,44,51,52,53},
        {54,55,56,63,64,65,72,73,74},
        {57,58,59,66,67,68,75,76,77},
        {60,61,62,69,70,71,78,79,80}
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private (int, int) Get_Square_Position(int square_index)
    {
        int row = -1;
        int col = -1;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if(highlight_line[i,j] == square_index)
                {
                    row = i;
                    col = j;
                }
            }
        }
        return (row, col);
    }

    public int[] Get_Row_Line(int square_index)
    {
        int[] line = new int[9];
        var square_position = Get_Square_Position(square_index).Item1;
        for(int index = 0; index < 9; index++)
        {
            line[index] = highlight_line[square_position, index];
        }
        return line;
    }

    public int[] Get_Col_Line(int square_index)
    {
        int[] line = new int[9];
        var square_position = Get_Square_Position(square_index).Item2;
        for (int index = 0; index < 9; index++)
        {
            line[index] = highlight_line[index, square_position];
        }
        return line;
    }

    public int[] Get_Big_Square(int square_index)
    {
        int[] line = new int[9];
        int row = -1;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if(three_box[i,j] == square_index)
                {
                    row = i;
                }
            }
        }

        for(int index = 0; index < 9; index++)
        {
            line[index] = three_box[row,index];
        }

        return line;
    }

    public int[] Get_Indices()
    {
        return highlight_line_flat;
    }
}
