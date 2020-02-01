using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        Debug.Log("New Game Cliked");
    }

    public void OnCreditsClicked()
    {
        Debug.Log("Credits Cliked");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
