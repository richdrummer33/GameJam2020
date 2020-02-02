using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampBehaviour : MonoBehaviour
{
    public GameObject playcontroller;
    private bool triggered;
    public float bounceforce = 1.0f;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        playcontroller = other.GameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
    void Start()
    {
        triggered = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered)
        {
            playcontroller.AddForce(transform.up * bounceforce);
        }
    }
}
