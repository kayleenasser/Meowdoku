using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Load_Easy(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.Game_Mode_Names.EASY);
        SceneManager.LoadScene(name);
    }
    public void Load_Medium(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.Game_Mode_Names.MEDIUM);
        SceneManager.LoadScene(name);
    }
    public void Load_Hard(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.Game_Mode_Names.HARD);
        SceneManager.LoadScene(name);
    }
    public void Load_Expert(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.Game_Mode_Names.EXPERT);
        SceneManager.LoadScene(name);
    }

    public void Activate_Object(GameObject game)
    {
        game.SetActive(true);
    }

    public void Deactivate_Object(GameObject game)
    {
        game.SetActive(false);
    }
    public void Set_Pause_Game(bool paused_game)
    {
        Game_Settings.Instance.Set_Pause(paused_game);
    }
}

