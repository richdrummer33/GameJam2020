using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampBehaviour : Minigame
{
    public GameObject bouncingobject;
    public Transform bouncingtransform;
    public bool triggered;
    public float bounceforce = 8;
    public Vector3 disttocenter;
    public float difficultyforce = 5;
    [SerializeField]
    RigidbodyFPSController player;
    public float funFactor = 0.05f;


    // public Rigidbody rb;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        // Rigidbody rb = other.GetComponent<Rigidbody>();
        bouncingtransform = other.GetComponent<Transform>();
        bouncingobject = other.gameObject; // set bouncingTHINGY to the collidingTHINGY

        Vector3 flattenedpos = other.transform.position; 
        flattenedpos.y = transform.position.y;
        disttocenter = (flattenedpos - transform.position); // flatten position and then to get x & z distance from trampoline center
        other.transform.GetComponent<Rigidbody>().AddForce(disttocenter * difficultyforce, ForceMode.Impulse); // use x & z distance from center to apply a destabilizing difficulty force, which is less severe the nearer to the "bullseye" the player landed

        player = other.transform.GetComponent<RigidbodyFPSController>();
        noBounceTime = 0f;
    }


    protected override void Start()
    {


    }

    float noBounceTime;
    void Update()
    {
        if (triggered)
        {

            bouncingobject.GetComponent<Rigidbody>().AddForce(bouncingtransform.up * bounceforce, ForceMode.Impulse); // make the bouncing thing bounce!
            triggered = false;


        }

        if(player)
        {
            if(!player.grounded)
            {
                UpdateFun(funFactor * Time.deltaTime);
            }
            else
            {
                noBounceTime += Time.deltaTime;

                if(noBounceTime > 1f)
                    player = null;
                
            }
        }
    }
}




