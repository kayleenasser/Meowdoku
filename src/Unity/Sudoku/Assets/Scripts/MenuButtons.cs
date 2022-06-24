using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadEasy(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.EGameMode.EASY);
        SceneManager.LoadScene(name);
    }
    public void LoadMedium(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.EGameMode.MEDIUM);
        SceneManager.LoadScene(name);
    }
    public void LoadHard(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.EGameMode.HARD);
        SceneManager.LoadScene(name);
    }
    public void LoadExpert(string name)
    {
        Game_Settings.Instance.SetGameMode(Game_Settings.EGameMode.EXPERT);
        SceneManager.LoadScene(name);
    }

    public void ActivateObject(GameObject game)
    {
        game.SetActive(true);
    }

    public void DeactivateObject(GameObject game)
    {
        game.SetActive(false);
    }
}
