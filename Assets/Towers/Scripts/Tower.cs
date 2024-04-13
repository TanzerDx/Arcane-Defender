using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Tower : MonoBehaviour
{
    //[SerializeField] int crystalCost = 5;
    //[SerializeField] int resourceCost = 5;

    #region Damage
    
    [Tooltip("Base damage of the tower, updated if upgraded")]
    [SerializeField] [Range(1, 5)] private int damage;
    
    [Tooltip("Damage gained after upgrading a tower to level 1")]
    [SerializeField] private int damageUp1;
    
    [Tooltip("Damage gained after upgrading a tower to level 2")]
    [SerializeField] private int damageUp2;
    
    #endregion

    
    
    #region Costs

    [Tooltip("Base cost of the tower (crystals, resources)")]
    [SerializeField] [Range(0, 1000)] private int crystalsCost;
    
    [Tooltip("Cost of the first upgrade (crystals, resources)")]
    [SerializeField] private int crystalsLvUpCost1;
    
    [Tooltip("Cost of the second upgrade (crystals, resources)")]
    [SerializeField] private int crystalsLvUpCost2;
    
    [Tooltip("Base cost of the tower (crystals, resources)")]
    [SerializeField] [Range(0, 1000)] private int resourcesCost;
    
    [Tooltip("Cost of the first upgrade (crystals, resources)")]
    [SerializeField] private int resourceLvUpCost1;
    
    [Tooltip("Cost of the second upgrade (crystals, resources)")]
    [SerializeField] private int resourceLvUpCost2;

    #endregion



    #region Range

    [Tooltip("Base range of the tower, updated if upgraded (1 range = 1 square)")]
    [SerializeField] [Range(1f, 5f)]private float range;
    
    [Tooltip("Range gained after upgrading a tower to level 1")]
    [SerializeField] private float rangeUp1;
    
    [Tooltip("Range gained after upgrading a tower to level 2")]
    [SerializeField] private float rangeUp2;

    #endregion



    #region AttackSpeed

    [Tooltip("Number of attacks per second")]
    [SerializeField] [Range(0.1f, 5f)] private float attackSpeed;
    
    [Tooltip("Attack speed gained after upgrading a tower to level 1")]
    [SerializeField] private float attackSpeedUp1;
    
    [Tooltip("Attack speed gained after upgrading a tower to level 2")]
    [SerializeField] private float attackSpeedUp2;

    #endregion



    #region Others

    [Tooltip("The category this tower belongs in")]
    [SerializeField] private string category;
    
    [Tooltip("Is the tower dealing physical damage? If false, the tower is dealing magical damage")]
    [SerializeField] private bool physical;

    #endregion


    private TowerData data;

    public AudioClip[] tower1Sounds;
    public AudioClip[] tower2Sounds;
    public AudioClip[] tower3Sounds;
    public AudioClip[] tower4Sounds;

    public AudioSource towerSource;

    int soundNumber;
    float soundPitch;
    
    public TowerData Data
    {
        get { return data; }
    }

    private void Awake()
    {
        data = new TowerData((damage, damageUp1, damageUp2), (crystalsCost, crystalsLvUpCost1, crystalsLvUpCost2),
            (resourcesCost, resourceLvUpCost1, resourceLvUpCost2), (range, rangeUp1, rangeUp2),
            (attackSpeed, attackSpeedUp1, attackSpeedUp2), category, physical);

        soundNumber = Random.Range(0, tower1Sounds.Length);
        soundPitch = Random.Range(1f, 1.5f);
    }


    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        
        if (bank == null)
        {
            return false;
        }


        if(bank.GetCurrentCrystalBalance >= crystalsCost && bank.GetCurrentResourceBalance >= resourcesCost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            
            towerSource.clip = tower1Sounds[soundNumber];
            towerSource.pitch = soundPitch;
            towerSource.Play();
            
            soundNumber = Random.Range(0, tower1Sounds.Length);
            soundPitch = Random.Range(1f, 1.5f);
            
            bank.Withdraw(crystalsCost, resourcesCost);
            return true;

        }

        return false;
    }
}
