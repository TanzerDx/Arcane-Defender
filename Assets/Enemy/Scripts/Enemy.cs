// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public event EventHandler OnEnemyKilled;
    
    [SerializeField] int crystalReward = 25;
    [SerializeField] int resourceReward = 40;

    public AudioClip[] enemyHitAudio;
    AudioSource enemySource;
    float soundPitch;

    
    float enemyColorTimer;

    Bank bank;

    [Tooltip("The current health of the monster")]
    [SerializeField] int health;
    
    [Tooltip("The move speed of the monster (1 being the base speed)")]
    [SerializeField] [Range(0f, 5f)] float moveSpeed;
    
    [Tooltip("The amount of damage dealt to the player")]
    [SerializeField] int damage;
    
    [Tooltip("The proportion of physical damage taken (2 = Weak to this || 1 = No resistance || 0 = Immunity)")]
    [SerializeField] [Range(0f, 2f)] float physRes;
    
    [Tooltip("The proportion of magical damage taken (2 = Weak to this || 1 = No resistance || 0 = Immunity)")]
    [SerializeField] [Range(0f, 2f)] float magicRes;

    [Tooltip("The proportion of slow duration applied (2 = Weak to this || 1 = No resistance || 0 = Immunity)")]
    [SerializeField] [Range(0f, 2f)] float slowRes;
    
    
    Stats stats;
    
    public Stats EnemyStats
    {
        get { return stats; }
    }

    private void Awake()
    {
        stats = new Stats(health, moveSpeed, damage, physRes, magicRes, slowRes);
    }

    void Start()
    {
        bank = FindObjectOfType<Bank>();
        enemySource = gameObject.GetComponent<AudioSource>();
    }

    // void OnEnable()
    // {
    //     stats.ResetHealth(health);
    // }

    void Update() {
        if(GetComponent<SpriteRenderer>().color == Color.red)
        {
            enemyColorTimer += Time.deltaTime;
        }

        if(enemyColorTimer > 0.25f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            enemyColorTimer = 0f;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Particle"))
        {
            Particle particleData = other.gameObject.GetComponent<Particle>();
            ProcessHit(particleData.Damage, particleData.IsPhysical, false);
        }
        //DOING: We'll need to modify this to take into account the various projectiles of the towers
    }

    public void Reward()
    {
        if(bank != null)
        {
            bank.Deposit(crystalReward, resourceReward);
        }
    }

    public void ProcessHit(int damageRecieved, bool isPhysical, bool isFromPlayer)
    {
        if (isFromPlayer)
        {

            int soundNumber = Random.Range(0, enemyHitAudio.Length);

            enemySource.clip = enemyHitAudio[soundNumber];
            enemySource.pitch = Random.Range(1f, 1.5f);
            enemySource.Play();
            
        }
        GetComponent<SpriteRenderer>().color = Color.red;

        if (stats.TakingDamage(damageRecieved, isPhysical) <= 0) {
            Reward();
            Destroy(gameObject);
        }
        
        Debug.Log("Outch! HP remaining = " + stats.Health + "\nDamage taken = " + damageRecieved);
    }

}

