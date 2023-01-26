using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    private void Start()
    {
        StartCoroutine(PlayMusicLoop());
    }

    public IEnumerator PlayMusicLoop()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        yield return new WaitForSeconds(audioClips[0].length);
        audioSource.clip = audioClips[1];
        audioSource.Play();
        yield return new WaitForSeconds(audioClips[1].length);
        StartCoroutine(PlayMusicLoop());
    }

}
