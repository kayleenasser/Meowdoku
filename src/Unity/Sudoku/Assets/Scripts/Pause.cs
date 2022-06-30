using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public Text paused_time;
    public void DisplayTime()
    {
        paused_time.text = Timer.instance.Get_Time().text;
    }
}
