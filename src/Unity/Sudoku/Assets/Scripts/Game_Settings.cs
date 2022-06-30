using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Settings : MonoBehaviour
{
    public enum EGameMode
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

    private EGameMode _GameMode;
    private bool paused = false;

    public void Set_Pause(bool paused_game) { paused = paused_game; }
    public bool Get_Pause() { return paused; }
    private void Start()
    {
        _GameMode = EGameMode.NONE;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }

    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(EGameMode.EASY);
        else if (mode == "Medium") SetGameMode(EGameMode.MEDIUM);
        else if (mode == "Hard") SetGameMode(EGameMode.HARD);
        else if (mode == "Expert") SetGameMode(EGameMode.EXPERT);
        else SetGameMode(EGameMode.NONE);
    }

    public string GetGameMode()
    {
        switch (_GameMode)
        {
            case EGameMode.EASY: return "Easy";
            case EGameMode.MEDIUM: return "Medium";
            case EGameMode.HARD: return "Hard";
            case EGameMode.EXPERT: return "Expert";
            case EGameMode.NONE: return "None";
        }
        Debug.LogError("Error: Game difficult not set");
        return " ";
    }
}
