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


    public IEnumerator PlayerResetWithDelay(float delay)
    {
        // get statistic
        RocketStatistic statistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();


        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();


        // get modelsprite
        GameObject model = player.transform.GetChild(0).gameObject;


        // disable model
        model.SetActive(false);


        // freeze movement
        rigidbody.Sleep();


        yield return new WaitForSeconds(delay);


        // reset position
        player.transform.position = startPosition;


        // reset gloabl velocity
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;


        // reset rotation
        player.transform.rotation = Quaternion.identity;


        // wake up
        rigidbody.WakeUp();


        // eable model
        model.SetActive(true);
        //model.enabled = true;


        // remove on life
        statistic.lifes--;


        // reset statistic;
        statistic.rocketShields = 3;


        // update ui
        statistic.UpdateUI();
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
            // get statistic
            RocketStatistic statistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();

            // play explosion sound
            Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);


            if (statistic.lifes > 0)
            {
                statistic.lifes--;

                // get rigidbody
                Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

                // reset gloabl velocity
                rigidbody.velocity = Vector2.zero;
                rigidbody.angularVelocity = 0;

                // reset rotation
                player.transform.rotation = Quaternion.identity;

                // update ui
                statistic.UpdateUI();
            }
            else if (statistic.lifes == 0)
            {
                // call reset method
                StartCoroutine(PlayerResetWithDelay(2));

                // update ui
                statistic.UpdateUI();
            }
        }
    }
}
