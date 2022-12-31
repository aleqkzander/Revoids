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

    [Header("Movement")]
    [HideInInspector]
    public bool isGrounded;
    private float checkRadius = 0.5f;

    [Header("Shooting")]
    public GameObject shootPosition;
    public GameObject bulletPrefab;
    public float shootForce = 10.0f;
    


    private void Awake()
    {
        // Nehme die Rigidbody2D Komponente vom GameObject
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        // Lege die X-Achse fest
        rotationAxis = Input.GetAxis("Horizontal");
        // Lege die Y-Achse fest
        driveAxis = Input.GetAxis("Vertical");
        // Prüfe ob der Spieler auf der Erde ist
        isGrounded = Physics2D.OverlapCircle(gameObject.transform.position, checkRadius, LayerMask.GetMask("Ground"));

        // Shoot als Methode belassen da später einem Button zuweisen auf dem Mobile Device
        Shoot();
    }


    private void FixedUpdate()
    {
        // Spielfigur bewegen
        rigidbody.rotation -= rotationAxis * rotationSpeed;
        rigidbody.AddRelativeForce(Vector2.up * driveAxis * driveForce);
    }


    /// <summary>
    /// Schießt eine Kugel wenn man Space drückt
    /// </summary>
    private void Shoot()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (bulletPrefab == null) return;
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootPosition.transform.up * shootForce, ForceMode2D.Impulse);
        } 
    }


    /// <summary>
    /// Debug GroundCheck Radius
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
