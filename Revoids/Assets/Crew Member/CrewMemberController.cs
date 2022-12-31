using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberController : MonoBehaviour
{
    private Rigidbody2D memberRigidbody;
    private GameObject playerObject;
    private RocketController rocketController;
    private float checkRadius = 0.2f;
    private bool isGrounded;
    private bool walkRight;
    private float moveRange;


    private void Start()
    {
        // Hole den Rigidbody2D vom CrewMember
        memberRigidbody = GetComponent<Rigidbody2D>();

        // Hole den Spieler
        playerObject = GameObject.FindGameObjectWithTag("Player");

        // Hole den Controller vom Spieler mit dem Spieler
        rocketController = playerObject.GetComponent<RocketController>();

        // Ermittle einen Zufallswert für links/rechts
        moveRange = Random.Range(0.25f, 1.5f);

        // Wechsle rechts/links alle 2 Sekunden nach 0.1 Sekunden
        InvokeRepeating("MoveAround", 0.25f, 2);

    }


    void MoveAround()
    {
        // Setze bool link
        if (walkRight) walkRight = false;

        // Setze bool rechts
        else walkRight = true;
    }

    private void Update()
    {
        // Überprüfe ob das Männchen den Boden berührt
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Ground"));
    }


    private void FixedUpdate()
    {
        //  Nicht aus führen wenn Crew Member nicht auf dem Boden
        if (!isGrounded) return; 

        // Wenn der Spieler auf dem Boden lauf zum Spieler
        if (rocketController.isGrounded)
        {
            MoveTowardsPlayer(memberRigidbody);
        }
        // Wenn der Spieler in der Luft ist lauf herrum
        else
        {
            MoveAround(memberRigidbody);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prüfen ob der Kollisionscheck den Spieler ergeben hat - wenn unwahr führe die Funktion nicht weiter aus
        if (!collision.gameObject.CompareTag("Player")) return;

        // Hole das RocketStatistic vom GameObject welches die Kollision auslöst
        RocketStatistic rocketStatistic = collision.gameObject.GetComponent<RocketStatistic>();

        // Für die Methode im dortigen Script aus
        rocketStatistic.MemberEnterSpaceShip(collision, gameObject);
    }


    private void MoveTowardsPlayer(Rigidbody2D rigidbody)
    {
        // Richtung berechnen
        Vector2 direction = playerObject.transform.position - transform.position;

        // Vector normalisieren
        direction.Normalize();

        // Auf den Spieler zulaufen
        rigidbody.MovePosition((Vector2)transform.position + (new Vector2(direction.x, 0) * 1.5f * Time.deltaTime));
    }


    private void MoveAround(Rigidbody2D  rigidbody)
    {
        // Laufe nach rechts
        if (walkRight) rigidbody.velocity = Vector2.right * moveRange;

        // Laufe nach links
        else rigidbody.velocity = Vector2.left * moveRange;
    } 


    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
