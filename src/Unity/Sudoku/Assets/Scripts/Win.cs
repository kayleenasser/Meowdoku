using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public GameObject WinScreen;
    void Start()
    {
        WinScreen.SetActive(false);
    }

    private void OnBoardFull()
    {
        WinScreen.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.OnBoardFull += OnBoardFull;
    }
    private void OnDisable()
    {
        GameEvents.OnBoardFull -= OnBoardFull;
    }
}
