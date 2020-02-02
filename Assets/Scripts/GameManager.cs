using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float countdown;
    public float targetTime = 60.0f;
    bool swamped = false;
    bool cutsceneActive = false;

    public List<BaseTask> taskList = new List<BaseTask>();
    public int maximumTasksBeforeSwamped = 5;
    public BaseTask taskPrefab;
    public CutsceneManager cutsceneMgr;
    public enum GameState { Start, Playing, Win, Lose }
    public GameState gameState = GameState.Start;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneMgr.StartCutscene(GameState.Start);

        countdown = targetTime;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!swamped && gameState == GameState.Playing)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                countdown = targetTime;
                CountDownFinished();
            }
        }
    }

    void CountDownFinished()
    {
        CreateTask();
        CheckSwampedWithTasks();
    }

    void CreateTask()
    {
        var newTask = Instantiate(taskPrefab, gameObject.transform);
        newTask.taskList = taskList;
        taskList.Add(newTask);
    }

    void CheckSwampedWithTasks()
    {
        if (taskList.Count >= maximumTasksBeforeSwamped)
        {
            GetSwamped();
        }
    }

    void GetSwamped()
    {
        Debug.Log("You were swamped with tasks for the day, but the next day comes");
        swamped = true;
    }

    public void SetState(GameState newState)
    {
        gameState = newState;
    }
}