using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

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

    [SerializeField] private GameObject popUps;

    private GameObject buildPanel;
    private GameObject upgradePanel;
    //bool isTowerPlaced = false;
    
    public bool IsPlaceableValue 
    { 
        get {return isPlaceable;}
    }
    

    GridManager gridManager;
    private Pathfinder pathfinder;
    private Vector2Int coordinates = new Vector2Int();


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        buildPanel = popUps.transform.GetChild(0).gameObject;
        upgradePanel = popUps.transform.GetChild(1).gameObject;
        // if (buildPanel == null)
        // {
        //     print("Well that is akward...");
        // }
        // buildPanel.gameObject.SetActive(false);
        // upgradePanel.gameObject.SetActive(false);

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
                Debug.Log("All good to go");
                buildPanel.gameObject.SetActive(true);
                IsBuildOpen = true;
                StartCoroutine(WaitForUIResponse());
            }
        }
    }

    IEnumerator WaitForUIResponse()
    {
        while (IsBuildOpen)
        {
            yield return new WaitForEndOfFrame();
        }

        if (!IsForcedClosed)
        {
            Vector3 opti = transform.position;
            bool isSuccessful = towerPrefab.CreateTower(UIManager.TowerChoosen, new Vector3(opti.x, opti.y, opti.z -1));
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
                isPlaceable = false;
            }
        }

        IsForcedClosed = true;
    }
    
}
