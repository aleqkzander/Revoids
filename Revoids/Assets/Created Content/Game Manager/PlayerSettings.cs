using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    #region PREFERENCE KEYS
    [Header("Player Preference Keys")]
    public string prefsAccepted = "playernotice";
    public string prefsScore = "playerscore";
    public string prefsSound = "playersound";
    public string prefsTutorial = "tutorial";
    public string prefsUsername = "playerusername";
    #endregion PREFERENCE KEYS

    #region CURRENT SETTINGS
    [Header("Current Settings")]
    public string playerAccepted;
    public int playerScore = 0;
    public string playerSound;
    public string playerTutorial;
    public string playerUsername;
    #endregion CURRENT SETTINGS

    #region USERNAME UI

    [Header("USERNAME REFERENCES")]
    public GameObject usernameUI;
    public TMP_InputField usernameInput;
    #endregion USERNAME UI

    #region PRIVACY REFERENCE
    [Header("Privacy References")]
    public GameObject privacyManager;
    #endregion PRIVACY REFERENCE

    #region AUDIO REFERENCES
    [Header("Audio References")]
    public AudioSource audioSource;
    public Image audioButton;
    public List<Sprite> audioButtonImages;
    #endregion

    #region LEADERBOARD REFERENCE
    [Header("Leaderboard References")]
    public LeaderboardManager leaderboardManager;
    public GameObject userAlreadyPresenetNotification;
    #endregion LEADERBOARD REFERENCE

    private void Awake()
    {
        privacyManager.SetActive(true);
        usernameUI.SetActive(true);
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


        if (playerSound == string.Empty) playerSound = "enabled";


        if (playerUsername != string.Empty) usernameUI.SetActive(false);


        // play sound or not
        if (playerSound == "enabled")
        {
            audioSource.enabled = true;
            audioButton.sprite = audioButtonImages[0];
        }
        else
        {
            audioSource.enabled = false;
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
        PlayerPrefs.SetString(prefsUsername, playerUsername);
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
        playerUsername = PlayerPrefs.GetString(prefsUsername);
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
        if (!audioSource.enabled)
        {
            audioSource.enabled = true;
            playerSound = "enabled";
            audioButton.sprite = audioButtonImages[0];
            SaveSettings();
        }
        else
        {
            audioSource.enabled = false;
            playerSound = "disabled";
            audioButton.sprite = audioButtonImages[1];
            SaveSettings();
        }
    }


    /// <summary>
    /// set a username
    /// </summary>
    public void SetUsername()
    {
        if (usernameInput.text == string.Empty) return;

        foreach (string entry in leaderboardManager.leaderboardUsers)
        {
            if (usernameInput.text == entry)
            {
                userAlreadyPresenetNotification.SetActive(true);
                Invoke("DisableNotification", 5);
                Debug.Log("Username already used");
                return;
            }
        }


        Debug.Log("Username was set and saved");
        playerUsername = usernameInput.text;
        PlayerPrefs.SetString(prefsUsername, playerUsername);
        SaveSettings();
        usernameUI.SetActive(false);
    }


    /// <summary>
    /// invoke method to disable notification
    /// </summary>
    public void DisableNotification()
    {
        userAlreadyPresenetNotification.SetActive(false);
    }
}
