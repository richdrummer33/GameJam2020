using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailboxMinigame : Minigame
{
    public GameObject letterPrefab;
    GameObject currentLetter;
    public float funFactor;
    bool interacted = false;

    public override void Interact()
    {
        base.Interact();

        bool gameStateActive = GameManager.instance.gameState == GameManager.GameState.Playing || GameManager.instance.gameState == GameManager.GameState.Lose || GameManager.instance.gameState == GameManager.GameState.MaxedFun;

        if (isActive && gameStateActive) //&& numPiecesGarbage == maxPiecesGarbage)
        {
            currentLetter = Instantiate(letterPrefab, transform.position, letterPrefab.transform.rotation, null);
            
            interacted = true;
            isActive = false;

            LookInteract.instance.selectedObject = currentLetter.GetComponent<GrabbableGarbage>();
            LookInteract.instance.AttemptGrab();
            UnHighlight();
        }
    }

    private void Update()
    {
        if(!currentLetter && interacted)
        {
            interacted = false;
            Finish();
        }
    }

    public override void ResetTask(BaseTask task)
    {
        base.ResetTask(task);

        isActive = true;

    }

}
