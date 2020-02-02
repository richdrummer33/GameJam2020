using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public float delay;
    public float duration;
    public GameManager.GameState transitionState;
    public GameObject character;

    private void Start()
    {
        
    }

    public void StartCutscene()
    {
        character.SetActive(true);

        GetComponent<AudioSource>().PlayDelayed(delay);

        StartCoroutine(DelayedComplete());
    }

    void StopCutscene()
    {
        character.SetActive(false);

        GetComponent<AudioSource>().Stop();
    }

    IEnumerator DelayedComplete()
    {
        yield return new WaitForSeconds(duration);

        StopCutscene();
        GameManager.instance.SetState(transitionState);
    }
}
