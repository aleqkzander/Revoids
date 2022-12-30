using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    new private Rigidbody2D rigidbody;

    private float rotationAxis;
    private float driveAxis;

    public float rotationSpeed = 2.0f;
    public float driveForce = 5.0f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ConsumeInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    /// <summary>
    /// Benutzereingaben registrieren und Werte zuweisen
    /// </summary>
    private void ConsumeInput()
    {
        rotationAxis = Input.GetAxis("Horizontal");
        driveAxis = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// Spielfigur bewegen bewegen (Nur Fixed Update benutzen. Gilt immer für physikalische Körper.)
    /// </summary>
    private void MovePlayer()
    {
        rigidbody.rotation -= rotationAxis * rotationSpeed;
        rigidbody.AddRelativeForce(Vector2.up * driveAxis * driveForce);
    }
}
