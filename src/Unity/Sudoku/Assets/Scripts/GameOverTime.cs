using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverTime : MonoBehaviour
{
    public Text text_timer;
    void Start()
    {
        text_timer.text = Timer.instance.Get_Time().text;
    }
}
