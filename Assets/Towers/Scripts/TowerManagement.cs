using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagement : MonoBehaviour
{
    public EventHandler OnSellTower;
    
    public static bool IsUpgrading = false;
    public static bool IsSelling = false;

    //Here we are going to make sure the player can Upgrade and Sell stuff
    [SerializeField] private Animator stateAnimation;

    [SerializeField] private Tower building;

    private GameObject buildPanel;
    private GameObject upgradePanel;
    private GridManager gridManager;
    
    private GameObject popUps;
    
    AudioPlayerUI audioPlayer;

    private Bank bank;



    private void Awake()
    {
        popUps = GameObject.Find("PopUps");
        gridManager = FindObjectOfType<GridManager>();
        buildPanel = popUps.transform.GetChild(0).gameObject;
        upgradePanel = popUps.transform.GetChild(1).gameObject;

        audioPlayer = FindObjectOfType<AudioPlayerUI>();

        bank = FindObjectOfType<Bank>();
        
        // if (buildPanel == null)
        // {
        //     print("Well that is akward...");
        // }
        // buildPanel.gameObject.SetActive(false);
        // upgradePanel.gameObject.SetActive(false);

    }
    
    
    

    private void OnMouseDown()
    {
        if (!Tile.IsBuildOpen && !Tile.IsUpgradeOpen)
        {
            upgradePanel.SetActive(true);

            audioPlayer.PlayOpenSound();

            Debug.Log("Getting that upgrade panel ready");
            Tile.IsUpgradeOpen = true;
            StartCoroutine(WaitForUIResponse());
        }
    }

    

    private void SellingTower()
    {

        (int a, int b) = building.Data.Sell();
        bank.Deposit(a,b);
        gridManager.UnlockNode(gridManager.GetCoordinatesFromPosition(transform.position));
        upgradePanel.SetActive(false);
        
        //Find A way for the script to make the Tile go back to isPlaceable = true
        if (OnSellTower != null)
        {
            OnSellTower(this, EventArgs.Empty);
            audioPlayer.PlaySellSound();
        }
        
        Destroy(gameObject, 0.1f);
    }

    

    private void UpgradingTower()
    {
        if (building.Data.Level != 2)
        {
            audioPlayer.PlayUgradeSound();
        }
        
        (int a, int b) = building.Data.Upgrade((bank.GetCurrentCrystalBalance, bank.GetCurrentResourceBalance));
        if ((a, b) != (0, 0))
        {
            bank.Withdraw(a,b);
        }
        else
        {
            //Do stuff to tell the player they don't have the money
        }

        stateAnimation.SetInteger("Level", building.Data.Level);
    }

    IEnumerator WaitForUIResponse()
    {
        while (Tile.IsUpgradeOpen)
        {
            yield return new WaitForEndOfFrame();
        }
        
        if (IsSelling)
        {
            //Just to be sure because we destroy the GO and this is not in Update
            IsSelling = false;
            IsUpgrading = false;
            SellingTower();

        }
        else if (IsUpgrading)
        {
            UpgradingTower();
        }

        IsSelling = false;
        IsUpgrading = false;
    }
}
