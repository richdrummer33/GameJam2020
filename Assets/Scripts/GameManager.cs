
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float introSceneCountdown = 10f;
    [SerializeField]
    float countdown;
    public float targetTime = 60.0f;
    bool swamped = false;

    public ObservableCollection<BaseTask> taskList = new ObservableCollection<BaseTask>();
    public int maximumTasksBeforeSwamped = 5;
    public BaseTask taskPrefab;

    public enum GameState { Intro, Dream, Start, Playing, Win, Lose, NA } // Start state is for the time after you wake up, you discover note on fridge with task list (which enables the UI and "Playing" state)
    public GameState gameState = GameState.Intro;

    public delegate void StateChangeEvent(GameState newState);
    public static StateChangeEvent OnStateChange;

    public delegate void CreateTaskEvent(string taskName);
    public static CreateTaskEvent OnCreateTask;

    public ToDoList todoDisplay;

    public bool gameOver;
    public bool gamePaused;


    // Start is called before the first frame update
    void Start()
    {
        countdown = targetTime;
        taskList.CollectionChanged += TaskList_CollectionChanged;
        todoDisplay.gameObject.SetActive(false);
        instance = this;
        
        ChangeState(gameState);
    }

    private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        todoDisplay.UpdateList(taskList.Select(x => x.name).ToList());
        CheckSwampedWithTasks();
    }

    // Update is called once per frame
    void Update()
    {
        bool gameStateActive = GameManager.instance.gameState == GameManager.GameState.Playing || GameManager.instance.gameState == GameManager.GameState.Lose;

        if (!swamped && gameStateActive)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                countdown = targetTime;
                CountDownFinished();
            }
        }
        else if(gameState == GameState.Intro)
        {
            introSceneCountdown -= Time.deltaTime;
            if(introSceneCountdown <= 0f)
            {
                ChangeState(GameState.Dream);
            }
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

    /*
    public void TaskCreated(BaseTask newlyCreatedTask)
    {
        OnCreateTask(newlyCreatedTask.assignedMinigame.taskName);
    }
    */

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

    public void ChangeState(GameState newState)
    {
        if (newState != GameState.NA)
        {
            gameState = newState;
            Debug.Log("Game State changed to " + newState.ToString());

            if (newState == GameState.Playing)
            {
                todoDisplay.gameObject.SetActive(true);
            }

            OnStateChange(newState); // Trigger any pertinent cutscenes
        }
    }

    public void Win()
    {
        gameOver = true;
        Debug.Log("You're now fun, game won");
        OnStateChange(GameState.Win);
    }

    public void Lose()
    {
        gameOver = true;
        Debug.Log("You've forgot what fun is, game lost");
        OnStateChange(GameState.Lose);
    }
}