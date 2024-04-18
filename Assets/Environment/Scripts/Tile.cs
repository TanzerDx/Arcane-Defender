using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System;

public class Tile : MonoBehaviour
{
    public static bool IsBuildOpen = false;
    public static bool IsUpgradeOpen = false;
    public static bool IsForcedClosed = true;
    
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject playerPrefab;

    Vector3 currentPlayerPosition;
    [SerializeField] float placeDistance = 1f;

    [SerializeField] bool isPlaceable;

    public bool IsPlaceableValue 
    { 
        get {return isPlaceable;}
    }

    [SerializeField] private GameObject popUps;
    
    [SerializeField] private SpriteRenderer sprite;

    private GameObject buildPanel;
    private GameObject upgradePanel;
    private GameObject towerOnThisTile;

    AudioSource audioSourceBuild;
    AudioSource audioSourceUpgrade;

    public AudioClip openPanel;

    AudioPlayerUI audioPlayer;
    
    GridManager gridManager;
    private Pathfinder pathfinder;
    private Vector2Int coordinates = new Vector2Int();

    Color normalColor;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        normalColor = transform.GetComponent<SpriteRenderer>().color;
        buildPanel = popUps.transform.GetChild(0).gameObject;
        upgradePanel = popUps.transform.GetChild(1).gameObject;

        audioPlayer = FindObjectOfType<AudioPlayerUI>();

    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                //Debug.Log("Blocking tile (" + coordinates.x + "," + coordinates.y + ")");
                gridManager.BlockNode(coordinates);
            }
        }

    }
    
    private void FreeTile(object sender, EventArgs e)
    {
        isPlaceable = true;
        Debug.Log("Freeing the tile");
        pathfinder.NotifyReceivers();
        towerOnThisTile.GetComponent<TowerManagement>().OnSellTower -= FreeTile;
    }

   void OnMouseEnter() {
       if (!IsBuildOpen && !IsUpgradeOpen)
       {
           ColorChanger(false);
       }
   }

    void OnMouseExit() {
        sprite.color = normalColor;
   }


    void OnMouseDown() {
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
        
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            /*currentPlayerPosition = playerPrefab.transform.position;

            Vector3 potentialTowerPosition = transform.position;
            potentialTowerPosition.z = transform.position.z - 1;

            float playerDistanceFromTile = Vector2.Distance(currentPlayerPosition, potentialTowerPosition);

            if (playerDistanceFromTile <= placeDistance)
            {
                isTowerPlaced = towerPrefab.CreateTower(towerPrefab, potentialTowerPosition);
            }
                    
            if (isTowerPlaced) {
                isPlaceable = false;
            }*/

            // Dictionary<Vector2Int, Node> grid = gridManager.Grid;
            // Vector2Int cord = gridManager.GetCoordinatesFromPosition(transform.position);
            // Debug.Log("isExplored: " + grid[cord].isExplored);
            // Debug.Log("isPath: " + grid[cord].isPath);
            // Debug.Log("isWalkable: " + grid[cord].isWalkable);

            float playerDistanceFromSquare = Vector2.Distance(transform.position, playerPrefab.transform.position);
            
            // Debug.Log("Clicked");

            if (playerDistanceFromSquare <= placeDistance && isPlaceable && !IsBuildOpen && !IsUpgradeOpen)
            {
                
                if (transform.position.x <= 8)
                {
                    buildPanel.transform.localPosition = new Vector3(200, -250, 0);
                }
                else
                {
                    buildPanel.transform.localPosition = new Vector3(-950, -250, 0);
                }
                
                //Debug.Log("Tile: +" + transform.position.ToString());
                buildPanel.gameObject.SetActive(true);
                IsBuildOpen = true;

                audioPlayer.PlayOpenSound();

                StartCoroutine(WaitForUIResponse());
            }
        }
    }
    
    
    private void ColorChanger(bool isSelected)
    {
        if (isPlaceable)
        {
            if (isSelected)
            {
                sprite.color = Color.blue;
            }
            else
            {
                sprite.color = Color.green;
            }
        }
        else if (!isPlaceable)
        {
            sprite.color = Color.red;
        }
    }
    

    IEnumerator WaitForUIResponse()
    {
        while (IsBuildOpen)
        {
            yield return new WaitForEndOfFrame();
            ColorChanger(true);
        }

        if (!IsForcedClosed)
        {
            Vector3 opti = transform.position;
            bool isSuccessful = towerPrefab.CreateTower(UIManager.TowerChoosen, new Vector3(opti.x, opti.y, opti.z -1), ref towerOnThisTile);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
                isPlaceable = false;
                towerOnThisTile.GetComponent<TowerManagement>().OnSellTower += FreeTile;
            }
        }

        sprite.color = normalColor;
        IsForcedClosed = true;
    }
    
}