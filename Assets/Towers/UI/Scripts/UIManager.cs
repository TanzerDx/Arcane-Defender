using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPool = new GameObject[4];

    public static Tower TowerChoosen = null;
    
    AudioPlayerUI audioPlayer;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayerUI>();
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;
    }


    public void OnGreenClicked()
    {
        TowerChoosen = towerPool[0].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked(Tile.IsForcedClosed);
    }
    
    public void OnBlueClicked()
    {
        TowerChoosen = towerPool[1].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked(Tile.IsForcedClosed);
    }
    
    public void OnGreyClicked()
    {
        TowerChoosen = towerPool[2].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked(Tile.IsForcedClosed);
    }
    
    public void OnOrangeClicked()
    {
        TowerChoosen = towerPool[3].GetComponent<Tower>();
        Tile.IsForcedClosed = false;
        OnExitClicked(Tile.IsForcedClosed);
    }

    public void OnUpgradeClicked()
    {
        TowerManagement.IsUpgrading = true;
        OnExitClicked(false);
    }

    public void OnSellClicked()
    {

        TowerManagement.IsSelling = true;
        Debug.Log("Selling clicked!");

        OnExitClicked(false);
    }

    public void OnExitClicked(bool IsForcedClosed)
    {
        if(IsForcedClosed)
        {
            audioPlayer.PlayCloseSound();
        }
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;

        gameObject.SetActive(false);
    }

    public void OnResetGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;
    }

    public void OnToMainMenuClicked()
    {
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;
        SceneManager.LoadScene("Main Menu");
    }
}
