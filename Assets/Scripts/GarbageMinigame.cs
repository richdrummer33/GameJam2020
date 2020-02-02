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

                FunFactor(garbageBag.throwTime);

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

    public override float FunFactor(float fun)
    {
        return fun;
    }

    // Start is called before the first frame update
    void Start()
    {
        binsToEmpty = FindObjectsOfType<GarbageBinController>().ToList();
        numtoDump = binsToEmpty.Count;
    }

}
