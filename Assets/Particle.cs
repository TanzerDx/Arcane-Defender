using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float particleSpeed = 1f;

    private Transform target;

    public void SetTarget(Transform toShoot) {
        target = toShoot;
    }

    private void FixedUpdate() {
        if (!target)
        {
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
    
        rb.velocity = direction * particleSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
