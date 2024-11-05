using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public bool canBeDestroyedOnCollision = true;
    public int collisionDamage = 20;
    public GameObject explosionPrefab; // Reference to the explosion prefab
    public int scoreValue = 100; // Points for destroying enemy.
    public EnemyDropFragments enemyDropFragments;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
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
        Debug.Log(damage);
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
            enemyDropFragments.DropFragment();
        }
    }

    void DestroyEnemy()
    {

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation); // Create the explosion
        Destroy(gameObject);
      
        // Notify wave manager that the enemy has been destroyed
        WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
        if (waveSystem != null)
        {
            waveSystem.NotifyEnemyDestroyed(gameObject);
        }
    }
}
