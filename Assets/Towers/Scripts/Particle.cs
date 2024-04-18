using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float particleSpeed = 1f;
    [SerializeField] float timeOfInitialization = 0f;

    private int damage;
    public int Damage
    {
        get { return damage;}
        set { damage = value; }
    }

    private bool isPhysical;
    
    public bool IsPhysical
    {
        get { return isPhysical; }
        set { isPhysical = value; }
    }

    private float slowDuration;

    public float SlowDuration
    {
        get { return slowDuration; }
        set { slowDuration = value; }
    }

    private float slowIntensity;

    public float SlowIntensity
    {
        get { return slowIntensity; }
        set { slowIntensity = value; }
    }
    
    private Transform target;

    public void SetTarget(Transform toShoot) {
        target = toShoot;
    }

    private void Update() {
        if (!target || timeOfInitialization >= 1.3f)
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
