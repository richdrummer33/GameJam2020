using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMinigame : Minigame
{
    public List<GameObject> garbageToDump;
    int numtoDump;

    private void OnTriggerEnter(Collider other)
    {
        if(garbageToDump.Contains(other.gameObject))
        {
            garbageToDump.Remove(other.gameObject);
            Destroy(other.gameObject, 5f);
            currentTaskCompletion = 1f - garbageToDump.Count / numtoDump;

            if(garbageToDump.Count == 0)
            {
                Finish();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numtoDump = garbageToDump.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
