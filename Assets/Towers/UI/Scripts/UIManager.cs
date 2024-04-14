using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPool = new GameObject[4];

    public static Tower TowerChoosen = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        TowerManagement.IsUpgrading = true;
        OnExitClicked();
    }

    public void OnSellClicked()
    {
        TowerManagement.IsSelling = true;
        Debug.Log("Selling clicked!");
        OnExitClicked();
    }

    public void OnExitClicked()
    {
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;
        gameObject.SetActive(false);
    }
}
