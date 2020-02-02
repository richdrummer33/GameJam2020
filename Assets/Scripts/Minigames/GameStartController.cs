﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : Minigame
{
    public GameObject constantHighlight;
    public GameObject text;

    protected override void Start()
    {
        base.Start();

        minigameType = MinigameType.Fun; // Not a task
        isActive = false;
        constantHighlight.SetActive(false);
        text.SetActive(false);

        GameManager.OnStateChange += SetVisible;
    }

    public void SetVisible(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Start)
        {
            isActive = true;
            constantHighlight.SetActive(true);
            text.SetActive(true);
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

        iTween.MoveTo(gameObject, RigidbodyFPSController.instance.transform.position, 3f);
        iTween.ScaleBy(gameObject, Vector3.one * 0.25f, 3f);

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
