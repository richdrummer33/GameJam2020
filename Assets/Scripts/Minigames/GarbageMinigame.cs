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
    List<GarbageBinController> activeBinsToEmpty; // Still need emptying
    int numtoDump;
    public int numDumped;
    public bool destroyOnCollision = true;

    public float funFactor = 50f;

    public string tagToCheck = "GrabbableGarbage";

    public override void ResetTask(BaseTask task)
    {
        base.ResetTask(task);

        numDumped = 0;

        foreach(GarbageBinController bin in binsToEmpty)
        {
            bin.isActive = true;
            bin.TempHighlight();
        }

        activeBinsToEmpty = binsToEmpty;
    }

    protected override void Start()
    {
        base.Start();
        binsToEmpty = FindObjectsOfType<GarbageBinController>().ToList();
        numtoDump = binsToEmpty.Count;
        activeBinsToEmpty = binsToEmpty;
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableGarbage garbageBag = other.GetComponent<GrabbableGarbage>();

        if (garbageBag && other.tag == tagToCheck)
        {
            Debug.Log("Bin felt this bag");
            if (binsToEmpty.Contains(garbageBag.binOfOrigin))
            {
                Debug.Log("Bin accepts this bag");
                numDumped++;

                UpdateFun(garbageBag.throwTime * funFactor);

                currentTaskCompletion = 1f - numDumped / numtoDump;

                if (numDumped == numtoDump)
                {
                    Finish();
                }
            }

            if(destroyOnCollision)
                Destroy(other.gameObject, 2f);

            UnHighlight();

            activeBinsToEmpty.Remove(garbageBag.binOfOrigin);
            foreach(GarbageBinController bin in activeBinsToEmpty)
            {
                bin.TempHighlight();
            }
        }
    }



}
