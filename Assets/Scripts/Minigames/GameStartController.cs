using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : Minigame
{
    Vector3 defaultLocation;
    Vector3 defaultScale;
    bool hasStarted;

    protected override void Start()
    {
        base.Start();

        minigameType = MinigameType.Fun; // Not a task
        isActive = false;
        //text.SetActive(false);

        if (!hasStarted)
        {
            defaultLocation = transform.position;
            defaultScale = transform.localScale;
            hasStarted = true;
        }

        GameManager.OnStateChange += SetVisible;
    }
    
    public void ResetThisCard()
    {
        transform.position = defaultLocation;
        transform.localScale = defaultScale;
    }

    public void SetVisible(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Start)
        {
            isActive = true;
        }
    }

    public override void Interact()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Start)
        {
            base.Interact();

            isActive = false;

            StartCoroutine(TakeThisPostitNote());
        }
    }

    IEnumerator TakeThisPostitNote()
    {
        RigidbodyFPSController.instance.canMove = false;
        MouseLook.Instance.cursorLock = true;

        iTween.MoveTo(gameObject, RigidbodyFPSController.instance.transform.position, 2f);
        iTween.ScaleBy(gameObject, Vector3.one * 0.25f, 2f);

        yield return new WaitForSeconds(3f);

        RigidbodyFPSController.instance.canMove = true;
        MouseLook.Instance.cursorLock = false;
        GameManager.instance.ChangeState(GameManager.GameState.Playing);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnStateChange -= SetVisible;
    }
}
