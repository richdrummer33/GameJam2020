using UnityEngine;

public class LookInteract : MonoBehaviour
{
    public static LookInteract instance;

    GrabbableObject heldObject;
    public GrabbableObject selectedObject;
    Minigame selectedGame;
    public Transform grabPosition;
    public float grabStrength = 30f;
    Rigidbody heldRb;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if ( heldObject )
        {
            if ( Input.GetKeyDown( KeyCode.Mouse0 ) )
            {
                AttemptRelease(0f);
            }
            else if ( Input.GetKeyDown( KeyCode.Mouse1 ) || Input.GetKeyDown( KeyCode.LeftShift ) )
            {
                AttemptRelease(10f);
            }

            if ( heldRb )
            {
                heldRb.AddForce((grabPosition.position - heldObject.transform.position) * grabStrength);

                Vector3 newDirection = Vector3.RotateTowards(heldObject.transform.forward, grabPosition.transform.forward, 10f * Time.deltaTime, 0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                heldObject.transform.rotation = Quaternion.LookRotation(newDirection);
            }

            return;
        }

        GameObject obj = AttemptSelect();
        if (obj)
        {
            GrabbableObject grabbableObj = obj.GetComponent<GrabbableObject>();

             if (grabbableObj)
            {
                selectedObject = grabbableObj;
                selectedObject.Highlight();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    AttemptGrab();
                }
            }
            else if (obj.GetComponent<Minigame>())
            {
                //Debug.Log("Minigame interaction");
                selectedGame = obj.GetComponent<Minigame>();
                selectedGame.Highlight(false);
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    selectedGame.Interact();
                }
            }
            else
            {
                selectedGame.UnHighlight();
                selectedGame = null;
            }
        }
        else if (selectedObject)
        {
            selectedObject.UnHighlight();
            selectedObject = null;
        }
        else if (selectedGame)
        {
            selectedGame.UnHighlight();
            selectedGame = null;
        }
    }

    GameObject AttemptSelect()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f))
        {
            if ( hit.transform.tag.Contains( "Grabbable" ) || hit.transform.CompareTag("Minigame"))
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    public void AttemptGrab()
    {
        if (selectedObject && !heldObject)
        {
            heldObject = selectedObject;

            heldObject.transform.parent = grabPosition;
            heldObject.transform.position = grabPosition.position;
            heldRb = heldObject.GetComponent<Rigidbody>();
            heldObject.OnGrab();

            selectedObject.UnHighlight();
            selectedObject = null;
        }
    }

    public void AttemptRelease(float throwForce)
    {
        if (heldObject)
        {
            heldObject.transform.parent = null;

            Vector3 throwVector = grabPosition.transform.forward * throwForce;

            if (heldObject.OnThrow(throwVector) || throwForce == 0f)
            {
                heldRb = null;
                heldObject = null;
            }
        }
    }
}
