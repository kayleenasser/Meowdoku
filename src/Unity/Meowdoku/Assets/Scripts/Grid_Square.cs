using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Grid_Square : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> note_list;
    private bool toggle_note;
    private bool toggle_erase;
    private int num = 0;
    private int correct_num = 0;

    private bool selected = false;
    private int square_index = -1;
    private bool default_value = false;
    private bool is_wrong = false;

    public bool Is_correct() { return num == correct_num; }
    public bool Wrong_Square_Value(){ return is_wrong; }

    public void Set_Default_Value(bool deflt){ default_value = deflt; }
    public bool Get_Default_Value() { return default_value; }

    void Start()
    {
        toggle_note = false;
        toggle_erase = false;
        selected = IsSelected();
        selected = false;
        Set_Note(0);
        SetClearNotes();
    }

    public List<string> Get_Note() { 
        List<string> notes = new List<string>();
        foreach (var number in note_list) {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;
    }

    private void SetClearNotes()
    {
        foreach (var number in note_list)
        {
            if (number.GetComponent<Text>().text == "0")
            {
                number.GetComponent<Text>().text = " ";
            }
        }
    }

    private void Set_Note(int value) {
        foreach (var number in note_list)
        {
            if(value <= 0)
                number.GetComponent<Text>().text = " ";
            else
                number.GetComponent<Text>().text =  value.ToString();
        }
    }

    private void SetOneNumberNote(int value, bool update = false)
    {
        if (toggle_note == false && update == false)
            return;
        if (value <= 0)
            note_list[value - 1].GetComponent<Text>().text = " ";
        else
        {
            if (note_list[value - 1].GetComponent<Text>().text == " " || update)
                note_list[value - 1].GetComponent<Text>().text = value.ToString();
            else
                note_list[value - 1].GetComponent <Text>().text = " ";
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach (var note in notes)
        {
            SetOneNumberNote(note, true);
        }
    }

    public void On_Toggle_Note(bool active)
    {
        toggle_note = active;
    }

    public void On_Toggle_Erase()
    {
        if (selected && !default_value)
        {
            if (is_wrong)
                Set_Square_Colour(Color.white);
            is_wrong = false;
            SetNumber(0);
            Set_Note(0);
            DisplayText();
        }
    }

    public bool IsSelected() { return selected; }
    public void SetIndex(int index)
    {
        square_index = index;
    }

    public void SetCorrectNumber(int number)
    {
        correct_num = number;
        is_wrong = false;
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
        Game_Events.Square_Selected_Func(square_index);
    }

    private void OnEnable()
    {
        Game_Events.On_Place_Number += OnSetNumber;
        Game_Events.On_Selected_Square += OnSelectedSquare;
        Game_Events.On_Notes_On += On_Toggle_Note;
        Game_Events.On_Erase_On += On_Toggle_Erase;
    }
    private void OnDisable()
    {
        Game_Events.On_Place_Number -= OnSetNumber;
        Game_Events.On_Selected_Square -= OnSelectedSquare;
        Game_Events.On_Notes_On -= On_Toggle_Note;
        Game_Events.On_Erase_On -= On_Toggle_Erase;
    }

    public void OnSetNumber(int number)
    {
        selected = IsSelected();
        if (selected && default_value == false)
        {
            if(toggle_note == true && is_wrong == false)
            {
                SetOneNumberNote(number);
            }
            else if(toggle_note == false)
            {
                Set_Note(0);
                SetNumber(number);
                if (num != correct_num)
                {
                    is_wrong = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;

                    Game_Events.On_Wrong_Number_Func();
                }
                else
                {
                    is_wrong = false;
                    default_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                }
            }
            Game_Events.On_Check_Complete_Func();   // Check if game should be over
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

    public void Set_Square_Colour(Color color)
    {
        var colours = this.colors;
        colours.normalColor = color;
        this.colors = colours;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
