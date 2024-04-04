using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemies;


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
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().ProcessHitFromPlayer(); 
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
