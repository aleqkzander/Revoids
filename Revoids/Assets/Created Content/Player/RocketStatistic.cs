using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketStatistic : MonoBehaviour
{
    [Header("Mothership")]
    public GameObject motherShip;

    [Header("UI References")]
    public GameObject crewDisplay;

    [Header("Statistic")]
    public int score;
    // 6 members required for mother ship
    public int members = 0;
    // 4 lives is default;
    public int lives = 4;
    // 2 shield default
    public int rocketShields = 2;

    

    /// <summary>
    /// use this method to add the member
    /// </summary>
    /// <param name="scorevalue"></param>
    /// <param name="pickupedCrew"></param>
    public void PickupCrewMember(int scorevalue, GameObject pickupedCrew)
    {
        // set the score on script
        score += scorevalue;

        // get ui scoretext component from scene
        GameObject uiscoretext = GameObject.Find("UI_Score");

        // set text to equal to player score
        uiscoretext.GetComponent<TMP_Text>().text = "SCORE " + score.ToString("0000000000");

        // get ui crewholder component from scene
        GameObject uicrewholder = GameObject.Find("UI_CrewHolder");

        // spawn uicrewmember
        GameObject display = Instantiate(crewDisplay, Vector2.zero, Quaternion.identity);

        // add to crew holder as child
        display.transform.SetParent(uicrewholder.transform);

        // increment members
        members++;

        // spawn mothership when membercount is 6
        if (members == 6)
        {
            // spawn on players x
            Instantiate(motherShip, new Vector2(gameObject.transform.position.x, 27), Quaternion.identity);
        }

        // destroy the crew member
        Destroy(pickupedCrew);
    }


}
