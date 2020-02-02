using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ToDoList : MonoBehaviour
{
    public TextMeshProUGUI listText;

    private List<string> items = new List<string>();


    void Start()
    {
        
    }

    public void UpdateList(List<string> newList )
    {
        items = newList;

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        listText.text = string.Join("\n", items);
    }
}
