using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    [Header("Player Preference Keys")]
    public string prefsAccepted = "playernotice";
    public string prefsScore = "playerscore";
    public string prefsSound = "playersound";
    public string prefsTutorial = "tutorial";

    [Header("Current Settings")]
    public string playerAccepted;
    public int playerScore = 0;
    public string playerSound;
    public string playerTutorial;

    [Header("Privacy References")]
    public GameObject privacyManager;

    [Header("Audio References")]
    public AudioLowPassFilter lowPass;
    public AudioHighPassFilter highPass;
    public Image audioButton;
    public List<Sprite> audioButtonImages;

    private void Awake()
    {
        privacyManager.SetActive(true);
    }

    private void Start()
    {
        LoadGameState();
    }


    /// <summary>
    /// load gamestate on start
    /// </summary>
    public void LoadGameState()
    {
        // load current settings
        LoadSettings();

        // show tutorial when not watched
        if (playerTutorial != "completed")
        {
            SceneManager.LoadScene("How To Play Scene", LoadSceneMode.Single);
            return;
        }

        // show privacy or not
        if (playerAccepted == "accepted") privacyManager.SetActive(false);

        // play sound or not
        if (playerSound == "enabled")
        {
            lowPass.enabled = false;
            highPass.enabled = false;
            audioButton.sprite = audioButtonImages[0];
        }
        else
        {
            lowPass.enabled = true;
            highPass.enabled = true;
            audioButton.sprite = audioButtonImages[1];
        }
    }


    /// <summary>
    /// use method for saving
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetString(prefsAccepted, playerAccepted);
        PlayerPrefs.SetInt(prefsScore, playerScore);
        PlayerPrefs.SetString(prefsSound, playerSound);
        PlayerPrefs.SetString(prefsTutorial, playerTutorial);
        PlayerPrefs.Save();
    }


    /// <summary>
    /// use method for loading
    /// </summary>
    public void LoadSettings()
    {
        playerAccepted = PlayerPrefs.GetString(prefsAccepted);
        playerScore = PlayerPrefs.GetInt(prefsScore);
        playerSound = PlayerPrefs.GetString(prefsSound);
        playerTutorial = PlayerPrefs.GetString(prefsTutorial);

    }


    /// <summary>
    /// use method to accept privacy
    /// </summary>
    public void AcceptPrivacy()
    {
        // if the user opts in to targeted advertising:
        MetaData gdprMetaData = new MetaData("gdpr");
        gdprMetaData.Set("consent", "true");
        Advertisement.SetMetaData(gdprMetaData);

        // set and save
        playerAccepted = "accepted";
        SaveSettings();
    }


    /// <summary>
    /// use method to revoke privacy
    /// </summary>
    public void RevokePrivacy()
    {
        // if the user opts out of targeted advertising:
        MetaData gdprMetaData = new MetaData("gdpr");
        gdprMetaData.Set("consent", "false");
        Advertisement.SetMetaData(gdprMetaData);

        // delete data
        PlayerPrefs.DeleteAll();

        // quit
        Application.Quit();
    }


    /// <summary>
    /// use method to activate/deactivate audio
    /// </summary>
    public void SwitchAudio()
    {
        if (lowPass.enabled)
        {
            lowPass.enabled = false;
            highPass.enabled = false;
            playerSound = "enabled";
            audioButton.sprite = audioButtonImages[0];
            SaveSettings();
        }
        else
        {
            lowPass.enabled = true;
            highPass.enabled = true;
            playerSound = "";
            audioButton.sprite = audioButtonImages[1];
            SaveSettings();
        }
    }
}
