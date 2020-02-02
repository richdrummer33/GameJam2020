using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunManager : MonoBehaviour
{
    private float amount = 50;
    public float decaySpeed = 0.07f;
    public FunMeter funMeter;

    public bool gameOver = false;

    public float Amount
    {
        get => amount; set
        {
            if (value > 100)
            {
                amount = 100;
            }
            else if (value < 0)
            {
                amount = 0;
            }
            else
            {
                amount = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            ChangeFun(-decaySpeed);

            if (Amount <= 0)
            {
                Lose();
                return;
            }

            if (Amount >= 100)
            {
                Win();
                return;
            }
        }
    }

    private void Win()
    {
        gameOver = true;
        Debug.Log("You're now fun, game won");
    }

    private void Lose()
    {
        gameOver = true;
        Debug.Log("You've forgot what fun is, game lost");
    }

    public void ChangeFun(float changeAmount)
    {
        funMeter.UpdateValue(Amount += changeAmount);
    }
}
