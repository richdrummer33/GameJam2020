using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public float cutsceneDuration;
    public GameManager.GameState thisState;
    public GameManager.GameState nextState = GameManager.GameState.NA;
    public GameObject character; // Story character (Genie?)

    public Transform moveToPosition; // Optional
    public bool fadeTransition; // visual fading?
    public float fadeDuration; // How long to fade to black 
    public Renderer visionBlocker; // Optional

    private void Start()
    {
        GameManager.OnStateChange += CheckStateOnChange;
    }

    public void CheckStateOnChange(GameManager.GameState newState)
    {
        if (newState == thisState)
            StartCutscene();
    }

    public void StartCutscene()
    {
        StartCoroutine(Transition(fadeDuration));
    }

    IEnumerator Transition(float fadeDuration)
    {
        if (visionBlocker) // Fade to black
        {
            float t = 0f;
            Material bMat = visionBlocker.material;
            Color col = bMat.color;
            while (t < fadeDuration)
            {
                col.a = t / fadeDuration;
                bMat.color = col;

                t += Time.deltaTime;
                yield return null;
            }
        }

        GetComponent<AudioSource>().Play();

        if (character)
            character.SetActive(true);

        if (moveToPosition)
            RigidbodyFPSController.instance.Teleport(moveToPosition);

        yield return new WaitForSeconds(cutsceneDuration); // Cutscene is playing.........
        
        GameManager.instance.ChangeState(nextState);

        if (character)
            character.SetActive(false);

        if (visionBlocker) // Fade back transparent (visible)
        {
            float t = 0f;
            Material bMat = visionBlocker.material;
            Color col = bMat.color;

            while (t < fadeDuration)
            {
                col.a = 1f - t / fadeDuration;
                bMat.color = col;

                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
