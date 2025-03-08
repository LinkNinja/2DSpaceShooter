using UnityEngine;

public class Enemy : MonoBehaviour
{


    public FloatReference Health;
    //public FloatReference MaxHealth;
    public float health;
    public bool canBeDestroyedOnCollision = true;
    public int collisionDamage = 20;
    public GameObject explosionPrefab; 
    public int scoreValue = 100;
    public EnemyDropFragments enemyDropFragments;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = Health.Value;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();

            if (bullet != null)
            {            
                TakeDamage(bullet.damage);
                Destroy(other.gameObject); 
            }
        }

        if (other.CompareTag("ChargedBullet"))
        {
            DestroyEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);

        health -= damage;

        animator.SetTrigger("TakeDamage");

    }

    void DestroyEnemy()
    {

        GameManager gameManager = FindObjectOfType<GameManager>();

        // Check if the game manager is empty
        if (gameManager != null)
        {
            // Add Score
            gameManager.AddScore(scoreValue);
        }
        // Create the explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);


        if (health <= 0)
        {
            DestroyEnemy();
            enemyDropFragments.DropFragment();
        }
        Destroy(gameObject);
      
        
        WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
        //Check if wave system is empty.
        if (waveSystem != null)
        {
            // Notify wave manager that the enemy has been destroyed
            waveSystem.NotifyEnemyDestroyed(gameObject);
        }
    }
}
