using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerHealth = 10f;
    [SerializeField] float playerDamage = 2f;

    public float GetPlayerHealth
    {
        get { return playerHealth; }
    }

    public float SetPlayerHealth(float newPlayerHealth)
    {
        playerHealth = newPlayerHealth;

        return playerHealth;
    }

        public float PlayerDamage
    {
        get { return playerDamage; }
    }

    void Update()
    {
        if(playerHealth <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    void DealDamage()
    {
        
    }
}
