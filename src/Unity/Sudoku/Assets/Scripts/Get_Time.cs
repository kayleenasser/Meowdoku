using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Get_Time : MonoBehaviour
{
    public Text text_timer;
    void Start()
    {
        text_timer.text = Timer.instance.Get_Time().text;
    }
}
