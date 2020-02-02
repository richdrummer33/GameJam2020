using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunManager : MonoBehaviour
{
    public static FunManager instance;

    private float amount = 50;
    public float decaySpeed = 0.07f;
    public FunMeter funMeter;


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
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver && GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            ChangeFun(-decaySpeed);

            if (Amount <= 0)
            {
                GameManager.instance.Lose();
                return;
            }

            if (Amount >= 100)
            {
                GameManager.instance.Win();
                return;
            }
        }
    }



    public void ChangeFun(float changeAmount)
    {
        funMeter.UpdateValue(Amount += changeAmount);
    }
}
