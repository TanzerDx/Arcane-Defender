using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform tower;
    [SerializeField] float shootingRange = 35f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;

    void Update() {
        FindClosestTarget();
        AimAtEnemy();
    }

    void FindClosestTarget() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;

                maxDistance = targetDistance;
            }

            target = closestTarget;
        }
    }

    void AimAtEnemy() {
        float targetDistance = Vector2.Distance(transform.position, target.position);

        if(targetDistance < shootingRange)
        {
            Attack(true);
        }
        else 
        {
            Attack(false);
        }

    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
