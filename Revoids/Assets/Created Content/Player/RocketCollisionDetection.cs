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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionCheck.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            // get statistic
            RocketStatistic statistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();


            // play explosion sound
            Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);


            // remove a rocketshield
            if (statistic.rocketShields > 0)
            {
                RemoveRocketShield(statistic);
            }


            if (statistic.rocketShields == 0)
            {
                // call reset method
                StartCoroutine(PlayerResetWithDelay(2));
            }
        }
    }


    /// <summary>
    /// Use this method to reset player position and update the ui
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
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


        // wait amount of time
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


        // remove on life
        statistic.lifes--;


        // reset statistic;
        statistic.rocketShields = 3;


        // update ui
        statistic.UpdateUI();
    }


    /// <summary>
    /// Use method to reset speed and rotation
    /// </summary>
    public void ResetSpeedAndRotation()
    {
        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

        // reset gloabl velocity
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;

        // reset rotation
        player.transform.rotation = Quaternion.identity;
    }


    /// <summary>
    /// Use this method to remove a rocketshield
    /// </summary>
    /// <param name="statistic"></param>
    private void RemoveRocketShield(RocketStatistic statistic)
    {
        // remove one shield
        statistic.rocketShields--;

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
}
