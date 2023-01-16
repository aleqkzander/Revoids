using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
