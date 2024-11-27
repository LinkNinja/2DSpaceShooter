using UnityEngine;

public class ChargedLaser : MonoBehaviour
{
    public Transform bossTransform; 
    public Vector3 offset; 
    public float duration = .1f; 
    public int damage = 20; 

    void Start()
    {
        // Destroy the laser after its duration
        Destroy(gameObject, duration);
    }

    void Update()
    {
        // Follow the boss's position while maintaining the offset
        if (bossTransform != null)
        {
            transform.position = bossTransform.position + offset;
        }
    }

    public void Initialize(Transform boss, Vector3 spawnPoint)
    {
        bossTransform = boss;
        // Set the initial offset based on the current positions of the boss and laser spawn point
        offset = transform.position - bossTransform.position;
        // Set the initial position to the spawn point
        transform.position = spawnPoint; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a script with a TakeDamage method
            //other.GetComponent<PlayerControls_V2>().TakeDamage(damage);
        }
    }
}
