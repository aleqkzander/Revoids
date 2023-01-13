using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionSound;
    private string shootFlag = string.Empty;

    private void Start()
    {
        // destroy bullet in 2 seconds
        Invoke("DestroyBullet", 2);
    }


    /// <summary>
    /// shoot the bullet in forward direction
    /// </summary>
    /// <param name="shootingPoint"></param>
    public void ShootBullet(Transform shootingPoint)
    {
        // set flag
        shootFlag = "player";

        // fire bullet
        GetComponent<Rigidbody2D>().AddRelativeForce(shootingPoint.transform.up * 20, ForceMode2D.Impulse);
    }


    /// <summary>
    /// use this method to attack player
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <param name="shootingPoint"></param>
    public void AttackPlayer(GameObject playerPosition, GameObject shootingPoint)
    {
        // set flag
        shootFlag = "tower";

        // calculate direction
        Vector2 direction = playerPosition.transform.position - shootingPoint.transform.position;

        // shoot bullet
        GetComponent<Rigidbody2D>().AddRelativeForce(direction * 3.5f, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when player gets hit by tower
        if (collision.gameObject.CompareTag("Player") && shootFlag == "tower")
        {
            // get rocket statistic from collision
            RocketStatistic statistic = collision.gameObject.transform.GetChild(1).GetComponent<RocketStatistic>();

            // check rocket shields
            if (statistic.rocketShields > 0) 
            {
                // subliment shield
                statistic.rocketShields--;

                // manageui
                statistic.UpdateUI();

                // destroy bullet
                Destroy(gameObject);
            }
            else if (statistic.rocketShields == 0)
            {
                // if shield==0 then reset player
                StartCoroutine(collision.gameObject.transform.GetChild(3).GetComponent<RocketCollisionDetection>().PlayerResetWithDelay(2));

                // destroy bullet
                Destroy(gameObject);
            }
        }


        // when crew station gets hit by player
        if (collision.gameObject.CompareTag("CrewStation") && shootFlag == "player")
        {
            // call method from crew station 
            collision.gameObject.GetComponent<CrewStation>().SpawnCrewMembers();

            // destroy the crew station
            Destroy(collision.gameObject);

            // destroy bullet
            Destroy(gameObject);
        }

        
        // if attack tower gets hit by player
        if (collision.gameObject.CompareTag("AttackTower") && shootFlag == "player")
        {
            Destroy(collision.gameObject);

            // destroy bullet
            Destroy(gameObject);
        }


        // if tree gets hit
        if (collision.gameObject.CompareTag("Tree"))
        {
            // if player is shooting add score
            if (shootFlag == "player")
            {
                // get rocket statistic from collision
                RocketStatistic statistic = GameObject.FindGameObjectWithTag("Player").gameObject.transform.GetChild(1).GetComponent<RocketStatistic>();

                // add score
                statistic.score += 2500;

                // update ui
                statistic.UpdateUI();
            }

            // destroy tree
            Destroy(collision.gameObject);

            // destroy bullet
            Destroy(gameObject);
        }


        // play explosion sound
        Instantiate(explosionSound, transform.position, Quaternion.identity);


        // Destroy bullet
        Destroy(gameObject);
    }


    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
