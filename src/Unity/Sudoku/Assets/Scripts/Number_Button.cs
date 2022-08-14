using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Number_Button : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public int value;

    public void OnPointerClick(PointerEventData eventData)
    {
        Game_Events.Place_Number_Func(value);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }
    
}
