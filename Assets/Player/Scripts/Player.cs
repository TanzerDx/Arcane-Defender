using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    float characterColorTimer;

    HealthManager healthManager;
    
    public int GetPlayerHealth { get { return playerHealth; }}

    public int SetPlayerHealth(int newPlayerHealth){
        playerHealth = newPlayerHealth;
        return playerHealth;
    }
    

    [SerializeField] int playerDamage = 2;
    public int GetPlayerDamage { get { return playerDamage; }}

    private void Awake() {
        healthManager = FindObjectOfType<HealthManager>();
    }

    void OnEnable() {
        GetComponent<SpriteRenderer>().color = Color.white;
        healthManager.ChangeHealthbar("player", playerHealth);
    }



    void Update()
    {
        if(playerHealth <= 0)
        {
            gameObject.SetActive(false);
            
            Tile.IsBuildOpen = true;
            Tile.IsUpgradeOpen = true;
        }

        if(GetComponent<SpriteRenderer>().color == Color.red)
        {
            characterColorTimer += Time.deltaTime;
        }

        if(characterColorTimer > 0.25f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            characterColorTimer = 0f;
        }
    }

}
