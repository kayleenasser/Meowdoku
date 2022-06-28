using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void PlaceNumber(int number);
    public static event PlaceNumber OnPlaceNumber;

    public static void PlaceNumberFunc(int number)
    {
        if(OnPlaceNumber != null)
            OnPlaceNumber(number);
    }

    public delegate void SelectedSquare(int square_index);
    public static event SelectedSquare OnSelectedSquare;

    public static void SquareSelectedFunc(int square_index)
    {
        if (OnSelectedSquare != null)
            OnSelectedSquare(square_index);
    }
}
