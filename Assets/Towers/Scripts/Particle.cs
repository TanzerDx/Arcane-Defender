using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float particleSpeed = 1f;
    [SerializeField] float timeOfInitialization = 0f;

    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage;}
        set { damage = value; }
    }

    [SerializeField] private bool isPhysical;
    
    public bool IsPhysical
    {
        get { return isPhysical; }
        set { isPhysical = value; }
    }
    
    private Transform target;

    public void SetTarget(Transform toShoot) {
        target = toShoot;
    }

    private void Update() {
        if (!target || timeOfInitialization >= 0.5f)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
    
        rb.velocity = direction * particleSpeed;

        timeOfInitialization += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
