using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject explosionSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Spawne die Explosion
            Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);

            // Zerstöre die Kugel
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // wenn die szene verlassen wird
        Destroy(gameObject);
    }
}
 