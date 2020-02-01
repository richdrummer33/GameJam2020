using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteract : MonoBehaviour
{
    GrabbableObject heldObject;
    GrabbableObject selectedObject;
    public Transform grabPosition;
    Rigidbody heldRb;

    void Start()
    {
        
    }
    
    void Update()
    {
        GrabbableObject newObj = AttemptSelect();

        if(!newObj && selectedObject)
        {
            selectedObject.UnHighlight();
            selectedObject = null;
        }
        else if (newObj)
        {
            selectedObject = newObj;
            selectedObject.Highlight();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttemptGrab();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            AttemptRelease(0f);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AttemptRelease(10f);
        }

        if(heldRb)
        {
            heldRb.AddForce((grabPosition.position - heldObject.transform.position) * 10f);

            Vector3 newDirection = Vector3.RotateTowards(heldObject.transform.forward, grabPosition.transform.forward, 10f * Time.deltaTime, 0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            heldObject.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    GrabbableObject AttemptSelect()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
        {
            if (hit.transform.tag == "Grabbable")
            {
                return hit.transform.GetComponent<GrabbableObject>();
            }
        }

        return null;
    }

    void AttemptGrab()
    {
        if (selectedObject && !heldObject)
        {
            heldObject = selectedObject;

            heldObject.transform.parent = grabPosition;
            heldObject.transform.position = grabPosition.position;
            heldRb = heldObject.GetComponent<Rigidbody>();
        }
    }

    void AttemptRelease(float throwForce)
    {
        if(heldObject)
        {
            heldObject.transform.parent = null;

            heldRb.AddForce(grabPosition.transform.forward * throwForce, ForceMode.Impulse);
            heldRb = null;
            heldObject = null;
        }
    }
}
