using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("This should be the same as the \"health\" parameter in the Enemy script")]
    int maxHitpoints;
    //[SerializeField] int currentHitpoints = 0;

    Enemy enemy;
    Player player;

    void Awake() {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
        Debug.Log(enemy);
        maxHitpoints = enemy.EnemyStats.Health;
    }

    void OnEnable()
    {
        enemy.EnemyStats.ResetHealth(maxHitpoints);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Particle")
        {
            ProcessHitFromTower();
        }
        //TODO: We'll need to modify this to take into account the various projectiles of the towers
    }

    void ProcessHitFromTower()
    {
        if (enemy.EnemyStats.TakingDamage(1, true) <= 0) {
            gameObject.SetActive(false);
            enemy.Reward();
        }
    }

    public void ProcessHitFromPlayer()
    {
        if (enemy.EnemyStats.TakingDamage(player.GetPlayerDamage, true) <= 0) {
            gameObject.SetActive(false);
            enemy.Reward();
        }
    }


}
