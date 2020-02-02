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
    public bool destroyGarbage = true;

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

                UpdateFun(garbageBag.throwTime * 50f);

                if(destroyGarbage)
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
    protected override void Start()
    {
        base.Start();
        binsToEmpty = FindObjectsOfType<GarbageBinController>().ToList();
        numtoDump = binsToEmpty.Count;
    }

}
