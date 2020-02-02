
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float countdown;
    public float targetTime = 60.0f;
    bool swamped = false;

    public ObservableCollection<BaseTask> taskList = new ObservableCollection<BaseTask>();
    public int maximumTasksBeforeSwamped = 5;
    public BaseTask taskPrefab;

    public ToDoList todoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        countdown = targetTime;
        taskList.CollectionChanged += TaskList_CollectionChanged;
    }

    private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        todoDisplay.UpdateList(taskList.Select(x => x.name).ToList());
        CheckSwampedWithTasks();
    }

    // Update is called once per frame
    void Update()
    {
        if (!swamped)
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
    }

    void CreateTask()
    {
        var newTask = Instantiate(taskPrefab, gameObject.transform);
        newTask.taskList = taskList;
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
}