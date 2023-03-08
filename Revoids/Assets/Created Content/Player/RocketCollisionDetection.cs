using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionDetection : MonoBehaviour
{
    public EdgeCollider2D collisionCheck;
    public GameObject explosionSound;
    private GameObject player;
    private Vector2 startPosition;
    private GameObject audioManager;
    public GameObject mainScreen;
    private GameObject adsManager;



    private void Start()
    {
        // get player 
        player = GameObject.FindGameObjectWithTag("Player");

        // get ads manager
        adsManager = GameObject.Find("AdsManager");

        // get audio manager
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");

        // get start position from player
        startPosition = player.transform.position;

        // Freeze the player on start
        FreezePlayer();
    }

    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PlayerResetWithDelay();
        }

        if (Input.touchCount == 4)
        {
            FreezePlayer();
        }
    }


    [System.Obsolete] // lootlocker seems to be outdated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionCheck.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // When player is hitting the ground falsely
            PlayerHit();
        }
    }


    /// <summary>
    /// Call method on player damage
    /// </summary>
    [System.Obsolete]
    public void PlayerHit()
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

        // prepare ads
        if (statistic.rocketShields == 1)
        {
            // laod ads
            adsManager.GetComponent<InterstitialAd>().LoadAd();
        }

        if (statistic.rocketShields == 0)
        {
            // get playersettings
            PlayerSettings playerSettings = GameObject.Find("GameManager").GetComponent<PlayerSettings>();

            // get leaderboardmanager
            LeaderboardManager leaderboardManager = GameObject.Find("LeaderboardManager").GetComponent<LeaderboardManager>();


            #region obsoblet (moved to playerrestwithdelay)

            // save score to leaderboard when currentscore is bigger then player highscore
            if (statistic.score > playerSettings.playerScore)
            {
                // set new hight score
                playerSettings.playerScore = statistic.score;

                // save
                playerSettings.SaveSettings();

                // submit score only when not devcode
                if (!playerSettings.playerUsername.Contains("#devmode"))
                {
                    // submit to database
                    StartCoroutine(leaderboardManager.SumbitScore(playerSettings.playerScore));
                }
            }

            #endregion obsolet (moved to playerrestwithdelay)


            // call reset method
            PlayerResetWithDelay();

            // show ads
            adsManager.GetComponent<InterstitialAd>().ShowAd();
        }
    }


    /// <summary>
    /// Use this method to reset player position and update the ui
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    [System.Obsolete] // lootlocker not up to data
    public void PlayerResetWithDelay()
    {
        // get statistic
        RocketStatistic statistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();

        // reset statistic
        statistic.rocketShields = 4;

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
    [System.Obsolete]
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


        // activate gameover screen
        mainScreen.SetActive(true);

        // disable model
        model.SetActive(false);

        // freeze movement
        rigidbody.Sleep();

        // remove simulation
        rigidbody.simulated = false;

        // enable lowpass filter
        audioManager.GetComponent<AudioLowPassFilter>().enabled = true;
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

        // wake up
        rigidbody.WakeUp();

        // eable model
        model.SetActive(true);

        // remove simulation
        rigidbody.simulated = true;

        // disable lowpass filter
        audioManager.GetComponent<AudioLowPassFilter>().enabled = false;
    }
}
