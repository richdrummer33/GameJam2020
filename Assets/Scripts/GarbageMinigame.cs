using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  For the bigass garbage bin outside
/// </summary>
public class GarbageMinigame : Minigame
{
    public List<GarbageBinController> binsToEmpty;
    int numtoDump;
    int numDumped;

    private void OnTriggerEnter(Collider other)
    {
        GrabbableGarbage garbageBag = other.GetComponent<GrabbableGarbage>();

        if (garbageBag)
        {
            if (binsToEmpty.Contains(garbageBag.binOfOrigin))
            {
                numDumped++;
                Destroy(other.gameObject, 5f);
                currentTaskCompletion = 1f - numDumped / numtoDump;

                if (numDumped == numtoDump)
                {
                    Finish();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numtoDump = binsToEmpty.Count;
    }

}
