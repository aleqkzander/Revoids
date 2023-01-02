using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    public float playerSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    [HideInInspector]
    public bool isGrounded;


    private void Awake()
    {
        // get rigidbody;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        // set and apply rotation
        float rotationAxis = Input.GetAxis("Horizontal");
        rigidbody.rotation += (-rotationAxis * rotationSpeed);

        // set and apply drive
        Vector2 drive = new Vector2(0, Input.GetAxis("Vertical"));
        rigidbody.AddRelativeForce(drive * playerSpeed * 10);        
    }


    #region set ground check
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
    #endregion


}
