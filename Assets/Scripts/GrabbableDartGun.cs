using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableDartGun : GrabbableObject
{
    public Transform muzzle;
    public float fireForce = 15f;
    public GameObject dartPrefab;

    public override bool OnThrow(Vector3 throwVector)
    {
        GameObject dart = Instantiate(dartPrefab, muzzle.position, muzzle.rotation);

        dart.GetComponent<Rigidbody>().AddForce(muzzle.forward * fireForce, ForceMode.Impulse);

        return false;
    }
}
