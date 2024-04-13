using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
   
    public AudioClip[] backgroundSounds;

    public AudioSource audioSource;

    int soundNumber;
    float soundPitch;


    void Update()
{
    if (!audioSource.isPlaying)
    {
        soundNumber = Random.Range(0, backgroundSounds.Length);
        soundPitch = Random.Range(1f, 1.5f);
        PlaySound();
    }
}

    void PlaySound()
    {
        audioSource.clip = backgroundSounds[soundNumber];
        audioSource.pitch = soundPitch;
        audioSource.Play();
    }

}
