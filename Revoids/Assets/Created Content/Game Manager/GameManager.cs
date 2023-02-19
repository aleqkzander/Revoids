using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // Make the game run as fast as possible
        // Application.targetFrameRate = 300;
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
