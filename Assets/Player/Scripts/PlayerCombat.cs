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

    AudioSource playerSource;

    private void Awake() {
        player = GetComponent<Player>();
        playerSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {         
            int soundNumberSwing = Random.Range(0, staffSwingSounds.Length);

            playerSource.clip = staffSwingSounds[soundNumberSwing];
            playerSource.pitch = Random.Range(1, 1.5f);
            playerSource.Play();

            Attack();

        }
    }

    void Attack()
    {
        //Play attack animation

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        
        if(hitEnemies.Length != 0)
        {
            int soundNumberHit = Random.Range(0, staffHitSounds.Length);

            playerSource.clip = staffHitSounds[soundNumberHit];
            playerSource.pitch = Random.Range(1, 1.5f);
            playerSource.Play();
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
