using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    Enemy enemy;
    Player player;

    float distanceFromPlayer;
    float timeBetweenHit;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<Player>();
    }

    void Update() {
            timeBetweenHit += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && timeBetweenHit >= 1f)
        {
            player.SetPlayerHealth(player.GetPlayerHealth - enemy.EnemyStats.Damage);
            timeBetweenHit = 0f;
        }
    }
}