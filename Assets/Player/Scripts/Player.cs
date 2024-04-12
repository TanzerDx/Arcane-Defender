using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;
    float characterColorTimer;
    
    public int GetPlayerHealth { get { return playerHealth; }}

    public int SetPlayerHealth(int newPlayerHealth){
        playerHealth = newPlayerHealth;
        return playerHealth;
    }
    

    [SerializeField] int playerDamage = 2;
    public int GetPlayerDamage { get { return playerDamage; }}



    void Update()
    {
        if(playerHealth <= 0)
        {
            gameObject.SetActive(false);
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
