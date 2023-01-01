using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewStation : MonoBehaviour
{
    public GameObject crewMemberPrefab;
    public int memberCount = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Zähle damit wie viele Crewmember bereits gespawned sind
            int memberToSpawn = memberCount;

            for (int i = 0; i < memberCount; i++)
            {
                // Ermittle einen Zufallswert
                int randomPositionAdd = Random.Range(0, 3);

                // Erstelle aus der Spielerpositionen und dem Zufallswert eine zweidimensionale Vektorposition
                Vector2 randomPosition = new Vector2(gameObject.transform.position.x + randomPositionAdd, gameObject.transform.position.y);

                // Spawne einen Crewmember
                Instantiate(crewMemberPrefab, randomPosition, Quaternion.identity);

                // Zähle die Member runter
                memberToSpawn--;
            }

            // Zerstöre die Station
            Destroy(gameObject);
        }
    }

}
