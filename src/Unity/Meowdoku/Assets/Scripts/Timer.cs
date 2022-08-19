using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Timer : MonoBehaviour
{ 

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
        if(Game_Settings.Instance.Get_Pause() || Game_Settings.Instance.Get_Game_Over() || Game_Settings.Instance.Get_Win())
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

    public void Game_Over_Time() // Stop time on Game Over
    {
        stop_time = true;
    }

    public void Win_Time() // Stop time on Win
    {
        stop_time = true;
    }

    private void OnEnable()
    {
        Game_Events.On_Game_Over += Game_Over_Time;
        Game_Events.On_Win += Win_Time;
    }

    private void OnDisable()
    {
        Game_Events.On_Game_Over -= Game_Over_Time; 
        Game_Events.On_Win -= Win_Time;
    }

    public Text Get_Time()
    {
        return timer_text;
    }
}
