
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float introSceneCountdown = 10f;
    [SerializeField]
    float countdown;
    public float targetTime = 60.0f;

    public ObservableCollection<BaseTask> taskList = new ObservableCollection<BaseTask>();
    public int maximumTasksBeforeSwamped = 5;
    public BaseTask taskPrefab;

    // MaxedFun state for different style of ending; if you max out the fun-o-meter, then you can finish all tasks with ease (fun-o-meter no longer drops). You complete the tasks, THEN you take a nap and wake up as a kid again. The idea here is we want to give the impression of striking a balance
    public enum GameState { Intro, Dream, Start, Playing, Win, Lose, NA, Outro, MaxedFun } // Start state is for the time after you wake up, you discover note on fridge with task list (which enables the UI and "Playing" state)
    public GameState gameState = GameState.Intro;

    public delegate void StateChangeEvent(GameState newState);
    public static StateChangeEvent OnStateChange;

    public delegate void CreateTaskEvent(string taskName);
    public static CreateTaskEvent OnCreateTask;

    public delegate void TaskCompletedEvent(bool highlightOthers);
    public static TaskCompletedEvent OnTaskComplete;

    public ToDoList todoDisplay;

    public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        countdown = targetTime;
        taskList.CollectionChanged += TaskList_CollectionChanged;
        instance = this;

        OnStateChange += SwitchUI;

        ChangeState(gameState);
    }

    private void SwitchUI(GameState newState)
    {
        var uiActive = false;

        switch (gameState)
        {
            case GameState.Playing:
            case GameState.MaxedFun:
                uiActive = true;
                break;
            default:
                break;
        }

        todoDisplay.gameObject.SetActive(uiActive);
    }

    private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        todoDisplay.UpdateList(taskList.Select(x => x.name).ToList());
        CheckSwampedWithTasks();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
            case GameState.Lose:

                countdown -= Time.deltaTime;
                if (countdown <= 0f)
                {
                    countdown = targetTime;
                    CountDownFinished();
                }

                break;
            case GameState.Intro:

                introSceneCountdown -= Time.deltaTime;
                if (introSceneCountdown <= 0f)
                {
                    ChangeState(GameState.Dream);
                }

                break;
            default:
                break;
        }
    }

    void CountDownFinished()
    {
        CreateTask();
    }

    void CreateTask()
    {
        var newTask = Instantiate(taskPrefab, gameObject.transform);
        newTask.taskList = taskList;
    }

    public void TaskCompleted(string taskName)
    {
        OnTaskComplete(true);
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
        taskList = new ObservableCollection<BaseTask>();
        ChangeState(GameState.Start);
    }

    public void ChangeState(GameState newState)
    {
        if (newState != GameState.NA)
        {
            gameState = newState;
            Debug.Log("Game State changed to " + newState.ToString());

            OnStateChange(newState); // Trigger any pertinent cutscenes
        }
    }

    public void MaxedFun()
    {
        Debug.Log("You're now max fun");
        ChangeState(GameState.MaxedFun);
    }

    public void Win()
    {
        Debug.Log("You're now fun, game won");
        ChangeState(GameState.Win); // Ideally, the UI fades* out and player drops the task list on the ground 
    }

    public void Lose()
    {
        Debug.Log("You've forgot what fun is, game lost");
        ChangeState(GameState.Lose);
    }
}