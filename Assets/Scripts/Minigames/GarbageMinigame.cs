using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
///  For the bigass garbage bin outside
/// </summary>
public class GarbageMinigame : Minigame
{
    [SerializeField]
    List<GarbageBinController> binsToEmpty;
    int numtoDump;
    public int numDumped;

    public float funFactor = 50f;

    public override void ResetTask(BaseTask task)
    {
        base.ResetTask(task);

        numDumped = 0;

        foreach(GarbageBinController bin in binsToEmpty)
        {
            bin.isActive = true;
        }
    }

    protected override void Start()
    {
        base.Start();
        binsToEmpty = FindObjectsOfType<GarbageBinController>().ToList();
        numtoDump = binsToEmpty.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableGarbage garbageBag = other.GetComponent<GrabbableGarbage>();

        if (garbageBag)
        {
            Debug.Log("Bin felt this bag");
            if (binsToEmpty.Contains(garbageBag.binOfOrigin))
            {
                Debug.Log("Bin accepts this bag");
                numDumped++;

                UpdateFun(garbageBag.throwTime * funFactor);

                Destroy(other.gameObject, 5f);
                currentTaskCompletion = 1f - numDumped / numtoDump;

                if (numDumped == numtoDump)
                {
                    Finish();
                }
            }
        }
    }



}
