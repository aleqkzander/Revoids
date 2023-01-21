using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject playerBullet;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ShootBullet();
        }
    }



    /// <summary>
    /// call this method to shoot a bullet
    /// </summary>
    public void ShootBullet()
    {
        // spawn bullet
        GameObject bullet = Instantiate(playerBullet, shootingPoint.transform.position, Quaternion.identity);

        // call shoot method and pass shooting point
        bullet.GetComponent<Bullet>().ShootBullet(shootingPoint);
    }
}
