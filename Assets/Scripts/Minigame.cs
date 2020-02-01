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

    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task


    public virtual void Interact()
    {

    }

    public void Finish()
    {
        associatedTask.Complete();
    }

}
