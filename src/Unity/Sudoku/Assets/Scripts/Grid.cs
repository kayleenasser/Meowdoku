using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new(0.0f, 0.0f);
    public float square_scale = 1.0f;
    public float gap = 0.1f;

    private List<GameObject> grid_squares_ = new List<GameObject>();
    private int select_grid_data = -1;
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("Grid_square object needs to have GridSquare script attached!");
        CreateBoard();
        SetGridNumbers(Game_Settings.Instance.GetGameMode());
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void CreateBoard()
    {
        GetSquares();
        SetSquarePosition();
    }

    private void GetSquares()
    {
        int square_i = 0;
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                grid_squares_.Add(Instantiate(grid_square) as GameObject);
                grid_squares_[grid_squares_.Count - 1].GetComponent<GridSquare>().SetIndex(square_i);
                grid_squares_[grid_squares_.Count - 1].transform.parent = this.transform; ///Instantiatet this game object as a child of the object holding this script
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);

                square_i++;
            }
        }
    }

    private void SetSquarePosition()
    {
        var square_rect = grid_squares_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2
        {
            x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset,
            y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset
        };
        Vector2 gap_number = new Vector2(0.0f, 0.0f);
        bool moved_row = false;

        int column_num = 0;
        int row_num = 0;

        foreach (GameObject square in grid_squares_)
        {
            if(column_num + 1 > columns)
            {
                row_num++;
                column_num = 0;
                gap_number.x = 0;
                moved_row = false;
            }

            var pos_x_offset = offset.x * column_num + (gap_number.x * gap);
            var pos_y_offset = offset.y * row_num + (gap_number.y * gap); 

            if(column_num > 0 && column_num % 3 == 0)
            {
                gap_number.x++;
                pos_x_offset += gap;
            }
            if(row_num > 0 && row_num % 3 == 0 && moved_row == false)
            {
                moved_row = true;
                gap_number.y++;
                pos_y_offset += gap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_num++;
          }
    }

    private void SetGridNumbers(string level)
    {
        select_grid_data = Random.Range(0, Sudoku_Data.Instance.sudoku_board[level].Count);
        var data = Sudoku_Data.Instance.sudoku_board[level][select_grid_data];

        SetGridData(data);
    }

    private void SetGridData(Sudoku_Data.Sudoku_Board_Data data)
    {
        for (int i = 0; i < grid_squares_.Count; i++)
        {
            grid_squares_[i].GetComponent<GridSquare>().SetNumber(data.play_data[i]);
            grid_squares_[i].GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[i]);
            grid_squares_[i].GetComponent<GridSquare>().Set_Default_Value(data.play_data[i] != 0 && data.play_data[i] == data.solved_data[i]);
        }
    }
}
