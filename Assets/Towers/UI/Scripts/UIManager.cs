using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPool = new GameObject[4];

    public static Tower TowerChoosen = null;
    
    public AudioClip onUpgrade;
    public AudioClip onSell;
    public AudioClip onExit;

    AudioSource audioSource;

    private void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnGreenClicked()
    {
        TowerChoosen = towerPool[0].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked();
    }
    
    public void OnBlueClicked()
    {
        TowerChoosen = towerPool[1].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked();
    }
    
    public void OnGreyClicked()
    {
        TowerChoosen = towerPool[2].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked();
    }
    
    public void OnOrangeClicked()
    {
        TowerChoosen = towerPool[3].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked();
    }

    public void OnUpgradeClicked()
    {
        audioSource.clip = onUpgrade;
        audioSource.Play();

        TowerManagement.IsUpgrading = true;

        OnExitClicked();
    }

    public void OnSellClicked()
    {
        audioSource.clip = onSell;
        audioSource.Play();

        TowerManagement.IsSelling = true;
        Debug.Log("Selling clicked!");

        OnExitClicked();
    }

    public void OnExitClicked()
    {
        audioSource.clip = onExit;
        audioSource.Play();

        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;

        gameObject.SetActive(false);
    }
}
