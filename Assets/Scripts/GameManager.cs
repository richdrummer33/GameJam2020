using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float countdown;
    public float targetTime = 60.0f;
    bool gameOver = false;

    public List<GameObject> taskList;
    public int maximumTasksBeforeSwamped = 5;
    public GameObject taskPrefab;

    // Start is called before the first frame update
    void Start()
    {
        countdown = targetTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
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
        var newTask = Instantiate(taskPrefab);
        taskList.Add(newTask);
    }

    void CheckSwampedWithTasks()
    {
        if (taskList.Count >= maximumTasksBeforeSwamped)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game over");
        gameOver = true;
    }
}