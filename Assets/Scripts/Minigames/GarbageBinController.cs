using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For the little garbage bins in das hause
/// </summary>
public class GarbageBinController : Minigame
{
    public GameObject garbageBagPrefab;

    [SerializeField]
    int numPiecesGarbage = 5;

    public int maxPiecesGarbage = 5;

    public override void Interact()
    {
        base.Interact();

        if (isActive) //&& numPiecesGarbage == maxPiecesGarbage)
        {
            //numPiecesGarbage = 0;

            GameObject newBag = Instantiate(garbageBagPrefab, transform.position, garbageBagPrefab.transform.rotation, null);

            newBag.GetComponent<GrabbableGarbage>().binOfOrigin = this;

            isActive = false;

            //GetComponent<Collider>().enabled = false;

            LookInteract.instance.selectedObject = newBag.GetComponent<GrabbableGarbage>();
            LookInteract.instance.AttemptGrab();
            UnHighlight();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GrabbableTrashBits>())
        {
            if(numPiecesGarbage < maxPiecesGarbage)
            {
                numPiecesGarbage++;
                Debug.Log("Added trash to bin");

                Destroy(other.gameObject);

                if(numPiecesGarbage == maxPiecesGarbage)
                {
                    //Finish(); // Can use later if have time
                }
            }

        }
    }
}
