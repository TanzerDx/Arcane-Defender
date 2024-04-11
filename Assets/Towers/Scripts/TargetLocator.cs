using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform tower;
    [SerializeField] float shootingRange = 15f;
    List<GameObject> enemies = new List<GameObject>();
    
    [SerializeField] GameObject particlePrefab;
    //[SerializeField] float projectilesPerSecond = 1f;
    float timeUntilFire = 0f;
    
    Transform target;

    ObjectPool objectPool;

    void Awake() {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    [SerializeField] private Tower link;
    private TowerData data;

    private void Start()
    {
        data = link.Data;
    }

    void Update() {
        FindClosestTarget();
        AimAtEnemy();
        timeUntilFire += Time.deltaTime;
        
    }

    void OnTriggerEnter2D(Collider2D other) {
            Debug.Log(other.gameObject);

            enemies.Add(other.gameObject);
            Debug.Log("Enemy added!");
    }

    void OnTriggerExit2D(Collider2D other) {
            enemies.Remove(other.gameObject);
            Debug.Log("Enemy removed!");
    
    }

    void FindClosestTarget() 
    {
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (enemyDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = enemyDistance;
            }
        }
        
        target = closestTarget;
    }

    void AimAtEnemy() {
        if(enemies.Count == 0)
        {
            return;
        }

        float targetDistance = Vector2.Distance(transform.position, target.position);

        if(targetDistance < shootingRange && timeUntilFire >= 1f / data.AttackSpeed)
        {
            Attack(true);
            timeUntilFire = 0f;
            
        }
        else 
        {
            Attack(false);
        }

    }

    void Attack(bool isActive)
    {
        if(isActive)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Particle particleScript = particle.GetComponent<Particle>();
            particleScript.SetTarget(target);
        
        }
    }
}
