using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public GameObject highlighter;

    private void OnEnable()
    {
        highlighter.SetActive(false);
    }

    public void Highlight()
    {
        highlighter.SetActive(true);
    }

    public void UnHighlight()
    {
        highlighter.SetActive(false);
    }
}
