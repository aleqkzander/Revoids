using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    public GameObject explosionSound;
    private string shootFlag = string.Empty;

    [Header("Light")]
    public Light2D light2D;


    /// <summary>
    /// Destroy when bullet no longer visible by the camera
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
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
        GetComponent<Rigidbody2D>().AddRelativeForce(shootingPoint.transform.up * 30f, ForceMode2D.Impulse);
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
        GetComponent<Rigidbody2D>().AddRelativeForce(direction * 4.5f, ForceMode2D.Impulse);
    }


    /// <summary>
    /// Call method on player hit
    /// </summary>
    /// <param name="collision"></param>
    [System.Obsolete]
    public void PlayerHit(Collision2D collision)
    {
        collision.gameObject.transform.GetChild(3).GetComponent<RocketCollisionDetection>().PlayerHit();
    }


    /// <summary>
    /// Call method on crewstation hit
    /// </summary>
    /// <param name="collision"></param>
    public void CrewStationHit(Collision2D collision)
    {
        // if player is shooting add score
        if (shootFlag == "player")
        {
            // get rocket statistic from collision
            RocketStatistic statistic = GameObject.FindGameObjectWithTag("Player").gameObject.transform.GetChild(1).GetComponent<RocketStatistic>();

            // add score
            statistic.score += 500;

            // update ui
            statistic.UpdateUI();
        }

        // call method from crew station 
        collision.gameObject.GetComponent<CrewStation>().SpawnCrewMembers();

        // destroy the crew station
        Destroy(collision.gameObject);
    }


    /// <summary>
    /// Call methon on tower hit
    /// </summary>
    /// <param name="collision"></param>
    public void TowerHit(Collision2D collision)
    {
        // if player is shooting add score
        if (shootFlag == "player")
        {
            // get rocket statistic from collision
            RocketStatistic statistic = GameObject.FindGameObjectWithTag("Player").gameObject.transform.GetChild(1).GetComponent<RocketStatistic>();

            // add score
            statistic.score += 5000;

            // update ui
            statistic.UpdateUI();
        }

        // destor the tower
        Destroy(collision.gameObject);
    }


    /// <summary>
    /// Call this method on tree hit
    /// </summary>
    /// <param name="collision"></param>
    public void TreeHit(Collision2D collision)
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
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // when player gets hit by tower
        if (collision.gameObject.CompareTag("Player") && shootFlag == "tower")
        {
            PlayerHit(collision);
        }


        // when crew station gets hit by player
        if (collision.gameObject.CompareTag("CrewStation") && shootFlag == "player")
        {
            CrewStationHit(collision);
        }


        // if attack tower gets hit by player
        if (collision.gameObject.CompareTag("AttackTower") && shootFlag == "player")
        {
            TowerHit(collision);
        }


        // if tree gets hit
        if (collision.gameObject.CompareTag("Tree"))
        {
            TreeHit(collision);
        }


        // explosion sound
        Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);

        // Destroy bullet
        Destroy(gameObject);
    }
}
