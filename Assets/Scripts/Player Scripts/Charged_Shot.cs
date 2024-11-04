using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charged_Shot : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;

        if (transform.position.x > 8)
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
        }
    }

}
