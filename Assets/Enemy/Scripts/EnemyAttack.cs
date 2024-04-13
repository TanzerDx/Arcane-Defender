using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    Enemy enemy;
    Player player;
    HealthManager healthManager;

    float distanceFromPlayer;
    float timeBetweenHit;
    float characterColorTimer;

    public AudioClip playerDamage;
    AudioSource playerSource;
    float soundPitch;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<Player>();
        healthManager = FindObjectOfType<HealthManager>();
        playerSource = player.GetComponent<AudioSource>();

        soundPitch = Random.Range(1f, 1.5f);
    }

    void Update() {
        timeBetweenHit += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && timeBetweenHit >= 1f)
        {
            playerSource.clip = playerDamage;
            playerSource.pitch = soundPitch;
            playerSource.Play();
            soundPitch = Random.Range(1f, 1.5f);

            player.SetPlayerHealth(player.GetPlayerHealth - enemy.EnemyStats.Damage);
            healthManager.ChangeHealthbar("player", player.GetPlayerHealth);

            player.GetComponent<SpriteRenderer>().color = Color.red;

            timeBetweenHit = 0f;
        }
    }
}
