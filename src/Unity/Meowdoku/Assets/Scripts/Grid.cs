using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float box_offset = 0.0f;
    public GameObject grid_box;
    public Vector2 start_position = new(0.0f, 0.0f);
    public float square_scale = 1.0f;
    public float gap = 0.1f;
    public Color highlight_colour = Color.cyan;

    private List<GameObject> grid_boxes = new();
    private int select_grid_data = -1;
    void Start()
    {
        if (grid_box.GetComponent<Grid_Square>() == null)
            Debug.LogError("grid_box object needs to have GridSquare script attached!");
        Create_Board();
        Set_Grid_Numbers(Game_Settings.Instance.Get_Game_Mode());
    }

    private void Create_Board() // Create sudoku board
    {
        Get_Squares();
        Set_Square_Position();
    }

    private void Get_Squares() // Create sudoku squares
    {
        int square_i = 0;
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                grid_boxes.Add(Instantiate(grid_box) as GameObject);
                grid_boxes[grid_boxes.Count - 1].GetComponent<Grid_Square>().SetIndex(square_i);
                grid_boxes[grid_boxes.Count - 1].transform.parent = this.transform; ///Instantiatet this game object as a child of the object holding this script
                grid_boxes[grid_boxes.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);

                square_i++;
            }
        }
    }

    private void Set_Square_Position() // Sets Position of grid squares
    {
        var square_rect = grid_boxes[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2
        {
            x = square_rect.rect.width * square_rect.transform.localScale.x + box_offset,
            y = square_rect.rect.height * square_rect.transform.localScale.y + box_offset
        };
        Vector2 gap_number = new Vector2(0.0f, 0.0f);
        bool moved_row = false;

        int column_num = 0;
        int row_num = 0;

        foreach (GameObject square in grid_boxes) // For each of the 81 grid squares, do the following
        {
            if(column_num + 1 > columns) // Start over columns if row reaches the end
            {
                row_num++;
                column_num = 0;
                gap_number.x = 0;
                moved_row = false;
            }

            var pos_x_offset = offset.x * column_num + (gap_number.x * gap); 
            var pos_y_offset = offset.y * row_num + (gap_number.y * gap); 

            if(column_num > 0 && column_num % 3 == 0) // Places column gap
            {
                gap_number.x++;
                pos_x_offset += gap;
            }
            if(row_num > 0 && row_num % 3 == 0 && moved_row == false) // Places row gap
            {
                moved_row = true;
                gap_number.y++;
                pos_y_offset += gap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_num++;
        }
    }

    private void Set_Grid_Numbers(string level)
    {
        select_grid_data = Random.Range(0, Sudoku_Data.Instance.sudoku_board[level].Count);
        var data = Sudoku_Data.Instance.sudoku_board[level][select_grid_data];

        Set_Grid_Data(data);
    }

    private void Set_Grid_Data(Sudoku_Data.Sudoku_Board_Data data)
    {
        for (int i = 0; i < grid_boxes.Count; i++)
        {
            grid_boxes[i].GetComponent<Grid_Square>().SetNumber(data.play_data[i]);
            grid_boxes[i].GetComponent<Grid_Square>().SetCorrectNumber(data.solved_data[i]);
            grid_boxes[i].GetComponent<Grid_Square>().Set_Default_Value(data.play_data[i] != 0 && data.play_data[i] == data.solved_data[i]);
        }
    }

    private void OnEnable()
    {
        Game_Events.On_Selected_Square += On_Square_Selected;
        Game_Events.OnCheckComplete += Check_Completion;
    }

    private void OnDisable()
    {
        Game_Events.On_Selected_Square -= On_Square_Selected;
        Game_Events.OnCheckComplete -= Check_Completion;
    }


    private void Set_Square_colour(int[] data, Color color)
    {
        foreach(var index in data)
        {
            var comp = grid_boxes[index].GetComponent<Grid_Square>();
            if(comp.Wrong_Square_Value() == false &&  comp.IsSelected() == false)
            {
                comp.Set_Square_Colour(color);
            }
        }
    }

    public void On_Square_Selected(int square_index)
    {
        var hori_line = Highlight.instance.Get_Row_Line(square_index);
        var vert_line = Highlight.instance.Get_Col_Line(square_index);
        var square_box = Highlight.instance.Get_Big_Square(square_index);

        Set_Square_colour(Highlight.instance.Get_Indices(), Color.white);
        Set_Square_colour(hori_line, highlight_colour);
        Set_Square_colour(vert_line, highlight_colour);
        Set_Square_colour(square_box, highlight_colour);
    }

    private void Check_Completion()
    {
        foreach(var square in grid_boxes)
        {
            var complete = square.GetComponent<Grid_Square>();
            if (complete.Is_correct() == false)
                return;
        }

        Game_Events.On_Board_Full_Func();
    }
}
