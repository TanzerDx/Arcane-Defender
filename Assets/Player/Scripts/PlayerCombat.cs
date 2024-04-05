using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemies;
    Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Play attack animation

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().ProcessHit(player.GetPlayerDamage, true); 
        }
    }

    void OnDrawGizmosSelected() {
        
        if (attackPoint == null) 
        {
            return;
        }

       Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}
