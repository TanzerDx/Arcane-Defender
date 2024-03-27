using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform tower;
    [SerializeField] float shootingRange = 15f;
    float targetDistance;
    
    [SerializeField] GameObject particlePrefab;
    [SerializeField] float projectilesPerSecond = 1f;
    float timeUntilFire = 0f;
    
    Transform target;

    void Update() {
        FindClosestTarget();
        AimAtEnemy();
        timeUntilFire += Time.deltaTime;
    }

    void FindClosestTarget() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            targetDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;

                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
        Debug.Log(maxDistance);
    }

    void AimAtEnemy() {
        if(targetDistance < shootingRange && timeUntilFire >= 1f / projectilesPerSecond)
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
