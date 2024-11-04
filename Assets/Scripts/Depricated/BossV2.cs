using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 100;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
            if (waveSystem != null)
            {
                waveSystem.NotifyEnemyDestroyed(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
