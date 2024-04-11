using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject playerPrefab;

    Vector3 currentPlayerPosition;
    [SerializeField] float placeDistance = 1f;

    [SerializeField] bool isPlaceable;
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

            if (playerDistanceFromSquare <= placeDistance && isPlaceable)
            {
                Vector3 opti = transform.position;
                bool isSuccessful = towerPrefab.CreateTower(towerPrefab, new Vector3(opti.x, opti.y, opti.z -1));
                if (isSuccessful)
                {
                    gridManager.BlockNode(coordinates);
                    pathfinder.NotifyReceivers();
                    isPlaceable = false;
                }
            }

        }
    }
}
