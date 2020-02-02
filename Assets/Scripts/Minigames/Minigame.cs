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

    public FunManager funManager;

    public BaseTask associatedTask;

    public string taskName;

    public GameObject highlighter;

    public enum MinigameType { Task, Fun }
    public MinigameType minigameType;

    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task

    protected virtual void Start()
    {
        if (highlighter)
            highlighter.SetActive(false);
        funManager = FindObjectOfType<FunManager>();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with minigame: " + name);
    }

    public virtual void Activate()
    {
        isActive = true;
    }

    public virtual void Finish()
    {
        associatedTask.Complete();
        isActive = false;
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

    protected void UpdateFun(float value)
    {
        Debug.Log("Added " + value + " fun");
        funManager.ChangeFun(value);
    }
}
