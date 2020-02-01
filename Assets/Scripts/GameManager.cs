using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float countdown;
    public float targetTime = 60.0f;
    public List<GameObject> taskList;

    // Start is called before the first frame update
    void Start()
    {
        countdown = targetTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            countdown = targetTime;
            countDownFinished();
        }
    }

    void countDownFinished()
    {
        //Add a prefabed task???
    }
}