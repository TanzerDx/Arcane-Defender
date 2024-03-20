using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject playerPrefab;
    
    Vector3 currentPlayerPosition;
    [SerializeField] float placeDistance = 1f;

    [SerializeField] bool isPlaceable;
    bool isTowerPlaced = false;

    Bank bank; 

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public bool IsPlaceableValue { 
        get {return isPlaceable;}
        }

    void OnMouseDown() {
        if (isPlaceable)
        {
            currentPlayerPosition = playerPrefab.transform.position;

            Vector3 potentialTowerPosition = transform.position;
            potentialTowerPosition.z = transform.position.z - 1;

            float playerDistanceFromTile = Vector2.Distance(currentPlayerPosition, potentialTowerPosition);

            if (playerDistanceFromTile <= placeDistance)
            {
                isTowerPlaced = towerPrefab.CreateTower(towerPrefab, potentialTowerPosition);
            }
                    
            if (isTowerPlaced) {
                isPlaceable = false;
            }
        }
    }
}
