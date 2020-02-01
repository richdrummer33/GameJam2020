using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all minigames
/// </summary>
public class Minigame : MonoBehaviour
{
    public float taskDuration;

    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task

    public float minClickRate = 1f; // Clicks per second to start progress

    protected float currentClickRate;

    protected float lastClickTime; // use Time.time to get delta from this current click

    public virtual void Interact()
    {

    }

}
