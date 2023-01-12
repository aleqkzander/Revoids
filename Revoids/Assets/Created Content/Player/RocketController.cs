using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Joystick")]
    public Joystick joystick;

    new private Rigidbody2D rigidbody;
    [Header("Movement")]
    public GameObject enginePower;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 4.0f;

    [Header("Grounded")]
    [HideInInspector]
    public bool isGrounded;
    private float checkRadius = 0.56f;



    private void Awake()
    {
        // get rigidbody;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float rotationAxis = 0;
        Vector2 drive = Vector2.zero;

        if (Application.isMobilePlatform)
        {
            // get joystick input
            rotationAxis = joystick.Horizontal;
            rigidbody.rotation += (-rotationAxis * rotationSpeed);

            // set and apply drive
            drive = new Vector2(0, joystick.Vertical);
            rigidbody.AddRelativeForce(drive * playerSpeed * 10);
        }
        else
        {
            // disable the joystick when windows
            joystick.gameObject.SetActive(false);

            // set and apply rotation
            rotationAxis = Input.GetAxis("Horizontal");
            rigidbody.rotation += (-rotationAxis * rotationSpeed);

            // set and apply drive
            drive = new Vector2(0, Input.GetAxis("Vertical"));
            rigidbody.AddRelativeForce(drive * playerSpeed * 10);
        }

        // activate/deactive engine power
        if (drive.sqrMagnitude > 0) enginePower.SetActive(true); else enginePower.SetActive(false);

        // set isgrounded
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Ground"));


        // reset velocity
        if (isGrounded)
        {
            // get collision detector
            RocketCollisionDetection collisionDetection = gameObject.transform.GetChild(3).GetComponent<RocketCollisionDetection>();


            // reset velocity
            collisionDetection.ResetSpeed();
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
