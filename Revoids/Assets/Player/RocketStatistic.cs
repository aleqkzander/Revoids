using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketStatistic : MonoBehaviour
{
    [Header("UI References")]
    public GameObject crewDisplay;

    [Header("Statistic")]
    public int score;
    public int members;
    

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
        GameObject display = Instantiate(crewDisplay);

        // add to crew holder as child
        display.transform.parent = uicrewholder.transform;

        // destroy the crew member
        Destroy(pickupedCrew);
    }
}
