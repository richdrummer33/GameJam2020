using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ToDoList : MonoBehaviour
{
    public TextMeshProUGUI listText;

    private List<string> items = new List<string>();


    void Start()
    {
        AddItem("list");
        AddItem("of");
        AddItem("things");
    }

    public void AddItem( string itemText )
    {
        items.Add(itemText);

        UpdateDisplay();
    }

    public void RemoveItem(string itemText)
    {
        _ = items.Remove(itemText);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        listText.text = string.Join("\n", items);
    }
}
