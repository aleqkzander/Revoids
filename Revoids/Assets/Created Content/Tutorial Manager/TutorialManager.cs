using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

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
            continueButton.text = "Tap to proceed";
            ShowTutorial1();
            currentCount++;
            return;
        }

        // crew station
        if (currentCount == 2)
        {
            continueButton.text = "Tap to proceed";
            ShowTutorial2();
            currentCount++;
            return;
        }

        // mothership
        if (currentCount == 3)
        {
            if (playerObject.transform.GetChild(1).GetComponent<RocketStatistic>().members != 6) { continueButton.text = "Collect 6 crew members"; return; }
            else
            continueButton.text = "Tap to proceed";
            ShowTutorial3();
            currentCount++;
            return;
        }

        // attack tower
        if (currentCount == 4)
        {
            if (playerObject.transform.GetChild(1).GetComponent<RocketStatistic>().members > 0) { continueButton.text = "Unload your members first"; return; }
            else
            continueButton.text = "Tap to proceed";
            ShowTutorial4();
            currentCount++;
            return;
        }

        // rotting tree
        if (currentCount == 5)
        {
            continueButton.text = "Tap to proceed";
            ShowTutorial5();
            currentCount++;
            return;
        }

        // activate exit
        if (currentCount == 6)
        {
            continueButton.text = "Tap to exit";
            currentCount++;
            return;
        }

        // exit
        if (currentCount == 7)
        {
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }

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

    public void ShowTutorial2()
    {
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

        // reset postition
        playerObject.transform.position = new Vector2(0, 1);
    }

    public void ShowTutorial3()
    {
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

        // reset postition
        playerObject.transform.position = new Vector2(0, 1);

        // spawn mother ship
        Instantiate(motherShipPrefab, new Vector2(playerObject.transform.position.x, 27), Quaternion.identity);
    }

    public void ShowTutorial4()
    {
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

        // reset postition
        playerObject.transform.position = new Vector2(0, 1);

        // spawn attack tower
        Instantiate(shootingTowerPrefab, new Vector2(playerObject.transform.position.x - 10, 5), Quaternion.identity);
    }

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

        // reset postition
        playerObject.transform.position = new Vector2(0, 1);

        // spawn rotting tree
        Instantiate(rottingTreePrefab, new Vector2(playerObject.transform.position.x + 10, 5), Quaternion.identity);
    }

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
}
