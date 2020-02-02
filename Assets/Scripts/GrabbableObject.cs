using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public GameObject highlighter;
    public float grabDrag = 5f;
    float defaultDrag;
    Rigidbody rb;

    private void OnEnable()
    {
        highlighter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.drag;
    }

    public void Highlight()
    {
        highlighter.SetActive(true);
    }

    public void UnHighlight()
    {
        highlighter.SetActive(false);
    }

    public virtual bool OnThrow(Vector3 throwVector)
    {
        Debug.Log("Threw " + name);

        rb.AddForce(throwVector, ForceMode.Impulse);
        rb.drag = defaultDrag;
        return true;
    }

    public virtual void OnGrab()
    {
        Debug.Log("Grabbed " + name);

        rb.drag = grabDrag;
    }
}
