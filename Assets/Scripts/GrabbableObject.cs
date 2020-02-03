using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public GameObject highlighter;
    public float grabDrag = 5f;
    float defaultDrag;
    Rigidbody rb;

    public enum Label { NotFun, Fun }
    public Label label = Label.NotFun;

    private void OnEnable()
    {
        highlighter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.drag;
        GameManager.OnStateChange += TempHighlight;
    }

    public void Highlight()
    {
        highlighter.SetActive(true);
    }

    public void TempHighlight(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.Playing && label == Label.Fun)
            StartCoroutine(TempHighlightTimer());
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

    IEnumerator TempHighlightTimer()
    {
        Highlight();

        yield return new WaitForSeconds(GameManager.instance.targetTime); // wait for first task

        UnHighlight();
    }
}
