using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        OnExitClicked();
        Tile.IsForcedClosed = false;
        Debug.Log("Clicked");
    }
    
    public void OnBlueClicked()
    {
        TowerChoosen = towerPool[1].GetComponent<Tower>();
        OnExitClicked();
        Tile.IsForcedClosed = false;
        Debug.Log("Clicked");
    }
    
    public void OnGreyClicked()
    {
        TowerChoosen = towerPool[2].GetComponent<Tower>();
        OnExitClicked();
        Tile.IsForcedClosed = false;
        Debug.Log("Clicked");
    }
    
    public void OnOrangeClicked()
    {
        TowerChoosen = towerPool[3].GetComponent<Tower>();
        OnExitClicked();
        Tile.IsForcedClosed = false;
        Debug.Log("Clicked");
    }

    public void OnUpgradeClicked()
    {
        
    }

    public void OnSellClicked()
    {
        
    }

    public void OnExitClicked()
    {
        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;
        gameObject.SetActive(false);
        Debug.Log("Clicked");
    }
}
