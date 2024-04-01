using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    //Base stats
    [Tooltip("The current health of the monster")]
    private int health;
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
    [Tooltip("The proportion of stun duration applied (1 = No resistance || 0 = Immunity)")]
    private float stunResist;
    [Tooltip("The proportion of slow duration applied (1 = No resistance || 0 = Immunity)")]
    private float slowResist;
    [Tooltip("The proportion of poison duration applied (1 = No resistance || 0 = Immunity)")]
    private float poisonResist;
    
    [Tooltip("Is this monster currently stunned?")]
    private bool isStunned;
    public bool IsStunned
    {
        get { return isStunned; }
    }
    [Tooltip("Is this monster currently slowed?")]
    private bool isSlowed;
    public bool IsSlowed
    {
        get { return isSlowed; }
    }
    [Tooltip("Is this monster currently poisoned?")]
    private bool isPoisoned;

    private int poisonPotency;
    public bool IsPoisoned
    {
        get { return isPoisoned; }
    }
    
    //Other & miscellaneous
    [Tooltip("The damage multiplier of this enemy. 1 Is the default value (no damage modification)")]
    private float damageMultiplier;



    public Stats(int _health, float _moveSpeed, int _damage, float _physResistance, float _magicResistance,
        float _stunResist, float _slowResist, float _poisonResist)
    {
        health = _health;
        moveSpeed = _moveSpeed;
        speedSaver = moveSpeed;
        damage = _damage;
        
        physResistance = _physResistance;
        magicResistance = _magicResistance;
        stunResist = _stunResist;
        slowResist = _slowResist;
        poisonResist = _poisonResist;
        
        damageMultiplier = 1f;

        isStunned = false;
        isSlowed = false;
        isPoisoned = false;
    }
    
    

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
    
    

    public float GetStunned(float duration)
    {
        isStunned = true;
        moveSpeed = 0;
        return duration * stunResist;
    }
    
    public float GetSlowed(float duration, float intensity)
    {
        isSlowed = true;
        moveSpeed *= intensity;
        return duration * slowResist;
    }
    
    public float GetPoisoned(float duration, int potency)
    {
        isPoisoned = true;
        poisonPotency = potency;
        return duration * poisonResist;
    }

    public void EndStun()
    {
        isStunned = false;
        moveSpeed = speedSaver;
    }
    
    public void EndSlow()
    {
        isSlowed = false;
        moveSpeed = speedSaver;
    }
    
    public void EndPoison()
    {
        isPoisoned = false;
    }

}
