using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    //Base stats
    private int health;
    private float moveSpeed;
    private int damage;
    
    //Damage resistances
    private float physResistance;
    private float magicResistance;
    
    //Crowd Control resistances
    private float stunResist;
    private float slowResist;
    private float poisonResist;
    private bool isStunned;
    private bool isSlowed;
    private bool isPoisoned;
    
    //Other & miscellaneous
    private float damageMultiplyer;



    public Stats(int _health, float _moveSpeed, int _damage, float _physResistance, float _magicResistance,
        float _stunResist, float _slowResist, float _poisonResist)
    {
        health = _health;
        moveSpeed = _moveSpeed;
        damage = _damage;
        physResistance = _physResistance;
        magicResistance = _magicResistance;
        stunResist = _stunResist;
        slowResist = _slowResist;
        poisonResist = _poisonResist;
        damageMultiplyer = 1f;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int TakingDamage(int amount, bool physical)
    {
        if (physical)
        {
            health = health - (int)(amount * physResistance*damageMultiplyer);
        }
        else
        {
            health = health - (int)(amount * magicResistance * damageMultiplyer);
        }

        return health;
    }

    public float GetStunned(float duration)
    {
        isStunned = true;
        return duration * stunResist;
    }
    
    public float GetSlowed(float duration)
    {
        isSlowed = true;
        return duration * slowResist;
    }
    
    public float GetPoisoned(float duration)
    {
        isPoisoned = true;
        return duration * poisonResist;
    }

    public void EndStun()
    {
        isStunned = false;
    }
    
    public void EndSlow()
    {
        isSlowed = false;
    }
    
    public void EndPoison()
    {
        isPoisoned = false;
    }

}
