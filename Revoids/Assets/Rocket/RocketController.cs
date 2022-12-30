using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private float rotationAxis;
    private float driveAxis;

    [Header("Movement")]
    public float rotationSpeed = 2.0f;
    public float driveForce = 5.0f;

    [Header("Grounded")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        ConsumeInputAndGroundCheck();

    }


    private void FixedUpdate()
    {
        MovePlayer();
    }


    /// <summary>
    /// Benutzereingaben registrieren und Werte zuweisen
    /// </summary>
    private void ConsumeInputAndGroundCheck()
    {
        // Lege die X-Achse fest
        rotationAxis = Input.GetAxis("Horizontal");
        // Lege die Y-Achse fest
        driveAxis = Input.GetAxis("Vertical");
        // Prüfe ob der Spieler auf der Erde ist
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
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
