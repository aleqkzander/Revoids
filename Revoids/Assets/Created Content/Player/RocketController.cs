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
    private float _rotationAxis;
    private Vector2 _drive;

    [Header("Grounded")]
    public bool isGrounded;
    public float checkRadius = 0.6f;



    private void Awake()
    {
        // get rigidbody;
        rigidbody = GetComponent<Rigidbody2D>();


        // activate joystick when mobile
        if (Application.isMobilePlatform)
        {
            joystick.gameObject.SetActive(true);
        }
    }


    private void Update()
    {
        // consume input 

        if (Application.isMobilePlatform)
        {
            // set input
            _rotationAxis = joystick.Horizontal;

            // set drive
            _drive = new Vector2(0, joystick.Vertical);
        }
        else
        {
            // set rotation
            _rotationAxis = Input.GetAxis("Horizontal");

            // set drive
            _drive = new Vector2(0, Input.GetAxis("Vertical"));
        }
    }


    private void FixedUpdate()
    {
        // apply rotation
        rigidbody.rotation += (-_rotationAxis * rotationSpeed);


        // apply drive
        rigidbody.AddRelativeForce(_drive * playerSpeed * 10);


        // activate/deactive engine power
        if (_drive.sqrMagnitude > 0) enginePower.SetActive(true); else enginePower.SetActive(false);


        // set isgrounded
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Ground"));


        // reset velocity
        if (isGrounded)
        {
            #region dont reset anymore
            // get collision detector
            //RocketCollisionDetection collisionDetection = gameObject.transform.GetChild(3).GetComponent<RocketCollisionDetection>();


            // reset speed and rotation
            //collisionDetection.ResetSpeedAndRotation();
            #endregion
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
