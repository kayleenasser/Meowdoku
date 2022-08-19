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
        Set_Game_Over_Menu(false);
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
            Game_Events.On_Game_Over_Func();
            game_over.SetActive(true);
            Set_Game_Over_Menu(true);
        }
    }

    public void Set_Game_Over_Menu(bool game_over_bool)
    {
        Game_Settings.Instance.Set_Game_Over(game_over_bool);
    }

    private void OnEnable()
    {
        Game_Events.On_Wrong_Number += WrongNumber;
    }

    private void OnDisable()
    {
        Game_Events.On_Wrong_Number -= WrongNumber;    
    }
}
