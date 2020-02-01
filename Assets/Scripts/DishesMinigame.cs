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

    public bool isActive;

    public override void Interact()
    {
        if (isActive)
        {
            base.Interact();

            currentClickRate = 1f / (Time.time - lastClickTime);
            Debug.Log("Current click rate " + currentClickRate);

            if (currentClickRate > minClickRate)
            {
                currentTaskCompletion += (Time.time - lastClickTime); // * currentClickRate / minClickRate;

                if (currentTaskCompletion / dishSpawnInterval > currentSlot && currentSlot < dishRackslots.Count)
                {
                    GameObject dish = Instantiate(dishPrefab, dishSpawnPosition.position, dishSpawnPosition.rotation);
                    TweenAnimatePosition animator = dish.GetComponent<TweenAnimatePosition>();
                    animator.duration = 2f;
                    Debug.Log("asd " + (dishRackslots.Count - currentSlot - 1));
                    animator.destination = dishRackslots[dishRackslots.Count - currentSlot - 1];
                    animator.Animate();
                    currentSlot++;
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
    void Start()
    {
        dishSpawnInterval = taskDuration / dishRackslots.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
