using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int columns = 9;
    public int rows = 9;
    public float every_square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2 (0.0f, 0.0f);
    public float square_scale = 1.0f;

    private List<GameObject> grid_squares_ = new List<GameObject>();
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("Grid_square object needs to have GridSquare script attached!");
        CreateBoard();
        SetGridNumbers();
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
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                grid_squares_.Add(Instantiate(grid_square) as GameObject);
                grid_squares_[grid_squares_.Count - 1].transform.parent = this.transform; ///Instantiatet this game object as a child of the object holding this script
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);
            }
        }
    }

    private void SetSquarePosition()
    {
        var square_rect = grid_squares_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + every_square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + every_square_offset;

        int column_num = 0;
        int row_num = 0;

        foreach (GameObject square in grid_squares_)
        {
            if(column_num + 1 > columns)
            {
                row_num++;
                column_num = 0;
            }

            var pos_x_offset = offset.x * column_num;
            var pos_y_offset = offset.y * row_num;

            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_num++;
          }
    }

    private void SetGridNumbers()
    { 
        foreach(var square in grid_squares_)
        {
            square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 10));
        }
    }
}
