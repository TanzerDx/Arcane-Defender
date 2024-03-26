using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float particleSpeed = 1f;
    float timeOfInitialization = 0f;

    private Transform target;

    public void SetTarget(Transform toShoot) {
        target = toShoot;
    }

    private void Update() {
        if (!target)
        {
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
    
        rb.velocity = direction * particleSpeed;

        timeOfInitialization += Time.deltaTime;

        if (timeOfInitialization >= 1f || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
