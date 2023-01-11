using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("BoxCollider2d")]
    public BoxCollider2D colliderWidth;

    [Header("Attack Tower")]
    public GameObject attackTower;
    public int towerAmount;

    [Header("Crew Station")]
    public GameObject crewStation;
    public int crewStationAmount;

    [Header("Tree")]
    public GameObject tree;
    public int treeAmount;


    /// <summary>
    /// Create a random vector2 based on collider2d width
    /// </summary>
    /// <param name="collider2D"></param>
    /// <returns></returns>
    private Vector2 CreateRandomVector(BoxCollider2D collider2D)
    {
        // get width
        float widht = collider2D.size.x;

        // randomize
        float random = Random.Range(-widht / 2, widht / 2);

        // build vector2
        Vector2 position = new Vector2(transform.position.x + random, transform.position.y);

        // return postion
        return position;
    }


    /// <summary>
    /// Spawn attack tower
    /// </summary>
    public void SpawnAttackTower()
    {
        for (int i = 0; i < towerAmount; i++)
        {
            Instantiate(attackTower, CreateRandomVector(colliderWidth), Quaternion.identity);
        }
    }


    /// <summary>
    /// Spawn crew station
    /// </summary>
    public void SpawnCrewStation()
    {
        for (int i = 0; i < crewStationAmount; i++)
        {
            Instantiate(crewStation, CreateRandomVector(colliderWidth), Quaternion.identity);
        }
    }


    /// <summary>
    /// Spawn trees
    /// </summary>
    public void SpawnTree()
    {
        for (int i = 0; i < treeAmount; i++)
        {
            Instantiate(tree, CreateRandomVector(colliderWidth), Quaternion.identity);
        }
    }


    private void Start()
    {
        // spawn towers
        SpawnAttackTower();

        // spawn crew stations
        SpawnCrewStation();

        // spawn tress
        SpawnTree();

        // destroy gameobject
        Destroy(gameObject);
    }
}
