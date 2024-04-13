using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemies;
    Player player;

    public AudioClip[] staffSwingSounds;
    public AudioClip[] staffHitSounds;

    public AudioSource playerSource;

    public int soundNumberSwing;
    public int soundNumberHit;

    private void Awake() {
        player = GetComponent<Player>();
        
        soundNumberSwing = Random.Range(0, staffSwingSounds.Length);
        soundNumberHit = Random.Range(0, staffHitSounds.Length);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {         
            playerSource.clip = staffSwingSounds[soundNumberSwing];
            playerSource.Play();
            soundNumberSwing = Random.Range(0, staffSwingSounds.Length);

            Attack();

        }
    }

    void Attack()
    {
        //Play attack animation

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        
        if(hitEnemies.Length != 0)
        {
            playerSource.clip = staffHitSounds[soundNumberHit];
            playerSource.Play();
            soundNumberHit = Random.Range(0, staffHitSounds.Length);
        }

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().ProcessHit(player.GetPlayerDamage, true, true); 
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
