using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Notes : Selectable, IPointerClickHandler
{
    public Sprite On_image;
    public Sprite Off_image;
    bool toggle;

    void Start()
    {
        toggle = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        toggle = !toggle;
        if(toggle)
            GetComponent<Image>().sprite = On_image;
        else
            GetComponent<Image>().sprite = Off_image;

        Game_Events.Notes_On_Func(toggle);
    }
}