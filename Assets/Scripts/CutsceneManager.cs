using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Cutscene startCutscene, loseCutscene, winCutscene;


    public void StartCutscene(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Win)
        {
            winCutscene.StartCutscene();
        }
        else if (state == GameManager.GameState.Lose)
        {
            loseCutscene.StartCutscene();
        }
        else if (state == GameManager.GameState.Start)
        {
            startCutscene.StartCutscene();
        }
    }
}
