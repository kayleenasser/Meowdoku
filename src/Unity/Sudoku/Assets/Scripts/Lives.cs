using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> error_image;
    public GameObject game_over;
    int lives = 0;
    int error_num = 0;

    void Start()
    {
        lives = error_image.Count;
        error_num = 0;
    }

    private void WrongNumber()
    {
        if (error_num < error_image.Count) { 
        error_image[error_num].SetActive(true);
        error_num++;
        lives--;
        }

        Check_Game_Over();
    }

    private void Check_Game_Over()
    {
        if (lives <= 0)
        {
            GameEvents.OnGameOverFunc();
            game_over.SetActive(true);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;    
    }
}
