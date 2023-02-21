using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text versionText;

    private void Start()
    {
        // Make the game run as fast as possible
        // Application.targetFrameRate = 300;

        versionText.text = "VERSION: " + Application.version;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("How To Play Scene", LoadSceneMode.Single);
    }

}
