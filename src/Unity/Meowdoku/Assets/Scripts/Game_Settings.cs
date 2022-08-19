using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Settings : MonoBehaviour
{
    public enum Game_Mode_Names // Gamemodes
    {
        NONE,
        EASY,
        MEDIUM,
        HARD,
        EXPERT
    }

    public static Game_Settings Instance;

    private void Awake()
    {
        paused = false;
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }

    private Game_Mode_Names Game_Mode;
    private bool paused = false;
    private bool game_over = false;
    private bool win = false;

    public void Set_Pause(bool paused_game) { paused = paused_game; }
    public bool Get_Pause() { return paused; }

    public void Set_Game_Over(bool game_over_stop) { game_over = game_over_stop; }
    public bool Get_Game_Over() { return game_over; }

    public void Set_Win(bool win_stop) { win = win_stop; }
    public bool Get_Win() { return win; }


    private void Start()
    {
        Game_Mode = Game_Mode_Names.NONE;
    }

    public void SetGameMode(Game_Mode_Names mode)
    {
        Game_Mode = mode;
    }

    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(Game_Mode_Names.EASY);
        else if (mode == "Medium") SetGameMode(Game_Mode_Names.MEDIUM);
        else if (mode == "Hard") SetGameMode(Game_Mode_Names.HARD);
        else if (mode == "Expert") SetGameMode(Game_Mode_Names.EXPERT);
        else SetGameMode(Game_Mode_Names.NONE);
    }

    public string Get_Game_Mode()
    {
        switch (Game_Mode)
        {
            case Game_Mode_Names.EASY: return "Easy";
            case Game_Mode_Names.MEDIUM: return "Medium";
            case Game_Mode_Names.HARD: return "Hard";
            case Game_Mode_Names.EXPERT: return "Expert";
            case Game_Mode_Names.NONE: return "None";
        }
        Debug.LogError("Error: Game difficult not set");
        return " ";
    }
}
