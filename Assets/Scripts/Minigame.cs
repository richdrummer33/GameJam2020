using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all minigames
/// </summary>
public abstract class Minigame : MonoBehaviour
{
    public float taskDuration;

    public bool isActive;

    public BaseTask associatedTask;

    public string taskName;

    public GameObject highlighter;

    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task

    private void Start()
    {
        if (highlighter)
            highlighter.SetActive(false);
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with minigame: " + name);
    }

    public void Finish()
    {
        associatedTask.Complete();
    }

    public void Highlight()
    {
        if(isActive && highlighter)
            highlighter.SetActive(true);
    }

    public void UnHighlight()
    {
        if(highlighter)
            highlighter.SetActive(false);
    }

}
