using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public GameObject spawner;
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

            // start unloading crew members every 1 second
            InvokeRepeating("UnloadCrewMembers", 0.25f, 1f);
        }
    }


    private void Update()
    {
        // when player null return
        if (player == null) return;

        // clip player to ship
        if (clipPlayer) player.transform.position = clipPoint.transform.position;
    }


    /// <summary>
    /// Use this method to call abroadship to unlead crewmembers
    /// </summary>
    private void UnloadCrewMembers()
    {
        // get statstic from player
        RocketStatistic rocketStatistic = player.transform.GetChild(1).GetComponent<RocketStatistic>();

        // if no crew members
        if (rocketStatistic.members == 0) { StartCoroutine(AbroadShip()); }

        // get all uicrewmembers
        GameObject[] uimembers = GameObject.FindGameObjectsWithTag("CrewDisplay");

        // loop through array
        foreach (GameObject member in uimembers)
        {
            // remove member on rocket
            rocketStatistic.members--;

            // remove member from ui
            Destroy(member);

            // break method
            break;
        }
    }


    /// <summary>
    /// Getting called by unleadcrewmembers
    /// </summary>
    /// <returns></returns>
    private IEnumerator AbroadShip()
    {
        clipPlayer = false;

        // get animation
        Animation animation = gameObject.transform.GetChild(0).GetComponent<Animation>();

        // get animation clip
        AnimationClip abroad = animation.GetClip("MotherShipAbroad");

        // set current clip
        animation.clip = abroad;

        // get lenght
        float animationLenght = abroad.length;

        // play animation
        animation.Play();

        // wait for animation lenght
        yield return new WaitForSecondsRealtime(animationLenght);

        // instantiate spawner
        GameObject spawnerObject = Instantiate(spawner, new Vector2(0, 13), Quaternion.identity);

        // get script
        Spawner spawnerScript = spawnerObject.GetComponent<Spawner>();

        // add one attacktower every time
        spawnerScript.towerAmount++;

        // destroy mother ship
        Destroy(gameObject);
    }
}
