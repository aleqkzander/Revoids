using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
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
        // set and apply rotation
        float rotationAxis = Input.GetAxis("Horizontal");
        rigidbody.rotation += (-rotationAxis * rotationSpeed);

        // set and apply drive
        Vector2 drive = new Vector2(0, Input.GetAxis("Vertical"));
        rigidbody.AddRelativeForce(drive * playerSpeed * 10);
        
        // activate/deactive engine power
        if (drive.sqrMagnitude > 0) enginePower.SetActive(true); else enginePower.SetActive(false);

        // set isgrounded
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Ground"));
    }


    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
