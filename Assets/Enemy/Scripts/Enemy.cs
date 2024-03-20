using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int crystalReward = 25;
    [SerializeField] int resourceReward = 40;


    Bank bank; 


    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void Reward()
    {
        if(bank != null)
        {
            bank.Deposit(crystalReward, resourceReward);
        }
    }

}

