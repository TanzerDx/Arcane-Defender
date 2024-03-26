using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitpoints = 5;
    [SerializeField] int currentHitpoints = 0;

    Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitpoints = maxHitpoints;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitpoints--;
        Debug.Log("The enemy has been hit! :D");

        if (currentHitpoints <= 0) {
            gameObject.SetActive(false);
            enemy.Reward();
        }
    }
}
