using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For the little garbage bins in das hause
/// </summary>
public class GarbageBinController : Minigame
{
    public GameObject garbageBagPrefab;

    public override void Interact()
    {
        base.Interact();

        if (isActive)
        {
            GameObject newBag = Instantiate(garbageBagPrefab, transform.position, garbageBagPrefab.transform.rotation, null);

            newBag.GetComponent<GrabbableGarbage>().binOfOrigin = this;

            isActive = false;

            GetComponent<Collider>().enabled = false;

            LookInteract.instance.selectedObject = newBag.GetComponent<GrabbableGarbage>();
            LookInteract.instance.AttemptGrab();
            UnHighlight();
        }
    }
}
