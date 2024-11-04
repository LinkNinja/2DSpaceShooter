using UnityEngine;

public class DiagonalChaser : MonoBehaviour
{
    public float diagonalSpeed = 2f;
    public float chaseSpeed = 5f;
    private Transform player;
    private bool isAligned = false;
  

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;

        // Check if the enemy is aligned horizontally or vertically with the player
        if (Mathf.Abs(transform.position.x - player.position.x) < 0.1f || Mathf.Abs(transform.position.y - player.position.y) < 0.1f)
        {
            isAligned = true;
        }

        // Move the enemy
        if (isAligned)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += (Vector3)(direction * diagonalSpeed * Time.deltaTime);
        }
    }


}
