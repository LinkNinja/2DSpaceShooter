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
            yield return new WaitForSeconds(2f);
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
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
