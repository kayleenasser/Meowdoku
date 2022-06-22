using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public GameObject number_text;
    private int num = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
