using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Erase : Selectable, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Game_Events.Erase_On_Func();
    }
}