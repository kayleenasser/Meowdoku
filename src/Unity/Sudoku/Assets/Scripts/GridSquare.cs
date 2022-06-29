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
    private int correct_num = 0;

    private bool selected = false;
    private int square_index = -1;
    private bool default_value = false;

    public void Set_Default_Value(bool deflt){ default_value = deflt; }
    public bool Get_Default_Value() { return default_value; }

    void Start()
    {
        selected = IsSelected();
        selected = false;
    }

    public bool IsSelected() { return selected; }
    public void SetIndex(int index)
    {
        square_index = index;
    }

    public void SetCorrectNumber(int number)
    {
        correct_num = number;
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
        if (selected && default_value == false)
        {
            SetNumber(number);
            if (num != correct_num)
            {
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

                GameEvents.OnWrongNumberFunc();
            }
            else
            {
                default_value = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
            }
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
