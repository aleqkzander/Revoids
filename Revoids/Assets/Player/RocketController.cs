using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    public float playerSpeed = 2.0f;


    private void Awake()
    {
        // get rigidbody;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        // set and apply rotation
        float rotationAxis = Input.GetAxis("Horizontal");
        rigidbody.rotation -= (rotationAxis * 1.0f);

        // set and apply drive
        Vector2 drive = new Vector2(0, Input.GetAxis("Vertical"));
        rigidbody.AddRelativeForce(drive * playerSpeed * 10);
    }


}
