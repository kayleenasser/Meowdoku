using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    private int num = 0;

    private bool selected = false;
    private int square_index = -1;

    void Start()
    {
        selected = IsSelected();
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsSelected() { return selected; }
    public void SetIndex(int index)
    {
        square_index = index;
    }
 
   public void DisplayText()
    {
        if (num <= 0)
            number_text.GetComponent<Text>().text = " ";
        else
            number_text.GetComponent<Text>().text = num.ToString();
    }

    public void SetNumber(int number)
    {
        num = number;
        DisplayText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = IsSelected();
        selected = true;
        GameEvents.SquareSelectedFunc(square_index);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnEnable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        GameEvents.OnPlaceNumber += OnSetNumber;
        GameEvents.OnSelectedSquare += OnSelectedSquare;
    }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnDisable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        GameEvents.OnPlaceNumber -= OnSetNumber;
        GameEvents.OnSelectedSquare -= OnSelectedSquare;
    }

    public void OnSetNumber(int number)
    {
        selected = IsSelected();
        if (selected)
        {
            SetNumber(number);
        }
    }

    public void OnSelectedSquare(int index)
    {
        if(square_index != index)
        {
            selected = IsSelected();
            selected = false;
        }
    }
}
