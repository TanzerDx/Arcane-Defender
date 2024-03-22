using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    Vector2 coordinates;

    void Awake() {
        FindPath();
    }

    void OnEnable()
    {
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }

    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;

            startPosition.z = waypoint.transform.position.z - 1;
            endPosition.z = waypoint.transform.position.z - 1;

            float travelPercent = 0f;

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
                        
        }

        gameObject.SetActive(false);
    }
}
