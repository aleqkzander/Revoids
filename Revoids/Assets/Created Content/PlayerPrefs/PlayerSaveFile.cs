using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrefsKey
{
    privacynotice,
    playerscore
}

public class PlayerSaveFile : MonoBehaviour
{
    public string privacyNotice = "Default";
    public int playerScore = 0;

    private void Start()
    {
        privacyNotice = PlayerPrefs.GetString(PrefsKey.privacynotice.ToString());
        playerScore = PlayerPrefs.GetInt(PrefsKey.playerscore.ToString());

        if (privacyNotice == string.Empty) privacyNotice = "not understood";
    }


    /// <summary>
    /// set privacynotice and save
    /// </summary>
    /// <param name="privacystate"></param>
    public void SetPrivacyNotice(string privacystate)
    {
        PlayerPrefs.SetString(PrefsKey.privacynotice.ToString(), privacystate);
        PlayerPrefs.Save();
    }


    /// <summary>
    /// set score and save
    /// </summary>
    /// <param name="score"></param>
    public void SetPlayerScore(int score)
    {
        PlayerPrefs.SetInt(PrefsKey.playerscore.ToString(), score);
        PlayerPrefs.Save();
    }
}
