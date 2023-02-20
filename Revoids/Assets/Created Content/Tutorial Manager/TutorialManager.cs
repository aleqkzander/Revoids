using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject playerUI;
    public GameObject playerObject;

    [Header("Prefabs")]
    public GameObject motherShipPrefab;
    public GameObject rottingTreePrefab;
    public GameObject crewStationPrefab;
    public GameObject shootingTowerPrefab;

    [Header("UI Objects")]
    public TMP_Text continueButton;
    public GameObject tutorialHolder;

    [Header("Tutorial Objects")]
    public int currentCount = 0;
    public List<GameObject> tutorialObject;

    public void ContinueTutorial()
    {
        // get tutorial and destroy
        GameObject tutorialObject = GameObject.FindGameObjectWithTag("TutorialText");

        if (tutorialObject != null)
        {
            continueButton.text = "Tap again to continue";
            Destroy(tutorialObject);
            return;
        }

        else

        // welcome
        if (currentCount == 0)
        {
            continueButton.text = "Tap to proceed";
            ShowTutorial0();
            currentCount++;
            return;
        }

        // player control
        if (currentCount == 1)
        {
            continueButton.text = "Give it a try";
            ShowTutorial1();
            currentCount++;
            return;
        }

        // crew station
        if (currentCount == 2)
        {
            continueButton.text = "Give it a try";
            ShowTutorial2();
            currentCount++;
            return;
        }

        // mothership
        if (currentCount == 3)
        {
            if (playerObject.transform.GetChild(1).GetComponent<RocketStatistic>().members != 6) { continueButton.text = "Collect 6 crew members"; return; }
            else
                continueButton.text = "Give it a try";
            ShowTutorial3();
            currentCount++;
            return;
        }

        // attack tower
        if (currentCount == 4)
        {
            if (playerObject.transform.GetChild(1).GetComponent<RocketStatistic>().members > 0) { continueButton.text = "Unload your members first"; return; }
            else
                continueButton.text = "Give it a try";
            ShowTutorial4();
            currentCount++;
            return;
        }

        // rotting tree
        if (currentCount == 5)
        {
            continueButton.text = "Give it a try";
            ShowTutorial5();
            currentCount++;
            return;
        }

        // activate free play
        if (currentCount == 6)
        {
            continueButton.text = "Tap for freeplay";
            ShowTutorial6();
            currentCount++;
            return;
        }

        // activate exit
        if (currentCount == 7)
        {
            continueButton.text = "Tap to exit";
            currentCount++;
            return;
        }

        // exit
        if (currentCount == 8)
        {
            PlayerPrefs.SetString("tutorial", "completed");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }



    /// <summary>
    /// welcome text
    /// </summary>
    public void ShowTutorial0()
    {
        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[0], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();
    }



    /// <summary>
    /// player and ui
    /// </summary>
    public void ShowTutorial1()
    {
        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[1], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();

        // activate player and hud
        playerObject.SetActive(true);
        playerUI.SetActive(true);
    }



    /// <summary>
    /// crew station
    /// </summary>
    public void ShowTutorial2()
    {
        // reset postition
        ResetPlayerRotation(playerObject);

        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[2], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();

        // spawn crew station
        Instantiate(crewStationPrefab, new Vector2(playerObject.transform.position.x + 10, 5), Quaternion.identity);

        // spawn crew station
        Instantiate(crewStationPrefab, new Vector2(playerObject.transform.position.x - 10, 5), Quaternion.identity);
    }



    /// <summary>
    /// mothership
    /// </summary>
    public void ShowTutorial3()
    {
        // reset postition
        ResetPlayerRotation(playerObject);

        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[3], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();

        // spawn mother ship
        Instantiate(motherShipPrefab, new Vector2(playerObject.transform.position.x, 27), Quaternion.identity);
    }



    /// <summary>
    /// shooting tower
    /// </summary>
    public void ShowTutorial4()
    {
        // reset postition
        ResetPlayerRotation(playerObject);

        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[4], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();

        // spawn attack tower
        Instantiate(shootingTowerPrefab, new Vector2(playerObject.transform.position.x - 10, 5), Quaternion.identity);
    }



    /// <summary>
    /// rotting tree
    /// </summary>
    public void ShowTutorial5()
    {
        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[5], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();

        // spawn rotting tree
        Instantiate(rottingTreePrefab, new Vector2(playerObject.transform.position.x + 10, 5), Quaternion.identity);
    }



    /// <summary>
    /// exit
    /// </summary>
    public void ShowTutorial6()
    {
        // spawn tutorial object and get tutoiral class
        TextPrinter tutorial = Instantiate(tutorialObject[6], Vector2.zero, Quaternion.identity).GetComponent<TextPrinter>();

        // add to hud
        tutorial.gameObject.transform.SetParent(tutorialHolder.transform);

        // set size
        tutorial.gameObject.transform.localScale = Vector2.one;

        // set location
        tutorial.gameObject.transform.localPosition = Vector2.zero;

        // print tutorial text
        tutorial.StartTutorial();
    }


    public void ResetPlayerRotation(GameObject player)
    {
        // get rigidbody
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

        // reset gloabl velocity
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;

        // reset rotation
        player.transform.rotation = Quaternion.identity;

        player.transform.position = new Vector2(0, -2);
    }


    public void SkipTutorial()
    {
        PlayerPrefs.SetString("tutorial", "completed");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
}
