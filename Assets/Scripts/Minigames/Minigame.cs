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

    public bool highlightOnLook = true;

    public bool loopingSound;
    protected AudioSource source;
    float defaultVolume;

    public enum MinigameType { Task, Fun }
    public MinigameType minigameType;

    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task
    
    protected virtual void Start()
    {
        if (highlighter)
            highlighter.SetActive(false);

        funManager = FindObjectOfType<FunManager>();

        if(source == null)
            gameObject.AddComponent<AudioSource>();

        source = GetComponent<AudioSource>();
        //defaultVolume = source.volume;
        //GameManager.OnTaskComplete += TempHighlight;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with minigame: " + name);
    }

    public virtual void Activate()
    {
        isActive = true;
        StartCoroutine(HighlightOnTaskStart());
    }

    public virtual void Finish()
    {
        isActive = false;
        associatedTask.Complete();
    }

    public void Highlight(bool force)
    {
        if(highlighter && (isActive && highlightOnLook || force))
            highlighter.SetActive(true);
    }

    public void TempHighlight()
    {
        if(highlighter != null)
            StartCoroutine(HighlightOnTaskStart());
    }

    public void UnHighlight()
    {
        if(highlighter)
            highlighter.SetActive(false);
    }

    protected void UpdateFun(float value)
    {
       // Debug.Log("Added " + value + " fun");
        funManager.ChangeFun(value);
    }

    public virtual void ResetTask(BaseTask task)
    {
        Debug.Log("Resetting " + name);

        associatedTask = task; // Next task on list
    }

    IEnumerator HighlightOnTaskStart()
    {
        Highlight(true);

        yield return new WaitForSeconds(6f);

        UnHighlight();
    }
}
