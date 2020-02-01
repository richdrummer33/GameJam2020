using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHUD : MonoBehaviour
{
    public Slider activityMetre;
    public Slider funMetre;

    void Start()
    {
        activityMetre.value = 25;
        funMetre.value = 10;
    }
}
