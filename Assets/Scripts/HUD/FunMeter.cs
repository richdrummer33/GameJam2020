using UnityEngine;
using UnityEngine.UI;

public class FunMeter : MonoBehaviour
{
    public Slider funSlider;

    private const float _defaultValue = 50;

    void Start()
    {
        funSlider.value = _defaultValue;
    }

    public void UpdateValue(float newValue)
    {
        funSlider.value = newValue;
    }
}
