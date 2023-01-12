using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionDetection : MonoBehaviour
{
    public EdgeCollider2D collisionCheck;
    public GameObject explosionSound;
    private GameObject player;
    private Vector2 startPosition;


    private void Start()
    {
        // get player 
        player = GameObject.FindGameObjectWithTag("Player");

        // get start position from player
        startPosition = player.transform.position;
    }


    public void PlayerReset()
    {
        // reset position
        player.transform.position = startPosition;

        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

        // reset gloabl velocity
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;

        // reset rotation
        player.transform.rotation = Quaternion.identity;
    }


    public void ResetSpeed()
    {
        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

        // reset gloabl velocity
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionCheck.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // play explosion sound
            Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);

            // call reset method
            PlayerReset();
        }
    }
}
