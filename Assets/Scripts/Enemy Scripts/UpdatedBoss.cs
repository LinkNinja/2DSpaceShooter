using System.Collections;
using UnityEngine;

public class UpdatedBoss : MonoBehaviour
{
    public int health = 1000;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;

    // Array to hold references to the cannons
    public Cannon[] cannons; 
    public float moveSpeed = 2f;
    private bool movingUp = true;
    private bool phaseTwo = false;
    private bool canTakeDamage = false;
    private bool isChargingOrFiring = false; 
    public Animator animator;

    // Time before firing the laser
    public float chargeTime = 2.0f; 

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
            // Upper limit
            if (transform.position.y >= 2.25) 
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            // Lower limit
            if (transform.position.y <= 0) 
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
        StartCoroutine(ShootLaserWithCharge());
        while (health > 0)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(ShootLaserWithCharge());
        }
    }

    IEnumerator ShootLaserWithCharge()
    {
        // Indicate the boss is charging or firing
        isChargingOrFiring = true;

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
            // Pass the boss's transform and the spawn point to the laser
            chargedLaser.Initialize(transform, laserSpawnPoint.position); 
        }

        // Wait for a short duration if you want the boss to remain invulnerable while the laser is active
        yield return new WaitForSeconds(1.0f); // Adjust the time as needed

        // Indicate the boss has finished charging and firing
        isChargingOrFiring = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && !isChargingOrFiring && other.CompareTag("PlayerBullet"))
        {
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                // Destroy the bullet on impact
                Destroy(other.gameObject); 
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage && !isChargingOrFiring)
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
