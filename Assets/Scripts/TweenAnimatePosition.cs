using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimatePosition : MonoBehaviour
{
    public Transform destination;
    public float duration;

    // Start is  before the first frame update
    public void Animate()
    {
        iTween.MoveTo(gameObject, destination.position, duration);
        iTween.RotateTo(gameObject, destination.rotation.eulerAngles, duration);
    }
}
