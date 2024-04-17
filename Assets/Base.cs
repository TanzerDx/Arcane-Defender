using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] int baseMaxHealth = 20;
    [SerializeField] int baseCurrentHealth;

    Player player;
    float reviveTimer = 0f;

    HealthManager healthManager;
    [SerializeField] GameObject lostGameUI;

    void Start()
    {
        baseCurrentHealth = baseMaxHealth;
        healthManager = FindObjectOfType<HealthManager>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        if(baseCurrentHealth <= 0)
        {
            lostGameUI.SetActive(true);
            Tile.IsBuildOpen = true;
            Tile.IsUpgradeOpen = true;
        }

        if(player.GetPlayerHealth <= 0)
        {
            reviveTimer += Time.deltaTime;
        }

        if (reviveTimer >= 3f)
        {
            RevivePlayer();
            reviveTimer = 0;
        }

    }

    void OnTriggerEnter2D(Collider2D collider){
        
        if(collider.gameObject.GetComponent<Boss>() != null)
        {
            baseCurrentHealth = 0;
            healthManager.ChangeHealthbar("base", baseCurrentHealth);
        }
        
        else if(collider.gameObject.tag == "Enemy")
        {
            baseCurrentHealth = baseCurrentHealth - collider.gameObject.GetComponent<Enemy>().EnemyDamage;
            healthManager.ChangeHealthbar("base", baseCurrentHealth);
        }

    }

    void RevivePlayer()
    {
        player.SetPlayerHealth(10);

        player.transform.position = new Vector3(8, 3, -1);

        Tile.IsBuildOpen = false;
        Tile.IsUpgradeOpen = false;

        player.gameObject.SetActive(true);
    }
}
