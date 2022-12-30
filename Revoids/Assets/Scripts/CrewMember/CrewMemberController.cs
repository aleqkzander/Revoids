using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    CrewMemberWalkAround walkAround;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        walkAround = GetComponent<CrewMemberWalkAround>();
    }


    private void FixedUpdate()
    {
        walkAround.MovePlayer(rigidbody);
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


}
