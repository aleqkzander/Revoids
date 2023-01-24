using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public PlayerSaveFile playerPrefs;
    public AudioLowPassFilter lowPass;
    public AudioHighPassFilter highPass;
    public List<Sprite> stateImage;
    public Image audioButton;
    public string state;


    private void Start()
    {
        CheckState();
    }


    /// <summary>
    /// use to check the state and set the audio accordingly
    /// </summary>
    public void CheckState()
    {
        if (playerPrefs.audioState == "audio=true") 
        {
            lowPass.enabled = false;
            highPass.enabled = false;
            audioButton.sprite = stateImage[0];
        }
        if (playerPrefs.audioState == "audio=false")
        {
            lowPass.enabled = true;
            highPass.enabled = true;
            audioButton.sprite = stateImage[1];
        }

        state = playerPrefs.audioState;
    }


    /// <summary>
    /// change the state on playerprefs 
    /// </summary>
    public void ChangeState()
    {
        playerPrefs.ChangeAudioState(state);
        CheckState();
    }

}
