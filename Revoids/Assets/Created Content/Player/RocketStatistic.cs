using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketStatistic : MonoBehaviour
{
    [Header("Mothership")]
    public GameObject motherShip;

    [Header("UI Score")]
    public TMP_Text scoreText;

    [Header("UI Shield")]
    public GameObject shieldHolder;
    public GameObject shieldDisplay;
    // 3 shield default
    public int rocketShields = 3;

    [Header("UI Crew")]
    public GameObject crewHolder;
    public GameObject crewDisplay;
    // 6 members required for mother ship
    public int members = 0;

    [Header("Statistic")]
    public int score;


    private void Start()
    {
        // update on first frame
        UpdateUI();
    }


    /// <summary>
    /// Update the ui
    /// </summary>
    public void UpdateUI()
    {
        #region SCORE
        // set score
        scoreText.text = "SCORE: " + score.ToString("0000000");
        #endregion

        #region SHIELDS
        // detach all lives
        shieldHolder.transform.DetachChildren();

        // get them after that
        GameObject[] oldShields = GameObject.FindGameObjectsWithTag("ShieldDisplay");

        // destrory them
        foreach (GameObject shield in oldShields) Destroy(shield);

        // set actual lives
        for (int i = 0; i < rocketShields; i++)
        {
            GameObject _gameobject = Instantiate(shieldDisplay, Vector2.zero, Quaternion.identity);
            _gameobject.transform.SetParent(shieldHolder.transform);
        }

        #endregion

        #region CREW

        // detach all lives
        crewHolder.transform.DetachChildren();

        // get them after that
        GameObject[] oldMembers = GameObject.FindGameObjectsWithTag("CrewDisplay");

        // destrory them
        foreach (GameObject member in oldMembers) Destroy(member);

        // set actual crew
        for (int i = 0; i < members; i++)
        {
            GameObject _gameobject = Instantiate(crewDisplay, Vector2.zero, Quaternion.identity);
            _gameobject.transform.SetParent(crewHolder.transform);
        }

        #endregion
    }


    /// <summary>
    /// use this method to add the member
    /// </summary>
    /// <param name="scorevalue"></param>
    /// <param name="pickupedCrew"></param>
    public void PickupCrewMember(int scorevalue, GameObject pickupedCrew)
    {
        // increment members
        members++;

        // update ui
        UpdateUI();

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
