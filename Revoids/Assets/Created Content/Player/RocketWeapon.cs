using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWeapon : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletObject;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletObject, shootingPoint.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ShootBullet(shootingPoint);
        }
    }
}
