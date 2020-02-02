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

    public float funFactor;

    public override void Interact()
    {
        base.Interact();

        bool gameStateActive = GameManager.instance.gameState == GameManager.GameState.Playing || GameManager.instance.gameState == GameManager.GameState.Lose || GameManager.instance.gameState == GameManager.GameState.MaxedFun;

        if (isActive && gameStateActive) //&& numPiecesGarbage == maxPiecesGarbage)
        {
            //numPiecesGarbage = 0;

            GameObject newBag = Instantiate(garbageBagPrefab, transform.position, garbageBagPrefab.transform.rotation, null);

            newBag.GetComponent<GrabbableGarbage>().binOfOrigin = this;

            newBag.GetComponent<GrabbableGarbage>().binOfOrigin.Highlight(true); // Temporary for player to know where to go

            isActive = false;

            //GetComponent<Collider>().enabled = false;

            LookInteract.instance.selectedObject = newBag.GetComponent<GrabbableGarbage>();
            LookInteract.instance.AttemptGrab();
            UnHighlight();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableGarbage trash = other.GetComponent<GrabbableGarbage>();

        if (trash)
        {
            //if(numPiecesGarbage < maxPiecesGarbage)
            if (trash.smallTrash)
            {
                numPiecesGarbage++;
                Debug.Log("Added trash to bin");

                UpdateFun(trash.throwTime * funFactor);

                Destroy(other.gameObject);

                if(numPiecesGarbage == maxPiecesGarbage)
                {
                    //Finish(); // Can use later if have time
                }
            }

        }
    }
}
