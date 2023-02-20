using System.Collections;
using UnityEngine;
using TMPro;
using LootLocker.Requests;
using System;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public PlayerSettings playerSettings;
    public GameObject leaderboardButton;
    public GameObject leaderboardEntry;
    public GameObject leaderboardHolder;
    public List<string> leaderboardUsers;

    int leaderboardID = 11904;
    string leaderboardKey = "revoid_leaderboard_key";

    [Obsolete]
    private void Start()
    {
        StartCoroutine(LoginRoutine());
    }


    /// <summary>
    /// use coroutine for logging in
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public IEnumerator LoginRoutine()
    {
        LootLocker.LootLockerResponse loginresponse = new LootLocker.LootLockerResponse();

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            loginresponse = response;

            if (!loginresponse.success)
            {
                Debug.Log("Error starting LootLocker session");
                return;
            }

            // activate leaderboard button
            if (leaderboardButton.gameObject != null) leaderboardButton.SetActive(true);

            Debug.Log("Successfully started LootLocker session");

            if (this == null) return;

            // Fetch highscores
            StartCoroutine(FetchHighScores());
        });

        // wait for done to become true
        yield return new WaitWhile(() => loginresponse.success == false);
    }


    /// <summary>
    /// use coroutine to submit a score
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    [Obsolete]
    public IEnumerator SumbitScore(int score)
    {
        LootLocker.LootLockerResponse submitresponse = new LootLocker.LootLockerResponse();

        LootLockerSDKManager.SubmitScore(playerSettings.playerUsername, score, leaderboardID, (response) =>
        {
            submitresponse = response;

            if (submitresponse.statusCode == 200)
            {
                Debug.Log("Successful");
                StartCoroutine(FetchHighScores());
            }
            else
            {
                Debug.Log("failed: " + submitresponse.Error);
            }
        });

        yield return new WaitWhile(() => submitresponse.success == false);
    }


    /// <summary>
    /// get the highscore (call coroutine on loginroutine and submitscore success)
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    public IEnumerator FetchHighScores()
    {
        int playerTop = 10; // Amount of top players to be shown
        LootLocker.LootLockerResponse fetchresponse = new LootLocker.LootLockerResponse();

        // clear the old list by destroying the gameobject information holder
        for (int i = 0; i < leaderboardHolder.transform.childCount; i++)
        {
            Destroy(leaderboardHolder.transform.GetChild(i).gameObject);
        }

        // clear the userlist
        leaderboardUsers.Clear();

        // now fetch again
        LootLockerSDKManager.GetScoreList(leaderboardKey, playerTop, 0, (response) =>
        {
            fetchresponse = response;

            if (fetchresponse.statusCode == 200)
            {
                LootLockerLeaderboardMember[] member = response.items;

                for (int i = 0; i < member.Length; i++)
                {
                    if (member[i] != null)
                    {
                        GameObject entry = Instantiate(leaderboardEntry);
                        TMP_Text entryText = entry.GetComponent<TMP_Text>();
                        entryText.text = string.Empty;

                        entryText.text += member[i].rank + ". ";
                        entryText.text += member[i].member_id + "    ";
                        leaderboardUsers.Add(member[i].member_id);
                        entryText.text += member[i].score;

                        entry.transform.SetParent(leaderboardHolder.transform);
                        entry.transform.localScale = Vector3.one;
                    }
                }

                Debug.Log("Fetched data successful");
            }
            else
            {
                Debug.Log("Fetched data failed");
            }
        });

        yield return new WaitWhile(() => fetchresponse.success == false);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

