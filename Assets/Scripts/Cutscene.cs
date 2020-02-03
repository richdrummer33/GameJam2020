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
    public bool stayfaded = false;
    public Color targetFadeColor = Color.black;

    public List<AudioClip> clips = new List<AudioClip>();
    int clipIndex;

    private void Awake()
    {
        GameManager.OnStateChange += CheckStateOnChange;
    }

    public void CheckStateOnChange(GameManager.GameState newState)
    {
        if (newState == thisState)
            StartCutscene();
    }

    void StartCutscene()
    {
        StartCoroutine(Transition(fadeDuration));
    }

    IEnumerator Transition(float fadeDuration)
    {
        if (clips.Count > 0)
            GetComponent<AudioSource>().clip = clips[clipIndex];
                
        if (visionBlocker) // Fade to black
        {
            float t = 0f;
            Material bMat = visionBlocker.material;
            Color col = targetFadeColor; //bMat.color;
            RigidbodyFPSController.instance.canMove = false;

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

        if (visionBlocker && !stayfaded) // Fade back transparent (visible)
        {
            float t = 0f;
            Material bMat = visionBlocker.material;
            Color col = targetFadeColor; // bMat.color;

            while (t < fadeDuration)
            {
                col.a = 1f - t / fadeDuration;
                bMat.color = col;

                t += Time.deltaTime;
                yield return null;
            }
        }

        if (clipIndex < clips.Count - 1)
            clipIndex++;

        RigidbodyFPSController.instance.canMove = true;
    }
}
