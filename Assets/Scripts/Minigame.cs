using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all minigames
/// </summary>
public class Minigame : MonoBehaviour
{
    [SerializeField]
    protected float currentTaskCompletion; // Number of seconds contributed to task

    public float minClickRate = 1f; // Clicks per second to start progress

    public string taskName;

    public bool isActive;

    public virtual void Interact()
    {

    }
}
