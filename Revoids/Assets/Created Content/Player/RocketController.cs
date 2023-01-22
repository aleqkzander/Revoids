using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("MobileControll")]
    public Joystick joystick;
    public GameObject driveButton;
    public GameObject driveButtonImage;

    new private Rigidbody2D rigidbody;
    [Header("Movement")]
    public GameObject enginePower;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 4.0f;
    private Vector2 _drive;
    private float _rotationAxis;

    [Header("Grounded")]
    public bool isGrounded;
    public float checkRadius = 0.6f;



    private void Awake()
    {
        // get rigidbody;
        rigidbody = GetComponent<Rigidbody2D>();


        // activate joystick when mobile
        if (!Application.isMobilePlatform)
        {
            joystick.gameObject.SetActive(false);
            driveButton.SetActive(false);
        }
    }


    private void Update()
    {
        // consume input 

        if (Application.isMobilePlatform)
        {
            // set rotation
            _rotationAxis = joystick.Horizontal;
        }
        else
        {
            // set drive
            _drive = new Vector2(0, Input.GetAxis("Vertical"));

            // set rotation
            _rotationAxis = Input.GetAxis("Horizontal");
        }
    }


    public void ApplyDrive()
    {
        // set drive in mobile
        _drive = new Vector2(0, 1);

        // enable button
        driveButtonImage.SetActive(true);
    }


    public void RemoveDrive()
    {
        // set drive in mobile
        _drive = new Vector2(0, 0);

        // disable button
        driveButtonImage.SetActive(false);
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
