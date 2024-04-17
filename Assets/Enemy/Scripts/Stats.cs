using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Maybe need to checkout GameDev.tv (Again...) to see what the serialization of a class is for
[System.Serializable]
public class Stats
{
    //IMPORTANT: The stats that can easily be accessed outside of the class are :
    // - Health
    // - Move speed
    // - Damage
    // - Is Stunned
    // - Is Slowed
    // - Is Poisoned

    //Base stats
    [Tooltip("The current health of the monster")]
    private int health;
    public int Health
    {
        get { return health; }
    }
    
    [Tooltip("The move speed of the monster (1 being the base speed)")]
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
    }
    [Tooltip("The base speed of the enemy")]
    private float speedSaver;
    
    
    [Tooltip("The amount of damage dealt to the player")]
    private int damage;
    public int Damage
    {
        get { return damage; }
    }
    
    //Damage resistances
    [Tooltip("The proportion of physical damage taken (1 = No resistance || 0 = Immunity)")]
    private float physResistance;
    [Tooltip("The proportion of magical damage taken (1 = No resistance || 0 = Immunity)")]
    private float magicResistance;
    
    //Crowd Control resistances
    [Tooltip("The proportion of slow duration applied (1 = No resistance || 0 = Immunity)")]
    private float slowResist;
    
    
    [Tooltip("Is this monster currently slowed?")]
    private bool isSlowed;
    public bool IsSlowed
    {
        get { return isSlowed; }
    }

    
    //Other & miscellaneous
    [Tooltip("The damage multiplier of this enemy. 1 Is the default value (no damage modification)")]
    private float damageMultiplier;



    public Stats(int _health, float _moveSpeed, int _damage, float _physResistance, float _magicResistance, 
        float _slowResist)
    {
        health = _health;
        moveSpeed = _moveSpeed;
        speedSaver = moveSpeed;
        damage = _damage;
        
        physResistance = _physResistance;
        magicResistance = _magicResistance;
        slowResist = _slowResist;

        damageMultiplier = 1f;
        
        isSlowed = false;
    }
    
    

    //This method deduces "amount" health from the enemy health.
    //The damage type needs to be provided in order to compute the actual damage received
    //This method returns the remaining amount of HP the enemy has (hp <= 0 means the enemy is dead)
    public int TakingDamage(int amount, bool physical)
    {
        if (physical)
        {
            health = health - (int)(amount * physResistance*damageMultiplier);
        }
        else
        {
            health = health - (int)(amount * magicResistance * damageMultiplier);
        }

        return health;
    }

    
    //Puts the enemy's health to its maximum
    public void ResetHealth(int maxHp)
    {
        health = maxHp;
    }
    
    
    //Apply a slow debuff to the enemy for "duration" seconds
    //The speed reduction is determined by "intensity"
    //0 locking the enemy in place and 1 doing nothing
    //This method returns the total duration of the effect (enemies can be resilient to slow effects)
    public float GetSlowed(float duration, float intensity = 0.5f)
    {
        isSlowed = true;
        moveSpeed = (speedSaver*intensity)/slowResist;
        return duration * slowResist;
    }
    
    
    
    //Reverts the enemies speed to its default value
    public void EndSlow()
    {
        isSlowed = false;
        moveSpeed = speedSaver;
    }
    

}
