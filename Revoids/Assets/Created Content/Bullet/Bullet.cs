using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionSound;


    /// <summary>
    /// shoot the bullet in forward direction
    /// </summary>
    /// <param name="shootingPoint"></param>
    public void ShootBullet(Transform shootingPoint)
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(shootingPoint.transform.up * 20, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.CompareTag("CrewStation"))
        {
            // get the crewstation from collsion and execute spawn method
            collision.gameObject.GetComponent<CrewStation>().SpawnCrewMembers();
        }

        // play explosion sound
        Instantiate(explosionSound, transform.position, Quaternion.identity);

        // Destroy bullet
        Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        // Destroy when not visible
        Destroy(gameObject);
    }
}
