using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class RigidbodyFPSController : MonoBehaviour
{
    public static RigidbodyFPSController instance;

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public bool grounded = true;
    public bool canMove = true;

    public float maxSprintDuration = 4f;
    float sprintTime;
    public float sprintSpeedModifier = 1.5f;
    float walkSpeed;
    bool scaled;

    Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.freezeRotation = true;
        myRigidbody.useGravity = false;
        walkSpeed = speed;
        GameManager.OnStateChange += GrowUp;
        instance = this;
    }

    public void GrowUp(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.Start && !scaled)
        {
            Vector3 scale = transform.localScale;
            scale.y *= 1.65f;
            transform.localScale = scale;
            scaled = true;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            speed = walkSpeed;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (sprintTime < maxSprintDuration)
                {
                    speed = walkSpeed * sprintSpeedModifier;
                    sprintTime += Time.deltaTime;
                }
            }
            else
            {
                sprintTime = Mathf.Clamp(sprintTime - Time.deltaTime, 0f, Mathf.Infinity);
            }

            Vector3 fwdVel = Camera.main.transform.forward.normalized;
            fwdVel.y = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                fwdVel *= speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                fwdVel *= -speed;
            }
            else
            {
                fwdVel *= 0f;
            }

            Vector3 sideVel = Camera.main.transform.right.normalized;
            sideVel.y = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                sideVel *= -speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                sideVel *= speed;
            }
            else
            {
                sideVel *= 0f;
            }


            // Calculate how fast we should be moving
            Vector3 targetVelocity = fwdVel + sideVel; //new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = myRigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            myRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                myRigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        myRigidbody.AddForce(new Vector3(0, -gravity * myRigidbody.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        if(tag != "IgnoreCollision")
            grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void Teleport(Transform pos)
    {
        transform.position = pos.position;
    }



}
