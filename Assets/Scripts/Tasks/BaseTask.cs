using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTask : MonoBehaviour    
{
    public List<Minigame> possiblMiniGames;
    public Minigame assignedMinigame;
    public List<BaseTask> taskList;
    
    // Start is called before the first frame update
    void Start()
    {
        possiblMiniGames = FindObjectsOfType<Minigame>().ToList();
        assignedMinigame = possiblMiniGames[Random.Range(0, possiblMiniGames.Count)];
        assignedMinigame.isActive = true;
        assignedMinigame.associatedTask = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Complete()
    {
        taskList.Remove(this);
        Destroy(gameObject);
    }
}
