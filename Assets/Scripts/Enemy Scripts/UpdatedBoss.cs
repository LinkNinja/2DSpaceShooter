using System.Collections;
using UnityEngine;

public class UpdatedBoss : MonoBehaviour
{
    public int health = 1000;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public Cannon[] cannons; // Array to hold references to the cannons
    public float moveSpeed = 2f;
    private bool movingUp = true;
    private bool phaseTwo = false;
    private bool canTakeDamage = false;
    public Animator animator;
    public float chargeTime = 2.0f; // Time before firing the laser

    void Update()
    {
        Move();

        // Check if all cannons are destroyed
        if (!phaseTwo && AllCannonsDestroyed())
        {
            phaseTwo = true;
            StartCoroutine(EnterPhaseTwo());
        }
    }

    private void Start()
    {
        // Initialize the Animator component
        animator = GetComponent<Animator>();
    }

    void Move()
    {
        // Vertical movement logic
        if (movingUp)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= 2.25) // Upper limit
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= -0.25) // Lower limit
            {
                movingUp = true;
            }
        }
    }

    bool AllCannonsDestroyed()
    {
        foreach (Cannon cannon in cannons)
        {
            if (cannon != null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator EnterPhaseTwo()
    {
        // Phase 2 logic
        moveSpeed *= 1.5f;
        canTakeDamage = true; // Boss can now take damage
        while (health > 0)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(ShootLaserWithCharge());
        }
    }

    IEnumerator ShootLaserWithCharge()
    {
        // Start the charging animation
        animator.SetBool("isCharging", true);

        // Wait for the charge time
        yield return new WaitForSeconds(chargeTime);

        // Stop the charging animation and shoot the laser
        animator.SetBool("isCharging", false);
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        ChargedLaser chargedLaser = laser.GetComponent<ChargedLaser>();
        if (chargedLaser != null)
        {
            chargedLaser.Initialize(transform, laserSpawnPoint.position); // Pass the boss's transform and the spawn point to the laser
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && other.CompareTag("PlayerBullet"))
        {
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject); // Destroy the bullet on impact
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            // Trigger the damage animation
            animator.SetTrigger("TakeDamage");
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Notify wave manager (or similar) that the enemy has been destroyed
        WaveSystem waveSystem = FindObjectOfType<WaveSystem>();

        // Boss death logic
        Destroy(gameObject);

        if (waveSystem != null)
        {
            waveSystem.NotifyEnemyDestroyed(gameObject);
        }
    }
}
