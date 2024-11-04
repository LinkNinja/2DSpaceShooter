using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    public int damage = 20; // Define damage dealt by the projectile

    void Start()
    {
        //Invoke("DestroyProjectile", lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;

        if (transform.position.x > 8)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to the enemy
                Destroy(gameObject); // Destroy the projectile
            }
        }
    }
}
