using UnityEngine;

public class PrivacySettings : MonoBehaviour
{
    public PlayerSettings settings;

    /// <summary>
    /// method to accept privacy notice
    /// </summary>
    public void Button_AcceptAndContinue()
    {
        settings.AcceptPrivacy();
        gameObject.SetActive(false);
    }


    /// <summary>
    /// open link for more information
    /// </summary>
    /// <param name="url"></param>
    public void Button_MoreInformation(string url)
    {
        Application.OpenURL(url);
    }


    /// <summary>
    /// revoke access
    /// </summary>
    public void Button_DeleteAndQuit()
    {
        settings.RevokePrivacy();
    }

}
