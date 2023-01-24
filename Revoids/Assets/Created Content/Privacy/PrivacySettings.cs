using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public enum PrivacyState
{
    accpeted,
    notaccepted
}


public class PrivacySettings : MonoBehaviour
{
    public PlayerSaveFile playerPrefs;

    private void Start()
    {
        CheckStatus();
    }


    /// <summary>
    /// check the current status in playerprefs
    /// </summary>
    public void CheckStatus()
    {
        if (playerPrefs.privacyNotice == PrivacyState.accpeted.ToString())
        {
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    ///  set state in playerprefs
    /// </summary>
    public void Accept() 
    {
        // if the user opts in to targeted advertising:
        MetaData gdprMetaData = new MetaData("gdpr");
        gdprMetaData.Set("consent", "true");
        Advertisement.SetMetaData(gdprMetaData);

        // save internally
        playerPrefs.SetPrivacyNotice(PrivacyState.accpeted.ToString());
        playerPrefs.privacyNotice = PrivacyState.accpeted.ToString();
        CheckStatus();
    }


    /// <summary>
    /// open link for more information
    /// </summary>
    /// <param name="link"></param>
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }


    /// <summary>
    ///  use to opt-out and quit the game
    /// </summary>
    public void OptOut()
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
}
