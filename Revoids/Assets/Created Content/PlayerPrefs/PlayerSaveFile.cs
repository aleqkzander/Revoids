using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SaveFile
{
    [Header("Player Preferences Keys")]
    public string privacyNotice = "privacynotice";
    public string playerScore = "playerscore";
    public string playerAudio = "playeraudio";
}

public class PlayerSaveFile : MonoBehaviour
{
    public SaveFile saveFile = new SaveFile();
    public string privacyNotice = "";
    public int playerScore = 0;
    public string audioState = "audio=true";

    private void Start()
    {
        privacyNotice = PlayerPrefs.GetString(saveFile.privacyNotice);
        playerScore = PlayerPrefs.GetInt(saveFile.playerScore);
        audioState = PlayerPrefs.GetString(saveFile.playerAudio);
    }


    /// <summary>
    /// set privacynotice and save
    /// </summary>
    /// <param name="privacystate"></param>
    public void SetPrivacyNotice(string privacystate)
    {
        PlayerPrefs.SetString(saveFile.privacyNotice, privacystate);
        PlayerPrefs.Save();
    }


    /// <summary>
    /// set score and save
    /// </summary>
    /// <param name="score"></param>
    public void SetPlayerScore(int score)
    {
        PlayerPrefs.SetInt(saveFile.playerScore, score);
        PlayerPrefs.Save();
    }


    /// <summary>
    /// use method to activate/deactive audio
    /// </summary>
    public void ChangeAudioState(string state)
    {
        switch (state)
        {
            case "audio=true":
                PlayerPrefs.SetString(saveFile.playerAudio, "audio=false");
                audioState = PlayerPrefs.GetString(saveFile.playerAudio);
                PlayerPrefs.Save();
                break;

            case "":
                PlayerPrefs.SetString(saveFile.playerAudio, "audio=true");
                audioState = PlayerPrefs.GetString(saveFile.playerAudio);
                PlayerPrefs.Save();
                break;
        }
    }
}
