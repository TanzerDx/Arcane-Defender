using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerUI : MonoBehaviour
{
    AudioSource audioSource;

public AudioSource GetAudioSource
{get {return audioSource;}}

    public AudioClip openPanel;
    public AudioClip upgradeTower;
    public AudioClip sellTower;
    public AudioClip closeWindow;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSound()
    {
        audioSource.clip = openPanel;
        audioSource.Play();
    }

    public void PlayUgradeSound()
    {
        audioSource.clip = upgradeTower;
        audioSource.Play();
    }

    public void PlaySellSound()
    {
        audioSource.clip = sellTower;
        audioSource.Play();
    }

    public void PlayCloseSound()
    {
        audioSource.clip = closeWindow;
        audioSource.Play();
    }
}
