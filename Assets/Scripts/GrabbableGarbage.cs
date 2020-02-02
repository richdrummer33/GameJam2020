using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableGarbage : GrabbableObject
{
    public float throwTime;
    bool isThrown;
    public GarbageBinController binOfOrigin; // Bin that spawned this bag (if spawned)
    public bool smallTrash = false;

    public override bool OnThrow(Vector3 throwVector)
    {
        isThrown = true;
        base.OnThrow(throwVector);
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "IgnoreCollision")
        {
            print("This garbage collided with " + collision.transform.name + " with tag " + collision.transform.tag);
            throwTime = 0f;
            isThrown = false;
        }
    }

    void Update()
    {
        if(isThrown)
        {
            throwTime += Time.deltaTime;
        }
    }
}
