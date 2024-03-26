using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int crystalCost = 5;
    [SerializeField] int resourceCost = 5;
    

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        
        if (bank == null)
        {
            return false;
        }


        if(bank.GetCurrentCrystalBalance >= crystalCost && bank.GetCurrentResourceBalance >= resourceCost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(crystalCost, resourceCost);
            return true;
        }

        return false;
    }
}
