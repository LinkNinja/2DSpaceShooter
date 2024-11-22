using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{

    public float moveSpeed = 5;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;


        if(pos.x < -10)
        {
            // Notify wave manager that enemy has been destroyed
            WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
            if (waveSystem != null)
            {
                Destroy(gameObject);
                waveSystem.NotifyEnemyDestroyed(gameObject);
            }
            
        }
        transform.position = pos;
    }
}
