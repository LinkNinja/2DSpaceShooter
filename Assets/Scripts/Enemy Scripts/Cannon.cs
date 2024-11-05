using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int health = 100;
    public UpdatedBoss boss; // Reference to the boss
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public GameObject explosionPrefab; // Reference to the explosion prefab

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject); // Destroy the bullet on impact
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation); // Create the explosion
            DestroyCannon();
        }
    }

    void DestroyCannon()
    {
        Destroy(gameObject);
        // Notify the boss that this cannon is destroyed (if needed)
    }
}
