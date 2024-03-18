using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] GameObject playerPrefab;
    
    Vector3 currentPlayerPosition;
    [SerializeField] float placeDistance = 1f;

    [SerializeField] bool isPlaceable;

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

            Debug.Log(playerDistanceFromTile.ToString());
            Debug.Log(currentPlayerPosition.ToString());

            if (playerDistanceFromTile <= placeDistance)
            {
                Instantiate(towerPrefab, potentialTowerPosition, Quaternion.identity);
                isPlaceable = false;
            }
        }
    }
}
