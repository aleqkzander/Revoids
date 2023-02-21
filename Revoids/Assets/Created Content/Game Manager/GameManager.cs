using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject interfaceEnable;
    public TMP_Text versionText;


    private void Awake()
    {
        // enable interface when not enabled
        interfaceEnable.SetActive(true);
    }

    private void Start()
    {
        // Make the game run as fast as possible
        // Application.targetFrameRate = 300;
        if (Application.isMobilePlatform) Application.targetFrameRate = 45;

        versionText.text = "VERSION: " + Application.version;
    }


    /// <summary>
    /// exit game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }



    /// <summary>
    ///  show tutorial
    /// </summary>
    public void LoadTutorial()
    {
        SceneManager.LoadScene("How To Play Scene", LoadSceneMode.Single);
    }
}
