using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacySettings : MonoBehaviour
{
    public PlayerSaveFile playerPrefs;
    private List<string> privacyNotice;


    private void Start()
    {
        CheckStatus();
    }


    /// <summary>
    /// check the current status in playerprefs
    /// </summary>
    public void CheckStatus()
    {
        if (playerPrefs.privacyNotice == privacyNotice[1])
        {
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    ///  set state in playerprefs
    /// </summary>
    public void UnderstandNotice() 
    {
        playerPrefs.SetPrivacyNotice(privacyNotice[1]);
        CheckStatus();
    }


    /// <summary>
    ///  use to quit the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
