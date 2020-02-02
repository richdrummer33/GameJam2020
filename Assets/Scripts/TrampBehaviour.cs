using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampBehaviour : MonoBehaviour
{
    public GameObject bouncingobject;
    public Transform bouncingtransform;
    public bool triggered;
    public float bounceforce = 2;
    public Rigidbody rb;
    // public Rigidbody rb;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        // Rigidbody rb = other.GetComponent<Rigidbody>();
        bouncingtransform = other.GetComponent<Transform>();
        bouncingobject = other.gameObject;
        
    }


    void Start()
    {
       

    }

    
    void Update()
    {
        if (triggered)
        {
            bouncingobject.GetComponent<Rigidbody>().AddForce(bouncingtransform.up * bounceforce, ForceMode.Impulse);
            triggered = false;

        }
    }
}

