using UnityEngine;
using UnityEngine.UI;

public class FunMeter : MonoBehaviour
{
    public Slider funSlider;

    private const int _defaultValue = 50;

    void Start()
    {
        funSlider.value = _defaultValue;
    }
}
