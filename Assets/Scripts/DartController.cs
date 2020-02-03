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
        if (collision.transform.tag != "Player" && rb)
        {
            if (Random.Range(0f, 1f) > 0.75f)
            {
                transform.parent = collision.transform;
                rb.isKinematic = true;
            }

            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRb)
            {
                if (!otherRb.isKinematic)
                {
                    FunManager.instance.ChangeFun(funFactor);
                }
            }
            else
            {
                FunManager.instance.ChangeFun(funFactor / 2f);
            }
        }

        Destroy(rb);
        Destroy(gameObject, 15f);
    }
}
