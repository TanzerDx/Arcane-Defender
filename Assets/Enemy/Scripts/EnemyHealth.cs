using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("This should be the same as the \"health\" parameter in the Enemy script")]
    int maxHitpoints;
    //[SerializeField] int currentHitpoints = 0;

    Enemy enemy;

    void Awake() {
        enemy = GetComponent<Enemy>();
        maxHitpoints = enemy.EnemyStats.Health;
    }

    void OnEnable()
    {
        enemy.EnemyStats.ResetHealth(maxHitpoints);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        ProcessHit();
        //TODO: We'll need to modify this to take into account the various projectiles of the towers
    }

    void ProcessHit()
    {
        if (enemy.EnemyStats.TakingDamage(1, true) <= 0) {
            gameObject.SetActive(false);
            enemy.Reward();
        }
    }
}
