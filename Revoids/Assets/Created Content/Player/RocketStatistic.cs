using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketStatistic : MonoBehaviour
{
    [Header("Mothership")]
    public GameObject motherShip;

    [Header("UI Score")]
    public TMP_Text scoreText;

    [Header("UI Shield")]
    public GameObject shieldHolder; // only needed for rotation
    public List<GameObject> rocketShieldsDisplay;
    public int rocketShields = 4;

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


    private void Update()
    {
        shieldHolder.transform.position = gameObject.transform.position;
    }


    /// <summary>
    /// Update the ui and rocketshield display
    /// </summary>
    public void UpdateUI()
    {
        #region SCORE
        // set score
        scoreText.text = "SCORE: " + score.ToString("0000000");
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
            _gameobject.transform.localScale = Vector3.one;
        }

        #endregion

        #region SHIELDS
        // deactivate every shield display
        foreach (GameObject shield in rocketShieldsDisplay)
        {
            shield.SetActive(false);
        }

        // activate the current amount of shields
        for (int i = 0; i < rocketShields; i++)
        {
            rocketShieldsDisplay[i].gameObject.SetActive(true);
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

        // add score value
        score += scorevalue;

        // update ui
        UpdateUI();

        // spawn mothership when membercount is 6
        if (members == 6)
        {
            if (SceneManager.GetActiveScene().name != "How To Play Scene")
                // spawn on players x
                Instantiate(motherShip, new Vector2(gameObject.transform.position.x, 27), Quaternion.identity);
        }

        // destroy the crew member
        Destroy(pickupedCrew);
    }
}
