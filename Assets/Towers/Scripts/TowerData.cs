using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData
{
    //Tower level
    [Tooltip("The current level of the tower 0 being a non-upgraded tower")]
    private int level;
    
    public int Level
    {
        get { return level; }
    }


    //Tower's damage stat depending on current level
    [Tooltip("Base damage of the tower, updated if upgraded")]
    private int damage;

    [Tooltip("Damage gained after upgrading a tower to level 1")]
    private int damageUp1;

    [Tooltip("Damage gained after upgrading a tower to level 2")]
    private int damageUp2;
    public int Damage
    {
        get { return damage; }
    }


    //Tower and upgrades costs
    [Tooltip("Base cost of the tower (crystals, resources)")]
    private (int crystals, int resources) cost;

    [Tooltip("Cost of the first upgrade (crystals, resources)")]
    private (int crystals, int resources) lvUpCost1;

    [Tooltip("Cost of the second upgrade (crystals, resources)")]
    private (int crystals, int resources) lvUpCost2;



    //Tower range depending on current level
    [Tooltip("Base range of the tower, updated if upgraded (1 range = 1 square)")]
    private float range;
    public float Range
    {
        get { return range; }
    }

    [Tooltip("Range gained after upgrading a tower to level 1")]
    private float rangeUp1;

    [Tooltip("Range gained after upgrading a tower to level 2")]
    private float rangeUp2;


    //Tower attack speed depending on current level
    [Tooltip("Number of attacks per second")]
    private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    [Tooltip("Attack speed gained after upgrading a tower to level 1")]
    private float attackSpeedUp1;

    [Tooltip("Attack speed gained after upgrading a tower to level 2")]
    private float attackSpeedUp2;


    //Tower category AKA does it inflicts slow + need for another one for the slow duration
    [Tooltip("The category this tower belongs in")]
    private bool category;
    public bool Category
    {
        get { return category; }
    }


    //Tower damage type
    [Tooltip("Is the tower dealing physical damage? If false, the tower is dealing magical damage")]
    private bool physical;
    public bool Physical
    {
        get { return physical; }
    }


    //Tower sell ratio
    [Tooltip("The proportion of a tower's total cost refunded to the player when selling")]
    private float sellRatio;

    public TowerData((int, int, int) _damage, (int, int, int) _crysCosts, (int, int, int) _ressCosts,
        (float, float, float) _ranges, (float, float, float) _speed, bool _category, bool _dmgType)
    {
        level = 0;
        (damage, damageUp1, damageUp2) = _damage;
        (cost.crystals, lvUpCost1.crystals, lvUpCost2.crystals) = _crysCosts;
        (cost.resources, lvUpCost1.resources, lvUpCost2.resources) = _ressCosts;
        (range, rangeUp1, rangeUp2) = _ranges;
        (attackSpeed, attackSpeedUp1, attackSpeedUp2) = _speed;
        category = _category;
        physical = _dmgType;
        sellRatio = 0.75f;
    }


    //This method returns the amount of crystals and resources a player gets by selling a tower
    //(Crystals, Resources)
    public (int, int) Sell()
    {
        (int, int) tot = cost;

        if (level >= 1)
        {
            tot.Item1 += lvUpCost1.Item1;
            tot.Item2 += lvUpCost1.Item2;
        }

        if (level >= 2)
        {
            tot.Item1 += lvUpCost2.Item1;
            tot.Item2 += lvUpCost2.Item2;
        }

        return ((int)(tot.Item1 * sellRatio), (int)(tot.Item2 * sellRatio));
    }


    //Returns the cost of the upgrade to the next level
    //If the upgrade is impossible, returns (0,0)
    //If it is possible, returns the cost of the upgrade and updates the stats
    //(Crystals, Resources)
    public (int, int) Upgrade((int, int) money)
    {
        (int, int) price = money;
        price.Item1 += 1;
        price.Item2 += 1;

        if (level == 0)
        {
            price = lvUpCost1;
        }
        else if (level == 1)
        {
            price = lvUpCost2;
        }

        if (price.Item1 > money.Item1 && price.Item2 > money.Item2)
        {
            return (0, 0);
        }

        level += 1;
        if (level == 1)
        {
            damage += damageUp1;
            range += rangeUp1;
            attackSpeed += attackSpeedUp1;
        }
        else
        {
            damage += damageUp2;
            range += rangeUp2;
            attackSpeed += attackSpeedUp2;
        }

        return price;
    }



}
