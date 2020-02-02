using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : Minigame
{
    Vector3 stuckPos;
    Quaternion stuckRot;
    Rigidbody rb;
    public float funFactor = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Random.Range(0f, 1f) > 0.75f)
        {
            transform.parent = collision.transform;
            rb.isKinematic = true;
            Destroy(gameObject, 15f);
        }

        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

        if (otherRb)
        {
            if (!otherRb.isKinematic)
            {
                FunManager.instance.ChangeFun(funFactor);
            }
        }
    }
}
