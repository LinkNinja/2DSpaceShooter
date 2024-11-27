using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int health = 100;
    public UpdatedBoss boss;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public GameObject explosionPrefab; 
    public Animator animator;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Start()
    {
        // Initialize the Animator component
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                // Destroy the bullet
                Destroy(other.gameObject); 
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
        // Trigger the damage animation
        animator.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            DestroyCannon();
        }
    }

    void DestroyCannon()
    {
        Destroy(gameObject);
        
    }
}
