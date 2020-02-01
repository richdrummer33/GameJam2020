using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableGarbage : GrabbableObject
{
    public float throwTime;
    bool isThrown;

    public override void OnThrow()
    {
        base.OnThrow();
        isThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        throwTime = 0f;
        isThrown = false;
    }

    void Update()
    {
        if(isThrown)
        {
            throwTime += Time.deltaTime;
        }
    }
}
