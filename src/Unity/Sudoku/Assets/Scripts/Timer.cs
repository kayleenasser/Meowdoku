using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Timer : MonoBehaviour
{
    private int hour = 0;
    private int minute = 0;
    private int second = 0;

    private Text timer_text;
    private float timer_value;
    private bool stop_time = false;

    public static Timer instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
        timer_text = GetComponent<Text>();
        timer_value = 0;
    }
    void Start()
    {
        stop_time = false;   
    }
    void Update()
    {
        if(Game_Settings.Instance.Get_Pause() == true)
            stop_time = true;
        else
            stop_time=false;
        if(stop_time == false)
        {
            timer_value += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(timer_value);

            string hour = Leading_Zero(span.Hours);
            string minute = Leading_Zero(span.Minutes);
            string second = Leading_Zero(span.Seconds);

            timer_text.text = hour + ":" + minute + ":" + second;
        }
    }

    string Leading_Zero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void GameOverTime()
    {
        stop_time = true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += GameOverTime;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= GameOverTime; 
    }

    public Text Get_Time()
    {
        return timer_text;
    }
}
