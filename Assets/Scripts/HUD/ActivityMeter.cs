using UnityEngine;
using UnityEngine.UI;

public class ActivityMeter : MonoBehaviour
{
    public Slider activitySlider;

    private const int _defaultValue = 100;

    void Start()
    {
        activitySlider.value = _defaultValue;
    }
}
