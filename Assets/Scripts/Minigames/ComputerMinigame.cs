using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMinigame : Minigame
{
    int currentImage;

    float imageChangeInterval; // At min click rate
    public List<Texture2D> screenImages = new List<Texture2D>();
    public Texture2D emailSentImage;

    protected float currentClickRate;
    protected float lastClickTime; // use Time.time to get delta from this current click
    public float minClickRate = 1f; // Clicks per second to start progress

    public Renderer crtDisplay;
    Material displayMat;

    public override void Activate()
    {
        base.Activate();
        ResetGame();
    }

    private void ResetGame()
    {
        StartCoroutine(DelayedRestart());
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds( 2f);

        currentTaskCompletion = 0f;
    }

    public override void ResetTask(BaseTask task)
    {
        base.ResetTask(task);

        ResetGame();

        isActive = true;
    }

    public override void Interact()
    {
        if (isActive)
        {
            base.Interact();

            currentClickRate = 1f / (Time.time - lastClickTime);
            //Debug.Log("Current click rate " + currentClickRate);

            if (currentClickRate > minClickRate)
            {
                currentTaskCompletion += (Time.time - lastClickTime); // * currentClickRate / minClickRate;

                currentImage = Mathf.RoundToInt(currentTaskCompletion / taskDuration * screenImages.Count);

                displayMat.mainTexture = screenImages[currentImage % screenImages.Count];

                if (currentTaskCompletion > taskDuration)
                {
                    Finish(); // TASK IS COMPLETE! Tell Game Manager (or task manager?) that it's done
                }
            }

            lastClickTime = Time.time;
        }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        displayMat = crtDisplay.material;
        imageChangeInterval = taskDuration / screenImages.Count;
    }

    public override void Finish()
    {
        base.Finish();

        displayMat.mainTexture = emailSentImage;
    }
}
