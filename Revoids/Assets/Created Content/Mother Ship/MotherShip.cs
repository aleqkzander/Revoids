using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public GameObject clipPoint;
    private bool clipPlayer;
    private GameObject player;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // get player
            player = collision.gameObject;

            // clip player
            clipPlayer = true;
        }
    }


    private void Update()
    {
        if (clipPlayer)
        {
            player.transform.position = clipPoint.transform.position;
        } 
    }


    public IEnumerator UnloadCrewMembers()
    {
        // wait 1 second
        yield return new WaitForSeconds(1);
    }

}
