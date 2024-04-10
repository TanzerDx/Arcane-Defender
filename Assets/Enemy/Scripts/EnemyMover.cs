using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    //[SerializeField] [Range(0f, 5f)] private float speed = 1f;
    
    List<Node> path = new List<Node>();

    Enemy enemy;
    GameObject player;
    GridManager gridManager;
    Pathfinder pathfinder;

    EnemyAttack enemyAttack;

    float distanceFromPlayer;
    

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        
        enemyAttack = GetComponent<EnemyAttack>();

        player = GameObject.FindWithTag("Player");

    }

    // void Update()
    // {
    //     distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);

    //     if(distanceFromPlayer < 2f)
    //     {
    //         FollowPlayer(true);
    //     }
    //     else
    //     {
    //         FollowPlayer(true);
    //     }
    // }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

            startPosition.z -= 1;
            endPosition.z -= 1;

            float travelPercent = 0f;

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * enemy.EnemyStats.MoveSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();       
            }                
        }

        Destroy(gameObject);
    }

    // void FollowPlayer(bool isInRange)
    // {
    //     if(isInRange)
    //     {
    //         StopAllCoroutines();
    //         path.Clear();

    //         enemyAttack.TargetPlayer(); 
    //     }

    //     RecalculatePath(false);
    // }
}
