using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public float MaximumY = 140f;
    public float softenYzone = 45f; // Buffer region on limits defined by MaximumY - softens/smooths the clamp and pushes camera back when in this zone
    public bool cursorVisible = false, cursorLock = false;

    Transform lookAtTemp;
    [HideInInspector] public bool lookedAt = false;
    Vector2 rot = new Vector2(0, 0);
    int wait = 50;
    float prevXrot;
    float prevYrot;
    Vector2 baseRot;
    bool baseRotStart = false;

    private static MouseLook _instance;
    public static MouseLook Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MouseLook>();

            return _instance;
        }
    }

    private void Update()
    {
        // if (!GameManager.instance.AppPaused())
        LookRotation();
    }

    private void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        if (!cursorVisible)
            Cursor.visible = false;

        if (cursorLock)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        if (!cursorVisible)
            Cursor.visible = true;

        if (cursorLock)
            Cursor.lockState = CursorLockMode.None;
    }

    // called in update every frame
    public void LookRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        rot.x = rot.x - xRot;
        rot.y = rot.y + yRot - Mathf.Round(rot.y / 360f) * 360f;

        //Debug.Log("UPD rot.y= " + rot.y + ", UPD rot.x = " + rot.x);

        transform.eulerAngles = (Vector2)rot;
        baseRotStart = true;
        wait--;
    }

    /*
    // overrides cam rotation to a new target transform. 
    // Called by a scene that wants to change cam rot to a specific object when its enabled.
    public void StartLookatSceneStart(Transform lookTarget)
    {
        lookedAt = false;
        SetLookRotation(lookTarget);
        wait = 50;
    }

    private void SetLookRotation(Transform lookAt)
    {
        Debug.Log("Changing cam look because this scene told me to look at some important transform, but was I asked?? No...No I wasn't");

        lookAtTemp = lookAt;

        float pi = (float)Mathf.PI;
        Vector3 targetDir = lookAt.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, pi * 2f, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
        rot.y = transform.eulerAngles.y;
        rot.x = transform.eulerAngles.x;
        transform.eulerAngles = (Vector2)rot;

        baseRot = rot;
        baseRotStart = false;
        lookedAt = true;
    }
    */
}
