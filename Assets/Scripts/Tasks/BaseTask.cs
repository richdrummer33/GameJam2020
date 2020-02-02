using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class BaseTask : MonoBehaviour    
{
    public List<Minigame> possiblMiniGames;
    public Minigame assignedMinigame;
    public ObservableCollection<BaseTask> taskList;
    
    // Start is called before the first frame update
    void Start()
    {
        possiblMiniGames = FindObjectsOfType<Minigame>().Where((x) => x.minigameType == Minigame.MinigameType.Task).ToList();
        assignedMinigame = possiblMiniGames[Random.Range(0, possiblMiniGames.Count)];
        assignedMinigame.Activate();
        assignedMinigame.associatedTask = this;
        name = assignedMinigame.taskName;
        taskList.Add(this);
        GameManager.OnTaskComplete += OnOtherTaskComplete;
        //GameManager.instance.TaskCreated(this);
    }

    public void Complete()
    {
        string completedTaskName = this.assignedMinigame.taskName;

        foreach (BaseTask task in taskList)
        {
            if (task != this) // RB added - switch to new task on the list to be active
            {
                if (task.assignedMinigame.taskName == completedTaskName)
                {
                    task.assignedMinigame.ResetTask(task);
                }
            }
        }

        GameManager.instance.TaskCompleted(assignedMinigame.taskName);
        taskList.Remove(this);
        Destroy(gameObject);
    }

    public void OnOtherTaskComplete(string name)
    {
        if(name != assignedMinigame.taskName)
        {
            assignedMinigame.TempHighlight();
        }
    }
}
