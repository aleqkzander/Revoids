using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionDetection : MonoBehaviour
{
    public EdgeCollider2D collisionCheck;
    public GameObject explosionSound;
    private GameObject player;
    private Vector2 startPosition;
    public GameObject mainScreen;


    private void Start()
    {
        // get player 
        player = GameObject.FindGameObjectWithTag("Player");

        // get start position from player
        startPosition = player.transform.position;

        // Freeze the player on start
        FreezePlayer();
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PlayerResetWithDelay();
        }
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
                PlayerResetWithDelay();
            }
        }
    }


    /// <summary>
    /// Use this method to reset player position and update the ui
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    public void PlayerResetWithDelay()
    {
        // get statistic
        RocketStatistic statistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();

        // reset statistic
        statistic.rocketShields = 3;

        // reset score
        statistic.score = 0;

        // freeze player
        FreezePlayer();

        // reset position
        player.transform.position = startPosition;

        // Reset speed and rotation
        ResetSpeedAndRotation();

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

        // clear position
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3);
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


    /// <summary>
    /// Use to freeze player
    /// </summary>
    /// <param name="rigidbody"></param>
    public void FreezePlayer()
    {
        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

        // get modelsprite
        GameObject model = player.transform.GetChild(0).gameObject;

        // enable music filter
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioLowPassFilter>().enabled = true;

        // activate gameover screen
        mainScreen.SetActive(true);

        // disable model
        model.SetActive(false);

        // freeze movement
        rigidbody.Sleep();

        // remove simulation
        rigidbody.simulated = false;
    }


    /// <summary>
    /// Use to unfreeze player
    /// </summary>
    public void UnfreezePlayer()
    {
        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();


        // get modelsprite
        GameObject model = player.transform.GetChild(0).gameObject;

        // enable music filter
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioLowPassFilter>().enabled = false;

        // wake up
        rigidbody.WakeUp();


        // eable model
        model.SetActive(true);

        // remove simulation
        rigidbody.simulated = true;
    }
}
