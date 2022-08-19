using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Events : MonoBehaviour
{
    public delegate void Place_Number(int number);
    public static event Place_Number On_Place_Number;

    public static void Place_Number_Func(int number)
    {
        if(On_Place_Number != null)
            On_Place_Number(number);
    }

    public delegate void Selected_Square(int square_index);
    public static event Selected_Square On_Selected_Square;

    public static void Square_Selected_Func(int square_index)
    {
        if (On_Selected_Square != null)
            On_Selected_Square(square_index);
    }

    public delegate void Wrong_Number();
    public static event Wrong_Number On_Wrong_Number;

    public static void On_Wrong_Number_Func()
    {
        if (On_Wrong_Number != null)
            On_Wrong_Number(); 
    }

    public delegate void Game_Over();
    public static event Game_Over On_Game_Over;

    public static void On_Game_Over_Func()
    {
        if(On_Game_Over != null)
        {
            On_Game_Over();
        }
    }

    public delegate void Win();
    public static event Win On_Win;

    public static void On_Win_Func()
    {
        if (On_Win != null)
        {
            On_Win();
        }
    }

    // --------------------------------

    public delegate void Notes_On(bool toggle);
    public static event Notes_On On_Notes_On; 

    public static void Notes_On_Func(bool toggle)
    {
        On_Notes_On?.Invoke(toggle);
    }

    public delegate void Erase_On();
    public static event Erase_On On_Erase_On;

    public static void Erase_On_Func()
    {
        if (On_Erase_On != null)
            On_Erase_On();
    }

    // -----------


    public delegate void Board_Full();
    public static event Board_Full On_Board_Full;

    public static void On_Board_Full_Func()
    {
        if (On_Board_Full != null) { 
            On_Board_Full();
        }    
    }

    public delegate void Check_Complete();
    public static event Check_Complete OnCheckComplete;

    public static void On_Check_Complete_Func()
    {
        if (OnCheckComplete != null) {
            OnCheckComplete();
        }
    }
}
