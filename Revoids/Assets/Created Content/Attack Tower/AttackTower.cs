using UnityEngine;

public class AttackTower : MonoBehaviour
{
    public GameObject towerBullet;
    public GameObject shootingPoint;
    private float attackRadius = 7.0f;


    private void Start()
    {
        // invoke attack method every 2 seconds
        InvokeRepeating("CheckForPlayerAndShoot", 0.25f, 2f);
    }


    /// <summary>
    /// Search player and shoot
    /// </summary>
    private void CheckForPlayerAndShoot()
    {
        // check for player
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRadius, LayerMask.GetMask("Player"));

        // return when not hit detected
        if (player == null) return;

        // spawn bullet
        GameObject bullet = Instantiate(towerBullet, shootingPoint.transform.position, Quaternion.identity);

        // fire bullet
        bullet.GetComponent<Bullet>().AttackPlayer(player.gameObject, shootingPoint);
    }


    private void OnDrawGizmosSelected()
    {
        // Zeige den Radius vom Ground Check bei Auswahl des Crewmembers
        Gizmos.color = new Color(1, 1, 0, 0.75f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
