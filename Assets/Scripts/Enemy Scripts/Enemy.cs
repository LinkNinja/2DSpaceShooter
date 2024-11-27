using UnityEngine;

public class Enemy : MonoBehaviour
{


    public int health = 100;
    public bool canBeDestroyedOnCollision = true;
    public int collisionDamage = 20;
    public GameObject explosionPrefab; 
    public int scoreValue = 100;
    public EnemyDropFragments enemyDropFragments;
    public Animator animator;

    private void Start()
    {
        // Initialize the Animator component
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the enemy is colliding with the player or players bullet
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            // Reference the player projectile script.
            Player_Projectile bullet = other.GetComponent<Player_Projectile>();

            //Check if the bullet reference is empty
            if (bullet != null)
            {
                // Run the Take damage function
                TakeDamage(bullet.damage);
                // Destroy the bullet on impact
                Destroy(other.gameObject); 
            }
        }

        // Check if Enemy is hit by the Charged Bullet from the player.
        if (other.CompareTag("ChargedBullet"))
        {
            // Destroy the enemy instantly. BUT they do not drop fragments.
            // **Depending on play testing might implement fragments here.**
            DestroyEnemy();
        }
    }


    // Function to Handle Enemy Damage.
    // Reduces health by the damage amount.
    // Plays the animation on the enemy when they take damage.
    // Runs the Destroy Enemy Function when health is below 0
    // Runs the Drop Fragment Function from the EnemyDropFragments script.
    public void TakeDamage(int damage)
    {
        Debug.Log(damage);

        // Reduce Health
        health -= damage;

        // Trigger the damage animation
        animator.SetTrigger("TakeDamage");

        // Check if player health is below 0
        if (health <= 0)
        {
            // Handle Enemy Destruction
            DestroyEnemy();
            // Enemy Drops Fragments
            enemyDropFragments.DropFragment();
        }
    }


    // Function to Handle enemy Destruction.
    // Updates the UI to increment the score with the enemy score value.
    // Notifies the wave manager when the enemy has been destroyed or moved off screen.
    // Instantiates the explosion prefab.
    void DestroyEnemy()
    {
        // Get Reference the game manager
        GameManager gameManager = FindObjectOfType<GameManager>();

        // Check if the game manager is empty
        if (gameManager != null)
        {
            // Add Score
            gameManager.AddScore(scoreValue);
        }
        // Create the explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //Remove the enemy from the game.
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
