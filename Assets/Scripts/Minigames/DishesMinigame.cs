using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesMinigame : Minigame
{
    public Transform dishSpawnPosition;
    public GameObject dishPrefab;

    public List<Transform> dishRackslots = new List<Transform>();
    int currentSlot;

    float dishSpawnInterval; // At min click rate
    List<GameObject> spawnedDishes = new List<GameObject>();

    protected float currentClickRate;
    protected float lastClickTime; // use Time.time to get delta from this current click
    public float minClickRate = 1f; // Clicks per second to start progress

    public override void Activate()
    {
        base.Activate();
        ResetGame();
    }

    private void ResetGame()
    {
        foreach (var dish in spawnedDishes)
        {
            Destroy(dish);
        }
        spawnedDishes = new List<GameObject>();
        currentSlot = 0;
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

                if (currentTaskCompletion / dishSpawnInterval > currentSlot && currentSlot < dishRackslots.Count)
                {
                    GameObject dish = Instantiate(dishPrefab, dishSpawnPosition.position, dishSpawnPosition.rotation, dishSpawnPosition.transform);
                    spawnedDishes.Add(dish);
                    TweenAnimatePosition animator = dish.GetComponent<TweenAnimatePosition>();
                    animator.duration = 2f;
                    //Debug.Log("Current dish slot: " + (dishRackslots.Count - currentSlot - 1));
                    animator.destination = dishRackslots[dishRackslots.Count - currentSlot - 1];
                    animator.Animate();
                    currentSlot++;

                    //if (!source.isPlaying)
                        //PlaySound();

                    if (spawnedDishes.Count >= dishRackslots.Count)
                    {
                        Finish(); // TASK IS COMPLETE! Tell Game Manager (or task manager?) that it's done
                    }
                }
            }

            lastClickTime = Time.time;
        }
        else
        {
            Debug.Log("Minigame " + name + " is inactive");
        }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dishSpawnInterval = taskDuration / dishRackslots.Count;

        //source.loop = true;
        //source.volume = 0f;
        //source.Play(); // Loops
    }

    float fadeDuration = 0.5f;
    IEnumerator PlaySound()
    {
        float t = 0f;
        if (loopingSound)
        {
            while (t < fadeDuration)
            {
                source.volume = t / fadeDuration;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator StopSound()
    {
        float t = 0f;
        if (loopingSound)
        {
            while (t < fadeDuration)
            {
                source.volume = 1f - t / fadeDuration;
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
